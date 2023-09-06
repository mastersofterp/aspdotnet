//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TO SELECT COMPNAY/CASH BOOK                                     
// CREATION DATE : 25-AUGUST-2009                                                  
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Account_selectCompany : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
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
                CheckPageAuthorization();

                PopulateCompanyList();
                Page.Title = Session["coll_name"].ToString();



                //Session["comp_select_url"] = Request.Url.ToString();
            }
        }

        if (lstCompany.Items.Count > 0)
        {
            //lstCompany.Items[0].Selected = true;
            lstCompany.Focus();
        }
        divMsg.InnerHtml = string.Empty;
    }

    protected void btnProceed_Click(object sender, EventArgs e)    
    {
        try
        {
            if (lstCompany.SelectedItem == null)
            {
                objCommon.DisplayMessage("Select Company/Cash Book.", this);
                return;

            }
            string id = Request.Form[lstCompany.UniqueID].ToString();
            string strID = lstCompany.SelectedValue;
            string strItem = lstCompany.SelectedItem.ToString();


            if (lstCompany.SelectedIndex >= 0)
            {
                Session["comp_no"] = lstCompany.SelectedItem.Value;
                Session["comp_name"] = lstCompany.SelectedItem.Text;



                //DataSet dsCompany = objCommon.FillDropDown("SysObjects", "name", "xtype", "xtype='U' and name='"+  Session["comp_name"].ToString().Trim() +"'", "name");
                //if (dsCompany.Tables[0].Rows.Count == 0)
                //{
                //    objCommon.DisplayMessage("Company/Cash book not found,Create again.", this);
                //    return;

                //}


                FinCashBookController objCBC = new FinCashBookController();
                DataTableReader dtr = objCBC.GetCashBookByCompanyNo(lstCompany.SelectedItem.Value);
                if (dtr.Read())
                {
                    Session["comp_code"] = dtr["COMPANY_CODE"];
                    Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
                    Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
                    Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
                }
                dtr.Close();
                // TrialBalanceReportController o = new TrialBalanceReportController();
                //o.ResetBalanceSheet();
                ShowMessage("Company selected successfully !!!");
                //objCommon.DisplayMessage("Company selected successfully !!!", this);

                //Response.Redirect("~/home.aspx", true);

                //if (Request.QueryString["Page"] != null)
                //{
                //    Response.Redirect("~/ACCOUNTS/" + Request.QueryString["Page"].ToString(), false);
                //}

            }
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

    //public void ShowMessage(string msg)
    //{
    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    //}

    public void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    //private void ShowMessage(string message)
    //{
    //    if (message != string.Empty)
    //    {
    //        //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('my message');", true);
    //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Startup", "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>", true); 
    //        divMsg.InnerHtml += "<script type='text/javascript'> alert('" + message + "'); </script>";
    //        //ScriptManager.RegisterStartupScript(updSElCompany, this.GetType(), updSElCompany.UniqueID, "alert('" + message + "');", true);
    //        //ScriptManager.RegisterClientScriptBlock(updSElCompany, this.GetType(), "alert", "alert('" + message + "');", true);
    //        //Response.Write("<script> alert('" + message + "'); </script>");
    //    }
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/selectCompany.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=Select Company");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=Select Company");
                }

            }

        }
        else
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/selectCompany.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Select Company");
            }
            //else
            //{
            //    //Even if PageNo is Null then, don't show the page
            //    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            //}
        }
    }

    private void PopulateCompanyList()
    {
        try
        {
            DataSet dsCompany = objCommon.FillDropDown("ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + Session["userno"].ToString() + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
            if (dsCompany.Tables.Count > 0)
            {
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    lstCompany.DataTextField = "COMPANY_NAME";
                    lstCompany.DataValueField = "COMPANY_NO";
                    lstCompany.DataSource = dsCompany.Tables[0];
                    lstCompany.DataBind();
                }
            }
            else { ShowMessage("You have not assigned any company. Kindly contact admin to assign the company."); }
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


}
