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
using System.Linq;

public partial class ACADEMIC_Minor_creation : System.Web.UI.Page
{
    Common objCommon = new Common();
    DataSet dss = new DataSet();

    static string sessionuid = string.Empty;
    static string msessionuid = string.Empty;
    static string IpAddress = string.Empty;
    static string OrgID = string.Empty;
    string ddlclgid = string.Empty;
    string mnr = string.Empty;
    string ddlclg = string.Empty;
    string ddlmnr = string.Empty;
    string ddlscheme = string.Empty;
    string ddlCourse = string.Empty;
    int crdt = 0;
    int mnrgprno = 0;

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
                Response.Redirect("~/default_atlas.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //to load all dropdown list
                this.PopulateDropDownList();
                this.PopulateCourseDropDownList();
                //this.MinorBind();
                this.BindListView();
                this.BindCourseListView();

                //assign session values to static variables
                
                OrgID = Session["OrgId"].ToString();
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Minor_creation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Minor_creation.aspx");
        }
    }

    #region First Tab "Create Minor"
    protected void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID)", "CDB.CDBNO", "CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_PROGRAM, CM.COLLEGE_ID, CM.COLLEGE_NAME, B.BRANCHNO, B.LONGNAME, CDB.COLLEGE_ID AS CDB_COLLEGE_ID, CDB.BRANCHNO AS CDB_BRANCH_NO, CM.OrganizationId AS CM_ORGID, B.OrganizationId AS B_ORGID", "CM.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND B.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CDB.CDBNO");
        //pnlCourse.Visible = false;
        //pnlMinor.Visible = false;
    }

    protected void PopulateCourseDropDownList()
    {
        objCommon.FillDropDownList(ddldegreeBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID)", "CDB.CDBNO", "CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_PROGRAM, CM.COLLEGE_ID, CM.COLLEGE_NAME, B.BRANCHNO, B.LONGNAME, CDB.COLLEGE_ID AS CDB_COLLEGE_ID, CDB.BRANCHNO AS CDB_BRANCH_NO, CM.OrganizationId AS CM_ORGID, B.OrganizationId AS B_ORGID", "CM.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND B.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CDB.CDBNO");
        //pnlCourse.Visible = false;
        //pnlMinor.Visible = false;
    }

    protected void BindListView()
    {
        dss = objCommon.GetMcreate();

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            lvMinor.DataSource = dss;
            lvMinor.DataBind();
            lvMinor.Visible = true;
        }
        else
        {
            lvMinor.DataSource = null;
            lvMinor.DataBind();
            lvMinor.Visible = false;
        }
    }

    protected void BindCourseListView()
    {
        dss = objCommon.GetCcreate();

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            lvCourse.DataSource = dss;
            lvCourse.DataBind();
            lvCourse.Visible = true;
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
        }
    }

    protected void AllClear()
    {
        ddlCollege.Items.Clear();
        ddlCollege.Items.Add(new ListItem("Please Select", "0"));
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID)", "CDB.CDBNO", "CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_PROGRAM, CM.COLLEGE_ID, CM.COLLEGE_NAME, B.BRANCHNO, B.LONGNAME, CDB.COLLEGE_ID AS CDB_COLLEGE_ID, CDB.BRANCHNO AS CDB_BRANCH_NO, CM.OrganizationId AS CM_ORGID, B.OrganizationId AS B_ORGID", "CM.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND B.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CDB.CDBNO");
        txtminor.Text = string.Empty;
        txtCredit.Text = string.Empty;
    }

    protected void AllCleared()
    {
        ddldegreeBranch.Items.Clear();
        ddldegreeBranch.Items.Add(new ListItem("Please Select", "0"));
        PopulateCourseDropDownList();
        ddlCourseMinor.Items.Clear();
        ddlCourseMinor.Items.Add(new ListItem("Please Select", "0"));
        //BindMinor(ddldegreeBranch.SelectedValue);
        ddlSemesterminor.Items.Clear();
        ddlSemesterminor.Items.Add(new ListItem("Please Select", "0"));
        //BindScheme(ddldegreeBranch.SelectedValue);
        ddlCourses.Items.Clear();
        //ddlCourses.Items.Add(new ListItem("Please Select", "0"));

    }

    protected void imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmit.Text = "Update";
        DataSet ds = new DataSet();
        ImageButton btn = sender as ImageButton;
        int MnrGrpNo = Convert.ToInt32(btn.CommandArgument);
        hdfsub.Value = MnrGrpNo.ToString();
        ds = objCommon.GetMEdit(MnrGrpNo);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlCollege.SelectedItem.Text = ds.Tables[0].Rows[0]["COLLEGE_PROGRAM"].ToString();
            ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["CDB_NO"].ToString();
            txtminor.Text = ds.Tables[0].Rows[0]["MNR_GRP_NAME"].ToString();
            txtCredit.Text = ds.Tables[0].Rows[0]["CREDIT"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ST = 0;
        string strViewRecord = string.Empty;
        //string strViewRecordCollege = string.Empty;

        try
        {
            ddlclgid = ddlCollege.SelectedValue;
            mnr = txtminor.Text.ToUpper().Trim();
            crdt = Convert.ToInt32(txtCredit.Text);
            sessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
            msessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
            IpAddress = Request.ServerVariables["REMOTE_ADDR"];

            strViewRecord = objCommon.LookUp("ACD_MINOR_GROUP_MASTER", "MNR_GRP_NO", "MNR_GRP_NAME='" + mnr + "' AND CDB_NO =" + ddlclgid);
            //strViewRecordCollege = objCommon.LookUp("ACD_MINOR_GROUP_MASTER", "MNR_GRP_NO", "CDB_NO =" + ddlclgid);
            

            if (btnSubmit.Text == "Submit")
            {
                if (strViewRecord != string.Empty)
                {
                    objCommon.DisplayMessage(this.updMinor, "Record is Already Exists...", this.Page);
                    return;
                }

                ST = objCommon.Mcreate(ddlclgid, mnr, crdt, sessionuid, IpAddress, msessionuid, mnrgprno);
                objCommon.DisplayMessage(this.updMinor, "Minor Created Successfully!!!", this.Page);
            }
            else if (btnSubmit.Text == "Update")
            {
                ST = objCommon.Mcreate(ddlclgid, mnr, crdt, sessionuid, IpAddress, msessionuid, Convert.ToInt32(hdfsub.Value));
                objCommon.DisplayMessage(this.updMinor, "Minor Updated Successfully!!!", this.Page);
            }

            //if (ST != 0)
            //{
            //    if (btnSubmit.Text == "Submit")
            //    {
            //        objCommon.DisplayMessage("Minor Created Successfully!!!", this.Page);
            //    }
            //    else if (btnSubmit.Text == "Update")
            //    {
            //        objCommon.DisplayMessage("Minor Updated Successfully!!!", this.Page);
            //    }
            //}

            AllClear();
            BindListView();
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //AllClear();
        Response.Redirect(Request.Url.ToString());
        btnSubmit.Text = "Submit";
    }
    #endregion First Tab "Create Minor"

    #region Second Tab "Add Course"
    protected void imgbtnCourse_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmitCourse.Text = "Update";
        DataSet ds = new DataSet();
        ImageButton btn = sender as ImageButton;
        int MnrGrpNo = Convert.ToInt32(btn.CommandArgument);
        hdfcourse.Value = MnrGrpNo.ToString();
        ds = objCommon.GetCEdit(MnrGrpNo);
        string cdbNo = string.Empty;
        string schemeNo = string.Empty;
        string courseGroup = string.Empty;
        string courseNoGroup = string.Empty;

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            PopulateCourseDropDownList();
            //ddldegreeBranch.SelectedItem.Text = ds.Tables[0].Rows[0]["COLLEGE_PROGRAM"].ToString();
            ddldegreeBranch.SelectedValue = ds.Tables[0].Rows[0]["CDB_NO"].ToString();
            cdbNo = ds.Tables[0].Rows[0]["CDB_NO"].ToString();
            schemeNo = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
            BindMinor(cdbNo);
            //ddlCourseMinor.SelectedItem.Text = ds.Tables[0].Rows[0]["MNR_GRP_NAME"].ToString();
            ddlCourseMinor.SelectedValue = ds.Tables[0].Rows[0]["MNR_GRP_NO"].ToString();
            BindScheme(cdbNo);
            //ddlSemesterminor.SelectedItem.Text = ds.Tables[0].Rows[0]["SCHEMENAME"].ToString();
            ddlSemesterminor.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
            BindCourse(cdbNo, schemeNo);
            courseGroup = ds.Tables[0].Rows[0]["COURSE_NAME"].ToString();
            courseNoGroup = ds.Tables[0].Rows[0]["COURSENO"].ToString();

            foreach (ListItem item in ddlCourses.Items)
            {
                if (courseNoGroup.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
            //ddlCourses.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"].ToString();
        }
    }

    protected void btnSubmitCourse_Click(object sender, EventArgs e)
    {
        int BT = 0;
        string strViewRecord = string.Empty;
        string cCode = string.Empty;
        string cName = string.Empty;
        try
        {
            ddlclg = ddldegreeBranch.SelectedValue;
            ddlmnr = ddlCourseMinor.SelectedValue;
            ddlscheme = ddlSemesterminor.SelectedValue;
            //ddlCourse = ddlCourses.SelectedValue;
            sessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
            msessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
            IpAddress = Request.ServerVariables["REMOTE_ADDR"];

            foreach(ListItem item in ddlCourses.Items)
            {
                if (item.Selected == true)
                {
                    ddlCourse += item.Value + ",";
                    cName += item.Text + ",";
                    cCode += objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (S.SCHEMENO = C.SCHEMENO)", "C.CCODE, C.COURSE_NAME, S.SCHEMENAME", "COURSENO =" + item.Value) + ",";
                }
            }
            ddlCourse = ddlCourse.TrimEnd(',');
            cName = cName.TrimEnd(',');
            cCode = cCode.TrimEnd(',');

            //cCode = objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (S.SCHEMENO = C.SCHEMENO)", "C.CCODE, C.COURSE_NAME, S.SCHEMENAME", "COURSENO =" + ddlCourse);
            //cName = ddlCourses.SelectedItem.Text;

            if (btnSubmitCourse.Text == "Submit")
            {
                strViewRecord = objCommon.LookUp("ACD_MINOR_OFFERED_COURSE", "MNR_OFFERED_COURSE_NO", "CDB_NO =" + ddlclg + "AND MNR_GRP_NO =" + ddlmnr + "AND SCHEMENO =" + ddlscheme + "AND COURSENO IN (SELECT VALUE FROM dbo.Split('" + ddlCourse + "', ','))");
                if (strViewRecord != string.Empty)
                {
                    objCommon.DisplayMessage(this.updCourse, "Record is Already Exists...", this.Page);
                    return;
                }

                BT = objCommon.Ccreate(ddlclg, ddlmnr, ddlscheme, ddlCourse, cCode, cName, sessionuid, IpAddress, msessionuid, mnrgprno);
                objCommon.DisplayMessage(this.updCourse, "Course Created Successfully!!!", this.Page);
            }
            else if (btnSubmitCourse.Text == "Update")
            {
                BT = objCommon.Ccreate(ddlclg, ddlmnr, ddlscheme, ddlCourse, cCode, cName, sessionuid, IpAddress, msessionuid, Convert.ToInt32(hdfcourse.Value));
                objCommon.DisplayMessage(this.updCourse, "Course Updated Successfully!!!", this.Page);
                btnSubmitCourse.Text = "Submit";
            }

            //if (BT != 0)
            //{
            //    if (btnSubmit.Text == "Submit")
            //    {
            //        objCommon.DisplayMessage("Course Created Successfully!!!", this.Page);
            //    }
            //    else if (btnSubmit.Text == "Update")
            //    {
            //        objCommon.DisplayMessage("Course Updated Successfully!!!", this.Page);
            //    }
            //}

            AllCleared();
            BindCourseListView();
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancelCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //AllCleared();
        //Response.Redirect(Request.Url.ToString());
        btnSubmitCourse.Text = "Submit";
    }

    protected void BindMinor(string degBranch)
    {
        objCommon.FillDropDownList(ddlCourseMinor, "ACD_MINOR_GROUP_MASTER", "MNR_GRP_NO", "MNR_GRP_NAME, CDB_NO", "CDB_NO > 0 AND CDB_NO =" + degBranch, "MNR_GRP_NO");
        //MinorBind();
    }

    protected void BindScheme(string degBranches)
    {
        objCommon.FillDropDownList(ddlSemesterminor, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID) INNER JOIN ACD_SCHEME ACDS ON (ACDS.BRANCHNO = CDB.BRANCHNO)", "ACDS.SCHEMENO", "ACDS.SCHEMENAME, CM.COLLEGE_ID, CM.COLLEGE_NAME, B.BRANCHNO, B.LONGNAME, CDB.COLLEGE_ID AS CDB_COLLEGE_ID, CDB.BRANCHNO AS CDB_BRANCH_NO, CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_PROGRAM, CM.OrganizationId AS CM_ORGID, B.OrganizationId AS B_ORGID, CDB.CDBNO", "CDB.CDBNO > 0 AND CDB.CDBNO =" + degBranches, "ACDS.SCHEMENO");
    }

    //protected void MinorBind()
    //{
    //    //objCommon.FillDropDownList(ddlCourseMinor, "ACD_MINOR_GROUP_MASTER", "MNR_GRP_NO", "MNR_GRP_NAME", "MNR_GRP_NO > 0", "MNR_GRP_NO");
    //    dss = objCommon.Get_Minor_List();

    //    if (dss != null && dss.Tables[0].Rows.Count > 0)
    //    {
    //        ddlCourseMinor.DataSource = dss;
    //        ddlCourseMinor.DataValueField = dss.Tables[0].Columns[0].ToString();
    //        ddlCourseMinor.DataTextField = dss.Tables[0].Columns[1].ToString();
    //        ddlCourseMinor.DataBind();
    //    }
    //}

    protected void BindCourse(string degreeBranch, string scheme)
    {
        ddlCourses.Items.Clear();

        dss = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID = CDB.COLLEGE_ID) INNER JOIN ACD_SCHEME ACDS ON (ACDS.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COURSE C ON (C.SCHEMENO = ACDS.SCHEMENO)", "C.COURSENO", "C.CCODE + '-' + C.COURSE_NAME, ACDS.SCHEMENO, ACDS.SCHEMENAME, CM.COLLEGE_ID, CM.COLLEGE_NAME, B.BRANCHNO, B.LONGNAME, CDB.COLLEGE_ID AS CDB_COLLEGE_ID, CDB.BRANCHNO AS CDB_BRANCH_NO, CM.COLLEGE_NAME + '-' + B.LONGNAME AS COLLEGE_PROGRAM, CM.OrganizationId AS CM_ORGID, B.OrganizationId AS B_ORGID, CDB.CDBNO", "CDB.CDBNO > 0 AND C.SCHEMENO > 0 AND CDB.CDBNO =" + degreeBranch + "AND C.SCHEMENO =" + scheme, "C.COURSENO");

        //dss = objCommon.Get_Minor_List();

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            ddlCourses.DataSource = dss;
            ddlCourses.DataValueField = dss.Tables[0].Columns[0].ToString();
            ddlCourses.DataTextField = dss.Tables[0].Columns[1].ToString();
            ddlCourses.DataBind();
        }
    }

    protected void ddldegreeBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMinor(ddldegreeBranch.SelectedValue);
        BindScheme(ddldegreeBranch.SelectedValue);
        //pnlCourse.Visible = false;
        //pnlMinor.Visible = false;
    }

    protected void ddlSemesterminor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse(ddldegreeBranch.SelectedValue, ddlSemesterminor.SelectedValue);
        //pnlCourse.Visible = false;
        //pnlMinor.Visible = false;
    }
    #endregion Second Tab "Add Course"
}