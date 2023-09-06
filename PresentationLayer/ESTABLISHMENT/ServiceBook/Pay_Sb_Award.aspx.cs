//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_FamilyParticulars.ascx                                                
// CREATION DATE : 11-FEB-2022
// CREATED BY    : SONAL BANODE                                                   
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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Award : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
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
            DeleteDirecPath(Docpath + "TEMP_AWDFILES\\" + _idnoEmp + "\\APP_0");
        }

        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue); 
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindListViewAchiement();
    }

    private void BindListViewAchiement()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllAwardEmployee(_idnoEmp);
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
        {
            try
            {
                DataTable dt = null;
                if (ViewState["FILE1"] != null)
                {
                    dt = (DataTable)ViewState["FILE1"];
                }

              
                ServiceBook objSevBook = new ServiceBook();
                objSevBook.IDNO = _idnoEmp;
                objSevBook.AwardName = txtAward.Text;
                objSevBook.OrganizationAdd = txtOrganizationAdd.Text;
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

                if (ddlAward.SelectedIndex > 0)
                {
                    objSevBook.Awardlevel = Convert.ToInt32(ddlAward.SelectedValue);
                }
                else
                {
                    objSevBook.Awardlevel = 0;
                }

                if (txtIssueOrg.Text != string.Empty)
                {
                    objSevBook.ISSUINGORGANIZATION = txtIssueOrg.Text;
                }
                else
                {
                    objSevBook.ISSUINGORGANIZATION = string.Empty;
                }

                if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //Add New Help
                        CustomStatus cs = (CustomStatus)objServiceBook.AddAward(objSevBook, dt);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            if (ViewState["DESTINATION_PATH"] != null)
                            {
                                string SFNO = objCommon.LookUp("PAYROLL_SB_AWARD", "MAX(AWDNO)", "");
                                AddDocuments(Convert.ToInt32(SFNO));
                            }
                            
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
                        if (ViewState["AWDNO"] != null)
                        {                            
                            objSevBook.AWDNO = Convert.ToInt32(ViewState["AWDNO"].ToString());
                            CustomStatus cs = (CustomStatus)objServiceBook.UpdateAward(objSevBook, dt);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                if (ViewState["DESTINATION_PATH"] != null)
                                {
                                    string SCNO = ViewState["AWDNO"].ToString();
                                    AddDocuments(Convert.ToInt32(SCNO));
                                }

                               
                                ViewState["action"] = "add";
                                ViewState["AWDNO"] = null;
                               
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

    }
  
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["FILE1"] = null;
            // lblFamilymsg.Text = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int AWDNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(AWDNO);
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

    private void ShowDetails(int AWDNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleAwardOfEmployee(AWDNO);
            //To show employee family details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["AWDNO"] = AWDNO.ToString();
                txtAward.Text = ds.Tables[0].Rows[0]["AWARDNAME"].ToString();
                txtOrganizationAdd.Text = ds.Tables[0].Rows[0]["ORG_ADDRESS"].ToString();
                txtDateOftalk.Text = ds.Tables[0].Rows[0]["DOA"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT_REC"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                //ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                ddlAward.SelectedValue = ds.Tables[0].Rows[0]["AWARD_LEVEL"].ToString();
                txtIssueOrg.Text = ds.Tables[0].Rows[0]["ISSUINGORGANIZATION"].ToString();

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
                    dr["FILEPATH"] = Docpath + "AWDN" + ViewState["idno"] + "\\APP_" + AWDNO;
                    dr["GETFILE"] = ds.Tables[1].Rows[i]["GETFILE"].ToString();
                    dr["DisplayFileName"] = ds.Tables[1].Rows[i]["DisplayFileName"].ToString();
                    dr["IDNO"] = ds.Tables[1].Rows[i]["IDNO"].ToString();
                    dr["FOLDER"] = "AWDN";
                    dr["APPID"] = AWDNO.ToString();
                    dtM.Rows.Add(dr);
                    dtM.AcceptChanges();
                    ViewState["FILE1"] = dtM;
                    ViewState["FUID"] = ds.Tables[1].Rows[i]["FUID"].ToString();
                }
                LVFiles.DataSource = (DataTable)ViewState["FILE1"];
                LVFiles.DataBind();
                pnlfiles.Visible = true;
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
            int AWDNO = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteAward(AWDNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
                BindListViewAchiement();
                //lblFamilymsg.Text = "Record Deleted Successfully";

                ViewState["action"] = "add";
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
        DeleteDirecPath(Docpath + "TEMP_AWDFILES\\" + _idnoEmp + "\\APP_0");
    }

    private void Clear()
    {
        txtAward.Text = txtDescription.Text = txtDateOftalk.Text = txtAmount.Text = txtDescription.Text = string.Empty;
        txtOrganizationAdd.Text = string.Empty;
        ViewState["action"] = "add";

        LVFiles.DataSource = null;
        LVFiles.DataBind();
        pnlfiles.Visible = false;
        ViewState["FILE1"] = null;
        ddlAward.SelectedIndex = 0;
        txtIssueOrg.Text = string.Empty;
    }

    public string GetFileNamePath(object filename, object AWDNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/Award/" + idno.ToString() + "/AWD_" + AWDNO + "." + extension[1].ToString().Trim());
        else
            return "";
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

                    string file = Docpath + "TEMP_AWDFILES\\" + idno + "\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "AWDN\\" + idno;
                    ViewState["DESTINATION_PATH"] = PATH;

                    if (!System.IO.Directory.Exists(file))
                    {
                        System.IO.Directory.CreateDirectory(file);
                    }

                    if (!System.IO.Directory.Exists(path))
                    {
                        if (!File.Exists(path))
                        {
                            int stcno = Addfieldstotbl();
                            path = file + "\\AW_" + stcno + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                            FileUpload1.PostedFile.SaveAs(path);
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

    private int Addfieldstotbl()
    {
        if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "AWDN" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "AW_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_AWDFILES";
            dr["APPID"] = 0;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            LVFiles.DataSource = ViewState["FILE1"];
            LVFiles.DataBind();
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["FILE1"];
            DataRow dr = dt.NewRow();
            int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["FILEPATH"] = Docpath + "AWDN" + ViewState["idno"] + "\\APP_";
            dr["GETFILE"] = "AW_" + FUID + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["IDNO"] = _idnoEmp;
            dr["FOLDER"] = "TEMP_AWDFILES";
            dr["APPID"] = 0;
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            LVFiles.DataSource = (DataTable)ViewState["FILE1"];
            LVFiles.DataBind();
            pnlfiles.Visible = true;
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

        ViewState["FILE1"] = dt;
    }
    private bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF"};
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
            return ("~/ESTABLISHMENT/upload_files/" + folder + "/" + idno.ToString() + "/APP_" + AppID + "/AW_" + SCNO + "." + extension[1].ToString().Trim());
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

    protected void imgbtnfiledelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int idno = _idnoEmp;
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            int appid = Convert.ToInt32(btnDelete.AlternateText);
            if (appid != 0)
            {
                path = Docpath + "AWDN" + "\\" + idno + "\\APP_" + Convert.ToInt32(ViewState["AWDNO"].ToString());
            }
            else
            {
                path = Docpath + "TEMP_AWDFILES" + "\\" + idno + "\\APP_" + appid;
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
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_SB_AWARD", "*", "Award_Name='" + txtAward.Text + "'", "Organization_Address='" + txtOrganizationAdd.Text + "'", "");
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
}