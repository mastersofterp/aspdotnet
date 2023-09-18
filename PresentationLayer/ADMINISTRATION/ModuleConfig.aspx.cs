using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System.Data;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Web.Services;
using Newtonsoft.Json;
using mastersofterp_MAKAUAT;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Data.SqlClient;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;
public partial class ADMINISTRATION_ModuleConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ModuleConfigController objMConfig = new ModuleConfigController();
    ModuleConfig objMod = new ModuleConfig();
    int PayNo = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), null);//Set label -  Added By Nikhil L. on 17/01/2021

            //if (Session["userno"]=="1")
            //        {


            Session["AuthFlag"] = 0;
            objCommon.FillListBox(ddluser, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=5 and UA_STATUS=0", "UA_NO DESC");
            objCommon.FillListBox(ddlUserLogin, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=1 and UA_STATUS=0", "UA_NO");
            objCommon.FillListBox(ddlAttendanceuser, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID NOT IN (2,14)", "USERTYPEID");
            objCommon.FillListBox(ddlCourseUser, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID NOT IN (2,14) ", "USERTYPEID");
            objCommon.FillListBox(ddlCourseLock, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (2,14) and UA_STATUS=0", "UA_NO");
            objCommon.FillDropDownList(ddlPageName, "ACD_STUDENT_CONFIG", "DISTINCT ORGANIZATION_ID", "DISPLAYPAGENAME", "DISPLAYPAGENAME IS NOT NULL", "DISPLAYPAGENAME ASC");
            objCommon.FillListBox(lboModAdmInfo, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=1 and UA_STATUS=0", "UA_NO");
            BindData();
            BindCourseExamRegConfig();
            BindAttendanceMailConfig();
            BindListviewPayment();
            //    BindStudentData();
            //        }
            //    else{
            //        CheckPageAuthorization();
            //        }
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            
                //Select AL_Link from ACCESS_LINK where AL_URL='ACADEMIC/ModifyAdmissionInfo.aspx'
        }
     
        ViewState["action"] = "add";
    }

    private void BindData()
    {
        try
        {
            DataSet ds = objMConfig.GetModuleConfigData();
            string rdID = "";
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //string FieldName = ds.Tables[0].Rows[0]["FieldName"] == null ? string.Empty : ds.Tables[0].Rows[0]["FieldName"].ToString();

                    if (ds.Tables[0].Rows[0]["ALLOW_ROLLNO"].ToString() != null && ds.Tables[0].Rows[0]["ALLOW_ROLLNO"].ToString() == "1")
                    {
                        rdID = "rdRollNo";
                        hfRollNo.Value = "true";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        rdID = "rdRollNo";
                        hfRollNo.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["ALLOW_REGNO"].ToString() != null && ds.Tables[0].Rows[0]["ALLOW_REGNO"].ToString() == "1")
                    {
                        rdID = "rdRegno";
                        hfdregno.Value = "true";

                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        rdID = "rdRegno";
                        hfdregno.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["ALLOW_ENROLLNO"].ToString() != null && ds.Tables[0].Rows[0]["ALLOW_ENROLLNO"].ToString() == "1")
                    {
                        rdID = "rdenroll";
                        hfenroll.Value = "true";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        rdID = "rdenroll";
                        hfenroll.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ");", true);
                    //    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetActive('" + id + "');", true);
                    //}

                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetActive('" + id + "');", true);

                    if (ds.Tables[0].Rows[0]["STUD_INFO_MANDATE"].ToString() != null && ds.Tables[0].Rows[0]["STUD_INFO_MANDATE"].ToString() == "1")
                    {
                        rdID = "rdStudMandate";
                        hfStudMandate.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        rdID = "rdStudMandate";
                        hfStudMandate.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }


                    if (ds.Tables[0].Rows[0]["STUD_ADM_PAYMENT_BTN"].ToString() != null && ds.Tables[0].Rows[0]["STUD_ADM_PAYMENT_BTN"].ToString() == "1")
                    {
                        rdID = "rdonlinepaymentbtn";
                        hfOnlinePaymentbtn.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript2", "Semadmbtn(true);", true);

                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        rdID = "rdonlinepaymentbtn";
                        hfOnlinePaymentbtn.Value = "false";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }
                    if (ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString() != null)
                    {
                        ddlEmailType.SelectedValue = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["REGNO_CREATION"].ToString() != null && ds.Tables[0].Rows[0]["REGNO_CREATION"].ToString() == "1")
                    {
                        rdID = "chkRegnocreation";
                        hfchkRegnocreation.Value = "true";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript3", "feescollregnocreation(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript6", "feescollregnocreation(true);", true);
                    }
                    else
                    {
                        rdID = "chkRegnocreation";
                        hfchkRegnocreation.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript6", "feescollregnocreation(false);", true);
                    }
                    if (ds.Tables[0].Rows[0]["FACULTY_ADVISOR_APPROVAL"].ToString() != null && ds.Tables[0].Rows[0]["FACULTY_ADVISOR_APPROVAL"].ToString() == "1")
                    {
                        rdID = "chkFaculyAdvisorApp";
                        hfchkFaculyAdvisorApp.Value = "true";
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript25", "facultadvisorallot(true);", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "facultadvisorallot(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        rdID = "chkFaculyAdvisorApp";
                        hfchkFaculyAdvisorApp.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["NEW_STUD_EMAIL_SEND"].ToString() != null && ds.Tables[0].Rows[0]["NEW_STUD_EMAIL_SEND"].ToString() == "1")
                    {
                        rdID = "hfchknewstudentemail";
                        hfchknewstudentemail.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript452565645", "NewstudEmailSend(true);", true);
                    }
                    else
                    {

                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript452565645", "NewstudEmailSend(false);", true);
                    }


                    if (ds.Tables[0].Rows[0]["THIRDPARTY_DOC_VERIFICATION"].ToString() != null && ds.Tables[0].Rows[0]["THIRDPARTY_DOC_VERIFICATION"].ToString() == "1")
                    {
                        rdID = "chkAllowDocumentVerification";
                        hfchkAllowDocumentVerification.Value = "true";
                        //  ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "paymentmailsend(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src4", "paymentmailsend(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9552511", "docverification(true);", true);
                    }
                    else
                    {
                        //rdID = "chkAllowDocumentVerification";
                        //hfchksendpaymentmailstudentry.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9552511", "docverification(false);", false);

                    }

                    if (ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString() != null && ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString() == "1")
                    {
                        rdID = "chksendpaymentmailstudentry";
                        hfchksendpaymentmailstudentry.Value = "true";
                        //  ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "docverification(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript955662511", "paymentmailsend(true);", true);
                    }
                    else
                    {
                        //rdID = "chksendpaymentmailstudentry";
                        //hfchkAllowDocumentVerification.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript955662511", "paymentmailsend(false);", false);
                    }

                    if (ds.Tables[0].Rows[0]["FEES_COLL_USER_CREATION"].ToString() != null && ds.Tables[0].Rows[0]["FEES_COLL_USER_CREATION"].ToString() == "1")
                    {
                        // rdID = "chkUserCreationonFee"; 
                        // hfchkUserCreationonFee.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        // rdID = "chkUserCreationonFee";
                        // hfchkUserCreationonFee.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(false);", true);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["NEW_STUD_USER_CREATION"].ToString() != null && ds.Tables[0].Rows[0]["NEW_STUD_USER_CREATION"].ToString() == "1")
                    {
                        rdID = "hfchkcreateusernewstudentry";
                        hfchkcreateusernewstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript7", "newstudentryusercreation(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript7", "newstuduser(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    //Added By Vinay Mishra on 01/08/2023 - New Flags Added & Submitted
                    if (ds.Tables[0].Rows[0]["CREATE_PARENT_USER_NEWSTUD"].ToString() != null && ds.Tables[0].Rows[0]["CREATE_PARENT_USER_NEWSTUD"].ToString() == "1")
                    {
                        rdID = "hfdchkcreateusernewprntentry";
                        hfdchkcreateusernewprntentry.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript786", "newchkcreateusernewprntentry(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript786", "newchkcreateusernewprntentry(false);", false);
                    }

                    if (ds.Tables[0].Rows[0]["NEW_STUD_REGNO_GEN"].ToString() != null && ds.Tables[0].Rows[0]["NEW_STUD_REGNO_GEN"].ToString() == "1")
                    {
                        rdID = "hfdchkCreateRegno";
                        hfdchkCreateRegno.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript765852654", "newchkCreateRegno(true);", true);  
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript765852654", "newchkCreateRegno(false);", false);

                    }

                    if (ds.Tables[0].Rows[0]["VALUE_ADDED_ON_TEACHINGPLAN_ATT"].ToString() != null && ds.Tables[0].Rows[0]["VALUE_ADDED_ON_TEACHINGPLAN_ATT"].ToString() == "1")
                    {
                        rdID = "hfdchkAttTeaching";
                        hfdchkAttTeaching.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript7366933", "newschkAttTeaching(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript7366933", "newschkAttTeaching(false);", false);

                    }



                    if (ds.Tables[0].Rows[0]["TRI_SEM_STATUS"].ToString() != null && ds.Tables[0].Rows[0]["TRI_SEM_STATUS"].ToString() == "1")
                    {
                        rdID = "hfchkAllowTrisemester";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript98", "trisemester(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript98", "trisemester(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }


                    if (ds.Tables[0].Rows[0]["CHECK_PREV_SEM_OUTSNADING"].ToString() != null && ds.Tables[0].Rows[0]["CHECK_PREV_SEM_OUTSNADING"].ToString() == "1")
                    {
                        rdID = "hfchkoutstandingfees";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript981", "outstanding(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript981", "outstanding(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["SEM_PROM_DEMAND_CREATION"].ToString() != null && ds.Tables[0].Rows[0]["SEM_PROM_DEMAND_CREATION"].ToString() == "1")
                    {
                        rdID = "hfchkdemandcreationsempromo";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9811", "demandcreationsempromo(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9811", "demandcreationsempromo(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["SEM_ADM_OFF_PAY_BTN"].ToString() != null && ds.Tables[0].Rows[0]["SEM_ADM_OFF_PAY_BTN"].ToString() == "1")
                    {
                        rdID = "hfchkafteresempromotion";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript952511", "semadmofflinebtn(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript952511", "semadmofflinebtn(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["SEM_PROM_BEFORE_SEM_ADM"].ToString() != null && ds.Tables[0].Rows[0]["SEM_PROM_BEFORE_SEM_ADM"].ToString() == "1")
                    {
                        rdID = "hfchkbeforesempromotion";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript511", "semadmbeforepromotion(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript511", "semadmbeforepromotion(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["SEM_PROM_AFTER_SEM_ADM"].ToString() != null && ds.Tables[0].Rows[0]["SEM_PROM_AFTER_SEM_ADM"].ToString() == "1")
                    {
                        rdID = "hfchkafteresempromotion";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript95252511", "semadmafterpromotion(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript95252511", "semadmafterpromotion(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["REACTIVATION_LATE_FINE_FLAG"].ToString() != null && ds.Tables[0].Rows[0]["REACTIVATION_LATE_FINE_FLAG"].ToString() == "1")
                    {
                        rdID = "hfdchkStdReactivationfee";
                        hfchksendemailonstudentry.Value = "true";
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src7", "newstuduser(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript98198", "studentReactivationfee(true);", true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript13", "Feescollusercreation(true);", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'true');", true);
                    }
                    else
                    {
                        //rdID = "chkUserCreationonFee";
                        //hfchkUserCreationonFee.Value = "false";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "newstuduser(true);", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript98198", "studentReactivationfee(false);", false);
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + ",'false');", true);
                    }

                    if (ds.Tables[0].Rows[0]["SEM_ADM_WITH_PAYMENT"].ToString() != null && ds.Tables[0].Rows[0]["SEM_ADM_WITH_PAYMENT"].ToString() == "1")
                    {
                        hfchksendemailonstudentry.Value = "true";
                        hfdSemAdmWithPayment.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9525251111", "SemAdmWithPayment(true);", true);
                    }
                    else
                    {
                        hfdSemAdmWithPayment.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript95252511111", "SemAdmWithPayment(false);", false);
                    }

                    if (ds.Tables[0].Rows[0]["IS_DEPARTMENT_ELECTIVE_CAPACITY_CHECK"].ToString() != null && ds.Tables[0].Rows[0]["IS_DEPARTMENT_ELECTIVE_CAPACITY_CHECK"].ToString() == "1")
                    {
                        hfdchkIntakeCapacity.Value = "true";
                        hfdchkIntakeCapacity.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript95252511113566", "IntakeCapacity(true);", true);
                    }
                    else
                    {
                        hfdchkIntakeCapacity.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript95252511113566", "IntakeCapacity(false);", false);
                    }

                    if (ds.Tables[0].Rows[0]["IS_SHORTNAME"].ToString() != null && ds.Tables[0].Rows[0]["IS_SHORTNAME"].ToString() == "1")
                    {
                        hfdchktimeReport.Value = "true";
                        hfdchktimeReport.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript64103658", "TimeTableReport(true);", true);
                    }
                    else
                    {
                        hfdchktimeReport.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript64103658", "TimeTableReport(false);", false);
                    }
                    if (ds.Tables[0].Rows[0]["IS_GLOBAL_ELECTIVE_CT_ALLOTMENT_REQUIRED"].ToString() != null && ds.Tables[0].Rows[0]["IS_GLOBAL_ELECTIVE_CT_ALLOTMENT_REQUIRED"].ToString() == "1")
                    {
                        hfdchkGlobalCTAllotment.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript64103658111", "GlobalElectiveCTAllotment(true);", true);
                    }
                    else
                    {
                        hfdchkGlobalCTAllotment.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript64103658111", "GlobalElectiveCTAllotment(false);", false);
                    }
                    if (ds.Tables[0].Rows[0]["IS_VALUE_ADDED_CT_ALLOTMENT_REQUIRED"].ToString() != null && ds.Tables[0].Rows[0]["IS_VALUE_ADDED_CT_ALLOTMENT_REQUIRED"].ToString() == "1")
                    {
                        hfdchkValueAddedCTAllotment.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript641036581111565", "ValueAddedCTAllotment(true);", true);
                    }
                    else
                    {
                        hfdchkValueAddedCTAllotment.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript641036581111565", "ValueAddedCTAllotment(false);", false);
                    }

                    if (ds.Tables[0].Rows[0]["BBC_MAIL_NEW_STUD_ENTRY"].ToString() != null)
                        txtaddBCC.Text = ds.Tables[0].Rows[0]["BBC_MAIL_NEW_STUD_ENTRY"].ToString();
                    else
                        txtaddBCC.Text = string.Empty;

                    if (ds.Tables[0].Rows[0]["HOSTE_TYPE_ONLINE_PAY"].ToString() != null && ds.Tables[0].Rows[0]["HOSTE_TYPE_ONLINE_PAY"].ToString() == "1")
                    {
                        hfdchkhosteltypeop.Value = "true";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript641036581113345", "HostelStatusOnPayment(true);", true);
                    }
                    else
                    {
                        hfdchkhosteltypeop.Value = "false";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript641036581113345", "HostelStatusOnPayment(false);", false);
                    }

                    if (ds.Tables[0].Rows[0]["IS_SELECT_CHOICEFOR_OF_ELECT_CRS_FROM_CRDIT_DEFINITION_PAGE"].ToString() != null && ds.Tables[0].Rows[0]["IS_SELECT_CHOICEFOR_OF_ELECT_CRS_FROM_CRDIT_DEFINITION_PAGE"].ToString() == "1")
                    {
                        rdID = "chkElectChoiceFor";
                        hfElectChoiceFor.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "SrcElectiveChoiceFor", "ElectiveChoiceFor(true);", true);
                    }
                    else
                    {
                        rdID = "chkElectChoiceFor";
                        hfElectChoiceFor.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "SrcElectiveChoiceFor", "ElectiveChoiceFor(false);", true);
                    }
                    if (ds.Tables[0].Rows[0]["CHECK_SEAT_CAPACITY_NEW_STUD"].ToString() != null && ds.Tables[0].Rows[0]["CHECK_SEAT_CAPACITY_NEW_STUD"].ToString() == "1")
                    {
                        rdID = "chkseatcapacitynewstudentry";
                        hfSeatcapacitynewstud.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Srcseatwisecapacity", "SeatcapacityNewStud(true);", true);
                    }
                    else
                    {
                        rdID = "chkseatcapacitynewstudentry";
                        hfSeatcapacitynewstud.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Srcseatwisecapacity", "SeatcapacityNewStud(false);", true);
                    }
                    if (ds.Tables[0].Rows[0]["STUD_DASH_OUTSTANING"].ToString() != null && ds.Tables[0].Rows[0]["STUD_DASH_OUTSTANING"].ToString() == "1")
                    {
                        rdID = "chkoutstandingdashorad";
                        hfoutstandingdashorad.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Srcdashboardoutstanding", "dashboardoutstanding(true);", true);
                    }
                    else
                    {
                        rdID = "chkoutstandingdashorad";
                        hfoutstandingdashorad.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Srcdashboardoutstanding", "dashboardoutstanding(false);", true);
                    }

                    if (ds.Tables[0].Rows[0]["DISPLAY_STUD_LOGIN_DASHBOARD"].ToString() != null && ds.Tables[0].Rows[0]["DISPLAY_STUD_LOGIN_DASHBOARD"].ToString() == "1")
                    {
                        //rdID = "chkoutstandingdashorad";
                        //hfoutstandingdashorad.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "SrcDisplayStudLoginDashboard", "DisplayStudLoginDashboard(true);", true);
                    }
                    else
                    {
                        //rdID = "chkoutstandingdashorad";
                        //hfoutstandingdashorad.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "SrcDisplayStudLoginDashboard", "DisplayStudLoginDashboard(false);", true);
                    }

                    if (ds.Tables[0].Rows[0]["DISPLAY_HTML_REPORT"].ToString() != null && ds.Tables[0].Rows[0]["DISPLAY_HTML_REPORT"].ToString() == "1")
                    {
                        //rdID = "chkoutstandingdashorad";
                        //hfoutstandingdashorad.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "DISPLAY_HTML_REPORT", "CheckReceiptDisplayInHTMLFormat(true);", true);
                    }
                    else
                    {
                        //rdID = "chkoutstandingdashorad";
                        //hfoutstandingdashorad.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "DISPLAY_HTML_REPORT", "CheckReceiptDisplayInHTMLFormat(false);", true);
                    }

                    if (ds.Tables[0].Rows[0]["SLOTS_MAND_FOR_TP"].ToString() != null && ds.Tables[0].Rows[0]["SLOTS_MAND_FOR_TP"].ToString() == "1")
                    {
                        rdID = "chkslotmand";
                        hfoutstandingdashorad.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Srcslotmandatory", "slotmandatory(true);", true);
                    }
                    else
                    {
                        rdID = "chkslotmand";
                        hfoutstandingdashorad.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Srcslotmandatory", "slotmandatory(false);", true);
                    }

                    if (ds.Tables[0].Rows[0]["ALLOW_CURRENT_SEM_FOR_REDO_IMPROVEMENT_CRS_REG"].ToString() != null && ds.Tables[0].Rows[0]["ALLOW_CURRENT_SEM_FOR_REDO_IMPROVEMENT_CRS_REG"].ToString() == "1")
                    {
                        hfdRedoImprovementCourseRegFlag.Value = "true";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CurrentSemForRedoImprovementCrsReg", "CheckAllowCurrentSemForRedoImprovementCrsReg(true);", true);
                    }
                    else
                    {
                        hfdRedoImprovementCourseRegFlag.Value = "false";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CurrentSemForRedoImprovementCrsReg", "CheckAllowCurrentSemForRedoImprovementCrsReg(false);", true);
                    }
                    

                    char delimiterChars = ',';

                    string usersnos = ds.Tables[0].Rows[0]["HEAD_TO_HEAD_ADJ_PAGE_USERS"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["HEAD_TO_HEAD_ADJ_PAGE_USERS"].ToString();
                    string[] utype = usersnos.Split(delimiterChars);

                    for (int j = 0; j < utype.Length; j++)
                    {
                        for (int i = 0; i < ddluser.Items.Count; i++)
                        {
                            if (utype[j] == ddluser.Items[i].Value)
                            {
                                ddluser.Items[i].Selected = true;
                            }
                        }
                    }


                    char tpslots = ',';

                    string tpslotsuser = ds.Tables[0].Rows[0]["ATTENDANCE_USER_TYPES"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["ATTENDANCE_USER_TYPES"].ToString();
                    string[] slotutype = tpslotsuser.Split(tpslots);

                    for (int j = 0; j < slotutype.Length; j++)
                    {
                        for (int i = 0; i < ddlAttendanceuser.Items.Count; i++)
                        {
                            if (slotutype[j] == ddlAttendanceuser.Items[i].Value)
                            {
                                ddlAttendanceuser.Items[i].Selected = true;
                            }
                        }
                    }

                    //Added By Vinay Mishra on 01/08/2023 - To Add New Flags & Submission
                    string tpslotsuserc = ds.Tables[0].Rows[0]["USER_TYPES_COURSE_CREATION"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["USER_TYPES_COURSE_CREATION"].ToString();
                    string[] slotutypec = tpslotsuserc.Split(tpslots);

                    for (int j = 0; j < slotutypec.Length; j++)
                    {
                        for (int i = 0; i < ddlCourseUser.Items.Count; i++)
                        {
                            if (slotutypec[j] == ddlCourseUser.Items[i].Value)
                            {
                                ddlCourseUser.Items[i].Selected = true;
                            }
                        }
                    }

                    string userLoginNos = ds.Tables[0].Rows[0]["AUTHORISED_USERS_FOR_GO_TO_USERLOGIN"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["AUTHORISED_USERS_FOR_GO_TO_USERLOGIN"].ToString();
                    string[] usrtype = userLoginNos.Split(',');

                    for (int j = 0; j < usrtype.Length; j++)
                    {
                        for (int i = 0; i < ddlUserLogin.Items.Count; i++)
                        {
                            if (usrtype[j] == ddlUserLogin.Items[i].Value)
                                ddlUserLogin.Items[i].Selected = true;
                        }
                    }

                    //Added By Vinay Mishra on 01/08/2023 - To Add New Flags & Submission
                    string userLoginNosL = ds.Tables[0].Rows[0]["USERS_COURSE_LOCK_UNLOCK"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["USERS_COURSE_LOCK_UNLOCK"].ToString();
                    string[] usrtypeL = userLoginNosL.Split(',');

                    for (int j = 0; j < usrtypeL.Length; j++)
                    {
                        for (int i = 0; i < ddlCourseLock.Items.Count; i++)
                        {
                            if (usrtypeL[j] == ddlCourseLock.Items[i].Value)
                                ddlCourseLock.Items[i].Selected = true;
                        }
                    }

                    string ModAdmInfoUserNos = ds.Tables[0].Rows[0]["AUTHORISED_USERS_FOR_MODIFY_ADMISSION_INFO"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["AUTHORISED_USERS_FOR_MODIFY_ADMISSION_INFO"].ToString();
                    string[] ModAdmInfoUsrtype = ModAdmInfoUserNos.Split(',');

                    for (int j = 0; j < ModAdmInfoUsrtype.Length; j++)
                    {
                        for (int i = 0; i < lboModAdmInfo.Items.Count; i++)
                        {
                            if (ModAdmInfoUsrtype[j] == lboModAdmInfo.Items[i].Value)
                                lboModAdmInfo.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.BindData-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindCourseExamRegConfig()
    {
        DataSet ds1 = objCommon.FillDropDown("ACD_MODULE_CONFIG_COURSE_EXAM_REG", " *", "", "ISNULL(ORGID,0)=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
        string rdID = "";
        if (ds1 != null)
        {
            ViewState["MODULE_CONFIG_DATA_FOR_COURSE_EXAM_REG"] = ds1;
            hfCourseExamRegData.Value = ds1.GetXml();
            DataRow[] dr = null;

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                dr = ds1.Tables[0].Select("COLLEGE_ID = 0 AND ISNULL(ORGID,0)=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                if (dr != null && dr.Count() > 0)
                {
                    ViewState["ConfigId"] = Convert.ToInt32(dr[0]["CONFIGID"]);
                    if (Convert.ToInt32(dr[0]["COURSE_EXAM_REG_BOTH"]) != null && Convert.ToBoolean(dr[0]["COURSE_EXAM_REG_BOTH"]) == true)
                    {
                        rdID = "rdRegSame";
                        hfRegSame.Value = "true";
                    }
                    else
                    {
                        rdID = "rdRegSame";
                        hfRegSame.Value = "false";
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + "," + hfRegSame.Value + ");", true);
                }
            }
        }
    }

    private void BindAttendanceMailConfig()
    {
        DataSet dsAtt = objMConfig.GetAttendanceTriggerMailConfig();
        if (dsAtt.Tables != null)
        {
            if (dsAtt.Tables[0].Rows.Count > 0)
            {
                txtStudCC.Text = dsAtt.Tables[0].Rows[0]["STUD_CC_MAIL"].ToString();
                txtStudBCC.Text = dsAtt.Tables[0].Rows[0]["STUD_BCC_MAIL"].ToString();
                txtFacMail.Text = dsAtt.Tables[0].Rows[0]["DAILY_FAC_TO_MAIL"].ToString();
                txtFacCC.Text = dsAtt.Tables[0].Rows[0]["DAILY_FAC_CC_MAIL"].ToString();
                txtFacBCC.Text = dsAtt.Tables[0].Rows[0]["DAILY_FAC_BCC_MAIL"].ToString();
                txtAbMail.Text = dsAtt.Tables[0].Rows[0]["ABSENT_STUD_TO_MAIL"].ToString();
                txtAbCC.Text = dsAtt.Tables[0].Rows[0]["ABSENT_STUD_CC_MAIL"].ToString();
                txtAbBCC.Text = dsAtt.Tables[0].Rows[0]["ABSENT_STUD_BCC_MAIL"].ToString();
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {        
        try
        {
            if (hfdregno.Value == "true")
            {
                objMod.AllowRegno = true;
                //objMod.AllowRollno = false;
                //objMod.AllowEnrollno = false;
            }

            if (hfRollNo.Value == "true")
            {
                //objMod.AllowRegno = false;
                objMod.AllowRollno = true;
                //objMod.AllowEnrollno = false;
            }

            if (hfenroll.Value == "true")
            {
                //objMod.AllowRegno = false;
                //objMod.AllowRollno = false;
                objMod.AllowEnrollno = true;
            }



            //if (rdoRegSeperate.Checked == true)
            //{
            //    objMod.course_exam_reg_seperate = 1;
            //    //rdoRegSame.Visible = false;
            //}
            //else
            //{
            //    objMod.course_exam_reg_seperate = 0;
            //    //rdoRegSame.Visible = true;
            //}

            //if (rdoRegSame.Checked == true)
            //{
            //    objMod.course_exam_reg_both = 1;
            //    //rdoRegSeperate.Visible = false;
            //}
            //else
            //{
            //    objMod.course_exam_reg_both = 0;
            //    //rdoRegSeperate.Visible = true;
            //}


            //if (hfRegSame.Value == "true")
            //{
            //    objMod.CourseExmRegSame = true;
            //}

            if (hfStudMandate.Value == "true")
            {
                objMod.StudInfoMandate = true;
            }
            if (hfOnlinePaymentbtn.Value == "true")
            {
                objMod.OnlinebtnStudadm = true;
            }

            objMod.EmailType = Convert.ToInt32(ddlEmailType.SelectedValue);

            if (hfRegnocreation.Value == "true")
            {
                objMod.RegnoGenFeeCollection = true;
            }

            if (hfchknewstudentemail.Value == "true")
            {
                objMod.NewStudEmailSend = true;
            }

            if (hfchkFaculyAdvisorApp.Value == "true")
            {
                objMod.FacultyAdvisorApp = true;
            }

            if (hfchkRegnocreation.Value == "true")
            {
                objMod.RegnoGenFeeCollection = true;
            }

            if (hfchkUserCreationonFee.Value == "true")
            {
                objMod.FeeCollUserCreation = true;
            }

            if (hfchksendpaymentmailstudentry.Value == "true")
            {
                objMod.TPPaymentLinkSendMail = true;
            }


            if (hfchkAllowDocumentVerification.Value == "true")
            {
                objMod.TPDocverificationAllow = true;
            }


            bool Trisemstatus = false;
            bool chkoutstanding = false;
            bool sempromodemandcreation = false;
            bool semadmofflinebtn = false;
            bool semadmbeforepromotion = false;
            bool semadmafterepromotion = false;
            bool studReactvationlarefine = false;
            bool IntakeCapacity = false;
            bool chktimeReport = false;
            bool chkGlobalCTAllotment = false;
            bool chkValueAddedCTAllotment = false;
            bool Seatcapacitynewstud = false;
            bool dashboardoutstanding = false;
            bool DisplayStudLoginDashboard = (hfchkAllowToDisplayStudLoginDashboard.Value == "true") ? true : false;
            bool TPSlot = false;
            bool DisplayReceiptInHTMLFormat = (hfReceiptDisplayInHTML_Format.Value == "true") ? true : false;
            bool createprnt = false;
            bool CreateRegno=false;
            bool AttTeaching=false;

            if (hfchkcreateusernewstudentry.Value == "true")
            {
                objMod.NewStudUserCreation = true;
            }

            if (hfchkAllowTrisemester.Value == "true")
            {
                Trisemstatus = true;
            }
            if (hfchkoutstandingfees.Value == "true")
            {
                chkoutstanding = true;
            }
            if (hfchkdemandcreationsempromo.Value == "true")
            {
                sempromodemandcreation = true;

            }

            if (hfSemadmOfflinebtn.Value == "true")
            {
                semadmofflinebtn = true;

            }
            if (hfchkbeforesempromotion.Value == "true")
            {
                semadmbeforepromotion = true;

            }
            if (hfchkafteresempromotion.Value == "true")
            {
                semadmafterepromotion = true;

            }

            if (hfdchkStdReactivationfee.Value == "true")
            {
                studReactvationlarefine = true;

            }

            if (hfdchkIntakeCapacity.Value == "true")
            {
                IntakeCapacity = true;

            }

            if (hfdchktimeReport.Value == "true")
            {
                chktimeReport = true;

            }
            if (hfdchkGlobalCTAllotment.Value == "true")
            {
                chkGlobalCTAllotment = true;

            }
            if (hfdchkValueAddedCTAllotment.Value == "true")
            {
                chkValueAddedCTAllotment = true;

            }

            if (hfSeatcapacitynewstud.Value == "true")
            {
                Seatcapacitynewstud = true;

            }

            if (hfoutstandingdashorad.Value == "true")
            {
                dashboardoutstanding = true;
            }
            if (hfchkslotmand.Value == "true")
            {
                TPSlot = true;
            }

            if (hfdchkCreateRegno.Value == "true")
            {
                CreateRegno = true;
            }

            if (hfdchkAttTeaching.Value == "true")
            {
                 AttTeaching= true;
            }

            if (hfdchkcreateusernewprntentry.Value == "true")
            {
                createprnt = true;
            }

            objMod.SemAdmWithPayment = (hfdSemAdmWithPayment.Value == "true") ? true : false;

            string BBCEMAIL_NEW_STUD = string.Empty;
            bool HostelTypeSelection = false;
            BBCEMAIL_NEW_STUD = txtaddBCC.Text;

            if (hfdchkhosteltypeop.Value == "true")
            {
                HostelTypeSelection = true;

            }

            if (objMod.SemAdmWithPayment == true)
            {
                if (objMod.OnlinebtnStudadm == false && semadmofflinebtn == false)
                {
                    objCommon.DisplayMessage(this.updpnl_details, "Please select atleast one option from Semester Admission Online or Offline Payment Button is Visible !", this.Page);
                    this.BindData();
                    return;
                }
            }



            bool chkElectChoiceFor = (hfElectChoiceFor.Value == "true") ? true : false;

            int UANO = 0;
            UANO = Convert.ToInt32(Session["userno"].ToString());
            string IP_ADDRESS = string.Empty;
            string MAC_ID = string.Empty;
            IP_ADDRESS = Session["ipAddress"].ToString();
            MAC_ID = Session["macAddress"].ToString();

            string Usernos = "";
            string attendanceusertype = "";
            string usercourseshow = "";
            foreach (ListItem items in ddluser.Items)
            {
                if (items.Selected == true)
                {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    Usernos += items.Value + ',';
                }
            }
            if (Usernos != "")
            {
                Usernos = Usernos.Remove(Usernos.Length - 1);
            }


            foreach (ListItem items in ddlAttendanceuser.Items)
            {
                if (items.Selected == true)
                {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    attendanceusertype += items.Value + ',';
                }
            }
            if (attendanceusertype != "")
            {
                attendanceusertype = attendanceusertype.Remove(attendanceusertype.Length - 1);
            }

            //Added By Vinay Mishra on 01/08/2023 - To Add New Flag For Course Creation View in Module Config Page
            foreach (ListItem items in ddlCourseUser.Items)
            {
                if (items.Selected == true)
                {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    usercourseshow += items.Value + ',';
                }
            }
            if (usercourseshow != "")
            {
                usercourseshow = usercourseshow.Remove(usercourseshow.Length - 1);
            }

            string UserLoginNos = string.Empty;
            foreach (ListItem items in ddlUserLogin.Items)
            {
                if (items.Selected == true)
                    UserLoginNos += items.Value + ',';
            }

            UserLoginNos = UserLoginNos.TrimEnd(',');

            //Added By Vinay Mishra on 01/08/2023 - To Add New Flag For Course Lock/Unlock in Module Config Page
            string usercourselocked = string.Empty;
            foreach (ListItem items in ddlCourseLock.Items)
            {
                if (items.Selected == true)
                    usercourselocked += items.Value + ',';
            }

            usercourselocked = usercourselocked.TrimEnd(',');
            int allowCurrSemForRedoImprovementCrsReg = (hfdRedoImprovementCourseRegFlag.Value == "true") ? 1 : 0;


            string ModAdmInfoUserNos = string.Empty;
            foreach (ListItem items in lboModAdmInfo.Items)
            {
                if (items.Selected == true)
                    ModAdmInfoUserNos += items.Value + ',';
            }

            ModAdmInfoUserNos = ModAdmInfoUserNos.TrimEnd(',');



            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["ConfigId"] != null)
                {
                    objMod.Configid = Convert.ToInt32(ViewState["ConfigId"]);
                }

                if (!string.IsNullOrEmpty(Session["AuthFlag"].ToString()) && Session["AuthFlag"].ToString() == "1")
                {
                    CustomStatus cs = (CustomStatus)objMConfig.SaveModuleConfiguration(objMod, UANO, IP_ADDRESS, MAC_ID, Trisemstatus, chkoutstanding, sempromodemandcreation, semadmofflinebtn,
                        semadmbeforepromotion, semadmafterepromotion, studReactvationlarefine, IntakeCapacity, chktimeReport, chkGlobalCTAllotment,
                        BBCEMAIL_NEW_STUD, HostelTypeSelection, chkElectChoiceFor, Seatcapacitynewstud, Usernos, dashboardoutstanding, attendanceusertype, usercourseshow, TPSlot, UserLoginNos, usercourselocked,
                        DisplayStudLoginDashboard, DisplayReceiptInHTMLFormat, chkValueAddedCTAllotment, CreateRegno, AttTeaching, createprnt, allowCurrSemForRedoImprovementCrsReg, ModAdmInfoUserNos); //3 Additional Parameters Passed By Vinay Mishra on 01/08/2023 for New Flag in Module Config

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //Clear();
                        objCommon.DisplayMessage(this.updpnl_details, "Record Saved Successfully!", this.Page);
                        this.BindData();
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        //Clear();
                        objCommon.DisplayMessage(this.updpnl_details, "Record Updated Successfully!", this.Page);
                        this.BindData();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnl_details, "Record Already exist", this.Page);
                    }
                    //Clear();
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnl_details, "Password access required to submit the data!", this.Page);
                    this.BindData();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    //To Clear Fields
    private void Clear()
    {
        hfRollNo.Value = "false";
        hfdregno.Value = "false";
        hfenroll.Value = "false";
        ViewState["action"] = "add";
        ViewState["ConfigId"] = null;
        //Response.Redirect(Request.Url.ToString());
    }

    private void BindStudentData()
    {
        try
        {
            //DataSet ds = objMConfig.GetStudentConfigData();
            //lvStudentConfig.DataSource = ds;
            //lvStudentConfig.DataBind();
            //objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBatchName);//Set label 

            //foreach (ListViewItem item in lvStudentConfig.Items)
            //{
            //    Label lblactinestatus = item.FindControl("lblactinestatus") as Label;
            //    if (lblactinestatus.Text == "1")
            //    {
            //        lblactinestatus.Text = "Active";
            //        lblactinestatus.Style.Add("color", "Green");
            //    }
            //    else
            //    {
            //        lblactinestatus.Text = "DeActive";
            //        lblactinestatus.Style.Add("color", "Red");
            //    }
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.BindStudentData-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    [WebMethod]
    public static string GetStudentConfigData(int OrgID, string PageNo, string PageName)
    {
        ModuleConfigController objMConfig = new ModuleConfigController();
        DataSet ds = objMConfig.GetStudentConfigData();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string SaveUpdateStudentconfig(List<StudentModuleConfig> StudentConfig)
    {
        string status = string.Empty;
        Common objCommon = new Common();
        ModuleConfigController objMConfig = new ModuleConfigController();
        CustomStatus cs = (CustomStatus)objMConfig.SaveUpdateStudentConfig(StudentConfig);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            status = "Record Saved Successfully!";
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            status = "Record Updated Successfully!";
        }
        else
        {
            status = "Record Already exist";
        }
        return status;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
        }
    }

    protected void btnCourseExamReg_Click(object sender, EventArgs e)
    {        
        try
        {
            if (hfRegSame.Value == "true")
                objMod.CourseExmRegSame = true;

            int UANO = Convert.ToInt32(Session["userno"].ToString());
            string IP_ADDRESS = Session["ipAddress"].ToString();
            int college_ID = Convert.ToInt32(ddlCollege.SelectedValue);
            if (hfSelectCollege.Value == "true" && college_ID <= 0)
            {
                objCommon.DisplayMessage(this.updpnl_details, "Please select College !", this.Page);
                this.BindData();
                BindCourseExamRegConfig();
                return;
            }

            if (ViewState["action"] != null)
            {
                DataSet ds = (DataSet)ViewState["MODULE_CONFIG_DATA_FOR_COURSE_EXAM_REG"];
                DataRow[] dr = null;

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (college_ID > 0)
                        dr = ds.Tables[0].Select("COLLEGE_ID = " + college_ID); // + " AND CONFIGID =" + Convert.ToInt32(ViewState["ConfigId"]) // + " AND DEGREENO = " + objcls.DegreeNo + " AND SCHEMENO = " + objcls.Schemeno + " AND COURSENO = " + objcls.Courseno + " AND BRANCHNO = " + objcls.BranchNo + " AND TO_SEMESTERNO=" + objcls.To_semesterno);
                    else
                        dr = ds.Tables[0].Select("COLLEGE_ID = 0 AND CONFIGID =" + Convert.ToInt32(ViewState["ConfigId"]));
                }

                if (dr != null && dr.Count() > 0)
                {
                    if (college_ID > 0)
                        objMod.Configid = Convert.ToInt32(dr[0]["CONFIGID"]);
                    else
                    {
                        if (ViewState["ConfigId"] != null)
                            objMod.Configid = Convert.ToInt32(ViewState["ConfigId"]);
                    }
                }
                else
                    objMod.Configid = 0;

                CustomStatus cs = (CustomStatus)objMConfig.UpsertCourseExamRegConfig(objMod, college_ID);

                if (cs.Equals(CustomStatus.RecordSaved))
                    objCommon.DisplayMessage(this.updpnl_details, "Record Saved Successfully!", this.Page);
                else if (cs.Equals(CustomStatus.RecordUpdated))
                    objCommon.DisplayMessage(this.updpnl_details, "Record Updated Successfully!", this.Page);
                else
                    objCommon.DisplayMessage(this.updpnl_details, "Record Already exist", this.Page);

                BindData();
                BindCourseExamRegConfig();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.btnCourseExamReg_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void chkSelectCollege_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkSelectCollege.Checked)
    //        dvCollege.Visible = true;
    //    else
    //        dvCollege.Visible = false;
    //}

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)ViewState["MODULE_CONFIG_DATA_FOR_COURSE_EXAM_REG"];
        DataRow[] dr = null;

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
                dr = ds.Tables[0].Select("COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue));
            string rdID = string.Empty;
            if (dr != null && dr.Count() > 0)
            {
                if (Convert.ToInt32(dr[0]["COURSE_EXAM_REG_BOTH"]) != null && Convert.ToBoolean(dr[0]["COURSE_EXAM_REG_BOTH"]) == true)
                {
                    rdID = "rdRegSame";
                    hfRegSame.Value = "true";
                }
                else
                {
                    rdID = "rdRegSame";
                    hfRegSame.Value = "false";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(" + rdID + "," + hfRegSame.Value + ");", true);
            }
        }
    }

    protected void btnSubmitMail_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = (CustomStatus)objMConfig.InsertAttendanceMailConfig(txtFacMail.Text, txtAbMail.Text, txtStudCC.Text, txtStudBCC.Text, txtFacCC.Text, txtFacBCC.Text, txtAbCC.Text, txtAbBCC.Text);

            if (cs.Equals(CustomStatus.RecordSaved))
                objCommon.DisplayMessage(this.updpnl_details, "Record Saved Successfully!", this.Page);
            else
                objCommon.DisplayMessage(this.updpnl_details, "Record Updated Successfully!", this.Page);
            BindAttendanceMailConfig();
            BindData();
            BindCourseExamRegConfig();
            BindListviewPayment();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.btnSubmitMail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void bntCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        Response.Redirect(Request.Url.ToString());
    }

    private void ClearControls()
    {
        txtStudCC.Text = string.Empty;
        txtStudBCC.Text = string.Empty;
        txtFacMail.Text = string.Empty;
        txtFacCC.Text = string.Empty;
        txtFacBCC.Text = string.Empty;
        txtAbMail.Text = string.Empty;
        txtAbCC.Text = string.Empty;
        txtAbBCC.Text = string.Empty;
    }

    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
        {
            popup.Visible = false;
            Session["AuthFlag"] = 1;
            BindData();
            BindCourseExamRegConfig();
            BindAttendanceMailConfig();
            BindListviewPayment();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);         
        }
        else
            objCommon.DisplayMessage("Password does not match!", this.Page);
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            Response.Redirect("~/principalHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "5")
        {
            Response.Redirect("~/homeNonFaculty.aspx", false);
        }
        else
        {
            Response.Redirect("~/home.aspx", false);
        }
    }

    #region Tab 4  Semester Admission Payment Related

    protected void BindListviewPayment()
    {
        try
        {
            DataSet ds = objMConfig.GetPaymentDetailsofSemesterAdmissionConfig();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                
                lvPaymentDetails.DataSource = ds;
                lvPaymentDetails.DataBind();
                foreach (ListViewDataItem item in lvPaymentDetails.Items)
                {
                    
                    ImageButton lnkPrintRegReport = item.FindControl("btnPrintReceipt") as ImageButton;
                    if (lnkPrintRegReport.ToolTip == null || lnkPrintRegReport.ToolTip == "")
                    {
                        lnkPrintRegReport.Enabled = false;
                    }
                    else
                    {
                        lnkPrintRegReport.Enabled = true;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.BindListviewPayment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
            
       
    }

    protected void ClearPaymentType()
    {
        txtPaymentMode.Text   = string.Empty;
        txtAccHolderName.Text = string.Empty;
        txtBankName.Text      = string.Empty;
        txtAccountNo.Text     = string.Empty;
        txtifsccode.Text      = string.Empty;
        txtBranchName.Text    = string.Empty;
        txtBounceCharge.Text  = string.Empty;
    }

    //private void ShowDetail(int feedbackNo)
    //{
    //    SqlDataReader dr = objAC.GetSlotNo(slotNo);

    //    if (dr != null)
    //    {
    //        if (dr.Read())
    //        {
    //            // ViewState["slotno"] = feedbackNo.ToString();
    //            if (dr["ACTIVESTATUS"].ToString() == "1")
    //                ScriptManager.RegisterStartupScript(this, GetType(), "act", "$('[id*=chkActive]').prop('checked', true);", true);
    //            else
    //                ScriptManager.RegisterStartupScript(this, GetType(), "act1", "$('[id*=chkActive]').prop('checked', false);", true);

    //            //txtSlotName.Text = dr["SLOTTYPE_NAME"] == null ? string.Empty : dr["SLOTTYPE_NAME"].ToString();
    //        }
    //    }
    //    if (dr != null) dr.Close();
    //}

    protected void btnCancelMode_Click(object sender, EventArgs e)
    {
        this.ClearPaymentType();
        BindData();
        BindCourseExamRegConfig();
        BindAttendanceMailConfig();
        BindListviewPayment();
    }
    protected void btnSubmitPaymentMode_Click(object sender, EventArgs e)
    {
        try
        {

            int PaymodeNo = 0;
            string PaymentMode = string.Empty;
            string AccHolderName = string.Empty;
            string BankName = string.Empty;
            string AccountNo = string.Empty;
            string IFSCCode = string.Empty;
            string BranchName = string.Empty;
            double BounceCharges = 0;
            string FileName = string.Empty;
            int ActiveStatus = 0;


            PaymentMode = txtPaymentMode.Text.ToString();
            AccHolderName = txtAccHolderName.Text.ToString();
            BankName = txtBankName.Text.ToString();
            AccountNo = txtAccountNo.Text.ToString();
            IFSCCode = txtifsccode.Text.ToString();
            BranchName = txtBranchName.Text.ToString();

            PaymodeNo = (btnSubmitPaymentMode.Text == "Update") ? Convert.ToInt32(Session["PayNo"]) : 0;
            BounceCharges = (txtBounceCharge.Text == string.Empty) ? 0.00 : Convert.ToDouble(txtBounceCharge.Text);
            ActiveStatus = (hfdChkActiveStatus.Value == "true") ? 1 : 0;

            if (Fuslip.HasFile)
            {

                string blob_ContainerName = "";
                string blob_ConStr = "";

                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"] != null)
                    {
                        blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
                        blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updSlot, "Something went wrong, Blob Storage container related details not found.", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(this.updSlot, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }

                string contentType = contentType = Fuslip.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(Fuslip.PostedFile.FileName);
                string userno = Session["userno"].ToString();
                // string Leavtype = ddlLeaveName.SelectedItem.Text;
                string OTP = GenerateOTP();
                if (ext == ".pdf")
                {
                    HttpPostedFile file = Fuslip.PostedFile;
                    string filename = userno + "_BANK_CHALLAN_" + OTP;//_SEM_PROM_CONFIG_
                    ViewState["filename"] = filename + ext;
                    int fileSize = Fuslip.PostedFile.ContentLength;
                    int KB = fileSize / 1024;
                    if (KB <= 150)
                    {
                        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                        {
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_BANK_CHALLAN_" + OTP, Fuslip);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
                        else
                        {
                            //if (CheckDuplicateEntry() == true)
                            //{
                            //    objCommon.DisplayMessage(updHoliday, "Entry For This Date Already Done!", this.Page);
                            //    return;
                            //}
                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_BANK_CHALLAN_" + OTP, Fuslip);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 150 kb only !", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
                    return;
                }
            }

            CustomStatus cs = (CustomStatus)objMConfig.AddPaymentTypeDetailsConfiguration(PaymodeNo, PaymentMode, AccHolderName, BankName, AccountNo, IFSCCode, BranchName, BounceCharges, Convert.ToString(ViewState["filename"]), ActiveStatus);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully", this.Page);
                this.ClearPaymentType();
                btnSubmitPaymentMode.Text = "Submit";
                Session["PayNo"] = string.Empty;
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Record Updated Successfully", this.Page);
                this.ClearPaymentType();
                btnSubmitPaymentMode.Text = "Submit";
                Session["PayNo"] = string.Empty;
                lblC.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage(this, "Error Occured While Updating Payment Details!!", this.Page);
                this.ClearPaymentType();
                btnSubmitPaymentMode.Text = "Submit";
                Session["PayNo"] = string.Empty;
            }

            BindListviewPayment();
            BindData();
            BindCourseExamRegConfig();
            BindAttendanceMailConfig();
        }
        catch (Exception Ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.btnSubmitPaymentMode_Click-> " + Ex.Message + " " + Ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int  PayModeNo = int.Parse(btnEdit.CommandArgument);
        Session["PayNo"] = PayModeNo;
        //Label1.Text = string.Empty;
        //ViewState["action"] = "Edit";
        btnSubmitPaymentMode.Text = "Update";
        this.ShowDetail(PayModeNo);
        BindData();
        BindCourseExamRegConfig();
        BindAttendanceMailConfig();
    }
    private void ShowDetail(int PayModeNo)
    {
        try
        {
            DataSet ds = objMConfig.GetPaymentDetailsofSemesterAdmissionConfigforEdit(PayModeNo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtPaymentMode.Text = ds.Tables[0].Rows[0]["PAYMENTMODE"].ToString();
                txtAccHolderName.Text = ds.Tables[0].Rows[0]["ACC_HOLDER_NAME"].ToString();
                txtBankName.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtAccountNo.Text = ds.Tables[0].Rows[0]["ACCOUNT_NO"].ToString();
                txtifsccode.Text = ds.Tables[0].Rows[0]["IFSC_CODE"].ToString();
                txtBranchName.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                txtBounceCharge.Text = ds.Tables[0].Rows[0]["CHK_BOUNCE_CHARGE"].ToString();
                lblC.Text = ds.Tables[0].Rows[0]["CHALLAN_FILE_NAME"].ToString();

                lblC.Visible = (lblC.Text == "" || lblC.Text == null) ? false : true;                

                if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "1")
                    ScriptManager.RegisterStartupScript(this, GetType(), "act", "$('[id*=rdActiveStatus]').prop('checked', true);", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "act1", "$('[id*=rdActiveStatus]').prop('checked', false);", true);


                //lvPaymentDetails.DataSource = ds;
                //lvPaymentDetails.DataBind();
            }
        }
        catch (Exception Ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.ShowDetail-> " + Ex.Message + " " + Ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string GenerateOTP()
    {
        string allowedChars = "";

        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string otpString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            otpString += temp;
        }
        return otpString;
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = System.IO.Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public void DeleteIFExits(string FileName)
    {
        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSlot, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updSlot, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = System.IO.Path.GetFileNameWithoutExtension(FileName);
        try
        {
            System.Threading.Tasks.Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;

            string blob_ContainerName = "";
            string blob_ConStr = "";
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"] != null)
                {
                    blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
                    blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(this.updSlot, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSlot, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            string directoryName = "~/CHALLANDOCUMENT" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {
                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string doc = FileName;
            var Document = doc;
            string extension = Path.GetExtension(doc.ToString());
            if (doc == null || doc == "")
            {
                objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 150 kb only !", this.Page);
                return;
            }
            else
            {
                if (extension == ".pdf")
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                    var Newblob = blobContainer.GetBlockBlobReference(Document);
                    string filePath = directoryPath + "\\" + Document;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                    var Newblob = blobContainer.GetBlockBlobReference(Document);
                    string filePath = directoryPath + "\\" + Document;
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(filePath);
                    //Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_ModuleConfig.btnPrintReceipt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion
    
}