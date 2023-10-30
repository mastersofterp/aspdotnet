using BusinessLogicLayer.BusinessLogic.PostAdmission;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS;
using IITMS.UAIMS;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
//using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMP_ADMOrderNote : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ADMPAdmissionOrderNoteController objOrder = new ADMPAdmissionOrderNoteController();

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
            // Check User Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["colcode"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
                ddlAdmissionBatch.SelectedIndex = 0;

                objCommon.FillDropDownList(ddlAdmBatchNote, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
                ddlAdmissionBatch.SelectedIndex = 0;
           
                // this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID");
                //objCommon.FillDropDownList(ddlCode, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "", "BRANCH_CODE");

            }
        }
    }

    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }

            ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();

            lstProgram.Items.Clear();   
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Exam_Hall_Ticket.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objOrder.GetBranch(Degree);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }


    //protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataSet ds = null;
       
    //        int DegreeId = Convert.ToInt32(ddlDegree.SelectedValue);
    //        int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
    //        string branchno = string.Empty;

    //        foreach (ListItem items in lstProgram.Items)
    //        {
    //            if (items.Selected == true)
    //            {
    //                branchno += items.Value + ',';
    //                //activitynames += items.Text + ',';
    //            }
    //        }

    //        //branchno.TrimEnd(',').TrimEnd();
    //        branchno = branchno.TrimEnd(',').Trim();
    //        //string Branch=ddlProgramType.SelectedValue;
    //        ds = objOrder.GetSChedule(DegreeId, branchno, ADMBATCH);

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAdmOrder, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAdmOrder, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upAdmOrder, "Please Select Branch/Program.", this.Page);
                return;
            }
       

            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);

            string branchno = string.Empty;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = objOrder.GetStudentForOrderNote(ADMBATCH, ProgramType, DegreeNo, branchno);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllistView.Visible = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                //lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                //lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lvSchedule.DataBind();
                pnlCount.Visible = true;
                
                //txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString("0000");
                //txtGeneratedAdmitCard.Text = ds.Tables[0].AsEnumerable().Where(c => c["STATUS"].ToString() == "Generated").ToList().Count.ToString();
                //txtNotGeneratedAdmitCard.Text = ds.Tables[0].AsEnumerable().Where(c => c["STATUS"].ToString() == "Not Generated").ToList().Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(upAdmOrder, "No Record Found.", this.Page);

                pnllistView.Visible = false;
                lvSchedule.Visible = false;
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
                pnlCount.Visible = false;

            }
           
        }
        catch
        {
            throw;
        }
    }


    protected void btnOrderGen_Click(object sender, EventArgs e)
    {

        try
        {
            int UserNo = 0;
            int UANO = 0;
            int UpdtCount = 0;
            foreach (ListViewDataItem lvItem in lvSchedule.Items)
            {
                Label lblStudentName = lvItem.FindControl("lblStudentName") as Label;
                Label lblApplicationNo = lvItem.FindControl("lblApplicationNo") as Label;
                Label lblMail = lvItem.FindControl("lblMail") as Label;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;

                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;

                UserNo = Convert.ToInt16(hdnUserNo.Value);
                UANO = Convert.ToInt32(Session["userno"]);

                if (chkBox.Checked == true)
                {
                    UpdtCount++;
                    DataSet dsconfig = null;
                    dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

                    if (dsconfig != null)
                    {

                        string MsgBody = "";

                        MsgBody += " <b>Greetings !!</b>";
                        MsgBody += "<br><br> Dear &nbsp;" + lblStudentName.Text + "";
                        MsgBody += "<br><br> Application Id :-  &nbsp;" + lblApplicationNo.Text + "";
                        MsgBody += "<br><br>Your Admission Order is generated for a Bachelor of Technology, now you can download your Admission Order, Please find the attachment to download your admission order.";
                        MsgBody += "<br><br><br><b>With Best Wishes</b>";
                        MsgBody += "<br><b>BSA Crescent University</b>";
                        MsgBody += "<br><br><br><b>Note : This is a system generated mail. Kindly do not reply to this e-mail. Emails send to this email ID will not be attended.</b>";

                        string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                        MailMessage msg = new MailMessage();
                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        // fromPassword = Common.DecryptPassword(fromPassword);


                        msg.From = new System.Net.Mail.MailAddress(fromAddress, "Crescent");
                        msg.To.Add(new System.Net.Mail.MailAddress(lblMail.Text));
                        //msg.To.Add(new System.Net.Mail.MailAddress("abhijitnaik1604@gmail.com"));
                        msg.Subject = "Admission Order";


                        //MemoryStream ms = new MemoryStream(File.ReadAllBytes(@"C:\Users\Admin\Desktop\CommonReport.pdf"));

                        //msg.Attachments.Add(new System.Net.Mail.Attachment(ms, "CommonReport.pdf"));

                        MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,rptAdmissionOrder.rpt", "@P_USERNO=" + hdnUserNo.Value + "");
                        //MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceipt.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + hfddcr.Value + ",@P_IDNO=" + Convert.ToInt32(chk.ToolTip) + "");
                        var bytesRpt = oAttachment1.ToArray();
                        var fileRpt = Convert.ToBase64String(bytesRpt);
                        byte[] test = (byte[])bytesRpt;
                        //  msg.AddAttachment("Offerletter.pdf", test);     

                        System.Net.Mail.Attachment attachment;

                        attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "AdmissionOrder.pdf");
                        msg.Attachments.Add(attachment);



                        msg.Body = MsgBody;
                        msg.IsBodyHtml = true;
                        smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                        smtp.EnableSsl = true;
                        smtp.Port = 587; // 587
                        smtp.Host = "smtp.gmail.com";

                        ServicePointManager.ServerCertificateValidationCallback =
                        delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        smtp.Send(msg);
                        if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                        {
                            CustomStatus cs = (CustomStatus)objOrder.UpdateMailStatus(UserNo, UANO);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {

                                objCommon.DisplayMessage(upAdmOrder, "Mail Send Successfully", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(upAdmOrder, "Mail Already Sent", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(upAdmOrder, "Something Is wrong", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(upAdmOrder, "Configuration Is Not Available", this.Page);
                    }
                }
            }
        }


        //try
        //{
        //    int UserNo = 0;
        //    int UANO = 0;
        //    int UpdtCount = 0;
        //    int ChkCnt = 0;
        //    foreach (ListViewDataItem lvItem in lvSchedule.Items)
        //    {
        //        Label lblStudentName = lvItem.FindControl("lblStudentName") as Label;
        //        Label lblApplicationNo = lvItem.FindControl("lblApplicationNo") as Label;
        //        Label lblMail = lvItem.FindControl("lblMail") as Label;
        //        HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;

        //        CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;

        //        UserNo = Convert.ToInt16(hdnUserNo.Value);
        //        UANO = Convert.ToInt32(Session["userno"]);

        //        if (chkBox.Checked == true)
        //        {
        //            UpdtCount++;
        //            DataSet dsconfig = null;
        //            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

        //            if (dsconfig != null)
        //            {

        //                string MsgBody = "";

        //                MsgBody += " <b>Greetings !!</b>";
        //                MsgBody += "<br><br> Dear &nbsp;" + lblStudentName.Text + "";
        //                MsgBody += "<br><br> Application Id :-  &nbsp;" + lblApplicationNo.Text + "";
        //                MsgBody += "<br><br>Your Admission Order is generated for a Bachelor of Technology, now you can download your Admission Order, Please find the attachment to download your admission order.";
        //                MsgBody += "<br><br><br><b>With Best Wishes</b>";
        //                MsgBody += "<br><b>BSA Crescent University</b>";
        //                MsgBody += "<br><br><br><b>Note : This is a system generated mail. Kindly do not reply to this e-mail. Emails send to this email ID will not be attended.</b>";

        //                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
        //                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

        //                MailMessage msg = new MailMessage();
        //                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //                // fromPassword = Common.DecryptPassword(fromPassword);


        //                msg.From = new System.Net.Mail.MailAddress(fromAddress, "Crescent");
        //                //msg.To.Add(new System.Net.Mail.MailAddress(lblMail.Text));
        //                msg.To.Add(new System.Net.Mail.MailAddress("abhijitnaik1604@gmail.com"));
        //                msg.Subject = "Admission Order";


        //                MemoryStream oAttachment1 = new MemoryStream(File.ReadAllBytes(@"C:\Users\Admin\Desktop\CommonReport.pdf"));

        //                //msg.Attachments.Add(new System.Net.Mail.Attachment(ms, "CommonReport.pdf"));

        //              //  MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,rptAdmissionOrder.rpt", "@P_USERNO=" + hdnUserNo.Value + "");
        //                //MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceipt.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + hfddcr.Value + ",@P_IDNO=" + Convert.ToInt32(chk.ToolTip) + "");
        //                var bytesRpt = oAttachment1.ToArray();
        //                var fileRpt = Convert.ToBase64String(bytesRpt);
        //                byte[] test = (byte[])bytesRpt;
        //                //  msg.AddAttachment("Offerletter.pdf", test);     

        //                System.Net.Mail.Attachment attachment;

        //                attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "AdmissionOrder.pdf");
        //                msg.Attachments.Add(attachment);



        //                msg.Body = MsgBody;
        //                msg.IsBodyHtml = true;
        //                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
        //                smtp.EnableSsl = true;
        //                smtp.Port = 587; // 587
        //                smtp.Host = "smtp.gmail.com";

        //                ServicePointManager.ServerCertificateValidationCallback =
        //                delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
        //                X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //                {
        //                    return true;
        //                };

        //                smtp.Send(msg);
        //                if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
        //                {
        //                    ChkCnt++;
        //                    CustomStatus cs = (CustomStatus)objOrder.UpdateMailStatus(UserNo, UANO);
        //                    if (cs.Equals(CustomStatus.RecordSaved))
        //                    {
        //                        objCommon.DisplayMessage(upAdmOrder, "Mail Send Successfully", this.Page);
        //                    }
        //                    else
        //                    {
        //                        objCommon.DisplayMessage(upAdmOrder, "Mail Already Sent", this.Page);
        //                    }
        //                }
        //                else
        //                {
        //                    objCommon.DisplayMessage(upAdmOrder, "Something Is wrong", this.Page);
        //                }
        //            }
        //            else
        //            {
        //                objCommon.DisplayMessage(upAdmOrder, "Configuration Is Not Available", this.Page);
        //            }
        //        }
        //        if (UpdtCount > 0)
        //        {
        //            objCommon.DisplayMessage(upAdmOrder, "Mail Send Successfully", this.Page);
        //        }            


        catch (Exception ex)
        {
            throw;
        }
    }

        //DataSet dsconfig = null;
        //dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


        //MailMessage msg = new MailMessage();
        //msg.To.Add(new System.Net.Mail.MailAddress("sangamnaik1604@gmail.com"));
        //msg.From = new System.Net.Mail.MailAddress("abhijitnaik1604@gmail.com");
        //msg.Subject = "";
        //StringBuilder sb = new StringBuilder();
        //msg.Body = "Test MSG";

        //MemoryStream oAttachment1 = ShowGeneralExportReportForMail("Reports,Academic,rptAdmissionOrder.rpt", "@P_ADMBATCH=" + 10 + ",@P_UGPGOT=" + 1 + ",@P_DEGREENO=" + 7 + ",@P_BRANCHNO=" + 15 + "");
        ////MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,FeeCollectionReceipt.rpt", "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + hfddcr.Value + ",@P_IDNO=" + Convert.ToInt32(chk.ToolTip) + "");
        //var bytesRpt = oAttachment1.ToArray();
        //var fileRpt = Convert.ToBase64String(bytesRpt);
        //byte[] test = (byte[])bytesRpt;
        ////  msg.AddAttachment("Offerletter.pdf", test);     

        //System.Net.Mail.Attachment attachment;

        //attachment = new System.Net.Mail.Attachment(new MemoryStream(test), "FeeReceipt.pdf");
        //msg.Attachments.Add(attachment);


        ////sb.AppendLine(message);
        //msg.BodyEncoding = Encoding.UTF8;

        ////msg.Body = Convert.ToString(sb);
        //msg.IsBodyHtml = true;

        //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //client.UseDefaultCredentials = false;

        //client.Credentials = new System.Net.NetworkCredential("abhijitnaik1604@gmail.com", "BigBull@13");

        //client.Port = 587; // You can use Port 25 if 587 is blocked (mine is)
        //client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
        //client.DeliveryMethod = SmtpDeliveryMethod.Network;
        ////client.TargetName = "STARTTLS/smtp.office365.com";
        //client.EnableSsl = true;
        //try
        //{
        //    client.Send(msg);
        //    //lblText.Text = "Message Sent Succesfully";
        //}
        //catch (Exception ex)
        //{
        //    //lblText.Text = ex.ToString();
        //}
      
   
    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }

    static private MemoryStream ShowGeneralExportReportForMail(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptAdmissionOrder.rpt");
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

    static private MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptAdmissionOrder.rpt");
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

    /// <summary>
    /// Admisssion Note
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void ddlProgramTypeNote_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramTypeNote.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
                objCommon.FillDropDownList(ddlDegreeNote, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramTypeNote.SelectedValue, "D.DEGREENO");
            }

            ddlDegreeNote.Items.Insert(0, new ListItem("Please Select Degree", "0"));
            ddlDegreeNote.SelectedIndex = 0;
            ddlDegreeNote.Focus();

            lstProgramNote.Items.Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_POSTADMISSION_ADMP_ADMOrderNote.ddlProgramTypeNote_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegreeNote_SelectedIndexChanged(object sender, 
        EventArgs e)
    {
        int Degree = Convert.ToInt16(ddlDegreeNote.SelectedValue);
        MultipleCollegeBindNote(Degree);
    }

    private void MultipleCollegeBindNote(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = objOrder.GetBranch(Degree);

            lstProgramNote.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgramNote.DataSource = ds;
                lstProgramNote.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgramNote.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgramNote.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnShowNote_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlProgramTypeNote.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAdmOrder, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegreeNote.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAdmOrder, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgramNote.SelectedValue == "")
            {
                objCommon.DisplayMessage(upAdmOrder, "Please Select Branch/Program.", this.Page);
                return;
            }


            int ADMBATCH = Convert.ToInt32(ddlAdmBatchNote.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramTypeNote.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegreeNote.SelectedValue);

            string branchno = string.Empty;

            foreach (ListItem items in lstProgramNote.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = objOrder.GetStudentForOrderNote(ADMBATCH, ProgramType, DegreeNo, branchno);

            lvScheduleNote.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllistViewNote.Visible = true;
                lvScheduleNote.Visible = true;
                lvScheduleNote.DataSource = ds;
                //lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                //lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lvScheduleNote.DataBind();
                //pnlCount.Visible = true;

                //txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString("0000");
                //txtGeneratedAdmitCard.Text = ds.Tables[0].AsEnumerable().Where(c => c["STATUS"].ToString() == "Generated").ToList().Count.ToString();
                //txtNotGeneratedAdmitCard.Text = ds.Tables[0].AsEnumerable().Where(c => c["STATUS"].ToString() == "Not Generated").ToList().Count.ToString();
            }
            else
            {

                objCommon.DisplayMessage(upAdmOrder, "No Record Found.", this.Page);
                pnllistViewNote.Visible = false;
                lvScheduleNote.Visible = false;
                lvScheduleNote.DataSource = null;          
                lvScheduleNote.DataBind();
            }

        }
        catch
        {
            throw;
        }
    }

    protected void btnAdmNote_Click(object sender, EventArgs e)
    {

        try
        {
            string UserNo = string.Empty;

            foreach (ListViewDataItem lvItem in lvScheduleNote.Items)
            {

                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
                if (chkBox.Checked == true)
                {
                    UserNo += hdnUserNo.Value + '$';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            UserNo = UserNo.TrimEnd('$').Trim();
            if (UserNo != "")
            {
                ShowGeneralReport("Admission_Note", "rptAdmissionNote.rpt", UserNo);
            }
            else
            {

                objCommon.DisplayMessage(upAdmOrder, "Please Select At Least One Student.", this.Page);
                return;
            }

        }
        //ShowReportNew("Student Admit Card Report", "rptAdmitCard.rpt", branchno);
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_POSTADMISSION_ADMP_ADMOrderNote.btnAdmNote_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   


    private void ShowGeneralReport(string reportTitle, string rptFileName, string UserNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNO=" + UserNo;
            //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_EXAMSCHEDULE=" +ddlExamSchedule.SelectedItem.Text + ",@P_BRANCHNO=" + branchno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@P_USERNO=" + UserNo + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_EXAMSCHEDULE=" + 1+ ",@P_BRANCHNO=" + 16 + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + ",@P_UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + branchno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_USERNO=" + UserNo;         
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}