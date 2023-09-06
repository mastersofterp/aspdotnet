//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Laboratory Test)     
// CREATION DATE : 16-APR-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_StockMaintenance_StockRegister : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LabMaster objLab = new LabMaster();
    LabController objLabController = new LabController();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlItem, "HEALTH_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
                    objCommon.FillDropDownList(ddlVendor, "HEALTH_PARTY", "PNO", "PCODE+ ' ' +PNAME", "", "PNAME");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_StockRegister.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }

    private void Clear()
    {
        txtFDate.Text = string.Empty;
        txtTDate.Text = string.Empty;
        ddlItem.SelectedIndex = 0;
        ddlVendor.SelectedIndex = 0;
    }

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                    txtFDate.Focus();
                    txtFDate.Text = string.Empty;
                    txtTDate.Text = string.Empty;
                    return;
                }
            }
            ShowReport("StockRegister", "rptStockRegister.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_StockRegister.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_ITEMNO=" + ddlItem.SelectedValue + ",@P_VENDOR=0"; // +ddlVendor.SelectedValue;           

            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                //url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
                url += ",@P_FDATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TDATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
            }
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_StockRegister.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnSummary_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFDate.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                    txtFDate.Focus();
                    txtFDate.Text = string.Empty;
                    txtTDate.Text = string.Empty;
                    return;
                }
            }
            //else
            //{
              
            //    objCommon.DisplayUserMessage(updActivity, "Please Enter From Date & To Date", this.Page);
            //    return;
            //}
            ShowSummaryReport("StockSummaryReport", "rptStockSummaryReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_StockMaintenance_StockRegister.btnSummary_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowSummaryReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            // url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");
            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                //url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
                url += "&param=@P_FROM_DATE=" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd");

            }
            else
            {
                url += "&param=@P_FROM_DATE=null,@P_TO_DATE=null";
            }


            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_StockRegister.ShowSummaryReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }





}