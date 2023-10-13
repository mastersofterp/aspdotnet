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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
public partial class HOSTEL_REPORT_HostelAttendanceReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAllotmentController objRMC = new RoomAllotmentController();

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
        try
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title

                PopulateDropdown();

                Page.Title = Session["coll_name"].ToString();
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelAttendanceReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    private void PopulateDropdown()
    {
        //objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND IS_SHOW=1", "FLOCK DESC");
        ddlSession.SelectedIndex = 1;
        if (Session["usertype"].ToString() == "1")
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
        else
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 AND STATUS=1 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
        for (int i = 1; i <= 12; i++)
        {
            DateTime date = new DateTime(1900, i, 1);
            ddlFromMonth.Items.Add(new ListItem(date.ToString("MMMM"), i.ToString()));
            ddlToMonth.Items.Add(new ListItem(date.ToString("MMMM"), i.ToString()));
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControlData();
        //Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("HostelAttendanceMonthly", "HostelAttendanceMonthly.rpt");
    }

    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S INNER JOIN ACD_HOSTEL_ROOM_ALLOTMENT A ON(S.IDNO=A.RESIDENT_NO) INNER JOIN ACD_HOSTEL_RESIDENT_TYPE HRT ON(A.RESIDENT_TYPE_NO=HRT.RESIDENT_TYPE_NO)", "S.IDNO", "STUDNAME", "S.CAN=0 AND A.CAN=0 AND IS_STUDENT=1 AND A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue), "A.ROOM_NO,S.IDNO");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ",@P_FROM_MONTH=" + Convert.ToInt32(ddlFromMonth.SelectedValue) + ",@P_TO_MONTH=" + Convert.ToInt32(ddlToMonth.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlStudent.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;

            DataSet ds = objRMC.MonthlyAttandance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddlFromMonth.SelectedValue), Convert.ToInt32(ddlToMonth.SelectedValue), Convert.ToInt32(ddlStudent.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string getdate = DateTime.Now.ToString("dd/MMM/yyyy_hh:mm:ss");
                string res = string.Concat(getdate, "HostelAttendanceMonthly.xls");
                string attachment = "attachment; filename=" + res;
                //string attachment = "attachment; filename=HostelAttendanceMonthly.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updAttendance, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelAttendanceReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelAttendanceReport.aspx");
        }

    }

    //Modify by Sonali Borekar
    private void ClearControlData()
    {
        try
        {
            ddlHostel.SelectedIndex = 0;
            ddlToMonth.SelectedIndex = 0;
            ddlFromMonth.SelectedIndex = 0;
            ddlStudent.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentHostelIdentityCard.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlToMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFromMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
        {
            if ((ddlToMonth.SelectedIndex >= ddlFromMonth.SelectedIndex) || (ddlToMonth.SelectedIndex > ddlFromMonth.SelectedIndex))
            {
            }
            else
            {
                objCommon.DisplayMessage(updAttendance, "From Month should be less than To Month.", this.Page);
                ddlToMonth.SelectedIndex = 0;

                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(updAttendance, "Please select from Month related with selected Month.", this.Page);
            ddlToMonth.SelectedIndex = 0;
            return;
        }
    }
    protected void ddlFromMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlToMonth.SelectedIndex != 0 && ddlFromMonth.SelectedIndex != 0)
        {
            if (ddlToMonth.SelectedIndex >= ddlFromMonth.SelectedIndex)
            {
            }
            else
            {
                objCommon.DisplayMessage(updAttendance, "From Month should be less than to Month.", this.Page);
                ddlFromMonth.SelectedIndex = 0;
                return;
            }
        }
        else if (ddlToMonth.SelectedIndex == 0)
        {
        }
        else
        {
            objCommon.DisplayMessage(updAttendance, "Please select from month related with selected Month", this.Page);
            ddlFromMonth.SelectedIndex = 0;
            return;
        }
    }
}
