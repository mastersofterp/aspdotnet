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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Bind the ListView with Domain
                BindListViewCreateUser();

                FillDepartment();            
                FillEntryFor();      

                //Disabled add panal
                pnlAdd.Visible = false;

                //Enabled listview panal
                pnlList.Visible = true;               
            }
        }
    }
        
    private void BindListViewCreateUser()
    {
        try
        {   
            ComplaintController objCC = new ComplaintController();
            DataSet dscreateuser = objCC.GetAllCreateUsers();

            if (dscreateuser.Tables[0].Rows.Count > 0)
            {
                lvCreateUser.DataSource = dscreateuser;
                lvCreateUser.DataBind();
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
           pnlAdd.Visible = true;
           pnlList.Visible = false;
           
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
            ComplaintController objcc = new ComplaintController();
            SqlDataReader dr = objcc.GetSingleCreateUsers(c_no);

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
            ComplaintController objCU = new ComplaintController();           
            Complaint objCreateUser= new Complaint();
            
            objCreateUser.Deptid = Convert.ToInt32(ddlRMDept.SelectedValue);
            objCreateUser.Ua_No = Convert.ToInt32(ddlRMEmp.SelectedValue);                       
            objCreateUser.EmpId = Convert.ToInt32(ddlRMentryfor.SelectedValue);          
            if (ChkRMAdmin.Checked == true)            
                objCreateUser.C_Status = "A";            
            else
                objCreateUser.C_Status = "E";

            //Check whether to add or update
            if (ViewState["action"] != null)
            {    
                if (ViewState["action"].ToString().Equals("add"))
                {                   
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objCU.AddComplaintUser(objCreateUser);
                    
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();                       
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["c_no"] != null)
                    {
                        objCreateUser.C_No = Convert.ToInt32(ViewState["c_no"].ToString());
                        CustomStatus cs = (CustomStatus)objCU.UpdateComplaintUser(objCreateUser);
                    
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;                        
                            pnlList.Visible = true;                            
                            ViewState["action"] = null;                            
                            Clear();
                        }
                    }
                }
            }
            //BindListViewCreateUser();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "CreateUser.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else                
                 objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
        
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlList.Visible = true;        
        ViewState["action"] = null;        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        lblchk.Text = "Employee";
        ViewState["action"]="add";              
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain
        BindListViewCreateUser();
    }
    
    private void FillDepartment()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", string.Empty, "DEPTID");
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
            ComplaintController objCC = new ComplaintController();            
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

    private void FillEntryFor()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_REPAIR_MAINTAINANCE_SP_DDW_CREATEUSERS_CEMPNO");
            ddlRMentryfor.DataSource = ds;
            ddlRMentryfor.DataValueField = ds.Tables[0].Columns["UA_NO"].ToString();
            ddlRMentryfor.DataTextField = ds.Tables[0].Columns["UA_FULLNAME"].ToString();
            ddlRMentryfor.DataBind();
            ddlRMentryfor.SelectedIndex = -1;
        }
        catch(Exception ex)
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
        lblchk.Text = "";
        ChkRMAdmin.Checked = false;
       
    }
      
    protected void ddlRMDept_SelectedIndexChanged(object sender, EventArgs e)
    {         
        ClearEmployeeAndFill(Convert.ToInt32(ddlRMDept.SelectedValue));
        ddlRMentryfor.SelectedIndex = 0;
        ChkRMAdmin.Checked = false;
        lblchk.Text = "Employee";
    }

    //Clear Emoloyee DDL and Fill again
    private void ClearEmployeeAndFill(Int32 deptid)
    {
        ddlRMEmp.Items.Clear();    
        ddlRMEmp.Items.Add("Please Select");        
        ddlRMEmp.SelectedItem.Value = "-1";        
        FillEmployee(deptid);
    }

    protected void ChkRMAdmin_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkRMAdmin.Checked == true)
            lblchk.Text = "Admin";
        else
            lblchk.Text = "Employee";
    }
    
    protected void ddlRMEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRMentryfor.Text = ddlRMEmp.SelectedValue;
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
    
}