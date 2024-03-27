using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using DynamicAL_v2;
//using _NVP;
using System.Collections.Specialized;
using EASendMail;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Ionic.Zip;

//added by amit pandey
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO.Compression;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;


public partial class EXAMINATION_Projects_Selection_Offer : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    StudentController objStud = new StudentController();
    Student objstudent = new Student();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    BlobController objBlob = new BlobController(); //ADDED BY AMIT 28/02/2024
    string flePath; string filePath1; string filePath;

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
                    //Page Authorization
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    //objCommon.FillDropDownList(ddlRound, "ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "", "SELECTNAME");
                    objCommon.FillDropDownList(ddlJobAnnouncement, "ACD_TP_COMPSCHEDULE A INNER JOIN ACD_TP_COMPANY  AS B ON (A.COMPID=B.COMPID)", "A.SCHEDULENO", " CONCAT(COMPNAME,'-',A.INTERVIEWFROM,'-',INTERVIEWTO) as COMPNAME ", "B.COMPREQUEST=2", "A.SCHEDULENO");
                    objCommon.FillDropDownList(ddlJobAnnouncementOffer, " ACD_TP_REGISTER A INNER JOIN ACD_TP_COMPSCHEDULE B ON (A.SCHEDULENO=B.SCHEDULENO) INNER JOIN ACD_TP_COMPANY C ON (C.COMPID=B.COMPID)", "DISTINCT B.SCHEDULENO", "(CONCAT(C.COMPNAME,'-',INTERVIEWFROM,'-',INTERVIEWTO)) as COMPNAME", "INTVSELECT>0", " B.SCHEDULENO");
                    // BindListViewSelection();
                    // BindListViewOfferLetter();

                    //ADDED BY AMIT 28/02/2024
                    BlobDetails();

                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    // string lastroundcount = objCommon.LookUp("ACD_TP_COMPSCHEDULE", "photo", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                    Session["EmailFileAttachemnt"] = null;
                    ViewState["folderPath"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Selection Code Starts from Here
    private void BindListViewSelection()
    {
        try
        {
            int Round = Convert.ToInt32(ddlRound.SelectedValue);
            DataSet ds = objCompany.InterviewSelectBindLV(Convert.ToInt32(ddlJobAnnouncement.SelectedValue), Round);
            lvSelectionOffer.DataSource = null;
            lvSelectionOffer.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSelectionOffer.DataSource = ds;
                lvSelectionOffer.DataBind();
                lvSelectionOffer.Visible = true;
                hfcount.Value = ds.Tables[0].Rows.Count.ToString();
                ViewState["roundcount"] = ds.Tables[0].Rows.Count;


            }
            else
            {
                lvSelectionOffer.DataSource = null;
                lvSelectionOffer.DataBind();
                lvSelectionOffer.Visible = false;
                objCommon.DisplayMessage(upnlroundselection, "No record found .", this);
            }
            foreach (ListViewDataItem dataitem in lvSelectionOffer.Items)
            {
                string Statuss = string.Empty;
                Label Status = dataitem.FindControl("lblStatus") as Label;
                if (Status.Text == "0")
                {
                    Status.Text = "PENDNG";
                }
                else
                {
                    Status.Text = (Status.Text.ToString());
                }
                 Statuss = (Status.Text.ToString());
                if (Statuss == "SELECTED")
                {
                    Status.CssClass = "badge badge-success";
                }
                else
                {
                    Status.CssClass = "badge badge-warning";
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlJobAnnouncement_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            ddlRound.DataSource = null;
            ddlRound.DataBind();
            ddlRound.Items.Clear();
            ddlRound.Items.Add(new ListItem("Please Select", "0"));
            if (ddlJobAnnouncement.SelectedIndex != 0)
            {
                DataSet dsRound = null;
                DataSet ds = objCompany.GetCompSceduleDetail(Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    //string rndCnt = objCommon.LookUp("ACD_TP_STUDENT_SELECTION_PROCESS", "COUNT(SPNO)CNT", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                    //string round = (Convert.ToInt32(rndCnt) + 1).ToString();
                    dsRound = objCompany.GetRoundFromSchedule(Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                    ViewState["roundcount"] = dsRound.Tables[0].Rows.Count;
                    if (dsRound.Tables[0].Rows.Count > 0)
                    {

                        ddlRound.DataSource = dsRound.Tables[0];
                        ddlRound.DataTextField = dsRound.Tables[0].Columns["SELECTNAME"].ToString();
                        ddlRound.DataValueField = dsRound.Tables[0].Columns["SELECTNO"].ToString();
                        ddlRound.DataBind();
                        btnrar.Visible = true;
                    }

                    //ViewState["Scheduledate"] = txtDate.Text.ToString().Replace("/", "_").Trim();

                }

                #region Date Vaidation For RoundName
                DataSet objds = objCommon.FillDropDown("ACD_TP_COMPSCHEDULE", "INTERVIEWFROM", "INTERVIEWTO", "SCHEDULENO=" + ddlJobAnnouncement.SelectedValue + "", " SCHEDULENO");
                #endregion
                BindListViewSelection();
            }
            else
            {
                lvSelectionOffer.DataSource = null;
                lvSelectionOffer.DataBind();
                lvSelectionOffer.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_Stud_SelectCompany.ddlSchedule_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnRoundSelection_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            if (ddlStatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upnlroundselection, "Please Select Status.", this.Page);
            }
            //if (this.lvofferLetter.Items.Count == 0)
            //{
            //    objCommon.DisplayMessage(upnlroundselection,"Please select Student !", this.Page);
            //    return;
            //}
            foreach (ListViewItem item in lvSelectionOffer.Items)
            {
                int Scheduleno = Convert.ToInt32(ddlJobAnnouncement.SelectedValue);
                int Roundno = Convert.ToInt32(ddlRound.SelectedValue);
                string Roundname = ddlRound.SelectedItem.Text;

                DateTime date = DateTime.UtcNow.Date;
                CheckBox chkround = (item.FindControl("chkRow") as CheckBox);
                if (chkround.Checked == true)
                {
                    //    objCommon.DisplayMessage(upnlroundselection, "Please select Student !", this.Page);
                    //        return;
                    //}

                    string idno = chkround.ToolTip.ToString();
                    ViewState["idno"] = idno;
                    if (txtDate.Text != string.Empty)
                    {
                        date = Convert.ToDateTime(txtDate.Text);
                    }
                    DateTime FromDate = Convert.ToDateTime(objCommon.LookUp("ACD_TP_COMPSCHEDULE", "INTERVIEWFROM", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue)));
                    if (Convert.ToDateTime(txtDate.Text) < FromDate)
                    {
                        objCommon.DisplayMessage(upnlroundselection, "Offer Selection Date Should Not Less Than Announce From Date !", this.Page);
                        return;
                    }


                    string collegecode = Convert.ToString(Session["colcode"]);
                    //string lastroundcount = objCommon.LookUp("ACD_TP_COMPSCHEDULE", "RIGHT(SELECTNO,CHARINDEX(',',REVERSE(SELECTNO))-1)", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                    string lastroundcount = objCommon.LookUp("ACD_TP_COMPSCHEDULE", "isnull((CASE WHEN isnulL(SELECTNO,0) like '%,%' THEN RIGHT(SELECTNO,CHARINDEX(',',REVERSE(SELECTNO))-1) ELSE SELECTNO END),0) as SELECTNO", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                    string countroundwise = ddlRound.SelectedValue.ToString();
                    if (lastroundcount == countroundwise)
                    {
                        int Intstatus = 1;
                        int id = Convert.ToInt32(ViewState["idno"]);
                        CustomStatus cs = (CustomStatus)objCompany.UpdateIntSelectStatus(objTP, id, Intstatus, Convert.ToInt32(ddlJobAnnouncement.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //objCommon.DisplayMessage(upnlroundselection, "Student Data Inserted Successfully.", this.Page);
                            //ViewState["action"] = null;
                            //clear();
                            lvSelectionOffer.Visible = false;
                        }
                    }
                    if (ddlStatus.SelectedIndex == 1)
                    {
                        // int Status = Convert.ToInt32(ddlRound.SelectedValue);
                        int Status = Convert.ToInt32(objCommon.LookUp("ACD_TP_SELECTIONTYPE A inner join ACD_TP_COMPSCHEDULE_ROUND B on (B.SELECTNO=A.SELECTNO)", "B.ROUNDS_SEQNO", "B.SCHEDULENO= '" + Convert.ToInt32(ddlJobAnnouncement.SelectedValue) + "' and B.SELECTNO='" + Convert.ToInt32(ddlRound.SelectedValue) + "'"));
                        if (chkround.Checked == true)
                        {
                            CustomStatus CS = (CustomStatus)objCompany.InsertRoundSelectionProcess(objTP, Scheduleno, Roundno, Roundname, idno, date, collegecode, Status);
                            if (CS.Equals(CustomStatus.RecordSaved))
                            {
                                string SelectRoundname = ddlRound.SelectedItem.Text.ToString();
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Student Selected for" + SelectRoundname + "Round.');", true);
                                string message = "Student Selected for " + SelectRoundname.ToString() + " Round.";
                                //string jscript = "<script>alert('Student Selected for" + SelectRoundname + "Round.')</script>";
                                //System.Type t = this.GetType();
                                //ClientScript.RegisterStartupScript(t, "k", jscript);


                                //Response.Write("Student Selected for" + SelectRoundname + "Round.");
                                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Student Selected for" + SelectRoundname + "Round.');", true);
                                objCommon.DisplayMessage(this.Page, message, this.Page);
                                //objCommon.DisplayMessage(this.Page, "Student Selected for" + SelectRoundname.ToString() + "Round.", this.Page);
                               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + message + "');window.location ='Selection_Offer.aspx';", true);
                                //+ SelectRoundname + "Round.", this.Page);
                                ViewState["action"] = null;
                                //clear();             
                                lvofferLetter.Visible = false;
                                BindListViewSelection();                    
                            }
                            if (CS.Equals(CustomStatus.DuplicateRecord))
                            {
                                objCommon.DisplayMessage(upnlroundselection, "Already Enter Data for This Round Please Select Other Round.", this.Page);
                                ViewState["action"] = null;
                            }
                        }
                    }
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(upnlroundselection, "Please select Student !", this.Page);
                return;
            }
            BindListViewSelection();
            lvSelectionOffer.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_Stud_SelectCompany.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion
    #region Offer Letter


    private void BindListViewOfferLetter()
    {
        try
        {
            DataSet ds = objCompany.selectionOfferLetterBindListView(Convert.ToInt32(ddlJobAnnouncementOffer.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvofferLetter.DataSource = ds;
                lvofferLetter.DataBind();
                hfoffer.Value = ds.Tables[0].Rows.Count.ToString();
                lvofferLetter.Visible = true;

            }
            foreach (ListViewDataItem dataitem in lvofferLetter.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "ON-PROGRESS")
                {
                    Status.CssClass = "badge badge-warning";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    protected void btnSubmitOfferLetter_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime FromDate = Convert.ToDateTime(objCommon.LookUp("ACD_TP_COMPSCHEDULE", "INTERVIEWFROM", "SCHEDULENO=" + Convert.ToInt32(ddlJobAnnouncementOffer.SelectedValue)));
            if (Convert.ToDateTime(txtDateOffer.Text) < FromDate)
            {
                objCommon.DisplayMessage(upnlroundselection, "Offer Selection Date Should Not Less Than Announce From Date !", this.Page);
                return;
            }
            sendmail();
            int count = 0;
            if (ddlJobAnnouncementOffer.SelectedValue == "0")
            {
                objCommon.DisplayMessage(unlintselect, "Please Select Job Announcement.", this.Page);
            }
            foreach (ListViewItem item in lvofferLetter.Items)
            {
                int scheduleno = Convert.ToInt32(ddlJobAnnouncementOffer.SelectedValue);
                DateTime Offerdate = Convert.ToDateTime(txtDateOffer.Text);

                //string JobDiscription = "" + Convert.ToString(hfdTemplate.Value).Replace(",", "^") + "," + "";
                CheckBox chkoffer = (item.FindControl("chkRowoffer") as CheckBox);
                string idnos = chkoffer.ToolTip.ToString();
                int idno = Convert.ToInt32(chkoffer.ToolTip);

                // string JobDiscription = "" + Convert.ToString(hfdTemplate.Value).Replace(",", "^") + "," + "";

                string JobDiscription = templateEditor.Text;
                if (chkoffer.Checked == true)
                {
                    if (fuoffer.HasFile)
                    {
                        string ext = System.IO.Path.GetExtension(fuoffer.FileName).ToLower();
                        if (ext == ".pdf")
                        {
                            fuoffer.SaveAs(Server.MapPath("~/ACADEMIC/OfferLetter/" + fuoffer.FileName));
                            string path = Server.MapPath("~/ACADEMIC/OfferLetter/");
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Upload only Pdf files", this.Page);
                            return;
                        }
                        if (fuoffer.FileName.ToString().Length > 50)
                        {
                            objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                            return;
                        }
                        string filename = fuoffer.FileName.Replace(idno + " ", "_");

                        CustomStatus CS = (CustomStatus)objCompany.UpdateOfferLetter(objTP, scheduleno, Offerdate, JobDiscription, filename, idno);
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(upnlroundselection, "Offer Letter Send Successfully.", this.Page);
                            ViewState["action"] = null;
                            clear();
                            templateEditor.Text = string.Empty;
                        }
                    }
                    count++;
                }


            }
            //Convert.ToDateTime(txtDateOffer.Text);



            //if (fuoffer.HasFile)
            //{


            //    string ext = System.IO.Path.GetExtension(fuoffer.FileName).ToLower();
            //    if (ext == ".pdf")
            //    {
            //        fuoffer.SaveAs(Server.MapPath("~/ACADEMIC/OfferLetter/" + fuoffer.FileName));
            //        string path = Server.MapPath("~/ACADEMIC/OfferLetter/");

            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage("Please Upload only Pdf files", this.Page);
            //        return;
            //    }

            //      if (fuoffer.FileName.ToString().Length > 50)
            //    {
            //        objCommon.DisplayMessage("Upload File Name is too long", this.Page);
            //        return;
            //    }

            //    string filename = fuoffer.FileName.Replace(" ", "_");
            //    CustomStatus CS = (CustomStatus)objCompany.UpdateOfferLetter(objTP, scheduleno, Offerdate, JobDiscription, filename);
            //    if (CS.Equals(CustomStatus.RecordUpdated))
            //    {
            //        objCommon.DisplayMessage(upnlroundselection, "Data Saved Successfully.", this.Page);
            //        ViewState["action"] = null;
            //        //clear();
            //    }
            //}
            //if (count == 0)
            //{
            //    objCommon.DisplayMessage(upnlroundselection, "Please Select Students First.", this.Page);

            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_Stud_SelectCompany.ddlSchedule_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void clear()
    {
        ddlJobAnnouncementOffer.SelectedValue = "0";
        txtDateOffer.Text = string.Empty;
        lvofferLetter.DataSource = null;
        lvofferLetter.DataBind();

    }
    protected void ddlJobAnnouncementOffer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewOfferLetter();
    }
    protected void btnCanceltab_Click(object sender, EventArgs e)
    {
        ddlJobAnnouncement.SelectedIndex = 0;
        ddlRound.SelectedIndex = 0;
        txtDate.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        lvSelectionOffer.Visible = false;
        btnrar.Visible = false;

    }
    //protected void btnCancelOfferLetter_Click(object sender, EventArgs e)
    //{
    //    ddlJobAnnouncementOffer.SelectedIndex = 0;
    //    txtDateOffer.Text = string.Empty;
    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "close();", true);
    //}
    protected void btnCancelOffer_Click(object sender, EventArgs e)
    {
        ddlJobAnnouncementOffer.SelectedIndex = 0;
        txtDateOffer.Text = string.Empty;
        lvofferLetter.Visible = false;
        //foreach (ListViewItem item in lvofferLetter.Items)
        //{
        //    CheckBox chkall = (item.FindControl("cbSAllOffer") as CheckBox);
        //    if (chkall.Checked)
        //    {
        //        chkall.Checked = false;
        //    }
        //}
        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "close();", true);
    }
    protected void btnCancelOfferLetter_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "close();", true);

    }



    protected void sendmail()
    {
        try
        {
            string studentIds = string.Empty; string filename = ""; int count = 0;
            string companyname = Convert.ToString(ddlJobAnnouncementOffer.SelectedItem);
            string folderPath = Server.MapPath("~/ACADEMIC/OfferLetter/");
            string[] progs = companyname.Split('-');
            string Subject = progs[0] + " Offer Letter !!";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            ViewState["EmailFileAttachemnt"] = fuoffer.FileName;
            if (Convert.ToString(Session["EmailFileAttachemnt"]) != string.Empty || Convert.ToString(Session["EmailFileAttachemnt"]) != "")
            {
                fuoffer.PostedFile.SaveAs(folderPath + Path.GetFileName(Convert.ToString(Session["EmailFileAttachemnt"])));
                ViewState["folderPath"] = folderPath + Path.GetFileName(Convert.ToString(Session["EmailFileAttachemnt"]));
            }

            DataSet dsUserContact = null;
            //if (rbtodayselect.SelectedValue == "1")
            //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            //else
            //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2830));
            dsUserContact = objCompany.EmailTemplate();

            if (dsUserContact.Tables[0] != null && dsUserContact.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvofferLetter.Items)
                {
                    CheckBox chkround = (item.FindControl("chkRowoffer") as CheckBox);
                    string emailid = chkround.Text;

                    if (chkround.Checked == true)
                    {
                        count++;
                        string message = "";
                        int scheduleno = Convert.ToInt32(ddlJobAnnouncementOffer.SelectedValue);
                        DataSet reff = objCompany.SendOfferLetterEmail(scheduleno);
                        emailid = reff.Tables[0].Rows[0]["EMAILID"].ToString();
                        filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                        int idno = 0;
                        foreach (ListViewItem item1 in lvofferLetter.Items)
                        {
                            CheckBox chkround1 = (item1.FindControl("chkRowoffer") as CheckBox);
                            if (chkround1.Checked == true)
                            {
                                Label lblIdno1 = item1.FindControl("lblIdno1") as Label;
                                idno = Convert.ToInt32(lblIdno1.Text);
                            }

                        }
                        DataSet dsconfig1 = objCommon.FillDropDown("ACD_STUDENT A inner join ACD_TP_REGISTER B on (A.IDNO=B.IDNO)", "A.EMAILID", "B.IDNO", "B.IDNO = '" + idno + "' and A.EMAILID<> ''", "");
                        string ToEmailId = dsconfig1.Tables[0].Rows[0]["EMAILID"].ToString();
                        //DataSet dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_APIKEY", "COMPANY_EMAILSVCID,EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", "");
                        //string API = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
                        //string FromMailId = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
                        //string FromMailPass = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                        //Executes(templateEditor.Text, ToEmailId, "Offer Letter !!", FromMailId, API, "Training And Placement");

                        //------------11-08-2023

                        DataSet dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                        string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                        string path = Server.MapPath("~/ACADEMIC/OfferLetter/");

                        int status1 = 0;
                        string email_type = string.Empty;
                        string Link = string.Empty;
                        int sendmail = 0;

                        message = templateEditor.Text;
                        // string filename = string.Empty;
                        filename = Convert.ToString(ViewState["EmailFileAttachemnt"]);
                        string Imgfile = string.Empty;
                        Byte[] Imgbytes = null;
                        if (filename != string.Empty)
                        {
                            path = path + filename;
                            string LogoPath = path;
                            Imgbytes = File.ReadAllBytes(LogoPath);
                            Imgfile = Convert.ToBase64String(Imgbytes);
                        }
                        status1 = objSendEmail.SendEmail(ToEmailId, message, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");

                        if (status1 == 1)
                        {
                            objCommon.DisplayUserMessage(this.Page, "Mail Sent Successfully.", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Failed To send email", this.Page);
                        }


                        //---------11-08-2023


                    }

                }


            }
            else
            {
                objCommon.DisplayMessage("Email Tamplate Not Found!!!", this.Page);
            }
            if (count == 0)
            {
                objCommon.DisplayMessage("Please select atleast one student for email send !!!", this.Page);

                Session["EmailFileAttachemnt"] = null;
                ViewState["folderPath"] = null;
                return;
            }
            else
            {
                string path = Server.MapPath("~/ACADEMIC/OfferLetter/" + fuoffer.FileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }
                // objCommon.DisplayMessage("Email send Successfully !!!", this.Page);

                Session["EmailFileAttachemnt"] = null;
                ViewState["folderPath"] = null;
            }

        }
        catch (Exception ex)
        {

        }


    }



    private void Executes(string Message, string toEmailId, string Subject, string FromMailId, string APIKey, string TomailSub)
    {
        try
        {
            var fromAddress = new System.Net.Mail.MailAddress(FromMailId, TomailSub);
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            var apiKey = APIKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FromMailId, TomailSub);
            var subject = Subject.ToString();
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;


            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //if (CC_Mail != "")
            //{
            //    string[] CCId = CC_Mail.Split(',');
            //    foreach (string CCEmail in CCId)
            //    {
            //        msg.AddCcs(CCEmail); //mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
            //    }
            //}
            var bytes = File.ReadAllBytes(ViewState["folderPath"].ToString());
            var file = Convert.ToBase64String(bytes);
            //byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(lblAttach.Text));
            msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {

                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content =file,//lblAttach.Text,//Convert.ToBase64String( byteData),
                        Filename = Session["EmailFileAttachemnt"].ToString(),
                        Type = "application/pdf",
                        Disposition = "attachment"

                    }
                };

            var response = client.SendEmailAsync(msg).Result;

        }
        catch (Exception ex)
        {

        }
    }

    protected void Execute(string message, string toSendAddress, string Subject, string ManualMesage, string firstname, string username, string filename, string ReffEmail, string ReffPassword, string APIkey, string FromMailId, string FromMailPass)
    {
        try
        {
            //SmtpMail oMail = new SmtpMail("TryIt");


            //oMail.From = ReffEmail;

            //oMail.To = toSendAddress;

            //oMail.Subject = Subject;

            //oMail.HtmlBody = message;
            ////if (rbtodayselect.SelectedValue == "1")
            ////{
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[MESSAGE]", ManualMesage.ToString());
            ////}
            ////else
            //{
            //    oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", firstname.ToString());
            //}
            //if (filename != string.Empty)
            //{
            //    oMail.AddAttachment(System.Web.HttpContext.Current.Server.MapPath("~/ACADEMIC/OfferLetter/" + filename + ""));
            //}
            //// SmtpServer oServer = new SmtpServer("smtp.live.com");
            //SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

            //// var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            //// var client = new SendGridClient(apiKey);
            //oServer.User = ReffEmail;
            //oServer.Password = ReffPassword;

            //oServer.Port = 587;

            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();


            //oSmtp.SendMail(oServer, oMail);



            //-----------------------------start---------




            DataSet dsconfig = null;
            // dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress("no-reply@deccansociety.org", Subject);
            var toAddress = new System.Net.Mail.MailAddress("sjuned527@gmail.com", "");
            // var apiKey = APIkey;
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply@deccansociety.org", Subject);
            var subject = Subject.ToString();
            var to = new EmailAddress("sjuned527@gmail.com", "");
            var plainTextContent = "test";
            var htmlContent = "test";



            // var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, itsMe<>, subject, plainTextContent, htmlContent);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            //if (fuoffer.FileName != "")
            //{
            //   // var bytes = File.ReadAllBytes(fuoffer.FileName);
            //   // var file = Convert.ToBase64String(bytes);
            //    byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(fuoffer.FileName));
            //    msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //{

            //    new SendGrid.Helpers.Mail.Attachment
            //    {
            //        Content =Convert.ToBase64String( byteData),
            //        Filename = fuoffer.FileName,
            //        Type = "application/pdf",
            //        Disposition = "attachment"

            //    }
            //};
            //}

            var response = client.SendEmailAsync(msg).Result;




            //--------------end----------

            //Common objCommon = new Common();


            //DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "COMPANY_EMAILSVCID,SENDGRID_USERNAME,SENDGRID_PWD,API_KEY_SENDGRID,SLIIT_EMAIL,SLIIT_EMAIL_PWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            ////MasterSoft
            ////var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
            //var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), "SLIIT");
            //var toAddress = new MailAddress(toSendAddress, "");
            ////var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
            //var apiKey = dsconfig.Tables[0].Rows[0]["API_KEY_SENDGRID"].ToString();
            //var client = new SendGridClient(apiKey.ToString());
            ////var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
            //var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), "SLIIT");

            ////Client 
            ////var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");
            ////var toAddress = new MailAddress(toSendAddress, "");
            ////var apiKey = dsconfig.Tables[0].Rows[0]["CLIENT_API_KEY"].ToString();  //"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";
            ////var client = new SendGridClient(apiKey.ToString());
            ////var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["FCGRIDEMAILID"].ToString(), "HSNCU ADMISSION");

            //var subject = Subject;// "Your OTP for Certificate Registration.";
            //var to = new EmailAddress(toSendAddress, "");
            //var plainTextContent = "";
            //var htmlContent = nMailbody;
            //var file = "";
            //if (filename != string.Empty)
            //{
            //    string AttcPath = System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + "");
            //    var bytes = File.ReadAllBytes(AttcPath);
            //    file = Convert.ToBase64String(bytes);
            //}
            //MailMessage msg = new MailMessage();
            //SmtpClient smtp = new SmtpClient();
            //var msgs = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //if (filename != string.Empty)
            //{
            //    msgs.AddAttachment("" + filename + "", file);
            //}
            //var response = await client.SendEmailAsync(msgs);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlRound_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvSelectionOffer;
        int roundno = Convert.ToInt32(ddlRound.SelectedValue);
        DataSet ds = objCompany.InterviewSelectBindLV(Convert.ToInt32(ddlJobAnnouncement.SelectedValue), roundno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvSelectionOffer.DataSource = ds;
            lvSelectionOffer.DataBind();
            hfcount.Value = ds.Tables[0].Rows.Count.ToString();
            ViewState["roundcount"] = ds.Tables[0].Rows.Count;

        }
        foreach (ListViewDataItem dataitem in lvSelectionOffer.Items)
        {
            Label Status = dataitem.FindControl("lblStatus") as Label;
            string Statuss = (Status.Text.ToString());
            if (Statuss == "SELECTED")
            {
                Status.CssClass = "badge badge-success";
            }
            else
            {
                Status.CssClass = "badge badge-warning";
            }
        }
    }

    //ADDED BY AMIT PANDEY 28/02/2024

    #region Blob
    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNametandpdoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    protected void lnkOfferDownload_Click(object sender, EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {
                MessageBox("Document not uploaded");
            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blobpath = dtBlobPic.Rows[0]["Uri"].ToString();
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string Script = string.Empty;

                string DocLink = blobpath;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void lnkOfferDownloadZip_Click(object sender, EventArgs e)
    {
        DataTable dtFilePath1 = new DataTable();
        dtFilePath1.Columns.Add("FilePath", typeof(string));

        DataTable dtResult = (DataTable)null;
        DataTable dtFilePath = new DataTable();
        dtFilePath.Columns.Add("FilePath", typeof(string));
        Session["Name"] = "Downloads";
        //string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
        //string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

        // Add your blob names here
        //  string[] blobNames = { "file1.txt", "file2.txt", "file3.txt" };

        //  DownloadAndZipFiles(blob_ConStr, blob_ContainerName, blobNames);

        //    dtResult.ImportRow(row);
        //}

        int Round = Convert.ToInt32(ddlRound.SelectedValue);
        DataSet ds = objCompany.InterviewSelectBindLV(Convert.ToInt32(ddlJobAnnouncement.SelectedValue), Round);


        dtResult = ds.Tables[0];
        string directoryPath = string.Empty;
        string directoryName = "~/TRAININGANDPLACEMENT\\Resume"; // + "/"
        directoryPath = Server.MapPath(directoryName);

        //if ((System.IO.File.Exists(directoryPath)))
        //{
        //    System.IO.File.Delete(directoryPath);
        //}
        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
        }

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }

        if (dtResult.Rows.Count > 0)
        {
            int count = dtResult.Rows.Count;
            for (int index = 0; index < count; index++)
            {
                //---------------Start----16-03-2024-------------
                string Url = string.Empty;


                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                //     BlobClient blobClient = containerClient.GetBlobClient(blob_ContainerName);
                // Get a reference to the Blob file
                //   BlobClient blobClient = containerClient.GetBlobClient("<blob_name>");
                //   string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
                // string img = Convert.ToString(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "FILE_PATH", "ROUTEID='" + routeid + "' and BUSSTR_ID='" + seating + "'"));
                string img = string.Empty;
                img = ds.Tables[0].Rows[index]["ATTACHMENTS"].ToString();
                var ImageName = img;
                if (img == null || img == "")
                {
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    //ltEmbed.Text = "Image Not Found....!";


                }
                else
                {
                    if (img != "")
                    {
                        DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                        var blob = blobContainer.GetBlockBlobReference(ImageName);

                        string filePath = directoryPath + "\\" + ImageName;
                        dtFilePath1.Rows.Add(filePath);

                        //foreach (DataRow drPhotoImg in ds.Tables[0].Rows)
                        //{
                        int count1 = dtResult.Rows.Count;
                        for (int index1 = 0; index1 < count1; index1++)
                        {
                            string BadPhotoImg = "0";
                            if (index1 >= 0 && index1 < dtBlobPic.Rows.Count)
                            {
                                string Uri = dtBlobPic.Rows[index1]["Uri"].ToString();
                                string Name = dtBlobPic.Rows[index1]["Name"].ToString();
                                //BadPhotoImg = Convert.ToString(drPhotoImg["OnlineRegId"]);
                                //ObjDpsm.PhotoFileName = "" + Convert.ToString(drPhotoImg["OnlineRegId"]) + "_PHOTO.jpg";


                                //string PhotoImgData = Convert.ToString(drPhotoImg["StudentPhoto"]);
                                //PhotoFileName = Convert.ToString(drPhotoImg["OnlineRegId"]);
                                //if (PhotoImgData != null && PhotoImgData != string.Empty)
                                //{
                                using (WebClient PhotoDownloadClient = new WebClient())
                                {
                                    //PhotoDownloadClient.DownloadData(Convert.ToString(drPhotoImg["StudentPhoto"]));
                                    PhotoDownloadClient.DownloadFile(Uri, filePath);

                                }
                            }
                            //}
                        }

                        // string localFilePath = @"C:\MyFolder\MyBlobFile.txt";
                        //using (FileStream downloadFileStream = File.OpenWrite(filePath))
                        //{
                        //  //  blobClient.DownloadTo(downloadFileStream);
                        //    await blob.DownloadToAsync(downloadFileStream);
                        //    fileStream.Close();
                        //}

                        //if ((System.IO.File.Exists(filePath)))
                        //{
                        //    System.IO.File.Delete(filePath);
                        //}
                        // blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                        //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                        //embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
                        //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                        //embed += "</object>";
                        // DownloadFile(Server.MapPath("~/ACADEMIC/Resume/"), ImageName);
                        //string FILENAME = img;
                        //string filePath1 = Server.MapPath("~/ACADEMIC/Resume/" + ImageName);


                        //string filee = Server.MapPath("~/Transactions/TP_PDF_Reader.aspx");
                        //FileInfo file = new FileInfo(filePath1);

                        //if (file.Exists)
                        //{
                        //    Session["sb"] = filePath.ToString();
                        //    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "window.open('"+filee+"'); alert('Your message here' );", true);

                        //    //Response.Redirect("~/TRAININGANDPLACEMENT/Transactions/TP_PDF_Reader.aspx");

                        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

                        //    url += "ACADEMIC/RESUME/" + FILENAME;
                        //    //string url = filePath;
                        //    // added by Gaurav varma 18/10/23
                        //    Response.Clear();
                        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + FILENAME);
                        //    Response.AddHeader("Content-Length", file.Length.ToString());
                        //    Response.ContentType = "application/octet-stream";
                        //    Response.TransmitFile(filePath1);
                        //    // Response.End();
                        //    // end Gaurav varma 18/10/23

                        //    //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                        //    //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        //    //divMsg.InnerHtml += " </script>";

                        //}
                    }
                }

            }


            //----------------END--------16-03-2024--------

            //if (dtResult.Rows[index]["ATTACHMENTS"].ToString() != "" && dtResult.Rows[index]["ATTACHMENTS"].ToString() != null)
            //{
            //    string filename = dtResult.Rows[index]["ATTACHMENTS"].ToString();
            //    Session["Name"] = "Downloads";
            //    filePath1 = getFilePath(filename);
            //    if (filePath1 != null)
            //    {
            //        dtFilePath.Rows.Add(filePath1);
            //    }

            //string text = filep; 
            //string filePath = filep;

            //listItemList.Add(new System.Web.UI.WebControls.ListItem(text, filePath));
            //try
            //{
            //    PdfReader pdfReader2 = new PdfReader(text);

            //    if (pdfReader2.IsOpenedWithFullPermissions)
            //    {
            //        pdfReaderList.Add(pdfReader2);
            //    }
            //}
            //catch (Exception exX)
            //{

            //}

            // }
            //}
            //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            //{
            //    string ua_no = Session["userno"].ToString();
            //    zip.AlternateEncodingUsage = Ionic.Zip.ZipOption.AsNecessary;
            //    string zipName = String.Format("OfferLetter.zip");
            //    string customZipFilePath = @"Desktop:/" + zipName;
            //  //  string directoryPath = "path_to_directory";

            //    if (Directory.Exists(directoryPath))
            //    {
            //        // Add the directory to the ZIP archive
            //        zip.AddDirectory(directoryPath, customZipFilePath);
            //    }

            //    Response.Clear();
            //    Response.BufferOutput = false;
            //    Response.ContentType = "application/zip";
            //    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            //    zip.Save(Response.OutputStream);

            //    Response.End();
            //}


            //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            //{
            //    //string ua_no = Session["userno"].ToString();
                //zip.AlternateEncodingUsage = Ionic.Zip.ZipOption.AsNecessary;
                //string zipName = String.Format("OfferLetter.zip");
                //string customZipFilePath = @"Desktop:/" + zipName;
                //// directoryPath = @"D:\git_rfcomman_code(28-09-2022)\Client13092023\18-09-2023\rf-common-code\PresentationLayer\TRAININGANDPLACEMENT\Resume";

                //if (Directory.Exists(directoryPath))
                //{
                //    // Add the directory to the ZIP archive
                //    zip.AddDirectory(directoryPath, customZipFilePath);
                ////    Ionic.Zip.ZipFile.(FolderPathToZip, ZipFileName);
                //}

                //Response.Clear();
                //Response.BufferOutput = false;
                //Response.ContentType = "application/zip";
                //Response.AddHeader("content-disposition", "attachment; filename=" + zipName);

                //// Save the ZIP archive to the response output stream

                //    zip.Save(Response.OutputStream);
                ////string Script = string.Empty;
                ////Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ////ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

                //Response.End();


            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                string ua_no = Session["userno"].ToString();
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                string zipName = String.Format("offerletter.zip");
                //string customZipFilePath = @"C:\\USA\\" + zipName + "";
                string customZipFilePath = @"D:/Downloads";
                //zip.AddFile(filePath, customZipFilePath);
                count = dtFilePath1.Rows.Count;
                for (int index = 0; index < count; index++)
                {
                    string file_path = dtFilePath1.Rows[index]["FilePath"].ToString();
                    zip.AddFile(file_path, customZipFilePath);
                }
                Response.Clear();
                Response.BufferOutput = false;
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);

                Response.End();
                //zipOutputStream.Close();
                //Response.Flush();
                //Response.Clear();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
               

           // }
        }
        else
        {
            MessageBox("Record Not Found!!");
        }

    }

  
    protected void DownloadAndZipFiles(string connectionString, string containerName, string[] blobNames)
    {
        try
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (string blobName in blobNames)
                    {
                        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobName);
                        using (Stream blobStream = blob.OpenRead())
                        {
                            ZipArchiveEntry entry = archive.CreateEntry(blobName);
                            using (Stream entryStream = entry.Open())
                            {
                                blobStream.CopyTo(entryStream);
                            }
                        }
                    }
                }

                // Send the zip file to the client for download
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=downloadedFiles.zip");
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(Response.OutputStream);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            throw;
        }
    }

    private string getFilePath(string filename)
    {
        string embed = string.Empty;
        //ltEmbed.Text = null;
        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
        string ua_no = Session["userno"].ToString();
        string directoryPath = string.Empty;
        string Link = filename;

        BlobDetails();
        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

        //string directoryName = "~/EmailUploadFile" + "/";
        //directoryPath = Server.MapPath(directoryName);

        //if (!Directory.Exists(directoryPath.ToString()))
        //{

        //    Directory.CreateDirectory(directoryPath.ToString());
        //}

        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string img = filename;

        var ImageName = img;

        if (img == null || img == "")
        {
            MessageBox("Document not uploaded");
        }
        else
        {
            DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
            var blob = blobContainer.GetBlockBlobReference(ImageName);
            //var blobpath = dtBlobPic.Rows[0]["Uri"].ToString();


            //filePath = blobpath;

            //if ((System.IO.File.Exists(filePath)))
            //{
            //    System.IO.File.Delete(filePath);
            //}
            var blobpath = dtBlobPic.Rows[0]["Uri"].ToString();
            //  var blob1 = blobContainer.GetBlockBlobReference(ImageName);
            string Script = string.Empty;

            string DocLink = blobpath;
            string filePath1 = directoryPath + "\\" + ua_no + ImageName;

            filePath = directoryPath + ua_no + ImageName;
            string newfilename = ua_no + time + ImageName;
            hdnfilepath.Value = filePath1;
            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }
            //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
            Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);



            //ltEmbed.Text = string.Format(embed, ResolveUrl("~/EmailUploadFile/" + newfilename));
        }
        return filePath;
    }
}