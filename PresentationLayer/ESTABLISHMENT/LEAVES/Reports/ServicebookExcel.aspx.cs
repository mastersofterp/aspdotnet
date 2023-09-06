using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ESTABLISHMENT_LEAVES_Reports_ServicebookExcel : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objShift = new LeavesController();

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
                Page.Title = Session["coll_name"].ToString();


                FillCollege();
                FillStaffandDept();
                FillServicebookCategory();
                CheckPageAuthorization();

            }

        }
        //blank div tag
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LeaveAllotmentReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LeaveAllotmentReport.aspx");
        }
    }

    private void FillCollege()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

            //if (Session["username"].ToString() != "admin")
            //{
            //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
            //    ddlCollege.Items.Remove(removeItem);
            //}
        }
        catch (Exception ex)
        {

        }
    }

    private void FillStaffandDept()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }

            objCommon.FillDropDownList(ddlDept, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
            //objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "", "STAFFTYPE");
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFF");
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void FillServicebookCategory()
    {
        //objCommon.FillDropDownList(ddlservicebook, "[dbo].[Menus]", "MenuId", "Title", "ParentMenuId !=0 and IsActive=1 and UserTypeId=1 and MenuId!=6", "");
        objCommon.FillDropDownList(ddlservicebook, "[dbo].[Menus]", "MenuId", "Title", "ParentMenuId !=0 and IsActive=1 and UserTypeId=1", "");
    }




    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.FillEmployee();
        }
        catch (Exception ex)
        {

        }
    }

    public void FillEmployee()
    {
        //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "isnull(FNAME,'') + ' ' + isnull(MNAME,'') + ' ' + isnull(LNAME,'') + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and SUBDEPTNO=" + ddlDept.SelectedValue, "FNAME");
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "isnull(FNAME,'') + ' ' +isnull(MNAME,'') + ' ' +isnull(LNAME,'') + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND (SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0) AND (STAFFNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " OR " + Convert.ToInt32(ddlStaffType.SelectedValue) + "=0)", "FNAME");
    }

    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlDept.SelectedValue != "0")
            //{
                
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "isnull(FNAME,'') + ' ' +isnull(MNAME,'') + ' ' +isnull(LNAME,'') + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND (SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0) AND (STAFFNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " OR " + Convert.ToInt32(ddlStaffType.SelectedValue) + "=0)", "FNAME");
                
            //}
            //else
            //{
            //    this.FillEmployee();
            //}
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = ddlDept.SelectedIndex = ddlStaffType.SelectedIndex = ddlEmp.SelectedIndex = ddlservicebook.SelectedIndex = 0;
        divcheck.Visible = false;
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView GVDayWiseAtt = new GridView();
            DataSet ds = null;

            int DEPT, STNO,Collegeno,IDNO = 0;
            string ServiceCategory = "";
            Boolean HighestQual = false;

            if (ddlCollege.SelectedIndex > 0)
            {
                Collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                Collegeno = 0;
            }
            if (ddlDept.SelectedIndex > 0)
            {
                DEPT = Convert.ToInt32(ddlDept.SelectedValue);
            }
            else
            {
                DEPT = 0;
            }
            if (ddlStaffType.SelectedIndex > 0)
            {
                STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                STNO = 0;
            }
            if (ddlEmp.SelectedIndex > 0)
            {
                IDNO = Convert.ToInt32(ddlEmp.SelectedValue);
            }
            else
            {
                IDNO = 0;
            }
            if (chkhigest.Checked == true)
            {
                HighestQual = true;
            }
            else
            {
                HighestQual = false;
            }
            

            ServiceCategory = ddlservicebook.SelectedItem.Text;
            // ds = objleave.LeaveEmployeeBalanceForExport(idno, lvno, Year, Fdate, Tdate, stno);   
            ds = objShift.ServicebookDataExcel(Collegeno, DEPT, STNO, IDNO, ServiceCategory, HighestQual);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView gv_ExcelData = new GridView();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_ExcelData.DataSource = ds;
                    gv_ExcelData.DataBind();
                    string attachment = "attachment; filename=EmployeeSericebook.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gv_ExcelData.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            else
            {
                // objCommon.DisplayMessage(this.Page, "No Data Found", this.Page);
                MessageBox("No Data Found");
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void ddlservicebook_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ServiceCategory = ddlservicebook.SelectedItem.Text;
            if (ServiceCategory == "Qualification")
            {
                divcheck.Visible = true;
            }
            else
            {
                divcheck.Visible = false;
            }  
        }
        catch (Exception ex)
        {

        }
    }
}