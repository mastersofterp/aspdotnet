using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Text;
using IITMS.UAIMS;
using System.IO;
public partial class ACADEMIC_POC_Email_SMS_WhatsApp : System.Web.UI.Page
{
    //SendEmailCommon objEmail = new SendEmailCommon();

    SendEmailCommonV2 objSendEmail = new SendEmailCommonV2(); //Object Creation
    Common objCommon = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string name = txtName.Text.ToString().Equals("") ? "Admin" : txtName.Text.ToString().TrimEnd();
            string email = txtMail.Text.ToString().Equals("") ? "nikhil.lambe@mastersofterp.co.in" : "nikhil.lambe@mastersofterp.co.in";
            string mobile = txtMobile.Text.ToString().Equals("") ? "0000" : txtMobile.Text.ToString().TrimEnd();
            string message = txtMessage.Text.ToString().Equals("") ? "DEMO" : txtMessage.Text.ToString().TrimEnd(); ;
            string subject = txtSub.Text.ToString().Equals("") ? "DEMO" : txtSub.Text.ToString().TrimEnd(); ;
            string pageNo = Request.QueryString["pageno"].ToString();
            int result = 0;
            string ccMails = string.Empty;
            string bccMails = string.Empty;
            DataSet ds = new DataSet();
            string attachmentFilename = string.Empty;
            string path = Server.MapPath("~/TempDocument/");
            string filename = "";                    
            Byte[] Imgbytes = null;
            ccMails = txtCC.Text.ToString();
            bccMails = txtBCC.Text.ToString();
            if (fuAttach.HasFile)
            {
                if (!Directory.Exists(path))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(path);
                }
                ViewState["FileName"] = fuAttach.FileName;
                filename = Convert.ToString(ViewState["FileName"]);
                string Imgfile = string.Empty;
                fuAttach.SaveAs(path + Path.GetFileName(fuAttach.FileName));
                if (filename != string.Empty)
                {
                    path = path + filename;
                    string LogoPath = path;
                    Imgbytes = File.ReadAllBytes(LogoPath);
                    Imgfile = Convert.ToBase64String(Imgbytes);
                }
                result = objSendEmail.SendEmail_New(pageNo, email, message, subject, ccMails, bccMails, ds, filename, Imgbytes, "image/png/pdf");
            }
            else if (fuAttach.HasFile == false && ccMails == "" && bccMails=="")
            {
                result = objSendEmail.SendEmail_New(pageNo, email, message, subject); 
            }
            else if (fuAttach.HasFile == false && ccMails != "" || bccMails != "")
            {
                result = objSendEmail.SendEmail_New(pageNo, email, message, subject, ccMails, bccMails);
            }
            if (result == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage()", "alert(`Email sent successfully.`)", true);
                Clear();
                return;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnSMS_Click(object sender, EventArgs e)
    {
        try
        {
            string mobile = txtMobile.Text.ToString();
            string templatename = "Today Attandance";
            string TemplateID = "";
            string TEMPLATE = "";
            string message = "";
            DataSet dsTemp = objSendEmail.SendSMS_New(Request.QueryString["pageno"].ToString(), mobile, templatename, TemplateID, TEMPLATE);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                ViewState["TemplateName"] = templatename;
                TEMPLATE = dsTemp.Tables[0].Rows[0]["TEMPLATE"].ToString();
                TemplateID = dsTemp.Tables[0].Rows[0]["TEM_ID"].ToString();
            }
            message = TEMPLATE;
            message = message.Replace("{#var#}", Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"));
            message = message.Replace("{#var1#}", txtName.Text);
            message = message.Replace("{#var2#}", Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"));

            // Create a StringBuilder and append the template
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(message);
            // Get the final message string
            TEMPLATE = stringBuilder.ToString();
            objSendEmail.SendSMS_New(Request.QueryString["pageno"].ToString(), mobile, ViewState["TemplateName"].ToString(), TemplateID, TEMPLATE);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage()", "alert(`Message sent successfully.`)", true);
            Clear();
            return;
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnWhatsapp_Click(object sender, EventArgs e)
    {
        try
        {
            string templateName = string.Empty;
            string mobile = txtMobile.Text;
            string Name = txtName.Text;
            string att =Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            string course = "COURSE-13";
            string Dept = "Dep 123";
            string SP_Name = string.Empty; string SP_Parameters = string.Empty; string SP_Values = string.Empty;
            DataSet dsCheck = null;
            var bodys = "";
            string pageNo = Request.QueryString["pageno"].ToString();
            SP_Name = "PKG_GET_WHATSAPP_CREDENTIAL";
            SP_Parameters = "@P_AL_NO";//,@P_CONFIG_TYPE";
            SP_Values = "" + Convert.ToInt32(pageNo) + "";// + configType + "";
            dsCheck = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
            if (dsCheck.Tables[0].Rows.Count > 0)
            {
                string API_URL = dsCheck.Tables[0].Rows[0]["WHATSAAP_API_URL"].ToString();
                string API_KEY = dsCheck.Tables[0].Rows[0]["API_KEY"].ToString();
                string UserName = dsCheck.Tables[0].Rows[0]["WHATSAAP_USER"].ToString();
                templateName = "Daily Attendance";

                SP_Name = "PKG_ACD_GET_WHATSAPP_TEMPLATE";
                SP_Parameters = "@P_PAGE_NO,@P_TEMP_NAME";
                SP_Values = "" + Convert.ToInt32(pageNo) + "," + templateName;
                DataSet dsTemplate = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, SP_Values);
                if (dsTemplate.Tables[0].Rows.Count > 0)
                {
                    bodys = dsTemplate.Tables[0].Rows[0]["TEMPLATE"].ToString();
                    if (Session["OrgId"].ToString().Equals("1") && dsTemplate.Tables[0].Rows[0]["TEMPLATE_NAME"].ToString() == "Daily Attendance")
                    {
                        bodys = bodys.Replace("{#apiKey#}", API_KEY);
                        bodys = bodys.Replace("{#destination#}", mobile);
                        bodys = bodys.Replace("{#userName#}", UserName);
                        bodys = bodys.Replace("{#name#}", Name);
                        bodys = bodys.Replace("{#att#}", att);
                        bodys = bodys.Replace("{#course#}", course);
                        bodys = bodys.Replace("{#department#}", Dept);
                    }
                }
            }
            objSendEmail.SendWhatsApp_New(mobile, Request.QueryString["pageno"].ToString(), bodys, dsCheck);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage()", "alert(`WhatsApp message sent successfully.`)", true);
            Clear();
            return;
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        txtName.Text = "";
        txtMail.Text = "";
        txtMessage.Text = "";
        txtSub.Text = "";
        txtMobile.Text = "";
        txtCC.Text = "";
        txtBCC.Text = "";
        
    }
}