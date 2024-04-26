//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TO CREATE REFERENCE VARIABLES FOR PROJECT                       
// CREATION DATE : 
// CREATED BY    : SHEETAL RAUT
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class error : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string pass = string.Empty;
    string smspass = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }
    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
           // CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
            ViewState["action"] = "update";
            if (Session["userno"].ToString() != "1")
            {

                //rdbDeveloper.Enabled = false;
                //rdbClient.Enabled = false;
                txtGovt.Enabled = false;
                txtName.Enabled = false;
                txtCollegeAddress.Enabled = false;
                txtPhoneNo.Enabled = false;
                txtEmailID.Enabled = false;
                //fuCollegeLogo.Enabled = false;       -- Commented by Shrikant W. After Discussion with Manoj Shanti Sir to enable Collge Logo Upload for all Admin User
                txtCollegeCode.Enabled = false;
                txtFacUserType.Enabled = false;
                //chkEnroll.Enabled = false;
                txtLateFee.Enabled = true;
                //rdbFeedback.Enabled = false;
                //rdbHorizontal.Enabled = false;
                //rdbVerticle.Enabled = false;
                txtStartYear.Enabled = false;
                txtEndYear.Enabled = false;
                //chkResetCounter.Enabled = false;
                ddlLogpop.Enabled = false;
                rdbFascility.Enabled = false;
                txtPopup.Enabled = false;
                txtAttempt.Enabled = false;
                txtEmailsvc.Enabled = false;
                txtEmailsvcpwd.Enabled = false;
                txtSMSsvc.Enabled = false;
            }
            GetAdminTypeUser(); //Addded Mahesh on Dated 23/06/2021
            objCommon.FillDropDownList(ddlCancelLateFineAuthorityPerson, "User_acc", "UA_NO", "UA_NAME", "UA_TYPE IN(1,5) AND UA_STATUS=0 AND UA_FIRSTLOG=1", "UA_NO");
            ShowDetails();
            //Comment by Mahesh on Dated 23/06/2021

            //txtEmailsvcpwd.Attributes.Add("onfocus", "enterTextBox();");
            //txtEmailsvcpwd.Attributes.Add("onblur", "Data('" + pass + "')");
            //txtSMSsvcpwd.Attributes.Add("onfocus", "enterTextBoxsms();");
            //txtSMSsvcpwd.Attributes.Add("onblur", "Datasms('" + smspass + "')");

            txtSMSsvcpwd.Attributes.Add("onmousedown", "return noCopyMouse(event);");
            txtSMSsvcpwd.Attributes.Add("onkeydown", "return noCopyKey(event);");
            txtEmailsvcpwd.Attributes.Add("onmousedown", "return noCopyMouse(event);");
            txtEmailsvcpwd.Attributes.Add("onkeydown", "return noCopyKey(event);");
        }
        string script = string.Empty;
        if (rdbMaintenance.SelectedValue == "0")
            script = "$('#txtMaintananceDateTime').prop('disabled', false);";
        else
            script = "$('#txtMaintananceDateTime').prop('disabled', true);";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "txtTime", script, true);
    }

    private void ShowDetails()
    {
        try
        {
            SqlDataReader dr = objCommon.GetCommonDetails();

            if (dr != null)
            {
                if (dr.Read())
                {
                    //char ch = char.Parse(dr["Errors"].ToString());
                    //if (ch  == '0')
                    //    rdbClient.Checked = true;
                    //else
                    //    rdbDeveloper.Checked = true;
                    string error = dr["Errors"].ToString();
                    if (dr["Errors"].ToString() == "1")
                    {
                        rdShowErr.Checked = true;
                    }
                    else
                    {
                        rdShowErr.Checked = false;
                    }

                    txtGovt.Text = dr["GOVT"].ToString();
                    txtName.Text = dr["COLLEGENAME"].ToString();
                    txtCollegeAddress.Text = dr["College_Address"].ToString();


                    imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";

                    txtCollegeCode.Text = dr["COLLEGE_CODE"].ToString();
                    txtCollegeAddress.Text = dr["COLLEGE_ADDRESS"].ToString();
                    txtPhoneNo.Text = dr["PHONE"].ToString();
                    txtEmailID.Text = dr["EMAIL"].ToString();
                    txtFacUserType.Text = dr["FAC_USERTYPE"].ToString();
                    txtLateFee.Text = dr["ADM_LATE_FEE_AMT"].ToString();
                    string feedback = dr["Feedback_Status"].ToString();
                    if (dr["Feedback_Status"].ToString().Equals("False"))
                    {
                        rdFeedback.Checked = false;
                    }
                    else
                    {
                        rdFeedback.Checked = true;
                    }
                    //rdbFeedback.SelectedValue = dr["Feedback_Status"].ToString(); 
                    string timetable = dr["Time_table_status"].ToString();
                    if (dr["Time_table_status"].ToString() == "1")
                    {
                        rdTableStatus.Checked = true;
                    }
                    else
                    {
                        rdTableStatus.Checked = false;
                    }

                    txtStartYear.Text = Convert.ToDateTime(dr["Start_Year"].ToString()).ToString("dd/MMM/yyyy");
                    txtEndYear.Text = Convert.ToDateTime(dr["End_Year"].ToString()).ToString("dd/MMM/yyyy");
                    txtNumBckAttensAllow.Text = dr["NOS_BK_ATTENDS_ALLOW"].ToString();
                    string resetcounter = dr["Reset_Counter"].ToString();
                    if (dr["Reset_Counter"].ToString() == "True")
                    {
                        chkResetCounter.Checked = true;
                    }
                    else
                    {
                        chkResetCounter.Checked = false;
                    }

                    string enrollmentno = dr["EnrollmentNo"].ToString();
                    if (dr["EnrollmentNo"].ToString() == "True")
                    {
                        rdEnrollment.Checked = true;
                    }
                    else
                    {
                        rdEnrollment.Checked = false;
                    }
                    txtEmailsvc.Text = dr["EMAILSVCID"].ToString();
                    //txtEmailsvcpwd.Text = Common.DecryptPassword(dr["EMAILSVCPWD"].ToString());
                    //txtEmailsvcpwd.TextMode = TextBoxMode.SingleLine; 
                    txtEmailsvcpwd.Attributes.Add("value", dr["EMAILSVCPWD"].ToString());
                    //txtEmailsvcpwd.Text=dr["EMAILSVCPWD"].ToString();

                    pass = Common.DecryptPassword(dr["EMAILSVCPWD"].ToString());
                    ViewState["EMAILPWD"] = dr["EMAILSVCPWD"].ToString();
                    txtSMSsvc.Text = dr["SMSSVCID"].ToString();
                    //txtSMSsvcpwd.Text = dr["SMSSVCPWD"].ToString();
                    txtSMSsvcpwd.Attributes.Add("value", dr["SMSSVCPWD"].ToString());

                    txtAttempt.Text = dr["ATTEMPT"].ToString();
                    ddlLogpop.SelectedValue = dr["ALLOWLOGOUTPOPUP"].ToString();
                    txtPopup.Text = dr["POPUPDURATION"].ToString();
                    if (dr["FASCILITY"].ToString() != "")
                        rdbFascility.SelectedValue = dr["FASCILITY"].ToString();
                    txtpopupmsg.Text = dr["POPUP_MSG"].ToString();
                    if (Convert.ToInt32(dr["POPUP_FLAG"]) == 1)
                    {
                        chkpopup.Checked = true;
                        divpop.Attributes.Add("style", "display: block");
                    }
                    else
                    {
                        chkpopup.Checked = false;
                        divpop.Attributes.Add("style", "display: none");
                    }
                    txtSender.Text = dr["USER_PROFILE_SENDERNAME"] == null ? string.Empty : dr["USER_PROFILE_SENDERNAME"].ToString();
                    txtSubject.Text = dr["USER_PROFILE_SUBJECT"] == null ? string.Empty : dr["USER_PROFILE_SUBJECT"].ToString();
                    if (dr["MARKENTRY_OTP"].ToString() != "" && dr["MARKENTRY_OTP"].ToString() != null)
                        rdobtnMarkOTP.SelectedValue = dr["MARKENTRY_OTP"].ToString();

                    if (dr["MARKENTRY_EMAIL"].ToString() != "" && dr["MARKENTRY_EMAIL"].ToString() != null)
                        rdomarkentrysaveLockemail.SelectedValue = dr["MARKENTRY_EMAIL"].ToString();

                    if (dr["MARKENTRY_SMS"].ToString() != "" && dr["MARKENTRY_SMS"].ToString() != null)
                        rdomarkentrysaveLockSMS.SelectedValue = dr["MARKENTRY_SMS"].ToString();

                    if (Convert.ToInt32(dr["Course_Reg_B_Time_Table"]) == 1)
                    {
                        chkCRBTimeTable.Checked = true;
                    }
                    else
                    {
                        chkCRBTimeTable.Checked = false;
                    }

                    chkDecodeNumOrEnrollNo.Checked = Convert.ToBoolean(dr["ENDSEMBY_DECODE_OR_ENROLL"].ToString() == null ? false : dr["ENDSEMBY_DECODE_OR_ENROLL"]);
                    if (chkDecodeNumOrEnrollNo.Checked == true)
                    {
                        lblDecodeNumOrEnrollNo.Text = "End Sem by Decode No. Wise";
                    }
                    else
                    {
                        lblDecodeNumOrEnrollNo.Text = "End Sem by Enrollment No. / Roll No. Wise";
                    }
                    txtIAMarks.Text = dr["IA_MARKS_VALUE"].ToString(); //Added by Nikhil on 01/04/2021
                    txtPCAMarks.Text = dr["PCA_MARKS_VALUE"].ToString();//Added by Nikhil on 01/04/2021

                    ddlAdminLevelMarksEntry.SelectedValue = dr["Admin_Level_Marks_Entry"] == null ? "0" : dr["Admin_Level_Marks_Entry"].ToString();

                    ddlUpdMigrationExamData.SelectedValue = dr["Update_OldExam_Data_Migration"] == null ? "0" : dr["Update_OldExam_Data_Migration"].ToString();

                    ddlReceiptCancel.SelectedValue = dr["RECEIPT_CANCEL"] == null ? "0" : dr["RECEIPT_CANCEL"].ToString();

                    ///added by tanu 09/12/2022
                    Imagebenner.ImageUrl = "~/showimage.aspx?id=0&type=CLGBANNER";

                    string a = dr["LATE_FEE_CANCEL"] == null ? "0" : dr["LATE_FEE_CANCEL"].ToString();
                    ddlCancelLateFineAuthorityPerson.SelectedValue = a;

                    //***********************************************************//

                    int maintenanceFlag = Convert.ToInt32(dr["MAINTENANCE"].ToString() == string.Empty ? "1" : dr["MAINTENANCE"].ToString());
                    if (maintenanceFlag == 0)
                    {
                        // chkMaintenance.Checked = true;
                        rdbMaintenance.SelectedValue = "0";
                        Session["maintenanceFlag"] = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "txtTime", "$('#txtMaintananceDateTime').prop('disabled', false);", true);
                    }
                    else
                    {
                        //chkMaintenance.Checked = false;
                        rdbMaintenance.SelectedValue = "1";
                        //txtMaintananceDateTime.Enabled = false;
                        // txtTimeDiff.Enabled = false;
                        // txtMainTimeSpan.Enabled = false;
                        ddlTimeDiff.Enabled = false;
                        ddlMainTimeSpan.Enabled = false;
                        Session["maintenanceFlag"] = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "txtTime", "$('#txtMaintananceDateTime').prop('disabled', true);", true);
                    }


                    DateTime sdt = DateTime.Parse(dr["MAINTENANCE_STIME"].ToString() == string.Empty ? "01/02/2023 12:00 AM" : dr["MAINTENANCE_STIME"].ToString());
                    ViewState["startTime"] = sdt.ToString("dd/MM/yyyy hh:mm tt");
                    hdfStartTIme.Value = ViewState["startTime"].ToString();
                    Session["miantenanceSTime"] = sdt.ToString("MM/dd/yyyy HH:mm:ss");
                    //DateTime edt = DateTime.Parse(dr["MAINTENANCE_ENDTIME"].ToString() == string.Empty ? "01/02/2023 01:00 PM" : dr["MAINTENANCE_ENDTIME"].ToString());
                    //ViewState["endTime"] = edt.ToString("dd/MM/yyyy hh:mm tt");
                    //txtMainTimeSpan.Text = dr["MAINTENANCE_ENDTIME"].ToString();
                    ddlMainTimeSpan.SelectedValue = dr["MAINTENANCE_ENDTIME"].ToString();
                    long tempAlertFreq = dr["ALERT_FREQ"].ToString() == string.Empty ? 00000 : Convert.ToInt64(dr["ALERT_FREQ"].ToString());

                    if (tempAlertFreq == 00000)
                        // txtTimeDiff.Text = string.Empty;
                        ddlTimeDiff.SelectedIndex = 0;
                    else
                        //txtTimeDiff.Text = Convert.ToString(tempAlertFreq / 60000);//converted miliseconds to minutes
                        ddlTimeDiff.SelectedValue = Convert.ToString(tempAlertFreq / 60000);
                    //***********************************************************//

                    txtErrorLogEmail.Text = dr["ERROR_LOG_EMAIL"].ToString(); //Added by Anurag Baghele on 15-02-2024

                }
            }

            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "reference.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //rdbClient.Checked = false;
        //rdbDeveloper.Checked = false;
        //txtName.Text = string.Empty;
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ReferenceController objEc = new ReferenceController();
            Reference objRef = new Reference();

            //Added By Rishabh B. on 08/11/2021
            if (rdShowErr.Checked==true)
            {
                objRef.Errors = true;
            }
            else
            {
                objRef.Errors = false;
            }

            //  string Symb = "!@#$%^&*()_+";
            objRef.CollegeName = txtName.Text.Trim().Replace(",", ",");
            objRef.CollegeAddress = txtCollegeAddress.Text.Trim();
            objRef.Gov = txtGovt.Text.Trim();
            if (fuCollegeLogo.HasFile)
            {
                HttpPostedFile FileSize = fuCollegeLogo.PostedFile;
                string Fileext = System.IO.Path.GetExtension(fuCollegeLogo.FileName);
                if (Fileext.ToLower() == ".jpg" || Fileext.ToLower() == ".jpeg" || Fileext.ToLower() == ".png")
                {
                    if (FileSize.ContentLength <= 512 * 1024)
                    {
                        objRef.CollegeLogo = objCommon.GetImageData(fuCollegeLogo);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload Files with a Maximum Size of 512KB.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files like .jpg, .jpeg and .png file format Only!", this.Page);
                    return;
                }
            }
            else
            {
                objRef.CollegeLogo = null;
            }

            objRef.CollegeCode = txtCollegeCode.Text.Trim();
          
           
            if (rdEnrollment.Checked==true)
            {
                objRef.EnrollmentNo = true;
            }
            else
            {
                objRef.EnrollmentNo = false;
            }  
            objRef.Fac_UserType = txtFacUserType.Text.Trim();
            objRef.Phone = txtPhoneNo.Text.Trim();
            objRef.EmailID = txtEmailID.Text.Trim();
            objRef.Admlatefee = Convert.ToDouble(txtLateFee.Text.Trim());
           
            //objRef.Feedback = Convert.ToBoolean(rdbFeedback.SelectedValue);

            //Added By Rishabh B. on 08/11/2021
            if (rdFeedback.Checked==true)
            {
                objRef.Feedback = true;
            }
            else
            {
                objRef.Feedback = false;
            }


            //objRef.Admlatefee = Convert.ToDouble(txtLateFee.Text.Trim());
            objRef.StartYear = Convert.ToDateTime(txtStartYear.Text.Trim());
            objRef.EndYear = Convert.ToDateTime(txtEndYear.Text.Trim());
            objRef.Emailsvcid = txtEmailsvc.Text.Trim();
            if (txtEmailsvcpwd.Text.Trim() == "")
            {
                objRef.Emailsvcpwd = ViewState["EMAILPWD"].ToString();
            }
            else
            {
                objRef.Emailsvcpwd = txtEmailsvcpwd.Text.Trim();
            }

            objRef.SMSsvcid = txtSMSsvc.Text.Trim();
            objRef.SMSsvcpwd = txtSMSsvcpwd.Text.Trim();
            objRef.Attempt = txtAttempt.Text == "" ? 0 : Convert.ToInt32(txtAttempt.Text.Trim());
            objRef.AllowLogoutpopup = Convert.ToInt32(ddlLogpop.SelectedValue);
            objRef.Popupduration = txtPopup.Text == "" ? 0 : Convert.ToInt32(txtPopup.Text.Trim());
            objRef.Fascility = Convert.ToInt32(rdbFascility.SelectedValue);
            if (chkCRBTimeTable.Checked == true)
            {
                objRef.Course_Reg_B_Time_Table = true;
            }
            else
            {
                objRef.Course_Reg_B_Time_Table = false;
            }


            if (chkResetCounter.Checked==true)
            {
                objRef.ResetCounter = true;
            }
            else
            {
                objRef.ResetCounter = false;
            }  

            if (rdTableStatus.Checked==true)
            {
                objRef.Timetable = 1;
            }
            else
            {
                objRef.Timetable = 2;
            }

            //objRef.Timetable = Convert.ToInt16(timetable);

            if (chkpopup.Checked == true)
            {
                objRef.POPUP_FLAG = 1;

            }
            else
            {
                objRef.POPUP_FLAG = 0;
                // objRef.POPUP_MSG = string.Empty;
            }
            objRef.POPUP_MSG = txtpopupmsg.Text.Trim() == string.Empty ? string.Empty : txtpopupmsg.Text.Trim();
            objRef.userProfileSender = txtSender.Text.ToString();
            objRef.userProfileSubject = txtSubject.Text.ToString();
            objRef.MarkEntry_OTP = Convert.ToInt32(rdobtnMarkOTP.SelectedValue);
            objRef.MarkSaveLock_Email = Convert.ToInt32(rdomarkentrysaveLockemail.SelectedValue);
            objRef.MarkSaveLock_SMS = Convert.ToInt32(rdomarkentrysaveLockSMS.SelectedValue);
            objRef.AttendanceBackDays = Convert.ToInt32(txtNumBckAttensAllow.Text);
            int IA_Marks = Convert.ToInt32(txtIAMarks.Text.Trim());   //Added by Nikhil on 01/04/2021
            int PCA_Marks = Convert.ToInt32(txtPCAMarks.Text.Trim());//Added by Nikhil on 01/04/2021
            if (chkDecodeNumOrEnrollNo.Checked == true)
            {
                objRef.ENDSEMBY_DECODE_OR_ENROLL = true;
            }
            else
            {
                objRef.ENDSEMBY_DECODE_OR_ENROLL = false;
            }


            if (fuCollegeBanner.HasFile)
            {
                HttpPostedFile FileSize = fuCollegeBanner.PostedFile;
                string Fileext = System.IO.Path.GetExtension(fuCollegeBanner.FileName);
                if (Fileext.ToLower() == ".jpg" || Fileext.ToLower() == ".jpeg" || Fileext.ToLower() == ".png")
                {
                    if (FileSize.ContentLength <= 512 * 1024)
                    {
                        objRef.CollegeBanner = objCommon.GetImageData(fuCollegeBanner);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload Files with a Maximum Size of 512KB.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files like .jpg, .jpeg and .png file format Only!", this.Page);
                    return;
                }
            }
            else
            {
                objRef.CollegeBanner = null;
            }

            objRef.Admin_Level_Marks_Entry = Convert.ToInt32(ddlAdminLevelMarksEntry.SelectedValue);

            objRef.Update_OldExam_Data_Migration = Convert.ToInt32(ddlUpdMigrationExamData.SelectedValue);
            objRef.Receipt_Cancel = Convert.ToInt32(ddlReceiptCancel.SelectedValue);//Added By Dileep Kare on 27.07.2021

            if (ddlCancelLateFineAuthorityPerson.SelectedIndex > 0)
            {
                //string idno = objCommon.LookUp("User_acc", "UA_NO", "UA_NAME  COLLATE DATABASE_DEFAULT ='" + txtCancelLateFineAuthorityPerson.Text.Trim().ToUpper() + "'");
                //if (!string.IsNullOrEmpty(idno))
                //    objRef.Late_Fine_Cancel_Authority_Fac_ID = Convert.ToInt32(ddlCancelLateFineAuthorityPerson.SelectedValue);
                //else
                //    objRef.Late_Fine_Cancel_Authority_Fac_ID = 0;

                objRef.Late_Fine_Cancel_Authority_Fac_ID = Convert.ToInt32(ddlCancelLateFineAuthorityPerson.SelectedValue);
            }
            else
            {
                objRef.Late_Fine_Cancel_Authority_Fac_ID = 0;
            }

            objRef.Error_Log_Email = txtErrorLogEmail.Text.Trim(); //Added by Anurag Baghele on 15-02-2024

            //*********************START FOR MAINTENANCE ADDED BY SHAHBAZ AHMAD 14-02-2023***********************//
            int maintenanceFlag = 1;
            if (rdbMaintenance.SelectedValue == "0") //&&chkMaintenance.Checked)
                maintenanceFlag = 0;
            else
                maintenanceFlag = 1;

            //string[] StartEndTime = txtMaintananceDateTime.Text.Split('-');
            string startTime = hdfStartTIme.Value;//txtMaintananceDateTime.Text;
            //string endTime = txtMainTimeSpan.Text;

            DateTime maintenanceStartTime = DateTime.Parse(startTime);

            int? maintenanceEndTime = null;

            //if (txtMainTimeSpan.Text == null || txtMainTimeSpan.Text == string.Empty || txtMainTimeSpan.Text == "")
            //    maintenanceEndTime = null;
            //else
            //    maintenanceEndTime = int.Parse(txtMainTimeSpan.Text);

            if (rdbMaintenance.SelectedValue == "0" && ddlMainTimeSpan.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Maintenance Time Span", this.Page);
                ddlMainTimeSpan.Focus();
                return;
            }
            else
            {
                if (ddlMainTimeSpan.SelectedIndex > 0)
                    maintenanceEndTime = Convert.ToInt32(ddlMainTimeSpan.SelectedValue);
                else
                    maintenanceEndTime = null;
            }


            long? alertFreqInMiliSec = null;
            //if (txtTimeDiff.Text == null || txtTimeDiff.Text == string.Empty || txtTimeDiff.Text =="")
            //   alertFreqInMiliSec = null;     
            //else
            //    alertFreqInMiliSec = Convert.ToInt32(txtTimeDiff.Text) * 60000;
            if (rdbMaintenance.SelectedValue == "0" && ddlTimeDiff.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Alert Time Difference", this.Page);
                ddlTimeDiff.Focus();
                return;
            }
            else
            {
                if (ddlTimeDiff.SelectedIndex > 0)
                    alertFreqInMiliSec = Convert.ToInt64(ddlTimeDiff.SelectedValue) * 60000;
                else
                    alertFreqInMiliSec = null;
            }

            //    hdfdate.Value = getdate.ToString("MM/dd/yyyy HH:mm:ss")getdate
            //***********************END FOR MAINTENANCE ADDED BY SHAHBAZ AHMAD 14-02-2023********************//
            if (ViewState["action"] != null)
            {
                //Edit Reference
                CustomStatus cs = (CustomStatus)objEc.Update(objRef, IA_Marks, PCA_Marks, maintenanceFlag, maintenanceStartTime, maintenanceEndTime, alertFreqInMiliSec);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Configuration Details Updated Successfully!!", this.Page);
                    //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<Script language='javascript' type='text/javascript'> alert('Configuration Details Updated Successfully!!'); if (!window._isReloaded) { window._isReloaded = true; location.reload(); }</Script>");
                    ShowDetails();
                    //Response.Redirect(Request.RawUrl);
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "reload", "if (!window._isReloaded) { window._isReloaded = true; location.reload(); }", true);
                    string script = "$(document).ready(function(){__doPostBack('rdbMaintenance',\"\");});";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "reload", script, true);
                    //Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
                else
                    objCommon.DisplayMessage("Error!!", this.Page);
            }
            ShowDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "reference.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=reference.aspx");

            }
            //Common objCommon = new Common();
            //objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
        }
    }

    protected void chkEnroll_CheckedChanged(object sender, EventArgs e)
    {
    //    if (chkEnroll.Checked == true)
    //        chkEnroll.Text = "Manual Enrollment/Registration No. of Student";
    //    else
    //        chkEnroll.Text = "Automatic Enrollment/Registration No. of Student";
    }

    private void GetAdminTypeUser()
    {
        DataSet ds = null;
        ds = objCommon.GetAdminUserDetail();

        ddlAdminLevelMarksEntry.Items.Clear();
        ddlAdminLevelMarksEntry.Items.Add("Please Select");
        ddlAdminLevelMarksEntry.SelectedItem.Value = "0";

        //added by Deepali on 12/07/2021
        ddlUpdMigrationExamData.Items.Clear();
        ddlUpdMigrationExamData.Items.Add("Please Select");
        ddlUpdMigrationExamData.SelectedItem.Value = "0";

        //Added By Dileep Kare on 26.07.2021
        ddlReceiptCancel.Items.Clear();
        ddlReceiptCancel.Items.Add("Please Select");
        ddlReceiptCancel.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAdminLevelMarksEntry.DataSource = ds;
            ddlAdminLevelMarksEntry.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlAdminLevelMarksEntry.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlAdminLevelMarksEntry.DataBind();
            ddlAdminLevelMarksEntry.SelectedIndex = 0;

            ddlUpdMigrationExamData.DataSource = ds;
            ddlUpdMigrationExamData.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlUpdMigrationExamData.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlUpdMigrationExamData.DataBind();
            ddlUpdMigrationExamData.SelectedIndex = 0;

            //Added By Dileep Kare on 26.07.2021
            ddlReceiptCancel.DataSource = ds;
            ddlReceiptCancel.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlReceiptCancel.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlReceiptCancel.DataBind();
            ddlReceiptCancel.SelectedIndex = 0;
        }
    }
    protected void rdbMaintenance_SelectedIndexChanged(object sender, EventArgs e)
    {
        string script = string.Empty;
        if (rdbMaintenance.SelectedValue == "0")
        {
            // txtTimeDiff.Enabled = true;
            //txtMaintananceDateTime.Enabled = true;
            ddlTimeDiff.Enabled = true;
            ddlMainTimeSpan.Enabled = true;
            // txtMainTimeSpan.Enabled = true;
            script = "$('#txtMaintananceDateTime').prop('disabled', false);";
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "txtTime", "$('#txtMaintananceDateTime').prop('disabled', true);", true);
        }
        else
        {
            //txtTimeDiff.Enabled = false;
            ddlTimeDiff.Enabled = false;
            ddlMainTimeSpan.Enabled = false;
            // txtMaintananceDateTime.Enabled = false;
            // txtMainTimeSpan.Enabled = false;
            script = "$('#txtMaintananceDateTime').prop('disabled', true);";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "txtTime", script, true);
    }
}