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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

using System.Data.SqlClient;

public partial class ACCOUNT_ConfigurationForVoucherPrint : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountConfigurationController objACController = new AccountConfigurationController();
    AccountConfiguration objACEntity = new AccountConfiguration();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //if (Session["masterpage"] != null)
        //    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        //else
        //    objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        


            if (!Page.IsPostBack)
            {
                ViewState["action"] = "update";

                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    if (Session["comp_code"] == null || Session["fin_yr"] == null)
                    {
                        Session["comp_set"] = "NotSelected";
                        Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                    }
                    else
                    {
                        Session["comp_set"] = "";
                    }


                    //Page Authorization
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }


                    setValues();
                     
                }
            }
        
    }

    private void setValues()
    {
        DataSet ds = objCommon.FillDropDown("ACC_CONFIG", "*", "", "", "");
        txtFirst.Text = ds.Tables[0].Rows[0]["Signature1"].ToString();
        txtSecond.Text = ds.Tables[0].Rows[0]["Signature2"].ToString();
        txtThird.Text = ds.Tables[0].Rows[0]["Signature3"].ToString();
        txtFourth.Text = ds.Tables[0].Rows[0]["Signature4"].ToString();
        txtFifth.Text = ds.Tables[0].Rows[0]["Signature5"].ToString();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "AccountingVouchers")
            {
              if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
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
                    Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
            }

        }
    }

    private void clear()
    {
        txtFifth.Text = string.Empty;
        txtFirst.Text = string.Empty;
        txtFourth.Text = string.Empty;
        txtSecond.Text = string.Empty;
        txtThird.Text = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objACEntity.Signature1 = txtFirst.Text.Trim();
            objACEntity.Signature2 = txtSecond.Text.Trim();
            objACEntity.Signature3 = txtThird.Text.Trim();
            objACEntity.Signature4 = txtFourth.Text.Trim();
            objACEntity.Signature5 = txtFifth.Text.Trim();
            int ret = objACController.AddSignature(objACEntity);
            if (ret == 1)
            {
                objCommon.DisplayUserMessage(UPDMainGroup, "Record Saved Successfully", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ConfigurationForVoucherPrint.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
}
