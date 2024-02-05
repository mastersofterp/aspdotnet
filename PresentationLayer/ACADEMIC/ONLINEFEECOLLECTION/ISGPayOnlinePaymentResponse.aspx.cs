using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;


public partial class ISGPayOnlinePaymentResponse : System.Web.UI.Page
{
    #region Class declaration
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu=new StudentController ();
    SemesterRegistration objsem = new SemesterRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    ISGPayReturnParameter isgPayReturnParams = null;
    OrganizationController objOrg = new OrganizationController();

    #endregion

    string hash_seq = string.Empty;
    string UserFirstPaymentStatus = string.Empty;
    int degreeno = 0;
    int college_id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                DataSet Orgds = null;
                int Ord_Id = Convert.ToInt32(Session["OrgId"]);
                Orgds = objOrg.GetOrganizationById(Ord_Id);
                byte[] imgData = null;
                if (Orgds.Tables != null)
                {
                    if (Orgds.Tables[0].Rows.Count > 0)
                    {

                        if (Orgds.Tables[0].Rows[0]["Logo"] != DBNull.Value)
                        {
                            imgData = Orgds.Tables[0].Rows[0]["Logo"] as byte[];
                            imgCollegeLogo.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                        }
                        else
                        {
                            // hdnLogoOrg.Value = "0";
                        }

                    }
                }
                //SqlDataReader dr = objCommon.GetCommonDetails();
                //if (dr != null)
                //{
                //    if (dr.Read())
                //    {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        Session["OrgId"] = dr["OrganizationId"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //    }
                //}

                string merchantID = string.Empty;
                string secureSecret = string.Empty;  // secureSecret
                string encryptionKey = string.Empty;

                //if (Session["merchentkey"].ToString() != null)
                //{
                //    merchantID = Session["merchentkey"].ToString();
                //}
                //else
                //{
                merchantID = objCommon.LookUp("ACD_PG_CONFIGURATION", "MERCHANT_ID", "ACTIVE_STATUS= 1");  //"101000000000781"; AND INSTANCE = 1
                //}

                DataSet pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "ACCESS_CODE", "CHECKSUM_KEY", "MERCHANT_ID= '" + merchantID + "' ", "CONFIG_ID DESC");   //Merchant_Id
                if (pg_ds != null && pg_ds.Tables[0].Rows.Count > 0)
                {
                    secureSecret = pg_ds.Tables[0].Rows[0]["ACCESS_CODE"].ToString();      //ADD SECURE_SECRET  HERE
                    encryptionKey = pg_ds.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();     //ADD EncryptionKey HERE
                }

                //Get call ISGPayResponse method 
                ISGPayResponse(secureSecret, encryptionKey);
           
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region ISGPay Response Code
    protected void ISGPayResponse(string secureSecret, string encryptionKey)
    {
        ISGPayReturnParameter isgPayReturnParams = null;

        string order_id = string.Empty;
        string amount = string.Empty;
        string firstname = string.Empty;
        string emailId = string.Empty;
        string Idno = string.Empty;
        string recipt = string.Empty;
        string saltkey = string.Empty;
        string hash_seq = string.Empty;
        string mihpayid = string.Empty;
        string Regno = string.Empty;
        string transactionDate = string.Empty;
        string totalAmount = string.Empty;
        string tokenID = string.Empty;
        string statustimeStamp = string.Empty;
        string trackID = string.Empty;
        string txnStatus = string.Empty;
        string actionInfo = string.Empty;
        string feeType = string.Empty;
        string merchanttxnID = string.Empty;
        string merchantSubID = string.Empty;
        string timeStamp = string.Empty;
        string studIdno = string.Empty;
        string studName = string.Empty;
        int installmentno = 0;

        Panel_Debug.Visible = false;
        string message = "";
        Label_HashValidation.Text = "<font color='orange'><b>NOT CALCULATED</b></font>";
        bool hashValidated = true;

        try
        {
            ISGPayDecryption a = new ISGPayDecryption();
            isgPayReturnParams = a.Decrypt(Page.Request.Form, encryptionKey, secureSecret);

            #region  Check Response Status

            if (isgPayReturnParams != null)
            {

                if (isgPayReturnParams.Vbpc_SecureHash.ToUpper().Equals(isgPayReturnParams.Isghash))
                {
                    // Secure Hash validation succeeded,
                    Label_HashValidation.Text = "<font color='#00AA00'><b>CORRECT</b></font>";
                }
                else
                {
                    // Secure Hash validation failed, add a data field to be displayed
                    Label_HashValidation.Text = "<font color='#FF0066'><b>INVALID HASH</b></font>";
                    hashValidated = false;
                }

                // Get the standard receipt data from the parsed response

               // Label_TxnResponseCode.Text = isgPayReturnParams.isgPayResponse.ResponseCode;
                lblamount.Text = isgPayReturnParams.isgPayResponse.Amount;
              //  Label_TerminalId.Text = isgPayReturnParams.isgPayResponse.TerminalId;
                lblTrasactionId.Text = isgPayReturnParams.isgPayResponse.RetRefNo;
               // Label_MerchantID.Text = isgPayReturnParams.isgPayResponse.MerchantId;
               // Label_AuthorizeID.Text = isgPayReturnParams.isgPayResponse.AuthCode;
                Label_AuthStatus.Text = isgPayReturnParams.isgPayResponse.AuthStatus;

                lblOrderId.Text = isgPayReturnParams.isgPayResponse.TxnRefNo;
               // Label_OrderInfo.Text = isgPayReturnParams.isgPayResponse.OrderInfo;

                //var TxnType = isgPayReturnParams.isgPayResponse.TxnType;
                var SecureHash = isgPayReturnParams.Isghash.ToString();
             
                //if (Session["Installmentno"].ToString() != null && Session["Installmentno"].ToString() != "0")
                //{
                //    installmentno = Convert.ToInt32(Session["Installmentno"]);     //isgPayReturnParams.isgPayResponse.UDF04  -  Session["Installmentno"]
                //}

                installmentno = 0;
    
                order_id = isgPayReturnParams.isgPayResponse.TxnRefNo;      //merchanttxnID;  //tokenID;
                Session["order_id"] = order_id;
                amount = isgPayReturnParams.isgPayResponse.Amount;
                txnStatus = isgPayReturnParams.isgPayResponse.ResponseCode;  // ResponseCode - success , failed
                mihpayid = isgPayReturnParams.isgPayResponse.RetRefNo;   // TransactionId 
                trackID = isgPayReturnParams.isgPayResponse.RetRefNo;      // TransactionId
                Idno = isgPayReturnParams.isgPayResponse.OrderInfo; // Get retrive student IDNO
                Session["Stud_IDNO"] = Idno;
                //Idno = Session["idno"].ToString(); //isgPayReturnParams.isgPayResponse.UDF01;//Convert.ToString(Session["idno"]);  //studIdno; temp pass 

                if (message.Length == 0)
                {
                    message = isgPayReturnParams.isgPayResponse.Message;
                }
                ViewState["IDNO"] = Idno;

                if (Session["OrgId"].ToString() == "6")
                {
                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                }
                if (Session["OrgId"].ToString() == "8")
                {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                }


                #region Fetch student details

                Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString());

                string BRANCHNAME = objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "B.LONGNAME", "A.IDNO=" + ViewState["IDNO"].ToString());

                DataSet ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT S ON(S.IDNO = U.UA_IDNO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO = S.BRANCHNO)", "UA_NAME", "UA_NO,UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG,B.LONGNAME", "UA_IDNO=" + Convert.ToInt32(Idno), string.Empty);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                    Session["userno"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                    Session["branchname"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                }

                Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");
                string semester = objCommon.LookUp("ACD_DCR_TEMP", "SEMESTERNO", "IDNO=" + ViewState["IDNO"].ToString());
                string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + ViewState["IDNO"].ToString());

                #endregion

                #region Payment status track

                lblRegNo.Text = Regno;
                lblstudentname.Text = Name;
                lblOrderId.Text = order_id;
                lblamount.Text = amount;
                mihpayid = trackID;
                lblBranch.Text = BRANCHNAME;
                //Convert.ToString(Session["branchname"]);
                lblSemester.Text = semester;
                lblTrasactionId.Text = mihpayid;          //trackID ==  mihpayid; 
                timeStamp = System.DateTime.Now.ToString();
                lblTransactionDate.Text = timeStamp;

                if (txnStatus.ToLower().ToString() == "00")        //Payment response in - (SUCCESS = "00", FAILURE =" ", AWAITED ="")
                {
                    string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                    string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");

                    divSuccess.Visible = true;
                    divFailure.Visible = false;
                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    objsem.IdNo = Convert.ToInt32(Idno);
                    objsem.SESSIONNO = Convert.ToInt32(Session["currentsession"].ToString());
                    objsem.SemesterNO = Convert.ToInt32(lblSemester.Text);
                    objsem.paymentMode = 1;
                    objsem.OfflineMode = 0;
                    objsem.Total_Amt = Convert.ToDecimal(lblamount.Text);

                    objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                    objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");
                    int output = 0;

                    if (Convert.ToInt32(installmentno) > 0)
                    {
                        output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");

                    }
                    else
                    {
                        output = objFees.InsertOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Regno, "-");
                    }


                    output = objFees.UpdateISGPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);

                    btnPrint.Visible = true;

                }

                if (txnStatus.ToLower().ToString() != "00")   // FAILURE,Cancel
                {
                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    int result = 0;
                    order_id = Session["order_id"].ToString();

                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    txnMessage = "";
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                    //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                    result = objFees.UpdateISGPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);
                    btnPrint.Visible = false;
                }


                //if (txnStatus.ToLower().ToString() == "awaited")    // AWAITED No use
                //{
                //    divSuccess.Visible = false;
                //    divFailure.Visible = true;
                //    int result = 0;
                //    order_id = Session["OrderId"].ToString();

                //    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                //    txnMessage = "";
                //    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                //    objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                //    //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                //    result = objFees.UpdateIOBPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);
                //    btnPrint.Visible = false;
                //}
                #endregion

            }
            else
            {
                objFees.UpdateISGPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);
            }
            #endregion

        }
        catch (Exception ex)
        {
            message = "(51) Exception encountered. " + ex.Message;
            if (ex.StackTrace.Length > 0)
            {
                Label_StackTrace.Text = ex.ToString();
                Panel_StackTrace.Visible = true;
            }

        }

        // output the message field
        Label_Message.Text = message;
    }
    #endregion

    #region Commom Code
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string ptype = (objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_PAYMENTTYPE P ON (A.PTYPE=P.PAYTYPENO) ", "PAYTYPENAME", "IDNO=" + Session["Stud_IDNO"].ToString()));
        if (ptype == "Provisional" && Session["OrgId"].ToString() == "5")
        {
            //ShowReport("InstallmentOnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));

            ShowReport("OnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt");
            return;
        }
        else
        {
            ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName) 
    {
        try
        {
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);

            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + Session["Stud_IDNO"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(Session["order_id"]) + "'");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        //Response.Redirect(returnpageurl);
        Response.Redirect("~/default.aspx");
    }
    #endregion
}
    
