using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;

public partial class ccavRequest : System.Web.UI.Page
{
    CCACrypto ccaCrypto = new CCACrypto();
    string workingKey = "598E8D2E71937502A7BF3A96D4E06E95";//put in the 32bit alpha numeric key in the quotes provided here
    string ccaRequest = "";
    public string strEncRequest = "";

    public string strAccessCode = "AVTH03IB10BN75HTNB";// put the access key in the quotes provided here.
    private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["studName"] != null)
        {
            string Idno = Session["idno"].ToString();
            string amount = Session["studAmt"].ToString();   //session amount
            string studName = Session["studName"].ToString(); //student Name
            string studPhone = Session["studPhone"].ToString(); //student Phone
            string studEmail = Session["studEmail"].ToString(); //student Phone
            string refno = Session["studrefno"].ToString();  // unique number for every transaction i.e order id
            string homelink = Session["homelink"].ToString();     //Url for payment indentification       
            string userno = Session["userno"].ToString();
            System.Collections.Hashtable data = new System.Collections.Hashtable();

            // Compulsory information
            data.Add("tid", "");
            data.Add("merchant_id", "338483");  //account id
            data.Add("order_id", refno);
            data.Add("amount", amount);
            data.Add("currency", "INR");

            int status = Convert.ToInt32(Session["admission"]);
            if (status == 1)
            {
                data.Add("redirect_url", "https://makauttest.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavAdmissionResponse.aspx");   //Responsne Url Local
                data.Add("cancel_url", "https://makauttest.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavAdmissionResponse.aspx");	   //Responsne Url Local
                //data.Add("redirect_url", "http://localhost:63344/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavAdmissionResponse.aspx");   //Responsne Url Local
                //data.Add("cancel_url", "http://localhost:63344/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavAdmissionResponse.aspx");	   //Responsne Url Local
                //data.Add("redirect_url", "https://makaut.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavAdmissionResponse.aspx");   //Responsne Url Live
                //data.Add("cancel_url", "https://makaut.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavAdmissionResponse.aspx");	   //Responsne Url Live
            }
            else
            {
                data.Add("redirect_url", "https://makauttest.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavResponse.aspx");   //Responsne Url Local
                data.Add("cancel_url", "https://makauttest.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavResponse.aspx");	   //Responsne Url Local
                //data.Add("redirect_url", "http://localhost:63344/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavResponse.aspx");   //Responsne Url Local
                //data.Add("cancel_url", "http://localhost:63344/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavResponse.aspx");	   //Responsne Url Local
                //data.Add("redirect_url", "https://makaut.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavResponse.aspx");   //Responsne Url Live
                //data.Add("cancel_url", "https://makaut.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavResponse.aspx");	   //Responsne Url Live
            }
            //Billing information(optional):
            data.Add("billing_name", studName);
            data.Add("billing_address", "Ranchi");
            data.Add("billing_city", "Ranchi");
            data.Add("billing_state", "Jharkhand");
            data.Add("billing_zip", "835103");
            data.Add("billing_country", "India");
            data.Add("billing_tel", studPhone);
            data.Add("billing_email", studEmail);

            //Shipping information(optional):
            data.Add("delivery_name", studName);
            data.Add("delivery_address", "Ranchi");
            data.Add("delivery_city", "Ranchi");
            data.Add("delivery_state", "Jharkhand");
            data.Add("delivery_zip", "835103");
            data.Add("delivery_country", "India");
            data.Add("delivery_tel", studPhone);
           

            //additional Info.
            data.Add("merchant_param1", studEmail);    //payment for which course
            data.Add("merchant_param2", studName);	//paramets for payment indentification
            data.Add("merchant_param3", "FEES COLLECTION");
            data.Add("merchant_param4", studPhone);
            data.Add("merchant_param5", Idno);
            //data.Add("promo_code", "");
            //data.Add("customer_identifier", userno);
            data.Add("offer_type", userno);
            data.Add("offer_code", userno);
            data.Add("eci_value", userno);
            data.Add("billing_notes", userno);

            foreach (System.Collections.DictionaryEntry key in data)
            {
                string name = key.Key.ToString();

                if (key.Key != null)
                {
                    if (!name.StartsWith("_"))
                    {
                        ccaRequest = ccaRequest + key.Key + "=" + key.Value + "&";
                    }
                }

            }

            strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);

        }
        else
        {
            Response.Write("Session is NULL");
        }
    }
}