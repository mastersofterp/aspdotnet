//======================================================================================
// PROJECT NAME  : UAIMS                                                             
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : Estb_Biometric_Consolidate_report.aspx.cs                                             
// CREATION DATE : 26/03/2018                                         
// CREATED BY    : Sagar Hiratkar 

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
using System.Globalization;

public partial class ESTABLISHMENT_LEAVES_Reports_Estb_Biometric_Consolidate_report : System.Web.UI.Page
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
                    
                    ddldept.Enabled = false;
                    ddlStaffType.Enabled = false;
                    ddlEmp.Enabled = false;
                    rblAllParticular.Visible = false;
                    rblAllParticular.SelectedValue = "1";
                    //ddlEmp.Visible = true;
                    //this.FillEmployee();
                    this.FillParticularEmployee();
                    // FillCollege();
                   
                }
                else
                {
                    tremp.Visible = false;
                   
                }
                
                FillCollege();

            }
        }
        divMsg.InnerHtml = string.Empty;
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
    public void FillDropDown()
    {
        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
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
       
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND (SUBDEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + " OR " + Convert.ToInt32(ddldept.SelectedValue) + "=0) AND (STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + " OR " + Convert.ToInt32(ddlStaffType.SelectedValue) + "=0)", "FNAME");
    }
    public void FillParticularEmployee()
    {
        int userno = Convert.ToInt32(Session["userno"]);
        int empidno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "isnull(UA_IDNO,0)as UA_IDNO", "UA_NO=" + userno));
        int deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO = " + empidno));
        int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO = " + empidno));

        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO=" + deptno, "SUBDEPTNO");

        objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO=" + stno + "", "STAFFTYPE");

        ddldept.SelectedValue = deptno.ToString();
        ddlStaffType.SelectedValue = stno.ToString();

        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO=" + empidno, "FNAME");
        ddlEmp.SelectedValue = empidno.ToString();
    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
            // ShowLeaveDeductionCombinrReport("Attendance", "Monthly_Leave.rpt");
        ShowReport("Attendance", "ESTB_Biometric_Consolidate_Report.rpt");//  Monthly_Leave_Format1
      
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
           
            string Script = string.Empty;
            int idno = 0;
            if (rblAllParticular.SelectedValue == "1")
            {
                idno = Convert.ToInt32(ddlEmp.SelectedValue);
            }
           
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPTNO=" + deptno + ",@P_EMPNO=" + empno + ",@P_FROMDATE=" + Fdate.ToString().Trim()+",@P_TODATE="+Tdate.ToString().Trim()+"";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + " ,@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlcollege.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(idno) + " ";
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlcollege.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + " ,@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlcollege.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(idno) + " ";

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
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlcollege.SelectedValue = "0";
        ddlStaffType.SelectedValue = "0";
        ddldept.SelectedValue = "0";
        rblAllParticular.SelectedValue = "0";
        ddlEmp.SelectedValue = "0";
        txtFdate.Text = string.Empty;
        txtDate.Text = string.Empty;
        tremp.Visible = false;
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtDate.Text != string.Empty && txtDate.Text != "__/__/____" && txtFdate.Text != string.Empty && txtFdate.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFdate.Text);
                DtTo = Convert.ToDateTime(txtDate.Text);
                if (DtTo < DtFrom)
                {
                    MessageBox("To Date Should be Greater than  or equal to From Date");
                    txtDate.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtDate.Text = string.Empty;
            txtFdate.Text = string.Empty;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnReport1_Click(object sender, EventArgs e)
    {
        try
        {
            ShowConsalidatedReport("IN OUT Report", "ESTB_Consolidate_biometriclog.rpt");
        }
        catch (Exception ex)
        {
        }
    }

    private void ShowConsalidatedReport(string reportTitle, string rptFileName)
    {
        try
        {
            int staffno = 0; int dept = 0;
            string Script = string.Empty;
            int idno = 0;
            if (rblAllParticular.SelectedValue == "1")
            {
                idno = Convert.ToInt32(ddlEmp.SelectedValue);
            }

            if (ddlStaffType.SelectedIndex > 0)
            {
                staffno = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                staffno = 0;
            }
            if (ddldept.SelectedIndex > 0)
            {
                dept = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                dept = 0;
            }


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@FROM_DATE=" + Convert.ToDateTime(txtFdate.Text.Trim()).ToString("yyyy-MM-dd") + ",@TO_DATE=" + Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_STNO=" + staffno + ",@P_DEPT=" + dept + ",@P_COLLEGENO=" + Convert.ToInt32(ddlcollege.SelectedValue) + ",@P_IDNO=" + idno;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlcollege.SelectedValue) + ",@FROM_DATE=" + Convert.ToDateTime(txtFdate.Text.Trim()).ToString("yyyy-MM-dd") + ",@TO_DATE=" + Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_STNO=" + staffno + ",@P_DEPT=" + dept + ",@P_COLLEGENO=" + Convert.ToInt32(ddlcollege.SelectedValue) + ",@P_IDNO=" + idno;
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
}