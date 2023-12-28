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
using System.Net;
using System.Net.Http;
using TraknPayRequery_Sheduler.Model;

public partial class ACADEMIC_ONLINEFEECOLLECTION_Omni_DoubleVerification : System.Web.UI.Page
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
                   
                }
                pnlSelection.Visible = true;
                pnlDetails.Visible = false;              
                DataSet dsFill = objCommon.Populate_PaymentActivity();
                ddlActivityName.DataSource = dsFill;
                ddlActivityName.DataTextField = "ACTIVITYNAME";
                ddlActivityName.DataValueField = "ACTIVITYNO";
                ddlActivityName.DataBind();
               

            }
        }
        
    }

    public static string Generatehash512(string text)
    {

        byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

        System.Text.UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex.ToUpper();
      
    }


    public static string gethashForPaymentStatus(VMTracknPay vmTracknPay)
    {
        string checksumString = vmTracknPay.salt + "|" + vmTracknPay.api_key + "|" + vmTracknPay.order_id;
        string result = Generatehash512(checksumString);
        return result;
    }


    public static string TrackNPayequeryCall(VMTracknPay vmTrackNpay)
    {
        try
        {
            VMTrackNPayRequeryPost trackNPayRequery = new VMTrackNPayRequeryPost();
            trackNPayRequery.api_key = vmTrackNpay.api_key;
            trackNPayRequery.hash = vmTrackNpay.hash;
            trackNPayRequery.order_id = vmTrackNpay.order_id;
         
            string response = string.Empty;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new HttpClient())
            {

                //string Baseurl = "https://biz.traknpay.in/v2/";         
                  string Baseurl = "https://pgbiz.omniware.in/v2/";               
          
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = client.PostAsJsonAsync<VMTrackNPayRequeryPost>("paymentstatus", trackNPayRequery).Result;
                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        #region Commented by gaurav
        string Reciept_code = string.Empty;
        try
        {
            VMTracknPay vmTracknPay = new VMTracknPay();             
    
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
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "PG configuration not done!!!!!", this.Page);
                        return;
                    }
               
               // vmTracknPay.order_id = "2286982612-720";// "7449832010-369";
               //vmTracknPay.salt = "e52b0ad66a2d284928d4f71afe485ced60f2d528";
               //vmTracknPay.api_key = "96d2b9ce-253e-4351-904c-6ecd255e6083";//merchand id



               vmTracknPay.order_id = txtOrderId.Text.Trim();
               vmTracknPay.salt = ViewState["saltkey"].ToString();
               vmTracknPay.api_key = ViewState["merchentkey"].ToString();
               vmTracknPay.hash = gethashForPaymentStatus(vmTracknPay);

               string str = TrackNPayequeryCall(vmTracknPay);
               VMTrackNPayPaymentStatus PgResponse = JsonConvert.DeserializeObject<VMTrackNPayPaymentStatus>(str);
               if (PgResponse.data != null)
               {
                   // if(PgResponse.data[0].response_message == "SUCCESS")                       
                   //{
                   ViewState["Status"] = PgResponse.data[0].response_message;
                  
                   int Idno = Convert.ToInt32(PgResponse.data[0].description);

                   StudentController objSC = new StudentController();
                   DataSet dsStudent = objSC.GetStudentDetailsExam(Idno);
                   lblname.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                   lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();

                   //                       int SESSIONNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SESSIONNO", "IDNO= " + Idno + " AND ORDER_ID='" + vmTracknPay.order_id.ToString()+"'"));
                   //string Sessionname  = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO= " + SESSIONNO );

                   //                       lblsessionName.Text = Sessionname.ToString();
                   //                       lblSem.Text = objCommon.LookUp("ACD_DCR", "SEMESTERNO", "IDNO= " + Idno + " AND ORDER_ID='" + vmTracknPay.order_id.ToString() + "'");

                   //PgResponse.data[0].transaction_id="";
                   //PgResponse.data[0].authorization_staus="";
                   //PgResponse.data[0].order_id="";
                   //PgResponse.data[0].customer_name="";
                   //PgResponse.data[0].amount_orig="";
                   //PgResponse.data[0].description = "";

                   //  pnlDetails.Visible = true;
                   DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_RECIEPT_TYPE RT ON(D.RECIEPT_CODE = RT.RECIEPT_CODE) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,D.RECIEPT_CODE,RECIEPT_TITLE,D.REC_NO,D.IDNO", "ORDER_ID= '" + vmTracknPay.order_id.ToString() + "'", "ENROLLNMENTNO");
                   if (ds != null && ds.Tables[0].Rows.Count > 0)
                   {
                       //   lblname.Text=
                       lblsessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                       lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                       lblyear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                       lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_TITLE"].ToString();
                       ViewState["reccode"] = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                       lblrecno.Text = ds.Tables[0].Rows[0]["REC_NO"].ToString();
                       Reciept_code = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                       ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                   }

                   //lblEnrollNo.Text = Enrollno.ToString();

                   Label_MerchTxnRef.Text = PgResponse.data[0].transaction_id;
                   Label_OrderInfo.Text = PgResponse.data[0].order_id;
                   lbldate.Text = PgResponse.data[0].payment_datetime;
                   Label_Amount.Text = PgResponse.data[0].amount_orig;
                   Label_Message.Text = PgResponse.data[0].response_message;
                   //lbldate.Text = listStrLineElements[15].ToString();

                   //Label_Message.Text = "Transaction Success";

                   divFooter.Visible = true;
                   pnlDetails.Visible = true;
                   pnlSelection.Visible = true;

                   //  }

                   //"transaction_id":"FGUPIU66136BF396A","bank_code":"UPIU","payment_mode":"UPI","payment_channel":"Unified Payments Interface","payment_datetime":"2023-12-26 10:55:20","response_code":0,"response_message":"SUCCESS","authorization_staus":"captured","order_id":"2286982612-720","amount":"3286.80","amount_orig":"3275.00","tdr_amount":10,"tax_on_tdr_amount":1.8,"description":"377","error_desc":null,"customer_phone":"8698663827","customer_name":"KADBE SHAAN SANJAY","customer_email":"shaankadbe18@gmail.com","currency":"INR","cardmasked":null,"udf1":"19","udf2":"1","udf3":"1","udf4":"0","udf5":null}],"hash":"359D1DC019AE898A0F7A12940D7463B3144F5537576BBB167943A9C49FCD0E52151619512F116806F819FD10D32DB56D4271D9684408C24D99C8C7739090A502"}


               }
               else
               {
                   objCommon.DisplayMessage(this.Page, "OrderID is Invalid Please Enter Valid OrderID", this.Page);
                   divFooter.Visible = false;
                   pnlDetails.Visible = false;
                  
                   return;
               
               }




            #endregion
        }

        catch (Exception ex)
        { 
}
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
            status = ViewState["Status"].ToString();
           // PgResponse.data[0].response_message == "SUCCESS"
            if (status == "SUCCESS")
            {
                //int output = objFees.InsertOnlinePayment_DCR(ViewState["IDNO"].ToString(), ViewState["reccode"].ToString(), Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", ViewState["EnrollemtID"].ToString(), "");

                int output = objFees.InsertOnlinePayment_DCR(ViewState["IDNO"].ToString(), ViewState["reccode"].ToString(), Label_OrderInfo.Text, Label_MerchTxnRef.Text, "O", string.Empty, Label_Amount.Text, "Success", lblEnrollNo.Text.ToString(), "");
                if (output == 1 && output != -99)
                {
                    objCommon.DisplayMessage(this.Page, "Success", this.Page);
                    Clear();
                    divFooter.Visible = false;
                }
                else if (output == 1)
                {
                    objCommon.DisplayMessage(this.Page, "Already Exists", this.Page);
                    divFooter.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Transaction FAILED", this.Page);
                divFooter.Visible = false;
            }
        }
        catch(Exception exception)
        {
            objCommon.DisplayMessage(Page, "Already Existss", this.Page);

            //throw;
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
            //DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails(Convert.ToInt32(Session["OrgId"]), 1, Convert.ToInt32(ddlActivityName.SelectedValue));
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
