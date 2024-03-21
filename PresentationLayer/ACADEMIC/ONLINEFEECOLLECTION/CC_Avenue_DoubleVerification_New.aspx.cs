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

public partial class ACADEMIC_ONLINEFEECOLLECTION_CC_Avenue_DoubleVerification_New : System.Web.UI.Page
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
            string accessCode =  string.Empty;    //"AVRI87KG16AW97IRWA";//from avenues
            string workingKey = string.Empty;    //"287A7F2B9B153C43182DD07D3F90B925";// from avenues
            string orderNo = string.Empty;
            string CCAReferNo = string.Empty;
            string orderStatusQuery  = string.Empty;

            orderNo = txtOrderId.Text;
            CCAReferNo = txtTransactionId.Text;
            // orderStatusQuery = orderNo + "|" + CCAReferNo;
            //string CollegeId = objCommon.LookUp("ACD_STUDENT  A LEFT JOIN ACD_DCR_TEMP DT ON A.IDNO = DT.IDNO", "DISTINCT COLLEGE_ID", "ORDER_ID='" + orderNo + "' ");
           //string reciptcode = objCommon.LookUp("ACD_STUDENT  A LEFT JOIN ACD_DCR_TEMP DT ON A.IDNO = DT.IDNO", "DISTINCT RECIEPT_CODE", "ORDER_ID='" + orderNo + "' ");

            DataSet StudDetails = objCommon.FillDropDown("ACD_DCR_TEMP A INNER JOIN ACD_STUDENT S ON (A.IDNO=S.IDNO) INNER JOIN ACD_SESSION_MASTER SM ON(A.SESSIONNO = SM.SESSIONNO)", "A.IDNO, A.ENROLLNMENTNO, A.NAME, A.BRANCHNO, A.BRANCH, S.COLLEGE_ID", "A.YEAR, A.DEGREENO, A.SEMESTERNO, A.SESSIONNO, SM.SESSION_NAME, A.RECIEPT_CODE", "A.ORDER_ID = '" + orderNo + "' ", "A.TEMP_DCR_NO DESC");
            DataTable dt = StudDetails.Tables[0];
            CCAvenueStatus(orderNo, CCAReferNo, "", "", StudDetails);

        }
        catch (Exception exp)
        {
            //Response.Write("Exception " + exp);

        }
    }
    private string postPaymentRequestToGateway(String queryUrl, String urlParam)
    {

        String message = "";
        try
        {
            StreamWriter myWriter = null;// it will open a http connection with provided url
            WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
            objRequest.Method = "POST";
            //objRequest.ContentLength = TranRequest.Length;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            // Skip validation of SSL/TLS certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
            myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(urlParam);//send data
            myWriter.Close();//closed the myWriter object

            // Getting Response
            System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
            using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
            {
                message = sr.ReadToEnd();
                //Response.Write(message);
            }
        }
        catch (Exception exception)
        {
            Console.Write("Exception occured while connection." + exception);
        }
        return message;

    }
    private NameValueCollection getResponseMap(String message)
    {
        NameValueCollection Params = new NameValueCollection();
        if (message != null || !"".Equals(message))
        {
            string[] segments = message.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }
        }
        return Params;
    }

    public void FillSuccessString(String data)
    {
        pnlSelection.Visible = false;
        pnlDetails.Visible = true;
        List<String> listStrLineElements;
        listStrLineElements = data.Split('|').ToList();
        string status = listStrLineElements[1].ToString();
        //string[] segments = data.Split('|');
        ViewState["Status"] = status;
        if (status == "Shipped")
        {
            Label_MerchTxnRef.Text = listStrLineElements[2].ToString();
            Label_OrderInfo.Text = listStrLineElements[21].ToString();
            int idno = Convert.ToInt32(listStrLineElements[46].ToString());
            Label_Amount.Text = listStrLineElements[34].ToString();
            lblname.Text = listStrLineElements[5].ToString();
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= " + idno);
            string Reciept_code = string.Empty;


            DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_RECIEPT_TYPE RT ON(D.RECIEPT_CODE = RT.RECIEPT_CODE) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,D.RECIEPT_CODE,RECIEPT_TITLE,D.REC_NO", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lblsessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblyear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_TITLE"].ToString();
                ViewState["RECIEPTCODE"] = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                lblrecno.Text = ds.Tables[0].Rows[0]["REC_NO"].ToString();
                Reciept_code = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            }
            //if (Reciept_code == "EF")
            //{
            //    int output = objFees.InsertExamPayment_DCR(Convert.ToString(idno), Reciept_code, Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno);
            //}
            //else
            //{
            //    int output = objFees.InsertOnlinePayment_DCR(Convert.ToString(idno), Reciept_code, Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno,"");
            //}
            lblEnrollNo.Text = Enrollno.ToString();

           // Label_MerchantID.Text = "338483";
            lbldate.Text = listStrLineElements[15].ToString();

            Label_Message.Text = "Transaction Success";
        }
        else
        {
            Label_MerchTxnRef.Text = listStrLineElements[2].ToString();
            Label_OrderInfo.Text = listStrLineElements[21].ToString();
            int idno = Convert.ToInt32(listStrLineElements[46].ToString());
            Label_Amount.Text = listStrLineElements[34].ToString();
            lblname.Text = listStrLineElements[5].ToString();
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= " + idno);

            //int output = objFees.InsertOnlinePayment_DCR(Convert.ToString(idno), "EF", Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno);

            DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,'Exam Fees' AS RECIEPT_TITLE,D.REC_NO", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lblsessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblyear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_TITLE"].ToString();
                ViewState["RECIEPTCODE"] = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                lblrecno.Text = ds.Tables[0].Rows[0]["REC_NO"].ToString();

            }

            lblEnrollNo.Text = Enrollno.ToString();

            Label_MerchantID.Text = "338483";
            lbldate.Text = listStrLineElements[15].ToString();

            Label_Message.Text = "Transaction Failed";
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


            string idno = objCommon.LookUp("ACD_DCR_TEMP", "IDNO", "ORDER_ID='" + Label_OrderInfo.Text+"'");
           // return;

            if (status == "Shipped")
            {   
                output = objFees.InsertOnlinePayment_DCR(idno, ViewState["RECIEPTCODE"].ToString(), Label_OrderInfo.Text,Label_MerchTxnRef.Text, "O", string.Empty, Label_Amount.Text, "Success", lblEnrollNo.Text.ToString(), "");           

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
            txtTransactionId.Text = string.Empty;
            pnlDetails.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // Added Payment double verification check status by Gopal M 06112023  Ticket#50138
    public void CCAvenueStatus(string OrderNo, string TrackingId, string accessCode, string workingKey, DataSet StudDetails)
    {

        string decResponse = "";
        try
        {
            var LiveUrl = "https://api.ccavenue.com/apis/servlet/DoWebTrans?command=orderStatusTracker&version=1.2&request_type=JSON&response_type=JSON&access_code=";
            var TestUrl = "https://apitest.ccavenue.com/apis/servlet/DoWebTrans?command=orderStatusTracker&version=1.2&request_type=JSON&response_type=JSON&access_code=";

            //var Key = "BC932E07F545163283B42DB94210C8D9";//"Keyxxxx xxxxx";
            //var Code = "AVZO18KJ08AJ36OZJA";//"Codexxxxxxxxx";
            var reqParam = new StatusEncRequest();
            // reqParam.order_no = OrderNo;
            reqParam.reference_no = TrackingId;

            string ccaRequest = JsonConvert.SerializeObject(reqParam);
            CCACrypto ccaCrypto = new CCACrypto();
            DataTable dt = new DataTable();


           DataSet pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", " DISTINCT ACCESS_CODE", "CHECKSUM_KEY", "ISNULL(ACTIVE_STATUS,0)=1", "");
            string strEncRequest = string.Empty;

            foreach (DataRow r in pg_ds.Tables[0].Rows)
            {
                accessCode = r["ACCESS_CODE"].ToString();
                workingKey = r["CHECKSUM_KEY"].ToString();

                try
                {
                    strEncRequest = (ccaCrypto.Encrypt(ccaRequest, workingKey));
                    var curl = LiveUrl + accessCode + "&enc_request=" + strEncRequest;
                    var client = new RestClient(curl);
                    var request = new RestRequest(curl, Method.POST);
                    //RestResponse restResponse = null;  //client.Execute(request);
                    IRestResponse restResponse = client.Execute(request);
                    var response = restResponse.Content;

                    if (!string.IsNullOrEmpty(response))
                    {
                        NameValueCollection Params = new NameValueCollection();
                        string[] segments = response.Split('&');
                        foreach (string seg in segments)
                        {
                            string[] parts = seg.Split('=');
                            if (parts.Length > 0)
                            {
                                string KeyParam = parts[0].Trim();
                                string Value = parts[1].Trim();
                                Params.Add(KeyParam, Value);
                            }
                        }

                        string enc_response = string.IsNullOrEmpty(Params["enc_response"]) ? "" : Params["enc_response"];
                        string enc_error_code = string.IsNullOrEmpty(Params["enc_error_code"]) ? "" : Params["enc_error_code"];
                        dt = StudDetails.Tables[0];


                        if (enc_response == "Access_code: Invalid Parameter")
                        {
                            continue;
                        }
                        else 
                        {
                            decResponse = ccaCrypto.Decrypt(enc_response, workingKey);
                        }
                       

                        var JsonData1 = JObject.Parse(decResponse);
                        string status1 = string.IsNullOrEmpty(JsonData1["status"].ToString()) ? "" : JsonData1["status"].ToString();
                        if (!string.IsNullOrEmpty(status1) && status1 == "0")
                        {
                            break;

                        }
                        else 
                        {
                            continue;
                        }

                        //        var check_error = "";

                        //        // break;
                    }
                }
                catch (Exception Ex)
                {
                    //continue;
                    //lblResponse.Text = Request.Form["encResp"].ToString();
                }

            }



            ////int Count = 0;
            //for (int i = 0; i <= pg_ds.Tables[0].Rows.Count; i++)
            //{
            //    //  Count++;
            //    accessCode = pg_ds.Tables[0].Rows[i]["ACCESS_CODE"].ToString();
            //        //"AVAY80KF51BR28YARB";

            //    workingKey = pg_ds.Tables[0].Rows[i]["CHECKSUM_KEY"].ToString();
            //        //"ACC3927142E9155023F668031786175B";
            //    // encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            //}



            var JsonData = JObject.Parse(decResponse);
            string status = string.IsNullOrEmpty(JsonData["status"].ToString()) ? "" : JsonData["status"].ToString();
            if (!string.IsNullOrEmpty(status) && status == "0")
            {
                pnlDetails.Visible = true;

                if (status == "0")
                {
                    if (StudDetails.Tables[0].Rows.Count > 0)
                    {

                        lblEnrollNo.Text = dt.Rows[0]["ENROLLNMENTNO"].ToString();
                        lblSem.Text = dt.Rows[0]["SEMESTERNO"].ToString();
                        lblyear.Text = dt.Rows[0]["YEAR"].ToString();
                        lblsessionName.Text = dt.Rows[0]["SESSION_NAME"].ToString();
                        lblRecieptType.Text = dt.Rows[0]["RECIEPT_CODE"].ToString();
                        ViewState["RECIEPTCODE"] = dt.Rows[0]["RECIEPT_CODE"].ToString();
                        Label_MerchTxnRef.Text = JsonData["reference_no"].ToString();
                        Label_OrderInfo.Text = JsonData["order_no"].ToString();
                        lblname.Text = JsonData["order_bill_name"].ToString();
                        Label_Amount.Text = JsonData["order_amt"].ToString();
                        lblOrderSatus.Text = JsonData["order_status"].ToString();
                        Label_Message.Text = JsonData["order_bank_response"].ToString();     //"SUCCESS" OR "Txn Success" OR  "Successfuly"
                        string DCRCount = objCommon.LookUp("ACD_DCR", "COUNT (IDNO)", "TRANSACTIONID= " + JsonData["reference_no"].ToString());
                        if ((Label_Message.Text == "SUCCESS" || lblOrderSatus.Text == "Shipped") && DCRCount == "0")
                        {

                            btnManage.Visible = true;
                            ViewState["Status"] = JsonData["order_status"].ToString();      // "Shipped" OR "Initiated" OR  "Approved"                              
                        }
                        else
                        {
                            string Receiptno = objCommon.LookUp("ACD_DCR", "REC_NO", "TRANSACTIONID= " + JsonData["reference_no"].ToString());
                            lblrecno.Text = Receiptno;
                            Label_Message.Text = "Already Managed";
                            btnManage.Visible = false;
                        }

                        lbldate.Text = JsonData["order_date_time"].ToString();
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
                Label_MerchTxnRef.Text = "NA"; //JsonData["reference_no"].ToString();
                Label_OrderInfo.Text = "NA";//JsonData["order_no"].ToString();
                lblname.Text = dt.Rows[0]["NAME"].ToString();
                //JsonData["order_bill_name"].ToString();
                Label_Amount.Text = "NA";
                lblOrderSatus.Text = "NA";
                Label_Message.Text = "NA";     //"SUCCESS" OR "Txn Success" OR  "Successfuly"
                Label_Message.Text = "NA";
                lbldate.Text = "NA";
                lblrecno.Text = "NA";
                pnlDetails.Visible = true;
                btnManage.Visible = false;
                //JsonData["order_date_time"].ToString();
                //decResponse = status + "|" + enc_response + "|" + enc_error_code;
                ////objCommon.DisplayMessage(this.updServertoServer, "failure response from ccAvenues: " + decResponse, this.Page);
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public class StatusEncRequest 
    {
        public string reference_no { get; set; }
        public string order_no { get; set; }
    }
    // end

}