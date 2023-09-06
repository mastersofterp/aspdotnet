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
using System.Configuration;
using System.IO;

public partial class MEETING_MANAGEMENT_Transaction_MM_Search : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();

    public static int pk_agenda_id;
    public static string PRE_FILEPATH = "";
    public static string path = "";
    public static string PRE_FILENAME = "";
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                //if (Session["userno"] == null || Session["username"] == null ||
                //    Session["usertype"] == null || Session["userfullname"] == null)                
                if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
                    
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
                  // objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE C INNER JOIN TBL_MM_MEETINGDETAILS M ON (C.ID = M.FK_Committe)", "C.ID", "C.NAME", "C.STATUS=0 AND M.LOCK_MEET ='Y'", "C.NAME");
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                    else
                        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");
                }
            }
            else
            {           
                string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\" + ViewState["agenda_no"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Search.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
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




    protected int checkcommitteemembers(int userid)
    {
        int result = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "count(*)AS RESULT", "IDNO=" + userid));
        return result;
    }

    // This method is used to check the page authorization.
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
    // This method is used to fill committee list.
   

    

    // This method is used to download the file.
    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + ViewState["agenda_no"].ToString();   
      //  string path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + txtAgendaNo.Text;
        ImageButton btn = sender as ImageButton;
        DownloadFile(path + "\\" + btn.AlternateText);
        //ImageButton btn = sender as ImageButton;
        
        //DownloadFile(path + "\\" + btn.AlternateText);



        //ImageButton btn = sender as ImageButton;
       // DownloadFile(btn.AlternateText);
    }


    //-----------------------------16/03/2022---------------------------
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
            Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileName(filePath));

             
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
//-------------------------16/03/2022--------------------------
    // This method is used to download the file.
   // public void DownloadFile(string filePath)
   // {
   //     try
   //     {   
   //         path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + txtcode.Text + "\\MEETING\\" + ViewState["agenda_no"].ToString();
   //         FileStream sourceFile = new FileStream((path + "\\" + filePath), FileMode.Open);
   //         long fileSize = sourceFile.Length;
   //         byte[] getContent = new byte[(int)fileSize];
   //         sourceFile.Read(getContent, 0, (int)sourceFile.Length);
   //         sourceFile.Close();

   //         Response.Clear();
   //         Response.BinaryWrite(getContent);
   //         Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
   //         Response.AddHeader("content-disposition", "attachment; filename=" + filePath);
   //     }
   //     catch (Exception ex)
   //     {
   //         Response.Clear();
   //         Response.ContentType = "text/html";
   //         Response.Write("Unable to download the attachment.");
   //     }
   // }
   // private string GetResponseType(string fileExtension)
   // {
   //     switch (fileExtension.ToLower())
   //     {
   //         case ".doc":
   //             return "application/vnd.ms-word";
   //             break;

   //         case ".docx":
   //             return "application/vnd.ms-word";
   //             break;

   //         case ".xls":
   //             return "application/ms-excel";
   //             break;

   //         case ".xlsx":
   //             return "application/ms-excel";
   //             break;

   //         case ".pdf":
   //             return "application/pdf";
   //             break;

   //         case ".ppt":
   //             return "application/vnd.ms-powerpoint";
   //             break;

   //         case ".txt":
   //             return "text/plain";
   //             break;

   //         case "":
   //             return "";
   //             break;

   //         default:
   //             return "";
   //             break;
   //     }
   // }
   //// This method is used to get the file name.
   // protected string GetFileName(object obj)
   // {
   //     string f_name = string.Empty;
   //     f_name = obj.ToString();
   //     return f_name;
   // }
//-------------------------16/03/2022-------------------------------------


    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            path = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + ViewState["agenda_no"].ToString();
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
                objCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Search.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {          
            pnlinfo.Visible = true;
            pnlFile.Visible = true;
            lvfile.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            ViewState["PK_MEETINGDETAILS"] = int.Parse(btnEdit.CommandArgument);
            int pk_Agenda_id = int.Parse(btnEdit.CommandName);          
            int pk_meeting_id = int.Parse(btnEdit.CommandArgument);

            DataSet dsInfo = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(AG.PK_AGENDA=MM.FK_AGENDA) INNER JOIN TBL_MM_VENUE V ON (AG.VENUEID = V.PK_VENUEID)", "PK_MEETINGDETAILS,AGENDADETAILS,Ag.FILEPATH, Ag.FILE_NAME,Ag.AGENDANO,CONVERT(VARCHAR, AG.MEETINGDATE ,103)MEETINGDATE", "Ag.MEETING_CODE ,PK_AGENDA, ag.MEETINGTIME, AGENDATITAL, V.VENUE, CONVERT(varchar(11), ag.MEETINGDATE,103)MEETINGDATE, ag.MEETING_CODE", "mm.FK_AGENDA=" + pk_Agenda_id + "", "");            
            if (dsInfo.Tables[0].Rows.Count > 0)
            {               
                foreach (DataRow dr in dsInfo.Tables[0].Rows)
                {
                    txttitle.Text = Convert.ToString(dr["AGENDATITAL"]);
                    txtvenue.Text = Convert.ToString(dr["VENUE"]);
                    txtcode.Text = Convert.ToString(dr["MEETING_CODE"]);
                    txtdetail.Text = Convert.ToString(dr["AGENDADETAILS"]);
                    txtMeetingDate.Text = Convert.ToString(dr["MEETINGDATE"]);
                    pk_agenda_id = Convert.ToInt32(dr["PK_AGENDA"]);
                    PRE_FILENAME = dr["FILE_NAME"].ToString();
                    PRE_FILEPATH = dr["FILEPATH"].ToString();
                    ViewState["agenda_no"] = Convert.ToString(dr["AGENDANO"]);  
                }
                RETPATH = Docpath + "MEETING_MANAGEMENT\\UPLOAD\\" + ddlMeeting.SelectedItem.Text + "\\MEETING\\" + ViewState["agenda_no"].ToString();
                BindListViewFiles(RETPATH);
            }  
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Search.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to bind the list of files.
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
                    pnlFile.Visible = true;
                    pnlList.Visible=true;
                   // lvfile.Visible = true;
                }
                else
                {
                    lvfile.DataSource = null;
                    lvfile.DataBind();
                    pnlFile.Visible = false;
                }
            }
            else
            {
                lvfile.DataSource = null;
                lvfile.DataBind();
                pnlFile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Search.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This method is used to clear the controls.
    public void Clear()
    {
        pnlFile.Visible = false;
        pnlinfo.Visible = false;
        pnlMeetingInfo.Visible = false;        
    }


    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    { 
        objCommon.FillDropDownList(ddlMeeting, "TBL_MM_AGENDA", "DISTINCT MEETING_CODE", "MEETING_CODE", "FK_MEETING=" + ddlCommitee.SelectedValue, "");
        Clear();      
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {             
        BindListView();       
    }

    //This method is  used to bind the list.
    private void BindListView()
    {
        try
        {
            //DateTime startdt = Convert.ToDateTime(txtstartdate.Text);
            //DateTime enddt = Convert.ToDateTime(txtenddate.Text);
            //objMM.STARTDATE = txtstartdate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtstartdate.Text);
            //objMM.ENDDATE = txtenddate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtenddate.Text);           
            //DataSet ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS", "Ag.AGENDANO,PK_AGENDA", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'  and mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
            DataSet ds = null;
            //DateTime startdt = Convert.ToDateTime(txtstartdate.Text);
            //DateTime enddt = Convert.ToDateTime(txtenddate.Text);

            objMM.STARTDATE = txtstartdate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtstartdate.Text);
            objMM.ENDDATE = txtenddate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtenddate.Text);

            if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
            {
                if (ddlCommitee.SelectedValue != "0" && ddlMeeting.SelectedValue != "0")
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'  and mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
                }
                else if (ddlCommitee.SelectedValue != "0" && ddlMeeting.SelectedValue == "0")
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'  and mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' ", "PK_MEETINGDETAILS");

                }
                else if (ddlCommitee.SelectedValue == "0" && ddlMeeting.SelectedValue != "0")
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'  mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
                }
                else
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "' ", "PK_MEETINGDETAILS");
                    // ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM iNNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
                    //ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL, AG.MEETING_CODE", "mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "PK_MEETINGDETAILS");
                }
            }
            else
            {
                if (ddlCommitee.SelectedValue != "0" && ddlMeeting.SelectedValue != "0")
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
                    //ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
                }
                else if (ddlCommitee.SelectedValue != "0" && ddlMeeting.SelectedValue == "0")
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' ", "PK_MEETINGDETAILS");

                }
                else if (ddlCommitee.SelectedValue == "0" && ddlMeeting.SelectedValue != "0")
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
                }
                else
                {
                    ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "", "PK_MEETINGDETAILS");
                }
            }
           
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlMeetingInfo.Visible = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lvAgenda.DataSource = ds;
                    lvAgenda.DataBind();
                }
            }
            else
            {
                pnlMeetingInfo.Visible = false;
                lvAgenda.DataSource = null;
                lvAgenda.DataBind();
                objCommon.DisplayMessage("Record Not Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Transaction_MM_Search.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
        pnlinfo.Visible = false;
        txtstartdate.Text = string.Empty;
        txtenddate.Text = string.Empty;
        pnlFile.Visible = false;      
        pnlMeetingInfo.Visible = false;
        ddlCommitee.SelectedIndex = 0;
        ddlMeeting.Items.Clear();
        ddlMeeting.DataSource = null;
        ddlMeeting.DataBind();
        //ddlCommitee.SelectedValue = "";
   
    }

    protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
        pnlinfo.Visible = false;        
        pnlFile.Visible = false;      
        pnlMeetingInfo.Visible = false;
        
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
    }

    protected void txtstartdate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
        {
            DateTime startdt = Convert.ToDateTime(txtstartdate.Text);
            DateTime enddt = Convert.ToDateTime(txtenddate.Text);
            objMM.STARTDATE = txtstartdate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtstartdate.Text);
            objMM.ENDDATE = txtenddate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtenddate.Text);
            ds = objCommon.FillDropDown("TBL_MM_COMITEE c inner join TBL_MM_AGENDA a on (C.ID=A.FK_MEETING)", "ID", "NAME", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'", "PK_MEETINGDETAILS");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCommitee.DataSource = ds.Tables[0];
                ddlCommitee.DataTextField = ds.Tables[0].Columns["NAME"].ToString();
                ddlCommitee.DataValueField = ds.Tables[0].Columns["ID"].ToString();
                ddlCommitee.DataBind();

            }
            //ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'  and mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
        }
    }
    protected void txtenddate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
        {
            DateTime startdt = Convert.ToDateTime(txtstartdate.Text);
            DateTime enddt = Convert.ToDateTime(txtenddate.Text);
            objMM.STARTDATE = txtstartdate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtstartdate.Text);
            objMM.ENDDATE = txtenddate.Text.ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtenddate.Text);
            ds = objCommon.FillDropDown("TBL_MM_COMITEE c inner join TBL_MM_AGENDA a on (C.ID=A.FK_MEETING)", "DISTINCT (NAME)", "ID", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'", "");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCommitee.DataSource = ds.Tables[0];
                ddlCommitee.DataTextField = ds.Tables[0].Columns["NAME"].ToString();
                ddlCommitee.DataValueField = ds.Tables[0].Columns["ID"].ToString();
                ddlCommitee.DataBind();

            }
            //ds = objCommon.FillDropDown("TBL_MM_MEETINGDETAILS MM INNER JOIN TBL_MM_AGENDA AG on(Ag.PK_AGENDA=mm.FK_AGENDA)", "PK_MEETINGDETAILS,AGENDADETAILS,MEETING_CODE,AGENDATITAL", "Ag.AGENDANO,PK_AGENDA, AG.AGENDATITAL", "MEETINGDATE between '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyy-MMM-dd") + "'  and '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MMM-dd") + "'  and mm.FK_Committe='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' and mm.METTINGCODE='" + ddlMeeting.SelectedItem.Text + "'", "PK_MEETINGDETAILS");
        }
    }
}
