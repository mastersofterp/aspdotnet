using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;

using System.Net.Mail;
using System.Net;
using System.Text;

public partial class GrievanceRedressal_Transaction_GrivSubApp : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public string path = string.Empty;
    public string dbPath = string.Empty;

    static int ImgId = 0;
    public int _idno;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    txtDate.Text = Convert.ToString(System.DateTime.Now);
                    FillDropDownList();
                    ViewState["action"] = "add";
                    BindlistView();
                    ViewState["GRIV_ID"] = null;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    protected void Clear()
    {
        ddlGrievanceT.SelectedIndex = 0;
        txtGrievance.Text = string.Empty;
        ViewState["GAID"] = null;
        ViewState["action"] = "add";
        // btnSave.Enabled = true;
        txtGrAppNo.Text = string.Empty;
        lvattachment.DataSource = null;
        lvattachment.DataBind();
        pnlAttach.Visible = false;
        ddlSubGriv.SelectedIndex = 0;
    }

    private void FillDropDownList()
    {

        objCommon.FillDropDownList(ddlGrievanceT, "GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "", "GRIV_ID");
        DataSet ds = null;
        ds = objGrivC.GetStudentPersonalDetails(Convert.ToInt32(Session["idno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtstudname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtsemester.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAdmissionNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
        }
    }

    private void BindlistView()
    {
        try
        {
            DataSet dss = objGrivC.GetSubGrievanceApplicationList(Convert.ToInt32(Session["idno"]));
            if (dss.Tables[0].Rows.Count > 0)
            {
                //ViewState["GRIV_ID"] = dss.Tables[0].Rows[0]["GRIV_ID"].ToString();
                lvGrApplication.DataSource = dss;
                lvGrApplication.DataBind();
            }
            else
            {
                lvGrApplication.DataSource = null;
                lvGrApplication.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected Boolean GrivDuplicate()
    {
        DataSet ds = null;
        if (ViewState["action"].Equals("add"))
        {
            ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION", "*", " ", "GRIEVANCE='" + txtGrievance.Text + "'", "");
        }
        else
        {
            //ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION", "*", " ", "GRIEVANCE='" + txtGrievance.Text + "' AND STUD_IDNO !=" + Convert.ToInt32(ViewState["idno"]), "");
        }
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnFileDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string case_entry_no = btnDelete.CommandArgument;
            string[] fname1 = case_entry_no.ToString().Split('_');

            path = Docpath + "GRIEVANCEAPPLICATION\\GrievanceApplicationFiles\\GrievanceApplicationNo_" + fname1[1];

            string fname = btnDelete.CommandArgument;


            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
                // objCommon.DisplayMessage("Record Deleted Sucessfully", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.btnFileDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        BindListViewAttachment();
    }

    private void BindListViewAttachment()
    {
        try
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = dir.GetFiles();
            lvattachment.DataSource = files;
            lvattachment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.BindListViewAttachment-> " + ex.Message + " " + ex.StackTrace);
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
            path = Docpath + "GRIEVANCEAPPLICATION\\GrievanceApplicationFiles\\GrievanceApplicationNo_" + fname1[1];
            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);

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

            case ".pdf":
                return "application/pdf";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (GrivDuplicate() == true)
            {
                objCommon.DisplayMessage(this.updGrievanceApplication, "This Grievance Already Exist.", this.Page);
                return;
            }
            // btnSave.Enabled = false;
            txtDate.Text = Convert.ToString(System.DateTime.Now);
            objGrivE.MOBILE_NO = txtMobile.Text == string.Empty ? "" : txtMobile.Text.Trim();
            objGrivE.EMAIL_ID = txtEmail.Text == string.Empty ? "" : txtEmail.Text.Trim();
            //objGrivE.GRCOMMITTEE_TYPE = Convert.ToInt32(ddlGrievanceT.SelectedValue);
            objGrivE.GRIEVANCE = txtGrievance.Text == string.Empty ? "" : txtGrievance.Text.Trim();
            objGrivE.GR_APPLICATION_DATE = Convert.ToDateTime(txtDate.Text);
            objGrivE.UANO = Convert.ToInt32(Session["idno"]);
            objGrivE.STUDEPTID = Convert.ToString(Session["userdeptno"]);
            objGrivE.GRIV_CODE = txtGrAppNo.Text == string.Empty ? "" : txtGrAppNo.Text.Trim();
            objGrivE.GRIV_ID = Convert.ToInt32(ddlGrievanceT.SelectedValue);
            objGrivE.GRIV_SUB_ID = Convert.ToInt32(ddlSubGriv.SelectedValue);

            if (flupDoc.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(flupDoc.FileName)))
                {
                    if (flupDoc.FileContent.Length >= 1024 * 10000)
                    {
                        objCommon.DisplayMessage(Page, "File Size Should Not Be Greater Than 10 Mb", this.Page);
                        flupDoc.Dispose();
                        flupDoc.Focus();
                        return;
                    }
                    else
                    {
                        objGrivE.GRIV_ATTACHMENT = Convert.ToString(flupDoc.PostedFile.FileName.ToString());

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Upload Valid Files[.jpg,.pdf,.doc,.txt]", this.Page);
                    flupDoc.Focus();
                    return;
                }
            }
            else
            {
                if (ViewState["attachment"] != null)
                {
                    objGrivE.GRIV_ATTACHMENT = ViewState["attachment"].ToString();
                }

                else
                {
                    objGrivE.GRIV_ATTACHMENT = string.Empty;
                }
            }


            if (Session["idno"] != null)
            {
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        objGrivE.GR_GAID = 0;
                        CustomStatus cs = (CustomStatus)objGrivC.AddUpSubGrievancTypeApplication(objGrivE);
                        if (flupDoc.HasFile)
                        {

                            int idno = Convert.ToInt32(Session["idno"]);
                            objGrivE.GRIV_ATTACHMENT = Convert.ToString(flupDoc.PostedFile.FileName.ToString());
                            objGrivC.upload_new_files("GrievanceApplicationDoc", idno, "GAID", "GRIV_GRIEVANCE_APPLICATION", "GAID_", flupDoc);
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["GAID"] = objGrivE.GR_GAID;
                            DataSet dsReff = objCommon.FillDropDown("REFF", "IS_GRIEVANCE_EMAIL", "EMAILSVCID, EMAILSVCPWD", "", "");
                            if (dsReff.Tables[0].Rows.Count > 0)
                            {
                                if (dsReff.Tables[0].Rows[0]["IS_GRIEVANCE_EMAIL"].ToString() == "1")
                                {
                                    SendMailToDeptCommitteeMember();
                                }
                            }
                            BindlistView();
                            objCommon.DisplayMessage(Page, "Record Saved Successfully.", this.Page);
                            Clear();
                        }
                    }
                    else
                    {
                        objGrivE.GR_GAID = Convert.ToInt32(ViewState["GAID"]);
                        // int gaid = objGrivE.GAID;
                        CustomStatus cs = (CustomStatus)objGrivC.AddUpSubGrievancTypeApplication(objGrivE);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //objGrivC.update_upload("GrievanceApplicationDoc", gaid, ViewState["attachment"].ToString(), _idno, "GAID_", flupDoc);
                            BindlistView();
                            objCommon.DisplayMessage(Page, "Record Updated Successfully.", this.Page);
                            Clear();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void SendMailToDeptCommitteeMember()
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;
            //int DEPTID = Convert.ToInt32(Session["userdeptno"]);
            string STUDEPTID = Convert.ToString(Session["userdeptno"]);
            string body = string.Empty;

            DataSet ds = objGrivC.GetDataForEmail(STUDEPTID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = "deansw@svce.ac.in"; // string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                sendmail(fromEmailId, fromEmailPwd, receiver, "SVCE Grievance Application", "Dear Sir");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.SendMailToDeptCommitteeMember-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;
            int GAID = Convert.ToInt32(ViewState["GAID"]);


            DataSet ds = objGrivC.GetDataGrAppDeptDetails(GAID);

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br /> Application No : " + ds.Tables[0].Rows[0]["GRIV_CODE"]);
            MailBody.AppendLine(@"<br /> Grievance Type : " + ds.Tables[0].Rows[0]["GT_NAME"]);
            MailBody.AppendLine(@"<br /> Application Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br /> Application By : " + ds.Tables[0].Rows[0]["STUDNAME"].ToString() + "/" + ds.Tables[0].Rows[0]["YEARNAME"].ToString() + "/" + ds.Tables[0].Rows[0]["SECTIONNAME"].ToString() + "/" + ds.Tables[0].Rows[0]["ENROLLNO"].ToString());
            MailBody.AppendLine(@"<br /> Details : " + ds.Tables[0].Rows[0]["GRIEVANCE"]);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> Kindly refer the above Grievance.");
            //MailBody.AppendLine(@"<br />Thanks And Regards");
            //MailBody.AppendLine(@"<br />" + ds.Tables[0].Rows[0]["UA_FULLNAME"]);


            mailMessage.Body = MailBody.ToString();

            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
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
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEditRecord_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int GAID = int.Parse(btnEdit.CommandArgument);
            ViewState["GAID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(GAID);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.btnEditRecord_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int GAID)
    {
        try
        {
            DataSet ds = null;
            ds = objGrivC.GetSubGrievanceTypeApplication(GAID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                txtGrievance.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
                ddlGrievanceT.SelectedValue = ds.Tables[0].Rows[0]["GRIV_ID"].ToString();
                objCommon.FillDropDownList(ddlSubGriv, "GRIV_SUB_GRIEVANCE_TYPE", "SUB_ID", "SUBGTNAME", "GRIV_ID=" + Convert.ToInt32(ddlGrievanceT.SelectedValue), "SUB_ID");
                ddlSubGriv.SelectedValue = ds.Tables[0].Rows[0]["SUB_ID"].ToString();
                GrApplnNo.Visible = true;
                txtGrAppNo.Text = ds.Tables[0].Rows[0]["GRIV_CODE"].ToString();

                ViewState["attachment"] = ds.Tables[0].Rows[0]["GRIV_ATTACHMENT"].ToString();

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Grievance Can Not Be Modified.');", true);
                Clear();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int GR_GAID = int.Parse(btnDelete.CommandArgument);
        objGrivE.GR_GAID = Convert.ToInt32(GR_GAID);

        DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION", "*", "", "GR_STATUS != 'P' AND GAID=" + GR_GAID, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Grievance Can Not Be Deleted.');", true);
            return;
        }
        else
        {
            CustomStatus CS = (CustomStatus)objGrivC.DeleteApplicationDetail(objGrivE);
            if (CS.Equals(CustomStatus.RecordDeleted))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Sucessfully.');", true);
                //objCommon.DisplayMessage("Record Deleted Sucessfully", this.Page);
                BindlistView();
                Clear();
            }
        }
    }

    private void GetGrievanceApplicationNo(int GRIV_ID)
    {
        try
        {
            DataSet ds = objGrivC.GetGrievanceApplicationNo(GRIV_ID, Convert.ToInt32(Session["idno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtGrAppNo.Text = ds.Tables[0].Rows[0]["GRIVTYPE_CODE"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.GetGrievanceApplicationNo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlGrievanceT_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrApplnNo.Visible = true;
        int GRIV_ID = Convert.ToInt32(ddlGrievanceT.SelectedValue);
        GetGrievanceApplicationNo(GRIV_ID);
        objCommon.FillDropDownList(ddlSubGriv, "GRIV_SUB_GRIEVANCE_TYPE", "SUB_ID", "SUBGTNAME", "GRIV_ID=" + Convert.ToInt32(ddlGrievanceT.SelectedValue), "SUB_ID");
    }

    public string GetFileNamePath(object filename, object GAID, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/GrievanceRedressal/upload_documents/GrievanceApplicationDoc/" + idno.ToString() + "/GAID_" + GAID + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".png", ".PNG", ".pdf", ".PDF", ".doc", ".DOC", ".txt", ".TXT", ".docx", ".DOCX" };
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

        if (fname[0] == "GrievanceApplicationNo")
            f_name = Convert.ToString(fname[2]);

        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);

        return f_name;
    }
    //GRIEVANCEAPPLICATION\\GrievanceApplicationFiles\\GrievanceApplicationNo_
    public void savefile(string fpath, string fname)
    {
        try
        {
            if (ViewState["action"].ToString() == "ce")
                fname = "GrievanceApplicationNo_" + Convert.ToInt32(ViewState["idno"].ToString()) + "_" + fname;
            if (ViewState["action"].ToString() == "pr")
                fname = "judDoc_GrievanceApplicationNo_" + Convert.ToInt32(ViewState["idno"].ToString()) + "_" + fname;

            //SAVING FILE IN TO SERVER PATH 
            flupDoc.PostedFile.SaveAs(fpath + "\\" + fname);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Uploaded Successfully.');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.savefile-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewAttachment(string PATH)
    {
        try
        {
            pnlAttach.Visible = true;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {
                System.IO.FileInfo[] files = dir.GetFiles();

                if (Convert.ToBoolean(files.Length))
                {
                    lvattachment.DataSource = files;
                    lvattachment.DataBind();
                    ViewState["FILE"] = files;
                }
                else
                {
                    lvattachment.DataSource = null;
                    lvattachment.DataBind();
                }
            }
            else
            {
                lvattachment.DataSource = null;
                lvattachment.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.BindListViewAttachment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


}