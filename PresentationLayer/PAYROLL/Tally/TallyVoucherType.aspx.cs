//=================================================================================
// PROJECT NAME  :                                                      
// MODULE NAME   :                                   
// CREATION DATE :                                                  
// CREATED BY    :                                              
// MODIFIED BY   :                                                                 
// MODIFIED DESC :                                                                 
//===================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;

public partial class ACADEMIC_PublicationDetail : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    Con_TallyConfig ObjTC = new Con_TallyConfig();
    Ent_TallyConfig ObjTCM = new Ent_TallyConfig();
   
    string Message = string.Empty;
    string UsrStatus = string.Empty;
    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    BindTallyVoucher();

                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title

                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                }
            }
            divMsg.InnerHtml = "";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_TallyVoucherType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentLedgerReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLedgerReport.aspx");
        }
    }
    #endregion

    public void BindTallyVoucher()
    {
        try
        {
            ObjTCM.CollegeId = Convert.ToInt32(Session["colcode"]);
            ObjTCM.CommandType = "BindTallyVoucher";
            DataSet ds = ObjTC.GetAllTallyVoucherDetails(ObjTCM);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Select("TallyVoucherType='Cash'"))
                    {
                        txtCashVName.Text = Convert.ToString(dr["TallyVoucherName"]);
                    }
                    foreach (DataRow dr in ds.Tables[0].Select("TallyVoucherType='Bank'"))
                    {
                        txtBankVName.Text = Convert.ToString(dr["TallyVoucherName"]);
                        ViewState["TallyVoucherTypeId"] = Convert.ToInt32(dr["TallyVoucherTypeId"]);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_TallyVoucherType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
   private void Clear()
        {
            try
            {
                txtBankVName.Text = string.Empty;
                txtCashVName.Text = string.Empty;
                btnSubmit.Text = "Submit";
                btnSubmit.ToolTip = "Click To Submit";
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "ACADEMIC_TallyVoucherType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                long res = 0;
                ObjTCM.CashTallyVoucherName = txtCashVName.Text.Trim();
                ObjTCM.BankTallyVoucherName = txtBankVName.Text.Trim();
                ObjTCM.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
                ObjTCM.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
                ObjTCM.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
                ObjTCM.IPAddress = Convert.ToString(Session["ipAddress"]);
                ObjTCM.MACAddress = Convert.ToString("0");
                ObjTCM.CollegeId = Convert.ToInt32(Session["colcode"].ToString());

                res = ObjTC.AddUpdateTallyVoucher(ObjTCM, ref Message);
               
                if (res == -99)
                {
                    objCommon.DisplayMessage("Exception Occure", this.Page);

                    return;

                }
                if (res == 1)
                {
                    objCommon.DisplayMessage("Record Save Successfully", this.Page);
                    BindTallyVoucher();
                    return;

                }
                if (res ==2)
                {
                    objCommon.DisplayMessage("Record Updated Successfully", this.Page);
                    BindTallyVoucher();
                    return;

                }
            }
            catch (System.Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "ACADEMIC_TallyVoucherType.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }  

 