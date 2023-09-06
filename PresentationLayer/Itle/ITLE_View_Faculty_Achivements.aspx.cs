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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Itle_ITLE_View_Faculty_Achivements : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    IAnnouncement objSub = new IAnnouncement();
    IAnnouncementController objSY = new IAnnouncementController();
    CourseControlleritle objCourse = new CourseControlleritle();
    string PageId;

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

                PageId = Request.QueryString["pageno"];
                
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourse.ForeColor = System.Drawing.Color.Green;
                
                lvFAchivement.Visible = true;
                divDesc.Visible = false;
                //pnlDesc.Visible = false;
                BindListView();


                // Used to insert id,date and courseno in Log_History table
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
            }
        }

    }

    private void BindListView()
    {
        try
        {


            objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            DataSet ds = objSY.GetAllFAchivementCourseNo(objSub);
            lvFAchivement.DataSource = ds;
            lvFAchivement.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_View_Faculty_Achivements.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkPlan_Click(object sender, EventArgs e)
    {
        try
        {


            LinkButton btnSelect = sender as LinkButton;
            //objPLan.PLAN_NO = int.Parse(btnSelect.CommandArgument);
            //objPLan.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            //objPLan.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            lvFAchivement.Visible = false;
            divDesc.Visible = true;
            ftbtxtDesc.ReadOnly = true;

            DataTableReader dtr = null;

            dtr = objSY.GetSingleAchiveByAchivNo(Convert.ToInt32(btnSelect.CommandArgument));

            if (dtr != null)
            {
                if (dtr.Read())
                {
                    divDesc.Visible = true;
                    // pnlDesc.Visible = true;
                    tdSubject.InnerText = dtr["AWDTYPE"].ToString();
                    ftbtxtDesc.Text = dtr["DESCRIPTION"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_View_Faculty_Achivements.lnkPlan_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ITLE_View_Faculty_Achivements.aspx");
    }

    #region Public Method

    public string GetFileName(object filename, object announcemetnno)
    {
        if (filename != null && filename.ToString() != "")
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
    #endregion

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/Achievements/" + "achievements_" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }
}
