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
using HostelBusinessLogicLayer.BusinessLogic;

public partial class HOSTEL_ONLINEFEECOLLECTION_HostelOnlinePaymentRequest : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelFeeCollectionController objFees = new HostelFeeCollectionController();
    StudentFees objStudentFees = new StudentFees();

    string hash_seq = string.Empty;    
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                SqlDataReader dr = objCommon.GetCommonDetails();

                if (dr != null)
                {
                    if (dr.Read())
                    {
                       // lblCollege.Text = dr["COLLEGENAME"].ToString();
                       // lblAddress.Text = dr["College_Address"].ToString();
                        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    }
                }

                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["studName"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int payId = Convert.ToInt32(Session["paymentId"]);
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]));
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {

                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentId = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                    string ChecksumKey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    string SecurityId = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    lblActivityName.Text = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();
                    ViewState["ResponseUrl"] = ResponseUrl;
                    ViewState["RequestUrl"] = RequestUrl;
                    ViewState["merchentId"] = merchentId;
                    ViewState["ChecksumKey"] = ChecksumKey;
                    ViewState["SecurityId"] = SecurityId;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        int status1 = 0;
        int Currency = 1;
        string amount = string.Empty;
        //amount = Convert.ToDouble(lblAmount.Text);

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
        string refno = string.Empty;

        refno = (i + "" + j + "" + k + "" + l + "" + m).ToString() + UserId;        

        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + refno + "'");

        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }

        Session["studrefno"] = refno;
        string datetm = indianTime.ToString("dd-MMM-yyyy");
        string status = "Not Continued";
        amount = Convert.ToString(Session["studAmt"]);

        //string custtype = "INR";


        String data = Convert.ToString(ViewState["merchentId"]) + "|" + refno + "|NA|" + amount + "|NA|NA|NA|INR|NA|R|" + Convert.ToString(ViewState["SecurityId"]) + "|NA|NA|F|" + Convert.ToString(Session["payStudName"]) + "|" + Convert.ToString(Session["StudentSelectedRoom"]) + "|" + Convert.ToString(Session["HostelNo"]) + "|" + Convert.ToString(Session["regno"]) + "|" + Convert.ToString(Session["idno"]) + "|" + Convert.ToString(Session["userno"]) + "|" + Convert.ToString(Session["HostelSessionNo"]) + "|" + Convert.ToString(ViewState["ResponseUrl"]);

        String commonkey = Convert.ToString(ViewState["ChecksumKey"]);

        String hash = String.Empty;

        hash = GetHMACSHA256(data, commonkey);

        data = data + "|" + hash.ToUpper();
        // requestparams.Value = data;


        int result = 0;   // In InsertOnlinePaymentlog method added two parameter OrgId and StudentSelectedRoom by Saurabh L on 31 May 2023 Purpose: To maitain Room_No log
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), status, refno, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["StudentSelectedRoom"]));


        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Idno), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), refno, Convert.ToDouble(amount), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), data);
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), refno, 1, Convert.ToString(Session["ReceiptType"]), data);
        }
      
        if (result > 0)
        {
            string strForm = PreparePOSTForm(Convert.ToString(ViewState["RequestUrl"]), data);
            Page.Controls.Add(new LiteralControl(strForm));
        }
        else
        {
            objCommon.DisplayMessage("Online Payment Not Done, Please Try Again..!!", this.Page);
            return;
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
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
}