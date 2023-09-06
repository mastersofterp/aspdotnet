using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ITLE_DownloadAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // GET THE FILE NAME AND PATH 
        if (Request.QueryString["file"] != null && Request.QueryString["file"].ToString() != "")
        {
            string filePath = Request.QueryString["file"];
            try
            {
                //filePath = Server.MapPath("").ToLower().Replace("dcmntscn", "") + filePath;


                // OPEN THE FILE 
                FileStream sourceFile = new FileStream(filePath, FileMode.Open);
                long fileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)fileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();

                Response.Clear();
                Response.BinaryWrite(getContent); 
                //Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
                Response.ContentType = GetResponseType(Path.GetExtension(filePath));
                Response.AddHeader("content-disposition", "attachment; filename=" + Request.QueryString["filename"]);

            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.ContentType = "text/html";
                Response.Write("Unable to download the attachment.");
            }
        }
    }

    private string GetResponseType(string fileExtension)
    {
        //CHECK THE FILE FORMAT
        switch (fileExtension.ToLower())
        {
            case ".docx":
            case ".doc":
            case ".rtf":
                return "Application/msword";
                break;
            
            //case ".doc":
            //    return "application/vnd.ms-word.document";
            //    break;
            //return "application / vnd.openxmlformats - officedocument.wordprocessingml.document";    
            //return "application/vnd.ms-word";
           
            //case ".docx":
            //    return "application/vnd.ms-word";
            //    break;

            //Application/x-msexcel

            //case ".xls":
            //    return "application/ms-excel";
            //    break;

            case ".xls":
            case ".csv":
            case ".xlsx":
                return "Application/x-msexcel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case ".jpg":
                return "image/jpeg";
                break;

            case ".htm":
            case ".html":
                return "text/HTML";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
}
