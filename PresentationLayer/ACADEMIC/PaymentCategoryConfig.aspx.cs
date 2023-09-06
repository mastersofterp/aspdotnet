//======================================================================================
// PROJECT NAME  : RFC COMMON
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PAYMENT CATEGORY CONFIGURATION                         
// CREATION DATE : 25/02/2022
// ADDED BY      : RISHABH BAJIRAO
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_PaymentCategoryConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController objFee = new FeeCollectionController();


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
                ////Page Authorization
                //this.CheckPageAuthorization();
                FillDropDownList();
                BindListView();
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
                Response.Redirect("~/notauthorized.aspx?page=PaymentCategoryConfig.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaymentCategoryConfig.aspx");
        }
    }

    private void FillDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0", "");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds =  objFee.GetPaymentCategoryConfigData(0);
            lvPaymentCategory.DataSource = ds;
            lvPaymentCategory.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearAll()
    {
        ddlReceiptType.SelectedIndex = 0;
        ddlFeeTitle.Items.Clear();
        ddlPaymentType.SelectedIndex = 0;
    }


    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "");
                objCommon.FillListBox(ddlFeeTitle, "ACD_FEE_TITLE", "FEE_HEAD", "FEE_LONGNAME", "RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND FEE_LONGNAME<>''", string.Empty);
                ddlPaymentType.Focus();
            }
            else
            {
                ddlPaymentType.Items.Clear();
                ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
                ddlFeeTitle.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ReceiptType = string.Empty;
        int PaymentTypno = 0;
        string Fee = string.Empty;
        try
        {
            ReceiptType = ddlReceiptType.SelectedValue;
            PaymentTypno = Convert.ToInt32(ddlPaymentType.SelectedValue);

            foreach (ListItem items in ddlFeeTitle.Items)
            {
                if (items.Selected == true)
                {
                    Fee += items.Value + '$';
                }
            }

            if (Fee.Length > 1)
            {
                Fee = Fee.Remove(Fee.Length - 1);
            }

            CustomStatus cs = (CustomStatus)objFee.SavePaymentCategoryConfig(ReceiptType, PaymentTypno, Fee);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updPaymentCatg, "Record Saved Successfully!", this.Page);
                BindListView();
                ClearAll();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updPaymentCatg, "Server Unavailable!", this.Page);
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int pcc_no = int.Parse(btnEdit.CommandArgument);
            ShowUserDetails(pcc_no);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowUserDetails(int pcc_no)
    {
        try
        {
            DataSet ds = objFee.GetPaymentCategoryConfigData(pcc_no);

            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["PCC_NO"] = pcc_no.ToString();
                    FillDropDownList();
                    ddlReceiptType.SelectedValue = ds.Tables[0].Rows[0]["RECIEPT_CODE"] == null ? string.Empty : ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                    objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "");
                    ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["PTYPENO"] == null ? string.Empty : ds.Tables[0].Rows[0]["PTYPENO"].ToString();
                    objCommon.FillListBox(ddlFeeTitle, "ACD_FEE_TITLE", "FEE_HEAD", "FEE_LONGNAME", "RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND FEE_LONGNAME<>''", string.Empty);
                    ddlFeeTitle.SelectedValue = ds.Tables[0].Rows[0]["FEEHEAD"] == null ? string.Empty : ds.Tables[0].Rows[0]["FEEHEAD"].ToString();
                    ddlReceiptType.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}