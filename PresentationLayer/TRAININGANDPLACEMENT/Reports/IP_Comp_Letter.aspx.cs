using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class TRAININGANDPLACEMENT_Reports_IP_Comp_Letter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["TPSession"] = objCommon.LookUp("ACD_REFE", "ISNULL(TP_CURRENT_SESSIONNO,0)AS TP_CURRENT_SESSIONNO", "");

                objCommon.FillDropDownList(ddlBranch, "ACD_TP_INPLANT_TRAINING A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO )",
                                          "DISTINCT A.BRANCHNO", "LONGNAME AS BRANCH", "SESSIONNO=" + Convert.ToInt32(ViewState["TPSession"]), "A.BRANCHNO");
           }
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowReport("IPCompanyLetter", "IPCompLetter.rpt");
            ShowReportInWordCompLetter("doc", "Letter_To_Industry.rpt"); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_IP_Comp_Letter.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnJobResponse_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportInWordFormate("doc", "JobResponseForm.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_IP_Comp_Letter.btnJobResponse_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void ShowReportInWordFormate(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Job_Response_Form.doc";
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + "Job_Response_Form" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportInWordCompLetter(string exporttype, string rptFileName)
    {
        try
        {
           string sessionNo= objCommon.LookUp("ACD_REFE", "ISNULL(TP_CURRENT_SESSIONNO,0)AS TP_CURRENT_SESSIONNO", "");
           //string sessionName = objCommon.LookUp("ACD_TP_SESSION", "", "SESSIONNO="+Convert.ToInt32(sessionNo));

           DataSet ds = objCommon.FillDropDown("ACD_TP_SESSION", "SESSIONNAME", "CONVERT(NVARCHAR(40),FROMDATE,106)FROMDATE,CONVERT(NVARCHAR(40),TODATE,106)TODATE", "SESSIONNO=" + Convert.ToInt32(sessionNo), "");
            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Letter_To_Industry.doc";
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@SESSION=" + ds.Tables[0].Rows[0]["SESSIONNAME"].ToString() + ",@SESSIONDATE=" + ds.Tables[0].Rows[0]["FROMDATE"].ToString() + " to " + ds.Tables[0].Rows[0]["TODATE"].ToString();
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + "Letter_To_Industry" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
           int SESSIONNO = Convert.ToInt32(ViewState["TPSession"]);
            string SessionName = objCommon.LookUp("ACD_TP_SESSION", "SESSIONNAME", "SESSIONNO=" + SESSIONNO);
            int compid = 0;
            //if (ddlCompany.SelectedValue.Equals("Other"))
            //{
            //    compid = -1;
            //}
            //else
            //{
            //    compid = Convert.ToInt32(ddlCompany.SelectedValue);
            //}
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            url += "&param=@P_SESSION=" + SESSIONNO + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);
           
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_IP_Comp_Letter.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}
