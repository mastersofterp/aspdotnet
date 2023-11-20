using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class TRAININGANDPLACEMENT_Reports_TPCategory_Reort : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();

   
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
                // Check User Authority 
               // this.CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


            }
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

    protected void btnTPreport_Click(object sender, EventArgs e)
    {
        try
        {
            
                ShowReport("Categoy_report", "TP_CategoryReport.rpt");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            objCommon = new Common();

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;

            url += "&param=@P_CATEGORY="+Convert.ToInt32(ddlApplicationType.SelectedValue)+"";
            
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnTPCancel_Click(object sender, EventArgs e)
    {
        ddlApplicationType.SelectedIndex = 0;
    }

}