using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class ACADEMIC_CourseRegAppByAdvisor : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    CourseRegistrationApproveByAdvisorEntity objEntity = new CourseRegistrationApproveByAdvisorEntity();
    CourseRegistrationApprovebyAdvisorController objController = new CourseRegistrationApprovebyAdvisorController();

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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;
          
               // IPADDRESS = ip.AddressList[0].ToString();
                IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    #region Page Event

    protected void ddlSession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        divCourseReg.Visible = false;
    }

    protected void ddlCollegeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divCourseReg.Visible = false;
            if (ddlCollegeName.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE C WITH (NOLOCK) ON (D.DEGREENO=C.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollegeName.SelectedValue + "", "DEGREENO");
            }
            else
            {
                    ddlDegree.Items.Clear();
                    ddlDegree.Items.Add("Please Select");
                    ddlDegree.SelectedItem.Value = "0";
            }

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add("Please Select");
            ddlBranch.SelectedItem.Value = "0";

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.ddlCollegeName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divCourseReg.Visible = false;
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND B.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "A.LONGNAME");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add("Please Select");
                ddlBranch.SelectedItem.Value = "0";
            }

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divCourseReg.Visible = false;
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER SM WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SM.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_STUDENT_RESULT R WITH (NOLOCK) ON (SM.SEMESTERNO=R.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "SM.SEMESTERNO > 0 AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + "AND S.DEGREENO =" + ddlDegree.SelectedValue + "AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "SM.SEMESTERNO");
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add("Please Select");
                ddlSemester.SelectedItem.Value = "0";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        divCourseReg.Visible = false;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try 
        {
            SqlDataReader dr = null;

            objEntity.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objEntity.CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
            objEntity.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objEntity.Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            objEntity.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objEntity.UA_NO = Convert.ToInt32(Session["userno"]);
            
            dr = objController.GetCourseRegistrationStudentData(objEntity);

            if (dr != null)
            {
                lvCourseRegistrationStudent.DataSource = dr;
                lvCourseRegistrationStudent.DataBind();
            }

            if (lvCourseRegistrationStudent.Items.Count > 0)
            {
                divCourseReg.Visible = true;
                btnBulkApprove.Visible = true;
            }
            else
            {
                btnBulkApprove.Visible = false;
                divCourseReg.Visible = false;
                objCommon.DisplayMessage(this.updpnl_details, "Sorry, Record not found !!!.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
       
        BindSingleStudentCourseRegistrationDetail(Convert.ToInt32(btn.CommandArgument));
        int Count = 0;

        lblRegistrationNo.Text = btn.CommandName;

        foreach (ListViewDataItem dataitem in lvCourseRegistrationStudent.Items)
        {
            CheckBox cbRowS = dataitem.FindControl("cbRow") as CheckBox;
            if (Convert.ToInt32(cbRowS.ToolTip) == Convert.ToInt32(btn.CommandArgument))
            {
                cbRowS.Checked = true;
                txtTotChk.Text = "1";
            }
        }

        foreach (ListViewDataItem dataitem in lstCourseDetail.Items)
        {
            CheckBox cbRow = dataitem.FindControl("cbRowCourse") as CheckBox;
            if (cbRow.Checked == true)
            {
                Count++;
            }
        }
        if (Count > 0)
        {
            txtSelectionRecord.Text = Convert.ToString(Count);
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View();", true);
    }

    protected void btnSingleSubmit_OnClick(object sender, EventArgs e)
    {
            DataRow dr = null;
            DataTable StudCourseReg = new DataTable("TBL_COURSE_REGISTRATION_BY_ADVISOR_BULK");
            StudCourseReg.Columns.Add("IDNO", typeof(int));
            StudCourseReg.Columns.Add("SEMESTERNO", typeof(int));
            StudCourseReg.Columns.Add("SESSIONNO", typeof(int));
            StudCourseReg.Columns.Add("SCHEMENO", typeof(int));
            StudCourseReg.Columns.Add("COURSENO", typeof(int));
            StudCourseReg.Columns.Add("CCODE", typeof(string));
            StudCourseReg.Columns.Add("COURSENAME", typeof(string));
            StudCourseReg.Columns.Add("CREDITS", typeof(decimal));
            StudCourseReg.Columns.Add("SUBID", typeof(int));
            StudCourseReg.Columns.Add("SRNO", typeof(int));
            
            foreach (ListViewDataItem dataitem in lstCourseDetail.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRowCourse") as CheckBox;
                if (cbRow.Checked == true)
                {
                    HiddenField hdfSchemeno = dataitem.FindControl("hdfSchemeno") as HiddenField;
                    HiddenField hdfCourseno = dataitem.FindControl("hdfCourseno") as HiddenField;
                    HiddenField hdfSubId = dataitem.FindControl("hdfSubId") as HiddenField;
                    HiddenField hdfSrNo = dataitem.FindControl("hdfSrNo") as HiddenField;

                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
                    Label lblCredits = dataitem.FindControl("lblCredits") as Label;

                    dr = StudCourseReg.NewRow();
                    dr["IDNO"] = cbRow.ToolTip;
                    dr["SEMESTERNO"] = ddlSemester.SelectedValue;
                    dr["SESSIONNO"] = ddlSession.SelectedValue;
                    dr["SCHEMENO"] = hdfSchemeno.Value;
                    dr["COURSENO"] = hdfCourseno.Value;
                    dr["CCODE"] = lblCCode.Text;
                    dr["COURSENAME"] = lblCourseName.Text;
                    dr["CREDITS"] = lblCredits.Text;
                    dr["SUBID"] = hdfSubId.Value;
                    dr["SRNO"] = hdfSrNo.Value;

                    StudCourseReg.Rows.Add(dr);
                }
            }

            objEntity.dtStudCourseReg = StudCourseReg;
            objEntity.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objEntity.CollegeId =Convert.ToInt32(ddlCollegeName.SelectedValue);
            objEntity.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objEntity.Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            objEntity.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objEntity.IPAddress = ViewState["ipAddress"].ToString();
            objEntity.EntryType = 1; //1 For Single Student Entry
            objEntity.UA_NO = Convert.ToInt32(Session["userno"]);

            int res = objController.InsertUpdate_ApprovalStatus(objEntity);
            if (res > 0)
            {
                objCommon.DisplayMessage(this.updpnl_details, "Record Save Successfully.", this.Page);
                btnShow_Click(sender, e);
                txtTotChk.Text = "";
            }
    }

    protected void btnBulkApprove_Click(object sender, EventArgs e)
    {
        DataRow dr = null;
        DataTable StudCourseReg = new DataTable("TBL_COURSE_REGISTRATION_BY_ADVISOR_BULK");
        StudCourseReg.Columns.Add("IDNO", typeof(int));
        StudCourseReg.Columns.Add("SEMESTERNO", typeof(int));
        StudCourseReg.Columns.Add("SESSIONNO", typeof(int));
        StudCourseReg.Columns.Add("SCHEMENO", typeof(int));
        StudCourseReg.Columns.Add("COURSENO", typeof(int));
        StudCourseReg.Columns.Add("CCODE", typeof(string));
        StudCourseReg.Columns.Add("COURSENAME", typeof(string));
        StudCourseReg.Columns.Add("CREDITS", typeof(decimal));
        StudCourseReg.Columns.Add("SUBID", typeof(int));
        StudCourseReg.Columns.Add("SRNO", typeof(int));

        foreach (ListViewDataItem dataitem in lvCourseRegistrationStudent.Items)
        {
            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            if (cbRow.Checked == true)
            {
                dr = StudCourseReg.NewRow();
                dr["IDNO"] = cbRow.ToolTip;
                dr["SEMESTERNO"] = ddlSemester.SelectedValue;
                dr["SESSIONNO"] = ddlSession.SelectedValue;
                dr["SCHEMENO"] = 0;
                dr["COURSENO"] = 0;
                dr["CCODE"] = "";
                dr["COURSENAME"] = "";
                dr["CREDITS"] = 0;
                dr["SUBID"] = 0;
                dr["SRNO"] = 0;

                StudCourseReg.Rows.Add(dr);
            }
        }

        objEntity.dtStudCourseReg = StudCourseReg;
        objEntity.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        objEntity.CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
        objEntity.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        objEntity.Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        objEntity.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        objEntity.EntryType = 2; //2 For Bulk Student Entry
        objEntity.IPAddress = ViewState["ipAddress"].ToString();
        objEntity.UA_NO = Convert.ToInt32(Session["userno"]);

        int res = objController.InsertUpdate_ApprovalStatus(objEntity);
        if (res > 0)
        {
            objCommon.DisplayMessage(this.updpnl_details, "Course Registration Approved Successfully.", this.Page);
            btnShow_Click(sender, e);
        }
    
    }

    #endregion Page Event

    #region User Define Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseRegistrationApproveByAdvisor.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseRegistrationApproveByAdvisor.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO>0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindSingleStudentCourseRegistrationDetail(int idno)
    {
        try
        {
            SqlDataReader dr = null;
            objEntity.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            objEntity.CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
            objEntity.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objEntity.Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            objEntity.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            objEntity.UA_NO = Convert.ToInt32(Session["userno"]);
            objEntity.IDNO = idno;

            dr = objController.GetSingleStudentCourseRegistrationDetail(objEntity);

            if (dr != null)
            {
                lstCourseDetail.DataSource = dr;
                lstCourseDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.BindSingleStudentCourseRegistrationDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        int ExitRecord = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT R WITH (NOLOCK) ON(S.IDNO=R.IDNO AND S.SEMESTERNO=R.SEMESTERNO AND S.SCHEMENO=R.SCHEMENO)", "COUNT(*)", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.COLLEGE_ID=" + ddlCollegeName.SelectedValue + " AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND S.FAC_ADVISOR=" + Convert.ToInt32(Session["userno"])+" AND ISNULL(R.REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0"));
       if (ExitRecord > 0)
       {
           ShowReport("Course_Registration_Approved_Detail", "rptCourseRegistrationApproveDetail.rpt");
       }
       else
       {
           objCommon.DisplayMessage(this.updpnl_details, "Course Registration Approved Record Not Found.!!!", this.Page);
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
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollegeName.SelectedValue) + ",@P_DEGREE_NO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH_NO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTER_NO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]);
      
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl_details, this.updpnl_details.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseRegistrationApproveByAdvisor.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion User Define Method
}