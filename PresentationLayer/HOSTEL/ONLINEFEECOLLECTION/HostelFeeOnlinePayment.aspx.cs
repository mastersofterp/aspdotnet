using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using HostelBusinessLogicLayer.BusinessLogic;

public partial class HOSTEL_ONLINEFEECOLLECTION_HostelFeeOnlinePayment : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelFeeCollectionController objFee = new HostelFeeCollectionController();
    DailyCollectionRegister dcr = new DailyCollectionRegister();

    public string action = string.Empty;
    public string hash = string.Empty;

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
                if (Session["payment"].ToString().Equals("payment"))
                {
                    Page.Title = Session["coll_name"].ToString();
                }
                else
                { // Check User Authority 
                    if (Session["usertype"].ToString().Equals("2"))
                    {
                        this.CheckPageAuthorization();
                    }
                }

                // Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                
                int StudIDNO = 0;
                if (Session["usertype"].ToString().Equals("2"))
                {

                    //divEnrollment.Visible = false;
                    divReceiptType.Attributes.Add("class", "form-group col-lg-3 col-md-6 col-12");
                    divStudSemester.Attributes.Add("class", "form-group col-lg-3 col-md-6 col-12");
                    StudIDNO = Convert.ToInt32(Session["idno"]);
                    Session["stuinfoidno"] = StudIDNO;

                    //Added by Saurabh L on 30/07/2022
                    //Purpose: Payment Gateway checksum get by OrgId and for Crescent get Hostel-wise
                    if (Convert.ToInt32(Session["OrgId"]) == 1)
                    {
                        Session["payactivityno"] = "8";
                    }
                    else if (Convert.ToInt32(Session["OrgId"]) == 2)
                    {
                        string Gender = objCommon.LookUp("ACD_STUDENT", "SEX", "IDNO =" + StudIDNO);

                        if (Gender == "M")
                        {
                            Session["payactivityno"] = "8";  //If else condition Added by Saurabh L on 27/07/2022
                        }                                    //Purpose: To get Account key as per boys Hostel or Girl hostel for Crescent 
                        else
                        {
                            Session["payactivityno"] = "9";
                        }
                    }
                    else
                    {
                        Session["payactivityno"] = "8";
                    }
                    //-------------End by Saurabh L -----------------------------
                   
                    myModal2.Visible = false;
                    DisplayInformation(StudIDNO);
                    DisplayStudentInfo(StudIDNO);
                    PopulateDropDown();
                }
                else
                {
                    if (Convert.ToInt32(Session["OrgId"]) == 1)
                    {
                        Session["payactivityno"] = "8";
                    }

                    myModal2.Visible = true;
                    btnCancel.Visible = false;

                    //Search Pannel Dropdown Added by Swapnil
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                    ddlSearch.SelectedIndex = 0;
                    //End Search Pannel Dropdown

                }

            }
        }
    }

    protected void ddlSemester_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnPayment.Visible = false;
        divMSG.Visible = false;
        //div_Studentdetail.Visible = false;
        DataSet ds = null;
        int IDNO = Convert.ToInt32(Session["stuinfoidno"]);
        ViewState["StudId"] = IDNO;
        int exam_type = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(EXAM_PTYPE,0)EXAM_PTYPE", "IDNO=" + IDNO));
        ViewState["Exam_Type"] = exam_type;
        int ptype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(PTYPE,0)PTYPE", "IDNO=" + IDNO));
        ViewState["pType"] = ptype;

      
            int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO =" + IDNO + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'"));

           
                divDirectPayment.Visible = true;
                // DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 AND PAY_MODE_CODE<>'SA' AND ISNULL(SCH_ADJ_AMT,0)=0 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'TF' THEN 'Admission Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);
                DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'HF' THEN 'Hostel Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND D.RECIEPT_CODE = 'HF' AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);
                if (dsDueFee.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDueFee.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(dsDueFee.Tables[0].Rows[i]["DRTOTAL_AMT"].ToString()) < Convert.ToDecimal(dsDueFee.Tables[0].Rows[i]["DTOTAL_AMT"].ToString()))
                        {
                            objCommon.DisplayMessage(this.Page, " Please Pay the Fees of " + dsDueFee.Tables[0].Rows[i]["FEE_TITLE"].ToString() + " Semester " + dsDueFee.Tables[0].Rows[i]["SEMESTERNAME"].ToString(), this.Page);
                            return;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {

                }

                SemesterWiseFees();
               
    }

    public void DisplayStudentInfo(int idno)
    {
        #region Display Student Information
        DataSet ds;

        ds = objFee.GetStudentInfoById(idno, Convert.ToInt32(Session["OrgId"].ToString()));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            div_Studentdetail.Visible = true;
            lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblStudBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
            lblStudRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
            lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            lblMailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
            hdfState.Value = ds.Tables[0].Rows[0]["STATENAME"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STATENAME"].ToString();
            hdfZipCode.Value = ds.Tables[0].Rows[0]["PPINCODE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["PPINCODE"].ToString();
            hdfIdno.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
            hdfName.Value = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            hdfEmailId.Value = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
            hdfMobileNo.Value = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            hdfCity.Value = ds.Tables[0].Rows[0]["CITY"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["CITY"].ToString();// ds.Tables[0].Rows[0]["CITYNAME"].ToString();
            //hdfSessioNo.Value = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            divReceiptType.Visible = true;
            divStudSemester.Visible = true;
            string yearNo = ds.Tables[0].Rows[0]["YEAR"].ToString();
            Session["YEARNO"] = yearNo;
            PopulateDropDown();
        }
        else//MAKE CHANGE HERE
        {
            div_Studentdetail.Visible = false;
            divReceiptType.Visible = false;
            divStudSemester.Visible = false;
            //divPreviousReceipts.Visible = false;
            //divHidPreviousReceipts.InnerHtml = "No Record Found.<br/>";
        }
        //divPreviousReceipts.Visible = true;
        #endregion
    }

    public void SemesterWiseFees()
    {
        DataSet ds = null;
        int IDNO = Convert.ToInt32(ViewState["StudId"].ToString());
        ds = objFee.GetStudentFeesforOnlinePayment(ddlReceiptType.SelectedValue, Convert.ToInt32(ddlSemester.SelectedValue), IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            decimal councellingamt = 0;
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO =" + IDNO));
            int paytype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO =" + IDNO));

            int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO =" + IDNO));



            decimal latefee = Convert.ToDecimal(ds.Tables[0].Rows[0]["LATE_FEE"].ToString());

            decimal total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AMOUNTS"));

            total = total + latefee;

            if (total > 0)
            {
                btnPayment.Visible = true;
                btnReciept.Visible = false;
                //div_Studentdetail.Visible = true;
                //lblRegno.Text = ds.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString();
                //lblStudName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                //lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                //lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                //lblStudBranch.Text = ds.Tables[0].Rows[0]["BRANCHLNAME"].ToString();
                //lblStudRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
                //lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                //lblMailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //hdfState.Value = ds.Tables[0].Rows[0]["STATENAME"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STATENAME"].ToString();
                //hdfZipCode.Value = ds.Tables[0].Rows[0]["PPINCODE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["PPINCODE"].ToString();
                //hdfIdno.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                //hdfName.Value = ds.Tables[0].Rows[0]["NAME"].ToString();
                //hdfEmailId.Value = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //hdfMobileNo.Value = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                //hdfCity.Value = ds.Tables[0].Rows[0]["CITYNAME"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["CITYNAME"].ToString();// ds.Tables[0].Rows[0]["CITYNAME"].ToString();
                hdfSessioNo.Value = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                ViewState["SessionNo"] = hdfSessioNo.Value;

                gvFeeAmount.DataSource = ds.Tables[0];
                gvFeeAmount.DataBind();

                //////Calculate Sum and display in Footer Row
                //////decimal total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AMOUNTS"));
                gvFeeAmount.FooterRow.Cells[1].Text = "Total Amount";
                gvFeeAmount.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvFeeAmount.FooterRow.Cells[2].Text = total.ToString("N2");
                gvFeeAmount.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                lblAmount.Text = total.ToString();//ds.Tables[0].Rows[0]["FEESAMOUNT"].ToString();
                hdfAmount.Value = total.ToString();//ds.Tables[0].Rows[0]["FEESAMOUNT"].ToString();

                // Below condition code added by Saurabh L on 5th June 2023 Purpose: 
                int OnlinePay_Direct_From_OnlinePayment = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_Stud_OnlinePay_Direct_From_OnlinePayment", "OrganizationId=" + Convert.ToInt32(Session["OrgId"])));

                if (OnlinePay_Direct_From_OnlinePayment == 0)
                {
                    if (Session["StudentSelectedRoom"] == null)
                    {
                        divMSG.Visible = true;
                        lblmsg.Attributes.Add("style", "color:red");
                        lblmsg.Text = "Selected Room not Found, Please select Room by click on below 'Redirect' button.";
                        btnPayment.Text = "Redirect";
                    }
                }
                //--------------------- by Saurabh L on 5th June 2023--------------------
            }
            else
            {
                // objCommon.DisplayMessage("Fees already paid for selected semester.", this.Page);
                divMSG.Visible = true;
                btnReciept.Visible = true;
                div_Studentdetail.Visible = true;
                lblmsg.Attributes.Add("style", "color:green");
                lblmsg.Text = "Fees already paid for selected semester.";
                return;
            }
        }
        else
        {
            // objCommon.DisplayMessage("", this.Page);
            divMSG.Visible = true;
            lblmsg.Attributes.Add("style", "color:red");
            lblmsg.Text = "Fees demand not Created for Selected Semester, Please Contact MIS Administrator!!";
            return;
        }
    }


    protected void btnPayment_Click(object sender, EventArgs e)
    {
        // Below condition code added by Saurabh L on 5th June 2023 Purpose: Restricted Crescent students not directly pay from 'Online Payment' form 
        int OnlinePay_Direct_From_OnlinePayment = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_Stud_OnlinePay_Direct_From_OnlinePayment", "OrganizationId=" + Convert.ToInt32(Session["OrgId"])));

        if (OnlinePay_Direct_From_OnlinePayment == 0)
        {
            if (Session["StudentSelectedRoom"]  == null)
            {
                Response.Redirect("~/HOSTEL/ViewRoomStatusAndAllotment.aspx?pageno=1075");
            }
        }
        else
        {
            if (Session["StudentSelectedRoom"] == null) // Added condition for Direct payment by student and room allot from Admin side and demad creation
            {
                Session["StudentSelectedRoom"] = "0";
            }
        }
        //------------ End by Saurabh L on 5th June 2023
            

        int status1 = 0;
        int Currency = 1;
        string amount = string.Empty;
        amount = Convert.ToString(hdfAmount.Value);
        try
        {
            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
          //  DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = amount;
            ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
          //  dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            Session["studName"] = lblStudName.Text;
            Session["studPhone"] = lblMobileNo.Text;
            Session["studEmail"] = lblMailId.Text;

            Session["ReceiptType"] = ddlReceiptType.SelectedValue;
            Session["idno"] = hdfIdno.Value;
            Session["paysession"] = hdfSessioNo.Value;
            Session["paysemester"] = ddlSemester.SelectedValue;
            Session["homelink"] = "HostelFeeOnlinePayment.aspx";
            Session["regno"] = lblRegno.Text;
            Session["payStudName"] = lblStudName.Text;
            Session["paymobileno"] = lblMobileNo.Text;
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblStudBranch.Text;

            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }




        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private DailyCollectionRegister Bind_FeeCollectionData()
    {
        /// Bind transaction related data from various controls.
        DailyCollectionRegister dcr = new DailyCollectionRegister();
        try
        {

            //dcr.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue.Trim());
            dcr.SemesterNo = ((ddlSemester.SelectedIndex > 0 && ddlSemester.SelectedValue != string.Empty) ? Int32.Parse(ddlSemester.SelectedValue) : 1);
           // int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            // Above line commited by Saurabh L on 08/06/2023
            ////if (examType == 1)
            ////{
            dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
            ////}
            ////else
            ////{
            ////    dcr.SessionNo = Convert.ToInt32(Session["currentsession"].ToString()) + 1;
            ////}

            dcr.FeeHeadAmounts = this.GetFeeItems();




            DemandDrafts[] dds = null;



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


            dcr.ChallanDate = DateTime.Today;

            /// This status is used to mark/flag unpaid/not received bank chalans.
            /// Default is false. if unpaid then will be marked as true.
            dcr.IsDeleted = false;
            dcr.CompanyCode = string.Empty;
            dcr.RpEntry = string.Empty;
            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
            dcr.PrintDate = DateTime.Today;
            dcr.CollegeCode = Session["colcode"].ToString();            //this is add to excess amount maintain. date: 10/04/2012
            // check the status of configuration page

            //string chkConfig = objCommon.LookUp("ACD_CONFIG", "STATUS", "CONFIGNO=1");
            //if(chkConfig == "Y")
            //{
            //dcr.ExcessAmount = Convert.ToDouble(txtTotalAmount.Text) - Convert.ToDouble(txtTotalFeeAmount.Text);


            //*****************
            foreach (ListViewDataItem item in lvFeeItems.Items)
            {
                string fee_head = string.Empty;//***************
                fee_head = ((HiddenField)item.FindControl("hdnfld_FEE_LONGNAME")).Value;//*****************
                string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();

                if (fee_head == "LATE FEE")
                {
                    if (feeAmt != null && feeAmt != string.Empty)
                    {
                        dcr.Late_fee = Convert.ToDouble(feeAmt);

                    }
                }
            }
            //*****************

            //}
            //else
            //{
            //    dcr.ExcessAmount = 0.00;
            //    objCommon.DisplayMessage("Excess amount cannot maintain. Beacause not maintain the Uaims Configuration status", this.Page);
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
        return dcr;
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
                CheckBox chkAmount = item.FindControl("chkFee") as CheckBox;

                if (chkAmount.Checked)//*****************
                {
                    string feeHeadSrNo = chkAmount.ToolTip;
                    if (feeHeadSrNo != null && feeHeadSrNo != string.Empty)
                        feeHeadNo = Convert.ToInt32(feeHeadSrNo);
                    string feeAmt = ((HiddenField)item.FindControl("hidFeeItemAmount")).Value;
                    // string feeAmt = ((TextBox)item.FindControl("txtFeeItemAmount")).Text.Trim();
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
            throw;
        }
        return feeHeadAmts;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtEnrollmentNo.Text = string.Empty;
        ddlReceiptType.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        //div_Studentdetail.Visible = false;
        btnPayment.Visible = false;
        divMSG.Visible = false;
        lblmsg.Text = string.Empty;

        PopulateDropDown(); // Added as per BUG-ID 164110	
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelOnlinePaymentRequest.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelOnlinePaymentRequest.aspx");
        }

    }

    private void PopulateDropDown()
    {

        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0 AND RECIEPT_CODE IN('HF')", "RECIEPT_TITLE");
        ddlReceiptType.SelectedIndex = 1;
        //this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        //S.ODD_EVEN=1 added by Saurabh L on 09/08/2022 for Odd semester only
        this.objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON (D.SEMESTERNO= S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND S.ODD_EVEN=1 AND IDNO =" + Convert.ToInt32(Session["stuinfoidno"]), "S.SEMESTERNO");
    }


    private void OnlinePayment(string Order_id)
    {
        string api_key = ConfigurationManager.AppSettings["API_KEY"];
        string country = "IND";
        string currency = "INR";
        string return_url = ConfigurationManager.AppSettings["RETURN_URL"];

        try
        {
            string[] hashVarsSeq;
            string hash_string = string.Empty;

            hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
            hash_string = "";
            foreach (string hash_var in hashVarsSeq)
            {
                if (hash_var == "api_key")
                {
                    hash_string = hash_string + api_key;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "amount")
                {
                    hash_string = hash_string + Convert.ToDecimal(lblAmount.Text.Trim()).ToString("g29");
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "SALT")
                {
                    hash_string = hash_string + ConfigurationManager.AppSettings["SALT"];
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "mode")
                {
                    hash_string = hash_string + ConfigurationManager.AppSettings["MODE"];
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "country")
                {
                    hash_string = hash_string + country;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "currency")
                {
                    hash_string = hash_string + currency;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "return_url")
                {
                    hash_string = hash_string + return_url;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "city")
                {
                    hash_string = hash_string + hdfCity.Value.Trim();
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "name")
                {
                    hash_string = hash_string + hdfName.Value.Trim();
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "order_id")
                {
                    hash_string = hash_string + Order_id;
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "phone")
                {
                    hash_string = hash_string + hdfMobileNo.Value.Trim();
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "state")
                {
                    hash_string = hash_string + hdfState.Value.Trim();
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "zip_code")
                {
                    hash_string = hash_string + hdfZipCode.Value.Trim();
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "description")
                {
                    hash_string = hash_string + "Online Payment";
                    hash_string = hash_string + '|';
                }
                else if (hash_var == "email")
                {
                    hash_string = hash_string + hdfEmailId.Value.Trim();
                    hash_string = hash_string + '|';
                }
            }

            hash_string = hash_string.Substring(0, hash_string.Length - 1);
            hash = Generatehash512(hash_string).ToUpper();         //generating hash
            action = ConfigurationManager.AppSettings["TRAKNPAY_URL"];// setting URL

            if (!string.IsNullOrEmpty(hash))
            {
                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                //data.Add("address_line_1", "-");
                //data.Add("address_line_2", "-");
                string AmountForm = Convert.ToDecimal(lblAmount.Text.Trim()).ToString("g29");// eliminating trailing zeros
                data.Add("amount", AmountForm);
                data.Add("api_key", api_key);
                data.Add("city", hdfCity.Value.Trim());
                data.Add("country", country);
                data.Add("currency", currency);
                data.Add("description", "Online Payment");
                data.Add("email", hdfEmailId.Value.Trim());
                data.Add("mode", ConfigurationManager.AppSettings["MODE"]);
                data.Add("name", hdfName.Value.Trim());
                data.Add("order_id", Order_id);
                data.Add("phone", hdfMobileNo.Value.Trim());
                data.Add("return_url", return_url);
                data.Add("state", hdfState.Value.Trim());
                //data.Add("udf1", '-');
                //data.Add("udf2", '-');
                //data.Add("udf3", '-');
                //data.Add("udf4", '-');
                //data.Add("udf5", '-');
                data.Add("zip_code", hdfZipCode.Value.Trim());
                data.Add("hash", hash);

                string strForm = PreparePOSTForm(action, data);
                Page.Controls.Add(new LiteralControl(strForm));
            }
        }
        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");
        }
    }

    /// <summary>
    /// Generate HASH for encrypt all parameter passing while transaction
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Generatehash512(string text)
    {
        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;

    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.



        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        strForm.Append("<iframe id=\"ifrm\" src=\"" + url +
                           "\"></iframe>");
        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }


        strForm.Append("</form>");


        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }
    protected void btnReciept_Click(object sender, EventArgs e)
    {
        ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(Session["stuinfoidno"]);

            // Added by Saurabh L on 5th May 2023 Purpose: Online Online payment Receipt can be generated
            string PAY_TYPE = objCommon.LookUp("ACD_DCR", "PAY_TYPE", "IDNO=" + IDNO + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIEPT_CODE  ='HF' AND PAY_TYPE = 'O' AND CAN=0");

            if (PAY_TYPE != "O")
            {
                lblmsg.Attributes.Add("style", "color:red");
                lblmsg.Text = "Only Online payment Receipt can be generated.";

                return;
            }
            //----------------------- End by Saurabh L on 5th May 2023 

            // CAN=0 condition added by Saurabh L on 08/06/2023 in below query

            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(IDNO) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAY_TYPE = 'O' AND RECIEPT_CODE  ='HF' AND CAN=0"));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void FeeItemsSection()
    {
        try
        {
            int status = 0;
            /// Get fee demand of the student for given semester no.
            /// if demand found then display fee items. Use status variable to 
            /// flag the demand status. status = -99 means demand not found.
            //int studId = Int32.Parse((GetViewStateItem("StudentId") != string.Empty) ? GetViewStateItem("StudentId") : "0");

            DataSet ds = null;
            int Currency = 1;
            ds = objFee.GetFeeItems_Data_For_Student(Convert.ToInt32(hdfSessioNo.Value), Convert.ToInt32(ViewState["StudId"]), Convert.ToInt32(ddlSemester.SelectedValue), ddlReceiptType.SelectedValue, Convert.ToInt16(ViewState["pType"]), ref status);

            if (status != -99 && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {


                hdnFeeItemCount.Value = ds.Tables[0].Rows.Count.ToString();
                lvFeeItems.DataSource = ds;
                lvFeeItems.DataBind();

                string RecieptCode = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                if (RecieptCode == "TF" || RecieptCode == "EF" || RecieptCode == "HF" || RecieptCode == "BCA" || RecieptCode == "MBA" || RecieptCode == "PG" || RecieptCode == "EVF" ||
                    RecieptCode == "PGF" || RecieptCode == "BMF" || RecieptCode == "BHE" || RecieptCode == "PDF" || RecieptCode == "UNG" || RecieptCode == "OF")
                {
                    /// Show remark for current fee demand


                    /// Set FeeCatNo from datasource
                    ViewState["FeeCatNo"] = ds.Tables[0].Rows[0]["FEE_CAT_NO"].ToString();

                    /// Show total fee amount to be paid by the student in total amount textbox.
                    /// This total fee amount can be changed by user according to the student's current 

                    //lblamtpaid.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                    //double totalamt = Convert.ToDouble(this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString());
                    //if (totalamt < 0)
                    //{
                    //    txtTotalAmountShow.Text = "0.00";
                    //}
                    //else
                    //{
                    //    txtTotalAmountShow.Text = this.GetTotalFeeDemandAmount(ds.Tables[0]).ToString();
                    //}
                    //txtTotalFeeAmount.Text = txtTotalAmountShow.Text;
                }
                /// If fee items are available then Enable
                /// submit button and show the div FeeItems.

                divFeeItems.Visible = true;


            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvFeeItems_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;
            if (((TextBox)e.Item.FindControl("txtFeeItemAmount")).Text == "0.00")
            {
                ((CheckBox)e.Item.FindControl("chkFee")).Enabled = false;
            }
            else
            {
                ((CheckBox)e.Item.FindControl("chkFee")).Enabled = true;
            }


        }
    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {

        int IDNO = Convert.ToInt32(Session["stuinfoidno"]);

        this.objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON (D.SEMESTERNO= S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND IDNO =" + IDNO, "S.SEMESTERNO");

        ddlSemester.SelectedIndex = 0;
        //div_Studentdetail.Visible = false;
        btnPayment.Visible = false;
        divMSG.Visible = false;
        lblmsg.Text = string.Empty;
        divFeeItems.Visible = false;
        lvFeeItems.DataSource = null;
        lvFeeItems.DataBind();
    }
    private void DisplayInformation(int studentId)
    {
        try
        {
            #region Display Previous Receipts Information
            DataSet ds;
            /// Display student's previously paid receipt information.
            /// These are the receipts(i.e. Fee) paid by the student during 
            /// previous semesters or part payment for current semester
            ds = objFee.GetPaidReceiptsInfoByStudIdOnline(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divPreviousReceipts.Visible = true;
                // Bind list view with paid receipt data 
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
            }
            else//MAKE CHANGE HERE
            {
                divPreviousReceipts.Visible = false;
                divHidPreviousReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
            }
            //divPreviousReceipts.Visible = true;
            #endregion
        }
        catch
        {
            throw;
        }
    }
    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnPrint = sender as ImageButton;
            if (btnPrint.CommandArgument != string.Empty)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    ShowReportPrevious("OnlineFeePayment", "HostelFeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]));
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_crescent.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]));
                }
                else
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_crescent.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]));
                }

            }
        }
        catch
        {
            throw;
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

    private void ShowReportPrevious(string reportTitle, string rptFileName, int dcrNo, int studentNo)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();

            //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {

        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt";
        return param;

    }

    protected void btnPay_Click(object sender, EventArgs e)
    {

        int status1 = 0;
        int Currency = 1;
        string amount = string.Empty;
        amount = Convert.ToString(hdfAmount.Value);

        try
        {

            Button btnPayNow = sender as Button;
            int installno = (btnPayNow.CommandArgument != string.Empty ? int.Parse(btnPayNow.CommandArgument) : 0);
            double Amount = Convert.ToDouble((btnPayNow.CommandName != string.Empty ? Double.Parse(btnPayNow.CommandName) : 0));
            int demandno = (btnPayNow.ToolTip != string.Empty ? int.Parse(btnPayNow.ToolTip) : 0);
            HiddenField hdfIdno = sender as HiddenField;
            int Idno = Convert.ToInt32(Session["stuinfoidno"]);

            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = Amount;
            ViewState["studAmt"] = Amount;//hdnTotalCashAmt.Value;
            dcr.TotalAmount = Convert.ToDouble(Amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            Session["studName"] = lblStudName.Text;
            Session["studPhone"] = lblMobileNo.Text;
            Session["studEmail"] = lblMailId.Text;

            Session["ReceiptType"] = ddlReceiptType.SelectedValue;
            Session["idno"] = Idno;

            Session["paysemester"] = ddlSemester.SelectedValue;
            Session["homelink"] = "OnlinePayment.aspx";
            Session["regno"] = lblRegno.Text;
            Session["payStudName"] = lblStudName.Text;
            Session["paymobileno"] = lblMobileNo.Text;
            Session["demandno"] = demandno;
            Session["Installmentno"] = installno;
            int session = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "SESSION_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND INSTALL_NO=" + Convert.ToInt32(installno)));

            Session["paysession"] = session;
            //int uano = Convert.ToInt32(Session["userno"]);

            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnPrintReceipt_Click1(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPrintReceipt = sender as ImageButton;
        int DcrNo = (btnPrintReceipt.CommandArgument != string.Empty ? int.Parse(btnPrintReceipt.CommandArgument) : 0);

        //ShowReport("InstallmentOnlineFeePayment", "rptInstallmentOnlineReceipt.rpt", DcrNo);
        ShowReportPrevious("InstallmentOnlineFeePayment", "rptInstallmentOnlineReceipt.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));
    }

    #region Search Panel
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

        //if (lnk.CommandArgument == null)
        //{
        //    string number = Session["StudId"].ToString();
        //    Session["stuinfoidno"] = Convert.ToInt32 (number);
        //}
        //else
        //{
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        ViewState["idno"] = Session["stuinfoidno"].ToString();

        DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

        //Server.Transfer("PersonalDetails.aspx", false);
        DisplayInformation(Convert.ToInt32(Session["stuinfoidno"]));
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;


    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

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

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            divReceiptType.Visible = false;
            divStudSemester.Visible = false;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        lblNoRecords.Visible = true;
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

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        div_Studentdetail.Visible = false;
        divMSG.Visible = false;
        btnPayment.Visible = false;
        btnReciept.Visible = false;
        divPreviousReceipts.Visible = false;
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
    }

    #endregion
}