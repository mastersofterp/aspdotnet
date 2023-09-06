//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Requisition.aspx                                                  
// CREATION DATE : 14-July-2018                                                        
// CREATED BY    : Mrunal Singh                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Transactions_Quotation_Str_Requisition_Track : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
 
    StoreMasterController objApp = new StoreMasterController();

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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    
                }
                BindRequisitionList(Convert.ToInt32(Session["strdeptcode"]));               
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

    public void BindRequisitionList(int SDNO)
    {
        try
        {
            DataSet ds = null;
            ds = objApp.GetRequisitionStatusList(SDNO, Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRequisition.DataSource = ds;
                lvRequisition.DataBind();
            }
            else
            {
                lvRequisition.DataSource = null;
                lvRequisition.DataBind();
            }
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "IITMS.NITPRM->PesentationLayer->STORES_Transactions_Quotation_Str_Requisition_Track->BindRequisitionList" + ex.Message);

        } 
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Requisition Track Report", "Str_Requisition_TrackReport.rpt");
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "IITMS.NITPRM->PesentationLayer->STORES_Transactions_Quotation_Str_Requisition_Track->btnReport_Click" + ex.Message);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_SDNO=" + Convert.ToInt32(Session["strdeptcode"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UANO=" + Convert.ToInt32(Session["userno"]);
           
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_Requisition_Track.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}