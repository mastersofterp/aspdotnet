//==============================================
// MODULE NAME   : DOCUMENT & SCANING
// PAGE NAME     : DocCopyMove.aspx                                                 
// CREATION DATE : 30-dec-2014                                                        
// CREATED BY    : MRUNAL SINGH
//==============================================           


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Configuration;

public partial class DOCUMENTANDSCANNING_DCMNTSCN_DocCopyMove : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,DOCUMENTCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DocumentController objC = new DocumentController();
    Documentation obj = new Documentation();
    
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";
    string pathDest1 = "";
    string path1 = "";

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (ViewState["action"] == null)
                {
                    ViewState["action"] = "add";
                }


                objCommon.FillDropDownList(ddlSource, "ADMN_DC_DOCUMENT_TYPE_DOC", "DNO", "DOCUMENTNAME", "DNO>0 and DNO IN (SELECT VALUE FROM FN_SPLIT((SELECT UA_CAT FROM USER_ACC WHERE UA_NO=" + Session["userno"].ToString() + "),','))", "DNO");

                objCommon.FillDropDownList(ddlTarget, "ADMN_DC_DOCUMENT_TYPE_DOC", "DNO", "DOCUMENTNAME", "DNO>0 and DNO IN (SELECT VALUE FROM FN_SPLIT((SELECT UA_CAT FROM USER_ACC WHERE UA_NO=" + Session["userno"].ToString() + "),','))", "DNO");
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
                Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ELibraryMaster.aspx");
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Clear();

    }
   
    private void Clear()
    {
        ddlSource.SelectedIndex = 0;
        ddlTarget.SelectedIndex = 0;
        lblSource.Text = "";
        lblTarget.Text = "";
      
    }   
          
   
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            string path = Docpath + "DOCUMENTANDSCANNING/FILEUPLOAD/";

            sourcePath = path + hdnSource.Value;

            targetPath = path + hdnTarget.Value;
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
           }
            Directory.Move(sourcePath, targetPath);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.btnCopy_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

    
    protected void btnCut_Click(object sender, EventArgs e)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            string path = Docpath + "DOCUMENTANDSCANNING/FILEUPLOAD/";

            sourcePath = path + hdnSource.Value;

            targetPath = path + hdnTarget.Value;
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                //Cut or Move the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path

                System.IO.File.Move(srcPath, srcPath.Replace(sourcePath, targetPath));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.btnCut_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCopyFolder_Click(object sender, EventArgs e)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            string path = Docpath + "DOCUMENTANDSCANNING/FILEUPLOAD/";

            sourcePath = path + hdnSource.Value;

            targetPath = path + hdnTarget.Value;
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            // foreach (var srcPath in Directory.GetFiles(sourcePath))
            foreach (var srcPath in Directory.GetDirectories(sourcePath))
            {
                //Cut or Move the folder from sourcepath and place into mentioned target path, 
                //Overwrite the folder if same folder is exist in target path

                System.IO.Directory.Move(srcPath, srcPath.Replace(sourcePath, targetPath));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.btnCopyFolder_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get path of directory according to category
        //string path1 = "";
        DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlSource.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            path1 = path1 + ds.Tables[0].Rows[0]["PATH"].ToString();
        }
        hdnSource.Value = path1;
        lblSource.Text = "Path : " + path1;

    }
    protected void ddlTarget_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlTarget.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            pathDest1 = pathDest1 + ds.Tables[0].Rows[0]["PATH"].ToString();
        }
        hdnTarget.Value = pathDest1;
        lblTarget.Text = "Path : " + pathDest1;


    }
}
