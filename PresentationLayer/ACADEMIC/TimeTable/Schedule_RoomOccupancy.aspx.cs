using BusinessLogicLayer.BusinessLogic;
using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_TimeTable_Schedule_RoomOccupancy : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    

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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
                    //divMsg.InnerHtml = string.Empty;
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    DataSet ds = objCommon.FillDropDown("ACD_ATTENDANCE_TRIGGER_MAIL_CONFIG", "STUD_CC_MAIL", "STUD_BCC_MAIL,DAILY_FAC_TO_MAIL,DAILY_FAC_CC_MAIL,DAILY_FAC_BCC_MAIL,ABSENT_STUD_TO_MAIL,ABSENT_STUD_CC_MAIL,ABSENT_STUD_BCC_MAIL", "SRNO > 0", "SRNO");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Session["STUD_CC_MAIL"] = ds.Tables[0].Rows[0]["STUD_CC_MAIL"].ToString();
                        Session["DAILY_FAC_TO_MAIL"] = ds.Tables[0].Rows[0]["DAILY_FAC_TO_MAIL"].ToString();
                        Session["DAILY_FAC_CC_MAIL"] = ds.Tables[0].Rows[0]["DAILY_FAC_CC_MAIL"].ToString();
                        Session["ABSENT_STUD_TO_MAIL"] = ds.Tables[0].Rows[0]["ABSENT_STUD_TO_MAIL"].ToString();
                        Session["ABSENT_STUD_CC_MAIL"] = ds.Tables[0].Rows[0]["ABSENT_STUD_CC_MAIL"].ToString();

                    }
                }

            }
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
                Response.Redirect("~/notauthorized.aspx?page=Schedule_RoomOccupancy.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Schedule_RoomOccupancy.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
            if (ddlAttendanceStatus.SelectedValue == "2")
            {
                btnSendEmail.Visible = true;
            }
            else
            {
                btnSendEmail.Visible = false;
            }
            this.BindListViewClassSchedule();
        }
        else if (ddlType.SelectedValue == "2")
        {
            this.BindListViewRoomOccupancy();
        }
        else if (ddlType.SelectedValue == "0")
        {
            lvClassSchedule.DataSource = null;
            lvClassSchedule.DataBind();
            lvRoomOccupancy.DataSource = null;
            lvRoomOccupancy.DataBind();
        }
    }

    protected void BindListViewClassSchedule()
    {
        try
        {

            DataSet ds;

            DateTime FromDate = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);

            ds = objAttC.GetDatewiseDataForClassScheduleModified(Convert.ToDateTime(FromDate), Convert.ToInt32(ddlAttendanceStatus.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvClassSchedule.DataSource = ds;
                lvClassSchedule.DataBind();
                lvRoomOccupancy.DataSource = null;
                lvRoomOccupancy.DataBind();

            }
            else
            {

                lvClassSchedule.DataSource = null;
                lvClassSchedule.DataBind();
                lvRoomOccupancy.DataSource = null;
                lvRoomOccupancy.DataBind();
                objCommon.DisplayMessage(this.updtime, "Record Not Found !", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BindListViewRoomOccupancy()
    {
        try
        {

            DataSet ds;

            DateTime Date = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);

            ds = objAttC.GetDatewiseDataForRoomOccupancy(Date);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {


                lvRoomOccupancy.DataSource = ds;
                lvRoomOccupancy.DataBind();

                foreach (ListViewDataItem dataitem in lvRoomOccupancy.Items)
                {
                    Label lblTag = dataitem.FindControl("lblTag") as Label;
                    string lblTagS = (lblTag.Text);
                    if (lblTagS == "UNDERUTILIZED")
                    {
                        lblTag.CssClass = "badge badge-pill badge-danger";
                    }
                    else if (lblTagS == "MODERATLY UTLIZED")
                    {
                        lblTag.CssClass = "badge badge-pill badge-warning";
                    }
                    else if (lblTagS == "FAIRLY UTLIZED")
                    {
                        lblTag.CssClass = "badge badge-pill badge-primary";
                    }
                    else if (lblTagS == "EXCELLENT")
                    {
                        lblTag.CssClass = "badge badge-pill badge-success";
                    }
                    else if (lblTagS == "REVISIT THE CONFIGURATIONS")
                    {
                        lblTag.CssClass = "badge badge-pill badge-secondary";
                    }
                    else if (lblTagS == "NOT ALLOTTED")
                    {
                        lblTag.CssClass = "badge badge-pill badge-dark";
                    }

                }

                lvClassSchedule.DataSource = null;
                lvClassSchedule.DataBind();

            }
            else
            {


                lvRoomOccupancy.DataSource = null;
                lvRoomOccupancy.DataBind();
                lvClassSchedule.DataSource = null;
                lvClassSchedule.DataBind();
                objCommon.DisplayMessage(this.updtime, "Record Not Found !", this.Page);
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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
            divAttendanceStatus.Visible = true;
        }
        else
        {
            divAttendanceStatus.Visible = false;
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        this.ExportinExcelforClassSchedule();
    }
    private void ExportinExcelforClassSchedule()
    {


        DateTime Date = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);
        int type = Convert.ToInt32(ddlType.SelectedValue);

        DataSet dsfeestud = objAttC.GetDataForExportinExcelforClassSchedule(Date, type);

        if (dsfeestud != null && dsfeestud.Tables.Count > 0)
        {
            if (type == 1)
            {
                dsfeestud.Tables[0].TableName = "ClassSchedule";
            }
            else
            {
                dsfeestud.Tables[0].TableName = "RoomOccupancy";
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                if (type == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename= ClassSchedule.xlsx");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename= RoomOccupancy.xlsx");

                }

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.Page, "No Record Found", this.Page);
            return;
        }

    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        DataSet dsconfig = null;
        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "CollegeName,SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP,CODE_STANDARD", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
       
        DateTime Date = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);
        int type = Convert.ToInt32(ddlType.SelectedValue);
        string Emailids = "";
        string FacutyName = "";
        string UA_No = "";
        string CCEmailids = "";
        try
        {
            bool flag = false; int status = 0;
            DateTime FromDate = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);
            DataSet ds = objAttC.GetDatewiseDataForClassScheduleSendMail(Convert.ToDateTime(FromDate), Convert.ToInt32(ddlAttendanceStatus.SelectedValue), 1);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Emailids = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    FacutyName = ds.Tables[0].Rows[i]["UA_FULLNAME"].ToString();
                    UA_No = ds.Tables[0].Rows[i]["UA_NO"].ToString();
                    DataSet dsDetails = objAttC.GetDatewiseDataForClassScheduleSendMail(Convert.ToDateTime(FromDate), Convert.ToInt32(ddlAttendanceStatus.SelectedValue), 2);
                    DataTable dt = (DataTable)dsDetails.Tables[0];
                    DataRow[] dr = dt.Select("UA_NO='" + UA_No + "'");

                    string MyHtmlString = string.Empty;
                    MyHtmlString = "Dear " + FacutyName + ", <br/><br/>";
                    MyHtmlString += "We have observed that attendance for your class has not been marked on ERP. Please ensure it is done end of the day as we need to share updated student record with students & parent.<br/><br/>";

                    MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid #190404'><thead style='background-color: #d3cccc !important;color: #0e0e0e !important;'><tr style='border: 1px solid #190404;'><th style='border: 1px solid #190404;' scope='col'>Attendance Date</th><th style='border: 1px solid #190404;' scope='col'>Slot</th>" +
                   "<th style='border: 1px solid #190404;' scope='col'>Section</th>" + "<th style='border: 1px solid #190404;' scope='col'>Course Name</th><th style='border: 1px solid #190404;' scope='col'>Degree</th><th style='border: 1px solid #190404;' scope='col'>Faculty Name</th></tr></thead><tbody>";

                    for (int j = 0; j < dr.Length; j++)
                    {
                        MyHtmlString += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404; text-align:center;'> " + dr[j][1].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + " - " + dr[j][4].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][14].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][10].ToString() + "-" + dr[j][11].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404; text-align:center;'> " + dr[j][15].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404; text-align:center;'> " + dr[j][12].ToString() + "</td></tr>";
                    }
                    MyHtmlString += "</tbody></table><br/><br/><br/> Regards,<br/>" + dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString();
                    string Subject = "Faculty Class Attendance Tracker - " + txtFromDate.Text + " (Daily)";
                    status = objSendEmail.SendEmail(Emailids, MyHtmlString, Subject, CCEmailids, "");
                    Emailids = "";
                    CCEmailids = "";
                    //byte[] test = null;
                    //Task<int> task = Execute(dr, 1, Emailids, FacutyName, txtFromDate.Text, CCEmailids, test, ds);
                    
                    //status = task.Result;
                }
                DataSet dsAllFaculty = objAttC.GetDatewiseDataForClassScheduleSendMail(Date, type, 2);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                
                    if (Convert.ToString(Session["DAILY_FAC_TO_MAIL"]) != "" || Convert.ToString(Session["DAILY_FAC_TO_MAIL"]) != string.Empty)
                    {
                        Emailids = Convert.ToString(Session["DAILY_FAC_TO_MAIL"]);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updtime, "Please define Daily Faculty Attendance To email id on Module Configuration", this.Page);
                    }
                    if (Convert.ToString(Session["DAILY_FAC_CC_MAIL"]) != "" || Convert.ToString(Session["DAILY_FAC_CC_MAIL"]) != string.Empty)
                    {
                        CCEmailids = Convert.ToString(Session["DAILY_FAC_CC_MAIL"]);
                    }
                    dsAllFaculty.Tables[0].TableName = "Faculty Class Attendance";
                    if (dsAllFaculty.Tables[0] != null && dsAllFaculty.Tables[0].Rows.Count <= 0)
                        dsAllFaculty.Tables[0].Rows.Add("No Record Found");

                    ViewState["DYNAMIC_DATASET"] = dsAllFaculty.Tables[0];
                    DataTable dt = (DataTable)dsAllFaculty.Tables[0];
                    DataRow[] dr = dt.Select();

                    byte[] test = null;

                
                    MemoryStream memStream = new MemoryStream();
                    BinaryFormatter brFormatter = new BinaryFormatter();



                    dsAllFaculty.RemotingFormat = SerializationFormat.Binary;
                    brFormatter.Serialize(memStream, dsAllFaculty);
                    test = memStream.ToArray();

                    memStream.Close();
                    memStream.Dispose();

                    string MyHtmlString = string.Empty;
                    MyHtmlString = "Dear Team, <br/><br/>";
                    MyHtmlString += "Please Find Attached the detailed Faculty Class Attendance Tracker(Attendance not marked data) for all lectures taught during the day.<br/><br/><br/>";
                    MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid #190404'><thead style='background-color: #d3cccc !important;color: #0e0e0e !important;'><tr style='border: 1px solid #190404;'><th style='border: 1px solid #190404;' scope='col'>Attendance Date</th><th style='border: 1px solid #190404;' scope='col'>Slot</th>" +
                  "<th style='border: 1px solid #190404;' scope='col'>Section</th>" + "<th style='border: 1px solid #190404;' scope='col'>Course Name</th><th style='border: 1px solid #190404;' scope='col'>Degree</th><th style='border: 1px solid #190404;' scope='col'>Faculty Name</th></tr></thead><tbody>";

                    for (int i = 0; i < dr.Length; i++)
                    {
                        MyHtmlString += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404; text-align:center;'> " + dr[i][1].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][3].ToString() + " - " + dr[i][4].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][14].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][10].ToString() + "-" + dr[i][11].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404; text-align:center;'> " + dr[i][15].ToString() + "</td>";
                        MyHtmlString += "<td style='border: 1px solid #190404; text-align:center;'> " + dr[i][12].ToString() + "</td></tr>";
                    }
                    MyHtmlString += "</tbody></table><br/><br/>";
                    MyHtmlString += "This is a system generated email. Please do not reply to this email.<br/><br/><br/>";
                    MyHtmlString += "Warm Regards,<br/>Team ERP";

                    string Subject = "Faculty Class Attendance Tracker - " + txtFromDate.Text + " (Daily)";
                    status = objSendEmail.SendEmail(Emailids, MyHtmlString, Subject, CCEmailids, "");

                    //Task<int> task = Execute(dr, 2, Emailids, FacutyName, txtFromDate.Text, CCEmailids, test, dsAllFaculty);
                    Emailids = "";
                    CCEmailids = "";

                }

            }

            if (status == 1)
            {
                objCommon.DisplayMessage(updtime, "Email Send Successfully !!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updtime, "Unable To Send Email !!!" + status, this.Page);
            }
        }
        catch (Exception ex)
        {
            Emailids = "";
        }
    }

    static async Task<int> Execute(DataRow[] dr, int type, string Emailid, string FacutyName, string fromdate, string CCEmailids, byte[] bytefile, DataSet dsfinal)
    {
        int ret = 0;
        try
        {

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string MyHtmlString = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            //var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ATLAS");
            //var subject = "Attendance Statistics – From " + fromdate + " - " + " to " + todate + " of " + dr[0][1].ToString();

            if (type == 1)
            {
                //var plainTextContent = "";
                //var to = new EmailAddress(Emailids, ""); 

                var emails = Emailid.Split(',');
                var ccemails = CCEmailids.Split(',');

                var to = new List<EmailAddress>();
                foreach (var i in emails)
                {
                    to.Add(new EmailAddress(i));
                }


                MyHtmlString = "Dear " + FacutyName + ", <br/><br/>";
                MyHtmlString += "We have observed that attendance for your class has not been marked on ERP. Please ensure it is done end of the day as we need to share updated student record with students & parent.<br/><br/>";

                MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid #190404'><thead style='background-color: #d3cccc !important;color: #0e0e0e !important;'><tr style='border: 1px solid #190404;'><th style='border: 1px solid #190404;' scope='col'>Attendance Date</th><th style='border: 1px solid #190404;' scope='col'>Slot</th>" +
               "<th style='border: 1px solid #190404;' scope='col'>Section</th>" + "<th style='border: 1px solid #190404;' scope='col'>Course Name</th><th style='border: 1px solid #190404;' scope='col'>Faculty Name</th></tr></thead><tbody>";

                for (int i = 0; i < dr.Length; i++)
                {
                    MyHtmlString += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404; text-align:center;'> " + dr[i][1].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][3].ToString() + " - " + dr[i][4].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][14].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][10].ToString() + "-" + dr[i][11].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404; text-align:center;'> " + dr[i][12].ToString() + "</td></tr>";
                }
                MyHtmlString += "</tbody></table><br/><br/><br/> Regards,<br/>ATLAS SKILLTECH UNIVERSITY";
                var msg = new SendGrid.Helpers.Mail.SendGridMessage()
                {
                    From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ATLAS"),
                    Subject = "Faculty Class Attendance Tracker - " + fromdate + " (Daily)",
                    HtmlContent = MyHtmlString
                };
                //msg.AddCcs(cc);
                var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, to, msg.Subject, "", msg.HtmlContent);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var response = await client.SendEmailAsync(msg1);
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
            if (type == 2)
            {
                DataSet ds = null;
                var emails = Emailid.Split(',');
                var ccemails = CCEmailids.Split(',');

                var to = new List<EmailAddress>();
                foreach (var i in emails)
                {
                    to.Add(new EmailAddress(i));
                }
                var cc = new List<EmailAddress>();
                foreach (var i in ccemails)
                {
                    cc.Add(new EmailAddress(i));
                }


                MyHtmlString = "Dear Team, <br/><br/>";
                MyHtmlString += "Please Find Attached the detailed Faculty Class Attendance Tracker(Attendance not marked data) for all lectures taught during the day.<br/><br/><br/>";
                MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid #190404'><thead style='background-color: #d3cccc !important;color: #0e0e0e !important;'><tr style='border: 1px solid #190404;'><th style='border: 1px solid #190404;' scope='col'>Attendance Date</th><th style='border: 1px solid #190404;' scope='col'>Slot</th>" +
              "<th style='border: 1px solid #190404;' scope='col'>Section</th>" + "<th style='border: 1px solid #190404;' scope='col'>Course Name</th><th style='border: 1px solid #190404;' scope='col'>Faculty Name</th></tr></thead><tbody>";

                for (int i = 0; i < dr.Length; i++)
                {
                    MyHtmlString += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404; text-align:center;'> " + dr[i][1].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][3].ToString() + " - " + dr[i][4].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][14].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[i][10].ToString() + "-" + dr[i][11].ToString() + "</td>";
                    MyHtmlString += "<td style='border: 1px solid #190404; text-align:center;'> " + dr[i][12].ToString() + "</td></tr>";
                }
                MyHtmlString += "</tbody></table><br/><br/>";
                MyHtmlString += "This is a system generated email. Please do not reply to this email.<br/><br/><br/>";
                MyHtmlString += "Warm Regards,<br/>Team ERP";

                var msg = new SendGrid.Helpers.Mail.SendGridMessage()
                {
                    From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ATLAS"),
                    Subject = "Faculty Class Attendance Tracker - " + fromdate + " (Daily)",
                    HtmlContent = MyHtmlString
                };


                //string fileContentsAsBase64 = Convert.ToBase64String(bytefile, 0, bytefile.Length);
                //var attachment = new Attachment
                //{
                //    Filename = "Test.xlsx",
                //    Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //    //Type = "application/vnd.ms-excel",
                //    Content = fileContentsAsBase64,
                //    Disposition = "attachment",
                //    ContentId = "1"
                //};
                
                //UploadBlobFile(dsfinal, bytefile);

                var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, to, msg.Subject, "", msg.HtmlContent);

                //msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                //{
                //    new SendGrid.Helpers.Mail.Attachment
                //    {
                //        Content = Convert.ToBase64String(bytefile),
                //        Filename = "test.xlsx",
                //        Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //        Disposition = "attachment"
                //    }
                //};

                //msg1.AddAttachment(attachment);
               // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
                msg1.AddCcs(cc);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                Console.WriteLine(msg1.Serialize());
                var response = await client.SendEmailAsync(msg1); ;
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

        }
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }


    protected void ddlAttendanceStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvClassSchedule.DataSource = null;
        lvClassSchedule.DataBind();
        btnSendEmail.Visible = false;
    }
}