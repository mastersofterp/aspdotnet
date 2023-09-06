using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_ExtraClassTT : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttE = new AcdAttendanceModel();

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
                getdropdowns();
                //to fill slots
                //this.FillSlots();

                //to load all dropdown list
                //this.PopulateDropDownList();

                //assign session values to static variables
                //sessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
                //IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                //Session["ipAddress"].ToString();
                Session["transferTbl"] = null;
                ViewState["TH_PR"] = 0;
                ViewState["RECORDCHECK"] = null;
                //OrgID = Session["OrgId"].ToString();
                //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
            }
        }

    }

    public void getdropdowns()
    {
        DataSet ds = objAttC.Get_Extra_Class_DD(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["OrgId"]));
        Session["ds"] = ds;
        DataTable dt = null;
        DataView view = new DataView(ds.Tables[0]);
        DataTable distinctValues = view.ToTable(true, "COSCHNO", "COL_SCHEME_NAME");
        ddlClgname.DataSource = distinctValues;
        ddlClgname.DataTextField = distinctValues.Columns[1].ToString();
        ddlClgname.DataValueField = distinctValues.Columns[0].ToString();
        ddlClgname.DataBind();
    }

    public void BindDropDowns(DropDownList ddl, string columnname, string columno, string expression, int table)
    {
        DataSet ds = (DataSet)Session["ds"];
        DataTable distinctValues = ds.Tables[table].Select(expression).CopyToDataTable();
        DataView view = new DataView(distinctValues);
        //if (expression == string.Empty)
        //{
        distinctValues = view.ToTable(true, columno, columnname);
        //}
        //else
        //{
        //   distinctValues = view.ToTable(true, columno, columnname).Select(expression).CopyToDataTable();
        //}
        ddl.Items.Clear();
        ddl.Items.Add("Please Select");
        ddl.SelectedItem.Value = "0";
        ddl.DataSource = distinctValues;
        ddl.DataTextField = distinctValues.Columns[1].ToString();
        ddl.DataValueField = distinctValues.Columns[0].ToString();
        ddl.DataBind();
        ViewState["RECORDCHECK"] = null;
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        ViewState["RECORDCHECK"] = null;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        if (ddlClgname.SelectedIndex > 0)
        {
            BindDropDowns(ddlSession, "SESSION_NAME", "SESSIONNO", "COSCHNO=" + ddlClgname.SelectedValue, 0);
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ViewState["RECORDCHECK"] = null;
        if (ddlSession.SelectedIndex > 0)
        {
            BindDropDowns(ddlDepartment, "DEPTNAME", "DEPTNO", "", 0);
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ViewState["RECORDCHECK"] = null;
        if (ddlDepartment.SelectedIndex > 0)
        {
            BindDropDowns(ddlSem, "SEMESTERNAME", "SEMESTERNO", "COSCHNO=" + ddlClgname.SelectedValue + " AND SESSIONNO=" + ddlSession.SelectedValue, 0);
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSlotType.SelectedIndex = -1;
        //ddlSection.SelectedIndex = -1;
        //txtStartDate.Text = string.Empty;
        //txtEndDate.Text = string.Empty;
        //BindDropDowns(ddlSection, "SECTIONNAME", "SECTIONNO", "COSCHNO=" + ddlClgname.SelectedValue, 0);

        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ViewState["RECORDCHECK"] = null;
        if (ddlSem.SelectedIndex > 0)
        {
            BindDropDowns(ddlCourse, "COURSE_NAME", "COURSENO", "COSCHNO=" + ddlClgname.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SESSIONNO=" + ddlSession.SelectedValue, 0);
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        ViewState["RECORDCHECK"] = null;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Theory", "1"));
        ddlCourseType.Items.Add(new ListItem("Tutorial", "2"));
        ddlCourseType.Items.Add(new ListItem("Practical", "3"));
        if (ddlCourse.SelectedIndex > 0)
        {
            DataSet ds = (DataSet)Session["ds"];

            DataTable dt = (ds.Tables[0].Select("COURSENO=" + ddlCourse.SelectedValue).CopyToDataTable());

            decimal tutorial = Convert.ToInt32(dt.Rows[0]["THEORY"]);
            //  Convert.ToDecimal(ds.Tables[0].Rows[0]["THEORY"].ToString());
            int TH_PR = Convert.ToInt32(dt.Rows[0]["TH_PR"]);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                if (tutorial > 0 && TH_PR == 1)
                {
                    ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("3"));
                    ddlCourseType_SelectedIndexChanged(sender, e);
                }
                else if (TH_PR == 1)
                {
                    ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("2"));
                    ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("3"));
                    ViewState["TH_PR"] = TH_PR;
                    ddlCourseType_SelectedIndexChanged(sender, e);
                }
                else
                {
                    ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("1"));
                    ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("2"));
                    ViewState["TH_PR"] = TH_PR;
                    ddlCourseType_SelectedIndexChanged(sender, e);
                }
            }
            if (ddlCourse.SelectedIndex > 0)
            {
                BindDropDowns(ddlSection, "SECTIONNAME", "SECTIONNO", "COSCHNO=" + ddlClgname.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue, 0);
            }
        }
    }


    protected void ddlCourseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.SelectedIndex = -1;
        if (ddlCourseType.SelectedValue != "1")
        {
            divBatch.Visible = true;
        }
        else
        {
            divBatch.Visible = false;
        }
        ViewState["RECORDCHECK"] = null;
    }


    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ViewState["RECORDCHECK"] = null;
        if (ddlSection.SelectedIndex > 0)
        {
            BindDropDowns(ddlSlotType, "SLOTTYPE_NAME", "SLOTTYPENO", "", 1);
            if (ddlCourseType.SelectedValue == "2")
            {
                divBatch.Visible = true;
                BindDropDowns(ddlBatch, "BATCHNAME", "BATCHNO", "COSCHNO=" + ddlClgname.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(BATCHNO,0)>0 AND TH_PR=1", 0); //Fill tutorial course batches
            }
            else if (ddlCourseType.SelectedValue == "3")
            {
                divBatch.Visible = true;
                BindDropDowns(ddlBatch, "BATCHNAME", "BATCHNO", "COSCHNO=" + ddlClgname.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(BATCHNO,0)>0 AND TH_PR=2", 0); //Fill practical course batches
            }
        }
    }

    protected void ddlSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        DataSet ds = (DataSet)Session["ds"];
        DataRow[] row = ds.Tables[0].Select("COSCHNO=" + ddlClgname.SelectedValue);
        ViewState["RECORDCHECK"] = null;
        if (ddlSlotType.SelectedIndex > 0)
        {
            BindDropDowns(ddlSlot, "SLOTNAME", "SLOTNO", "SCHEMENO=" + row[0]["SCHEMENO"].ToString() + " AND SLOTTYPE=" + ddlSlotType.SelectedValue + " AND COLLEGE_ID=" + row[0]["COLLEGE_ID"].ToString() + " AND DEGREENO=" + row[0]["DEGREENO"].ToString(), 2);
        }
    }
    protected void bntSave_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)Session["ds"];
        DataRow[] row = ds.Tables[0].Select("COSCHNO=" + ddlClgname.SelectedValue);
        int college_id = Convert.ToInt32(row[0]["COLLEGE_ID"].ToString());
        int schemeno = Convert.ToInt32(row[0]["SCHEMENO"].ToString());
        int deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
        int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
        int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
        int slotno = Convert.ToInt32(ddlSlot.SelectedValue);
        DateTime Sdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime Edate = Convert.ToDateTime(txtEndDate.Text);
        int slottype = Convert.ToInt32(ddlSlotType.SelectedValue);
        string ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int uano = Convert.ToInt32(Session["userno"]);
        int orgid = Convert.ToInt32(Session["OrgId"]);
        int batchno = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
        ViewState["RECORDCHECK"] = null;
        CustomStatus cs = (CustomStatus)objAttC.Insert_External_Class(college_id, schemeno, sessionno, uano, courseno, sectionno, semesterno, Sdate, Edate, slotno, ipaddress, orgid, slottype, batchno);
        //if (CustomStatus.RecordSaved.Equals(cs.ToString()))
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updTimeTable, "External Time Table created successfully.", this.Page);
            btnExisting_Click(sender, e);
        }
    }
    protected void btnExisting_Click(object sender, EventArgs e)
    {

        DataSet ds = objAttC.Get_Extra_Class_Time_Table(Convert.ToInt32(Session["userno"]));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvTimeTable.DataSource = ds;
            lvTimeTable.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvTimeTable);//Set label 
            lvTimeTable.Visible = true;
        }
        else
        {
            if (ViewState["RECORDCHECK"] == null)
            {
                objCommon.DisplayMessage(updTimeTable, "No data found.", this.Page);
            }
            lvTimeTable.DataSource = null;
            lvTimeTable.DataBind();
            lvTimeTable.Visible = false;
        }
    }
    protected void bntCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void BindListView()
    {
        DataSet ds = objAttC.Get_Extra_Class_Time_Table(Convert.ToInt32(Session["userno"]));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvTimeTable.DataSource = ds;
            lvTimeTable.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvTimeTable);//Set label 
            lvTimeTable.Visible = true;
        }
        else
        {
            if (ViewState["RECORDCHECK"] == null)
            {
                objCommon.DisplayMessage(updTimeTable, "No data found.", this.Page);
            }
            lvTimeTable.DataSource = null;
            lvTimeTable.DataBind();
            lvTimeTable.Visible = false;
        }
    }
    protected void btnCancleExtra_Click(object sender, EventArgs e)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        string TimeTableNo = string.Empty;

        foreach (ListViewDataItem item in lvTimeTable.Items)
        {
            if ((item.FindControl("chkTimetable") as CheckBox).Checked)
            {
                if (TimeTableNo.Length > 0)
                    TimeTableNo += "$";
                TimeTableNo += (item.FindControl("hidTtno") as HiddenField).Value.Trim();
            }
        }
        if (TimeTableNo == string.Empty)
        {
            objCommon.DisplayMessage(this.Page, "Please select atleast one Time table/record to cancel", this.Page);
            BindListView();
        }
        else
        {
            retStatus = objAttC.Update_Extra_Class_Time_Table(TimeTableNo);

            if (retStatus == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.Page, "Extra Time Table Cancelled Sucessfully!", this.Page);
                ViewState["RECORDCHECK"] = 1;
                btnExisting_Click(sender, e);
            }
            BindListView();
        }
    }
}