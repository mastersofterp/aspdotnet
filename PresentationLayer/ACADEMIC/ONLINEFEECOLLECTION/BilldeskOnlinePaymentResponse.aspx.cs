﻿using System;
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class BilldeskOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objSC = new StudentController();
    OrganizationController objOrg = new OrganizationController();
    string hash_seq = string.Empty;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    string installmentno = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //SqlDataReader dr = objCommon.GetCommonDetails();
                //if (dr != null)
                //{
                //    if (dr.Read())
                //    {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //    }
                //}

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


                if (Convert.ToString(Request.Form["msg"]) != string.Empty && Convert.ToString(Request.Form["msg"]) != null)
                {
                    string MSG = Request.Form["msg"].ToString();// Request.Form["msg"].ToString();

                    string[] repoarray;
                    repoarray = MSG.Split('|');

                    string orderid = repoarray[1].ToString();
                    string txnid = repoarray[2].ToString();
                    string bankreferenceno = repoarray[3].ToString();
                    string amount = repoarray[4].ToString();
                    amount = amount.TrimStart(new Char[] { '0' });
                    string bankid = repoarray[5].ToString();
                    string bankmr_id = repoarray[6].ToString();
                    string transactiondate = repoarray[13].ToString();
                    string authstatus = repoarray[14].ToString();
                    Regno = repoarray[19].ToString();
                    string repo_msg = string.Empty;
                    Idno = repoarray[20].ToString();
                    userno = repoarray[21].ToString();
                    installmentno = repoarray[22].ToString();
                    Session["userno"] = userno;
                    lblTrasactionId.Text = txnid;
                    lblamount.Text = amount;
                    lblstudentname.Text = repoarray[16].ToString();
                   // lblRegNo.Text = Regno; -- commented bt gaurav 31_11_2023
                    lblOrderId.Text = orderid;
                    lblTransactionDate.Text = transactiondate;
                    string RecieptType = repoarray[18].ToString();
                    #region added by gaurav for crescent substitute EXAM REg
                    if (RecieptType == "REF")
                    {
                        String[] Regno1 = repoarray[19].ToString().Split('-');
                        Regno = Regno1[0];
                        String ExamNo = Regno1[1];
                        ViewState["ExamNo"] = ExamNo;
                        lblRegNo.Text = Regno1[0].ToString();
                        // String Examno=Session["ExamNo"].ToString();

                    }
                    else
                    {
                        Regno = repoarray[19].ToString();
                        lblRegNo.Text = Regno.ToString();
                    }
                    #endregion 
                    lblResponsecode.Text = authstatus;
                    DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_NAME", "UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG", "UA_NO=" + Convert.ToInt32(userno), string.Empty);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                        Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                        Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                        Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                        Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();

                    }

                    Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                    Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                    Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                    Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");

                    Session["payment"] = "payment";
                    Session["orgid"] = objCommon.LookUp("REFF", "OrganizationId", "");
                    if (authstatus == "0300")
                    {

                        divSuccess.Visible = true;
                        int output = 0;
                        if (Convert.ToInt32(installmentno) > 0)
                        {
                            output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, RecieptType, orderid, txnid, "O", "1", amount, "Success", Convert.ToInt32(installmentno), MSG);
                        }
                        else
                        {
                            output = objFees.InsertOnlinePayment_DCR(Idno, RecieptType, orderid, txnid, "O", "1", amount, "Success", Regno, MSG);
                        }

                        #region Retest Exam
                        // Addeded by gaurav 23-09-2022 for retest 
                        if (RecieptType == "REF")
                        {

                            int session = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "TOP 1 MAX(SESSIONNO)", "IDNO=" + Idno + " AND Examno=" + Convert.ToInt32(ViewState["ExamNo"])));
                           // DataSet dsstudent = null; -- Commented by gaurav 30_11_2023
                            // dsstudent = objSC.GetRetestStudentDetailsExam(Convert.ToInt32(Idno), session);---- Commented by gaurav 30_11_2023
                            #region
                            string SP_Name = "PKG_ACD_UPDATE_RETEST_STUDENT_FLAG_CRESCENT";
                            string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_EXAMNO,@P_OUTPUT";
                            string Call_Values = "" + Idno + "," + Convert.ToInt32(session) + "," + Convert.ToInt32(ViewState["ExamNo"].ToString()) + ",0";
                            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                            #endregion

                        }
                        #endregion

                        #region Revaluation
                        if (Session["orgid"].ToString() == "2")
                        {
                            if (RecieptType == "PRF" || RecieptType == "RF" || RecieptType == "SEF")        //Added dt on 18112022
                            {
                                int result = 0;
                                result = objFees.OnlinePhotoRevalPaymentDetails(txnid, orderid, amount, authstatus, "Success", repo_msg, authstatus, bankreferenceno, RecieptType, 1); //1 for photo copy
                            }
                        }
                        #endregion

                        if (output == -99)
                        {
                            divSuccess.Visible = false;
                            divFailure.Visible = true;

                            objFees.InsertOnlinePaymentlog(Idno, RecieptType, "O", amount, "Payment Fail", orderid);
                        }
                        else
                        {
                            ViewState["out"] = output;
                            btnReciept.Visible = true;
                        }

                    }
                    else
                    {
                        divFailure.Visible = true;
                        divSuccess.Visible = false;
                        btnReciept.Visible = false;
                        objFees.InsertOnlinePaymentlog(Idno, RecieptType, "O", amount, "Payment Fail", orderid);

                    }
                }
  
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

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

        byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

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
   
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'");
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));

            int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(IDNO)));
            //int IDNO = Convert.ToInt32(ViewState["IDNO"]);
            //string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + ViewState["IDNO"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(ViewState["order_id"]) + "'");
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(college_id) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_UA_NAME=" + Session["UAFULLNAME"];

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


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }
  


    protected void btnReciept_Click(object sender, EventArgs e)
    {
        Session["idno"] = Idno;
        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
        string ua_type = objCommon.LookUp("acd_dcr", "RECIEPT_CODE", "IDNO=" + Convert.ToInt32(IDNO) + "AND ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'");  //Convert.ToInt32(Session["idno"]));  
        if (ua_type == "REF")//added by gaurav for substitute report
        {
            ShowReport("OnlineFeePayment", "rptOnlineReceipt_Retest.rpt");
        }
        else if (ua_type == "PRF" || ua_type == "RF")
        {
            ShowReportPhotoCopy("Photo Copy Registration Slip", "rptOnlineReceiptPhotoCopyCRESCENT.rpt");
        }
        else if (ua_type == "SEF")
        {
            ShowReportPhotoCopy("Photo Copy Registration Slip", "rptOnlineReceiptSupplyExam.rpt");
        }
        else
        {
            //ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
            ShowReport("OnlineFeePayment", "rptOnlineReceipt_New.rpt");
        }
    }


    private void ShowReportPhotoCopy(string reportTitle, string rptFileName)
    {
        try
        {
            Session["username"] = "Admin";
            Session["userno"] = 1;
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
            int collegecode = Convert.ToInt32(objCommon.LookUp("REFF", "COLLEGE_CODE", ""));
            int revalType = 0;
            string rec_code = objCommon.LookUp("ACD_DCR", "DISTINCT RECIEPT_CODE", "ORDER_ID = '" + Convert.ToString(lblOrderId.Text) + "'");
            if (rec_code == "PRF")
            {
                revalType = 1;
            }
            else if (rec_code == "RF")
            {
                revalType = 2;
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (rec_code == "PRF" || rec_code == "RF")
            {
                url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_REVALTYPE=" + revalType;
            }
            else  //Supplementry Exam
            {
                url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_ORDERID=" + Convert.ToString(lblOrderId.Text) + ",@P_REVALTYPE=" + rec_code;
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            ////To open new window from Updatepanel
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