using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;
using System.Web;

public partial class Itle_selectCourse : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    public string NewForum = string.Empty;
    CourseControlleritle objCourse = new CourseControlleritle();
    int flag = 0;

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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                
               // lblSession.Text = Session["SESSION_NAME"].ToString();
               // lblSession.ToolTip = Session["SessionNo"].ToString();  
                FillDropdown();
            }
            //BindListView();
            FillDropdown();
         //   BindTestList();
          //  BindNewAssignment();     
            //Session["COLLEGE_ID"].ToString();
        }

        //BindTestList();
        //BindNewAssignment();
        //NewMailNotification();

        if (Convert.ToInt32(Session["usertype"]) == 4 || Convert.ToInt32(Session["usertype"]) == 2)
        {
            NewForumNotification();
            trForum.Visible = true;
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
               Response.Redirect("~/notauthorized.aspx?page=selectCourse.aspx");
                
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=selectCourse.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            //CourseController objCourse = new CourseController();
            
            DataSet ds = null;
        
            if (Convert.ToInt32(Session["usertype"]) == 2)    
               ds = objCourse.GetCourseByUaNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["usertype"]));
               // ds = objCourse.GetCourseByUaNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["usertype"]));
            
            else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
                ds = objCourse.GetCourseByUaNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]));

            
            lvCourse.DataSource = ds;
            lvCourse.DataBind();
            trSession.Visible = false;
            pnllvCourseList.Visible = true;
            lblSession.Text = ddlSession.SelectedItem.Text.Trim();

            RpCourse.DataSource = ds;
            RpCourse.DataBind();
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_selectCourse.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDropdown()
    {
        DataSet ds = null;  
        try
        {
            // GAYATRI RODE 04-05-2022      (11-05-2022)
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
              // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");
               
               // objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A INNER JOIN ACD_SESSION_MASTER B ON(A.SESSIONNO =B.SESSIONNO)  ", " DISTINCT A.SESSIONNO", "B.SESSION_NAME", "A.SESSIONNO>0 AND EXAMTYPE IN (1,3) and IDNO="+Convert.ToInt32(Session["userno"]), "SESSIONNO DESC");

              // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) inner join ACD_STUDENT_RESULT S on (A.SESSIONNO=S.SESSIONNO)", "DISTINCT A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "A.SESSIONNO>0 AND EXAMTYPE IN (1,3) AND IDNO=" + Convert.ToInt32(Session["idno"]), "A.SESSIONNO ");
            }
            else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
            {

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");

//objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID)INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID AND CT.SESSIONNO = SM.SESSIONNO)INNER JOIN ACD_COLLEGE_MASTER B ON (SM.COLLEGE_ID=B.COLLEGE_ID )", "DISTINCT CT.SESSIONNO", "CONCAT( SM.SESSION_NAME ,' - ', B.COLLEGE_NAME) as SESSION_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND sm.SESSIONNO>0 AND sm.EXAMTYPE IN (1,3)" + "and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID)INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID AND CT.SESSIONNO = SM.SESSIONNO)INNER JOIN ACD_COLLEGE_MASTER B ON (SM.COLLEGE_ID=B.COLLEGE_ID )", "CT.SESSIONNO", "CONCAT( SM.SESSION_NAME ,' - ', B.COLLEGE_NAME) as SESSION_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  sm.SESSIONNO>0 AND sm.EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");
            }
                
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");
          //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND EXAMTYPE IN (1,3) ", "SESSIONNO DESC");

            if (ds == null)
            {
                pnllvCourseList.Visible = false;
            }
            else
            {
                pnllvCourseList.Visible = true;
            }
            //commet by zubair on client requirement on 10-03-2016
            //ds = objCommon.FillDropDown("ACD_SESSION_MASTER", " Top 1 SESSION_NAME", "SESSIONNO", "SESSIONNO>0 AND FLOCK=1", "SESSIONNO DESC");
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            //    BindListView();

            //    Session["SessionNo"] = Convert.ToInt32(ddlSession.SelectedValue);
            //    Session["SESSION_NAME"] = ddlSession.SelectedItem.Text;
            //}
            if (Session["SessionNo"] != null)
            {
                ddlSession.SelectedValue = Session["SessionNo"].ToString();
               // BindListView();
            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void BindTestList()
    {
        try
        {

            CourseControlleritle objAC = new CourseControlleritle();
            DataSet ds = null;
            int idno;
            if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 1)
            {
                idno = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                idno = Convert.ToInt32(Session["idno"]);
            }

            ds = objAC.GetTestNotifications(idno, Convert.ToInt32(Session["usertype"]));

            lvTest.DataSource = ds;
            lvTest.DataBind();


        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "ITLE_StudTest.aspx.BindListView->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    // Used to Notify New Assignments
    private void BindNewAssignment()
    {
        try
        {

            AssignmentController objAM = new AssignmentController();
            int idno;

            if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 1)
            {
                idno = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                idno = Convert.ToInt32(Session["idno"]);
            }

            DataSet ds = objAM.GetAllAssignmentNotificatios(Convert.ToInt32(Session["SessionNo"]), idno, Convert.ToInt32(Session["usertype"]));
            if (Convert.ToInt32(Session["usertype"]) == 4 || Convert.ToInt32(Session["usertype"]) == 2)
            {

                lvNewAssignment.DataSource = ds;
                lvNewAssignment.DataBind();
                lvNewAssignment.Visible = true;
            }

            else
            {
                lvFacultyAssingment.DataSource = ds;
                lvFacultyAssingment.DataBind();
                lvFacultyAssingment.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // Used to Notify New Mail
    private void NewMailNotification()
    {
        try
        {
            int unreadMail = Convert.ToInt32(objCommon.LookUp("ACD_IRECEIVER", "COUNT(ISNULL(STATUS,0))", "STATUS='U' AND RECEIVER_ID=" + Convert.ToInt32(Session["userno"])));
            lblMailCount.Text = unreadMail.ToString();
            if (unreadMail > 0)
            {
                trNewMail.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //Used to Notify New Forum
    private void NewForumNotification()
    {
        try
        {
            IForumMasterController objFM = new IForumMasterController();
            string usename = objCommon.LookUp("user_acc", "ua_fullname", "ua_no=" + Convert.ToInt32(Session["userno"].ToString()));

            NewForum = objFM.NewForumNotification(usename, Convert.ToInt32(Session["idno"]));

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion

    #region Page Events

    protected void btnlnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnSelect = sender as LinkButton;
            Session["ICourseNo"] = int.Parse(btnSelect.CommandArgument);
            Session["ICourseName"] = btnSelect.Text;

            int CourseNo = Convert.ToInt32(Session["ICourseNo"]);


            //Used to insert id,date and courseno in Log_History table
            //int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]),Convert.ToInt32(Session["ICourseNo"]));

            //TEMPORARY FOR IITMS FRESHER'S TEST
            //TestPage();

            //Response.Redirect("InterMediate.aspx?cno=" + (Session["ICourseNo"]));

            //Response.Redirect("InterMediate.aspx?cno=" + CourseNo);  //Previous One

            string url = "~/ITLE/InterMediate.aspx?UId=" + CourseNo; //Added by Saket Singh on 05-09-2017
            Response.Redirect(url, false);

            //Response.Redirect("~/home.aspx");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_selectCourse.btnlnkSelect_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlMainCourse, "ACD_COURSE A INNER JOIN ACD_STUDENT_RESULT B ON A.COURSENO=B.COURSENO", "DISTINCT B.COURSENO", "A.COURSE_NAME", "B.SESSIONNO=" + ddlSession.SelectedValue, "B.COURSENO");

                BindListView();

                Session["SessionNo"] = Convert.ToInt32(ddlSession.SelectedValue);
                Session["SESSION_NAME"] = ddlSession.SelectedItem.Text;
                trSession.Visible = false;
                pnllvCourseList.Visible = true;
            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "selectCourse.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void imgNotify_Click(object sender, EventArgs e)
    {
        try
        {
            if (imgNotify.ToolTip == "Open Notifications")
            {
                pnlNotifications.Visible = true;
                imgNotify.ToolTip = "Close Notifications";
                BindNewAssignment(); BindTestList();
            }
            else
            {
                pnlNotifications.Visible = false;
                imgNotify.ToolTip = "Open Notifications";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "selectCourse.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Commented Codes

    //protected void ddlMainCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlMainCourse.SelectedIndex > 0)
    //        {
    //            BindListView();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        objUCommon.ShowError(Page, "selectCourse.ddlMainCourse_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //    }
    //}

   
    //private void TestPage()
    //{
    //    try
    //    {
    //        int idno=Convert.ToInt32(Session["idno"]);

    //        switch(idno)
    //        {
    //            case 223:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 224:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 225:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 226:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 227:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 228:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 229:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 230:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 231:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            case 232:
    //                Response.Redirect("IITMS_FRESHERS_TEST.aspx");
    //                break;

    //            default:
    //                Response.Redirect("InterMediate.aspx?cno=" + (Session["ICourseNo"]));
    //                break;


    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
    //    }
    //}

    #endregion

    //added by tanu for corse selection 
    protected void btnlncardkSelect_Click(object sender, EventArgs e)
    {
        LinkButton btnlncardkSelect = sender as LinkButton;
        Session["ICourseNo"] = Convert.ToInt32(btnlncardkSelect.CommandArgument);
        Session["ICourseName"] = btnlncardkSelect.CommandName;
        int CourseNo = Convert.ToInt32(Session["ICourseNo"]);
        string CourseName = Session["ICourseName"].ToString();
        //Image imglogo = new Image();
        if (Session["ICourseNo"] != null || Session["ICourseName"] != null)
        {
            string url = string.Empty;
            url += HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
           // url += "/PresentationLayer/Images/courseselection.png";//local
            url += "/Images/courseselection.png";// live
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "validate('" + CourseName.ToString() + "','" + url.ToString() + "')", true);
        }
        else
        {
            MessageBox("Please Select The Course");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
   
}
