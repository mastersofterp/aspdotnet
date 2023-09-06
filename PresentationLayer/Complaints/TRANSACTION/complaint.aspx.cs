//===============================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : REPAIR AND MAINTANANCE                                          
// PAGE NAME     : TO ALLOT COMPLAINT TO SELECTED EMPLOYEE                         
// CREATION DATE : 16-April-2006                                                   
// CREATED BY    : SANJAY RATNPARKHI & G.V.S KIRAN                                 
// MODIFIED BY   : MRUNAL SINGH 
// MODIFIED DATE : 10-SEP-2015
// MODIFIED DESC : AS PER THE REQUIREMENT OF MIZO UNIVERSITY A COMMON COMPLAINT FORM IS REQUIRED.
//===============================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;

public partial class Estt_complaint : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Complaint objCT = new Complaint();
    ComplaintController objCC = new ComplaintController();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public string path = string.Empty;
    public string dbPath = string.Empty;



    protected void Page_PreInit(object sender, EventArgs e)
    {
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //else
                //    lblHelp.Text = "No Help Added";


                BindListView();
                FillDepartment();
                ViewState["action"] = "add";
                ViewState["ComplaintId"] = null;

                txtComplaintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                //txtComplaintDate.Text = Convert.ToString(DateTime.Now);
                txtName.Text = Session["userfullname"].ToString();

                //txtComplaintDate.Attributes.Add("readonly", "true");
                objCommon.FillDropDownList(ddlArea, "COMPLAINT_AREA", "AREAID", "AREANAME", "", "AREAID");
                //  objCommon.FillDropDownList(ddlPriorityWork, "COMPLAINT_PRIORITY_WORK", "PWID", "PWNAME", "", "PWID");

                if (Session["usertype"].ToString() == "2")
                {
                    stuInfo.Visible = true;
                    GetStudentDetails(Convert.ToInt32(Session["idno"]));
                }
                else
                {
                    stuInfo.Visible = false;
                }
            }
        }
    }

    private void GetComplaintNo(int DeptId, int TypeId)
    {
        try
        {
            DataSet ds = objCC.GetComplaintNo(DeptId, TypeId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtComplaintNo.Text = ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.GetComplaintNo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // get student details
    private void GetStudentDetails(int idno)
    {
        try
        {
            DataSet ds = objCC.GetStudentInfo(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
                lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                lblEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                lblBranch.Text = ds.Tables[0].Rows[0]["SHORTNAME"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.GetStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCC.GetAllComplaints(Convert.ToInt32(Session["userno"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvComplaint.DataSource = ds;
                lvComplaint.DataBind();
                lvComplaint.Visible = true;
            }
            else
            {
                lvComplaint.DataSource = ds;
                lvComplaint.DataBind();
                lvComplaint.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDepartment()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", "FLAG_SP=1", "DEPTNAME");
            ddlRMDept.DataSource = ds;
            ddlRMDept.DataValueField = ds.Tables[0].Columns["DeptId"].ToString();
            ddlRMDept.DataTextField = ds.Tables[0].Columns["Deptname"].ToString();
            ddlRMDept.DataBind();
            ddlRMDept.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.FillEntryFor-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            objCT.ComplaintNo = txtComplaintNo.Text;
            objCT.ComplaintDate = Convert.ToDateTime(txtComplaintDate.Text);
            objCT.complaint = txtDetails.Text.Trim();
            objCT.Complaintee_Name = txtName.Text.Trim();
            objCT.Complaintee_Address = txtLocation.Text.Trim();
            objCT.Complaintee_PhoneNo = txtContactNo.Text;
            //objCT.ComplaintStatus = 'P';
            //objCT.Deptid = Int32.Parse(objCommon.LookUp("user_ACC", "UA_DEPTNO", "ua_no = " + Session["userno"].ToString()));
            objCT.Deptid = Convert.ToInt32(ddlRMDept.SelectedValue);
            objCT.AllotmentStatus = 'N';
            objCT.Ua_No = Int32.Parse(Session["userno"].ToString());
            objCT.Admin_UA_no = Convert.ToInt32(ddlRMCompTo.SelectedValue);
            objCT.AreaId = Convert.ToInt32(ddlArea.SelectedValue);
            objCT.Complaintee_OtherPhoneNo = txtContactNoOther.Text.Trim() == string.Empty ? string.Empty : txtContactNoOther.Text.Trim();
            objCT.PWID = 0;
            objCT.TypeId = Convert.ToInt32(ddlCompNature.SelectedValue);
            objCT.PreferableDate = DateTime.MinValue;
            objCT.PreferableTime = DateTime.MinValue;
            objCT.PreferableTimeTo = DateTime.MinValue;
            if (rdStatus.Checked == true)
            {
                objCT.ComplaintStatus = 'R';
            }
            else
            {
                objCT.ComplaintStatus = 'P';
            }
            if (txtRemark.Text != string.Empty)
            {
                objCT.OPENREMARK = txtRemark.Text;
            }
            else
            {
                objCT.OPENREMARK = string.Empty;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objCC.AddComplaint(objCT);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ShowMessage("Service Requested Successfully.");
                        ViewState["ComplaintId"] = objCT.ComplaintId;
                        BindListView();
                        SendSMSToComplaintee(Convert.ToInt32(ViewState["ComplaintId"].ToString()));
                        ClearAll();
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        // ShowMessage("Service Requested Successfully.");
                        ClearAll();
                        return;
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                     
                    objCT.ComplaintId = Convert.ToInt32(ViewState["ComplaintId"]);
                    CustomStatus cs = (CustomStatus)objCC.UpdateComplaint(objCT);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ShowMessage("Service Updated Successfully.");
                        ViewState["ComplaintId"] = objCT.ComplaintId;
                        BindListView();
                        //objCommon.DisplayMessage(UpdatePanel1, "Complaint Registerd Successfully.", this.Page);                            
                       // SendSMSToComplaintee(Convert.ToInt32(ViewState["ComplaintId"].ToString()));
                        ClearAll();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This button is use to send emails to committee members
    private void SendSMSToComplaintee(int ComplaintId)
    {
        try
        {
            string body = string.Empty;
            DataSet ds = objCC.GetUserData(ComplaintId, Convert.ToInt32(Session["usertype"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                string receiver = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["UA_EMAIL"].ToString() != "")
                    {
                        if (receiver == string.Empty)
                        {
                            receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                        }
                    }
                }

                string FromEmailID = string.Empty;
                string FromEmailPassword = string.Empty;
                DataSet dsReff = objCommon.FillDropDown("REFF", "IS_COMPLAINT_EMAIL", "EMAILSVCID,EMAILSVCPWD", "", "");
                if (dsReff.Tables[0].Rows.Count > 0)
                {
                    if (dsReff.Tables[0].Rows[0]["IS_COMPLAINT_EMAIL"].ToString() == "1")
                    {
                        FromEmailID = dsReff.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                        FromEmailPassword = dsReff.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                        sendmail(FromEmailID, FromEmailPassword, receiver, "Service Request", "Dear Sir");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void sendmail(string FromEmailID, string FromEmailPassword, string toEmailId, string Sub, string body)
    {
        try
        {
            DataSet ds = null;

            ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN COMPLAINT_DEPARTMENT CD ON (CR.DEPTID = CD.DEPTID) INNER JOIN COMPLAINT_TYPE CT ON (CR.TYPEID = CT.TYPEID)", "CR.COMPLAINTNO, CR.COMPLAINTDATE, CD.DEPTNAME", "CR.PREFERABLE_DATE, CR.PREFERABLE_TIME, CR.COMPLAINTEE_NAME, CT.TYPENAME, CR.COMPLAINT", "CR.COMPLAINTID=" + Convert.ToInt32(ViewState["ComplaintId"]) + "", "");

            if (toEmailId != string.Empty)
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = Sub;

                string MemberEmailId = string.Empty;
                mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(FromEmailID));
                mailMessage.To.Add(toEmailId);

                var MailBody = new StringBuilder();
                MailBody.AppendFormat("Dear Sir, New Service Request is assigned to you with Service Req.no.{0}\n", " ");
                MailBody.AppendLine(@"<br />" + ds.Tables[0].Rows[0]["COMPLAINTNO"]);
                MailBody.AppendLine(@"<br /> On date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["COMPLAINTDATE"]).ToString("dd/MM/yyyy"));
                MailBody.AppendLine(@"<br /> Department : " + ds.Tables[0].Rows[0]["DEPTNAME"]);
                MailBody.AppendLine(@"<br /> Requester Name : " + ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"]);
                MailBody.AppendLine(@"<br /> Request Type : " + ds.Tables[0].Rows[0]["TYPENAME"]);
                MailBody.AppendLine(@"<br /> Request: " + ds.Tables[0].Rows[0]["COMPLAINT"]);
                MailBody.AppendLine(@"<br /> ");
                MailBody.AppendLine(@"<br /> ");
                MailBody.AppendLine(@"<br /> Thank You. ");

                mailMessage.Body = MailBody.ToString();
                mailMessage.IsBodyHtml = true;
                SmtpClient smt = new SmtpClient("smtp.gmail.com");

                smt.UseDefaultCredentials = false;
                smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(FromEmailID), HttpUtility.HtmlEncode(FromEmailPassword));
                smt.Port = 587;
                smt.EnableSsl = true;

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smt.Send(mailMessage);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    //For Message Box
    public void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    private void ClearAll()
    {

        //txtComplaintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        txtContactNo.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtLocation.Text = string.Empty;
        txtName.Text = Session["userfullname"].ToString();
        ddlRMDept.SelectedIndex = 0;
        ddlRMCompTo.Items.Clear();
        txtComplaintNo.Text = string.Empty;
        ddlCompNature.SelectedIndex = 0;

        ddlCompNature.Enabled = true;
        ddlRMDept.Enabled = true;
        ddlRMCompTo.Enabled = true;


        ddlArea.SelectedIndex = 0;
        txtContactNoOther.Text = string.Empty;
        txtEnterDate.Text = string.Empty;
        txtOPT.Text = string.Empty;
        lvfile.DataSource = null;
        lvfile.DataBind();
        pnlFile.Visible = false;
        ViewState["ComplaintId"] = null;
        divOpen.Visible = false;
        rdStatus.Checked = false;
        txtRemark.Text = string.Empty;
        txtComplaintDate.Text = string.Empty;
    }

    protected void ddlRMDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearEmployeeAndFill(Convert.ToInt32(ddlRMDept.SelectedValue));
        objCommon.FillDropDownList(ddlCompNature, "COMPLAINT_TYPE", "TYPEID", "TYPENAME", "DEPTID = " + Convert.ToInt32(ddlRMDept.SelectedValue), "");
        ddlRMDept.Focus();
    }

    private void FillComplaintTo(int deptid)
    {
        try
        {
            DataSet ds = objCC.GetComplaintTo(deptid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRMCompTo.DataSource = ds;
                ddlRMCompTo.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
                ddlRMCompTo.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
                ddlRMCompTo.DataBind();
                ddlRMCompTo.SelectedIndex = 0;
            }
            else
            {
                ddlRMCompTo.Items.Clear();
                ddlRMCompTo.Items.Add("Please Select");
                ddlRMCompTo.SelectedItem.Value = "0";
            }
            //DataSet ds = objCC.GetComplaintTo(deptid);
            //ddlRMCompTo.DataSource = ds;
            //ddlRMCompTo.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
            //ddlRMCompTo.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
            //ddlRMCompTo.DataBind();
            //ddlRMCompTo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Clear Emoloyee DDL and Fill again
    private void ClearEmployeeAndFill(Int32 deptid)
    {
        ddlRMCompTo.Items.Clear();
        FillComplaintTo(deptid);
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
       // BindListView();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int complaintID = int.Parse(btnDelete.CommandArgument);
            ViewState["ComplaintId"] = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER", "*", "", "ALLOTMENTSTATUS IN ('A', 'Y') AND COMPLAINTID=" + complaintID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Requested Service Can Not Be Deleted.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objCC.DeleteComplaint(complaintID, Convert.ToInt32(Session["idno"]));
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    ClearAll();
                    BindListView();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Service Deleted.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlArea.SelectedIndex > 0)
            {
                txtLocation.Enabled = true;
                txtLocation.Focus();
            }
            else
            {
                txtLocation.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnGenerateOTP_Click(object sender, EventArgs e)
    {
        lblOTP.Text = string.Empty;
        //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string alphabets = "";
        string small_alphabets = "";
        string numbers = "1234567890";

        string characters = numbers;
        if (rbType.SelectedItem.Value == "1")
        {
            characters += alphabets + small_alphabets + numbers;
        }
        int length = int.Parse(ddlLength.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        lblOTP.Text = otp;
        SendSMSforOTP();
    }

    private void SendSMSforOTP()
    {
        try
        {
            string fromSMSId = string.Empty;
            string fromSMSPwd = string.Empty;
            string msg = string.Empty;

            //============= Message for SMS ==============
            msg += "For Service Requested One Time Password is " + lblOTP.Text;
            int a = msg.Length;

            if (txtContactNo.Text != string.Empty)
            {
                int smsStatus = objCC.SENDMSG_PASS(msg, txtContactNo.Text);
                objCommon.DisplayMessage(UpdatePanel1, "OTP sent successfully.", this.Page);
                return;
            }
            else if (txtContactNoOther.Text != string.Empty)
            {
                int smsStatus = objCC.SENDMSG_PASS(msg, txtContactNoOther.Text);
                objCommon.DisplayMessage(UpdatePanel1, "OTP sent successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Enter Mobile No.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.SendSMSforOTP()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Attachment

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                if (txtComplaintNo.Text != string.Empty)
                {
                    if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                    {
                        string file = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + txtComplaintNo.Text.Replace("/", "-");

                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }
                        ViewState["FILENAME"] = file;

                        path = file + "\\ComplaintNo_" + txtComplaintNo.Text.Replace("/", "-") + "_" + FileUpload1.FileName;

                        dbPath = file;
                        string filename = FileUpload1.FileName;
                        ViewState["FILE_NAME"] = filename;

                        //CHECKING FOLDER EXISTS OR NOT file
                        HttpPostedFile chkFileSize = FileUpload1.PostedFile;
                        if (chkFileSize.ContentLength <= 102400) // For Allowing 100 Kb Size Files only 
                        {
                            if (txtComplaintNo.Text != "")
                            {
                                if (!System.IO.Directory.Exists(path))
                                {
                                    FileUpload1.PostedFile.SaveAs(path);
                                }
                            }
                            BindListViewFiles(file);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "File size should not exceed 100 Kb.", this.Page);
                        }
                    }

                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
                        FileUpload1.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Plese enter complaint no.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //ChecK for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".zip", ".ZIP", ".txt", ".TXT", ".docx", ".DOCX", ".XLSX", ".xlsx", ".rar", ".RAR" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;
        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "ComplaintNo")
            f_name = Convert.ToString(fname[2]);

        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);

        return f_name;
    }

    protected string GetFileNameCaseNo(object obj)
    {
        string f_name = string.Empty;

        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "ComplaintNo")
            f_name = Convert.ToString(fname[1]);
        f_name = f_name.Replace('-', '/');
        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);
        return f_name;
    }

    protected string GetFileDate(object obj)
    {
        string file_path = Convert.ToString(ViewState["FILE_PATH"] + "\\" + obj.ToString());
        FileInfo fileInfo = new FileInfo(file_path);

        DateTime creationTime = DateTime.MinValue;
        creationTime = fileInfo.CreationTime;

        string f_date = string.Empty;
        //f_date = creationTime.ToString("dd-MMM-yyyy");
        f_date = DateTime.Today.ToString("dd-MMM-yyyy");
        return f_date;
    }

    public void savefile(string fpath, string fname)
    {
        try
        {
            if (ViewState["action"].ToString() == "ce")
                fname = "ComplaintNo_" + Convert.ToInt32(ViewState["idno"].ToString()) + "_" + fname;
            if (ViewState["action"].ToString() == "pr")
                fname = "judDoc_ComplaintNo_" + Convert.ToInt32(ViewState["idno"].ToString()) + "_" + fname;

            //SAVING FILE IN TO SERVER PATH 
            FileUpload1.PostedFile.SaveAs(fpath + "\\" + fname);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Uploaded Successfully.');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.savefile-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewFiles(string PATH)
    {
        try
        {
            pnlFile.Visible = true;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {
                System.IO.FileInfo[] files = dir.GetFiles();

                if (Convert.ToBoolean(files.Length))
                {
                    lvfile.DataSource = files;
                    lvfile.DataBind();
                    ViewState["FILE"] = files;
                }
                else
                {
                    lvfile.DataSource = null;
                    lvfile.DataBind();
                }
            }
            else
            {
                lvfile.DataSource = null;
                lvfile.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAttachDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string case_entry_no = btnDelete.CommandArgument;
            string[] fname1 = case_entry_no.ToString().Split('_');

            path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + fname1[1];

            string fname = btnDelete.CommandArgument;


            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted  !!');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        BindListViewFiles();

    }

    private void BindListViewFiles()
    {
        try
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = dir.GetFiles();
            lvfile.DataSource = files;
            lvfile.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void imgFileDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }

    public void DownloadFile(string filePath)
    {
        try
        {
            string[] fname1 = filePath.ToString().Split('_');
            string filename = filePath.Substring(filePath.LastIndexOf("_") + 1);
            path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + fname1[1];
            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            //Response.AddHeader("content-disposition", "attachment; filename=" + filePath);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case ".jpg":
                return "application/{0}";
                break;
            case ".jpeg":
                return "application/{0}";
                break;
            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    #endregion



    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton ebtn = sender as ImageButton;
            int CompId = int.Parse(ebtn.CommandArgument);
            ViewState["ComplaintId"] = int.Parse(ebtn.CommandArgument);
            ViewState["action"] = "edit";

            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER", "*", "", "ALLOTMENTSTATUS IN ('A', 'Y') AND COMPLAINTID=" + CompId, "");
            DataSet dsS = objCommon.FillDropDown("COMPLAINT_REGISTER", "*", "", "COMPLAINTSTATUS IN ('D') AND COMPLAINTID=" + CompId, "");
            DataSet DSStatus = objCommon.FillDropDown("COMPLAINT_REGISTER", "*", "", "COMPLAINTSTATUS IN ('C','R') AND COMPLAINTID=" + CompId, "");

            if (ds.Tables[0].Rows.Count > 0 || dsS.Tables[0].Rows.Count > 0)
            {
               // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Service Cannot Be Modified.');", true);
               // return;
                //divOpen.Visible = true;
                ShowDetails(CompId);
            }
            else
            {
                ShowDetails(CompId);
               // divOpen.Visible = false;
            }

            if (DSStatus.Tables[0].Rows.Count > 0)
            {
                divOpen.Visible = true;
            }
            else
            {
                divOpen.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btnEdit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int CompId)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("COMPLAINT_REGISTER", "COMPLAINTID,	COMPLAINTNO, COMPLAINTDATE, COMPLAINT, COMPLAINTEE_NAME, COMPLAINTEE_ADDRESS", "COMPLAINTEE_PHONENO, AREAID, ALLOTMENTSTATUS, COMPLAINTSTATUS, DEPTID, UA_NO, ADMIN_UA_NO, OTHER_CONTACTNO, TYPEID", "COMPLAINTID=" + CompId, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtComplaintDate.Text = ds.Tables[0].Rows[0]["COMPLAINTDATE"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"].ToString();
                ddlRMDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTID"].ToString();
                txtComplaintNo.Text = ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString();
                ClearEmployeeAndFill(Convert.ToInt32(ddlRMDept.SelectedValue));
                ddlRMCompTo.SelectedValue = ds.Tables[0].Rows[0]["ADMIN_UA_NO"].ToString();

                objCommon.FillDropDownList(ddlCompNature, "COMPLAINT_TYPE", "TYPEID", "TYPENAME", "DEPTID = " + Convert.ToInt32(ddlRMDept.SelectedValue), "");
                ddlCompNature.SelectedValue = ds.Tables[0].Rows[0]["TYPEID"].ToString();

                ddlCompNature.Enabled = false;
                ddlRMDept.Enabled = false;
                ddlRMCompTo.Enabled = false;

                txtDetails.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                ddlArea.Text = ds.Tables[0].Rows[0]["AREAID"].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_ADDRESS"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_PHONENO"].ToString();
                txtContactNoOther.Text = ds.Tables[0].Rows[0]["OTHER_CONTACTNO"].ToString();


                path = Docpath + "COMPLAINTS\\ComplaintsFiles\\ComplaintNo_" + txtComplaintNo.Text.Replace("/", "-");
                BindListViewFiles(path);
            }

            DataSet ds1 = null;
            ds1 = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN COMPLAINT_DETAIL CD ON (CR.COMPLAINTID = CD.COMPLAINTID)", "CR.COMPLAINTID,	CD.REMARK", "CR.COMPLAINTSTATUS", "CR.COMPLAINTSTATUS IN ('R') AND CR.COMPLAINTID=" + CompId, "");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    rdStatus.Checked = true;
                    txtRemark.Text = ds1.Tables[0].Rows[0]["REMARK"].ToString();
                }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.ShowDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["COMPLAINTID"] = int.Parse(btnPrint.CommandArgument);
            ShowReport("ComplaintRegister", "rptServiceRequestRegister.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@P_COMPLAINTID=" +  ViewState["ComplaintId"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    protected void ddlCompNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetComplaintNo(Convert.ToInt32(ddlRMDept.SelectedValue), Convert.ToInt32(ddlCompNature.SelectedValue));
            ddlCompNature.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}