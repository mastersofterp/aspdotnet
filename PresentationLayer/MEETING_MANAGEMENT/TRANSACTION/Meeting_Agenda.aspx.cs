// ============================================================================
// CREATE BY   :      
// CREATE DATE : 03/09/2014
// MODIFY DATE : 15/09/2014
// MODIFY BY   : MRUNAL SINGH
// DESCRIPTION : USED TO INSERT UPDATE AGENDA INFORMATION FOR THE COMMITEE
//               BACK DATED ENTRIES SHOULD BE ACCEPT WHEN CREATING AGENDA. 
// (06-JUN-2017) ADD SEND NOTIFICATION FUNCTIONALITY TO THE COMMITTEE MEMBERS
// =============================================================================

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
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;



public partial class MEETING_MGT_Transaction_Meeting_Agenda : System.Web.UI.Page
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
                if (Convert.ToInt32(Session["usertype"]) == 1)
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                else
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");

                objCommon.FillDropDownList(ddlVenue, "tbl_mm_Venue", "PK_VENUEID", "VENUE", "[STATUS] = 0", "VENUE");
                FILL_DROPDOWN();
                BindlistView();
                objMM.LOCK = 'N';
                objMM.TABLE_ITEM = 'N';
                //btnSendMail.Visible = false;  //SHAIKH JUNED 27-06-2022
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    public void FILL_DROPDOWN()
    {
        objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITY");
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");
        objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
    }

    


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string[] result1 = txttime.Text.Split(' ');
            string[] result2 = txtToTime.Text.Split(' ');
            DateTime d1 = DateTime.Parse(result1[0]);
            TimeSpan timeFrom = TimeSpan.Parse(d1.ToString("HH:mm"));

            DateTime d2 = DateTime.Parse("12:59:59");
            TimeSpan checkTime = TimeSpan.Parse(d2.ToString("hh:mm"));

            DateTime d3 = DateTime.Parse(result2[0]);
            TimeSpan timeTo = TimeSpan.Parse(d3.ToString("HH:mm"));

            if (timeFrom > checkTime)
            {
                MessageBox("Enter Meeting From Time In 12Hour Format");
                return;
            }
            if (timeTo > checkTime)
            {
                MessageBox("Enter Meeting To Time In 12Hour Format");
                return;
            }


            if (!txtLastDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtLastDate.Text), Convert.ToDateTime(txtdate.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Last Date Of Approval Can Not Be Greater Than Meeting Date.');", true);
                    txtLastDate.Focus();
                    return;
                }
            }
            if (txttime.Text == txtToTime.Text)
            {
                MessageBox("Meeting From Time and To Time Should not be Same");
                return;   //Shaikh Juned (25-06-2022)
            }
            //DateTime d11 = DateTime.Parse(txttime.Text);
            //DateTime d22 = DateTime.Parse(txtToTime.Text);
            //TimeSpan timeFro = TimeSpan.Parse(d11.ToString("HH:mm"));
            //TimeSpan timeT = TimeSpan.Parse(d22.ToString("HH:mm"));

            //if (timeFrom > timeTo)
            //{
            //    MessageBox("Meeting From Time Should not be Greater Than To Time");

            //    return;
            //}




            if (ViewState["action"] != null)
            {
                objMM.DEPTNO = Convert.ToInt32(Session["UA_EmpDeptNo"]);
                objMM.CODE = Convert.ToString(txtcode.Text.Trim());
                objMM.FILEPATH = Convert.ToString(ViewState["FILENAME"]);
                objMM.FILE_NAME = Convert.ToString(ViewState["FILE_NAME"]);
                objMM.AGENDA_NO = txtnumber.Text;
                objMM.FK_MEETING_ID = Convert.ToInt32(ddlCommitee.SelectedValue);
                objMM.MEETING_DATE = txtdate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtdate.Text.Trim());
                objMM.MEETING_TIME = txttime.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txttime.Text.Trim());
                objMM.MEETING_TO_TIME = txtToTime.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtToTime.Text.Trim());

                objMM.TITLE = txttitle.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txttitle.Text.Trim());
                // objMM.VENUE = ddlVenue.SelectedValue;///Convert.ToString(txtvenue.Text);
                objMM.USERID = Convert.ToInt32(Session["userno"]);
                objMM.LAST_DATE = txtLastDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtLastDate.Text.Trim());

                objMM.ADDLINE2 = Convert.ToString(txtAddLine.Text);
                objMM.CITY = Convert.ToInt32(ddlCity.SelectedValue);
                objMM.STATE = Convert.ToInt32(ddlState.SelectedValue);
                objMM.VENUEID = Convert.ToInt32(ddlVenue.SelectedValue);
                if (txtZipCode.Text == string.Empty)
                {
                    objMM.ZIPCODE = 0;
                }
                else
                {
                    objMM.ZIPCODE = Convert.ToInt32(txtZipCode.Text);
                }

                objMM.COUNTRY = Convert.ToInt32(ddlCountry.SelectedValue);

                
                objMM.TABLE_ITEM = 'N';

                //Save Data
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckPurpose();

                    if (result == false)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("There Is Already Meeting Schedueled On Same Venue!");
                        return;
                    }
                    else
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
                }
                //Update Data
                else
                {
                     bool result = CheckPurpose();

                     if (result == false)
                     {
                         //objCommon.DisplayMessage("Record Already Exist", this);
                         MessageBox("There Is Already Meeting Schedueled On Same Venue!");
                         return;
                     }
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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string path = string.Empty;
    public string dbPath = string.Empty;

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

    // This method is used to bind the list View.
    private void BindlistView()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA A INNER JOIN tbl_mm_Venue V ON(V.PK_VENUEID=A.VENUEID)", "A.PK_AGENDA, A.AGENDANO, A.AGENDATITAL", "A.MEETINGDATE, A.MEETINGTIME,V.VENUE, A.MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lvAgenda.DataSource = ds;
                    lvAgenda.DataBind();
                }
                lvAgenda.Visible = true;
            }
            else
            {
                lvAgenda.DataSource = null;
                lvAgenda.DataBind();
                lvAgenda.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to clear the controls.
    private void Clear1()
    {
        txtdate.Text = string.Empty;
        txtcode.Text = string.Empty;
        txtnumber.Text = string.Empty;
        txttime.Text = string.Empty;
        txttitle.Text = string.Empty;
        txtToTime.Text = string.Empty;
        txtLastDate.Text = string.Empty;
        // txtvenue.Text = string.Empty;
        ddlVenue.SelectedIndex = 0;
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
        lvfile.DataSource = null;
        lvfile.DataBind();
        lvfile.Visible = false;
        ViewState["FILE"] = null;
    }

    // This method is used to check Agenda.
    public void CheckAgenda()
    {
        try
        {
            string strAgenda = string.Empty;
            ViewState["strAgenda"] = objCommon.LookUp("TBL_MM_AGENDA", "AGENDANO", "AGENDANO='" + txtnumber.Text + "'");

            if (ViewState["strAgenda"].ToString().Equals(""))
            {
                string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + txtnumber.Text;

                if (Directory.Exists(path))
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        File.Delete(file);
                    }
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
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.CheckAgenda -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to clear the controls.
    private void Clear()
    {
        CheckAgenda();
        txtdate.Text = string.Empty;
        txtcode.Text = string.Empty;
        txtnumber.Text = string.Empty;
        txttime.Text = string.Empty;
        txttitle.Text = string.Empty;
        //txtvenue.Text = string.Empty;
        ddlVenue.SelectedIndex = 0;
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
        lvfile.Items.Clear();
        lvfile.DataSource = null;
        lvfile.DataBind();
        lvfile.Visible = false;
        ViewState["FILE"] = null;
        txtAddLine.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtZipCode.Text = string.Empty;
        //ddlCollege.SelectedIndex = 0;     
        txtLastDate.Text = string.Empty;
        txtToTime.Text = string.Empty;
        ViewState["PK_AGENDA_ID"] = null;
        ViewState["action"] = "add";

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }


    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Enabled = true;
        DataSet DS_MEETINCODE = objCommon.FillDropDown("TBL_MM_AGENDA", "distinct MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "");
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
        Clear1();
        pnlFile.Visible = false;
        DataSet ds1 = objCommon.FillDropDown("TBL_MM_COMITEE", "CODE", "ID", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");

        if (ds1.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                txtcode.Text = Convert.ToString(dr["CODE"]);
            }
        }
        //ds1 = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS", "DISTINCT FK_COMMITTE ", "METTINGCODE", "FK_COMMITTE=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
        ds1 = objCommon.FillDropDown("TBL_MM_AGENDA", "DISTINCT PK_AGENDA ", "MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
        if (ds1.Tables[0].Rows.Count >= 0)
        {
            int current_code = Convert.ToInt32(ds1.Tables[0].Rows.Count)+1;
            txtcode.Text = txtcode.Text + " " + current_code;
        }
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "(COUNT(PK_AGENDA)) AS TOTALCODE ", "(COUNT(PK_AGENDA)+1) AS MAXCODE ", "MEETING_CODE='" + txtcode.Text + "'and FK_MEETING= " + Convert.ToInt32(ddlCommitee.SelectedValue) + " ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                txtnumber.Text = txtcode.Text + "." + Convert.ToString(dr["MAXCODE"]);
            }
        }
        DataSet ds_Details = objCommon.FillDropDown("TBL_MM_AGENDA", "(COUNT(PK_AGENDA)+1)A1", "CONVERT(CHAR(11), MEETINGDATE, 103) AS MEETINGDATE,MEETINGTOTIME,VENUEID,MEETINGTIME,VENUE,Lock,ADD_LINE,CITY,STATE,COUNTRY,ZIPCODE, LAST_DATE", "FK_MEETING ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' AND MEETING_CODE ='" + txtcode.Text + "'GROUP BY MEETINGDATE,MEETINGTIME,VENUE,LOCK,ADD_LINE,CITY,STATE,COUNTRY,ZIPCODE, LAST_DATE,MEETINGTOTIME,VENUEID", "");
        if (ds_Details.Tables.Count > 0)
        {
            if (ds_Details.Tables[0].Rows.Count > 0)
            {
                
                //txtdate.Text = (ds_Details.Tables[0].Rows[0]["MEETINGDATE"].ToString());
                //txttime.Text = ds_Details.Tables[0].Rows[0]["MEETINGTIME"].ToString();
                
                //txtAddLine.Text = ds_Details.Tables[0].Rows[0]["ADD_LINE"].ToString();
                //ddlCity.SelectedValue = ds_Details.Tables[0].Rows[0]["CITY"].ToString();
                //ddlState.SelectedValue = ds_Details.Tables[0].Rows[0]["STATE"].ToString();
                //ddlCountry.SelectedValue = ds_Details.Tables[0].Rows[0]["COUNTRY"].ToString();
                //txtZipCode.Text = ds_Details.Tables[0].Rows[0]["ZIPCODE"].ToString();
                //txtLastDate.Text = ds_Details.Tables[0].Rows[0]["LAST_DATE"].ToString();
                //txtToTime.Text = ds_Details.Tables[0].Rows[0]["MEETINGTOTIME"].ToString();
                //ddlVenue.SelectedValue = ds_Details.Tables[0].Rows[0]["VENUEID"].ToString();
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
                    txtdate.Text = Convert.ToString(dr["MEETINGDATE"]);
                    txttime.Text = Convert.ToString(dr["MEETINGTIME"]);
                    txtcode.Text = Convert.ToString(dr["MEETING_CODE"]);                   
                    ddlVenue.SelectedValue = Convert.ToString(dr["VENUEID"]);
                    txtToTime.Text = Convert.ToString(dr["MEETINGTOTIME"]);
                    txtLastDate.Text = Convert.ToString(dr["LAST_DATE"]);                     
                }
            }
            RETPATH = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + txtnumber.Text;
            BindListViewFiles(RETPATH);
            btnSubmit.Visible = true;
            btnCancel.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //protected void chklock_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chklock.Checked == true)
    //    {
    //        objMM.LOCK = 'Y';
    //    }
    //    else
    //    {
    //        objMM.LOCK = 'N';
    //    }
    //}


    protected void ddlpremeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "(COUNT(PK_AGENDA)+1)A1", "CONVERT(CHAR(11),MEETINGDATE,103)MEETINGDATE,MEETINGTIME,VENUE,Lock,LAST_DATE", "FK_MEETING ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and MEETING_CODE ='" + ddlpremeeting.SelectedItem.Text + "'GROUP BY MEETINGDATE,MEETINGTIME,VENUE,LOCK,LAST_DATE", "");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcode.Text = ddlpremeeting.SelectedItem.Text;
                txtnumber.Text = ddlpremeeting.SelectedItem.Text + "." + ds.Tables[0].Rows[0]["A1"].ToString();
                txtdate.Text = (ds.Tables[0].Rows[0]["MEETINGDATE"].ToString());
                txttime.Text = ds.Tables[0].Rows[0]["MEETINGTIME"].ToString();
                //txtvenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
                txtLastDate.Text = ds.Tables[0].Rows[0]["LAST_DATE"].ToString();
                char lock1 = Convert.ToChar(ds.Tables[0].Rows[0]["LOCK"]);
                if (lock1 == 'Y')
                {
                    btnSubmit.Enabled = false;
                    btnSendMail.Enabled = false;
                    objCommon.DisplayMessage("Agenda Locked For This Meeting.", this.Page);
                }
                else
                {
                    btnSubmit.Enabled = true;
                    btnSendMail.Enabled = true;
                }
            }
        }
    }

    //Check for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".docx", ".xlsx", ".DOCX", ".XLSX", ".zip", ".ZIP", ".txt", ".TXT", ".rar", ".RAR" };
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
                        string file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + txtnumber.Text;

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
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Meeting Code.\\n Please Enter Agenda Number.');  ", true);
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
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
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                    lvfile.Visible = true;
                }
                else
                {
                    lvfile.DataSource = null;
                    lvfile.DataBind();
                    lvfile.Visible = false;
                }
            }
            else
            {
                lvfile.DataSource = null;
                lvfile.DataBind();
                lvfile.Visible = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }


    public void DownloadFile(string filePath)
    {
        try
        {
            path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + txtnumber.Text;
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

            //strpath = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\" + txtnumber.Text;
            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + txtnumber.Text;//Chenged by Andoju vijay on 2019-12-11
            //CHECKING FILE EXISTS OR NOT
            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                //Modified by Saahil Trivedi 25/02/2022
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
                BindListViewFiles(path);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
        // objCommon.FillDropDownList(ddlMeeting, "TBL_MM_MEETING_MASTER MM INNER JOIN Tbl_MM_COMITEE MC ON (MM.ID = MC.ID)", "MM.MEETING_NO", "MM.MEETING_NAME", "MC.COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue), "MM.MEETING_NO");        
    }



    // This button is use to send emails to committee members
    protected void btnSendMail_Click(object sender, EventArgs e)
    
    {
        try
        {
            if (ddlCommitee.SelectedIndex > 0 && txtcode.Text != string.Empty) //ddlpremeeting.SelectedIndex > 0)
            {

                string fromEmailId = string.Empty;
                string fromEmailPwd = string.Empty;
                string body = string.Empty;

                DataSet ds = OBJmc.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["IS_MOM_EMAIL"].ToString() != "0")
                    {
                        fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                        fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                        string receiver = string.Empty;
                        string userid = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (receiver == string.Empty)
                            {
                                receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                userid = ds.Tables[0].Rows[i]["userid"].ToString();
                            }
                            else
                            {
                                receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
                                userid = userid + "," + ds.Tables[0].Rows[i]["userid"].ToString();
                            }
                        }


                        sendmail(fromEmailId, fromEmailPwd, "Related to Meeting.", "Dear Members", userid);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage("Please Select Committee & Meeting.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string Sub, string body, string userid)
    {
        try
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            // MailMessage mailMessage = new MailMessage();

            mailMessage.Subject = Sub;
            string AgendaItemList = string.Empty;
            string AgendaContentList = string.Empty;
            DataSet dsCommittee = null;
            dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA A INNER JOIN TBL_MM_VENUE V ON (A.VENUEID = V.PK_VENUEID)", "distinct A.MEETING_CODE, A.AGENDANO", "A.MEETINGDATE, A.MEETINGTIME, V.VENUE, A.LAST_DATE", "A.FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND A.MEETING_CODE='" + txtcode.Text.Trim() + "'", "");

            if (ds.Tables[0].Rows.Count < 0)
            {
                objCommon.DisplayMessage("Agendas Are Not Defined For Selected Meeting.", this.Page);
                return;
            }

            //string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + txtcode.Text;
            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + txtnumber.Text;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            if (System.IO.Directory.Exists(path))
            {
                FileInfo[] files = dir.GetFiles();
                if (Convert.ToBoolean(files.Length))
                {
                    foreach (FileInfo fi in files)
                    {
                        Attachment file = new Attachment(fi.FullName.ToString());
                        mailMessage.Attachments.Add(file);
                    }
                }
                else
                {
                }
            }

            string[] idno = userid.Split(',');

            for (int j = 0; j < idno.Length; j++)
            {
                string MemberEmailId = string.Empty;
                mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));            

                MemberEmailId = objCommon.LookUp("TBL_MM_MENBERDETAILS MD INNER JOIN TBL_MM_RELETIONMASTER RM ON (MD.PK_CMEMBER = RM.FK_MEMBER)", "ISNULL(T_EMAIL,' ') AS T_EMAIL", "MD.USERID=" + idno[j] + " AND RM.FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue));

                mailMessage.To.Add(MemberEmailId);
                var MailBody = new StringBuilder();
                MailBody.AppendFormat("{0}\n", "Dear Member,");
                MailBody.AppendLine(@"<br />");
                MailBody.AppendLine(@"<br />Committee   : " + dsCommittee.Tables[0].Rows[0]["NAME"]);
                MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]);
                MailBody.AppendLine(@"<br />Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["MEETINGDATE"]).ToString("yyyy-MM-dd"));
                MailBody.AppendLine(@"<br />Time : " + ds.Tables[0].Rows[0]["MEETINGTIME"]);
                MailBody.AppendLine(@"<br />Venue : " + ds.Tables[0].Rows[0]["VENUE"]);               
               
                MailBody.AppendLine(@"<br />-----------------------------------------------------------");


                DataSet dsAgendaList = objCommon.FillDropDown("TBL_MM_AGENDA", "PK_AGENDA, MEETING_CODE, AGENDANO", "ROW_NUMBER() OVER(ORDER BY PK_AGENDA ASC) SrNo, AGENDATITAL", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + txtcode.Text.Trim() + "'", "");

                if (dsAgendaList.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsAgendaList.Tables[0].Rows.Count; i++)
                    {

                        DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA_CONTENT_DETAILS ACD ON (AC.ACID = ACD.ACID)", "ACD.SRNO", "ACD.CONTENT_DETAILS", "AC.AGENDA_ID =" + dsAgendaList.Tables[0].Rows[i]["PK_AGENDA"].ToString(), "");
                        if (AgendaItemList == string.Empty)
                        {
                            // AgendaItemList = dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString();
                            MailBody.AppendLine(@"<br /><b>Meeting Title :</b>" + dsAgendaList.Tables[0].Rows[i]["SrNo"].ToString() + ") " + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString());

                            if (dsContent.Tables[0].Rows.Count > 0)
                            {
                                for (int k = 0; k < dsContent.Tables[0].Rows.Count; k++)
                                {
                                    if (AgendaContentList == string.Empty)
                                    {
                                        AgendaContentList = dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
                                    }
                                    else
                                    {
                                        AgendaContentList = AgendaContentList + "<br />" + dsContent.Tables[0].Rows[k]["CONTENT_DETAILS"].ToString();
                                    }
                                }

                                MailBody.AppendLine(@"<br />   " + AgendaContentList);
                                MailBody.AppendLine(@"<br />-----------------------------------------------------------");
                                AgendaItemList = string.Empty;
                                AgendaContentList = string.Empty;
                            }

                        }
                        else
                        {
                            // AgendaItemList = AgendaItemList + "<br />" + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString();
                        }
                    }
                }              

               
                mailMessage.Body = MailBody.ToString();
                mailMessage.IsBodyHtml = true;



                SmtpClient smt = new SmtpClient("smtp.gmail.com");

                smt.UseDefaultCredentials = false;
                smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
                smt.Port = 587;
                smt.EnableSsl = true;


                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                //smt.Timeout = 2000000; // Add Timeout property                      

                smt.Send(mailMessage);


                mailMessage.To.Clear();
                //string script = "<script>alert('Mail Sent Successfully')</script>";
                //ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
                MessageBox("Mail Sent Successfully");

                // mailMessage.To.RemoveAt(j);
            }
            mailMessage.Attachments.Dispose();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCommitee.SelectedIndex > 0 ) //&& txtcode.Text != string.Empty) //ddlpremeeting.SelectedIndex > 0)
            {
                ShowAgendaReport("pdf", "AgendaListReport.rpt");
            }
            else
            {
               
                MessageBox("Please Select Committee");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowAgendaReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=AgendaListReport" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_COMMITTEEID=" + Convert.ToInt32(ddlCommitee.SelectedValue);  //@P_MEETING_CODE=" + txtcode.Text + ",
           
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.ShowCommitteeListReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //if (rdbCommitteeType.SelectedValue == "U")
            //{
            //    trCollegeName.Visible = false;
            //    ddlCollege.SelectedIndex = 0;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
            //}
            //else
            //{
            //    trCollegeName.Visible = true;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancelMeeting_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlCommitee.SelectedIndex > 0 && txtcode.Text != string.Empty)
            {
                //============== SEND EMAIL TO CANCEL MEETINGS ==============//

                string fromEmailId = string.Empty;
                string fromEmailPwd = string.Empty;
                string body = string.Empty;

                // DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER a INNER JOIN Tbl_MM_COMITEE b on (a.FK_COMMITEE =b.ID) INNER JOIN TBL_MM_MENBERDETAILS c on(a.FK_MEMBER=c.PK_CMEMBER)", "c.PK_CMEMBER", "c.T_EMAIL as EMAIL,c.T_PHONE", "a.FK_COMMITEE='" + ddlCommitee.SelectedValue + "'" + "   AND c.T_EMAIL<>''", "");
                DataSet ds = OBJmc.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                    fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                    string receiver = string.Empty;
                    string userid = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (receiver == string.Empty)
                        {
                            receiver = ds.Tables[0].Rows[i]["EMAIL"].ToString();
                            userid = ds.Tables[0].Rows[i]["userid"].ToString();
                        }
                        else
                        {
                            receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
                            userid = userid + "," + ds.Tables[0].Rows[i]["userid"].ToString();
                        }
                    }

                    sendmailToCancelMeeting("recruitment@iitms.co.in", "IITMS@123", receiver, "Meeting Is Postpone", "Dear Members", userid);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Committee & Meeting.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnCancelMeeting_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    public void sendmailToCancelMeeting(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    {
        try
        {

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            // MailMessage mailMessage = new MailMessage();

            mailMessage.Subject = Sub;
            string AgendaItemList = string.Empty;

            DataSet dsCommittee = null;
            dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + txtcode.Text.Trim() + "'", "");



            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Member, {0}\n", "");
            MailBody.AppendLine(@"<br />Committee   : " + dsCommittee.Tables[0].Rows[0]["NAME"]);
            MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]);
            MailBody.AppendLine(@"<br />Meeting Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["MEETINGDATE"]).ToString("yyyy-MM-dd"));
            MailBody.AppendLine(@"<br />Meeting Time : " + ds.Tables[0].Rows[0]["MEETINGTIME"]);
            MailBody.AppendLine(@"<br />Meeting Venue : " + ds.Tables[0].Rows[0]["VENUE"]);
            MailBody.AppendLine(@"<br />The above meeting is postpone or it would be held at a later date or time.");

            mailMessage.Body = MailBody.ToString();
            mailMessage.IsBodyHtml = true;

            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            //smt.Timeout = 2000000; // Add Timeout property                      
            smt.Send(mailMessage);

            //string script = "<script>alert('Mail Sent Successfully')</script>";
            //ClientScript.RegisterStartupScript(this.GetType(), "mailSent", script);
            MessageBox("Mail Sent Successfully");


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
        dsPURPOSE = objCommon.FillDropDown("TBL_MM_AGENDA", "FK_MEETING,VENUEID,MEETINGTIME", "MEETINGDATE,MEETINGTOTIME", "VENUEID=" + Convert.ToInt32(ddlVenue.SelectedValue) + "AND MEETINGDATE='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "'", "FK_MEETING");


        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {

            //string Time = "16:23:01";
            //DateTime date = DateTime.Parse(dsPURPOSE.Tables[0].Rows[0]["MEETINGTIME"].ToString(), System.Globalization.CultureInfo.CurrentCulture);

            //string t = date.ToString("HH:mm:ss tt");

            TimeSpan ExisttimeFrom = TimeSpan.Parse(Convert.ToDateTime(dsPURPOSE.Tables[0].Rows[0]["MEETINGTIME"]).ToString("HH:mm"));
            TimeSpan timeFrom = TimeSpan.Parse(Convert.ToDateTime(txttime.Text).ToString("HH:mm"));
            TimeSpan ExisttimeTo = TimeSpan.Parse(Convert.ToDateTime(dsPURPOSE.Tables[0].Rows[0]["MEETINGTOTIME"]).ToString("HH:mm"));
            TimeSpan timeTo = TimeSpan.Parse(Convert.ToDateTime(txtToTime.Text).ToString("HH:mm"));
            if (dsPURPOSE.Tables[0].Rows[0]["FK_MEETING"].ToString() == ddlCommitee.SelectedValue)
            {
                result = false;
            }
            else if (dsPURPOSE.Tables[0].Rows[0]["FK_MEETING"].ToString() != ddlCommitee.SelectedValue && Convert.ToDateTime(dsPURPOSE.Tables[0].Rows[0]["MEETINGDATE"]) == Convert.ToDateTime(txtdate.Text) && dsPURPOSE.Tables[0].Rows[0]["VENUEID"].ToString() == ddlVenue.SelectedValue.ToString() && (ExisttimeFrom == timeFrom || timeFrom <= ExisttimeTo))
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }
        else
        {
            result = true;
        }


        return result;


    }

}
