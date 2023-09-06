//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITCalculation.aspx                                                  
// CREATION DATE : 13-April-2011                                                      
// CREATED BY    : Ankit Agrawal                                                     
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.Caching;

public partial class Pay_ITReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
 
    string UsrStatus = string.Empty;
    string sParams = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //Checking logon Status and redirection to Login Page(Default.aspx) if user is not logged in
    protected void Page_Load(object sender, EventArgs e)
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

                //function to fill dropdownlists
                FillDropdown();
                //FillEmp();
                ddlStaff.SelectedIndex = 0;
                ddlCalculationBy.SelectedIndex = 0;

                DateTime fdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITFDATE", ""));
                txtFromDate.Text = fdate.ToString();
                DateTime tdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITTDATE", ""));
                txtToDate.Text = tdate.ToString();
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
                Response.Redirect("~/notauthorized.aspx?Pay_ITReports.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITReports.aspx");
        }
    }
    protected void rblCalculationBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rblCalculationBy.SelectedIndex == 0)
        //{
        //    lblCalculationBy.Text = "Select College";
        //    chkStaffWise.Visible = true;
        //    chkStaffWise.Checked = false;
        //    rfvCalculationBy.ErrorMessage = "Please Select College Type";
        //}
        //else if (rblCalculationBy.SelectedIndex == 1)
        //{
        //    lblCalculationBy.Text = "Select Employee";
        //    chkStaffWise.Visible = false;
        //    rowStaffWise.Visible = false;
        //    ddlStaff.SelectedIndex = 0;
        //    rfvCalculationBy.ErrorMessage="Please Select Employee Name";
        //}
        //FillDropdown();
    }

   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pay_ITReports.aspx");
    }
    protected void chkStaffWise_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStaffWise.Checked == true)
        {
            rowStaffWise.Visible = true;
        }
        else
        {
            rowStaffWise.Visible = false;
        }
    }
    protected void btnShowTax_Click(object sender, EventArgs e)
    {
       
            ShowReport("ShowTax", "Pay_IT_ShowTax.rpt");
        
    }
    protected void btnTaxColumns_Click(object sender, EventArgs e)
    {
        
            ShowReport("TaxColumn", "Pay_IT_TaxColumn.rpt");
        
    }
    protected void btnTaxDetails_Click(object sender, EventArgs e)
    {
        
            ShowReport("TaxDetail", "Pay_IT_Tax_Details.rpt");
        
    }

    protected void  btn24Q_Click(object sender, EventArgs e)
    {
         ShowReport("FORMNO24Q", "Pay_IT_24Q_Report.rpt");
    }
    protected void btnRemainingIT_Click(object sender, EventArgs e)
    {
        ShowRemainingMonthReport("ITRamainingMonth", "Pay_ITRemainingMonAmount.rpt");
    }

    protected void btnForm16_Click(object sender, EventArgs e)
    {
        
          ShowReport("FORMNO16", "Pay_IT_Form_No16_New.rpt");
        //ShowReport("FORMNO16", "Pay_IT_Form_No16_New_partB.rpt");
           // ShowReport("FORMNO16", "Pay_IT_Form_No16.rpt");
        
    }

    protected void btn26Q_Click(object sender, EventArgs e)
    {
        Show26QReport("IT26QReport", "PayIT26QReport.rpt");
    }

    protected void GetParameters()
    {
        int iEmpno;
        int iStaffNo;
        int iCollegeNo;
        int iSupZero;
        int iSupNeg;
        int iSupAmt;
        //string sHeading;
        string sColHead;

        DateTime frmdate;
        DateTime todate;
        int month;

        frmdate = Convert.ToDateTime(txtFromDate.Text);
        todate = Convert.ToDateTime(txtToDate.Text);

        int maxsalno = Convert.ToInt32(objCommon.LookUp("PAYROLL_SALFILE", "MAX(SALNO)", "SALNO>0"));

        DateTime salmondt = Convert.ToDateTime(objCommon.LookUp("PAYROLL_SALFILE", "DT", "SALNO=" + maxsalno + ""));

        month = (todate.Year - salmondt.Year) * 12 + todate.Month - salmondt.Month;
        month = month - 1;


        iEmpno = Convert.ToInt32(ddlEmp.SelectedValue);

        iStaffNo = Convert.ToInt32(ddlStaff.SelectedValue);

        iCollegeNo = Convert.ToInt32(Session["colcode"].ToString());
        // iCollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);

        iSupZero = chkDontShowZero.Checked == true ? 1 : 0;

        iSupNeg = chkDontShowNegative.Checked == true ? 1 : 0;

        iSupAmt = chkDontShowAmount.Checked == true ? 1 : 0;

        //sHeading=ddlCalculationBy.SelectedItem.Text.ToString().Trim();
        //sHeading = iStaffNo == 0 ? "" : " - " + ddlStaff.SelectedItem.Text.ToString().Trim();

        sColHead = rblOptions.SelectedIndex == 0 ? "Amount" : rblOptions.SelectedItem.Text.ToString().ToUpper().Trim();

        sParams = "@P_FROMDATE=" + txtFromDate.Text.ToString().Trim();
        sParams += ",@P_TODATE=" + txtToDate.Text.ToString().Trim();
        //sParams += ",@P_EMPNO=" + iEmpno;
        sParams += ",@P_EMPNO=" + ddlEmp.SelectedValue;
        //sParams += ",@P_EMPID=" + ddlEmp.SelectedValue;
        sParams += ",@P_STAFFNO=" + iStaffNo;
        sParams += ",@P_COLLEGENO=" + iCollegeNo;
        sParams += ",@P_OPTIONS=" + rblOptions.SelectedValue.ToString().Trim();
        sParams += ",@P_OREDERBY=" + rblOrderBy.SelectedValue.ToString().Trim();
        sParams += ",@P_REMAINING_MONTH=" + month;
        sParams += ",@P_SUPZEROAMT=" + iSupZero;
        sParams += ",@P_SUPNEG=" + iSupNeg;
        //sParams += ",@P_HEAD=" + sHeading.ToUpper();
        sParams += ",@P_COLNAME=" + sColHead;
        //sParams += ",@P_WorkingDate="+Session["WorkingDate"].ToString().Trim();
        sParams += ",@P_UserId=" + Session["userfullname"].ToString();
        //sParams += ",@P_Ip="+Session["IPADDR"].ToString();
        sParams += ",@P_SUPAMT=" + iSupAmt;
        sParams += ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    }

    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            if (reportTitle != "FORMNO16")
            {
                GetParametersOtherIT();
            }
            else
            {
                GetParameters();
            }
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            if (reportTitle == "FORMNO16")
            {
                url += "&param=" + sParams + ",@P_EMPID=" + ddlEmp.SelectedValue;
                    //+ ",@P_COLLEGE_CODE=" + Session["colcode"].ToString(); ; 
            }

            if (reportTitle != "FORMNO16")
            {

            }

            url += "&param="+sParams;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    //Function to fill dropdownlist for college type,stafftype
    protected void FillDropdown()
    {
        //if (rblCalculationBy.SelectedIndex == 0)
        //    //objCommon.FillDropDownList(ddlCalculationBy, "PAYROLL_COLLEGE", "COLLEGENO", "COLLEGENAME", "COLLEGENO>0", "COLLEGENO");
        //else 
        //if (rblCalculationBy.SelectedIndex == 1)
        //    objCommon.FillDropDownList(ddlCalculationBy, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),PFILENO) +']')", "IDNO>0", "IDNO");
       // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        //objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
    }

    protected void FillEmp()
    {
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "IDNO");
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillEmp();
        if (ddlStaff.SelectedValue == "0")
        {
           // objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "" + rblOrderBy.SelectedValue + "");
        }
        else
        {
            //objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0 AND COLLEGE_NO="+ddlCollege.SelectedValue+" AND STAFFNO="+ddlStaff.SelectedValue, "" + rblOrderBy.SelectedValue + "");
        }

    }
    protected void btnForm16partB_Click(object sender, EventArgs e)
    {
        ShowReport("FORMNO16", "Pay_IT_Form_No16_New_partB.rpt");
    }
    protected void rblOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", ""+rblOrderBy.SelectedValue+"");
    }
   //function to generate ramaining month amount
    private void ShowRemainingMonthReport(string reportTitle, string rptFileName)
    {
        try
        {
            GetParametersRemainMon();
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            if (reportTitle == "FORMNO16")
            {
                url += "&param=" + sParams + ",@P_EMPID=" + ddlEmp.SelectedValue;
                //+ ",@P_COLLEGE_CODE=" + Session["colcode"].ToString(); ; 
            }
            url += "&param=" + sParams;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void GetParametersRemainMon()
    {
        int iEmpno;
        int iStaffNo;
        int iCollegeNo;
        int iSupZero;
        int iSupNeg;
        int iSupAmt;
        //string sHeading;
        string sColHead;

        DateTime frmdate;
        DateTime todate;
        int month;

        frmdate = Convert.ToDateTime(txtFromDate.Text);
        todate = Convert.ToDateTime(txtToDate.Text);

        int maxsalno = Convert.ToInt32(objCommon.LookUp("PAYROLL_SALFILE", "MAX(SALNO)", "SALNO>0"));

        DateTime salmondt = Convert.ToDateTime(objCommon.LookUp("PAYROLL_SALFILE", "DT", "SALNO=" + maxsalno + ""));

        month = (todate.Year - salmondt.Year) * 12 + todate.Month - salmondt.Month;
        month = month - 1;

        iEmpno = Convert.ToInt32(ddlEmp.SelectedValue);

        iStaffNo = Convert.ToInt32(ddlStaff.SelectedValue);

        iCollegeNo = Convert.ToInt32(Session["colcode"].ToString());

        iSupZero = chkDontShowZero.Checked == true ? 1 : 0;

        iSupNeg = chkDontShowNegative.Checked == true ? 1 : 0;

        iSupAmt = chkDontShowAmount.Checked == true ? 1 : 0;

        //sHeading=ddlCalculationBy.SelectedItem.Text.ToString().Trim();
        //sHeading = iStaffNo == 0 ? "" : " - " + ddlStaff.SelectedItem.Text.ToString().Trim();

        sColHead = rblOptions.SelectedIndex == 0 ? "Amount" : rblOptions.SelectedItem.Text.ToString().ToUpper().Trim();

        sParams = "@P_FROMDATE=" + txtFromDate.Text.ToString().Trim();
        sParams += ",@P_TODATE=" + txtToDate.Text.ToString().Trim();
        sParams += ",@P_REMAINING_MONTH=" + month;
        sParams += ",@P_STAFFNO=" + iStaffNo;
        sParams += ",@P_COLLEGENO=" + iCollegeNo;
        //sParams += ",@P_OPTIONS=" + rblOptions.SelectedValue.ToString().Trim();
        sParams += ",@P_OREDERBY=" + rblOrderBy.SelectedValue.ToString().Trim();
        //sParams += ",@P_SUPZEROAMT=" + iSupZero;
        //sParams += ",@P_SUPNEG=" + iSupNeg;
        //sParams += ",@P_COLNAME=" + sColHead;
        //sParams += ",@P_UserId=" + Session["userfullname"].ToString();
        //sParams += ",@P_SUPAMT=" + iSupAmt;
        sParams += ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    }

    protected void GetParametersOtherIT()
    {
        int iEmpno;
        int iStaffNo;
        int iCollegeNo;
        int iSupZero;
        int iSupNeg;
        int iSupAmt;
        //string sHeading;
        string sColHead;

        DateTime frmdate;
        DateTime todate;
        int month;

        frmdate = Convert.ToDateTime(txtFromDate.Text);
        todate = Convert.ToDateTime(txtToDate.Text);

        int maxsalno = Convert.ToInt32(objCommon.LookUp("PAYROLL_SALFILE", "MAX(SALNO)", "SALNO>0"));

        DateTime salmondt = Convert.ToDateTime(objCommon.LookUp("PAYROLL_SALFILE", "DT", "SALNO=" + maxsalno + ""));

        month = (todate.Year - salmondt.Year) * 12 + todate.Month - salmondt.Month;
        month = month - 1;


        iEmpno = Convert.ToInt32(ddlEmp.SelectedValue);

        iStaffNo = Convert.ToInt32(ddlStaff.SelectedValue);

        iCollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);

        iSupZero = chkDontShowZero.Checked == true ? 1 : 0;

        iSupNeg = chkDontShowNegative.Checked == true ? 1 : 0;

        iSupAmt = chkDontShowAmount.Checked == true ? 1 : 0;

        //sHeading=ddlCalculationBy.SelectedItem.Text.ToString().Trim();
        //sHeading = iStaffNo == 0 ? "" : " - " + ddlStaff.SelectedItem.Text.ToString().Trim();

        sColHead = rblOptions.SelectedIndex == 0 ? "Amount" : rblOptions.SelectedItem.Text.ToString().ToUpper().Trim();

        sParams = "@P_FROMDATE=" + txtFromDate.Text.ToString().Trim();
        sParams += ",@P_TODATE=" + txtToDate.Text.ToString().Trim();
        //sParams += ",@P_EMPNO=" + iEmpno;
        sParams += ",@P_EMPNO=" + ddlEmp.SelectedValue;
        //sParams += ",@P_EMPID=" + ddlEmp.SelectedValue;
        sParams += ",@P_STAFFNO=" + iStaffNo;
        sParams += ",@P_COLLEGENO=" + iCollegeNo;
        sParams += ",@P_OPTIONS=" + rblOptions.SelectedValue.ToString().Trim();
        sParams += ",@P_OREDERBY=" + rblOrderBy.SelectedValue.ToString().Trim();
        //sParams += ",@P_REMAINING_MONTH=" + month;
        sParams += ",@P_SUPZEROAMT=" + iSupZero;
        sParams += ",@P_SUPNEG=" + iSupNeg;
        //sParams += ",@P_HEAD=" + sHeading.ToUpper();
        sParams += ",@P_COLNAME=" + sColHead;
        //sParams += ",@P_WorkingDate="+Session["WorkingDate"].ToString().Trim();
        sParams += ",@P_UserId=" + Session["userfullname"].ToString();
        //sParams += ",@P_Ip="+Session["IPADDR"].ToString();
        sParams += ",@P_SUPAMT=" + iSupAmt;
        sParams += ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    }

    protected void btnForm16RemAmt_Click(object sender, EventArgs e)
    {
        ShowReport("FORMNO16", "Pay_IT_Form_No16_New_partB_ITRemainingAMt.rpt");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
    }

    //26Q report
    private void Show26QReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text) + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text) + ",@P_EMPID=" + ddlEmp.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
