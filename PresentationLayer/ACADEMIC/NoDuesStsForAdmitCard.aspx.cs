using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_NoDuesStsForAdmitCard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    QrCodeController objQrC = new QrCodeController();
    int prev_status;

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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //ShowDetails();
                }

                if (rdoSingleStudent.Checked)
                {
                    pnlSingleStud.Visible = true;
                    objCommon.FillDropDownList(ddlSessionSingleStud, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                    // ddlSessionSingleStud
                }
                else
                {
                    pnlBulkStud.Visible = true;
                }
            }
            
            // lblSession.Text = Convert.ToString(Session["sessionname"]);

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN SCHEME TYPE
            //objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            // FILL DROPDOWN BATCH
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0", "college_id");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //  objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

            // FILL DROPDOWN ADMISSION BATCH

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds;
            int sessionno = Convert.ToInt32(Session["currentsession"]);

            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }
            ds = studCont.GetStudentListForAdmitCardNoDues(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), prev_status, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {



                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                lvStudentRecords.Visible = true;
                hftot.Value = lvStudentRecords.Items.Count.ToString();
                txtTotStud.Value = lvStudentRecords.Items.Count.ToString();
                foreach (ListViewDataItem itm in lvStudentRecords.Items)
                {

                   
                    RadioButton rdby = (RadioButton)itm.FindControl("rdoYes");
                    RadioButton rdbn = (RadioButton)itm.FindControl("rdoNo");

                    //rdby.InputAttributes.Add("disabled", "true");
                    //rdbn.InputAttributes.Add("disabled", "true");
                    string sts = ds.Tables[0].Rows[itm.DataItemIndex]["NODUES_STATUS"].ToString();

                    if (Convert.ToInt32(sts.ToString()) == 1)
                    //if ( Convert.ToInt32(sts.ToString())== 0)
                    {
                        rdbn.Checked = true;
                    }
                    else
                        if(sts.ToString()==string.Empty)
                    {
                        rdby.Checked = true;
                    }
                }
            }

            else
            {
                objCommon.DisplayMessage(updtime, "Record Not Found!!", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            Session["listIdCard"] = null;
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_PREV_STATUS=" + prev_status + ",@P_USER_FUll_NAME=" + Session["userfullname"];
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_PREV_STATUS=" + prev_status + ",@P_DATEOFISSUE=" + txtDateofissue.Text.ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updtime, this.updtime.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += ".";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", string.Empty, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            // ddlAdmbatch.SelectedIndex = 0;
            //  ddlAdmbatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear(); 
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON(S.SCHEMENO = B.SCHEMENO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue + " AND B.SESSIONNO=" + ddlSession.SelectedValue, "A.SEMESTERNO");            
            ddlSemester.Focus();
        }
        else
        {
            //ddlBranch.Items.Clear();
            //ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }

    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }

    //This Method Generate QR-CODE & also  save image in ACD_STUD_PHOTO Table & QR-Code Files Folder.

    private void GenerateQrCode(string idno, string regno)
    {

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT WITH (NOLOCK)", "*", "", "REGNO='" + regno + "'", "REGNO");
        if (rbRegEx.SelectedIndex == 0)
        {
            prev_status = 0;
        }
        else
        {
            prev_status = 1;
        }
        // int sessionno = Convert.ToInt32(Session["currentsession"]);
        // string dateOfIssue = txtDateofissue.Text.ToString();
        // DataSet ds1 = objQrC.GetDetailsForAdmitCard(sessionno, Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), prev_status, Convert.ToInt16(idno), dateOfIssue);
        DataSet ds1 = objQrC.GetDetailsForAdmitCard(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), prev_status, Convert.ToInt16(idno));

        //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";
        string Qrtext = "RollNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";Degree=" +
                                 ds1.Tables[0].Rows[0]["DEGREENAME"].ToString().Trim() + ";Semester=" +
                                ds1.Tables[0].Rows[0]["SEMESTER"].ToString().Trim() + "";



        Session["qr"] = Qrtext.ToString();
        QRCodeEncoder encoder = new QRCodeEncoder();
        encoder.QRCodeVersion = 10;
        Bitmap img = encoder.Encode(Session["qr"].ToString());

        //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
        img.Save(Server.MapPath("~\\img.Jpeg"));
        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

        //img.Save(Server.MapPath("~\\img.Jpeg"));
        byte[] QR_IMAGE = ViewState["File"] as byte[];
        long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds.Tables[0].Rows[0]["IDNO"].ToString().Trim()), QR_IMAGE);
    }

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))
            {
                string studentIds = string.Empty;
                foreach (ListViewDataItem lvItem in lvStudentRecords.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("chkReport") as CheckBox;
                    if (chkBox.Checked == true)
                    {
                        studentIds += chkBox.ToolTip + ",";
                        string RegNo = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "REGNO", "IDNO=" + Convert.ToInt16((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip) + ""));
                        //  GenerateQrCode((((lvItem.FindControl("chkReport")) as CheckBox).ToolTip), RegNo);
                    }
                }

              //  int chkg = studCont.InsAdmitCardLog(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), studentIds, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
                //ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");  
                ShowReport(ids, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");
                //ShowReport(ids, "Student_Admit_Card_Report", "DemoReport.rpt"); 
            }
            else
            {
                objCommon.DisplayMessage("Please Select Students!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    //sending from mail aayushi gupta

    protected void btnSendEmail_Click1(object sender, EventArgs e)
    {
        {
            try
            {
                DataSet d = new DataSet();
                string studentIds = string.Empty;
                ReportDocument customReport = new ReportDocument();
                //DataSet ds = objCommon.FillDropDown("Reff", "EMAILSVCID", "EMAILSVCPWD",string.Empty,string.Empty);
                foreach (ListViewDataItem item in lvStudentRecords.Items)
                {
                    CheckBox chk = item.FindControl("chkReport") as CheckBox;
                    HiddenField hdfuserno = item.FindControl("hidIdNo") as HiddenField;
                    HiddenField hdfAppli = item.FindControl("hdfAppliid") as HiddenField;
                    HiddenField Hdfemail = item.FindControl("Hdfemail") as HiddenField;

                    if (chk.Checked == true && chk.Enabled == true)
                    {
                        studentIds += hdfuserno.Value + "$";

                        string reportPath = Server.MapPath(@"~,Reports,Academic,rptBulkExamRegslip.rpt".Replace(",", "\\"));
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

                        if (rbRegEx.SelectedIndex == 0)
                        {
                            prev_status = 0;
                        }
                        else
                        {
                            prev_status = 1;
                        }

                        //Parameter to Report Document
                        //================================
                        //Extract Parameters From queryString
                        customReport.SetParameterValue("@P_IDNO", hdfuserno.Value);
                        customReport.SetParameterValue("@P_SESSIONNO", Convert.ToInt32(ddlSession.SelectedValue));
                        customReport.SetParameterValue("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedValue));
                        customReport.SetParameterValue("@P_BRANCHNO", Convert.ToInt32(ddlBranch.SelectedValue));
                        customReport.SetParameterValue("@P_SEMESTERNO", Convert.ToInt32(ddlSemester.SelectedValue));
                        customReport.SetParameterValue("@P_PREV_STATUS", Convert.ToInt32(prev_status));
                        customReport.SetParameterValue("@P_USER_FUll_NAME", Session["userfullname"]);
                        customReport.SetParameterValue("@P_COLLEGE_CODE", 0);

                        string path = Server.MapPath("~/AdmitCardMail\\");
                        if (!(Directory.Exists(path)))
                            Directory.CreateDirectory(path);
                        customReport.ExportToDisk(ExportFormatType.PortableDocFormat, path + hdfAppli.Value + ".pdf");


                        DataSet ds = objCommon.FillDropDown("REFF WITH (NOLOCK)", "EMAILSVCID", "EMAILSVCPWD", string.Empty, string.Empty);
                        var fromAddress = ds.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                        //const string fromPassword = "MUadmission2016";
                        string fromPassword = ds.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                        if (Hdfemail.Value == "")
                        {
                            objCommon.DisplayMessage("Kindly check Email Id .", this.Page);
                        }
                        else
                        {
                            string EmailTemplate = "<html><body>" +
                                                "<div align=\"center\">" +
                                                "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                                 "<tr>" +
                                                 "<td>" + "</tr>" +
                                                 "<tr>" +
                                                "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><B>Regards,<br/><br/><b>Controller Of Examination <br/>Indus University</td>" +
                                                "</tr>" +
                                                "</table>" +
                                                "</div>" +
                                                "</body></html>";
                            StringBuilder mailBody = new StringBuilder();
                            mailBody.AppendFormat("<h1>Greetings !!</h1>");
                            mailBody.AppendFormat("Dear " + objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_FULLNAME", "UA_IDNO=" + hdfuserno.Value));
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            //mailBody.AppendFormat("<p>Your Admit Card is Generated.</p>");
                            mailBody.AppendFormat("<br />");
                            mailBody.AppendFormat("<br />");
                            // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                            string sessionnoname = objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSION_NAME", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue));
                            mailBody.AppendFormat("<p>Please find the following attachment for the Hall Ticket of END-SEM Examination <b>" + sessionnoname + "</b> .</p>");
                            mailBody.AppendFormat("<br />");
                            string Mailbody = mailBody.ToString();
                            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                            //msg.From = new MailAddress(HttpUtility.HtmlEncode(sendersemailid));
                            msg.From = new MailAddress(HttpUtility.HtmlEncode(fromAddress));
                            msg.To.Add(Hdfemail.Value);
                            msg.Body = nMailbody;
                            msg.Attachments.Add(new Attachment(path + hdfAppli.Value + ".pdf"));
                            msg.IsBodyHtml = true;
                            msg.Subject = "Hall Ticket For " + sessionnoname;
                            SmtpClient smt = new SmtpClient();
                            smt.Host = "smtp.gmail.com";
                            smt.Port = 587;
                            smt.UseDefaultCredentials = true;
                            smt.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);

                            smt.EnableSsl = true;
                            // smt.Send(msg);
                            //SmtpClient smt = new SmtpClient("smtp.gmail.com");
                            //smt.Port = 587;
                            //smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromAddress), HttpUtility.HtmlEncode(fromPassword));
                            //smt.EnableSsl = true;
                            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                            smt.Send(msg);
                            objCommon.DisplayMessage(Page, "Mail Sent Successfully For Selected Student(s)!!", this);
                            //  objCommon.DisplayMessage("Mail Sent Successfully !!", this.Page);
                            //string script = "<script>alert('Mail Sent Successfully')</script>";
                            //ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
                            msg.Attachments.Dispose();
                            // BindListView();
                        }
                        if (File.Exists(path + hdfAppli.Value + ".pdf"))
                        {
                            File.Delete(path + hdfAppli.Value + ".pdf");
                        }

                    }

                }
                if (studentIds.Equals(""))
                {
                    objCommon.DisplayMessage("Please Select at least one Student!", this.Page);

                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "AdmitCard.btnSendSMS_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            //objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();
            objS.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);//Convert.ToInt32(Session["currentsession"]);

            objS.StudType = Convert.ToInt32(rbRegEx.SelectedValue.ToString());

            foreach (ListViewDataItem itm in lvStudentRecords.Items)
            {
                CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                HiddenField hdnf = itm.FindControl("hidIdNo") as HiddenField;
                HiddenField hdfSemester = itm.FindControl("hdfSemester") as HiddenField;

                RadioButton rdby = itm.FindControl("rdoYes") as RadioButton;
                RadioButton rdbn = itm.FindControl("rdoNo") as RadioButton;

                objS.SemesterNo = Convert.ToInt32( hdfSemester.Value);
                objS.IdNo = Convert.ToInt32(hdnf.Value);
                id = objS.IdNo;
               // objS.NoDueStatus = Convert.ToInt32(ddlDuesType.SelectedValue);
                //int idno = Convert.ToInt32(hdfn.Value);
                string exist1 = objCommon.LookUp("ACD_NODUES_STATUS WITH (NOLOCK)", "COUNT(1)", "IDNO='" + id + "'");
                //if (Convert.ToInt32(exist1) > 0)
                //{
                //    if (rdbn.Checked == true)
                //    {
                //        objS.NoDueStatus = 0;
                if (chk.Checked)
                {
                    objS.NoDueStatus = 1;
                    flag = true;
                    output = studCont.AddNoDuesStatusForAdmitCard(objS);
                }

                else
                {
                    objS.NoDueStatus = 0;
                    flag = true;
                    output = studCont.AddNoDuesStatusForAdmitCard(objS);
                }
                //    }
                //    else
                //        if (rdby.Checked == true)
                //        {
                //            objS.NoDueStatus = 1;
                //            flag = true;
                //            output = studCont.AddNoDuesStatusForAdmitCard(objS);
                //        }
                //        else
                //        {
                //            BindListView();
                //        }
                //}
                //else
                //    if (rdbn.Checked == true)
                //    {
                //        objS.NoDueStatus = 0;
                //        flag = true;
                //        output = studCont.AddNoDuesStatusForAdmitCard(objS);
 
                //    }

                //if (chk.Checked == true && chk.Enabled == true)
                //{
                //    if (rdby.Checked == true && rdby.Enabled == true)
                //    {
                //        objS.NoDueStatus = 1;
                //        flag = true;
                //    }
                //    else if (rdbn.Checked == true && rdby.Enabled == true)
                //    {
                //        objS.NoDueStatus = 0;
                //        flag = true;
                //    }
                //    else
                //    {
                //        BindListView();
                //        objCommon.DisplayMessage(this.updtime, "Please Select No Dues Status.", this.Page);                        
                //        flag = false;
                //    }
                //if (flag.Equals(true))
                //{
                    
                //}               
                //}
                //if (chk.Checked)
                //{
                //    count++;
                //}
            }

            //if (count == 0)
            //{
            //    objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            //}
            //else 
                if(flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student Dues Status Alloted Successfully", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lvStudentRecords_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;
           
                HiddenField hdfn = e.Item.FindControl("hidIdNo") as HiddenField;
                CheckBox chkdis = e.Item.FindControl("chkReport") as CheckBox;

                RadioButton rdby = e.Item.FindControl("rdoYes") as RadioButton;
                RadioButton rdbn = e.Item.FindControl("rdoNo") as RadioButton;
                Label lblStatus = e.Item.FindControl("lblStatus") as Label;
                int idno =Convert.ToInt32(hdfn.Value);
                //string sts = dr["NODUES_STATUS"].ToString();

                string exist1 = objCommon.LookUp("ACD_NODUES_STATUS WITH (NOLOCK)", "COUNT(1)", "IDNO='" + idno + "'");

                if (Convert.ToInt32(exist1) > 0)
                {
                    if (dr["NODUES_STATUS"].Equals(1))
                    {
                        //((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                        //chkdis.InputAttributes.Add("disabled", "true");
                        //((RadioButton)e.Item.FindControl("rdoYes")).Enabled = true;
                        //((RadioButton)e.Item.FindControl("rdoYes")).Checked = true;
                        //((RadioButton)e.Item.FindControl("rdoNo")).Enabled = true;
                        //((Label)e.Item.FindControl("lblStatus")).Text = "Dues Clear";
                        //((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                        //((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                    }
                    else if (dr["NODUES_STATUS"].Equals(2))
                    {
                        //((CheckBox)e.Item.FindControl("chkReport")).Checked = true;
                        //chkdis.InputAttributes.Add("disabled", "true");
                        //((RadioButton)e.Item.FindControl("rdoYes")).Enabled = true;
                        //((RadioButton)e.Item.FindControl("rdoNo")).Enabled = true;
                        //((RadioButton)e.Item.FindControl("rdoNo")).Checked = true;
                        //((Label)e.Item.FindControl("lblStatus")).Text = "Dues Pending";
                        //((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                        //((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                    }
                }
                else
                {
                    //((CheckBox)e.Item.FindControl("chkReport")).Checked = false;
                    //((RadioButton)e.Item.FindControl("rdoYes")).Enabled = false;
                    //((RadioButton)e.Item.FindControl("rdoNo")).Enabled = false;
                    //((Label)e.Item.FindControl("lblStatus")).Text = "Dues not Alloted";
                    //((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                }               
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void Clear()
    {
        ddlSession.SelectedIndex=0;
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void rbRegEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void rdoSingleStudent_CheckedChanged(object sender, EventArgs e)
    {
        pnlSingleStud.Visible = true;
        Panel1.Visible = false;
        pnlBulkStud.Visible = false;       
        ClearForBulkStudent();

    }
    protected void rdoBulkStudent_CheckedChanged(object sender, EventArgs e)
    {
        pnlSingleStud.Visible = false;
        pnlBulkStud.Visible = true;
        Panel1.Visible = true;        
        ClearForSingleStudent();
       
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        divCourses.Visible = true;
        //btnShow.Visible = true;
        
        ShowDetails();
    }

    private void ShowDetails()
    {             
        int idno = 0;
         int No_Dues_Status = 0;
       
        StudentController objSC = new StudentController();
        try
        {
            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            int sessionno = Convert.ToInt32(ddlSessionSingleStud.SelectedValue);
            string Enrollno = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "ENROLLNO", "IDNO=" + Convert.ToInt16(idno) + "");

            if (Enrollno.Equals(txtEnrollno.Text))
            {
                Panel1.Visible = true;
                if (idno > 0)
                {
                    DataSet dsStudent = objSC.GetStudentDetails_No_Dues(idno, sessionno);

                    if (dsStudent != null && dsStudent.Tables.Count > 0)
                    {
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                            lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                            lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                            lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                            lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                            lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            lblDegrreno.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                            lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                            lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                            lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            lblSingCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                            lblSingCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            lblSingCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            No_Dues_Status = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["NODUES_STATUS"].ToString());

                            if (No_Dues_Status.Equals(1))
                            {
                                //rdoYesSingle.Checked = true;
                                //rdoNoSingle.Checked = false;
                                rdoYesSingle.Checked = false;
                                rdoNoSingle.Checked = true;
                            }
                            //else if (No_Dues_Status.Equals(0))
                            //{
                            //    rdoNoSingle.Checked = true;
                            //}
                            else
                            {
                                rdoYesSingle.Checked = true;
                            }

                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                          
                        }
                        else
                        {
                            divCourses.Visible = false;
                            objCommon.DisplayMessage(updtime, "Registration Details not found for this session!", this.Page);
                        }
                    }
                    else
                    {
                        divCourses.Visible = false;
                        objCommon.DisplayMessage(updtime, "Registration Details not found for this session!", this.Page);
                    }
                }
            }
            else
            {
                divCourses.Visible = false;
                objCommon.DisplayMessage(updtime, "No Record Found!!!", this.Page);
               // Panel1.Visible = false;
            }          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmitForSingleStu_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        string output = "";
        bool flag = false;
        try
        {
            objS.College_ID = Convert.ToInt32(lblSingCollege.ToolTip);
            objS.DegreeNo = Convert.ToInt32(lblDegrreno.ToolTip);
            objS.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            objS.SemesterNo = Convert.ToInt32(lblSemester.ToolTip);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();
            objS.SessionNo = Convert.ToInt32(Session["currentsession"]);
            objS.StudType = 0;
            objS.IdNo = Convert.ToInt32(lblName.ToolTip);
            id = objS.IdNo;

            if (rdoYesSingle.Checked)
            {
                objS.NoDueStatus = 0;
                flag = true;
            }
            else if (rdoNoSingle.Checked)
            {
                objS.NoDueStatus =1;
                flag = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updtime, "Please Select No Dues Status.", this.Page);
                flag = false;
            }
            if (flag.Equals(true))
            {
                output = studCont.AddNoDuesStatusForAdmitCard(objS);
                count++;
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(updtime, "Please select at least one student", this.Page);
            }
            else if (flag.Equals(true))
            {
                objCommon.DisplayMessage(updtime, "Student No-Dues Status Alloted Successfully", this.Page);
                txtEnrollno.Text = "";
                ddlSessionSingleStud.SelectedIndex = 0;
                pnlSingleStud.Visible = true;
                pnlBulkStud.Visible = false;
                divCourses.Visible = false;
                //lvStudentRecords.DataSource = null;
                //lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void ClearForBulkStudent()
    {
        ddlSession.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        lvStudentRecords.Visible = false;
        Panel1.Visible = false;

    }

    public void ClearForSingleStudent()
    {
        ddlSessionSingleStud.SelectedIndex = 0;
        txtEnrollno.Text ="";
        divCourses.Visible = false;
        pnlSingleStud.Visible = false;
       // Panel1.Visible = false;

    }
    protected void btnDuesStatus_Click(object sender, EventArgs e)
    {
        int Sessionno=Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = studCont.GetDuesStatusAllotment(Sessionno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView gv = new GridView();
            gv.DataSource = ds;
            gv.DataBind();
            string attachment = "attachment;filename = DuesStatusAllotment.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updtime, "No Data Found", this.Page);
        }
    }
}