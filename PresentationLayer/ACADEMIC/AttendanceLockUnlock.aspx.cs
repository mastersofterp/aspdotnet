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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class AttendanceLockUnlock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    // AcdAttendanceModel objAttE = new AcdAttendanceModel();

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
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            //Populate the Drop Down Lists
            PopulateDropDownList();
            //BindListView();
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 29/01/2022
            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 29/01/2022
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            // objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            //objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_PNAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
            ddlSession.SelectedIndex = 0;


        }
        catch
        {
            throw;
        }
    }


    protected void BindListView()
    {
        try
        {
            //int collegeid = Convert.ToInt32(ddlClgScheme.SelectedValue);
            int collegeid = Convert.ToInt32(ddlClgScheme.SelectedValue);
            int sessionno = Convert.ToInt32(ViewState["Sessionno"]);
            int facultyno = Convert.ToInt32(ddlFaculty.SelectedValue);
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            //string startdate = txtStartDate.Text.ToString();
            //string enddate = txtEndDate.Text.ToString();
            string StartEndDate = hdnDate.Value;
            // ViewState["StartEndDate"] = hdnDate.Value;
            string[] dates = new string[] { };
            if ((StartEndDate) == "")
            {
                objCommon.DisplayMessage(this.updattendancelock, "Please select Start Date End Date !", this.Page);
                return;
            }
            else
            {
                StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
                //string[]
                dates = StartEndDate.Split('-');
            }
            string StartDate = dates[0];//Jul 15, 2021
            string EndDate = dates[1];
            //DateTime dateTime10 = Convert.ToDateTime(a);
            DateTime dtStartDate = DateTime.Parse(StartDate);
            string SDate = dtStartDate.ToString("yyyy/MM/dd");
            DateTime dtEndDate = DateTime.Parse(EndDate);
            string EDate = dtEndDate.ToString("yyyy/MM/dd");


            DataSet ds = objAttC.GetDataAttendanceLockUnlockDetails(collegeid, sessionno, facultyno, courseno, sectionno, semesterno, Convert.ToDateTime(SDate), Convert.ToDateTime(EDate), Convert.ToInt32(ddlattstatus.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {

                pnlattendancelockunlock.Visible = true;
                lvattendancelockunlock.DataSource = ds;
                lvattendancelockunlock.DataBind();
                // objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvattendancelockunlock);
                //foreach (ListViewDataItem lvItem in lvattendancelockunlock.Items)
                //{
                //    CheckBox chkBox = lvItem.FindControl("Chkfaculty") as CheckBox;
                //    HiddenField hdnUaNo = lvItem.FindControl("hdnUaNo") as HiddenField;
                //    HiddenField hdnCourse = lvItem.FindControl("hdnCourseNo") as HiddenField;
                //    HiddenField hdnsectionno = lvItem.FindControl("hdnsectionno") as HiddenField;
                //    HiddenField hdnsemesterno = lvItem.FindControl("hdnsemesterno") as HiddenField;
                //    HiddenField hddate = lvItem.FindControl("hddate") as HiddenField;
                //    HiddenField hdnslotno = lvItem.FindControl("hdnslotno") as HiddenField;
                //    HiddenField hdttno = lvItem.FindControl("hdttno") as HiddenField;

                //    //HiddenField hdnStatus = lvItem.FindControl("hdnStatus") as HiddenField;

                //    string Lockstatus = objCommon.LookUp("ACD_ATTENDANCE_LOCKUNLOCK", "LockStatus", "TTNO=" + hdttno.Value);


                //    if (Lockstatus == "1")
                //    {
                //        chkBox.Checked = true;
                //    }
                //    else
                //    {
                //        chkBox.Checked = false;
                //    }                  

                //}
                //pnlattendancelockunlock.Visible = true;
                //lvattendancelockunlock.Visible = true;
            }
            else
            {
                pnlattendancelockunlock.Visible = true;
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found ", this.Page);

            }
            ScriptManager.RegisterClientScriptBlock(updattendancelock, updattendancelock.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlClgScheme_SelectedIndexChanged2(object sender, EventArgs e)
    {
        if (ddlClgScheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgScheme.SelectedValue));
            //ViewState["degreeno"]
            ddlFaculty.SelectedIndex = -1;
            ddlCourse.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
            ddlSem.SelectedIndex = -1;
            txtRemark.Text = string.Empty;
            lvattendancelockunlock.DataSource = null;
            lvattendancelockunlock.DataBind();
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                ViewState["Sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]));
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                //ddlSession.Focus();
                objCommon.FillDropDownList(ddlFaculty, "ACD_COURSE_TEACHER  CT INNER JOIN ACD_TIME_TABLE_CONFIG TM ON (CT.CT_NO=TM.CTNO) INNER JOIN User_Acc  U ON(CT.UA_NO=U.UA_NO) INNER JOIN ACD_SESSION_MASTER SM ON (CT.SESSIONNO=SM.SESSIONNO)", "DISTINCT CT.UA_NO", "U.UA_NAME COLLATE SQL_Latin1_General_CP1_CI_AS+' - '+ UA_FULLNAME COLLATE SQL_Latin1_General_CP1_CI_AS UA_NAME", "CT.UA_NO>0 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "CT.UA_NO");
                ScriptManager.RegisterClientScriptBlock(updattendancelock, updattendancelock.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlClgScheme.Focus();
            lvattendancelockunlock.DataSource = null;
            lvattendancelockunlock.DataBind();
        }

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlFaculty, "ACD_COURSE_TEACHER  CT INNER JOIN ACD_TIME_TABLE_CONFIG TM ON (CT.CT_NO=TM.CTNO) INNER JOIN User_Acc  U ON(CT.UA_NO=U.UA_NO)", "DISTINCT CT.UA_NO", "U.UA_NAME COLLATE SQL_Latin1_General_CP1_CI_AS+' - '+ UA_FULLNAME COLLATE SQL_Latin1_General_CP1_CI_AS UA_NAME", "CT.UA_NO>0 AND CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "CT.UA_NO");
                //ddlFaculty.Focus();
                DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Session["college_nos"].ToString(), Session["userdeptno"].ToString());
                if (resultDataSet != null && resultDataSet.Tables[0].Rows.Count > 0)
                {
                    ddlClgScheme.Items.Clear();
                    ddlClgScheme.Items.Add(new ListItem("Please Select", "0"));
                    ddlClgScheme.DataSource = resultDataSet;
                    ddlClgScheme.DataTextField = "COL_SCHEME_NAME";
                    ddlClgScheme.DataValueField = "COSCHNO";
                    ddlClgScheme.DataBind();
                    ddlClgScheme.Focus();
                }
                else
                {
                    ddlClgScheme.Items.Clear();
                    ddlClgScheme.Items.Add(new ListItem("Please Select", "0"));
                }
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();
                ScriptManager.RegisterClientScriptBlock(updattendancelock, updattendancelock.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
            else
            {
                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.SelectedIndex = -1;
                ddlSection.SelectedIndex = -1;
                ddlSem.SelectedIndex = -1;
                txtRemark.Text = string.Empty;
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    private DataSet GetDropDowns(int sessionno, int ua_type, string collegeids, string deptnos)
    {
        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        DataSet ds = new DataSet();
        SP_Name = "PKG_ACD_ATTENDANCE_LOCK_UNLOCK_SCHEME_DROPDOWN";
        SP_Parameters = "@P_SESSIONID,@P_UATYPE,@P_DEPTNIOS,@P_COLLEGEIDS";
        Call_Values = "" + sessionno + "," + ua_type + "," + collegeids + "," + deptnos + "";
        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        return ds;
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFaculty.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE  C INNER JOIN ACD_COURSE_TEACHER CT ON (C.COURSENO=ct.COURSENO) INNER JOIN ACD_COLLEGE_SCHEME_MAPPING CS ON CS.SCHEMENO=C.SCHEMENO AND CS.COLLEGE_ID=CT.COLLEGE_ID", "DISTINCT c.COURSENO", "C.CCODE+' - '+ c.COURSE_NAME", "c.COURSENO>0 AND ct.UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + " AND ISNULL(CT.CANCEL,0)=0 AND COSCHNO=" + ddlClgScheme.SelectedValue, "c.COURSENO");
                ddlCourse.Focus();
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();
                ScriptManager.RegisterClientScriptBlock(updattendancelock, updattendancelock.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlSem.SelectedIndex = -1;
                ddlSection.SelectedIndex = -1;
                txtRemark.Text = string.Empty;
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();

            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_SECTION S INNER JOIN ACD_COURSE_TEACHER ct ON (s.SECTIONNO=ct.SECTIONNO)", "DISTINCT s.SECTIONNO", "s.SECTIONNAME", "S.SECTIONNO>0 AND ct.COURSENO=" + ddlCourse.SelectedValue, "s.SECTIONNAME");

                //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "SECTIONNO");
                ddlSection.Focus();
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();
                ScriptManager.RegisterClientScriptBlock(updattendancelock, updattendancelock.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
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
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER sm INNER JOIN ACD_COURSE_TEACHER ct ON (sm.SEMESTERNO=ct.SEMESTERNO)", "DISTINCT sm.SEMESTERNO", "Sm.SEMESTERNAME", "Sm.SEMESTERNO>0 AND ct.SECTIONNO=" + ddlSection.SelectedValue, "Sm.SEMESTERNAME");
                ddlSem.Focus();
                lvattendancelockunlock.DataSource = null;
                lvattendancelockunlock.DataBind();
                ScriptManager.RegisterClientScriptBlock(updattendancelock, updattendancelock.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            }
            else
            {
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch
        {
            throw;
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        //if (txtStartTime.Text == string.Empty || txtEndTime.Text == string.Empty)
        //{
        //    if (txtStartTime.Text == string.Empty)
        //    {
        //        objCommon.DisplayMessage(this.Page, "Plese Enter Start Time", this.Page);
        //    }
        //    else if (txtEndTime.Text == string.Empty)
        //    {
        //        objCommon.DisplayMessage(this.Page, "Plese Enter End Time", this.Page);
        //    }
        //}
        //else if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
        //{
        //    objCommon.DisplayMessage(this.Page, "Plese Enter Start Date less than End Date", this.Page);
        //}
        //else
        //{
        BindListView();
        //}

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    //Modified by nehal on 13/04/23
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int id = 0;
            if (txtStartTime.Text == string.Empty || txtEndTime.Text == string.Empty)
            {
                if (txtStartTime.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Plese Enter Start Time", this.Page);
                }
                else if (txtEndTime.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Plese Enter End Time", this.Page);
                }
            }
            else if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                objCommon.DisplayMessage(this.Page, "Plese Enter Start Date less than End Date", this.Page);
            }
            else
            {
                AcdAttendanceController objAttC = new AcdAttendanceController();

                int collegeid = Convert.ToInt32(ViewState["college_id"]);
                int degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int branchno = Convert.ToInt32(ViewState["branchno"]);
                int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                int sessionno = Convert.ToInt32(ViewState["Sessionno"]);

                string remark = txtRemark.Text;
                //string stdt = txtStartDate.Text.ToString();
                string stdt = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy/MM/dd");
                //string endt = txtEndDate.Text;
                string endt = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy/MM/dd");

                //DateTime startdt = DateTime.ParseExact(stdt, "yyyy/MM/dd", null);
                //DateTime enddt = DateTime.ParseExact(endt, "yyyy/MM/dd", null);

                string starttime = txtStartTime.Text;
                string endtime = txtEndTime.Text;

                int CREATED_BY = Convert.ToInt32(Session["userno"]);
                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                int organizationid = Convert.ToInt32(Session["OrgId"]);


                foreach (ListViewDataItem lvItem in lvattendancelockunlock.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("Chkfaculty") as CheckBox;
                    HiddenField hdnUaNo = lvItem.FindControl("hdnUaNo") as HiddenField;
                    HiddenField hdnCourse = lvItem.FindControl("hdnCourseNo") as HiddenField;
                    HiddenField hdnsectionno = lvItem.FindControl("hdnsectionno") as HiddenField;
                    HiddenField hdnsemesterno = lvItem.FindControl("hdnsemesterno") as HiddenField;
                    HiddenField hddate = lvItem.FindControl("hddate") as HiddenField;
                    HiddenField hdnslotno = lvItem.FindControl("hdnslotno") as HiddenField;
                    HiddenField hdttno = lvItem.FindControl("hdttno") as HiddenField;

                    int Lockstatus = 0, uano = 0, courseno = 0, sectionno = 0, semesterno = 0, slot = 0;


                    int ttno = Convert.ToInt32(chkBox.ToolTip.ToString());
                    if (chkBox.Checked == true)
                    {
                        ttno = Convert.ToInt32(hdttno.Value);
                        uano = Convert.ToInt32(hdnUaNo.Value);
                        courseno = Convert.ToInt32(hdnCourse.Value);
                        sectionno = Convert.ToInt32(hdnsectionno.Value);
                        semesterno = Convert.ToInt32(hdnsemesterno.Value);
                        slot = Convert.ToInt32(hdnslotno.Value);
                        string Date = hddate.Value;
                        Lockstatus = 1;
                        CustomStatus cs = (CustomStatus)objAttC.Add_Attendance_lockunlock(ttno, collegeid, degreeno, branchno, schemeno, sessionno, uano, courseno, sectionno, semesterno, Convert.ToDateTime(Date), remark, slot, CREATED_BY, ipAddress, organizationid, Lockstatus, stdt, endt, starttime, endtime);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            count++;
                        }
                    }
                }
                if (count < 1)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Atleast One..!!", this.Page);
                    BindListView();
                }
                else if (count > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Attendance Locked Sucessfully..", this.Page);
                    //clearControls();
                    BindListView();
                    // return;
                }

                //this.BindListView();
            }

        }
        catch
        {
            throw;
        }

    }


    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int id = 0;
            if (txtStartTime.Text == string.Empty || txtEndTime.Text == string.Empty)
            {
                if (txtStartTime.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Plese Enter Start Time", this.Page);
                }
                else if (txtEndTime.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.Page, "Plese Enter End Time", this.Page);
                }
            }
            else if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                objCommon.DisplayMessage(this.Page, "Plese Enter Start Date less than End Date", this.Page);
            }
            else
            {
                AcdAttendanceController objAttC = new AcdAttendanceController();

                int collegeid = Convert.ToInt32(ViewState["college_id"]);
                int degreeno = Convert.ToInt32(ViewState["degreeno"]);
                int branchno = Convert.ToInt32(ViewState["branchno"]);
                int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                int sessionno = Convert.ToInt32(ViewState["Sessionno"]);

                string remark = txtRemark.Text;
                string stdt = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy/MM/dd");
                string endt = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy/MM/dd");
                string starttime = txtStartTime.Text;
                string endtime = txtEndTime.Text;

                int CREATED_BY = Convert.ToInt32(Session["userno"]);
                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                int organizationid = Convert.ToInt32(Session["OrgId"]);

                foreach (ListViewDataItem lvItem in lvattendancelockunlock.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("Chkfaculty") as CheckBox;
                    HiddenField hdnUaNo = lvItem.FindControl("hdnUaNo") as HiddenField;
                    HiddenField hdnCourse = lvItem.FindControl("hdnCourseNo") as HiddenField;
                    HiddenField hdnsectionno = lvItem.FindControl("hdnsectionno") as HiddenField;
                    HiddenField hdnsemesterno = lvItem.FindControl("hdnsemesterno") as HiddenField;
                    HiddenField hddate = lvItem.FindControl("hddate") as HiddenField;
                    HiddenField hdnslotno = lvItem.FindControl("hdnslotno") as HiddenField;
                    HiddenField hdttno = lvItem.FindControl("hdttno") as HiddenField;

                    int Lockstatus = 0, uano = 0, courseno = 0, sectionno = 0, semesterno = 0, slot = 0;


                    int ttno = Convert.ToInt32(chkBox.ToolTip.ToString());
                    if (chkBox.Checked == true)
                    {
                        ttno = Convert.ToInt32(hdttno.Value);
                        uano = Convert.ToInt32(hdnUaNo.Value);
                        courseno = Convert.ToInt32(hdnCourse.Value);
                        sectionno = Convert.ToInt32(hdnsectionno.Value);
                        semesterno = Convert.ToInt32(hdnsemesterno.Value);
                        slot = Convert.ToInt32(hdnslotno.Value);
                        string Date = hddate.Value;
                        Lockstatus = 0;
                        CustomStatus cs = (CustomStatus)objAttC.Add_Attendance_lockunlock(ttno, collegeid, degreeno, branchno, schemeno, sessionno, uano, courseno, sectionno, semesterno, Convert.ToDateTime(Date), remark, slot, CREATED_BY, ipAddress, organizationid, Lockstatus, stdt, endt, starttime, endtime);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            count++;
                        }
                    }
                }
                if (count < 1)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Atleast One..!!", this.Page);
                    this.BindListView();
                }
                else if (count > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Attendance Unlocked Sucessfully..", this.Page);
                    //clearControls();
                    this.BindListView();
                    // return;
                }
                this.BindListView();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void clearControls()
    {
        ddlClgScheme.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlFaculty.Items.Clear();
        ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    }
    protected void ddlattstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }
}