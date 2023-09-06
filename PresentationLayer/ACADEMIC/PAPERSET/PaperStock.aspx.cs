//======================================================================================
// PROJECT NAME  : RFC-COMMON                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PAPER SET 
// CREATION DATE : 30-08-2012
// ADDED BY      : PRIYANKA KABADE                                                  
// ADDED DATE    : 
// MODIFIED BY   : SHUBHAM BARKE (FOR RFC-COMMON)
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

public partial class ACADEMIC_PaperStock : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int sessionno;

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                string deptno = string.Empty;
                if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                    deptno = "0";
                else
                    deptno = Session["userdeptno"].ToString();
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
            }
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

    #endregion

    #region DDL EVENT
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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_PNAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "S.SESSIONID DESC"); //--AND FLOCK = 1
                ddlSession.Focus();
            }
        }
        else
        {
            ddlClgname.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblCCode = e.Item.FindControl("lblCCode") as Label;

        TextBox txtQTY = e.Item.FindControl("txtQTY") as TextBox;
        TextBox txtReorder = e.Item.FindControl("txtReorder") as TextBox;

        //LOCK STATUS
        HiddenField hfBosLock = e.Item.FindControl("hfBosLock") as HiddenField;
        HiddenField hfDeanLock = e.Item.FindControl("hfDeanLock") as HiddenField;

        if (hfBosLock.Value.ToLower() == "true" || hfDeanLock.Value.ToLower() == "true")
        {
            txtQTY.Enabled = false;
            txtReorder.Enabled = false;
        }
        else
        {
            txtQTY.Enabled = true;
            txtReorder.Enabled = true;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        //  pnlList.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        ddlSemester.Focus();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), " S.SEMESTERNO");
                ddlSemester.Focus();
            }
            else
            {
                ddlSemester.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperStock.ddlSession_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Private Method
    private void BindListView()
    {
        int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));

        DataSet ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO LEFT OUTER JOIN ACD_PAPERSET_DETAILS P ON (P.CCODE = C.CCODE AND P.SEMESTERNO=C.SEMESTERNO AND ISNULL(P.SESSIONNO,0) =" + Convert.ToInt32(SessionNo) + ")", "DISTINCT C.CCODE,COURSE_NAME,DBO.FN_DESC('SCHEMETYPE',SCHEMETYPE)SCHEMETYPE,DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREENAME", "QTY,REQ_LEVEL,BOS_LOCK,DEAN_LOCK", " C.SEMESTERNO = " + ddlSemester.SelectedValue + "AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND MAXMARKS_E > 0 AND SUBID =1", "C.CCODE");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                btnSave.Visible = true;
                btnCancel.Visible = true;
                hdftotalrows.Value = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                btnSave.Visible = false;
                btnCancel.Visible = false;
                hdftotalrows.Value = "0";
                objCommon.DisplayMessage(this.updpaperStock, "Data Not Found", this.Page);
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            btnSave.Visible = false;
            btnCancel.Visible = false;
            hdftotalrows.Value = "0";
        }
    }
    #endregion

    #region Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CourseController objCCont = new CourseController();
            int item_count = 0, saved_count = 0;
            foreach (ListViewItem item in lvCourse.Items)
            {
                Label lblCCode = item.FindControl("lblCCode") as Label;
                Label lblCourseName = item.FindControl("lblCourseName") as Label;
                TextBox txtQTY = item.FindControl("txtQTY") as TextBox;
                TextBox txtReorder = item.FindControl("txtReorder") as TextBox;
                int qty = txtQTY.Text == "" ? 0 : Convert.ToInt16(txtQTY.Text);
                int reorder = txtReorder.Text == "" ? 0 : Convert.ToInt16(txtReorder.Text);

                int ret = objCCont.UpdatePaperSetCourseQTY(lblCCode.Text, Convert.ToInt16(ViewState["schemeno"]), Convert.ToInt16(ddlSemester.SelectedValue), qty, reorder);
                if (ret != -99)
                {
                    saved_count++;
                }
                else
                {
                    objCommon.DisplayMessage(this.updpaperStock, "Data Not Saved", this.Page);
                    return;
                }

            }
            BindListView();
            if (saved_count == 0)
            {
                objCommon.DisplayMessage(this.updpaperStock, "Please enter quantity for at least 1 course!", this.Page);
            }
            else
            {
                if (saved_count == item_count)
                {
                    objCommon.DisplayMessage(this.updpaperStock, "Available and required quantity saved for all courses!", this.Page);
                }

                else
                {
                    objCommon.DisplayMessage(this.updpaperStock, "Available and required quantity saved for " + saved_count + " courses!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperStock.btnSave_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        //pnlList.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            if (ddlSemester.SelectedIndex > 0)
                BindListView();
            else
            {
                objCommon.DisplayMessage(this.updpaperStock, "Please Select Semester", this.Page);
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                //   pnlList.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updpaperStock, "Please Select College Scheme ", this.Page);
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            //   pnlList.Visible = false;
        }
        btnShow.Focus();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        //   pnlList.Visible = false;
        btnCancel.Visible = false;
        btnSave.Visible = false;
        //ddlSchemeType.SelectedIndex = 0; //commented by reena on 17_10_16

    }
    #endregion

    #region comment
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    CourseController objCCont = new CourseController();
    //    int item_count = 0, saved_count = 0;
    //    foreach (ListViewItem item in lvCourse.Items)
    //    {
    //        Label lblCCode = item.FindControl("lblCCode") as Label;
    //        Label lblCourseName = item.FindControl("lblCourseName") as Label;
    //        TextBox txtQTY = item.FindControl("txtQTY") as TextBox;
    //        TextBox txtReorder = item.FindControl("txtReorder") as TextBox;
    //        int qty = txtQTY.Text == "" ? 0 : Convert.ToInt16(txtQTY.Text);
    //        int reorder = txtReorder.Text == "" ? 0 : Convert.ToInt16(txtReorder.Text);
    //        //string str = "1";
    //        //string str = objCommon.LookUp("ACD_PAPERSET_DETAILS P INNER JOIN ACD_SESSION_MASTER SM ON (P.SESSIONNO = SM.SESSIONNO)", "COUNT(P.SEMESTERNO)", "(P.DEAN_LOCK = 0 OR P.DEAN_LOCK IS NULL) AND (P.APPROVED IS NULL) and (P.cancel is null or P.cancel = 0) AND (P.BOS_LOCK = 0 OR P.BOS_LOCK IS NULL) AND P.BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND P.CCODE = '" + lblCCode.Text + "' AND P.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue));
    //        string str = objCommon.LookUp("ACD_PAPERSET_DETAILS P", "COUNT(P.SEMESTERNO)", "(P.DEAN_LOCK = 0 OR P.DEAN_LOCK IS NULL) AND (P.APPROVED IS NULL) and (P.cancel is null or P.cancel = 0) AND (P.BOS_LOCK = 0 OR P.BOS_LOCK IS NULL) AND P.CCODE = '" + lblCCode.Text + "' AND P.SEMESTERNO = " + ddlSemester.SelectedValue);
    //        if (str == "" || str == "0")
    //        {

    //            item_count++;
    //            //if (qty == 0 || reorder == 0)
    //            //{
    //            //    if (qty == 0) objCommon.DisplayMessage(this.updpaperStock, "Please enter quantity for " + lblCCode.Text, this.Page);
    //            //    if (reorder == 0) objCommon.DisplayMessage(this.updpaperStock, "Please enter requirement for " + lblCCode.Text, this.Page);
    //            //    return;
    //            //}
    //            //else
    //            //{
    //                //if (qty >= reorder)
    //                //{
    //                    //int ret = objCCont.UpdateCourseQTY(lblCCode.Text, Convert.ToInt16(ViewState["schemeno"]), Convert.ToInt16(ddlSemester.SelectedValue), qty, reorder);
    //                    int ret = objCCont.UpdatePaperSetCourseQTY(lblCCode.Text, Convert.ToInt16(ViewState["schemeno"]), Convert.ToInt16(ddlSemester.SelectedValue), qty, reorder);
    //                    if (ret != -99)
    //                        saved_count++;
    //                    else
    //                    {
    //                        objCommon.DisplayMessage(this.updpaperStock, "Data Not Saved", this.Page);
    //                        return;
    //                    }
    //                //}
    //                //else
    //                //{
    //                //    objCommon.DisplayMessage(this.updpaperStock, "Enter Quantity for " + lblCourseName.Text + " be Greater than Required Quantity", this.Page);
    //                //    return;
    //                //}
    //            //}
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updpaperStock, "You are not permitted to enter new paper set request!", this.Page);
    //            break;
    //        }
    //    }
    //    BindListView();
    //    if (saved_count == 0)
    //        objCommon.DisplayMessage(this.updpaperStock, "Please enter quantity for at least 1 course!", this.Page);
    //    else
    //        if (saved_count == item_count)
    //        ////    objCommon.DisplayMessage(this.updpaperStock, "Quantiy & reorder-level saved for all courses!", this.Page);
    //        ////else objCommon.DisplayMessage(this.updpaperStock, "Quantiy & reorder-level saved for " + saved_count + " courses!", this.Page);
    //                    objCommon.DisplayMessage(this.updpaperStock, "Available and required quantity saved for all courses!", this.Page);
    //        else objCommon.DisplayMessage(this.updpaperStock, "Available and required quantity saved for " + saved_count + " courses!", this.Page);

    //}
    //Save button is used for to save the Subject paper Requierd pr Availability  details 
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    CourseController objCCont = new CourseController();
    //    int item_count = 0, saved_count = 0;
    //    foreach (ListViewItem item in lvCourse.Items)
    //    {
    //        Label lblCCode = item.FindControl("lblCCode") as Label;
    //        TextBox txtQTY = item.FindControl("txtQTY") as TextBox;
    //        TextBox txtReorder = item.FindControl("txtReorder") as TextBox;
    //        int qty = txtQTY.Text == "" ? 0 : Convert.ToInt16(txtQTY.Text);//get Available quantity
    //        int reorder = txtReorder.Text == "" ? 0 : Convert.ToInt16(txtReorder.Text);//get requierd quantity

    //        string str = objCommon.LookUp("ACD_PAPERSET_DETAILS", "COUNT(SEMESTERNO)", "(DEAN_LOCK = 0 OR DEAN_LOCK IS NULL) AND (APPROVED IS NULL) and (cancel is null or cancel = 0) AND (BOS_LOCK = 0 OR BOS_LOCK IS NULL) AND BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND CCODE = '" + lblCCode.Text + "' AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SESSIONNO = " + sessionno);

    //        if (str == "" || str == "0")
    //        {

    //            item_count++;
    //            if (qty == 0 || reorder == 0)
    //            {
    //            }
    //            else
    //            {//update course quantity details
    //                int ret = objCCont.UpdatePaperSetCourseQTY(lblCCode.Text, Convert.ToInt16(ViewState["schemeno"]), Convert.ToInt16(ddlSemester.SelectedValue), qty, reorder);

    //                if (ret != -99)
    //                    saved_count++;
    //                else
    //                {
    //                    objCommon.DisplayMessage(this.updpaperStock, "Data Not Saved", this.Page);
    //                    return;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updpaperStock, "You are not permitted to enter new paper set request !", this.Page);
    //            break;
    //        }
    //    }
    //    BindListView();//bind list view for Course details
    //    if (saved_count == 0)
    //        objCommon.DisplayMessage(this.updpaperStock, "Please enter quantity for at least 1 Subject!", this.Page);
    //    else
    //        if (saved_count == item_count)
    //            objCommon.DisplayMessage(this.updpaperStock, "Available and Required quantity saved for all Subject !", this.Page);
    //        else objCommon.DisplayMessage(this.updpaperStock, "Available and Required quantity saved for " + saved_count + " Subject !", this.Page);

    //}
    #endregion


}
