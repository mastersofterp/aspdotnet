using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_MentorMentee_Meeting_Scheduled_Send_Email_Sms : System.Web.UI.Page
{

    Common objCommon = new Common();
    User_AccController objUC = new User_AccController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
                    //CheckPageAuthorization();
                    //FillDropdown();
                    BindParentListview();
                    objCommon.FillDropDownList(ddlCommitee, "ACD_MEETING_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");

                    Page.Title = Session["coll_name"].ToString();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
         {
             if (Session["usertype"].ToString() == "3")
             {
                 
                 objCommon.FillDropDownList(ddlMeeting, "ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_COMITEE C ON (A.FK_MEETING = C.ID)", "A.FK_MEETING", "(CONVERT(nvarchar, A.MEETINGDATE, 101) + ' ' + A.MEETINGTIME + ' ' + A.MEETINGTOTIME)", "ISNULL(A.ACTIVE_STATUS, 0) = 1  AND A.FK_MEETING = " + Convert.ToInt32(ddlCommitee.SelectedValue), "");

             }
             else
             {
                     
                 objCommon.FillDropDownList(ddlMeeting, "ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_COMITEE C ON (A.FK_MEETING = C.ID)", "A.FK_MEETING", "(CONVERT(nvarchar, A.MEETINGDATE, 101) + ' ' + A.MEETINGTIME + ' ' + A.MEETINGTOTIME)", "ISNULL(A.ACTIVE_STATUS, 0) = 1  AND A.FK_MEETING = " + Convert.ToInt32(ddlCommitee.SelectedValue), "");

             }
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 this.objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SendSmstoParents.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
             else
                 this.objCommon.ShowError(Page, "Server Unavailable.");
         }
    }

    
  

    private void BindParentListview()
    {
        DataSet ds;
        ds = objCommon.FillDropDown("ACD_STUDENT", "STUDNAME,FATHERMOBILE,FATHER_EMAIL", "FATHERNAME,REGNO,IDNO", "FAC_ADVISOR =" + Session["userno"] +  "", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvParent.Visible = true;
                lvParent.DataSource = ds;
                lvParent.DataBind();
            }
            else
            {
                lvParent.Visible = false;
                lvParent.DataSource = null;
                lvParent.DataBind();
            }

    }
    protected void btnSendParentEmail_Click(object sender, EventArgs e)
    {
        User_AccController objUC = new User_AccController();
        UserAcc objUA = new UserAcc();
       
        string CodeStandard = objCommon.LookUp("Reff", "CODE_STANDARD", "");
        string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");

        //ds = objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN   ", "AGENDATITAL,VENUE", "CONTENT_DETAILS", "VENUEID", "");
        //objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_VENUE V ON(V.PK_VENUEID=A.VENUEID)", "A.PK_AGENDA, A.AGENDANO, A.AGENDATITAL", "A.MEETINGDATE, A.MEETINGTIME,A.MEETINGTOTIME,V.VENUE,A.CONTENT_DETAILS", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");

        string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
        int countparent = 0;
        int countpartcheck = 0;
        foreach (ListViewDataItem item in lvParent.Items)
        {
            System.Web.UI.WebControls.CheckBox chkID = item.FindControl("chkSelect") as System.Web.UI.WebControls.CheckBox;
            System.Web.UI.WebControls.Label lblPLogin = item.FindControl("lblPLogin") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label lblFatherEmailID = item.FindControl("lblFatherEmailID") as System.Web.UI.WebControls.Label;

            if (chkID.Checked == true)
            {
                countpartcheck++;
            }

            if (chkID.Checked == true && lblFatherEmailID.Text != "")
            {
                DataSet ds;
                ds= objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_VENUE V ON(V.PK_VENUEID=A.VENUEID)", "A.PK_AGENDA, A.AGENDANO, A.AGENDATITAL", "A.MEETINGDATE, A.MEETINGTIME,A.MEETINGTOTIME,V.VENUE,A.CONTENT_DETAILS", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");

                countparent++;
                string VENUEID = ds.Tables[0].Rows[0]["VENUE"].ToString();
                string AGENDATITAL = ds.Tables[0].Rows[0]["AGENDATITAL"].ToString();
                string CONTENT_DETAILS = ds.Tables[0].Rows[0]["CONTENT_DETAILS"].ToString();

                
                System.Web.UI.WebControls.Label lblFatherMobile = item.FindControl("lblFatherMobile") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblPstudent = item.FindControl("lblPstudent") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblPwd = item.FindControl("lblPreg") as System.Web.UI.WebControls.Label;
                string email = lblFatherEmailID.Text;
                string Subject = CodeStandard + "Committee Meeting Scheduled"; 
                string message = "Dear " + lblPstudent.Text + "<br />";                      
                message = message + "Meeting Title :" + AGENDATITAL + "<br />";
                message = message + "Meeting Purpose:" + CONTENT_DETAILS + "<br />";
                message = message + "Meeting Scheduled on :" + ddlMeeting.SelectedItem.Text + "<br />";
                message = message + "Venue :" + VENUEID + "<br />";


                string subjectText = string.Empty;
                string templateText = string.Empty;
                int TemplateTypeId =7;
                int TemplateId =6;

                DataSet ds_mstQry = objUC.GetEmailTemplateConfigData(TemplateTypeId, TemplateId, 0, "43654587575");

                if (ds_mstQry != null && ds_mstQry.Tables.Count == 3)
                {
                    //ds_mstQry.Tables[0]====> Return DATAFIELDDISPLAY,DATAFIELD
                    //ds_mstQry.Tables[1]====> Return EMAILSUBJECT,TEMPLATETEXT
                    //ds_mstQry.Tables[2]====> Return USER_REGISTRATION Data
                    if (ds_mstQry.Tables[0].Rows.Count > 0 && ds_mstQry.Tables[1].Rows.Count > 0 && ds_mstQry.Tables[2].Rows.Count > 0)
                    {
                        DataTable dt_DataField = ds_mstQry.Tables[0];
                        subjectText = ds_mstQry.Tables[1].Rows[0]["EMAILSUBJECT"].ToString();
                        templateText = ds_mstQry.Tables[1].Rows[0]["TEMPLATETEXT"].ToString();

                        for (int i = 0; i < dt_DataField.Rows.Count; i++)
                        {
                            if (templateText.Contains(dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString()))
                            {
                                string dataFieldDisp = dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString();
                                templateText = templateText.Replace("[" + dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString() + "]", ds_mstQry.Tables[2].Rows[0][dataFieldDisp].ToString());
                            }
                        }
                    }
                }
              
                //------------Code for sending email,It is optional---------------
                //added by kajal jaiswal  on 03-06-2023
                int status = 0;
                SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
                status = objSendEmail.SendEmail(email, message, Subject); //Calling Method

            }
        }
        // added by kajal jaiswal on 16-02-2023 for validating send email button 
        if (countparent != 0)
        {
            objCommon.DisplayMessage(Page, "Email send successfully to the Parent having Proper EmailId !", this.Page);
        }
        else if (lvParent.Items.Count == 0)
        {
            objCommon.DisplayMessage(Page, "No Record Found!", this.Page);
        }
        else if (lvParent.Items.Count != 0 && countpartcheck == 0)
        {
            objCommon.DisplayMessage(Page, "Please select at least one Parent!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(Page, "Email send successfully to the Parent having Proper EmailId  !", this.Page);
        }

    }

    protected void btnPCancel_Click(object sender, EventArgs e)
    {
        clear();

        foreach (ListViewItem item in lvParent.Items)
        {
            CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");

            chkSelect.Checked = false;
        }
    }
    private void clear()
    {

        ddlCommitee.SelectedIndex = 0;
        ddlMeeting.SelectedIndex = 0;    
    }
}


  