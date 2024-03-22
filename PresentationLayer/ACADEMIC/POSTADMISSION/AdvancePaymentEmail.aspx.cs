using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessLogic.PostAdmission;
using System.Web.Services;
using Newtonsoft.Json;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Web;
/*                                                  
---------------------------------------------------------------------------------------------------------------------------                                                          
Created By : Bhagyashree                                                      
Created On : 15-03-2024                             
Purpose    : Create page to send email to student                              
Version    : 1.0.0                                                
---------------------------------------------------------------------------------------------------------------------------                                                            
Version   Modified On   Modified By        Purpose                                                            
---------------------------------------------------------------------------------------------------------------------------                                                            
                                      
------------------------------------------- -------------------------------------------------------------------------------                             
*/ 
public partial class ACADEMIC_POSTADMISSION_AdvancePaymentEmail : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AdvancePaymentEmailController objAC = new AdvancePaymentEmailController();
    DataSet ds = null;
    string jsonstring = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); 
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
                Response.Redirect("~/notauthorized.aspx?page=AppFeeReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AppFeeReport.aspx");
        }
    }

    [WebMethod]
    public static string GetAdmissionBatch()
    {
        Common objCommon = new Common();
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO)", "DISTINCT A.ADMBATCH BATCHNO", "BATCHNAME", "ISNULL(B.ACTIVESTATUS,0)=1", "A.ADMBATCH ASC");
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject("");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetDegree(int ugpgot)
    {
        Common objCommon = new Common();
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)=1 AND D.DEGREENO > 0 AND UGPGOT=" + ugpgot + "", "D.DEGREENO");
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject("");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string GetStudentDetails(int BatchNo, int UgPgOt, int DegreeNo, string BranchNos) 
    {
        AdvancePaymentEmailController objAC = new AdvancePaymentEmailController();
        DataSet ds = null;
        try
        {
            ds = objAC.GetStudentDetails(BatchNo, UgPgOt, DegreeNo, BranchNos);
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject("");
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetSendEmailStudentDetails(string UsernoXml)
    {
        SendEmailCommon objSendEmail = new SendEmailCommon();
        AdvancePaymentEmailController objAEC = new AdvancePaymentEmailController();
        ApplicationProcessController objAPC = new ApplicationProcessController();
        ApplicationProcess objAP = new ApplicationProcess();
        DataSet ds = null;
        string EmailStatus = string.Empty; string subject = string.Empty;
        try
        {
            string loginurladmp = System.Configuration.ConfigurationManager.AppSettings["WebServerADMP"].ToString();
            string loginurlbtech = System.Configuration.ConfigurationManager.AppSettings["WebServerBTECH"].ToString();
            ds = objAEC.GetStudentDetailsToSendEmail(UsernoXml);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                subject = "Crescent – " + ds.Tables[0].Rows[0]["DEGREE_SHORT_CODE"].ToString() + " Provisional Admission Order";

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string emailid = ds.Tables[0].Rows[i]["EMAILID"].ToString();
                    string message = "Dear " + ds.Tables[0].Rows[i]["STUDETNAME"].ToString() + "<br />";
                    message += "<br />";
                    message += "Greetings from B.S.A. Crescent Institute of Science and Technology <br />";
                    message += "<br />";
                    message += "Congratulations! <br />";
                    message += "<br />";
                    message += "<br />";
                    message += "We are pleased to inform you that you are selected for the provisional admission to the following <br />";
                    message += "programme for the academic year " + ds.Tables[0].Rows[i]["BATCHNAME"].ToString() + ". <br />";
                    message += "<br />";
                    message += "<br />";
                    message += "Application Number: " + ds.Tables[0].Rows[i]["USERNAME"].ToString() + "<br />";
                    if (ds.Tables[0].Rows[i]["BRANCH_NAME"].ToString() != string.Empty)
                    {
                        message += "Programme: " + ds.Tables[0].Rows[i]["DEGREENAME"].ToString() + " with " + ds.Tables[0].Rows[i]["BRANCH_NAME"].ToString() + " <br />";
                    }
                    else
                    {
                        message += "Programme: " + ds.Tables[0].Rows[i]["DEGREENAME"].ToString() + " <br />";
                    }
                    message += "<br />";
                    message += "To confirm the provisional admission to the above programme, you are requested to make the online <br />";
                    message += "payment of advance Rs." + ds.Tables[0].Rows[i]["FEEPAYMENT"].ToString() + " towards the first semester tuition fee using the below Institute <br />";
                    message += "payment link on or before " + ds.Tables[0].Rows[i]["PAYMENT_ENDDATE"].ToString() + " or you can visit the Institute and make the payment in the Admission Office on or before " + ds.Tables[0].Rows[i]["OFFICE_VISIT_END_DATE"].ToString() + ". <br />";
                    message += "<br />";
                    if (ds.Tables[0].Rows[i]["DEGREENO"].ToString() == "7")
                    {
                        message += "Institute Online Fee Payment Link: " + loginurlbtech  + " <br />";
                    }
                    else
                    {
                        message += "Institute Online Fee Payment Link: " + loginurladmp + "  <br />";
                    }
                    message += "Login ID: " + ds.Tables[0].Rows[i]["USERNAME"].ToString() + " <br />";
                    message += "<br />";
                    message += "After making the online payment, please send the E- receipt as the attachment to the email ids given here to get the confirmation of your provisional admission. <br />";
                    message += "<br />";
                    message += "admissionoffice@crescent.education <br />";
                    message += "financeofficer@crescent.education <br />";
                    message += "asst.registrar-admissions@crescent.education <br />";
                    message += "<br />";
                    message += "<br />";
                    if (ds.Tables[0].Rows[i]["DEGREENO"].ToString() == "7")
                    {
                        message += "Eligibility for Admission to B.Tech. programmes: <br />";
                        message += "<br />";
                        message += "Pass in 12th Std. Examination with a minimum average of 50 % of marks in Mathematics, Physics and Chemistry. For B.Tech. Biotechnogy, minimum average of 50% of marks in Mathematics / Biology, physics and Chemistry <br />";
                    }
                    message += "Confirmation of Admission: <br />";
                    message += "The admission to the above programme will be confirmed after the <br />";
                    message += "1. Verification of eligibility <br />";
                    message += "2. Payment of the entire fee for the first semester including Amenities and Service Fee <br />";
                    message += "3. Submission of original mark sheet of 10th Std., 11th Std. (if applicable), 12th std., Original Transfer Certificate, Original Community Certificate (if applicable) and Original Nativity Certificate (If applicable), etc. <br />";
                    message += "4. Reporting for the final admission process on the scheduled date. <br />";
                    message += "<br />";
                    message += "This provisional admission offer is valid till " + ds.Tables[0].Rows[i]["PROVISIONAL_ADMISSION_OFFER_VALID_DATE"].ToString() + " <br />";
                    message += "<br />";
                    message += " Please provide the following information in all your future correspondence: <br />";
                    message += "1. Name <br />";
                    message += "2. CIEAT ID / Application No. <br />";
                    message += "3. Programme (course) Applied <br />";
                    message += "4. Registered Mobile Number. <br />";
                    message += "<br />";
                    message += "Wish you all the best. <br />";
                    message += "For any assistance, please call +91-95432 77888 <br />";
                    message += "<br />";
                    message += "(Monday to Saturday 9.30 a.m. to 5.30 p.m.) <br />";
                    message += "<br />";
                    message += "Thanks & Regards <br />";
                    message += "<br />";
                    message += "Admission Team <br />";
                    message += "B.S. Abdur Rahman Institute of Science & Technology";
                    EmailStatus = objSendEmail.SendEmail(emailid, message, subject).ToString();
                    if (EmailStatus == "1")
                    {
                        objAP.UserNo = ds.Tables[0].Rows[i]["USERNO"].ToString();
                        objAP.UaNo = Convert.ToInt32(HttpContext.Current.Session["uano"].ToString());
                        objAP.ProcessType = 'E';
                        objAP.Description = ds.Tables[0].Rows[i]["STUDETNAME"].ToString() + " | " + ds.Tables[0].Rows[i]["MOBILENO"].ToString() + " | " + ds.Tables[0].Rows[i]["EMAILID"].ToString();
                        objAP.ESubject = subject.ToString();
                        objAP.EWBody = message.ToString();
                        int retstatus = objAPC.InsUpdEmailSmsSendLog(objAP);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject("");
        }
        return JsonConvert.SerializeObject(EmailStatus);
    }
}