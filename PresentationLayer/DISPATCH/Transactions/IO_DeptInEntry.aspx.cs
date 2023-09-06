using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Dispatch_Transactions_IO_DeptInEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    IOTranController objIOtranc = new IOTranController();
    IOTranController objIO = new IOTranController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IO_DeptInEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IO_DeptInEntry.aspx");
        }
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
                    pnlList.Visible = true;
                    pnlAdd.Visible = false;

                    ViewState["IONO"] = null;
                    BindListViewInward();
                    //txtReceivedDT.Text = System.DateTime.Now.ToString();
                }
            }
            divfrmdate.Visible = false;
            divtodate.Visible = false;
            btnShow.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_DeptInEntry.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void BindListViewInward()
    {
        try
        {
            DataSet ds = null;
            string uaIdNo = objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            if (uaIdNo != "")
            {
                ds = objIOtranc.GetAllInwardByIdN(Convert.ToInt32(uaIdNo));
            }
            else
            {
                if (Session["usertype"].ToString() == "8")
                {
                    string deptNo = objCommon.LookUp("user_acc", "UA_EMPDEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ds = objIOtranc.GetAllInwardByUserId(Convert.ToInt32(deptNo));
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvInward.DataSource = ds;
                lvInward.DataBind();
            }
            else
            {
                lvInward.DataSource = null;
                lvInward.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.BindListViewInward -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpLett_PreRender(object sender, EventArgs e)
    {
        BindListViewInward();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IOTRAN objIOtran = new IOTRAN();
        try
        {
            int dpno = Convert.ToInt16(objCommon.LookUp("user_acc", "Isnull(UA_EMPDEPTNO,0)", "UA_NO=" + Convert.ToInt32(Session["userno"]))); //-Modified condition and handle null value
            int Idno = Convert.ToInt16(objCommon.LookUp("user_acc", "isnull(UA_IDNO,0)", "UA_NO=" + Convert.ToInt32(Session["userno"])));
           
                if (ViewState["IONO"] != null)
                    objIOtran.DEPTRECSENTDT = Convert.ToDateTime(txtReceivedDT.Text);
                //objIOtran.DEPTREFERENCENO = txtRefNo.Text.Trim().Equals(string.Empty) ? string.Empty : txtRefNo.Text.Trim();
                objIOtran.DEPTREMARKS = txtRemarks.Text.Trim().Equals(string.Empty) ? string.Empty : txtRemarks.Text.Trim();
                objIOtran.IOTRANNO = Convert.ToInt32(ViewState["IONO"]);
                objIOtran.TODEPT = dpno;
                CustomStatus cs = (CustomStatus)objIOtranc.UpdateDept_InwardEntry(objIOtran, Idno);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                    pnlAdd.Visible = false;
                    BindListViewInward();
                    ViewState["IONO"] = null;
                }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_DeptInEntry.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_DeptInEntry.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReceived_Click(object sender, EventArgs e)
    {
        Button btnReceived = sender as Button;
        int IONO = int.Parse(btnReceived.CommandArgument);
        ViewState["IONO"] = IONO;
        pnlAdd.Visible = true;
        txtReceivedDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtRemarks.Text = string.Empty;
        lblFrom.Text = objCommon.LookUp("ADMN_IO_TRAN", "IOFROM", "IOTRANNO=" + IONO);
        lblSubject.Text = objCommon.LookUp("ADMN_IO_TRAN", "SUBJECT", "IOTRANNO=" + IONO);
        divfrmdate.Visible = false;
        divtodate.Visible = false;
        btnShow.Visible = false;


    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

            DataSet dsdate = objIOtranc.GetAllInwardDatewise(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), deptno);

            if (dsdate.Tables[0].Rows.Count > 0)
            {
                lvInward.DataSource = dsdate;
                lvInward.DataBind();
            }
            else
            {

                lvInward.DataSource = null;
                lvInward.DataBind();
                objCommon.DisplayMessage(this.updActivity, "Record Not Found...!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_DeptInEntry.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("DISPATCH")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,DISPATCH," + rptFileName;
            url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@P_DEPTNO=" + deptno + ",@P_USERNO=0,@P_POSTTYPE=0,@P_CHEQUE=0,@P_USERTYPE='',@P_TYPE=0";
            //url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd").Trim() + "," + "@P_DEPTNO=" + deptno + ",@P_USERNO=0,@P_POSTTYPE=0,@P_CHEQUE=0,username=" + Session["userfullname"].ToString() + ",@P_TYPE=0";  18/02/2022
            

            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updActivity, updActivity.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Reports_IO_OutwardDeptEntry.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDeptEntry.btnBack_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    protected void btnnotreceive_Click(object sender, EventArgs e)
    {
        try
        {
            int iotranno = Convert.ToInt32(ViewState["IONO"]);

            CustomStatus cs = (CustomStatus)objIOtranc.UpdateNotReceive(iotranno);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully", this.Page);
                pnlAdd.Visible = false;
                BindListViewInward();
                ViewState["IONO"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_DeptInEntry.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            int deptno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
            DataSet ds = objIO.VerifyInwardRecordDept(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(deptno), 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShowReport("Dispatch", "InwardRegister.rpt");
            }
            BindListViewInward();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_DeptInEntry.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
}