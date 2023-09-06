//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                                      
// CREATION DATE :                                                    
// CREATED BY    :                                                       
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
using IITMS.NITPRM;
public partial class STORES_Transactions_Quotation_STR_TENDOR_RECOMMENDATION : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_Tender_Recommandation_Controller ObjStrRecom = new Str_Tender_Recommandation_Controller();

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
                //if (Request.QueryString["pageno"] != null)
                //{
                   
                //}

                this.BindTender();
                ViewState["TVNO"] = null;

                // objCommon.ReportPopUp(btncmpitem, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Single_Item_Cmp_Report.rpt&param=@UserName=" + Session["userfullname"].ToString() + "," + "@P_tenderno=" + lstTender.SelectedValue + "," + "@P_ITEM_NO=" + lstItem.SelectedValue, "UAIMS");
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


    void BindTender()
    {
        DataSet ds = ObjStrRecom.GetTenderByDepartment(Convert.ToInt32(Session["strdeptcode"].ToString()));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstTender.DataSource = ds.Tables[0];
            lstTender.DataTextField = "TENDERNO";
            lstTender.DataValueField = "TNO";
            lstTender.DataBind();
        }
        else
        {
            lstTender.Items.Clear();
        }
    }
    protected void lstTender_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindVendor(Convert.ToInt32(lstTender.SelectedValue));
        grdSelItemList.DataSource = null;
        grdSelItemList.DataBind();
        grdvendorList.DataSource = null;
        grdvendorList.DataBind();
        lvItemDetails.DataSource = null;
        lvItemDetails.DataBind();
        divItemDetails.Visible = false;
        divVendorList.Visible = false;
        divSelItemList.Visible = false;
        BindItems(Convert.ToInt32(lstTender.SelectedValue));
        BindAlreadyRecommandItems(Convert.ToInt32(lstTender.SelectedValue));
        //objCommon.ReportPopUp(btnrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "tenderreccomenation.rpt&param=@P_TENDERNO=" + Convert.ToInt32(lstTender.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
    }
    void BindVendor(int tenderno)
    {
        DataSet ds = ObjStrRecom.GetVendorByTender(tenderno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstVendor.DataSource = ds.Tables[0];
            lstVendor.DataTextField = "VENDORNAME";
            lstVendor.DataValueField = "TVNO";
            lstVendor.DataBind();
        }
        else
        {
            lstVendor.Items.Clear();
        }
    }
    void BindItems(int tenderno)
    {
        DataSet ds = ObjStrRecom.GetVendorByTender(tenderno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdvendorList.DataSource = ds.Tables[0];
            grdvendorList.DataBind();
            divVendorList.Visible = true;
        }
        else
        {
            grdvendorList.DataSource = null;
            grdvendorList.DataBind();
            divVendorList.Visible = false;
        }
    }
    void BindSpecificationByVendor(int tenderno, int pno)
    {
        //DataTable DtItemS = ObjStrRecom.GetVendorTenderEntryForParty(tenderno, pno).Tables[0];
        DataSet DtItemS = ObjStrRecom.GetVendorTenderEntryForParty(tenderno, pno);
        if (DtItemS.Tables[0].Rows.Count > 0)
        {
            lvItemDetails.DataSource = DtItemS;
            lvItemDetails.DataBind();
            divItemDetails.Visible = true;
        }
        else
        {
            lvItemDetails.DataSource = null;
            lvItemDetails.DataBind();
            divItemDetails.Visible = false;
        }
        lblGrossAmount.Text = DtItemS.Tables[1].Rows[0]["TOTAMOUNT"].ToString();
        lblTaxAmt.Text = DtItemS.Tables[2].Rows[0]["TOT_TAX_AMT"].ToString();
        lblNetAmt.Text = Math.Round(Convert.ToDouble(lblGrossAmount.Text) + Convert.ToDouble(lblTaxAmt.Text)).ToString("00.00");
        //DataTable DtFieldS = ObjStrRecom.GetVendorTenderFieldEntry(tenderno, pno).Tables[0];
        //if (DtFieldS.Rows.Count > 0)
        //{
        //    grdFiedSpec.DataSource = DtFieldS;
        //    grdFiedSpec.DataBind();
        //}
        //else
        //{
        //    grdFiedSpec.DataSource = null;
        //    grdFiedSpec.DataBind();
        //}
    }

    protected void lvItemSpec_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            ListViewDataItem dataitem = (ListViewDataItem)e.Item;

            ListView lv = dataitem.FindControl("lvDetails") as ListView;
            Label lblItemNo = dataitem.FindControl("lblItemNo") as Label;

            DataSet DtFieldS = ObjStrRecom.GetVendorTenderFieldEntryForParty(lstTender.SelectedValue, Convert.ToInt32(ViewState["TVNO"]), Convert.ToInt32(lblItemNo.Text));

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
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            CustomStatus cs = CustomStatus.Others;

            if (grdSelItemList.Rows.Count > 0)
            {
                DisplayMessage("Vendor Already Recommended, Delete Previous Vendor To Recommend New Vendor.");
                return;
            }

            for (int j = 0; j < grdvendorList.Rows.Count; j++)
            {
                RadioButton chk = (RadioButton)grdvendorList.Rows[j].FindControl("rdbItem");
                if (chk.Checked)
                {
                    for (int i = 0; i < lvItemDetails.Items.Count(); i++)
                    {
                        HiddenField hdnTino = lvItemDetails.Items[i].FindControl("hdnTino") as HiddenField;
                        Label lblItemNo = lvItemDetails.Items[i].FindControl("lblItemNo") as Label;
                        int tenderno = Convert.ToInt32(lstTender.SelectedValue);
                        int itemno = Convert.ToInt32(lblItemNo.Text);
                        HiddenField hdnQty = lvItemDetails.Items[i].FindControl("hdnQty") as HiddenField;
                        int pno = Convert.ToInt32(grdvendorList.DataKeys[j].Value.ToString());
                        // int pno = Convert.ToInt32(lstVendor.SelectedValue);
                        int tenderitementry = Convert.ToInt32(hdnTino.Value);//Convert.ToInt32(lvItemDetails.DataKeys[i].Value.ToString());
                        cs = (CustomStatus)Convert.ToInt32(ObjStrRecom.SaveReccomanforParty(tenderno, pno, tenderitementry, Session["colcode"].ToString(), itemno, Convert.ToInt32(hdnQty.Value)));
                        //int check = Convert.ToInt32(objCommon.LookUp("STORE_TENDER_RECOMMAND", "COUNT(*)", "TENDERNO=" + tenderno + "AND TINO=" + tenderitementry));
                        //if (check == 0)
                        //{
                        //    cs = (CustomStatus)Convert.ToInt32(ObjStrRecom.SaveReccomanforParty(tenderno, pno, tenderitementry, Session["colcode"].ToString(), itemno, Convert.ToInt32(hdnQty.Value)));
                        //}
                        //else
                        //{
                        //    DisplayMessage("Already Recommended ! Delete Previous Vendor.");
                        //    cs = CustomStatus.RecordExist;
                        //}
                    }
                    break;
                }
            }
            if (cs == CustomStatus.RecordSaved || cs == CustomStatus.RecordUpdated)
            {

                this.DisplayMessage("Recommendation Saved Successfully");

            }
            else if (cs == CustomStatus.RecordExist)
            {
                BindAlreadyRecommandItems(Convert.ToInt32(lstTender.SelectedValue));
            }
            else
            {
                //DisplayMessage("Select Vendor to Be Recommand");
                this.DisplayMessage("Recommendation Saved Successfully");
            }

            BindAlreadyRecommandItems(Convert.ToInt32(lstTender.SelectedValue));
            //BindItems(Convert.ToInt32(lstTender.SelectedValue));
            //BindVendor(Convert.ToInt32(lstTender.SelectedValue));


        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, ("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString()));
        }
    }
    protected void lstVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItems(Convert.ToInt32(lstTender.SelectedValue));
        if (lstVendor.SelectedIndex >= 0)
        {
            BindAlreadyRecommandItems(Convert.ToInt32(lstTender.SelectedValue));
        }
        BindSpecificationByVendor(Convert.ToInt32(lstTender.SelectedValue), Convert.ToInt32(lstVendor.SelectedValue));
        BindAlreadyRecommandItems(Convert.ToInt32(lstTender.SelectedValue));
        BindItems(Convert.ToInt32(lstTender.SelectedValue));
    }
    void BindAlreadyRecommandItems(int tenderno)
    {
        DataSet ds = ObjStrRecom.GetAlreadyRecommandItemsForParty(tenderno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdSelItemList.DataSource = ds.Tables[0];
            grdSelItemList.DataBind();
            divSelItemList.Visible = true;
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Count = Convert.ToInt32(objCommon.LookUp("STORE_PORDER", "Count(*)", "TENDERNO ='" + lstTender.SelectedItem.Text + "'"));
        if (Count > 0)
        {
            this.DisplayMessage("You Can Not Modify Recommendation, Related PO Has Been Locked");
            return;

        }
        else
        {
            int resstat = ObjStrRecom.DeleteRecommandation(Convert.ToInt32(lstTender.SelectedValue));
            this.DisplayMessage("Vendor Deleted Successfully From The Recommendation");
            // lstVendor.SelectedIndex = 0;
            //BindVendor(Convert.ToInt32(lstTender.SelectedValue));
            //BindItems(Convert.ToInt32(lstTender.SelectedValue));
            grdSelItemList.DataSource = null;
            grdSelItemList.DataBind();
            divSelItemList.Visible = false;
        }
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
    protected void grdSelItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int pno = Convert.ToInt32(grdSelItemList.DataKeys[0].Value.ToString());
        if (ObjStrRecom.CheckForPoLock(lstTender.SelectedValue, pno))
        {
            this.DisplayMessage("You Cannot Modify Recommendation ,Related PO has been Locked");

        }
        else
        {
            int resstat = ObjStrRecom.DeleteRecommandation(Convert.ToInt32(grdSelItemList.DataKeys[e.RowIndex].Value.ToString()));
            this.DisplayMessage("Vendor Deleted From Vendor Recommendation List");
            // lstVendor.SelectedIndex = 0;
            BindVendor(Convert.ToInt32(lstTender.SelectedValue));
            BindItems(Convert.ToInt32(lstTender.SelectedValue));
            grdSelItemList.DataSource = null;
            grdSelItemList.DataBind();
            divSelItemList.Visible = false;
        }
    }

    //Display Jquery Message Window.

    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);
    }


    //void DisplayMessage(string Message)
    //{

    //    string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
    //    string message = string.Format(prompt, Message);
    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    //}


    protected void btnRept1_Click(object sender, EventArgs e)
    {
        ShowReport("Tender_Reccomenation_Report", "tenderreccomenation.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            //url += "&param=@username=" + Session["userfullname"].ToString() + "," + "@P_TENDERNO=" + lstTender.SelectedValue  + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_TENDERNO=" + lstTender.SelectedValue;

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel

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


    protected void rdbItem_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow oldrow in grdvendorList.Rows)
        {
            ((RadioButton)oldrow.FindControl("rdbItem")).Checked = false;
        }

        //Set the new selected row
        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;
        ((RadioButton)row.FindControl("rdbItem")).Checked = true;


        int count = 0;
        for (int i = 0; i < grdvendorList.Rows.Count; i++)
        {
            RadioButton rdbItem = (RadioButton)grdvendorList.Rows[i].FindControl("rdbItem");
            if (rdbItem.Checked)
            {
                int TVNO = Convert.ToInt32(rdbItem.ToolTip);
                
                if (TVNO > 0)
                {
                    BindAlreadyRecommandItems(Convert.ToInt32(lstTender.SelectedValue));
                }
                ViewState["TVNO"] = TVNO;
                BindSpecificationByVendor(Convert.ToInt32(lstTender.SelectedValue), TVNO);               
                //BindItems(Convert.ToInt32(lstTender.SelectedValue));              
                count++;
            }
            if (count > 0)
                break;
        }

    }
}
