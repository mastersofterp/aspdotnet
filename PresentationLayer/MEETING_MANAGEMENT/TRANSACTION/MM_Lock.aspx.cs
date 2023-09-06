// ==================================================
// MODIFY BY   : MRUNAL SINGH
// MODIFY DATE : 21/11/2014
// DESCRIPTION : ADD FUNCTIONALITY TO SEND EMAIL, 
//               SMS AND GENERATE A CIRCULAR REPORT 
// ==================================================

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using System.Text;


public partial class MEETING_MANAGEMENT_Transaction_MM_Lock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();
    Sms objSms = new Sms();

    SmsController objSmsController = new SmsController();
    static char LOCK_MEETING;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];

    public string path = string.Empty;
    public string dbPath = string.Empty;

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
                   
                    FillCollege();
                    ViewState["action"] = "edit";
                    // chksendSms.Visible = false;
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                    else
                        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Lock.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




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


    // This method is used to check page authorization.
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


    // This method is used to fill Colleges
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  BindlistView(Convert.ToInt32(ddlCollege.SelectedValue));
        FILL_DROPDOWN();
    }
    // This method is used to fill committee list.
    public void FILL_DROPDOWN()
    {
        // objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "STATUS=0 AND (DEPTNO=" + Session["userEmpDeptno"] + " OR " + Session["userEmpDeptno"] + "=0)", "NAME");
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0  AND COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
    }

    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlcode, "TBL_MM_AGENDA", " DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " ", "");
        chkagenda.Checked = false;
        chkmeeting.Checked = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string mobilenum = string.Empty;
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
                    {
                        if (chkagenda.Checked == true)
                        {
                            objMM.LOCK = 'Y';
                        }
                        else
                        {
                            objMM.LOCK = 'N';
                        }
                        if (chkmeeting.Checked == true)
                        {
                            LOCK_MEETING = 'Y';
                        }
                        else
                        {
                            LOCK_MEETING = 'N';
                        }
                        objMM.COMMITEE_NO = Convert.ToInt32(ddlCommitee.SelectedValue);
                        objMM.CODE = Convert.ToString(ddlcode.SelectedItem);
                        CustomStatus cs = (CustomStatus)objMC.Update_Agenda_Meeting_Lock(objMM, LOCK_MEETING);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "edit";
                            objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                            clear();
                        }
                    }
                    else
                    {
                        // objCommon.DisplayMessage(this.updActivity, "Please Select From List!!", this.Page);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Lock.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    // This method is used to check Lock/Unlock.
    private void AgendaCheck()
    {
        if (chkagenda.Checked == true)
        {
            if (chkmeeting.Checked == true)
            {
                chkagenda.Text = "Lock";
                chkmeeting.Text = "Lock";

                objMM.LOCK = 'Y';
                LOCK_MEETING = 'Y';
            }
            else
            {
                chkagenda.Text = "Lock";
                chkmeeting.Text = "Lock";
                objMM.LOCK = 'Y';
                LOCK_MEETING = 'N';
            }
        }
        else
        {
            objMM.LOCK = 'N';
            chkagenda.Text = "Lock";
        }
    }
    // This method is used to Meeting Lock/Unlock.
    private void MeetingCheck()
    {
        if (chkmeeting.Checked == true)
        {
            if (chkagenda.Checked == true)
            {
                chkagenda.Text = "Lock";
                chkmeeting.Text = "Lock";
                objMM.LOCK = 'Y';
                LOCK_MEETING = 'Y';
            }
            else
            {
                chkmeeting.Text = "Lock";
                chkagenda.Text = "Lock";
                objMM.LOCK = 'Y';
                LOCK_MEETING = 'N';
            }
        }
        else
        {
            objMM.LOCK = 'N';
            chkmeeting.Text = "Lock";
        }
    }

    protected void chkagenda_CheckedChanged(object sender, EventArgs e)
    {
        if (chkagenda.Checked == true)
        {
            if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
            {
                AgendaCheck();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Committee.\\n Please select Meeting Code.'  );  ", true);

            }
        }
    }

    protected void chkmeeting_CheckedChanged(object sender, EventArgs e)
    {
        // MeetingCheck();
    }

    // This method is used to clear the controls.
    private void clear()
    {
        chkagenda.Checked = false;
        chkmeeting.Checked = false;
        // chksendEmail.Checked = false;
        ddlcode.SelectedIndex = 0;
        ddlCommitee.SelectedIndex = 0;
        ddlcode.DataSource = null;
        ddlcode.DataBind();
        //  chksendSms.Checked = false;
        ddlCollege.SelectedIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void ddlcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Code to display Agenda Lock data
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "*", "", "FK_MEETING =" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + Convert.ToString(ddlcode.SelectedItem) + "' ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            char lock1 = Convert.ToChar(ds.Tables[0].Rows[0]["Lock"]);
            if (lock1 == 'Y')
            {
                chkagenda.Checked = true;
                chkagenda.Text = "Lock";
            }
            else
            {
                chkagenda.Checked = false;
                chkagenda.Text = "Lock";
            }
        }
        // Code to display Meeting Lock Data
        //tbl_mm_MeetingDetails
        DataSet ds1 = objCommon.FillDropDown(" TBL_MM_MEETINGDETAILS", "*", "", "FK_COMMITTE=" + ddlCommitee.SelectedValue + " and METTINGCODE='" + Convert.ToString(ddlcode.SelectedItem) + "' ", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            chkmeeting.Enabled = true;
            LOCK_MEETING = Convert.ToChar(ds1.Tables[0].Rows[0]["LOCK_MEET"]);
            if (LOCK_MEETING == 'Y')
            {
                chkmeeting.Checked = true;
                chkmeeting.Text = "Lock";
            }
            else
            {
                chkmeeting.Checked = false;
                chkmeeting.Text = "Lock";
            }
        }
        else
        {
            //objCommon.DisplayMessage(this.updActivity, "Meeting Not Exist.", this.Page);
            chkmeeting.Enabled = false;
            chkmeeting.Checked = false;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "CommitteeMembersCircular.rpt");
    }

    // This method is used to generate report.
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=CommitteeMembersCircular" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_MEETINGCODE=" + ddlcode.SelectedItem.Text + ",@P_COMMITTEE=" + ddlCommitee.SelectedValue;


            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Lock.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".docx", ".xlsx", ".DOCX", ".XLSX", ".zip", ".ZIP", ".txt", ".TXT", ".rar", ".RAR" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void btnAttachFile_Click(object sender, EventArgs e)
    {
        try
        {
            //CHECKING FILE EXISTS OR NOT
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
                    {
                        string file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlcode.SelectedItem.Text;

                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }
                        ViewState["FILENAME"] = file;
                        path = file + "//" + FileUpload1.FileName; ;
                        dbPath = file;
                        string filename = FileUpload1.FileName;
                        ViewState["FILE_NAME"] = filename;
                        //CHECKING FOLDER EXISTS OR NOT
                        if (!System.IO.Directory.Exists(path))
                        {
                            FileUpload1.PostedFile.SaveAs(path);
                        }
                        BindListViewFiles(file);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Meeting Code.');  ", true);
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
                    FileUpload1.Focus();
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please Select File.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void BindListViewFiles(string PATH)
    {
        try
        {
            //  pnlFile.Visible = true;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {

                System.IO.FileInfo[] files = dir.GetFiles();
                if (Convert.ToBoolean(files.Length))
                {
                    lvfile.DataSource = files;
                    lvfile.DataBind();
                    ViewState["FILE"] = files;
                    lvfile.Visible = true;
                }
                else
                {
                    lvfile.DataSource = null;
                    lvfile.DataBind();
                    lvfile.Visible = false;
                }
            }
            else
            {
                lvfile.DataSource = null;
                lvfile.DataBind();
                lvfile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }

    public void DownloadFile(string filePath)
    {
        try
        {
            path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlcode.SelectedItem.Text;

            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            Response.AddHeader("content-disposition", "attachment; filename=" + filePath);
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

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;

        f_name = obj.ToString();
        return f_name;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlcode.SelectedItem.Text;


            //CHECKING FILE EXISTS OR NOT
            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted.');", true);
                BindListViewFiles(path);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //protected void btnSendMail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
    //   if (chkmeeting.Checked==true)
    //        {

    //            string fromEmailId = string.Empty;
    //            string fromEmailPwd = string.Empty;
    //            string body = string.Empty;

    //            // DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER a INNER JOIN Tbl_MM_COMITEE b on (a.FK_COMMITEE =b.ID) INNER JOIN TBL_MM_MENBERDETAILS c on(a.FK_MEMBER=c.PK_CMEMBER)", "c.PK_CMEMBER", "c.T_EMAIL as EMAIL,c.T_PHONE", "a.FK_COMMITEE='" + ddlCommitee.SelectedValue + "'" + "   AND c.T_EMAIL<>''", "");
    //            DataSet ds = objMC.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                if(ds.Tables[1].Rows[0]["IS_MOM_EMAIL"].ToString()!="0")
    //                {
    //                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
    //                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();
    //                }
    //                else{
    //                      objCommon.DisplayMessage(this.updActivity, "Sending Notification Faild.", this.Page);
    //                }

    //                string receiver = string.Empty;
    //                string userid = string.Empty;
    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (receiver == string.Empty)
    //                    {
    //                        receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
    //                        userid = ds.Tables[0].Rows[i]["userid"].ToString();
    //                    }
    //                    else
    //                    {
    //                        receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
    //                        userid = userid + "," + ds.Tables[0].Rows[i]["userid"].ToString();
    //                    }
    //                }

    //                //sendmail("recruitment@iitms.co.in", "IITMS@123", receiver, "Reminder Mail For Meeting", "Dear Members", userid);
    //                sendmail(fromEmailId, fromEmailPwd,receiver, "Related to Meeting.", "Dear Members", userid);
    //            }

    //        }
    //        else
    //        {
    //           // objCommon.DisplayMessage(this.updActivity, "Please Select Committee & Meeting.", this.Page);
    //            objCommon.DisplayMessage(this.updActivity, "Meeting Should Be Locked", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }


    //}


    //// This method is used to send bulk Emails.

    //public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    //{
    //    try
    //    {
    //        System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
    //        // MailMessage mailMessage = new MailMessage();

    //        mailMessage.Subject = Sub;
    //        string AgendaItemList = string.Empty;
    //        string AgendaContentList = string.Empty;

    //        DataSet dsCommittee = null;
    //        dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
    //        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE, AGENDATITAL", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

    //        //  string[] idno = userid.Split(',');


    //        //for (int j = 0; j < idno.Length; j++)
    //        //{
    //        string MemberEmailId = string.Empty;
    //        mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));

    //        //  MemberEmailId = objCommon.LookUp("TBL_MM_MENBERDETAILS MD INNER JOIN TBL_MM_RELETIONMASTER RM ON (MD.PK_CMEMBER = RM.FK_MEMBER)", "ISNULL(P_EMAIL,' ') AS P_EMAIL", "P_EMAIL <> '' AND RM.USERID=" + idno[j] + " AND RM.FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue)); // multipleIdno);

    //        mailMessage.To.Add(toEmailId);



    //        var MailBody = new StringBuilder();
    //        MailBody.AppendFormat("Dear Members, {0}\n", " ");
    //        MailBody.AppendLine(@"<br />");
    //        MailBody.AppendLine(@"<br />Committee   : " + dsCommittee.Tables[0].Rows[0]["NAME"]);
    //        MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]);
    //        MailBody.AppendLine(@"<br />Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["MEETINGDATE"]).ToString("yyyy-MM-dd"));
    //        MailBody.AppendLine(@"<br />Meeting Title : " + ds.Tables[0].Rows[0]["AGENDATITAL"]);
    //        //MailBody.AppendLine(@"<br />Time : " + ds.Tables[0].Rows[0]["MEETINGTIME"]);
    //        //MailBody.AppendLine(@"<br /> Venue : " + ds.Tables[0].Rows[0]["VENUE"]);
    //        MailBody.AppendLine(@"<br />-----------------------------------------------------------");
    //        MailBody.AppendLine(@"<br />This meeting is available for Search Meeting Details.");



    //        DataSet dsAgendaList = objCommon.FillDropDown("TBL_MM_AGENDA", "PK_AGENDA, MEETING_CODE, AGENDANO", "ROW_NUMBER() OVER(ORDER BY PK_AGENDA ASC) SrNo, AGENDATITAL", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

    //        //if (dsAgendaList.Tables[0].Rows.Count > 0)
    //        //{
    //        //    for (int i = 0; i < dsAgendaList.Tables[0].Rows.Count; i++)
    //        //    {

    //        //        DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA_CONTENT_DETAILS ACD ON (AC.ACID = ACD.ACID)", "ACD.SRNO", "ACD.CONTENT_DETAILS", "AC.AGENDA_ID =" + dsAgendaList.Tables[0].Rows[i]["PK_AGENDA"].ToString(), "");
    //        //        if (AgendaItemList == string.Empty)
    //        //        {
    //        //            MailBody.AppendLine(@"<br />Agenda Item: " + dsAgendaList.Tables[0].Rows[i]["SrNo"].ToString() + ") " + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString());

    //        //            if (dsContent.Tables[0].Rows.Count > 0)
    //        //            {
    //        //                for (int k = 0; k < dsContent.Tables[0].Rows.Count; k++)
    //        //                {
    //        //                    if (AgendaContentList == string.Empty)
    //        //                    {
    //        //                        AgendaContentList = dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
    //        //                    }
    //        //                    else
    //        //                    {
    //        //                        AgendaContentList = AgendaContentList + "<br />" + dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
    //        //                    }
    //        //                }

    //        //                MailBody.AppendLine(@"<br />   " + AgendaContentList);
    //        //                MailBody.AppendLine(@"<br />-----------------------------------------------------------");
    //        //                AgendaItemList = string.Empty;
    //        //                AgendaContentList = string.Empty;
    //        //            }

    //        //        }
    //        //        else
    //        //        {
    //        //            // AgendaItemList = AgendaItemList + "<br />" + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString();
    //        //        }
    //        //    }

    //        //}

    //        mailMessage.Body = MailBody.ToString();
    //        mailMessage.IsBodyHtml = true;



    //        //foreach (DataRow dr in ds.Tables[0].Rows)
    //        //{
    //        //string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlcode.SelectedItem.Text;

    //        //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
    //        //if (System.IO.Directory.Exists(path))
    //        //{
    //        //    FileInfo[] files = dir.GetFiles();
    //        //    if (Convert.ToBoolean(files.Length))
    //        //    {
    //        //        foreach (FileInfo fi in files)
    //        //        {
    //        //            Attachment file = new Attachment(fi.FullName.ToString());
    //        //            mailMessage.Attachments.Add(file);
    //        //        }
    //        //    }
    //        //    else
    //        //    {
    //        //    }
    //        //}
    //        //}


    //        SmtpClient smt = new SmtpClient("smtp.gmail.com");

    //        smt.UseDefaultCredentials = false;
    //        smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
    //        smt.Port = 587;
    //        smt.EnableSsl = true;


    //        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
    //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
    //        System.Security.Cryptography.X509Certificates.X509Chain chain,
    //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
    //        {
    //            return true;
    //        };
    //        //smt.Timeout = 2000000; // Add Timeout property                      
    //        smt.Send(mailMessage);
    //        objCommon.DisplayMessage(this.updActivity, "Mail Sent Successfully.", this.Page);
    //        mailMessage.Attachments.Dispose();

    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //}






    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //if (rdbCommitteeType.SelectedValue == "U")
            //{
            //    trCollegeName.Visible = false;
            //    ddlCollege.SelectedIndex = 0;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
            //}
            //else
            //{
            //    trCollegeName.Visible = true;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void btnSendSMS_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
    //        {

    //            string msg = string.Empty;
    //            MailMessage mailMessage = new MailMessage();
    //            mailMessage.IsBodyHtml = true;
    //            DataSet dsCommittee = null;

    //            string smsUserName = string.Empty;
    //            string smsPassword = string.Empty;



    //            DataSet ds = objMC.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                smsUserName = ds.Tables[1].Rows[0]["SMSSVCID"].ToString();
    //                smsPassword = ds.Tables[1].Rows[0]["SMSSVCPWD"].ToString();

    //                string receiver = string.Empty;
    //                string mobilenum = string.Empty;

    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (receiver == string.Empty)
    //                    {
    //                        receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
    //                        mobilenum = "91" + ds.Tables[0].Rows[i]["T_MOBILE"].ToString();
    //                        objSms.Mobileno = mobilenum;
    //                    }
    //                    else
    //                    {
    //                        receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
    //                        mobilenum = mobilenum + "," + "91" + ds.Tables[0].Rows[i]["T_MOBILE"].ToString();
    //                        objSms.Mobileno = mobilenum;
    //                    }
    //                }
    //            }

    //            objSms.Usename = "ceo@iitms.co.in";
    //            objSms.Password = "iitms123";
    //            objSms.Ua_no = Convert.ToInt32(Session["userno"]);
    //            objSms.Module_code = "MOM";


    //            dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");

    //            DataSet dsSms = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE,AGENDANO", "MEETINGDATE,MEETINGTIME,VENUE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + (ddlcode.SelectedItem.Text).ToString() + "'", "");
    //            mailMessage.Subject += "MEETING SCHEDULE DETAILS" + "%0a";
    //            mailMessage.Subject += "COMMITTEE    : " + dsCommittee.Tables[0].Rows[0]["NAME"] + "%0a";
    //            mailMessage.Subject += "MEETING CODE : " + dsSms.Tables[0].Rows[0]["MEETING_CODE"] + "%0a";
    //            mailMessage.Subject += "MEETING DATE : " + Convert.ToDateTime(dsSms.Tables[0].Rows[0]["MEETINGDATE"]).ToString("yyyy-MM-dd") + "%0a";
    //            mailMessage.Subject += "MEETING TIME : " + dsSms.Tables[0].Rows[0]["MEETINGTIME"] + "%0a";
    //            mailMessage.Subject += "MEETING VENUE: " + dsSms.Tables[0].Rows[0]["VENUE"] + "%0a";
    //            mailMessage.Subject += "This is Remainder for meeting held at above scheduled.";
    //            msg = mailMessage.Subject.ToString();
    //            int a = msg.Length;
    //            objSms.Msg_content = msg;
    //            objSmsController.SendBulkSms(objSms);
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updActivity, "Please Select Committee & Meeting.", this.Page);

    //        }
    //        objCommon.DisplayMessage(this.updActivity, "SMS Send Successfully.", this.Page);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.btnSendSMS_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }

    //}


//-------------------------------Send mail functionality------- Added by shabina 27/03/2023------------///
    protected void btnSendMail_Click(object sender, EventArgs e)
    
    {
        try
        {
            DataSet dss = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE,LOCK", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");
            string AgendaLock = dss.Tables[0].Rows[0]["LOCK"].ToString();
            if (AgendaLock == "Y")
            {
                if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
                {
                    string fromEmailId = string.Empty;
                    string fromEmailPwd = string.Empty;
                    string body = string.Empty;

                    // DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER a INNER JOIN Tbl_MM_COMITEE b on (a.FK_COMMITEE =b.ID) INNER JOIN TBL_MM_MENBERDETAILS c on(a.FK_MEMBER=c.PK_CMEMBER)", "c.PK_CMEMBER", "c.T_EMAIL as EMAIL,c.T_PHONE", "a.FK_COMMITEE='" + ddlCommitee.SelectedValue + "'" + "   AND c.T_EMAIL<>''", "");
                    DataSet ds = objMC.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                        fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                        string receiver = string.Empty;
                        string userid = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count;i++)
                        {
                            if (receiver == string.Empty)
                            {
                                receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                userid = ds.Tables[0].Rows[i]["userid"].ToString();
                            }
                            else
                            {
                                receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                userid = userid + "," + ds.Tables[0].Rows[i]["userid"].ToString();
                            }
                        }

                      //  sendmail("techsupport@makautwb.ac.in", "makaut@12345", receiver, "Reminder Mail For Meeting", "Dear Members", userid);
                         sendmail(fromEmailId, fromEmailPwd, receiver, "Reminder Mail For Meeting", "Dear Members", userid);
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Select Committee & Meeting.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Agenda is not locked, You Can Not Send the Email.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        // AGENDAsendmailBySendGrid();

    }
    // This method is used to send bulk Emails.
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    {
        try
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            // MailMessage mailMessage = new MailMessage();

            mailMessage.Subject = Sub;
            string AgendaItemList = string.Empty;
            string AgendaContentList = string.Empty;

            DataSet dsCommittee = null;
            dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE,MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

            //  string[] idno = userid.Split(',');


            //for (int j = 0; j < idno.Length; j++)
            //{
            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));

            
            //  MemberEmailId = objCommon.LookUp("TBL_MM_MENBERDETAILS MD INNER JOIN TBL_MM_RELETIONMASTER RM ON (MD.PK_CMEMBER = RM.FK_MEMBER)", "ISNULL(P_EMAIL,' ') AS P_EMAIL", "P_EMAIL <> '' AND RM.USERID=" + idno[j] + " AND RM.FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue)); // multipleIdno);

            mailMessage.To.Add(toEmailId);

           
           // int ModeOfMeeting = Convert.ToInt32(objCommon.LookUp("TBL_MM_AGENDA MD", "isnull(ModeOfMeeting,0) As ModeOfMeeting", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue))); // multipleIdno);
            var MailBody = new StringBuilder();
                     
            MailBody.AppendFormat("Dear Member, {0}\n", " ");
            MailBody.AppendLine(@"<br />");
            //MailBody.AppendFormat("Kindly Note The Following. {0}\n", " ");
            MailBody.AppendLine(@"<br /><b>Kindly Note The Following.</b> ");
            MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]);
            MailBody.AppendLine(@"<br /><b>Meeting Date :</b> " + Convert.ToDateTime(ds.Tables[0].Rows[0]["MEETINGDATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br /><b>Meeting Time :</b> " + ds.Tables[0].Rows[0]["MEETINGTIME"]);
                       
            MailBody.AppendLine(@"<br /><b>Meeting Venue :</b> " + ds.Tables[0].Rows[0]["VENUE"]);
           // MailBody.AppendLine(@"<br /><b>Meeting Link :</b> " + ds.Tables[0].Rows[0]["MeetingLink"]);
            // MailBody.AppendLine(@"<br /><b>Meeting Venue :</b> " + ds.Tables[0].Rows[0]["VENUE"]);

           // MailBody.AppendLine(@"<br /><b>For Any Query Email Us On -> </b> " + ds.Tables[0].Rows[0]["CommunicationEmail"]);     //09/01/2023

            MailBody.AppendLine(@"<br />");
            MailBody.AppendLine(@"<br /><b>Kindly Attend.</b>");
            MailBody.AppendLine(@"<br />-----------------------------------------------------------");


            DataSet dsAgendaList = objCommon.FillDropDown("TBL_MM_AGENDA", "PK_AGENDA, MEETING_CODE, AGENDANO", "ROW_NUMBER() OVER(ORDER BY PK_AGENDA ASC) SrNo, AGENDATITAL", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

            if (dsAgendaList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsAgendaList.Tables[0].Rows.Count; i++)
                {

                    DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA_CONTENT_DETAILS ACD ON (AC.ACID = ACD.ACID)", "ACD.SRNO", "ACD.CONTENT_DETAILS", "AC.AGENDA_ID =" + dsAgendaList.Tables[0].Rows[i]["PK_AGENDA"].ToString(), "");
                    if (AgendaItemList == string.Empty)
                    {
                        MailBody.AppendLine(@"<br /><b>Agenda Title: </b>" + dsAgendaList.Tables[0].Rows[i]["SrNo"].ToString() + ") " + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString());

                        if (dsContent.Tables[0].Rows.Count > 0)
                        {
                            for (int k = 0; k < dsContent.Tables[0].Rows.Count; k++)
                            {
                                if (AgendaContentList == string.Empty)
                                {
                                    AgendaContentList = dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
                                }
                                else
                                {
                                    AgendaContentList = AgendaContentList + "<br />" + dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
                                }
                            }

                            MailBody.AppendLine(@"<br /><b>Agenda Details:  </b> " + AgendaContentList);
                            MailBody.AppendLine(@"<br />-----------------------------------------------------------");
                            AgendaItemList = string.Empty;
                            AgendaContentList = string.Empty;
                        }

                    }
                    else
                    {
                        // AgendaItemList = AgendaItemList + "<br />" + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString();
                    }
                }

            }

            mailMessage.Body = MailBody.ToString();
            mailMessage.IsBodyHtml = true;



            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlcode.SelectedItem.Text;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            if (System.IO.Directory.Exists(path))
            {
                FileInfo[] files = dir.GetFiles();
                if (Convert.ToBoolean(files.Length))
                {
                    foreach (FileInfo fi in files)
                    {
                        Attachment file = new Attachment(fi.FullName.ToString());
                        mailMessage.Attachments.Add(file);
                    }
                }
                else
                {
                }
            }
            //}


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
            //smt.Timeout = 2000000; // Add Timeout property                      
            smt.Send(mailMessage);
            objCommon.DisplayMessage(this.updActivity, "Mail Sent Successfully For Agenda.", this.Page);
            mailMessage.Attachments.Dispose();

            //}

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dss = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE,MeetingSubject,MeetingLink,LOCK", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");
            string AgendaLock = dss.Tables[0].Rows[0]["LOCK"].ToString();
            if (AgendaLock == "Y")
            {

                if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
                {
                    string msg = string.Empty;
                    // MailMessage mailMessage = new MailMessage();
                    // mailMessage.IsBodyHtml = true;
                    DataSet dsCommittee = null;
                    string smsUserName = string.Empty;
                    string smsPassword = string.Empty;
                    DataSet ds = objMC.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        smsUserName = ds.Tables[1].Rows[0]["SMSSVCID"].ToString();
                        smsPassword = ds.Tables[1].Rows[0]["SMSSVCPWD"].ToString();

                        string receiver = string.Empty;
                        string mobilenum = string.Empty;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (receiver == string.Empty)
                            {
                                receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                mobilenum = "91" + ds.Tables[0].Rows[i]["T_MOBILE"].ToString();
                                objSms.Mobileno = mobilenum;
                            }
                            else
                            {
                                receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                mobilenum = mobilenum + "," + "91" + ds.Tables[0].Rows[i]["T_MOBILE"].ToString();
                                objSms.Mobileno = mobilenum;
                            }
                        }
                    }

                    //objSms.Usename = "ceo@iitms.co.in";
                    //objSms.Password = "iitms123";
                    objSms.Usename = smsUserName;
                    objSms.Password = smsPassword;
                    objSms.Ua_no = Convert.ToInt32(Session["userno"]);
                    objSms.Module_code = "MOM";


                    dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");

                    DataSet dsSms = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE,AGENDANO", "MEETINGDATE,MEETINGTIME,VENUE,Isnull(ModeOfMeeting,0) As ModeOfMeeting ", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + (ddlcode.SelectedItem.Text).ToString() + "'", "");
                    // mailMessage.Subject += "MEETING SCHEDULE DETAILS" + "%0a";
                    // mailMessage.Subject += "COMMITTEE    : " + dsCommittee.Tables[0].Rows[0]["NAME"] + "%0a";
                    // mailMessage.Subject += "MEETING CODE : " + dsSms.Tables[0].Rows[0]["MEETING_CODE"] + "%0a";
                    //mailMessage.Subject += "MEETING DATE : " + Convert.ToDateTime(dsSms.Tables[0].Rows[0]["MEETINGDATE"]).ToString("yyyy-MM-dd") + "%0a";
                    //mailMessage.Subject += "MEETING TIME : " + dsSms.Tables[0].Rows[0]["MEETINGTIME"] + "%0a";
                    //mailMessage.Subject += "MEETING VENUE: " + dsSms.Tables[0].Rows[0]["VENUE"] + "%0a";
                    //mailMessage.Subject += "This is Remainder for meeting held at above scheduled.";

                    DataSet dsss = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE,MeetingSubject,MeetingLink", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

                    string abc2 = dsss.Tables[0].Rows[0]["MeetingSubject"].ToString();// +"%0a";
                    int ModeOfMeeting = Convert.ToInt32(dsSms.Tables[0].Rows[0]["ModeOfMeeting"]);// +"%0a";
                    string abc4 = Convert.ToDateTime(dsss.Tables[0].Rows[0]["MEETINGDATE"]).ToString("dd-MM-yyyy");// +"%0a";
                    string abc5 = dsss.Tables[0].Rows[0]["MEETINGTIME"].ToString();// +"%0a";
                    string abc6 = "";
                    string abc7 = "";
                    if (ModeOfMeeting == 2) //-------------for offline meeting-Addeed by shabina 17/12/2022-----------
                    {
                        abc6 = dsss.Tables[0].Rows[0]["VENUE"].ToString();// +"%0a";
                        abc7 = "-";
                    }
                    else if (ModeOfMeeting == 1)
                    {
                        abc7 = dsss.Tables[0].Rows[0]["MeetingLink"].ToString();
                        abc6 = "-";
                    }
                    else
                    {
                        abc7 = "-";
                        abc6 = "-";
                    }




                    string message;
                    message = "Dear Member, " + "\r\n\nKindly note the following," + "\r\nMeeting Title: " + abc2 + "," + "\r\n Date: " + abc4 + "," + "\r\n Time: " + abc5 + "," + "\r\n Venue: " + abc6 + "\r\n Kindly attend" + "." + "\r\n\n Thank You\r\n\n MAKAUT, WB";
                    SendSMSTEMPLATE(objSms.Mobileno, message, "1007386120811349655");

                }
                else
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Select Committee & Meeting.", this.Page);
                    return;

                }
                objCommon.DisplayMessage(this.updActivity, "SMS Send Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Agenda is not locked, You Can Not Send the SMS.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.btnSendSMS_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    public void SendSMSTEMPLATE(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                // uid = "registrar@makautwb.ac.in";            //---for live
                // pass ="M@kaughjrthnkrrb@05";             //for live 
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnSendMeetingMail_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dss = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS", " distinct METTINGCODE, AgendaDetails", "AUDITDATE, LOCK_MEET, METTINGCODE", "FK_Committe=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND METTINGCODE='" + ddlcode.SelectedItem.Text + "'", "");
            if (dss.Tables[0].Rows.Count > 0)
            {
                string AgendaLock = dss.Tables[0].Rows[0]["LOCK_MEET"].ToString();
                if (AgendaLock == "Y")
                {
                    if (ddlCommitee.SelectedIndex > 0 && ddlcode.SelectedIndex > 0)
                    {
                        string fromEmailId = string.Empty;
                        string fromEmailPwd = string.Empty;
                        string body = string.Empty;

                        // DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER a INNER JOIN Tbl_MM_COMITEE b on (a.FK_COMMITEE =b.ID) INNER JOIN TBL_MM_MENBERDETAILS c on(a.FK_MEMBER=c.PK_CMEMBER)", "c.PK_CMEMBER", "c.T_EMAIL as EMAIL,c.T_PHONE", "a.FK_COMMITEE='" + ddlCommitee.SelectedValue + "'" + "   AND c.T_EMAIL<>''", "");
                        DataSet ds = objMC.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));

                        //DataSet dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_APIKEY", "COMPANY_EMAILSVCID,EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", "");
                        //string API = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();


                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString(); //dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();// 
                            fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();  // dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();      // 

                            string receiver = string.Empty;
                            string userid = string.Empty;
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (receiver == string.Empty)
                                {
                                    receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                    userid = ds.Tables[0].Rows[i]["userid"].ToString();
                                }
                                else
                                {
                                    receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                    userid = userid + "," + ds.Tables[0].Rows[i]["userid"].ToString();
                                }
                            }

                            //SendMeetingMail("techsupport@makautwb.ac.in", "makaut@12345", receiver, "Meeting Minutes", "Dear Members", userid);
                            SendMeetingMail(fromEmailId, fromEmailPwd, receiver, "Meeting Minutes.", "Dear Members", userid);
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updActivity, "Please Select Committee & Meeting.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updActivity, "Meeting is not locked, You Can Not Send the Email.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Meeting is not locked, You Can Not Send the Email.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

        // MEETINGsendmailBySendGrid();
    }
    public void SendMeetingMail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    {
        try
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            // MailMessage mailMessage = new MailMessage();

            mailMessage.Subject = Sub;
            string AgendaItemList = string.Empty;
            string AgendaContentList = string.Empty;

            DataSet dsCommittee = null;
            dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE,MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

            //  string[] idno = userid.Split(',');


            //for (int j = 0; j < idno.Length; j++)
            //{
            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));

            //  MemberEmailId = objCommon.LookUp("TBL_MM_MENBERDETAILS MD INNER JOIN TBL_MM_RELETIONMASTER RM ON (MD.PK_CMEMBER = RM.FK_MEMBER)", "ISNULL(P_EMAIL,' ') AS P_EMAIL", "P_EMAIL <> '' AND RM.USERID=" + idno[j] + " AND RM.FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue)); // multipleIdno);
           // int ModeOfMeeting = Convert.ToInt32(objCommon.LookUp("TBL_MM_AGENDA MD", "isnull(ModeOfMeeting,0) As ModeOfMeeting", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue))); // multipleIdno);

            mailMessage.To.Add(toEmailId);



            var MailBody = new StringBuilder();
            //MailBody.AppendFormat("Dear Members, {0}\n", " ");               
            //MailBody.AppendLine(@"<br />Committee   : " + dsCommittee.Tables[0].Rows[0]["NAME"]);
            //MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]);
            //MailBody.AppendLine(@"<br />Meeting Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["MEETINGDATE"]).ToString("yyyy-MM-dd"));
            //MailBody.AppendLine(@"<br />Meeting Time : " + ds.Tables[0].Rows[0]["MEETINGTIME"]);
            //MailBody.AppendLine(@"<br />Meeting Venue : " + ds.Tables[0].Rows[0]["VENUE"]);
            //MailBody.AppendLine(@"<br />-----------------------------------------------------------");
            //MailBody.AppendLine(@"<br />This is the Remainder mail of meeting held at above date, time and location.");
            //MailBody.AppendLine(@"<br />The Finalise Agenda Items are given below.");                

            MailBody.AppendFormat("Dear Member, {0}\n", " ");
            MailBody.AppendLine(@"<br />");
            //MailBody.AppendFormat("Kindly Note The Following. {0}\n", " ");
            MailBody.AppendLine(@"<br /><b>Kindly Note The Following.</b> ");
          //  MailBody.AppendLine(@"<br /><b>Meeting Subject :</b> " + ds.Tables[0].Rows[0]["MeetingSubject"]);
            MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]);
            MailBody.AppendLine(@"<br /><b>Meeting Date :</b> " + Convert.ToDateTime(ds.Tables[0].Rows[0]["MEETINGDATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br /><b>Meeting Time :</b> " + ds.Tables[0].Rows[0]["MEETINGTIME"]);
            MailBody.AppendLine(@"<br /><b>Meeting Venue :</b> " + ds.Tables[0].Rows[0]["VENUE"]);


            //if (ModeOfMeeting == 2)
            //{
            //    MailBody.AppendLine(@"<br /><b>Meeting Venue :</b> " + ds.Tables[0].Rows[0]["VENUE"]);
            //}
            //else if (ModeOfMeeting == 1)
            //{
            //    MailBody.AppendLine(@"<br /><b>Meeting Venue : Onine</b> ");
            //}
            //else
            //{
            //    MailBody.AppendLine(@"<br /><b>Meeting Venue :</b> " + "-");
            //}



            //  MailBody.AppendLine(@"<br /><b>Meeting Link :</b> " + ds.Tables[0].Rows[0]["MeetingLink"]);
           // MailBody.AppendLine(@"<br /><b>For Any Query Email Us On -> </b> " + ds.Tables[0].Rows[0]["CommunicationEmail"]);     //09/01/2023
            MailBody.AppendLine(@"<br />");
            //  MailBody.AppendLine(@"<br /><b>Kindly Attend.</b>");
            MailBody.AppendLine(@"<br />-----------------------------------------------------------");


            DataSet dsAgendaList = objCommon.FillDropDown("TBL_MM_AGENDA", "PK_AGENDA, MEETING_CODE, AGENDANO", "ROW_NUMBER() OVER(ORDER BY PK_AGENDA ASC) SrNo, AGENDATITAL", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");

            if (dsAgendaList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsAgendaList.Tables[0].Rows.Count; i++)
                {

                    DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA_CONTENT_DETAILS ACD ON (AC.ACID = ACD.ACID)", "ACD.SRNO", "ACD.CONTENT_DETAILS", "AC.AGENDA_ID =" + dsAgendaList.Tables[0].Rows[i]["PK_AGENDA"].ToString(), "");
                    if (AgendaItemList == string.Empty)
                    {
                        MailBody.AppendLine(@"<br /><b>Agenda Title: </b>" + dsAgendaList.Tables[0].Rows[i]["SrNo"].ToString() + ") " + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString());

                        if (dsContent.Tables[0].Rows.Count > 0)
                        {
                            for (int k = 0; k < dsContent.Tables[0].Rows.Count; k++)
                            {
                                if (AgendaContentList == string.Empty)
                                {
                                    AgendaContentList = dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
                                }
                                else
                                {
                                    AgendaContentList = AgendaContentList + "<br />" + dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
                                }
                            }

                            MailBody.AppendLine(@"<br /><b>Agenda Details:  </b> " + AgendaContentList);

                            DataSet meetingminutes = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS A inner join TBL_MM_AGENDA B on (A.FK_Committe=B.FK_Meeting and A.FK_AGENDA=B.PK_AGENDA)", "FK_Committe", "AGENDADETAILS", "A.FK_Committe =" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND B.MEETING_CODE='" + ddlcode.SelectedItem.Text + "'", "");
                            MailBody.AppendLine(@"<br /><b>Meeting Minutes :</b> " + meetingminutes.Tables[0].Rows[0]["AGENDADETAILS"]);
                            MailBody.AppendLine(@"<br />");
                            MailBody.AppendLine(@"<br /><b>Thank You for Participating in Meeting.</b>");
                            MailBody.AppendLine(@"<br />-----------------------------------------------------------");
                            AgendaItemList = string.Empty;
                            AgendaContentList = string.Empty;
                        }

                    }
                    else
                    {
                        // AgendaItemList = AgendaItemList + "<br />" + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString();
                    }
                }

            }

            mailMessage.Body = MailBody.ToString();
            mailMessage.IsBodyHtml = true;



            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlcode.SelectedItem.Text;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            if (System.IO.Directory.Exists(path))
            {
                FileInfo[] files = dir.GetFiles();
                if (Convert.ToBoolean(files.Length))
                {
                    foreach (FileInfo fi in files)
                    {
                        Attachment file = new Attachment(fi.FullName.ToString());
                        mailMessage.Attachments.Add(file);
                    }
                }
                else
                {
                }
            }
            //}


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
            //smt.Timeout = 2000000; // Add Timeout property                      
            smt.Send(mailMessage);
            objCommon.DisplayMessage(this.updActivity, "Mail Sent Successfully For Meeting.", this.Page);
            mailMessage.Attachments.Dispose();

            //}

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }





//------------------------end  send mail -----------------------------------------------------------------//
}
