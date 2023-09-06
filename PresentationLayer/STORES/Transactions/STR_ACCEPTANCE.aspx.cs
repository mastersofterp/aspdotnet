//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : STR_ACCEPTANCE.ASPX                                     
// CREATION DATE : 18-MAY-2011                                                        
// CREATED BY    : VIKRAMRAJ SAHU                                                        
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System.Collections;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Collections.Generic;

//using BusinessLogicLayer.BusinessEntities.Stores;

public partial class STORES_Transactions_STR_ACCEPTANCE : System.Web.UI.Page
{



    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_ISSUE_ITEM_CONTROLLER objIssueItemController = new STR_ISSUE_ITEM_CONTROLLER();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
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
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["butAction"] = "add";
                //Session["dtitems"] = null;
                txtItemAcceptDate.Text = System.DateTime.Now.ToString();
            }
            pnlReport.Visible = false;
            int mdno = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "MDNO", "MDNAME='" + Session["strdeptname"].ToString() + "'"));
            FillIssueSlipNo();
            tdIssue.Visible = true;

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


    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStoreUser";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    protected void radReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_itemLV();
        if (radReq.SelectedValue == "0")
        {
            FillRequestionNo();
            //tdReq.Visible = true;
            tdIssue.Visible = false;
        }
        else
            if (radReq.SelectedValue == "1")
            {
                FillIssueSlipNo();
                tdReq.Visible = false;
                tdIssue.Visible = true;
            }
    }
    protected void FillIssueSlipNo()
    {
        try
        {
            int mdno = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "MDNO", "MDNAME='" + Session["strdeptname"].ToString() + "'"));

            objCommon.FillDropDownList(ddlIssueSlipNo, "STORE_JVSTOCK_MAIN A inner join STORE_JVSTOCK_TRAN B on (A.JVTRAN_ID=B.JVTRAN_ID)", "Distinct(A.JVTRAN_ID)", "JVTRAN_SLIP_NO", "A.JVTRAN_TYPE=1 and A.JVTRAN_ID not in (SELECT JVTRAN_ID FROM STORE_JVSTOCK_TRAN WHERE ACC_DATE IS NOT NULL)", "A.JVTRAN_ID desc");
            //objCommon.FillDropDownList(ddlIssueSlipNo, "STORE_ISSUE_ITEM A INNER JOIN STORE_ISSUE_ITEM_TRAN B ON (A.ISSUENO=B.ISSUENO)", "DISTINCT A.ISSUENO", "ISSUE_SLIPNO", "STATUS='P' AND ISSUED_TO_MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void FillRequestionNo()
    //{

    //          try
    //    {
    //        int mdno = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "MDNO", "MDNAME='" + Session["strdeptname"].ToString() + "'"));

    //                    objCommon.FillDropDownList(ddlReq , "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "REQ_NO", "I.STATUS=" + "'P' and R.MDNO="+mdno , "REQ_NO DESC");

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void FillRequestionNo()
    {

        try
        {
            int mdno = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "MDNO", "MDNAME='" + Session["strdeptname"].ToString() + "'"));

            //if (objCommon.LookUp("STORE_CONFIG", "PARAMETER", "CONFIGDESC='CONDENSE REQUISITION CREATED BY APPROVAL'").ToString().Trim() == "Y")
            //{
            //    objCommon.FillDropDownList(ddlReq, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "REQ_NO", "I.STATUS=" + "'P' and R.MDNO=" + mdno + " AND R.CLUBREQSTATUS='NR'", "REQ_NO DESC");
            //}
            //else
            //{

            //}
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                objCommon.FillDropDownList(ddlReq, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "REQ_NO", "I.STATUS=" + "'P' and R.MDNO=" + mdno + " AND R.STORE_USER_TYPE='M' ", "REQ_NO DESC");
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                objCommon.FillDropDownList(ddlReq, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "REQ_NO", "I.STATUS=" + "'P' and R.MDNO=" + mdno + " AND R.STORE_USER_TYPE='D' ", "REQ_NO DESC");
            }
            else
            {
                objCommon.FillDropDownList(ddlReq, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "REQ_NO", "I.STATUS=" + "'P' and R.MDNO=" + mdno + " AND R.STORE_USER_TYPE='N' ", "REQ_NO DESC");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlIssueSlipNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string ISSUE_TYPE = objCommon.LookUp("Store_JvStock_Main", "ISSUE_TYPE", "JVTRAN_ID=" + Convert.ToInt32(ddlIssueSlipNo.SelectedValue));
        GetIssuedItem(Convert.ToInt32(ddlIssueSlipNo.SelectedValue), "I");

    }


    protected void GetIssuedItem(int issueno, string ISSUE_TYPE)
    {
        if (issueno > 0)
        {
            ViewState["issueno"] = issueno;
            DataSet ds = objIssueItemController.GetIssuedItemByIssueNo(issueno, ISSUE_TYPE);
            ViewState["dsIssuedItem"] = ds;
            lvitemReq.DataSource = ds;
            lvitemReq.DataBind();
        }
        else
        {
            lvitemReq.DataSource = null;
            lvitemReq.DataBind();
        }
    }
    protected void ddlReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetIssuedItem(Convert.ToInt32(ddlReq.SelectedValue));

    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {

            string Invidno = string.Empty;
            foreach (ListViewItem lv in lvDsr.Items)
            {
                //CheckBox chkItmSelect = lv.FindControl("chkItmSelect") as CheckBox;
                CheckBox chkDsrselect = lv.FindControl("chkDsrselect") as CheckBox;
                if (chkDsrselect.Checked)
                {
                    string invidno = chkDsrselect.ToolTip;
                    Invidno += invidno + ',';
                }

            }
            if (Invidno != string.Empty)
                Invidno = Invidno.Substring(0, Invidno.Length - 1);

            DataTable dt = new DataTable();
            dt.Columns.Add("ITEM_NO", typeof(int));
            dt.Columns.Add("ACCEPTED_QTY", typeof(int));
            DataRow dtRow = null;
            foreach (ListViewItem lv in lvitemReq.Items)
            {
                CheckBox chkItmSelect = lv.FindControl("chkItmSelect") as CheckBox;
                HiddenField hdItemno = lv.FindControl("hdItemno") as HiddenField;
                TextBox txtAccQty = lv.FindControl("txtAccQty") as TextBox;

                if (chkItmSelect.Checked && chkItmSelect.ToolTip == "2")
                {
                    dtRow = dt.NewRow();
                    dtRow["ITEM_NO"] = hdItemno.Value;
                    dtRow["ACCEPTED_QTY"] = txtAccQty.Text;
                    dt.Rows.Add(dtRow);
                }
            }

            CustomStatus cs = (CustomStatus)objIssueItemController.UpdateAcceptanceStatus(Convert.ToInt32(ViewState["issueno"].ToString()), txtRemark.Text, txtItemAcceptDate.Text, Invidno, dt);
            DisplayMessage("Item Accepted Successfully");
            Clear();
            Clear_itemLV();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    void Clear_itemLV()
    {
        lvitemReq.DataSource = null;
        lvitemReq.DataBind();
        lvDsr.DataSource = null;
        lvDsr.DataBind();
        lvDsr.Visible = false;
        hdnDsrRowCount.Value = "0";
    }
    void Clear()
    {
        txtRemark.Text = string.Empty;
        tdReq.Visible = false;
        radReq.ClearSelection();
        pnlReject.Visible = false;
        ViewState["issueno"] = null;
        FillIssueSlipNo();
    }
    protected void btnRejectSlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (tdIssue.Visible == true)
            {
                //CustomStatus cs = (CustomStatus)objIssueItemController.UpdateAcceptanceStatus(Convert.ToInt32(ViewState["issueno"]), "R", txtRemark.Text, txtItemAcceptDate.Text, 0, 0);
                //if (cs.Equals(CustomStatus.RecordSaved))
                //{
                //    DisplayMessage("Rejected Successfully");
                //}
            }
            Clear();
            Clear_itemLV();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int rejQty = Convert.ToInt32(txtRejectItems.Text);
            int issuedQty = Convert.ToInt32(lblIssuedItem.Text);


            {
                if (rejQty == 0)
                {
                    DisplayMessage("Enter Rejected Quantity greater than 0");
                    return;
                }
                else
                {
                    if (rejQty <= issuedQty)
                    {
                        if (ViewState["rejaction"].ToString() == "add")
                        {
                            int count = Convert.ToInt32(objCommon.LookUp("STORE_REJECTED_ITEM", "count(*)", "issueno=" + ViewState["issueno"] + "and item_no=" + lblIName.ToolTip));
                            if (count == 0)
                            {
                                CustomStatus cs = (CustomStatus)objIssueItemController.RejectedItemInsert(Convert.ToInt32(ViewState["issueno"]), Convert.ToInt32(txtRejectItems.Text), txtRejReason.Text, Convert.ToInt32(lblIName.ToolTip), Convert.ToInt32(lblIssuedItem.Text));
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    DisplayMessage("Rejected Item saved Successfully");
                                    FillAcceptedQty();
                                }
                            }
                            else
                            {
                                DisplayMessage("Item Already Rejected. So, you cannot reject it again");
                            }
                        }
                        else
                        {
                            CustomStatus cs1 = (CustomStatus)objIssueItemController.UpdateRejectedItemDetails(Convert.ToInt32(ViewState["rejno"]), Convert.ToInt32(txtRejectItems.Text), txtRejReason.Text);
                            if (cs1.Equals(CustomStatus.RecordSaved))
                            {
                                DisplayMessage("Item Updated successfully");
                                FillAcceptedQty();
                            }
                        }
                        BindRejItems();

                        txtRejectItems.Text = string.Empty;
                        txtRejReason.Text = string.Empty;
                    }

                    else
                    {
                        DisplayMessage("Rejected Quantity cannot be greater than issued quantity");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnRejDeletet_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int rejNo = Convert.ToInt32(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objIssueItemController.DeleteRejectedItemDetails(rejNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                DisplayMessage("Item Deleted successfully");
            }
            BindRejItems();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnRejEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int rejNo = Convert.ToInt32(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("STORE_REJECTED_ITEM R INNER JOIN STORE_ITEM I ON(R.ITEM_NO=I.ITEM_NO)", "REJTRNO,I.ITEM_NAME,ISSUENO,R.ITEM_NO", "ISSUED_QTY,REJ_QTY,REASON", "rejtrno=" + rejNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblIName.Text = ds.Tables[0].Rows[0]["ITEM_NAME"].ToString();
                lblIssuedItem.Text = ds.Tables[0].Rows[0]["ISSUED_QTY"].ToString();
                txtRejectItems.Text = ds.Tables[0].Rows[0]["REJ_QTY"].ToString();
                txtRejReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
            }
            ViewState["rejaction"] = "edit";
            ViewState["rejno"] = rejNo;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    void clear_rejItem()
    {
        lblIName.Text = string.Empty;
        lblIssuedItem.Text = string.Empty;
        txtRejReason.Text = string.Empty;
        txtRejectItems.Text = string.Empty;
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        Button btnReject = sender as Button;
        HiddenField hdItemno = lvitemReq.Items[((btnReject.Parent) as ListViewDataItem).DataItemIndex].FindControl("hdItemno") as HiddenField;
        Label lblItemName = lvitemReq.Items[((btnReject.Parent) as ListViewDataItem).DataItemIndex].FindControl("lblItemName") as Label;
        Label lblIssuedQty = lvitemReq.Items[((btnReject.Parent) as ListViewDataItem).DataItemIndex].FindControl("lblIssueQty") as Label;
        lblIName.ToolTip = hdItemno.Value;
        lblIName.Text = lblItemName.Text;
        lblIssuedItem.Text = lblIssuedQty.Text;
        BindRejItems();
        pnlReject.Visible = true;
        pnlreq.Visible = false;
        ViewState["rejaction"] = "add";
    }

    void BindRejItems()
    {
        DataSet ds = objCommon.FillDropDown("STORE_REJECTED_ITEM R INNER JOIN STORE_ITEM I ON(R.ITEM_NO=I.ITEM_NO)", "REJTRNO,I.ITEM_NAME,ISSUENO,R.ITEM_NO", "ISSUED_QTY,REJ_QTY,REASON", "ISSUENO=" + ViewState["issueno"] + " and R.ITEM_NO=" + lblIName.ToolTip, "");
        lvRejItems.DataSource = ds;
        lvRejItems.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear_rejItem();
        pnlReject.Visible = false;
        pnlreq.Visible = true;
    }
    void FillAcceptedQty()
    {
        DataSet ds = (DataSet)ViewState["dsIssuedItem"];
        //ds.Tables[0].Columns.Add("ACC_QTY");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if ((int)ds.Tables[0].Rows[i]["ITEM_NO"] == Convert.ToInt32(lblIName.ToolTip))
            {
                ds.Tables[0].Rows[i]["ACC_QTY"] = (Convert.ToUInt32(lblIssuedItem.Text) - Convert.ToInt32(txtRejectItems.Text)).ToString();
                break;
            }
        }
        lvitemReq.DataSource = ds;
        lvitemReq.DataBind();
    }
    void DisplayMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
        //string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        //string message = string.Format(prompt, Message);
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {

        pnlReport.Visible = true;
        pnlAcceptance.Visible = false;
        int mdno = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "MDNO", "MDNAME='" + Session["strdeptname"].ToString() + "'"));
        if (ViewState["StoreUser"].ToString() == "MainStoreUser")
        {
            objCommon.FillDropDownList(ddlIssueSlipNoRpt, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "ISSUE_SLIPNO", "R.MDNO=" + mdno + " AND R.STORE_USER_TYPE='M' ", "ISSUE_SLIPNO DESC");
        }
        else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
        {
            objCommon.FillDropDownList(ddlIssueSlipNoRpt, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "ISSUE_SLIPNO", "R.MDNO=" + mdno + " AND R.STORE_USER_TYPE='D' ", "ISSUE_SLIPNO DESC");
        }
        else
        {
            objCommon.FillDropDownList(ddlIssueSlipNoRpt, "STORE_REQ_MAIN R INNER JOIN STORE_ISSUE_ITEM I ON(R.REQTRNO=I.REQTRNO)", "ISSUENO", "ISSUE_SLIPNO", "R.MDNO=" + mdno + " AND R.STORE_USER_TYPE='N' ", "ISSUE_SLIPNO DESC");
        }



        //objCommon.FillDropDownList(ddlIssueSlipNoRpt, "STORE_ISSUE_ITEM", "ISSUENO", "ISSUE_SLIPNO", "", "ISSUE_SLIPNO");

        pnlReport.Visible = true;

    }
    protected void btnCancelRpt_Click1(object sender, EventArgs e)
    {
        //pnlReport.Visible = false;
        //pnlAcceptance.Visible = true;

        ddlIssueSlipNoRpt.SelectedIndex = 0;
    }
    protected void btnPrintRpt_Click(object sender, EventArgs e)
    {
        ShowReportIndividual("Acceptance", "Str_Acceptance.rpt");
    }
    protected void ddlIssueSlipNoRpt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void ShowReportIndividual(string reportTitle, string rptFileName)
    {
        try
        {

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("STORE")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ISSUENO=" + Convert.ToInt32(ddlIssueSlipNoRpt.SelectedValue) + ",@P_ISSUE_SLIPNO=" + ddlIssueSlipNoRpt.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkIssueSelect_CheckedChanged(object sender, EventArgs e)
    {
        string ItemNo = string.Empty;
        foreach (ListViewItem lv in lvitemReq.Items)
        {
            CheckBox chkItmSelect = lv.FindControl("chkItmSelect") as CheckBox;
            HiddenField hdItemno = lv.FindControl("hdItemno") as HiddenField;
            if (chkItmSelect.Checked)
            {
                string Migno = chkItmSelect.ToolTip;
                 if (Migno == "1")
                 {
                     string itemno = hdItemno.Value;
                     ItemNo += itemno + ',';
                 }
                
            }

        }
        if (ItemNo != string.Empty)
        {
            ItemNo = ItemNo.Substring(0, ItemNo.Length - 1);
            DataSet ds = objCommon.FillDropDown("STORE_JVSTOCK_MAIN A INNER JOIN  STORE_JVSTOCK_TRAN B ON (A.JVTRAN_ID=B.JVTRAN_ID)", "B.INVDINO", "B.DSR_NUMBER,B.ITEM_NO", "B.ITEM_NO IN (" + ItemNo + ") AND A.JVTRAN_ID=" + ddlIssueSlipNo.SelectedValue, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDsr.DataSource = ds.Tables[0];
                lvDsr.DataBind();
                lvDsr.Visible = true;
                hdnDsrRowCount.Value = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                for (int i = 0; i < ItemNo.Length; i++)
                {
                    string Migno = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO IN (" + ItemNo + ") AND MIGNO=1");
                    if (Migno == "1")
                    {
                        objCommon.DisplayMessage(this.Page, "For Fixed Item Serial Number Is Not Generated", this);
                        return;
                    }
                }
                //DisplayMessage("This Item Not  Exist.");
            }
        }
        else
        {
            lvDsr.DataSource = null;
            lvDsr.DataBind();
            hdnDsrRowCount.Value = "0";
            lvDsr.Visible = false;
        }

    }
}
