//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : RECEIPT PAYMENT REPORT                                                    
// CREATION DATE : 24-MAY-2010                                               
// CREATED BY    : ASHISH THAKRE                                                 
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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class FixedDeposite_Details : System.Web.UI.Page
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

        //if (hdnBal.Value != "")
        //{

        //    //lblCurBal.Text = hdnBal.Value;
        //    //lblmode.Text = hdnMode.Value;
        //}

        //Session["WithoutCashBank"] = "N";
        //btnGo.Attributes.Add("onClick", "return CheckFields();");
        if (!Page.IsPostBack)
        {
          //  SetDataColumn();


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
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }




                    objCommon.FillDropDownList(ddlFundingLedger, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "", "");

                    //ViewState["action"] = "add";
                }

               

            }


          //  SetFinancialYear();

        }



        divMsg.InnerHtml = string.Empty;
    }
   
  
   
  
    
 
    
    

   
    
    
   
   
  
    
    



    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtAcc.Text.ToString().Trim() != "")
        //{
        //    btnGo_Click(sender, e);
        //}
        txtUptoDate.Focus();
    }
    
    
  
    protected void btnRP_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtScheme.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Scheme", this);
                txtScheme.Focus();
                return;
            }
            if (txtFdrNo.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter FDR Number", this);
                txtFdrNo.Focus();
                return;

            }
            if (txtRoi.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Rate Of Interest", this);
                txtRoi.Focus();
                return;

            }
            if (txtFrmDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Date Of Deposit", this);
                txtFrmDate.Focus();
                return;
            }
            if (txtUptoDate.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Date Of Maturity", this);
                txtUptoDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 0)
            {
                objCommon.DisplayMessage(UPDLedger, "Date Of Deposit Can Not Be Same As Date Of Maturity. ", this);
                txtUptoDate.Focus();
                return;
            }
           
            if (DateTime.Compare(Convert.ToDateTime(txtFrmDate.Text), Convert.ToDateTime(txtUptoDate.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Date Of Deposit Can Not Be Greater Than Date Of Maturity. ", this);
                txtUptoDate.Focus();
                return;
            }

            if (txtamt.Text.ToString().Trim() == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Enter Amount ", this);
                txtamt.Focus();
                return;
            }
            //check for FDR.No
            DataSet dschk = new DataSet();

            dschk=objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_FixedDeposit_Details", "*", "", "FDR_No='" + txtFdrNo.Text + "'", "");
            if (dschk.Tables[0].Rows.Count > 0)
            {
                objCommon.DisplayMessage(UPDLedger, "F.D.R Noumber Allready Exists ", this);
                txtFdrNo.Focus();
                return;
            }
            AccountTransactionController acobj = new AccountTransactionController();
            FixedDepositeClass FdObj = new FixedDepositeClass();
            FdObj.PARTY_NO =ddlFundingLedger.SelectedValue.ToString();
            FdObj.SCHEME = txtScheme.Text;
            FdObj.FDR_NO = txtFdrNo.Text;
            FdObj.RateOfIntrest =Convert.ToDouble(txtRoi.Text);
           // Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")
            FdObj.Deposite_Date =Convert.ToDateTime(txtFrmDate.Text).ToString("dd-MMM-yyyy");
            FdObj.Maturity_Date = Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy");
            FdObj.Amount = Convert.ToDouble(txtamt.Text);
            string code = Session["comp_code"].ToString();
           // acobj.InsertFixedDepositeDetails(FdObj, code);
            int i = 0;
            i = acobj.InsertFixedDepositeDetails(FdObj, code);
            if (i == 1)
            {
                objCommon.DisplayMessage("Fixed Deposite Scheme Save Successfully........", this);
                txtamt.Text = string.Empty;
                txtFdrNo.Text = string.Empty;
                txtFrmDate.Text = string.Empty;
                txtRoi.Text = string.Empty;
                txtScheme.Text = string.Empty;
                txtUptoDate.Text = string.Empty;
                txtScheme.Focus();

                return;
            }

            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FixedDeposite_Details.btnRP_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }









    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtamt.Text = string.Empty;
        txtFdrNo.Text = string.Empty;
        txtFrmDate.Text = string.Empty;
        txtRoi.Text = string.Empty;
        txtScheme.Text = string.Empty;
        txtUptoDate.Text = string.Empty;
    }
}



