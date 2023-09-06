// =========================================================================
// MODIFIED DATE : 17-FEB-2015
// MODIFY BY     : MRUNAL SINGH
// DESCRIPTION   : USED TO INSERT UPDATE AGENDA INFORMATION FOR THE COMMITEE
//               : ORGANISED & MAINTAIN COMPLETE CODE IN PROPER WAY, DOCUMENTATION,
//               : DISPLAY COMMITTEE DEPARTMENTWISE.
// ==========================================================================
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
using System.Configuration;
using System.IO;

public partial class MEETING_MANAGEMENT_TRANSACTION_Meeting_TableItems : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController OBJmc = new MeetingController();
    public static int pk_agenda_id;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

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
                }
                FILL_DROPDOWN();
                objMM.LOCK = 'N';
                objMM.TABLE_ITEM = 'N';
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
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
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    // This method is used to fill Committee list.
    public void FILL_DROPDOWN()
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "DEPTNO=" + Session["userEmpDeptno"] + " OR " + Session["userEmpDeptno"] + "=0", "NAME");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtdate.Text) >= Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy")))
            {
                if (ViewState["action"] != null)
                {
                    objMM.CODE = Convert.ToString(txtcode.Text.Trim());
                    objMM.FILEPATH = Convert.ToString(ViewState["FILENAME"]);
                    objMM.FILE_NAME = Convert.ToString(ViewState["FILE_NAME"]);
                    objMM.AGENDA_NO = txtnumber.Text;
                    objMM.FK_MEETING_ID = Convert.ToInt32(ddlCommitee.SelectedValue);
                    objMM.MEETING_DATE = txtdate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtdate.Text.Trim());
                    objMM.MEETING_TIME = txttime.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txttime.Text.Trim());
                    objMM.TITLE = txttitle.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txttitle.Text.Trim());
                    objMM.VENUE = Convert.ToString(txtvenue.Text);
                    objMM.USERID = Convert.ToInt32(Session["userno"]);

                    if (chklock.Checked == true)
                    {
                        objMM.LOCK = 'Y';
                    }
                    else
                    {
                        objMM.LOCK = 'N';
                    }
                    objMM.TABLE_ITEM = 'N';
                    //Save Data
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        objMM.PK_AGENDA_ID = 0;
                        if (OBJmc.AddUpdate_Agenda_Details(objMM) != 0)
                        {
                            BindlistView();
                            ViewState["action"] = "add";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                            Clear();
                            ddlCommitee.SelectedIndex = 0;
                        }
                    }
                    //Update Data
                    else
                    {
                        objMM.PK_AGENDA_ID = pk_agenda_id;
                        CustomStatus cs = (CustomStatus)OBJmc.AddUpdate_Agenda_Details(objMM);
                        if (OBJmc.AddUpdate_Agenda_Details(objMM) != 0)
                        {
                            BindlistView();
                            ViewState["action"] = "add";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                            Clear();
                            ddlCommitee.SelectedIndex = 0;
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("Meeting Date Must Be Equal To Or Greater Than Current date.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string path = string.Empty;
    public string dbPath = string.Empty;

    // This method is used to create table. 
    public void CreateTable()
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
        ViewState["FILE"] = dt;
    }
    //This method is used to bind list.
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "*", "", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lvAgenda.DataSource = ds;
                    lvAgenda.DataBind();
                }
            }
            else
            {
                lvAgenda.DataSource = null;
                lvAgenda.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear controls.
    private void ClearRec()
    {
        txtdate.Text = string.Empty;
        txtcode.Text = string.Empty;
        txtnumber.Text = string.Empty;
        txttime.Text = string.Empty;
        txttitle.Text = string.Empty;
        txtvenue.Text = string.Empty;
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
        lvfile.DataSource = null;
        lvfile.DataBind();
        ViewState["FILE"] = null;
    }
    // This method is used to clear the controls.
    private void Clear()
    {
        txtdate.Text = string.Empty;
        txtcode.Text = string.Empty;
        txtnumber.Text = string.Empty;
        txttime.Text = string.Empty;
        txttitle.Text = string.Empty;
        txtvenue.Text = string.Empty;
        ddlCommitee.SelectedIndex = 0;
        if (ddlpremeeting.Items.Count > 0)
        {
            ddlpremeeting.Items.Clear();
            ddlpremeeting.DataSource = null;
            ddlpremeeting.DataBind();
        }
        else
        {
        }
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
        lvfile.DataSource = null;
        lvfile.DataBind();
        ViewState["FILE"] = null;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Enabled = true;
        DataSet DS_MEETINCODE = objCommon.FillDropDown("TBL_MM_AGENDA", "DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "");
        if (DS_MEETINCODE.Tables.Count > 0)
        {
            if (DS_MEETINCODE.Tables[0].Rows.Count > 0)
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.Items.Add("Please Select");
                ddlpremeeting.SelectedItem.Value = "0";
                ddlpremeeting.DataTextField = "MEETING_CODE";
                ddlpremeeting.DataValueField = "MEETING_CODE";
                ddlpremeeting.DataSource = DS_MEETINCODE.Tables[0];
                ddlpremeeting.DataBind();
                ddlpremeeting.SelectedIndex = 0;
            }
            else
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.DataSource = null;
                ddlpremeeting.DataBind();
            }
        }
        ClearRec();
        pnlFile.Visible = false;
        DataSet ds1 = objCommon.FillDropDown("TBL_MM_COMITEE", "CODE", "ID", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                txtcode.Text = Convert.ToString(dr["CODE"]);
            }
        }
        ds1 = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS", " DISTINCT FK_COMMITTE ", "METTINGCODE", "FK_COMMITTE=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");

        if (ds1.Tables[0].Rows.Count >= 0)
        {
            int current_code = Convert.ToInt32(ds1.Tables[0].Rows.Count) + 1;
            txtcode.Text = txtcode.Text + " " + current_code;
        }
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "(COUNT(PK_AGENDA)) as totalcode ", "(COUNT(PK_AGENDA)+1) as maxcode ", "MEETING_CODE='" + txtcode.Text + "'and FK_MEETING= " + Convert.ToInt32(ddlCommitee.SelectedValue) + " ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                txtnumber.Text = txtcode.Text + "." + Convert.ToString(dr["maxcode"]);
            }
        }
        DataSet ds_Details = objCommon.FillDropDown("TBL_MM_AGENDA", "(COUNT(PK_AGENDA)+1)A1", "convert(char(11),MEETINGDATE,103)MEETINGDATE,MEETINGTIME,VENUE,Lock ", "FK_MEETING ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and MEETING_CODE ='" + txtcode.Text + "'group by MEETINGDATE,MEETINGTIME,VENUE,Lock", "");
        if (ds_Details.Tables.Count > 0)
        {
            if (ds_Details.Tables[0].Rows.Count > 0)
            {
                txtdate.Text = (ds_Details.Tables[0].Rows[0]["MEETINGDATE"].ToString());
                txttime.Text = ds_Details.Tables[0].Rows[0]["MEETINGTIME"].ToString();
                txtvenue.Text = ds_Details.Tables[0].Rows[0]["VENUE"].ToString();
            }
        }
        BindlistView();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            pk_agenda_id = int.Parse(btnEdit.CommandArgument);
            ViewState["PK_AGENDA_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "*", "", "PK_AGENDA=" + pk_agenda_id + " ", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtnumber.Text = Convert.ToString(dr["AGENDANO"]);
                    txttitle.Text = Convert.ToString(dr["AGENDATITAL"]);
                    txtvenue.Text = Convert.ToString(dr["VENUE"]);
                    txtdate.Text = Convert.ToString(dr["MEETINGDATE"]);
                    txttime.Text = Convert.ToString(dr["MEETINGTIME"]);
                    txtcode.Text = Convert.ToString(dr["MEETING_CODE"]);
                    char chlock = Convert.ToChar(dr["Lock"]);
                    if (chlock == 'Y')
                    {
                        chklock.Checked = true;
                    }
                    else
                    {
                        chklock.Checked = false;
                    }
                }
            }
            RETPATH = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\AGENDA\\" + txtnumber.Text;
            BindListViewFiles(RETPATH);
            btnSubmit.Visible = true;
            btnCancel.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void chklock_CheckedChanged(object sender, EventArgs e)
    {
        if (chklock.Checked == true)
        {
            objMM.LOCK = 'Y';
        }
        else
        {
            objMM.LOCK = 'N';
        }
    }

    protected void ddlpremeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "(COUNT(PK_AGENDA)+1)A1", "convert(char(11),MEETINGDATE,103)MEETINGDATE,MEETINGTIME,VENUE,Lock ", "FK_MEETING ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and MEETING_CODE ='" + ddlpremeeting.SelectedItem.Text + "'group by MEETINGDATE,MEETINGTIME,VENUE,Lock", "");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcode.Text = ddlpremeeting.SelectedItem.Text;
                txtnumber.Text = ddlpremeeting.SelectedItem.Text + "." + ds.Tables[0].Rows[0]["A1"].ToString();
                txtdate.Text = (ds.Tables[0].Rows[0]["MEETINGDATE"].ToString());
                txttime.Text = ds.Tables[0].Rows[0]["MEETINGTIME"].ToString();
                txtvenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
                char lock1 = Convert.ToChar(ds.Tables[0].Rows[0]["Lock"]);
                if (lock1 == 'Y')
                {
                    btnSubmit.Enabled = false;
                    objCommon.DisplayMessage("Agenda is Locked For This Meeting.", this.Page);
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
        }
    }

    //Check for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".zip", ".ZIP", ".txt", ".TXT" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //CHECKING FILE EXISTS OR NOT
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (txtcode.Text != string.Empty && txtnumber.Text != string.Empty)
                    {
                        string file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\AGENDA\\" + txtnumber.Text;

                        //  string file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text +"\\" + txtnumber.Text + "\\AGENDA";
                        // +agendano + "_" + fupath.FileName;
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }
                        ViewState["FILENAME"] = file;
                        path = file + "//" + FileUpload1.FileName; ;
                        dbPath = file;
                        string filename = FileUpload1.FileName;
                        ViewState["FILE_NAME"] = filename;
                        //CHECKING FOLDER EXISTS OR NOT
                        if (!System.IO.Directory.Exists(path))
                        {
                            FileUpload1.PostedFile.SaveAs(path);
                        }
                        BindListViewFiles(file);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Meeting Code.\\n Please Enter Agenda Number.'  );  ", true);
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Upload Valid Files.[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
                    FileUpload1.Focus();
                }
            }

            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please Select File.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to bind the list of Files.
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
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }
    // This method is used to download files.
    public void DownloadFile(string filePath)
    {
        try
        {
            path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\AGENDA\\" + txtnumber.Text;
            FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            Response.AddHeader("content-disposition", "attachment; filename=" + filePath);
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
    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;
        f_name = obj.ToString();
        return f_name;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            //path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\" + txtnumber.Text;
            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\AGENDA\\" + txtnumber.Text;
            //CHECKING FILE EXISTS OR NOT
            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted.');", true);
                BindListViewFiles(path);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Transaction_Meeting_TableItems.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}
