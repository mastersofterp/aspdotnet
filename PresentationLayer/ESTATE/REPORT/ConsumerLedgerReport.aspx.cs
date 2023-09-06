//==================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : ESATE
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 03-NOV-2017
// DESCRIPTION   : CONSUMER LEDGER BILL REPORT.
//==================================================
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ESTATE_Report_ConsumerLedgerReport : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                }

                divMsg.InnerHtml = string.Empty;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd");
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updQuarterReport, this.updQuarterReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtFromdate.Text = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtFromdate.Text != string.Empty)
        {
            //ShowReport("Abstract Bill Report", "rptAbstractBill.rpt");
            ShowReport("Consumer Ledger Report", "rptConsumerLedgerReport.rpt");
        }
        else
        {
            // objCommon.DisplayMessage("Please Select Quarter Type and Month.", this.Page);
            objCommon.DisplayMessage(this.updQuarterReport, "Please Select Month.", this.Page);
        }
    }
}