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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using IITMS.NITPRM;


public partial class PAYROLL_MASTERS_Acc_DepartmentMapping_aspx : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    PayController objFTS = new PayController();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                //if (Session["comp_code"] == null)
                //{
                //    Session["comp_set"] = "NotSelected";
                //    objCommon.DisplayMessage("Select company/cash book.", this);
                //    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                //}
                //else
                //{
                // Session["comp_set"] = "";
                //Page Authorization
               // CheckPageAuthorization();

                //divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                ViewState["Action"] = "Add";
                FillDropdown();
                ShowData();

                //}
            }

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=Acc_DepartmentMapping.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=Acc_DepartmentMapping.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=Acc_DepartmentMapping.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=Acc_DepartmentMapping.aspx");
            }
        }
    }
    private void FillDropdown()
    {
        objCommon.FillDropDownList(ddlPayrollDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0", "");
        objCommon.FillDropDownList(ddlAcadDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO<>0 and ACTIVESTATUS = 1", "");
    }

    private void clear()
    {
        ddlAcadDept.SelectedIndex = 0;
        ddlPayrollDept.SelectedIndex = 0;
        ViewState["Action"] = "Add";
        btnSubmit.Text = "Submit";

    }

    private void ShowData()
    {
        DataSet ds = null;
      //  ds = objCommon.FillDropDown("ACC_DEPARTMENT_MAPPING a inner join PAYROLL_SUBDEPT b on a.PAYROLL_DEPTNO=b.SUBDEPTNO", "DEPTNO,ACAD_DEPTNO,ACAD_DEPTNAME", "PAYROLL_DEPTNO,b.SUBDEPT as PAYROLL_DEPTNAME", "", "");
        //   objCommon.FillDropDownList(ddlhod, "PAYROLL_LEAVE_PASSING_AUTHORITY  a inner join User_Acc b on a.UA_NO=b.UA_NO  inner join Payroll_empmas  c on b.UA_IDNO =  c.IDNO ", "a.PANO", "ISNULL(c.PFILENO,'')+'-'+b.UA_FULLNAME+' - '+ a.PANAME as AuthorityName", "", "");
        ds = GetPayACDDepartmentMapping();
        if (ds != null)
        {
            lvDept.DataSource = ds;
            lvDept.DataBind();
        }
    }

    private DataSet GetPayACDDepartmentMapping()
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAY_ACD_DEPARTMENT_MAPPING", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetPayACDDepartmentMapping-> " + ex.ToString());
        }
        return ds;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ret = 0;
        int Dept_no = 0;

        int Acd_DeptNo = Convert.ToInt32(ddlAcadDept.SelectedValue);
        string Acd_DeptName = ddlAcadDept.SelectedItem.Text;

        int Pay_DeptNo = Convert.ToInt32(ddlPayrollDept.SelectedValue);
        string Pay_DeptName = ddlPayrollDept.SelectedItem.Text;

        // string Com_Code = Session["comp_code"].ToString();
        string Com_Code = string.Empty;
        DataSet ds = objCommon.FillDropDown("ACC_DEPARTMENT_MAPPING", "*", "", "ACAD_DEPTNO=" + Convert.ToInt32(Acd_DeptNo) + " and PAYROLL_DEPTNO=" + Convert.ToInt32(Pay_DeptNo), "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            objCommon.DisplayMessage(UPDLedger, "Department Already  exist.", this);
            return;
        }
        else
        {

            if (ViewState["Action"].ToString() == "Add")
            {
                Dept_no = 0;
                ret = objFTS.AddUpdateMapDept(Acd_DeptNo, Acd_DeptName, Pay_DeptNo, Pay_DeptName, Dept_no, Com_Code);
                if (ret != null)
                {
                    objCommon.DisplayMessage(UPDLedger, "Department Mapped Successfully.", this);
                    clear();
                    ShowData();
                }
            }
            else if (ViewState["Action"].ToString() == "edit")
            {
                Dept_no = Convert.ToInt32(ViewState["Dept_No"]);
                ret = objFTS.AddUpdateMapDept(Acd_DeptNo, Acd_DeptName, Pay_DeptNo, Pay_DeptName, Dept_no, Com_Code);
                if (ret != null)
                {
                    objCommon.DisplayMessage(UPDLedger, "Department Mapped Successfully.", this);
                    clear();
                    ShowData();
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAcadDept.SelectedIndex = 0;
        ddlPayrollDept.SelectedIndex = 0;
        btnSubmit.Text = "Submit";
        ViewState["Action"] = "Add";

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            ImageButton btnEdit = sender as ImageButton;
            int Dept_no = int.Parse(btnEdit.CommandArgument);
            ViewState["Dept_No"] = Dept_no;
            ViewState["Action"] = "edit";
            DataSet ds = objCommon.FillDropDown("ACC_DEPARTMENT_MAPPING", "*", "", "DEPTNO=" + Convert.ToInt32(Dept_no), "");
            if (ds != null)
            {
                //txtAcNo.Text = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                ddlAcadDept.SelectedValue = ds.Tables[0].Rows[0]["ACAD_DEPTNO"].ToString();
                ddlPayrollDept.SelectedValue = ds.Tables[0].Rows[0]["PAYROLL_DEPTNO"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }   
}
