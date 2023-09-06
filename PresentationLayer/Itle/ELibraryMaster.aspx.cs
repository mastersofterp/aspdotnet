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

public partial class Itle_ELibraryMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BlobController objBlob = new BlobController();
    ILibrary objLib = new ILibrary();
    ILibraryController objIL = new ILibraryController();

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
                //if (Session["ICourseNo"] == null)
                //    Response.Redirect("selectCourse.aspx");

                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                BindListView();
                Session["Attachments"] = null;
                BlobDetails();
            }
        }

        // Used to get maximum size of file attachment
        GetAttachmentSize();
    }

    #endregion

    #region Private Method

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("ELibraryMaster.aspx");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
        }
    }

    private void ClearControls()
    {
        txtAuthor.Text = string.Empty;
        txtWebLinks.Text = string.Empty;
        txtBTitle.Text = string.Empty;
        txtPublisher.Text = string.Empty;
        //lblStatus.Text = string.Empty;
        lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblSession.ToolTip = Session["SessionNo"].ToString();
        // lblPreAttach.Text = string.Empty;
        ViewState["action"] = null;

        DataTable dt = null;
        lvCompAttach.DataSource = string.Empty;
        lvCompAttach.DataBind();
        BindListView_Attachments(dt);
        Session["Attachments"] = null;
    }

    private void BindListView()
    {
        try
        {
            ILibraryController objILB = new ILibraryController();
            ILibrary objLib = new ILibrary();

            objLib.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLib.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objLib.UA_NO = Convert.ToInt32(Session["userno"]);
            //DataSet ds = objILB.GetAllEbooksByUaNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));

            DataSet ds = objILB.GetAllEbooksByUaNo(objLib);
            lvLibrary.DataSource = ds;
            lvLibrary.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_ELibraryMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetStatus(object status)
    {
        if (status.ToString().Equals("Expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    private void ShowDetail(int book_no, int courseno, int sessionno, int ua_no)
    {
        try
        {


            ILibraryController objLC = new ILibraryController();
            ViewState["book_no"] = book_no;

            //used to access attachments
            DataSet ds = objLC.GetAllAtachmentByBookNo(book_no, courseno, sessionno, ua_no);
            DataTable dt = new DataTable();


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

            if (dt.Rows.Count > 0)
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



            DataTableReader dtr = objLC.GetSingleEBook(Convert.ToInt32(ViewState["book_no"]), courseno, sessionno, ua_no);

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //ViewState["assignno"] = int.Parse(dtr["AS_NO"].ToString());
                    txtBTitle.Text = dtr["BOOK_NAME"] == null ? "" : dtr["BOOK_NAME"].ToString();
                    txtAuthor.Text = dtr["AUTHOR_NAME"] == null ? "" : dtr["AUTHOR_NAME"].ToString();
                    txtPublisher.Text = dtr["PUBLISHER_NAME"] == null ? "" : dtr["PUBLISHER_NAME"].ToString();
                    txtWebLinks.Text = dtr["WEBSITE_LINK"] == null ? "" : dtr["WEBSITE_LINK"].ToString();
                    //hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    lblCurrdate.Text = dtr["UPLOAD_DATE"] == null ? "" : Convert.ToDateTime(dtr["UPLOAD_DATE"].ToString()).ToString("dd-MMM-yyyy");


                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_ELibraryMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "AddForum.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
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

           
            List<ELibraryAttachment> attachments = new List<ELibraryAttachment>();

            // USED TO ADD 'http://' befor weblink
            string website = string.Empty;

            string str = txtWebLinks.Text.Trim();

            if (str != "")
            {

                if (str.Contains("https://"))
                {
                    website = str;
                }
                else if (str.Contains("http://"))
                {
                    website = str;
                }
                else
                {
                    website = "http://" + str;
                }
            }

            string filename = string.Empty;
            objLib.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objLib.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objLib.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLib.AUTHOR_NAME = txtAuthor.Text;

            objLib.WEBSITE_LINK =
            objLib.WEBSITE_LINK = website;
            objLib.BOOK_NAME = txtBTitle.Text.Trim();
            objLib.PUBLISHER_NAME = txtPublisher.Text.Trim();
            objLib.UPLOAD_DATE = Convert.ToDateTime(lblCurrdate.Text);
            
           // objLib.ATTACHMENT = fuILib.FileName;

            if (lblBlobConnectiontring.Text == "")
            {
                objLib.ATTACHMENT = "0";
            }
            else
            {
                objLib.ATTACHMENT = "1";
            }

            objLib.COLLEGE_CODE = Session["colcode"].ToString();


            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                DataTable dt = ((DataTable)Session["Attachments"]);
                foreach (DataRow dr in dt.Rows)
                {
                    ELibraryAttachment attach = new ELibraryAttachment();
                    attach.AttachmentId = Convert.ToInt32(dr["ATTACH_ID"]);
                    attach.FileName = dr["FILE_NAME"].ToString();
                    attach.FilePath = dr["FILE_PATH"].ToString();
                    attach.Size = Convert.ToInt32(dr["SIZE"]);
                    attachments.Add(attach);
                }
            }
            objLib.Attachments = attachments;



            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                if (ViewState["book_no"] != null)
                    objLib.BOOK_NO = Convert.ToInt32(ViewState["book_no"]);
                objLib.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objLib.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                objLib.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
                objLib.AUTHOR_NAME = txtAuthor.Text;
                objLib.WEBSITE_LINK = website;
                objLib.BOOK_NAME = txtBTitle.Text.Trim();
                objLib.PUBLISHER_NAME = txtPublisher.Text.Trim();
                objLib.UPLOAD_DATE = Convert.ToDateTime(lblCurrdate.Text);
                //objLib.ATTACHMENT = fuILib.FileName;
                objLib.COLLEGE_CODE = Session["colcode"].ToString();
                //objLib.OLDFILENAME = hdnFile.Value;

                ELibraryAttachment attach = new ELibraryAttachment();
                attach.BOOK_NO = Convert.ToInt32(ViewState["book_no"]);

                CustomStatus cs = (CustomStatus)objIL.UpdateILibrary(objLib, Convert.ToInt32(ViewState["book_no"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(UpdLibrary, "Record Modified", this.Page);
                    ClearControls();
                    //lblStatus.Text = "Record Modified";
                    //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));                    
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayUserMessage(UpdLibrary, "Record Allready Exist", this.Page);
                }
                else
                    if (cs.Equals(CustomStatus.FileExists))
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
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
                    //  Add E-Book to Library
                    CustomStatus cs = (CustomStatus)objIL.AddILibrary(objLib);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(UpdLibrary, "e-Book added successfully.....", this.Page);
                        //lblStatus.Text = "....e-Book added successfully.";
                        ClearControls();
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayUserMessage(UpdLibrary, "e-Book Allready Exist.....", this.Page);
                    }
                    else
                        if (cs.Equals(CustomStatus.FileExists))
                            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                }
            }

            BindListView();
            ClearControls();
            Session["Attachments"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_ELibraryMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblStatus.Text = string.Empty;
        Session["Attachments"] = null;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int book_no = int.Parse(btnDel.CommandArgument);

            ILibraryController objLC = new ILibraryController();
            ILibrary objLib = new ILibrary();

            objLib.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objLib.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLib.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objLib.BOOK_NO = book_no;

            //CustomStatus cs = (CustomStatus)objLC.DeleteEBookByUaNo(Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["CourseNo"]), book_no);

            if (Convert.ToInt16(objLC.DeleteEBookByUaNo(objLib)) == Convert.ToInt16(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayUserMessage(UpdLibrary, "E-Book Deleted Successfully...", this.Page);
                //lblStatus.Text = "E-Book Deleted Successfully...";
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_ELibraryMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int book_no = int.Parse(btnEdit.CommandArgument);

            ShowDetail(book_no, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_ELibraryMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnViewELibrary_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_E-Library_Report", "Itle_ELibrary_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AddForum.btnViewForum_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
        // added by gayatri rode  condition of courceno 08-07-2022 
        dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text +"' and COURSENO='"+Convert.ToInt32(Session["ICourseNo"])+"'", "");
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

    public string GetFileName(object filename, object book_no)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/ILIBRARY/ILIBRARY_" + Convert.ToInt32(book_no) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/ILibrary/" + filename.ToString();
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

            GetAttachmentSize();

            string filename = string.Empty;
            string FILEPATH = string.Empty;

            if (fileUploader.HasFile)
            {
                string DOCFOLDER = file_path + "ITLE\\upload_files\\ILIBRARY";

                if (!System.IO.Directory.Exists(DOCFOLDER))
                {
                    System.IO.Directory.CreateDirectory(DOCFOLDER);

                }
                string fileName = System.Guid.NewGuid().ToString() + fileUploader.FileName.Substring(fileUploader.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);


                int SRNO = Convert.ToInt32(objCommon.LookUp("ACD_IELIBRARY_ATTACHMENTS", "(ISNULL(MAX(SR_NO),0))+1 AS SR_NO", ""));

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    DataTable dt1;
                    dt1 = ((DataTable)Session["Attachments"]);
                    int attachid = dt1.Rows.Count;

                    filename = SRNO + "_itleElibraryMaster_" + Session["userno"] + "-" + attachid;
                }
                else
                {
                    filename = SRNO + "_itleElibraryMaster_" + Session["userno"];
                }

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));


                DataSet dsPURPOSE = new DataSet();


                dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");
              
                if (count != 0)
                {

                    string filePath = file_path + "ITLE\\upload_files\\ILIBRARY\\" + fileName;

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

                           FILEPATH = file_path + "ITLE\\upload_files\\ILIBRARY\\" + fileName;
                            
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
                           // {
                                dr["ATTACH_ID"] = dt.Rows.Count + 1;
                               // dr["FILE_NAME"] = fileUploader.FileName;
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
                string count = objCommon.LookUp("ACD_IELIBRARY_ATTACHMENTS", "ATTACHMENT_ID", "BOOK_NO =" + Convert.ToInt32(ViewState["book_no"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ACD_IELIBRARY_ATTACHMENTS", "BOOK_NO =" + Convert.ToInt32(ViewState["book_no"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
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
        string filePath = file_path + "Itle/upload_files/ILIBRARY/" + "ILIBRARY_" + Convert.ToInt32(an_no);

        Response.ContentType = "Text/Doc";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        Response.End();

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

