//======================================================================================
// PROJECT NAME  : RFC CC
// MODULE NAME   : HOSTEL
// PAGE NAME     : EasyPay HostelOnlinePaymentResponse form
// CREATION DATE : 21-JULY-2023
// ADDED BY      : MR. SAURABH LONARE
// DESCERIPTION  : THIS FORM GET RESPONSE FROM EASY PAY PAYMENT GATEWAY THEN ADD PAYMENT RECORD IN DCR TABLE AND ROOM BOOK IF PAYMENT SUCCESSFUL
//======================================================================================
using HostelBusinessLogicLayer.BusinessLogic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOSTEL_ONLINEFEECOLLECTION_EasyPayHostelOnlinePaymentResponse : System.Web.UI.Page
{
    #region class

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelFeeCollectionController objFees = new HostelFeeCollectionController();

    RoomAllotment roomAllotment = new RoomAllotment();
    RoomAllotmentController raController = new RoomAllotmentController();

    string hash_seq = string.Empty;
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    string HostelSessionNo = string.Empty;
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
                        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    }
                }
                string responsevalue = string.Empty;
                string resposestring = string.Empty;
                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                {
                    responsevalue = HttpContext.Current.Request.Form[key];
                    resposestring = resposestring + key + ":'" + responsevalue + "'|";
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ResponseAlert", "alert(" + resposestring + ")", true);
                string messageString = string.Empty;
                string urlPath = string.Empty;
                urlPath = HttpContext.Current.Request.Url.AbsoluteUri;
                string ResponseCode = HttpContext.Current.Request.Form["Response Code"];
                string mandatoryFields = Request.Form["mandatory fields"];
                string[] mandatoryFieldsArray = mandatoryFields.Split(new char[] { '|' });
               // lblmessage.Text = resposestring;
                // lblmandfileds.Text = Request.Form["mandatory fields"];
                // lblmerchantid.Text = Request.Form["merchantid"];
                lblOrderId.Text = Request.Form["ReferenceNo"];
                ViewState["Order_id"] = Request.Form["ReferenceNo"];

                lblamount.Text = mandatoryFieldsArray[2];
                lblTransactionDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                string Idno = string.Empty;
                Idno = mandatoryFieldsArray[3];

                string Studname = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Idno)));
                string BRANCHNO = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + Convert.ToInt32(Idno)));
                string BranchName = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + BRANCHNO);
                string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Idno));
              //  string SEMSTERNO = mandatoryFieldsArray[4];
                //string RECIPT_CODE = objCommon.LookUp("ACD_DEMAND", "RECIEPT_CODE", "IDNO=" + Convert.ToInt32(Idno) + " AND ISNULL(CAN,0)=0 AND SEMESTERNO=" + Convert.ToInt32(SEMSTERNO));

                
                lblRegNo.Text = Regno;
                ViewState["userno"] = mandatoryFieldsArray[5];
               // Session["ReceiptType"] = RECIPT_CODE;
                ViewState["IDNO"] = Idno;
                lblstudentname.Text = Studname;
                lblBranch.Text = BranchName;
               // lblSemester.Text = SEMSTERNO;
               //  int installmentno = Convert.ToInt32(mandatoryFieldsArray[6]);
                string UniqueRefNumber = string.Empty;//Transaction ID              
                string PaymentMode = string.Empty;
                string TotalAmount = string.Empty;
                string TransactionAmount = string.Empty;
                string merchantid = string.Empty;
                string returnurl = string.Empty;
                string ReferenceNo = string.Empty;
                string sub_merchant_id = string.Empty;
                string transaction_amount = string.Empty;
                string paymode = string.Empty;
                string Recipt_Code = string.Empty;
                string firstname = string.Empty;
                string APPLICATION_USERNO = string.Empty;//udf1
                string APPLICATION_ID = string.Empty;//udf2
                string DegreeNo = string.Empty;//udf3
                string BranchNo = string.Empty;//udf4
                string DegreeBranchNo = string.Empty;
                string order_id = string.Empty;
                string amount = Request.Form["Transaction Amount"];
                string emailId = string.Empty;
                string saltkey = string.Empty;
                string hash_seq = string.Empty;
                order_id = Request.Form["ReferenceNo"];
                string StatusF = string.Empty;
                lblTrasactionId.Text = Request.Form["Unique Ref Number"];
                string tranID = Request.Form["Unique Ref Number"];

                Session["OrgId"] = objCommon.LookUp("REFF", "OrganizationId", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");

                HostelSessionNo = objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");

                string hostelno = mandatoryFieldsArray[4];
                string StudentSelectedRoom = mandatoryFieldsArray[6];

                
               // lblmessage.Text = "HostelNo: " + hostelno + " userno: " + Convert.ToString(ViewState["userno"]) + " collegeCode: " + Convert.ToString(Session["colcode"]) + "  OrgId " + Convert.ToString(Session["OrgId"]);

                if (ResponseCode == "E000")  // ResponseCode == "E000" means payment Success in EasyPay Payment gateway
                {                   
                    divFailure.Visible = false;

                    lblRoomNo.Text = StudentSelectedRoom;

                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;

                    int output = 0;
                    int OrgId = int.Parse(Session["OrgId"].ToString());

                    output = objFees.InsertOnlinePayment_DCR(Idno, "HF", order_id, tranID, "O", "1", amount, "Success", Regno, resposestring, OrgId);
                    
                    if (output == -99)
                    {
                        divSuccess.Visible = false;
                        divFailure.Visible = true;

                        objFees.InsertOnlinePaymentlog(Idno, "HF", "O", amount, "Payment Fail", order_id, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(StudentSelectedRoom));
                    }
                    else
                    {
                        ViewState["out"] = output;
                        divSuccess.Visible = true;
                        btnPrint.Visible = true;
                    }

                    roomAllotment.HostelSessionNo = Convert.ToInt32(HostelSessionNo);

                    roomAllotment.RoomNo = Convert.ToInt32(StudentSelectedRoom);
                    roomAllotment.UserNo = int.Parse(ViewState["userno"].ToString());
                    roomAllotment.CollegeCode = Session["colcode"].ToString();

                    roomAllotment.ResidentTypeNo = 1;
                    roomAllotment.ResidentNo = Convert.ToInt32(Idno);
                    roomAllotment.AllotmentDate = DateTime.Now;
                    int hostel_no = Convert.ToInt32(hostelno);
                    int mess_no = 0;
                    string vehicle_type = string.Empty;
                    string vehicle_name = string.Empty;
                    string vehicle_no = string.Empty;

                    int status = raController.AllotRoomWithoutDemand(roomAllotment, hostel_no, mess_no, vehicle_type, vehicle_name, vehicle_no, OrgId);

                    if (status == 1)
                    {
                        lblHostelStatus.Text = "Selected Room Allotted Successfully.";
                    }
                    else
                    {
                        lblHostelStatus.Text = "Selected Room not Allotted, Contact College Staff.";
                    }

                }
                else
                {
                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    //divStudDetails.Visible = false;
                    btnPrint.Visible = false;
                    objFees.InsertOnlinePaymentlog(Idno, "HF", "O", amount, "Payment Fail", order_id, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(StudentSelectedRoom));
                }
            }
            catch (Exception ex)
            {
                ////lblData2.Text = ex.Message;
                ////During Payment Exception catch
                divSuccess.Visible = false;
                divFailure.Visible = true;
                //divStudDetails.Visible = false;
                int result = 0;
                btnPrint.Visible = false;

                objCommon.DisplayMessage(this.Page, "Oops! Something went wrong. " + ex.Message, this.Page);
                return;
                Response.Write(ex.Message);
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("HostelOnlineFeePayment", "rptOnlineReceipt.rpt");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(ViewState["ReturnpageUrl"]);

        Response.Redirect("~/default.aspx");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);

            Session["userno"] = ViewState["userno"];
            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + IDNO + "' AND ORDER_ID ='" + ViewState["Order_id"].ToString() + "'");

            int College_code = int.Parse(Session["colcode"].ToString()); 

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HOSTEL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + College_code + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

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