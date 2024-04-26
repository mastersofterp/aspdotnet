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
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_RevenueGenerated : System.Web.UI.Page
{
    // Created by Piyush Thakre on 28/02/2024
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBook objSevBook = new ServiceBook();
    ServiceBookController objServiceBook = new ServiceBookController();
    BlobController objBlob = new BlobController();
    public int _idnoEmp;
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
            FillYear();
        }
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BlobDetails();
        BindListViewRevenue();
        btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
        GetConfigForEditAndApprove();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Sb_RevenueGenerated.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Sb_RevenueGenerated.aspx");
        }
    }
    private void FillYear()
    {
        int Yr = DateTime.Now.Year;

        ddlYear.Items.Add(Convert.ToString(Yr - 2));
        ddlYear.Items.Add(Convert.ToString(Yr - 1));
        ddlYear.Items.Add(Convert.ToString(Yr));
        ddlYear.Items.Add(Convert.ToString(Yr + 1));
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ServiceBook objSevBook = new ServiceBook();
            if (flupld.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(flupld.FileName)))
                {
                    if (flupld.HasFile)
                    {
                        if (flupld.FileContent.Length >= 1024 * 10000)
                        {

                            MessageBox("File Size Should Not Be Greater Than 5 Mb");
                            flupld.Dispose();
                            flupld.Focus();
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
                    string FileName = flupld.FileName;
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

                    string file = Docpath + "TEMP_REVENUE_GENERATED_FILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "REVENUE_GENERATED\\" + idno;
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
                        if (flupld.HasFile)
                        {
                            string contentType = contentType = flupld.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(flupld.PostedFile.FileName);
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = IdNo + "_revenuegenerated_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (flupld.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_revenuegenerated_" + time, flupld);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    int tano = Addfieldstotbl(filename);
                                }
                            }
                        }
                    }
                    else
                    {
                        string filename = flupld.FileName;
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                int tano = Addfieldstotbl(filename);
                                path = file + "\\TC_" + tano + System.IO.Path.GetExtension(flupld.PostedFile.FileName);
                                flupld.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]", this.Page);
                    flupld.Focus();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValid)
            {
                return;
            }
            objSevBook.IDNO = _idnoEmp;
            if (ddlYear.SelectedIndex != 0)
            {
                objSevBook.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
            }
            else
            {
                objSevBook.YEAR = 0;
            }
            if (txtRGTVAC.Text != string.Empty)
            {
                objSevBook.RGVAC = Convert.ToDouble(txtRGTVAC.Text);
            }
            else
            {
                objSevBook.RGVAC = 0;
            }
            if (txtRGTEvents.Text != string.Empty)
            {
                objSevBook.RGEVENTS = Convert.ToDouble(txtRGTEvents.Text);
            }
            else
            {
                objSevBook.RGEVENTS = 0;
            }
            if (txtRGTSponsor.Text != string.Empty)
            {
                objSevBook.RGSPONSORSHIP = Convert.ToDouble(txtRGTSponsor.Text);
            }
            else
            {
                objSevBook.RGSPONSORSHIP = 0;
            }
            if (txtWebLink.Text != string.Empty)
            {
                objSevBook.WEBLINK = txtWebLink.Text;
            }
            else
            {
                objSevBook.WEBLINK = string.Empty;
            }
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();

            DataTable dt = null;
            if (ViewState["FILE1"] != null)
            {
                dt = (DataTable)ViewState["FILE1"];
            }
            if (flupld.HasFile)
            {
                objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
            }
            else
            {
                if (ViewState["attachment"] != null)
                {
                    objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
                }
                else
                {
                    objSevBook.ATTACHMENTS = string.Empty;
                }

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
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddRevenueGenerated(objSevBook, dt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string TNO = objCommon.LookUp("PAYROLL_SB_REVENUE_GENERATED", "MAX(RGNO)", "");
                                AddDocuments(Convert.ToInt32(TNO));
                            }
                        }
                        this.Clear();
                        DeletePath();
                        BindListViewRevenue();
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        this.Clear();
                        BindListViewRevenue();
                        MessageBox("Record Already Exist");

                    }
                }
                else
                {
                    //Edit
                    if (ViewState["RGNO"] != null)
                    {
                        objSevBook.RGNO = Convert.ToInt32(ViewState["RGNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateRevenueGenerated(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string TNO = objCommon.LookUp("PAYROLL_SB_REVENUE_GENERATED", "MAX(RGNO)", "");
                                    AddDocuments(Convert.ToInt32(TNO));
                                }
                            }
                            ViewState["action"] = "add";
                            this.Clear();
                            BindListViewRevenue();
                            DeletePath();
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            this.Clear();
                            BindListViewRevenue();
                            MessageBox("Record Already Exist");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        GetConfigForEditAndApprove();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int nfNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(nfNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Sb_RevenueGenerated.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlYear.SelectedValue = "0";
        txtRGTVAC.Text = string.Empty;
        txtRGTEvents.Text = string.Empty;
        txtRGTSponsor.Text = string.Empty;
        txtWebLink.Text = string.Empty;
        ViewState["action"] = "add";

        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        //pnlfiles.Visible = false;
        pnlAttachmentList.Visible = false;
        ViewState["FILE1"] = null;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int RGNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_REVENUE_GENERATED", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "RGNO=" + RGNO, "");
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
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteRevenueGenerated(RGNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindListViewRevenue();
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnDeleteFile_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            int appid = Convert.ToInt32(btnDelete.AlternateText);
            if (appid != 0)
            {
                path = Docpath + "REVENUE_GENERATED" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["RGNO"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_REVENUE_GENERATED_FILES" + "\\" + idno + "\\APP_" + appid;
            }

            if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
            {
                DataTable dt = (DataTable)ViewState["FILE1"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, fname));
                ViewState["FILE1"] = dt;
                BindListView_Attachments(dt);
                //LVFiles.DataSource = dt;
                //LVFiles.DataBind();
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
                DataTable dtM = (DataTable)ViewState["FILE1"];
                pnlAttachmentList.Visible = true;
                this.BindListView_Attachments(dtM);

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
    private int Addfieldstotbl(string filename)
    {
        if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "REVENUE_GENERATED" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "TC_" + FUID + System.IO.Path.GetExtension(flupld.PostedFile.FileName);
            dr["DisplayFileName"] = flupld.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_REVENUE_GENERATED_FILES";
            dr["APPID"] = 0;
            dr["FILENAME"] = filename;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //LVFiles.DataSource = ViewState["FILE1"];
            //LVFiles.DataBind();
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
            dr["FILEPATH"] = Docpath + "REVENUE_GENERATED" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "TC_" + FUID + System.IO.Path.GetExtension(flupld.PostedFile.FileName);
            dr["DisplayFileName"] = flupld.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_REVENUE_GENERATED_FILES";
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
    private void AddDocuments(int TNO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            int idno = _idnoEmp;

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\APP_" + TNO;

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
    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();
            //divAttch.Visible = true;

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

    private void BindListViewRevenue()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllRevenueGeneratedOfEmployee(_idnoEmp);
            lvRevenue.DataSource = ds;
            lvRevenue.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.BindListViewRevenue-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int RGNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleRevenueGeneratedOfEmployee(RGNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                //nfno,idno,ntno,name,relation,per,remark,srno,dob,last,Address,Conting,Age
                ViewState["RGNO"] = RGNO.ToString();
                ddlYear.SelectedValue = ds.Tables[0].Rows[0]["YEAR"].ToString();
                txtRGTVAC.Text = ds.Tables[0].Rows[0]["RGT_VAC"].ToString();
                txtRGTEvents.Text = ds.Tables[0].Rows[0]["RGT_EVENT"].ToString();
                txtRGTSponsor.Text = ds.Tables[0].Rows[0]["RGT_SPONSOR"].ToString();
                txtWebLink.Text = ds.Tables[0].Rows[0]["WEBLINK"].ToString();

                if (Convert.ToInt32(ds.Tables[1].Rows.Count) > 0)
                {
                    int rowCount = ds.Tables[1].Rows.Count;
                    CreateTable();
                    DataTable dtM = (DataTable)ViewState["FILE1"];
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow dr = dtM.NewRow();
                        dr["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                        dr["FILEPATH"] = Docpath + "REVENUE" + ViewState["idno"] + "\\APP_" + RGNO;
                        dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                        dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                        dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                        dr["FOLDER"] = "REVENUE_GENERATED";
                        dr["APPID"] = RGNO.ToString();
                        dr["FILENAME"] = ds.Tables[1].Rows[i]["FILENAME"].ToString();
                        dtM.Rows.Add(dr);
                        dtM.AcceptChanges();
                        ViewState["FILE1"] = dtM;
                        ViewState["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                    }
                    pnlAttachmentList.Visible = true;
                    this.BindListView_Attachments(dtM);
                }
                else
                {
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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    public string GetFileNamePathForMultiple(object filename, object TNO, object idno, object folder, object AppID)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/TC_" + TNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    public string GetFileNamePath(object filename, object ACNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/Accomplishment_INFO/" + idno.ToString() + "/ACI_" + ACNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

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

    #endregion

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Revenue Generated";
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