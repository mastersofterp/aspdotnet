using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_BulkPhdSemesterPramotion : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objSc = new PhdController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkPhdSemesterPramotion.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkPhdSemesterPramotion.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
               // CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                if ((Session["usertype"].ToString() == "4") || (Session["usertype"].ToString() == "1"))
                {
                    FillDropDown();
                    pnllist.Visible = false;
                }
            }
            ViewState["userBranch"] = null;
        }
    }

    protected void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "Top 5 SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPE");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " Top 10 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO >0", "DEGREENO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO NOT IN(0)", "LONGNAME");
            //ddlDegree.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlupsession, "ACD_SESSION_MASTER", "DIDTINCT(SESSIONNO)", "SESSION_NAME", "IS_ACTIVE > 0 ", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlupsemester, "ACD_SEMESTER", " SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "semesterno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkPhdAnnexureA.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void BindStudents()
    {
        try
        {
            DataSet ds = objSc.GETBulkPhdSemesterPramotion(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                pnllist.Visible = true;
                tblupdate.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("No more Student to approve", this.Page);
                pnllist.Visible = false;
                tblupdate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkPhdAnnexureA .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindStudents();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkPhdAnnexureA .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Phd objS = new Phd();
            string IDNOs = string.Empty;
            int cnt = 0;
            string Coursenos = string.Empty;
            if ((Session["usertype"].ToString() == "1"))
            {

                foreach (ListViewDataItem lvItem in lvStudent.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                    TextBox txtRegno = lvItem.FindControl("txtRegNo") as TextBox;
                    Label lblCourseno = lvItem.FindControl("lblCoursename") as Label;

                    if (chkBox.Checked == true)
                    {
                        if (IDNOs.Equals(string.Empty))
                            IDNOs = chkBox.ToolTip;
                        else
                            IDNOs += "," + chkBox.ToolTip;

                        if (Coursenos.Equals(string.Empty))
                            Coursenos = lblCourseno.ToolTip;
                        else
                            Coursenos += ";" + lblCourseno.ToolTip;
                        cnt += 1;
                    }

                }
                if (IDNOs.Equals(string.Empty))
                {
                    objCommon.DisplayMessage("Please Select At least one Student", this.Page);
                    return;
                }
                if (!(IDNOs.Equals(string.Empty)))
                {
                    CustomStatus cs = (CustomStatus)objSc.UpdateBulkPhdSemesterPramotion(IDNOs, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlupsemester.SelectedValue), Convert.ToInt32(ddlupsession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Coursenos);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Information Updated Successfully!!", this.Page);
                        this.BindStudents();
                        //ddlDegree.SelectedIndex = 0;
                        //ddlDeptName.SelectedIndex = 0;
                        //lvStudent.DataSource = null;
                        //lvStudent.DataBind();
                        //pnllist.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayMessage("Information Not Update", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("You Are Not Authorized , Only Dean Can Update Details !!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkPhdAnnexureA .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Button btnPreview = sender as Button;

        Session["userpreviewidA"] = Convert.ToString(btnPreview.CommandName);

        string url = "PhdAnnexure.aspx?pageno=1085";

        string newWin = "window.open('" + url + "');";

        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }

    protected void lvStudent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH ACD ON (B.BRANCHNO=ACD.BRANCHNO)", "B.BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
        }
        ddlBranch.SelectedIndex = 0;

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + ddlScheme.SelectedValue + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
        //}
       // ddlSemester.SelectedIndex = 0;
        ddlSemester.SelectedValue="1";
    }
    
}
