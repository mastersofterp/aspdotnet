//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Emp_Pay_Slip_Download.aspx                                                  
// CREATION DATE :                                                      
// CREATED BY    :Purva Raut                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data.SqlClient;
//using IITMS;
//using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using IITMS.SQLServer.SQLDAL;
//using System.Net;
//using System.Net;
//using System.Net.Mail;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Web;
//using CrystalDecisions.Shared;
//using iTextSharp.text.pdf;
//using System.Configuration;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using mastersofterp_MAKAUAT;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Text;
using iTextSharp.text.pdf;

public partial class PAYROLL_REPORTS_Pay_Emp_Pay_Slip_Download : System.Web.UI.Page
{
     string pdfFile = "D:\\Emppayslip.pdf";
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                FillDropdown();
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_seq_Num_Allotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_seq_Num_Allotment.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0 and SALLOCK =1", "convert(datetime,monyear,103) DESC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlEmpType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO > 0", "EMPTYPENO ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Appointment.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindListViewList();
    }
    private void BindListViewList()
    {
        try
        {
                pnlSeqNum.Visible = true;
                string MonthYear = ddlMonthYear.SelectedItem.ToString();
                int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
                int EmpTypeNo = Convert.ToInt32(ddlEmpType.SelectedValue);
                int Staffno = Convert.ToInt32(ddlStaff.SelectedValue);
                string orderby = string.Empty;
                if (ddlorderby.SelectedValue == "0")
                {
                    orderby = "IDNO";
                }
                else
                {
                    if (ddlorderby.SelectedValue == "1")
                        orderby = "IDNO";
                    else
                        orderby = "SEQ_NO";
                }
                DataSet ds = GetEmpMonthFile(MonthYear, Staffno, collegeno, EmpTypeNo, orderby);
                lvEmployeeMonth.DataSource = ds;
                lvEmployeeMonth.DataBind();
                if (ds.Tables[0].Rows.Count <= 0)
                {

                    btnsend.Visible = false;
                    btnCancel.Visible = true;
                    objCommon.DisplayMessage(UpdatePanel1, "No Record found for Selection", this);
                }
                else
                {
                    btnsend.Visible = true;
                    btnshow.Visible = true;
                    btnCancel.Visible = true;
                }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_seq_Num_Allotment.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
     private DataSet GetEmpMonthFile(string monYear,int staff, int collegeNo,int EmpType, string orderby)
       {
                  DataSet ds = null;
                  try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[1] = new SqlParameter("@P_STAFF", staff);
                        objParams[2] = new SqlParameter("@P_MONYEAR", monYear);
                        objParams[3] = new SqlParameter("@P_EMPTYPENO", EmpType);
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_MONFILE_PAYSLIP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModifySalaryController.GetEmpMonthFile-> " + ex.ToString());
                    }
                    return ds;
      }
    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsend_Click(object sender, EventArgs e)
    {
        int count = 0;
        int ret = 0;
        Common objCommon1 = new Common();
        DataSet dsconfig = null;
        int status = 0;
        int Idno = 0;
        string url = string.Empty;
        string emailid;
        string path;
        int OrganizationId=Convert.ToInt32(Session["OrgId"]);
        string Type = "SalaryCerticaicate";
        string ReportName = objCommon.LookUp("PayReportConfiguration", "IDCardReportName", "IDCardType='" + Type + "' AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
        if (ReportName != "")
        {
             path = "~,Reports,Payroll,"+ReportName;
        }
        else
        {
             path = "~,Reports,Payroll,rptEmployee_SalarySlip.rpt";
        }
        bool salarylockstatus = Convert.ToBoolean(objCommon.LookUp("PAYROLL_SALFILE", "SALLOCK", "MONYEAR = '" + ddlMonthYear.SelectedItem.Text + "' AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STAFFNO = " + Convert.ToInt32(ddlStaff.SelectedValue) + ""));
        if (salarylockstatus == true)
        {
        string CodeStandard = objCommon.LookUp("Reff", "CODE_STANDARD", "");
        string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");
        ReportDocument crystalReport = new ReportDocument();
        // path = "~,Reports,Payroll,rptEmployee_SalarySlip.rpt";
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        crystalReport.Load(reportPath);
        crystalReport.Refresh();
        string subject = "Pay Slip Report for the Month of " + ddlMonthYear.SelectedItem.ToString();
       // string message = "Dear Sir/Ma'am , Attachment with this email is Pay Slip Report for the Month of " + ddlMonthYear.SelectedItem.ToString();
        var MailBody = new StringBuilder();
        MailBody.AppendFormat("Dear Sir/Ma'am, {0}\n\n\n", "  ");
        MailBody.AppendFormat("\nAttachment with this email is Pay Slip Report for the Month of " + ddlMonthYear.SelectedItem.ToString()+".", " ");
        MailBody.AppendLine(@"<br /> " + " ");
        MailBody.AppendLine(@"<br /> ");
        MailBody.AppendLine(@"<br /> ");
        MailBody.AppendLine(@"<br /> ");
        MailBody.AppendLine(@"<br />Thanks And Regards");
        MailBody.AppendLine(@"<br />"+ ddlCollege.SelectedItem.Text.ToString());
        foreach (ListViewDataItem lvitem in lvEmployeeMonth.Items)
        { 
            CheckBox chkbox = lvitem.FindControl("chkid") as CheckBox;
            HiddenField hdnidno = lvitem.FindControl("hdnidno") as HiddenField;
            Label lblEmailId = lvitem.FindControl("lblemailid") as Label;
        //    //emailid = "purva.raut@mastersofterp.com";
             // emailid = "anmolsawarkar@gmail.com";
        //    emailid = "prashant.wankar86@gmail.com";
            if (chkbox.Checked == true)
            {
                if (chkbox != null && chkbox.Checked)
                {
                    Idno = Convert.ToInt32(hdnidno.Value);
                    emailid = lblEmailId.Text.Trim();
                    if (emailid != string.Empty)
                    {
                        count = 1;
                        this.ConfigureCrystalReports(crystalReport);
                        crystalReport.SetParameterValue("@P_MON_YEAR", (ddlMonthYear.SelectedItem.Text));
                        crystalReport.SetParameterValue("@P_STAFF_NO", Convert.ToInt32(ddlStaff.SelectedValue));
                        crystalReport.SetParameterValue("@P_IDNO", Convert.ToInt32(hdnidno.Value));
                        crystalReport.SetParameterValue("@P_COLLEGE_NO", Convert.ToInt32(ddlCollege.SelectedValue));
                        crystalReport.SetParameterValue("@P_EMPTYPENO", 0);
                        crViewer.ReportSource = crystalReport;
                        // Updated  on 21-04-2023
                        if (issendgrid == "1" || issendgrid == "0")
                        {
                            dsconfig = objCommon1.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);
                            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
                            var client = new SendGridClient(apiKey);
                            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
                            var subject1 = subject;
                            var to = new EmailAddress(emailid, "");
                            var plainTextContent = "";
                            // var htmlContent = message;
                            var htmlContent = Convert.ToString(MailBody);
                            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                            var outputStream = new MemoryStream();
                            Stream stream;
                            var pdfReader = new PdfReader(crystalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                            var pdfStamper = new PdfStamper(pdfReader, outputStream);
                            //stream.CopyTo(outputStream);
                            byte[] bytes;
                            bytes = outputStream.ToArray();
                            var writer = pdfStamper.Writer;
                            writer.AddJavaScript(GetAutoPrintJs());
                            pdfStamper.Close();
                            var content = outputStream.ToArray();
                            outputStream.Close();
                            outputStream.Dispose();
                            msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                           {
                            new SendGrid.Helpers.Mail.Attachment
                           {
                                Content=Convert.ToBase64String(content),
                                Filename = "Payslip_"+ddlMonthYear.SelectedItem.ToString()+".pdf",
                                Type = "application/pdf",
                                Disposition = "attachment"
                            }
                         };
                            var response = client.SendEmailAsync(msg).Result;
                            string res = Convert.ToString(response.StatusCode);
                            if (res == "Accepted")
                            {
                                ret = 1;
                                //count = 1;
                            }
                            else
                            {
                                ret = 0;
                            }
                        }
                        else
                        {
                            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
                            string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                            MailMessage msg = new MailMessage();
                            try
                            {
                                msg.From = new MailAddress(fromAddress);
                                msg.To.Add(emailid);
                                // msg.Body = message;
                                msg.Body = Convert.ToString(MailBody);
                                msg.Attachments.Add(new System.Net.Mail.Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), "Payslip_" + ddlMonthYear.SelectedItem.ToString() + ".pdf"));
                                msg.IsBodyHtml = true;
                                msg.Subject = subject;
                                SmtpClient smt = new SmtpClient("smtp.gmail.com");
                                smt.Port = 587;
                                smt.Credentials = new NetworkCredential(fromAddress, fromPassword);
                                smt.EnableSsl = true;
                                smt.Send(msg);
                                //count = 1;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
         }
            if(count == 1)
            {
              string script = "<script>alert('Pay Slip Sent on Email Successfully')</script>";
              ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
            }
            else
            {
                string script = "<script>alert('Please Select Atleast One Employee')</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
            }
         }
         else
         {
            objCommon.DisplayMessage("Salary not locked for " + ddlMonthYear.SelectedItem.Text + " month.", this.Page);
           return;
         }
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
    static async Task<int> Execute(string Message, string toEmailId, string sub, string reportPath, ReportDocument crystalReport,string MonthYear,int Staffno,int idno,int Collegeno)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);
            //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            //var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
          //  var response = client.SendEmailAsync(msg).Result; 
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
            string ms=  ex.Message;
        }
        return ret;
    }
    protected string GetAutoPrintJs()
    {
        var script = new StringBuilder();
        script.Append("var pp = getPrintParams();");
        script.Append("pp.interactive= pp.constants.interactionLevel.full;");
        script.Append("print(pp);");
        return script.ToString();
    }

    //private void sendMail()
    //{
    //    MailMessage msg = new MailMessage();
    //    try
    //    {
    //        msg.From = new MailAddress("noreply4.mis@nitrr.ac.in");
    //        msg.To.Add("purva.raut@mastersofterp.com");
    //        msg.Body = "Employee Pay Slip";
    //        msg.Attachments.Add(new Attachment(pdfFile));
    //        msg.IsBodyHtml = true;
    //        msg.Subject = "Emp Data Report uptil " + DateTime.Now.ToString() + " date";
    //        SmtpClient smt = new SmtpClient("smtp.gmail.com");
    //        smt.Port = 587;
    //        smt.Credentials = new NetworkCredential("noreply4.mis@nitrr.ac.in", "mis@nitraipur4");
    //        smt.EnableSsl = true;
    //        smt.Send(msg);
    //        string script = "<script>alert('Mail Sent Successfully')</script>";
    //        ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    finally
    //    {
    //    }
    //}

    private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        //ConnectionInfo connectionInfo = CSMS_COMMON.GetCrystalConnection(Session["DataBase"].ToString().Trim());
        ConnectionInfo connectionInfo = GetCrystalConnectionFromClass();
        SetDBLogonForReport(connectionInfo, customReport);
        // customReport.VerifyDatabase();
    }

    public static void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
    {
        Tables tables = reportDocument.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
        {
            TableLogOnInfo tableLogonInfo = table.LogOnInfo;

            tableLogonInfo.ConnectionInfo = connectionInfo;
            table.ApplyLogOnInfo(tableLogonInfo);

        }
        //   reportDocument.VerifyDatabase();
    }
    protected void crViewer_Unload(object sender, EventArgs e)
    {
    }

    public static ConnectionInfo GetCrystalConnectionFromClass()
    {
        //DataInfo objdata = new DataInfo();
        ConnectionInfo connectionInfo = new ConnectionInfo();
        //string strcon = connectionInfo.ServerName;
        //string[] CON;
        //if (HttpContext.Current.Session["SOC_CODE"].ToString() == "")
        //{
        //    HttpContext.Current.Session["SOC_CODE"] = "NA";
        //    CON = objdata.SERLOGIN(HttpContext.Current.Session["SOC_CODE"].ToString()).Split(';');
        //}
        //else
        //{
        //    CON = objdata.SERLOGIN(HttpContext.Current.Session["SOC_CODE"].ToString()).Split(';');
        //}
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        builder.ConnectionString = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString; ;
        string server = builder["SERVER"] as string;
        string database = builder["DataBase"] as string;
        string UserID = builder["User ID"] as string;
        string password = builder["Password"] as string;

        //Following for Remote Server
        connectionInfo.UserID = UserID; //System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString();//"sa";
        connectionInfo.Password = password;//CON[0].Split('=').ToString(); ;// System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();//"M@ster$oftware";
        connectionInfo.ServerName = server;//CON[2].Split('=').ToString(); ;// "" + HttpContext.Current.Session["Server"].ToString().Trim() + "";// System.Configuration.ConfigurationManager.AppSettings["Server"].ToString();
        connectionInfo.DatabaseName = database;// CON[3].Split('=').ToString(); ;// CollegeCode;// System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();
        return connectionInfo;
    }


    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindListViewList();
    //}
}
//}
 //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
        //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
        //string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
        // Updated code to send Pay slip   REPORT ON EMAIL SING SMTP
        //int Idno = 0;
        //string url=string.Empty;
        //string emailid = string.Empty;
        //ReportDocument crystalReport = new ReportDocument();
        //string path = "~,Reports,Payroll,rptEmployee_SalarySlip.rpt";
        //string reportPath = Server.MapPath(path.Replace(",", "\\"));
        //crystalReport.Load(reportPath);
        //crystalReport.Refresh();

        //foreach (ListViewDataItem lvitem in lvEmployeeMonth.Items)
        //{
        //    CheckBox chkbox = lvitem.FindControl("chkid") as CheckBox;
        //    HiddenField hdn = lvitem.FindControl("hdnidno") as HiddenField;
        //    HiddenField hdnemail = lvitem.FindControl("hdnemailid") as HiddenField;
        //    emailid = hdnemail.Value;
        //    if (chkbox.Checked == true)
        //    {
        //        if (chkbox != null && chkbox.Checked)
        //        {
        //            this.ConfigureCrystalReports(crystalReport);
        //            crystalReport.SetParameterValue("@P_MON_YEAR", (ddlMonthYear.SelectedItem.Text));
        //            crystalReport.SetParameterValue("@P_STAFF_NO", Convert.ToInt32(ddlStaff.SelectedValue));
        //            crystalReport.SetParameterValue("@P_IDNO", Convert.ToInt32(hdn.Value));
        //            crystalReport.SetParameterValue("@P_COLLEGE_NO",(ddlCollege.SelectedValue));
        //            crystalReport.SetParameterValue("@P_EMPTYPENO", 0);
        //            crViewer.ReportSource = crystalReport;
               //MailMessage msg = new MailMessage();
               //try
               // {
               //    msg.From = new MailAddress("noreply4.mis@nitrr.ac.in");
               //    msg.To.Add("purva.raut@mastersofterp.com");
               //    msg.To.Add(emailid);
               //    msg.Body = "Employee Pay Slip for month of " +ddlMonthYear.SelectedItem.ToString();
               //      msg.Attachments.Add(new Attachment(pdfFile));
               // msg.Attachments.Add(new Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), "Payslip_"+ddlMonthYear.SelectedItem.ToString()+".pdf"));
              
               //    msg.IsBodyHtml = true;
               //  msg.Subject = "Pay Slip Report for the month of " + ddlMonthYear.SelectedItem.ToString();
               //  SmtpClient smt = new SmtpClient("smtp.gmail.com");
               //  smt.Port = 587;
               //  smt.Credentials = new NetworkCredential(fromAddress, fromPassword);
               //  smt.EnableSsl = true;
               //  smt.Send(msg);
        //}
        //catch (Exception ex)
        //{

        //}
        //        }
        //    }
        //}
        //string script = "<script>alert('Mail Sent Successfully')</script>";
        //ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
//try
//{
//    using (MailMessage msg = new MailMessage(fromAddress, emailid))
//    {
//        msg.Subject = subject;
//        // mm.Body = body;
//        msg.Body = Convert.ToString(MailBody);
//        msg.Attachments.Add(new System.Net.Mail.Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), "Payslip_" + ddlMonthYear.SelectedItem.ToString() + ".pdf"));
//        msg.IsBodyHtml = true;
//        SmtpClient smtp = new SmtpClient();
//        smtp.Host = "smtp.gmail.com";
//        smtp.EnableSsl = true;
//        NetworkCredential NetworkCred = new NetworkCredential(fromAddress, fromPassword);
//        smtp.UseDefaultCredentials = true;
//        smtp.Credentials = NetworkCred;
//        smtp.Port = 587;
//        smtp.Send(msg);

//        // ViewBag.Message = "Email sent.";
//    }
//}
//MailMessage msg = new MailMessage();
//try
//{
//    msg.From = new MailAddress(fromAddress);
//    msg.To.Add(emailid);
//    msg.Body = Convert.ToString(MailBody);
//    msg.Attachments.Add(new System.Net.Mail.Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), "Payslip_" + ddlMonthYear.SelectedItem.ToString() + ".pdf"));
//    msg.IsBodyHtml = true;
//    msg.Subject = subject;
//    SmtpClient smt = new SmtpClient("smtp.gmail.com");
//    smt.Port = 587;
//    smt.Credentials = new NetworkCredential(fromAddress, fromPassword);
//    smt.EnableSsl = true;
//    smt.Send(msg);
//}