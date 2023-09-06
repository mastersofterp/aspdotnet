//======================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 15-DEC-2014
//DESCRIPTION   : THIS FORM IS USED FOR SPORT PARTICIPATION ENTRY. 
//MODIFY DATE   : 04-MAY-2017   
//======================================================================
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
using System.Text;

public partial class Sports_Transaction_SportParticipation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport = new Sport();
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
                    //objCommon.FillDropDownList(ddlStaff, "SPRT_STAFF_MASTER", "POSTID", "POSTNAME", "", "POSTNAME");
                    objCommon.FillDropDownList(ddlEventType, "SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "", "EVENT_TYPE_NAME");
                    objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "", "EVENTNAME");
                    BindlistView();
                }     
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID = " + Convert.ToInt32(ddlEventType.SelectedValue), "EVENTNAME");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objSport.NITSTATUS = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
            objSport.TEAMID = Convert.ToInt32(ddlTeam.SelectedValue.ToString());
            objSport.EVENTID = Convert.ToInt32(ddlEvent.SelectedValue.ToString());
            objSport.SPID = Convert.ToInt32(ddlSportName.SelectedValue.ToString());
            objSport.MEDALID = Convert.ToInt32(ddlResult.SelectedValue.ToString());
            //objSport.POSTID = Convert.ToInt32(ddlStaff.SelectedValue.ToString());
            objSport.DESCRIPTION = txtDescription.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDescription.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);
            objSport.FILENAME = Convert.ToString(ViewState["FILENAME"]);
            objSport.FILEPATH =  Convert.ToString(ViewState["FILEPATH"]);  
           
            if (ViewState["PARTICID"] == null)
            {

                objSport.PARTICID = 0;
                CustomStatus cs = (CustomStatus)objSportC.AddUpdate_Sport_Participation(objSport);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                pnlFile.Visible = false;
                BindlistView();
                Clear();
            }
            else
            {
                objSport.PARTICID = Convert.ToInt32(ViewState["PARTICID"].ToString());
                objSportC.AddUpdate_Sport_Participation(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                BindlistView();
                Clear();
            }    
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        BindlistView(); 
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int particid = int.Parse(btnEdit.CommandArgument);
            ViewState["PARTICID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(particid);           
            pnlFile.Visible = true;           
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SPORT_PARTICIPATION SP INNER JOIN SPRT_TEAM_MASTER TM ON(SP.TEAMID = TM.TEAMID) INNER JOIN SPRT_SPORT_MASTER SM ON(SM.SPID = SP.SPID) INNER JOIN SPRT_EVENT_MASTER EM ON (SP.EVENTID = EM.EVENTID) INNER JOIN SPRT_MEDAL_MASTER MM ON(MM.MEDALID = SP.MEDALID)", "TM.TEAMNAME, SM.SNAME, EM.EVENTNAME, MM.MEDALNAME AS RESULT", "SP.PARTICID, (CASE SP.NITSTATUS WHEN 1 THEN 'INTER' ELSE 'INTRA' END) AS NITSTATUS", "(SP.EVENTID =" + Convert.ToInt32(ddlEvent.SelectedValue) + " OR " + Convert.ToInt32(ddlEvent.SelectedValue) + "=0)" , "PARTICID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSportParticipation.DataSource = ds;
                lvSportParticipation.DataBind();
            }
            else
            {
                lvSportParticipation.DataSource = null;
                lvSportParticipation.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void ShowDetails(int particid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("[SPRT_SPORT_PARTICIPATION] SP INNER JOIN SPRT_EVENT_MASTER EM ON (SP.EVENTID = EM.EVENTID)", "SP.PARTICID, SP.NITSTATUS, SP.TEAMID, SP.EVENTID, SP.SPID, SP.MEDALID", "SP.[DESCRIPTION], SP.FILEPATH , SP.USERID, SP.[FILENAME], EM.ETID", "PARTICID=" + particid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["NITSTATUS"].ToString();
                objCommon.FillDropDownList(ddlEventType, "SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "", "EVENT_TYPE_NAME");
                ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["ETID"].ToString();
                objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "ETID =" + Convert.ToInt32(ddlEventType.SelectedValue), "EVENTNAME");
                ddlEvent.SelectedValue = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER SM INNER JOIN SPRT_EVENT_DETAILS ED ON (SM.SPID = ED.SPID)", "ED.SPID", "SM.SNAME", "EVENTID=" + ddlEvent.SelectedValue + "", "ED.SPID");
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();
              //  objCommon.FillDropDownList(ddlTeam, "SPRT_SPORT_MASTER SM INNER JOIN SPRT_TEAM_MASTER TM ON (SM.SPID = TM.SPID)", "TM.TEAMID", "TM.TEAMNAME", "TM.SPID=" + ddlSportName.SelectedValue + "", "TM.SPID");
                
                //objCommon.FillDropDownList(ddlResult, "SPRT_MEDAL_MASTER MM INNER JOIN SPRT_SPORT_MASTER SM ON (MM.TYPID = SM.TYPID)", "MEDALID", "MEDALNAME", "SPID=" + ddlSportName.SelectedValue + "", "MEDALID DESC");
                objCommon.FillDropDownList(ddlTeam, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_TEAM_MASTER TM ON(ED.TEAMID = TM.TEAMID) LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "DISTINCT ED.TEAMID", "TM.TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "ED.SPID = " + Convert.ToInt32(ddlSportName.SelectedValue) + " AND EVENTID =" + Convert.ToInt32(ddlEvent.SelectedValue), "TEAMNAME");
                ddlTeam.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
                objCommon.FillDropDownList(ddlResult, "SPRT_MEDAL_MASTER MM INNER JOIN SPRT_SPORT_MASTER SM ON (MM.SPID = SM.SPID) ", "MEDALID", "MEDALNAME", "MM.SPID=" + ddlSportName.SelectedValue + "", "MEDALID DESC");

                ddlResult.SelectedValue = ds.Tables[0].Rows[0]["MEDALID"].ToString();

                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();        
            }
            RETPATH = Docpath + "Sports\\" + ddlEvent.SelectedItem + "\\" + ddlTeam.SelectedItem;
            BindListViewFiles(RETPATH);
            BindPlayerList();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlStatus.SelectedIndex = 0;
        ddlTeam.SelectedIndex = 0;
        ddlEvent.SelectedIndex = 0;
        ddlResult.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        //ddlStaff.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        //lvSportParticipation.DataSource = null;
        //lvSportParticipation.DataBind();
        //lvSportParticipation.Visible = false;
        lvfile.Visible = false;
        lvPlayer.Visible = false;
        ViewState["PARTICID"] = null;
    }
  

    private void PopulateSportName()
    {
        try
        {            
           // objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER SM INNER JOIN SPRT_EVENT_DETAILS ED ON (SM.SPID = ED.SPID) INNER JOIN SPRT_SPORT_TYPE S ON (S.TYPID = SM.TYPID)", "ED.SPID", "S.GAME_TYPE + ' - ' + SM.SNAME AS SNAME, SM.TYPID ", "EVENTID=" + ddlEvent.SelectedValue + "", "ED.SPID");                                                                                                                                             
            objCommon.FillDropDownList(ddlSportName, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_SPORT_MASTER SM ON (ED.SPID = SM.SPID)", "DISTINCT ED.SPID", "SM.SNAME", "ED.EVENTID = " + Convert.ToInt32(ddlEvent.SelectedValue) , ""); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.PopulateSportName()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    //private void PopulateTeamName()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlTeam, "SPRT_SPORT_MASTER SM INNER JOIN SPRT_TEAM_MASTER TM ON (SM.SPID = TM.SPID)", "TM.TEAMID", "TM.TEAMNAME", "TM.SPID=" + ddlSportName.SelectedValue + "", "TM.SPID");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.PopulateSportName()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    // To add files
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //CHECKING FILE EXISTS OR NOT
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (ddlEvent.SelectedIndex > 0 && ddlTeam.SelectedIndex > 0)
                    {
                        string file = Docpath + "Sports\\" + ddlEvent.SelectedItem + "\\" + ddlTeam.SelectedItem;

                       
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }
                        ViewState["FILEPATH"] = file;
                        path = file + "//" + FileUpload1.FileName; ;
                        dbPath = file;
                        string filename = FileUpload1.FileName;
                        ViewState["FILENAME"] = filename;
                        //CHECKING FOLDER EXISTS OR NOT
                        if (!System.IO.Directory.Exists(path))
                        {
                            FileUpload1.PostedFile.SaveAs(path);
                        }
                        BindListViewFiles(file);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Event Name and  Team Name');  ", true);
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
                objCommon.DisplayUserMessage(this.Page, "Please Select File", this.Page);
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //To Check the valid Extensions of the File. 
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
    // To bind the uploaded files
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
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // To get the file name
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

            string path = Docpath + "Sports\\" + ddlEvent.SelectedItem + "\\" + ddlTeam.SelectedItem;

            //CHECKING FILE EXISTS OR NOT
            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                    //<%--Modified by Saahil Trivedi 24-02-2022--%>
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
                BindListViewFiles(path);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Transaction_SportParticipation.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
            path = Docpath + "Sports\\" + ddlEvent.SelectedItem + "\\" + ddlTeam.SelectedItem;
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

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=SportsParticipationDetailsReport.pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_EVENTID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEvent.SelectedIndex > 0)
        {
            PopulateSportName();
            BindlistView();
        }

    }
    protected void ddlSportName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSportName.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlTeam, "SPRT_EVENT_DETAILS ED INNER JOIN SPRT_TEAM_MASTER TM ON(ED.TEAMID = TM.TEAMID) LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "DISTINCT ED.TEAMID", "TM.TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "ED.SPID = " + Convert.ToInt32(ddlSportName.SelectedValue) + " AND EVENTID =" + Convert.ToInt32(ddlEvent.SelectedValue), "TEAMNAME");

            objCommon.FillDropDownList(ddlResult, "SPRT_MEDAL_MASTER MM INNER JOIN SPRT_SPORT_MASTER SM ON (MM.SPID = SM.SPID) ", "MEDALID", "MEDALNAME", "MM.SPID=" + ddlSportName.SelectedValue + "", "MEDALID DESC");
           
        }

    }
    private void BindPlayerList()
    { 
        try
        {
            DataSet ds = null;
           
            //StringBuilder query = new StringBuilder();
            //query.Append("(( SELECT PM.PLAYER_REGNO+' - '+ PM.PLAYERNAME AS PLAYERNAME FROM");
            //query.Append(" SPRT_PLAYER_MASTER PM INNER JOIN SPRT_TEAM_DETAILS TD ON(TD.PLAYERID = PM.PLAYERID)");
            //query.Append(" INNER JOIN SPRT_TEAM_DETAILS TD ON(TD.PLAYERID = PM.PLAYERID)");          
            //query.Append(" TD.TEAMID=" + ddlTeam.SelectedValue);
            //query.Append(") UNION ALL ");
            //query.Append("(SELECT SM.POSTNAME FROM SPRT_STAFF_MASTER SM INNER JOIN SPRT_TEAM_DETAILS TD ON(TD.POSTID = SM.POSTID)");
            //query.Append(" WHERE TD.TEAMID=" + ddlTeam.SelectedValue);
            //query.Append(")) as a ");
            //ds = objCommon.FillDropDown(query.ToString(), "*", "", "","");
          //  ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER PM INNER JOIN SPRT_TEAM_DETAILS TD ON(TD.PLAYERID = PM.PLAYERID)", "PM.PLAYER_REGNO+' - '+ PM.PLAYERNAME AS PLAYERNAME", "", "", "");

            objSport.TEAMID = Convert.ToInt32(ddlTeam.SelectedValue);
            ds = objSportC.GetTeamDetails(objSport);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPlayer.DataSource = ds;
                lvPlayer.DataBind();
                lvPlayer.Visible = true;
            }
            else
            {
                lvPlayer.DataSource = null;
                lvPlayer.DataBind();
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SportParticipation.BindPlayerList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTeam.SelectedIndex > 0)
        {           
            BindPlayerList();            
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Event Wise Report", "cryEventDetailReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Transaction_SportParticipation.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
               
    }
}
