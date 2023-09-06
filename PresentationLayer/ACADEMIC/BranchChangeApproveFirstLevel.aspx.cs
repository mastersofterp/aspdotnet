using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using SendGrid;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
public partial class ACADEMIC_BranchChangeApproveFirstLevel : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BranchController objBrn = new BranchController();

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
            
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {                
                //CheckPageAuthorization();                
                BindListView();
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
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
                Response.Redirect("~/notauthorized.aspx?page=BranchChangeApproveFirstLevel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchChangeApproveFirstLevel.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            int admbatch = ddlAdmBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmBatch.SelectedValue) : 0;
            DataSet ds = new DataSet();
           
           // ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRCHANGE A ON A.IDNO = S.IDNO INNER JOIN ACD_BRANCH B ON A.OLDBRANCHNO = B.BRANCHNO INNER JOIN ACD_BRANCH C ON A.NEWBRANCHNO = C.BRANCHNO", "BRCHNO", "S.IDNO,S.REGNO,STUDNAME,B.LONGNAME OLD_BRANCH,C.LONGNAME NEW_BRANCH,A.OLDBRANCHNO,A.NEWBRANCHNO", "ISNULL(IS_FIRST_LEVEL_APPROVE,0) = 0", "");
           // ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRCHANGE A ON A.IDNO = S.IDNO INNER JOIN ACD_BRANCH B ON A.OLDBRANCHNO = B.BRANCHNO INNER JOIN ACD_BRANCH C ON A.NEWBRANCHNO = C.BRANCHNO", "BRCHNO", "S.IDNO,S.REGNO,STUDNAME,B.LONGNAME OLD_BRANCH,C.LONGNAME NEW_BRANCH,A.OLDBRANCHNO,A.NEWBRANCHNO,A.REMARK REQUEST_REMARK,FILE_NAME,PATH PREVIEW_PATH", "ISNULL(IS_FIRST_LEVEL_APPROVE,0) = 0 AND (S.ADMBATCH=" + admbatch + " OR " + admbatch + "=0)", "");
            ds = objBrn.GetStudentsforBranchangeApproval(admbatch);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlBranchChange.Visible = true;
                lvBranchChange.DataSource = ds;
                lvBranchChange.DataBind();
                
            }
            else
            {
                pnlBranchChange.Visible = false;
                lvBranchChange.DataSource = null;
                lvBranchChange.DataBind();
                objCommon.DisplayMessage(updSession, "No Data Found.", this.Page);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox cbRow = new CheckBox();
            Label lblIDNO = new Label();
            TextBox txtRemark = new TextBox();
            Label lblOldBranch = new Label();
            Label lblNewBranch = new Label();
            HiddenField hdnCollege = new HiddenField();
            int count = 0;
            foreach (ListViewDataItem dataitem in lvBranchChange.Items)
            {
                cbRow = dataitem.FindControl("chkApprove") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    lblIDNO = dataitem.FindControl("lblIDNO") as Label;
                    txtRemark = dataitem.FindControl("txtFirstLevelRemark") as TextBox;
                    hdnCollege = dataitem.FindControl("hdnCollege") as HiddenField;
                    lblOldBranch = dataitem.FindControl("lblOldBranch") as Label;
                    lblNewBranch = dataitem.FindControl("lblNewBranch") as Label;
                    if (txtRemark.Text == "")
                    {
                        objCommon.DisplayMessage(updSession, "Please Enter Remark for Selected Student.", this.Page);
                        return;
                    }
                    objBrn.Branch_Change_First_Level(Convert.ToInt32(lblIDNO.Text), Convert.ToInt32(Session["userno"]), txtRemark.Text);                    
                    count++;
                    TransferToEmail(lblOldBranch.ToolTip, lblIDNO.ToolTip,lblNewBranch.ToolTip, lblOldBranch.Text, lblNewBranch.Text, txtRemark.Text,hdnCollege.Value);
                }
            }
            if (count < 1)
            {
                objCommon.DisplayMessage(updSession, "Please Select Atleast one record.", this.Page);
                return;
            }
            if (count > 0)
            {
                objCommon.DisplayMessage(updSession, "Programme/Branch Change Approved Successfully.", this.Page);
                BindListView();
            }
        }
        catch (Exception ex)
        {
 
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("SessionMaster", "rptBranchChangeFirstLevel.rpt");
        ExportinExcelBranchChange();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
             
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ExportinExcelBranchChange()
    {
        string attachment = "attachment; filename=" + "BranchChangeExcel.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = objBrn.GetBranchChangeFirstLevel();

        DataGrid dg = new DataGrid();
        
        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            BindListView();
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkView = (LinkButton)(sender);
        //string path = lnkView.CommandArgument;
        string path = lnkView.ToolTip;
        iframeView.Attributes.Add("src", path);

        mpeViewDocument.Show();
    }
    protected void lvBranchChange_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataitem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataitem.DataItem).Row;
            string filename=((LinkButton)e.Item.FindControl("lnkView")).CommandName;
            if (filename == string.Empty)
            {
                ((Label)e.Item.FindControl("lblView")).Text = "Preview Not Available";
                ((Label)e.Item.FindControl("lblView")).ForeColor = System.Drawing.Color.Red;
                ((LinkButton)e.Item.FindControl("lnkView")).Visible = false;
            }
            else
            {
                ((Label)e.Item.FindControl("lblView")).Visible = false;
                ((LinkButton)e.Item.FindControl("lnkView")).Visible = true;
            }
        }
    }

    public void TransferToEmail(string studname, string Regno,string degree, string oldbranch, string newbranch, string remark,string College)
    {
        try
        {
            int ret = 0;
            //  string Session = ddlSession.SelectedItem.Text;
            // string sem = ddlSem.SelectedItem.Text;//kare.dileep@mastersofterp.co.in
            string useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=3");
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();



                msg.From = new MailAddress(fromAddress, "ABBS - Programme/Branch Change");
                msg.To.Add(new MailAddress(useremail));



                msg.Subject = "Regarding Programme/Branch Change Approval";
                //FOR MANISH : ERR: AT HTML TAGS :
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                const string EmailTemplate = "<html><body>" +
                                            "<div align=\"center\">" +
                                            "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                             "<tr>" +
                                             "<td>" + "</tr>" +
                                             "<tr>" +
                                            "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                            "</tr>" +
                                            "<tr>" +
                                            "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b><br/></td>" +
                                            "</tr>" +
                                            "</table>" +
                                            "</div>" +
                                            "</body></html>";
                StringBuilder mailBody = new StringBuilder();
                //  mailBody.AppendFormat("<h1>Greetings !!</h1>");
                mailBody.AppendFormat("Dear Sir/Madam <b>" + "" + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Below Student has opted for a Program change that required your approval with Comments.");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>Student Details </b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Reg. No. : </b> " + Regno + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Name : </b>" + studname + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicantion Date :</b> " + DateTime.Now.ToString("dd/MM/yyyy") + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> College Name  : </b>" + College + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Department  : </b>" + degree + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Currenct Programme : </b>" + oldbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> New Programme  : </b>" + newbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>New College Name  : </b>" + College + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                //  mailBody.AppendFormat("<b>Your new Login Password is </b>");
                mailBody.AppendFormat("<b>Comments :" + remark + " </b>");
                mailBody.AppendFormat("<br />");
                string Mailbody = mailBody.ToString();
                string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                msg.IsBodyHtml = true;
                msg.Body = nMailbody;


                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);


                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };




                smtp.Send(msg);

                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                {
                    ret = 1;
                    //    objCommon.DisplayMessage(updSession, "Email Sent Successfully.", this.Page);
                    //Storing the details of sent email
                }

            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}