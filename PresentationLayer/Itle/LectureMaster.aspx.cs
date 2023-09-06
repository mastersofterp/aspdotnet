using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;

public partial class Itle_LectureMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ILecture objLNote = new ILecture();
    ILectureNotesController objLN = new ILectureNotesController();
    BlobController objBlob = new BlobController();

    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    decimal File_size;
    string PageId;

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
        if (!Page.IsPostBack)
        {
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //if (ViewState["action"] == null)
                ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                PopulateDropDownList();
                BindListView();

            }
        }

        // Used to get maximum size of file attachment
        GetAttachmentSize();
        BlobDetails();
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_LectureMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_LectureMaster.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        DataSet ds = null;
        try
        {


            objLNote.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLNote.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            ds = objLN.GetTopicsFromSyllabus(objLNote);

            ddlTopic.Items.Clear();
            ddlTopic.Items.Add(new ListItem("Please Select", "0"));

            if (ds.Tables.Count > 0)
            {
                ddlTopic.DataSource = ds;
                ddlTopic.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                ddlTopic.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                ddlTopic.DataBind();
            }
            else
            {
                ddlTopic.DataSource = null;
                ddlTopic.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_LectureMaster.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            ILecture objLNote = new ILecture();
            ILectureNotesController objLN = new ILectureNotesController();

            objLNote.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLNote.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objLNote.UA_NO = Convert.ToInt32(Session["userno"]);

            DataSet ds = objLN.GetAllLectureNotes(objLNote);
            lvNotes.DataSource = ds;
            lvNotes.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_LectureMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("LectureMaster.aspx");
        }

    }

    private void ClearControls()
    {
        ViewState["action"] = "add";
        ftbDescription.Text = "&nbsp;";
        txtSubject.Text = string.Empty;
        lblStatus.Text = string.Empty;
        lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblSession.ToolTip = Session["SessionNo"].ToString();
        ddlTopic.SelectedIndex = 0;


        DataTable dt = null;
        lvCompAttach.DataSource = string.Empty;
        lvCompAttach.DataBind();
        BindListView_Attachments(dt);
        Session["Attachments"] = null;
        // lblPreAttach.Text = string.Empty;
    }

    private void ShowDetails(ILecture objLNote)
    {
        try
        {
            ILectureNotesController objLN = new ILectureNotesController();



            //used to access attachments
            DataSet ds = objLN.GetAllAtachmentByNoteNo(objLNote, Convert.ToInt32(Session["userno"]));
            DataTable dt = new DataTable();
            //int totFiles = ds.Tables[0].Rows.Count;
            // if (ds.Tables[0].Rows.Count > 0)                                 
            //{
            dt = this.GetAttachmentDataTable();
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                //dt = this.GetAttachmentDataTable();
                DataRow dr = dt.NewRow();
                dr["ATTACH_ID"] = ds.Tables[0].Rows[j]["ATTACHMENT_ID"];
                dr["FILE_NAME"] = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_PATH"] = ds.Tables[0].Rows[j]["FILE_PATH"].ToString();
                dr["SIZE"] = ds.Tables[0].Rows[j]["SIZE"];
                dt.Rows.Add(dr);
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

           

            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();

            if (dt.Rows.Count > 0)
            {
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                    Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                    Control ctrhead2 = lvCompAttach.FindControl("divattach");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = true;
                    ctrhead2.Visible = false;

                    foreach (ListViewItem lvRow in lvCompAttach.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdBlob");
                        Control ckattach = (Control)lvRow.FindControl("attachfile");
                        Control attachblob = (Control)lvRow.FindControl("attachblob");
                        ckBox.Visible = true;
                        attachblob.Visible = true;
                        ckattach.Visible = false;

                    }
                }
                else
                {



                    Control ctrHeader = lvCompAttach.FindControl("divDownload");
                    ctrHeader.Visible = false;

                    foreach (ListViewItem lvRow in lvCompAttach.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                        ckBox.Visible = false;

                    }
                }



            }



            DataTableReader dtr = objLN.GetSingleNoteByNoteNo(objLNote);

            //Show Announcement Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    ddlTopic.SelectedValue = dtr["SUB_NO"].ToString();
                    txtSubject.Text = dtr["TOPIC_NAME"] == null ? "" : dtr["TOPIC_NAME"].ToString();
                    ftbDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    //hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    lblCurrdate.Text = dtr["CREATED_DATE"] == null ? "" : Convert.ToDateTime(dtr["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");

                    //lblPreAttach.Visible = false;
                    //lblPreAttach.Text = "";
                    //if (dtr["ATTACHMENT"] != null)
                    //{
                    //    lblPreAttach.Visible = true;
                    //    lblPreAttach.Text = dtr["ATTACHMENT"].ToString();
                    //}

                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_LectureMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Events

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            List<LectureNotesAttachment> attachments = new List<LectureNotesAttachment>();

            string filename = string.Empty;
            objLNote.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objLNote.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLNote.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objLNote.SUB_NO = Convert.ToInt32(ddlTopic.SelectedValue);
            objLNote.TOPIC_NAME = txtSubject.Text.Trim();
            objLNote.DESCRIPTION = ftbDescription.Text.Trim();
            //objLNote.ATTACHMENT = fileUploader.FileName;

            if (lblBlobConnectiontring.Text == "")
            {
                objLNote.ATTACHMENT = "0";
            }
            else
            {
                objLNote.ATTACHMENT = "1";
            }

            objLNote.CREATED_DATE = Convert.ToDateTime(lblCurrdate.Text.ToString());
            objLNote.COLLEGE_CODE = Session["colcode"].ToString();


            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                DataTable dt = ((DataTable)Session["Attachments"]);
                foreach (DataRow dr in dt.Rows)
                {
                    LectureNotesAttachment attach = new LectureNotesAttachment();
                    attach.AttachmentId = Convert.ToInt32(dr["ATTACH_ID"]);
                    attach.FileName = dr["FILE_NAME"].ToString();
                    attach.FilePath = dr["FILE_PATH"].ToString();
                    attach.Size = Convert.ToInt32(dr["SIZE"]);
                    attachments.Add(attach);
                }
            }
            objLNote.Attachments = attachments;

            if (!string.IsNullOrEmpty(ViewState["action"].ToString()) && ViewState["action"].ToString().Equals("edit"))
            {
                objLNote.NOTE_NO = Convert.ToInt32(ViewState["note_no"]);

                LectureNotesAttachment attach = new LectureNotesAttachment();
                attach.NOTE_NO = Convert.ToInt32(ViewState["note_no"]);

                CustomStatus cs = (CustomStatus)objLN.UpdateLectureNote(objLNote, Convert.ToInt32(ViewState["note_no"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    lblStatus.Text = "Record Updated Successfully!";

                    string script = "Record Updated Successfully!";
                    objCommon.DisplayMessage(updLectureNotes, "Record Updated Successfully!", this.Page);
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    ClearControls();
                }
                //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                else
                    if (cs.Equals(CustomStatus.FileExists))
                    {
                        objCommon.DisplayMessage(updLectureNotes, "File already exists. Please upload another file or rename and upload.", this.Page);
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                        string script = "File already exists. Please upload another file or rename and upload.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    }
            }
            else
            {
                bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {

                    CustomStatus cs = (CustomStatus)objLN.AddLectureNotes(objLNote);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        string script = "Record Saved Successfully!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                        objCommon.DisplayMessage(updLectureNotes, "Record Saved Successfully!", this.Page);

                        lblStatus.Text = "Record Saved Successfully!";
                        ClearControls();
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updLectureNotes, "Record Already Exist.", this.Page);
                        lblStatus.Text = "Record Already Exist";

                        string script = "Record Already Exist";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                        //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    }
                    else if (cs.Equals(CustomStatus.FileExists))
                    {
                        string script = "File already exists. Please upload another file or rename and upload.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                        objCommon.DisplayMessage(updLectureNotes, "File already exists. Please upload another file or rename and upload.", this.Page);
                    }
                }
            }

            ClearControls();
            BindListView();
            Session["Attachments"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_LectureMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        //Response.Redirect("LectureMaster.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ILecture objLNote = new ILecture();
            ImageButton btnEdit = sender as ImageButton;
            int note_no = int.Parse(btnEdit.CommandArgument);
            ViewState["note_no"] = note_no;
            objLNote.NOTE_NO = Convert.ToInt32(ViewState["note_no"]);
            objLNote.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLNote.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            ShowDetails(objLNote);

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_LectureMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int an_no = int.Parse(btnDel.CommandArgument);

            ILectureNotesController objlC = new ILectureNotesController();

            CustomStatus cs = (CustomStatus)objlC.DeleteLectureNotes(Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), an_no);
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayMessage(updLectureNotes, "Notes Deleted Successfully...", this.Page);
            //lblStatus.Text = "Announcement Deleted Successfully...";
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LectureMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("ACD_ILECTURENOTES", "*", "", "TOPIC_NAME='" + txtSubject.Text +" and COURSENO="+ Convert.ToInt32(Session["ICourseNo"])+ "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #endregion

    #region Attachments

    public string GetFileName(object filename, object assingmentno)
    {
        if (filename != null && filename.ToString() != "")
            //return filename.ToString();
            //  return "assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/ILectures/lectureNotes_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/ILectures/" + filename.ToString();
        else
            return "";
    }

    //Get Page Id for getting File size from configuration

    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));

            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));

                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);

                }



        }
        catch (Exception ex)
        {

        }

    }

    //USED FOR MULTIPLE FILE ATTACHMENTS
    protected void btnAttachFile_Click(object sender, EventArgs e)
    {
        try
        {
            bool result;
            string filename = string.Empty;
            string FILEPATH = string.Empty;

           

            if (fileUploader.HasFile)
            {

                string DOCFOLDER = file_path + "ITLE\\upload_files\\LectureNotes";

                if (!System.IO.Directory.Exists(DOCFOLDER))
                {
                    System.IO.Directory.CreateDirectory(DOCFOLDER);

                }

                string fileName = System.Guid.NewGuid().ToString() + fileUploader.FileName.Substring(fileUploader.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);



                int SRNO = Convert.ToInt32(objCommon.LookUp("ACD_ILECTURENOTES_ATTACHMENTS", "(ISNULL(MAX(SR_NO),0))+1 AS SR_NO", ""));

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    DataTable dt1;
                    dt1 = ((DataTable)Session["Attachments"]);
                    int attachid = dt1.Rows.Count;

                    filename = SRNO + "_itleLectureMaster_" + Session["userno"] + "-" + attachid;
                }
                else
                {
                    filename = SRNO + "_itleLectureMaster_" + Session["userno"];
                }

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));
                DataSet dsPURPOSE = new DataSet();

                dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");

                if (count != 0)
                {

                    string filePath = file_path + "ITLE\\upload_files\\LectureNotes\\" + fileName;

                    if (fileUploader.PostedFile.ContentLength < File_size)
                    {
                       

                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);
                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, fileUploader);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                            FILEPATH = fileUploader.FileName;

                        }
                        else
                        {
                            HttpPostedFile file = fileUploader.PostedFile;
                            fileUploader.SaveAs(filePath);
                            FILEPATH = file_path + "ITLE\\upload_files\\LectureNotes\\" + fileName;

                        }
                        
                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                        return;
                    }

                    DataTable dt;
                    //int totFile = dt.Rows.Count;
                    if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                    {
                        dt = ((DataTable)Session["Attachments"]);
                        DataRow dr = dt.NewRow();

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //for (int i = 0; i < dt.Rows.Count; i++)
                            //{
                                dr["ATTACH_ID"] = dt.Rows.Count + 1;
                                if (result == true)
                                {
                                    dr["FILE_NAME"] = filename + fileExtention;
                                }
                                else
                                {
                                    dr["FILE_NAME"] = fileUploader.FileName;
                                }
                                dr["FILE_PATH"] = FILEPATH;
                                dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                                dt.Rows.Add(dr);
                                Session["Attachments"] = dt;
                                this.BindListView_Attachments(dt);
                           // }
                        }
                        else
                        {
                            dt = this.GetAttachmentDataTable();
                            dr = dt.NewRow();
                            dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            if (result == true)
                            {
                                dr["FILE_NAME"] = filename + fileExtention;
                            }
                            else
                            {
                                dr["FILE_NAME"] = fileUploader.FileName;
                            }
                            dr["FILE_PATH"] = FILEPATH;
                            dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session.Add("Attachments", dt);
                            this.BindListView_Attachments(dt);
                        }
                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        DataRow dr = dt.NewRow();
                        dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (result == true)
                        {
                            dr["FILE_NAME"] = filename + fileExtention;
                        }
                        else
                        {
                            dr["FILE_NAME"] = fileUploader.FileName;
                        }
                        dr["FILE_PATH"] = FILEPATH;
                        dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                        dt.Rows.Add(dr);
                        Session.Add("Attachments", dt);
                        this.BindListView_Attachments(dt);
                    }
                }
                else
                {
                    string Extension = "";
                    for (int i = 0; i < dsPURPOSE.Tables[0].Rows.Count; i++)
                    {
                        if (Extension == "")
                            Extension = dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                        else
                            Extension = Extension + ", " + dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                    }
                    objCommon.DisplayMessage("Upload Supported File Format.Please Upload File In " + Extension, this);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnSaveDD_Info_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private DataTable GetAttachmentDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ATTACH_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("FILE_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FILE_PATH", typeof(string)));
        dt.Columns.Add(new DataColumn("SIZE", typeof(int)));
        return dt;
    }

    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = Convert.ToInt32(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

            //to permanently delete from database in case of Edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                string count = objCommon.LookUp("ACD_ILECTURENOTES_ATTACHMENTS", "ATTACHMENT_ID", "NOTE_NO =" + Convert.ToInt32(ViewState["note_no"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ACD_ILECTURENOTES_ATTACHMENTS", "NOTE_NO =" + Convert.ToInt32(ViewState["note_no"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ATTACH_ID"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                Control ctrhead2 = lvCompAttach.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;

                }
            }
            else
            {



                Control ctrHeader = lvCompAttach.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;

                }
            }





        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //END

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/LectureNotes/" + "lectureNotes_" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        //HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }
    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameitledoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion
    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            //string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
