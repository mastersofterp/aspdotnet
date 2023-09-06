using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Web.Caching;
using System.IO;
using System.Drawing;
using System.Configuration;

public partial class EMP_APPRAISAL_TRANSACTION_StatusReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpApprEnt objEA = new EmpApprEnt();
    EmployeeAppraisal_Controller objAPPRController = new EmployeeAppraisal_Controller();
  
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlSession, "APPRAISAL_SESSION_MASTER", "SESSION_ID", "SESSION_NAME", "IS_ACTIVE = 1", "SESSION_ID DESC");
                    objCommon.FillDropDownList(ddlDeptStatusRepo, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO <> 0 AND SUBDEPT<>'-'", "SUBDEPTNO");
                   // objCommon.FillDropDownList(ddlAppraisaltype, "APPRAISAL_APPRAISALTYPE", "AT_ID", "APPRAISAL_TYPE", "FILLBYESTB > 0", "AT_ID");
                    ViewState["APPRAISAL_EMPLOYEE_ID"] = "";

                    ViewState["deptno"] = 0;
                    //BindEmpStatusList(Convert.ToInt32(ddlDeptStatusRepo.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));

                    BindEmpStatusList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));


                    if (Convert.ToInt32(Session["usertype"]) == 1)          //admin user
                    {                        
                        //BindEmpStatusList(Convert.ToInt32(ddlDeptStatusRepo.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
                       BindEmpStatusList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
                    }
                    else
                    {
                        // return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_StatusReport.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to check page authorization.  
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }
    private void BindEmpStatusList(int SessionNo, int ua_no)
    {
        try
        {
             // 08-09-2022
            DataSet ds = null;
            // ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO) LEFT JOIN APPRAISAL_EMPLOYEE IE ON (IE.EMPLOYEE_ID = E.IDNO)", "E.IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN IE.LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN IE.ASSESS_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS ASSESS_LOCK", "P.PSTATUS='Y' AND E.STAFFNO = 3 AND (E.SUBDEPTNO =" + deptEmp + " OR " + deptEmp + "=0) AND (IE.SESSIONNO=" + SessionNo + " OR IE.SESSIONNO IS NULL)", "E.IDNO");


           // int PA = Convert.ToInt32(objCommon.LookUp("APPRAISAL_PASSING_AUTHORITY", "PANO", "UA_NO =" + Session["userno"]));
           // if (PA == 2)
           // {
           //     ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO = B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID = C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID  = E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.[STATUS] IN ('P','L') AND (C.SESSIONNO=" + SessionNo + " OR C.SESSIONNO IS NULL) AND B.UA_NO =" + ua_no +"AND C.REPORT_LOCK ="+1, "E.IDNO");
           
           // }
           // else if (PA == 3)
           // {
           //     ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO = B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID = C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID  = E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.[STATUS] IN ('P','L') AND (C.SESSIONNO=" + SessionNo + " OR C.SESSIONNO IS NULL) AND B.UA_NO =" + ua_no + "AND C.REPORT_LOCK =" + 1, "E.IDNO");
           

           // }
           // else if (PA == 3)
           // {
           //     ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO = B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID = C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID  = E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.[STATUS] IN ('P','L') AND (C.SESSIONNO=" + SessionNo + " OR C.SESSIONNO IS NULL) AND B.UA_NO =" + ua_no + "AND C.REPORT_LOCK =" + 1, "E.IDNO");


           // }
           // else if (PA == 4)
           // {
           //     ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO = B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID = C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID  = E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.[STATUS] IN ('P','L') AND (C.SESSIONNO=" + SessionNo + " OR C.SESSIONNO IS NULL) AND B.UA_NO =" + ua_no + "AND C.REPORT_LOCK =" + 1, "E.IDNO");


           // }
           //  else
           // {

           //     ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO = B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID = C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID  = E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.[STATUS] IN ('P','L') AND (C.SESSIONNO=" + SessionNo + " OR C.SESSIONNO IS NULL) AND B.UA_NO =" + ua_no, "E.IDNO");
           //}
           // ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO=B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID=C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID=E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO=P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO)  INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME,  C.SESSIONNO, B.UA_NO", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK=1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK=1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.STATUS IN ('P','L')", "E.IDNO");

            ds = objCommon.FillDropDown("APPRAISAL_REPORTING_AUTHORITY A INNER JOIN APPRAISAL_PASSING_AUTHORITY B ON (A.PANO = B.PANO) INNER JOIN APPRAISAL_EMPLOYEE C ON (A.APPRAISAL_EMPLOYEE_ID = C.APPRAISAL_EMPLOYEE_ID) INNER JOIN PAYROLL_EMPMAS E ON (C.EMPLOYEE_ID  = E.IDNO) INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SUBDEPT PD ON (E.SUBDEPTNO = PD.SUBDEPTNO) INNER JOIN USER_ACC U ON (U.UA_NO=B.UA_NO) INNER JOIN PAYROLL_SUBDESIG  D ON ( E.SUBDESIGNO = D.SUBDESIGNO)", "A.RA_NO, A.APPRAISAL_EMPLOYEE_ID, A.COLLEGE_NO, A.SRNO, B.PANO, B.PANAME, C.EMPLOYEE_ID AS IDNO, E.PFILENO,isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "PD.SUBDEPT, D.SUBDESIG, (CASE WHEN C.USER_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS LOCK, (CASE WHEN C.REPORT_LOCK = 1 THEN 'COMPLETE' ELSE 'INCOMPLETE' END) AS REPORT_LOCK, (CASE U.UA_TYPE WHEN 3 THEN 'T' ELSE 'NT' END) AS EMPLOYEE_TYPE", "A.[STATUS] IN ('P','L') AND (C.SESSIONNO=" + SessionNo + " OR C.SESSIONNO IS NULL) AND B.UA_NO =" + ua_no, "E.IDNO");
          
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpStatus.DataSource = ds;
                lvEmpStatus.DataBind();
                //btnReport.Visible = true;     //20062022

                
                
            }
            else
            {
                //lvEmpStatus.DataSource = ds;
                //lvEmpStatus.DataBind();
                //btnReport.Visible = true;

                lvEmpStatus.DataSource = null;
                lvEmpStatus.DataBind();
                //btnReport.Visible = false;        //20062022
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_StatusReport.BindEmpStatusList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {  
        BindEmpStatusList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
    }   

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindEmpStatusList(Convert.ToInt32(ddlDeptStatusRepo.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
        BindEmpStatusList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
    }
    protected void ddlDeptStatusRepo_SelectedIndexChanged(object sender, EventArgs e)
    { 
        BindEmpStatusList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
    }
    

    // This button is used to show list of faculties who fill proforma &  who don't fill.
    protected void btnShow_Click(object sender, EventArgs e)
    {
       // BindEmpStatusList(Convert.ToInt32(ddlDeptStatusRepo.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));   //, Convert.ToInt32(ddlAppraisaltype.SelectedValue));
        BindEmpStatusList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
    }

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("StatusReport", "rptProformaStatus.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_StatusReport.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void btnReview_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Button btnReview = sender as Button;

    //        ViewState["IDNO"] = int.Parse(btnReview.CommandName);
    //        int idno = int.Parse(btnReview.CommandName);
    //        int Appraisal_Employee_id = int.Parse(btnReview.ToolTip);
    //        int Session_No = Convert.ToInt32(ddlSession.SelectedValue);
    //        Response.Redirect("ClassIIIAppraisal.aspx?pageno=1159&autho=Y&AUTHORITYUANO=" + Convert.ToInt32(Session["userno"]) + "&empid=" + Convert.ToInt32(ViewState["IDNO"]) + "&SRNO=" + btnReview.CommandArgument + "&APPRAISAL_EMPLOYEE_ID=" + Appraisal_Employee_id + "&SESSION_NO=" + Session_No, false);
    //        //Response.Redirect("ClassIIIAppraisal.aspx?pageno=1129" + "&empid=" + btnReview.CommandName + "&SessionNo=" + Convert.ToInt32(ddlSession.SelectedValue), false);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "EmpAppraisal_StatusReport.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}

    protected void btnReview_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnReview = sender as Button;
            ViewState["IDNO"] = int.Parse(btnReview.ToolTip); //btnReview.CommandName
            int idno = int.Parse(btnReview.ToolTip); // int.Parse(btnReview.CommandName);
            string EmployeeType = btnReview.CommandName;


           
            Session["APAR_autho"] = "Y";
            Session["APAR_AUTHORITYUANO"] = Convert.ToInt32(Session["userno"]);
            Session["APAR_empid"] = Convert.ToInt32(ViewState["IDNO"]);
            Session["APAR_SRNO"] = btnReview.CommandArgument;
            Session["APAR_EmployeeType"] = btnReview.CommandName;

           // Response.Redirect("../Transaction/EmployeeAppraisalForm.aspx?pageno=1727");
            //Response.Redirect("../Transaction/EmployeeAppraisalForm.aspx?pageno=1727");
           //  Response.Redirect("../Transaction/EmployeeAppraisalForm.aspx?pageno=1727," + "&pageno=" + 1727);
            int pagno = 1727;
            Response.Redirect("EmployeeAppraisalForm.aspx?pageno=" + pagno);
              
            //Response.Redirect("../Transaction/EmployeeAppraisalForm.aspx?pageno=1727" , false);
          




           // Response.Redirect("../Transaction/EmployeeAppraisalForm.aspx?pageno=2798&autho=Y&AUTHORITYUANO=" + Convert.ToInt32(Session["userno"]) + "&empid=" + Convert.ToInt32(ViewState["IDNO"]) + "&SRNO=" + btnReview.CommandArgument + "&EmployeeType=" + btnReview.CommandName, false);
            //// Response.Redirect("" + Convert.ToInt32(Session["userno"]) + "&empid=" + Convert.ToInt32(ViewState["IDNO"]) + "&SRNO=" + btnReview.CommandArgument + "&EmployeeType=" + btnReview.CommandName, false);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_StatusReport.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["IDNO"] = int.Parse(btnPrint.CommandName);
            ViewState["SRNO"] = int.Parse(btnPrint.CommandArgument);
            string ua_type = objCommon.LookUp("APPRAISAL_EMPLOYEE", "UA_TYPE", "EMPLOYEE_ID=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

            if (ua_type == "3")
            {

                if (ViewState["SRNO"].ToString() == "1")
                {
                    ShowReport("PBAS Assessment By Reporting Officer", "AppraisalFacultyReport.rpt");
                }
                else if (ViewState["SRNO"].ToString() == "2")
                {
                    ShowReport("PBAS Assessment By Reviewing Officer", "AppraisalFacultyReport.rpt");
                }
                else
                {
                    ShowReport("PBAS Assessment By Counter Sign Officer", "AppraisalFacultyReport.rpt");
                }
            }
            else
            {
                if (ViewState["SRNO"].ToString() == "1")
                {
                    //ShowReport("APAR Assessment By Reporting Officer", "AppraisalFacultyReport.rpt");
                    ShowReport("Employee-Appraisal", "AppraisalFacultyReport.rpt");
                }
                else if (ViewState["SRNO"].ToString() == "2")
                {
                    ShowReport("APAR Assessment By Reviewing Officer", "AppraisalFacultyReport.rpt");
                }
                else
                {
                    ShowReport("APAR Assessment By Counter Sign Officer", "AppraisalFacultyReport.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_StatusReport.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEmpStatusReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("EMP_APPRAISAL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,EMP_APPRAISAL," + rptFileName;
            url += "&param=@P_EMP_ID=" + Convert.ToInt32(ViewState["IDNO"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

          
            //string appraisalEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            //ViewState["APPRAISAL_EMPLOYEE_ID"] = appraisalEmp_Id;
       
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("EMP_APPRAISAL")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=SelfAppraisal.pdf";
            //url += "&path=~,Reports,Emp_Appraisal," + rptFileName;
            //url += "&param=@P_SESSION_ID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_APPRAISAL_EMPLOYEE_ID=" + Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);

            string appraisalEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            ViewState["APPRAISAL_EMPLOYEE_ID"] = appraisalEmp_Id;


            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("IQAC")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("EMP_APPRAISAL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=SelfAppraisal.pdf";
            url += "&path=~,Reports,Emp_Appraisal," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EMPLOYEE_ID=" + appraisalEmp_Id + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

}