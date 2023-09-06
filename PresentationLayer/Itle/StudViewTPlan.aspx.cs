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


public partial class Itle_StudViewTPlan : System.Web.UI.Page
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];
                      
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lvTPlan.Visible = true;
                divDesc.Visible = false;
                //pnlDesc.Visible = false;
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
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_StudViewTPlan.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_StudViewTPlan.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            ITeachingPlan objTPlan = new ITeachingPlan();
            ITeachingPlanController objPL = new ITeachingPlanController();

            objTPlan.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objTPlan.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            DataSet ds = objPL.GetAllTeachingPlanbyCourseNo(objTPlan);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTPlan.DataSource = ds;
                lvTPlan.DataBind();
                DivTeachingPlanList.Visible = true;
            }
            else
            {
                lvTPlan.DataSource = null;
                lvTPlan.DataBind();
                DivTeachingPlanList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_StudViewTPlan.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Events

    protected void lnkPlan_Click(object sender, EventArgs e)
    {
        try
        {
            //ITeachingPlan objPLan = new ITeachingPlan();
            ITeachingPlanController objPL = new ITeachingPlanController();
            LinkButton btnSelect = sender as LinkButton;

            //objPLan.PLAN_NO = int.Parse(btnSelect.CommandArgument);
            //objPLan.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            //objPLan.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            DivTeachingPlanList.Visible = false;
            
            DataTableReader dtr = null;

            dtr = objPL.GetSinglePlanByPlanNo(Convert.ToInt32(btnSelect.CommandArgument));

            if (dtr != null)
            {
               if (dtr.Read())
                {
                    divDesc.Visible = true;
                   // pnlDesc.Visible = true;
                   string TP = "Teaching Plan";
                   //tdSubject.InnerText =  Convert.ToDateTime(dtr["sDATE"]) + TP;
                   tdSubject.InnerText = Convert.ToDateTime(dtr["sDATE"]).ToString("dd/MM/yyyy") +'-'+ TP;
                    //ftbtxtDesc .Text= dtr["DESCRIPTION"].ToString();
                    divDescription.InnerHtml = dtr["TOPIC_COVERED"].ToString();
                }
            }
            //ftbtxtDesc.ReadOnly = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_StudViewTPlan.lnkPlan_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        divDesc.Visible = false;
        DivTeachingPlanList.Visible = true;
        //Response.Redirect("StudViewTPlan.aspx");
    }

    #endregion
}
