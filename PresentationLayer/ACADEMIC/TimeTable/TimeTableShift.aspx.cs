//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TIME TABLE CREATION                                                 
// CREATION DATE : 19-SEPT-2019
// CREATED BY    : RAJU BITODE  
// DISCRIPTION   : PAGE USED TO SHIFT SELECTED DAY TIME TABLE TO ANOTHER DAY/DATE.(AS PER SELECTION)                           
// MODIFIED BY   : NEHA BARANWAL
// MODIFIED DESC : 27-Nov-19
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

public partial class ACADEMIC_TimeTable_TimeTableShift : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttE = new AcdAttendanceModel();
    static string IpAddress = string.Empty;

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
                IpAddress = Request.ServerVariables["REMOTE_ADDR"]; //Session["ipAddress"].ToString();
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 29/01/2022
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 29/01/2022
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TimeTableShift.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TimeTableShift.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            ddlSession.SelectedIndex = 0;
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            if (Session["usertype"].ToString() != "1")
            {
                //string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND DB.DEPTNO IN (" + Session["userdeptno"].ToString() + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

                //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString() +" AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "C.COLLEGE_ID");
                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
            }
            else
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
                //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
            }

            //objCommon.FillDropDownList(ddlTimeTableDays, "ACD_DAY_MASTER", "DAY_NO", "DAY_NAME", "DAY_NO > 0", "DAY_NO");
        }
        catch (Exception ex)
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
            //ddlSchoolInstitute.SelectedIndex = 0;     // commented by Nikhil L. on 29/01/2022
            ddlDepartment.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
            }
            else
            {

                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "DEPTNAME ASC");
            }
        }
        catch (Exception ex)
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
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            if (ddlDegree.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "B.BRANCHNO");
                ddlBranch.Focus();
            }
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
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            if (ddlBranch.SelectedIndex > 0)
            {
                this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"]));
            }

            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ViewState["degreeno"] + " AND BRANCHNO =" + Convert.ToInt32(ViewState["branchno"]), "SCHEMENO DESC");
            ddlScheme.Focus();
        }
        catch (Exception ex)
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
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            ddlSem.Focus();
        }
        catch
        {
            throw;
        }
    }

    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        try
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
            // txtTTDate.Text = "";
            txtShiftTTDate.Text = "";
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
            ddlSection.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;

            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
            }
            ddlSection.Focus();
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
            ddlSlotType.SelectedIndex = 0;
            ddlSlotType.Focus();
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0", "SLOTTYPENO");

        }
        catch
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
                ddlExistingDates.SelectedIndex = 0;
                ddlTimeTableDays.SelectedIndex = 0;
                txtShiftTTDate.Text = string.Empty;
                pnllv.Visible = false;
                lvTimeTableShift.DataSource = null;
                lvTimeTableShift.DataBind();
                btnSubmit.Enabled = false;

                LoadExisitingDates();
            }
            else
            {
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;

                ddlExistingDates.SelectedIndex = 0;

                ddlTimeTableDays.Items.Clear();
                ddlTimeTableDays.Items.Add(new ListItem("Please Select", "0"));
                ddlTimeTableDays.SelectedIndex = 0;

                ddlTimeTableDays.SelectedIndex = 0;

                txtShiftTTDate.Text = string.Empty;
                pnllv.Visible = false;
                lvTimeTableShift.DataSource = null;
                lvTimeTableShift.DataBind();
                btnSubmit.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    //to load existing dates
    public void LoadExisitingDates()
    {
        try
        {
            //if (ddlSession.SelectedIndex > 0 && ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0 && ddlSchoolInstitute.SelectedIndex > 0)
            if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0 && ddlSchoolInstitute.SelectedIndex > 0)
            {
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;
                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TT INNER JOIN  ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES ", "START_DATE,END_DATE,MONTH(START_DATE) as STARTDATEMONTH", "ISNULL(TT.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + "and SLOTTYPE=" + ddlSlotType.SelectedValue + " AND TT.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "MONTH(START_DATE) ");
                if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                {
                    ddlExistingDates.DataSource = dsGetExisitingDates.Tables[0];
                    ddlExistingDates.DataTextField = "EXISTINGDATES";
                    ddlExistingDates.DataBind();
                }
                else
                {
                    ddlExistingDates.DataSource = null;
                    ddlExistingDates.DataBind();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlExistingDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlExistingDates.SelectedIndex > 0)
            {
                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();

                ddlTimeTableDays.Items.Clear();
                ddlTimeTableDays.Items.Add(new ListItem("Please Select", "0"));
                ddlTimeTableDays.SelectedIndex = 0;
                DataSet dsTimeTableDays = objCommon.FillDropDown("ACD_DAY_MASTER D INNER JOIN ACD_TIME_TABLE_CONFIG T ON T.DAYNO=D.DAY_NO INNER JOIN ACD_COURSE_TEACHER C ON C.CT_NO=T.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT DAYNO", "DAY_NAME", "ISNULL(T.CANCEL,0)=0 AND T.TIME_TABLE_DATE BETWEEN CONVERT(DATE,'" + Convert.ToDateTime(startdate).ToString("dd-MM-yyyy") + "',103) AND CONVERT(DATE,'" + Convert.ToDateTime(enddate).ToString("dd-MM-yyyy") + "',103) and C.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + " AND  TTS.SLOTTYPE = " + ddlSlotType.SelectedValue + " AND T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "DAYNO");
                if (dsTimeTableDays.Tables[0].Rows.Count > 0)
                {
                    ddlTimeTableDays.DataSource = dsTimeTableDays.Tables[0];
                    ddlTimeTableDays.DataTextField = "DAY_NAME";
                    ddlTimeTableDays.DataValueField = "DAYNO";
                    ddlTimeTableDays.DataBind();
                }
                else
                {
                    ddlTimeTableDays.DataSource = null;
                    ddlTimeTableDays.DataBind();
                }

                ddlTimeTableDays.SelectedIndex = 0;
                txtShiftTTDate.Text = string.Empty;
                pnllv.Visible = false;
                lvTimeTableShift.DataSource = null;
                lvTimeTableShift.DataBind();
                btnSubmit.Enabled = false;
            }
            else
            {
                ddlExistingDates.SelectedIndex = 0;

                ddlTimeTableDays.Items.Clear();
                ddlTimeTableDays.Items.Add(new ListItem("Please Select", "0"));
                ddlTimeTableDays.SelectedIndex = 0;

                ddlTimeTableDays.SelectedIndex = 0;
                txtShiftTTDate.Text = string.Empty;
                pnllv.Visible = false;
                lvTimeTableShift.DataSource = null;
                lvTimeTableShift.DataBind();
                btnSubmit.Enabled = false;

            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTimeTableDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtShiftTTDate.Text = string.Empty;
        pnllv.Visible = false;
        lvTimeTableShift.DataSource = null;
        lvTimeTableShift.DataBind();
        btnSubmit.Enabled = false;
    }

    protected void txtShiftTTDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //to check date in between selected existitng dates or not
            if (ddlExistingDates.SelectedIndex > 0)
            {
                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();

                if (Convert.ToDateTime(txtShiftTTDate.Text) < Convert.ToDateTime(startdate))
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                }
                else if (Convert.ToDateTime(txtShiftTTDate.Text) > Convert.ToDateTime(enddate))
                {
                    objCommon.DisplayMessage(this.Page, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                }
            }

            int hDayStatus = 0; string hDayName = string.Empty;
            int shiftedDateDayNo = txtShiftTTDate.Text != string.Empty ? Convert.ToInt32(Convert.ToDateTime(txtShiftTTDate.Text.ToString()).DayOfWeek) : 0;
            if (ddlTimeTableDays.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlTimeTableDays.SelectedValue) == shiftedDateDayNo)
                {
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    btnSubmit.Enabled = false;
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                    objCommon.DisplayMessage(this.Page, "Time Table (Shift) Date/Day should be different than Time Table day!", this.Page);
                }
                else
                {
                    if (txtShiftTTDate.Text != string.Empty)
                    {
                        hDayStatus = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "Distinct 1", "CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) = CONVERT(DATE,'" + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "Distinct 1", "CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) = CONVERT(DATE,'" + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)"));
                        hDayName = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "TOP 1 ACADEMIC_HOLIDAY_NAME", "CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) = CONVERT(DATE,'" + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)");
                    }
                    if (hDayStatus == 1)
                    {
                        pnllv.Visible = false;
                        lvTimeTableShift.DataSource = null;
                        lvTimeTableShift.DataBind();
                        btnSubmit.Enabled = false;
                        txtShiftTTDate.Text = string.Empty;
                        txtShiftTTDate.Focus();
                        objCommon.DisplayMessage(this.Page, "Compalsary holiday (" + hDayName + ") exists on selected date, you cant shift time table on this date!", this.Page);
                    }
                    else
                    {
                    }
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                pnllv.Visible = false;
                lvTimeTableShift.DataSource = null;
                lvTimeTableShift.DataBind();
                btnSubmit.Enabled = false;
                txtShiftTTDate.Text = string.Empty;
                ddlTimeTableDays.SelectedIndex = 0;
                ddlTimeTableDays.Focus();
                objCommon.DisplayMessage(this.Page, "Please select time table day!", this.Page);
            }

        }
        catch
        {
            throw;
        }
    }

    //to show time table details
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0 && ddlTimeTableDays.SelectedIndex > 0 && ddlExistingDates.SelectedIndex > 0 && txtShiftTTDate.Text != "")
            {
                int hDayStatus = 0; string hDayName = string.Empty;
                int shiftedDateDayNo = Convert.ToInt32(Convert.ToDateTime(txtShiftTTDate.Text.ToString()).DayOfWeek);
                hDayStatus = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "Distinct 1", "CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) = CONVERT(DATE,'" + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "Distinct 1", "CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) = CONVERT(DATE,'" + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)"));
                hDayName = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "TOP 1 ACADEMIC_HOLIDAY_NAME", "CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) = CONVERT(DATE,'" + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)");

                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();

                if (Convert.ToDateTime(txtShiftTTDate.Text) < Convert.ToDateTime(startdate))
                {
                    objCommon.DisplayMessage(updTimeTable, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                    return;
                }
                else if (Convert.ToDateTime(txtShiftTTDate.Text) > Convert.ToDateTime(enddate))
                {
                    objCommon.DisplayMessage(updTimeTable, "Selected Date should be in between " + startdate + " - " + enddate + " Existing Dates", this.Page);
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                    return;
                }
                else if (Convert.ToInt32(ddlTimeTableDays.SelectedValue) == shiftedDateDayNo)
                {
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    btnSubmit.Enabled = false;
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                    objCommon.DisplayMessage(updTimeTable, "Time Table (Shift) Date/Day should be different than Time Table day!", this.Page);
                    return;
                }
                else if (hDayStatus == 1)
                {
                    pnllv.Visible = false;
                    lvTimeTableShift.DataSource = null;
                    lvTimeTableShift.DataBind();
                    btnSubmit.Enabled = false;
                    txtShiftTTDate.Text = string.Empty;
                    txtShiftTTDate.Focus();
                    objCommon.DisplayMessage(updTimeTable, "Compulsary holiday (" + hDayName + ") exists on selected date, you can't shift time table on this date!", this.Page);
                    return;
                }
                else
                {
                    BindListView();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Check the selection Session/Degree/Scheme/Semester/Section/Slot Type/Existing Date/Time Table Days/TimeTable Date", this.Page);
                lvTimeTableShift.DataSource = null;
                lvTimeTableShift.DataBind();
                pnllv.Visible = false;
                btnSubmit.Enabled = false;
                txtShiftTTDate.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }

    //bind listview on selected data
    private void BindListView()
    {
        DataSet ds = new DataSet();

        string myStr = ddlExistingDates.SelectedItem.ToString();
        string[] ssizes = myStr.Split(' ');
        string startdate = ssizes[0].ToString();
        string enddate = ssizes[2].ToString();
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        ds = objAttC.GetSelectedDayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]),
            Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue),
            Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSlotType.SelectedValue), Convert.ToInt32(ddlTimeTableDays.SelectedValue),
            Convert.ToDateTime(txtShiftTTDate.Text), Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), Convert.ToInt32(ViewState["college_id"]), OrgId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnllv.Visible = true;
            lvTimeTableShift.DataSource = ds;
            lvTimeTableShift.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvTimeTableShift);//Set label -
            btnSubmit.Enabled = true;

            foreach (ListViewDataItem dataitem in lvTimeTableShift.Items)
            {
                CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
                HiddenField hdnStatus = dataitem.FindControl("hdnStatus") as HiddenField;
                //CheckBox cbHead = dataitem.FindControl("cbHead") as CheckBox;
                chkAccept.Checked = true;
                if (hdnStatus.Value == "1")
                    chkAccept.BackColor = System.Drawing.Color.OrangeRed;
                //cbHead.Checked = true;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Time Table does not exist on selected date!", this.Page);
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clgID = ViewState["college_id"].ToString();
        //string clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
        //int clgID= Convert.ToInt32(ViewState["college_id"]);
        if (ddlSchoolInstitute.SelectedIndex > 0)
        {
            ddlDepartment.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            ddlSession.SelectedIndex = 0;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
        }
        else
        {
            ddlDepartment.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
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
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID=" + ViewState["college_id"], "D.DEGREENO");
            this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"]));

        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
        }
        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
    }

    #endregion "Selected Index Changed"

    #region Transaction
    //shift time table to particular date
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus cs = CustomStatus.Others;
        string result = string.Empty, batchnos = string.Empty, slotnos = string.Empty,
            ua_nos = string.Empty, coursenos = string.Empty, subids = string.Empty;
        int dayno = 0;
        int clgID = 0;
        try
        {
            objAttE.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objAttE.DEGREENO = Convert.ToInt32(ViewState["degreeno"]);

            objAttE.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
            objAttE.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
            objAttE.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            dayno = Convert.ToInt32(ddlTimeTableDays.SelectedValue);
            objAttE.ToDate = Convert.ToDateTime(txtShiftTTDate.Text);

            objAttE.UA_No = Convert.ToInt32(Session["userno"].ToString());
            objAttE.Ipaddress = Session["ipAddress"].ToString();
            //clgID = ddlSchoolInstitute.SelectedIndex > 0 ? Convert.ToInt32(ddlSchoolInstitute.SelectedValue) : 0;
            clgID = Convert.ToInt32(ViewState["college_id"]);

            //==============================================================================//
            foreach (ListViewDataItem lvitem in lvTimeTableShift.Items)
            {
                CheckBox ckh = lvitem.FindControl("chkAccept") as CheckBox;
                HiddenField hdnbatch = lvitem.FindControl("hdnbatchNo") as HiddenField;
                HiddenField hdnCourse = lvitem.FindControl("hdnCourseNo") as HiddenField;
                HiddenField hdnUaNo = lvitem.FindControl("hdnUaNo") as HiddenField;
                HiddenField hdnSlotNo = lvitem.FindControl("hdnSlotNo") as HiddenField;
                HiddenField hdnSubid = lvitem.FindControl("hdnSubid") as HiddenField;
                if (ckh.Checked == true)
                {
                    batchnos += hdnbatch.Value + ",";
                    coursenos += hdnCourse.Value + ",";
                    ua_nos += hdnUaNo.Value + ",";
                    slotnos += hdnSlotNo.Value + ",";
                    subids += hdnSubid.Value + ",";
                }
            }

            if (batchnos.Substring(batchnos.Length - 1) == ",") batchnos = batchnos.Substring(0, batchnos.Length - 1);
            if (coursenos.Substring(coursenos.Length - 1) == ",") coursenos = coursenos.Substring(0, coursenos.Length - 1);
            if (ua_nos.Substring(ua_nos.Length - 1) == ",") ua_nos = ua_nos.Substring(0, ua_nos.Length - 1);
            if (slotnos.Substring(slotnos.Length - 1) == ",") slotnos = slotnos.Substring(0, slotnos.Length - 1);
            if (subids.Substring(subids.Length - 1) == ",") subids = subids.Substring(0, subids.Length - 1);

            //==============================================================================//
            if (CheckDuplicateEntry() == true)
            {
                objCommon.DisplayMessage(updTimeTable, "Time Table already exists on selected shift date!", this.Page);
                return;
            }
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            //Insert/Update Shift TimeTable
            cs = (CustomStatus)objAttC.AddShiftTimeTable(objAttE, batchnos, coursenos, ua_nos, slotnos, subids, dayno, clgID, OrgId);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                btnSubmit.Text = "Submit";
                objCommon.DisplayMessage(updTimeTable, "Time Table Shifted Successfully!!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Time Table already exists on selected shift date!", this.Page);

            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this, "Transaction Failed", this.Page);
            }
            this.BindListView();
            this.Clear(1);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string attStatus;
            attStatus = objCommon.LookUp("ACD_TIME_TABLE_SHIFT", "DISTINCT 1", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND DEGREENO   =" + Convert.ToInt32(ViewState["degreeno"])
                + " AND SCHEMENO   =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue)
                + " AND SECTIONNO  =" + Convert.ToInt32(ddlSection.SelectedValue)
                + " AND ISNULL(CANCEL,0)=0 AND CAST(SHIFT_TT_DATE AS DATE) = CAST('"
                + Convert.ToDateTime(txtShiftTTDate.Text.Trim()).ToString("yyyy/MM/dd") + "' AS DATE) AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])
                );
            if (attStatus != null && attStatus != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return flag;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Clear(0);
    }
    private void Clear(int num)
    {
        ddlExistingDates.Items.Clear();
        ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
        ddlExistingDates.SelectedIndex = 0;
        ddlTimeTableDays.Items.Clear();
        ddlTimeTableDays.Items.Add(new ListItem("Please Select", "0"));
        ddlTimeTableDays.SelectedIndex = 0;
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
        ddlSchoolInstitute.SelectedIndex = -1;
        if (num == 1)
        {
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSlotType.SelectedIndex = 0;
            ddlTimeTableDays.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtShiftTTDate.Text = string.Empty;
            pnllv.Visible = false;
            lvTimeTableShift.DataSource = null;
            lvTimeTableShift.DataBind();
            btnSubmit.Enabled = false;
            ddlSchoolInstitute.SelectedIndex = 0;

        }
    }

    #endregion Transaction
}
