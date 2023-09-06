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

public partial class Estate_R_CreateUser : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ComplaintController objCC = new ComplaintController();
    Complaint objCreateUser = new Complaint();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, System.EventArgs e)
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

                //Bind the ListView with Domain
                BindListViewCreateUser();

                FillDepartment();


                //Disabled add panal
                //pnlAdd.Visible = false;
                //Enabled listview panal
                //pnlList.Visible = true;                 
                ViewState["action"] = "add";
               // ChkRMAdmin.Checked = true;               
                ChkRMAdmin.Enabled = true;
            }
        }
    }

    private void BindListViewCreateUser()
    {
        try
        {

            DataSet dscreateuser = objCC.GetAllCreateUsers();

            if (dscreateuser.Tables[0].Rows.Count > 0)
            {
                lvCreateUser.DataSource = dscreateuser;
                lvCreateUser.DataBind();
                for (int i = 0; i < lvCreateUser.Items.Count; i++)
                {
                    Label lblstatus = lvCreateUser.Items[i].FindControl("lblAsta") as Label;
                    if (dscreateuser.Tables[0].Rows[i]["ACTIVE_STATUS"].ToString() == "DEACTIVE")
                    {
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CreateUser.BindListViewCreateUser-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int c_no = int.Parse(btnEdit.CommandArgument);
            ShowDetails(c_no);
            ViewState["action"] = "edit";
            // pnlAdd.Visible = true;
            //  pnlList.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CreateUser.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int c_no)
    {
        try
        {
            //  ComplaintController objcc = new ComplaintController();
            SqlDataReader dr = objCC.GetSingleCreateUsers(c_no);

            //To show created user details 
            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["c_no"] = c_no.ToString();
                    ClearEmployeeAndFill(Convert.ToInt32(dr["C_DEPTNO"]));
                    ddlRMDept.Text = dr["C_DEPTNO"] == null ? "" : dr["C_DEPTNO"].ToString();
                    ddlRMEmp.Text = dr["C_UANO"] == null ? "" : dr["C_UANO"].ToString();
                    ddlRMentryfor.Text = dr["C_EMPNO"] == null ? "" : dr["C_EMPNO"].ToString();
                    rdoStatus.SelectedValue = dr["C_ACTIVE_STATUS"].ToString();
                    if (dr["C_STATUS"].Equals("A"))
                    {
                        ChkRMAdmin.Checked = true;
                        lblchk.Text = "Admin";
                    }
                    else
                    {
                       
                       lblchk.Text = "Employee";
                    }
                }
            }

            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CreateUser.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // ComplaintController objCU = new ComplaintController();           

            objCreateUser.Deptid = Convert.ToInt32(ddlRMDept.SelectedValue);
            objCreateUser.Ua_No = Convert.ToInt32(ddlRMEmp.SelectedValue);
            objCreateUser.EmpId = Convert.ToInt32(ddlRMentryfor.SelectedValue);
            if (ChkRMAdmin.Checked == true)
            {
                objCreateUser.C_Status = "A";
            }
            else
            {
                objCreateUser.C_Status = "E";
            }
            objCreateUser.C_ACTIVE_STATUS = Convert.ToInt32(rdoStatus.SelectedValue);

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (Duplicate() == true)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist.", this.Page);
                        Clear();
                        return;
                    }
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objCC.AddComplaintUser(objCreateUser);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //pnlAdd.Visible = false;
                        //pnlList.Visible = true;
                        ViewState["action"] = "add";
                        Clear();
                        BindListViewCreateUser();
                        objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully.", this.Page);
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {

                        objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist.", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["c_no"] != null)
                    {
                        objCreateUser.C_No = Convert.ToInt32(ViewState["c_no"].ToString());
                        CustomStatus cs = (CustomStatus)objCC.UpdateComplaintUser(objCreateUser);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            BindListViewCreateUser();
                            objCommon.DisplayMessage(UpdatePanel1, "Record Updated Successfully.", this.Page);
                        }
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist.", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CreateUser.btnSave_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        Clear();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = "add";
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewCreateUser();
    }

    private void FillDepartment()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", "ISNULL(DEL_STATUS,0) = 0 AND FLAG_SP=1", "DEPTNAME");
            ddlRMDept.DataSource = ds;
            ddlRMDept.DataValueField = ds.Tables[0].Columns["DeptId"].ToString();
            ddlRMDept.DataTextField = ds.Tables[0].Columns["Deptname"].ToString();
            ddlRMDept.DataBind();
            ddlRMDept.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_R_CreateUser.FillDepartment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEmployee(int deptid)
    {
        try
        {
            DataSet ds = objCC.GetEmployeeCreateusersCuno(deptid);

            ddlRMEmp.DataSource = ds;
            ddlRMEmp.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
            ddlRMEmp.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
            ddlRMEmp.DataBind();
            ddlRMEmp.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_R_CreateUser.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEntryFor(int deptid)
    {
        try
        {
            // DataSet ds = objCommon.GetDropDownData("PKG_REPAIR_MAINTAINANCE_SP_DDW_CREATEUSERS_CEMPNO");

            DataSet ds = objCC.GetEmployeeCreateusersCuno(deptid);
            ddlRMentryfor.DataSource = ds;
            ddlRMentryfor.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
            ddlRMentryfor.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
            ddlRMentryfor.DataBind();
            ddlRMentryfor.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estate_R_CreateUser.FillEntryFor-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlRMDept.SelectedIndex = 0;
        ddlRMEmp.SelectedIndex = 0;
        ddlRMentryfor.SelectedIndex = 0;
        lblchk.Text = "Employee";
        ChkRMAdmin.Checked = false;
        rdoStatus.SelectedValue = "0";

    }

    protected void ddlRMDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearEmployeeAndFill(Convert.ToInt32(ddlRMDept.SelectedValue));
        ddlRMentryfor.SelectedIndex = 0;
        ChkRMAdmin.Checked = false;
        lblchk.Text = "Employee";
        ddlRMEmp.Focus();
    }

    //Clear Emoloyee DDL and Fill again
    private void ClearEmployeeAndFill(Int32 deptid)
    {
        ddlRMEmp.Items.Clear();
        ddlRMEmp.Items.Add("Please Select");
        ddlRMEmp.SelectedItem.Value = "-1";
        FillEmployee(deptid);
        FillEntryFor(deptid);
    }

    protected void ChkRMAdmin_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkRMAdmin.Checked == true)
            lblchk.Text = "Admin";
        else
            lblchk.Text = "Employee";
        ChkRMAdmin.Focus();
    }

    protected void ddlRMEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRMentryfor.Text = ddlRMEmp.SelectedValue;
        ddlRMentryfor.Focus();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected Boolean Duplicate()
    {
        DataSet ds = null;
        if (ViewState["action"].Equals("add"))
        {
            ds = objCommon.FillDropDown("COMPLAINT_USER", "*", " ", "C_UANO='" + Convert.ToInt32(ddlRMEmp.SelectedValue) + "'", "");
        }
        else
        {
            ds = objCommon.FillDropDown("COMPLAINT_USER", "*", " ", "C_UANO='" + Convert.ToInt32(ddlRMEmp.SelectedValue) + "' AND C_NO !=" + Convert.ToInt32(ViewState["c_no"]), "");
        }
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}