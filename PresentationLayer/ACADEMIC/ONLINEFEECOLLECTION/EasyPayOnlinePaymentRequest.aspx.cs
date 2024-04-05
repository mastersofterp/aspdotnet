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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using CCA.Util;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic.Academic;
using BusinessLogicLayer.BusinessEntities.Academic;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class ACADEMIC_ONLINEFEECOLLECTION_EasyPayOnlinePaymentRequest : System.Web.UI.Page
{

    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    OrganizationController objOrg = new OrganizationController();

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
                //SqlDataReader dr = objCommon.GetCommonDetails();
                //if (dr != null)
                //    {
                //    if (dr.Read())
                //        {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //        }
                //    }

                    DataSet Orgds = null;
                    var OrgId = objCommon.LookUp("REFF", "OrganizationId", "");
                    Orgds = objOrg.GetOrganizationById(Convert.ToInt32(OrgId));
                    byte[] imgData = null;
                    if (Orgds.Tables != null)
                    {
                        if (Orgds.Tables[0].Rows.Count > 0)
                        {

                            if (Orgds.Tables[0].Rows[0]["Logo"] != DBNull.Value)
                            {
                                imgData = Orgds.Tables[0].Rows[0]["Logo"] as byte[];
                                imgCollegeLogo.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                            }
                            else
                            {
                                // hdnLogoOrg.Value = "0";
                            }

                        }
                    }

                //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.

                if (Session["OrgId"].ToString() == "6")
                    {
                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    }
                if (Session["OrgId"].ToString() == "8")
                    {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                    }

                //**********************************End by Nikhil L.********************************************//


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
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]), Convert.ToInt32(degreeno), Convert.ToInt32(college_id));
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
                    //udf4.Text = Convert.ToString(Session["Installmentno"]);
                    udf4.Text = Convert.ToString(Session["Installmentno"]);

                    }
                }
            catch (Exception ex)
                {
                Response.Write(ex.Message);
                }
            }
        }

    #region Method
    public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
        {
        try
            {
            //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
            //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            //Img.ContentId = "MyImage";   

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
            //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
            string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
            string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "NIT GOA");
            msg.To.Add(new MailAddress(ToID));

            msg.Subject = "Your transaction with MAKAUT";

            const string EmailTemplate = "<html><body>" +
                                     "<div align=\"left\">" +
                                     "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                      "<tr>" +
                                      "<td>" + "</tr>" +
                                      "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</div>" +
                                     "</body></html>";
            StringBuilder mailBody = new StringBuilder();
            //mailBody.AppendFormat("<h1>Greating !!</h1>");
            mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody5);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg1);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg2);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody4);
            mailBody.AppendFormat("<br />");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            msg.IsBodyHtml = true;
            msg.Body = nMailbody;

            smtp.Host = "smtp.gmail.com";

            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    public string Generatehash512(string text)
        {

        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
            {
            hex += String.Format("{0:x2}", x);
            }
        return hex;

        }
    #endregion




    protected void btnPay_Click(object sender, EventArgs e)
        {
        //string USERNO = UserNo.ToString();//commandArgs[0];

        int result = 0;

        string studName = firstname.Text;
        //string APPLICATION_ID = ApplicationIds.ToString();//commandArgs[1];
        string DegreeNo = "0";//commandArgs[2];
        string BranchNos = "0"; //commandArgs[3];
        //string[] BranchNos_arr = BranchNos.Split(new char[] { ',' });
        string BranchNo = "0";//BranchNos_arr[0];
        //string PayNowAmount = "1.00"; // TotalAmount.ToString();// "1.00"; //commandArgs[4];
        string PayNowAmount = Session["studAmt"].ToString();// "1.00"; //commandArgs[4];

       // Session["APPLICATION_USERNO"] = USERNO;
        //Session["APPLICATION_ID"] = APPLICATION_ID;
        //Session["DegreeNo"] = DegreeNo;
        ////Session["BranchNo"] = BranchNo;
       // string DegreeBranchNos = DegreeBranchs.ToString();
        //string ApplicationIdNriFees = ApplicationIdnriFees.ToString();
        Session["studAmt"] = PayNowAmount;

        //DataSet ds_ApplicantInfo = objCommon.FillDropDown("ACD_USER_REGISTRATION", "FIRSTNAME,LASTNAME,EMAILID,MOBILENO", "", " USERNO = " + USERNO, "USERNO DESC");
        //if (ds_ApplicantInfo.Tables[0].Rows.Count >= 1)
        //{
        //    studName = ds_ApplicantInfo.Tables[0].Rows[0]["FIRSTNAME"].ToString() + " " + ds_ApplicantInfo.Tables[0].Rows[0]["LASTNAME"].ToString();
        //    Session["studEmail"] = ds_ApplicantInfo.Tables[0].Rows[0]["EMAILID"].ToString();
        //    Session["studPhone"] = ds_ApplicantInfo.Tables[0].Rows[0]["MOBILENO"].ToString();
        //}


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
        Semester =Convert.ToString(Session["paysemester"]);
        StudentType = "Regular";//"xy";
        paymode = "9";
        string dummy = " ";
        string Regno = string.Empty;
        Regno = Session["regno"].ToString();
        Session["idno"] = Session["stuinfoidno"].ToString();
        string Idno = Session["idno"].ToString();
        string Userno = string.Empty;
        Userno = Session["userno"].ToString();
        string Installno = Session["Installmentno"].ToString();
        string StudName = objCommon.LookUp("ACD_STUDENT", "CONCAT(STUDNAME,' - ',REGNO) as STUDNAME", "IDNO='" + Session["idno"] + "'");
       // dummy = "XY";
        redirecturl += RequestUrl;
        redirecturl += "merchantid=" + merchantid;
        redirecturl += "&mandatory fields=" + Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Idno + "|" + StudName + "|" + Installno + "|" + Semester + "|" + Mobile_No + "|" + EmailID + "|" + ReceiptType + "|" + dummy + "|";//+ "|" + Semester + "|" + StudentType Mobile_No + "|456";
        redirecturl += "&optional fields=" ;// +city + "|" + name;
        redirecturl += "&returnurl=" + ResponseUrl;
        redirecturl += "&Reference No=" + Reference_No;
        redirecturl += "&submerchantid=" + sub_merchant_id;
        redirecturl += "&transaction amount=" + pgamount;
        redirecturl += "&Idno=" + Idno;
        redirecturl += "&Studname=" + StudName;
        redirecturl += "&Userno=" + Userno;
        redirecturl += "&Semester=" + Semester;
        redirecturl += "&ReceiptType=" + ReceiptType;
        redirecturl += "&Installno=" + Installno;
        redirecturl += "&paymode=" + paymode;
        encryptredirecturl += RequestUrl;
        encryptredirecturl += "merchantid=" + merchantid;
        encryptredirecturl += "&mandatory fields=" + encryptFile(Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Idno + "|" + StudName + "|" + Installno + "|" + Semester + "|" + Mobile_No + "|" + EmailID + "|" + ReceiptType + "|" + dummy + "|", ASEKEY);//+ 

        //encryptredirecturl += "&mandatory fields=" + encryptFile(Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Idno + "|" + Semester + "|" + Userno + "|" + Installno + "|" + Mobile_No + "|" + EmailID + "|" + dummy + "|" + dummy, ASEKEY);//+ "|" + Semester + "|" + StudentType   (Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + Mobile_No + "|456", ASEKEY);
        encryptredirecturl += "&optional fields=";// +encryptFile(city + "|" + name, ASEKEY);
        encryptredirecturl += "&returnurl=" + encryptFile(ResponseUrl, ASEKEY);
        encryptredirecturl += "&Reference No=" + encryptFile(Reference_No, ASEKEY);
        encryptredirecturl += "&submerchantid=" + encryptFile(sub_merchant_id, ASEKEY);
        encryptredirecturl += "&transaction amount=" + encryptFile(pgamount, ASEKEY);
        encryptredirecturl += "&Idno=" + encryptFile(Idno, ASEKEY);
        encryptredirecturl += "&Studname=" + encryptFile(StudName, ASEKEY);
        encryptredirecturl += "&Userno=" + encryptFile(Userno, ASEKEY);
        encryptredirecturl += "&Semester=" + encryptFile(Semester, ASEKEY);
        encryptredirecturl += "&ReceiptType=" + encryptFile(ReceiptType, ASEKEY);
        encryptredirecturl += "&Installno=" + encryptFile(Installno, ASEKEY);
        encryptredirecturl += "&paymode=" + encryptFile(paymode, ASEKEY);
        int semregflag=0;
        semregflag = Convert.ToInt32(Session["SEMREG"]);
       
        //Decryption Logic Done
        ////string ss = encryptFile(Reference_No + "|" + sub_merchant_id + "|" + pgamount + "|" + StudentName + "|" + Mobile_No + "|" + EmailID + "|" + REG_NO + "|" + ReceiptType + "|" + Semester + "|" + StudentType, ASEKEY);////encryptFile(paymode, ASEKEY);
        ////ss = decryptFile(ss, ASEKEY);


        /*
          Reference_No----->txnID (OrderID)
          REG_NO      ----->APPLICATION_ID
          sub_merchant_id-->For EazyPay Gateway
         *             
        */
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), ReceiptType.ToString(), Convert.ToString(paymode), Convert.ToString(Session["studAmt"]), "Not Continued", Reference_No);

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
            {
            result = InsertInstallmentOnlinePayment_TempDCR_EASYPAY(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Semester), Reference_No, Convert.ToDouble(pgamount), Convert.ToString(ReceiptType), Convert.ToInt32(Session["userno"]), "-", semregflag);
            }
        else
            {
            result = InsertOnlinePayment_TempDCR_EASYPAY(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), Reference_No, 1, Convert.ToString(Session["ReceiptType"]), "-",semregflag);
            }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Encryptedurl('" + encryptredirecturl + "');</script>", false);
         Response.Redirect(encryptredirecturl);

        }


    protected void btnBack_Click(object sender, EventArgs e)
        {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
        }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)   // post form
        {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
            {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
            }


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
    public static string decryptFile(string textToEncrypt, string key)
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
        ICryptoTransform transform = rijndaelCipher.CreateDecryptor(rijndaelCipher.Key, rijndaelCipher.IV);
        string plaintext = null;
        byte[] cipherText = Convert.FromBase64String(textToEncrypt);
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                   transform, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }

        return plaintext;
    }


    public static string decryptFile_0(string encryptedText, string aes_key)
    {
        string decrypted = null;

        byte[] pwdBytes = Encoding.UTF8.GetBytes(aes_key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);


        ////aes_key = "AXe8YwuIn1zxt3FPWTZFlAa14EHdPAdN9FaZ9RQWihc=";
        ////string aes_iv = "bsxnWolsAyO7kCfWuyrnqg==";
        byte[] cipher = Convert.FromBase64String(encryptedText);
        string str = Encoding.Default.GetString(cipher);

        using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        {
            aes.Key = keyBytes;// Convert.FromBase64String(aes_key);
            aes.IV = keyBytes;//Convert.FromBase64String(aes_iv);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 0x80;
            aes.BlockSize = 0x80;

            ICryptoTransform dec = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipher))
            {
                using (CryptoStream cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        decrypted = sr.ReadToEnd();
                    }
                }
            }
        }

        return decrypted;
    }


    public int InsertOnlinePayment_TempDCR_EASYPAY(int IDNO, int SESSIONNO, int SEMESTERNO, string ORDER_ID, int PAYSERVICETYPE, string RECEIPTCODE, string msg, int SemREG)
        {
        int retStatus = 0;
        try
            {
            SQLHelper objSqlHelper = new SQLHelper(_connectionString);
            SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_PAYSERVICETYPE", PAYSERVICETYPE),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_SEMREG",SemREG),
                            new SqlParameter("@P_MESSAGE",msg),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
            param[param.Length - 1].Direction = ParameterDirection.Output;
            object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_DCR_EASYPAY", param, true);

            if (ret != null && ret.ToString() != "-99")
                retStatus = Convert.ToInt32(ret);
            else
                retStatus = -99;
            }
        catch (Exception ex)
            {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
            }
        return retStatus;
        }


    public int InsertInstallmentOnlinePayment_TempDCR_EASYPAY(int IDNO, int Dmno, int SEMESTERNO, string ORDER_ID, double amount, string RECEIPTCODE, int uano, string data, int SemREG)
        {
        int retStatus = 0;
        try
            {
            SQLHelper objSqlHelper = new SQLHelper(_connectionString);
            SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_DM_NO", Dmno),
                            new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_AMOUNT", amount),
                            new SqlParameter("@P_RECIEPT_CODE", RECEIPTCODE),
                            new SqlParameter("@P_UANO", uano),
                            new SqlParameter("@P_SEMREG",SemREG),
                            new SqlParameter("@P_MESSAGE", data),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
            param[param.Length - 1].Direction = ParameterDirection.Output;
            object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSTALLMENT_INSERT_ONLINE_PAYMENT_DCR_EASYPAY", param, true);

            if (ret != null && ret.ToString() != "-99")
                retStatus = Convert.ToInt32(ret);
            else
                retStatus = -99;
            }
        catch (Exception ex)
            {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertInstallmentOnlinePayment_TempDCR-> " + ex.ToString());
            }
        return retStatus;
        }

    
}