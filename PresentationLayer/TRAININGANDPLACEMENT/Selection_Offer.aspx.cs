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

public partial class EXAMINATION_Projects_Selection_Offer : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    StudentController objStud = new StudentController();
    Student objstudent = new Student();
     SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvSelectionOffer.DataSource = ds;
                    lvSelectionOffer.DataBind();
                    hfcount.Value = ds.Tables[0].Rows.Count.ToString();
                    ViewState["roundcount"] = ds.Tables[0].Rows.Count;
                   

                }
                else {
                    lvSelectionOffer.DataSource = null;
                    lvSelectionOffer.DataBind();
                    lvSelectionOffer.Visible = false;
                    objCommon.DisplayMessage(upnlroundselection, "No record found .", this);
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
                            
                            ddlRound.DataSource = dsRound;
                            ddlRound.DataTextField = dsRound.Tables[0].Columns["SELECTNAME"].ToString();
                            ddlRound.DataValueField = dsRound.Tables[0].Columns["SELECTNO"].ToString();
                            ddlRound.DataBind();
                        }

                        //ViewState["Scheduledate"] = txtDate.Text.ToString().Replace("/", "_").Trim();

                    }

                    #region Date Vaidation For RoundName
                    DataSet objds = objCommon.FillDropDown("ACD_TP_COMPSCHEDULE", "INTERVIEWFROM", "INTERVIEWTO", "SCHEDULENO=" + ddlJobAnnouncement.SelectedValue + "", " SCHEDULENO");
                    #endregion
                    BindListViewSelection();
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
                    int Scheduleno=Convert.ToInt32(ddlJobAnnouncement.SelectedValue);
                    int Roundno =Convert.ToInt32(ddlRound.SelectedValue);
                    string Roundname=ddlRound.SelectedItem.Text;
                  
                        DateTime date = DateTime.UtcNow.Date;
                        CheckBox chkround = (item.FindControl("chkRow") as CheckBox);
                        if (chkround.Checked != true)
                    {
                        objCommon.DisplayMessage(upnlroundselection, "Please select Student !", this.Page);
                            return;
                    }

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

                                //objCommon.DisplayMessage(this.Page, "Student Selected for" + SelectRoundname.ToString() + "Round.", this.Page);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + message + "');window.location ='Selection_Offer.aspx';", true);
                                   //+ SelectRoundname + "Round.", this.Page);
                                    ViewState["action"] = null;                                    
                                    //clear();                                                                
                                }
                                if (CS.Equals(CustomStatus.DuplicateRecord))
                                {
                                    objCommon.DisplayMessage(upnlroundselection, "Already Enter Data for This Round Please Select Other Round.", this.Page);
                                    ViewState["action"] = null; 
                                }       
                                }                                
                            }                               
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

                 sendmail(); 
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
                            string filename = fuoffer.FileName.Replace(idno+" ", "_");

                            CustomStatus CS = (CustomStatus)objCompany.UpdateOfferLetter(objTP, scheduleno, Offerdate, JobDiscription, filename,idno);
                            if (CS.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage(upnlroundselection, "Offer Letter Send Successfully.", this.Page);
                                ViewState["action"] = null;
                                clear();
                                templateEditor.Text = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(upnlroundselection, "Please Select Students First.", this.Page);

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
                

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Transactions_Stud_SelectCompany.ddlSchedule_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

          public void   clear()
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
                string Subject = progs[0]+" Offer Letter !!";
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

       
}