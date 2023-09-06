using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

public partial class Itle_StudAnnouncement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                pnlAnnounce.Visible = true;
                divAnnounce.Visible = false;

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lblLastdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblCourseName.Text = Session["ICourseName"].ToString();
                BindListView();
                // Used to insert id,date and courseno in Log_History table
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
            }
        }
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_StudAnnouncement");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_StudAnnouncement");
        }
    }

    private void BindListView()
    {
        try
        {
            IAnnouncementController objAC = new IAnnouncementController();
            IAnnouncement objAnnounce = new IAnnouncement();

            objAnnounce.SESSIONNO = Convert.ToInt32(Session["SessionNo"]);
            objAnnounce.COURSENO = Convert.ToInt32(Session["ICourseno"]);

            DataSet ds = objAC.GetAnnouncementByCourseNo(objAnnounce);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAnnouncement.DataSource = ds;
                lvAnnouncement.DataBind();
                DivAnnouncementList.Visible = true;
            }
            else
            {
                lvAnnouncement.DataSource = null;
                lvAnnouncement.DataBind();
                DivAnnouncementList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_StudAnnouncement.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int an_no, int courseno, int sessionno)
    {
        try
        {
            IAnnouncementController objAC = new IAnnouncementController();
            ViewState["anno"] = an_no;
            DataTableReader dtr = objAC.GetSingleAnnouncement(Convert.ToInt32(ViewState["anno"]), courseno, sessionno, 0);

            //Show Announcement Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    tdSubject.InnerText = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    trDesc.Visible = true;
                    //ftbDescription.Text = dtr["DESCRIPTION"].ToString();
                    divAnnouncement.InnerHtml = dtr["DESCRIPTION"].ToString();

                    //linkAssingFile.Text = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    //linkAssingFile.NavigateUrl = dtr["ATTACHMENT"] == null ? "" : "upload_files/assignment/assignment_" + Convert.ToInt32(ViewState["asno"]) + System.IO.Path.GetExtension(dtr["ATTACHMENT"].ToString());

                    if (dtr["ATTACHMENT"].ToString() == null)
                        trFileAttach.Visible = false;
                    else if (dtr["ATTACHMENT"] != null)
                    {
                        trFileAttach.Visible = false;
                        // hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                        // hylFile.NavigateUrl = "~/ITLE/IAnnounce/" + dtr["ATTACHMENT"].ToString();
                        hylFile.Text = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                        hylFile.NavigateUrl = dtr["ATTACHMENT"] == null ? "" : "upload_files/announcement/announcement_" + Convert.ToInt32(ViewState["anno"]) + System.IO.Path.GetExtension(dtr["ATTACHMENT"].ToString());



                    }
                    lblLastdate.Text = dtr["STARTDATE"] == null ? "" : Convert.ToDateTime(dtr["STARTDATE"].ToString()).ToString("dd-MMM-yyyy");
                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Events

   

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlAnnounce.Visible = true;
        pnlText.Visible = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        DivAnnouncementList.Visible = true;
        //pnlAnnounce.Visible = true;
        divAnnounce.Visible = false;
    }

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/announcement/" + "announcement_" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    #endregion

    #region Attachments

    public string GetFileName(object filename, object announcemetnno)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/announcement/announcement_" + Convert.ToInt32(announcemetnno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/IAnnounce/" + filename.ToString();
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

    #endregion

   
    
    protected void btnEdit_Click(object sender, EventArgs e)
{
         try
        {
            LinkButton btnEdit = sender as LinkButton;
            int anno = Convert.ToInt32(btnEdit.CommandArgument);

            divAnnounce.Visible = true;
            DivAnnouncementList.Visible = false;
            //pnlAnnounce.Visible = false;
            ShowDetail(anno, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["SessionNo"]));
            //ftbDescription.ReadOnly = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_StudAnnouncement.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
}
}

