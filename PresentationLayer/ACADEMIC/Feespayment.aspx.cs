using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using SFA;
//using paytm;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Configuration;
using System.Web;
using System.Drawing;

using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;
using DotNetIntegrationKit;

public partial class ACADEMIC_Feespayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExam = new ExamController();
    FeeCollectionController ObjNuc = new FeeCollectionController();
    StudentFees objStudentFees = new StudentFees();
    ActivityController objActController = new ActivityController();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;

    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (((Session["IDNO"]) == null))
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {

                //GetStudentDeatlsForActivity();
                //if (CheckActivity() == false)
                //{
                //    fees.Visible = false;
                //    return;
                //}
                //else
                //{
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    getDetails();
                    Session["payactivityno"] = "1";
                    //// this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO IN (1,2,3)", "RECIEPT_TITLE")                    
                    //lblSession.ToolTip = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SESSION_NO", "STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'");

                    lblSession.ToolTip = "50";
                    this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE ART INNER JOIN ACD_DEMAND D ON ART.RECIEPT_CODE=D.RECIEPT_CODE", "ART.RECIEPT_CODE", "ART.RECIEPT_TITLE,ART.RCPTTYPENO", "ISNULL(D.CAN,0)=0 AND SESSIONNO=" + lblSession.ToolTip + " AND IDNO=" + Convert.ToInt32(Session["IDNO"]), "ART.RCPTTYPENO"); // ADDED BY NARESH BEERLA ON 01032021 FOR RECEIPTNO >8 CONDITION AS PER REQUIREMENT BY ANKUSH SIR
                    lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_PNAME", "SESSIONNO=" + lblSession.ToolTip);

                    objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO DESC");

                    ddlsession.Focus();              
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void GetStudentDeatlsForActivity()
    {
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                CheckActivity();
            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_StudentCondonationFees.GetStudentDeatlsForEligibilty --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void getDetails()
    {
        DataSet ds = objExam.GetStudentDetail(Convert.ToInt32(Session["IDNO"]));

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {


                lblname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
             //   lblapp.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblapp.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();   
                lblbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblmobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();

                //// For PhD Students sem
                //if (Convert.ToInt16(ds.Tables[0].Rows[0]["DEGREENO"].ToString()) == 3)   //  DEGREE = PhD
                //{
                //    int evenodd = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "FLOCK=0 AND EXAMTYPE=1"));
                //    if (evenodd == 1)
                //    {
                //        ddlsem.Items.Clear();
                //        ddlsem.Items.Add("AUTUMN SEMESTER");
                //        ddlsem.SelectedItem.Value = "5";//3
                //        lblSem.Text = "AUTUMN SEMESTER";
                //        lblSem.ToolTip = "5";
                //    }
                //    else
                //    {
                //        ddlsem.Items.Clear();
                //        ddlsem.Items.Add("SPRING SEMESTER");
                //        ddlsem.SelectedItem.Value = "6";//4
                //        lblSem.Text = "SPRING SEMESTER";
                //        lblSem.ToolTip = "6";
                //    }

                //}
                //else
                //{
                lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblSem.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                objCommon.FillDropDownList(ddlsem, "ACD_SEMESTER SM INNER JOIN ACD_STUDENT S ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SM.SEMESTERNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]), "S.SEMESTERNO");
                //}
            }
        }
    }

    private void BindInstallmentDetailsInListview()
    {
        try
        {
            string receiptType = ddlReceiptType.SelectedValue;
            int idno = Convert.ToInt32(Session["IDNO"]);
            int session = Convert.ToInt32(ddlsession.SelectedValue);
            DataSet ds = ObjNuc.GetFeesPaymentData(idno, receiptType, session);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvFeesPayment.DataSource = ds;
                lvFeesPayment.DataBind();
                //coment by pankaj 19032020
                foreach (ListViewDataItem dataitem in lvFeesPayment.Items)
                {

                    Button btnOnlinePay = dataitem.FindControl("btnOnlinePay") as Button;

                    Button btnChallan = dataitem.FindControl("btnChallan") as Button;

                    HiddenField hfrecon = dataitem.FindControl("hfrecon") as HiddenField;

                    HiddenField hdftransactionmode = dataitem.FindControl("hdftransactionmode") as HiddenField;

                    if (Convert.ToInt32(hfrecon.Value) == 1)
                    {
                        btnOnlinePay.Enabled = false;
                        //btnChallan.Enabled = false;

                        //btnOnlinePay.BackColor = Color.DarkOrange;
                        //btnOnlinePay.BorderColor = Color.DarkOrange;

                       // btnChallan.BackColor = Color.DarkOrange;
                       // btnChallan.BorderColor = Color.DarkOrange;
                    }
                    else
                    {
                        btnOnlinePay.Enabled = true;
                       // btnChallan.Enabled = true;
                    }


                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_SessionActivityDefinition.LoadDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private bool CheckActivity()
    {
        try
        {
            bool ret = true;
            string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (sessionno != "")
            {
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));
                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage(pnlTransferCredit, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                        ret = false;
                    }

                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage(pnlTransferCredit, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        ret = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(pnlTransferCredit, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    ret = false;
                }
                dtr.Close();
                return ret;
            }
            else
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
                return ret;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_PhotoCopyRegistration.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                //Response.Redirect("~/notauthorized.aspx?page=~/Feespayment.aspx");
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            //Response.Redirect("~/notauthorized.aspx?page=~/Feespayment.aspx");
            Response.Redirect("~/Default.aspx");
        }
    }


    private void GetSessionValues()
    {
        Session["FirstName"] = lblname.Text;
        Session["RegNo"] = lblapp.Text;
        Session["MobileNo"] = lblmobile.Text;
        Session["EMAILID"] = lblEmail.Text;
        Session["OrderID"] = lblOrderID.Text;
        Session["TOTAL_AMT"] = lbltotalfees.Text;
    }

    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        lblOrderID.Text = Convert.ToString(Convert.ToString(Session["USERNO"]) + Convert.ToString(ir));
    }


    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
    protected void ddlsem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindInstallmentDetailsInListview();       
        if (ddlReceiptType.SelectedIndex == 0)
        {
            lblRecpttype.Visible = false;
            lblRecpttypeAmount.Visible = false;
            Label4.Visible = false;
            lvFeesPayment.Visible = false;
            objCommon.DisplayMessage("Please Select the Fees Type", this.Page);
            ddlReceiptType.Focus();
            return;
        }
        else
        {
            lblRecpttype.Text = ddlReceiptType.SelectedItem.Text;
            lblRecpttype.Visible = true;
            lblRecpttypeAmount.Visible = true;
            Label4.Visible = true;
            lvFeesPayment.Visible = true;
        }
        // int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(ddlsem.SelectedValue) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0"));

        //CHECKS WHETHER TOTAL AMOUNT IS PAID OR NOT FROM DEMAND 
        //if (ddlReceiptType.SelectedValue == "TF" || ddlReceiptType.SelectedValue == "HF" || ddlReceiptType.SelectedValue == "TPF")
        if (ddlReceiptType.SelectedIndex > 0)
        {
            string receipt_type = ddlReceiptType.SelectedValue;
            // int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + receipt_type + "'"));
            // int recept = Convert.ToInt32(ddlReceiptType.SelectedValue);

            Label4.Visible = true;
            string receiptname = ddlReceiptType.SelectedItem.Text;
            lblRecpttype.Text = receiptname;
            double DemandAmount = Convert.ToDouble(objCommon.LookUp("ACD_DEMAND d inner join ACD_RECIEPT_TYPE rt on rt.reciept_code=d.reciept_code", "d.TOTAL_AMT", "ISNULL(d.CAN,0)=0 AND IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND sessionno =" + Convert.ToInt32(ddlsession.SelectedValue) + " and rt.RECIEPT_CODE ='" + receipt_type + "'"));
            lblRecpttypeAmount.Text = DemandAmount.ToString();
        }
        //ENDS HERE

        int demand_sem = 0;
        try
        {
            demand_sem = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DISTINCT SEMESTERNO", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + ddlsession.SelectedValue + " AND RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND ISNULL(CAN,0)=0 "));
        }
        catch { }
        if (demand_sem == 0)
        {
            objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
            return;
        }

        int demand_count = 0;
        try
        {
            demand_count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "ISNULL(Count(*),0)", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + ddlsession.SelectedValue + " AND RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND ISNULL(CAN,0)=0 AND SEMESTERNO=" + Convert.ToInt32(demand_sem) + " "));
        }
        catch { }
        if (demand_count == 0)
        {
            objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
            return;
        }


        //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(lblSem.ToolTip) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0"));



        int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(ddlsession.SelectedValue) + " AND SEMESTERNO =" + Convert.ToInt32(demand_sem) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0 AND (SELECT SUM(TOTAL_AMT) FROM ACD_DCR WHERE IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(ddlsession.SelectedValue) + " AND SEMESTERNO =" + Convert.ToInt32(demand_sem) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0)=" + lblRecpttypeAmount.Text));
        //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(lblSem.ToolTip) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0 AND (SELECT SUM(TOTAL_AMT) FROM ACD_DCR WHERE IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(lblSem.ToolTip) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0)=" + lblRecpttypeAmount.Text));
        if (ifPaidAlready > 0)
        {
            objCommon.DisplayMessage("Fee has been paid already. Can't proceed with the transaction !", this.Page);
            lblMsg.Text = "Fee has been paid already. Can't proceed with the transaction !";
            btnPAY.Visible = false;
            btnReports.Visible = true;
            return;
        }
        else
        {
            btnReports.Visible = false;
            lblMsg.Text = string.Empty;
        }

        //gettotal amount
        //check this is installment person or not
        int IsInstallment = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSION_NO =" + Convert.ToInt32(ddlsession.SelectedValue)));


        // for adding late fee in demand
        double LateFee = 0;//comment by pankaj nakhale 11 march 2020    

        lblLateFee.Text = LateFee.ToString("0.00");
        btnPAY.Visible = true;
        btnReports.Visible = false;

        ////////////////////////////////// add for giving installment details on based on  receipt type code by pankaj nakhale 13 march 2020 ///////////////////////
    }


    //ADDED ON [17 march 2020]GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY. br pankaj nakhale
    private void ShowReport_NEW(string reportTitle, string rptFileName, int OrderId)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_ORDER_ID=" + OrderId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void PostOnlinePayment(double Amount, int orderid)
    {
        #region Declarations
        string feeAmount = string.Empty;
        string Transacionid = "NA";
        string TransactionFor = string.Empty;
        string TSPLTxnCode = string.Empty;
        string TSPLtxtITC = string.Empty;
        #endregion

        #region Get Payment Details
        feeAmount = Amount.ToString();// (ViewState["Final_Amt"]).ToString();"1"; //
        #endregion

        #region Payment Log for Different Transaction Id
        string TransactionCode = string.Empty;
        TransactionCode = orderid.ToString(); //lblOrderID.Text; // This may be configured from Database for Different Running Number
        #endregion

        #region BillDesk Data Declaration
        string MerchantID = string.Empty;
        string UniTranNo = string.Empty;
        string NA1 = string.Empty;
        string txn_amount = string.Empty;
        string NA2 = string.Empty;
        string NA3 = string.Empty;
        string NA4 = string.Empty;
        string CurrencyType = string.Empty;
        string NA5 = string.Empty;
        string TypeField1 = string.Empty;
        string SecurityID = string.Empty;
        string NA6 = string.Empty;
        string NA7 = string.Empty;
        string TypeField2 = string.Empty;
        string additional_info1 = string.Empty;
        string additional_info2 = string.Empty;
        string additional_info3 = string.Empty;
        string additional_info4 = string.Empty;
        string additional_info5 = string.Empty;
        string additional_info6 = string.Empty;
        string additional_info7 = string.Empty;
        string ReturnURL = string.Empty;
        string ChecksumKey = string.Empty;
        #endregion

        #region Set Bill Desk Param Data
        MerchantID = ConfigurationManager.AppSettings["MerchantID"];
        UniTranNo = TransactionCode;
        txn_amount = feeAmount;
        CurrencyType = "INR";
        SecurityID = ConfigurationManager.AppSettings["SecurityCode"];
        additional_info1 = ViewState["STUDNAME"].ToString(); // Project Name
        additional_info2 = ViewState["IDNO"].ToString();  // Project Code
        additional_info3 = ddlReceiptType.SelectedValue; //ViewState["RECIEPT"].ToString(); // Transaction for??
        additional_info4 = ViewState["info"].ToString(); // Payment Reason
        additional_info5 = feeAmount; // Amount Passed
        additional_info6 = ViewState["basicinfo"].ToString();  // basic details like regno/enroll no/branchname
        additional_info7 = ViewState["SESSIONNO"].ToString();

        ReturnURL = "https://svce.mastersofterp.in/ACADEMIC/FeesPay_Response.aspx";
        //ReturnURL = "http://localhost:50139/PresentationLayer/ACADEMIC/FeesPay_Response.aspx";


        ChecksumKey = ConfigurationManager.AppSettings["ChecksumKey"];
        #endregion

        #region Generate Bill Desk Check Sum

        StringBuilder billRequest = new StringBuilder();
        billRequest.Append(MerchantID).Append("|");
        billRequest.Append(UniTranNo).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(txn_amount).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(CurrencyType).Append("|");
        billRequest.Append("DIRECT").Append("|");
        billRequest.Append("R").Append("|");
        billRequest.Append(SecurityID).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("F").Append("|");
        billRequest.Append(additional_info1).Append("|");
        billRequest.Append(additional_info2).Append("|");
        billRequest.Append(additional_info3).Append("|");
        billRequest.Append(additional_info4).Append("|");
        billRequest.Append(additional_info5).Append("|");
        billRequest.Append(additional_info6).Append("|");
        billRequest.Append(additional_info7).Append("|");
        billRequest.Append(ReturnURL);

        string data = billRequest.ToString();

        String hash = String.Empty;
        hash = GetHMACSHA256(data, ChecksumKey);
        hash = hash.ToUpper();

        string msg = data + "|" + hash;

        #endregion

        #region Post to BillDesk Payment Gateway

        string PaymentURL = ConfigurationManager.AppSettings["BillDeskURL"] + msg;

        //Response.Redirect(PaymentURL, false);
        Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
        Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
        Response.Write("</script>");
        Response.Write("<script language='javascript' >");
        Response.Write("</script>");
        Response.Write("</form> ");
        Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
        #endregion
    }
    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    protected void btnOnlinePay_Click(object sender, EventArgs e)
    {
        #region "Online Payment---- Backup"
        //try
        //{

        //    ////////////////////////////
        //    if (ddlReceiptType.SelectedIndex > 0)
        //    {

        //        Button btnOnlinePay = (Button)(sender);
        //        Button btnOnlinePayy = (Button)(sender);
        //        int InstallmentNo = Convert.ToInt32(btnOnlinePay.CommandArgument);
        //        double Amount = Convert.ToDouble(btnOnlinePayy.CommandName);
        //        string payment_mode = "O";

        //        ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;
        //        //Find the label control
        //        //Label lblAmount = (Label)item.FindControl("lblAmount");
        //        //Label lblLateFee = (Label)item.FindControl("lblLateFee");
        //        Label lblReceiptCode = (Label)item.FindControl("lblReceiptCode");

        //        string receipt_type = ddlReceiptType.SelectedValue;
        //        if (receipt_type == "")
        //        {
        //            objCommon.DisplayMessage(pnlTransferCredit, "Something Went Wrong !.", this.Page);
        //            return;
        //        }
        //        if (lblReceiptCode.Text == "")
        //        {
        //            objCommon.DisplayMessage(pnlTransferCredit, "Something Went Wrong !.", this.Page);
        //            return;
        //        }

        //        CreateCustomerRef();
        //        GetSessionValues();

        //        int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + lblReceiptCode.Text + "'"));
        //        int idno = Convert.ToInt32(Session["IDNO"]);
        //        int session = Convert.ToInt32(ddlsession.SelectedValue);

        //        objStudentFees.Amount = Amount;
        //        objStudentFees.UserNo = (Convert.ToInt32(Session["IDNO"]));
        //        objStudentFees.SessionNo = ddlsession.SelectedValue;
        //        objStudentFees.OrderID = lblOrderID.Text;
        //        objStudentFees.TransDate = System.DateTime.Today;
        //        objStudentFees.BranchName = lblbranch.Text;
        //        objStudentFees.SessionNo = session.ToString();

        //        int demand_sem = 0;
        //        try
        //        {
        //            demand_sem = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DISTINCT SEMESTERNO", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + session + " AND RECIEPT_CODE ='" + lblReceiptCode.Text + "' AND ISNULL(CAN,0)=0 "));
        //        }
        //        catch { }
        //        if (demand_sem == 0)
        //        {
        //            objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
        //            return;
        //        }

        //        int demand_count = 0;
        //        try
        //        {
        //            demand_count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "ISNULL(Count(*),0)", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + session + " AND RECIEPT_CODE ='" + lblReceiptCode.Text + "' AND ISNULL(CAN,0)=0 AND SEMESTERNO=" + Convert.ToInt32(demand_sem) + " "));
        //        }
        //        catch { }
        //        if (demand_count == 0)
        //        {
        //            objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
        //            return;
        //        }


        //        //ObjNuc.InsertPendingAmountInDCR(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(lblSem.ToolTip), lblReceiptCode.Text, Amount,
        //        //    payment_mode, Convert.ToInt32(lblOrderID.Text), session, recept, InstallmentNo);
        //        //int result = 0;
        //        //result = ObjNuc.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(lblSem.ToolTip));
        //        //int orderid = Convert.ToInt32(objStudentFees.OrderID);

        //        ObjNuc.InsertPendingAmountInDCR(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(demand_sem), lblReceiptCode.Text, Amount,
        //            payment_mode, Convert.ToInt32(lblOrderID.Text), session, recept, InstallmentNo);
        //        int result = 0;
        //        result = ObjNuc.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(demand_sem));
        //        int orderid = Convert.ToInt32(objStudentFees.OrderID);

        //        if (result > 0)
        //        {

        //            DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(Session["IDNO"]) + "'", "");
        //            ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
        //            ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
        //            ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
        //            ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
        //            ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
        //            ViewState["SESSIONNO"] = objStudentFees.SessionNo;
        //            //ViewState["SEM"] = Convert.ToInt32(lblSem.ToolTip);
        //            ViewState["SEM"] = Convert.ToInt32(demand_sem);
        //            ViewState["RECIEPT"] = receipt_type;

        //            ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
        //            ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());

        //            if (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == "" || d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty)
        //            {
        //                ViewState["MOBILENO"] = "NA";
        //            }
        //            if (d.Tables[0].Rows[0]["REGNO"].ToString() == "" || d.Tables[0].Rows[0]["REGNO"].ToString() == string.Empty)
        //            {
        //                ViewState["REGNO"] = "NA";
        //            }
        //            if (d.Tables[0].Rows[0]["ENROLLNO"].ToString() == "" || d.Tables[0].Rows[0]["ENROLLNO"].ToString() == string.Empty)
        //            {
        //                ViewState["ENROLLNO"] = "NA";
        //            }
        //            string info = string.Empty;

        //            //ViewState["info"] = receipt_type + ViewState["REGNO"] + "," + ViewState["SESSIONNO"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];

        //            //ViewState["info"] = ViewState["SEM"] + "," + ViewState["MOBILENO"];
        //            //ViewState["basicinfo"] = ViewState["REGNO"] + "," + ViewState["ENROLLNO"] + "," + ViewState["SHORTNAME"];

        //            ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
        //            ViewState["basicinfo"] = ViewState["ENROLLNO"];

        //            PostOnlinePayment(Amount, orderid);
        //        }
        //        else
        //        {
        //            objCommon.DisplayMessage(pnlTransferCredit, "Transaction Failed !.", this.Page);
        //            return;
        //        }

        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(pnlTransferCredit, "Please Select Fees Type For Paying  Fees..", this.Page);
        //    }

        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}
        #endregion
            
        // ADDED BY NARESH BEERLA FOR THE TECH PROCESS PAYMENT GATEWAY PROCESS ON 23112021

        try
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
            String response = "";
            String lblResponse = "";
            string payment_mode = "O";
            RequestURL objRequestURL = new RequestURL();

            // ADDED HERE 

            Button btnOnlinePay = (Button)(sender);
            Button btnOnlinePayy = (Button)(sender);
            int InstallmentNo = Convert.ToInt32(btnOnlinePay.CommandArgument);
            double Amount = Convert.ToDouble(btnOnlinePayy.CommandName);
            
            ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;
            //Find the label control
            //Label lblAmount = (Label)item.FindControl("lblAmount");
            //Label lblLateFee = (Label)item.FindControl("lblLateFee");
            Label lblReceiptCode = (Label)item.FindControl("lblReceiptCode");
            string receipt_type = ddlReceiptType.SelectedValue;

            CreateCustomerRef();
            GetSessionValues();

            // int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + lblReceiptCode.Text + "'"));
            // int recept = Convert.ToInt32(ddlReceiptType.SelectedValue);
            int idno = Convert.ToInt32(Session["IDNO"]);
            int session = Convert.ToInt32(ddlsession.SelectedValue);

            objStudentFees.Amount = Amount;
            objStudentFees.UserNo = (Convert.ToInt32(Session["IDNO"]));
            objStudentFees.SessionNo = ddlsession.SelectedValue;
            objStudentFees.OrderID = lblOrderID.Text;
            objStudentFees.TransDate = System.DateTime.Today;
            objStudentFees.BranchName = lblbranch.Text;
            objStudentFees.SessionNo = session.ToString();

            int demand_semester = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DISTINCT SEMESTERNO", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + ddlsession.SelectedValue + " AND RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND ISNULL(CAN,0)=0 "));

            DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME", "IDNO = '" + Convert.ToInt32(Session["IDNO"]) + "'", "");
            ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
           
            ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
            ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
            ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
            ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
            ViewState["SESSIONNO"] = objStudentFees.SessionNo;
            //ViewState["SEM"] = Convert.ToInt32(lblSem.ToolTip);
            ViewState["SEM"] = Convert.ToInt32(ddlsem.SelectedValue); //Convert.ToInt32(demand_sem);
            ViewState["RECIEPT"] = receipt_type;

            ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
            ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());

            //For PG page 
            Session["firstname"] = d.Tables[0].Rows[0]["STUDNAME"].ToString();
            Session["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
            Session["SESSIONNO"] = objStudentFees.SessionNo;
            Session["InstallmentNo"] = InstallmentNo;
            //Session["txtBranchName"] = d.Tables[0].Rows[0]["BRANCHNAME"].ToString();
            //Session["txtDegreeName"] = d.Tables[0].Rows[0]["DEGREENAME"].ToString();
            //Session["txtYear"] = d.Tables[0].Rows[0]["YEAR"].ToString();
            //Session["lblAdmisstionBatch"] = d.Tables[0].Rows[0]["ADMBATCH"].ToString();
            Session["lblSemester"] = demand_semester;
            Session["receipt_type"] = receipt_type;
            //Session["amount"] = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
            Session["txtPhone"] = d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            Session["email"] = d.Tables[0].Rows[0]["EMAILID"].ToString();
            Session["productinfo"] = d.Tables[0].Rows[0]["IDNO"].ToString();
            Session["Amount"] = Amount;


            if (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == "" || d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty)
            {
                ViewState["MOBILENO"] = "NA";
            }
            if (d.Tables[0].Rows[0]["REGNO"].ToString() == "" || d.Tables[0].Rows[0]["REGNO"].ToString() == string.Empty)
            {
                ViewState["REGNO"] = "NA";
            }
            if (d.Tables[0].Rows[0]["ENROLLNO"].ToString() == "" || d.Tables[0].Rows[0]["ENROLLNO"].ToString() == string.Empty)
            {
                ViewState["ENROLLNO"] = "NA";
            }


            //ENDS HERE 

            string TXT_requesttype = "T";
            string TXT_ITC = ViewState["ENROLLNO"].ToString(); //"Saleel_K";//lblStudRollNo.Text;    //dtDetails.Rows[0]["REGNO"].ToString();//"Saleel_K";
            string TXT_Shoppingcartdetails = "FIRST_"+Amount+"_0.0"; // "FIRST_2000_0.0"; //"FIRST_" + dtDetails.Rows[0]["AMOUNT"] + "_0.0";
            string TXT_propertyPath = "D:\\DotnetIntegrationKit\\" + Convert.ToString(ConfigurationManager.AppSettings["merchant_id"]) + "\\Merchant.property";
            string TXT_merchantcode = Convert.ToString(ConfigurationManager.AppSettings["merchant_id"]);
            //TXT_MerchantTxnRefNo = Convert.ToString(dtDetails.Rows[0]["ORDERID"]);
            string TXT_Amount = Convert.ToString(Amount); //"20.00";//lblAmount.Text;//Convert.ToString(dtDetails.Rows[0]["AMOUNT"]);
            string TXT_Currencycode = Convert.ToString("INR");
            string TXT_returnURL = Convert.ToString(ConfigurationManager.AppSettings["RETURN_URL"]);
            string TXT_Email = ViewState["EMAILID"].ToString(); //lblMailId.Text;//Convert.ToString(dtDetails.Rows[0]["EMAILID"]);
            string TXT_mobileNo = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;//Convert.ToString(dtDetails.Rows[0]["MOBILENO"]);
            string TXT_customerName = ViewState["STUDNAME"].ToString(); // lblStudName.Text;//Convert.ToString(dtDetails.Rows[0]["STUDNAME"]);
            string TXT_IsKey = Convert.ToString(ConfigurationManager.AppSettings["IsKey"]);
            string TXT_IsIv = Convert.ToString(ConfigurationManager.AppSettings["IsIv"]);
            string TXT_uniqueCustomerID = lblOrderID.Text.Trim(); //"156465";//Convert.ToString(dtDetails.Rows[0]["ORDERID"]);
            txnid1 = TXT_uniqueCustomerID;
            string TXT_TxnDate = DateTime.Now.ToString("dd-MM-yyyy");
            string TXT_MerchantTxnRefNo = lblOrderID.Text.Trim();  //"156465";//Convert.ToString(dtDetails.Rows[0]["ORDERID"]);

            string TXT_StoSreturnURL = "0";
            string TXT_TPSLTXNID = "0";
            string TXT_Bankcode = "0";
            string TXT_CardID = "0";
            string TXT_AccountNo = "0";



            // *************************************************** ADDED FROM SVCE (FOR INSERTING THE DATA INTO ACD_DCR TABLE) 22-11-2021 ***********************************************//

            ObjNuc.InsertPendingAmountInDCR(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(demand_semester), ddlReceiptType.SelectedValue, Convert.ToDouble(TXT_Amount),
                   payment_mode, Convert.ToInt32(TXT_MerchantTxnRefNo), Convert.ToInt32(ViewState["SESSIONNO"].ToString()), receipt_type, InstallmentNo);
            int result = 0;
            result = ObjNuc.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(demand_semester));
            int orderid = Convert.ToInt32(objStudentFees.OrderID);

            Session["txnid1"] = txnid1;
            // *************************************************** ADDED FROM SVCE (FOR INSERTING THE DATA INTO ACD_DCR TABLE) 22-11-2021 ************************************************//

            DataSet ds1 = ObjNuc.GetOnlinePaymentConfigurationDetails(Convert.ToInt32(Session["OrgId"]), 0, Convert.ToInt32(Session["payactivityno"]));
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


        // ENDS HERE BY NARESH BEERLA FOR THE TECH PROCESS PAYMENT GATEWAY PROCESS ON 23112021


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

    //this is used for get reciept for all payment type added byt pankaj nakhale 17 march 2020
    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        //if (ddlReceiptType.SelectedIndex > 0)
        //{
        //    Button btnReceipt = (Button)(sender);
        //    string order_id = Convert.ToString(btnReceipt.CommandArgument);
        //    string session = Convert.ToString(ddlsession.SelectedValue);
        //    //this.ShowReport_receiptTypewise("Payment_Details", "PaymentReceiptInstallment_all_receipt_type.rpt");
        //    this.ShowReport_receiptTypewise("Payment_Details", "PaymentReceiptInstallment_all_receipt_type.rpt", order_id, session);
        //}
        //else
        //{
        //    objCommon.DisplayMessage(pnlTransferCredit, "Please Select Fees Type For Paying Fees..", this.Page);
        //    //objCommon.ShowError(this.Page, "Please Select Fees Type For paying the Fees..");
        //}

        Button btnPrintReceipt = sender as Button;
        int DcrNo = (btnPrintReceipt.CommandArgument != string.Empty ? int.Parse(btnPrintReceipt.CommandArgument) : 0);

        //ShowReport("InstallmentOnlineFeePayment", "rptInstallmentOnlineReceipt.rpt", DcrNo);
        ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt", Convert.ToInt32(DcrNo));
    }

    //ADDED ON [17 march 2020]GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY. br pankaj nakhale
    //private void ShowReport_receiptTypewise(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        //url += "&param=@P_IDNO=" + Convert.ToInt32(Session["IDNO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue.ToString();
    //        url += "&param=@P_IDNO=" + Convert.ToInt32(Session["IDNO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue.ToString();
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowReport_receiptTypewise(string reportTitle, string rptFileName, string _Order_id, string session)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_TRANSACTION_NO=" + ViewState["Vmer_txn"] + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_IDNO=" + Convert.ToInt32(Session["IDNO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue.ToString() + ",@P_SESSIONNO=" + session.ToString() + ",@P_ORDER_ID=" + _Order_id.ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //this is used for get Bank challan reciept  added byt pankaj nakhale 17 march 2020
    protected void btnChallan_Click(object sender, EventArgs e)
    {
        if (ddlReceiptType.SelectedIndex > 0)
        {
            ////////////////////////for challan insert added by pankaj 18032020///////////////////////////////////
            Button btnChallan = (Button)(sender);
            Button btnChallann = (Button)(sender);
            int InstallmentNo = Convert.ToInt32(btnChallan.CommandArgument);
            double Amount = Convert.ToDouble(btnChallann.CommandName);
            string payment_mode = "C";

            CreateCustomerRef();
            GetSessionValues();


            ListViewItem item = (ListViewItem)(sender as Control).NamingContainer;
            //Find the label control
            //Label lblAmount = (Label)item.FindControl("lblAmount");
            //Label lblLateFee = (Label)item.FindControl("lblLateFee");
            Label lblReceiptCode = (Label)item.FindControl("lblReceiptCode");

            string receipt_type = ddlReceiptType.SelectedValue;
            if (receipt_type == "")
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Something Went Wrong !.", this.Page);
                return;
            }
            if (lblReceiptCode.Text == "")
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Something Went Wrong !.", this.Page);
                return;
            }


           //  string receipt_type = ddlReceiptType.SelectedValue;
            // int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + lblReceiptCode.Text + "'"));
            // int recept = Convert.ToInt32(ddlReceiptType.SelectedValue);
            int idno = Convert.ToInt32(Session["IDNO"]);
            int session = Convert.ToInt32(ddlsession.SelectedValue);

            objStudentFees.Amount = Amount;
            objStudentFees.UserNo = (Convert.ToInt32(Session["IDNO"]));
            objStudentFees.SessionNo = ddlsession.SelectedValue;
            objStudentFees.OrderID = lblOrderID.Text;
            objStudentFees.TransDate = System.DateTime.Today;
            objStudentFees.BranchName = lblbranch.Text;
            objStudentFees.SessionNo = session.ToString();

            int demand_sem = 0;
            try
            {
                demand_sem = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DISTINCT SEMESTERNO", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + session + " AND RECIEPT_CODE ='" + lblReceiptCode.Text + "' AND ISNULL(CAN,0)=0 "));
            }
            catch { }
            if (demand_sem == 0)
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
                return;
            }

            int demand_count = 0;
            try
            {
                demand_count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "ISNULL(Count(*),0)", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + session + " AND RECIEPT_CODE ='" + lblReceiptCode.Text + "' AND ISNULL(CAN,0)=0 AND SEMESTERNO=" + Convert.ToInt32(demand_sem) + " "));
            }
            catch { }
            if (demand_count == 0)
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
                return;
            }



            int DCRNOTEMP = ObjNuc.InsertAmountInDCR_forBankChallan(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(demand_sem), lblReceiptCode.Text, Amount,
                payment_mode, Convert.ToInt32(lblOrderID.Text), session, receipt_type, InstallmentNo);
           

            this.ShowReport_BankChallan_forInstallmentStudent("Payment_Details", "rptBankChallan_ForInstallmentStudent.rpt", DCRNOTEMP);
            BindInstallmentDetailsInListview();
        }
        else
        {
            objCommon.DisplayMessage(pnlTransferCredit, "Please Select Fees Type For Paying  Fees..", this.Page);
        }
    }
   
    private void ShowReport_BankChallan_forInstallmentStudent(string reportTitle, string rptFileName, int DCRNOTEMP)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(Session["IDNO"]) + ",@P_SESSIONNO=" + ddlsession.SelectedValue + ",@P_RECEIPTTYPE=" + ddlReceiptType.SelectedValue + ",@P_tempdcr=" + DCRNOTEMP + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Atom_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlReceiptType.Items.Clear();
        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE ART INNER JOIN ACD_DEMAND D ON ART.RECIEPT_CODE=D.RECIEPT_CODE", "ART.RECIEPT_CODE", "ART.RECIEPT_TITLE,ART.RCPTTYPENO", "ISNULL(D.CAN,0)=0 AND SESSIONNO=" + ddlsession.SelectedValue + " AND IDNO=" + Convert.ToInt32(Session["IDNO"]), "ART.RCPTTYPENO"); // ADDED BY NARESH BEERLA ON 12052021 FOR RECEIPTNO >8 CONDITION AS PER REQUIREMENT
        //ddlReceiptType.SelectedIndex = 1;
        if (ddlsession.SelectedIndex > 0)
        {
            //string existsdemand = objCommon.LookUp("ACD_DEMAND", "count(1)", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO=" + ddlsession.SelectedValue + " AND RECIEPT_CODE='" + ddlReceiptType.SelectedValue + "'");
            string existsdemand = objCommon.LookUp("ACD_DEMAND", "count(1)", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO=" + ddlsession.SelectedValue);
            if (existsdemand == "0")
            {
                objCommon.DisplayMessage("No Demand found for the selection", this.Page);
                lvFeesPayment.DataSource = null;
                lvFeesPayment.DataBind();
                lvFeesPayment.Visible = false;
                lblRecpttypeAmount.Text = "0";
                return;
            }

         //   BindInstallmentDetailsInListview();
            if (ddlReceiptType.SelectedIndex == 0)
            {
                lblRecpttype.Visible = false;
                lblRecpttypeAmount.Visible = false;
                Label4.Visible = false;
                lvFeesPayment.Visible = false;
                objCommon.DisplayMessage("Please Select the Fees Type", this.Page);
                ddlReceiptType.Focus();
                return;
            }
            else
            {
                lblRecpttype.Text = ddlReceiptType.SelectedItem.Text;
                lblRecpttype.Visible = true;
                lblRecpttypeAmount.Visible = true;
                Label4.Visible = true;
                lvFeesPayment.Visible = true;
            }
            // int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(ddlsem.SelectedValue) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0"));

            //CHECKS WHETHER TOTAL AMOUNT IS PAID OR NOT FROM DEMAND 
            //if (ddlReceiptType.SelectedValue == "TF" || ddlReceiptType.SelectedValue == "HF" || ddlReceiptType.SelectedValue == "TPF")
            if (ddlReceiptType.SelectedIndex > 0)
            {
                string receipt_type = ddlReceiptType.SelectedValue;
                // int recept = Convert.ToInt32(objCommon.LookUp("ACD_Reciept_Type", "RCPTTYPENO", "RECIEPT_CODE ='" + receipt_type + "'"));
                // int recept = Convert.ToInt32(ddlReceiptType.SelectedValue);
                Label4.Visible = true;
                string receiptname = ddlReceiptType.SelectedItem.Text;
                lblRecpttype.Text = receiptname;
                double DemandAmount = Convert.ToDouble(objCommon.LookUp("ACD_DEMAND d inner join ACD_RECIEPT_TYPE rt on rt.reciept_code=d.reciept_code", "d.TOTAL_AMT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND sessionno =" + Convert.ToInt32(ddlsession.SelectedValue) + " and rt.RECIEPT_CODE ='" + receipt_type + "'"));
                lblRecpttypeAmount.Text = DemandAmount.ToString();
            }
            //ENDS HERE


            int demand_sem = 0;
            try
            {
                demand_sem = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "DISTINCT SEMESTERNO", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + ddlsession.SelectedValue + " AND RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND ISNULL(CAN,0)=0 "));
            }
            catch { }
            if (demand_sem == 0)
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
                return;
            }

            int demand_count = 0;
            try
            {
                demand_count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "ISNULL(Count(*),0)", "IDNO = " + Convert.ToInt32(Session["IDNO"]) + "AND SESSIONNO =" + ddlsession.SelectedValue + " AND RECIEPT_CODE ='" + ddlReceiptType.SelectedValue + "' AND ISNULL(CAN,0)=0 AND SEMESTERNO=" + Convert.ToInt32(demand_sem) + " "));
            }
            catch { }
            if (demand_count == 0)
            {
                objCommon.DisplayMessage(pnlTransferCredit, "Demand is not created proper !.", this.Page);
                return;
            }

            //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(lblSession.ToolTip) + " AND SEMESTERNO =" + Convert.ToInt32(lblSem.ToolTip) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0"));
            int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(ddlsession.SelectedValue) + " AND SEMESTERNO =" + Convert.ToInt32(demand_sem) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0 AND (SELECT SUM(TOTAL_AMT) FROM ACD_DCR WHERE IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(ddlsession.SelectedValue) + " AND SEMESTERNO =" + Convert.ToInt32(demand_sem) + " AND RECIEPT_CODE = '" + ddlReceiptType.SelectedValue + "' AND RECON = 1 AND ISNULL(CAN,0)=0)=" + lblRecpttypeAmount.Text));


            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage("Fee has been paid already. Can't proceed with the transaction !", this.Page);
                lblMsg.Text = "Fee has been paid already. Can't proceed with the transaction !";
                btnPAY.Visible = false;
                btnReports.Visible = true;
                return;
            }
            else
            {
                btnReports.Visible = false;
                lblMsg.Text = string.Empty;
            }

            //gettotal amount
            //check this is installment person or not
            int IsInstallment = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "COUNT(1)", "IDNO=" + Convert.ToInt32(Session["IDNO"]) + " AND SESSION_NO =" + Convert.ToInt32(ddlsession.SelectedValue)));


            // for adding late fee in demand
            double LateFee = 0;//comment by pankaj nakhale 11 march 2020    

            lblLateFee.Text = LateFee.ToString("0.00");
            btnPAY.Visible = true;
            btnReports.Visible = false;

           
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int DcrNo)
    {
        try
        {
            int IDNO = Convert.ToInt32(Session["idno"]);
          
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE="+Convert.ToInt32(Session["colcode"])+",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}