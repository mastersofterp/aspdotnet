//======================================================================================
// PROJECT NAME  : UAIMS                                                             
// MODULE NAME   : ESTABLISHMENT
// PAGE NAME     : Leave_OD_Report.aspx.cs                                             
// CREATION DATE :                                                    
// CREATED BY    :     
// Modified By   : Mrunal Bansod  
// MODIFIED DATE : 28/02/2012
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

public partial class ESTABLISHMENT_LEAVES_Reports_Leave_OD_Report : System.Web.UI.Page
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;
                tremp.Visible = false;
                //this.FillEmployee();

                int prevmonth = System.DateTime.Today.AddMonths(-1).Month;
                int prevyr = System.DateTime.Today.AddYears(-1).Year;
                int month = System.DateTime.Today.Month;
                int year = System.DateTime.Today.Year;
                string frmdt = null;
                if (month == 1)
                {
                    frmdt = "21" + "/" + "12" + "/" + prevyr.ToString();
                }
                else
                {
                    frmdt = "21" + "/" + prevmonth.ToString() + "/" + year.ToString();
                }


                string todt = "20" + "/" + month.ToString() + "/" + year.ToString();

                txtFromdt.Text = frmdt;
                txtTodt.Text = todt;
            }
        }
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
        if (chkDept.Checked == true)
        {
              objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND SUBDEPTNO=" + ddldept.SelectedValue, "FNAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0", "FNAME");
        }
    }

    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int empno = 0;
            int chkpurpose = 0;
            int purposeno = 0;
            int chkdept1 = 0;
            int deptno = 0;
            string odapl;
            if (rblAllParticular.SelectedValue == "0")
            {
                empno = 0;
            }
            else
            {
                empno = Convert.ToInt32(ddlEmp.SelectedValue);
            }
            if (chkODPurpose.Checked)
            {
                chkpurpose = 1;
                purposeno = Convert.ToInt32(ddlODPurpose.SelectedValue);
            }
            else
            {
                chkpurpose = 0;
                purposeno = 0;
            }

            if (chkDept.Checked)
            {
                chkdept1 = 1;
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                chkdept1 = 0;
                deptno = 0;
            }
            if(rblODAppSlip.SelectedValue =="0")
            {
                  odapl = "ODA";
            }
            else
            {
                 odapl = "ODS";
            }
                
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@username=" + Session["userfullname"].ToString() + ",@P_EMPNO=" + empno + ",@P_FROM_DATE=" + txtFromdt.Text + ",@P_TO_DATE=" + txtTodt.Text + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()
                      + ",@P_CHKPURPOSE=" + chkpurpose + ",@P_PURPOSENO=" + purposeno  + ",@P_CHKDEPT=" + chkdept1 + ",@P_DEPTNO=" + deptno +",@P_ODTYPE="+odapl;

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
        if (rblODAppSlip.SelectedValue == "0")
        {
            ShowReport("ODLeave", "ESTB_OD_WORKING_REPORT.rpt");
        }
        else
        {
            ShowReport("ODSlipLeave", "ESTB_ODSLIP_Report.rpt");
        }
    }

    protected void chkODPurpose_CheckedChanged(object sender, EventArgs e)
    {
        if (chkODPurpose.Checked)
        {
            trddlOD.Visible = true;
            objCommon.FillDropDownList(ddlODPurpose, "PAYROLL_OD_PURPOSE", "PURPOSENO", "PURPOSE", "PURPOSENO>0", "PURPOSE");
        }
        else
            trddlOD.Visible = false;
    }
    protected void chkDept_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDept.Checked)
        {
            trddldept.Visible = true;
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        }
        else
            trddldept.Visible = false;
    }
    protected void btnLeaveRpt_Click(object sender, EventArgs e)
    {
        ShowReport("LeaveReport", "ESTB_Leave_Report.rpt");
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
}
