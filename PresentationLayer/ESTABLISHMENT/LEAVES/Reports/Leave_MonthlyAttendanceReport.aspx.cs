//======================================================================================
// PROJECT NAME  : UAIMS                                                             
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : Leave_MonthlyAttendanceReport.aspx.cs                                             
// CREATION DATE :                                                    
// CREATED BY    :     
// Modified By   : Mrunal Bansod  
// MODIFIED DATE : 14/07/2012
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

public partial class ESTABLISHMENT_LEAVES_Reports_Leave_MonthlyAttendanceReport : System.Web.UI.Page
{

    string date = "";
    int counter = 0;
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();

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
                CheckPageAuthorization();
                FillDropDown();
                chkDept.Checked = true;
                trddldept.Visible = true;
                //objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
       
                
                Page.Title = Session["coll_name"].ToString();

                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                //pnlAdd.Visible = false;
                int ua_type = Convert.ToInt32(Session["usertype"]);
                int user_no = Convert.ToInt32(Session["userno"]);
                if (ua_type == 3 || ua_type == 4 || ua_type == 5) 
                {
                    tremp.Visible = true;
                    chkDept.Visible = true;
                    chkDept.Enabled = false;
                    chkDept.Checked = true;
                    //ddldept.Visible = true;
                    trddldept.Visible = true;
                    ddldept.Enabled = false;
                    ddlStaffType.Enabled = false;
                    ddlEmp.Enabled = false;
                    rblAllParticular.Visible = false;
                    rblAllParticular.SelectedValue = "1";
                    //ddlEmp.Visible = true;
                    //this.FillEmployee();
                    this.FillParticularEmployee();
                   // FillCollege();
                   txtMonthYear.Text = System.DateTime.Now.ToString("MM/yyyy");
                }
                else
                {
                    tremp.Visible = false;
                    //this.FillEmployee();
                    //first day and last day of month
                    txtMonthYear.Text = System.DateTime.Now.ToString("MM/yyyy");
                 
                }
                //DateTime today = DateTime.Today;
                //int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                //DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                //DateTime endOfMonth = new DateTime(today.Year, today.Month, daysInMonth);
                //txtMonthYear.Text = startOfMonth.ToString();
                //txtTodt.Text = endOfMonth.ToString();
                //first day and last day of month end
                FillCollege();

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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlcollege.Items.FindByValue("0");
            ddlcollege.Items.Remove(removeItem);
        }
    }
    // objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
    public void FillDropDown()
    {
        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1 , "STAFFTYPE");
    }

    protected void rblAllParticular_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAllParticular.SelectedValue == "1")
        {
            tremp.Visible = true;
            this.FillEmployee();
        }
        else
            tremp.Visible = false;
    }

    public void FillEmployee()
    {
        //if (chkDept.Checked == true)
        //{
        //    objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND SUBDEPTNO=" + ddldept.SelectedValue, "FNAME");

        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0", "FNAME");
        //}
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND (SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " OR " + Convert.ToInt32(ddldept.SelectedValue) + "=0) AND (STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " OR " + Convert.ToInt32(ddlStaffType.SelectedValue) + "=0)", "FNAME");
    }

    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
           // string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtMonthYear.Text)));
           
           //Fdate = Fdate.Substring(0, 10);
           // string Tdate = (String.Format("{0:u}",Convert.ToDateTime(txtTodt.Text)));
           // Tdate = Tdate.Substring(0, 10);


            string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
            DateTime dt = Convert.ToDateTime(ToDate);
            int month = dt.Month;
            int year = dt.Year;

            int empno = 0;
          
            int deptno = 0;
            if (rblAllParticular.SelectedValue == "0")
            {
                empno = 0;
            }
            else
            {
                empno = Convert.ToInt32(ddlEmp.SelectedValue);
            }

            if(chkDept.Checked)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }

           
            string Script = string.Empty;
            //int Exists = objApp.GetAttendMonyear(Convert.ToDateTime(txtTodt.Text));
            //if (Exists == 0)
            //{
            //    objCommon.DisplayUserMessage(updAdd, "Sorry! Please Process Attendance", this);
            //    return;
            //}
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPTNO=" + deptno + ",@P_EMPNO=" + empno + ",@P_FROMDATE=" + Fdate.ToString().Trim()+",@P_TODATE="+Tdate.ToString().Trim()+"";

            url += "&param=@P_EMPNO=" + empno + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " ,@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + ",@Month=" + month + ",@Year=" + year + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlcollege.SelectedValue) + " ";

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
        string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
        DateTime dt = Convert.ToDateTime(ToDate);
       
        objLM.FROMDT = dt;
        objLM.TODT = dt;
        objLM.COLLEGE_NO = Convert.ToInt32(ddlcollege.SelectedValue);
        objLM.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
        //objLM.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
        int ret = Convert.ToInt32(objApp.CheckAttendanceProcess(objLM));
        if (ret == 1)
        {
            // ShowLeaveDeductionCombinrReport("Attendance", "Monthly_Leave.rpt");
            ShowReport("Attendance", "Monthly_Leave_Report.rpt");//  Monthly_Leave_Format1
        }
        else if (ret == 0)
        {
            objCommon.DisplayMessage("Sorry! Attendance Not Processed", this);

        }
       
    }

    protected void chkDept_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkDept.Checked)
        //{
        //    trddldept.Visible = true;
        //    objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        //}
        //else
        //    trddldept.Visible = false;
        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");

    }
    protected void txtMonthYear_TextChanged(object sender, EventArgs e)
    {
        //butAttendanceProcess.Text = "Leave Process For" + " " + Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    }
    //protected void txtTodt_TextChanged(object sender, EventArgs e)
    //{
    //    btnReport.Text = "Leave Process For" + " " + Convert.ToDateTime(txtTodt.Text).ToString("MMM").ToUpper() + "." + " " + "Month ";
    //}


    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/ESTABLISHMENT/LEAVES/Reports/Leave_MonthlyAttendanceReport.aspx");
        ddlcollege.SelectedIndex = ddlStaffType.SelectedIndex = ddldept.SelectedIndex = 0;
        rblAllParticular.SelectedValue = "0";
        tremp.Visible = false;
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    //ddlStaffType_SelectedIndexChanged
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    //To fill particular employee
    public void FillParticularEmployee()
    {
        int userno = Convert.ToInt32(Session["userno"]);
        int empidno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "isnull(UA_IDNO,0)as UA_IDNO", "UA_NO=" + userno));
        int deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO = " + empidno));
        int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO = " + empidno));

        objCommon.FillDropDownList(ddldept,"PAYROLL_SUBDEPT","SUBDEPTNO","SUBDEPT","SUBDEPTNO="+deptno,"SUBDEPTNO");
       
        objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO="+stno+"", "STAFFTYPE");

        ddldept.SelectedValue = deptno.ToString();
        ddlStaffType.SelectedValue = stno.ToString();

        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO=" + empidno, "FNAME");
        ddlEmp.SelectedValue = empidno.ToString();
    }
    protected void btnReport_Format2_Click(object sender, EventArgs e)
    {
        string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
        DateTime dt = Convert.ToDateTime(ToDate);
       
        objLM.FROMDT = dt;
        objLM.TODT = dt;
        objLM.COLLEGE_NO = Convert.ToInt32(ddlcollege.SelectedValue);
        objLM.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
        objLM.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
        int ret = Convert.ToInt32(objApp.CheckAttendanceProcess(objLM));
        if (ret == 1)
        {

            ShowLeaveDeductionCombinrReport("Attendance", "Monthly_Leave_Formate2.rpt");
        }
        else if (ret == 0)
        {
            objCommon.DisplayMessage("Sorry! Attendance Not Processed", this);

        }
    }
    private void ShowLeaveDeductionReport(string reportTitle, string rptFileName)
    {
        try
        {
            string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
            DateTime dt = Convert.ToDateTime(ToDate);
            int month = dt.Month;
            int year = dt.Year;



            int empno = 0;
            int deptno = 0;
           // int year = DateTime.Now.Year;
            //int year = 2014;
            int lno = 0;
               empno = Convert.ToInt32(ddlEmp.SelectedValue);
               deptno = Convert.ToInt32(ddldept.SelectedValue);
           

            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + empno + ",@Month=" + month + ",@Year=" + year + ",@P_YEAR=" + year + ",@P_LNO=" + lno + " ";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

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
    protected void btnCombinreRpt_Click(object sender, EventArgs e)
    {
       // ShowLeaveDeductionCombinrReport("Attendance", "Monthly_Leave_Combine_NEW.rpt");
       
         string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
        DateTime dt = Convert.ToDateTime(ToDate);
       
        objLM.FROMDT = dt;
        objLM.TODT = dt;
        objLM.COLLEGE_NO = Convert.ToInt32(ddlcollege.SelectedValue);
        objLM.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
        objLM.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
        int ret = Convert.ToInt32(objApp.CheckAttendanceProcess(objLM));
        if (ret == 1)
        {
            ShowLeaveDeductionCombinrReport("Attendance", "Monthly_Leave_Report_Dynamic.rpt");
        }
        else if (ret == 0)
        {
            objCommon.DisplayMessage("Sorry! Attendance Not Processed", this);

        }
        //ShowLeaveDeductionCombinrReport("Attendance", "demo.rpt");
    }

    private void ShowLeaveDeductionCombinrReport(string reportTitle, string rptFileName)
    {
        try
        {
            string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
            DateTime dt = Convert.ToDateTime(ToDate);
            int month = dt.Month;
            int year = dt.Year;

            int empno = 0;
            int deptno = 0;
          //  int year = DateTime.Now.Year;
            int lno = 0;
            empno = Convert.ToInt32(ddlEmp.SelectedValue);
           
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EMPNO=" + empno + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " ,@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + ",@Month=" + month + ",@Year=" + year + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlcollege.SelectedValue);//@P_COLLEGE_NO
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
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LeavesController objLeave = new LeavesController();
    //        GridView GVDayWiseAtt = new GridView();
    //        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtMonthYear.Text)));
    //        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
    //        Fdate = Fdate.Substring(0, 10);
    //        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
    //        Tdate = Tdate.Substring(0, 10);

    //        int empno = 0;
    //        int deptno = 0;
    //        int year = DateTime.Now.Year;
    //        int lno = 0;
    //        empno = Convert.ToInt32(ddlEmp.SelectedValue);

    //        DataSet ds = null;


    //       // ds = objApp.GetMonthlyAttendanceFormat2Excel(Convert.ToDateTime(Fdate), Convert.ToInt32(Tdate), Convert.ToInt32(empno), Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue));

    //        ds = objApp.GetMonthlyAttendanceFormat2Excel(Convert.ToDateTime(Fdate), Convert.ToDateTime(Tdate), empno, Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue));
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            GVDayWiseAtt.DataSource = ds;
    //            GVDayWiseAtt.DataBind();
    //            string attachment = "attachment; filename=MonthlyAttendance.xls";
    //            Response.ClearContent();
    //            Response.AddHeader("content-disposition", attachment);
    //            Response.ContentType = "application/vnd.MS-excel";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            GVDayWiseAtt.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");
    //    }

        
    //}
}
