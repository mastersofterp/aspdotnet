using HostelBusinessLogicLayer.BusinessLogic;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOSTEL_ONLINEFEECOLLECTION_EasyPayHostelOnlinePaymentRequest : System.Web.UI.Page
{
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    #region class

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelFeeCollectionController objFees = new HostelFeeCollectionController();
    StudentFees objStudentFees = new StudentFees();

    string hash_seq = string.Empty;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    public string txnid1 = string.Empty;
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    int degreeno = 0;
    int college_id = 0;

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
                        lblCollege.Text = dr["COLLEGENAME"].ToString();
                        lblAddress.Text = dr["College_Address"].ToString();
                        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    }
                }
                
                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["payStudName"]);
                lblBranch.Text = Convert.ToString(Session["Branchname"]);
                firstname.Text = Convert.ToString(Session["payStudName"]);

                lblSemester.Text = Convert.ToString(Session["paysemester"]);
                email.Text = Convert.ToString(Session["studEmail"]);
                phone.Text = Convert.ToString(Session["studPhone"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int payId = Convert.ToInt32(Session["paymentId"]);
                lblYear.Text = Session["YEARNO"].ToString();
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]));
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    lblActivityName.Text = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();
                    ViewState["ResponseUrl"] = ResponseUrl;
                    ViewState["RequestUrl"] = RequestUrl;
                    ViewState["merchentkey"] = merchentkey;
                    ViewState["saltkey"] = saltkey;
                    ViewState["accesscode"] = accesscode;
                    ViewState["hashsequence"] = hashsequence;

                    key.Value = merchentkey;
                    //hash.Value = hashsequence;
                    surl.Text = ResponseUrl;
                    furl.Text = ResponseUrl;
                    productinfo.Text = Convert.ToString(Session["idno"]);
                    udf1.Text = Convert.ToString(Session["OrgId"]);
                    udf2.Text = payId.ToString();
                    udf3.Text = Convert.ToString(Session["payactivityno"]);
                    udf4.Text = Convert.ToString(Session["Installmentno"]);
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
        int result = 0;

        string studName = firstname.Text;
        //string APPLICATION_ID = ApplicationIds.ToString();//commandArgs[1];
        string DegreeNo = "0";//commandArgs[2];
        string BranchNos = "0"; //commandArgs[3];
        //string[] BranchNos_arr = BranchNos.Split(new char[] { ',' });
        string BranchNo = "0";//BranchNos_arr[0];
        //string PayNowAmount = "1.00"; // TotalAmount.ToString();// "1.00"; //commandArgs[4];
        string PayNowAmount = Session["studAmt"].ToString();

        Session["studAmt"] = PayNowAmount;

        string RequestUrl = ViewState["RequestUrl"].ToString();
        string ResponseUrl = ViewState["ResponseUrl"].ToString();

        string redirecturl = "";
        string encryptredirecturl = "";
        string ASEKEY, merchantid;
        ASEKEY = ViewState["accesscode"].ToString();//"9966330111421998";
        merchantid = ViewState["merchentkey"].ToString(); //"600111";//Unique Identity Provided by ICICI bank to consumer.

        string UserId = Convert.ToString(Session["userno"]);

    Reprocess:
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        Random ram = new Random();
        int i = ram.Next(1, 9);
        int j = ram.Next(21, 51);
        int k = ram.Next(471, 999);
        int l = System.DateTime.Today.Day;
        int m = System.DateTime.Today.Month;
        string txnID = (i + "" + j + "" + k + "" + l + "" + m).ToString() + "-" + UserId;
        string str1 = objCommon.LookUp("ACD_DCR_ONLINE", "ORDER_ID", "ORDER_ID='" + txnID + "'");

        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }

        int jj = ram.Next(10, 99);
        //Sub merchant id coming from merchant is non numeric (Not allowed non-numeric)
        string subMerchantID = (jj).ToString();// +"-" + UserId;


        //Dynamic fields
        string Reference_No, sub_merchant_id, pgamount, StudentName, Mobile_No, EmailID, REG_NO;

        //static|fix fields
        string ReceiptType, Semester, StudentType, paymode;

        Reference_No = txnID;//"08";   //Order_ID  
        sub_merchant_id = subMerchantID;//"45";  2-digit

        pgamount = PayNowAmount;//"10";
        Mobile_No = Session["studPhone"].ToString();//;"1111111111";
        StudentName = studName;//"xyz";
        EmailID = Session["studEmail"].ToString();//"abc@gmail.com";
        // REG_NO = UserNo.ToString();//Session["APPLICATION_ID"].ToString();//"xy";//applicationID
        REG_NO = Session["regno"].ToString();
        ReceiptType = Session["ReceiptType"].ToString();
        Semester = Convert.ToString(Session["paysemester"]);
        StudentType = "Regular";//"xy";
        paymode = "9";
        string dummy = " ";
        string Regno = string.Empty;
        Regno = Session["regno"].ToString();
        Session["idno"] = Session["stuinfoidno"].ToString();
        string Idno = Session["idno"].ToString();
        string Userno = string.Empty;
        Userno = Session["userno"].ToString();
      //  string Installno = Session["Installmentno"].ToString();

        string StudentSelectedRoomNo = Session["StudentSelectedRoom"].ToString();
        string HostelNo = Session["HostelNo"].ToString();

        // dummy = "XY";
        redirecturl += RequestUrl;
        redirecturl += "merchantid=" + merchantid;
        redirecturl += "&mandatory fields=" + Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Idno + "|" + HostelNo + "|" + Userno + "|" + StudentSelectedRoomNo + "|" + Mobile_No + "|" + EmailID + "|" + dummy + "|" + dummy;//+ "|" + Semester + "|" + StudentType Mobile_No + "|456";
        redirecturl += "&optional fields=";// +city + "|" + name;
        redirecturl += "&returnurl=" + ResponseUrl;
        redirecturl += "&Reference No=" + Reference_No;
        redirecturl += "&submerchantid=" + sub_merchant_id;
        redirecturl += "&transaction amount=" + pgamount;
        redirecturl += "&Idno=" + Idno;
        redirecturl += "&HostelNo=" + HostelNo;
        redirecturl += "&Userno=" + Userno;
        redirecturl += "&StudentSelectedRoomNo=" + StudentSelectedRoomNo;
        redirecturl += "&paymode=" + paymode;


        encryptredirecturl += RequestUrl;
        encryptredirecturl += "merchantid=" + merchantid;
        encryptredirecturl += "&mandatory fields=" + encryptFile(Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Idno + "|" + HostelNo + "|" + Userno + "|" + StudentSelectedRoomNo + "|" + Mobile_No + "|" + EmailID + "|" + dummy + "|" + dummy, ASEKEY);//+ "|" + Semester + "|" + StudentType   (Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Mobile_No + "|456", ASEKEY);
        encryptredirecturl += "&optional fields=";// +encryptFile(city + "|" + name, ASEKEY);
        encryptredirecturl += "&returnurl=" + encryptFile(ResponseUrl, ASEKEY);
        encryptredirecturl += "&Reference No=" + encryptFile(Reference_No, ASEKEY);
        encryptredirecturl += "&submerchantid=" + encryptFile(sub_merchant_id, ASEKEY);
        encryptredirecturl += "&transaction amount=" + encryptFile(pgamount, ASEKEY);
        encryptredirecturl += "&Idno=" + encryptFile(Idno, ASEKEY);
        encryptredirecturl += "&HostelNo=" + encryptFile(HostelNo, ASEKEY);
        encryptredirecturl += "&Userno=" + encryptFile(Userno, ASEKEY);
        encryptredirecturl += "&StudentSelectedRoomNo=" + encryptFile(StudentSelectedRoomNo, ASEKEY);
        encryptredirecturl += "&paymode=" + encryptFile(paymode, ASEKEY);

        int semregflag = 0;
        semregflag = Convert.ToInt32(Session["SEMREG"]);


        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), ReceiptType.ToString(), Convert.ToString(paymode), Convert.ToString(Session["studAmt"]), "Not Continued", Reference_No, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["StudentSelectedRoom"]));

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Semester), Reference_No, Convert.ToDouble(pgamount), Convert.ToString(ReceiptType), Convert.ToInt32(Session["userno"]), redirecturl);
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), Reference_No, 1, Convert.ToString(Session["ReceiptType"]), redirecturl);
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Encryptedurl('" + encryptredirecturl + "');</script>", false);
        Response.Redirect(encryptredirecturl);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
    }

    #region Method

    public static string encryptFile(string textToEncrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.ECB;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }

    #endregion
}