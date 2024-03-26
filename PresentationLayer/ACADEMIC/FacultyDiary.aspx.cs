//=================================================================================               
// CREATION DATE : 08/01/2024                                       
// CREATED BY    : RISHABH                            
// MODIFIED BY   : 
// MODIFICATIONS :                                                 
// MODIFIED DATE :                      
//=================================================================================
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using System.IO;

public partial class ACADEMIC_FacultyDiary : System.Web.UI.Page
{
    Common objCommon = new Common();

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
                getdropdowns();
                //Page Authorization
                this.CheckPageAuthorization();
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
                Response.Redirect("~/notauthorized.aspx?page=FacultyDiary.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FacultyDiary.aspx");
        }
    }

    public void getdropdowns()
    {
        try
        {
            AcdAttendanceController objAttC = new AcdAttendanceController();

            DataSet ds = objAttC.Get_FacultyDiary_Data(Convert.ToInt32(Session["userno"]));
            Session["ds"] = ds;

            DataView view = new DataView(ds.Tables[0]);
            DataTable distinctValues = view.ToTable(true, "COLLEGE_SCHEME", "COL_SCHEME_NAME");
            ddlCollegeScheme.DataSource = distinctValues;
            ddlCollegeScheme.DataTextField = distinctValues.Columns[1].ToString();
            ddlCollegeScheme.DataValueField = distinctValues.Columns[0].ToString();
            ddlCollegeScheme.DataBind();
        }
        catch
        {
            throw;
        }
    }

    public void BindDropDowns(DropDownList ddl, string columnname, string columno, string expression, int table)
    {
        try
        {
            DataSet ds = (DataSet)Session["ds"];
            DataTable distinctValues = ds.Tables[table].Select(expression).CopyToDataTable();
            DataView view = new DataView(distinctValues);

            distinctValues = view.ToTable(true, columno, columnname);

            ddl.Items.Clear();
            ddl.Items.Add("Please Select");
            ddl.SelectedItem.Value = "0";
            ddl.DataSource = distinctValues;
            ddl.DataTextField = distinctValues.Columns[1].ToString();
            ddl.DataValueField = distinctValues.Columns[0].ToString();
            ddl.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCollegeScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnSubmit.Visible = false;
        btnReport.Visible = false;
        if (ddlCollegeScheme.SelectedIndex > 0)
        {
            string College_Scheme = ddlCollegeScheme.SelectedValue.ToString();
            string[] repoarray;
            repoarray = College_Scheme.Split('-');
            int collegeId = Convert.ToInt32(repoarray[0]);
            int schemeno = Convert.ToInt32(repoarray[1]);

            BindDropDowns(ddlSession, "SESSION_NAME", "SESSIONNO", "COLLEGE_ID=" + collegeId + " AND SCHEMENO=" + schemeno, 0);
            ddlSession.Focus();
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnSubmit.Visible = false;
        btnReport.Visible = false;
        if (ddlSession.SelectedIndex > 0)
        {
            string College_Scheme = ddlCollegeScheme.SelectedValue.ToString();
            string[] repoarray;
            repoarray = College_Scheme.Split('-');
            int collegeId = Convert.ToInt32(repoarray[0]);
            int schemeno = Convert.ToInt32(repoarray[1]);

            BindDropDowns(ddlSemester, "SEMESTERNAME", "SEMESTERNO", "COLLEGE_ID=" + collegeId + " AND SCHEMENO=" + schemeno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), 0);
            ddlSemester.Focus();
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            AcdAttendanceController objAttC = new AcdAttendanceController();
            Attendance.FacultyDiary objEFac = new Attendance.FacultyDiary();

            string College_Scheme = ddlCollegeScheme.SelectedValue.ToString();
            string[] repoarray;
            repoarray = College_Scheme.Split('-');
            int collegeId = Convert.ToInt32(repoarray[0]);
            int schemeno = Convert.ToInt32(repoarray[1]);

            objEFac.CollegeId = collegeId;
            objEFac.Schemeno = schemeno;
            objEFac.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objEFac.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objEFac.Operator = ddlOptVal.SelectedItem.Text;
            objEFac.Percentage = Convert.ToInt32(txtPercentage.Text);
            objEFac.FromDate = txtFromDate.Text.ToString();
            objEFac.ToDate = txtToDate.Text.ToString();


            DataSet ds = objAttC.Get_Students_FacultyDiary(objEFac);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                btnSubmit.Visible = true;
                btnReport.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
                btnReport.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                objCommon.DisplayMessage(updFac, "No data found.", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AcdAttendanceController objAttC = new AcdAttendanceController();
        Attendance.FacultyDiary objEFac = new Attendance.FacultyDiary();

        try
        {
            // Create a DataTable to store the ListView data
            DataTable dt = new DataTable();
            dt.Columns.Add("ROLLNO", typeof(string));
            dt.Columns.Add("REGNO", typeof(string));
            dt.Columns.Add("STUDNAME", typeof(string));
            dt.Columns.Add("ATTENDANCE_PERCENTAGE", typeof(string));
            dt.Columns.Add("REMARK", typeof(string));

            // Iterate through each item in the ListView and add data to the DataTable
            foreach (ListViewItem item in lvStudent.Items)
            {
                TextBox txtRemark = (TextBox)item.FindControl("txtRemark");
                if (!string.IsNullOrEmpty(txtRemark.Text))
                {
                    DataRow row = dt.NewRow();
                    row["ROLLNO"] = ((Label)item.FindControl("lblRollNo")).Text;
                    row["REGNO"] = ((Label)item.FindControl("lblRegNo")).Text;
                    row["STUDNAME"] = ((Label)item.FindControl("lblStudName")).Text;
                    row["ATTENDANCE_PERCENTAGE"] = ((Label)item.FindControl("lblAttendancePercentage")).Text;
                    row["REMARK"] = ((TextBox)item.FindControl("txtRemark")).Text;

                    dt.Rows.Add(row);
                }
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dt);
            string xmldata = dataSet.GetXml();

            string College_Scheme = ddlCollegeScheme.SelectedValue.ToString();
            string[] repoarray;
            repoarray = College_Scheme.Split('-');
            int collegeId = Convert.ToInt32(repoarray[0]);
            int schemeno = Convert.ToInt32(repoarray[1]);
            objEFac.CollegeId = collegeId;
            objEFac.Schemeno = schemeno;
            objEFac.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objEFac.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objEFac.Operator = ddlOptVal.SelectedItem.Text;
            objEFac.Percentage = Convert.ToInt32(txtPercentage.Text);
            objEFac.FromDate = txtFromDate.Text.ToString();
            objEFac.ToDate = txtToDate.Text.ToString();


            int ret = objAttC.SaveFacultyDiary(objEFac, xmldata);
            if (ret == 1)
            {
                objCommon.DisplayMessage(updFac, "Data Added Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updFac, "No data found.", this.Page);
            }
        }
        catch
        {
            throw;
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("FacultyDiary", "FacultyDiary.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string College_Scheme = ddlCollegeScheme.SelectedValue.ToString();
            string[] repoarray;
            repoarray = College_Scheme.Split('-');
            int collegeId = Convert.ToInt32(repoarray[0]);
            int schemeno = Convert.ToInt32(repoarray[1]);


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + collegeId
                + ",@P_COLLEGEID=" + collegeId
                + ",@P_SCHEMENO=" + schemeno
                + ",@P_SESSIONNO=" + ddlSession.SelectedValue
                + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue
                + ",@P_UANO=" + Session["userno"].ToString();



            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFac, this.updFac.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        btnReport.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        ClearAll();
    }

    private void ClearAll()
    {
        ddlCollegeScheme.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlOptVal.SelectedIndex = 2;
        txtPercentage.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
}