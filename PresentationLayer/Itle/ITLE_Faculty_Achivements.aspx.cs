using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Itle_ITLE_Faculty_Achivements : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IAnnouncementController objAC = new IAnnouncementController();
    IAnnouncement objANC = new IAnnouncement();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];
    decimal File_size;
    string PageId;

    #region Page Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCorseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCorseName.ForeColor = System.Drawing.Color.Green;
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                BindListView();
                //GetStatusOnPageLoad();
            }
        }
        // Used to get maximum size of file attachment
        GetAttachmentSize();

    }

    #endregion

    #region Actions
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        IAnnouncement objAnnounce = new IAnnouncement();
       
        try
        {
            string DOCFOLDER = file_path + "ITLE\\upload_files\\Achievements";

            if (!System.IO.Directory.Exists(DOCFOLDER))
            {
                System.IO.Directory.CreateDirectory(DOCFOLDER);

            }

            string filename = string.Empty;
            objAnnounce.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objAnnounce.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            //objAnnounce.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objAnnounce.COURSENO= Convert.ToInt32(Session["ICourseNo"]);
            //objAnnounce.ATTACHMENT = fuAnnounce.FileName;
            objAnnounce.AWDID = Convert.ToInt32(ddlAchivType.SelectedItem.Value);
            objAnnounce.DESCRIPTION = ftbDescription.Text.Trim();
            objAnnounce.STARTDATE = Convert.ToDateTime(txtAchivDate.Text);

            
            objAnnounce.STATUS = '1';
            objAnnounce.COLLEGE_CODE = Session["colcode"].ToString();

            if (fuAnnounce.HasFile == true)
            {
                string fileName = System.Guid.NewGuid().ToString() + fuAnnounce.FileName.Substring(fuAnnounce.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));

                if (count != 0)
                {

                    if (fuAnnounce.PostedFile.ContentLength < File_size || fuAnnounce.HasFile.ToString() == "")
                    {

                        objAnnounce.ATTACHMENT = fuAnnounce.FileName;


                        //fileUploader.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);

                        return;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("File Format not supprted.", this);
                    return;
                }
            }
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objAnnounce.AN_NO = Convert.ToInt32(ViewState["an_no"]);
                objAnnounce.OLDFILENAME = hdnFile.Value;

                if (hdnFile.Value != "" && hdnFile.Value != null && fuAnnounce.HasFile == false)
                {
                    objAnnounce.ATTACHMENT = hdnFile.Value;
                }
                

                CustomStatus cs = (CustomStatus)objAC.UpdateAchivement(objAnnounce, fuAnnounce);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Achievement Modified Successfully!", this.Page);
                }
                // lblStatus.Text = "Record Modified...... ";
                //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                else
                    if (cs.Equals(CustomStatus.FileExists))
                    {
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                    }
            }
            else
            {
                CustomStatus cs = (CustomStatus)objAC.AddAchivements(objAnnounce, fuAnnounce);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Achievement Saved Successfully!", this.Page);
                }
                else
                    if (cs.Equals(CustomStatus.FileExists))
                    {
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                    }
            }

            ClearControls();
            BindListView();
            //Panel_Confirm.Visible = true;
            //Response.Redirect("ITLE_Faculty_Achivements.aspx");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Faculty_Achivements.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblStatus.Text = string.Empty;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int an_no = int.Parse(btnDel.CommandArgument);

            //IAnnouncementController objAC = new IAnnouncementController();

            CustomStatus cs = (CustomStatus)objAC.DeleteAchivement(Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), an_no);
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayUserMessage(UpdatePanel1,"Record Deleted Successfully...", this.Page);
                //lblStatus.Text = "Record Deleted Successfully...";
            BindListView();
            //Panel_Confirm.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Faculty_Achivements.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int achiev_no = int.Parse(btnEdit.CommandArgument);
            ViewState["an_no"] = achiev_no;

            ShowDetail(achiev_no, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));
            lblStatus.Text = string.Empty;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Faculty_Achivements.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    #endregion

    #region Private Methods
    private void BindListView()
    {
        try
        {
            objCommon.FillDropDownList(ddlAchivType, "ACD_ITLE_AchiveAwrdType", "AWDID", "AWDTYPE", "AWDID>0", "AWDTYPE");
            //IAnnouncementController objAC = new IAnnouncementController();
            DataSet ds = objAC.GetAllAchivementListByUaNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));
            lvAchievements.DataSource = ds;
            lvAchievements.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Faculty_Achivements.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        ViewState["action"] = "add";
        ftbDescription.Text = "&nbsp;";
        txtAchivDate.Text = string.Empty;
        lblPreAttach.Text = string.Empty;
        ddlAchivType.SelectedIndex = 0;
    }

    private void ShowDetail(int an_no, int courseno, int sessionno, int ua_no)
    {
        try
        {
            //IAnnouncementController objAC = new IAnnouncementController();
            ViewState["anno"] = an_no;
            DataTableReader dtr = objAC.GetSingleAchivement(Convert.ToInt32(ViewState["anno"]), courseno, sessionno, ua_no);

            //Show Announcement Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    ddlAchivType.SelectedValue = dtr["AWDID"] == null ? "" : dtr["AWDID"].ToString();
                    ftbDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    txtAchivDate.Text = dtr["ACHIV_DATE"] == null ? "" : dtr["ACHIV_DATE"].ToString();
                    hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    //lblCurrdate.Text = dtr["STARTDATE"] == null ? "" : Convert.ToDateTime(dtr["STARTDATE"].ToString()).ToString("dd-MMM-yyyy");
                    lblPreAttach.Visible = false;
                    lblPreAttach.Text = "";
                    if (dtr["ATTACHMENT"] != null)
                    {
                        lblPreAttach.Visible = true;
                        lblPreAttach.Text = dtr["ATTACHMENT"].ToString();
                    }
                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ITLE_Faculty_Achivements.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //Get Page Id for getting File size from configuration

    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));

            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));

                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);

                }



        }
        catch (Exception ex)
        {

        }

    }

    #endregion

    #region Public Method

    public string GetFileName(object filename, object announcemetnno)
    {
        if (filename != null && filename.ToString() != "")
            //return "~/ITLE/upload_files/Achievements/Achievements_" + Convert.ToInt32(announcemetnno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/upload_files/Achievements/Achievements_" + Convert.ToInt32(announcemetnno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/Achievements/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        DateTime DT = Convert.ToDateTime(status);
        if (DT < DateTime.Today)
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/Achievements/" + "achievements_" + Convert.ToInt32(an_no);

        Response.ContentType = "Text/Doc";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        Response.End();

    }

    #endregion
}
