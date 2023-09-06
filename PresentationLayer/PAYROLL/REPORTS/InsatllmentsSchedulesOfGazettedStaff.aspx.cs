//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE INSTALLMENT SCHEDULE                        
// CREATION DATE : 22-MAY-2010                                                          
// CREATED BY    : G.V.S.KIRAN KUMAR                                                  
// MODIFIED DATE : 15-Jun-2011
// MODIFIED BY   : MRUNAL BANSOD                                                                     
// MODIFIED DESC : Added code to give rights to the Gazetted Empoyee and for Admin .                                                                     
//======================================================================================
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class PAYROLL_REPORTS_InsatllmentsSchedulesOfGazettedStaff : System.Web.UI.Page
{
   
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objPay = new PayController();

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

        int idno = Convert.ToInt32(Session["idno"]);
        int usertype = Convert.ToInt32(Session["usertype"]);
        string stafftype = Session["staffType"].ToString();
        string Authno = Session["AuthNo"].ToString();

        string[] Auth = Authno.Split(',');
        string GazzettedPlan = Auth[0];
        string GazzettedNonPlan = Auth[1];


        if (stafftype != GazzettedPlan && stafftype != GazzettedNonPlan && usertype != 1)
        {
            Response.Redirect("~/notauthorized.aspx");
        }
        try
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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    if (usertype == 1)
                    {
                        rwstaff.Visible = true;
                        FillStaff();
                    }
                    else
                    {
                        rwstaff.Visible = false;
                    }

                }
                //Populate DropdownList
                PopulateDropDownList();


            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEmployeeInstalmentSchedule(string reportTitle, string rptFileName, string paramIdno,string reportname)
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            int usertype = Convert.ToInt32(Session["usertype"]);
            string stafftype = Session["staffType"].ToString();
            string Authno = Session["AuthNo"].ToString();

            string[] Auth = Authno.Split(',');
            string GazzettedPlan = Auth[0];
            string GazzettedNonPlan = Auth[1];
      string   group = Convert.ToString (objCommon.LookUp("PAYROLL_EMPMAS", "CLNO", "IDNO=" + idno));
            if (usertype == 1)
            {
                stafftype = ddlstaff.SelectedValue.ToString();
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));
            //int StaffNo = Convert.ToInt32(objCommon.LookUp("payroll_empmas","staffno","idno="+Convert.ToInt32(Session["idno"].ToString())));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
           
            if(reportname=="Insurance")
                url += "&param=@P_IDNO=" + paramIdno.ToString() + ",@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafftype + ",@P_GROUPNO=" + group ;
            //else if(reportname=="ImportantInstruction")


            else
                url += "&param=@P_IDNO=" + paramIdno.ToString() + ",@P_MON_YEAR=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + stafftype + ",@P_PAYHEAD=" + ddlPayhead.SelectedValue.ToString() + ",username=" + Session["username"].ToString(); 

            
            ScriptManager.RegisterClientScriptBlock(updPayBillReport, updPayBillReport.GetType(), "Message", " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.ShowEmployeeInstalmentSchedule() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            string reportName = string.Empty;

            string paramIdno = string.Empty;

            if (ddlPayhead.SelectedValue == "D1" || ddlPayhead.SelectedValue == "D2")
            {
                reportName = "ScheduleOfGPF_Gazetted.rpt";
            }
            else if (ddlPayhead.SelectedValue == "D9" || ddlPayhead.SelectedValue == "D11")
            {
                reportName = "NewPensionScheemEPF_Gazetted.rpt";
            }
            else if (ddlPayhead.SelectedValue == "D10")
            {
                reportName = "NewPensionScheemEPFGOVT_Gazetted.rpt";
            }
            else if (ddlPayhead.SelectedValue == "D4")
            {
                reportName = "IncomeTaxSchedule_Gazetted.rpt";

            }
            else
            {
                reportName = "InstallmentSchedule_Gazetted.rpt";
            }

            paramIdno = Session["idno"].ToString();

            ShowEmployeeInstalmentSchedule("Employee_InstallMent", reportName, paramIdno.Trim(),"Installment");



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?InsatllmentsSchedulesOfGazettedStaff.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=InsatllmentsSchedulesOfGazettedStaff.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL MONTH YEAR 
            objCommon.FillSalfileDropDownList(ddlMonthYear);
            //FILL PAYHEAD
            objCommon.FillDropDownList(ddlPayhead, "payroll_payhead", "PAYHEAD", "PAYFULL", "srno>15 and payshort<>'' or payshort<>null", "PAYHEAD");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_REPORTS_InstallmentSchedule.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void InsuranceSavingFund_Click(object sender, EventArgs e)
    {
        if (ddlMonthYear.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Please select month Year", this);

        }
        else
        {
            try
            {
                ShowEmployeeInstalmentSchedule("Employee_GroupInsuranceSavingFund", "GroupInsuranceSavingFund_Gazetted.rpt", Session["idno"].ToString(), "Insurance");

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "btnShow_Click.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }


    //Fill Dropdownlist With Staff 
    protected void FillStaff()
    {

        string Authno = Session["AuthNo"].ToString();
        string[] Auth = Authno.Split(',');
        string GazzettedPlan = Auth[0];
        string GazzettedNonPlan = Auth[1];
        int GP = Convert.ToInt32(GazzettedPlan);
        int GNP = Convert.ToInt32(GazzettedNonPlan);

        objCommon.FillDropDownList(ddlstaff , "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO =" + GP + "OR STAFFNO =" + GNP, "STAFFNO");

    }

    protected void btnInstructions_Click(object sender, EventArgs e)
    {
        try
        {
            ShowEmployeeInstalmentSchedule("ImportantInstruction", "InstructionsReport_gazetted.rpt", Session["idno"].ToString(), "Insurance");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "btnInstructions_Click.btnInstructions_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}
