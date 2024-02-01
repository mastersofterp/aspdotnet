using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_WithheldEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Student_Acd objStudent = new Student_Acd();
    StudentController objSC = new StudentController();
    int idno = 0;

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
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));
            PopulateDropDownList();
            ViewState["action"] = "add";
            ViewState["Idno"] = null;
        }
        ddlSession.Focus();
        // btnShow.Visible = true;
        divMsg.InnerHtml = string.Empty;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=WithHeldEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=WithHeldEntry.aspx");
        }
    }


    // Populate DropDown List  
    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"]) + ") OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");





            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");



            //objCommon.FillDropDownList(ddlclgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "DISTINCT R.SESSIONNO", "SESSION_NAME +' - '+ C.COLLEGE_NAME SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSION_NAME DESC");
            //*objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            // objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlRemark, "ACD_WITHHELD_REASON WITH (NOLOCK)", "WITHHELDNO", "WITHHELD_NAME", string.Empty, "WITHHELDNO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_WithHeldEntry.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #region Fill DropDownList

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSem.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        // txtStudent.Text = " ";





        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");

            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
            btnShow.Visible = false;
        }
        else
        {
            btnShow.Visible = false;
            Clearall();
        }
        lvWithHeld.DataSource = null;
        lvWithHeld.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        // ddlSem.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        // txtStudent.Text = " ";

        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            // ddlSem.Items.Clear();
            // ddlSem.Items.Add(new ListItem("Please Select", "0"));
            btnShow.Visible = false;
            ddlScheme.Focus();
        }
        else
        {
            btnShow.Visible = false;
            //ddlSem.Items.Clear();
            //ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
        lvWithHeld.DataSource = null;
        lvWithHeld.DataBind();

    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSem.SelectedIndex = 0;
        //txtStudent.Text = " ";

        //if (ddlScheme.SelectedIndex > 0)
        //{
        //    ddlSem.Items.Clear();
        //    objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        //    ddlSem.Focus();
        //    btnShow.Visible = false;

        //}
        //else
        //{
        //    ddlSem.Items.Clear();
        //    ddlSem.Items.Add(new ListItem("Please Select", "0"));
        //    ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //    btnShow.Visible = false;
        //}
        lvWithHeld.DataSource = null;
        lvWithHeld.DataBind();
    }
    #endregion


    // Show The With Held Entry Details on Listview
    private void BindListView()
    {
        try
        {
            DataSet ds = objSC.GetWithHeldData(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvWithHeld.DataSource = ds;
                lvWithHeld.DataBind();
            }
            else
            {
                lvWithHeld.DataSource = null;
                lvWithHeld.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_WithHeldEntry.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Show details of WithHeld Entry
    private void ShowDetail(int idno, int sessionno)
    {
        SqlDataReader dr = objSC.GetWithHeldDetailsById(idno, sessionno);

        //Show WithHeld Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["IDNO"] = idno.ToString();
                ddlSession.Text = dr["SESSIONNO"] == DBNull.Value ? string.Empty : dr["SESSIONNO"].ToString();
                ddlSem.Text = dr["SEMESTERNO"] == DBNull.Value ? "0" : dr["SEMESTERNO"].ToString();
                txtStudent.Text = dr["REGNO"].ToString();
                lblDegreeName.Text = dr["DEGREENAME"].ToString();
                lblDegreeName.ToolTip = dr["DEGREENO"].ToString();
                lblBranch.Text = dr["BRANCHNAME"].ToString();
                lblClass.Text = dr["ROLL_NO"].ToString();
                lblSeatNo.Text = dr["SEATNO"].ToString();
                lblStudent.Text = dr["STUDNAME"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dr["SEMESTERNO"].ToString();
                lblScheme.Text = dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"].ToString();
                lblSection.Text = dr["SECTIONNAME"].ToString();
                lblSection.ToolTip = dr["SECTIONNO"].ToString();
                //txtStudent.ToolTip = dr["IDNO"].ToString();
                //ddlRemark.Text = dr["REMARK"] == DBNull.Value ? string.Empty : dr["REMARK"].ToString();
                ddlRemark.SelectedValue = dr["STATUS"] == DBNull.Value ? "0" : dr["STATUS"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    //Edit WithHeld Details
    protected void btnEditWithHeld_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int idno = int.Parse(btnEdit.CommandArgument);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            ViewState["Idno"] = idno;
            ShowDetail(idno, sessionno);
            ViewState["action"] = "edit";
            btnRelease.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_WithHeldEntry.btnEditWithHeld_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    // Add With Held Entry Details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlRemark.SelectedIndex > 0)
        {
            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStudent.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objStudent.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
            objStudent.DegreeNo = Convert.ToInt32(lblDegreeName.ToolTip.Trim());

            if (lblSeatNo.Text == "")
            {
                objStudent.Seatno = 0;
            }
            else
            {
                objStudent.Seatno = Convert.ToInt32(lblSeatNo.Text.Trim());
            }
            objStudent.StudName = lblStudent.Text.Trim();
            objStudent.SemesterNo = Convert.ToInt32(lblSemester.ToolTip.Trim());
            objStudent.SchemeNo = Convert.ToInt32(lblScheme.ToolTip.Trim());
            objStudent.Remark = ddlRemark.SelectedItem.Text;
            objStudent.Status = Convert.ToInt32(ddlRemark.SelectedValue);

            idno = Convert.ToInt32(objCommon.LookUp("ACD_WITHHELD WITH (NOLOCK)", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND IDNO=" + Convert.ToInt32(ViewState["idno"].ToString())));
            if (idno > 0 && ViewState["Idno"] == null)
            {
                objCommon.DisplayMessage(this.updWithheld, "Record Already Available", this.Page);
                return;

            }
            else
            {

                //if (ViewState["action"] != null)
                //{
                //    if (idno > 0 && ViewState["action"].Equals("add"))
                //    {
                //        objCommon.DisplayMessage(this.updWithheld,"Record Already Available", this.Page);
                //        return;
                //    }
                //    else
                //    {
                //Add
                //if (ViewState["action"].ToString().Equals("add"))  ViewState["Idno"]
                if (ViewState["Idno"] == null)
                {
                    CustomStatus cs = (CustomStatus)objSC.AddWithHeld(objStudent);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updWithheld, "Record Save successfully", this.Page);
                        //BindListView();
                        ViewState["action"] = "add";
                        Clearall();
                    }
                }
                else
                //Edit

                //if (ViewState["action"].ToString().Equals("edit"))
                //{
                {
                    objStudent.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
                    CustomStatus cs = (CustomStatus)objSC.UpadteWithHeldEntryStatus(objStudent);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ViewState["Idno"] = null;
                        objCommon.DisplayMessage(this.updWithheld, "Record Update successfully", this.Page);
                        //BindListView();
                        ViewState["action"] = "add";
                        Clearall();
                    }
                }
            }
        }
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.updWithheld,"Please Select Reason For Withheld", this.Page);
        //}
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {


        try
        {
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            //DataSet ds = objSC.GetWithHeldReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["idno"]));
            DataSet ds = objSC.GetWithHeldReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updWithheld, this.updWithheld.GetType(), "controlJSScript", sb.ToString(), true);
            }

            else
            {

                objCommon.DisplayMessage(this.updWithheld, "Report Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_WithHeldEntry.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Show Student Details
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = objSC.GetStudentIDByRegNo(txtStudent.Text.Trim());
            if (idno == 0)
            {
                lblDegreeName.Text = string.Empty;
                lblMsg.Text = "Student Not Found!!";
                objCommon.DisplayMessage(this.updWithheld, "Student Not Found!!", this.Page);
                ddlSession.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                txtStudent.Text = string.Empty;
                lblSemester.Text = string.Empty;
                lblScheme.Text = string.Empty;
                lvWithHeld.DataSource = null;
                pnlStudInfo.Visible = false;
                btnRelease.Visible = false;

                return;
            }
            else
            {




                lblDegreeName.Text = string.Empty;
                lblBranch.Text = string.Empty;
                lblClass.Text = string.Empty;

                lblStudent.Text = string.Empty;
                lblSemester.Text = string.Empty;
                lblScheme.Text = string.Empty;
                lblSection.Text = string.Empty;
                ddlRemark.SelectedIndex = 0;
                lblSeatNo.Text = string.Empty;
                //rfvSession.Visible = true;
                //rfvSemester.Visible = true;
                //rfvRegNo.Visible = true;
                ViewState["idno"] = idno;
                pnlStudInfo.Visible = true;
                lvWithHeld.Visible = true;
                btnRelease.Visible = false;

                //Show Student Data
                DataTableReader dtr = objSC.GetWithHeldStudentDetails(idno, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));

                if (dtr.Read())
                {
                    lblDegreeName.Text = dtr["DEGREENAME"].ToString();
                    lblDegreeName.ToolTip = dtr["DEGREENO"].ToString();
                    lblBranch.Text = dtr["BRANCHNAME"].ToString();
                    lblClass.Text = dtr["ROLL_NO"].ToString();
                    lblSeatNo.Text = dtr["SEATNO"].ToString();
                    lblStudent.Text = dtr["STUDNAME"].ToString();
                    lblSemester.Text = dtr["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
                    lblScheme.Text = dtr["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dtr["SCHEMENO"].ToString();
                    lblSection.Text = dtr["SECTIONNAME"].ToString();
                    lblSection.ToolTip = dtr["SECTIONNO"].ToString();
                    lblMsg.Text = string.Empty;
                    BindListView();
                    ViewState["action"] = "add";
                }
                else
                {
                    pnlStudInfo.Visible = false;
                    lvWithHeld.DataSource = null;
                    lvWithHeld.DataBind();
                    objCommon.DisplayMessage(this.updWithheld, "No Record Found For Withheld Entry...", this.Page);
                }
                dtr.Close();

                //objCommon.FillDropDownList(ddlRemark, "ACD_STUDENT_RESULT SR,ACD_COURSE CS", "SR.COURSENO", "CS.CCODE+'-'+CS.COURSE_NAME", "CS.COURSENO > 0 AND SR.COURSENO=CS.COURSENO AND SR.SEMESTERNO=CS.SEMESTERNO AND SR.CCODE=CS.CCODE AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.IDNO=" + Convert.ToInt32(idno), "CS.CCODE");


            }



        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_WithHeldEntry.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clearall()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        txtStudent.Text = string.Empty;
        lblDegreeName.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblClass.Text = string.Empty;
        lblStudent.Text = string.Empty;
        lblSemester.Text = string.Empty;
        lblScheme.Text = string.Empty;
        lblSection.Text = string.Empty;
        ddlRemark.SelectedIndex = 0;
        // txtPunishment.Text = string.Empty;
        lblSeatNo.Text = string.Empty;
        pnlStudInfo.Visible = false;
        lvWithHeld.DataSource = null;
        lvWithHeld.DataBind();





    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ViewState["action"] = null;
    }

    protected void rdoWithHeld_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStudent.Text = "";
        ddlSem.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        regno.Visible = false;



        if (rdoWithHeld.SelectedValue == "2")
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "DISTINCT R.SESSIONNO", "SESSION_NAME +' - '+ C.COLLEGE_NAME SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSION_NAME DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            pnlRpt.Visible = true;
            rfvRegNo.Visible = false;
            pnlStudInfo.Visible = false;
            lvWithHeld.DataSource = null;
            lvWithHeld.DataBind();
            btnReport.Visible = true;
            btnShow.Visible = false;

            txtStudent.Text = string.Empty;
            //rfvSemester.Visible = false;
        }
        else if (rdoWithHeld.SelectedValue == "1")
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "FLOCK > 0", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "DISTINCT R.SESSIONNO", "SESSION_NAME +' - '+ C.COLLEGE_NAME SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSION_NAME DESC");
            pnlRpt.Visible = false;
            rfvRegNo.Visible = true;
            pnlStudInfo.Visible = false;
            btnShow.Visible = true;
            btnReport.Visible = false;
            regno.Visible = true;
            lvWithHeld.DataSource = null;
            lvWithHeld.DataBind();
        }

        else
        {

            objCommon.DisplayMessage(this.updWithheld, "Please Select Action", this.Page);

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        lvWithHeld.DataSource = null;
        lvWithHeld.DataBind();
        //string idno = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "(REGNO='"+txtStudent.Text.Trim()+"' AND REGNO !='')");
        if (rdoWithHeld.SelectedValue == "2")
        {
            btnShow.Visible = false;
            try
            {
                //ShowReportBlankTR("MSE_RESULT", "rptNewConsolidatedMarkReport.rpt");
                //ShowReport(idno, "WithHeldEntry", "rptWithHeldEntryReport.rpt");
                ShowReport("WithHeldEntry", "rptWithHeldEntryReport.rpt");
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_WithHeldEntry.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        else
        {
            btnShow.Visible = true;
            Clearall();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        // pnlRpt.Visible = true;
        //ddlSession.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        //ddlSem.SelectedIndex = 0;
        //txtStudent.Text = string.Empty;
        //if (Convert.ToInt32(rdoWithHeld.SelectedValue) > 1)
        //{
        //    btnShow.Visible = false;
        //}
        //else
        //{
        //    btnShow.Visible = true;

        //}
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSem.Items.Clear();
        //ddlSem.Items.Add(new ListItem("Please Select","0"));


        pnlStudInfo.Visible = false;
        ddlSem.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        // btnShow.Visible = false;
        lvWithHeld.Visible = false;

        txtStudent.Text = " ";


        if (ddlSession.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue, "S.SEMESTERNO");

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

            ddlSession.Focus();

        }


    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStudent.Text = " ";


    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {
            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            }
        }
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        txtStudent.Text = string.Empty;
        pnlStudInfo.Visible = false;
        lvWithHeld.Visible = false;



    }
    protected void btnRelease_Click(object sender, EventArgs e)
    {


        objStudent.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
        objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objStudent.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        CustomStatus cs = (CustomStatus)objSC.UpadteWithHeldEntryRelease(objStudent);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            ViewState["Idno"] = null;
            objCommon.DisplayMessage(this.updWithheld, "Record Release successfully", this.Page);
            //BindListView();
            ViewState["action"] = "add";
            Clearall();
        }


    }
}
