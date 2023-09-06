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

public partial class Itle_Itle_QuestionImage : System.Web.UI.Page
{
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FileName"] != null)
    {
        

    try
    {
        // Read the file and convert it to Byte Array
        string filePath = file_path + "ITLE/upload_files/IMAGE_QUESTION/";
        string filename = Request.QueryString["FileName"];
        string contenttype = "image/" +
        Path.GetExtension(Request.QueryString["FileName"].Replace(".",""));
        FileStream fs = new FileStream(filePath + filename,
        FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        br.Close();
        fs.Close();
 
        //Write the file to response Stream
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contenttype;
        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
    catch
    {
    }
}
}
}
