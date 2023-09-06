//======================================================================================
// PROJECT NAME  : UAIMS                                                        
// MODULE NAME   : Leave Mgt.
// PAGE NAME     : AnnualIncrement.aspx                                    
// CREATION DATE : 21-APRIL-2015                                                  
// CREATED BY    : Swati Ghate                                                      
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;


using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class AnnualLWPReport: System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLC = new LeavesController();
   

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (!Page.IsPostBack)
        {

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
   
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
              DateTime dt1 = Convert.ToDateTime(txtFromdt.Text.Trim());
            DateTime dt2 = Convert.ToDateTime(txtTodt.Text.Trim());
            TimeSpan ts = dt1.Subtract(dt2);
            // int diffMonth = Math.Abs((dt2.Year - dt1.Year) * 12 + dt1.Month - dt2.Month);
            divMsg.InnerHtml = string.Empty;
            int diffDay = Math.Abs(dt2.Month - dt1.Month); //+ dt1.Month - dt2.Month)
            if (diffDay > 1)
            {
                ShowMessage("Date difference Should not more than 1 Month.");
                return;
            }
            else
            {
                int ret = Convert.ToInt32(objLC.CheckAttendance(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text)));
                if (ret == 1)
                {
                    //code to call lwp report.
                    ShowReport("LWP_Report", "ESTB_LWP_Report.rpt");
                }
                else if (ret == 0)
                {
                    objCommon.DisplayMessage("Sorry! Please Process Attendance", this);
                    return;
                }
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=username=" + Session["username"].ToString() + ",@P_FSDATE=" + txtFromdt.Text.Trim() + ",@P_FEDATE=" + txtTodt.Text.Trim() + ",@P_DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue); //+ ",FSDATE=" + Convert.ToDateTime(txtFromdt.Text.Trim()).ToString("dd/MMM/yyyy") + ",FEDATE=" + Convert.ToDateTime(txtTodt.Text.Trim()).ToString("dd/MMM/yyyy")
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEPT_NO=0,@P_FDT=" + Convert.ToDateTime(txtFromdt.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_TDT=" + Convert.ToDateTime(txtTodt.Text.Trim()).ToString("yyyy-MM-dd");
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromdt.Text =txtTodt.Text= string.Empty;
       
    }


}
