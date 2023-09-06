//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : Cost Center                                                     
// CREATION DATE : 22-July-2014                                               
// CREATED BY    : Nitin Meshram
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Collections.Generic;
using IITMS.NITPRM;


public partial class ACCOUNT_CostCenterMaster : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenter objCostCenter=new CostCenter();
    CostCenterController objCostCenterController=new CostCenterController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");

            }
            else
            {
                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack)
        {
            
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                //  Response.Redirect("~/default.aspx");
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
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    
                    populateCostCenter();
                    
                    ViewState["action"] = "add";
                    ViewState["CCHead"] = "add";
                }
            }
        }
    }

    protected void populateCostCenter()
    {
        DataSet dsCostCenter = objCommon.FillDropDown("Acc_" + Session["comp_code"] + "_CostCenter ACC inner join Acc_" + Session["comp_code"] + "_CostCategory ACAT on (ACC.Cat_ID=ACAT.Cat_ID)", "ACC.CC_ID", "ACC.CCNAME + ' ( '+ACAT.Category+' ) ' as CCNAME", "", "ACC.CC_ID");
        lstCostCenter.DataTextField = "CCNAME";
        lstCostCenter.DataValueField = "CC_ID";
        lstCostCenter.DataSource = dsCostCenter;
        lstCostCenter.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int retVal=0;
        if (ViewState["CCHead"].ToString() == "add")
        {
            if (ViewState["action"].ToString() == "add")
            {

                string IsAvailable = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCategory", "Category", "Category='" + txtCostCategory.Text + "'");
                if (IsAvailable != string.Empty)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Category Already Defined", this.Page);
                    return;
                }
                objCostCenter.CATID = 0;
                objCostCenter.CATEGORYNAME = txtCostCategory.Text;
                retVal = objCostCenterController.CostCategoryAddUpdate(objCostCenter, Session["comp_code"].ToString());
                if (retVal == 1)
                {
                   
                    string Cat_ID = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCategory", "Cat_ID", "Category='" + txtCostCategory.Text + "'");
                    objCostCenter.CATID = Convert.ToInt32(Cat_ID);

                    string IsAvailableCostCenter = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCenter", "CC_ID", "CCNAME='" + txtCCHead.Text + "' AND Cat_ID=" + Convert.ToInt32(Cat_ID));
                    if (IsAvailableCostCenter != string.Empty)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Cost Center Head Already Defined", this.Page);
                        return;
                    }
                    objCostCenter.CC_ID = 0;
                    objCostCenter.CCNAME = txtCCHead.Text;
                    retVal = objCostCenterController.CostCenterAddUpdate(objCostCenter, Session["comp_code"].ToString());
                    if (retVal == 1)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Record Save Successfully", this.Page);
                        Clear();
                        populateCostCenter();
                    }
                }
            }
            else
            {
                string[] catID = txtCostCategory.Text.Trim().Split('*');
                objCostCenter.CATID = Convert.ToInt32(catID[0]);
                string IsAvailable = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCategory", "Category", "Category='" + catID[1].ToString() + "'");
                if (IsAvailable == string.Empty)
                {
                    objCostCenter.CATEGORYNAME = catID[1].ToString();
                    retVal = objCostCenterController.CostCategoryAddUpdate(objCostCenter, Session["comp_code"].ToString());
                }
                if (retVal == 1 || IsAvailable != string.Empty)
                {
                    string Cat_ID = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCategory", "Cat_ID", "Category='" + catID[1].ToString() + "'");
                    objCostCenter.CATID = Convert.ToInt32(Cat_ID);
                    string IsAvailableCostCenter = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCenter", "CC_ID", "CCNAME='" + txtCCHead.Text + "'AND Cat_ID=" + Convert.ToInt32(Cat_ID));
                    if (IsAvailableCostCenter != string.Empty)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Cost Center Head Already Defined", this.Page);
                        return;
                    }
                 
                    objCostCenter.CC_ID = 0;
                    objCostCenter.CCNAME = txtCCHead.Text;
                    retVal = objCostCenterController.CostCenterAddUpdate(objCostCenter, Session["comp_code"].ToString());
                    if (retVal == 1)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Record Save Successfully", this.Page);
                        Clear();
                        populateCostCenter();
                    }
                }
            }
        }
        else
        {
            string[] catID = txtCostCategory.Text.Trim().Split('*');
            string IsAvailable = string.Empty;
            if (catID.Length > 1)
            {
                objCostCenter.CATID = Convert.ToInt32(catID[0]);
                IsAvailable = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCategory", "Category", "Category='" + catID[1].ToString() + "'");
                
                if (IsAvailable == string.Empty)
                {
                    objCostCenter.CATEGORYNAME = catID[1].ToString();
                    retVal = objCostCenterController.CostCategoryAddUpdate(objCostCenter, Session["comp_code"].ToString());
                }
            }
            else
            {
                objCostCenter.CATID = 0;
                objCostCenter.CATEGORYNAME = txtCostCategory.Text;
                retVal = objCostCenterController.CostCategoryAddUpdate(objCostCenter, Session["comp_code"].ToString());
            }
                           
               
            if (retVal == 1 || IsAvailable != string.Empty)
            {
                //string IsAvailableCostCenter = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCenter", "CC_ID", "CCNAME='" + txtCCHead.Text + "'");
                //if (IsAvailableCostCenter != string.Empty)
                //{
                //    objCommon.DisplayUserMessage(UPDLedger, "Cost Center Head Already Defined", this.Page);
                //    return;
                //}
                if (catID.Length < 2)
                {
                    string Cat_ID = objCommon.LookUp("Acc_" + Session["comp_code"] + "_CostCategory", "Cat_ID", "Category='" + txtCostCategory.Text + "'");
                    objCostCenter.CATID = Convert.ToInt32(Cat_ID);
                }

                objCostCenter.CC_ID = Convert.ToInt32(ViewState["CCID"].ToString());
                objCostCenter.CCNAME = txtCCHead.Text;
                retVal = objCostCenterController.CostCenterAddUpdate(objCostCenter, Session["comp_code"].ToString());
                if (retVal == 1)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Record Update Successfully", this.Page);
                    Clear();
                    populateCostCenter();
                }
            }
        }
    }

    protected void txtCostCategory_TextChanged(object sender, EventArgs e)
    {
        string[] Cat_ID = txtCostCategory.Text.Trim().Split('*');
        if (Cat_ID.Length > 1)
        {
            ViewState["action"] = "Edit";
        }
    }

    protected void Clear()
    {
        txtCostCategory.Text = string.Empty;
        txtCCHead.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["CCHead"] = "add";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsData = objCommon.FillDropDown("Acc_" + Session["comp_code"] + "_CostCenter CC inner join Acc_" + Session["comp_code"] + "_CostCategory CCat on (cc.Cat_ID=CCat.Cat_ID)", "CC.CC_ID,CC.CCNAME", "CCat.Category,cc.Cat_ID", "CC_ID=" + lstCostCenter.SelectedValue, "");
        txtCostCategory.Text = dsData.Tables[0].Rows[0]["Cat_ID"].ToString() + "*" + dsData.Tables[0].Rows[0]["Category"].ToString();
        txtCCHead.Text = dsData.Tables[0].Rows[0]["CCNAME"].ToString();
        ViewState["CCHead"] = "edit";
        ViewState["CCID"] = dsData.Tables[0].Rows[0]["CC_ID"].ToString();
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

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCostCategoryData(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetCostCategory(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["Category"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

}
