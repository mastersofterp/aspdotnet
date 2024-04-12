using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;

public partial class HOSTEL_GATEPASS_ApprovalbyAdmin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdminApprovalController objAAC = new AdminApprovalController();
    int recid = 0;

    //Below Code Added By Himanshu Tamrakar 02042024
    DateTime Fromdate = DateTime.Now.AddDays(-1);
    DateTime Todate = DateTime.Now.AddDays(7);
    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //commented and added by Himanshu Tamrakar 05042024
                    //BindListView():
                    BindListView(null, 0, Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")), "0");

                    
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Master_GatePassRequest.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Page Events

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ApprovalbyAdmin.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ApprovalbyAdmin.aspx");
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            bool isChecked = false;
            foreach (ListViewDataItem item in lvGatePass.Items)
            {
                CheckBox Ischeck = item.FindControl("chkApprove") as CheckBox;
                if (Ischeck.Checked)
                {
                    isChecked = true;
                    break;
                }
            }

            if (isChecked)
            {
                ViewState["action"] = "checked";
            }
            else
            {
                objCommon.DisplayMessage("Please select at least one record.", this.Page);
                ViewState["action"] = "not_checked";
            }

            if (ViewState["action"] == "checked")
            {
                //int Approve = 0;
                string Approve = "";
                string Remark="";
                int recid = 0;
                CustomStatus cs = new CustomStatus();
                foreach (ListViewDataItem item in lvGatePass.Items)
                {
                    CheckBox chkApprove = item.FindControl("chkApprove") as CheckBox;
                    HiddenField hidrecid = item.FindControl("hidrecid") as HiddenField;

                    //if (chkApprove.Checked)
                    //{
                    //    Approve = 1;
                    //}
                    //else
                    //{
                    //    Approve = 0;
                    //}
                    if (chkApprove.Checked)
                    {
                        Remark = txtRemark.Text;
                        Approve = ddlremark.SelectedValue;
                        recid = Convert.ToInt16(hidrecid.Value);
                        cs = (CustomStatus)objAAC.UpdateApproval(recid, Approve, Remark);
                    }
                   
                }

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage("Record Saved successfully.", this.Page);

                    //commented and added by Himanshu Tamrakar 05042024
                    //BindListView():
                    BindListView(null, 0, Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")), "0");
                    //Added by Himanshu Tamrakar 24/11/2023 for bug id 169646
                    Clear();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindListView(string Applydate, int Purpose, string Todate, string Fromdate, string Status)
    {
        try
        {
            DataSet ds = objAAC.GetAllGatePass(Applydate, Purpose, Todate, Fromdate, Status);
            lvGatePass.DataSource = ds;
            lvGatePass.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelPurpose.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        changeApproval.Visible = false;
        btnChangeApproval.Visible = false; 
        btnUpdatePath.Visible = false;
        finalAproval.Visible = true;
        txtRemark.Text = string.Empty;
        txtName.Text = string.Empty;
        txtAAPath.Text = string.Empty;
        ddlAA1.SelectedIndex = 0;
        ddlAA2.SelectedIndex = 0;
        ddlAA3.SelectedIndex = 0;
        ddlAA4.SelectedIndex = 0;
        btnApprove.Visible = true;
        ddlremark.SelectedIndex = 0;
        //foreach (ListViewItem item in lvGatePass.Items)
        //{
        //    CheckBox chkApprove = (CheckBox)item.FindControl("chkApprove");
        //    if (chkApprove != null)
        //    {
        //        chkApprove.Checked = false;
        //    }
        //}
    }
    protected void btnChangeApproval_Click(object sender, EventArgs e)
    {
         int Checked = 0;
            foreach (ListViewDataItem item in lvGatePass.Items)
            {
                CheckBox chkApprove = item.FindControl("chkApprove") as CheckBox;
                HiddenField hidrecid = item.FindControl("hidrecid") as HiddenField;

                if (chkApprove.Checked)
                {
                    Checked++;
                    recid = Convert.ToInt16(hidrecid.Value);
                }
            }
            if (Checked == 0)
            {
                objCommon.DisplayMessage(this, "Please Select a Record to Update Passing Path.",this);
                return;
            }
            btnApprove.Visible = false;
            finalAproval.Visible = false;
            changeApproval.Visible = true;
            btnChangeApproval.Visible = false;
            btnUpdatePath.Visible = true;
            
            if (Checked == 1)
            {
                //Added by Himanshu Tamrakar 24/11/2023
                txtName.Text = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN ACD_STUDENT S ON (HGD.IDNO = S.IDNO)", "S.STUDNAME", "HGP_ID=" + recid);
                string p_path = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "APPROVAL_PASSING_PATH", "HGP_ID=" + recid);
                string fst_apr = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "FIRST_APPROVAL_UANO", "HGP_ID=" + recid);
                string scnd_apr = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "SECOND_APPROVAL_UANO", "HGP_ID=" + recid);
                string trd_apr = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "THIRD_APPROVAL_UANO", "HGP_ID=" + recid);
                string frth_apr = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "FOURTH_APPROVAL_UANO", "HGP_ID=" + recid);
                txtName.Text = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN ACD_STUDENT S ON (HGD.IDNO = S.IDNO)", "S.STUDNAME", "HGP_ID=" + recid);

                if (fst_apr == "14")
                {
                    objCommon.FillDropDownList(ddlAA1, "User_Rights", "USERTYPEID", "USERDESC", "USERTYPEID=14", "USERTYPEID");
                    objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", "", "UA_NO");
                }
                else if (scnd_apr == "14")
                {
                    objCommon.FillDropDownList(ddlAA2, "User_Rights", "USERTYPEID", "USERDESC", "USERTYPEID=14", "USERTYPEID");
                    objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", "", "UA_NO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlAA1, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", "", "UA_NO");
                    objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", "", "UA_NO");
                }

                objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", "", "UA_NO");
                objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", "", "UA_NO");
                // Assign values to variables
                ddlAA1.SelectedValue = fst_apr;
                ddlAA2.SelectedValue = scnd_apr;
                ddlAA3.SelectedValue = trd_apr;
                ddlAA4.SelectedValue = frth_apr;
                string[] pathParts = p_path.Split(new string[] { "->" }, StringSplitOptions.None);

                // Ensure there are at least 4 parts
                if (pathParts.Length >= 4)
                {
                    // Assign values to variables
                    ddlAA1.SelectedItem.Text = Convert.ToString(pathParts[0]);
                    ddlAA2.SelectedItem.Text = Convert.ToString(pathParts[1]);
                    ddlAA3.SelectedItem.Text = Convert.ToString(pathParts[2]);
                    ddlAA4.SelectedItem.Text = Convert.ToString(pathParts[3]);
                }

                //Commented By Himanshu Tamrakar 24/11/2023
                //ddlAA1.SelectedItem.Text = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN USER_ACC UA ON (HGD.FIRST_APPROVAL_UANO = UA.UA_NO)", "UA.UA_DESIG +'-  '+ UA.UA_NAME ", "HGD.HGP_ID=" + recid);
                //ddlAA2.SelectedItem.Text = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN USER_ACC UA ON (HGD.SECOND_APPROVAL_UANO = UA.UA_NO)", "UA.UA_DESIG+'  -'+ UA.UA_NAME", "HGD.HGP_ID=" + recid);
                //ddlAA3.SelectedItem.Text = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN USER_ACC UA ON (HGD.THIRD_APPROVAL_UANO = UA.UA_NO)", "UA.UA_DESIG +' - '+ UA.UA_NAME", "HGD.HGP_ID=" + recid);
                //ddlAA4.SelectedItem.Text = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN USER_ACC UA ON (HGD.FOURTH_APPROVAL_UANO = UA.UA_NO)", "UA.UA_DESIG+'  -'+  UA.UA_NAME", "HGD.HGP_ID=" + recid);
                txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                if ((ddlAA1.SelectedItem.Text).ToUpper() == "PARENT" || (ddlAA1.SelectedItem.Text).ToUpper() == "PARENTS")
                {
                    ddlAA1.Enabled = false;
                    ddlAA2.Enabled = true;

                }
                else if ((ddlAA2.SelectedItem.Text).ToUpper() == "PARENT" || (ddlAA2.SelectedItem.Text).ToUpper() == "PARENTS")
                {
                    ddlAA1.Enabled = true;
                    ddlAA2.Enabled = false;
                }
                //End By Himanshu tamrakar
            }
            if (Checked < 0 || Checked > 1)
            {
                objCommon.DisplayMessage("Please select only one record to change approval", this.Page);
                finalAproval.Visible = true;
                changeApproval.Visible = false;
                btnApprove.Visible = true;
            }

    }

    //(ddlAA1.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1.SelectedItem.Text.ToUpper() == "PARENTS") condition Added by Himanshu Tamrakar 24/11/2023
    private void EnableDisable(int index)  
    {
            switch (index)
            {
                case 1:
                    if (ddlAA1.SelectedIndex == 0)
                    {

                        if (ddlAA1.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1.SelectedItem.Text.ToUpper() == "PARENTS") //Added by Himanshu Tamrakar 24/11/2023
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = true;
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = false;
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        }

                    }
                    else
                    {
                        if (ddlAA2.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            ddlAA3.SelectedIndex = 0;
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString();
                            ddlAA2.SelectedItem.Text = "Parent";
                        }
                        else
                        {
                            ddlAA2.Enabled = true;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                        }
                    }
                            
                    break;

                case 2:
                    if (ddlAA2.SelectedIndex == 0)
                    {
                        if(ddlAA1.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA3.Enabled = true;
                            ddlAA3.SelectedIndex = 0;
                            string swhere = "organizationid=" + Session["OrgId"].ToString() + "and ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() ;
                        }
                        else
                        {
                            ddlAA2.SelectedIndex = 0;
                            ddlAA2.Enabled = false;
                            ddlAA3.SelectedIndex = 0;
                            ddlAA3.Enabled = false;
                            string swhere = "ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA2, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                        }
                            
                    }
                    else
                    {
                        if (ddlAA2.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "organizationid=" + Session["OrgId"].ToString() + "and ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                        }
                        
                        else
                        {
                            ddlAA3.Enabled = true;
                            string swhere = "organizationid=" + Session["OrgId"].ToString() + "and ua_type not in(2)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA3, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
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
                        objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME", swhere, "UA_NO");
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString();
                    }
                    else
                    {
                        if (ddlAA2.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA2.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "organizationid=" + Session["OrgId"].ToString() + "and ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        else if (ddlAA1.SelectedItem.Text.ToUpper() == "PARENT" || ddlAA1.SelectedItem.Text.ToUpper() == "PARENTS")
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "organizationid=" + Session["OrgId"].ToString() + "and ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                        else
                        {
                            ddlAA4.Enabled = true;
                            string swhere = "organizationid=" + Session["OrgId"].ToString() + "and ua_type not in(3)" + " and UA_NO NOT IN (" + ddlAA1.SelectedValue + "," + ddlAA2.SelectedValue + "," + ddlAA3.SelectedValue + ")";
                            objCommon.FillDropDownList(ddlAA4, "user_acc", "UA_NO", "ISNULL(UA_DESIG,'') +' - '+ UA_FULLNAME COLLATE DATABASE_DEFAULT AS UANAME", swhere, "UA_NO");
                            txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString();
                        }
                            
                    }
                    break;

                case 4:
                    if (ddlAA4.SelectedIndex != 0)
                    {
                        txtAAPath.Text = ddlAA1.SelectedItem.ToString() + "->" + ddlAA2.SelectedItem.ToString() + "->" + ddlAA3.SelectedItem.ToString() + "->" + ddlAA4.SelectedItem.ToString();
                    }
                    break;
            }
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
                objCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA1_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
                objCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA12_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
                objCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA3_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
                objCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA4_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
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
                objCommon.ShowError(Page, "HostelGatePassAuthApprovalMaster.ddlAA5_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Commented By Himanshu Tamrakar 11-04-2024
    //protected void lvGatePass_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        // Find the controls within the ListViewItem
    //        CheckBox chkApprove = (CheckBox)e.Item.FindControl("chkApprove");
    //        Label lblStatus = (Label)e.Item.FindControl("lblStatus");
    //        Label lbloutdate = (Label)e.Item.FindControl("lblOutdate");
    //        if (Convert.ToDateTime(lbloutdate.Text)<DateTime.Now.AddDays(-1))
    //        {
    //            chkApprove.Enabled = false;
    //        }
    //        else
    //        {
    //            chkApprove.Enabled = true;
    //        }
    //    }
    //}

    protected void btnUpdatePath_Click(object sender, EventArgs e)
    {
        int Checked = 0;
        int AA1 = 0;
        int AA2 = 0;
        int AA3 = 0;
        int AA4 = 0;
        CustomStatus cs = new CustomStatus();
        foreach (ListViewDataItem item in lvGatePass.Items)
        {

            CheckBox chkApprove = item.FindControl("chkApprove") as CheckBox;
            HiddenField hidrecid = item.FindControl("hidrecid") as HiddenField;

            if (chkApprove.Checked)
            {
                Checked++;
                recid = Convert.ToInt16(hidrecid.Value);
            }
        }
        if (Checked == 1)
        {
            //Commented By Himanshu Tamrakar 24/11/2023
            //AA1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + ddlAA1.SelectedItem.Text + "' OR UA_FULLNAME='" + ddlAA1.SelectedItem.Text + "'"));
            //AA2 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + ddlAA2.SelectedItem.Text + "' OR UA_FULLNAME='" + ddlAA2.SelectedItem.Text + "'"));
            //AA3 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + ddlAA3.SelectedItem.Text + "' OR UA_FULLNAME='" + ddlAA3.SelectedItem.Text + "'"));
            //AA4 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NAME='" + ddlAA4.SelectedItem.Text + "' OR UA_FULLNAME='" + ddlAA4.SelectedItem.Text + "'"));

            //Added by Himanshu Tamrakar 24/11/2023
            AA1 = Convert.ToInt32(ddlAA1.SelectedValue);
            AA2 = Convert.ToInt32(ddlAA2.SelectedValue);
            AA3 = Convert.ToInt32(ddlAA3.SelectedValue);
            AA4 = Convert.ToInt32(ddlAA4.SelectedValue);

            if ((ddlAA1.SelectedItem.Text).ToUpper() == "PARENT" || (ddlAA1.SelectedItem.Text).ToUpper() == "PARENTS")
            {
                AA1 = 14;
            }
            else if ((ddlAA2.SelectedItem.Text).ToUpper() == "PARENT" || (ddlAA2.SelectedItem.Text).ToUpper() == "PARENTS")
            {
                AA2 = 14;
            }
            //End By Himanshu Tamrakar
            cs = (CustomStatus)objAAC.UpdateApprovalsAndPath(recid, AA1, AA2, AA3, AA4, txtAAPath.Text);
        }

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
            ViewState["action"] = "add";
            Clear();
        }

        if (Checked < 0 || Checked > 1)
        {
            objCommon.DisplayMessage("Please select only one record to change approval", this.Page);
            finalAproval.Visible = true;
            changeApproval.Visible = false;
            btnApprove.Visible = true;
        }

    }

    //Added By Himanshu Tamrakar 02042024
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtFromDateSearch.Text) > Convert.ToDateTime(txtToDateSearch.Text))
        {
            objCommon.DisplayMessage("To Date is Greater Than From date.", this);
            txtToDateSearch.Text = string.Empty;
            txtFromDateSearch.Text = string.Empty;
            return;
        }
        string Applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? null : DateTime.Parse(txtApplyDate.Text).ToString("yyyy-MM-dd");
        //int Purpose = string.IsNullOrEmpty(ddlPurposeSearch.SelectedValue) ? 0 : Convert.ToInt32(ddlPurposeSearch.SelectedValue);
        string Todate = string.IsNullOrEmpty(txtToDateSearch.Text) ? null : DateTime.Parse(txtToDateSearch.Text).ToString("yyyy-MM-dd");
        string Fromdate = string.IsNullOrEmpty(txtFromDateSearch.Text) ? null : DateTime.Parse(txtFromDateSearch.Text).ToString("yyyy-MM-dd");
        //string Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? null : ddlStatus.SelectedValue;
        this.BindListView(Applydate, 0, Todate, Fromdate, "0");
    }

    //Added By Himanshu Tamrakar 02042024
    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtApplyDate.Text = string.Empty;
        //ddlPurposeSearch.SelectedValue = "0";
        txtToDateSearch.Text = string.Empty;
        txtFromDateSearch.Text = string.Empty;
        //ddlStatus.SelectedValue = "0";
        BindListView(null, 0, Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")), "0");
    }
}