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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Security.Cryptography.X509Certificates;
using mastersofterp_MAKAUAT;
using System.Net.Security;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;


public partial class PayUOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu=new StudentController ();
    SemesterRegistration objsem = new SemesterRegistration();
    string hash_seq = string.Empty;
    int degreeno = 0;
    int college_id = 0;
    #endregion

    FeeCollectionController feeController = new FeeCollectionController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

    string UserFirstPaymentStatus = string.Empty;
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
                        Session["OrgId"] = dr["OrganizationId"].ToString();
                        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    }
                }

                string[] merc_hash_vars_seq;
                string merc_hash_string = string.Empty;
                string merc_hash = string.Empty;
                string order_id = string.Empty;
                string amount = string.Empty;
                string firstname = string.Empty;
                string emailId = string.Empty;
                string Idno = string.Empty;
                string recipt = string.Empty;
                string saltkey = string.Empty;
                int payid = 0;
                int orgid = 0;
                string hash_seq = string.Empty;
               //hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
                firstname = Request.Form["firstname"];
                emailId = Request.Form["email"];
                order_id = Request.Form["txnid"];
                ViewState["order_id"] = order_id;
                amount = Request.Form["amount"];
                Idno = Request.Form["productinfo"];
                orgid = Convert.ToInt32(Request.Form["udf1"]);
                payid = Convert.ToInt32(Request.Form["udf2"]);
                int payactivityno = Convert.ToInt32(Request.Form["udf3"]);
                int installmentno = Convert.ToInt32(Request.Form["udf4"]);
                //int installmentno = Convert.ToInt32(0);
                ViewState["IDNO"] = Idno;
                //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.
                if (Session["OrgId"].ToString() == "6")
                {
                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                }
                if (Session["OrgId"].ToString() == "8")
                {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                }
                //**********************************End by Nikhil L.********************************************//
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(orgid, payid, payactivityno, degreeno, college_id);
                 if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                 {
                     saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                     hash_seq = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                 }
                string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString());

                DataSet ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT S ON(S.IDNO = U.UA_IDNO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO = S.BRANCHNO)", "UA_NAME", "UA_NO,UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG,B.LONGNAME", "UA_IDNO=" + Convert.ToInt32(Idno), string.Empty);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                    Session["userno"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                    Session["branchname"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                }

                Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");
                string semester = objCommon.LookUp("ACD_DCR_TEMP", "SEMESTERNO", "IDNO=" + ViewState["IDNO"].ToString());
                lblRegNo.Text = Regno;
                lblstudentname.Text = firstname;
                lblOrderId.Text = order_id;
                lblamount.Text = amount;
                lblTransactionDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                string mihpayid = Request.Form["mihpayid"].ToString();
                lblBranch.Text = Convert.ToString(Session["branchname"]);
                lblSemester.Text = semester;
                lblTrasactionId.Text = mihpayid;

                if (Request.Form["status"] == "success")
                {
                    merc_hash_vars_seq = hash_seq.Split('|');
                    Array.Reverse(merc_hash_vars_seq);
                    merc_hash_string = saltkey + "|" + Request.Form["status"];
                    //Check for presence of additionalCharges and include in hash
                    if (Request.Form["additionalCharges"] != null)
                        merc_hash_string = Request.Form["additionalCharges"] + "|" + saltkey + "|" + Request.Form["status"];

                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");

                    }
                    //Calculate response hash to verify	
                    merc_hash = Generatehash512(merc_hash_string).ToLower();
                    if (merc_hash != Request.Form["hash"])
                    //if (merc_hash != Session["hashvalue"])
                    {
                        //Value didn't match that means some paramter value change between transaction 
                        Response.Write("Hash value did not matched");
                    }
                    else
                    {
                        //lblTrasactionStatus.Text = "Success";

                    if (Session["OrgId"].ToString() == "5")
                        {
                        string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                        string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");
                        if (UA_IDNO == UA_NAME)
                            {
                            UserFirstPaymentStatus = "5151";
                            ViewState["First_PaymentStatus"] = "5151";

                            }
                        else
                            {
                            ViewState["First_PaymentStatus"] = "0";
                            }
                        }
                    else
                        {
                        ViewState["First_PaymentStatus"] ="0";
                        }

                        divSuccess.Visible = true;
                        divFailure.Visible = false;
                        int result = 0;
                        string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                        objsem.IdNo = Convert.ToInt32(Session["idno"]);
                        objsem.SESSIONNO = Convert.ToInt32(Session["currentsession"].ToString());
                        objsem.SemesterNO = Convert.ToInt32(lblSemester.Text);
                        objsem.paymentMode = 1;
                        objsem.OfflineMode = 0;
                        objsem.Total_Amt = Convert.ToDecimal(lblamount.Text);

                        objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                        objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");
                        int output = 0;
                        if (Convert.ToInt32(installmentno) > 0)
                        {
                            output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");
                            if (output != 1)
                                {

                                if (ViewState["First_PaymentStatus"] == "5151")
                                    {
                                    string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                                    UPDATE_USER(UA_IDNO, 1);

                                    Sendmail();

                                    }
                                else
                                    {

                                    }
                              }
                        }
                        else
                        {

                            output = objFees.InsertOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Regno, "-");

                            if (output != 1)
                                {
                                if (Session["OrgId"].ToString() == "5")
                                    {
                                    if (ViewState["First_PaymentStatus"].Equals("5151"))
                                        {
                                        string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                                        UPDATE_USER(UA_IDNO, 1);

                                        Sendmail();
                                       // ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
                                        //objCommon.DisplayMessage(this.Page,"Your USERNAME & Password send will be send on Your Registered Email address",this.Page);
                                        }
                                    else
                                        {

                                        }
                                    }
                                }

                            //AddSemesterRegistration_Online
                        }
                        btnPrint.Visible = true;
                    }
                }
                else
                {
                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);
                    
                    //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                    btnPrint.Visible = false;
                }
                
                
               
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region Method
    //public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    //{
    //    try
    //    {
    //        //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
    //        //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
    //        //Img.ContentId = "MyImage";   

    //        ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
    //        //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
    //        //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
    //        string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
    //        string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

    //        MailMessage msg = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();

    //        msg.From = new MailAddress(fromAddress, "NIT GOA");
    //        msg.To.Add(new MailAddress(ToID));

    //        msg.Subject = "Your transaction with MAKAUT";

    //        const string EmailTemplate = "<html><body>" +
    //                                 "<div align=\"left\">" +
    //                                 "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                                  "<tr>" +
    //                                  "<td>" + "</tr>" +
    //                                  "<tr>" +
    //                                 "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
    //                                 "</tr>" +
    //                                 "<tr>" +
    //                                 "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
    //                                 "</tr>" +
    //                                 "</table>" +
    //                                 "</div>" +
    //                                 "</body></html>";
    //        StringBuilder mailBody = new StringBuilder();
    //        //mailBody.AppendFormat("<h1>Greating !!</h1>");
    //        mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(messBody5);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg1);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg2);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(messBody4);
    //        mailBody.AppendFormat("<br />");
    //        string Mailbody = mailBody.ToString();
    //        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
    //        msg.IsBodyHtml = true;
    //        msg.Body = nMailbody;

    //        smtp.Host = "smtp.gmail.com";

    //        smtp.Port = 587;
    //        smtp.UseDefaultCredentials = true;
    //        smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
    //        smtp.EnableSsl = true;
    //        smtp.Send(msg);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

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




    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        //Response.Redirect(returnpageurl);
        Response.Redirect("~/default.aspx");
    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
    string ptype = (objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_PAYMENTTYPE P ON (A.PTYPE=P.PAYTYPENO) ", "PAYTYPENAME", "IDNO=" + Session["idno"].ToString()));
    if (ptype == "Provisional" && Session["OrgId"].ToString() == "5")
        {
        //ShowReport("InstallmentOnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));

        ShowReport("OnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt");
        return;
        }
    else
        {
        ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);

            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + ViewState["IDNO"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(ViewState["order_id"]) + "'");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void UPDATE_USER(string UA_NO,int FirstTimePay)
        {
        try
            {
            string UA_PWD = string.Empty;
            string password = string.Empty;
            //string student_Name = "ROHIT";
            int IDNO = 0;
            string REGNO = string.Empty;
            string Email = string.Empty;
            string UA_ACC = string.Empty;
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 5)
                {
                IDNO = Convert.ToInt32(Session["IDNO"]);
                //string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UANAME = '" + UA_IDNO + "'");
                REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = '" + Session["IDNO"] + "'");

                string Username = string.Empty;
                //string Password = string.Empty;
                //string Userno = objCommon.LookUp("ACD_STUDENT", "USERNO", "IDNO=" + IDNO);
                //Email = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + IDNO);
                //Username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
                //Password = objCommon.LookUp("ACD_USER_REGISTRATION", "User_Password", "USERNO=" + Userno);
                //Username = Session["IDNO"].ToString();
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(REGNO.ToString());
               // UA_ACC = "0,500,76";
                //REGNO = Username.ToString();
                }
            else
                {
                IDNO = Convert.ToInt32(Session["IDNO"]);
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(IDNO.ToString());
               // UA_ACC = "0,500,76";
                REGNO = IDNO.ToString();
                }

            CustomStatus CS = (CustomStatus)feeController.UpdateUser(REGNO, UA_PWD, IDNO, FirstTimePay);

            //string sp_Name = string.Empty;
            //string sp_Parameter = string.Empty;
            //string sp_Call = string.Empty;
            //int outP = 0;
            //sp_Name = "PKG_BIND_ADMBATCH_DROPDOWN_APPLIES_STUDENT_LIST";
            //sp_Parameter = "@P_OUT";
            //sp_Call = "" + outP + "";
            //DataSet ds = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameter, sp_Call);
            //if (ds.Tables[0].Rows.Count > 0)
            //    {
            //    ddlAdmbatch.DataSource = ds;
            //    ddlAdmbatch.DataTextField = "BATCHNAME";
            //    ddlAdmbatch.DataValueField = "BATCH";
            //    ddlAdmbatch.DataBind();
            //    }
            }
        catch (Exception ex)
            {
            throw;
            }
        }
     private DataSet getModuleConfig()
        {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
        }
    public void Sendmail()
        {
        string email_type = string.Empty;
        string Link = string.Empty;
        int sendmail = 0;
        string subject = string.Empty;
        string srnno = string.Empty;
        string pwd = string.Empty;
        int status = 0;
        string IDNO = Session["IDNO"].ToString();

       // int IDNOnew = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO='" + IDNO + "'"));

        //string DAmount = Convert.ToString(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0) TOTAL_AMT", "IDNO=" + IDNOnew));

        string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);

        string Username = string.Empty;
        string Password = string.Empty;

        string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENAME, ' in ',B.LONGNAME)", "IDNO=" + Session["IDNO"].ToString());

        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
        // Username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
        //  Password = objCommon.LookUp("ACD_USER_REGISTRATION", "User_Password", "USERNO=" + Userno);
        Username = REGNO;
       // objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
        Password = REGNO;

        Session["Enrollno"] = srnno;
        DataSet ds = getModuleConfig();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
            email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            Link = ds.Tables[0].Rows[0]["LINK"].ToString();
            sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

            if (sendmail == 1)
                {
                subject = "New MIS Login Credentials";
             //string message = "<p>Dear :<b>" + Name + "</b> </p>";
             //       message += "<p>Your New Login Credentials Are Below Please Use this Credentails to Login in ERP from Following Link.</P>";
             //       message+="<p style=font-weight:bold;> " + MISLink + " </p>";
             //       message+="<p>Username   : " + Username + " <br/>Password: " + Password + "</p>";

                string message = "";
                message += "<p>Dear :<b>" + Name + "</b> </p>";
                message += "<p><b>" + Branchname + "</>b</p>";
                message += "<p>Your fees have been submitted successfully and you have been registered for the program mentioned above.Your new Login credentials are as follows</p><p>" + MISLink + " </p><p>Username   : " + Username + " <br/>Password: " + Password + "</p>";
                message+="<p>Note for Provisional Registration only:</p>";
                message += "<p>All the documents must be uploaded on URL: <b>" + MISLink + "</b>";
                message += "<p>Process of fee payment: Login using above credentials in <b>" + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.: ";
                message += "<p>The fee payment should be made within 7 days of receiving this mail/letter, after which your claim for admission may be requested.</p>";
                message += "<p style=font-weight:bold;>Thanks<br>Team Admissions<br>admissions@jecrcu.edu.in<br>JECRC University, Jaipur</p>";

                //if (email_type == "1" && email_type != "")
                //    {
                //    int reg = TransferToEmail(EmailID, message, subject);
                //    }
                //else if (email_type == "2" && email_type != "")
                //    {
                //    Task<int> task = Execute(message, EmailID, subject);
                //    status = task.Result;
                //    }
                //if (email_type == "3" && email_type != "")
                //    {
                //    OutLook_Email(message, EmailID, subject);
                //    }

                status = objSendEmail.SendEmail(EmailID, message, subject); //Calling Method

                }
            }

        if (status == 1)
            {
            //objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
            }
        else
            {
            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
            } 

        }

    
    protected void btnredirect_Click(object sender, EventArgs e)
        {
        //Response.Redirect("FeeCollectionOptions.aspx");
        }
}