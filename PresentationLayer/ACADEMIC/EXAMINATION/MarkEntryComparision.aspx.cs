//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : MarkEntryComparision                                   
// CREATION DATE : 15-OCT-2011
// CREATED BY    :                                                 
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================


using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_MarkEntryComparision : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objmec = new MarksEntryController(); string idnos = string.Empty;
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1"); // ADD BY ROSHAN PANNASE 31-12-2015 FOR COURSE REGISTER IN START SESSION ONELY.

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (CheckActivity())
                {
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        //CHECK ACTIVITY FOR EXAM REGISTRATION
                        pnlMain.Visible = false;
                        this.CheckPageAuthorization();
                    }
                    //else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
                    //{
                    //    this.CheckPageAuthorization();
                    //}
                    else
                    {
                        this.CheckPageAuthorization();
                        pnlMain.Visible = true;
                    }
                }
                else
                {

                }

                string IPADDRESS = string.Empty;


               ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
               //ViewState["ipAddress"] = "14.139.110.183"; //Request.ServerVariables["REMOTE_ADDR"];
               PopulateDropDownList();
            }
        }
    }

    private bool CheckActivity()
    {
        bool ret = true;
        string sessionno = string.Empty;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        //sessionno = Session["currentsession"].ToString();
        if (String.IsNullOrEmpty(sessionno))
            sessionno = "0";
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlMain.Visible = false;
                ret = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlMain.Visible = false;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlMain.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");
            ddlSession.SelectedIndex = 0;
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "DEGREENAME DESC");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME  ", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID,C.COLLEGE_NAME DESC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region OldCompareCode
    //protected void btnCompare_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // GridView gvmismatch = new GridView();
    //        string CHKLOCK = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and ( college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "= 0)" + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));
    //        string CHKLOCK1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and ( college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "= 0)" + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "AND ISNULL(LOCK,0)=1");


    //        if ((CHKLOCK == string.Empty || CHKLOCK == "" || CHKLOCK == "0") && (CHKLOCK1 == string.Empty || CHKLOCK1 == "" || CHKLOCK1 == "0"))
    //        {
    //            objCommon.DisplayMessage(updCompare, "Mark entry not locked by operator", this.Page);

    //            return;
    //        }
    //        else if (CHKLOCK == CHKLOCK1)
    //        {

    //            string countop = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "and (college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "=0)");

    //            if (Convert.ToInt32(countop) != 1)
    //            {
    //                DataSet ds = objmec.GetMarkCompare(Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlInEx.SelectedValue));
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    objCommon.DisplayMessage(updCompare, "Below Marks entry MISMATCHED for both the operators", this.Page);
    //                    gvmismatch.DataSource = ds.Tables[0];
    //                    gvmismatch.DataBind();
    //                    gvmismatch.HeaderStyle.BackColor = System.Drawing.Color.Ivory;
    //                    btnUnlock.Enabled = true;
    //                    foreach (GridViewRow item in gvmismatch.Rows)
    //                    {
    //                        for (int i = 0; i < gvmismatch.Rows.Count; i++)
    //                        {
    //                            ViewState["idnos"] += ds.Tables[0].Rows[i]["idno"].ToString() + ",";
    //                        }
    //                    }

    //                }
    //                else
    //                {
    //                    btnUnlock.Enabled = false;
    //                    string movestatus = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "and move_status=1");
    //                    //btnMove.Enabled = true;
    //                    btnMove_Click(sender, e);
    //                    // objCommon.DisplayMessage(updCompare, "Marks entry matched for both the operators", this.Page);
    //                    gvmismatch.DataSource = null;
    //                    gvmismatch.DataBind();
    //                    return;
    //                }
    //            }
    //            else if (Convert.ToInt32(countop) == 1)
    //            {
    //                objCommon.DisplayMessage(updCompare, "Both Operator entry must to required for comparison.", this.Page);
    //                gvmismatch.DataSource = null;
    //                gvmismatch.DataBind();
    //                btnUnlock.Enabled = false;
    //                btnMove.Enabled = false;
    //                return;
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(updCompare, "Entry not proper for this selection.", this.Page);
    //                gvmismatch.DataSource = null;
    //                gvmismatch.DataBind();
    //                btnUnlock.Enabled = false;
    //                btnMove.Enabled = false;
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updCompare, "Mark entry not locked by operator.", this.Page);
    //            gvmismatch.DataSource = null;
    //            gvmismatch.DataBind();
    //            btnUnlock.Enabled = false;
    //            btnMove.Enabled = false;
    //            return;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayMessage(updCompare, "Mark entry not Proper.", this.Page);
    //    }
    //}
    #endregion

    protected void btnCompare_Click(object sender, EventArgs e)
    {
        try
        {
            string CHKLOCK = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " and schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue)+ " and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue)+ " and is_mids_ends=" + Convert.ToInt32(ddlExamName.SelectedValue));
            string CHKLOCK1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " and schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and is_mids_ends=" + Convert.ToInt32(ddlExamName.SelectedValue) + " and ISNULL(LOCK,0)=1");
            string chkop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1");
            string chkop1Courses = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(DISTINCT COURSENO)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1");
            //string chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(DISTINCT COURSENO)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND (" + ddlExamName.SelectedValue == "1" ? "S3MARK" : "EXTERMARK" + ") IS NOT NULL AND (" + ddlExamName.SelectedValue == "1" ? "ISNULL(LOCKS3,0)" : "ISNULL(LOCKE,0)" + ")  = 1");
            string chklOCKop1 = string.Empty;
            
            if(ddlExamName.SelectedValue == "1")
                chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND S3MARK IS NOT NULL AND ISNULL(LOCKS3,0)  = 1");
            else if (ddlExamName.SelectedValue == "2")
                chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND EXTERMARK IS NOT NULL AND ISNULL(LOCKE,0)  = 1");

            // FOR OPERATOR 2
            DataSet dsData = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A  INNER JOIN ACD_STUDENT_RESULT B ON (A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.SESSIONNO = B.SESSIONNO AND A.COURSENO = B.COURSENO) INNER JOIN ACD_STUDENT C ON A.SCHEMENO = C.SCHEMENO AND C.COLLEGE_ID = A.COLLEGE_ID AND B.IDNO = C.IDNO INNER JOIN ACD_COURSE D ON D.COURSENO = A.COURSENO INNER JOIN USER_ACC E ON E.UA_NO = A.OP_ID", "C.IDNO,C.REGNO AS ENROLLMENTNO,C.ROLLNO", "D.CCODE + ' - ' + COURSE_NAME AS COURSENAME,A.OP_ID,E.UA_FULLNAME AS OP", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND A.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue) + " AND ISNULL(A.LOCK,0)=0", "C.REGNO,A.COURSENO");
            // FOR OPERATOR 1
            DataSet dsData1 = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT C ON A.SCHEMENO = C.SCHEMENO AND A.IDNO = C.IDNO INNER JOIN ACD_COURSE D ON D.COURSENO = A.COURSENO LEFT JOIN USER_ACC E ON E.UA_NO = A.UA_NO", "C.IDNO,C.REGNO AS ENROLLMENTNO,C.ROLLNO", "D.CCODE + ' - ' + COURSE_NAME AS COURSENAME,A.UA_NO,CASE WHEN ISNULL(A.UA_NO,0) = 0 THEN ' - ' ELSE E.UA_FULLNAME END AS OP", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND A.SUBID = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN ISNULL(A.LOCKS3,0) WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.LOCKE,0) END = 0 AND ISNULL(REGISTERED,0) = 1 AND ISNULL(ACCEPTED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(EXAM_REGISTERED,0) END = 1", "C.REGNO,A.COURSENO");

            if (!String.IsNullOrEmpty(CHKLOCK) && CHKLOCK.ToString() != "0")
            {
                if (!String.IsNullOrEmpty(chkop1) && chkop1.ToString() != "0")
                {
                    if (CHKLOCK == chkop1Courses)// chkop1)
                    {
                        if (!String.IsNullOrEmpty(CHKLOCK1) && CHKLOCK1.ToString() != "0")
                        {
                            if (CHKLOCK == CHKLOCK1)
                            {
                                if (!String.IsNullOrEmpty(chkop1) && chkop1.ToString() != "0")
                                {
                                    if (!String.IsNullOrEmpty(chklOCKop1) && chklOCKop1.ToString() != "0")
                                    {
                                        if (chkop1 == chklOCKop1)
                                        {
                                            DataSet ds = objmec.GetMarkCompare(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
                                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                                            {
                                                objCommon.DisplayMessage(updCompare, "Below Marks entry MISMATCHED for both the operators !!!!", this.Page);
                                                gvmismatch.DataSource = ds.Tables[0];
                                                gvmismatch.DataBind();
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                                gvmismatch.HeaderStyle.BackColor = System.Drawing.Color.Ivory;
                                                btnUnlock.Enabled = true;
                                                int col = gvmismatch.Columns.Count;
                                                lblNote.Visible = true;
                                                //foreach (GridViewRow item in gvmismatch.Rows)
                                                //{
                                                //    for (int i = 0; i < gvmismatch.Rows.Count; i++)
                                                //    {
                                                //        ViewState["idnos"] += ds.Tables[0].Rows[i]["idno"].ToString() + ",";
                                                //        ViewState["OPIDs"] += ds.Tables[0].Rows[i]["OPID"].ToString() + ",";
                                                //    }
                                                //}
                                                
                                                    for (int i = 0; i < gvmismatch.Rows.Count; i++)
                                                    {
                                                        ViewState["idnos"] += ds.Tables[0].Rows[i]["idno"].ToString() + ",";
                                                        ViewState["OPIDs"] += ds.Tables[0].Rows[i]["OPID"].ToString() + ",";
                                                    }
                                                
                                                ViewState["idnos"] = ViewState["idnos"].ToString().TrimEnd(',');
                                                ViewState["OPIDs"] = ViewState["OPIDs"].ToString().TrimEnd(',');
                                            }
                                            else
                                            {
                                                btnUnlock.Enabled = false;
                                                lblNote.Visible = false;
                                                try
                                                {
                                                    int n = objmec.InsMatchMarksCompLog(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                                objCommon.DisplayMessage(updCompare, "Marks entry matched for both the operators !!!!", this.Page);
                                               
                                                gvmismatch.DataSource = null;
                                                gvmismatch.DataBind();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(updCompare, "Mark entry not locked by Faculty / Operator 1 !!!!", this.Page);
                                            gvmismatch.DataSource = null;
                                            gvmismatch.DataBind();
                                            lblNote.Visible = false;
                                            if (dsData1.Tables[0].Rows.Count > 0 && dsData1 != null)
                                            {
                                                gvData.Visible = true;
                                                gvData.DataSource = dsData1;
                                                gvData.DataBind();
                                            }
                                            else
                                            {
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                            }
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(updCompare, "Mark entry not locked by Faculty / Operator 1 !!!!", this.Page);
                                        gvmismatch.DataSource = null;
                                        gvmismatch.DataBind();
                                        lblNote.Visible = false;
                                        if (dsData1.Tables[0].Rows.Count > 0 && dsData1 != null)
                                        {
                                            gvData.Visible = true;
                                            gvData.DataSource = dsData1;
                                            gvData.DataBind();
                                        }
                                        else
                                        {
                                            gvData.Visible = false;
                                            gvData.DataSource = null;
                                            gvData.DataBind();
                                        }
                                        return;
                                    }

                                }
                                else
                                {
                                    objCommon.DisplayMessage(updCompare, "No record found for Faculty / Operator 1 for the selection !!!!", this.Page);
                                    gvmismatch.DataSource = null;
                                    gvmismatch.DataBind();
                                    lblNote.Visible = false;
                                    gvData.Visible = false;
                                    gvData.DataSource = null;
                                    gvData.DataBind();
                                    return;
                                }

                            }
                            else
                            {
                                objCommon.DisplayMessage(updCompare, "Mark entry not locked by Operator 2 !!!!", this.Page);
                                gvmismatch.DataSource = null;
                                gvmismatch.DataBind();
                                lblNote.Visible = false;
                                if (dsData.Tables[0].Rows.Count > 0 && dsData != null)
                                {
                                    gvData.Visible = true;
                                    gvData.DataSource = dsData;
                                    gvData.DataBind();
                                }
                                else
                                {
                                    gvData.Visible = false;
                                    gvData.DataSource = null;
                                    gvData.DataBind();
                                }
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCompare, "Mark entry not locked by Operator 2 !!!!", this.Page);
                            gvmismatch.DataSource = null;
                            gvmismatch.DataBind();
                            lblNote.Visible = false;
                            if (dsData.Tables[0].Rows.Count > 0 && dsData != null)
                            {
                                gvData.Visible = true;
                                gvData.DataSource = dsData;
                                gvData.DataBind();
                            }
                            else
                            {
                                gvData.Visible = false;
                                gvData.DataSource = null;
                                gvData.DataBind();
                            }
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCompare, "Courses for the Operator 2 allotment mismatched with Faculty / Operator 1 courses!!!!", this.Page);
                        gvmismatch.DataSource = null;
                        gvmismatch.DataBind();
                        lblNote.Visible = false;
                        gvData.Visible = false;
                        gvData.DataSource = null;
                        gvData.DataBind();
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCompare, "No record found for Faculty / Operator 1 for the above selection !!!!", this.Page);
                    gvmismatch.DataSource = null;
                    gvmismatch.DataBind();
                    lblNote.Visible = false;
                    gvData.Visible = false;
                    gvData.DataSource = null;
                    gvData.DataBind();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updCompare, "No record found of Operator 2 for Comparison !!!!", this.Page);
                gvmismatch.DataSource = null;
                gvmismatch.DataBind();
                lblNote.Visible = false;
                gvData.Visible = false;
                gvData.DataSource = null;
                gvData.DataBind();
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(updCompare, "Mark entry not Proper !!!!", this.Page);
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO DESC"); // added on 23-03-2020 by Vaishali
                if (ddlColg.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "A.LONGNAME");

                }
                else
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue , "A.LONGNAME");
                }
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO ="+ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSemester.SelectedIndex = 0;
            }



            //ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue, "CCODE");
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO inner join acd_student ss on(sr.idno=ss.idno)", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "and (college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "=0)", "COURSE_NAME");
                ddlCourse.Focus();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }



            ddlCourse.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }


    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0

            }
            else
            {
                ddlSemester.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlColg.SelectedValue+"", "D.DEGREENO");
            ddlDegree.Focus();
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");

            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lvDetails.DataSource = ds.Tables[0];
            //    lvDetails.DataBind();
            //}
            //else
            //{
            //    lvDetails.DataSource = null;
            //    lvDetails.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            //string cNTop = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "AND UNLOCK_DATE is not null ");
           
            //DataSet opid = objmec.Getopidstring(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlInEx.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            //string opidstr = opid.Tables[0].Rows[0]["opid"].ToString();
           // string opids = ViewState["OPIDs"].ToString();
            string idnos = ViewState["idnos"].ToString();
            int cs = objmec.UnLockOperatorMarkEntry(ViewState["OPIDs"].ToString(), ViewState["idnos"].ToString(), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
            if (cs == 2)
            {
                objCommon.DisplayMessage(updCompare, "Marks Unlocked Successfully !!!!", this.Page);
                gvmismatch.DataSource = null;
                gvmismatch.DataBind();
                gvData.DataSource = null;
                gvData.DataBind();
                lblNote.Visible = false;
                return;
            }
            else
            {
                objCommon.DisplayMessage(updCompare, "Error...", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.btnUnlock_Click-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    #region MisMatchReportOldCode
    //protected void btnreport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridView GVDayWiseAtt = new GridView();
    //        string ContentType = string.Empty;
    //        string CHKLOCK = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and ( college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "= 0)" + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));
    //        string CHKLOCK1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and ( college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "= 0)" + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "AND ISNULL(LOCK,0)=1");


    //        if ((CHKLOCK == string.Empty || CHKLOCK == "" || CHKLOCK == "0") && (CHKLOCK1 == string.Empty || CHKLOCK1 == "" || CHKLOCK1 == "0"))
    //        {
    //            objCommon.DisplayMessage(updCompare, "Mark entry not locked by operator", this.Page);
    //            return;
    //        }
    //        else if (CHKLOCK == CHKLOCK1)
    //        {
    //            string countop = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));
    //            if (countop == string.Empty || countop == "" || countop == "0")
    //            {
    //                objCommon.DisplayMessage(updCompare, "Data not available", this.Page);
    //                return;
    //            }
    //            DataSet ds = objmec.GetMarkCompareReport(Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlInEx.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                //ds.Tables[0].Columns.RemoveAt(3);
    //                GVDayWiseAtt.DataSource = ds;
    //                GVDayWiseAtt.DataBind();
    //                string attachment = "attachment; filename=Mismatch Mark entry by oprerator.xls";
    //                Response.ClearContent();
    //                Response.AddHeader("content-disposition", attachment);
    //                Response.ContentType = "application/vnd.MS-excel";
    //                StringWriter sw = new StringWriter();
    //                HtmlTextWriter htw = new HtmlTextWriter(sw);
    //                GVDayWiseAtt.RenderControl(htw);
    //                Response.Write(sw.ToString());
    //                Response.End();
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(updCompare, "Mismatch data not available for above selection", this.Page);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updCompare, "Mark entry not locked by operator.", this.Page);
    //            gvmismatch.DataSource = null;
    //            gvmismatch.DataBind();
    //            btnUnlock.Enabled = false;
    //            btnMove.Enabled = false;
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //        {
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}
    #endregion

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();

            string CHKLOCK = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " and schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and is_mids_ends=" + Convert.ToInt32(ddlExamName.SelectedValue));
            string CHKLOCK1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " and schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and is_mids_ends=" + Convert.ToInt32(ddlExamName.SelectedValue) + " and ISNULL(LOCK,0)=1");
            string chkop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1");
            string chkop1Courses = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(DISTINCT COURSENO)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1");
            //string chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(DISTINCT COURSENO)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND (" + ddlExamName.SelectedValue == "1" ? "S3MARK" : "EXTERMARK" + ") IS NOT NULL AND (" + ddlExamName.SelectedValue == "1" ? "ISNULL(LOCKS3,0)" : "ISNULL(LOCKE,0)" + ")  = 1");
            string chklOCKop1 = string.Empty;

            if (ddlExamName.SelectedValue == "1")
                chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND S3MARK IS NOT NULL AND ISNULL(LOCKS3,0)  = 1");
            else if (ddlExamName.SelectedValue == "2")
                chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND EXTERMARK IS NOT NULL AND ISNULL(LOCKE,0)  = 1");

            // FOR OPERATOR 2
            DataSet dsData = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A  INNER JOIN ACD_STUDENT_RESULT B ON (A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.SESSIONNO = B.SESSIONNO AND A.COURSENO = B.COURSENO) INNER JOIN ACD_STUDENT C ON A.SCHEMENO = C.SCHEMENO AND C.COLLEGE_ID = A.COLLEGE_ID AND B.IDNO = C.IDNO INNER JOIN ACD_COURSE D ON D.COURSENO = A.COURSENO INNER JOIN USER_ACC E ON E.UA_NO = A.OP_ID", "C.IDNO,C.REGNO AS ENROLLMENTNO,C.ROLLNO", "D.CCODE + ' - ' + COURSE_NAME AS COURSENAME,A.OP_ID,E.UA_FULLNAME AS OP", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND A.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue) + " AND ISNULL(A.LOCK,0)=0", "C.REGNO,A.COURSENO");
            // FOR OPERATOR 1
            DataSet dsData1 = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT C ON A.SCHEMENO = C.SCHEMENO AND A.IDNO = C.IDNO INNER JOIN ACD_COURSE D ON D.COURSENO = A.COURSENO LEFT JOIN USER_ACC E ON E.UA_NO = A.UA_NO", "C.IDNO,C.REGNO AS ENROLLMENTNO,C.ROLLNO", "D.CCODE + ' - ' + COURSE_NAME AS COURSENAME,A.UA_NO,CASE WHEN ISNULL(A.UA_NO,0) = 0 THEN ' - ' ELSE E.UA_FULLNAME END AS OP", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND A.SUBID = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN ISNULL(A.LOCKS3,0) WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.LOCKE,0) END = 0 AND ISNULL(REGISTERED,0) = 1 AND ISNULL(ACCEPTED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(EXAM_REGISTERED,0) END = 1", "C.REGNO,A.COURSENO");

            if (!String.IsNullOrEmpty(CHKLOCK) && CHKLOCK.ToString() != "0")
            {
                if (!String.IsNullOrEmpty(chkop1) && chkop1.ToString() != "0")
                {
                    if (CHKLOCK == chkop1Courses)// chkop1)
                    {
                        if (!String.IsNullOrEmpty(CHKLOCK1) && CHKLOCK1.ToString() != "0")
                        {
                            if (CHKLOCK == CHKLOCK1)
                            {
                                if (!String.IsNullOrEmpty(chkop1) && chkop1.ToString() != "0")
                                {
                                    if (!String.IsNullOrEmpty(chklOCKop1) && chklOCKop1.ToString() != "0")
                                    {
                                        if (chkop1 == chklOCKop1)
                                        {
                                            DataSet ds = objmec.GetMarkCompareReport(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                                GVDayWiseAtt.DataSource = ds;
                                                GVDayWiseAtt.DataBind();
                                                string attachment = "attachment; filename=Mismatch Mark entry by operator.xls";
                                                Response.ClearContent();
                                                Response.AddHeader("content-disposition", attachment);
                                                Response.ContentType = "application/vnd.MS-excel";
                                                StringWriter sw = new StringWriter();
                                                HtmlTextWriter htw = new HtmlTextWriter(sw);
                                                GVDayWiseAtt.RenderControl(htw);
                                                Response.Write(sw.ToString());
                                                Response.End();
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(updCompare, "Mismatch data not available for above selection", this.Page);
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(updCompare, "Mark entry not locked by Faculty / Operator 1 !!!!", this.Page);
                                            gvmismatch.DataSource = null;
                                            gvmismatch.DataBind();
                                            lblNote.Visible = false;
                                            if (dsData1.Tables[0].Rows.Count > 0 && dsData1 != null)
                                            {
                                                gvData.Visible = true;
                                                gvData.DataSource = dsData1;
                                                gvData.DataBind();
                                            }
                                            else
                                            {
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                            }
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(updCompare, "Mark entry not locked by Faculty / Operator 1 !!!!", this.Page);
                                        gvmismatch.DataSource = null;
                                        gvmismatch.DataBind();
                                        lblNote.Visible = false;
                                        if (dsData1.Tables[0].Rows.Count > 0 && dsData1 != null)
                                        {
                                            gvData.Visible = true;
                                            gvData.DataSource = dsData1;
                                            gvData.DataBind();
                                        }
                                        else
                                        {
                                            gvData.Visible = false;
                                            gvData.DataSource = null;
                                            gvData.DataBind();
                                        }
                                        return;
                                    }

                                }
                                else
                                {
                                    objCommon.DisplayMessage(updCompare, "No record found for Faculty / Operator 1 for the selection !!!!", this.Page);
                                    gvmismatch.DataSource = null;
                                    gvmismatch.DataBind();
                                    lblNote.Visible = false;
                                    gvData.Visible = false;
                                    gvData.DataSource = null;
                                    gvData.DataBind();
                                    return;
                                }

                            }
                            else
                            {
                                objCommon.DisplayMessage(updCompare, "Mark entry not locked by Operator 2 !!!!", this.Page);
                                gvmismatch.DataSource = null;
                                gvmismatch.DataBind();
                                lblNote.Visible = false;
                                if (dsData.Tables[0].Rows.Count > 0 && dsData != null)
                                {
                                    gvData.Visible = true;
                                    gvData.DataSource = dsData;
                                    gvData.DataBind();
                                }
                                else
                                {
                                    gvData.Visible = false;
                                    gvData.DataSource = null;
                                    gvData.DataBind();
                                }
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCompare, "Mark entry not locked by Operator 2 !!!!", this.Page);
                            gvmismatch.DataSource = null;
                            gvmismatch.DataBind();
                            lblNote.Visible = false;
                            if (dsData.Tables[0].Rows.Count > 0 && dsData != null)
                            {
                                gvData.Visible = true;
                                gvData.DataSource = dsData;
                                gvData.DataBind();
                            }
                            else
                            {
                                gvData.Visible = false;
                                gvData.DataSource = null;
                                gvData.DataBind();
                            }
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCompare, "Courses for the Operator 2 allotment mismatched with Faculty / Operator 1 courses!!!!", this.Page);
                        gvmismatch.DataSource = null;
                        gvmismatch.DataBind();
                        lblNote.Visible = false;
                        gvData.Visible = false;
                        gvData.DataSource = null;
                        gvData.DataBind();
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCompare, "No record found for Faculty / Operator 1 for the above selection !!!!", this.Page);
                    gvmismatch.DataSource = null;
                    gvmismatch.DataBind();
                    lblNote.Visible = false;
                    gvData.Visible = false;
                    gvData.DataSource = null;
                    gvData.DataBind();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updCompare, "No record found of Operator 2 for Comparison !!!!", this.Page);
                gvmismatch.DataSource = null;
                gvmismatch.DataBind();
                lblNote.Visible = false;
                gvData.Visible = false;
                gvData.DataSource = null;
                gvData.DataBind();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            gvmismatch.DataSource = null;
            gvmismatch.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.ddlCourse_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void btnMove_Click(object sender, EventArgs e)
    {
        try
        {
            string movestatus = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "and (college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "=0)" );

            string movestatus1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "and (college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "=0)" + "and move_status=1");

            //if ((movestatus != string.Empty || movestatus != "") && movestatus != "0")
            if (movestatus == movestatus1)
            {
                objCommon.DisplayMessage(updCompare, "Data already moved", this.Page);
                return;
            }

            DataSet ds = objmec.GetMarkCompare(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                objCommon.DisplayMessage(updCompare, "Data not match", this.Page);
                return;
            }
            else
            {

                //string opid = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "top 1 opid", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "and (college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "=0)");

                //int cs = objmec.MoveOperatorMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(opid), Convert.ToInt32(ddlInEx.SelectedValue));
                //if (cs == 2)
                //{
                //    objCommon.DisplayMessage(updCompare, "Mark Matched and Saved successfully", this.Page);
                //    return;
                //}
                //else
                //{
                //    objCommon.DisplayMessage(updCompare, "Error...", this.Page);
                //    return;
                //}
                DataSet dsopid = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION", "opid", "college_id", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "and (college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "=0)", string.Empty);
                if (dsopid.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i <= dsopid.Tables[0].Rows.Count - 1; i++)
                    {
                        int cs = objmec.MoveOperatorMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(dsopid.Tables[0].Rows[i]["opid"]), Convert.ToInt32(ddlInEx.SelectedValue));
                        if (cs == 2)
                        {
                            objCommon.DisplayMessage(updCompare, "Mark Matched and Saved successfully", this.Page);

                        }
                        else
                        {
                            objCommon.DisplayMessage(updCompare, "Error...", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCompare, "Mark Entry data is not available.", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.btnMove_Click-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    #region MatchReportOldCode
    //protected void btnMatchReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridView GVDayWiseAtt = new GridView();
    //        string ContentType = string.Empty;
    //        string CHKLOCK = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and ( college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "= 0)" + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));
    //        string CHKLOCK1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and ( college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " or " + Convert.ToInt32(ddlColg.SelectedValue) + "= 0)" + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + "AND ISNULL(LOCK,0)=1");


    //        if ((CHKLOCK == string.Empty || CHKLOCK == "" || CHKLOCK == "0") && (CHKLOCK1 == string.Empty || CHKLOCK1 == "" || CHKLOCK1 == "0"))
    //        {
    //            objCommon.DisplayMessage(updCompare, "Mark entry not locked by operator", this.Page);
    //            return;
    //        }
    //        else if (CHKLOCK == CHKLOCK1)
    //        {
    //            string countop = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));
    //            if (countop == string.Empty || countop == "" || countop == "0")
    //            {
    //                objCommon.DisplayMessage(updCompare, "Match Data not available", this.Page);
    //                return;
    //            }
    //            DataSet ds = objmec.GetMatchMarkCompare(Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlInEx.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                //ds.Tables[0].Columns.RemoveAt(3);
    //                GVDayWiseAtt.DataSource = ds;
    //                GVDayWiseAtt.DataBind();
    //                string attachment = "attachment; filename=Match Mark entry by oprerator.xls";
    //                Response.ClearContent();
    //                Response.AddHeader("content-disposition", attachment);
    //                Response.ContentType = "application/vnd.MS-excel";
    //                StringWriter sw = new StringWriter();
    //                HtmlTextWriter htw = new HtmlTextWriter(sw);
    //                GVDayWiseAtt.RenderControl(htw);
    //                Response.Write(sw.ToString());
    //                Response.End();
    //            }

    //            else
    //            {
    //                objCommon.DisplayMessage(updCompare, "Match data not available for above selection", this.Page);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updCompare, "Mark entry not locked by operator.", this.Page);
    //            gvmismatch.DataSource = null;
    //            gvmismatch.DataBind();
    //            btnUnlock.Enabled = false;
    //            btnMove.Enabled = false;
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.btnMatchReport_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //        {
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}
    #endregion

    protected void btnMatchReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();

            string CHKLOCK = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " and schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and is_mids_ends=" + Convert.ToInt32(ddlExamName.SelectedValue));
            string CHKLOCK1 = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + " and schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " and is_mids_ends=" + Convert.ToInt32(ddlExamName.SelectedValue) + " and ISNULL(LOCK,0)=1");
            string chkop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1");
            string chkop1Courses = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(DISTINCT COURSENO)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1");
            //string chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(DISTINCT COURSENO)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND (" + ddlExamName.SelectedValue == "1" ? "S3MARK" : "EXTERMARK" + ") IS NOT NULL AND (" + ddlExamName.SelectedValue == "1" ? "ISNULL(LOCKS3,0)" : "ISNULL(LOCKE,0)" + ")  = 1");
            string chklOCKop1 = string.Empty;

            if (ddlExamName.SelectedValue == "1")
                chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND S3MARK IS NOT NULL AND ISNULL(LOCKS3,0)  = 1");
            else if (ddlExamName.SelectedValue == "2")
                chklOCKop1 = objCommon.LookUp("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON A.IDNO = B.IDNO", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue) + " and a.schemeno=" + Convert.ToInt32(ddlScheme.SelectedValue) + " and a.semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(A.ACCEPTED,0) = 1 AND ISNULL(A.REGISTERED,0) = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.EXAM_REGISTERED,0) END = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND EXTERMARK IS NOT NULL AND ISNULL(LOCKE,0)  = 1");

            // FOR OPERATOR 2
            DataSet dsData = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A  INNER JOIN ACD_STUDENT_RESULT B ON (A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.SESSIONNO = B.SESSIONNO AND A.COURSENO = B.COURSENO) INNER JOIN ACD_STUDENT C ON A.SCHEMENO = C.SCHEMENO AND C.COLLEGE_ID = A.COLLEGE_ID AND B.IDNO = C.IDNO INNER JOIN ACD_COURSE D ON D.COURSENO = A.COURSENO INNER JOIN USER_ACC E ON E.UA_NO = A.OP_ID", "C.IDNO,C.REGNO AS ENROLLMENTNO,C.ROLLNO", "D.CCODE + ' - ' + COURSE_NAME AS COURSENAME,A.OP_ID,E.UA_FULLNAME AS OP", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND A.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue) + " AND ISNULL(A.LOCK,0)=0", "C.REGNO,A.COURSENO");
            // FOR OPERATOR 1
            DataSet dsData1 = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT C ON A.SCHEMENO = C.SCHEMENO AND A.IDNO = C.IDNO INNER JOIN ACD_COURSE D ON D.COURSENO = A.COURSENO LEFT JOIN USER_ACC E ON E.UA_NO = A.UA_NO", "C.IDNO,C.REGNO AS ENROLLMENTNO,C.ROLLNO", "D.CCODE + ' - ' + COURSE_NAME AS COURSENAME,A.UA_NO,CASE WHEN ISNULL(A.UA_NO,0) = 0 THEN ' - ' ELSE E.UA_FULLNAME END AS OP", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND A.SUBID = 1 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN ISNULL(A.LOCKS3,0) WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(A.LOCKE,0) END = 0 AND ISNULL(REGISTERED,0) = 1 AND ISNULL(ACCEPTED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(EXAM_REGISTERED,0) END = 1", "C.REGNO,A.COURSENO");

            if (!String.IsNullOrEmpty(CHKLOCK) && CHKLOCK.ToString() != "0")
            {
                if (!String.IsNullOrEmpty(chkop1) && chkop1.ToString() != "0")
                {
                    if (CHKLOCK == chkop1Courses)// chkop1)
                    {
                        if (!String.IsNullOrEmpty(CHKLOCK1) && CHKLOCK1.ToString() != "0")
                        {
                            if (CHKLOCK == CHKLOCK1)
                            {
                                if (!String.IsNullOrEmpty(chkop1) && chkop1.ToString() != "0")
                                {
                                    if (!String.IsNullOrEmpty(chklOCKop1) && chklOCKop1.ToString() != "0")
                                    {
                                        if (chkop1 == chklOCKop1)
                                        {
                                            DataSet ds = objmec.GetMatchedMarkCompareReport(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                                GVDayWiseAtt.DataSource = ds;
                                                GVDayWiseAtt.DataBind();
                                                string attachment = "attachment; filename=Match Mark entry by operator.xls";
                                                Response.ClearContent();
                                                Response.AddHeader("content-disposition", attachment);
                                                Response.ContentType = "application/vnd.MS-excel";
                                                StringWriter sw = new StringWriter();
                                                HtmlTextWriter htw = new HtmlTextWriter(sw);
                                                GVDayWiseAtt.RenderControl(htw);
                                                Response.Write(sw.ToString());
                                                Response.End();
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(updCompare, "Match data not available for above selection !!!!", this.Page);
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(updCompare, "Mark entry not locked by Faculty / Operator 1 !!!!", this.Page);
                                            gvmismatch.DataSource = null;
                                            gvmismatch.DataBind();
                                            lblNote.Visible = false;
                                            if (dsData1.Tables[0].Rows.Count > 0 && dsData1 != null)
                                            {
                                                gvData.Visible = true;
                                                gvData.DataSource = dsData1;
                                                gvData.DataBind();
                                            }
                                            else
                                            {
                                                gvData.Visible = false;
                                                gvData.DataSource = null;
                                                gvData.DataBind();
                                            }
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(updCompare, "Mark entry not locked by Faculty / Operator 1 !!!!", this.Page);
                                        gvmismatch.DataSource = null;
                                        gvmismatch.DataBind();
                                        lblNote.Visible = false;
                                        if (dsData1.Tables[0].Rows.Count > 0 && dsData1 != null)
                                        {
                                            gvData.Visible = true;
                                            gvData.DataSource = dsData1;
                                            gvData.DataBind();
                                        }
                                        else
                                        {
                                            gvData.Visible = false;
                                            gvData.DataSource = null;
                                            gvData.DataBind();
                                        }
                                        return;
                                    }

                                }
                                else
                                {
                                    objCommon.DisplayMessage(updCompare, "No record found for Faculty / Operator 1 for the selection !!!!", this.Page);
                                    gvmismatch.DataSource = null;
                                    gvmismatch.DataBind();
                                    lblNote.Visible = false;
                                    gvData.Visible = false;
                                    gvData.DataSource = null;
                                    gvData.DataBind();
                                    return;
                                }

                            }
                            else
                            {
                                objCommon.DisplayMessage(updCompare, "Mark entry not locked by Operator 2 !!!!", this.Page);
                                gvmismatch.DataSource = null;
                                gvmismatch.DataBind();
                                lblNote.Visible = false;
                                if (dsData.Tables[0].Rows.Count > 0 && dsData != null)
                                {
                                    gvData.Visible = true;
                                    gvData.DataSource = dsData;
                                    gvData.DataBind();
                                }
                                else
                                {
                                    gvData.Visible = false;
                                    gvData.DataSource = null;
                                    gvData.DataBind();
                                }
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCompare, "Mark entry not locked by Operator 2 !!!!", this.Page);
                            gvmismatch.DataSource = null;
                            gvmismatch.DataBind();
                            lblNote.Visible = false;
                            if (dsData.Tables[0].Rows.Count > 0 && dsData != null)
                            {
                                gvData.Visible = true;
                                gvData.DataSource = dsData;
                                gvData.DataBind();
                            }
                            else
                            {
                                gvData.Visible = false;
                                gvData.DataSource = null;
                                gvData.DataBind();
                            }
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCompare, "Courses for the Operator 2 allotment mismatched with Faculty / Operator 1 courses!!!!", this.Page);
                        gvmismatch.DataSource = null;
                        gvmismatch.DataBind();
                        lblNote.Visible = false;
                        gvData.Visible = false;
                        gvData.DataSource = null;
                        gvData.DataBind();
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCompare, "No record found for Faculty / Operator 1 for the above selection !!!!", this.Page);
                    gvmismatch.DataSource = null;
                    gvmismatch.DataBind();
                    lblNote.Visible = false;
                    gvData.Visible = false;
                    gvData.DataSource = null;
                    gvData.DataBind();
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updCompare, "No record found of Operator 2 for Comparison !!!!", this.Page);
                gvmismatch.DataSource = null;
                gvmismatch.DataBind();
                lblNote.Visible = false;
                gvData.Visible = false;
                gvData.DataSource = null;
                gvData.DataBind();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.btnMatchReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void btnMarkcomp_Click(object sender, EventArgs e)
    {
        ShowReport("Mark_Comparision_Status_Report", "Markcomparisionstatus.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",(@P_COLLEGEID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0),@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updCompare, this.updCompare.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowCompareeport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEM=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COLLEGE_ID="+ Convert.ToInt32(ddlColg.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updCompare, this.updCompare.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCompareReport_Click(object sender, EventArgs e)
    {
        //if (ddlSession.SelectedValue == "0" || ddlDegree.SelectedValue == "0" || ddlBranch.SelectedValue == "0" || ddlScheme.SelectedValue == "0" || ddlSemester.SelectedValue == "0")
        //{
        //    objCommon.DisplayMessage(updCompare, "Please select proper fields", this.Page);
        //}
        //else
        //{
        //    ShowCompareeport("Comrape Status Report", "CompareStatusReport.rpt");
        //}
        GridView GVDayWiseAtt = new GridView();
        DataSet ds = objmec.GetCOMPSTATUSREPORT(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();
            string attachment = "attachment; filename=CompareStatus.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updCompare, "No Compare Status Found for the above selection !!!!", this.Page);
            return;
        }

    }

    protected void btnRmove_Click(object sender, EventArgs e)
    {
        string movestatus = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue) + " and isnull(move_status,0)=1");

        if ((movestatus == string.Empty || movestatus == "") || movestatus == "0")
        {
            objCommon.DisplayMessage(updCompare, "Data is not Saved properly", this.Page);
            return;
        }
        else
        {
            int cs = objmec.UpdateMoveStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlInEx.SelectedValue));
            if (cs == 2)
            {
                objCommon.DisplayMessage(updCompare, "Remove Data entry status .", this.Page);

                return;
            }
            else
            {
                objCommon.DisplayMessage(updCompare, "Error...", this.Page);
                return;
            }

        }
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvmismatch.DataSource = null;
        gvmismatch.DataBind();
        gvData.DataSource = null;
        gvData.DataBind();
    }
}
