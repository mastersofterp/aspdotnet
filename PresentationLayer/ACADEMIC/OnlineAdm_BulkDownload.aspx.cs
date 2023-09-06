using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
public partial class ACADEMIC_OnlineAdm_BulkDownload : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
    string spName = string.Empty; string spParameters = string.Empty; string spValue = string.Empty;
    int admBatch = 0; int degreeType = 0; int degree = 0; int branch = 0; DataSet dsDdl = null;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString_CRESCENT"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_CRESCENT"].ToString();
    string FileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    PopulateDropDown();
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlineAdm_BulkDownload.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlineAdm_BulkDownload.aspx");
        }
    }
    protected void btnExtract_Click(object sender, EventArgs e)
    {
        try
        {
            string Url = string.Empty;
            string directoryPath = string.Empty; string filePathBulk = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadZipFile_Bulk" + "/";
            directoryPath = Server.MapPath(directoryName);
            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }
            System.IO.DirectoryInfo di = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = "";
            admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
            degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
            degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            string path = string.Empty;
            spName = "PKG_ACD_OA_GET_USER_BY_DEGREE_TYPE_BULK_ZIP_DOC";
            spParameters = "@P_DEGREE_TYPE,@P_ADMBATCH,@P_DEGREE,@P_BRANCH";
            spValue = "" + degreeType + "," + admBatch + "," + degree + "," + branch + "";
            DataSet dsStud = null;
            string usernos = string.Empty;
            string filePath = string.Empty;
            string docNo = string.Empty;
            dsStud = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsStud.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsStud.Tables[0].Rows.Count; i++)
                {
                    img = dsStud.Tables[0].Rows[i]["DOC_FILENAME"].ToString();
                    usernos += dsStud.Tables[0].Rows[i]["USERNO"].ToString() + "$";
                    if (ddlDegreeType.SelectedValue == "3")
                    {
                        docNo += dsStud.Tables[0].Rows[i]["DOCUMENT_NO"].ToString() + "$"; ;
                    }
                    var ImageName = img;
                    if (img != null || img != "")
                    {

                        DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                        var Newblob = blobContainer.GetBlockBlobReference(ImageName);
                        filePath = directoryPath + "\\" + ImageName;

                        //if ((System.IO.File.Exists(filePath)))
                        //{
                        //    System.IO.File.Delete(filePath);
                        //}
                        Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                        filePathBulk += filePath.ToString() + "$";
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                return;
            }
            if (ddlDegreeType.SelectedValue == "1" || ddlDegreeType.SelectedValue == "2")
            {
                UpdateZipLocFile(usernos, admBatch, filePathBulk);
            }
            else if (ddlDegreeType.SelectedValue == "3")
            {
                UpdateZipLocFile_PhD(usernos, admBatch, filePathBulk, docNo);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Server Unavailable.`)", true);
                return;
            }
            string Type = string.Empty;
            spName = "PKG_ACD_OA_GET_USER_BY_DEGREE_TYPE_BULK_ZIP_DOC";
            spParameters = "@P_DEGREE_TYPE,@P_ADMBATCH,@P_DEGREE,@P_BRANCH";
            spValue = "" + degreeType + "," + admBatch + "," + degree + "," + branch + "";
            DataSet dsStud_Upd = null;
            dsStud_Upd = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsStud_Upd.Tables[0].Rows.Count > 0)
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    // FIRST DELETE PREVIOUS FILES...
                    //Array.ForEach(Directory.GetFiles(directoryPath), File.Delete);
                    // .......................
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //zip.AddDirectoryByName("files");

                    foreach (DataTable table in dsStud_Upd.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {

                                string filename, filepath, docname;
                                //filename = row["FILENAME"].ToString();
                                filepath = row["ZIP_PATH_BULK"].ToString();
                                docname = row["DOC_FILENAME"].ToString();
                                string filePath_ForBulk = filepath;
                                zip.AddFile(filePath_ForBulk, row["USERNAME"].ToString());
                                //zip.AddFile(DirPath, row["REGNO"].ToString());
                            }
                            catch (Exception ex)
                            {
                                throw new IITMSException("IITMS.UAIMS.showimage->" + ex.ToString());
                            }
                        }
                    }
                    Response.Clear();
                    Response.BufferOutput = false;
                    string ZipFileName = string.Empty;
                    if (ddlBatch.SelectedIndex > 0)
                    {
                        ZipFileName += ddlBatch.SelectedItem.Text + "_";
                    }
                    else
                    {
                        ZipFileName += "";
                    }
                    if (ddlDegreeType.SelectedIndex > 0)
                    {
                        ZipFileName += ddlDegreeType.SelectedItem.Text + "_";
                    }
                    else
                    {
                        ZipFileName += "";
                    }
                    if (ddlDegree.SelectedIndex > 0)
                    {
                        ZipFileName += ddlDegree.SelectedItem.Text + "_";
                    }
                    else
                    {
                        ZipFileName += "";
                    }
                    if (ddlBranch.SelectedIndex > 0)
                    {
                        ZipFileName += ddlBranch.SelectedItem.Text + "_";
                    }
                    else
                    {
                        ZipFileName += "";
                    }
                    ZipFileName = ZipFileName.Substring(0, ZipFileName.LastIndexOf("_"));
                    //string zipName = String.Format("Zip_{0}.zip", ZipFileName.Replace(' ', '_'));
                    //string zipName = "ZipFile.zip";
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + ZipFileName + ".zip");
                    zip.Save(Response.OutputStream);

                    Response.End();
                }
            }
            //int fileCount = 0;
            //string compressedFileName = ddlBatch.SelectedItem.Text + "_" + ddlDegreeType.SelectedItem.Text + "_" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text;
            //compressedFileName=compressedFileName.Substring(0, compressedFileName.LastIndexOf("_"));
            //string zipPath = directoryPath;
            //if (System.IO.File.Exists(filePath))
            //{
            //    fileCount++;
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + compressedFileName.Replace(" ", "_") + ".zip");
            //    Response.ContentType = "application/zip";
            //}
            //if (fileCount > 0)
            //{

            //    using (var zipStream = new ZipOutputStream(Response.OutputStream))
            //    {

            //        //foreach (DataRow dr in dsStud.Tables[0].Rows)
            //            for(int j=0;j<dsStud.Tables[0].Rows.Count;j++)
            //            {
            //            //string fileP = dsStud.Tables[0].Rows[j][].ToString();
            //            string FILENAME = dsStud.Tables[0].Rows[j]["DOC_FILENAME"].ToString();

            //            if (System.IO.File.Exists(filePath))
            //            {
            //                fileCount++;
            //                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            //                Stream fs = File.OpenRead(filePath);
            //                ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(FILENAME));
            //                zipEntry.Size = fs.Length;
            //                zipStream.PutNextEntry(zipEntry);
            //                int count = fs.Read(fileBytes, 0, fileBytes.Length);
            //                while (count > 0)
            //                {
            //                    zipStream.Write(fileBytes, 0, count);
            //                    count = fs.Read(fileBytes, 0, fileBytes.Length);
            //                    if (!Response.IsClientConnected)
            //                    {
            //                        break;
            //                    }
            //                    Response.Flush();
            //                }
            //                fs.Close();
            //            }
            //        }
            //        zipStream.Close();
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
        }
        catch (Exception)
        {
            throw;
        }
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown();
            PopulateDropDown();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ClearDropDown()
    {
        try
        {
            ddlBatch.Items.Clear();
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlDegreeType.Items.Clear();
            ddlDegreeType.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void PopulateDropDown()
    {
        try
        {
            admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
            degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
            degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            spName = "PKG_ACD_OA_GET_DROPDOWN_DOWNLOAD_BULK_EXTRACT";
            spParameters = "@P_ADMBATCH,@P_DEGREE_TYPE,@P_DEGREE";
            spValue = "" + admBatch + "," + degreeType + "," + degree + "";
            dsDdl = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsDdl.Tables[0].Rows.Count > 0)
            {
                ddlBatch.Items.Clear();
                ddlBatch.Items.Add(new ListItem("Please Select", "0"));
                ddlBatch.DataSource = dsDdl.Tables[0];
                ddlBatch.DataTextField = "BATCHNAME";
                ddlBatch.DataValueField = "BATCHNO";
                ddlBatch.DataBind();
                ddlBatch.Focus();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBatch.SelectedIndex > 0)
            {
                admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
                degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
                degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
                branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
                spName = "PKG_ACD_OA_GET_DROPDOWN_DOWNLOAD_BULK_EXTRACT";
                spParameters = "@P_ADMBATCH,@P_DEGREE_TYPE,@P_DEGREE";
                spValue = "" + admBatch + "," + degreeType + "," + degree + "";
                dsDdl = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                if (dsDdl.Tables[1].Rows.Count > 0)
                {
                    ddlDegreeType.Items.Clear();
                    ddlDegreeType.Items.Add(new ListItem("Please Select", "0"));
                    ddlDegreeType.DataSource = dsDdl.Tables[1];
                    ddlDegreeType.DataTextField = "UA_SECTIONNAME";
                    ddlDegreeType.DataValueField = "UA_SECTION";
                    ddlDegreeType.DataBind();

                    ddlDegreeType.Items.Add(new ListItem("NRI", "4"));
                    ddlDegreeType.Items.Add(new ListItem("Lateral", "5"));
                    ddlDegreeType.Focus();
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ddlDegreeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegreeType.SelectedIndex > 0)
            {
                admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
                degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
                degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
                branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
                spName = "PKG_ACD_OA_GET_DROPDOWN_DOWNLOAD_BULK_EXTRACT";
                spParameters = "@P_ADMBATCH,@P_DEGREE_TYPE,@P_DEGREE";
                spValue = "" + admBatch + "," + degreeType + "," + degree + "";
                dsDdl = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                if (dsDdl.Tables[2].Rows.Count > 0)
                {
                    ddlDegree.Items.Clear();
                    ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                    ddlDegree.DataSource = dsDdl.Tables[2];
                    ddlDegree.DataTextField = "DNAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataBind();
                    ddlDegree.Focus();
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
                degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
                degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
                branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
                spName = "PKG_ACD_OA_GET_DROPDOWN_DOWNLOAD_BULK_EXTRACT";
                spParameters = "@P_ADMBATCH,@P_DEGREE_TYPE,@P_DEGREE";
                spValue = "" + admBatch + "," + degreeType + "," + degree + "";
                dsDdl = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                if (dsDdl.Tables[3].Rows.Count > 0)
                {
                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                    ddlBranch.DataSource = dsDdl.Tables[3];
                    ddlBranch.DataTextField = "BRANCHNAME";
                    ddlBranch.DataValueField = "BRANCHNO";
                    ddlBranch.DataBind();
                    ddlBranch.Focus();
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void UpdateZipLocFile(string usernos, int admbatch, string path_Bulk)
    {
        try
        {
            spName = "";
            spParameters = "";
            spValue = "";
            spName = "PKG_ACD_OA_UPDATE_PATH_FOR_BULK_USERS";
            spParameters = "@P_USERNOS,@P_ADMBATCH,@P_PATH_BULK";
            spValue = "" + usernos + "," + admBatch + "," + path_Bulk + "";
            DataSet dsUpd = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsUpd.Tables[0].Rows.Count > 0)
            {
                if (dsUpd.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {

                }
            }
            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void UpdateZipLocFile_PhD(string usernos, int admbatch, string path_Bulk, string docNo)
    {
        try
        {
            spName = "";
            spParameters = "";
            spValue = "";
            spName = "PKG_ACD_OA_UPDATE_PATH_FOR_PHD_BULK_USERS";
            spParameters = "@P_USERNOS,@P_ADMBATCH,@P_PATH_BULK,@P_DOCNO";
            spValue = "" + usernos + "," + admBatch + "," + path_Bulk + "," + docNo + "";
            DataSet dsUpd = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsUpd.Tables[0].Rows.Count > 0)
            {
                if (dsUpd.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {

                }
            }
            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnPhotoExtract_Click(object sender, EventArgs e)
    {
        FileName = "USERNAME";
        string Type = "PHOTO";
        admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
        degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
        degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        string path = string.Empty;
        spName = "PKG_ACD_OA_GET_USER_PHOTO_BY_DEGREE_TYPE_BULK";
        spParameters = "@P_DEGREE_TYPE,@P_ADMBATCH,@P_DEGREE,@P_BRANCH";
        spValue = "" + degreeType + "," + admBatch + "," + degree + "," + branch + "";
        DataSet dsStud = null;

        dsStud = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
        string FolderName = ddlBatch.SelectedItem.Text + '_' + ddlDegreeType.SelectedItem.Text + '_' + Type;
        if (!Directory.Exists("C://BULK_PHOTO_EXTRACT//" + FolderName))
        {
            Directory.CreateDirectory("C://BULK_PHOTO_EXTRACT//" + FolderName);
        }

        string DirPath = "C://BULK_PHOTO_EXTRACT//" + FolderName + "//";
        if (Directory.Exists(DirPath))
        {
            if (dsStud.Tables[0].Rows.Count > 0)
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    // FIRST DELETE PREVIOUS FILES...
                    Array.ForEach(Directory.GetFiles(DirPath), File.Delete);
                    // .......................
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("files");
                    foreach (DataTable table in dsStud.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                byte[] imgData = null;
                                imgData = row["" + Type + ""] as byte[];
                                if (imgData.Length > 0 && imgData != null)
                                {
                                    MemoryStream memStream = new MemoryStream(imgData);
                                    System.Drawing.Bitmap.FromStream(memStream).Save(DirPath + row[FileName].ToString() + ".jpg");//.jpg
                                    string filename;
                                    filename = row[FileName].ToString() + ".jpg";
                                    string filePath = DirPath + filename;
                                    zip.AddFile(filePath, "files");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }

                    Response.Clear();
                    Response.BufferOutput = false;
                    // string ZipFileName = ddlDegree.SelectedItem.Text.Trim() + "_" + ddlAdmissionBatch.SelectedItem.Text.Trim() + "_" + (ddlBranch.SelectedValue == "0" ? "" : ddlBranch.SelectedItem.Text.Trim()) + "_" + (ddlSem.SelectedValue == "0" ? "" : ddlSem.SelectedItem.Text.Trim()) + "_" + (ddlStudentType.SelectedValue == "0" ? "" : ddlStudentType.SelectedItem.Text.Trim()) + "_" + Type;
                    string ZipFileName = ddlBatch.SelectedItem.Text + '_' + ddlDegreeType.SelectedItem.Text + '_' + Type;
                    string zipName = String.Format("Zip_{0}.zip", ZipFileName.Replace(' ', '_'));
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);

                    this.DeleteDirectory(DirPath);
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updManual, "Record not found.", this.Page);
            }
        }
    }

    private void DeleteDirectory(string path)
    {
        // Delete all files from the Directory  
        foreach (string filename in Directory.GetFiles(path))
        {
            File.Delete(filename);
        }
        // Check all child Directories and delete files  
        foreach (string subfolder in Directory.GetDirectories(path))
        {
            DeleteDirectory(subfolder);
        }
        string directory = "C://BULK_PHOTO_EXTRACT";
        Directory.Delete(path);
        Directory.Delete(directory, true);
    }
    protected void btnSignExtract_Click(object sender, EventArgs e)
    {
        FileName = "USERNAME";
        string Type = "SIGN";
        admBatch = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
        degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
        degree = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        branch = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        string path = string.Empty;
        spName = "PKG_ACD_OA_GET_USER_SIGN_BY_DEGREE_TYPE_BULK";
        spParameters = "@P_DEGREE_TYPE,@P_ADMBATCH,@P_DEGREE,@P_BRANCH";
        spValue = "" + degreeType + "," + admBatch + "," + degree + "," + branch + "";
        DataSet dsStud = null;

        dsStud = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
        string FolderName = ddlBatch.SelectedItem.Text + '_' + ddlDegreeType.SelectedItem.Text + '_' + Type;
        if (!Directory.Exists("C://BULK_PHOTO_EXTRACT//" + FolderName))
        {
            Directory.CreateDirectory("C://BULK_PHOTO_EXTRACT//" + FolderName);
        }

        string DirPath = "C://BULK_PHOTO_EXTRACT//" + FolderName + "//";
        if (Directory.Exists(DirPath))
        {
            if (dsStud.Tables[0].Rows.Count > 0)
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    // FIRST DELETE PREVIOUS FILES...
                    Array.ForEach(Directory.GetFiles(DirPath), File.Delete);
                    // .......................
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("files");
                    foreach (DataTable table in dsStud.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            try
                            {
                                byte[] imgData = null;
                                imgData = row["" + Type + ""] as byte[];
                                if (imgData.Length > 0 && imgData != null)
                                {
                                    MemoryStream memStream = new MemoryStream(imgData);
                                    System.Drawing.Bitmap.FromStream(memStream).Save(DirPath + row[FileName].ToString() + ".jpg");//.jpg
                                    string filename;
                                    filename = row[FileName].ToString() + ".jpg";
                                    string filePath = DirPath + filename;
                                    zip.AddFile(filePath, "files");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }

                    Response.Clear();
                    Response.BufferOutput = false;
                    // string ZipFileName = ddlDegree.SelectedItem.Text.Trim() + "_" + ddlAdmissionBatch.SelectedItem.Text.Trim() + "_" + (ddlBranch.SelectedValue == "0" ? "" : ddlBranch.SelectedItem.Text.Trim()) + "_" + (ddlSem.SelectedValue == "0" ? "" : ddlSem.SelectedItem.Text.Trim()) + "_" + (ddlStudentType.SelectedValue == "0" ? "" : ddlStudentType.SelectedItem.Text.Trim()) + "_" + Type;
                    string ZipFileName = ddlBatch.SelectedItem.Text + '_' + ddlDegreeType.SelectedItem.Text + '_' + Type;
                    string zipName = String.Format("Zip_{0}.zip", ZipFileName.Replace(' ', '_'));
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);

                    this.DeleteDirectory(DirPath);
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updManual, "Record not found.", this.Page);
            }
        }
    }
}