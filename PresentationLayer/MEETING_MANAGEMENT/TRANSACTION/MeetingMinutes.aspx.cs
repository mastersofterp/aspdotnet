// =========================================================================
// MODIFIED DATE : 17-FEB-2015
// MODIFY BY     : MRUNAL SINGH
// DESCRIPTION   : ORGANISED COMPLETE CODE IN PROPER WAY, DOCUMENTATION,
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
using System.IO;
using System.Configuration;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;


public partial class MEETING_MANAGEMENT_Transaction_MeetingMinutes : System.Web.UI.Page
{    
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingEntity objmas = new MeetingEntity();
    MM_CONTROLLER objcon = new MM_CONTROLLER();
     MeetingMaster objMM = new MeetingMaster();
    public static int PK_AGENDA = 0;
    public static string Flag = "ADD";
    public static string PRE_FILEPATH = "";
    public static string PRE_FILENAME = "";
    public string path = string.Empty;
    public string dbPath = string.Empty;
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

                    if (Convert.ToInt32(Session["usertype"]) == 1)
                        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                    else
                        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");
                    FillCollege();
                }
                Flag = "ADD";
            }

            if (ddlMeeting.DataSource == null)
            {
                ddlMeeting.Enabled = false;
            }
            else
            {
                ddlMeeting.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MeetingMinutes.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used for page authorization.
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

    private void FillCollege()
    {
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}

    }

    
    // This method is used to clear the controls.
    private void Clear()
    {
        txtAgendaNo.Text = "";
        ddlCommitee.SelectedValue = "0";
        ddlMeeting.SelectedValue = "0";
    }


    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCommitee.SelectedValue != "0")
        {            
            objCommon.FillDropDownList(ddlMeeting, "TBL_MM_AGENDA AG", "DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' AND AG.MEETING_CODE NOT IN(SELECT DISTINCT METTINGCODE FROM TBL_MM_MEETINGDETAILS WHERE FK_COMMITTE='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' AND LOCK_MEET='Y')", "");
            DataSet DS_MEET = objCommon.FillDropDown("TBL_MM_AGENDA AG", "DISTINCT FK_MEETING", "MEETING_CODE", "FK_MEETING=" + ddlCommitee.SelectedValue, "");
            if (DS_MEET.Tables.Count > 0)
            {
                if (DS_MEET.Tables[0].Rows.Count > 0)
                {
                    ddlMeeting.Enabled = true;
                    DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER RR INNER JOIN TBL_MM_MENBERDETAILS ME ON (RR.FK_MEMBER = ME.PK_CMEMBER)", "DISTINCT RR.FK_MEMBER", "(title+' '+fname+' ' +mname+' '+lname)as NAME", "FK_COMMITEE='" + ddlCommitee.SelectedValue + "'", "RR.FK_MEMBER");
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lvStudents.DataSource = ds;
                            lvStudents.DataBind();
                            lvStudents.Visible = true;
                            pnlMemberList.Visible = true;
                        }
                        else
                        {
                            lvStudents.Items.Clear();
                            lvStudents.DataSource = null;
                            lvStudents.DataBind();
                            lvStudents.Visible = false;
                            pnlMemberList.Visible = false;
                        }
                    }
                    lvAgenda.DataSource = null;
                    lvAgenda.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage("Details Not Available.", this.Page);
                    ClearForm();
                }
            }                  
        }
        else
        {
            ddlMeeting.Items.Clear();
            ddlMeeting.DataSource = null;
            ddlMeeting.DataBind();
            ddlMeeting.Enabled = false;
        }

        ddlMeeting.Focus();
    }
    
    // This method is used to clear the controls.
    public void ClearForm()
    {      
        ddlCommitee.SelectedIndex = 0;
        ddlMeeting.Items.Clear();
        ddlMeeting.DataSource = null;
        ddlMeeting.DataBind();
        lblagtital.Text = string.Empty;
        txtAgendaDetails.Text = "";
        txtAgendaNo.Text = "";
        chklock.Checked = false;
        lvfile.Items.Clear();
        lvfile.DataSource = null;
        lvfile.DataBind();      
        lvAgenda.Items.Clear();
        lvAgenda.DataSource = null;
        lvAgenda.DataBind(); 
        lvStudents.Items.Clear();
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        pnlMemberList.Visible = false;
            pnlAgenda.Visible = false;
            pnlAgendaDetails.Visible = false;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {       
        ImageButton lnk = (ImageButton)sender;
        if (lnk != null)
        {
            PK_AGENDA = Convert.ToInt32(lnk.CommandArgument);
            DataSet ds1 = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS", "PK_MEETINGDETAILS", "AGENDADETAILS", "FK_AGENDA='" + PK_AGENDA + "' AND FK_COMMITTE='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' AND METTINGCODE='" +  ddlMeeting.SelectedItem.Text+"'", "");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        Flag = "UPDATE";
                    }
                    else
                    {
                        Flag = "ADD";
                    }
                }
            }          
            DataSet dsGetAgendaNo = objCommon.FillDropDown("TBL_MM_AGENDA AG   LEFT OUTER  JOIN TBL_MM_MEETINGDETAILS MS on(MS.FK_AGENDA = AG.PK_AGENDA)", "MS.AGENDADETAILS,MS.METTINGCODE,AG.AGENDANO,AG.AGENDATITAL", "AG.FILEPATH,AG.FILE_NAME,PK_MEETINGDETAILS,MS.LOCK_MEET", "AG .PK_AGENDA=" + PK_AGENDA, "");
            if (dsGetAgendaNo != null)
            {
                if (dsGetAgendaNo.Tables.Count > 0)
                {
                    pnlAgendaDetails.Visible = true;
                    if (dsGetAgendaNo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsGetAgendaNo.Tables[0].Rows)
                        {
                            lblagtital.Text = dr["AGENDATITAL"].ToString();
                            txtAgendaNo.Text = dr["AGENDANO"].ToString();
                            txtAgendaDetails.Text = dr["AGENDADETAILS"].ToString();
                            PRE_FILENAME = dr["FILE_NAME"].ToString();
                            string lock_m = dr["LOCK_MEET"].ToString();
                            if (lock_m == "Y")
                            {
                                chklock.Checked = true;
                            }
                            else
                            {
                                chklock.Checked = false;
                            }
                        }
                        RETPATH = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + txtAgendaNo.Text;
                        BindListViewFiles(RETPATH);
                    }
                    else
                    {
                       
                    }
                }
                if (dsGetAgendaNo.Tables.Count > 0)
                {
                   
                }
            }  
        }
    }

    //This method is used to bind the list of Designation in LisView.
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DropDownList chk = e.Item.FindControl("ddldesig") as DropDownList;
        DataSet ds = objCommon.FillDropDown("TBL_MM_COMMITEEDESIG", "PK_COMMITEEDES", "DESIGNATION", "STATUS=0", "");
        chk.DataTextField = "DESIGNATION";
        chk.DataValueField = "PK_COMMITEEDES";
        chk.DataSource = ds;
        chk.DataBind();
        chk.SelectedIndex = 0;
   }

    protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMeeting.SelectedValue != "0")
        {
            DataSet DS_CHECKLOCK = objCommon.FillDropDown("TBL_MM_AGENDA", "COUNT(PK_AGENDA) 'UNLOCK'", "FK_MEETING,MEETING_CODE", "LOCK='N'and MEETING_CODE='" + ddlMeeting.SelectedItem.Text + "' GROUP BY FK_MEETING,MEETING_CODE", "");
            if (DS_CHECKLOCK.Tables.Count > 0)
            {
                if (DS_CHECKLOCK.Tables[0].Rows.Count > 0)
                {
                    objCommon.DisplayMessage("Agenda are Not Locked For This Meeting.", this.Page);
                    //ddlMeeting.DataSource = null;
                    //ddlMeeting.DataBind();
                    ddlMeeting.SelectedValue = "0";
                    pnlAgenda.Visible = false;
                    
                }
                else
                {
                    DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", "PK_AGENDA", "MEETINGDATE,MEETINGTIME,AGENDANO,AGENDATITAL,VENUE,MEETING_CODE", " MEETING_CODE ='" + ddlMeeting.SelectedItem.Text + "'", "");
                    lvAgenda.DataSource = ds;
                    lvAgenda.DataBind();
                    lvAgenda.Visible = true;
                    pnlAgenda.Visible = true;
                }
            }
            if (lvStudents.Items.Count > 0)
            {
                for (int i = 0; i < lvStudents.Items.Count; i++)
                {
                    CheckBox chkRow = lvStudents.Items[i].FindControl("chkRow") as CheckBox;
                    HiddenField hdnmember = lvStudents.Items[i].FindControl("hdnmember") as HiddenField;
                    DropDownList ddl = lvStudents.Items[i].FindControl("ddldesig") as DropDownList;

                    DataSet ds = objCommon.FillDropDown("Tbl_MM_MEETINGPRESENTY", "FK_MEMBER", "FK_COMMITEEDESIG", "MEETINGCODE='" + ddlMeeting.SelectedItem.Text+ "' and FK_COMMITEE = '" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "");

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                if (Convert.ToInt32(ds.Tables[0].Rows[j]["FK_MEMBER"].ToString()) == Convert.ToInt32(hdnmember.Value))
                                {
                                    chkRow.Checked = true;
                                    ddl.SelectedValue = ds.Tables[0].Rows[j]["FK_COMMITEEDESIG"].ToString();
                                }
                            }
                        }
                    }
                }
            }      
        }
        else
        {
            lvAgenda.DataSource = null;
            lvAgenda.DataBind();
            lvAgenda.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlMeeting.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Meeting.');", true);
        }

        //if (lblagtital.Text == string.Empty)
        //{            
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Agenda.');", true);
        //}       

        //if (txtAgendaDetails.Text == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Meeting Details Against Selected Agenda.');", true);
        //}
        else
        {
            string filename = "";
            string path = "";
            string file = "";
            if (FileUpload1.HasFile)
            {
                filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\MEETING\\" + ddlCommitee.SelectedItem.Text + "\\" + ddlMeeting.SelectedItem.Text + "\\" + txtAgendaNo.Text; ;// +agendano + "_" + fupath.FileName;
                if (!System.IO.Directory.Exists(file))
                {
                    System.IO.Directory.CreateDirectory(file);
                    file = file + "//" + txtAgendaNo.Text + "_" + filename;
                    FileUpload1.PostedFile.SaveAs(file);
                }
            }
            else
            {
            }
            objmas.FK_AGENDA = PK_AGENDA;
            objmas.FK_Committe = Convert.ToInt32(ddlCommitee.SelectedValue);
            if (chklock.Checked)
            {
                objmas.LOCK_MEET = 'Y';
            }
            else
            {
                objmas.LOCK_MEET = 'N';
            }

            objmas.AGENDADETAILS = txtAgendaDetails.Text;
            objmas.DisplayFileName = filename;
            objmas.Filepath = file.Replace(@"\", "/");
            objmas.METTINGCODE = ddlMeeting.SelectedItem.Text;
            objmas.PK_MEETINGDETAILS = 0;

            //Shaikh Juned 27-06-2022--start
            DataSet ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS", "FK_Committe", "PK_MEETINGDETAILS", "METTINGCODE='" + ddlMeeting.SelectedItem.Text + "' and FK_Committe = '" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", ""); 
            if (ds.Tables[0].Rows.Count > 0)
            {
                Flag = "UPDATE";
            }                                               //Shaikh Juned 27-06-2022--end



            if (Flag == "ADD")
            {
                objmas.QTYPE = 1;
            }
            if (Flag == "UPDATE")
            {
                objmas.QTYPE = 2;
                if (filename == "")
                {
                    objmas.DisplayFileName = PRE_FILENAME;
                }
                if (file == "")
                {
                    objmas.Filepath = file.Replace(@"\", "/");
                }
            }
            int result = 0;
            result = objcon.AddUpdate_Meeting_Details(objmas);
            if (result > 0)
            {

                //sHAIKH JUNED 27-06-2022---START

                //int DELRESULT = objCommon.DeleteRow("Tbl_MM_MEETINGPRESENTY", "FK_COMMITEE ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and MEETINGCODE='" + ddlMeeting.SelectedItem.Text + "'");
                //if (DELRESULT > 0)
                //{



                //if (lvStudents.Items.Count > 0)
                //{
                //    for (int i = 0; i < lvStudents.Items.Count; i++)
                //    {
                //        CheckBox chkRow = lvStudents.Items[i].FindControl("chkRow") as CheckBox;
                //        HiddenField hdnmember = lvStudents.Items[i].FindControl("hdnmember") as HiddenField;
                //        DropDownList ddl = lvStudents.Items[i].FindControl("ddldesig") as DropDownList;
                //        //if (ddl.SelectedIndex == 0)
                //        //{
                //        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Designation.');", true);
                //        //}

                //        if (ddl.SelectedValue == "0")
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Designation.');", true);
                //        }

                //       else if (chkRow.Checked)
                //        {
                //            objmas.PK_MEETINGDETAILS = 0;
                //            objmas.FKMEMBER = Convert.ToInt32(hdnmember.Value);
                //            objmas.METTINGCODE = ddlMeeting.SelectedItem.Text;
                //            objmas.FK_Committe = Convert.ToInt32(ddlCommitee.SelectedValue);
                //            objmas.DESIGID = Convert.ToInt32(ddl.SelectedValue);
                //            objcon.INSERT_MEETINGPRESENTY(objmas);
                //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Save Successfully.');", true);

                //            ClearForm();   //22/03/2022
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Atleast one Member.');", true);
                //        }

                //sHAIKH JUNED 27-06-2022---END


                //------------------------------SHAIKH JUNED   27-06-22----START---

                CustomStatus cs = new CustomStatus();
                string IdNo = string.Empty;
                foreach (ListViewDataItem dti in lvStudents.Items)
                {
                    CheckBox chkRow = dti.FindControl("chkRow") as CheckBox;
                    HiddenField hdnmember = dti.FindControl("hdnmember") as HiddenField;
                    DropDownList ddl = dti.FindControl("ddldesig") as DropDownList;

                    if (chkRow.Checked)
                    {
                        if (ddl.SelectedValue == "0")
                        {
                            objCommon.DisplayMessage(this.Page, "Please Select Designation.", this.Page);
                            return;
                        }

                        if (IdNo.Equals(string.Empty))
                        {
                            IdNo = chkRow.ToolTip;
                        }
                        else
                        {
                            IdNo = IdNo + "," + chkRow.ToolTip;
                        }
                    }
                }

                if (IdNo.Equals(string.Empty))
                {

                    objCommon.DisplayMessage(this.Page, "Please Select Member.", this.Page); //18/11/2021
                    return;
                }
                int DELRESULT = objCommon.DeleteRow("Tbl_MM_MEETINGPRESENTY", "FK_COMMITEE ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and MEETINGCODE='" + ddlMeeting.SelectedItem.Text + "'");
                if (DELRESULT > 0)
                {
                    if (lvStudents.Items.Count > 0)
                    {
                        for (int i = 0; i < lvStudents.Items.Count; i++)
                        {
                            CheckBox chkRow = lvStudents.Items[i].FindControl("chkRow") as CheckBox;
                            HiddenField hdnmember = lvStudents.Items[i].FindControl("hdnmember") as HiddenField;
                            DropDownList ddl = lvStudents.Items[i].FindControl("ddldesig") as DropDownList;
                            objMM.MEMBER_NO = Convert.ToInt32(hdnmember.Value);
                            if (chkRow.Checked == true)
                            {
                                int j = Convert.ToInt32(chkRow.ToolTip);
                                j = j - 1;
                                objmas.PK_MEETINGDETAILS = 0;
                                objmas.FKMEMBER = Convert.ToInt32(hdnmember.Value);
                                objmas.METTINGCODE = ddlMeeting.SelectedItem.Text;
                                objmas.FK_Committe = Convert.ToInt32(ddlCommitee.SelectedValue);
                                objmas.DESIGID = Convert.ToInt32(ddl.SelectedValue);
                                objcon.INSERT_MEETINGPRESENTY(objmas);
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Save Successfully.');", true);


                                //---------------------SHAIKH JUNED 27-06-22------END-----------
                            }
                        }
                    }


                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Update Successfully.');", true); //shaikh juned (30/03/2022)

                }
                ClearForm();
                Flag = "ADD";

            }
        }
    }
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("pdf", "meetingDetails.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MeetingMinutes.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string exporttype, string rptFileName)
    {

        if (ddlCommitee.SelectedValue == "0" || ddlMeeting.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(this.Page, "Please Select  Committee And Meeting Options.", this.Page);
            return;
        }       
        
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));          
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=MeetingDetails" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_MEETINGCODE=" + ddlMeeting.SelectedItem.Text +",@P_COMMITTEE="+ ddlCommitee.SelectedValue ; 

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MeetingMinutes.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Flag = "ADD";
        ClearForm();
    }

    //ChecK for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".zip", ".ZIP", ".txt", ".TXT", ".docx", ".DOCX", ".XLSX", ".xlsx" };
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
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    
                    if (ddlMeeting.SelectedIndex > 0)
                      {
                                //string file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text ;// +agendano + "_" + fupath.FileName;
                                string file = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + txtAgendaNo.Text;
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
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Meeting Code.\\n Please Select Meeting.'  );  ", true);
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
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MeetingMinutes.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewFiles(string PATH)
    {
        try
        {
            //pnlFile.Visible = true;
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
                lvfile.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MeetingMinutes.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
 
    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + txtAgendaNo.Text;
        ImageButton btn = sender as ImageButton;
        DownloadFile(path + "\\"+ btn.AlternateText);
    }

    // This method is used to download.
    public void DownloadFile(string filePath)
    {
        try
        {
           
            FileStream sourceFile = new FileStream(filePath, FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            Response.AddHeader("content-disposition", "attachment; filename=" +  Path.GetFileName(filePath));           
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


    // This method is used to get the name of file.
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
            path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + txtAgendaNo.Text;
            //CHECKING FILE EXISTS OR NOT
            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
                BindListViewFiles(path);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Meeting_Management_Transaction_MeetingMinutes.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{

        //    if (rdbCommitteeType.SelectedValue == "U")
        //    {
        //        trCollegeName.Visible = false;
        //        ddlCollege.SelectedIndex = 0;
        //        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
        //    }
        //    else
        //    {
        //        trCollegeName.Visible = true;
        //        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
        //    }

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
       // objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");             
    }


    // This button is use to send MOM Draft to committee members
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            if (lvAgenda.Items.Count < 0)
            {
                objCommon.DisplayMessage("Meeting Agendas Are Not Lock.", this.Page);
                return;
            }            
            else
            {
                if (ddlCommitee.SelectedIndex > 0 && ddlMeeting.SelectedIndex > 0) //ddlpremeeting.SelectedIndex > 0)
                {
                    //============== SEND EMAIL WITH AGENDA FOR APPROVAL ==============//

                    string fromEmailId = string.Empty;
                    string fromEmailPwd = string.Empty;
                    string body = string.Empty;


                    DataSet ds = objcon.GetFromDataForEmail(Convert.ToInt32(ddlCommitee.SelectedValue));
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

                        sendmail("recruitment@iitms.co.in", "IITMS@123", receiver, "Draft of Meeting Agendas.", "Dear Members", userid);
                    }                   
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Committee & Meeting.", this.Page);
                }
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


    // This method is used to send Emails one by one.
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    {
        try
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            // MailMessage mailMessage = new MailMessage();

            mailMessage.Subject = Sub;
            string AgendaItemList = string.Empty;
            string AgendaDetails = string.Empty;
            string AgendaContentList = string.Empty;

            DataSet dsCommittee = null;
            dsCommittee = objCommon.FillDropDown("Tbl_MM_COMITEE", "ID, code", "NAME", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA", " distinct MEETING_CODE, AGENDANO", "MEETINGDATE, MEETINGTIME, VENUE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlMeeting.SelectedItem.Text + "'", "");


            string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\SENDEMAIL" + ddlCommitee.SelectedItem.Text + "\\" + ddlMeeting.SelectedItem.Text;

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
                //MemberEmailId = objCommon.LookUp("TBL_MM_MENBERDETAILS MD INNER JOIN TBL_MM_RELETIONMASTER RM ON (MD.PK_CMEMBER = RM.FK_MEMBER)", "ISNULL(P_EMAIL,' ') AS P_EMAIL", "P_EMAIL <> '' AND RM.USERID=" + idno[j] + " AND RM.FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue)); // multipleIdno);
                MemberEmailId = objCommon.LookUp("TBL_MM_MENBERDETAILS MD INNER JOIN TBL_MM_RELETIONMASTER RM ON (MD.PK_CMEMBER = RM.FK_MEMBER)", "ISNULL(T_EMAIL,' ') AS T_EMAIL", "RM.USERID=" + idno[j] + " AND RM.FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue));
              
                mailMessage.To.Add(MemberEmailId); 

                var MailBody = new StringBuilder();
                MailBody.AppendFormat("Dear Member, {0}\n", "");                              
                MailBody.AppendLine(@"<br />Draft of minutes of following meeting is created. It is also available at your login for any remark.");               
                MailBody.AppendLine(@"<br />Committee   : " + dsCommittee.Tables[0].Rows[0]["NAME"]);
                MailBody.AppendLine(@"<br />Meeting Code : " + ds.Tables[0].Rows[0]["MEETING_CODE"]); 
                MailBody.AppendLine(@"<br />-----------------------------------------------------------");


                DataSet dsAgendaList = objCommon.FillDropDown("TBL_MM_AGENDA A INNER JOIN TBL_MM_MEETINGDETAILS M ON (A.PK_AGENDA = M.FK_AGENDA)", "A.MEETING_CODE, A.AGENDANO", "ROW_NUMBER() OVER(ORDER BY A.PK_AGENDA ASC) AS SrNo, A.AGENDATITAL, M.AGENDADETAILS, PK_AGENDA", "A.FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND A.MEETING_CODE='" + ddlMeeting.SelectedItem.Text + "'", "");
               
                if (dsAgendaList.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsAgendaList.Tables[0].Rows.Count; i++)
                    {
                        DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA_CONTENT_DETAILS ACD ON (AC.ACID = ACD.ACID)", "ACD.SRNO", "ACD.CONTENT_DETAILS", "AC.AGENDA_ID =" + dsAgendaList.Tables[0].Rows[i]["PK_AGENDA"].ToString(), "");
                        if (AgendaItemList == string.Empty)
                        {                           
                            MailBody.AppendLine(@"<br /><b>Agenda Item:</b> " + dsAgendaList.Tables[0].Rows[i]["SrNo"].ToString() + ") " + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString());

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

                                MailBody.AppendLine(@"<br /> " + AgendaContentList);
                                MailBody.AppendLine(@"<br /><b>Meeting Details :</b>" + dsAgendaList.Tables[0].Rows[i]["AGENDADETAILS"].ToString());
                                MailBody.AppendLine(@"<br />-----------------------------------------------------------");
                              
                                AgendaContentList = string.Empty;                               
                            }
                        }
                        else
                        {
                            // AgendaItemList = AgendaItemList + "<br />" + dsAgendaList.Tables[0].Rows[i]["AGENDATITAL"].ToString();
                        }
                    }

                }


                //BodyOfMail.AppendLine(@"For Confirmation of Receive this mail");
                //BodyOfMail.AppendLine("<html><body><h1>My Message</h1><br><a href=http://www.stackoverflow.com>stackoverflow</a></body></html>");
                //BodyOfMail.AppendLine("<html><body><h1>For Confirmation of Receive this mail</h1><br><a href=http://localhost:6587/PresentationLayer/MEETING_MANAGEMENT/EmailReceived.aspx?receivedType=M&uid=" + multipleIdno + ">Click Here</a></body></html>");


                MailBody.AppendLine("<html><body><h2>For Confirmation of Receive this mail</h2><br><a href=http://indusuni.mastersofterp.in/MEETING_MANAGEMENT/EmailReceived.aspx?receivedType=M&uid=" + idno[j] + "&CommitteeId=" + dsCommittee.Tables[0].Rows[0]["ID"].ToString() + ">Click Here</a></body></html>");
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
                objCommon.DisplayMessage(this.Page, "Mail Sent Successfully.", this.Page);    
                mailMessage.To.Clear();
            }
            mailMessage.Attachments.Dispose();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void chkRow_CheckedChanged(object sender, EventArgs e)  //sHAIKH JUNED 27-06-2022  --START
    {
        if (lvStudents.Items.Count > 0)
        {
            for (int i = 0; i < lvStudents.Items.Count; i++)
            {
                CheckBox chkRow = lvStudents.Items[i].FindControl("chkRow") as CheckBox;
                if (chkRow.Checked == false)
                {
                    //TextBox txtstartdate = lvStudents.Items[i].FindControl("txtstartdate") as TextBox;//start date
                    //TextBox txtenddate = lvStudents.Items[i].FindControl("txtenddate") as TextBox;//txtenddate 
                    //DropDownList ddlDesignation = lvStudents.Items[i].FindControl("ddlDesignation") as DropDownList;//txtenddate 

                    // CheckBox chkRow1 = lvStudents.Items[i].FindControl("chkRow") as CheckBox;
                    HiddenField hdnmember = lvStudents.Items[i].FindControl("hdnmember") as HiddenField;
                    DropDownList ddl = lvStudents.Items[i].FindControl("ddldesig") as DropDownList;

                    // chkRow.Text = string.Empty;
                    //  txtstartdate.Text = string.Empty;
                    // ddldesig.selectedvalue= 0;
                    ddl.SelectedIndex = 0;
                }
            }
        }
    }       //sHAIKH JUNED 27-06-2022  --END
}
