//=====================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ESTATE                           
// CREATION DATE : 25-SEP-2017                                                           
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//===================================================================== 

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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Report_EmpMonthlyEnergyBill : System.Web.UI.Page
{
    Common objCommon = new Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                ViewState["IDNO"] = "0";
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtselectdate.Text = string.Empty;
        //txtSearch.Text = string.Empty;
        //hfInvNo.Value = null;
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
           
            ViewState["IDNO"] = Convert.ToInt32(Session["idno"]);
                ShowReport("Monthly_Energy_billing", "rptEmployeeEnergyCalculation.rpt");
          
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
            if (!string.IsNullOrEmpty(txtselectdate.Text))
            {              
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,ESTATE," + rptFileName;
              
                url += "&param=@p_college_code=" + Session["colcode"].ToString() + ",@p_Date=" + Convert.ToDateTime(txtselectdate.Text).ToString("yyyy-MM-dd") + ",@P_EMPID=" + Convert.ToInt16(ViewState["IDNO"]);

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updReport, "Please Select Bill Month.", this.Page);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }




    protected void imgbtnclearname_Click(object sender, ImageClickEventArgs e)
    {
        txtselectdate.Text = string.Empty;

    }
}