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
            //String data = Convert.ToString("0122|BSABDURCOE|" + ORDERID + "|" + DateTime.Now.ToString("yyyyMMddHHmmss"));



            // int idno = Convert.ToInt32((myButton.CommandName != string.Empty ? Double.Parse(myButton.CommandName) : 0));
            // String hash = String.Empty;

            // hash = GetHMACSHA256(data, commonkey);

            // data = data + "|" + hash.ToUpper();
            // //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+data+"');", true);
            // //string strForm = PreparePOSTForm("https://www.billdesk.com/pgidsk/PGIQueryController", data);
            // //Page.Controls.Add(new LiteralControl(strForm));

            // ServicePointManager.Expect100Continue = true;
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // ServicePointManager.DefaultConnectionLimit = 1000;

            // //var request = (HttpWebRequest)WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController?");
            // //request.KeepAlive = false;
            // //request.Timeout = 20000;
            // //request.ProtocolVersion = HttpVersion.Version10;
            // //request.ServicePoint.Expect100Continue = false;
            // //var postData = "msg=" + data;

            // //var data1 = Encoding.ASCII.GetBytes(postData);

            // //request.Method = "POST";
            // //request.ContentType = "application/x-www-form-urlencoded";
            // //request.ContentLength = data.Length;

            // //using (var stream = request.GetRequestStream())
            // //{
            // //    stream.Write(data1, 0, data1.Length);
            // //}

            // //var response = (HttpWebResponse)request.GetResponse();

            // //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            // //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + responseString + "');", true);

            // WebRequest request = WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController");
            // byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + data);
            // request.Method = WebRequestMethods.Http.Post;
            // request.ContentType = "application/x-www-form-urlencoded";
            // Stream requestStream = request.GetRequestStream();
            // requestStream.Write(buffer, 0, buffer.Length);
            // requestStream.Close();
            // Console.Write(buffer.Length);
            // Console.Write(request.ContentLength);
            // WebResponse myResponse = request.GetResponse();
            // string result = "";
            // StreamReader reader = new StreamReader(myResponse.GetResponseStream());
            // result = reader.ReadToEnd();
            // //Label1.Text = result;
            // //Console.WriteLine(result);
            //// divFooter.Visible = true;
            // ViewState["result"] = result;
            // ViewState["idno"] = idno;
            // ViewState["ORDERID"] = ORDERID;

            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showPopup();", true);
            //txnLabel.Text = "1212131";
            //FillSuccessString(result, idno, ORDERID);

        }

        catch (Exception exp)
        {
            //Response.Write("Exception " + exp);

        }
        //}
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

    public void FillSuccessString(String data, int idno, string ORDERID)
    {
        //data = "0130|BSABDURCOE|7227162465649|WCPH1234502060|217509835007|1500.00|CPH|NA|10|INR|DIRECT|NA|NA|00.00|24-06-2022 14:09:12|0300|NA|SATHISH. J|9489563290|AEF|188031601043|972|5649|0|NA|Success|N|NA|0.00|NA|NA|Y|511A4984C8737D9AF18598DCB000E593D26A018443D84EC3D8A6B15FA444A910";
        //0130|BSABDURCOE|63767719121175|XHD50922370598|235364370158|1000.00|HD5|NA|10|INR|DIRECT|NA|NA|00000000.00|19-12-2022 21:32:12|0300|NA|SURYA HUSSAIN|9884678193|CONV|191291601175|1175|1175|0|NA|NA|Y|NA|0.00|NA|NA|Y|D5A4F7AD2E486181594EC06A064DB240C7EB96B9298C7B7CE6E5D2EFA8C2EC22

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

       // ViewState["Transactionid"] = listStrLineElements[3].ToString();         //APTRANSACTIONID 

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

        //string[] segments = data.Split('|');
        //if (status == "0300" && refundstatus == "NA")
        //Label_MerchTxnRef.Text = listStrLineElements[3].ToString();
        // lblEnrollNo.Text = listStrLineElements[2].ToString();
        //else
        //{ 
        //}
        //foreach (ListViewItem item in lvlist.Items)
        //{
        //    Label lborderid = item.FindControl("lblorderId") as Label;
        //  //  string idno = (item.FindControl("hdid") as HiddenField).Value;
        //    Label lbamount = item.FindControl("lblamount") as Label;
        //    Label lbmerchantid = item.FindControl("lblmertransactionid") as Label;

        // ViewState["IDNO"] = idno;
        // lblamount.Text = listStrLineElements[5].ToString();
        // lblname.Text = listStrLineElements[17].ToString();
        // string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= " + idno);
        //lblEnrollNo.Text = Enrollno.ToString();

        // }
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
            //FillSuccessString(ViewState["result"].ToString(),Convert.ToInt32(ViewState["idno"]),ViewState["ORDERID"].ToString());
            if (txnLabel.Text == "NA" || lblAmount.Text == "NA" || lblStatus.Text == "NA")
            {
                objCommon.DisplayMessage(this.Page, "Failed to manage", this.Page);
                 return;
            }
            string Enrollno = string.Empty;
            Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= '" + ViewState["idno"] + "'");
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
                // output = (objFees.BULKInsertOnlinePayment_DCR((idno).ToString(), (ViewState["reccode"]).ToString(), ORDERID.ToString, lbmerchantid.ToString, "O", "1", lblamount, "Success", Enrollno.ToString(), lblResponse.ToString()));
                //output = objFees.BulkInsertOnlinePayment_DCR(ViewState["idno"].ToString(), ViewState["reccode"].ToString(), ViewState["ORDERID"].ToString(), txnLabel.Text, "O", "1", lblAmount.Text, "Success", Enrollno.ToString(), lblResponse.ToString(), Convert.ToInt32(Session["OrgId"]));
                //output = objFees.BulkInsertOnlinePaymentLog(ViewState["ORDERID"].ToString(), Convert.ToInt32(ViewState["idno"]), "1", Reciept_code, Convert.ToString(ViewState["TranDate"]), txnLabel.Text, "1", lblResponse.ToString(), TranDateTime);

                string SP_Name = "PKG_ACAD_CONVOCATION_AUTO_UPDATED_TRANSACTION";
                string SP_Parameters = "@P_ORDER_ID, @P_IDNO, @P_ISVALID, @P_RECEIPT_CODE, @P_TRANSACTION_DATE, @P_TRANSID, @P_PAY_STATUS, @P_MESSAGE,@P_APTRANSACTIONID,@P_OUTPUT";
                string Call_Values = "" + ViewState["ORDERID"].ToString() + "," + Convert.ToInt32(ViewState["idno"]) + ",1," + Reciept_code + "," + Convert.ToString(ViewState["TranDate"]) + "," + Convert.ToString(ViewState["Transactionid"]) + ",1," + Convert.ToString(ViewState["result"]) + "," + Convert.ToString(ViewState["ApTransactionid"]) + ",0";
                  
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
    //public void doubleVerification()
    //{
    //    try
    //    {
    //        String data = "0122|CRES" + "|" + ViewState["Orderid"] + "|" + DateTime.Now.ToString("yyyyMMddhhmmss");
    //        //string data = "0122|SVCE2|" + ViewState["Orderid"] + "|SHMP8634441559|993142|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|19-03-2020 10:46:48|0300|NA|BALACHANDAR M|1662|AEF|AEF180301010,58,3,9444208734|1|NA|58|NA|PGS10001-Success|gpOT4FkIAejC";
    //        string ChecksumKey = "sNPAFD72PKSWXYICHM2SyYgaQ7pF1Xf4";
    //        String hash = String.Empty;
    //        hash = GetHMACSHA256(data, ChecksumKey);
    //        hash = hash.ToUpper();
    //        string msg = data + "|" + hash;

    //        //TO ENABLE SECURE CONNECTION SSL/TLS
    //        ServicePointManager.Expect100Continue = true;
    //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

    //        var request = (HttpWebRequest)WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController?msg=" + msg);
    //        var response = (HttpWebResponse)request.GetResponse();
    //        Stream dataStream = response.GetResponseStream();
    //        StreamReader reader = new StreamReader(dataStream);
    //        string strResponse = reader.ReadToEnd();
    //        //lblNote1.Text = strResponse;
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
    //        if (con.State == ConnectionState.Open)
    //        {
    //            con.Close();
    //        }
    //        con.Open();
    //        string res = "RD";
    //        SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + strResponse.ToString() + "','" + res + "')", con);
    //        cmd.Connection = con;
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        //lblNote1.Text = string.Empty;
    //        string[] repoarray;
    //        repoarray = strResponse.Split('|');

    //        string authstatus = repoarray[15].ToString();
    //        string txnid1 = repoarray[2].ToString();
    //        string amount1 = repoarray[5].ToString();
    //        string apitransid = repoarray[3].ToString();
    //        string BankReferenceNo = repoarray[4].ToString();
    //        string TxnType = repoarray[8].ToString();
    //        string receipt = repoarray[19].ToString();
    //        //string status_msg = repoarray[25].ToString();

    //        string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"] + "'");
    //        //string status = repoarray[15].ToString();
    //        //string status_msg = repoarray[25].ToString();
    //        //string msgR = repoarray[32].ToString();
    //        string txnMessage = string.Empty;
    //        string txnStatus = string.Empty;
    //        #region Get Transaction Details
    //        if (authstatus == "0300")
    //        {
    //            txnMessage = "Successful Transaction";
    //            txnStatus = "Success";
    //        }
    //        else if (authstatus == "0399")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Invalid Authentication at Bank";
    //        }
    //        else if (authstatus == "NA")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Invalid Input in the Request Message";
    //        }
    //        else if (authstatus == "0002")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "BillDesk is waiting for Response from Bank";
    //        }
    //        else if (authstatus == "0001")
    //        {
    //            txnMessage = "Cancel Transaction";
    //            txnStatus = "Error at BillDesk";
    //        }
    //        else
    //        {
    //            txnMessage = "Something went wrong. Try Again!.";
    //            txnStatus = "Payment Faild";
    //        }
    //        #endregion

    //        FeeCollectionController objFeesCnt = new FeeCollectionController();
    //        //if (authstatus == "0300")
    //        //{
    //        int retval = 0;
    //        //retval = objFees.BulkInsertOnlinePayment_DCR(txnid1, Convert.ToInt32(ViewState["idno"]), "1", receipt, Convert.ToString(ViewState["TranDate"]), txnStatus, "1", txnMessage, TranDateTime);

    //        //if (rec_code == "PRF")
    //        //{
    //        //    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 1);

    //        //}
    //        //else if (rec_code == "RF")
    //        //{
    //        //    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 2);

    //        //}


    //    }
    //    catch { }
    //}
}