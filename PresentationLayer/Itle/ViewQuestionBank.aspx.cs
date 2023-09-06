using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Itle_ViewQuestionBank : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IQuestionbankController objIQBC = new IQuestionbankController();
    IQuestionbank objQuest = new IQuestionbank();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
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
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

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
                FillDropdown();
                BindListView();
                
            }
        }
        pnlAdd.Visible = true;

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
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
                Response.Redirect("~/notauthorized.aspx?page=QuestionBankMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QuestionBankMaster.aspx");
        }
    }


    private void BindListView()
    {
        try
        {
            objQuest.OBJECTIVE_DESCRIPTIVE = Convert.ToChar(rbnObjectiveDescriptive.SelectedValue);
            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objQuest.UA_NO = Convert.ToInt32(Session["userno"]);
            objQuest.TOPIC = Convert.ToString(ddlSession.SelectedValue.ToString());
            DataSet ds = objIQBC.ViewAllStudentQuestion(objQuest);
            lvQuestions.DataSource = ds.Tables[0];
            lvQuestions.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("StudQuestionBank.aspx");
        }

    }

    private void FillDropdown()
    {
        DataSet ds = null;
        try
        {
            objIQBC.FillDropDownTopic(ddlSession, "ACD_STUDQUESTIONBANK", "DISTINCT", "TOPIC", "COURSENO=" + Session["ICourseNo"], "TOPIC DESC");
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    #endregion
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void rbnObjectiveDescriptive_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }
}