
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
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
public partial class ACADEMIC_OnlinePayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFee = new FeeCollectionController();
    DailyCollectionRegister dcr = new DailyCollectionRegister();

    public string action = string.Empty;
    public string hash = string.Empty;
    int degreeno = 0;
    int college_id = 0;
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
                    this.CheckPageAuthorization();
                }

                // Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                Session["payactivityno"] = "1";
                int IDNO1 = 0;
                if (Session["usertype"].ToString().Equals("2") || Session["usertype"].ToString().Equals("14"))
                {

                    //divEnrollment.Visible = false;
                    divReceiptType.Attributes.Add("class", "form-group col-lg-3 col-md-6 col-12");
                    divStudSemester.Attributes.Add("class", "form-group col-lg-3 col-md-6 col-12");
                    IDNO1 = Convert.ToInt32(Session["idno"]);
                    Session["stuinfoidno"] = IDNO1;
                    myModal2.Visible = false;
                    DisplayInformation(IDNO1);
                    DisplayStudentInfo(IDNO1);
                    PopulateDropDown();
                    //divamount.Visible = false;
                }
                else
                {
                    myModal2.Visible = true;
                    btnCancel.Visible = false;

                    //Search Pannel Dropdown Added by Swapnil
                    //PopulateDropDownList();


                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                    ddlSearch.SelectedIndex = 1;
                    ddlSearch_SelectedIndexChanged(sender, e);

                    // search();
                    //pnltextbox.Visible = true;
                    //divpanel.Visible = true;
                    //pnltextbox.Visible = true;
                    //ddlSearch_SelectedIndexChanged(sender, e);
                    //End Search Pannel Dropdowns

                }



            }
        }



        if (Request.Params["__EVENTTARGET"] != null &&
                               Request.Params["__EVENTTARGET"].ToString() != string.Empty)
        {
            if (Request.Params["__EVENTTARGET"].ToString() == "ModifyDemand")
            {
                this.ModifyDemand();

            }
        }
        //else
        //{
        //    int count = 0;
        //    if (Page.Request.Params["__EVENTTARGET"] != null)
        //    {
        //        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
        //        {
        //            string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
        //            bindlist(arg[0], arg[1]);
        //        }

        //        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
        //        {

        //            //lvRegStatus.DataSource = null;
        //            //lvRegStatus.DataBind();
        //            //lvFees.DataSource = null;
        //            //lvFees.DataBind();
        //            //lvCertificate.DataSource = null;
        //            //lvCertificate.DataBind();
        //            //lblMsg.Text = string.Empty;

        //        }
        //        //if (Convert.ToInt32(ViewState["count"]) == 0)
        //        //{
        //        //    int id = 0;
        //        //    if (ViewState["usertype"].ToString() == "2")
        //        //    {
        //        //        id = Convert.ToInt32(Session["userno"].ToString());
        //        //        this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + id, "R.SESSIONNO DESC");
        //        //        ddlSession.SelectedIndex = 1;
        //        //        count++;
        //        //        ViewState["count"] = count;
        //        //    }
        //        //    //else  if (!txtEnrollmentSearch.Text.Trim().Equals(string.Empty))
        //        //    //{
        //        //    //    id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO= " + "'" + txtEnrollmentSearch.Text.Trim() + "'"));
        //        //    //}

        //        //}
        //    }
        //}

        // lbltotals.Visible = false;
        // divamount.Visible = false;
    }

    protected void ddlSemester_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlSemester.SelectedIndex > 0)
        {
            btnPayment.Visible = false;
            divMSG.Visible = false;
            int HostelTypeSelection = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(HOSTE_TYPE_ONLINE_PAY,0) as HOSTE_TYPE_ONLINE_PAY", ""));

            if (HostelTypeSelection == 1)
            {
                divHostelTransport.Visible = true;
            }
            else
            {
                divHostelTransport.Visible = false;
            }
            //div_Studentdetail.Visible = false;
            DataSet ds = null;
            int IDNO = Convert.ToInt32(Session["stuinfoidno"]);
            ViewState["StudId"] = IDNO;
            int exam_type = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(EXAM_PTYPE,0)EXAM_PTYPE", "IDNO=" + IDNO));
            ViewState["Exam_Type"] = exam_type;
            int ptype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(PTYPE,0)PTYPE", "IDNO=" + IDNO));
            int Hosteltype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(HOSTEL_STATUS,0)HOSTEL_STATUS", "IDNO=" + IDNO));
            ViewState["pType"] = ptype;

            if (ddlSemester.SelectedIndex > 0)
            {
                int count = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "count(*)", "IDNO =" + IDNO + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND RECIPTCODE = '" + Convert.ToString(ddlReceiptType.SelectedValue) + "'" + "AND ISNULL(INSTAL_CANCEL,0)=0"));

                if (count > 0)
                {

                    divHostelTransport.Visible = false;


                    divInstallmentPayment.Visible = true;
                    divDirectPayment.Visible = false;
                    ds = objFee.GetStudentInstallmentDetails(IDNO, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                        lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
                        lblCollegeName.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                        lblDegreeName.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        lblBranchName.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                        lblMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        lblTotalAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                        lblEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        lblTotalInstallment.Text = ds.Tables[0].Rows[0]["TOTAL_INSTALMENT"].ToString();

                        lvInstallment.DataSource = ds;
                        lvInstallment.DataBind();
                    }
                }
                else
                {


                    if (Hosteltype == 0)
                    {
                        if (HostelTypeSelection == 1)
                        {
                            divHostelTransport.Visible = true;
                        }
                        else
                        {
                            divHostelTransport.Visible = false;
                        }

                    }
                    else
                    {
                        divHostelTransport.Visible = false;

                        if (HostelTypeSelection == 1)
                        {
                            divhosteltype.Visible = true;
                            this.objCommon.FillDropDownList(ddlhosteltype, "ACD_HOSTEL_TYPE", "DISTINCT HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO >0 AND ACTIVESTATUS=1", "HOSTEL_TYPE_NO");
                            ddlhosteltype.SelectedValue = Hosteltype.ToString();
                            ddlhosteltype.Enabled = false;
                        }
                    }
                    divInstallmentPayment.Visible = false;
                    divDirectPayment.Visible = true;

                    int checkprevsemoutstanding = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(CHECK_PREV_SEM_OUTSNADING,0)", "ConfigNo>0"));

                    if (checkprevsemoutstanding == 1)
                    {
                        // DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 AND PAY_MODE_CODE<>'SA' AND ISNULL(SCH_ADJ_AMT,0)=0 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'TF' THEN 'Admission Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);
                        DataSet dsDueFee = objCommon.FillDropDown("ACD_DEMAND D  LEFT OUTER JOIN (SELECT IDNO,SEMESTERNO, SUM(ISNULL(DR.TOTAL_AMT,0)) DRTOTAL_AMT FROM  ACD_DCR DR WHERE IDNO=" + IDNO + "  AND RECON=1 GROUP BY IDNO,SEMESTERNO )A  ON (A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO)", "DISTINCT D.IDNO,D.SEMESTERNO,DBO.FN_DESC('SEMESTER',D.SEMESTERNO)SEMESTERNAME", "ISNULL(DRTOTAL_AMT,0)DRTOTAL_AMT,(ISNULL(D.TOTAL_AMT,0)) DTOTAL_AMT, (CASE WHEN D.RECIEPT_CODE = 'TF' THEN 'Admission Fees' ELSE 'Other Fees' END) FEE_TITLE", " D.IDNO = " + IDNO + " AND D.SEMESTERNO<" + ddlSemester.SelectedValue + " AND ISNULL(CAN,0)=0 AND ISNULL(DELET,0)=0", string.Empty);
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
                    }
                    SemesterWiseFees();
                }
            }
            else
            {
                divInstallmentPayment.Visible = false;
                divDirectPayment.Visible = true;
                btnCancel.Visible = true;
                divHostelTransport.Visible = false;
            }

            
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

        bindfeesdetails();
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

                //FeeItemsSection();   // Commented by swapnil
            }
            else
            {
                // objCommon.DisplayMessage("Fees already paid for selected semester.", this.Page);
                divMSG.Visible = true;
                btnReciept.Visible = false;
                div_Studentdetail.Visible = true;
                lblmsg.Attributes.Add("style", "color:green");
                lblmsg.Text = "Fees already paid for selected semester.";
                if (Session["OrgId"].ToString() == "5")
                {
                    int HostelTypeSelection = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(HOSTE_TYPE_ONLINE_PAY,0) as HOSTE_TYPE_ONLINE_PAY", ""));

                    if (HostelTypeSelection == 1)
                    {
                        divHostelTransport.Visible = true;
                        int currentsem = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"].ToString())));
                        if (currentsem == Convert.ToInt32(ddlSemester.SelectedValue))
                        {
                            divHostelTransport.Visible = true;
                        }
                        else
                        {
                            divHostelTransport.Visible = false;
                        }
                    }
                    else
                    {
                        divHostelTransport.Visible = false;
                    }
                    return;
                }
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

        int status1 = 0;
        int Currency = 1;
        string amount = string.Empty;
        amount = Convert.ToString(hdfAmount.Value);
        //amount = Convert.ToString("1");
        try
        {
            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = amount;
            ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
            dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            Session["studName"] = lblStudName.Text;
            Session["studPhone"] = lblMobileNo.Text;
            Session["studEmail"] = lblMailId.Text;

            Session["ReceiptType"] = ddlReceiptType.SelectedValue;
            Session["idno"] = hdfIdno.Value;
            Session["paysession"] = hdfSessioNo.Value;
            Session["paysemester"] = ddlSemester.SelectedValue;
            Session["homelink"] = "OnlinePayment.aspx";
            Session["regno"] = lblRegno.Text;
            Session["payStudName"] = lblStudName.Text;
            Session["paymobileno"] = lblMobileNo.Text;
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblStudBranch.Text;
            //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.

            if (Session["OrgId"].ToString() == "6")
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                if (ddlReceiptType.SelectedValue == "AEF" || ddlReceiptType.SelectedValue == "EF")
                {
                    int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME = 'Exam Registration'"));

                    Session["payactivityno"] = payactivityno;
                }
            }
            else if (Session["OrgId"].ToString() == "16")
            {
                if (ddlReceiptType.SelectedValue == "EF")
                {
                    // college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='EXAM REG'"));
                    Session["payactivityno"] = activityno;
                }
                else if (ddlReceiptType.SelectedValue == "AEF")
                {
                    //college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='EXAM REG'"));
                    Session["payactivityno"] = activityno;
                }
                else
                {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));
                    Session["payactivityno"] = activityno;
                }

            }
            else if (Session["OrgId"].ToString() == "15")
            {
                int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));
                Session["payactivityno"] = activityno;
            }
            else
            {
                Session["payactivityno"] = 1;
            }

            if (Session["OrgId"].ToString() == "8")
            {
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }
            //**********************************End by Nikhil L.********************************************//


            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Session["AccessCode"] = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
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
            int examType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
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
        divHostelTransport.Visible = false;
        divhosteltype.Visible = false;
        ddlReceiptType.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        //div_Studentdetail.Visible = false;
        btnPayment.Visible = false;
        divMSG.Visible = false;
        lblmsg.Text = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePaymentRequest.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePaymentRequest.aspx");
        }

    }

    private void PopulateDropDown()
    {

        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE RT INNER JOIN ACD_DEMAND D ON(RT.RECIEPT_CODE = D.RECIEPT_CODE)", "DISTINCT RT.RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0 AND D.IDNO =" + Convert.ToInt32(Session["stuinfoidno"]), "RECIEPT_TITLE");
        ddlReceiptType.SelectedIndex = 1;
        //this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        this.objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON (D.SEMESTERNO= S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND IDNO =" + Convert.ToInt32(Session["stuinfoidno"]), "S.SEMESTERNO");
    }

    //private void PopulateDropDownList()
    //    {
    //    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
    //    ddlSearch.SelectedIndex = 1;
    //    ddlSearch_SelectedIndexChanged(sender, e);
    //    }


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
            ;
            //if (divEnrollment.Visible == false)
            //{
            //    IDNO = Convert.ToInt32(Session["idno"] == string.Empty ? "0" : Session["idno"].ToString());
            //}
            //else
            //{
            //    IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollmentNo.Text + "'") == "" ? "0" : objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtEnrollmentNo.Text + "'"));

            //}
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(IDNO) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND PAY_TYPE = 'O' and PAY_SERVICE_TYPE = 1"));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

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
            ds = objFee.GetAllFeesDetailsStudIdOnline(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divpaymentdetails.Visible = true;
                // Bind list view with paid receipt data 
                lvPaymentDetails.DataSource = ds;
                lvPaymentDetails.DataBind();

            }
            else//MAKE CHANGE HERE
            {
                divPreviousReceipts.Visible = false;
                divpaymentdetails.Visible = false;
                divHidPreviousReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
            }


            ds = objFee.GetPaidReceiptsInfoByStudIdOnline(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divPreviousReceipts.Visible = true;

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

            int DcrNo = (btnPrint.CommandArgument != string.Empty ? int.Parse(btnPrint.CommandArgument) : 0);
            string ptype = (objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_PAYMENTTYPE P ON (A.PTYPE=P.PAYTYPENO) ", "PAYTYPENAME", "IDNO=" + Session["idno"].ToString()));
            int ReportFlag = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(DISPLAY_HTML_REPORT,0) AS DISPLAY_HTML_REPORT", "OrganizationId=" + Session["OrgId"].ToString()));


            //if (btnPrint.ToolTip == "True")
            //    {
            //    Session["CANCEL_REC"] = 1;
            //    }
            //else if (btnPrint.ToolTip == "False")
            //    {
            //    Session["CANCEL_REC"] = 0;
            //    }
            //else
            //{
            Session["CANCEL_REC"] = 0;
            // }

            if (ptype == "Provisional" && Session["OrgId"].ToString() == "5")
            {
                //ShowReport("InstallmentOnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));

                ShowReportProvisional("OnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", DcrNo);
                return;
            }

            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (btnPrint.CommandArgument != string.Empty)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    if (ReportFlag == 1)
                    {
                        // Below Code added by Rohit M. on dated 26.06.2023 
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/Academic/Fees/RcpitReceipt.html?";
                        url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(Session["stuinfoidno"]) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                        //  // Above Code added by Rohit M. on dated 26.06.2023 
                    }
                    else
                    {
                        ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    if (ReportFlag == 1)
                    {
                        // Below Code added by Rohit M. on dated 26.06.2023 
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/Academic/Fees/RCPIPERReceipt.html?";
                        url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(Session["stuinfoidno"]) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                        // Above Code added by Rohit M. on dated 26.06.2023 
                    }
                    else
                    {
                        ShowReportPrevious_RCPIPER("OnlineFeePayment", "FeeCollectionReceiptForCash_RCPIPER.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_crescent.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    ShowReportPrevious_MIT("OnlineFeePayment", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
                else if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4"))
                {
                    if (ReportFlag == 1)
                    {
                        // Below Code added by Rohit M. on dated 26.06.2023 
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/Academic/Fees/CpuKota.html?";
                        url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(Session["stuinfoidno"]) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                        // Above Code added by Rohit M. on dated 26.06.2023 
                    }
                    else
                    {

                        this.ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_cpukota.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }


                }
                else if (Session["OrgId"].ToString().Equals("5"))
                {

                    if (ReportFlag == 1)
                    {
                        //// Below Code added by Rohit M. on dated 29.05.2023 
                        //string url = Request.Url.ToString();
                        //url = Request.ApplicationPath + "/Reports/Academic/Fees/FeeReceipt.html";
                        //// Response.Redirect(url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["username"].ToString() +"&Idno=" + Int32.Parse(GetViewStateItem("StudentId")) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]));
                        //string urlForReceipt = string.Empty;
                        //urlForReceipt = url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(Session["stuinfoidno"]) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + urlForReceipt + "');", true);


                        // // Below Code added by ROHIT M. on dated 01.06.2023 
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/Academic/Fees/FeeReceipt.html?";
                        url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(Session["stuinfoidno"]) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                        //  // Above Code added by ROHIT M. on dated 01.06.2023
                    }
                    else
                    {
                        ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_JECRC.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                }
                else if (Session["OrgId"].ToString().Equals("19"))  //PCEN RECIPT ADDED ON 23_11_2023 DATED ON 50439
                {
                    this.ShowReport_ForCash_PCEN("FeeCollectionReceiptForCash_PCEN.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), "1", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                }
                else
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));

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

    private void ShowInstllmentReportPrevious(string reportTitle, string rptFileName, int dcrNo, int studentNo)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetInstallmnentReportParameters(dcrNo, studentNo, "2");

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

    private void ShowReportPrevious_MIT(string reportTitle, string rptFileName, int dcrNo, int studentNo, string Username)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString() + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + studentNo + ",@P_DCRNO=" + dcrNo + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();

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

    private void ShowReport_MIT(string reportTitle, string rptFileName, string UANAME)
    {
        try
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["IDNO"] + ",@P_DCRNO=" + Session["DCRNO"] + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportPrevious_RCPIPER(string reportTitle, string rptFileName, int dcrNo, int studentNo, int Cancel)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString() + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + "username=" + Session["username"].ToString()+","+this.GetReportParameters(dcrNo, studentNo, "1");

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, "2")+ ",username=" + Session["username"].ToString();

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

    private void ShowReportPrevious(string reportTitle, string rptFileName, int dcrNo, int studentNo, string Username, int Cancel)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString() + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();


            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();


            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["UAFULLNAME"].ToString() +
            "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, "2");

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
    private string GetInstallmnentReportParameters(int dcrNo, int studentNo, string copyNo)
    {

        string param = "@P_DCRNO=" + dcrNo.ToString() + ",@P_IDNO=" + studentNo.ToString();
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
            //double Amount = Convert.ToDouble((btnPayNow.CommandName != string.Empty ? Double.Parse(btnPayNow.CommandName) : 0));
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
            Session["Branchname"] = lblStudBranch.Text;
            //Session["YearNo"]=
            int session = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "SESSION_NO", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND INSTALL_NO=" + Convert.ToInt32(installno)));

            Session["paysession"] = session;
            //int uano = Convert.ToInt32(Session["userno"]);
            if (Session["OrgId"].ToString() == "6")
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                if (ddlReceiptType.SelectedValue == "AEF" || ddlReceiptType.SelectedValue == "EF")
                {
                    int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME = 'Exam Registration'"));

                    Session["payactivityno"] = payactivityno;
                }
                else
                {
                    Session["payactivityno"] = 1;
                }
            }
            if (Session["OrgId"].ToString() == "8")
            {
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }
            else if (Session["OrgId"].ToString() == "16")
            {
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));
                Session["payactivityno"] = activityno;
            }
            else if (Session["OrgId"].ToString() == "15")
            {
                int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));
                Session["payactivityno"] = activityno;
            }
            else
            {
                Session["payactivityno"] = 1;
            }

            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Session["AccessCode"] = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    Response.Redirect(RequestUrl);
                    //Response.Redirect("http://localhost:55403/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/PayUOnlinePaymentRequest.aspx");

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


        Session["DCR_NOS"] = Convert.ToInt32(DcrNo);
        string ptype = (objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_PAYMENTTYPE P ON (A.PTYPE=P.PAYTYPENO) ", "PAYTYPENAME", "IDNO=" + Session["idno"].ToString()));
        if (ptype == "Provisional" && Session["OrgId"].ToString() == "5")
        {
            //ShowReport("InstallmentOnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));

            ShowReportProvisional("OnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", DcrNo);
        }
        else
        {
            //ShowReport("InstallmentOnlineFeePayment", "rptInstallmentOnlineReceipt.rpt", DcrNo);
            ShowInstllmentReportPrevious("InstallmentOnlineFeePayment", "rptInstallmentOnlineReceipt.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));
        }

    }

    private void ShowReportProvisional(string reportTitle, string rptFileName, int DCRNO)
    {
        try
        {
            int IDNO = Convert.ToInt32(Session["idno"].ToString());

            // string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + Session["idno"].ToString() + "'");


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DCRNO);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
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
        divDirectPayment.Visible = true;
        div_Studentdetail.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        lvfeehead.Visible = false;


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


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
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
        lvPaymentDetails.Visible = false;
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
        div_Studentdetail.Visible = false;
        divInstallmentPayment.Visible = false;
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

    //public void search()
    //    {
    //    try
    //        {
    //       Panel3.Visible = false;
    //        lblNoRecords.Visible = false;
    //        lvStudent.DataSource = null;
    //        lvStudent.DataBind();
    //        if (ddlSearch.SelectedIndex > 0)
    //        {
    //            DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
    //                string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
    //                string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
    //                string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
    //                if (ddltype == "ddl")
    //                {
    //                    pnltextbox.Visible = false;
    //                    txtSearch.Visible = false;
    //                    pnlDropdown.Visible = true;

    //                    divtxt.Visible = false;
    //                    lblDropdown.Text = ddlSearch.SelectedItem.Text;


    //                    objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

    //                }
    //                else
    //                {
    //                    pnltextbox.Visible = true;
    //                    divtxt.Visible = true;
    //                    txtSearch.Visible = true;
    //                    pnlDropdown.Visible = false;

    //                }
    //            }
    //        }
    //        else
    //        {

    //            pnltextbox.Visible = false;
    //            pnlDropdown.Visible = false;

    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //        }

    protected void btnHostel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
        //divhottrsansport.Visible = true;
        divhosteltype.Visible = true;
        this.objCommon.FillDropDownList(ddlhosteltype, "ACD_HOSTEL_TYPE", "DISTINCT HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO >0 AND ACTIVESTATUS=1", "HOSTEL_TYPE_NO");

    }
    protected void rdbhostel_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rblhottransport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblhottransport.SelectedValue == "1")
        {
            divhosteltype.Visible = true;
            //divHostelTransport.Visible = false;
            this.objCommon.FillDropDownList(ddlhosteltype, "ACD_HOSTEL_TYPE", "DISTINCT HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO >0 AND ACTIVESTATUS=1", "HOSTEL_TYPE_NO");
        }
        else
        {
            divhosteltype.Visible = false;
            //divHostelTransport.Visible = false;
        }

    }
    protected void ddlhosteltype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlhosteltype.SelectedValue != "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmhostel();", true);
        }

        //objCommon.DisplayMessage(this, "your Demand Has been Modify Succesfully", this.Page);
    }

    public void ModifyDemand()
    {
        StudentController objsc = new StudentController();
        int IDNO = Convert.ToInt32(Session["stuinfoidno"]);
        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        string Receipt_code = ddlReceiptType.SelectedValue.ToString();
        int UA_NO = Convert.ToInt32(Session["userno"]);
        string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

        CustomStatus cs = (CustomStatus)objsc.UpdateHostelStatusOnlinePay(IDNO, semester, Receipt_code, Convert.ToInt32(ddlhosteltype.SelectedValue), IPADDRESS, UA_NO);
        if (cs == CustomStatus.RecordSaved)
        {
            SemesterWiseFees();
            //lblTotalAmount.Text = (objCommon.LookUp("ACD_DEMAND", "TOTAL_AMT", "IDNO=" + IDNO + "AND SEMESTERNO=" + semester + "AND RECIEPT_CODE='" + Receipt_code + "'"));
            objCommon.DisplayMessage(this, "Demand Modified Successfully!!", this.Page);

            //lblAmount.Text = (objCommon.LookUp("ACD_DEMAND", "TOTAL_AMT", "IDNO=" + IDNO + "AND SEMESTERNO=" + semester + "AND RECIEPT_CODE='" + Receipt_code + "'"));
            int Hosteltype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(HOSTEL_STATUS,0)HOSTEL_STATUS", "IDNO=" + IDNO));
            divHostelTransport.Visible = false;
            divhosteltype.Visible = true;
            this.objCommon.FillDropDownList(ddlhosteltype, "ACD_HOSTEL_TYPE", "DISTINCT HOSTEL_TYPE_NO", "HOSTEL_TYPE_NAME", "HOSTEL_TYPE_NO >0 AND ACTIVESTATUS=1", "HOSTEL_TYPE_NO");
            ddlhosteltype.SelectedValue = Hosteltype.ToString();
            ddlhosteltype.Enabled = false;
            return;
        }
        else if (cs == CustomStatus.RecordNotFound)
        {
            objCommon.DisplayMessage(this, "Standard Fees is not Defined for Hostel Types please Contact to Admin!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this, "Server Error", this.Page);
        }
    }

    #endregion

    public DataSet GetFeeItems_Data(int studentId, int semesterNo, string receiptType)
    {
        DataSet ds = null;
        int status = 0;
        try
        {
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                {
                   
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RECEIPT_CODE", receiptType),
                     // ADDED BY SHAILENDRA K. ON DATED 29.04.2023 AS PER DR. MANOJ SIR SUGGESTION CALCULATING LATE FINE ON TRANS DATE.
                    new SqlParameter("@P_OUT", status)
                };
            sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
            ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT_ONLINEPAYMENT", sqlParams);
            //ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_FEE_ITEMS_AMOUNT_SRK_29042023", sqlParams); // ADDED BY SHAILENDRA K. ON DATED 29.04.2023 AS PER DR. MANOJ SIR SUGGESTION CALCULATING LATE FINE ON TRANS DATE.
        }
        catch (Exception ex)
        {
            throw new IITMSException("Academic_FeeCollection.GetFeeItems_Data() --> " + ex.Message + " " + ex.StackTrace);
        }
        return ds;
    }

    private void ShowReport_ForCash_PCEN(string rptName, int dcrNo, int studentNo, string copyNo, string UA_FULLNAME, int Cancel)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + studentNo + ",@P_DCRNO=" + dcrNo + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]);



            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["UAFULLNAME"].ToString() +
            //"," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(Session["IDNO"].ToString(), studentNo, "0");
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    public void bindfeesdetails()
    {
        try
        {
            DataSet ds = GetFeeItems_Data(Convert.ToInt32(ViewState["StudId"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToString(ddlReceiptType.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                lvfeehead.DataSource = ds;
                lvfeehead.DataBind();
                lvfeehead.Visible = true;
            }
            else
            {
                lvFeeItems.DataSource = null;
                lvFeeItems.DataBind();
                lvfeehead.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            
        }
    
    
    }
}