//======================================================================================
// PROJECT NAME  : UAIMS                                                             
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : Leave_OD_Report.aspx.cs                                             
// CREATION DATE :                                                    
// CREATED BY    :     
// Modified By   : Mrunal Bansod  
// MODIFIED DATE : 17/07/2012
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
using System.Globalization;
public partial class ESTABLISHMENT_LEAVES_Reports_LeaveAndODSummay : System.Web.UI.Page
{

    string date = "";
    int counter = 0;
    
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();

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

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;
                tremp.Visible = false;
                //this.FillEmployee();
                FillCollege();
                FillStafftype();
                FillEmployee();
                CheckPageAuthorization();
            }
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LeaveAndODReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LeaveAndODReport.aspx");
        }
    }

    protected void rblAllParticular_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAllParticular.SelectedValue == "1")
            tremp.Visible = true;
        else
            tremp.Visible = false;
    }

    public void FillEmployee()
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0 && ddlstafftype.SelectedIndex ==0 && ddldept.SelectedIndex ==0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0  AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "FNAME");
            }
            else if(ddlCollege.SelectedIndex > 0 && ddlstafftype.SelectedIndex >0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0  AND STNO=" + Convert.ToInt32(ddlstafftype.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "FNAME");
            }
            else if(ddlCollege.SelectedIndex > 0 && ddlstafftype.SelectedIndex >0 && ddldept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and SUBDEPTNO=" + ddldept.SelectedValue + " AND STNO=" + Convert.ToInt32(ddlstafftype.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "FNAME");
            }
            else if(ddlCollege.SelectedIndex== 0 && ddlstafftype.SelectedIndex >0 && ddldept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and SUBDEPTNO=" + ddldept.SelectedValue + " AND STNO=" + Convert.ToInt32(ddlstafftype.SelectedValue)+ "", "FNAME");
            }
            else if(ddlCollege.SelectedIndex== 0 && ddlstafftype.SelectedIndex ==0 && ddldept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and SUBDEPTNO=" + ddldept.SelectedValue+ "", "FNAME");
            }
            else if (ddlCollege.SelectedIndex == 0 && ddlstafftype.SelectedIndex > 0 && ddldept.SelectedIndex == 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and STNO=" + ddlstafftype.SelectedValue + "", "FNAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0" + "", "FNAME");
            }

            //if(
            //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and SUBDEPTNO=" + ddldept.SelectedValue + " AND STNO=" + Convert.ToInt32(ddlstafftype.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "FNAME");
        }
        catch(Exception ex)
        {
            
        }
    }

    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int empno = 0; int stno = 0;

            int deptno = 0; int collegeno = 0;
            if (rblAllParticular.SelectedValue == "0")
            {
                empno = 0;
            }
            else
            {
                empno = Convert.ToInt32(ddlEmp.SelectedValue);
            }
            
            //if (chkDept.Checked)
            //{
               
            //    deptno = Convert.ToInt32(ddldept.SelectedValue);
            //}
            //else
            //{
               
            //    deptno = 0;
            //}

            if (ddlstafftype.SelectedIndex > 0)
            {
                stno = Convert.ToInt32(ddlstafftype.SelectedValue);
            }
            else
            {
                stno = 0;
            }

            deptno = Convert.ToInt32(ddldept.SelectedValue);
            if (ddlCollege.SelectedIndex > 0)
            {
                collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                collegeno = 0;
            }
            string Script = string.Empty;
            string frmdt = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
            string todt = Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@username=" + Session["userfullname"].ToString() + ",@P_EMPNO=" + empno + ",@P_FROMDATE=" + frmdt + ",@P_TODATE=" + todt + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()
                     + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + stno + ",@P_COLLEGE_NO=" + collegeno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rblAllParticular.SelectedValue == "1")
        {
            if (ddlEmp.SelectedIndex > 0)
            {

            }
            else
            {
                MessageBox("Please Select At Least One Employee");
                return;
            }
        }

        ShowReport("ODLeaveSummary", "ESTB_ODLeaveSummary.rpt");
    }

    //protected void chkDept_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkDept.Checked)
    //    {
    //        trddldept.Visible = true;
    //        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
    //    }
    //    else
    //        trddldept.Visible = false;
    //}

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        ShowReport("ODLeaveDetail", "ESTB_ODLeaveDetail.rpt");
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        ddldept.SelectedIndex = 0;
        ddlstafftype.SelectedIndex = 0;
        rblAllParticular.SelectedValue = "0";
        this.FillEmployee();
        //ddlEmp.Items.Clear();
        tremp.Visible = false;
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

    }

    private void FillStafftype()
    {
        objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1 , "STAFFTYPE");
    }

    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
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
        txtFromdt.Text = txtTodt.Text = string.Empty;
        ddldept.SelectedIndex = ddlEmp.SelectedIndex =ddlCollege.SelectedIndex= 0;
        ddlstafftype.SelectedIndex = 0;
        rblAllParticular.SelectedValue = "0";
        tremp.Visible = false;
       // chkDept.Checked = true;
        ddldept.Visible = true;
    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtTodt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtTodt.Text != string.Empty && txtTodt.Text != "__/__/____" && txtFromdt.Text != string.Empty && txtFromdt.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromdt.Text);
                DtTo = Convert.ToDateTime(txtTodt.Text);
                if (DtTo < DtFrom)
                {
                    MessageBox("To Date Should be Greater than  or equal to From Date");
                    txtTodt.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtTodt.Text = string.Empty;
        }
    }
    protected void ddlstafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.FillEmployee();
        }
        catch (Exception ex)
        {

        }
    }
}
