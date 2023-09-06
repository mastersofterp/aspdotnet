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

public partial class ACADEMIC_ONLINEFEECOLLECTION_PayUOnlinePaymentResponse_Semester_Admission : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu = new StudentController();
    SemesterRegistration objsem = new SemesterRegistration();
    string hash_seq = string.Empty;
    int degreeno = 0;
    int college_id = 0;
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
                            objStu.AddSemesterRegistration_Online(objsem, lblOrderId.Text);


                            }
                        else
                            {
                            output = objFees.InsertOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Regno, "-");
                            objStu.AddSemesterRegistration_Online(objsem, lblOrderId.Text);


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
        ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
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
}