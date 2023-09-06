using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_Advance_Apply_Report : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AdvancePassingAuthorityController objAdvPassAuthCon = new AdvancePassingAuthorityController();
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                AdvancePassingAuthority objAPAuth = new AdvancePassingAuthority();
                BindLVLeaveApprStatusAll();
                int usernock = Convert.ToInt32(Session["userno"]);
                ViewState["USERNO"] = usernock;
                BindLVLeaveApprStatusAll();
                txtFromdt.Text = System.DateTime.Now.ToString();
                txtTodt.Text = System.DateTime.Now.ToString();
            }
        }
        divMsg.InnerHtml = string.Empty;
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
    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            //DateTime DT = Convert.ToDateTime (txtFromdt.Text);
            string status = ddlstatus.SelectedItem.Text;
            if (status == "Pending")
            {
                status = "P";
            }
            else if (status == "Approved")
            {
                status = "A";
            }
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);
            DataSet ds = objAdvPassAuthCon.GetPendListforLVApprovalPendingStatusParticular(Convert.ToInt32(Session["userno"]), Fdate, Tdate, status);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                dpPager.Visible = false;
            }
            else
            {
                 dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_APPLY_REPORT.BindLVAdvanceApplPendingApprovalList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVLeaveApprStatusAll();
    }

    private void ShowAdvanceApplyReport(string reportTitle, string rptFileName)
    {
        try
        {
            string status = ddlstatus.SelectedItem.Text;
            if (status == "Pending")
            {
                status = "P";
            }
            else if (status == "Approved")
            {
                status = "A";
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_STATUS=" + status + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd") + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Advance_ApplyReportReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnprint_Click(object sender, EventArgs e)
    {
        ShowAdvanceApplyReport("Advance_Apply_Report", "Pay_Advance_Apply_Report.rpt");
    }
}