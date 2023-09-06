//=====================================================================
//CREATED BY: Swati Ghate
//CREATED DATE:22-12-2015
//PURPOSE: TO DISPLAY POSTING DETAIL REPORT
//=====================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class PostingReport : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = true;
            
                pnlbutton.Visible = true;

               
               
             }

        }
    }
   
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //SELECT * FROM PAYROLL_SB_POSTING  WHERE FROMDATE BETWEEN @P_FROMDATE AND @P_TODATE OR  TODATE BETWEEN @P_FROMDATE AND @P_TODATE
            string frmdt = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
            string todt = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");
            int year_status = 0;
            if (chkThreeYear.Checked == false)
            {
                DataSet ds = objCommon.FillDropDown("PAYROLL_SB_POSTING", "*", "", "FROMDATE BETWEEN '" + frmdt + "' and  '" + todt + "' OR TODATE BETWEEN '" + frmdt + "' and  '" + todt + "'", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("List", "PostingReport.rpt", frmdt, todt);
                }
                else
                {
                    MessageBox("Record Not Found!");
                    return;
                }

            }
            else
            {
                ShowReport("List", "PostingReport_Year.rpt", frmdt, todt);
            }
           
           
           
               
            
           
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Reports_EmployeeListReport.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName,string frmdt,string todt)
    {
        try
        {
            string Script = string.Empty;
            //string Status = "N";
            
          
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;

            url += "&param=@P_FROMDATE=" + frmdt + ",@P_TODATE=" + todt + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() ;
           
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void Clear()
    {
        txtFromDate.Text = txtToDate.Text = string.Empty;
        chkThreeYear.Checked = false;
    }    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }     
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
    
}