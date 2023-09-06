//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ServiceBook_Report.aspx
// CREATION DATE : 29-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PayRoll_Pay_ServiceBook_Report : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();


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
            }

            //To fill department
            this.FillDepartMent();
        }
        else
        {
            //divMsg.InnerHtml = "";
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["userfullname"].ToString() + "," + param;
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



    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.ToString());
        //Clear();
        //Response.Redirect(Request.Url.ToString());
    }

    private void Clear()
    {
        chkPersonalMemoranda.Checked = true;
        chkDeptExamAndOtherDetails.Checked = false;
        ChkDetailsOfLoansAndAdvances.Checked = false;
        //chkDetailsOfServiceBook.Checked = false;
        chkEducationalQualification.Checked = false;
        chkFamilyParticulars.Checked = false;
        chkForeginService.Checked = false;
        //chkGoodServiceAdvIncrcPunishMent.Checked = false;
        chkIncetmentTermination.Checked = false;
        chkLeaveRecords.Checked = false;
        ChkNomination.Checked = false;
        chkMatter.Checked = false;
        chkPayRevisionOrPromotion.Checked = false;       
        chkPreviousQualifyingService.Checked = false;
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {       
            string param = this.GetParams(ddlEmployee.SelectedValue);
            this.ShowReport(param, "Service_Book_Details","Pay_ServiceBook.rpt");
    }
        
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    private void FillDepartMent()
    {
        try
        {
            objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.FillDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void FillEmployee(int deptNo)
    {
        try
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL and EM.SUBDEPTNO=" + deptNo, "EM.IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDept.SelectedValue != string.Empty )
            {
                this.TableRowVisibleTrueFalse();
                this.FillEmployee(Convert.ToInt32(ddlDept.SelectedValue));
                lblDepartment.Text = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPT", "SUBDEPTNO=" + ddlDept.SelectedValue);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.ddlDept_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }



    private void TableRowVisibleTrueFalse()
    {
        if (ddlDept.SelectedValue == "0")
            trDept.Visible = false;
        else
            trDept.Visible = true;    
    }


    private string GetParams(string idno)
    {
        string param ;
        param = string.Empty;
        if (chkPersonalMemoranda.Checked)
        {
            param += "@P_IDNO=" + idno + "*Pay_Personal_Information.rpt"; 
        }
        else 
        {
            param += "@P_IDNO=-1*Pay_Personal_Information.rpt"; 
        }

        if (chkFamilyParticulars.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Family_Details.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Family_Details.rpt";
        }

        if (chkEducationalQualification.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Qualification_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Qualification_Information.rpt";
        }

        if (chkMatter.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Matter_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Matter_Information.rpt";
        }

        if(chkDeptExamAndOtherDetails.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_DepartmantalExam_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_DepartmantalExam_Information.rpt";
        }

        //if (chkDeptExamAndOtherDetails.Checked)
        //{
        //    param += ",@P_IDNO=" + idno + "*Pay_DepartmantalExam_Information.rpt";
        //}
        //else
       // {
       //     param += ",@P_IDNO=0*Pay_DepartmantalExam_Information.rpt";
       // }

        if (chkPreviousQualifyingService.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Previous_Service.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Previous_Service.rpt";
        }


        if (chkForeginService.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Foregin_Service.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Foregin_Service.rpt";
        }


        if (ChkDetailsOfLoansAndAdvances.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Loan_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Loan_Information.rpt";
        }



        if (ChkNomination.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Nominee_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Nominee_Information.rpt";
        }

        if (chkLeaveRecords.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Leave_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Leave_Information.rpt";
        }


        if (chkTraning.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Traning_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Traning_Information.rpt";
        }

        if (chkPayRevisionOrPromotion.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_PayRevision_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_PayRevision_Information.rpt";
        }



        if (chkIncetmentTermination.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Increment_Tremination_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Increment_Tremination_Information.rpt";
        }


        return param;
    }

 


}
