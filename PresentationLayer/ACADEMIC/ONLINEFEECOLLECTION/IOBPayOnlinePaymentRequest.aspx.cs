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
using System.IO;
using System.Runtime.Serialization.Json;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class IOBPayOnlinePaymentRequest : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    OrganizationController objOrg = new OrganizationController();

    string hash_seq = string.Empty;
    #endregion

    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    public string txnid1 = string.Empty;
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string tokenid = string.Empty;
    int degreeno = 0;
    int college_id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
              
                //SqlDataReader dr = objCommon.GetCommonDetails();

                //if (dr != null)
                //{
                //    if (dr.Read())
                //    {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //    }
                //}

                DataSet Orgds = null;
                var OrgId = objCommon.LookUp("REFF", "OrganizationId", "");
                Orgds = objOrg.GetOrganizationById(Convert.ToInt32(OrgId));
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


                //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.

                //if (Session["OrgId"].ToString() == "6")
                //{
                //    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                //}
                //if (Session["OrgId"].ToString() == "8")
                //{
                //    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                //}

                //**********************************End by Nikhil L.********************************************//


                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["payStudName"]);
                lblBranch.Text = Convert.ToString(Session["Branchname"]);
                //firstname.Text = Convert.ToString(Session["payStudName"]);

                lblSemester.Text = Convert.ToString(Session["paysemester"]);
               // email.Text = Convert.ToString(Session["studEmail"]);
               // phone.Text = Convert.ToString(Session["studPhone"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int ConfigID = Convert.ToInt32(Session["ConfigID"]);
                lblYear.Text = Session["YEARNO"].ToString();

                DataSet ds1 = objFees.GetOnlinePaymentConfigurationAllDetailsV2(ConfigID);
                
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    lblActivityName.Text = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();

                    Session["SubMerchant_id"] = ds1.Tables[0].Rows[0]["SUBMERCHANT_ID"].ToString();
                    Session["BankFee_Type"] = ds1.Tables[0].Rows[0]["BANKFEE_TYPE"].ToString();
                    Session["ResponseUrl"] = ResponseUrl;
                    Session["RequestUrl"]  = RequestUrl;
                    Session["merchentkey"] = merchentkey;
                    Session["saltkey"]     = saltkey;
                    Session["accesscode"] = accesscode;
                    Session["hashsequence"] = hashsequence;
                    Session["Instance"] = ds1.Tables[0].Rows[0]["INSTANCE"].ToString();

                    //key.Value = merchentkey;
                    ////hash.Value = hashsequence;
                    //surl.Text = ResponseUrl;
                    //furl.Text = ResponseUrl;
                    //productinfo.Text = Convert.ToString(Session["idno"]);
                    //udf1.Text = Convert.ToString(Session["OrgId"]);
                    //udf2.Text = payId.ToString();
                    //udf3.Text = Convert.ToString(Session["payactivityno"]);
                    ////udf4.Text = Convert.ToString(Session["Installmentno"]);
                    //udf4.Text = Convert.ToString(Session["Installmentno"]);
                }


                BindAndCheckPayDetails();
               
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }


    #region old code not use
    protected void btnPay_Click(object sender, EventArgs e)
    {
        string[] hashVarsSeq;
        string hash_string = string.Empty;

        string UserId = Convert.ToString(Session["userno"]);
        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    Reprocess:
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        Random ram = new Random();
        int i = ram.Next(1, 9);
        int j = ram.Next(21, 51);
        int k = ram.Next(471, 999);
        int l = System.DateTime.Today.Day;
        int m = System.DateTime.Today.Month;
        string txnid1 = (i + "" + j + "" + k + "" + l + "" + m).ToString() + "-" + UserId;
        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + txnid1 + "'");

        Session["OrderId"] = txnid1;
        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }


        int result = 0;
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), "Not Continued", txnid1);

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-");
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }
      
        //result = objFees.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(demand_semester));
        //int orderid = Convert.ToInt32(objStudentFees.OrderID);
     
        string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + txnid1 + "'");
        if (orderid != "" || orderid != string.Empty || orderid == txnid1)
        {

            //Get Fetch initiatePayment Details
            var jsonObj = initiatePayment(txnid1);
           
            var action1 = Session["RequestUrl"].ToString();
            reqjson.Value = jsonObj.ToString();

            #region Old Code

            //if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
            //{

            //    if (
            //        string.IsNullOrEmpty(Session["merchentkey"].ToString()) ||
            //        string.IsNullOrEmpty(txnid1) ||
            //        string.IsNullOrEmpty(Convert.ToString(Session["studAmt"])) ||
            //        string.IsNullOrEmpty(Convert.ToString(Session["payStudName"])) ||
            //        string.IsNullOrEmpty(Convert.ToString(Session["studEmail"])) ||
            //        string.IsNullOrEmpty(Convert.ToString(Session["studPhone"])) ||
            //        string.IsNullOrEmpty(Convert.ToString(Session["idno"])) ||
            //        string.IsNullOrEmpty(Session["ResponseUrl"].ToString()) ||
            //        string.IsNullOrEmpty(Session["ResponseUrl"].ToString()) ||
            //        string.IsNullOrEmpty("payu_biz")

            //        )
            //    {
            //        //error
            //        //frmError.Visible = true;
            //        return;
            //    }
            //    else
            //    {
            //        //frmError.Visible = false;
            //        hashVarsSeq = Session["hashsequence"].ToString().Split('|'); // spliting hash sequence from config
            //        hash_string = "";
            //        foreach (string hash_var in hashVarsSeq)
            //        {
            //            if (hash_var == "key")
            //            {
            //                hash_string = hash_string + Session["merchentkey"].ToString();
            //                hash_string = hash_string + '|';
            //            }
            //            else if (hash_var == "txnid")
            //            {
            //                hash_string = hash_string + txnid1;
            //                hash_string = hash_string + '|';
            //            }
            //            else if (hash_var == "amount")
            //            {
            //                hash_string = hash_string + Convert.ToDecimal(Session["studAmt"]).ToString("g29");
            //                hash_string = hash_string + '|';
            //            }
            //            else
            //            {

            //                hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
            //                hash_string = hash_string + '|';
            //            }
            //        }

            //        hash_string += Session["saltkey"].ToString();// appending SALT

            //        hash1 = Generatehash512(hash_string).ToLower();         //generating hash
            //        action1 = Session["RequestUrl"].ToString() + "/_payment";// setting URL
            //    }
            //}
            //else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            //{
            //    hash1 = Request.Form["hash"];
            //    action1 = Session["RequestUrl"].ToString() + "/_payment";
            //}

            #endregion

            if (jsonObj != null)
            {

                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in hash table for data post
                data.Add("reqjson", jsonObj.ToString());


                //data.Add("txnid", txnid.Value);
                //data.Add("key", key.Value);
                //string amount = Convert.ToString(Session["studAmt"]);
                //string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros
                ////amount.Text = AmountForm;
                //data.Add("amount", AmountForm);
                //data.Add("firstname", Convert.ToString(Session["payStudName"]));
                //data.Add("email", Convert.ToString(Session["studEmail"]));
                //data.Add("phone", Convert.ToString(Session["studPhone"]));
                //data.Add("productinfo", Convert.ToString(Session["idno"]));
                ////data.Add("productinfo", productinfo.Text.Trim());
                //data.Add("surl", Session["ResponseUrl"].ToString());
                //data.Add("furl", Session["ResponseUrl"].ToString());
                //data.Add("service_provider", "iobpay");   // payu_biz


                //if (Request.QueryString["ReceiptType"] != null)
                //string ReceiptType = Request.QueryString["ReceiptType"];
                //data.Add("udf1", udf1.Text.Trim());
                //data.Add("udf2", udf2.Text.Trim());
                //data.Add("udf3", udf3.Text.Trim());
                //data.Add("udf4", udf4.Text.Trim());
                //data.Add("udf5", udf5.Text.Trim());
                //data.Add("udf6", udf6.Text.Trim());

                string strForm = PreparePOSTForm(action1, data);
                Page.Controls.Add(new LiteralControl(strForm));
            }
            //}

            //else
            //{
            //    //no hash
            //}
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm1";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\"  runat=\"server\"  name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        //type=\"hidden\"
        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" runat=\"server\" id=\"reqjson\" name=\"" + key.Key +
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

    #endregion

    #region IOBPay Methods - 23/08/2023

    protected void BindAndCheckPayDetails()
    {
        string[] hashVarsSeq;
        string hash_string = string.Empty;

        string UserId = Convert.ToString(Session["userno"]);
        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    Reprocess:
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        Random ram = new Random();
        int i = ram.Next(1, 9);
        int j = ram.Next(21, 51);
        int k = ram.Next(471, 999);
        int l = System.DateTime.Today.Day;
        int m = System.DateTime.Today.Month;
        string txnid1 = (i + "" + j + "" + k + "" + l + "" + m).ToString() + "-" + UserId;
        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + txnid1 + "'");

        Session["OrderId"] = txnid1;
        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }


        int result = 0;
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), "Not Continued", txnid1);

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-");
        }
        else if (Session["ReceiptType"].ToString() == "PRF" || Session["ReceiptType"].ToString() == "RF" || Session["ReceiptType"].ToString() == "SEF")
        {
            result = objFees.InsertPayment_Log_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Session["semesternos"].ToString(), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }

        string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + txnid1 + "'");

        if (orderid != "" || orderid != string.Empty || orderid == txnid1)
        {
            //Get Fetch initiatePayment Details
            var jsonObj = initiatePayment(txnid1);
            tokenid = Session["tokenid"].ToString();     // pass tokenid and trackid value
            //var action1 = Session["RequestUrl"].ToString();
            reqjson.Value = jsonObj.ToString();

            result = objFees.InsertIOBPayOnlinePaymentlog(Convert.ToInt32(Session["idno"]), Convert.ToDecimal(Session["studAmt"]), txnid1, tokenid, Session["paysemester"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
        }

    }

    public String initiatePayment(string Trxid )
    {
        //System.Diagnostics.Debug.WriteLine("Initiating payment process ....");
        //variable initialisations
        string encryptionkey = Session["accesscode"].ToString();           //ADD ENCRYPTION KEY HERE
        string encryptioniv = Session["hashsequence"].ToString();          //ADD ENCRYPTION IV HERE
        string signkey = Session["saltkey"].ToString();                    //ADD SIGN KEY HERE
        string merchantid = Session["merchentkey"].ToString();             //ADD MERCHANT ID HERE
        string merchantsubid = Session["SubMerchant_id"].ToString();       //ADD MERCHANTSUBID HERE

        string tokenaction = "GENTOK";
        string txninitaction = "TXNINIT";
        string feetype = Session["BankFee_Type"] .ToString();
        string totalamt = Convert.ToString(Session["studAmt"]);
        string LINK = string.Empty;

        if (Convert.ToInt32(Session["Instance"]) == 1)
            {
            LINK = "https://testapp.iobnet.org/iobpay/iobpayRESTService/apitokenservice/generatenewtoken/";
            }
        else if (Convert.ToInt32(Session["Instance"]) == 2)
            {
            LINK = "https://www.iobnet.co.in/iobpay/iobpayRESTService/apitokenservice/generatenewtoken/";
            }
        else
            {
            LINK = "https://www.iobnet.co.in/iobpay/iobpayRESTService/apitokenservice/generatenewtoken/";
            }
        //string replyurl = "http://localhost:8080";
        string replyurl = Session["ResponseUrl"].ToString();   //"http://localhost:8080/ResponseUrl.aspx";    
        string iobpayapiurl = LINK;//"https://testapp.iobnet.org/iobpay/iobpayRESTService/apitokenservice/generatenewtoken/";
        string iobpaytxniniturl = Session["RequestUrl"].ToString();     //"https://testapp.iobnet.org/iobpay/apitxninit.do";
        string merchanttxnid = Trxid;

        //Random random = new Random();
        //int randomNumber = random.Next(0, 1000);
        //merchanttxnid = randomNumber.ToString();

        TokenReq tokenreq = new TokenReq();
        tokenreq.merchantid = merchantid;
        tokenreq.merchantsubid = merchantsubid;
        tokenreq.action = tokenaction;
        tokenreq.feetype = feetype;
        tokenreq.totalamt = totalamt;
        tokenreq.replyurl = replyurl;

        JSonHelper helper = new JSonHelper();
        string jsonResult = helper.ConvertObjectToJSon(tokenreq);

        // Console.WriteLine("token request(before encryption)   : " + jsonResult);

        EncryptHelper encrypthelper = new EncryptHelper();
        string encryptedtokendata = encrypthelper.Encrypt(jsonResult.ToString(), encryptionkey, encryptioniv);
        byte[] byteArrayNEw = Encoding.ASCII.GetBytes(signkey);
        byte[] byteArrayNEw1 = Encoding.ASCII.GetBytes(encryptedtokendata);
        byte[] signedtokenhmac = encrypthelper.HashHMAC(byteArrayNEw, byteArrayNEw1);

        DataExchange dataexchange = new DataExchange();
        StringBuilder sb = new StringBuilder(signedtokenhmac.Length * 2);
        foreach (byte ba in signedtokenhmac)
        {
            sb.AppendFormat("{0:x2}", ba);
        }
        string Value = sb.ToString().ToUpper();
        dataexchange.merchantid = merchantid;
        dataexchange.merchantsubid = merchantsubid;
        dataexchange.action = tokenaction;
        dataexchange.data = encryptedtokendata;
        dataexchange.hmac = Value.ToString();

        string jsonResultNew = helper.ConvertObjectToJSon(dataexchange);

        // System.Diagnostics.Debug.WriteLine("token request(after encryption)   : " + jsonResultNew);

        byte[] byteArray = Encoding.ASCII.GetBytes(jsonResultNew);

        //
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(iobpayapiurl);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;
        request.AllowAutoRedirect = true;
        request.Proxy.Credentials = CredentialCache.DefaultCredentials;
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        dataStream = response.GetResponseStream();
        StringBuilder Sb = new StringBuilder();
        using (var sr = new StreamReader(dataStream, Encoding.UTF8))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Sb.Append(line);
            }
        }
        //System.Diagnostics.Debug.WriteLine("token response(before decryption)   : " + Sb.ToString());
        string OrginalValue = string.Empty;
        TokenRes PS = new TokenRes();

        PS = helper.ConvertJSonToObject<TokenRes>(Sb.ToString());

        //System.Diagnostics.Debug.WriteLine("token response(before decryption) :: data       " + PS.data);
        encryptedtokendata = encrypthelper.Decrypt(PS.data, encryptionkey, encryptioniv);
        //System.Diagnostics.Debug.WriteLine("token response(after decryption) :: data " + encryptedtokendata);

        TokenResDecrypted tokenresdecryptedobj = helper.ConvertJSonToObject<TokenResDecrypted>(encryptedtokendata);
        // System.Diagnostics.Debug.WriteLine("tokenid :: " + tokenresdecryptedobj.tokenid);

        //--Start for txn init service
        TxnInitReq txninitreq = new TxnInitReq();
        txninitreq.merchantid = merchantid;
        txninitreq.merchantsubid = merchantsubid;
        txninitreq.action = txninitaction;
        txninitreq.feetype = feetype;
        txninitreq.totalamt = totalamt;
        txninitreq.tokenid = tokenresdecryptedobj.tokenid;              // check whether token id is received and pass the parameter here if token generation is successful
                                                                        //txninitreq.merchanttxnid = "000000001"; // unique merchant txn id
        txninitreq.merchanttxnid = merchanttxnid;
        txninitreq.udf1 = Session["idno"].ToString();                  // user defined field 1
        txninitreq.udf2 = Session["Installmentno"].ToString();           // user defined field 2
        txninitreq.udf3 = "";// Session["Installmentno"].ToString(); // user defined field 3

        Session["tokenid"] = txninitreq.tokenid.ToString();
        string txninitjson = helper.ConvertObjectToJSon(txninitreq);
        //System.Diagnostics.Debug.WriteLine("txninit request(before encryption)   : " + txninitjson);

        string encryptedtxninit = encrypthelper.Encrypt(txninitjson.ToString(), encryptionkey, encryptioniv);
        byte[] byteArrayencryptedtxninit = Encoding.ASCII.GetBytes(encryptedtxninit);
        byte[] txninithmac = encrypthelper.HashHMAC(byteArrayNEw, byteArrayencryptedtxninit);

        StringBuilder sb1 = new StringBuilder(txninithmac.Length * 2);
        foreach (byte ba in txninithmac)
        {
            sb1.AppendFormat("{0:x2}", ba);
        }
        string txninithmachexvalue = sb1.ToString().ToUpper();
        DataExchange txninitdataexchange = new DataExchange();
        txninitdataexchange.merchantid = merchantid;
        txninitdataexchange.merchantsubid = merchantsubid;
        txninitdataexchange.action = txninitaction;
        txninitdataexchange.data = encryptedtxninit;
        txninitdataexchange.hmac = txninithmachexvalue;

        string txninitfinaljson = helper.ConvertObjectToJSon(txninitdataexchange);
        //System.Diagnostics.Debug.WriteLine("txninit request(after encryption)   : " + txninitfinaljson);
        //pass the value of this txninitfinaljson to a form at client/browser and then submit this value as reqjson parameter to iobpaytxniniturl using POST method
        return txninitfinaljson;

    }

    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        public T ConvertJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }

    public class EncryptHelper
    {
        public RijndaelManaged GetRijndaelManaged(byte[] secretKey, String iv)
        {
            //  var keyBytes = new byte[32];
            //   var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            //   Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                BlockSize = 128,
                Key = secretKey,
                IV = Encoding.UTF8.GetBytes(iv)
            };
        }

        public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        public String Encrypt(String plainText, String key, String iv)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var keybytes = getHashSha256(key);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(keybytes, iv)));
        }


        public String Decrypt(String encryptedText, String key, String iv)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var keybytes = getHashSha256(key);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(keybytes, iv)));
        }

        public byte[] getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash;
        }

        public byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);

        }
    }

    #endregion

    #region Method
    public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    {
        try
        {
            //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
            //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            //Img.ContentId = "MyImage";   

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
            //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
            string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
            string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "NIT GOA");
            msg.To.Add(new MailAddress(ToID));

            msg.Subject = "Your transaction with MAKAUT";

            const string EmailTemplate = "<html><body>" +
                                     "<div align=\"left\">" +
                                     "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                      "<tr>" +
                                      "<td>" + "</tr>" +
                                      "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</div>" +
                                     "</body></html>";
            StringBuilder mailBody = new StringBuilder();
            //mailBody.AppendFormat("<h1>Greating !!</h1>");
            mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody5);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg1);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg2);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody4);
            mailBody.AppendFormat("<br />");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            msg.IsBodyHtml = true;
            msg.Body = nMailbody;

            smtp.Host = "smtp.gmail.com";

            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
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
    #endregion









}