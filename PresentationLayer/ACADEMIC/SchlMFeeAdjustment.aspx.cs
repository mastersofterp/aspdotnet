using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Drawing;

public partial class ACADEMIC_SchlMFeeAdjustment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    DailyCollectionRegister dcr = new DailyCollectionRegister();
    int prev_status;

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
                 //PopulateDropDown();
                 //PopulateDropDownList();
                // Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                // Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["ReceiptType"] = "TF";

               


                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                {
                    int studId = int.Parse(Request.QueryString["id"].ToString());
                    
                    int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studId + ""));

                     if (studId > 0)
                     {
                         if (semester != 1)
                         {
                              int sessionno = 0;
                                int sessionmax = 0;
                                string feedback = string.Empty;
                                sessionno = Convert.ToInt16(Session["currentsession"].ToString());

                                sessionmax = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(MAX(SESSIONNO),0)  ", "EXAMTYPE=1 AND SESSIONNO < " + sessionno));

                               // feedback = objCommon.LookUp("REFF", "Feedback_Status", "");

                               // if (feedback == "True")
                               // {
                                   // int Feedback = Convert.ToInt16(SFB.FeedbackCount(studId, sessionmax));

                                    //if (Feedback == 1)
                                   // {
                                        this.DisplayInformation(studId);
                                    //}
                                  //  else
                                   // {
                                        //objCommon.DisplayUserMessage(updFee, "Student has not provided Feedback.", this.Page);
                                    //}
                               // }
                               // else
                               // {
                                   // this.DisplayInformation(studId);
                                //}
                         }
                     }
                }
                objCommon.FillDropDownList(ddlSchltypeMultiFee, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0", "SCHOLORSHIPTYPENO");  // added on 2020 feb 11
                objCommon.FillDropDownList(ddlReceipttype, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO=1", "RECIEPT_TITLE");

                ddlReceipttype.SelectedValue ="TF";

                this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SchlMFeeAdjustment.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SchlMFeeAdjustment.aspx");
        }
    }

 

    #region Tab 2 Multiple Fee Head Adjustment


    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        try
        {
            StudentFeedBackController SFB = new StudentFeedBackController();
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                if (ddlSemester.SelectedIndex > 0)
                {
                    int ISCounter = Convert.ToInt32(objCommon.LookUp("ACD_COUNTER_REF", "COUNT(*)", "RECEIPT_PERMISSION IN('" + ViewState["ReceiptType"] + "')  AND UA_NO=" + Session["userno"]));//AND (REC1<>0 OR REC2<>0 OR REC3<>0 OR REC4<>0 OR REC5<>0)
                    if (ISCounter != 0)
                    {
                        int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
                        ViewState["idno"] = studentId;
                        int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studentId + ""));
                        int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP ", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                        if (count > 0)
                        {                                                 
                           string SchAdjAmt = (objCommon.LookUp("ACD_DCR D INNER JOIN ACD_STUDENT_SCHOLERSHIP SS ON SS.IDNO=D.IDNO", "D.SCH_ADJ_AMT", "D.IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND PAY_MODE_CODE='SA' AND  D.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));

                            if (SchAdjAmt == "0" || SchAdjAmt == string.Empty)
                            {
                                if (studentId > 0)
                                {
                                    if (semester != 1)
                                    {
                                        int sessionno = 0;
                                        int sessionmax = 0;
                                        //string feedback = string.Empty;

                                        sessionno = Convert.ToInt16(Session["currentsession"].ToString());

                                        sessionmax = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO) ", "EXAMTYPE=1 AND SESSIONNO < " + sessionno));

                                        //feedback = objCommon.LookUp("REFF", "Feedback_Status", "");

                                        //if (feedback == "True")
                                        //{
                                        //    int Feedback = Convert.ToInt16(SFB.FeedbackCount(studentId, sessionmax));

                                        //    if (Feedback == 1)
                                        //    {
                                        //        this.DisplayInformation(studentId);
                                        //    }
                                        //    else
                                        //    {
                                        //        objCommon.DisplayUserMessage(updFee, "Student has not provided Feedback.", this.Page);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    this.DisplayInformation(studentId);
                                        //}

                                        this.DisplayInformation(studentId);
                                    }
                                    else
                                    {
                                        this.DisplayInformation(studentId);
                                    }
                                }

                                else
                                    objCommon.DisplayUserMessage(updFee, "No student found with given enrollment number.", this.Page);
                                    return;
                            }
                            else
                                objCommon.DisplayUserMessage(updFee, "Scholarship Adjustment already done for this Student.", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(updFee, "Scholarship Allotment not done for this Student.", this.Page);
                            divReceipttype.Visible = false;
                            return;
                        }
                    }
                    else
                        objCommon.DisplayUserMessage(updFee, "Counter is Not Assign To Generate Receipt No. Please Assign Counter For User := " + Session["userfullname"], this.Page);
                    return;
                }
                else
                    objCommon.DisplayUserMessage(updFee, "Please select semester.", this.Page);
                return;
            }
            else
                objCommon.DisplayUserMessage(updFee, "Please enter enrollment number.", this.Page);
            return;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SchlMFeeAdjustment.btnShowInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    private void DisplayInformation(int idno)
    {
        #region Display Student Information

        /// Display student's personal and academic data in 
        /// student information section
      
        DataSet ds = feeController.GetStudentInfoByIdFor_Scholarship(idno);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            // show student information
            this.PopulateStudentInfoSection(dr);
            divStudInfo.Style["display"] = "block";
            divReceipttype.Visible = true;
              
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
           // divpanel.Visible = false;
        }
        else
        {
            objCommon.DisplayUserMessage(updFee, "Unable to retrieve student's record.", this.Page);
            return;
        }
        int semNo = Convert.ToInt32(ddlSemester.SelectedValue);
        this.PopulateFeeItemsSection(semNo);
        ItemsEnabled();

        #endregion
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {            
            
            
             //string SchAmt = (objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "ISNULL(SCHL_AMOUNT,0)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
            #region Bind data to labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            //lblSchamt.Text = SchAmt;
            //lblschtype.Text = dr["SCHOLORSHIPNAME"].ToString();
            //lblschtype.Attributes["style"] = "color:green";
            //lblSchamt.Attributes["style"] = "color:green";
            #endregion
            int PayTypeno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME='" + lblPaymentType.Text + "'"));
            ViewState["PaymentTypeNo"] = PayTypeno;
            #region Show Student's Data Selected in DDLs
            //if (ddlSemester.Items.FindByValue(dr["SEMESTERNO"].ToString()) != null)
            //    ddlSemester.SelectedValue = dr["SEMESTERNO"].ToString();
            //else
            //    ddlSemester.SelectedIndex = 0;

            //if (ddlPaymentType.Items.FindByValue(dr["PTYPE"].ToString()) != null)
            //    ddlPaymentType.SelectedValue = dr["PTYPE"].ToString();
            //else
            //    ddlPaymentType.SelectedIndex = 0;

            //if (ddlScholarship.Items.FindByValue(dr["SCHOLORSHIPTYPENO"].ToString()) != null)
            //    ddlScholarship.SelectedValue = dr["SCHOLORSHIPTYPENO"].ToString();
            //else
            //    ddlScholarship.SelectedIndex = 0;
            #endregion

            #region Secure imporatant data
            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the fee collection record.
            ViewState["StudentId"] = dr["IDNO"].ToString();
            ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["YearNo"] = dr["YEAR"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();
            //ddlSemester.SelectedValue;
            ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
            ViewState["PaymentMode"] = "SA";
            #endregion

            // this.objCommon.FillDropDownList(ddlCurrency, "ACD_CURRENCY_TITLE A INNER JOIN ACD_CURRENCY B ON (A.CUR_NO = B.CUR_NO) ", "distinct A.CUR_NO", "B.CUR_NAME", "RECIEPT_CODE='" + ViewState["ReceiptType"] + "' AND PAYTYPENO=" + Convert.ToInt16(ViewState["PaymentTypeNo"]) + "", "A.CUR_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SchlMFeeAdjustment.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private FeeHeadAmounts ItemsEnabled()
    {
        FeeHeadAmounts feeHeadAmts = new FeeHeadAmounts();

        try
        {
            //  trExcessAmt.Visible = false;
            // trExcesschk.Visible = false;
            // trNote.Visible = false; //Sunita
            //dvchkScholar.Visible = false;//Dileep
            //dvScholar.Visible = false;//Dileep
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                Label lblDAmt = item.FindControl("lblDAmount") as Label;
                if (lblDAmt.Text == "0.00" || lblDAmt.Text == string.Empty)
                    ((TextBox)item.FindControl("txtFeeItemAmount")).Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SchlMFeeAdjustment.ItemsEnabled() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }


    private void PopulateFeeItemsSection(int semesterNo)
    {
        try
        {
            int status = 0;

            /// Get fee demand of the student for given semester no.
            /// if demand found then display fee items. Use status variable to 
            /// flag the demand status. status = -99 means demand not found.
            
            int studId = Int32.Parse((GetViewStateItem("idno") != string.Empty) ? GetViewStateItem("idno") : "0");
            //int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            //int sessionno = 0;
            //if (examType == 1)
            //{
            //    sessionno = Convert.ToInt32(Session["currentsession"]);
            //}
            //else
            //{
            //    sessionno = Convert.ToInt32(Session["currentsession"]) + 1;
            //}
            DataSet ds = null;
            ds = feeController.GetFeeItems_Data_ForScholarship(Convert.ToInt32(Session["currentsession"]), studId, semesterNo, GetViewStateItem("ReceiptType"), Convert.ToInt32(ddlExamType.SelectedValue), 1, Convert.ToInt16(ViewState["PaymentTypeNo"]), ref status);

            if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string CollegeId = objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_STUDENT S ON(D.IDNO=S.IDNO)", "ISNULL(COLLEGE_ID,0)COLLEGE_ID", "D.IDNO=" + Convert.ToInt32(ViewState["StudentId"]) + " AND RECIEPT_CODE='TF' AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + " AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["SemesterNo"]));

                if (CollegeId == "")
                {
                    CollegeId = "0";
                }

                ViewState["COLLEGE_ID"] = CollegeId;

                /// Bind fee items list view with the data source found.
                lvFeeItems.DataSource = ds;
                lvFeeItems.DataBind();
                string RecieptCode = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                if (RecieptCode == "TF" || RecieptCode == "EF" || RecieptCode == "HF" || RecieptCode == "BCA" || RecieptCode == "MBA" || RecieptCode == "PG" || RecieptCode == "EVF" ||
                    RecieptCode == "PGF" || RecieptCode == "BMF" || RecieptCode == "BHE" || RecieptCode == "PDF" || RecieptCode == "UNG" || RecieptCode == "TOF")
                {
                    /// Show remark for current fee demand
                    txtRemark.Text = ds.Tables[0].Rows[0]["PARTICULAR"].ToString();
                    txtFeeBalance.Text = ds.Tables[0].Rows[0]["EXCESS_AMT"].ToString();

                    /// Set FeeCatNo from datasource
                    ViewState["FeeCatNo"] = ds.Tables[0].Rows[0]["FEE_CAT_NO"].ToString();

                    /// Show total fee amount to be paid by the student in total amount textbox.
                    /// This total fee amount can be changed by user according to the student's current 
                    /// payment amount (i.e. student can do part payment of Fee also).
                    //  txtTotalAmount.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                    //   txtTotalAmountShow.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                    // lblamtpaid.Text = objCommon.LookUp("acd_demand", "TOTAL_AMT", "IDNO=" + studId + " and SEMESTERNO=" + semesterNo + " and sessionno="+Convert.ToInt32(Session["currentsession"])+" and paytypeno="+Convert.ToInt16(ViewState["PaymentTypeNo"])+" and  RECIEPT_CODE='" + GetViewStateItem("ReceiptType")+"'");
                    //lblamtpaid.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                    //txtTotalAmount.Text = txtTotalAmountShow.Text;
                    // txtTotalFeeAmount.Text = txtTotalAmount.Text;
                    // txtTotalFeeAmount.Text = txtTotalAmountShow.Text;
                }
                /// If fee items are available then Enable
                /// submit button and show the div FeeItems.
                btnSubmit.Enabled = true;
                divFeeItems.Visible = true;


            }
            else
            {
                ///// As no demand record found, ask user if he want to create one.
                //this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'>";
                //this.divMsg.InnerHtml += " if(confirm('No demand found for semester " + ddlSemester.SelectedItem.Text + ".\\nDo you want to create demand for this semester?'))";
                //this.divMsg.InnerHtml += "{__doPostBack('CreateDemand', '');}</script>";

                /// If fee items are not available then disable
                /// submit button and hide divFeeItems.
               

                divFeeItems.Visible = false;
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SchlMFeeAdjustment.PopulateFeeItemsSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
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


            ////foreach (ListViewDataItem item in lvFeeItems.Items)
            ////{
            ////    int feeHeadNo = 0;
            ////    double feeAmount = 0.00;

            ////    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
            ////    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
            ////        feeHeadNo = Convert.ToInt32(feeHeadSrNo);

            ////    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
            ////    if (feeAmt != null && feeAmt != string.Empty)
            ////        feeAmount = Convert.ToDouble(feeAmt);

            ////    feeHeadAmts[feeHeadNo - 1] = feeAmount;
            ////}

            //foreach (ListViewDataItem item in lvFeeItems.Items)
            //{
            //    int feeHeadNo = 0;
            //    double feeAmount = 0.00;
            //    string fee_head = string.Empty;//***************
            //    fee_head = ((Label)item.FindControl("FEE_LONGNAME")).Text;//*****************

            //    string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
            //    string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
            //    if(fee_head != "LATE FEE")//*****************
            //    {
            //        ////string feeHeadSrNo = ((Label)item.FindControl("lblFeeHeadSrNo")).Text;
            //        if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
            //            feeHeadNo = Convert.ToInt32(feeHeadSrNo);

            //        ////string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
            //        if (feeAmt != null && feeAmt != string.Empty)
            //            feeAmount = Convert.ToDouble(feeAmt);

            //        feeHeadAmts[feeHeadNo - 1] = feeAmount;
            //    }

            //    //*****************
            //    if(fee_head == "LATE FEE")
            //    {
            //        if (feeAmt != null && feeAmt != string.Empty)
            //        {
            //            feeAmount = Convert.ToDouble(feeAmt);
            //            late_fee = feeAmount;
            //        }
            //    }
            //    //*****************
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SchlMFeeAdjustment.GetFeeItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return feeHeadAmts;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveTransaction();
    }
    private void SaveTransaction()
    {
        Bind_FeeCollectionData();
    }
    private DailyCollectionRegister Bind_FeeCollectionData()
    {
        DailyCollectionRegister dcr = new DailyCollectionRegister();
        try
        {
            dcr.StudentId = (GetViewStateItem("idno") != string.Empty) ? Convert.ToInt32(GetViewStateItem("idno")) : 0;
            dcr.EnrollmentNo = lblRegNo.Text;
            dcr.StudentName = lblStudName.Text;
            dcr.BranchNo = (GetViewStateItem("BranchNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("BranchNo")) : 0;
            dcr.BranchName = lblBranch.Text;
            dcr.YearNo = (GetViewStateItem("YearNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("YearNo")) : 0;
            dcr.DegreeNo = (GetViewStateItem("DegreeNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("DegreeNo")) : 0;
            //dcr.SemesterNo = (GetViewStateItem("SemesterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("SemesterNo")) : 0;
            //dcr.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
            dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            ////if (examType == 1)
            ////{
            dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
            ////}
            ////else
            ////{
            ////    dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString()) + 1;
            ////}
            dcr.Currency = 1;
            dcr.FeeHeadAmounts = this.GetFeeItems();



            dcr.TotalAmount = (txtTotalFeeAmount.Text.Trim() != string.Empty) ? Convert.ToDouble(txtTotalFeeAmount.Text) : 0.00;

            DemandDrafts[] dds = null;
            dcr.DemandDraftAmount = 0.00;
            dcr.PaidDemandDrafts = dds;

            dcr.CashAmount = 0.00;

            dcr.CounterNo = (GetViewStateItem("CounterNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("CounterNo")) : 0;

            dcr.ReceiptTypeCode = GetViewStateItem("ReceiptType");

            // dcr.ReceiptNo = txtReceiptNo.Text.Trim();

            //int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(REC_NO)", "REC_NO=" + txtReceiptNo.Text));
            //if (count == 1)
            //{
            //    objCommon.DisplayMessage("Receipt No Already Exists", this.Page); 
            //}
            //else
            //{
            //    dcr.ReceiptNo = txtReceiptNo.Text.Trim();
            //}

            dcr.ReceiptDate = DateTime.Now;
            dcr.PaymentModeCode = "SA";
            dcr.PaymentType = "SA";
            dcr.FeeCatNo = (GetViewStateItem("FeeCatNo") != string.Empty) ? Convert.ToInt32(GetViewStateItem("FeeCatNo")) : 0;


            /// For Cash(C) or ATM(A) transaction cancel status should be false.
            /// In case of bank chalan (B) cancel status is by default true, because
            /// we have just printing the chalan we have not yet received the money.
            

            dcr.IsCancelled = (GetViewStateItem("PaymentMode") == "C" ||
                GetViewStateItem("PaymentMode") == "A" || GetViewStateItem("PaymentMode") == "SA") ? false : true;

            /// For Cash(C) or ATM(A) transaction reconciliation status should be true.
            /// In case of bank chalan (B) reconciliation status is by default false, because
            /// we have just printing the chalan we have not yet received the money.
            dcr.IsReconciled = (GetViewStateItem("PaymentMode") == "C" ||
                GetViewStateItem("PaymentMode") == "A" || GetViewStateItem("PaymentMode") == "SA") ? true : false;

            // Applicable only for bank chalan 
            if (GetViewStateItem("PaymentMode") == "B")
                dcr.ChallanDate = DateTime.Today;

            /// This status is used to mark/flag unpaid/not received bank chalans.
            /// Default is false. if unpaid then will be marked as true.
            dcr.IsDeleted = false;
            dcr.CompanyCode = string.Empty;
            dcr.RpEntry = string.Empty;
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.PrintDate = DateTime.Today;
            dcr.Remark = txtRemark.Text.Trim();
            dcr.ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
            dcr.CollegeCode = Session["colcode"].ToString();

            //this is add to excess amount maintain. date: 10/04/2012
            // check the status of configuration page

            //string chkConfig = objCommon.LookUp("ACD_CONFIG", "STATUS", "CONFIGNO=1");
            //if(chkConfig == "Y")
            //{
            //dcr.ExcessAmount = Convert.ToDouble(txtTotalAmount.Text) - Convert.ToDouble(txtTotalFeeAmount.Text);

            dcr.ExcessAmount = 0.00;
            dcr.CreditDebitNo = string.Empty;
            dcr.TransReffNo = string.Empty;
            int ScholarshipId =Convert.ToInt32(ddlSchltypeMultiFee.SelectedValue);  
        
            DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + dcr.StudentId + " AND SEMESTERNO=" + dcr.SemesterNo + " AND RECON=1 AND RECIEPT_CODE='TF' GROUP BY IDNO )A  ON (A.IDNO=D.IDNO)", "DISTINCT D.IDNO", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,SUM(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT", " D.IDNO = " + dcr.StudentId + " AND D.RECIEPT_CODE='TF' AND SEMESTERNO=" + dcr.SemesterNo + " GROUP BY D.IDNO,DRTOTAL_AMT", string.Empty);

            if (dsDueFee.Tables[0].Rows.Count > 0)
            {
                double demandtotal = Convert.ToDouble(dsDueFee.Tables[0].Rows[0]["DTOTAL_AMT"]);
                double DcrTotal = Convert.ToDouble(dsDueFee.Tables[0].Rows[0]["DRTOTAL_AMT"]);

                if (demandtotal != 0 || DcrTotal != 0)
                {

                    if (demandtotal > DcrTotal)
                    {
                        double remaining = Convert.ToDouble(demandtotal - DcrTotal);
                        if (dcr.TotalAmount > remaining)
                            dcr.ExcessAmount = dcr.TotalAmount - remaining;
                    }
                    else
                    {

                    }
                }

                //if (txtTotalFeeAmount.Text == lblSchamt.Text)
                //{
                    if (feeController.SaveFeeCollection_TransactionScholarship(ref dcr, 0, ScholarshipId))
                    {
                        string ScholorshipNO = (objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "ISNULL(SCHLSHPNO,0)", "IDNO=" + dcr.StudentId + "AND SEMESTERNO=" + dcr.SemesterNo + " AND SCHOLARSHIP_ID=" + ScholarshipId));

                        feeController.UpdateShcolorshipStatusForMultipleFeeHeads(ScholorshipNO, dcr.TotalAmount, int.Parse(Session["userno"].ToString()), ViewState["ipAddress"].ToString());

                        int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + Convert.ToInt32(dcr.StudentId) + "  AND PAY_MODE_CODE= 'SA' AND SCHOLARSHIP_ID =" + ScholarshipId));

                        if (count > 0)
                        {
                            objCommon.DisplayUserMessage(updFee, " Scholarship Adjusted Successfully ", this.Page);
                            btnSubmit.Enabled = false;
                        }
                    }           
                    else
                    {
                        objCommon.DisplayUserMessage(updFee, "Scholarship Total Amount is not Match with Scholarship Allotment Amount", this.Page);
                    }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.Bind_FeeCollectionData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcr;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    protected void ddlSchltypeMultiFee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}