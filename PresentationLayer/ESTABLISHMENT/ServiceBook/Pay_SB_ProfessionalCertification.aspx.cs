//--------------------------------------
//Created by : Sonal Banode
//Date : 17-02-2022
//--------------------------------------
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
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class ESTABLISHMENT_ServiceBook_Pay_SB_ProfessionalCertification : System.Web.UI.Page
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
                // CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
            DeleteDirecPath(Docpath + "TEMP_PROFESSIONALCOURSE_FILES\\" + _idnoEmp + "\\APP_0");
        }
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }

        if (_idnoEmp != 0)
        {
            BindListViewProfessional();
            btnSubmit.Enabled = true;
        }
        else
        {
            //MessageBox("Invalid Login! Please Login Again");
            btnSubmit.Enabled = false;
            //return;
        }
        BlobDetails();
    }

     

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_SB_ProfessionalCertification.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SB_ProfessionalCertification.aspx");
        }
    }

    private void BindListViewProfessional()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllProfessionalCourseDetail(_idnoEmp);
            lvProfessional.DataSource = ds;
            lvProfessional.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.BindListViewProfessional-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.COURSE = txtCourse.Text;
            objSevBook.INST = txtInstitute.Text;
            objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
            objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);

            if (txtSponsoredAmt.Text != string.Empty)
            {
                objSevBook.SPONSORED_AMOUNT = Convert.ToDecimal(txtSponsoredAmt.Text);
            }
            else
            {
                objSevBook.SPONSORED_AMOUNT = 0;
            }
            objSevBook.LOCATION = txtVenue.Text;

            objSevBook.SPONSOREDBY = txtSponsoredBy.Text;

            objSevBook.REMARK = txtRemarks.Text;

            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();

            if (txtPresentation.Text != string.Empty)
            {
                objSevBook.PRESENTATION_DETAILS = txtPresentation.Text;
            }
            else
            {
                objSevBook.PRESENTATION_DETAILS = string.Empty;
            }
            objSevBook.DURATION = txtDuration.Text;

            objSevBook.ROLENAME = txtRole.Text;

            if (txtCostInvolved.Text != string.Empty)
            {
                objSevBook.COST_INVOLVED = Convert.ToDecimal(txtCostInvolved.Text);
            }
            else
            {
                objSevBook.COST_INVOLVED = 0;
            }
            
            if (ddlProgramLevel.SelectedIndex > 0)
            {
                objSevBook.PROGRAM_LEVEL = Convert.ToInt32(ddlProgramLevel.SelectedValue);
            }
            else
            {
                objSevBook.PROGRAM_LEVEL = 0;
            }

            if (ddlProgramType.SelectedIndex > 0)
            {
                objSevBook.PROGRAM_TYPE = Convert.ToInt32(ddlProgramType.SelectedValue);
            }
            else
            {
                objSevBook.PROGRAM_TYPE = 0;
            }
            objSevBook.InternalFaculty = txtInfac.Text.Trim();
            objSevBook.ExternalFaculty = txtExtFac.Text.Trim();
            objSevBook.ExternalStudent = txtInStud.Text.Trim();
            objSevBook.InternalStudent = txtExStud.Text.Trim();
            objSevBook.PROFESSIONALBODY = txtProf.Text.Trim();

            if (rdbMode.SelectedValue == "0")
            {
                objSevBook.MODE = "Online";
            }
            else
            {
                objSevBook.MODE = "Offline";
            }

            if (FileUpload1.HasFile)
            {
                objSevBook.ATTACHMENTS = Convert.ToString(FileUpload1.PostedFile.FileName.ToString());
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

            if (txtAcadYear.Text != string.Empty)
            {
                objSevBook.YEAR = Convert.ToInt32(txtAcadYear.Text);
            }
            else
            {
                objSevBook.YEAR = null;
            }
            if (ddlPartitionType.SelectedIndex > 0)
            {
                objSevBook.PARTITION_TYPE = Convert.ToInt32(ddlPartitionType.SelectedValue);
            }
            else
            {
                objSevBook.PARTITION_TYPE = 0;
            }
            if (txtThemeOfTrainingAttended.Text != string.Empty)
            {
                objSevBook.ThemeOfTrainingAttended = txtThemeOfTrainingAttended.Text;
            }
            else
            {
                objSevBook.ThemeOfTrainingAttended = string.Empty;
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
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddProfessionalCourse(objSevBook, dt);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (objSevBook.ISBLOB == 0)
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string PNO = objCommon.LookUp("PAYROLL_SB_PROFESSIONAL_COURSE", "MAX(PNO)", "");
                                AddDocuments(Convert.ToInt32(PNO));
                            }
                        }
                        this.Clear();
                        this.BindListViewProfessional();
                        DeletePath();
                       
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        MessageBox("Record Already Exist");
                        this.Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["pNo"] != null)
                    {
                        objSevBook.PNO = Convert.ToInt32(ViewState["pNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateProfessionalCourse(objSevBook, dt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (objSevBook.ISBLOB == 0)
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string PNO = objCommon.LookUp("PAYROLL_SB_PROFESSIONAL_COURSE", "MAX(PNO)", "");
                                    AddDocuments(Convert.ToInt32(PNO));
                                }
                            }
                           
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewProfessional();
                            DeletePath();
                            
                            MessageBox("Record Updated Successfully");
                        }

                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            MessageBox("Record Already Exist");
                            this.Clear();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
       
    }

    

    private void Clear()
    {
        txtRemarks.Text = string.Empty;
        txtInstitute.Text = string.Empty;
        txtCourse.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtSponsoredAmt.Text = string.Empty;
        txtSponsoredBy.Text = string.Empty;
        txtVenue.Text = string.Empty;
       
        
        txtCostInvolved.Text = string.Empty;
        txtRole.Text = string.Empty;
        txtPresentation.Text = string.Empty;
        txtDuration.Text = string.Empty;
      
        ddlProgramLevel.SelectedIndex = 0;
        ddlProgramType.SelectedIndex = 0;
       

        LVFiles.DataSource = null;
        LVFiles.DataBind();
        pnlfiles.Visible = false;
        ViewState["FILE1"] = null;
        ViewState["action"] = "add";

        txtInfac.Text = txtExtFac.Text = txtInStud.Text = txtExStud.Text = string.Empty;
        txtProf.Text = string.Empty;
        rdbMode.SelectedIndex = 0;
        ddlPartitionType.SelectedIndex = 0;
        txtAcadYear.Text = string.Empty;
        txtThemeOfTrainingAttended.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        DeleteDirecPath(Docpath + "TEMP_PROFESSIONALCOURSE_FILES\\" + _idnoEmp + "\\APP_0");
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["FILE1"] = null;
            ImageButton btnEdit = sender as ImageButton;
            int pNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(pNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int pNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleProfessionalCourseDetailsOfEmployee(pNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["pNo"] = pNo.ToString();
                txtCourse.Text = ds.Tables[0].Rows[0]["course"].ToString();
                txtInstitute.Text = ds.Tables[0].Rows[0]["inst"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                txtSponsoredAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["SPONSORED_AMOUNT"].ToString()) > 0 ? ds.Tables[0].Rows[0]["SPONSORED_AMOUNT"].ToString() : "";
                txtVenue.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();
                txtSponsoredBy.Text = ds.Tables[0].Rows[0]["SPONSOREDBY"].ToString();

                ddlProgramLevel.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM_LEVEL"].ToString();
                ddlProgramType.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM_TYPE"].ToString();
                txtPresentation.Text = ds.Tables[0].Rows[0]["PRESENTATION_DETAILS"].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();
                txtCostInvolved.Text = ds.Tables[0].Rows[0]["COST_INVOLVED"].ToString();
                
                txtRole.Text = ds.Tables[0].Rows[0]["ROLE"].ToString();


                txtInfac.Text = ds.Tables[0].Rows[0]["INTERNAL_FACULTY"].ToString();
                txtExtFac.Text = ds.Tables[0].Rows[0]["EXTERNAL_FACULTY"].ToString();
                txtExStud.Text = ds.Tables[0].Rows[0]["EXTERNAL_STUDENT"].ToString();
                txtInStud.Text = ds.Tables[0].Rows[0]["INTERNAL_STUDENT"].ToString();
                txtProf.Text = ds.Tables[0].Rows[0]["PROFESSIONAL_BODY"].ToString();
                //ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                string mode = ds.Tables[0].Rows[0]["MODE"].ToString();
                if (mode == "Online")
                {
                    rdbMode.SelectedValue = "0";
                }
                else
                {
                    rdbMode.SelectedValue = "1";
                }
                txtAcadYear.Text = ds.Tables[0].Rows[0]["Acad_Year"].ToString();
                txtThemeOfTrainingAttended.Text = ds.Tables[0].Rows[0]["ThemeOfTrainingAttended"].ToString();
                ddlPartitionType.SelectedValue = ds.Tables[0].Rows[0]["PARTITION_TYPE"].ToString();
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
                    dr["FILEPATH"] = Docpath + "PROFESSIONAL" + ViewState["idno"] + "\\APP_" + pNo;
                    dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                    dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                    dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                    dr["FOLDER"] = "PROFESSIONAL_COURSE";
                    dr["APPID"] = pNo.ToString();
                    dr["FILENAME"] = ds.Tables[1].Rows[i]["FILENAME"].ToString();
                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["FILE1"] = dtM;
                    ViewState["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                }
                //LVFiles.DataSource = (DataTable)ViewState["FILE1"];
                //LVFiles.DataBind();
                pnlfiles.Visible = true;
                this.BindListView_Attachments(dtM);
            }
            else
            {
                pnlfiles.Visible = false;
                LVFiles.DataSource = null;
                LVFiles.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }
    
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int pNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteProfessionalCourse(pNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                BindListViewProfessional();
                this.Clear();
                ViewState["action"] = "add";
                MessageBox("Record Deleted Successfully");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Training.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

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

                    string file = Docpath + "TEMP_PROFESSIONALCOURSE_FILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "PROFESSIONAL_COURSE\\" + idno;
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
                            filename = IdNo + "_professionalcertificate_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;
                            objSevBook.FILEPATH = "Blob Storage";

                            if (FileUpload1.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, IdNo + "_professionalcertificate_" + time, FileUpload1);
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
                                int pcno = Addfieldstotbl(filename);
                                path = file + "\\PC_" + pcno + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
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
            dr["FILEPATH"] = Docpath + "PROFESSIONAL_COURSE" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "PC_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_PROFESSIONALCOURSE_FILES";
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
            dr["FILEPATH"] = Docpath + "PROFESSIONAL_COURSE" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "PC_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_PROFESSIONALCOURSE_FILES";
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
    public string GetFileNamePath(object filename, object PNO, object idno, object folder, object AppID)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/PC_" + PNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    private void AddDocuments(int PNO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;

            int idno = _idnoEmp;

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\APP_" + PNO;

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

   
    protected void imgbtndelFile_Click(object sender, ImageClickEventArgs e)
    {
         try
        {
            int idno = _idnoEmp;
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            int appid = Convert.ToInt32(btnDelete.AlternateText);
            if (appid != 0)
            {
                path = Docpath + "PROFESSIONAL_COURSE" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["pNo"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_PROFESSIONALCOURSE_FILES" + "\\" + idno + "\\APP_" + appid;
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