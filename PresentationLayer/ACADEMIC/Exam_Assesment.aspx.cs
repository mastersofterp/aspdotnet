//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : EXAM COMPONENT                                                    
// CREATION DATE : 15-JULLY-2022                                                         
// CREATED BY    : NIKHIL SHENDE                                
// MODIFIED DATE :                                                          
// MODIFIED DESC :                                                                      
//======================================================================================
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
using System.Collections.Generic;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Exam_Assesment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamComponentConroller objexam = new ExamComponentConroller();

    #region MasterPage

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage

        if (Session["masterpage"] != null)

            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else

            objCommon.SetMasterPage(Page, "");
    }

    #endregion End MasterPage set

    #region Page Load

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
                //CheckPageAuthorization();

                //Set the Page Title

                Page.Title = Session["coll_name"].ToString();

                // CHECK THE STUDENT LOGIN

                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                //
            }

            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //Populate the Drop Down Lists
            //PopulateDropDownList();
            PopulateDropDownDeptMap();
            // BinGridAssesment();
            //SetInitialRow();
        }
    }

    #endregion End Page Load

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

            //or
            //Client_IPAddress = Request.UserHostAddress;
            //or
            //User_IPAddress = Request.ServerVariables["REMOTE_HOST"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter,
                                                          System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];

            //User_IPAddress = IP_Array[0];
        }

        return User_IPAddress;
    }

    private bool CheckActivity()
    {
        if (Convert.ToInt32(ViewState["usertype"]) == 2)
        {
            bool ret = true;
            string sessionno = string.Empty;

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");

            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");

            ViewState["sessionno"] = sessionno;

            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage(this.Page, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7)
                    {
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
                    }
                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))

                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(this.Page, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7)
                    {
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
                    }
                }
            }
            else
            {
                //divenroll.Visible = false;
                //btnSearch.Visible = false;
                btncancel.Visible = false;
                objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
            }

            dtr.Close();
            return ret;
        }
        else
        {
            bool ret = true;
            string sessionno = string.Empty;

            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            ViewState["sessionno"] = sessionno;

            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage(this.Page, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7)
                    {
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
                    }
                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))

                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(this.Page, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7)
                    {
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;

                ret = false;
            }

            dtr.Close();
            return ret;
        }
    }

    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS="+ cid +" UNION ALL SELECT 0 AS SESSION_NO");
        //sessionno = Session["currentsession"].ToString();

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        ViewState["sessionnonew"] = sessionno;

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                //dvMain.Visible = false;
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                //dvMain.Visible = false;
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);

            //txtEnrollno.Text = string.Empty;
            // dvMain.Visible = false;

            ret = false;
        }

        dtr.Close();
        return ret;
    }

    #region Check For Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }
    #endregion End Authentication

    #region Fill DropdownList For Session
    protected void PopulateDropDownList()
    {
        //try
        //{
        //    // FILL DROPDOWN  
        //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO ASC");
        //    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID="+Convert.ToInt32(ddlCollegeIdDepMap.SelectedValue), "SESSIONNO DESC");
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }
    #endregion End DropDownlist

    #region Fill DropdownList For CourseType based on Session

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsubjecttype.SelectedIndex = 0;
        divMarksDetails.Visible = false;
        DivAdd.Visible = false;
        lvAssessment.DataSource = null;
        lvAssessment.DataBind();
        if (Convert.ToInt32(Session["OrgId"]) == 7)
        {
            #region CheckActivity

            if (CheckActivity())
            {
                //FillDropdown();
                if (ViewState["usertype"].ToString() == "2")
                {
                    int cid = 0;
                    int idno = 0;

                    idno = Convert.ToInt32(Session["idno"]);
                    cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

                    if (CheckActivityCollege(cid))
                    {
                        //CHECK ACTIVITY FOR EXAM REGISTRATION
                        //CheckActivity();

                        //txtEnrollno.Text = string.Empty;
                        //btnSearch.Visible = false;
                        btncancel.Visible = false;
                        //divCourses.Visible = true;
                        //pnlSearch.Visible = false;
                        //this.ShowDetails();
                        //bindcourses();

                        //foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        //{
                        //    int count;
                        //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        //    {

                        //      //  CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                        //      //  //HiddenField hdfexam = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                        //      //  Label lblExamType = dataitem.FindControl("lblExamType") as Label;
                        //      //  if ()
                        //      //  {
                        //            count++;
                        //        }
                        //        //else if (lblExamType.ToolTip == "0")
                        //        //{
                        //        //    chkacc.Enabled = true;
                        //        //}
                        //        //else
                        //        //{
                        //        //    chkacc.Enabled = true;
                        //        //}
                        //    }

                        // int a = lvFailCourse.Items.Count;
                        // int b = 0;
                        // foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        // {
                        //
                        //     CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        //     if (chk.Enabled == true)
                        //         b++;
                        //
                        // }
                        //if (a == b)
                        //{
                        //    btnSubmit.Enabled = false;
                        //}
                        //else
                        //{
                        //    btnSubmit.Enabled = true;
                        //}

                        // BindStudentDetails();
                    }
                }

                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                return;
            }

            #endregion CheckActivityEnd
        }
        else
        {
            if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7)
            {
                objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
            }
            else
            {
                //  objCommon.FillDropDownList(ddlsubjecttype, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                objCommon.FillDropDownList(ddlsubjecttype, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
            }
        }
    }

    #endregion  End Fill DropdownList For CourseType based on Session

    #region OndataBound for SubExam


    #endregion End

    #region College DropDownBind
    private void PopulateDropDownDeptMap()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollegeIdDepMap, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_SCHEME SC ON SM.SCHEMENO=SC.SCHEMENO INNER JOIN USER_ACC UA ON SC.DEPTNO IN (SELECT VALUE FROM DBO.SPLIT(UA.UA_DEPTNO,','))", "SM.COSCHNO", "SM.COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"].ToString() + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND UA.UA_NO=" + Session["userno"].ToString(), "COLLEGE_ID");
            
            //objCommon.FillDropDownList(ddlCollegeIdDepMap, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"].ToString() + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            //rdoDegree.SelectedIndex = 0;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion End Bind Dropdownlists

    #region Set Intial Row
    private void SetInitialRow()
    {
        //new 
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("ASSESS_COMP_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("SubExamComponent", typeof(string)));
        //dt.Columns.Add(new DataColumn("MarksOutof", typeof(string)));
        dt.Columns.Add(new DataColumn("MarksOutof1", typeof(string)));
        dt.Columns.Add(new DataColumn("Weightage", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalMark", typeof(string)));
        dt.Columns.Add(new DataColumn("Fix", typeof(string)));
        dt.Columns.Add(new DataColumn("LOCK", typeof(string)));

        dr = dt.NewRow();
        dr["ASSESS_COMP_NO"] = 0;
        dr["SubExamComponent"] = string.Empty;
        //dr["MarksOutof"] = string.Empty;
        dr["MarksOutof1"] = string.Empty;
        dr["Weightage"] = string.Empty;
        dr["TotalMark"] = string.Empty;
        dr["Fix"] = string.Empty;
        dr["LOCK"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvAssessment.DataSource = dt;
        lvAssessment.DataBind();
    }

    #endregion End Set Intial Row

    #region Add Auto Increment Column
    public void AddAutoIncrementColumn(DataTable dt)
    {
        DataColumn column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.AutoIncrement = true;
        column.AutoIncrementSeed = 0;
        column.AutoIncrementStep = 1;
        dt.Columns.Add(column);
        int index = -1;
        foreach (DataRow row in dt.Rows)
        {
            row.SetField(column, ++index);
        }
    }
    #endregion End Auto Increment Column

    #region Set Previous Data

    string ExamNo = String.Empty;
    int Flag = 0;

    private void SetPreviousData()
    {
        int rowIndex = 0;

        foreach (ListViewDataItem dataitem in lvAssessment.Items)
        {
            DropDownList ddlAssessment = dataitem.FindControl("ddlAssessment") as DropDownList;
            HiddenField hfdvalue = dataitem.FindControl("hfdvalue") as HiddenField;
            //TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
            //TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("hfdvalue");
            string SUBEXAMNO = hfdvalue.Value;

            if (SUBEXAMNO != "")
            {
                ExamNo = ExamNo + SUBEXAMNO;
            }

            objCommon.FillDropDownList(ddlAssessment, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "PATTERNNO=" + Convert.ToInt32(ViewState["PATTERNNO"].ToString()) + "AND ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()), "SUBEXAMNO");
            //objCommon.FillDropDownList(ddlAssessment, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "PATTERNNO=" + Convert.ToInt32(ViewState["PATTERNNO"].ToString()) + "AND SUBEXAM_SUBID=" + Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()) + " and SUBEXAMNO not in (" + (ExamNo) + ")", "SUBEXAMNO");

            if (Flag == 0)
            {
                ExamNo = SUBEXAMNO + ",";
                Flag = 1;
            }
        }

        if (ViewState["CurrentTable"] != null)
        {
            // DataSet dtCurrentTable = (DataSet)ViewState["CurrentTable"];
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfsrno = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfsrno");
                    HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdvalue");
                    DropDownList ddlAssessment = (DropDownList)lvAssessment.Items[rowIndex].FindControl("ddlAssessment");

                    TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
                    TextBox box2 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtWeightage");
                    Label lblFix = (Label)lvAssessment.Items[rowIndex].FindControl("lblFix");

                    Label lblTotalMarks = (Label)lvAssessment.Items[rowIndex].FindControl("lblTotalMarks");
                    HiddenField hdfIsLock = lvAssessment.Items[rowIndex].FindControl("hdfIsLock") as HiddenField;
                    lblTotalMarks.Text = dt.Rows[i]["TotalMark"].ToString();

                    hdfid.Value = dt.Rows[i]["ASSESS_COMP_NO"].ToString();
                    // int ExamNo = Convert.ToInt32(dt.Rows[i]["Sub Exam Component"].ToString());
                    ddlAssessment.SelectedValue = dt.Rows[i]["SubExamComponent"].ToString(); // ExamNo.ToString();
                    //box1.Text = dt.Rows[i]["MarksOutof"].ToString();
                    box1.Text = dt.Rows[i]["MarksOutof1"].ToString();
                    box2.Text = dt.Rows[i]["Weightage"].ToString();
                    lblFix.Text = dt.Rows[i]["Fix"].ToString();

                    if (lblFix.Text == "1")
                    {
                        ddlAssessment.Enabled = false;
                        //box1.Enabled = false;
                        //box2.Enabled = false;
                    }
                    if (hdfIsLock.Value == "True")
                    {
                        ddlAssessment.Enabled = false;
                        box1.Enabled = false;
                        box2.Enabled = false;
                        btnsubmit.Enabled = false;
                        btnLock.Enabled = false;
                        btnLock.Text = "Locked";
                    }
                    else
                    {

                        btnsubmit.Enabled = true;
                        btnLock.Enabled = true;
                        btnLock.Text = "Submit & Lock";
                    }

                    rowIndex++;
                }

                rowIndex = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int count = 0;

                    DropDownList ddlExistingModule = lvAssessment.Items[rowIndex].FindControl("ddlAssessment") as DropDownList;
                    //objCommon.FillDropDownList(ddlExistingModule, "ACD_COURSE", "DISTINCT COURSENO", "COURSE_NAME", "ISNULL(ACTIVE,0)=1", "");
                    //objCommon.FillDropDownList(ddlExistingModule, "ACD_MODULE_REVISION", "COURSENO", "(NEW_CCODE  +'-'+ NEW_COURSENAME)AS COURSENAME", "ISNULL(REVISION_STATUS,0)=1 AND DEPTNO=" + ddlDepartment.SelectedValue, "NEW_COURSENAME");

                    ddlExistingModule.SelectedValue = dt.Rows[i]["SubExamComponent"].ToString();

                    foreach (ListViewDataItem item in lvAssessment.Items)
                    {
                        DropDownList ddlExistingModule1 = lvAssessment.Items[count].FindControl("ddlAssessment") as DropDownList;
                        count++;

                        if (Convert.ToInt32(ddlExistingModule1.SelectedValue) == 0)
                        {
                            ListItem itemToRemove = ddlExistingModule.Items.FindByValue(Convert.ToString(dt.Rows[i]["SubExamComponent"].ToString()));
                            ddlExistingModule1.Items.Remove(itemToRemove);
                        }
                        else
                        {
                            if (Convert.ToString(ddlExistingModule1.SelectedValue) != Convert.ToString(dt.Rows[i]["SubExamComponent"].ToString()))
                            {
                                ListItem itemToRemove = ddlExistingModule.Items.FindByValue(Convert.ToString(dt.Rows[i]["SubExamComponent"].ToString()));
                                ddlExistingModule1.Items.Remove(itemToRemove);
                            }
                        }
                    }

                    rowIndex++;
                }
            }
        }
        else
        {
            SetInitialRow();
        }
    }

    #endregion End Data

    decimal totalMarks = 0;

    protected void ddlsubjecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
  
        if (ddlsubjecttype.SelectedValue != "0")
        {
            SetInitialRow();

            ViewState["PATTERNNO"] = 0;
            ViewState["SUBEXAM_SUBID"] = 0;
            ViewState["SCHEMENO"] = 0;

            DataSet ds = objCommon.FillDropDown("ACD_SUBEXAM_NAME SN INNER JOIN ACD_COURSE C ON(SN.SUBEXAM_SUBID=C.SUBID)  INNER JOIN ACD_SCHEME S ON(S.SCHEMENO=C.SCHEMENO and sn.PATTERNNO=s.PATTERNNO)", "DISTINCT S.SCHEMENO", " TOTAL_MARK AS OVERALL,MAXMARKS_I AS INTERNAL_WEIGHT,sn.PATTERNNO,SUBEXAM_SUBID,MAXMARKS_E AS EXTERNAL_WEIGHT,MINMARKS AS MIN_INTERNAL,MINMARK_I AS MIN_EXTERNAL", "C.COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue), "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCaPer1.Text = ds.Tables[0].Rows[0]["INTERNAL_WEIGHT"].ToString();
                txtOverall.Text = ds.Tables[0].Rows[0]["OVERALL"].ToString();
                txtFinal.Text = ds.Tables[0].Rows[0]["EXTERNAL_WEIGHT"].ToString();
                txtMinCa.Text = ds.Tables[0].Rows[0]["MIN_INTERNAL"].ToString();
                txtMinFinal.Text = ds.Tables[0].Rows[0]["MIN_EXTERNAL"].ToString();

                ViewState["PATTERNNO"] = ds.Tables[0].Rows[0]["PATTERNNO"].ToString();
                ViewState["SUBEXAM_SUBID"] = ds.Tables[0].Rows[0]["SUBEXAM_SUBID"].ToString();
                ViewState["SCHEMENO"] = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();

                divMarksDetails.Visible = true;
                DivAdd.Visible = true;

                if (Convert.ToInt32(ViewState["usertype"]) == 1 || Convert.ToInt32(ViewState["usertype"]) == 7)
                {
                    btnReport.Visible = true;
                }
                else
                {
                    btnReport.Visible = false;
                }
            }
            else
            {
                divMarksDetails.Visible = false;
                DivAdd.Visible = false;
                lvAssessment.DataSource = null;
                lvAssessment.DataBind();
            }

            if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7)
            {
                // ViewState["SEMESTERNO"] = objCommon.LookUp("ACD_COURSE_TEACHER", "SEMESTERNO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue)); //COMMENT BY GAURAV 13_12_2022 SEMESTER NO GETTING NULL 


                //    ViewState["SEMESTERNO"] = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue));     /////COMMENT BY prafull  21_08_2023 


                ViewState["SEMESTERNO"] = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue));

            }
            else
            {
                //ViewState["SEMESTERNO"] = objCommon.LookUp("ACD_COURSE_TEACHER", "SEMESTERNO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue) + " AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString())); //COMMENT BY GAURAV 13_12_2022 SEMESTER NO GETTING NULL 
                ViewState["SEMESTERNO"] = objCommon.LookUp("ACD_STUDENT_RESULT", " DISTINCT SEMESTERNO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue) + " AND (UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "OR UA_NO_PRAC=" + Convert.ToInt32(Session["userno"].ToString())+")");
            }

            //divMarksDetails.Visible = true;
            //DivAdd.Visible = true;

            foreach (ListViewDataItem dataitem in lvAssessment.Items)
            {
                DropDownList ddlAssessment = dataitem.FindControl("ddlAssessment") as DropDownList;
                objCommon.FillDropDownList(ddlAssessment, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "PATTERNNO=" + Convert.ToInt32(ViewState["PATTERNNO"].ToString()) + "AND ISNULL(ACTIVESTATUS,0)=1  AND SUBEXAM_SUBID=" + Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()), "SUBEXAMNO");
            }

            //DataSet dss = objCommon.FillDropDown("ACD_ASSESSMENT_EXAM_COMPONENT", "SESSIONNO AS ASSESS_COMP_NO ", "SUBEXAMNO AS SubExamComponent,MARKOUTOF AS MarksOutof,WEIGHTAGE AS Weightage,ISNULL(FIXCOMP,0) AS Fix ", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue) + " AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND ISNULL(CANCLE,0)=0", "ASSES_COMP_NO");
            //DataSet dss = objCommon.FillDropDown("ACD_ASSESSMENT_EXAM_COMPONENT AEC inner join ACD_SUBEXAM_NAME SN on AEC.SUBEXAMNO=sn.SUBEXAMNO", "SESSIONNO AS ASSESS_COMP_NO ", "sn.SUBEXAMNO AS SubExamComponent,MARKOUTOF AS MarksOutof1,WEIGHTAGE AS Weightage,ISNULL(FIXCOMP,0) AS Fix,sn.SUBEXAMNAME,sn.MAXMARK AS MarksOutof,sn.FIXED", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue) + " AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND ISNULL(CANCLE,0)=0 and sn.FIXED=1", "ASSES_COMP_NO");

           // DataSet dss = objCommon.FillDropDown("ACD_ASSESSMENT_EXAM_COMPONENT AEC inner join ACD_SUBEXAM_NAME SN on AEC.SUBEXAMNO=sn.SUBEXAMNO", "SESSIONNO AS ASSESS_COMP_NO ", "sn.SUBEXAMNO AS SubExamComponent,MARKOUTOF AS MarksOutof1,WEIGHTAGE AS Weightage,ISNULL(FIXCOMP,0) AS Fix,sn.SUBEXAMNAME,sn.MAXMARK AS MarksOutof,sn.FIXED,AEC.TotalMark", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue) + " AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND ISNULL(CANCLE,0)=0 and sn.FIXED=1 union all select SESSIONNO AS ASSESS_COMP_NO,sn.SUBEXAMNO AS SubExamComponent,MARKOUTOF AS MarksOutof1,WEIGHTAGE AS Weightage,ISNULL(FIXCOMP,0) AS Fix,sn.SUBEXAMNAME,sn.MAXMARK AS MarksOutof,sn.FIXED,AEC.TotalMark from ACD_ASSESSMENT_EXAM_COMPONENT AEC inner join ACD_SUBEXAM_NAME SN on AEC.SUBEXAMNO=sn.SUBEXAMNO where SESSIONNO= " + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + " AND COURSENO=" + ddlsubjecttype.SelectedValue + " AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + " AND ISNULL(CANCLE,0)=0 and ISNULL(sn.FIXED,0)=0", "SESSIONNO");

            string p = "@P_COLLEGE_ID,@P_SESSIONNO,@P_COURSENO,@P_UANO";
            string c = "" + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsubjecttype.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString());
            string n = "PKG_ACD_GET_EXAM_COMPONENT";
            DataSet dss = objCommon.DynamicSPCall_Select(n, p, c);

            decimal TotalM = 0;
            decimal Mark = 0;

            if (dss.Tables[0].Rows.Count > 0)
            {
                //ViewState["CurrentTable"] = dss;
                //SetPreviousData();

                lvAssessment.DataSource = dss;
                lvAssessment.DataBind();

                foreach (ListViewDataItem dataitem in lvAssessment.Items)
                {
                    DropDownList ddlAssessment = dataitem.FindControl("ddlAssessment") as DropDownList;
                    objCommon.FillDropDownList(ddlAssessment, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "PATTERNNO=" + Convert.ToInt32(ViewState["PATTERNNO"].ToString()) + "AND ISNULL(ACTIVESTATUS,0)=1  AND SUBEXAM_SUBID=" + Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()), "SUBEXAMNO");
                }

                //decimal TotalM = 0;

                foreach (ListViewDataItem dataitem in lvAssessment.Items)
                {
                    DropDownList ddlAssessment = dataitem.FindControl("ddlAssessment") as DropDownList;
                    HiddenField hfdvalue = dataitem.FindControl("hfdvalue") as HiddenField;
                    TextBox txtOutOfMarks = dataitem.FindControl("txtOutOfMarks") as TextBox;
                    TextBox txtWeightage = dataitem.FindControl("txtWeightage") as TextBox;
                    Label lblFix = dataitem.FindControl("lblFix") as Label;
                    HiddenField hdfIsLock = dataitem.FindControl("hdfIsLock") as HiddenField;
                    //Label lblTotalMarks = (Label)lvAssessment.Items[rowIndex].FindControl("lblTotalMarks");
                    Label lblTotalMarks = dataitem.FindControl("lblTotalMarks") as Label;

                    //Label lblTotalM = dataitem.FindControl("lblTotalM") as Label;
                    //decimal Mark = Convert.ToDecimal(lblTotalMarks.Text);

                    //totalMarks = totalMarks + Mark;

                    //ViewState["totalMarks"] = Convert.ToDecimal(ViewState["totalMarks"].ToString()) + totalMarks;
                    //lblTotalM.Text = Math.Round(Convert.ToDecimal(totalMarks), 2).ToString();

                    TotalM = Math.Round(Convert.ToDecimal(totalMarks), 2);

                    //lblTotalM.Text = ViewState["totalMarks"].ToString();

                    //ViewState["Total"] = Convert.ToDecimal(ViewState["Total"].ToString()) + totalMarks;

                    //lblTotalMarks.Text = Math.Round(Convert.ToDecimal(TotalMarks), 2).ToString();
                    //lblTotalM.Text = Math.Round(Convert.ToDecimal(totalMarks), 2).ToString();

                    if (lblFix.Text == "1")
                    {
                        ddlAssessment.Enabled = false;
                        //txtOutOfMarks.Enabled = false;
                        //txtWeightage.Enabled = false;

                        if (txtWeightage.Text == "")
                        {
                            txtWeightage.Text = "100";

                            decimal OutOfMarks = Convert.ToDecimal(txtOutOfMarks.Text);
                            decimal weightagemarks = Convert.ToDecimal(txtWeightage.Text);
                            decimal TotalMarks = OutOfMarks * (weightagemarks / 100);

                            //Label lblTotalMarks = (Label)lvAssessment.Items[rowIndex].FindControl("lblTotalMarks");
                            lblTotalMarks.Text = Math.Round(Convert.ToDecimal(TotalMarks), 2).ToString();
                            
                        }
                        else
                        {

                        }

                    }

                    Label lblTotalM = dataitem.FindControl("lblTotalM") as Label;

                    if (lblTotalMarks.Text != "")
                    {
                        Mark = Convert.ToDecimal(lblTotalMarks.Text);
                    }
                    else
                    {
                        Mark = Convert.ToDecimal(0);
                    }

                    totalMarks = totalMarks + Mark;

                    TotalM = Math.Round(Convert.ToDecimal(totalMarks), 2);

                    ddlAssessment.SelectedValue = (hfdvalue.Value);

                    if (hdfIsLock.Value == "True")
                    {
                        ddlAssessment.Enabled = false;
                        txtWeightage.Enabled = false;
                        txtOutOfMarks.Enabled = false;
                        btnsubmit.Enabled = false;
                        btnLock.Enabled = false;
                        btnLock.Text = "Locked";
                    }
                    else
                    {

                        btnsubmit.Enabled = true;
                        btnLock.Enabled = true;
                        btnLock.Text = "Submit & Lock";
                    }
                }

                //Label lblTotalM = (sender)lvAssessment.FindControl(lblTotalM);

                Label lblTtalM = lvAssessment.FindControl("lblTotalM") as Label;
                lblTtalM.Text = TotalM.ToString();
                // TotalM = lblTtalM.ToString();

                ViewState["totalM"] = lblTtalM.Text;
            }
            else
            {
                ViewState["totalM"] = 0;
                //decimal Mark = 0;

                Label lblTtalM = lvAssessment.FindControl("lblTotalM") as Label;
                lblTtalM.Text = TotalM.ToString();
                // TotalM = lblTtalM.ToString();

                ViewState["totalM"] = lblTtalM.Text;

                DataSet ExamData = objCommon.FillDropDown("ACD_SUBEXAM_NAME", "SUBEXAMNO AS SubExamComponent", "MAXMARK AS MarksOutof1,FIXED as Fix,'' as Weightage,'' as TotalMark,0 as LOCK", "PATTERNNO=" + Convert.ToInt32(ViewState["PATTERNNO"].ToString()) + "AND SUBEXAM_SUBID=" + Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()) + "AND FIXED=" + 1, "SUBEXAMNO");

                if (ExamData.Tables[0].Rows.Count > 0)
                {
                    lvAssessment.DataSource = ExamData;
                    lvAssessment.DataBind();

                    foreach (ListViewDataItem dataitem in lvAssessment.Items)
                    {
                        DropDownList ddlAssessment = dataitem.FindControl("ddlAssessment") as DropDownList;
                        objCommon.FillDropDownList(ddlAssessment, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "PATTERNNO=" + Convert.ToInt32(ViewState["PATTERNNO"].ToString()) + "AND ISNULL(ACTIVESTATUS,0)=1  AND SUBEXAM_SUBID=" + Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()), "SUBEXAMNO");
                    }

                    foreach (ListViewDataItem dataitem in lvAssessment.Items)
                    {
                        DropDownList ddlAssessment = dataitem.FindControl("ddlAssessment") as DropDownList;
                        HiddenField hfdvalue = dataitem.FindControl("hfdvalue") as HiddenField;
                        TextBox txtOutOfMarks = dataitem.FindControl("txtOutOfMarks") as TextBox;
                        Label lblFix = dataitem.FindControl("lblFix") as Label;
                        HiddenField HDFfIX = dataitem.FindControl("hfdvalue") as HiddenField;

                        TextBox txtWeightage = dataitem.FindControl("txtWeightage") as TextBox;
                        Label lblTotalMarks = dataitem.FindControl("lblTotalMarks") as Label;

                        if (lblFix.Text == "1")
                        {
                            ddlAssessment.SelectedValue = (hfdvalue.Value);

                            ddlAssessment.Enabled = false;
                            //txtOutOfMarks.Enabled = false;
                            //txtWeightage.Enabled = false;

                            if (txtWeightage.Text == "")
                            {
                                txtWeightage.Text = "100";

                                decimal OutOfMarks = Convert.ToDecimal(txtOutOfMarks.Text);
                                decimal weightagemarks = Convert.ToDecimal(txtWeightage.Text);
                                decimal TotalMarks = OutOfMarks * (weightagemarks / 100);

                                lblTotalMarks.Text = Math.Round(Convert.ToDecimal(TotalMarks), 2).ToString();
                                //txtWeightage_TextChanged(new object(), EventArgs.Empty);

                            }
                            else
                            {

                            }
                        }

                        Label lblTotalM = dataitem.FindControl("lblTotalM") as Label;

                        if (lblTotalMarks.Text != "")
                        {
                            Mark = Convert.ToDecimal(lblTotalMarks.Text);
                        }
                        else
                        {
                            Mark = Convert.ToDecimal(0);
                        }

                        totalMarks = totalMarks + Mark;

                        TotalM = Math.Round(Convert.ToDecimal(totalMarks), 2);
                    }

                    Label lblTtalM1 = lvAssessment.FindControl("lblTotalM") as Label;
                    lblTtalM1.Text = TotalM.ToString();
                }
            }
            txtWeightage_TextChanged(new object(), EventArgs.Empty);
        }
        else
        {
            divMarksDetails.Visible = false;
            DivAdd.Visible = false;
            lvAssessment.DataSource = null;
            lvAssessment.DataBind();
        }
       
        //SetInitialRow();
        //List<string> listSubId = new List<string>();
        //// listSubId.Add(ddlsubjecttype.SelectedValue);
        //listSubId.Add(ddlsubjecttype.SelectedValue);
        //lvAssessment.DataSource = listSubId;
        //lvAssessment.DataBind();

        //List<string> listSubId = new List<string>();
        //listSubId.Add(ddlsubjecttype.SelectedValue);
        //lvAssessment.DataSource = listSubId;
        //lvAssessment.DataBind();
    }

    protected void ddlAssessment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet ds = (DataSet)ViewState["CurrentTable"];
        //if (ds != null)
        //{
        foreach (ListViewDataItem item in lvAssessment.Items)
        {
            string Row = item.ID;
            DropDownList ddlAssessment = item.FindControl("ddlAssessment") as DropDownList;
            TextBox txtOutOfMarks = item.FindControl("txtOutOfMarks") as TextBox;
            foreach (ListViewDataItem items in lvAssessment.Items)
            {
                string RowNO = items.ID;
                DropDownList Assessment = items.FindControl("ddlAssessment") as DropDownList;
                TextBox OutOfMarks = item.FindControl("txtOutOfMarks") as TextBox;
                if (Row != RowNO)
                {
                    if (Convert.ToInt32(ddlAssessment.SelectedValue) == Convert.ToInt32(Assessment.SelectedValue))
                    {
                        Assessment.SelectedIndex = 0;

                        objCommon.DisplayMessage(updSession, "Already Selected Sub Exam Component", this.Page);
                        return;

                    }
                }
            }
            int ExamNo = Convert.ToInt32(ddlAssessment.SelectedValue);
            if (txtOutOfMarks.Text == "")
            {
                txtOutOfMarks.Text = objCommon.LookUp("ACD_SUBEXAM_NAME", "MAXMARK", "SUBEXAMNO=" + ExamNo);
            }
            //    txtOutOfMarks.Text = ds.Tables[0].Rows[1]["MAXMARK"].ToString();
            //  txtOutOfMarks.Text=ds.Tables[0].Select()
            //txtOutOfMarks.Text = ds.Tables[0].AsEnumerable().Where(z => z.Field<int>("SUBEXAMNO") == Convert.ToInt32(ddlAssessment.SelectedValue.ToString())).Select(x => x.Field<decimal>("MAXMARK")).FirstOrDefault().ToString();
            //ddlAssessment.SelectedValue = ds.Tables[0].AsEnumerable().Where(z => z.Field<string>("SUBEXAMNAME") == Convert.ToInt32(ddlAssessment.SelectedValue)).Select(x => x.Field<string>("SUBEXAMNO")).FirstOrDefault().ToString();
        }
        //}
    }

    #region List Bind For ExamComponent List
    private void BindListView()
    {
        try
        {
            DataSet ds = objexam.GetAllAssesment();
            lvAssessmentComponent.DataSource = ds;
            lvAssessmentComponent.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Exam_Assesment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion  List Bind View

    #region List Bind For Exam Component
    private void BinGridAssesment()
    {
        try
        {
            DataSet ds = objexam.GetAllSubExamName();
            lvAssessment.DataSource = ds;
            lvAssessment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Exam_Assesment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion  List Bind View

    #region Clear Data
    protected void Clear()
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Clear Data

    #region Add Assesment button Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                if (ddlSession.SelectedIndex == 0 && ddlsubjecttype.SelectedIndex == 0 && ddlCollegeIdDepMap.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updSession, "Please Select  College, Session and Course/Subject", this.Page);
                }
                else
                {
                    //int Componentno = Convert.ToInt32(objCommon.LookUp("EXAM_COMPONENT5 ", "COUNT(EXAMNO)", "EXAMNO>0 AND COMPONENTNO <> 2"));
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    // DataSet dtCurrentTable = (DataSet)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    //if (dtCurrentTable.Rows.Count > 0 )
                    if (lvAssessment.Items.Count > 0)
                    {
                        DataTable dtNewTable = new DataTable();
                        dtNewTable.Columns.Add(new DataColumn("ASSESS_COMP_NO", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("SubExamComponent", typeof(string)));
                        //dtNewTable.Columns.Add(new DataColumn("MarksOutof", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("MarksOutof1", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("Weightage", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("TotalMark", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("Fix", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("LOCK", typeof(string)));

                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["ASSESS_COMP_NO"] = 0;
                        drCurrentRow["SubExamComponent"] = string.Empty;
                        //drCurrentRow["MarksOutof"] = string.Empty;
                        drCurrentRow["MarksOutof1"] = string.Empty;
                        drCurrentRow["Weightage"] = string.Empty;
                        drCurrentRow["TotalMark"] = string.Empty;
                        drCurrentRow["Fix"] = "0";
                        drCurrentRow["LOCK"] = string.Empty;

                        //for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        //for (int i = 1; i <= dtCurrentTable.Tables[0].Rows.Count; i++)
                        //{
                        int ExamType = 0;
                        ViewState["TotalMarks"] = "0";
                        for (int i = 0; i < lvAssessment.Items.Count; i++)
                        {
                            //for (int i = 1; i <= dtCurrentTable.Tables[0].Rows.Count; i++)
                            //{

                            HiddenField hfsrno = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfsrno");
                            HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdvalue");
                            DropDownList ddlAssessment = (DropDownList)lvAssessment.Items[rowIndex].FindControl("ddlAssessment");


                            decimal Intertnal = Convert.ToDecimal(txtCaPer1.Text);
                            decimal External = Convert.ToDecimal(txtFinal.Text);
                            // TextBox txtAssessmentComponent = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtAssessmentComponent");
                            TextBox markoutof = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
                            TextBox weightage = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtWeightage");
                            Label lblFix = (Label)lvAssessment.Items[rowIndex].FindControl("lblFix");
                            Label lblTotalMarks = (Label)lvAssessment.Items[rowIndex].FindControl("lblTotalMarks");

                            HiddenField hdfIsLock = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hdfIsLock") as HiddenField;

                            if (ddlAssessment.SelectedIndex == 0)
                            {
                                objCommon.DisplayMessage(updSession, "Please Select Sub Exam Component.", this.Page);
                                return;
                            }
                            else if (markoutof.Text.Trim() == string.Empty)
                            {
                                objCommon.DisplayMessage(updSession, "Please Enter Out Of Marks", this.Page);
                                return;
                            }
                            else if (weightage.Text.Trim() == string.Empty)
                            {
                                objCommon.DisplayMessage(updSession, "Please Enter Weightage", this.Page);
                                return;
                            }
                            else
                            {
                                drCurrentRow = dtNewTable.NewRow();
                                drCurrentRow["ASSESS_COMP_NO"] = hdfid.Value;
                                drCurrentRow["SubExamComponent"] = ddlAssessment.Text;
                                //drCurrentRow["MarksOutof"] = markoutof.Text;
                                drCurrentRow["MarksOutof1"] = markoutof.Text;
                                drCurrentRow["Weightage"] = weightage.Text;
                                drCurrentRow["TotalMark"] = lblTotalMarks.Text;
                                drCurrentRow["Fix"] = lblFix.Text;
                                drCurrentRow["Lock"] = hdfIsLock.Value;

                                if (hdfIsLock.Value == "True")
                                {
                                    ddlAssessment.Enabled = false;
                                    markoutof.Enabled = false;
                                    weightage.Enabled = false;
                                    btnsubmit.Enabled = false;
                                    btnLock.Enabled = false;
                                    btnLock.Text = "Locked";
                                    objCommon.DisplayMessage(updSession, "Can not add Assesment after its been Locked", this.Page);
                                    return;
                                }
                                else
                                {

                                    btnsubmit.Enabled = true;
                                    btnLock.Enabled = true;
                                    btnLock.Text = "Submit & Lock";
                                }

                                rowIndex++;
                                dtNewTable.Rows.Add(drCurrentRow);
                            }

                            ExamType = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO)", "EN.EXAMTYPE", "SUBEXAMNO=" + Convert.ToInt32(ddlAssessment.Text)));
                            if (ExamType == 1 || ExamType == 0)
                            {
                                decimal OutOfMarks = Convert.ToDecimal(markoutof.Text);
                                decimal weightagemarks = Convert.ToDecimal(weightage.Text);
                                decimal TotalMarks = OutOfMarks * (weightagemarks / 100);

                                ViewState["TotalMarks"] = Math.Round(Convert.ToDecimal(ViewState["TotalMarks"].ToString()) + TotalMarks);
                                if (Convert.ToInt32(Session["OrgId"]) != 8 && (Convert.ToInt32(Session["OrgId"]) != 6 && Convert.ToInt32(ViewState["degreeno"]) !=5 ) )
                                {
                                    if (Intertnal < Convert.ToDecimal(ViewState["TotalMarks"].ToString()))
                                    {
                                        decimal marks = Convert.ToDecimal(ViewState["TotalMarks"].ToString());
                                        ViewState["TotalMarks"] = "0";
                                        objCommon.DisplayMessage(updSession, "Internal Marks is" + " " + Intertnal + " " + " And your Weightage is" + " " + marks + " " + "Weightage cannot be greater than Internal mark.Please Enter Proper Weightage", this.Page);
                                        return;
                                    }
                                }

                            }
                            else if (ExamType == 2)
                            {
                                decimal OutOfMarks = Convert.ToDecimal(markoutof.Text);
                                decimal weightagemarks = Convert.ToDecimal(weightage.Text);
                                decimal TotalMarks = OutOfMarks * (weightagemarks / 100);
                                if (External < TotalMarks)
                                {
                                    if (Convert.ToInt32(Session["OrgId"]) != 8 && (Convert.ToInt32(Session["OrgId"]) != 6 && Convert.ToInt32(ViewState["degreeno"]) != 5))
                                    {
                                        objCommon.DisplayMessage(updSession, "External Marks is" + " " + External + " " + " And your Weightage is" + " " + TotalMarks + " " + "Weightage cannot be greater than External marks.Please Enter Proper Weightage", this.Page);
                                        return;
                                    }
                                }

                            }
                        }


                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["ASSESS_COMP_NO"] = 0;
                        drCurrentRow["SubExamComponent"] = string.Empty;
                        //drCurrentRow["MarksOutof"] = string.Empty;
                        drCurrentRow["MarksOutof1"] = string.Empty;
                        drCurrentRow["Weightage"] = string.Empty;
                        drCurrentRow["TotalMark"] = string.Empty;
                        drCurrentRow["Fix"] = "0";
                        drCurrentRow["Lock"] = string.Empty;
                        dtNewTable.Rows.Add(drCurrentRow);

                        ViewState["CurrentTable"] = dtNewTable;
                        lvAssessment.DataSource = dtNewTable;
                        lvAssessment.DataBind();
                        //SetPreviousData();
                        //divSubmit.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updSession, "Maximum Options Limit Reached", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updSession, "Error!!!", this.Page);
            }
            // SetPreviousData();
        }
        catch (Exception ex)
        {

        }
        SetPreviousData();
    }
    #endregion End Button Click

    #region Submit button click

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveOrLock(0);
    }
    private void SaveOrLock(int isLock)
    {
        ExamComponentConroller objExamDa = new ExamComponentConroller();

        if (ddlCollegeIdDepMap.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select College ", this.Page);
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select  Session.", this.Page);
            return;
        }
        else if (ddlsubjecttype.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updSession, "Please Select Course / Subject ", this.Page);
            return;
        }

        string que_out1 = "";
        string ExamNo = "";
        string OutOffmarks = "";
        string Weightage = "";
        string SrNo = "";
        string TotalMark = "";

        CustomStatus cs = 0;
        ViewState["Total"] = "0";

        foreach (ListViewDataItem item in lvAssessment.Items)
        {
            HiddenField hfsrno = item.FindControl("hfsrno") as HiddenField;
            TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
            DropDownList ddlAssessment = item.FindControl("ddlAssessment") as DropDownList;
            TextBox txtOutOfMarks = item.FindControl("txtOutOfMarks") as TextBox;
            Label lblTotalMarks = item.FindControl("lblTotalMarks") as Label;
            //TextBox txtAssessmentComponent = item.FindControl("txtAssessmentComponent") as TextBox;

            if (ddlAssessment.SelectedItem.Text == "Please Select")
            {
                objCommon.DisplayMessage(updSession, "Please Select Sub Exam Component.", this.Page);
                return;
            }
            // if (txtAssessmentComponent.Text.Trim() == string.Empty)
            //{
            //    objCommon.DisplayMessage(updSession, "Please Select Assesment Name", this.Page);
            //    return;
            //}
            if (txtOutOfMarks.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(updSession, "Please Enter Out Of Marks", this.Page);
                return;
            }

            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                if (txtOutOfMarks.Text.Trim() == "" || (txtOutOfMarks.Text.Trim() == ""))
                {
                    objCommon.DisplayMessage(updSession, "Please Enter Out Of Marks Properly", this.Page);
                    return;
                }
                if (txtWeightage.Text.Trim() == string.Empty)
                {
                    objCommon.DisplayMessage(updSession, "Please Enter Weightage", this.Page);
                    return;
                }
                if (txtWeightage.Text.Trim() == "" || (txtWeightage.Text.Trim() == ""))
                {
                    objCommon.DisplayMessage(updSession, "Please Enter Out Of Weightage Properly", this.Page);
                    return;
                }
            }
            else
            {
                if (txtOutOfMarks.Text.Trim() == "0" || (txtOutOfMarks.Text.Trim() == "0.00"))
                {
                    objCommon.DisplayMessage(updSession, "Please Enter Out Of Marks Properly", this.Page);
                    return;
                }
                if (txtWeightage.Text.Trim() == string.Empty)
                {
                    objCommon.DisplayMessage(updSession, "Please Enter Weightage", this.Page);
                    return;
                }
                if (txtWeightage.Text.Trim() == "0" || (txtWeightage.Text.Trim() == "0.00"))
                {
                    objCommon.DisplayMessage(updSession, "Please Enter Out Of Weightage Properly", this.Page);
                    return;
                }
            }

            ExamNo += Convert.ToInt32(ddlAssessment.SelectedValue) + ",";
            OutOffmarks += (txtOutOfMarks.Text) + ",";
            Weightage += (txtWeightage.Text) + ",";
            SrNo += Convert.ToInt32(hfsrno.Value);
            TotalMark += lblTotalMarks.Text + ",";

            decimal Internal = Convert.ToDecimal(txtCaPer1.Text);
            decimal External = Convert.ToDecimal(txtFinal.Text);

            //int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_NAME", "EXAMTYPE", "EXAMNO=" + Convert.ToInt32(ddlAssessment.SelectedValue)));
            int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO)", "EN.EXAMTYPE", "SUBEXAMNO=" + Convert.ToInt32(ddlAssessment.SelectedValue)));

            if (ExamType == 1 || ExamType == 0)
            {
                decimal OutOfMarks = Convert.ToDecimal(txtOutOfMarks.Text);
                decimal weightagemarks = Convert.ToDecimal(txtWeightage.Text);
                decimal TotalMarks = OutOfMarks * (weightagemarks / 100);

                ViewState["Total"] =Convert.ToDecimal(ViewState["Total"].ToString()) + TotalMarks;

                if (Convert.ToInt32(Session["OrgId"]) != 8 && (Convert.ToInt32(Session["OrgId"]) != 6 && Convert.ToInt32(ViewState["degreeno"]) != 5))
                {
                    if (Internal < Convert.ToDecimal(ViewState["Total"].ToString()))
                    {
                        string marks = (ViewState["Total"].ToString());
                        ViewState["Total"] = "0";
                        objCommon.DisplayMessage(updSession, "Internal Marks is" + " " + Internal + " " + " And your Weightage is" + " " + marks + " " + "Weightage cannot be greater than Internal marks.Please Enter Proper Weightage", this.Page);
                        return;
                    }
                }
            }
            else if (ExamType == 2)
            {
                decimal OutOfMarks = Convert.ToDecimal(txtOutOfMarks.Text);
                decimal weightagemarks = Convert.ToDecimal(txtWeightage.Text);
                decimal TotalMarks = OutOfMarks * (weightagemarks / 100);

                if (External < TotalMarks)
                {
                    if (Convert.ToInt32(Session["OrgId"]) != 8 && (Convert.ToInt32(Session["OrgId"]) != 6 && Convert.ToInt32(ViewState["degreeno"]) != 5))
                    {
                        objCommon.DisplayMessage(updSession, "External Marks is" + " " + External + " " + " And your Weightage is" + " " + TotalMarks + " " + "Weightage can not be greater than External mark.Please Enter Proper Weightage", this.Page);
                        return;
                    }
                }
            }

            //objCommon.DisplayUserMessage(updSession, "Exam Component Added Successfully.", this.Page);

            ////ddlAssessment.SelectedIndex = -1;
            //ddlAssessment.SelectedIndex = 0;
            //txtWeightage.Text = "";
            //txtOutOfMarks.Text = "";
            //SetInitialRow();
        }
        cs = (CustomStatus)objExamDa.Insert_Exam_Components_Details(SrNo, ExamNo, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlsubjecttype.SelectedValue), OutOffmarks, Weightage, (txtCaPer1.Text), (txtFinal.Text), (txtOverall.Text), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["SEMESTERNO"].ToString()), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ViewState["SUBEXAM_SUBID"].ToString()), TotalMark, isLock);

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updSession, "Exam Component Added Successfully.", this.Page);
            ddlsubjecttype_SelectedIndexChanged(new object(), new EventArgs());

            // BindListViewRuleAllocation();
            //BindListView();
            //ClearDDL();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.updSession, "Failed To Save Record ", this.Page);
        }
    }

    #endregion End Submit button

    #region Delete Button Click

    protected void imgbtnDeleteAssessmentNew_Click1(object sender, ImageClickEventArgs e)
    {
        ExamComponentConroller objexamRemo = new ExamComponentConroller();
        ImageButton imgbtn = sender as ImageButton;
        ListViewDataItem item = imgbtn.NamingContainer as ListViewDataItem;
        //TextBox txtRemark = item.FindControl("txtRemark") as TextBox;

        int assessment_no = 0;
        //assessment_no=Convert.ToInt32(imgbtn.CommandArgument);
        DropDownList ddlAssessment = item.FindControl("ddlAssessment") as DropDownList;
        TextBox box1 = (TextBox)item.FindControl("txtOutOfMarks");
        TextBox box2 = (TextBox)item.FindControl("txtWeightage");
        Label lblFix = (Label)item.FindControl("lblFix");
        HiddenField hdfExam = (HiddenField)item.FindControl("hfdvalue");

        Label lblTotalMarks = (Label)item.FindControl("lblTotalMarks");
        //lblTotalMarks.Text = Math.Round(Convert.ToDecimal(TotalMarks), 2).ToString();

        //ViewState["TotalMarks"] = Convert.ToDecimal(ViewState["TotalMarks"].ToString()) + TotalMarks;
        HiddenField hdfisLock = (HiddenField)item.FindControl("hdfIsLock");
        if (hdfisLock.Value == "True")
        {
            objCommon.DisplayMessage(updSession, "Locked assessments cannot be removed", this.Page);
            return;
        }

        if (lblFix.Text == "1")
        {
            objCommon.DisplayMessage(updSession, "Fixed components cannot be removed!", this.Page);
            return;
        }


        if (lblTotalMarks.Text != "")
        {
            ViewState["totalM"] = Convert.ToDecimal(ViewState["totalM"].ToString()) - Convert.ToDecimal(lblTotalMarks.Text);
        }

        Label lblTtalM2 = lvAssessment.FindControl("lblTotalM") as Label;
        lblTtalM2.Text = ViewState["totalM"].ToString();

        ViewState["totalM"] = lblTtalM2.Text;

        //TextBox txtAssessmentComponent = (TextBox)item.FindControl("txtAssessmentComponent");
        int ExamNo = 0;

        if (Convert.ToString(hdfExam.Value) != "0")
        {
            ExamNo = int.Parse(imgbtn.CommandArgument);
            ViewState["Examno"] = ExamNo;
        }

        assessment_no = Convert.ToInt32(ddlAssessment.SelectedValue);
        string[] splitValue;
        splitValue = ddlsubjecttype.SelectedItem.Text.Split('-');

        string FieldName = objCommon.LookUp("ACD_SUBEXAM_NAME", "FLDNAME+''+'MARK'", "SUBEXAMNO=" + ExamNo);
        int assessment_count = 0;
        string ExamMarks = "";
        string SP_Name2 = "PKG_ACAD_GET_SUBEXAMNAME_MARKS";
        string SP_Parameters2 = "@P_SUBEXAMNO,@P_SESSIONNO,@P_COURSENO,@P_UANO";
        string Call_Values2 = "" + ExamNo + "," + ddlSession.SelectedValue + "," + ddlsubjecttype.SelectedValue + "," + (Session["userno"].ToString()) + "";
        DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

        try
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                DataRow drs = ds1.Tables[0].Rows[0];
                ExamMarks = drs["MARKS"].ToString();
            }
        }
        catch
        {

        }
        CustomStatus cs1 = 0;
        cs1 = (CustomStatus)objexamRemo.CheckEntrydoneornot(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlsubjecttype.SelectedValue), ExamNo, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["usertype"]));
        if(cs1.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updSession, "Mark Entry Done.Exam Component cannot be Remove..  ", this.Page);
        }
        else
        {
        
        assessment_count = Convert.ToInt32(objCommon.LookUp("ACD_ASSESSMENT_EXAM_COMPONENT", "count(COURSENO)", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ddlsubjecttype.SelectedValue) + "AND SUBEXAMNO=" + ExamNo));

        if (assessment_count > 0)
        {
            CustomStatus cs = 0;
            if (ExamMarks.ToString() == "")
            {
                cs = (CustomStatus)objexamRemo.CancleExamComponents(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlsubjecttype.SelectedValue), ExamNo, Convert.ToInt32(Session["userno"].ToString()));
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Unable to remove. The mark entry has already been completed for this component.", this.Page);
                //This Sub Exam Component Already Mark Entry Done,Not Removed..
                return;
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updSession, "Exam Component Remove..", this.Page);
                ddlsubjecttype_SelectedIndexChanged(new object(), new EventArgs());
            }


        }
        else
        {
            //LinkButton lb = (LinkButton)sender;
            ImageButton lb = (ImageButton)sender;

            ListViewDataItem gvRow = (ListViewDataItem)lb.NamingContainer;
            //int rowID = gvRow.RowIndex;

            int rowID = gvRow.DataItemIndex;
            //int rowID = gvRow.DataItemIndex + 1;
            if (ViewState["CurrentTable"] != null)
            {
                //DataSet ds = (DataSet)ViewState["CurrentTable"];
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                //DataSet dtCurrentTable = (DataSet)ViewState["CurrentTable"];
                //if (dt.Rows.Count > 1)
                //{
                //    //if (gvRow.RowIndex < dt.Rows.Count )
                //    if (gvRow.DataItemIndex < dt.Rows.Count - 1)
                //    {
                //        //Remove the Selected Row data
                //        dt.Rows.Remove(dt.Rows[rowID]);
                //    }
                //}
                if (lblFix.Text != "1")
                {

                    if (dt.Rows.Count == 1)
                    {
                        //if (dt.Rows.Count == 1)
                        //{
                        //    ddlModule_SelectedIndexChanged(this, EventArgs.Empty);
                        //}

                        //if (gvRow.RowIndex < dt.Rows.Count )
                        if (gvRow.DataItemIndex <= dt.Rows.Count - 1)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }

                    if (dt.Rows.Count > 1)
                    {
                        rowID = gvRow.DataItemIndex + 1;
                        if (gvRow.DataItemIndex < dt.Rows.Count - 1)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID - 1]);
                        }
                    }


                    //if (ddlAssessment.SelectedValue == "0" && box1.Text == "" && box2.Text == "")
                    //{

                    if (dt.Rows.Count > 1)
                    {
                        rowID = gvRow.DataItemIndex;
                        if (gvRow.DataItemIndex <= dt.Rows.Count - 1)
                        {
                            //Remove the Selected Row data
                            dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }
                    //}
                    //Store the current data in ViewState for future reference
                    ViewState["CurrentTable"] = dt;
                    //Re bind the GridView for the updated data
                    lvAssessment.DataSource = dt;
                    lvAssessment.DataBind();

                    //Set Previous Data on Postbacks
                    if (rowID == 0)
                    {
                        SetInitialRow();
                        //BindListView();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updSession, "Fix Exam Component Not Remove..", this.Page);
                    return;
                }
                SetPreviousData();
            }
        }
      }

        //Added by Lalit on dated 10062023 as ticket no 42872

    }
    #endregion End Button Click

    #region ClearDropDown
    protected void ClearDDL()
    {
        ddlSession.SelectedIndex = -1;
        ddlsubjecttype.SelectedIndex = -1;
        ddlCollegeIdDepMap.SelectedIndex = -1;

    }
    #endregion

    #region cancel button
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    #endregion end cancel button

    protected void ddlCollegeIdDepMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlsubjecttype.SelectedIndex = 0;
        divMarksDetails.Visible = false;
        DivAdd.Visible = false;
        lvAssessment.DataSource = null;
        lvAssessment.DataBind();
        try
        {

            if (ddlCollegeIdDepMap.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeIdDepMap.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // FILL DROPDOWN  ddlSession_SelectedIndexChanged
                }
            }
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ISNULL (IS_ACTIVE,0)= 1", "SESSIONNO DESC");
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtWeightage_TextChanged(object sender, EventArgs e)
    {
        int rowIndex = 0;
        decimal Mark = 0;

        for (int i = 0; i < lvAssessment.Items.Count; i++)
        {
            TextBox markoutof = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
            TextBox weightage = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtWeightage");

            decimal OutOfMarks = Convert.ToDecimal(markoutof.Text == string.Empty ? "0" : markoutof.Text);
            decimal weightagemarks = Convert.ToDecimal(weightage.Text == string.Empty ? "0" : weightage.Text);
            decimal TotalMarks = OutOfMarks * (weightagemarks / 100);

            Label lblTotalMarks = (Label)lvAssessment.Items[rowIndex].FindControl("lblTotalMarks");
            lblTotalMarks.Text = Math.Round(Convert.ToDecimal(TotalMarks), 2).ToString();

            Mark = Mark + TotalMarks;

            rowIndex++;
        }

        //Label lblTotalM = (Label)lvAssessment.Items[rowIndex].FindControl("lblTotalM");
        //lblTotalM.Text = Math.Round(Convert.ToDecimal(Mark), 2).ToString();

        Label lblTtalM1 = lvAssessment.FindControl("lblTotalM") as Label;
        lblTtalM1.Text = Math.Round(Convert.ToDecimal(Mark), 2).ToString();

        ViewState["totalM"] = lblTtalM1.Text;

        Label lblTotal = lvAssessment.FindControl("lblTotalM") as Label;
        double internalW = Convert.ToDouble(txtCaPer1.Text);
        double externalW = Convert.ToDouble(txtFinal.Text);
        double total = Convert.ToDouble(lblTotal.Text);
        if (total == internalW + externalW)
        {
            btnLock.Visible = true;
        }
        else
        {
            btnLock.Visible = false;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Exam Component", "rptShowExamComponent.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        //if (ddlClgname.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
        //    ddlClgname.Focus();
        //    return;
        //}

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int branchno = Convert.ToInt32(ViewState["branchno"]);
            int CollegeId = Convert.ToInt32(ViewState["college_id"]);
            //int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "[PKG_Exam_Show_ExamComponent]";
            string SP_Parameters = "@P_SESSIONNO,@P_COLLEGE_ID";
            string Call_Values = "" + Sessionno + "," + CollegeId + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
              //  url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updSession, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        Label lblTotal = lvAssessment.FindControl("lblTotalM") as Label;
        double internalW = Convert.ToDouble(txtCaPer1.Text);
        double externalW = Convert.ToDouble(txtFinal.Text);
        double total = Convert.ToDouble(lblTotal.Text);
        if (total == internalW + externalW)
        {
            SaveOrLock(1);
        }
        else
        {
            btnLock.Visible = false;
        }
    }
}



