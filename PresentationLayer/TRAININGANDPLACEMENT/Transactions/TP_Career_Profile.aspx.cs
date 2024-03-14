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
using System.Text;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS.NITPRM;
using System.Web.UI.HtmlControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;



public partial class EXAMINATION_Projects_Career_Profile : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TPTraining objTPT = new TPTraining();
    TrainingPlacement objTP = new TrainingPlacement();
    BlobController objBlob = new BlobController();
    Panel panelfordropdown;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    decimal File_size;
    public string stud_idno = string.Empty;
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    //To Set the MasterPage
    //    if (Session["masterpage"] != null)
    //        objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
    //    else
    //        objCommon.SetMasterPage(Page, "");
    //}
    protected void Page_Load(object sender, EventArgs e)
    
    
    
    {
        try
        {
            if (Request.QueryString["studentId"] != null)   /// Eknath
            { stud_idno = Request.QueryString["studentId"].ToString().Trim(); }
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

                    Page.Title = Session["coll_name"].ToString();


                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    //objCommon.FillDropDownList(ddlExam, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO NOT IN(0) AND QEXAMSTATUS='Q' ", "QUALIFYNO");
                
                    //objCommon.FillDropDownList(ddlSkill, "ACD_TP_SKILLS", "SKILNO", "SKILLS", "STATUS!=0 ", "SKILLS");

                    ViewState["Stud_idno"] = stud_idno;
                    StudentInformation(stud_idno);
                    int idno_iscomf = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO='" + Session["userno"] + "'"));
                    string isconform = objCommon.LookUp("ACD_TP_STUDENT_REGISTRATION", "ConfirmStatus", "IDNO='" + stud_idno + "'");
                    if (isconform=="Y")
                    {
                        chkConfirm.Checked = true;
                        chkConfirm.Enabled = false;
                        btnProceed.Enabled = false;
                    }
                   // pnlPreviousExam.Visible = true;
                 //   btnProceed.Enabled = false;
                    objCommon.FillDropDownList(ddlCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
                    objCommon.FillDropDownList(ddlCompanySector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS=1", "");
                    objCommon.FillDropDownList(ddlJobType, "ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "STATUS=1", "");
                    objCommon.FillDropDownList(ddlCurrency, "ACD_CURRENCY", "CUR_NO", "CUR_NAME", "STATUS=1", "CUR_NAME");

                    //Technical Skills
                      objCommon.FillDropDownList(ddlSkillName, "ACD_TP_SKILLS", "SKILNO", "SKILLS", "STATUS=1", "");
                      objCommon.FillDropDownList(ddlProficiency, "ACD_TP_PROFICIENCY", "PROFNO", "PROFICIENCY", "STATUS=1", "");

                      //Lauguage Tab
                      objCommon.FillDropDownList(ddlLauguage, "ACD_TP_Language", "LANGUAGENO", "LANGUAGES", "STATUS=1", "");
                      objCommon.FillDropDownList(ddlProficiencyLanguage, "ACD_TP_PROFICIENCY", "PROFNO", "PROFICIENCY", "STATUS=1", "");

                     //Awards and Recognitions Tab
                      objCommon.FillDropDownList(ddlLevel, "acd_tp_level", "LEVELNO", "LEVELS", "STATUS=1", "");

                      //Awards and Competitions Tab
                      objCommon.FillDropDownList(ddlLevel1, "acd_tp_level", "LEVELNO", "LEVELS", "STATUS=1", "");
                      objCommon.FillDropDownList(ddlParticipationStatus, "ACD_TP_PARTICIPATION", "PARTICIPATIONNO", "PARTICIPATIONS", "STATUS=1", "");


                      //Awards and Training And WorkShop Tab
                      objCommon.FillDropDownList(ddlCategory, "ACD_TP_CATEGORY", "CATEGORYNO", "CATEGORYS", "STATUS=1", "");
                     // objCommon.FillDropDownList(ddlParticipationStatus, "ACD_TP_PARTICIPATION", "PARTICIPATIONNO", "PARTICIPATIONS", "STATUS=1", "");

                      //Awards and Test Scores Tab
                      objCommon.FillDropDownList(ddlExam, "ACD_TP_EXAM", "EXAMNO", "EXAMS", "STATUS=1", "");
                      objCommon.FillDropDownList(ddlQualificationStatus, "ACD_TP_QUALIFICATION_STATUS", "QUAFNO", "QUALIFICATION", "STATUS=1", "");
                      // objCommon.FillDropDownList(ddlParticipationStatus, "ACD_TP_PARTICIPATION", "PARTICIPATIONNO", "PARTICIPATIONS", "STATUS=1", "");

                    //find idno number
                    //  int idno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO='" + Session["userno"] + "'"));
                      int idno = Convert.ToInt32(stud_idno);
                    //rdoSalary.Checked = false;
                      BindCompDetails(idno);
                      BindSkillDetails(idno);
                    BindProjectDetails(idno);
                    BindCertificationDetails(idno);
                    BindLanguageDetails(idno);
                    BindAwardsDetails(idno);
                    BindCompetitionsDetails(idno);
                    BindTrainingAndWorkshopDetails(idno);
                    BindTestScoresDetails(idno);
                    BlobDetails();
                    //BindTrainingAndWorkshopDetails();
                    BindCompDetails(idno);
                    BindUploasResumeDetails(idno);
                    BindExamDetails(idno);
                }
              
                    
            }

          //  btnProceed.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void StudentInformation(string idno)
    {
        DataSet ds = null;
        //lblMsg.Text = string.Empty;
        User_AccController objUC = new User_AccController();
        TPController objTP = new TPController();
        try
        {

            ds = objTP.GetstudentDetailByRegNo(idno);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            if (ds.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
            {
               btnImage.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";
            }
         
            lblFirstName.Text = ds.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
            lblMiddleName.Text = ds.Tables[0].Rows[0]["STUDMIDDLENAME"].ToString();
            lblLastName.Text = ds.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
            lblDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
            lblGender.Text = ds.Tables[0].Rows[0]["SEX"].ToString();
            lblMobileNo.Text = ds.Tables[0].Rows[0]["PMOBILE"].ToString();
            lblEmailID.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            lblCurrentProgram.Text = ds.Tables[0].Rows[0]["SHORTNAME"].ToString();
            lblSem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
            lbnAddress.Text = ds.Tables[0].Rows[0]["PADDRESS"].ToString();
            lblState.Text = ds.Tables[0].Rows[0]["STATENAME"].ToString();
            lblDistrict.Text = ds.Tables[0].Rows[0]["DISTRICTNAME"].ToString();
            lblTaluka.Text = ds.Tables[0].Rows[0]["TALUKANAME"].ToString();
            lblCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
            lblPinCode.Text = ds.Tables[0].Rows[0]["PPINCODE"].ToString();
            int SEM = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString());



            DataSet dsexam = objTP.GetLastQualificationByIDNo(Convert.ToInt32(Session["idno"]));
            if (dsexam.Tables[0].Rows.Count > 0)
            {
                lvExam.DataSource = dsexam.Tables[0];
                lvExam.DataBind();
                ViewState["qualifyTbl"] = dsexam.Tables[0];
            }



            DataSet ds1 = objTP.GetSemesterHistoryDetails( Convert.ToInt32(Session["idno"]),SEM);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                LVCurrentProgress.DataSource = ds1.Tables[0];
                LVCurrentProgress.DataBind();
                ViewState["qualifyTbl"] = ds1.Tables[0];
            }


            string regno = objCommon.LookUp("ACD_TP_STUDENT_REGISTRATION", "IDNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'");
            hdWorkExp.Value = regno;
            Session["userpreview"] = null;


        }


        catch (Exception ex)
        {
            //lblMsg.Text = " Please Check Your Username Or Password !";

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "TP_Reg_form.btnNext_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }
   
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
           // TPController objTP = new TPController();
            if (ViewState["action"] != null)
            {
               // int idno = Convert.ToInt32(Session["idno"]);
                int idno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "' "));
                //string regno = objCommon.FillDropDown("ACD_STUDENT", "REGNO", "IDNO", "IDNO='" + idno + "' ", "");
                string regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO='" + idno + "'");
                string REGSTATUS = "N";
                string STUDENTTYPE = "R";
                int org = Convert.ToInt32(Session["OrgId"]);
                string confirmStatus = null;
                 if (chkConfirm.Checked == true)
                {
                     confirmStatus = "Y";
                }
                if (ViewState["action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)objCompany.InsStudentBasicDetails(idno, regno, REGSTATUS, STUDENTTYPE, org, confirmStatus);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    }
                    else
                    {
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(this.Page, "Record Update Successfully.", this.Page);
                    }
                }
            }
                   
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Career_Profile.btnProceed_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkConfirm_CheckedChanged(object sender, EventArgs e)
    {
        btnProceed.Enabled = true;
    }




    //protected void btnlinkfirsttab_Click(object sender, EventArgs e)
    //{
    //    btnProceed.Enabled = true;
    //    tab_1.Visible = true;
    //}
    protected void chkCurrentlyWork_CheckedChanged(object sender, EventArgs e)
    {
 
       
        if (chkCurrentlyWork.Checked == true)
        {
            txtEndPeriod.Enabled = false;
            imgDate2.Visible = false;
            txtEndPeriod.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        else
        {
            txtEndPeriod.Enabled = true;
            imgDate2.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        
    }
    protected void rdoSalary_CheckedChanged(object sender, EventArgs e)
    {
        divSalary.Visible = true;
        divCurrency.Visible = true;
        divStipend.Visible = false;
        //divPerAnnum.Visible = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPositionType, "ACD_TP_JOB_ROLE A LEFT JOIN ACD_TP_JOBTYPE B ON (A.JOBNO=B.JOBNO)", "A.ROLENO", "A.JOBROLETYPE", "A.STATUS=1 AND B.JOBNO='" +Convert.ToInt32( ddlJobType.SelectedValue)+ "'", "");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    protected void rdoStipend_CheckedChanged(object sender, EventArgs e)
    {
        divSalary.Visible = false;
        divCurrency.Visible = true;
        divStipend.Visible = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    protected void btnCancelWorkExperience_Click(object sender, EventArgs e)
    {
        clearWorkExperience();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    public void clearWorkExperience()
    {
        ddlCompany.SelectedValue = "0";
        txtJobTitle.Text = string.Empty;
        txtLocation.Text = string.Empty;
        ddlCompanySector.SelectedValue = "0";
        ddlJobType.SelectedValue = "0";
        ddlPositionType.SelectedValue = "0";
        txtDetails.Text = string.Empty;
        txtStartPeriod.Text = string.Empty;
        txtEndPeriod.Text = string.Empty;
        chkCurrentlyWork.Checked = false;
      //  RelevantDocWorkExperience.Text = string.Empty;
        txtSalary.Text = string.Empty;
        TxtStipend.Text = string.Empty;
        ddlCurrency.SelectedValue = "0";
        txtEndPeriod.Enabled = true;
        imgDate2.Visible = true;
        RdSalaeyType.SelectedValue = null;
        divSalary.Visible = false;
        divStipend.Visible = false;
        divCurrency.Visible = false;
        RdWorkType.SelectedValue = null;
        ViewState["action"] = "add";
    }
    protected void btnSubmitWorkExperience_Click(object sender, EventArgs e)
    {
        try
        {

            string WorkType=string.Empty;
            int cmpid;
            string jobtitle;
            string location;
            DateTime StartDate;
            DateTime EndDate;
            TimeSpan Duration;
            int JobSector;
            int JobType;
            int PositionType;
            string WorkSummery;
            int currentlyWorking;
            int SalaryType=0;
            double NrOfDays;
           // int IDNO = Convert.ToInt32(Session["idno"]);
            int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
            int IsAdmin = Convert.ToInt32(Session["userno"]);
            int org = Convert.ToInt32(Session["OrgId"]);
            string RelevantDocument = string.Empty;

            //----------start---23-02-2024

            TPTraining objTpTraining = new TPTraining();
            string file = string.Empty;
            if (FileUploadWorkExp.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUploadWorkExp.FileName)))
                {
                    if (FileUploadWorkExp.HasFile)
                    {
                        if (FileUploadWorkExp.FileContent.Length >= 1024 * 500)
                        {

                            MessageBox("File Size Should Not Be Greater Than 500 kb");
                            FileUploadProject.Dispose();
                            FileUploadProject.Focus();
                            return;
                        }
                    }

                    int req_id = 0;
                    int reqid1 = 0;


                    if (lblBlobConnectiontring.Text == "")
                    {
                        objTpTraining.IsBlob = 0;
                    }
                    else
                    {
                        objTpTraining.IsBlob = 1;
                    }
                    if (objTpTraining.IsBlob == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        // string IdNo = _idnoEmp.ToString();
                        if (FileUploadWorkExp.HasFile)
                        {
                            string contentType = contentType = FileUploadWorkExp.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(FileUploadWorkExp.PostedFile.FileName);
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            string[] split = FileUploadWorkExp.FileName.Split('.');
                            string firstfilename = string.Join(".", split.Take(split.Length - 1));
                            string lastfilename = split.Last();
                            // filename = req_id + "_REQTRNO_" + time + ext;
                            //  objTpTraining.ReleventDocument = firstfilename + "_File_" + time + "." + lastfilename;
                            file = firstfilename + "_" + time + "." + lastfilename;
                            if (FileUploadWorkExp.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, firstfilename + "_" + time + "." + lastfilename, FileUploadWorkExp);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    else
                                    {

                                    }
                                }
                            }



                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.pdf]", this.Page);
                    FileUploadProject.Focus();
                }
            }

            //---------end------23-02-2024

            if (chkCurrentlyWork.Checked)
            {
                currentlyWorking = 1;
            }
            else
            {
                currentlyWorking = 0;
            }
            if (RdWorkType.SelectedValue == "1")
            {
                WorkType = "E";
            }
            else
            {
                WorkType = "I";
            }


            if (txtEndPeriod.Text!=string.Empty)
            {
            if (Convert.ToDateTime(txtStartPeriod.Text) > Convert.ToDateTime(txtEndPeriod.Text))
            {
                objCommon.DisplayMessage(this.Page, "Start date should not be greater than End date.", this.Page);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
                return;
            }
            }
           // cmpid =Convert.ToInt32( ddlCompany.SelectedValue);
            //if (txtStartPeriod.Text != string.Empty & txtEndPeriod.Text != string.Empty)
            //{
           // if (txtStartPeriod.Text != string.Empty)
           // {
                 StartDate = Convert.ToDateTime(txtStartPeriod.Text);
         //   }
           // if (txtEndPeriod.Text != string.Empty)
          ///  {
                // EndDate = Convert.ToDateTime(txtEndPeriod.Text);
                 EndDate = txtEndPeriod.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtEndPeriod.Text.Trim());
                 
            //}
                 if (RdSalaeyType.SelectedValue == "1")
            {

                if (txtSalary.Text==string.Empty)
                     {
                    objCommon.DisplayMessage(this.Page, "Please Enter Salary.", this.Page);
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
                  return;
                     }
                else if (ddlCurrency.SelectedValue=="0")
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Currency.", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
                    return;
                }
            }

                 if (RdSalaeyType.SelectedValue == "2")
                 {

                     if (TxtStipend.Text == string.Empty)
                     {
                         objCommon.DisplayMessage(this.Page, "Please Enter Stipend.", this.Page);
                         ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
                         return;
                     }
                     else if (ddlCurrency.SelectedValue == "0")
                     {
                         objCommon.DisplayMessage(this.Page, "Please Select Currency.", this.Page);
                         ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
                         return;
                     }
                 }

            Duration = EndDate - StartDate;
            
                 NrOfDays = Duration.TotalDays;
            //}
                 if (RdSalaeyType.SelectedValue == "1")
                 {
                     SalaryType = 1;  //Salary For 1
                 }
                 else
                 {
                     SalaryType = 2;  //Stipend for 2
                 }
            decimal Salary ;
            decimal Stipend;
            if (SalaryType==2)
            {
                Stipend = Convert.ToDecimal(TxtStipend.Text);
                Salary = 0;
            }
            else
            {
               Salary =Convert.ToDecimal( txtSalary.Text);
               Stipend = 0;
            }
           //= Convert.ToDouble(TxtStipend.Text);
            //if (TxtStipend.Text == string.Empty)
            //{
            //    Stipend = 0;
            //}
            //else
            //{
            //     Stipend = Convert.ToDecimal(TxtStipend.Text);
            //     Salary = 0;
            //}
            //if (RdSalaeyType.SelectedValue == "1")
            //{

            //}
            //else
            //{
            //}
            int currency =Convert.ToInt32( ddlCurrency.SelectedValue);
             cmpid = Convert.ToInt32(ddlCompany.SelectedValue);
            JobSector=Convert.ToInt32(ddlCompanySector.SelectedValue);
            JobType = Convert.ToInt32(ddlJobType.SelectedValue);
            PositionType = Convert.ToInt32(ddlPositionType.SelectedValue);
            jobtitle = txtJobTitle.Text;
            location = txtLocation.Text;
            WorkSummery = txtDetails.Text;
            RelevantDocument = file;
            if (ViewState["action"].ToString().Equals("add"))
            {

                CustomStatus cs = (CustomStatus)objCompany.InsWorkExperience(IDNO, currentlyWorking, WorkType, SalaryType, Salary, Stipend, currency, cmpid, JobSector, JobType, PositionType, WorkSummery, jobtitle, location, StartDate, EndDate, NrOfDays, RelevantDocument, org, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);

                    BindCompDetails(IDNO);
                    clearWorkExperience();
                }
            }
            else
            {
                if (ViewState["WORKEXPNO"] != null)
                {
                    int WORKEXPNO = Convert.ToInt32(ViewState["WORKEXPNO"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdWorkExperience(WORKEXPNO, currentlyWorking, WorkType, SalaryType, Salary, Stipend, currency, cmpid, JobSector, JobType, PositionType, WorkSummery, jobtitle, location, StartDate, EndDate, NrOfDays, RelevantDocument, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        BindCompDetails(IDNO);
                        clearWorkExperience();
               
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }

    }
    protected void LinkButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int WORKEXPNO = int.Parse(btnEdit.CommandArgument);
            ViewState["WORKEXPNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsWorkExp(WORKEXPNO);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }

    }

    private void ShowDetailsWorkExp(int WORKEXPNO)
    {
        try
        {

            DataSet ds = objCompany.GetIdWorkExp(WORKEXPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                char WExp=Convert.ToChar(ds.Tables[0].Rows[0]["WorkType"].ToString());
                if (WExp == 'E')
                {
                    RdWorkType.SelectedValue = "1";
                }
                else
                {
                    RdWorkType.SelectedValue = "2";
                }
                ddlCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPID"].ToString();
                txtJobTitle.Text = ds.Tables[0].Rows[0]["JOBTITLE"].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0]["CMPLocation"].ToString();
                ddlCompanySector.SelectedValue = ds.Tables[0].Rows[0]["JobSector"].ToString();
                ddlJobType.SelectedValue = ds.Tables[0].Rows[0]["JobType"].ToString();
                objCommon.FillDropDownList(ddlPositionType, "ACD_TP_JOB_ROLE A LEFT JOIN ACD_TP_JOBTYPE B ON (A.JOBNO=B.JOBNO)", "A.ROLENO", "A.JOBROLETYPE", "A.STATUS=1 AND B.JOBNO='" + Convert.ToInt32(ddlJobType.SelectedValue) + "'", "");
                ddlPositionType.SelectedValue = ds.Tables[0].Rows[0]["PositionType"].ToString();
                txtDetails.Text = ds.Tables[0].Rows[0]["WorkSummery"].ToString();

             //   txtStartPeriod.Text = ds.Tables[0].Rows[0]["StartDate"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"].ToString()).ToString("dd-MM-yyyy");

                //DateTime StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]);
                txtStartPeriod.Text =ds.Tables[0].Rows[0]["StartDate"].ToString();
               // txtEndPeriod.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
               // txtEndPeriod.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                //chkCurrentlyWork.Text = ds.Tables[0].Rows[0]["currentlyworking"].ToString();
                int check = Convert.ToInt32(ds.Tables[0].Rows[0]["currentlyworking"].ToString());
                if (check == 1)
                {
                    txtEndPeriod.Enabled = false;
                    imgDate2.Visible = false;
                    chkCurrentlyWork.Checked = true;
                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                }
                else
                {
                    chkCurrentlyWork.Checked = false;
                    txtEndPeriod.Text =ds.Tables[0].Rows[0]["EndDate"].ToString();
                }
                //if (ds.Tables[0].Rows[0]["currentlyworking"].ToString() != null)
                //{
                //    chkCurrentlyWork.Checked = true;
                //}
                //else
                //{
                //    chkCurrentlyWork.Checked = false;
                //}

                //if (Convert.ToInt32(ds.Tables[0].Rows[0]["currentlyworking"]) == 0)
                //{
                //    chkCurrentlyWork.Checked = false;
                //}
                //else
                //{
                //    chkCurrentlyWork.Checked = true;
                //}

            //    RelevantDocWorkExperience.Text = ds.Tables[0].Rows[0]["RelevantDocument"].ToString();
                string salarytype = ds.Tables[0].Rows[0]["SalaryType"].ToString();
                if (salarytype=="2")
                {
                    RdSalaeyType.SelectedValue = "2";
                    divCurrency.Visible = true;
                    divStipend.Visible = true;
                }
                else
                {
                    RdSalaeyType.SelectedValue = "1";
                    divSalary.Visible = true;
                    divCurrency.Visible = true;
                }
                txtSalary.Text = ds.Tables[0].Rows[0]["Salary"].ToString();
                TxtStipend.Text = ds.Tables[0].Rows[0]["Stipend"].ToString();
                ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["currency"].ToString();
                ////  this.fuCollegeLogo = ds.Tables[0].Rows[0]["LOGO"].ToString();
                //objCommon.FillDropDownList(ddlJobSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "", "JOBSECTOR");
                //ddlJobSector.SelectedValue = ds.Tables[0].Rows[0]["JOBSECNO"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void BindCompDetails( int idno)
    {
        try
        {
            DataSet ds = objCompany.BindStuWORKEXPERIENCE(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvWORKEXPERIENCE.DataSource = ds;
                lvWORKEXPERIENCE.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void RdSalaeyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdSalaeyType.SelectedValue == "1")
        {
            divSalary.Visible = true;
            divCurrency.Visible = true;
            divStipend.Visible = false;
            TxtStipend.Text = string.Empty;
        }
        else
        {
            divStipend.Visible = true;
            divCurrency.Visible = true;
            divSalary.Visible = false;
            txtSalary.Text = string.Empty;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);

    }
    protected void btnSubmitTechSkill_Click(object sender, EventArgs e)
    {
        try
        {
           

           
           
         
            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.SkillName =Convert.ToInt32( ddlSkillName.SelectedValue);
                objTPT.Proficiency = Convert.ToInt32(ddlProficiency.SelectedValue);
                objTPT.ReleventDocument = RelevantDocTechSkill.Text;

                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);

            //    int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                CustomStatus cs = (CustomStatus)objCompany.InsTechnicalSkill(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    BindSkillDetails(IDNO);
                    ddlSkillName.SelectedValue = "0";
                    ddlProficiency.SelectedValue = "0";
                }
            }
            else
            {
                if (ViewState["TechSkill"] != null)
                {
                    objTPT.SkillName = Convert.ToInt32(ddlSkillName.SelectedValue);
                    objTPT.Proficiency = Convert.ToInt32(ddlProficiency.SelectedValue);
                    objTPT.ReleventDocument = RelevantDocTechSkill.Text;
                    int id = Convert.ToInt32(ViewState["TechSkill"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                    int IDNO = 0;
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                   // int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                    CustomStatus cs = (CustomStatus)objCompany.InsTechnicalSkill(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        BindSkillDetails(IDNO1);
                        ddlSkillName.SelectedValue = "0";
                        ddlProficiency.SelectedValue = "0";
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
        }
    }

    protected void BindSkillDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindTechnicalSkill(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvtechSkill.DataSource = ds;
                lvtechSkill.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void LinkButton2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int TechSkill = int.Parse(btnEdit.CommandArgument);
            ViewState["TechSkill"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsTechSkill(TechSkill);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
        }
    }


    private void ShowDetailsTechSkill(int TechSkill)
    {
        try
        {

            DataSet ds = objCompany.GetIdTechSkill(TechSkill);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlSkillName.SelectedValue = ds.Tables[0].Rows[0]["SKILNO"].ToString();
                ddlProficiency.SelectedValue = ds.Tables[0].Rows[0]["PROFNO"].ToString();
                RelevantDocTechSkill.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();
                
              

               

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelTechSkill_Click(object sender, EventArgs e)
    {
        ddlProficiency.SelectedValue = "0";
        ddlSkillName.SelectedValue = "0";
        ddlProficiency.SelectedValue = "0";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSubmitProject_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this.Page, "Start date should not be greater than End date.", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
                    return;
                }
            }

            //----------start---23-02-2024

            TPTraining objTpTraining = new TPTraining();
            string file = string.Empty;
            if (FileUploadProject.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUploadProject.FileName)))
                {
                    if (FileUploadProject.HasFile)
                    {
                        if (FileUploadProject.FileContent.Length >= 1024 * 500)
                        {

                            MessageBox("File Size Should Not Be Greater Than 500 kb");
                            FileUploadProject.Dispose();
                            FileUploadProject.Focus();
                            return;
                        }
                    }

                    int req_id = 0;
                    int reqid1 = 0;


                    if (lblBlobConnectiontring.Text == "")
                    {
                        objTpTraining.IsBlob = 0;
                    }
                    else
                    {
                        objTpTraining.IsBlob = 1;
                    }
                    if (objTpTraining.IsBlob == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        // string IdNo = _idnoEmp.ToString();
                        if (FileUploadProject.HasFile)
                        {
                            string contentType = contentType = FileUploadProject.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(FileUploadProject.PostedFile.FileName);
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            string[] split = FileUploadProject.FileName.Split('.');
                            string firstfilename = string.Join(".", split.Take(split.Length - 1));
                            string lastfilename = split.Last();
                            // filename = req_id + "_REQTRNO_" + time + ext;
                            //  objTpTraining.ReleventDocument = firstfilename + "_File_" + time + "." + lastfilename;
                            file = firstfilename + "_" + time + "." + lastfilename;
                            if (FileUploadProject.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, firstfilename + "_" + time + "." + lastfilename, FileUploadProject);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    else
                                    {

                                    }
                                }
                            }



                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.pdf]", this.Page);
                    FileUploadProject.Focus();
                }
            }

            //---------end------23-02-2024

            if (ViewState["action"].ToString().Equals("add"))
            {
               

                int id = 0;
                objTPT.ProjectTitle=txtProjectTitle.Text;
                objTPT.ProjectDomian=txtProjectDomian.Text;
                objTPT.GuideSupervissorName=txtSupervisorName.Text;
                objTPT.StartDate=Convert.ToDateTime(txtStartDate.Text);
                objTPT.EndDate = txtEndDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtEndDate.Text.Trim());

               // objTPT.EndDate=Convert.ToDateTime(txtEndDate.Text);
                if (chcurrwntlywork.Checked == true)
                {
                   objTPT.CurrentlyWork=1;
                }
                else
                {
                    objTPT.CurrentlyWork=0;
                }
                objTPT.Descripition = txtDescription.Text;

                objTPT.ReleventDocument1 = file;
                objTPT.Hr = txtHr.Text.Trim().Equals(string.Empty) ? string.Empty : txtHr.Text.Trim().ToString();
                objTPT.CompLoc = txtCompLoc.Text.Trim().Equals(string.Empty) ? string.Empty : txtCompLoc.Text.Trim().ToString();
             //   int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);
                CustomStatus cs = (CustomStatus)objCompany.InsUpdProject(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clear();
                    BindProjectDetails(IDNO);
                    
                }
            }
            else
            {
                if (ViewState["Project"] != null)
                {
                    int id =Convert.ToInt32( ViewState["Project"]);
                    objTPT.ProjectTitle = txtProjectTitle.Text;
                    objTPT.ProjectDomian = txtProjectDomian.Text;
                    objTPT.GuideSupervissorName = txtSupervisorName.Text;
                    objTPT.StartDate = Convert.ToDateTime(txtStartDate.Text);
                    //objTPT.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    objTPT.EndDate = txtEndDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtEndDate.Text.Trim());

                    if (chcurrwntlywork.Checked == true)
                    {
                        objTPT.CurrentlyWork = 1;
                    }
                    else
                    {
                        objTPT.CurrentlyWork = 0;
                    }
                    objTPT.Descripition = txtDescription.Text;

                    objTPT.ReleventDocument1 = file;
                    objTPT.Hr = txtHr.Text.Trim().Equals(string.Empty) ? string.Empty : txtHr.Text.Trim().ToString();
                    objTPT.CompLoc = txtCompLoc.Text.Trim().Equals(string.Empty) ? string.Empty : txtCompLoc.Text.Trim().ToString();
                    int IDNO =0;
                   // int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                   // int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdProjects(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clear();
                      //  int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); 
                        int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                        BindProjectDetails(IDNO1);
                        
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
    }

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".pdf", ".PDF"};
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }  

    protected void chcurrwntlywork_CheckedChanged(object sender, EventArgs e)
    {
        if (chcurrwntlywork.Checked == true)
        {
            txtEndDate.Enabled = false;
            Div2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        else
        {
            txtEndDate.Enabled = true;
            Div2.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
    }
    protected void btnCancelProject_Click(object sender, EventArgs e)
    {
        clear();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
    }
    public void clear()
    {
        txtProjectTitle.Text = string.Empty;
        txtProjectDomian.Text = string.Empty;
        txtSupervisorName.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        chcurrwntlywork.Checked = false;
        txtDescription.Text = string.Empty;
        //RelevantDocProject.Text = string.Empty;
        txtEndDate.Enabled = true;
        Div2.Visible = true;
        txtHr.Text = string.Empty;
        txtCompLoc.Text = string.Empty;
    }

    protected void BindProjectDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindProjects(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvProject.DataSource = ds;
                lvProject.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEditProject_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Project = int.Parse(btnEdit.CommandArgument);
            ViewState["Project"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsProject(Project);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
    }

    private void ShowDetailsProject(int Project)
    {
        try
        {

            DataSet ds = objCompany.GetIdProject(Project);
            if (ds.Tables[0].Rows.Count > 0)
            {

           

                txtProjectTitle.Text = ds.Tables[0].Rows[0]["PROJECT_TITLE"].ToString();
                txtProjectDomian.Text = ds.Tables[0].Rows[0]["PROJECT_DOMIAN"].ToString();
                txtSupervisorName.Text = ds.Tables[0].Rows[0]["GUIDE_SUPERVISOR_NAME"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["START_DATE"].ToString();
                int check =Convert.ToInt32( ds.Tables[0].Rows[0]["CURRENTLY_WORK"].ToString());
                if (check == 1)
                {
                    txtEndDate.Enabled = false;
                    Div2.Visible = false;
                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                }
                else
                {
                    txtEndDate.Enabled = true;
                    Div2.Visible = true;
                    txtEndDate.Text = ds.Tables[0].Rows[0]["END_DATE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CURRENTLY_WORK"].ToString() == "0")
                {
                    chcurrwntlywork.Checked = false;
                }
                else
                {
                    chcurrwntlywork.Checked = true;
                }
                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                txtHr.Text = ds.Tables[0].Rows[0]["HR"].ToString();
                txtCompLoc.Text = ds.Tables[0].Rows[0]["CompanyLocation"].ToString();
             //   RelevantDocProject.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitCertification_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtToDate.Text != string.Empty)
            {

                if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                {
                    objCommon.DisplayMessage(this.Page, "From date should not be greater than To date.", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
                    return;
                }
            }

            //----------start---23-02-2024

            TPTraining objTpTraining = new TPTraining();
            string file = string.Empty;
            if (FileUploadCertification.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUploadCertification.FileName)))
                {
                    if (FileUploadCertification.HasFile)
                    {
                        if (FileUploadCertification.FileContent.Length >= 1024 * 500)
                        {

                            MessageBox("File Size Should Not Be Greater Than 500 kb");
                            FileUploadCertification.Dispose();
                            FileUploadCertification.Focus();
                            return;
                        }
                    }

                    int req_id = 0;
                    int reqid1 = 0;


                    if (lblBlobConnectiontring.Text == "")
                    {
                        objTpTraining.IsBlob = 0;
                    }
                    else
                    {
                        objTpTraining.IsBlob = 1;
                    }
                    if (objTpTraining.IsBlob == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        // string IdNo = _idnoEmp.ToString();
                        if (FileUploadCertification.HasFile)
                        {
                            string contentType = contentType = FileUploadCertification.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(FileUploadCertification.PostedFile.FileName);
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            string[] split = FileUploadCertification.FileName.Split('.');
                            string firstfilename = string.Join(".", split.Take(split.Length - 1));
                            string lastfilename = split.Last();
                            // filename = req_id + "_REQTRNO_" + time + ext;
                            //  objTpTraining.ReleventDocument = firstfilename + "_File_" + time + "." + lastfilename;
                            file = firstfilename + "_" + time + "." + lastfilename;
                            if (FileUploadCertification.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, firstfilename + "_" + time + "." + lastfilename, FileUploadCertification);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    else
                                    {

                                    }
                                }
                            }



                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.pdf]", this.Page);
                    FileUploadProject.Focus();
                }
            }

            //---------end------23-02-2024

            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.Title = txtTitle.Text;
                objTPT.CertifiedBy = txtCertifiedBy.Text;
                objTPT.Grade = txtGrade.Text;
                objTPT.FromDate = Convert.ToDateTime(txtFromDate.Text);
               // objTPT.ToDate = Convert.ToDateTime(txtToDate.Text);
                objTPT.ToDate = txtToDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text.Trim());
                if (chkCertification.Checked == true)
                {
                    objTPT.CurrentlyWork1 = 1;
                }
                else
                {
                    objTPT.CurrentlyWork1 = 0;
                }

                objTPT.ReleventDocument2 = file;

             //   int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);
                CustomStatus cs = (CustomStatus)objCompany.InsUpdCertificate(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clearCrt();
                    BindCertificationDetails(IDNO);

                }
            }
            else
            {
                if (ViewState["Certification"] != null)
                {
                    int id = 0;
                    id = Convert.ToInt32(ViewState["Certification"]);
                    objTPT.Title = txtTitle.Text;
                    objTPT.CertifiedBy = txtCertifiedBy.Text;
                    objTPT.Grade = txtGrade.Text;
                    objTPT.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    //objTPT.ToDate = Convert.ToDateTime(txtToDate.Text);
                    objTPT.ToDate = txtToDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text.Trim());
                    if (chkCertification.Checked == true)
                    {
                        objTPT.CurrentlyWork1 = 1;
                    }
                    else
                    {
                        objTPT.CurrentlyWork1 = 0;
                    }

                    objTPT.ReleventDocument2 = file;
                    int IDNO = 0;
                    // IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                     IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdCertification(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clearCrt();
                       // int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                        int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                        BindCertificationDetails(IDNO1);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
    }

    public void clearCrt()
    {
        txtTitle.Text = string.Empty;
        txtCertifiedBy.Text = string.Empty;
        txtGrade.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        chkCertification.Checked = false;
        //RelevantDocCertification.Text = string.Empty;
        txtToDate.Enabled = true;
        Div4.Visible = true;
    }
    protected void BindCertificationDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindCertificat(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvcertfication.DataSource = ds;
                lvcertfication.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnEditCertificat_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Certification = int.Parse(btnEdit.CommandArgument);
            ViewState["Certification"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsCertification(Certification);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
    }

    private void ShowDetailsCertification(int Certification)
    {
        try
        {

            DataSet ds = objCompany.GetIdCertification(Certification);
            if (ds.Tables[0].Rows.Count > 0)
            {



                txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                txtCertifiedBy.Text = ds.Tables[0].Rows[0]["CERTIFIED_BY"].ToString();
                txtGrade.Text = ds.Tables[0].Rows[0]["GRADE"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();

                if (ds.Tables[0].Rows[0]["CURRENTLY_WORK"].ToString() == "0")
                {
                    chkCertification.Checked = false;
                }
                else
                {
                    chkCertification.Checked = true;
                }
                int check1 = Convert.ToInt32(ds.Tables[0].Rows[0]["CURRENTLY_WORK"].ToString());
                if (check1 == 1)
                {
                    txtToDate.Enabled = false;
                    Div4.Visible = false;
                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
                }
                else
                {
                    txtToDate.Enabled = true;
                    Div4.Visible = true;
                    txtToDate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                }
                //RelevantDocCertification.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelCertification_Click(object sender, EventArgs e)
    {
        clearCrt();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
    }



    protected void btnSubmitLanguage_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.language =Convert.ToInt32( ddlLauguage.SelectedValue);
                objTPT.Proficiency =Convert.ToInt32( ddlProficiencyLanguage.SelectedValue);
                objTPT.ReleventDocument3 = RelevantDocLanguage.Text;
                //added by amit pandey
                if (chkReadLang.Checked)
                {
                    objTPT.Read = 1;
                }
                if (chkWriteLang.Checked)
                {
                    objTPT.Write = 1;
                }
                if (chkSpeakLang.Checked)
                {
                    objTPT.Speak = 1;
                }

               // int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);
                CustomStatus cs = (CustomStatus)objCompany.InsLanguage(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clearLang();
                    BindLanguageDetails(IDNO);

                }
            }
            else
            {
                if (ViewState["Language"] != null)
                {
                    int id = 0;
                    id = Convert.ToInt32(ViewState["Language"]);
                    objTPT.language = Convert.ToInt32(ddlLauguage.SelectedValue);
                    objTPT.Proficiency = Convert.ToInt32(ddlProficiencyLanguage.SelectedValue);
                    objTPT.ReleventDocument3 = RelevantDocLanguage.Text;
                    //added by amit pandey
                    if (chkReadLang.Checked)
                    {
                        objTPT.Read = 1;
                    }
                    if (chkWriteLang.Checked)
                    {
                        objTPT.Write = 1;
                    }
                    if (chkSpeakLang.Checked)
                    {
                        objTPT.Speak = 1;
                    }

                    int IDNO = 0;
                   // IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                     IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdLanguage(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clearLang();
                        //int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                        int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                        BindLanguageDetails(IDNO1);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
        }
    }
    public void clearLang()
    {
        ddlLauguage.SelectedValue = "0";
        ddlProficiencyLanguage.SelectedValue = "0";
        RelevantDocLanguage.Text = string.Empty;
        chkReadLang.Checked = false;
        chkSpeakLang.Checked = false;
        chkWriteLang.Checked = false;
    }
    protected void BindLanguageDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindLAnguage(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvLanguage.DataSource = ds;
                lvLanguage.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnEditLanguage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            clearLang();
            int Language = int.Parse(btnEdit.CommandArgument);
            ViewState["Language"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsLanguage(Language);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
        }
    }

    private void ShowDetailsLanguage(int Language)
    {
        try
        {

            DataSet ds = objCompany.GetIdLanguage(Language);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlLauguage.SelectedValue = ds.Tables[0].Rows[0]["LAUGUAGE"].ToString();
                ddlProficiencyLanguage.SelectedValue = ds.Tables[0].Rows[0]["Proficiency"].ToString();
                RelevantDocLanguage.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();
                //added by amit pandey
                if (ds.Tables[0].Rows[0]["Read"].ToString() == "True")
                {
                    chkReadLang.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["Write"].ToString() == "True")
                {
                    chkWriteLang.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["Speak"].ToString() == "True")
                {
                    chkSpeakLang.Checked = true;
                }


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancelLanguage_Click(object sender, EventArgs e)
    {
        clearLang();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
    }
    protected void btnSubmitAward_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtAwardTitle.Text==string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Award Title.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
                    return;
              
            }
            if (txtAwardDate.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Date of Award.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
                return;

            }
            if (txtGivenBy.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Given By.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
                return;

            }
            if (ddlLevel.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Level.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
                return;

            }


            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.Award_Title =txtAwardTitle.Text;
                objTPT.Date_of_Award =Convert.ToDateTime( txtAwardDate.Text);
                  objTPT.Given_By = txtGivenBy.Text;
                  objTPT.Level = Convert.ToInt32(ddlLevel.SelectedValue);
                objTPT.ReleventDocument4 = RelevantDocAward.Text;
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);

              //  int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                CustomStatus cs = (CustomStatus)objCompany.InsAWARDS_RECOGNITIONS(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clearAward();
                    BindAwardsDetails(IDNO);
                }
            }
            else
            {
                if (ViewState["AR_ID"] != null)
                {
                    int id = 0;
                    id = Convert.ToInt32(ViewState["AR_ID"]);
                    objTPT.Award_Title = txtAwardTitle.Text;
                    objTPT.Date_of_Award = Convert.ToDateTime(txtAwardDate.Text);
                    objTPT.Given_By = txtGivenBy.Text;
                    objTPT.Level = Convert.ToInt32(ddlLevel.SelectedValue);
                    objTPT.ReleventDocument4 = RelevantDocAward.Text;
                    int IDNO = 0;
                   // IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                    IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdAWARDS_RECOGNITIONS(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clearAward();
                        //int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                        int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                        BindAwardsDetails(IDNO1);
                        ViewState["AR_ID"] = null;

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
        }
    }

    public void clearAward()
    {
        txtAwardTitle.Text = string.Empty;
        txtAwardDate.Text =string.Empty;
        txtGivenBy.Text = string.Empty;
        ddlLevel.SelectedValue = "0";
        RelevantDocAward.Text = string.Empty;
        ViewState["AR_ID"] = null;
    }
    protected void BindAwardsDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindAWARD(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAWARD.DataSource = ds;
                lvAWARD.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnEditAward_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int AR_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["AR_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsAward(AR_ID);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
        }
    }

    private void ShowDetailsAward(int AR_ID)
    {
        try
        {

            DataSet ds = objCompany.GetIdAward(AR_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {



                txtAwardTitle.Text = ds.Tables[0].Rows[0]["AWARD_TITLE"].ToString();
                txtAwardDate.Text = ds.Tables[0].Rows[0]["DATE_OF_AWARD"].ToString();
                txtGivenBy.Text = ds.Tables[0].Rows[0]["GIVEN_BY"].ToString();
                ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["LEVEL"].ToString();
                RelevantDocAward.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitCompetition_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtToDateCompetition.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtFromDateCompetition.Text) > Convert.ToDateTime(txtToDateCompetition.Text))
                {
                    objCommon.DisplayMessage(this.Page, "From date should not be greater than To date.", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
                    return;
                }
            }



            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.Competition_Title = txtCompetitionTitle.Text;
                objTPT.Organized_By = txtOrganizedBy.Text;
                objTPT.Level1 = Convert.ToInt32(ddlLevel1.SelectedValue);
                objTPT.From_Date = Convert.ToDateTime(txtFromDateCompetition.Text);
                objTPT.To_Date = Convert.ToDateTime(txtToDateCompetition.Text);
                objTPT.Project_Title = txtProjectTitleCompetition.Text;
                objTPT.Participation_Status = Convert.ToInt32(ddlParticipationStatus.SelectedValue);
                objTPT.ReleventDocument5 = RelevantDocCompetition.Text;
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);
               // int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                CustomStatus cs = (CustomStatus)objCompany.InsCompetitions(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clearCompetitions();
                    BindCompetitionsDetails(IDNO);
                }
            }
            else
            {
                if (ViewState["CP_ID"] != null)
                {
                    int id = 0;
                    id = Convert.ToInt32(ViewState["CP_ID"]);
                    objTPT.Competition_Title = txtCompetitionTitle.Text;
                    objTPT.Organized_By = txtOrganizedBy.Text;
                    objTPT.Level1 = Convert.ToInt32(ddlLevel1.SelectedValue);
                    objTPT.From_Date = Convert.ToDateTime(txtFromDateCompetition.Text);
                    objTPT.To_Date = Convert.ToDateTime(txtToDateCompetition.Text);
                    objTPT.Project_Title = txtProjectTitleCompetition.Text;
                    objTPT.Participation_Status = Convert.ToInt32(ddlParticipationStatus.SelectedValue);
                    objTPT.ReleventDocument5 = RelevantDocCompetition.Text;
                    int IDNO = 0;
                    //IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                    IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdCOMPETITIONS(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clearCompetitions();
                       // int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                        int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                        BindCompetitionsDetails(IDNO1);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
        }
    }

    public void clearCompetitions()
    {
        txtCompetitionTitle.Text=string.Empty;
        txtOrganizedBy.Text=string.Empty;
        ddlLevel1.SelectedValue="0";
        txtFromDateCompetition.Text=string.Empty;
        txtToDateCompetition.Text=string.Empty;
        txtProjectTitleCompetition.Text=string.Empty;
        ddlParticipationStatus.SelectedValue="0";
        RelevantDocCompetition.Text=string.Empty;
        ViewState["CP_ID"] = null;
    
    }
    protected void btnCancelCompetition_Click(object sender, EventArgs e)
    {
        clearCompetitions();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
    }

    protected void BindCompetitionsDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindCompetitions(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lVCompetition.DataSource = ds;
                lVCompetition.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEditCompetition_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CP_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["CP_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsCompetition(CP_ID);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
        }
    }


    private void ShowDetailsCompetition(int CP_ID)
    {
        try
        {

            DataSet ds = objCompany.GetIdCompetition(CP_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {



                txtCompetitionTitle.Text = ds.Tables[0].Rows[0]["COMPETITION_TITLE"].ToString();
                txtOrganizedBy.Text = ds.Tables[0].Rows[0]["ORGANIZED_BY"].ToString();
                ddlLevel1.SelectedValue = ds.Tables[0].Rows[0]["Level"].ToString();
                txtFromDateCompetition.Text = ds.Tables[0].Rows[0]["From_Date"].ToString();
                txtToDateCompetition.Text = ds.Tables[0].Rows[0]["To_Date"].ToString();
                txtProjectTitleCompetition.Text = ds.Tables[0].Rows[0]["Project_Title"].ToString();
                ddlParticipationStatus.SelectedValue = ds.Tables[0].Rows[0]["Participation_Status"].ToString();
                RelevantDocCompetition.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitTraining_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtToDateTraining.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtFromDateTraining.Text) > Convert.ToDateTime(txtToDateTraining.Text))
                {
                    objCommon.DisplayMessage(this.Page, "From date should not be greater than To date.", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
                    return;
                }
            }

            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.Training_Title = txtTrainingTitle.Text;
                objTPT.Organized_By1 = txtOrganized.Text;
                objTPT.Category = Convert.ToInt32(ddlCategory.SelectedValue);
                objTPT.From_Date1 = Convert.ToDateTime(txtFromDateTraining.Text);
                objTPT.To_Date1 = Convert.ToDateTime(txtToDateTraining.Text);
                objTPT.ReleventDocument6 = RelevantDocTraining.Text;
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);
             //   int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                CustomStatus cs = (CustomStatus)objCompany.InsTrainingAndWorkshop(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clearTrainingAndWorkshop();
                    BindTrainingAndWorkshopDetails(IDNO);
                }
            }
            else
            {
                if (ViewState["TW_ID"] != null)
                {
                    int id = 0;
                    id = Convert.ToInt32(ViewState["TW_ID"]);
                    objTPT.Training_Title = txtTrainingTitle.Text;
                    objTPT.Organized_By1 = txtOrganized.Text;
                    objTPT.Category = Convert.ToInt32(ddlCategory.SelectedValue);
                    objTPT.From_Date1 = Convert.ToDateTime(txtFromDateTraining.Text);
                    objTPT.To_Date1 = Convert.ToDateTime(txtToDateTraining.Text);
                    objTPT.ReleventDocument5 = RelevantDocTraining.Text;
                    int IDNO = 0;
                    IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                 //   IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);
                    CustomStatus cs = (CustomStatus)objCompany.UpdTrainingAndWorkshop(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clearTrainingAndWorkshop();
                        //int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                        int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                        BindTrainingAndWorkshopDetails(IDNO1);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
        }
    }

    public void clearTrainingAndWorkshop()
    {
        txtTrainingTitle.Text = string.Empty;
        txtOrganized.Text = string.Empty;
        ddlCategory.SelectedValue = "0";
        txtFromDateTraining.Text = string.Empty;
        txtToDateTraining.Text = string.Empty;
        RelevantDocTraining.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["TW_ID"] = null;
    }
    protected void btnCancelTraining_Click(object sender, EventArgs e)
    {
        clearTrainingAndWorkshop();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
    }
    protected void BindTrainingAndWorkshopDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindTrainings(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTrainingAndPlacement.DataSource = ds;
                lvTrainingAndPlacement.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEditTraining_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int TW_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["TW_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsTraining(TW_ID);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
        }
    }

    private void ShowDetailsTraining(int TW_ID)
    {
        try
        {

            DataSet ds = objCompany.GetIdTraining(TW_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {



                txtTrainingTitle.Text = ds.Tables[0].Rows[0]["TRAINING_TITLE"].ToString();
                txtOrganized.Text = ds.Tables[0].Rows[0]["ORGANIZED_BY"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                txtFromDateTraining.Text = ds.Tables[0].Rows[0]["From_Date"].ToString();
                txtToDateTraining.Text = ds.Tables[0].Rows[0]["To_Date"].ToString();
                RelevantDocTraining.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitScore_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (ddlExam.SelectedValue=="0")
            {
                objCommon.DisplayMessage("Please Select Exam.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
                return;
            }
            if (ddlQualificationStatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Qualification Status.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
                return;
            }
            if (txtyear.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Year.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
                return;
            }
            if (txtTestScore.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Test Score/Grade.", this);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
                return;
            }
            string DOCFOLDER = file_path + "TRAININGANDPLACEMENT\\UploadFile";
            string filename = string.Empty;
            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                objTPT.Exam =Convert.ToInt32( ddlExam.SelectedValue);
                objTPT.QualificationStatus = Convert.ToInt32(ddlQualificationStatus.SelectedValue);
                objTPT.Year = Convert.ToDateTime(txtyear.Text);
                objTPT.TestScore = txtTestScore.Text;
                //objTPT.ReleventDocument7 = RelevantDocScore.Text;
               
                //if (lblBlobConnectiontring.Text != "")
                //{
                //    objTPT.IsBlob = 1;
                //}
                //else
                //{
                //    objTPT.IsBlob = 0;
                //}
                //int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);


                //bool result1;
                //string FilePath = string.Empty;

                //if (RelevantDocScore.HasFile == true)
                //{
                //    string fileName = System.Guid.NewGuid().ToString() + RelevantDocScore.FileName.Substring(RelevantDocScore.FileName.IndexOf('.'));
                //    string fileExtention = System.IO.Path.GetExtension(fileName);
                //    string ext = System.IO.Path.GetExtension(RelevantDocScore.PostedFile.FileName);
                   


                //        int sub_no = Convert.ToInt32(objCommon.LookUp("ACD_TP_TEST_SCORES", "MAX(TS_ID+1) AS TS_ID", ""));

                //        filename = sub_no + "_TestScores_" + sub_no;

                //        objTPT.ReleventDocument7 = sub_no + "_TestScores_" + sub_no + ext;



                //        if (RelevantDocScore.HasFile.ToString() != "")
                //        {

                //            objTPT.ReleventDocument7 = RelevantDocScore.FileName;

                //            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                //            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                //            result1 = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                //            if (result1 == true)
                //            {

                //                int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, RelevantDocScore);
                //                if (retval == 0)
                //                {
                //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                //                    return;
                //                }

                //                objTPT.ReleventDocument7 = RelevantDocScore.FileName;


                //            }
                //            else
                //            {
                //                objTPT.ReleventDocument7 = RelevantDocScore.FileName;

                //            }

                //            //fileUploader.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
                //        }
                //    }
                //    else
                //    {
                //        //objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                //        objCommon.DisplayMessage("Upload Supported File Format.Please Upload File In .Pdf", this);

                //        return;

                //    }
                  

                //}
                CustomStatus cs = (CustomStatus)objCompany.InsTestScores(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    clearTestScores();
                    BindTestScoresDetails(IDNO);
                }
            }
    
            else
            {
                if (ViewState["TS_ID"] != null)
                {
                    int id = 0;
                    id = Convert.ToInt32(ViewState["TS_ID"]);
                    objTPT.Exam = Convert.ToInt32(ddlExam.SelectedValue);
                    objTPT.QualificationStatus = Convert.ToInt32(ddlQualificationStatus.SelectedValue);
                    objTPT.Year = Convert.ToDateTime(txtyear.Text);
                    objTPT.TestScore = txtTestScore.Text;
                   // objTPT.ReleventDocument7 = RelevantDocScore.Text;
                    int IDNO = 0;
                  // int IDNO1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                    int IDNO1 = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    int org = Convert.ToInt32(Session["OrgId"]);


                    CustomStatus cs = (CustomStatus)objCompany.UpdTestScores(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        clearTestScores();
                        BindTestScoresDetails(IDNO1);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
        }


        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
        }
    }
    protected void btnCancelScore_Click(object sender, EventArgs e)
    {
        clearTestScores();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
    }
    public void clearTestScores()
    {
        ddlExam.SelectedValue = "0";
        ddlQualificationStatus.SelectedValue = "0";
        txtyear.Text = string.Empty;
        txtTestScore.Text = string.Empty;
       // RelevantDocScore.Text = string.Empty;
        ViewState["action"] = "add";

    }
    protected void BindTestScoresDetails(int idno)
    {
        try
        {
            DataSet ds = objCompany.BindTestScores(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvtestscored.DataSource = ds;
                lvtestscored.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEditTestScore_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int TS_ID = int.Parse(btnEdit.CommandArgument);
            ViewState["TS_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsTestScores(TS_ID);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
        }
    }

    private void ShowDetailsTestScores(int TS_ID)
    {
        try
        {

            DataSet ds = objCompany.GetIdTestScores(TS_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {



                ddlExam.SelectedValue = ds.Tables[0].Rows[0]["Exam"].ToString();
                ddlQualificationStatus.SelectedValue = ds.Tables[0].Rows[0]["QualificationStatus"].ToString();
                txtyear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                txtTestScore.Text = ds.Tables[0].Rows[0]["TestScore"].ToString();
               // RelevantDocScore.Text = ds.Tables[0].Rows[0]["UPLOAD_RELEVANT_DOCUMENT"].ToString();



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


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
    protected void chkCertification_CheckedChanged(object sender, System.EventArgs e)
    {
        if (chkCertification.Checked == true)
        {
            txtToDate.Enabled = false;
            Div4.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
        else
        {
            txtToDate.Enabled = true;
            Div4.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
    }
    protected void btnCancelAward_Click(object sender, System.EventArgs e)
    {
        clearAward();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
    }
    protected void btnlncardkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            string IDNO = string.Empty;
           // IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'");
            IDNO=ViewState["Stud_idno"].ToString();
            ShowReport("Training And Placement", "GenerateResumeReport.rpt", IDNO);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
        }
        
    }

    private void ShowReport(string reportTitle, string rptFileName, string IDNO)
    {
        try
        {

            string Script = string.Empty;
         
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;

            url += "&param=@P_REGNO=" + IDNO;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmitResume_Click(object sender, EventArgs e)
    {
        bool result1;
        string FilePath = string.Empty;
        string DOCFOLDER = file_path + "ACADEMIC\\Resume";
        string filename = string.Empty;
        if (UploadResume.HasFile == false)
        {
            objCommon.DisplayMessage(this.Page, "Please Upload Resume.", this.Page);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
            return;
        }

        if (lblBlobConnectiontring.Text != "")
        {
            objTPT.IsBlob1 = 1;
        }
        else
        {
            objTPT.IsBlob1 = 0;
        }
        if (UploadResume.HasFile == true)
        {
            TPTraining objTpTraining = new TPTraining();
            string file = string.Empty;
            if (FileTypeValid(System.IO.Path.GetExtension(UploadResume.FileName)))
            {
                if (UploadResume.HasFile)
                {
                    if (UploadResume.FileContent.Length >= 1024 * 500)
                    {

                        MessageBox("File Size Should Not Be Greater Than 500 kb");
                        FileUploadProject.Dispose();
                        FileUploadProject.Focus();
                        return;
                    }
                }

                int req_id = 0;
                int reqid1 = 0;


                if (lblBlobConnectiontring.Text == "")
                {
                    objTpTraining.IsBlob = 0;
                }
                else
                {
                    objTpTraining.IsBlob = 1;
                }
                if (objTpTraining.IsBlob == 1)
                {
                    //string filename = string.Empty;
                    //string FilePath = string.Empty;
                    // string IdNo = _idnoEmp.ToString();
                    if (UploadResume.HasFile)
                    {
                        string contentType = contentType = UploadResume.PostedFile.ContentType;
                        string ext = System.IO.Path.GetExtension(UploadResume.PostedFile.FileName);
                        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                        string[] split = UploadResume.FileName.Split('.');
                        string firstfilename = string.Join(".", split.Take(split.Length - 1));
                        string lastfilename = split.Last();
                        // filename = req_id + "_REQTRNO_" + time + ext;
                        //  objTpTraining.ReleventDocument = firstfilename + "_File_" + time + "." + lastfilename;
                        file = firstfilename + "_" + time + "." + lastfilename;
                        objTPT.ReleventDocument8 = file;
                        if (UploadResume.FileContent.Length <= 1024 * 10000)
                        {
                            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                            bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                            if (result == true)
                            {

                                int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, firstfilename + "_" + time + "." + lastfilename, UploadResume);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                                else
                                {
                                    int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                                    CustomStatus cs = (CustomStatus)objCompany.UploadResume(objTPT, IDNO, IsAdmin);
                                    if (cs.Equals(CustomStatus.RecordSaved))
                                    {

                                        //  ViewState["action"] = "add";
                                        objCommon.DisplayMessage(this.Page, "Resume Upload Successfully.", this.Page);
                                        BindUploasResumeDetails(IDNO);
                                    }
                                }
                            }
                        }



                    }
                }
                else
                {

                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.pdf]", this.Page);
                UploadResume.Focus();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
        }
    }


    protected void BindUploasResumeDetails(int idno)
    {
        try
        {
            int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
            DataSet ds = objCompany.BindUploadResume(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvResumeUpload.DataSource = ds;
                lvResumeUpload.DataBind();
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    //{
    //    string Url = string.Empty;
    //    string directoryPath = string.Empty;
    //    try
    //    {
    //        //string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    //        //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
    //        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
    //        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

    //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
    //        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();      
    //        string directoryName = "~/ACADEMIC\\Resume" + "/";
    //        directoryPath = Server.MapPath(directoryName);

    //        if (!Directory.Exists(directoryPath.ToString()))
    //        {

    //            Directory.CreateDirectory(directoryPath.ToString());
    //        }           

    //        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
    //        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
    //        // string img = Convert.ToString(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "FILE_PATH", "ROUTEID='" + routeid + "' and BUSSTR_ID='" + seating + "'"));
    //        var ImageName = img;
    //        if (img == null || img == "")
    //        {
    //            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
    //            embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
    //            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    //            embed += "</object>";
    //            //ltEmbed.Text = "Image Not Found....!";

    //        }
    //        else
    //        {
    //            if (img != "")
    //            {
    //                 DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr,blob_ContainerName,img);
    //                var blob = blobContainer.GetBlockBlobReference(ImageName);
                 
    //                string filePath = directoryPath + "\\" + ImageName;
    //                if ((System.IO.File.Exists(filePath)))
    //                {
    //                    System.IO.File.Delete(filePath);
    //                }
    //                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
              
    //                //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
    //                //embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
    //                //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    //                //embed += "</object>";
    //               // DownloadFile(Server.MapPath("~/ACADEMIC/Resume/"), ImageName);


    //                string FILENAME = img;
                    
                
    //                string filePath1 = Server.MapPath("~/ACADEMIC/Resume/" + ImageName);


    //                string filee = Server.MapPath("~/Transactions/TP_PDF_Reader.aspx");
    //                FileInfo file = new FileInfo(filePath1);

    //                if (file.Exists)
    //                {
    //                    Session["sb"] = filePath.ToString();
    //                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "window.open('"+filee+"'); alert('Your message here' );", true);

    //                    //Response.Redirect("~/TRAININGANDPLACEMENT/Transactions/TP_PDF_Reader.aspx");

    //                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

    //                    url += "ACADEMIC/RESUME/" + FILENAME;
                   

    //                    //string url = filePath;
               
    //                    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //                    divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //                    divMsg.InnerHtml += " </script>";                      
    //            } 
    //            }

    //      }
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
    //    }

    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            ////string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            ////string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
            //string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            //string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            //CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            //string directoryName = "~/ACADEMIC\\Resume" + "/";
            //directoryPath = Server.MapPath(directoryName);

            //if (!Directory.Exists(directoryPath.ToString()))
            //{

            //    Directory.CreateDirectory(directoryPath.ToString());
            //}
            //CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            //string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            //// string img = Convert.ToString(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "FILE_PATH", "ROUTEID='" + routeid + "' and BUSSTR_ID='" + seating + "'"));
            //var ImageName = img;
            //if (img == null || img == "")
            //{
            //    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
            //    embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
            //    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            //    embed += "</object>";
            //    //ltEmbed.Text = "Image Not Found....!";


            //}
            //else
            //{
            //    if (img != "")
            //    {
            //        DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
            //        var blob = blobContainer.GetBlockBlobReference(ImageName);

            //        string filePath = directoryPath + "\\" + ImageName;

            //        if ((System.IO.File.Exists(filePath)))
            //        {
            //            System.IO.File.Delete(filePath);
            //        }
            //        blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
            //        //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
            //        //embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
            //        //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            //        //embed += "</object>";
            //        // DownloadFile(Server.MapPath("~/ACADEMIC/Resume/"), ImageName);
            //        string FILENAME = img;
            //        string filePath1 = Server.MapPath("~/ACADEMIC/Resume/" + ImageName);


            //        string filee = Server.MapPath("~/Transactions/TP_PDF_Reader.aspx");
            //        FileInfo file = new FileInfo(filePath1);

            //        if (file.Exists)
            //        {
            //            Session["sb"] = filePath.ToString();
            //            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "window.open('"+filee+"'); alert('Your message here' );", true);

            //            //Response.Redirect("~/TRAININGANDPLACEMENT/Transactions/TP_PDF_Reader.aspx");

            //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            //            url += "ACADEMIC/RESUME/" + FILENAME;
            //            //string url = filePath;
            //            // added by Gaurav varma 18/10/23
            //            Response.Clear();
            //            Response.AddHeader("Content-Disposition", "attachment; filename=" + FILENAME);
            //            Response.AddHeader("Content-Length", file.Length.ToString());
            //            Response.ContentType = "application/octet-stream";
            //            Response.TransmitFile(filePath1);
            //           // Response.End();
            //            // end Gaurav varma 18/10/23

            //            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //            divMsg.InnerHtml += " </script>";

            //        }
            //    }

            //}
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);

            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
        }

        catch (Exception ex)
        {
            throw;
        }
    }


    public void DownloadFile(string path, string fileName)
    {
        try
        {
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }
    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
    protected void btnCancelResume_Click1(object sender, EventArgs e)
    {
        Session["sb"] = null;
        clear();
        
        string Url = string.Empty;
        string directoryPath = string.Empty;
        string img = string.Empty;
        string blob_ConStr = string.Empty;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);

    }
    protected void btnSubmitExamDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                int id = 0;
                if (rbGap.SelectedValue == "1")
                {
                    objTPT.IS_GAP = true;
                }
                else
                {
                    objTPT.IS_GAP = false;
                }
                objTPT.GAP = Convert.ToInt32(txtGapYear.Text);

               // int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'")); //Convert.ToInt32(Session["userno"]);
                int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                int IsAdmin = Convert.ToInt32(Session["userno"]);
                int org = Convert.ToInt32(Session["OrgId"]);
                CustomStatus cs = (CustomStatus)objCompany.InsExamDetails(objTPT, org, id, IDNO, IsAdmin);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ViewState["action"] = "add";
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    BindExamDetails(IDNO);
                }
            }
            else
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (rbGap.SelectedValue == "1")
                    {
                        objTPT.IS_GAP = true;
                        objTPT.GAP = Convert.ToInt32(txtGapYear.Text);
                    }
                    else
                    {
                        objTPT.IS_GAP = false;
                        objTPT.GAP = 0;
                    }
                    int id = Convert.ToInt32(hdnGapID.Value);
                    int org = Convert.ToInt32(Session["OrgId"]);
 
                   // int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
                    int IDNO = Convert.ToInt32(ViewState["Stud_idno"]);
                    int IsAdmin = Convert.ToInt32(Session["userno"]);
                    CustomStatus cs = (CustomStatus)objCompany.InsExamDetails(objTPT, org, id, IDNO, IsAdmin);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        ViewState["action"] = "add";
                        BindExamDetails(IDNO);
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
    }
    protected void btnCancelExamDetails_Click(object sender, EventArgs e)
    {
        txtGapYear.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
    }
    protected void rbGap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbGap.SelectedValue == "1")
        {
            divGapBtn.Visible = true;
            divGap.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
        else
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                divGapBtn.Visible = false;
            }
            divGap.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
    }
    protected void BindExamDetails(int idno)
    {
        try
        {
            bool isgap;
            DataSet ds = objCompany.BindExamDetails(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvExamGPA.DataSource = ds;
                lvExamGPA.DataBind();
                isgap = Convert.ToBoolean(ds.Tables[0].Rows[1]["IS_GAP"].ToString());
                lblHArrear.Text = ds.Tables[1].Rows[0]["BACKLOG_HISTORY"].ToString();
                lblCArrear.Text = ds.Tables[1].Rows[0]["CURRENT_BACKLOG"].ToString();
                if (isgap == true)
                {
                    divGap.Visible = true;
                    divGapBtn.Visible = true;
                    rbGap.SelectedValue = "1";
                    txtGapYear.Text = ds.Tables[0].Rows[1]["GAP"].ToString();
                    hdnGapID.Value = ds.Tables[0].Rows[1]["GAP_ID"].ToString();
                    ViewState["action"] = "edit";
                }
                else
                {
                    divGap.Visible = false;
                    divGapBtn.Visible = false;
                    rbGap.SelectedValue = "2";
                    txtGapYear.Text = string.Empty;
                    hdnGapID.Value = null;
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void imgbtnProjectPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        catch (Exception ex)
        {
            throw;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
    }
    protected void imgbtnCertificationPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        catch (Exception ex)
        {
            throw;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
        }
    }
    protected void imgbtnWorkExpPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
        }
        catch (Exception ex)
        {
            throw;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
    }
  
}
