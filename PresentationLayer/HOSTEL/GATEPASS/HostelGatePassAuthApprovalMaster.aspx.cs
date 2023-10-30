using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class HOSTEL_GATEPASS_HostelGatePassAuthApprovalMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AddHostelGatePassAuthApproval OBJHGPAA = new AddHostelGatePassAuthApproval();
    AddHostelGatePassAuthController objHGPA = new AddHostelGatePassAuthController();

    #region Page Events
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
                ////CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                pnlAdd.Visible = false;
                pnlauthority.Visible = false;
                pnlEmpList.Visible = false;
                pnlbtn.Visible = false;
                pnlauthoritybtn.Visible = false;
                pnlList.Visible = true;
                FillAAuthority();
                GetDays();
                BindListViewAAMaster();
                objCommon.FillDropDownList(ddlStuType, "ACD_HOSTEL_STUDENT_TYPE", "STUDENT_TYPE_ID", "STUDENT_TYPE", "STUDENT_TYPE_ID>0", "STUDENT_TYPE_ID");
                objCommon.FillDropDownList(ddlStuTypeAuth, "ACD_HOSTEL_STUDENT_TYPE", "STUDENT_TYPE_ID", "STUDENT_TYPE", "STUDENT_TYPE_ID>0", "STUDENT_TYPE_ID");
                objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");
                objCommon.FillDropDownList(ddlApp, "ACD_HOSTEL_AUTH_APPROVAL_TYPE_MASTER", "AUTHORITY_APPROVAL_NO", "AUTHORITY_APPROVAL_TYPE", "AUTHORITY_APPROVAL_NO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "AUTHORITY_APPROVAL_NO");
                ViewState["edit"] = "add";
                Session["action"] = "add";
            }
        }
    }
    #endregion Page Events

    #region Action
    protected void btnaddauthority_Click(object sender, EventArgs e)
    {
        clearnew();
        pnlAdd.Visible = false;
        pnlauthority.Visible = true;
        pnlList.Visible = false;
        pnlbtn.Visible = false;
        pnlauthoritybtn.Visible = true;
        ViewState["editauth"] = "add";
        Session["actionauth"] = "add";
        ViewState["actionauth"] = "add";
        pnlAAPaList.Visible = false;
        pnlAuthapprovalList.Visible = true;
        BindListViewAuthApprovalMaster();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clearnew();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlbtn.Visible = true;
        ViewState["action"] = "add";
        pnlAAPaList.Visible = false;
        pnlauthority.Visible = false;

        DataSet dsStudent = objCommon.FillDropDown("ACD_HOSTEL_ADD_AUTH_MASTER", "APPROVAL1,APPROVAL2", "APPROVAL3", "", "");
        if (dsStudent.Tables[0].Rows.Count > 0)
        {
            lblApprover1.Text = lblApprover1.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL1"].ToString() + ")";
            lblApprover2.Text = lblApprover2.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL2"].ToString() + ")";
            lblApprover3.Text = lblApprover3.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL3"].ToString() + ")";
        }
        else
        {
            //objCommon.DisplayMessage(this.Page, "Enter Appprovals", this.Page);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        clearnew();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        pnlbtn.Visible = false;
        pnlAAPaList.Visible = true;
        pnlAuthapprovalList.Visible = false;
        pnlauthority.Visible = false;
    }

    protected void btnauthBack_Click(object sender, EventArgs e)
    {
        clearnewAuthApproval();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        pnlbtn.Visible = false;
        pnlauthoritybtn.Visible = false;
        pnlAAPaList.Visible = true;
        pnlAuthapprovalList.Visible = false;
        pnlauthority.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            OBJHGPAA.AUTHORITY = Convert.ToInt32(ddlApp.SelectedValue);
            OBJHGPAA.AUTHORITY_NAME = ddlApp.SelectedItem.Text;

            if (ddlAA1.SelectedValue != string.Empty)
            {
                OBJHGPAA.APPROVAL_1 = Convert.ToInt32(ddlAA1.SelectedValue);
            }
            else
            {
                OBJHGPAA.APPROVAL_1 = 0;
            }

            string ua_type = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + OBJHGPAA.APPROVAL_1);

            if (ddlAA2.SelectedValue != string.Empty)
            {
                OBJHGPAA.APPROVAL_2 = Convert.ToInt32(ddlAA2.SelectedValue);
            }
            else
            {
                OBJHGPAA.APPROVAL_2 = 0;
            }

            if (ddlAA3.SelectedValue != string.Empty)
            {
                OBJHGPAA.APPROVAL_3 = Convert.ToInt32(ddlAA3.SelectedValue);
            }
            else
            {
                OBJHGPAA.APPROVAL_3 = 0;
            }

            OBJHGPAA.AAPATH = Convert.ToString(txtAAPath.Text);
            OBJHGPAA.CREATED_BY = 1;
            OBJHGPAA.IP_ADDRESS = "::1";
            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                OBJHGPAA.APP_NO = Convert.ToInt32(Session["APP_NO"]);
                CustomStatus cs = (CustomStatus)objHGPA.UpdateAAPath(OBJHGPAA, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    clearnew();
                    BindListViewAAMaster();
                    objCommon.DisplayMessage(this.Page, "Record Updated sucessfully", this.Page);
                    ViewState["edit"] = null;
                    Session["action"] = null;
                }
            }
            else
            {
                int idno = 0;
                idno = objHGPA.AddAAPath(OBJHGPAA, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
                if (idno == 2627)
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                }
                else if (idno != 2727)
                {
                    clearnew();
                    BindListViewAAMaster();
                    objCommon.DisplayMessage(this.Page, "Record added successfully", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnauthSave_Click(object sender, EventArgs e)
    {
        try
        {
            int AUTHORITYTYPE = Convert.ToInt32(ddlauthorityapproval.SelectedValue);
            int STUDENTTYPE = Convert.ToInt32(ddlStuType.SelectedValue);
            string AUTHORITYNAME = ddlauthorityapproval.SelectedItem.Text;
            string APPROVAL1 = txtauthapproval1.Text;
            string APPROVAL2 = txtauthapproval2.Text;
            string APPROVAL3 = txtauthapproval3.Text;
            string APPROVAL4 = txtauthapproval4.Text;

            OBJHGPAA.CREATED_BY = 1;
            OBJHGPAA.IP_ADDRESS = "::1";

            if (Session["actionauth"] != null && Session["actionauth"].ToString().Equals("edit"))
            {
                OBJHGPAA.APP_NO = Convert.ToInt32(Session["APP_NO"]);
                CustomStatus cs = (CustomStatus)objHGPA.UpdateAuthApprovalPath(OBJHGPAA, Convert.ToInt32(ddlauthorityapproval.SelectedValue), STUDENTTYPE, AUTHORITYNAME, APPROVAL1, APPROVAL2, APPROVAL3, APPROVAL4);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    clearnewAuthApproval();
                    BindListViewAuthApprovalMaster();
                    objCommon.DisplayMessage(this.Page, "Record Updated sucessfully", this.Page);
                    ViewState["editauth"] = null;
                    Session["actionauth"] = null;
                }
            }
            else
            {
                //Add New
                int idno = 0;
                idno = objHGPA.AddAuthApprovalPath(OBJHGPAA, Convert.ToInt32(ddlauthorityapproval.SelectedValue), STUDENTTYPE, AUTHORITYNAME, APPROVAL1, APPROVAL2, APPROVAL3, APPROVAL4);
                if (idno == 2627)
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                }
                if (idno == 1027)
                {
                    // objCommon.DisplayMessage(this.Page, "", this.Page);
                }
                else if (idno != 2727)
                {
                    clearnewAuthApproval();
                    BindListViewAuthApprovalMaster();
                    objCommon.DisplayMessage(this.Page, "Record added successfully", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Error", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAAuthority();
    }

    protected void ddlAA1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA1_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlAA2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA12_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlAA3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA3_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", "UA_TYPE !=2 AND " + ddlCollege.SelectedValue + " IN (select value from dbo.Split(UA_COLLEGE_NOS,',')) AND " + ddldepartment.SelectedValue + " IN (select value from dbo.Split(UA_DEPTNO,','))", "UA_NO");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlApp.SelectedValue == "1")
            {
                if (ddlCollege.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", "UA_TYPE NOT in(2) AND " + ddlCollege.SelectedValue + " IN (select value from dbo.Split(UA_COLLEGE_NOS,','))", "UA_NO");
                }
            }
            else
            {
                if (ddlCollege.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", "UA_TYPE NOT IN (1,2) AND " + ddlCollege.SelectedValue + " IN (select value from dbo.Split(UA_COLLEGE_NOS,','))", "UA_NO");
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlauthoritybtn.Visible = true;
            ImageButton btnauthappEdit = sender as ImageButton;
            int APPNO = int.Parse(btnauthappEdit.CommandArgument);
            Session["APP_NO"] = int.Parse(btnauthappEdit.CommandArgument);
            this.ShowDetailsAuthApprovalPath(APPNO);
            ViewState["editauth"] = "edit";
            Session["actionauth"] = "edit";
            pnlauthority.Visible = true;
            pnlList.Visible = false;
            pnlAuthapprovalList.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnauthappEdit_Click(object sender, ImageClickEventArgs e)
    {
        pnlbtn.Visible = true;
        ImageButton btnEdit = sender as ImageButton;
        int APPNO = int.Parse(btnEdit.CommandArgument);
        Session["APP_NO"] = int.Parse(btnEdit.CommandArgument);
        this.ShowDetails(APPNO);
        ViewState["edit"] = "edit";
        Session["action"] = "edit";
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlAAPaList.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearnew();
    }

    protected void btnauthCancel_Click(object sender, EventArgs e)
    {
        clearnewAuthApproval();
    }
    #endregion Action

    #region Private Methods
    private void EnableDisable(int index)
    {
        if (ddlApp.SelectedValue == "1")
        {
            switch (index)
            {
                case 1:
                    if (ddlAA1.SelectedIndex == 0)
                    {
                        ddlAA2.SelectedIndex = 0;
                        ddlAA2.Enabled = false;
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                    }
                    else
                    {
                        ddlAA2.Enabled = true;
                        string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    break;

                case 2:
                    if (ddlAA2.SelectedIndex == 0)
                    {
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    else
                    {
                        ddlAA3.Enabled = true;
                        string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString();
                    }
                    break;

                case 3:
                    if (ddlAA3.SelectedIndex != 0)
                    {
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + ".";
                    }
                    break;
            }
        }
        else
        {
            switch (index)
            {
                case 1:
                    if (ddlAA1.SelectedIndex == 0)
                    {
                        ddlAA2.SelectedIndex = 0;
                        ddlAA2.Enabled = false;
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                    }
                    else
                    {
                        ddlAA2.Enabled = true;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");

                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    break;

                case 2:
                    if (ddlAA2.SelectedIndex == 0)
                    {
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    else
                    {
                        ddlAA3.Enabled = true;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString();
                    }
                    break;

                case 3:
                    if (ddlAA3.SelectedIndex != 0)
                    {
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + ".";
                    }
                    break;
            }
        }
    }

    private void FillAAuthority()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.FillAAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void clearnew()
    {
        ddlApp.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlauthorityapproval.SelectedIndex = 0;
        ddldepartment.SelectedIndex = 0;
        ddlAA1.SelectedIndex = 0;
        ddlAA2.SelectedIndex = 0;
        ddlAA3.SelectedIndex = 0;
        txtAAPath.Text = string.Empty;
        lblApprover1.Text = "Approval 1";
        lblApprover2.Text = "Approval 2";
        lblApprover3.Text = "Approval 3";
    }
    
    private void clearnewAuthApproval()
    {
        ddlauthorityapproval.SelectedIndex = 0;
        txtauthapproval1.Text = string.Empty;
        txtauthapproval2.Text = string.Empty;
        txtauthapproval3.Text = string.Empty;
        ViewState["actionauth"] = "add";
        ViewState["APP_NO"] = null;
    }

    protected void BindListViewAuthApprovalMaster()
    {
        try
        {
            DataSet ds = objHGPA.GetAllAuthApprovalPathMaster();

            if (ds.Tables[0].Rows.Count > 0)
            {
                btnShowReport.Visible = false;
                lvAuthapprovalList.DataSource = ds;
                lvAuthapprovalList.DataBind();
                lvAuthapprovalList.Visible = true;
            }
            else
            {
                btnShowReport.Visible = false;
                lvAuthapprovalList.DataSource = null;
                lvAuthapprovalList.DataBind();
                lvAuthapprovalList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.BindListViewAAMaster ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void BindListViewAAMaster()
    {
        try
        {
            DataSet ds = objHGPA.GetAllAAMaster();

            if (ds.Tables[0].Rows.Count > 0)
            {
                btnShowReport.Visible = false;
                lvAAPath.DataSource = ds;
                lvAAPath.DataBind();

                Label lblappproval1 = (Label)lvAAPath.FindControl("lblappproval1");
                lblappproval1.Text = "APPROVAL1" + " (" + (ds.Tables[0].Rows[0]["APPROVAL1"].ToString()) + ")";

                Label lblappproval2 = (Label)lvAAPath.FindControl("lblappproval2");
                lblappproval2.Text = "APPROVAL2" + " (" + (ds.Tables[0].Rows[0]["APPROVAL2"].ToString()) + ")";

                Label lblappproval3 = (Label)lvAAPath.FindControl("lblappproval3");
                lblappproval3.Text = "APPROVAL3" + " (" + (ds.Tables[0].Rows[0]["APPROVAL3"].ToString()) + ")";
            }
            else
            {
                btnShowReport.Visible = false;
                lvAAPath.DataSource = null;
                lvAAPath.DataBind();
                lvAAPath.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.BindListViewAAMaster ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void ShowDetailsAuthApprovalPath(Int32 APP_NO)
    {
        DataSet ds = null;
        try
        {
            ds = objHGPA.GetSingleAuthApprovalPath(APP_NO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["APP_NO"] = APP_NO;
                ddlauthorityapproval.SelectedValue = ds.Tables[0].Rows[0]["AUTHORITY_TYPE_NO"].ToString();
                txtauthapproval1.Text = ds.Tables[0].Rows[0]["APPROVAL1"].ToString();
                txtauthapproval2.Text = ds.Tables[0].Rows[0]["APPROVAL2"].ToString();
                txtauthapproval3.Text = ds.Tables[0].Rows[0]["APPROVAL3"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 APP_NO)
    {
        DataSet ds = null;
        try
        {
            ds = objHGPA.GetSingleAAPath(APP_NO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["APP_NO"] = APP_NO;

                objCommon.FillDropDownList(ddlApp, "ACD_HOSTEL_AUTHORITY_APPROVAL_MASTER", "APP_NO", "AUTHORITY_TYPE", "APP_NO= APP_NO", "APP_NO");
                ddlApp.SelectedValue = ds.Tables[0].Rows[0]["AUTHORITY_TYPE_NO"].ToString();

                FillAAuthority();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim().Equals(string.Empty) || ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim().Equals(string.Empty) ? "0" : ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim();

                objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_NAME +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", "UA_TYPE NOT in(2) AND " + ddlCollege.SelectedValue + " IN (select value from dbo.Split(UA_COLLEGE_NOS,','))", "UA_NO");
                txtAAPath.Text = ds.Tables[0].Rows[0]["APP_PATH"].ToString();
                ddlAA1.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_1_UANO"].ToString();
                ddldepartment.SelectedValue = ds.Tables[0].Rows[0]["dept_noaa"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["APPROVAL_2_UANO"].ToString().Trim().Equals("0")))
                {
                    ddlAA2.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_2_UANO"].ToString();
                    this.EnableDisable(2);
                    ddlAA2.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["APPROVAL_3_UANO"].ToString().Trim().Equals("0")))
                {
                    ddlAA3.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_3_UANO"].ToString();
                    this.EnableDisable(3);
                    ddlAA3.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetDays()
    {
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_DAYS", "DAY_CODE", "DAY_NAME", "", "DAY_NO");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstDays.DataTextField = "DAY_NAME";
            cblstDays.DataValueField = "DAY_CODE";
            cblstDays.DataSource = ds.Tables[0];
            cblstDays.DataBind();
        }
    }
    #endregion Private Methods
    protected void chkDays_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cblstDays.Items)
        {
            item.Selected = chkDays.Checked;
        }
    }
}