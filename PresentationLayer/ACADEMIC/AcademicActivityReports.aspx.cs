//===============================================//
// MODULE NAME   : RFC ERP Portal (RFC Common Code)
// PAGE NAME     : Academic Activity Report
// CREATION DATE : 
// CREATED BY    :  
// Version       : 1.0.0
//===============================================//
// Version       Modified On         Modified By          Purpose
//----------------------------------------------------------------------------------------------------------------------------------------------
// 1.0.1         04/04/2024         Jay Takalkhede        Add Session and Multiselected College Instanct of CollegeSession (TkNo.56910)
//----------------------------------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;


public partial class ACADEMIC_AcademicActivityReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaims = new UAIMS_Common();
    AcademicReportsController objAcdReportcon = new AcademicReportsController();

    #region Page Load
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
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONID DESC"); 
            //this.BindSession();

        }
    }
    #endregion Page Load

    #region BindSession
    private void BindSession()
    {
        AcademinDashboardController objADEController = new AcademinDashboardController();
        DataSet ds = objADEController.Get_Session_Activity_Report(1, Convert.ToInt32(ddlSession.SelectedValue));
        ddlCollegeSession.Items.Clear();
        ddlCollegeSession.Items.Add("Please Select");
        ddlCollegeSession.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstbxCollegeSession.DataSource = ds;
            lstbxCollegeSession.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstbxCollegeSession.DataTextField = ds.Tables[0].Columns[1].ToString();
            lstbxCollegeSession.DataBind();
        }
    }
    #endregion BindSession

    #region ExportToExcel
    private void ExportToExcel(string FileName, DataSet ds)
    {
        GridView gv = new GridView();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            string Attachment = "Attachment; filename=" + FileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                ds.Tables[i].TableName = ds.Tables[i].Rows[0]["SHEET_NAME"].ToString();
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Remove("SHEET_NAME");
                        wb.Worksheets.Add(dt);
                    }
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(upReports, "No Data Found", this.Page);
            return;
        }
    }
    #endregion ExportToExcel

    #region Excel Reports

    #region Offered Course Status
    protected void btnOfferedCourseStatus_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex==0)
        {
            objCommon.DisplayUserMessage(upReports, "Please select Session.", this.Page);
            return;
        }
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one College.", this.Page);
            return;
        }
        string Collegenos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Collegenos == string.Empty)
                {
                    Collegenos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Collegenos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
        DataSet ds = objAcdReportcon.Offered_Course_Status_Excel(Convert.ToInt32(ddlSession.SelectedValue),Collegenos);
        this.ExportToExcel("Offered_Course_Status", ds);
    }
    #endregion

    #region Course Reg Status
    protected void btnCourseRegStatus_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayUserMessage(upReports, "Please select Session.", this.Page);
            return;
        }
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Collegenos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Collegenos == string.Empty)
                {
                    Collegenos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Collegenos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
        DataSet ds = objAcdReportcon.GetSessionwiseRegistrationCount(Convert.ToInt32(ddlSession.SelectedValue),Collegenos);
        this.ExportToExcel("Course_Registration_Status", ds);
    }
    #endregion

    #region Time Table Status
    protected void btnTimeTableStatus_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayUserMessage(upReports, "Please select Session.", this.Page);
            return;
        }
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Collegenos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Collegenos == string.Empty)
                {
                    Collegenos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Collegenos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        DataSet ds = objAcdReportcon.GetTimeTableStatus(Convert.ToInt32(ddlSession.SelectedValue), Collegenos);
        this.ExportToExcel("Time_Table_Status", ds);
    }
    #endregion

    #region Teaching Attendance Status
    protected void btnTeachingAttendanceStatus_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayUserMessage(upReports, "Please select Session.", this.Page);
            return;
        }
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Collegenos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Collegenos == string.Empty)
                {
                    Collegenos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Collegenos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
        DataSet ds = objAcdReportcon.GetTeachingPlanAttendanceStatus(Convert.ToInt32(ddlSession.SelectedValue), Collegenos);
        this.ExportToExcel("TeachingPlan_Attendance_Status", ds);
    }
    #endregion

    #region Course Teacher Allotment
    protected void btnCourseTeacherAllotment_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayUserMessage(upReports, "Please select Session.", this.Page);
            return;
        }
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Collegenos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Collegenos == string.Empty)
                {
                    Collegenos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Collegenos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
        DataSet ds = objAcdReportcon.GetCourseTeacherAllotmentStatus(Convert.ToInt32(ddlSession.SelectedValue), Collegenos);

        this.ExportToExcel("Course_Teacher_Allotment_Status", ds);
    }
    #endregion

    #region TimeTable Cancel
    protected void btnTimeTableCancel_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayUserMessage(upReports, "Please select Session.", this.Page);
            return;
        }
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Collegenos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Collegenos == string.Empty)
                {
                    Collegenos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Collegenos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
        DataSet ds = objAcdReportcon.GetCancelTimeTableReport(Convert.ToInt32(ddlSession.SelectedValue), Collegenos);
        this.ExportToExcel("Cancel_Time_Table_Report", ds);
    }
    #endregion

    #endregion Excel Reports

    #region DDl 
    //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstbxCollegeSession.Items.Clear();
        if (ddlSession.SelectedIndex > 0)
        {
            this.BindSession();
        }
        else
        {
            lstbxCollegeSession.Items.Clear();
        }
    }
    #endregion

    #region Cancel
    //Updated by jay takalkhede on dated 03042024 add college and session both on the place of session
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion
}