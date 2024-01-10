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


public partial class Itle_StudLibrary : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    BlobController objBlob = new BlobController();

    ILibraryController objLC = new ILibraryController();
    ILibrary objLib = new ILibrary();
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
               
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                //pnlAnnounce.Visible = true;
                //pnlText.Visible = false;

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                //lblLastdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //lblCourseName.Text = Session["ICourseName"].ToString();
                BindListView();
                BlobDetails();
                // Used to insert id,date and courseno in Log_History table
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
            }
        }
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_StudLibrary");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_StudLibrary");
        }
    }

    private void BindListView()
    {
        try
        {
            objLib.SESSIONNO = Convert.ToInt32(Session["SessionNo"]);
            objLib.COURSENO = Convert.ToInt32(Session["ICourseno"]);
            objLib.UA_NO = 0;

            DataSet ds = objLC.GetAllEbooksByUaNo(objLib);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLibrary.DataSource = ds;
                lvLibrary.DataBind();
                DivLibraryList.Visible = true;
            }
            else
            {
                lvLibrary.DataSource = null;
                lvLibrary.DataBind();
                DivLibraryList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudLibrary.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void showDetail(int Lno)
    {
        objLib.BOOK_NO = Convert.ToInt32(Lno);
        objLib.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
        objLib.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
        DataSet ds = objLC.GetStudentEbookByBookNo(objLib);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblBookName.Text = ds.Tables[0].Rows[0]["BOOK_NAME"].ToString();
            lblAuthorName.Text = ds.Tables[0].Rows[0]["AUTHOR_NAME"].ToString();
            lblPublisher.Text = ds.Tables[0].Rows[0]["PUBLISHER_NAME"].ToString();
        }

    }

    #endregion

    #region Attatchments

    public string GetFileName(object filename, object book_no)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/UPLOAD_FILES/ILIBRARY/ILIBRARY_" + Convert.ToInt32(book_no) + System.IO.Path.GetExtension(filename.ToString());
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

    public string GetStatus(object status)
    {
        if (status.ToString().Equals("Expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    //USED TO GET ALL ATTACHMENTS FROM LECTURE NOTES
    void GetAttachmentByNoteNo(int bookno)
    {

        try
        {
            ILibraryController objLC = new ILibraryController();
            DataSet ds;
            ds = objLC.GetEBookAttachments(bookno);
            lvAttachments.DataSource = ds;
            lvAttachments.DataBind();

             DataSet DS1 = objCommon.FillDropDown("ACD_ILIBRARY", "ATTACHMENT", "", "BOOK_NO=" + bookno, "");
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


    void GetAttachmentDown(int bookno)
    {

        try
        {
            ILibraryController objLC = new ILibraryController();
            DataSet ds;
            ds = objLC.GetEBookAttachments(bookno);

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvDoc.DataSource = ds;
                lvDoc.DataBind();
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

    protected void btnEdit_Click(object sender, EventArgs e)
    {


        LinkButton btnEdit = sender as LinkButton;
        int Lno = int.Parse(btnEdit.CommandArgument);
        showDetail(Lno);
        GetAttachmentByNoteNo(Lno);
        tblBookInfo.Visible = true;
        DivLibraryList.Visible = false;
        //showDetail(Lno);
        

    }

    //To DownLoad File
    protected void lnkbtnDownload_Click(object sender, EventArgs e)
    {
        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

        LinkButton lnkbtn = sender as LinkButton;

        int book_no = int.Parse(lnkbtn.CommandArgument);
        
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/ILIBRARY/" + "ILIBRARY_" + Convert.ToInt32(book_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        tblBookInfo.Visible = false;
        DivLibraryList.Visible = true;
    }

    #endregion
    protected void imgbtndow_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int Lno = int.Parse(btnEdit.CommandArgument);
        GetAttachmentDown(Lno);
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
                embed += "If you are unable to view file, you can download from <a target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";

            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath  + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                hdnfilename.Value = filePath;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BTNCLOSEDOC_Click(object sender, EventArgs e)
    {
        string directoryPath = Server.MapPath("~/DownloadImg/");

        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                if (file == hdnfilename.Value)
                {
                    File.Delete(file);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                }
            }
        }
    }
}
