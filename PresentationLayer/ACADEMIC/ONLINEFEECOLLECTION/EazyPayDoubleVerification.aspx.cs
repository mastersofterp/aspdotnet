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
                showonline.Visible = false;
                DataSet dsFill = objCommon.Populate_PaymentActivity();
                if (rdbOnline.Checked)
                    {
                    rdbOnline_CheckedChanged(sender, e);
                    }
                //ddlActivityName.DataSource = dsFill;
                //ddlActivityName.DataTextField = "ACTIVITYNAME";
                //ddlActivityName.DataValueField = "ACTIVITYNO";
                //ddlActivityName.DataBind();
                objCommon.FillDropDownList(ddlCode, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE<>''", "RECIEPT_CODE");
                objCommon.FillDropDownList(ddldegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                }
            }
        }


    protected void btnShow_Click(object sender, EventArgs e)
        {
        FeeCollectionController objFees = new FeeCollectionController();
        //pnlSelection.Visible = false;
        pnlDetails.Visible = true;
        DataSet ds = GetAllLABELBULK_DAIICT(txtprnno.Text.Trim().ToString(), Convert.ToInt32(ddlActivityName.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
            {

            string Branch = ds.Tables[0].Rows[0]["BRANCH"].ToString().Replace(",", "<br>");
            lblname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblEnrollNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            ViewState["REGNO"] = lblEnrollNo.Text;
            lblbranch.Text = Branch;
            showonline.Visible = true;
         
            // lbldegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
            // ViewState["DegreeNo"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            bind_listview();
            }
        }

    protected void bind_listview()
        {
        //pnlSelection.Visible = false;
        pnlDetails.Visible = true;
        DataSet ds = GetAllOnlineDcrTempData(txtprnno.Text.Trim().ToString(), Convert.ToInt32(ddlActivityName.SelectedValue));

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

    protected void bind_listviewOnline()
        {
        //pnlSelection.Visible = false;
        showonline.Visible = true;
        DataSet ds = GetAllOnlineDcrTempData_online(txtRegno.Text.Trim().ToString(), Convert.ToInt32(ddldegree.SelectedValue), "TF");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
            lvOnline.DataSource = ds;
            lvOnline.DataBind();
            //pnlListView.Visible = true;
            lvOnline.Visible = true;
            panelOnlinelv.Visible = true;
            }
        else
            {
            lvOnline.DataSource = null;
            lvOnline.DataBind();
            lvOnline.Visible = false;
            }
        }




    protected void btnManage_Click(object sender, EventArgs e)
        {
        try
            {
            //return;
            Button myButton = (Button)sender;
            String ORDERID = (myButton.CommandArgument.ToString());
            DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree_DAIICT(Convert.ToInt32(Session["OrgId"]), 1, 1, 0, 0);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {


                string merchantid = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();

                int idno = Convert.ToInt32((myButton.CommandName != string.Empty ? Double.Parse(myButton.CommandName) : 0));


                string ezpaytranid = "";
                string amount = "";

                //String data = Convert.ToString("ezpaytranid" = "" + "&amount" = "" + "&paymentmode" = "" + "&merchantid" = merchantid + "&trandate" = "" + "&pgreferenceno" = ORDERID);

                WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify?");
                //WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify");
                byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + "ezpaytranid=&amount=&paymentmode=&merchantid=" + merchantid + "&trandate=&pgreferenceno=" + ORDERID + "");
                // byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("ezpaytranid=&amount=&paymentmode=&merchantid=368293&trandate=&pgreferenceno=742571254-440");
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
                Console.WriteLine(result);
                // divFooter.Visible = true;
                ViewState["result"] = result;
                ViewState["idno"] = idno;
                ViewState["ORDERID"] = ORDERID;


                txnLabel.Text = "1212131";
                //lblResponse.Text = data;
                pnlSelection.Visible = false;
                pnlDetails.Visible = false;
                List<String> listStrLineElements;
                listStrLineElements = result.Split('&').ToList();
                string status = listStrLineElements[0].ToString();
                //string refundstatus = listStrLineElements[27].ToString();
                txnLabel.Text = listStrLineElements[1].Substring(listStrLineElements[1].IndexOf('=') + 1);
                lblAmount.Text = listStrLineElements[2].Substring(listStrLineElements[2].IndexOf('=') + 1);
                lblStatus.Text = listStrLineElements[0].Substring(listStrLineElements[2].IndexOf('=') + 1);
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
            ServicePointManager.ServerCertificateValidationCallback = delegate
                {
                    return true;
                };

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
        listStrLineElements = data.Split('&').ToList();
        //string status = listStrLineElements[15].ToString();
        // string refundstatus = listStrLineElements[27].ToString();
        //txnLabel.Text = listStrLineElements[3].ToString();
        //lblAmount.Text = listStrLineElements[5].ToString();
        TranDateTime = listStrLineElements[3].Substring(listStrLineElements[3].IndexOf('=') + 1);
        string[] Separate = TranDateTime.Split(' ');
        string desiredDate = Separate[0];
        TranDate = desiredDate;

        string status = listStrLineElements[0].Substring(listStrLineElements[0].IndexOf('=') + 1);
        //string refundstatus = listStrLineElements[27].ToString();
        txnLabel.Text = listStrLineElements[1].Substring(listStrLineElements[1].IndexOf('=') + 1);
        lblAmount.Text = listStrLineElements[2].Substring(listStrLineElements[2].IndexOf('=') + 1);
        lblStatus.Text = listStrLineElements[0].Substring(listStrLineElements[0].IndexOf('=') + 1);

        ViewState["TranDate"] = TranDateTime.Split(' ')[0];


        if (status == "Success")
            {
            lblStatus.Text = "Success";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            btnSubmit.Visible = true;
            btnsubmitonline.Visible = true;
            }
        else
            {
            lblStatus.Text = Convert.ToString(status);
            btnSubmit.Visible = false;
            btnsubmitonline.Visible = false;
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
            //string data = "0130|BSARAPPN|428589231-237|XHD50970363574|302328115486|1.00|HD5|NA|10|INR|DIRECT|NA|NA|00000000.00|23-01-2023 12:44:34|0300|NA|NA|237|9049963903|NA|NA|NA|NIKHIL|NA|NA|Y|NA|0.00|NA|NA|Y|F3A0B61B127FA8B8B1E7466EC6849F832DB8496FDC53AE728068DD6AEE04AA48";//ViewState["result"].ToString();
            string data = ViewState["result"].ToString();
            //string data = "status=RIP&ezpaytranid=230430148386993&amount=2832&trandate=2023-04-30 15:40:51.0&pgreferenceno=738852304-2300&sdt=&BA=2832.00&PF=0.00&TAX=0.0&PaymentMode=UPI_ICICI";
            string[] repoarray;
            repoarray = data.Split('&');



            string orderid = repoarray[4].Substring(repoarray[4].IndexOf('=') + 1);
            string txnid = repoarray[1].Substring(repoarray[1].IndexOf('=') + 1);
            string amount = repoarray[2].Substring(repoarray[2].IndexOf('=') + 1);
            //amount = amount.TrimStart(new Char[] { '0' });
            //string userno = ViewState["idno"].ToString();
            string userno = ViewState["idno"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string transactiondate = repoarray[3].Substring(repoarray[3].IndexOf('=') + 1);
            string authstatus = repoarray[0].Substring(repoarray[0].IndexOf('=') + 1);
            //string name = repoarray[23].ToString();

            DateTime txnDate = Convert.ToDateTime(transactiondate);

            //FillSuccessString(ViewState["result"].ToString(),Convert.ToInt32(ViewState["idno"]),ViewState["ORDERID"].ToString());

            int output = 0;

            //output = (objFees.BULKInsertOnlinePayment_DCR((idno).ToString(), (ViewState["reccode"]).ToString(), ORDERID.ToString, lbmerchantid.ToString, "O", "1", lblamount, "Success", Enrollno.ToString(), lblResponse.ToString()));
            //output = objFees.BulkInsertOnlinePayment_DCR(ViewState["idno"].ToString(), ViewState["reccode"].ToString(), ViewState["ORDERID"].ToString(), txnLabel.Text, "O", "1", lblAmount.Text, "Success", Enrollno.ToString(), lblResponse.ToString(), Convert.ToInt32(Session["OrgId"]));
            //output = objFees.BulkInsertOnlinePayment_DCR(ViewState["ORDERID"].ToString(), Convert.ToInt32(ViewState["idno"]), "1", "", Convert.ToString(ViewState["TranDate"]), txnLabel.Text, "1", lblResponse.ToString(), TranDateTime);
            //if (ddlActivityName.SelectedValue == "2")
            //{
            output = OnlineAdmFeesPayment(orderid, amount, authstatus, Convert.ToInt32(userno), txnid, txnDate, UA_NO);
            //}
            //else
            //{
            //    output = OnlineAdmFeesPayment(orderid, amount, authstatus, Convert.ToInt32(userno), txnid, txnDate);
            //}
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

    protected void ClearPopOnline()
        {
        lbltransid.Text = string.Empty;
        lblamountonline.Text = string.Empty;
        lbltransstatus.Text = string.Empty;
        pnlDetails.Visible = false;
        pnlSelection.Visible = true;
        ddlActivityName.SelectedIndex = 0;
        txtRegno.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closePopupOnline();", true);
        }


    public DataSet GetAllLABELBULK_DAIICT(string applicationid, int degreeType)
        {
        DataSet ds = null;
        try
            {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objparams = null;
            objparams = new SqlParameter[2];
            objparams[0] = new SqlParameter("@P_APPLICATION_ID", applicationid);
            objparams[1] = new SqlParameter("@P_DEGREETYPE", degreeType);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_ADMP_APPLICATION_IDWISE_DETAILS_DAIICT", objparams);
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


    public DataSet GetAllLABELBULK_DAIICT_ONLINE(string applicationid, int degreeType)
        {
        DataSet ds = null;
        try
            {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objparams = null;
            objparams = new SqlParameter[2];
            objparams[0] = new SqlParameter("@P_REGNO", applicationid);
            objparams[1] = new SqlParameter("@P_DEGREETYPE", degreeType);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_DETAILS_DAIICT_ONLINE_PAYMENT", objparams);
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

    public DataSet GetAllOnlineDcrTempData_online(string Regno, int degreeType, string Receipt_code)
        {
        DataSet ds = null;
        try
            {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objparams = new SqlParameter[0];
            objparams = new SqlParameter[3];
            objparams[0] = new SqlParameter("@P_REGNO", Regno);
            objparams[1] = new SqlParameter("@P_DEGREETYPE", degreeType);
            objparams[2] = new SqlParameter("@P_RECIEPT_CODE", Receipt_code);
            //objparams[1] = new SqlParameter("@P_RECIEPT_CODE", receiptCode);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_ONLINE_DCR_TEMP_DETAILS_DAIICT", objparams);
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


    public DataSet GetAllOnlineDcrTempData(string applicationid, int degreeType)
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
            ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_ONLINE_DCR_BULKBILLDESK_DETAILS_DAIICT", objparams);
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


    public int OnlineAdmFeesPayment(string orderID, string AMOUNT, string TRANSACTIONSTATUS, int userNo, string txnID, DateTime txnDate, int UA_No)//, int ORDER_ID), string REC_TYPE
        {
        int countrno = Convert.ToInt32(CustomStatus.Others);
        try
            {
            SQLHelper objSqlhelper = new SQLHelper(_connectionString);
            SqlParameter[] sqlparam = null;
                {
                sqlparam = new SqlParameter[8];
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
                sqlparam[6] = new SqlParameter("@P_UA_NO", UA_No);
                sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                sqlparam[7].Direction = ParameterDirection.Output;
                };
                //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACD_ADMP_ONLINE_PAYMENT_EAZYPAY_DOUBLE_VERIFICATION", sqlparam, true);
                // object studid = 1;
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

    protected void lvlist_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        if (e.Item.ItemType == ListViewItemType.DataItem)
            {

            //lblStatus = (Label)e.Item.FindControl("lblStatus");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            Button btnManage = (Button)e.Item.FindControl("btnManage");


            if (lblStatus.Text == "Success")
                {
                btnManage.Visible = false;
                }
            else
                {
                btnManage.Visible = true;
                }
            }
        }

    protected void rdbOnline_CheckedChanged(object sender, EventArgs e)
        {
        pnlonline.Visible = true;
        pnlMIS.Visible = false;

        }
    protected void rdbMIS_CheckedChanged(object sender, EventArgs e)
        {
        pnlMIS.Visible = true;
        pnlonline.Visible = false;
        }
    protected void btnshowonline_Click(object sender, EventArgs e)
        {
        btnshowonline.Visible = true;
        showonline.Visible = true;
        DataSet ds = GetAllLABELBULK_DAIICT_ONLINE(txtRegno.Text.Trim().ToString(), Convert.ToInt32(ddldegree.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
            {
            string Branch = ds.Tables[0].Rows[0]["BRANCH"].ToString().Replace(",", "<br>");
            lblStudentName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblRegno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            lblDeg.Text = Branch;
            ViewState["REGNONEW"] = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            ViewState["studidno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
            // lbldegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
            // ViewState["DegreeNo"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            bind_listviewOnline();
            }
        else
            {
            objCommon.DisplayMessage(this.Page, "No student found for selected criteria.", this.Page);
            showonline.Visible = false;
            }
        }
    protected void btncancelonline_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        }
    protected void lvOnline_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        if (e.Item.ItemType == ListViewItemType.DataItem)
            {

            //lblStatus = (Label)e.Item.FindControl("lblStatus");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            Button btnManage = (Button)e.Item.FindControl("btnManage");


            if (lblStatus.Text == "Success")
                {
                btnManage.Visible = false;
                }
            else
                {
                btnManage.Visible = true;
                }
            }
        }
    protected void btnManageOnline_Click(object sender, EventArgs e)
        {
        Button myButton = (Button)sender;
        String ORDERID = (myButton.CommandArgument.ToString());
        int activityno = 0;
        if (Session["OrgId"].ToString() == "15")
            {
            activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));

            }

        DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree_DAIICT(Convert.ToInt32(Session["OrgId"]), 1, activityno, 0, 0);
        if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
            string merchantid = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();

            int idno = Convert.ToInt32((myButton.CommandName != string.Empty ? Double.Parse(myButton.CommandName) : 0));
            string ezpaytranid = "";
            string amount = "";
            //String data = Convert.ToString("ezpaytranid" = "" + "&amount" = "" + "&paymentmode" = "" + "&merchantid" = merchantid + "&trandate" = "" + "&pgreferenceno" = ORDERID);

            //WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify");
            ////WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify");
            byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + "ezpaytranid=&amount=&paymentmode=&merchantid=" + merchantid + "&trandate=&pgreferenceno=" + ORDERID + "");

            //byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("ezpaytranid=&amount=&paymentmode=&merchantid=368293&trandate=&pgreferenceno=742571254-440");

            WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify?");
            //WebRequest request = WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify");
            //byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("msg=" + "ezpaytranid=&amount=&paymentmode=&merchantid=" + "368293" + "&trandate=&pgreferenceno=" + ORDERID + "");
            // byte[] buffer = System.Text.Encoding.GetEncoding(1252).GetBytes("ezpaytranid=&amount=&paymentmode=&merchantid=368293&trandate=&pgreferenceno=742571254-440");
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

            Console.WriteLine(result);
          
            ViewState["result"] = result;
            ViewState["studidno"] = idno;
            ViewState["ORDERID"] = ORDERID;


            // lbltransid.Text = string.Empty;
            //  "1212131";
            //lblResponse.Text = data;
            pnlonlinedetails.Visible = false;
            showonline.Visible = false;
            List<String> listStrLineElements;
            listStrLineElements = result.Split('&').ToList();
            string status = listStrLineElements[0].ToString();
            //string refundstatus = listStrLineElements[27].ToString();
            lbltransid.Text = listStrLineElements[1].Substring(listStrLineElements[1].IndexOf('=') + 1);
            lblamountonline.Text = listStrLineElements[2].Substring(listStrLineElements[2].IndexOf('=') + 1);
            lbltransstatus.Text = listStrLineElements[0].Substring(listStrLineElements[2].IndexOf('=') + 1);
            FillSuccessString(result, idno, ORDERID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showPopupOnline();", true);
            pnlonlinedetails.Visible = true;
            showonline.Visible = true;

            }
        }
    protected void lvOnline_ItemDataBound1(object sender, ListViewItemEventArgs e)
        {
        if (e.Item.ItemType == ListViewItemType.DataItem)
            {

            //lblStatus = (Label)e.Item.FindControl("lblStatus");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            Button btnManage = (Button)e.Item.FindControl("btnManageOnline");


            if (lblStatus.Text == "Success")
                {
                btnManage.Visible = false;
                }
            else
                {
                btnManage.Visible = true;
                }
            }
        }
    protected void btnsubmitonline_Click(object sender, EventArgs e)
        {
        try
            {
            string data = ViewState["result"].ToString();
           
            string[] repoarray;
            repoarray = data.Split('&');
            string orderid = repoarray[4].Substring(repoarray[4].IndexOf('=') + 1);
            string txnid = repoarray[1].Substring(repoarray[1].IndexOf('=') + 1);
            string amount = repoarray[2].Substring(repoarray[2].IndexOf('=') + 1);
            //amount = amount.TrimStart(new Char[] { '0' });
            //string userno = ViewState["idno"].ToString();
            string userno = ViewState["studidno"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string transactiondate = repoarray[3].Substring(repoarray[3].IndexOf('=') + 1);
            string authstatus = repoarray[0].Substring(repoarray[0].IndexOf('=') + 1);
            //string name = repoarray[23].ToString();
            //if (txnDate=="NA")
           // DateTime txnDate = Convert.ToDateTime(transactiondate);           
            //int output = 0;
            ViewState["reccode"] = "TF";


            try
                {
                int SessionNo;
                int UserNo;
                string EasyPayId;
                double dAmount = 0;
                double Fee;
                double Tax;
                string OrderId;
                string RazorPayOrderId;
                string IP_Address;
                double EasyPay_Amount;
                IP_Address = Request.ServerVariables["REMOTE_HOST"].ToString();
                SessionNo = 0;
                Fee = 0;
                Tax = 0;
                UserNo = Convert.ToInt32(Session["USERNO"].ToString());
                EasyPayId = "";

                int Result = 0;
                Result = InsertEasyPayNotCaptureTransaction(SessionNo, UserNo, EasyPayId, dAmount, Convert.ToDouble(dAmount), Fee, Tax, orderid, orderid, IP_Address);
                }
            catch (Exception ex)
                {
                objCommon.DisplayMessage(this.Page, "Error Occured ..!", this.Page);
                }



            int output = objFees.InsertOnlinePayment_DCR(userno, ViewState["reccode"].ToString(), orderid, txnid, "ONLINE FEE COLLECTION", string.Empty, amount, "Success", ViewState["REGNONEW"].ToString(), "");
            

           // output = OnlineAdmFeesPayment(orderid, amount, authstatus, Convert.ToInt32(userno), txnid, txnDate, UA_NO);
            //}
            //else
            //{
            //    output = OnlineAdmFeesPayment(orderid, amount, authstatus, Convert.ToInt32(userno), txnid, txnDate);
            //}
            if (output == -99)
                {
                objCommon.DisplayMessage(this.Page, "Failed to manage", this.Page);
                }
            else
                {
                objCommon.DisplayMessage(this.Page, "Payment Manage Successful", this.Page);
                ClearPopOnline();
                }

            }
        catch (Exception ex)
            {

            }
        }



    public int InsertEasyPayNotCaptureTransaction(int SessionNo, int UserNo, string EasyPayId, double Amount, double EasyPay_Amount, double Fee, double Tax, string OrderId, string EasyPayOrderId, string IPAddress)
        {
        int retStatus = 0;
        try
            {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[11];
            objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
            objParams[1] = new SqlParameter("@P_USERNO", UserNo);
            objParams[2] = new SqlParameter("@P_EASYPAY_PAYMENT_ID", EasyPayId);
            objParams[3] = new SqlParameter("@P_AMOUNT", Amount);
            objParams[4] = new SqlParameter("@P_EASYPAY_AMOUNT", EasyPay_Amount);
            objParams[5] = new SqlParameter("@P_FEE", Fee);
            objParams[6] = new SqlParameter("@P_TAX", Tax);
            objParams[7] = new SqlParameter("@P_ORDER_ID", OrderId);
            objParams[8] = new SqlParameter("@P_EASYPAY_ORDER_ID", EasyPayOrderId);
            objParams[9] = new SqlParameter("@P_IPADDRESS", IPAddress);
            objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[10].Direction = ParameterDirection.Output;
            object ret = objSQLHelper.ExecuteNonQuerySP("INS_EASYPAY_NOT_CAPTURE_TRANS", objParams, false);
            if ((int)ret != -99 && (int)ret != 0)
                {
                retStatus = (int)ret;
                }
            else
                {
                retStatus = -99;
                }
            return retStatus;
            }
        catch (Exception ex)
            {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
            }
        }
    }