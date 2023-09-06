//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COVID-19 VACCINATION INFORMATION                                                     
// CREATION DATE : 27-SEPT-2010                                                          
// CREATED BY    : SNEHA G.                            
// MODIFIED DATE : 01-11-2021                
// ADDED BY      :                                    
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using System.Configuration;

public partial class ACADEMIC_CovidVaccinationDetails : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    VaccinationController objvaccination = new VaccinationController();
    StudentController objSC = new StudentController();

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
                // Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                trVaccinated.Visible = false;
                string idno = string.Empty;
                ViewState["usertype"] = Session["usertype"];

                divadmissiondetailstreeview.Visible = false;

                string status = objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "STATUS", "IDNO=" + Convert.ToInt32(Session["idno"]));  //Added by sachin on 14-07-2022
                DataSet dsinfo = objCommon.FillDropDown("ACD_ADM_STUD_INFO_SUBMIT_LOG", "PERSONAL_INFO,ADDRESS_INFO,DOC_INFO,QUAL_INFO,OTHER_INFO,FINAL_SUBMIT", "ADMBATCH", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", string.Empty);
                if (dsinfo != null && dsinfo.Tables[0].Rows.Count > 0)
                {
                    string personal_info = dsinfo.Tables[0].Rows[0]["PERSONAL_INFO"].ToString();
                    string address_info = dsinfo.Tables[0].Rows[0]["ADDRESS_INFO"].ToString();
                    string doc_info = dsinfo.Tables[0].Rows[0]["DOC_INFO"].ToString();
                    string qual_info = dsinfo.Tables[0].Rows[0]["QUAL_INFO"].ToString();
                    string other_info = dsinfo.Tables[0].Rows[0]["OTHER_INFO"].ToString();
                    string final_submit = dsinfo.Tables[0].Rows[0]["FINAL_SUBMIT"].ToString();

                    int FinalSubmit = 0;
                    if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                    {
                        FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    }
                    if (FinalSubmit == 1)
                    { divPrintReport.Visible = true; }
                    else
                    { divPrintReport.Visible = false; }
                    //divPrintReport.Visible = true; 

                    if (personal_info == "1")
                    {
                        lnkAddressDetail.Enabled = true;
                    }
                    if (address_info == "1")
                    {
                        lnkUploadDocument.Enabled = true;
                    }
                    if (doc_info == "1")
                    {
                        lnkQualificationDetail.Enabled = true;
                    }
                    if (qual_info == "1")
                    {
                        lnkotherinfo.Enabled = true;
                    }
                    if (other_info == "1")
                    {
                        lnkprintapp.Enabled = true;
                    }
                    if (final_submit == "1")
                    {
                        Button1.Visible = false;
                        //btnAdd.Visible = false;
                        //btnAddEntranceExam.Visible = false;
                    }
                }


                if (Session["usertype"].ToString() == "2")
                {
                    divhome.Visible = false;
                    idno = Session["idno"].ToString();
                    //lnkApproveAdm.Visible = false;
                    divAdmissionApprove.Visible = false;
                    divadmissiondetailstreeview.Visible = false;
                    ShowDetails(idno);
                    CheckFinalSubmission();  // Added By Bhagyashree on 30052023
                }
                else
                {
                    idno = Session["stuinfoidno"].ToString();
                    //lnkApproveAdm.Visible = true;
                    divAdmissionApprove.Visible = true;
                    divadmissiondetailstreeview.Visible = true;
                    ShowDetails(idno);
                    // HideRowForAdmin();
                }

            }


        }
        divMsg.InnerHtml = string.Empty;

    }

    private void HideRowForAdmin()
    {
        trVaccinationCondition.Visible = true;
        trVaccinated.Visible = true;
    }
    private void CheckFinalSubmission()
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        DataSet dsallowprocess = objSC.GetAllowProcess(Convert.ToInt32(Session["idno"]), 5, 'E');
        int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        if (finalsubmit == "1" && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
        {
            Button1.Visible = true;
        }
    }
    protected void btnFirstDose_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFirstDoseVaccName.Text == string.Empty || txtFirstDoseVaccCenter.Text == string.Empty || txtFirstDoseVaccDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this, "Please fill up all the mandatory field", this.Page);
                return;
            }
            if (Convert.ToDateTime(txtFirstDoseVaccDate.Text) > DateTime.Now.Date)
            {
                objCommon.DisplayMessage(this, "Please enter valid First Dose Vaccination Date  ", this.Page);
                return;
            }

            if (fuFirstDoseVaccCert.HasFile)
            {
                string filePath = MapPath("~/FilePathVaccination/");  // for test
                //string filePath = ConfigurationManager.AppSettings["FilePathVaccination"].ToString();
                string fileSavePath = filePath + "\\First_Dose";
                string fileName = "First_Dose_" + lblName.ToolTip + Path.GetExtension(fuFirstDoseVaccCert.FileName);
                int ret = UploadVaccinationCertificate(fuFirstDoseVaccCert, fileName, fileSavePath);
                if (ret == 0)
                {
                    return;
                }
                lnkbtnFirstDoseCert.Text = fileName;
                hidFirstDoseFilePath.Value = fileSavePath;
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Upload the Covid Vaccination Certificate !!", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objvaccination.AddUpdateFirstDoseVaccination(lblName.ToolTip, txtFirstDoseVaccName.Text, txtFirstDoseVaccCenter.Text, txtFirstDoseVaccDate.Text, hidFirstDoseFilePath.Value, lnkbtnFirstDoseCert.Text, hidVaccinationStat.Value);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                ShowDetails(lblName.ToolTip);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);
                ShowDetails(lblName.ToolTip);
            }

            else
            {
                objCommon.DisplayMessage(this, "Error While Saving !!", this.Page);
            }



        }
        catch (Exception ex)
        {
            throw;

        }
    }
    private int UploadVaccinationCertificate(FileUpload fileUpload, string fileName, string fileSavePath)
    {
        int retval = 0;
        var supportedTypes = new[] { ".pdf" };
        string fileExtension = Path.GetExtension(fileUpload.FileName);
        if (!supportedTypes.Contains(fileExtension))
        {
            objCommon.DisplayMessage("Only pdf file allow !!", this.Page);
            return retval;
        }
        //check for file size
        if (fileUpload.PostedFile.ContentLength > (1048576))
        {
            objCommon.DisplayMessage("File size should be less than 1MB !!", this.Page);
            return retval;
        }


        bool folderExists = Directory.Exists(fileSavePath);
        if (!folderExists)
        {
            Directory.CreateDirectory(fileSavePath);
        }
        string fullPath = Path.Combine(fileSavePath, fileName);
        FileInfo file = new FileInfo(fullPath);
        if (file.Exists)//check file exsit or not  
        {
            file.Delete();
        }
        fileUpload.SaveAs(fullPath);
        retval = 1;
        return retval;
    }

    protected void btnSecondDose_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSecondDoseVaccName.Text == string.Empty || txtSecondDoseVaccCenter.Text == string.Empty || txtSecondDoseVaccDate.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please fill up all the mandatory field", this.Page);
                return;
            }
            if (Convert.ToDateTime(txtSecondDoseVaccDate.Text) > DateTime.Now.Date)
            {
                objCommon.DisplayMessage(this, "Please enter valid Second Dose Vaccination Date  ", this.Page);
                return;
            }
            if (fuSecondDoseVaccCert.HasFile)
            {
                string filePath = MapPath("~/FilePathVaccination/");  // for test
                //string filePath = ConfigurationManager.AppSettings["FilePathVaccination"].ToString();
                string fileSavePath = filePath + "\\Second_Dose";
                string fileName = "Second_Dose_" + lblName.ToolTip + Path.GetExtension(fuSecondDoseVaccCert.FileName);
                int ret = UploadVaccinationCertificate(fuSecondDoseVaccCert, fileName, fileSavePath);
                if (ret == 0)
                {
                    return;
                }
                lnkbtnSecondDoseVaccCert.Text = fileName;
                hidSecondDoseFilePath.Value = fileSavePath;
            }
            //// Added by sachin 21-10-2022  validation 
            else
            {
                objCommon.DisplayMessage(this, "Please Upload the Covid Vaccination Certificate !!", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objvaccination.UpdateSecondDoseVaccination(lblName.ToolTip, txtSecondDoseVaccName.Text, txtSecondDoseVaccCenter.Text, txtSecondDoseVaccDate.Text, hidSecondDoseFilePath.Value, lnkbtnSecondDoseVaccCert.Text);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Record Saved Successfully !!", this.Page);
                ShowDetails(lblName.ToolTip);
            }
            else
            {
                objCommon.DisplayMessage("Error While Updating !!", this.Page);
            }


        }
        catch (Exception ex)
        {
            throw;

        }
    }

    protected void rdVaccinated_CheckedChanged(object sender, EventArgs e)
    {
        if (rdVaccinated.Checked == true)
        {
            // trNotVaccinated.Visible = false;
            trVaccinated.Visible = true;
            divNote.Visible = true;

        }
        else if (rdNotVaccinated.Checked == true)
        {
            //trNotVaccinated.Visible = true;
            trVaccinated.Visible = false;
            divNote.Visible = false;
        }
    }

    protected void lnkbtnFirstDoseCert_Click(object sender, EventArgs e)
    {
        string url = string.Format("VeiwVaccinationCertificate.aspx?idno={0}&dose=1", lblName.ToolTip);
        string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);
    }

    protected void lnkbtnSecondDoseVaccCert_Click(object sender, EventArgs e)
    {
        string url = string.Format("VeiwVaccinationCertificate.aspx?idno={0}&dose=2", lblName.ToolTip);
        string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);
    }

    protected void btnsecondcancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnFirstCancel_Click(object sender, EventArgs e)
    {

    }

    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "1")
        {
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/PersonalDetails.aspx");
    }

    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AddressDetails.aspx");
    }

    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/AdmissionDetails.aspx");
    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }

    protected void lnkUploadDocument_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/UploadDocument.aspx");
    }

    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }

    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
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
                objCommon.DisplayMessage(this, "Please Search Enrollment No!!", this.Page);
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
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }

    private void ShowDetails(string idno)
    {
        trGeneralInfo.Visible = true;
        trVaccinationCondition.Visible = true;
        if (Session["usertype"].ToString() != "2")
        {
            //rdVaccinated.Enabled = false;
            //rdNotVaccinated.Enabled = false;
        }
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO)", "S.IDNO,S.STUDNAME,S.ENROLLNO", "D.DEGREENAME,B.LONGNAME,S.SEX", "S.IDNO=" + idno, "S.IDNO");
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Show Student Details..
                lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblName.ToolTip = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblRegNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                if (ds.Tables[0].Rows[0]["SEX"].ToString() == "M")
                {
                    lblGender.Text = "Male";
                }
                else
                {
                    lblGender.Text = "Female";
                }

                int count = Convert.ToInt32(objCommon.LookUp("ACD_COVID_VACCINATION_DETAIL", "COUNT(IDNO)", "IDNO=" + lblName.ToolTip));
                if (count > 0)
                {
                    ShowVaccinationDetails(lblName.ToolTip);
                }
                else
                {
                    trVaccinated.Visible = false;
                    //trNotVaccinated.Visible = false;
                    rdVaccinated.Checked = false;
                    rdNotVaccinated.Checked = false;
                }
                //string documentUpdStat = ds.Tables[0].Rows[0]["DOCUMENTSTAT"].ToString();
                //FillDocumentList(lblSchoName.ToolTip, documentUpdStat);
            }
            else
            {
                objCommon.DisplayMessage("Please enter from student login !!", this.Page);
                return;
            }
        }
    }

    private void ShowVaccinationDetails(string idno)
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + idno + "");
        DataSet dsallowprocess = objSC.GetAllowProcess(Convert.ToInt32(Session["idno"]), 5, 'E');
        ViewState["AllowProcess"] = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        ViewState["FinalSubmit"] = finalsubmit.ToString();
        DataSet cvDs = objCommon.FillDropDown("ACD_COVID_VACCINATION_DETAIL", "VACCINATION_ID,VACCINATION_STAT,FIRSTDOSE_VACCINE_NAME,FIRSTDOSE_VACCINATION_CENTER,FIRSTDOSE_VACCINATED_DATE,FIRSTDOSE_FILE_PATH,FIRSTDOSE_FILE_NAME,FIRSTDOSE_LOCK", "SECONDDOSE_VACCINE_NAME,SECONDDOSE_VACCINATION_CENTER,SECONDDOSE_VACCINATED_DATE,SECONDDOSE_FILE_PATH,SECONDDOSE_FILE_NAME,SECONDDOSE_LOCK", "IDNO=" + idno, "IDNO");
        string vaccinationStat = cvDs.Tables[0].Rows[0]["VACCINATION_STAT"].ToString();
        hidVaccinationStat.Value = vaccinationStat;
        if (vaccinationStat == "0")
        {
            rdNotVaccinated.Checked = true;
            rdVaccinated.Checked = false;
            //trNotVaccinated.Visible = true;
            trVaccinated.Visible = false;
            //btnVaccineNotTaken.Visible = false;
        }
        else if (vaccinationStat == "1" || vaccinationStat == "2")
        {
            if (vaccinationStat == "1")
            {
                btnsecondcancel.Enabled = true;
                btnSecondDose.Enabled = true;
            }
            rdNotVaccinated.Checked = false;
            rdVaccinated.Checked = true;
            rdNotVaccinated.Enabled = false;
            rdVaccinated.Enabled = false;
            //trNotVaccinated.Visible = false;
            trVaccinated.Visible = true;

            string firstdoseVaccineName = cvDs.Tables[0].Rows[0]["FIRSTDOSE_VACCINE_NAME"].ToString();
            string firstdoseVaccinatedCenter = cvDs.Tables[0].Rows[0]["FIRSTDOSE_VACCINATION_CENTER"].ToString();
            string firstdoseVaccinatedDate = Convert.ToDateTime(cvDs.Tables[0].Rows[0]["FIRSTDOSE_VACCINATED_DATE"]).ToString("dd/MM/yyyy");
            string firstdoseFileName = cvDs.Tables[0].Rows[0]["FIRSTDOSE_FILE_NAME"].ToString();
            string firstdoseFilePath = cvDs.Tables[0].Rows[0]["FIRSTDOSE_FILE_PATH"].ToString();
            string firstdoseLock = cvDs.Tables[0].Rows[0]["FIRSTDOSE_LOCK"].ToString();
            DisplayFirstDoseDetails(firstdoseVaccineName, firstdoseVaccinatedCenter, firstdoseVaccinatedDate, firstdoseFileName, firstdoseFilePath, firstdoseLock);
            if (vaccinationStat == "2")
            {
                string seconddoseVaccineName = cvDs.Tables[0].Rows[0]["SECONDDOSE_VACCINE_NAME"].ToString();
                string seconddoseVaccinatedCenter = cvDs.Tables[0].Rows[0]["SECONDDOSE_VACCINATION_CENTER"].ToString();
                string seconddoseVaccinatedDate = Convert.ToDateTime(cvDs.Tables[0].Rows[0]["SECONDDOSE_VACCINATED_DATE"]).ToString("dd/MM/yyyy");
                string seconddoseFileName = cvDs.Tables[0].Rows[0]["SECONDDOSE_FILE_NAME"].ToString();
                string seconddoseFilePath = cvDs.Tables[0].Rows[0]["SECONDDOSE_FILE_PATH"].ToString();
                string seconddoseLock = cvDs.Tables[0].Rows[0]["SECONDDOSE_LOCK"].ToString();
                DisplaySecondDoseDetails(seconddoseVaccineName, seconddoseVaccinatedCenter, seconddoseVaccinatedDate, seconddoseFileName, seconddoseFilePath, seconddoseLock);
            }
            else
            {
                if (Session["usertype"].ToString() != "2")
                {
                    //txtSecondDoseVaccName.Enabled = false;
                    //txtSecondDoseVaccCenter.Enabled = false;
                    //txtSecondDoseVaccDate.Enabled = false;
                    //ceSecondDoseVaccDate.Enabled = false;
                    //fuSecondDoseVaccCert.Enabled = false;
                    //btnSecondDose.Visible = false;
                    // btnUnlockSecondDose.Visible = false;
                }
            }
        }
    }

    private void DisplayFirstDoseDetails(string firstdoseVaccineName, string firstdoseVaccinatedCenter, string firstdoseVaccinatedDate, string firstdoseFileName, string firstdoseFilePath, string firstdoseLock)
    {
      
        txtFirstDoseVaccName.Text = firstdoseVaccineName;
        txtFirstDoseVaccCenter.Text = firstdoseVaccinatedCenter;
        txtFirstDoseVaccDate.Text = firstdoseVaccinatedDate;
        if (firstdoseFileName != string.Empty)
        {
            trFirstDoseCert.Visible = true;
            lnkbtnFirstDoseCert.Text = firstdoseFileName;
            hidFirstDoseFilePath.Value = firstdoseFilePath;
        }
        btnSecondDose.Visible = true;

        if (Session["usertype"].ToString() == "2")
        {
            if (Convert.ToInt32(ViewState["FinalSubmit"].ToString()) == 1 && Convert.ToInt32(ViewState["AllowProcess"].ToString()) > 0)
            {
                txtFirstDoseVaccName.Enabled = true;
                txtFirstDoseVaccCenter.Enabled = true;
                txtFirstDoseVaccDate.Enabled = true;
                ceFirstDoseVaccDate.Enabled = true;
                fuFirstDoseVaccCert.Enabled = true;
                btnFirstDose.Visible = true;
                btnFirstCancel.Visible = true;
                btnFirstDose.Enabled = true;
                btnFirstCancel.Enabled = true;
            }
            else if (firstdoseLock == "1")
            {
                txtFirstDoseVaccName.Enabled = false;
                txtFirstDoseVaccCenter.Enabled = false;
                txtFirstDoseVaccDate.Enabled = false;
                ceFirstDoseVaccDate.Enabled = false;
                fuFirstDoseVaccCert.Enabled = false;
                btnFirstDose.Visible = false;
                btnFirstCancel.Visible = false;
            }
            else
            {
                txtFirstDoseVaccName.Enabled = true;
                txtFirstDoseVaccCenter.Enabled = true;
                txtFirstDoseVaccDate.Enabled = true;
                ceFirstDoseVaccDate.Enabled = true;
                fuFirstDoseVaccCert.Enabled = true;
                btnFirstDose.Visible = true;
                btnFirstCancel.Visible = true;
            }
        }
        //btnUnlockFirstDose.Visible = false;
        else
        {
            txtFirstDoseVaccName.Enabled = false;
            txtFirstDoseVaccCenter.Enabled = false;
            txtFirstDoseVaccDate.Enabled = false;
            ceFirstDoseVaccDate.Enabled = false;
            fuFirstDoseVaccCert.Enabled = false;
            btnFirstDose.Visible = false;
            btnFirstCancel.Visible = false;
            //if (firstdoseLock == "1")
            //{
            //    btnUnlockFirstDose.Visible = true;
            //}
            //else
            //{
            //    btnUnlockFirstDose.Visible = false;
            //}
        }
    }

    private void DisplaySecondDoseDetails(string seconddoseVaccineName, string seconddoseVaccinatedCenter, string seconddoseVaccinatedDate, string seconddoseFileName, string seconddoseFilePath, string seconddoseLock)
    {
        txtSecondDoseVaccName.Text = seconddoseVaccineName;
        txtSecondDoseVaccCenter.Text = seconddoseVaccinatedCenter;
        txtSecondDoseVaccDate.Text = seconddoseVaccinatedDate;
        if (seconddoseFileName != string.Empty)
        {
            trSecondDoseCert.Visible = true;
            lnkbtnSecondDoseVaccCert.Text = seconddoseFileName;
            hidSecondDoseFilePath.Value = seconddoseFilePath;
        }
        if (Session["usertype"].ToString() == "2")
        {
            if (Convert.ToInt32(ViewState["FinalSubmit"].ToString()) == 1 && Convert.ToInt32(ViewState["AllowProcess"].ToString()) > 0)
            {
                txtSecondDoseVaccName.Enabled = true;
                txtSecondDoseVaccCenter.Enabled = true;
                txtSecondDoseVaccDate.Enabled = true;
                ceSecondDoseVaccDate.Enabled = true;
                fuSecondDoseVaccCert.Enabled = true;
                btnSecondDose.Visible = true;
                btnsecondcancel.Visible = true;
                btnSecondDose.Enabled = true;
                btnsecondcancel.Enabled = true;
            }
            else if (seconddoseLock == "1")
            {
                txtSecondDoseVaccName.Enabled = false;
                txtSecondDoseVaccCenter.Enabled = false;
                txtSecondDoseVaccDate.Enabled = false;
                ceSecondDoseVaccDate.Enabled = false;
                fuSecondDoseVaccCert.Enabled = false;
                btnSecondDose.Visible = false;
                btnsecondcancel.Visible = false;
            }
            else
            {
                txtSecondDoseVaccName.Enabled = true;
                txtSecondDoseVaccCenter.Enabled = true;
                txtSecondDoseVaccDate.Enabled = true;
                ceSecondDoseVaccDate.Enabled = true;
                fuSecondDoseVaccCert.Enabled = true;
                btnSecondDose.Visible = true;
                btnsecondcancel.Visible = true;
            }
            // btnUnlockSecondDose.Visible = false;
        }
        else
        {
            txtSecondDoseVaccName.Enabled = false;
            txtSecondDoseVaccCenter.Enabled = false;
            txtSecondDoseVaccDate.Enabled = false;
            ceSecondDoseVaccDate.Enabled = false;
            fuSecondDoseVaccCert.Enabled = false;
            btnSecondDose.Visible = false;
            btnsecondcancel.Visible = false;
            //if (seconddoseLock == "1")
            //{
            //    btnUnlockSecondDose.Visible = true;
            //}
            //else
            //{
            //    btnUnlockSecondDose.Visible = false;
            //}
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = (Convert.ToInt32(Session["idno"]));

        }
        else
        {
            idno = (Convert.ToInt32(Session["stuinfoidno"]));
        }

        if (rdNotVaccinated.Checked == true)          //Added by sachin on 11-08-2022 
        {
            CustomStatus cs = (CustomStatus)objvaccination.UpdateVaccinationStatus(idno);
            Response.Redirect("~/academic/OtherInformation.aspx");

        }

        else
        {

            //idno = Convert.ToInt32(Session["stuinfoidno"]) == 0 ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(Session["stuinfoidno"]);
            DataSet cvDs = objCommon.FillDropDown("ACD_COVID_VACCINATION_DETAIL", "VACCINATION_ID,VACCINATION_STAT,FIRSTDOSE_VACCINE_NAME,FIRSTDOSE_VACCINATION_CENTER,FIRSTDOSE_VACCINATED_DATE,FIRSTDOSE_FILE_PATH,FIRSTDOSE_FILE_NAME,FIRSTDOSE_LOCK", "SECONDDOSE_VACCINE_NAME,SECONDDOSE_VACCINATION_CENTER,SECONDDOSE_VACCINATED_DATE,SECONDDOSE_FILE_PATH,SECONDDOSE_FILE_NAME,SECONDDOSE_LOCK", "IDNO=" + idno, "IDNO");

            if (cvDs.Tables[0].Rows.Count > 0)
            {
                CustomStatus cs = (CustomStatus)objvaccination.UpdateVaccinationStatus(Convert.ToInt32(Session["stuinfoidno"]));
                string vaccinationStat = cvDs.Tables[0].Rows[0]["VACCINATION_STAT"].ToString();
                hidVaccinationStat.Value = vaccinationStat;
                if (vaccinationStat == "0")
                {
                    Response.Redirect("~/academic/OtherInformation.aspx");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vaccination Details Saved Successfully.');window.location ='OtherInformation.aspx';", true);
                    // return;
                }
                else if (vaccinationStat == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Second Dose of Vaccine Details yet not Uploaded.');window.location ='OtherInformation.aspx';", true);
                }


                else if (vaccinationStat == "2")
                {
                    Response.Redirect("~/academic/OtherInformation.aspx");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vaccination Details Saved Successfully.');window.location ='OtherInformation.aspx';", true);
                }
            }
            else
            {
                if (rdVaccinated.Checked == true)
                {
                    if (txtFirstDoseVaccName.Text == "" || txtFirstDoseVaccCenter.Text == "" || txtFirstDoseVaccDate.Text == "")
                    {
                        objCommon.DisplayMessage(this, "Please fill the first Dose vaccination Details", this.Page);
                        return;
                    }
                }
            }
        }
    }

    protected void btnGohome_ServerClick(object sender, EventArgs e)
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
}