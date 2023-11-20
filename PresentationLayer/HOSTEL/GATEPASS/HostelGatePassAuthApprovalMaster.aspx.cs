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
                //FillAAuthority();
                //GetDays();
                BindListViewAAMaster();
                objCommon.FillDropDownList(ddlDays, "ACD_HOSTEL_DAYS", "DAY_TYPE_NO", "DAY_TYPE_NAME", "DAY_TYPE_NO>0", "DAY_TYPE_NO");
                objCommon.FillDropDownList(ddlDaysAuth, "ACD_HOSTEL_DAYS", "DAY_TYPE_NO", "DAY_TYPE_NAME", "DAY_TYPE_NO>0", "DAY_TYPE_NO");
                objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
                objCommon.FillDropDownList(ddlStuType, "ACD_HOSTEL_STUDENT_TYPE", "STUDENT_TYPE_ID", "STUDENT_TYPE", "STUDENT_TYPE_ID>0", "STUDENT_TYPE_ID");
                objCommon.FillDropDownList(ddlStuTypeAuth, "ACD_HOSTEL_STUDENT_TYPE", "STUDENT_TYPE_ID", "STUDENT_TYPE", "STUDENT_TYPE_ID>0", "STUDENT_TYPE_ID");
                objCommon.FillDropDownList(ddldepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");

                objCommon.FillDropDownList(ddlApp, "ACD_HOSTEL_AUTH_APPROVAL_TYPE_MASTER", "AUTHORITY_APPROVAL_NO", "AUTHORITY_APPROVAL_TYPE", "AUTHORITY_APPROVAL_NO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "AUTHORITY_APPROVAL_NO");
                ddlApp.SelectedIndex = 1;
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

        DataSet dsStudent = objCommon.FillDropDown("ACD_HOSTEL_ADD_AUTH_MASTER", "APPROVAL1,APPROVAL2", "APPROVAL3,APPROVAL4,APPROVAL5", "", "");
        if (dsStudent.Tables[0].Rows.Count > 0)
        {
            lblApprover1.Text = lblApprover1.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL1"].ToString() + ")";
            lblApprover2.Text = lblApprover2.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL2"].ToString() + ")";
            lblApprover3.Text = lblApprover3.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL3"].ToString() + ")";
            lblApprover4.Text = lblApprover4.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL4"].ToString() + ")";
            lblApprover5.Text = lblApprover5.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL5"].ToString() + ")";
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

        ddlStuTypeAuth.Enabled = true;
        ddlDaysAuth.Enabled = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            OBJHGPAA.AUTHORITY = Convert.ToInt32(ddlApp.SelectedValue);
            OBJHGPAA.AUTHORITY_NAME = ddlApp.SelectedItem.Text;

            if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
            {
                OBJHGPAA.APPROVAL_1 = Convert.ToInt32(ddlAA1parent.SelectedValue);
                //string ua_type = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + OBJHGPAA.APPROVAL_1);
            }
            else
            {
                if (ddlAA1.SelectedValue != "0")
                {
                    OBJHGPAA.APPROVAL_1 = Convert.ToInt32(ddlAA1.SelectedValue);
                   // string ua_type = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + OBJHGPAA.APPROVAL_1);
                }
                else
                {
                    OBJHGPAA.APPROVAL_1 = 0;
                }
            }


            if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
            {
                OBJHGPAA.APPROVAL_2 = Convert.ToInt32(ddlAA2Parent.SelectedValue);
                //string ua_type = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + OBJHGPAA.APPROVAL_1);
            }
            else
            {
                if (ddlAA2.SelectedValue != "0")
                {
                    OBJHGPAA.APPROVAL_2 = Convert.ToInt32(ddlAA2.SelectedValue);
                }
                else
                {
                    OBJHGPAA.APPROVAL_2 = 0;
                }
            }

            if (ddlAA3.SelectedValue != string.Empty)
            {
                OBJHGPAA.APPROVAL_3 = Convert.ToInt32(ddlAA3.SelectedValue);
            }
            else
            {
                OBJHGPAA.APPROVAL_3 = 0;
            }

            if (ddlAA4.SelectedValue != string.Empty)
            {
                OBJHGPAA.APPROVAL_4 = Convert.ToInt32(ddlAA4.SelectedValue);
            }
            else
            {
                OBJHGPAA.APPROVAL_4 = 0;
            }

            if (ddlAA5.SelectedValue != string.Empty)
            {
                OBJHGPAA.APPROVAL_5 = Convert.ToInt32(ddlAA5.SelectedValue);
            }
            else
            {
                OBJHGPAA.APPROVAL_5 = 0;
            }

            //string dayno = string.Empty;
            //Boolean day = false;

            //foreach (ListItem li in cblstDays.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        day = true;
            //        if (!dayno.Equals(string.Empty))
            //        {
            //            dayno = dayno + ',' + li.Value;
            //        }
            //        else
            //        {
            //            dayno = li.Value;
            //        }
            //    }
            //}

            OBJHGPAA.DAYS = ddlDays.SelectedValue;
            OBJHGPAA.AAPATH = Convert.ToString(txtAAPath.Text);
            OBJHGPAA.CREATED_BY = 1;
            OBJHGPAA.IP_ADDRESS = "::1";
            OBJHGPAA.STUDTYPE = Convert.ToInt32(ddlStuType.SelectedValue);

            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                OBJHGPAA.APP_NO = Convert.ToInt32(Session["APP_NO"]);
                CustomStatus cs = (CustomStatus)objHGPA.UpdateAAPath(OBJHGPAA, Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    
                    BindListViewAAMaster();
                    objCommon.DisplayMessage(this.Page, "Record Updated successfully.", this.Page);
                    ViewState["edit"] = null;
                    Session["action"] = null;
                    clearnew();
                }
                if (cs.Equals(CustomStatus.DuplicateRecord))
                {

                    objCommon.DisplayMessage(this.Page, "Record already exist.", this.Page);
                    ViewState["edit"] = null;
                    Session["action"] = null;
                    clearnew();
                }
            }
            else
            {
                int idno = 0;
                idno = objHGPA.AddAAPath(OBJHGPAA, Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddldepartment.SelectedValue));
                if (idno == 2627)
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist.", this.Page);
                }
                else if (idno == 2323)
                {
                    BindListViewAAMaster();
                    objCommon.DisplayMessage(this.Page, "Insert operation failed. The maximum limit for STUDENT TYPE has been reached for one or more hostels.", this.Page);
                    clearnew();
                }
                else if (idno != 2727)
                {
                    BindListViewAAMaster();
                    objCommon.DisplayMessage(this.Page, "Record added successfully.", this.Page);
                    clearnew();
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
            int STUDENTTYPE = Convert.ToInt32(ddlStuTypeAuth.SelectedValue);
            int DAYS_TYPE = Convert.ToInt32(ddlDaysAuth.SelectedValue);
            string APPROVAL1 = txtauthapproval1.Text;
            string APPROVAL2 = txtauthapproval2.Text;
            string APPROVAL3 = txtauthapproval3.Text;
            string APPROVAL4 = txtauthapproval4.Text;
            string APPROVAL5 = txtauthapproval5.Text;

            OBJHGPAA.CREATED_BY = 1;
            OBJHGPAA.IP_ADDRESS = "::1";
            OBJHGPAA.APP_NO = 0;
            if (Session["actionauth"] != null && Session["actionauth"].ToString().Equals("edit"))
            {
                OBJHGPAA.APP_NO = Convert.ToInt32(Session["APP_NO"]);
                CustomStatus cs = (CustomStatus)objHGPA.UpdateAuthApprovalPath(OBJHGPAA, STUDENTTYPE, DAYS_TYPE, APPROVAL1, APPROVAL2, APPROVAL3, APPROVAL4, APPROVAL5);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    
                    BindListViewAuthApprovalMaster();
                    objCommon.DisplayMessage(this.Page, "Record Updated successfully.", this.Page);
                    ViewState["editauth"] = null;
                    Session["actionauth"] = null;
                    clearnewAuthApproval();

                    ddlStuTypeAuth.Enabled = true;
                    ddlDaysAuth.Enabled = true;
                }
                if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist.", this.Page);
                    ViewState["editauth"] = null;
                    Session["actionauth"] = null;
                    clearnewAuthApproval();
                }
            }
            else
            {
                //Add New
                int idno = 0;
                idno = objHGPA.AddAuthApprovalPath(OBJHGPAA, STUDENTTYPE, DAYS_TYPE, APPROVAL1, APPROVAL2, APPROVAL3, APPROVAL4, APPROVAL5);
                if (idno == 2627)
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist.", this.Page);
                }
                if (idno == 1027)
                {
                    // objCommon.DisplayMessage(this.Page, "", this.Page);
                }
                else if (idno != 2727)
                {
                    BindListViewAuthApprovalMaster();
                    objCommon.DisplayMessage(this.Page, "Record added successfully.", this.Page);
                    clearnewAuthApproval();
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
        //FillAAuthority();
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

    protected void ddlAA4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA4_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlAA5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA5_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedIndex > 0)
        {
            if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT")
            {
                ddlAA1parent.SelectedValue = "14";
                ddlAA1parent.Enabled= false;
                ddlAA2.Enabled = true;
                this.EnableDisable(1);

            }
            else
            {
                objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", "" + ddldepartment.SelectedValue + " IN (select value from dbo.Split(UA_DEPTNO,','))", "UA_NO");
            }
            
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

    //protected void chkDays_CheckedChanged(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in cblstDays.Items)
    //    {
    //        item.Selected = chkDays.Checked;
    //    }
    //}

    protected void ddlStuType_SelectedIndexChanged(object sender, EventArgs e)
    {
        OBJHGPAA.STUDTYPE = ddlStuType.SelectedIndex;
        OBJHGPAA.DAYS = ddlDays.SelectedIndex.ToString();
        DataSet ds = objHGPA.GetLabel(OBJHGPAA);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblApprover1.Text = "APPROVAL1" + " (" + (ds.Tables[0].Rows[0]["APPROVAL1"].ToString()) + ")";

            lblApprover2.Text = "APPROVAL2" + " (" + (ds.Tables[0].Rows[0]["APPROVAL2"].ToString()) + ")";

            lblApprover3.Text = "APPROVAL3" + " (" + (ds.Tables[0].Rows[0]["APPROVAL3"].ToString()) + ")";

            lblApprover4.Text = "APPROVAL4" + " (" + (ds.Tables[0].Rows[0]["APPROVAL4"].ToString()) + ")";

            lblApprover5.Text = "APPROVAL5" + " (" + (ds.Tables[0].Rows[0]["APPROVAL5"].ToString()) + ")";
        }
        if ((ds.Tables[0].Rows[0]["APPROVAL1"].ToString().ToUpper() == "PARENT") || (ds.Tables[0].Rows[0]["APPROVAL1"].ToString().ToUpper() == "PARENTS"))
        {
            ddlAA1parent.Visible = true;
            ddlAA1parent.SelectedValue = "14";
            ddlAA2Parent.SelectedValue = "0";
            ddlAA1.Visible = false;
            ddlAA2Parent.Visible = false;
            ddlAA2.Visible = true;
            this.EnableDisable(1);
        }
        else if ((ds.Tables[0].Rows[0]["APPROVAL2"].ToString().ToUpper() == "PARENT") || (ds.Tables[0].Rows[0]["APPROVAL2"].ToString().ToUpper() == "PARENTS"))
        {
            ddlAA2Parent.Visible = true;
            ddlAA2Parent.SelectedValue = "14";
            ddlAA1parent.SelectedValue = "0";
            ddlAA2.Visible = false;
            ddlAA1parent.Visible = false;
            ddlAA1.Visible = true;
            this.EnableDisable(2);
        }
        else
        {
            ddlAA1parent.Visible = false;
            ddlAA2Parent.SelectedValue = "0";
            ddlAA1parent.SelectedValue = "0";
            ddlAA1.Visible = true;
            ddlAA2Parent.Visible = false;
            ddlAA2.Visible = true;
        }

    }

    protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        OBJHGPAA.STUDTYPE = ddlStuType.SelectedIndex;
        OBJHGPAA.DAYS = ddlDays.SelectedIndex.ToString();
        DataSet ds = objHGPA.GetLabel(OBJHGPAA);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblApprover1.Text = "APPROVAL1" + " (" + (ds.Tables[0].Rows[0]["APPROVAL1"].ToString()) + ")";

            lblApprover2.Text = "APPROVAL2" + " (" + (ds.Tables[0].Rows[0]["APPROVAL2"].ToString()) + ")";

            lblApprover3.Text = "APPROVAL3" + " (" + (ds.Tables[0].Rows[0]["APPROVAL3"].ToString()) + ")";

            lblApprover4.Text = "APPROVAL4" + " (" + (ds.Tables[0].Rows[0]["APPROVAL4"].ToString()) + ")";

            lblApprover5.Text = "APPROVAL5" + " (" + (ds.Tables[0].Rows[0]["APPROVAL5"].ToString()) + ")";
        }
        if ((ds.Tables[0].Rows[0]["APPROVAL1"].ToString().ToUpper() == "PARENT") || (ds.Tables[0].Rows[0]["APPROVAL1"].ToString().ToUpper() == "PARENTS"))
        {
            ddlAA1parent.Visible = true;
            ddlAA1parent.SelectedValue="14";
            ddlAA2Parent.SelectedValue = "0";
            ddlAA1.Visible = false;
            ddlAA2Parent.Visible = false;
            ddlAA2.Visible = true;
            this.EnableDisable(1);
        }
        else if ((ds.Tables[0].Rows[0]["APPROVAL2"].ToString().ToUpper() == "PARENT") || (ds.Tables[0].Rows[0]["APPROVAL2"].ToString().ToUpper() == "PARENTS"))
        {
            ddlAA2Parent.Visible = true;
            ddlAA2Parent.SelectedValue = "14";
            ddlAA1parent.SelectedValue = "0";
            ddlAA2.Visible = false;
            ddlAA1parent.Visible = false;
            ddlAA1.Visible = true;
            this.EnableDisable(2);
        }
        else
        {
            ddlAA1parent.Visible = false;
            ddlAA2Parent.SelectedValue = "0";
            ddlAA1parent.SelectedValue = "0";
            ddlAA1.Visible = true;
            ddlAA2Parent.Visible = false;
            ddlAA2.Visible = true;
        }
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
                        if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = true;
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = false;
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        }
                    }
                    else
                    {
                        if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            ddlAA3.SelectedIndex = 0;
                            txtAAPath.Text = ddlAA2Parent.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA2.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                        }
                        
                    }
                    break;

                case 2:
                    if (ddlAA2.SelectedIndex == 0)
                    {
                        if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = true;
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                        }
                    }
                    else
                    {
                        if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                             txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2Parent.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                        }
                        else if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString();
                        }
                    }
                    break;

                case 3:
                    if (ddlAA3.SelectedIndex == 0)
                    {
                        ddlAA4.SelectedIndex = 0;
                        ddlAA4.Enabled = false;
                        string swhere = "ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    else
                    {
                        if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2Parent.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        else if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        
                    }
                    break;

                case 4:
                    if (ddlAA4.SelectedIndex == 0)
                    {
                        ddlAA5.SelectedIndex = 0;
                        ddlAA5.Enabled = false;
                        string swhere = "ua_type not in(4)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA4.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA5, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    else
                    {
                        if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA5.Enabled = true;
                            string swhere = "ua_type not in(4)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA5, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2Parent.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                        }
                        else if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA5.Enabled = true;
                            string swhere = "ua_type not in(4)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA5, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA5.Enabled = true;
                            string swhere = "ua_type not in(4)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA4.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA5, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                        }
                    }
                    break;

                case 5:
                    if (ddlAA5.SelectedIndex != 0)
                    {
                        if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2Parent.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString() + "->" + ddlAA5.SelectedItem.ToString() + ".";
                        }
                        else if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString() + "->" + ddlAA5.SelectedItem.ToString() + ".";
                        }
                        else
                        {
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString() + "->" + ddlAA5.SelectedItem.ToString() + ".";
                        }
                    }
                    break;
            }
        }
        else
        {
            switch (index)
            {
                case 1:
                    if (ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1parent.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = true;
                            ddlAA3.SelectedIndex = 0;
                            string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1parent.SelectedItem.ToString();
                        }
                        else if (ddlAA1.SelectedIndex == 0)
                     {
                        ddlAA2.SelectedIndex = 0;
                        ddlAA2.Enabled = false;
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = false;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                    }
                    else if (ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2Parent.SelectedItem.Text.ToUpper() == "PARENTS")
                    {
                        ddlAA2.SelectedIndex = 0;
                        ddlAA2.Enabled = false;
                        ddlAA3.SelectedIndex = 0;
                        ddlAA3.Enabled = true;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                    }
                    else
                    {
                        ddlAA2.Enabled = true;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");

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
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    else
                    {
                        ddlAA3.Enabled = true;
                        string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                        objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
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

    //private void FillAAuthority()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.FillAAuthority ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void clearnew()
    {
        ddlApp.SelectedIndex = 0;
        ddldepartment.SelectedIndex = 0;
        ddlHostel.SelectedIndex = 0;
        ddlAA1.SelectedIndex = 0;
        ddlAA2.SelectedIndex = 0;
        ddlAA3.SelectedIndex = 0;
        ddlAA4.SelectedIndex = 0;
        ddlAA5.SelectedIndex = 0;
        ddlDays.SelectedIndex = 0;
        //chkDays.Checked = false;
        ddlStuType.SelectedIndex = 0;
        txtAAPath.Text = string.Empty;
        lblApprover1.Text = "Approval 1";
        lblApprover2.Text = "Approval 2";
        lblApprover3.Text = "Approval 3";
        lblApprover4.Text = "Approval 4";
        lblApprover5.Text = "Approval 5";
        ddlAA1parent.Visible = false;  //Added By Himanshu Tamrakar on date 16-NOV-2023
        ddlAA2Parent.Visible = false;
        ddlAA1.Enabled = true;
        ddlAA2.Enabled = true;
        ddlAA1.Visible = true;
        ddlAA2.Visible = true;

    }
    
    private void clearnewAuthApproval()
    {
        ddlStuTypeAuth.SelectedIndex = 0;
        ddlDaysAuth.SelectedIndex = 0;
        txtauthapproval1.Text = string.Empty;
        txtauthapproval2.Text = string.Empty;
        txtauthapproval3.Text = string.Empty;
        txtauthapproval4.Text = string.Empty;
        txtauthapproval5.Text = string.Empty;
        ViewState["actionauth"] = "add";
        ViewState["APP_NO"] = null;
        ddlStuTypeAuth.Enabled = true;
        ddlDaysAuth.Enabled = true;
       
        
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
                ddlStuTypeAuth.SelectedValue = ds.Tables[0].Rows[0]["STUDENT_TYPE_ID"].ToString();
                ddlDaysAuth.SelectedValue = ds.Tables[0].Rows[0]["DAYS_TYPE_ID"].ToString();
                txtauthapproval1.Text = ds.Tables[0].Rows[0]["APPROVAL1"].ToString();
                txtauthapproval2.Text = ds.Tables[0].Rows[0]["APPROVAL2"].ToString();
                txtauthapproval3.Text = ds.Tables[0].Rows[0]["APPROVAL3"].ToString();
                txtauthapproval4.Text = ds.Tables[0].Rows[0]["APPROVAL4"].ToString();
                txtauthapproval5.Text = ds.Tables[0].Rows[0]["APPROVAL5"].ToString();

                ddlStuTypeAuth.Enabled = false;
                ddlDaysAuth.Enabled = false;
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
//                objCommon.FillDropDownList(ddlApp, "ACD_HOSTEL_AUTHORITY_APPROVAL_MASTER", "APP_NO", "AUTHORITY_TYPE", "APP_NO= APP_NO", "APP_NO");
                objCommon.FillDropDownList(ddlApp, "ACD_HOSTEL_AUTH_APPROVAL_TYPE_MASTER", "AUTHORITY_APPROVAL_NO", "AUTHORITY_APPROVAL_TYPE", "AUTHORITY_APPROVAL_NO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "AUTHORITY_APPROVAL_NO");
                ddlApp.SelectedValue = ds.Tables[0].Rows[0]["AUTHORITY_TYPE_NO"].ToString();

                //ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim().Equals(string.Empty) || ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim().Equals(string.Empty) ? "0" : ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim();
                ddlHostel.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_NO"].ToString().Trim().Equals(string.Empty) || ds.Tables[0].Rows[0]["HOSTEL_NO"].ToString().Trim().Equals(string.Empty) ? "0" : ds.Tables[0].Rows[0]["HOSTEL_NO"].ToString().Trim();
                ddlStuType.SelectedValue = ds.Tables[0].Rows[0]["STUDENT_TYPE_ID"].ToString().Trim().Equals(string.Empty) || ds.Tables[0].Rows[0]["STUDENT_TYPE_ID"].ToString().Trim().Equals(string.Empty) ? "0" : ds.Tables[0].Rows[0]["STUDENT_TYPE_ID"].ToString().Trim();
                ddlDays.SelectedValue = ds.Tables[0].Rows[0]["DAYS"].ToString().Trim().Equals(string.Empty) || ds.Tables[0].Rows[0]["DAYS"].ToString().Trim().Equals(string.Empty) ? "0" : ds.Tables[0].Rows[0]["DAYS"].ToString().Trim();

                //DataSet dt = objCommon.FillDropDown("ACD_HOSTEL_AUTHORITY_APPROVAL_MASTER", "APP_NO", "DAYS", "APP_NO=" + ViewState["APP_NO"], "");
                //BindDays(dt.Tables[0].Rows[0]);

                objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_DESIG +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", "", "UA_NO");
                txtAAPath.Text = ds.Tables[0].Rows[0]["APP_PATH"].ToString();
                if ((ds.Tables[0].Rows[0]["APPROVAL_1_UANO"].ToString().Trim().Equals("14")))
                {
                    ddlAA1parent.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_1_UANO"].ToString();
                    ddlAA1parent.Visible = true;
                    ddlAA1.Visible = false;
                    ddlAA2Parent.Visible = false;
                    this.EnableDisable(1);
                }
                else
                {
                    ddlAA1.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_1_UANO"].ToString();
                    ddlAA1parent.Visible = false;
                    ddlAA1.Visible = true;
                    ddlAA2Parent.Visible = true;
                    this.EnableDisable(1);
                }
                ddldepartment.SelectedValue = ds.Tables[0].Rows[0]["dept_noaa"].ToString();
                //this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["APPROVAL_2_UANO"].ToString().Trim().Equals("0")))
                {
                    if ((ds.Tables[0].Rows[0]["APPROVAL_2_UANO"].ToString().Trim().Equals("14")))
                    {
                        ddlAA2.Visible = false;
                        ddlAA2Parent.Visible = true;
                        ddlAA2Parent.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_2_UANO"].ToString();
                        this.EnableDisable(2);
                    }
                    else
                    {
                        ddlAA2.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_2_UANO"].ToString();
                        this.EnableDisable(2);
                        ddlAA2.Enabled = true;
                    }
                }

                if (!(ds.Tables[0].Rows[0]["APPROVAL_3_UANO"].ToString().Trim().Equals("0")))
                {
                    ddlAA3.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_3_UANO"].ToString();
                    this.EnableDisable(3);
                    ddlAA3.Enabled = true;
                }

                if (!(ds.Tables[0].Rows[0]["APPROVAL_4_UANO"].ToString().Trim().Equals("0")))
                {
                    ddlAA4.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_4_UANO"].ToString();
                    this.EnableDisable(4);
                    ddlAA4.Enabled = true;
                }

                if (!(ds.Tables[0].Rows[0]["APPROVAL_5_UANO"].ToString().Trim().Equals("0")))
                {
                    ddlAA5.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_5_UANO"].ToString();
                    this.EnableDisable(5);
                    ddlAA5.Enabled = true;
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

    //private void GetDays()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_DAYS", "DAY_TYPE_CODE", "DAY_TYPE_NAME", "", "DAY_TYPE_NO");
    //    if (ds != null && ds.Tables.Count > 0)
    //    {
    //        cblstDays.DataTextField = "DAY_TYPE_NAME";
    //        cblstDays.DataValueField = "DAY_TYPE_CODE";
    //        cblstDays.DataSource = ds.Tables[0];
    //        cblstDays.DataBind();
    //    }
    //}
    
    //private void BindDays(DataRow dr)
    //{
    //    string[] daynolist = dr["DAYS"].ToString().Split(',');

    //    foreach (string dayno in daynolist)
    //    {
    //        foreach (ListItem li in cblstDays.Items)
    //        {
    //            if (li.Value == dayno)
    //            {
    //                li.Selected = true;
    //            }
    //        }
    //    }
    //}
    #endregion Private Methods

}