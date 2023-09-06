using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Configuration;
using System.IO;
using IITMS;
using System.Data;

public partial class Atlas_Payment_Response : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentRegist objSR = new StudentRegist();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                SendTransaction();
            }
        }

        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

        }
    }
    protected void SendTransaction()
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        foreach (string key in Request.Form.Keys)
        {
            parameters.Add(key.Trim(), Request.Form[key].Trim());
        }
        ViewState["Transaction"] = parameters["orderId"];
        ViewState["Amount"] = parameters["orderAmount"];
        ViewState["PaymentMode"] = parameters["paymentMode"];
        ViewState["Message"] = parameters["txMsg"];
        ViewState["Sataus"] = parameters["txStatus"];
        ViewState["ReferenceId"] = parameters["referenceId"]; //TransactionID
       

        ViewState["TimeTransaction"] = parameters["txTime"];
        //lblTransactionID.Text = ViewState["Transaction"].ToString();
        lblTransactionID.Text = ViewState["ReferenceId"].ToString();//TransactionID
        lblAmount.Text = ViewState["Amount"].ToString();
        lblPaymentMode.Text = ViewState["PaymentMode"].ToString();
        lblMessage.Text = ViewState["Message"].ToString();
        lblSataus.Text = ViewState["Sataus"].ToString();
        // string Time = parameters["txTime"];
        // lblReferenceId.Text = ViewState["ReferenceId"].ToString();
        lblReferenceId.Text = ViewState["Transaction"].ToString();

        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "DISTINCT RECIEPT_CODE", "ORDER_ID = '" + ViewState["Transaction"] + "'");   //Added dt on 02112022
        ViewState["Reciept_Code"] = rec_code;
        if (rec_code == "PRF" || rec_code == "RF")
        {
            CheckResponseFromChashfree();
        }
        else
        {
            Update_ResultTable();
        }
       

    }
    protected void Update_ResultTable()
    {
        // Get first 4 digit substring from a string ORDER ID    
        string GetIdno = ViewState["Transaction"].ToString();       
        string IDNO = GetIdno.Substring(0, 4).ToString();
        Session["IDNO"] = IDNO;       
        int output = 0;
        string orderid = objCommon.LookUp("ACD_DCR_TEMP DT", "DISTINCT ORDER_ID", "ORDER_ID='" + ViewState["Transaction"]+"'");
        if (orderid != null)
        {
            StudentController objSC = new StudentController();
            DataSet dsStudent = objSC.GetStudentDetailsExamRegistration(ViewState["Transaction"].ToString());
            string Idno = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
            string amount = ViewState["Amount"].ToString();
            objSR.STUDNAME = dsStudent.Tables[0].Rows[0]["NAME"].ToString();
            objSR.ReceiptFlag = dsStudent.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            objSR.REGNO = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
            string Regno = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
            string MSG = ViewState["Message"].ToString();
            int orgid = Convert.ToInt32(ViewState["OrganizationId"]);           
            //Inser Entry into DCR after Payment "SUCCESS" get info on orderid wise

            if (ViewState["Sataus"].ToString() == "SUCCESS" || ViewState["Sataus"].ToString() == "Success")
            {
                output = objFees.InsertOnlinePayment_DCR(Idno, objSR.ReceiptFlag, orderid, ViewState["ReferenceId"].ToString(), "O", "1", amount, "Success", Regno, MSG);
                btnReciept.Visible = true;
            }
            else
            {                
                output = objFees.InsertOnlinePaymentlog(Idno, objSR.ReceiptFlag, "O", amount, MSG, orderid);
            }
            
        }
        //Add FEES LOG IF ORDER ID NOT GENERATE 09_11_2022
        else
        {
            output = objFees.InsertOnlinePaymentlog(IDNO, objSR.ReceiptFlag, "O", ViewState["Amount"].ToString(), ViewState["Message"].ToString(), ViewState["ReferenceId"].ToString());          
      
        }
    
    }
    protected void btnReciept_Click(object sender, EventArgs e)
    {



        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "DISTINCT RECIEPT_CODE", "ORDER_ID = '" + ViewState["Transaction"] + "'");   //Added dt on 02112022
        if (rec_code == "PRF" || rec_code == "RF")
        {
            ShowReportPhotoCopy("Photo Copy Registration Slip", "rptOnlineReceiptPhotoCopy_ATLAS.rpt");
        }
        else if (rec_code == "AEF")
        {
            ShowReport("BacklogRegistration", "rptOnlineReceiptbBacklog_ATLAS.rpt");
        }
        else {

            ShowReport("RESIT", "rptOnlineReceiptbResit_ATLAS.rpt");
        }
      //  ShowReport("BacklogRegistration", "rptOnlineReceiptbBacklog_ATLAS.rpt");

    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            Session["username"] = "Admin";
            Session["userno"] = 1;
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(ViewState["Transaction"]) + "'"));
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(ViewState["Transaction"]) + "'"));
           // int collegecode = Convert.ToInt32(objCommon.LookUp("", "COLLEGE_CODE", "ORDER_ID='" + Convert.ToString(ViewState["Transaction"]) + "'"));
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + IDNO));
        /// string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
           
             
           string url = "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE="+collegecode+",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            ////To open new window from Updatepanel
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }


    // ----  BILl Desk Payment Response 
    public void CheckResponseFromChashfree()
    {
        string TxnReferenceNo = string.Empty;
        string PaymentFor = string.Empty;       //Transaction Type used for to check online transaction type
        string Trans_Status = Convert.ToString(ViewState["Sataus"]);

        if (Trans_Status == "Success" || Trans_Status == "SUCCESS")
        {
            Session["ResponseMsg"] = "Payment success";
            string AuthStatus = "0300";
            btnReciept.Visible = true;
            if (ViewState["Transaction"] != null)
            {
                int result = 0;
                string rec_code = objCommon.LookUp("ACD_DCR_temp", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Transaction"] + "'");
                // string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + Session["Order_Id"] + "'");

                if (rec_code == "PRF")
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 1); //1 for photo copy
                }
                else if (rec_code == "RF")//"RF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 2); //2 for reval
                }
                else if (rec_code == "AEF")//"AEF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 3); //3 for arrear exam
                }
                else if (rec_code == "CF")//"CF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 4); //4 for Condonation
                }
                else if (rec_code == "REF")//"REF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 5); //5 for review on 06052022
                }
            }

        }
        else
        {
            string AuthStatus = "0399";   //ViewState["Sataus"].ToString();        //"0399";
            Session["ResponseMsg"] = "Please try again.";
            if (ViewState["Transaction"] != null)
            {
                int result = 0;
                string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Transaction"] + "'");

                if (rec_code == "PRF")
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 1);  //1 for photo copy
                }
                else if (rec_code == "RF")//"RF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 2);  //2 for reval
                }
                else if (rec_code == "AEF")//"AEF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 3);  //3 for arrear
                }
                else if (rec_code == "CF")//"CF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 4);  //4 for Condonation
                }
                else if (rec_code == "REF")//"REF"
                {
                    result = objFees.OnlinePhotoRevalPayment(TxnReferenceNo, Convert.ToString(ViewState["Transaction"]), Convert.ToString(ViewState["Amount"]), AuthStatus, Convert.ToString(ViewState["Message"]), Trans_Status, Convert.ToString(ViewState["Message"]), Convert.ToString(ViewState["ReferenceId"]), PaymentFor, 5);  //5 for REVIEW  on 06052022
                }
            }
        }

    }
    private void ShowReportPhotoCopy(string reportTitle, string rptFileName)
    {
        try
        {
            Session["username"] = "Admin";
            Session["userno"] = 1;
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(ViewState["Transaction"]) + "'"));
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(ViewState["Transaction"]) + "'"));
            // int collegecode = Convert.ToInt32(objCommon.LookUp("", "COLLEGE_CODE", "ORDER_ID='" + Convert.ToString(ViewState["Transaction"]) + "'"));
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + IDNO));
            /// string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            int revalType = 0;
            string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "DISTINCT RECIEPT_CODE", "ORDER_ID = '" + ViewState["Transaction"] + "'");
            if (rec_code == "PRF")
            {
                revalType = 1;
            }
            else if (rec_code == "RF")
            {
                revalType = 2;
            }

            string url = "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_REVALTYPE=" + revalType;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            ////To open new window from Updatepanel
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