using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

using mastersofterp_MAKAUAT;
using System.Web.UI;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
using EASendMail;
using BusinessLogicLayer.BusinessLogic;



public partial class ACADEMIC_AdmissionConfirmation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmissionController objOnlline = new OnlineAdmissionController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

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
                //CheckPageAuthorization();
            }
            ViewState["studNo"] = null;
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

            string test = objCommon.LookUp("ACD_ADMBATCH", "MAX(BATCHNAME)", "");



            string[] name = test.Split(new char[] { ' ' }, 2);

            string firstname = name[0];


        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch
        {
            throw;
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objOnlline.GetStudentListForAdmApproval(Convert.ToInt32(ddlAdmbatch.SelectedValue),Convert.ToInt32(ddlClg.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentConf.DataSource = ds;
                lvStudentConf.DataBind();
                if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
                {
                    btnSend.Visible = true;

                }


                if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    btnsendoffer.Visible = true;
                    btnsendmailjecrc.Visible = true;
                   ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#idStatus').hide();$('td:nth-child(8)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#idStatus').hide();$('td:nth-child(8)').hide();});", true); //For Hide Status Column for JECRC
                }
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentConf);//Set label 
            }
            else
            {
                objCommon.DisplayUserMessage(updHouse, "Record Not Found.", this.Page);
                lvStudentConf.DataSource = null;
                lvStudentConf.DataBind();
                btnSend.Visible = false;
                btnsendoffer.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSend.Visible = false;
        btnsendoffer.Visible = false;
        ddlAdmbatch.SelectedIndex = 0;
        lvStudentConf.DataSource = null;
        ddlClg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        lvStudentConf.DataBind();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            SENDMAIL();
        }
        catch
        {
            throw;
        }
    }

    protected void SENDMAIL()
    {
        string Subject = "CPUK | Provisional Admission Letter";

        foreach (ListViewDataItem item in lvStudentConf.Items)
        {
            int status = 0;
            try
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblEmail = item.FindControl("lblEmail") as Label;
                Label RRNO = item.FindControl("lblRrno") as Label;
                int idno = Convert.ToInt32(chk.ToolTip);

                if (chk.Checked == true)
                {
                    if (lblEmail.Text != string.Empty)
                    {
                        string university = objCommon.LookUp("REFF", "COLLEGENAME", "");
                        DataSet dsStud = objOnlline.GetStudentInfo(idno);
                        string college_name = dsStud.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        string student_Name = dsStud.Tables[0].Rows[0]["STUDNAME"].ToString();
                        string stud_Mobile = dsStud.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        string father_name = dsStud.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        string branch_name = dsStud.Tables[0].Rows[0]["LONGNAME"].ToString();
                        string house_Name = dsStud.Tables[0].Rows[0]["HOUSE_NAME"].ToString();
                        string dress_Colour = dsStud.Tables[0].Rows[0]["COLOUR"].ToString();
                        string programme = dsStud.Tables[0].Rows[0]["PROGRAMME"].ToString();
                        string EmailId = dsStud.Tables[0].Rows[0]["EMAILID"].ToString();
                        string programme_colg = dsStud.Tables[0].Rows[0]["PROGRAMME_COLLEGE"].ToString();
                        string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + RRNO.Text + "'");
                        string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);

                        string body = MailBody(lblEmail.Text, RRNO.Text, university, college_name, student_Name, father_name, branch_name, house_Name, dress_Colour, programme, stud_Mobile, EmailId, strPwd, programme_colg);
                        //string body = "";
                       // Task<int> task = Execute(body, lblEmail.Text, Subject, Convert.ToInt32(Session["OrgId"]));

                        status = objSendEmail.SendEmail(lblEmail.Text, body, Subject); //Calling Method

                       // status = task.Result;
                        if (status == 1)
                        {
                            OfferLetterLog(idno, RRNO.Text);
                            BindListView();
                            objCommon.DisplayUserMessage(updHouse, "Mail Sent Successfully.", this.Page);
                        }
                    }
                    else
                        objCommon.DisplayMessage("Sorry..! Didn't find Email Id for some Students", this.Page);
                }
            }

            catch (Exception ex)
            {
                throw;
            }

        }
    }

    private string MailBody(string Email, string RRNO, string university, string colg_name, string stud_Name, string father_name, string branch_name, string house_Name, string dress_Colour, string programme, string stud_mobile, string studemail, string pass, string programme_colg)
    {
        string body = "";
        body += "<table align ='centre'>";
        body += "<tr>";
        body += "<td style=align:left;> Enrollment No </td>";
        body += "<td style=font-weight:bold;> : " + "" + RRNO + "</td>";
        body += "<td style=align:left;></td>";
        body += "<td style=font-weight:bold;></td>";
        body += "</tr>";
        body += "<tr>";
        body += "<td style=align:left;>Student Name </td>";
        body += "<td style=font-weight:bold;>: " + "" + stud_Name + " </td>";
        body += "<td style=align:left;padding-left:25px> Father Name </td>";
        body += "<td style=font-weight:bold;>: " + "" + father_name + "</td>";
        body += "</tr>";
        body += "<tr>";
        body += "<td style=align:left;> ERP Link </td>";
        body += "<td style=font-weight:bold;>: " + "" + "https://cpukota.mastersofterp.in" + "</td>";
        body += "<td style=align:left;padding-left:25px> ERP Password</td>";
        body += "<td style=font-weight:bold;>: " + "" + pass + "</td>";
        body += "</tr>";
        body += "<tr>";
        body += "<td style=align:left;> Mobile No</td>";
        body += "<td style=font-weight:bold;>: " + "" + stud_mobile + "</td>";
        body += "<td style=align:left;padding-left:25px> Email</td>";
        body += "<td style=font-weight:bold;>: " + "" + Email + "</td>";
        body += "</tr>";
        body += "<tr>";
        body += "<td style=align:left;> House Name</td>";
        body += "<td style=font-weight:bold;>: " + "" + house_Name + "</td>";
        body += "<td style=align:left;padding-left:25px> House Color </td>";
        body += "<td style=font-weight:bold;>: " + "" + dress_Colour + "</td>";
        body += "</tr>";
        body += "<tr>";
        body += "<td style=align:left;> Programme Name </td>";
        body += "<td style=font-weight:bold;>: " + "" + programme + "</td>";
        body += "<td style=align:left;padding-left:25px> Branch Name </td>";
        body += "<td style=font-weight:bold;>: " + "" + branch_name + "</td>";
        body += "</tr>";
        body += "</table><br>";
        body += "</table><br>";
        body += "<tr>";
        body += "<td></td>";
        body += "</tr>";
        body += "<p>Dear " + "<b>" + stud_Name + "</b>" + ",</p>";
        body += "<p>With reference to your application for admission, you have been given provisional admission in " + "<b>" + programme_colg + "</b>" + "";
        body += "<p><b>Please note the following</b></p>";
        body += "<p>1. Your university identification number (UID/Enrollment No) and your ERP Username is " + "<b>" + RRNO + "</b>" + ". For all further communication, please always use your UID number.</p>";
        body += "<p>2. Please check your email ID mentioned above. If there is any change, please bring in the notice of the admission office.</p>";
        body += "<p>3. Kindly login in ERP link https://cpukota.mastersofterp.in and change the password for security reasons.</p>";
        body += "<p>4. If you wish to avail hostel/transport facility, please Contact the Student Welfare Desk.</p>";
        body += "<p>5. Your admission shall be confirmed only after verification of all documents with originals and once submitted various undertakings as per the list given below.</p>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "<tr>";
        body += "<td></td>";
        body += "</tr>";
        body += "<p> <b> For  " + university + "</b> </p>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "</br>";
        body += "<p><b>Admission Officer</b></p>";
        body += "<p style=text-underline-position;>List of documents to be submitted to the University on reporting day as per the format attached -</p>";
        body += "<p>1. Migration and TC from last academic institution</p>";
        body += "<p>2. Class 10th marksheet</p>";
        body += "<p>3. Class 12th marksheet (if applicable)</p>";
        body += "<p>4. UG marksheet (if applicable)</p>";
        body += "<p>5. Proof of address (Aadhar Card)</p>";
        body += "<p>6. Gap Certificate (if applicable)</p>";

        return body;
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub, int OrgId)
    {
        int ret = 0;
        try
        {
            Common objCommon = new Common();

            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> '' AND OrganizationId=" + OrgId, string.Empty);

            string Org = (dsconfig != null && dsconfig.Tables.Count > 0 && dsconfig.Tables[0].Rows.Count > 0) ? dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() : string.Empty;
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), Org);
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);



            var response = client.SendEmailAsync(msg);
            string res = "Accepted";
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }
            //attachments.Dispose();
        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }

    protected void OfferLetterLog(int idno, string RRNO)
    {
        try
        {
            CustomStatus CS = (CustomStatus)objOnlline.OfferLetterLog(idno, RRNO);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnsendoffer_Click(object sender, EventArgs e)
    {
        string ids = string.Empty;
        foreach (ListViewDataItem item in lvStudentConf.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            if (chk.Checked)
            {
                if (ids == string.Empty)
                    ids += chk.ToolTip;
                else
                    ids += "$" + chk.ToolTip;
            }
        }

        if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
        {
            ShowReport("AdmissionConfirmation", "AdmissionOfferLetter.rpt", "pdf", ids);
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 5)
        {
 
            ShowReport("AdmissionConfirmation", "AdmissionLetter_jecrc.rpt", "pdf", ids);

           
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string exporttype, string ids)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 5)
            {
                url += "&param=@P_IDNO=" + ids + "";
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + "";
            }

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.updHouse, updHouse.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_PHDVerifiactionPortal.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnViewPdf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnViewPdf = sender as ImageButton;
            string idno = btnViewPdf.CommandArgument;
            string Regno = btnViewPdf.ToolTip;

            //string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + Regno + "'");
            //string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);
            //objCommon.DisplayMessage(updHouse,strPwd, this.Page);
        if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
        {
            ShowReportPdf("AdmissionConfirmation", "AdmissionOfferLetter.rpt", idno, "ocra");
        }
        else 
        {
        ShowReport("AdmissionConfirmation", "AdmissionLetter_jecrc.rpt", "pdf", idno);
        }
            }
        catch
        {
            throw;
        }
    }

    private void ShowReportPdf(string reportTitle, string rptFileName, string ids, string password)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;

            url += "&path=~,Reports,Academic," + rptFileName;

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids + ",@Password=" + password + "";

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ids; //+ password;

            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.updHouse, updHouse.GetType(), "Report", Script, true);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updHouse, this.updHouse.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Sendmail()
        {
        string email_type = string.Empty;
        string Link = string.Empty;
        int sendmail = 0;
        string subject = string.Empty;
        string srnno = string.Empty;
        string pwd = string.Empty;
        int status = 0;
       // string IDNO = Session["IDNO"].ToString();

        foreach (ListViewDataItem item in lvStudentConf.Items)
            {
           // int status = 0;
      
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                Label lblEmail = item.FindControl("lblEmail") as Label;
                Label RRNO = item.FindControl("lblRrno") as Label;
                int idno = Convert.ToInt32(chk.ToolTip);
                Session["IDNO"] = idno;
                if (chk.Checked == true)
                    {
                    if (lblEmail.Text != string.Empty)
                        {


                        // int IDNOnew = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO='" + IDNO + "'"));

                        //string DAmount = Convert.ToString(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0) TOTAL_AMT", "IDNO=" + IDNOnew));

                        string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);

                        string Username = string.Empty;
                        string Password = string.Empty;

                        string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
                        string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENAME, ' in ',B.LONGNAME)", "IDNO=" + Session["IDNO"].ToString());

                        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
                        string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
                        string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
                        // Username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
                        //  Password = objCommon.LookUp("ACD_USER_REGISTRATION", "User_Password", "USERNO=" + Userno);
                       // Username = REGNO;
                        // objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
                       // Password = REGNO;

                        Username = objCommon.LookUp("USER_ACC", "UA_NAME", " UA_TYPE=2 AND UA_IDNO=" + Convert.ToInt32(Session["IDNO"]));
                        Password = objCommon.LookUp("USER_ACC", "UA_PWD", " UA_TYPE=2 AND UA_IDNO=" + Convert.ToInt32(Session["IDNO"]));


                        Password = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Password.ToString());
                        Session["Enrollno"] = srnno;
                        DataSet ds = getModuleConfig();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                            email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                            Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                            sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

                            if (sendmail == 1)
                                {
                                subject = "New MIS Login Credentials";
                                //string message = "<p>Dear :<b>" + Name + "</b> </p>";
                                //       message += "<p>Your New Login Credentials Are Below Please Use this Credentails to Login in ERP from Following Link.</P>";
                                //       message+="<p style=font-weight:bold;> " + MISLink + " </p>";
                                //       message+="<p>Username   : " + Username + " <br/>Password: " + Password + "</p>";

                                string message = "";
                                message += "<p>Dear :<b>" + Name + "</b> </p>";
                                message += "<p><b>" + Branchname + "</>b</p>";
                                message += "<p>you have been registered for the program mentioned above.Your new Login credentials are as follows</p><p>" + MISLink + " </p><p>Username   : " + Username + " <br/>Password: " + Password + "</p>";
                                message += "<p>Note for Provisional Registration only:</p>";
                                message += "<p>All the documents must be uploaded on URL: <b>" + MISLink + "</b>";
                                message += "<p>Process of fee payment: Login using above credentials in <b>" + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.: ";
                                message += "<p>The fee payment should be made within 7 days of receiving this mail/letter, after which your claim for admission may be requested.</p>";
                                message += "<p style=font-weight:bold;>Thanks<br>Team Admissions<br>admissions@jecrcu.edu.in<br>JECRC University, Jaipur</p>";

                                //if (email_type == "1" && email_type != "")
                                //    {
                                //    int reg = TransferToEmail(EmailID, message, subject);
                                //    }
                                //else if (email_type == "2" && email_type != "")
                                //    {
                                //    Task<int> task = Execute(message, EmailID, subject);
                                //    status = task.Result;
                                //    }
                                //if (email_type == "3" && email_type != "")
                                //    {
                                //    OutLook_Email(message, EmailID, subject);
                                //    }


                                status = objSendEmail.SendEmail(EmailID, message, subject); //Calling Method
                                }
                            }

                        if (status == 1)
                            {
                            objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
                           
                            }
                        else
                            {
                            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
                            }
                           }
                    else
                        objCommon.DisplayMessage("Sorry..! Didn't find Email Id for some Students", this.Page);


                    }
                }
        }



    private DataSet getModuleConfig()
        {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
        }
    //public int TransferToEmail(string useremail, string message, string subject)
    //    {
    //    int ret = 0;
    //    try
    //        {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //        if (dsconfig != null)
    //            {
    //            string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

    //            MailMessage msg = new MailMessage();
    //            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
    //            // fromPassword = Common.DecryptPassword(fromPassword);
    //            msg.From = new System.Net.Mail.MailAddress(fromAddress, "RCPIPER");
    //            msg.To.Add(new System.Net.Mail.MailAddress(useremail));
    //            msg.Subject = subject;
    //            msg.Body = message;
    //            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
    //            smtp.EnableSsl = true;
    //            smtp.Port = 587; // 587
    //            smtp.Host = "smtp.gmail.com";

    //            ServicePointManager.ServerCertificateValidationCallback =
    //            delegate(object s, X509Certificate certificate,
    //            X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //                {
    //                return true;
    //                };

    //            smtp.Send(msg);
    //            if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
    //                {
    //                return ret = 1;
    //                //Storing the details of sent email
    //                }
    //            else
    //                {
    //                return ret = 0;
    //                }
    //            }
    //        }
    //    catch (Exception ex)
    //        {
    //        throw;
    //        }
    //    return ret;

    //    }
    //static async Task<int> Execute(string Message, string toEmailId, string sub)
    //    {
    //    int ret = 0;
    //    try
    //        {
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
    //        //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //        string res = Convert.ToString(response.StatusCode);
    //        if (res == "Accepted")
    //            {
    //            ret = 1;
    //            Console.WriteLine("Email Sent successfully!");

    //            }
    //        else
    //            {
    //            ret = 0;
    //            Console.WriteLine("Fail to send Mail!");
    //            }
    //        //attachments.Dispose();
    //        }
    //    catch (Exception ex)
    //        {
    //        ret = 0;
    //        }
    //    return ret;
    //    }
    //private int OutLook_Email(string Message, string toEmailId, string sub)
    //    {

    //    int ret = 0;
    //    try
    //        {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;

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
    //        }
    //    catch (Exception ep)
    //        {
    //        Console.WriteLine("failed to send email with the following error:");
    //        Console.WriteLine(ep.Message);
    //        ret = 0;
    //        }
    //    return ret;
    //    }

    protected void btnsendmailjecrc_Click(object sender, System.EventArgs e)
        {
        Sendmail();
        }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        // FILL DROPDOWN BATCH
        //  objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");'

        if (ddlDegree.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND  B.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue), "A.LONGNAME");
            //(B.DEPTNO IN(SELECT VALUE FROM dbo.Split( Session["userdeptno"].ToString()))or '0' in(SELECT VALUE FROM dbo.Split( Session["userdeptno"].ToString())))
            ddlBranch.Focus();
            //ddlAdmbatch.SelectedIndex = 0;
            }
        else
            {
            ddlDegree.SelectedIndex = 0;
            }
        }


    protected void ddlClg_SelectedIndexChanged(object sender, EventArgs e)
        {

        if (ddlClg.SelectedIndex > 0)
            {

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlClg.SelectedValue, "D.DEGREENO");
            ddlDegree.Focus();
            }
        else
            {
            ddlClg.SelectedIndex = 0;
            }
           
        }
}