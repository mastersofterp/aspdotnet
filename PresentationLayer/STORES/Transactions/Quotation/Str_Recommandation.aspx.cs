//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Recommandation.aspx                                      
// CREATION DATE : 19-march-2010                                                    
// CREATED BY    : chaitanya Bhure                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
public partial class Stores_Transactions_Str_Recommandation : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Recommandaion_Controller ObjStrRecom = new Str_Recommandaion_Controller();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //int UANO = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENTUSER", "UA_NO", "APLNO=3 AND MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString())));
                //if (Convert.ToInt32(Session["userno"]) == UANO)
                //{
                //} 


                this.BindQuotation();


                // objCommon.ReportPopUp(btncmpitem, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Single_Item_Cmp_Report.rpt&param=@UserName=" + Session["userfullname"].ToString() + "," + "@P_QUOTNO=" + lstQtNo.SelectedValue + "," + "@P_ITEM_NO=" + lstItem.SelectedValue, "UAIMS");
            }
        }
        divMsg.InnerText = string.Empty;

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


    void BindQuotation()
    {
        lstQtNo.Items.Clear();
        lstQtNo.Items.Insert(0, new ListItem("Please Select", "0"));
        DataSet ds = ObjStrRecom.GetApprovedQuotation(Convert.ToInt32(Session["strdeptcode"].ToString()));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstQtNo.DataSource = ds.Tables[0];
            lstQtNo.DataTextField = "REFNO";
            lstQtNo.DataValueField = "QUOTNO";
            lstQtNo.DataBind();
        }
    }
    protected void lstQtNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVendor(lstQtNo.SelectedValue);
        grdSelItemList.DataSource = null;
        grdSelItemList.DataBind();
        grdItemsList.DataSource = null;
        grdItemsList.DataBind();
        lvItemDetails.DataSource = null;
        lvItemDetails.DataBind();
        lblTaxAmt.Text = string.Empty;
        lblGrossAmount.Text = string.Empty;
        lblNetAmt.Text = string.Empty;
        //objCommon.ReportPopUp(btnRpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "reccomendationQuotation.rpt&param=@P_QUOTNO=" + lstQtNo.SelectedValue + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");

    }

    //To Show RECOMENDATION report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_QUOTNO=" + lstQtNo.SelectedValue + "," + "@P_PNO=" + lstVendor.SelectedValue + "," + "@username=" + Session["userfullname"].ToString();
            // url += "&param=@P_QUOTNO=" + lstQtNo.SelectedValue + ",@P_PNO=" + lstVendor.SelectedValue + "," + "@username=" + Session["userfullname"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    void BindVendor(string quotno)
    {
        lstVendor.Items.Clear();
        lstVendor.Items.Insert(0,new ListItem("Please Select","0"));
        DataSet ds = ObjStrRecom.GetVendorByQuotation(quotno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstVendor.DataSource = ds.Tables[0];
            lstVendor.DataTextField = "PNAME";
            lstVendor.DataValueField = "PNO";
            lstVendor.DataBind();
        }
    }
    void BindItems(string quotno)
    {
        DataSet ds = ObjStrRecom.GetItemsByQuotNo(quotno);
        grdItemsList.DataSource = ds.Tables[0];
        grdItemsList.DataBind();
    }
    protected void dpUserDet_PreRender(object sender, EventArgs e)
    {

    }
    protected void dpLett_PreRender(object sender, EventArgs e)
    {
        //BindLetter();
    }
    void BindSpecificationByVendor(string Quotno, int pno)
    {
        //DataSet DtItemS = ObjStrRecom.GetVendorQuotationEntryForParty(Quotno, pno);
        //lvItemDetails.DataSource = DtItemS.Tables[0];
        //lvItemDetails.DataBind();
        //lblGrossAmount.Text = DtItemS.Tables[1].Rows[0]["TOTAMOUNT"].ToString();
        //// DataTable DtFieldS = ObjStrRecom.GetVendorQuotationFieldEntryForParty(Quotno, pno).Tables[0];
        ////DataSet DtFieldS = ObjStrRecom.GetVendorQuotationFieldEntryForParty(Quotno, pno,0);
        ////lvFiedSpec.DataSource = DtFieldS.Tables[0];
        ////lvFiedSpec.DataBind();

        //lblTaxAmt.Text = DtItemS.Tables[2].Rows[0]["TOT_TAX_AMT"].ToString();
        ////lblOthAmount.Text = DtFieldS.Tables[2].Rows[0]["OTHER_CHARG"].ToString();

        ////double Amount = Convert.ToDouble(DtItemS.Tables[1].Rows[0]["TOTAMOUNT"].ToString());
        ////double Tax = Convert.ToDouble(DtFieldS.Tables[1].Rows[0]["AMT"].ToString());
        ////double OthAmount = lblOthAmount.Text == "" ? 0.0 :Convert.ToDouble(lblOthAmount.Text);

        //lblNetAmt.Text = Math.Round(Convert.ToDouble(lblGrossAmount.Text) + Convert.ToDouble(lblTaxAmt.Text)).ToString("00.00");
        //// lblNetAmt.Text = Math.Round(Convert.ToDouble(lblGrossAmount.Text)).ToString("00.00");
        //--------------------------19/03/2022---------end-------------------------------------------------------------------------------------------------------
        DataSet DtItemS = ObjStrRecom.GetVendorQuotationEntryForParty(Quotno, pno);
        if (DtItemS.Tables[0].Rows.Count > 0)
        {
            lvItemDetails.DataSource = DtItemS.Tables[0];
            lvItemDetails.DataBind();
            lblGrossAmount.Text = DtItemS.Tables[1].Rows[0]["TAXABLE_AMT"].ToString();
            lblTaxAmt.Text = DtItemS.Tables[2].Rows[0]["TOT_TAX_AMT"].ToString();
            lblNetAmt.Text = DtItemS.Tables[1].Rows[0]["TOTAMOUNT"].ToString();
            //Math.Round(Convert.ToDouble(lblGrossAmount.Text) + Convert.ToDouble(lblTaxAmt.Text)).ToString("00.00");
            // divVendorSpec.Visible = true;
        }
        else
        {
            lvItemDetails.DataSource = null;
            lvItemDetails.DataBind();
            // divVendorSpec.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (lstQtNo.SelectedValue == "")
            //{
            //    Showmessage("Please Select Quotaion No. From Quotaion List.");
            //    return;

            //}
            DataTable dtQty = new DataTable();
            int count = 0;

            if (!(ViewState["dtQty"] == null))
                dtQty = (DataTable)ViewState["dtQty"];

            if (!dtQty.Columns.Contains("Qty"))
                dtQty.Columns.Add("Qty");

            CustomStatus cs = CustomStatus.Others;

            if (lstVendor.SelectedIndex >= 0)
            {
                for (int i = 0; i < grdItemsList.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)grdItemsList.Rows[i].FindControl("chkItem");
                    TextBox txtQty = grdItemsList.Rows[i].FindControl("txtQty") as TextBox;
                    HiddenField hdnQty = grdItemsList.Rows[i].FindControl("hdnQty") as HiddenField;
                    if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(hdnQty.Value))
                    {
                        Showmessage("Final Req.Qty Should Be Less Than Or Equal To Req.Qty.");
                        return;
                    }
                    if (chk.Checked)
                    {
                        count = 1;
                        string Quotno = lstQtNo.SelectedValue;
                        int pno = Convert.ToInt32(lstVendor.SelectedValue);
                        int quotitementry = Convert.ToInt32(grdItemsList.DataKeys[i].Value.ToString());
                        int itemno = Convert.ToInt32(chk.ToolTip);
                        string Qty = txtQty.Text;
                        cs = (CustomStatus)Convert.ToInt32(ObjStrRecom.SaveReccomanforParty(Quotno, pno, quotitementry, Session["colcode"].ToString(), itemno, Qty, txtJustification.Text));
                    }
                }
                if (count != 0)
                {
                    //if (cs == CustomStatus.RecordSaved || cs == CustomStatus.RecordUpdated)
                    // {
                    Showmessage("Recommendation Saved Successfully.");
                    // }
                }
                else
                {
                    Showmessage("Please Select At Least One Item.");
                    return;
                }
                BindItems(lstQtNo.SelectedValue);
                BindAlreadyRecommandItems(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));


            }
            else
            {
                Showmessage("Please Select Vendor From Vendor List");
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, ("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString()));
        }
    }

    protected void lstVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItems(lstQtNo.SelectedValue);
        if (lstVendor.SelectedIndex >= 0)
        {
            BindAlreadyRecommandItems(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
        }
        BindSpecificationByVendor(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
    }

    void BindAlreadyRecommandItems(string quotno, int pno)
    {
        DataSet ds = ObjStrRecom.GetAlreadyRecommandItemsForParty(quotno, pno);
        grdSelItemList.DataSource = ds.Tables[0];
        grdSelItemList.DataBind();
    }

    protected void grdSelItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // we are using a html anchor and a hidden asp:button for the delete
            HtmlAnchor linkDelete = (HtmlAnchor)e.Row.FindControl("linkDelete");
            Button btnDelete = (Button)e.Row.FindControl("btnDelete");

            //for each delete link - the corresponding submit buttons id will be passed to delete call back as a hidden field
            string prompt = "$.prompt('Are you sure you want to delete the selected item?"
                + "<input type=\"hidden\" value=\"{0}\" name=\"hidID\" />'"
                + ", {{buttons: {{ Ok: true, Cancel: false }}, callback: confirmDeleteResult}} ); return false; ";
            linkDelete.Attributes["onclick"] = string.Format(prompt, btnDelete.ClientID);
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton IMG = sender as ImageButton;
        int QINO = Convert.ToInt32(IMG.CommandArgument);
        if (ObjStrRecom.CheckForPoLock(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue)))
        {
            //this.DisplayMessage("You Cannot Modify Recommendation ,Related PO has been Locked");
            Showmessage("You Cannot Modify Recommendation ,Related PO has been Locked");
        }
        else
        {
            //int resstat = ObjStrRecom.DeleteRecommandation(Convert.ToInt32(grdSelItemList.DataKeys[e.RowIndex].Value.ToString()));
            int resstat = ObjStrRecom.DeleteRecommandation(QINO);
            Showmessage("Item Deleted Successfully From Vendor Recommendation List");
            BindItems(lstQtNo.SelectedValue);
            BindAlreadyRecommandItems(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
        }
    }
    protected void grdSelItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ObjStrRecom.CheckForPoLock(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue)))
        {
            //this.DisplayMessage("You Cannot Modify Recommendation ,Related PO has been Locked");
            Showmessage("You Cannot Modify Recommendation ,Related PO has been Locked");
        }
        else
        {
            int resstat = ObjStrRecom.DeleteRecommandation(Convert.ToInt32(grdSelItemList.DataKeys[e.RowIndex].Value.ToString()));
            Showmessage("Item Deleted Successfully From Vendor Recommendation List");
            BindItems(lstQtNo.SelectedValue);
            BindAlreadyRecommandItems(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue));
        }
    }

    //Display Jquery Message Window.
    void DisplayMessage(string Message)
    {

        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    protected void btnRpt_Click(object sender, EventArgs e)
    {
        if (lstQtNo.SelectedValue == "0" || lstQtNo.SelectedValue == "")
        {
            //DisplayMessage("Select Quotation and Vendor from list");
            Showmessage("Select Quotation And Vendor From List");
        }
        else

            if (lstVendor.SelectedValue == "0" || lstVendor.SelectedValue == "")
            {
                Showmessage("Select Vendor From List");
            }
            else
            {
                ShowReport("RECOMMANDATION_REPORT", "reccomendationQuotation.rpt");
            }
        //ShowReport("RECOMMANDATION_REPORT", "Str_Recommand_New.rpt");
    }
    protected void lvItemSpec_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            ListViewDataItem dataitem = (ListViewDataItem)e.Item;

            ListView lv = dataitem.FindControl("lvDetails") as ListView;
            Label lblItemNo = dataitem.FindControl("lblItemNo") as Label;

            DataSet DtFieldS = ObjStrRecom.GetVendorQuotationFieldEntryForParty(lstQtNo.SelectedValue, Convert.ToInt32(lstVendor.SelectedValue), Convert.ToInt32(lblItemNo.Text));

            lv.DataSource = DtFieldS.Tables[0];
            lv.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stores_Transactions_Str_Recommandation.lvItemSpec_ItemDataBound --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
