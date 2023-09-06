//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : Academic
// PAGE NAME     : COPY COURSE SCHEME
// ADDED BY      : Mr. Manish Walde
// ADDED DATE    : 17-NOV-2011
// MODIFIED DATE : 17-NOV-2014 
// MODIFIED BY   : Mr.Manish Walde
// MODIFIED DESC : 
//======================================================================================
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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_CopyCoursetoScheme : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SlotMaster objSM = new SlotMaster();
    SlotController objSC = new SlotController();

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();
                //PopulateCopyDropdownlist ();
                //BindListLiew();
                ViewState["action"] = "add";
                btnCopyCourse.Enabled = false;
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddldegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListLiewByCondition()
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) INNER JOIN ACD_SEMESTER Sem ON (C.SEMESTERNO = Sem.SEMESTERNO)", "COURSENO", "SHORTNAME, COURSE_NAME, C.SCHEMENO, S.SCHEMENAME, S.BRANCHNO, CCODE, C.SEMESTERNO, sem.SEMESTERNAME", "C.SCHEMENO = " + Convert.ToInt32(ddlSchemeOld.SelectedValue) + "AND C.SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue), "CCODE");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTimeTable.DataSource = ds;
            lvTimeTable.DataBind();
            //btnShowCopy.Enabled = false;
            btnCopyCourse.Enabled = false;


            //objCommon.FillDropDownList (ddlSchemeNew, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString () + " AND BRANCHNO = " + ddlBranch.SelectedValue.ToString () + "AND SCHEMENO != "+ddlSchemeOld.SelectedValue, "SCHEMENAME" );
            objCommon.FillDropDownList(ddlSchemeNew, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString() + "AND SCHEMENO != " + ddlSchemeOld.SelectedValue, "SCHEMENAME");


            objCommon.FillDropDownList(ddlSchemeNew, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString() + " AND BRANCHNO = " + ddlBranch.SelectedValue.ToString() + "AND SCHEMENO != " + ddlSchemeOld.SelectedValue, "SCHEMENAME");
            objCommon.FillDropDownList(ddlSchemeNew, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString() + "AND SCHEMENO != " + ddlSchemeOld.SelectedValue, "SCHEMENAME");
            //objCommon.FillDropDownList (ddlSchemeNew, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString () + " AND BRANCHNO = " + ddlBranch.SelectedValue.ToString () + "AND SCHEMENO != "+ddlSchemeOld.SelectedValue, "SCHEMENAME" );
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvTimeTable);//Set label - 
        }
        else
        {
            //lvTimeTable.DataSource = null;
            //lvTimeTable.DataBind ();
            objCommon.DisplayMessage(updCopyCourse, "Record Not Found", this.Page);
        }
    }

    private void CheckforNewSchemeandBind()
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) INNER JOIN ACD_SEMESTER Sem ON (C.SEMESTERNO = Sem.SEMESTERNO)", "COURSENO", "SHORTNAME, COURSE_NAME, C.SCHEMENO, S.SCHEMENAME, S.BRANCHNO, CCODE, C.SEMESTERNO, sem.SEMESTERNAME", "C.SCHEMENO = " + Convert.ToInt32(ddlSchemeNew.SelectedValue) + "AND C.SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue), "CCODE");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTimeTable.DataSource = ds;
            lvTimeTable.DataBind();
            btnCopyCourse.Enabled = false;
            objCommon.DisplayMessage(updCopyCourse, "Course already exists for selected new scheme", this.Page);
        }
        else
        {
            btnCopyCourse.Enabled = true;
            //lvTimeTable.DataSource = null;
            //lvTimeTable.DataBind();
            //objCommon.DisplayMessage("Record Not Found", this.Page);
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            BindListLiewByCondition();
            ddlSchemeNew.SelectedIndex = 0;
        }
    }

    protected void btnCopyCourse_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddldegree.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updCopyCourse, "Please Select the Degree", this.Page);
            }
            else if (ddlBranch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updCopyCourse, "Please Select the Branch", this.Page);
            }
            else if (ddlSchemeOld.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updCopyCourse, "Please Select the Old Scheme", this.Page);
            }
            else if (ddlSemester.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updCopyCourse, "Please Select the Semester", this.Page);
            }
            else if (ddlSchemeNew.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updCopyCourse, "Please Select the New Scheme", this.Page);
            }
            else
            {
                CourseController objCourse = new CourseController();

                int ret = 0;

                if (lvTimeTable.Items.Count > 0)
                {
                    string ids = GetCourseIDs();
                    if (!string.IsNullOrEmpty(ids))
                    {
                        foreach (ListViewDataItem i in lvTimeTable.Items)
                        {
                            if ((i.FindControl("chkSelect") as CheckBox).Checked)
                            {
                                int CourseNo = Convert.ToInt32(((HiddenField)i.FindControl("hdncourseno")).Value);
                                int FromSchemeNo = Convert.ToInt32(ddlSchemeOld.SelectedValue);
                                int ToSchemeNo = Convert.ToInt32(ddlSchemeNew.SelectedValue);
                                int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

                                ret = objCourse.CopyCourseToNewScheme(CourseNo, FromSchemeNo, ToSchemeNo, SemesterNo);
                            }
                        }

                        if (ret >= 0)
                        {
                            objCommon.DisplayMessage(updCopyCourse, "Selected Course Successfully Copied to new scheme", this.Page);

                            DataSet ds = new DataSet();
                            ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) INNER JOIN ACD_SEMESTER Sem ON (C.SEMESTERNO = Sem.SEMESTERNO)", "COURSENO", "SHORTNAME, COURSE_NAME, C.SCHEMENO, S.SCHEMENAME, S.BRANCHNO, CCODE, C.SEMESTERNO, sem.SEMESTERNAME", "C.SCHEMENO = " + Convert.ToInt32(ddlSchemeNew.SelectedValue) + "AND C.SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue), "CCODE");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lvTimeTable.DataSource = ds;
                                lvTimeTable.DataBind();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCopyCourse, "Failed to Save Record...", this.Page);
                        }
                        //CheckforNewSchemeandBind ();
                        btnCopyCourse.Enabled = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCopyCourse, "Please Select atleast single record to copy...", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCopyCourse, "No Record for copying.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetCourseIDs()
    {
        string courseIds = string.Empty;
        try
        {
            foreach (ListViewDataItem  item in lvTimeTable.Items)
            {
                if ((item.FindControl("chkSelect") as CheckBox).Checked)
                {
                    if (courseIds.Length > 0)
                        courseIds += ".";
                    courseIds += (item.FindControl("hdncourseno") as HiddenField).Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return courseIds;
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON (B.BRANCHNO = C.BRANCHNO)", "DISTINCT (B.BRANCHNO)", "LONGNAME", "B.BRANCHNO > 0 AND C.DEGREENO = " + ddldegree.SelectedValue, "LONGNAME");

            //if ( Convert.ToInt32 ( Session["usertype"].ToString () ) == 1 || Convert.ToInt32 ( Session["usertype"].ToString () ) == 4 )
            //{
            //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString(), "LONGNAME");
            //    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //}
            //else if ( Convert.ToInt32 ( Session["usertype"].ToString () ) == 3 )
            //{
            //    string deptNo = objCommon.LookUp ( "USER_ACC", "UA_DEPTNO", "UA_NO = " + Session["userno"] );

            //    objCommon.FillDropDownList ( ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString () + "AND DEPTNO = " + deptNo, "LONGNAME" );
            //    objCommon.FillDropDownList ( ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO" );
            //}
            //else
            //{
            //    ddlBranch.Items.Clear();
            //    ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            //    ddlSemester.Items.Clear();
            //    ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            //}
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        ddlBranch.Focus();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSchemeOld, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO = " + ddldegree.SelectedValue.ToString() + " AND BRANCHNO = " + ddlBranch.SelectedValue.ToString(), "SCHEMENAME");
            //objCommon.FillDropDownList ( ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO" );
            ddlSchemeOld.Focus();
        }
        else
        {
            ddlSchemeOld.Items.Clear();
            ddlSchemeOld.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
        }
    }

    protected void ddlSchemeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchemeNew.SelectedIndex > 0)
        {
            CheckforNewSchemeandBind();
        }
    }
    protected void ddlSchemeOld_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_COURSE C INNER JOIN ACD_SEMESTER S ON (C.SEMESTERNO = S.SEMESTERNO)", "DISTINCT C.SEMESTERNO", "SEMESTERNAME", "C.SEMESTERNO > 0 AND C.SCHEMENO = " + ddlSchemeOld.SelectedValue.ToString(), "C.SEMESTERNO");
        //objCommon.FillDropDownList ( ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO" );
        ddlSemester.SelectedIndex = 0;
        ddlSchemeNew.SelectedIndex = 0;
        ddlSemester.Focus();
    }
}
