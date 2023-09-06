using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Threading;
using System.Drawing;

public partial class ACADEMIC_EVENT_EventRegistrationDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventCreationController objEC = new EventCreationController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            PopulateDropDown();
            GetEventForRegistration();

            string ip = Request.ServerVariables["REMOTE_HOST"];
            Session["ipAddress"] = ip;
        }

    }
  
    protected void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENO");
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnRegister = sender as Button;
            ViewState["TITLE_ID"] = btnRegister.CommandArgument;
            int paid = Convert.ToInt32(objCommon.LookUp("ACD_EVENT_CREATION", "ISNULL(ISPAID,0) ISPAID", "EVENT_TITLE_ID=" + Convert.ToInt32(ViewState["TITLE_ID"])));

            if (paid == 1)
            {
                btnOnlinePayment.Visible = true;
                btnSave.Visible = false;
            }
            else
            {
                btnOnlinePayment.Visible = false;
                btnSave.Visible = true;
            }

            //objCommon.FillDropDownList(ddlParticipant, "ACD_PARTICIPANT_TYPE P INNER JOIN ACD_EVENT_FEE_DETAIL F ON(P.PARTICIPANT_ID = F.PARTICIPANT_ID)", "DISTINCT P.PARTICIPANT_ID", "P.PARTICIPANT_TYPE", "P.PARTICIPANT_ID > 0 AND EVENT_TITLE_ID =" + Convert.ToInt32(ViewState["TITLE_ID"]), "PARTICIPANT_ID");
            objCommon.FillDropDownList(ddlParticipant, "ACD_PARTICIPANT_TYPE", "PARTICIPANT_ID", "PARTICIPANT_TYPE", "PARTICIPANT_ID > 0", "PARTICIPANT_ID");

            hdnRegister.Value =ViewState["TITLE_ID"].ToString();
            pnlEventReg.Visible = false;
            lvEventReg.Visible = false;
            divRegister.Visible = true;
            divHead.Visible = false;
            divHome.Visible = false;
            
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventRegistrationDetails.btnRegister_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string CandidateName = string.Empty;
            string Mobile = string.Empty;
            string Email = string.Empty;
            int Gender = 0;
            int Participant = 0;
            int State = 0;
            string City = string.Empty;
            string OrgName = string.Empty;
            string OrgAddress = string.Empty;
            
            int TitleId =Convert.ToInt32(ViewState["TITLE_ID"]);
            CandidateName=txtName.Text.Trim();
            Mobile=txtMobile.Text.Trim();
            Email=txtEmail.Text.Trim();
            Gender=Convert.ToInt32(rdoGender.SelectedValue);
            Participant=Convert.ToInt32(ddlParticipant.SelectedValue);
            State = Convert.ToInt32(ddlState.SelectedValue);
            City = txtCity.Text.Trim();
            OrgName = txtOrgName.Text.Trim();
            OrgAddress = txtAddress.Text.Trim();
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            Session["ipAddress"] = ipAddress;
            string EventName = objCommon.LookUp("ACD_EVENT_CREATION", "EVENT_TITLE", "EVENT_TITLE_ID=" + TitleId);
            string StartDate = objCommon.LookUp("ACD_EVENT_CREATION", "CONVERT(varchar, EVENT_START_DATE, 103)", "EVENT_TITLE_ID=" + TitleId);
            string EndDate = objCommon.LookUp("ACD_EVENT_CREATION", "CONVERT(varchar, EVENT_END_DATE, 103)", "EVENT_TITLE_ID=" + TitleId);

            CustomStatus cs = (CustomStatus)objEC.AddEventRegistration(TitleId, CandidateName, Mobile, Email, Gender, Participant, State, City, OrgName, OrgAddress, Convert.ToString(Session["ipAddress"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('Do you want to Proceed For Payment');", true);
                string message = "<b>Dear " + CandidateName + "</b><br />";
                message += "<br />Your Event Registration done for <b>" + EventName + "</b>";
                message += "<br />Event Date <b>" + StartDate + "</b> to <b>" + EndDate + "</b>";
                message += "<br /><br /><br />Thank You<br />";
                message += "<br />Team MAKAUT, WB<br />";
                message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";
                string subject = "MAKAUT | Event Registration";
                Task<int> task = Execute(message, Email, subject);
                int status = task.Result;

                if (status == 1)
                {
                    objCommon.DisplayMessage("Registration submit successfully, check your email for registration details", this.Page);
                }

                ClearField();
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventRegistrationDetails.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

   
    protected void ClearField()
    {
        txtName.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        rdoGender.ClearSelection();        
        ddlParticipant.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        txtCity.Text = string.Empty;
        txtOrgName.Text = string.Empty;
        txtAddress.Text = string.Empty;       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearField();
    }
    protected void GetEventForRegistration()
    {
        try
        {
            DataSet ds = objEC.GetEventDetailsForRegister();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventReg.DataSource = ds;
                lvEventReg.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventRegistrationDetails.GetEventForRegistration-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
            string sb = "Hello";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            divRegister.Visible = false;
            pnlEventReg.Visible = true;
            lvEventReg.Visible = true;
            divHead.Visible = true;
            pnlEventReg.Visible = true;
            divHome.Visible = true;
            GetEventForRegistration();
            Response.Redirect(Request.Url.ToString());
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventRegistrationDetails.btnBack_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    [System.Web.Services.WebMethod]
    public static bool CheckMobile(string mobile, int titleId)
    {
        bool status = false;
        string constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("CheckMobAvailabilityForEventReg", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mobile", mobile.Trim());
                cmd.Parameters.AddWithValue("@P_EVENT_TITLE_ID", titleId);
                conn.Open();
                status = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
        }
        return status;
    }

    [System.Web.Services.WebMethod]
    public static bool CheckEmail(string username,int titleId)
    {
        bool status = false;
        string constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("CheckEmailAvailabilityForEventReg", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", username.Trim());
                cmd.Parameters.AddWithValue("@P_EVENT_TITLE_ID", titleId);
                conn.Open();
                status = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
            }
        }
        return status;
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/default.aspx");
    }
    protected void btnOnlinePayment_Click(object sender, EventArgs e)
    {
        try
        {
            string CandidateName = string.Empty;
            string Mobile = string.Empty;
            string Email = string.Empty;
            int Participant = 0;
            int Gender = 0;
            int State = 0;
            string City = string.Empty;
            string OrgName = string.Empty;
            string OrgAddress = string.Empty;

            int TitleId = Convert.ToInt32(ViewState["TITLE_ID"]);
            CandidateName = txtName.Text.Trim();
            Mobile = txtMobile.Text.Trim();
            Email = txtEmail.Text.Trim();
            Participant = Convert.ToInt32(ddlParticipant.SelectedValue);
            //Session["ipAddress"]
            Gender = Convert.ToInt32(rdoGender.SelectedValue);
            State = Convert.ToInt32(ddlState.SelectedValue);
            City = txtCity.Text.Trim();
            OrgName = txtOrgName.Text.Trim();
            OrgAddress = txtAddress.Text.Trim();

            string Regfee = objCommon.LookUp("ACD_EVENT_FEE_DETAIL", "REG_FEE", "PARTICIPANT_ID =" + Participant + " AND EVENT_TITLE_ID=" + TitleId);

            int status1 = 0;
            int Currency = 1;
            double amount = 0;
            amount = Convert.ToDouble(Regfee);

        Reprocess:
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            Random ram = new Random();
            int i = ram.Next(1, 9);
            int j = ram.Next(21, 51);
            int k = ram.Next(471, 999);
            int l = System.DateTime.Today.Day;
            int m = System.DateTime.Today.Month;
            string refno = (i + "" + j + "" + k + "" + l + "" + m).ToString();

            string str1 = objCommon.LookUp("ACD_EVENT_PAYMENT_LOG", "ORDER_ID", "ORDER_ID='" + refno + "'");

            if (str1 != "" || str1 != string.Empty)
            {
                goto Reprocess;
            }


            Session["studAmt"] = amount;
            ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
            Session["studName"] = CandidateName;
            Session["studPhone"] = Mobile;
            Session["studEmail"] = Email;
            Session["studrefno"] = refno;
            Session["Participant"] = Participant;
            Session["TitleId"] = TitleId;
            Session["homelink"] = "EventRegistrationDetails.aspx";
            string datetm = indianTime.ToString("dd-MMM-yyyy");
            string status = "Not Continued";
            string transactionid = string.Empty;


            CustomStatus cs = (CustomStatus)objEC.AddEventRegistrationTemp(TitleId, CandidateName, Mobile, Email, Gender, Participant, State, City, OrgName, OrgAddress, Convert.ToDouble(amount), Convert.ToString(Session["ipAddress"]), refno);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                int result = objEC.InsertOnlinePayment_Log(Convert.ToInt32(TitleId), Convert.ToInt32(Participant), Convert.ToDouble(amount), refno, transactionid, CandidateName, Mobile, Email, status, Convert.ToString(Session["ipAddress"]));

                if (result > 0)
                {
                    string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + refno + "'");
                    if (orderid != "" || orderid != string.Empty || orderid == refno)
                    {
                        Response.Write(datetm);

                        //Response.Redirect("https://makauttest.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavEventRequest.aspx", false);
                        Response.Redirect("https://makaut.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavEventRequest.aspx", false);
                        //Response.Redirect("http://localhost:63344/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavEventRequest.aspx");

                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                        //Response.Redirect("http://localhost:63344/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavEventRequest.aspx");
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Online Payment Not Done, Please Try Again..!!", this.Page);
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void ddlParticipant_SelectedIndexChanged(object sender, EventArgs e)
    {
        int paid = Convert.ToInt32(objCommon.LookUp("ACD_EVENT_FEE_DETAIL", "count(1)", "EVENT_TITLE_ID=" + Convert.ToInt32(ViewState["TITLE_ID"]) + " AND PARTICIPANT_ID=" + Convert.ToInt32(ddlParticipant.SelectedValue) + " AND ISNULL(ISPAID,0)=1 AND REG_FEE>0"));

        if (paid == 1)
        {
            string paymentamount = Convert.ToString(objCommon.LookUp("ACD_EVENT_FEE_DETAIL", "CASE WHEN REG_FEE = '' OR REG_FEE = 0 THEN 'N/A' ELSE REG_FEE + ' Rs.' END AS REG_FEE", "EVENT_TITLE_ID=" + Convert.ToInt32(ViewState["TITLE_ID"]) + " AND PARTICIPANT_ID=" + Convert.ToInt32(ddlParticipant.SelectedValue) + " AND ISNULL(ISPAID,0)=1"));

            if (paymentamount != "")
            {
                lblPayment.Text = paymentamount;
                lblPayment.ForeColor = Color.Green;
            }
            else
            {
                lblPayment.Text = paymentamount;
                lblPayment.ForeColor = Color.Red;
            }


            btnOnlinePayment.Visible = true;
            btnSave.Visible = false;
        }
        else
        {
            lblPayment.Text = "N/A";
            lblPayment.ForeColor = Color.Red;
            btnOnlinePayment.Visible = false;
            btnSave.Visible = true;
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
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
    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkDownload = (ImageButton)(sender);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);
            string filename = lnkDownload.CommandArgument;
            string filepath = Server.MapPath("~/UPLOAD_FILES/Event/" + filename);
            fileDownload(filename, filepath);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.btnDownload_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void fileDownload(string fileName, string fileUrl)
    {
        Page.Response.Clear();
        bool success = ResponseFile(Page.Request, Page.Response, fileName, fileUrl, 1024000);
        if (!success)
        {
            objCommon.DisplayMessage(this.Page, "File Not Found!", this.Page);
            return;
        }
        Page.Response.End();
    }
    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 10240; //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
}