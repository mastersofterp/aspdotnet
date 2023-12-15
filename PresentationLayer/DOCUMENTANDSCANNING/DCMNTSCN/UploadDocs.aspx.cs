//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : DOCUMENT & SCANING
// PAGE NAME     : DCMNTSCN_Category.ASPX                                                    
// CREATION DATE : 18-JAN-2011                                                        
// CREATED BY    : PRAKASH RADHWANI 
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

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
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


public partial class DCMNTSCN_UploadDocs : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,DOCUMENTCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DocumentController objC = new DocumentController();
    Documentation obj = new Documentation();
    BlobController objBlob = new BlobController();
    int count = 0;
    int docSaveCnt = 0;
    int obje = 0;
    static decimal File_size;
    string PageId;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

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
        //btnAskSave.Attributes.Add("onClick", "return AskSave();");
        btnAlelrt.Style.Add("display", "none");
        if (!Page.IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (ViewState["action"] == null)
                {
                    ViewState["action"] = "add";
                }
                Bind();
                pnlAdd.Visible = false;
                Pancatgrid.Visible = false;
                pnlList.Visible = true;
                Session["Attachments"] = null;
                // Main();
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                objCommon.FillDropDownList(ddlCategory, "ADMN_DC_DOCUMENT_TYPE_DOC", "DNO", "DOCUMENTNAME", "DNO>0 and DNO IN (SELECT VALUE FROM FN_SPLIT((SELECT UA_CAT FROM USER_ACC WHERE UA_NO=" + Session["userno"].ToString() + "),','))", "DNO");
                //string directoryPath = "E:\\VSS";
                //GetFilesList(directoryPath);
                //FillDept();
                pnlButton.Visible = false;

            }
            BlobDetails();
            //delete previous override files if any.
            //DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath("~/DOCUMENTANDSCANNING/fileupload_overide/"));
            //    FileInfo[] fInfo = dInfo.GetFiles();
            //    foreach (FileInfo f in fInfo)
            //    {
            //        f.Delete();
            //    }

        }
    }
    protected void Bind()
    {
        try
        {
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            DataSet ds = objC.RetrieveAllDocument(obj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDoc.DataSource = ds;
                lvDoc.DataBind();
            }
            else
            {
                lvDoc.DataSource = null;
                lvDoc.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
    private void GetFilesList(string strPath)
    {
        System.IO.DirectoryInfo xMainDir = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] filein = xMainDir.GetFiles();
        count = filein.Length;
        System.IO.DirectoryInfo[] dirin = xMainDir.GetDirectories();

        for (int i = 0; i < dirin.Length; i++)
        {
            GetChildDetails(dirin[i].FullName);
        }
    }


    private void GetChildDetails(string strPath)
    {
        System.IO.DirectoryInfo xMainDir = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] filein = xMainDir.GetFiles();
        count += filein.Length;
        System.IO.DirectoryInfo[] dirin = xMainDir.GetDirectories();

        for (int i = 0; i < dirin.Length; i++)
        {
            GetChildDetails(dirin[i].FullName);
        }

    }
    static void Main()
    {

        String line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("E:\\iitms_doc\a.txt");

            //Read the first line of text
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                string CHAR = Convert.ToString(sr.ReadToEnd());
                sr.ReadToEnd();
                //write the lie to console window
                Console.WriteLine(line);
                //Read the next line
                line = sr.ReadLine();
            }

            //close the file
            sr.Close();
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }

        //long totalSize = 0;
        //string directoryPath = "D:\\T&P"; // "xxxx------ Your Directory Path -------xxx";

        //DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        //FileStream obj = new FileStream(directoryPath, FileMode.Open);


        ////Action which is to be applied on all files
        //Action<FileInfo> action = new Action<FileInfo>(delegate(FileInfo file)
        //{
        //    totalSize += file.Length;
        //});
        //ApplyActionToAllFiles(directoryInfo, action);

        //Console.WriteLine("Total size in bytes: {0}", totalSize);
        //Console.ReadLine();
    }
    public static void ApplyActionToAllFiles(DirectoryInfo directory, Action<FileInfo> action)
    {
        foreach (FileInfo file in directory.GetFiles())
        {
            action(file);
        }
        foreach (DirectoryInfo subDirectory in directory.GetDirectories())
        {
            ApplyActionToAllFiles(subDirectory, action);
        }
    }

    //protected void FillDept()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNAME");
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        chkLstDept.DataTextField = "DEPTNAME";
    //        chkLstDept.DataValueField = "DEPTNO";
    //        chkLstDept.DataSource = ds.Tables[0];
    //        chkLstDept.DataBind();
    //    }
    //    else
    //    {
    //        chkLstDept.DataSource = null;
    //        chkLstDept.DataBind();
    //    }
    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ddlCategory.Enabled = false;
            ImageButton btnEdit = sender as ImageButton;
            int UPLNO = int.Parse(btnEdit.CommandArgument);
            Session["UPLNO"] = Convert.ToInt32(UPLNO);
            ViewState["action"] = "edit";
            this.ShowDetails(UPLNO);
            pnlAdd.Visible = true;
            Pancatgrid.Visible = true;
            pnlList.Visible = false;
            lnkAdd.Visible = false;
            pnlButton.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void jQueryUploadFiles(object sender, EventArgs e)
    {
        try
        {
            obj.DNO = Convert.ToInt32(ddlCategory.SelectedValue);
            obj.DESCRIPTION = ftbDescription.Text.Trim();
            obj.COLLEGE_CODE = Session["colcode"].ToString();
            obj.KEYWORD = Convert.ToString(txtKeyword.Text.Trim());
            obj.TITLE = Convert.ToString(txtTitle.Text.Trim());
            obj.SHARED = chkId.Checked ? Convert.ToChar('Y') : Convert.ToChar('N');
            // obj.AttachTable = (Session["Attachments"]) as DataTable;

            if (lblBlobConnectiontring.Text == "")
            {
                obj.IS_BLOB = 0;
            }
            else
            {
                obj.IS_BLOB = 1;
            }
            if (obj.IS_BLOB == 1)
            {
                obj.AttachTable = (Session["Attachments"]) as DataTable;
            }
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            Session["obj"] = obj;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool IsOverride = false;

                    #region
                    //int obje = Convert.ToInt32(objC.AddDocument(obj));
                    //Session["obje"] = obje;

                    //if (ValTitle() != string.Empty)
                    //{
                    //    objCommon.DisplayMessage(ValTitle(), this);
                    //    return;
                    //}

                    //HttpFileCollection files = Request.Files;
                    //Queue<int> q = new Queue<int>();
                    //int cnt = 0;
                    //obje = -1;


                    //for (int i = 0; i < files.Count; i++)
                    //{
                    //    HttpPostedFile file = files[i];
                    //   // if (file.ContentLength > 0)
                    //   // {
                    //        //string path = Server.MapPath("~/FILEUPLOAD/"+ddlCategory.SelectedItem.Text+"/");

                    //        //Get path of directory according to category

                    //        string path1 = Docpath + "DOCUMENTANDSCANNING/FILEUPLOAD/";
                    //        //  string path1 = Server.MapPath("~/DOCUMENTANDSCANNING/FILEUPLOAD/");
                    //        //path1 = path1 + "iitms_doc\\";
                    //        DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlCategory.SelectedValue));
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {
                    //            path1 = path1 + ds.Tables[0].Rows[0]["PATH"].ToString();
                    //        }

                    //        string fileName = Path.GetFileName(file.FileName);

                    //        //checks whether same file is exist in same loc if yes ask user to replace it or not
                    //        if (!Directory.Exists(path1))
                    //        {
                    //            Directory.CreateDirectory(path1);
                    //        }
                    //        DirectoryInfo d = new DirectoryInfo(path1);
                    //        FileInfo[] f = d.GetFiles();

                    //        foreach (FileInfo oFile in f)
                    //        {
                    //            cnt = 0;
                    //            if (oFile.Name == fileName)
                    //            {
                    //                cnt = 1;
                    //                IsOverride = true;
                    //                // save the file to the disk at another temp folder fileupload_overide
                    //                //  file.SaveAs((path1+"/fileupload_overide/") + fileName);
                    //                break;
                    //            }

                    //        }
                    //        if (cnt == 0)
                    //        {
                    //            // now save the file to the disk
                    //            file.SaveAs(path1 + fileName);

                    //            docSaveCnt += 1;
                    //            ViewState["docSaveCnt"] = docSaveCnt;
                    //            //save doc info for first time

                    //obj.FILENAME = Convert.ToString(fileName);
                    //obj.SIZE = (file.ContentLength / 1024);
                    //obj.ORIGINAL_FILENAME = Path.GetFileName(file.FileName);
                    //obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    //obj.UPLNO = Convert.ToInt32(obje);
                    ////obj.FILE_PATH = "FILEUPLOAD\\" + ddlCategory.SelectedItem.Text + "\\" + fileName;
                    //obj.FILE_PATH = path1 + fileName;
                    #endregion


                    if (docSaveCnt <= 1)
                    {
                        obje = Convert.ToInt32(objC.AddDocument(obj));
                        Session["obje"] = obje;
                    }

                    CustomStatus cs = (CustomStatus)objC.AddFile(obj);
                    // }
                    // }
                    // }
                    //if (cnt == 1)
                    if (IsOverride == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showAl", "ShowConfirmation();", true);
                    }
                    if (Convert.ToInt32(obje) != -1)
                    {
                        Clear();
                        objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                        Bind();
                    }
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("edit"))
                    {

                        obj.UPLNO = Convert.ToInt32(Session["UPLNO"].ToString());
                        int no = Convert.ToInt32(objC.UpdateDocument(obj));
                        CustomStatus cse = (CustomStatus)objC.AddFile(obj);

                        #region
                        //HttpFileCollection files = Request.Files;

                        //for (int i = 0; i < files.Count; i++)
                        //{
                        //    HttpPostedFile file = files[i];

                        //    if (file.ContentLength > 0)
                        //    {
                        //        //string path = Server.MapPath("~/FILEUPLOAD/"+ddlCategory.SelectedItem.Text+"/");
                        //        string path = Docpath + "DOCUMENTANDSCANNING/FILEUPLOAD/";
                        //        // string path = Server.MapPath("~/DOCUMENTANDSCANNING/FILEUPLOAD/");

                        //        DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlCategory.SelectedValue));
                        //        if (ds.Tables[0].Rows.Count > 0)
                        //        {
                        //            path = path + ds.Tables[0].Rows[0]["PATH"].ToString();
                        //        }
                        //        string fileName = Path.GetFileName(file.FileName);
                        //        //string chkfile = path + fileName;
                        //        DataSet dsFile = objCommon.FillDropDown("ADMN_DC_DOCUMENT_FILE", "*", "", "", "");
                        //        if (dsFile != null && dsFile.Tables[0].Rows.Count > 0)
                        //        {
                        //            for (int k = 0; k < dsFile.Tables[0].Rows.Count; k++)
                        //            {
                        //                if ((path + fileName).ToLower().Replace("//", "/") == dsFile.Tables[0].Rows[k]["FILEPATH"].ToString().ToLower())
                        //                {
                        //                    objCommon.DisplayMessage("This file name already exists.", this.Page);
                        //                    return;
                        //                }
                        //            }
                        //        }

                        // now save the file to the disk
                        //file.SaveAs(path + fileName);


                        //obj.FILENAME = Convert.ToString(fileName);
                        //obj.SIZE = (file.ContentLength / 1024);
                        //obj.ORIGINAL_FILENAME = Path.GetFileName(file.FileName);
                        //obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                        //obj.UPLNO = Convert.ToInt32(Session["UPLNO"].ToString());
                        ////obj.FILE_PATH = "FILEUPLOAD\\" + ddlCategory.SelectedItem.Text + "\\" + fileName;
                        //obj.FILE_PATH = path + fileName;

                        //CustomStatus cse = (CustomStatus)objC.AddFile(obj);
                        //}
                        //  }

                        #endregion

                        if (no != -1)
                        {
                            Clear();
                            objCommon.DisplayMessage("Record Updated Successfully", this.Page);
                        }
                    }
                }

            }

            ViewState["action"] = "add";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage("Error In Saving !!!", this.Page);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAlelrt_Click(object sender, EventArgs e)
    {
        try
        {
            // write your code which you wish to execute after the confirmation from the user....
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "showVal", "alert('" + "aaa" + "');", true);

            //string path = Server.MapPath("~/FILEUPLOAD/" + ddlCategory.SelectedItem.Text + "/");
            //string path = "E:\\iitms_doc\\";

            string path = Docpath + "DOCUMENTANDSCANNING/FILEUPLOAD/";
            DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlCategory.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                path = path + ds.Tables[0].Rows[0]["PATH"].ToString();
            }


            string path1 = path + "/fileupload_overide/";// Server.MapPath("~/fileupload_overide/");

            DirectoryInfo dInfo = new DirectoryInfo(path1);
            FileInfo[] fInfo = dInfo.GetFiles();

            foreach (FileInfo f in fInfo)
            {
                // now save the file to the disk
                f.CopyTo(path + f.Name, true);

                //save doc info if it is not saved
                if (Convert.ToInt32(ViewState["docSaveCnt"]) == 0)
                {
                    obje = Convert.ToInt32(objC.AddDocument((Documentation)Session["obj"]));
                    Session["obje"] = obje;
                }

                obj.FILENAME = Convert.ToString(f.Name);
                obj.SIZE = (Convert.ToInt32((f.Length)) / 1024);
                obj.ORIGINAL_FILENAME = Path.GetFileName(f.Name);
                obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                obj.UPLNO = Convert.ToInt32(Convert.ToInt32(Session["obje"]));
                //obj.FILE_PATH = "FILEUPLOAD\\" + f.Name;
                obj.FILE_PATH = path + f.Name;

                CustomStatus cs = (CustomStatus)objC.AddFile(obj);
                f.Delete();
            }
            if (Convert.ToInt32(obje) != -1)
            {
                Clear();
                objCommon.DisplayMessage("Record Save Succesfully", this.Page);
                Bind();
            }
            objCommon.DisplayMessage("File(s) are override", this);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.btnAlelrt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            obj.UPLNO = int.Parse(btnDel.CommandArgument);
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            CustomStatus cs = (CustomStatus)objC.DeleteDocument(obj);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record Deleted Sucessfully", this.Page);
                Bind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowDetails(int UPLNO)
    {

        try
        {
            obj.UPLNO = Convert.ToInt32(UPLNO);
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            DataSet ds = objC.RetrieveDocument(obj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlAdd.Visible = true;
                Pancatgrid.Visible = true;
                pnlList.Visible = false;
                txtKeyword.Text = ds.Tables[0].Rows[0]["KEYWORD"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                ddlCategory.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["DNO"].ToString()).ToString();
                chkId.Checked = ds.Tables[0].Rows[0]["SHARED"].ToString().Equals("Y") ? true : false;
                ftbDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();

                ////check selected departments
                //string[] sArr= ds.Tables[0].Rows[0]["ALLOW_DEPTS"].ToString().Split(',');
                //for (int k = 0; k < chkLstDept.Items.Count; k++)
                //{
                //    foreach (string s in sArr)
                //    {
                //        if (chkLstDept.Items[k].Value.ToString() == s)
                //            chkLstDept.Items[k].Selected = true;
                //    }
                //}

            }
            DataSet d = objC.RetrieveDocumentFiles(obj);
            if (d.Tables[0].Rows.Count > 0)
            {
                //lvAttachments.DataSource = d;
                // lvAttachments.DataBind();
                DataTable dt = new DataTable();

                dt = this.GetAttachmentDataTable();
                for (int j = 0; j < d.Tables[0].Rows.Count; j++)
                {

                    DataRow dr = dt.NewRow();
                    dr["IDNO"] = d.Tables[0].Rows[j]["IDNO"];

                    dr["ORIGINAL_FILENAME"] = d.Tables[0].Rows[j]["ORIGINAL_FILENAME"].ToString();
                    dr["FILEPATH"] = d.Tables[0].Rows[j]["FILE_PATH"].ToString();
                    dr["SIZE"] = d.Tables[0].Rows[j]["SIZE"];
                    dr["UA_NO"] = d.Tables[0].Rows[j]["UA_NO"];
                    dt.Rows.Add(dr);
                    Session["Attachments"] = dt;
                    this.BindListView_Attachments(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    int blob;
                    blob = Convert.ToInt32(objCommon.FillDropDown("ADMN_DC_DOCUMENT_UPLOAD", "ISBLOB", "", "UPLNO='" + Convert.ToInt32(UPLNO) + "'", ""));


                    if (blob == 1)
                    {
                        Control ctrHeader = lvAttach.FindControl("divBlobDownload");
                        Control ctrHead1 = lvAttach.FindControl("divattachblob");
                        Control ctrhead2 = lvAttach.FindControl("divattach");
                        ctrHeader.Visible = true;
                        ctrHead1.Visible = true;
                        ctrhead2.Visible = false;

                        foreach (ListViewItem lvRow in lvAttach.Items)
                        {
                            Control ckBox = (Control)lvRow.FindControl("tdBlob");
                            Control ckattach = (Control)lvRow.FindControl("attachfile");
                            Control attachblob = (Control)lvRow.FindControl("attachblob");
                            ckBox.Visible = true;
                            attachblob.Visible = true;
                            ckattach.Visible = false;

                        }
                    }
                    else
                    {



                        Control ctrHeader = lvAttach.FindControl("divDownload");

                        ctrHeader.Visible = false;


                        foreach (ListViewItem lvRow in lvAttach.Items)
                        {
                            Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");

                            ckBox.Visible = false;

                        }
                    }

                }

            }
            else 
            {
                lvAttach.DataSource = null;
                lvAttach.DataBind();
            }
            LVCATDOC.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Clear();
        Session["Attachments"] = null;

    }

    private string ValTitle()
    {
        string msg = string.Empty;

        string title = objCommon.LookUp("ADMN_DC_DOCUMENT_UPLOAD", "TITLE", "LOWER(TITLE) LIKE '" + txtTitle.Text + "' AND DNO=" + ddlCategory.SelectedValue);
        if (title != string.Empty)
            msg = "Title already exist please spacify a different name";
        return msg;
    }
    private void Clear()
    {
        ddlCategory.Enabled = true;
        txtKeyword.Text = string.Empty;
        txtTitle.Text = string.Empty;
        chkId.Checked = false;
        ddlCategory.SelectedIndex = 0;
        ftbDescription.Text = string.Empty;
        lvAttach.DataSource = null;
        lvAttach.DataBind();
        //chkLstDept .DataSource = null;
        //chkLstDept .DataBind();
        lbl.Text = "";
        LVCATDOC.DataSource = null;
        LVCATDOC.DataBind();
        ViewState["action"] = "add";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = true;
        Pancatgrid.Visible = true;
        pnlList.Visible = false;
        Clear();
        lnkAdd.Visible = false;
        pnlButton.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        Pancatgrid.Visible = false;
        pnlList.Visible = true;
        Bind();
        LVCATDOC.DataSource = null;
        LVCATDOC.DataBind();
        pnlButton.Visible = false;
        lnkAdd.Visible = true;
        lvAttach.DataSource = null;
        lvAttach.DataBind();
    }

    //UPLOAD FILES
    //protected void jQueryUploadFiles(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        obj.DNO = Convert.ToInt32(ddlCategory.SelectedValue);
    //        obj.DESCRIPTION = ftbDescription.Text.Trim();
    //        obj.COLLEGE_CODE = Session["colcode"].ToString();
    //        obj.KEYWORD = Convert.ToString(txtKeyword.Text.Trim());
    //        obj.TITLE = Convert.ToString(txtTitle.Text.Trim());
    //        obj.SHARED = chkId.Checked ? Convert.ToChar('Y') : Convert.ToChar('N');
    //        obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());


    //        if (ViewState["action"] != null)
    //        {
    //            if (ViewState["action"].ToString().Equals("add"))
    //            {
    //                int obje = Convert.ToInt32(objC.AddDocument(obj));
    //                HttpFileCollection files = Request.Files;
    //                for (int i = 0; i < files.Count; i++)
    //                {
    //                    HttpPostedFile file = files[i];
    //                    if (file.ContentLength > 0)
    //                    {
    //                        string path = Server.MapPath("~/FILEUPLOAD/");
    //                        string fileName = Path.GetFileName(file.FileName);

    //                        //
    //                        DirectoryInfo d = new DirectoryInfo(path);
    //                        FileInfo[] f= d.GetFiles();
    //                       foreach(FileInfo oFile in f)
    //                       {
    //                           if (oFile.Name == fileName)
    //                           {

    //                               //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "window.setTimeout('PopupModal()',50);", true);
    //                               //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);


    //                               // btnAskSave_Click(sender, e);
    //                              // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "return AskSave();", true);
    //                            //   ClientScript.RegisterClientScriptBlock(this.GetType(), "AskSave", "return AskSave();", true);
    //                               //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", "return AskSave();", true);
    //                               //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "AskSave()", true);
    //                               //objCommon.ConfirmMessage(this.UPDLedger, "aa", this.Page);
    //                              // ScriptManager.RegisterClientScriptBlock(this.UPDLedger, this.UPDLedger.GetType(), "Message", "return confirm('" + "aa" + "');", true);
    //                               ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "return confirm('Changing the language will clear. Click OK to proceed.');", true);



    //                               if (hdnAskSave.Value == "1")
    //                               {
    //                                   // now save the file to the disk
    //                                   file.SaveAs(path + fileName);

    //                                   //string filePath = "ITLE\\UPLOAD_FILES\\" + fileName;

    //                                   obj.FILENAME = Convert.ToString(fileName);
    //                                   obj.SIZE = (file.ContentLength / 1024);
    //                                   obj.ORIGINAL_FILENAME = Path.GetFileName(file.FileName);
    //                                   obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
    //                                   obj.UPLNO = Convert.ToInt32(obje);
    //                                   obj.FILE_PATH = "FILEUPLOAD\\" + fileName;

    //                                   CustomStatus cs = (CustomStatus)objC.AddFile(obj);
    //                               }



    //                           }
    //                       }
    //                    }
    //                }
    //                if (Convert.ToInt32(obje) != -1)
    //                {
    //                    Clear();
    //                    objCommon.DisplayMessage("Record Save Succesfully", this.Page);
    //                    Bind();
    //                }
    //            }
    //            else
    //            {
    //                if (ViewState["action"].ToString().Equals("edit"))
    //                {
    //                    obj.UPLNO = Convert.ToInt32(Session["UPLNO"].ToString());
    //                    int no = Convert.ToInt32(objC.UpdateDocument(obj));

    //                    HttpFileCollection files = Request.Files;
    //                    for (int i = 0; i < files.Count; i++)
    //                    {
    //                        HttpPostedFile file = files[i];
    //                        if (file.ContentLength > 0)
    //                        {
    //                            string path = Server.MapPath("~/FILEUPLOAD/");
    //                            string fileName = Path.GetFileName(file.FileName);



    //                            // now save the file to the disk
    //                            file.SaveAs(path + fileName);

    //                            //string filePath = "ITLE\\UPLOAD_FILES\\" + fileName;

    //                            obj.FILENAME = Convert.ToString(fileName);
    //                            obj.SIZE = (file.ContentLength / 1024);
    //                            obj.ORIGINAL_FILENAME = Path.GetFileName(file.FileName);
    //                            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
    //                            obj.UPLNO = Convert.ToInt32(Session["UPLNO"].ToString());
    //                            obj.FILE_PATH = "FILEUPLOAD\\" + fileName;

    //                            CustomStatus cse = (CustomStatus)objC.AddFile(obj);
    //                        }
    //                    }
    //                    if (no!=-1)
    //                    {
    //                        Clear();
    //                        objCommon.DisplayMessage("Record Updated Sucessfully",this.Page);
    //                    }
    //                }
    //            }
    //        }
    //        //if (Convert.ToInt32(obj) != -99)
    //        //        {
    //        //            Clear();
    //        //            objCommon.DisplayMessage("Record Save Successfully", this.Page);
    //        //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnAskSave_Click(object sender, EventArgs e)
    {

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get path of directory according to category
        string path1 = "";
        DataSet ds = objC.GetNestedPath(Convert.ToInt32(ddlCategory.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            path1 = path1 + ds.Tables[0].Rows[0]["PATH"].ToString();
        }
        lbl.Text = "Path : " + path1;
        BindDocCategorywise(Convert.ToInt32(ddlCategory.SelectedValue));
        lvAttach.DataSource = null;
        lvAttach.DataBind();
        LVCATDOC.Visible = true;
    }

    protected void BindDocCategorywise(int category)
    {
        try
        {
            obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            obj.DNO = category;
            DataSet ds = objC.RetrieveDocumentByCategory(obj);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LVCATDOC.DataSource = ds;
                LVCATDOC.DataBind();
            }
            else
            {
                LVCATDOC.DataSource = null;
                LVCATDOC.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.BindDocCategorywise-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["UA_NO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }
    // This method is used to delete files from physical location and from table also.
    protected void lnkDelete_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = Convert.ToInt32(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

            //to permanently delete from database in case of Edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                string count = objCommon.LookUp("ACD_IASSIGNMENT_ATTACHMENTS", "ATTACHMENT_ID", "AS_NO =" + Convert.ToInt32(ViewState["assignno"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ACD_IASSIGNMENT_ATTACHMENTS", "AS_NO =" + Convert.ToInt32(ViewState["assignno"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        // LinkButton lnk = sender as  LinkButton;
        //// HiddenField hdn = sender as HiddenField;
        // string filePath = lnk.CommandArgument;
        // string IDNO = lnk.ToolTip;
        // string UPLNO = lnk.CommandName;

        // if (File.Exists(filePath)) // + "\\" + fileName))
        // {
        //     //DELETING THE FILE
        //     File.Delete(filePath ) ; //+ "\\" + fileName);
        //     ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted.');", true);
        // }
        // if (IDNO != "")
        // {
        //     objCommon.DeleteRow("ADMN_DC_DOCUMENT_FILE", "IDNO=" + IDNO);
        // }
        // obj.UPLNO = Convert.ToInt32(UPLNO);
        // obj.UA_NO = Convert.ToInt32(Session["userno"].ToString());

        // DataSet d = objC.RetrieveDocumentFiles(obj);

        // if (d.Tables[0].Rows.Count > 0)
        // {
        //     lvAttachments.DataSource = d;
        //     lvAttachments.DataBind();
        // }
        // else
        // {
        //     lvAttachments.DataSource = null;
        //     lvAttachments.DataBind();
        // }

    }

    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId + " and OrganizationId=" + Convert.ToInt32(Session["OrgId"])));
                //lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_ADMIN,'Bytes')AS FILE_SIZE_ADMIN", "PAGE_ID=" + PageId);
            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId + " and OrganizationId=" + Convert.ToInt32(Session["OrgId"])));
                    // lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_STUDENT,'Bytes')AS FILE_SIZE_STUDENT", "PAGE_ID=" + PageId);
                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));

                    //lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId + " and OrganizationId=" + Convert.ToInt32(Session["OrgId"]));

                }



        }
        catch (Exception ex)
        {

        }

    }


    //protected void btnAttachFile_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bool result;



    //        GetAttachmentSize();

    //        string filename = string.Empty;
    //        string FilePath = string.Empty;
    //        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");

    //        if (FileUpload10.HasFile)
    //        {

    //            string DOCFOLDER = file_path + "DOCUMENTANDSCANNING\\FILEUPLOAD";

    //            if (!System.IO.Directory.Exists(DOCFOLDER))
    //            {
    //                System.IO.Directory.CreateDirectory(DOCFOLDER);

    //            }
    //            string fileName = System.Guid.NewGuid().ToString() + FileUpload10.FileName.Substring(FileUpload10.FileName.IndexOf('.'));

    //            string contentType = contentType = FileUpload10.PostedFile.ContentType;
    //            string ext = System.IO.Path.GetExtension(FileUpload10.PostedFile.FileName);

    //            HttpPostedFile file = FileUpload10.PostedFile;


    //            filename = filename + time;

    //            obj.ATTACHMENT = filename;
    //            obj.FILEPATH = FileUpload10.FileName;


    //            int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + ext.ToString() + "' AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + ""));

    //            DataSet dsPURPOSE = new DataSet();

    //            //dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");

    //            if (count != 0)
    //            {
    //                string filePath = file_path + "DOCUMENTANDSCANNING\\FILEUPLOAD" + fileName;


    //                if (FileUpload10.PostedFile.ContentLength < File_size)
    //                {
    //                    FileUpload10.SaveAs(filePath);

    //                    string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
    //                    string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
    //                    result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

    //                    if (result == true)
    //                    {

    //                        int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, FileUpload10);
    //                        if (retval == 0)
    //                        {
    //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
    //                            return;
    //                        }

    //                        obj.FILEPATH = FileUpload10.FileName;


    //                    }
    //                    else
    //                    {
    //                        obj.FILEPATH = file_path + "DOCUMENTANDSCANNING\\FILEUPLOAD" + fileName;

    //                    }

    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
    //                    return;
    //                }

    //                DataTable dt;
    //                int maxVal = 0;

    //                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
    //                {
    //                    dt = ((DataTable)Session["Attachments"]);
    //                    DataRow dr = dt.NewRow();

    //                    if (dt != null && dt.Rows.Count > 0)
    //                    {
    //                        // dr["ATTACH_ID"] = dt.Rows.Count + 1;
    //                        if (dt != null)
    //                        {
    //                            maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATTACH_ID"]));
    //                        }
    //                        dr["ATTACH_ID"] = maxVal + 1;
    //                        if (result == true)
    //                        {
    //                            dr["ORIGINAL_FILENAME"] = filename + ext;
    //                        }
    //                        else
    //                        {
    //                            dr["ORIGINAL_FILENAME"] = FileUpload10.FileName;
    //                        }

    //                        dr["FILE_PATH"] = obj.FILEPATH;
    //                        dr["SIZE"] = (FileUpload10.PostedFile.ContentLength);
    //                        //  dr["UPLNO"] = 0;

    //                        // dr["IDNO"] = Convert.ToInt32(Session["userno"].ToString()); ;
    //                        dt.Rows.Add(dr);
    //                        Session["Attachments"] = dt;
    //                        this.BindListView_Attachments(dt);

    //                    }
    //                    else
    //                    {
    //                        dt = this.GetAttachmentDataTable();
    //                        dr = dt.NewRow();
    //                        //dr["ATTACH_ID"] = dt.Rows.Count + 1;
    //                        if (dt != null)
    //                        {
    //                            maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATTACH_ID"]));
    //                        }
    //                        dr["ATTACH_ID"] = maxVal + 1;
    //                        if (result == true)
    //                        {
    //                            dr["ORIGINAL_FILENAME"] = filename + ext;
    //                        }
    //                        else
    //                        {
    //                            dr["ORIGINAL_FILENAME"] = FileUpload10.FileName;
    //                        }
    //                        dr["FILE_PATH"] = obj.FILEPATH;
    //                        dr["SIZE"] = (FileUpload10.PostedFile.ContentLength);
    //                        //  dr["UPLNO"] = 0;

    //                        // dr["IDNO"] = Convert.ToInt32(Session["userno"].ToString()); ;
    //                        dt.Rows.Add(dr);
    //                        Session.Add("Attachments", dt);
    //                        this.BindListView_Attachments(dt);
    //                    }
    //                }
    //                else
    //                {
    //                    dt = this.GetAttachmentDataTable();
    //                    DataRow dr = dt.NewRow();
    //                    // dr["ATTACH_ID"] = dt.Rows.Count + 1;
    //                    if (dt != null)
    //                    {
    //                        maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATTACH_ID"]));
    //                    }
    //                    dr["ATTACH_ID"] = maxVal + 1;
    //                    if (result == true)
    //                    {
    //                        dr["ORIGINAL_FILENAME"] = filename + ext;
    //                    }
    //                    else
    //                    {
    //                        dr["ORIGINAL_FILENAME"] = FileUpload10.FileName;
    //                    }
    //                    dr["FILE_PATH"] = obj.FILEPATH;
    //                    dr["SIZE"] = (FileUpload10.PostedFile.ContentLength);
    //                    //  dr["UPLNO"] = 0;

    //                    // dr["IDNO"] = Convert.ToInt32(Session["userno"].ToString()); ;
    //                    dt.Rows.Add(dr);
    //                    Session.Add("Attachments", dt);
    //                    this.BindListView_Attachments(dt);
    //                }
    //            }
    //            // else
    //            // {
    //            //    string Extension = "";
    //            //    for (int i = 0; i < dsPURPOSE.Tables[0].Rows.Count; i++)
    //            //    {
    //            //        if (Extension == "")
    //            //            Extension = dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
    //            //        else
    //            //            Extension = Extension + ", " + dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
    //            //    }
    //            //    objCommon.DisplayMessage("Upload Supported File Format.Please Upload File In " + Extension, this);
    //            //  }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Please select a file to attach.", this);
    //        }
    //    }


    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "assignmentMaster.btnAttachFile_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}



    protected void btnAttachFile_Click(object sender, EventArgs e)
    {
        try
        {
            bool result;

            GetAttachmentSize();

            string filename = string.Empty;
            string FilePath = string.Empty;
            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");

            if (FileUpload10.HasFile)
            {

                string DOCFOLDER = file_path + "DOCUMENTANDSCANNING\\FILEUPLOAD";

                //if (!System.IO.Directory.Exists(DOCFOLDER))
                //{
                //    System.IO.Directory.CreateDirectory(DOCFOLDER);

                //}
                string fileName = System.Guid.NewGuid().ToString() + FileUpload10.FileName.Substring(FileUpload10.FileName.IndexOf('.'));

                string contentType = contentType = FileUpload10.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(FileUpload10.PostedFile.FileName);




                filename = filename + time;

                obj.ATTACHMENT = filename;
                obj.FILEPATH = FileUpload10.FileName;


                // int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + ext.ToString() + "'"));

                DataSet dsPURPOSE = new DataSet();

                //dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");



                string filePath = file_path + "DOCUMENTANDSCANNING\\FILEUPLOAD" + fileName;


                //if (FileUpload10.PostedFile.ContentLength < File_size)
                //{
                //  FileUpload10.SaveAs(filePath);

                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                if (result == true)
                {

                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, FileUpload10);
                    if (retval == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                        return;
                    }

                    obj.FILEPATH = FileUpload10.FileName;


                }
                else
                {
                    obj.FILEPATH = file_path + "DOCUMENTANDSCANNING\\FILEUPLOAD" + fileName;
                    HttpPostedFile file = FileUpload10.PostedFile;
                }

                //}
                //else
                //{
                //    objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                //    return;
                //}

                DataTable dt;
                int maxVal = 0;

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    dt = ((DataTable)Session["Attachments"]);
                    DataRow dr = dt.NewRow();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dr["IDNO"] = Convert.ToInt32(Session["userno"].ToString());
                        dr["UPLNO"] = Convert.ToInt32(Session["UPLNO"]);
                        dr["FILENAME"] = filename;
                        dr["FILEPATH"] = obj.FILEPATH;
                        dr["SIZE"] = (FileUpload10.PostedFile.ContentLength);
                        if (result == true)
                        {
                            dr["ORIGINAL_FILENAME"] = filename + ext;
                        }
                        else
                        {
                            dr["ORIGINAL_FILENAME"] = FileUpload10.FileName;
                        }
                        // dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (dt != null)
                        {
                            // maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UA_NO"]));
                        }
                        dr["UA_NO"] = maxVal + 1;

                        dt.Rows.Add(dr);
                        Session["Attachments"] = dt;
                        this.BindListView_Attachments(dt);

                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        dr = dt.NewRow();
                        //dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (dt != null)
                        {
                            maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UA_NO"]));
                        }

                        dr["IDNO"] = Convert.ToInt32(Session["userno"].ToString());
                        dr["UPLNO"] = Convert.ToInt32(Session["UPLNO"]);
                        dr["FILENAME"] = filename;
                        dr["FILEPATH"] = obj.FILEPATH;
                        dr["SIZE"] = (FileUpload10.PostedFile.ContentLength);
                        if (result == true)
                        {
                            dr["ORIGINAL_FILENAME"] = filename + ext;
                        }
                        else
                        {
                            dr["ORIGINAL_FILENAME"] = FileUpload10.FileName;
                        }
                        // dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (dt != null)
                        {
                            //maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UA_NO"]));
                        }
                        dr["UA_NO"] = maxVal + 1;

                        dt.Rows.Add(dr);
                        Session["Attachments"] = dt;
                        this.BindListView_Attachments(dt);
                    }
                }
                else
                {
                    dt = this.GetAttachmentDataTable();
                    DataRow dr = dt.NewRow();
                    // dr["ATTACH_ID"] = dt.Rows.Count + 1;
                    if (dt != null)
                    {
                        maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UA_NO"]));
                    }

                    dr["IDNO"] = Convert.ToInt32(Session["userno"].ToString());
                    dr["UPLNO"] = Convert.ToInt32(Session["UPLNO"]);
                    dr["FILENAME"] = filename;
                    dr["FILEPATH"] = obj.FILEPATH;
                    dr["SIZE"] = (FileUpload10.PostedFile.ContentLength);
                    if (result == true)
                    {
                        dr["ORIGINAL_FILENAME"] = filename + ext;
                    }
                    else
                    {
                        dr["ORIGINAL_FILENAME"] = FileUpload10.FileName;
                    }
                    // dr["ATTACH_ID"] = dt.Rows.Count + 1;
                    if (dt != null)
                    {
                        //maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["UA_NO"]));
                    }
                    dr["UA_NO"] = maxVal + 1;

                    dt.Rows.Add(dr);
                    Session["Attachments"] = dt;
                    this.BindListView_Attachments(dt);
                }

                // else
                // {
                //    string Extension = "";
                //    for (int i = 0; i < dsPURPOSE.Tables[0].Rows.Count; i++)
                //    {
                //        if (Extension == "")
                //            Extension = dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                //        else
                //            Extension = Extension + ", " + dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                //    }
                //    objCommon.DisplayMessage("Upload Supported File Format.Please Upload File In " + Extension, this);
                //  }
            }
            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "assignmentMaster.btnAttachFile_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void BindListView_Attachments(DataTable dt)
    {

        #region
        //divAttch.Style["display"] = "block";
        // lvCompAttach.DataSource = dt;
        // lvCompAttach.DataBind();


        //if (lblBlobConnectiontring.Text != "")
        //{
        //    Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
        //    Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
        //    Control ctrhead2 = lvCompAttach.FindControl("divattach");
        //    ctrHeader.Visible = true;
        //    ctrHead1.Visible = true;
        //    ctrhead2.Visible = false;

        //    foreach (ListViewItem lvRow in lvCompAttach.Items)
        //    {
        //        Control ckBox = (Control)lvRow.FindControl("tdBlob");
        //        Control ckattach = (Control)lvRow.FindControl("attachfile");
        //        Control attachblob = (Control)lvRow.FindControl("attachblob");
        //        ckBox.Visible = true;
        //        attachblob.Visible = true;
        //        ckattach.Visible = false;

        //    }
        //}
        //else
        //{



        //    Control ctrHeader = lvCompAttach.FindControl("divDownload");
        //    ctrHeader.Visible = false;

        //    foreach (ListViewItem lvRow in lvCompAttach.Items)
        //    {
        //        Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
        //        ckBox.Visible = false;

        //    }
        //}
        #endregion

        //lvAttach.DataSource = dt;
        //lvAttach.DataBind();


        try
        {
            divAttch.Style["display"] = "block";
            lvAttach.DataSource = dt;
            lvAttach.DataBind();


            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvAttach.FindControl("divattachblob");
                Control ctrhead2 = lvAttach.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in lvAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;

                }
            }
            else
            {
                Control ctrHeader = lvAttach.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in lvAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }



    private DataTable GetAttachmentDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("IDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("UPLNO", typeof(int)));
        dt.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        dt.Columns.Add(new DataColumn("SIZE", typeof(int)));
        dt.Columns.Add(new DataColumn("ORIGINAL_FILENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("UA_NO", typeof(int)));
        return dt;
    }


    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = int.Parse(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

            //to permanently delete from database in case of Edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                string count = objCommon.LookUp("ADMN_DC_DOCUMENT_FILE", "UPLNO,UA_NO", "UPLNO =" + Convert.ToInt32(Session["UPLNO"]) + "AND UA_NO =" + Convert.ToInt32(fileId));
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ADMN_DC_DOCUMENT_FILE", "UPLNO =" + Convert.ToInt32(Session["UPLNO"]) + "AND UA_NO=" + Convert.ToInt32(fileId));
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "Documentandscanning";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {

            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DOCUMENTANDSCANNING" + "/";
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
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                // ltEmbed.Text = "Image Not Found....!";


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DOCUMENTANDSCANNING/" + ImageName));

                hdnfilename.Value = filePath;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BTNCLOSEData_Click(object sender, EventArgs e)
    {
        string directoryPath = Server.MapPath("~/DOCUMENTANDSCANNING/");

        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                if (file == hdnfilename.Value)
                {
                    File.Delete(file);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                }
            }
        }
    }
}
