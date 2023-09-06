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
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS;

public partial class ACADEMIC_BulkBilldeskDoubleVerification : System.Web.UI.Page
{
    Common objCommon = new Common();
    FeeCollectionController objFees = new FeeCollectionController();
    string TranDate = "";
    string TranDateTime = "";
    int college_id = 0;
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //ddlActivityName.DataSource = dsFill;
                //ddlActivityName.DataTextField = "ACTIVITYNAME";
                //ddlActivityName.DataValueField = "ACTIVITYNO";
                //ddlActivityName.DataBind();
                objCommon.FillDropDownList(ddlCode, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE<>''", "RECIEPT_CODE");
            }
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        FeeCollectionController objFees = new FeeCollectionController();
        //pnlSelection.Visible = false;
        pnlDetails.Visible = true;
        DataSet ds = GetAllLABELBULK_Crescent(txtprnno.Text.Trim().ToString(),Convert.ToInt32(ddlActivityName.SelectedValue));
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
        DataSet ds = GetAllOnlineDcrTempData(txtprnno.Text.Trim().ToString(),Convert.ToInt32(ddlActivityName.SelectedValue));

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
            DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(Session["OrgId"]), 0, 3, 7, 0);
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
                Label1.Text = result;
                Console.WriteLine(result);
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
        //data = "0130|BSARAPPN|428589231-237|XHD50970363574|302328115486|1.00|HD5|NA|10|INR|DIRECT|NA|NA|00000000.00|23-01-2023 12:44:34|0300|NA|NA|237|9049963903|NA|NA|NA|NIKHIL|NA|NA|Y|NA|0.00|NA|NA|Y|F3A0B61B127FA8B8B1E7466EC6849F832DB8496FDC53AE728068DD6AEE04AA48";
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
            //string data = "0130|BSARAPPN|428589231-237|XHD50970363574|302328115486|1.00|HD5|NA|10|INR|DIRECT|NA|NA|00000000.00|23-01-2023 12:44:34|0300|NA|NA|237|9049963903|NA|NA|NA|NIKHIL|NA|NA|Y|NA|0.00|NA|NA|Y|F3A0B61B127FA8B8B1E7466EC6849F832DB8496FDC53AE728068DD6AEE04AA48";//ViewState["result"].ToString();
            string data = ViewState["result"].ToString();
            string[] repoarray;
            repoarray = data.Split('|');
            string orderid = repoarray[2].ToString();
            string txnid = repoarray[3].ToString();
            string amount = repoarray[5].ToString();
            amount = amount.TrimStart(new Char[] { '0' });
            string userno = repoarray[18].ToString();
            string transactiondate = repoarray[14].ToString();
            string authstatus = repoarray[15].ToString();
            string name = repoarray[23].ToString();
            Session["userno"] = userno;
            DateTime txnDate = Convert.ToDateTime(transactiondate);

            //FillSuccessString(ViewState["result"].ToString(),Convert.ToInt32(ViewState["idno"]),ViewState["ORDERID"].ToString());

            int output = 0;

            // output = (objFees.BULKInsertOnlinePayment_DCR((idno).ToString(), (ViewState["reccode"]).ToString(), ORDERID.ToString, lbmerchantid.ToString, "O", "1", lblamount, "Success", Enrollno.ToString(), lblResponse.ToString()));
            //output = objFees.BulkInsertOnlinePayment_DCR(ViewState["idno"].ToString(), ViewState["reccode"].ToString(), ViewState["ORDERID"].ToString(), txnLabel.Text, "O", "1", lblAmount.Text, "Success", Enrollno.ToString(), lblResponse.ToString(), Convert.ToInt32(Session["OrgId"]));
            //output = objFees.BulkInsertOnlinePayment_DCR(ViewState["ORDERID"].ToString(), Convert.ToInt32(ViewState["idno"]), "1", "", Convert.ToString(ViewState["TranDate"]), txnLabel.Text, "1", lblResponse.ToString(), TranDateTime);     
            if (ddlActivityName.SelectedValue == "2")
            {
                output = OnlineAdmFeesPayment_PhD(orderid, amount, authstatus, Convert.ToInt32(userno), txnid, txnDate);
            }
            else
            {
                output = OnlineAdmFeesPayment(orderid, amount, authstatus, Convert.ToInt32(userno), txnid, txnDate);
            }
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
    public DataSet GetAllLABELBULK_Crescent(string applicationid,int degreeType)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objparams = null;
            objparams = new SqlParameter[2];
            objparams[0] = new SqlParameter("@P_APPLICATION_ID", applicationid);
            objparams[1] = new SqlParameter("@P_DEGREETYPE", degreeType);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_APPLICATION_BULKBILLDESK_DETAILS", objparams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetAllOnlineDcrTempData() --> " + ex.Message + " " + ex.StackTrace);
        }

        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    public DataSet GetAllOnlineDcrTempData(string applicationid,int degreeType)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objparams = new SqlParameter[0];
            objparams = new SqlParameter[2];
            objparams[0] = new SqlParameter("@P_APPLICATION_ID", applicationid);
            objparams[1] = new SqlParameter("@P_DEGREETYPE", degreeType);

            	
            //objparams[1] = new SqlParameter("@P_RECIEPT_CODE", receiptCode);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_ONLINE_DCR_BULKBILLDESK_DETAILS", objparams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.CreateDemandForRedoCourseReg() --> " + ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    public int OnlineAdmFeesPayment(string orderID, string AMOUNT, string TRANSACTIONSTATUS, int userNo, string txnID, DateTime txnDate)//, int ORDER_ID), string REC_TYPE
    {
        int countrno = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSqlhelper = new SQLHelper(_connectionString);
            SqlParameter[] sqlparam = null;
            {
                sqlparam = new SqlParameter[7];
                //sqlparam[0] = new SqlParameter("@P_APTRANSACTIONID", APTRANSACTIONID);  // -- bank id 
                sqlparam[0] = new SqlParameter("@P_ORDERID", orderID);  // -- orderid
                sqlparam[1] = new SqlParameter("@P_AMOUNT", Convert.ToDecimal(AMOUNT));                //-- amt 
                sqlparam[2] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);  // sucess/fail
                //sqlparam[3] = new SqlParameter("@P_MESSAGE", MESSAGE);    // message 
                //sqlparam[3] = new SqlParameter("@P_ap_SecureHash", ap_SecureHash);  // dont knw
                //sqlparam[5] = new SqlParameter("@P_REMARK", remark);    // remark 
                //sqlparam[6] = new SqlParameter("@P_GATEWAYTXNID", gatewaytxnid); // bank id 
                //sqlparam[7] = new SqlParameter("@P_EXTRA_CHARGE", EXTRA_CHARGE);
                //sqlparam[8] = new SqlParameter("@P_RECIEPT_TYPE", recieptcode);
                sqlparam[3] = new SqlParameter("@P_USERNO", userNo);
                sqlparam[4] = new SqlParameter("@P_TRANSACTION_ID", txnID);
                sqlparam[5] = new SqlParameter("@P_TRANSACTION_DATE", txnDate);
                sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                sqlparam[6].Direction = ParameterDirection.Output;
            };
            //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
            object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ADD_ONLINE_PAYMENT_FOR_ONLINE_ADM", sqlparam, true);
            if (Convert.ToInt32(studid) == -99)
            {
                countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            else
                countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlineAdmFeesPayment() --> " + ex.Message + " " + ex.StackTrace);
        }
        return countrno;
    }
    public int OnlineAdmFeesPayment_PhD(string orderID, string AMOUNT, string TRANSACTIONSTATUS, int userNo, string txnID, DateTime txnDate)
    {
        int countrno = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSqlhelper = new SQLHelper(_connectionString);
            SqlParameter[] sqlparam = null;
            {
                sqlparam = new SqlParameter[7];
                sqlparam[0] = new SqlParameter("@P_ORDERID", orderID);  // -- orderid
                sqlparam[1] = new SqlParameter("@P_AMOUNT", Convert.ToDecimal(AMOUNT));                //-- amt 
                sqlparam[2] = new SqlParameter("@P_TRANSACTIONSTATUS", TRANSACTIONSTATUS);  // sucess/fail               
                sqlparam[3] = new SqlParameter("@P_USERNO", userNo);                                                  // user
                sqlparam[4] = new SqlParameter("@P_TRANSACTION_ID", txnID);                                     // txn id
                sqlparam[5] = new SqlParameter("@P_TRANSACTION_DATE", txnDate);                            // txn date
                sqlparam[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                sqlparam[6].Direction = ParameterDirection.Output;
            };
            //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
            object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ADD_ONLINE_PAYMENT_FOR_ONLINE_ADM_PHD", sqlparam, true);
            if (Convert.ToInt32(studid) == -99)
            {
                countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            else
                countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlineAdmFeesPayment() --> " + ex.Message + " " + ex.StackTrace);
        }
        return countrno;
    }
}