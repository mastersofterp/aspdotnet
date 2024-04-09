//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ATTENDANCE ENTRY BY FACULTY                 
// CREATION DATE : 18-MAR-2019                                                     
// CREATED BY    : RAJU BITODE                                       
// MODIFIED BY   : 
// MODIFICATIONS :                                                 
// MODIFIED DATE :                      
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class ACADEMIC_TIMETABLE_AttendanceEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttController = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();
    CourseController objCourse = new CourseController();
    string Section = string.Empty;
    int SubID = 0;
    #region Page events
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
                //CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Session["usertype"].ToString() == "1")
                {
                    attpanel.Visible = false;
                    divFaculty.Visible = true;
                    objCommon.FillDropDownList(ddlFaculty, "ACD_TIME_TABLE_CONFIG TTC INNER JOIN ACD_COURSE_TEACHER CT ON(TTC.CTNO = CT.CT_NO) INNER JOIN USER_ACC UA ON (CT.UA_NO = UA.UA_NO OR UA.UA_NO = CT.ADTEACHER) LEFT JOIN PAYROLL_EMPMAS PE ON (PE.IDNO = UA.UA_IDNO)", "DISTINCT UA.UA_NO", "CASE WHEN ISNULL(PFILENO,'')='' THEN UA.UA_FULLNAME ELSE CONCAT(UA.UA_FULLNAME,' - ',PFILENO) END AS UA_FULLNAME", "UA.UA_TYPE IN(3,8) AND ISNULL(UA_STATUS,0)=0", "UA_NO");
                    //Session["userno_Faculty"] = 77;
                }
                else
                {
                    attpanel.Visible = true;
                    divFaculty.Visible = false;
                    Session["userno_Faculty"] = Session["userno"];
                    //**   this.LockAttendance();
                    this.PopulateDropDownList();
                    divTutorial.Visible = true;

                    //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
                    if (ddlColgSession.SelectedValue != null && !ddlColgSession.SelectedValue.ToString().Equals("Please Select"))
                    {
                        this.GetDates();
                        this.GetCourse();
                    }
                }
            }
        }
        else
        {
            if (ddlColgSession.SelectedIndex > 0)
            {
                int college_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "COLLEGE_ID", "SESSIONNO=" + ddlColgSession.SelectedValue + ""));
                Session["college_id_att"] = college_id.ToString();
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno_Faculty"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendanceEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendanceEntry.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        try
        {
            //int TEST = Convert.ToInt32(Session["userno_Faculty"]);
            int _schemeType = (rbnNew.Checked ? 1 : 2);
            //  objCommon.FillDropDownList(ddlDegree,"ACD_DEGREE D INNER JOIN ACD_ATTENDANCE_CONFIG AC ON D.DEGREENO=AC.DEGREENO INNER JOIN ACD_COURSE_TEACHER CT ON AC.SESSIONNO","")
            string sessionno = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "DISTINCT SESSIONNO", "ISNULL(ACTIVE,0)=1 AND ISNULL(SCHEMETYPE,0)=" + _schemeType);
            objCommon.FillDropDownList(ddlClassType, "ACD_TT_CLASSTYPE_MASTER", "CLASSTYPENO", "CLASSTYPE", "", "CLASSTYPENO");
            if (sessionno == "0")
            {
                objCommon.DisplayMessage(updTeachingPlan, "There is no actived session for selected scheme type!", this.Page);
            }


            //Bind Collegre Drop Down

            DataSet dsColgSession = objCourse.GetCollegeSessionForAttendance(Convert.ToInt32(Session["userno_Faculty"]));
            if (dsColgSession.Tables[0] != null && dsColgSession.Tables[0].Rows.Count > 0)
            {
                ddlColgSession.DataSource = dsColgSession.Tables[0];
                ddlColgSession.DataTextField = dsColgSession.Tables[0].Columns["COLLEGE_SESSION"].ToString();
                ddlColgSession.DataValueField = dsColgSession.Tables[0].Columns["SESSIONNO"].ToString();
                ddlColgSession.DataBind();
                ddlColgSession.SelectedIndex = 1;

                int college_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "COLLEGE_ID", "SESSIONNO=" + ddlColgSession.SelectedValue + ""));
                Session["college_id_att"] = college_id.ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data not found!!.", this.Page);
                return;
            }

        }
        catch
        {
            throw;
        }
    }
    private bool CheckActivity()
    {
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(updTeachingPlan, "This Activity has been Stopped. Contact Administrator.!!", this.Page);
                return false;
            }

            if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(updTeachingPlan, "Pre-Requisite Activity for this Page is Not Stopped. Contact Administrator.!!", this.Page);
                    return false;
                }
        }
        else
        {
            objCommon.DisplayMessage(updTeachingPlan, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Administrator!!.", this.Page);
            return false;
        }
        dtr.Close();
        return true;
    }
    #endregion

    #region User defined Methods
    private void BindStatus()
    {
        CourseController objCC = new CourseController();
        Course objc = new Course();
        DataSet ds = null;

        ds = objCommon.FillDropDown("ACD_ATTENDANCE_STATUS_MASTER", "STATUS_NO", "STATUS_NAME", "", "STATUS_NO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlStatus.DataSource = ds;
            ddlStatus.DataTextField = ds.Tables[0].Columns["STATUS_NAME"].ToString();
            ddlStatus.DataValueField = ds.Tables[0].Columns["STATUS_NO"].ToString();
            ddlStatus.DataBind();
        }
        ddlStatus.SelectedIndex = 0;
    }
    private void GetDates()
    {
        int _schemeType = (rbnNew.Checked ? 1 : 2);

        DataSet dsDate = objCourse.GetAttendanceConfigData(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["college_id_att"]), Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]));

        if (dsDate != null && dsDate.Tables[0].Rows.Count > 0)
        {
            string startDate = dsDate.Tables[0].Rows[0]["START_DATE"].ToString();
            string endDate = dsDate.Tables[0].Rows[0]["END_DATE"].ToString();
            string attLockDay = dsDate.Tables[0].Rows[0]["LOCK_ATT_DAYS"].ToString();

            ViewState["startDate"] = startDate;
            ViewState["endDate"] = endDate;
            ViewState["attLockDay"] = attLockDay;
        }
        /****************************************************** Added by Dileep Kare on 01.01.2023 *********************************************************************************/
        DataSet dsUnlock = objAttController.Get_Individual_Lock_Unlock(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"].ToString()));
        ViewState["dtUnlock"] = (dsUnlock != null && dsUnlock.Tables.Count > 0) ? dsUnlock.Tables[0] : null;
        /*************************************************************************** End *******************************************************************************************/
    }
    private bool LockAttendance()
    {
        int ret = objAttController.LockAttendacneEntry(Convert.ToInt16(ddlColgSession.SelectedValue), Convert.ToInt16(Session["userno_Faculty"].ToString()));
        if (ret == Convert.ToInt16(CustomStatus.TransactionFailed))
        {
            objCommon.DisplayMessage(updTeachingPlan, "Error!!", this.Page);
            return false;
        }
        return true;
    }
    private void BindTopicCovered()
    {
        DateTime Date = Convert.ToDateTime(ViewState["date"].ToString());
        try
        {
            int tutorial = rdoTutorial.Checked ? 1 : 0;
            int electtype = 0;
            if (rdoCore.Checked == true)
            {
                electtype = 1;
                ddlTopicCovered.Items.Clear();
                //ddlTopicCovered.Items.Add(new ListItem("Please Select", "0"));
                DataSet ds = objAttController.GetFacultyWiseTopicCovered(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["college_id_att"]), Convert.ToInt32(Session["_semNo"]), Convert.ToInt32(Session["_schemeNo"]), Convert.ToInt32(ViewState["Courseno"]), Convert.ToInt32(Session["userno_Faculty"]), tutorial, Convert.ToInt32(Session["_sectionNo"]), Convert.ToInt32(Session["_batchNo"]), Convert.ToInt32(Session["OrgId"]), electtype);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTopicCovered.DataSource = ds.Tables[0];
                    ddlTopicCovered.DataTextField = ds.Tables[0].Columns["TOPIC_COVERED"].ToString();
                    ddlTopicCovered.DataValueField = ds.Tables[0].Columns["TP_NO"].ToString();
                    ddlTopicCovered.DataBind();
                    //  ddlTopicCovered.SelectedIndex = 1;
                }
                //else
                //{//ddlTopicCovered.Items.Clear();
                //    // ddlTopicCovered.Items.Add(new ListItem("Select All", "0"));
                //}
            }
            else if (rdoGlobalElective.Checked == true)
            {
                electtype = 2;
                ddlTopicCovered.Items.Clear();
                //ddlTopicCovered.Items.Add(new ListItem("Please Select", "0"));
                DataSet ds = objAttController.GetFacultyWiseTopicCovered(Convert.ToInt32(ddlSessionGlobal.SelectedValue), Convert.ToInt32(Session["college_id_att"]), Convert.ToInt32(Session["_semNo"]), Convert.ToInt32(Session["_schemeNo"]), Convert.ToInt32(ViewState["Alt_Courseno"]), Convert.ToInt32(Session["userno_Faculty"]), tutorial, Convert.ToInt32(Session["_sectionNo"]), Convert.ToInt32(Session["_batchNo"]), Convert.ToInt32(Session["OrgId"]), electtype);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlTopicCovered.DataSource = ds.Tables[0];
                    ddlTopicCovered.DataTextField = ds.Tables[0].Columns["TOPIC_COVERED"].ToString();
                    ddlTopicCovered.DataValueField = ds.Tables[0].Columns["TP_NO"].ToString();
                    ddlTopicCovered.DataBind();
                    //ddlTopicCovered.SelectedIndex = 1;
                }
                //else
                //{
                //    ddlTopicCovered.Items.Clear();
                //    ddlTopicCovered.Items.Add(new ListItem("Please Select", "0"));
                //}
            }
            //txtTopcDesc.Visible = false;
            // objCommon.FillDropDownList(ddlTopicCovered, "ACD_COURSE_TEACHER T INNER JOIN ACD_TEACHINGPLAN TP ON (T.SESSIONNO = TP.SESSIONNO AND T.SEMESTERNO = TP.TERM AND (T.CCODE  = TP.CCODE AND T.COURSENO = TP.COURSENO) AND T.UA_NO = TP.UA_NO AND T.COLLEGE_ID=TP.COLLEGE_ID)", "DISTINCT TP_NO", "TOPIC_COVERED", " TERM=" + Session["_semNo"].ToString() + " AND T.SCHEMENO=" + Session["_schemeNo"].ToString() + " AND SLOT=" + Session["_slotNo"] + " AND T.SECTIONNO =" + Session["_sectionNo"].ToString() + " AND (T.UA_NO=" + Session["userno_Faculty"].ToString() + " OR T.ADTEACHER =" + Session["userno_Faculty"].ToString() + ") AND T.COURSENO =" + ViewState["Alt_Courseno"].ToString() + "  AND (T.BATCHNO =" + Session["_batchNo"].ToString() + " OR T.BATCHNO =" + Session["_batchNo"].ToString() + ") AND ISNULL(T.CANCEL,0)=0 AND T.SESSIONNO =" + ddlSession.SelectedValue + "  AND CAST(TP.[DATE] AS DATE) = CAST(CONVERT(DATE,'" + Date.ToString("dd/MM/yyyy") + "',103) AS DATE) AND T.COLLEGE_ID=" + ddlInstitute.SelectedValue + "AND T.ORGANIZATIONID="+Session["OrgId"]+" AND  ISNULL(TUTORIAL,0)=" + tutorial, "");
            //objCommon.FillDropDownList(ddlTopicCovered, "ACD_COURSE_TEACHER T INNER JOIN ACD_TEACHINGPLAN TP ON (T.SESSIONNO = TP.SESSIONNO AND T.SEMESTERNO = TP.TERM AND (T.CCODE  = TP.CCODE AND T.COURSENO = TP.COURSENO) AND (T.UA_NO = TP.UA_NO OR T.ADTEACHER = TP.UA_NO) AND T.COLLEGE_ID=TP.COLLEGE_ID)", "DISTINCT TP_NO", "CONCAT(UNIT_NO,' - ', LECTURE_NO ,' - ', TOPIC_COVERED)", "ISNULL(TP.CANCEL,0)=0 AND TERM=" + Session["_semNo"].ToString() + " AND T.SCHEMENO=" + Session["_schemeNo"].ToString() + " AND T.SECTIONNO =" + Session["_sectionNo"].ToString() + " AND (T.UA_NO=" + Session["userno_Faculty"].ToString() + " OR T.ADTEACHER =" + Session["userno_Faculty"].ToString() + ") AND T.COURSENO =" + ViewState["Alt_Courseno"].ToString() + "  AND (T.BATCHNO =" + Session["_batchNo"].ToString() + " OR T.BATCHNO =" + Session["_batchNo"].ToString() + ") AND ISNULL(T.CANCEL,0)=0 AND T.SESSIONNO =" + ddlColgSession.SelectedValue + " AND T.COLLEGE_ID=" + Convert.ToInt32(Session["college_id_att"]) + "AND T.ORGANIZATIONID=" + Session["OrgId"] + " AND  ISNULL(TUTORIAL,0)=" + tutorial, "");

        }
        catch
        {
            throw;
        }
    }

    private void BindStudentCount()
    {
        try
        {
            int total = 0;
            int present = 0;
            int odTotal = 0;
            foreach (ListViewDataItem lvitem1 in lvStudent.Items)
            {
                CheckBox ckhbx = lvitem1.FindControl("cbRow") as CheckBox;
                HiddenField hf = lvitem1.FindControl("hdfIdNo") as HiddenField;
                HiddenField hdfLeaveStatus = lvitem1.FindControl("hdfLeaveStatus") as HiddenField;
                total++;
                if (ckhbx.Checked == false && hdfLeaveStatus.Value == "1")
                {
                    odTotal++;
                }
                if (ckhbx.Checked == true) //&& hdfLeaveStatus.Value == "0"
                {
                    present++;
                }
            }
            txtODStudent.Text = (odTotal).ToString();//show OD approved student
            txtAbsentStudent.Text = (total - present - odTotal).ToString();
            txtPresentStudent.Text = (present).ToString();
            txtTotalStudent.Text = total.ToString();
        }
        catch
        {

        }
    }
    #endregion

    #region Calender Event
    //============================================================================================//
    private void GetCourse()
    {
        try
        {
            int _schemeType = (rbnNew.Checked ? 1 : 2);
            int istutorial = rdoTutorial.Checked ? 1 : 0;
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            //=========== get all course of login faculty =====================//
            DataSet dsCalederCourse = objAttController.GetAllCoursesModified(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, Session["college_id_att"].ToString(), istutorial, OrgId);
            ViewState["dsCalederCourse"] = dsCalederCourse;//Insert Data Set in View State
            //ViewState["SessionNo"] = ddlSession.SelectedValue;

            /// //Added By Rishabh on 23 jan 2023   - extra class
            DataTable firstTable = dsCalederCourse.Tables[0];
            DataRow[] filterData1 = firstTable.Select("EXTRACLASS=1");
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("TT_DATE", typeof(string)));
            foreach (DataRow row in filterData1)
            {
                DataRow dr = dt.NewRow();
                dr["TT_DATE"] = row["TT_DATE"].ToString();
                dt.Rows.Add(dr);
                // ViewState["EXTRA_CLASS"] += row["TT_DATE"].ToString();
            }
            ViewState["EXTRA_CLASS"] = dt;
            ///


            //=========== get all Holiday from master =====================//
            DataSet dsHDay = objAttController.GetRestrictedCoursesModified(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, Session["college_id_att"].ToString(), istutorial);
            //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
            ViewState["dsHDay"] = dsHDay;//Insert Data Set in View State          

            //=========== get all alternate assinged courses to login faculty =====================//
            DataSet dsAlAtt = objAttController.GetAlternateAllottedCoursesModified(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, Session["college_id_att"].ToString(), istutorial, OrgId);
            //objCommon.FillDropDown("ACD_ALTERNATE_ATTENDANCE AL INNER JOIN ACD_COURSE C ON C.COURSENO=AL.TAKEN_COURSENO INNER JOIN ACD_SCHEME S ON S.SCHEMENO=AL.SCHEMENO INNER JOIN ACD_ATTENDANCE_CONFIG A ON A.SESSIONNO=AL.SESSIONNO AND A.DEGREENO=S.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE AND A.SEMESTERNO=AL.SEMESTERNO",
            //    "DISTINCT (SELECT DATEPART(DW,ATTENDANCE_DATE)-1)AltDayNO", "CCODE,ATTENDANCE_DATE,COURSE_NAME,START_DATE,END_DATE",
            //    "S.SCHEMETYPE=" + _schemeType + " AND ISNULL(CANCEL,0)=0 AND TAKEN_UANO=" + Convert.ToInt32(Session["userno_Faculty"].ToString()),
            //    "");
            ViewState["dsAlAtt"] = dsAlAtt;

            //=========== get all Lock Holiday from master =====================//
            DataSet dsLockHDay = objAttController.GetAllHolidays(Convert.ToInt32(ddlColgSession.SelectedValue));
            //objCommon.FillDropDown("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "ACADEMIC_HOLIDAY_NAME,ACADEMIC_HOLIDAY_STDATE", "SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue), "HOLIDAY_NO");
            ViewState["dsLockHDay"] = dsLockHDay;//Insert Data Set in View State  

            DataSet dsShiftTT = objAttController.GetAllShiftTTCoursesModified(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, Session["college_id_att"].ToString(), istutorial, OrgId);
            //objCommon.FillDropDown("ACD_TIME_TABLE_SHIFT TS INNER JOIN ACD_COURSE C ON C.COURSENO=TS.COURSENO INNER JOIN ACD_SCHEME S ON S.SCHEMENO=TS.SCHEMENO INNER JOIN ACD_ATTENDANCE_CONFIG A ON  A.SESSIONNO=TS.SESSIONNO AND A.DEGREENO=S.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE AND A.SEMESTERNO=TS.SEMESTERNO", "DISTINCT (SELECT DATEPART(DW,SHIFT_TT_DATE)-1)ShiftDayNO", "CCODE,TT_DAYNO,SHIFT_TT_DATE,COURSE_NAME,START_DATE,END_DATE", "S.SCHEMETYPE=" + _schemeType + " AND UA_NO=" + Convert.ToInt32(Session["userno_Faculty"].ToString()), "");
            ViewState["dsShiftTT"] = dsShiftTT;//Insert Data Set in View State 
        }
        catch
        {
            throw;
        }
    }
    //============================================================================================//
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            if (rdoCore.Checked == true)
            {
                if (ddlColgSession.SelectedValue != null && !ddlColgSession.SelectedValue.ToString().Equals("Please Select"))
                {
                    string _tdate = DateTime.Now.ToString("dd/MM/yyyy");
                    int AttLockDay = Convert.ToInt32(ViewState["attLockDay"]);
                    //============ get all courses which time table are created..==============//

                    DataSet dsCalederCourse = (DataSet)ViewState["dsCalederCourse"]; //objAttController.GetAllCourses(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]));
                    //  objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN ACD_TIME_TABLE_CONFIG F ON (T.CT_NO = F.CTNO OR T.ADTEACHER=F.CTNO) INNER JOIN ACD_COURSE C ON (C.COURSENO = T.COURSENO)", "DISTINCT DAYNO", "CTNO,T.COURSENO,T.CCODE,COURSE_NAME", "(UA_NO = " + Convert.ToInt32(Session["userno_Faculty"]) + " OR ADTEACHER=" + Convert.ToInt32(Session["userno_Faculty"]) + ") AND ISNULL(CANCEL,0)=0 AND SESSIONNO = " + ddlSession.SelectedValue + "", "DAYNO");
                    DataTable dt = dsCalederCourse.Tables[0];
                    DataRowCollection drc = dt.Rows;

                    DataSet dsHDay = (DataSet)ViewState["dsHDay"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                    DataTable dtHDay = dsHDay != null ? dsHDay.Tables[0] : null;
                    DataRowCollection drcHDay = dtHDay != null ? dtHDay.Rows : null;

                    DataSet dsAlAtt = (DataSet)ViewState["dsAlAtt"];

                    DataTable dtAlAtt = dsAlAtt != null ? dsAlAtt.Tables[0] : null;
                    DataRowCollection drcAlAtt = dtAlAtt != null ? dtAlAtt.Rows : null;

                    DataSet dsLockHDay = (DataSet)ViewState["dsLockHDay"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                    DataTable dtLockHDay = dsLockHDay != null ? dsLockHDay.Tables[0] : null;
                    DataRowCollection drcLockHDay = dtLockHDay != null ? dtLockHDay.Rows : null;

                    DataSet dsShiftTT = (DataSet)ViewState["dsShiftTT"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                    DataTable dtShiftTT = dsShiftTT != null ? dsShiftTT.Tables[0] : null;
                    DataRowCollection drcShiftTT = dtShiftTT != null ? dtShiftTT.Rows : null;

                    #region Alternate Att.
                    if (drcAlAtt != null && drcAlAtt.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litAlAttDay = new Label();
                            e.Cell.Controls.Add(litAlAttDay);

                            for (int i = 0; i <= dsAlAtt.Tables[0].Rows.Count - 1; i++)
                            {
                                int dayno = Convert.ToInt32(dsAlAtt.Tables[0].Rows[i]["AltDayNO"]);
                                //SET DATES WITH SEA SHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION

                                if (e.Day.Date >= Convert.ToDateTime(dsAlAtt.Tables[0].Rows[i]["START_DATE"]) && e.Day.Date <= Convert.ToDateTime(dsAlAtt.Tables[0].Rows[i]["END_DATE"]))
                                {//------ Current Session Start & End Date -------//                           

                                    if (e.Day.Date == Convert.ToDateTime(dsAlAtt.Tables[0].Rows[i]["ATTENDANCE_DATE"]))
                                    {
                                        //if (e.Day.Date >= Convert.ToDateTime(ViewState["startDate"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDate"]))
                                        //{//------ Current Session Start & End Date -------//

                                        //if (e.Day.Date >= Convert.ToDateTime(ViewState["startDateW"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDateW"]))
                                        //{//------ Current Week Start & End Date -------//
                                        if (dsAlAtt.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                        {
                                            if ((int)e.Day.Date.DayOfWeek == dayno)
                                            {
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litAlAttDay.Text += "<br/>" + dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString();
                                                string course = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litAlAttDay.ToolTip += course;
                                            }
                                            else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                            {
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litAlAttDay.Text += "<br/>" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString();
                                                string course = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litAlAttDay.ToolTip += course;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litAlAttDay = new Label();
                            e.Cell.Controls.Add(litAlAttDay);
                        }
                    }

                    #endregion Alternate Att.

                    #region TimeTable
                    if (drc != null && drc.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label literal1 = new Label();
                            e.Cell.Controls.Add(literal1);

                            foreach (DataRow dr in drc)
                            {
                                int dayno = Convert.ToInt32(dr["DAYNO"]);
                                //SET DATES WITH SEASHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION

                                if (e.Day.Date >= Convert.ToDateTime(dr["START_DATE"]) && e.Day.Date <= Convert.ToDateTime(dr["END_DATE"]))
                                {//------ Current Session Start & End Date -------//                           

                                    if (e.Day.Date == Convert.ToDateTime(dr["TT_DATE"]))
                                    {
                                        DataRow rw1 = dtShiftTT.AsEnumerable().FirstOrDefault(tt => tt.Field<DateTime>("SHIFT_TT_DATE") == e.Day.Date);
                                        if (rw1 == null)// Check the shifted time table date(if it exist then skip this date regular timetable)
                                        {
                                            if ((int)e.Day.Date.DayOfWeek == dayno)
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                literal1.Text += "<br/>" + dr["CCODE"].ToString();
                                                string course = dr["COURSE_CODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dr["CCODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                literal1.ToolTip += course;
                                            }
                                            else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                literal1.Text += "<br/>" + dr["CCODE"].ToString();
                                                string course = dr["COURSE_CODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dr["CCODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                literal1.ToolTip += course;
                                            }
                                        }
                                    }
                                }//-------- end Session master date block ------//
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label literal1 = new Label();
                            e.Cell.Controls.Add(literal1);
                        }
                    }
                    #endregion TimeTable

                    #region Shifted TimeTable
                    if (drcShiftTT != null && drcShiftTT.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litShiftTTDay = new Label();
                            e.Cell.Controls.Add(litShiftTTDay);

                            for (int i = 0; i <= dsShiftTT.Tables[0].Rows.Count - 1; i++)
                            {
                                int dayno = Convert.ToInt32(dsShiftTT.Tables[0].Rows[i]["ShiftDayNO"]);
                                //SET DATES WITH SEASHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION

                                if (e.Day.Date >= Convert.ToDateTime(dsShiftTT.Tables[0].Rows[i]["START_DATE"]) && e.Day.Date <= Convert.ToDateTime(dsShiftTT.Tables[0].Rows[i]["END_DATE"]))
                                //if (e.Day.Date >= Convert.ToDateTime(ViewState["startDate"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDate"]))
                                {//------ Current Session Start & End Date -------//

                                    DataRow rw = dtShiftTT.AsEnumerable().FirstOrDefault(tt => tt.Field<DateTime>("SHIFT_TT_DATE") == e.Day.Date);
                                    if (rw != null)// Check the shifted time table date(if it exist then show Shifted TT slot/course)
                                    {
                                        if (dsShiftTT.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                        {
                                            if ((int)e.Day.Date.DayOfWeek == dayno)
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.CssClass = "shifted-lecture";
                                                //e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litShiftTTDay.Text += "<br/>" + dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString();
                                                string course = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litShiftTTDay.ToolTip += course;
                                            }
                                            else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.CssClass = "shifted-lecture";
                                                //e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litShiftTTDay.Text += "<br/>" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString();
                                                string course = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litShiftTTDay.ToolTip += course;
                                            }
                                        }
                                    }//--------- End Shift TT dates Block...
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litShiftTTDay = new Label();
                            e.Cell.Controls.Add(litShiftTTDay);
                        }
                    }

                    #endregion Shifted TimeTable

                    #region Restricted Holiday(Partial Allow Att.)

                    if (drcHDay != null && drcHDay.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);

                            for (int i = 0; i <= dsHDay.Tables[0].Rows.Count - 1; i++)
                            {
                                if (dsHDay.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsHDay.Tables[0].Rows[i][1].ToString();

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsHDay.Tables[0].Rows[i][1].ToString();//for tooltip                              

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);
                        }
                    }
                    #endregion  Restricted Holiday(Partial Allow Att.)

                    #region Lock Holiday(Not Allow Att.)

                    if (drcLockHDay != null && drcLockHDay.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);

                            for (int i = 0; i <= dsLockHDay.Tables[0].Rows.Count - 1; i++)
                            {
                                if (dsLockHDay.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsLockHDay.Tables[0].Rows[i][1].ToString();

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsLockHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsLockHDay.Tables[0].Rows[i][1].ToString();//for tooltip                              

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsLockHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);//3, 169, 243);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);
                        }
                    }

                    #endregion Lock Holiday(Not Allow Att.)

                    string lockDate = string.Empty;                                                     //Convert.ToDateTime(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
                    // lockDate = objCommon.LookUp("ACD_HOLIDAY_MASTER", "ISNULL(LOCK,0)LOCK", "HOLIDAY_DATE=CONVERT(DATETIME,'" + e.Day.Date.ToString() + "',103)") == string.Empty ? string.Empty : objCommon.LookUp("ACD_HOLIDAY_MASTER", "ISNULL(LOCK,0)LOCK", "HOLIDAY_DATE=CONVERT(DATETIME,'" + e.Day.Date.ToString() + "',103)");
                    //lockDate = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103)=CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue)) == string.Empty ? string.Empty : objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103)=CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue));
                    lockDate = "0";// objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) BETWEEN CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103) AND CONVERT(DATETIME,ACADEMIC_HOLIDAY_ENDDATE,103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue)) == string.Empty ? string.Empty : objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) BETWEEN CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103) AND CONVERT(DATETIME,ACADEMIC_HOLIDAY_ENDDATE,103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue));
                    // above lockDate = "0" added by rishabh on 22/11/2023. tkt no 50662 for unlocking attendance for holidays also.
                    #region Lock/Unlock
                    //SET DATES SELECTABLE ONLY WHICH ARE SET LOCK DAYS BEFORE CURRENT DAY FOR ATTENDANCE LOCK IN ATTENDANCE CONFIGURATION
                    //AND FUTURE DATES ARE NOT SELECTABLE TO MARK ATTENDANCE
                    /*************************************ADDED BY DILEEP KARE ON 05.01.2023 ****************************/
                    DataTable dtUnlock = (DataTable)ViewState["dtUnlock"];
                    string Expression = "ATTLOCK_DATE='" + e.Day.Date.ToString("dd/MM/yyyy") + "'";
                    DataRow[] dtRow = dtUnlock != null ? dtUnlock.Select(Expression) : null;

                    if ((e.Day.Date < DateTime.Today.AddDays(-AttLockDay) || e.Day.Date > DateTime.Today) && (dtRow == null || dtRow.Length == 0)) //ADDED BY DILEEP ON 05.01.2022 (dtRow == null || dtRow.Length == 0)
                    {
                        e.Day.IsSelectable = false;
                        e.Cell.Font.Strikeout = true;

                        Label l = new Label();
                        //set the text of the label             
                        l.CssClass = "fa fa-lock";
                        l.Style.Add("color", "#d11e1e");
                        l.Style.Add("opacity", ".2");
                        l.Font.Size = 11;
                        l.ToolTip = "Lock";
                        e.Cell.Controls.Add(l);
                    }
                    else
                    {
                        if (lockDate == "1")
                        {
                            e.Day.IsSelectable = false;
                            e.Cell.Font.Strikeout = true;

                            Label l = new Label();
                            //set the text of the label             
                            l.CssClass = "fa fa-lock";
                            l.Style.Add("color", "#d11e1e");
                            l.Style.Add("opacity", ".2");
                            l.Font.Size = 11;
                            l.ToolTip = "Lock";
                            e.Cell.Controls.Add(l);
                        }
                        else
                        {
                            e.Day.IsSelectable = true;
                            e.Cell.Font.Strikeout = false;
                            //e.Cell.BackColor = System.Drawing.Color.Yellow;

                            Label l = new Label();
                            //set the text of the label             
                            l.CssClass = "fa fa-unlock";
                            l.Style.Add("color", "#36c60a");
                            l.Style.Add("opacity", ".5");
                            l.Font.Size = 11;
                            //l.ToolTip = "UnLock";
                            e.Cell.Controls.Add(l);
                        }
                    }

                    #endregion Lock/Unlock
                }
            }
            else if (rdoGlobalElective.Checked == true)
            {
                if (ddlSessionGlobal.SelectedValue != null && !ddlSessionGlobal.SelectedValue.ToString().Equals("Please Select"))
                {
                    string _tdate = DateTime.Now.ToString("dd/MM/yyyy");
                    int AttLockDay = Convert.ToInt32(ViewState["attLockDay"]);
                    //============ get all courses which time table are created..==============//

                    DataSet dsCalederCourse = (DataSet)ViewState["dsCalederCourse"]; //objAttController.GetAllCourses(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]));
                    //  objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN ACD_TIME_TABLE_CONFIG F ON (T.CT_NO = F.CTNO OR T.ADTEACHER=F.CTNO) INNER JOIN ACD_COURSE C ON (C.COURSENO = T.COURSENO)", "DISTINCT DAYNO", "CTNO,T.COURSENO,T.CCODE,COURSE_NAME", "(UA_NO = " + Convert.ToInt32(Session["userno_Faculty"]) + " OR ADTEACHER=" + Convert.ToInt32(Session["userno_Faculty"]) + ") AND ISNULL(CANCEL,0)=0 AND SESSIONNO = " + ddlSession.SelectedValue + "", "DAYNO");
                    DataTable dt = dsCalederCourse.Tables[0];
                    DataRowCollection drc = dt.Rows;

                    DataSet dsHDay = (DataSet)ViewState["dsHDay"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                    DataTable dtHDay = dsHDay != null ? dsHDay.Tables[0] : null;
                    DataRowCollection drcHDay = dtHDay != null ? dtHDay.Rows : null;

                    DataSet dsAlAtt = (DataSet)ViewState["dsAlAtt"];

                    DataTable dtAlAtt = dsAlAtt != null ? dsAlAtt.Tables[0] : null;
                    DataRowCollection drcAlAtt = dtAlAtt != null ? dtAlAtt.Rows : null;

                    DataSet dsLockHDay = (DataSet)ViewState["dsLockHDay"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                    DataTable dtLockHDay = dsLockHDay != null ? dsLockHDay.Tables[0] : null;
                    DataRowCollection drcLockHDay = dtLockHDay != null ? dtLockHDay.Rows : null;

                    DataSet dsShiftTT = (DataSet)ViewState["dsShiftTT"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                    DataTable dtShiftTT = dsShiftTT != null ? dsShiftTT.Tables[0] : null;
                    DataRowCollection drcShiftTT = dtShiftTT != null ? dtShiftTT.Rows : null;

                    #region Alternate Att.
                    if (drcAlAtt != null && drcAlAtt.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litAlAttDay = new Label();
                            e.Cell.Controls.Add(litAlAttDay);

                            for (int i = 0; i <= dsAlAtt.Tables[0].Rows.Count - 1; i++)
                            {
                                int dayno = Convert.ToInt32(dsAlAtt.Tables[0].Rows[i]["AltDayNO"]);
                                //SET DATES WITH SEA SHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION

                                if (e.Day.Date >= Convert.ToDateTime(dsAlAtt.Tables[0].Rows[i]["START_DATE"]) && e.Day.Date <= Convert.ToDateTime(dsAlAtt.Tables[0].Rows[i]["END_DATE"]))
                                {//------ Current Session Start & End Date -------//                           

                                    if (e.Day.Date == Convert.ToDateTime(dsAlAtt.Tables[0].Rows[i]["ATTENDANCE_DATE"]))
                                    {
                                        //if (e.Day.Date >= Convert.ToDateTime(ViewState["startDate"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDate"]))
                                        //{//------ Current Session Start & End Date -------//

                                        //if (e.Day.Date >= Convert.ToDateTime(ViewState["startDateW"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDateW"]))
                                        //{//------ Current Week Start & End Date -------//
                                        if (dsAlAtt.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                        {
                                            if ((int)e.Day.Date.DayOfWeek == dayno)
                                            {
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litAlAttDay.Text += "<br/>" + dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString();
                                                string course = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litAlAttDay.ToolTip += course;
                                            }
                                            else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                            {
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litAlAttDay.Text += "<br/>" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString();
                                                string course = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsAlAtt.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsAlAtt.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litAlAttDay.ToolTip += course;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litAlAttDay = new Label();
                            e.Cell.Controls.Add(litAlAttDay);
                        }
                    }

                    #endregion Alternate Att.

                    #region TimeTable
                    if (drc != null && drc.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label literal1 = new Label();
                            e.Cell.Controls.Add(literal1);

                            foreach (DataRow dr in drc)
                            {
                                int dayno = Convert.ToInt32(dr["DAYNO"]);
                                //SET DATES WITH SEASHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION

                                if (e.Day.Date >= Convert.ToDateTime(dr["START_DATE"]) && e.Day.Date <= Convert.ToDateTime(dr["END_DATE"]))
                                {//------ Current Session Start & End Date -------//                           

                                    if (e.Day.Date == Convert.ToDateTime(dr["TT_DATE"]))
                                    {
                                        DataRow rw1 = dtShiftTT.AsEnumerable().FirstOrDefault(tt => tt.Field<DateTime>("SHIFT_TT_DATE") == e.Day.Date);
                                        if (rw1 == null)// Check the shifted time table date(if it exist then skip this date regular timetable)
                                        {
                                            if ((int)e.Day.Date.DayOfWeek == dayno)
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                literal1.Text += "<br/>" + dr["CCODE"].ToString();
                                                string course = dr["CCODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dr["CCODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                literal1.ToolTip += course;
                                            }
                                            else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                literal1.Text += "<br/>" + dr["CCODE"].ToString();
                                                string course = dr["CCODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dr["CCODE"].ToString() + "-" + dr["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                literal1.ToolTip += course;
                                            }
                                        }
                                    }
                                }//-------- end Session master date block ------//
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label literal1 = new Label();
                            e.Cell.Controls.Add(literal1);
                        }
                    }
                    #endregion TimeTable

                    #region Shifted TimeTable
                    if (drcShiftTT != null && drcShiftTT.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litShiftTTDay = new Label();
                            e.Cell.Controls.Add(litShiftTTDay);

                            for (int i = 0; i <= dsShiftTT.Tables[0].Rows.Count - 1; i++)
                            {
                                int dayno = Convert.ToInt32(dsShiftTT.Tables[0].Rows[i]["ShiftDayNO"]);
                                //SET DATES WITH SEASHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION

                                if (e.Day.Date >= Convert.ToDateTime(dsShiftTT.Tables[0].Rows[i]["START_DATE"]) && e.Day.Date <= Convert.ToDateTime(dsShiftTT.Tables[0].Rows[i]["END_DATE"]))
                                //if (e.Day.Date >= Convert.ToDateTime(ViewState["startDate"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDate"]))
                                {//------ Current Session Start & End Date -------//

                                    DataRow rw = dtShiftTT.AsEnumerable().FirstOrDefault(tt => tt.Field<DateTime>("SHIFT_TT_DATE") == e.Day.Date);
                                    if (rw != null)// Check the shifted time table date(if it exist then show Shifted TT slot/course)
                                    {
                                        if (dsShiftTT.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                        {
                                            if ((int)e.Day.Date.DayOfWeek == dayno)
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.CssClass = "shifted-lecture";
                                                //e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litShiftTTDay.Text += "<br/>" + dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString();
                                                string course = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litShiftTTDay.ToolTip += course;
                                            }
                                            else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                            {
                                                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                                    e.Cell.CssClass = "today-date";
                                                e.Cell.CssClass = "shifted-lecture";
                                                //e.Cell.BackColor = System.Drawing.Color.AliceBlue;
                                                e.Cell.ForeColor = System.Drawing.Color.Black;

                                                e.Cell.ToolTip = "Click on date to Mark Attendance";
                                                litShiftTTDay.Text += "<br/>" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString();
                                                string course = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";
                                                Session["courseName"] = dsShiftTT.Tables[0].Rows[i]["CCODE"].ToString() + "-" + dsShiftTT.Tables[0].Rows[i]["COURSE_NAME"].ToString() + "\n";// session para. used for email/sms text.
                                                litShiftTTDay.ToolTip += course;
                                            }
                                        }
                                    }//--------- End Shift TT dates Block...
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litShiftTTDay = new Label();
                            e.Cell.Controls.Add(litShiftTTDay);
                        }
                    }

                    #endregion Shifted TimeTable

                    #region Restricted Holiday(Partial Allow Att.)

                    if (drcHDay != null && drcHDay.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);

                            for (int i = 0; i <= dsHDay.Tables[0].Rows.Count - 1; i++)
                            {
                                if (dsHDay.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsHDay.Tables[0].Rows[i][1].ToString();

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsHDay.Tables[0].Rows[i][1].ToString();//for tooltip                              

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);
                        }
                    }
                    #endregion  Restricted Holiday(Partial Allow Att.)

                    #region Lock Holiday(Not Allow Att.)

                    if (drcLockHDay != null && drcLockHDay.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);

                            for (int i = 0; i <= dsLockHDay.Tables[0].Rows.Count - 1; i++)
                            {
                                if (dsLockHDay.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsLockHDay.Tables[0].Rows[i][1].ToString();

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsLockHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        e.Cell.ForeColor = System.Drawing.Color.Magenta;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        e.Cell.ToolTip = dsLockHDay.Tables[0].Rows[i][1].ToString();//for tooltip                              

                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsLockHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);//3, 169, 243);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            Label litHDay = new Label();
                            e.Cell.Controls.Add(litHDay);
                        }
                    }

                    #endregion Lock Holiday(Not Allow Att.)

                    string lockDate = string.Empty;                                                     //Convert.ToDateTime(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
                    // lockDate = objCommon.LookUp("ACD_HOLIDAY_MASTER", "ISNULL(LOCK,0)LOCK", "HOLIDAY_DATE=CONVERT(DATETIME,'" + e.Day.Date.ToString() + "',103)") == string.Empty ? string.Empty : objCommon.LookUp("ACD_HOLIDAY_MASTER", "ISNULL(LOCK,0)LOCK", "HOLIDAY_DATE=CONVERT(DATETIME,'" + e.Day.Date.ToString() + "',103)");
                    //lockDate = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103)=CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue)) == string.Empty ? string.Empty : objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103)=CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue));
                    lockDate = "0";// objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) BETWEEN CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103) AND CONVERT(DATETIME,ACADEMIC_HOLIDAY_ENDDATE,103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue)) == string.Empty ? string.Empty : objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "CONVERT(DATETIME,'" + Convert.ToDateTime(e.Day.Date.ToString("dd/MM/yyyy")) + "',103) BETWEEN CONVERT(DATETIME,ACADEMIC_HOLIDAY_STDATE,103) AND CONVERT(DATETIME,ACADEMIC_HOLIDAY_ENDDATE,103) AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue));
                    // above lockDate = "0" added by rishabh on 22/11/2023. tkt no 50662 for unlocking attendance for holidays also.

                    #region Lock/Unlock
                    //SET DATES SELECTABLE ONLY WHICH ARE SET LOCK DAYS BEFORE CURRENT DAY FOR ATTENDANCE LOCK IN ATTENDANCE CONFIGURATION
                    //AND FUTURE DATES ARE NOT SELECTABLE TO MARK ATTENDANCE
                    /*************************************ADDED BY DILEEP KARE ON 05.01.2023 ****************************/
                    DataTable dtUnlock = (DataTable)ViewState["dtUnlock"];
                    string Expression = "ATTLOCK_DATE='" + e.Day.Date.ToString("dd/MM/yyyy") + "'";
                    DataRow[] dtRow = dtUnlock != null ? dtUnlock.Select(Expression) : null;

                    if ((e.Day.Date < DateTime.Today.AddDays(-AttLockDay) || e.Day.Date > DateTime.Today) && (dtRow == null || dtRow.Length == 0)) //ADDED BY DILEEP ON 05.01.2022 (dtRow == null || dtRow.Length == 0)
                    {
                        e.Day.IsSelectable = false;
                        e.Cell.Font.Strikeout = true;

                        Label l = new Label();
                        //set the text of the label             
                        l.CssClass = "fa fa-lock";
                        l.Style.Add("color", "#d11e1e");
                        l.Style.Add("opacity", ".2");
                        l.Font.Size = 11;
                        l.ToolTip = "Lock";
                        e.Cell.Controls.Add(l);
                    }
                    else
                    {
                        if (lockDate == "1")
                        {
                            e.Day.IsSelectable = false;
                            e.Cell.Font.Strikeout = true;

                            Label l = new Label();
                            //set the text of the label             
                            l.CssClass = "fa fa-lock";
                            l.Style.Add("color", "#d11e1e");
                            l.Style.Add("opacity", ".2");
                            l.Font.Size = 11;
                            l.ToolTip = "Lock";
                            e.Cell.Controls.Add(l);
                        }
                        else
                        {
                            e.Day.IsSelectable = true;
                            e.Cell.Font.Strikeout = false;
                            //e.Cell.BackColor = System.Drawing.Color.Yellow;

                            Label l = new Label();
                            //set the text of the label             
                            l.CssClass = "fa fa-unlock";
                            l.Style.Add("color", "#36c60a");
                            l.Style.Add("opacity", ".5");
                            l.Font.Size = 11;
                            //l.ToolTip = "UnLock";
                            e.Cell.Controls.Add(l);
                        }
                    }

                    #endregion Lock/Unlock
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            int _schemeType = (rbnNew.Checked ? 1 : 2);
            int istutorial = rdoTutorial.Checked ? 1 : 0;
            DateTime date = Convert.ToDateTime(Calendar1.SelectedDate.ToString("dd/MM/yyyy"));
            DataSet ds = null;
            ViewState["date"] = date;
            int dayName = ((int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek); // Here dayofWeek function return sunday values(0)..

            int TPlanYesNo = 0, TPFlag = 0;
            ViewState["TPlanYesNo"] = 0;
            if (rdoCore.Checked == true)
            {
                int sessionno = Convert.ToInt32(ddlColgSession.SelectedValue);
                ViewState["TPlanYesNo"] = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + sessionno + " AND ISNULL(ACTIVE,0)=1") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + sessionno + " AND ISNULL(ACTIVE,0)=1"));
                TPlanYesNo = Convert.ToInt32(ViewState["TPlanYesNo"]);
                //var dtExtra = ViewState["EXTRA_CLASS"] as DataTable; //Added By Rishabh - Excluding date if extra class time table is present for selected date for teaching plan.
                //DataRow[] rows = dtExtra.Select("TT_DATE='" + date + "'");
                //if (rows.Length >= 1)
                //{
                //    TPlanYesNo = 0;
                //}

                if (Convert.ToInt32(ViewState["TPlanYesNo"]) == 1)// if(TPlanYesNo=1)THEN attendance base on teaching plan..
                {
                    ds = objAttController.GetSubjectForAttendanceModified(sessionno, dayName, Convert.ToInt32(Session["userno_Faculty"]), Convert.ToDateTime(ViewState["date"]), TPlanYesNo, _schemeType, Session["college_id_att"].ToString(), istutorial);
                }
                else
                {
                    ds = objAttController.GetSubjectForAttendanceModified(sessionno, dayName, Convert.ToInt32(Session["userno_Faculty"]), Convert.ToDateTime(ViewState["date"]), TPlanYesNo, _schemeType, Session["college_id_att"].ToString(), istutorial);
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TPFlag = 0;
                    lvSubjectList.DataSource = ds;
                    lvSubjectList.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]), lvSubjectList);//Set label - 
                    Session["AttSubUano"] = Convert.ToString(ds.Tables[0].Rows[0]["UA_NO"]);//stored uano of faculty who has been submitted att.
                    txtTopcDesc.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOPIC_COVERED"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TOPIC_COVERED"]);
                }
                else
                {
                    TPFlag = 1;
                    Session["AttSubUano"] = "0";
                    lvSubjectList.DataSource = null;
                    lvSubjectList.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]), lvSubjectList);//Set label - 
                }
            }
            else if (rdoGlobalElective.Checked == true)
            {
                int sessionno = Convert.ToInt32(ddlSessionGlobal.SelectedValue);
                ViewState["TPlanYesNo"] = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG AC INNER JOIN ACD_COURSE_TEACHER CT WITH (NOLOCK) ON(CT.SESSIONNO=AC.SESSIONNO AND AC.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT ISNULL(TEACHING_PLAN,0)", "ISNULL(GLOBAL_ELECTIVE,0)=1 AND UA_NO =" + Convert.ToInt32(Session["userno_Faculty"])));
                if (Convert.ToInt32(ViewState["TPlanYesNo"]) == 1)// if(TPlanYesNo=1)THEN attendance base on teaching plan..
                {
                    ds = objAttController.GetSubjectForAttendanceModifiedGlobalElective(sessionno, dayName, Convert.ToInt32(Session["userno_Faculty"]), Convert.ToDateTime(ViewState["date"]), Convert.ToInt32(ViewState["TPlanYesNo"]), _schemeType, istutorial);
                }
                else
                {
                    ds = objAttController.GetSubjectForAttendanceModifiedGlobalElective(sessionno, dayName, Convert.ToInt32(Session["userno_Faculty"]), Convert.ToDateTime(ViewState["date"]), Convert.ToInt32(ViewState["TPlanYesNo"]), _schemeType, istutorial);
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TPFlag = 0;
                    lvSubjectList.DataSource = ds;
                    lvSubjectList.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]), lvSubjectList);//Set label - 
                    Session["AttSubUano"] = Convert.ToString(ds.Tables[0].Rows[0]["UA_NO"]);//stored uano of faculty who has been submitted att.
                    txtTopcDesc.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOPIC_COVERED"]) == null ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["TOPIC_COVERED"]);
                }
                else
                {
                    TPFlag = 1;
                    Session["AttSubUano"] = "0";
                    lvSubjectList.DataSource = null;
                    lvSubjectList.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]), lvSubjectList);//Set label - 
                }
            }
            Calendar1.SelectedDate = DateTime.MinValue;

            var dt = ViewState["EXTRA_CLASS"] as DataTable; //Added By Rishabh - Excluding date if extra class time table is present for selected date for teaching plan.
            DataRow[] rows2 = dt.Select("TT_DATE='" + date + "'");


            if (TPFlag == 1 && Convert.ToInt32(ViewState["TPlanYesNo"]) == 1 && rows2.Length == 0)
            {
                objCommon.DisplayMessage(updTeachingPlan, "No TeachingPlan for Attendance for the Day!", this.Page);
            }
            else
            {
                //OPEN POPUP
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            ddlTopicCovered.Visible = false;
            LinkButton lnk = sender as LinkButton;
            int _schemeType = (rbnNew.Checked ? 1 : 2);
            string argVal = lnk.CommandArgument;
            string[] args = argVal.Split(';');

            Session["_tpNo"] = args[0];
            string isAddStatus = string.Empty;
            DataSet ds1 = null;
            //======================== DECLARATION PART ====================================//

            int schemeno = 0, sem = 0, sectionno = 0, batchno = 0, extraclass = 0;
            int TPlanYesNo = 0, AttType = 1, TranType = 0, altCourseno = 0;
            DateTime date = DateTime.MinValue;

            LinkButton btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;
            HiddenField hdnScheme = item.FindControl("hfvSchme") as HiddenField;
            HiddenField hfvSem = item.FindControl("hfvSem") as HiddenField;
            HiddenField hfvSection = item.FindControl("hfvSection") as HiddenField;
            HiddenField hfvbatch = item.FindControl("hfvbatch") as HiddenField;
            HiddenField hfvslotNo = item.FindControl("hfvslotNo") as HiddenField;
            HiddenField hfvAttType = item.FindControl("hfvAttType") as HiddenField;
            HiddenField hfvLectTypeNo = item.FindControl("hdnLectTypeNO") as HiddenField;
            HiddenField hfvAltCourseno = item.FindControl("hfvAltCourseno") as HiddenField;
            HiddenField hdnExtraClass = item.FindControl("hdnExtraClass") as HiddenField;
            HiddenField hdnAttendanceNo = item.FindControl("hdnAttendanceNo") as HiddenField;
            HiddenField hdnTopicCoveredStatus = item.FindControl("hdnTopicCoveredStatus") as HiddenField;
            HiddenField hdnAttendanceStatus = item.FindControl("hdnAttendanceStatus") as HiddenField;
            schemeno = Convert.ToInt32(hdnScheme.Value);
            sem = Convert.ToInt32(hfvSem.Value);
            sectionno = Convert.ToInt32(hfvSection.Value);
            batchno = Convert.ToInt32(hfvbatch.Value);
            AttType = Convert.ToInt32(hfvAttType.Value);
            extraclass = Convert.ToInt32(hdnExtraClass.Value);

            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(DEGREENO,0)", "SCHEMENO=" + schemeno + "") == "" ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(DEGREENO,0)", "SCHEMENO=" + schemeno + "")));
            Session["degreeno"] = degreeno;
            Session["_schemeNo"] = hdnScheme.Value;
            Session["_semNo"] = hfvSem.Value;
            Session["_sectionNo"] = hfvSection.Value;
            Session["_batchNo"] = hfvbatch.Value;
            Session["_slotNo"] = hfvslotNo.Value;
            Session["_AltCourseno"] = hfvAltCourseno.Value;

            Session["_AttType"] = hfvAttType.Value;//hfvslotNo.Value;// lecture type(0 Regular) or (1 Alternate)
            Session["_TranType"] = hfvLectTypeNo.Value; //ACD_TT_CLASSTYPE_MASTER(mutual,engage,swapping)value
            TranType = Convert.ToInt32(hfvLectTypeNo.Value);
            Session["_ScheUaNo"] = "0"; // uano for submit att.
            //============================================================================//

            if (!lnk.ToolTip.Equals(string.Empty))
            {
                int sessionno = 0;
                if (rdoCore.Checked == true)
                {
                    sessionno = Convert.ToInt32(ddlColgSession.SelectedValue);
                }
                else if (rdoGlobalElective.Checked == true)
                {
                    sessionno = Convert.ToInt32(ddlSessionGlobal.SelectedValue);
                }
                int uano = Convert.ToInt32(Session["userno_Faculty"]);
                int courseno = Convert.ToInt32(lnk.ToolTip);
                int _slotNo = Convert.ToInt32(hfvslotNo.Value);
                ViewState["Courseno"] = courseno;

                //  hdnSlotVal.Value = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_TT_SLOT", "LEN(CONCAT((DATEDIFF(Minute,CONVERT(NVARCHAR(5),TIMEFROM),CONVERT(NVARCHAR(5),TIMETO))/60),':',(DATEDIFF(Minute,CONVERT(NVARCHAR(5),TIMEFROM),CONVERT(NVARCHAR(5),TIMETO))%60))) TotalHours", "SLOTNO=" + _slotNo)) > 5 ? "1:00" : objCommon.LookUp("ACD_ACADEMIC_TT_SLOT", "CONCAT((DATEDIFF(Minute,CONVERT(NVARCHAR(5),TIMEFROM),CONVERT(NVARCHAR(5),TIMETO))/60),':',(DATEDIFF(Minute,CONVERT(NVARCHAR(5),TIMEFROM),CONVERT(NVARCHAR(5),TIMETO))%60)) TotalHours", "SLOTNO=" + _slotNo);

                altCourseno = (TranType == 2 ? Convert.ToInt32(hfvAltCourseno.Value) : courseno);// alt att. mu
                ViewState["Alt_Courseno"] = altCourseno;
                //======================== THIS IS REGULAR ATTENDANCE PATCH ==========================//

                if (AttType == 0)//(0 <--Regular, 1 <-- Alternet) AttType used to check selected course(lecture,slot) is regular or alternate.
                {
                    #region regular att..
                    ddlClassType.Enabled = false;

                    //Comment by Mahesh on Dated 18-05-2021
                    //isAddStatus = objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(IS_ADTEACHER,0)", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + "  AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ") AND ISNULL(CANCEL,0)=0 AND (UA_NO=" + uano + " OR ADTEACHER=" + uano + ")");


                    //if (string.IsNullOrEmpty(isAddStatus) || isAddStatus == "0" || isAddStatus == string.Empty)
                    //{
                    //    uano = objCommon.LookUp("ACD_COURSE_TEACHER", "UA_NO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + " AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ")  AND ISNULL(IS_ADTEACHER,0)=1 AND ISNULL(CANCEL,0)=0") == string.Empty ? uano : Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "UA_NO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + " AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ") AND ISNULL(CANCEL,0)=0"));
                    //}


                    Session["AttUaNo"] = uano;//session["AttUaNo"] stored the uano of selected course Main teacher.(while submitting att.send this uano). 

                    txtCourse.Text = lnk.Text.Trim();
                    txtCourse.ToolTip = lnk.ToolTip;
                    txtLectDate.Text = Convert.ToDateTime(ViewState["date"]).ToString("dd/MM/yyyy");
                    date = Convert.ToDateTime(ViewState["date"]);

                    this.BindStatus();
                    //TPlanYesNo = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + sessionno + " AND ISNULL(ACTIVE,0)=1 AND SCHEMETYPE=" + _schemeType) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + sessionno + " AND ISNULL(ACTIVE,0)=1 AND SCHEMETYPE=" + _schemeType));
                    if (Convert.ToInt32(ViewState["TPlanYesNo"]) == 1)// if(1) attendance base on teaching plan then.. -- && extraclass != 1
                    {
                        // txtTopcDesc.Visible = true;
                        ddlTopicCovered.Visible = true;
                        divTopicCoveredStatus.Visible = true;
                        this.BindTopicCovered();
                        // ddlTopicCovered.SelectedValue = hdnAttendanceNo.Value == null || hdnAttendanceNo.Value == "" ? "0" : hdnAttendanceNo.Value.ToString();
                        ddltopicstatus.SelectedValue = hdnTopicCoveredStatus.Value == null || hdnTopicCoveredStatus.Value == "" || hdnTopicCoveredStatus.Value == "0" ? "1" : hdnTopicCoveredStatus.Value.ToString();
                        ddlStatus.SelectedValue = hdnAttendanceStatus.Value == null || hdnAttendanceStatus.Value == "" || hdnAttendanceStatus.Value == "0" ? "1" : hdnAttendanceStatus.Value.ToString();
                        txtTopcDesc.Visible = false;
                        //txtTopcDesc.Text = objCommon.LookUp("ACD_COURSE_TEACHER T INNER JOIN ACD_TEACHINGPLAN TP ON (T.SESSIONNO = TP.SESSIONNO AND T.SEMESTERNO = TP.TERM AND (T.CCODE  = TP.CCODE AND T.COURSENO = TP.COURSENO) AND T.UA_NO = TP.UA_NO)", "DISTINCT TP.TOPIC_COVERED", "(T.UA_NO=" + uano + " OR T.ADTEACHER =" + uano + ") AND T.COURSENO =" + courseno + "  AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND ISNULL(T.CANCEL,0)=0 AND T.SESSIONNO =" + sessionno + "  AND CAST(TP.[DATE] AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)");
                        ddlClassType.SelectedValue = TranType.ToString();
                        if (extraclass == 1) //Added By Rishabh
                        {
                            ddlClassType.Enabled = false;
                        }
                        hdnTeachingPlanStatus.Value = "1";

                        if (string.IsNullOrEmpty(hdnAttendanceNo.Value))
                            ddlTopicCovered.DataValueField = "0";
                        else
                        {
                            if (ddlTopicCovered.Items.Count > 0)
                            {
                                foreach (ListItem itm in ddlTopicCovered.Items)
                                {
                                    if (hdnAttendanceNo.Value.Contains(itm.Value))
                                        itm.Selected = true;
                                }
                            }
                            else
                                return;
                        }
                    }
                    else
                    {
                        //====================================================//
                        ds1 = objAttController.GetSubjectDetails(sessionno, schemeno, sem, sectionno, batchno, courseno, _slotNo, uano, Convert.ToDateTime(date.ToString("dd/MM/yyyy")), 0);

                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                txtTopcDesc.Visible = true;
                                txtTopcDesc.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TOPIC_COVERED"]);//stored uano of faculty who has been submitted att.
                                ddlClassType.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CLASS_TYPE"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["CLASS_TYPE"]);
                                ddlStatus.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ATTENDANCE_STATUS"]) == null ? "1" : Convert.ToString(ds1.Tables[0].Rows[0]["ATTENDANCE_STATUS"]);
                                ddlCONumber.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CO_NUM"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["CO_NUM"]);
                                ddltopicstatus.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["TC_STATUS"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["TC_STATUS"]);
                                //LectStatus
                                if (extraclass == 1) //Added By Rishabh
                                {
                                    ddlClassType.Enabled = false;
                                }
                                divTopicCoveredStatus.Visible = false;
                            }
                            else
                            {
                                txtTopcDesc.Text = string.Empty;
                                txtTopcDesc.Visible = true;
                                divTopicCoveredStatus.Visible = false;
                                //PopulateDropDownList();
                                ddlClassType.SelectedValue = TranType.ToString();
                                if (extraclass == 1)
                                {
                                    ddlClassType.Enabled = false;
                                }
                                ddlStatus.SelectedValue = "1";
                                ddlCONumber.SelectedValue = "0";
                                ds1 = null;
                            }
                        }
                        else
                        {
                            txtTopcDesc.Text = string.Empty;
                            txtTopcDesc.Visible = true;
                            //PopulateDropDownList();
                            ddlClassType.SelectedValue = TranType.ToString();
                            ddlStatus.SelectedValue = "1";
                            ddlCONumber.SelectedValue = "0";
                            ds1 = null;
                        }
                        //txtTopcDesc.Text = objCommon.LookUp("ACD_ATTENDANCE A1 INNER JOIN ACD_COURSE_TEACHER T ON T.COURSENO = A1.COURSENO AND T.SCHEMENO = A1.SCHEMENO AND T.SEMESTERNO = A1.SEMESTERNO AND T.SESSIONNO = A1.SESSIONNO AND ISNULL(A1.SECTIONNO,0)=ISNULL(T.SECTIONNO,0) AND ISNULL(A1.BATCHNO,0)=ISNULL(T.BATCHNO,0)", "DISTINCT A1.TOPIC_COVERED", "(T.UA_NO =" + uano + " OR T.ADTEACHER =" + uano + ") AND ISNULL(CANCEL,0)=0 AND T.COURSENO =" + courseno + " AND ISNULL(SLOTNO,0)=" + _slotNo + " AND ISNULL(T.SECTIONNO,0) =" + sectionno + " AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND T.SESSIONNO =" + sessionno + " AND CAST(A1.ATT_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)");
                        //LectStatus = objCommon.LookUp("ACD_ATTENDANCE A1 INNER JOIN ACD_COURSE_TEACHER T ON T.COURSENO = A1.COURSENO AND T.SCHEMENO = A1.SCHEMENO AND T.SEMESTERNO = A1.SEMESTERNO AND T.SESSIONNO = A1.SESSIONNO AND ISNULL(A1.SECTIONNO,0)=ISNULL(T.SECTIONNO,0) AND ISNULL(A1.BATCHNO,0)=ISNULL(T.BATCHNO,0)", "DISTINCT ISNULL(A1.CLASS_TYPE,0)", "(T.UA_NO =" + uano + " OR T.ADTEACHER =" + uano + ") AND ISNULL(CANCEL,0)=0 AND T.COURSENO =" + courseno + " AND ISNULL(SLOTNO,0)=" + _slotNo + " AND ISNULL(T.SECTIONNO,0) =" + sectionno + " AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND T.SESSIONNO =" + sessionno + " AND CAST(A1.ATT_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A1 INNER JOIN ACD_COURSE_TEACHER T ON T.COURSENO = A1.COURSENO AND T.SCHEMENO = A1.SCHEMENO AND T.SEMESTERNO = A1.SEMESTERNO AND T.SESSIONNO = A1.SESSIONNO AND ISNULL(A1.SECTIONNO,0)=ISNULL(T.SECTIONNO,0) AND ISNULL(A1.BATCHNO,0)=ISNULL(T.BATCHNO,0)", "DISTINCT ISNULL(A1.CLASS_TYPE,0)", "(T.UA_NO =" + uano + " OR T.ADTEACHER =" + uano + ") AND ISNULL(CANCEL,0)=0 AND T.COURSENO =" + courseno + " AND ISNULL(SLOTNO,0)=" + _slotNo + " AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND ISNULL(T.SECTIONNO,0) =" + sectionno + " AND T.SESSIONNO =" + sessionno + " AND CAST(A1.ATT_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)"));
                        //rblStatus.SelectedValue = objCommon.LookUp("ACD_ATTENDANCE A1 INNER JOIN ACD_COURSE_TEACHER T ON T.COURSENO = A1.COURSENO AND T.SCHEMENO = A1.SCHEMENO AND T.SEMESTERNO = A1.SEMESTERNO AND T.SESSIONNO = A1.SESSIONNO AND ISNULL(A1.SECTIONNO,0)=ISNULL(T.SECTIONNO,0) AND ISNULL(A1.BATCHNO,0)=ISNULL(T.BATCHNO,0)", "DISTINCT ISNULL(A1.ATTENDANCE_STATUS,0)", "(T.UA_NO =" + uano + " OR T.ADTEACHER =" + uano + ") AND ISNULL(CANCEL,0)=0 AND T.COURSENO =" + courseno + " AND ISNULL(SLOTNO,0)=" + _slotNo + " AND ISNULL(T.SECTIONNO,0) =" + sectionno + " AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND T.SESSIONNO =" + sessionno + " AND CAST(A1.ATT_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)") == string.Empty ? "1" : objCommon.LookUp("ACD_ATTENDANCE A1 INNER JOIN ACD_COURSE_TEACHER T ON T.COURSENO = A1.COURSENO AND T.SCHEMENO = A1.SCHEMENO AND T.SEMESTERNO = A1.SEMESTERNO AND T.SESSIONNO = A1.SESSIONNO AND ISNULL(A1.SECTIONNO,0)=ISNULL(T.SECTIONNO,0) AND ISNULL(A1.BATCHNO,0)=ISNULL(T.BATCHNO,0)", "DISTINCT ISNULL(A1.ATTENDANCE_STATUS,0)", "(T.UA_NO =" + uano + " OR T.ADTEACHER =" + uano + ")  AND ISNULL(CANCEL,0)=0 AND T.COURSENO =" + courseno + " AND ISNULL(SLOTNO,0)=" + _slotNo + " AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND ISNULL(T.SECTIONNO,0) =" + sectionno + " AND T.SESSIONNO =" + sessionno + " AND CAST(A1.ATT_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)");
                        //====================================================//

                        //if (LectStatus == 1)//Attendance Status
                        //{
                        //    rbRegular.Checked = false;
                        //}
                        //else
                        //{
                        //    rbRegular.Checked = true;
                        //}
                        hdnTeachingPlanStatus.Value = "0";
                    }
                    #endregion regular att..
                }
                //============================ END REGULAR ATT.=======================================//

                //======================== THIS IS ALTERNATE ATTENDANCE PATCH ==========================//
                else
                {
                    #region alternate att..
                    ddlClassType.Enabled = false;
                    date = Convert.ToDateTime(ViewState["date"]);

                    if (TranType == 2)// (2) <--Mutual
                    {
                        Session["_ScheUaNo"] = uano;// Mutual credits goes to alt. Faculty.
                        // stored (schedule ua_no)fac. uano to get the students for selected course.
                        Session["AttUaNo"] = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "SCHEDULE_UANO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND TAKEN_COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + " AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ")  AND TAKEN_UANO=" + uano + " AND ISNULL(TRAN_SLOTNO,0)=" + _slotNo + " AND ISNULL(CANCEL,0)=0 and CAST(ATTENDANCE_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "SCHEDULE_UANO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND TAKEN_COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + " AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ")  AND TAKEN_UANO=" + uano + " AND ISNULL(TRAN_SLOTNO,0)=" + _slotNo + " AND ISNULL(CANCEL,0)=0 and CAST(ATTENDANCE_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)"));
                    }
                    else if (TranType == 3)//(3) <-- Engage,
                    {

                        // stored (schedule ua_no)fac. uano to get the students for selected course.
                        Session["AttUaNo"] = objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "SCHEDULE_UANO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND TAKEN_COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + " AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ")  AND TAKEN_UANO=" + uano + " AND ISNULL(TRAN_SLOTNO,0)=" + _slotNo + " AND ISNULL(CANCEL,0)=0 and CAST(ATTENDANCE_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ALTERNATE_ATTENDANCE", "SCHEDULE_UANO", "SESSIONNO=" + sessionno + " AND SCHEMENO=" + schemeno + " AND TAKEN_COURSENO=" + courseno + " AND SEMESTERNO=" + sem + " AND SECTIONNO=" + sectionno + " AND (BATCHNO =" + batchno + " OR BATCHNO =" + batchno + ")  AND TAKEN_UANO=" + uano + " AND ISNULL(TRAN_SLOTNO,0)=" + _slotNo + " AND ISNULL(CANCEL,0)=0 and CAST(ATTENDANCE_DATE AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)"));
                        Session["_ScheUaNo"] = Session["AttUaNo"].ToString();// Engage credits goes to Main Faculty(Schedule Uano).
                    }
                    else //(4) <-- Swapping.
                    {
                        // for swapping alte type stored login fac. uano.
                        Session["AttUaNo"] = uano;
                        Session["_ScheUaNo"] = uano;
                    }

                    txtCourse.Text = lnk.Text.Trim();
                    txtCourse.ToolTip = lnk.ToolTip;
                    txtLectDate.Text = Convert.ToDateTime(ViewState["date"]).ToString("dd/MM/yyyy");
                    date = Convert.ToDateTime(ViewState["date"]);

                    this.BindStatus();

                    TPlanYesNo = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + sessionno + " AND ISNULL(ACTIVE,0)=1") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + sessionno + " AND ISNULL(ACTIVE,0)=1"));
                    if (TPlanYesNo == 1)// if(1) attendance base on teaching plan then..
                    {

                        // Changes pending......  

                        this.BindTopicCovered();
                        //  txtTopcDesc.Text = objCommon.LookUp("ACD_COURSE_TEACHER T INNER JOIN ACD_TEACHINGPLAN TP ON (T.SESSIONNO = TP.SESSIONNO AND T.SEMESTERNO = TP.TERM AND (T.CCODE  = TP.CCODE AND T.COURSENO = TP.COURSENO) AND T.UA_NO = TP.UA_NO)", "DISTINCT TP.TOPIC_COVERED", "(T.UA_NO=" + uano + " OR T.ADTEACHER =" + uano + ") AND T.COURSENO =" + courseno + "  AND (T.BATCHNO =" + batchno + " OR T.BATCHNO =" + batchno + ") AND ISNULL(T.CANCEL,0)=0 AND T.SESSIONNO =" + sessionno + "  AND CAST(TP.[DATE] AS DATE) = CAST(CONVERT(DATE,'" + date.ToString("dd/MM/yyyy") + "',103) AS DATE)");
                        ds1 = objAttController.GetSubjectDetails(sessionno, schemeno, sem, sectionno, batchno, courseno, _slotNo, uano, date, 1);
                        //if (ds1 != null && ds1.Tables.Count > 0)
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                //txtTopcDesc.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TOPIC_COVERED"]);//stored uano of faculty who has been submitted att.
                                ddlClassType.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CLASS_TYPE"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["CLASS_TYPE"]);
                                ddlStatus.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ATTENDANCE_STATUS"]) == null ? "1" : Convert.ToString(ds1.Tables[0].Rows[0]["ATTENDANCE_STATUS"]); //Added by Rishabh on 29/02/2024 for binding selected value as attendance status
                                ddlCONumber.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CO_NUM"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["CO_NUM"]);
                                //ddlTopicCovered.SelectedValue = hdnAttendanceNo.Value == null || hdnAttendanceNo.Value == "" ? "0" : hdnAttendanceNo.Value.ToString(); //Added by Rishabh on 28/02/2023 for alternate attendance with teaching plan
                                ddltopicstatus.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["TC_STATUS"]) == null || Convert.ToString(ds1.Tables[0].Rows[0]["TC_STATUS"]) == "0" ? "1" : Convert.ToString(ds1.Tables[0].Rows[0]["TC_STATUS"]); //Added by rishabh on 05/05/23 - topic covered status
                                txtTopcDesc.Visible = false;
                                ddlTopicCovered.Visible = true;
                                divTopicCoveredStatus.Visible = true;
                            }
                            else
                            {
                                txtTopcDesc.Text = string.Empty;
                                ddlClassType.SelectedValue = TranType.ToString();
                                ddlStatus.SelectedValue = "1";
                                ddlCONumber.SelectedValue = "0";
                                ds1 = null;
                                txtTopcDesc.Visible = true;
                                ddlTopicCovered.Visible = false;
                                divTopicCoveredStatus.Visible = false;
                            }
                        }
                        else
                        {
                            txtTopcDesc.Text = string.Empty;
                            ddlClassType.SelectedValue = TranType.ToString();
                            ddlStatus.SelectedValue = "1";
                            ddlCONumber.SelectedValue = "0";
                            ds1 = null;
                            txtTopcDesc.Visible = false;
                            ddlTopicCovered.Visible = true;
                            divTopicCoveredStatus.Visible = true;
                            ddltopicstatus.SelectedValue = hdnTopicCoveredStatus.Value == null || hdnTopicCoveredStatus.Value == "" || hdnTopicCoveredStatus.Value == "0" ? "1" : hdnTopicCoveredStatus.Value.ToString();
                            //ddlTopicCovered.SelectedValue = hdnAttendanceNo.Value == null || hdnAttendanceNo.Value == "" ? "0" : hdnAttendanceNo.Value.ToString(); //Added by Rishabh on 28/02/2023 for alternate attendance with teaching plan
                        }
                        if (string.IsNullOrEmpty(hdnAttendanceNo.Value))
                            ddlTopicCovered.DataValueField = "0";
                        else
                        {
                            if (ddlTopicCovered.Items.Count > 0)
                            {
                                foreach (ListItem itm in ddlTopicCovered.Items)
                                {
                                    if (hdnAttendanceNo.Value.Contains(itm.Value))
                                        itm.Selected = true;
                                }
                            }
                            else
                                return;
                        }
                    }
                    else
                    {
                        //===========================================//
                        ds1 = objAttController.GetSubjectDetails(sessionno, schemeno, sem, sectionno, batchno, courseno, _slotNo, uano, date, 1);
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            txtTopcDesc.Visible = true;
                            txtTopcDesc.Text = Convert.ToString(ds1.Tables[0].Rows[0]["TOPIC_COVERED"]);//stored uano of faculty who has been submitted att.
                            ddlClassType.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CLASS_TYPE"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["CLASS_TYPE"]);
                            ddlStatus.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ATTENDANCE_STATUS"]) == null ? "1" : Convert.ToString(ds1.Tables[0].Rows[0]["ATTENDANCE_STATUS"]);
                            ddlCONumber.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["CO_NUM"]) == null ? "0" : Convert.ToString(ds1.Tables[0].Rows[0]["CO_NUM"]);
                        }
                        else
                        {
                            txtTopcDesc.Text = string.Empty;
                            ddlClassType.SelectedValue = TranType.ToString();
                            ddlStatus.SelectedValue = "1";
                            ddlCONumber.SelectedValue = "0";
                            ds1 = null;
                            if (Convert.ToInt32(ViewState["TPlanYesNo"]) == 0)
                            {
                                txtTopcDesc.Visible = true;
                                ddlTopicCovered.Visible = false;
                                divTopicCoveredStatus.Visible = false;
                            }
                            else
                            {
                                ddlTopicCovered.Visible = true;
                                txtTopcDesc.Visible = false;
                                divTopicCoveredStatus.Visible = true;
                            }

                        }
                    }
                    #endregion alternate att..
                }
                //================================ END ALTERNATE ATT. ===========================================//
                int is_Tutorial = rdoTutorial.Checked ? 1 : 0;
                DataSet ds = null;
                if (AttType == 0)// regular
                {
                    if (rdoCore.Checked == true)
                    {
                        ds = objAttController.GetStudentFacultywiseAttendanceModified(sessionno, uano, courseno, date, _schemeType, schemeno, sem, sectionno, batchno, _slotNo, courseno, Session["college_id_att"].ToString(), Convert.ToInt32(Session["OrgId"]), is_Tutorial);//college_id added by Dileep Kare on 10.04.2021
                    }
                    else if (rdoGlobalElective.Checked == true)
                    {
                        ds = objAttController.GetStudentFacultywiseAttendanceModifiedGlobalElective(sessionno, uano, courseno, date, _schemeType, schemeno, sem, sectionno, batchno, _slotNo, courseno, Convert.ToInt32(Session["OrgId"]));//college_id added by Dileep Kare on 10.04.2021

                    }
                }
                else// alternate
                {
                    if (rdoCore.Checked == true)
                    {
                        ds = objAttController.GetStudentFacultywiseAttendanceModified(sessionno, Convert.ToInt32(Session["AttUaNo"]), altCourseno, date, _schemeType, schemeno, sem, sectionno, batchno, _slotNo, courseno, Session["college_id_att"].ToString(), Convert.ToInt32(Session["OrgId"]), is_Tutorial);//college_id added by Dileep Kare on 10.04.2021
                    }
                    else if (rdoGlobalElective.Checked == true)
                    {
                        ds = objAttController.GetStudentFacultywiseAttendanceModifiedGlobalElective(sessionno, Convert.ToInt32(Session["AttUaNo"]), altCourseno, date, _schemeType, schemeno, sem, sectionno, batchno, _slotNo, courseno, Convert.ToInt32(Session["OrgId"]));//college_id added by Dileep Kare on 10.04.2021

                    }
                }
                if (ds.Tables[0].Rows.Count == 0)
                {
                    objCommon.DisplayMessage(updTeachingPlan, "No student alloted for this course!", this.Page);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);

                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    pnlAttendenceEntry.Visible = false;
                    divCalendar.Visible = true;
                    spanNote.Visible = true;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "hideModal();", true);
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]), lvStudent);//Set label - 
                    pnlAttendenceEntry.Visible = true;
                    spanNote.Visible = false;
                    divCalendar.Visible = false;
                    //**btnSubmit.Enabled = true;
                    if (Convert.ToInt32(Session["userno_Faculty"]) == Convert.ToInt32(Session["AttSubUano"]) || string.IsNullOrEmpty(Session["AttSubUano"].ToString()) || Session["AttSubUano"].ToString() == "0")
                    {
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnSubmit.Enabled = false;
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        Label lblhead1 = this.lvStudent.Controls[0].FindControl("lblhead1") as Label;
                        Label lblhead2 = this.lvStudent.Controls[0].FindControl("lblhead2") as Label;
                        Label lblhead3 = this.lvStudent.Controls[0].FindControl("lblhead3") as Label;
                        Label lblhead4 = this.lvStudent.Controls[0].FindControl("lblhead4") as Label;
                        Label lblhead5 = this.lvStudent.Controls[0].FindControl("lblhead5") as Label;
                        Label lblhead6 = this.lvStudent.Controls[0].FindControl("lblhead6") as Label;
                        Label lblhead7 = this.lvStudent.Controls[0].FindControl("lblhead7") as Label;
                        lblhead1.Text = ""; lblhead2.Text = ""; lblhead3.Text = ""; lblhead4.Text = ""; lblhead5.Text = "";
                        lblhead6.Text = ""; lblhead7.Text = "";

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                lblhead1.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                            if (i == 1)
                            {
                                lblhead2.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                            if (i == 2)
                            {
                                lblhead3.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                            if (i == 3)
                            {
                                lblhead4.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                            if (i == 4)
                            {
                                lblhead5.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                            if (i == 5)
                            {
                                lblhead6.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                            if (i == 6)
                            {
                                lblhead7.Text = ds.Tables[1].Rows[i]["HEADER"].ToString();
                            }
                        }
                        int index = 0;
                        for (int s = 0; s < ds.Tables[0].Rows.Count; s++)
                        {
                            foreach (ListViewDataItem lvHead in lvStudent.Items)
                            {

                                Label lbl1 = lvHead.FindControl("lbl1") as Label;
                                Label lbl2 = lvHead.FindControl("Label1") as Label;
                                Label lbl3 = lvHead.FindControl("Label2") as Label;
                                Label lbl4 = lvHead.FindControl("Label3") as Label;
                                Label lbl5 = lvHead.FindControl("Label4") as Label;
                                Label lbl6 = lvHead.FindControl("Label5") as Label;
                                Label lbl7 = lvHead.FindControl("Label6") as Label;

                                if (index < ds.Tables[0].Rows.Count)
                                {
                                    if (lblhead1.Text != string.Empty)
                                    {
                                        lbl1.Text = (ds.Tables[0].Rows[index][lblhead1.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead1.Text].ToString() == "") ? "A" : "P";
                                        lbl1.ForeColor = lbl1.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    if (lblhead2.Text != string.Empty)
                                    {
                                        lbl2.Text = (ds.Tables[0].Rows[index][lblhead2.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead2.Text].ToString() == "") ? "A" : "P";
                                        lbl2.ForeColor = lbl2.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    if (lblhead3.Text != string.Empty)
                                    {
                                        lbl3.Text = (ds.Tables[0].Rows[index][lblhead3.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead3.Text].ToString() == "") ? "A" : "P";
                                        lbl3.ForeColor = lbl3.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    if (lblhead4.Text != string.Empty)
                                    {
                                        lbl4.Text = (ds.Tables[0].Rows[index][lblhead4.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead4.Text].ToString() == "") ? "A" : "P";
                                        lbl4.ForeColor = lbl4.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    if (lblhead5.Text != string.Empty)
                                    {
                                        lbl5.Text = (ds.Tables[0].Rows[index][lblhead5.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead5.Text].ToString() == "") ? "A" : "P";
                                        lbl5.ForeColor = lbl5.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    if (lblhead6.Text != string.Empty)
                                    {
                                        lbl6.Text = (ds.Tables[0].Rows[index][lblhead6.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead6.Text].ToString() == "") ? "A" : "P";
                                        lbl6.ForeColor = lbl6.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    if (lblhead7.Text != string.Empty)
                                    {
                                        lbl7.Text = (ds.Tables[0].Rows[index][lblhead7.Text].ToString() == "0" || ds.Tables[0].Rows[index][lblhead7.Text].ToString() == "") ? "A" : "P";
                                        lbl7.ForeColor = lbl7.Text.Equals("A") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                }
                this.BindAttendance();
                //  this.ddlStatus_SelectedIndexChanged(new object(), new EventArgs());
                this.BindStudentCount();
                // this.BindTopicCovered();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_TIMETABLE_AttendanceEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Calender Event

    #region Event Methods
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "myfunction", "$(document).ready(function(){if ($('#tblStudent td').children().length > 0) {$(this).find('.fa').not('span + span').remove();}});", true);
        pnlAttendenceEntry.Visible = false;
        divCalendar.Visible = true;
        spanNote.Visible = true;
        if (rdoCore.Checked == true)
        {
            GetCourse();
        }
        else if (rdoGlobalElective.Checked == true)
        {
            GetCourseGlobalElective();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus cs = CustomStatus.Others;
        string result = string.Empty, smsFlag = string.Empty, emailFlag = string.Empty, mobNo = string.Empty, faEmailId = string.Empty;
        int _schemeType = (rbnNew.Checked ? 1 : 2);
        try
        {
            if (Session["_AttType"].Equals("1"))// alternate
            {
                if (Session["_TranType"].ToString().Equals("3"))//for Engage credits goes to main t.(scheduleno)
                {
                    objAttModel.UA_No = Convert.ToInt32(Session["_ScheUaNo"]);
                }
                else// for Mutual & Swapping credits goes to att. taken faculty.
                {
                    objAttModel.UA_No = Convert.ToInt32(Session["userno_Faculty"]);
                }
                objAttModel.ClassType = Convert.ToInt32(Session["_TranType"]);
            }
            else// regular
            {
                objAttModel.UA_No = Convert.ToInt32(Session["userno_Faculty"]);
                objAttModel.ClassType = Convert.ToInt32(ddlClassType.SelectedValue);// (rbRegular.Checked == true ? 0 : 1);
            }

            objAttModel.Att_date = Convert.ToDateTime(txtLectDate.Text);
            objAttModel.CourseNo = Convert.ToInt32(txtCourse.ToolTip);

            //if (ddlTopicCovered.SelectedIndex == 0)   // ADDED BY SAFAL GUPTA ON 06052021
            //{

            //    objAttModel.Topic_Covered = txtTopcDesc.Text.Trim();
            //}
            //else
            //{
            //    objAttModel.Topic_Covered = ddlTopicCovered.SelectedItem.Text;
            //}
            //objAttModel.Topic_Covered = ddlClassType.SelectedValue == "0" ? ddlTopicCovered.SelectedItem.Text : txtTopcDesc.Text.Trim();
            objAttModel.Curdate = DateTime.Now;
            objAttModel.StudID = string.Empty;
            objAttModel.Att_status = string.Empty;
            objAttModel.LectStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            objAttModel.College_Id = Convert.ToInt32(Session["college_id_att"]);//Added By Dileep Kare on 10.04.2021 

            objAttModel.CoNo = Convert.ToInt32(ddlCONumber.SelectedValue);//Course Object Number..

            objAttModel.SCHEMENO = (Session["_schemeNo"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["_schemeNo"]));
            objAttModel.SEMESTERNO = (Session["_semNo"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["_semNo"]));
            objAttModel.Sectionno = (Session["_sectionNo"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["_sectionNo"]));
            objAttModel.BatchNo = (Session["_batchNo"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["_batchNo"]));
            objAttModel.Slot = (Session["_slotNo"].ToString() == string.Empty ? 0 : Convert.ToInt32(Session["_slotNo"]));
            objAttModel.Tutorial = rdoTutorial.Checked ? 1 : 0;
            int TPlanYesNo = 0;

            //TPlanYesNo = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue) + " AND ISNULL(ACTIVE,0)=1 and ISNULL(SCHEMETYPE,0)=" + _schemeType) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(TEACHING_PLAN,0)", " SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue) + " AND ISNULL(ACTIVE,0)=1 and ISNULL(SCHEMETYPE,0)=" + _schemeType + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_att"]) + " AND ORGANIZATIONID=" + Session["OrgId"]));//COLLEGE_ID="+Convert.ToInt32(ddlInstitute.SelectedValue) Added By Dileep Kare on 10.04.2021
            //if (Convert.ToInt32(ViewState["TPlanYesNo"]) == 1)// if(TPlanYesNo=1)THEN attendance base on teaching plan..
            //{
            //    objAttModel.TpNo = Convert.ToInt32(ddlTopicCovered.SelectedValue) == null ? 0 : Convert.ToInt32(ddlTopicCovered.SelectedValue);
            //}
            //else
            //{
            //    objAttModel.TpNo = 0;
            //}

            objAttModel.TopicCoveredStatus = Convert.ToInt32(ddltopicstatus.SelectedValue) == null ? 0 : Convert.ToInt32(ddltopicstatus.SelectedValue); // Added By Rishabh on 02/03/2023

            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                CheckBox ckh = lvitem.FindControl("cbRow") as CheckBox;
                HiddenField hf = lvitem.FindControl("hdfIdNo") as HiddenField;
                HiddenField hfvLeave = lvitem.FindControl("hdfLeaveStatus") as HiddenField;
                TextBox txtLTime = lvitem.FindControl("txtLTime") as TextBox;
                if (ckh.Checked == true)
                {
                    objAttModel.StudID += hf.Value.ToString() + ",";
                    objAttModel.Att_status += "1,";
                    objAttModel.Att_LateTime += txtLTime.Text + ",";
                }
                else
                {
                    // for Absent student set '0'                   
                    objAttModel.StudID += hf.Value.ToString() + ",";
                    if (hfvLeave.Value == "1")
                        objAttModel.Att_status += "2,";
                    else
                        objAttModel.Att_status += "0,";
                    objAttModel.Att_LateTime += txtLTime.Text + ",";
                }
            }

            if (objAttModel.StudID.Substring(objAttModel.StudID.Length - 1) == ",")
                objAttModel.StudID = objAttModel.StudID.Substring(0, objAttModel.StudID.Length - 1);

            if (Convert.ToInt32(ViewState["TPlanYesNo"]) == 1)
            {
                foreach (ListItem itm in ddlTopicCovered.Items)
                {
                    if (itm.Selected)
                    {
                        objAttModel.Topic_Covered += itm.Text + "|";
                        objAttModel.TpNos += itm.Value + ",";
                    }
                }

                if (string.IsNullOrEmpty(objAttModel.TpNos) && (Convert.ToInt32(ddlStatus.SelectedValue) == 1 || Convert.ToInt32(ddlStatus.SelectedValue) == 2))
                {
                    objCommon.DisplayMessage(updTeachingPlan, "Please select Atleast one Topic.", this.Page);
                    return;
                }

                objAttModel.TpNos = objAttModel.TpNos.TrimEnd(',');
                objAttModel.Topic_Covered = objAttModel.Topic_Covered.TrimEnd('|');
            }
            else
            {
                objAttModel.Topic_Covered = txtTopcDesc.Text.Trim();
                objAttModel.TpNos = "0";
            }

            //Add Attendance Entry
            if (rdoCore.Checked == true)
            {
                objAttModel.Sessionno = Convert.ToInt32(ddlColgSession.SelectedValue);
                cs = (CustomStatus)objAttController.AddAttendance(objAttModel, Convert.ToInt32(Session["OrgId"]));
                result = "Added";
            }
            else if (rdoGlobalElective.Checked == true)
            {
                objAttModel.Sessionno = Convert.ToInt32(ddlSessionGlobal.SelectedValue);
                cs = (CustomStatus)objAttController.AddAttendanceGlobalElective(objAttModel, Convert.ToInt32(Session["OrgId"]));
                result = "Added";
            }

            if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updTeachingPlan, "Error!!", this.Page);
            }
            else
            {
                btnSubmit.Text = "Submit";
                objCommon.DisplayMessage(updTeachingPlan, "Attendance " + result + " Successfully!!", this.Page);

                //==============================================//
                //smsFlag = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(SMS_FACILITY,0)", "ACTIVE=1 AND SCHEMETYPE=" + _schemeType + " AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_att"]) + " AND ORGANIZATIONID=" + Session["OrgId"]);
                //emailFlag = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "ISNULL(EMAIL_FACILITY,0)", "ACTIVE=1 AND SCHEMETYPE=" + _schemeType + " AND SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_att"]) + " AND ORGANIZATIONID=" + Session["OrgId"]);
                //mobNo = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_NO=" + Convert.ToInt32(Session["userno_Faculty"]) + " AND ORGANIZATIONID=" + Session["OrgId"]);
                //faEmailId = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO=" + Convert.ToInt32(Session["userno_Faculty"]) + " AND ORGANIZATIONID=" + Session["OrgId"]);

                //if (Convert.ToBoolean(smsFlag) == true && !String.IsNullOrEmpty(mobNo))//send sms to faculty..
                //{
                //    this.SendSMS(mobNo, "Dear" + " " + "<b>" + Session["userfullname"] + "," + "</b><br /><br /> Attendance for <b>" + Session["courseName"].ToString() + "</b> has been submitted successfully on " + DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszzz"));
                //}
                //if (Convert.ToBoolean(emailFlag) == true && !String.IsNullOrEmpty(faEmailId))//send email to faculty..
                //{
                //    TransferToEmail(faEmailId, "Dear" + " " + "<b>" + Session["userfullname"] + "," + "</b><br /><br /> Attendance for <b>" + Session["courseName"].ToString() + "</b> has been submitted successfully on " + DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszzz"));
                //}
                //==============================================//
            }
            int altCourseno = 0;
            if (Session["_AttType"].Equals("1"))// alternate
            {
                if (Session["_TranType"].ToString().Equals("2"))//mutual send schedule facultyno for get the stud list.
                {
                    altCourseno = Convert.ToInt32(Session["_AltCourseno"]);
                }
                else
                {
                    altCourseno = Convert.ToInt32(txtCourse.ToolTip);
                }
            }
            else
            {
                altCourseno = Convert.ToInt32(txtCourse.ToolTip);
            }
            // while taking alt mutual att. send altCourseno[courseno 1st paramtr](Main Teacher courseno)to get students 
            // and txtCourse.ToolTip[altCourseno 2st paramtr] to get submitted stud. att. status
            //DataSet ds = objAttController.GetStudentFacultywiseAttendanceModified(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["AttUaNo"]), Convert.ToInt32(altCourseno), Convert.ToDateTime(ViewState["date"]), _schemeType, Convert.ToInt32(Session["_schemeNo"]), Convert.ToInt32(Session["_semNo"]), Convert.ToInt32(Session["_sectionNo"]), Convert.ToInt32(Session["_batchNo"]), Convert.ToInt32(Session["_slotNo"]), Convert.ToInt32(txtCourse.ToolTip), (Session["college_id_att"]).ToString(), Convert.ToInt32(Session["OrgId"]));
            int is_Tutorial = rdoTutorial.Checked ? 1 : 0;
            DataSet ds = new DataSet();
            if (rdoCore.Checked == true)
            {
                ds = objAttController.GetStudentFacultywiseAttendanceModified(Convert.ToInt32(ddlColgSession.SelectedValue), Convert.ToInt32(Session["AttUaNo"]), Convert.ToInt32(altCourseno), Convert.ToDateTime(ViewState["date"]), _schemeType, Convert.ToInt32(Session["_schemeNo"]), Convert.ToInt32(Session["_semNo"]), Convert.ToInt32(Session["_sectionNo"]), Convert.ToInt32(Session["_batchNo"]), Convert.ToInt32(Session["_slotNo"]), Convert.ToInt32(txtCourse.ToolTip), (Session["college_id_att"]).ToString(), Convert.ToInt32(Session["OrgId"]), is_Tutorial);
            }
            else if (rdoGlobalElective.Checked == true)
            {
                ds = objAttController.GetStudentFacultywiseAttendanceModifiedGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue), Convert.ToInt32(Session["AttUaNo"]), Convert.ToInt32(altCourseno), Convert.ToDateTime(ViewState["date"]), _schemeType, Convert.ToInt32(Session["_schemeNo"]), Convert.ToInt32(Session["_semNo"]), Convert.ToInt32(Session["_sectionNo"]), Convert.ToInt32(Session["_batchNo"]), Convert.ToInt32(Session["_slotNo"]), Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["OrgId"]));//college_id added by Dileep Kare on 10.04.2021

            }
            lvStudent.DataSource = ds;
            lvStudent.DataBind();

            txtODStudent.Text = string.Empty;
            txtAbsentStudent.Text = string.Empty;
            txtPresentStudent.Text = string.Empty;
            txtTotalStudent.Text = string.Empty;

            this.BindAttendance();
            //this.ddlStatus_SelectedIndexChanged(new object(), new EventArgs());
            this.BindStudentCount();
        }
        catch
        {
            throw;
        }
    }

    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;

            DataSet ds = objCommon.FillDropDown("Reff", "SMSSVCID", "SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + "www.SMSnMMS.co.in/sms.aspx" + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }
        }
        catch
        {
        }
    }
    public void TransferToEmail(string mailId, string text)
    {
        try
        {
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", "");

            // any address where the email will be sending
            var toAddress = EMAILID.Trim();
            //Password of your gmail address

            var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");
            // Passing the values and make a email formate to display

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "RFC-Attendance");
            msg.To.Add(new MailAddress(toAddress));
            msg.Subject = "Attendance Submitted Successfully";

            msg.Body = text + "<br/><br/><b>Note :</b> This is system generated email. Please do not reply to this email.<br/>";

            msg.IsBodyHtml = true;
            //smtp.enableSsl = "true";
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            { }
        }

        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlAttendenceEntry.Visible = false;
        divCalendar.Visible = true;
        spanNote.Visible = true;
    }
    // protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 3 || Convert.ToInt32(ddlStatus.SelectedValue) == 5)
        {
            CheckBox chkHead = lvStudent.FindControl("cbHead") as CheckBox;
            chkHead.Checked = false;
            chkHead.Enabled = false;
            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                CheckBox ckh = lvitem.FindControl("cbRow") as CheckBox;
                HiddenField hf = lvitem.FindControl("hdfIdNo") as HiddenField;
                TextBox txtLM = lvitem.FindControl("txtLTime") as TextBox;
                ckh.Checked = false;
                ckh.Enabled = false;
                txtLM.Enabled = false;
                this.BindStudentCount();
            }
            ddlCONumber.SelectedIndex = 0;
            ddlCONumber.Enabled = false;
            lblAttend.Visible = false;
            rfvtopcv.Enabled = false;
            txtTopcDesc.Text = string.Empty;
            txtTopcDesc.Enabled = false;
        }
        else
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowHideCopy();", true);
            }
            CheckBox chkHead = lvStudent.FindControl("cbHead") as CheckBox;
            chkHead.Checked = true;
            chkHead.Enabled = true;
            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                CheckBox ckh = lvitem.FindControl("cbRow") as CheckBox;
                HiddenField hf = lvitem.FindControl("hdfIdNo") as HiddenField;
                TextBox txtLM = lvitem.FindControl("txtLTime") as TextBox;

                HiddenField hdfLeaveStatus = lvitem.FindControl("hdfLeaveStatus") as HiddenField;
                HiddenField hdfAttStatus = lvitem.FindControl("hdfAttStatus") as HiddenField;
                HiddenField hdfAttdone = lvitem.FindControl("hdfAttdone") as HiddenField;

                if (Convert.ToInt32(hdfAttdone.Value) == 1)// if (hdfLeaveStatus.Value == "1")
                {
                    // if (hdfAttStatus.Value == "1")
                    if (hdfLeaveStatus.Value == "1")
                    {
                        ckh.Enabled = true;
                        ckh.Checked = false;
                        txtLM.Enabled = false;
                    }
                    else
                    {
                        ckh.Enabled = true;
                        ckh.Checked = true;
                        txtLM.Enabled = true;
                    }
                }
                if (Convert.ToInt32(hdfAttdone.Value) == 0)
                {
                    if (hdfAttStatus.Value == "0")// && hdfLeaveStatus.Value == "0")
                    {
                        ckh.Enabled = true;
                        ckh.Checked = true;
                        txtLM.Enabled = true;
                    }
                }
            }
            this.BindStudentCount();
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 4 || Convert.ToInt32(ddlStatus.SelectedValue) == 5 || Convert.ToInt32(ddlStatus.SelectedValue) == 6 || Convert.ToInt32(ddlStatus.SelectedValue) == 7)
            {
                ddlCONumber.SelectedIndex = 0;
                ddlCONumber.Enabled = false;
                txtTopcDesc.Text = string.Empty;
                lblAttend.Visible = false;
                rfvtopcv.Enabled = false;
                txtTopcDesc.Enabled = false;
            }
            else
            {
                ddlCONumber.Enabled = true;
                txtTopcDesc.Enabled = true;
                lblAttend.Visible = true;
                rfvtopcv.Enabled = true;
            }
        }
    }

    private void BindAttendance()
    {
        try
        {
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 3 || Convert.ToInt32(ddlStatus.SelectedValue) == 5)
            {
                CheckBox chkHead = lvStudent.FindControl("cbHead") as CheckBox;
                chkHead.Checked = false;
                chkHead.Enabled = false;
                foreach (ListViewDataItem lvitem in lvStudent.Items)
                {
                    CheckBox ckh = lvitem.FindControl("cbRow") as CheckBox;
                    HiddenField hf = lvitem.FindControl("hdfIdNo") as HiddenField;
                    TextBox txtLM = lvitem.FindControl("txtLTime") as TextBox;
                    ckh.Checked = false;
                    ckh.Enabled = false;
                    txtLM.Enabled = false;
                    this.BindStudentCount();

                    ddlCONumber.SelectedIndex = 0;
                    ddlCONumber.Enabled = false;
                    txtTopcDesc.Text = string.Empty;
                    txtTopcDesc.Enabled = false;
                }

            }
            else
            {
                CheckBox chkHead = lvStudent.FindControl("cbHead") as CheckBox;
                chkHead.Checked = true;
                chkHead.Enabled = true;
                foreach (ListViewDataItem lvitem in lvStudent.Items)
                {
                    CheckBox ckh = lvitem.FindControl("cbRow") as CheckBox;
                    HiddenField hf = lvitem.FindControl("hdfIdNo") as HiddenField;
                    TextBox txtLM = lvitem.FindControl("txtLTime") as TextBox;

                    HiddenField hdfLeaveStatus = lvitem.FindControl("hdfLeaveStatus") as HiddenField;
                    HiddenField hdfAttStatus = lvitem.FindControl("hdfAttStatus") as HiddenField;
                    HiddenField hdfAttdone = lvitem.FindControl("hdfAttdone") as HiddenField;

                    if (Convert.ToInt32(hdfAttdone.Value) == 1)// if (hdfLeaveStatus.Value == "1")
                    {
                        if (hdfAttStatus.Value == "1")
                        {
                            ckh.Checked = true;
                            txtLM.Enabled = true;
                        }
                        else
                        {
                            ckh.Checked = false;
                            txtLM.Enabled = false;
                        }
                    }
                    if (Convert.ToInt32(hdfAttdone.Value) == 0)
                    {
                        if (hdfAttStatus.Value == "0")// && hdfLeaveStatus.Value == "0")
                        {
                            ckh.Checked = true;
                            txtLM.Enabled = true;
                        }
                    }
                }
                this.BindStudentCount();
                if (Convert.ToInt32(ddlStatus.SelectedValue) == 4 || Convert.ToInt32(ddlStatus.SelectedValue) == 5 || Convert.ToInt32(ddlStatus.SelectedValue) == 6 || Convert.ToInt32(ddlStatus.SelectedValue) == 7)
                {
                    ddlCONumber.SelectedIndex = 0;
                    ddlCONumber.Enabled = false;
                    txtTopcDesc.Text = string.Empty;
                    txtTopcDesc.Enabled = false;
                }
                else
                {
                    ddlCONumber.Enabled = true;
                    txtTopcDesc.Enabled = true;
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            CheckBox ckhbx = e.Item.FindControl("cbRow") as CheckBox;
            HiddenField hdfLeaveStatus = e.Item.FindControl("hdfLeaveStatus") as HiddenField;
            HiddenField hdfAttStatus = e.Item.FindControl("hdfAttStatus") as HiddenField;
            HiddenField hdfAttdone = e.Item.FindControl("hdfAttdone") as HiddenField;
            //Added by RAJU B.
            if (Convert.ToInt32(hdfAttdone.Value) == 1)// if (hdfLeaveStatus.Value == "1")
            {
                if (hdfAttStatus.Value == "1")
                    ckhbx.Checked = true;
                else
                    ckhbx.Checked = false;
            }
            if (Convert.ToInt32(hdfAttdone.Value) == 0)
            {
                if (hdfAttStatus.Value == "0")// && hdfLeaveStatus.Value == "0")
                {
                    ckhbx.Checked = false;
                }
            }
            //if ((e.Item.ItemType == ListViewItemType.DataItem))
            //{
            //    ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            //    DataRow dr = ((DataRowView)dataItem.DataItem).Row;

            //    string lblhead1 = dr["lblhead1"].ToString();
            //    string lblhead2 = dr["lblhead2"].ToString();
            //    string lblhead3 = dr["lblhead3"].ToString();
            //    string lblhead4 = dr["lblhead4"].ToString();
            //    string lblhead5 = dr["lblhead5"].ToString();
            //    string lblhead6 = dr["lblhead6"].ToString();
            //    string lblhead7 = dr["lblhead7"].ToString();

            //    string Script = "";
            //    Script += "var arrOfHiddenColumns = [];";

            //    if (lblhead2 == "")
            //    {
            //        Script += "$('#myTable th:nth-child(4)').hide();$('#myTable td:nth-child(5)').hide();";
            //        Script += "arrOfHiddenColumns.push(5);";
            //    }
            //    if (lblhead3 == "")
            //    {
            //        Script += "$('#myTable th:nth-child(6)').hide();$('#myTable td:nth-child(5)').hide();";
            //        Script += "arrOfHiddenColumns.push(6);";
            //    }
            //    if (lblhead4 == "")
            //    {
            //        Script += "$('#myTable th:nth-child(7)').hide();$('#myTable td:nth-child(6)').hide();";
            //        Script += "arrOfHiddenColumns.push(6);";
            //    }
            //    if (lblhead5 == "")
            //    {
            //        Script += "$('#myTable th:nth-child(8)').hide();$('#myTable td:nth-child(7)').hide();";
            //        Script += "arrOfHiddenColumns.push(7);";
            //    }
            //    if (lblhead6 == "")
            //    {
            //        Script += "$('#myTable th:nth-child(9)').hide();$('#myTable td:nth-child(8)').hide();";
            //        Script += "arrOfHiddenColumns.push(8);";
            //    }
            //    if (lblhead7 == "")
            //    {
            //        Script += "$('#myTable th:nth-child(10)').hide();$('#myTable td:nth-child(9)').hide();";
            //        Script += "arrOfHiddenColumns.push(9);";
            //    }
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Src", Script, true);
            //}
        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        Calendar1.Dispose();
        //  this.PopulateDropDownList();
        //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
        this.GetDates();
        this.GetCourse();
        Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["userno_Faculty"] = ddlFaculty.SelectedValue;
        this.PopulateDropDownList();
        divTutorial.Visible = true;

        //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
        if (ddlColgSession.SelectedValue != null && !ddlColgSession.SelectedValue.ToString().Equals("Please Select"))
        {
            this.GetDates();
            this.GetCourse();
            attpanel.Visible = true;
        }
    }
    protected void rbnOld_CheckedChanged(object sender, EventArgs e)  //THIS IS FOR NON-CBCS SCHEME TYPE
    {
        Calendar1.Dispose();
        // this.PopulateDropDownList();
        //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
        this.GetDates();
        this.GetCourse();
        Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
    }

    protected void rbnNew_CheckedChanged(object sender, EventArgs e)  //THIS IS FOR CBCS SCHEME TYPE
    {
        Calendar1.Dispose();
        // this.PopulateDropDownList();
        //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
        this.GetDates();
        this.GetCourse();
        Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        dvAtt.Visible = false;
        dvRegister.Visible = true;
    }
    protected void PopulateData()
    {
        foreach (ListViewDataItem item in lvSubjectList.Items)
        {
            HiddenField hdnSection = item.FindControl("hfvSection") as HiddenField;
            Section = hdnSection.Value;
            HiddenField hdnSubID = item.FindControl("hfvsubID") as HiddenField;
            SubID = Convert.ToInt32(hdnSubID.Value);
        }
    }
    protected void btnDayWise_Click(object sender, EventArgs e)
    {
        //string[] fromDate = txtTodate.Text.Split('/');
        string[] fromDate = txtFromDate.Text.Split('/');
        string[] toDate = txtTodate.Text.Split('/');
        DateTime fromdate = Convert.ToDateTime(fromDate[0] + "/" + fromDate[1] + "/" + fromDate[2]);
        DateTime todate = Convert.ToDateTime(toDate[0] + "/" + toDate[1] + "/" + toDate[2]);

        //DateTime fromdate = Convert.ToDateTime(txtFromDate.Text);
        //DateTime todate = Convert.ToDateTime(txtTodate.Text);
        PopulateData();
        if (fromdate > todate)
        {
            objCommon.DisplayMessage(this.Page, "From Date always be less than To date. Please Enter proper Date range.", this.Page);
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
        }

        else
        {
            ShowDayWiseReport();
        }

    }
    private void ShowDayWiseReport()
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ccode = string.Empty;

            if (Convert.ToInt32(txtCourse.ToolTip) != 0)
            {
                ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + txtCourse.ToolTip);
                SubID = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(SUBID,0)", "COURSENO=" + txtCourse.ToolTip));
            }
            else
            {
                ccode = ccode.ToString();
            }

            int SchemeNo = 0;
            int CourseType = 0;
            if (Convert.ToInt32(txtCourse.ToolTip) != 0)
            {
                SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + txtCourse.ToolTip));
            }

            string degree = string.Empty;
            string branch = string.Empty;
            if (Convert.ToInt32(txtCourse.ToolTip) != 0)
            {
                degree = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "SCHEMENO=" + SchemeNo);
                branch = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO)BRANCH", "SCHEMENO=" + SchemeNo);
            }

            string ContentType = string.Empty;
            DataSet ds = null;
            StudentAttendanceController objAC = new StudentAttendanceController();
            if (rdoCore.Checked == true)
            {

                if (Session["usertype"].ToString() == "3")//teacher
                {
                    //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (Convert.ToInt32(txtCourse.ToolTip) != 0)
                    {
                        //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                        ds = objAttController.GetDayWiseData(Convert.ToInt32(ddlColgSession.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Session["_sectionNo"]), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                        //Convert.ToInt32(ddlAttFor.SelectedValue) Commented by Nikhil V.Lambe on 06/02/2021 as there is not needed in MAKAUT CODE
                    }
                    else
                    {
                        ds = objAttController.GetDayWiseData(Convert.ToInt32(ddlColgSession.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Session["_sectionNo"]), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());

                    }
                }
                else if (Session["usertype"].ToString() == "1")//admin
                {
                    //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (Convert.ToInt32(txtCourse.ToolTip) != 0)
                    {
                        ds = objAttController.GetDayWiseData(Convert.ToInt32(ddlColgSession.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Section), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());

                    }
                    else
                    {
                        ds = objAttController.GetDayWiseData(Convert.ToInt32(ddlColgSession.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Section), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());

                    }
                }
            }
            else if (rdoGlobalElective.Checked == true)
            {
                if (Session["usertype"].ToString() == "3")//teacher
                {
                    //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (Convert.ToInt32(txtCourse.ToolTip) != 0)
                    {
                        //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                        ds = objAttController.GetDayWiseDataGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Session["_sectionNo"]), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                        //Convert.ToInt32(ddlAttFor.SelectedValue) Commented by Nikhil V.Lambe on 06/02/2021 as there is not needed in MAKAUT CODE
                    }
                    else
                    {
                        ds = objAttController.GetDayWiseDataGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Session["_sectionNo"]), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());

                    }
                }
                else if (Session["usertype"].ToString() == "1")//admin
                {
                    //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (Convert.ToInt32(txtCourse.ToolTip) != 0)
                    {
                        ds = objAttController.GetDayWiseDataGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Section), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());

                    }
                    else
                    {
                        ds = objAttController.GetDayWiseDataGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue), SchemeNo, Convert.ToInt32(txtCourse.ToolTip), Convert.ToInt32(Session["userno_Faculty"].ToString()), SubID, Convert.ToInt32(Section), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, Convert.ToInt32(ViewState["batch"]), ccode.ToString());

                    }
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.RemoveAt(7);
                //ds.Tables[0].Columns.Remove("ROLLNO");
                //ds.Tables[0].Columns.Remove("IDNO");

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtFromDate.Text.Trim() + "_" + txtTodate.Text.Trim() + ".xls";
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
                objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        dvAtt.Visible = true;
        dvRegister.Visible = false;
    }

    protected void rdoTutorial_CheckedChanged(object sender, EventArgs e)
    {
        Calendar1.Dispose();
        // this.PopulateDropDownList();
        //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
        this.GetDates();
        this.GetCourse();
        Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
    }

    protected void rdoRegular_CheckedChanged(object sender, EventArgs e)
    {
        Calendar1.Dispose();
        // this.PopulateDropDownList();
        //GET START AND END DATE OF CURRENT SESSION IN ATTENDANCE CONFIG
        this.GetDates();
        this.GetCourse();
        Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
    }

    protected void ddlColgSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Calendar1.Dispose();
            if (ddlColgSession.SelectedIndex > 0)
            {
                this.GetDates();
                this.GetCourse();
                Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
            }
        }
        catch
        {
            throw;
        }
    }

    //Added By Rishabh B.on 05-09-2022
    [WebMethod()]
    public static string GetExistingSlotDetails(string LectureDate, int courseno)
    {
        AcdAttendanceController objAttController = new AcdAttendanceController();

        int ua_no = Convert.ToInt32(HttpContext.Current.Session["userno_Faculty"]);
        int degreeno = Convert.ToInt32(HttpContext.Current.Session["degreeno"]);
        int batchno = Convert.ToInt32(HttpContext.Current.Session["_batchNo"]);

        DataSet ds = objAttController.GetAttendanceSlot(LectureDate, courseno, ua_no, degreeno, batchno);
        var js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
        }
        return js.Serialize(rows);
    }

    [WebMethod]
    public static List<ListItem> GetTimeSlots(int slotno)
    {
        AcdAttendanceController objAttController = new AcdAttendanceController();
        int college_id = Convert.ToInt32(HttpContext.Current.Session["college_id_att"]);
        int degreeno = Convert.ToInt32(HttpContext.Current.Session["degreeno"]);

        SqlDataReader sdr = objAttController.FillTimeSlot(college_id, slotno, degreeno);
        List<ListItem> timeSlots = new List<ListItem>();

        while (sdr.Read())
        {
            timeSlots.Add(new ListItem
            {
                Value = sdr["SLOTNO"].ToString(),
                Text = sdr["LECTURESLOT"].ToString()
            });
        }
        return timeSlots;
    }

    [WebMethod]
    public static int CopyAttendance(int slotno, int att_no, int class_type, int att_status, string topic_desc, string Tpno)
    {
        AcdAttendanceController objAttController = new AcdAttendanceController();
        int outval = 0;

        outval = objAttController.CopyAttendacnce(slotno, att_no, class_type, att_status, topic_desc, Tpno);

        if (outval != null || outval != 0)
        {
            outval = 1;
        }
        else
        {
            outval = 0;
        }
        return Convert.ToInt32(outval);
    }
    //end   05-09-2022
    #region Global Elective
    protected void rdoCore_CheckedChanged(object sender, EventArgs e)
    {
        divClgSession.Visible = true;
        divGlobalSession.Visible = false;
        DataSet dsColgSession = objCourse.GetCollegeSessionForAttendance(Convert.ToInt32(Session["userno_Faculty"]));
        if (dsColgSession.Tables[0] != null && dsColgSession.Tables[0].Rows.Count > 0)
        {
            ddlColgSession.DataSource = dsColgSession.Tables[0];
            ddlColgSession.DataTextField = dsColgSession.Tables[0].Columns["COLLEGE_SESSION"].ToString();
            ddlColgSession.DataValueField = dsColgSession.Tables[0].Columns["SESSIONNO"].ToString();
            ddlColgSession.DataBind();
            ddlColgSession.SelectedIndex = 1;

            int college_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "COLLEGE_ID", "SESSIONNO=" + ddlColgSession.SelectedValue + ""));
            Session["college_id_att"] = college_id.ToString();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Data not found!!.", this.Page);
            return;
        }

        //divClgSession.Visible = false;
        try
        {
            Calendar1.Dispose();
            if (ddlSessionGlobal.SelectedIndex > 0)
            {
                this.GetDates();
                this.GetCourse();
                Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
            }
        }
        catch
        {
            throw;
        }
    }
    protected void rdoGlobalElective_CheckedChanged(object sender, EventArgs e)
    {
        divGlobalSession.Visible = true;
        divClgSession.Visible = false;
        DataSet dsSessionGlobal = objCourse.GetCollegeSessionForAttendanceGlobalElective(Convert.ToInt32(Session["userno_Faculty"]));
        if (dsSessionGlobal.Tables[0] != null && dsSessionGlobal.Tables[0].Rows.Count > 0)
        {
            ddlSessionGlobal.DataSource = dsSessionGlobal.Tables[0];
            ddlSessionGlobal.DataTextField = dsSessionGlobal.Tables[0].Columns["SESSION_NAME"].ToString();
            ddlSessionGlobal.DataValueField = dsSessionGlobal.Tables[0].Columns["SESSIONID"].ToString();
            ddlSessionGlobal.DataBind();
            ddlSessionGlobal.SelectedIndex = 1;

        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Data not found!!.", this.Page);
            return;
        }

        //divClgSession.Visible = false;
        try
        {
            Calendar1.Dispose();
            if (ddlSessionGlobal.SelectedIndex > 0)
            {
                this.GetDatesGlobalElective();
                this.GetCourseGlobalElective();
                Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
            }
        }
        catch
        {
            throw;
        }
    }
    private void GetDatesGlobalElective()
    {
        int _schemeType = (rbnNew.Checked ? 1 : 2);

        DataSet dsDate = objCourse.GetAttendanceConfigDataGlobalElective(Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["userno_Faculty"]), Convert.ToInt32(ddlSessionGlobal.SelectedValue));

        if (dsDate != null && dsDate.Tables[0].Rows.Count > 0)
        {
            string startDate = dsDate.Tables[0].Rows[0]["START_DATE"].ToString();
            string endDate = dsDate.Tables[0].Rows[0]["END_DATE"].ToString();
            string attLockDay = dsDate.Tables[0].Rows[0]["LOCK_ATT_DAYS"].ToString();

            ViewState["startDate"] = startDate;
            ViewState["endDate"] = endDate;
            ViewState["attLockDay"] = attLockDay;
        }
    }
    private void GetCourseGlobalElective()
    {
        try
        {
            int _schemeType = (rbnNew.Checked ? 1 : 2);
            int istutorial = rdoTutorial.Checked ? 1 : 0;
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            //=========== get all course of login faculty =====================//
            DataSet dsCalederCourse = objAttController.GetAllCoursesModifiedGlobalElective(Convert.ToInt32(Session["userno_Faculty"]), _schemeType, Convert.ToInt32(ddlSessionGlobal.SelectedValue), istutorial, OrgId);
            ViewState["dsCalederCourse"] = dsCalederCourse;//Insert Data Set in View State
            //ViewState["SessionNo"] = ddlSession.SelectedValue;

            //=========== get all Holiday from master =====================//
            DataSet dsHDay = objAttController.GetRestrictedCoursesGlobalElectiveModified(Convert.ToInt32(ddlSessionGlobal.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, istutorial);
            //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
            ViewState["dsHDay"] = dsHDay;//Insert Data Set in View State          

            //=========== get all alternate assinged courses to login faculty =====================//
            DataSet dsAlAtt = objAttController.GetAlternateAllottedCoursesGlobalElectiveModified(Convert.ToInt32(ddlSessionGlobal.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, istutorial, OrgId);
            //objCommon.FillDropDown("ACD_ALTERNATE_ATTENDANCE AL INNER JOIN ACD_COURSE C ON C.COURSENO=AL.TAKEN_COURSENO INNER JOIN ACD_SCHEME S ON S.SCHEMENO=AL.SCHEMENO INNER JOIN ACD_ATTENDANCE_CONFIG A ON A.SESSIONNO=AL.SESSIONNO AND A.DEGREENO=S.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE AND A.SEMESTERNO=AL.SEMESTERNO",
            //    "DISTINCT (SELECT DATEPART(DW,ATTENDANCE_DATE)-1)AltDayNO", "CCODE,ATTENDANCE_DATE,COURSE_NAME,START_DATE,END_DATE",
            //    "S.SCHEMETYPE=" + _schemeType + " AND ISNULL(CANCEL,0)=0 AND TAKEN_UANO=" + Convert.ToInt32(Session["userno_Faculty"].ToString()),
            //    "");
            ViewState["dsAlAtt"] = dsAlAtt;

            //=========== get all Lock Holiday from master =====================//
            DataSet dsLockHDay = objAttController.GetAllHolidaysGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue));
            //objCommon.FillDropDown("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "ACADEMIC_HOLIDAY_NAME,ACADEMIC_HOLIDAY_STDATE", "SESSIONNO=" + Convert.ToInt32(ddlColgSession.SelectedValue), "HOLIDAY_NO");
            ViewState["dsLockHDay"] = dsLockHDay;//Insert Data Set in View State  

            DataSet dsShiftTT = objAttController.GetAllShiftTTCoursesGlobalElectiveModified(Convert.ToInt32(ddlSessionGlobal.SelectedValue), Convert.ToInt32(Session["userno_Faculty"]), _schemeType, istutorial, OrgId);
            //objCommon.FillDropDown("ACD_TIME_TABLE_SHIFT TS INNER JOIN ACD_COURSE C ON C.COURSENO=TS.COURSENO INNER JOIN ACD_SCHEME S ON S.SCHEMENO=TS.SCHEMENO INNER JOIN ACD_ATTENDANCE_CONFIG A ON  A.SESSIONNO=TS.SESSIONNO AND A.DEGREENO=S.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE AND A.SEMESTERNO=TS.SEMESTERNO", "DISTINCT (SELECT DATEPART(DW,SHIFT_TT_DATE)-1)ShiftDayNO", "CCODE,TT_DAYNO,SHIFT_TT_DATE,COURSE_NAME,START_DATE,END_DATE", "S.SCHEMETYPE=" + _schemeType + " AND UA_NO=" + Convert.ToInt32(Session["userno_Faculty"].ToString()), "");
            ViewState["dsShiftTT"] = dsShiftTT;//Insert Data Set in View State 
        }
        catch
        {
            throw;
        }
    }
    //Global Elective Date 21-01-2023
    protected void ddlSessionGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        divClgSession.Visible = false;
        try
        {
            Calendar1.Dispose();
            if (ddlColgSession.SelectedIndex > 0)
            {
                this.GetDatesGlobalElective();
                this.GetCourseGlobalElective();
                Calendar1.DayRender += new DayRenderEventHandler(this.Calendar1_DayRender);
            }
        }
        catch
        {
            throw;
        }
    }
    //End
    #endregion

}