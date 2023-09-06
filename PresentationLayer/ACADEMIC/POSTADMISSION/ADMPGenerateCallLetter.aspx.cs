using BusinessLogicLayer.BusinessEntities.Academic.PoastAdmission;
using BusinessLogicLayer.BusinessLogic.PostAdmission;
using EASendMail;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMPGenerateCallLetter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPGenerateCallLetter objGenCallLetter = new ADMPGenerateCallLetter();
    CallLetterGenration EntGenCll = new CallLetterGenration();


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
            }

            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

            this.FillDropdown();
        }
    }

    private void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH desc");

        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlAdmissionBatch.SelectedIndex = 0;

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstProgram.Items.Clear();
        txtLoginTime.Text = string.Empty;
        txtScheduleDate.Text = string.Empty;
        pnlGV1.Visible = false;
        lvSchedule.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();

        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objGenCallLetter.GetBranch(Degree);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.Items.Clear();
        lstProgram.Items.Clear();
        txtLoginTime.Text = string.Empty;
        txtScheduleDate.Text = string.Empty;
        pnlGV1.Visible = false;
        lvSchedule.Visible = false;
        lvSchedule.DataSource = null;

        lvSchedule.DataBind();
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0", "D.DEGREENO");
        ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
        ddlDegree.SelectedIndex = 0;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select Branch/Program.", this.Page);
                return;
            }
            EntGenCll.ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            EntGenCll.ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            EntGenCll.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            branchno = branchno.TrimEnd(',').Trim();
            EntGenCll.Branchno = branchno;

            DataSet ds = null;
            ds = objGenCallLetter.GetStudentsForCallLetter(EntGenCll);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlGV1.Visible = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                lvSchedule.DataBind();

            }
            else
            {

                objCommon.DisplayMessage(updGenerateCallLetter, "No Recored Found.", this.Page);
                pnlGV1.Visible = false;
                lvSchedule.Visible = false;
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select Branch/Program.", this.Page);
                return;
            }
            else if (txtScheduleDate.Text == string.Empty || txtScheduleDate.Text == "")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Insert Schedule Date.", this.Page);
                return;
            }
            else if (txtLoginTime.Text==string.Empty || txtLoginTime.Text=="")
            {
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Insert Schedule Time.", this.Page);
                return;
            }

            string ipaddress = string.Empty;

            string rollno = string.Empty;
            int chkCount = 0;
            int updCount = 0;
            string time = "";
            ipaddress = Request.ServerVariables["REMOTE_HOST"];

            //int ua_no = Convert.ToInt32(Session["userno"].ToString());
            //int userNo = 0;
            //int ACNO = 0;
            //string rollNo = string.Empty;
            //bool IsAttend;
            //int AttendanceNo = 0; ;
            foreach (ListViewDataItem lvItem in lvSchedule.Items)
            {

                // CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField HdnBatchNo = lvItem.FindControl("hdnAdmBatch") as HiddenField;
                HiddenField HdnDegreeNo = lvItem.FindControl("hdnDegreeNo") as HiddenField;
                HiddenField HdnBranchNo = lvItem.FindControl("hdnBranchNo") as HiddenField;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
                CheckBox ChkCall = lvItem.FindControl("chkCallLrt") as CheckBox;

                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    EntGenCll.USerNo = Convert.ToInt32(hdnUserNo.Value);
                }

                if (ChkCall.Checked == true)
                {
                    time = txtLoginTime.Text + chkTime.Text.ToString().Trim();
                    EntGenCll.ADMBATCH = Convert.ToInt32(HdnBatchNo.Value);
                    EntGenCll.DegreeNo = Convert.ToInt32(HdnDegreeNo.Value);
                    EntGenCll.Branchno = HdnBranchNo.Value.ToString();
                    EntGenCll.USerNo = Convert.ToInt32(hdnUserNo.Value);

                    EntGenCll.CallDate = Convert.ToDateTime(txtScheduleDate.Text);
                    EntGenCll.Calltime = time;

                    int CreatedBy = Convert.ToInt32(Session["UserNo"]);

                    chkCount++;
                    CustomStatus cs = (CustomStatus)objGenCallLetter.INSERT_UPDATE_CALLLETTER(EntGenCll, ipaddress, CreatedBy);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        updCount++;
                    }
                }
            }
            if (chkCount == 0)
            {
                //objCommon.DisplayMessage(this.Page, "Please Select At Least One Student.", this.Page);
                objCommon.DisplayMessage(updGenerateCallLetter, "Please Select At Least One Student.", this.Page);
                return;
            }
            if (chkCount > 0 && chkCount == updCount)
            {

                objCommon.DisplayMessage(updGenerateCallLetter, "Call Letter Generated Successfully.", this.Page);
                btnShow_Click(sender, e);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void chkTime_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTime.Checked)
            {
                chkTime.Text = "AM";
            }
            else
            {
                chkTime.Text = "PM";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        Task<int> task = Execute("Msg", "abhijit.naik@mastersofterp.com", "Call Letter");

        //DocumentControllerAcad objdocContr = new DocumentControllerAcad();
        //CustomStatus cs = CustomStatus.Others;
        //string SP_Name1 = "PKG_ACD_UPDATE_PHD_STUDENT_FINAL_STATUS";
        //string SP_Parameters1 = "@P_USERNO,@P_CAMPUSNO,@P_WEEKDAYNO,@P_UA_NO,@P_OUTPUT";
        //string Call_Values1 = "" + Convert.ToInt32(ViewState["USERNO"]) + "," + Convert.ToInt32(ddlCampusName.SelectedValue) + "," + Convert.ToInt32(ddlWeekDay.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "," + 0 + "";

        //string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true, 1);//grade allotment
        //if (que_out1 == "1")
        //{
        //    objCommon.DisplayMessage(this.Page, "Record Saved Successfully !!!", this.Page);

        //    // DynamicPdfViewer();
        //    //ShowConfirmationReport("Final_Confirmation_Report", "AdmisionConfirmSlip.rpt", Convert.ToInt32(ViewState["ID_NO"]));
        //    string FinalStatus = "";
        //    FinalStatus = objCommon.LookUp("ACD_STUDENT", "ISNULL(ENROLLNO,0)", "ISNULL(CAN,0) = 0 AND ISNULL(ADMCAN,0) = 0 AND IDNO=" + Convert.ToInt32(ViewState["IDNO"]));
        //    if (FinalStatus.ToString() == "0" || FinalStatus.ToString() == string.Empty)
        //    {
        //        lnkSendEmail.Enabled = true;
        //        lnkPrintReport.Visible = false;
        //        btnFrontBackReport.Visible = false;
        //    }
        //    else
        //    {
        //        lnkSendEmail.Enabled = true;
        //        lnkPrintReport.Visible = true;
        //        btnFrontBackReport.Visible = true;
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Error !!!", this.Page);
        //    return;
        //}
        //DataSet dsUserContact = null;
        //UserController objUC = new UserController();
        //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", ViewState["USERNO"].ToString(), Convert.ToInt32(2879));
        //string STUDNAME = lblFirstNameP.Text + ' ' + lblLastNameP;
        //if (dsUserContact.Tables[1] != null && dsUserContact.Tables[1].Rows.Count > 0)
        //{
        //    if (dsUserContact.Tables[3] != null && dsUserContact.Tables[3].Rows.Count > 0)
        //    {
                //string Subject = "Subject";
                //string message = "";
                //message ="This Is For Test";

                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //MailMessage msg = new MailMessage();
                //msg.To.Add(new System.Net.Mail.MailAddress("sangamnaik1604@gmail.com"));
                //msg.From = new System.Net.Mail.MailAddress("no-reply@crescent.education");
                //msg.Subject = Subject;
                //StringBuilder sb = new StringBuilder();
                //msg.Body = message;
                ////msg.Body = msg.Body.Replace("[USERFIRSTNAME]", dsUserContact.Tables[3].Rows[0]["STUDNAME"].ToString());
                ////msg.Body = msg.Body.Replace("[PROGRAM]", dsUserContact.Tables[3].Rows[0]["DEGREENAME"].ToString());
                ////msg.Body = msg.Body.Replace("[CAMPUS]", dsUserContact.Tables[3].Rows[0]["CAMPUSNAME"].ToString());
                ////msg.Body = msg.Body.Replace("[WEEKDAY]", dsUserContact.Tables[3].Rows[0]["WEEKDAYSNAME"].ToString());
                ////msg.Body = msg.Body.Replace("[Date]", dsUserContact.Tables[3].Rows[0]["COMMENCE_DATE"].ToString());

                ////   MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,OfferLetterBulk.rpt", "@P_COLLEGE_CODE=" + collegecode + ",@P_USERNO=" + userno + ",@P_ADMBATCH=" + Convert.ToInt32(ddlIntakeuser) + ",@P_DEGREENO=" + Convert.ToInt32(ddlUserDegree) + ",@P_ENTERANCE=" + Convert.ToInt32(0));
                ////var bytesRpt = oAttachment1.ToArray();
                ////var fileRpt = Convert.ToBase64String(bytesRpt);
                ////byte[] test = (byte[])bytesRpt;
                ////  msg.AddAttachment("Offerletter.pdf", test);     

                ////  System.Net.Mail.Attachment attachment;

                ////attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "Offerletter.pdf");
                ////msg.Attachments.Add(attachment);


                ////sb.AppendLine(message);
                //msg.BodyEncoding = Encoding.UTF8;

                ////msg.Body = Convert.ToString(sb);
                //msg.IsBodyHtml = true;

                //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                //client.UseDefaultCredentials = false;

                //client.Credentials = new System.Net.NetworkCredential("no-reply@crescent.education", "crescentmis");

                //client.Port = 587; // You can use Port 25 if 587 is blocked (mine is)
                //client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                ////client.TargetName = "STARTTLS/smtp.office365.com";
                //client.EnableSsl = true;
                //try
                //{
                //    client.Send(msg);
                //    //lblText.Text = "Message Sent Succesfully";
                //}
                //catch (Exception ex)
                //{
                //    //lblText.Text = ex.ToString();
                //}


        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(this.Page, "Unable to send email !!!", this.Page);
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Unable to send email !!!", this.Page);
        //}
        

    }


    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {


        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            SmtpMail oMail = new SmtpMail("TryIt");



            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();



            oMail.To = toEmailId;



            oMail.Subject = sub;



            oMail.HtmlBody = Message;




            // SmtpServer oServer = new SmtpServer("smtp.live.com");



            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022



            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();



            oServer.Port = 587;



            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            Console.WriteLine("start to send email over TLS...");



            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            oSmtp.SendMail(oServer, oMail);



            Console.WriteLine("email sent successfully!");
            ret = 1;
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");
            Console.WriteLine(ep.Message);
            ret = 0;
        }
        return ret;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlDegree.Items.Insert(0, new ListItem("Please Select", "0"));
        lstProgram.Items.Clear();
        txtScheduleDate.Text = string.Empty;
        txtLoginTime.Text = string.Empty;
        pnlGV1.Visible = false;
        lvSchedule.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
    }
    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtScheduleDate.Text = string.Empty;
        txtLoginTime.Text = string.Empty;
        pnlGV1.Visible = false;
        lvSchedule.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
    }
    protected void txtLoginTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string text = txtLoginTime.Text;

            int tm = Convert.ToInt32(text.Split(':').LastOrDefault());


            string[] separatingStrings = { 
                                             ":00", ":00", ":01", ":01", ":02", ":02", ":03", ":03", ":04", ":04", ":05", ":05",
                                           ":06", ":06", ":07", ":07", ":08", ":08", ":09", ":09", ":10", ":10", ":11", ":11",
                                           ":12", ":12", ":13", ":13", ":14", ":14", ":15", ":15", ":16", ":16", ":17", ":17",
                                            ":18", ":18", ":19", ":19", ":20", ":20", ":21", ":21", ":22", ":22", ":23", ":23",
                                           ":24", ":24", ":25", ":25", ":26", ":26", ":27", ":27", ":28", ":28", ":29", ":29",
                                          ":30", ":30", ":31", ":31", ":32", ":32", ":33", ":33", ":34", ":34", ":35", ":35",
                                          ":36", ":36", ":37", ":37", ":38", ":38", ":39", ":39", ":40", ":40", ":41", ":41",
                                          ":42", ":42", ":43", ":43", ":44", ":44", ":45", ":45", ":46", ":46", ":47", ":47",
                                           ":48", ":48", ":49", ":49", ":50", ":50", ":51", ":51", ":52", ":52", ":53", ":53",
                                            ":54", ":54", ":55", ":55", ":56", ":56", ":57", ":57", ":58", ":58", ":59", ":59",
                                             ":60", ":60", ":61", ":61", ":62", ":62", ":63", ":63", ":64", ":64", ":65", ":65",
                                             ":66", ":66", ":67", ":67", ":68", ":68", ":69", ":69", ":70", ":70", ":71", ":71",
                                             ":72", ":72", ":73", ":73", ":74", ":74", ":75", ":75", ":76", ":76", ":77", ":77",
                                             ":78", ":78", ":79", ":79", ":80", ":80", ":81", ":81", ":82", ":82", ":83", ":83",
                                             ":84", ":84", ":85", ":85", ":86", ":86", ":87", ":87", ":88", ":88", ":89", ":89",
                                             ":90", ":90", ":91", ":92", ":93", ":93", ":94", ":94", ":95", ":95", ":96", ":96",
                                             ":97", ":97", ":98", ":98", ":99", ":99"
                                         };



            //**********added on 01052022*******


            //string[] TempData = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //string[] data = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            //*******************END****************
            string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            //string[] word = text1.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);



            int FromTime = Convert.ToInt32(words[0]);
            //int ToTime = Convert.ToInt32(words[0]);

            if (FromTime > 12)
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtLoginTime.Text = string.Empty;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtLoginTime.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (tm >= 60)
                {
                    objCommon.DisplayMessage(this, "Please Enter Valid Time!", this);
                    txtLoginTime.Text = string.Empty;
                    return;
                }
            }
        }
        catch (Exception ex)
        { }
    }
}