//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// PAGE NAME     : BULK USER CREATION OF EMPLOYEES                                  
// CREATION DATE : 19-Aug-2009                                                     
// CREATED BY    :  G.V.S.KIRAN KUMAR                                              
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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

public partial class ADMINISTRATION_Bulk_User_Id_Creation_Employees : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ConnectionString
   // private string nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
    
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
            //Populate DropDownLists
            PopulateDropDown();
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlEmployeeType.SelectedIndex = 0;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int id = 0;
        int count = 0;
        try
        {
            User_AccController objACC = new User_AccController();
            UserAcc objUA = new UserAcc();
            PayController objpay = new PayController();

            DataSet ds = objpay.GetEmployeeForUserCreation(Convert.ToInt32(ddlEmployeeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue));
            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                     foreach (ListViewDataItem itm in lvStudents.Items)
                    {
                        CheckBox chk = itm.FindControl("chkRow") as CheckBox;
                        Label lblreg = itm.FindControl("lblreg") as Label;
                        HiddenField hdnf = itm.FindControl("hidStudentId") as HiddenField;
                        Label lblstud = itm.FindControl("lblstud") as Label;

                        if (chk.Checked == true && (chk.Enabled == true))
                        {
                            //objUA.UA_IDNo = Convert.ToInt32(dtr["IDNO"]);
                            objUA.UA_IDNo = Convert.ToInt32(hdnf.Value);
                            id = objUA.UA_IDNo;
                            //objUA.UA_Name = Convert.ToString(dtr["PFILENO"]);
                            objUA.UA_Name = lblreg.Text;
                            //dtr["pfileno"].ToString();
                            //string[] name = dtr["NAME"].ToString().Split(' ');
                            string pwd = string.Empty;

                            pwd = lblreg.Text;
                            objUA.UA_Pwd = Common.EncryptPassword(pwd);
                           // objUA.UA_FullName = dtr["NAME"].ToString();
                            objUA.UA_FullName = lblstud.Text;
                            objUA.UA_Status = 0;
                            objUA.UA_Type = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                            //objUA.UA_DeptNo = Convert.ToInt32(dtr["subdeptno"]);
                            string deptno = objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + hdnf.Value +"");
                            objUA.UA_DeptNo = deptno;
                            objACC.AddEmployeeUser(objUA);
                        }
                        if (chk.Checked == true)//chk.Enabled == true && 
                        {
                            count++;
                        }
                    }
                }
            }
            ShowMessage("Login Created Successfully");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.btnModify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO > 0", "STAFFNO ASC");
            objCommon.FillDropDownList(ddlEmployeeType, "User_Rights", "usertypeid", "userdesc", "usertypeid > 0 and usertypeid in (3,4,5)", "usertypeid DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "CASE WHEN PFILENO IS NULL THEN '-' ELSE PFILENO END PFILENO,FNAME+' '+MNAME+' '+LNAME AS NAME,ISNULL(LOGIN_STATUS,0) AS LOGIN_STATUS", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STAFFNO=" + Convert.ToInt32(ddlStaff.SelectedValue) + " AND UA_TYPE=" + Convert.ToInt32(ddlEmployeeType.SelectedValue), "IDNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds.Tables[0];
            lvStudents.DataBind();
            //pnllistview.Visible = true;
            lvStudents.Visible = true;
            btnUpdate.Enabled = true;
        }
        else
        {
            objCommon.DisplayMessage("No record found!", this.Page);
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            //pnllistview.Visible = false;
            lvStudents.Visible = false;
            btnUpdate.Enabled = false;

        }
    }
}
