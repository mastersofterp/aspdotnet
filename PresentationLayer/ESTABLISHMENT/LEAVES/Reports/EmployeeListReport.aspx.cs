//=====================================================================
//CREATED BY: Swati Ghate
//CREATED DATE:29-09-2015
//PURPOSE: TO DISPLAY EMPLOYEE LIST REPORT AS PER SCHOOL & DEPARTMENT
//=====================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ESTABLISHMENT_LEAVES_Reports_EmployeeListReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaveName = new LeavesController();
    SchoolMaster objSM = new SchoolMaster();
    SchoolController objSC = new SchoolController();
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
                pnlAdd.Visible = true;
            
                pnlbutton.Visible = true;

               
                FillDropDown();
             }

        }
    }
    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchool, "PAYROLL_LEAVE_SCHOOL", "SCHOOL_NO", "SCHOOL_NAME", "SCHOOL_NO>0", "SCHOOL_NAME");
            //objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
            
            
            //select DEPT.SUBDEPT from PAYROLL_LEAVE_SCHOOL_DEPARTMENT SD INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=SD.SUBDEPTNO) WHERE isnull(school_no,0)=0
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_LEAVE_SCHOOL_DEPARTMENT SD INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=SD.SUBDEPTNO)", "SD.SUBDEPTNO", "DEPT.SUBDEPT", "ISNULL(SD.SCHOOL_NO,0)=0", "DEPT.SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_EmployeeListReport.FillDropDown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            objSM.SCHOOL_NO = Convert.ToInt32(ddlSchool.SelectedValue);
            objSM.DEPT_NO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objSM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            if (ddlSchool.SelectedIndex > 0)
            {
                if (ddlDepartment.SelectedIndex > 0)
                {
                    DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)  INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO) INNER JOIN PAYROLL_LEAVE_SCHOOL SCH ON(SCH.SCHOOL_NO=E.SCHOOL_NO)", "*", "", "P.PSTATUS='Y' AND E.SUBDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + " AND E.SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + "", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ShowReport("List", "EmployeeListReport.rpt");
                    }
                    else
                    {
                        MessageBox("Record Not Found!");
                        return;
                    }
                }
                else
                {
                    DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)  INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO) INNER JOIN PAYROLL_LEAVE_SCHOOL SCH ON(SCH.SCHOOL_NO=E.SCHOOL_NO) 	 INNER JOIN PAYROLL_LEAVE_SCHOOL_DEPARTMENT SD ON(SD.SCHOOL_NO=E.SCHOOL_NO AND SD.SUBDEPTNO=E.SUBDEPTNO)", "*", "", "P.PSTATUS='Y' AND E.SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + "", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ShowReport("List", "EmployeeListReport.rpt");
                    }
                    else
                    {
                        MessageBox("Record Not Found!");
                        return;
                    }
                }
            }
            else if (ddlDepartment.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)  INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO) INNER JOIN PAYROLL_LEAVE_SCHOOL SCH ON(SCH.SCHOOL_NO=E.SCHOOL_NO)", "*", "", "P.PSTATUS='Y' AND E.SUBDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("List", "EmployeeListReport.rpt");
                }
                else
                {
                    MessageBox("Record Not Found!");
                    return;
                }
            }
            else
            {
                DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)  INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO) INNER JOIN PAYROLL_LEAVE_SCHOOL SCH ON(SCH.SCHOOL_NO=E.SCHOOL_NO)", "*", "", "P.PSTATUS='Y' ", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("List", "EmployeeListReport.rpt");
                }
                else
                {
                    MessageBox("Record Not Found!");
                    return;
                }
            }
           
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_EmployeeListReport.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
                 
            url += "&param=@P_SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + ",@P_SUBDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString(); 
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void Clear()
    {
        ddlDepartment.SelectedIndex = ddlSchool.SelectedIndex = 0;  
    }    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }     
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        //select DEPT.SUBDEPT from PAYROLL_LEAVE_SCHOOL_DEPARTMENT SD INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=SD.SUBDEPTNO) WHERE SCHOOL_NO=11
        objCommon.FillDropDownList(ddlDepartment, "PAYROLL_LEAVE_SCHOOL_DEPARTMENT SD INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=SD.SUBDEPTNO)", "SD.SUBDEPTNO", "DEPT.SUBDEPT", "SD.SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + "", "DEPT.SUBDEPT");
    }
}