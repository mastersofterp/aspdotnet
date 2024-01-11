using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace IITMS
{
    namespace UAIMS
    {
        namespace NonAcadBusinessLogicLayer.BusinessLogic
        {
            public class BlobController
            {   /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


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

                public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
                {
                    CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
                    int retval = 1;
                    string Ext = Path.GetExtension(FU.FileName);
                    string FileName = DocName + Ext;
                    try
                    {
                        DeleteIFExits(FileName, ConStr, ContainerName);
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

                public void DeleteIFExits(string FileName, string ConStr, string ContainerName)
                {
                    CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
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

                public bool CheckBlobExists(string ConStr, string ContainerName)
                {
                    bool result = false;
                    string s = ContainerName;
                    if (!String.IsNullOrEmpty(s))
                    {
                        // Key exists
                        //blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                        //blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
                        result = true;
                    }
                    else
                    {
                        // Key doesn't exist
                        //blob_ConStr = string.Empty;
                        //blob_ContainerName = string.Empty;
                    }
                    return result;
                }

                public DataSet GetBlobInfo(int OrganizationId, string Commandtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrganizationId);
                        objParams[1] = new SqlParameter("@P_Command", Commandtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GET_BLOB_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlobController.GetBlobInfo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetConnectionString(int OrganizationId, string Commandtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrganizationId);
                        objParams[1] = new SqlParameter("@P_Command", Commandtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_SB_GET_CONNECTION_STRING_FROM_BLOB", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlobController.GetConnectionString-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            }
        }
    }
}
