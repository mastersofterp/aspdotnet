using DynamicAL_v2;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
public partial class ACADEMIC_CopyNpfImageToErp : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmissionController objOAC = new OnlineAdmissionController();
    Student objstud = new Student();
    DynamicControllerAL AL = new DynamicControllerAL();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                CheckPageAuthorization();
                PopulateDropDownList();
            }
        }



    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CopyNpfImageToErp.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CopyNpfImageToErp.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
        txtTotStud.Text = string.Empty;
    }

    public void BindListView()
    {
        try
        {
            DataSet ds = objOAC.GetStudentPhotoInfoByNPF(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlType.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
            }
            else
            {
                lvStudents.Visible = false;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.updpnl, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public byte[] ResizePhoto(byte[] bytes)
    {
        byte[] image = null;
        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(bytes);

        System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(myMemStream);

        int imageHeight = imageToBeResized.Height;
        int imageWidth = imageToBeResized.Width;
        int maxHeight = 240;
        int maxWidth = 320;
        imageHeight = (imageHeight * maxWidth) / imageWidth;
        imageWidth = maxWidth;

        if (imageHeight > maxHeight)
        {
            imageWidth = (imageWidth * maxHeight) / imageHeight;
            imageHeight = maxHeight;
        }

        // Saving image to smaller size and converting in byte[]
        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
        System.IO.MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        stream.Position = 0;
        image = new byte[stream.Length + 1];
        stream.Read(image, 0, image.Length);
        return image;
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    public static byte[] ReduceImage(byte[] bytes, Size size)
    {
        MemoryStream memoryStream = new MemoryStream(bytes);
        memoryStream.Seek(0, SeekOrigin.Begin);
        Bitmap originalImage = new Bitmap(memoryStream);
        int sourceWidth = size.Width;
        //Get the image current height  
        int sourceHeight = size.Height;
        var resized = new Bitmap(sourceWidth, sourceHeight);
        Graphics graphics = Graphics.FromImage(resized);
        graphics.CompositingQuality = CompositingQuality.HighSpeed;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.DrawImage(originalImage, 0, 0, size.Width, size.Height);
        MemoryStream stream = new MemoryStream();
        resized.Save(stream, ImageFormat.Jpeg);
        return stream.ToArray();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int admbatch = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
        int type = Convert.ToInt32(ddlType.SelectedValue);
        foreach (ListViewDataItem itm in lvStudents.Items)
        {
            CheckBox chk = itm.FindControl("cbRow") as CheckBox;

            int userno = Convert.ToInt32(chk.ToolTip);
            if (chk.Checked.Equals(true))
            {

                using (WebClient webClient = new WebClient())
                {

                    DataSet ds = objOAC.GetStudentPhotoInfoByNPF(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlType.SelectedValue));
                    ds.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
                    DataTable dt = (ds.Tables[0].DefaultView).ToTable();
                    string path = dt.Rows[0]["PHOTO"].ToString();
                    byte[] data = webClient.DownloadData(path);
                    // byte[] data = webClient.DownloadData("https://jecrcuapplication.jecrcuniversity.edu.in/uploads/student/document/198/1583/2022/06/08/62a0546c679b0386453495_image.jpg");
                    //int type = 1;

                    //Image Conversion
                    int bytes = data.Length;
                    double kilobytes = bytes / 1024.0;
                    if (kilobytes > 150)
                    {
                        data = ResizePhoto(data);
                        int fileSize2 = data.Length;
                    }
                    //END
                    objstud.IdNo = userno;
                    objstud.StudPhoto = data;
                    CustomStatus cs = (CustomStatus)objOAC.UpdateStudPhotoFromNpf(objstud, type);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updpnl, "Photo uploaded Successfully!!", this.Page);
                        BindListView();
                    }
                }

            }
        }
    }


    public void downloaduploadimage()
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData("https://jecrcuapplication.jecrcuniversity.edu.in/uploads/student/document/198/1583/2022/06/08/62a0546c679b0386453495_image.jpg");
                int type = 1;
                objstud.IdNo = 4;
                objstud.StudPhoto = data;
                CustomStatus cs = (CustomStatus)objOAC.UpdateStudPhotoFromNpf(objstud, type);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //objCommon.DisplayMessage(this.updpersonalinformation, "Photo uploaded Successfully!!", this.Page);
                    //showstudentphoto();

                }


            }
        }
        catch { }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        if (ddlType.SelectedIndex == 1 || ddlType.SelectedIndex == 2)
        {
            btnSubmit.Visible = true;
            btnSubmitDocument.Visible = false;
        }
        else
        {
            btnSubmit.Visible = false;
            btnSubmitDocument.Visible = true;
        }
    }
    protected void btnSubmitDocument_Click(object sender, EventArgs e)
    {

        string documentno = objCommon.LookUp("ACD_DOCUMENT_LIST", "TOP 1 DOCUMENTNO", "DOCUMENTNAME='" + Convert.ToString(ddlType.SelectedItem.Text) + "'");

        int admbatch = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
        int type = Convert.ToInt32(ddlType.SelectedValue);
        foreach (ListViewDataItem itm in lvStudents.Items)
        {
            CheckBox chk = itm.FindControl("cbRow") as CheckBox;

            int userno = Convert.ToInt32(chk.ToolTip);
            if (chk.Checked.Equals(true))
            {


                DataSet ds = objOAC.GetStudentPhotoInfoByNPF(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlType.SelectedValue));
                ds.Tables[0].DefaultView.RowFilter = "USERNO = " + userno;
                DataTable dt = (ds.Tables[0].DefaultView).ToTable();

                string file = dt.Rows[0]["PHOTO"].ToString();
                string username = dt.Rows[0]["USERNAME"].ToString();
                string ext = file.Split('.').Last();
                byte[] data;
                using (WebClient webClient = new WebClient())
                {
                    data = webClient.DownloadData(file);

                }
                string filename = username + "_" + userno + "_" + documentno + "." + ext;
                //byte[] bytes = System.IO.File.ReadAllBytes(localPath);
                Blob_Upload(blob_ConStr, blob_ContainerName, filename, data);
                //objCommon.DisplayMessage(this.updpnl, documentno.ToString(), this.Page);
                int result = objOAC.UpdateDocument(userno, Convert.ToInt32(documentno), filename);
                if (result == 1)
                {
                    objCommon.DisplayMessage(this.updpnl, "Document uploaded Successfully!!", this.Page);
                    BindListView();
                }
            }
        }
    }



    public int Blob_Upload(string ConStr, string ContainerName, string filename, byte[] data)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        //string Ext = Path.GetExtension(FU.FileName);
        string FileName = filename;
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
                using (Stream stream = new MemoryStream(data))
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

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

}
