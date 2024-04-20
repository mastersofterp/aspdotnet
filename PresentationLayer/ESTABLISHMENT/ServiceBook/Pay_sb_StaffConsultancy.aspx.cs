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
using System.IO;
using IITMS.UAIMS;
using System.Configuration;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class ESTABLISHMENT_ServiceBook_Pay_sb_StaffConsultancy : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;
    public static int v = 0;
    public string path = string.Empty;
    public string Docpath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/");
    public static string RETPATH = "";
    BlobController objBlob = new BlobController();

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
            DeleteDirecPath(Docpath + "TEMP_UPLOADFILES\\" + _idnoEmp + "\\APP_0");
        }

        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue); 
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        if (_idnoEmp != 0)
        {
            BindListViewStaffConsultancy();
            btnSubmit.Enabled = true;
        }
        else
        {
            //MessageBox("Invalid Login! Please Login Again");
            btnSubmit.Enabled = false;
            //return;
        }
        BlobDetails();
        GetConfigForEditAndApprove();
    }

    private void BindListViewStaffConsultancy()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllStaffConsultancyEmployee(_idnoEmp);
            lvAchiveInfo.DataSource = ds;
            lvAchiveInfo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.BindListViewFamilyInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime DtFrom, DtTo;
            if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtDateOftalk.Text != string.Empty && txtDateOftalk.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtDateOftalk.Text.ToString());
                DtTo = Convert.ToDateTime(txtToDate.Text.ToString());
                if (DtFrom > DtTo)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    return;
                }
            }
            if (_idnoEmp != 0)
            {

            }
            else
            {
                MessageBox("Invalid Login! Please Login Again");
                btnSubmit.Enabled = false;
                return;
            }
            DataTable dt = null;
            if (ViewState["FILE1"] != null)
            {
                dt = (DataTable)ViewState["FILE1"];
            }

            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.Name_org = txtname.Text;
            objSevBook.ADDRESS = txtadd.Text;
            objSevBook.TITLE = txtTitle.Text;
            objSevBook.FROMDATE = Convert.ToDateTime(txtDateOftalk.Text);
            objSevBook.TODATE = Convert.ToDateTime(txtToDate.Text);
            objSevBook.DURATION = txtConsultancy.Text;

            if (txtAmount.Text != string.Empty)
            {
                objSevBook.AMOUNT = Convert.ToDecimal(txtAmount.Text);
            }
            else
            {
                objSevBook.AMOUNT = 0;
            }
            if (txtnaturework.Text != string.Empty)
            {
                objSevBook.NatureOfWorkText = txtnaturework.Text;
            }
            else
            {
                objSevBook.NatureOfWorkText = string.Empty;
            }
            if (txtDescription.Text != string.Empty)
            {
                objSevBook.Description = txtDescription.Text;
            }
            else
            {
                objSevBook.Description = string.Empty;
            }

            if (Session["colcode"] != null) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Changes done for Blob
            if (lblBlobConnectiontring.Text == "")
            {
                objSevBook.ISBLOB = 0;
            }
            else
            {
                objSevBook.ISBLOB = 1;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objServiceBook.AddStaffConsultancy(objSevBook, dt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string SCNO = objCommon.LookUp("PAYROLL_SB_StaffConsultancy", "MAX(SCNO)", "");
                                AddDocuments(Convert.ToInt32(SCNO));
                            }
                        }
                        this.Clear();
                        this.BindListViewStaffConsultancy();
                        DeletePath();
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exits ");
                        this.Clear();
                    }
                }
                else
                {
                    //Edit  <%-- //Modified by Saahil Trivedi 20/07/2022--%>
                    if (ViewState["SCNO"] != null)
                    {
                        objSevBook.SCNO = Convert.ToInt32(ViewState["SCNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateStaffConsultancyInfo(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string SCNO = ViewState["SCNO"].ToString();
                                    AddDocuments(Convert.ToInt32(SCNO));
                                }
                            }
                            ViewState["action"] = "add";
                            //ViewState["SCNO"] = null;
                            this.Clear();
                            this.BindListViewStaffConsultancy();
                            DeletePath();
                            MessageBox("Record Updated Successfully");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["FILE1"] = null;
            // lblFamilymsg.Text = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int SCNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(SCNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int SCNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleStaffConsultancyOfEmployee(SCNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SCNO"] = SCNO.ToString();
                txtname.Text = ds.Tables[0].Rows[0]["Name_Of_Org"].ToString();
                txtadd.Text = ds.Tables[0].Rows[0]["ORG_ADDRESS"].ToString();
                txtDateOftalk.Text = ds.Tables[0].Rows[0]["From_Date"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["To_Date"].ToString();
                txtConsultancy.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["Amount_Earned"].ToString();
                txtnaturework.Text = ds.Tables[0].Rows[0]["Nature_of_work"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
            }

            if (Convert.ToInt32(ds.Tables[1].Rows.Count) > 0)
            {
                int rowCount = ds.Tables[1].Rows.Count;
                CreateTable();
                DataTable dtM = (DataTable)ViewState["FILE1"];
                for (int i = 0; i < rowCount; i++)
                {
                    DataRow dr = dtM.NewRow();
                    dr["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                    dr["FILEPATH"] = Docpath + "StaffConsultancy" + ViewState["idno"] + "\\APP_" + SCNO;
                    dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                    dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                    dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                    dr["FOLDER"] = "StaffConsultancy";
                    dr["APPID"] = SCNO.ToString();
                    dr["FILENAME"] = ds.Tables[1].Rows[i]["FILENAME"].ToString();
                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["FILE1"] = dtM;
                    ViewState["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                }
                //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
                //LVFiles.DataBind();
                //pnlfiles.Visible = true;

                this.BindListView_Attachments(dtM);
                pnlAttachmentList.Visible = true;
            }
            else
            {
                //pnlfiles.Visible = false;
                //LVFiles.DataSource = null;
                //LVFiles.DataBind();
                pnlAttachmentList.Visible = false;
                lvCompAttach.DataSource = null;
                lvCompAttach.DataBind();
            }
            if (Convert.ToBoolean(ViewState["IsApprovalRequire"]) == true)
            {
                string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                if (STATUS == "A")
                {
                    MessageBox("Your Details Are Approved You Cannot Edit.");
                    btnSubmit.Enabled = false;
                    return;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
                GetConfigForEditAndApprove();
            }
            else
            {
                btnSubmit.Enabled = true;
                GetConfigForEditAndApprove();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lblFamilymsg.Text = string.Empty;
            ImageButton btnDel = sender as ImageButton;
            int SCNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_StaffConsultancy", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "SCNO=" + SCNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved You Cannot Delete.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteStaffConsultancyInfo(SCNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    BindListViewStaffConsultancy();
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        DeleteDirecPath(Docpath + "TEMP_UPLOADFILES\\" + _idnoEmp + "\\APP_0");
        GetConfigForEditAndApprove();
    }

    private void Clear()
    {
        txtname.Text = txtadd.Text = txtDateOftalk.Text = txtToDate.Text = txtConsultancy.Text = txtAmount.Text = txtnaturework.Text = txtDescription.Text = string.Empty;
        txtTitle.Text = string.Empty;
        ViewState["action"] = "add";

        //LVFiles.DataSource = null;
        //LVFiles.DataBind();
        //pnlfiles.Visible = false;
        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        pnlAttachmentList.Visible = false;
        ViewState["FILE1"] = null;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #region Upload File
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ServiceBook objSevBook = new ServiceBook();
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileContent.Length >= 1024 * 10000)
                        {

                            MessageBox("File Size Should Not Be Greater Than 10 Mb");
                            FileUpload1.Dispose();
                            FileUpload1.Focus();
                            return;
                        }
                    }
                    if (Session["serviceIdNo"] != null && Convert.ToInt32(Session["serviceIdNo"]) != 0)
                    {
                        idno = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
                    }
                    else
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    string FileName = FileUpload1.FileName;
                    if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["FILE1"];
                        for (int i = 0; i < dtM.Rows.Count; i++)
                        {
                            if (dtM.Rows[i]["DisplayFileName"].ToString() == FileName)
                            {
                                MessageBox("File Already Exist!");
                                return;
                            }
                        }
                    }

                    string file = Docpath + "TEMP_UPLOADFILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "StaffConsultancy\\" + idno;
                    ViewState["DESTINATION_PATH"] = PATH;
                    if (lblBlobConnectiontring.Text == "")
                    {
                        objSevBook.ISBLOB = 0;
                    }
                    else
                    {
                        objSevBook.ISBLOB = 1;
                    }
                    if (objSevBook.ISBLOB == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        string IdNo = _idnoEmp.ToString();
                        if (FileUpload1.HasFile)
                        {
                            string contentType = contentType = FileUpload1.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                            //HttpPostedFile file = flupld.PostedFile;
                            //filename = objSevBook.IDNO + "_familyinfo" + ext;
                            //string name = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = IdNo + "_consultancy_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (FileUpload1.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_consultancy_" + time, FileUpload1);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    int stcno = Addfieldstotbl(filename);
                                    //BindListView_Attachments();
                                }
                            }
                        }
                    }
                    else
                    {
                        string filename = FileUpload1.FileName;
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                int stcno = Addfieldstotbl(filename);
                                path = file + "\\STC_" + stcno + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                                FileUpload1.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]", this.Page);
                    FileUpload1.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private int Addfieldstotbl(string filename)
    {
        if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "StaffConsultancy" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "STC_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_UPLOADFILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = ViewState["FILE1"];
            //LVFiles.DataBind();
            //lvCompAttach.DataSource = ViewState["FILE1"];
            //lvCompAttach.DataBind();
            this.BindListView_Attachments(dt);
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "StaffConsultancy" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "STC_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_UPLOADFILES";
            dr["APPID"] = 0;
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
            //LVFiles.DataBind();
            //pnlfiles.Visible = true;
            //lvCompAttach.DataSource = ViewState["FILE1"];
            //lvCompAttach.DataBind();
            this.BindListView_Attachments(dt);
            pnlAttachmentList.Visible = true;
        }
        return Convert.ToInt32(ViewState["FUID"]);
    }
    private void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn("FUID", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FILEPATH", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("GETFILE", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("IDNO", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FOLDER", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("APPID", typeof(int));
        dt.Columns.Add(dc);

        dc = new DataColumn("FILENAME", typeof(string));
        dt.Columns.Add(dc);

        ViewState["FILE1"] = dt;
    }
    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    public string GetFileNamePath(object filename, object SCNO, object idno, object folder, object AppID)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/STC_" + SCNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    private void AddDocuments(int SCNO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            int idno = _idnoEmp;

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\APP_" + SCNO;

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
            DeleteDirectory(sourcePath);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.AddDocuments-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            //Delete all files from the Directory
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            //Delete all child Directories
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            //Delete a Directory
            Directory.Delete(path);
        }
    }
    protected void btnDelFile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            int appid = Convert.ToInt32(btnDelete.AlternateText);
            if (appid != 0)
            {
                path = Docpath + "StaffConsultancy" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["SCNO"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_UPLOADFILES" + "\\" + idno + "\\APP_" + appid;
            }

            if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
            {
                DataTable dt = (DataTable)ViewState["FILE1"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, fname));
                ViewState["FILE1"] = dt;
                //LVFiles.DataSource = dt;
                //LVFiles.DataBind();     
                BindListView_Attachments(dt);
                //lvCompAttach.DataSource = dt;
                //lvCompAttach.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);

                if (ViewState["DELETE_BILLS"] != null && ((DataTable)ViewState["DELETE_BILLS"]) != null)
                {
                    DataTable dtD = (DataTable)ViewState["DELETE_BILLS"];
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = path;
                    dr["FILENAME"] = fname;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BILLS"] = dtD;
                }
                else
                {
                    DataTable dtD = this.CreateTableBill();
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = path;
                    dr["FILENAME"] = fname;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_BILLS"] = dtD;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private DataTable CreateTableBill()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }
    private DataRow GetEditableDatarowBill(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["GETFILE"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return datRow;
    }
    private void DeletePath()
    {
        if (ViewState["DELETE_BILLS"] != null && ((DataTable)ViewState["DELETE_BILLS"]) != null)
        {
            int i = 0;
            DataTable DtDel = (DataTable)ViewState["DELETE_BILLS"];
            foreach (DataRow Dr in DtDel.Rows)
            {
                string filename = DtDel.Rows[i]["FILENAME"].ToString();
                string filepath = DtDel.Rows[i]["FILEPATH"].ToString();

                if (File.Exists(filepath + "\\" + filename))
                {
                    File.Delete(filepath + "\\" + filename);
                }
                i++;
            }
            ViewState["DELETE_BILLS"] = null;
        }
    }
    private void DeleteDirecPath(string FilePath)
    {
        if (System.IO.Directory.Exists(FilePath))
        {
            try
            {
                System.IO.Directory.Delete(FilePath, true);
            }

            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    #endregion

    //protected void txtDateOftalk_TextChanged(object sender, EventArgs e)
    //{
    //    txtToDate.Text = string.Empty;
    //}
    //protected void txtToDate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime DtFrom, DtTo, Test;
    //    if (DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
    //    {
    //        if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtDateOftalk.Text != string.Empty && txtDateOftalk.Text != "__/__/____")
    //        {
    //            DtFrom = Convert.ToDateTime(txtDateOftalk.Text.ToString());

    //            DtTo = Convert.ToDateTime(txtToDate.Text.ToString());

    //            if (DtFrom > DtTo)
    //            {
    //                MessageBox("To Date Should Be Larger Than Or Equals To From Date");
    //                btnSubmit.Enabled = false;                   
    //                return;
    //            }
    //            else
    //            {                   
    //                btnSubmit.Enabled = true;   
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //txtToDate.Text = string.Empty;
    //        //btnSubmit.Enabled = true;   
    //    }
    //}

    #region Blob
    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameEmployee";
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

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                Control ctrhead2 = lvCompAttach.FindControl("divattach");
                Control ctrHead3 = lvCompAttach.FindControl("divDownload");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;
                ctrHead3.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    Control download = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;
                    download.Visible = false;
                }
            }
            else
            {

                Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                Control ctrhead2 = lvCompAttach.FindControl("divattach");
                Control ctrHead3 = lvCompAttach.FindControl("divDownload");
                ctrHeader.Visible = false;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;
                ctrHead3.Visible = true;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    Control download = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;
                    attachblob.Visible = false;
                    ckattach.Visible = true;
                    download.Visible = true;

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

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Staff Consultancy";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSubmit.Enabled = true;
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