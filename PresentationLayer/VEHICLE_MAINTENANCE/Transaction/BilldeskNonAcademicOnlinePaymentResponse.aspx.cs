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
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;

public partial class BilldeskOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objSC = new StudentController();
    VMController objVMCont = new VMController();

    string hash_seq = string.Empty;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    string installmentno = string.Empty;
    string BusBookingDetails = string.Empty;
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
                    //installmentno = repoarray[22].ToString();
                    Session["userno"] = userno;
                    lblTrasactionId.Text = txnid;
                    lblamount.Text = amount;
                    lblstudentname.Text = repoarray[16].ToString();
                    lblRegNo.Text = Regno;
                    lblOrderId.Text = orderid;
                    lblTransactionDate.Text = transactiondate;
                    string RecieptType = repoarray[18].ToString();

                    #region 
                    // Added By Shaikh Juned 06-04-2023 for get Route Stope and Bus Seat number
                    BusBookingDetails = repoarray[22].ToString();
                    string[] busdetailsarray;
                    busdetailsarray = BusBookingDetails.Split('-');
                    string BusRoute = busdetailsarray[0].ToString();
                    string BusStope = busdetailsarray[1].ToString();
                    string BusSeat = busdetailsarray[2].ToString();
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
                    
                    if (authstatus == "0300")
                    {

                        divSuccess.Visible = true;
                        int output = 0;
                        //if (Convert.ToInt32(installmentno) > 0)
                        //{
                        //    output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, RecieptType, orderid, txnid, "O", "1", amount, "Success", Convert.ToInt32(installmentno), MSG);
                        //}
                        //else
                        //{
                        output = objVMCont.InsertOnlinePayment_Non_DCR(Idno, RecieptType, orderid, txnid, "O", "1", amount, "Success", Regno, MSG);
                        //}

                        //#region Retest Exam
                        //// Addeded by gaurav 23-09-2022 for retest 
                        //if (RecieptType == "REF")
                        //{

                        //    int session = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "TOP 1 (SESSIONNO)", "IDNO=" + Idno));
                        //    DataSet dsstudent = null;
                        //    dsstudent = objSC.GetRetestStudentDetailsExam(Convert.ToInt32(Idno), session);

                        //}
                        //#endregion

                        #region Insert Bus Booking Data 
                        // Addeded by Shaikh Juned 05-04-2023 for retest 
                        if (RecieptType == "BFR")
                        {
                           //int sessionbfr=Convert.ToInt32(objCommon.FillDropDown( "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "IS_CURRENT_FY=1 and ACTIVE_STATUS=1", ""));
                            int sessionbfr = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "IS_CURRENT_FY=1 and ACTIVE_STATUS=1"));
                            DataSet ds1 = objCommon.FillDropDown("ACD_STUDENT", "SEMESTERNO,SECTIONNO", "IDNO", "IDNO = '" + Idno + "'", "");
                            int Semesterbfr =Convert.ToInt32( ds1.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                            //int sessionbfr = Convert.ToInt32(ds1.Tables[0].Rows[0]["SECTIONNO"].ToString());
                            DataSet dsstudent = null;

                            dsstudent = objVMCont.AddBusBookingDetails(Convert.ToInt32(Idno), lblstudentname.Text, sessionbfr, Semesterbfr, Convert.ToInt32(BusRoute), Convert.ToInt32(BusStope), Convert.ToInt32(BusSeat), Convert.ToDecimal(lblamount.Text), orderid);

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

            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
            int collegeCode =Convert.ToInt32(objCommon.LookUp("Reff", "College_code", ""));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

            url += "&param=@P_COLLEGE_CODE="+collegeCode+",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }
    protected void btnReciept_Click(object sender, EventArgs e)
    {
        //string ua_type = objCommon.LookUp("acd_dcr", "RECIEPT_CODE", "IDNO=" + Convert.ToInt32(Session["idno"]));
        //if (ua_type == "REF")//added by gaurav for substitute report
        //{
        //    ShowReport("OnlineFeePayment", "rptOnlineReceipt_Retest.rpt");
        //}
        //else
        //{
        ShowReport("OnlineFeePayment", "rptBusOnlineReceipt.rpt");
        //}
    }


   
}