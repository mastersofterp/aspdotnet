
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Org.BouncyCastle.X509;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EASendMail;
using System.Text.RegularExpressions;
using mastersofterp_MAKAUAT;
using System.Web.Services;
using System.Globalization;

public partial class ACADEMIC_PHD_PhDSheduleofTestInterview : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhd = new PhdController();

    #region Page Action
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    #endregion

    #region PageLoad
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
            objCommon.FillDropDownList(ddlAdmbatchS, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
            objCommon.FillListBox(lboSchool, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
          
        }
        catch (Exception ex)
        {
            throw;
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
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    Session["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //fill dropdown method
                    PopulateDropDown();
                    BindList1();

                    divvenue.Visible = false;

                }

            }
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhDSheduleofTestInterview.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page  
            Response.Redirect("~/notauthorized.aspx?page=PhDSheduleofTestInterview.aspx");
        }
    }
    #endregion

    #region AddSchedule
    protected void BindList1()
    {
        try
        {
            DataSet ds;
            //  int AdmBatch = 0;
            // AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            ds = objPhd.GetApprovedScheduleListForPhd();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAddSch.DataSource = ds.Tables[0];
                lvAddSch.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updSend, "No student data found", this);
                lvAddSch.DataSource = null;
                lvAddSch.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SCHNO = Convert.ToInt32(btnEdit.CommandArgument);
            Session["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(SCHNO);
            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PbiConfiguration.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDetail(int SCHNO)
    {
        DataSet ds = objPhd.EditScheduleListForPhd(SCHNO);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["VENUE"].ToString() != string.Empty)
            {
                divvenue.Visible = true;
                txtVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
            }
            else
            {
                divvenue.Visible = false;
                txtVenue.Text = string.Empty;
            }
            ddlAdmBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
            ddlPhDSch.SelectedValue = ds.Tables[0].Rows[0]["SCHEDULEFOR"].ToString();
            txtStartTime.Text = ds.Tables[0].Rows[0]["SCHEDULETIME"].ToString();
            txtStartDate.Text = ds.Tables[0].Rows[0]["SCHEDULEDATE"].ToString();
            lboSchool.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            if (ds.Tables[0].Rows[0]["EXAM_MODE"].ToString() != string.Empty)
            {
                ddlExammode.SelectedValue = ds.Tables[0].Rows[0]["EXAM_MODE"].ToString();
            }
            else
            {
                ddlExammode.SelectedValue = "0";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Select Admission Batch.`)", true);
                return;
            }
            if (lboSchool.SelectedIndex <= -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Select Atleast One School.`)", true);
                return;
            }
            if (ddlPhDSch.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Select PhD Schedule of.`)", true);
                return;
            }
            if (ddlExammode.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Select Exam Mode.`)", true);
                return;
            }
            if (txtStartDate.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Enter Attendance Start Date.`)", true);
                return;
            }
            if (txtStartTime.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Enter Start Time.`)", true);
                return;
            }
            if (divvenue.Visible == true)
            {
                if (txtVenue.Text == string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please Enter Venue.`)", true);
                    return;
                }
            }
            DateTime time = new DateTime();
            if (DateTime.TryParseExact(txtStartTime.Text, "hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out time))
            {
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Please enter a valid time.`)", true);
                return;
            }
            if (Convert.ToDateTime(txtStartDate.Text) < System.DateTime.Now)
            {
                //objCommon.DisplayMessage(this.updSChedul, "Date should be greater than today's Date ", this.Page);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Date should be greater than today's Date `)", true);
                return;
            }
            else
            {
                StudentRegist objSR = new StudentRegist();
                int AdmBatch = 0;
                int collegeid = 0;
                int PhDSch = 0;
                string STime = string.Empty;
                string Venue = string.Empty;
                AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                PhDSch = Convert.ToInt32(ddlPhDSch.SelectedValue);
                objSR.IPADDRESS = Session["ipAddress"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"]);

                int count = 0;
                string Collegeids = string.Empty;
                foreach (ListItem Item in lboSchool.Items)
                {
                    if (Item.Selected)
                    {
                        Collegeids += Item.Value + ",";
                        count++;
                    }
                }
                Collegeids = Collegeids.Substring(0, Collegeids.Length - 1);

                //Collegeids = Convert.ToInt32(lboSchool.SelectedValue);
                DateTime Config_SDate = Convert.ToDateTime(txtStartDate.Text);
                Venue = txtVenue.Text;
                STime = txtStartTime.Text;
                if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
                {
                    int SCHNO = Convert.ToInt32(Session["id"]);

                    CustomStatus cs = (CustomStatus)objPhd.UpdPhdSchedule(objSR,AdmBatch, PhDSch, Venue, Config_SDate, STime, SCHNO, Collegeids, Convert.ToInt32(ddlExammode.SelectedValue.ToString()));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindList1();
                        CLEAR();
                        objCommon.DisplayMessage(this, "Schedule Updated successfully", this.Page);
                        Session["action"] = null;
                    }
                    else
                    {
                        BindList1();
                        CLEAR();
                        objCommon.DisplayMessage(this, "Schedule Already Exists", this.Page);
                        Session["action"] = null;
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objPhd.InsPhdSchedule(objSR, AdmBatch, PhDSch, Venue, Config_SDate, STime, Collegeids, Convert.ToInt32(ddlExammode.SelectedValue.ToString()));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindList1();
                        CLEAR();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Schedule Submitted Successfully.`)", true);
                        return;
                    }
                    else
                    {
                        BindList1();
                        CLEAR();
                        objCommon.DisplayMessage(this, "Schedule Already Exists", this.Page);
                        return;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void CLEAR()
    {
        txtVenue.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlPhDSch.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        lboSchool.ClearSelection();
        ddlExammode.SelectedValue = "0";
        divvenue.Visible = false;
        //ddlPhDSch.Items.Clear();
        //ddlAdmBatch.Items.Clear();
        //lboSchool.Items.Clear();
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region SendEmail
    protected void ddlExammode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExammode.SelectedValue.ToString() == "2")
        {
            divvenue.Visible = true;
        }
        else
        {
            divvenue.Visible = false;
            txtVenue.Text = string.Empty;
        }
    }
    protected void BindList()
    {
        try
        {
            DataSet ds;
            int AdmBatch = 0;
            int Collegeid = 0;
            AdmBatch = Convert.ToInt32(ddlAdmbatchS.SelectedValue);
            Collegeid = Convert.ToInt32(ddlCollgeid.SelectedValue);
            ds = objPhd.GetApprovedStudentListForPhd(AdmBatch, Collegeid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPhdA.Visible = true;
                lvPhdA.DataSource = ds.Tables[0];
                lvPhdA.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updSend, "No student data found", this);
                lvPhdA.DataSource = null;
                lvPhdA.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmbatchS.SelectedIndex > 0)
            {
                if (ddlCollgeid.SelectedIndex > 0)
                {
                    BindList();
                }
                else
                {
                    objCommon.DisplayMessage(updSend, "Please select school applied for. ", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updSend, "Please Select Admission Batch ", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public int sendEmail(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = sub,
                Body = Message,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                IsBodyHtml = true
            })
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

                // ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(message);
                return ret = 1;
            }
        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }
    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])));
        return ds;
    }
    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }
    private int OutLook_Email(string Message, string toEmailId, string sub)
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
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (ListViewDataItem dataitem in lvPhdA.Items)
            {
                CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updSend, "Please Select atleast one Student For Send Email", this);
                return;
            }
            else
            {
                if (ddlAdmbatchS.SelectedIndex > 0)
                {
                    foreach (ListViewDataItem lv in lvPhdA.Items)
                    {
                        CheckBox cbRow = lv.FindControl("chkReport") as CheckBox;
                        if (cbRow.Checked == true)
                        {
                            Label lblEmail = lv.FindControl("lblEmail") as Label;
                            string useremail = lblEmail.Text.Trim().Replace("'", "");

                            if (lblEmail.Text.Trim() != "")
                            {
                                try
                                {
                                    int status = 0;
                                    Label lblName = lv.FindControl("lblName") as Label; ;
                                    Label lblUsername = lv.FindControl("lblUsername") as Label;
                                    HiddenField hdfb = lv.FindControl("hidBranch") as HiddenField;
                                    string EMAILID = string.Empty;
                                    string CollegeName = objCommon.LookUp("REFF", "CollegeName", " OrganizationId='" + Convert.ToInt32(Session["OrgId"]) + "'");
                                    string StudName = lblName.Text;
                                    string username = lblUsername.Text;
                                    string branch = hdfb.Value;
                                    string MSG = ddlSchedule.SelectedItem.Text;
                                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                                    {
                                        EMAILID = "dean.research@crescent.education , dy.dean.academicresearch@crescent.education";
                                    }
                                    else
                                    {
                                        EMAILID = objCommon.LookUp("REFF", "Email", " OrganizationId='" + Convert.ToInt32(Session["OrgId"]) + "'");
                                    }
                                    string[] repoarray;
                                    repoarray = MSG.Split('-');
                                    string Event = repoarray[0].ToString().TrimEnd();
                                    string time = string.Empty;
                                    string Exam = string.Empty;
                                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                                    {
                                        //if (Event == "Interview")
                                        if (Event.Equals("Interview"))
                                        {
                                            time = "as directed by the Head of the Department / Department Research Coordinator ";
                                            Exam = Event;
                                        }
                                        else
                                        {
                                            time = repoarray[2].ToString();
                                            Exam = "Entrance Exam";
                                        }
                                    }
                                    else
                                    {
                                        time = repoarray[2].ToString();
                                    }
                                    string date = repoarray[1].ToString();

                                    string exammode = repoarray[3].ToString();
                                    string venue = repoarray[4].ToString();
                                    string message = "<b>Dear " + StudName + "," + "</b><br />";
                                    message += "<br/>Greetings from " + CollegeName + "<br/>";
                                    message += "<br /><br /> With reference to your application " + username + ", we are pleased to inform you that your Online  " + Exam + " is scheduled as follows : <br />";
                                    message += "<br />Department of Registration: " + branch + "<br />";
                                    message += "<br />Date: " + date + "<br />";
                                    message += "<br />Venue: " + exammode + "<br />";
                                    message += "<br />Venue: " + venue + "<br />";
                                    message += "<br />Time Slot: " + time + " <br />";
                                    message += "<br />In case of any assistance, feel free to contact us at " + EMAILID + ".<br />";
                                    message += "<br />Regards<br />";
                                    message += "<br/>Dean (Research)<br />";
                                    message += "<br/>" + CollegeName + "<br />";
                                    if (lblEmail.Text != string.Empty)
                                    {
                                        try
                                        {
                                            string email_type = string.Empty;
                                            string Link = string.Empty;
                                            int sendmail = 0;
                                            DataSet ds = getModuleConfig();
                                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                            {
                                                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                                                Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                                            }

                                            if (email_type == "1" && email_type != "")
                                            {
                                                status = sendEmail(message, lblEmail.Text, "PHD Schedule Announcement For Test and Interview ");
                                            }
                                            else if (email_type == "2" && email_type != "")
                                            {
                                                Task<int> task = Execute(message, lblEmail.Text, "PHD Schedule Announcement For Test and Interview");
                                                status = task.Result;
                                            }
                                            if (email_type == "3" && email_type != "")
                                            {
                                                status = OutLook_Email(message, lblEmail.Text, "PHD Schedule Announcement For Test and Interview");
                                            }
                                            //if (status == 1)
                                            //{
                                            int count1 = 0;
                                            StudentRegist objSR = new StudentRegist();
                                            int AdmBatchS = Convert.ToInt32(ddlAdmbatchS.SelectedValue);
                                            objSR.IPADDRESS = Session["ipAddress"].ToString();
                                            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
                                            int Userno = Convert.ToInt32(cbRow.ToolTip);
                                            int COLLEGE_ID = Convert.ToInt32(ddlCollgeid.SelectedValue);
                                            int Ca = objPhd.InsPhdScheduleemailLogmodified(objSR, Userno, username, AdmBatchS, COLLEGE_ID, Convert.ToInt32(ddlSchedule.SelectedValue.ToString()));
                                            //objCommon.DisplayMessage(this.updSend, "Send email Successfully", this.Page);
                                            count1++;
                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                            throw;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }

                            }
                        }
                    }
                    if (count <= 0)
                    {
                        objCommon.DisplayMessage(this.updSend, "Failed To send email", this.Page);
                        BindList();
                        CLEAR1();
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updSend, "Send email Successfully", this.Page);
                        BindList();
                        CLEAR1();
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updSend, "Please Select Schedule", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlAdmbatchS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmbatchS.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCollgeid, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
            //objCommon.FillDropDownList(ddlSchedule, "ACD_PHD_SELECTION_SCHEDULE", "DISTINCT SCHEDULENO", "(CASE WHEN SCHEDULEFOR=1 THEN 'Entrance_Exam' WHEN SCHEDULEFOR=2 THEN 'Interview' END )+' - '+CONVERT(NVARCHAR,SCHEDULEDATE,103)+' - '+SCHEDULETIME+ ' - ' + VENUE AS SCHEDULE", "ADMBATCH = " + ddlAdmbatchS.SelectedValue, "SCHEDULENO");
            lvPhdA.DataSource = null;
            lvPhdA.DataBind();
            lvPhdA.Visible = false;
            ddlSchedule.SelectedIndex = 0;
            ddlSchedule.Items.Clear();
            ddlSchedule.Items.Add(new ListItem("Please Select"));
           
        }
        else
        {
            objCommon.DisplayMessage("Please Select Admission Batch", this.Page);
            ddlSchedule.SelectedIndex = 0;
            ddlSchedule.Items.Clear();
            ddlCollgeid.SelectedIndex = 0;
            ddlCollgeid.Items.Clear();
            ddlCollgeid.Items.Add(new ListItem("Please Select"));
            ddlSchedule.Items.Add(new ListItem("Please Select"));
            ddlAdmbatchS.Focus();
            lvPhdA.Visible = false;

        }
    }
    protected void CLEAR1()
    {
        ddlSchedule.SelectedIndex = 0;
        ddlCollgeid.SelectedIndex = 0;
        ddlAdmbatchS.SelectedIndex = 0;
        ddlCollgeid.Items.Clear();
        ddlSchedule.Items.Clear();
        ddlCollgeid.Items.Add(new ListItem("Please Select"));
        ddlSchedule.Items.Add(new ListItem("Please Select"));
        lvPhdA.Visible = true;
        lvPhdA.DataSource = null;
        lvPhdA.DataBind();
        foreach (ListViewDataItem dataitem in lvPhdA.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkReport") as CheckBox;
            cbRow.Checked = false;
        }
    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void chkFrom_CheckedChanged(object sender, EventArgs e)
    {
        // chkFrom.Text = chkFrom.Checked == true ? "AM" : "PM";
    }
    protected void ddlCollgeid_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollgeid.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSchedule, "ACD_PHD_SELECTION_SCHEDULE", "DISTINCT SCHEDULENO", "(CASE WHEN SCHEDULEFOR=1 THEN 'Entrance_Exam' WHEN SCHEDULEFOR=2 THEN 'Interview' END )+' - '+CONVERT(NVARCHAR,SCHEDULEDATE,103)+' - '+SCHEDULETIME+' - '+(CASE WHEN EXAM_MODE=1 THEN 'Online' WHEN ISNULL(EXAM_MODE,0)=0 THEN 'Offline' WHEN EXAM_MODE=2 THEN 'Offline' END )+ ' - ' + VENUE AS SCHEDULE", "ADMBATCH = " + ddlAdmbatchS.SelectedValue + " and COLLEGE_ID= " + ddlCollgeid.SelectedValue, "SCHEDULENO");
            lvPhdA.DataSource = null;
            lvPhdA.DataBind();
            lvPhdA.Visible = false;
           
        }
        else
        {
            lvPhdA.DataSource = null;
            lvPhdA.DataBind();
            lvPhdA.Visible = false;
            ddlSchedule.SelectedIndex = 0;
            ddlSchedule.Items.Clear();
            //ddlCollgeid.Items.Add(new ListItem("Please Select"));
            ddlSchedule.Items.Add(new ListItem("Please Select"));
        }
        //lvPhdA.DataSource = null;
        //lvPhdA.DataBind();
        //lvPhdA.Visible = false;
        //ddlSchedule.SelectedIndex = 0;
        //ddlSchedule.DataSource = null;
    }
    #endregion

    //protected void chkFrom_CheckedChanged(object sender, EventArgs e)
    //{
    //    //chkFrom.Text = chkFrom.Checked == true ? "AM" : "PM";
    //}


   
}