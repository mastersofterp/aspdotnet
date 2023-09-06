//======================================================================================
// PROJECT NAME  : UAIMS                                                        
// MODULE NAME   : Leave Mgt.
// PAGE NAME     : AnnualIncrement.aspx                                    
// CREATION DATE : 21-APRIL-2015                                                  
// CREATED BY    : Swati Ghate                                                      
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;


using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class AnnualIncrementReport : System.Web.UI.Page
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
                //select COUNT(1) FROM PAYROLL_SB_SERVICEBK WHERE MONTH(DOI)=5 AND YEAR(DOI)=2015
                //lblIncrMonth.Text =str +" "+ sMonthName + "," + System.DateTime.Today.Year;
                int mon=Convert.ToDateTime(txtdate.Text).Month;
                int year=Convert.ToDateTime(txtdate.Text).Year;
                DataSet dsInfo = objCommon.FillDropDown("PAYROLL_SB_SERVICEBK", "*", "", "MONTH(DOI)=" + mon + " AND YEAR(DOI)=" + year + " ", "");
                if (dsInfo.Tables[0].Rows.Count > 0)
                {
                    ShowReport("Atendance", "ESTB_AnnualIncrement_Report.rpt");
                }
                else
                {
                    ShowMessage("Sorry! Record Not Found");
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string AttDate = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //string monyear = Convert.ToDateTime(txtdate.Text).ToString("MMMyyyy");
            DateTime dt = Convert.ToDateTime(txtdate.Text);

            int prevmonth = 0;
            int prevyear = 0;
            int currentmonth = dt.Month;
            int currentyear = dt.Year;
            prevyear = currentyear - 1;
            //if (currentmonth == 1)
            //{
            //    prevyear = currentyear - 1;
            //    prevmonth = 12;
            //}
            //else
            //{
            //    prevyear = currentyear;
            //    prevmonth = currentmonth - 1;
            //}

            int month = dt.Month;
            int year = dt.Year;
            //string frmdt = null;
            //if (month == 1)
            //{
            //    frmdt = "21" + "/" + "12" + "/" + prevyear.ToString();
            //}
            //else
            //{
            //    frmdt = "21" + "/" + prevmonth.ToString() + "/" + year.ToString();
            //}

            //frmdt = "01/07/" + prevyear.ToString() ;
            //string todt = "30/06/" + year.ToString();

            string frmdt = prevyear.ToString() + "-07-01";
            string todt = year.ToString()+"-06-30";
           

            string DOI = dt.ToString("yyyy-MM-dd");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_DATE=" + DOI + ",@P_FROM_DATE=" + frmdt + ",@P_TO_DATE=" + todt + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //txtdate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AnnualIncrementReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
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
