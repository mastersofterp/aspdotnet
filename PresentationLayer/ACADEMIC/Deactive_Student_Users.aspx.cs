using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_Deactive_Student_Users : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController objStud = new StudentController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Deactive_Student_Users.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlDegree, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO>0 AND ACTIVESTATUS=1", "");

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Deactive_Student_Users.PopulateDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    public void BindListView()
    {
        int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
        int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        int BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);

        DataSet ds = objStud.GetPassOutStudentData(BatchNo, DegreeNo, BranchNo);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.Visible = true;
            pnlStudent.Visible = true;
            btnDeactive.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
        }
        else
        {
            lvStudent.Visible = false;
            pnlStudent.Visible = false;
            btnDeactive.Visible = false;
            lvStudent.DataBind();
            objCommon.DisplayMessage(this.Page, "Record not found.", Page);
            return;
        }


        foreach (ListViewDataItem dataitem in lvStudent.Items)
        {
            Label ActiveStatus = dataitem.FindControl("lblStatus") as Label;

            if (ActiveStatus.Text == "ACTIVE")
            {
                ActiveStatus.Text = "ACTIVE";
                ActiveStatus.Style.Add("color", "Green");
            }
            else
            {
                ActiveStatus.Text = "INACTIVE";
                ActiveStatus.Style.Add("color", "Red");
            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        btnDeactive.Visible = false;
        lvStudent.Visible = false;
        pnlStudent.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue, "BRANCHNO");
            ddlBranch.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Deactive_Student_Users.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    public DataTable CreateTable_PassOutStudents()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UANO", typeof(int));
        dt.Columns.Add("UA_NAME", typeof(string));
        dt.Columns.Add("IDNO", typeof(int));
        dt.Columns.Add("STUDENTNAME", typeof(string));
        dt.Columns.Add("FACULTY_NAME", typeof(string));
        dt.Columns.Add("STATUS", typeof(string));
        return dt;
    }

    protected void btnDeactive_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = CreateTable_PassOutStudents();
        int rowIndex = 0;
        int count = 0;

        foreach (var lvItems in lvStudent.Items)
        {
            DataRow dRow = dt.NewRow();
            HiddenField hfdUaNo = lvItems.FindControl("hfdUaNo") as HiddenField;
            Label lblUserName = lvItems.FindControl("lblUserName") as Label;
            HiddenField hfdIdNo = lvItems.FindControl("hfdIdNo") as HiddenField;
            Label lblStudentName = lvItems.FindControl("lblStudentName") as Label;
            Label lblFacultyName = lvItems.FindControl("lblFacultyName") as Label;
            Label lblStatus = lvItems.FindControl("lblStatus") as Label;
            CheckBox chkActive = lvItems.FindControl("chkActive") as CheckBox;

            if (chkActive.Checked == true && chkActive.Enabled == true)
            {
                dRow["UANO"] = hfdUaNo.Value;
                dRow["UA_NAME"] = lblUserName.Text;
                dRow["IDNO"] = hfdIdNo.Value;
                dRow["STUDENTNAME"] = lblStudentName.Text;
                dRow["FACULTY_NAME"] = lblFacultyName.Text;
                dRow["STATUS"] = lblStatus.Text;
                dt.Rows.Add(dRow);
                rowIndex++;
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select atleast One Student to Deactivate!", this.Page);
            BindListView();
            return;
        }


        ds.Tables.Add(dt);
        string PassOutStudents = ds.GetXml();
        int UaNo = Convert.ToInt32(Session["userno"].ToString());
        int PageNo = Convert.ToInt32(Request.QueryString["pageno"].ToString());

        int status = Convert.ToInt32(objStud.DeActiveStudentsList(PassOutStudents, UaNo, PageNo));

        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Student Deactived Successfully!", Page);
            BindListView();
            return;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error!", this.Page);
            return;
        }
    }

    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            CheckBox chkActive = (CheckBox)e.Item.FindControl("chkActive");

            if (lblStatus.Text == "INACTIVE")
            {
                chkActive.Enabled = false;
            }
            else
            {
                chkActive.Enabled = true;
            }
        }
    }
}