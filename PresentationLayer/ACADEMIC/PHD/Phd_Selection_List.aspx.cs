using System;
using System.Collections;
using System.Configuration;
using System.IO;
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
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using EASendMail;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ACADEMIC_PHD_Phd_Selection_List : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    PhdController objPhd = new PhdController();

    #region Page Load
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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                this.CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                //  HiddenItem();
                //LvOffer.DataSource = GetData();
                //LvOffer.DataBind();
                PopulateDropDownList();
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
        }
    }

    public void HiddenItem()
    {

        //if (Convert.ToInt32(Session["OrgId"]) == 2)
        //{
        //    divTime.Visible = true;
        //    divDate.Visible = true;
        //}
        //else
        //{
        divTime.Visible = false;
        divDate.Visible = false;
        // }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Phd_Selection_List.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page  
            Response.Redirect("~/notauthorized.aspx?page=Phd_Selection_List.aspx");
        }
    }
    #endregion

    #region PhD Selection
    private void SHOW()
    {
        try
        {

            string SP_Name2 = "PKG_ACD_GET_PHD_STUDENTS_FOR_ENTRY_SELECTION_LIST";
            string SP_Parameters2 = "@P_ADMBATCH,@P_DEPARTMENT_NO,@P_PHD_MODE,@P_COLLEGE_ID";
            string Call_Values2 = "" + Convert.ToInt32(ddlAdmBatch.SelectedValue.ToString()) + "," +
                                 Convert.ToInt32(ddlprogram.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlPhDMode.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlSchool.SelectedValue.ToString()) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                Panel3.Visible = true;
                LvPhdMark.DataSource = dsStudList;
                LvPhdMark.DataBind();
                HiddenItem();
            }
            else
            {
                btnSubmit.Visible = false;
                objCommon.DisplayMessage(this.updmarkEntry, "No Record Found", this.Page);
                ddlAdmBatch.SelectedIndex = 0;
                ddlPhDMode.SelectedIndex = 0;
                ddlprogram.SelectedIndex = 0;
                ddlSchool.SelectedIndex = 0;
                Panel3.Visible = false;
                LvPhdMark.DataSource = null;
                LvPhdMark.DataBind();
                HiddenItem();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void PopulateDropDownList()
    {
        //objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION_ACTIVITY", "ADMBATCH BATCHNO", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH", "ACTIVITY_STATUS=1", "ADMBATCH");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
        objCommon.FillDropDownList(ddlsuper, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_NO");

        //objCommon.FillDropDownList(ddladmoffer, "ACD_PHD_REGISTRATION_ACTIVITY", "ADMBATCH BATCHNO", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH", "ACTIVITY_STATUS=1", "ADMBATCH");
        objCommon.FillDropDownList(ddladmoffer, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
        // objCommon.FillDropDownList(ddlpgm, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3", "CDB.BRANCHNO");

        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (Session["OrgId"].ToString() == "3")
        {
            divmandatory.Visible = false;
        }
        else
        {
            divmandatory.Visible = true;
        }
        SHOW();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OrgId"].ToString() == "3")
            {
                divmandatory.Visible = false;
            }
            else
            {
                divmandatory.Visible = true;
                if (ddlsuper.SelectedValue.ToString() == "0")
                {
                    objCommon.DisplayMessage(this, "Please Select Supervisor !!", this.Page);
                    return;
                }
            }
            PhdController marks = new PhdController();
            int count = 0;
            CustomStatus cs = 0;
            string phdmode = "";
            string username = ""; string name = "";
            string testmark = ""; string interviewmarks = ""; string total = "";
            string USERNO = ""; string Text_Marks = string.Empty; string INTER_Marks = string.Empty;
            foreach (ListViewDataItem dataitem in LvPhdMark.Items)
            {
                CheckBox chkBoxX = dataitem.FindControl("chkallotment") as CheckBox;
                Label lblusername = dataitem.FindControl("lblusername") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                Label lblbranch = dataitem.FindControl("lblbranch") as Label;
                Label lblphdmode = dataitem.FindControl("lblphdmode") as Label;
                Label lbltestmark = dataitem.FindControl("lbltestmark") as Label;
                Label lblinterviewmark = dataitem.FindControl("lblinterviewmark") as Label;
                Label lbltotalmark = dataitem.FindControl("lbltotalmark") as Label;
                if (chkBoxX.Checked == true && chkBoxX.Enabled == true)
                {
                    count++;
                    USERNO += lblusername.ToolTip + ',';
                    username += lblusername.Text.ToString() + ',';
                    name += lblname.Text.ToString() + ',';
                    total += lbltotalmark.Text + ',';

                    if (lbltestmark.Text.ToUpper() == "AB")
                    {
                        testmark += "-1" + ',';
                    }
                    else
                    {
                        testmark += lbltestmark.Text + ',';
                    }

                    if (lblinterviewmark.Text.ToUpper() == "AB")
                    {
                        interviewmarks += "-1" + ',';
                    }
                    else if (lblinterviewmark.Text.ToUpper() == "")
                    {
                        interviewmarks += "-0" + ',';
                    }
                    else
                    {
                        interviewmarks += lblinterviewmark.Text + ',';
                    }
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select At Least One Student !!", this.Page);
                HiddenItem();
                return;
            }
            USERNO = USERNO.TrimEnd(',');
            username = username.TrimEnd(',');
            name = name.TrimEnd(',');
            testmark = testmark.TrimEnd(',');
            interviewmarks = interviewmarks.TrimEnd(',');
            total = total.TrimEnd(',');
            cs = (CustomStatus)marks.PhdMarkEntrySelectionList(Convert.ToString(ddlsuper.SelectedValue), Convert.ToInt32(Convert.ToInt32(Session["OrgId"])), USERNO, Convert.ToInt32(ddlAdmBatch.SelectedValue.ToString()), Convert.ToInt32(ddlprogram.SelectedValue.ToString()), Convert.ToString(testmark), Convert.ToString(interviewmarks), Convert.ToString(total), Convert.ToString(ViewState["ipAddress"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSchool.SelectedValue.ToString()));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                Clear();
                HiddenItem();
            }
            else
            {
                objCommon.DisplayMessage(this, "Error in Saving", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void Clear()
    {
        Panel3.Visible = false;
        btnSubmit.Visible = false;
        ddlsuper.SelectedIndex = 0;
        ddlprogram.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        ddlPhDMode.SelectedIndex = 0;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
        HiddenItem();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void lkoffer_Click(object sender, EventArgs e)
    {
        divselection.Visible = false;
        divoffer.Visible = true;
        divlkselection.Attributes.Remove("class");
        divlkoffer.Attributes.Add("class", "active");
        HiddenItem();
        Clear();
    }
    protected void lkselection_Click(object sender, EventArgs e)
    {
        divoffer.Visible = false;
        divselection.Visible = true;
        divlkoffer.Attributes.Remove("class");
        divlkselection.Attributes.Add("class", "active");
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
        HiddenItem();
        CancelOffer();
    }
    #endregion

    #region SendEmail
    private void CancelOffer()
    {
        ddlmode.SelectedIndex = 0;
        ddladmoffer.SelectedIndex = 0;
        ddlpgm.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        LvOffer.DataSource = null;
        LvOffer.DataBind();
        btnSend.Visible = false;
        Panel1.Visible = false;
        txtStartTime.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        LvOffer.DataSource = null;
        LvOffer.DataBind();
        HiddenItem();
    }

    protected void btncanceloffer_Click(object sender, EventArgs e)
    {
        CancelOffer();
    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])));
        return ds;
    }

    #region commit
    //static async Task<int> Execute(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //        var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //        var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //        var client = new SendGridClient(apiKey);
    //        var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var subject = sub;
    //        var to = new EmailAddress(toEmailId, "");
    //        var plainTextContent = "";
    //        var htmlContent = Message;
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //        string res = Convert.ToString(response.StatusCode);
    //        if (res == "Accepted")
    //        {
    //            ret = 1;
    //        }
    //        else
    //        {
    //            ret = 0;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}

    //private int OutLook_Email(string Message, string toEmailId, string sub)
    //{

    //    int ret = 0;
    //    try
    //    {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        SmtpMail oMail = new SmtpMail("TryIt");
    //        oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //        oMail.To = toEmailId;
    //        oMail.Subject = sub;
    //        oMail.HtmlBody = Message;
    //        // SmtpServer oServer = new SmtpServer("smtp.live.com");
    //        SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
    //        oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //        oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        oServer.Port = 587;
    //        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
    //        Console.WriteLine("start to send email over TLS...");
    //        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
    //        oSmtp.SendMail(oServer, oMail);
    //        Console.WriteLine("email sent successfully!");
    //        ret = 1;
    //    }
    //    catch (Exception ep)
    //    {
    //        Console.WriteLine("failed to send email with the following error:");
    //        Console.WriteLine(ep.Message);
    //        ret = 0;
    //    }
    //    return ret;
    //}

    //public int sendEmail(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

    //        var smtp = new System.Net.Mail.SmtpClient
    //        {
    //            Host = "smtp.gmail.com",
    //            Port = 587,
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //        };
    //        using (var message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = sub,
    //            Body = Message,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true
    //        })
    //        {
    //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;

    //            // ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(message);
    //            return ret = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}
    #endregion

    private void SHOW1()
    {
        try
        {

            string SP_Name2 = "PKG_ACD_GET_PHD_STUDENTS_FOR_ENTRY_SELECTION_LIST";
            string SP_Parameters2 = "@P_ADMBATCH,@P_DEPARTMENT_NO,@P_PHD_MODE,@P_COLLEGE_ID ";
            string Call_Values2 = "" + Convert.ToInt32(ddladmoffer.SelectedValue.ToString()) + "," +
                                 Convert.ToInt32(ddlpgm.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlmode.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlCollege.SelectedValue.ToString()) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {

                Panel1.Visible = true;
                btnSend.Visible = true;
                LvOffer.DataSource = dsStudList;
                LvOffer.DataBind();
                HiddenItem();
                //if (Convert.ToInt32(Session["OrgId"]) == 2)
                //{
                divTime.Visible = true;
                divDate.Visible = true;
                //}
                //else
                //{
                //    divTime.Visible = false;
                //    divDate.Visible = false;
                //}

            }
            else
            {

                objCommon.DisplayMessage(this.updoffer, "No Record Found", this.Page);
                ddlmode.SelectedIndex = 0;
                ddlpgm.SelectedIndex = 0;
                ddladmoffer.SelectedIndex = 0;
                ddlCollege.SelectedIndex = 0;
                btnSend.Visible = false;
                Panel1.Visible = false;
                LvOffer.DataSource = null;
                LvOffer.DataBind();
                HiddenItem();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnShowOffer_Click(object sender, EventArgs e)
    {
        SHOW1();
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
        HiddenItem();
    }

    protected void ddlprogram_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
        HiddenItem();
    }

    protected void ddlPhDMode_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSubmit.Visible = false;
        btnSend.Visible = false;
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
        HiddenItem();
    }

    protected void ddladmoffer_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSend.Visible = false;
        Panel1.Visible = false;
        LvOffer.DataSource = null;
        LvOffer.DataBind();
        HiddenItem();
    }

    protected void ddlpgm_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSend.Visible = false;
        Panel1.Visible = false;
        LvOffer.DataSource = null;
        HiddenItem();
        LvOffer.DataBind();
    }

    protected void ddlmode_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSend.Visible = false;
        Panel1.Visible = false;
        LvOffer.DataSource = null;
        LvOffer.DataBind();
        HiddenItem();
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            btnSubmit.Visible = false;
            btnSend.Visible = false;
            Panel3.Visible = false;
            LvPhdMark.DataSource = null;
            LvPhdMark.DataBind();
            if (ddlSchool.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlprogram, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3 AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "CDB.BRANCHNO");

            }
            HiddenItem();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Something went wrong.`)", true);
            return;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            btnSend.Visible = false;
            Panel1.Visible = false;
            LvOffer.DataSource = null;
            LvOffer.DataBind();
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlpgm, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "CDB.BRANCHNO");
            }
            HiddenItem();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Something went wrong.`)", true);
            return;
        }
    }

    protected void LvOffer_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            PhdController marks = new PhdController();
            int count = 0;
            int status = 0;
            string USERNO = "";
            string USERNO_CRESCENT = "";
            foreach (ListViewDataItem lv in LvOffer.Items)
            {
                CheckBox cbRow = lv.FindControl("chkallotment") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    count++;

                    Label lblEmail = lv.FindControl("lblemail") as Label;
                    string useremail = lblEmail.Text.Trim().Replace("'", "");

                    if (lblEmail.Text.Trim() != "")
                    {

                        Label lblName = lv.FindControl("lblname") as Label; ;
                        Label lblUsername = lv.FindControl("lblusername") as Label;
                        Label degreeduration = lv.FindControl("lblbranch") as Label;
                        USERNO += cbRow.ToolTip + ',';
                        USERNO_CRESCENT += cbRow.ToolTip + '$';
                        string EMAILID = objCommon.LookUp("REFF", "Email", " OrganizationId='" + Convert.ToInt32(Session["OrgId"]) + "'");
                        string collegeid = objCommon.LookUp("REFF", "CollegeName", " OrganizationId='" + Convert.ToInt32(Session["OrgId"]) + "'");
                        string StudName = lblName.Text;
                        string username = lblUsername.Text;
                        string message = "<b>To<br> " + StudName + "," + "</b><br />";
                        message += "<br /><br /><br/>In response to his/her application," + collegeid + " allows him/her to take provisional admission in the " + degreeduration.Text + " course. His/Her admission will be confirmed after the payment of the requisite fees as given below and verification of his/her testimonials duly attested from office and No Objection letter from the same office. <br />";
                        message += "<br />As a part of next process, your Test and Interview is scheduled and its details are as follows<br />";
                        message += "<br />Program: " + degreeduration.Text + "<br />";
                        message += "<br />He/She is requested to remit the amount by a demand draft in favour of “" + collegeid + "”  India.<br />";
                        message += "<br/>It may be noted that, if he/she withdraws his/her admission for any reason, the University shall not be liable for refund of any fees.<br/>";
                        message += "<br />Regards<br />";
                        message += "<br />" + collegeid + "";
                        if (lblEmail.Text != string.Empty)
                        {
                            try
                            {
                                string email_type = string.Empty;
                                string Link = string.Empty;
                                int sendmail = 0;

                                #region Comment
                                //DataSet ds = getModuleConfig();
                                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                //{
                                //    email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                                //    Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                                //}

                                //if (email_type == "1" && email_type != "")
                                //{
                                //    status = sendEmail(message, lblEmail.Text, "PHD Offer Letter");
                                //}
                                //else if (email_type == "2" && email_type != "")
                                //{
                                //    Task<int> task = Execute(message, lblEmail.Text, "PHD Offer Letter");
                                //    status = task.Result;
                                //}
                                //if (email_type == "3" && email_type != "")
                                //{
                                //    status = OutLook_Email(message, lblEmail.Text, "PHD Offer Letter");
                                //}
                                #endregion

                                if (Convert.ToInt32(Session["OrgId"]) == 2)
                                {

                                    if (lblEmail.Text != string.Empty)
                                    {
                                        if (txtStartDate.Text != string.Empty && txtStartTime.Text != string.Empty)
                                        {
                                            int admbatch = Convert.ToInt32(ddladmoffer.SelectedValue.ToString());
                                            int deptno = Convert.ToInt32(ddlpgm.SelectedValue.ToString());
                                            int college_id = Convert.ToInt32(ddlCollege.SelectedValue.ToString());
                                            int phd_mode = Convert.ToInt32(ddlmode.SelectedValue.ToString());
                                            string Filename = "OfferLetter_" + StudName;
                                            string collegecode = Session["colcode"].ToString();
                                            DateTime DATE = Convert.ToDateTime(txtStartDate.Text);
                                            string Time = txtStartTime.Text;

                                            #region comment
                                            //            // ShowReport1("ApplicationForm", "PHDOfferLetter_CRESCENT.rpt", USERNO);
                                            //            string directoryPath = string.Empty;
                                            //            ReportDocument reportDocument = new ReportDocument();

                                            //            // Load the Crystal Report file (.rpt)
                                            //            string path = Server.MapPath("~/Reports/Academic/PHDOfferLetter_CRESCENT.rpt");
                                            //            reportDocument.Load(path);

                                            //            // Set the report's data source or parameters if needed
                                            //            reportDocument.SetParameterValue("@P_USERNO", USERNO_CRESCENT);
                                            //            reportDocument.SetParameterValue("@P_COLLEGE_CODE", Session["colcode"].ToString());
                                            //            reportDocument.SetParameterValue("@P_ADMBATCH", admbatch);
                                            //            reportDocument.SetParameterValue("@P_DEPARTMENT_NO", deptno);
                                            //            reportDocument.SetParameterValue("@P_PHD_MODE", phd_mode);
                                            //            reportDocument.SetParameterValue("@P_COLLEGE_ID", college_id);
                                            //            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                                            //            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                                            //            ConnectionInfo crConnectionInfo = new ConnectionInfo();
                                            //            Tables CrTables;
                                            //            // Set the report's database creditial
                                            //            crConnectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"];
                                            //            crConnectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                                            //            crConnectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"];
                                            //            crConnectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                                            //            CrTables = reportDocument.Database.Tables;
                                            //            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                                            //            {
                                            //                crtableLogoninfo = CrTable.LogOnInfo;
                                            //                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                                            //                CrTable.ApplyLogOnInfo(crtableLogoninfo);
                                            //            }

                                            //            string directoryName = "~/PhDOfferLetter" + "/";
                                            //            directoryPath = Server.MapPath(directoryName);

                                            //            if (!Directory.Exists(directoryPath.ToString()))
                                            //            {
                                            //                Directory.CreateDirectory(directoryPath.ToString());
                                            //            }
                                            //            // Export the report to a PDF file
                                            //            string exportFilePath = Server.MapPath("~/" + directoryPath + "/" + Filename + ".pdf");
                                            //            reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilePath);

                                            //            string message_crescent = "<b>To<br> " + StudName + "," + "</b><br />";
                                            //            Task<int> task = Execute(message_crescent, lblEmail.Text, "PHD Offer Letter", Filename, exportFilePath, Convert.ToInt32(Session["OrgId"]));
                                            //            status = task.Result;
                                            //            File.Delete(exportFilePath);



                                            //MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,PHDOfferLetter_CRESCENT.rpt", "@P_USERNO=" + USERNO_CRESCENT + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + admbatch + ",@P_DEPARTMENT_NO=" + deptno + ",@P_PHD_MODE=" + phd_mode + ",@P_COLLEGE_ID=" + college_id + "");

                                            // var bytesRpt = oAttachment1.ToArray();
                                            // var fileRpt = Convert.ToBase64String(bytesRpt);
                                            // byte[] test = (byte[])bytesRpt;
                                            // msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                                            //    {
                                            //        new SendGrid.Helpers.Mail.Attachment
                                            //        {
                                            //            Content = Convert.ToBase64String(test),
                                            //            Filename = "AbsentStudentReportWeekly.pdf",
                                            //            Type = "application/pdf",
                                            //            //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee Leave Taken Report");
                                            //            //return File(stream.ToArray(), "application/vnd.ms-excel", "EmployeeLeaveTakenReport.xlsx");
                                            //            Disposition = "attachment"
                                            //        }
                                            //    };
                                            #endregion

                                            Task<int> task = ExecuteOfferLetter(lblEmail.Text, admbatch, deptno, college_id, phd_mode, college_id, USERNO_CRESCENT, collegecode, StudName, DATE, Time);
                                            status = task.Result;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updoffer, "Please Select Date and Time ", this);
                                            if (Convert.ToInt32(Session["OrgId"]) == 2)
                                            {
                                                divTime.Visible = true;
                                                divDate.Visible = true;
                                            }
                                            else
                                            {
                                                divTime.Visible = false;
                                                divDate.Visible = false;
                                            }
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    status = objSendEmail.SendEmail(lblEmail.Text, message, "PHD Offer Letter"); //Calling Method
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        if (status == 1)
                        {
                            objCommon.DisplayMessage(this.updoffer, "Email Send Successfully", this);
                            HiddenItem();
                            //if (Convert.ToInt32(Session["OrgId"]) == 2)
                            //{

                            DateTime Config_SDate = Convert.ToDateTime(txtStartDate.Text);
                            string STime = txtStartTime.Text;
                            string IP_ADDRESS = Convert.ToString(ViewState["ipAddress"]);
                            string userno = cbRow.ToolTip;
                            int Deptno = Convert.ToInt32(ddladmoffer.SelectedValue.ToString());
                            int que_out1 = objPhd.InsPhdOfferLetteremailLog_crescent(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])), cbRow.ToolTip, Convert.ToInt32(ddladmoffer.SelectedValue.ToString()),
                                              Convert.ToInt32(ddlpgm.SelectedValue.ToString()), Convert.ToString(ViewState["ipAddress"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlCollege.SelectedValue.ToString()), 1, Config_SDate, STime);

                            //}
                            //else
                            //{
                            //string IP_ADDRESS = Convert.ToString(ViewState["ipAddress"]);
                            //string userno = cbRow.ToolTip;
                            //int Deptno = Convert.ToInt32(ddladmoffer.SelectedValue.ToString());
                            //int que_out1 = objPhd.InsPhdOfferLetteremailLog(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])), cbRow.ToolTip, Convert.ToInt32(ddladmoffer.SelectedValue.ToString()),
                            //                Convert.ToInt32(ddlpgm.SelectedValue.ToString()), Convert.ToString(ViewState["ipAddress"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlCollege.SelectedValue.ToString()), 1);
                            //}
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updoffer, "Failed To send email", this.Page);
                            if (Convert.ToInt32(Session["OrgId"]) == 2)
                            {
                                divTime.Visible = true;
                                divDate.Visible = true;
                            }
                            else
                            {
                                divTime.Visible = false;
                                divDate.Visible = false;
                            }
                        }
                    }
                }
            }
            CancelOffer();
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updoffer, "Please Select atleast one Student For Send Email", this);
                if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    divTime.Visible = false;
                    divDate.Visible = false;
                }
                else
                {
                    divTime.Visible = false;
                    divDate.Visible = false;
                }
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    #region Cresentmail
    static async Task<int> Execute(string Message, string toEmailId, string sub, string filename, string path, int OrgId)
    {
        int ret = 0;
        try
        {
            string Imgfile = string.Empty;
            string LogoPath = path;
            Byte[] Imgbytes = File.ReadAllBytes(LogoPath);
            Imgfile = Convert.ToBase64String(Imgbytes);
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new SendGrid.Helpers.Mail.EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
            var subject = sub;
            var to = new SendGrid.Helpers.Mail.EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
            var attachment = new SendGrid.Helpers.Mail.Attachment()
            {
                Content = Imgfile,
                Type = "image/png/pdf",
                Filename = filename,
                Disposition = "application/pdf",
                ContentId = "Logo"
            };
            attachments.Add(attachment);
            msg.AddAttachments(attachments);
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
            throw;
        }
        return ret;
    }

    static async Task<int> ExecuteOfferLetter(string Emailid, int admbatch, int deptno, int college_id, int phd_mode, int collegeId, string Userno, string collegecode, string Name, DateTime DATE, string Time)
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

            var to = new SendGrid.Helpers.Mail.EmailAddress(Emailid, "");

            #region comment
            //var to = Emailid;
            //foreach (var i in emails)
            //{
            //    to.Add(new EmailAddress(i));
            //}
            //var cc = new List<EmailAddress>();
            //foreach (var i in ccemails)
            //{
            //    cc.Add(new EmailAddress(i));
            //}
            #endregion

            MyHtmlString = "Dear " + Name + ",<br/><br/>";
            MyHtmlString += "Please Find Attached the Ph.D. Offer Letter<br/><br/><br/>";
            MyHtmlString += "This is a system generated report.<br/><br/><br/>";

            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
                Subject = "Ph.D. Offer Letter",
                HtmlContent = MyHtmlString,

            };
            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,PHDOfferLetter_CRESCENT.rpt", "@P_USERNO=" + Userno + ",@P_COLLEGE_CODE=" + collegecode + ",@P_ADMBATCH=" + admbatch + ",@P_DEPARTMENT_NO=" + deptno + ",@P_PHD_MODE=" + phd_mode + ",@P_COLLEGE_ID=" + collegeId + ",@P_DATE=" + DATE + ",@P_TIME =" + Time + "");

            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;


            var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
            msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {
                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content = Convert.ToBase64String(test),
                        Filename = "(Ph.D.OfferLetter).pdf",
                        Type = "application/pdf",
                        Disposition = "attachment"
                    }
                };

            #region  comment
            //byte[] fileBytes = ConvertDataSetToExcel(ds1);
            //var file = Convert.ToBase64String(fileBytes);
            //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //msg1.AddAttachment(attachment);
            // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
            //if (cc.Count > 0)
            //{
            //    msg1.AddCcs(cc);
            //}
            #endregion

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            Console.WriteLine(msg1.Serialize());
            var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
            // var response = await client.SendEmailAsync(msg1); ;
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
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/PHDOfferLetter_CRESCENT.rpt");
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
    #endregion


    #endregion

}