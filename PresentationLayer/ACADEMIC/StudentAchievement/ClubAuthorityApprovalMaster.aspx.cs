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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

using IITMS.UAIMS.BusinessLayer.BusinessLogic.BusinessLogicLayer.BusinessLogic.Academic;



public partial class ClubAuthorityApprovalMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AuthorityApproval OBJAAP = new AuthorityApproval();
    AuthorityApprovalcontroller objAAM = new AuthorityApprovalcontroller();

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
                //CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                pnlAdd.Visible = false;
                pnlEmpList.Visible = false;
                pnlbtn.Visible = false;
                pnlList.Visible = true;
                FillAAuthority();
                BindListViewAAMaster();
                objCommon.FillDropDownList(ddlclub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
                ViewState["action"] = "add";
            }
        }
    }
      private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AuthorityApprovalMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AuthorityApprovalMaster.aspx");
        }
    }


    protected void BindListViewAAMaster()
    {
        try
        {
            AuthorityApprovalcontroller OBJAA = new AuthorityApprovalcontroller();
            DataSet ds = OBJAA.clubGetAllAAMaster();
             
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
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.BindListViewAAMaster ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    private void FillAAuthority()
    {
        try
        {
            objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_NAME", "UA_TYPE NOT IN (2)", "UA_NO");
            //objCommon.FillDropDownList(ddlCollege, "USER_ACC CROSS APPLY STRING_SPLIT(UA_COLLEGE_NOS, ',') INNER JOIN ACD_COLLEGE_MASTER CM ON (CM.COLLEGE_ID=VALUE)", "DISTINCT value AS COLLEGE_ID", "COLLEGE_NAME", "value > 0", "COLLEGE_NAME");
            //objCommon.FillDropDownList(ddlclub, "ACD_CLUB_ACTIVITY_REGISTRATION  R INNER JOIN ACD_CLUB_MASTER cm ON (R.CLUBACTIVITY_TYPE=cm.CLUB_ACTIVITY_NO) ", "CM.CLUB_ACTIVITY_NO", "CM.CLUB_ACTIVITY_TYPE", "", "R.CLUB_NO");
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.FillAAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private void EnableDisable(int index)
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
                            ddlAA4.SelectedIndex = 0;
                            ddlAA4.Enabled = false;
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");
                        }
                        else
                        {

                            ddlAA2.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            //string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");

                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            ddlAA4.SelectedIndex = 0;
                            ddlAA4.Enabled = false;
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                        }

                        break;

                    case 2:
                        if (ddlAA2.SelectedIndex == 0)
                        {
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            ddlAA4.SelectedIndex = 0;
                            ddlAA4.Enabled = false;
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");
                            ddlAA4.SelectedIndex = 0;
                            ddlAA4.Enabled = false;
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString();
                        }
                        break;
                    case 3:
                        if (ddlAA3.SelectedIndex == 0)
                        {
                            ddlAA4.SelectedIndex = 0;
                            ddlAA4.Enabled = false;
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN  (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN  (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }

                        break;
                    case 4:
                        if (ddlAA4.SelectedIndex == 0)
                        {
                            ddlAA5.SelectedIndex = 0;
                            ddlAA5.Enabled = false;
                            string swhere = "ua_type not in(1,2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + "," + ddlAA4.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA5, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA5.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN  (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + "," + ddlAA4.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA5, "user_acc", "UA_NO", "UA_NAME", swhere, "UA_NO");

                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                        }
                        break;
                    case 5:
                        if (!(ddlAA4.SelectedIndex == 0))
                        {
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString() + "->" + ddlAA5.SelectedItem.ToString();
                        }
                        break;
                
            }
        
    }
    private void Clear()
    {
        //ddlLeavename.SelectedIndex = 0;
        ddlclub.SelectedIndex = 0;
        ddlAA1.SelectedIndex = 0;
        ddlAA2.SelectedIndex = 0;
        ddlAA3.SelectedIndex = 0;
        ddlAA4.SelectedIndex = 0;
        ddlAA5.SelectedIndex = 0;
        ddlAA2.Enabled = false;
        ddlAA3.Enabled = false;
        ddlAA4.Enabled = false;
        ddlAA5.Enabled = false;
        txtAAPath.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
    }
    protected void ddlclub_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillAAuthority();
    }

    protected void ddlAA1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ddlAA1_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlAA2_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ddlAA12_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlAA3_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ddlAA3_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlAA4_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ddlAA4_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlAA5_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ddlAA5_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   
    private void clearnew()
    {
        ddlclub.SelectedIndex = 0;
        ddlAA1.SelectedIndex = 0;
        ddlAA2.SelectedIndex = 0;
        ddlAA3.SelectedIndex = 0;
        ddlAA4.SelectedIndex = 0;
        ddlAA5.SelectedIndex = 0;
        ddlAA2.Enabled = false;
        ddlAA3.Enabled = false;
        ddlAA4.Enabled = false;
        ddlAA5.Enabled = false;
        txtAAPath.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["APP_NO"] = null;
        //Session["action"] = null;
        //Session["APP_NO"] = null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearnew();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clearnew();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlbtn.Visible = true;
        //ddlAA2.Enabled = false;
        //ddlAA3.Enabled = false;
        //ddlAA4.Enabled = false;
        //ddlAA5.Enabled = false;
        //ViewState["action"] = "add";
        //Session["APP_NO"] = null;
        pnlAAPaList.Visible = false;
    }
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlbtn.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int APPNO = int.Parse(btnEdit.CommandArgument);
            ViewState["APP_NO"] = Convert.ToInt32(btnEdit.CommandArgument);
            //Session["APP_NO"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails(APPNO);
            ViewState["edit"] = "edit";
            //Session["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            pnlAAPaList.Visible = false;           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            AuthorityApprovalcontroller OBJAA = new AuthorityApprovalcontroller();
            OBJAAP.CLUB_ACTIVITY_NO = Convert.ToInt32(ddlclub.SelectedValue);
            OBJAAP.CLUB_ACTIVITY_TYPE = ddlclub.SelectedItem.Text;

            if (ddlAA1.SelectedValue != string.Empty)
            {
                OBJAAP.APPROVAL_1 = Convert.ToInt32(ddlAA1.SelectedValue);
            }
            else
            {
                OBJAAP.APPROVAL_1 = 0;
            }
            string ua_type = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + OBJAAP.APPROVAL_1);
            if (ddlAA2.SelectedValue != string.Empty)
            {
                OBJAAP.APPROVAL_2 = Convert.ToInt32(ddlAA2.SelectedValue);
            }
            else
            {
                OBJAAP.APPROVAL_2 = 0;
            }

            if (ddlAA3.SelectedValue != string.Empty)
            {
                OBJAAP.APPROVAL_3 = Convert.ToInt32(ddlAA3.SelectedValue);
            }
            else
            {
                OBJAAP.APPROVAL_3 = 0;
            }
            if (ddlAA4.SelectedValue != string.Empty)
            {
                OBJAAP.APPROVAL_4 = Convert.ToInt32(ddlAA4.SelectedValue);
            }
            else
            {
                OBJAAP.APPROVAL_4 = 0;
            }
            if (ddlAA5.SelectedValue != string.Empty)
            {
                OBJAAP.APPROVAL_5 = Convert.ToInt32(ddlAA5.SelectedValue);
            }
            else
            {
                OBJAAP.APPROVAL_5 = 0;
            }
            OBJAAP.AAPATH = Convert.ToString(txtAAPath.Text);
            OBJAAP.CREATED_BY = 1;
            OBJAAP.IP_ADDRESS = "::1";
            //int deptNo = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_COLLEGE_NOS IN(" + "'" + ddlCollege.SelectedValue + "')"));


            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)OBJAA.clubAddAAPath(OBJAAP, Convert.ToInt32(ddlCollege.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //ViewState["actionRoom"] = "add";
                        clearnew();
                        BindListViewAAMaster();
                        objCommon.DisplayMessage(this.updPanel, "Record add sucessfully", this.Page);
                        // BindListviewRoomIntake();
                    }
                    else
                    {
                        clearnew();
                        BindListViewAAMaster();
                        objCommon.DisplayMessage(this.updPanel, "Record Already Exist !", this.Page);
                        //BindListviewRoomIntake();
                    }

                }
                else
                {

                    if (ViewState["APP_NO"] != null)
                    {
                        OBJAAP.APP_NO = Convert.ToInt32(ViewState["APP_NO"].ToString());
                        CustomStatus cs = (CustomStatus)OBJAA.clubUpdateAAPath(OBJAAP, Convert.ToInt32(ddlCollege.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //ViewState["action"] = null;
                            clearnew();
                            BindListViewAAMaster();
                            objCommon.DisplayMessage(this.updPanel, "Record Updated sucessfully", this.Page);
                            btnSave.Text = "Submit";
                        }
                        else
                        {
                            //ViewState["action"] = null;
                            clearnew();
                            BindListViewAAMaster();
                            objCommon.DisplayMessage(this.updPanel, "Record Already Exist !", this.Page);
                            btnSave.Text = "Submit";
                        }

                    }
                }

            }
        }

        //    if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
        //    {
        //        //Edit 
        //        OBJAAP.APP_NO = Convert.ToInt32(Session["APP_NO"]);
        //        CustomStatus cs = (CustomStatus)OBJAA.clubUpdateAAPath(OBJAAP, Convert.ToInt32(ddlCollege.SelectedValue));
        //        if (cs.Equals(CustomStatus.RecordUpdated))
        //        {
        //            clearnew();
        //            BindListViewAAMaster();
        //            objCommon.DisplayMessage(this.Page, "Record Updated sucessfully", this.Page);

        //        }
        //    }
        //    else
        //    {
        //        if (ViewState["action"].ToString().Equals("add"))
        //        {
        //            //Add New
        //            CustomStatus cs = (CustomStatus)OBJAA.clubAddAAPath(OBJAAP, Convert.ToInt32(ddlCollege.SelectedValue));
        //            if (cs.Equals(CustomStatus.RecordSaved))
        //            {

        //                clearnew();
        //                BindListViewAAMaster();
        //                objCommon.DisplayMessage(this.Page, "Record add sucessfully", this.Page);
        //            }
        //            else
        //            {
        //                clearnew();
        //                BindListViewAAMaster();
        //                objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
        //            }
        //        }
        //    }
        //}
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(Int32 APP_NO)
    {
        DataSet ds = null;
        try
        {
            ds = objAAM.clubGetSingleAAPath(APP_NO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["APP_NO"] = APP_NO;
              
                objCommon.FillDropDownList(ddlclub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
                ddlclub.SelectedValue = ds.Tables[0].Rows[0]["CLUB_ACTIVITY_NO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["CLUB_ACTIVITY_NO"].ToString();
                FillAAuthority();
               // ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim().Equals(string.Empty) || ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim().Equals(string.Empty) ? "0" : ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString().Trim();

                objCommon.FillDropDownList(ddlAA1,"user_acc", "UA_NO", "UA_NAME","UA_TYPE NOT IN (2)", "UA_NO");
                txtAAPath.Text = ds.Tables[0].Rows[0]["APP_PATH"].ToString();
                ddlAA1.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_1_UANO"].ToString();
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
                ViewState["action"] = "edit";
                btnSave.Text = "Update";

            }
            else
            {
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        clearnew();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        pnlbtn.Visible = false;
        pnlAAPaList.Visible = true;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
                if (ddlCollege.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "UA_NAME", "UA_TYPE NOT IN (2) AND UA_COLLEGE_NOS IN(" + "'" + ddlCollege.SelectedValue + "'" + ")", "UA_NO");
                }

            
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnShowapprovedstud_Click(object sender, EventArgs e)
    {
        //int userNo = Convert.ToInt32(Session["userno"].ToString());
        //int userType = Convert.ToInt32(Session["usertype"].ToString());

        //try
        //{
        //    DataSet dsColleges = objSC.GetCollegesByUser_For_NoDues(Convert.ToInt32(Session["userno"]));
        //    string college = string.Empty;
        //    string actual_College = string.Empty;
        //    if (dsColleges.Tables.Count > 0)
        //    {
        //        lvclubRegapprove.DataSource = ds;
        //        lvclubRegapprove.DataBind();
        //        foreach (ListViewDataItem item in lvclubRegapprove.Items)
        //        {

        //            CheckBox status = item.FindControl("chkapprove") as CheckBox;
        //            Label lblstatus = item.FindControl("lbltype") as Label;

        //            string approve = lblstatus.ToolTip;

        //            if (approve == "1")
        //            {
        //                status.Checked = true;
        //                status.Enabled = false;

        //            }
        //            if (approve == "0")
        //            {
        //                status.Checked = false;

        //            }

        //        }

        //    }
        //    else
        //    {
        //        lvclubRegapprove.DataSource = null;
        //        lvclubRegapprove.DataBind();
        //        objCommon.DisplayMessage(this.Page, "No Record Found for current selection", this);

        //    }


        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }


   
}