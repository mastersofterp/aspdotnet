using System;
using System.Collections;
using System.Configuration;
using System.IO;
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


public partial class Academic_Default : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentRegistration objRegistration = new StudentRegistration();

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
        try
            {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
                {
                Response.Redirect("~/default.aspx");
                }

            if (!Page.IsPostBack)
                {

                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                PopulateDropDownList();

                txtREGNo.Enabled = ((objCommon.LookUp("REFF", "ENROLLMENTNO", "ENROLLMENTNO>0") == "False") ? false : true);
                if (txtREGNo.Enabled == true)
                    watREGNo.WatermarkText = "Enter Registration No. ";
                else
                    watREGNo.WatermarkText = "Automatic Registration No. Generation";

                txtDateOfAdmission.Text = DateTime.Today.ToString("dd/MM/yyyy");
                // btnReport.Enabled = false;

                ph.Visible = false;
                ph1.Visible = false;
                dis.Visible = false;
                dis1.Visible = false;
                }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;

            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (ua_type == "2")
                {

                updEdit.Visible = false;
                divmain.Visible = true;
                ShowStudentDetails();
                }
            //else if (ua_type == "2" && Session["idno"] == string.Empty)
            //{
            //    updEdit.Visible = false;
            //    divmain.Visible = true;
            //}
            else if (ua_type == "1")
                {
                updEdit.Visible = true;
                divmain.Visible = false;
                }


            }
        catch (Exception ex)
            {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "StudentRegistration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable");
            }
        }

    private void CheckPageAuthorization()
        {
        if (Request.QueryString["pageno"] != null)
            {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                Response.Redirect("~/notauthorized.aspx?page=StudentRegistrationPhd.aspx");
                }
            }
        else
            {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentRegistrationPhd.aspx");
            }
        }

    protected void PopulateDropDownList()
        {
        try
            {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            // FILL DROPDOWN BATCH?
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>1", "BATCHNO DESC");
            ddlBatch.SelectedIndex = 1;
            // FILL DROPDOWN YEAR
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 and YEAR = 1", "YEAR");
            ddlYear.SelectedIndex = 1;
            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 and SEMESTERNO <6", "SEMESTERNO");
            // FILL DROPDOWN PAYMENT TYPE
            //objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO IN (3,5,7)", "PAYTYPENO");
            objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO NOT IN(0,1,2)", "PAYTYPENO");
            //fill dropdown id type
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO >1", "IDTYPENO");
            //fill dropdown adm round
            objCommon.FillDropDownList(ddlAdmRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0", "ROUNDNAME");
            //fill dropdown adm quota
            objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");
            //fill dropdown adm quota
            //objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO");
            this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
            //objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO");
            //fill dropdown Supervisor
            this.objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "", "SUPERVISORNAME");
            this.objCommon.FillDropDownList(ddlCoSupevisor1, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "", "SUPERVISORNAME");
            this.objCommon.FillDropDownList(ddlCoSupevisor2, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "", "SUPERVISORNAME");
            objCommon.FillDropDownList(ddlTypeofPhy, "ACD_PHYSICAL_HANDICAPPED", "HANDICAP_NO", "HANDICAP_NAME", "HANDICAP_NO>0", "HANDICAP_NO");

            //--- add for remove  autocomplete text box on 17/4/2018
            objCommon.FillDropDownList(ddlBloodGrp, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO");
            objCommon.FillDropDownList(ddlReligion, "acd_Religion", "Religionno", "Religion", "Religionno > 0", "Religion");
            objCommon.FillDropDownList(ddlNationality, "acd_nationality", "nationalityno", "nationality", "nationalityno > 0", "nationality");
            objCommon.FillDropDownList(ddlCategory, "acd_category", "categoryno", "category", "categoryno > 0  AND CATEGORYNO IN(1,2,3,4)", "category");
            objCommon.FillDropDownList(ddlAdmCategory, "acd_category", "categoryno", "category", "categoryno > 0", "category");

            objCommon.FillDropDownList(ddlcity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITYNO");
            objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENO");

            objCommon.FillDropDownList(ddlLocalCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO > 0", "CITY");
            objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0", "STATENAME");


            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0", "LONGNAME");
            // ddlBranch.Focus();
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0", "QUALIFYNO");
            objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "SUPERVISORNO>0", "SUPERVISORNO");
            //objCommon.FillDropDownList(ddlStatusCat, "ACD_PHDSTATUS_CATEGORY", "PHDSTATAUSCATEGORYNO", "PHDSTATAUSCATEGRYNAME", "PHDSTATAUSCATEGORYNO>0", "PHDSTATAUSCATEGORYNO");
            objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 and FLOCK=1", "SESSIONNO DESC");

            trPhdStatus.Visible = true;
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentRegistrationPhd.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    protected void btnSave_Click(object sender, EventArgs e)
        {
        //string COUNT = objCommon.LookUp("ACD_PHD_DGC", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]));
        //if (COUNT == "1")
        //{ 
        //    objCommon.DisplayMessage(upnldivmain,"You are Already Registered for PHD",Page);

        //}

        string regNo = string.Empty;
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        GEC_Student objStud = new GEC_Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        try
            {
            DateTime dtmin = new DateTime(1753, 1, 1);
            DateTime dtmax = new DateTime(9999, 12, 31);
            if (!string.IsNullOrEmpty(txtDateOfBirth.Text.Trim()))
                {
                if ((DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmin) <= 0) ||
                    (DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmax) >= 0))
                    {
                    //1/1/1753 12:00:00 AM and 12/31/9999
                    objCommon.DisplayMessage(this.updpnlUser, "Please Enter Valid Date!!", this.Page);
                    return;
                    }
                }
            string RRNO = string.Empty;

            if (!txtRRNo.Text.Trim().Equals(string.Empty))
                RRNO = txtRRNo.Text.Trim();

            //if (!txtEnrollno.Text.Trim().Equals(string.Empty)) objS.EnrollNo = txtEnrollno.Text.Trim();
            if (!txtStudentName.Text.Trim().Equals(string.Empty))
                objS.StudName = txtStudentName.Text.Trim();
            if (!txtFatherName.Text.Trim().Equals(string.Empty))
                objS.FatherName = txtFatherName.Text.Trim();
            if (!txtMotherName.Text.Trim().Equals(string.Empty))
                objS.MotherName = txtMotherName.Text.Trim();
            if (!txtDateOfBirth.Text.Trim().Equals(string.Empty))
                objS.Dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());

            if (rdoMale.Checked)
                objS.Sex = 'M';
            else
                objS.Sex = 'F';
            if (rdoMarriedNo.Checked)
                objS.Married = 'N';
            else
                objS.Married = 'Y';

            if (!txtPermanentAddress.Text.Trim().Equals(string.Empty))
                objSAddress.PADDRESS = txtPermanentAddress.Text.Trim();

            objS.IdNo = Convert.ToInt32(Session["idno"].ToString());
            if (!txtPIN.Text.Trim().Equals(string.Empty))
                objSAddress.PPINCODE = txtPIN.Text.Trim();
            if (!txtContactNumber.Text.Trim().Equals(string.Empty))
                objSAddress.PTELEPHONE = txtContactNumber.Text.Trim();
            if (!txtDateOfAdmission.Text.Trim().Equals(string.Empty))
                objS.AdmDate = Convert.ToDateTime(txtDateOfAdmission.Text.Trim());
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
            objS.PType = Convert.ToInt32(ddlPaymentType.SelectedValue);
            objS.ExamPtype = Convert.ToInt32(ddlPaymentType.SelectedValue); // only for gec project
            objS.Hosteler = Convert.ToInt32(rdoHostelerYes.Checked);
            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.SemesterNo = (objS.Year * 2) - 1; // calculating semester based on selected year

            objS.CollegeCode = Session["colcode"].ToString();
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = ViewState["ipAddress"].ToString();
            objS.IdType = Convert.ToInt32(ddlAdmType.SelectedValue);

            //////Photo

            if (fuPhotoUpload.HasFile)
                {
                objSPhoto.Photo1 = this.ResizePhoto(fuPhotoUpload);
                }
            else
                {
                objSPhoto.Photo1 = null;
                }

            //////objSPhoto.Photo1 = objCommon.GetImageData(fuPhotoUpload);
            if (fuSignUpload.HasFile)
                {
                objSPhoto.SignPhoto = this.ResizePhotoSign(fuSignUpload);
                }
            else
                {
                objSPhoto.SignPhoto = null;
                }
            objS.AdmroundNo = Convert.ToInt32(ddlAdmRound.SelectedValue);
            objS.PH = ddlPhyHandicap.SelectedValue;
            //**********************************
            if (ddlPhyHandicap.SelectedValue == "1")
                {
                objS.Physical_Handicap = Convert.ToInt32(ddlTypeofPhy.SelectedValue);
                objS.TypeofDisablity = txtdisablity.Text == "" ? string.Empty : txtdisablity.Text;
                }
            else
                {
                objS.TypeofDisablity = string.Empty;
                objS.Physical_Handicap = 0;
                }
            //**********************************

            //Check whether Masters are new entries
            objS.ReligionNo = Convert.ToInt32(ddlReligion.SelectedValue);
            //   objS.ReligionNo = Convert.ToInt32(objCommon.GetIDNo(txtReligion));
            //if (objS.ReligionNo == 0 && txtReligion.Text.Trim() != string.Empty)
            //    objS.ReligionNo = objCommon.AddMasterTableData("ACD_RELIGION", "RELIGIONNO", "RELIGION", txtReligion.Text.Trim().ToUpper(), 0);
            //if (objS.ReligionNo == -99) objS.ReligionNo = 0;

            objS.NationalityNo = Convert.ToInt32(ddlNationality.SelectedValue);
            //objS.NationalityNo = Convert.ToInt32(objCommon.GetIDNo(txtNationality));
            //if (objS.NationalityNo == 0 && txtNationality.Text.Trim() != string.Empty)
            //    objS.NationalityNo = objCommon.AddMasterTableData("ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", txtNationality.Text.Trim().ToUpper(), 0);
            //if (objS.NationalityNo == -99) objS.NationalityNo = 0;


            objSAddress.PCITY = Convert.ToInt32(ddlcity.SelectedValue);
            // objSAddress.PCITY = Convert.ToInt32(objCommon.GetIDNo(txtCity));
            //if (objSAddress.PCITY == 0 && txtCity.Text.Trim() != string.Empty)
            //    objSAddress.PCITY = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtCity.Text.Trim().ToUpper(), 0);
            //if (objSAddress.PCITY == -99) objSAddress.PCITY = 0;

            objS.CategoryNo = Convert.ToInt32(ddlCategory.SelectedValue);
            //objS.CategoryNo = Convert.ToInt32(objCommon.GetIDNo(txtCategory));
            //if (objS.CategoryNo == 0 && txtCategory.Text.Trim() != string.Empty)
            //    objS.CategoryNo = objCommon.AddMasterTableData("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", txtCategory.Text.Trim().ToUpper(), 0);
            //if (objS.CategoryNo == -99) objS.CategoryNo = 0;

            objSAddress.PSTATE = Convert.ToInt32(ddlstate.SelectedValue);
            //  objSAddress.PSTATE = Convert.ToInt32(objCommon.GetIDNo(txtState));
            //if (objSAddress.PSTATE == 0 && txtState.Text.Trim() != string.Empty)
            //    objSAddress.PSTATE = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtState.Text.Trim().ToUpper(), 0);
            //if (objSAddress.PSTATE == -99) objSAddress.PSTATE = 0;

            objS.StateNo = Convert.ToInt32(objCommon.GetIDNo(txtStateOfEligibility));
            //if (objS.StateNo == 0 && txtState.Text.Trim() != string.Empty)
            //    objS.StateNo = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtStateOfEligibility.Text.Trim().ToUpper(), 0);
            //if (objS.StateNo == -99) objS.StateNo = 0;

            objS.AdmCategoryNo = Convert.ToInt32(ddlAdmCategory.SelectedValue);
            //  objS.AdmCategoryNo = Convert.ToInt32(objCommon.GetIDNo(txtAdmCaste));
            //if (objS.AdmCategoryNo == 0 && txtAdmCaste.Text.Trim() != string.Empty)
            //    objS.AdmCategoryNo = objCommon.AddMasterTableData("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", txtAdmCaste.Text.Trim().ToUpper(), 0);
            //if (objS.AdmCategoryNo == -99) objS.AdmCategoryNo = 0;

            objS.BloodGroupNo = Convert.ToInt32(ddlBloodGrp.SelectedValue);
            // objS.BloodGroupNo = Convert.ToInt32(objCommon.GetIDNo(txtBloodGrp));
            //if (objS.BloodGroupNo == 0 && txtBloodGrp.Text.Trim() != string.Empty)
            //    objS.BloodGroupNo = objCommon.AddMasterTableData("ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", txtBloodGrp.Text.Trim().ToUpper(), 0);
            //if (objS.BloodGroupNo == -99) objS.BloodGroupNo = 0;


            //ENTRANCE EXAM DETAILS..
            objS.QUALIFYNO = ddlExamNo.SelectedValue;
            if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty))
                objS.ALLINDIARANK = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
            if (!txtYearOfExam.Text.Trim().Equals(string.Empty))
                objS.YearOfExam = txtYearOfExam.Text.Trim();
            if (!txtStateRank.Text.Trim().Equals(string.Empty))
                objS.STATERANK = Convert.ToInt32(txtStateRank.Text.Trim());
            if (!txtPer.Text.Trim().Equals(string.Empty))
                objS.Percentage = Convert.ToDecimal(txtPer.Text.Trim());
            if (!txtQExamRollNo.Text.Trim().Equals(string.Empty))
                objS.QexmRollNo = txtQExamRollNo.Text.Trim();
            if (!txtPercentile.Text.Trim().Equals(string.Empty))
                objS.PERCENTILE = Convert.ToDecimal(txtPercentile.Text.Trim());


            //ADD THE CODE FOR ONLY M.TECH SPOT ADMISSION
            if (ddlExamNo.SelectedValue == "9")
                {
                objS.GetScholarship = Convert.ToInt32(ddlSpotOption.SelectedValue);
                }
            else
                {
                objS.GetScholarship = 0;
                }

            //for Phd. student admission only

            int session = Convert.ToInt32(ddlsession.SelectedValue);
            int SupervisorName = Convert.ToInt32(ddlSupervisor.SelectedValue);
            int EmpIdGuide = 0;//Convert.ToInt32(txtEMployeeID.Text);
            //txtEMployeeID.Text = null ?  : txtEMployeeID.Text;
            //string EmpIdGuide = txtEMployeeID.Text.Trim();
            int StatusCat = Convert.ToInt32(ddlStatusCat.SelectedValue);
            string PerEmail = string.Empty;//txtPersonalemail.Text;
            if (!txtPersonalemail.Text.Trim().Equals(string.Empty))
                PerEmail = txtPersonalemail.Text.Trim();


            objS.PhdStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            objS.PhdSupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
            objS.PhdCoSupervisorNo1 = Convert.ToInt32(ddlCoSupevisor1.SelectedValue);
            objS.PhdCoSupervisorNo2 = Convert.ToInt32(ddlCoSupevisor2.SelectedValue);
            objS.TypeSupervisorNo = Convert.ToInt32(ddlSupervisorType.SelectedValue);
            objS.TypeCoSupervisorNo1 = Convert.ToInt32(ddlCoSupervisorType1.SelectedValue);
            objS.TypeCoSupervisorNo2 = Convert.ToInt32(ddlCoSupervisorType2.SelectedValue);

            if (Convert.ToInt32(rblNet.SelectedValue) == 0)
                {
                objS.Net = true;
                }
            else
                {
                objS.Net = false;
                }

            //ADD THE CONTAIN TO RELATED LOCAL ADDRESS
            if (!txtStudEmail.Text.Trim().Equals(string.Empty))
                objS.EmailID = txtStudEmail.Text.Trim();
            if (!txtPostalAddress.Text.Trim().Equals(string.Empty))
                objSAddress.LADDRESS = txtPostalAddress.Text.Trim();
            if (!txtGuardianPhone.Text.Trim().Equals(string.Empty))
                objSAddress.LTELEPHONE = txtGuardianPhone.Text.Trim();
            if (!txtGuardianMobile.Text.Trim().Equals(string.Empty))
                objSAddress.LMOBILE = txtGuardianMobile.Text.Trim();
            if (!txtGuardianEmail.Text.Trim().Equals(string.Empty))
                objSAddress.LEMAIL = txtGuardianEmail.Text.Trim();

            objS.ADMQUOTANO = Convert.ToInt32(ddlQuota.SelectedValue);

            objSAddress.LCITY = Convert.ToInt32(ddlLocalCity.SelectedValue);
            // objSAddress.LCITY = Convert.ToInt32(objCommon.GetIDNo(txtLocalCity));
            //if (objSAddress.LCITY == 0 && txtLocalCity.Text.Trim() != string.Empty)
            //    objSAddress.LCITY = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtLocalCity.Text.Trim().ToUpper(), 0);
            //if (objSAddress.LCITY == -99) objSAddress.LCITY = 0;

            objSAddress.LSTATE = Convert.ToInt32(ddlLocalState.SelectedValue);
            //objSAddress.LSTATE = Convert.ToInt32(objCommon.GetIDNo(txtLocalState));
            //if (objSAddress.LSTATE == 0 && txtLocalState.Text.Trim() != string.Empty)
            //    objSAddress.LSTATE = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtLocalState.Text.Trim().ToUpper(), 0);
            //if (objSAddress.LSTATE == -99) objSAddress.LSTATE = 0;

            objS.AadharCardNo = txtAddharno.Text.Trim();

            //----  phd modification ----15042019
            if (!txtinstitute.Text.Trim().Equals(string.Empty))
                objS.PhdInstitutename = txtinstitute.Text.Trim();
            objS.PhdNoc = rdbnocyes.Checked == true ? 1 : 0;

            //string output = objSC.AddStudent(objS, objSAddress, objSPhoto, objStud, RRNO, session, SupervisorName, EmpIdGuide, StatusCat, PerEmail);
            string output = objSC.UpdateStudentInfo(objS, objSAddress, objSPhoto, objStud, RRNO, session, SupervisorName, EmpIdGuide, StatusCat, PerEmail);
            if (output != "-99")
                {
                //GEC_Student objGecStud = new GEC_Student();

                //objGecStud.IdNo = Convert.ToInt32(output);
                //objGecStud.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                //objGecStud.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
                //objGecStud.CollegeCode = Session["colcode"].ToString();

                //btnSave.Enabled = false;
                //txtREGNo.Text = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + output);

                ////add code 24/07/2012
                ////Create DCR and print Challan
                //FeeDemand demandCriteria = this.GetDemandCriteria();
                //string studentIDs = objGecStud.IdNo.ToString();
                //bool overwriteDemand = true;

                //string demandno = objCommon.LookUp("ACD_DEMAND", "COUNT(*)", "IDNO=" + studentIDs.ToString() + " AND SEMESTERNO=" + objS.SemesterNo);
                //if (Convert.ToInt32(demandno) <= 0)
                //{
                //    string response = dmController.CreateDemandForStudents(studentIDs, demandCriteria, objS.SemesterNo, overwriteDemand);
                //}


                //string receiptno = this.GetNewReceiptNo();
                //FeeDemand dcr = this.GetDcrCriteria();
                //string dcritem = dmController.CreateDcrForStudents(studentIDs, dcr, 1, overwriteDemand, receiptno);

                //string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + objS.SemesterNo);


                //if (dcrNo != string.Empty && studentIDs != string.Empty)
                //{
                //    objRegistration.GenereateSingleEnrollmentNo(Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(output));
                //    this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
                //    //ShowReport("Admission_Slip_Report", "AdmissionSlipForPhd.rpt", output);
                //}

                //SENDING SMS

                ////string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                ////if (txtContactNumber.Text.ToString().Trim() != "")
                ////{endSMSToClient(_nitprm_constr, txtContactNumber.Text.Trim(), "Your Admission has been successfully completed, for more details College Admin Department.", "4");

                ////    objCommon.SendSMSToCl
                ////    //objCommon.Sient(_nitprm_constr, txtContactNumber.Text.Trim(), "Your Admission has been successfully completed" + txtStudentName.Text, "4");

                ////}

                //----------------------------------------------
                //ShowReport("Admission_Slip_Report", "AdmissionSlip.rpt", output);
                //DisableControlsRecursive(Page);


                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                if (ua_type == "1")
                    {
                    objCommon.DisplayMessage(upnldivmain, "Student Information Update Successfully.", Page);
                    clear();
                    }
                else
                    {

                    objCommon.DisplayMessage(upnldivmain, "Student Registration Done Successfully.", Page);
                    clear();
                    }

                }
            // Response.Redirect(Request.Url.ToString());
            }
        catch (Exception ex)
            {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistrationPhd.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }

        }

    private void DisableControlsRecursive(Control root)
        {
        if (root is TextBox)
            {
            ((TextBox)root).Text = string.Empty;
            }
        if (root is DropDownList)
            {
            ((DropDownList)root).SelectedIndex = 0;
            }
        foreach (Control child in root.Controls)
            {
            DisableControlsRecursive(child);
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        //string text6 = "You have received message from " ;
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" , "<script>showpop5('" + text6 + "')</script>", false);

        //clear();

        }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
        }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
        {
        try
            {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@Year=" + ddlYear.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistrationPhd.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }


    private DataTable GetDemandDraftDataTable()
        {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("IDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("BRANCHNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ADMBATCH", typeof(int)));
        dt.Columns.Add(new DataColumn("DDNO", typeof(string)));
        dt.Columns.Add(new DataColumn("DDDATE", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("DDAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("BANKNO", typeof(int)));
        dt.Columns.Add(new DataColumn("CITYNO", typeof(int)));
        dt.Columns.Add(new DataColumn("BANKNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CITYNAME", typeof(string)));

        dt.Columns.Add(new DataColumn("RECEIPTNO", typeof(int)));
        dt.Columns.Add(new DataColumn("RECEIPTAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("RECEIPTDATE", typeof(DateTime)));
        return dt;
        }

    private DataRow GetEditableDataRow(DataTable dt, string value)
        {
        DataRow dataRow = null;
        try
            {
            foreach (DataRow dr in dt.Rows)
                {
                if (dr["DDNO"].ToString() == value)
                    {
                    dataRow = dr;
                    break;
                    }
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistrationPhd.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            }
        return dataRow;
        }

    private void BindDDInfo(ref GEC_Student[] gecStudent)
        {
        DataTable dt;
        if (Session["DDINFO"] != null && ((DataTable)Session["DDINFO"]) != null)
            {
            int index = 0;
            dt = (DataTable)Session["DDINFO"];
            gecStudent = new GEC_Student[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
                {
                GEC_Student objGecStud = new GEC_Student();
                //objGecStud.IdNo = Convert.ToInt32(txtIDNo.Text.Trim());
                objGecStud.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                objGecStud.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
                objGecStud.DdNo = dr["DDNO"].ToString();
                objGecStud.Dddate = Convert.ToDateTime(dr["DDDATE"]);
                objGecStud.DdAmount = dr["DDAMOUNT"].ToString();
                objGecStud.BankNo = Convert.ToInt32(dr["BANKNO"]);
                objGecStud.cityNo = Convert.ToInt32(dr["CITYNO"]);
                objGecStud.ReceiptNo = Convert.ToInt32(dr["RECEIPTNO"]);
                objGecStud.ReceiptAmount = dr["RECEIPTAMOUNT"].ToString();
                objGecStud.ReceiptDate = Convert.ToDateTime(dr["RECEIPTDATE"]);
                objGecStud.CollegeCode = Session["colcode"].ToString();
                gecStudent[index] = objGecStud;
                index++;
                }
            }

        }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
        try
            {
            //btnSave.Enabled = false;
            this.ClearControls_DemandDraftDetails();
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "DEGREENO > 0", "QUALIFYNO");
            if (txtREGNo.Text.Trim() == string.Empty)
                {
                objCommon.DisplayMessage(this.updpnlUser, "Enter Registration No. to Modify!", this.Page);
                return;
                }

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_STU_ADDRESS B ON A.IDNO=B.IDNO", "A.IDNO", "A.REGNO,A.IDNO,A.ROLLNO,A.STUDNAME,A.FATHERNAME,A.MOTHERNAME,A.DOB,A.SEX,A.RELIGIONNO,A.MARRIED,A.NATIONALITYNO,A.CATEGORYNO,A.CASTE,A.ADMDATE,A.DEGREENO,A.BRANCHNO,A.YEAR, A.SEMESTERNO,A.PTYPE,A.STATENO,A.ADMBATCH,A.IDTYPE,A.YEAR_OF_EXAM,A.ALL_INDIA_RANK,A.STATE_RANK,A.PERCENTAGE,A.PERCENTILE,A.QEXMROLLNO,A.ADMCATEGORYNO,A.QUALIFYNO,A.SCHOLORSHIPTYPENO,A.PHYSICALLY_HANDICAPPED,A.TYPE_OF_PHYSICALLY_HANDICAP,A.TYPE_OF_PHYSICAL_DISABILITY,A.COLLEGE_CODE,A.PHDSTATUS,A.PHDSUPERVISORNO,A.PHDCOSUPERVISORNO1,A.PHDCOSUPERVISORNO2,A.TYPESUPERVISOR,A.TYPECOSUPERVISOR1,A.TYPECOSUPERVISOR2,B.STADDNO, B.IDNO, B.PADDRESS, B.PCITY, B.PSTATE, B.PPINCODE,A.EMAILID,B.PTELEPHONE,B.LADDRESS,B.LTELEPHONE,B.LMOBILE,B.LEMAIL,A.ADMQUOTANO,A.BLOODGRPNO,B.LCITY,B.LSTATE", " A.IDNO = " + txtREGNo.Text.Trim(), string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    int degree = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());

                    if (degree == 6)
                        {
                        //txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        txtStudentName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        txtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                        //txtAddharno.Text = dsStudent.Tables[0].Rows[0]["AADHARNO"].ToString();

                        if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                            {
                            rdoMale.Checked = true;
                            rdoFemale.Checked = false;
                            }
                        else
                            {
                            rdoFemale.Checked = true;
                            rdoMale.Checked = false;
                            }

                        txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                        if (dsStudent.Tables[0].Rows[0]["MARRIED"].ToString().Trim().Equals("Y"))
                            {
                            rdoMarriedYes.Checked = true;
                            rdoMarriedNo.Checked = false;
                            }
                        else
                            {
                            rdoMarriedYes.Checked = false;
                            rdoMarriedNo.Checked = true;
                            }

                        //ddlNationality.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString()), "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY"));
                        if (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString() == "" && ddlNationality.SelectedItem.Text == "Please Select")
                            {
                            ddlNationality.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlNationality.SelectedValue = dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString();
                            }
                        // ddlCategory.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString()), "ACD_CATEGORY", "CATEGORYNO", "CATEGORY"));
                        if (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString() == "" && ddlCategory.SelectedItem.Text == "Please Select")
                            {
                            ddlCategory.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlCategory.SelectedValue = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                            }

                        txtPermanentAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();

                        if (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == "" && ddlcity.SelectedItem.Text == "Please Select")
                            {
                            ddlcity.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlcity.SelectedValue = dsStudent.Tables[0].Rows[0]["PCITY"].ToString();
                            }

                        //ddlcity.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                        if (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == "" && ddlstate.SelectedItem.Text == "Please Select")
                            {
                            ddlstate.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlstate.SelectedValue = dsStudent.Tables[0].Rows[0]["PSTATE"].ToString();
                            }





                        // ddlstate.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                        txtPIN.Text = dsStudent.Tables[0].Rows[0]["PPINCODE"].ToString();
                        txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                        txtDateOfAdmission.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));

                        if (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == "" && ddlDegree.SelectedItem.Text == "Please Select")
                            {
                            ddlDegree.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlDegree.SelectedValue = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                            }
                        // ddlDegree.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()), "ACD_DEGREE", "DEGREENO", "DEGREENAME"));
                        // ddlSemester.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME"));
                        if (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() == "" && ddlSemester.SelectedItem.Text == "Please Select")
                            {
                            ddlSemester.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            }
                        //ddlReligion.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString()), "ACD_RELIGION", "RELIGIONNO", "RELIGION"));

                        if (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == "" && ddlReligion.SelectedItem.Text == "Please Select")
                            {
                            ddlReligion.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlReligion.SelectedValue = dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString();
                            }
                        // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 6 AND BRANCHNO > 0", "LONGNAME");
                        //  ddlBranch.SelectedValue = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), "ACD_BRANCH", "BRANCHNO", "LONGNAME"));

                        if (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == "" && ddlBranch.SelectedItem.Text == "Please Select")
                            {
                            ddlBranch.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlBranch.SelectedValue = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            }
                        if (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == "" && ddlYear.SelectedItem.Text == "Please Select")
                            {
                            ddlYear.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlYear.SelectedValue = dsStudent.Tables[0].Rows[0]["YEAR"].ToString();
                            }

                        if (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == "" && ddlBatch.SelectedItem.Text == "Please Select")
                            {
                            ddlBatch.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlBatch.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            }


                        //ddlBatch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                        //ddlYear.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["YEAR"].ToString()), "ACD_YEAR", "YEAR", "YEARNAME"));

                        // ddlPaymentType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()), "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME"));

                        if (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == "" && ddlPaymentType.SelectedItem.Text == "Please Select")
                            {
                            ddlPaymentType.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlPaymentType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();
                            }

                        if (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == "" && ddlAdmType.SelectedItem.Text == "Please Select")
                            {
                            ddlAdmType.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlAdmType.SelectedValue = dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString();
                            }

                        txtStateOfEligibility.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                        //ddlAdmType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()), "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION"));
                        txtAllIndiaRank.Text = dsStudent.Tables[0].Rows[0]["ALL_INDIA_RANK"].ToString();
                        txtYearOfExam.Text = dsStudent.Tables[0].Rows[0]["YEAR_OF_EXAM"].ToString();
                        txtStateRank.Text = dsStudent.Tables[0].Rows[0]["STATE_RANK"].ToString();
                        txtPer.Text = dsStudent.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                        txtQExamRollNo.Text = dsStudent.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                        txtPercentile.Text = dsStudent.Tables[0].Rows[0]["PERCENTILE"].ToString();
                        // ddlAdmCategory.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString()), "ACD_CATEGORY", "CATEGORYNO", "CATEGORY"));

                        if (dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString() == "" && ddlAdmCategory.SelectedItem.Text == "Please Select")
                            {
                            ddlAdmCategory.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlAdmCategory.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString();
                            }

                        if (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString() == "" && ddlExamNo.SelectedItem.Text == "Please Select")
                            {
                            ddlExamNo.SelectedValue = "0";
                            }
                        else
                            {
                            //objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0", "QUALIFYNO");
                            ddlExamNo.SelectedValue = dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString();
                            }


                        //ddlExamNo.SelectedValue = (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString());

                        txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        txtPostalAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                        txtGuardianPhone.Text = dsStudent.Tables[0].Rows[0]["LTELEPHONE"].ToString();
                        txtGuardianMobile.Text = dsStudent.Tables[0].Rows[0]["LMOBILE"].ToString();
                        txtGuardianEmail.Text = dsStudent.Tables[0].Rows[0]["LEMAIL"].ToString();

                        if (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString() == "" && ddlBloodGrp.SelectedItem.Text == "Please Select")
                            {
                            ddlBloodGrp.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlBloodGrp.SelectedValue = dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                            }
                        // ddlBloodGrp.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString()), "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME"));

                        if (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == "" && ddlQuota.SelectedItem.Text == "Please Select")
                            {
                            ddlQuota.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlQuota.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString();
                            }
                        // ddlQuota.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString()), "ACD_QUOTA", "QUOTANO", "QUOTA"));

                        if (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == "" && ddlLocalCity.SelectedItem.Text == "Please Select")
                            {
                            ddlLocalCity.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlLocalCity.SelectedValue = dsStudent.Tables[0].Rows[0]["LCITY"].ToString();
                            }
                        //ddlLocalCity.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                        if (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == "" && ddlLocalState.SelectedItem.Text == "Please Select")
                            {
                            ddlLocalState.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlLocalState.SelectedValue = dsStudent.Tables[0].Rows[0]["LSTATE"].ToString();
                            }

                        // ddlLocalState.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));

                        //PHYSICAL HADICAPP
                        ddlPhyHandicap.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString());
                        if (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString() == "1")
                            {
                            //ViewState["IsHandicap"] = "1";
                            ph.Visible = true;
                            ph1.Visible = true;
                            dis.Visible = true;
                            dis1.Visible = true;
                            ddlTypeofPhy.SelectedValue = dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICALLY_HANDICAP"] == null ? "0" : dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICALLY_HANDICAP"].ToString();//****************
                            txtdisablity.Text = dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICAL_DISABILITY"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICAL_DISABILITY"].ToString();//****************
                            }


                        //show the student photo and sign code
                        int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString())));
                        if (idno > 0)
                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=student";
                        ImgSign.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=studentsign";

                        //ADD SPOT OPTION FOR MTECH SPOT OPTION
                        ddlSpotOption.SelectedValue = (dsStudent.Tables[0].Rows[0]["SCHOLORSHIPTYPENO"].ToString());
                        ddlStatus.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHDSTATUS"].ToString());

                        // ddlSupervisor.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PHDSUPERVISORNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PHDSUPERVISORNO"].ToString()), "acd_phd_supervisor", "SUPERVISORNO", "SUPERVISORNAME"));
                        ddlSupervisor.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                        ddlCoSupevisor1.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PHDCOSUPERVISORNO1"].ToString());
                        ddlCoSupevisor2.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PHDCOSUPERVISORNO2"].ToString());
                        ddlSupervisorType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["TYPESUPERVISOR"].ToString());
                        ddlCoSupervisorType1.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["TYPECOSUPERVISOR1"].ToString());
                        ddlCoSupervisorType2.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["TYPECOSUPERVISOR2"].ToString());


                        //  === phd modification ============15042019
                        rdbnocyes.Checked = dsStudent.Tables[0].Rows[0]["PHDNOC"].ToString() == "1" ? true : false;
                        rdbnocno.Checked = dsStudent.Tables[0].Rows[0]["PHDNOC"].ToString() == "1" ? false : true;
                        //ddlStatus_SelectedIndexChanged(sender, e);
                        txtinstitute.Text = dsStudent.Tables[0].Rows[0]["INSTITUTENMAE"].ToString();
                        //M.TECH SPOT ADMISSION AMOUNT TO BE PAID FOR DD
                        //double ddAmount = Convert.ToDouble(objCommon.LookUp("ACD_DCR", "EXCESS_AMOUNT", "IDNO=" + txtREGNo.Text.Trim()));
                        double ddAmount = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT", "isnull(CSAB_AMT,0)", "IDNO=" + txtREGNo.Text.Trim()));
                        txtDDAmountPaid.Text = ddAmount.ToString();

                        int spotoption = Convert.ToInt32(ddlSpotOption.SelectedValue);
                        if (spotoption > 0)
                            {
                            trSpotOption.Visible = true;

                            }
                        else
                            {
                            trSpotOption.Visible = false;
                            }
                        trReconcile.Visible = true;
                        trPaytype.Visible = true;
                        trmsg.Visible = true;

                        // btnSave.Enabled = false;
                        btnReport.Enabled = true;

                        int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + txtREGNo.Text.Trim()));
                        if (count > 0)
                            {
                            btnChallan.Visible = true;
                            }
                        else
                            {
                            btnChallan.Visible = false;
                            }


                        string chk = objCommon.LookUp("ACD_DCR", "RECON", "IDNO=" + idno);
                        if (chk == "True")
                            {
                            trReconcile.Visible = false;
                            trPaytype.Visible = false;
                            trmsg.Visible = false;
                            }
                        else
                            {
                            trReconcile.Visible = true;
                            trPaytype.Visible = true;
                            }
                        }
                    else
                        {
                        objCommon.DisplayMessage(this.updpnlUser, "Please Enter Only Phd Registration No.!", this.Page);
                        }
                    }
                else
                    {
                    // objCommon.DisplayMessage(this.updpnlUser, "Please Enter Valid Registration No.!", this.Page);
                    }
                }
            else
                {
                //objCommon.DisplayMessage(this.updpnlUser, "Please Enter Valid Registration No.!", this.Page);
                }
            }
        catch (Exception ex)
            {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistrationPhd.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }


    protected void btnReport_Click(object sender, EventArgs e)
        {
        try
            {
            GEC_Student objGecStud = new GEC_Student();

            objGecStud.RegNo = txtREGNo.Text.Trim().ToString();
            objGecStud.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objGecStud.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
            objGecStud.CollegeCode = Session["colcode"].ToString();
            string output = objGecStud.RegNo;

            //if (rblReconcile.SelectedValue == "0")
            //{
            //    feeController.ReconcileData(Convert.ToInt32(txtREGNo.Text.Trim()));
            //}

            //ShowReport("Admission_Slip_Report", "AdmissionSlipForMtech.rpt", output);




            if (rblReconcile.SelectedIndex == 0)
                {
                string chk = objCommon.LookUp("ACD_DCR", "RECON", "IDNO=" + Convert.ToInt32(objGecStud.RegNo));

                if (chk == "True")
                    {
                    trPaytype.Visible = false;
                    //ShowReport("Admission_Slip_Report", "AdmissionSlipForPhd.rpt", output);
                    ShowReport("Admission_Slip_Report", "AdmissionSlipForMtech.rpt", output);
                    }
                else
                    {
                    if (txtPayType.Text != string.Empty)
                        {
                        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(objGecStud.RegNo));
                        if (dcrNo != string.Empty && objGecStud.RegNo != string.Empty)
                            {

                            // check for the DD and SBI-Collect
                            if (txtPayType.Text.Trim() == "D" || txtPayType.Text.Trim() == "I")
                                {
                                DemandModificationController dmController = new DemandModificationController();
                                string amount = txtDDAmount.Text;
                                string bankname = ddlBank.SelectedItem.Text;
                                string bankno = ddlBank.SelectedValue;
                                string date = txtDDDate.Text.Trim();
                                string city = txtDDCity.Text.Trim();
                                string ddno = txtDDNo.Text.Trim();
                                string collegecode = Session["colcode"].ToString();
                                int sessionno = Convert.ToInt32(Session["currentsession"]);
                                int semesterno = 1;
                                string paytype = txtPayType.Text.Trim();

                                //string output = dmController.InserDDData(Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs),ddno,Convert.ToDateTime(date),Convert.ToInt32(bankno),bankname, amount,city,collegecode);
                                int output1 = dmController.InserDDDataSemPramote(Convert.ToInt32(dcrNo), Convert.ToInt32(objGecStud.RegNo), ddno, Convert.ToDateTime(date), Convert.ToInt32(bankno), bankname, amount, city, collegecode, sessionno, semesterno, paytype, txtRemarkunder.Text);
                                }
                            feeController.ReconcileData(Convert.ToInt32(objGecStud.RegNo));
                            // ShowReport("Admission_Slip_Report", "AdmissionSlipForPhd.rpt", output);



                            string ptype = objCommon.LookUp("ACD_STUDENT", "PTYPE", "IDNO=" + Convert.ToInt32(objGecStud.RegNo));
                            if (txtPayType.Text.Trim() == "U")
                                {
                                if (ptype == "2")
                                    {
                                    if (txtRemarkunder.Text == "")
                                        {
                                        objCommon.DisplayMessage("Please Enter Remark For Undertaking....", this.Page);

                                        }
                                    else
                                        {
                                        feeController.UpdateReconcileDateUnder(Convert.ToInt32(objGecStud.RegNo), Convert.ToDateTime(txtUnder.Text.Trim()), txtRemarkunder.Text, Convert.ToInt32(Session["currentsession"].ToString()));
                                        }
                                    }
                                else
                                    {
                                    objCommon.DisplayMessage("Undertaking(U) is only for SC/ST !!....", this.Page);

                                    }
                                }
                            if (txtPayType.Text.Trim() == "U")
                                {
                                if (ptype == "2")
                                    {
                                    if (txtRemarkunder.Text != "" && txtUnder.Text != "")
                                        {

                                        ShowReport("Admission_Slip_Report", "AdmissionSlipForMtech.rpt", output);
                                        }
                                    }
                                }
                            else
                                {
                                ShowReport("Admission_Slip_Report", "AdmissionSlipForMtech.rpt", output);
                                }
                            }
                        else
                            {
                            // objCommon.DisplayMessage(UpdatePanel1, "Please paid the Fees", this.Page);
                            }
                        }
                    else
                        {
                        objCommon.DisplayMessage(UpdpayType, "Please enter type of payment whether cash(C) or demand draft(D).", this.Page);
                        }
                    }
                }
            else
                {
                //objCommon.DisplayMessage(UpdatePanel1, "Please Choose the Payment Status YES for Reconcilation", this.Page);
                }

            }
        catch (Exception ex)
            {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistrationPhd.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    //get the new receipt No.
    private string GetNewReceiptNo()
        {
        string receiptNo = string.Empty;

        try
            {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRegistrationPhd.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        return receiptNo;
        }

    private FeeDemand GetDcrCriteria()
        {
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
            {
            int sem = (Convert.ToInt32(ddlYear.SelectedValue) * 2) - 1;
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = Convert.ToInt16(ddlBranch.SelectedValue);
            dcrCriteria.SemesterNo = sem;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            //dcrCriteria.UserNo = 7;
            if (txtDDAmountPaid.Text == "")
                {
                dcrCriteria.ExcessAmount = 0;
                }
            else
                {
                dcrCriteria.ExcessAmount = Convert.ToDouble(txtDDAmountPaid.Text.Trim());
                }
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentRegistrationPhd.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        return dcrCriteria;
        }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
        {
        try
            {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRegistrationPhd.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
        {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        //string param = "@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        //param += ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        //param += ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        //param += ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        //return param;
        string collegeCode = "8";

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + collegeCode + "";
        return param;
        }

    private FeeDemand GetDemandCriteria()
        {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();

        try
            {
            int sem = (Convert.ToInt32(ddlYear.SelectedValue) * 2) - 1;
            demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            demandCriteria.SemesterNo = sem;
            demandCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentRegistrationPhd.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        return demandCriteria;
        }

    //protected void ddlExamNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlExamNo.SelectedValue == "17" || ddlExamNo.SelectedValue == "15")
    //    {
    //        trNet.Visible = true;
    //    }
    //    else
    //    {
    //        trNet.Visible = false;
    //    }


    //}

    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //int deptno = Convert.ToInt32( objCommon.LookUp("ACD_BRANCH", "DEPTNO", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)));
    //    //objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "DEPTNO=" + deptno, "SUPERVISORNAME");
    //    //ddlCoSupevisor1.Focus();
    //    //objCommon.FillDropDownList(ddlCoSupevisor1, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "DEPTNO=" + deptno, "SUPERVISORNAME");
    //    //objCommon.FillDropDownList(ddlCoSupevisor2, "ACD_PHD_SUPERVISOR", "SUPERVISORNO", "SUPERVISORNAME", "DEPTNO=" + deptno, "SUPERVISORNAME");
    //}

    //protected void ddlSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlSupervisorType.Text = "0";// objCommon.LookUp("ACD_PHD_SUPERVISOR", "TYPENAME", "SUPERVISORNO=" + ddlSupervisor.SelectedValue);
    //    ddlSupervisorType.Focus();
    //}

    protected void ddlCoSupevisor1_SelectedIndexChanged(object sender, EventArgs e)
        {
        ddlCoSupervisorType1.Text = objCommon.LookUp("ACD_PHD_SUPERVISOR", "TYPENAME", "SUPERVISORNO=" + ddlCoSupevisor1.SelectedValue);
        ddlCoSupervisorType1.Focus();
        }

    protected void ddlCoSupevisor2_SelectedIndexChanged(object sender, EventArgs e)
        {
        ddlCoSupervisorType2.Text = objCommon.LookUp("ACD_PHD_SUPERVISOR", "TYPENAME", "SUPERVISORNO=" + ddlCoSupevisor2.SelectedValue);
        ddlCoSupervisorType2.Focus();
        }

    public byte[] ResizePhoto(FileUpload fu)
        {

        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
            {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);
            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
                {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
                }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
            }
        return image;
        }

    public byte[] ResizePhotoSign(FileUpload fu)
        {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
            {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);
            //string strExtension = System.IO.Path.GetExtension(hdfSignUpload.Value);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
                {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
                }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
            }
        return image;
        }

    private void ClearControls_DemandDraftDetails()
        {
        txtDDNo.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDCity.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtPayType.Text = string.Empty;
        txtCashDate.Text = string.Empty;
        }

    protected void btnChallan_Click(object sender, EventArgs e)
        {
        string studentIDs = txtREGNo.Text.Trim();
        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(txtREGNo.Text.Trim()) + " AND SEMESTERNO=1");

        if (dcrNo != string.Empty)
            {
            this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
            }
        }

    //protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlPaymentType.SelectedValue) > 0)
    //    {
    //        if (Convert.ToInt32(ddlPaymentType.SelectedValue) == 5)
    //        {
    //            ddlStatus.SelectedValue = "2";
    //            ddlStatus_SelectedIndexChanged(sender, e);

    //        }
    //        else if (Convert.ToInt32(ddlPaymentType.SelectedValue) == 3)
    //        {
    //            ddlStatus.SelectedValue = "1";
    //        }
    //        else if (Convert.ToInt32(ddlPaymentType.SelectedValue) == 7)
    //        {
    //            ddlStatus.SelectedValue = "2";
    //            ddlStatus_SelectedIndexChanged(sender, e);
    //        }
    //    }
    //    else
    //    {
    //        ddlStatus.SelectedValue = "0";
    //    }
    //}
    protected void ddlPhyHandicap_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            if (ddlPhyHandicap.SelectedValue == "1")
                {
                ph.Visible = true;
                ph1.Visible = true;
                dis.Visible = true;
                dis1.Visible = true;
                //upnldivmain.Visible = true;
                upnldivmain.Visible = true;
                updEdit.Visible = false;
                }
            else
                {
                ph.Visible = false;
                ph1.Visible = false;
                dis.Visible = false;
                dis1.Visible = false;
                //upnldivmain.Visible = true;
                //updEdit.Visible = false;
                upnldivmain.Visible = true;

                }
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentRegistrationPhd.ddlPhyHandicap_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlStatus.SelectedValue == "2")
    //    {
    //        divnoc.Visible = true;            
    //    }
    //    else
    //    {
    //        divnoc.Visible = false;
    //    }
    //}


    protected void btnSearch_Click(object sender, EventArgs e)
        {
        //// Panel1.Visible = true;
        // lblNoRecords.Visible = true;
        // //divbranch.Attributes.Add("style", "display:none");
        // //divSemester.Attributes.Add("style", "display:none");
        // //divtxt.Attributes.Add("style", "display:none");
        // string value = string.Empty;
        // if (ddlDropdown.SelectedIndex > 0)
        // {
        //     value = ddlDropdown.SelectedValue;
        // }
        // else
        // {
        //     value = txtSearch.Text;
        // }

        // //ddlSearch.ClearSelection();

        // bindlist(ddlSearch.SelectedItem.Text, value);
        // ddlDropdown.ClearSelection();
        // txtSearch.Text = string.Empty;
        //// div_Studentdetail.Visible = false;
        // //divMSG.Visible = false;
        // //btnPayment.Visible = false;
        //// btnReciept.Visible = false;
        //// divPreviousReceipts.Visible = false;
        // //if (value == "BRANCH")
        // //{
        // //    divbranch.Attributes.Add("style", "display:block");

        // //}
        // //else if (value == "SEM")
        // //{
        // //    divSemester.Attributes.Add("style", "display:block");
        // //}
        // //else
        // //{
        // //    divtxt.Attributes.Add("style", "display:block");
        // //}

        // //ShowDetails();
        // Panel3.Visible = true;

        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            {
            value = ddlDropdown.SelectedValue;
            }
        else
            {
            value = txtSearch.Text;
            }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;

        }


    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
                {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                    {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                        {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;


                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
                        }
                    else
                        {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                        }
                    }
                }
            else
                {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

                }
            }
        catch
            {
            throw;
            }
        txtSearch.Text = string.Empty;
        }



    private void bindlist(string category, string searchtext)
        {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
            {
            Panellistview.Visible = true;
            // divReceiptType.Visible = false;
            //  divStudSemester.Visible = false;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
        else
            {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            }
        }

    protected void lnkId_Click(object sender, EventArgs e)
        {
        LinkButton lnk = sender as LinkButton;
        //string url = string.Empty;
        //if (Request.Url.ToString().IndexOf("&id=") > 0)
        //    url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        //else
        //    url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();

        //if (lnk.CommandArgument == null)
        //{
        //    string number = Session["StudId"].ToString();
        //    Session["stuinfoidno"] = Convert.ToInt32 (number);
        //}
        //else
        //{
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        Session["idno"] = Session["stuinfoidno"].ToString();

        //DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

        //Server.Transfer("PersonalDetails.aspx", false);
        // DisplayInformation(Convert.ToInt32(Session["stuinfoidno"]));
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        divmain.Visible = true;

        ShowStudentDetails();

        divmain.Visible = true;
        // DivSutLog.Visible = true;
        //divGeneralInfo.Visible = true;
        updEdit.Visible = false;
        Panellistview.Visible = false;

        }
    protected void btnClose_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());
        
        }


    public void ShowStudentDetails()
        {


        try
            {
            //btnSave.Enabled = false;
            this.ClearControls_DemandDraftDetails();
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "DEGREENO > 0", "QUALIFYNO");
            //if (txtREGNo.Text.Trim() == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.updpnlUser, "Enter Registration No. to Modify!", this.Page);
            //    return;
            //}

            //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_STU_ADDRESS B ON A.IDNO=B.IDNO INNER JOIN ACD_PHD_DGC PHD ON PHD.IDNO=A.IDNO LEFT JOIN ACD_STUD_PHOTO P ON (P.IDNO=A.IDNO)", "PHD.IDNO", "A.REGNO,A.IDNO,A.ROLLNO,A.STUDNAME,A.FATHERNAME,A.MOTHERNAME,A.DOB,A.SEX,A.RELIGIONNO,A.MARRIED,A.NATIONALITYNO,A.CATEGORYNO,A.CASTE,A.ADMDATE,A.DEGREENO,A.BRANCHNO,A.YEAR, A.SEMESTERNO,A.PTYPE,A.STATENO,A.ADMBATCH,A.IDTYPE,A.YEAR_OF_EXAM,A.ALL_INDIA_RANK,A.STATE_RANK,A.PERCENTAGE,A.PERCENTILE,A.QEXMROLLNO,A.ADMCATEGORYNO,A.QUALIFYNO,A.SCHOLORSHIPTYPENO,A.PHYSICALLY_HANDICAPPED,A.TYPE_OF_PHYSICALLY_HANDICAP,A.TYPE_OF_PHYSICAL_DISABILITY,A.COLLEGE_CODE,A.PHDSTATUS,A.PHDSUPERVISORNO,A.PHDCOSUPERVISORNO1,A.PHDCOSUPERVISORNO2,A.TYPESUPERVISOR,A.TYPECOSUPERVISOR1,A.TYPECOSUPERVISOR2,B.STADDNO, B.IDNO, B.PADDRESS, B.PCITY, B.PSTATE, B.PPINCODE,A.EMAILID,B.PTELEPHONE,B.LADDRESS,B.LTELEPHONE,B.LMOBILE,B.LEMAIL,A.ADMQUOTANO,A.BLOODGRPNO,B.LCITY,B.LSTATE,PHD.SUPERVISORNO,PHD.SCHOOLNAME,PHD.MODE,PHD.DRCSTATUS,PHD.EMPIDGUIDE,PHD.OFFICALEMAIL,PHD.PERSONALEMAIL,PHD.SESSION,PHD.INSTITUTENAME,P.PHOTO,P.STUD_SIGN", " PHD.IDNO = " + Session["idno"], string.Empty);

            StudentController objSC = new StudentController();
            DataSet dsStudent = objSC.GetPhdStudentInfoNew(Convert.ToInt32(Session["idno"].ToString()));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    int degree = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());

                    //if (degree == 6)
                        {
                        //txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        txtStudentName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        txtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                        //txtAddharno.Text = dsStudent.Tables[0].Rows[0]["AADHARNO"].ToString();
                        txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                            {
                            rdoMale.Checked = true;
                            rdoFemale.Checked = false;
                            }
                        else
                            {
                            rdoFemale.Checked = true;
                            rdoMale.Checked = false;
                            }

                        txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                        if (dsStudent.Tables[0].Rows[0]["MARRIED"].ToString().Trim().Equals("Y"))
                            {
                            rdoMarriedYes.Checked = true;
                            rdoMarriedNo.Checked = false;
                            }
                        else
                            {
                            rdoMarriedYes.Checked = false;
                            rdoMarriedNo.Checked = true;
                            }

                        //ddlNationality.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString()), "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY"));
                        if (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString() == "" && ddlNationality.SelectedItem.Text == "Please Select")
                            {
                            ddlNationality.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlNationality.SelectedValue = dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString();
                            }
                        // ddlCategory.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString()), "ACD_CATEGORY", "CATEGORYNO", "CATEGORY"));
                        if (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString() == "" && ddlCategory.SelectedItem.Text == "Please Select")
                            {
                            ddlCategory.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlCategory.SelectedValue = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                            }

                        txtPermanentAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();

                        if (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == "" && ddlcity.SelectedItem.Text == "Please Select")
                            {
                            ddlcity.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlcity.SelectedValue = dsStudent.Tables[0].Rows[0]["PCITY"].ToString();
                            }

                        //ddlcity.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                        if (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == "" && ddlstate.SelectedItem.Text == "Please Select")
                            {
                            ddlstate.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlstate.SelectedValue = dsStudent.Tables[0].Rows[0]["PSTATE"].ToString();
                            }

                        // ddlstate.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                        txtPIN.Text = dsStudent.Tables[0].Rows[0]["PPINCODE"].ToString();
                        txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                        txtDateOfAdmission.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));

                        if (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == "" && ddlDegree.SelectedItem.Text == "Please Select")
                            {
                            ddlDegree.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlDegree.SelectedValue = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                            }
                        // ddlDegree.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()), "ACD_DEGREE", "DEGREENO", "DEGREENAME"));
                        // ddlSemester.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()), "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME"));
                        if (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString() == "" && ddlSemester.SelectedItem.Text == "Please Select")
                            {
                            ddlSemester.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            }
                        //ddlReligion.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString()), "ACD_RELIGION", "RELIGIONNO", "RELIGION"));

                        if (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == "" && ddlReligion.SelectedItem.Text == "Please Select")
                            {
                            ddlReligion.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlReligion.SelectedValue = dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString();
                            }
                        // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = 6 AND BRANCHNO > 0", "LONGNAME");
                        //  ddlBranch.SelectedValue = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), "ACD_BRANCH", "BRANCHNO", "LONGNAME"));

                        if (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == "" && ddlYear.SelectedItem.Text == "Please Select")
                            {
                            ddlYear.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlYear.SelectedValue = dsStudent.Tables[0].Rows[0]["YEAR"].ToString();
                            }

                        if (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == "" && ddlBatch.SelectedItem.Text == "Please Select")
                            {
                            ddlBatch.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlBatch.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                            }



                        if (dsStudent.Tables[0].Rows[0]["SESSION"].ToString() == "" && ddlsession.SelectedItem.Text == "Please Select")
                            {
                            ddlsession.SelectedIndex = 0;
                            }

                        else
                            {
                            ddlsession.SelectedValue = dsStudent.Tables[0].Rows[0]["SESSION"].ToString();
                            }

                        txtPersonalemail.Text = dsStudent.Tables[0].Rows[0]["PERSONALEMAIL"].ToString();
                        txtEMployeeID.Text = dsStudent.Tables[0].Rows[0]["EMPIDGUIDE"].ToString();




                        //ddlBatch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                        //ddlYear.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["YEAR"].ToString()), "ACD_YEAR", "YEAR", "YEARNAME"));

                        // ddlPaymentType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()), "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME"));

                        if (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == "" && ddlPaymentType.SelectedItem.Text == "Please Select")
                            {
                            ddlPaymentType.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlPaymentType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();
                            }

                        if (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == "" && ddlAdmType.SelectedItem.Text == "Please Select")
                            {
                            ddlAdmType.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlAdmType.SelectedValue = dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString();
                            }

                        txtStateOfEligibility.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                        //ddlAdmType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()), "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION"));
                        txtAllIndiaRank.Text = dsStudent.Tables[0].Rows[0]["ALL_INDIA_RANK"].ToString();
                        txtYearOfExam.Text = dsStudent.Tables[0].Rows[0]["YEAR_OF_EXAM"].ToString();
                        txtStateRank.Text = dsStudent.Tables[0].Rows[0]["STATE_RANK"].ToString();
                        txtPer.Text = dsStudent.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                        txtQExamRollNo.Text = dsStudent.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                        txtPercentile.Text = dsStudent.Tables[0].Rows[0]["PERCENTILE"].ToString();
                        // ddlAdmCategory.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString()), "ACD_CATEGORY", "CATEGORYNO", "CATEGORY"));

                        if (dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString() == "" && ddlAdmCategory.SelectedItem.Text == "Please Select")
                            {
                            ddlAdmCategory.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlAdmCategory.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString();
                            }



                        if (dsStudent.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" && ddlStatus.SelectedItem.Text == "Please Select")
                            {
                            ddlStatus.SelectedValue = "0";
                            }
                        else
                            {
                            // objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0", "QUALIFYNO");
                            ddlStatus.SelectedValue = dsStudent.Tables[0].Rows[0]["DRCSTATUS"].ToString();

                            }


                        // string a = dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString();


                        if (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString() == "" && ddlExamNo.SelectedItem.Text == "Please Select")
                            {
                            ddlExamNo.SelectedValue = "0";
                            }
                        else
                            {
                            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0", "QUALIFYNO");
                            ddlExamNo.SelectedValue = dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString();
                            }


                        //ddlExamNo.SelectedValue = (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString());

                        txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        txtPostalAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                        txtGuardianPhone.Text = dsStudent.Tables[0].Rows[0]["LTELEPHONE"].ToString();
                        txtGuardianMobile.Text = dsStudent.Tables[0].Rows[0]["LMOBILE"].ToString();
                        txtGuardianEmail.Text = dsStudent.Tables[0].Rows[0]["LEMAIL"].ToString();

                        if (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString() == "" && ddlBloodGrp.SelectedItem.Text == "Please Select")
                            {
                            ddlBloodGrp.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlBloodGrp.SelectedValue = dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                            }
                        // ddlBloodGrp.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString()), "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME"));

                        if (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == "" && ddlQuota.SelectedItem.Text == "Please Select")
                            {
                            ddlQuota.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlQuota.SelectedValue = dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString();
                            }
                        // ddlQuota.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString()), "ACD_QUOTA", "QUOTANO", "QUOTA"));

                        if (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == "" && ddlLocalCity.SelectedItem.Text == "Please Select")
                            {
                            ddlLocalCity.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlLocalCity.SelectedValue = dsStudent.Tables[0].Rows[0]["LCITY"].ToString();
                            }
                        //ddlLocalCity.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                        if (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == "" && ddlLocalState.SelectedItem.Text == "Please Select")
                            {
                            ddlLocalState.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlLocalState.SelectedValue = dsStudent.Tables[0].Rows[0]["LSTATE"].ToString();
                            }


                        if (dsStudent.Tables[0].Rows[0]["DRCSTATUS"].ToString() == "" && ddlLocalState.SelectedItem.Text == "Please Select")
                            {
                            ddlStatusCat.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlStatusCat.SelectedValue = dsStudent.Tables[0].Rows[0]["DRCSTATUS"].ToString();
                            }

                        // ddlLocalState.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));

                        //PHYSICAL HADICAPP
                        ddlPhyHandicap.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString());
                        if (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString() == "1")
                            {
                            //ViewState["IsHandicap"] = "1";
                            ph.Visible = true;
                            ph1.Visible = true;
                            dis.Visible = true;
                            dis1.Visible = true;
                            ddlTypeofPhy.SelectedValue = dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICALLY_HANDICAP"] == null ? "0" : dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICALLY_HANDICAP"].ToString();//****************
                            txtdisablity.Text = dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICAL_DISABILITY"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TYPE_OF_PHYSICAL_DISABILITY"].ToString();//****************
                            }
                        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
                        if (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == "" && ddlBranch.SelectedItem.Text == "Please Select")
                            {
                            ddlBranch.SelectedIndex = 0;
                            }
                        else
                            {
                            ddlBranch.SelectedValue = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            }

                        //show the student photo and sign code
                        int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString())));
                        if (idno > 0)

                            imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=student";
                        ImgSign.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=studentsign";

                        //objCommon.LookUp("ACD_STUD_PHOTO", "PHOTO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()));


                        //ADD SPOT OPTION FOR MTECH SPOT OPTION
                        ddlSpotOption.SelectedValue = (dsStudent.Tables[0].Rows[0]["SCHOLORSHIPTYPENO"].ToString());
                        ddlStatus.SelectedValue = (dsStudent.Tables[0].Rows[0]["DRCSTATUS"].ToString());

                        // ddlSupervisor.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PHDSUPERVISORNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PHDSUPERVISORNO"].ToString()), "acd_phd_supervisor", "SUPERVISORNO", "SUPERVISORNAME"));
                        ddlSupervisor.SelectedValue = (dsStudent.Tables[0].Rows[0]["SUPERVISORNO"].ToString());
                        ddlCoSupevisor1.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PHDCOSUPERVISORNO1"].ToString());
                        ddlCoSupevisor2.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PHDCOSUPERVISORNO2"].ToString());
                        ddlSupervisorType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["TYPESUPERVISOR"].ToString());
                        ddlCoSupervisorType1.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["TYPECOSUPERVISOR1"].ToString());
                        ddlCoSupervisorType2.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["TYPECOSUPERVISOR2"].ToString());


                        //  === phd modification ============15042019
                        // rdbnocyes.Checked = dsStudent.Tables[0].Rows[0]["PHDNOC"].ToString() == "1" ? true : false;
                        // rdbnocno.Checked = dsStudent.Tables[0].Rows[0]["PHDNOC"].ToString() == "1" ? false : true;
                        //ddlStatus_SelectedIndexChanged(sender, e);
                        txtinstitute.Text = dsStudent.Tables[0].Rows[0]["INSTITUTENAME"].ToString();
                        //M.TECH SPOT ADMISSION AMOUNT TO BE PAID FOR DD
                        //double ddAmount = Convert.ToDouble(objCommon.LookUp("ACD_DCR", "EXCESS_AMOUNT", "IDNO=" + txtREGNo.Text.Trim()));
                        double ddAmount = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT", "isnull(CSAB_AMT,0)", "IDNO=" + txtREGNo.Text.Trim()));
                        txtDDAmountPaid.Text = ddAmount.ToString();

                        int spotoption = Convert.ToInt32(ddlSpotOption.SelectedValue);
                        if (spotoption > 0)
                            {
                            trSpotOption.Visible = true;

                            }
                        else
                            {
                            trSpotOption.Visible = false;
                            }
                        trReconcile.Visible = true;
                        trPaytype.Visible = true;
                        trmsg.Visible = true;

                        //btnSave.Enabled = false;
                        btnReport.Enabled = true;

                        int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "IDNO=" + txtREGNo.Text.Trim()));
                        if (count > 0)
                            {
                            btnChallan.Visible = true;
                            }
                        else
                            {
                            btnChallan.Visible = false;
                            }


                        string chk = objCommon.LookUp("ACD_DCR", "RECON", "IDNO=" + idno);
                        if (chk == "True")
                            {
                            trReconcile.Visible = false;
                            trPaytype.Visible = false;
                            trmsg.Visible = false;
                            }
                        else
                            {
                            trReconcile.Visible = true;
                            trPaytype.Visible = true;
                            }
                        }
                        //else
                            {
                            //objCommon.DisplayMessage(this.updpnlUser, "Please Enter Only Phd Registration No.!", this.Page);
                            }
                    }
                else
                    {
                    // objCommon.DisplayMessage(this.updpnlUser, "Please Enter Valid Registration No.!", this.Page);
                    }
                }
            else
                {
                // objCommon.DisplayMessage(this.updpnlUser, "Please Enter Valid Registration No.!", this.Page);
                }
            }
        catch (Exception ex)
            {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRegistrationPhd.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            }

        }


    public void clear()
        {

        ddlAdmCategory.SelectedIndex = 0;
        ddlAdmRound.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlBloodGrp.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlCoSupervisorType1.SelectedIndex = 0;
        ddlCoSupervisorType2.SelectedIndex = 0;
        ddlExamNo.SelectedIndex = 0;
        ddlLocalCity.SelectedIndex = 0;
        ddlLocalState.SelectedIndex = 0;
        ddlNationality.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        ddlPhyHandicap.SelectedIndex = 0;
        ddlQuota.SelectedIndex = 0;
        ddlReligion.SelectedIndex = 0;
        ddlsession.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlStatusCat.SelectedIndex = 0;
        txtContactNumber.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtEMployeeID.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtMotherName.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtPersonalemail.Text = string.Empty;
        txtStudEmail.Text = string.Empty;
        txtPermanentAddress.Text = string.Empty;
        txtinstitute.Text = string.Empty;
        txtYearOfExam.Text = string.Empty;
        txtPer.Text = string.Empty;
        txtPercentile.Text = string.Empty;
        rdoFemale.Checked = false;
        rdoMarriedYes.Checked = false;
        ddlBranch.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlLocalCity.SelectedIndex = 0;
        ddlLocalState.SelectedIndex = 0;
        txtFatherName.Text = string.Empty;
        txtGuardianMobile.Text = string.Empty;
        txtMotherName.Text = string.Empty;
        txtPayType.Text = string.Empty;
        txtPIN.Text = string.Empty;
        txtPostalAddress.Text = string.Empty;
        txtGuardianPhone.Text = string.Empty;
        ddlcity.SelectedIndex = 0;
        txtGuardianEmail.Text = string.Empty;
        txtDateOfAdmission.Text = string.Empty;
        ddlSupervisor.SelectedIndex = 0;
        ddlSearch.SelectedIndex = 0;

        }

    }