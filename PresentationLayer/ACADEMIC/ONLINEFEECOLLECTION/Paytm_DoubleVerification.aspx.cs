using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paytm;

public partial class ACADEMIC_ONLINEFEECOLLECTION_Paytm_DoubleVerification : System.Web.UI.Page
{
    Common objCommon = new Common();
    FeeCollectionController objFees = new FeeCollectionController();

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
            if (Session["userno"] == null || Session["username"] == null ||
             Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                lblColName.Text = Session["coll_name"].ToString();
                Img1.Src = "~/showimage.aspx?id=0&type=college";
                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelection.Visible = true;
                pnlDetails.Visible = false;
              
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
                #region fetch student details 

                string merchantId = string.Empty;    //"AVRI87KG16AW97IRWA";//from avenues
                string merchantKey = string.Empty;    //"287A7F2B9B153C43182DD07D3F90B925";// from avenues
                string orderNo = string.Empty;
                string CCAReferNo = string.Empty;
                string orderStatusQuery = string.Empty;
                string instanceMode = string.Empty;

                orderNo = txtOrderId.Text;

                DataSet StudDetails = objCommon.FillDropDown("ACD_DCR_TEMP A INNER JOIN ACD_STUDENT S ON (A.IDNO=S.IDNO) INNER JOIN ACD_SESSION_MASTER SM ON(A.SESSIONNO = SM.SESSIONNO)", "A.IDNO, A.ENROLLNMENTNO, A.NAME, A.BRANCHNO, A.BRANCH, S.COLLEGE_ID", "A.YEAR, A.DEGREENO, A.SEMESTERNO, A.SESSIONNO, SM.SESSION_NAME, A.RECIEPT_CODE", "A.ORDER_ID = '" + orderNo + "' ", "A.TEMP_DCR_NO DESC");
                DataTable dt = StudDetails.Tables[0];
                DataSet pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "MERCHANT_ID", "CHECKSUM_KEY, INSTANCE", "COLLEGE_ID= '" + dt.Rows[0]["COLLEGE_ID"].ToString() + "' AND  ACTIVE_STATUS = 1", "CONFIG_ID DESC");

                if (dt.Rows[0]["RECIEPT_CODE"].ToString() == "TF")
                {
                    if (pg_ds.Tables[0].Rows.Count == 0)
                    {
                        pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "MERCHANT_ID", "CHECKSUM_KEY, INSTANCE", "ACTIVITY_NO= 1 AND COLLEGE_ID=0 AND ACTIVE_STATUS=1", "CONFIG_ID DESC");
                    }
                }
                else
                {
                    pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "MERCHANT_ID", "CHECKSUM_KEY, INSTANCE", "ACTIVITY_NO= 2 AND COLLEGE_ID=0 AND ACTIVE_STATUS=1", "CONFIG_ID DESC");
                }

                if (pg_ds != null && pg_ds.Tables[0].Rows.Count > 0)
                {
                    merchantId = pg_ds.Tables[0].Rows[0]["MERCHANT_ID"].ToString();      //ADD MERCHANT_ID KEY HERE
                    merchantKey = pg_ds.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();     //ADD MERCHANT_KEY HERE
                    instanceMode = pg_ds.Tables[0].Rows[0]["INSTANCE"].ToString();
                }

                CheckTransactionStatus(orderNo, merchantId, merchantKey, StudDetails, instanceMode);
                //CheckTransactionStatus(orderId);
                //CCAvenueStatus(orderNo, CCAReferNo, accessCode, workingKey, StudDetails);
                #endregion
       
        }
        catch (Exception exp)
        {
            //Response.Write("Exception " + exp);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnhomered_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
        try
        {
            string status = "";
            status = ViewState["Status"].ToString();
            int output = 0;

          //  output = objFees.InsertOnlinePayment_DCR(ViewState["IDNO"].ToString(), "EF", Label_OrderInfo.Text, Label_MerchTxnRef.Text, "O", string.Empty, Label_Amount.Text, "Success", ViewState["EnrollemtID"].ToString(), "");


            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + lblEnrollNo.Text+"'");
           // return;

            if (status == "TXN_SUCCESS")
            {

                output = objFees.InsertOnlinePayment_DCR(idno, ViewState["RECIEPTCODE"].ToString(),Label_OrderInfo.Text,Label_MerchTxnRef.Text, "O", string.Empty, Label_Amount.Text, "Success", lblEnrollNo.Text.ToString(), "");           

                if (output == 1 && output != -99)
                {
                    objCommon.DisplayMessage(this.Page, "Success", this.Page);
                    Clear();
                }
                //else if (output == 1)
                //{
                //    objCommon.DisplayMessage(this.Page, "Already Exists", this.Page);
                //}
            }

        }
        catch (Exception exception)
        {
            //objCommon.DisplayMessage(Page, "Something Went Wrong", this.Page);
            throw;
        }
    }

    protected void Clear()
    {
        try
        {
           // ddlActivityName.SelectedIndex = 0;
          //  txtTransactionId.Text = string.Empty;
            pnlDetails.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Check Paytm Transaction Status
    protected void CheckTransactionStatus(string orderId, string merchantId, string merchantKey, DataSet StudDetails, string instanceMode)
    {
        Dictionary<string, string> body = new Dictionary<string, string>();
        Dictionary<string, string> head = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
        string url = string.Empty;
        //merchantId = "People73378390650054";
        //merchantKey = "w&dAbczGowoxu4eX";

        body.Add("mid", merchantId);
        body.Add("orderId", orderId);

        /*
        * Generate checksum by parameters we have in body
        * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
        */
        string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), merchantKey);

        head.Add("signature", paytmChecksum);

        requestBody.Add("body", body);
        requestBody.Add("head", head);

        string post_data = JsonConvert.SerializeObject(requestBody);
        //For  Staging"--https://securegw-stage.paytm.in/order/process?" --https://securegw-stage.paytm.in/v3/order/status
        if (instanceMode == "1")
        {
             url = "https://securegw-stage.paytm.in/v3/order/status";
        }
        if (instanceMode == "2")
        {
             url = "https://secure.paytm.in/v3/order/status";
        }

        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";
        webRequest.ContentLength = post_data.Length;

        using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
        {
            requestWriter.Write(post_data);
        }

        string responseData = string.Empty;

        using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
        {
            responseData = responseReader.ReadToEnd();
            Console.WriteLine(responseData);
        }

        var JsonData = JObject.Parse(responseData);
        var JsonBody = JObject.Parse(JsonData["body"].ToString());
        var JsonStatus = JObject.Parse(JsonBody["resultInfo"].ToString());

        //status NOTE -  resultCode = 01 OR resultStatus = "TXN_SUCCESS"  OR   resultMsg = "Txn Success"
        string resultStatus = JsonStatus["resultStatus"].ToString();
        string resultCode = JsonStatus["resultCode"].ToString();
        string resultMsg = JsonStatus["resultMsg"].ToString();

        // data
        if (resultCode == "01")
        {
            string txnId = JsonBody["txnId"].ToString();
            string bankTxnId = JsonBody["bankTxnId"].ToString();
            string order_Id = JsonBody["orderId"].ToString();
            string txnAmount = JsonBody["txnAmount"].ToString();
            string txnType = JsonBody["txnType"].ToString();
            string gatewayName = JsonBody["gatewayName"].ToString();
            string mid = JsonBody["mid"].ToString();
            string paymentMode = JsonBody["paymentMode"].ToString();
            if (paymentMode != "UPI") 
            {
                string bankName = JsonBody["bankName"].ToString();
            }
            string refundAmt = JsonBody["refundAmt"].ToString();
            string txnDate = JsonBody["txnDate"].ToString();
        }
        DataTable dt = StudDetails.Tables[0];
        if (!string.IsNullOrEmpty(resultCode) && resultCode == "01")
        {
            pnlDetails.Visible = true;

            if (resultCode == "01")
            {
                if (StudDetails.Tables[0].Rows.Count > 0)
                {
                    lblEnrollNo.Text = dt.Rows[0]["ENROLLNMENTNO"].ToString();
                    lblSem.Text = dt.Rows[0]["SEMESTERNO"].ToString();
                    lblyear.Text = dt.Rows[0]["YEAR"].ToString();
                    lblsessionName.Text = dt.Rows[0]["SESSION_NAME"].ToString();
                    lblRecieptType.Text = dt.Rows[0]["RECIEPT_CODE"].ToString();
                    ViewState["RECIEPTCODE"] = dt.Rows[0]["RECIEPT_CODE"].ToString();

                    Label_MerchTxnRef.Text = JsonBody["txnId"].ToString();
                    Label_OrderInfo.Text = JsonBody["orderId"].ToString();
                    lblname.Text = dt.Rows[0]["NAME"].ToString();
                    Label_Amount.Text = JsonBody["txnAmount"].ToString();
                    lblOrderSatus.Text = JsonStatus["resultStatus"].ToString();   
                    Label_Message.Text = JsonStatus["resultMsg"].ToString();    

                    string DCRCount = objCommon.LookUp("ACD_DCR", "COUNT (IDNO)", "TRANSACTIONID= '" + JsonBody["txnId"].ToString() + "' ");

                    if (resultCode == "01" && DCRCount == "0")
                    {
                        btnManage.Visible = true;
                        ViewState["Status"] = JsonStatus["resultStatus"].ToString();                   
                    }
                    else
                    {
                        string Receiptno = objCommon.LookUp("ACD_DCR", "REC_NO", "TRANSACTIONID='" + JsonBody["txnId"].ToString() + "' ");
                        lblrecno.Text = Receiptno;
                        Label_Message.Text = "Already Managed";
                        btnManage.Visible = false;
                    }

                    lbldate.Text = JsonBody["txnDate"].ToString();
                }

            }
        }
        else
        {
            lblEnrollNo.Text = dt.Rows[0]["ENROLLNMENTNO"].ToString();
            lblSem.Text = dt.Rows[0]["SEMESTERNO"].ToString();
            lblyear.Text = dt.Rows[0]["YEAR"].ToString();
            lblsessionName.Text = dt.Rows[0]["SESSION_NAME"].ToString();
            lblRecieptType.Text = dt.Rows[0]["RECIEPT_CODE"].ToString();
            ViewState["RECIEPTCODE"] = dt.Rows[0]["RECIEPT_CODE"].ToString();

            Label_MerchTxnRef.Text = "NA";
            Label_OrderInfo.Text = JsonBody["orderId"].ToString();
            lblname.Text = dt.Rows[0]["NAME"].ToString();
            Label_Amount.Text = "NA";
            lblOrderSatus.Text = JsonStatus["resultStatus"].ToString();
            Label_Message.Text = JsonStatus["resultMsg"].ToString();    //"SUCCESS" OR "Txn Success" OR  "Successfuly"
            lbldate.Text = "NA";

            lblrecno.Text = "NA";
            pnlDetails.Visible = true;
            btnManage.Visible = false;
            //JsonData["order_date_time"].ToString();
            //decResponse = status + "|" + enc_response + "|" + enc_error_code;
            ////objCommon.DisplayMessage(this.updServertoServer, "failure response from ccAvenues: " + decResponse, this.Page);
        }
    }

    
    #endregion


    



}