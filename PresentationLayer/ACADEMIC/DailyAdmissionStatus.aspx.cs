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
using ClosedXML.Excel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_DailyAdmissionStatus : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ModuleConfigController objMConfig = new ModuleConfigController();
    ModuleConfig objMod = new ModuleConfig();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
            else
            {
                //CheckPageAuthorization();
                if (Session["usertype"].Equals(1))
                {
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=AffiliatedFeesCategory.aspx");
                }

            }
            //this.FillDropdown();
            objCommon.FillDropDownList(ddladmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
            if(rblSelection.SelectedValue=="1")
            {
                BindListViewDaily();
            }
            BindAttendanceMailConfig();
            ViewState["action"] = "add";
        }
        divMsg.InnerHtml = string.Empty;
        
    }
    protected void btnSubmitMail_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = (CustomStatus)objMConfig.InsertDailyAdmissionEmailConfig(txtToMail.Text, txtCC.Text, txtBCC.Text);

            if (cs.Equals(CustomStatus.RecordSaved))
                objCommon.DisplayMessage(this.pnladmstatus, "Record Saved Successfully!", this.Page);
            else
                objCommon.DisplayMessage(this.pnladmstatus, "Record Updated Successfully!", this.Page);
            BindAttendanceMailConfig();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindAttendanceMailConfig()
    {
        DataSet dsAtt = objMConfig.GetAdmissionStatusEMailConfig();
        if (dsAtt.Tables != null)
        {
            if (dsAtt.Tables[0].Rows.Count > 0)
            {
                txtToMail.Text = dsAtt.Tables[0].Rows[0]["DAILY_TO_MAIL"].ToString();
                txtCC.Text = dsAtt.Tables[0].Rows[0]["DAILY_CC_MAIL"].ToString();
                txtBCC.Text = dsAtt.Tables[0].Rows[0]["DAILY_BCC_MAIL"].ToString();
               
            }
        }
    }


    private void ClearControls()
    {
        txtToMail.Text = string.Empty;
        txtCC.Text = string.Empty;
        txtBCC.Text = string.Empty;
       
    }
    protected void bntCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        Response.Redirect(Request.Url.ToString());
    }


    protected void BindListView()
    {
        try
        {

            DataSet ds = objMConfig.GetDailyAdmissionStatussendemailConfig(Convert.ToInt32(ddladmbatch.SelectedValue));


            if (ds.Tables[0].Rows.Count > 0)
            {

                lvsendemail.DataSource = ds;
                lvsendemail.DataBind();
            }

            else
            {
                objCommon.DisplayMessage(this.Updsendmail, "No Record found", this.Page);
                lvsendemail.DataSource = null;
                lvsendemail.DataBind();
                //lvsendemail.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_DailyAdmissionStatus.BindListViewAAMaster ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    protected void BindListViewDaily()
    {
        try
        {
            DataSet ds = objMConfig.GetAdmissionStatussendemailConfigDaily();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvsendemail.DataSource = ds;
                lvsendemail.DataBind();
            }

            else
            {
                //objCommon.DisplayMessage(this.Updsendmail, "No Record found", this.Page);
                //lvsendemail.DataSource = null;
                //lvsendemail.DataBind();
                ////lvsendemail.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_DailyAdmissionStatus.BindListViewDaily ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnsendemail_Click(object sender, EventArgs e)
    {
        string Emailids = "";
        string CCEmailids = "";
        DataSet dsconfig = null;
        //string college = "";
        //string degree = "";
        //string branch = "";
        string CODE_STANDARD = objCommon.LookUp("REFF","CODE_STANDARD", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''");
        string admbatch = ddladmbatch.SelectedItem.Text;
        dsconfig = objCommon.FillDropDown("ACD_ADMISSION_STATUS_EMAIL_CONFIG", "DAILY_TO_MAIL", "DAILY_CC_MAIL,DAILY_BCC_MAIL,CONVERT(NVARCHAR(50),GETDATE(),103)as DATE", "", "");
        try
        {
            bool flag = false; int status = 0;
               DataSet ds = objMConfig.GetDailyAdmissionStatussendemailConfig(Convert.ToInt32(ddladmbatch.SelectedValue));

               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   DataTable dt = (DataTable)ds.Tables[0];
                   DataRow[] dr = dt.Select();
                   //college = ds.Tables[0].Rows[0]["COLLEGE"].ToString();
                   dt = (DataTable)ViewState["DYNAMIC_DATASET"];
                 
                     Emailids = dsconfig.Tables[0].Rows[0]["DAILY_TO_MAIL"].ToString();
                     CCEmailids = dsconfig.Tables[0].Rows[0]["DAILY_CC_MAIL"].ToString();
                     string MyHtmlString = string.Empty;

                   MyHtmlString = "Dear sir, Please find the below admission status on date-" + dsconfig.Tables[0].Rows[0]["DATE"].ToString()  +  " for "  + admbatch + " batch.<br/><br/>";           
                   MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid black;margin:0 auto;'><thead><tr><th scope='col'>College</th><th scope='col'>Degree</th>" +
                  "<th scope='col'>Branch</th>" + "<th scope='col'>Student Count</th></tr></thead><tbody>";

                   for (int i = 0; i < dr.Length; i++)
                   {
                       MyHtmlString += "<tr><td style='text-align:center;'> " + dr[i][0].ToString() + "</td>";
                       MyHtmlString += "<td style='text-align:center;'> " + dr[i][1].ToString() + "</td>";
                       MyHtmlString += "<td style='text-align:center;'> " + dr[i][2].ToString() + "</td>";
                       MyHtmlString += "<td style='text-align:center;'> " + dr[i][3].ToString() + "</td></tr>";
                     
                   }
                   MyHtmlString += "</tbody></table><br/>Regards,<br/>" + CODE_STANDARD; 
                   string Subject = "Daily Admission Status";
                   status = objSendEmail.SendEmail(Emailids, MyHtmlString, Subject, CCEmailids, "");

               }
                
                if (status == 1)
                {
                    objCommon.DisplayMessage(Updsendmail, "Email Send Successfully !!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(Updsendmail, "Unable To Send Email !!!", this.Page);
                }
            //}
        }
        catch (Exception ex)
        {
            Emailids = "";
        }
    }


   
    protected void btnsendcancel_Click(object sender, EventArgs e)
    {
        ddladmbatch.SelectedIndex = 0;
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddladmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelection.SelectedValue == "2")
        {
            lvsendemail.DataSource = null;
            lvsendemail.DataBind();

            if (ddladmbatch.SelectedIndex == 0)
            {
                lvsendemail.DataSource = null;
                lvsendemail.DataBind();
            }
            else
            {
                BindListView();
            }
            
        }
       
    }
    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
       try
       {
           if (rblSelection.SelectedValue == "1")
           {
               lvsendemail.DataSource = null;
               lvsendemail.DataBind();
               DivadmBatch.Visible = false;
               btnSendmailDailyStatus.Visible = true;
               btnsendemail.Visible = false;
               ddladmbatch.SelectedIndex = 0;
               BindListViewDaily();
           }
           else if (rblSelection.SelectedValue == "2")
           {
               lvsendemail.DataSource = null;
               lvsendemail.DataBind();
               DivadmBatch.Visible = true;
               btnSendmailDailyStatus.Visible = false;
               btnsendemail.Visible = true;
           }

       }
       catch (Exception ex)
       {
           if (Convert.ToBoolean(Session["error"]) == true)
               objUCommon.ShowError(Page, "ACADEMIC_DailyAdmissionStatus.rblSelection_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
           else
               objUCommon.ShowError(Page, "Server UnAvailable");
       }
    }
    protected void btnSendmailDailyStatus_Click(object sender, EventArgs e)
    {
        string Emailids = "";
        string CCEmailids = "";
        DataSet dsconfig = null;
        //string college = "";
        //string degree = "";
        //string branch = "";
        string CODE_STANDARD = objCommon.LookUp("REFF", "CODE_STANDARD", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''");
        string admbatch = ddladmbatch.SelectedItem.Text;
        dsconfig = objCommon.FillDropDown("ACD_ADMISSION_STATUS_EMAIL_CONFIG", "DAILY_TO_MAIL", "DAILY_CC_MAIL,DAILY_BCC_MAIL,CONVERT(NVARCHAR(50),GETDATE(),103)as DATE", "", "");
        try
        {
            bool flag = false; int status = 0;
            DataSet ds = objMConfig.GetDailyAdmissionStatussendemailConfig(Convert.ToInt32(ddladmbatch.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = (DataTable)ds.Tables[0];
                DataRow[] dr = dt.Select();
                //college = ds.Tables[0].Rows[0]["COLLEGE"].ToString();
                dt = (DataTable)ViewState["DYNAMIC_DATASET"];

                Emailids = dsconfig.Tables[0].Rows[0]["DAILY_TO_MAIL"].ToString();
                CCEmailids = dsconfig.Tables[0].Rows[0]["DAILY_CC_MAIL"].ToString();
                string MyHtmlString = string.Empty;

                MyHtmlString = "Dear sir, Please find the below admission status on date-" + dsconfig.Tables[0].Rows[0]["DATE"].ToString() + ".<br/><br/>";
                MyHtmlString += "<table width='1000px' cellspacing='0' style='border: 1px solid black;margin:0 auto;'><thead><tr><th scope='col'>College</th><th scope='col'>Degree</th>" +
               "<th scope='col'>Branch</th>" + "<th scope='col'>Student Count</th></tr></thead><tbody>";

                for (int i = 0; i < dr.Length; i++)
                {
                    MyHtmlString += "<tr><td style='text-align:center;'> " + dr[i][0].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][1].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][2].ToString() + "</td>";
                    MyHtmlString += "<td style='text-align:center;'> " + dr[i][3].ToString() + "</td></tr>";

                }
                MyHtmlString += "</tbody></table><br/>Regards,<br/>" + CODE_STANDARD;
                string Subject = "Daily Admission Status";
                status = objSendEmail.SendEmail(Emailids, MyHtmlString, Subject, CCEmailids, "");

            }

            if (status == 1)
            {
                objCommon.DisplayMessage(Updsendmail, "Email Send Successfully !!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(Updsendmail, "Unable To Send Email !!!", this.Page);
            }
            //}
        }
        catch (Exception ex)
        {
            Emailids = "";
        }
    }
}