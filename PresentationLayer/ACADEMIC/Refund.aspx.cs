//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REFUND
// CREATION DATE : 08-AUG-2013
// CREATED BY    : 
// MODIFIED DATE : ASHISH DHAKATE
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_Refund : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentRegist objSR = new StudentRegist();
    string recno = string.Empty;
    int idno;
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

                    // Fill Dropdown lists                
                    this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
                    this.objCommon.FillDropDownList(ddlbankonline, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    //this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    //this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    //this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "");

                    // Set Receipt No.
                    txtVoucherNo.Text = this.GetNewVoucherNo();

                    // Set date fields to today's date
                    txtVoucherDate.Text = DateTime.Today.ToShortDateString();
                    txtChqDDDate.Text = DateTime.Today.ToShortDateString();

                    // Clear cheque/dd details table from session
                    Session["tblChqDD_Info"] = null;
                    Session["tblOnline_Info"] = null;
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
                    ddlSearch.SelectedIndex = 1;
                    ddlSearch_SelectedIndexChanged(sender, e);
                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        int studentId = int.Parse(Request.QueryString["id"].ToString());
                        this.DisplayInformation(studentId);
                    }
                }

            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by btnSearch then call search method.
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                        this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
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
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                // Check user's authrity for Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
                }
            }
            else
            {
                // Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Search Student



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

            //Response.Redirect(url + "&id=" + lnk.CommandArgument);
            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;
            objSR.IDNO = idno;
            ViewState["idno"] = Session["stuinfoidno"].ToString();
            int Organizationid = Convert.ToInt32(Session["OrgId"]);

            //objSR.REGNO = txtSearch.Text;

            //FeeCollectionController feeController = new FeeCollectionController();
            //int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
          //  int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + lblenrollno.Text.Trim() + "'"));
            if (idno > 0)
            {
                this.DisplayInformation(idno);
                lvStudent.Visible = false;
                //divStudInfo.Visible = true;
                //updEdit.Visible = false;
            }
            else
            {
                ShowMessage("No student found having enrollment no.: " + txtSearch.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 2)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                        case "Name":
                            objSearch.StudentName = paramValue;
                            break;
                        case "EnrollNo":
                            objSearch.EnrollmentNo = paramValue;
                            break;
                        case "Srno":
                            objSearch.Srno = paramValue;
                            break;
                        case "DegreeNo":
                            objSearch.DegreeNo = int.Parse(paramValue);
                            break;
                        case "BranchNo":
                            objSearch.BranchNo = int.Parse(paramValue);
                            break;
                        case "YearNo":
                            objSearch.YearNo = int.Parse(paramValue);
                            break;
                        case "SemNo":
                            objSearch.SemesterNo = int.Parse(paramValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.GetStudents(objSearch);
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region Displaying Student's and His Existing Collection Info`rmation

    private void DisplayInformation(int studentId)
    {
        try
        {
            FeeCollectionController feeController = new FeeCollectionController();

            /// Display student's personal and academic data in 
            /// student information section
            /// 
            int organizationid = Convert.ToInt32(Session["OrgId"]);//added by dileep on 31.12.2021
            DataSet ds = feeController.GetStudentInfoByIdRefund(studentId, organizationid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                // show student information
                this.PopulateStudentInfoSection(dr);
            }
            else
            {
            objCommon.DisplayMessage(updRefund, "Unable to retrieve student's record.", this.Page);
                // "Fee Not Found Cannot Refund."
                return;
            }

            /// Show all receipt for the student
            this.DisplayAllCollections(studentId, 0);

            /// Only for GP Mumbai:
            /// Mostly cash payment is done in the college hence setting
            /// pay type to cash(C)
            txtPayType.Text = "C";
            txtPayType.Focus();

            /// Hide search student div and student list so that
            /// once a student's data is populated, user should not select
            /// other student to show information, unless and until user 
            /// clicks the cancel button.

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void clear()
    {

        txtTotalRefundAmt.Text = "";
        txtRemark.Text = "";
        txtVoucherNo.Text = "";
        txtPayType.Text = "";
        txtReceiptAmount.Text = "";
        txtVoucherDate.Text = "";
        // lvFeeItems.DataSource = null;
        //lvFeeItems.Items.Clear();
        foreach (ListViewDataItem item in lvFeeItems.Items)
        {
            TextBox txtfind = item.FindControl("txtFeeItemAmount") as TextBox;
            txtfind.Text = string.Empty;
        }


    }


    //aayushi
    //private ListViewItem foundItem;

    //private void button1_Click(object sender, EventArgs e)
    //{
    //    foundItem = lvFeeItems.FindItemWithText(txtFeeItemAmount.Text);

    //    if (foundItem != null)
    //    {
    //        lvFeeItems.Items.Remove(foundItem);
    //        lvFeeItems.Items.Insert(0, foundItem);
    //    }
    //    else
    //    {
    //        // do something here if no match ?
    //    }
    //}
    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            // Bind data with labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["ENROLLNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();

            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the refund record.
            ViewState["StudentId"] = dr["IDNO"].ToString();
            ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["YearNo"] = dr["YEAR"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();
            ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private double GetReceiptAmount(DataTable dt)
    {
        double totalFeeAmt = 0.00;
        try
        {
            foreach (DataRow dr in dt.Rows)
                totalFeeAmt += ((dr["AMOUNT"].ToString().Trim() != string.Empty) ? Convert.ToDouble(dr["AMOUNT"].ToString()) : 0.00);
        }
        catch (Exception ex)
        {
            throw;
        }
        return totalFeeAmt;
    }

    private void DisplayAllCollections(int studentId, int dcrNo)
    {
        try
        {
            RefundController refundController = new RefundController();
            DataSet ds = refundController.GetAllCollections(studentId, dcrNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvAllCollections.DataSource = ds;
                lvAllCollections.DataBind();
                divAllCollections.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAllCollections);//Set label - 
                lvAllCollections.Items[0].FindControl("btnRefund").Focus();
            }
            else
                ShowMessage("No fee collection found from this student.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnRefund_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnRefundAmount = sender as ImageButton;
            int dcrNo = (btnRefundAmount.CommandArgument != string.Empty ? int.Parse(btnRefundAmount.CommandArgument) : 0);
            int studentId = (btnRefundAmount.CommandName != string.Empty ? int.Parse(btnRefundAmount.CommandName) : 0);

            RefundController refundController = new RefundController();
            DataSet ds = refundController.GetAmountsToRefund(dcrNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvFeeItems.DataSource = ds;
                lvFeeItems.DataBind();
                divFeeItems.Visible = true;
                if (Session["OrgId"].ToString() == "1")
                {
                    divCancellationCharges.Visible = true;
                }
                else
                {
                    divCancellationCharges.Visible = false;
                }
                /// Store demand no in view state 
                /// to be used while saving the record.
                ViewState["DcrNo"] = dcrNo;
                ViewState["ReceiptNo"] = btnRefundAmount.ToolTip;

                /// Show total receipt amount
                txtReceiptAmount.Text = this.GetReceiptAmount(ds.Tables[0]).ToString();

                divAllCollections.Visible = false;
                this.btnSubmit.Enabled = true;
                // Show voucher info div
                divVoucherInfo.Style["display"] = "block";
                txtPayType.Focus();
                divVoucherInfo.Visible = true;
                divFeeItems.Visible = true;
                txtVoucherNo.Text = this.GetNewVoucherNo();
                txtVoucherDate.Text = DateTime.Today.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Displaying Cheque/Demand Draft Details

    protected void btnSaveChqDD_Info_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;

            if (Session["tblChqDD_Info"] != null && ((DataTable)Session["tblChqDD_Info"]) != null)
                {
                dt = ((DataTable)Session["tblChqDD_Info"]);
                for (int i = 0; i < dt.Rows.Count; i++)
                    {
                    string ddno = dt.Rows[i]["ChqDd_No"].ToString();
                    string bnknm = dt.Rows[i]["ChqDd_BankName"].ToString();

                    if (ddno.Equals(txtChqDDNo.Text) && bnknm.Equals(ddlBank.SelectedItem.Text))
                        {
                        // flag = true;
                        // return;
                        objCommon.DisplayMessage(this.Page, "Same DD number with Same bank is already exists", this.Page);
                        return;
                        }
                    }

                }
            if (Session["tblChqDD_Info"] != null && ((DataTable)Session["tblChqDD_Info"]) != null)
            {
                dt = ((DataTable)Session["tblChqDD_Info"]);
                DataRow dr = dt.NewRow();
                dr["ChqDd_No"] = txtChqDDNo.Text.Trim();
                dr["ChqDd_Date"] = txtChqDDDate.Text.Trim();
                dr["ChqDd_City"] = txtChqDDCity.Text.Trim();
                dr["ChqDd_BankNo"] = ddlBank.SelectedValue;
                dr["ChqDd_BankName"] = ddlBank.SelectedItem.Text;
                dr["ChqDd_Amount"] = txtChqDDAmount.Text.Trim();
                dt.Rows.Add(dr);
                Session["tblChqDD_Info"] = dt;


                
                this.BindListView_ChequeOrDemandDraftDetails(dt);
            }
            else
            {
                dt = this.GetDemandDraftDataTable();
                DataRow dr = dt.NewRow();
                dr["ChqDd_No"] = txtChqDDNo.Text.Trim();
                dr["ChqDd_Date"] = txtChqDDDate.Text.Trim();
                dr["ChqDd_City"] = txtChqDDCity.Text.Trim();
                dr["ChqDd_BankNo"] = ddlBank.SelectedValue;
                dr["ChqDd_BankName"] = ddlBank.SelectedItem.Text;
                dr["ChqDd_Amount"] = txtChqDDAmount.Text.Trim();
                dt.Rows.Add(dr);
                Session.Add("tblChqDD_Info", dt);               
                this.BindListView_ChequeOrDemandDraftDetails(dt);
            }
            this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> UpdateCash_DD_Amount();  </script> ";
            this.ClearControls_DemandDraftDetails();
            btnSaveChqDD_Info.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void btnEditChqDDInfo_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["tblChqDD_Info"] != null && ((DataTable)Session["tblChqDD_Info"]) != null)
            {
                dt = ((DataTable)Session["tblChqDD_Info"]);
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                txtChqDDNo.Text = dr["ChqDd_No"].ToString();
                txtChqDDDate.Text = dr["ChqDd_Date"].ToString();
                txtChqDDCity.Text = dr["ChqDd_City"].ToString();
                ddlBank.SelectedValue = dr["ChqDd_BankNo"].ToString();
                txtChqDDAmount.Text = dr["ChqDd_Amount"].ToString();
                dt.Rows.Remove(dr);
                Session["tblChqDD_Info"] = dt;
                this.BindListView_ChequeOrDemandDraftDetails(dt);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDeleteChqDDInfo_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["tblChqDD_Info"] != null && ((DataTable)Session["tblChqDD_Info"]) != null)
            {
                DataTable dt = ((DataTable)Session["tblChqDD_Info"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["tblChqDD_Info"] = dt;
                this.BindListView_ChequeOrDemandDraftDetails(dt);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListView_ChequeOrDemandDraftDetails(DataTable dt)
    {
        try
        {
            lvChqDdDetails.DataSource = dt;
            lvChqDdDetails.DataBind();
            divChqDDInfo.Style["display"] = "block";
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void BindListView_OnlineOtherFeesDetails(DataTable dt)
        {
        try
            {
            LvOnlinePayDetils.DataSource = dt;
            LvOnlinePayDetils.DataBind();
            divOnline.Style["display"] = "block";
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private DataTable GetDemandDraftDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ChqDd_No", typeof(string)));
        dt.Columns.Add(new DataColumn("ChqDd_Date", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("ChqDd_City", typeof(string)));
        dt.Columns.Add(new DataColumn("ChqDd_BankNo", typeof(int)));
        dt.Columns.Add(new DataColumn("ChqDd_BankName", typeof(string)));
        dt.Columns.Add(new DataColumn("ChqDd_Amount", typeof(double)));

        return dt;
    }
    private DataTable GetOnlineFeesDataTable()
        {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Mode", typeof(string)));
        dt.Columns.Add(new DataColumn("OnlineMode", typeof(int)));
        dt.Columns.Add(new DataColumn("TrsancationID", typeof(string)));
        dt.Columns.Add(new DataColumn("Online_Date", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("Online_City", typeof(string)));
        dt.Columns.Add(new DataColumn("Online_BankNo", typeof(int)));
        dt.Columns.Add(new DataColumn("Online_BankName", typeof(string)));
        dt.Columns.Add(new DataColumn("Online_Amount", typeof(double)));

        return dt;
        }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ChqDd_No"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return dataRow;
    }

    private DataRow GetEditableOnlineDataRow(DataTable dt, string value)
        {
        DataRow dataRow = null;
        try
            {
            foreach (DataRow dr in dt.Rows)
                {
                if (dr["TrsancationID"].ToString() == value)
                    {
                    dataRow = dr;
                    break;
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return dataRow;
        }

    private void ClearControls_DemandDraftDetails()
    {
        txtChqDDNo.Text = string.Empty;
        txtChqDDAmount.Text = string.Empty;
        txtChqDDCity.Text = string.Empty;
        txtChqDDDate.Text = DateTime.Today.ToShortDateString();
        ddlBank.SelectedIndex = 0;
    }

    private void ClearControls_OnlineFeeDetails()
        {
        rdlOnlineMode.SelectedIndex = 0;
        txttransactionno.Text = string.Empty;
        txtOnlineAmount.Text = string.Empty;
        ddlbankonline.SelectedIndex = 0;
        txtOnlineDate.Text = DateTime.Today.ToShortDateString();
        txtcityOnline.Text = string.Empty;
        }

    #endregion

    #region Saving Refund Transaction

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            /// if payment type is (D)demand draft
            /// then validate DD data and other submission data
            /// 

        if (txtPayType.Text.ToUpper() == "D")
            {
            if (Session["tblChqDD_Info"] == null)
                {
                objCommon.DisplayMessage(this.Page, "Please Enter Cheque/Demand Draft Details.", this.Page);
                return;
                }
            }
        if (txtPayType.Text.ToUpper() == "O")
            {
            if (Session["tblOnline_Info"] == null)
                {
                objCommon.DisplayMessage(this.Page, "Please Enter Online Payment Details.", this.Page);
                return;
                }
            }
        if (txtPayType.Text.ToUpper() == "D" || txtPayType.Text.ToUpper() == "Q" || txtPayType.Text.ToUpper() == "O")
            {
                string msg = this.ValidateChqDd_Details();
                string msg1 =this.ValidateOnlineFees_Details();

                if (msg != string.Empty && txtPayType.Text.ToUpper() == "D" )
                {
                    this.ShowMessage(msg);
                    return;
                }
                else if (msg1 != string.Empty && txtPayType.Text.ToUpper() == "O")
                    {
                    this.ShowMessage(msg1);
                    return;
                    }
                else
                    {
                    Refund refund = this.Bind_RefundData();
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_REFUND", "count(1)", "REC_NO='" + recno + "' and idno =" + idno));
                    string Voucherno = objCommon.LookUp("ACD_REFUND", "VOUCHER_NO", "REC_NO='" + recno + "' and idno =" + idno);
                    string refundamount = objCommon.LookUp("ACD_REFUND", "Total_AMT", "REC_NO='" + recno + "' and idno =" + idno);
                    string paidamount = objCommon.LookUp("ACD_DCR", "Total_AMT", "ISNULL(CAN,0)=0 AND REC_NO='" + recno + "' and idno =" + idno);
                    if (count == 1 && refundamount==paidamount)
                        {
                        objCommon.DisplayMessage(this.Page, "Amount is Already Refunded for Receipt No. " + Voucherno + "", this.Page);

                        }
                    else
                        {
                        this.SaveRefund();
                        
                        }
                    }
            } // if pay type is C (Cash) then validate submission data.
            else if (txtPayType.Text.ToUpper() == "C")
            {
                string msg = string.Empty;
                this.ValidateSubmissionData(ref msg);

                if (msg != string.Empty)
                {
                    this.ShowMessage(msg);
                    objCommon.DisplayMessage(this.Page, "Total refund amount to be paid can not be zero or less than zero.", this.Page);
                    return;
                }
                else
                {
                    Refund refund = this.Bind_RefundData();
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_REFUND", "count(1)", "REC_NO='" + recno + "' and idno =" + idno));
                    string  Voucherno = objCommon.LookUp("ACD_REFUND", "VOUCHER_NO", "REC_NO='" + recno + "' and idno =" + idno);
                    string  refundamount = objCommon.LookUp("ACD_REFUND", "Total_AMT", "REC_NO='" + recno + "' and idno =" + idno);
                    string  paidamount = objCommon.LookUp("ACD_DCR", "Total_AMT", "ISNULL(CAN,0)=0 AND REC_NO='" + recno + "' and idno =" + idno);
                    if (count == 1 && refundamount == paidamount)
                        {
                        objCommon.DisplayMessage(this.Page, "Amount is Already Refunded for Receipt No. " + Voucherno + "", this.Page);

                        }
                    else
                    {
                        this.SaveRefund();
                    }
                }
            }
            else
            {
                this.ShowMessage("Please select pay type.");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Data Validation
    private string ValidateChqDd_Details()
    {
        string msg = string.Empty;

        if (Session["tblChqDD_Info"] == null || ((DataTable)Session["tblChqDD_Info"]).Rows.Count < 1)
        {
            msg = "You have entered pay type as ";
            if (txtPayType.Text.ToUpper() == "D")
                msg += "demand draft but no demand details has been entered. \\nPlease enter demand draft details.";
            else if (txtPayType.Text.ToUpper() == "Q")
                msg += "cheque but no cheque has been entered. \\nPlease enter cheque details.";
        }
        this.ValidateSubmissionData(ref msg);
        return msg;
    }

    private string ValidateOnlineFees_Details()
        {
        string msg1 = string.Empty;

        if (Session["tblOnline_Info"] == null || ((DataTable)Session["tblOnline_Info"]).Rows.Count < 1)
            {
            msg1 = "You have entered pay type as ";
            if (txtPayType.Text.ToUpper() == "O")
                msg1 += "Online fees Details but no Online details has been entered. \\nPlease enter demand draft details.";
            else if (txtPayType.Text.ToUpper() == "Q")
                msg1 += "cheque but no cheque has been entered. \\nPlease enter cheque details.";
            }
        //this.ValidateSubmissionData(ref msg);
        return msg1;
        }

    private string ValidateSubmissionData(ref string msg)
    {
        if (txtVoucherNo.Text.Trim() == string.Empty)
        {
            if (msg.Length > 0) msg += "\\n";
            msg += "Voucher No. can not be blank.";
            objCommon.DisplayMessage(this.Page, "Voucher No. can not be blank.", this.Page);
        }
        if (txtVoucherDate.Text.Trim() == string.Empty)
        {
            if (msg.Length > 0) msg += "\\n";
            msg += "Please enter voucher date.";
            objCommon.DisplayMessage(this.Page, "Please enter voucher date.", this.Page);
        }
        if (txtTotalRefundAmt.Text.Trim() == string.Empty)
        {
            if (msg.Length > 0) msg += "\\n";
            msg += "Please enter refund amount.";
            objCommon.DisplayMessage(this.Page, "Please enter refund amount.", this.Page);
        }
        if (txtTotalRefundAmt.Text.Trim() != string.Empty && Convert.ToInt32(txtTotalRefundAmt.Text.Trim()) < 1)
        {
            if (msg.Length > 0) msg += "\\n";
            msg += "Total refund amount to be paid can not be zero or less than zero.";
            objCommon.DisplayMessage(this.Page, "Total refund amount to be paid can not be zero or less than zero.", this.Page);
        }
        if (txtPayType.Text.Trim() == string.Empty)
        {
            if (msg.Length > 0) msg += "\\n";
            msg += "Please enter pay type.";
            objCommon.DisplayMessage(this.Page, "Please enter pay type.", this.Page);
        }
        return msg;
    }
    #endregion

    private void SaveRefund()
    {
        try
        {
            /// Bind all refund transaction related data
            Refund refund = this.Bind_RefundData();
            RefundController refundController = new RefundController();
            // Save refund transaction data.
             //if successfully saved then only continue with voucher printing 
            if (refundController.SaveRefund_Transaction(ref refund, Convert.ToInt32(ViewState["PaymentMode"])))
            {
                //ViewState["RefundNo"] = refund.RefundNo.ToString();
                this.ShowReport(refund.RefundNo);
                this.btnReport.Enabled = true;
                // Set DCR_NO to be used to show report later.
                ViewState["RefundNo"] = refund.RefundNo.ToString();
                btnSubmit.Enabled = false;
            }
            objCommon.DisplayMessage(this.updEdit, "Amount Refund successfully. Your Voucher Number is " + txtVoucherNo.Text + "  Now you can print Refund Voucher.Click on Print Refund Voucher.", this.Page);

            clear();

            lvChqDdDetails.DataSource = null;
            lvChqDdDetails.Items.Clear();
            LvOnlinePayDetils.DataSource = null;
            LvOnlinePayDetils.Items.Clear();
            //ShowMessage("Transaction saved successfully.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.SaveFeeCollection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private Refund Bind_RefundData()
    {
        /// Bind transaction related data from various controls.
        Refund refund = new Refund();
        try
        {
            refund.DCR_NO = (GetViewStateItem("DcrNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("DcrNo")) : 0;
            refund.ReceiptNo = GetViewStateItem("ReceiptNo");

            recno = GetViewStateItem("ReceiptNo");

            // refund.RefundNo = (GetViewStateItem("REFUND_NO") != string.Empty) ? Convert.ToInt32(GetViewStateItem("REFUND_NO")) : 0;
            refund.IDNO = (GetViewStateItem("StudentId") != string.Empty) ? Convert.ToInt32(GetViewStateItem("StudentId")) : 0;
            idno = Convert.ToInt32(GetViewStateItem("StudentId"));
            refund.VoucherNo = txtVoucherNo.Text;
            refund.VoucherDate = (txtVoucherDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtVoucherDate.Text.Trim()) : DateTime.MinValue);
            double amt = 0.00;
            refund.FeeItemsAmount = this.GetFeeItemsAndTotalRefundAmt(ref amt);
            //refund.TotalAmount = amt;
            DemandDrafts[] dds = null;

            if (txtPayType.Text == "D")
                {
                refund.ChequeDD_Amount = this.GetTotalChqDDAmountAndSetCompleteDetails(ref dds);

                refund.PaidChequesDemandDrafts = dds;
                }
            if (txtPayType.Text == "O")
                {

                DemandDrafts[] onlinefee = null;
                refund.ChequeDD_Amount = this.GetTotalOnlineFeeAndSetCompleteDetails(ref onlinefee );

                int Paymode = Convert.ToInt32(ViewState["PaymentMode"]);
                refund.PaidChequesDemandDrafts = onlinefee;
                }

            refund.PayType = txtPayType.Text.Trim().ToUpper();
            refund.IsCanceled = false;
            refund.PrintDate = DateTime.Today;

            refund.Remark = "This receipt has been Refunded by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
            refund.Remark += txtRemark.Text.Trim();

            refund.excessamount = Convert.ToString(ViewState["excessamount"]).ToString();
            if (refund.excessamount == "")
            {
                refund.excessamount = "0";
            }
            refund.TotalAmount = amt + Convert.ToDouble(refund.excessamount);
            refund.CashAmount = this.GetCashAmount(refund.TotalAmount, refund.ChequeDD_Amount);
            refund.UserNo = Convert.ToInt32(Session["userno"].ToString());
            refund.CounterNo = (GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0;
            refund.CollegeCode = Session["colcode"].ToString();
            refund.Organizationid = Convert.ToInt32(Session["OrgId"]);
            refund.Cancellation_Charges = txtAdmCancelCharge.Text != string.Empty ? Convert.ToInt32(txtAdmCancelCharge.Text) : 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.Bind_FeeCollectionData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return refund;
    }

   

    private double GetCashAmount(double totalRefundAmt, double totalChqDDAmt)
    {
        double cashAmount = 0.00;
        try
        {
            /// if payment type is cash then total paid amount 
            /// is equal to cash amount
            if (txtPayType.Text.ToUpper() == "C")
            {
                cashAmount = totalRefundAmt;
            }
            else
            {
                /// In case of payment by cheque or demand Draft,
                /// cash amount is equal to total paid 
                /// amount minus total cheque/dd amount.
                cashAmount = totalRefundAmt - totalChqDDAmt;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return cashAmount;
    }

    private double GetTotalChqDDAmountAndSetCompleteDetails(ref DemandDrafts[] paidChqDemandDrafts)
    {
        /// This method not only returns the total of all cheques or dd amounts
        /// but also sets/initializes the complete information
        /// of each cheque/demand draft into referenced paidChqDemandDrafts array.

        double totalDdAmt = 0.00;
        try
        {
            /// Collect cheque/demand draft details only if the pay type is 
            /// D (i.e. Demand draft) or Q (i.e. Cheque)
            if (txtPayType.Text.Trim() == "D" || txtPayType.Text.Trim() == "Q")
            {
                if (Session["tblChqDD_Info"] != null && ((DataTable)Session["tblChqDD_Info"]) != null)
                {
                    DataTable dt = ((DataTable)Session["tblChqDD_Info"]);

                    paidChqDemandDrafts = new DemandDrafts[dt.Rows.Count];
                    int index = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        DemandDrafts dd = new DemandDrafts();

                        dd.DemandDraftNo = dr["ChqDd_No"].ToString();
                        dd.DemandDraftCity = dr["ChqDd_City"].ToString();
                        dd.DemandDraftBank = dr["ChqDd_BankName"].ToString();
                        dd.BankNo = (dr["ChqDd_BankNo"].ToString() != string.Empty ? int.Parse(dr["ChqDd_BankNo"].ToString()) : 0);

                        string ddDate = dr["ChqDd_Date"].ToString();
                        if (ddDate != null && ddDate != string.Empty)
                            dd.DemandDraftDate = Convert.ToDateTime(ddDate);

                        string amount = dr["ChqDd_Amount"].ToString();
                        if (amount != null && amount != string.Empty)
                        {
                            dd.DemandDraftAmount = Convert.ToDouble(amount);
                            totalDdAmt += dd.DemandDraftAmount;
                        }
                        /// Set cheque/dd details in paid cheque/dd collection
                        paidChqDemandDrafts[index] = dd;
                        index++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return totalDdAmt;
    }



    private double GetTotalOnlineFeeAndSetCompleteDetails(ref DemandDrafts[] onlinefee)
        {
        /// This method not only returns the total of all cheques or dd amounts
        /// but also sets/initializes the complete information
        /// of each cheque/demand draft into referenced paidChqDemandDrafts array.

        double totalDdAmt = 0.00;
        try
            {
            /// Collect cheque/demand draft details only if the pay type is 
            /// D (i.e. Demand draft) or Q (i.e. Cheque)
            if (txtPayType.Text.Trim() == "O" || txtPayType.Text.Trim() == "Q")
                {
                if (Session["tblOnline_Info"] != null && ((DataTable)Session["tblOnline_Info"]) != null)
                    {
                    DataTable dt = ((DataTable)Session["tblOnline_Info"]);

                    onlinefee = new DemandDrafts[dt.Rows.Count];
                    int index = 0;
                    foreach (DataRow dr in dt.Rows)
                        {
                        DemandDrafts dd = new DemandDrafts();
                        
                        ViewState["PaymentMode"] =dr["OnlineMode"].ToString();
                        dd.DemandDraftNo = dr["TrsancationID"].ToString();
                        dd.DemandDraftCity = dr["Online_City"].ToString();
                        dd.DemandDraftBank = dr["Online_BankName"].ToString();
                        dd.BankNo = (dr["Online_BankNo"].ToString() != string.Empty ? int.Parse(dr["Online_BankNo"].ToString()) : 0);
                        string Amount = dr["Online_Amount"].ToString();
                        string ddDate = dr["Online_Date"].ToString();
                        if (ddDate != null && ddDate != string.Empty)
                            dd.DemandDraftDate = Convert.ToDateTime(ddDate);
                        if (Amount != null && Amount != string.Empty)
                            {
                            dd.DemandDraftAmount = Convert.ToDouble(Amount);
                            totalDdAmt += dd.DemandDraftAmount;
                            }
                        
                        /// Set cheque/dd details in paid cheque/dd collection
                        onlinefee[index] = dd;
                        index++;
                        }
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return totalDdAmt;
        }

    private FeeHeadAmounts GetFeeItemsAndTotalRefundAmt(ref double totalRefundAmt)
    {

        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                //if (item == lvFeeItems.Items.Count) { }
                int feeHeadNo = 0;
                double feeAmount = 0.00;

                string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
                int srno = Convert.ToInt32(feeHeadSrNo);
                if (srno == lvFeeItems.Items.Count)
                {
                    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                    string excessamount = feeAmt;
                    ViewState["excessamount"] = excessamount;


                }
                else
                {
                    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                        feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                    if (feeAmt != null && feeAmt != string.Empty)
                        feeAmount = Convert.ToDouble(feeAmt);

                    feeHeadAmts[feeHeadNo - 1] = feeAmount;
                    totalRefundAmt += feeAmount;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return feeHeadAmts;
    }
    #endregion

    #region Show Report

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (GetViewStateItem("RefundNo") != string.Empty)
            {
                this.ShowReport(Int32.Parse(GetViewStateItem("RefundNo")));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(int refundNo)
    {
        try
        {       
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Refund_Voucher";
            url += "&path=~,Reports,Academic,RefundVoucher.rpt";
            url += "&param=@P_REFUND_NO=" + refundNo.ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Refund_Voucher','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Private Methods

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private string GetNewVoucherNo()
    {
        string receiptNo = string.Empty;
        try
        {
            RefundController refundController = new RefundController();
            DataSet ds = refundController.GetVoucherNo(Int32.Parse(Session["userno"].ToString()));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["VOUCHER"] = Int32.Parse(dr["VOUCHER"].ToString()) + 1;
                receiptNo = dr["PRINTNAME"].ToString() + "/R/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["VOUCHER"].ToString();

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }
    #endregion

    #region Refresh or Reload Page
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
    #endregion

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

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        divAllCollections.Visible = false;
        //clear();
        lvFeeItems.DataSource = null;
        lvFeeItems.Items.Clear();
        lvChqDdDetails.DataSource = null;
        lvChqDdDetails.Items.Clear();
        LvOnlinePayDetils.DataSource = null;
        LvOnlinePayDetils.Items.Clear();
        divVoucherInfo.Visible = false;
        divFeeItems.Visible = false;
        

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
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
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
    protected void btnsaveonline_Click(object sender, EventArgs e)
        {
        try
            {
            DataTable dt;
            if (Session["tblOnline_Info"] != null && ((DataTable)Session["tblOnline_Info"]) != null)
                {
                dt = ((DataTable)Session["tblOnline_Info"]);
                DataRow dr = dt.NewRow();
                dr["Mode"] = rdlOnlineMode.SelectedItem.Text;
                dr["OnlineMode"] = rdlOnlineMode.SelectedValue;
                dr["TrsancationID"] = txttransactionno.Text;
                dr["Online_Date"] = txtOnlineDate.Text;
                dr["Online_City"] = txtcityOnline.Text;
                dr["Online_BankNo"] = ddlbankonline.SelectedValue;
                dr["Online_BankName"] = ddlbankonline.SelectedItem.Text;
                dr["Online_Amount"] = txtOnlineAmount.Text.Trim();

                dt.Rows.Add(dr);
                Session["tblOnline_Info"] = dt;
                this.BindListView_OnlineOtherFeesDetails(dt);
                }
            else
                {
                dt = this.GetOnlineFeesDataTable();
                DataRow dr = dt.NewRow();
                dr["Mode"] = rdlOnlineMode.SelectedItem.Text;
                dr["OnlineMode"] = rdlOnlineMode.SelectedValue;
                dr["TrsancationID"] = txttransactionno.Text;
                dr["Online_Date"] = txtOnlineDate.Text;
                dr["Online_City"] = txtcityOnline.Text;
                dr["Online_BankNo"] = ddlbankonline.SelectedValue;
                dr["Online_BankName"] = ddlbankonline.SelectedItem.Text;
                dr["Online_Amount"] = txtOnlineAmount.Text.Trim();
                dt.Rows.Add(dr);
                Session.Add("tblOnline_Info", dt);
                this.BindListView_OnlineOtherFeesDetails(dt);
                }
            this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> UpdateCash_DD_Amount();  </script> ";
            this.ClearControls_OnlineFeeDetails();
            btnsaveonline.Focus();
            }
        catch (Exception ex)
            {
            throw;
            }

        }

    protected void btnEditOnlineInfo_Click(object sender, ImageClickEventArgs e)
        {
        try
            {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["tblOnline_Info"] != null && ((DataTable)Session["tblOnline_Info"]) != null)
                {
                dt = ((DataTable)Session["tblOnline_Info"]);
                DataRow dr = this.GetEditableOnlineDataRow(dt, btnEdit.CommandArgument);
                rdlOnlineMode.SelectedValue = dr["OnlineMode"].ToString();
                txttransactionno.Text = dr["TrsancationID"].ToString();
                txtOnlineDate.Text = dr["Online_Date"].ToString();
                txtcityOnline.Text = dr["Online_City"].ToString();
                ddlbankonline.SelectedValue = dr["Online_BankNo"].ToString();
                txtOnlineAmount.Text = dr["Online_Amount"].ToString();
                dt.Rows.Remove(dr);
                Session["tblOnline_Info"] = dt;
                this.BindListView_OnlineOtherFeesDetails(dt);
                }
            }
        catch (Exception ex)
            {
            throw;
            }

        }
    protected void btnDeleteOnlineInfo_Click(object sender, ImageClickEventArgs e)
        {
        try
            {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["tblOnline_Info"] != null && ((DataTable)Session["tblOnline_Info"]) != null)
                {
                DataTable dt = ((DataTable)Session["tblOnline_Info"]);
                dt.Rows.Remove(this.GetEditableOnlineDataRow(dt, btnDelete.CommandArgument));
                Session["tblOnline_Info"] = dt;
                this.BindListView_OnlineOtherFeesDetails(dt);
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }
    protected void btnprintrefund_Click(object sender, ImageClickEventArgs e)
        {
        try{
        ImageButton btnRefundAmount = sender as ImageButton;
        int idno = Convert.ToInt32(btnRefundAmount.CommandName);
        int Dcrno = Convert.ToInt32(btnRefundAmount.CommandArgument);


        int Refundno = Convert.ToInt32(objCommon.LookUp("ACD_REFUND", "ISNULL(REFUND_NO,0) as REFUND_NO", "DCR_NO='" + Dcrno + "' and idno =" + idno));
        //int edit = int.Parse(btnEditSms.CommandArgument);

        if (Refundno != 0)
            {
            this.ShowReport(Refundno);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        }
    protected void lvAllCollections_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        try
            {



           ImageButton btnprint = e.Item.FindControl("btnprintrefund") as ImageButton;
           HiddenField hdndcrno = e.Item.FindControl("hdndcr") as HiddenField;
           HiddenField hdnidno = e.Item.FindControl("hdnidno") as HiddenField;
           int Idno = Convert.ToInt32(hdnidno.Value);
           int Dcrno = Convert.ToInt32(hdndcrno.Value);


           int Refundno = Convert.ToInt32(objCommon.LookUp("ACD_REFUND", "COUNT(ISNULL(REFUND_NO,0)) as REFUND_NO", "DCR_NO='" + Dcrno + "' and idno =" + Idno));
            if (Refundno != 0)
                {
                btnprint.Enabled = true;

                }
            else
                {
                btnprint.Enabled = false;
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Refund.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }
}