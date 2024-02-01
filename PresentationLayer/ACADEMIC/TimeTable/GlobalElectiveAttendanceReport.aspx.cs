using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Drawing;
using System.IO;
using ClosedXML.Excel;

public partial class ACADEMIC_TimeTable_GlobalElectiveAttendanceReport : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();

    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PopulateDropDownList();

                    
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }


    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
           
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControls()
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        if (ddlSession.SelectedIndex > 0)
        {
            CourseController objStud = new CourseController();
            DataSet dsCollegeSession = objStud.GetCollegeBySessionid(1, Convert.ToInt32(ddlSession.SelectedValue));
            ddlCollege.Items.Clear();
            ddlCollege.DataSource = dsCollegeSession;
            ddlCollege.DataValueField = "COLLEGE_ID";
            ddlCollege.DataTextField = "COLLEGE_NAME";
            ddlCollege.DataBind();
        }
        else
        {
            ddlCollege.DataSource = null;
            ddlCollege.DataBind();
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
        }
    }
  
    protected void btsShowAvgAttendance_Click(object sender, EventArgs e)
    {

    }
    protected void btnExcelAvgAttendance_Click(object sender, EventArgs e)
    {

    }
    protected void btnExcelCoursewise_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != string.Empty && txtTodate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
            {
                objCommon.DisplayMessage(this, "To Date should be greater than From Date", this.Page);
                return;
            }
        }
        else
        {
            string collegenos = string.Empty;
            foreach (ListItem itm in ddlCollege.Items)
            {
                if (itm.Selected != true)
                    continue;
                collegenos += itm.Value + ",";
            }

            collegenos = collegenos.Remove(collegenos.Length - 1);
            int sessionid = 0;
            sessionid = Convert.ToInt32(ddlSession.SelectedValue);
            string FromDate = txtFromDate.Text;
            string ToDate = txtTodate.Text;
            DataSet ds = objAttC.GetAllCoursesWiseAttendanceExcelReport(sessionid, collegenos, FromDate, ToDate);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Courses-Wise Attendance Summary";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                        wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=CourseWiseAttendanceSummary.xlsx");
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
                objCommon.DisplayMessage(this.updAttStatus, "No Record Found For Your Selection!", this.Page);
            }
        }

    }
    protected void btnShowStudentWise_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != string.Empty && txtTodate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(txtFromDate.Text))
            {
                objCommon.DisplayMessage(this, "To Date should be greater than From Date", this.Page);
                return;
            }
        }
        else
        {
            string collegenos = string.Empty;
            foreach (ListItem itm in ddlCollege.Items)
            {
                if (itm.Selected != true)
                    continue;
                collegenos += itm.Value + ",";
            }

            collegenos = collegenos.Remove(collegenos.Length - 1);
            int sessionid = 0;
            sessionid = Convert.ToInt32(ddlSession.SelectedValue);
            string FromDate = txtFromDate.Text;
            string ToDate = txtTodate.Text;
            DataSet ds = objAttC.GetAllStudentWiseAttendanceExcelReport(sessionid, collegenos, FromDate, ToDate);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Student-Wise Attendance Details";
                ds.Tables[1].TableName = "Student-Wise Attendance Summary";
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
                    ds.Tables[0].Rows.Add("No Record Found");

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count <= 0)
                    ds.Tables[1].Rows.Add("No Record Found");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                        wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=StudentWiseAttendanceSummary.xlsx");
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
                objCommon.DisplayMessage(this.updAttStatus, "No Record Found For Your Selection!", this.Page);
            }
        }
    }
}