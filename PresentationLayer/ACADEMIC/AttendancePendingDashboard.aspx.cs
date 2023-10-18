using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;


using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using SendGrid;

using mastersofterp_MAKAUAT;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
public partial class ACADEMIC_AttendancePendingDashboard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Page Authorization
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();
            if (Session["userno"].ToString() == "1")
            {
                ViewState["deptno"] = 0;
            }
            else
            {

                ViewState["deptno"] = Session["userdeptno"];
            }
            PopulateDropDownList();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendancePendingDashboard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendancePendingDashboard.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            AcademinDashboardController objADEController = new AcademinDashboardController();
            DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedItem.Value = "0";

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt =(from r in ds.Tables[0].AsEnumerable() orderby Convert.ToInt32(r["SESSIONNO"]) descending select r).CopyToDataTable();
                ddlSession.DataSource = dt;
                ddlSession.DataValueField = dt.Columns[0].ToString();
                ddlSession.DataTextField = dt.Columns[4].ToString();
                ddlSession.DataBind();
                ddlSession.SelectedIndex = 0;
                ddlSessionBulk.DataSource = dt;
                ddlSessionBulk.DataValueField = dt.Columns[0].ToString();
                ddlSessionBulk.DataTextField = dt.Columns[4].ToString();
                ddlSessionBulk.DataBind();
                ddlSessionBulk.SelectedIndex = 0;
            }
            //  objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                DataSet ds = objAttC.GetPendingAttData(Convert.ToInt32(ddlSession.SelectedValue), ViewState["deptno"].ToString(), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvParent.DataSource = ds;
                    gvParent.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updMarksEntryDetailReport, "No Record Found", this.Page);
                    gvParent.DataSource = null;
                    gvParent.DataBind();
                }
            }
            else
            {
                gvParent.DataSource = null;
                gvParent.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChildAttDates");

                HiddenField hdfCourseno = e.Row.FindControl("hdfCourseno") as HiddenField;
                HiddenField hdfSecno = e.Row.FindControl("hdfSecno") as HiddenField;
                HiddenField hdfSemno = e.Row.FindControl("hdfSemno") as HiddenField;
                HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;
                HiddenField hdnUaNo = e.Row.FindControl("hdnUaNo") as HiddenField;

                DataSet ds = objAttC.GetPendingAttDates(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfCourseno.Value), Convert.ToInt32(hdfSecno.Value), Convert.ToInt32(hdfSemno.Value), Convert.ToInt32(hdfSchemeno.Value), Convert.ToInt32(hdnUaNo.Value), ViewState["deptno"].ToString());
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvChildGrid.DataSource = ds;
                        gvChildGrid.DataBind();
                    }
                    else
                    {
                        gvChildGrid.DataSource = null;
                        gvChildGrid.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame__gvParent_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvParent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild");

                HiddenField hdfTempExam = e.Row.FindControl("hdfTempExam") as HiddenField;
                //HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                HiddenField hdfUano = e.Row.FindControl("hdfUano") as HiddenField;
                DataSet ds = objAttC.GetPendingAttDataCourseWise(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfTempExam.Value), Convert.ToInt32(hdfUano.Value), ViewState["deptno"].ToString());

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvChildGrid.DataSource = ds;
                        gvChildGrid.DataBind();
                    }
                    else
                    {
                        gvChildGrid.DataSource = null;
                        gvChildGrid.DataBind();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame__gvParent_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }

    protected void btnSent_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsconfig = null;
            string COMPANY_EMAILSVCID = string.Empty;
            string SENDGRID_APIKEY = string.Empty;
            string CollegeId = string.Empty;
            string SrNo = string.Empty;
            int SendingEmailStatus = 0;
            string EmailId = string.Empty;
            string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            Session["SENDGRID_APIKEY"] = string.Empty;
            if (dsconfig.Tables[0].Rows.Count > 0)
            {
                COMPANY_EMAILSVCID = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
                SENDGRID_APIKEY = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            }


            string[] mail = Convert.ToString(Session["ToEmail"]).Split(',');
            DataRow dr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("TOEMAILID", typeof(string));
            dt.Columns.Add("FROMEMAILID", typeof(string));
            dt.Columns.Add("EMAIL_STATUS", typeof(string));
            dt.Columns.Add("EMAIL_TEXTMATTER", typeof(string));
            dt.Columns.Add("EMAIL_SUBJECT", typeof(string));
            dt.Columns.Add("EMAILFROM_UA_NO", typeof(int));
            dt.Columns.Add("IPADDRESS", typeof(string));
            dt.Columns.Add("CC_EMAIL", typeof(string));
            int status = 0;

            string email_type = string.Empty;
            DataSet ds = getModuleConfig();
            string collegeCode = objCommon.LookUp("REFF", "CODE_STANDARD", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            }

            if (email_type == "2" && email_type != "")
            {
                // Gridview Mail Sending Process.
                Task<int> task = Execute(txtBody.Text, txt_emailid.Text, txtSubject.Text, COMPANY_EMAILSVCID, SENDGRID_APIKEY, txtCc.Text, collegeCode);
                status = task.Result;
            }
            if (email_type == "3" && email_type != "")
            {
                status = OutLook_Email(txtBody.Text, txt_emailid.Text, txtSubject.Text, txtCc.Text, collegeCode);
            }

            dr = dt.NewRow();
            dr["TOEMAILID"] = txt_emailid.Text;
            dr["FROMEMAILID"] = COMPANY_EMAILSVCID;
            dr["EMAIL_STATUS"] = status == 1 ? "Delivered" : "Not Delivered";
            dr["EMAIL_TEXTMATTER"] = txtBody.Text;
            dr["EMAIL_SUBJECT"] = txtSubject.Text;
            dr["EMAILFROM_UA_NO"] = Convert.ToInt32(Session["userno"]);
            dr["IPADDRESS"] = ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            dr["CC_EMAIL"] = txtCc.Text == string.Empty ? "-" : txtCc.Text;
            dt.Rows.Add(dr);

            // objMEDWFController.Insert_MarksEntry_Dashboard_Wire_Frame_Email_Sending_Log(dt);

            if (status == 1)
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Sent Successfully", this.Page);
                txt_emailid.Text = string.Empty;
                txtBody.Text = string.Empty;
                txtSubject.Text = string.Empty;


                string divname = Convert.ToString(Session["divname"]) != string.Empty ? Convert.ToString(Session["divname"]) : string.Empty;
                if (divname != string.Empty)
                {
                    string[] div = divname.Split('-');
                    string divSrNo = div[0];
                    string divCollege = div[1];
                    string divExamName = div[2];
                    string divDept = div[3];
                    string Collegewise = divSrNo + divExamName + divCollege;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divBackexpandcollapse('div4" + divSrNo + "','div5" + Collegewise + "','div6" + divname + "');", true);
                }
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Not Send Some Faculty like " + EmailId + ", Please Try Again !!!.", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + txt_emailid.Text + "','" + txtCc.Text + "');", true);

                string divname = Convert.ToString(Session["divname"]) != string.Empty ? Convert.ToString(Session["divname"]) : string.Empty;
                if (divname != string.Empty)
                {
                    string[] div = divname.Split('-');
                    string divSrNo = div[0];
                    string divCollege = div[1];
                    string divExamName = div[2];
                    string divDept = div[3];
                    string Collegewise = divSrNo + divExamName + divCollege;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divBackexpandcollapse('div4" + divSrNo + "','div5" + Collegewise + "','div6" + divname + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_btnSent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSendDeptMail_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string ToEmail = string.Empty;
        string CCMail = string.Empty;
        ToEmail = btn.ValidationGroup;

        if (ToEmail != string.Empty)
        {
            Session["ToEmail"] = ToEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "','" + CCMail + "');", true);
            //txtCc.Text = CCMail;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divBackexpandcollapse('div4" + divSrNo + "','div5" + Collegewise + "','div6" + divname + "');", true);
        }
    }

    private int OutLook_Email(string Message, string toEmailId, string sub, string Cc, string college_code)
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
            oMail.Cc = Cc;
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



    static async Task<int> Execute(string Message, string toEmailId, string sub, string COMPANY_EMAILSVCID, string SENDGRID_APIKEY, string Cc, string college_code)
    {
        int ret = 0;
        try
        {
            var fromAddress = new System.Net.Mail.MailAddress(COMPANY_EMAILSVCID, college_code);
            //var toAddress = new MailAddress(toEmailId, "");

            var apiKey = SENDGRID_APIKEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(COMPANY_EMAILSVCID, college_code);
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            //To Multiple Mail ID
            if (toEmailId != string.Empty)
            {
                string[] mail = toEmailId.Split(',');
                for (int i = 0; i < mail.Length; i++)
                {
                    if (mail[i] != string.Empty)
                    {
                        msg.AddTo(Convert.ToString(mail[i]));
                    }
                }
            }

            //Cc Mail Id
            if (Cc != string.Empty)
            {
                string[] mailCc = Cc.Split(',');
                for (int i = 0; i < mailCc.Length; i++)
                {
                    if (mailCc[i] != string.Empty)
                    {
                        msg.AddCc(Convert.ToString(mailCc[i]));
                    }
                }
            }


            //var response = await client.SendEmailAsync(msg);
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
    protected void gvBulkEmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void rdblBulkEmail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblBulkEmail.SelectedValue == "1")
        {
            divBulk.Visible = false;
            divSingle.Visible = true;
        }
        else if (rdblBulkEmail.SelectedValue == "2")
        {
            divSingle.Visible = false;
            divBulk.Visible = true;
            btnSendEmail.Visible = false;
        }
    }
    protected void btnbulkShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSessionBulk.SelectedIndex > 0)
            {
                DataSet ds = objAttC.GetPendingAttData(Convert.ToInt32(ddlSessionBulk.SelectedValue), ViewState["deptno"].ToString(), Convert.ToDateTime(txtbulkFDate.Text), Convert.ToDateTime(txtbulkTDate.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBulkEmail.DataSource = ds;
                    gvBulkEmail.DataBind();
                    btnSendEmail.Enabled = true;
                }
                else
                {
                    objCommon.DisplayMessage(updMarksEntryDetailReport, "No Record Found", this.Page);
                    gvBulkEmail.DataSource = null;
                    gvBulkEmail.DataBind();
                }
            }
            else
            {
                gvBulkEmail.DataSource = null;
                gvBulkEmail.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    //Send Bulk Mail
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsconfig = null;
            string COMPANY_EMAILSVCID = string.Empty;
            string SENDGRID_APIKEY = string.Empty;
            string CollegeId = string.Empty;
            string SrNo = string.Empty;
            int SendingEmailStatus = 0;
            string EmailId = string.Empty;
            string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            Session["SENDGRID_APIKEY"] = string.Empty;
            if (dsconfig.Tables[0].Rows.Count > 0)
            {
                COMPANY_EMAILSVCID = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
                SENDGRID_APIKEY = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            }
            string[] mail = Convert.ToString(Session["ToEmail"]).Split(',');
            DataRow dr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("TOEMAILID", typeof(string));
            dt.Columns.Add("FROMEMAILID", typeof(string));
            dt.Columns.Add("EMAIL_STATUS", typeof(string));
            dt.Columns.Add("EMAIL_TEXTMATTER", typeof(string));
            dt.Columns.Add("EMAIL_SUBJECT", typeof(string));
            dt.Columns.Add("EMAILFROM_UA_NO", typeof(int));
            dt.Columns.Add("IPADDRESS", typeof(string));
            dt.Columns.Add("CC_EMAIL", typeof(string));
            int status = 0;

            string email_type = string.Empty;
            DataSet ds = getModuleConfig();
            string collegeCode = objCommon.LookUp("REFF", "CODE_STANDARD", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            }
            foreach (GridViewRow row in gvBulkEmail.Rows)
            {
                //TextBox txtEmail = row.FindControl("txtEmailMsg") as TextBox;
                //TextBox txtEmailSubject = row.FindControl("txtSubjectBulk") as TextBox;
                CheckBox chkBox = row.FindControl("chkSelect") as CheckBox;
                HiddenField hdnEmail = row.FindControl("hdnEmail") as HiddenField;
                HiddenField hdfTempExam = row.FindControl("hdfTempExam") as HiddenField;
                HiddenField hdfUano = row.FindControl("hdfUano") as HiddenField;
                HiddenField UA_FULLNAME = row.FindControl("hdnUaname") as HiddenField;
                Label CCODE = row.FindControl("lbl") as Label;
                string subject = string.Empty;
                subject = "Incomplete Attendance Mail";

                DataSet dsAtt = objAttC.GetPendingAttDataTimeSlot(Convert.ToInt32(ddlSessionBulk.SelectedValue), ViewState["deptno"].ToString(), Convert.ToDateTime(txtbulkFDate.Text), Convert.ToDateTime(txtbulkTDate.Text), Convert.ToInt32(hdfTempExam.Value), Convert.ToInt32(hdfUano.Value));
                string date = string.Empty;
                string message = string.Empty;
                //message = "Dear " + UA_FULLNAME.Value + ",\n Kindly mark the incomplete attendance entry. \n It is mandatory to complete the attendance within (Attendance lock by days).\n";
                //message = message + "Subject - " + CCODE.Text;
                string semestername = string.Empty;
                string sectionname = string.Empty;
                string lockdays = string.Empty;
                if (dsAtt != null && dsAtt.Tables.Count > 0 && dsAtt.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsAtt.Tables[0].Rows.Count; i++)
                    {
                        date = "<p>" + date + dsAtt.Tables[0].Rows[i]["DateString1"].ToString() + " - " + dsAtt.Tables[0].Rows[i]["SLOT"].ToString() + " - Section " + dsAtt.Tables[0].Rows[i]["SECTIONNAME"].ToString() + "</b></p>";
                    }
                    semestername = dsAtt.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    sectionname = dsAtt.Tables[0].Rows[0]["SECTIONNAME"].ToString();
                    lockdays = dsAtt.Tables[0].Rows[0]["LOCK_ATT_DAYS"].ToString();
                }
                message = MailBody(UA_FULLNAME.Value, CCODE.Text, semestername, sectionname, date, lockdays);

                if (chkBox.Checked)
                {
                    if (email_type == "2" && email_type != "")
                    {
                        // Gridview Mail Sending Process.
                        Task<int> task = Execute(message, hdnEmail.Value, subject, COMPANY_EMAILSVCID, SENDGRID_APIKEY, txtCc.Text, collegeCode);
                        status = task.Result;
                    }
                    if (email_type == "3" && email_type != "")
                    {
                        status = OutLook_Email(message, hdnEmail.Value, subject, txtCc.Text, collegeCode);
                    }
                }
            }
            dr = dt.NewRow();
            dr["TOEMAILID"] = txt_emailid.Text;
            dr["FROMEMAILID"] = COMPANY_EMAILSVCID;
            dr["EMAIL_STATUS"] = status == 1 ? "Delivered" : "Not Delivered";
            dr["EMAIL_TEXTMATTER"] = txtBody.Text;
            dr["EMAIL_SUBJECT"] = txtSubject.Text;
            dr["EMAILFROM_UA_NO"] = Convert.ToInt32(Session["userno"]);
            dr["IPADDRESS"] = ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            dr["CC_EMAIL"] = txtCc.Text == string.Empty ? "-" : txtCc.Text;
            dt.Rows.Add(dr);

            // objMEDWFController.Insert_MarksEntry_Dashboard_Wire_Frame_Email_Sending_Log(dt);

            if (status == 1)
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Sent Successfully", this.Page);
                txt_emailid.Text = string.Empty;
                txtBody.Text = string.Empty;
                txtSubject.Text = string.Empty;

                string divname = Convert.ToString(Session["divname"]) != string.Empty ? Convert.ToString(Session["divname"]) : string.Empty;
                if (divname != string.Empty)
                {
                    string[] div = divname.Split('-');
                    string divSrNo = div[0];
                    string divCollege = div[1];
                    string divExamName = div[2];
                    string divDept = div[3];
                    string Collegewise = divSrNo + divExamName + divCollege;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divBackexpandcollapse('div4" + divSrNo + "','div5" + Collegewise + "','div6" + divname + "');", true);
                }
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Not Send Some Faculty like " + EmailId + ", Please Try Again !!!.", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + txt_emailid.Text + "','" + txtCc.Text + "');", true);

                string divname = Convert.ToString(Session["divname"]) != string.Empty ? Convert.ToString(Session["divname"]) : string.Empty;
                if (divname != string.Empty)
                {
                    string[] div = divname.Split('-');
                    string divSrNo = div[0];
                    string divCollege = div[1];
                    string divExamName = div[2];
                    string divDept = div[3];
                    string Collegewise = divSrNo + divExamName + divCollege;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divBackexpandcollapse('div4" + divSrNo + "','div5" + Collegewise + "','div6" + divname + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_btnSent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string MailBody(string name,string ccode,string semester,string section,string slot,string lockdays)
    {
        string body = "";
        body += "<p>Dear " + "<b>" + name + "</b>" + ",</p></br>";
        body += "<p>Kindly mark the incomplete attendance entry. </p>" + "";
        body += "<p>It is mandatory to complete the attendance within " + lockdays + " days.</p></br>";
        body += "<p>Subject - " + ccode + "</p>";
        body += "<p>Semester - " + semester + "</p></br>";
        //body += "<p>Section - " + section + "</p></br>";
        body += "<p>" + slot + "</p></br>";
        body += "<p>Thanks</p>";
        body += "<p>Academic Team</p>";
        
        return body;
    }

    protected void btnClearSingle_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        gvParent.DataSource = null;
        gvParent.DataBind();
        divSingle_List.Visible = false;
    }
    protected void btnClearBulk_Click(object sender, EventArgs e)
    {
        ddlSessionBulk.SelectedIndex = 0;
        txtbulkFDate.Text = string.Empty;
        txtbulkTDate.Text = string.Empty;
        divBulk_List.Visible = false;
        btnSendEmail.Visible = false;
        gvBulkEmail.DataSource = null;
        gvBulkEmail.DataBind();
       
       
    }
}