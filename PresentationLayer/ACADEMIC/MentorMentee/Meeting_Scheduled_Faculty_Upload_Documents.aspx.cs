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
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using ClosedXML.Excel;

public partial class ACADEMIC_MentorMentee_Meeting_Scheduled_Faculty_Upload_Documents : System.Web.UI.Page
{
    Common objCommon = new Common();
    MeetingScheduleMaster objMM = new MeetingScheduleMaster();
    Schedule_MeetingController OBJmc = new Schedule_MeetingController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    public static int dec_id;

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
                    BindListView();
                    objCommon.FillDropDownList(ddlCommitee, "ACD_MEETING_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
         {
             if (Session["usertype"].ToString() == "3")
             {
                 
                objCommon.FillDropDownList(ddlMeeting, "ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_COMITEE C ON (A.FK_MEETING = C.ID)", "A.PK_AGENDA", "(CONVERT(nvarchar, A.MEETINGDATE, 101) + ' ' + A.MEETINGTIME + ' ' + A.MEETINGTOTIME)", "ISNULL(A.ACTIVE_STATUS, 0) = 1  AND A.FK_MEETING = " + Convert.ToInt32(ddlCommitee.SelectedValue), "");
                 
             }
             else
             {
                     
                 objCommon.FillDropDownList(ddlMeeting, "ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_COMITEE C ON (A.FK_MEETING = C.ID)", "A.PK_AGENDA", "(CONVERT(nvarchar, A.MEETINGDATE, 101) + ' ' + A.MEETINGTIME + ' ' + A.MEETINGTOTIME)", "ISNULL(A.ACTIVE_STATUS, 0) = 1  AND A.FK_MEETING = " + Convert.ToInt32(ddlCommitee.SelectedValue), "");

             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 this.objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SendSmstoParents.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
             else
                 this.objCommon.ShowError(Page, "Server Unavailable.");
         }
    }

    #region BlogStorage


    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }


    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    #endregion BlogStorage

    private void Clear()
    {

        txtDescription.Text = string.Empty;
        lblfile.Text = string.Empty;     
        ddlMeeting.SelectedIndex = 0;
        ddlCommitee.SelectedIndex = 0;
        ViewState["action"] = "add";
      
    }
    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            dec_id = int.Parse(btnEdit.CommandArgument);
            ViewState["dec_id"] = int.Parse(btnEdit.CommandArgument);

            DataSet ds = objCommon.FillDropDown("ACD_MEETING_DESCRIPTION_DOCUMENTS", "*", "", "DEC_ID=" + dec_id + " ", "");

            int DEC_ID = Convert.ToInt32(ViewState["dec_id"].ToString());

            ViewState["action"] = "edit";

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ViewState["dec_id"] = dec_id;
                    ddlCommitee.SelectedValue = dr["COMMITEEID"].ToString();
                    ddlMeeting.SelectedValue = dr["PK_AGENDA"].ToString();
                    txtDescription.Text = Convert.ToString(dr["DESCRIPT_ION"]);                    
                    lblfile.Text = dr["FILE_NAME"] == null ? "" : dr["FILE_NAME"].ToString();
                }
            }          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Scheduled_Faculty_Upload_Documents.aspx.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string FILE_NAME = string.Empty;
            objMM.UA_Userno = Convert.ToInt32(Session["userno"]);
            objMM.COMMITEEID = Convert.ToInt32(ddlCommitee.SelectedValue);
            objMM.PK_AGENDA_ID = Convert.ToInt32(ddlMeeting.SelectedValue);
            objMM.DESCRIPTION = txtDescription.Text;

            if (ViewState["action"].ToString().Equals("add"))
            {
                int newid = Convert.ToInt32(objCommon.LookUp("ACD_MEETING_DESCRIPTION_DOCUMENTS", "ISNULL(MAX(DEC_ID),0)", ""));
                ViewState["dec_id"] = newid + 1;
            }

            byte[] imgData;
            if (fuDescription.HasFile)
            {
                if (!fuDescription.PostedFile.ContentLength.Equals(string.Empty) || fuDescription.PostedFile.ContentLength != null)
                {
                    int fileSize = fuDescription.PostedFile.ContentLength;
                    string ext = System.IO.Path.GetExtension(fuDescription.FileName).ToLower();
                    int KB = fileSize / 1024;

                    if (ext == ".pdf")
                    {
                        if (KB >= 500 && ext != ".pdf")
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);
                            return;
                        }
                        else if (KB >= 5120 && ext == ".pdf")
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 5 mb.", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Upload PDF file only", this.Page);
                        return;
                    }

                    if (fuDescription.FileName.ToString().Length > 50)
                    {
                        objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                        return;
                    }

                    imgData = objCommon.GetImageData(fuDescription);
                    FILE_NAME = imgData.ToString();
                    string filename_Certificate = Path.GetFileName(fuDescription.PostedFile.FileName);

                    FILE_NAME = (ViewState["dec_id"]) + "_Meeting_Description_Documents_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                    int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["dec_id"]) + "_Meeting_Description_Documents_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fuDescription, imgData);
                    if (retval == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);

                        return;
                    }

                }
            }
            else if (!fuDescription.HasFile && lblfile.Text != string.Empty)
            {
                FILE_NAME = lblfile.Text;
            }
                   
            if (ViewState["action"].ToString().Equals("add"))
            {
                if (OBJmc.AddUpdate_Meeting_Schedule_Documents(objMM, FILE_NAME) != 0)
                {
                    BindListView();
                    ViewState["action"] = "add";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                    Clear();
                }
            }         
            else
            {
                objMM.DEC_ID = Convert.ToInt32(ViewState["dec_id"]);
                CustomStatus cs = (CustomStatus)OBJmc.AddUpdate_Meeting_Schedule_Documents(objMM, FILE_NAME);
                if (OBJmc.AddUpdate_Meeting_Schedule_Documents(objMM, FILE_NAME) != 0)
                {
                    BindListView();
                    ViewState["action"] = "add";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                    Clear();
                }
            }
        }      
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindListView()
    {
        int id = Convert.ToInt32(ViewState["dec_id"]);

        DataSet ds = OBJmc.GetDocumentList(id);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvDescription.Visible = true;
            pnlDescription.Visible = true;
            lvDescription.DataSource = ds;
            lvDescription.DataBind();
        }
        else
        {
            lvDescription.Visible = false;
            pnlDescription.Visible = false;
            lvDescription.DataSource = null;
            lvDescription.DataBind();
        }
    }

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    
    {
        ////Added By Prafull
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
            if (img != null || img != "")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";

                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                BindListView();
            }
        }
        catch (Exception ex)
        {
        }
    }
}

