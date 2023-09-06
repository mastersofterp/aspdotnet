﻿//======================================================================================
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


public partial class ESTABLISHMENT_ServiceBook_Payservicereport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
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
            }

            int user_type = Convert.ToInt32(Session["usertype"].ToString());
            if (user_type != 1)
            {
                this.FillDropDownByIdno(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(Session["userdeptno"].ToString()));
                ddlEmployee.Enabled = false;
                ddlCollege.Enabled = false;
            }
            //if (!(Convert.ToInt32(Session["userno"].ToString()) == 1 || Convert.ToInt32(Session["userno"].ToString())==11725 || Convert.ToInt32(Session["userno"].ToString())==10))
            //{   
            //    //Session["userdeptno"]

            //    this.FillDropDownByIdno(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(Session["userdeptno"].ToString()));
            //    ddlEmployee.Enabled = false;
            //    ddlCollege.Enabled = false;

            //}
            else
            {
                //To fill department
                ddlEmployee.Enabled = true;
                ddlCollege.Enabled = true;
                this.FillCollege();
            }
        }
        else
        {
            //divMsg.InnerHtml = "";
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_ServiceBook_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ServiceBook_Report.aspx");
        }
    }

    private void ShowReport(string param, string reportTitle, string rptFileName)
    {
        try
        {

            string pfileNo = objCommon.LookUp("payroll_empmas", "pfileno", "idno=" + Convert.ToInt32(ddlEmployee.SelectedValue));
            string IP = Request.ServerVariables["REMOTE_HOST"];
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,PayRoll," + rptFileName;
            url += "&path=~,Reports,Establishment,ServiceBook," + rptFileName;
            url += "&param=username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",IP=" + IP + ",@pfileNo=" + pfileNo + ",@idno=" + Convert.ToInt32(ddlEmployee.SelectedValue) + "," + param;
            //url += "&param=collegename=" + Session["coll_name"].ToString() + ",username=" + Session["userfullname"].ToString() + "," + param;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> ";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Establishment_ServiceBook_Report.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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
        //chkForeginService.Checked = false;
        chkTrainingConducted.Checked = false;
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
        this.ShowReport(param, "Service_Book_Details", "Pay_ServiceBook.rpt");
    }

    //private void FillCollege()
    //{

    //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

    //}
    private void FillCollege()
    {
        int ua_type = Convert.ToInt32(Session["usertype"]);
        if (ua_type == 3 || ua_type == 4 || ua_type == 5)
        {
            int college_no = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + ""));
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "college_id=" + college_no + "", "COLLEGE_NAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        }

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
        if (ua_type != 1)
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }
    private void FillEmployee(int collegeno)
    {
        try
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM INNER JOIN PAYROLL_PAYMAS PM ON(EM.IDNO=PM.IDNO AND PM.PSTATUS='Y')", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL and EM.COLLEGE_NO=" + collegeno, "EM.FNAME");
            // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL and EM.SUBDEPTNO=" + collegeno, "EM.IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void FillDropDownByIdno(int idNo, int deptNo)
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0 and SUBDEPTNO=" + deptNo, "SUBDEPT");
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL and EM.idno=" + idNo, "EM.FNAME");
            ddlCollege.SelectedValue = deptNo.ToString();
            ddlEmployee.SelectedValue = idNo.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.FillDropDownByIdno-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedValue != string.Empty)
            {
                this.TableRowVisibleTrueFalse();
                this.FillEmployee(Convert.ToInt32(ddlCollege.SelectedValue));
                // lblDepartment.Text = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPT", "SUBDEPTNO=" + ddlCollege.SelectedValue);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ServiceBook_Report.ddlCollege_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void TableRowVisibleTrueFalse()
    {
        if (ddlCollege.SelectedValue == "0")
            trDept.Visible = false;
        else
            trDept.Visible = true;
    }
    private string GetParams(string idno)
    {
        string param;
        param = string.Empty;
        if (chkPersonalMemoranda.Checked)
        {
            param += "@P_IDNO=" + idno + "*Pay_Personal_Information.rpt";
        }
        else
        {
            param += "@P_IDNO=-1*Pay_Personal_Information.rpt";
        }
        if (chkPersonalMemoranda.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Personal2.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Personal2.rpt";
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

        if (chkDeptExamAndOtherDetails.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_DepartmantalExam_Information.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_DepartmantalExam_Information.rpt";
        }

        if (chkPreviousQualifyingService.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Previous_Service.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Previous_Service.rpt";
        }
        if (chkTrainingConducted.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Training_Conducted.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Training_Conducted.rpt";
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


        if (chkPublicationDetails.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Publication_Details.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Publication_Details.rpt";
        }


        if (chkAdministrativeResponsibilities.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Administrative_Responsibilities.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Administrative_Responsibilities.rpt";
        }



        if (chkInvitedTalks.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_InvitedTalks.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_InvitedTalks.rpt";
        }



        return param;
    }
    private string GetParamsOLD(string idno)
    {
        string param;
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

        if (chkDeptExamAndOtherDetails.Checked)
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





        //if (chkForeginService.Checked)
        //{
        //    param += ",@P_IDNO=" + idno + "*Pay_Foregin_Service.rpt";
        //}
        //else
        //{
        //    param += ",@P_IDNO=-1*Pay_Foregin_Service.rpt";
        //}

        if (chkTrainingConducted.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Training_Conducted.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Training_Conducted.rpt";
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


        if (chkPublicationDetails.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Publication_Details.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Publication_Details.rpt";
        }


        if (chkAdministrativeResponsibilities.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_Administrative_Responsibilities.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_Administrative_Responsibilities.rpt";
        }



        if (chkInvitedTalks.Checked)
        {
            param += ",@P_IDNO=" + idno + "*Pay_InvitedTalks.rpt";
        }
        else
        {
            param += ",@P_IDNO=-1*Pay_InvitedTalks.rpt";
        }



        return param;
    }
}