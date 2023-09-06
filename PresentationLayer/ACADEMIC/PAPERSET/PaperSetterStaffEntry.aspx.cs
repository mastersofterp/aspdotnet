//======================================================================================
// PROJECT NAME  : UAIMS [RAIPUR]                                                               
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STAFF MASTER FORM                                                    
// CREATION DATE : 30-AUG-2012                                                        
// CREATED BY    : UMESH K. GANORKAR                                             
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_Masters_Staff : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    PStaffController objPStaffController = new PStaffController();
    PStaff objPStaff = new PStaff();
    int SessionNo;
    private static int index = -1;

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
                    //this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    // Set form mode equals to -1(New Mode).
                    ViewState["pstaffno"] = "0";
                    this.PopulateDropDown();
                    ViewState["action"] = "add";
                    // Session["ps"] = null;
                    AddPS_ModCourseHeaders("ps");
                    lblUserMsg.Text = "Note : After adding new Staff Entry give preference for further process!";
                    lblUserMsg.ForeColor = System.Drawing.Color.Red;

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_NAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ddlcollege.SelectedValue, "S.SESSIONID DESC"); //--AND FLOCK = 1

                    FillDropdown();
                    this.LoadStaff();
                    this.LoadStaffExternaltab();
                    this.LoadInternalStaff();
                }

            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        PnlForEnterCode.Visible = false;
        PnlDetailForStaff.Visible = true;

    }


    #region User-Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaperStock.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaperStock.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            // objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO > 0 AND DEPTNO > 0", "DEPTNO");
            // objCommon.FillDropDownList(ddlSchemeType, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0 ", "SCHEMETYPENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //private void Clear()
    //{

    //    txtStaff.Text = string.Empty;
    //    txtAddress.Text = string.Empty;
    //    txtContactNo.Text = string.Empty;
    //    txtEmail.Text = string.Empty;
    //    txtQualification.Text = string.Empty;
    //    txtTeachExp.Text = string.Empty;
    //    lvPSCourse.DataSource = null;
    //    lvPSCourse.DataBind();
    //    ddlDept.SelectedIndex = 0;
    //    ddlCourse.Items.Clear();
    //    ddlCourse.Items.Add(new ListItem("Please Select", "0"));



    //    ViewState["pstaffno"] = "0";
    //}
    #endregion
    private void FillDropdown() // ADDED BY SHUBHAM ON 20022023
    {
        try
        {

            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
            //AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            else

                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            }
            objCommon.FillDropDownList(ddlDeptl, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO > 0 AND DEPTNO > 0", "DEPTNO");
            objCommon.FillDropDownList(ddlBnkName, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_Staff.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDeptPS_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDept.SelectedIndex > 0)
        //{
        //    //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.BOS_DEPTNO =" + ddlDept.SelectedValue, " S.SEMESTERNO");
        //    // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID in( 1,3) AND C.MAXMARKS_E > 0  AND C.BOS_DEPTNO =" + ddlDepartment.SelectedValue, " S.SEMESTERNO");
        //    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), " S.SEMESTERNO");
        //    ddlSemester.Focus();

        //}
        //else
        //{
        //    ddlDept.SelectedIndex = 0;
        //}

        //if (ViewState["pstaffno"].ToString() == "0")
        //{
        //    objCommon.DisplayMessage(this.updatePanel1, "Please select staff first!", this.Page);
        //    return;
        //}
        //ddlSemester.ClearSelection();
        //ddlCourse.ClearSelection();
        //if (ViewState["pstaffno"] != null)
        //    this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
        //ddlSemester.Focus();
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.BOS_DEPTNO =" + ddlDept.SelectedValue, " S.SEMESTERNO");
            // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID in( 1,3) AND C.MAXMARKS_E > 0  AND C.BOS_DEPTNO =" + ddlDepartment.SelectedValue, " S.SEMESTERNO");

        }
        else
        {
            ddlSession.SelectedIndex = 0;
        }

        if (ViewState["pstaffno"].ToString() == "0")
        {
            objCommon.DisplayMessage(this.updatePanel1, "Please select staff first!", this.Page);
            return;
        }
        ddlSemester.ClearSelection();
        ddlCourse.ClearSelection();
        if (ViewState["pstaffno"] != null)
            this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
        ddlSemester.Focus();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSession.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (D.DEPTNO = CD.DEPTNO)", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "D.DEPTNAME");
        //    ddlDepartment.Focus();

        //}
        //else
        //{
        //    ddlDept.SelectedIndex = 0;
        //    objCommon.DisplayMessage("Please Select Session", this.Page);
        //    return;
        //}

        //////ddlDept.ClearSelection();
        //////ddlSemester.ClearSelection();
        //////ddlCourse.ClearSelection();
        //if (ddlSession.SelectedIndex > 0)
        //{
        //    if (ViewState["pstaffno"].ToString() == "0")
        //    {
        //        //ddlSemester.SelectedIndex = 0;
        //        objCommon.DisplayMessage(this.updatePanel1, "Please select staff first!", this.Page);
        //        return;
        //    }
        //}
        ////else
        ////{
        ////    objCommon.DisplayMessage("Please Select Session", this.Page);
        ////    return;
        ////}

        //ddlSemester.Focus();
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.BOS_DEPTNO =" + Convert.ToInt32(ViewState["DeptNo"]) + "AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), " S.SEMESTERNO");
            // objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (D.DEPTNO = CD.DEPTNO)", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 AND COLLEGE_ID=" + Convert.ToInt32(ddlClgname.SelectedValue) + "", "D.DEPTNAME");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Session", this.Page);
            return;
            //ddlDept.SelectedIndex = 0;
        }
        //ddlDept.ClearSelection();
        //ddlSemester.ClearSelection();
        //ddlCourse.ClearSelection();
        //if (ddlSession.SelectedIndex > 0)
        //{
        //    if (ViewState["pstaffno"] != null)
        //        this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
        //    ddlDept.Focus();
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Please Select Session", this.Page);
        //    return;
        //}
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            Boolean IsrecordExist = false;

            objPStaff.staffno = Convert.ToInt32(ViewState["staff"]);
            // objPStaff.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objPStaff.PStaffName = txtIName.Text.Trim().ToUpper();
            objPStaff.PStaffAddress = txtIAdd.Text.Trim().ToUpper();
            objPStaff.Contactno = txtMobile.Text.Trim();
            objPStaff.Emailid = txtIEmail.Text.Trim();
            //objPStaff.Internal_External = rbExternal.Checked == true ? "E" : "I";
            objPStaff.Internal_External = "E";
            objPStaff.Qualification = txtIQualify.Text.Trim();
            objPStaff.Teach_exp = txtITeach.Text.Trim();
            objPStaff.CollegeCode = Session["colcode"].ToString();
            objPStaff.Uno = 0;
            int bankno = Convert.ToInt32(ddlBnkName.SelectedValue);
            int deptno = Convert.ToInt32(ddlDeptl.SelectedValue);
            string NameofInst = txtNameOfInst.Text.Trim();
            string Accno = txtAccNo.Text.Trim();
            string ifscCode = txtIFSC.Text.Trim();
            string Panno = txtPanNo.Text.Trim();

            DataSet ds = objCommon.FillDropDown("ACD_STAFF", "STAFFNO", "STAFF_NAME,STAFF_ADDRESS", "STAFF_NAME LIKE '%" + txtIName.Text + "%' AND INTERNAL_EXTERNAL = 'E' AND CONTACTNO LIKE '%" + txtMobile.Text + "%'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"].ToString()) > 0)
                {
                    IsrecordExist = true;
                    sb.Append(ds.Tables[0].Rows[0]["STAFF_NAME"].ToString());
                    sb.Append(",");
                }
            }
            else
            {
                int staffno = objPStaffController.AddStaff(objPStaff, bankno, deptno, Accno, NameofInst, ifscCode, Panno);
                if (staffno > 0)
                {
                    objCommon.DisplayMessage(updatePanel3, "Staff Added Successfully!", this.Page);

                    ViewState["pstaffno"] = staffno.ToString();
                    this.LoadStaff();
                    this.LoadStaffExternaltab();
                    //Clear();

                    txtStaff.Enabled = true;
                    PnlDetailForStaff.Visible = true;
                    //PnlForEnterCode.Visible = false;
                    // div_preference.Visible = false; //***************
                }

                else if (Convert.ToInt32(staffno) == -1)
                {
                    objCommon.DisplayMessage(updatePanel3, "Staff Updated Successfully!", this.Page);
                    // div_preference.Visible = false; //***************
                    this.LoadStaff();
                    Response.Redirect(Request.Url.ToString());

                    // ViewState["pstaffno"] = staffno.ToString();
                }
                else
                    objCommon.DisplayMessage(updatePanel3, "Error Updating Staff!", this.Page);
                char[] sp = { '-' };


                ViewState["staff"] = 0;

                // Comment by Shubham On 28/02/2023
                //foreach (ListViewItem item in lvPSCourse.Items)
                //{
                //    HiddenField hfCcode = item.FindControl("hfCcode") as HiddenField;
                //    //HiddenField hdfcourse = item.FindControl("hfCcode") as HiddenField;

                //    string ccode1 = hfCcode.Value;
                //    // int courseno = Convert.ToInt32(hdfcourse.Value);
                //    ////***********int cnt = int.Parse(objCommon.LookUp("ACD_PS_MOD_PREFERENCE", "COUNT(*)", "PS_MOD = 1 AND CCODE = '" + ccode1 + "' AND STAFFNO = " + ViewState["pstaffno"].ToString()));
                //    int cnt = int.Parse(objCommon.LookUp("ACD_PS_MOD_PREFERENCE", "COUNT(*)", "PS_MOD = 1 AND CCODE = '" + ccode1 + "' AND STAFFNO = " + ViewState["pstaffno"].ToString() + "AND SESSIONNO= " + Convert.ToInt32(ddlSession.SelectedValue)));
                //    if (cnt > 0)
                //    {
                //        objCommon.DisplayMessage(this.updatePanel3, "Course already added to List!", this.Page);
                //        return;
                //    }
                //    else//*********************
                //        objPStaffController.AddStaffPreference(Convert.ToInt32(ViewState["pstaffno"]), ccode1, 1, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddl));


                //    this.LoadStaff();

                //}
            }
            if (IsrecordExist)
            {
                string ss = sb.ToString();
                ScriptManager.RegisterStartupScript(updatePanel1, GetType(), "YourUniqueScriptKey", "alert('Staff " + ss + " entry already done!');", true);
                //ScriptManager.RegisterClientScriptBlock(updatePanel1, GetType(), "YourUniqueScriptKey", "alert('Staff " + ss + " entry already done!');", true);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;

            CustomStatus cs = (CustomStatus)objPStaffController.DeleteStaff(int.Parse(btnDelete.CommandArgument));
            // dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(updatePanel1, "Staff canceled Successfully!", this.Page);
                this.LoadStaff();
                //this.Clear();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.Master.Staff.btnDelete_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnEditRecord = sender as ImageButton;
            lblUserMsg.Text = "Note : Give paperset preference to staff for further process!";
            DataSet ds = objPStaffController.GetStaffDetails(int.Parse(btnEditRecord.CommandArgument));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                div_preference.Visible = true; //***************

                ImageButton btnEdit = sender as ImageButton;
                int staffno = Convert.ToInt32(btnEdit.CommandArgument);
                ViewState["staff"] = Convert.ToInt32(btnEdit.CommandArgument);  //added by reena

                DataRow dr = ds.Tables[0].Rows[0];
                ViewState["action"] = "edit";
                ViewState["pstaffno"] = dr["STAFFNO"].ToString();
                //ViewState["sessionno"] = dr["SESSIONNO"].ToString();
                txtStaff.Text = dr["STAFF_NAME"].ToString();
                txtAddress.Text = dr["STAFF_ADDRESS"].ToString();
                txtContactNo.Text = dr["CONTACTNO"].ToString();
                txtEmail.Text = dr["EMAIL_ID"].ToString();

                txtQualification.Text = dr["QUALIFICATION"] == DBNull.Value ? string.Empty : dr["QUALIFICATION"].ToString();
                txtTeachExp.Text = dr["TEACH_EXP"] == DBNull.Value ? string.Empty : dr["TEACH_EXP"].ToString();

                txtStaff.Enabled = true; // added by shubham on 130302023

                if (dr["INTERNAL_EXTERNAL"].ToString() == "I")
                {
                    rbInternal.Checked = true;
                    rbExternal.Checked = false;
                }
                else if (dr["INTERNAL_EXTERNAL"].ToString() == "E")
                {
                    rbExternal.Checked = true;
                    rbInternal.Checked = false;
                }
                ddlCourse.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
                ddlDept.SelectedIndex = 0;
                txtStaff.Enabled = false;
                this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillCourse(int deptno, int semesterNo)
    {
        //try
        //{

        //    //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO)", "DISTINCT C.CCODE", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", "C.MAXMARKS_E > 0 AND C.SUBID=1 AND C.BOS_DEPTNO = " + deptno + " AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(SR.CANCEL,0) = 0 ", "C.CCODE");

        //    //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = SR.SESSIONNO)", "DISTINCT C.CCODE", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", "C.MAXMARKS_E > 0 AND C.SUBID IN (1,3)  AND C.BOS_DEPTNO = " + deptno + " AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(SR.CANCEL,0) = 0 ", "C.CCODE");
        //    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO)", "DISTINCT C.CCODE", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", "C.MAXMARKS_E > 0 AND C.SUBID IN (1,3)  AND C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND C.BOS_DEPTNO =" + deptno + " AND ISNULL(SR.CANCEL,0) = 0 ", "C.CCODE");
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_Masters_Staff.FillCourse --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
        try
        {

            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO)", "DISTINCT C.CCODE", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", "C.MAXMARKS_E > 0 AND C.SUBID=1 AND C.BOS_DEPTNO = " + deptno + " AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(SR.CANCEL,0) = 0 ", "C.CCODE");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO LEFT OUTER JOIN ACD_PAPERSET_DETAILS P ON (P.CCODE = C.CCODE AND P.SEMESTERNO=C.SEMESTERNO)", "DISTINCT C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", " C.SEMESTERNO = " + ddlSemester.SelectedValue + "AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND MAXMARKS_E > 0 AND SUBID =1", "C.COURSENO");
            //(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO", "DISTINCT C.CCODE", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", "C.MAXMARKS_E > 0 AND C.SUBID IN (1)  AND C.BOS_DEPTNO = " + deptno + " AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "C.CCODE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.FillCourse --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected bool CheckCourse(string courseno, DataTable dt)
    {
        bool flag = false;
        try
        {

            DataTableReader dtr = dt.CreateDataReader();
            if (dtr != null)
            {
                while (dtr.Read())
                {
                    if (courseno.Equals(dtr["CCODE"].ToString()))
                    {
                        flag = true;
                    }
                }
                dtr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.CheckCourse-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return flag;
    }

    protected void btnAddPS_Click(object sender, EventArgs e)
    {
        try 
        {
            char[] sp = { '-' };
            string[] course = ddlCourse.SelectedItem.Text.Split(sp);
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue));
            string semester = ddlSemester.SelectedItem.Text.ToString();

            if (ViewState["pstaffno"].ToString() != string.Empty && ViewState["pstaffno"].ToString() == "0")
            {
                objCommon.DisplayMessage(this.updatePanel1, "Please select staff", this.Page);
                return;
            }
            else
            {
                //Edit.. if edit directly add to the table
                //string ccode = course[0].Trim();
                // int courseno = int.Parse(ddlCourse.SelectedValue);
                int ps_mod = 1;     //paper setting preference


                SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));
                ViewState["SessionNo"] = SessionNo;
                int cnt = int.Parse(objCommon.LookUp("ACD_PS_MOD_PREFERENCE", "COUNT(*)", " CCODE = '" + ccode + "' AND STAFFNO= " + ViewState["pstaffno"].ToString() + " AND COURSENO= " + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO= " + Convert.ToInt32(SessionNo)));
                if (cnt > 0)
                {
                    objCommon.DisplayMessage(this.updatePanel1, "Course already added to List!", this.Page);
                }
                else
                {
                    objPStaffController.AddStaffPreference(Convert.ToInt32(ViewState["pstaffno"]), ccode, ps_mod, Convert.ToInt32(SessionNo), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlCourse.SelectedValue));
                }

                this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
                clearall();
                ClearPs();
            }
            ddlCourse.Focus();
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.btnAddPS_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        
    }

    private void BindPsCourse(int staffno)
    {
        try 
        {
            int Session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));
            DataSet ds = objCommon.FillDropDown("ACD_PS_MOD_PREFERENCE P INNER JOIN ACD_COURSE C ON C.CCODE = P.CCODE INNER JOIN ACD_SCHEME S ON(S.SCHEMENO = C.SCHEMENO)", "DISTINCT P.CCODE", "C.CCODE,C.COURSE_NAME + '-' + C.CCODE AS COURSE_NAME, DBO.FN_DESC('DEGREENAME',S.DEGREENO)DEGREENAME,DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTERNAME", "PS_MOD = 1 AND STAFFNO= " + staffno + " AND SESSIONNO= " + Session + " AND C.SEMESTERNO  > 0 AND (C.SEMESTERNO = " + ddlSemester.SelectedValue + " OR " + ddlSemester.SelectedValue + "= 0)", "P.CCODE");
            // Session["ps"] = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvPSCourse.DataSource = ds;
                lvPSCourse.DataBind();
                lvPSCourse.Visible = true;
            }
            else
            {
                lvPSCourse.DataSource = null;
                lvPSCourse.DataBind();
                lvPSCourse.Visible = false;
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.BindPsCourse-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void AddPS_ModCourseHeaders(string ps_mod)
    {
        //Create a Datatable and all the records
        DataTable dt = new DataTable(ps_mod);
        DataColumn column;

        //Add Header Columns
        //==================
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "COURSENO";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "CCODE";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "COURSE_NAME";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "SEMESTERNAME";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);


        //column = new DataColumn();
        //column.DataType = System.Type.GetType("System.String");
        //column.ColumnName = "BRANCHNAME";
        //column.ReadOnly = true;
        ////add column to table
        //dt.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "DEGREENAME";
        column.ReadOnly = true;
        //add column to table
        dt.Columns.Add(column);

        Session[ps_mod] = dt;
    }

    protected void btnDeletePS_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDeletePS = sender as ImageButton;

            if (ViewState["pstaffno"].ToString() != string.Empty && ViewState["pstaffno"].ToString() == "0")
            {
                if (Session["ps"] != null)
                {
                    DataTable dtCourse = (DataTable)Session["ps"];

                    index = (btnDeletePS.Parent as ListViewDataItem).DataItemIndex;

                    dtCourse.Rows[index].Delete();
                    dtCourse.AcceptChanges();

                    //Store DataTable back to session
                    Session["ps"] = dtCourse;

                    if (dtCourse.Rows.Count > 0 && dtCourse != null)
                    {
                        lvPSCourse.DataSource = dtCourse;
                        lvPSCourse.DataBind();
                    }
                    else
                    {
                        lvPSCourse.DataSource = null;
                        lvPSCourse.DataBind();
                    }
                }
            }
            else
            {
                //Edit.. if edit directly add to the table..
                //string ccode = btnDeletePS.AlternateText;
                string ccode = Convert.ToString(btnDeletePS.CommandArgument);
                int ps_mod = 1;     //paper setting preference

                objPStaffController.DeleteStaffPreference(Convert.ToInt32(ViewState["pstaffno"]), ccode, ps_mod);
                objCommon.DisplayMessage(this.updatePanel1, "Course Deleted Successfully", this.Page);
                this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.btnDeletePS_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void LoadStaff()
    {
        try
        {
            //int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
            DataSet ds = objPStaffController.GetStaffList();
            ////if (ds != null && ds.Tables.Count > 0)
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //Panel1.Visible = true;
                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                txtStaff.Enabled = false;
            }
            else
            {
                lvlinks.DataSource = null;
                lvlinks.DataBind();
                //Panel1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.LoadStaff() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //ADDED BY SHUBHAM ON 16022023

    private void LoadStaffExternaltab()
    {
        try
        {
            //int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
            DataSet ds = objPStaffController.GetExternalStaffList();
            ////if (ds != null && ds.Tables.Count > 0)
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //Panel1.Visible = true;
                lvElist.DataSource = ds;
                lvElist.DataBind();
            }
            else
            {
                lvElist.DataSource = null;
                lvElist.DataBind();
                //Panel1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.LoadStaff() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void LoadInternalStaff()
    {
        try
        {
            //int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
            DataSet ds = objPStaffController.GetInternalStaffList();
            ////if (ds != null && ds.Tables.Count > 0)
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //Panel1.Visible = true;
                lvintrn.DataSource = ds;
                lvintrn.DataBind();
            }
            else
            {
                lvintrn.DataSource = null;
                lvintrn.DataBind();
                //Panel1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.LoadStaff() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private bool CheckStaff()
    {
        string name = txtStaff.Text.Trim();
        string Contact = txtContactNo.Text.Trim();
        string staff = ViewState["pstaffno"] == null ? "" : " AND STAFFNO <>" + ViewState["pstaffno"].ToString();
        string session = ViewState["sessionno"] == null ? "" : " AND SESSIONNO <>" + ViewState["sessionno"].ToString();
        int cnt = int.Parse(objCommon.LookUp("ACD_STAFF", "COUNT(STAFFNO)", "STAFF_NAME ='" + name + "' AND CONTACTNO='" + Contact + "' " + staff + session));
        if (cnt > 0)
            return false;
        else
            return true;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            if (ViewState["pstaffno"].ToString() == "0")
            {
                objCommon.DisplayMessage(this.updatePanel1, "Please select staff first!", this.Page);
                ddlSession.SelectedIndex = 0;
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                return;
            }

            ddlCourse.ClearSelection();
            if (ViewState["pstaffno"] != null)
                this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
            this.FillCourse(Convert.ToInt32(ViewState["DeptNo"]), Convert.ToInt32(ddlSemester.SelectedValue));
            ddlCourse.Focus();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }

        //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO)", "DISTINCT C.CCODE", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME,DBO.FN_DESC('SCHEMETYPE',S.SCHEMETYPE)SCHEMETYPE", "C.MAXMARKS_E > 0 AND C.SUBID=1 AND C.BOS_DEPTNO = " + deptno + " AND S.SCHEMETYPE=" + Convert.ToInt32(ddlSchemeType.SelectedValue) + "AND C.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(SR.CANCEL,0) = 0 ", "C.CCODE");

        //ddlSemester.ClearSelection();
        //ddlCourse.ClearSelection();
        //if (ViewState["pstaffno"] != null)
        //    this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
    }

    public void clearall()
    {

        ddlcollege.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        //ddlDept.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        txtIAdd.Text = "";
        txtIEmail.Text = "";
        txtMobile.Text = "";
        txtIName.Text = "";
        txtIQualify.Text = "";
        txtITeach.Text = "";
    }

    protected void rbInternal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbInternal.Checked == true)
        {

            PnlForEnterCode.Visible = true;
            PnlDetailForStaff.Visible = false;


        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string ccode1 = TextBox1.Text;
            if (TextBox1.Text == "")
            {
                objCommon.DisplayMessage(this.updatePanel1, "Please Enter Employee Code!", this.Page);
                PnlForEnterCode.Visible = true;
                PnlDetailForStaff.Visible = false;
            }
            if (TextBox1.Text != "")
            {

                int result = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "COUNT(1)", "EXISTS(SELECT UA_IDNO  FROM user_acc WHERE IDNO = UA_IDNO  AND PFILENO ='" + ccode1 + "' AND ua_type not in (1,2) AND  ISNULL(UA_STATUS,0) <> 1)"));
                if (result == 1)
                {

                    DataSet ds = null;

                    //ds = objCommon.FillDropDown("payroll_empmas p inner join user_acc u on (p.IDNO=u.UA_IDNO)", "p.PHONENO", "u.UA_EMAIL,u.UA_FULLNAME,p.RESADD1,u. UA_NO", "p.PFILENO='" + ccode1 + "' AND u.ua_type not in (1,2) AND  ISNULL(UA_STATUS,0) <> 1", "");

                    ds = objCommon.FillDropDown("PAYROLL_EMPMAS P INNER JOIN USER_ACC U ON (P.IDNO=U.UA_IDNO)LEFT JOIN ACD_STAFF S ON (S.UA_NO = U.UA_NO)", "p.PHONENO", "u.UA_EMAIL,u.UA_FULLNAME,p.RESADD1,u. UA_NO,ISNULL(S.STAFFNO,0)STAFFNO", "p.PFILENO='" + ccode1 + "' AND u.ua_type not in (1,2) AND  ISNULL(UA_STATUS,0) <> 1", "");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"].ToString()) > 0)
                        {
                            objCommon.DisplayMessage(this.updatePanel1, "Staff entry already done!", this.Page);
                            TextBox1.Text = "";
                            PnlForEnterCode.Visible = true;
                            PnlDetailForStaff.Visible = false;
                            return;
                        }
                        PnlForEnterCode.Visible = false;
                        TextBox1.Text = "";
                        PnlDetailForStaff.Visible = true;
                        txtStaff.Enabled = false;

                        txtContactNo.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["UA_EMAIL"].ToString();
                        txtStaff.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                        txtAddress.Text = ds.Tables[0].Rows[0]["RESADD1"].ToString();
                        ViewState["uano"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                    }


                }
                else
                {
                    objCommon.DisplayMessage(this.updatePanel1, "Employee Code Not Present!", this.Page);
                    PnlForEnterCode.Visible = true;
                    PnlDetailForStaff.Visible = false;
                    TextBox1.Text = "";

                }
            }
        }
        catch
        {
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                //objCommon.FillListBox(ddlStaff, "USER_ACC", "UA_NO", "UA_FULLNAME", "ua_type not in (1,2) AND  ISNULL(UA_STATUS,0) <> 1 AND " + ddlDepartment.SelectedValue + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_FULLNAME");
                objCommon.FillListBox(ddlStaff, "USER_ACC", "UA_NO", "UA_FULLNAME", "ua_type not in (1,2) AND  ISNULL(UA_STATUS,0) <> 1", "UA_FULLNAME");
            }
            else
            {
                //ddlDepartment.SelectedIndex
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.LoadStaff() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlcollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (D.DEPTNO = CD.DEPTNO)", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "", "D.DEPTNAME");
            ddlSession.Focus();
        }
        else
        {

            //ddlSession.SelectedIndex = 0;
            //ddlDepartment.SelectedIndex = 0;
            //ddlSemester.SelectedIndex = 0;
        }
    }

    //protected void BtnAddStaff_Click(object sender, EventArgs e)
    //{
    //    PStaffController objPStaffController = new PStaffController();
    //    Button btn = sender as Button;
    //    try
    //    {
    //        CourseController objCC = new CourseController();
    //        foreach (ListViewItem item in lvintrn.Items)
    //        {
    //            //Button btnRCVd1 = item.FindControl("btnRCVd1") as Button;
    //            Button BtnAddStaff = item.FindControl("BtnAddStaff") as Button;
    //            if (BtnAddStaff.GetHashCode() == btn.GetHashCode())
    //            {

    //                Label lblname = item.FindControl("lblname") as Label;
    //                Label lblContact = item.FindControl("lblContact") as Label;
    //                Label lblDepartment = item.FindControl("lblDepartment") as Label;
    //                Label lblEmail = item.FindControl("lblEmail") as Label;
    //                HiddenField HdnAdd = item.FindControl("HdnAdd") as HiddenField;

    //                //objPStaff.staffno = Convert.ToInt32(BtnAddStaff.ToolTip);
    //                // objPStaff.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
    //                objPStaff.PStaffName = lblname.Text;
    //                objPStaff.PStaffAddress = HdnAdd.Value.ToString();
    //                objPStaff.Contactno = lblContact.Text.Trim();
    //                objPStaff.Emailid = lblEmail.Text.Trim();
    //                objPStaff.Internal_External = "I";
    //                objPStaff.Uno = Convert.ToInt32(BtnAddStaff.ToolTip);
    //                objPStaff.CollegeCode = Session["colcode"].ToString();

    //                DataSet ds = null;
    //                ds = objCommon.FillDropDown("PAYROLL_EMPMAS P INNER JOIN USER_ACC U ON (P.IDNO=U.UA_IDNO)LEFT JOIN ACD_STAFF S ON (S.UA_NO = U.UA_NO)", "p.PHONENO", "u.UA_EMAIL,u.UA_FULLNAME,p.RESADD1,u. UA_NO,ISNULL(S.STAFFNO,0)STAFFNO", "S.UA_NO=" + Convert.ToInt32(BtnAddStaff.ToolTip) + " AND u.ua_type not in (1,2) AND  ISNULL(UA_STATUS,0) <> 1", "");

    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"].ToString()) > 0)
    //                    {
    //                        objCommon.DisplayMessage(updatePanel1, "Staff entry already done!", this.Page);
    //                        return;
    //                    }
    //                }
    //                int staffno = objPStaffController.AddStaff(objPStaff);
    //                if (staffno > 0)
    //                {
    //                    objCommon.DisplayMessage(updatePanel1, "Staff Added Successfully!", this.Page);

    //                    ViewState["pstaffno"] = staffno.ToString();
    //                    this.LoadStaff();
    //                    this.LoadStaffExternaltab();
    //                    Clear();
    //                    rbExternal.Checked = true;
    //                    rbInternal.Checked = false;
    //                    txtStaff.Enabled = true;
    //                    PnlDetailForStaff.Visible = true;
    //                    PnlForEnterCode.Visible = false;
    //                    div_preference.Visible = false; //***************
    //                }
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 and ISNULL(IS_ACTIVE,0)=1  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_PNAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "S.SESSIONID DESC"); //--AND FLOCK = 1
                ViewState["DeptNo"] = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]));
                ddlSession.Focus();
            }

        }
        //if (ddlClgname.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_NAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ddlClgname.SelectedValue, "S.SESSIONID DESC"); //--AND FLOCK = 1
        //    ddlSession.Focus();
        //    // objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEPT CD ON (D.DEPTNO = CD.DEPTNO)", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "", "D.DEPTNAME");
        //    // ddlSession.Focus();
        //}
        else
        {

            ddlSession.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
        }
    }
    protected void BtnAddStaff_Click(object sender, EventArgs e)
    {
        String Ua_no = String.Empty;
        String CollegeCode = Session["colcode"].ToString();
        StringBuilder sb = new StringBuilder();
        Boolean IsrecordExist = false;
        string tempid = "0";


        foreach (ListItem items in ddlStaff.Items)
        {
            tempid = items.Value;
            if (items.Selected == true)
            {
                //Ua_no += items.Value + ",";
                Ua_no = items.Value;
                DataSet ds = null;
                ds = objCommon.FillDropDown("ACD_STAFF", "STAFFNO", "STAFF_NAME,STAFF_ADDRESS", "UA_NO=" + Convert.ToInt32(Ua_no) + "AND DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"].ToString()) > 0)
                    {
                        IsrecordExist = true;
                        //objCommon.DisplayMessage(updatePanel1, "Staff entry already done!", this.Page);
                        sb.Append(ds.Tables[0].Rows[0]["STAFF_NAME"].ToString());
                        sb.Append(",");
                        //sb.Append("");
                        // return;
                    }
                    //if (IsrecordExist)
                    //{
                    //    string ss = sb.ToString();
                    //    objCommon.DisplayMessage(updatePanel1, "Staff " + ss + " entry already done!", this.Page);
                    //    //objCommon.DisplayMessage(updatePanel1, "Staff Added Successfully!", this.Page);
                    //    //objCommon.DisplayUserMessage(this.Page, "Staff " + ss + " entry already done!", this.Page);
                    //}
                }
                else
                {
                    int staffno = this.AddStaff(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(Ua_no), Session["colcode"].ToString());
                    if (staffno > 0)
                    {
                        objCommon.DisplayMessage(updatePanel1, "Staff Added Successfully!", this.Page);

                        ViewState["pstaffno"] = staffno.ToString();
                        this.LoadStaff();
                        this.LoadStaffExternaltab();
                        this.LoadInternalStaff();
                        //Clear();
                        rbExternal.Checked = true;
                        rbInternal.Checked = false;
                        txtStaff.Enabled = true;
                        PnlDetailForStaff.Visible = true;
                        PnlForEnterCode.Visible = false;
                        div_preference.Visible = false; //***************
                        // ClearI();
                    }
                }
            }
        }



        //if (!String.IsNullOrEmpty(ss))
        //{
        //    objCommon.DisplayMessage(updatePanel1, "Staff " + ss + " entry already done!", this.Page);
        //}
        if (IsrecordExist)
        {
            string ss = sb.ToString();
            ScriptManager.RegisterStartupScript(updatePanel1, GetType(), "YourUniqueScriptKey", "alert('Staff " + ss + " entry already done!');", true);
            //ScriptManager.RegisterClientScriptBlock(updatePanel1, GetType(), "YourUniqueScriptKey", "alert('Staff " + ss + " entry already done!');", true);
            ClearI();
        }
        ClearI();

    }

    public int AddStaff(int dept, int ua_no, String CollegeCode)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        int status;
        try
        {
            SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
            SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_DEPARTMENTID", dept),
                    new SqlParameter("@P_UA_NO", ua_no),
                    new SqlParameter("@P_COLLEGE_CODE", CollegeCode),
                    new SqlParameter("@P_OUT", SqlDbType.Int),
                };
            sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

            object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_INTERNAL_EMP_PAPERSET", sqlParams, true);
            status = Convert.ToInt16(ret);

            if (ret != null)
            {
                retStatus = status;
            }
            else
            {
                retStatus = -99;
            }

            return retStatus;
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
        }
        // return ret;
    }

    protected void btnIClear_Click(object sender, EventArgs e)
    {
        ClearI();
    }

    protected void btnEClear_Click(object sender, EventArgs e)
    {
        ClearE();
    }

    protected void btnPsCancel_Click(object sender, EventArgs e)
    {
        ClearPs();
    }

    protected void ClearI()
    {
        ddlcollege.SelectedIndex = 0;
        //ddlDepartment.SelectedIndex = 0;
        //ddlStaff.SelectedIndex = -1;
        ddlDepartment.Items.Clear();     //Added by Injamam on 14-3-23
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlStaff.Items.Clear();
    }

    protected void ClearPs()
    {
        txtContactNo.Text = "";
        txtEmail.Text = "";
        txtStaff.Text = "";
        txtStaff.Enabled = true;
        txtAddress.Text = "";
        txtQualification.Text = "";
        txtTeachExp.Text = "";
        ddlClgname.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlDept.Items.Clear();
        ddlDept.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ViewState["pstaffno"] = "0";
        lvPSCourse.DataSource = null;
        lvPSCourse.DataBind();

    }

    protected void ClearE()
    {
        txtIName.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtIEmail.Text = string.Empty;
        txtIQualify.Text = string.Empty;
        txtIAdd.Text = string.Empty;
        txtITeach.Text = string.Empty;
    }

    protected void lnkStaffName_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkStaff = sender as LinkButton;
            lblUserMsg.Text = "Note : Give paperset preference to staff for further process!";
            string url = string.Empty;
            DataSet ds = objPStaffController.GetStaffDetails(int.Parse(lnkStaff.CommandArgument));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                div_preference.Visible = true; //***************

                LinkButton lnkStaffName = sender as LinkButton;
                int staffno = Convert.ToInt32(lnkStaffName.CommandArgument);
                ViewState["staff"] = Convert.ToInt32(lnkStaffName.CommandArgument);  //added by reena

                DataRow dr = ds.Tables[0].Rows[0];
                ViewState["action"] = "edit";
                ViewState["pstaffno"] = dr["STAFFNO"].ToString();
                //ViewState["sessionno"] = dr["SESSIONNO"].ToString();
                txtStaff.Text = dr["STAFF_NAME"].ToString();
                txtAddress.Text = dr["STAFF_ADDRESS"].ToString();
                txtContactNo.Text = dr["CONTACTNO"].ToString();
                txtEmail.Text = dr["EMAIL_ID"].ToString();

                txtQualification.Text = dr["QUALIFICATION"] == DBNull.Value ? string.Empty : dr["QUALIFICATION"].ToString();
                txtTeachExp.Text = dr["TEACH_EXP"] == DBNull.Value ? string.Empty : dr["TEACH_EXP"].ToString();

                txtStaff.Enabled = true; // added by shubham on 130302023

                if (dr["INTERNAL_EXTERNAL"].ToString() == "I")
                {
                    rbInternal.Checked = true;
                    rbExternal.Checked = false;
                }
                else if (dr["INTERNAL_EXTERNAL"].ToString() == "E")
                {
                    rbExternal.Checked = true;
                    rbInternal.Checked = false;
                }

                if (ddlCourse.SelectedIndex > 0)
                {
                    ddlCourse.SelectedIndex = 0;
                }
                if (ddlSemester.SelectedIndex > 0)
                {
                    ddlSemester.SelectedIndex = 0;
                }
                if (ddlDept.SelectedIndex > 0)
                {
                    ddlDept.SelectedIndex = 0;
                }
                txtStaff.Enabled = false;
                this.BindPsCourse(Convert.ToInt32(ViewState["pstaffno"]));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Staff.lnkStaffName_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
