using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Data.SqlClient;

public partial class ACADEMIC_adjDcrAfterFeesPaid : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //   this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "");

                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        try
                        {
                            int studId = int.Parse(Request.QueryString["id"].ToString());
                            /// passing demand no as zero to retrieve all demand record  first time.
                            // this.DisplayAllDemands(studId, 0);
                        }
                        catch (Exception ex)
                        {
                            throw ex = new Exception("Invalid Student Id has been passed.");
                        }
                    }

                  // (select value from dbo.split(@V_USER_TYPES,','))

                    string UserNos = objCommon.LookUp("ACD_MODULE_CONFIG", "HEAD_TO_HEAD_ADJ_PAGE_USERS", Session["userno"].ToString() + "IN (select value from dbo.split(HEAD_TO_HEAD_ADJ_PAGE_USERS,','))");


                    if (UserNos=="")
                        {
                        objCommon.DisplayMessage(this.Page, "You are not authorized to view this page. Please Contact to Admin.", this.Page);
                        divsearchstud.Visible = false;
                        return;
                        }
                  // UserNos = UserNos.Substring(0, UserNos.Length - 1);

                  // string Program = UserNos;
                  // string[] subs = Program.Split(',');
                  ////int  Count = subs.Count();
                  // for (int i = 0; i < subs.Count(); i++)
                  //     {
                  //     if (Session["userno"].ToString() == subs[i].ToString())
                  //         {
                  //         objCommon.DisplayMessage(this.Page, "ok", this.Page);

                  //         }
                  //     else
                  //         {
                  //         objCommon.DisplayMessage(this.Page, "you are not authorized to view this page", this.Page);
                  //         return;
                  //         }
                  //     }

                    //if(Session["userno"].ToString() )
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
                if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty &&
                    Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                {
                    //  this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnclear"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    ddlDegree.ClearSelection();
                    ddlBranch.ClearSelection();
                    ddlYear.ClearSelection();
                    ddlSem.ClearSelection();

                }
                //if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                //{
                //    try
                //    {
                //        int studId = int.Parse(Request.QueryString["id"].ToString());
                //        /// passing demand no as zero to retrieve all demand record  first time.
                //        this.DisplayAllDemands(studId, 0);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex = new Exception("Invalid Student Id has been passed.");
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                divFeeItems.Visible = false;
                btnSubmit.Enabled = false;
                FeeCollectionController feeController = new FeeCollectionController();
                int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
                if (studentId > 0)
                {
                    this.DisplayAllPayments(studentId, 0);
                }
                else
                {
                    ShowMessage("No student found with given enrollment number.");
                    divAllDemands.Visible = false;
                    divFeeItems.Visible = false;
                    btnSubmit.Enabled = false;
                }
            }
            else
                ShowMessage("Please enter enrollment number.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void DisplayAllPayments(int studentId, int DCRNO)
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetAllPayments(studentId, DCRNO);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvAllDemands.DataSource = ds;
                lvAllDemands.DataBind();
                divAllDemands.Visible = true;
                ViewState["ds"] = ds;
            }
            else
                ShowMessage("No fee demand found for this student.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.DisplayAllDemands() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
   
    protected void btnEditDCR_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //int count = 0;
            ImageButton btnEditRecord = sender as ImageButton;
            int dcrno = (btnEditRecord.CommandArgument != string.Empty ? int.Parse(btnEditRecord.CommandArgument) : 0);
            int studentId = (btnEditRecord.CommandName != string.Empty ? int.Parse(btnEditRecord.CommandName) : 0);
            int rowindex = Convert.ToInt32(btnEditRecord.ToolTip);

            HiddenField hdnDegree = this.lvAllDemands.Items[rowindex].FindControl("hdnDegree") as HiddenField;
            HiddenField hdnBranch = this.lvAllDemands.Items[rowindex].FindControl("hdnBranch") as HiddenField;
            HiddenField hdnSem = this.lvAllDemands.Items[rowindex].FindControl("hdnSem") as HiddenField;
            HiddenField hdnRcpt = this.lvAllDemands.Items[rowindex].FindControl("hdnRcpt") as HiddenField;

            //count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(IDNO)", "IDNO=" + studentId + " AND SEMESTERNO=" + Convert.ToInt32(hdnSem.Value) + " AND RECIEPT_CODE='" + hdnRcpt.Value + "' AND DEGREENO=" + Convert.ToInt32(hdnDegree.Value) + " AND BRANCHNO=" + Convert.ToInt32(hdnBranch.Value) + ""));
            //if (count > 0)
            //{
            //    //objCommon.DisplayUserMessage(updEdit, "This student has paid for this demand , so you can not modify the demand.", this.Page);
            //    this.ShowMessage("This Student Has Paid For This Demand , Hence You Can Not Modify The Demand.");
            //    return;
            //}
            //else
            //{
            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetPayment(dcrno, studentId);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvFeeItems.DataSource = ds;
                lvFeeItems.DataBind();

                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
           string Excess_amt=     objCommon.LookUp("ACD_DCR", "ISNULL(EXCESS_AMOUNT,0)", "EXCESS_AMOUNT>0 AND DCR_NO="+dcrno);
           hdnPrevExcessAmt.Value = Excess_amt == string.Empty ? Convert.ToString(0) : Excess_amt.ToString();
           txtExcessAmt.Text = Excess_amt == string.Empty ? Convert.ToString(0) : Excess_amt.ToString();
                divFeeItems.Visible = true;
                /// showing total demand amount in total amount textbox using javascript.
                divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmount(); </script>";
                                /// Store student Id and demand no in view state 
                /// to be used while saving the record.
                ViewState["StudentId"] = studentId;
                ViewState["DCRNO"] = dcrno;

                DataSet ds1 = (DataSet)ViewState["ds"];
                if (ds1 != null)
                {
                    hdnTotalAmount.Value = ds1.Tables[0].Rows[rowindex]["TOTAL_AMT1"].ToString();
                }
                this.DisplayAllPayments(studentId, dcrno);
                this.btnSubmit.Enabled = true;
            }
            //}


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.btnEditDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FeeDemand modifiedDemand = new FeeDemand();
            modifiedDemand.StudentId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            modifiedDemand.DemandId = (GetViewStateItem("DCRNO") != string.Empty ? int.Parse(GetViewStateItem("DCRNO")) : 0);
            modifiedDemand.FeeHeads = this.GetFeeItems();
            modifiedDemand.TotalFeeAmount = this.GetTotalDemandAmt();
            modifiedDemand.Remark = txtRemark.Text.Trim();
            modifiedDemand.UANO = int.Parse(Session["userno"].ToString());
            modifiedDemand.IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            double Excess_Amt=Convert.ToDouble(hdnExcess.Value);
            DemandModificationController dmController = new DemandModificationController();
            if (dmController.UpdatePayment(modifiedDemand, Excess_Amt))
            {
                this.ShowMessage("Payment updated successfully.");
                DisplayAllPayments(modifiedDemand.StudentId, modifiedDemand.DemandId);
                btnSubmit.Enabled = false;
            }
            else
                this.ShowMessage("Unable to update Payment.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private FeeHeadAmounts GetFeeItems()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                int feeHeadNo = 0;
                double feeAmount = 0.00;

                string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
                if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                    feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                if (feeAmt != null && feeAmt != string.Empty)
                    feeAmount = Convert.ToDouble(feeAmt);

                feeHeadAmts[feeHeadNo - 1] = feeAmount;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }

    private double GetTotalDemandAmt()
    {
        double totalAmt = 0.00;
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                string amt = (item.FindControl("txtFeeItemAmount") as TextBox).Text.Trim();
                if (amt != null && amt != string.Empty)
                    totalAmt += double.Parse(amt);
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DemandModification.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        
        return totalAmt;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }
}