//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_Ledger_Report.ASPX                                                    
// CREATION DATE : 25-Feb-2010                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_REPORTS_PF_Ledger_Report : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,GPF_CONTROLLER,GPF
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PFCONTROLLER objPfcontroller = new PFCONTROLLER();

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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropDown();
                ViewState["getpfno"] = "0";
                tblEmployee.Visible = false;
                this.GetFinYearSdateEdate();

            }
        }

    }

    protected void butReport_Click(object sender, EventArgs e)
    {
        try
        {

            string param = this.GetParams();
            this.ShowReport(param, "PF_Personal_Ledger_Report", "PF_Personal_Ledger_Report_Main.rpt");


         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PF_Intrest_Calculation.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PayRoll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@username=" + Session["userfullname"].ToString() + "," + param;
            //url += "&param=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + "," + param;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> ";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') ", "EM.IDNO");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PF_Intrest_Calculation.FillDropDown-> " + ex.ToString());
        }

    }

    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            ViewState["getpfno"] = objCommon.LookUp("payroll_empmas", "isnull(pfno,0)", "(status is null or status='') and idno=" + Convert.ToInt32(ddlemployee.SelectedValue));
            string shortname = objCommon.LookUp("payroll_pf_mast", "shortname", "pfno=" + Convert.ToInt32(ViewState["getpfno"].ToString()));
            lbleligibleFor.Text = shortname;
        }
    }

    private void GetFinYearSdateEdate()
    {
        string Fsdate = string.Empty;
        string Fedate = string.Empty;
        Fsdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_PAY_REF", "PFFSDATE", string.Empty)).ToString("dd/MM/yyyy");
        Fedate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_PAY_REF", "PFFEDATE", string.Empty)).ToString("dd/MM/yyyy");
        txtFromDate.Text = Fsdate;
        txtToDate.Text = Fedate;
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = Convert.ToString(Convert.ToDateTime(txtFromDate.Text).AddMonths(12).AddDays(-1));
    }

    protected void radStaff_CheckedChanged(object sender, EventArgs e)
    {
        trEligibleFor.Visible = false;
        trEmployee.Visible = false;
        trStaff.Visible = true;
        tblEmployee.Visible = true;
    }

    protected void radEmployee_CheckedChanged(object sender, EventArgs e)
    {
        trEligibleFor.Visible = true;
        trEmployee.Visible = true;
        trStaff.Visible = false;
        tblEmployee.Visible = true;
    }


    private string GetParams()
    {
        
        string param;
        param = string.Empty;
        if (trEmployee.Visible)
        {
            param += "@P_IDNO=" + Convert.ToInt32(ddlemployee.SelectedValue);
        }
        else
        {
            param += "@P_IDNO=0";
        }

        if (trStaff.Visible)
        {
            param += ",@P_STAFFNO=" + Convert.ToInt32(ddlStaff.SelectedValue);
        }
        else
        {
            param += ",@P_STAFFNO=0";
        }
        
        param += ",@P_FSDATE=" + txtFromDate.Text;
        param += ",@P_FEDATE=" + txtToDate.Text;

        return param;
    }



}
