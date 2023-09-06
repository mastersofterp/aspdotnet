using System;
using System.Collections.Generic;
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
using System.IO;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;

public partial class UserReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
        }
    }

    protected void rdUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbDate.Checked = false;
        rbRangeDate.Checked = false;
        if (rdUser.SelectedValue == "1")
        {
            //divActive.Visible = true;
            //divDeDeactive.Visible = false;
            divrb.Visible = true;
            btnReport.Visible = false;
            btnCancel.Visible = false;
            divRangeDate.Visible = false;
            divDate.Visible = false;
            btnExcel.Visible = false;
            btnExcelMonthWiseCount.Visible = false;
        }
        if (rdUser.SelectedValue == "2")
        {
            //divDeDeactive.Visible = true;
            //divActive.Visible = false;
            divrb.Visible = true;
            divRangeDate.Visible = false;
            divDate.Visible = false;
            btnReport.Visible = false;
            btnCancel.Visible = false;
            btnExcel.Visible = false;
            btnExcelMonthWiseCount.Visible = false;
            //btnReport.Visible = true;
            //btnCancel.Visible = true;
        }
        //txtdate.Text = string.Empty;
        //txtFromdate.Text = string.Empty;
        //txtTodate.Text=string.Empty;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (rbDate.Checked == false && rbRangeDate.Checked == false)
        {
            objCommon.DisplayMessage(this.updpnlUser, "Please Select Option", this.Page);
            return;
        }
        else
        {
            if (txtdate.Text == string.Empty && txtFromdate.Text == string.Empty && txtTodate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updpnlUser, "Please Select Date", this.Page);
                return;
            }
            else
                ShowReport("user_Details", "rptuserdetailsRegistered.rpt", Convert.ToInt32(rdUser.SelectedValue));
        }
    }

    private void ShowReport(string reportTitle, string rptFileName,int userstatus)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("userreports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (userstatus == 1)
            {
                int status;
                if (rbDate.Checked == true)
                {
                    url += "&param=@P_USERSTATUS=" + Convert.ToInt32(rdUser.SelectedValue) + ",@P_STATUS=" + 1 + ",@P_DATE=" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + ",@P_FROMDATE=" + string.Empty + ",@P_TODATE=" + string.Empty + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                if (rbRangeDate.Checked == true)
                {
                    url += "&param=@P_USERSTATUS=" + Convert.ToInt32(rdUser.SelectedValue) + ",@P_STATUS=" + 2 + ",@P_DATE=" + string.Empty + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
            }
            if (userstatus == 2)
            {
                int status;
                if (rbDate.Checked == true)
                {
                    url += "&param=@P_USERSTATUS=" + Convert.ToInt32(rdUser.SelectedValue) + ",@P_STATUS=" + 1 + ",@P_DATE=" + Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd") + ",@P_FROMDATE=" + string.Empty + ",@P_TODATE=" + string.Empty + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
                if (rbRangeDate.Checked == true)
                {
                    url += "&param=@P_USERSTATUS=" + Convert.ToInt32(rdUser.SelectedValue) + ",@P_STATUS=" + 2 + ",@P_DATE=" + string.Empty + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                }
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TabulationChart.ShowFinalGradeCard() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void rbActiveDate_CheckedChanged(object sender, System.EventArgs e)
    //{
    //    divActiveDate.Visible = true;
    //    divActiveRangeDate.Visible = false;
    //}

    //protected void rbActiveRangeDate_CheckedChanged(object sender, System.EventArgs e)
    //{
    //    divActiveRangeDate.Visible = true;
    //    divActiveDate.Visible = false;
    //}

    //protected void rbDeactiveDate_CheckedChanged(object sender, System.EventArgs e)
    //{
    //    divDeActiveDate.Visible = true;
    //    divDeActiveRangeDate.Visible = false;
    //}

    //protected void rbDeActiveRangeDate_CheckedChanged(object sender, System.EventArgs e)
    //{
    //    divDeActiveRangeDate.Visible = true;
    //    divDeActiveDate.Visible = false;
    //}

    protected void rbDate_CheckedChanged(object sender, System.EventArgs e)
    {
        divDate.Visible = true;
        divRangeDate.Visible = false;
        txtFromdate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        btnReport.Visible = true;
        btnCancel.Visible = true;
        btnExcel.Visible = true;
        btnExcelMonthWiseCount.Visible = false;
    }

    protected void rbRangeDate_CheckedChanged(object sender, System.EventArgs e)
    {
        divRangeDate.Visible = true;
        divDate.Visible = false;
        txtdate.Text = string.Empty;
        btnReport.Visible = true;
        btnCancel.Visible = true;
        btnExcel.Visible = true;
        btnExcelMonthWiseCount.Visible = true;
    }

    protected void btnExcel_Click(object sender, System.EventArgs e)
    {
        if (txtdate.Text == string.Empty && txtFromdate.Text == string.Empty && txtTodate.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.updpnlUser, "Please Select Date", this.Page);
            return;
        }
        string DATE = string.Empty;
        string FROMDATE = string.Empty;
        string TODATE = string.Empty;
        int STATUS = 0;
        int User_Status = Convert.ToInt32(rdUser.SelectedValue);
        if (rbRangeDate.Checked == true)
        {
            STATUS = 2;
            DATE = string.Empty;
            FROMDATE = txtFromdate.Text;
            TODATE = txtTodate.Text;
        }
        if (rbDate.Checked == true)
        {
            STATUS = 1;
            DATE = txtdate.Text;
            FROMDATE = string.Empty;
            TODATE = string.Empty;
        }
        GridView gv = new GridView();
        DataSet ds = objCommon.getUserReportsDetails(User_Status, STATUS, DATE, FROMDATE, TODATE);
        if (ds!=null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["UA_NO"]);
            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["UA_PWD"]);
            ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["UA_STATUS"]);
            gv.DataSource = ds;
            gv.DataBind();
            string Attachment = "Attachment; filename=User_Login_Details.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.ContentType = "application/ms-excel";
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updpnlUser, "No Data Found.", this.Page);
        }
    }

    protected void btnExcelMonthWiseCount_Click(object sender, System.EventArgs e)
    {
        if (txtdate.Text == string.Empty && txtFromdate.Text == string.Empty && txtTodate.Text == string.Empty)
        {
            objCommon.DisplayMessage(this.updpnlUser, "Please Select Date", this.Page);
            return;
        }
        string DATE = string.Empty;
        string FROMDATE = string.Empty;
        string TODATE = string.Empty;
        int STATUS = 0;
        int User_Status = Convert.ToInt32(rdUser.SelectedValue);
        if (rbRangeDate.Checked == true)
        {
            STATUS = 2;
            DATE = string.Empty;
            FROMDATE = txtFromdate.Text;
            TODATE = txtTodate.Text;
        }
        if (rbDate.Checked == true)
        {
            STATUS = 1;
            DATE = txtdate.Text;
            FROMDATE = string.Empty;
            TODATE = string.Empty;
        }
        GridView gv = new GridView();
        DataSet ds = objCommon.getUser_Wise_Count_for_Excel_Report(User_Status, STATUS, DATE, FROMDATE, TODATE);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
           
            gv.DataSource = ds;
            gv.DataBind();
            string Attachment = "Attachment; filename=Month_Wise_User_Count.xls";
            ds.Tables[0].TableName = "Month_Wise_User_Count";
            ds.Tables[1].TableName ="Student_Details";
            ds.Tables[2].TableName = "Faculty_HOD_Details";
            ds.Tables[3].TableName = "Non_Teaching_Details";
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt.Rows.Count > 0)
                    {
                        wb.Worksheets.Add(dt);
                    }
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Month_Wise_User_Count.xlsx");
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
            objCommon.DisplayMessage(updpnlUser, "No Data Found.", this.Page);
        }
    }
}