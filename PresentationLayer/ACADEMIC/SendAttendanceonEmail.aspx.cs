using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using BusinessLogicLayer.BusinessLogic;


public partial class ACADEMIC_SendAttendanceonEmail : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

    protected void Page_PreInit(object sender, EventArgs e)
    {
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

                    PopulateDropDownList();

                    DataSet ds = objCommon.FillDropDown("ACD_ATTENDANCE_TRIGGER_MAIL_CONFIG", "STUD_CC_MAIL", "STUD_BCC_MAIL,DAILY_FAC_TO_MAIL,DAILY_FAC_CC_MAIL,DAILY_FAC_BCC_MAIL,ABSENT_STUD_TO_MAIL,ABSENT_STUD_CC_MAIL,ABSENT_STUD_BCC_MAIL", "SRNO > 0", "SRNO");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Session["STUD_CC_MAIL"] = ds.Tables[0].Rows[0]["STUD_CC_MAIL"].ToString();
                        Session["DAILY_FAC_TO_MAIL"] = ds.Tables[0].Rows[0]["DAILY_FAC_TO_MAIL"].ToString();
                        Session["DAILY_FAC_CC_MAIL"] = ds.Tables[0].Rows[0]["DAILY_FAC_CC_MAIL"].ToString();
                        Session["ABSENT_STUD_TO_MAIL"] = ds.Tables[0].Rows[0]["ABSENT_STUD_TO_MAIL"].ToString();
                        Session["ABSENT_STUD_CC_MAIL"] = ds.Tables[0].Rows[0]["ABSENT_STUD_CC_MAIL"].ToString();
                        Session["ABSENT_STUD_BCC_MAIL"] = ds.Tables[0].Rows[0]["ABSENT_STUD_BCC_MAIL"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 and SM.ODD_EVEN<>3", "S.SESSIONID DESC");
            this.objCommon.FillDropDownList(ddlSessionAbsentStudent, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 and SM.ODD_EVEN<>3", "S.SESSIONID DESC");

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COURSE_TEACHER CT ON (CM.COLLEGE_ID = CT.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "UA_NO = " + Session["userno"] + " AND ISNULL(CT.CANCEL,0)=0", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.COLLEGE_ID = DB.COLLEGE_ID)", "DISTINCT SM.COLLEGE_ID ", "SM.COLLEGE_NAME", "DB.COLLEGE_ID IN(" + Session["college_nos"] + ") AND SM.COLLEGE_ID > 0", "SM.COLLEGE_ID");
                }
            }
            else
            {
                ddlSem.Items.Clear();
                ddlSem.Items.Add("Please Select");
                ddlSem.SelectedItem.Value = "0";

                ddlSchoolInstitute.Items.Clear();
                ddlSchoolInstitute.Items.Add("Please Select");
                ddlSchoolInstitute.SelectedItem.Value = "0";
            }
            lvAttStatus.DataSource = null;
            lvAttStatus.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Sessionnos = "";
            if (ddlSchoolInstitute.SelectedIndex > 0)
            {
                Sessionnos = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT STRING_AGG(SESSIONNO,',')", " IS_ACTIVE = 1 AND SESSIONID =" + ddlSession.SelectedValue + " AND COLLEGE_ID =" + ddlSchoolInstitute.SelectedValue);
                ViewState["Sessionnos"] = Sessionnos;
                //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER SEM, ACD_SESSION_MASTER SM", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "SEM.SEMESTERNO%2 IN(CASE WHEN SM.ODD_EVEN=1 THEN SM.ODD_EVEN ELSE 0 END) AND SM.SESSIONNO IN(" + Sessionnos + ") AND SEM.SEMESTERNO>0", "SEM.SEMESTERNO");
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_ATTENDANCE A ON(S.SEMESTERNO = A.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON(A.SESSIONNO = SM.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ddlSchoolInstitute.SelectedValue), "S.SEMESTERNO");
               
            }
            else
            {
                ddlSem.Items.Clear();
                ddlSem.Items.Add("Please Select");
                ddlSem.SelectedItem.Value = "0";
            }
            lvAttStatus.DataSource = null;
            lvAttStatus.DataBind();
        }
        catch (Exception ex)
        {

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

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtTodate.Text))
            {
                objCommon.DisplayMessage(updAttendance, "From date cannot be greater than To date !!!", this.Page);
                return;
            }
            else
            {
                //ViewState["Sessionnos"] = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT STRING_AGG(SESSIONNO,',')", " IS_ACTIVE = 1 AND SESSIONID =" + ddlSession.SelectedValue + " AND COLLEGE_ID =" + ddlSchoolInstitute.SelectedValue);

                string SP_Name1 = "PKG_ACD_GET_STUDENT_DETAILS_FOR_SEND_ATTENDANCE_EMAIL";
                string SP_Parameters1 = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SEMESTERNO,@P_START_DATE,@P_END_DATE";
                string Call_Values1 = "" + ViewState["Sessionnos"].ToString().Replace(',', '$') + "," + Convert.ToInt32(ddlSchoolInstitute.SelectedValue) + "," + Convert.ToInt32(ddlSem.SelectedValue) + "," + txtFromDate.Text + "," + txtTodate.Text;

                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvAttStatus.DataSource = ds.Tables[0];
                    lvAttStatus.DataBind();
                    ViewState["DYNAMIC_DATASET"] = ds.Tables[1];
                    ViewState["DYNAMIC_DATASET_LIST"] = ds.Tables[0];
                }
                else
                {
                    lvAttStatus.DataSource = null;
                    lvAttStatus.DataBind();
                    objCommon.DisplayMessage(updAttendance, "Record Not Found !!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSendEmailtostudent_Click(object sender, EventArgs e)
    {
        string Emailids = "";
        string CCEmailids = "";
        DataSet dsconfig = null;
        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "CollegeName,SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP,CODE_STANDARD", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
        try
        {
            bool flag = false; int status = 0;
            foreach (ListViewDataItem item in lvAttStatus.Items)
            {
                CheckBox chk = item.FindControl("chkRows") as CheckBox;
                Label lblEmailStud = item.FindControl("lblEmailStud") as Label;
                Label lblEmailparent = item.FindControl("lblEmailparent") as Label;
                Label lblStudName = item.FindControl("lblStudName") as Label;
                Label lblMotherEmail = item.FindControl("lblMotherEmail") as Label;

                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DYNAMIC_DATASET"];

                if ((chk.Checked == true && lblEmailStud.Text != string.Empty))
                {
                    flag = true;
                    Emailids = lblEmailStud.Text + ',';
                    DataRow[] dr = dt.Select("REGNO='" + chk.ToolTip + "'");
                    if (lblEmailparent.Text != string.Empty)
                    {
                        Emailids += lblEmailparent.Text + ',';
                    }
                    if (lblMotherEmail.Text != string.Empty)
                    {
                        Emailids += lblMotherEmail.Text + ',';
                    }
                    Emailids = Emailids.TrimEnd(',');
                    //Emailids = "shubhamgirhepunje@gmail.com,python.shubham@gmail.com,nikhillambe97@gmail.com";
                    if (Convert.ToString(Session["STUD_CC_MAIL"]) != "" || Convert.ToString(Session["STUD_CC_MAIL"]) != string.Empty)
                    {
                        CCEmailids = Convert.ToString(Session["STUD_CC_MAIL"]);
                    }

                    string MyHtmlString = string.Empty;

                    MyHtmlString = "Dear Student & Parent, <br/><br/>";
                    MyHtmlString += dsconfig.Tables[0].Rows[0]["CollegeName"].ToString() + " focuses on unique pedagogies and hands on project based learning to prepare students for a path-breaking career. <br/><br/>"
                    + "Our faculty at " + dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString() + " take great effort in delivering the curriculum which is vetted by the Industry & is based on research, class participation, presentation and most important reflection.<br/><br/>" +
                    "We would like to inform you that we have a ZERO TOLERANCE policy where in a minimum of 75% attendance is mandatory for all students.<br/><br/>" +
                    "We request for complete involvement and focus from students to make the learning outcomes of this program a great success.<br/><br/>" +
                    "<div style='width: 600px;height: 120px;border: 1px solid black;margin:0 auto;'><p style='color:blue;text-align:center'><b>Please find enclosed the attendance from " + txtFromDate.Text + " to " + txtTodate.Text + " for </b></p>" +
                     "<p style='color:blue;text-align:center'><b>" + lblStudName.Text + "(" + dr[0][3].ToString() + ")" + " at </b></p><p style='color:blue;text-align:center'><b>" + dr[0][1].ToString() + "</b></p></div><br/><br/><p style='text-align:center'><b>" + dr[0][1].ToString() + "</b></p><p style='text-align:center'><b>" + dr[0][17].ToString() + "</b></p>" +
                     "<p style='text-align:center'><b>" + dr[0][16].ToString() + "</b></p><p style='text-align:center'><b>" + "Attendance Statistics – From " + txtFromDate.Text + " to " + txtTodate.Text + " for </b></p><p style='text-align:center'><b>" + dr[0][15].ToString() + "</b></p>";

                    MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid black;margin:0 auto;'><thead><tr><th scope='col'>Subject Category</th><th scope='col'>Course Code</th>" +
                   "<th scope='col'>Subject Name</th>" + "<th scope='col'>Present</th><th scope='col'>Absent</th><th scope='col'>Total</th></tr></thead><tbody>";

                    for (int i = 0; i < dr.Length; i++)
                    {
                        MyHtmlString += "<tr><td style='text-align:center;'> " + dr[i][19].ToString() + "</td>";
                        MyHtmlString += "<td style='text-align:center;'> " + dr[i][6].ToString() + "</td>";
                        MyHtmlString += "<td style='text-align:center;'> " + dr[i][7].ToString() + "</td>";
                        MyHtmlString += "<td style='text-align:center;'> " + dr[i][9].ToString() + "</td>";
                        MyHtmlString += "<td style='text-align:center;'> " + dr[i][18].ToString() + "</td>";
                        MyHtmlString += "<td style='text-align:center;'> " + dr[i][8].ToString() + "</td></tr>";
                    }
                    MyHtmlString += "</tbody></table><br/><b>Overall Present Percentage : " + dr[0][20].ToString() + "</b><br/><br/> Regards,<br/>" + dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString(); ;
                    string Subject = "Attendance Statistics – From " + txtFromDate.Text + " - " + " to " + txtTodate.Text + " of " + dr[0][1].ToString();

                    status = objSendEmail.SendEmail(Emailids,MyHtmlString,Subject,CCEmailids,"");

                    //Task<int> task = Execute(dr, 1, lblEmailStud.Text, lblEmailparent.Text, lblStudName.Text, txtFromDate.Text, txtTodate.Text, Emailids, CCEmailids);
                    //Emailids = "";
                    //CCEmailids = "";
                    //status = task.Result;
                }
            }
            if (flag == false)
            {
                objCommon.DisplayMessage(updAttendance, "Please Select Atleast One CheckBox !!!", this.Page);
            }
            else
            {
                if (status == 1)
                {
                    objCommon.DisplayMessage(updAttendance, "Email Send Successfully !!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updAttendance, "Unable To Send Email !!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            Emailids = "";
        }
    }
    

    static async Task<int> Execute(DataRow[] dr, int type, string EmailidStud, string EmailidParent, string StudName, string fromdate, string todate, string Emailids, string CCEmailids)
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

                var emails = Emailids.Split(',');
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

                MyHtmlString = "Dear Student & Parent, <br/><br/>";
                MyHtmlString += "ATLAS SKILLTECH UNIVERSITY focuses on unique pedagogies and hands on project based learning to prepare students for a path-breaking career. <br/><br/>"
                + "Our faculty at ATLAS take great effort in delivering the curriculum which is vetted by the Industry & is based on research, class participation, presentation and most important reflection.<br/><br/>" +
                "We would like to inform you that we have a ZERO TOLERANCE policy where in a minimum of 75% attendance is mandatory for all students.<br/><br/>" +
                "We request for complete involvement and focus from students to make the learning outcomes of this program a great success.<br/><br/>" +
                "<div style='width: 600px;height: 120px;border: 1px solid black;margin:0 auto;'><p style='color:blue;text-align:center'><b>Please find enclosed the attendance from " + fromdate + " to " + todate + " for </b></p>" +
                 "<p style='color:blue;text-align:center'><b>" + StudName + "(" + dr[0][3].ToString() + ")" + " at </b></p><p style='color:blue;text-align:center'><b>" + dr[0][1].ToString() + "</b></p></div><br/><br/><p style='text-align:center'><b>" + dr[0][1].ToString() + "</b></p><p style='text-align:center'><b>" + dr[0][17].ToString() + "</b></p>" +
                 "<p style='text-align:center'><b>" + dr[0][16].ToString() + "</b></p><p style='text-align:center'><b>" + "Attendance Statistics – From " + fromdate + " to " + todate + " for </b></p><p style='text-align:center'><b>" + dr[0][15].ToString() + "</b></p>";

                MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid black;margin:0 auto;'><thead><tr><th scope='col'>Subject Category</th><th scope='col'>Course Code</th>" +
               "<th scope='col'>Subject Name</th>" + "<th scope='col'>Present</th><th scope='col'>Absent</th><th scope='col'>Total</th></tr></thead><tbody>";

                for (int i = 0; i < dr.Length; i++)
                {
                    MyHtmlString += "<tr><td style='text-align:center;'> " + dr[i][19].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][6].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][7].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][9].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][18].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][8].ToString() + "</td></tr>";
                }
                MyHtmlString += "</tbody></table><br/><b>Overall Present Percentage : " + dr[0][20].ToString() + "</b><br/><br/> Regards,<br/>ATLAS SKILLTECH UNIVERSITY";
                var msg = new SendGrid.Helpers.Mail.SendGridMessage()
                {
                    From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ATLAS"),
                    Subject = "Attendance Statistics – From " + fromdate + " - " + " to " + todate + " of " + dr[0][1].ToString(),
                    HtmlContent = MyHtmlString
                };
                //msg.AddCcs(cc);
                var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, to, msg.Subject, "", msg.HtmlContent);
                msg1.AddCcs(cc);
                
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
            //else
            //{
            //    var plainTextContent = "";
            //    var to = new EmailAddress(EmailidParent, "");
            //    MyHtmlString = "Dear Student & Parent, <br/><br/>";
            //    MyHtmlString += "ATLAS SKILLTECH UNIVERSITY focuses on unique pedagogies and hands on project based learning to prepare students for a path-breaking career. <br/><br/>"
            //    + "Our faculty at ATLAS take great effort in delivering the curriculum which is vetted by the Industry & is based on research, class participation, presentation and most important reflection.<br/><br/>" +
            //    "We would like to inform you that we have a ZERO TOLERANCE policy where in a minimum of 75% attendance is mandatory for all students.<br/><br/>" +
            //    "We request for complete involvement and focus from students to make the learning outcomes of this program a great success.<br/><br/>" +
            //    "<div style='width: 600px;height: 120px;border: 1px solid black;margin:0 auto;'><p style='color:blue;text-align:center'><b>Please find enclosed the attendance from " + fromdate + " to " + todate + " for </b></p>" +
            //     "<p style='color:blue;text-align:center'><b>" + StudName + "(" + dr[0][3].ToString() + ")" + " at </b></p><p style='color:blue;text-align:center'><b>" + dr[0][1].ToString() + "</b></p></div><br/><br/><p style='text-align:center'><b>" + dr[0][1].ToString() + "</b></p><p style='text-align:center'><b>" + dr[0][17].ToString() + "</b></p>" +
            //     "<p style='text-align:center'><b>" + dr[0][16].ToString() + "</b></p><p style='text-align:center'><b>" + "Attendance Statistics – From " + fromdate + " to " + todate + " for </b></p><p style='text-align:center'><b>" + dr[0][15].ToString() + "</b></p>";

            //    MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid black;margin:0 auto;'><thead><tr><th scope='col'>Subject Category</th><th scope='col'>Course Code</th>" +
            //   "<th scope='col'>Subject Name</th>" + "<th scope='col'>Present</th><th scope='col'>Absent</th><th scope='col'>Total</th></tr></thead><tbody>";

            //    for (int i = 0; i < dr.Length; i++)
            //    {
            //        MyHtmlString += "<tr><td style='text-align:center;'> " + dr[i][19].ToString() + "</td>";
            //        MyHtmlString += "<td style='text-align:center;'> " + dr[i][6].ToString() + "</td>";
            //        MyHtmlString += "<td style='text-align:center;'> " + dr[i][7].ToString() + "</td>";
            //        MyHtmlString += "<td style='text-align:center;'> " + dr[i][9].ToString() + "</td>";
            //        MyHtmlString += "<td style='text-align:center;'> " + dr[i][18].ToString() + "</td>";
            //        MyHtmlString += "<td style='text-align:center;'> " + dr[i][8].ToString() + "</td></tr>";
            //    }
            //    MyHtmlString += "</tbody></table><br/><b>Overall Present Percentage : " + dr[0][20].ToString() + "</b><br/><br/> Regards,<br/>ATLAS SKILLTECH UNIVERSITY";
            //    var htmlContent = MyHtmlString;
            //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //    var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            //    string res = Convert.ToString(response.StatusCode);
            //    if (res == "Accepted")
            //    {
            //        ret = 1;
            //    }
            //    else
            //    {
            //        ret = 0;
            //    }
            //}
        }
        catch (Exception ex)
        {

        }
        return ret;
    }


    protected void lvAttStatus_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (lvAttStatus.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DYNAMIC_DATASET_LIST"];
            lvAttStatus.DataSource = dt;
            lvAttStatus.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSendEmailForWeekly_Click(object sender, EventArgs e)
    {

        string Emailids = "";
        string CCEmailids = "";
        string BCCEmailids = "";
        try
        {
            bool flag = false; int status = 0;
            string fdate = txtAbsentFromDate.Text;
            string todate = txtAbsentToDate.Text;
            int sessionno = Convert.ToInt32(ddlSessionAbsentStudent.SelectedValue);
            int college_id = 0;
            DateTime FromDate = txtAbsentFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtAbsentFromDate.Text);
            DateTime ToDate = txtAbsentToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtAbsentToDate.Text);
            DataSet ds = objAttC.GetAbsentStudentDataForWeeklySendMail(sessionno, college_id, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
           
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = (DataTable)ds.Tables[0];
                DataRow[] dr = dt.Select();
                if (Convert.ToString(Session["ABSENT_STUD_TO_MAIL"]) != "" || Convert.ToString(Session["ABSENT_STUD_TO_MAIL"]) != string.Empty)
                {
                    Emailids = Convert.ToString(Session["ABSENT_STUD_TO_MAIL"]);
                }
                else
                {
                    objCommon.DisplayMessage(updAbsentStudent, "Please define Absent Student Weekly To email id on Module Configuration", this.Page);
                }
                if (Convert.ToString(Session["ABSENT_STUD_CC_MAIL"]) != "" || Convert.ToString(Session["ABSENT_STUD_CC_MAIL"]) != string.Empty)
                {
                    CCEmailids = Convert.ToString(Session["ABSENT_STUD_CC_MAIL"]);
                }
                if (Convert.ToString(Session["ABSENT_STUD_BCC_MAIL"]) != "" || Convert.ToString(Session["ABSENT_STUD_BCC_MAIL"]) != string.Empty)
                {
                    BCCEmailids = Convert.ToString(Session["ABSENT_STUD_BCC_MAIL"]);
                }
                string FROMD = ds.Tables[0].Columns["FROM_DATE"].ToString();
                string TOD = ds.Tables[0].Columns["TO_DATE"].ToString();
                string subject = "Absent Student Report - Last Week(" + fdate + " to " + todate + ") (Below)75% attendance";
                ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["FROM_DATE"]);
                ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["TO_DATE"]);
                string MyHtmlString = string.Empty;
                MyHtmlString = "Dear Team, <br/><br/>";
                MyHtmlString += "Please Find Attached the detailed of Absent Student Report of the students who are having overall attendance below 75% for the date " + fdate + " to " + todate + " <br/><br/><br/>";
                MyHtmlString += "This is a system generated email. Please do not reply to this email.<br/><br/><br/>";
                MyHtmlString += "Warm Regards,<br/>Team ERP";

                status = objSendEmail.SendEmail(Emailids, MyHtmlString, subject, CCEmailids, BCCEmailids, ds, "AbsentStudentReportWeekly.xlsx",null,"excel");
                //Task<int> task = ExecuteAbsent(dr, Emailids, txtAbsentFromDate.Text, txtAbsentToDate.Text, CCEmailids, test, sessionno, college_id, fdate, todate, ds);
                //Emailids = "";
                //CCEmailids = "";
                //status = task.Result;

            }
            if (status == 1)
            {
                objCommon.DisplayMessage(updAttendance, "Email Send Successfully !!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updAttendance, "Unable To Send Email !!!" + status, this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }

    static async Task<int> ExecuteAbsent(DataRow[] dr, string Emailid, string fromdate, string todate, string CCEmailids, byte[] bytefile, int sessionno, int college_id, string FromDate, string ToDate, DataSet ds1)
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
            MyHtmlString += "Please Find Attached the detailed of Absent Student Report of the students who are having overall attendance below 75% for the date " + fromdate + " to " + todate + " <br/><br/><br/>";
            MyHtmlString += "This is a system generated email. Please do not reply to this email.<br/><br/><br/>";
            MyHtmlString += "Warm Regards,<br/>Team ERP";
          

            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "ATLAS"),
                Subject = "Absent Student Report - Last Week(" + fromdate + " to " + todate + ") (Below)75% attendance",
                HtmlContent = MyHtmlString
            };


            var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, to, msg.Subject, "", msg.HtmlContent);


            byte[] fileBytes = ConvertDataSetToExcel(ds1);
            var file = Convert.ToBase64String(fileBytes);
            msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            if (cc.Count > 0)
            {
                msg1.AddCcs(cc);
            }
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
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }

    static private MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptStudentAttAbsentList.rpt");
        customReport.Load(reportPath);
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {

                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');
                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";
                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        ConfigureCrystalReports(customReport);
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        return oStream;
    }
    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
 
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_RECEIPT_STATUS=" + Convert.ToInt32(ddlReceiptstatus.SelectedValue) + ",@P_RECEIPT_TYPE=" + rectype + ",@P_COUNTER=" + Convert.ToInt32(ddlCounter.SelectedValue) + ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text ;
            // url += "&param=@P_SESSION_ID=" + Convert.ToString(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToString(ddlSchoolInstitute.SelectedValue) + ",@P_FROM_DATE=" + Convert.ToString(Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd")) + ",@P_TO_DATE=" +Convert.ToString(Convert.ToDateTime(txtTodate.Text).ToString("yyyy/MM/dd")) + ",@P_COLLEGE_CODE=" + Convert.ToString(ddlSchoolInstitute.SelectedValue);
            url += "&param=@P_SESSION_ID=" + Convert.ToString(ddlSessionAbsentStudent.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToString(0) + ",@P_FROM_DATE=" + Convert.ToDateTime(txtAbsentFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtAbsentToDate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Convert.ToString(1);

            //url += "&param=@P_SESSION_ID=" + Convert.ToString(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToString(ddlSchoolInstitute.SelectedValue) + ",@P_FROM_DATE=" + Convert.ToString(txtTodate.Text) + ",@P_TO_DATE=" + Convert.ToString(txtTodate.Text) + ",@P_COLLEGE_CODE=" + Convert.ToString(ddlSchoolInstitute.SelectedValue);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updAbsentStudent, this.updAbsentStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    static byte[] ConvertDataSetToExcel(DataSet ds)
    {
        using (var ms = new MemoryStream())
        {
            using (var package = new ExcelPackage(ms))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    var worksheet = package.Workbook.Worksheets.Add(dt.TableName);

                    // Set header row style
                    var headerRange = worksheet.Cells[1, 1, 1, dt.Columns.Count];
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                    worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                    worksheet.Cells.AutoFitColumns();
                }

                package.Save();
                return ms.ToArray();
            }
        }
    }
    protected void btnShowAbsentStudent_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtAbsentFromDate.Text) > Convert.ToDateTime(txtAbsentToDate.Text))
            {
                objCommon.DisplayMessage(updAbsentStudent, "From date cannot be greater than To date !!!", this.Page);
                return;
            }
            else
            {
                //ViewState["Sessionnos"] = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT STRING_AGG(SESSIONNO,',')", " IS_ACTIVE = 1 AND SESSIONID =" + ddlSession.SelectedValue + " AND COLLEGE_ID =" + ddlSchoolInstitute.SelectedValue);

                string SP_Name1 = "PKG_ACD_STUDENT_ABSENT_ATTENDANCE_FOR_MAIL_END";
                string SP_Parameters1 = "@P_SESSION_ID,@P_COLLEGE_ID,@P_FROM_DATE,@P_TO_DATE";
                string Call_Values1 = "" + Convert.ToInt32(ddlSessionAbsentStudent.SelectedValue) + "," + Convert.ToInt32(0) + "," + Convert.ToDateTime(txtAbsentFromDate.Text).ToString("yyyy-MM-dd") + "," + Convert.ToDateTime(txtAbsentToDate.Text).ToString("yyyy-MM-dd");

                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvAbsentAttedance.DataSource = ds.Tables[0];
                    lvAbsentAttedance.DataBind();
                   
                }
                else
                {
                    lvAbsentAttedance.DataSource = null;
                    lvAbsentAttedance.DataBind();
                    objCommon.DisplayMessage(updAbsentStudent, "Record Not Found !!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("REPORT", "rptStudentAttAbsentList.rpt");
    }
    protected void btnCancelAbsenStudent_Click(object sender, EventArgs e)
    {
        ddlSessionAbsentStudent.SelectedIndex = 0;
        txtAbsentToDate.Text = "";
        txtAbsentFromDate.Text = "";
    }
}