// =============================================
// PRODUCT NAME : UAIMS
// MODULE NAME  : DISPATCH
// CREATED BY   : MRUNAL SINGH     
// CREATED DATE : 20-NOV-2015
// DESCRIPTION  : USED TO BIND USERS
// =============================================
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

public partial class Complaints_MASTER_CommitteeCellUser : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ComplaintController objCC = new ComplaintController();   
    Complaint objCT = new Complaint();   

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
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //Bind the ListView with Domain
                BindListViewCreateUser();
                //objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "");
                objCommon.FillDropDownList(ddlDept, "COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME", "DEL_STATUS=0 AND FLAG_SP=1", "DEPTNAME");
                ViewState["action"] = "add";
            }
        }
    }

    private void BindListViewCreateUser()
    {
        try
        {
            DataSet dscreateuser = objCC.GetCommitteeUsers();

            if (dscreateuser.Tables[0].Rows.Count > 0)
            {
                lvCreateUser.DataSource = dscreateuser;
                lvCreateUser.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_CommitteeCellUser.BindListViewCreateUser-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int user_no = int.Parse(btnEdit.CommandArgument);
            ShowDetails(user_no);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_CommitteeCellUser.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int user_no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_COMMITTEE_USER", "EMPDEPTNO, EMPID", "USERNO", "USERNO=" + user_no, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["user_no"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
                ddlDept.Text = ds.Tables[0].Rows[0]["EMPDEPTNO"].ToString();
               // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "isnull(FNAME,'')+''+isnull(MNAME,'')+''+isnull(LNAME,'') AS NAME", "SUBDEPTNO=" + ddlDept.SelectedValue, "IDNO");
                objCommon.FillDropDownList(ddlEmployee, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (3,4,5) AND UA_DEPTNO=" + ddlDept.SelectedValue, "UA_FULLNAME");                
                ddlEmployee.Text = ds.Tables[0].Rows[0]["EMPID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_CommitteeCellUser.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objCT.EMP_DEPT = Convert.ToInt32(ddlDept.SelectedValue);
            objCT.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
            objCT.Ua_No = Convert.ToInt32(Session["userno"]);
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objCC.AddUpdateUser(objCT);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        BindListViewCreateUser();
                        objCommon.DisplayMessage(UpdatePanel1, "Record Save Successfully.", this.Page);
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {

                        objCommon.DisplayMessage(UpdatePanel1, "Record Already Exist.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["user_no"] != null)
                    {
                        objCT.USERNO = Convert.ToInt32(ViewState["user_no"].ToString());
                        CustomStatus cs = (CustomStatus)objCC.AddUpdateUser(objCT);
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
                objUCommon.ShowError(Page, "Complaints_MASTER_CommitteeCellUser.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = "add";
    }
  
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewCreateUser();
    }

    private void Clear()
    {
        ddlDept.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["user_no"] = null;
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
            {
               // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "isnull(FNAME,'')+''+isnull(MNAME,'')+''+isnull(LNAME,'') AS NAME", "SUBDEPTNO=" + ddlDept.SelectedValue, "IDNO");
                objCommon.FillDropDownList(ddlEmployee, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (3,4,5) AND UA_NO NOT IN (SELECT DISTINCT EMPID FROM COMPLAINT_COMMITTEE_USER WHERE EMPDEPTNO NOT IN (" + Convert.ToInt32(ddlDept.SelectedValue) + "))", "UA_FULLNAME");
            } // 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Complaints_MASTER_CommitteeCellUser.ddlDept_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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