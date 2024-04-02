using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.Globalization;
using IITMS.SQLServer.SQLDAL;

public partial class ResetFinancialYear : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                //CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
        }
        //blank div tag
        divMsg.InnerHtml = string.Empty;
    }
    //This method id for reset counter reff table
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Reset_financial_year_and_reff_startdatenddate
        DailyFeeCollectionController objfees = new DailyFeeCollectionController();

        try
        {
            string result = string.Empty;
            string dtFrom = txtDateFrom.Text;
            string dtTo = txtDateTo.Text;
            int FromYear = 0;

             DateTime fromDate = Convert.ToDateTime(txtDateFrom.Text);
             FromYear = Convert.ToDateTime(txtDateFrom.Text).Year;

             int Frommonth = Convert.ToDateTime(txtDateFrom.Text).Month;
             int Fromday = Convert.ToDateTime(txtDateFrom.Text).Day;


             if (Frommonth > 3 && Fromday <= 31)
             {
                 FromYear++;
             }

             string financialDate = "31/03/" + FromYear;

             DateTime toDate = Convert.ToDateTime(txtDateTo.Text);

             if (toDate > Convert.ToDateTime(financialDate))
             {
                 objCommon.DisplayMessage(this.Page, "To date should be less than or equal to financial year :" + financialDate, this.Page);
                 return;
             }



             result = objfees.Reset_financial_year_and_reff_startdatenddate(dtFrom, dtTo);

            if (result == "1")
            {
                objCommon.DisplayMessage(updFY,"Record Updated Succesfully", this.Page);
                //ClearControl();
            }
             

        }
         catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ResetFinancialYear.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
   
}