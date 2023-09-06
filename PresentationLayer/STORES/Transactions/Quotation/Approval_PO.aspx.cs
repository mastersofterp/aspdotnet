using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using IITMS.SQLServer.SQLDAL;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class STORES_Transactions_Quotation_Approval_PO : System.Web.UI.Page
{
    Common ObjComman = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Quotationa_PO_Approval_Controller objQPAC = new Str_Quotationa_PO_Approval_Controller();

    //Str_Configuration_Controller objConfigCon = new Str_Configuration_Controller();
    //Str_Configuration_Entity objConfigEnty = new Str_Configuration_Entity();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = ObjComman.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindPurchase();
                fillReportDropDown();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PA_Path.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PA_Path.aspx");
        }
    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {

        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    void fillReportDropDown()
    {
        ObjComman.FillDropDownList(ddlAppPO, "STORE_PURCHASE_ORDER_APPROVAL a inner join STORE_PORDER b on (a.PNO = b.PORDNO) ", "A.PNO", "b.REFNO", "A.UA_NO=" + Session["userno"] + "", "A.PNO desc");
    }

    void BindPurchase()
    {
        DataSet ds = objQPAC.GetPOItemDatail("PurchaseDetail", 0, int.Parse(Session["userno"].ToString()));
        rptPODetail.DataSource = ds;
        rptPODetail.DataBind();

        foreach (RepeaterItem item in rptPODetail.Items)
        {
            Label lblAppStatus = item.FindControl("lblAppStatus") as Label;
            if (lblAppStatus.Text == "REJECTED")
            {
                lblAppStatus.Style.Add("color", "Red");                
            }
            else
            {
                lblAppStatus.Style.Add("color", "Green");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlPOReport.Visible = false;
        pnlAdd.Visible = true;
        ddlAppPO.SelectedIndex = 0;
        pnlQuotation.Visible = true;
    }

    protected void btnReportPanel_Click(object sender, EventArgs e)
    {
        pnlPOReport.Visible = true;
        pnlAdd.Visible = false;
        pnlQuotation.Visible = false;
        pnlItemDetail.Visible = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        Str_Purchase_Order_Controller objstrPO = new Str_Purchase_Order_Controller();
        DataSet ds = objstrPO.GetSinglePONO(int.Parse(ddlAppPO.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() == "D")
            {
                ShowReport("PO_REPORT", "str_porder_dpurchase.rpt", int.Parse(ddlAppPO.SelectedValue));
            }
            else if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() == "P")
            {
                ShowReport("PO_REPORT", "str_porder_dpurchase.rpt", int.Parse(ddlAppPO.SelectedValue));
            }
            else
            {
                ShowReport("PO_REPORT", "Str_Purchase_order_Report_New.rpt", int.Parse(ddlAppPO.SelectedValue));
            }
        }
        else
        {
            Response.Write("<script>alert('Please Select PO.');</script>");
        }
    }



    private void ShowReport(string reportTitle, string rptFileName, int PoNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            if (rptFileName == "Str_Purchase_order_Report_New.rpt")
            {
                url += "&param=@P_PORDNO=" + PoNo + "," + "@username=" + Session["userfullname"].ToString() + "";
            }
            else
            {
                url += "&param=@P_PORDNO=" + PoNo + "," + "@username=" + Session["userfullname"].ToString() + "";
            }
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Transactions_Quotation_Approval_PO.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            int chk = 0;
            DataSet MS = new DataSet();
            foreach (RepeaterItem item in rptPODetail.Items)
            {
                string Status = string.Empty;
                CheckBox chkQuotation = item.FindControl("chkQuotation") as CheckBox;
                HiddenField hdPNO = item.FindControl("hdPNO") as HiddenField;
                TextBox txtRemarks = item.FindControl("txtRemarks") as TextBox;
                int PNO = int.Parse(hdPNO.Value);
                Session["PONO"] = PNO;

                if (ddlSelect.SelectedItem.Text == "Approve")
                {
                    Status = "A";
                }
                else if (ddlSelect.SelectedItem.Text == "Approved and Forward")
                {
                    Status = "F";
                }
                else
                {
                    Status = "R";
                }

                if (chkQuotation.Checked == true)
                {
                    ret = int.Parse(objQPAC.UpdatePOApproval(Status, PNO, int.Parse(Session["userno"].ToString()), txtRemarks.Text).ToString());
                    chk = 1;
                }
            }

            if (ret == 2)
            {
                if (ddlSelect.SelectedValue == "A")
                {
                    //GetConfiguration();
                    //if (objConfigEnty.SendSms == true)
                    //{
                    //    MS = objQPAC.POPPATHEmailSMSINFO(int.Parse(Session["PONO"].ToString()));

                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                    //            SendSMSMsg(objConfigEnty.SMSURL, objConfigEnty.SMSUserName, objConfigEnty.SMSPassword, MS.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear Sir,  " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " is waiting for your Approval,Kindly Log In and Approve or Forward the same.");
                    //    }

                    //    MS = objQPAC.POEmailSMSINFO(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                    //        {
                    //            SendSMSMsg(objConfigEnty.SMSURL, objConfigEnty.SMSUserName, objConfigEnty.SMSPassword, MS.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear Sir, Your " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " Purchase Order is Approved");
                    //        }
                    //    }
                    //}

                    //if (objConfigEnty.SendEmail == true)
                    //{
                    //    MS = objQPAC.POPPATHEmailSMSINFO(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
                    //            SendEmailMsg(MS.Tables[0].Rows[0]["UA_EMAIL"].ToString(), "Mail About Quotation Approval", "This Purchase Order " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " " + Environment.NewLine + " is waiting for your Approval,Kindly Log In and Approve or Forward the same");
                    //    }
                    //    //sending mail for Requisition User

                    //    MS = objQPAC.QUOTATIONEmailSMSINFO(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
                    //            SendEmailMsg(MS.Tables[0].Rows[0]["UA_EMAIL"].ToString(), "Mail About Purchase Approval Status", "Your " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " " + Environment.NewLine + " Purchase Order is Approved");
                    //    }
                    //}

                    Response.Write("<script>alert('PO Approved Successfully');</script>");
                }
                else if (ddlSelect.SelectedValue == "F")
                {
                    //GetConfiguration();
                    //if (objConfigEnty.SendSms == true)
                    //{
                    //    MS = objQPAC.POPPATHEmailSMSINFO(int.Parse(Session["PONO"].ToString()));

                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                    //            SendSMSMsg(objConfigEnty.SMSURL, objConfigEnty.SMSUserName, objConfigEnty.SMSPassword, MS.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear Sir, This Purchase Order " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " is waiting for your Approval,Kindly Log In and Approve the same.");
                    //    }

                    //    MS = objQPAC.POEmailSMSINFO(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                    //        {
                    //            SendSMSMsg(objConfigEnty.SMSURL, objConfigEnty.SMSUserName, objConfigEnty.SMSPassword, MS.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear Sir, Your " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " Purchase Order is Approved");
                    //        }
                    //    }
                    //}

                    //if (objConfigEnty.SendEmail == true)
                    //{
                    //    MS = objQPAC.POPPATHEmailSMSINFO(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
                    //            SendEmailMsg(MS.Tables[0].Rows[0]["UA_EMAIL"].ToString(), "Mail About Purchase Approval", "This Purchase Order " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " " + Environment.NewLine + " is waiting for your Approval,Kindly Log In and Approve or Forward the same.");
                    //    }
                    //    //sending mail for Requisition User

                    //    MS = objQPAC.POEmailSMSINFO(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
                    //            SendEmailMsg(MS.Tables[0].Rows[0]["UA_EMAIL"].ToString(), "Mail About Purchase Approval Status", "Your " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " " + Environment.NewLine + " Purchase Order is Approved");
                    //    }
                    //}
                    Response.Write("<script>alert('PO Forwarded Successfully');</script>");
                }
                else
                {
                    //GetConfiguration();
                    //if (objConfigEnty.SendSms == true)
                    //{
                    //    MS = objQPAC.POEmailSMSINFOREJECTED(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_EMAIL"].ToString() != "")
                    //        {
                    //            SendEmailMsg(MS.Tables[0].Rows[0]["UA_EMAIL"].ToString(), "Mail About Purchase Approval Status", Session["userfullname"] + " is Rejected Your " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " " + Environment.NewLine + " Purchase Order.");
                    //        }
                    //    }
                    //}

                    //if (objConfigEnty.SendEmail == true)
                    //{
                    //    MS = objQPAC.POEmailSMSINFOREJECTED(int.Parse(Session["PONO"].ToString()));
                    //    if (MS.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (MS.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                    //        {
                    //            SendSMSMsg(objConfigEnty.SMSURL, objConfigEnty.SMSUserName, objConfigEnty.SMSPassword, MS.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear Sir, " + Session["userfullname"] + " is Rejected Your " + MS.Tables[0].Rows[0]["REFNO"].ToString() + " Purchase Order.");
                    //        }
                    //    }
                    //}
                    Response.Write("<script>alert('PO Rejected Successfully');</script>");
                }

                BindPurchase();
            }
            if (chk == 0)
            {
                if (ddlSelect.SelectedItem.Text == "Approve")
                {
                    Response.Write("<script>alert('Please Select PO for Approval.');</script>");
                }
                else 
                {
                    Response.Write("<script>alert('Please Select PO for Rejection.');</script>");
                }

                
            }
        }
        catch (Exception ex)
        {
            objUaimsCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_PO.btnSave_Click() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSelect.SelectedValue = "A";
        BindPurchase();
    }

    // code for Email & SMS 

    //private void GetConfiguration()
    //{
    //    DataSet ds = new DataSet();
    //    ds = objConfigCon.GetConfigurationInfo();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        objConfigEnty.SMSURL = ds.Tables[0].Rows[0]["SMSURL"].ToString();
    //        objConfigEnty.SMSUserName = ds.Tables[0].Rows[0]["SMSUserName"].ToString();
    //        objConfigEnty.SMSPassword = ds.Tables[0].Rows[0]["SMSPassword"].ToString();
    //        if (ds.Tables[0].Rows[0]["SendSMS_Purchase"].ToString() == "False")
    //            objConfigEnty.SendSms = false;
    //        else
    //            objConfigEnty.SendSms = true;

    //        objConfigEnty.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
    //        objConfigEnty.EmailPassword = ds.Tables[0].Rows[0]["EmailPassword"].ToString();

    //        if (ds.Tables[0].Rows[0]["SendEmail_Purchase"].ToString() == "False")
    //            objConfigEnty.SendEmail = false;
    //        else
    //            objConfigEnty.SendEmail = true;

    //    }
    //    else
    //    {
    //        objConfigEnty.SendSms = false;
    //        objConfigEnty.SendEmail = false;
    //    }
    //}

    //public void SendSMSMsg(string url, string uid, string pass, string mobno, string message)
    //{
    //    try
    //    {
    //        WebRequest request = HttpWebRequest.Create("" + url + "?ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "");
    //        WebResponse response = request.GetResponse();
    //        StreamReader reader = new StreamReader(response.GetResponseStream());
    //        string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
    //        //return urlText;
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //        // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Error);
    //    }
    //}

    //public void SendEmailMsg(string toEmailId, string Sub, string body)
    //{

    //    try
    //    {
    //        string msg = string.Empty;
    //        System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
    //        mailMessage.IsBodyHtml = true;
    //        mailMessage.Subject = Sub;

    //        string MemberEmailId = string.Empty;
    //        mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(objConfigEnty.EmailId));
    //        mailMessage.To.Add(toEmailId);

    //        var MailBody = new StringBuilder();
    //        MailBody.AppendFormat("Dear Sir, {0}\n", " ");
    //        MailBody.AppendLine(@"<br /> " + body);
    //        mailMessage.Body = MailBody.ToString();

    //        mailMessage.IsBodyHtml = true;
    //        SmtpClient smt = new SmtpClient("smtp.gmail.com");

    //        smt.UseDefaultCredentials = false;
    //        smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(objConfigEnty.EmailId), HttpUtility.HtmlEncode(objConfigEnty.EmailPassword));
    //        smt.Port = 587;
    //        smt.EnableSsl = true;

    //        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
    //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
    //        System.Security.Cryptography.X509Certificates.X509Chain chain,
    //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
    //        {
    //            return true;
    //        };

    //        smt.Send(mailMessage);
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //}
    protected void btnPPath_Click(object sender, EventArgs e)
    {
        LinkButton btnPPath = sender as LinkButton;
        int PNO = int.Parse(btnPPath.CommandArgument);
        DataSet ds = objQPAC.GetPOItemDatail("PassingPath", PNO, 0);
        pnlItemDetail.Visible = true;
        pnlAdd.Visible = true;
        lvitemPurchase.DataSource = ds;
        lvitemPurchase.DataBind();
        this.MdlApproval.Show();
    }
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        LinkButton btnDetail = sender as LinkButton;
        int PNO = int.Parse(btnDetail.CommandArgument);
        Str_Purchase_Order_Controller objstrPO = new Str_Purchase_Order_Controller();
        DataSet ds = objstrPO.GetSinglePONO(PNO);
        if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() == "D")
        {
            ShowReport("PO_REPORT", "str_porder_dpurchase.rpt", PNO);
        }
        else if (ds.Tables[0].Rows[0]["ISTYPE"].ToString() == "P")
        {
            ShowReport("PO_REPORT", "str_porder_dpurchase.rpt", PNO);
        }
        else
        {
            ShowReport("PO_REPORT", "Str_Purchase_order_Report_New.rpt", PNO);
        }
        //DataSet ds = objQPAC.GetPOItemDatail("PurchaseWiseItem", PNO, 0);
        //pnlItemDetail.Visible = true;
        //lvitemPurchase.DataSource = ds;
        //lvitemPurchase.DataBind();
        //MdlApproval.Show();
    }
}