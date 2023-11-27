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
            this.BindSession();

        }
    }

    private void BindSession()
    {
        AcademinDashboardController objADEController = new AcademinDashboardController();
        DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
        ddlCollegeSession.Items.Clear();
        ddlCollegeSession.Items.Add("Please Select");
        ddlCollegeSession.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ddlCollegeSession.DataSource = ds;
            //ddlCollegeSession.DataValueField = ds.Tables[0].Columns[0].ToString();
            //ddlCollegeSession.DataTextField = ds.Tables[0].Columns[4].ToString();
            //ddlCollegeSession.DataBind();
            //ddlCollegeSession.SelectedIndex = 0;

            lstbxCollegeSession.DataSource = ds;
            lstbxCollegeSession.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstbxCollegeSession.DataTextField = ds.Tables[0].Columns[4].ToString();
            lstbxCollegeSession.DataBind();
            //lstbxCollegeSession.SelectedIndex = 0;
        }
    }
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
    }
    protected void btnOfferedCourseStatus_Click(object sender, EventArgs e)
    {
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Sessionnos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Sessionnos == string.Empty)
                {
                    Sessionnos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Sessionnos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }

        DataSet ds = objAcdReportcon.Offered_Course_Status_Excel(Sessionnos);
        this.ExportToExcel("Offered_Course_Status", ds);
    }
    protected void btnCourseRegStatus_Click(object sender, EventArgs e)
    {
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Sessionnos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Sessionnos == string.Empty)
                {
                    Sessionnos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Sessionnos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        DataSet ds = objAcdReportcon.GetSessionwiseRegistrationCount(Sessionnos);
        this.ExportToExcel("Course_Registration_Status", ds);
    }
    protected void btnTimeTableStatus_Click(object sender, EventArgs e)
    {
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Sessionnos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Sessionnos == string.Empty)
                {
                    Sessionnos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Sessionnos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        DataSet ds = objAcdReportcon.GetTimeTableStatus(Sessionnos);
        this.ExportToExcel("Time_Table_Status", ds);
    }
    protected void btnTeachingAttendanceStatus_Click(object sender, EventArgs e)
    {
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Sessionnos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Sessionnos == string.Empty)
                {
                    Sessionnos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Sessionnos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }
        DataSet ds = objAcdReportcon.GetTeachingPlanAttendanceStatus(Sessionnos);
        this.ExportToExcel("TeachingPlan_Attendance_Status", ds);
    }
    protected void btnCourseTeacherAllotment_Click(object sender, EventArgs e)
    {
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Sessionnos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Sessionnos == string.Empty)
                {
                    Sessionnos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Sessionnos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }

        DataSet ds = objAcdReportcon.GetCourseTeacherAllotmentStatus(Sessionnos);
        this.ExportToExcel("Course_Teacher_Allotment_Status", ds);
    }
    protected void btnTimeTableCancel_Click(object sender, EventArgs e)
    {
        if (lstbxCollegeSession.SelectedValue == "")
        {
            objCommon.DisplayUserMessage(upReports, "Please select atleast one Session.", this.Page);
            return;
        }
        string Sessionnos = string.Empty;
        for (int i = 0; i < lstbxCollegeSession.Items.Count; i++)
        {
            if (lstbxCollegeSession.Items[i].Selected)
            {
                if (Sessionnos == string.Empty)
                {
                    Sessionnos = lstbxCollegeSession.Items[i].Value;
                }
                else
                {
                    Sessionnos += ',' + lstbxCollegeSession.Items[i].Value;
                }
            }
        }

        DataSet ds = objAcdReportcon.GetCancelTimeTableReport(Sessionnos);
        this.ExportToExcel("Cancel_Time_Table_Report", ds);
    }
}