//============================================================
// CREATED BY    : MRUNAL SINGH
// CREATION DATE : 28-10-2019
// DESCRIPTION   : RETURN BACK DISPATCG ENTRY FORM
//============================================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class DISPATCH_Transactions_ReturnDispatch : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();
    IOTRAN objIOtran = new IOTRAN();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    ViewState["action"] = null;
                    //  BindListViewOutwardDispatch();
                    //  objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                }
               // BindListViewOutwardDispatch();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // it is used check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_OutwardDispatch.aspx");
        }
    }


    // This button is used to search dispatch
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            
            DataSet ds = null;
            ds = objIOtranc.SearchDispatch(txtRefNo.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                divDetails.Visible = true;
                hdnIOTRANNO.Value = ds.Tables[0].Rows[0]["IOTRANNO"].ToString(); 
                //txtFrom.Text = ds.Tables[0].Rows[0]["IOFROM"].ToString();
                //txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                //txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                //txtAddLine.Text = ds.Tables[0].Rows[0]["ADDRESS_LINE"].ToString();
                //txtCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
                //txtState.Text = ds.Tables[0].Rows[0]["STATENAME"].ToString();
                //txtPIN.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                //txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRYNAME"].ToString();
                //txtPeon.Text = ds.Tables[0].Rows[0]["PEON"].ToString();
                //txtToUser.Text = ds.Tables[0].Rows[0]["TOUSER"].ToString();
                //txtReturnDT.Text = ds.Tables[0].Rows[0]["RETURN_DATE"].ToString();
                //txtRRemark.Text = ds.Tables[0].Rows[0]["RETURN_REMARK"].ToString();
            }
            else
            {
                divDetails.Visible = false;
                objCommon.DisplayMessage(this.updActivity, "No Record Found.", this.Page);
                return;
            }


            DataSet dsRec = objIOtranc.GetRecieverOutwardDispatchByIotranNo(Convert.ToInt32(hdnIOTRANNO.Value));
            if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
            {
                lvTo.DataSource = dsRec.Tables[0];
                lvTo.DataBind();
                divList.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnSearch_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }


    // This button is used to update return remark.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {       
        try
        {
            if (hdnIOTRANNO.Value != "")
            {
                DateTime strRDate = Convert.ToDateTime(txtReturnDT.Text);
                string strRRemark = txtRRemark.Text;
                objIOtran.IOTRANNO = Convert.ToInt32(hdnIOTRANNO.Value);

                CustomStatus cs = (CustomStatus)objIOtranc.UpdateReturnRemark(objIOtran, strRDate, strRRemark);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    Clear_Controls();
                    BindListViewOutwardDispatch();
                    objCommon.DisplayUserMessage(updActivity, "Record Saved Successfully.", this);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updActivity, "Please Search Dispatch.", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //This method is used to clear control.
    protected void Clear_Controls()
    {
       divDetails.Visible= false;
       hdnIOTRANNO.Value ="";
       txtRefNo.Text = string.Empty;
       txtFrom.Text =    string.Empty;
       txtSubject.Text = string.Empty;
       txtAddress.Text = string.Empty;
       txtAddLine.Text = string.Empty;
       txtCity.Text =    string.Empty;
       txtState.Text =   string.Empty;
       txtPIN.Text =     string.Empty;
       txtCountry.Text = string.Empty;
       txtPeon.Text =    string.Empty;
       txtToUser.Text = string.Empty;
       txtReturnDT.Text = string.Empty;
       txtRRemark.Text = string.Empty;
    }

    // It is used to cancel the selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Controls();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // It is used to get report.
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Return Dispatch", "ReturnDispatch.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("DISPATCH")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,DISPATCH," + rptFileName;           
            url += "&param=@P_IOTRANNO=0";


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_Dispatch.btnShow_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindListViewOutwardDispatch()
    {
        try
        {
           // DataSet ds = null;
           // ds = objIOtranc.GetDepartmentOutwards();
            DataSet ds = objCommon.FillDropDown("ADMN_IO_TRAN", " CENTRALREFERENCENO,CENTRALRECSENTDT", "RETURN_DATE,RETURN_REMARK", "IOTYPE='O' and RETURN_DATE is not null", "");


            if (ds.Tables[0].Rows.Count > 0)
            {
                IvOutwardDispatch.DataSource = ds;
                IvOutwardDispatch.DataBind();
                IvOutwardDispatch.Visible = true;
            }
            else
            {
                IvOutwardDispatch.DataSource = null;
                IvOutwardDispatch.DataBind();
                IvOutwardDispatch.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.BindListViewOutwardDispatch -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
}