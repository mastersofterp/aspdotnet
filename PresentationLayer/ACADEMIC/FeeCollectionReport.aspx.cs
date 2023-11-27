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
using System.IO;

public partial class ACADEMIC_FeeCollectionReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController objFee = new FeeCollectionController();

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
                    //  CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "RCPTTYPENO");
                    objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }


    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lvAmount_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem daitem = (ListViewDataItem)e.Item;
        ListView lvDetails = daitem.FindControl("lvDetails") as ListView;
        Label lblCollegeName = daitem.FindControl("lblCollName") as Label;
        DataSet ds = objFee.GetUniversityCollegeFeeData(ddlReceiptType.SelectedValue, Convert.ToInt32(lblCollegeName.ToolTip));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.DataSource = ds;
            lvDetails.DataBind();
        }
    }
    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lvAdmissionBatch_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem daitem = (ListViewDataItem)e.Item;
        ListView lvDetails = daitem.FindControl("lvDetails") as ListView;
        Label lblCollegeName = daitem.FindControl("lblCollName") as Label;
        DataSet ds = objFee.GetAdmissionBatchCollege(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(lblCollegeName.ToolTip));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.DataSource = ds;
            lvDetails.DataBind();
        }
    }
    public void GetStudentsList()
    {
        try
        {
            DataSet ds = objFee.Get_Paid_Students_List(ddlReceiptType.SelectedValue);
            string Receipt_Code = ddlReceiptType.SelectedValue;
            ViewState["Receipt_Code"] = Receipt_Code;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divListView.Visible = true;
                lvStudList.DataSource = ds;
                lvStudList.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudList);//Set label 
                pnlStudentLedger.Visible = true;
                pnlDegreeBranch.Visible = false;
                pnlAmount.Visible = false;
                pnlAdmissionBatch.Visible = false;
                pnlFinancial.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnReceiptInfo_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkButton = sender as LinkButton;
            int Idno = int.Parse(lnkButton.CommandArgument);
            ViewState["Idno"] = Idno;
            string Receipt_Code = Convert.ToString(ViewState["Receipt_Code"]);
            this.ShowReceiptInfo(Idno, Receipt_Code);
            //OPEN POPUP
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //divPopup.Visible = true;
            //mpePnlStudent.Show();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReceiptInfo(int Idno, string Receipt_Code)
    {
        try
        {
            //Receipt_Code =Convert.ToString(ViewState["Receipt_Code"]);
            DataSet ds = objFee.GetReceiptInfo(Idno, Receipt_Code);


            // Create new DataTable and DataSource objects.
            DataTable dtPaidFees = new DataTable();

            dtPaidFees.Columns.Add("Semster", typeof(string));
            dtPaidFees.Columns.Add("REC_NO", typeof(string));
            dtPaidFees.Columns.Add(new DataColumn("REC_DATE", typeof(DateTime)));
            dtPaidFees.Columns.Add("APPLIED_AMT", typeof(string));
            dtPaidFees.Columns.Add("PAID_AMT", typeof(float));
            dtPaidFees.Columns.Add("BAL_AMT", typeof(float));
            double TOTALPAID_AMT = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtPaidFees.NewRow();
                    if (i > 0)
                    {
                        if (ds.Tables[0].Rows[i - 1]["SEMESTERNAME"].ToString() == ds.Tables[0].Rows[i]["SEMESTERNAME"].ToString())
                        {
                            dr["Semster"] = ds.Tables[0].Rows[i]["SEMESTERNAME"];
                            dr["REC_NO"] = ds.Tables[0].Rows[i]["REC_NO"];
                            dr["REC_DATE"] = ds.Tables[0].Rows[i]["REC_DT"];
                            dr["APPLIED_AMT"] = dtPaidFees.Rows[i - 1]["BAL_AMT"];
                            //dr["APPLIED_AMT"] = ds.Tables[0].Rows[i - 1]["BAL_AMT"].ToString();
                            dr["PAID_AMT"] = ds.Tables[0].Rows[i]["PAID_AMOUNT"];
                            dr["BAL_AMT"] = Convert.ToDouble(ds.Tables[0].Rows[i]["APPLIED_AMOUNT"]) - (this.TOTALPAID_AMOUNT(i, ds, ds.Tables[0].Rows[i]["SEMESTERNAME"].ToString())); //(Convert.ToDouble(ds.Tables[0].Rows[i - 1]["PAID_AMOUNT"]) + Convert.ToDouble(ds.Tables[0].Rows[i]["PAID_AMOUNT"]));
                        }
                        else
                        {
                            dr["Semster"] = ds.Tables[0].Rows[i]["SEMESTERNAME"];
                            dr["REC_NO"] = ds.Tables[0].Rows[i]["REC_NO"];
                            dr["REC_DATE"] = ds.Tables[0].Rows[i]["REC_DT"];
                            dr["APPLIED_AMT"] = ds.Tables[0].Rows[i]["APPLIED_AMOUNT"];
                            dr["PAID_AMT"] = ds.Tables[0].Rows[i]["PAID_AMOUNT"];
                            dr["BAL_AMT"] = ds.Tables[0].Rows[i]["BAL_AMT"];
                        }
                    }
                    else
                    {
                        dr["Semster"] = ds.Tables[0].Rows[i]["SEMESTERNAME"];
                        dr["REC_NO"] = ds.Tables[0].Rows[i]["REC_NO"];
                        dr["REC_DATE"] = ds.Tables[0].Rows[i]["REC_DT"];
                        dr["APPLIED_AMT"] = ds.Tables[0].Rows[i]["APPLIED_AMOUNT"];
                        dr["PAID_AMT"] = ds.Tables[0].Rows[i]["PAID_AMOUNT"];
                        dr["BAL_AMT"] = ds.Tables[0].Rows[i]["BAL_AMT"];
                    }
                    dtPaidFees.Rows.Add(dr);
                }


                lvReceipt.DataSource = dtPaidFees;
                lvReceipt.DataBind();
                lvReceipt.Visible = true;
                //lvProgramme.Visible = true;
            }
            else
            {
                lvReceipt.DataSource = null;
                lvReceipt.DataBind();
                lvReceipt.Visible = false;
            }

            //foreach (ListViewDataItem item in lvReceipt.Items)
            //{
            //    Label lblApply = item.FindControl("lblAppliedAmount") as Label;
            //    Label lblPaid = item.FindControl("lblPaidAmount") as Label;
            //    Label lblBal = item.FindControl("lblBalanceAmount") as Label;
            //    double lblamount =convert.ToDouble(lblApply.Text
            //    if (lblApply.Text == lblPaid.Text)
            //    {
            //        lblBal.Text=Convert.ToString();
            //    }

            //}

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private double TOTALPAID_AMOUNT(int I, DataSet dstot, string semester)
    {
        double totpaid_amt = 0;
        for (int j = 0; j <= I; j++)
        {
            if (dstot.Tables[0].Rows[j]["SEMESTERNAME"].ToString() == semester)
            {
                totpaid_amt += Convert.ToDouble(dstot.Tables[0].Rows[j]["PAID_AMOUNT"]);
            }
        }
        return totpaid_amt;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.BRANCHNO=B.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO>0 AND DEGREENO= " + ddlDegree.SelectedValue, "B.BRANCHNO");
        pnlDegreeBranch.Visible = false;
        lvDetails.DataSource = null;
        lvDetails.DataBind();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlDegreeBranch.Visible = false;
        lvDetails.DataSource = null;
        lvDetails.DataBind();
    }
    protected void lvFinancialYear_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem daitem = (ListViewDataItem)e.Item;
        ListView lvDetails = daitem.FindControl("lvDetails") as ListView;
        Label lblCollegeName = daitem.FindControl("lblCollName") as Label;
        DataSet ds = objFee.GetFinancialYearWiseReport(ddlReceiptType.SelectedValue, Convert.ToInt32(lblCollegeName.ToolTip));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.DataSource = ds;
            lvDetails.DataBind();
            pnlAdmissionBatch.Visible = false;
            pnlAmount.Visible = false;
            pnlDegreeBranch.Visible = false;
            pnlStudentLedger.Visible = false;

        }
    }
    protected void rblFeeCollection_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblFeeCollection.SelectedValue == "1" || rblFeeCollection.SelectedValue == "2" || rblFeeCollection.SelectedValue == "5")
        {
            divRecieptType.Visible = true;
            divAdmissionBatch.Visible = false;
            divBranch.Visible = false;
            divDegree.Visible = false;

        }
        if (rblFeeCollection.SelectedValue == "3")
        {
            divRecieptType.Visible = true;
            divAdmissionBatch.Visible = false;
            divBranch.Visible = true;
            divDegree.Visible = true;
        }
        if (rblFeeCollection.SelectedValue == "4")
        {
            divRecieptType.Visible = true;
            divAdmissionBatch.Visible = true;
            divBranch.Visible = false;
            divDegree.Visible = false;
        }
        pnlAmount.Visible = false;
        lvAmount.DataSource = null;
        lvAmount.DataBind();
        lvAdmissionBatch.DataSource = null;
        lvAdmissionBatch.DataBind();
        lvAdmissionBatch.Visible = false;
        pnlAdmissionBatch.Visible = true;
        lvDetails.DataSource = null;
        lvDetails.DataBind();
        lvFinancialYear.DataSource = null;
        lvFinancialYear.DataBind();
        pnlDegreeBranch.Visible = false;
        pnlFinancial.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        divListView.Visible = false;
        ddlReceiptType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlAdmbatch.SelectedIndex = 0;
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (rblFeeCollection.SelectedValue == "1")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }

            GetStudentsList();
        }
        else if (rblFeeCollection.SelectedValue == "2")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }

            DataSet ds = objFee.GetUniversityFeeData(ddlReceiptType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAmount.DataSource = ds;
                lvAmount.DataBind();
                pnlAmount.Visible = true;
                lvAmount.Visible = true;
                pnlDegreeBranch.Visible = false;
                pnlStudentLedger.Visible = false;
                pnlAdmissionBatch.Visible = false;
                pnlFinancial.Visible = false;
            }
        }

        else if (rblFeeCollection.SelectedValue == "5")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }

            DataSet ds = objFee.GetFinancialYearCollegeList(ddlReceiptType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFinancialYear.DataSource = ds;
                lvFinancialYear.DataBind();
                pnlFinancial.Visible = true;
                lvFinancialYear.Visible = true;
                pnlAdmissionBatch.Visible = false;
                pnlAmount.Visible = false;
                pnlStudentLedger.Visible = false;
                pnlDegreeBranch.Visible = false;
            }
        }

        else if (rblFeeCollection.SelectedValue == "4")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }
            if (ddlAdmbatch.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Admission Batch.", this.Page);
                return;
            }

            DataSet ds = objFee.GetAdmissionBatchFeeData(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlAdmbatch.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAdmissionBatch.DataSource = ds;
                lvAdmissionBatch.DataBind();
                pnlAdmissionBatch.Visible = true;
                lvAdmissionBatch.Visible = true;
                pnlAmount.Visible = false;
                pnlStudentLedger.Visible = false;
                pnlDegreeBranch.Visible = false;
                pnlFinancial.Visible = false;
            }
        }
        else if (rblFeeCollection.SelectedValue == "3")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }
            if (ddlDegree.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Degree.", this.Page);
                return;
            }
            //if (ddlBranch.SelectedValue == "0")
            //{
            //    objUCommon.DisplayMessage(this.Page, "Please Select Branch.", this.Page);
            //    return;
            //}


            DataSet ds = objFee.GetDegreeBranchWiseReport(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
                pnlDegreeBranch.Visible = true;
                pnlAmount.Visible = false;
                pnlStudentLedger.Visible = false;
                pnlAdmissionBatch.Visible = false;
                pnlFinancial.Visible = false;
            }
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        if (rblFeeCollection.SelectedValue == "1")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }

            DataSet ds = objFee.GetStudentLedgerExcelReport(ddlReceiptType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "StudentLedgerReport");
            }
        }
        if (rblFeeCollection.SelectedValue == "2")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }
            DataSet ds = objFee.GetUniversityLevelExcelReport(ddlReceiptType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "UniversityLevelReport");
            }
        }

        if (rblFeeCollection.SelectedValue == "3")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }
            if (ddlDegree.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Degree.", this.Page);
                return;
            }
            //if (ddlBranch.SelectedValue == "0")
            //{
            //    objUCommon.DisplayMessage(this.Page, "Please Select Branch.", this.Page);
            //    return;
            //}
            DataSet ds = objFee.GetDegreeBranchExcelReport(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "DegreeBranchWiseReport");
            }
        }
        if (rblFeeCollection.SelectedValue == "4")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }
            if (ddlAdmbatch.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Admission Batch.", this.Page);
                return;
            }

            DataSet ds = objFee.GetAdmissionBatchExcelReport(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlAdmbatch.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "AdmissionBatchWiseReport");
            }
        }
        if (rblFeeCollection.SelectedValue == "5")
        {
            if (ddlReceiptType.SelectedValue == "0")
            {
                objUCommon.DisplayMessage(this.Page, "Please Select Receipt Type.", this.Page);
                return;
            }
            DataSet ds = objFee.GetFinancialYearExcelReport(ddlReceiptType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ExcelReport(ds, "FinancialYearWiseReport");
            }
        }
    }
    private void ExcelReport(DataSet ds, string Title)
    {
        GridView GV = new GridView();
        string ContentType = string.Empty;

        GV.DataSource = ds;
        GV.DataBind();
        string attachment = "attachment; filename=" + Title +"_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +  ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GV.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}