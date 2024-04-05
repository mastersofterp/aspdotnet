using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mastersofterp_MAKAUAT;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

/*---------------------------------------------------------------------------------------------------------------------------
Created By :
Created On :
Purpose :
Version :
-----------------------------------------------------------------------------------------------------------------------------
Version   Modified On     Modified By             Purpose
-----------------------------------------------------------------------------------------------------------------------------
1.0.1     16-03-2024      Anurag Baghele          Added college id to get college banner
------------------------------------------- ---------------------------------------------------------------------------------*/

public partial class ACADEMIC_ApproveAdmission : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //  CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];

                //this.FillDropDown();
                // objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENAME");
                ShowStudentDetails();

                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetails.Visible = false;
                    divAdmissionApproval.Visible = false;
                    // btnGohome.Visible = false;
                    divhome.Visible = false;
                    divPrintReport.Visible = true;
                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    divadmissiondetails.Visible = false;
                    divAdmissionApproval.Visible = true;
                    divhome.Visible = true;
                }
                else
                {
                    divadmissiondetails.Visible = true;
                    divAdmissionApproval.Visible = true;
                    // btnGohome.Visible = true;
                    divhome.Visible = true;
                }

            }

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
        }
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/PersonalDetails.aspx", false);

        Response.Redirect("~/academic/PersonalDetails.aspx");

        // HttpContext.Current.RewritePath("PersonalDetails.aspx");
    }

    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AddressDetails.aspx", false);

        Response.Redirect("~/academic/AddressDetails.aspx");
    }

    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AdmissionDetails.aspx", false);
        Response.Redirect("~/academic/AdmissionDetails.aspx");

    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/DASAStudentInformation.aspx", false);
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }

    protected void lnkUploadDocument_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/UploadDocument.aspx");
    }

    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/QualificationDetails.aspx", false);
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }

    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/OtherInformation.aspx", false);
        Response.Redirect("~/academic/OtherInformation.aspx");
    }

    protected void lnkApproveAdm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/ApproveAdmission.aspx");
    }

    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Registration No!!", this.Page);
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            url += "pagetitle=Admission Form Report " + Session["stuinfoenrollno"].ToString();
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(regno) + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updApproveAdmission, this.updApproveAdmission.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //Added By Ruchika Dhakate on 17.10.2022 Ticket No:33448
    private void ShowGeneralExportReport(string exporttype, string rptFileName, int IDNO, string regno)
    {
        try
        {
            string collegeid = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"])); //<1.0.1>
            string filename = regno;
            string FileStud = "StudP";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            //url += "&filename=" + filename + "." + exporttype;     
            url += "&filename=" + FileStud + filename + "." + exporttype;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + collegeid + ",@P_IDNO=" + (IDNO) + "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updApproveAdmission, this.updApproveAdmission.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlAdmStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmStatus.SelectedValue == "2")
        {
            divRemark.Visible = true;
            divReason.Visible = true;
            divStatus.Visible = false;
        }
        else if (ddlAdmStatus.SelectedValue == "1")
        {
            divRemark.Visible = false;
            divReason.Visible = false;
            divStatus.Visible = true;
        }
        else
        {
            divRemark.Visible = false;
            divReason.Visible = false;
            divStatus.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        DemandModificationController dmController = new DemandModificationController();
        FeeDemand demandCriteria = new FeeDemand(); //this.GetDemandCriteria();
        try
        {
            if (ViewState["usertype"].ToString() != "2")
            {
                string reason = string.Empty;
                int admstatus = 0;
                //Update Payment Status

                //Create Demand 
                //demandCriteria.StudentId = Convert.ToInt32(Session["stuinfoidno"]);
                //demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
                //demandCriteria.UANO = Convert.ToInt32(Session["userno"]);
                //demandCriteria.CollegeCode = Session["colcode"].ToString();
                //demandCriteria.ReceiptTypeCode = "TF";
                //int AdmStatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);
                //demandCriteria.PaymentTypeNo = Convert.ToInt32(ddlPayType.SelectedValue);
                // string response = dmController.CreateDemandForExcelUploadedStudents(demandCriteria, AdmStatus);
                //if (response == "1")
                //{
                //if (response.Length > 2)
                //{
                //    objCommon.DisplayMessage(this.updApproveAdmission, "Standard fees is not defined for fees criteria applicable to these students", this.Page);
                //    //ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                //    return;
                //}
                //else
                //{
                objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                objS.Uano = Convert.ToInt32(Session["userno"]);
                objS.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                if (ddlAdmStatus.SelectedValue == "2")
                {
                    objS.Remark = txtRemark.Text;
                    reason = ddlReason.SelectedItem.Text.Trim();
                    admstatus = 0;

                }
                else
                {
                    objS.Remark = "";
                    reason = "";
                    //admstatus = Convert.ToInt32(ddlStatus.SelectedValue);
                }
                int status = Convert.ToInt32(ddlAdmStatus.SelectedValue);

                int usertype = Convert.ToInt32(Session["usertype"]);// Added By Kajal J. on 20032024 for maintaining log
                //string reason = ddlReason.SelectedItem.Text.Trim();
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentAdmissionStatus(objS, status, reason, admstatus, usertype);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    int AdStatus = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "STATUS", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"])));
                    //    if (AdStatus == 1)
                    //    {
                    //        //send SMS and Email to student for login credentials
                    //        string StudName = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]));
                    //        string StudEmail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + "AND UA_TYPE=2");
                    //        string subject = "Online Admission Fee Payment";
                    //        string message = "<b>Dear " + StudName + ","+"</b><br />";
                    //        message += "Your Admission Registration is approved. Please Visit https://makaut.mastersofterp.in for Online Admission Fee Payment." + "</b>";
                    //        message += "<br />Link Path: Academic -> Student Related -> Online Payment<br />";
                    //        message += "<br /><br /><br />Thank You<br />";
                    //        message += "<br />Team MAKAUT, WB<br />";
                    //        message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                    //        Task<int> task = Execute(message, StudEmail, subject);
                    //    }
                    if (AdStatus == 2)
                    {
                        //send SMS and Email to student for login credentials
                        string StudName = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]));
                        string StudEmail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + "AND UA_TYPE=2");
                        string Remark = objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "REJECT_REMARK", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]));
                        string subject = "Admission Registration Status";
                        string message = "<b>Dear " + StudName + "," + "</b><br />";
                        message += "Your Admission Registration is rejected due to the following reason" + "</b>";
                        message += "<br />Remark: " + Remark + "<br />";
                        message += "<br /><br />For any queries, kindly contact to the Admission department of MAKAUT.<br />";
                        message += "<br /><br /><br />Thank You<br />";
                        message += "<br />Team MAKAUT, WB<br />";
                        message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                        Task<int> task = Execute(message, StudEmail, subject);
                    }
                    objCommon.DisplayMessage(this.updApproveAdmission, "Admission Status Updated Successfully!!", this.Page);
                    btnSubmit.Visible = false;
                    ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage(this.updApproveAdmission, "Error Occured While Updating Admission Status!!", this.Page);
                    return;
                }

                //}

                //}
                //else if (response == "-99")
                //      objCommon.DisplayMessage(this.updApproveAdmission, "There is an error while creating demands.", this.Page);
                //else if (response == "5")
                //    //objCommon.DisplayMessage(this.updApproveAdmission, "Define standard fees first for selected criteria!", this.Page);
                //      objCommon.DisplayMessage(this.updApproveAdmission, "Fee Defination not done for selected criteria. Please Contact to Account department of MAKAUT.", this.Page);

            }
        }
        catch (Exception ex)
        {
            throw;
            //this.ClearControl();
        }
    }

    protected void btnGohome_Click(object sender, EventArgs e)
    {



        //Response.Redirect("~\\academic\\StudentInfoEntryNew.aspx");
    }

    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            Session["admidstatus"] = 1;
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
        }
        else
        {
            Session["admidstatus"] = 0;

        }
        Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");

    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        Student objS = new Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
            divAdmissionApproval.Visible = false;
            //btnreport.Visible = true;

        }
        else if (ViewState["usertype"].ToString() == "8")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
            divAdmissionApproval.Visible = true;
            //btnreport.Visible = true;
        }
        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
            divAdmissionApproval.Visible = true;


        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                    int finalstatus = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(FINAL_STATUS,0)", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"])));
                    if (finalstatus == 1)
                    {
                        btnreport.Visible = true;   //Added by sachin on 14-07-2022 change button visible false
                        btnSubmit.Visible = true;   //Added by sachin on 14-07-2022 
                    }
                    else
                    {
                        btnreport.Visible = false;
                        btnSubmit.Visible = true;
                    }
                }

                ddlAdmStatus.SelectedValue = dtr["STATUS"] == null ? "0" : dtr["STATUS"].ToString();
                ddlAdmStatus.SelectedValue = dtr["STATUS"] == null ? "0" : dtr["STATUS"].ToString();

                if (ddlAdmStatus.SelectedValue == "2")
                {
                    divRemark.Visible = true;
                    divReason.Visible = true;
                    divStatus.Visible = false;
                    txtRemark.Text = dtr["REJECT_REMARK"].ToString();

                    if (dtr["REJECT_REASON"].ToString() == null || dtr["REJECT_REASON"].ToString() == string.Empty || dtr["REJECT_REASON"].ToString() == "")
                    {
                        //ddlReason.Items.Add(new ListItem("Please Select", "0"));
                        ddlReason.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlReason.SelectedItem.Text = dtr["REJECT_REASON"].ToString();
                    }
                }
                else if (ddlAdmStatus.SelectedValue == "1")
                {
                    divRemark.Visible = false;
                    divReason.Visible = false;
                    divStatus.Visible = true;

                    //   ddlStatus.SelectedValue = dtr["ADM_STATUS"].ToString();

                }
                else
                {
                    divRemark.Visible = false;
                    divReason.Visible = false;
                    divStatus.Visible = false;
                }



                // string paytype = objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]));
                //  ddlPayType.SelectedValue = dtr["PAYTYPENO"] == null ? "0" : dtr["PAYTYPENO"].ToString();
                // ddlPayType.SelectedValue = paytype.ToString();

            }
        }


    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }

    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {

        string exportttpe = "pdf";
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {

            int IDNO = Convert.ToInt32(Session["stuinfoidno"]);               //Added By Ruchika Dhakate on 17.10.2022 Ticket No:33448
            string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + IDNO + "");

            ShowGeneralExportReport(exportttpe, "Admission_Slip_Confirm_PHD_General.rpt", IDNO, RegNo);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                int IDNO = Convert.ToInt32(Session["stuinfoidno"]);
                string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + IDNO + "");

                ShowGeneralExportReport(exportttpe, "Admission_Slip_Confirm_PHD_General.rpt", IDNO, RegNo);
            }
            else
            {
                objCommon.DisplayMessage("Please Search Registration No!!", this.Page);
            }
        }
    }
}