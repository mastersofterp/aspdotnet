//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Fees Installment Details
// CREATION DATE : 02/03/2020
// CREATED BY    : Sneha Doble
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC :  
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using System.Configuration;
using System.IO;
public partial class ACADEMIC_StudentDocumentList : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    int count = 0;

    #region Pageload

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
                Session["usertype"] == null || Session["userfullname"] == null)
                {
                Response.Redirect("~/default.aspx");
                }
            else
                {
                if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                    CheckPageAuthorization();
                    }
                else
                    {
                    }
                SetInitialRow();
                ViewState["uano"] = Session["userno"];
                }
            //PopulateDropDownList();
            //int mul_col_flag = Convert.ToInt32(objCommon.LookUp("REFF", "MUL_COL_FLAG", "CollegeName='" + Session["coll_name"].ToString() + "'"));
            // if (mul_col_flag == 0)
            //{
            //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //    ddlCollege.SelectedIndex = 1;
            //}
            //else
            //{
            //objCommon.FillDropDownList(ddlSemester, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");                
            //ddlCollege.SelectedIndex = 0;
            // }
            //objCommon.FillDropDownList(ddlSemester, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE NOT IN ('MIS')", "RECIEPT_TITLE");
            objCommon.FillDropDownList(ddlreceipt, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE NOT IN ('MIS')", "RECIEPT_TITLE");
            //AND RECIEPT_CODE='TF'
            pnlsingleinstallment.Visible = true;
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
            this.objCommon.FillDropDownList(ddlsearchDisc, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
            //ddlsearchDisc.SelectedIndex = 1;
            ddlsearchDisc_SelectedIndexChanged(sender, e);
            ddlSearch.SelectedIndex = 1;
            ddlSearch_SelectedIndexChanged(sender, e);

            }
        }

    private void CheckPageAuthorization()
        {
        if (Request.QueryString["pageno"] != null)
            {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
                }
            }
        else
            {
            Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
            }
        }
    #endregion

    #region private Methods

    //private void PopulateDropDownList()
    //{
    //    //objCommon.FillDropDownList(ddlSemester, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSION_PNAME DESC");
    //   // objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO IN (1,2,3)", "RCPTTYPENO");
    //}

    private void showdetails()
        {
        DataSet ds = new DataSet();
        //Show Total Demand
        string RecieptCode = Convert.ToString(ddlReceiptType.SelectedValue);

        string demand = "";
        int idnodcr = Convert.ToInt32(Session["stuinfoidno"]);
        demand = (objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)TOTAL_AMT", "SEMESTERNO = " + ddlSemester.SelectedValue + " and IDNO=" + idnodcr + "and RECIEPT_CODE='" + RecieptCode + "'"));

        if (demand != "")
            {

            int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'AND ISNULL(INSTAL_CANCEL,0)=0"));
            if (count > 0)
                {

                objCommon.DisplayMessage(upBulkInstalment, "Installment Already Created for Selected Criteria.", this.Page);

                ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), idnodcr, ViewState["receipt_code"].ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                    divStudInfo.Visible = true;
                    pnlStudinstalment.Visible = false;
                    btnRemove.Visible = true;
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.PopulateStudentInfoSection(dr);

                    }
                }
            else
                {
                ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), idnodcr, ViewState["receipt_code"].ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                    btnRemove.Visible = false;
                    divStudInfo.Visible = true;
                    pnlStudinstalment.Visible = false;
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.PopulateStudentInfoSection(dr);

                    }
                }

            }
        else
            {
            objCommon.DisplayMessage(upBulkInstalment, "Demand not found for this Receipt Type.", this.Page);
            ClearControl();
            }
        }

    private void PopulateStudentInfoSection(DataRow dr)
        {
        try
            {
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblStudName.ToolTip = dr["ENROLLNO"].ToString();
            lblidno.ToolTip = dr["IDNO"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblDegree.ToolTip = dr["DEGREENO"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            // lblSemester.Text = dr["YEARWISE"].ToString() == "1" ? dr["YEARNAME"].ToString() : dr["SEMESTERNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            ViewState["PAYTYPENAME"] = dr["PAYTYPENAME"].ToString();
            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            lblDemand.Text = dr["INSTALLMENT_AMT"].ToString();
            lblPayDemand.Text = dr["TOTAL_AMT"].ToString();
            lblinstalment.Text = dr["TOTAL_INSTALMENT"].ToString();
            hdfDmno.Value = dr["DM_NO"].ToString();
            txtRemark.Text = dr["REMARK_BY_AUTHORITY"].ToString();
            lblRegno.Text = dr["REGNO"].ToString();
            //DataSet dslist = objCommon.FillDropDown("ACD_STUDENT_FEES_INSTALLMENT", "NO_OF_INSTALL", "SESSION_NO", "IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()), "");
            //if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
            //{


            DataSet dslist = objCommon.FillDropDown("ACD_FEES_INSTALLMENT", "CONVERT(DATETIME, DUE_DATE, 103) DUE_DATE", "INSTALL_AMOUNT,INSTALMENT_NO,ISNULL(RECON,0) RECON", "IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dr["SEMESTERNO"].ToString()) + " AND RECIPTCODE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "'AND ISNULL(INSTAL_CANCEL,0)=0", "");
            if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
                {
                DataTable dt = dslist.Tables[0];

                pnlStudinstalment.Visible = true;
                grdinstalment.Visible = true;
                btnRemove.Visible = true;
                SetInitialRow();
                btnsubmit.Visible = true;
                btnsubmit.Enabled = true;
                //SetBindPreviousData();
                ViewState["CurrentTable"] = dt;
                grdinstalment.DataSource = dslist;
                grdinstalment.DataBind();


                }
            else
                {
                int dcr_recon = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(*)", "IDNO =" + Convert.ToInt32(dr["IDNO"].ToString()) + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND RECIEPT_CODE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "' AND ISNULL(LOCKED,0) = 1"));
                if (dcr_recon > 0)
                    {
                    DataSet ds = objCommon.FillDropDown("ACD_DCR", "REC_DT AS DUE_DATE", "TOTAL_AMT AS INSTALL_AMOUNT,ROW_NUMBER() OVER(ORDER BY IDNO) AS INSTALMENT_NO,RECON", "IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dr["SEMESTERNO"].ToString()) + " AND RECIEPT_CODE='" + Convert.ToString(ddlReceiptType.SelectedValue) + "' AND ISNULL(LOCKED,0) = 1", "");

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                        DataTable dt = ds.Tables[0];

                        pnlStudinstalment.Visible = true;
                        grdinstalment.Visible = true;
                        btnRemove.Visible = true;
                        SetInitialRow();
                        //SetBindPreviousData();
                        ViewState["CurrentTable"] = dt;
                        grdinstalment.DataSource = ds;
                        grdinstalment.DataBind();
                        }
                    //btnsubmit.Enabled = false;
                    //pnlStudinstalment.Visible = true;
                    return;
                    }

                pnlStudinstalment.Visible = true;
                grdinstalment.Visible = true;
                btnRemove.Visible = false;
                //grdinstalment.DataSource = dslist;
                SetInitialRow();
                SetPreviousData();
                grdinstalment.DataBind();
                btnsubmit.Visible = true;
                btnsubmit.Enabled = true;

                btncancel.Visible = true;
                }
            }
        catch (Exception ex)
            {
            }
        }

    private void ClearControl()
        {
        pnlStudinstalment.Visible = false;

        ddlReceiptType.SelectedIndex = 0;
        //ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        divStudInfo.Visible = false;
        grdinstalment.Visible = false;
        }
    #endregion

    #region Button Click Functionality

    protected void btnShow_Click(object sender, EventArgs e)
        {
        DataSet ds = new DataSet();
        try
            {

            int idnodcr = Convert.ToInt32(Session["stuinfoidno"]);

            double DEMANDAMOUNT = Convert.ToDouble(objCommon.LookUp("ACD_DEMAND", "ISNULL(SUM(TOTAL_AMT),0)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIEPT_CODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "' AND CAN=0"));

            double DCRAMOUNT = Convert.ToDouble(objCommon.LookUp("ACD_DCR", "ISNULL(SUM(TOTAL_AMT),0)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIEPT_CODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'AND CAN=0"));

            if (DEMANDAMOUNT == DCRAMOUNT)
                {
                objCommon.DisplayMessage(upBulkInstalment, "Fees Has Been Paid For Selected Criteria.", this.Page);
                return;
                }



            string recieptcode = Convert.ToString(ddlReceiptType.SelectedValue);
            //string recieptcode = Convert.ToString(objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO = " + ddlReceiptType.SelectedValue));
            ViewState["receipt_code"] = Convert.ToString(ddlReceiptType.SelectedValue);
            int dcr_recon = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(*)", "IDNO =" + idnodcr + " AND SEMESTERNO=" + ddlSemester.SelectedValue + "AND RECIEPT_CODE='" + recieptcode + "'"));
            if (dcr_recon > 0)
                {
                //int RECON = Convert.ToInt32(objCommon.LookUp("ACD_STUD_INSTALMENT_AMOUNT", "count(*)", "IDNO = '" + idnodcr + "' AND SESSION_NO=" + ddlSemester.SelectedValue + "' AND RECIEPT_NO=" + ddlReceiptType.SelectedValue + "'"));
                int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'AND ISNULL(INSTAL_CANCEL,0)=0"));
                if (count > 0)
                    {
                    objCommon.DisplayMessage(upBulkInstalment, "Installment Already Created for Selected Criteria.", this.Page);

                    //objCommon.DisplayMessage(upBulkInstalment, "Amount Already Paid For this Selection Criteria.", this.Page);
                    ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), idnodcr, recieptcode);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                        divStudInfo.Visible = true;
                        pnlStudinstalment.Visible = true;
                        //  btnsubmit.Visible = true;
                        btnRemove.Visible = true;
                        //  btncancel.Visible = true;
                        //btnsubmit.Enabled = false;
                        btnRemove.Visible = true;
                        DataRow dr = ds.Tables[0].Rows[0];
                        this.PopulateStudentInfoSection(dr);

                        }
                    }
                else
                    {

                    ds = objSC.GetStudentInfoInstalmentListByEnrollNo(Convert.ToInt32(ddlSemester.SelectedValue), idnodcr, ViewState["receipt_code"].ToString());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                        divStudInfo.Visible = true;
                        pnlStudinstalment.Visible = true;
                        //  btnsubmit.Visible = true;
                        btnRemove.Visible = true;
                        //  btncancel.Visible = true;
                        //btnsubmit.Enabled = false;
                        btnRemove.Visible = true;
                        DataRow dr = ds.Tables[0].Rows[0];
                        this.PopulateStudentInfoSection(dr);

                        }
                    }
                }
            else
                {

                showdetails();

                }
            }


        catch (Exception ex)
            {
            }
        }

    protected void submit_Click(object sender, EventArgs e)
        {
        try
            {

            int idnodcr = Convert.ToInt32(Session["stuinfoidno"]);
            int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
            if (count > 0)
                {
                CustomStatus cs = (CustomStatus)objSC.DeleteStudentInstalment(idnodcr, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue));

                }

            decimal TotalSum = 0.00M;
            foreach (GridViewRow item in grdinstalment.Rows)
                {
                TextBox txtamount = item.FindControl("txtAmount") as TextBox;
                if (txtamount.Enabled == true)
                    {
                    TotalSum += txtamount.Text == string.Empty ? 0 : Convert.ToDecimal(txtamount.Text);
                    }
                }

            if (TotalSum.ToString() == lblDemand.Text.ToString())
                {
                foreach (GridViewRow item in grdinstalment.Rows)
                    {
                    TextBox txtamount = item.FindControl("txtAmount") as TextBox;
                    if (txtamount.Enabled == true)
                        {
                        SubmitDatarow(item);
                        }
                    }

                ddlReceiptType.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
                }
            else
                {
                objCommon.DisplayMessage(upBulkInstalment, "Installment Amount not matched with Total Installment Amount. Insert Proper Calculated Amount.", this.Page);
                }

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.submit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    private void SubmitDatarow(GridViewRow dRow)
        {
        try
            {
            int idnodcr = Convert.ToInt32(Session["stuinfoidno"]);
            //int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONNO = " + ddlSemester.SelectedValue));
            string recieptcode = Convert.ToString(ddlReceiptType.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

            string remark = txtRemark.Text == string.Empty ? null : txtRemark.Text;
            int installment = Convert.ToInt32(dRow.Cells[1].Text);//Get Bounded value from GridView.

            int totalcount = Convert.ToInt32(grdinstalment.Rows.Count);
            int dmno = Convert.ToInt32(hdfDmno.Value);

            int usertype = Convert.ToInt32(Session["usertype"]);
            int userno = Convert.ToInt32(Session["userno"]);
            int CollegeCode = Convert.ToInt32(Session["colcode"]);

            TextBox txtduedate = dRow.FindControl("txtDueDate") as TextBox;
            TextBox txtamount = dRow.FindControl("txtAmount") as TextBox;
            Label lblStatus = dRow.FindControl("lblStatus") as Label;

            string date = Convert.ToString(txtduedate.Text);
            decimal amount = txtamount.Text == string.Empty ? 0 : Convert.ToDecimal(txtamount.Text);
            decimal totalamount = lblDemand.Text == string.Empty ? 0 : Convert.ToDecimal(lblDemand.Text);
            string status = Convert.ToString(lblStatus.Text);

            int recon = 0;
            if (status == "Paid")
                {
                recon = 1;
                }
            else
                {
                recon = 0;
                }
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_INSTALMENT_AMOUNT", " count(*)", "SESSION_NO = '" + ddlSemester.SelectedValue + "' and IDNO='" + idnodcr + "'and RECIEPT_NO='" + ddlReceiptType.SelectedValue + "'"));
            if (ddlSemester.SelectedItem != null && ddlReceiptType.SelectedItem != null)
                {
                CustomStatus cs = (CustomStatus)objSC.InsertStudentInstalment((idnodcr), Semesterno, date, amount, totalamount, installment, remark, totalcount, usertype, recieptcode, dmno, recon, userno, CollegeCode, Convert.ToString(Session["ipAddress"]));
                objCommon.DisplayMessage(upBulkInstalment, "Student Installment Data Saved Successfully", this.Page);
                pnlStudinstalment.Visible = false;
                //btnsubmit.Enabled = false;
                divStudInfo.Visible = false;

                }
            }
        catch (Exception ex)
            {
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }

    #endregion

    #region Gridview
    private void SetInitialRow()
        {
        DataTable dt = new DataTable();
        DataRow dr = null;
        //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("RECON", typeof(string)));
        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["INSTALMENT_NO"] = 1;
        dr["DUE_DATE"] = string.Empty;
        dr["INSTALL_AMOUNT"] = string.Empty;
        dr["RECON"] = 0;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        grdinstalment.DataSource = dt;
        grdinstalment.DataBind();
        }

    private void SetInitialRow1()
        {
        DataTable dt = new DataTable();
        DataRow dr = null;
        //dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
        dt.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
        dt.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
        dr = dt.NewRow();
        //dr["RowNumber"] = 1;
        dr["INSTALMENT_NO"] = 1;
        dr["DUE_DATE"] = string.Empty;
        dr["INSTALL_AMOUNT"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        //ViewState["CurrentTable"] = dt;

        grdinstalment.DataSource = dt;
        grdinstalment.DataBind();

        }

    private void SetPreviousData()
        {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
            {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
                {
                for (int i = 0; i < dt.Rows.Count; i++)
                    {
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

                    box1.Text = dt.Rows[i]["DUE_DATE"].ToString();
                    box2.Text = dt.Rows[i]["INSTALL_AMOUNT"].ToString();

                    rowIndex++;
                    }
                }
            }
        }

    private void SetBindPreviousData()
        {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
            {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
                {
                for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

                    box1.Text = dt.Rows[i]["DUE_DATE"].ToString();
                    box2.Text = dt.Rows[i]["INSTALL_AMOUNT"].ToString();

                    AddNewRowToGrid1();
                    rowIndex++;
                    }
                }
            }
        }


    protected void btnAdd_Click(object sender, EventArgs e)
        {
        AddNewRowToGrid();
        }

    protected void lnkRemove_Click(object sender, ImageClickEventArgs e)
        {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (rowID > 0)
            {
            if (ViewState["CurrentTable"] != null)
                {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                    {
                    if (gvRow.RowIndex <= dt.Rows.Count)
                        {
                        //Remove the Selected Row data
                        dt.Rows.Remove(dt.Rows[rowID]);
                        }
                    }
                //Store the current data in ViewState for future reference
                ViewState["CurrentTable"] = dt;
                //Re bind the GridView for the updated data
                grdinstalment.DataSource = dt;
                grdinstalment.DataBind();

                //Set Previous Data on Postbacks
                SetPreviousData();
                }
            }
        btnsubmit.Visible = true;
        btncancel.Visible = true;
        }

    private void AddNewRowToGrid()
        {
        int rowIndex = 0;
        int recon;
        if (ViewState["CurrentTable"] != null)
            {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
                {
                DataTable dtNewTable = new DataTable();
                //dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALMENT_NO", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("RECON", typeof(string)));

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");
                    Label lbl1 = (Label)grdinstalment.Rows[rowIndex].Cells[3].FindControl("lblStatus");
                    if (lbl1.Text == "Paid")
                        {
                        recon = 1;
                        }
                    else
                        {
                        recon = 0;
                        }

                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                        {
                        drCurrentRow = dtNewTable.NewRow();

                        //drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["INSTALMENT_NO"] = i;
                        drCurrentRow["DUE_DATE"] = box1.Text;
                        drCurrentRow["INSTALL_AMOUNT"] = box2.Text;
                        drCurrentRow["RECON"] = recon;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                        }
                    else
                        {
                        if (box1.Text.Trim() == string.Empty)
                            {
                            objCommon.DisplayUserMessage(upBulkInstalment, "Please select due date.", this.Page);
                            }
                        if (box2.Text.Trim() == string.Empty)
                            {
                            objCommon.DisplayUserMessage(upBulkInstalment, "Please enter amount.", this.Page);
                            }

                        return;
                        }
                    }

                drCurrentRow = dtNewTable.NewRow();
                //drCurrentRow["RowNumber"] = 1;
                drCurrentRow["INSTALMENT_NO"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["DUE_DATE"] = string.Empty;
                drCurrentRow["INSTALL_AMOUNT"] = string.Empty;
                drCurrentRow["RECON"] = 0;
                
                dtNewTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtNewTable;
                grdinstalment.DataSource = dtNewTable;
                grdinstalment.DataBind();

                // SetPreviousData();
                }
            else
                {
                objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
                }
            }
        else
            {
            Response.Write("ViewState is null");
            }
        }


    //private void AddNewRowToGrid()
    //{
    //    int rowIndex = 0;
    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        DataRow drCurrentRow = null;
    //        if (dtCurrentTable.Rows.Count > 0)
    //        {
    //            DataTable dtNewTable = new DataTable();
    //            dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
    //            dtNewTable.Columns.Add(new DataColumn("InstalmentNo", typeof(string)));
    //            dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
    //            dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
    //            drCurrentRow = dtNewTable.NewRow();

    //            drCurrentRow["RowNumber"] = 1;
    //            drCurrentRow["InstalmentNo"] = 1;
    //            drCurrentRow["Column1"] = string.Empty;
    //            drCurrentRow["Column2"] = string.Empty;

    //            dtNewTable.Rows.Add(drCurrentRow);

    //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
    //            {
    //                //extract the TextBox values
    //                TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
    //                TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

    //                if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
    //                {
    //                    drCurrentRow = dtNewTable.NewRow();

    //                    drCurrentRow["RowNumber"] = i + 1;
    //                    drCurrentRow["InstalmentNo"] = i + 1;
    //                    drCurrentRow["Column1"] = box1.Text;
    //                    drCurrentRow["Column2"] = box2.Text;

    //                    rowIndex++;
    //                    dtNewTable.Rows.Add(drCurrentRow);
    //                }
    //                else
    //                {
    //                    return;
    //                }
    //            }
    //            ViewState["CurrentTable"] = dtNewTable;
    //            grdinstalment.DataSource = dtNewTable;
    //            grdinstalment.DataBind();

    //            SetPreviousData();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("ViewState is null");
    //    }
    //}


    private void AddNewRowToGrid1()
        {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
            {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
                {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("InstalmentNo", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("DUE_DATE", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("INSTALL_AMOUNT", typeof(string)));
                drCurrentRow = dtNewTable.NewRow();

                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["InstalmentNo"] = 1;
                drCurrentRow["DUE_DATE"] = string.Empty;
                drCurrentRow["INSTALL_AMOUNT"] = string.Empty;

                dtNewTable.Rows.Add(drCurrentRow);

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)grdinstalment.Rows[rowIndex].Cells[1].FindControl("txtDueDate");
                    TextBox box2 = (TextBox)grdinstalment.Rows[rowIndex].Cells[2].FindControl("txtAmount");

                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                        {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["InstalmentNo"] = i + 1;
                        drCurrentRow["DUE_DATE"] = box1.Text;
                        drCurrentRow["INSTALL_AMOUNT"] = box2.Text;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                        }
                    else
                        {
                        return;
                        }
                    }
                ViewState["CurrentTable"] = dtNewTable;
                grdinstalment.DataSource = dtNewTable;
                grdinstalment.DataBind();

                SetBindPreviousData();
                }
            else
                {
                objCommon.DisplayMessage(upBulkInstalment, "Maximum Installment Limit Reached", this.Page);
                }
            }
        else
            {
            Response.Write("ViewState is null");
            }
        }
    #endregion

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlReceiptType.SelectedIndex > 0)
            {
            int id_no = Convert.ToInt32(Session["stuinfoidno"]);
            objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO)", "DISTINCT D.SEMESTERNO", "S.SEMESTERNAME", "D.RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND D.IDNO = '" + id_no + "'", "D.SEMESTERNO");
            ddlReceiptType.Focus();
            }
        else
            {
            ddlSemester.Focus();
            ddlReceiptType.SelectedIndex = 0;
            }
        }

    protected void btnClear_Click(object sender, EventArgs e)
        {
        //Response.Redirect(Request.Url.ToString());

        ddlSemester.SelectedIndex = 0;
        ddlReceiptType.SelectedIndex = 0;
        divStudInfo.Visible = false;
        }

    protected void btnRemove_Click(object sender, EventArgs e)
        {
        int idnodcr = Convert.ToInt32(Session["stuinfoidno"]);

        int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO= " + idnodcr + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));
        if (count > 0)
            {
            CustomStatus cs = (CustomStatus)objSC.DeleteStudentInstalment(idnodcr, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue));
            objCommon.DisplayMessage(upBulkInstalment, "Student Installment Data Remove Successfully", this.Page);
            pnlStudinstalment.Visible = false;
            btnsubmit.Enabled = false;
            //  divStudInfo.Visible = false;
            btnRemove.Visible = false;
            ddlReceiptType.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            }
        }
    protected void rblisintallmentconfig_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            if (rblisintallmentconfig.SelectedValue == "1")
                {
                pnlsingleinstallment.Visible = true;
                pnlBulkInstallment.Visible = false;
                pnlBulkDiscount.Visible = false;
                pnlSingleDiscount.Visible = false;
                divStudInfoDis.Visible = false;
                ddlsearchDisc.SelectedIndex = 0;
                divpanel.Visible = false;
                }
            else if (rblisintallmentconfig.SelectedValue == "2")
                {
                pnlsingleinstallment.Visible = false;
                pnlBulkInstallment.Visible = true;
                pnlBulkDiscount.Visible = false;
                divpanel.Visible = false;
                ddlsearchDisc.SelectedIndex = 0;
                pnlSingleDiscount.Visible = false;
                divStudInfoDis.Visible = false;
                PopulatedDropDownFill();
                //ShowData();
                //objCommon.FillDropDownList(ddlpayment, "ACD_PAYMENT_GATEWAY", "PAYID", "PAY_GATEWAY_NAME", "PAYID > 0", "PAYID DESC");
                //objCommon.FillDropDownList(ddlActivityname, "ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME", "ACTIVITYNO > 0", "ACTIVITYNO");
                }
            else if (rblisintallmentconfig.SelectedValue == "3")
                {
                pnlBulkDiscount.Visible = false;
                pnlsingleinstallment.Visible = false;
                pnlBulkInstallment.Visible = false;
                divpanel.Visible = false;
                ddlsearchDisc.SelectedIndex = 0;
                divStudInfoDis.Visible = false;
                pnlSingleDiscount.Visible = true;
                ddlschool.SelectedIndex = 0;
                ddldegreedis.SelectedIndex = 0;
                ddlbranchdis.SelectedIndex = 0;
                ddlsemesterdis.SelectedIndex = 0;
                ddlreceiptdis.SelectedIndex = 0;
                ddlpaymentdis.SelectedIndex = 0;
                btnsubmitdis.Visible = false;
                divConcessionOption.Visible = false;
                LvDiscount.DataSource = null;
                LvDiscount.DataBind();
                objCommon.FillDropDownList(ddlpaymentsingle, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");
                }
            else if (rblisintallmentconfig.SelectedValue == "4")
                {
                pnlBulkDiscount.Visible = true;
                pnlsingleinstallment.Visible = false;
                pnlBulkInstallment.Visible = false;
                divpanel.Visible = false;
                Panel2.Visible = false;
                pnlSingleDiscount.Visible = false;
                PopulatedDropDownFillDiscount();
                btnSubmitSingleDis.Visible = false;
                divConcessionOptionSingle.Visible = false;
                LvDiscountSingle.DataSource = null;
                LvDiscountSingle.DataBind();
                ddlreceipt.SelectedIndex = 0;
                ddlsemesterShow.SelectedIndex = 0;
                divStudInfoDis.Visible = false;
                ddlsearchDisc.SelectedIndex = 0;
                Panel3.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private void PopulatedDropDownFill()
        {
        objCommon.FillDropDownList(ddlBulkReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE NOT IN ('MIS')", "RECIEPT_TITLE");
        objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID > 0 AND ActiveStatus=1", "COLLEGE_NAME");
        //if (Convert.ToInt32(Session["OrgId"]) == 1)
        //{
        //    ddlColg.SelectedValue = "1";
        //}
        objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0 AND ISNULL(ACTIVESTATUS,0)=1", "INSTALLMENT_NO");
        objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");
        }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
                {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    divpanel.Visible = true;
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                        {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;
                        rfvDDL.Enabled = true;
                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                        }
                    else
                        {
                        rfvSearchtring.Enabled = true;
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
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
    protected void lnkId_Click(object sender, EventArgs e)
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

        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        ViewState["idno"] = Session["stuinfoidno"].ToString();

        //DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

        ////Server.Transfer("PersonalDetails.aspx", false);
        //DisplayInformation(Convert.ToInt32(Session["stuinfoidno"]));
        lblStudName.Text = lnk.Text.Trim();

        lblDegree.Text = ViewState["DEGREENAME"].ToString();
        lblRegno.Text = ViewState["REGNO"].ToString();
        lblBranch.Text = ViewState["BRANCHNAME"].ToString();
        lblSemester.Text= ViewState["SEMESTERNO"].ToString();
        lblPaymentType.Text = ViewState["PAYTYPENAME"].ToString();
        lblidno.Text = Session["stuinfoidno"].ToString();
        divStudInfo.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        pnlStudinstalment.Visible = false;
        btnsubmit.Visible = false;
        btnRemove.Visible = false;
        btncancel.Visible = false;
        ddlSearch.SelectedIndex = 0;
        }
    protected void btnSearch_Click(object sender, EventArgs e)
        {

        lblNoRecords.Visible = true;
        divStudInfo.Visible = false;
        //
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            {
            value = ddlDropdown.SelectedValue;
            }
        else
            {
            value = txtSearch.Text;
            }
        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        pnltextbox.Visible = false;
        pnlDropdown.Visible = false;
        // divpanel.Attributes.Add("style", "display:none");
        divMsg.Visible = false;
        }
    private void bindlist(string category, string searchtext)
        {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
            {
            Panel3.Visible = true;

            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            ViewState["DEGREENAME"] = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            ViewState["REGNO"] = ds.Tables[0].Rows[0]["ENROLLMENTNO"].ToString();
            ViewState["BRANCHNAME"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["semestername"].ToString();
            ViewState["PAYTYPENAME"] = ds.Tables[0].Rows[0]["PAYTYPENAME"].ToString();

            }
        else
            {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            }
        }
    protected void btnClose_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlColg.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENAME");
            ddlDegree.Focus();
            }
        else
            {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            }
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlDegree.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "B.LONGNAME");
            ddlBranch.SelectedIndex = 0;
            ddlBranch.Focus();
            }
        else
            {
            ddlBranch.SelectedIndex = 0;
            }

        }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlBranch.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlBulkSemester, "ACD_SEMESTER S INNER JOIN ACD_DEMAND D  ON(D.SEMESTERNO = S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "D.BRANCHNO =" + Convert.ToInt32(ddlBranch.SelectedValue), "S.SEMESTERNO");
            ddlBulkSemester.SelectedIndex = 0;
            ddlBulkSemester.Focus();
            }
        else
            {
            ddlBulkSemester.SelectedIndex = 0;
            }
        }
    protected void btnBulkShow_Click(object sender, EventArgs e)
        {
        try
            {
            ShowStudents();

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnBulkShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    private void ShowStudents()
        {
        DataSet ds = objSC.ShowBulkInstallmentStudents(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlBulkSemester.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), Convert.ToString(ddlBulkReceiptType.SelectedValue));

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

            lvBulkStudent.DataSource = ds;
            lvBulkStudent.DataBind();

            }
        else
            {
            objCommon.DisplayMessage(upBulkInstalment, "No Students for selected criteria", this.Page);

            lvBulkStudent.DataSource = null;
            lvBulkStudent.DataBind();
            }
        }
    protected void btnBulkCancel_Click(object sender, EventArgs e)
        {
        ddlDegree.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlBulkSemester.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        ddlinstallmenttype.SelectedIndex = 0;
        lvBulkStudent.DataSource = null;
        lvBulkStudent.DataBind();
        div1st.Visible = false;
        div2nd.Visible = false;
        div3rd.Visible = false;
        div4th.Visible = false;
        div5th.Visible = false;
        }
    protected void btnSave_Click(object sender, EventArgs e)
        {
        try
            {
            // ADDED BY: M. REHBAR SHEIKH ON 19-06-2019

            bool flag = false;
            Student objS = new Student();
            foreach (ListViewDataItem item in lvBulkStudent.Items)
                {

                CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                    {
                    flag = true;

                    objS.IdNo = Convert.ToInt32(cbRow.ToolTip);
                    objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objS.College_ID = Convert.ToInt32(ddlColg.SelectedValue);
                    objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                    objS.SemesterNo = Convert.ToInt32(ddlBulkSemester.SelectedValue);
                    objS.PayTypeNO = Convert.ToInt32(ddlPaymentType.SelectedValue);
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                    string ipAddress = Request.ServerVariables["REMOTE_HOST"];

                    int InstallmentNO = Convert.ToInt32(ddlinstallmenttype.SelectedValue);
                    objS.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0"));

                    string Installmenttype = string.Empty;
                    Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
                    Installmenttype = Installmenttype + '-';
                    int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));

                    string Date_string = string.Empty;

                    if (count == 2)
                        {

                        Date_string = txtDuedate1.Text.Trim() + ',' + txtDuedate2.Text.Trim();
                        }
                    if (count == 3)
                        {

                        Date_string = txtDuedate1.Text.Trim() + ',' + txtDuedate2.Text.Trim() + ',' + txtDuedate3.Text.Trim();
                        }
                    if (count == 4)
                        {

                        Date_string = txtDuedate1.Text.Trim() + ',' + txtDuedate2.Text.Trim() + ',' + txtDuedate3.Text.Trim() + ',' + txtDuedate4.Text.Trim();
                        }
                    if (count == 5)
                        {


                        Date_string = txtDuedate1.Text.Trim() + ',' + txtDuedate2.Text.Trim() + ',' + txtDuedate3.Text.Trim() + ',' + txtDuedate4.Text.Trim() + ',' + txtDuedate5.Text.Trim();
                        }


                    CustomStatus cs = (CustomStatus)objSC.bulkStudentInstallmentCreate(objS, Convert.ToInt32(ddlinstallmenttype.SelectedValue), ipAddress, Convert.ToString(ddlBulkReceiptType.SelectedValue), Date_string);

                    if (cs.Equals(CustomStatus.RecordSaved))
                        {
                        objCommon.DisplayMessage(this.upBulkInstalment, "Installment Created Successfully", this.Page);
                        ShowStudents();
                        }
                    else
                        {
                        objCommon.DisplayMessage(this.upBulkInstalment, "Error", this.Page);
                        return;
                        }
                    }
                }

            if (flag == false)
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Please select atleast single student", this.Page);
                return;
                }


            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void ddlinstallmenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlinstallmenttype.SelectedIndex > 0)
            {
            string Installmenttype = string.Empty;
            Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
            Installmenttype = Installmenttype + '-';
            int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));

            if (count == 2)
                {
                div1st.Visible = true;
                div2nd.Visible = true;
                div3rd.Visible = false;
                div4th.Visible = false;
                div5th.Visible = false;
                }

            if (count == 3)
                {
                div1st.Visible = true;
                div2nd.Visible = true;
                div3rd.Visible = true;
                div4th.Visible = false;
                div5th.Visible = false;
                }

            if (count == 4)
                {
                div1st.Visible = true;
                div2nd.Visible = true;
                div3rd.Visible = true;
                div4th.Visible = true;
                div5th.Visible = false;
                }


            if (count == 5)
                {
                div1st.Visible = true;
                div2nd.Visible = true;
                div3rd.Visible = true;
                div4th.Visible = true;
                div5th.Visible = true;
                }
            //txtduedate1.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtduedate1.Enabled = false;
            }
        else
            {
            div1st.Visible = false;
            div2nd.Visible = false;
            div3rd.Visible = false;
            div4th.Visible = false;
            div5th.Visible = false;
            }
        }
    protected void btnExcelReport_Click(object sender, EventArgs e)
        {
        try
            {

            GridView GV = new GridView();
            string ContentType = string.Empty;

            DataSet ds = objSC.ShowBulkInstallmentStudentsExcelReport(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlBulkSemester.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), Convert.ToString(ddlBulkReceiptType.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
                {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=InstallmentAllotmentList.xls";
                //string attachment = "attachment; filename=AdmissionRegisterStudents.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
                }
            else
                {
                objCommon.DisplayUserMessage(upBulkInstalment, "No Record Found.", this.Page);
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    //discount tab added by aashna 09-11-2022
    private void PopulatedDropDownFillDiscount()
        {
        objCommon.FillDropDownList(ddlreceiptdis, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0 AND RECIEPT_CODE NOT IN ('MIS')", "RECIEPT_TITLE");
        objCommon.FillDropDownList(ddlschool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID > 0 AND ActiveStatus=1", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlpaymentdis, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");

        }
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlschool.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddldegreedis, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlschool.SelectedValue, "D.DEGREENAME");
            ddldegreedis.Focus();
            Panel2.Visible = false;
            LvDiscount.DataSource = null;
            LvDiscount.DataBind();
            }
        else
            {
            ddldegreedis.SelectedIndex = 0;
            ddlbranchdis.SelectedIndex = 0;
            }
        ddldegreedis.SelectedIndex = 0;
        ddlbranchdis.SelectedIndex = 0;
        }
    protected void ddldegreedis_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddldegreedis.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlbranchdis, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddldegreedis.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlschool.SelectedValue), "B.LONGNAME");
            ddlbranchdis.SelectedIndex = 0;
            ddlbranchdis.Focus();
            Panel2.Visible = false;
            LvDiscount.DataSource = null;
            LvDiscount.DataBind();
            }
        else
            {
            ddlBranch.SelectedIndex = 0;
            }
        }
    protected void ddlbranchdis_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlbranchdis.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlsemesterdis, "ACD_SEMESTER S INNER JOIN ACD_DEMAND D  ON(D.SEMESTERNO = S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "D.BRANCHNO =" + Convert.ToInt32(ddlbranchdis.SelectedValue), "S.SEMESTERNO");
            ddlsemesterdis.SelectedIndex = 0;
            ddlsemesterdis.Focus();
            Panel2.Visible = false;
            LvDiscount.DataSource = null;
            LvDiscount.DataBind();
            }
        else
            {
            ddlsemesterdis.SelectedIndex = 0;
            }
        }
    protected void btnshowdis_Click(object sender, EventArgs e)
        {
        try
            {

            int FACULTY = Convert.ToInt32(ddlschool.SelectedValue);
            divConcessionOption.Visible = true;
            rdConcessionOption.SelectedValue = null;
            DataSet dsS = objSC.ShowBulkDiscountStudents(Convert.ToInt32(ddlschool.SelectedValue), Convert.ToInt32(ddldegreedis.SelectedValue), Convert.ToInt32(ddlbranchdis.SelectedValue), Convert.ToInt32(ddlsemesterdis.SelectedValue), Convert.ToInt32(ddlpaymentdis.SelectedValue), Convert.ToString(ddlreceiptdis.SelectedValue));
            if (dsS.Tables.Count > 0 && dsS.Tables[0].Rows.Count > 0)
                {
                btnsubmitdis.Visible = true;
                divConcessionOption.Visible = true;
                rdConcessionOption.SelectedValue = null;
                Panel2.Visible = true;
                LvDiscount.DataSource = dsS;
                LvDiscount.DataBind();
                }
            else
                {
                btnsubmitdis.Visible = false;
                Panel2.Visible = false;
                divConcessionOption.Visible = false;
                objCommon.DisplayMessage(upBulkInstalment, "No Students for selected criteria", this.Page);
                LvDiscount.DataSource = null;
                LvDiscount.DataBind();
                }
            string SP_Name1 = "PKG_ACD_BIND_DROP_DOWN_LISTS";
            string SP_Parameters1 = "@P_OUTPUT";
            string Call_Values1 = "" + 1;
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
            foreach (ListViewDataItem dataitem in LvDiscount.Items)
                {
                CheckBox chktransfer = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList ddlDiscount = dataitem.FindControl("ddlDiscount") as DropDownList;
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                    ddlDiscount.Items.Clear();
                    ddlDiscount.Items.Add("Please Select");
                    ddlDiscount.SelectedItem.Value = "0";
                    ddlDiscount.DataSource = ds.Tables[0];
                    ddlDiscount.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlDiscount.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlDiscount.DataBind();
                    ddlDiscount.SelectedIndex = 0;

                    }
                DropDownList ddlConcession = dataitem.FindControl("ddlConcession") as DropDownList;
                DropDownList dllSelectAllType = this.LvDiscount.Controls[0].FindControl("dllSelectAllType") as DropDownList;
                if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
                    {
                    ddlConcession.Items.Clear();
                    ddlConcession.Items.Add("Please Select");
                    ddlConcession.SelectedItem.Value = "0";
                    ddlConcession.DataSource = ds.Tables[1];
                    ddlConcession.DataValueField = ds.Tables[1].Columns[0].ToString();
                    ddlConcession.DataTextField = ds.Tables[1].Columns[1].ToString();
                    ddlConcession.DataBind();
                    ddlConcession.SelectedIndex = 0;
                    dllSelectAllType.Items.Clear();
                    dllSelectAllType.Items.Add("Please Select");
                    dllSelectAllType.SelectedItem.Value = "0";
                    dllSelectAllType.DataSource = ds.Tables[1];
                    dllSelectAllType.DataValueField = ds.Tables[1].Columns[0].ToString();
                    dllSelectAllType.DataTextField = ds.Tables[1].Columns[1].ToString();
                    dllSelectAllType.DataBind();
                    dllSelectAllType.SelectedIndex = 0;

                    }
                Label lblDiscount = dataitem.FindControl("lblDiscount") as Label;
                Label lblConcessionno = dataitem.FindControl("lblConcessionno") as Label;
                TextBox txtDiscountFee = dataitem.FindControl("txtDiscountFee") as TextBox;
                TextBox txtNetPayable = dataitem.FindControl("txtNetPayable") as TextBox;
                Label lbldcridno = dataitem.FindControl("lbldcridno") as Label;
                string DiscountFee = (txtDiscountFee.Text);
                string NetPayable = (txtNetPayable.Text);
                string dcridno = (lbldcridno.Text);
                ddlDiscount.SelectedItem.Text = lblDiscount.Text;
                ddlConcession.SelectedValue = lblConcessionno.Text;
                if (dcridno.ToString() != "0")
                    {
                    chktransfer.Enabled = false;
                    ddlDiscount.Enabled = false;
                    ddlConcession.Enabled = false;
                    lbldcridno.Text = "Utilize";
                    lbldcridno.CssClass = "lbl-green";
                    }
                else
                    {
                    lbldcridno.Text = "Not Utilize";
                    lbldcridno.CssClass = "lbl-red";
                    }
                }
            }
        catch (Exception ex)
            {
            }
        }
    protected void btnsubmitdis_Click(object sender, EventArgs e)
        {
        try
            {
            if (rdConcessionOption.SelectedValue == string.Empty)
                {
                objCommon.DisplayMessage(this.Page, "Please Select Payment Type (Amount Wise or Percentage Wise)", this.Page);
                return;
                }
            string StudentReg = string.Empty;

            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in LvDiscount.Items)
                {
                CheckBox chk = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList ddlDiscount = dataitem.FindControl("ddlDiscount") as DropDownList;
                DropDownList ddlConcession = dataitem.FindControl("ddlConcession") as DropDownList;
                Label lblreg = dataitem.FindControl("lblreg") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                Label lblIdno = dataitem.FindControl("lblIdno") as Label;
                TextBox lbltotal = dataitem.FindControl("lbltotal") as TextBox;
                TextBox txtDiscountFee = dataitem.FindControl("txtDiscountFee") as TextBox;
                HiddenField hdfDiscountFee = dataitem.FindControl("hdfDiscountFee") as HiddenField;
                HiddenField hdfNetPayable = dataitem.FindControl("hdfNetPayable") as HiddenField;
                TextBox txtNetPayable = dataitem.FindControl("txtNetPayable") as TextBox;
                Label lblDiscount = dataitem.FindControl("lblDiscount") as Label;
                //ddlDiscount.SelectedValue = "0";
                if (chk.Checked == true && chk.Enabled == true)
                    {
                    StudentReg += lblreg.Text + '$';


                    if (rdConcessionOption.SelectedValue != "1")
                        {
                        if (ddlDiscount.SelectedValue == "0")
                            {
                            objCommon.DisplayMessage(this.upBulkInstalment, "Please Select Discount.", this.Page);
                            rdConcessionOption.ClearSelection();
                            return;
                            }

                        if (ddlConcession.SelectedValue == "0")
                            {
                            objCommon.DisplayMessage(this.upBulkInstalment, "Please Select Discount Type.", this.Page);
                            rdConcessionOption.ClearSelection();
                            return;
                            }
                        }

                    if (hdfDiscountFee.Value == "" && hdfNetPayable.Value == "")
                        {
                        objCommon.DisplayMessage(this.upBulkInstalment, "Please Enter Discount Fee And Net Payable", this.Page);
                        rdConcessionOption.ClearSelection();
                        return;
                        }
                    else
                        {
                        //string Discount = "1";//Convert.ToString(ddlDiscount.SelectedItem.Text);
                        string Discount = Convert.ToString(ddlDiscount.SelectedItem.Text);
                        string Regno = lblreg.Text;
                        string name = lblname.Text;
                        decimal Totals = Convert.ToDecimal(lbltotal.Text);
                        int Total = Convert.ToInt32(Totals);
                        decimal DiscountFeee = Convert.ToDecimal(Convert.ToString(hdfDiscountFee.Value) == string.Empty ? "0" : hdfDiscountFee.Value);
                        decimal NetPayable = Convert.ToDecimal(Convert.ToString(hdfNetPayable.Value) == string.Empty ? "0" : hdfNetPayable.Value);//Convert.ToDecimal(txtNetPayable.Text);
                        int Concession_no = Convert.ToInt32(ddlConcession.SelectedValue);
                        int UA_NO = Convert.ToInt32(Session["userno"].ToString());
                        int Idno = Convert.ToInt32(lblIdno.Text);
                        cs = (CustomStatus)objSC.InsertFeesDiscount(Regno, Convert.ToInt32(ddlschool.SelectedValue), name, Totals, DiscountFeee, Discount, NetPayable, Convert.ToInt32(ddlsemesterdis.SelectedValue), UA_NO, Concession_no, Idno, Convert.ToInt32(rdConcessionOption.SelectedValue), Convert.ToString(ddlreceiptdis.SelectedValue), Convert.ToString(ddlpaymentdis.SelectedItem), Convert.ToInt32(Session["OrgId"]),Convert.ToInt32(lblreg.ToolTip));
                        }
                    }
                }
            if (StudentReg.ToString() == string.Empty)
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Please Select At List One.", this.Page);
                return;
                }

            if (cs.Equals(CustomStatus.RecordSaved))
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Record Saved Successfully.", this.Page);
                ddlschool.SelectedIndex = 0;
                ddldegreedis.SelectedIndex = 0;
                ddlbranchdis.SelectedIndex = 0;
                ddlsemesterdis.SelectedIndex = 0;
                ddlreceiptdis.SelectedIndex = 0;
                ddlpaymentdis.SelectedIndex = 0;
                btnsubmitdis.Visible = false;
                divConcessionOption.Visible = false;
                LvDiscount.DataSource = null;
                LvDiscount.DataBind();
                return;
                }
            else
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Failed To Save Record ", this.Page);
                }
            }
        catch
            {
            }
        }
    protected void btncanceldis_Click(object sender, EventArgs e)
        {
        ddlschool.SelectedIndex = 0;
        ddldegreedis.SelectedIndex = 0;
        ddlbranchdis.SelectedIndex = 0;
        ddlsemesterdis.SelectedIndex = 0;
        ddlreceiptdis.SelectedIndex = 0;
        ddlpaymentdis.SelectedIndex = 0;
        rdConcessionOption.Visible = false;
        btnsubmitdis.Visible = false;
        LvDiscount.DataSource = null;
        LvDiscount.DataBind();
        }
    protected void ddlsearchDisc_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            Panel3.Visible = false;
            lblNoRecordsDis.Visible = false;
            lvStudentDis.DataSource = null;
            lvStudentDis.DataBind();
            if (ddlsearchDisc.SelectedIndex > 0)
                {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlsearchDisc.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    divpanel.Visible = true;
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                        {
                        pnltextboxDis.Visible = false;
                        txtSearchDis.Visible = false;
                        pnlDropdownDis.Visible = true;
                        RequiredFieldValidator26.Enabled = true;
                        TxtSrchDis.Visible = false;
                        lblDropdownDis.Text = ddlsearchDisc.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdownDis, tablename, column1, column2, column1 + ">0", column1);
                        }
                    else
                        {
                        rfvSearchtring.Enabled = true;
                        pnltextboxDis.Visible = true;
                        TxtSrchDis.Visible = true;
                        txtSearchDis.Visible = true;
                        pnlDropdownDis.Visible = false;
                        }
                    }
                }
            else
                {
                pnltextboxDis.Visible = false;
                pnlDropdownDis.Visible = false;
                }
            }
        catch
            {
            throw;
            }
        }
    protected void btnSerchDis_Click(object sender, EventArgs e)
        {
        lblNoRecordsDis.Visible = true;
        string value = string.Empty;
        if (ddlDropdownDis.SelectedIndex > 0)
            {
            value = ddlDropdownDis.SelectedValue;

            }
        else
            {
            value = txtSearchDis.Text;
            LvDiscountSingle.Visible = false;
            LvDiscountSingle.DataSource = null;
            LvDiscountSingle.DataBind();
            }
        bindlistDis(ddlsearchDisc.SelectedItem.Text, value);
        ddlDropdownDis.ClearSelection();
        txtSearchDis.Text = string.Empty;
        pnltextboxDis.Visible = false;
        pnlDropdownDis.Visible = false;
        divMsg.Visible = false;

        }
    protected void btnCloseDis_Click(object sender, EventArgs e)
        {

        }
    private void bindlistDis(string category, string searchtext)
        {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);
        if (ds.Tables[0].Rows.Count > 0)
            {
            Panel4.Visible = true;
            lvStudentDis.Visible = true;
            lvStudentDis.DataSource = ds;
            lvStudentDis.DataBind();
            ViewState["DEGREENAME"] = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            ViewState["branchname"] = ds.Tables[0].Rows[0]["branchname"].ToString();
            ViewState["ENROLLMENTNO"] = ds.Tables[0].Rows[0]["ENROLLMENTNO"].ToString();
            ViewState["total_amt"] = ds.Tables[0].Rows[0]["total_amt"].ToString();
            lblNoRecordsDis.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
        else
            {
            lblNoRecordsDis.Text = "Total Records : 0";
            lvStudentDis.Visible = false;
            lvStudentDis.DataSource = null;
            lvStudentDis.DataBind();
            }
        }
    protected void lnkIdDis_Click(object sender, EventArgs e)
        {

        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();
        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
        HiddenField hdfdegree = lnk.Parent.FindControl("hdfdegree") as HiddenField;
        HiddenField hdfbranch = lnk.Parent.FindControl("hdfbranch") as HiddenField;
        HiddenField hdfregno = lnk.Parent.FindControl("hdfregno") as HiddenField;
        HiddenField hdfdemand = lnk.Parent.FindControl("hdfdemand") as HiddenField;
        HiddenField hdfsemester = lnk.Parent.FindControl("hdfsemester") as HiddenField;
        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        lblDegreeDis.Text = hdfdegree.Value;
        lblBranchDis.Text = hdfbranch.Value;
        lblRegnoDis.Text = hdfregno.Value;
        lblPayDemandDis.Text = hdfdemand.Value;
        lblsemesterDis.Text = hdfsemester.Value;
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        ViewState["idno"] = Session["stuinfoidno"].ToString();
        lblStudNameDis.Text = lnk.Text.Trim();
        lblidnoDis.Text = Session["stuinfoidno"].ToString();
        divStudInfoDis.Visible = true;
        lvStudentDis.Visible = false;
        lvStudentDis.DataSource = null;
        lblNoRecordsDis.Visible = false;
        pnlStudinstalment.Visible = false;
        btnsubmit.Visible = false;
        btnRemove.Visible = false;
        btncancel.Visible = false;
        ddlSearch.SelectedIndex = 0;
        }
    protected void ddlreceipt_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlreceipt.SelectedIndex > 0)
            {
            int id_no = Convert.ToInt32(Session["stuinfoidno"]);
            objCommon.FillDropDownList(ddlsemesterShow, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO)", "DISTINCT D.SEMESTERNO", "S.SEMESTERNAME", "D.RECIEPT_CODE ='" + ddlreceipt.SelectedValue + "' AND D.IDNO = '" + id_no + "'", "D.SEMESTERNO");
            ddlreceipt.Focus();
            }
        else
            {
            ddlsemesterShow.Focus();
            ddlreceipt.SelectedIndex = 0;
            }
        }
    protected void CancelDis_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        divStudInfoDis.Visible = false;
        ddlsearchDisc.SelectedIndex = 0;
        LvDiscountSingle.Visible = false;
        LvDiscountSingle.DataSource = null;
        LvDiscountSingle.DataBind();
        }
    protected void btnShowDisSingle_Click(object sender, EventArgs e)
            {
        try
            {
            if (ddlsemesterShow.SelectedValue == "0")
                {
                objCommon.DisplayMessage(upBulkInstalment, "Please Select Semester", this.Page);
                }
            else if (ddlsemesterShow.SelectedValue == "0")
                {
                objCommon.DisplayMessage(upBulkInstalment, "Please Select Receipt Type", this.Page);
                }
            divConcessionOptionSingle.Visible = true;
            rdoSelect.SelectedValue = null;
            DataSet dsS = objSC.ShowSingleDiscountStudents(Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt32(ddlsemesterShow.SelectedValue), Convert.ToString(ddlreceipt.SelectedValue));
            if (dsS.Tables.Count > 0 && dsS.Tables[0].Rows.Count > 0)
                {
                btnSubmitSingleDis.Visible = true;
                divConcessionOptionSingle.Visible = true;
                rdoSelect.SelectedValue = null;
                LvDiscountSingle.DataSource = dsS;
                LvDiscountSingle.DataBind();
                Panel5.Visible = true;
                LvDiscountSingle.Visible = true;
                }
            else
                {
                btnSubmitSingleDis.Visible = false;
                divConcessionOptionSingle.Visible = false;
                objCommon.DisplayMessage(upBulkInstalment, "No Students for selected criteria", this.Page);
                LvDiscountSingle.DataSource = null;
                LvDiscountSingle.DataBind();
                }
            string SP_Name1 = "PKG_ACD_BIND_DROP_DOWN_LISTS";
            string SP_Parameters1 = "@P_OUTPUT";
            string Call_Values1 = "" + 1;
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);
            foreach (ListViewDataItem dataitem in LvDiscountSingle.Items)
                {
                CheckBox chktransfer = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList ddlDiscount = dataitem.FindControl("ddlDiscount") as DropDownList;
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                    ddlDiscount.Items.Clear();
                    ddlDiscount.Items.Add("Please Select");
                    ddlDiscount.SelectedItem.Value = "0";
                    ddlDiscount.DataSource = ds.Tables[0];
                    ddlDiscount.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlDiscount.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlDiscount.DataBind();
                    ddlDiscount.SelectedIndex = 0;

                    }
                DropDownList ddlConcession = dataitem.FindControl("ddlConcession") as DropDownList;
                DropDownList dllSelectAllType = this.LvDiscountSingle.Controls[0].FindControl("dllSelectAllType") as DropDownList;
                if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
                    {
                    ddlConcession.Items.Clear();
                    ddlConcession.Items.Add("Please Select");
                    ddlConcession.SelectedItem.Value = "0";
                    ddlConcession.DataSource = ds.Tables[1];
                    ddlConcession.DataValueField = ds.Tables[1].Columns[0].ToString();
                    ddlConcession.DataTextField = ds.Tables[1].Columns[1].ToString();
                    ddlConcession.DataBind();
                    ddlConcession.SelectedIndex = 0;
                    //dllSelectAllType.Items.Clear();
                    dllSelectAllType.Items.Add("Please Select");
                    dllSelectAllType.SelectedItem.Value = "0";
                    dllSelectAllType.DataSource = ds.Tables[1];
                    dllSelectAllType.DataValueField = ds.Tables[1].Columns[0].ToString();
                    dllSelectAllType.DataTextField = ds.Tables[1].Columns[1].ToString();
                    dllSelectAllType.DataBind();
                    dllSelectAllType.SelectedIndex = 0;

                    }
                Label lblDiscount = dataitem.FindControl("lblDiscount") as Label;
                Label lblConcessionno = dataitem.FindControl("lblConcessionno") as Label;
                TextBox txtDiscountFee = dataitem.FindControl("txtDiscountFee") as TextBox;
                TextBox txtNetPayable = dataitem.FindControl("txtNetPayable") as TextBox;
                Label lbldcridno = dataitem.FindControl("lbldcridno") as Label;
                string DiscountFee = (txtDiscountFee.Text);
                string NetPayable = (txtNetPayable.Text);
                string dcridno = (lbldcridno.Text);
                ddlDiscount.SelectedItem.Text = lblDiscount.Text;
                ddlConcession.SelectedValue = lblConcessionno.Text;
                if (dcridno.ToString() != "0")
                    {
                    chktransfer.Enabled = false;
                    ddlDiscount.Enabled = false;
                    ddlConcession.Enabled = false;
                    lbldcridno.Text = "Utilize";
                    lbldcridno.CssClass = "lbl-green";
                    }
                else
                    {
                    lbldcridno.Text = "Not Utilize";
                    lbldcridno.CssClass = "lbl-red";
                    }
                }
            }
        catch (Exception ex)
            {
            }
        }
    protected void btnSubmitSingleDis_Click(object sender, EventArgs e)
        {
        try
            {
            if (rdoSelect.SelectedValue == string.Empty)
                {
                objCommon.DisplayMessage(this.Page, "Please Select Payment Type (Amount Wise or Percentage Wise)", this.Page);
                return;
                }
            string StudentReg = string.Empty;

            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in LvDiscountSingle.Items)
                {
                CheckBox chk = dataitem.FindControl("chktransfer") as CheckBox;
                DropDownList ddlDiscount = dataitem.FindControl("ddlDiscount") as DropDownList;
                DropDownList ddlConcession = dataitem.FindControl("ddlConcession") as DropDownList;
                Label lblreg = dataitem.FindControl("lblreg") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                Label lblIdno = dataitem.FindControl("lblIdno") as Label;
                TextBox lbltotal = dataitem.FindControl("lbltotal") as TextBox;
                TextBox txtDiscountFee = dataitem.FindControl("txtDiscountFee") as TextBox;
                TextBox txtNetPayable = dataitem.FindControl("txtNetPayable") as TextBox;
                Label lblDiscount = dataitem.FindControl("lblDiscount") as Label;
                HiddenField hdfCollege = dataitem.FindControl("hdfCollege") as HiddenField;

                if (chk.Checked == true && chk.Enabled == true)
                    {
                    StudentReg += lblreg.Text + '$';


                    if (rdoSelect.SelectedValue != "1")
                        {
                        if (ddlConcession.SelectedValue == "0")
                            {

                            objCommon.DisplayMessage(this, "Please Select Discount Type.", this.Page);
                            rdoSelect.ClearSelection();
                            rdConcessionOption.SelectedValue = null;
                            txtNetPayable.Text = string.Empty;
                            txtDiscountFee.Text = string.Empty;
                            return;
                            }
                        if (lblDiscount.Text == "")
                            {
                            if (ddlDiscount.SelectedValue == "0")
                                {
                                objCommon.DisplayMessage(this.upBulkInstalment, "Please Select Discount.", this.Page);
                                return;
                                }
                            }
                        }

                    if (txtDiscountFee.Text == "" && txtNetPayable.Text == "")
                        {
                        objCommon.DisplayMessage(this.upBulkInstalment, "Please Enter Discount Fee And Net Payable", this.Page);
                        }
                    else
                        {
                        string Discount = Convert.ToString(ddlDiscount.SelectedItem.Text);
                        string Regno = lblreg.Text;
                        string name = lblname.Text;
                        decimal Totals = Convert.ToDecimal(lbltotal.Text);
                        int Total = Convert.ToInt32(Totals);
                        decimal DiscountFeee = Convert.ToDecimal(txtDiscountFee.Text);
                        decimal NetPayable = Convert.ToDecimal(txtNetPayable.Text);
                        int Concession_no = Convert.ToInt32(ddlConcession.SelectedValue);
                        int UA_NO = Convert.ToInt32(Session["userno"].ToString());
                        int Idno = Convert.ToInt32(lblIdno.Text);
                        cs = (CustomStatus)objSC.InsertFeesDiscount(Regno, Convert.ToInt32(hdfCollege.Value), name, Totals, DiscountFeee, Discount, NetPayable, Convert.ToInt32(ddlsemesterShow.SelectedValue), UA_NO, Concession_no, Idno, Convert.ToInt32(rdoSelect.SelectedValue), Convert.ToString(ddlreceipt.SelectedValue), Convert.ToString(ViewState["PAYTYPENAME"]), Convert.ToInt32(Session["OrgId"]),Convert.ToInt32(lblreg.ToolTip));
                        }
                    }
                }

            if (StudentReg.ToString() == string.Empty)
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Please Select At List One.", this.Page);
                rdoSelect.ClearSelection();// To Clear the RBL Selection
                //rdoSelect.Items.Remove("ListItem"); 
                //  rdoSelect.SelectedIndex = -1;

                rdConcessionOption.SelectedValue = null;
                //txtNetPayable.Text = string.Empty;
                //txtDiscountFee.Text = string.Empty;
                return;
                }

            if (cs.Equals(CustomStatus.RecordSaved))
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Record Saved Successfully.", this.Page);
                btnSubmitSingleDis.Visible = false;
                divConcessionOptionSingle.Visible = false;
                LvDiscountSingle.DataSource = null;
                LvDiscountSingle.DataBind();
                ddlreceipt.SelectedIndex = 0;
                ddlsemesterShow.SelectedIndex = 0;
                divStudInfoDis.Visible = false;
                ddlsearchDisc.SelectedIndex = 0;
                ddlpaymentsingle.SelectedIndex = 0;
                return;
                }
            else
                {
                objCommon.DisplayMessage(this.upBulkInstalment, "Failed To Save Record ", this.Page);
                }
            }
        catch
            {
            }
        }
    }

