//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EXAMINATION
// PAGE NAME     : BILLDESK DOUBLE VERIFICATION FOR CONVOCATION                                                   
// CREATION DATE : 29-02-2024                                                          
// CREATED BY    : SACHIN A                          
// MODIFIED DATE :                                         
// MODIFIED DESC : ONLINE PAYMENT RECONCILE FOR CONVOCATION REGISTERED STUDENT                                                                 
//======================================================================================

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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DynamicAL_v2;
 

public partial class ACADEMIC_BulkBilldeskDoubleVerificationConvocation : System.Web.UI.Page
{
    Common objCommon = new Common();
    FeeCollectionController objFees = new FeeCollectionController();
    //DynamicAL_v2.DynamicControllerAL AL = new DynamicAL_v2.DynamicControllerAL();
    string TranDate = "";
    string TranDateTime = "";

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
                DataSet dsFill = objCommon.Populate_PaymentActivity();
                ddlActivityName.DataSource = dsFill;
                ddlActivityName.DataTextField = "ACTIVITYNAME";
                ddlActivityName.DataValueField = "ACTIVITYNO";
                ddlActivityName.DataBind();
                objCommon.FillDropDownList(ddlCode, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE<>''", "RECIEPT_CODE");
            }
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        FeeCollectionController objFees = new FeeCollectionController();
        //pnlSelection.Visible = false;
        pnlDetails.Visible = true;

        string SP_Name = "PKG_SP_GET_CONVOCATION_DETAILS";
        string SP_Parameters = "@P_ENROLLNO";
        string Call_Values = "" + txtprnno.Text.Trim().ToString() + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
         
        // DataSet ds = objFees.GetConvocationDetails(txtprnno.Text.Trim().ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblEnrollNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            lblbranch.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            lbldegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
            bind_listview();
        }
    }

    protected void bind_listview()
    {
        //pnlSelection.Visible = false;
        pnlDetails.Visible = true;

        string SP_Name = "PKG_SP_GETDATA_CONVOCATION_DETAILS";
        string SP_Parameters = "@P_ENROLLNO, @P_RECIEPT_CODE";
        string Call_Values = "" + txtprnno.Text.Trim().ToString() + "," + ddlCode.SelectedValue + "";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
       // DataSet ds = objFees.GetAllBULKConvoDetails(txtprnno.Text.Trim().ToString(), ddlCode.SelectedValue);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvlist.DataSource = ds;
            lvlist.DataBind();
            //pnlListView.Visible = true;
            lvlist.Visible = true;
        }
        else
        {
            lvlist.DataSource = null;
            lvlist.DataBind();
            lvlist.Visible = false;
        }
    }
    protected void btnManage_Click(object sender, EventArgs e)
    {
        try
        {
            //return;
            Button myButton = (Button)sender;
            String ORDERID = (myButton.CommandArgument.ToString());
            // String ORDERID = ((sender as Button).CommandArgument);
            //String data = Convert.ToString("0122|BDSKUATY|" + txtOrderId.Text + "|"+ DateTime.Now.ToString("yyyyMMddHHmmss") + "|455057528");
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

                //this.doubleVerification();
                //return;
                String data = Convert.ToString("0122|" + merchentkey + "|" + ORDERID + "|" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                String commonkey = Convert.ToString(saltkey);
                int idno = Convert.ToInt32((myButton.CommandName != string.Empty ? Double.Parse(myButton.CommandName) : 0));
                String hash = String.Empty;
                hash = GetHMACSHA256(data, commonkey);
                data = data + "|" + hash.ToUpper();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 1000;

                WebRequest request = WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController");
                byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + data);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();
                Console.Write(buffer.Length);
                Console.Write(request.ContentLength);
                WebResponse myResponse = request.GetResponse();
                string result = "";
                StreamReader reader = new StreamReader(myResponse.GetResponseStream());

                result = reader.ReadToEnd();
                //Label1.Text = result;
                //Console.WriteLine(result);
                // divFooter.Visible = true;
                ViewState["result"] = result;
                ViewState["idno"] = idno;
                ViewState["ORDERID"] = ORDERID;


                //txnLabel.Text = "1212131";
                //lblResponse.Text = data;
                //pnlSelection.Visible = false;
                //pnlDetails.Visible = false;
                //List<String> listStrLineElements;
                //listStrLineElements = data.Split('|').ToList();
                //string status = listStrLineElements[15].ToString();
                //string refundstatus = listStrLineElements[27].ToString();
                //txnLabel.Text = listStrLineElements[1].ToString();
                //lblAmount.Text = listStrLineElements[3].ToString();
                //lblStatus.Text = listStrLineElements[15].ToString(); 
                FillSuccessString(result, idno, ORDERID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showPopup();", true);
                pnlDetails.Visible = true;
                pnlSelection.Visible = true;
            } 
        } 
        catch (Exception exp)
        {
            //Response.Write("Exception " + exp); 
        } 
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

    public void FillSuccessString(String data, int idno, string ORDERID)
    { 
        lblResponse.Text = data;
        pnlSelection.Visible = false;
        pnlDetails.Visible = false;
        List<String> listStrLineElements;
        listStrLineElements = data.Split('|').ToList();
        string status = listStrLineElements[15].ToString();
        string refundstatus = listStrLineElements[27].ToString();
        txnLabel.Text = listStrLineElements[3].ToString();
        lblAmount.Text = listStrLineElements[5].ToString();
        TranDateTime = listStrLineElements[14].ToString();
        TranDate = TranDateTime.Split(' ')[0];
        ViewState["TranDate"] = TranDateTime.Split(' ')[0]; 

        ViewState["ApTransactionid"] = listStrLineElements[3].ToString();         //APTRANSACTIONID  
        ViewState["Transactionid"] = listStrLineElements[4].ToString();           //Trnasaction id 

        if (status == "0300" && refundstatus == "NA")
        {
            lblStatus.Text = "Success";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            btnSubmit.Visible = true;
        }
        else
        {
            lblStatus.Text = "Failed";
            btnSubmit.Visible = false;      
            lblStatus.ForeColor = System.Drawing.Color.Red;
        } 
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        { 
            if (txnLabel.Text == "NA" || lblAmount.Text == "NA" || lblStatus.Text == "NA")
            {
                objCommon.DisplayMessage(this.Page, "Failed to manage", this.Page);
                 return;
            }
            string Enrollno = string.Empty;
            Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= '" + Convert.ToString(ViewState["idno"]) + "'");
            lblEnrollNo.Text = Enrollno.ToString();
            string Reciept_code = string.Empty;

            //string lblamount = objCommon.LookUp("ACD_DCR_TEMP", "TOTAL_AMT", "ORDER_ID= '" + ViewState["ORDERID"].ToString() + "'");
            DataSet ds = objCommon.FillDropDown("ACD_CONVOCATION_PAYEMENT_LOG D", "UA_NO", "D.RECEIPT_CODE", "ORDERID= '" + ViewState["ORDERID"].ToString() + "'", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Reciept_code = ds.Tables[0].Rows[0]["RECEIPT_CODE"].ToString();
                ViewState["reccode"] = Reciept_code;
            }
            {
                int output = 0;
                 
                string inputDate = Convert.ToString(ViewState["TranDate"]);  
                DateTime parsedDate = DateTime.ParseExact(inputDate, "dd-MM-yyyy", null);  // Parse the input date using the specified format 
                string outputDate = parsedDate.ToString("yyyy-MM-dd");                // Format the parsed date to the desired output format
 
                string SP_Name = "PKG_ACAD_CONVOCATION_AUTO_UPDATED_TRANSACTION";
                string SP_Parameters = "@P_ORDER_ID, @P_IDNO, @P_ISVALID, @P_RECEIPT_CODE, @P_TRANSACTION_DATE, @P_TRANSID, @P_PAY_STATUS, @P_MESSAGE,@P_APTRANSACTIONID,@P_OUTPUT";
                string Call_Values = "" + ViewState["ORDERID"].ToString() + "," + Convert.ToInt32(ViewState["idno"]) + ",1," + Reciept_code + "," + outputDate + "," + Convert.ToString(ViewState["Transactionid"]) + ",1," + Convert.ToString(ViewState["result"]) + "," + Convert.ToString(ViewState["ApTransactionid"]) + ",0";
                  
                string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true); 
               // string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 2);
               
                if (output == -99)
                {
                    objCommon.DisplayMessage(this.Page, "Failed to manage", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Payment Manage Successful", this.Page);
                    ClearPop();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ClearPop()
    {
        txnLabel.Text = string.Empty;
        lblAmount.Text = string.Empty;
        lblStatus.Text = string.Empty;
        pnlDetails.Visible = false;
        pnlSelection.Visible = true;
        ddlActivityName.SelectedIndex = 0;
        txtprnno.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closePopup();", true);
    }
    
}