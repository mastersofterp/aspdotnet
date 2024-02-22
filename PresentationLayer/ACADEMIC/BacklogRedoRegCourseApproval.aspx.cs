using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_BacklogRedoRegCourseApproval : System.Web.UI.Page
{
    //string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();

    protected void Page_Load(object sender, EventArgs e)
    {
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
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Populate the DropDownList 
                PopulateDropDownList();
            }
            Session["reportdate"] = null;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Course_Registration_Approval.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Course_Registration_Approval.aspx");
        }
    }

    #region Dropdown list 
    private void PopulateDropDownList()
    {

        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN ACD_SESSION S ON(A.SESSIONID = S.SESSIONID) INNER JOIN ACD_COURSE_REG_CONFIG_ACTIVITY B ON A.SESSIONNO = B.SESSION_NO", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "B.STARTED = 1 AND B.CRS_ACTIVITY_NO =2 AND A.OrganizationId = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " ", "S.SESSIONID");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", " COLLEGE_ID IN (" + ddlCollege.SelectedValue + ") AND isnull(FLOCK,0)=1 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A INNER JOIN ACD_STUDENT_RESULT SR ON A.SESSIONNO=SR.SESSIONNO AND SR.RE_REGISTER=1 AND SR.PREV_STATUS=1 AND ISNULL(SR.CANCEL,0)=0",
            "DISTINCT A.SESSIONID", "A.SESSION_NAME", "", "A.SESSIONID DESC");
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", " COLLEGE_ID IN (" + ddlCollege.SelectedValue + ") AND isnull(FLOCK,0)=1 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE C ON D.DEGREENO=C.DEGREENO", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND C.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "DEGREENO");
            lvApproveCourse.DataSource = null;
            lvApproveCourse.DataBind();
            lvApproveCourse.Visible = false;
        }
        else
        {
            objCommon.DisplayMessage(uplReg, "Please Select School/Institute", this.Page);
            ddlCollege.Focus();
        }

       
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            lvApproveCourse.DataSource = null;
            lvApproveCourse.DataBind();
            lvApproveCourse.Visible = false;
          
            if (ddlDegree.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH AD ON ( B.BRANCHNO = AD.BRANCHNO )", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND AD.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND AD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));

            }
            else
            {
                objCommon.DisplayMessage(uplReg, "Please Select Branch", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER  WITH (NOLOCK)", "SEMESTERNO",
                "SEMESTERNAME",
                " SEMESTERNO > 0 AND  ACTIVESTATUS = 1 AND SEMESTERNO <=(SELECT DISTINCT DURATION_LAST_SEM FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) +
                " AND  BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + ")", "SEMESTERNO");
        }
        else
        {
            objCommon.DisplayMessage(uplReg, "Please Select Semester", this.Page);
            ddlBranch.Focus();
        }
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO<>0 and ACTIVESTATUS = 1", "");
           

            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlCollege, "ACD_SESSION_MASTER A INNER JOIN ACD_COLLEGE_MASTER CM ON(A.COLLEGE_ID = CM.COLLEGE_ID)",
                    "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME",
                    " A.OrganizationId = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])
                    + " AND A.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue)
                    + " ", "CM.COLLEGE_ID");
            else
            {
                string clg_Nos = objCommon.LookUp("USER_ACC", "DISTINCT ISNULL(UA_COLLEGE_NOS,0)AS UA_COLLEGE_NOS", "UA_TYPE=" + Session["usertype"].ToString() + " AND ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND UA_NO=" + Session["userno"].ToString());

                if (string.IsNullOrEmpty(clg_Nos))
                {
                    objCommon.DisplayMessage(uplReg, "College Not Defined to User!", this.Page);
                    return;
                }

                //objCommon.FillDropDownList(ddlCollege, "ACD_SESSION_MASTER A INNER JOIN ACD_COLLEGE_MASTER CM ON(A.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_COURSE_REG_CONFIG_ACTIVITY B ON A.SESSIONNO = B.SESSION_NO",
                //    "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME",
                //    "B.STARTED = 1 AND B.CRS_ACTIVITY_NO  =2 AND A.OrganizationId = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])
                //    + " AND A.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue)
                //    + " AND CM.COLLEGE_ID IN(" + clg_Nos + ")  ", "CM.COLLEGE_ID");

                objCommon.FillDropDownList(ddlCollege, "ACD_SESSION_MASTER A INNER JOIN ACD_COLLEGE_MASTER CM ON(A.COLLEGE_ID = CM.COLLEGE_ID) ",
                   "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME",
                   "A.OrganizationId = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])
                   + " AND A.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue)
                   + " AND CM.COLLEGE_ID IN(" + clg_Nos + ")  ", "CM.COLLEGE_ID");

            }          

            lvApproveCourse.DataSource = null;
            lvApproveCourse.DataBind();
            lvApproveCourse.Visible = false;
        }
        else
        {
            objCommon.DisplayMessage(uplReg, "Please Select Session!", this.Page);
            ddlSession.Focus();
        }

    }
    #endregion

    #region Show and Submit Event Functions
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int userno = 0;
            DataSet ds2 = objCommon.FillDropDown("ACD_GLOBAL_OFFERED_COURSE", "*", "", "", "GLOBAL_OFFER_ID");
            CourseController objCC = new CourseController();
            if (Session["userno"].ToString() != string.Empty)
                userno = int.Parse(Session["userno"].ToString());
            else
                Response.Redirect("~/default.aspx", false);
            CustomStatus cs = CustomStatus.Error;

            bool cbChecked = false;
            var StudIDNOs = string.Empty;
            foreach (ListViewDataItem dataitem in lvApproveCourse.Items)
            {
                CheckBox cbApprove = (CheckBox)dataitem.FindControl("cbApprove");
                if (cbApprove.Checked)
                {
                    Label IDNO = (Label)dataitem.FindControl("lblIDNO");
                    StudIDNOs = IDNO.ToolTip + "," + StudIDNOs;
                    cbChecked = true;
                    //break;
                }
            }
            //StudIDNOs = StudIDNOs.Length-1;
            if (cbChecked)
            {
                int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                int BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                int College_ID = Convert.ToInt32(ddlCollege.SelectedValue);
                //int OrgId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                string ipAddress = Request.ServerVariables["REMOTE_HOST"];
                cs = (CustomStatus)objCC.UpdateBacklogRedoCourseRegApproval(userno, SessionNo, DegreeNo, BranchNo, College_ID, StudIDNOs, ipAddress);
            }
            else
            {
                objCommon.DisplayMessage(uplReg, "Please Select at least One Student.", this.Page);
                return;
            }

            //foreach (ListViewDataItem dataitem in lvApproveCourse.Items)
            //{
            //    CheckBox cbApprove = (CheckBox)dataitem.FindControl("cbApprove");
            //    if (cbApprove.Checked)
            //    {
            //        Label IDNO = (Label)dataitem.FindControl("lblIDNO");
            //        Label StudentName = (Label)dataitem.FindControl("lblStudentName");
            //        Student objS = new Student();

            //        objS.Uano = userno;
            //        objS.IdNo = Convert.ToInt32(IDNO.ToolTip); //Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);                       
            //        objS.RegNo = IDNO.Text;
            //        objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            //        objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            //        objS.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            //        objS.College_ID = Convert.ToInt32(ddlCollege.SelectedValue);
            //        string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            //        cs = (CustomStatus)objCC.InsertUpdateBacklogRedoCourseRegApproval(objS, ipAddress);
            //    }
            //}

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                btnShow.Enabled = true;
                //btnSubmit.Enabled = false;
                objCommon.DisplayMessage(uplReg, "Backlog Redo Courses Approved successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(uplReg, "You don't have authority to approve the courses.", this.Page);               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_GLobal_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
           
            if (CheckValidation() == true) 
            {
                lvApproveCourse.Visible = false;

                CourseController objC = new CourseController();
                DataSet ds = objC.GetBacklogRedoCourseRegistrationApprvlList(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvApproveCourse.DataSource = ds;
                    lvApproveCourse.DataBind();
                    lvApproveCourse.Visible = true;
                    tblInfo.Visible = true;
                    btnSubmit.Enabled = true;
                }
                else 
                {
                    lvApproveCourse.DataSource = null;
                    lvApproveCourse.DataBind();
                   // dvStudentInfo.Visible = false;
                    tblInfo.Visible = false;
                    btnSubmit.Enabled = false;
                    objCommon.DisplayMessage(uplReg, "No Record Found", this.Page);
                }
                              
            }
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Course_Registration_Approval.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Validations
    protected void lvApproveCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label HODApprove = (Label)e.Item.FindControl("lblHODApproveStatus");
            Label DEANApprove = (Label)e.Item.FindControl("lblDeanApproveStatus");
            Label lblPayment = (Label)e.Item.FindControl("lblPayment");
            CheckBox cbApprove = (CheckBox)e.Item.FindControl("cbApprove");
            cbApprove.Checked = (HODApprove.Text == "Approved") ? true : false;
            cbApprove.Checked = (DEANApprove.Text == "Approved") ? true : false;

            if (Convert.ToInt16(Session["OrgId"]) != 2)
            {
                DEANApprove.Visible = false;
                lblPayment.Visible = false;
                lvApproveCourse.FindControl("thPayment").Visible = false;
                lvApproveCourse.FindControl("thDeanApprove").Visible = false;
                Label lblbb = (Label)lvApproveCourse.FindControl("thHODArrpve");
                lblbb.Text = "Approval Status";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
     
        lvApproveCourse.DataSource = null;
        lvApproveCourse.DataBind();
        lvApproveCourse.Visible = false;
        Response.Redirect(Request.Url.ToString());
    }

    public bool CheckValidation()
    {
        bool result = true;
        if (ddlCollege.SelectedValue == "0" && ddlCollege.SelectedValue != null)
        {
            result = false;
            objCommon.DisplayMessage(uplReg, "Please Select College !", this.Page);
            return false;
        }
        //if (ddlDepartment.SelectedValue == "0" && ddlDepartment.SelectedValue != null)
        //{
        //    result = false;
        //    objCommon.DisplayMessage(uplReg, "Please Select Department !", this.Page);
        //    return false;
        //}
        if (ddlSession.SelectedValue == "0" && ddlSession.SelectedValue != null)
        {
            result = false;
            objCommon.DisplayMessage(uplReg, "Please Select Session !", this.Page);
            return false;
        }
        if (ddlDegree.SelectedValue == "0" && ddlDegree.SelectedValue != null)
        {
            result = false;
            objCommon.DisplayMessage(uplReg, "Please Select Degree !", this.Page);
            return false;
        }
        if (ddlSemester.SelectedValue == "0" && ddlSemester.SelectedValue != null)
        {
            result = false;
            objCommon.DisplayMessage(uplReg, "Please Select Semester !", this.Page);
            return false;
        }

        return result;
    }
    #endregion

}