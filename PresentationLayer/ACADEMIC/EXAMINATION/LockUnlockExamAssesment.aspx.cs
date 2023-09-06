using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_EXAMINATION_LockUnlockExamAssesment : System.Web.UI.Page
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

            }

            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

            PopulateDropDownDeptMap();

        }
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

    #region College DropDownBind
    private void PopulateDropDownDeptMap()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollegeIdDepMap, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"].ToString() + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
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

    protected void ddlCollegeIdDepMap_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlcourse.SelectedIndex = 0;
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
                }
            }
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ISNULL (IS_ACTIVE,0)= 1", "SESSIONNO DESC");
            lvExamStatus.DataSource = null;
            lvExamStatus.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcourse.SelectedIndex = 0;
        
        if (Convert.ToInt32(Session["OrgId"]) == 7)
        {
            #region CheckActivity

            if (CheckActivity())
            {

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
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
            }
            else
            {
                //  objCommon.FillDropDownList(ddlcourse, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
            }
            lvExamStatus.DataSource = null;
            lvExamStatus.DataBind();
        }
    }
    private bool CheckActivity()
    {
        if (Convert.ToInt32(ViewState["usertype"]) == 7)
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
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlcourse, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
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
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlcourse, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
                    }
                }
            }
            else
            {
                //divenroll.Visible = false;
                //btnSearch.Visible = false;
                //btncancel.Visible = false;
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

            //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");

          //  DataSet DateSessionDs = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "substring(cast(SA.START_DATE as varchar),1,12)START_DATE,substring(cast(SA.END_DATE as varchar),1,12)END_DATE", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1", "");

            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO","AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1");
          
          
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
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlcourse, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
                    }
                }


                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(this.Page, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT SR   INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO)", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "CA.COURSENO");
                    }
                    else
                    {
                        //  objCommon.FillDropDownList(ddlcourse, "ACD_COURSE_TEACHER  C INNER JOIN ACD_COURSE CA ON (C.COURSENO= CA.COURSENO ) ", " DISTINCT C.COURSENO", "CA.COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"]+")", "C.COURSENO");//ua_prac
                        objCommon.FillDropDownList(ddlcourse, "ACD_STUDENT_RESULT  SR INNER JOIN ACD_COURSE CA ON (SR.COURSENO= CA.COURSENO ) ", " DISTINCT CA.COURSENO", "CA.CCODE +'- '+CA.COURSE_NAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR UA_NO_PRAC=" + Session["userno"] + ")", "CA.COURSENO");//ua_prac
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem dataRow in lvExamStatus.Items)
            {
                UpdateLockStatus(dataRow);
            }
            objCommon.DisplayMessage(this.updLockUnlock, "Lock/Unlock Done Successfully.", this);
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockUnlockExamComponent.btnSave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void UpdateLockStatus(ListViewDataItem dataRow)
    {
        int lockunlock = 0;
        bool lockunlock_status = ((CheckBox)dataRow.FindControl("chklock")).Checked;
        if (lockunlock_status == true)
        {
            lockunlock = 1;
        }
        else
        {
            lockunlock = 0;
        }
        try
        {
            int CollegeId = Convert.ToInt32(ViewState["college_id"]);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int courseno = Convert.ToInt32(ddlcourse.SelectedValue);
            int subexam = Convert.ToInt32(((Label)dataRow.FindControl("lblCourseNo")).ToolTip);
            int uano = Convert.ToInt32(((Label)dataRow.FindControl("lblFaculty")).ToolTip);
            string ipaddress = Convert.ToString(Session["ipAddress"].ToString()); 
            objexam.UpadteExamComponentLockStatus(lockunlock,CollegeId,SessionNo,courseno,uano,subexam,ipaddress);
        }
        catch (Exception ex)
        {
 
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        PopulateDropDownDeptMap();
        ddlCollegeIdDepMap.SelectedValue = "0";
    }
    private void Clear()
    {
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        lvExamStatus.DataSource = null;
        lvExamStatus.DataBind();
    }
    protected void ddlcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            if (ddlcourse.SelectedIndex > 0)
            {
                ds = objexam.ExamComponantLockStatus(Convert.ToInt32(ddlcourse.SelectedValue));
                lvExamStatus.DataSource = ds;
                lvExamStatus.DataBind();
            }
            else
            {
                lvExamStatus.DataSource = null;
                lvExamStatus.DataBind();
            }
        }
        catch (Exception ex) 
        {
            throw ex;
        }
    }
}