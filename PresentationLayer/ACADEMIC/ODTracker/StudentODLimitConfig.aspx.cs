using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using BusinessLogicLayer.BusinessLogic.Academic;
using BusinessLogicLayer.BusinessEntities.Academic;

public partial class ACADEMIC_ODTracker_StudentODLimitConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    ODTrackerController ObjTrackerCon = new ODTrackerController();
    ODTracker ObjODTracker = new ODTracker();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

            }             
            PopulateDropDown();
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header
        }
    }
    
    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        txtAllowedDays.Text = "";
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        if (ddlAdmbatch.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "ADMBATCH = " + ddlAdmbatch.SelectedValue, "SCHEMENO DESC");

            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_STUDENT WHERE ADMBATCH = " + ddlAdmbatch.SelectedValue + ")", "SCHEMENO DESC");
        }
        else
        { }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmbatch, "acd_admbatch", "BATCHNO", "BATCHNAME", "ISNULL(ACTIVESTATUS,0)=1", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
 
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        txtAllowedDays.Text = "";
        lvCourse.DataSource = null;
        lvCourse.DataBind();

        if (ddlScheme.SelectedIndex > 0)
        {
            DataSet ds = new DataSet();
            //ds = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "SHORTNAME,COURSE_NAME,SCHEMENO,CCODE,SRNO,SUBID,ELECT,GLOBALELE,CREDITS,LECTURE,THEORY,PRACTICAL,MAXMARKS_I,MAXMARKS_E,MINMARKS,S1MAX,S2MAX,S3MAX,S4MAX,S5MAX,S6MAX,S7MAX,S8MAX,S9MAX,S10MAX,ASSIGNMAX,S1MIN,S2MIN,S3MIN,S4MIN,S5MIN,S6MIN,S7MIN,S8MIN,S9MIN,S10MIN,ASSIGNMIN,DRAWING,LEVELNO,GROUPNO,PREREQUISITE,PREREQUISITE_CREDIT,PREREQUISITE_LEVELNO,OFFERED,SEMESTERNO,COLLEGE_CODE,GRADE,MINGRADE,SPECIALISATIONNO,BOS_DEPTNO,PAPER_HRS,QTY,REQ_LEVEL,SCALEDN_MARK,CGROUPNO,CORE_SUBID,CATEGORYNO,INTAKE,Ename,OrganizationId,DEPTNO,CAPACITY,ACTIVE,CREATEDBY,MODIFIED_BY,MODIFIED_DATE,DATE,COURSE_RELEVANCE,REMARK,IS_SPECIAL,MINMARK_I,TOTAL_MARK", "SCHEMENO = " + ddlScheme.SelectedValue, "");
            //ds = objCommon.FillDropDown("ACD_COURSE A INNER JOIN ACD_SCHEME B ON A.SCHEMENO = B.SCHEMENO LEFT JOIN ACD_OD_TRACKER_STUDENT_OD_LIMIT_CONFIG C ON B.SCHEMENO = C.SCHEMENO AND ADM_BATCH_NO = ADMBATCH", "COURSENO", "ISNULL(IS_ACTIVE,0) IS_ACTIVE,CASE WHEN C.SCHEMENO IS NOT NULL THEN 1 ELSE 0 END IS_CONFIG_DONE,CASE WHEN C.SCHEMENO IS NOT NULL THEN ALLOWED_OD_DAYS ELSE 0 END ALLOWED_OD_DAYS,SHORTNAME,COURSE_NAME,A.SCHEMENO,CCODE,SRNO,SUBID,ELECT,GLOBALELE,CREDITS,LECTURE,THEORY,PRACTICAL,MAXMARKS_I,MAXMARKS_E,MINMARKS,S1MAX,S2MAX,S3MAX,S4MAX,S5MAX,S6MAX,S7MAX,S8MAX,S9MAX,S10MAX,ASSIGNMAX,S1MIN,S2MIN,S3MIN,S4MIN,S5MIN,S6MIN,S7MIN,S8MIN,S9MIN,S10MIN,ASSIGNMIN,DRAWING,LEVELNO,GROUPNO,PREREQUISITE,PREREQUISITE_CREDIT,PREREQUISITE_LEVELNO,OFFERED,A.SEMESTERNO,A.COLLEGE_CODE,GRADE,MINGRADE,SPECIALISATIONNO,BOS_DEPTNO,PAPER_HRS,QTY,REQ_LEVEL,SCALEDN_MARK,CGROUPNO,CORE_SUBID,CATEGORYNO,INTAKE,ENAME,A.ORGANIZATIONID,A.DEPTNO,CAPACITY,ACTIVE,CREATEDBY,MODIFIED_BY,MODIFIED_DATE,DATE,COURSE_RELEVANCE,REMARK,IS_SPECIAL,MINMARK_I,TOTAL_MARK", "B.SCHEMENO = " + ddlScheme.SelectedValue + " AND ADMBATCH = " + ddlAdmbatch.SelectedValue, "");
            //ds = objCommon.FillDropDown("ACD_SCHEME A LEFT JOIN ACD_COURSE B ON A.SCHEMENO = B.SCHEMENO LEFT JOIN ACD_OD_TRACKER_STUDENT_OD_LIMIT_CONFIG C ON B.SCHEMENO = C.SCHEMENO AND ADM_BATCH_NO = ADMBATCH", "COURSENO", "CASE WHEN COURSENO IS NOT NULL THEN 1 ELSE 0  END IS_COURSE_DEFINE_FOR_SCHEME,ISNULL(IS_ACTIVE,0) IS_ACTIVE,CASE WHEN C.SCHEMENO IS NOT NULL THEN 1 ELSE 0 END IS_CONFIG_DONE,CASE WHEN C.SCHEMENO IS NOT NULL THEN ALLOWED_OD_DAYS ELSE 0 END ALLOWED_OD_DAYS,SHORTNAME,COURSE_NAME,A.SCHEMENO,CCODE,SRNO,SUBID,ELECT,GLOBALELE,CREDITS,LECTURE,THEORY,PRACTICAL,MAXMARKS_I,MAXMARKS_E,MINMARKS,S1MAX,S2MAX,S3MAX,S4MAX,S5MAX,S6MAX,S7MAX,S8MAX,S9MAX,S10MAX,ASSIGNMAX,S1MIN,S2MIN,S3MIN,S4MIN,S5MIN,S6MIN,S7MIN,S8MIN,S9MIN,S10MIN,ASSIGNMIN,DRAWING,LEVELNO,GROUPNO,PREREQUISITE,PREREQUISITE_CREDIT,PREREQUISITE_LEVELNO,OFFERED,A.SEMESTERNO,A.COLLEGE_CODE,GRADE,MINGRADE,SPECIALISATIONNO,BOS_DEPTNO,PAPER_HRS,QTY,REQ_LEVEL,SCALEDN_MARK,CGROUPNO,CORE_SUBID,CATEGORYNO,INTAKE,ENAME,A.ORGANIZATIONID,A.DEPTNO,CAPACITY,ACTIVE,CREATEDBY,MODIFIED_BY,MODIFIED_DATE,DATE,COURSE_RELEVANCE,REMARK,IS_SPECIAL,MINMARK_I,TOTAL_MARK", "ADMBATCH = " + ddlAdmbatch.SelectedValue + " AND A.SCHEMENO = " + ddlScheme.SelectedValue + "", "");
            //ds = objCommon.FillDropDown("ACD_SCHEME A LEFT JOIN ACD_COURSE B ON A.SCHEMENO = B.SCHEMENO LEFT JOIN ACD_OD_TRACKER_STUDENT_OD_LIMIT_CONFIG C ON B.SCHEMENO = C.SCHEMENO AND ADM_BATCH_NO = ADMBATCH INNER JOIN ACD_ADMBATCH ADM ON ADM.BATCHNO = A.ADMBATCH", "DISTINCT SCHEMENAME", "CASE WHEN COURSENO IS NOT NULL THEN 1 ELSE 0  END IS_COURSE_DEFINE_FOR_SCHEME,ISNULL(IS_ACTIVE,0) IS_ACTIVE,CASE WHEN C.SCHEMENO IS NOT NULL THEN 1 ELSE 0 END IS_CONFIG_DONE,CASE WHEN C.SCHEMENO IS NOT NULL THEN ALLOWED_OD_DAYS ELSE 0 END ALLOWED_OD_DAYS,A.SCHEMENO,BATCHNAME", "ADMBATCH = " + ddlAdmbatch.SelectedValue + " AND A.SCHEMENO = " + ddlScheme.SelectedValue + "", "");
            ds = ObjTrackerCon.GetStudentODLimitConfig(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    string ss = ds.Tables[0].Rows[0]["IS_COURSE_DEFINE_FOR_SCHEME"].ToString();
                    if (ss == "0")
                    {
                        objCommon.DisplayMessage(updConfig, "Course creation not done for selected scheme.", this.Page);
                        return;
                    }
                    if(ds.Tables[0].Rows[0]["IS_CONFIG_DONE"].ToString() =="1")
                    {
                        btnSubmit.Text = "Update";
                    }
                    
                    txtAllowedDays.Text = ds.Tables[0].Rows[0]["ALLOWED_OD_DAYS"].ToString();
                    //
                    if (ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == "Active" || ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString().ToLower() == "true")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetODConfig(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetODConfig(false);", true);
                    }
                    //
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                }
            }
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ObjODTracker.UANO = Convert.ToInt32(Session["userno"]);
        ObjODTracker.OrganizationID = Convert.ToInt32(Session["OrgId"]);
        int ActiveStatus = 0;

        if (hfdODConfig.Value == "true")
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        if (btnSubmit.Text.ToLower() == "update")
        {
            CustomStatus cs = (CustomStatus)ObjTrackerCon.UpdateStudentODLimitConfig(ObjODTracker, ActiveStatus, Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(txtAllowedDays.Text));
                 
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearControl();                 
                objCommon.DisplayMessage(updConfig, "Record Updated sucessfully.", this.Page);                 
                btnSubmit.Text = "Submit";
            }
        }
        else
        {
            CustomStatus cs = (CustomStatus)ObjTrackerCon.InsertStudentODLimitConfig(ObjODTracker,ActiveStatus,Convert.ToInt32(ddlAdmbatch.SelectedValue),Convert.ToInt32(ddlScheme.SelectedValue),Convert.ToInt32(txtAllowedDays.Text));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updConfig, "Record Added sucessfully.", this.Page);
                ClearControl();
                //BindEventConfigData(3);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updConfig, "Record Already Exist.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updConfig, "Record Already Exist", this.Page);
            }
        }
    }

    private void ClearControl()
    {
        txtAllowedDays.Text = "";
        ddlScheme.SelectedIndex = 0;
        ddlAdmbatch.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();

        if (ddlScheme.SelectedIndex > 0)
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "SHORTNAME,COURSE_NAME,SCHEMENO,CCODE,SRNO,SUBID,ELECT,GLOBALELE,CREDITS,LECTURE,THEORY,PRACTICAL,MAXMARKS_I,MAXMARKS_E,MINMARKS,S1MAX,S2MAX,S3MAX,S4MAX,S5MAX,S6MAX,S7MAX,S8MAX,S9MAX,S10MAX,ASSIGNMAX,S1MIN,S2MIN,S3MIN,S4MIN,S5MIN,S6MIN,S7MIN,S8MIN,S9MIN,S10MIN,ASSIGNMIN,DRAWING,LEVELNO,GROUPNO,PREREQUISITE,PREREQUISITE_CREDIT,PREREQUISITE_LEVELNO,OFFERED,SEMESTERNO,COLLEGE_CODE,GRADE,MINGRADE,SPECIALISATIONNO,BOS_DEPTNO,PAPER_HRS,QTY,REQ_LEVEL,SCALEDN_MARK,CGROUPNO,CORE_SUBID,CATEGORYNO,INTAKE,Ename,OrganizationId,DEPTNO,CAPACITY,ACTIVE,CREATEDBY,MODIFIED_BY,MODIFIED_DATE,DATE,COURSE_RELEVANCE,REMARK,IS_SPECIAL,MINMARK_I,TOTAL_MARK", "SCHEMENO = " + ddlScheme.SelectedValue, "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                }
            }
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        btnSubmit.Text = "Submit";
    }
}