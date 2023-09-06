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
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class ACADEMIC_ONLINEFEECOLLECTION_PayUDoubleVerification : System.Web.UI.Page
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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelection.Visible = true;
                pnlDetails.Visible = false;
                //objCommon.FillDropDownList(ddlActivityName, "ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME", "ACTIVITYNO > 0", "ACTIVITYNO");
                DataSet dsFill = objCommon.Populate_PaymentActivity();
                ddlActivityName.DataSource = dsFill;
                ddlActivityName.DataTextField = "ACTIVITYNAME";
                ddlActivityName.DataValueField = "ACTIVITYNO";
                ddlActivityName.DataBind();
                //DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails((5), 0, 1);
                //if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                //{
                //    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                //    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                //    string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                //    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                //    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                //    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                //    ViewState["ResponseUrl"] = ResponseUrl;
                //    ViewState["RequestUrl"] = RequestUrl;
                //    ViewState["merchentkey"] = merchentkey;
                //    ViewState["saltkey"] = saltkey;
                //    ViewState["accesscode"] = accesscode;
                //    ViewState["hashsequence"] = hashsequence;
                //}

            }
        }
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (VerifyPayment())
            //    Response.Write("<h2>Payment Verified...</h2><br />");
            //else
            //    Response.Write("<h2>Payment Verification Failed...</h2><br />");
            if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_RCPIPER(Convert.ToInt32(Session["OrgId"]), 1, Convert.ToInt32(ddlActivityName.SelectedValue), txtOrderId.Text);
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    ViewState["ResponseUrl"] = ResponseUrl;
                    ViewState["RequestUrl"] = RequestUrl;
                    ViewState["merchentkey"] = merchentkey;
                    ViewState["saltkey"] = saltkey;
                    ViewState["accesscode"] = accesscode;
                    ViewState["hashsequence"] = hashsequence;
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "OrderID is Invalid Please Enter Valid OrderID", this.Page);
                    return;
                }
            }
            divFooter.Visible = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            //string Url = "https://test.payu.in/merchant/postservice.php?form=2";
            string Url = "https://info.payu.in/merchant/postservice.php?form=2";

            string method = "verify_payment";
            string salt = Convert.ToString(ViewState["saltkey"]);      // ConfigurationManager.AppSettings["SALT2"];   // Salt Value
            string key = Convert.ToString(ViewState["merchentkey"]);   //ConfigurationManager.AppSettings["MERCHANT_KEY2"];      // Merchant key
            string var1 = txtOrderId.Text;  // Transaction id        "1cfca4b43ebcad84fdf7";


            string toHash = key + "|" + method + "|" + var1 + "|" + salt;

            string Hashed = Generatehash512(toHash);

            string postString = "key=" + key +
                "&command=" + method +
                "&hash=" + Hashed +
                "&var1=" + var1;

            WebRequest myWebRequest = WebRequest.Create(Url);
            myWebRequest.Method = "POST";
            myWebRequest.ContentType = "application/x-www-form-urlencoded";
            myWebRequest.Timeout = 180000;
            StreamWriter requestWriter = new StreamWriter(myWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            StreamReader responseReader = new StreamReader(myWebRequest.GetResponse().GetResponseStream());
            WebResponse myWebResponse = myWebRequest.GetResponse();
            Stream ReceiveStream = myWebResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(ReceiveStream, encode);

            string response = readStream.ReadToEnd();
            JObject account = JObject.Parse(response);
            String status = (string)account.SelectToken("transaction_details." + var1 + ".status");

            String idno = (string)account.SelectToken("transaction_details." + var1 + ".productinfo");
            ViewState["IDNO"] = idno;
            String firstname = (string)account.SelectToken("transaction_details." + var1 + ".firstname");
            String transaction_amount = (string)account.SelectToken("transaction_details." + var1 + ".transaction_amount");
            String txnid = (string)account.SelectToken("transaction_details." + var1 + ".txnid");
            String additional_charges = (string)account.SelectToken("transaction_details." + var1 + ".additional_charges");
            String mihpayid = (string)account.SelectToken("transaction_details." + var1 + ".mihpayid");
            String addedon = (string)account.SelectToken("transaction_details." + var1 + ".addedon");
            String card_type = (string)account.SelectToken("transaction_details." + var1 + ".card_type");
            String card_no = (string)account.SelectToken("transaction_details." + var1 + ".card_no");
            String name_on_card = (string)account.SelectToken("transaction_details." + var1 + ".name_on_card");
            String unmappedstatus = (string)account.SelectToken("transaction_details." + var1 + ".unmappedstatus");
            String amt = (string)account.SelectToken("transaction_details." + var1 + ".amt");
            String trandate = (string)account.SelectToken("transaction_details." + var1 + ".addedon");
            if (idno == null)
            {
                btnManage.Visible = false;
                btnhomered.Visible = false;
                objCommon.DisplayMessage(this.Page, "OrderID is Invalid Please Enter Valid OrderID", this.Page);
            }
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= " + idno);

            pnlDetails.Visible = true;
            lblname.Text = firstname;
            lblEnrollNo.Text = Enrollno;
            ViewState["EnrollemtID"] = Enrollno;
            Label_OrderInfo.Text = txnid;
            ViewState["Orderid"] = Label_OrderInfo.Text;
            Label_MerchTxnRef.Text = mihpayid;
            ViewState["Merchantid"] = Label_MerchTxnRef.Text;
            lbldate.Text = trandate;
            Label_Amount.Text = amt;
            ViewState["Amount"] = Label_Amount.Text;
            Label_Message.Text = status;
            ViewState["Status"] = status;
            DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_RECIEPT_TYPE RT ON(D.RECIEPT_CODE = RT.RECIEPT_CODE) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,D.RECIEPT_CODE,RECIEPT_TITLE,D.REC_NO", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lblsessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblyear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_TITLE"].ToString();
                lblrecno.Text = ds.Tables[0].Rows[0]["REC_NO"].ToString();
                lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                lblrecno.Text = ds.Tables[0].Rows[0]["REC_NO"].ToString();
            }

            txtTransactionId.Text = mihpayid;
            //int OrgID=3;
            // Session["OrgId"]  =  OrgID;
            if (status == "success")
            {
                string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + txnid + "'");
                ViewState["reccode"] = rec_code;
               // int result = objFees.OnlineInstallmentFeesPayment(mihpayid, txnid, amt, 1, "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
               // int output = objFees.InsertOnlinePayment_DCR(Convert.ToString(idno), rec_code, Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno, "",Convert.ToInt32(Session["OrgId"]));
                
            }

            if (status != "success")
            {
                btnManage.Visible = false;
                btnhomered.Visible = false;
            }
            else
            {
                btnManage.Visible = true;
                btnhomered.Visible = true;
            }



        }
        catch (Exception exp)
        {
            //Response.Write("Exception " + exp);

        }
    }

    public Boolean VerifyPayment()
    {
        string command = "verify_payment";
        string hashstr = Convert.ToString(ViewState["merchentkey"]) + "|" + command + "|" + txtOrderId.Text + "|" + Convert.ToString(ViewState["saltkey"]);

        string hash = Generatehash512(hashstr);

        ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var request = (HttpWebRequest)WebRequest.Create("https://info.payu.in/merchant/postservice.php?form=2");

        var postData = "key=" + Uri.EscapeDataString(Convert.ToString(ViewState["merchentkey"]));
        postData += "&hash=" + Uri.EscapeDataString(hash);
        postData += "&var1=" + Uri.EscapeDataString(txtOrderId.Text);
        postData += "&command=" + Uri.EscapeDataString(command);
        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


      
        //JObject json = JObject.Parse(responseString);
        //var api_status = json.Value<String>("status");
        //if (Convert.ToInt32(api_status) != -1)
        //{
        //    var result = json.Value<JArray>("transaction_details");
        //    for (var i = 0; i < result.Count; i++)
        //    {
        //        JObject jObject = result.Value<JObject>(i);
        //        var postBackParam = jObject.Value<JObject>("postBackParam");
        //        String status = postBackParam.Value<String>("status");
        //        String idno = postBackParam.Value<String>("productinfo");
        //        String firstname = postBackParam.Value<String>("firstname");
        //        String transaction_amount = postBackParam.Value<String>("amount");
        //        String txnid = postBackParam.Value<String>("txnid");
        //    }
        //}
        //JObject json = JObject.Parse(responseString);
        //string rString = responseString.ToString();
        //string[] authorsList = rString.Split(new string[] {"transaction_details"}, StringSplitOptions.None);
        //string json2 = authorsList[1].ToString();
        //json2 = json2.Substring(2, json2.Length - 2);
        //json2 = json2.Remove(json2.Length - 1, 1);

        //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseString);
        //TransactionDetails myDeserializedClass1 = JsonConvert.DeserializeObject<TransactionDetails>(json2);
       

        return true;
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


    private string PreparePOSTForm(string url, string data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        strForm.Append("<input type=\"hidden\" name=\"msg" +
                       "\" value=\"" + data + "\">");

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
            string status="";
            status=ViewState["Status"].ToString();
            if (status == "success")
            {
                int output = objFees.InsertOnlinePayment_DCR(ViewState["IDNO"].ToString(), ViewState["reccode"].ToString(), Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", ViewState["EnrollemtID"].ToString(), "");
                if (output == 1 && output != -99)
                {
                    objCommon.DisplayMessage(this.Page, "Success", this.Page);
                    Clear();
                }
                //else if (output == 1)
                //{ 
                    //objCommon.DisplayMessage(this.Page, "Already Exists", this.Page);
                //}
            }

        }
        catch(Exception exception)
        {
            //objCommon.DisplayMessage(Page, "Something Went Wrong", this.Page);
            throw;
        }        
    }
    protected void Clear()
    {
        try
        {
            ddlActivityName.SelectedIndex = 0;
            txtTransactionId.Text = string.Empty;
            pnlDetails.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlActivityName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlActivityName.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Activity Name.", this.Page);
                return;
            }
            DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails(Convert.ToInt32(Session["OrgId"]), 1, Convert.ToInt32(ddlActivityName.SelectedValue));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                ViewState["ResponseUrl"] = ResponseUrl;
                ViewState["RequestUrl"] = RequestUrl;
                ViewState["merchentkey"] = merchentkey;
                ViewState["saltkey"] = saltkey;
                ViewState["accesscode"] = accesscode;
                ViewState["hashsequence"] = hashsequence;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    //}
    }
    protected void btnexcelreport_Click(object sender, EventArgs e)
        {
        try
            {
            GridView GVDayWiseAtt = new GridView();
            StudentController objSC = new StudentController();
            DataSet ds = null;
            ds = objSC.GetDoubleVerificationExceReport();
            if (ds.Tables[0].Rows.Count > 0)


                if (ds.Tables[0].Rows.Count > 0)
                    {

                    GVDayWiseAtt.DataSource = ds;
                    GVDayWiseAtt.DataBind();

                    string attachment = "attachment; filename=DoubleVerificationReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVDayWiseAtt.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                   
                    }
                else
                    {
                    objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
                    }
            }
        catch (Exception ex)
            {
            throw;
            }
        }
}
