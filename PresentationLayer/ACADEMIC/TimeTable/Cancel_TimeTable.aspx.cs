//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TIME TABLE CREATION                                                 
// CREATION DATE : 1-JUNE-2011
// CREATED BY    : GAURAV S SONI                               
// MODIFIED BY   : NEHA BARANWAL
// MODIFIED DESC : 26-NOV-2019
//=================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web;

public partial class ACADEMIC_TIMETABLE_Cancel_TimeTable : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttE = new AcdAttendanceModel();

    #region Declaration
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //static string sessionuid = string.Empty;
    //static string IpAddress = string.Empty;
    #endregion Declaration

    #region "Page Event"
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
                this.PopulateDropDownList();
                //sessionuid = Session["userno"].ToString();
                //IpAddress = Session["ipAddress"].ToString();
                Session["transferTbl"] = null;
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 30/01/2022
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 30/01/2022

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TimeTable.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TimeTable.aspx");
        }
    }

    //to load all dropdownlist
    private void PopulateDropDownList()
    {
        try
        {
            ddlSession.SelectedIndex = 0;
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            if (Session["usertype"].ToString() != "1")
            {
                //string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString() +" AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "C.COLLEGE_ID");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
            }
            else
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
                //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
            }

            objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND DB.DEPTNO IN (" + Session["userdeptno"].ToString() + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
        }
        catch
        {
            throw;
        }
    }

    #endregion "Page Event"

    #region "Selected Index Changed"

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN (" + Session["userdeptno"].ToString() + " )", "DEPTNAME ASC");
                }
                else
                {
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "DEPTNAME ASC");
                }
                ddlDepartment.Focus();
            }
            else
            {
                ddlDepartment.Items.Clear();
                ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
                ddlDegree.Items.Clear();
                ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSlotType.Items.Clear();
                ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            }
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            pnlTimeTable.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            // pnlSlots.Visible = false;
            ddlSlotType.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";

            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "B.BRANCHNO");
                ddlBranch.Focus();
            }
            pnlTimeTable.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            //pnlSlots.Visible = false;

            ddlSlotType.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";

            if (ddlBranch.SelectedIndex > 0)
            {
            }
            //if (ddlBranch.SelectedValue == "99")    //FIRST YEAR
            //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "REPLACE(SCHEMENAME,'CIVIL ENGINEERING','FIRST YEAR') SCHEMENAME", "DEGREENO = 1 AND BRANCHNO = 1 AND SCHEMENO IN (1,24)", "SCHEMENO DESC");
            //else
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND BRANCHNO =" + Convert.ToInt32(ViewState["branchno"]), "SCHEMENO DESC");

            ddlScheme.Focus();

            pnlTimeTable.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            //pnlSlots.Visible = false;
            ddlSem.Focus();
            ddlSlotType.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";

            pnlTimeTable.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                ddlSection.Focus();
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSlotType.Items.Clear();
                ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            }
            //to enable the save btn
            // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);
            pnlTimeTable.Visible = false;

            AttendanceConfigDate();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSection.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0", "SLOTTYPENO");
            }
            else
            {
                ddlSlotType.Items.Clear();
                ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            }
            ddlSlotType.Focus();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            //to enable the save btn
            //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);
            pnlTimeTable.Visible = false;
            AttendanceConfigDate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSlotType.SelectedIndex > 0)
            {
                txtStartDate.Focus();
            }
            //LoadExisitingDates();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            //to enable the save btn
            // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);
            pnlTimeTable.Visible = false;

            AttendanceConfigDate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clgID = ViewState["college_id"].ToString();
        string clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
        if (ddlSchoolInstitute.SelectedIndex > 0)
        {
            ddlDepartment.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO=SM.SESSIONNO)", "SM.SESSIONNO", "SESSION_PNAME", "FLOCK=1 AND SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SM.SESSIONNO DESC");            
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_TIME_TABLE_CONFIG T ON (CT.CT_NO=T.CTNO)", "DISTINCT SM.SESSIONNO", "SESSION_PNAME", "SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND ISNULL(CT.CANCEL,0)=0 AND ISNULL(T.CANCEL,0)=0 AND ISNULL(CT.ORGANIZATIONID,0)=" + Session["OrgId"], "SM.SESSIONNO DESC");
            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        int Deptno = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        if (ddlDepartment.SelectedIndex > 0)
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;

            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID="+ddlSchoolInstitute.SelectedValue, "D.DEGREENO");
            this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"]));
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));

        }
        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
    }

    #endregion "Selected Index Changed"

    //static string startdate, enddate;
    DateTime ATT_StartDate, ATT_EndDate;
    public void AttendanceConfigDate()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0)
            {
                ATT_StartDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
                ATT_EndDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
                divDateDetails.Visible = true;
                lblTitleDate.Text = "Selected Session Start Date : " + ATT_StartDate.ToShortDateString() + " End Date : " + ATT_EndDate.ToShortDateString();

            }
        }
        catch { lblTitleDate.Text = "Selected Session Start Date : - End Date : -"; }
    }


    #region General
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSlotType.SelectedIndex = 0;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        lvTimeTable.DataSource = null;
        lvTimeTable.DataBind();
        pnlTimeTable.Visible = false;
        btnCancel.Visible = false;
        divDateDetails.Visible = false;
        rdoCancelType.SelectedValue = "0";
        ddlSchoolInstitute.ClearSelection();
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));


    }

    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add("Please Select");
        ddlsemester.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsemester.DataSource = ds;
            ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlsemester.DataBind();
            ddlsemester.SelectedIndex = 0;
        }
    }

    DataSet Dsfac;
    //to show time table details for selected date
    public void ShowDetails()
    {
        try
        {

            if (rdoCancelType.SelectedValue.ToString() == "0") //for time table
            {
                Dsfac = objAttC.LoadTimeTableDetails(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(ddlSection.SelectedValue), Convert.ToInt32(ddlSlotType.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt32(ViewState["college_id"]));
            }
            else if (rdoCancelType.SelectedValue.ToString() == "1") //for Attendance
            {
                Dsfac = objAttC.LoadAttendanceDetails(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(ddlSection.SelectedValue), Convert.ToInt32(ddlSlotType.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));
            }
            else if (rdoCancelType.SelectedValue.ToString() == "2") //for Both(Time table + Attendnace)
            {
                Dsfac = objAttC.LoadBothAttAndTTDetails(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(ddlSection.SelectedValue), Convert.ToInt32(ddlSlotType.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt32(ViewState["college_id"]));
            }

            if (Dsfac.Tables[0].Rows.Count > 0)
            {
                lvTimeTable.DataSource = Dsfac;
                lvTimeTable.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvTimeTable);//Set label -
                //to disable checkbox if attendance already done
                if (rdoCancelType.SelectedValue.ToString() == "0") //for time table
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#Timetable').show();$('td:nth-child(15)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Timetable').show();$('td:nth-child(15)').show();});", true);

                    foreach (ListViewDataItem item in lvTimeTable.Items)
                    {
                        CheckBox chkTTNO = item.FindControl("chkTTNO") as CheckBox;
                        Label lblDate = item.FindControl("lblDate") as Label;
                        HiddenField hfTempAttStatus = item.FindControl("hfTempAttStatus") as HiddenField;
                        HiddenField hfIsAlternate = item.FindControl("hfIsAlternate") as HiddenField;

                        //HiddenField hfUA_NO = item.FindControl("hfUA_NO") as HiddenField;
                        //HiddenField hfCOURSENO = item.FindControl("hfCOURSENO") as HiddenField;
                        //HiddenField hfSLOTNO = item.FindControl("hfSLOTNO") as HiddenField;

                        //string count = objCommon.LookUp("ACD_ATTENDANCE", "count(1)", "UA_NO=" + hfUA_NO.Value + " AND  COURSENO=" + hfCOURSENO.Value + " AND SLOTNO=" + hfSLOTNO.Value + " AND ATT_DATE = CONVERT(DATE,'" + Convert.ToDateTime(lblDate.Text).ToString("dd-MM-yyyy") + "',103)  AND ISNULL(CANCEL,0)=0");
                        if (hfTempAttStatus.Value == "COMPLETED" || hfTempAttStatus.Value == "--")//if (count == "1")
                        {
                            chkTTNO.Enabled = false;
                            chkTTNO.ToolTip = "Already Attendance Done";
                            chkTTNO.BackColor = System.Drawing.Color.Red;
                        }
                        else if (hfIsAlternate.Value == "ALTERNATE")//if (count == "1")
                        {
                            chkTTNO.Enabled = false;
                            chkTTNO.ToolTip = "Alternate Faculty";
                            chkTTNO.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            chkTTNO.Enabled = true;
                            //chkTTNO.ToolTip = "Please Select";
                            //chkTTNO.BackColor = System.Drawing.Color.Gray;
                        }

                    }
                }
                else if (rdoCancelType.SelectedValue.ToString() == "1") //for Attendance
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#Timetable').hide();$('td:nth-child(15)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Timetable').hide();$('td:nth-child(15)').hide();});", true);

                }
                else if (rdoCancelType.SelectedValue.ToString() == "2") //for time table
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#Timetable').show();$('td:nth-child(15)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#Timetable').show();$('td:nth-child(15)').show();});", true);

                    foreach (ListViewDataItem item in lvTimeTable.Items)
                    {
                        CheckBox chkTTNO = item.FindControl("chkTTNO") as CheckBox;
                        Label lblDate = item.FindControl("lblDate") as Label;
                        HiddenField hfTempAttStatus = item.FindControl("hfTempAttStatus") as HiddenField;
                        HiddenField hfIsAlternate = item.FindControl("hfIsAlternate") as HiddenField;
                        //HiddenField hfUA_NO = item.FindControl("hfUA_NO") as HiddenField;
                        //HiddenField hfCOURSENO = item.FindControl("hfCOURSENO") as HiddenField;
                        //HiddenField hfSLOTNO = item.FindControl("hfSLOTNO") as HiddenField;

                        //string count = objCommon.LookUp("ACD_ATTENDANCE", "count(1)", "UA_NO=" + hfUA_NO.Value + " AND  COURSENO=" + hfCOURSENO.Value + " AND SLOTNO=" + hfSLOTNO.Value + " AND ATT_DATE = CONVERT(DATE,'" + Convert.ToDateTime(lblDate.Text).ToString("dd-MM-yyyy") + "',103)  AND ISNULL(CANCEL,0)=0");
                        //if (hfTempAttStatus.Value == "COMPLETED" || hfTempAttStatus.Value == "--")//if (count == "1")
                        //{
                        //    chkTTNO.Enabled = false;
                        //    chkTTNO.ToolTip = "Already Attendance Done";
                        //    chkTTNO.BackColor = System.Drawing.Color.Red;
                        //}
                        if (hfIsAlternate.Value == "ALTERNATE")//if (count == "1")
                        {
                            chkTTNO.Enabled = false;
                            chkTTNO.ToolTip = "Alternate Faculty";
                            chkTTNO.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            chkTTNO.Enabled = true;
                            //chkTTNO.ToolTip = "Please Select";
                            //chkTTNO.BackColor = System.Drawing.Color.Gray;
                        }

                    }

                }
                pnlTimeTable.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                lvTimeTable.DataSource = null;
                lvTimeTable.DataBind();
                pnlTimeTable.Visible = false;
                btnCancel.Visible = false;
                objCommon.DisplayMessage(this.updTimeTable, "Record Not Found!", this.Page);
                //txtDate.Text = "";
            }

        }
        catch
        {
            throw;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {


            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this, "Please Select Dates Properly", this);
                    txtStartDate.Text = string.Empty;
                    txtEndDate.Text = string.Empty;

                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
            }



            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));//Added By Dileep Kare on 22/02/2021
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));  //Added By Dileep Kare on 22/02/2021

            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                //to check end date is under session dates or not
                if (Convert.ToDateTime(txtStartDate.Text) < SDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                    txtStartDate.Text = string.Empty;
                    txtStartDate.Focus();
                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
                else if (Convert.ToDateTime(txtStartDate.Text) > EDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                    txtStartDate.Text = string.Empty;
                    txtStartDate.Focus();
                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                //to check end date is under session dates or not
                if (Convert.ToDateTime(txtEndDate.Text) < SDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();
                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
                else if (Convert.ToDateTime(txtEndDate.Text) > EDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();
                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
            }



            ShowDetails();
            //if (rdoCancelType.SelectedValue.ToString() == "0") //for time table
            //{

            //}
            //else if (rdoCancelType.SelectedValue.ToString() == "1") //for Attendance
            //{
            //}
            //else if (rdoCancelType.SelectedValue.ToString() == "2") //for Both(Time table + Attendnace)
            //{
            //}


            // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "DatatableChanges();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {DatatableChanges();});", true);

            //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "DatatableChanges();", true);


        }
        catch
        {
            throw;
        }

    }


    //to get selected records to cancel
    private string getttno()
    {
        try
        {
            string retHNO = string.Empty;
            foreach (ListViewDataItem item in lvTimeTable.Items)
            {
                HiddenField hfTTNO = item.FindControl("hfTTNO") as HiddenField;
                CheckBox chkTTNO = item.FindControl("chkTTNO") as CheckBox;

                if (chkTTNO.Checked == true && chkTTNO.Enabled == true)
                {
                    if (retHNO.Length == 0) retHNO = hfTTNO.Value.ToString();
                    else
                        retHNO += "," + hfTTNO.Value.ToString();
                }

            }
            if (retHNO.Equals(""))
            {
                return "0";
            }
            else
            {
                return retHNO;
            }
        }
        catch { return null; }
    }

    //to cancel time table for selcted date
    int ret;
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Session["userno"].ToString()) && !string.IsNullOrEmpty(Session["ipAddress"].ToString()))
            {
                string ttnos = "";
                ttnos = getttno();


                if (txtStartDate.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter Start Date", this);
                    txtStartDate.Text = string.Empty;
                    txtStartDate.Focus();
                    return;
                }

                if (txtEndDate.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter End Date", this);
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();
                    return;
                }

                if (txtStartDate.Text != "" && txtEndDate.Text != "")
                {
                    if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                    {
                        objCommon.DisplayMessage(this, "Please Select Dates Properly", this);
                        txtStartDate.Text = string.Empty;
                        txtEndDate.Text = string.Empty;

                        pnlTimeTable.Visible = false;
                        btnCancel.Visible = false;
                        return;
                    }
                }



                DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
                DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));

                if (!string.IsNullOrEmpty(txtStartDate.Text))
                {
                    //to check end date is under session dates or not
                    if (Convert.ToDateTime(txtStartDate.Text) < SDate)
                    {
                        objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                        txtStartDate.Text = string.Empty;
                        txtStartDate.Focus();
                        pnlTimeTable.Visible = false;
                        btnCancel.Visible = false;
                        return;
                    }
                    else if (Convert.ToDateTime(txtStartDate.Text) > EDate)
                    {
                        objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                        txtStartDate.Text = string.Empty;
                        txtStartDate.Focus();
                        pnlTimeTable.Visible = false;
                        btnCancel.Visible = false;
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    //to check end date is under session dates or not
                    if (Convert.ToDateTime(txtEndDate.Text) < SDate)
                    {
                        objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                        txtEndDate.Text = string.Empty;
                        txtEndDate.Focus();
                        pnlTimeTable.Visible = false;
                        btnCancel.Visible = false;
                        return;
                    }
                    else if (Convert.ToDateTime(txtEndDate.Text) > EDate)
                    {
                        objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                        txtEndDate.Text = string.Empty;
                        txtEndDate.Focus();
                        pnlTimeTable.Visible = false;
                        btnCancel.Visible = false;
                        return;
                    }
                }


                if (ttnos == "0")
                {
                    objCommon.DisplayMessage(updTimeTable, "Please Select At Least One Record To Cancel!!", this.Page);
                    return;
                }


                if (rdoCancelType.SelectedValue.ToString() == "0") //for time table
                {
                    foreach (ListViewDataItem item in lvTimeTable.Items)
                    {
                        HiddenField hfTTNO = item.FindControl("hfTTNO") as HiddenField;
                        HiddenField hfATT_NO = item.FindControl("hfATT_NO") as HiddenField;
                        //HiddenField hfCTNO = item.FindControl("hfCTNO") as HiddenField;
                        //HiddenField hfDAYNO = item.FindControl("hfDAYNO") as HiddenField;
                        //HiddenField hfSLOTNO = item.FindControl("hfSLOTNO") as HiddenField;
                        CheckBox chkTTNO = item.FindControl("chkTTNO") as CheckBox;
                        Label lblRemark = item.FindControl("lblRemark") as Label;

                        if (chkTTNO.Checked == true && chkTTNO.Enabled == true)
                        {
                            ret = objAttC.CancelTimeTable(Convert.ToInt32(hfTTNO.Value), Convert.ToInt32(Session["userno"].ToString()), Request.ServerVariables["REMOTE_ADDR"].ToString(), lblRemark.Text, 0, Convert.ToInt32(hfATT_NO.Value));
                        }
                    }

                    if (ret == 2)
                    {
                        objCommon.DisplayMessage(this, "Selected Time Table Cancelled Successfully!", this);
                        ShowDetails();
                        //ddlSlotType.SelectedIndex = 0;
                        //txtStartDate.Text = "";
                    }
                }
                else if (rdoCancelType.SelectedValue.ToString() == "1") //for Attendance
                {
                    foreach (ListViewDataItem item in lvTimeTable.Items)
                    {
                        HiddenField hfTTNO = item.FindControl("hfTTNO") as HiddenField;
                        CheckBox chkTTNO = item.FindControl("chkTTNO") as CheckBox;
                        Label lblRemark = item.FindControl("lblRemark") as Label;
                        HiddenField hfATT_NO = item.FindControl("hfATT_NO") as HiddenField;
                        if (chkTTNO.Checked == true)
                        {
                            ret = objAttC.CancelTimeTable(Convert.ToInt32(hfTTNO.Value), Convert.ToInt32(Session["userno"].ToString()), Session["ipAddress"].ToString(), lblRemark.Text, 1, Convert.ToInt32(hfATT_NO.Value));
                        }
                    }

                    if (ret == 2)
                    {
                        objCommon.DisplayMessage(this, "Selected Attendance Cancelled Successfully!", this);
                        ShowDetails();
                        //ddlSlotType.SelectedIndex = 0;
                        //txtStartDate.Text = "";
                    }

                }
                else if (rdoCancelType.SelectedValue.ToString() == "2") //for Both(Time table + Attendnace)
                {
                    foreach (ListViewDataItem item in lvTimeTable.Items)
                    {
                        HiddenField hfTTNO = item.FindControl("hfTTNO") as HiddenField;
                        CheckBox chkTTNO = item.FindControl("chkTTNO") as CheckBox;
                        Label lblRemark = item.FindControl("lblRemark") as Label;
                        HiddenField hfATT_NO = item.FindControl("hfATT_NO") as HiddenField;
                        if (chkTTNO.Checked == true)
                        {
                            ret = objAttC.CancelTimeTable(Convert.ToInt32(hfTTNO.Value), Convert.ToInt32(Session["userno"].ToString()), Session["ipAddress"].ToString(), lblRemark.Text, 2, Convert.ToInt32(hfATT_NO.Value));
                        }
                    }

                    if (ret == 2)
                    {
                        objCommon.DisplayMessage(this, "Selected Time Table And Attendance Cancelled Successfully!", this);
                        ShowDetails();
                        //ddlSlotType.SelectedIndex = 0;
                        // txtStartDate.Text = "";
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Something went wrong! Please refresh the browser!", this);
            }

        }
        catch
        {
            throw;
        }
    }

    #endregion General


    protected void rdoCancelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0 || ddlSchoolInstitute.SelectedIndex == 0 || ddlDepartment.SelectedIndex == 0 || ddlSem.SelectedIndex == 0 || ddlSection.SelectedIndex == 0 || ddlSlotType.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this, "Please Select Details", this);
            txtStartDate.Focus();
            pnlTimeTable.Visible = false;
            btnCancel.Visible = false;
        }
        else if (txtStartDate.Text == "")
        {
            objCommon.DisplayMessage(this, "Please Enter Start Date", this);
            txtStartDate.Focus();
            pnlTimeTable.Visible = false;
            btnCancel.Visible = false;
        }
        else if (txtEndDate.Text == "")
        {
            objCommon.DisplayMessage(this, "Please Enter End Date", this);
            txtEndDate.Focus();
            pnlTimeTable.Visible = false;
            btnCancel.Visible = false;
        }
        else
        {
            ShowDetails();
        }
    }


    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != "" && txtEndDate.Text != "" && txtEndDate.Text != "__/__/____")
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Time Table Start Date should be greater than Time Table End Date", this.Page);
                    txtStartDate.Text = string.Empty;
                    txtStartDate.Focus();

                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
            }
            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

            if (Convert.ToDateTime(txtStartDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                txtStartDate.Text = string.Empty;
                txtStartDate.Focus();

                pnlTimeTable.Visible = false;
                btnCancel.Visible = false;
            }
            else if (Convert.ToDateTime(txtStartDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                txtStartDate.Text = string.Empty;
                txtStartDate.Focus();

                pnlTimeTable.Visible = false;
                btnCancel.Visible = false;
            }
        }
        catch { throw; }
    }
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDate.Text != "" && txtEndDate.Text != "" && txtEndDate.Text != "__/__/____")
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Time Table Start Date should be greater than Time Table End Date", this.Page);
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();

                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                    return;
                }
            }


            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            if (txtEndDate.Text != "" && txtEndDate.Text != "__/__/____")
            {
                if (Convert.ToDateTime(txtEndDate.Text) < SDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();

                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                }
                else if (Convert.ToDateTime(txtEndDate.Text) > EDate)
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();

                    pnlTimeTable.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }
        catch { throw; }

    }
}
