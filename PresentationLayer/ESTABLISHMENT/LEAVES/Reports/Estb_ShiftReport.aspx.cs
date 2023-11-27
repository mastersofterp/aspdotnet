using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;
using System.Collections;
using System.Web;

public partial class ESTABLISHMENT_LEAVES_Reports_Estb_ShiftReport : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    CheckPageAuthorization();
                    Page.Title = Session["coll_name"].ToString();

                    if (Request.QueryString["pageno"] != null)
                    {

                    }

                    FillCollege();
                    //FillDepartment();
                    FillStaffType();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffTypeType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void FillDepartment()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffTypeType ->" + ex.Message + "  " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string FDATE, TODATE;
            if (txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____" && txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____")
            {

                FDATE = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
                FDATE = FDATE.Substring(0, 10);
                TODATE = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
                TODATE = TODATE.Substring(0, 10);

            }
            else
            {
                FDATE = "9999-12-31";
                TODATE = "9999-12-31";
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,SHIFT," + rptFileName;
            url += "&param=@P_FROMDATE=" + FDATE.ToString().Trim() + ",@P_TODATE=" + TODATE.ToString().Trim() + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlstafftype.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime DtFrom, DtTo;
            DtFrom = Convert.ToDateTime(txtFromdt.Text);
            DtTo = Convert.ToDateTime(txtTodt.Text);
            if (DtTo < DtFrom)
            {
                MessageBox("To Date Should be Greater than  or equal to From Date");
                txtTodt.Text = string.Empty;
                return;
            }
            else
            {
                ShowReport("Shift Report", "ShiftReport.rpt");
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlCollege.SelectedIndex = ddlstafftype.SelectedIndex = 0;
            txtFromdt.Text = txtTodt.Text = string.Empty;
        }
        catch (Exception ex)
        {
        }
    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime DtFrom, DtTo;
            DtFrom = Convert.ToDateTime(txtFromdt.Text);
            DtTo = Convert.ToDateTime(txtTodt.Text);
            if (DtTo < DtFrom)
            {
                MessageBox("To Date Should be Greater than  or equal to From Date");
                txtTodt.Text = string.Empty;
                btnReport.Enabled = false;
                return;

            }
            else
            {
                btnReport.Enabled = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
}