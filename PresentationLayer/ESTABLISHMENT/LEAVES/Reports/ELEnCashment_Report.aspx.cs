using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ESTABLISHMENT;

public partial class ESTABLISHMENT_LEAVES_Reports_ELEnCashment_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EnCashmentControllar objEnCash = new EnCashmentControllar();
    Leaves objLeaveMaster = new Leaves();


    #region Load
    protected void pre_init(object sender, EventArgs e)
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
                pnlAdd.Visible = true;
                //rbtnApprove.Checked = true;
                ViewState["action"] = "add";
                FillCollege();
            }

        }
    }

    #endregion

    #region Page Event
    protected void btnShowReport_Click(object sender, EventArgs e)
     {
         try
         {
             ShowReport("EL Encashment Report", "EncashmentStatusReport.rpt");
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
             {
                 objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_REPORT_ELEnCashment_Report ->" + ex.Message + "  " + ex.StackTrace);
             }
             else
             {
                 objUCommon.ShowError(Page, "Server UnAvailable");
             }
         }
     }

     protected void btnCancel_Click(object sender, EventArgs e)
     {
         ddlCollege.SelectedIndex = 0;
         txtFromDt.Text = string.Empty;
         txtToDt.Text = string.Empty;
         rdbType.SelectedValue = "A";
     }
     #endregion

     #region Load
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

     protected void FillCollege()
     {
         objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
     }

     private void ShowReport(string reportTitle, string rptFileName)
     {
         try
         {
           

             string start_date = "", end_date = "";


             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
             start_date = txtFromDt.Text.ToString().Trim();
             end_date = txtToDt.Text.ToString().Trim();
             if (txtFromDt.Text != string.Empty)
             {

                 start_date = Convert.ToDateTime(txtFromDt.Text).ToString("yyyy-MM-dd");
             }
             else
             {
                 start_date = "";
             }



             if (txtToDt.Text != string.Empty)
             {

                 end_date = Convert.ToDateTime(txtToDt.Text).ToString("yyyy-MM-dd");
             }
             else
             {
                 end_date = "";
             }
             
             //url += "&param=username=" + Session["username"].ToString() + ",@P_FSDATE=" + txtFromdt.Text.Trim() + ",@P_FEDATE=" + txtTodt.Text.Trim() + ",@P_DEPT_NO=" + Convert.ToInt32(ddlDepartment.SelectedValue); //+ ",FSDATE=" + Convert.ToDateTime(txtFromdt.Text.Trim()).ToString("dd/MMM/yyyy") + ",FEDATE=" + Convert.ToDateTime(txtTodt.Text.Trim()).ToString("dd/MMM/yyyy")
             // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FSDATE=" + Convert.ToDateTime(txtFromdt.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_FEDATE=" + Convert.ToDateTime(txtTodt.Text.Trim()).ToString("yyyy-MM-dd");
            // url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROMDT=" + Convert.ToDateTime(txtFromDt.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_TODT=" + Convert.ToDateTime(txtToDt.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_STATUS=" + Convert.ToChar(rdbType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
             url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROMDT=" + start_date + ",@P_TODT=" + end_date + ",@P_STATUS=" + Convert.ToChar(rdbType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            // url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_FROMDT=" + objLeaveMaster.FROMDT + ",@P_TODT=" + objLeaveMaster.TODT + ",@P_STATUS=" + Convert.ToChar(rdbType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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
     #endregion
}