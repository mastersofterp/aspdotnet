using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
public partial class ACADEMIC_SendEmailToStudents : System.Web.UI.Page
{
    Common objCommon = new Common();
    OnlineAdmissionController objOA = new OnlineAdmissionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();
                    PoplulateDropDown();
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
                Response.Redirect("~/notauthorized.aspx?page=SendEmailToStudents.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SendEmailToStudents.aspx");
        }
    }
    protected void rdoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
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
            BindStudentList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string userno = string.Empty;string Email = string.Empty;string message = string.Empty;string Subject = string.Empty;string appIds = string.Empty;
        int sendGrid=Convert.ToInt32(objCommon.LookUp("REFF","ISNULL(SENDGRID_STATUS,0)SENDGRID_STATUS",""));
        try
        {
            int count = 0;
            int status = 0;
            foreach (ListViewDataItem lv in lvStudList.Items)
            {
                CheckBox cbRow = lv.FindControl("chkSelect1") as CheckBox;
                //CheckBox chek = item.FindControl("chkSelect1") as CheckBox;
                Label lblEmail = lv.FindControl("lblEmail") as Label;
                Label lblStudmobile = lv.FindControl("lblStudmobile") as Label;
                if (cbRow.Checked==true)
                {
                    count++;
                    Email = lblEmail.Text.ToString().Trim();
                    Subject = txtSubject.Text.ToString().Trim();
                    string s = txtMessage.Text.ToString().Trim();
                    message = s.Replace("\r\n", "<br/>");

                    if (sendGrid == 1)
                    {
                        Task<int> task = Execute(message, Email, Subject);
                        status = task.Result;
                    }
                    else
                    {
                        sendEmail(message, Email, Subject);
                    }
                }
                
                //else
                //{
 
                //}
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student.", this.Page);
                return;
            }
            else if (count > 0)
            {
                objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
                Clear();
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void PoplulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_USER_REGISTRATION UR INNER JOIN ACD_ADMBATCH AD ON(UR.ADMBATCH=AD.BATCHNO)", "DISTINCT (ADMBATCH)", "AD.BATCHNAME", "BATCHNO > 0", "ADMBATCH DESC");
            if (ddlAdmBatch.Items.Count == 2)
            {
                ddlAdmBatch.SelectedIndex = 1;
            }
            objCommon.FillDropDownList(ddlProgramme, "ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "(CASE WHEN UGPGOT=1 THEN 'UG' WHEN UGPGOT=2 THEN 'PG' END) PROGRAMME_TYPE", "UGPGOT > 0", "UGPGOT");
            if (ddlProgramme.Items.Count == 2)
            {
                ddlProgramme.SelectedIndex = 1;
            }
            objCommon.FillDropDownList(ddlDegree, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=BP.DEGREENO)", "DISTINCT BP.DEGREENO", "DBO.FN_DESC('DEGREENAME',BP.DEGREENO) DEGREENAME", "BP.DEGREENO > 0 AND UGPGOT=" + Convert.ToInt32(ddlProgramme.SelectedValue), "DEGREENAME");
            if (ddlDegree.Items.Count < 2)
            {
                ddlDegree.SelectedIndex = 1;
            }
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
            if (ddlProgramme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=BP.DEGREENO)", "DISTINCT BP.DEGREENO", "DBO.FN_DESC('DEGREENAME',BP.DEGREENO) DEGREENAME", "BP.DEGREENO > 0 AND UGPGOT=" + Convert.ToInt32(ddlProgramme.SelectedValue), "DEGREENAME");

                if (ddlDegree.Items.Count < 2)
                {
                    ddlDegree.SelectedIndex = 1;
                }
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BindStudentList()
    {
        try
        {
            DataSet dsStudList = objOA.GetStudentsForEmailSMS(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(rdoList.SelectedValue), Convert.ToInt32(ddlProgramme.SelectedValue));
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                lvStudList.DataSource = dsStudList;
                lvStudList.DataBind();
                lvStudList.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);
                lvStudList.DataSource = null;
                lvStudList.DataBind();
                lvStudList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlProgramme.SelectedIndex= -1;
            ddlProgramme.Items.Clear();
            ddlProgramme.Items.Add(new ListItem("Please Select", "0"));

            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select","0"));

            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;

            if (ddlAdmBatch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlProgramme, "ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "(CASE WHEN UGPGOT=1 THEN 'UG' WHEN UGPGOT=2 THEN 'PG' END) PROGRAMME_TYPE", "UGPGOT > 0", "UGPGOT");                
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
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName,SUBJECT_OTP", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new MailAddress(toEmailId, "");
            // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
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
                //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                ServicePointManager.ServerCertificateValidationCallback =
               delegate(object s, X509Certificate certificate,
               X509Chain chain, SslPolicyErrors sslPolicyErrors)
               {
                   return true;
               };
                smtp.Send(message);
                return ret = 1;
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
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

            //dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_NAME", "OrganizationId=2" , string.Empty);  //Convert.ToInt32(Session["OrgId"])
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
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
    protected void Clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgramme.Items.Clear();
        ddlProgramme.Items.Add(new ListItem("Please Select", "0"));

        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        rdoList.SelectedIndex = 1;
        txtMessage.Text = string.Empty;
        txtSubject.Text = string.Empty;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        lvStudList.Visible = false;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}