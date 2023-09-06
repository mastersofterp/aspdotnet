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

public partial class ACADEMIC_ONLINEFEECOLLECTION_BilldeskDoubleVerification : System.Web.UI.Page
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
              
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //String data = Convert.ToString("0122|BDSKUATY|" + txtOrderId.Text + "|"+ DateTime.Now.ToString("yyyyMMddHHmmss") + "|455057528");
            String data = Convert.ToString("0122|BSABREHFEE|" + txtOrderId.Text + "|" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            String commonkey = Convert.ToString("7QOrIqImgcxkoD8J2RuCbMSI8DM6srl6");

            String hash = String.Empty;

            hash = GetHMACSHA256(data, commonkey);

            data = data + "|" + hash.ToUpper();
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+data+"');", true);
            //string strForm = PreparePOSTForm("https://www.billdesk.com/pgidsk/PGIQueryController", data);
            //Page.Controls.Add(new LiteralControl(strForm));

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 1000;

            //var request = (HttpWebRequest)WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController?");
            //request.KeepAlive = false;
            //request.Timeout = 20000;
            //request.ProtocolVersion = HttpVersion.Version10;
            //request.ServicePoint.Expect100Continue = false;
            //var postData = "msg=" + data;

            //var data1 = Encoding.ASCII.GetBytes(postData);

            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;

            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data1, 0, data1.Length);
            //}

            //var response = (HttpWebResponse)request.GetResponse();

            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + responseString + "');", true);

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
            divFooter.Visible = true;
            FillSuccessString(result);

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
        //data = "0130|BSABDURCOE|7227162465649|WCPH1234502060|217509835007|1500.00|CPH|NA|10|INR|DIRECT|NA|NA|00.00|24-06-2022 14:09:12|0300|NA|SATHISH. J|9489563290|AEF|188031601043|972|5649|0|NA|Success|N|NA|0.00|NA|NA|Y|511A4984C8737D9AF18598DCB000E593D26A018443D84EC3D8A6B15FA444A910";
        lblResponse.Text = data;
        pnlSelection.Visible = false;
        pnlDetails.Visible = true;
        List<String> listStrLineElements;
        listStrLineElements = data.Split('|').ToList();
        string status = listStrLineElements[15].ToString();
        string refundstatus = listStrLineElements[27].ToString();
        //string[] segments = data.Split('|');
        if (status == "0300" && refundstatus == "NA")
        {
            btnManage.Visible = true;
            Label_MerchTxnRef.Text = listStrLineElements[3].ToString();
            Label_OrderInfo.Text = listStrLineElements[2].ToString();
            int idno = Convert.ToInt32(listStrLineElements[21].ToString());
            ViewState["IDNO"] = idno;
            Label_Amount.Text = listStrLineElements[5].ToString();
            lblname.Text = listStrLineElements[17].ToString();
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= " + idno);
            string Reciept_code = string.Empty;

           // DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_RECIEPT_TYPE RT ON(D.RECIEPT_CODE = RT.RECIEPT_CODE) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,D.RECIEPT_CODE,RECIEPT_TITLE,D.REC_NO", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");

            DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_RECIEPT_TYPE RT ON(D.RECIEPT_CODE = RT.RECIEPT_CODE) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "D.RECIEPT_CODE,SESSION_NAME,RECIEPT_TITLE", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lblsessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                //lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                //lblyear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_TITLE"].ToString();
                //lblrecno.Text = ds.Tables[0].Rows[0]["REC_NO"].ToString();
                Reciept_code = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                ViewState["reccode"] = Reciept_code;
            }
            //if (Reciept_code == "AEF")
            //{
            //    int output = objFees.InsertExamPayment_DCR(Convert.ToString(idno), Reciept_code, Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno);
            //}
            //else
            //{
            //    int output = objFees.InsertOnlinePayment_DCR(Convert.ToString(idno), Reciept_code, Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno, data);
            //}
            lblEnrollNo.Text = Enrollno.ToString();

            //Label_MerchantID.Text = "338483";
            lbldate.Text = listStrLineElements[14].ToString();

            Label_Message.Text = "Transaction Success";
            
        }
        else
        {
            btnManage.Visible = false;
            Label_MerchTxnRef.Text = listStrLineElements[3].ToString();
            Label_OrderInfo.Text = listStrLineElements[2].ToString();
            int idno = Convert.ToInt32(listStrLineElements[21].ToString());
            ViewState["IDNO"] = idno;
            Label_Amount.Text = listStrLineElements[5].ToString();
            lblname.Text = listStrLineElements[17].ToString();
            string Enrollno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO= " + idno);

            //int output = objFees.InsertOnlinePayment_DCR(Convert.ToString(idno), "EF", Label_OrderInfo.Text, Label_MerchTxnRef.Text, "EXAM FEES COLLECTION", string.Empty, Label_Amount.Text, "Success", Enrollno, data);
            //DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,'Exam Fees' AS RECIEPT_TITLE,D.REC_NO", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");

            DataSet ds = objCommon.FillDropDown("ACD_DCR_TEMP D INNER JOIN ACD_YEAR Y ON(D.YEAR = Y.YEAR) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON(D.SESSIONNO = SM.SESSIONNO)", "ENROLLNMENTNO", "Y.YEARNAME,S.SEMESTERNAME,SM.SESSION_NAME,'Exam Fees' AS RECIEPT_TITLE,D.REC_NO", "ORDER_ID= '" + Label_OrderInfo.Text + "'", "ENROLLNMENTNO");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lblsessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                //lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                //lblyear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
                lblRecieptType.Text = ds.Tables[0].Rows[0]["RECIEPT_TITLE"].ToString();
                

            }

            lblEnrollNo.Text = Enrollno.ToString();

            //Label_MerchantID.Text = "338483";
            lbldate.Text = listStrLineElements[14].ToString();

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
        int output = 0;
        output = objFees.InsertOnlinePayment_DCR(Convert.ToString(ViewState["IDNO"]), Convert.ToString(ViewState["reccode"]), Label_OrderInfo.Text, Label_MerchTxnRef.Text, "O", "1", Label_Amount.Text, "Success", lblEnrollNo.Text, lblResponse.Text);
        if (output == -99)
        {
            objCommon.DisplayMessage(this.Page, "Failed to manage", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Payment Manage Successful", this.Page);
        }

    }
}