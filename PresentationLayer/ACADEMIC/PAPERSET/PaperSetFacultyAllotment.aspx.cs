
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PAPER SET 
// CREATION DATE : 30-08-2012
// ADDED BY      : PRIYANKA KABADE                                                  
// ADDED DATE    : 
// MODIFIED BY   : 
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_PaperSetFacultyAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region PAGE EVENTS
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
                ////Page Authorization
              //  CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
               // if (Request.QueryString["pageno"] != null)
                    /////lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

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
                
            }
            divMsg.InnerHtml = string.Empty;
        }
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaperSetFacultyAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaperSetFacultyAllotment.aspx");
        }
    }
    #endregion

    #region Private Method

    private void BindListView()
    {

        ////DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)LEFT OUTER JOIN ACD_STAFF S ON(P.FACULTY1 = S.STAFFNO OR P.FACULTY2 = S.STAFFNO OR P.FACULTY3 = S.STAFFNO)INNER JOIN ACD_SCHEME SCH ON (SCH.SCHEMENO = C.SCHEMENO)INNER JOIN ACD_SCHEMETYPE ST ON (ST.SCHEMETYPENO = SCH.SCHEMETYPE)", "DISTINCT P.CCODE", "(SELECT SCHEMETYPE FROM ACD_SCHEMETYPE WHERE SCHEMETYPENO > 0 AND SCHEMETYPENO IN (SELECT SCHEMETYPE FROM ACD_SCHEME WHERE SCHEMENO = C.SCHEMENO))SCHEMETYPE,COURSE_NAME,P.FACULTY1,P.FACULTY2,P.FACULTY3,ISNULL(QT1,REQ_LEVEL)QT1,MOI1,ISNULL(QT2,REQ_LEVEL)QT2,MOI2,MOI3,ISNULL(QT3,REQ_LEVEL)QT3,BOS_LOCK,DEAN_LOCK,APPROVED,DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "(P.CANCEL IS NULL OR P.CANCEL = 0) AND (S.CANCEL IS NULL OR S.CANCEL = 0) AND P.BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND P.SEMESTERNO = " + ddlSemester.SelectedValue + " AND MAXMARKS_E > 0 AND SUBID IN (1,3) AND QTY <> 0  AND P.SESSIONNO = " + ddlSession.SelectedValue, "P.CCODE");
        //DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO=C.SEMESTERNO)LEFT OUTER JOIN ACD_STAFF S ON(P.FACULTY1 = S.STAFFNO OR P.FACULTY2 = S.STAFFNO OR P.FACULTY3 = S.STAFFNO)INNER JOIN ACD_SCHEME SCH ON (SCH.SCHEMENO = C.SCHEMENO)INNER JOIN ACD_SCHEMETYPE ST ON (ST.SCHEMETYPENO = SCH.SCHEMETYPE)", "DISTINCT P.CCODE", "(SELECT SCHEMETYPE FROM ACD_SCHEMETYPE WHERE SCHEMETYPENO > 0 AND SCHEMETYPENO IN (SELECT SCHEMETYPE FROM ACD_SCHEME WHERE SCHEMENO = C.SCHEMENO))SCHEMETYPE,COURSE_NAME,P.FACULTY1,P.FACULTY2,P.FACULTY3,ISNULL(QT1,REQ_LEVEL)QT1,MOI1,ISNULL(QT2,REQ_LEVEL)QT2,MOI2,MOI3,ISNULL(QT3,REQ_LEVEL)QT3,BOS_LOCK,DEAN_LOCK,APPROVED,DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "(P.CANCEL IS NULL OR P.CANCEL = 0) AND (S.CANCEL IS NULL OR S.CANCEL = 0) AND P.BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND P.SEMESTERNO = " + ddlSemester.SelectedValue + " AND MAXMARKS_E > 0 AND SUBID IN (1,3) AND QTY <> 0  AND P.SESSIONNO = " + ddlSession.SelectedValue, "P.CCODE");
       // DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO=C.SEMESTERNO)LEFT OUTER JOIN ACD_STAFF S ON(P.FACULTY1 = S.STAFFNO OR P.FACULTY2 = S.STAFFNO OR P.FACULTY3 = S.STAFFNO)INNER JOIN ACD_SCHEME SCH ON (SCH.SCHEMENO = C.SCHEMENO)INNER JOIN ACD_SCHEMETYPE ST ON (ST.SCHEMETYPENO = SCH.SCHEMETYPE) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT P.CCODE", "(SELECT SCHEMETYPE FROM ACD_SCHEMETYPE WHERE SCHEMETYPENO > 0 AND SCHEMETYPENO IN (SELECT SCHEMETYPE FROM ACD_SCHEME WHERE SCHEMENO = C.SCHEMENO))SCHEMETYPE,COURSE_NAME,P.FACULTY1,P.FACULTY2,P.FACULTY3,ISNULL(QT1,REQ_LEVEL)QT1,MOI1,ISNULL(QT2,REQ_LEVEL)QT2,MOI2,MOI3,ISNULL(QT3,REQ_LEVEL)QT3,BOS_LOCK,DEAN_LOCK,APPROVED,DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "(P.CANCEL IS NULL OR P.CANCEL = 0) AND (S.CANCEL IS NULL OR S.CANCEL = 0) AND P.BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND P.SEMESTERNO = " + ddlSemester.SelectedValue + " AND MAXMARKS_E > 0 AND SUBID IN (1,3) AND SM.SESSIONID = " + ddlSession.SelectedValue, "P.CCODE");
        DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO=C.SEMESTERNO)LEFT OUTER JOIN ACD_STAFF S ON(P.FACULTY1 = S.STAFFNO OR P.FACULTY2 = S.STAFFNO OR P.FACULTY3 = S.STAFFNO)INNER JOIN ACD_SCHEME SCH ON (SCH.SCHEMENO = C.SCHEMENO)INNER JOIN ACD_SCHEMETYPE ST ON (ST.SCHEMETYPENO = SCH.SCHEMETYPE) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT P.CCODE", "(SELECT SCHEMETYPE FROM ACD_SCHEMETYPE WHERE SCHEMETYPENO > 0 AND SCHEMETYPENO IN (SELECT SCHEMETYPE FROM ACD_SCHEME WHERE SCHEMENO = C.SCHEMENO))SCHEMETYPE,COURSE_NAME,P.FACULTY1,P.FACULTY2,P.FACULTY3,ISNULL(QT1,REQ_LEVEL)QT1,MOI1,ISNULL(QT2,REQ_LEVEL)QT2,MOI2,MOI3,ISNULL(QT3,REQ_LEVEL)QT3,BOS_LOCK,DEAN_LOCK,APPROVED,DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "(P.CANCEL IS NULL OR P.CANCEL = 0) AND (S.CANCEL IS NULL OR S.CANCEL = 0) AND C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND P.SEMESTERNO = " + ddlSemester.SelectedValue + " AND MAXMARKS_E > 0 AND SUBID IN (1,3) AND SM.SESSIONID = " + ddlSession.SelectedValue, "P.CCODE");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSave.Visible = true;
                btnLock.Visible = true;
                btnCancel.Visible = true;

                btnSave.Enabled = false;
                btnLock.Enabled = false;
                btnCancel.Enabled = false;

                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                lvCourse.Visible = true;

            }
            else
            {
                lvCourse.Visible = false;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnLock.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnLock.Visible = false;
        }
    }

    private void SaveAndLock(bool bos_lock)
    {
        CourseController objCCont = new CourseController();
        int cnt_total = 0;
        if (bos_lock)
        {
            foreach (ListViewItem item in lvCourse.Items)
            {
                Label lblCCode = item.FindControl("lblCCode") as Label;
                DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
                DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
                DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;

                TextBox txQt1 = item.FindControl("txQt1") as TextBox;
                TextBox txtMOI1 = item.FindControl("txtMOI1") as TextBox;
                TextBox txQt2 = item.FindControl("txQt2") as TextBox;
                TextBox txtMOI2 = item.FindControl("txtMOI2") as TextBox;
                TextBox txQt3 = item.FindControl("txQt3") as TextBox;
                TextBox txtMOI3 = item.FindControl("txtMOI3") as TextBox;
                int ret = 0;
                int count = 0;

                if (ddlFaculty1.Enabled && ddlFaculty2.Enabled && ddlFaculty3.Enabled)
                {
                    //if (ddlFaculty1.SelectedIndex > 0)
                    //{
                    //    if (txQt1.Text == "" || txtMOI1.Text == "")
                    //    {
                    //        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quanties & MOI for the paper setter 1 ", this.Page);
                    //        return;
                    //    }
                    //    count++;
                    //}
                    //else
                    //{
                    //    txQt1.Text = "0";
                    //    txtMOI1.Text = "0";
                    //}
                    //if (ddlFaculty2.SelectedIndex > 0)
                    //{
                    //    if (txQt2.Text == "" || txtMOI2.Text == "")
                    //    {
                    //        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quanties & MOI for the paper setter 2", this.Page);
                    //        return;
                    //    }
                    //    count++;
                    //}
                    //else
                    //{
                    //    txQt2.Text = "0";
                    //    txtMOI2.Text = "0";
                    //}

                    //if (ddlFaculty3.SelectedIndex > 0)
                    //{
                    //    if (txQt3.Text == "" || txtMOI3.Text == "")
                    //    {
                    //        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quanties & MOI for the paper setter 3", this.Page);
                    //        return;
                    //    }
                    //    count++;

                    //}
                    //else
                    //{
                    //    txQt3.Text = "0";
                    //    txtMOI3.Text = "0";
                    //}
                    //if (count == 0)
                    //{
                    //    objCommon.DisplayMessage(this.updFacAllot, "Please select atleast one paper setter & fill the quantity,MOI for each course", this.Page);
                    //    return;
                    //}

                    if (ddlFaculty1.SelectedIndex > 0)
                    {
                        if (txQt1.Text == "")
                        {
                            objCommon.DisplayMessage(this.updFacAllot, "Please fill the quantity for the paper setter 1 ", this.Page);
                            return;
                        }
                        count++;
                    }
                    else
                    {
                        txQt1.Text = "0";
                        txtMOI1.Text = "0";
                    }
                    if (ddlFaculty2.SelectedIndex > 0)
                    {
                        if (txQt2.Text == "")
                        {
                            objCommon.DisplayMessage(this.updFacAllot, "Please fill the quantity for the paper setter 2", this.Page);
                            return;
                        }
                        count++;
                    }
                    else
                    {
                        txQt2.Text = "0";
                        txtMOI2.Text = "0";
                    }

                    if (ddlFaculty3.SelectedIndex > 0)
                    {
                        if (txQt3.Text == "")
                        {
                            objCommon.DisplayMessage(this.updFacAllot, "Please fill the quantity for the paper setter 3", this.Page);
                            return;
                        }
                        count++;

                    }
                    else
                    {
                        txQt3.Text = "0";
                        txtMOI3.Text = "0";
                    }
                    //if (count == 0)
                    //{
                    //    objCommon.DisplayMessage(this.updFacAllot, "Please select atleast one paper setter & fill the quantity for each course", this.Page);
                    //    return;
                    //}
                }
            }
        }

        foreach (ListViewItem item in lvCourse.Items)
        {
            Label lblCCode = item.FindControl("lblCCode") as Label;
            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;

            TextBox txQt1 = item.FindControl("txQt1") as TextBox;
            TextBox txtMOI1 = item.FindControl("txtMOI1") as TextBox;
            TextBox txQt2 = item.FindControl("txQt2") as TextBox;
            TextBox txtMOI2 = item.FindControl("txtMOI2") as TextBox;
            TextBox txQt3 = item.FindControl("txQt3") as TextBox;
            TextBox txtMOI3 = item.FindControl("txtMOI3") as TextBox;
            int ret = 0;
            int count = 0;
            if (ddlFaculty1.Enabled && ddlFaculty2.Enabled && ddlFaculty3.Enabled)
            {
                //if (ddlFaculty1.SelectedIndex > 0)
                //{
                //    if (txQt1.Text == "" || txtMOI1.Text == "")
                //    {
                //        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quanties & MOI for the paper setter 1 ", this.Page);
                //        return;
                //    }
                //    count++;
                //}
                //else
                //{
                //    txQt1.Text = "0";
                //    txtMOI1.Text = "0";
                //}
                //if (ddlFaculty2.SelectedIndex > 0)
                //{
                //    if (txQt2.Text == "" || txtMOI2.Text == "")
                //    {
                //        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quanties & MOI for the paper setter 2", this.Page);
                //        return;
                //    }
                //    count++;
                //}
                //else
                //{
                //    txQt2.Text = "0";
                //    txtMOI2.Text = "0";
                //}

                //if (ddlFaculty3.SelectedIndex > 0)
                //{
                //    if (txQt3.Text == "" || txtMOI3.Text == "")
                //    {
                //        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quanties & MOI for the paper setter 3", this.Page);
                //        return;
                //    }
                //    count++;

                //}
                //else
                //{
                //    txQt3.Text = "0";
                //    txtMOI3.Text = "0";
                //}
                //if (count > 0)
                //{
                //    cnt_total++;
                //    ret = objCCont.AddPaperSetFaculty(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), lblCCode.Text, Convert.ToInt16(ddlFaculty1.SelectedValue), Convert.ToInt16(txQt1.Text), Convert.ToInt16(txtMOI1.Text), Convert.ToInt16(ddlFaculty2.SelectedValue), Convert.ToInt16(txQt2.Text), Convert.ToInt16(txtMOI2.Text), Convert.ToInt16(ddlFaculty3.SelectedValue), Convert.ToInt16(txQt3.Text), Convert.ToInt16(txtMOI3.Text), bos_lock);
                //    if (ret == -99)
                //    {
                //        objCommon.DisplayMessage("Data Not Saved.", this.Page);
                //        cnt_total--;
                //        return;
                //    }
                //}

                if (ddlFaculty1.SelectedIndex > 0)
                {
                    if (txQt1.Text == "")
                    {
                        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quantity for the paper setter 1 ", this.Page);
                        return;
                    }
                    count++;
                    txtMOI1.Text = "0";
                }
                else
                {
                    txQt1.Text = "0";
                    txtMOI1.Text = "0";
                }
                if (ddlFaculty2.SelectedIndex > 0)
                {
                    if (txQt2.Text == "")
                    {
                        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quantity for the paper setter 2", this.Page);
                        return;
                    }
                    count++;
                    txtMOI2.Text = "0";
                }
                else
                {
                    txQt2.Text = "0";
                    txtMOI2.Text = "0";
                }

                if (ddlFaculty3.SelectedIndex > 0)
                {
                    if (txQt3.Text == "")
                    {
                        objCommon.DisplayMessage(this.updFacAllot, "Please fill the quantity for the paper setter 3", this.Page);
                        return;
                    }
                    count++;
                    txtMOI3.Text = "0";
                }
                else
                {
                    txQt3.Text = "0";
                    txtMOI3.Text = "0";
                }
                if (count > 0)
                {
                    cnt_total++;
                    int Deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"])));
                    int collegeId = Convert.ToInt32(ViewState["college_id"]);
                    ret = objCCont.AddPaperSetFaculty(Convert.ToInt16(ddlSession.SelectedValue), Deptno, Convert.ToInt16(ddlSemester.SelectedValue), lblCCode.Text, Convert.ToInt16(ddlFaculty1.SelectedValue), Convert.ToInt16(txQt1.Text), Convert.ToInt16(txtMOI2.Text), Convert.ToInt16(ddlFaculty2.SelectedValue), Convert.ToInt16(txQt2.Text), Convert.ToInt16(txtMOI2.Text), Convert.ToInt16(ddlFaculty3.SelectedValue), Convert.ToInt16(txQt3.Text), Convert.ToInt16(txtMOI3.Text), bos_lock, collegeId);
                    if (ret == -99)
                    {
                        objCommon.DisplayMessage("Data Not Saved.", this.Page);
                        cnt_total--;
                        return;
                    }
                }
            }
        }

        BindListView();
        if (bos_lock)
            if (cnt_total > 0)
                objCommon.DisplayMessage(this.updFacAllot, "Paper Setters Allotment Locked", this.Page);
            else
                objCommon.DisplayMessage(this.updFacAllot, "Please select atleast one paper setter for all courses & then lock!", this.Page);

        else
            if (cnt_total > 0)
                objCommon.DisplayMessage(this.updFacAllot, "Paper Setters Alloted!", this.Page);
            else
                objCommon.DisplayMessage(this.updFacAllot, "Please select atleast fields for one course to save!", this.Page);

    }


    #endregion

    #region Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAndLock(false);
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        SaveAndLock(true);
        btnShow_Click(null, null);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                BindListView();
            }
            else
            {
                objCommon.DisplayMessage(this.updFacAllot, "Please Select Semester", this.Page);
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;

            }
        }
        else
        {

            objCommon.DisplayMessage(this.updFacAllot, "Please Select College & Scheme", this.Page);
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

        //ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnLock.Visible = false;
    }
    #endregion

    #region Other Events

    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlScheme.SelectedIndex > 0)
    //    {
    //        // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND P.BOS_DEPTNO =" + ddlDepartment.SelectedValue + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue), " S.SEMESTERNO");
    //        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_COURSE C ON (C.CCODE = P.CCODE AND C.BOS_DEPTNO = P.BOS_DEPTNO)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND C.SCHEMENO =" + ddlScheme.SelectedValue + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue), " S.SEMESTERNO");
    //    }
    //    else
    //        ddlSemester.SelectedIndex = 0;

    //    lvCourse.DataSource = null;
    //    lvCourse.DataBind();
    //    lvCourse.Visible = false;
    //    btnSave.Visible = false;
    //    btnCancel.Visible = false;
    //    btnLock.Visible = false;
    //}
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;

        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnLock.Visible = false;
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblCCode = e.Item.FindControl("lblCCode") as Label;

        //DROPDOWNS TO FILL
        DropDownList ddlFaculty1 = e.Item.FindControl("ddlFaculty1") as DropDownList;
        DropDownList ddlFaculty2 = e.Item.FindControl("ddlFaculty2") as DropDownList;
        DropDownList ddlFaculty3 = e.Item.FindControl("ddlFaculty3") as DropDownList;

        TextBox txQt1 = e.Item.FindControl("txQt1") as TextBox;
        TextBox txQt2 = e.Item.FindControl("txQt2") as TextBox;
        TextBox txQt3 = e.Item.FindControl("txQt3") as TextBox;

        TextBox txtMOI1 = e.Item.FindControl("txtMOI1") as TextBox;
        TextBox txtMOI2 = e.Item.FindControl("txtMOI2") as TextBox;
        TextBox txtMOI3 = e.Item.FindControl("txtMOI3") as TextBox;

        //PREVIOUS ALLOTMENT IF DONE
        HiddenField hffaculty1 = e.Item.FindControl("hffaculty1") as HiddenField;
        HiddenField hffaculty2 = e.Item.FindControl("hffaculty2") as HiddenField;
        HiddenField hffaculty3 = e.Item.FindControl("hffaculty3") as HiddenField;

        //LOCK STATUS
        HiddenField hfBosLock = e.Item.FindControl("hfBosLock") as HiddenField;
        HiddenField hfDeanLock = e.Item.FindControl("hfDeanLock") as HiddenField;
        string staffno1 = string.Empty;
        string staffno2 = string.Empty;
        string staffno3 = string.Empty;

        string ua_no1 = "0", ua_no2 = "0", ua_no3 = "0";

        string cancel = objCommon.LookUp("ACD_PAPERSET_DETAILS", "ISNULL(APPROVED,0)", "CANCEL = 1 AND CCODE = '" + lblCCode.Text + "' AND SESSIONNO = " + ddlSession.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue);
        cancel = cancel == "" ? "0" : cancel;

        if (hfBosLock.Value.ToLower() == "true")
        {
            //objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO = " + hffaculty1.Value, string.Empty);
            objCommon.FillDropDownList(ddlFaculty1, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFFNO <> " + cancel + " AND (S.STAFFNO = " + hffaculty1.Value + " OR " + hffaculty1.Value + "= 0)  AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            //objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFFNO <> " + cancel + " AND (S.STAFFNO = " + hffaculty2.Value + " OR " + hffaculty2.Value + "= 0)  AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFFNO <> " + cancel + " AND (S.STAFFNO = " + hffaculty2.Value + " OR " + hffaculty2.Value + "= 0)  AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            //objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFFNO <> " + cancel + " AND (S.STAFFNO = " + hffaculty3.Value + " OR " + hffaculty3.Value + "= 0)  AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFFNO <> " + cancel + " AND (S.STAFFNO = " + hffaculty3.Value + " OR " + hffaculty3.Value + "= 0)  AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);

            txQt1.Enabled = false;
            txQt2.Enabled = false;
            txQt3.Enabled = false;

            txtMOI1.Enabled = false;
            txtMOI2.Enabled = false;
            txtMOI3.Enabled = false;

        }
        else
        {
            // objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NOT NULL AND UA_NO > 0)AND UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0", string.Empty);
            objCommon.FillDropDownList(ddlFaculty1, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO <> " + cancel + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO <> " + cancel + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO <> " + cancel + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
            ////objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + ")", "S.STAFF_NAME");
            ////objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + ")", "S.STAFF_NAME");

            txQt1.Enabled = true;
            txQt2.Enabled = true;
            txQt3.Enabled = true;

            txtMOI1.Enabled = true;
            txtMOI2.Enabled = true;
            txtMOI3.Enabled = true;
        }
        //SELECTED UANO IS PREVIOUS VALUE ALLOTED OR THE TOP 1 OF THE ALLOTED FACULTY
        if (hffaculty1.Value != "" && hffaculty1.Value != cancel)
            ua_no1 = hffaculty1.Value;
        else
            ua_no1 = objCommon.LookUp("ACD_STAFF P INNER JOIN ACD_STUDENT_RESULT SR ON P.UA_NO = SR.UA_NO LEFT OUTER JOIN ACD_PAPERSET_DETAILS PD ON (PD.APPROVED = P.STAFFNO AND PD.CANCEL = 1 AND PD.CCODE = SR.CCODE AND PD.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT STAFFNO", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND (P.CANCEL IS NULL OR P.CANCEL = 0) AND SR.UA_NO > 0 AND SR.CCODE = '" + lblCCode.Text + "' AND (P.CANCEL = 0 OR P.CANCEL IS NULL) AND SR.SEMESTERNO = " + ddlSemester.SelectedValue);

        if (hffaculty2.Value != "" && hffaculty2.Value != cancel)
            ua_no2 = hffaculty2.Value;
        //else
        //    ua_no2 = objCommon.LookUp("ACD_STAFF P INNER JOIN ACD_STUDENT_RESULT SR ON P.UA_NO = SR.UA_NO LEFT OUTER JOIN ACD_PAPERSET_DETAILS PD ON (PD.APPROVED = P.STAFFNO AND PD.CANCEL = 1 AND PD.CCODE = SR.CCODE AND PD.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT STAFFNO", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND (P.CANCEL IS NULL OR P.CANCEL = 0) AND SR.UA_NO > 0 AND SR.CCODE = '" + lblCCode.Text + "' AND (P.CANCEL = 0 OR P.CANCEL IS NULL) AND SR.SEMESTERNO = " + ddlSemester.SelectedValue);

        if (hffaculty3.Value != "" && hffaculty3.Value != cancel)
            ua_no3 = hffaculty3.Value;
        //else
        //    ua_no3 = objCommon.LookUp("ACD_STAFF P INNER JOIN ACD_STUDENT_RESULT SR ON P.UA_NO = SR.UA_NO LEFT OUTER JOIN ACD_PAPERSET_DETAILS PD ON (PD.APPROVED = P.STAFFNO AND PD.CANCEL = 1 AND PD.CCODE = SR.CCODE AND PD.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT STAFFNO", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND (P.CANCEL IS NULL OR P.CANCEL = 0) AND SR.UA_NO > 0 AND SR.CCODE = '" + lblCCode.Text + "' AND (P.CANCEL = 0 OR P.CANCEL IS NULL) AND SR.SEMESTERNO = " + ddlSemester.SelectedValue);


        if (hfBosLock.Value.ToLower() == "true" || hfDeanLock.Value.ToLower() == "true")
        {
            ddlFaculty1.SelectedValue = ua_no1;
            ddlFaculty2.SelectedValue = ua_no2;
            ddlFaculty3.SelectedValue = ua_no3;

            ddlFaculty1.Enabled = false;
            ddlFaculty2.Enabled = false;
            ddlFaculty3.Enabled = false;

        }
        else
        {
            //ua_no1 = ua_no1 == "" ? ddlFaculty1.Items.Count > 1 ? ddlFaculty1.Items[1].Value : ddlFaculty1.Items[0].Value : ua_no1;
            ddlFaculty1.SelectedValue = ua_no1;
            ddlFaculty2.SelectedValue = ua_no2;
            ddlFaculty3.SelectedValue = ua_no3;

            //if (ua_no2 != "0")
            //{
            //    ddlFaculty1.Items.Remove(ddlFaculty2.SelectedItem);
            //    ddlFaculty3.Items.Remove(ddlFaculty2.SelectedItem);
            //}
            //if (ua_no3 != "0")
            //{
            //    ddlFaculty1.Items.Remove(ddlFaculty3.SelectedItem);
            //    ddlFaculty2.Items.Remove(ddlFaculty3.SelectedItem);
            //}
            //if (ua_no1 != "0" && ua_no1 != "")
            //{
            //    ddlFaculty2.Items.Remove(ddlFaculty1.SelectedItem);
            //    ddlFaculty3.Items.Remove(ddlFaculty1.SelectedItem);
            //}
            ddlFaculty1.Enabled = true;
            ddlFaculty2.Enabled = true;
            ddlFaculty3.Enabled = true;

            btnSave.Visible = true;
            btnLock.Visible = true;
            btnCancel.Visible = true;

            btnSave.Enabled = true;
            btnLock.Enabled = true;
            btnCancel.Enabled = true;
        }
    }


    protected void ddlFaculty1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
            Label lblCCode = item.FindControl("lblCCode") as Label;


            if (sender.GetHashCode() == ddlFaculty1.GetHashCode())
            {
                string ua_no2 = ddlFaculty2.SelectedValue;
                string ua_no3 = ddlFaculty3.SelectedValue;

                ////objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND S.STAFFNO !=" + ddlFaculty1.SelectedValue, "S.STAFF_NAME");
                ////objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND S.STAFFNO !=" + ddlFaculty1.SelectedValue, "S.STAFF_NAME");
                if (ddlFaculty1.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                    objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                }
                else
                {
                    objCommon.FillDropDownList(ddlFaculty1, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO <> 0 AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                    ddlFaculty2.ClearSelection();
                    ddlFaculty3.ClearSelection();
                }

                //objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(S.CANCEL = 0 OR S.CANCEL IS NULL) AND s.STAFFNO > 0 AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ua_no2 + " ) AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                //objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(S.CANCEL = 0 OR S.CANCEL IS NULL) AND s.STAFFNO > 0 AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ua_no3 + " )AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                //     objCommon.FillDropDownList(ddlFaculty2, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ua_no3 + " )", string.Empty);

                //ddlFaculty2.SelectedValue = ua_no2 == ddlFaculty1.SelectedValue ? "0" : ua_no2;
                //ddlFaculty3.SelectedValue = ua_no3 == ddlFaculty1.SelectedValue ? "0" : ua_no3;
                return;
            }
        }
    }
    protected void ddlFaculty2_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
            Label lblCCode = item.FindControl("lblCCode") as Label;


            if (sender.GetHashCode() == ddlFaculty2.GetHashCode())
            {
                ////objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0  AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ddlFaculty2.SelectedValue + ")", "S.STAFF_NAME");
                if (ddlFaculty2.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ddlFaculty2.SelectedValue + ") AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                }
                else
                {
                    objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                    objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                }

                //string ua_no1 = ddlFaculty1.SelectedValue;
                //string ua_no3 = ddlFaculty3.SelectedValue;

                //objCommon.FillDropDownList(ddlFaculty1, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(S.CANCEL = 0 OR S.CANCEL IS NULL) AND s.STAFFNO > 0 AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (" + ddlFaculty2.SelectedValue + "," + ua_no3 + " )AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                //objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(S.CANCEL = 0 OR S.CANCEL IS NULL) AND s.STAFFNO > 0 AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (" + ddlFaculty2.SelectedValue + "," + ua_no1 + " )AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);

                //ddlFaculty1.SelectedValue = ua_no1 == ddlFaculty2.SelectedValue ? "0" : ua_no1;
                //ddlFaculty3.SelectedValue = ua_no3 == ddlFaculty2.SelectedValue ? "0" : ua_no3;
                //return;
            }
        }
    }
    protected void ddlFaculty3_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
            Label lblCCode = item.FindControl("lblCCode") as Label;


            if (sender.GetHashCode() == ddlFaculty3.GetHashCode())
            {
                string ua_no1 = ddlFaculty1.SelectedValue;
                string ua_no2 = ddlFaculty2.SelectedValue;

                ////objCommon.FillDropDownList(ddlFaculty1, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(S.CANCEL = 0 OR S.CANCEL IS NULL) AND s.STAFFNO > 0 AND p.CCODE = '" + lblCCode.Text + "' AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (" + ddlFaculty3.SelectedValue + "," + ua_no2 + " )AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                ////objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(S.CANCEL = 0 OR S.CANCEL IS NULL) AND s.STAFFNO > 0 AND S.STAFF_NAME<> '" + ddlFaculty1.SelectedItem.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty3.SelectedValue + "," + ua_no1 + ")", "S.STAFF_NAME");
                if (ddlFaculty3.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlFaculty1, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty3.SelectedValue + "," + ua_no2 + " )AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                    objCommon.FillDropDownList(ddlFaculty2, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty3.SelectedValue + "," + ua_no1 + ") AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                }
                else
                {
                    objCommon.FillDropDownList(ddlFaculty3, "ACD_PS_MOD_PREFERENCE  P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE)INNER  JOIN ACD_STAFF S ON(P.STAFFNO = S.STAFFNO)", " DISTINCT S.STAFFNO", "S.STAFF_NAME", "(CANCEL = 0 OR CANCEL IS NULL) AND S.STAFFNO > 0 AND P.CCODE = '" + lblCCode.Text + "' AND S.STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ddlFaculty2.SelectedValue + ") AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND S.STAFFNO NOT IN (SELECT ISNULL(PD.APPROVED,0) FROM ACD_PAPERSET_DETAILS PD WHERE PD.CANCEL = 1 AND PD.CCODE = '" + lblCCode.Text + "' AND PD.SEMESTERNO = C.SEMESTERNO AND PD.SESSIONNO =  " + ddlSession.SelectedValue + ")", string.Empty);
                }

                ddlFaculty1.SelectedValue = (ua_no1 == ddlFaculty3.SelectedValue ? "0" : ua_no1);
                ddlFaculty2.SelectedValue = (ua_no2 == ddlFaculty3.SelectedValue ? "0" : ua_no2);
                return;
            }
        }
    }

    #endregion

    #region Report
    protected void btnReport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        PStaffController objPStaff = new PStaffController();
        if (ddlClgname.SelectedIndex > 0)
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                string ccode = string.Empty;
                ds = objPStaff.PaperSetReportFacultyAlloted(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), ccode, Convert.ToInt32(ddlSemester.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ShowReport("Paper_Set_Faculty_List", "rptPaperSetFacultyList.rpt");
                }
                else
                {
                    objCommon.DisplayMessage(this.updFacAllot, "No Record found!", this.Page);
                }

                BindListView();
            }
            else
            {
                objCommon.DisplayMessage(this.updFacAllot, "Please Select Semester", this.Page);
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;

            }
        }
        else
        {
            if (ddlClgname.Items.Count == 1)
                objCommon.DisplayMessage(this.updFacAllot, "Please Select College & Scheme", this.Page);
            else
                objCommon.DisplayMessage(this.updFacAllot, "Please Select College & Scheme", this.Page);

            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int clg_id = Convert.ToInt32(ViewState["college_id"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                //int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "FLOCK = 1 AND IS_ACTIVE = 1 AND SESSIONID= " + ddlSession.SelectedValue));

                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONID=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_CCODE=,@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONID=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_CCODE=,@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            }
            
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updFacAllot, updFacAllot.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ShowReportNotSet(string reportTitle, string rptFileName)
    {
        try
        {
            int clg_id = Convert.ToInt32(ViewState["college_id"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                //int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "FLOCK = 1 AND IS_ACTIVE = 1 AND SESSIONID= " + ddlSession.SelectedValue));
                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue);
            }
           
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updFacAllot, updFacAllot.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReportNotSet_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();
        PStaffController objPStaff = new PStaffController();
      
            if (ddlSemester.SelectedIndex > 0)
            {
                string ccode = string.Empty;
                ds = objPStaff.FacultyNotSet(Convert.ToInt32(ddlSession.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReportNotSet("Paper_Not_Set_Faculty_List", "FacultyNotSet.rpt");
                }
                else
                {
                    objCommon.DisplayMessage(this.updFacAllot, "No Record found!", this.Page);
                }

                BindListView();
            }
            else
            {
                objCommon.DisplayMessage(this.updFacAllot, "Please Select Semester", this.Page);
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;

            }

       
    }
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
            }

            ddlSession.Focus();
        }
        else
        {
            ddlClgname.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;

            ddlSemester.SelectedIndex = 0;
        }
    }
   
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_COURSE C ON (C.CCODE = P.CCODE)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue), " S.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }
    }
}

