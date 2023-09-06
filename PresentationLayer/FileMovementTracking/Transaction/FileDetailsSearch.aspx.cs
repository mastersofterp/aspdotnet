//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 12-SEP-2017                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.FileMovement;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using System.Configuration;
using System.IO;

using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;

using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;


public partial class FileMovementTracking_Transaction_FileDetailsSearch : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FileMovement objFMov = new FileMovement();
    FileMovementController objFController = new FileMovementController();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";
    public string path = string.Empty;
    public string dbPath = string.Empty;

    #region PageLoad Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

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
                    //Page Authorization
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["FILE_ID"] = null;

                    //if (Session["usertype"].ToString() == "1")   // super admin
                    //{
                    //    objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "", "FILE_ID DESC");
                    //    objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "", "FILE_ID DESC");
                    //}
                    //else
                    //{
                    //    objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");
                    //    objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DECS");
                       
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileDetailsSearch.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region User-Define Methods
    // This method is used to check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    // this method is used to clear the controls.
    private void Clear()
    {       
        ddlFileName.SelectedIndex = 0;
        txtFileName.Text = string.Empty;
        lvFile.DataSource = null;
        lvFile.DataBind();

        lvFileMovement.DataSource = null;
        lvFileMovement.DataBind();
        lvFileMovement.Visible = false;
        
        lvNewFiles.DataSource = null;
        lvNewFiles.DataBind();
        pnlNewFiles.Visible = false;

        divFileName.Visible = false;
        divFileNo.Visible = false;


         divFileDate.Visible = false;
         divToDate.Visible = false;
       

        txtKeywords.Text = string.Empty;
        divKeywords.Visible = false;
       // rdbSearch.SelectedValue = ;
       rdbSearch.ClearSelection();
     

    }


    // this method is used to display the file movement entry list.
    private void BindListOfFileMovement(int FILE_ID)
    {
        try
        {
            DataSet ds = null;  
            ds = objFController.GetSelectedFileDetails(FILE_ID, Convert.ToInt32(Session["userno"]));                
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFileMovement.DataSource = ds;
                lvFileMovement.DataBind();
                lvFileMovement.Visible = true;
                pnlMovemnt.Visible = true;
            }
            else
            {
                lvFileMovement.DataSource = null;
                lvFileMovement.DataBind();
                lvFileMovement.Visible = false;
                pnlMovemnt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileDetailsSearch.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else


                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   
   

    #endregion


    #region Page Actions

    protected void rdbSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["FILE_ID"] = null;                   //16/06/2022
                pnlList.Visible = false;
                pnlMovemnt.Visible = false;
                pnlNewFiles.Visible = false;
                pnlFinalList.Visible = false;

            if (rdbSearch.SelectedValue == "0")  //File Name
            {
           
                divFileName.Visible = true;
                txtFileName.Visible = false;
                divFileNo.Visible = false;
                divFileDate.Visible = false;
                divToDate.Visible=false;
                 //objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");
                divKeywords.Visible = false;
                txtFileName.Text = string.Empty;





                if (Session["usertype"].ToString() == "1")   // super admin
                {
                    objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "", "FILE_ID DESC");
                   // objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "", "FILE_ID DESC");
                }
                else
                {
                    objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");
                   // objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DECS");

                }


                
            }
            else if (rdbSearch.SelectedValue == "1") // File No
            {
                divFileName.Visible = false;
                divFileNo.Visible = true;
                txtFileCode.Visible = false;
                divFileDate.Visible = false;
                divToDate.Visible = false;
                 //objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");   /////comment by gayatri rode15-01-2021
                divKeywords.Visible = false;
                txtFileCode.Text = string.Empty;




                if (Session["usertype"].ToString() == "1")   // super admin
                {
                   // objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "", "FILE_ID DESC");
                    objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "", "FILE_ID DESC");
                }
                else
                {
                   // objCommon.FillDropDownList(ddlFileName, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");
                    objCommon.FillDropDownList(ddlFileNo, "FILE_FILEMASTER", "FILE_ID", "FILE_CODE", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");

                }



            }
            else if (rdbSearch.SelectedValue == "2")  // File Creation Date
            {
                divFileName.Visible = false;
                divFileNo.Visible = false;
                divFileDate.Visible = true;
                divToDate.Visible = true;

                divKeywords.Visible = false;
                txtenddate.Text = string.Empty;
                txtstartdate.Text = string.Empty;
            }
            else                                // Keywords
            {
                divFileName.Visible = false;
                divFileNo.Visible = false;
                divFileDate.Visible = false;
                divToDate.Visible = false;
                divKeywords.Visible = true;
                txtKeywords.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileDetailsSearch.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    
    // this method is used to find the file name.
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSectionName(string prefixText)
    {
        List<string> SectionUserName = new List<string>();
        DataSet ds = new DataSet();       
        try
        { 
            FileMovementController objFController = new FileMovementController();
            ds = objFController.FillFileName(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SectionUserName.Add(ds.Tables[0].Rows[i]["FILE_ID"].ToString() + "---------*" + ds.Tables[0].Rows[i]["FILE_NAME"].ToString());
            }           
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return SectionUserName;        
    }



    // this method is used to find the file name.
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetFileNameByCode(string prefixText)
    {
        List<string> FileCode = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            FileMovementController objFController = new FileMovementController();
            ds = objFController.FillFileNameByFileCode(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                FileCode.Add(ds.Tables[0].Rows[i]["FILE_ID"].ToString() + "---------*" + ds.Tables[0].Rows[i]["FILE_CODE"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return FileCode;
    }

    // this button is used show details of selected files.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (rdbSearch.SelectedValue == "0")  //File Name
            {
                if (ddlFileName.SelectedIndex > 0)
                {
                    BindListViewFile(Convert.ToInt32(ddlFileName.SelectedValue));
                }
                else
                {
                    Showmessage("Please Select File Name.");
                    return;
                }
            }
            else if (rdbSearch.SelectedValue == "1") // File No
            {
                if (ddlFileNo.SelectedIndex > 0)
                {
                    BindListViewFile(Convert.ToInt32(ddlFileNo.SelectedValue));
                }
                else
                {
                    Showmessage("Please Select File No.");
                    return;
                }
            }
            else if (rdbSearch.SelectedValue == "2") // File Creation Date
            {
                if (!txtstartdate.Text.Equals(string.Empty) && !txtenddate.Text.Equals(string.Empty))
                {
                    if (DateTime.Compare(Convert.ToDateTime(txtstartdate.Text), Convert.ToDateTime(txtenddate.Text)) == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                        txtstartdate.Focus();                        
                        return;
                    }
                    else
                    {
                        string Fdate = Convert.ToDateTime(txtstartdate.Text).ToString("yyyy-MM-dd");
                        string Tdate = Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MM-dd");
                        BindFiles(Fdate, Tdate);
                    }
                }  
                else
                {
                    Showmessage("Please Enter From Date And To Date.");
                    return;
                }
            }
            else if (rdbSearch.SelectedValue == "3") // File Keywords
            {
                if (!txtKeywords.Text.Equals(string.Empty))
                {
                    DataSet ds = null;
                    if (Session["usertype"].ToString() == "1")   // super admin
                    {
                        ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) LEFT JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE,  (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID", "F.FILE_KEYWORDS LIKE '%" + txtKeywords.Text.Trim() + "%'", "F.FILE_ID DESC");
                    }
                    else
                    {
                        ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) LEFT JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE,  (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID", "F.USERNO = " + Convert.ToInt32(Session["userno"]) + " AND F.FILE_KEYWORDS LIKE '%" + txtKeywords.Text.Trim() + "%'", "F.FILE_ID DESC");
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                        {
                            lvFile.DataSource = ds;
                            lvFile.DataBind();
                            pnlList.Visible = true;
                           
                        }
                        else
                        {
                            lvFile.DataSource = null;
                            lvFile.DataBind();
                            pnlList.Visible = false;
                            
                        }
                   
                }
                else
                {
                    Showmessage("Please Enter Keywords.");
                    return;
                }
            }
            else
            {
                Showmessage("Please Select File Details Search Option.");
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileDetailsSearch.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }    


    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }


    // this method is used to display the list of Files which are used for movement.
    private void BindListViewFile(int fileId)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) left JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE,  (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID ", "F.FILE_ID=" + fileId, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFile.DataSource = ds;
                lvFile.DataBind();
                pnlList.Visible = true;
            }
            else
            {
                lvFile.DataSource = null;
                lvFile.DataBind();
                pnlList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileDetailsSearch.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindFiles(string Fdate, string Tdate)
    {
        try
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")   // super admin
            {
                ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE,  (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID ", "CONVERT(DATE,F.CREATION_DATE) BETWEEN '" + Fdate + "' AND '" + Tdate + "'", "F.FILE_ID DESC");
            }
            else
            {
                ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE,  (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID ", "F.USERNO = " + Convert.ToInt32(Session["userno"]) + " AND CONVERT(DATE,F.CREATION_DATE) BETWEEN '" + Fdate + "' AND '" + Tdate + "'", "F.FILE_ID DESC");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFile.DataSource = ds;
                lvFile.DataBind();
                pnlList.Visible = true;
                ViewState["lvFile"] = "yes";
            }
            else
            {
                lvFile.DataSource = null;
                lvFile.DataBind();
                pnlList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileDetailsSearch.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdoFile_CheckedChanged(object sender, EventArgs e)
    {

        RadioButton rdbBtn = sender as RadioButton;        
        int file_id = int.Parse(rdbBtn.ToolTip);
        ViewState["FILE_ID"] = int.Parse(rdbBtn.ToolTip);
        string FilePath = Docpath + "FILEMOVEMENT\\UPLOADFILES\\";
        BindListOfFileMovement(file_id);     

        DataSet dFile = objFController.RetrieveDocuments(file_id, Convert.ToInt32(Session["userno"]), FilePath);
        if (dFile.Tables.Count > 0)
        {
            ViewState["FILE_NAME"] = dFile.Tables[0].Rows[0]["FILENAME"].ToString();

            if (dFile.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "1")
            {
                lvFinalList.DataSource = dFile;
                lvFinalList.DataBind();
                pnlFinalList.Visible = true;

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                pnlNewFiles.Visible = false;
            }
            else
            {
                lvFinalList.DataSource = null;
                lvFinalList.DataBind();
                pnlFinalList.Visible = false;

                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + dFile.Tables[0].Rows[0]["FILE_NAME"].ToString();
                BindNewFiles(path);

            }
        }
    }

    private void BindNewFiles(string PATH)
    {
        try
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {
                System.IO.FileInfo[] files = dir.GetFiles();

                if (Convert.ToBoolean(files.Length))
                {
                    lvNewFiles.DataSource = files;
                    lvNewFiles.DataBind();
                    ViewState["FILE"] = files;
                    pnlNewFiles.Visible = true;
                }
                else
                {
                    lvNewFiles.DataSource = null;
                    lvNewFiles.DataBind();
                    pnlNewFiles.Visible = false;
                }
            }
            else
            {
                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                pnlNewFiles.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.BindNewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;

        f_name = obj.ToString();
        return f_name;
    }

    protected void imgdownloadNew_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFileNew(btn.AlternateText);
    }

    public void DownloadFileNew(string fileName)
    {
        try
        {

            path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + ViewState["FILE_NAME"].ToString();
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }

    public void DownloadFile(string fileName)
    {
        try
        {
            path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + ViewState["FOLDER_PATH"].ToString();
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
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

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {

        //------------------31/05/2022- Start
        if (rdbSearch.SelectedValue == "")  //None select
        {
            //Showmessage("Please Choose Any One Select Option.");
            objCommon.DisplayMessage("Please Select File Detail Search Option.", this.Page);
            return;
        }

        if (rdbSearch.SelectedValue == "0")  //File Name
        {
            if (ddlFileName.SelectedIndex > 0)
            {
                //objCommon.DisplayMessage("Please Click On Show Button First And Select One Of The File from List.", this.Page);
               // return;
                                    ViewState["FILE_ID"] = ddlFileName.SelectedValue;

            }
            else
            {
                // Showmessage("Please Select File Name.");
                objCommon.DisplayMessage("Please Select File Name.", this.Page);
                return;
            }
        }
        else if (rdbSearch.SelectedValue == "1") // File No
        {
            if (ddlFileNo.SelectedIndex > 0)
            {
               // objCommon.DisplayMessage("Please Click On Show Button First And Select One Of The File from List.", this.Page);
               // return;
                ViewState["FILE_ID"] = ddlFileNo.SelectedValue;
            }
            else
            {
                // Showmessage("Please Select File No.");
                objCommon.DisplayMessage("Please Select File No.", this.Page);
                return;
            }
        }
        else if (rdbSearch.SelectedValue == "2") // File Creation Date
        {
            if (!txtstartdate.Text.Equals(string.Empty) && !txtenddate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtstartdate.Text), Convert.ToDateTime(txtenddate.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                    txtstartdate.Focus();
                    return;
                }
                else
                {
                    //if (ViewState["lvFile"] != "yes")
                    //{
                    //    objCommon.DisplayMessage("Please Click On Show Button First And Select One Of The File from List.", this.Page);

                    //    return;
                    //}
                    if (ViewState["FILE_ID"] == null)
                    {
                        objCommon.DisplayMessage("Pleas Select One Of The File from List.", this.Page);

                    return;
                    }
                }
            }
            else
            {
                // Showmessage("Please Enter From Date And To Date.");
                objCommon.DisplayMessage("Please Enter From Date And To Date.", this.Page);
                return;
            }
        }
        else if (rdbSearch.SelectedValue == "3") // File Keywords
        {
            if (!txtKeywords.Text.Equals(string.Empty) && ViewState["FILE_ID"] == null)
            {
                objCommon.DisplayMessage("Please Select One Of The File from List.", this.Page);
                return;

            }
            else if (txtKeywords.Text.Equals(string.Empty))
            {
                // Showmessage("Please Enter Keywords.");
                objCommon.DisplayMessage("Please Enter Keywords.", this.Page);
                return;
            }
        }
        else
        {
            //Showmessage("Please Select File Details Search Option.");
            objCommon.DisplayMessage("Please Select File Details Search Option.", this.Page);
            return;
        }

//-----------------------------------31/05/2022---end---------










        if (ViewState["FILE_ID"] != null || ddlFileName.SelectedIndex > 0 || ddlFileNo.SelectedIndex > 0)
        {
            if (ViewState["FILE_ID"] == null)
            {
                //if (ddlFileName.SelectedIndex < 0)
                //{
                //    ViewState["FILE_ID"] = ddlFileNo.SelectedValue;
                //}
                //else
                //{
                //    ViewState["FILE_ID"] = ddlFileName.SelectedValue;
                //}
            }

            ShowReport("File Movement Details", "rptFileMovementSearch.rpt");
        }
        else
        {
            Showmessage("Please Select One Of The File");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;           
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) ;
          
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);             
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnStatusReport_Click(object sender, EventArgs e)
    {
        //------------------31/05/2022- Start
        if (rdbSearch.SelectedValue == "")  //None select
        {
            //Showmessage("Please Choose Any One Select Option.");
            objCommon.DisplayMessage("Please Select File Detail Search Option.", this.Page);
            return;
        }

        if (rdbSearch.SelectedValue == "0")  //File Name
        {
            if (ddlFileName.SelectedIndex > 0)
            {
                //objCommon.DisplayMessage("Please Click On Show Button First And Select One Of The File from List.", this.Page);
                // return;
                ViewState["FILE_ID"] = ddlFileName.SelectedValue;

            }
            else
            {
                // Showmessage("Please Select File Name.");
                objCommon.DisplayMessage("Please Select File Name.", this.Page);
                return;
            }
        }
        else if (rdbSearch.SelectedValue == "1") // File No
        {
            if (ddlFileNo.SelectedIndex > 0)
            {
                // objCommon.DisplayMessage("Please Click On Show Button First And Select One Of The File from List.", this.Page);
                // return;
                ViewState["FILE_ID"] = ddlFileNo.SelectedValue;
            }
            else
            {
                // Showmessage("Please Select File No.");
                objCommon.DisplayMessage("Please Select File No.", this.Page);
                return;
            }
        }
        else if (rdbSearch.SelectedValue == "2") // File Creation Date
        {
            if (!txtstartdate.Text.Equals(string.Empty) && !txtenddate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtstartdate.Text), Convert.ToDateTime(txtenddate.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                    txtstartdate.Focus();
                    return;
                }
                else
                {
                    //if (ViewState["lvFile"] != "yes")
                    //{
                    //    objCommon.DisplayMessage("Please Click On Show Button First And Select One Of The File from List.", this.Page);

                    //    return;
                    //}
                    if (ViewState["FILE_ID"] == null)
                    {
                        objCommon.DisplayMessage("Pleas Select One Of The File from List.", this.Page);

                        return;
                    }
                }
            }
            else
            {
                // Showmessage("Please Enter From Date And To Date.");
                objCommon.DisplayMessage("Please Enter From Date And To Date.", this.Page);
                return;
            }
        }
        else if (rdbSearch.SelectedValue == "3") // File Keywords
        {
            if (!txtKeywords.Text.Equals(string.Empty) && ViewState["FILE_ID"] == null)
            {
                objCommon.DisplayMessage("Please Select One Of The File from List.", this.Page);
                return;

            }
            else if (txtKeywords.Text.Equals(string.Empty))
            {
                // Showmessage("Please Enter Keywords.");
                objCommon.DisplayMessage("Please Enter Keywords.", this.Page);
                return;
            }
        }
        else
        {
            //Showmessage("Please Select File Details Search Option.");
            objCommon.DisplayMessage("Please Select File Details Search Option.", this.Page);
            return;
        }

        //-----------------------------------31/05/2022---end---------


        if (ViewState["FILE_ID"] != null || ddlFileName.SelectedIndex > 0 || ddlFileNo.SelectedIndex > 0)
        {
            if (ViewState["FILE_ID"] == null)
            {
                //if (ddlFileName.SelectedIndex < 0)
                //{
                //    ViewState["FILE_ID"] = ddlFileNo.SelectedValue;
                //}
                //else
                //{
                //    ViewState["FILE_ID"] = ddlFileName.SelectedValue;
                //}
            }

            ShowStatusReport("File Status Report", "ConsolidateFileMovementDetails.rpt");
           
        }
        else
        {
            Showmessage("Please Select One Of The File");
        }
    }

    private void ShowStatusReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
            url += "&param=@P_FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);    
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    #endregion
   

  
}