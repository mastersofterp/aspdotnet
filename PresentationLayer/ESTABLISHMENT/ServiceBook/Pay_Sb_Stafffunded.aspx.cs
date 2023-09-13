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
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Stafffunded : System.Web.UI.Page
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
            FillDropDown();
            DeleteDirecPath(Docpath + "TEMP_FUNDFILES\\" + _idnoEmp + "\\APP_0");
        }

        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue); 
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        if (_idnoEmp != 0)
        {
            BindListViewStaffFunded();

            btnSubmit.Enabled = true;
        }
        else
        {
            //MessageBox("Invalid Login! Please Login Again");
            btnSubmit.Enabled = false;
            //return;
        }
        txtweblink.Attributes.Add("maxlength", txtweblink.MaxLength.ToString());
        BlobDetails();
    }


    private void FillDropDown()
    {

        objCommon.FillDropDownList(ddlInvestigator, "PAYROLL_EMPMAS EM INNER JOIN USER_ACC A  ON(EM.IDNO=A.UA_IDNO)", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = A.UA_IDNO and EM.IDNO > 0 AND A.UA_TYPE=3", "EM.FNAME");
        objCommon.FillDropDownList(ddlCoInvestigator, "PAYROLL_EMPMAS EM,USER_ACC A", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = A.UA_IDNO and EM.IDNO > 0 AND A.UA_TYPE=3", "EM.FNAME,EM.MNAME,EM.LNAME");
    }
    private void BindListViewStaffFunded()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllStaffFundedEmployee(_idnoEmp);
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
            objSevBook.Name_org = txtProjname.Text;
            objSevBook.Name_agency = txtFunName.Text;
            objSevBook.ADDRESS = txtadd.Text;
            //objSevBook.AGECATNO = Convert.ToInt32(ddlagency.SelectedValue);
            objSevBook.ROLE = Convert.ToInt32(ddlrole.SelectedValue);
            objSevBook.ProjectNature = txtnature.Text;
            objSevBook.SchemeName = Txtsname.Text;
            objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
            objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);
            objSevBook.DURATION = txtDuration.Text;

            if (txtfund.Text != string.Empty)
            {
                objSevBook.AMOUNT = Convert.ToDecimal(txtfund.Text);
            }
            else
            {
                objSevBook.AMOUNT = 0;
            }
            //if (txtproject.Text != string.Empty)
            //{
            //    objSevBook.ProjectStatus = txtproject.Text;
            //}
            //else
            //{
            //    objSevBook.ProjectStatus = string.Empty;
            //}

            if (ddlProject.SelectedIndex > 0)
            {
                objSevBook.ProjectLevel = Convert.ToInt32(ddlProject.SelectedValue);
            }
            else
            {
                objSevBook.ProjectLevel = 0;
            }

            if (ddlagency.SelectedIndex > 0)
            {
                objSevBook.AGECATNO = Convert.ToInt32(ddlagency.SelectedValue);
            }
            else
            {
                objSevBook.AGECATNO = 0;
            }
            if (ddlInvestigator.SelectedIndex > 0)
            {
                objSevBook.INVESTIGATOR = Convert.ToInt32(ddlInvestigator.SelectedValue);
            }
            else
            {
                objSevBook.INVESTIGATOR = 0;
            }
            if (ddlCoInvestigator.SelectedIndex > 0)
            {
                objSevBook.COINVESTIGATOR = Convert.ToInt32(ddlCoInvestigator.SelectedValue);
            }
            else
            {
                objSevBook.COINVESTIGATOR = 0;
            }
            objSevBook.WEBLINK = txtweblink.Text;
            if (ddlProjectStatus.SelectedIndex > 0)
            {
                objSevBook.PROJECT_STATUS_ID = Convert.ToInt32(ddlProjectStatus.SelectedValue);
                objSevBook.ProjectStatus = (ddlProjectStatus.SelectedItem.Text).ToString();
            }
            else
            {
                objSevBook.PROJECT_STATUS_ID = 0;
                objSevBook.ProjectStatus = string.Empty;
            }
            //Changes done for Blob
            if (lblBlobConnectiontring.Text == "")
            {
                objSevBook.ISBLOB = 0;
            }
            else
            {
                objSevBook.ISBLOB = 1;
            }

            if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //bool result = CheckPurpose();

                    //if (result == true)
                    //{
                    //    //objCommon.DisplayMessage("Record Already Exist", this);
                    //    MessageBox("Record Already Exist");
                    //    return;
                    //}
                    //else
                    //{
                    CustomStatus cs = (CustomStatus)objServiceBook.AddStafffunded(objSevBook, dt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string SFNO = objCommon.LookUp("PAYROLL_SB_StaffFunded", "MAX(SFNO)", "");
                                AddDocuments(Convert.ToInt32(SFNO));
                            }
                        }
                        this.Clear();
                        this.BindListViewStaffFunded();
                        DeletePath();
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exits ");
                        this.Clear();
                    }
                    //}
                }
                else
                {
                    if (ViewState["SFNO"] != null)
                    {
                        objSevBook.SFNO = Convert.ToInt32(ViewState["SFNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateStafffunded(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string SCNO = ViewState["SFNO"].ToString();
                                    AddDocuments(Convert.ToInt32(SCNO));
                                }
                            }
                            ViewState["action"] = "add";
                            ViewState["SFNO"] = null;
                            this.Clear();
                            this.BindListViewStaffFunded();
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
            int SFNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(SFNO);
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

    private void ShowDetails(int SFNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleStaffFundOfEmployee(SFNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SFNO"] = SFNO.ToString();
                txtProjname.Text = ds.Tables[0].Rows[0]["Project_Title"].ToString();
                txtFunName.Text = ds.Tables[0].Rows[0]["Funding_Name"].ToString();
                txtadd.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                ddlagency.SelectedValue = ds.Tables[0].Rows[0]["Agency_Category"].ToString();
                ddlrole.SelectedValue = ds.Tables[0].Rows[0]["Role"].ToString();
                txtfund.Text = ds.Tables[0].Rows[0]["Fund_Received"].ToString();
                //txtproject.Text = ds.Tables[0].Rows[0]["Project_Status"].ToString();
                ddlProjectStatus.SelectedValue = ds.Tables[0].Rows[0]["PROJECT_STATUS_ID"].ToString();
                txtnature.Text = ds.Tables[0].Rows[0]["ProjectNature"].ToString();
                Txtsname.Text = ds.Tables[0].Rows[0]["SchemeName"].ToString();
                ddlProject.SelectedValue = ds.Tables[0].Rows[0]["ProjectLevel"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FDT"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["TDT"].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();
                ddlInvestigator.SelectedValue = ds.Tables[0].Rows[0]["INVESTIGATOR"].ToString();
                ddlCoInvestigator.SelectedValue = ds.Tables[0].Rows[0]["CO_INVESTIGATOR"].ToString();
                txtweblink.Text = ds.Tables[0].Rows[0]["WEB_LINK"].ToString();
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
                    dr["FILEPATH"] = Docpath + "STAFF_FUNDED" + ViewState["idno"] + "\\APP_" + SFNO;
                    dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                    dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                    dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                    dr["FOLDER"] = "STAFF_FUNDED";
                    dr["APPID"] = SFNO.ToString();
                    dr["FILENAME"] = ds.Tables[1].Rows[i]["FILENAME"].ToString();
                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["FILE1"] = dtM;
                    ViewState["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                }
                //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
                //LVFiles.DataBind();
                this.BindListView_Attachments(dtM);
                pnlfiles.Visible = true;
            }
            else
            {
                pnlfiles.Visible = false;
                LVFiles.DataSource = null;
                LVFiles.DataBind();
            }
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot edit.");
                return;
            }
            else
            {
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
            ImageButton btnDel = sender as ImageButton;
            int SFNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_StaffFunded", "*", "", "SFNO=" + SFNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved you cannot delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteStaffFundedInfo(SFNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    BindListViewStaffFunded();
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
        DeleteDirecPath(Docpath + "TEMP_FUNDFILES\\" + _idnoEmp + "\\APP_0");
    }

    private void Clear()
    {
        //txtProjname.Text = txtFunName.Text = txtadd.Text = txtfund.Text = txtproject.Text = string.Empty;
        txtProjname.Text = txtFunName.Text = txtadd.Text = txtfund.Text = string.Empty;
        ddlagency.SelectedIndex = 0;
        ddlrole.SelectedIndex = 0;
        ViewState["action"] = "add";

        LVFiles.DataSource = null;
        LVFiles.DataBind();
        pnlfiles.Visible = false;
        ViewState["FILE1"] = null;
        txtnature.Text = string.Empty;
        Txtsname.Text = string.Empty;
        ddlProject.SelectedIndex = 0;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtDuration.Text = string.Empty;
        ddlInvestigator.SelectedIndex = 0;
        ddlCoInvestigator.SelectedIndex = 0;
        txtweblink.Text = string.Empty;
        ddlProjectStatus.SelectedIndex = 0;

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

                    string file = Docpath + "TEMP_FUNDFILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "STAFF_FUNDED\\" + idno;
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
                            filename = IdNo + "_staffunded_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (FileUpload1.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_staffunded_" + time, FileUpload1);
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

                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                string filename = FileUpload1.FileName;
                                int stcno = Addfieldstotbl(filename);
                                path = file + "\\STF_" + stcno + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                                FileUpload1.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.doc]", this.Page);
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
            dr["FILEPATH"] = Docpath + "STAFF_FUNDED" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "STF_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_FUNDFILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = ViewState["FILE1"];
            //LVFiles.DataBind();
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            this.BindListView_Attachments(dt);
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "STAFF_FUNDED" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "STF_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_FUNDFILES";
            dr["APPID"] = 0;
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
            //LVFiles.DataBind();
            pnlfiles.Visible = true;
            this.BindListView_Attachments(dt);
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
        //string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        string[] Ext = { ".jpg", ".JPG", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".DOC", ".doc", ".JPEG", ".jpeg", ".DOCX" };
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
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/STF_" + SCNO + "." + extension[1].ToString().Trim());
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
                path = Docpath + "STAFF_FUNDED" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["SFNO"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_FUNDFILES" + "\\" + idno + "\\APP_" + appid;
            }

            if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
            {
                DataTable dt = (DataTable)ViewState["FILE1"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, fname));
                ViewState["FILE1"] = dt;
                LVFiles.DataSource = dt;
                LVFiles.DataBind();
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


    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_SB_StaffFunded", "*", "Funding_Name='" + txtFunName.Text + "'", "Project_Title='" + txtProjname.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    #endregion

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
            LVFiles.DataSource = dt;
            LVFiles.DataBind();


            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = LVFiles.FindControl("divBlobDownload");
                Control ctrHead1 = LVFiles.FindControl("divattachblob");
                Control ctrhead2 = LVFiles.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in LVFiles.Items)
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

                Control ctrHeader = LVFiles.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in LVFiles.Items)
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
}