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
public partial class ACCOUNT_store_vendor_mapping : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    storeIntegration objStore = new storeIntegration();
    storeIntegrationController objSIC = new storeIntegrationController();
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS"].ConnectionString;
  
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        { }
        else
            Response.Redirect("~/Default.aspx");

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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    objCommon.DisplayMessage("Select company/cash book.", this);
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                    //Fill dropdown list
                    // Filling Degrees

                    //Filling Recept list

                    getVendorName();
                    
                }
            }

            
        }

        divMsg.InnerHtml = string.Empty;
        
    }

    private void getVendorName()
    {
        DataSet dsVendorName = objSIC.getVendorName(_CCMS);
        GridData.DataSource = dsVendorName;
        GridData.DataBind();

        for (int i = 0; i<GridData.Rows.Count; i++)
        {
            DropDownList ddlleagerHead = GridData.Rows[i].FindControl("ddlleagerHead") as DropDownList;
            DropDownList ddlExpense = GridData.Rows[i].FindControl("ddlExpense") as DropDownList;
            objCommon.FillDropDownList(ddlleagerHead, "ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");
            objCommon.FillDropDownList(ddlExpense, "ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");

            HiddenField hdnPNO = GridData.Rows[i].FindControl("hdnPNO") as HiddenField;
            ddlleagerHead.SelectedValue = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_STORE_MAPPING", "PARTY_NO", "PNO=" + Convert.ToInt32(hdnPNO.Value));
            ddlExpense.SelectedValue = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_STORE_MAPPING", "PARTY_NO_EXPE", "PNO=" + Convert.ToInt32(hdnPNO.Value));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objSIC.DELETEFROMSTOREMAPPINGTABLE(Session["comp_code"].ToString());
        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            HiddenField hdnPNO = GridData.Rows[i].FindControl("hdnPNO") as HiddenField;
            DropDownList ddlleagerHead = GridData.Rows[i].FindControl("ddlleagerHead") as DropDownList;
            DropDownList ddlExpense = GridData.Rows[i].FindControl("ddlExpense") as DropDownList;
            Label lblvendor = GridData.Rows[i].FindControl("lblvendor") as Label;


            objStore.PNO = Convert.ToInt32(hdnPNO.Value);
            objStore.PNAME = lblvendor.Text;
            objStore.PARTY_NO = Convert.ToInt32(ddlleagerHead.SelectedValue);
            objStore.PARTY_NO_EXPE = Convert.ToInt32(ddlExpense.SelectedValue);
            int retVal = 0;
            retVal = objSIC.SetStoreMapping(objStore, Session["comp_code"].ToString());
            if (retVal == 1)
            {
                objCommon.DisplayUserMessage(UPDLedger, "Mapping Saved Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayUserMessage(UPDLedger, "Mapping Not Saved", this.Page);
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            DropDownList ddlleagerHead = GridData.Rows[i].FindControl("ddlleagerHead") as DropDownList;
            DropDownList ddlExpense = GridData.Rows[i].FindControl("ddlExpense") as DropDownList;
            ddlleagerHead.SelectedValue = "0";
            ddlExpense.SelectedValue = "0";
        }
    }
}
