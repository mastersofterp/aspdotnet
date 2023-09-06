//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE Lock-Unlock                                                 
// CREATION DATE : 27-JUL-2023
// CREATED BY    : GOPAL MANDAOGADE                               
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using System.Data.OleDb;

public partial class Administration_CourseLockUnlock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    Course objC = new Course();

    //ConnectionStrings
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                string College_code = objCommon.LookUp("REFF", "College_code", "OrganizationId = '" + Session["OrgId"].ToString() + "'");
                ViewState["college_id"] = College_code;

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               
                string UserNos = objCommon.LookUp("ACD_MODULE_CONFIG", "USERS_COURSE_LOCK_UNLOCK", Session["userno"].ToString() + "IN (select value from dbo.split(USERS_COURSE_LOCK_UNLOCK,','))");
                if (UserNos == "")
                {
                    objCommon.DisplayMessage(this.Page, "You are not authorized to view this page. Please Contact to Admin.", this.Page);
                    divMain.Visible = false;
                    return;
                }

                //Populate the DropDownList 
                PopulateDropDown();

                //ViewState["action"] = "add";

            }
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipaddress"] = Request.ServerVariables["REMOTE_ADDR"];
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
        }
    }

    #endregion

    #region Dropdown List Events
    // bind department on degree selection
    protected void ddlDegree_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), string.Empty);
               
                //if (Session["usertype"].ToString() != "1")
                //    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "D.DEPTNAME");
                //else
                //    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "D.DEPTNAME");
            
            }
            else
            {
               // ddlDept.SelectedIndex = 0;
                //lvCourse.DataSource = null;
                //lvCourse.DataBind();
            }
            pnlPreCorList.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    // bind Scheme
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(ddlSemester, @"ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO) ", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME","S.SEMESTERNO > 0 AND C.SCHEMENO = " + ddlScheme.SelectedValue, "S.SEMESTERNO ASC");
        // objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT max(DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ")", "SEMESTERNO");
        pnlPreCorList.Visible = false;
        lvCourse.Visible = false;
    }

    //Load data on page Load
    private void PopulateDropDown()
    {
        try
        {
            //fill degree name
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "CD.DEGREENO=D.DEGREENO AND D.DEGREENO>0  AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                //if (dec == "0")
                //{
                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEPTNO=" + deptno,"");
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_BRANCH B ON D.DEGREENO=B.DEGREENO ", "distinct(D.DEGREENO)", "DEGREENAME", "DEPTNO=" + deptno, "DEGREENO");
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "D.DEGREENO");
                //}
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
            }
            pnlPreCorList.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void BindCourseList(int degreeno, int schemeno, string semesterno)
    {
        DataSet ds = null;
        try
        {
            CourseController objCC = new CourseController();
            ds = objCC.GetCourseLockUnlock_Details(degreeno, schemeno, semesterno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                hftot.Value = ds.Tables[0].Rows.Count.ToString();
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label 
                Session["CheckExistsDB"] = ds.Tables[0];
                Session["CheckExistsLUDB"] = ds.Tables[1];
                Session["TempCourseDB"] = null;
            }
            else
            {

                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(this.UPDCOURSE, "Course Record Not found!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.BindCourseList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   //for course check lock unlock
    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = e.Item as ListViewDataItem;

        Label lblStatus = item.FindControl("lblSTATUS") as Label;
        if (lblStatus.Text == "True")
        {
            lblStatus.Text = "Lock";
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblStatus.Text = "UnLock";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }
    }
    #endregion

    #region Course Show Data and Clear Events

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedValue != "" && ddlDegree.SelectedValue != "0")
            {
                if (ddlScheme.SelectedValue != "" && ddlScheme.SelectedValue != "0")
                {
                    if (ddlSemester.SelectedValue != "" && ddlSemester.SelectedValue != "0")
                    {
                        var semesternos = string.Empty;//ddlSemester.SelectedValue;
                        pnlPreCorList.Visible = true;
                        lvCourse.Visible = true;

                        foreach (ListItem items in ddlSemester.Items)
                        {
                            if (items.Selected == true)
                                semesternos += (items.Value).Split('-')[0] + ',';
                        }
                        semesternos = semesternos.TrimEnd(',');


                        BindCourseList(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), semesternos);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Semester!", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Scheme!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Degree!", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseLockUnlock.btnShowData_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();     
    }

    protected void Clear()
    {
        ddlDegree.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = -1;        
        lblMsg.Text = string.Empty;
        lblStatus.Text = string.Empty;

        DataSet ds = null;
        lvCourse.DataSource = ds;
        lvCourse.DataBind();
        pnlPreCorList.Visible = false;
        lvCourse.Visible = false;
    }
    #endregion

    #region Course Lock & Unlock Event
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            CoursesLockUnlock ObjCLU = new CoursesLockUnlock();
            CustomStatus cs = new CustomStatus();
            int chkCount = 0;
            Session["TempCourseDB"] = null;
            var tot = hfTotCourse.Value;

            CheckBox ctrHeader = (CheckBox)lvCourse.FindControl("cbHead");

            foreach (ListViewDataItem item in lvCourse.Items)
            {
                
                CheckBox chkSub = item.FindControl("cbRow") as CheckBox;
                if (chkSub.Checked == true)
                {
                    chkCount++;
                    Label lblCNO = item.FindControl("lblCNO") as Label;
                    Label lblCCode = item.FindControl("lblCCode") as Label;
                    Label lblCOURSE_NAME = item.FindControl("lblCOURSE_NAME") as Label;
                    Label lblSUBID = item.FindControl("lblSUBID") as Label;
                    Label lblSUBNAME = item.FindControl("lblSUBNAME") as Label;
                    Label lblSEMESTERNO = item.FindControl("lblSEMESTERNO") as Label;
                    Label lblSEMESTERNAME = item.FindControl("lblSEMESTERNAME") as Label;
                    Label lblSTATUS = item.FindControl("lblSTATUS") as Label;

                    ObjCLU = new CoursesLockUnlock();

                    ObjCLU.COURSENO = Convert.ToInt32(lblCNO.Text);
                    ObjCLU.DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
                    ObjCLU.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
                    ObjCLU.SEMESTERNO = Convert.ToInt32(lblSEMESTERNO.Text);
                    ObjCLU.CCode = lblCCode.Text;
                    ObjCLU.COURSE_NAME = lblCOURSE_NAME.Text;
                    ObjCLU.COURSE_TYPE = Convert.ToInt32(lblSUBID.Text);
                    ObjCLU.STATUS = true;
                    ObjCLU.COLLEGE_CODE = Session["colcode"].ToString();
                    ObjCLU.CREATED_BY = Convert.ToInt32(Session["userno"]);
                    
                    TempCourseDB(ObjCLU);
                   
                }
               
            }
            if (chkCount == 0)
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Atleat One Course!!", this.Page);
                return;
            }
            else 
            {
                CheckExistsCourseLOCK();
                ctrHeader.Checked = false;
                hfTotCourse.Value = "0";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseLockUnlock.btnLock_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable!!");
        }
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {            
            CoursesLockUnlock ObjCLU = new CoursesLockUnlock();
            CustomStatus cs = new CustomStatus();
            int chkCount = 0;
            Session["TempCourseDB"] = null;
           
            CheckBox ctrHeader = (CheckBox)lvCourse.FindControl("cbHead");

            foreach (ListViewDataItem item in lvCourse.Items)
            {
                CheckBox chkSub = item.FindControl("cbRow") as CheckBox;
                if (chkSub.Checked == true)
                {
                    chkCount++;
                    Label lblCNO = item.FindControl("lblCNO") as Label;
                    Label lblCCode = item.FindControl("lblCCode") as Label;
                    Label lblCOURSE_NAME = item.FindControl("lblCOURSE_NAME") as Label;
                    Label lblSUBID = item.FindControl("lblSUBID") as Label;
                    Label lblSUBNAME = item.FindControl("lblSUBNAME") as Label;
                    Label lblSEMESTERNO = item.FindControl("lblSEMESTERNO") as Label;
                    Label lblSEMESTERNAME = item.FindControl("lblSEMESTERNAME") as Label;
                    Label lblSTATUS = item.FindControl("lblSTATUS") as Label;

                    ObjCLU = new CoursesLockUnlock();

                    ObjCLU.COURSENO = Convert.ToInt32(lblCNO.Text);
                    ObjCLU.DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
                    ObjCLU.SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
                    ObjCLU.SEMESTERNO = Convert.ToInt32(lblSEMESTERNO.Text);
                    ObjCLU.CCode = lblCCode.Text;
                    ObjCLU.COURSE_NAME = lblCOURSE_NAME.Text;
                    ObjCLU.COURSE_TYPE = Convert.ToInt32(lblSUBID.Text);
                    ObjCLU.STATUS = false;
                    ObjCLU.COLLEGE_CODE = Session["colcode"].ToString();
                    ObjCLU.MODIFIED_BY = Convert.ToInt32(Session["userno"]);

                    //TempCourseDB(ObjCLU);
                    string Mode = "UPDATE";

                    cs = (CustomStatus)objCC.UpdateCourseLockUnlock(ObjCLU, Mode);
                }

            }
            if (chkCount == 0)
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Atleat One Course!!", this.Page);
                return;
            }

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
               
                CLockUnlockbind();
                ctrHeader.Checked = false;
                hfTotCourse.Value = "0";
                objCommon.DisplayMessage(this.UPDCOURSE, "Course UnLocked Successfully!!", this.Page);             
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Please Try Again!!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseLockUnlock.btnLock_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable!!");
        }
    }
 
    protected void CLockUnlockbind()
    {
        string semesternos = string.Empty;
        foreach (ListItem items in ddlSemester.Items)
        {
            if (items.Selected == true)
                semesternos += (items.Value).Split('-')[0] + ',';
        }
        semesternos = semesternos.TrimEnd(',');
        BindCourseList(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), semesternos);
    }

    protected void CheckExistsCourseLOCK() 
    {
        try
        {
            // DataTable dt = new DataTable();
            DataTable dt1 = (DataTable)Session["CheckExistsDB"];
            DataTable dt2 = (DataTable)Session["TempCourseDB"];
            DataTable dt3 = (DataTable)Session["CheckExistsLUDB"];

            DataTable fnlist = new DataTable();
            var mode = string.Empty;
          
            if (dt1.Rows.Count > 0)
            {
                var list1 = (from r1 in dt1.AsEnumerable() select r1).ToList();
                var list2 = (from r2 in dt2.AsEnumerable() select r2).ToList();
                var list3 = (from r3 in dt3.AsEnumerable() select r3).ToList();

                var flist = list1.Where(item1 => list2.Any(item2 =>
                                         item1.Field<string>("CCode").Trim().ToLower().ToString() == item2.Field<string>("CCode").Trim().ToLower().ToString()
                                         && item1.Field<int>("COURSENO") == item2.Field<int>("COURSENO")
                                         && item1.Field<int>("DEGREENO") == item2.Field<int>("DEGREENO")
                                         && item1.Field<int>("SCHEMENO") == item2.Field<int>("SCHEMENO")
                                         && item1.Field<int>("SEMESTERNO") == item2.Field<int>("SEMESTERNO")
                                         && item1.Field<int>("SUBID") == item2.Field<int>("COURSE_TYPE")
                                         && item1.Field<bool>("STATUS") == true
                                     )).ToList();


                //if (list3.Count() > 0)
                //{
                //    var chkExistslist = list2.Where(item2 => list3.Any(item3 =>
                //                            item2.Field<string>("CCode").Trim().ToLower().ToString() == item3.Field<string>("CCode").Trim().ToLower().ToString()
                //                            && item2.Field<int>("COURSENO") == item3.Field<int>("COURSENO")
                //                            && item2.Field<int>("DEGREENO") == item3.Field<int>("DEGREENO")
                //                            && item2.Field<int>("SCHEMENO") == item3.Field<int>("SCHEMENO")
                //                            && item2.Field<int>("SEMESTERNO") == item3.Field<int>("SEMESTERNO")
                //                        )).ToList();
                //}

                //if (list3.Count() > 0)
                //{
                //    mode = "UPDATE";
                //    if (flist.Any())
                //        fnlist = flist.CopyToDataTable();
                //    else
                //        fnlist = dt1.Clone();
                //}
                //else
                //{
                    if (flist.Any())
                        fnlist = flist.CopyToDataTable();
                    else
                        fnlist = dt1.Clone();
                    mode = "INSERT";
                //}

                if (fnlist.Rows.Count == 0)
                {
                    CourseLockUnlockExists(mode);
                    Session["TempCourseDB"] = null;
                }
                else
                {
                    objCommon.DisplayMessage(this.UPDCOURSE, "Course Already Locked!!", this.Page);
                    return;
                }
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseLockUnlock.CheckExistsCourseLOCK -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable!!");
        }
    
    }

    protected void TempCourseDB(CoursesLockUnlock ObjCLU) 
    {
        try
        {
            DataTable dt = new DataTable();

            if (Session["TempCourseDB"] == null)
            {
                dt.Columns.AddRange(new DataColumn[12] {
                new DataColumn("ID", typeof(int)),
                new DataColumn("COURSENO", typeof(int)),
                new DataColumn("DEGREENO", typeof(int)),
                new DataColumn("SCHEMENO", typeof(int)),
                new DataColumn("SEMESTERNO", typeof(int)),
                new DataColumn("CCode", typeof(string)),
                new DataColumn("COURSE_NAME", typeof(string)),
                new DataColumn("COURSE_TYPE", typeof(int)),
                new DataColumn("STATUS", typeof(bool)),
                new DataColumn("COLLEGE_CODE", typeof(string)),
                new DataColumn("CREATED_BY", typeof(int)),   
                new DataColumn("MODIFIED_BY", typeof(int)),
                });

                dt.Rows.Add(1, ObjCLU.COURSENO, ObjCLU.DEGREENO, ObjCLU.SCHEMENO, ObjCLU.SEMESTERNO, ObjCLU.CCode, ObjCLU.COURSE_NAME, ObjCLU.COURSE_TYPE, ObjCLU.STATUS, ObjCLU.COLLEGE_CODE, ObjCLU.CREATED_BY,ObjCLU.MODIFIED_BY);
                Session["TempCourseDB"] = dt;
            }
            else
            {
                dt = (DataTable)Session["TempCourseDB"];
                int rno = dt.Rows.Count;
                dt.Rows.Add(rno + 1, ObjCLU.COURSENO, ObjCLU.DEGREENO, ObjCLU.SCHEMENO, ObjCLU.SEMESTERNO, ObjCLU.CCode, ObjCLU.COURSE_NAME, ObjCLU.COURSE_TYPE, ObjCLU.STATUS, ObjCLU.COLLEGE_CODE, ObjCLU.CREATED_BY, ObjCLU.MODIFIED_BY);
                Session["TempCourseDB"] = dt;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseLockUnlock.TempCourseDB -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable!!");
        }
    }

    protected void CourseLockUnlockExists(string mode) 
    {
        try
        {
            CoursesLockUnlock ObjCLU = new CoursesLockUnlock();
            CustomStatus cs = new CustomStatus();
            DataTable dt = new DataTable();

            if (Session["TempCourseDB"] != null)
            {
                dt = (DataTable)Session["TempCourseDB"];

                foreach (DataRow r in dt.Rows)
                {
                    ObjCLU = new CoursesLockUnlock();

                    ObjCLU.COURSENO = Convert.ToInt32(r["COURSENO"]);
                    ObjCLU.DEGREENO = Convert.ToInt32(r["DEGREENO"]);
                    ObjCLU.SCHEMENO = Convert.ToInt32(r["SCHEMENO"]);
                    ObjCLU.SEMESTERNO = Convert.ToInt32(r["SEMESTERNO"]);
                    ObjCLU.CCode = r["COURSENO"].ToString();
                    ObjCLU.COURSE_NAME = r["COURSE_NAME"].ToString();
                    ObjCLU.COURSE_TYPE = Convert.ToInt32(r["COURSE_TYPE"]);
                    ObjCLU.STATUS = true;
                    ObjCLU.COLLEGE_CODE = r["COLLEGE_CODE"].ToString();
                   
                    if (mode == "INSERT")
                    {
                        ObjCLU.CREATED_BY = Convert.ToInt32(r["CREATED_BY"]);
                        cs = (CustomStatus)objCC.InsertCourseLockUnlock(ObjCLU, mode);
                    }
                    else
                    {
                        ObjCLU.MODIFIED_BY = Convert.ToInt32(r["CREATED_BY"]);  // update
                        cs = (CustomStatus)objCC.UpdateCourseLockUnlock(ObjCLU, mode);
                    }
                   
                }

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    CLockUnlockbind();
                    Session["TempCourseDB"] = null;
                    objCommon.DisplayMessage(this.UPDCOURSE, "Course Locked Successfully!!", this.Page);                  
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    CLockUnlockbind();
                    Session["TempCourseDB"] = null;
                    objCommon.DisplayMessage(this.UPDCOURSE, "Course Locked Successfully!!", this.Page);
                }
                else
                {
                    CLockUnlockbind();
                    objCommon.DisplayMessage(this.UPDCOURSE, "Please Try Again!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseLockUnlock.CourseLockUnlockExists -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable!!");
        }

    }
    #endregion

    #region Export Excle file
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = objCC.RetrieveCourseMasterDataForExcel();

        ds.Tables[0].TableName = "Course Data Format";
        ds.Tables[1].TableName = "Subject Master";
        ds.Tables[2].TableName = "Semester Master";
        ds.Tables[3].TableName = "Scheme Master";
        ds.Tables[4].TableName = "BOS_Department Master";
        ds.Tables[5].TableName = "Elective Group";

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadCourseData.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Define All Masters!!", this.Page);
        }
    }

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        int drawing = 0;
        CourseController objCC = new CourseController();
        Course objC = new Course();
        try
        {
            CustomStatus cs = new CustomStatus();
            string conStr = "";

            switch (Extension)
            {
                //case ".xls": //Excel 97-03
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            //Bind Excel to GridView
            DataSet ds = new DataSet();
            oda.Fill(ds);

            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "isnull(COURSENAME,'')<>''";
            DataTable dtNew = dv1.ToTable();

            //lvStudData.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            //lvStudData.DataBind();
            int i = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //foreach (DataRow dr in dt.Rows)
            {

                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Name = row[0];
                if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
                {
                    //string city = string.Empty;
                    //string district = string.Empty;
                    //string state = string.Empty;
                    //string leadSource = string.Empty;
                    //string leadCollectedby = string.Empty;


                    if (!(dtNew.Rows[i]["COURSENAME"]).ToString().Equals(string.Empty))
                    {
                        objC.CourseName = (dtNew.Rows[i]["COURSENAME"]).ToString();
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnlImportData, "Please enter Course Name at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (!(dtNew.Rows[i]["SHORTNAME"]).ToString().Equals(string.Empty))
                    {
                        objC.CourseShortName = (dtNew.Rows[i]["SHORTNAME"]).ToString();
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnlImportData, "Please enter CourseShortName at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (!(dtNew.Rows[i]["COURSECODE"]).ToString().Equals(string.Empty))
                    {
                        objC.CCode = (dtNew.Rows[i]["COURSECODE"]).ToString();
                    }
                    else
                    {
                        // objCommon.DisplayMessage(updpnlImportData, "Please enter Course Code at Row no. " + (i + 1), this.Page);
                        return;
                    }


                    if (!(dtNew.Rows[i]["CREDITS"]).ToString().Equals(string.Empty))
                    {
                        objC.Credits = Convert.ToDecimal((dtNew.Rows[i]["CREDITS"]).ToString());
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpnlImportData, "Please enter Credits at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (dtNew.Rows[i]["ISELECTIVE"].Equals("YES") || dtNew.Rows[i]["ISELECTIVE"].Equals("Yes") || dtNew.Rows[i]["ISELECTIVE"].Equals("yes"))
                    {
                        objC.Elect = 1;
                        if (dtNew.Rows[i]["ELECTIVEGROUP"].ToString().Equals(string.Empty))
                        {
                            // objCommon.DisplayMessage(updpnlImportData, "Please enter Elective Group at Row no. " + (i + 1), this.Page);
                            return;
                        }
                    }
                    else if (dtNew.Rows[i]["ISELECTIVE"].Equals("NO") || dtNew.Rows[i]["ISELECTIVE"].Equals("No") || dtNew.Rows[i]["ISELECTIVE"].Equals("no") || dtNew.Rows[i]["ISELECTIVE"].Equals("nO"))
                    {
                        objC.Elect = 0;
                    }

                    //else
                    //{
                    //    objCommon.DisplayMessage(updpnlImportData, "Please enter Elective at Row no. " + (i + 1), this.Page);
                    //    return;
                    //}



                    if (dtNew.Rows[i]["SUBJECTTYPE"].ToString() == string.Empty)
                    {
                        // objCommon.DisplayMessage(updpnlImportData, "Please Enter Subject Type at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }
                    else
                    {
                        string subid = objCommon.LookUp("ACD_SUBJECTTYPE", "COUNT(1)", "SUBNAME='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'");

                        if (Convert.ToInt32(subid) > 0)
                        {
                            objC.SubID = (objCommon.LookUp("ACD_SUBJECTTYPE", "SUBID", "SUBNAME ='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "SUBID", "SUBNAME ='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'"));
                        }
                        else
                        {
                            // objCommon.DisplayMessage(updpnlImportData, "Subject Type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }

                    if (dtNew.Rows[i]["SEMESTER"].ToString() == string.Empty)
                    {
                        // objCommon.DisplayMessage(updpnlImportData, "Please Enter Semester at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string semesterno = objCommon.LookUp("acd_semester", "COUNT(1)", "SEMESTERNAME='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'");

                        if (Convert.ToInt32(semesterno) > 0)
                        {
                            objC.SemesterNo = Convert.ToInt32((objCommon.LookUp("acd_semester", "SemesterNo", "SEMESTERNAME ='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("acd_semester", "SemesterNo", "SEMESTERNAME ='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'")));
                        }
                        else
                        {
                            //  objCommon.DisplayMessage(updpnlImportData, "Semester not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                    }
                    if (dtNew.Rows[i]["SCHEME"].ToString() == string.Empty)
                    {
                        //objCommon.DisplayMessage(updpnlImportData, "Please Enter Scheme at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string schemeno = objCommon.LookUp("acd_scheme", "COUNT(1)", "SCHEMENAME='" + dtNew.Rows[i]["SCHEME"].ToString() + "'");

                        if (Convert.ToInt32(schemeno) > 0)
                        {
                            objC.SchemeNo = Convert.ToInt32((objCommon.LookUp("acd_scheme", "SchemeNo", "SCHEMENAME ='" + dtNew.Rows[i]["SCHEME"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("acd_scheme", "SchemeNo", "SCHEMENAME ='" + dtNew.Rows[i]["SCHEME"].ToString() + "'")));

                        }
                        else
                        {
                            // objCommon.DisplayMessage(updpnlImportData, "Scheme not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }

                    if (dtNew.Rows[i]["BOS_DEPT"].ToString() == string.Empty)
                    {
                        //  objCommon.DisplayMessage(updpnlImportData, "Please Enter BOS_DEPT at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string deptno = objCommon.LookUp("ACD_DEPARTMENT", "COUNT(1)", "DEPTNAME='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'");

                        if (Convert.ToInt32(deptno) > 0)
                        {
                            objC.Deptno = Convert.ToInt32((objCommon.LookUp("ACD_DEPARTMENT", "Deptno", "DEPTNAME ='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "Deptno", "DEPTNAME ='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'")));

                        }
                        else
                        {
                            //  objCommon.DisplayMessage(updpnlImportData, "BOS_DEPT not found in ERP Master at Row no. " + (i + 1), this.Page);

                            return;
                        }

                    }

                    if (dtNew.Rows[i]["ISELECTIVE"].Equals("YES") || dtNew.Rows[i]["ISELECTIVE"].Equals("Yes") || dtNew.Rows[i]["ISELECTIVE"].Equals("yes"))
                    {
                        string electivegrpno = objCommon.LookUp("ACD_ELECTGROUP", "COUNT(1)", "GROUPNAME='" + dtNew.Rows[i]["ELECTIVEGROUP"].ToString() + "'");

                        if (Convert.ToInt32(electivegrpno) > 0)
                        {
                            objC.Electivegrpno = Convert.ToInt32((objCommon.LookUp("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME ='" + dtNew.Rows[i]["ELECTIVEGROUP"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME ='" + dtNew.Rows[i]["ELECTIVEGROUP"].ToString() + "'")));
                        }
                        else
                        {
                            // objCommon.DisplayMessage(updpnlImportData, "ELECTIVEGROUP not found in ERP Master at Row no. " + (i + 1), this.Page);

                            return;
                        }
                    }


                    if (!(dtNew.Rows[i]["MINMARK_I"]).ToString().Equals(string.Empty)) // Min Marks Internal
                    {
                        objC.InterMarkMin = Convert.ToDecimal(dtNew.Rows[i]["MINMARK_I"]);
                    }
                    else
                    {
                        objC.InterMarkMin = 0;
                    }


                    if (!(dtNew.Rows[i]["MAXMARKS_I"]).ToString().Equals(string.Empty)) // Max Marks Internal
                    {
                        objC.MaxMarks_I = Convert.ToDecimal(dtNew.Rows[i]["MAXMARKS_I"]);
                    }
                    else
                    {
                        objC.MaxMarks_I = 0;
                    }

                    if (!(dtNew.Rows[i]["MINMARK_E"]).ToString().Equals(string.Empty)) // Min Marks External
                    {
                        objC.ExtermarkMin = Convert.ToDecimal(dtNew.Rows[i]["MINMARK_E"]);
                    }
                    else
                    {
                        objC.ExtermarkMin = 0;
                    }

                    if (!(dtNew.Rows[i]["MAXMARK_E"]).ToString().Equals(string.Empty)) // Max Marks External
                    {
                        objC.ExtermarkMax = Convert.ToDecimal(dtNew.Rows[i]["MAXMARK_E"]);
                    }
                    else
                    {
                        objC.ExtermarkMax = 0;
                    }

                    if (!(dtNew.Rows[i]["MIN_TOTAL_MARKS"]).ToString().Equals(string.Empty)) // Min Total Marks
                    {
                        objC.MinTotalMarks = Convert.ToDecimal(dtNew.Rows[i]["MIN_TOTAL_MARKS"]);
                    }
                    else
                    {
                        objC.MinTotalMarks = 0;
                    }

                    if (!(dtNew.Rows[i]["TOTAL_MARK"]).ToString().Equals(string.Empty)) // Total Marks
                    {
                        objC.Total_Marks = Convert.ToDecimal(dtNew.Rows[i]["TOTAL_MARK"]);
                    }
                    else
                    {
                        objC.Total_Marks = 0;

                    }

                    if (dtNew.Rows[i]["ISVALUE_ADDED"].Equals("YES") || dtNew.Rows[i]["ISVALUE_ADDED"].Equals("Yes") || dtNew.Rows[i]["ISVALUE_ADDED"].Equals("yes"))
                    {
                        objC.ValueAdded = 1;
                    }
                    else
                    {
                        objC.ValueAdded = 0;
                    }

                    if (dtNew.Rows[i]["IS_GLOBAL_ELECTIVE"].Equals("YES") || dtNew.Rows[i]["IS_GLOBAL_ELECTIVE"].Equals("Yes") || dtNew.Rows[i]["IS_GLOBAL_ELECTIVE"].Equals("yes"))
                    {
                        objC.GlobalEle = 1;
                    }
                    else
                    {
                        objC.GlobalEle = 0;
                    }


                    if (dtNew.Rows[i]["IS_SPECIALZATION"].Equals("YES") || dtNew.Rows[i]["IS_SPECIALZATION"].Equals("Yes") || dtNew.Rows[i]["IS_SPECIALZATION"].Equals("yes"))
                    {
                        objC.Specialisation = 1;
                    }
                    else
                    {
                        objC.Specialisation = 0;
                    }

                    if (dtNew.Rows[i]["IS_CONSIDER_FOR_FEEDBACK"].Equals("YES") || dtNew.Rows[i]["IS_CONSIDER_FOR_FEEDBACK"].Equals("Yes") || dtNew.Rows[i]["IS_CONSIDER_FOR_FEEDBACK"].Equals("yes"))
                    {
                        objC.IsFeedback = 1;
                    }
                    else
                    {
                        objC.IsFeedback = 0;
                    }


                    if (!(dtNew.Rows[i]["LECTURE"]).ToString().Equals(string.Empty))
                    {
                        objC.Lecture = Convert.ToDecimal(dtNew.Rows[i]["LECTURE"]);
                    }
                    else
                    {
                        objC.Lecture = 0;
                    }

                    if (!(dtNew.Rows[i]["TUTORIAL"]).ToString().Equals(string.Empty))
                    {
                        objC.Theory = Convert.ToDecimal(dtNew.Rows[i]["TUTORIAL"]);
                    }
                    else
                    {
                        objC.Theory = 0;
                    }

                    if (!(dtNew.Rows[i]["PRACTICAL"]).ToString().Equals(string.Empty))
                    {
                        objC.Practical = Convert.ToDecimal(dtNew.Rows[i]["PRACTICAL"]);
                    }
                    else
                    {
                        objC.Practical = 0;
                    }



                    if (!(dtNew.Rows[i]["DRAWING"]).ToString().Equals(string.Empty))
                    {
                        drawing = Convert.ToInt32(dtNew.Rows[i]["DRAWING"]);
                    }
                    else
                    {
                        drawing = 0;
                    }


                    if (!(dtNew.Rows[i]["PAPER_HRS"]).ToString().Equals(string.Empty))
                    {
                        objC.Paper_hrs = Convert.ToInt32(dtNew.Rows[i]["PAPER_HRS"]);
                    }
                    else
                    {
                        objC.Paper_hrs = 0;
                    }

                    //objC.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                    cs = (CustomStatus)objCC.SaveExcelSheetCourseDataInDataBase(objC, drawing);
                    connExcel.Close();
                }

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                // BindListView();
                //  objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Uploaded Successfully!!", this.Page);
            }
            else
            {
                //BindListView();
                //objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Updated Successfully!!", this.Page);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                //objCommon.DisplayMessage(updpnlImportData, "Data not available in ERP Master", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnreportnew_Click(object sender, EventArgs e)
    {
        ExcelReport();
    }

    private void ExcelReport()
    {
        try
        {
            int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            int Schemeno = ddlScheme.SelectedIndex > 0 ? Convert.ToInt32(ddlScheme.SelectedValue) : 0;
            int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
            CourseController objCC = new CourseController();
            DataSet ds = null; //objCC.AllCourseDetailsforExcel(Degreeno, Schemeno, Semesterno);

            ds.Tables[0].TableName = "AllCourseDetails";
            if (ds.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        wb.Worksheets.Add(dt);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=All_Couse_Details.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                //objCommon.DisplayMessage(updpnlImportData, "Record Not Found.", this.Page);
            }
        }
        catch
        {

        }
    }

    #endregion

    #region User Methods Click Events - No use
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string filename = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;

        //To Get the physical Path of the file(test.txt)
        string filepath = Server.MapPath("~/CourseMaterial/");

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath + filename);

        // Checking if file exists
        if (myfile.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", myfile.Length.ToString());

            // Set the ContentType
            Response.ContentType = ReturnExtension(myfile.Extension.ToLower());

            // Write the file into the  response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(myfile.FullName);

            // End the response
            Response.End();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMsg.Text = string.Empty;
    }

    private void ClearControls()
    {
        ViewState["action"] = null;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowReportnew("Check_List", "rptLockUnlockCourseList.rpt");
    }

    private void ShowReportnew(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_DEGREENO=" + ddlDegree.SelectedValue
                + ",@P_SCHEMENO=" + ddlScheme.SelectedValue
                + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UPDCOURSE, this.UPDCOURSE.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetails(int courseno, int schemeno)
    {
        try
        {
            CourseController objCC = new CourseController();
            SqlDataReader dr = objCC.GetCourses(courseno, schemeno);
            if (dr != null)
            {
                if (dr.Read())
                {

                    ////Course Details
                    //txtCourseName.Text = dr["COURSE_NAME"] == DBNull.Value ? string.Empty : dr["COURSE_NAME"].ToString();
                    //txtCourseshortname.Text = dr["SHORTNAME"] == DBNull.Value ? string.Empty : dr["SHORTNAME"].ToString();
                    //txtLectures.Text = dr["LECTURE"] == DBNull.Value ? "0" : dr["LECTURE"].ToString();
                    //txtTutorial.Text = dr["THEORY"] == DBNull.Value ? "0" : dr["THEORY"].ToString();
                    //txtPract.Text = dr["PRACTICAL"] == DBNull.Value ? "0" : dr["PRACTICAL"].ToString();
                    //txtTheory.Text = dr["CREDITS"] == DBNull.Value ? "0" : dr["CREDITS"].ToString();
                    //txtCCode.Text = dr["CCODE"] == DBNull.Value ? string.Empty : dr["CCODE"].ToString();
                    //txtPaper.Text = dr["PAPER_HRS"] == DBNull.Value ? string.Empty : dr["PAPER_HRS"].ToString();
                    //txtTotal.Text = (Convert.ToDecimal(txtLectures.Text) + Convert.ToDecimal(txtTutorial.Text) + Convert.ToDecimal(txtPract.Text)).ToString();
                    ////  txtScaling.Text = dr["SCALEDN_MARK"] == DBNull.Value ? "0" : dr["SCALEDN_MARK"].ToString();
                    //ddlTP.SelectedValue = dr["SUBID"] == DBNull.Value ? "-1" : dr["SUBID"].ToString();
                    //ddlParentDept.SelectedValue = dr["BOS_DEPTNO"] == DBNull.Value ? "-1" : dr["BOS_DEPTNO"].ToString();
                    ////lvCourseMarks.DataSource = dr;
                    ////lvCourseMarks.DataBind();
                    //ddlSemester.SelectedValue = dr["SemesterNo"] == DBNull.Value ? "0" : dr["SemesterNo"].ToString();

                    //ddlCElectiveGroup.SelectedValue = dr["CATEGORYNO"].ToString() == "" ? "0" : dr["CATEGORYNO"].ToString();
                    //////ddlSubcategory.SelectedValue = dr["CATEGORYNO"].ToString() == "" ? "0" : dr["CATEGORYNO"].ToString();

                    //txtCourseTotalMarks.Text = dr["TOTAL_MARK"].ToString() == "" ? "0" : dr["TOTAL_MARK"].ToString();
                    ////txtPreCredit.Text = dr["PreRequisite_Credit"].ToString() == "0" ? string.Empty : dr["PreRequisite_Credit"].ToString();

                    //if (dr["ELECT"] != DBNull.Value)
                    //{
                    //    chkElective.Checked = Convert.ToBoolean(dr["ELECT"]);
                    //    ddlElectiveGroup.SelectedValue = dr["GROUPNO"].ToString() == "" ? "0" : dr["GROUPNO"].ToString();
                    //    chkGlobal.Checked = Convert.ToBoolean(dr["GLOBALELE"]);
                    //}

                    //ChkValueAdded.Checked = Convert.ToBoolean(dr["ISVALUE_ADDED"]);

                    //ChkSpecialization.Checked = Convert.ToBoolean(dr["IS_SPECIAL"]);

                    //ChkIsFeedBack.Checked = Convert.ToBoolean(dr["IS_FEEFBACK"]);
                    ////prerequisite
                    //// string[] schno = ddlScheme.SelectedValue.Split('-');
                    //// string[] semno = ddlSem.SelectedValue.Split('-');
                    //if (dr["PreRequisite"].ToString() == string.Empty)
                    //    this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0", Convert.ToInt32(ddlSem.SelectedValue));
                    //else
                    //    this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), dr["PreRequisite"].ToString(), Convert.ToInt32(ddlSem.SelectedValue));




                    #region Comment
                    //Pre-Defined Marks
                    //txtEndSem.Text = dr["MAXMARKS_E"] == null ? string.Empty : dr["MAXMARKS_E"].ToString();
                    //txtEndSemMin.Text = dr["MINMARKS"] == null ? string.Empty : dr["MINMARKS"].ToString();
                    //txtS1Max.Text = dr["S1MAX"] == null ? string.Empty : dr["S1MAX"].ToString();
                    //txtS2Max.Text = dr["S2MAX"] == null ? string.Empty : dr["S2MAX"].ToString();
                    //txtS3Max.Text = dr["S3MAX"] == null ? string.Empty : dr["S3MAX"].ToString();
                    //txtS4Max.Text = dr["S4MAX"] == null ? string.Empty : dr["S4MAX"].ToString();
                    //txtS5Max.Text = dr["S5MAX"] == null ? string.Empty : dr["S5MAX"].ToString();
                    //txtS6Max.Text = dr["S6MAX"] == null ? string.Empty : dr["S6MAX"].ToString();
                    //txtS7Max.Text = dr["S7MAX"] == null ? string.Empty : dr["S7MAX"].ToString();
                    //txtS8Max.Text = dr["S8MAX"] == null ? string.Empty : dr["S8MAX"].ToString();
                    //txtS9Max.Text = dr["S9MAX"] == null ? string.Empty : dr["S9MAX"].ToString();
                    //txtS10Max.Text = dr["S10MAX"] == null ? string.Empty : dr["S10MAX"].ToString();

                    //txtS1Min.Text = dr["S1MIN"] == null ? string.Empty : dr["S1MIN"].ToString();
                    //txtS2Min.Text = dr["S2MIN"] == null ? string.Empty : dr["S2MIN"].ToString();
                    //txtS3Min.Text = dr["S3MIN"] == null ? string.Empty : dr["S3MIN"].ToString();
                    //txtS4Min.Text = dr["S4MIN"] == null ? string.Empty : dr["S4MIN"].ToString();
                    //txtS5Min.Text = dr["S5MIN"] == null ? string.Empty : dr["S5MIN"].ToString();
                    //txtS6Min.Text = dr["S6MIN"] == null ? string.Empty : dr["S6MIN"].ToString();
                    //txtS7Min.Text = dr["S7MIN"] == null ? string.Empty : dr["S7MIN"].ToString();
                    //txtS8Min.Text = dr["S8MIN"] == null ? string.Empty : dr["S8MIN"].ToString();
                    //txtS9Min.Text = dr["S9MIN"] == null ? string.Empty : dr["S9MIN"].ToString();
                    //txtS10Min.Text = dr["S10MIN"] == null ? string.Empty : dr["S10MIN"].ToString();

                    //txtTotMinMarks.Text = dr["MINMARKS"] == null ? string.Empty : dr["MINMARKS"].ToString();

                    //DDLGrade.SelectedValue = dr["GRADE"] == null ? "0" : dr["GRADE"].ToString();
                    // DDLMinGrade.SelectedValue = dr["MINGRADE"] == null ? "0" : dr["MINGRADE"].ToString();
                    //ddlSpecialisation.SelectedValue = dr["SPECIALISATIONNO"] == null ? "0" : dr["SPECIALISATIONNO"].ToString();
                    #endregion
                }
                dr.Close();

                int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "schemeno='" + schemeno + "'"));
                DataSet ds = objCC.GetCoursesMarks(courseno, patternno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //rtpScheme.DataSource = ds;
                        //rtpScheme.DataBind();
                    }
                    else
                    {
                        //rtpScheme.DataSource = null;
                        //rtpScheme.DataBind();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
            case ".cs":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    public string GetFileNamePath(string filename)
    {
        string path = MapPath("~/CourseMaterial/");
        if (filename != null && filename.ToString() != "")
            return path.ToString() + filename.ToString().Replace("%2520", " ");
        else
            return "";
    }

    protected void btnCheckListReport_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue != "0")
        {
            int chkExit = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME SS WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME EN WITH (NOLOCK) ON(SS.PATTERNNO=EN.PATTERNNO)", "COUNT(*)", "SS.SCHEMENO=" + ddlScheme.SelectedValue + "")); //Added Mahesh on Dated 09-02-2020 due to Report Required Exam name(Pattern) without Exam Name showing . 
            if (chkExit > 0)
            {
                string[] sno = ddlScheme.SelectedValue.Split('-');
                ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", 2, Convert.ToInt32(sno[0]));
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Exam pattern not found for selected scheme, Please check exam creation & pattern.!!!", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Scheme", this.Page);
            return;
        }

    }

    private void ShowReport(string reportTitle, string rptFileName, int type, int schemeno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + schemeno.ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UPDCOURSE, this.UPDCOURSE.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    
}
