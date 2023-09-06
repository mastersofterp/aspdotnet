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

using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;


public partial class ITLE_StudNotes : System.Web.UI.Page
{
    ILecture objLNote = new ILecture();
    ILectureNotesController objLN = new ILectureNotesController();
    BlobController objBlob = new BlobController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    string PageId;

    #region Page Load

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["session_name"].ToString();
                lblSession.ToolTip = Session["sessionno"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lblCurrdate.ForeColor = System.Drawing.Color.Green;
                //lblCurrdate.ForeColor = System.Drawing.Color.Green;
                BindListView();
                BlobDetails();
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));

            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                Response.Redirect("~/notauthorized.aspx?page=StudNotes.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudNotes.aspx");
        }
    }

    private void BindListView()
    {
        try
        {


            objLNote.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objLNote.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objLNote.UA_NO = Convert.ToInt32(Session["userno"]);

            DataSet ds = objLN.GetStudentNotes(objLNote);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvNotes.DataSource = ds;
                lvNotes.DataBind();
                DivLectureNotesList.Visible = true;
            }
            else
            {
                lvNotes.DataSource = ds;
                lvNotes.DataBind();
                DivLectureNotesList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_LectureMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }   
    }

    private void showDetail(int Lno)
    {
        objLNote.NOTE_NO = Convert.ToInt32(Lno);
        objLNote.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
        objLNote.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
        DataSet ds = objLN.GetStudentNotesBYLNo(objLNote);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblSubject.Text = ds.Tables[0].Rows[0]["TOPIC_NAME"].ToString();
            //txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            divDescription.InnerHtml = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            lblCurrdate.Text = ds.Tables[0].Rows[0]["CREATED_DATE"].ToString().Equals(string.Empty) ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");
        }
        //txtDescription.ReadOnly = true;

    }

    protected void GetAttachmentDown(int bookno)
    {

        try
        {
            ILibraryController objLC = new ILibraryController();
            DataSet ds;
            ds = objLC.GetEBookAttachments(bookno);

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvLDoc.DataSource = ds;
                lvLDoc.DataBind();
            }
            else
            {
                objCommon.DisplayMessage("Data Not Found...", this.Page);


            }
        }
        catch (Exception ex)
        {

        }

    }

    #endregion

    #region Page Events

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAssignDetail.Visible = false;
        DivLectureNotesList.Visible = true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        pnlAssignDetail.Visible = true;
        DivLectureNotesList.Visible = false;
        LinkButton btnEdit = sender as LinkButton;
        int Lno = int.Parse(btnEdit.CommandArgument);
        showDetail(Lno);
        GetAttachmentByNoteNo(Lno);

    }

    protected void lnkbtnDownload_Click(object sender, EventArgs e)
    {
        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/LectureNotes/" + "LectureNotes_" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    #endregion

    #region Public Methods

    public string GetFileName(object filename, object assingmentno)
    {
        if (filename != null && filename.ToString() != "")
            //return filename.ToString();
            //  return "assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/upload_files/LectureNotes/lectureNotes_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/LectureNotes/" + filename.ToString();
        else
            return "";
    }

    //USED TO GET ALL ATTACHMENTS FROM LECTURE NOTES
    void GetAttachmentByNoteNo(int noteno)
    {

        try
        {
            DataSet ds;
            ds = objLN.GetLectureNotesAttachments(noteno);
            lvAttachments.DataSource = ds;
            lvAttachments.DataBind();


            DataSet DS1 = objCommon.FillDropDown("ACD_ILECTURENOTES", "ATTACHMENT", "", "NOTE_NO=" + noteno, "");
            string blob = DS1.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            if (blob == "1")
            {
                foreach (ListViewItem lvRow in lvAttachments.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckBox1 = (Control)lvRow.FindControl("tdDownloadLink");

                    ckBox.Visible = true;
                    ckBox1.Visible = true;
                }
            }
            else
            {
              
                foreach (ListViewItem lvRow in lvAttachments.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownload");
                    
                    ckBox.Visible = true;
                }
            
            }
        }
        catch (Exception ex)
        {

        }

    }


    void GetAttachmentLectureNotes(int noteno)
    {

        try
        {
            DataSet ds;
            ds = objLN.GetLectureNotesAttachments(noteno);
            lvLDoc.DataSource = ds;
            lvLDoc.DataBind();

        }
        catch (Exception ex)
        {

        }

    }

    #endregion
    protected void btnimgdow_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtndow = sender as ImageButton;
        int Lno = int.Parse(imgbtndow.CommandArgument);

        GetAttachmentLectureNotes(Lno);
        upd_ModalPopupExtender1.Show();
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
