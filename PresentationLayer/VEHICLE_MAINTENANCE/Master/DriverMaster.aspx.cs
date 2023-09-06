//==============================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 06-OCT-2015
// DESCRIPTION   : TO ADD VARIOUS TYPE OF DRIVERS AND CONDUCTORS.
//===============================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;


public partial class VEHICLE_MAINTENANCE_Master_DriverMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    public string path = string.Empty;
    public string dbPath = string.Empty;

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
                    ViewState["action"] = "add";
                   // ViewState["FILE"] = null;
                   // BindlistView();
                    BindExpiryList();
                }
                

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    private void BindExpiryList()
    {
        try
        {
           DataSet ds = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "DNAME,D_DRIVING_LICENCE_NO", "D_DRIVING_LICENCE_EXPIRY_DATE,DNO", "convert(date,D_DRIVING_LICENCE_EXPIRY_DATE) between GETDATE() and DATEADD(day,15,GETDATE())", "");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvDriverLicExp.Visible = true;
                        Panel2.Visible = true;
                        lvDriverLicExp.DataSource = ds;
                        lvDriverLicExp.DataBind();
                    }
                }
                else
                {
                    lvDriverLicExp.Visible = false;
                    Panel2.Visible = false;
                }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindlistView(int DRIVER_CON_TYPE)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "DNO,DNAME,PHONE,DADD1, (CASE D_DRIVING_LICENCE_TYPE WHEN '' THEN '-' ELSE D_DRIVING_LICENCE_TYPE END) AS D_DRIVING_LICENCE_TYPE , (CASE D_DRIVING_LICENCE_NO WHEN '' THEN '-' ELSE D_DRIVING_LICENCE_NO END) AS D_DRIVING_LICENCE_NO", "(CASE WHEN DCATEGORY=1 THEN 'REGULAR' WHEN DCATEGORY=2 THEN 'CONTRACT' WHEN DCATEGORY=3 THEN 'MR' WHEN DCATEGORY=4 THEN 'HIRED' END) AS DCATEGORY, (CASE WHEN DRIVER_CON_TYPE=1 THEN 'DRIVER' WHEN DRIVER_CON_TYPE=2 THEN 'CONDUCTOR' END) AS DRIVER_CON_TYPE  ", "DRIVER_CON_TYPE=" + DRIVER_CON_TYPE, "DNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDriver.DataSource = ds;
                lvDriver.DataBind();
            }
            else
            {
                lvDriver.DataSource = null;
                lvDriver.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtExpiryDate.Text.Equals(string.Empty))
            {               
               if (Convert.ToDateTime(txtExpiryDate.Text) <  Convert.ToDateTime(System.DateTime.Now))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Expiry Date Should be Greater than Todays Date.');", true);                  
                    txtExpiryDate.Focus();
                    return;
                }

               
            }

            if (!txtExpiryDate.Text.Equals(string.Empty))
            {
                if (Convert.ToDateTime(txtExpiryDate.Text) < Convert.ToDateTime(txtFromDate.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Expiry Date Should be Greater than From Date.');", true);
                    txtExpiryDate.Focus();
                    return;
                }
            }

            objVM.DNAME = txtDrvrName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrvrName.Text.Trim());
            objVM.DPHONE = txtDrvrCntNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrvrCntNo.Text);
            objVM.DADD1 = txtDrvrAdd1.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrvrAdd1.Text.Trim());
            objVM.DADD2 = txtDrvrAdd2.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrvrAdd2.Text.Trim());
            objVM.D_DRIVING_LICENCE_TYPE = txtDrivingLicenceType.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrivingLicenceType.Text.Trim());
            objVM.D_DRIVING_LICENCE_NO = txtDrivingLicenceNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDrivingLicenceNo.Text.Trim());
            if (ddlDriConType.SelectedIndex == 2)
            {
                objVM.D_DRIVING_LICENCE_FROM_DATE = DateTime.MinValue;
                objVM.D_DRIVING_LICENCE_EXPIRY_DATE = DateTime.MinValue;
            }
            else
            {
                objVM.D_DRIVING_LICENCE_FROM_DATE = Convert.ToDateTime(txtFromDate.Text.Trim());
                objVM.D_DRIVING_LICENCE_EXPIRY_DATE = Convert.ToDateTime(txtExpiryDate.Text.Trim());
            }    
           
            objVM.D_CATEGORY = Convert.ToInt32(ddlCategory.SelectedValue);
            objVM.DRI_CON_TYPE = Convert.ToInt32(ddlDriConType.SelectedValue);


           
            


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataSet ds1 = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "D_DRIVING_LICENCE_NO", "DNAME", "D_DRIVING_LICENCE_NO='" + txtDrivingLicenceNo.Text.ToString() + "'and DRIVER_CON_TYPE=1", "");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds1.Tables[0].Rows)
                        {
                            string Dlicence = dr["D_DRIVING_LICENCE_NO"].ToString();
                            if (Dlicence == txtDrivingLicenceNo.Text)
                            {
                                objCommon.DisplayMessage(this.updActivity, "Driving Licence No. Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }

                    //--======start===Shaikh Juned 22-08-2022

                    DataSet ds = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "PHONE", "DNAME", "PHONE='" + txtDrvrCntNo.Text.ToString() + "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string phone = dr["PHONE"].ToString();
                            if (phone == txtDrvrCntNo.Text)
                            {
                                objCommon.DisplayMessage(this.updActivity, "Contact No. Is Already Exist.", this.Page);
                                return;
                            }

                        }
                    }
                    //---========end=====
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateDriverMaster(objVM);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                       
                        Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlistView(Convert.ToInt32(ddlDriConType.SelectedValue));
                        ViewState["action"]="add";
                        Clear();
                        objCommon.DisplayMessage(this.updActivity,"Record Saved Successfully.",this.Page);
                    }
                }
                else
                {
                    if (ViewState["DNO"]!=null)
                    {
                        objVM.DNO=Convert.ToInt32(ViewState["DNO"].ToString());
                        //--======start===Shaikh Juned 12-09-2022

                        DataSet ds1 = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "D_DRIVING_LICENCE_NO", "DNAME", "D_DRIVING_LICENCE_NO='" + txtDrivingLicenceNo.Text.ToString() + "'and DRIVER_CON_TYPE=1 and DNO!='" + Convert.ToInt32(ViewState["DNO"].ToString()) + "'", "");
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                string Dlicence = dr["D_DRIVING_LICENCE_NO"].ToString();
                                if (Dlicence == txtDrivingLicenceNo.Text)
                                {
                                    objCommon.DisplayMessage(this.updActivity, "Driving Licence No. Is Already Exist.", this.Page);
                                    return;
                                }

                            }
                        }

                        DataSet ds = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "*", "", "PHONE='" + txtDrvrCntNo.Text.ToString() + "' and DNO!='" + Convert.ToInt32(ViewState["DNO"].ToString()) + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                int Dno = Convert.ToInt32(dr["DNO"]);
                                if (Dno != Convert.ToInt32(objVM.DNO))
                                {
                                    objCommon.DisplayMessage(this.updActivity, "Contact No Is Already Exist.", this.Page);
                                    return;
                                }

                            }
                        }
                        //---========end=====
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdateDriverMaster(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView(Convert.ToInt32(ddlDriConType.SelectedValue));
                            ViewState["action"] = "add";
                            BindExpiryList();
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                            Clear();
                        }

                    }
                }
            }
        
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as  ImageButton;
            int DNO = int.Parse(btnEdit.CommandArgument);
            ViewState["DNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(DNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int DNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "*", "", "DNO=" + DNO, "DNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDrvrName.Text = ds.Tables[0].Rows[0]["DNAME"].ToString();
                txtDrvrCntNo.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                txtDrvrAdd1.Text = ds.Tables[0].Rows[0]["DADD1"].ToString();
                txtDrvrAdd2.Text = ds.Tables[0].Rows[0]["DADD2"].ToString();
                txtDrivingLicenceType.Text = ds.Tables[0].Rows[0]["D_DRIVING_LICENCE_TYPE"].ToString();
                txtDrivingLicenceNo.Text = ds.Tables[0].Rows[0]["D_DRIVING_LICENCE_NO"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["D_DRIVING_LICENCE_FROM_DATE"].ToString();
                txtExpiryDate.Text = ds.Tables[0].Rows[0]["D_DRIVING_LICENCE_EXPIRY_DATE"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["DCATEGORY"].ToString();
                ddlDriConType.SelectedValue = ds.Tables[0].Rows[0]["DRIVER_CON_TYPE"].ToString();
                
                path = Docpath + "VEHICLE\\DrivingLicenceFiles\\DrivingLicenceNo_" + txtDrivingLicenceNo.Text.Replace("/", "-");
                BindListViewFiles(path);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtDrvrAdd1.Text = string.Empty;
        txtDrvrAdd2.Text = string.Empty;
        txtDrvrCntNo.Text = string.Empty;
        txtDrvrName.Text = string.Empty;
        txtDrivingLicenceType.Text = string.Empty;
        txtDrivingLicenceNo.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        ddlCategory.SelectedIndex = 0;
        ddlDriConType.SelectedIndex = 0;
        ViewState["DNO"] = null;
        ViewState["action"] = "add";
        lvfile.DataSource = null;
        lvfile.DataBind();
    }

    protected void btnExpiry_Click(object sender, EventArgs e)
    {
       ShowReport("pdf", "DriverLicenceExpiry.rpt");           
    }
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));

            url += "Reports/CommonReport.aspx?";
           url += "exporttype=" + exporttype;
            url += "&filename=DrivingLicenceExpiry" + ".pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            //url += "&param=@P_EXPIRY_FOR=DRIVING_LICENCE,@P_EXPIRY_DURATION=" + hdnexpiryinput.Value + ",@P_VEHICLE_TYPE=1";
            url += "&param=@P_EXPIRY_FOR=DRIVING_LICENCE,@P_VEHICLE_TYPE=1";
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDriConType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            if (ddlDriConType.SelectedIndex > 0)
            {
                if (ddlDriConType.SelectedIndex == 2)
                {
                    trLicType.Visible = false;
                    trLicNo.Visible = false;
                    trFroDt.Visible = false;
                    trExpDt.Visible = false;
                    Panel2.Visible = false;
                    file.Visible = false;
                }
                else
                {
                    trLicType.Visible = true;
                    trLicNo.Visible = true;
                    trFroDt.Visible = true;
                    trExpDt.Visible = true;
                    Panel2.Visible = true;
                    file.Visible = true;
                }
                BindlistView(Convert.ToInt32(ddlDriConType.SelectedValue));
            }
        }
        catch(Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_DriverMaster.ddlDriConType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #region Attachment

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                if (ddlDriConType.SelectedIndex != 0)
                {
                    if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                    {
                        string file = Docpath + "VEHICLE\\DrivingLicenceFiles\\DrivingLicenceNo_" + txtDrivingLicenceNo.Text.Replace("/", "-");

                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }
                        ViewState["FILENAME"] = file;

                        path = file + "\\DrivingLicenceNo_" + txtDrivingLicenceNo.Text.Replace("/", "-") + "_" + FileUpload1.FileName;

                        dbPath = file;
                        string filename = FileUpload1.FileName;
                        ViewState["FILE_NAME"] = filename;

                        //CHECKING FOLDER EXISTS OR NOT file
                        HttpPostedFile chkFileSize = FileUpload1.PostedFile;
                        if (chkFileSize.ContentLength <= 102400) // For Allowing 100 Kb Size Files only 
                        {
                            if (txtDrivingLicenceNo.Text != "")
                            {
                                if (!System.IO.Directory.Exists(path))
                                {
                                    FileUpload1.PostedFile.SaveAs(path);
                                }
                            }
                            BindListViewFiles(file);
                        }
                        else
                        {
                            //objCommon.DisplayMessage(this, "File size should not exceed 100 Kb.", this.Page);
                            objCommon.DisplayMessage(this, "Valid Files Format are [.jpg,.pdf,.xls,.zip]. Upload File Size Upto 100Kb.", this.Page);
                        }
                    }

                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel3, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
                        FileUpload1.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Plese Select Driver / Conductor Type.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel3, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //ChecK for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".zip", ".ZIP", ".txt", ".TXT", ".docx", ".DOCX", ".XLSX", ".xlsx", ".rar", ".RAR" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;
        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "DrivingLicenceNo")
            f_name = Convert.ToString(fname[2]);

        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);

        return f_name;
    }

    protected string GetFileNameCaseNo(object obj)
    {
        string f_name = string.Empty;

        string[] fname = obj.ToString().Split('_');

        if (fname[0] == "DrivingLicenceNo")
            f_name = Convert.ToString(fname[1]);
        f_name = f_name.Replace('-', '/');
        if (fname[0] == "judDoc")
            f_name = Convert.ToString(fname[3]);
        return f_name;
    }

    protected string GetFileDate(object obj)
    {
        string file_path = Convert.ToString(ViewState["FILE_PATH"] + "\\" + obj.ToString());
        FileInfo fileInfo = new FileInfo(file_path);

        DateTime creationTime = DateTime.MinValue;
        creationTime = fileInfo.CreationTime;

        string f_date = string.Empty;
        //f_date = creationTime.ToString("dd-MMM-yyyy");
        f_date = DateTime.Today.ToString("dd-MMM-yyyy");
        return f_date;
    }

    public void savefile(string fpath, string fname)
    {
        try
        {
            if (ViewState["action"].ToString() == "ce")
                fname = "ComplaintNo_" + Convert.ToInt32(ViewState["idno"].ToString()) + "_" + fname;
            if (ViewState["action"].ToString() == "pr")
                fname = "judDoc_ComplaintNo_" + Convert.ToInt32(ViewState["idno"].ToString()) + "_" + fname;

            //SAVING FILE IN TO SERVER PATH 
            FileUpload1.PostedFile.SaveAs(fpath + "\\" + fname);
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Uploaded Successfully.');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.savefile-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewFiles(string PATH)
    {
        try
        {
            pnlFile.Visible = true;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {
                System.IO.FileInfo[] files = dir.GetFiles();

                if (Convert.ToBoolean(files.Length))
                {
                    lvfile.DataSource = files;
                    lvfile.DataBind();
                    ViewState["FILE"] = files;
                }
                else
                {
                    lvfile.DataSource = null;
                    lvfile.DataBind();
                }
            }
            else
            {
                lvfile.DataSource = null;
                lvfile.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAttachDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string case_entry_no = btnDelete.CommandArgument;
            string[] fname1 = case_entry_no.ToString().Split('_');

            path = Docpath + "VEHICLE\\DrivingLicenceFiles\\DrivingLicenceNo_" + fname1[1];

            string fname = btnDelete.CommandArgument;


            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted  Successfully!!');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        BindListViewFiles();

    }

    private void BindListViewFiles()
    {
        try
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = dir.GetFiles();
            lvfile.DataSource = files;
            lvfile.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Estt_complaint.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void imgFileDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }

    public void DownloadFile(string filePath)
    {
        try
        {
            string[] fname1 = filePath.ToString().Split('_');
            string filename = filePath.Substring(filePath.LastIndexOf("_") + 1);
            path = Docpath + "VEHICLE\\DrivingLicenceFiles\\DrivingLicenceNo_" + fname1[1];           
            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            //Response.AddHeader("content-disposition", "attachment; filename=" + filePath);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
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

            case ".jpg":
                return "application/{0}";
                break;
            case ".jpeg":
                return "application/{0}";
                break;
            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    #endregion
   
}
