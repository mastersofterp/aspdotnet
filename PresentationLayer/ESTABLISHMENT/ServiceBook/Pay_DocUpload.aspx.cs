using System;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;


public partial class ESTABLISHMENT_ServiceBook_Pay_DocUpload : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();


    public int _idnoEmp;


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
                //Page Authorization
                //CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";

           


        }

        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindDocument();
        GetConfigForEditAndApprove();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Nomination.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Nomination.aspx");
        }
    }


    private void BindDocument()
    {
        DataSet dslist = null;

        //dslist = objstud.GetDocumentList(Convert.ToInt32(Session["stuinfoidno"]));
        dslist = objServiceBook.RetrieveAllDocument(_idnoEmp);


        if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
        {
            lvBinddata.Visible = true;
            lvBinddata.DataSource = dslist;
            lvBinddata.DataBind();

            foreach (ListViewDataItem dataItem in lvBinddata.Items)
            {
                //HiddenField hdfidno = dataItem.FindControl("hdfidno") as HiddenField;

                Label Uploaded = dataItem.FindControl("lblFile") as Label;
                if (Uploaded.Text.Trim().ToUpper() == "1")
                {
                    Uploaded.Text = "Uploaded";
                    Uploaded.Style.Add("color", "Green");
                }
                if (Uploaded.Text.Trim().ToUpper() == "0")
                {
                    Uploaded.Text = "Not Uploaded";
                    Uploaded.Style.Add("color", "Red");
                }
            }
        }
        else
        {
            lvBinddata.Visible = false;
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();
           

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
            //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();

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

                // objCommon.DisplayMessage(this, "Image not Found...", this);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";
                BindDocument();

            }
            else
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
                BindDocument();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
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
    
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            uploadDocument();
        }
        catch (Exception ex)
        {
        }

    }

    protected void uploadDocument()
    {
        try
        {
            ServiceBook objSevBook = new ServiceBook();
            //string IdNo = Session["stuinfoidno"].ToString();
            string IdNo = _idnoEmp.ToString();
            //string idno = Session["idno"].ToString();
            string idno = _idnoEmp.ToString();
            //string studentname = Session["userfullname"].ToString();

            //string folderPath = WebConfigurationManager.AppSettings["SVCE_STUDENT_DOC"].ToString() + idno + "_" + studentname + "\\";
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
            {

                FileUpload fuStudPhoto = lvitem.FindControl("fuFile") as FileUpload;
                HiddenField hidstudocno = lvitem.FindControl("HiddenField1") as HiddenField;
                int no = int.Parse(hidstudocno.Value.TrimStart());
                Button btndocno = lvitem.FindControl("btnSubmit") as Button;
                int docno = int.Parse(btndocno.CommandArgument);
                string Docno = btndocno.ToolTip;
                string FUToll = fuStudPhoto.ToolTip;
                //int docno = Convert.ToInt32(Docno);



                if (fuStudPhoto.HasFile)
                {
                    string contentType = contentType = fuStudPhoto.PostedFile.ContentType;
                    //if (!Directory.Exists(folderPath))
                    //{
                    //    Directory.CreateDirectory(folderPath);
                    //}

                    string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                    HttpPostedFile file = fuStudPhoto.PostedFile;
                    string filename = IdNo + "_doc_" + docno + ext;   //Path.GetFileName(fuStudPhoto.PostedFile.FileName);


                    if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                    {
                        //int retval = AL.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_doc_" + hidstudocno.Value + "", fuStudPhoto);


                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_doc_" + docno + "", fuStudPhoto);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        //CustomStatus cs = (CustomStatus)objstud.AddUpdateStudentDocumentsDetail(Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, "Blob Storage");


                        //   return;
                        objSevBook.UPLOADED = "1";

                        CustomStatus cs = (CustomStatus)objServiceBook.AddUpdateEmployeeDocumentsDetailNew(Convert.ToInt32(idno), Convert.ToInt32(hidstudocno.Value), ext, contentType, filename, "Blob Storage", objSevBook);

                        //fuStudPhoto.PostedFile.SaveAs(folderPath + filename);
                        if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
                        {
                            objCommon.DisplayMessage(this, "Uploaded Sucessfully.... !", this);
                            BindDocument();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                        }

                    }


                    else
                    {
                        objCommon.DisplayMessage(this, "Please Upload file Below or Equal to 512 Kb only !", this);
                        //lblmessageShow.ForeColor = System.Drawing.Color.Red;
                        //lblmessageShow.Text = "Please Upload file Below or Equal to 40 Kb only !";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                        return;


                        //goto outer;
                    }


                }
                else
                {
                    objSevBook.UPLOADED = "0";
                }
            }
            BindDocument();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    //public void DeleteIFExits(string FileName)
    //{
    //    CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
    //    string FN = Path.GetFileNameWithoutExtension(FileName);
    //    try
    //    {
    //        Parallel.ForEach(container.ListBlobs(FN, true), y =>
    //        {
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            ((CloudBlockBlob)y).DeleteIfExists();
    //        });
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

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



    protected void lvBinddata_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //Label status = e.Item.FindControl("status") as Label;

        }
    }

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "DocumentUpload";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSave.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.GetConfigForEditAndApprove-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    #endregion
}