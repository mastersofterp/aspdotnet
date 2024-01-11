using BusinessLogicLayer.BusinessLogic.PostAdmission;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using EASendMail;

public partial class ACADEMIC_POSTADMISSION_ApplicationProcessNew : System.Web.UI.Page
{
    Common objCommon = new Common();
    ApplicationProcess objAP = new ApplicationProcess();
    ApplicationProcessController objAC = new ApplicationProcessController();
    ADMPExamHallTicketController objexam = new ADMPExamHallTicketController();
    User_AccController objUC = new User_AccController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameADMP"].ToString();

    string degreenos = "0"; string branchnos = "0"; string userno = string.Empty; string chkstatus = string.Empty;
    int count = 0; int Checked_count = 0; int emptyEmail_count = 0; int emailSend_count = 0; int retstatus = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["UserNo"] = null;
            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                FillDropDownList();
            }
            string filepath = Server.MapPath("~//XML//ApplicationProcessNew.xml");
            ManagePageControlsModule.ManagePageControls(Page, filepath);  
     
        }

        if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
        {
            if (chkCommunication.Checked == true)
            {
                btnSubmit.Style["display"] = "none";
            }
            else
            {
                btnSubmit.Style["display"] = "inline";
            }
        }
        else
        {
            btnSubmit.Style["display"] = "none";
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void FillDropDownList()
    {

        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS=1", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlTemplateType, "ACD_ADMP_EMAILTEMPLATETYPE", "TEMPTYPEID", "TEMPLATETYPE", "TEMPTYPEID > 0 AND ACTIVE_STATUS=1 AND MODULETYPE='ADMP'", "TEMPTYPEID");
        objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "DISTINCT UA_NO", "UA_FULLNAME", "UA_TYPE<>2", "UA_NO");
    }

    private void ShowSubmit()
    {
        if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
        {
            btnSubmit.Style["display"] = "inline";
        }
        else
        {
            btnSubmit.Style["display"] = "none";
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlScheduleSlot.SelectedIndex = 0;
        ddlApplicantType.SelectedIndex = 0;
        ddlDegreeProgram.SelectedIndex = 0;
        ddlCurrentAppStage.SelectedIndex = 0;
        ddlToAppStage.SelectedIndex = 0;
        divCurrentAppStage.Style["display"] = "none";
        divToAppStage.Style["display"] = "none";
        divApplicationProcess.Style["display"] = "none";
        divEmailSMS.Style["display"] = "none";
        divTemplateType.Style["display"] = "none";
        divTemplateName.Style["display"] = "none";
        divCommunication.Style["display"] = "none";
        divFaculty.Style["display"] = "none";
        ddlFaculty.SelectedIndex = 0;
        chkCommunication.Checked = false;
        ShowSubmit();
    }
    protected void ddlApplicantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegreeProgram.Items.Clear();
            ddlDegreeProgram.Items.Insert(0, new ListItem("Please Select", "0"));
            lvApplicationProcess.Items.Clear();
            divApplicationProcess.Style["display"] = "none";
            divCurrentAppStage.Style["display"] = "none";
            divToAppStage.Style["display"] = "none";
            divScheduleSlot.Style["display"] = "none";
            divEmailSMS.Style["display"] = "none";
            divTemplateType.Style["display"] = "none";
            divTemplateName.Style["display"] = "none";
            divCommunication.Style["display"] = "none";
            divFaculty.Style["display"] = "none";
            ddlFaculty.SelectedIndex = 0;
            chkCommunication.Checked = false;
            ShowSubmit();
            if (ddlApplicantType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegreeProgram, "VW_ACD_COLLEGE_DEGREE_BRANCH", "(Convert(varchar,DEGREENO) + ',' + Convert(varchar,BRANCHNO))PROGRAM_ID", "(DEGREE_SHORT_CODE +'-'+ LONGNAME)PROGRAM", "DEGREETYPEID=" + Convert.ToInt32(ddlApplicantType.SelectedValue) + "", "");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.ddlApplicantType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegreeProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int pagesize = Convert.ToInt32(DataPager1.PageSize);
            NumberDropDown.SelectedValue = pagesize.ToString();
            DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
            if (ddlAdmBatch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Admission Batch", Page);
                return;
            }
            if (ddlApplicantType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select  Applicant Type", Page);
                return;
            }
            ddlCurrentAppStage.Items.Clear();
            ddlCurrentAppStage.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlToAppStage.Items.Clear();
            ddlToAppStage.Items.Insert(0, new ListItem("Please Select", "0"));
            lvApplicationProcess.Items.Clear();
            divApplicationProcess.Style["display"] = "none";
            divCurrentAppStage.Style["display"] = "none";
            divToAppStage.Style["display"] = "none";
            divCommunication.Style["display"] = "none";
            divEmailSMS.Style["display"] = "none";
            divTemplateType.Style["display"] = "none";
            divTemplateName.Style["display"] = "none";
            divFaculty.Style["display"] = "none";
            ddlFaculty.SelectedIndex = 0;
            chkCommunication.Checked = false;
            ShowSubmit();
            if (ddlDegreeProgram.SelectedIndex > 0)
            {
                BindListView(1);
                SelectedDegreeProgram();
                if (Convert.ToInt32(Session["usertype"].ToString()) != 1)
                {
                    objCommon.FillDropDownList(ddlCurrentAppStage, " ACD_APP_PROCESS_FACULTY_DETAILS FD INNER JOIN ACD_ADM_APPLICATIONSTAGES AA ON (FD.FACULTY_VERIFY_STAGENO=AA.STAGEID)", "DISTINCT FD.FACULTY_VERIFY_STAGENO", "STAGENAME", "FD.BATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND FD.DEGREENO=" + Convert.ToInt32(degreenos) + "AND FD.BRANCHNO=" + Convert.ToInt32(branchnos) + "AND FACULTY_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + "", "FD.FACULTY_VERIFY_STAGENO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlCurrentAppStage, "ACD_ADMP_STAGE_DEGREE_MAPPING DM INNER JOIN ACD_ADM_APPLICATIONSTAGES AA ON (DM.STAGEID=AA.STAGEID)", "DISTINCT DM.STAGEID", "STAGENAME", " BATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(degreenos) + "", "DM.STAGEID");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.ddlDegreeProgram_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCurrentAppStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int pagesize = Convert.ToInt32(DataPager1.PageSize);
            NumberDropDown.SelectedValue = pagesize.ToString();
            DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
            ddlToAppStage.Items.Clear();
            ddlToAppStage.Items.Insert(0, new ListItem("Please Select", "0"));
            divToAppStage.Style["display"] = "none";
            divScheduleSlot.Style["display"] = "none";
            lvApplicationProcess.Items.Clear();
            chkCommunication.Checked = false;
            divEmailSMS.Style["display"] = "none";
            divTemplateType.Style["display"] = "none";
            divTemplateName.Style["display"] = "none";
            divFaculty.Style["display"] = "none";
            ddlFaculty.SelectedIndex = 0;
            BindListView(2);
            ShowSubmit();
            if (ddlCurrentAppStage.SelectedIndex > 0)
            {
                SelectedDegreeProgram();
                objCommon.FillDropDownList(ddlToAppStage, "ACD_ADMP_STAGES_DEPENDANCIES SD INNER JOIN ACD_ADM_APPLICATIONSTAGES AA ON (SD.NEXTSTAGEID=AA.STAGEID)", "DISTINCT SD.NEXTSTAGEID", "STAGENAME", "CURRENTSTAGEID=" + Convert.ToInt32(ddlCurrentAppStage.SelectedValue) + "AND DEGREENO=" + degreenos + "AND BATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "", "SD.NEXTSTAGEID");
            }
            ddlScheduleSlot.Visible = false;
            ddlFaculty.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.ddlCurrentAppStage_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlToAppStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        divEmailSMS.Style["display"] = "none";
        divTemplateType.Style["display"] = "none";
        divTemplateName.Style["display"] = "none";
        divScheduleSlot.Style["display"] = "none";
        divFaculty.Style["display"] = "none";
        ddlFaculty.SelectedIndex = 0;
        chkCommunication.Checked = false;
        ShowSubmit();
        chkstatus = objCommon.LookUp("ACD_ADM_ApplicationStages", "ISALLOWPROCESS", "STAGEID=" + Convert.ToInt32(ddlToAppStage.SelectedValue) + "");

        if (chkstatus.ToString().Trim() == "S")
        {
            BindScheduleSlot();
            ddlScheduleSlot.Visible = true;
        }
        else
        {
            ddlScheduleSlot.SelectedIndex = 0;
            divScheduleSlot.Style["display"] = "none";
            ddlScheduleSlot.Visible = false;
        }
        if (chkstatus.ToString().Trim() == "V")
        {
            divFaculty.Style["display"] = "inline";
            ddlFaculty.SelectedIndex = 0;
            ddlFaculty.Visible = true;
        }
        else
        {
            divFaculty.Style["display"] = "none";
            ddlFaculty.Visible = false;
        }
    }

    private void SelectedDegreeProgram()
    {
        string[] DegreeBranch = ddlDegreeProgram.SelectedValue.Split(',');
        if (ddlApplicantType.SelectedIndex > 0)
        {
            if (!string.IsNullOrEmpty(degreenos))
                degreenos = DegreeBranch[0];
            else
                degreenos = DegreeBranch[0];

            if (!string.IsNullOrEmpty(branchnos))
                branchnos = DegreeBranch[1];
            else
                branchnos = DegreeBranch[1];
        }

        degreenos = degreenos.Trim();
        branchnos = branchnos.Trim();
    }
    private void BindListView(int ctrlid)
    {
        try
        {
            objAP.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objAP.UGPGOT = Convert.ToInt32(ddlApplicantType.SelectedValue);
            SelectedDegreeProgram();
            objAP.DegreeNo = Convert.ToInt32(degreenos);
            objAP.BranchNo = Convert.ToInt32(branchnos);
            if (ddlCurrentAppStage.SelectedIndex > 0)
            {
                objAP.ApplicationStage = Convert.ToInt32(ddlCurrentAppStage.SelectedValue);
                chkstatus = objCommon.LookUp("ACD_ADM_ApplicationStages", "ISALLOWPROCESS", "STAGEID=" + Convert.ToInt32(ddlCurrentAppStage.SelectedValue) + "");
            }
            else
            {
                objAP.ApplicationStage = 0;
                chkstatus = "";
            }
            objAP.UaType = Convert.ToInt32(Session["usertype"].ToString());

            objAP.ScheduleNo = Convert.ToInt32(ddlScheduleSlot.SelectedValue);
            if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
            {
                objAP.FacultyUaNo = Convert.ToInt32(Session["userno"].ToString());
            }
            else
            {
                objAP.UaNo = Convert.ToInt32(Session["userno"].ToString());
            }
            string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServerADMP"].ToString();
            string urlLink = loginurl + "/Default.aspx";
            objAP.LoginUrl = urlLink.ToString();
            DataSet ds = objAC.GetApplicationProcess(objAP);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lvApplicationProcess.DataSource = ds.Tables[0];
                lvApplicationProcess.DataBind();
                lvApplicationProcess.Style["display"] = "inline";
                divApplicationProcess.Style["display"] = "inline";

                if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
                {
                    Control hdrchkAll = lvApplicationProcess.FindControl("thchkAll");
                    hdrchkAll.Visible = false;
                    foreach (ListViewItem item in lvApplicationProcess.Items)
                    {
                        item.FindControl("tdchkstud").Visible = false;
                    }
                }
                else
                {
                    Control hdrchkAll = lvApplicationProcess.FindControl("thchkAll");
                    hdrchkAll.Visible = true;
                    foreach (ListViewItem item in lvApplicationProcess.Items)
                    {
                        item.FindControl("tdchkstud").Visible = true;
                    }
                }
                if (chkstatus.ToString().Trim() == "P")
                {
                    foreach (ListViewItem item in lvApplicationProcess.Items)
                    {
                        item.FindControl("tdlinkFullName").Visible = true;
                        item.FindControl("tdFullName").Visible = false;
                    }
                }
                else
                {
                    foreach (ListViewItem item in lvApplicationProcess.Items)
                    {
                        item.FindControl("tdlinkFullName").Visible = false;
                        item.FindControl("tdFullName").Visible = true;
                    }
                }
                if (chkstatus.ToString().Trim() == "V")
                {
                    Control ctrHeader = lvApplicationProcess.FindControl("thVerify");
                    ctrHeader.Visible = true;
                    foreach (ListViewItem item in lvApplicationProcess.Items)
                    {
                        item.FindControl("tdVerify").Visible = true;

                    }
                }
                else
                {
                    Control ctrHeader = lvApplicationProcess.FindControl("thVerify");
                    ctrHeader.Visible = false;
                    foreach (ListViewItem item in lvApplicationProcess.Items)
                    {
                        item.FindControl("tdVerify").Visible = false;
                    }
                }
                if (ctrlid == 1)
                {
                    divCurrentAppStage.Style["display"] = "inline";
                    ddlCurrentAppStage.Visible = true;
                    if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
                    {
                        divCommunication.Style["display"] = "inline";
                        ddlToAppStage.Visible = false;
                        ddlScheduleSlot.Visible = false;
                        ddlFaculty.Visible = false;
                    }
                }
                if (ctrlid == 2)
                {
                    if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
                    {
                        divToAppStage.Style["display"] = "inline";
                        divCommunication.Style["display"] = "inline";
                        ddlToAppStage.Visible = true;
                    }
                }
            }
            else
            {
                lvApplicationProcess.Items.Clear();
                lvApplicationProcess.DataSource = null;
                lvApplicationProcess.DataBind();
                lvApplicationProcess.Style["display"] = "none";
                divApplicationProcess.Style["display"] = "none";
                if (ctrlid == 1)
                {
                    divCurrentAppStage.Style["display"] = "none";
                    divCommunication.Style["display"] = "none";
                    ddlCurrentAppStage.Visible = false;
                    ddlToAppStage.Visible = false;
                    ddlScheduleSlot.Visible = false;
                    ddlFaculty.Visible = false;
                    lvApplicationProcess.Items.Clear();
                }
                if (ctrlid == 2)
                {
                    divToAppStage.Style["display"] = "none";
                    divCommunication.Style["display"] = "none";
                    ddlToAppStage.Visible = false;
                }
                objCommon.DisplayMessage(this.Page, "Record Not Found", Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindScheduleSlot()
    {
        try
        {
            int ADMBATCH = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            DataSet ds = objexam.GetSChedule(0, "0", ADMBATCH);
            ddlScheduleSlot.Items.Clear();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlScheduleSlot.DataSource = ds;
                ddlScheduleSlot.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlScheduleSlot.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlScheduleSlot.DataBind();
                ddlScheduleSlot.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlScheduleSlot.SelectedIndex = 0;
                divScheduleSlot.Style["display"] = "inline";
                ShowSubmit();
            }
            else
            {
                ddlScheduleSlot.Items.Insert(0, new ListItem("Please Select", "0"));
                divScheduleSlot.Style["display"] = "none";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.BindScheduleSlot-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string scheduledno = string.Empty; int schedulecount = 0;
            chkstatus = objCommon.LookUp("ACD_ADM_ApplicationStages", "ISALLOWPROCESS", "STAGEID=" + Convert.ToInt32(ddlToAppStage.SelectedValue) + "");

            if (ddlCurrentAppStage.SelectedIndex == 0 && ddlCurrentAppStage.Visible == true)
            {

                objCommon.DisplayMessage(this.Page, "Please Select Current Application Stage", Page);
                return;
            }

            if (ddlToAppStage.SelectedIndex == 0 && ddlToAppStage.Visible == true)
            {
                objCommon.DisplayMessage(this.Page, "Please Select To Application Stage", Page);
                return;
            }
            if (ddlScheduleSlot.SelectedIndex == 0 && ddlScheduleSlot.Visible == true)
            {

                objCommon.DisplayMessage(this.Page, "Please Select Scheduled Slot", Page);
                return;
            }
            if (ddlFaculty.SelectedIndex == 0 && ddlFaculty.Visible == true)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Faculty", Page);
                return;
            }
            foreach (ListViewDataItem lvProcess in lvApplicationProcess.Items)
            {
                HiddenField hduserno = lvProcess.FindControl("hdnuserno") as HiddenField;
                CheckBox chStud = lvProcess.FindControl("chkStud") as CheckBox;

                if (chStud.Checked == true)
                {
                    count++;
                    userno += hduserno.Value.ToString() + ",";
                }
            }
            //if (lvApplicationProcess.Items.Count == 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Record Not Found", Page);
            //    return;
            //}
            if (count == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please select at least one student", Page);
                return;
            }
            objAP.UserNo = userno.ToString();
            SelectedDegreeProgram();
            objAP.DegreeNo = Convert.ToInt32(degreenos);
            objAP.BranchNo = Convert.ToInt32(branchnos);
            objAP.ApplicationStage = Convert.ToInt32(ddlToAppStage.SelectedValue);
            objAP.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            if (chkstatus.ToString().Trim() == "S") //Scheduled
            {
                objAP.ExamSchedule = ddlScheduleSlot.SelectedItem.Text;
                objAP.ScheduleNo = Convert.ToInt32(ddlScheduleSlot.SelectedValue);
            }
            objAP.UaNo = Convert.ToInt32(Session["userno"].ToString());
            if (ddlFaculty.SelectedIndex > 0)
            {
                objAP.FacultyUaNo = Convert.ToInt32(ddlFaculty.SelectedValue);
            }
            else
            {
                objAP.FacultyUaNo = 0;
            }

            int status = objAC.InsAppProcessExamSchedule(objAP);
            if (status == 1)
            {
                objCommon.DisplayMessage(this.Page, "Scheduled done successfully", Page);
                clearControls();
                DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
                BindListView(0);
            }
            else if (status == 2)
            {
                objCommon.DisplayMessage(this.Page, "Student assign to faculty successfully", Page);
                clearControls();
                DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
                BindListView(0);
            }
            else if (status == 3)
            {
                objCommon.DisplayMessage(this.Page, "Student save successfully", Page);
                clearControls();
                DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
                BindListView(0);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error", Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void clearControls()
    {
        ddlCurrentAppStage.SelectedIndex = 0;
        ddlToAppStage.Items.Clear();
        ddlToAppStage.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlScheduleSlot.Items.Clear();
        ddlScheduleSlot.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlFaculty.SelectedIndex = 0;
        divToAppStage.Style["display"] = "none";
        divScheduleSlot.Style["display"] = "none";
        divFaculty.Style["display"] = "none";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlApplicantType.SelectedIndex = 0;
        ddlDegreeProgram.Items.Clear();
        ddlDegreeProgram.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlCurrentAppStage.Items.Clear();
        ddlCurrentAppStage.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlToAppStage.Items.Clear();
        ddlToAppStage.Items.Insert(0, new ListItem("Please Select", "0"));
        lvApplicationProcess.Items.Clear();
        //  lvApplicationProcess.DataSource = null;
        //lvApplicationProcess.DataBind();
        lvApplicationProcess.Style["display"] = "none";
        divApplicationProcess.Style["display"] = "none";
        divCurrentAppStage.Style["display"] = "none";
        divToAppStage.Style["display"] = "none";
        ddlScheduleSlot.Items.Clear();
        ddlScheduleSlot.Items.Insert(0, new ListItem("Please Select", "0"));
        divScheduleSlot.Style["display"] = "none";
        //   divEmailTemplate.Style["display"] = "none";
        divEmailSMS.Style["display"] = "none";
        divTemplateType.Style["display"] = "none";
        divTemplateName.Style["display"] = "none";
        divCommunication.Style["display"] = "none";
        chkCommunication.Checked = false;
        divFaculty.Style["display"] = "none";
        ddlFaculty.SelectedIndex = 0;
    }
    #region Email
    protected void ddlTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTemplateName.Items.Clear();
        ddlTemplateName.Items.Insert(0, new ListItem("Please Select", "0"));
        if (ddlTemplateType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlTemplateName, "ACD_ADMP_EMAILTEMPLATE", "DISTINCT TEMPLATEID", "TEMPLATENAME", "TEMPTYPEID=" + ddlTemplateType.SelectedValue + "AND ACTIVESTATUS=1", "TEMPLATEID");
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string email_type = string.Empty;
            string subjectText = string.Empty;
            string templateText = string.Empty;
            string ApplicationId = string.Empty;
            string whatsappTemplate = string.Empty;
            int emptyWhatsapp_count = 0; int whatsappSend_count = 0;
            int emptysmscount = 0, smscount = 0;
            if (chkEmail.Checked == false && chkWhatsApp.Checked == false )  //&& chkSMS.Checked == false
            {
                objCommon.DisplayMessage(this.updApplicationProcess, "Please Select Communication Type", this.Page);
                return;
            }
            if (ddlTemplateType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updApplicationProcess, "Please Select Template Type", this.Page);
                return;
            }
            if (ddlTemplateName.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updApplicationProcess, "Please Select Template Name", this.Page);
                return;
            }
            whatsappTemplate = objCommon.LookUp("ACD_ADMP_EMAILTEMPLATE", "WhatsAppTemplateName", "TEMPLATEID=" + Convert.ToInt32(ddlTemplateName.SelectedValue) + "");
            foreach (ListViewDataItem lvItem in lvApplicationProcess.Items)
            {
                CheckBox ckStud = lvItem.FindControl("chkStud") as CheckBox;
                Label lbEmail = lvItem.FindControl("lblEmail") as Label;
                Label lbStudName = lvItem.FindControl("lblFullName") as Label;
                Label lbEmailSend = lvItem.FindControl("lblEmailSend") as Label;
                HiddenField hduserno = lvItem.FindControl("hdnuserno") as HiddenField;
                Label lbApplicationId = lvItem.FindControl("lblApplicationId") as Label;
                Label lbMobileNo = lvItem.FindControl("lblMobileNo") as Label;

                if (ckStud.Checked == true)// && lbEmailSend.Text != "YES")
                {
                    Checked_count++;

                 

                    if (chkEmail.Checked == true)
                    {
                        ApplicationId = lbApplicationId.Text;
                        objAP.UserNo = hduserno.Value;
                        objAP.UaNo = Convert.ToInt32(Session["userno"].ToString());
                        objAP.ProcessType = 'E';
                        objAP.Description = lbStudName.Text + ' ' + '|' + ' ' + lbMobileNo.Text + ' ' + '|' + ' ' + lbEmail.Text;
                        objAP.ApplicationStage = Convert.ToInt32(ddlCurrentAppStage.SelectedValue);
                        objAP.TemplateId = Convert.ToInt32(ddlTemplateName.SelectedValue);

                        int TemplateTypeId = Convert.ToInt32(ddlTemplateType.SelectedValue);
                        int TemplateId = Convert.ToInt32(ddlTemplateName.SelectedValue);

                        DataSet ds_mstQry = objUC.GetEmailTemplateConfigData(TemplateTypeId, TemplateId, Convert.ToInt32(hduserno.Value), ApplicationId);

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

                        string StudentEmail = lbEmail.Text;
                        if (StudentEmail != string.Empty)
                        {
                            //Task<int> ret = Execute(templateText, StudentEmail, subjectText);
                            DataSet ds1 = getModuleConfig();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                email_type = ds1.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                            }

                            if (email_type == "1" && email_type != "")
                            {
                                TransferToEmail(StudentEmail, templateText, subjectText);
                            }
                            else if (email_type == "2" && email_type != "")
                            {
                                Task<int> ret = Execute(templateText, StudentEmail, subjectText);
                            }
                            if (email_type == "3" && email_type != "")
                            {
                                OutLook_Email(templateText, StudentEmail, subjectText);
                            }
                            objAP.ESubject = subjectText.ToString();
                            objAP.EWBody = templateText.ToString();
                            retstatus = objAC.InsUpdEmailSmsSendLog(objAP);
                            emailSend_count++;
                        }
                        else
                        {
                            emptyEmail_count++;
                        }
                    }
                    if (chkWhatsApp.Checked == true)
                    {
                        ApplicationId = lbApplicationId.Text;
                        objAP.UserNo = hduserno.Value;
                        objAP.UaNo = Convert.ToInt32(Session["userno"].ToString());
                        objAP.ProcessType = 'W';
                        objAP.Description = lbStudName.Text + ' ' + '|' + ' ' + lbMobileNo.Text + ' ' + '|' + ' ' + lbEmail.Text;
                        objAP.ApplicationStage = Convert.ToInt32(ddlTemplateType.SelectedValue);
                        retstatus = objAC.InsUpdEmailSmsSendLog(objAP);
                        if (retstatus == 1)
                        {
                            if (lbMobileNo.Text != string.Empty)
                            {
                                string otpnumber = CommonComponent.GenerateRandomPassword.GenearteOTP(4);
                                WhatsappOtp(otpnumber, lbMobileNo.Text, lbStudName.Text, whatsappTemplate);
                                whatsappSend_count++;
                            }
                            else
                            {
                                emptyWhatsapp_count++;
                            }
                        }
                    }
                    if (chkSMS.Checked == true)
                    {
                        objAP.ProcessType = 'S';  
                        if (lbMobileNo.Text != string.Empty)
                        {
                            retstatus = objAC.InsUpdEmailSmsSendLog(objAP);
                            if (retstatus == 1)
                            {
                                SendSms("0", "Mobile No. Verification Successful", lbStudName.Text, lbMobileNo.Text);
                                smscount++;
                            }
                        }
                        else
                        {
                            emptysmscount++;
                        }
                    }
                }
            }
            if (Checked_count == 0)
            {
                objCommon.DisplayMessage(updApplicationProcess, "Please Select at least one student to send email/sms", this.Page);
                return;
            }
            else if (emptyEmail_count == 0 && emailSend_count > 0 && emptyWhatsapp_count == 0 && whatsappSend_count > 0 && retstatus == 1)
            {
                objCommon.DisplayMessage(updApplicationProcess, "Email/Whats App message sent successfully.", this.Page);
                return;
            }
            else if (emptyEmail_count == 0 && emailSend_count > 0 && emptysmscount == 0 && smscount > 0 && retstatus == 1)
            {
                objCommon.DisplayMessage(updApplicationProcess, "Email/SMS message sent successfully.", this.Page);
                return;
            }
            else if (emptyEmail_count == 0 && emailSend_count > 0 && retstatus == 1)
            {
                objCommon.DisplayMessage(updApplicationProcess, "Email sent successfully.", this.Page);
                return;
            }
            else if (emptyWhatsapp_count == 0 && whatsappSend_count > 0 && retstatus == 1)
            {
                objCommon.DisplayMessage(updApplicationProcess, "Whats App message sent successfully.", this.Page);
                return;
            }
            else if (emptysmscount == 0 && smscount > 0 && retstatus == 1)
            {
                objCommon.DisplayMessage(updApplicationProcess, "SMS sent successfully.", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(updApplicationProcess, "Error..", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Application_process.btnSendEmail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }
    public int TransferToEmail(string useremail, string message, string subject)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                // fromPassword = Common.DecryptPassword(fromPassword);
                msg.From = new System.Net.Mail.MailAddress(fromAddress, "DAIICT");
                msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                msg.Subject = subject;

                msg.Body = message;
                msg.IsBodyHtml = true;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smtp.Send(msg);
                if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                {
                    return ret = 1;
                    //Storing the details of sent email
                }
                else
                {
                    return ret = 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;

    }
    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
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
            ret = 0;
        }
        return ret;
    }
    private int OutLook_Email(string Message, string toEmailId, string sub)
    {

        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oMail.To = toEmailId;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            Console.WriteLine("start to send email over TLS...");
            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
            Console.WriteLine("email sent successfully!");
            ret = 1;
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");
            Console.WriteLine(ep.Message);
            ret = 0;
        }
        return ret;
    }
    #endregion
    #region Verify Document List
    private void BindVerfyDocumentList()
    {

        objAP.Userno = Convert.ToInt32(ViewState["userno"].ToString());
        DataSet ds = objAC.GetVerifyDocumentList(objAP);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblCandidateName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
            txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
            lvVerifyDocumentList.DataSource = ds.Tables[0];
            lvVerifyDocumentList.DataBind();
        }
        else
        {
            lblCandidateName.Text = string.Empty;
            txtRemark.Text = string.Empty;
            lvVerifyDocumentList.DataSource = null;
            lvVerifyDocumentList.DataBind();
            lvVerifyDocumentList.Items.Clear();
        }
    }



    protected void btnVerify_Click(object sender, EventArgs e)
    {
        //string filepath = Server.MapPath("~//XML//ApplicationProcessNewVerify.xml");
       // ManagePageControlsModule.ManagePageControls(Page, filepath);

        txtRemark.Text = string.Empty;
        Button btnVerify = sender as Button;
        int userno = Convert.ToInt32(btnVerify.CommandArgument);
        ViewState["userno"] = userno.ToString();
        ViewState["UserNo"] = userno.ToString();
        GetDetailsForPreview();
        disabletextbox();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ModelPopupDocumentVerify();</script>", false);

        BindVerfyDocumentList();
        if (lvVerifyDocumentList.Items.Count > 0)
        {
            pnlPopup.Visible = false;
            divDocumentList.Style["display"] = "inline";
            divMarksheetList.Style["display"] = "none";
            //btnSubmitDocVerify.Style["display"] = "none";
            btnSubmitDocVerify.Attributes.Remove("class");
            btnSubmitDocVerify.Attributes.Add("class", "btn btn-primary");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "ModelPopupDocumentVerify();", true);
        }
        else
        {
            pnlPopup.Visible = false;
            divDocumentList.Style["display"] = "none";
            divMarksheetList.Style["display"] = "none";
            btnSubmitDocVerify.Attributes.Add("class", "d-none");
            //btnSubmitDocVerify.Style["display"] = "none";
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "ModelPopupDocumentVerify();", false);
        }
    }
    protected void btnDocVerifyPreview_Click(object sender, ImageClickEventArgs e)
    {
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "ModelPopupDocumentVerify();", true);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ModelPopupDocumentVerify();</script>", false);
        pnlPopup.Visible = false;
        ImageButton lnkbtn = (ImageButton)sender;
        PreviewAndDownload(lnkbtn);
        string[] commandArgs = lnkbtn.CommandName.ToString().Split(new char[] { ',' });
        string DocumentFrom = commandArgs[0];
        string Userno = commandArgs[1];
        string Qualifyno = commandArgs[2];
        //divlastname.Style["position"] = "absolute";
        //divlastname.Style["top"] = "20px"; //--- replace with your value
        //divlastname.Style["left"] = "20px"; //---
        if (Convert.ToInt32(Session["OrgId"].ToString()) == 7) // For Rajagiri
        {
            if (DocumentFrom == "Q")
            {

                btnSubmitDocVerify.Attributes.Add("class", "d-none");
                divMarksheetList.Style["display"] = "inline";
                divDocumentList.Style["display"] = "none";
                // divbtnsub.Style["display"] = "block";
                objAP.Userno = Convert.ToInt32(Userno);
                objAP.QualifyNo = Convert.ToInt32(Qualifyno);
                DataSet dsList = objAC.GetMarksheetList(objAP);
                if (dsList != null && dsList.Tables[0].Rows.Count > 0)
                {
                    pnlSubject.Style["display"] = "inline";
                    pnlGrade.Style["display"] = "none";
                    lvSubject.DataSource = dsList.Tables[0];
                    lvSubject.DataBind();
                }
                if (dsList != null && dsList.Tables[1].Rows.Count > 0)
                {
                    pnlGrade.Style["display"] = "inline";
                    pnlSubject.Style["display"] = "none";
                    lvGrade.DataSource = dsList.Tables[1];
                    lvGrade.DataBind();
                }

            }
            else
            {
                btnSubmitDocVerify.Attributes.Remove("class");
                btnSubmitDocVerify.Attributes.Add("class", "btn btn-primary");
                divMarksheetList.Style["display"] = "none";
                divDocumentList.Style["display"] = "inline";
            }
        }
        else if (Convert.ToInt32(Session["OrgId"].ToString()) == 15) // For DAIICT
        {

            btnSubmitDocVerify.Attributes.Remove("class");
            btnSubmitDocVerify.Attributes.Add("class", "btn btn-primary");
            divMarksheetList.Style["display"] = "none";
            divDocumentList.Style["display"] = "none";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "ModelPopupDocumentVerify();", true);
        }
        else  // For Other
        {
            btnSubmitDocVerify.Attributes.Remove("class");
            btnSubmitDocVerify.Attributes.Add("class", "btn btn-primary");
            divMarksheetList.Style["display"] = "none";
            divDocumentList.Style["display"] = "inline";
        }
    }
    private DataTable Add_Datatable_IsVerify()
    {
        DataTable dt = CreateDatatable_IsVerify();
        try
        {
            int rowIndex = 0;
            int isverify = 0;

            foreach (var item in lvVerifyDocumentList.Items)
            {
                CheckBox chDocument = (CheckBox)lvVerifyDocumentList.Items[rowIndex].FindControl("chkDocument");
                DataRow dRow = dt.NewRow();
                HiddenField hfUserno = (HiddenField)lvVerifyDocumentList.Items[rowIndex].FindControl("hdnUserNo");
                HiddenField hfDocumentno = (HiddenField)lvVerifyDocumentList.Items[rowIndex].FindControl("hdnDocumentNo");
                HiddenField hfDocumemntFrom = (HiddenField)lvVerifyDocumentList.Items[rowIndex].FindControl("hdnDocumentFrom");

                if (chDocument.Checked == true)
                {
                    isverify = 1;
                }
                else
                {
                    isverify = 0;
                }
                if (Convert.ToInt32(Session["OrgId"].ToString()) == 7)
                {
                    if (Convert.ToInt32(hfDocumentno.Value) == 4 && hfDocumemntFrom.Value == "Q") // for plus two level certificate check || 4-XII document from qualification page
                    {
                        if (chDocument.Enabled == true && chDocument.Checked == true)
                        {
                            isverify = 1;
                        }
                        else
                        {
                            isverify = 0;
                        }
                    }
                }
                else if (Convert.ToInt32(Session["OrgId"].ToString()) == 15)
                {
                    if (Convert.ToInt32(hfDocumentno.Value) == 6 && hfDocumemntFrom.Value == "Q") // for plus two level certificate check || 6-XII document from qualification page
                    {
                        if (chDocument.Enabled == true && chDocument.Checked == true)
                        {
                            isverify = 1;
                        }
                        else
                        {
                            isverify = 0;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(hfDocumentno.Value) == 4 && hfDocumemntFrom.Value == "Q") // for plus two level certificate check || 4-XII document from qualification page
                    {
                        if (chDocument.Enabled == true && chDocument.Checked == true)
                        {
                            isverify = 1;
                        }
                        else
                        {
                            isverify = 0;
                        }
                    }
                }
                dRow["UserNo"] = Convert.ToInt32(hfUserno.Value);
                dRow["IsVerify"] = isverify;
                dRow["DocumentFrom"] = hfDocumemntFrom.Value;
                dRow["DocumentNo"] = Convert.ToInt32(hfDocumentno.Value);
                dRow["Remark"] = txtRemark.Text;
                rowIndex += 1;
                dt.Rows.Add(dRow);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcess.Add_Datatable_IsVerify() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable CreateDatatable_IsVerify()
    {
        DataTable dt = new DataTable();
        dt.TableName = "IsVerifyDatatable";
        dt.Columns.Add("UserNo");
        dt.Columns.Add("IsVerify");
        dt.Columns.Add("DocumentNo");
        dt.Columns.Add("DocumentFrom");
        dt.Columns.Add("Remark");
        ViewState["CurrentTable"] = dt;
        return dt;
    }
    protected void btnSubmitDocVerify_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtRemark.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updApplicationProcess, "Please Enter Remark", Page);
                return;
            }
            string documentno = string.Empty;
            string documentfrom = string.Empty;
            DataTable dt = Add_Datatable_IsVerify();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            string IsVerify = ds.GetXml();
            objAP.UaNo = Convert.ToInt32(Session["userno"].ToString());
            int status = objAC.InsUpdVerifyDocument(objAP, IsVerify);
            if (status == 2)
            {
              //  DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
                BindListView(0);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "ModelPopupDocumentVerify();", true);
                divDocumentList.Style["display"] = "inline";
                divMarksheetList.Style["display"] = "none";
                objCommon.DisplayMessage(this.updApplicationProcess, "Document Verified Successfully", Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ModelPopupDocumentVerify();</script>", false);
                pnlPopup.Visible = false;
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updApplicationProcess, "Error", Page);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "ModelPopupDocumentVerify();", true);
                divDocumentList.Style["display"] = "inline";
                divMarksheetList.Style["display"] = "none";
                // txtRemark.Text = string.Empty;
                pnlPopup.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.btnSubmitDocVerify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PreviewAndDownload(object sender)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameADMP"].ToString();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/ONLINEIMAGESUPLOAD" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).CommandArgument.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {

            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string filePath = directoryPath + "\\" + ImageName;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string urlpath = System.Configuration.ConfigurationManager.AppSettings["VirtualPathOnlineAdmissionDoc"].ToString();
                iframeView.Src = urlpath + ImageName;
                //      mpeViewDocument.Show();
                pnlPopup.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationProcessNew.PreviewAndDownload-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
    #region BlogStorage
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
           

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
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
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    #endregion BlogStorage
    #region Preview Form Button Methods
    private void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO DESC");
        objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0", "RELIGIONNO ASC");
        objCommon.FillDropDownList(ddlSocCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORYNO Asc");
        objCommon.FillDropDownList(ddlStateDomicile, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
        objCommon.FillDropDownList(ddlMOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCUPATION");
        objCommon.FillDropDownList(ddlFOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0", "OCCUPATION");
        //int CategoryOfFrom = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "DISTINCT FORM_CATEGORY", "USERNO=" + ((UserDetails)(Session["user"])).UserNo));
        //if (CategoryOfFrom == 1)
        //{
        objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0", "NATIONALITYNO");
        ddlNationality.SelectedIndex = 1;
        ddlNationality.Enabled = false;
        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>1", "NATIONALITY");
        //    ddlStateDomicile.Visible = false;
        //    txtNRIstate.Visible = true;
        //}


        //objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO DESC");
    }
    protected void btnPreviewForm_Click(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "OpenPreview();", true);
            ImageButton btnPreview = sender as ImageButton;

            string UserNo = btnPreview.CommandArgument.ToString();
            ViewState["UserNo"] = UserNo;

            GetDetailsForPreview();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> ModelPopupDocumentVerify();</script>", false);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "OpenPreview();", true);

            disabletextbox();
        }
        catch (Exception ex)
        {

        }
    }
    protected void GetDetailsForPreview()
    {
        try
        {

            BindBranchPref_details();
            BindData();

            bindEdulistview();

            BindTestScoreList();

            BindWorkExpEmp();
            BindWorkExpEnt();
            BindWorkReference();

            BindUploadedDocs();

            BindSubmittedReservDetails();

            BindFinalSubmissionDetails();

        }
        catch (Exception ex)
        {

        }
    }
    private void BindBranchPref_details()
    {
        try
        {
            ////DataSet ds = objAC.GetAllBranchDetails(((UserDetails)(Session["user"])).UserNo);
            if (ViewState["UserNo"] == null)
            {
                return;
            }
            DataSet ds = objAC.GetAllBranchDetails(Convert.ToInt32(ViewState["UserNo"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvProgramme.DataSource = ds;
                lvProgramme.DataBind();
                lvProgramme.Visible = true;
            }
            else
            {
                lvProgramme.DataSource = null;
                lvProgramme.DataBind();
                lvProgramme.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }


    private void BindData()
    {
        try
        {
            if (ViewState["UserNo"] == null)
            {
                return;
            }

            PopulateDropDown();

            int userno = (Convert.ToInt32(ViewState["UserNo"]));
            string dob_User = objCommon.LookUp("ACD_USER_REGISTRATION", "DOB", "USERNO= " + userno);
            txtDateOfBirth.Text = dob_User;
            txtDateOfBirth.Enabled = false;
            string gender = objCommon.LookUp("ACD_USER_REGISTRATION", "GENDER", "USERNO =" + (Convert.ToInt32(ViewState["UserNo"])));
            if (gender.TrimEnd().Equals("M"))
            {
                rbMale.Checked = true;
                rbMale.Visible = true;
                rbMale.Enabled = false;
                rbFemale.Visible = false;
                rbTransGender.Visible = false;
            }
            else if (gender.TrimEnd().Equals("F"))
            {
                rbFemale.Checked = true;
                rbFemale.Visible = true;
                rbFemale.Enabled = false;
                rbMale.Visible = false;
                rbTransGender.Visible = false;
            }
            else if (gender.TrimEnd().Equals("T"))
            {
                rbTransGender.Checked = true;
                rbTransGender.Visible = true;
                rbTransGender.Enabled = false;
                rbMale.Visible = false;
                rbFemale.Visible = false;
            }
            //DataSet ds = objUC.GetRecordForPersonalDetails(userno);s
            DataSet ds = objAC.GetRecordForPersonalDetails(userno);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lblAppId.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                txtFullName.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                txtFirstName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                txtLastName.Text = ds.Tables[0].Rows[0]["LASTNAME"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMothersName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                txtAlternateEmailId.Text = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();

                if (ds.Tables[0].Rows[0]["GENDER"].ToString().Trim().Equals("F"))
                {
                    rbFemale.Checked = true;
                    rbMale.Checked = false;

                }
                else
                {
                    rbFemale.Checked = false;
                    rbMale.Checked = true;
                    int programtype = 0;
                    programtype = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "ISNULL(MAX(PROGRAMME_TYPE),0)", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"]))));
                }

                if (ds.Tables[0].Rows[0]["COUNTRY_CODE"] == DBNull.Value)
                {
                    txtmobilecode.Text = "+91";
                }
                else
                {
                    txtmobilecode.Text = ds.Tables[0].Rows[0]["COUNTRY_CODE"].ToString();
                }

                int religion = Convert.ToInt32(ddlReligion.SelectedValue = ds.Tables[0].Rows[0]["RELIGIONNO"].ToString());
                ddlSocCategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();

                string NTEXT = ds.Tables[0].Rows[0]["NATIONALITY"].ToString();
                string SN = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY ", "NATIONALITY='" + NTEXT + "'");
                ddlStateDomicile.SelectedValue = ds.Tables[0].Rows[0]["STATE_DOMICILE"].ToString();
                ddlBloodGroup.SelectedValue = ds.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                txtIdentificationMark.Text = ds.Tables[0].Rows[0]["IDENTITY_MARK"].ToString();
                txtAdhaarNo.Text = ds.Tables[0].Rows[0]["ADHAARNO"].ToString();
                ddlMarital.SelectedValue = ds.Tables[0].Rows[0]["MARITALSTATUS"].ToString();
                int Diffable = Convert.ToInt32(ddlDiffAbility.SelectedValue = ds.Tables[0].Rows[0]["DIFFERENTLY_ABLED"].ToString());
                if (Diffable == 1)
                {
                    Abilility.Visible = true;
                    txtNatureOfDisability.Text = ds.Tables[0].Rows[0]["NATURE_DISABILITY"].ToString();
                    txtPercentageOfDisability.Text = ds.Tables[0].Rows[0]["PERCENTAGE_DISABILITY"].ToString();
                }


                txtHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0]["WEIGHT"].ToString();

                txtFTelNo.Text = ds.Tables[0].Rows[0]["F_TELNUMBER"].ToString();
                txtFMobile.Text = ds.Tables[0].Rows[0]["F_MOBILENO"].ToString();
                ddlFOccupation.SelectedValue = ds.Tables[0].Rows[0]["F_OCCUPATION"].ToString();
                txtFDesignation.Text = ds.Tables[0].Rows[0]["F_DESIGNATION"].ToString();
                txtFEmail.Text = ds.Tables[0].Rows[0]["FEMAILADDRESS"].ToString();

                txtMTelNo.Text = ds.Tables[0].Rows[0]["M_TELNUMBER"].ToString();
                txtMMobile.Text = ds.Tables[0].Rows[0]["M_MOBILENO"].ToString();
                ddlMOccupation.SelectedValue = ds.Tables[0].Rows[0]["MOCCUPATION"].ToString();
                txtMDesignation.Text = ds.Tables[0].Rows[0]["M_DESIGNATION"].ToString();
                txtMEmail.Text = ds.Tables[0].Rows[0]["M_EMAILADDRESS"].ToString();
                ddlParentsIncome.SelectedValue = ds.Tables[0].Rows[0]["ANNUAL_INCOME"].ToString();


                txtCorresAddress.Text = ds.Tables[0].Rows[0]["LADDRESS"].ToString();
                objCommon.FillDropDownList(ddlLCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "", "COUNTRYNAME");
                ddlLCon.SelectedValue = ds.Tables[0].Rows[0]["LCOUNTRYID"].ToString();
                objCommon.FillDropDownList(ddlLSta, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ACTIVESTATUS = 1 AND COUNTRYNO=" + ddlLCon.SelectedValue + "", "STATENAME");
                ddlLSta.SelectedValue = ds.Tables[0].Rows[0]["LSTATEID"].ToString();
                objCommon.FillDropDownList(ddlCorrDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 AND ACTIVESTATUS = 1 AND STATENO=" + ddlLSta.SelectedValue, "DISTRICTNAME");
                ddlCorrDistrict.SelectedValue = ds.Tables[0].Rows[0]["LDISTRICTID"].ToString();
                txtLCity.Text = ds.Tables[0].Rows[0]["LCITY"].ToString();
                txtLocalPIN.Text = ds.Tables[0].Rows[0]["LPINCODE"].ToString();
                txtLTaluka.Text = ds.Tables[0].Rows[0]["LTALUKA"].ToString();

                txtPermAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
                objCommon.FillDropDownList(ddlPCon, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "", "COUNTRYNAME");
                ddlPCon.SelectedValue = ds.Tables[0].Rows[0]["PCOUNTRYID"].ToString();
                objCommon.FillDropDownList(ddlPermanentState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ACTIVESTATUS = 1 AND COUNTRYNO=" + ddlPCon.SelectedValue + "", "STATENAME");
                ddlPermanentState.SelectedValue = ds.Tables[0].Rows[0]["PSTATEID"].ToString();
                objCommon.FillDropDownList(ddlPermanentDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 AND ACTIVESTATUS = 1 AND STATENO=" + ddlPermanentState.SelectedValue, "DISTRICTNAME");
                ddlPermanentDistrict.SelectedValue = ds.Tables[0].Rows[0]["PDISTRICTID"].ToString();
                txtPermanentCity.Text = ds.Tables[0].Rows[0]["PCITY"].ToString();
                txtPermPIN.Text = ds.Tables[0].Rows[0]["PPINCODE"].ToString();
                txtPerTaluka.Text = ds.Tables[0].Rows[0]["PTALUKA"].ToString();
                //Added by Saurabh Kumare on 27-03-2023

                ddlresidency.SelectedValue = ds.Tables[0].Rows[0]["PLACE_OF_RESIDENCY"].ToString().Trim();
                txtPerTaluka.Text = ds.Tables[0].Rows[0]["PTALUKA"].ToString().Trim();
                txtLTaluka.Text = ds.Tables[0].Rows[0]["LTALUKA"].ToString().Trim();
                txtPassportNo.Text = ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();

                txtDateOfIssuePass.Text = ds.Tables[0].Rows[0]["DATE_OF_ISSUE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE_OF_ISSUE"].ToString()).ToString("yyyy-MM-dd");
                txtDateOfExpiryPass.Text = ds.Tables[0].Rows[0]["DATE_OF_EXPIRY"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE_OF_EXPIRY"].ToString()).ToString("yyyy-MM-dd");
                txtPlaceOfIssuePass.Text = ds.Tables[0].Rows[0]["PLACE_OF_ISSUE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["PLACE_OF_ISSUE"].ToString();
                txtProofNRIStatus.Text = ds.Tables[0].Rows[0]["ISSUING_PROOF"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["ISSUING_PROOF"].ToString();
                ddlCountryOfIssuePass.SelectedValue = ds.Tables[0].Rows[0]["COUNTRY_OF_ISSUE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["COUNTRY_OF_ISSUE"].ToString();


                byte[] imgData = null;
                //UPDATED BY: MD. REHBAR SHEIKH ON 09-02-2018
                if (ds.Tables[0].Rows[0]["PHOTO"] != DBNull.Value)
                {
                    imgData = ds.Tables[0].Rows[0]["PHOTO"] as byte[];

                    imgPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                    //imgPhotoNoCrop.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);

                    //imgPhoto.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                    //imgPhotoNoCrop.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENT";
                    ViewState["PHOTO"] = ds.Tables[0].Rows[0]["PHOTO"];
                }
                else
                {
                    imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                    //imgPhotoNoCrop.ImageUrl = "~/IMAGES/nophoto.jpg";

                }
                if (ds.Tables[0].Rows[0]["USER_SIGN"] != DBNull.Value)
                {
                    imgData = ds.Tables[0].Rows[0]["USER_SIGN"] as byte[];
                    ImgSign.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                    //ImgSignNoCrop.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);

                    //ImgSign.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENTSIGN";
                    //ImgSignNoCrop.ImageUrl = "~/showEmpImage.aspx?id=" + ((UserDetails)(Session["user"])).UserNo + "&type=STUDENTSIGN";
                    ViewState["USER_SIGN"] = ds.Tables[0].Rows[0]["USER_SIGN"];
                }
                else
                {
                    ImgSign.ImageUrl = "~/IMAGES/sign11.jpg";
                    //ImgSignNoCrop.ImageUrl = "~/IMAGES/sign11.jpg";

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void bindEdulistview()
    {
        try
        {
            if (ViewState["UserNo"] == null)
            {
                return;
            }
            //int userno = ((UserDetails)(Session["user"])).UserNo;
            int userno = (Convert.ToInt32(ViewState["UserNo"]));

            DataSet ds = objAC.GetEducationalDetails(Convert.ToInt32(ViewState["UserNo"]));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("1"))
                    {

                        //for ssc 10th
                        divQualiX.Visible = true;
                        divQualiX.Visible = true;
                        DataTable tblSSC_Filtered = ds.Tables[0].AsEnumerable()
                                            .Where(row => row.Field<int>("USERNO").Equals(userno)
                                             && row.Field<int>("QUALIFYNO").Equals(1))
                                .CopyToDataTable();

                        txtRegisterNumber.Text = tblSSC_Filtered.Rows[0]["REGNO"].ToString();
                        txtInstitution.Text = tblSSC_Filtered.Rows[0]["INSTITUTION_NAME"].ToString();
                        ddlCountry.Text = tblSSC_Filtered.Rows[0]["COUNTRYNAME"].ToString();
                        ddlState.Text = tblSSC_Filtered.Rows[0]["STATENAME"].ToString();
                        ddlBoardExamination.Text = tblSSC_Filtered.Rows[0]["BOARDNAME"].ToString();
                        ddlYearPassing.Text = tblSSC_Filtered.Rows[0]["YEARNAME"].ToString();
                        ddlMonthPassing.Text = tblSSC_Filtered.Rows[0]["MONTH_OF_PASSING_NAME"].ToString();
                        txtNumberAttempts.Text = tblSSC_Filtered.Rows[0]["NO_OF_ATTEMPTS"].ToString();
                        ddlEvaluationType.Text = tblSSC_Filtered.Rows[0]["EVALUATION_TYPE_TEXT"].ToString();

                        txtXMaxMarks.Text = tblSSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MAX_MARK"].ToString();
                        txtXMarksObtained.Text = tblSSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MARKS_OBTAINED"].ToString();
                        txtXPerOfMarks.Text = tblSSC_Filtered.Rows[0]["PERCENTAGE"].ToString();

                        txtCertificateStatus.Text = tblSSC_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString();

                        break;
                    }
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("2"))
                    {
                        //for hsc 12th
                        divQualiPlusTwo.Visible = true;
                        DataTable tblHSC_Filtered = ds.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(2))
                            .CopyToDataTable();

                        txtPlusTwoRegNo.Text = tblHSC_Filtered.Rows[0]["REGNO"].ToString();
                        txtPluseTwoInstitutionName.Text = tblHSC_Filtered.Rows[0]["INSTITUTION_NAME"].ToString();
                        ddlPlusTwoCountry.Text = tblHSC_Filtered.Rows[0]["COUNTRYNAME"].ToString();
                        ddlPlusTwoState.Text = tblHSC_Filtered.Rows[0]["STATENAME"].ToString();
                        ddlPlusTwoBOE.Text = tblHSC_Filtered.Rows[0]["BOARDNAME"].ToString();
                        //ddlPlusTwoGroup.Text = tblHSC_Filtered.Rows[0]["GROUPNAME"].ToString();
                        ddlPlusTwoYearFrom.Text = tblHSC_Filtered.Rows[0]["YEAR_OF_STUDY_FROM"].ToString();
                        ddlPlusTwoYearTo.Text = tblHSC_Filtered.Rows[0]["YEAR_OF_STUDY_TO"].ToString();
                        ddlPlusTwoYPassing.Text = tblHSC_Filtered.Rows[0]["YEARNAME"].ToString();
                        ddlPlusTwoMPassing.Text = tblHSC_Filtered.Rows[0]["MONTH_OF_PASSING_NAME"].ToString();
                        txtPlusTwoNoOfAttempts.Text = tblHSC_Filtered.Rows[0]["NO_OF_ATTEMPTS"].ToString();

                        txtPTMaxMarks.Text = tblHSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MAX_MARK"].ToString();
                        txtPTMarksObtained.Text = tblHSC_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MARKS_OBTAINED"].ToString();
                        txtPTPerOfMarks.Text = tblHSC_Filtered.Rows[0]["PERCENTAGE"].ToString();

                        ddlEvaluationType.Text = tblHSC_Filtered.Rows[0]["EVALUATION_TYPE_TEXT"].ToString();
                        txtPlusTwoCertificateStatus.Text = tblHSC_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString();

                        txtPTAggPhyMaths.Text = tblHSC_Filtered.Rows[0]["AGG_PER_PHYSICS_MATHS"].ToString();

                        txtMaxMarksPhyMaths.Text = tblHSC_Filtered.Rows[0]["MAX_MARKS_PHY_MATHS"].ToString();
                        txtObtainedPhyMaths.Text = tblHSC_Filtered.Rows[0]["OBTAINED_MARKS_PHY_MATHS"].ToString();
                        ddlPTCountryOfInstitution.SelectedValue = tblHSC_Filtered.Rows[0]["INSTITUTION_COUNTRY_ID"].ToString();

                        if (Convert.ToInt32(tblHSC_Filtered.Rows[0]["RESULT_NOT_ANNOUNCED"].ToString()) == 1)
                        {
                            chkResult.Checked = true;
                            divaggpm.Visible = true;
                            divagg.Visible = true;
                            divxiaggmm.Visible = true;
                            divxiaggcgpa.Visible = true;
                            divXIAggPhyMaths.Visible = true;
                            txtXiAggCgpa.Text = tblHSC_Filtered.Rows[0]["XI_AGG_CGPA"].ToString();
                            txtXiAggPhyMaths.Text = tblHSC_Filtered.Rows[0]["XI_AGG_PER_PHYSICS_MATHS"].ToString();

                            txtXiMaxMarksPhyMaths.Text = tblHSC_Filtered.Rows[0]["XI_MAX_MARKS_PHY_MATHS"].ToString();
                            txtXiObtainedPhyMaths.Text = tblHSC_Filtered.Rows[0]["XI_OBTAINED_MARKS_PHY_MATHS"].ToString();

                        }
                        else
                        {
                            chkResult.Checked = false;
                            divaggpm.Visible = false;
                            divagg.Visible = false;
                            divxiaggmm.Visible = false;
                            divxiaggcgpa.Visible = false;
                            divXIAggPhyMaths.Visible = false;
                        }


                        break;
                    }
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("3"))
                    {
                        //for Graduation
                        objCommon.FillDropDownList(ddlUGQualifyDegree, "ACD_ADMP_QUALIDEGREE QD INNER JOIN ACD_ADMP_QUALIPROGRAM QP ON (QD.QUALI_DEGREE_ID=QP.QUALI_DEGREE_ID)", "QD.QUALI_DEGREE_ID", "QD.QUALI_DEGREE_NAME", "PROG_TYPE='" + "UG" + "'AND QD.STATUS= 1 AND QD.OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "union select 9999 ,'OTHER'", "QUALI_DEGREE_NAME");

                        divQualiUG.Visible = true;
                        DataTable tblUG_Filtered = ds.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(3))
                            .CopyToDataTable();

                        txtUGInstitutionName.Text = tblUG_Filtered.Rows[0]["INSTITUTION_NAME"].ToString();
                        ddlUGCountry.Text = tblUG_Filtered.Rows[0]["COUNTRYNAME"].ToString();
                        ddlUGState.Text = tblUG_Filtered.Rows[0]["STATENAME"].ToString();
                        ddlUGUniversity.Text = tblUG_Filtered.Rows[0]["BOARDNAME"].ToString();


                        try
                        {
                            ddlUGQualifyDegree.SelectedValue = tblUG_Filtered.Rows[0]["QUALI_DEGREE_ID"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txtUGQualifyDegree.Text = tblUG_Filtered.Rows[0]["OTHER_QUALI_DEGREE"].ToString();
                        if (txtUGQualifyDegree.Text == string.Empty)
                        {
                            divQualifyDegree.Visible = false;
                        }
                        else
                        {
                            divQualifyDegree.Visible = true;
                        }
                        objCommon.FillDropDownList(ddlUGProgram, "ACD_ADMP_QUALIPROGRAM ", "PROG_ID", "PROG_NAME", "QUALI_DEGREE_ID='" + Convert.ToInt32(ddlUGQualifyDegree.SelectedValue) + "'AND STATUS=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + " union select 9999 ,'OTHER'", "PROG_NAME");
                        try
                        {
                            ddlUGProgram.SelectedValue = tblUG_Filtered.Rows[0]["PROG_ID"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txtUGQualifyProgram.Text = tblUG_Filtered.Rows[0]["OTHER_QUALI_PROGRAM"].ToString();
                        if (txtUGQualifyProgram.Text == string.Empty)
                        {
                            divQualifyProgram.Visible = false;
                        }
                        else
                        {
                            divQualifyProgram.Visible = true;
                        }

                        txtUGSpecialization.Text = tblUG_Filtered.Rows[0]["SPECIALIZATION"].ToString();
                        ddlUGSemester.Text = tblUG_Filtered.Rows.Count.ToString();
                        ddlUGCourseDurationFrom.Text = tblUG_Filtered.Rows[0]["YEAR_OF_STUDY_FROM"].ToString();
                        ddlUGCourseDurationTo.Text = tblUG_Filtered.Rows[0]["YEAR_OF_STUDY_TO"].ToString();
                        ddlUGEvaluationType.Text = tblUG_Filtered.Rows[0]["EVALUATION_TYPE_TEXT"].ToString();

                        ddlUGYearOfPassing.Text = tblUG_Filtered.Rows[0]["YEARNAME"].ToString();


                        txtUGCGPA.Text = tblUG_Filtered.Rows[0]["UGPG_CGPA"].ToString();
                        txtUGMaxCGPA.Text = tblUG_Filtered.Rows[0]["UGPG_MAXCGPA"].ToString();
                        txtUGPercentOfMarks.Text = tblUG_Filtered.Rows[0]["PERCENTAGE"].ToString();

                        txtUGCertificateStatus.Text = tblUG_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString();

                        txtUGPerCGPA.Text = tblUG_Filtered.Rows[0]["UGPG_CGPA_PER"].ToString();  // exception

                        ddlUGQualifyDegree.Enabled = false;
                        ddlUGProgram.Enabled = false;


                        break;
                    }
                }


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("4"))
                    {
                        //for Post Graduation
                        divQualiPG.Visible = true;
                        DataTable tblPG_Filtered = ds.Tables[0].AsEnumerable()
                                            .Where(row => row.Field<int>("USERNO").Equals(userno)
                                             && row.Field<int>("QUALIFYNO").Equals(4))
                                .CopyToDataTable();

                        txtPGInstitutionName.Text = tblPG_Filtered.Rows[0]["INSTITUTION_NAME"].ToString();
                        ddlPGCountry.Text = tblPG_Filtered.Rows[0]["COUNTRYNAME"].ToString();
                        ddlPGState.Text = tblPG_Filtered.Rows[0]["STATENAME"].ToString();
                        ddlPGUniversity.Text = tblPG_Filtered.Rows[0]["BOARDNAME"].ToString();
                        ddlPGProgram.Text = tblPG_Filtered.Rows[0]["PROG_NAME"].ToString();
                        txtPGSpecialization.Text = tblPG_Filtered.Rows[0]["SPECIALIZATION"].ToString();
                        ddlPGSemester.Text = tblPG_Filtered.Rows.Count.ToString();
                        ddlPGCourseDurationFrom.Text = tblPG_Filtered.Rows[0]["YEAR_OF_STUDY_FROM"].ToString();
                        ddlPGCourseDurationTo.Text = tblPG_Filtered.Rows[0]["YEAR_OF_STUDY_TO"].ToString();
                        ddlPGEvaluationType.Text = tblPG_Filtered.Rows[0]["EVALUATION_TYPE_TEXT"].ToString();

                        ddlPGYearOfPassing.Text = tblPG_Filtered.Rows[0]["YEARNAME"].ToString();

                        // UGPG_SGPA,	UGPG_MAXSGPA,	UGPG_GRADE,	UGPG_CGPA,	UGPG_MAXCGPA, ALL_SUBJECT_TOTAL_MARKS_OBTAINED, ALL_SUBJECT_TOTAL_MAX_MARK
                        txtAllSubjectMarksObtained.Text = tblPG_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MARKS_OBTAINED"].ToString();
                        txtAllSubjectMaxMark.Text = tblPG_Filtered.Rows[0]["ALL_SUBJECT_TOTAL_MAX_MARK"].ToString();
                        txtPGPercentOfMarks.Text = tblPG_Filtered.Rows[0]["PERCENTAGE"].ToString();

                        txtPGCertificateStatus.Text = tblPG_Filtered.Rows[0]["IS_UPLOAD_DOCS"].ToString();

                        lvQualiPG.DataSource = tblPG_Filtered;//ds;
                        lvQualiPG.DataBind();
                        lvQualiPG.Visible = true;
                        break;
                    }
                }



                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["QUALIFYNO"].ToString().Equals("5"))
                    {
                        //for Other Education Details
                        divQualiOther.Visible = true;
                        DataTable tblOther_Filtered = ds.Tables[0].AsEnumerable()
                                        .Where(row => row.Field<int>("USERNO").Equals(userno)
                                         && row.Field<int>("QUALIFYNO").Equals(5))
                            .CopyToDataTable();

                        lvQualiOther.DataSource = tblOther_Filtered;//ds;
                        lvQualiOther.DataBind();
                        lvQualiOther.Visible = true;
                        break;
                    }
                }

            }
            else
            {
                divQualiX.Visible = false;
                divQualiPlusTwo.Visible = false;
                divQualiUG.Visible = false;
                divQualiPG.Visible = false;
                divQualiOther.Visible = false;
                lvQualiOther.DataSource = null;//ds;
                lvQualiOther.DataBind();
                lvQualiOther.Visible = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.bindEdulistview() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void BindTestScoreList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ADMP_TESTSCORE TRN LEFT OUTER JOIN ACD_ADMP_TESTSCORE_MASTER MST ON MST.SCOREID=TRN.SCOREID",
                "TRN.SCOREID,GATE_SUB_CODE,TESTNAME,QUALIFY_YEAR,REGNO ,SCORE_OBTAINED,SCORE_OUT_OF,INDIA_RANK",
                "CASE WHEN BLOB_CERTIFICATE_NAME = '' THEN 'NO' ELSE 'YES' END DOCUMENT", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"])), string.Empty);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divTestScoreList.Visible = true;
                lvTestScore.Visible = true;
                lvTestScore.DataSource = ds;
                lvTestScore.DataBind();
            }
            else
            {
                divTestScoreList.Visible = false;
                lvTestScore.Visible = false;
                lvTestScore.DataSource = null;
                lvTestScore.DataBind();
            }
            int ugpgot = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "UGPGOT", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"])) + ""));
            if (ugpgot == 1)
            {
                foreach (ListViewDataItem dataitem in lvTestScore.Items)
                {
                    System.Web.UI.Control thsubcode = lvTestScore.FindControl("thsubcode");
                    System.Web.UI.Control thrank = lvTestScore.FindControl("thrank");
                    System.Web.UI.Control thtotalscore = lvTestScore.FindControl("thtotalscore");
                    System.Web.UI.Control thobtscore = lvTestScore.FindControl("thobtscore");

                    thsubcode.Visible = false;
                    dataitem.FindControl("tdsubcode").Visible = false;
                    thrank.Visible = false;
                    dataitem.FindControl("tdrank").Visible = false;
                    thtotalscore.Visible = false;
                    dataitem.FindControl("tdtscore").Visible = false;
                    thobtscore.Visible = false;
                    dataitem.FindControl("tdobtscore").Visible = false;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindWorkExpEmp() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void BindUploadedDocs()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("DOCUMENTENTRY_FILE", "DOCUMENTTYPENO,DOCNAME AS	DOCUMENTNAME,	[FILENAME],	DOCUMENT_STATUS,[PATH] AS	PREVIEW_PATH,IIF([FILENAME]=NULL,'NO','YES') AS IS_UPLOAD_DOCS", "", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"])), string.Empty);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divDocuments.Visible = true;
                lvDocuments.DataSource = ds;
                lvDocuments.DataBind();
                lvDocuments.Visible = true;
            }
            else
            {
                divDocuments.Visible = false;
                lvDocuments.DataSource = null;
                lvDocuments.DataBind();
                lvDocuments.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindUploadedDocs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void BindWorkExpEmp()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_USER_WORK_EXPERIENCE", "ROW_NUMBER() OVER(ORDER BY [USERNO]) AS ID,USERNO,EMPLOYEMENT_ID,WORK_EXPERIENCE,ORGANIZATION,DESIGNATION,START_DURATION,END_DURATION,NATURE_WORK,NATURE_BUSINESS,MONTHLY_REMUNERATION", "", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"])) + "AND EMPLOYEMENT_ID=0", string.Empty);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divWorkExpEmployed.Visible = true;
                lvWorkExpEmp.DataSource = ds;
                lvWorkExpEmp.DataBind();
            }
            else
            {
                divWorkExpEmployed.Visible = false;
                lvWorkExpEmp.DataSource = null;
                lvWorkExpEmp.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindWorkExpEmp() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BindWorkExpEnt()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_USER_WORK_EXPERIENCE", "ROW_NUMBER() OVER(ORDER BY [USERNO]) AS ID,USERNO,EMPLOYEMENT_ID,WORK_EXPERIENCE,ORGANIZATION,DESIGNATION,START_DURATION,END_DURATION,NATURE_WORK,NATURE_BUSINESS,MONTHLY_REMUNERATION", "", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"])) + "AND EMPLOYEMENT_ID=1", string.Empty);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divWorkExpEntrepre.Visible = true;
                lvWorkExpEnt.DataSource = ds;
                lvWorkExpEnt.DataBind();
                lvWorkExpEnt.Visible = true;
            }
            else
            {
                divWorkExpEntrepre.Visible = false;
                lvWorkExpEnt.DataSource = null;
                lvWorkExpEnt.DataBind();
                lvWorkExpEnt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindWorkExpEnt() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BindWorkReference()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_USER_REFERENCE", "ROW_NUMBER() OVER(ORDER BY [USERNO]) AS ID,USERNO,REFERENCE_NAME,ORGANIZATION,DESIGNATION,EMAIL_ID,MOBILE_NO", "", "USERNO=" + (Convert.ToInt32(ViewState["UserNo"])), string.Empty);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divWorkExpRefer.Visible = true;
                lvWorkExpRefer.DataSource = ds;
                lvWorkExpRefer.DataBind();
                lvWorkExpRefer.Visible = true;
            }
            else
            {
                divWorkExpRefer.Visible = false;
                lvWorkExpRefer.DataSource = null;
                lvWorkExpRefer.DataBind();
                lvWorkExpRefer.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindWorkReference() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void BindSubmittedReservDetails()
    {

        try
        {
            if (ViewState["UserNo"] == null)
            {
                return;
            }
            Session["SS_DT_RESERVATION_ID"] = null;
            ViewState["VS_DB_TBL_RESERVATION"] = null;
            ViewState["VS_TBL_SPEvent"] = null;
            ViewState["VS_TBL_CULEvent"] = null;

            bool isDisable = false;
            divParentOfReservation.Visible = false;
            //StudentReservationDetailsController objReservDet = new StudentReservationDetailsController();
            //DataSet ds = objReservDet.GetAll_StudReservDetails(((UserDetails)(Session["user"])).UserNo);
            DataSet ds = objAC.GetReservationDetails(Convert.ToInt32(ViewState["UserNo"]));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                setReservationValues(ds, isDisable);
            }
            else
            {


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Intermediate.BindSubmittedReservDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void setReservationValues(DataSet dataSetFromDB, bool isDisable)
    {
        DataTable DT_ReservData = dataSetFromDB.Tables[0];
        if (DT_ReservData != null)
        {

            divParentOfReservation.Visible = true;
            ////ViewState["VS_DB_TBL_RESERVATION"] = DT_ReservData;// dataSetFromDB.Tables[0];

            if (Session["SS_DT_RESERVATION_ID"] == null)
            {
                ////Session["SS_DT_RESERVATION_ID"] = DT_ReservData;
            }

            visibleAllDiv(DT_ReservData);

            for (int i = 0; i < DT_ReservData.Rows.Count; i++)
            {
                if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("1"))
                {
                    //Reservation-1

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("N"))
                    {
                        visibilityOfReservation1(false);
                        rdoUnionTerr.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation1(true);
                        rdoUnionTerr.SelectedValue = "0";
                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuUnionTerrFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoUnionTerr.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("2"))
                {
                    //Reservation-2

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("N"))
                    {
                        visibilityOfReservation2(false);
                        rdoUniAndman.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation2(true);
                        rdoUniAndman.SelectedValue = "0";
                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuUniAndamanFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoUniAndman.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("3"))
                {
                    //Reservation-3 NCC
                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("N"))
                    {
                        visibilityOfReservation3(false);
                        rdoNCCGrade.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation3(true);
                        rdoNCCGrade.SelectedValue = "0";
                        ddlNCCGrade.SelectedValue = DT_ReservData.Rows[i]["NCC_GRADE_ID"] == null ? "0" : DT_ReservData.Rows[i]["NCC_GRADE_ID"].ToString();
                        ddlNCCGrade.Enabled = isDisable;
                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuNCCGradeFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoNCCGrade.Enabled = isDisable;
                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("4"))
                {
                    //Reservation-4 NSS
                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("N"))
                    {
                        visibilityOfReservation4(false);
                        rdoNSSGrade.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation4(true);
                        rdoNSSGrade.SelectedValue = "0";
                        ddlNSSGrade.SelectedValue = DT_ReservData.Rows[i]["NSS_GRADE_ID"] == null ? "0" : DT_ReservData.Rows[i]["NSS_GRADE_ID"].ToString();
                        ddlNSSGrade.Enabled = isDisable;
                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuNSSGradeFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoNSSGrade.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("5"))
                {
                    //Reservation-5 Others
                    //object other = DT_ReservData.Rows[i]["OTHER"];
                    if (string.IsNullOrEmpty(DT_ReservData.Rows[i]["OTHER"].ToString()))
                    {
                        visibilityOfReservation5(false);
                        rdoOthers.SelectedValue = "0";
                    }
                    else if (DT_ReservData.Rows[i]["OTHER"].ToString().Equals("0"))
                    {
                        visibilityOfReservation5(false);
                        rdoOthers.SelectedValue = "0";
                    }
                    else if (Convert.ToInt32(DT_ReservData.Rows[i]["OTHER"].ToString()) > 0)
                    {
                        visibilityOfReservation5(true);
                        rdoOthers.SelectedValue = DT_ReservData.Rows[i]["OTHER"].ToString();
                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuOtherFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoOthers.Enabled = isDisable;


                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("6"))
                {
                    //Reservation-6 Sports|Cultural Event

                    //Taken Implemetation After All Done

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("N"))
                    {
                        //ViewState["VS_TBL_SPEvent"] = null;
                        //ViewState["VS_TBL_CULEvent"] = null;
                        visibilityOfReservation6(false, false, false);
                        rdoSportsCultural.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        rdoSportsCultural.SelectedValue = "0";

                        #region Sports|Cultural
                        if (dataSetFromDB.Tables[1].Rows.Count > 0 && dataSetFromDB.Tables[2].Rows.Count > 0)
                        {
                            chkBtnSportsCultural.Items[0].Selected = true;
                            chkBtnSportsCultural.Items[1].Selected = true;
                            if (dataSetFromDB.Tables[1].Rows[0]["FILENAME"].ToString().Length > 0)
                            {
                                //lblfuSportsEventFileName.Text = dataSetFromDB.Tables[1].Rows[0]["FILENAME"].ToString();
                            }

                            if (dataSetFromDB.Tables[2].Rows[0]["FILENAME"].ToString().Length > 0)
                            {
                                //lblfuCulturalEventFileName.Text = dataSetFromDB.Tables[2].Rows[0]["FILENAME"].ToString();
                            }
                            //ViewState["VS_TBL_SPEvent"] = dataSetFromDB.Tables[1];
                            //ViewState["VS_TBL_CULEvent"] = dataSetFromDB.Tables[2];

                            BindSportsEventList_DB(dataSetFromDB.Tables[1], isDisable);
                            BindCulturalEventList_DB(dataSetFromDB.Tables[2], isDisable);
                            visibilityOfReservation6(true, true, true);

                        }
                        else if (dataSetFromDB.Tables[1].Rows.Count > 0 && dataSetFromDB.Tables[2].Rows.Count <= 0)
                        {
                            if (dataSetFromDB.Tables[1].Rows[0]["FILENAME"].ToString().Length > 0)
                            {
                                //lblfuSportsEventFileName.Text = dataSetFromDB.Tables[1].Rows[0]["FILENAME"].ToString();
                            }

                            chkBtnSportsCultural.Items[0].Selected = true;
                            chkBtnSportsCultural.Items[1].Selected = false;
                            //ViewState["VS_TBL_SPEvent"] = dataSetFromDB.Tables[1];
                            //ViewState["VS_TBL_CULEvent"] = null;
                            BindSportsEventList_DB(dataSetFromDB.Tables[1], isDisable);

                            visibilityOfReservation6(true, true, false);

                        }
                        else if (dataSetFromDB.Tables[1].Rows.Count <= 0 && dataSetFromDB.Tables[2].Rows.Count > 0)
                        {
                            if (dataSetFromDB.Tables[2].Rows[0]["FILENAME"].ToString().Length > 0)
                            {
                                //lblfuCulturalEventFileName.Text = dataSetFromDB.Tables[2].Rows[0]["FILENAME"].ToString();
                            }
                            chkBtnSportsCultural.Items[0].Selected = false;
                            chkBtnSportsCultural.Items[1].Selected = true;
                            //ViewState["VS_TBL_SPEvent"] = null;
                            //ViewState["VS_TBL_CULEvent"] = dataSetFromDB.Tables[2];
                            BindCulturalEventList_DB(dataSetFromDB.Tables[2], isDisable);

                            visibilityOfReservation6(true, false, true);

                        }
                        else { }
                        #endregion
                    }
                    else
                    {

                    }

                    rdoSportsCultural.Enabled = isDisable;
                    chkBtnSportsCultural.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("7"))
                {
                    //Reservation-7 Reserv State Quota

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("0"))
                    {
                        visibilityOfReservation7(false);
                        rdoReservStateQuota.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation7(true);
                        rdoReservStateQuota.SelectedValue = "0";
                        ddlReservStateQuota.SelectedValue = DT_ReservData.Rows[i]["CATEGORY_ID"] == null ? "0" : DT_ReservData.Rows[i]["CATEGORY_ID"].ToString();
                        ddlReservStateQuota.Enabled = isDisable;
                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuReservStateQuotaFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoReservStateQuota.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("8"))
                {
                    //Reservation-8 Disability Quota

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("0"))
                    {
                        visibilityOfReservation8(false);
                        rdoReservDisability.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation8(true);
                        rdoReservDisability.SelectedValue = "0";
                        ddlReservDisability.SelectedValue = DT_ReservData.Rows[i]["DISABILITY_ID"] == null ? "0" : DT_ReservData.Rows[i]["DISABILITY_ID"].ToString();
                        ddlReservDisability.Enabled = isDisable;
                        txtDisabilityPercent.Text = DT_ReservData.Rows[i]["DISABILITY_PERCENT"] == null ? "" : DT_ReservData.Rows[i]["DISABILITY_PERCENT"].ToString();
                        txtDisabilityPercent.Enabled = isDisable;
                        txtDisabilityUdidNumber.Text = DT_ReservData.Rows[i]["UDID_NO"] == null ? "" : DT_ReservData.Rows[i]["UDID_NO"].ToString();
                        txtDisabilityUdidNumber.Enabled = isDisable;

                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuReservDisabilityFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoReservDisability.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("9"))
                {
                    //Reservation-9 Other Disability

                    txtOtherDisability.Text = DT_ReservData.Rows[i]["OTHER_DISABILITY"] == null ? "" : DT_ReservData.Rows[i]["OTHER_DISABILITY"].ToString();
                    if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                    {
                        //lblfuOtherDisabilityFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                    }
                    txtOtherDisability.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("10"))
                {
                    //Reservation-10 Nanmamudra or Rajyapuraskar in Higher Secondary Level

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("0"))
                    {
                        visibilityOfReservation10(false);
                        rdoNanmamudra.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation10(true);
                        rdoNanmamudra.SelectedValue = "0";

                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            //lblfuNanmamudraFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoNanmamudra.Enabled = isDisable;

                }

                else if (DT_ReservData.Rows[i]["RESERVATION_ID"].ToString().Equals("11"))
                {
                    //Reservation-11 Student Police Cadets

                    if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("0"))
                    {
                        visibilityOfReservation11(false);
                        rdoPoliceCadets.SelectedValue = "1";
                    }
                    else if (DT_ReservData.Rows[i]["IS_ALLOW"].ToString().Equals("Y"))
                    {
                        visibilityOfReservation11(true);
                        rdoPoliceCadets.SelectedValue = "0";

                        if (DT_ReservData.Rows[i]["FILENAME"].ToString().Length > 0)
                        {
                            ////lblfuPoliceCadetsFileName.Text = DT_ReservData.Rows[i]["FILENAME"].ToString();
                        }
                    }
                    else
                    {

                    }

                    rdoPoliceCadets.Enabled = isDisable;
                }
                else
                {
                    divReservation1.Visible = false;
                    divReservation2.Visible = false;
                    divReservation3.Visible = false;
                    divReservation4.Visible = false;
                    divReservation5.Visible = false;
                    divReservation6.Visible = false;
                    divReservation7.Visible = false;
                    divReservation8.Visible = false;
                    divReservation9.Visible = false;
                    divReservation10.Visible = false;
                    divReservation11.Visible = false;
                }

            }

        }
        else
        {
            //dt = null;
        }
    }


    #region Visibility Of Div

    private void visibleAllDiv(DataTable dt)
    {
        try
        {

            rdoUnionTerr.SelectedValue = "1";
            rdoUniAndman.SelectedValue = "1";
            rdoNCCGrade.SelectedValue = "1";
            rdoNSSGrade.SelectedValue = "1";
            rdoOthers.SelectedValue = "0";
            rdoSportsCultural.SelectedValue = "1";
            rdoReservStateQuota.SelectedValue = "1";
            rdoReservDisability.SelectedValue = "1";
            rdoNanmamudra.SelectedValue = "1";
            rdoPoliceCadets.SelectedValue = "1";

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                //Reservation-1
                if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("1"))
                {
                    divReservation1.Visible = true;
                }
                //Reservation-2
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("2"))
                {
                    divReservation2.Visible = true;
                }
                //Reservation-3
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("3"))
                {
                    divReservation3.Visible = true;
                }
                //Reservation-4
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("4"))
                {
                    divReservation4.Visible = true;
                }
                //Reservation-5
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("5"))
                {
                    divReservation5.Visible = true;
                }
                //Reservation-6
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("6"))
                {
                    divReservation6.Visible = true;
                }
                //Reservation-7
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("7"))
                {
                    divReservation7.Visible = true;
                }
                //Reservation-8
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("8"))
                {
                    divReservation8.Visible = true;
                }
                //Reservation-9
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("9"))
                {
                    divReservation9.Visible = true;
                }
                //Reservation-10
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("10"))
                {
                    divReservation10.Visible = true;
                }
                //Reservation-11
                else if (dt.Rows[i]["RESERVATION_ID"].ToString().Equals("11"))
                {
                    divReservation11.Visible = true;
                }
                else
                {
                    Session["fuOtherDisability"] = null;
                    lblfuOtherDisabilityFileName.Text = "";

                    divReservation1.Visible = false;
                    divReservation2.Visible = false;
                    divReservation3.Visible = false;
                    divReservation4.Visible = false;
                    divReservation5.Visible = false;
                    divReservation6.Visible = false;
                    divReservation7.Visible = false;
                    divReservation8.Visible = false;
                    divReservation9.Visible = false;
                    divReservation10.Visible = false;
                    divReservation11.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.PopulateAllDivVisibility() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    #region Reservation-1
    protected void visibilityOfReservation1(bool isVisible)
    {
        try
        {
            divUnionTerr.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation1() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-2
    protected void visibilityOfReservation2(bool isVisible)
    {
        try
        {
            divUniAndaman.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation2() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-3
    protected void visibilityOfReservation3(bool isVisible)
    {
        try
        {
            divNCCGradeDDL.Visible = isVisible;
            divNCCGradeFile.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation3() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-4
    protected void visibilityOfReservation4(bool isVisible)
    {
        try
        {
            divNSSGradeDDL.Visible = isVisible;
            divNSSGradeFile.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation4() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-5
    protected void visibilityOfReservation5(bool isVisible)
    {
        try
        {
            divOtherFile.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation5() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-6
    protected void visibilityOfReservation6(bool isVisible, bool isSportCheck, bool isCulturalCheck)
    {
        try
        {
            chkBtnSportsCultural.Visible = isVisible;
            if (isVisible)
            {
                chkBtnSportsCultural.Items[0].Selected = isSportCheck;
                chkBtnSportsCultural.Items[1].Selected = isCulturalCheck;

                bool isSportsVisible = chkBtnSportsCultural.Items[0].Selected ? true : false;
                visibilityOfSportsEvent(isSportsVisible);
                bool isCulturalVisible = chkBtnSportsCultural.Items[1].Selected ? true : false;
                visibilityOfCulturalEvent(isCulturalVisible);
            }
            else
            {
                divSportsEvent.Visible = isVisible;
                divCulturalEvent.Visible = isVisible;
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation6() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void visibilityOfSportsEvent(bool isVisible)
    {
        try
        {
            divSportsEvent.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfSportsEvent() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void visibilityOfCulturalEvent(bool isVisible)
    {
        try
        {
            divCulturalEvent.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfCulturalEvent() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-7
    protected void visibilityOfReservation7(bool isVisible)
    {
        try
        {
            divReservStateQuotaDDL.Visible = isVisible;
            divReservStateQuotaFile.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation7() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-8
    protected void visibilityOfReservation8(bool isVisible)
    {
        try
        {
            divReservDisabilityAll.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation8() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-9
    //Other Disability
    #endregion

    #region Reservation-10
    protected void visibilityOfReservation10(bool isVisible)
    {
        try
        {
            divNanmamudraFile.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation10() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Reservation-11
    protected void visibilityOfReservation11(bool isVisible)
    {
        try
        {
            divPoliceCadetsFile.Visible = isVisible;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lnkReservationDetails.visibilityOfReservation11() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #endregion
    #region Bind Sports Event Data From Database
    private void BindSportsEventList_DB(DataTable dtDbSports, bool isDisable)
    {
        try
        {

            if (dtDbSports != null && dtDbSports.Rows.Count > 0)
            {
                pnlSportsEvent.Visible = true;
                lvSportsEvent.DataSource = dtDbSports;// ds;
                lvSportsEvent.DataBind();
            }
            else
            {
                pnlSportsEvent.Visible = false;
                lvSportsEvent.DataSource = null;// ds;
                lvSportsEvent.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_AddSubjectTypeSubjectDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    #region Bind Cultural Event Data From Database
    private void BindCulturalEventList_DB(DataTable dtDbCultural, bool isDisable)
    {
        try
        {
            if (dtDbCultural != null && dtDbCultural.Rows.Count > 0)
            {
                pnlCulturalEvent.Visible = true;
                lvCulturalEvent.DataSource = dtDbCultural;// ds;
                lvCulturalEvent.DataBind();
            }
            else
            {
                pnlCulturalEvent.Visible = false;
                lvCulturalEvent.DataSource = null;// ds;
                lvCulturalEvent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_AddSubjectTypeSubjectDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    #region Bind Final Submission Details
    private void BindFinalSubmissionDetails()
    {

        try
        {
            if (ViewState["UserNo"] == null)
            {
                return;
            }
            bool isDisable = true;
            DataSet dsCheckDisable = objCommon.FillDropDown("ACD_USER_PROFILE_STATUS", "USERNO,	PER_BANK_DETAILS,	ADD_DETAILS,	EDU_DETAILS,	PHOTO_DETAILS,	DOCU_DETAILS,	PAY_SETAILS,	CONFIRM_STATUS," +
                                                                                        "OTHER_STATUS,	EXP_DETAILS,	OrganizationId,	DOC_STATUS,	PAY_DETAILS,	PERSONAL_DETAILS,	CONFIRM_DATE,	FEES_DETAILS," +
                                                                                        "APPLIED_PROGRAMME,	RESERVATION_DETAILS,	FINAL_SUBMISSION,	FINAL_SUBMISSION_SOURCESFROM,	FINAL_SUBMISSION_OTHERSOURCES"
                                                                                        , "",
                                                                                        "USERNO =" + Convert.ToInt32(ViewState["UserNo"]), "USERNO");

            if (dsCheckDisable.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dsCheckDisable.Tables[0].Rows[0]["FINAL_SUBMISSION"].ToString()))
                {
                    isDisable = false;
                    divFinalSubmission.Visible = true;
                    setFinalSubmissionValues(dsCheckDisable, isDisable);
                }
                else
                {
                    divFinalSubmission.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FinalSubmission.BindFinalSubmissionDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void setFinalSubmissionValues(DataSet dsCheckDisable, bool isDisable)
    {
        try
        {
            if (!string.IsNullOrEmpty(dsCheckDisable.Tables[0].Rows[0]["FINAL_SUBMISSION_SOURCESFROM"].ToString()))
            {
                setCheckBoxListOfSources(dsCheckDisable.Tables[0].Rows[0]["FINAL_SUBMISSION_SOURCESFROM"].ToString(), isDisable);
            }

            if (!string.IsNullOrEmpty(dsCheckDisable.Tables[0].Rows[0]["FINAL_SUBMISSION_OTHERSOURCES"].ToString()))
            {
                divOtherSources.Visible = true;
                txtOtherSources.Text = dsCheckDisable.Tables[0].Rows[0]["FINAL_SUBMISSION_OTHERSOURCES"].ToString();

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FinalSubmission.setFinalSubmissionValues-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void setCheckBoxListOfSources(string commaSeparatedIDs, bool isDisable)
    {
        try
        {
            divOtherSources.Visible = false;
            chkSourcesFrom.ClearSelection();
            //string commaSeparatedIDs = "1";
            int[] nums = Array.ConvertAll(commaSeparatedIDs.Split(','), int.Parse);

            for (int i = 0; i < nums.Length; i++)
            {
                foreach (ListItem item in chkSourcesFrom.Items)
                {
                    if (nums[i].ToString().Equals(item.Value))
                    {
                        item.Selected = true;
                    }
                    item.Enabled = isDisable;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FinalSubmission.setCheckBoxListOfSources-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    #endregion
    public void disabletextbox()
    {
        txtFullName.Enabled = false;
        txtEmail.Enabled = false;
        txtAlternateEmailId.Enabled = false;
        txtMobile.Enabled = false;
        ddlReligion.Enabled = false;
        ddlSocCategory.Enabled = false;
        ddlBloodGroup.Enabled = false;
        txtIdentificationMark.Enabled = false;
        txtAdhaarNo.Enabled = false;
        ddlDiffAbility.Enabled = false;
        ddlMarital.Enabled = false;
        ddlStateDomicile.Enabled = false;
        ddlSports.Enabled = false;
        ddlLevelOfSports.Enabled = false;
        txtSportsName.Enabled = false;
        txtFatherName.Enabled = false;
        txtFTelNo.Enabled = false;
        txtFMobile.Enabled = false;
        txtFDesignation.Enabled = false;
        ddlFOccupation.Enabled = false;
        txtFEmail.Enabled = false;
        txtMothersName.Enabled = false;
        txtMDesignation.Enabled = false;
        txtMEmail.Enabled = false;
        txtMDesignation.Enabled = false;
        ddlMOccupation.Enabled = false;
        txtMTelNo.Enabled = false;
        txtMMobile.Enabled = false;
        txtCorresAddress.Enabled = false;
        ddlParentsIncome.Enabled = false;

        ddlLCon.Enabled = false;
        ddlLSta.Enabled = false;

        txtLocalPIN.Enabled = false;
        txtPermAddress.Enabled = false;
        ddlPCon.Enabled = false;
        ddlPermanentState.Enabled = false;

        txtPermPIN.Enabled = false;
        txtNatureOfDisability.Enabled = false;
        txtPercentageOfDisability.Enabled = false;
    }
    #endregion
    #region Download pdf report Button Methods
    //protected void btnDownload_Click(object sender, EventArgs e)
    //{
    //    ImageButton btnDownload = sender as ImageButton;

    //    string[] commandArgs = btnDownload.CommandArgument.ToString().Split(new char[] { ',' });
    //    string UserNo = commandArgs[0];
    //    string ApplicationId = commandArgs[1];


    //    //string UserNo = btnDownload.CommandArgument.ToString();

    //    ViewState["UserNo"] = UserNo;
    //    //ViewState["ApplicationId"] = ApplicationId;


    //    if (ViewState["UserNo"] == null)
    //    {
    //        return;
    //    }
    //    int userno = Convert.ToInt32(ViewState["UserNo"]);
    //    Response.Redirect("PrintApplicationFormReport.aspx?userno=" + userno + "");


    //}
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ImageButton btnDownload = sender as ImageButton;

        string[] commandArgs = btnDownload.CommandArgument.ToString().Split(new char[] { ',' });
        string UserNo = commandArgs[0];
        string ApplicationId = commandArgs[1];

        //string UserNo = btnDownload.CommandArgument.ToString();

        ViewState["UserNo"] = UserNo;
        //ViewState["ApplicationId"] = ApplicationId;

        if (ViewState["UserNo"] == null)
        {
            return;
        }
        int userno = Convert.ToInt32(ViewState["UserNo"]);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("")));
        url += "PrintApplicationFormReport.aspx?userno=" + userno + "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


    }
    private void ShowRajagiriAppFormReport(string reportTitle, string rptFileName, int userno, string ApplicationId)
    {
        try
        {
            ShowGeneralReport("~,Reports,Academic," + rptFileName, "@P_USERNO=" + Convert.ToInt32(userno), ApplicationId);

            /*
            string url = "http://localhost:55403/PresentationLayer/";////Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("")));
           
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNO=" + Convert.ToInt32(userno);// +",@P_COLLEGE_CODE=59";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updApplicationProcess, this.updApplicationProcess.GetType(), "controlJSScript", sb.ToString(), true);
             */
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Default2.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    /// For Crystal Report 
    ReportDocument customReport;

    private void ShowGeneralReport(string path, string paramString, string ApplicationId)
    {

        /// Set Report
        customReport = new ReportDocument();
        string reportPath = Server.MapPath(path.Replace(",", "\\"));
        customReport.Load(reportPath);

        /// Assign parameters to report document        
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                /// Each array in val contains string in following format.
                /// ParamName=Value*ReportName
                /// Here report name is the name of report from which this parameter belongs.
                /// if parameter belongs to main report then report name is equal to MainRpt
                /// else if parameter belongs to subreport then report name is equal to name of subreport.
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');

                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";

                paramName = val[i].Substring(0, indexOfEql);

                /// if report name is not passed with the parameter(means indexOfSlash will be -1) then 
                /// handle the scenario to work properly.
                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        /// set login details & db details for report document
        this.ConfigureCrystalReports(customReport);

        /// set login details & db details for each subreport 
        /// inside main report document.
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        ////Export to PDF
        customReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "ApplicationAdmission_" + ApplicationId);


        /*byte[] byteArray = null;
        Stream oStream;
        oStream = (Stream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        byteArray = new byte[oStream.Length];
        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        //Response.BinaryWrite(oStream.ToArray()); 
        Response.BinaryWrite(byteArray);
        Response.End();*/

    }

    private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ////SET Login Details & DB DETAILS
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }
    #endregion

    protected void WhatsappOtp(string Otp, string ToMobileNo, string StudentName, string TemplateName)
    {
        bool success = true;
        //string Account_SID = "HXIN1700894763IN";
        //string api_key = "A2a2b94ce1945b32e4eeb7e784aac9ac8";
        //string API_URL = "https://api.kaleyra.io/v1/" + Account_SID.ToString() + "/messages?";
        //string TemplateName = "otperp";

        string Account_SID = string.Empty;
        string api_key = string.Empty;
        string API_URL = string.Empty;
        string FROM_MOBILENO = string.Empty;

        DataSet ds = new DataSet();
        ds = CommonComponent.WhatsAppConfiguration.GetWhatsAppConfiguration();
        Account_SID = ds.Tables[0].Rows[0]["ACCOUNT_SID"].ToString();
        api_key = ds.Tables[0].Rows[0]["API_KEY"].ToString();
        API_URL = ds.Tables[0].Rows[0]["API_URL"].ToString();
        API_URL = API_URL.Trim() + Account_SID.ToString() + "/messages?";
        FROM_MOBILENO = ds.Tables[0].Rows[0]["FROM_MOBILENO"].ToString();

        try
        {
            //string from = "919645081287";

            using (WebClient client = new WebClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //InfoData payloadObj = new InfoData() { to = "919503244325", from = from, type ="template", channel = "WhatsApp", template_name = "otperp", lang_code = "en", @params = "Roshan,253132" };

                string Para = '"' + StudentName.ToString() + '"' + "," + '"' + Otp.ToString() + '"';
                string myParamet = "from=" + FROM_MOBILENO.ToString() + "&" + "to=" + ToMobileNo.ToString() + "&" + "type=template" + "&" + "channel=WhatsApp" + "&" + "params=" + Para.ToString() + "&template_name=" + TemplateName + "" + "&" + "lang_code=en";

                using (WebClient wc = new WebClient())
                {
                    // wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers["api-key"] = api_key.ToString();
                    wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(API_URL, myParamet);
                }
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
            Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String body = reader.ReadToEnd();
            Console.WriteLine(body);
            success = false;
        }
    }
    public int SendSms(string otp, string templatename,string studentname,string studentmobileno)
    {
        int status = 0;
        try
        {
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;

            DataSet ds = objUC.GetSMSTemplate(0, templatename);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
            }
            string username = studentname.ToString();
            string message = TEMPLATE;
            StringBuilder stringBuilder = new StringBuilder();
            message = message.Replace("{#var#}", username);
            if (otp != "0")
            {
                message = message.Replace("{#var1#}", otp);
            }

            if (studentmobileno != string.Empty)
            {
                SendSMS(studentmobileno, message, TemplateID);
            }
            status = 1;
        }
        catch
        {
            status = 0;
        }
        return status;
    }
    public void SendSMS(string Mobile, string text, string TemplateID)
    {
        string status = string.Empty;
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = string.Empty;
                string uid = string.Empty;
                string pass = string.Empty;
                url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + Mobile + "&TEXT=" + text + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                status = "1";
            }
            else
            {
                status = "0";
            }
        }
        catch
        {
            throw;
        }

    }
   
    protected void lvApplicationProcess_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindListView(0);
    }


    protected void NumberDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataPager1.SetPageProperties(0, DataPager1.PageSize, true);
        DataPager1.PageSize = Convert.ToInt32(NumberDropDown.SelectedValue);
    }
}