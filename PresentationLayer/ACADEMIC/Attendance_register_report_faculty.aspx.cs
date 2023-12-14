using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Attendance_register_report_faculty : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();

    #region Page Evnets
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
                // this.CheckPageAuthorization();
                if (Session["usertype"].ToString() == "3")
                {

                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=Attendance_register_report_faculty.aspx");
                }
                FillDropDownList();
                //Set the Page Title
             //   this.Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Attendance_register_report_faculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Attendance_register_report_faculty.aspx");
        }
    }
    private void FillDropDownList()
    {
        try
        {
            //Fill Dropdown Session
            objCommon.FillDropDownList(ddlSession, " acd_Attendance A inner join ACD_SESSION_MASTER S ON(S.SESSIONNO=A.SESSIONNO)INNER JOIN ACD_SESSION SE ON(S.SESSIONID=SE.SESSIONID)", "distinct SE.SESSIONID", "SE.SESSION_NAME", "ISNULL(CANCEL,0)=0 AND A.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "SESSION_NAME");
            objCommon.FillDropDownList(ddlSessionG, " acd_Attendance A inner join ACD_SESSION_MASTER S ON(S.SESSIONNO=A.SESSIONNO)INNER JOIN ACD_SESSION SE ON(S.SESSIONID=SE.SESSIONID)", "distinct SE.SESSIONID", "SE.SESSION_NAME", "ISNULL(CANCEL,0)=0 AND A.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "SESSION_NAME");
        }
        catch
        {
            throw;
        }
    }
    #endregion
    #region Core Subject
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO) INNER JOIN ACD_SESSION_MASTER S ON(S.COLLEGE_ID=DB.COLLEGE_ID) INNER JOIN ACD_COURSE_TEACHER CT ON (SM.COLLEGE_ID = CT.COLLEGE_ID AND CT.SCHEMENO=SC.SCHEMENO AND CT.SESSIONNO=S.SESSIONNO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND SM.COLLEGE_ID > 0 and  s.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "and UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND ISNULL(CT.CANCEL,0)=0", "COL_SCHEME_NAME");
               // objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)INNER JOIN ACD_SESSION_MASTER S ON(S.COLLEGE_ID=DB.COLLEGE_ID)INNER JOIN ACD_COURSE_TEACHER CT ON (SM.COLLEGE_ID = CT.COLLEGE_ID)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND SM.COLLEGE_ID > 0 and  s.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "and UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND ISNULL(CT.CANCEL,0)=0", "COL_SCHEME_NAME");

            }
            else
            {
                ddlSem.SelectedIndex = 0;
                ddlSection.SelectedIndex = 0;
                ddlCourse.SelectedIndex = 0;
                ddlSubjectType.SelectedIndex = 0;
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSection.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            if (ddlInstitute.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlInstitute.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    //this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]));
                    int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                    objCommon.FillDropDownList(ddlSem, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SEMESTER SEM  ON SR.SEMESTERNO = SEM.SEMESTERNO", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "SR.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND ISNULL(SR.CANCEL,0)=0 AND SR.SESSIONNO=" + sessionno + " AND SEM.SEMESTERNO>0", "SEM.SEMESTERNO");
                    ddlSem.Focus();
                }
            }
            else
            {
                ddlSection.SelectedIndex = 0;
                ddlCourse.SelectedIndex = 0;
                ddlSubjectType.SelectedIndex = 0;
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }
    private void FillDatesDropDown(DropDownList ddlsemester, int sessionnoid, int degree)
    {
        try
        {
            int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + sessionnoid + "and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
            DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);

            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSem.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSem.DataSource = ds;
                ddlSem.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSem.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSem.DataBind();
                ddlSem.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubjectType.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            if (ddlSem.SelectedIndex > 0)
            {
                int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + ddlSession.SelectedValue + "and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                //bind Subject name in drop down list
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSE_NAME", "CT.SESSIONNO=" + sessionno + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND ISNULL(C.GLOBALELE,0)=0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.COURSENO");
                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + sessionno + " AND SR.SECTIONNO > 0 AND SR.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND SR.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND ISNULL(SR.CANCEL,0)=0 ", "SC.SECTIONNAME");

            }
            else
            {
                ddlSubjectType.SelectedIndex = 0;
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            if (ddlCourse.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO=C.COURSENO INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and OC.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND OC.SEMESTERNO = " + ddlSem.SelectedValue + "AND  OC.COURSENO=" + ddlCourse.SelectedValue, "C.SUBID");
            }
            else
            {
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }

    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
            }
        }
        catch
        {
            throw;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != string.Empty && txtTodate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
                {
                    objCommon.DisplayMessage(this, "To Date should be greater than From Date", this.Page);
                    return;
                }

                else
                {

                    GridView GVDayWiseAtt = new GridView();
                    AcdAttendanceController objAttController = new AcdAttendanceController();
                    string ccode = string.Empty;
                    DataSet ds = null;
                    int batch = 0;
                    int CourseType = 0;
                    string degree = string.Empty;
                    string branch = string.Empty;
                    degree = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));
                    branch = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO)BRANCH", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));
                    ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
                    int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + ddlSession.SelectedValue + "and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                    ds = objAttController.GetDayWiseData(Convert.ToInt32(sessionno), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), CourseType, batch, ccode.ToString());
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ds.Tables[0].Columns.RemoveAt(7);

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
            }
            
        }
        catch
        {
            throw;
        }
    }
    #endregion

#region GlobalElective


    protected void ddlSessionG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtFdate.Text = string.Empty;
            txtTdate.Text = string.Empty;
            if (ddlSessionG.SelectedIndex > 0)
            {
               
                objCommon.FillDropDownList(ddlGlobalCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) inner join ACD_Session_MASTER S ON(S.SESSIONNO=CT.SESSIONNO)", "DISTINCT CT.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSE_NAME", "S.SESSIONID=" + Convert.ToInt32(ddlSessionG.SelectedValue) + " AND ISNULL(CT.CANCEL,0)=0 AND ISNULL(C.GLOBALELE,0)=1 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.COURSENO");

            }
            else
            {
                txtFdate.Text = string.Empty;
                txtTdate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlGlobalCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtFdate.Text=string.Empty;
            txtTdate.Text = string.Empty;

            if (ddlGlobalCourse.SelectedIndex > 0)
            {


            }
            else
            {
                txtFdate.Text = string.Empty;
                txtTdate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        if (txtFdate.Text != string.Empty && txtTdate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtTdate.Text) <= Convert.ToDateTime(txtFdate.Text))
            {
                objCommon.DisplayMessage(this, "To Date should be greater than From Date", this.Page);
                return;
            }

            else
            {
                GridView GVDayWiseAtt = new GridView();
                AcdAttendanceController objAttController = new AcdAttendanceController();
                string ccode = string.Empty;
                DataSet ds = null;
                int batch = 0;
                int CourseType = 0;
                string degree = string.Empty;
                string branch = string.Empty;
                int SchemeNo = 0;
                int Section = 0;
                int Subid = 0;
                degree = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));
                branch = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO)BRANCH", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));
                ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlGlobalCourse.SelectedValue);
                ds = objAttController.GetDayWiseDataGlobalElective(Convert.ToInt32(ddlSessionG.SelectedValue), SchemeNo, Convert.ToInt32(ddlGlobalCourse.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Subid, Section, Convert.ToDateTime(txtFdate.Text), Convert.ToDateTime(txtTdate.Text), CourseType, batch, ccode.ToString());
                if (ds.Tables[0].Rows.Count > 1)
                {
                    ds.Tables[0].Columns.RemoveAt(7);

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
        }

    }
    protected void BtnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

#endregion

}