using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.IO;

public partial class ACADEMIC_FineAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    FeeCollectionController feeController = new FeeCollectionController();
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDown();
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FineAllotment.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FineAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FineAllotment.aspx");
        }
    }

    private void PopulateDropDown()
    {
        //objCommon.FillDropDownList(ddlReceipt, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE='OF'", "");
        objCommon.FillDropDownList(ddlReceipt, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
        ddlReceipt.SelectedValue = "OF";
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER SM INNER JOIN ACD_SESSION S ON (SM.SESSIONID=S.SESSIONID)", "DISTINCT SM.SESSIONID", "SM.SESSION_NAME", "SM.FLOCK=1 AND ISNULL(SM.IS_ACTIVE,0)=1", "SM.SESSIONID DESC");
        //AcademinDashboardController objADEController = new AcademinDashboardController();
        //DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
        //ddlsession.Items.Clear();
        //ddlsession.Items.Add("Please Select");
        //ddlsession.SelectedItem.Value = "0";

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlsession.DataSource = ds;
        //    //ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
        //    ddlsession.DataValueField = ds.Tables[0].Columns[0].ToString();
        //    ddlsession.DataTextField = ds.Tables[0].Columns[4].ToString();
        //    ddlsession.DataBind();
        //    ddlsession.SelectedIndex = 0;
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowDetails();
    }
    private void ShowDetails()
    {
        int idno = 0;
        int No_Dues_Status = 0;

        StudentController objSC = new StudentController();
        try
        {

            idno = studCont.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
            ViewState["idno"] = idno;
            //  int sessionno = Convert.ToInt32(ddlSessionSingleStud.SelectedValue);
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16(idno) + "");

            if (Enrollno.Equals(txtEnrollno.Text))
            {
                if (ddlReceipt.SelectedValue != "OF")
                {
                    int DcrCount = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "ISNULL(COUNT(DCR_NO),0) DCRNO", "IDNO =" + idno + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND RECIEPT_CODE='" + Convert.ToString(ddlReceipt.SelectedValue) + "' "));
                    if (DcrCount != 0)
                    {
                        objCommon.DisplayMessage(updFine, "Fees Has been paid for selected critera.", this.Page);
                        return;
                    }
                }

                // Panel1.Visible = true;
                if (idno > 0)
                {
                    //DataSet dsStudent = objSC.GetStudentDetails_No_Dues(idno, sessionno);
                    DataSet dsStudent = objSC.GetStudentDetails_For_FineAllotment(idno);

                    if (dsStudent != null && dsStudent.Tables.Count > 0)
                    {
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                            lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                            lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                            lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                            lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                            lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            lblDegreeno.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                            lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                            lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                            lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            lblSingCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                            lblSingCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                            lblSessionName.Text = dsStudent.Tables[0].Rows[0]["SESSION"].ToString();
                            ViewState["ptype"] = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();
                            PopulateFeeItemsSection();
                            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                            divCourses.Visible = true;
                            pnlSingleStud.Visible = true;

                        }
                        else
                        {
                            divCourses.Visible = false;
                            objCommon.DisplayMessage(updFine, "Registration Details not found for this session!", this.Page);
                        }
                    }
                    else
                    {
                        divCourses.Visible = false;
                        objCommon.DisplayMessage(updFine, "Registration Details not found for this session!", this.Page);
                    }
                }
            }
            else
            {
                divCourses.Visible = false;
                objCommon.DisplayMessage(updFine, "No Record Found!!!", this.Page);
                // Panel1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void PopulateFeeItemsSection()
    {
        try
        {
            int status = 0;
            int studId = Convert.ToInt32(ViewState["idno"].ToString());
            DataSet ds = null;
            int semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

            if (ddlReceipt.SelectedValue == "OF")
            {
                ds = feeController.GetFeeItems_DataFineAllotment(Convert.ToInt32(Session["currentsession"]), studId, semesterNo, ddlReceipt.SelectedValue, 0, 1, Convert.ToInt32(ViewState["ptype"].ToString()), ref status);
                if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string CollegeId = objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON(D.IDNO=S.IDNO)", "ISNULL(COLLEGE_ID,0)COLLEGE_ID", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND RECIEPT_CODE='OF' AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + " AND D.SEMESTERNO=" + Convert.ToInt32(semesterNo));




                    if (CollegeId == "")
                    {
                        CollegeId = "0";
                    }
                    btnSubmit.Enabled = true;
                    ViewState["COLLEGE_ID"] = CollegeId;

                    /// Bind fee items list view with the data source found.
                    lvFeeItems.DataSource = ds;
                    lvFeeItems.DataBind();
                    string RecieptCode = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                    if (RecieptCode == "TF" || RecieptCode == "EF" || RecieptCode == "HF" || RecieptCode == "BCA" || RecieptCode == "MBA" || RecieptCode == "PG" || RecieptCode == "EVF" ||
                        RecieptCode == "PGF" || RecieptCode == "BMF" || RecieptCode == "BHE" || RecieptCode == "PDF" || RecieptCode == "UNG" || RecieptCode == "OF")
                    {
                        /// Show remark for current fee demand
                        // txtRemark.Text = ds.Tables[0].Rows[0]["PARTICULAR"].ToString();
                        //txtFeeBalance.Text = ds.Tables[0].Rows[0]["EXCESS_AMT"].ToString();

                        /// Set FeeCatNo from datasource
                        ViewState["FeeCatNo"] = ds.Tables[0].Rows[0]["FEE_CAT_NO"].ToString();

                        /// Show total fee amount to be paid by the student in total amount textbox.
                        /// This total fee amount can be changed by user according to the student's current 
                        /// payment amount (i.e. student can do part payment of Fee also).
                        //  txtTotalAmount.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                        //txtTotalAmountShow.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                        // lblamtpaid.Text = objCommon.LookUp("acd_demand", "TOTAL_AMT", "IDNO=" + studId + " and SEMESTERNO=" + semesterNo + " and sessionno="+Convert.ToInt32(Session["currentsession"])+" and paytypeno="+Convert.ToInt16(ViewState["PaymentTypeNo"])+" and  RECIEPT_CODE='" + GetViewStateItem("ReceiptType")+"'");
                        //lblamtpaid.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                        //txtTotalAmount.Text = txtTotalAmountShow.Text;
                        // txtTotalFeeAmount.Text = txtTotalAmount.Text;
                        // double totalamt = Convert.ToDouble(this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString());
                        //if (totalamt < 0)
                        //{
                        //   // txtTotalAmountShow.Text = "0.00";
                        //}
                        //else
                        //{
                        //    txtTotalAmountShow.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                        //}
                        //txtTotalFeeAmount.Text = txtTotalAmountShow.Text;
                    }
                    /// If fee items are available then Enable
                    /// submit button and show the div FeeItems.
                    btnSubmit.Enabled = true;
                    //divFeeItems.Visible = true;


                }

                else
                {
                    /// As no demand record found, ask user if he want to create one.
                    //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
                    //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + ddlSemester.SelectedItem.Text + ".\\nDo you want to create demand for this semester?'))";
                    //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + ddlSemester.SelectedItem.Text + ".'))";
                    //this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";

                    /// If fee items are not available then disable
                    /// submit button and hide divFeeItems.
                    /// 
                    //flag = 1;
                    //objCommon.DisplayUserMessage(updFee, "No demand found for semester " + ddlSemester.SelectedItem.Text, this.Page);
                    //divStudentSearch.Visible = true;
                    //divStudInfo.Visible = false;
                    //divCurrentReceiptInfo.Visible = false;
                    //divFeeItems.Visible = false;
                    btnSubmit.Enabled = true;
                    // divPreviousReceipts.Visible = false;
                    return;
                }
            }

            else
            {
                int currentSemesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(SEMESTERNO,0) SEMESTERNO", "IDNO=" + studId));
                //int count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON (D.IDNO=S.IDNO)", "ISNULL(DM_NO,0) AS DM_NO", "IDNO =" + IDNO + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'" + "AND ISNULL(INSTAL_CANCEL,0)=0"));

                int DemCount = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON (D.IDNO=S.IDNO ) ", "COUNT(DM_NO)", "S.IDNO=" + studId + " AND RECIEPT_CODE='" + ddlReceipt.SelectedValue + "' AND S.SEMESTERNO=" + Convert.ToInt32((ddlSemester.SelectedValue))));
                if (DemCount != 0)
                {
                    int demandno = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON (D.IDNO=S.IDNO ) ", "ISNULL(DM_NO,0)DM_NO", "S.IDNO=" + studId + " AND RECIEPT_CODE='" + ddlReceipt.SelectedValue + "' AND S.SEMESTERNO=" + Convert.ToInt32((ddlSemester.SelectedValue))));
                }
                //int demandno = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON (D.IDNO=S.IDNO ) ", "ISNULL(DM_NO,0)DM_NO", "S.IDNO=" + studId + " AND RECIEPT_CODE='"+ddlReceipt.SelectedValue +"' AND S.SEMESTERNO=" + Convert.ToInt32((ddlSemester.SelectedValue))));

                //if (demandno == 0 || demandno == null)
                //{
                //     demandno = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON (D.IDNO=S.IDNO ) ", "ISNULL(DM_NO,0)DM_NO", "S.IDNO=" + studId + " AND RECIEPT_CODE='" + ddlReceipt.SelectedValue + "' AND S.SEMESTERNO=" + Convert.ToInt32(currentSemesterno)));
                //}
                DataSet dsFine = null;
               // dsFine = GetDemandOtherFee(demandno, studId);
                dsFine = feeController.GetFeeItems_DataFineAllotmentNew(Convert.ToInt32(ddlsession.SelectedValue), studId, Convert.ToInt32(ddlSemester.SelectedValue), ddlReceipt.SelectedValue, 0, 1, Convert.ToInt32(ViewState["ptype"].ToString()), ref status);

                //dsFine = feeController.GetFeesHeadsFineAllotment(ddlReceipt.SelectedValue);
                if (dsFine.Tables[0].Rows.Count > 0 && dsFine.Tables.Count > 0)
                {
                    //DataTable dt = ds.Tables[0];
                    LVFineallotment.DataSource = dsFine;
                    LVFineallotment.DataBind();
                    LVFineallotment.Visible = true;
                    divfineallotment.Visible = true;
                }
                else
                {

                }


            }
            // DisplayExcessAmount(studId);//sunita
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.PopulateFeeItemsSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlReceipt.SelectedValue == "OF")
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            FeeDemand feeDemand = new FeeDemand();
            feeDemand.StudentId = Convert.ToInt32(ViewState["idno"].ToString());
            feeDemand.SessionNo = Convert.ToInt32(ddlsession.SelectedValue.ToString());
            feeDemand.StudentName = lblName.Text;
            dcr.EnrollmentNo = txtEnrollno.Text;
            feeDemand.DegreeNo = Convert.ToInt32(lblDegreeno.ToolTip);
            feeDemand.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            feeDemand.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            feeDemand.ReceiptTypeCode = ddlReceipt.SelectedValue;
            dcr.FeeHeadAmounts = this.GetFeeItems();
            feeDemand.AdmBatchNo = Convert.ToInt32(lblAdmBatch.ToolTip);
            feeDemand.PaymentTypeNo = Convert.ToInt32(ViewState["ptype"].ToString());
            feeDemand.UserNo = Convert.ToInt32(Session["userno"].ToString());
            feeDemand.Remark = "Other Fees/Fine Allotment";
            feeDemand.CollegeCode = Session["colcode"].ToString();
            feeDemand.TotalFeeAmount = Convert.ToDouble(this.GetTotalFeeDemandAmount());
            int ret = Convert.ToInt32(feeController.CreateDemandForExaminationForFineAllotment(feeDemand, dcr));
            if (ret != -99 && ret > 0)
            {
                objUCommon.DisplayMessage(updFine, "Other Fees/Fine Alloted Successfully.", this.Page);
            }
        }
        else
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            FeeDemand feeDemand = new FeeDemand();
            feeDemand.StudentId = Convert.ToInt32(ViewState["idno"].ToString());
            feeDemand.SessionNo = Convert.ToInt32(ddlsession.SelectedValue.ToString());
            feeDemand.StudentName = lblName.Text;
            dcr.EnrollmentNo = txtEnrollno.Text;
            feeDemand.DegreeNo = Convert.ToInt32(lblDegreeno.ToolTip);
            feeDemand.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            feeDemand.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            feeDemand.ReceiptTypeCode = ddlReceipt.SelectedValue;
            dcr.FeeHeadAmounts = this.GetFeeItemsFineAllotment();
            feeDemand.AdmBatchNo = Convert.ToInt32(lblAdmBatch.ToolTip);
            feeDemand.PaymentTypeNo = Convert.ToInt32(ViewState["ptype"].ToString());
            feeDemand.UserNo = Convert.ToInt32(Session["userno"].ToString());
            feeDemand.Remark = "Other Fees Demand Created";
            feeDemand.CollegeCode = Session["colcode"].ToString();
            feeDemand.TotalFeeAmount = Convert.ToDouble(this.GetTotalDemandAmtFineAllotment());
            int ret = Convert.ToInt32(feeController.CreateDemandForExaminationForFineAllotment(feeDemand, dcr));
            if (ret != -99 && ret > 0)
            {
                objUCommon.DisplayMessage(updFine, "Demand Created Successfully..", this.Page);
            }
        }
    }

    private double GetTotalFeeDemandAmount()
    {
        double totalFeeAmt = 0.00;
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {

                //totalFeeAmt += ((dr["AMOUNT"].ToString().Trim() != string.Empty) ? Convert.ToDouble(dr["AMOUNT"].ToString()) : 0.00);
                totalFeeAmt += (((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim() != string.Empty) ? Convert.ToDouble(((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim()) : 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetTotalFeeDemandAmount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return totalFeeAmt;
    }

    //private double GetTotalFeeDemandAmountNew()
    //{
    //    double totalFeeAmt = 0.00;
    //    try
    //    {
    //        foreach (ListViewDataItem item in lvFeesHead.Items)
    //        {

    //            //totalFeeAmt += ((dr["AMOUNT"].ToString().Trim() != string.Empty) ? Convert.ToDouble(dr["AMOUNT"].ToString()) : 0.00);
    //            totalFeeAmt += (((TextBox)item.FindControl("txtDemandAmount")).Text.Trim() != string.Empty) ? Convert.ToDouble(((TextBox)item.FindControl("txtDemandAmount")).Text.Trim()) : 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_FeeCollection.GetTotalFeeDemandAmount() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return totalFeeAmt;
    //}

    private FeeHeadAmounts GetFeeItems()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                int feeHeadNo = 0;
                double feeAmount = 0.00;

                string fee_head = string.Empty;//***************

                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************

                if (fee_head != "LATE FEE")//*****************
                {
                    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
                    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                        feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
                    if (feeAmt != null && feeAmt != string.Empty)
                        feeAmount = Convert.ToDouble(feeAmt);

                    feeHeadAmts[feeHeadNo - 1] = feeAmount;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }

    //private FeeHeadAmounts GetFeeItemsNew()
    //{
    //    FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
    //    try
    //    {
    //        foreach (ListViewDataItem item in lvFeesHead.Items)
    //        {
    //            int feeHeadNo = 0;
    //            double feeAmount = 0.00;

    //            string fee_head = string.Empty;//***************

    //            fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAMENEW")).Value;//*****************

    //            if (fee_head != "LATE FEE")//*****************
    //            {
    //                //string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
    //                //if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
    //                //    feeHeadNo = Convert.ToInt32(feeHeadSrNo);

    //                string feeAmt = ((TextBox)item.FindControl("txtDemandAmount")).Text.Trim();
    //                if (feeAmt != null && feeAmt != string.Empty)
    //                    feeAmount = Convert.ToDouble(feeAmt);

    //                //feeHeadAmts[feeHeadNo - 1] = feeAmount;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_FeeCollection.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return feeHeadAmts;
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlReceipt.SelectedIndex > 0)
        {
            if (ddlReceipt.SelectedValue == "OF")
            {

            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>10", "SEMESTERNO");
            }
        }
        else
        {
            txtEnrollno.Text = string.Empty;
        }
    }

    public DataSet GetDemandOtherFee(int demandId, int studentId)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DMNO", demandId),
                    new SqlParameter("@P_IDNO", studentId)
                };
            ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_DEMAND_MOD_GET_DEMAND_OTHER_FEE", sqlParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetDemand() --> " + ex.Message + " " + ex.StackTrace);
        }
        return ds;
    }

    private FeeHeadAmounts GetFeeItemsFineAllotment()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();
        try
        {
            foreach (ListViewDataItem item in LVFineallotment.Items)
            {
                int feeHeadNo = 0;
                double feeAmount = 0.00;

                string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNoFine")).Text;
                if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                    feeHeadNo = Convert.ToInt32(feeHeadSrNo);

                string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmountFine")).Text.Trim();
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

    private double GetTotalDemandAmtFineAllotment()
    {
        double totalAmt = 0.00;
        try
        {
            foreach (ListViewDataItem item in LVFineallotment.Items)
            {
                string amt = (item.FindControl("txtFeeItemAmountFine") as TextBox).Text.Trim();
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
}