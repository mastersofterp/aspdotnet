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
using IITMS.NITPRM;
 
public partial class ITLE_DownloadAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["file"] != null && Request.QueryString["file"].ToString() != "")
        {
            string filePath = Request.QueryString["file"];
            try
            {
                //filePath = Server.MapPath("").ToLower().Replace("itle", "") + filePath;

                FileStream sourceFile = new FileStream(filePath, FileMode.Open);
                long fileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)fileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();

                Response.Clear();

              // By Zubair 15 NOV 2014
                string fileName = Request.QueryString["filename"];
                //fileName = fileName.Replace(" ", "_");

                Response.BinaryWrite(getContent); 
                Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));

                Response.AddHeader("Content-Length", getContent.Length.ToString());
               // Response.AddHeader("content-disposition", "attachment; filename=" + fileName); // By Zubair 20/11/14
                Response.AddHeader("content-disposition", "attachment; filename=\"" + fileName+ "\"");
                
              
                // Response.AddHeader("content-disposition", " filename=" + Request.QueryString["filename"]);
                //Response.TransmitFile(Server.MapPath(Request.QueryString["filename"]));
                //Response.End();

               
        
        //Response.TransmitFile(path);
        //Response.End();
   

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
        switch (fileExtension.ToLower())
        {   
            case ".mp3":
                return "application/mp3";
                break;

            case ".mp4" :
                return "application/mp4";
                break;
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
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

            case ".rtf":
                return "text/rtf";
                break;

            default:
                return "";
                break;
        }
    }
}
