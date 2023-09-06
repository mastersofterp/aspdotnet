using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ValurAdded : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    string PageId;
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
        pnl.Visible = false;
        if (!Page.IsPostBack)
            {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
                {
                Response.Redirect("~/default.aspx");
                }
            else
                {
                //CheckPageAuthorization();
                if (Session["usertype"].Equals(1))
                    {
                    }
                else
                    {
                    Response.Redirect("~/notauthorized.aspx?page=AffiliatedFeesCategory.aspx");
                    }

                }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            PopulateDropDown();
            //BindValueAddedEntries();
            Session["ValueCourseTbl"] = null;
            ViewState["action"] = "add";

            }

        }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=DefineIntake.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=DefineIntake.aspx");
    //    }
    //}

    protected void PopulateDropDown()
        {
        try
            {

            objCommon.FillDropDownList(ddlCollegeScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

            objCommon.FillDropDownList(ddlgroupname, "ACD_GROUP_MASTER_SPECIALIZATION", "GROUPID", "GROUP_NAME", "ACTIVE_STATUS=1", "GROUPID");

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DefineIntake.PopulateDropDown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    protected void ddlCollegeScheme_SelectedIndexChanged(object sender, EventArgs e)
        {

        //ddlsemester.DataSource = null;

        //ddlsemester.DataValueField = string.Empty;
        //ddlsemester.DataTextField = string.Empty;
        //ddlsemester.DataBind();
        //ddlsemester.SelectedIndex = 0;
        //ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        //ddlassessment.SelectedIndex = 0;

        if (ddlCollegeScheme.SelectedIndex > 0)
            {

            // ddlgroup.SelectedIndex = 0;
            ddlsemester.Items.Clear();
            ddlsemester.Items.Add(new ListItem("Please Select", "0"));
            int Schemeno = Convert.ToInt32(objCommon.LookUp("acd_college_scheme_mapping", "ISNULL(SCHEMENO,0) as SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlCollegeScheme.SelectedValue)));

            ViewState["SCHEMENO"] = Schemeno;
            DataSet dssemester = objSC.GetSchemwiseSemester(Convert.ToInt32(Schemeno), 1);

            if (dssemester.Tables[0].Rows.Count > 0)
                {
                ddlsemester.DataSource = dssemester;
                //ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlsemester.DataValueField = dssemester.Tables[0].Columns[0].ToString();
                ddlsemester.DataTextField = dssemester.Tables[0].Columns[1].ToString();

                ddlsemester.DataBind();
                ddlsemester.SelectedIndex = 0;

                }
            }
        else
            {
            ddlsemester.Items.Clear();
            //ddlsemester.DataSource = null;
            //ddlsemester.DataValueField = "0";
            //ddlsemester.DataTextField = "Please Select";
            //ddlsemester.DataBind();
            //ddlsemester.SelectedIndex = 0;
            ddlsemester.Items.Add(new ListItem("Please Select", "0"));
            ddlassessment.SelectedIndex = 0;
            }

        }

    private DataTable CreateTable_ValueCourse()
        {
        DataTable dtBValuecourse = new DataTable();
        dtBValuecourse.Columns.Add(new DataColumn("COSCHNO", typeof(int)));
        dtBValuecourse.Columns.Add(new DataColumn("COLLEGE_ID", typeof(int)));
        dtBValuecourse.Columns.Add(new DataColumn("COL_SCHEME_NAME", typeof(string)));
        dtBValuecourse.Columns.Add(new DataColumn("SEMESTERNO", typeof(string)));
        dtBValuecourse.Columns.Add(new DataColumn("SEMESTERNAME", typeof(string)));
        dtBValuecourse.Columns.Add(new DataColumn("GROUPNAME", typeof(string)));
        dtBValuecourse.Columns.Add(new DataColumn("GROUPID", typeof(string)));
        dtBValuecourse.Columns.Add(new DataColumn("COURSENO", typeof(int)));
        dtBValuecourse.Columns.Add(new DataColumn("COURSE_NAME", typeof(string)));

        dtBValuecourse.Columns.Add(new DataColumn("DURATIONNO", typeof(int)));
        dtBValuecourse.Columns.Add(new DataColumn("DURATION", typeof(string)));


        dtBValuecourse.Columns.Add(new DataColumn("STARTDATE", typeof(string)));

        dtBValuecourse.Columns.Add(new DataColumn("ENDDATE", typeof(string)));


        dtBValuecourse.Columns.Add(new DataColumn("ASSESSMENTNO", typeof(int)));
        dtBValuecourse.Columns.Add(new DataColumn("ASSESSMENT", typeof(string)));

        dtBValuecourse.Columns.Add(new DataColumn("GRADENO", typeof(int)));
        dtBValuecourse.Columns.Add(new DataColumn("GRADE", typeof(string)));




        dtBValuecourse.Columns.Add(new DataColumn("VALUE_NO", typeof(int)));

        dtBValuecourse.Columns.Add(new DataColumn("SCHEMENO", typeof(int)));
        return dtBValuecourse;

        }
    protected void btnadd_Click(object sender, EventArgs e)
        {
        try
            {


            DataTable dt;
            if (Session["ValueCourseTbl"] != null && ((DataTable)Session["ValueCourseTbl"]) != null)
                {

                dt = (DataTable)Session["ValueCourseTbl"];
                DataTable dt1 = (DataTable)Session["ValueCourseTbl"];
                foreach (DataRow drow in dt1.Rows)
                    {
                    if (drow["COSCHNO"].ToString().Equals(ddlCollegeScheme.SelectedValue) && drow["SEMESTERNAME"].Equals(ddlsemester.SelectedValue) && drow["GROUPNAME"].Equals(ddlgroupname.SelectedValue) && drow["COURSENO"].ToString().Equals(ddlcourse.SelectedValue.ToString()))
                        {

                        objCommon.DisplayMessage(this.Page, "Record Already Exists.", this.Page);
                        ddlCollegeScheme.Focus();
                        return;
                        }

                    }
                }

            else
                {
                dt = this.CreateTable_ValueCourse();
                }
            DataRow dr = dt.NewRow();
            dr["COLLEGE_ID"] = ViewState["college_id"];
            dr["COSCHNO"] = Convert.ToInt32(ddlCollegeScheme.SelectedValue);
            dr["COL_SCHEME_NAME"] = ddlCollegeScheme.SelectedItem;
            dr["SEMESTERNO"] = Convert.ToInt32(ddlsemester.SelectedValue);
            dr["SEMESTERNAME"] = ddlsemester.SelectedItem.Text;
            dr["GROUPNAME"] = ddlgroupname.SelectedItem.Text;
            dr["GROUPID"] = ddlgroupname.SelectedValue;
            dr["COURSENO"] = Convert.ToInt32(ddlcourse.SelectedValue);
            dr["COURSE_NAME"] = ddlcourse.SelectedItem;
            dr["SCHEMENO"] = Convert.ToInt32(ViewState["SCHEMENO"]);
            if (ddlDuration.SelectedValue == "0")
                {
                dr["DURATIONNO"] = 0;
                dr["DURATION"] = "";
                }
            else
                {
                dr["DURATIONNO"] = ddlDuration.SelectedValue;
                dr["DURATION"] = ddlDuration.SelectedItem.Text.ToString();
                }
            dr["STARTDATE"] = txtStartDate.Text;
            dr["ENDDATE"] = txtEndDate.Text;
            if (ddlassessment.SelectedValue == "0")
                {

                dr["ASSESSMENTNO"] = 0;
                dr["ASSESSMENT"] = "";
                }
            else
                {
                dr["ASSESSMENTNO"] = ddlassessment.SelectedValue;
                dr["ASSESSMENT"] = ddlassessment.SelectedItem.Text.ToString();
                }
            if (ddlgrade.SelectedValue == "0")
                {
                dr["GRADENO"] = 0;
                dr["GRADE"] = "";
                }
            else
                {
                dr["GRADENO"] = ddlgrade.SelectedValue;
                dr["GRADE"] = ddlgrade.SelectedItem;
                }

            dt.Rows.Add(dr);
            Session["ValueCourseTbl"] = dt;
            lvvaluecourse.DataSource = dt;
            lvvaluecourse.DataBind();
            pnlvaluecourse.Visible = true;
            lvvaluecourse.Visible = true;
            ClearOnAdd();


            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DefineIntake.btnAdd_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }

        }


    protected void ClearOnAdd()
        {
        ddlCollegeScheme.SelectedIndex = 0;
        ddlgroupname.SelectedIndex = 0;
        ddlcourse.SelectedIndex = 0;
        ddlDuration.SelectedIndex = 0;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        // ddlassessment.SelectedIndex = 0;
        ddlassessment.SelectedIndex = 0;
        ddlgrade.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        }

    protected void BindAllData()
        {
        DataSet ds = objSC.GetAllAddedEnteries();
        // DataSet ds = objSC.GetAllAddedEnteriesbycoshno(Convert.ToInt32(ddlCollegeScheme.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
            {
            lvvalueaddedcourse.DataSource = ds;
            lvvalueaddedcourse.DataBind();
            pnlvalue.Visible = true;
            lvvalueaddedcourse.Visible = true;
            }
        else
            {
            lvvalueaddedcourse.DataSource = null;
            lvvalueaddedcourse.DataBind();
            lvvalueaddedcourse.Visible = false;
            objCommon.DisplayMessage(this.Page, "Data Not Found.", this.Page);
            }

        ViewState["action"] = "add";

        }


    protected void BindCollegeSchemeData()
        {
        //DataSet ds = objSC.GetAllAddedEnteries();

        DataSet ds = objSC.GetAllAddedEnteriesbycoshno(Convert.ToInt32(ddlCollegeScheme.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
            {
            lvvalueaddedcourse.DataSource = ds;
            lvvalueaddedcourse.DataBind();
            pnlvalue.Visible = true;
            lvvalueaddedcourse.Visible = true;
            }
        else
            {
            lvvalueaddedcourse.DataSource = null;
            lvvalueaddedcourse.DataBind();
            lvvalueaddedcourse.Visible = false;
            // objCommon.DisplayMessage(this.Page, "Data Not Found.", this.Page);
            }

        ViewState["action"] = "add";

        }
    public void clear()
        {

        ddlCollegeScheme.SelectedIndex = 0;
        //txtGroupName.Text = string.Empty;     
        ddlgroupname.SelectedIndex = 0;
        //ddlDuration.SelectedItem.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ddlcourse.SelectedIndex = 0;
        //ddlsemester.SelectedIndex = 0;
        ddlgrade.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        ddlassessment.SelectedIndex = 0;
        //ddlassessment.SelectedItem.Text = string.Empty;
        //ddlgrade.SelectedItem.Text = string.Empty;


        //ddlassessment.SelectedIndex = 0;
        //ddlgrade.SelectedIndex = 0;
        }


    protected void btnSubmit_Click(object sender, EventArgs e)
        {
        string collegeid = Convert.ToString(ViewState["college_id"]);
        string coschname = string.Empty;
        int Groupid = 0;
        int semesterno = 0;
        string courseno = string.Empty;
        string duration = string.Empty;
        string startdate = string.Empty;
        string enddate = string.Empty;
        string assessment = string.Empty;
        string grade = string.Empty;
        int durationno = 0;
        int assessmentno = 0;
        int gradeno = 0;
        int Orgid = 0;
        int COSHNO = 0;
        int COURSENO = 0;
        int VALUENO = 0;
        int Schemeno = 0;

        DataTable dt;

        if (ViewState["action"].Equals("edit"))
            {

            if (Session["ValueCourseTbl"] == null)
                {
                objCommon.DisplayMessage(this.Page, "Please Add Atleast One Entry", this.Page);
                return;
                }

            if (Session["ValueCourseTbl"] != null && ((DataTable)Session["ValueCourseTbl"]) != null)
                {
                dt = (DataTable)Session["ValueCourseTbl"];

                DataTable dt1 = (DataTable)Session["ValueCourseTbl"];
                foreach (DataRow drow in dt1.Rows)
                    {
                    if (drow["COSCHNO"].ToString().Equals(ddlCollegeScheme.SelectedValue) && drow["SEMESTERNAME"].Equals(ddlsemester.SelectedValue) && drow["GROUPNAME"].Equals(ddlgroupname.SelectedValue) && drow["COURSENO"].ToString().Equals(ddlcourse.SelectedValue.ToString()))
                        {

                        objCommon.DisplayMessage(this.Page, "Record Already Exists.", this.Page);
                        ddlCollegeScheme.Focus();
                        return;
                        }

                    }
                foreach (DataRow dr in dt.Rows)
                    {
                    collegeid = dr["COLLEGE_ID"].ToString();
                    coschname = dr["COSCHNO"].ToString();
                    semesterno = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    Groupid = Convert.ToInt32(dr["GROUPID"].ToString());
                    courseno = dr["COURSENO"].ToString();
                    duration = dr["DURATION"].ToString();
                    startdate = dr["STARTDATE"].ToString();
                    enddate = dr["ENDDATE"].ToString();
                    assessment = dr["ASSESSMENT"].ToString();
                    grade = dr["GRADE"].ToString();
                    Schemeno = Convert.ToInt32(dr["SCHEMENO"].ToString());
                    }
                }



            //coschname = ddlCollegeScheme.SelectedValue;
            //Groupid = Convert.ToInt32(ddlgroupname.SelectedValue);
            //courseno = ddlcourse.SelectedValue;
            //duration = ddlDuration.SelectedItem.Text;
            //startdate = txtStartDate.Text;
            //enddate = txtEndDate.Text;
            //assessment = ddlassessment.SelectedItem.Text;
            //grade = ddlgrade.SelectedItem.Text;


            int CREATED_BY = Convert.ToInt32(Session["userno"]);

            Orgid = Convert.ToInt32(Session["OrgId"]);
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];



            VALUENO = Convert.ToInt32(ViewState["VALUENO"]);
            CustomStatus cs = (CustomStatus)objSC.Update_ValueAdded_Entry(VALUENO, coschname, Groupid, courseno, durationno, duration, startdate, enddate, assessmentno, assessment, gradeno, grade, CREATED_BY, ipAddress, Orgid, semesterno, Convert.ToInt32(ddlCollegeScheme.SelectedValue));
            if (cs.Equals(CustomStatus.RecordUpdated))
                {

                objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);

                // clear();

                //BindValueAddedEntries();
                BindAllData();
                //ddlCollegeScheme.Enabled = false;
                lvvaluecourse.DataSource = null;
                lvvaluecourse.DataBind();

                lvvaluecourse.Visible = false;

                pnlvaluecourse.Visible = false;
                Session["ValueCourseTbl"] = null;
                ddlCollegeScheme.Enabled = true;
                btnAdd.Visible = true;
                btnSubmit.Text = "Submit";
                clear();

                //ddlsemester.SelectedIndex = 0;
                //ddlCollegeScheme.SelectedIndex = 0;
                // ddlsemester.Items.Clear();
                }

            }


        else
            {

            //DataTable dt;

            if (Session["ValueCourseTbl"] == null)
                {
                objCommon.DisplayMessage(this.Page, "Please Add Atleast One Entry", this.Page);
                return;
                }


            if (Session["ValueCourseTbl"] != null && ((DataTable)Session["ValueCourseTbl"]) != null)
                {
                dt = (DataTable)Session["ValueCourseTbl"];

                DataTable dt1 = (DataTable)Session["ValueCourseTbl"];
                foreach (DataRow drow in dt1.Rows)
                    {
                    if (drow["COSCHNO"].ToString().Equals(ddlCollegeScheme.SelectedValue) && drow["SEMESTERNAME"].Equals(ddlsemester.SelectedValue) && drow["GROUPNAME"].Equals(ddlgroupname.SelectedValue) && drow["COURSENO"].ToString().Equals(ddlcourse.SelectedValue.ToString()))
                        {

                        objCommon.DisplayMessage(this.Page, "Record Already Exists.", this.Page);
                        ddlCollegeScheme.Focus();
                        return;
                        }

                    }
                foreach (DataRow dr in dt.Rows)
                    {
                    collegeid = dr["COLLEGE_ID"].ToString();
                    coschname = dr["COSCHNO"].ToString();
                    semesterno = Convert.ToInt32(dr["SEMESTERNO"].ToString());
                    Groupid = Convert.ToInt32(dr["GROUPID"].ToString());
                    courseno = dr["COURSENO"].ToString();
                    duration = dr["DURATION"].ToString();
                    startdate = dr["STARTDATE"].ToString();
                    enddate = dr["ENDDATE"].ToString();
                    assessment = dr["ASSESSMENT"].ToString();
                    grade = dr["GRADE"].ToString();
                    Schemeno = Convert.ToInt32(dr["SCHEMENO"].ToString());

                    int CREATED_BY = 1;

                    Orgid = Convert.ToInt32(Session["OrgId"]);
                    string ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                    CustomStatus cs = (CustomStatus)objSC.Add_ValueAdded_Entry(collegeid, coschname, Groupid, courseno, durationno, duration, startdate, enddate, assessmentno, assessment, gradeno, grade, CREATED_BY, ipAddress, Orgid, semesterno, Schemeno);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        {

                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                        //ViewState["action"] = null;

                        //BindValueAddedEntries();
                        BindAllData();

                        lvvaluecourse.DataSource = null;
                        lvvaluecourse.DataBind();

                        lvvaluecourse.Visible = false;

                        pnlvaluecourse.Visible = false;
                        clear();
                        Session["ValueCourseTbl"] = null;

                        ddlsemester.SelectedIndex = 0;
                        ddlCollegeScheme.SelectedIndex = 0;
                        // ddlsemester.Items.Clear();
                        //btnAdd.Visible = true;
                        // btnAdd.Text = "Add";
                        }
                    else
                        {
                        objCommon.DisplayMessage(this.Page, "Record Already Exists.", this.Page);
                        clear();
                        BindAllData();

                        lvvaluecourse.DataSource = null;
                        lvvaluecourse.DataBind();

                        lvvaluecourse.Visible = false;

                        pnlvaluecourse.Visible = false;
                        Session["ValueCourseTbl"] = null;
                        //btnAdd.Visible = true;
                        //btnAdd.Text = "Add";
                        }
                    }

                }
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        ViewState["action"] = null;
        Response.Redirect(Request.Url.ToString());
        }
    private void GetDetailsByValueNo(int valueno, int Groupid)
        {
        try
            {

            DataSet ds = objSC.GetDetailsByValueCourseId(valueno);
            if (ViewState["action"].ToString() == "edit")
                {
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    objCommon.FillDropDownList(ddlCollegeScheme, "acd_college_scheme_mapping", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO > 0", "COSCHNO DESC");
                    ddlCollegeScheme.SelectedValue = ds.Tables[0].Rows[0]["COSCHNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["COSCHNO"].ToString();
                    // txtGroupName.Text
                    ddlgroupname.SelectedValue = ds.Tables[0].Rows[0]["GROUPID"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["GROUPID"].ToString();
                    //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE  C INNER JOIN acd_college_scheme_mapping  AC ON (C.SCHEMENO = AC.SCHEMENO) ", "DISTINCT C.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSENAME", "C.COURSENO>0 AND AC.COSCHNO=" + ddlCollegeScheme.SelectedValue, "C.COURSENO");

                    //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE  C INNER JOIN acd_college_scheme_mapping  AC ON (C.SCHEMENO = AC.SCHEMENO) ", "DISTINCT (C.COURSENO)", "C.COURSE_NAME", "C.COURSENO>0 AND AC.COSCHNO=" + ddlCollegeScheme.SelectedValue, "C.COURSE_NAME");

                    int Schemeno = Convert.ToInt32(objCommon.LookUp("acd_college_scheme_mapping", "ISNULL(SCHEMENO,0) as SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlCollegeScheme.SelectedValue)));

                    ViewState["SCHEMENO"] = Schemeno;
                    DataSet dssemester = objSC.GetSchemwiseSemester(Convert.ToInt32(Schemeno), 1);

                    if (dssemester.Tables[0].Rows.Count > 0)
                        {
                        ddlsemester.DataSource = dssemester;
                        //ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlsemester.DataValueField = dssemester.Tables[0].Columns[0].ToString();
                        ddlsemester.DataTextField = dssemester.Tables[0].Columns[1].ToString();

                        ddlsemester.DataBind();
                        ddlsemester.SelectedIndex = 0;

                        }
                    }
                else
                    {
                    ddlsemester.Items.Clear();
                    //ddlsemester.DataSource = null;
                    //ddlsemester.DataValueField = "0";
                    //ddlsemester.DataTextField = "Please Select";
                    //ddlsemester.DataBind();
                    //ddlsemester.SelectedIndex = 0;
                    ddlsemester.Items.Add(new ListItem("Please Select", "0"));
                    ddlassessment.SelectedIndex = 0;
                    }














                ddlsemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ddlcourse.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["COURSENO"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["STARTDATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["STARTDATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["ENDDATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ENDDATE"].ToString();
                ddlDuration.SelectedItem.Text = ds.Tables[0].Rows[0]["DURATION"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["DURATION"].ToString();
                if (ddlDuration.SelectedItem.Text == "Semester Long")
                    {
                    ddlDuration.SelectedValue = "1";
                    }
                else if (ddlDuration.SelectedItem.Text == "Year Long")
                    {
                    ddlDuration.SelectedValue = "2";
                    }
                else if (ddlDuration.SelectedItem.Text == "Dates")
                    {
                    ddlDuration.SelectedValue = "3";
                    }
                else
                    {
                    ddlDuration.SelectedValue = "0";
                    }



                ddlassessment.SelectedItem.Text = ds.Tables[0].Rows[0]["ASSESSMENT"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ASSESSMENT"].ToString();
                if (ddlassessment.SelectedItem.Text == "Yes")
                    {

                    ddlassessment.SelectedValue = "1";
                    }
                else if (ddlassessment.SelectedItem.Text == "No")
                    {
                    ddlassessment.SelectedValue = "2";
                    }
                else
                    {
                    ddlassessment.SelectedValue = "0";
                    }

                ddlgrade.SelectedItem.Text = ds.Tables[0].Rows[0]["GRADE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["GRADE"].ToString();

                if (ddlgrade.SelectedItem.Text == "Yes")
                    {

                    ddlgrade.SelectedValue = "1";
                    }
                else if (ddlgrade.SelectedItem.Text == "No")
                    {
                    ddlgrade.SelectedValue = "2";
                    }
                else
                    {
                    ddlgrade.SelectedValue = "0";
                    }
                //btnAdd.Visible = false;
                btnSubmit.Text = "Update";

                ddlCollegeScheme.Enabled = false;
                }


            //}

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DefineIntake.GetEventDetailsByTitleId-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
        {
        //clear();
        //lvvaluecourse.DataSource = null;s
        //lvvaluecourse.DataBind();


        ImageButton btnEditEvent = sender as ImageButton;
        ViewState["action"] = "edit";
        int valueno = int.Parse(btnEditEvent.CommandArgument);
        //string courseno = btnEditEvent.CommandName;
        int Groupid = Convert.ToInt32(btnEditEvent.CommandName);

        ViewState["VALUENO"] = valueno;
        //GetEventDetailsByTitleId(valueno,courseno);
        GetDetailsByValueNo(valueno, Convert.ToInt32(Groupid));



        }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;


        if (Session["ValueCourseTbl"] != null && ((DataTable)Session["ValueCourseTbl"]) != null)
            {

            dt = ((DataTable)Session["ValueCourseTbl"]);
            DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
            ddlCollegeScheme.SelectedValue = dr["COSCHNO"].ToString();
            int Schemeno = Convert.ToInt32(objCommon.LookUp("acd_college_scheme_mapping", "ISNULL(SCHEMENO,0) as SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlCollegeScheme.SelectedValue)));
            DataSet dssemester = objSC.GetSchemwiseSemester(Convert.ToInt32(Schemeno), 1);

            if (dssemester.Tables[0].Rows.Count > 0)
                {
                ddlsemester.DataSource = dssemester;
                //ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlsemester.DataValueField = dssemester.Tables[0].Columns[0].ToString();
                ddlsemester.DataTextField = dssemester.Tables[0].Columns[1].ToString();

                ddlsemester.DataBind();
                ddlsemester.SelectedIndex = 0;
                }
            ddlsemester.SelectedValue = dr["SEMESTERNO"].ToString();

            objCommon.FillDropDownList(ddlgroupname, "ACD_GROUP_MASTER_SPECIALIZATION", "GROUPID", "GROUP_NAME", "ACTIVE_STATUS=1", "GROUPID");
            //txtGroupName.Text
            ddlgroupname.SelectedValue = dr["GROUPID"].ToString();
            //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE  C INNER JOIN acd_college_scheme_mapping  AC ON (C.SCHEMENO = AC.SCHEMENO) ", "DISTINCT (C.COURSENO)", "C.COURSE_NAME", "C.COURSENO>0 AND AC.COSCHNO=" + ddlCollegeScheme.SelectedValue, "C.COURSE_NAME");

            ddlcourse.SelectedValue = dr["COURSENO"].ToString();
            txtStartDate.Text = dr["STARTDATE"].ToString();
            txtEndDate.Text = dr["ENDDATE"].ToString();
            ddlDuration.SelectedValue = dr["DURATIONNO"].ToString();
            // ddlDuration.SelectedItem.Text = dr["DURATION"].ToString();
            ddlassessment.SelectedValue = dr["ASSESSMENTNO"].ToString();
            //ddlassessment.SelectedItem.Text = dr["ASSESSMENT"].ToString();
            ddlgrade.SelectedValue = dr["GRADENO"].ToString();


            dt.Rows.Remove(dr);
            Session["RegFeeTbl"] = dt;
            lvvaluecourse.DataSource = dt;
            lvvaluecourse.DataBind();
            // btnAdd.Text = "Edit";

            }
        }
    private DataRow GetEditableDataRow(DataTable dt, string value)
        {
        DataRow dataRow = null;
        try
            {
            foreach (DataRow dr in dt.Rows)
                {
                if (dr["VALUE_NO"].ToString() == value)
                    {
                    dataRow = dr;
                    break;
                    }
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DefineIntake.GetEditableDataRow-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        return dataRow;
        }



    protected void btnback_Click(object sender, EventArgs e)
        {
        upd_ModalPopupExtender1.Hide();
        }
    protected void btnview_Click(object sender, EventArgs e)
        {
        upd_ModalPopupExtender1.Show();
        DataSet ds = objSC.GetViewGroupData();
        lvmodal.DataSource = null;
        lvmodal.DataBind();
        pnlmodal.Visible = false;
        lvmodal.Visible = false;
        ddlgroup.Items.Clear();
        ddlgroup.Items.Add(new ListItem("Please Select", "0"));

        if (ds.Tables[0].Rows.Count > 0)
            {
            ddlgroup.DataSource = ds;
            ddlgroup.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlgroup.DataTextField = ds.Tables[0].Columns[1].ToString();
            

            ddlgroup.DataBind();
            //ddlgroup.SelectedIndex = 0;
            pnl.Visible = true;

            }

        }
    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlgroup.SelectedIndex > 0)
            {
            DataSet ds = objSC.GetCountDataShow(ddlgroup.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
                {

                lvmodal.DataSource = ds;
                lvmodal.DataBind();
                pnl.Visible = true;
                pnlmodal.Visible = true;
                lvmodal.Visible = true;

                }
            }
        }
    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlsemester.SelectedIndex > 0)
            {


            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeScheme.SelectedValue));


            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                }

            int Schemeno = Convert.ToInt32(objCommon.LookUp("acd_college_scheme_mapping", "ISNULL(SCHEMENO,0) as SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlCollegeScheme.SelectedValue)));

            DataSet dscourse = objSC.GetSchemwiseSemester(Schemeno, 2);

            if (dscourse.Tables[0].Rows.Count > 0)
                {
                ddlcourse.DataSource = dscourse;
                //ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlcourse.DataValueField = dscourse.Tables[0].Columns[0].ToString();
                ddlcourse.DataTextField = dscourse.Tables[0].Columns[1].ToString();

                ddlcourse.DataBind();
                ddlcourse.SelectedIndex = 0;
                }

            //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE  C INNER JOIN acd_college_scheme_mapping  AC ON (C.SCHEMENO = AC.SCHEMENO) ", "DISTINCT C.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSENAME", "C.COURSENO>0 AND AC.SCHEMENO=" + Schemeno, "C.COURSENO");
            //BindCollegeSchemeData();
            //if (ddlCollegeScheme.SelectedIndex > 0)
            //    {
            //objCommon.FillDropDownList(ddlsemester, "ACD_GROUP_MASTER_SPECIALIZATION", "GROUPID", "GROUP_NAME", "ACTIVE_STATUS=1", "GROUPID");

            BindCollegeSchemeData();
            //}
            //BindCollegeSchemeData();
            }
        }
    }