//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : FEES COLLECTION
// CREATION DATE : 06-JUL-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;

public partial class Academic_DemandModification : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    #region Page Events

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
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        try
                        {
                            int studId = int.Parse(Request.QueryString["id"].ToString());
                            /// passing demand no as zero to retrieve all demand record  first time.
                            this.DisplayAllDemands(studId, 0);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
                //Search Pannel Dropdown Added by Swapnil
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO    ");
                ddlSearch.SelectedIndex = 1;
                ddlSearch_SelectedIndexChanged(sender, e);
                remark.Visible = false;
                btnSubCan.Visible = false;
                //End Search Pannel Dropdown
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
                if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty &&
                    Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                {

                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnclear"))
                {

                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DemandModification.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DemandModification.aspx");
        }
    }

    #endregion



    #region SearchPannel

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        //divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lvAllDemands.DataSource = null;
            lvAllDemands.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvAllDemands.DataSource = null;
            lvAllDemands.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        //if (value == "BRANCH")
        //{
        //    divbranch.Attributes.Add("style", "display:block");

        //}
        //else if (value == "SEM")
        //{
        //    divSemester.Attributes.Add("style", "display:block");
        //}
        //else
        //{
        //    divtxt.Attributes.Add("style", "display:block");
        //}

        //ShowDetails();
        //divAllDemands.Visible = false;
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;
            FeeCollectionController feeController = new FeeCollectionController();
            // int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
            if (idno > 0)
            {
                this.DisplayAllDemands(idno, 0);
            }
            else
            {
                ShowMessage("No student found with given enrollment number.");
                divAllDemands.Visible = true;
                //divFeeItems.Visible = false;
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion


    protected void btnEditDemand_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;
            int demandId = (btnEditRecord.CommandArgument != string.Empty ? int.Parse(btnEditRecord.CommandArgument) : 0);
            int studentId = (btnEditRecord.CommandName != string.Empty ? int.Parse(btnEditRecord.CommandName) : 0);

            int semester = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "SEMESTERNO", "IDNO=" + studentId + " AND DM_NO=" + demandId));
            string RecieptCode = Convert.ToString(objCommon.LookUp("ACD_DEMAND", "RECIEPT_CODE", "IDNO=" + studentId + " AND DM_NO=" + demandId));

            int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO =" + studentId + " AND SEMESTERNO =" + Convert.ToInt32(semester) + " AND ISNULL(INSTAL_CANCEL,0)=0 AND RECIPTCODE = '" + Convert.ToString(RecieptCode) + "'"));

            if (count > 0)
            {
                // objCommon.DisplayMessage(this.Page, "Installment Allotment found for this Demand. Kindly remove the Installment for Demand Modification.", this.Page);
                //this.ShowMessage("Installment Allotment found for this Demand. Kindly remove the Installment for Demand Modification.");
                // return;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "CallConfirmBox", "OpenConfirmDialog(" + studentId + "," + semester + "," + demandId + ");", true);
            }


            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetDemand(demandId, studentId);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvFeeItems.DataSource = ds;
                lvFeeItems.DataBind();

                txtRemark.Text = ds.Tables[0].Rows[0]["PARTICULAR"].ToString();
                
                //divFeeItems.Visible = true;
                /// showing total demand amount in total amount textbox using javascript.
                divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> UpdateTotalAmount(); </script>";

                /// Store student Id and demand no in view state 
                /// to be used while saving the record.
                ViewState["StudentId"] = studentId;
                ViewState["DemandId"] = demandId;
                this.remark.Visible = true;
                this.btnSubCan.Visible = true;
                this.DisplayAllDemands(studentId, demandId);
                this.btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void DisplayAllDemands(int studentId, int demandNo)
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetAllDemands(studentId, demandNo);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.Visible = false;
                lvAllDemands.DataSource = ds;
                lvAllDemands.DataBind();
                lvAllDemands.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAllDemands);//Set label - 
                lblNoRecords.Visible = false;
            }
            else
                objCommon.DisplayMessage(this.Page, "No fee demand found for this student.", this.Page);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

        if (txtRemark.Text == string.Empty )
            {
            objCommon.DisplayMessage(this.Page, "Please Enter Remark.", this.Page);
            return;
            }

            FeeDemand modifiedDemand = new FeeDemand();
            modifiedDemand.StudentId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            modifiedDemand.DemandId = (GetViewStateItem("DemandId") != string.Empty ? int.Parse(GetViewStateItem("DemandId")) : 0);
            modifiedDemand.FeeHeads = this.GetFeeItems();
            modifiedDemand.TotalFeeAmount = this.GetTotalDemandAmt();
            modifiedDemand.Remark = "This Demand has been Modified by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
            modifiedDemand.Remark += txtRemark.Text.Trim();
            modifiedDemand.UANO = int.Parse(Session["userno"].ToString());
            modifiedDemand.IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

            DemandModificationController dmController = new DemandModificationController();
            if (dmController.UpdateDemand(modifiedDemand))
            {
                //this.ShowMessage("Demand Updated Successfully.");
                objCommon.DisplayMessage(this.Page, "Demand Updated Successfully.", this.Page);
                DisplayAllDemands(modifiedDemand.StudentId, modifiedDemand.DemandId);
            }
            else
                //this.ShowMessage("Unable to update demand.");
                objCommon.DisplayMessage(this.Page, "Unable to update demand.", this.Page);
        }
        catch (Exception ex)
        {
            throw;
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
            throw;
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
            throw;
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

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }
    }

    [WebMethod()]
    //[System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public static string CancelInstallnment(int studId, int sem, int demandId)
    {
        try
        {
            FeeDemand modifiedDemand = new FeeDemand();
            modifiedDemand.StudentId = studId;
            modifiedDemand.DemandId = demandId;
            modifiedDemand.SemesterNo = sem;
            modifiedDemand.UANO = Convert.ToInt32(HttpContext.Current.Session["userno"]);           //modifiedDemand.IpAddress = HttpRequest.ServerVariables["REMOTE_ADDR"].ToString();

            DemandModificationController dmController = new DemandModificationController();
            int ret = dmController.CancelInstallnment(modifiedDemand);

            var js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (ret > 0)
                return js.Serialize(ret);

            return null;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}