using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_BulkPhdCourseRegistration : System.Web.UI.Page
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
                Response.Redirect("~/notauthorized.aspx?page=BulkPhdCourseRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkPhdCourseRegistration.aspx");
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
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " top 5 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND EXAMTYPE = 1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO >0 AND DEGREENO = 6 ", "DEGREENO");
            ddlDegree.SelectedIndex = 1;
            int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
            if (deptno > 0)
            {
                objCommon.FillDropDownList(ddlDeptName, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + deptno, "LONGNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlDeptName, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");
            }
            ddlDeptName.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BulkPhdCourseRegistration.FillDropDown() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void BindStudents()
    {
        try
        {
            DataSet ds = objSc.GETBulkPhdCourseRegistration(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                pnllist.Visible = true;

                foreach (ListViewDataItem dataRow in lvStudent.Items)
                {
                    CheckBox chkProv = dataRow.FindControl("cbRow") as CheckBox;
                    Button btnprview = dataRow.FindControl("btnPreview") as Button;

                    Label lblsup = dataRow.FindControl("lblSupervisior") as Label;
                    Label lbljointsup = dataRow.FindControl("lbljointsupervisior") as Label;
                    Label lblinsfaculty = dataRow.FindControl("lblinsfaculty") as Label;
                    Label lbldrc = dataRow.FindControl("lbldrc") as Label;
                    Label lbldean = dataRow.FindControl("lbldean") as Label;
                    Label lbldrcchairman = dataRow.FindControl("lbldrcchairman") as Label;

                    if (chkProv.Checked == true)
                    {
                        chkProv.Checked = true;
                        chkProv.Enabled = false;
                    }
                    if (lblsup.Text.ToString() != "APPROVED" || lbljointsup.Text.ToString() != "APPROVED" || lblinsfaculty.Text.ToString() != "APPROVED" || lbldrc.Text.ToString() != "APPROVED" || lbldrcchairman.Text.ToString() != "APPROVED")
                    {
                        btnprview.Enabled = false;
                        // btnprview.Visible = false;
                        chkProv.Visible = false;
                    }
                    if (lbldean.Text.ToString() == "APPROVED")
                    {
                        chkProv.Visible = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("No more Student to approve", this.Page);
                pnllist.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.BindStudents ->" + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.btnShow_Click ->" + ex.Message + " " + ex.StackTrace);
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
            if ((Session["usertype"].ToString() == "4"))
            {

                foreach (ListViewDataItem lvItem in lvStudent.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                    TextBox txtRegno = lvItem.FindControl("txtRegNo") as TextBox;

                    Label lblsup = lvItem.FindControl("lblSupervisior") as Label;
                    Label lbljointsup = lvItem.FindControl("lbljointsupervisior") as Label;
                    Label lblinsfaculty = lvItem.FindControl("lblinsfaculty") as Label;
                    Label lbldrc = lvItem.FindControl("lbldrc") as Label;
                    Label lbldean = lvItem.FindControl("lbldean") as Label;
                    Label lbldrcchairman = lvItem.FindControl("lbldrcchairman") as Label;

                    if (lblsup.Text.ToString() == "APPROVED" && lbljointsup.Text.ToString() == "APPROVED" && lblinsfaculty.Text.ToString() == "APPROVED" && lbldrc.Text.ToString() == "APPROVED" && lbldrcchairman.Text.ToString() == "APPROVED" && lbldean.Text.ToString() != "APPROVED")
                    {

                        if (chkBox.Checked == true)
                        {
                            if (IDNOs.Equals(string.Empty))
                                IDNOs = chkBox.ToolTip;
                            else
                                IDNOs += "," + chkBox.ToolTip;
                            cnt += 1;
                        }
                    }
                }
                if (IDNOs.Equals(string.Empty))
                {
                    objCommon.DisplayMessage("Please Select At least one Student", this.Page);
                    return;
                }
                if (!(IDNOs.Equals(string.Empty)))
                {
                    CustomStatus cs = (CustomStatus)objSc.UpdateBulkPhdCourseRegistration(IDNOs, Convert.ToInt32(ddlSession.SelectedValue));

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
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval.btnSubmit_Click ->" + ex.Message + " " + ex.StackTrace);
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
        Label lblsem = sender as Label;
        Session["userpreviewidC"] = Convert.ToString(btnPreview.CommandName);
        Session["prvcoursesession"] = ddlSession.SelectedValue;
        Session["prvcoursesem"] = Convert.ToString(btnPreview.ToolTip);
        string url = "CourseRegistrationPhd.aspx?pageno=1122";

        string newWin = "window.open('" + url + "');";

        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }

    protected void lvStudent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
