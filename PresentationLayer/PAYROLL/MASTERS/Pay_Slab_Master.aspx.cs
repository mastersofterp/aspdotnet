using System;
using System.Collections;
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
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.IO;

public partial class PAYROLL_MASTERS_Pay_Slab_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();   
    PayController objPaySlab = new PayController();

    string UsrStatus = string.Empty;

    #region Page Load

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ViewState["action"] = "add";
                BindListView();
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "onLoad();", true);
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PayDepartment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Slab_Master.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objPaySlab.GetSlabCalculation();

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvSlab.DataSource = ds;
                lvSlab.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Slab_Master.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle;
            //",@P_Session=" + Session["Session"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString() ++ ",@P_WorkingDate=" + Session["WorkingDate"].ToString().Trim()


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Slab_Master.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    private void Clear()
    {
        txtSlabName.Text = "";
        txtFromSlab.Text = "";
        txtToSlab.Text = "";
        txtAmount.Text = string.Empty;
        ViewState["action"] = "add";
    }

    #endregion

    #region Page Events

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Payroll objPay = new Payroll();

        objPay.SLAB_NAME = txtSlabName.Text.Trim();
        objPay.FROM_SLAB = txtFromSlab.Text.Trim().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtFromSlab.Text.Trim());
        objPay.TO_SLAB = txtToSlab.Text.Trim().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtToSlab.Text.Trim());
        objPay.AMOUNT = txtAmount.Text.Trim().Equals(string.Empty) ? 0.00 : Convert.ToDouble(txtAmount.Text.Trim());

        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                //objPay.PTSLABID = 0;
                objPay.PTSLABID = -1;
                CustomStatus cs = (CustomStatus)objPaySlab.AddUpdateSlabCalculation(objPay);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindListView();
                    objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                    ViewState["PTSLABID"] = null;
                    Clear();
                }
                else
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
            }
            else
            {
                ViewState["action"] = "add";
                objPay.PTSLABID = Convert.ToInt32(ViewState["PTSLABID"].ToString().Trim());
                CustomStatus cs = (CustomStatus)objPaySlab.AddUpdateSlabCalculation(objPay);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindListView();
                    objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                    ViewState["PTSLABID"] = null;
                    Clear();
                }
                else
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
            }
        }
    }

    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PTSLABID = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PTSLABID);
            ViewState["action"] = "edit";
          
            //ImageButton btnEdit = sender as ImageButton;
            //ViewState["lblFNO"] = int.Parse(btnEdit.CommandArgument);
            //ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            //Label lblDeptName = lst.FindControl("lblDept") as Label;
            //Label lblDeptShortName = lst.FindControl("lblDeptShort") as Label;
            //Label lblDeptKannadName = lst.FindControl("lblDeptKannada") as Label;          
            //txtSlabName.Text = lblDeptName.Text.Trim();
            //txtFromSlab.Text = lblDeptShortName.Text.Trim();
            //txtToSlab.Text = lblDeptKannadName.Text.Trim();
            //ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Slab_Master.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetails(Int32 PTSLABID)
    {
        DataSet ds = null;
        try
        {
            ds = objPaySlab.GetSlabCalculationById(PTSLABID);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PTSLABID"] = PTSLABID.ToString();
                txtSlabName.Text = ds.Tables[0].Rows[0]["SLAB_NAME"].ToString();
                txtFromSlab.Text = ds.Tables[0].Rows[0]["FROM_SLAB"].ToString();
                txtToSlab.Text = ds.Tables[0].Rows[0]["TO_SLAB"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();    
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Slab_Master.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {  
        //ShowReport("ITDEDUCTIONHEADS", "Pay_ITDeductionHead.rpt");       
    }

    #endregion
}