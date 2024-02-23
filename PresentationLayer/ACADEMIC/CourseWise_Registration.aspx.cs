//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STUDENT COURSE REGISTRATION REPORT                                   
// CREATION DATE : 22-AUG-2011                                                       
// CREATED BY    :                                                    
// MODIFIED DATE : 22/12/2021
// MODIFIED BY   : Rishabh Bajirao                                                                     
// MODIFIED DESC : Added only one selection for Excel report.(Umesh Sir)                                                                   
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using ClosedXML.Excel;
using System.Text;
using SendGrid;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using System.Net.Security;

public partial class CourseWise_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                btnCancel_Click(sender, e);
                //Page Authorization
                CheckPageAuthorization();

                PopulateDropDown();

                if (Session["usertype"].ToString() != "1")
                {
                    dvCollege.Visible = false;
                    Div1.Visible = false;
                    btnExcel.Visible = false;
                    btnStudentAllotment.Visible = true;
                }
                else
                {
                    dvCollege.Visible = true;
                    Div1.Visible = true;
                    btnExcel.Visible = true;
                    btnStudentAllotment.Visible = false;
                    
                }

            }
            divMsg.InnerHtml = string.Empty;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 22/12/2021
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 22/12/2021
        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID) INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO = SR.SESSIONNO)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 AND (SR.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR SR.UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + " OR SR.UA_NO_TUTR = " + Convert.ToInt32(Session["userno"]) + ")", "S.SESSIONID DESC");
            }
            else
            {
                this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
                ////Fill Dropdown Session 
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string SessionNo = string.Empty;
        foreach (ListItem itm in ddlCollege.Items)
        {
            if (itm.Selected != true)
                continue;
            SessionNo += itm.Value + ",";
        }

        SessionNo = SessionNo.Remove(SessionNo.Length - 1);

        DataSet ds = objCC.GetAllCourseRegistrationData(SessionNo, Convert.ToInt32(rdbReport.SelectedValue));
        if (rdbReport.SelectedValue == "1")
        {
            //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
            //    ds.Tables.RemoveAt(0);
            //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count <= 0)
            //    ds.Tables.RemoveAt(2);

            ds.Tables[0].TableName = "Registration Details";
            ds.Tables[1].TableName = "Course Registered Student List";

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                ds.Tables[0].Rows.Add("No Record Found");

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                ds.Tables[1].Rows.Add("No Record Found");

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                    wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CourseReg_Summary_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else if (rdbReport.SelectedValue == "2")
        {
            //if (ds.Tables[1] != null)
            //    ds.Tables.RemoveAt(1);
            //if (ds.Tables[3] != null && ds.Tables[3].Rows.Count <= 0)
            //    ds.Tables.RemoveAt(3);

            ds.Tables[0].TableName = "Registration Count";
            ds.Tables[1].TableName = "Activity Status";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count < 1)
                ds.Tables[0].Rows.Add("No Record Found");

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count < 1)
                ds.Tables[1].Rows.Add("No Record Found");

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                    wb.Worksheets.Add(dt);   //Add System.Data.DataTable as Worksheet.

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=CourseReg_Statistical.xlsx");
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

            DataSet dsPending = null;
            string sp_Name = string.Empty; string sp_Call = string.Empty; string sp_Value = string.Empty;
            sp_Name = "PKG_GET_COURSE_REG_PENDING_EXCEL";
            sp_Call = "@P_SESSIONNO";
            sp_Value = "" + SessionNo + "";
            dsPending = objCommon.DynamicSPCall_Select(sp_Name, sp_Call, sp_Value);
            if (dsPending.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsPending.Tables)
                        wb.Worksheets.Add(dt);   //Add System.Data.DataTable as Worksheet.

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=CourseReg_Pending.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            //objCC.GetAllCourseRegistrationData(SessionNo, Convert.ToInt32(rdbReport.SelectedValue));   
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        chkEnabledEmailSubject.Checked = false;
        txtSubject.Text = txtMessage.Text = string.Empty;
        dvSubject.Visible = false;
        dvEmail.Visible = false;
        rdbReport.SelectedIndex = -1;
        ddlCollege.SelectedIndex = -1;
        rdbCourse.SelectedIndex = -1;
        pnlCourse.Visible = lvStudents.Visible = false;
        ddlCourseType.SelectedIndex = -1;
        ddlReportType.SelectedIndex = -1;
        ddlSession.SelectedIndex = -1;

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCollege.SelectedIndex > 0)
        //{
        // this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

        //    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
        //    ddlSession.Focus();
        //}
        //else
        //{
        //    ddlSession.Items.Clear();
        //    ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //}
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //txtSubject.Text = txtMessage.Text = string.Empty;
        //dvSubject.Visible = false;
        //dvEmail.Visible = false;

        //if (rdbReport.SelectedValue == "3")
        //{
        //    string SessionNo = string.Empty;
        //    foreach (ListItem item in ddlCollege.Items)
        //    {
        //        if (item.Selected == true)
        //            SessionNo += item.Value + ",";
        //    }
        //    SessionNo = SessionNo.Remove(SessionNo.Length - 1);
        //    //DataSet ds = objCC.GetPendingCourseRegDataForStudent(SessionNo);
        //    string spName = string.Empty; string spCall = string.Empty; string spValue = string.Empty;
        //    spName = "PKG_GET_COURSE_REG_PENDING_SHOW";
        //    spCall = "@P_SESSIONNO,@P_COURSE_MODE";
        //    int courseType = 0;
        //    courseType = Convert.ToInt32(rdbCourse.SelectedValue);
        //    string session = SessionNo.Replace(",", "$");
        //    spValue = "" + session + "," + courseType + "";
        //    DataSet ds = objCommon.DynamicSPCall_Select(spName, spCall, spValue);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        pnlCourse.Visible = lvStudents.Visible = true;
        //        lvStudents.DataSource = ds;
        //        lvStudents.DataBind();
        //        hftot.Value = lvStudents.Items.Count.ToString();
        //    }
        //    else
        //    {
        //        lvStudents.DataSource = null;
        //        lvStudents.DataBind();
        //        pnlCourse.Visible = lvStudents.Visible = false;
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
        //        return;
        //    }
        //}
        string collegenos = string.Empty;
        foreach (ListItem itm in ddlCollege.Items)
        {
            if (itm.Selected != true)
                continue;
            collegenos += itm.Value + ",";
        }

        collegenos = collegenos.Remove(collegenos.Length - 1);
        int sessionid = 0, reporttype = 0, coursetype = 0;
        sessionid = Convert.ToInt32(ddlSession.SelectedValue);
        reporttype = Convert.ToInt32(ddlReportType.SelectedValue);
        coursetype = Convert.ToInt32(ddlCourseType.SelectedValue);

        if (ddlReportType.SelectedValue == "5")
        {
            DataSet ds = objCC.GetAllCourseRegistrationPendingReportData(sessionid, collegenos, coursetype);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlCourse.Visible = lvPendingStudentList.Visible = true;
                lvPendingStudentList.DataSource = ds;
                lvPendingStudentList.DataBind();
                hftot.Value = lvPendingStudentList.Items.Count.ToString();
            }
            else
            {
                pnlCourse.Visible = lvPendingStudentList.Visible = false;
                lvPendingStudentList.DataSource = null;
                lvPendingStudentList.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
            }
        }
        else
        {
            pnlCourse.Visible = lvPendingStudentList.Visible = false;
            lvPendingStudentList.DataSource = null;
            lvPendingStudentList.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
        }

    }
    protected void rdbReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbReport.SelectedValue == "3")
        {
            btnShow.Enabled = true;
            btnShow.Visible = true;
            //dvEmail.Visible = true;
            //dvSubject.Visible = true;
            chkEnabledEmailSubject.Visible = true;
            chkEnabledEmailSubject.Checked = false;
            lvStudents.Visible = true;
            btnSendEmail.Visible = true;
            divCourse.Visible = true;
        }
        else
        {
            btnShow.Enabled = false;
            btnShow.Visible = false;
            chkEnabledEmailSubject.Visible = false;
            chkEnabledEmailSubject.Checked = false;
            lvStudents.Visible = false;
            btnSendEmail.Visible = false;
            dvEmail.Visible = false;
            dvSubject.Visible = false;
            txtMessage.Text = string.Empty;
            txtSubject.Text = string.Empty;
            divCourse.Visible = false;
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        bool checkBxChecked = false;
        foreach (ListViewDataItem lvItem in lvPendingStudentList.Items)
        {
            CheckBox chkBx = lvItem.FindControl("chkreport") as CheckBox;
            if (chkBx.Checked == true)
            {
                checkBxChecked = true;
                break;
            }
        }
        string msg = txtMessage.Text;
        string subject = txtSubject.Text;
        string MailSendStatus = string.Empty;
        string MailNotSendStatus = string.Empty;
        string useremails = string.Empty;
        string name = string.Empty;
        string idno = string.Empty;
        string Sregno = string.Empty;
        DataSet ds1 = null;
        DataSet ds2 = null;
        try
        {
            if (!checkBxChecked)
            {
                objCommon.DisplayMessage(UpdatePanel1, "please select atleast One Student", this.Page);
                return;
            }
            string coursetype = string.Empty;
            coursetype = ddlCourseType.SelectedItem.Text;
            string CodeStandard = objCommon.LookUp("REFF", "CODE_STANDARD", "");
            string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
            string OrgName = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
            foreach (ListViewDataItem lvItem in lvPendingStudentList.Items)
            {
                CheckBox chkBox = lvItem.FindControl("chkreport") as CheckBox;
                HiddenField idNo = lvItem.FindControl("hidIdNo") as HiddenField;
                Label lblStudname = lvItem.FindControl("lblStudentName") as Label;
                Label lblSemester = lvItem.FindControl("lblSemester") as Label;
                //  Label lblScheme = lvItem.FindControl("lblScheme") as Label;
                //Label lblRegno = lvItem.FindControl("lblRegNo") as Label;
                if (chkBox.Checked == true)
                {
                    //Sregno = lblRegno.Text.TrimEnd();
                    idno = chkBox.ToolTip.TrimEnd();
                    string studname = lblStudname.Text;
                    string useremail = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + chkBox.ToolTip);
                    if (useremail != string.Empty)
                    {
                        string nbody = MessageBody(studname, msg, lblSemester.Text, CodeStandard, coursetype);
                        int status;

                        if (issendgrid == "1")
                        {
                            Task<int> ret = Execute(nbody, useremail, CodeStandard + " ERP || Course Registration Pending..");
                            status = ret.Result;
                        }
                        else
                            status = sendEmail(nbody, useremail, CodeStandard + " ERP ||Course Registration Pending..");

                        MailSendStatus += chkBox.ToolTip + ',';
                    }
                    else
                        MailNotSendStatus += chkBox.ToolTip + ',';
                }
            }

            if (MailNotSendStatus != string.Empty)
                ds1 = (objCommon.FillDropDown("ACD_STUDENT", "(STUDNAME + '  #  ' + REGNO) collate DATABASE_DEFAULT  AS STUDNAME", "IDNO", "IDNO IN (" + MailNotSendStatus.TrimEnd(',') + ")", "IDNO"));


            if (MailSendStatus != string.Empty)
                ds2 = (objCommon.FillDropDown("ACD_STUDENT", "(STUDNAME + '  #  ' + REGNO) collate DATABASE_DEFAULT AS STUDNAME", "IDNO", "IDNO IN (" + MailSendStatus.TrimEnd(',') + ")", "IDNO"));

            string MailSendTo = string.Empty;
            string MailNotSendTo = string.Empty;

            if (MailNotSendStatus != string.Empty)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "\n" + ",";
            }

            if (MailSendStatus != string.Empty)
            {
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    MailSendTo += ds2.Tables[0].Rows[i]["STUDNAME"].ToString() + ",";
            }

            if (MailSendTo != string.Empty)
                objCommon.DisplayMessage(this.UpdatePanel1, "Email Sent successfully to this Students : " + MailSendTo, this.Page);

            if (MailNotSendTo != string.Empty)
                objCommon.DisplayMessage(this.UpdatePanel1, "Email Not Sent to this students : " + MailNotSendTo, this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Bulk EmailSending-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkEnabledEmailSubject_CheckedChanged(object sender, EventArgs e)
    {
        txtSubject.Text = txtMessage.Text = string.Empty;
        if (chkEnabledEmailSubject.Checked)
        {
            dvSubject.Visible = true;
            dvEmail.Visible = true;
            btnSendEmail.Visible = true;
        }
        else
        {
            dvSubject.Visible = false;
            dvEmail.Visible = false;
            btnSendEmail.Visible = false;
        }
    }

    public string MessageBody(string studname, string message, string semesterNo, string OrgName, string coursetype)
    {
        const string EmailTemplate = "<html><body>" +
                              "<div align=\"center\">" +
                              "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                               "<tr>" +
                               "<td>" + "</tr>" +
                               "<tr>" +
                              "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                              "</tr>" +
                              "</table>" +
                              "</div>" +
                              "</body></html>";
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>Alert !!</h1>");
        mailBody.AppendFormat("Dear" + " " + "<b>" + studname + "," + "</b>");   //b
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<b> This is to inform you that your registration process for the " + coursetype + " is not yet completed.</b>" + "<br/><br/><br/>");
        mailBody.AppendFormat("You are requested to register yourself as soon as possible for the same, since this is a very crucial activity in the forthcoming academic session.<br/><br/>");
        mailBody.AppendFormat("This is an auto generated response to your email. Please do not reply to this mail.");
        mailBody.AppendFormat("<br /><br /><br /><br />Regards,<br />");   //bb
        mailBody.AppendFormat(OrgName + "<br /><br />");   //bb

        string Mailbody = mailBody.ToString();
        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
        return nMailbody;
    }

    public int sendEmail(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
            var toAddress = new MailAddress(toEmailId, "");
            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

            var smtp = new SmtpClient
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
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
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

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            ret = (Convert.ToString(response.StatusCode) == "Accepted") ? 1 : 0;
        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }
    protected void rdbCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbCourse.SelectedIndex > -1)
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                pnlCourse.Visible = lvStudents.Visible = true;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            CourseController objStud = new CourseController();
            DataSet dsCollegeSession = objStud.GetCollegeBySessionid(1, Convert.ToInt32(ddlSession.SelectedValue));
            ddlCollege.Items.Clear();
            ddlCollege.DataSource = dsCollegeSession;
            ddlCollege.DataValueField = "COLLEGE_ID";
            ddlCollege.DataTextField = "COLLEGE_NAME";
            ddlCollege.DataBind();
        }
        else
        {
            ddlCollege.DataSource = null;
            ddlCollege.DataBind();
        }
        //ddlCollege.SelectedIndex = -1;
        //rdbReport.SelectedIndex = -1;
        ddlReportType.SelectedIndex = 0;
        ddlCourseType.SelectedIndex = 0;
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
    if (ddlReportType.SelectedIndex == 5)
        {
        divPendingCourseType.Visible = true;
        //chkEnabledEmailSubject.Visible = true;
        btnSendEmail.Visible = true;
        btnShow.Visible = true;
        }
    else
        {
        divPendingCourseType.Visible = false;
        //chkEnabledEmailSubject.Visible = false;
        btnShow.Visible = false;
        btnSendEmail.Visible = false;
        }
    }

    //modified by Nehal on 03/04/2023
    protected void btnExcel_Click1(object sender, EventArgs e)
    {
        string collegenos = string.Empty;
        foreach (ListItem itm in ddlCollege.Items)
        {
            if (itm.Selected != true)
                continue;
            collegenos += itm.Value + ",";
        }

        collegenos = collegenos.Remove(collegenos.Length - 1);
        int sessionid = 0, reporttype = 0, coursetype = 0;
        sessionid = Convert.ToInt32(ddlSession.SelectedValue);
        reporttype = Convert.ToInt32(ddlReportType.SelectedValue);
        coursetype = Convert.ToInt32(ddlCourseType.SelectedValue);

        if (ddlReportType.SelectedValue != "5")
        {
            if (ddlReportType.SelectedValue == "6")
            {
                DataSet ds = objCC.GetAllCourseRegistrationDataExcel(sessionid, collegenos);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (reporttype == 1)
                    {
                        ds.Tables[0].TableName = "Core Courses Summary";
                        ds.Tables[1].TableName = "Core Courses Details";

                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");
                    }
                    else if (reporttype == 2)
                    {
                        ds.Tables[0].TableName = "Elective Courses Summary";
                        ds.Tables[1].TableName = "Elective Courses Details";
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");
                    }
                    else if (reporttype == 3)
                    {
                        ds.Tables[0].TableName = "Global Elective Courses Summary";
                        ds.Tables[1].TableName = "Global Elective Courses Details";
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");
                    }
                    else if (reporttype == 4)
                    {
                        ds.Tables[0].TableName = "Core Courses Summary";
                        ds.Tables[1].TableName = "Core Courses Details";
                        ds.Tables[2].TableName = "Elective Courses Summary";
                        ds.Tables[3].TableName = "Elective Courses Details";
                        ds.Tables[4].TableName = "Global Elective Courses Summary";
                        ds.Tables[5].TableName = "Global Elective Courses Details";
                        ds.Tables[6].TableName = "Course Wise Regsitration Count";
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");

                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count <= 0)
                            ds.Tables[2].Rows.Add("No Record Found");

                        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count <= 0)
                            ds.Tables[3].Rows.Add("No Record Found");

                        if (ds.Tables[4] != null && ds.Tables[4].Rows.Count <= 0)
                            ds.Tables[4].Rows.Add("No Record Found");

                        if (ds.Tables[5] != null && ds.Tables[5].Rows.Count <= 0)
                            ds.Tables[5].Rows.Add("No Record Found");

                        if (ds.Tables[6] != null && ds.Tables[6].Rows.Count <= 0)
                            ds.Tables[6].Rows.Add("No Record Found");
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=CourseReg_Details_Summary.xlsx");
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                }
            }
            else if (ddlReportType.SelectedValue == "7")
                {

                DataSet ds = objCC.GetAllStudentWiseCourseRegistrationSummaryReportExcelData(sessionid, collegenos);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                    ds.Tables[0].TableName = "Core Courses Pending Summary";
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                        ds.Tables[0].Rows.Add("No Record Found");

                    using (XLWorkbook wb = new XLWorkbook())
                        {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=StdentWiseCourseSummary.xlsx");
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                    }
                }
            else
            {
                DataSet ds = objCC.GetAllCourseRegistrationReportExcelData(sessionid, collegenos, reporttype);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (reporttype == 1)
                    {
                        ds.Tables[0].TableName = "Core Courses Summary";
                        ds.Tables[1].TableName = "Core Courses Details";

                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");
                    }
                    else if (reporttype == 2)
                    {
                        ds.Tables[0].TableName = "Elective Courses Summary";
                        ds.Tables[1].TableName = "Elective Courses Details";
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");
                    }
                    else if (reporttype == 3)
                    {
                        ds.Tables[0].TableName = "Global Elective Courses Summary";
                        ds.Tables[1].TableName = "Global Elective Courses Details";
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");
                    }
                    else if (reporttype == 4)
                    {
                        ds.Tables[0].TableName = "Core Courses Summary";
                        ds.Tables[1].TableName = "Core Courses Details";
                        ds.Tables[2].TableName = "Elective Courses Summary";
                        ds.Tables[3].TableName = "Elective Courses Details";
                        ds.Tables[4].TableName = "Global Elective Courses Summary";
                        ds.Tables[5].TableName = "Global Elective Courses Details";
                        ds.Tables[6].TableName = "Course Wise Regsitration Count";
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                            ds.Tables[0].Rows.Add("No Record Found");

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                            ds.Tables[1].Rows.Add("No Record Found");

                        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count <= 0)
                            ds.Tables[2].Rows.Add("No Record Found");

                        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count <= 0)
                            ds.Tables[3].Rows.Add("No Record Found");

                        if (ds.Tables[4] != null && ds.Tables[4].Rows.Count <= 0)
                            ds.Tables[4].Rows.Add("No Record Found");

                        if (ds.Tables[5] != null && ds.Tables[5].Rows.Count <= 0)
                            ds.Tables[5].Rows.Add("No Record Found");

                        if (ds.Tables[6] != null && ds.Tables[6].Rows.Count <= 0)
                            ds.Tables[6].Rows.Add("No Record Found");
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=CourseReg_Details_Summary.xlsx");
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                }
            }
        }
        else if (ddlReportType.SelectedValue == "5")
        {
            int ctype = Convert.ToInt32(ddlCourseType.SelectedValue);
            DataSet ds = objCC.GetAllCourseRegistrationPendingReportExcelData(sessionid, collegenos, ctype);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ctype == 1)
                {
                    ds.Tables[0].TableName = "Core Courses Pending Summary";
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                        ds.Tables[0].Rows.Add("No Record Found");

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=CoreCourseReg_Pending_Summary.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                if (ctype == 2)
                {
                    ds.Tables[0].TableName = "Elective Pending Summary";
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                        ds.Tables[0].Rows.Add("No Record Found");

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=ElectiveCourseReg_Pending_Summary.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                if (ctype == 3)
                {
                    ds.Tables[0].TableName = "Global Courses Pending Summary";
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                        ds.Tables[0].Rows.Add("No Record Found");

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                            wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=GlobalCourseReg_Pending_Summary.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
            }
        }
        
        else
            {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
            }
    }

    protected void btnStudentAllotment_Click(object sender, EventArgs e)
    {
        try
        {
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int UA_NO = Convert.ToInt32(Session["userno"]);
            DataSet dsAllotment = null;
            string sp_Name = string.Empty; string sp_Call = string.Empty; string sp_Value = string.Empty;
            sp_Name = "PKG_ACD_COURSE_REGISTRATION_ALLOTMENT_DETAILS_FOR_FACULTY";
            sp_Call = "@P_SESSION_ID,@P_UA_NO";
            sp_Value = "" + SessionNo + "," + UA_NO + "";
            dsAllotment = objCommon.DynamicSPCall_Select(sp_Name, sp_Call, sp_Value);
            if (dsAllotment.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsAllotment.Tables)
                        wb.Worksheets.Add(dt);   //Add System.Data.DataTable as Worksheet.

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=StudentRegistrationReport.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch
        {
        }
    }
}

