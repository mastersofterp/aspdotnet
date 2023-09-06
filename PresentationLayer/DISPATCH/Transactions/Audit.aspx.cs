//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : DISPATCH
// PAGE NAME     : AUDIT.ASPX.CS
// CREATION DATE : 20 APRIL 2011
// CREATED BY    : PRAKASH RADHWANI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class DISPATCH_Transactions_Audit : System.Web.UI.Page
{

    /// <summary>
    /// CREATING OBJECT OF COMMON CLASS,UAIMS_COMMON AND VMCONTROLLER
    /// </summary>
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IOTranController objIOtranc = new IOTranController();
    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["userno"],Session["username"],
    /// Session["usertype"]. If the user has not Login, he will be redirected to "default.aspx" page.
    /// The Page_Load  event calls the CheckPageAuthorization() Method.The CheckPageAuthorization() method checks whether the user is authorised to access this Page
    /// If he is not authorised, user will be redirected to "notauthorized.aspx" page.
    /// The Page_Load  calls the BindListView() method.This method fills the listview with the existing Book title records.
    /// State is maintained using viewstate."add" is stored in the viewstate. ViewState["action"] = "add".
    /// </summary>
    /// 
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
                    if (ViewState["action"] == null)
                    {
                        ViewState["action"] = "add";
                    }

                    BindListView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Audit.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Audit.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Audit.aspx");
        }
    }

    // This event call the setmasterpage() method
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                //checking viewstate status. If it is edit then records will be updated else it will be added
                if (ViewState["action"].ToString().Equals("add"))
                {
                  
                    CustomStatus cs = (CustomStatus)objIOtranc.AddAudit(Convert.ToInt32(txtAmt.Text.Trim()), Convert.ToDateTime(txtAmtDt.Text.Trim()));
                    if (Convert.ToInt32(cs) != -99)
                    {
                        clear();
                        objCommon.DisplayMessage(updActivity,"Record Saved Succesfully", this.Page);
                    }

                }
                else
                {
                    if (ViewState["AU_NO"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objIOtranc.UpdAudit(Convert.ToInt32(txtAmt.Text.Trim()), Convert.ToDateTime(txtAmtDt.Text.Trim()), Convert.ToInt32(ViewState["AU_NO"].ToString()));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            clear();
                            objCommon.DisplayMessage(updActivity,"Record Update Succesfully", this.Page);
                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_Audit.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    /// <summary>
    /// Used to edit the records which are already added
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            int AU_NO = int.Parse(btnEdit.CommandArgument);
            ViewState["AU_NO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            int loc = Convert.ToInt32(objCommon.LookUp("IO_AUDIT", "LOCK", "AU_NO=" + AU_NO));
            if (loc.Equals("1"))
            {
                objCommon.DisplayMessage("Please contact to Admin to unlock this record", this.Page);
                return;
            }
            else
            {
                this.ShowDetails(AU_NO);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int au_no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("IO_AUDIT", "*", "", "AU_NO=" + Convert.ToInt32(au_no), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAmt.Text = ds.Tables[0].Rows[0]["AMT_ACC"].ToString();
                txtAmtDt.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Transactions_IO_OutwardDispatch.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("IO_AUDIT", "(CASE LOCK WHEN 1 THEN 'LOCK' ELSE 'UNLOCK' END) AS STATUS ", "*", "AU_NO>0 and AMT_ACC IS NOT NULL", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAudit.DataSource = ds;
                lvAudit.DataBind();
            }
            else
            { 
                lvAudit.DataSource = null;
                lvAudit.DataBind();
            }

        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    private void clear()
    {
        txtAmt.Text = string.Empty;
        txtAmtDt.Text = string.Empty;
        BindListView();
        ViewState["action"] = null;
    }
}
