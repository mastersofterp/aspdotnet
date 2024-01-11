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
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using ClosedXML.Excel;

public partial class ACADEMIC_MentorMentee_Meeting_Schedule : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingScheduleMaster objMM = new MeetingScheduleMaster();
    Schedule_MeetingController OBJmc = new Schedule_MeetingController();

    public static int pk_agenda_id;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 3)
                    objCommon.FillDropDownList(ddlCommitee, "ACD_MEETING_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                else
                    objCommon.FillDropDownList(ddlCommitee, "ACD_MEETING_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");

                objCommon.FillDropDownList(ddlVenue, "ACD_MEETING_VENUE", "PK_VENUEID", "VENUE", "[STATUS] = 1", "VENUE");
              
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

            if (txttime.Text == txtToTime.Text)
            {
                MessageBox("Meeting From Time and To Time Should not be Same");
                return;   //Shaikh Juned (25-06-2022)
            }
          
            if (hfdActive.Value == "true")
                objMM.STATUS = 1;
            else
            {
                objMM.STATUS = 0;
            }
            if (ViewState["action"] != null)
            {
                objMM.UA_Userno = Convert.ToInt32(Session["userno"]);
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
                objMM.AGENDA_CONTAIN = txtAgendaDetails.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtAgendaDetails.Text.Trim());

                // objMM.VENUE = ddlVenue.SelectedValue;///Convert.ToString(txtvenue.Text);
                objMM.USERID = Convert.ToInt32(Session["userno"]);
                //objMM.LAST_DATE = txtLastDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtLastDate.Text.Trim());

                objMM.VENUEID = Convert.ToInt32(ddlVenue.SelectedValue);

                objMM.TABLE_ITEM = 'N';

                //Save Data
                if (ViewState["action"].ToString().Equals("add"))
                {                 
                    objMM.PK_AGENDA_ID = 0;

                    if (OBJmc.AddUpdate_Meeting_Schedule_Details(objMM) != 0)
                    {
                        BindlistView();
                        ViewState["action"] = "add";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        Clear();
                        ddlCommitee.SelectedIndex = 0;
                    }
                }
            
                else
                {
                    objMM.PK_AGENDA_ID = pk_agenda_id;
                    CustomStatus cs = (CustomStatus)OBJmc.AddUpdate_Meeting_Schedule_Details(objMM);
                    if (OBJmc.AddUpdate_Meeting_Schedule_Details(objMM) != 0)
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
            DataSet ds = objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_VENUE V ON(V.PK_VENUEID=A.VENUEID)", "A.PK_AGENDA, A.AGENDANO, A.AGENDATITAL", "A.MEETINGDATE, A.MEETINGTIME,A.MEETINGTOTIME,V.VENUE,A.FILE_NAME,A.CONTENT_DETAILS, A.MEETING_CODE, CASE WHEN A.ACTIVE_STATUS = 1 THEN 'Active' ELSE 'InActive' END AS [ACTIVE_STATUS]", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");

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

            foreach (ListViewDataItem dataitem in lvAgenda.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

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
        txtAgendaDetails.Text = string.Empty;

        //txtLastDate.Text = string.Empty;
        // txtvenue.Text = string.Empty;
        ddlVenue.SelectedIndex = 0;
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
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
        lblfile.Text = string.Empty;
        //txtvenue.Text = string.Empty;
        txtAgendaDetails.Text = string.Empty;
        ddlVenue.SelectedIndex = 0;
        ddlCommitee.SelectedIndex = 0;     
        lvAgenda.DataSource = null;
        lvAgenda.DataBind();
        lvAgenda.DataSource = null; 
        ViewState["FILE"] = null;
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
        DataSet DS_MEETINCODE = objCommon.FillDropDown("ACD_MEETING_SCHEDULE", "distinct MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "");  
        Clear1();
      
        DataSet ds1 = objCommon.FillDropDown("ACD_MEETING_COMITEE", "CODE", "ID", "ID=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");

        if (ds1.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                txtcode.Text = Convert.ToString(dr["CODE"]);
            }
        }
        ds1 = objCommon.FillDropDown("ACD_MEETING_SCHEDULE", "DISTINCT PK_AGENDA ", "MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "");
        if (ds1.Tables[0].Rows.Count >= 0)
        {
            int current_code = Convert.ToInt32(ds1.Tables[0].Rows.Count) + 1;
            txtcode.Text = txtcode.Text + " " + current_code;
        }
        DataSet ds = objCommon.FillDropDown("ACD_MEETING_SCHEDULE", "(COUNT(PK_AGENDA)) AS TOTALCODE ", "(COUNT(PK_AGENDA)+1) AS MAXCODE ", "MEETING_CODE='" + txtcode.Text + "'and FK_MEETING= " + Convert.ToInt32(ddlCommitee.SelectedValue) + " ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                txtnumber.Text = txtcode.Text + "." + Convert.ToString(dr["MAXCODE"]);
            }
        }
        DataSet ds_Details = objCommon.FillDropDown("ACD_MEETING_SCHEDULE", "(COUNT(PK_AGENDA)+1)A1", "CONVERT(CHAR(11), MEETINGDATE, 103) AS MEETINGDATE,MEETINGTOTIME,VENUEID,MEETINGTIME,VENUE,Lock,ADD_LINE,CITY,STATE,COUNTRY,ZIPCODE, LAST_DATE", "FK_MEETING ='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "' AND MEETING_CODE ='" + txtcode.Text + "'GROUP BY MEETINGDATE,MEETINGTIME,VENUE,LOCK,ADD_LINE,CITY,STATE,COUNTRY,ZIPCODE, LAST_DATE,MEETINGTOTIME,VENUEID", "");
        if (ds_Details.Tables.Count > 0)
        {
            if (ds_Details.Tables[0].Rows.Count > 0)
            {

              
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
           
            int PK_AGENDA_ID = Convert.ToInt32(ViewState["PK_AGENDA_ID"].ToString());

            ViewState["action"] = "edit";
            DataSet ds = objCommon.FillDropDown("ACD_MEETING_SCHEDULE", "*", "", "PK_AGENDA=" + pk_agenda_id + " ", "");
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
                    txtAgendaDetails.Text = Convert.ToString(dr["CONTENT_DETAILS"]);
                    lblfile.Text = dr["FILE_NAME"] == null ? "" : dr["FILE_NAME"].ToString();
                    
                    //txtLastDate.Text = Convert.ToString(dr["LAST_DATE"]);


                    if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(false);", true);
                    }
                }
            }
           
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
                string filetext = string.Empty;
                if (ViewState["PK_AGENDA"] == null)
                {

                    int newid = Convert.ToInt32(objCommon.LookUp("ACD_MEETING_SCHEDULE", "ISNULL(MAX(PK_AGENDA),0)", ""));
                    ViewState["PK_AGENDA"] = newid + 1;
                }
                
                byte[] imgData;
                if (fumeeting.HasFiles)
                {
                    
                    if (!fumeeting.PostedFile.ContentLength.Equals(string.Empty) || fumeeting.PostedFile.ContentLength != null)
                    {
                        int fileSize = fumeeting.PostedFile.ContentLength;

                        int KB = fileSize / 1024;
                        if (KB >= 500)
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);

                            return;
                        }

                        string ext = System.IO.Path.GetExtension(fumeeting.FileName).ToLower();
                       
                        if (fumeeting.FileName.ToString().Length > 50)
                        {
                            objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                            return;
                        }

                        imgData = objCommon.GetImageData(fumeeting);
                        filetext = imgData.ToString();
                        string filename_Certificate = Path.GetFileName(fumeeting.PostedFile.FileName);
                                        
                        filetext = (ViewState["PK_AGENDA"]) + "_MM_Schedule_Meetings_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

                        ViewState["FILE_NAME"]=filetext;

                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["PK_AGENDA"]) + "_MM_Schedule_Meetings_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fumeeting, imgData);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "File Uploaded Successfully!", this.Page);
                        }
                    }
                }
                else if (!fumeeting.HasFile && lblfile.Text != string.Empty)
                {
                    filetext = lblfile.Text;
                    
                }
                objMM.FILE_NAME = filetext;

                if(!fumeeting.HasFile)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select File To Upload!", this.Page);
                    return;
                }

            }

            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
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
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommitee, "ACD_MEETING_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
    }
    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
        dsPURPOSE = objCommon.FillDropDown("ACD_MEETING_SCHEDULE", "FK_MEETING,VENUEID,MEETINGTIME", "MEETINGDATE,MEETINGTOTIME", "VENUEID=" + Convert.ToInt32(ddlVenue.SelectedValue) + "AND MEETINGDATE='" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + "'", "FK_MEETING");


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
    #region Plan & Schedule Meetings Details Excel Report
    protected void btnplanScheduleExcelReports_Click(object sender, EventArgs e)
    {
      if(ddlCommitee.SelectedIndex > 0)
        {
            try
            {
                int uano = Convert.ToInt32(Session["userno"].ToString());
                GridView GVStudData = new GridView();
                DataSet ds = OBJmc.GetPlanScheduleMeetingExcelReport(Convert.ToInt32(ddlCommitee.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ds.Tables[0].TableName = "ScheduleMeetings";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                        {
                            //Add System.Data.DataTable as Worksheet.
                            if (dt != null && dt.Rows.Count > 0)
                                wb.Worksheets.Add(dt);
                        }

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Plan_Schedule_Meeting_Excel_Report.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Record Not Found", this.Page);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select Committee", this.Page);
            lvAgenda.DataSource = null;
            lvAgenda.DataBind();
        }
       
    }
    #endregion
    #region BlogStorage
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }


    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    #endregion BlogStorage
    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        ////Added By Prafull
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img != null || img != "")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";

                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                //BindListViewNotice();
                BindlistView();


            }

        }
        catch (Exception ex)
        {

        }

    }
    
}

