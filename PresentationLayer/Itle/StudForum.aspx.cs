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

public partial class Itle_StudForum : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
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
                if (Session["Page"] == null)
                {
                    CheckPageAuthorization();
                    Session["Page"] = 1;
                } 
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];
               
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                //lblCourseName.ForeColor = System.Drawing.Color.Green;
                BindForumGrid();

                // Used to insert id,date and courseno in Log_History table
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));

            }
         }

         

    }

    #endregion

    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudForum.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudForum.aspx");
        }
    }

    private void BindForumMessageListView(int forum_no)
    {
        try
        {
            IForumMasterController objAMC = new IForumMasterController();
            DataSet ds = objAMC.GetAllMessageByForum_No(forum_no);
            //lstVwThread.DataSource = ds;
            //lstVwThread.DataBind();
            lstVwMessage.DataSource = ds;
            lstVwMessage.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AddForum.BindForumMessageListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindForumGrid()
    {
        try
        {
            IForumMasterController objFMC = new IForumMasterController();
            DataSet ds = objFMC.GetAllForumByCourseNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]));
            lstVwForum.DataSource = ds;
            lstVwForum.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion

    #region Page events

    protected void btnlnkSelect_Click(object sender, EventArgs e)
    {
        LinkButton btnSelect = sender as LinkButton;
        ViewState["forum_no"] = int.Parse(btnSelect.CommandArgument); ;  /// if (ViewState["forum_no"] != null) 
        BindForumMessageListView(Convert.ToInt32(ViewState["forum_no"]));
        tdForumList.Visible = false;  
        tdMessages.Visible = true;  
    }

    protected void btmSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            IForumMasterController objFMC = new IForumMasterController();
            IForumMaster objFM = new IForumMaster();
            objFM.FORUM_NO = Convert.ToInt32(ViewState["forum_no"]);
            objFM.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objFM.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objFM.MESSAGE = txtMessage.Text.Trim();
            objFM.CREATEDDATE = DateTime.Now;
            if (txtMessage.Text != "" && txtMessage.Text != "&nbsp;")
            {
                CustomStatus cs = (CustomStatus)objFMC.AddMessage(objFM);
                txtMessage.Text = "&nbsp;";
                lblErrorMessage.Text = "";
                
            }
            else
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Text="Please enter text";
                return;
            }
           
            BindForumMessageListView(Convert.ToInt32(ViewState["forum_no"]));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_StudForum.btmSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }   

        tdMessages.Visible = true;
        tdAddPost.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtMessage.Text = "&nbsp;";
        tdMessages.Visible = true;
        tdAddPost.Visible = false;
        lblErrorMessage.Text = "";
    }

    protected void lnkPostMessage_Click(object sender, EventArgs e)
    {
        tdMessages.Visible = false;
        tdAddPost.Visible = true;  
    }

    protected void lnkBackForum_Click(object sender, EventArgs e)
    {
        tdMessages.Visible = false;
        tdForumList.Visible = true;  
    }

    protected void btnlnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddForum.aspx");
    }

    protected void btnlnkGoBack_Click(object sender, EventArgs e)
    {
        tdMessages.Visible = false;
        tdForumList.Visible = true;
    }

    #endregion
}
