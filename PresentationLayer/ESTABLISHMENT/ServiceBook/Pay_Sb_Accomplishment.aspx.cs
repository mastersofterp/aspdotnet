//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_FamilyParticulars.ascx                                                
// CREATION DATE : 21-OCT-2019
// CREATED BY    : SHRIKANT BHARNE                                                      
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Accomplishment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;
    BlobController objBlob = new BlobController();

    public string path = string.Empty;
    public string Docpath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/");
    public static string RETPATH = "";

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
            DeleteDirecPath(Docpath + "TEMP_ACMFILES\\" + _idnoEmp + "\\APP_0");
        }

        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue); 
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindListViewAchiement();
        BlobDetails();
        GetConfigForEditAndApprove();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
    }

    private void BindListViewAchiement()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllAccomplishmentEmployee(_idnoEmp);
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
            DataTable dt = null;
            if (ViewState["FILE1"] != null)
            {
                dt = (DataTable)ViewState["FILE1"];
            }

            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.AwardName = txtAwarname.Text;
            objSevBook.OrganizationAdd = txtOrganization.Text;
            objSevBook.DOACH = Convert.ToDateTime(txtDateOftalk.Text);
            objSevBook.AMOUNT_REC = txtAmount.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToDecimal(txtAmount.Text.Trim());

            if (txtDescription.Text != string.Empty)
            {
                objSevBook.Description = txtDescription.Text;
            }
            else
            {
                objSevBook.Description = string.Empty;
            }

            if (ddlAwardlevel.SelectedIndex > 0)
            {
                objSevBook.Awardlevel = Convert.ToInt32(ddlAwardlevel.SelectedValue);
            }
            else
            {
                objSevBook.Awardlevel = 0;
            }

            if (txtIssue.Text != string.Empty)
            {
                objSevBook.ISSUINGORGANIZATION = txtIssue.Text;
            }
            else
            {
                objSevBook.ISSUINGORGANIZATION = string.Empty;
            }

            //if (flupld.HasFile)
            //{
            //   // objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
            //    if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
            //    {
            //        objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
            //    }
            //    else
            //    {
            //        MessageBox("Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]");
            //        flupld.Focus();
            //    }
            //}
            //else
            //{
            //    if (ViewState["attachment"] != null)
            //    {
            //        objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();

            //    }
            //    else
            //    {
            //        objSevBook.ATTACHMENTS = string.Empty;
            //    }

            //}

            if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update
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
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddAccomplishment(objSevBook, dt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string SFNO = objCommon.LookUp("PAYROLL_SB_Accomplishment", "MAX(ACNO)", "");
                                AddDocuments(Convert.ToInt32(SFNO));
                            }
                        }
                        //objServiceBook.upload_new_files("Accomplishment_INFO", _idnoEmp, "ACNO", "PAYROLL_SB_Accomplishment", "ACI_", flupld);
                        //lblFamilymsg.Text = "Record Saved Successfully";
                        this.Clear();
                        this.BindListViewAchiement();
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exist ");
                        this.Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["ACNO"] != null)
                    {
                        #region
                        //bool result = CheckPurpose();
                        //
                        //if (result == true)
                        //{
                        //    //objCommon.DisplayMessage("Record Already Exist", this);
                        //    MessageBox("Record Already Exist");
                        //    return;
                        //}
                        //else
                        //{
                        #endregion
                        objSevBook.ACNO = Convert.ToInt32(ViewState["ACNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateAccomplishmentInfo(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string SCNO = ViewState["ACNO"].ToString();
                                    AddDocuments(Convert.ToInt32(SCNO));
                                }
                            }

                            //objServiceBook.update_upload("Accomplishment_INFO", objSevBook.ACNO, ViewState["attachment"].ToString(), _idnoEmp, "ACI_", flupld);
                            //objServiceBook.update_upload("Accomplishment_INFO", objSevBook.ACNO, ViewState["attachment"].ToString(), _idnoEmp, "ACI_", flupld);
                            ViewState["action"] = "add";
                            ViewState["ACNO"] = null;
                            //lblFamilymsg.Text = "Record Updated Successfully";
                            this.Clear();
                            this.BindListViewAchiement();
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            MessageBox("Record Already Exist");
                            this.Clear();
                        }

                    }
                }
                // }
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
            int ACNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(ACNO);
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

    private void ShowDetails(int ACNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleAccomplishmentOfEmployee(ACNO);
            //To show employee family details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["ACNO"] = ACNO.ToString();
                txtAwarname.Text = ds.Tables[0].Rows[0]["AwardName"].ToString();
                txtOrganization.Text = ds.Tables[0].Rows[0]["ORG_ADDRESS"].ToString();
                txtDateOftalk.Text = ds.Tables[0].Rows[0]["DOA"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT_REC"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                //ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                ddlAwardlevel.SelectedValue = ds.Tables[0].Rows[0]["Award_Level"].ToString();
                txtIssue.Text = ds.Tables[0].Rows[0]["ISSUINGORGANIZATION"].ToString();

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
                    dr["FILEPATH"] = Docpath + "ACCM" + ViewState["idno"] + "\\APP_" + ACNO;
                    dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                    dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                    dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                    dr["FOLDER"] = "ACCM";
                    dr["APPID"] = ACNO.ToString();
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
            int ACNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_Accomplishment", "LTRIM(RTRIM(isnull(APPROVE_STATUS,''))) as APPROVE_STATUS", "", "ACNO=" + ACNO, "");
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
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteAccomplishmentInfo(ACNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    BindListViewAchiement();
                    //lblFamilymsg.Text = "Record Deleted Successfully";

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
        DeleteDirecPath(Docpath + "TEMP_ACMFILES\\" + _idnoEmp + "\\APP_0");
        GetConfigForEditAndApprove();
    }

    private void Clear()
    {
        txtAwarname.Text = txtDescription.Text = txtDateOftalk.Text = txtAmount.Text = txtDescription.Text = string.Empty;
        txtOrganization.Text = string.Empty;
        ViewState["action"] = "add";

        //LVFiles.DataSource = null;
        //LVFiles.DataBind();
        //pnlfiles.Visible = false;
        pnlAttachmentList.Visible = false;
        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        ViewState["FILE1"] = null;
        ddlAwardlevel.SelectedIndex = 0;
        txtIssue.Text = string.Empty;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    public string GetFileNamePath(object filename, object ACNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/Accomplishment_INFO/" + idno.ToString() + "/ACI_" + ACNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }


    //public bool CheckPurpose()
    //{
    //    bool result = false;
    //    DataSet dsPURPOSE = new DataSet();

    //    dsPURPOSE = objCommon.FillDropDown("PAYROLL_SB_Accomplishment", "*", "", " DOA='" + Convert.ToDateTime(txtDateOftalk.Text) + "' AND IDNO=" + _idnoEmp + " AND AWARDNAME='" + txtAwarname.Text + "' AND ORG_ADDRESS='" + txtOrganization.Text + "' AND ACNO<>" + ViewState["ACNO"].ToString(), "");
    //    if (dsPURPOSE.Tables[0].Rows.Count > 0)
    //    {
    //        result = true;

    //    }
    //    return result;
    //}

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    //private bool FileTypeValid(string FileExtention)
    //{
    //    bool retVal = false;
    //    string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".docx", ".PNG", ".pdf", ".PDF", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
    //    foreach (string ValidExt in Ext)
    //    {
    //        if (FileExtention == ValidExt)
    //        {
    //            retVal = true;
    //        }
    //    }
    //    return retVal;
    //}

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
                        if (FileUpload1.FileContent.Length >= 1024 * 5000)
                        {

                            MessageBox("File Size Should Not Be Greater Than 5 Mb");
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

                    string file = Docpath + "TEMP_ACMFILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "ACCM\\" + idno;
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
                            filename = IdNo + "_accomplishment_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (FileUpload1.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_accomplishment_" + time, FileUpload1);
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
                                path = file + "\\ACM_" + stcno + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                                FileUpload1.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf]", this.Page);
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
            dr["FILEPATH"] = Docpath + "ACCM" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "ACM_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_ACMFILES";
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
            dr["FILEPATH"] = Docpath + "ACCM" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "ACM_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_ACMFILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
            //LVFiles.DataBind();
            //pnlfiles.Visible = true;
            pnlAttachmentList.Visible = true;
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
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF" };
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
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/ACM_" + SCNO + "." + extension[1].ToString().Trim());
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
                path = Docpath + "ACCM" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["ACNO"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_ACMFILES" + "\\" + idno + "\\APP_" + appid;
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
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_SB_Accomplishment", "*", "Award_Name='" + txtAwarname.Text + "'", "Organization_Address='" + txtOrganization.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
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
            string Command = "Accomplishment";
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