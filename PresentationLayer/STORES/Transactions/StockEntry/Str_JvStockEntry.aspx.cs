//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_JvStockEntry.aspx                                             
// CREATION DATE : 26/07/2021                                                     
// CREATED BY    : GOPAL ANTHATI                                                      
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
using System.Net.Mail;


public partial class STORES_Transactions_StockEntry_Str_JvStockEntry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_JvStockCon objJVCon = new Str_JvStockCon();
    Str_JvStockEnt objJVEnt = new Str_JvStockEnt();

    DataTable JVItemsTbl = null;
    DataRow dtRow = null;

    DataTable InvoiceTbl = null;  //27-05-2023
    DataRow dtInvRow = null;
    DataTable dtItemTable = null;


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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                ViewState["REQTRNO"] = "0";
                ViewState["dtItem1"] = null;
            }
            FillDropDownList();
            ViewState["action"] = "add";
            txtTranDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
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



    //---------24-05-2023---start--create data table for hold invoice qty and rate

    private DataTable CreateInvoiceTable()
    {
        InvoiceTbl = new DataTable();
        InvoiceTbl.Columns.Add("ITEM_NO", typeof(int));
        InvoiceTbl.Columns.Add("INVTRNO", typeof(int));
        InvoiceTbl.Columns.Add("INVOICE_QTY", typeof(int));
        InvoiceTbl.Columns.Add("ISSUE_QTY", typeof(int));
        InvoiceTbl.Columns.Add("RATE", typeof(int));
        InvoiceTbl.Columns.Add("Total_Amount", typeof(int));
        InvoiceTbl.Columns.Add("STATUS", typeof(int));
        InvoiceTbl.Columns.Add("INV_DSR_TYPE", typeof(char));
        InvoiceTbl.Columns.Add("DSTK_ENTRY_ID", typeof(int));
        return InvoiceTbl;
    }
    private DataTable CreateInvoiceMultiTable()
    {
        dtItemTable = new DataTable();
        dtItemTable.Columns.Add("ITEM_NO", typeof(int));
        dtItemTable.Columns.Add("INVTRNO", typeof(int));
        dtItemTable.Columns.Add("INVOICE_QTY", typeof(int));
        dtItemTable.Columns.Add("ISSUE_QTY", typeof(int));
        dtItemTable.Columns.Add("RATE", typeof(int));
        dtItemTable.Columns.Add("Total_Amount", typeof(int));
        dtItemTable.Columns.Add("STATUS", typeof(int));
        dtItemTable.Columns.Add("INV_DSR_TYPE", typeof(char));
        dtItemTable.Columns.Add("DSTK_ENTRY_ID", typeof(int));
        return dtItemTable;
    }
    //--------24-05-2023----end



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtTranDate.Text == string.Empty) //Issue Item
            {
                objCommon.DisplayMessage(this.Page, "Please Enter " + lblTranDate.Text, this);
                return;
            }

            //if (rdbDirectIssue.Text == "Direct Item Issue")
            //{
            //    if (ddlItemIsue.SelectedItem.Text == "Please Select") //Issue Item
            //    {
            //        objCommon.DisplayMessage(this.Page, "Please Select Item Name", this);
            //        return;
            //    }
            //    if (txtItemQty.Text == string.Empty) // Quantity
            //    {
            //        objCommon.DisplayMessage(this.Page, "Please Enter  in Quantity", this);
            //        return;
            //    }
            //}


            //----------------start----18-07-2023----Shaikh Juned---
            if (ddlTranType.SelectedValue == "1")
            {
                foreach (ListViewItem lv in lvIssueItem.Items)
                {
                    CheckBox chkIssueSelect = lv.FindControl("chkIssueSelect") as CheckBox;

                    if (chkIssueSelect.Checked == true)
                    {
                        int BalQty = 0;
                        int invQty = 0; int INVTRNO; int status = 0; int BalInvQty;
                        int BalOfIssueQty = 1; int IssuedQty = 0; int DSTK_ENTRY_ID; string DeadStockStatus = string.Empty;
                        int InvBalQty; int UseQty = 0; int eqlQty = 0;
                        HiddenField hdnItemNo = lv.FindControl("hdnItemNo") as HiddenField;
                        int itemno = Convert.ToInt32(hdnItemNo.Value);
                        TextBox txtIQty = lv.FindControl("txtIQty") as TextBox;
                        int IssueQty = Convert.ToInt32(txtIQty.Text);

                        string ItemType = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO =" + itemno);
                        if (ItemType == "2") //  Consumable Item
                        {
                            if (IssueQty != 0)
                            {
                                DataSet ds = objJVCon.GetInvoiceDetailsByItem(itemno);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
                                    {
                                        if (IssueQty != 0)
                                        {
                                            invQty = Convert.ToInt32(ds.Tables[0].Rows[i]["INV_QTY"]);
                                            INVTRNO = Convert.ToInt32(ds.Tables[0].Rows[i]["INVTRNO"]);
                                            DSTK_ENTRY_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["DSTK_ENTRY_ID"]);

                                            if (IssuedQty == 0)
                                            {
                                                if (DSTK_ENTRY_ID == 0)
                                                {
                                                    IssuedQty = Convert.ToInt32(objCommon.LookUp("STORE_INVOICE_ISSUE_ITEM_QTY", "isnull(Sum(cast(ISSUE_QTY as int)),0) as INVOICE_QTY", "INVTRNO ='" + INVTRNO + "' and ITEM_NO='" + itemno + "'"));
                                                }
                                                else
                                                {
                                                    IssuedQty = Convert.ToInt32(objCommon.LookUp("STORE_INVOICE_ISSUE_ITEM_QTY", "isnull(Sum(cast(ISSUE_QTY as int)),0) as INVOICE_QTY", "DSTK_ENTRY_ID ='" + DSTK_ENTRY_ID + "' and ITEM_NO='" + itemno + "' and INV_DSR_TYPE='" + 'D' + "'"));
                                                }
                                            }

                                            InvBalQty = System.Math.Abs(invQty - IssuedQty);
                                            invQty = InvBalQty;

                                            if (invQty == IssueQty)
                                            {
                                                dtInvRow = null;
                                                eqlQty = IssueQty;
                                                BalInvQty = invQty - InvBalQty;
                                                IssueQty = BalInvQty;
                                                //   BalQty = IssuedQty + InvBalQty;
                                                IssuedQty = 0;
                                                if (BalInvQty == 0)
                                                {
                                                    status = 1;
                                                }
                                                else
                                                {
                                                    status = 0;
                                                }

                                                if (DSTK_ENTRY_ID != 0)
                                                {
                                                    DeadStockStatus = "D";
                                                }
                                                else if (INVTRNO != 0)
                                                {
                                                    DeadStockStatus = "I";
                                                }
                                                else if (INVTRNO == 0 && DSTK_ENTRY_ID == 0)
                                                {
                                                    DeadStockStatus = "O";
                                                }
                                                InvoiceTbl = this.CreateInvoiceTable();
                                                dtInvRow = InvoiceTbl.NewRow();
                                                dtInvRow["ITEM_NO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ITEM_NO"]);
                                                if (DSTK_ENTRY_ID == 0)
                                                {
                                                    dtInvRow["INVTRNO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["INVTRNO"]);
                                                }
                                                else
                                                {
                                                    dtInvRow["DSTK_ENTRY_ID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["DSTK_ENTRY_ID"]);
                                                }
                                                dtInvRow["INVOICE_QTY"] = Convert.ToInt32(ds.Tables[0].Rows[i]["INV_QTY"]);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    dtInvRow["ISSUE_QTY"] = Convert.ToInt32(txtIQty.Text);
                                                }
                                                else
                                                {
                                                    dtInvRow["ISSUE_QTY"] = eqlQty;
                                                }
                                                dtInvRow["RATE"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    dtInvRow["Total_Amount"] = Convert.ToInt32(txtIQty.Text) * Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                }
                                                else
                                                {
                                                    dtInvRow["Total_Amount"] = Convert.ToInt32(eqlQty) * Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                }
                                                dtInvRow["STATUS"] = status;
                                                dtInvRow["INV_DSR_TYPE"] = DeadStockStatus;
                                                InvoiceTbl.Rows.Add(dtInvRow);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    ViewState["dtItem1"] = InvoiceTbl;
                                                }

                                            }
                                            else if (IssueQty < invQty)
                                            {

                                                BalOfIssueQty = 0;
                                                BalInvQty = System.Math.Abs(InvBalQty - IssueQty);
                                                if (InvBalQty > IssueQty)
                                                {
                                                    UseQty = IssueQty;
                                                }

                                                if (IssuedQty == 0)
                                                {
                                                    BalQty = IssueQty + IssuedQty;
                                                }
                                                else
                                                {
                                                    BalQty = IssuedQty + InvBalQty;
                                                }
                                                IssueQty = BalInvQty;
                                                IssuedQty = 0;
                                                if (BalQty == invQty)
                                                {
                                                    status = 1;
                                                }
                                                else
                                                {
                                                    status = 0;
                                                }

                                                if (DSTK_ENTRY_ID != 0)
                                                {
                                                    DeadStockStatus = "D";
                                                }
                                                else if (INVTRNO != 0)
                                                {
                                                    DeadStockStatus = "I";
                                                }
                                                else if (INVTRNO == 0 && DSTK_ENTRY_ID == 0)
                                                {
                                                    DeadStockStatus = "O";
                                                }
                                                if (InvBalQty > IssueQty)
                                                {
                                                    IssueQty = 0;
                                                }
                                                InvoiceTbl = this.CreateInvoiceTable();
                                                dtInvRow = InvoiceTbl.NewRow();
                                                dtInvRow["ITEM_NO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ITEM_NO"]);
                                                if (DSTK_ENTRY_ID == 0)
                                                {
                                                    dtInvRow["INVTRNO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["INVTRNO"]);
                                                }
                                                else
                                                {
                                                    dtInvRow["DSTK_ENTRY_ID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["DSTK_ENTRY_ID"]);
                                                }
                                                dtInvRow["INVOICE_QTY"] = Convert.ToInt32(ds.Tables[0].Rows[i]["INV_QTY"]);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    dtInvRow["ISSUE_QTY"] = Convert.ToInt32(txtIQty.Text);
                                                }
                                                else
                                                {
                                                    dtInvRow["ISSUE_QTY"] = Convert.ToInt32(UseQty);
                                                }
                                                dtInvRow["RATE"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    dtInvRow["Total_Amount"] = Convert.ToInt32(txtIQty.Text) * Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                }
                                                else
                                                {
                                                    dtInvRow["Total_Amount"] = Convert.ToInt32(UseQty) * Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                }
                                                dtInvRow["STATUS"] = status;
                                                dtInvRow["INV_DSR_TYPE"] = DeadStockStatus;
                                                InvoiceTbl.Rows.Add(dtInvRow);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    ViewState["dtItem1"] = InvoiceTbl;
                                                }

                                            }
                                            else if (IssueQty > invQty)
                                            {
                                                BalInvQty = System.Math.Abs(IssueQty - InvBalQty);
                                                if (InvBalQty < IssueQty)
                                                {
                                                    UseQty = InvBalQty;
                                                    BalQty = InvBalQty - invQty;
                                                }
                                                else
                                                {
                                                    BalQty = IssuedQty + InvBalQty;
                                                    BalQty = BalQty - invQty;
                                                }
                                                IssueQty = BalInvQty;

                                                IssuedQty = 0;
                                                //if (IssuedQty < BalInvQty)
                                                //{
                                                //    BalQty = 0;
                                                //}
                                                if (BalQty == 0)
                                                {
                                                    status = 1;
                                                }
                                                else
                                                {
                                                    status = 0;
                                                }
                                                if (DSTK_ENTRY_ID != 0)
                                                {
                                                    DeadStockStatus = "D";
                                                }
                                                else if (INVTRNO != 0)
                                                {
                                                    DeadStockStatus = "I";
                                                }
                                                else if (INVTRNO == 0 && DSTK_ENTRY_ID == 0)
                                                {
                                                    DeadStockStatus = "O";
                                                }
                                                InvoiceTbl = this.CreateInvoiceTable();
                                                dtInvRow = InvoiceTbl.NewRow();
                                                dtInvRow["ITEM_NO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ITEM_NO"]);
                                                if (DSTK_ENTRY_ID == 0)
                                                {
                                                    dtInvRow["INVTRNO"] = Convert.ToInt32(ds.Tables[0].Rows[i]["INVTRNO"]);
                                                }
                                                else
                                                {
                                                    dtInvRow["DSTK_ENTRY_ID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["DSTK_ENTRY_ID"]);
                                                }
                                                dtInvRow["INVOICE_QTY"] = Convert.ToInt32(ds.Tables[0].Rows[i]["INV_QTY"]);
                                                //dtInvRow["ISSUE_QTY"] = Convert.ToInt32(txtIQty.Text);
                                                if (BalInvQty != 0)
                                                {
                                                    dtInvRow["ISSUE_QTY"] = Convert.ToInt32(UseQty);
                                                }
                                                else
                                                {
                                                    dtInvRow["ISSUE_QTY"] = Convert.ToInt32(UseQty);
                                                }
                                                dtInvRow["RATE"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                dtInvRow["Total_Amount"] = Convert.ToInt32(UseQty) * Convert.ToInt32(ds.Tables[0].Rows[i]["RATE"]);
                                                dtInvRow["STATUS"] = status;
                                                dtInvRow["INV_DSR_TYPE"] = DeadStockStatus;
                                                InvoiceTbl.Rows.Add(dtInvRow);
                                                if (ViewState["dtItem1"] == null)
                                                {
                                                    ViewState["dtItem1"] = InvoiceTbl;
                                                }
                                                // }
                                            }
                                            if (ViewState["dtItem1"] != null)
                                            {
                                                dtItemTable = this.CreateInvoiceMultiTable();
                                                dtItemTable = (DataTable)ViewState["dtItem1"];
                                            }
                                            if (InvoiceTbl != null)
                                            {
                                                dtItemTable.Merge(InvoiceTbl);
                                                ViewState["dtItem1"] = dtItemTable;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    objJVEnt.INV_ITEM_TBL = dtItemTable;

                }
            }
            //---------------end------------24-05-2023-----



            if (ddlTranType.SelectedValue == "1") //Issue Item
            {
                IssueItem();
            }
            else if (ddlTranType.SelectedValue == "4") //Consume Item            
            {
                ConsumeItem();
            }
            else
            {
                SMRItem(); //Scrap , Missing , Return , Transfer
            }
            objJVEnt.JV_ITEM_TBL = JVItemsTbl;

            objJVEnt.TRAN_DATE = Convert.ToDateTime(txtTranDate.Text);
            objJVEnt.JVTRAN_TYPE = Convert.ToInt32(ddlTranType.SelectedValue);

            if (ddlTranType.SelectedValue == "3")
            {
                objJVEnt.FROM_COLLEGE = Convert.ToInt32(ddlFromCollege.SelectedValue);
                objJVEnt.FROM_DEPT = Convert.ToInt32(ddlFromDept.SelectedValue);
                objJVEnt.FROM_EMPLOYEE = Convert.ToInt32(ddlFromEmployee.SelectedValue);
            }
            else
            {
                objJVEnt.FROM_COLLEGE = 0;
                //objJVEnt.FROM_DEPT = Convert.ToInt32(Session["SubDeptId"]);Session["strdeptname"]
                objJVEnt.FROM_DEPT = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "MDNO", "MDNAME ='" + Session["strdeptname"].ToString() + "'"));
                objJVEnt.FROM_EMPLOYEE = Convert.ToInt32(objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0)", "UA_NO =" + Convert.ToInt32(Session["userno"])));
            }

            objJVEnt.TO_COLLEGE = Convert.ToInt32(ddlToCollege.SelectedValue);
            objJVEnt.TO_DEPT = Convert.ToInt32(ddlToDept.SelectedValue);
            objJVEnt.TO_EMPLOYEE = Convert.ToInt32(ddlToEmployee.SelectedValue);
            objJVEnt.LOCATIONNO = Convert.ToInt32(ddlLocation.SelectedValue);            //---31/10/2022


            if (ddlTranType.SelectedValue == "3")
            {
                if (objJVEnt.FROM_EMPLOYEE == objJVEnt.TO_EMPLOYEE)
                {
                    DisplayMessage("You Can Not Transfer The Item To The Same Employee.");
                    return;
                }
            }


            objJVEnt.REMARK = txtRemark.Text;
            objJVEnt.STORE_USER_TYPE = ViewState["StoreUser"].ToString();
            objJVEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
            objJVEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);
            objJVEnt.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString() == "add")
                {
                    GenTranSlipNo();
                    objJVEnt.JVTRAN_SLIP_NO = txtTranSlipNum.Text;
                    objJVEnt.JVTRAN_ID = 0;

                    CustomStatus cs = (CustomStatus)objJVCon.AddUpdateJvStock(objJVEnt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        DisplayMessage("Record Saved & Transaction Slip No. Generated Successfully");


                        //=========================04/02/2022=======FOr Sending MAil=======================

                        //if (ddlTranType.SelectedValue == "1")
                        //{
                        //    if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                        //    {
                        //        int TRNO = objJVEnt.REQTRNO;
                        //        DataSet dss = objCommon.FillDropDown("store_req_main", "REQTRNO", "REQ_NO", "REQ_FOR='I' and STAPPROVAL='A' and REQTRNO=" + TRNO, "");
                        //        ViewState["REQ_NO"] = dss.Tables[0].Rows[0]["REQ_NO"].ToString();
                        //        //SendEmailToAuthority(Convert.ToInt32(TRNO));
                        //        SendEmailToRequisitionUser(Convert.ToInt32(TRNO));
                        //        DataSet ds = objCommon.FillDropDown("STORE_JVSTOCK_MAIN M inner join STORE_JVSTOCK_TRAN T  on (M.JVTRAN_ID=T.JVTRAN_ID)", "M.JVTRAN_ID ,M.REQTRNO", "M.ISSUE_TYPE, M.JVTRAN_TYPE", "ISSUE_TYPE='R' and  JVTRAN_TYPE=1 and REQTRNO=" + TRNO, "");

                        //        //int userno = Convert.ToInt32(ds.Tables[0].Rows[0]["UA_NO"]);
                        //        // int idno = Convert.ToInt32(Session["userno"]);
                        //        if (ds.Tables[0].Rows.Count > 0)
                        //        {
                        //            string issuetype = ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString();
                        //            int jvtrantype = Convert.ToInt32(ds.Tables[0].Rows[0]["JVTRAN_type"]);
                        //            if (issuetype == "R" && jvtrantype == 1)
                        //            {
                        //                SendEmailToCentralStoreUser(Convert.ToInt32(TRNO));
                        //            }
                        //        }
                        //    }
                        //}

                        //=======================================04/02=/2022=end========================

                        //FillDropDownList();
                        divSlipNum.Visible = true;
                        btnSubmit.Enabled = false;
                        btnAddNew2.Visible = true;
                        //ClearAll();

                    }
                }
                //else
                //{
                //    SDNO = 0;//Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "SDNO", "REQTRNO=" + Convert.ToInt32(ViewState["REQTRNO"].ToString())));

                //    objJVEnt._JVTRAN_ID = Convert.ToInt32(ddlTranSlipNum.SelectedValue);
                //    CustomStatus cs = (CustomStatus)objJVCon.UpdateIssueDetails(objJVEnt);
                //    if (cs.Equals(CustomStatus.RecordUpdated))
                //    {
                //        ViewState["printissueno"] = Convert.ToInt32(ddlTranSlipNum.SelectedValue);
                //        ddlTranSlipNum.SelectedValue = "0";
                //        txtReqNo.Text = string.Empty;
                //        ViewState["action"] = "add";
                //        ClearAll();
                //        DisplayMessage("Issued Item Updated successfully");
                //    }
                //}
            }
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void SMRItem()
    {
        dtRow = null;
        JVItemsTbl = this.CreateJVItemTable();
        foreach (ListViewItem lv in lvSMRDsr.Items)
        {
            CheckBox chkDsrselect = lv.FindControl("chkSMRDsrItem") as CheckBox;
            HiddenField hdnSMRDsrItemNo = lv.FindControl("hdnSMRDsrItemNo") as HiddenField;

            Label lblSMRDsrNumber = lv.FindControl("lblSMRDsrNumber") as Label;
            TextBox lblSMRDsrRemark = lv.FindControl("lblSMRDsrRemark") as TextBox;
            TextBox txtFineAmt = lv.FindControl("txtFineAmt") as TextBox;
            HiddenField hdnInvDINO = lv.FindControl("hdnSMRDsrInvidno") as HiddenField;
            //HiddenField HiddenBalance = lv.FindControl("HiddenBalance") as HiddenField;
            //TextBox txtAlredyIssue = lv.FindControl("txtAlredyIssue") as TextBox;

            if (chkDsrselect.Checked)
            {
                dtRow = JVItemsTbl.NewRow();
                dtRow["ITEM_NO"] = Convert.ToInt32(hdnSMRDsrItemNo.Value);
                dtRow["ITEM_NAME"] = "";
                dtRow["AVLQTY"] = 0;
                dtRow["JVSTOCK_QTY"] = 1;
                dtRow["DSR_NUMBER"] = lblSMRDsrNumber.Text;
                dtRow["ITEM_REMARK"] = lblSMRDsrRemark.Text;
                dtRow["FINE_AMOUNT"] = txtFineAmt.Text == "" ? 0 : Convert.ToDouble(txtFineAmt.Text);
                dtRow["INVDINO"] = Convert.ToInt32(hdnInvDINO.Value);
                dtRow["ALLREADYISSUEDQTY"] = 0;
                dtRow["BALANCE"] = 0;
                //dtRow["ALLREADYISSUEDQTY"] = Convert.ToInt32(txtAlredyIssue.Text);
                //dtRow["BALANCE"] = Convert.ToInt32(HiddenBalance.Value);
                JVItemsTbl.Rows.Add(dtRow);
            }
        }

    }

    private void ConsumeItem()
    {
        dtRow = null;
        JVItemsTbl = this.CreateJVItemTable();
        foreach (ListViewItem lv in lvConsItem.Items)
        {
            CheckBox chkSelConsItem = lv.FindControl("chkSelConsItem") as CheckBox;
            Label lblSMRDsrNumber = lv.FindControl("lblSMRDsrNumber") as Label;
            Label lblConsAvlQty = lv.FindControl("lblConsAvlQty") as Label;
            TextBox lblConsumeQty = lv.FindControl("lblConsumeQty") as TextBox;
            TextBox lblConsItemRemark = lv.FindControl("lblConsItemRemark") as TextBox;
            //HiddenField hdnInvDINO = lv.FindControl("hdnSMRDsrInvidno") as HiddenField;
            // HiddenField HiddenBalance = lv.FindControl("HiddenBalance") as HiddenField;
            // TextBox txtAlredyIssue = lv.FindControl("txtAlredyIssue") as TextBox;

            if (chkSelConsItem.Checked)
            {
                dtRow = JVItemsTbl.NewRow();
                dtRow["ITEM_NO"] = Convert.ToInt32(chkSelConsItem.ToolTip);
                dtRow["ITEM_NAME"] = "";
                dtRow["AVLQTY"] = Convert.ToInt32(lblConsAvlQty.Text);
                dtRow["JVSTOCK_QTY"] = Convert.ToInt32(lblConsumeQty.Text);
                dtRow["DSR_NUMBER"] = "";
                dtRow["ITEM_REMARK"] = lblConsItemRemark.Text;
                dtRow["FINE_AMOUNT"] = 0;
                dtRow["INVDINO"] = 0;
                dtRow["ALLREADYISSUEDQTY"] = 0;
                //dtRow["ALLREADYISSUEDQTY"] = Convert.ToInt32(txtAlredyIssue.Text);
                //dtRow["BALANCE"] = Convert.ToInt32(HiddenBalance.Value);
                dtRow["BALANCE"] = 0;
                JVItemsTbl.Rows.Add(dtRow);
            }
        }

    }

    private void IssueItem()
    {
        dtRow = null;
        JVItemsTbl = this.CreateJVItemTable();

        foreach (ListViewDataItem item in lvIssueItem.Items)
        {

            CheckBox chkIssueSelect = item.FindControl("chkIssueSelect") as CheckBox;
            Label lblIssueItem = item.FindControl("lblIssueItem") as Label;
            TextBox txtAQty = item.FindControl("txtAQty") as TextBox;
            TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
            TextBox txtAlredyIssue = item.FindControl("txtAlredyIssue") as TextBox;
            TextBox txtIssueItemRemark = item.FindControl("txtIssueItemRemark") as TextBox;
            HiddenField HiddenBalance = item.FindControl("HiddenBalance") as HiddenField;
            string bal = HiddenBalance.Value;
            if (chkIssueSelect.Checked == true)
            {
                string ItemType = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO =" + Convert.ToInt32(chkIssueSelect.ToolTip));
                if (ItemType == "1") // Non Consumable Item
                {
                    foreach (ListViewItem lv in lvDsrIssue.Items)
                    {
                        CheckBox chkDsrselect = lv.FindControl("chkDsrselect") as CheckBox;
                        HiddenField hdnInvidno = lv.FindControl("hdnInvidno") as HiddenField;
                        Label lblDsrNumber = lv.FindControl("lblDsrNumber") as Label;
                        HiddenField hdnIDSRNo = lv.FindControl("hdnInvidno") as HiddenField;
                        if (chkDsrselect.Checked)
                        {
                            dtRow = JVItemsTbl.NewRow();
                            dtRow["ITEM_NO"] = Convert.ToInt32(chkIssueSelect.ToolTip);
                            dtRow["ITEM_NAME"] = lblIssueItem.Text;
                            dtRow["AVLQTY"] = txtAQty.Text == "" ? 0 : Convert.ToInt32(txtAQty.Text);
                            dtRow["JVSTOCK_QTY"] = 1;
                            dtRow["DSR_NUMBER"] = lblDsrNumber.Text;
                            dtRow["ITEM_REMARK"] = txtIssueItemRemark.Text;
                            dtRow["FINE_AMOUNT"] = 0;
                            dtRow["INVDINO"] = Convert.ToInt32(hdnIDSRNo.Value);
                            if (txtAlredyIssue.Text == "")
                            {
                                dtRow["ALLREADYISSUEDQTY"] = 0;
                            }
                            else
                            {
                                dtRow["ALLREADYISSUEDQTY"] = Convert.ToInt32(txtAlredyIssue.Text);
                            }

                            if (HiddenBalance.Value == "")
                            {
                                dtRow["BALANCE"] = 0;
                            }
                            else
                            {
                                dtRow["BALANCE"] = Convert.ToInt32(HiddenBalance.Value);
                            }

                            JVItemsTbl.Rows.Add(dtRow);
                        }
                    }
                }
                else if (ItemType == "2") // Consumable Item
                {
                    dtRow = JVItemsTbl.NewRow();
                    dtRow["ITEM_NO"] = Convert.ToInt32(chkIssueSelect.ToolTip);
                    dtRow["ITEM_NAME"] = lblIssueItem.Text;
                    dtRow["AVLQTY"] = txtAQty.Text == "" ? 0 : Convert.ToInt32(txtAQty.Text);
                    dtRow["JVSTOCK_QTY"] = txtIQ.Text == "" ? 0 : Convert.ToInt32(txtIQ.Text);
                    dtRow["DSR_NUMBER"] = "";
                    dtRow["ITEM_REMARK"] = txtIssueItemRemark.Text;
                    dtRow["FINE_AMOUNT"] = 0;
                    dtRow["INVDINO"] = 0;
                    dtRow["ALLREADYISSUEDQTY"] = txtAlredyIssue.Text == "" ? 0 : Convert.ToInt32(txtAlredyIssue.Text);
                    dtRow["BALANCE"] = HiddenBalance.Value == "" ? 0 : Convert.ToInt32(HiddenBalance.Value);
                    JVItemsTbl.Rows.Add(dtRow);
                }
            }
        }

        //string Invidno = string.Empty;
        //foreach (ListViewItem lv in lvDsrIssue.Items)
        //{
        //    CheckBox chkDsrselect = lv.FindControl("chkDsrselect") as CheckBox;
        //    HiddenField hdnInvidno = lv.FindControl("hdnInvidno") as HiddenField;
        //    if (chkDsrselect.Checked)
        //    {
        //        string invidno = hdnInvidno.Value;
        //        Invidno += invidno + ',';
        //    }
        //}
        //if (Invidno != string.Empty)
        //    Invidno = Invidno.Substring(0, Invidno.Length - 1);        
        //objJVEnt.INVIDNO = Invidno;

        objJVEnt.REQTRNO = Convert.ToInt32(ddlReq.SelectedValue);
        if (rdbDirectIssue.Checked == true)
            objJVEnt.ISSUE_TYPE = 'D';
        if (rdbRequisition.Checked == true)
            objJVEnt.ISSUE_TYPE = 'R';
    }

    private DataTable CreateJVItemTable()
    {
        JVItemsTbl = new DataTable();
        JVItemsTbl.Columns.Add("ITEM_NO", typeof(int));
        JVItemsTbl.Columns.Add("ITEM_NAME", typeof(string));
        JVItemsTbl.Columns.Add("AVLQTY", typeof(int));
        JVItemsTbl.Columns.Add("JVSTOCK_QTY", typeof(int));
        JVItemsTbl.Columns.Add("DSR_NUMBER", typeof(string));
        JVItemsTbl.Columns.Add("ITEM_REMARK", typeof(string));
        JVItemsTbl.Columns.Add("FINE_AMOUNT", typeof(double));
        JVItemsTbl.Columns.Add("INVDINO", typeof(int));
        JVItemsTbl.Columns.Add("ALLREADYISSUEDQTY", typeof(int));
        JVItemsTbl.Columns.Add("BALANCE", typeof(int));
        return JVItemsTbl;
    }

    void clear()
    {
        Response.Redirect(Request.Url.ToString());
    }
    void ClearAll()
    {
        divTranDate.Visible = false;
        divAsset.Visible = false;
        divToFields.Visible = false;
        divIssue.Visible = false;
        divFromFields.Visible = false;
        txtTranSlipNum.Text = string.Empty;
        divSlipNum.Visible = false;

        ddlTranType.SelectedIndex = 0;
        txtTranDate.Text = DateTime.Now.ToString();
        rdbDirectIssue.Checked = false;
        rdbRequisition.Checked = true;
        ddlReq.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        divAddItem.Visible = false;
        lblReqDate.Text = string.Empty;
        lblReqDept.Text = string.Empty;
        lblReqUser.Text = string.Empty;
        ddlToCollege.SelectedValue = "0"; //--21/11/2022
        ddlToDept.SelectedIndex = 0;
        ddlToEmployee.SelectedIndex = 0;

        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        ddlItemSMR.SelectedIndex = 0;

        lvIssueItem.DataSource = null;
        lvIssueItem.DataBind();
        pnlItemDetails.Visible = false;

        lvDsrIssue.DataSource = null;
        lvDsrIssue.DataBind();
        lvDsrIssue.Visible = false;

        lvConsItem.DataSource = null;
        lvConsItem.DataBind();
        lvConsItem.Visible = false;

        lvSMRDsr.DataSource = null;
        lvSMRDsr.DataBind();
        lvSMRDsr.Visible = false;

        divreqDetails.Visible = false;
        divAddItem.Visible = false;
        ddlItemIsue.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;
        divreq.Visible = true;
        ViewState["REQTRNO"] = null;

        ddlFromCollege.SelectedIndex = 0;
        ddlFromDept.SelectedIndex = 0;
        ddlFromEmployee.SelectedIndex = 0;

        ddlLocation.SelectedIndex = 0; //---31/10/2022
        ViewState["dtItem1"] = null;

    }
    void GenTranSlipNo()
    {
        DataSet ds = new DataSet();
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ds = objJVCon.GenrateJvTranSlipNo(Convert.ToInt32(ddlTranType.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtTranSlipNum.Text = Convert.ToString(ds.Tables[0].Rows[0]["TRAN_SLIP_NO"].ToString());
        }
    }

    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1;
        if (Session["usertype"].ToString() != "1")
        {
            test1 = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND APLNO=1");
        }
        else
        {
            test1 = Session["strdeptcode"].ToString();
        }


        // if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        if (test1 == Application["strrefmaindept"].ToString())
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

    private void FillDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlToCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
            ddlToCollege.SelectedValue = "0";        //--21/11/2022
            objCommon.FillDropDownList(ddlToEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS NAME", "", "FNAME");
            objCommon.FillDropDownList(ddlToDept, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");


            objCommon.FillDropDownList(ddlFromCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlFromEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS NAME", "", "FNAME");
            objCommon.FillDropDownList(ddlFromDept, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");

            objCommon.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO <> 0", "MIGNAME");//and ITEM_TYPE='F'            
            objCommon.FillDropDownList(ddlTranType, "STORE_STOCK_TRAN_TYPE", "STOCK_TYPE_ID", "STOCK_TRAN_TYPE", "IS_ACTIVE = 1", "STOCK_TYPE_ID");
            objCommon.FillDropDownList(ddlLocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS = 1", "LOCATIONNO");    //-31/10/2022

            //objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");
            //}

            // }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillRequisition()
    {
        ddlReq.Items.Clear();
        ddlReq.Items.Insert(0, new ListItem("Please Select", "0"));
        DataSet _ds = objJVCon.GetDropDownREQ();

        if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
        {
            ddlReq.DataSource = _ds.Tables[0];
            ddlReq.DataTextField = _ds.Tables[0].Columns["REQ_NO"].ToString();
            ddlReq.DataValueField = _ds.Tables[0].Columns["REQTRNO"].ToString();
            ddlReq.DataBind();

        }
    }

    protected void butModify_Click(object sender, EventArgs e)
    {
        // radInvoicewise.Enabled = false;
        //radItemwise.Enabled = false;
        ddlTranSlipNum.Visible = true;
        txtTranSlipNum.Visible = false;
        ddlReq.Visible = false;
        txtReqNo.Visible = true;
        lvIssueItem.DataSource = null;
        lvIssueItem.DataBind();
        objCommon.FillDropDownList(ddlTranSlipNum, "STORE_ISSUE_ITEM", "ISSUENO", "ISSUE_SLIPNO", "STATUS='P'", "ISSUE_SLIPNO DESC");
        ViewState["action"] = "edit";

    }
    void GetIssueDetailsByIssueNo(int issueno)
    {
        try
        {
            DataSet ds = objJVCon.GetIssueInfoByissueNo(Convert.ToInt32(ddlTranSlipNum.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtReqNo.Text = ds.Tables[0].Rows[0]["REQ_NO"].ToString();
                txtTranDate.Text = ds.Tables[0].Rows[0]["ISSUE_DATE"].ToString();
                //if (ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString() == "0")
                //    radInvoicewise.Checked = true;
                //if (ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString() == "1")
                //    radItemwise.Checked = true;
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                //ViewState["REQTRNO"] = ds.Tables[0].Rows[0]["REQTRNO"].ToString();
            }
            GetIssuedItem(issueno);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.BindListView_ItemDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    void GetIssuedItem(int issueno)
    {
        if (ddlTranSlipNum.SelectedValue != "0")
        {
            pnlItemDetails.Visible = true;

            DataSet ds = (DataSet)objJVCon.GetIssuedItemByIssueNo(issueno);
            if (!ds.Tables[0].Columns.Contains("AQTY"))
                ds.Tables[0].Columns.Add("AQTY");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //ds.Tables[0].Rows[i]["AQTY"] = objJVCon.GetAvaiItemQtyBy(Convert.ToInt32(ds.Tables[0].Rows[i]["ITEM_NO"]), Convert.ToInt32(Session["strdeptcode"]), Convert.ToInt32(ddlInvoice.SelectedValue));
            }

            lvIssueItem.DataSource = ds;
            lvIssueItem.DataBind();

        }
        else
        {
            // radItemwise.Checked = false;
            DisplayMessage("Please Select Issueed Slip no.");
        }
    }
    protected void ddlTranSlipNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTranSlipNum.SelectedValue != "0")
        {
            ClearAll();
            GetIssueDetailsByIssueNo(Convert.ToInt32(ddlTranSlipNum.SelectedValue));
        }
        else
        {
            lvIssueItem.DataSource = null;
            lvIssueItem.DataBind();
            pnlItemDetails.Visible = false;
            txtTranDate.Text = string.Empty;
            txtReqNo.Text = string.Empty;
            txtRemark.Text = string.Empty;
        }
    }
    private void BindListView_ItemDetails()
    {
        DataSet ds = null;
        try
        {
            int ISSUENO = 0;

            if (txtTranSlipNum.Text != null || txtTranSlipNum.Text != "")
                ISSUENO = Convert.ToInt32(objCommon.LookUp("STORE_ISSUE_ITEM", "ISSUENO", "ISSUE_SLIPNO='" + txtTranSlipNum.Text + "'"));
            else
                ISSUENO = 0;

            ds = objJVCon.GetTranDetailsByIssueNo(ISSUENO);
            Session["dtitems"] = ds.Tables[0];
            lvIssueItem.DataSource = ds;
            lvIssueItem.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.BindListView_ItemDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    void GetInvoiceItem()
    {
        objCommon.FillDropDown("STORE_INVOICE_ITEM", "INVINO", "INVNO", "", "INVNO");
    }



    void GetItemsForSelectedReq()
    {
        try
        {
            DataSet ds = null;
            ds = objJVCon.GetItemByReqNo(Convert.ToInt32(ddlReq.SelectedValue));
            lvIssueItem.DataSource = ds;
            lvIssueItem.DataBind();
            pnlItemDetails.Visible = true;
            foreach (ListViewDataItem item in lvIssueItem.Items)
            {
                TextBox txtQty = item.FindControl("txtReqQty") as TextBox;
                //if (radInvoicewise.Checked == true)
                //{
                //    txtQty.Enabled = true;
                //}
                //if (radItemwise.Checked == true)
                //{
                //    txtQty.Enabled = false;
                //}
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.ShowEditDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void ddlReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvDsrIssue.Visible = false;
        DataSet ds = objJVCon.GetItemByReqNo(Convert.ToInt32(ddlReq.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblReqDate.Text = ds.Tables[0].Rows[0]["REQ_DATE"].ToString();
            lblReqDept.Text = ds.Tables[0].Rows[0]["SDNAME"].ToString();
            lblReqUser.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
            ddlToDept.SelectedValue = ds.Tables[0].Rows[0]["SDNO"].ToString();
            ddlToEmployee.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
            divreqDetails.Visible = true;
            lvDsrIssue.Visible = false;
            //lvDsrIssue.Visible = true;

        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                DataSet dsAqty = objJVCon.GetItemAvlQty(Convert.ToInt32(ds.Tables[1].Rows[i]["ITEM_NO"]));
                ds.Tables[1].Rows[i]["AVLQTY"] = dsAqty.Tables[0].Rows[0]["AVLQTY"].ToString();
            }
            pnlItemDetails.Visible = true;
            lvIssueItem.DataSource = ds.Tables[1];
            lvIssueItem.DataBind();
            hdnRowCount.Value = ds.Tables[1].Rows.Count.ToString();


        }
    }

    void DisplayMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    protected void butCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ClearAll();
        ddlFromCollege.SelectedIndex = 0;
        ddlFromDept.SelectedIndex = 0;
        ddlFromEmployee.SelectedIndex = 0;
        btnSubmit.Visible = false;
        //clear();
    }


    protected void butReport_Click(object sender, EventArgs e)
    {
        divIssue.Visible = false;
        pnlItemDetails.Visible = false;
        pnlReport.Visible = true;
        //trIssueButtons.Visible = false;
        objCommon.FillDropDownList(ddlTranSlipNumRpt, "STORE_ISSUE_ITEM", "ISSUENO", "ISSUE_SLIPNO", "issue_from='I' AND MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "ISSUE_SLIPNO DESC");
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
            if (Convert.ToInt32(ddlTranSlipNumRpt.SelectedValue) > 0)
                ViewState["printissueno"] = Convert.ToInt32(ddlTranSlipNumRpt.SelectedValue);

            if (reportTitle == "Issue_Iems_Code")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_ISSUENO=" + ViewState["printissueno"] + "," + "@username=" + Session["userfullname"].ToString();
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_ISSUENO=" + ViewState["printissueno"] + "," + "@username=" + Session["userfullname"].ToString() + "," + "@P_ISSUE_SLIPNO=" + Convert.ToInt32(ddlTranSlipNumRpt.SelectedValue);
            }
            //url += "&param=@P_ISSUENO=" + Convert.ToInt32(ddlTranSlipNumRpt.SelectedValue);
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

    protected void btnCancelRpt_Click1(object sender, EventArgs e)
    {
        divIssue.Visible = true;
        pnlItemDetails.Visible = true;
        pnlReport.Visible = false;
        //trIssueButtons.Visible = true;

    }
    protected void btnPrintRpt_Click(object sender, EventArgs e)
    {
        ShowReportIndividual("IssueItem", "Str_IssueItem_Report_Latest.rpt");
        //ShowReportIndividual("Issue_Iems_Code", "Str_IssueItem_Code_Report.rpt");
    }

    protected void btnCodeRpt_Click(object sender, EventArgs e)
    {
        ShowReportIndividual("Issue_Iems_Code", "Str_IssueItem_Code_Report.rpt");
    }
    protected void rdbRequisition_CheckedChanged(object sender, EventArgs e)
    {
        ClearReqDirect();
        divreq.Visible = true;
        divAddItem.Visible = false;
    }
    protected void rdbDirectIssue_CheckedChanged(object sender, EventArgs e)
    {
        //if (ddlItemIsue.SelectedItem.Text == "Please Select") //Issue Item
        //{
        //    objCommon.DisplayMessage(this.Page, "Please Select Item Name", this);
        //    return;
        //}
        //if (txtItemQty.Text == string.Empty) // Quantity
        //{
        //    objCommon.DisplayMessage(this.Page, "Please Enter  in Quantity", this);
        //    return;
        //}




        ClearReqDirect();
        divreq.Visible = false;
        divAddItem.Visible = true;
        divreqDetails.Visible = false;
        objCommon.FillDropDownList(ddlItemIsue, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
    }

    private void ClearReqDirect()
    {
        ddlReq.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        divAddItem.Visible = false;
        lblReqDate.Text = string.Empty;
        lblReqDept.Text = string.Empty;
        lblReqUser.Text = string.Empty;
        ddlToCollege.SelectedValue = "0";        // 21/11/2022
        ddlToDept.SelectedIndex = 0;
        ddlToEmployee.SelectedIndex = 0;

        lvIssueItem.DataSource = null;
        lvIssueItem.DataBind();
        pnlItemDetails.Visible = false;

        lvDsrIssue.DataSource = null;
        lvDsrIssue.DataBind();
        lvDsrIssue.Visible = false;

        divreqDetails.Visible = false;

        divAddItem.Visible = false;
        ddlItemIsue.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;

        divreq.Visible = true;
    }
    protected void chkIssueSelect_CheckedChanged(object sender, EventArgs e)
    {
        string ItemNo = string.Empty;
        foreach (ListViewItem lv in lvIssueItem.Items)
        {
            CheckBox chkIssueSelect = lv.FindControl("chkIssueSelect") as CheckBox;
            if (chkIssueSelect.Checked)
            {
                string itemno = chkIssueSelect.ToolTip;
                ItemNo += itemno + ',';
            }
            //if (ItemNo != string.Empty)
            //    ItemNo = ItemNo.Substring(0, ItemNo.Length - 1);
            //DataSet dss = objCommon.FillDropDown("STORE_ITEM ", "ITEM_NO,MIGNO", "ITEM_NAME", "ITEM_NO IN (" + ItemNo + ") AND MIGNO = 1", "");
            //if (dss.Tables[0].Rows.Count > 0)
            //{
            //    string itm=dss.Tables[0].Rows[0]["MIGNO"].ToString();

            //    //int itmm = Convert.ToInt32(itm);
            //    if (itm == "1")
            //    {
            //        DisplayMessage("Fixed Item Serial Number is not Generated");

            //    }
            //}

        }
        if (ItemNo != string.Empty)
        {
            ItemNo = ItemNo.Substring(0, ItemNo.Length - 1);
            //=====================for checking item serial no. is generated or not====MIGNO=1 Nonconsumable==
            //string Migno1 = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO IN (" + ItemNo + ") AND MIGNO=1");         
            //if (Migno1 == "1")

            //---------------Enhancement modified the static condition into dynamic--11/05/2023 checking item is consumable or nonconsmble---------------///
            int NonConsMIGNO = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_GROUP", "MIGNO", "ITEM_TYPE='F'"));   //--getting Non consumable MIGNO 
            string Migno1 = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO IN (" + ItemNo + ") AND MIGNO=" + NonConsMIGNO);
            if (Migno1 == NonConsMIGNO.ToString())
            {
                int ret = Convert.ToInt32(objCommon.LookUp("STORE_INVOICE_DSR_ITEM", "Count(*)", "ITEM_NO IN (" + ItemNo + ")"));
                if (ret < 1)
                {
                    objCommon.DisplayMessage(this.Page, "Please Generate Item Serial No. First.", this);
                    return;
                }
            }
            
            //=====================for checking item serial no. is generated or not======end=============================

          //  DataSet ds = objCommon.FillDropDown("STORE_INVOICE_DSR_ITEM A INNER JOIN STORE_ITEM B ON (A.ITEM_NO=B.ITEM_NO)", "INVDINO,SRNO", "DSR_NUMBER,A.ITEM_NO,B.ITEM_NAME", "A.ITEM_NO IN (" + ItemNo + ") AND DSR_NUMBER IS NOT NULL AND B.MIGNO = 1 AND INVDINO NOT IN (SELECT DISTINCT INVDINO FROM STORE_JVSTOCK_TRAN WHERE JVTRAN_ID <> 5)", "");   //31/10/2022
            DataSet ds = objCommon.FillDropDown("STORE_INVOICE_DSR_ITEM A INNER JOIN STORE_ITEM B ON (A.ITEM_NO=B.ITEM_NO)", "INVDINO,SRNO", "DSR_NUMBER,A.ITEM_NO,B.ITEM_NAME", "A.ITEM_NO IN (" + ItemNo + ") AND DSR_NUMBER IS NOT NULL AND B.MIGNO = 1 AND INVDINO NOT IN (SELECT  INVDINO FROM STORE_JVSTOCK_TRAN A inner join STORE_JVSTOCK_MAIN C on (A.JVTRAN_ID=C.JVTRAN_ID) WHERE INVDINO is not null)", "");
            //and A.DSR_NUMBER not in(Select T.DSR_NUMBER from STORE_JVSTOCK_TRAN T where T.DSR_NUMBER=A.DSR_NUMBER)
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDsrIssue.DataSource = ds.Tables[0];
                lvDsrIssue.DataBind();
                hdnDsrRowCount.Value = ds.Tables[0].Rows.Count.ToString();
                lvDsrIssue.Visible = true;
            }
            else
            {
                for (int i = 0; i < ItemNo.Length; i++)
                {
                    //string Migno = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO IN (" + ItemNo + ") AND MIGNO=1");
                    //if (Migno == "1")     
                    //---------------Enhancement modified the static condition into dynamic--11/05/2023 checking item is consumable or nonconsmble--11/05/2023-------------///
                    int NonConsMIGNO1 = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_GROUP", "MIGNO", "ITEM_TYPE='F'"));   //--getting Non consumable MIGNO 
                    string Migno = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO IN (" + ItemNo + ") AND MIGNO=" + NonConsMIGNO);
                    if (Migno == NonConsMIGNO1.ToString())
                    {
                        // objCommon.DisplayMessage(this.Page, "For This Item, Serial Number Is Not Generated", this); 11/05/2023-
                        objCommon.DisplayMessage(this.Page, "Please Generate the Item Serial Number First", this);
                        
                        return;
                    }
                }
                //DisplayMessage("This Item Not  Exist.");
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {


        DataRow drI = null;
        DataTable ItemsTbl = this.CreateJVItemTable();

        foreach (ListViewDataItem item in lvIssueItem.Items)
        {
            CheckBox chkIssueSelect = item.FindControl("chkIssueSelect") as CheckBox;
            Label lblIssueItem = item.FindControl("lblIssueItem") as Label;
            TextBox txtAQty = item.FindControl("txtAQty") as TextBox;
            TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
            TextBox txtRemark = item.FindControl("txtIssueItemRemark") as TextBox;
            HiddenField hdnItemtype = item.FindControl("hdnItemtype") as HiddenField;
            TextBox txtReqQty = item.FindControl("txtReqQty") as TextBox;
            TextBox txtAlredyIssue = item.FindControl("txtAlredyIssue") as TextBox;
            HiddenField HiddenBalance = item.FindControl("HiddenBalance") as HiddenField;

            //if (lblIssueItem.Text == ddlItemIsue.SelectedItem.Text)
            //{

            //    DisplayMessage("This Item Name Already Exist.");
            //    return;
            //}

            drI = ItemsTbl.NewRow();
            drI["ITEM_NO"] = Convert.ToInt32(chkIssueSelect.ToolTip);
            drI["ITEM_NAME"] = lblIssueItem.Text;

            drI["AVLQTY"] = Convert.ToInt32(txtAQty.Text);
            drI["JVSTOCK_QTY"] = Convert.ToInt32(txtIQ.Text);
            drI["DSR_NUMBER"] = "";
            drI["ITEM_REMARK"] = txtRemark.Text;
            if (!ItemsTbl.Columns.Contains("ITEM_TYPE"))
                ItemsTbl.Columns.Add("ITEM_TYPE", typeof(string));
            drI["ITEM_TYPE"] = hdnItemtype.Value;

            if (!ItemsTbl.Columns.Contains("REQ_QTY"))
                ItemsTbl.Columns.Add("REQ_QTY", typeof(int));
            drI["REQ_QTY"] = Convert.ToInt32(txtReqQty.Text);
            drI["ALLREADYISSUEDQTY"] = txtAlredyIssue.Text == "" ? 0 : Convert.ToInt32(txtAlredyIssue.Text);
            drI["BALANCE"] = HiddenBalance.Value == "" ? 0 : Convert.ToInt32(HiddenBalance.Value);
            ItemsTbl.Rows.Add(drI);

        }

        if (IsItemExist(ItemsTbl, ddlItemIsue.SelectedItem.Text.Trim()))
        {
            DisplayMessage("This Item Name Already Exist.");
            return;
        }

        drI = ItemsTbl.NewRow();

        drI["ITEM_NO"] = Convert.ToInt32(ddlItemIsue.SelectedValue);
        drI["ITEM_NAME"] = ddlItemIsue.SelectedItem.Text.Trim();
        DataSet dsAQty = objJVCon.GetItemAvlQty(Convert.ToInt32(ddlItemIsue.SelectedValue));
        drI["AVLQTY"] = dsAQty.Tables[0].Rows[0]["AVLQTY"].ToString();
        drI["JVSTOCK_QTY"] = Convert.ToInt32(txtItemQty.Text);
        drI["DSR_NUMBER"] = "";
        drI["ITEM_REMARK"] = "";
        if (!ItemsTbl.Columns.Contains("ITEM_TYPE"))
            ItemsTbl.Columns.Add("ITEM_TYPE", typeof(string));
        drI["ITEM_TYPE"] = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO=" + ddlItemIsue.SelectedValue);

        if (!ItemsTbl.Columns.Contains("REQ_QTY"))
            ItemsTbl.Columns.Add("REQ_QTY", typeof(int));
        drI["REQ_QTY"] = Convert.ToInt32(txtItemQty.Text);
        //drI["ALLREADYISSUEDQTY"] = Convert.ToInt32(txtAlredyIssue.Text);

        ItemsTbl.Rows.Add(drI);
        ddlItemIsue.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;
        lvIssueItem.DataSource = ItemsTbl;
        lvIssueItem.DataBind();
        pnlItemDetails.Visible = true;
        hdnRowCount.Value = ItemsTbl.Rows.Count.ToString();
    }

    // It is used to check duplicate Item name. 
    private bool IsItemExist(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_NAME"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_GRN_Entry.IsItemExist()  -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divTranDate.Visible = true;
        divFromFields.Visible = false;
        if (ddlTranType.SelectedValue == "4")
        {
            ddlCategory.SelectedValue = "2";
            objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=" + ddlCategory.SelectedValue, "MISGNAME");
        }
        else
        {
            ddlCategory.SelectedValue = "1";
            objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=" + ddlCategory.SelectedValue, "MISGNAME");
        }

        if (ddlTranType.SelectedValue == "1")
        {
            lblTranDate.Text = "Issue Date :";
            divIssue.Visible = true;
            divAsset.Visible = false;
            divStore.Visible = false;
            divToFields.Visible = true;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
            FillRequisition();
        }
        else if (ddlTranType.SelectedValue == "2")
        {
            lblTranDate.Text = "Scrap Date :";
            divAsset.Visible = true;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "Scrap Item";
            divStore.Visible = false;
            divToFields.Visible = false;
            divItem.Visible = true;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
        }
        else if (ddlTranType.SelectedValue == "3")
        {
            lblTranDate.Text = "Transfer Date :";
            divAsset.Visible = true;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "Transfer Item";
            divStore.Visible = false;
            divFromFields.Visible = true;
            divToFields.Visible = true;
            divItem.Visible = true;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
        }
        else if (ddlTranType.SelectedValue == "4")
        {
            lblTranDate.Text = "Consume Date :";
            divAsset.Visible = true;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "Consume Item";

            divStore.Visible = false;
            divToFields.Visible = false;
            divItem.Visible = false;


            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
        }
        else if (ddlTranType.SelectedValue == "5")
        {
            lblTranDate.Text = "Missing Date :";
            divAsset.Visible = true;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "Missing Item";
            divStore.Visible = false;
            divToFields.Visible = true;
            divItem.Visible = true;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
        }
        else if (ddlTranType.SelectedValue == "6")
        {
            lblTranDate.Text = "Return Date :";
            divAsset.Visible = true;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "Return Item";
            divStore.Visible = false;
            divToFields.Visible = true;
            divItem.Visible = true;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
        }
        else if (ddlTranType.SelectedValue == "7")
        {
            lblTranDate.Text = "Damage Date :";
            divAsset.Visible = true;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "Damage Item";
            divStore.Visible = false;
            divToFields.Visible = true;
            divItem.Visible = true;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;
        }
        else
        {
            divAsset.Visible = false;
            divIssue.Visible = false;
            lblScrpOrTransfer.Text = "";
            divStore.Visible = false;
            divToFields.Visible = false;

            lvConsItem.DataSource = null;
            lvConsItem.DataBind();
            lvConsItem.Visible = false;
            divItem.Visible = false;

            lvSMRDsr.DataSource = null;
            lvSMRDsr.DataBind();
            lvSMRDsr.Visible = false;

            divTranDate.Visible = false;
        }
        btnSubmit.Visible = true;
        btnSubmit.Enabled = true;
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=" + ddlCategory.SelectedValue, "MISGNAME");
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubCategory.SelectedItem.Text == "Please Select")
        {
            lvSMRDsr.Visible = false;
        }
        DataSet ds = null;
        if (ddlTranType.SelectedValue == "4")
        {
            ds = objJVCon.GetConsumableItemDetBySubCategory(Convert.ToInt32(ddlSubCategory.SelectedValue));//objCommon.FillDropDown("STORE_ITEM", "ITEM_NO", "ITEM_NAME,10 AS AVLQTY", "MISGNO="+ddlSubCategory.SelectedValue, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvConsItem.DataSource = ds.Tables[0];
                lvConsItem.DataBind();
                hdnConsDsrRowCount.Value = ds.Tables[0].Rows.Count.ToString();
                lvConsItem.Visible = true;
                lvSMRDsr.Visible = false;
            }
            else
            {
                lvConsItem.DataSource = null;
                lvConsItem.DataBind();
                hdnConsDsrRowCount.Value = "0";
                lvConsItem.Visible = false;
                lvSMRDsr.Visible = false;
            }

        }
        else
        {
            objCommon.FillDropDownList(ddlItemSMR, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        }
    }
    protected void ddlItemSMR_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlItemSMR.SelectedItem.Text == "Please Select")
        {
            lvSMRDsr.Visible = false;
        }

        DataSet ds = null;
        if (ddlTranType.SelectedValue != "4")
        {
            if (ddlTranType.SelectedValue == "2")
                ds = objCommon.FillDropDown("STORE_INVOICE_DSR_ITEM I", "INVDINO,SRNO", "I.DSR_NUMBER,ITEM_NO", "I.DSR_NUMBER IS NOT NULL and I.DSR_NUMBER not  in (SELECT DSR_NUMBER FROM STORE_JVSTOCK_TRAN A INNER JOIN STORE_JVSTOCK_MAIN B ON (A.JVTRAN_ID=B.JVTRAN_ID) WHERE JVTRAN_TYPE IN (2,5))  AND ITEM_NO=" + ddlItemSMR.SelectedValue, "");
            else
                ds = objCommon.FillDropDown("STORE_INVOICE_DSR_ITEM I", "INVDINO,SRNO", "I.DSR_NUMBER,ITEM_NO", "I.DSR_NUMBER IS NOT NULL and I.DSR_NUMBER not  in (SELECT DSR_NUMBER FROM STORE_JVSTOCK_TRAN A INNER JOIN STORE_JVSTOCK_MAIN B ON (A.JVTRAN_ID=B.JVTRAN_ID) WHERE JVTRAN_TYPE IN (5))  AND ITEM_NO=" + ddlItemSMR.SelectedValue, "");


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSMRDsr.DataSource = ds.Tables[0];
                lvSMRDsr.DataBind();
                hdnSMRRowCount.Value = ds.Tables[0].Rows.Count.ToString();
                if (ddlTranType.SelectedValue == "2" || ddlTranType.SelectedValue == "3" || ddlTranType.SelectedValue == "6")
                    foreach (ListViewItem lv in lvSMRDsr.Items)
                    {
                        TextBox txtFineAmt = lv.FindControl("txtFineAmt") as TextBox;
                        txtFineAmt.Enabled = false;
                    }
                lvSMRDsr.Visible = true;
                lvConsItem.Visible = false;
            }
            else
            {
                lvSMRDsr.DataSource = null;
                lvSMRDsr.DataBind();
                lvSMRDsr.Visible = false;
                lvConsItem.Visible = false;
                objCommon.DisplayMessage(this.Page, "No Records Found", this);
            }
        }

    }
    protected void btnAddNew2_Click(object sender, EventArgs e)
    {
        ClearAll();
        btnAddNew2.Visible = false;
        btnSubmit.Enabled = true;
        ddlFromCollege.SelectedIndex = 0;
        ddlFromDept.SelectedIndex = 0;
        ddlFromEmployee.SelectedIndex = 0;
        //pnlJVEntry.Visible = true;
        //divSlipNum.Visible = true;
        ViewState["dtItem1"] = null;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
    }


    //04/02/2022
    #region
    // it is used to send email to Requisition User.
    private void SendEmailToRequisitionUser(int TRNO)
    {
        try
        {
            STR_DEPT_REQ_CONTROLLER ObjReq = new STR_DEPT_REQ_CONTROLLER();
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = ObjReq.GetDataForEmailToRequisitionUser(TRNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                body = "The above requisition Item is Issued.";

                //if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
                //{
                //    body = "The above requisition is approved.";
                //}
                //else
                //{
                //    //body = "The above requisition is rejected by the approval authority.";
                //    body = "The above requisition has been Rejected /Cancelled by the approval authority," + "<br />" + "To resend the proposal again using the same requisition number use the below link," + "<br />" + " Link :  Stores >> Transaction >> Department Proposal.";
                //}
                sendmail(fromEmailId, fromEmailPwd, receiver, "New Requisition", body, Session["userfullname"].ToString());

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void SendEmailToAuthority(int TRNO)
    {
        try
        {
            STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
            StoreMasterController objApp = new StoreMasterController();
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;
            string body = string.Empty;
            char approved = 'A';  //04/02/2022
            DataSet dss = objApp.GetHighiestApprovalAuthrity(Convert.ToInt32(TRNO));  //04/02/2022
            int userno = Convert.ToInt32(dss.Tables[0].Rows[0]["UA_NO"]);   //04/02/2022
            DataSet ds = objDeptReqController.GetNextAuthorityForSendingEmail(TRNO, userno, approved);////04/02/2022

            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                body = "The above requisition Item is Issued.";
                //if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
                //{
                //    body = "The above requisition is approved and sent it to your further approval.";
                //}
                //else
                //{
                //    body = "The above requisition is rejected successfully.";

                //}
                sendmail(fromEmailId, fromEmailPwd, receiver, "Requisition Approval", body, Session["userfullname"].ToString());

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string username)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;
            string ReqSlipNo = string.Empty;
            //  DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");

            if (ViewState["REQ_NO"] != null)  //04/02/2022
            {
                ReqSlipNo = ViewState["REQ_NO"].ToString();  //04/02/2022
            }

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(System.Web.HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new System.Text.StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />Requisition Slip No. : " + ReqSlipNo);
            //if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
            //{
            //    MailBody.AppendLine(@"<br />is approved and send it to you for further approval.");
            //}
            //else
            //{
            //    MailBody.AppendLine(@"<br />is rejected by the approval authority.");
            //}
            MailBody.AppendLine(@"<br /> " + body);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            // MailBody.AppendLine(@"<br />" + Session["userfullname"].ToString());
            MailBody.AppendLine(@"<br />" + username);


            mailMessage.Body = MailBody.ToString();

            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new System.Net.NetworkCredential(System.Web.HttpUtility.HtmlEncode(fromEmailId), System.Web.HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mailMessage);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void SendEmailToCentralStoreUser(int TRNO)
    {
        try
        {
            STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;
            string body = string.Empty;
            char approved = 'A';
            DataSet ds = objDeptReqController.GetCentralStoreUserDataForSendingEmail(TRNO, Convert.ToInt32(Session["userno"]), approved);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                body = "The above requisition Item Issued .";
                //if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
                //{
                //    body = "The above requisition is approved and sent it to your further approval.";
                //}
                //else
                //{
                //    body = "The above requisition is rejected successfully.";

                //}
                sendmail(fromEmailId, fromEmailPwd, receiver, "Requisition Approval", body, Session["userfullname"].ToString());

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }




    #endregion

    //04/02/2022

    //protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    foreach (ListViewDataItem lvitem in lvDsrIssue.Items)
    //    {
    //        CheckBox chk = lvitem.FindControl("Check") as CheckBox;
    //        CheckBox chkDsrSel = lvitem.FindControl("chkDsrselect") as CheckBox;

    //        if (chk.Checked == true)
    //        {
    //            chkDsrSel.Checked = true;
    //        }
    //    }
    //}
}