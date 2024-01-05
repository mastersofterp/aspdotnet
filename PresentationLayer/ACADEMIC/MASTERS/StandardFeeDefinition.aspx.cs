//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STANDARD FEE DEFINITION (USER CONTROL)
// CREATION DATE : 15-MAY-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.IO;

public partial class Payments_StandardFeeDefinition : System.Web.UI.Page
{
    Common _objCommon = new Common();
    UAIMS_Common _objUaimsCommon = new UAIMS_Common();

    /// This object is used to store reciept_code, degree_no, Batch_no and 
    /// payment_type_no for new fees item    
    StandardFee _newFeeItem = new StandardFee();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            _objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            _objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    ////Load Page Help
                    //if (Request.QueryString["pageno"] != null)
                    //{
                    //    lblHelp.Text = _objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //}

                    // Set form action as add
                    ViewState["action"] = "add";

                    // Fill Dropdown lists
                    this._objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "");//"RECIEPT_CODE IN('TF','HF')", "");
                    this._objCommon.FillDropDownList(ddlReceiptTypeRbd, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "");
                    ////this._objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

                    //this._objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                    ////this._objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    ////this._objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");


                    this._objCommon.FillDropDownList(ddlRecieptCopy, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "");//"RECIEPT_CODE IN('TF','HF')", "");
                    this._objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    this._objCommon.FillDropDownList(ddlBatchCopy, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    this._objCommon.FillDropDownList(ddlAdmissionBatchFrom, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

                    /// Fill listbox.
                    /// Onfirst time page loading, we need all fee items in listbox hence send no values 
                    /// in parameters not to filter records.
                    this.FillListbox(string.Empty, 0, 0, 0, 0, 0);

                    this.btnSubmit.Enabled = false;
                }
                _objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label
            }
            else
                this.divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StandardFeeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StandardFeeDefinition.aspx");
        }
    }
    #endregion

    #region Load and Reloading Listbox
    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            ddlSchClg.Items.Clear();
            ddlSchClg.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
            lv.DataSource = null;
            lv.DataBind();
            lv.Visible = false;
            lblFeeName.Text = "";
            if (ddlReceiptType.SelectedIndex > 0)
            {
                this._objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                ddlSchClg.Focus();
            }
            else
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Please select receipt type.", Page);
                ddlReceiptType.Focus();
            }
            this.Call_FillListbox();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlReceiptType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            ////this.Call_FillListbox();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
            lv.DataSource = null;
            lv.DataBind();
            lv.Visible = false;
            lblFeeName.Text = "";
            btnSubmit.Enabled = false;
            if (ddlDegree.SelectedIndex > 0)
            {
                _objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 and B.DEGREENO=" + ddlDegree.SelectedValue + " and B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.LONGNAME");
                this.Call_FillListbox();
            }
            else
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Please select degree.", Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////this.Call_FillListbox();
        try
        {
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
            lv.DataSource = null;
            lv.DataBind();
            lblFeeName.Text = "";
            btnSubmit.Enabled = false;
            if (ddlBranch.SelectedIndex > 0)
            {
                this._objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
                ddlPaymentType.Focus();
                this.Call_FillListbox();
            }
            else
            {
                _objCommon.DisplayMessage("Please select admission batch.", Page);
                ddlBatch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlBatch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            ////this.Call_FillListbox();
            lv.DataSource = null;
            lv.DataBind();
            lblFeeName.Text = "";
            btnSubmit.Enabled = false;
            if (ddlPaymentType.SelectedIndex > 0)
            {
                this.Call_FillListbox();
            }
            else
            {
                _objCommon.DisplayMessage("Please select payment type.", Page);
                ddlPaymentType.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlPaymentType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

    private void Call_FillListbox()
    {
        try
        {
            // Call FillListbox() method by passing appropriate values
            ////this.FillListbox(
            ////    (this.GetDropDownListValue(ddlReceiptType) != null ? ddlReceiptType.SelectedValue : string.Empty),
            ////    (this.GetDropDownListValue(ddlDegree) != null ? Int32.Parse(ddlDegree.SelectedValue) : 0),
            ////    (this.GetDropDownListValue(ddlBatch) != null ? Int32.Parse(ddlBatch.SelectedValue) : 0),
            ////    (this.GetDropDownListValue(ddlPaymentType) != null ? Int32.Parse(ddlPaymentType.SelectedValue) : 0)
            ////    );
            this.FillListbox(
               (this.GetDropDownListValue(ddlReceiptType) != null ? ddlReceiptType.SelectedValue : string.Empty),
               (this.GetDropDownListValue(ddlSchClg) != null ? Int32.Parse(ddlSchClg.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlDegree) != null ? Int32.Parse(ddlDegree.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlBranch) != null ? Int32.Parse(ddlBranch.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlBatch) != null ? Int32.Parse(ddlBatch.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlPaymentType) != null ? Int32.Parse(ddlPaymentType.SelectedValue) : 0)
               );
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Call_FillListbox-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    ////private void FillListbox(string recieptCode, int degreeNo, int batchNo, int payTypeNo)
    ////{
    ////    try
    ////    {
    ////        StandardFeeController feeController = new StandardFeeController();
    ////        lstFeesItems.DataSource = feeController.GetFeesItemsForListbox(recieptCode, degreeNo, batchNo, payTypeNo);
    ////        lstFeesItems.DataTextField = "FEE_DESCRIPTION";
    ////        lstFeesItems.DataValueField = "FEE_CAT_NO";
    ////        lstFeesItems.DataBind();
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw;
    ////    }
    ////}

    private void FillListbox(string recieptCode, int collegeId, int degreeNo, int branchno, int batchNo, int payTypeNo)
    {
        try
        {
            StandardFeeController feeController = new StandardFeeController();
            ////lstFeesItems.DataSource = feeController.GetFeesItemsForListbox(recieptCode, degreeNo, batchNo, payTypeNo);
            lstFeesItems.DataSource = feeController.GetFeesItemsForListbox(recieptCode, collegeId, degreeNo, branchno, batchNo, payTypeNo);
            lstFeesItems.DataTextField = "FEE_DESCRIPTION";
            lstFeesItems.DataValueField = "FEE_CAT_NO";
            lstFeesItems.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.FillListbox-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Fill Fee Entry Grid

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptType.SelectedIndex > 0 && ddlSchClg.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0 &&
                ddlBatch.SelectedIndex > 0 && ddlPaymentType.SelectedIndex > 0)
            {
                this.DisplayFeeName();
                this.DisplayFeeDefinitionGrid();
                this.btnSubmit.Enabled = true;
                /// Refill listbox.
                /// while reloading, we need all fee items in listbox hence send no values 
                /// in parameters not to filter records.
                ////***this.FillListbox(string.Empty, 0, 0, 0);
                this.FillListbox(string.Empty, 0, 0, 0, 0, 0);
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                btnSubmitCopy.Visible = false;
                btnCancelCopy.Visible = false;

                idFeesName.Visible = true;
                pnlCopy.Visible = false;
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('Please select complete criteria.');</script>");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lstFeesItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblFeeName.Text = lstFeesItems.SelectedItem.Text;
            StandardFeeController feeController = new StandardFeeController();
            DataSet ds = feeController.GetFeeDefinition(lstFeesItems.SelectedValue);
            ViewState["STD"] = ds;
            if (ds.Tables[0].Rows.Count > 0)//********
            {
                this._objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "");//"RECIEPT_CODE IN('TF','HF')", "");
                ddlReceiptType.SelectedValue = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                this._objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                ddlSchClg.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                this.ddlSchClg_SelectedIndexChanged(sender, e);
                this._objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.DEGREENAME");
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                _objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 and B.DEGREENO=" + ddlDegree.SelectedValue + " ", "A.LONGNAME");//**********
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();//***********
                this._objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");//*********
                ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                this._objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
                ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["PAYTYPENO"].ToString();
                this.BindListView(ds);
                this.btnSubmit.Enabled = true;
                this.btnSubmit.Visible = true;
                this.btnCancel.Visible=true;
                pnlCopy.Visible = false;
                btnSubmitCopy.Visible =  false;
                btnSubmitCopyDiv.Visible = false;
                btnCancelCopy.Visible = false;
                rdbCopyStanderdFees.SelectedIndex = 0;
                //this.DisplayFeeName();
                idFeesName.Visible = true;
                this.DisplayFeeName();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.lstFeesItems_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void DisplayFeeDefinitionGrid()
    {
        try
        {
            StandardFeeController feeController = new StandardFeeController();
            ////DataSet ds = feeController.GetFeeDefinition(ddlReceiptType.SelectedValue, ddlSchClg.SelectedValue, ddlDegree.SelectedValue, ddlBatch.SelectedValue, ddlPaymentType.SelectedValue);
            DataSet ds = feeController.GetFeeDefinition(ddlReceiptType.SelectedValue, ddlSchClg.SelectedValue, ddlDegree.SelectedValue, ddlBranch.SelectedValue, ddlBatch.SelectedValue, ddlPaymentType.SelectedValue);
            ViewState["STD"] = ds;
            if (ds.Tables[0].Rows.Count > 0)//********
            {
                this.BindListView(ds);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.DisplayFeeDefinitionGrid-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView(DataSet ds)
    {
        try
        {
            this.lv.DataSource = ds;
            this.lv.DataBind();
            this.ShowFeeTotal();
            lv.Visible = true;
            btnSubmit.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //NOT IN USED METHOD
    private void ShowFeeTotal()
    {
        try
        {
            bool isFirstFeeItem = true;
            foreach (ListViewDataItem item in lv.Items)
            {
                for (int sem = 1; sem <= 10; sem++)
                {
                    string strFeeItemAmt = (item.FindControl("txtSem" + sem.ToString()) as TextBox).Text.Trim();
                    double feeItemAmt = (strFeeItemAmt != null && strFeeItemAmt != string.Empty) ? double.Parse(strFeeItemAmt) : 0;

                    string strTotalFeeItemAmt = (lv.FindControl("txtSem" + sem.ToString() + "TotalAmt") as TextBox).Text.Trim();
                    double totalFeeItemAmt = (strTotalFeeItemAmt != null && strTotalFeeItemAmt != string.Empty) ? double.Parse(strTotalFeeItemAmt) : 0;

                    if (isFirstFeeItem)
                        totalFeeItemAmt = feeItemAmt;
                    else
                        totalFeeItemAmt += feeItemAmt;

                    (lv.FindControl("txtSem" + sem.ToString() + "TotalAmt") as TextBox).Text = totalFeeItemAmt.ToString();
                }

                isFirstFeeItem = false;
            }
            this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmounts(); </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ShowFeeTotal-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Save Data

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            /// This variable is used to store and maintain FeeCatNo generated in the stored procedure 
            /// while inserting data for F1 fee head in case of new standard fee record 
            int feeCatNo = 0;

            ////foreach (ListViewDataItem dataRow in lv.Items)
            ////{
            ////    feeCatNo = this.UpdateStandardFeeItem(dataRow, feeCatNo);
            ////}

            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            DataSet ds = (DataSet)ViewState["STD"];
            DataTable dt =(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0) ? ds.Tables[0] :null;

           //string Condition="RECIEPT_CODE='"+ddlReceiptType.SelectedValue+"' AND PAYTYPENO="+ddlPaymentType.SelectedValue+" AND CUR_NO=1";

           int curno = Convert.ToInt32(ds.Tables[0].Rows[0]["CUR_NO"].ToString().Trim());

            string Condition="RECIEPT_CODE='"+ddlReceiptType.SelectedValue+"' AND PAYTYPENO="+ddlPaymentType.SelectedValue+" AND CUR_NO="+ curno ;

            if (dt != null && dt.Rows.Count > 0 && (dt.Select(Condition).Length == 0))
            {
                _objCommon.DisplayMessage(this.updProg, "Did not found the Currency Mapping for " + ddlReceiptType.SelectedItem.Text + " and Payment Type " + ddlPaymentType.SelectedItem.Text + ".Kindly do the Currency Mapping.", this.Page);
                return;
            }

            foreach (ListViewDataItem dataRow in lv.Items)
            {
                feeCatNo = this.UpdateStandardFeeItem(dataRow, feeCatNo);
                if (feeCatNo == -99) // if error occurred
                {
                    break;
                }
            }
            if (feeCatNo > 0)
            {
                //_objCommon.DisplayMessage(pnlFeeTable, "Record saved successfully.", Page);
                _objCommon.DisplayMessage(pnlFeeTable, "Record saved successfully ", Page);
                //this.FillListbox(string.Empty, 0, 0, 0, 0);
            }
            else if (feeCatNo == -99)
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Error occurred!", Page);
            }
            DisplayFeeDefinitionGrid();
            this.FillListbox(string.Empty, 0, 0, 0, 0, 0);

            /// Refill listbox.
            /// while reloading, we need all fee items in listbox hence send no values 
            /// in parameters not to filter records.
            ////********this.FillListbox(string.Empty, 0, 0, 0, 0);
            ////********_objCommon.DisplayMessage(pnlFeeTable, "Record saved successfully.", Page);

            //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('Record saved successfully.'); </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private int UpdateStandardFeeItem(ListViewDataItem dataRow, int feeCatNo)
    {
        int output = 0;//*********
        StandardFee feeItem = new StandardFee();
        try
        {
            string feeCatNum = ((HiddenField)dataRow.FindControl("hidFeeCatNo")).Value;
            feeItem.FeeCatNo = (feeCatNum != null && feeCatNum != string.Empty) ? Convert.ToInt32(feeCatNum) : 0;

            feeItem.FeeHead = ((HiddenField)dataRow.FindControl("hidFeeHead")).Value;
            feeItem.FeeDesc = lblFeeName.Text; //((HiddenField)dataRow.FindControl("hidFeeDesc")).Value;
            feeItem.RecieptCode = ((HiddenField)dataRow.FindControl("hidRecieptCode")).Value;

            //string currencyNo = ((Label)dataRow.FindControl("lblCurrency")).Text;
            //feeItem.CurrencyNo = ((HiddenField)dataRow.FindControl("hidCurrency")).Value;
            string srNo = ((HiddenField)dataRow.FindControl("hidSrNo")).Value;
            feeItem.SerialNo = (srNo != null && srNo != string.Empty) ? Convert.ToInt32(srNo) : 0;

            string batchNo = ((HiddenField)dataRow.FindControl("hidBatchNo")).Value;
            feeItem.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue); //(batchNo != null && batchNo != string.Empty) ? Convert.ToInt32(batchNo) : 0;

            string degreeNo = ((HiddenField)dataRow.FindControl("hidDegreeNo")).Value;
            feeItem.DegreeNo = (degreeNo != null && degreeNo != string.Empty) ? Convert.ToInt32(degreeNo) : 0;

            string branchNo = ((HiddenField)dataRow.FindControl("hidBranchNo")).Value;//********************
            feeItem.Branchno = (branchNo != null && branchNo != string.Empty) ? Convert.ToInt32(branchNo) : 0;//**********************

            string payTypeNo = ((HiddenField)dataRow.FindControl("hidPaymentNo")).Value;
            feeItem.PaymentTypeNo = Convert.ToInt32(ddlPaymentType.SelectedValue); //(payTypeNo != null && payTypeNo != string.Empty) ? Convert.ToInt32(payTypeNo) : 0;

            string sem1_Fee = ((TextBox)dataRow.FindControl("txtSem1")).Text.Trim();
            feeItem.Sem1_Fee = (sem1_Fee != null && sem1_Fee != string.Empty) ? Convert.ToDouble(sem1_Fee) : 0;

            string sem2_Fee = ((TextBox)dataRow.FindControl("txtSem2")).Text.Trim();
            feeItem.Sem2_Fee = (sem2_Fee != null && sem2_Fee != string.Empty) ? Convert.ToDouble(sem2_Fee) : 0;

            string sem3_Fee = ((TextBox)dataRow.FindControl("txtSem3")).Text.Trim();
            feeItem.Sem3_Fee = (sem3_Fee != null && sem3_Fee != string.Empty) ? Convert.ToDouble(sem3_Fee) : 0;

            string sem4_Fee = ((TextBox)dataRow.FindControl("txtSem4")).Text.Trim();
            feeItem.Sem4_Fee = (sem4_Fee != null && sem4_Fee != string.Empty) ? Convert.ToDouble(sem4_Fee) : 0;

            string sem5_Fee = ((TextBox)dataRow.FindControl("txtSem5")).Text.Trim();
            feeItem.Sem5_Fee = (sem5_Fee != null && sem5_Fee != string.Empty) ? Convert.ToDouble(sem5_Fee) : 0;

            string sem6_Fee = ((TextBox)dataRow.FindControl("txtSem6")).Text.Trim();
            feeItem.Sem6_Fee = (sem6_Fee != null && sem6_Fee != string.Empty) ? Convert.ToDouble(sem6_Fee) : 0;

            string sem7_Fee = ((TextBox)dataRow.FindControl("txtSem7")).Text.Trim();
            feeItem.Sem7_Fee = (sem7_Fee != null && sem7_Fee != string.Empty) ? Convert.ToDouble(sem7_Fee) : 0;

            string sem8_Fee = ((TextBox)dataRow.FindControl("txtSem8")).Text.Trim();
            feeItem.Sem8_Fee = (sem8_Fee != null && sem8_Fee != string.Empty) ? Convert.ToDouble(sem8_Fee) : 0;

            string sem9_Fee = ((TextBox)dataRow.FindControl("txtSem9")).Text.Trim();
            feeItem.Sem9_Fee = (sem9_Fee != null && sem9_Fee != string.Empty) ? Convert.ToDouble(sem9_Fee) : 0;

            string sem10_Fee = ((TextBox)dataRow.FindControl("txtSem10")).Text.Trim();
            feeItem.Sem10_Fee = (sem10_Fee != null && sem10_Fee != string.Empty) ? Convert.ToDouble(sem10_Fee) : 0;

            feeItem.CollegeCode = Session["colcode"].ToString();

            /// If degree no, batch no and payment type is equal to 0 then record is in new mode.
            /// In new mode assign values of their respective drop down list.
            ////if (feeItem.DegreeNo == 0 && feeItem.BatchNo == 0 && feeItem.PaymentTypeNo == 0)
            if (feeItem.DegreeNo == 0 && feeItem.Branchno == 0 && feeItem.BatchNo == 0 && feeItem.PaymentTypeNo == 0)//************
            {
                /// If degree no, batch no and payment type is not defined for fee head 'F1' 
                /// then this record is a new standard fee record.
                if (feeItem.FeeHead == "F1")
                {
                    feeItem.RecieptCode = ddlReceiptType.SelectedValue;
                    feeItem.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                    feeItem.Branchno = Convert.ToInt32(ddlBranch.SelectedValue);//**********
                    feeItem.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
                    feeItem.PaymentTypeNo = Convert.ToInt32(ddlPaymentType.SelectedValue);
                    feeItem.FeeCatNo = feeCatNo;
                    feeItem.FeeDesc = lblFeeName.Text;
                }
                else
                {
                    /// this record can be a new standard fee record or new fee item in predefined std. fees.
                    feeItem.RecieptCode = _newFeeItem.RecieptCode;
                    feeItem.DegreeNo = _newFeeItem.DegreeNo;
                    feeItem.Branchno = _newFeeItem.Branchno; //**********
                    feeItem.BatchNo = _newFeeItem.BatchNo;
                    feeItem.PaymentTypeNo = _newFeeItem.PaymentTypeNo;
                    feeItem.FeeDesc = _newFeeItem.FeeDesc;
                    feeItem.FeeCatNo = _newFeeItem.FeeCatNo;
                }
            }

            feeItem.CollegeId = Convert.ToInt32(ddlSchClg.SelectedValue);

            // Initialize newFeeItem object to contain basic unique identifying data.
            if (feeItem.FeeHead == "F1") _newFeeItem = feeItem;

            StandardFeeController feeController = new StandardFeeController();
            ////feeController.MaintainStandardFeeItem(ref feeItem);
            output = feeController.MaintainStandardFeeItem(ref feeItem);
            if (output == -99)
            {
                return output;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.UpdateStandardFeeItem-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
        return feeItem.FeeCatNo;
        //return output;//**************
    }

    #endregion

    private void DisplayFeeName()
    {
        ////string Coll_Code = _objCommon.LookUp("ACD_COLLEGE_MASTER", "+'('+ SHORT_NAME+ '-' +code + ')'   as College_code", "COLLEGE_ID=" + ddlSchClg.SelectedValue);
        ////lblFeeName.Text = ddlDegree.SelectedItem.Text + " ";
        ////lblFeeName.Text += Coll_Code + "";
        ////lblFeeName.Text += ddlBatch.SelectedItem.Text + " ";
        ////lblFeeName.Text += ddlPaymentType.SelectedItem.Text + " ";
        ////lblFeeName.Text += ddlReceiptType.SelectedItem.Text;
        string Coll_Code = _objCommon.LookUp("ACD_COLLEGE_MASTER", "+'('+ SHORT_NAME+')'   as College_code", "COLLEGE_ID=" + ddlSchClg.SelectedValue);
        string branchname = _objCommon.LookUp("ACD_BRANCH", "SHORTNAME", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue));
        lblFeeName.Text = ddlDegree.SelectedItem.Text + " ";
        lblFeeName.Text += branchname + " ";//************
        lblFeeName.Text += Coll_Code + "";
        lblFeeName.Text += ddlBatch.SelectedItem.Text + " ";
        lblFeeName.Text += ddlPaymentType.SelectedItem.Text + " ";
        lblFeeName.Text += ddlReceiptType.SelectedItem.Text;
    }

    private string GetDropDownListValue(DropDownList ddl)
    {
        return (ddl.SelectedIndex > 0) ? ddl.SelectedValue : null;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBatch.Items.Count > 0)
                ddlBatch.SelectedIndex = 0;

            if (ddlDegree.Items.Count > 0)
                ddlDegree.SelectedIndex = 0;

            if (ddlBranch.Items.Count > 0)
                ddlBranch.SelectedIndex = 0;

            if (ddlPaymentType.Items.Count > 0)
                ddlPaymentType.SelectedIndex = 0;

            if (ddlReceiptType.Items.Count > 0)
                ddlReceiptType.SelectedIndex = 0;

            if (ddlSchClg.Items.Count > 0)
                ddlSchClg.SelectedIndex = 0;
            lblFeeName.Text = string.Empty;

            /// while refreshing page we need all fee items in listbox hence send no values 
            /// in parameters to filter records.
            this.FillListbox(string.Empty, 0, 0, 0, 0, 0);

            this.lv.Visible = false;
            this.btnSubmit.Enabled = false;
            idFeesName.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSchClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Call_FillListbox();
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlPaymentType.Items.Clear();
        ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        lv.DataSource = null;
        lv.DataBind();
        lv.Visible = false;
        //lblFeeName.Text = "";
        if (ddlSchClg.SelectedIndex > 0)
        {
            this._objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            //ddlDegree.Items.Clear();
            //ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            _objCommon.DisplayMessage("Please select college/school.", Page);
            ddlSchClg.Focus();
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
            lv.DataSource = null;
            lv.DataBind();
            lblFeeName.Text = "";
            btnSubmit.Enabled = false;
            if (ddlBranch.SelectedIndex > 0)
            {
                this.Call_FillListbox();
                this._objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                ddlBatch.Focus();
            }
            else
            {
                _objCommon.DisplayMessage("Please select branch", Page);
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtSearchBox_TextChanged(object sender, EventArgs e)
    {
        Call_FillListbox();

        if (txtSearchBox.Text != "")
        {
            List<ListItem> newlist = new List<ListItem>();
            foreach (ListItem item in lstFeesItems.Items)
            {
                newlist.Add(item);
            }

            lstFeesItems.Items.Clear();

            foreach (ListItem item in newlist)
            {
                if (item.ToString().StartsWith(txtSearchBox.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    lstFeesItems.Items.Add(item.ToString());
                }
            }
        }
        else
        {
            Call_FillListbox();
            //return;
        }
    }

    //    List<ListItem> newlist = new List<ListItem>();
    //    foreach (ListItem item in lstFeesItems.Items)
    //    {
    //        newlist.Add(item);
    //    }

    //    lstFeesItems.Items.Clear();

    //    for (int i = 0; i < newlist.Count; i++)
    //    {
    //        foreach (ListItem item in newlist)
    //        {
    //            if (item.ToString().StartsWith(txtSearchBox.Text, StringComparison.CurrentCultureIgnoreCase))
    //            {

    //                lstFeesItems.Items.Add(item.ToString());
    //            }

    //        }
    //        return;
    //    }
    //}

    #region Copy Standard Fees

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try 
        {
            pnlCopy.Visible = true;
            this.btnSubmit.Enabled = true;
            //ddlRecieptCopy.SelectedValue = ddlReceiptType.SelectedValue;
            lv.Visible = false;
            lv.DataSource = null;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            idFeesName.Visible = false;
            btnCancelCopy.Visible = true;
            ddlRecieptCopy.Focus();


            if (rdbCopyStanderdFees.SelectedValue == "1")
            {
                divAdmBatch.Visible = true;
                divBranch.Visible = false;
                btnSubmitCopy.Visible = true;
                btnSubmitCopyDiv.Visible = false;
                clearcopy();

            }

            if (rdbCopyStanderdFees.SelectedValue == "2")
            {
                divAdmBatch.Visible = false;
                divBranch.Visible = true;
                btnSubmitCopyDiv.Visible = true;
                btnSubmitCopy.Visible = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnCopy_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clearcopy()
    {
        try 
        {
            ddlReceiptTypeRbd.SelectedIndex = 0;
            ddlAdmBatchFrom.SelectedIndex = 0;
            ddlCollegeFrom.SelectedIndex = 0;
            ddlDegreeFrom.SelectedIndex = 0;
            ddlBranchFrom.SelectedIndex = 0;
            ddlAdmBatchTo.SelectedIndex = 0;
            ddlCollegeTo.SelectedIndex = 0;
            ddlDegreeTo.SelectedIndex = 0;
            lboBranchTo.Items.Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.clearcopy-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

    protected void ddlRecieptCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Call_FillListbox_Copy();

        lblFeeName.Text = "";
        if (ddlRecieptCopy.SelectedIndex > 0)
        {

            //  ddlColgCopy.Focus();
        }
        else
        {
            _objCommon.DisplayMessage(pnlFeeTable, "Please select receipt type.", Page);
            ddlRecieptCopy.Focus();
        }
    }
    protected void ddlColgCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranchCopy.Items.Clear();
            ddlBranchCopy.Items.Add(new ListItem("Please Select", "0"));
            //ddlBatch.Items.Clear();
            //ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlDegreeCopy.Items.Clear();
            ddlDegreeCopy.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentCopy.Items.Clear();
            ddlPaymentCopy.Items.Add(new ListItem("Please Select", "0"));
            //lv.DataSource = null;
            //lv.DataBind();
            //lv.Visible = false;
            //lblFeeName.Text = "";
            if (ddlColgCopy.SelectedIndex > 0)
            {
                this._objCommon.FillDropDownList(ddlDegreeCopy, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlColgCopy.SelectedValue), "A.DEGREENAME");
                ddlDegreeCopy.Focus();
            }
            else
            {
                //ddlDegree.Items.Clear();
                //ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                _objCommon.DisplayMessage("Please select college/school.", Page);
                ddlColgCopy.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlColgCopy_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
      
    }
    protected void ddlDegreeCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////this.Call_FillListbox();
            ddlBranchCopy.Items.Clear();
            ddlBranchCopy.Items.Add(new ListItem("Please Select", "0"));
            //ddlBatch.Items.Clear();
            //ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentCopy.Items.Clear();
            ddlPaymentCopy.Items.Add(new ListItem("Please Select", "0"));
            //lv.DataSource = null;
            //lv.DataBind();
            //lv.Visible = false;
            lblFeeSNameCopy.Text = "";
            btnSubmit.Enabled = true;
            if (ddlDegreeCopy.SelectedIndex > 0)
            {
                _objCommon.FillDropDownList(ddlBranchCopy, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 and B.DEGREENO=" + ddlDegreeCopy.SelectedValue + " ", "A.LONGNAME");
                this.Call_FillListbox_Copy();
            }
            else
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Please select degree.", Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlDegreeCopy_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
      
    }
    protected void ddlBranchCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlBatch.Items.Clear();
            //ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentCopy.Items.Clear();
            ddlPaymentCopy.Items.Add(new ListItem("Please Select", "0"));
            //lv.DataSource = null;
            //lv.DataBind();
            lblFeeSNameCopy.Text = "";
            btnSubmit.Enabled = true;
            if (ddlBranchCopy.SelectedIndex > 0)
            {
                this.Call_FillListbox_Copy();
                this._objCommon.FillDropDownList(ddlBatchCopy, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                ddlBatchCopy.Focus();
            }
            else
            {
                _objCommon.DisplayMessage("Please select branch", Page);
                ddlBranchCopy.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlBranchCopy_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBatchCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////this.Call_FillListbox();
        try
        {
            ddlPaymentCopy.Items.Clear();
            ddlPaymentCopy.Items.Add(new ListItem("Please Select", "0"));
            //lv.DataSource = null;
            //lv.DataBind();
            lblFeeSNameCopy.Text = "";
            btnSubmit.Enabled = true;
            if (ddlBatchCopy.SelectedIndex > 0)
            {
                this._objCommon.FillDropDownList(ddlPaymentCopy, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
                ddlPaymentCopy.Focus();
                this.Call_FillListbox_Copy();
            }
            else
            {
                _objCommon.DisplayMessage("Please select admission batch.", Page);
                ddlBatchCopy.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlBatchCopy_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlPaymentCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////this.Call_FillListbox();
            //lv.DataSource = null;
            //lv.DataBind();
            lblFeeSNameCopy.Text = "";
            btnSubmit.Enabled = true;
            if (ddlPaymentCopy.SelectedIndex > 0)
            {
                this.Call_FillListbox_Copy();
                DisplayFeeNameCopy();
            }
            else
            {
                _objCommon.DisplayMessage("Please select payment type.", Page);
                ddlPaymentCopy.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.ddlPaymentCopy_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
      
    }

    private void Call_FillListbox_Copy()
    {
        try
        {
            // Call FillListbox() method by passing appropriate values
            ////this.FillListbox(
            ////    (this.GetDropDownListValue(ddlReceiptType) != null ? ddlReceiptType.SelectedValue : string.Empty),
            ////    (this.GetDropDownListValue(ddlDegree) != null ? Int32.Parse(ddlDegree.SelectedValue) : 0),
            ////    (this.GetDropDownListValue(ddlBatch) != null ? Int32.Parse(ddlBatch.SelectedValue) : 0),
            ////    (this.GetDropDownListValue(ddlPaymentType) != null ? Int32.Parse(ddlPaymentType.SelectedValue) : 0)
            ////    );
            this.FillListbox_Copy(
               (this.GetDropDownListValue(ddlRecieptCopy) != null ? ddlRecieptCopy.SelectedValue : string.Empty),
               (this.GetDropDownListValue(ddlDegreeCopy) != null ? Int32.Parse(ddlDegreeCopy.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlBranchCopy) != null ? Int32.Parse(ddlBranchCopy.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlBatchCopy) != null ? Int32.Parse(ddlBatchCopy.SelectedValue) : 0),
               (this.GetDropDownListValue(ddlPaymentCopy) != null ? Int32.Parse(ddlPaymentCopy.SelectedValue) : 0)
               );
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.Call_FillListbox_Copy-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void DisplayFeeNameCopy()
    {
        try
        {
            string Coll_Code = _objCommon.LookUp("ACD_COLLEGE_MASTER", "+'('+ SHORT_NAME+ '-' +code + ')'   as College_code", "COLLEGE_ID=" + ddlColgCopy.SelectedValue);
            if (ddlDegreeCopy.SelectedIndex > 0)
            {
                lblFeeSNameCopy.Text = ddlDegreeCopy.SelectedItem.Text + " ";
                lblFeeSNameCopy.Text += Coll_Code + "";
                lblFeeSNameCopy.Text += ddlBatchCopy.SelectedItem.Text + " ";
                lblFeeSNameCopy.Text += ddlPaymentCopy.SelectedItem.Text + " ";
                lblFeeSNameCopy.Text += ddlRecieptCopy.SelectedItem.Text;
            }
            else
            {
                lblFeeSNameCopy.Text += Coll_Code + "";
                lblFeeSNameCopy.Text += ddlBatchCopy.SelectedItem.Text + " ";
                lblFeeSNameCopy.Text += ddlPaymentCopy.SelectedItem.Text + " ";
                lblFeeSNameCopy.Text += ddlRecieptCopy.SelectedItem.Text;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.DisplayFeeNameCopy-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillListbox_Copy(string recieptCode, int degreeNo, int branchno, int batchNo, int payTypeNo)
    {
        try
        {
            StandardFeeController feeController = new StandardFeeController();
            ////lstFeesItems.DataSource = feeController.GetFeesItemsForListbox(recieptCode, degreeNo, batchNo, payTypeNo);
            lstFeesItems.DataSource = feeController.GetFeesItemsForListboxForBulk(recieptCode, degreeNo, branchno, batchNo, payTypeNo);
            lstFeesItems.DataTextField = "FEE_DESCRIPTION";
            lstFeesItems.DataValueField = "FEE_CAT_NO";
            lstFeesItems.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.FillListbox_Copy-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitCopy_Click(object sender, EventArgs e)
    {
        StandardFeeController feeItem = new StandardFeeController();
        try
        {
            /// This variable is used to store and maintain FeeCatNo generated in the stored procedure 
            /// while inserting data for F1 fee head in case of new standard fee record 
            int feeCatNo = 0;

            ////foreach (ListViewDataItem dataRow in lv.Items)
            ////{
            ////    feeCatNo = this.UpdateStandardFeeItem(dataRow, feeCatNo);
            ////}

            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            string Receipt_Code=ddlRecieptCopy.SelectedValue;
            int AdmBatchFrom =ddlAdmissionBatchFrom.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmissionBatchFrom.SelectedValue) : 0;
            int AdmBatchTo=ddlBatchCopy.SelectedIndex > 0 ? Convert.ToInt32(ddlBatchCopy.SelectedValue) : 0;
            CustomStatus cs=(CustomStatus)feeItem.CopyStandardFees(Receipt_Code, AdmBatchFrom, AdmBatchTo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Standard Fee Copied successfully ", Page);
                ClearCopy();
                return;
            }      
            /***********Commented By Dileep Kare on 10.08.2021 **************************
            foreach (ListViewDataItem dataRow in lv.Items)
            {
                feeCatNo = this.UpdateStandardFeeItem_Copy(dataRow, feeCatNo);
                if (feeCatNo == -99) // if error occurred
                {
                    break;
                }
            }
            if (feeCatNo > 0)
            {
                //_objCommon.DisplayMessage(pnlFeeTable, "Record saved successfully.", Page);
                _objCommon.DisplayMessage(pnlFeeTable, "Standard Fee Copied successfully ", Page);
                //this.FillListbox(string.Empty, 0, 0, 0, 0);
            }
            else if (feeCatNo == -99)
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Error occurred!", Page);
            }
            DisplayFeeDefinitionGrid();
            **************************************END*******************************************/
            /// Refill listbox.
            /// while reloading, we need all fee items in listbox hence send no values 
            /// in parameters not to filter records.
            ////********this.FillListbox(string.Empty, 0, 0, 0, 0);
            ////********_objCommon.DisplayMessage(pnlFeeTable, "Record saved successfully.", Page);

            //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('Record saved successfully.'); </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnSubmitCopy_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearCopy()
    {
        ddlRecieptCopy.Items.Clear();
        ddlRecieptCopy.Items.Add(new ListItem("Please Select", "0"));
        ddlAdmissionBatchFrom.Items.Clear();
        ddlAdmissionBatchFrom.Items.Add(new ListItem("Please Select", "0"));
        ddlBatchCopy.Items.Clear();
        ddlBatchCopy.Items.Add(new ListItem("Please Select", "0"));
    }
    

    protected void btnCancelCopy_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBatch.Items.Count > 0)
                ddlBatch.SelectedIndex = 0;

            if (ddlDegree.Items.Count > 0)
                ddlDegree.SelectedIndex = 0;

            if (ddlBranch.Items.Count > 0)
                ddlBranch.SelectedIndex = 0;

            if (ddlPaymentType.Items.Count > 0)
                ddlPaymentType.SelectedIndex = 0;

            if (ddlReceiptType.Items.Count > 0)
                ddlReceiptType.SelectedIndex = 0;

            if (ddlSchClg.Items.Count > 0)
                ddlSchClg.SelectedIndex = 0;
            lblFeeName.Text = string.Empty;

            if (ddlBatchCopy.Items.Count > 0)
                ddlBatchCopy.SelectedIndex = 0;

            if (ddlDegreeCopy.Items.Count > 0)
                ddlDegreeCopy.SelectedIndex = 0;

            if (ddlBranchCopy.Items.Count > 0)
                ddlBranchCopy.SelectedIndex = 0;

            if (ddlPaymentCopy.Items.Count > 0)
                ddlPaymentCopy.SelectedIndex = 0;

            if (ddlRecieptCopy.Items.Count > 0)
                ddlRecieptCopy.SelectedIndex = 0;

            if (ddlColgCopy.Items.Count > 0)
                ddlColgCopy.SelectedIndex = 0;
            lblFeeSNameCopy.Text = string.Empty;

            /// while refreshing page we need all fee items in listbox hence send no values 
            /// in parameters to filter records.
            this.FillListbox_Copy(string.Empty, 0, 0, 0, 0);
            ddlAdmissionBatchFrom.SelectedIndex = 0;
            rdbCopyStanderdFees.SelectedIndex = 0;
            ddlReceiptTypeRbd.SelectedIndex = 0;
            ddlAdmBatchFrom.SelectedIndex = 0;
            ddlCollegeFrom.SelectedIndex = 0;
            ddlDegreeFrom.SelectedIndex = 0;
            ddlBranchFrom.SelectedIndex = 0;
            ddlAdmBatchTo.SelectedIndex = 0;
            ddlCollegeTo.SelectedIndex = 0;
            ddlDegreeTo.SelectedIndex = 0;
            //ddlBranchTo.SelectedIndex = 0;
            //lboBranchTo.SelectedIndex = 0;
            lboBranchTo.Items.Clear();
            this.lv.Visible = false;
            this.btnSubmit.Enabled = false;
            pnlCopy.Visible = false;
            btnSubmitCopy.Visible = false;
            btnSubmitCopyDiv.Visible = false;
            btnCancelCopy.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnCancelCopy_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void rdbCopyStanderdFees_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbCopyStanderdFees.SelectedValue == "1")
            {
                divAdmBatch.Visible = true;
                divBranch.Visible = false;
                btnSubmitCopyDiv.Visible = false;
                btnSubmitCopy.Visible = true;
                btnCancelCopy.Visible = true;

                ddlReceiptTypeRbd.SelectedIndex = 0;
                ddlAdmBatchFrom.SelectedIndex = 0;
                ddlCollegeFrom.SelectedIndex = 0;
                ddlDegreeFrom.SelectedIndex = 0;
                ddlBranchFrom.SelectedIndex = 0;
                ddlAdmBatchTo.SelectedIndex = 0;
                ddlCollegeTo.SelectedIndex = 0;
                ddlDegreeTo.SelectedIndex = 0;
                lboBranchTo.Items.Clear();
            }

            if (rdbCopyStanderdFees.SelectedValue == "2")
            {
                divAdmBatch.Visible = false;
                divBranch.Visible = true;
                btnSubmitCopyDiv.Visible = true;
                btnSubmitCopy.Visible = false;
                btnCancelCopy.Visible = true;

                ddlRecieptCopy.SelectedIndex = 0;
                ddlAdmissionBatchFrom.SelectedIndex = 0;
                ddlBatchCopy.SelectedIndex = 0;

                this._objCommon.FillDropDownList(ddlAdmBatchFrom, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                this._objCommon.FillDropDownList(ddlAdmBatchTo, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
            }
        
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.rdbCopyStanderdFees_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlAdmBatchFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._objCommon.FillDropDownList(ddlCollegeFrom, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }


    protected void ddlCollegeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._objCommon.FillDropDownList(ddlDegreeFrom, "ACD_DEGREE A INNER JOIN ACD_STANDARD_FEES B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollegeFrom.SelectedValue), "A.DEGREENAME");
    }


    protected void ddlDegreeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._objCommon.FillDropDownList(ddlBranchFrom, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 and B.DEGREENO=" + ddlDegreeFrom.SelectedValue + " ", "A.LONGNAME");
    }


    protected void ddlAdmBatchTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._objCommon.FillDropDownList(ddlCollegeTo, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }


    protected void ddlCollegeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._objCommon.FillDropDownList(ddlDegreeTo, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID =" + Convert.ToInt32(ddlCollegeTo.SelectedValue), "A.DEGREENAME");
    }


    protected void ddlDegreeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatchFrom.SelectedValue == ddlAdmBatchTo.SelectedValue)
        {
            this._objCommon.FillListBox(lboBranchTo, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 AND (A.BRANCHNO NOT IN(" + Convert.ToInt32(ddlBranchFrom.SelectedValue) + ") ) AND B.DEGREENO=" + ddlDegreeTo.SelectedValue + " ", "A.LONGNAME");
        }
        else
        {
            this._objCommon.FillListBox(lboBranchTo, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT (A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO>0 AND B.DEGREENO=" + ddlDegreeTo.SelectedValue + " ", "A.LONGNAME");
        }
    }

    private void ClearSubmit()
    {
        ddlReceiptTypeRbd.SelectedIndex = 0;
        ddlAdmBatchFrom.SelectedIndex = 0;
        ddlCollegeFrom.SelectedIndex = 0;
        ddlDegreeFrom.SelectedIndex = 0;
        ddlBranchFrom.SelectedIndex = 0;
        ddlAdmBatchTo.SelectedIndex = 0;
        ddlCollegeTo.SelectedIndex = 0;
        ddlDegreeTo.SelectedIndex = 0;
        //ddlBranchTo.SelectedIndex = 0;
        lboBranchTo.Items.Clear();
    }
    protected void btnSubmitCopyDiv_Click(object sender, EventArgs e)
    {
        StandardFeeController feeItem = new StandardFeeController();
        try
        {
            /// This variable is used to store and maintain FeeCatNo generated in the stored procedure 
            /// while inserting data for F1 fee head in case of new standard fee record 
            int feeCatNo = 0;

            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            string Receipt_Code = ddlReceiptTypeRbd.SelectedValue;

            int AdmBatchFrom = ddlAdmBatchFrom.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmBatchFrom.SelectedValue) : 0;
            int AdmBatchTo = ddlAdmBatchTo.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmBatchTo.SelectedValue) : 0;

            int CollegeidFrom = ddlCollegeFrom.SelectedIndex > 0 ? Convert.ToInt32(ddlCollegeFrom.SelectedValue) : 0;
            int CollegeidTo   = ddlCollegeTo.SelectedIndex > 0 ? Convert.ToInt32(ddlCollegeTo.SelectedValue) : 0;

            int DegreenoFrom = ddlDegreeFrom.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeFrom.SelectedValue) : 0;
            int DegreenoTo = ddlDegreeTo.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeTo.SelectedValue) : 0;

            int BranchnoFrom = ddlBranchFrom.SelectedIndex > 0 ? Convert.ToInt32(ddlBranchFrom.SelectedValue) : 0;

            //int BranchnoTo = ddlBranchTo.SelectedIndex > 0 ? Convert.ToInt32(ddlBranchTo.SelectedValue) : 0;

            int count = 0;

            string BranchnoTo = string.Empty;

            foreach (ListItem Item in lboBranchTo.Items)
            {
                if (Item.Selected)
                {
                    BranchnoTo += Item.Value + ",";
                    count++;
                }
            }
            BranchnoTo = BranchnoTo.Substring(0, BranchnoTo.Length - 1);

             //BranchnoTo = lboBranchTo.SelectedValue;
            int ChkOveride = 0;

            if (ChkBranchCopyOverride.Checked == true)
            {
                ChkOveride =1;
 
            }
            else
            {
                ChkOveride = 0;
            }

            CustomStatus cs = (CustomStatus)feeItem.CopyStandardFeesBranchWise(Receipt_Code, AdmBatchFrom, AdmBatchTo, CollegeidFrom, CollegeidTo, DegreenoFrom, DegreenoTo, BranchnoFrom, BranchnoTo, ChkOveride);
            
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                _objCommon.DisplayMessage(pnlFeeTable, "Standard Fee Copied successfully ", Page);
                ClearSubmit();
                return;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnSubmitCopyDiv_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
   
    #endregion Copy Standard Fees


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            StandardFeeController feeController = new StandardFeeController();
            ////DataSet ds = feeController.GetFeeDefinition(ddlReceiptType.SelectedValue, ddlSchClg.SelectedValue, ddlDegree.SelectedValue, ddlBatch.SelectedValue, ddlPaymentType.SelectedValue);
            DataSet ds = feeController.GetFeeDefinition_Excel(ddlBatch.SelectedValue);

            GridView GV = new GridView();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//********
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment;filename=StandardFeesReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.HeaderStyle.Font.Bold = true;
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                _objCommon.DisplayMessage(this.updProg, "No data found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                _objUaimsCommon.ShowError(Page, "Payments_StandardFeeDefinition.btnExcel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                _objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



   
}
