using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.IO;

public partial class ACCOUNT_DeleteStoreInvoice : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    
    DeleteStoreInvoiceJournalController objPC1 = new DeleteStoreInvoiceJournalController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    //BindInvoice();
                }
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
                Response.Redirect("~/notauthorized.aspx?page=AccountingVouchersModifications.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AccountingVouchersModifications.aspx");
        }
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMergeLedger(string prefixText)
    {
        List<string> Vendor = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            // AutoCompleteController objAutocomplete = new AutoCompleteController();
            DeleteStoreInvoiceJournalController objAutocomplete = new DeleteStoreInvoiceJournalController();
            ds = objAutocomplete.GetMergeData(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Vendor.Add(ds.Tables[0].Rows[i]["VENDOR_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Vendor;
    }

    

    public void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (txtAcc.Text.Split('*').Length > 1)
        {           
            BindInvoice(Convert.ToInt32(txtAcc.Text.Split('*')[1].ToString()));
        }
        else
        {
            ShowMessage("Please Select Proper Vendor Name");
            
        }
    }
    protected void lnkselect_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    pnl.Visible = false;
        //    LinkButton lnkbutton = sender as LinkButton;

        //    int tranno = int.Parse(lnkbutton.CommandName);
        //    string VoucherNo = lnkbutton.CommandArgument;
        //    string compcode = lnkbutton.ToolTip;

        //    int uano = int.Parse(Session["userno"].ToString());
        //    CustomStatus cs = (CustomStatus)objPC1.DeleteTransactionInvoiceStore(tranno, compcode, VoucherNo, "", "J", uano);

        //    if (cs.Equals(CustomStatus.RecordSaved))
        //    {
        //        ShowMessage("Record Deleted Successfully");
        //    }
        //    else if (cs.Equals(CustomStatus.TransactionFailed))
        //    {
        //        ShowMessage("Transaction Failed");
        //    }
        //    else
        //    {
        //        ShowMessage("Something Went Wrong");
        //    }
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }
    protected void BindInvoice(int pno)
    {
        try
        {
             DataSet ds = objPC1.GetInvoiceDetails(pno);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 GridInvoice.DataSource = ds;
                 GridInvoice.DataBind();
             }
             else
             {
                 GridInvoice.DataSource = null;
                 GridInvoice.DataBind();
                 ShowMessage("No Record Found");
             }      
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAcc.Text = "";
        txtAcc.Focus();
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            //pnl.Visible = false;
            //LinkButton lnkbutton = sender as LinkButton;
            Button btndel = sender as Button;
            int tranno = int.Parse(btndel.CommandName);
            string VoucherNo = btndel.CommandArgument;
            string compcode = btndel.ToolTip;
            int uano = int.Parse(Session["userno"].ToString());

            CustomStatus cs1 = (CustomStatus)objPC1.GetInvoicePaymentDetails(tranno);

            if (cs1.Equals(CustomStatus.RecordNotFound))
            {
                CustomStatus cs = (CustomStatus)objPC1.DeleteTransactionInvoiceStore(tranno, compcode, VoucherNo, "J","", uano);

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ShowMessage("Record Deleted Successfully");
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    ShowMessage("Transaction Failed");
                }
                else
                {
                    ShowMessage("Something Went Wrong");
                }

                if (txtAcc.Text.Split('*').Length > 1)
                {
                    BindInvoice(Convert.ToInt32(txtAcc.Text.Split('*')[1].ToString()));
                }
                else
                {
                    ShowMessage("Please Select Proper Vendor Name");
                }
            }
            else
            {
                ShowMessage("This journal can not be deleted as already payment is done against this journal. First delete payment entry.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}