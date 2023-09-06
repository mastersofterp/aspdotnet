//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COUNTER 1
// CREATION DATE : 01-MARCH-2014
// CREATED BY    : RENUKA ADULKAR
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public partial class Academic_GenerateOfferLetter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();

    #region page
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
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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

                //fill sessions
                //string sessionno = objCommon.LookUp("ACD_ADMCONFIGURATION", "SESSIONNO", string.Empty);
                //if (string.IsNullOrEmpty(sessionno))
                //{
                //    objCommon.DisplayMessage("Please Set the Admission Session from Reference Page!!", this.Page);
                //    return;
                //}
                //else
                //{
                objCommon.FillDropDownList(ddlSession, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                ddlSession.Items.RemoveAt(0);
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
               
                //}

            }
            txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


    }

    #endregion

    #region User Defined Methods



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GenerateOfferLetter.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GenerateOfferLetter.aspx");
        }
    }
    #endregion

    #region button
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        CheckBox chkAllot = dataitem.FindControl("chkAllot") as CheckBox;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnOfferletter_Click(object sender, EventArgs e)
    {
        string studentIds = string.Empty;
        //Get Selected Students..
        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("chkAllot") as CheckBox;
            if (chkBox.Checked == true)
            {
                studentIds += chkBox.ToolTip + "$";
            }
        }
        if (studentIds.Length <= 0)
        {
            objCommon.DisplayMessage("Please Select Atleast One Student.", this.Page);
            return;
        }
        this.ShowOfferLetterReport("Offer Letter", "OfferLetterBulk.rpt", studentIds);
    }
    private void ShowOfferLetterReport(string reportTitle, string rptFileName, string userno)
    {
        try
        {
            int Report_Type = 0;
            if (ddlListType.SelectedValue == "1")
            {
                Report_Type = 1;
            }
            else if (ddlListType.SelectedValue == "2")
            {
                Report_Type = 2;
            }
            else
            {
                Report_Type = 3;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "filename=" + "offerletter" + ".pdf";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno + ",@P_REPORT_TYPE=" + Report_Type + ",@P_PRINT_DATE=" + Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyyy") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue).ToString() + ",@P_ENTERANCE=" + Convert.ToInt32(ddlEntrance.SelectedValue).ToString() + ",@P_ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue).ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd/MMM/yyyy") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd/MMM/yyyy");
            ///url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno + ",@P_REPORT_TYPE=" + Report_Type + ",@P_PRINT_DATE=" + Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue).ToString() + ",@P_ENTERANCE=" + Convert.ToInt32(ddlEntrance.SelectedValue).ToString() + ",@P_ROUNDNO=" + Convert.ToInt32(ddlRound.SelectedValue).ToString() + ",@P_FROM_DATE=" + txtFromDate.Text.ToString() + ",@P_TO_DATE=" + txtToDate.Text.ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_gradeCard .ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlListType.SelectedValue == "1")
                bindallotedlist(Convert.ToInt32(ddlSession.SelectedValue), 1, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
            else if (ddlListType.SelectedValue == "2")
                bindallotedlist(Convert.ToInt32(ddlSession.SelectedValue), 2, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));
            else if (ddlListType.SelectedValue == "3")
                bindallotedlist(Convert.ToInt32(ddlSession.SelectedValue), 3, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_GenerateOfferLetter.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void bindallotedlist(int sessionno, int status, int degree, int Entrance, int round)
    {
        DataSet ds = objSC.GetConfirmWaitingbothStudents(sessionno, status, degree, Entrance, round);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // pnlStudents.Visible = true;
            lvStudents.DataSource = ds.Tables[0];
            lvStudents.DataBind();
            lvStudents.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage("Record not found", this.Page);
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
        }
    }


    protected void ddlListType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        //pnlStudents.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlEntrance, "ACD_ENTRE_DEGREE ED INNER JOIN ACD_QUALEXM Q ON(ED.QUALIFYNO=Q.QUALIFYNO)", "ED.QUALIFYNO", "Q.QUALIEXMNAME", "ED.DEGREENO=" + ddlDegree.SelectedValue, "QUALIFYNO DESC");
        }
    }


    protected void btnDownload_Click(object sender, EventArgs e)
    {


        string studentIds = string.Empty;
        //Get Selected Students..
        ReportDocument customReport = new ReportDocument();

        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            CheckBox chkBox = lvItem.FindControl("chkAllot") as CheckBox;
            if (chkBox.Checked == true)
            {

                studentIds = chkBox.ToolTip;
                string reportPath = Server.MapPath(@"~,Reports,Academic,OfferLetterBulk.rpt".Replace(",", "\\"));
                customReport.Load(reportPath);
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;
                crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
                crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                CrTables = customReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
                int Report_Type = 0;
                if (ddlListType.SelectedValue == "1")
                {
                    Report_Type = 1;
                }
                else if (ddlListType.SelectedValue == "2")
                {
                    Report_Type = 2;
                }
                else
                {
                    Report_Type = 3;
                }
                string clgname = Session["coll_name"].ToString();
                customReport.SetParameterValue("@P_COLLEGE_CODE", 9);
                customReport.SetParameterValue("@P_USERNO", studentIds);
                customReport.SetParameterValue("@P_REPORT_TYPE", Report_Type);
                customReport.SetParameterValue("@P_PRINT_DATE", Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy"));


                string username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Convert.ToInt32(studentIds));
                string userfullname = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME + '_'+ LASTNAME AS NAME", "USERNO=" + Convert.ToInt32(studentIds));


                string directoryPath = "E:\\OfferLetterMail\\" + ddlListType.SelectedItem.Text + "\\";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                customReport.ExportToDisk(ExportFormatType.PortableDocFormat, directoryPath + username + "_" + userfullname.Replace(" ", "") + ".pdf");


            }
        }
        objCommon.DisplayMessage("Offer Letter Download Successfully", this.Page);
        if (studentIds.Length <= 0)
        {
            objCommon.DisplayMessage("Please Select Student", this.Page);
            return;
        }


    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string studentIds = string.Empty;
            ReportDocument customReport = new ReportDocument();
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox chk = item.FindControl("chkAllot") as CheckBox;
                HiddenField hdfuserno = item.FindControl("hidIdNo") as HiddenField;
                HiddenField hdfAppli = item.FindControl("hdfAppliid") as HiddenField;
                HiddenField hdfEmailid = item.FindControl("hdfEmailid") as HiddenField;
                HiddenField hdfirstname = item.FindControl("hdfirstname") as HiddenField;
                HiddenField hdlastname = item.FindControl("hdlastname") as HiddenField;

                
                if (chk.Checked == true && chk.Enabled == true)
                {
                 
                    studentIds = hdfuserno.Value + "$";


                    string reportPath = Server.MapPath(@"~,Reports,Academic,OfferLetterBulk.rpt".Replace(",", "\\"));
                    customReport.Load(reportPath);

                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables;

                    crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
                    crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                    crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                    crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                    CrTables = customReport.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }

                    //Parameter to Report Document
                    //================================
                    //Extract Parameters From queryString
                    int Report_Type = 0;
                    if (ddlListType.SelectedValue == "1")
                    {
                        Report_Type = 1;
                    }
                    else if (ddlListType.SelectedValue == "2")
                    {
                        Report_Type = 2;
                    }
                    else
                    {
                        Report_Type = 3;
                    }
                    
                    string clgname = Session["coll_name"].ToString();
                    customReport.SetParameterValue("@P_COLLEGE_CODE", Session["colcode"].ToString());
                    customReport.SetParameterValue("@P_USERNO", studentIds);
                    customReport.SetParameterValue("@P_REPORT_TYPE", Report_Type);
                    customReport.SetParameterValue("@P_PRINT_DATE", Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy"));
                    customReport.SetParameterValue("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedValue).ToString());
                    customReport.SetParameterValue("@P_ENTERANCE", Convert.ToInt32(ddlEntrance.SelectedValue).ToString());
                    customReport.SetParameterValue("@P_ROUNDNO", Convert.ToInt32(ddlRound.SelectedValue).ToString());
                    customReport.SetParameterValue("@P_FROM_DATE" , Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy"));
                    customReport.SetParameterValue("@P_TO_DATE" , Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy"));
                    //customReport.SetParameterValue("@P_ADMBATCH", Convert.ToInt32(ddlAdmbatch.SelectedValue));
                    //customReport.SetParameterValue("@P_ADMTYPE", Convert.ToInt32(ddlAdmissionType.SelectedValue));
               
                    string path = Server.MapPath("OfferLetter");
                    var directoryInfo = new DirectoryInfo(path);
                    if (!(Directory.Exists(path)))
                    {
                        Directory.CreateDirectory(path);
                    }
                    else
                    {
                        customReport.ExportToDisk(ExportFormatType.PortableDocFormat, path + hdfAppli.Value + ".pdf");
                    }

                  
                    string sendersemailid = objCommon.LookUp("Reff", "EMAILSVCID", String.Empty);
                    string senderspwd = objCommon.LookUp("Reff", "EMAILSVCPWD", String.Empty);
                    if (hdfEmailid.Value == "")
                    {
                        objCommon.DisplayMessage("Kindly check Email Id .", this.Page);
                    }
                    else
                    {

                        string EmailTemplate = "<html><body>" +
                                             "<div" +
                                             "<table style=\"width:602px;\" cellspacing=\"0\" cellpadding=\"0\">" +
                                              "<tr>" +
                                              "<td>" + "</tr>" +
                                              "<tr>" +
                                             "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                             "</tr>" +

                                             "</table>" +
                                             "</div>" +
                                             "</body></html>";
                        StringBuilder mailBody = new StringBuilder();
                        //mailBody.AppendFormat("<b>Application ID : " + objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + hdfuserno.Value) + "</b>");
                        //mailBody.AppendFormat("<br />");
                        //mailBody.AppendFormat("<br /><b>Email ID : " + empemailid + "</b>");
                        //mailBody.AppendFormat("<br />");
                        mailBody.AppendFormat("<u><center><b><p>Allotment Letter for Admissions-2019-20<p></b></center></u>");
                        mailBody.AppendFormat("<br />");
                        mailBody.AppendFormat("Dear " +hdfirstname.Value+ hdlastname.Value);
                        mailBody.AppendFormat("<br />");
                        mailBody.AppendFormat("<p> Congratulations! We are pleased to inform you that based on your score, you have been allotted a seat at the Sarala Birla University, Ranchi as per letter attached.<p>");
                        ////mailBody.AppendFormat("<p>Please download the attached  Offer Letter and bring the print-out to the Exam Hall. Admit Card is also uploaded on your Admission Portal.</p>");
                        //mailBody.AppendFormat("<br />");
                        mailBody.AppendFormat("<br /><b> Regards,<b>");
                        mailBody.AppendFormat("<br />");
                        mailBody.AppendFormat("<br /><b> Registrar,<b>");
                        mailBody.AppendFormat("<br />");
                        mailBody.AppendFormat("<br /> <b>Sarala Birla University, Ranchi<b>");
                        //mailBody.AppendFormat("<br />");
                        //mailBody.AppendFormat("<br /><b>Aizawl, Mizoram<b>");
                        string Mailbody = mailBody.ToString();
                        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.From = new MailAddress(HttpUtility.HtmlEncode(sendersemailid));
                        msg.To.Add(hdfEmailid.Value);
                        msg.Body = nMailbody;
                        msg.Attachments.Add(new Attachment(path + hdfAppli.Value + ".pdf"));
                        msg.IsBodyHtml = true;
                        msg.Subject = "Allotment of Seat in Sarala Birla University, Ranchi";
                        SmtpClient smt = new SmtpClient("smtp.gmail.com");
                        smt.Port = 587;
                        smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(sendersemailid), HttpUtility.HtmlEncode(senderspwd));
                        smt.EnableSsl = true;
                        // smtpClient.EnableSsl = true;

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        //smtpClient.EnableSsl = true;

                        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smt.Send(msg);
                        string script = "<script>alert('Mail Sent Successfully')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
                        msg.Attachments.Dispose();

                    }

                    if (File.Exists(path + hdfAppli.Value + ".pdf"))
                    {
                        File.Delete(path + hdfAppli.Value + ".pdf");
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AdmitCard.btnSendEmail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
}
   