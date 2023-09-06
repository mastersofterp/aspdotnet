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

public partial class ESTATE_Report_QuareterAllotmentReport : System.Web.UI.Page
{

    Common objcomm = new Common();
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

    private void UnOccupiedQuarterReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

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

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            if (txtFromdate.Text.Trim() != string.Empty && txtTodate.Text.Trim() != string.Empty)
            {
                url += ",@P_FROMDATE=" + Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FROMDATE=null,@P_TODATE=null";
            }

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
        txtTodate.Text = string.Empty;
    }


    protected void btnQrtAllotment_Click(object sender, EventArgs e)
    {
        if (!txtFromdate.Text.Equals(string.Empty))
        {
            if (DateTime.Compare(Convert.ToDateTime(txtFromdate.Text), Convert.ToDateTime(txtTodate.Text)) == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                txtFromdate.Focus();
                return;
            }
        }
        //if (!string.IsNullOrEmpty(txtFromdate.Text) && !string.IsNullOrEmpty(txtTodate.Text))
        //{
            ShowReport("QuarterAllotment", "rptQuarterAllotment.rpt");
        //}
        //else
        //{
        //    objcomm.DisplayMessage(this.updQuarterReport, "Please Select From Date And To date", this.Page);
        //}
    }
    protected void btnQrtVacant_Click(object sender, EventArgs e)
    {
        if (!txtFromdate.Text.Equals(string.Empty))
        {
            if (DateTime.Compare(Convert.ToDateTime(txtFromdate.Text), Convert.ToDateTime(txtTodate.Text)) == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                txtFromdate.Focus();
                return;
            }
        }
        //if (!string.IsNullOrEmpty(txtFromdate.Text) && !string.IsNullOrEmpty(txtTodate.Text))
        //{
            ShowReport("QuarterVacant", "rptQuarterVacant.rpt");
        //}
        //else
        //{
        //    objcomm.DisplayMessage(this.updQuarterReport, "Please Select From Date And TO date", this.Page);
        //}
    }  
    protected void btnUnoccupied_Click(object sender, EventArgs e)
    {
        if (!txtFromdate.Text.Equals(string.Empty))
        {
            if (DateTime.Compare(Convert.ToDateTime(txtFromdate.Text), Convert.ToDateTime(txtTodate.Text)) == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                txtFromdate.Focus();
                return;
            }
        }
        UnOccupiedQuarterReport("Un Occupied Quarter", "rptQuarterList.rpt");
    }
}
