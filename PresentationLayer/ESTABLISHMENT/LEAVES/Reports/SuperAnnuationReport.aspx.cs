//======================================================================================
// PROJECT NAME  : UAIMS                                                        
// MODULE NAME   : Leave Mgt.
// PAGE NAME     : SuperAnnuationReport.aspx                                    
// CREATION DATE : 09-APRIL-2015                                                  
// CREATED BY    : Swati Ghate                                                      
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class SuperAnnuationReport: System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
  
   

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (!Page.IsPostBack)
        {
            
            txtdate.Text = DateTime.Now.ToString();
          

        }
       
    }
   
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtdate.Text == string.Empty)
            {
               ShowMessage("Please Select Date");
                return;
            }
            else
            {
                ShowReport("Atendance", "ESTB_SuperAnnuation_Report.rpt");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SuperAnnuationReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string AttDate = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            string monyear = Convert.ToDateTime(txtdate.Text).ToString("MMMyyyy");
         
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_DATE=" + AttDate + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //txtdate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SuperAnnuationReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtdate.Text = string.Empty;
       
    }


}
