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

public partial class ACADEMIC_ONLINEFEECOLLECTION_CC_Avenue_DoubleVerification : System.Web.UI.Page
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

            string workingKey = ConfigurationManager.AppSettings["workingKey"];
            string accessCode = ConfigurationManager.AppSettings["AccessCode"]; //"AVED47KD55AM13DEMA";         
           // string queryUrl = "https://logintest.ccavenue.com/apis/servlet/DoWebTrans";
            string orderStatusQuery = txtTransactionId.Text + '|' + txtOrderId.Text + "|"; // Ex.= CCAvenue Reference No.|Order No.|
            string encQuery = "";
            string queryUrl = "https://logintest.ccavenue.com/apis/servlet/DoWebTrans?";
            //logintest.ccavenue.com/apis/servlet/DoWebTrans?enc_request=&access_code=&request_type=JSON&response_type=JSON&command=orderStatusTracker&version=1.2
            CCACrypto ccaCrypto = new CCACrypto();
            encQuery = ccaCrypto.Encrypt(orderStatusQuery, workingKey);

            // make query for the status of the order to ccAvenues change the command param as per your need
            string authQueryUrlParam = "enc_request=" + encQuery + "&access_code=" + accessCode + "&request_type=STRING&response_type=STRING&command=orderStatusTracker&version=1.2";

            // Url Connection
            String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);          
            NameValueCollection param = getResponseMap(message);
            String status = "";
            String encRes = "";
            ViewState["status"] = 0;
            if (param != null && param.Count == 2)
            {
                for (int i = 0; i < param.Count; i++)
                {
                    if ("status".Equals(param.Keys[i]))
                    {
                        status = param[i];
                    }
                    if ("enc_response".Equals(param.Keys[i]))
                    {
                        encRes = param[i];
                        //Response.Write(encResXML);
                    }
                }
                if (!"".Equals(status) && status.Equals("0"))
                {
                    String ResString = ccaCrypto.Decrypt(encRes, workingKey);
                    FillSuccessString(ResString);
                    //Response.Write(ResString);
                }
                else if (!"".Equals(status) && status.Equals("1"))
                {
                    objCommon.DisplayMessage(this.updServertoServer, "failure response from ccAvenues:" + encRes, this.Page);
                    //String ResString = ccaCrypto.Decrypt(encRes, workingKey);
                    //FillFailureString(ResString);
                    //Response.Write(ResString);
                    //Console.WriteLine("failure response from ccAvenues: " + encRes);
                }

            }

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


            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + lblEnrollNo.Text+"'");
           // return;

            if (status == "Shipped")
            {

              
                output = objFees.InsertOnlinePayment_DCR(idno, ViewState["RECIEPTCODE"].ToString(), Label_MerchTxnRef.Text, Label_OrderInfo.Text, "O", string.Empty, Label_Amount.Text, "Success", lblEnrollNo.Text.ToString(), "");           



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
}