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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class HOSTEL_ONLINEFEECOLLECTION_HostelOnlinePaymentResponse : System.Web.UI.Page
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
                       // lblCollege.Text = dr["COLLEGENAME"].ToString();
                       // lblAddress.Text = dr["College_Address"].ToString();
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
                    HostelSessionNo = repoarray[22].ToString();
                    Session["userno"] = userno;
                    lblTrasactionId.Text = txnid;
                    lblamount.Text = amount;
                    lblstudentname.Text = repoarray[16].ToString();
                    lblRegNo.Text = Regno;
                    lblOrderId.Text = orderid;
                    lblTransactionDate.Text = transactiondate;
                    string hostelno = repoarray[18].ToString();
                    string StudentSelectedRoom = repoarray[17].ToString();
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

                    Session["OrgId"] = objCommon.LookUp("REFF", "OrganizationId", ""); // Added by Saurabh L on 23/09/2022
                   
                    Session["hostel_session"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");

                    Session["sessionname"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "SESSION_NAME", "FLOCK=1");
                    Session["payment"] = "payment";

                    if (authstatus == "0300")
                    {
                        int OrgId = int.Parse(Session["OrgId"].ToString()); // Added by Saurabh L on 23/09/2022
                        int output = 0;

                        //output = objFees.InsertOnlinePayment_DCR(Idno, "HF", orderid, txnid, "O", "1", amount, "Success", Regno, MSG);
                        //Above InsertOnlinePayment_DCR() commented by Saurabh L and add below line on 26/09/2022
                        //Purpose: Add OrgId parameter in below method
                        output = objFees.InsertOnlinePayment_DCR(Idno, "HF", orderid, txnid, "O", "1", amount, "Success", Regno, MSG, OrgId);
                            if (output == -99)
                            {
                                divSuccess.Visible = false;
                                divFailure.Visible = true;
                                // In InsertOnlinePaymentlog method added two parameter OrgId and StudentSelectedRoom by Saurabh L on 31 May 2023 Purpose: To maitain Room_No log
                                objFees.InsertOnlinePaymentlog(Idno, "HF", "O", amount, "Payment Fail", orderid, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(StudentSelectedRoom));
                            }
                            else
                            {
                                ViewState["out"] = output;
                                btnReciept.Visible = true;
                                divSuccess.Visible = true;
                            }


                            roomAllotment.HostelSessionNo = Convert.ToInt32(HostelSessionNo);

                            roomAllotment.RoomNo = Convert.ToInt32(StudentSelectedRoom);
                            roomAllotment.UserNo = int.Parse(Session["userno"].ToString());
                            roomAllotment.CollegeCode = Session["colcode"].ToString();

                            roomAllotment.ResidentTypeNo = 1;
                            roomAllotment.ResidentNo = Convert.ToInt32(Idno);
                            roomAllotment.AllotmentDate = DateTime.Now;
                            int hostel_no = Convert.ToInt32(hostelno);
                            int mess_no = 0;
                            string vehicle_type = string.Empty;
                            string vehicle_name = string.Empty;
                            string vehicle_no = string.Empty;
                            // int OrganizationId = 2; // commented by Saurabh L on 23/09/2022

                            int status = raController.AllotRoom(roomAllotment, hostel_no, mess_no, vehicle_type, vehicle_name, vehicle_no, OrgId);

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
                        divFailure.Visible = true;
                        divSuccess.Visible = false;
                        btnReciept.Visible = false;
                        // In InsertOnlinePaymentlog method added two parameter OrgId and StudentSelectedRoom by Saurabh L on 31 May 2023 Purpose: To maitain Room_No log
                        objFees.InsertOnlinePaymentlog(Idno, "HF", "O", amount, "Payment Fail", orderid, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(StudentSelectedRoom));
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }
    protected void btnReciept_Click(object sender, EventArgs e)
    {
        ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));

            int College_code = int.Parse(Session["colcode"].ToString()); // Added by Saurabh L on 23/09/2022

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