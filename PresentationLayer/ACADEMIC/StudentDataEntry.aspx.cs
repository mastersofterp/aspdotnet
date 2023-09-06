using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class StudentDataEntry : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    Student objStudent = new Student();
    StudentController objStudCont = new StudentController();
    StudentAddress objStudAddress = new StudentAddress();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentRegistration objRegistration = new StudentRegistration();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!Page.IsPostBack)
            {
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
                ViewState["action"] = "add";
                ViewState["brachno"] = null;
                divMsg.InnerHtml = string.Empty;
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StudentDataEntry.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
        ViewState["action"] = "add";
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        int count = 0;
        int branchno = 0;
        DataTableReader dtr = null;
        //Gate i.e MTECH Data
        //if (ddlExamName.SelectedValue == "1")
        //{
        //    count = Convert.ToInt16(objCommon.LookUp("TEMP_STUDENT_DATA", "Count(*)", " [GATE Reg] =" + txtStudentRoll.Text + " AND DOB = CONVERT(DATETIME ,'" + txtDateOfBirth.Text + "',103) AND ADMBATCH = " + lblAdmBatch.ToolTip));
        //    if (count == 0)
        //        dtr = objCommon.FillDropDown("TEMP_STUDENT", "[NAME], [AIR OVERALL],[GATE YEAR] AS EXAMYEAR, [GATE REG] AS ROLL, [GATE SCORE] AS SCORE, Programme,[GATE PAPER] AS PAPER, [DOB], [GENDER] , [MOBILE], [APPLICANT CATEGORY]", "[ALLOTTED CATEGORY], [Home State] , [QUOTA],[GROUP],[ROUND NO], [DEGREENO], [BRANCHNO]", " [GATE Reg] =" + txtStudentRoll.Text + " AND DOB = CONVERT(datetime ,'" + txtDateOfBirth.Text + "',103)  AND ADMBATCH = " + lblAdmBatch.ToolTip.ToString(), string.Empty).CreateDataReader();
        //    //dtr = objCommon.FillDropDown("TEMP_STUDENT S LEFT OUTER JOIN TEMP_STUDENT_DATA D ON (S.[GATE Reg] = D.[GATE Reg]] AND S.DOB = D.DOB )", "S.[NO#], S.[NAME], S.[PRN], S.[GATE YEAR], S.[GATE REG], S.[GATE SCORE], S.[GATE PAPER], S.[DOB], S.[GENDER], S.[MOBILE], S.[APPLICANT CATEGORY], S.[PROGRAMME], S.[INSTITUTE], S.[ALLOTTED CATEGORY], S.[GROUP], S.[DEGREENO], S.[BRANCHNO],S.[ROLL NO], S.[AIR OVERALL], D.[YEAR OF EXAM], S.[BRANCH NAME], S.[PH]", " S.[HOME STATE], S.[REPORTING DATE], S.[QUOTA], S.[ROUND NO],[FATHER'S NAME],[MOTHER'S NAME],[RELIGION],[MARITAL STATUS],[NATIONALITY],[EMAIL],[BLOOD GROUP],[PERMANANT ADDRESS],[CITY],[STATE],[PIN CODE],[CONTACT NUMBER],[POSTAL ADDRESS],[Local City],[Local STATE],[GUARDIAN PHONE],[Parent Phone],[GUARDIAN EMAIL]", "S.[NAME] = '" + txtStudentName.Text.Trim() + "' AND S.DOB = CONVERT(datetime , '" + txtDateOfBirth.Text.Trim() + "',103)", string.Empty).CreateDataReader();
        //    else
        //    {
        //        objCommon.DisplayMessage("Student Information Already Entered!!", this.Page);
        //        pnlStudent.Visible = false;
        //        //  ViewState["action"] = null;
        //        ViewState["brachno"] = null;
        //        lblRound.Text = "";

        //        return;
        //    }
        //}
            //AIEEE i.e BTECH Data
        //else
        if (ddlExamName.SelectedValue == "1")
        {
            count = Convert.ToInt16(objCommon.LookUp("TEMP_STUDENT_DATA", "Count(*)", "[ROLL NO] =" + txtStudentRoll.Text + "  AND ADMBATCH = "+ lblAdmBatch.ToolTip));
            if (count == 0)
                dtr = objCommon.FillDropDown("TEMP_STUDENT", "[NAME],0 AS EXAMYEAR,MOBILE, [ROLL NO] AS ROLL,[APPLICANT CATEGORY],  [ALLOTTED CATEGORY]", " [DEGREENO], [BRANCHNO], Gender ,DOB, [AIR OVERALL],  [BRANCH NAME], [HOME STATE], [QUOTA],PROGRAMME, [ROUND NO],[FATHERNAME],[MOTHERNAME]", "[ROLL NO] =" + txtStudentRoll.Text + " AND [GATE Reg] IS  NULL AND ADMBATCH = "+ lblAdmBatch.ToolTip.ToString(), string.Empty).CreateDataReader();
               // dtr = objCommon.FillDropDown("TEMP_STUDENT S LEFT OUTER JOIN TEMP_STUDENT_DATA D ON (S.[Name] = D.[Name] AND S.DOB = D.DOB )", "S.[NO#], S.[NAME], S.[PRN], S.[GATE YEAR], S.[GATE REG], S.[GATE SCORE], S.[GATE PAPER], S.[DOB], S.[GENDER], S.[MOBILE], S.[APPLICANT CATEGORY], S.[PROGRAMME], S.[INSTITUTE], S.[ALLOTTED CATEGORY], S.[GROUP], S.[DEGREENO], S.[BRANCHNO],S.[ROLL NO], S.[AIR OVERALL], D.[YEAR OF EXAM], S.[BRANCH NAME], S.[PH]", " S.[HOME STATE], S.[REPORTING DATE], S.[QUOTA], S.[ROUND NO],[FATHER'S NAME],[MOTHER'S NAME],[RELIGION],[MARITAL STATUS],[NATIONALITY],[EMAIL],[BLOOD GROUP],[PERMANANT ADDRESS],[CITY],[STATE],[PIN CODE],[CONTACT NUMBER],[POSTAL ADDRESS],[Local City],[Local STATE],[GUARDIAN PHONE],[Parent Phone],[GUARDIAN EMAIL]", "S.[NAME] = '" + txtStudentName.Text.Trim() + "' AND S.DOB = CONVERT(datetime , '" + txtDateOfBirth.Text.Trim() + "',103)", string.Empty).CreateDataReader();
            else
            {
                objCommon.DisplayMessage("Student Information Already Entered!!", this.Page);
                pnlStudent.Visible = false;
                ViewState["brachno"] = null;
                ViewState["action"] = null;
                lblRound.Text = "";
               
                return;
            }
        }  
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //Student Details
                    txtStudentName.Text = dtr["Name"] == DBNull.Value ? string.Empty : dtr["Name"].ToString();
                    txtFatherName.Text = dtr["FatherName"] == DBNull.Value ? string.Empty : dtr["FatherName"].ToString();
                    txtMotherName.Text = dtr["MotherName"] == DBNull.Value ? string.Empty : dtr["MotherName"].ToString();
                    txtMobileNo.Text = dtr["MOBILE"] == DBNull.Value ? string.Empty : dtr["MOBILE"].ToString();
                    //ddlState.Text = dtr["Home State"] == DBNull.Value ? "0" : dtr["Home State"].ToString();

                    if (dtr["Gender"] != DBNull.Value)
                    {
                        if (dtr["Gender"].ToString().ToUpper().Contains("F"))
                        {
                            rdoGender.SelectedValue = "1";
                        }
                        else
                        {
                            rdoGender.SelectedValue = "0";
                        }
                    }
                    
                    txtExamRollNo.Text = dtr["ROLL"] == DBNull.Value ? string.Empty : dtr["ROLL"].ToString();
                    txtExamYear.Text = dtr["EXAMYEAR"] == DBNull.Value ? string.Empty : dtr["EXAMYEAR"].ToString();
                    txtAllIndiaRank.Text = dtr["AIR OVERALL"] == DBNull.Value ? string.Empty : dtr["AIR OVERALL"].ToString();
                    if (dtr["Quota"] == DBNull.Value)
                        ddlQuota.SelectedIndex = 0;
                    else
                        ddlQuota.SelectedValue = ddlQuota.Items.FindByText(dtr["Quota"] == DBNull.Value ? "0" : dtr["Quota"].ToString()).Value;

                    //if (dtr["DEGREENO"] == DBNull.Value)
                    //    ddlDegree.SelectedIndex = 0;
                    //else

                    //    ddlDegree.SelectedValue = dtr["DEGREENO"].ToString();
                    
                    if (ddlExamName.SelectedValue == "1")
                    {
                        //txtDateOfBirth.Enabled = false;
                        //txtExamPaper.Text = dtr["PAPER"] == DBNull.Value ? string.Empty : dtr["PAPER"].ToString();
                        //txtExamScore.Text = dtr["score"] == DBNull.Value ? string.Empty : dtr["score"].ToString();
                        if (dtr["Applicant Category"] != DBNull.Value)
                            GetBTECHCategory(dtr["Applicant Category"].ToString(), 1);
                        if (dtr["ALLOTTED CATEGORY"] != DBNull.Value)
                            GetBTECHCategory(dtr["ALLOTTED CATEGORY"].ToString(), 2);
                        txtDateOfBirth.Enabled = true;
                        txtExamPaper.Visible = false;
                        tdpaper.Visible = false;
                        //txtDateOfBirth.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                        //brachno = Convert.ToInt16(objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "LONGNAME LIKE '%" + dtr["Branch Name"].ToString(). + "%' AND "));

                        //Extracting Branch No form the  available branchname 
                        //if (dtr["branch name"].ToString().ToUpper().Contains("BIO") && dtr["branch name"].ToString().ToUpper().Contains("MEDICAL"))
                        //    branchno = 11;
                        //else if (dtr["branch name"].ToString().ToUpper().Contains("BIO") && dtr["branch name"].ToString().ToUpper().Contains("TECHNOLOGY"))
                        //        branchno = 12;
                        //    else  if (dtr["branch name"].ToString().ToUpper().Contains("CHEMICAL"))
                        //            branchno = 13;
                        //        else if (dtr["branch name"].ToString().ToUpper().Contains("CIVIL"))
                        //                branchno = 14;
                        //            else if (dtr["branch name"].ToString().ToUpper().Contains("COMPUTER"))
                        //                    branchno = 15;
                        //                else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRONICS"))
                        //                        branchno = 16;
                        //                    else  if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRICAL"))
                        //                            branchno = 17;
                        //                        else if (dtr["branch name"].ToString().ToUpper().Contains("INFORMATION"))
                        //                                branchno = 18;
                        //                            else if (dtr["branch name"].ToString().ToUpper().Contains("MECHANICAL"))
                        //                                    branchno = 19;
                        //                                else if (dtr["branch name"].ToString().ToUpper().Contains("METALLURGICAL"))
                        //                                        branchno = 20;
                        //                                    else if (dtr["branch name"].ToString().ToUpper().Contains("MINING"))
                        //                                            branchno = 21;
                        if (dtr["branch name"].ToString().ToUpper().Contains("COMPUTER"))
                                branchno = 1;
                           
                            else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRICAL"))
                                branchno = 2;
                             else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRONICS"))
                                 branchno = 3;
                        //if (dtr["Programme"].ToString().ToUpper().Contains("CHEMICAL"))
                        //    branchno = 45;
                        //else if (dtr["Programme"].ToString().ToUpper().Contains("CIVIL"))
                        //    branchno = 44;
                        //else if (dtr["Programme"].ToString().ToUpper().Contains("ELECTRICAL"))
                        //    branchno = 24;
                        //else if (dtr["Programme"].ToString().ToUpper().Contains("MECHANICAL"))
                        //    branchno = 29;
                        ViewState["brachno"] = branchno.ToString();
                    }
                    else
                        if (ddlExamName.SelectedValue == "2")
                        {
                            if (dtr["Applicant Category"] != DBNull.Value)
                                GetMTECHCategory(dtr["Applicant Category"].ToString(), 1);
                            if (dtr["ALLOTTED CATEGORY"] != DBNull.Value)
                                GetMTECHCategory(dtr["ALLOTTED CATEGORY"].ToString(), 2);
                            txtDateOfBirth.Enabled = true;
                            txtExamPaper.Visible = false;
                            tdpaper.Visible = false;
                            //if (dtr["branch name"].ToString().ToUpper().Contains("BIO") && dtr["branch name"].ToString().ToUpper().Contains("MEDICAL"))
                            //    branchno = 11;
                            //else if (dtr["branch name"].ToString().ToUpper().Contains("BIO") && dtr["branch name"].ToString().ToUpper().Contains("TECHNOLOGY"))
                            //    branchno = 12;
                            //else if (dtr["branch name"].ToString().ToUpper().Contains("CHEMICAL"))
                            //    branchno = 13;
                            
                             if (dtr["branch name"].ToString().ToUpper().Contains("COMPUTER"))
                                branchno = 1;
                           
                            else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRICAL"))
                                branchno = 2;
                             else if (dtr["branch name"].ToString().ToUpper().Contains("ELECTRONICS"))
                                 branchno = 3;
                            //else if (dtr["branch name"].ToString().ToUpper().Contains("MECHANICAL"))
                            //    branchno = 4;
                            // else if (dtr["branch name"].ToString().ToUpper().Contains("CIVIL"))
                            //     branchno = 5;
                            
                            //else if (dtr["branch name"].ToString().ToUpper().Contains("METALLURGICAL"))
                            //    branchno = 20;
                            //else if (dtr["branch name"].ToString().ToUpper().Contains("MINING"))
                            //    branchno = 21;
                            //else if (dtr["branch name"].ToString().ToUpper().Contains("ARCHITECTURE"))
                            //    branchno = 22;
                            //if (dtr["Programme"].ToString().ToUpper().Contains("CHEMICAL"))
                            //    branchno = 45;
                            //else if (dtr["Programme"].ToString().ToUpper().Contains("CIVIL"))
                            //    branchno = 44;
                            //else if (dtr["Programme"].ToString().ToUpper().Contains("ELECTRICAL"))
                            //    branchno = 24;
                            //else if (dtr["Programme"].ToString().ToUpper().Contains("MECHANICAL"))
                            //    branchno = 29;
                            ViewState["brachno"] = branchno.ToString();

                        }
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegreesel.SelectedValue), "BRANCHNO");
                    ddlBranch.SelectedValue = ViewState["brachno"].ToString();
                    lblRound.Text = dtr["ROUND NO"] == DBNull.Value ? string.Empty : "Round No. = " + dtr["ROUND NO"].ToString();
                    //pnlStudent.Visible = true;
                    //check the degree code
                    if (ddlDegreesel.SelectedIndex == 2 && branchno == 22)
                    {
                        pnlStudent.Visible = true;

                    }
                    else if (ddlDegreesel.SelectedIndex == 1 )
                    {
                        pnlStudent.Visible = true;
                    }
                    else
                    {
                        pnlStudent.Visible = false;
                        objCommon.DisplayMessage("Please Select Correct Degree!!", this.Page);

                    }
                    
                    txtExamRollNo.Enabled = false;
                    //ddlBranch.SelectedItem.Text=dtr[""]
                    ddlExamName.Enabled = false;
                    
                }
                else
                    Response.Redirect("~/default.aspx");
            dtr.Close();
        }
        else
        {
            ViewState["action"] = null;
            objCommon.DisplayMessage("Your Information is already Saved!", this);
            txtExamRollNo.Enabled = false;
            txtDateOfBirth.Enabled = false;
            btnShow.Visible= false;
            btnSubmit.Visible= false;
            btnClear.Visible = false;
            pnlStudent.Visible = false;
            
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/default.aspx");
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int hostel = 0;
        if (ViewState["action"].ToString() == "add")
        {
            objStudent.StudName = txtStudentName.Text;
            if (ddlExamName.SelectedValue == "2")
            {
                objStudent.GATE_YEAR = txtExamYear.Text;// == "" ? "0" : txtExamYear.Text;
                objStudent.GATE_REG = txtExamRollNo.Text;// == "" ? "0" : txtExamRollNo.Text;
                objStudent.GATE_SCORE = txtExamScore.Text;// == "" ? "0" : txtExamScore.Text;
                objStudent.GATE_PAPER = txtExamPaper.Text;
                //objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToDouble(txtAllIndiaRank.Text);
                objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToInt32(txtAllIndiaRank.Text);
                objStudent.DegreeNo = 5;
            }

            else
                if (ddlDegreesel.SelectedValue == "1")
                {
                    if (ddlExamName.SelectedValue == "1")
                    {
                        objStudent.YearOfExam = txtExamYear.Text;// == "" ? "0" : txtExamYear.Text;
                        objStudent.RollNo = txtExamRollNo.Text;//== "" ? "0" : txtExamRollNo.Text;
                        //objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToDouble(txtAllIndiaRank.Text);
                        objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToInt32(txtAllIndiaRank.Text);
                        objStudent.DegreeNo = 1;
                        if (!txtExamScore.Text.Trim().Equals(string.Empty)) objStudent.Score = Convert.ToDecimal(txtExamScore.Text.Trim());
                    }
                }
                else
                {
                    if (ddlExamName.SelectedValue == "2")
                    {
                        objStudent.YearOfExam = txtExamYear.Text;// == "" ? "0" : txtExamYear.Text;
                        objStudent.RollNo = txtExamRollNo.Text;//== "" ? "0" : txtExamRollNo.Text;
                        //objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToDouble(txtAllIndiaRank.Text);
                        objStudent.ALLINDIARANK = txtAllIndiaRank.Text == "" ? 0 : Convert.ToInt32(txtAllIndiaRank.Text);
                        objStudent.DegreeNo = 1;
                    }
                
                }
            objStudent.Dob = Convert.ToDateTime(txtDateOfBirth.Text);
            objStudent.Sex = rdoGender.SelectedValue == "0" ? 'M' : 'F';
            objStudent.PMobile = txtMobileNo.Text;//== "" ? "0" : txtMobileNo.Text ;
            objStudent.CategoryNo = Convert.ToInt16(ddlApplicantCategory.SelectedValue);
            objStudent.AdmCategoryNo = Convert.ToInt16(ddlAdmissionCategory.SelectedValue);
            objStudent.BranchNo = Convert.ToInt16(ViewState["brachno"].ToString());
            objStudent.ADMQUOTANO = Convert.ToInt16(ddlQuota.SelectedValue);
            objStudent.PState = Convert.ToInt16(ddlState.SelectedValue);
            objStudent.FatherName = txtFatherName.Text;
            objStudent.MotherName = txtMotherName.Text;
            objStudent.ReligionNo = Convert.ToInt16(ddlReligion.Text);
            objStudent.Married = rdbMaritalStatus.SelectedValue == "0" ? 'N' : 'Y';
            objStudent.PH = rdbPH.SelectedValue == "2" ? "YES" : "NO";
            objStudent.NationalityNo = Convert.ToInt16(ddlNationality.SelectedValue);
            objStudAddress.PEMAIL = txtStudEmail.Text;
            objStudent.BloodGroupNo = Convert.ToInt16(ddlBloodGroupNo.SelectedValue);
            objStudent.PAddress = txtPermanentAddress.Text.Trim();
            objStudent.PCity = ddlCity.SelectedValue;
            objStudent.StateNo = Convert.ToInt16(ddlState.SelectedValue);
            objStudent.PPinCode = txtPIN.Text == "" ? "0" : txtPIN.Text;
            objStudent.StudentMobile = txtMobileNo.Text;// == "" ? "0" : txtMobileNo.Text;
            objStudent.AdmBatch = Convert.ToInt16(lblAdmBatch.ToolTip.ToString());
            objStudAddress.LADDRESS = txtPostalAddress.Text;
            //objStudAddress.LCITY = txtLocalCity.Text;
            objStudAddress.LSTATE = Convert.ToInt16(ddlLocalState.SelectedValue);
            objStudAddress.LMOBILE = txtGuardianMobile.Text == "" ? "0" : txtGuardianMobile.Text;
            objStudent.PMobile = txtConatctNo.Text == "" ? "0" : txtConatctNo.Text;
            objStudAddress.GEMAIL = txtGuardianEmail.Text;
            objStudent.PayTypeNO = Convert.ToInt32(ddlPaymentType.SelectedValue);
            objStudent.Remark = txtRemark.Text.Trim();
            objStudent.CsabAmount = Convert.ToDouble(txtPaidCCB.Text.Trim());

            if (chkHostel.Checked == true)
                hostel = 0;
            else
                hostel = 1;
            // add the document list
            //if (lvDocuments.Items.Count >= 1)
            //{
            //    foreach (ListViewDataItem dataitem in lvDocuments.Items)
            //    {
            //        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //            objStudent.DOCUMENTS = objStudent.DOCUMENTS + (dataitem.FindControl("lbldocuments") as Label).ToolTip + ",";
            //    }
            //}
            objStudent.DOCUMENTS = string.Empty;
            int ret = Convert.ToInt16(objStudCont.AddStudentTempData(objStudent, objStudAddress,hostel));
            if (ret > 0)
            {
                string studentIDs = ret.ToString();
                objCommon.DisplayMessage("Registration Completed and your IDNO:" + ret, this.Page);
                pnlStudent.Visible = false;
                Clear();
               // Response.Redirect(Request.Url.ToString());

                //Create DCR and print Challan


                //bool overwriteDemand = true;
                //string receiptno = this.GetNewReceiptNo();
                //FeeDemand dcr = this.GetDcrCriteria();
                //int currency = 0;
                //string paymodecode = "B";
              
                //currency = Convert.ToInt32(ddlCurrency.SelectedValue);
                //string dcritem = dmController.CreateDcrForStudents(studentIDs, dcr, 1, overwriteDemand, receiptno, currency, paymodecode);

                //if (chkHostel.Checked == true)
                //{
                //    //Create DCR for  hostller and print Challan
                //    overwriteDemand = true;
                //    string receiptnoHostel = this.GetNewReceiptNoForHostel();
                //    FeeDemand dcrForhostel = this.GetDcrCriteriaFoHostel();
                //    string dcrHostel = dmController.CreateDcrForStudents(studentIDs, dcrForhostel, 1, overwriteDemand, receiptnoHostel, currency, paymodecode);

                //}
                //ShowReport("Admission_Slip_Report", "AdmissionSlip.rpt", studentIDs);



                //Print Challan..

                //this.ShowReport("FeeCollectionReceiptForCourseRegister.rpt", Convert.ToInt32(studentIDs), "1");

                //string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=1");

                //if (txtPayType.Text == "D")
                //{
                //    DailyCollectionRegister dcrn = new DailyCollectionRegister();
                //    DemandDrafts[] dds = null;
                //    dcrn.DemandDraftAmount = this.GetTotalDDAmountAndSetCompleteDetails(ref dds);
                //    dcrn.PaidDemandDrafts = dds;

                //    //dmController.DDTransactionStudent(ref dcrn);
                    
                //}

                //if (dcrNo != string.Empty && studentIDs != string.Empty)
                //{
                //   // objRegistration.GenereateSingleEnrollmentNo(Convert.ToInt32(lblAdmBatch.ToolTip), 1, Convert.ToInt32(studentIDs));
                //    this.ShowReport("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
                   
                   
                //}
            }
            else
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
        }
        else
            Response.Redirect("~/default.aspx");
    }                
    protected void PopulateDropDownList()
    {
        this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
        objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0", "NATIONALITYNO");
        objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION","RELIGIONNO>0", "RELIGIONNO");
        objCommon.FillDropDownList(ddlApplicantCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
        objCommon.FillDropDownList(ddlAdmissionCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY","CATEGORYNO>0", "CATEGORY");
        objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0", "RELIGION");
        objCommon.FillDropDownList(ddlBloodGroupNo, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME","BLOODGRPNO>0", "BLOODGRPNAME");
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
        objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
        objCommon.FillDropDownList(ddlLocalCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
        objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
        objCommon.FillDropDownList(ddlDegreesel, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO IN (1,3)", "DEGREENO");
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO IN (1,3)", "DEGREENO");
       
        //objCommon.FillDropDownList(ddlCurrency, "ACD_CURRENCY C INNER JOIN ACD_CURRENCY_TITLE T ON C.CUR_NO= T.CUR_NO", " DISTINCT T.CUR_NO", "C.CUR_NAME", "", "T.CUR_NO");
        //fill dropdown adm quota
        objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");
        lblAdmBatch.Text = objCommon.LookUp("ACD_ADMBATCH", "TOP 1 BATCHNAME", "BATCHNO <>0  order by BATCHNO DESC");
        lblAdmBatch.ToolTip = objCommon.LookUp("ACD_ADMBATCH", "TOP 1 BATCHNO", "BATCHNO <>0  order by BATCHNO DESC");
        //FILL DROPDOWM PATMENT TYPE
        //objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>O", "PAYTYPENO");
        // document list 
        //DataSet ds = objCommon.FillDropDown("ACD_DOCUMENT_LIST", "DOCUMENTNO", "DOCUMENTNAME", "DOCUMENTNO > 0", "DOCUMENTNO");
        //if (ds != null && ds.Tables.Count > 0)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //chkDoc.DataTextField = "DOCUMENTNAME";
        //        //chkDoc.DataValueField = "DOCUMENTNO";
        //        //chkDoc.DataSource = ds.Tables[0];
        //        //chkDoc.DataBind();

        //        lvDocuments.DataSource = ds.Tables[0];
        //        lvDocuments.DataBind();
        //    }
        //}
       // this.CheckAllDocs();

    }
    protected void rdbExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAllIndiaRank.Text = "";
        txtExamYear.Text = "";
        txtExamRollNo.Text = "";
        txtExamScore.Text = "";
        ddlQuota.SelectedIndex = 0;
    }
    private void GetBTECHCategory(string category, int type)
    {
        if (category.Contains("G") && category.Contains("E"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "4";
            else
                ddlAdmissionCategory.SelectedValue = "4";
        else if (category.Contains("O") && category.Contains("B"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "3";
            else
                ddlAdmissionCategory.SelectedValue = "3";
        else if (category.Contains("S") && category.Contains("C"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "2";
            else ddlAdmissionCategory.SelectedValue = "2";
        else if (category.ToString().Contains("S") && category.Contains("T"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "1";
            else
                ddlAdmissionCategory.SelectedValue = "1";
        else if (category.ToString().Contains("C") && category.Contains("H"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "5";
            else
                ddlAdmissionCategory.SelectedValue = "5";

        //PHYSICALLY HANDICAPPED
        if (category.Contains("-N"))
        {
            rdbPH.SelectedValue = "0";
        }
        else
            if (category.Contains("-Y"))
            {
                rdbPH.SelectedValue = "1";
            }
    }

    private void GetMTECHCategory(string category, int type)
    {
        if (category.Contains("BC"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "3";
            else
                ddlAdmissionCategory.SelectedValue = "3";
        else if (category.Contains("EN"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "4";
            else
                ddlAdmissionCategory.SelectedValue = "4";
        else if (category.Contains("SC"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "2";
            else
                ddlAdmissionCategory.SelectedValue = "2";
        else if (category.ToString().Contains("ST"))
            if (type == 1)
                ddlApplicantCategory.SelectedValue = "1";
            else
                ddlAdmissionCategory.SelectedValue = "1";

        //PHYSICALLY HANDICAPPED
        if (category.Contains("NO"))
        {
            rdbPH.SelectedValue = "0";
        }
        else
            if (category.Contains("PH"))
            {
                rdbPH.SelectedValue = "1";
            }
    }
    private void Clear()
    {
        ddlQuota.SelectedIndex = 0;
        ddlExamName.SelectedIndex = 0;
        ddlAdmissionCategory.SelectedIndex = 0;
        ddlApplicantCategory.SelectedIndex = 0;
        ddlBloodGroupNo.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlExamName.SelectedIndex = 0;
        ddlLocalCity.SelectedIndex = 0;
        ddlLocalState.SelectedIndex = 0;
        ddlNationality.SelectedIndex = 0;
        ddlQuota.SelectedIndex = 0;
        ddlReligion.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        

        rdoGender.ClearSelection();
        rdbPH.ClearSelection();

        txtStudentName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtExamRollNo.Text = string.Empty;
        txtExamYear.Text = string.Empty;
        txtAllIndiaRank.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtExamPaper.Text = string.Empty;
        txtExamScore.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtConatctNo.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtGuardianEmail.Text = string.Empty;
        txtGuardianMobile.Text = string.Empty;
        txtGuardianPhone.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtMotherName.Text = string.Empty;
        txtPermanentAddress.Text = string.Empty;
        txtPIN.Text = string.Empty;
        txtPostalAddress.Text = string.Empty;
        txtStudEmail.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtStudentRoll.Text = string.Empty;
        lblRound.Text = string.Empty;

        txtExamPaper.Visible = true;
        tdpaper.Visible = true;

        lblRound.Text = string.Empty;
        txtExamRollNo.Enabled = true;
        ddlExamName.Enabled = true;
        txtRemark.Text = string.Empty;
        ViewState["brachno"] = null;
        ViewState["action"] = "edit";
        //  pnlStudent.Visible = false;


    }
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;
        try
        {
            string paymodecode = "B";
            string receiptType = "TF";
            DataSet ds = feeController.GetNewReceiptData(paymodecode, Int32.Parse(Session["userno"].ToString()), receiptType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //String reciptCode;
                //reciptCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECEIPT_CODE","");
                String FeesSessionStartDate;
                FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");

                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                //receiptNo = dr["PRINTNAME"].ToString() + "/" + GetViewStateItem("PaymentMode") + "/" + Session["FeesSessionStartDate"].ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString();
                receiptNo = dr["PRINTNAME"].ToString() + "/" + paymodecode + "/" + receiptType + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }
    //get the new receipt No.
    //private string GetNewReceiptNo()
    //{
    //    string receiptNo = string.Empty;

    //    try
    //    {
    //        string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
    //        DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataRow dr = ds.Tables[0].Rows[0];
    //            //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
    //            dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
    //            receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

    //            // save counter no in hidden field to be used while saving the record
    //            ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return receiptNo;
    //}

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = objStudent.BranchNo;
            dcrCriteria.SemesterNo = 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            //dcrCriteria.ExcessAmount = Convert.ToDouble(txtPaidCCB.Text.Trim());
            dcrCriteria.ExcessAmount = Convert.ToDouble(txtPaidCCB.Text.Trim());
            dcrCriteria.CollegeCode = Session["colcode"].ToString(); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    private void ShowReport(string reportTitle, string rptFileName, string idno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private string GetNewReceiptNoForHostel()
    //{
    //    string receiptNo = string.Empty;

    //    try
    //    {
    //        string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
    //        DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "HF");
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataRow dr = ds.Tables[0].Rows[0];
    //            //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
    //            dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
    //            receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

    //            // save counter no in hidden field to be used while saving the record
    //            ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return receiptNo;
    //}
    private string GetNewReceiptNoForHostel()
    {
        string receiptNo = string.Empty;
        try
        {
            string paymodecode = "B";
            string receiptType = "HF";
            DataSet ds = feeController.GetNewReceiptData(paymodecode, Int32.Parse(Session["userno"].ToString()), receiptType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //String reciptCode;
                //reciptCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECEIPT_CODE","");
                String FeesSessionStartDate;
                FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");

                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                //receiptNo = dr["PRINTNAME"].ToString() + "/" + GetViewStateItem("PaymentMode") + "/" + Session["FeesSessionStartDate"].ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString();
                receiptNo = dr["PRINTNAME"].ToString() + "/" + paymodecode + "/" + receiptType + "/" + FeesSessionStartDate + "/" + dr["FIELD"].ToString();
                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteriaFoHostel()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "HF";
            dcrCriteria.BranchNo = objStudent.BranchNo;
            dcrCriteria.SemesterNo = 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            //dcrCriteria.UserNo = 7;
            dcrCriteria.ExcessAmount = 0;
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
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
        string collegeCode = "15";

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + collegeCode + "";
        return param;
    }


    //#region Displaying Demand Draft Details

    //protected void btnSaveDD_Info_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt;
    //        if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //        {
    //            dt = ((DataTable)Session["DD_Info"]);
    //            DataRow dr = dt.NewRow();
    //            dr["DD_NO"] = txtDDNo.Text.Trim();
    //            dr["DD_DT"] = txtDDDate.Text.Trim();
    //            dr["DD_CITY"] = txtDDCity.Text.Trim();
    //            dr["DD_BANK_NO"] = ddlBank.SelectedValue;
    //            dr["DD_BANK"] = ddlBank.SelectedItem.Text;
    //            dr["DD_AMOUNT"] = txtDDAmount.Text.Trim();
    //            dt.Rows.Add(dr);
    //            Session["DD_Info"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);

    //            // add the two DD for the same semester add the previous DD amount and current DD amount// 25/05/2012
    //           // txtTotalAmount.Text = (Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtDDAmount.Text.Trim())).ToString();

    //        }
    //        else
    //        {
    //            dt = this.GetDemandDraftDataTable();
    //            DataRow dr = dt.NewRow();
    //            dr["DD_NO"] = txtDDNo.Text.Trim();
    //            dr["DD_DT"] = txtDDDate.Text.Trim();
    //            dr["DD_CITY"] = txtDDCity.Text.Trim();
    //            dr["DD_BANK_NO"] = ddlBank.SelectedValue;
    //            dr["DD_BANK"] = ddlBank.SelectedItem.Text;
    //            dr["DD_AMOUNT"] = txtDDAmount.Text.Trim();
    //            dt.Rows.Add(dr);
    //            Session.Add("DD_Info", dt);
    //            this.BindListView_DemandDraftDetails(dt);

    //            //Enter the DD amount then add the DD amount to total amount textbox //add code 25/05/2012

    //            //txtTotalAmount.Text = txtDDAmount.Text.Trim();

    //        }
    //        this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> UpdateCash_DD_Amount();  </script> ";
    //        this.ClearControls_DemandDraftDetails();
    //       // txtTotalAmount.Focus();
    //        //btnSaveDD_Info.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnSaveDD_Info_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }

    //}

    //protected void btnEditDDInfo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEdit = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //        {
    //            dt = ((DataTable)Session["DD_Info"]);
    //            DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
    //            txtDDNo.Text = dr["DD_NO"].ToString();
    //            txtDDDate.Text = dr["DD_DT"].ToString();
    //            txtDDCity.Text = dr["DD_CITY"].ToString();
    //            ddlBank.SelectedValue = dr["DD_BANK_NO"].ToString();
    //            txtDDAmount.Text = dr["DD_AMOUNT"].ToString();
    //            dt.Rows.Remove(dr);
    //            Session["DD_Info"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);

    //            // for Edit the data to maintain the Total amount // 25/04/2012
    //            //txtTotalAmount.Text = (Convert.ToDouble(txtTotalAmount.Text.Trim()) - Convert.ToDouble(txtDDAmount.Text.Trim())).ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnEditDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void btnDeleteDDInfo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelete = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //        {
    //            dt = ((DataTable)Session["DD_Info"]);
    //            dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
    //            Session["DD_Info"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);

    //            // This code add for delete the DD amount to duduct the amount for total amount.// 26/04/2012
    //            if (dt.Rows.Count != 0)
    //            {
    //                string ddAmt = dt.Rows[0]["DD_AMOUNT"].ToString();
    //                //txtTotalAmount.Text = "0";
    //                //txtTotalDDAmount.Text = "0";
    //            }
    //            //else
    //            //{
    //            //    string ddAmt = dt.Rows[0]["DD_AMOUNT"].ToString();
    //            //    txtTotalAmount.Text = ddAmt;

    //            //}
    //            //txtTotalAmount.Focus();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private void BindListView_DemandDraftDetails(DataTable dt)
    //{
    //    try
    //    {
    //        divDDDetails.Style["display"] = "block";
    //        //divFeeItems.Style["display"] = "block";
    //        lvDemandDraftDetails.DataSource = dt;
    //        lvDemandDraftDetails.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private DataTable GetDemandDraftDataTable()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add(new DataColumn("DD_NO", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DD_DT", typeof(DateTime)));
    //    dt.Columns.Add(new DataColumn("DD_CITY", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DD_BANK_NO", typeof(int)));
    //    dt.Columns.Add(new DataColumn("DD_BANK", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DD_AMOUNT", typeof(double)));

    //    return dt;
    //}

    //private DataRow GetEditableDataRow(DataTable dt, string value)
    //{
    //    DataRow dataRow = null;
    //    try
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            if (dr["DD_NO"].ToString() == value)
    //            {
    //                dataRow = dr;
    //                break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return dataRow;
    //}

    //private void ClearControls_DemandDraftDetails()
    //{
    //    txtDDNo.Text = string.Empty;
    //    txtDDAmount.Text = string.Empty;
    //    txtDDCity.Text = string.Empty;
    //    txtDDDate.Text = DateTime.Today.ToShortDateString();
    //    ddlBank.SelectedIndex = 0;
    //}
    //#endregion

    //private double GetTotalDDAmountAndSetCompleteDetails(ref DemandDrafts[] paidDemandDrafts)
    //{
    //    /// This method not only return the total of dd amounts of all paid dds
    //    /// but also initializes the complete information
    //    /// of each demand draft into referenced DemandDrafts array.

    //    double totalDdAmt = 0.00;
    //    try
    //    {
    //        /// Collect demand draft details only if the pay type
    //        /// is D (i.e. Demand draft)
    //        if (txtPayType.Text.Trim() == "D")
    //        {
    //            if (Session["DD_Info"] != null && ((DataTable)Session["DD_Info"]) != null)
    //            {
    //                DataTable dt = ((DataTable)Session["DD_Info"]);

    //                paidDemandDrafts = new DemandDrafts[dt.Rows.Count];
    //                int index = 0;
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    DemandDrafts dd = new DemandDrafts();
    //                    dd.DemandDraftNo = dr["DD_NO"].ToString();
    //                    dd.DemandDraftCity = dr["DD_CITY"].ToString();
    //                    dd.DemandDraftBank = dr["DD_BANK"].ToString();
    //                    dd.BankNo = (dr["DD_BANK_NO"].ToString() != string.Empty ? int.Parse(dr["DD_BANK_NO"].ToString()) : 0);

    //                    string ddDate = dr["DD_DT"].ToString();
    //                    if (ddDate != null && ddDate != string.Empty)
    //                        dd.DemandDraftDate = Convert.ToDateTime(ddDate);

    //                    string amount = dr["DD_AMOUNT"].ToString();
    //                    if (amount != null && amount != string.Empty)
    //                    {
    //                        dd.DemandDraftAmount = Convert.ToDouble(amount);
    //                        totalDdAmt += dd.DemandDraftAmount;
    //                    }
    //                    /// Set cheque/dd details in paid cheque/dd collection
    //                    paidDemandDrafts[index] = dd;
    //                    index++;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_FeeCollection.GetTotalDDAmountAndSetCompleteDetails() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return totalDdAmt;
    //}

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPaymentType.SelectedIndex == 1)
        //{
        //    txtPaidCCB.Text = "35000";
        //}
        //else if (ddlPaymentType.SelectedIndex == 2)
        //{
        //    txtPaidCCB.Text = "25000";
        //}
        //if (ddlPaymentType.SelectedIndex == 0)
        //{
        //    txtPaidCCB.Text = string.Empty;
        //}
    }
}
