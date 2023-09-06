using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using System.IO;


public partial class ACADEMIC_TcIssueReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                    // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                this.FillDropdown();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void FillDropdown()
    {
        try
        {


            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH ", "BRANCHNO", "LONGNAME", "BRANCHNO>0 ", "BRANCHNO");

            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_TcIssueReport.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void BindListview()
    {
        try
        {
            DataSet ds = objSC.GetTcDetails(Convert.ToInt32(ddlBranch.SelectedValue),txtFromDate.Text.ToString(), txtToDate.Text.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lvStudent.Visible = true;               
                lvStudent.Visible = true;
              
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                lvStudent.Visible = false;
                objCommon.DisplayMessage(this.UpdTc, "No Record found for selected criteria.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_TcIssueReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListview();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("TcIssueReport", "TransferCertificateReport.rpt", "pdf");
    }

    private void ShowReport(string reportTitle, string rptFileName, string exporttype)
    {
        string FROMDATE = string.Empty;
        string TODATE = string.Empty;
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        FROMDATE = txtFromDate.Text;
        TODATE = txtToDate.Text;
        try
        {

            //string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" +Session["colcode"].ToString() +",@P_BRANCHNO="+Convert.ToInt32(branchno)+ ",@P_FROMDATE=" + FROMDATE.ToString() + ",@P_TODATE= "+ TODATE.ToString() +"";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdTc, this.UpdTc.GetType(), "controlJSScript", sb.ToString(), true);
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.UpdTc, UpdTc.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_TcIssueReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}