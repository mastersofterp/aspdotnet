using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Azure;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Data;
//using ICSharpCode.SharpZipLib.Zip;
using System.Threading.Tasks;
using DynamicAL_v2;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using ICSharpCode.SharpZipLib.Zip;

public partial class Call_Blob_Storage : System.Web.UI.Page
{
    DynamicControllerAL AL = new DynamicControllerAL();
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    //string blob_ContainerName = "mnrtrail-att";

    protected void Page_Load(object sender, EventArgs e)
    {
        //ListandDisplyBlobs();
        //CreateBlobContainer("cpukotaadmissionstest");


        //CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        //string FN = Path.GetFileNameWithoutExtension("5306_doc_5.jpg");
        //try
        //{
        //    int retVal = 0;
        //    Parallel.ForEach(container.ListBlobs(FN, true), y =>
        //    {
        //        retVal = 1;
        //    });
        //}
        //catch (Exception) { }
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    void CreateBlobContainer(string Name)
    {
        //int x = AL.Blob_CreateContainer("fv/sygOiys73+HmocWnoZEymcNp9jijH13mgxTHpmlMjTEtpZ6D8NDVGWJgEb7aqSVen663Q5YEsx3sigzelGQ==", "makautdocstorage", Name, "Nokia6600");
        //if (x == 1)
        //{
        //    Response.Write("Blob Container '" + Name + "' is created successfully.");
        //}
        //else
        //{
        //    Response.Write("Error.");
        //}

        //Get the reference of the Storage Account  
        CloudStorageAccount storageaccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString());
        //Get the reference of the Storage Blob  
        CloudBlobClient client = storageaccount.CreateCloudBlobClient();
        //Get the reference of the Container. The GetConainerReference doesn't make a request to the Blob Storage but the Create() &CreateIfNotExists() method does. The method CreateIfNotExists() could be use whether the Container exists or not  
        CloudBlobContainer container = client.GetContainerReference("cpukotadoctest");
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
       
    }

    void Blob_Upload()
    {
        //string accountname = "hsncustorage";
        //string accesskey = "28jmKWtM4YflbsFy6UM9yMO4kK0kROnNa/f23xqVInkqv4WuxpRQVx7EAelsI53ZDnFEZCuv+XOJWQOehBCrew==";

        //try
        //{
        //    StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        //    CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
        //    CloudBlobClient client = acc.CreateCloudBlobClient();
        //    CloudBlobContainer cont = client.GetContainerReference("mysample");

        //    cont.CreateIfNotExists();
        //    cont.SetPermissions(new BlobContainerPermissions
        //    {
        //        PublicAccess = BlobContainerPublicAccessType.Blob
        //    });

        //    CloudBlockBlob cblob = cont.GetBlockBlobReference("Sampleblob.png");

        //    using (Stream file = System.IO.File.OpenRead(@"E:\user.png"))
        //    {
        //        cblob.UploadFromStream(file);
        //    }

        //}
        //catch (Exception ex)
        //{

        //}
    }

    void Blob_Download()
    {
        //string accountname = "hsncustorage";
        //string accesskey = "28jmKWtM4YflbsFy6UM9yMO4kK0kROnNa/f23xqVInkqv4WuxpRQVx7EAelsI53ZDnFEZCuv+XOJWQOehBCrew==";

        //StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        //CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
        //CloudBlobClient client = acc.CreateCloudBlobClient();
        ////CloudBlobContainer cont = client.GetContainerReference("mysample");

        //CloudBlobDirectory dist = CloudBlobContainer.GetDirectoryReference("hsncustorage");

        //CloudBlockBlob blockBlob = CloudBlobContainer.GetBlockBlobReference("mysample");

        //blockBlob.Properties.ContentType = "image/jpg";
        //blockBlob.SetProperties();
        //return blockBlob;
    }

    void ListandDisplyBlobs()
    {
        string connStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
        string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        CloudStorageAccount account = CloudStorageAccount.Parse(connStr);

        CloudBlobClient client = account.CreateCloudBlobClient();

        CloudBlobContainer container = client.GetContainerReference(containerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
       

        //CloudBlockBlob blob = container.GetBlockBlobReference("hsncustorage");

        // Set the Permission at Container Level for Public Access
        // So that the data stored inside it can be accessed publically
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        // S4: List All Blobs Available in the Container
        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        var blobList = container.ListBlobs(useFlatBlobListing: true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
            //if (y == Convert.ToString(Session["userno"]))
            //{
            //    dt.Rows.Add(x, blob.Uri);
            //}
        }
        
        //gdvBlobs.DataSource = container.ListBlobs();
        //gdvBlobs.DataBind();

        //DataTable dt = AL.Blob_GetAllBlobs(blob_ConStr, blob_ContainerName);
        gdvBlobs.DataSource = dt;
        gdvBlobs.DataBind();
    }


    protected void gdvBlobs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string blobName = gdvBlobs.Rows[e.RowIndex].Cells[0].Text;
        DeleteSelectedBlob(blobName);
        //Blob_DeleteIfExists(blob_ConStr, blob_ContainerName, "20RE5A0501_spic");
        ListandDisplyBlobs();
    }

    void DeleteSelectedBlob(string selBlobName)
    {
        //string connStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
        //string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        //CloudStorageAccount account = CloudStorageAccount.Parse(connStr);
        //CloudBlobClient client = account.CreateCloudBlobClient();
        //CloudBlobContainer container = client.GetContainerReference(containerName);

        //var blobNameToDelete = container.GetBlobReference(selBlobName);
        //blobNameToDelete.Delete();
    }

    protected void downloadBlob_Click(object sender, EventArgs e)
    {
        string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
        string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
        string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

        StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
        CloudBlobClient client = acc.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //var blobFileNames = new string[] { "file1.png", "file2.png", "file3.png", "file4.png" };
        var blobFileNames = new string[] { "JECRCQD4MM_39_.pdf" };
        using (var zipOutputStream = new ZipOutputStream(Response.OutputStream))
        {
            foreach (var blobFileName in blobFileNames)
            {
                zipOutputStream.SetLevel(0);
                var blob = container.GetBlobReference(blobFileName);
                var entry = new ZipEntry(blobFileName);
                zipOutputStream.PutNextEntry(entry);
                
                blob.DownloadToStream(zipOutputStream);
            }
            zipOutputStream.Finish();
            zipOutputStream.Close();
        }
        Response.BufferOutput = false;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "zipFileName.zip");
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.End();
    }

    //private DataTable Blob_Photos(string ID)
    //{
    //    //string connStr = "DefaultEndpointsProtocol=https;AccountName=hsncustorage;AccountKey=28jmKWtM4YflbsFy6UM9yMO4kK0kROnNa/f23xqVInkqv4WuxpRQVx7EAelsI53ZDnFEZCuv+XOJWQOehBCrew==;EndpointSuffix=core.windows.net";

    //    //CloudStorageAccount account = CloudStorageAccount.Parse(connStr);
    //    //CloudBlobClient client = account.CreateCloudBlobClient();
    //    //CloudBlobContainer container = client.GetContainerReference("mysample");

    //    //var permission = container.GetPermissions();
    //    //permission.PublicAccess = BlobContainerPublicAccessType.Container;
    //    //container.SetPermissions(permission);

    //    //DataTable dt = new DataTable();
    //    //dt.TableName = "FilteredBolb";
    //    //dt.Columns.Add("Name");
    //    //dt.Columns.Add("Uri");

    //    ////var blobList = container.ListBlobs(useFlatBlobListing: true);
    //    //var blobList = container.ListBlobs("" + ID + "_Photo", true);
    //    //foreach (var blob in blobList)
    //    //{
    //    //    string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
    //    //    string y = x.Split('_')[0];
    //    //    //if (y == Convert.ToString(Session["userno"]))
    //    //    //{
    //    //    //    dt.Rows.Add(blob.Uri);
    //    //    //}
    //    //    dt.Rows.Add(x, blob.Uri);
    //    //}

    //    //return dt;
    //}

    private void Blob_DeleteIfExists(string ConStr, string ContainerName, string FileName)
    {
        //CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        ////var blockBlobs = container.ListBlobs(FileName, true);

        //Parallel.ForEach(container.ListBlobs(FileName, true), y =>
        //{
        //    ((CloudBlockBlob)y).DeleteIfExists();
        //});

        //try
        //{
        //    //blockBlobs.Delete();
        //}
        //catch (Exception) { }
    }

    //private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    //{
    //    CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
    //    CloudBlobClient client = account.CreateCloudBlobClient();
    //    CloudBlobContainer container = client.GetContainerReference(ContainerName);
    //    return container;
    //}
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        if (fuStudentPhoto1.HasFile)
        {
            string Ext = Path.GetExtension(fuStudentPhoto1.FileName);
            string FileName = "File1" + Ext;
            try
            {
                DeleteIFExits(FileName);
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
               

                CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
                cblob.UploadFromStream(fuStudentPhoto1.PostedFile.InputStream);
            }
            catch
            { }

        }
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

    
}