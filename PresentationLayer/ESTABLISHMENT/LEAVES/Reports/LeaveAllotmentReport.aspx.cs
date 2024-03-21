//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : LeaveAllotmentReport.aspx                                                 
// CREATION DATE : 1-9-2016
// CREATED BY    : Swati Ghate                  
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.IO;


public partial class LeaveAllotmentReport : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
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

                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                FillCollege();
                FillPeriod();
                FillYear();
                FillStaffandDept();
                FillLeaveName();
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
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }
    private void FillYear()
    {
        int Yr = DateTime.Now.Year;

        ddlYear.Items.Add(Convert.ToString(Yr - 1));
        ddlYear.Items.Add(Convert.ToString(Yr));
        ddlYear.Items.Add(Convert.ToString(Yr + 1));
        ddlYear.SelectedValue = (Convert.ToString(Yr));
    }
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
    }
    private void FillLeaveName()
    {
        objCommon.FillDropDownList(ddlLeaveName, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "", "LEAVE_NAME");
    }
    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = ddlPeriod.SelectedIndex= ddlStaffType.SelectedIndex = ddlLeaveName.SelectedIndex=0;        
        ViewState["selectedDates"] = null;
        ddlPeriod.Focus();
        ddlEmp.SelectedIndex = 0;
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
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStaffandDept();
        FillEmployee();
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int Collegeno = 0;
            if (ddlCollege.SelectedIndex > 0)
            {
                Collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                Collegeno = 0;
            }
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));      
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;

            //url += "&param=@P_COLLEGE_NO=" + Collegeno + ",@P_PERIOD=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_LEAVENO=" + Convert.ToInt32(ddlLeaveName.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue);
            url += "&param=@P_COLLEGE_NO=" + Collegeno + ",@P_PERIOD=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_LEAVENO=" + Convert.ToInt32(ddlLeaveName.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "," + "@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue);

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
      
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LeaveAllotmentReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Allotment", "ESTB_LeaveCreditOpeningReport.rpt");
              
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LeaveAllotmentReport.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnexport_Click(object sender, EventArgs e)
    {
        LeavesController objleave = new LeavesController();
        try
        {
            int collegeno, stno, deptno = 0,leaveno=0 ,empno = 0;
            if (ddlCollege.SelectedIndex > 0)
            {
                collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                collegeno = 0;
            }
            if (ddlStaffType.SelectedIndex > 0)
            {
                stno = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                stno = 0;
            }
            if (ddlDept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddlDept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }
            if(ddlLeaveName.SelectedIndex > 0 )
            {
                leaveno=Convert.ToInt32(ddlLeaveName.SelectedValue);
            }
            else
            {
                leaveno=0;
            }
            if (ddlEmp.SelectedIndex > 0)
            {
                empno = Convert.ToInt32(ddlEmp.SelectedValue);
            }
            else
            {
                empno = 0;
            }
            //DataSet ds = objleave.GetleaveApplicationDataForExport(collegeno, stno, deptno, Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text));
            DataSet ds = objleave.GetleaveAllotmentDataForExport(collegeno, 0, 0, leaveno, deptno, stno,empno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView gv_ExcelData = new GridView();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_ExcelData.DataSource = ds;
                    gv_ExcelData.DataBind();
                    string attachment = "attachment; filename=LeaveApplication.xls";
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
                MessageBox("No Records found.");
                return;
            }



        }
        catch (Exception ex)
        {

        }
    }

    public void FillEmployee()
    {
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "isnull(FNAME,'') + ' ' +isnull(MNAME,'') + ' ' +isnull(LNAME,'') + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND (COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ") AND (STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + ") AND (SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "FNAME");
    }

    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
}
