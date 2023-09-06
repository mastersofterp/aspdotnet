//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TO SELECT COMPNAY/CASH BOOK                                     
// CREATION DATE : 25-AUGUST-2009                                                  
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

public partial class Account_SetFinancialYr : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                // PopulateCompanyList();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


                //Session["comp_select_url"] = Request.Url.ToString();
            }
        }

        //if (lstCompany.Items.Count > 0)
        //{
        //    //lstCompany.Items[0].Selected = true;
        //    lstCompany.Focus();
        //}
        divMsg.InnerHtml = string.Empty;
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            string BookWritingDate = objCommon.LookUp("ACC_COMPANY", "cast(BOOKWRTDATE as nvarchar(20))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
            //if (Convert.ToDateTime(BookWritingDate).Year > Convert.ToInt32(txtFromDate.Text))
            //{

            //}
            //else
            //{
            FinCashBook objCashBookEntity = new FinCashBook();
            FinanceCashBookController objCashBookController = new FinanceCashBookController();
            objCashBookEntity.Company_Code = Session["comp_code"].ToString();
            objCashBookEntity.Company_FindDate_From = Convert.ToDateTime("01-Apr-" + txtFromDate.Text);
            objCashBookEntity.Company_FindDate_To = Convert.ToDateTime("31-Mar-" + txtToDate.Text);

            int Return = objCashBookController.UpdateCashBook(objCashBookEntity);
            if (Return > 0)
            {
                //Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);


                Session["fin_date_from"] = objCommon.LookUp("ACC_COMPANY", "COMPANY_FINDATE_FROM", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
                Session["fin_date_to"] = objCommon.LookUp("ACC_COMPANY", "COMPANY_FINDATE_TO", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
                Session["comp_name"] = objCommon.LookUp("ACC_COMPANY", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4)))", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");
                Session["fin_yr"] = objCommon.LookUp("ACC_COMPANY", "SUBSTRIng(cast(year(COMPANY_FINDATE_FROM) as nvarchar(5)),3,2)+SUBSTRIng(cast(year(COMPANY_FINDATE_TO) as nvarchar(5)),3,2)", "COMPANY_CODE='" + Session["comp_code"].ToString() + "'");

            }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_selectCompany.btnProceed_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region User Defined Methods
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }


    private void PopulateCompanyList()
    {
        try
        {
            DataSet dsCompany = objCommon.FillDropDown("ACC_COMPANY", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
            if (dsCompany.Tables.Count > 0)
            {
                //if (dsCompany.Tables[0].Rows.Count > 0)
                //{
                //    lstCompany.DataTextField = "COMPANY_NAME";
                //    lstCompany.DataValueField = "COMPANY_NO";
                //    lstCompany.DataSource = dsCompany.Tables[0];
                //    lstCompany.DataBind();
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_selectCompany.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion


    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        int Date = Convert.ToInt32(txtFromDate.Text);
        txtToDate.Text = (Date + 1).ToString();
    }
}
