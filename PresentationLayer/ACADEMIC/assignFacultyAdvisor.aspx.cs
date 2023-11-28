using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Text.RegularExpressions;
public partial class assignFacultyAdvisor : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objStud = new StudentController();
    Student objStudent = new Student();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Load
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
        try
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
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //Get the Current Session
                    //GetCurrentSession();
                    //Populate the user dropdownlist
                    PopulateDropDownList();
                    //lblCurrentSession.Text = Session["currentsession"].ToString();
                    btnAssignFA0.Enabled = false;
                    btnClassAdvisor.Enabled = false;
                    btnPrint.Enabled = false;
                    //btnReportClass.Enabled = false;
                }
                hdfTot.Value = "0";
                hdnstud.Value = "0";
            }

            //hdnOrg.Value = Session["OrgId"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }

        //Set Report Parameters
        //string parmar = "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Academic" + "," + "rptFaculty_Advisor.rpt&FAC=" + ddlAdvisor.Text + "&BATCH=" + ddlYear.SelectedValue + "&BRANCH=" + ddlBranch.SelectedValue + "&SEM=" + ddlSemester.SelectedValue + "&SECTIONNO=" + ddlSection.SelectedValue + "&FLAG=" + Convert.ToInt16(1).ToString() + "&RB=" + (RB2.Checked == true ? "0" : "1") + "&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_UserName=" + Session["userfullname"].ToString();   
        //string param = "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Academic" + "," + "rptFaculty_Advisor.rpt&sem=" + ddlSemester.SelectedValue + "&DEPT=" + ddlDeptName.SelectedValue + " &FAC=" + ddlAdvisor.Text + " + &param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_UserName=" + Session["userfullname"].ToString();
        //objCommon.ReportPopUp(btnPrint, param, "UAIMS");
    }
    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void GetCurrentSession()
    {
        //try
        //{
        //    DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_CURRENT_SESSION");
        //    //Get the First Rows first column
        //    DataRow dr = ds.Tables[0].Rows[0];

        //    if (dr != null)
        //    {
        //        lblCurrentSession.ToolTip = dr[0].ToString();
        //        lblCurrentSession.Text = dr[1].ToString();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.GetCurrentSession-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }

    private void PopulateDropDownList()
    {
        try
        {
            // Degree
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "0") // for faculty
            {
                // objCommon.FillDropDownList(ddlCollegeName, "ACD_STUDENT A INNER JOIN ACD_COLLEGE_MASTER B ON(A.COLLEGE_ID=B.COLLEGE_ID)", "DISTINCT A.COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "ISNULL(CLS_ADVISOR,0)=" + Session["userno"].ToString() + " AND A.COLLEGE_ID > 0", "A.COLLEGE_ID");
                //objCommon.FillDropDownList(ddlCollegeName, "ACD_STUDENT A INNER JOIN ACD_COLLEGE_MASTER B ON(A.COLLEGE_ID=B.COLLEGE_ID)", "DISTINCT A.COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "CASE " +Session["OrgId"].ToString()+ " WHEN 1 THEN ISNULL(FAC_ADVISOR,0) WHEN 2 THEN ISNULL(CLS_ADVISOR,0) END =" + Session["userno"].ToString() + " AND A.COLLEGE_ID > 0", "A.COLLEGE_ID");

                objCommon.FillDropDownList(ddlCollegeName, "ACD_STUDENT A INNER JOIN ACD_COLLEGE_MASTER B ON(A.COLLEGE_ID=B.COLLEGE_ID) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.COLLEGE_ID=B.COLLEGE_ID)", "DISTINCT A.COLLEGE_ID", "ISNULL(B.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "CASE  WHEN " + Session["OrgId"].ToString() + "=1 THEN ISNULL(FAC_ADVISOR,0) WHEN " + Session["OrgId"].ToString() + "=2 THEN ISNULL(CLS_ADVISOR,0) ELSE ISNULL(FAC_ADVISOR,0) END = " + Session["userno"].ToString() + "or ISNULL(FAC_ADVISOR,0)=0 AND A.COLLEGE_ID > 0 and DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "A.COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "DISTINCT COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            }


            objCommon.FillDropDownList(ddlCollegeNameClass, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "DISTINCT COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "DISTINCT SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region DropDown
    protected void lvFaculty_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            ListView lst = sender as ListView;
            if (lst.ID == "lvFaculty")
            {
                hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
            }
            else
            {
                hdnstud.Value = (Convert.ToInt16(hdnstud.Value) + 1).ToString();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;
            string ddlId = ddl.ID.ToString();
            if (ddlId == "ddlCollegeName")
            {
                if (ddlCollegeName.SelectedIndex > 0)
                {
                    if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "0")// For Faculty.
                    {
                        objCommon.FillDropDownList(ddlDegree, "ACD_STUDENT S INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON (D.DEGREENO=S.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE C WITH (NOLOCK) ON (D.DEGREENO=C.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND CASE  WHEN " + Session["OrgId"].ToString() + "=1 THEN ISNULL(FAC_ADVISOR,0) WHEN " + Session["OrgId"].ToString() + "=2 THEN ISNULL(CLS_ADVISOR,0) ELSE ISNULL(FAC_ADVISOR,0) END = " + Session["userno"].ToString() + "OR ISNULL(FAC_ADVISOR,0)=0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEGREENO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE C WITH (NOLOCK) ON (D.DEGREENO=C.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEGREENO");
                        lvFaculty.DataSource = null;
                        lvFaculty.DataBind();
                    }
                }
                else
                {
                    ddlDegree.Items.Clear();
                    ddlDegree.Items.Add("Please Select");
                    ddlDegree.SelectedItem.Value = "0";

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add("Please Select");
                    ddlBranch.SelectedItem.Value = "0";

                    ddlSemester.Items.Clear();
                    ddlSemester.Items.Add("Please Select");
                    ddlSemester.SelectedItem.Value = "0";

                    //ddlBranch.Items.Clear();
                    //ddlBranch.Items.Add("Please Select");
                    //ddlBranch.SelectedItem.Value = "0";
                  
                }
            }
            else
            {
                lvClassAdv.DataSource = null;
                lvClassAdv.DataBind();
           
                if (ddlCollegeNameClass.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlAssignDept, "ACD_COLLEGE_DEGREE_BRANCH AB WITH (NOLOCK) INNER JOIN ACD_DEPARTMENT AD WITH (NOLOCK) ON(AB.DEPTNO=AD.DEPTNO)", "DISTINCT AD.DEPTNO", "AD.DEPTNAME", "AB.COLLEGE_ID =" + ddlCollegeNameClass.SelectedValue + " AND AD.DEPTNO IN(" + Session["userdeptno"] + ")", "AD.DEPTNAME");
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    ddlDegreeClass.Items.Clear();
                    ddlDegreeClass.Items.Add("Please Select");
                    ddlDegreeClass.SelectedItem.Value = "0";
                    ddlBranchClass.Items.Clear();
                    ddlBranchClass.Items.Add("Please Select");
                    ddlBranchClass.SelectedItem.Value = "0";
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    btnClassAdvisor.Enabled = false;
                }
                else
                {
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    ddlDegreeClass.Items.Clear();
                    ddlDegreeClass.Items.Add("Please Select");
                    ddlDegreeClass.SelectedItem.Value = "0";
                    ddlBranchClass.Items.Clear();
                    ddlBranchClass.Items.Add("Please Select");
                    ddlBranchClass.SelectedItem.Value = "0";
                    ddlAssignDept.Items.Clear();
                    ddlAssignDept.Items.Add("Please Select");
                    ddlAssignDept.SelectedItem.Value = "0";
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    btnClassAdvisor.Enabled = false;

                }
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
            DropDownList ddl = sender as DropDownList;
            string ddlId = ddl.ID.ToString();
            if (ddlId == "ddlDegree")
            {
                if (ddlDegree.SelectedIndex > 0)
                {
                    if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "0") // for faculty
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_STUDENT S INNER JOIN ACD_BRANCH A WITH (NOLOCK) ON (A.BRANCHNO=S.BRANCHNO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND CASE  WHEN " + Session["OrgId"].ToString() + "=1 THEN ISNULL(FAC_ADVISOR,0) WHEN " + Session["OrgId"].ToString() + "=2 THEN ISNULL(CLS_ADVISOR,0) ELSE ISNULL(FAC_ADVISOR,0) END =" + Session["userno"].ToString() + " or ISNULL(FAC_ADVISOR,0)=0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "A.LONGNAME");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "A.LONGNAME");
                        lvFaculty.DataSource = null;
                        lvFaculty.DataBind();
                    }
                }
                else
                {
                    ddlBranch.Items.Clear();
                    ddlDegree.SelectedIndex = 0;
                }
            }
            else
            {
                if (ddlDegreeClass.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlBranchClass, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNameClass.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlDegreeClass.SelectedValue), "A.LONGNAME");
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    //ddlBranchClass.Items.Clear();
                    //ddlBranchClass.Items.Add("Please Select");
                    //ddlBranchClass.SelectedItem.Value = "0";
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    btnClassAdvisor.Enabled = false;
                    ddlSectionClass.SelectedItem.Value = "0";
                }
                else
                {
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    ddlBranchClass.Items.Clear();
                    ddlBranchClass.Items.Add("Please Select");
                    ddlBranchClass.SelectedItem.Value = "0";
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    //ddlBranchClass.Items.Clear();
                    btnClassAdvisor.Enabled = false;
                    //ddlDegreeClass.SelectedIndex = 0;
                }
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
            DropDownList ddl = sender as DropDownList;
            string ddlId = ddl.ID.ToString();
            if (ddlId == "ddlBranch")
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlDeptName, "ACD_COLLEGE_DEGREE_BRANCH AB WITH (NOLOCK) INNER JOIN ACD_DEPARTMENT AD WITH (NOLOCK) ON(AB.DEPTNO=AD.DEPTNO)", "DISTINCT AD.DEPTNO", "AD.DEPTNAME", "", "AD.DEPTNAME");

                    if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "0") // for faculty
                    {

                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "SM.SEMESTERNO > 0 AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND S.DEGREENO =" + ddlDegree.SelectedValue + "AND CASE  WHEN " + Session["OrgId"].ToString() + "=1 THEN ISNULL(FAC_ADVISOR,0) WHEN " + Session["OrgId"].ToString() + "=2 THEN ISNULL(CLS_ADVISOR,0) ELSE ISNULL(FAC_ADVISOR,0) END =" + Session["userno"].ToString() + "or ISNULL(FAC_ADVISOR,0)=0 AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SM.SEMESTERNO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "SM.SEMESTERNO > 0 AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND S.DEGREENO =" + ddlDegree.SelectedValue + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SM.SEMESTERNO");
                        lvFaculty.DataSource = null;
                        lvFaculty.DataBind();
                    }
                }
            }
            else
            {
                if (ddlBranchClass.SelectedIndex > 0)
                {
                    //objCommon.FillDropDownList(ddlDeptNameClass, "ACD_COLLEGE_DEGREE_BRANCH AB WITH (NOLOCK) INNER JOIN ACD_DEPARTMENT AD WITH (NOLOCK) ON(AB.DEPTNO=AD.DEPTNO)", "AD.DEPTNO", "AD.DEPTNAME", "AB.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNameClass.SelectedValue) + "AND AB.DEGREENO=" + Convert.ToInt32(ddlDegreeClass.SelectedValue) + "AND BRANCHNO=" + Convert.ToInt32(ddlBranchClass.SelectedValue), "DEGREENO");

                    objCommon.FillDropDownList(ddlDeptNameClass, "ACD_COLLEGE_DEGREE_BRANCH AB WITH (NOLOCK) INNER JOIN ACD_DEPARTMENT AD WITH (NOLOCK) ON(AB.DEPTNO=AD.DEPTNO)", "DISTINCT AD.DEPTNO", "AD.DEPTNAME", "", "AD.DEPTNAME");

                    objCommon.FillDropDownList(ddlSemesterClass, "ACD_SEMESTER SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "SM.SEMESTERNO > 0 AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNameClass.SelectedValue) + "AND S.DEGREENO =" + ddlDegreeClass.SelectedValue + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranchClass.SelectedValue), "SM.SEMESTERNO");
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    //ddlBranchClass.Items.Clear();
                    //ddlBranchClass.Items.Add("Please Select");
                    //ddlBranchClass.SelectedItem.Value = "0";
                    //ddlSemesterClass.Items.Clear();
                    //ddlSemesterClass.Items.Add("Please Select");
                    //ddlSemesterClass.SelectedItem.Value = "0";
                    //ddlDeptNameClass.Items.Clear();
                    //ddlDeptNameClass.Items.Add("Please Select");
                    //ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    btnClassAdvisor.Enabled = false;

                }
                else
                {
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    btnClassAdvisor.Enabled = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;
            string ddlId = ddl.ID.ToString();
            if (ddlId == "ddlDeptName")
            {
                if (ddlDeptName.SelectedIndex > 0)
                {
                    PopulateFaculty(ddl, ddlAdvisor);
                }
                else
                {
                    ddlAdvisor.DataSource = null;
                    ddlAdvisor.DataBind();
                }
            }
            if (ddlId == "ddlAssignDept")
            {
                if (ddlAssignDept.SelectedIndex > 0)
                {
                    //objCommon.FillDropDownList(ddlDegreeClass, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE C WITH (NOLOCK) ON (D.DEGREENO=C.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeNameClass.SelectedValue + "", "DEGREENO");
                    objCommon.FillDropDownList(ddlDegreeClass, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C WITH (NOLOCK) ON (D.DEGREENO=C.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeNameClass.SelectedValue + "  AND DEPTNO=" + ddlAssignDept.SelectedValue, "D.DEGREENO");
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    //ddlDegreeClass.Items.Clear();
                    //ddlDegreeClass.Items.Add("Please Select");
                    //ddlDegreeClass.SelectedItem.Value = "0";
                    ddlBranchClass.Items.Clear();
                    ddlBranchClass.Items.Add("Please Select");
                    ddlBranchClass.SelectedItem.Value = "0";
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    btnClassAdvisor.Enabled = false;
                }
                else
                {

                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    ddlDegreeClass.Items.Clear();
                    ddlDegreeClass.Items.Add("Please Select");
                    ddlDegreeClass.SelectedItem.Value = "0";
                    ddlBranchClass.Items.Clear();
                    ddlBranchClass.Items.Add("Please Select");
                    ddlBranchClass.SelectedItem.Value = "0";
                    ddlSemesterClass.Items.Clear();
                    ddlSemesterClass.Items.Add("Please Select");
                    ddlSemesterClass.SelectedItem.Value = "0";
                    ddlDeptNameClass.Items.Clear();
                    ddlDeptNameClass.Items.Add("Please Select");
                    ddlDeptNameClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlSectionClass.Items.Clear();
                    ddlSectionClass.Items.Add("Please Select");
                    ddlSectionClass.SelectedItem.Value = "0";
                    ddlAdvisor.DataSource = null;
                    ddlAdvisor.DataBind();
                    btnClassAdvisor.Enabled = false;
                }
            }
            else
            {
                if (ddlDeptNameClass.SelectedIndex > 0)
                {
                    PopulateFaculty(ddl, ddlAdvisorClass);
                    // btnClassAdvisor.Enabled = false;
                    //ddlAdvisorClass.Items.Clear();
                    //ddlAdvisorClass.Items.Add("Please Select");
                    //ddlAdvisorClass.SelectedItem.Value = "0";
                }
                else
                {
                    ddlAdvisorClass.Items.Clear();
                    ddlAdvisorClass.Items.Add("Please Select");
                    ddlAdvisorClass.SelectedItem.Value = "0";
                    ddlAdvisorClass.DataSource = null;
                    ddlAdvisorClass.DataBind();
                    // btnClassAdvisor.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        string ddlId = ddl.ID.ToString();
        if (ddlId == "ddlSemester")
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "0") // for faculty
                {
                    objCommon.FillDropDownList(ddlSectionFA, "ACD_SECTION SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SECTIONNO = S.SECTIONNO)", "DISTINCT SM.SECTIONNO", "SM.SECTIONNAME", " S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND S.DEGREENO =" + ddlDegree.SelectedValue + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND CASE " + Session["OrgId"].ToString() + " WHEN 1 THEN ISNULL(FAC_ADVISOR,0) WHEN 2 THEN ISNULL(CLS_ADVISOR,0) END =" + Session["userno"].ToString() + " AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "SM.SECTIONNO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSectionFA, "ACD_SECTION SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SECTIONNO = S.SECTIONNO)", "DISTINCT SM.SECTIONNO", "SM.SECTIONNAME", " S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND S.DEGREENO =" + ddlDegree.SelectedValue + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "SM.SECTIONNO");
                    lvFaculty.DataSource = null;
                    lvFaculty.DataBind();
                }
            }
            else
            {
                ddlSectionFA.Items.Clear();
                ddlSectionFA.Items.Add("Please Select");
                ddlSectionFA.SelectedItem.Value = "0";
                btnClassAdvisor.Enabled = false;
            }
        }
        else
        {
            if (ddlSemesterClass.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSectionClass, "ACD_SECTION SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SECTIONNO = S.SECTIONNO)", "DISTINCT SM.SECTIONNO", "SM.SECTIONNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNameClass.SelectedValue) + "AND S.DEGREENO =" + ddlDegreeClass.SelectedValue + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranchClass.SelectedValue) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemesterClass.SelectedValue), "SM.SECTIONNO");
                lvClassAdv.DataSource = null;
                lvClassAdv.DataBind();
                btnClassAdvisor.Enabled = false;
            }
            else
            {
                lvClassAdv.DataSource = null;
                lvClassAdv.DataBind();
                ddlSectionClass.Items.Clear();
                ddlSectionClass.Items.Add("Please Select");
                ddlSectionClass.SelectedItem.Value = "0";
                btnClassAdvisor.Enabled = false;
            }
        }


    }
    protected void ddlSectionClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSectionClass.SelectedIndex > 0)
        {
            lvClassAdv.DataSource = null;
            lvClassAdv.DataBind();
            btnClassAdvisor.Enabled = false;
        }
        else
        {
            lvClassAdv.DataSource = null;
            lvClassAdv.DataBind();
            btnClassAdvisor.Enabled = false;
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            lvClassAdv.DataSource = null;
            lvClassAdv.DataBind();
            btnClassAdvisor.Enabled = false;
        }
        else
        {
            lvClassAdv.DataSource = null;
            lvClassAdv.DataBind();
            btnClassAdvisor.Enabled = false;
        }
    }
    #endregion

    #region ShowData
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            if (btn.ID == "btnShow")
            {
                txtTotChk.Text = "0";
                hdfTot.Value = "0";
                //PopulateFaculty(ddlDeptName, ddlAdvisor, lvFaculty); 

            }
            else
            {
                txtTotStud.Text = "0";
                hdnstud.Value = "0";
                //PopulateFaculty(ddlDeptNameClass, ddlAdvisorClass, lvClassAdv);

            }


            //Fill the ListView
            BindListView(btn.ID);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void BindListView(string btnID)
    {
        try
        {
            //int facno = int.Parse(ddlAdvisor.SelectedValue);
            //int deptno = int.Parse((ddlDeptName.SelectedValue != string.Empty) ? ddlDeptName.SelectedValue : "0");
            //int sem = int.Parse(ddlSemester.SelectedValue);
            //int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            //int branch = int.Parse((ddlBranch.SelectedValue != string.Empty) ? ddlBranch.SelectedValue : "0");
            //int batch = int.Parse(ddlYear.SelectedValue);
            //string sem = "'" + ddlSemester.SelectedItem.Text.Trim() + "'";
            //int sectionno = 0;
            //if (ddlSection.SelectedIndex > 0) sectionno = int.Parse(ddlSection.SelectedValue);
            // char rb = '0';
            //if (RB2.Checked == true)
            //    rb = '1';//

            DataSet dsfaculty = null;
            if (btnID == "btnShow" || btnID == "btnAssignFA0")
            {
                dsfaculty = objStud.GetStudentForFacultyAdvisor(0, int.Parse(Session["userno"].ToString()), int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlSemester.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlSectionFA.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));

                if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
                {
                    lvFaculty.DataSource = dsfaculty;
                    lvFaculty.DataBind();

                    btnAssignFA0.Enabled = true;
                    btnPrint.Enabled = true;
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvFaculty);//Set label -  Added By Nikhil L. on 17/01/2021
                }
                else
                {
                    btnAssignFA0.Enabled = false;
                    lvFaculty.DataSource = null;
                    lvFaculty.DataBind();
                    //objCommon.DisplayMessage(updpnl_details, "No Student Found.", this.Page);
                    objCommon.DisplayMessage(this, "No Student Found.", this.Page);
                }

            }
            else
            {
                dsfaculty = objStud.GetStudentForFacultyAdvisor(0, int.Parse(ddlAdvisorClass.SelectedValue), int.Parse(ddlDeptNameClass.SelectedValue), int.Parse(ddlSemesterClass.SelectedValue), Convert.ToInt32(ddlDegreeClass.SelectedValue), Convert.ToInt32(ddlBranchClass.SelectedValue), Convert.ToInt32(ddlCollegeNameClass.SelectedValue), Convert.ToInt32(ddlSectionClass.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));

                if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
                {
                    lvClassAdv.DataSource = dsfaculty;
                    lvClassAdv.DataBind();

                    //GridView1.DataSource = dsfaculty;
                    //GridView1.DataBind();

                    btnClassAdvisor.Enabled = true;
                    btnReportClass.Enabled = true;
                    //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvClassAdv);//Set label -  Added By Nikhil L. on 17/01/2021
                }
                else
                {
                    btnClassAdvisor.Enabled = false;
                    lvClassAdv.DataSource = null;
                    lvClassAdv.DataBind();
                    //objCommon.DisplayMessage(updpnl_details, "No Student Found.", this.Page);
                    objCommon.DisplayMessage(this, "No Student Found.", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnAssignFA0_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            if (btn.ID == "btnAssignFA0")
            {
                if (Convert.ToInt32(ddlAdvisor.SelectedValue) > 0)
                {

                    objStudent.FacAdvisor = Convert.ToInt32(ddlAdvisor.SelectedValue);
                    objStudent.FASectionNo = Convert.ToInt32(ddlSection.SelectedValue);
                    objStudent.SectionNo = Convert.ToInt32(ddlSectionFA.SelectedValue);
                    //foreach (ListViewDataItem lvItem in lvFaculty.Items)
                    //{
                    //    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                    //    if (chkBox.Checked == true)
                    //        objStudent.StudId += chkBox.ToolTip + ",";
                    //}
                    if (hdnStudId.Value != "0")
                    {
                        string result = Regex.Replace(hdnStudId.Value, ",+", ",");
                        objStudent.StudId = result;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Please Select Atlest One From Student List", this.Page);
                        return;
                    }
                    if (objStud.UpdateStudent_FacultAdvisor(objStudent) == Convert.ToInt32(CustomStatus.RecordUpdated))
                    {
                        txtTotChk.Text = string.Empty;
                        hdnStudId.Value = "0";
                        ddlAdvisor.SelectedIndex = 0;
                        BindListView(btn.ID);
                        //objCommon.DisplayMessage(updpnl_details, "Faculty Advisor Alloted Successfully", this.Page);
                        objCommon.DisplayMessage(this, "Faculty Advisor Alloted Successfully", this.Page);
                        //lblStatus2.Text = "Faculty Advisor Alloted Successfully";

                    }
                    else
                        // objCommon.DisplayMessage(updpnl_details, "Error in Alloting Faculty Advisor", this.Page);
                        objCommon.DisplayMessage(this, "Error in Alloting Faculty Advisor", this.Page);
                    //lblStatus2.Text = "Error in Alloting Faculty Advisor";
                }
                else
                {
                    //objCommon.DisplayMessage(this.updpnl_details, "Please Select Faculty Advisor!!", this.Page);
                    objCommon.DisplayMessage(this, "Please Select Faculty Advisor!!", this.Page);
                }
            }
            else
            {
                if (Convert.ToInt32(ddlAdvisorClass.SelectedValue) > 0)
                {
                    objStudent.ClassAdvisor = Convert.ToInt32(ddlAdvisorClass.SelectedValue);
                    objStudent.SectionNo = Convert.ToInt32(ddlSectionClass.SelectedValue);
                    objStudent.ClassDept = Convert.ToInt32(ddlDeptNameClass.SelectedValue);
                    //foreach (ListViewDataItem lvItem in lvClassAdv.Items)
                    //{
                    //    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                    //    if (chkBox.Checked == true)
                    //        objStudent.StudId += chkBox.ToolTip + ",";
                    //}
                    if (hdnStudId.Value != "0")
                    {
                        string result = Regex.Replace(hdnStudId.Value, ",+", ",");
                        objStudent.StudId = result;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Please Select Atlest One From Student List", this.Page);
                        return;
                    }
                    if (objStud.UpdateStudent_FacultAdvisor(objStudent) == Convert.ToInt32(CustomStatus.RecordUpdated))
                    {
                        txtTotStud.Text = string.Empty;
                        hdnStudId.Value = "0";
                        ddlAdvisorClass.SelectedIndex = 0;
                        BindListView(btn.ID);
                        //objCommon.DisplayMessage(updpnl_details, "Faculty Advisor Alloted Successfully", this.Page);
                        objCommon.DisplayMessage(this, "Class Advisor Alloted Successfully", this.Page);
                        //lblStatus2.Text = "Faculty Advisor Alloted Successfully";

                    }
                    else
                        // objCommon.DisplayMessage(updpnl_details, "Error in Alloting Faculty Advisor", this.Page);
                        objCommon.DisplayMessage(this, "Error in Alloting Class Advisor", this.Page);
                    //lblStatus2.Text = "Error in Alloting Faculty Advisor";
                }
                else
                {
                    //objCommon.DisplayMessage(this.updpnl_details, "Please Select Faculty Advisor!!", this.Page);
                    objCommon.DisplayMessage(this, "Please Select Class Advisor!!", this.Page);
                }
            }
            hdnStudId.Value = "0";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            if (btn.ID == "btnCancel")
            {
                ddlCollegeName.SelectedIndex = 0;
                ddlDegree.SelectedIndex = 0;
                ddlBranch.SelectedIndex = 0;
                ddlDeptName.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
                txtTotChk.Text = "0";
                hdfTot.Value = "0";
                lvFaculty.DataSource = null;
                lvFaculty.DataBind();
                ddlSectionFA.SelectedIndex = 0;
                ddlSection.SelectedIndex = 0;
                ddlAdvisor.SelectedIndex = 0;
                ddlAdmBatch.SelectedIndex = 0;
                btnAssignFA0.Enabled = false;
                btnPrint.Enabled = false;
                lvFaculty.DataSource = null;

            }
            else
            {
                
                lvClassAdv.DataSource = null;
                lvClassAdv.DataBind();
                //ddlCollegeNameClass.Items.Clear();
                ddlCollegeNameClass.SelectedIndex = 0;
                //ddlCollegeNameClass.SelectedItem.Value = "0";
                //ddlCollegeNameClass.Items.Add("Please Select");
                ddlDegreeClass.Items.Clear();
                ddlDegreeClass.Items.Add("Please Select");
                ddlDegreeClass.SelectedItem.Value = "0";
                ddlBranchClass.Items.Clear();
                ddlBranchClass.Items.Add("Please Select");
                ddlBranchClass.SelectedItem.Value = "0";
                ddlAssignDept.Items.Clear();
                ddlAssignDept.Items.Add("Please Select");
                ddlAssignDept.SelectedItem.Value = "0";
                ddlSemesterClass.Items.Clear();
                ddlSemesterClass.Items.Add("Please Select");
                ddlSemesterClass.SelectedItem.Value = "0";
                ddlDeptNameClass.Items.Clear();
                ddlDeptNameClass.Items.Add("Please Select");
                ddlDeptNameClass.SelectedItem.Value = "0";
                ddlAdvisorClass.Items.Clear();
                ddlAdvisorClass.Items.Add("Please Select");
                ddlAdvisorClass.SelectedItem.Value = "0";
                ddlSectionClass.Items.Clear();
                ddlSectionClass.Items.Add("Please Select");
                ddlSectionClass.SelectedItem.Value = "0";
                btnClassAdvisor.Enabled = false;
                txtTotStud.Text = "0";
                hdnstud.Value = "0";
                ddlSectionClass.SelectedIndex = 0;
                ddlSection.SelectedIndex = 0;
                ddlAdmBatch.SelectedIndex = 0;
                ddlAdvisorClass.SelectedIndex = 0;
               
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void PopulateFaculty(DropDownList ddl, DropDownList ddlToBind)
    {
        try
        { //DropDownList ddl = sender as DropDownList;
            //string ddlId = ddl.ID.ToString();
            //if (ddlId == "ddlDeptName")
            //{
            //lst.DataSource = null;
            //lst.DataBind();
            //Populating Faculty dropdownlist
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddl.SelectedValue));

            DataTable dtFaculty = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            ddlToBind.Items.Clear();
            ddlToBind.Items.Add(new ListItem("Please Select", "0"));
            ddlToBind.DataSource = dtFaculty;
            ddlToBind.DataTextField = dtFaculty.Columns[1].ToString();
            ddlToBind.DataValueField = dtFaculty.Columns[0].ToString();
            ddlToBind.DataBind();
            //}
            //else
            //{
            //    lvFaculty.DataSource = null;
            //    lvFaculty.DataBind();
            //    //Populating Faculty dropdownlist
            //    SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            //    SqlParameter[] objParams = new SqlParameter[1];
            //    objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));

            //    DataTable dtFaculty = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            //    ddlAdvisor.Items.Clear();
            //    ddlAdvisor.Items.Add(new ListItem("Please Select", "0"));
            //    ddlAdvisor.DataSource = dtFaculty;
            //    ddlAdvisor.DataTextField = dtFaculty.Columns[1].ToString();
            //    ddlAdvisor.DataValueField = dtFaculty.Columns[0].ToString();
            //    ddlAdvisor.DataBind();
            //}
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    #endregion

    #region Report
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name2 = "PKG_STUDENT_SP_RET_STUDENT_FOR_FA_EXCEL";
            string SP_Parameters2 = "@P_COLLEGE_ID";
            string Call_Values2 = "" + Convert.ToInt32(ddlCollegeNameClass.SelectedValue.ToString()) + "";
            DataSet dsfee = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

            if (dsfee.Tables.Count > 0 && dsfee.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=Class_Advisor_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                DataGrid dg = new DataGrid();

                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void ExportExcel(string id, string title)
    {
        try
        {
            DataSet dsfee = null;

            if (id == "btnPrint")
            {
                dsfee = objStud.GetStudentForFacultyAdvisor(int.Parse(ddlAdvisor.SelectedValue), 0, int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlSemester.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlSectionFA.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));
            }
            else
            {
                dsfee = objStud.GetStudentForFacultyAdvisor(0, int.Parse(ddlAdvisorClass.SelectedValue), int.Parse(ddlDeptNameClass.SelectedValue), int.Parse(ddlSemesterClass.SelectedValue), Convert.ToInt32(ddlDegreeClass.SelectedValue), Convert.ToInt32(ddlBranchClass.SelectedValue), Convert.ToInt32(ddlCollegeNameClass.SelectedValue), Convert.ToInt32(ddlSectionClass.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue));
            }

            if (dsfee.Tables.Count > 0 && dsfee.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + title + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                DataGrid dg = new DataGrid();

                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SEM=" + ddlSemester.SelectedValue + ",@P_FAC=" + ddlAdvisor.SelectedValue + ",@P_DEPTNO=" + ddlDeptName.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_USERNAME=" + Session["username"] + ",@P_COLLEGE_ID=" + ddlCollegeName.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            // ScriptManager.RegisterClientScriptBlock(this.updpnl_details, this.updpnl_details.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SP_Name2 = "PKG_GET_FACULTY_ADVISOR_ALLOTMENT_REPORT";
            string SP_Parameters2 = "@P_DUMMY";
            string Call_Values2 = "" + 0 + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            DataGrid dg = new DataGrid();
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename= faculty_advisior_allotment_overall_report.xls";

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dg.DataSource = dsStudList.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion



}