using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_DualDegreeRegistrationEntry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentRegistration objRegistration = new StudentRegistration();

    protected void Page_Load(object sender, EventArgs e)
    {
         //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }


            if (!Page.IsPostBack)
            {

                //Page Authorization
                //CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();              
                PopulateDropDownList();
                PopulateDropDown();                                
                //txtDateOfReporting.Text = DateTime.Today.ToString("dd/MM/yyyy");
                divSearchInfo.Visible = false;
                
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
             
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DualDegreeRegistrationEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DualDegreeRegistrationEntry.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
 
            objCommon.FillDropDownList(ddlCollegeNew, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID > 0 AND ActiveStatus=1", "COLLEGE_NAME");
            // objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID > 0 AND ActiveStatus=1", "COLLEGE_NAME");
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
                ddlSchool.SelectedValue = "1";
            }
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "TOP (1) BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            
            //ddlBatch.SelectedValue = "1";
            ddlBatch.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
            ddlYear.SelectedValue = "1";
            objCommon.FillDropDownList(ddlClaimedCat, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORY");

            // objCommon.FillDropDownList(ddlCategory, "ACD_SRCATEGORY", "srcategoryno", "srcategory", "srcategoryno > 0", "srcategory");

            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSemesterNew, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
            ddlSemester.SelectedValue = "1";
            // FILL DROPDOWN PAYMENT TYPE
            objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");

            objCommon.FillDropDownList(ddlPaymentTypeNew, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");
            // ddlPaymentType.SelectedValue = "1";
            //objCommon.FillDropDownList(ddlclaim, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
            //fill dropdown id type
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
            objCommon.FillDropDownList(ddlAdmtypeNew, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
            //ddlAdmType.SelectedValue = "1";
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "TOP (1) BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
            //fill dropdown adm round
            // objCommon.FillDropDownList(ddlAdmRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0", "ROUNDNAME");
            //fill dropdown adm quota
            //objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");

            objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO > 0 AND ACTIVESTATUS=1", "RELIGION");
            objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO > 0 AND ACTIVESTATUS=1", "NATIONALITY");
            objCommon.FillDropDownList(ddlAllotedCat, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORY");
            objCommon.FillDropDownList(ddlBloodGrp, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO > 0 AND ACTIVESTATUS=1", "BLOODGRPNAME");
            objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORYNO");
            //fill dropdown adm quota
            //objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO");
           // this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "ACTIVESTATUS=1", "BANKNAME");
            //objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "QUALIFYNO");
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIEXMNAME");

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0 AND ACTIVESTATUS=1", "SECTIONNO ASC");
            objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");
            //ddlstate.SelectedValue = "5";

            objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");

        }
        else
        {
            ddlCity.SelectedIndex = 0;
        }
    }
    
    
    
    protected void ddlAdmType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmType.SelectedValue.Equals("1"))
            {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                ddlYear.SelectedValue = "1";
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP (1) SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlAdmType.SelectedValue, "SEMESTERNO");
                ddlSemester.SelectedValue = "1";
            }
            else if (ddlAdmType.SelectedValue.Equals("2"))
            {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                ddlYear.SelectedValue = "2";
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP (1) SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlAdmType.SelectedValue, "SEMESTERNO");
                ddlSemester.SelectedIndex = 1;
            }
            else
            {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP (8) SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0", "SEMESTERNO");

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlExamNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtAadhaarNo_TextChanged(object sender, EventArgs e)
    {
        int l = (txtAadhaarNo.Text).Length;

        if (l < 12 && l != 0)
        {
            objCommon.DisplayUserMessage(updStudent, "Please enter 12 digit Aadhar No.", this.Page);
        }
    }

    private void ShowStudentDetails()
    {
        pnlLV.Visible = false;
        divsearchbar.Visible = false;
        string idno = string.Empty;
         
        PopulateDropDownList();
        DataSet dsStudent = objSC.GetStudentDataForDualDegree(Session["stuinfoidno"].ToString());
        //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A,ACD_STU_ADDRESS B", "A.IDNO", "A.STUDNAME,A.REGNO,A.IDNO,A.ROLLNO,A.STUDFIRSTNAME,A.STUDMIDDLENAME,A.STUDLASTNAME,A.FATHERFIRSTNAME,A.FATHERMIDDLENAME,A.FATHERLASTNAME,A.MOTHERNAME,A.DOB,A.SEX,ISNULL(A.RELIGIONNO,0)RELIGIONNO,A.MARRIED,A.NATIONALITYNO,ISNULL(A.CATEGORYNO,0)CATEGORYNO,A.CASTE,A.ADMDATE,A.DEGREENO,A.BRANCHNO,A.YEAR,A.STUDENTMOBILE,A.STUDENTMOBILE2,A.SEMESTERNO,A.PTYPE,A.STATENO,A.ADMBATCH,A.IDTYPE,A.YEAR_OF_EXAM,A.ALL_INDIA_RANK,A.STATE_RANK,A.PERCENTAGE,A.PERCENTILE,A.QEXMROLLNO,A.ADMCATEGORYNO,A.QUALIFYNO,A.SCHOLORSHIPTYPENO,A.PHYSICALLY_HANDICAPPED,A.ADMROUNDNO,A.COLLEGE_CODE,A.MERITNO,A.APPLICATIONID,A.SCORE,B.STADDNO, B.IDNO, B.PADDRESS, ISNULL(A.SCHOLORSHIP,0),A.COLLEGE_ID,A.FATHERMOBILE,SCHOLORSHIP,A.ADDHARCARDNO,ISNULL(A.TRANSPORT,0)TRANSPORT,A.HOSTELER,B.PCITY,ISNULL(A.INSTALLMENT,0)INSTALLMENT,B.PSTATE,B.PPINCODE,A.EMAILID,B.PTELEPHONE,B.LADDRESS,B.LTELEPHONE,B.LMOBILE,B.LEMAIL,A.ADMQUOTANO,A.BLOODGRPNO,B.LCITY,B.LSTATE", "ISNULL(A.ADMCAN,0)=0 AND A.IDNO=B.IDNO AND A.REGNO = '" + txtREGNo.Text.Trim() + "'","");
        if (dsStudent != null && dsStudent.Tables.Count > 0)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                //PopulateDropDownList();
                //txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                string srnno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()));
                Session["Enrollno"] = srnno;
                Session["output"] = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                txtStudentfullName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                txtStudentName.Text = dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                txtStudentMiddleName.Text = dsStudent.Tables[0].Rows[0]["STUDMIDDLENAME"].ToString();
                txtStudentLastName.Text = dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERFIRSTNAME"].ToString();
                txtFatherMiddleName.Text = dsStudent.Tables[0].Rows[0]["FATHERMIDDLENAME"].ToString();
                txtFatherLastName.Text = dsStudent.Tables[0].Rows[0]["FATHERLASTNAME"].ToString();
                txtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

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

                //txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));//Commented by Irfan Shaikh on 20190405
                txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                //ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString()), "ACD_RELIGION", "RELIGIONNO", "RELIGION"));
                ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString());
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
                //objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID > 0 AND ActiveStatus=1", "COLLEGE_NAME");
                ddlSchool.SelectedValue = (dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
                ddlNationality.SelectedValue = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString());
                ddlCategory.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                txtPermanentAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                txtCity.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                txtState.Text = (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                txtPIN.Text = dsStudent.Tables[0].Rows[0]["PPINCODE"].ToString();
                txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                // txtDateOfReporting.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));
                ddlDegree.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()), "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME"));
                ddlBranchShow.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), "ACD_BRANCH", "BRANCHNO", "LONGNAME"));
                ddlBatch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                ddlYear.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["YEAR"].ToString()), "ACD_YEAR", "YEAR", "YEARNAME"));
                ddlPaymentType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()), "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME"));

                 
                if (ddlPaymentType.SelectedItem.Text != string.Empty)
                {
                    string Paytype = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENO=" + dsStudent.Tables[0].Rows[0]["PTYPE"].ToString());
                     ViewState["Ptype"] = Paytype ;
                }

               
                // txtStateOfEligibility.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                ddlAdmType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()), "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION"));
                // txtAllIndiaRank.Text = dsStudent.Tables[0].Rows[0]["ALL_INDIA_RANK"].ToString();
                //  txtYearOfExam.Text = dsStudent.Tables[0].Rows[0]["YEAR_OF_EXAM"].ToString();
                //txtStateRank.Text = dsStudent.Tables[0].Rows[0]["STATE_RANK"].ToString();
                //txtPer.Text = dsStudent.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                //txtQExamRollNo.Text = dsStudent.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                //txtPercentile.Text = dsStudent.Tables[0].Rows[0]["PERCENTILE"].ToString();
                ddlAllotedCat.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                //ddlExamNo.SelectedValue = (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString());
                txtStudMobile.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                txtStudMobile2.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE2"].ToString();
                txtParentmobno.Text = dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString();
                txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                txtPostalAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                txtGuardianPhone.Text = dsStudent.Tables[0].Rows[0]["LTELEPHONE"].ToString();
                txtGuardianMobile.Text = dsStudent.Tables[0].Rows[0]["LMOBILE"].ToString();
                txtGuardianEmail.Text = dsStudent.Tables[0].Rows[0]["LEMAIL"].ToString();
                ddlBloodGrp.SelectedValue = (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString());
                //ddlQuota.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString()), "ACD_QUOTA", "QUOTANO", "QUOTA"));
                txtLocalCity.Text = (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                txtLocalState.Text = (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                //PHYSICAL HADICAPP
                ddlPhyHandicap.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString());
                //ROUND FOR MCA
                ddladmthrough.SelectedValue = (dsStudent.Tables[0].Rows[0]["ADMROUNDNO"].ToString());
                ddlSemester.SelectedValue = (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                //show the student photo and sign code              
                //txtmerirtno.Text = dsStudent.Tables[0].Rows[0]["MERITNO"].ToString();
                //txtscore.Text = dsStudent.Tables[0].Rows[0]["SCORE"].ToString();
                //txtapplicationid.Text = dsStudent.Tables[0].Rows[0]["APPLICATIONID"].ToString();

                txtAadhaarNo.Text = dsStudent.Tables[0].Rows[0]["ADDHARCARDNO"].ToString();

                objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
                ddlstate.SelectedValue = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString());
                objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");
                ddlCity.SelectedValue = dsStudent.Tables[0].Rows[0]["PCITY"].ToString();

                //show the student photo and sign code
                //int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString())));
                //if (idno > 0)
                //    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=student";
                //ImgSign.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=studentsign";


            }


        }
        else
        {
            objCommon.DisplayMessage(this, "This Registration No is not found in the System", this.Page);
            Response.Redirect(Request.Url.ToString());
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //ddlBatch.Enabled = true;
        //string regNo = string.Empty;

        int BranchNo = 0;
        string IUEmail = string.Empty;
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        GEC_Student objStud = new GEC_Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        UserAcc objUa = new UserAcc();

        try
        {
            int idno = Convert.ToInt32(Session["stuinfoidno"]);

            int DualDegreeStatus = Convert.ToInt32(ddlTypeofStudent.SelectedValue);

            if (rdoMale.Checked == false && rdoFemale.Checked == false && rdoFemale.Checked == false)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Gender!", this.Page);
                return;
            }

           

            if (ddlSpecialisation.SelectedIndex > 0)
            {
                objS.Specialization = "1";
                objS.BranchNo = Convert.ToInt32(ddlSpecialisation.SelectedValue);
                BranchNo = Convert.ToInt32(ddlSpecialisation.SelectedValue);
            }
            else
            {
                objS.Specialization = "0";
                objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            }
            DataSet dsstandardfees = objSC.GetStandardFeesDetails(Convert.ToInt32(ddlCollegeNew.SelectedValue), Convert.ToInt32(ddlDegreeNew.SelectedValue), BranchNo , "TF", Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlPaymentTypeNew.SelectedValue), Convert.ToInt32(Session["OrgId"]));
            int Count = 0;

            if (dsstandardfees.Tables[0].Rows.Count > 0)
            {
                Count = Convert.ToInt32(dsstandardfees.Tables[0].Rows[0]["COUNT"].ToString());
                //objCommon.DisplayMessage(this.Page, "Standard Fees is Not Defined For Selected ", this.Page);
            }
            if (Count == 0)
            {
                objCommon.DisplayMessage(this.Page, "Standard Fees is Not Defined Please Define Standard Fees First.", this.Page);
            }
            else
            {

                DateTime dtmin = new DateTime(1753, 1, 1);
                DateTime dtmax = new DateTime(9999, 12, 31);
                if (!string.IsNullOrEmpty(txtDateOfBirth.Text.Trim()))
                {
                    if ((DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmin) <= 0) ||
                        (DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmax) >= 0))
                    {
                        //1/1/1753 12:00:00 AM and 12/31/9999
                        objCommon.DisplayMessage(this.updStudent, "Please Enter Valid Date!!", this.Page);
                        return;
                    }
                }

                int lmobile = (txtStudMobile.Text).Length;

                if (lmobile < 10 && lmobile != 0)
                {
                    objCommon.DisplayUserMessage(updStudent, "Please enter 10 digit mobile Number", this.Page);
                    return;
                }


                int Pmobile = (txtParentmobno.Text).Length;

                if (Pmobile < 10 && Pmobile != 0)
                {
                    objCommon.DisplayUserMessage(updStudent, "Please enter 10 digit Parent mobile Number", this.Page);
                    return;
                }

                if (ddlBranch.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updStudent, "Please select branch!", this.Page);
                    return;
                }

                if (!txtStudentfullName.Text.Trim().Equals(string.Empty))
                    objS.StudName = txtStudentfullName.Text.Trim();
                if (!txtStudentMiddleName.Text.Trim().Equals(string.Empty))
                    objS.MiddleName = txtStudentMiddleName.Text.Trim();
                if (!txtStudentLastName.Text.Trim().Equals(string.Empty))
                    objS.LastName = txtStudentLastName.Text.Trim();
                if (!txtFatherName.Text.Trim().Equals(string.Empty))
                    objS.FatherName = txtFatherName.Text.Trim();
                if (!txtFatherMiddleName.Text.Trim().Equals(string.Empty))
                    objS.FatherMiddleName = txtFatherMiddleName.Text.Trim();
                if (!txtFatherLastName.Text.Trim().Equals(string.Empty))
                    objS.FatherLastName = txtFatherLastName.Text.Trim();
                if (!txtMotherName.Text.Trim().Equals(string.Empty))
                    objS.MotherName = txtMotherName.Text.Trim();
                if (!txtDateOfBirth.Text.Trim().Equals(string.Empty))
                    objS.Dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
                //if (!txtDateOfReporting.Text.Trim().Equals(string.Empty))
                //    objS.DOR = Convert.ToDateTime(txtDateOfReporting.Text.Trim());
                if (!txtStudMobile.Text.Trim().Equals(string.Empty))
                    objS.StudentMobile = txtStudMobile.Text.Trim();
                if (!txtStudMobile2.Text.Trim().Equals(string.Empty))
                    objS.StudMobileno2 = txtStudMobile2.Text.Trim(); 
                if (!txtParentmobno.Text.Trim().Equals(string.Empty))
                    objS.FatherMobile = txtParentmobno.Text.Trim();
                if (!txtcetcomorederno.Text.Trim().Equals(string.Empty))
                    objS.Cetorderno = txtcetcomorederno.Text.Trim();
                if (!txtcetcomdate.Text.Trim().Equals(string.Empty))
                    objS.Cetdate = txtcetcomdate.Text.Trim();
                if (!txtfeepaid.Text.Trim().Equals(string.Empty))
                    objS.Cetamount = Convert.ToDecimal(txtfeepaid.Text.Trim());

                objS.CollegeJss = ddlcolcode.SelectedItem.Text;

                if (rdoMale.Checked)
                    objS.Sex = 'M';
                else if (rdoFemale.Checked)
                    objS.Sex = 'F';
                else if (rdoTransGender.Checked)
                    objS.Sex = 'T';
                else
                    objCommon.DisplayMessage(this.updStudent, "Please Select Gender.", this.Page);

                if (rdoMarriedNo.Checked)
                    objS.Married = 'N';
                else
                    objS.Married = 'Y';

                if (!txtPermanentAddress.Text.Trim().Equals(string.Empty))
                    objSAddress.PADDRESS = txtPermanentAddress.Text.Trim();
                if (!txtMobileNo.Text.Trim().Equals(string.Empty))
                    objSAddress.PMOBILE = txtMobileNo.Text.Trim();

                if (!txtPIN.Text.Trim().Equals(string.Empty))
                    objSAddress.PPINCODE = txtPIN.Text.Trim();
                if (!txtContactNumber.Text.Trim().Equals(string.Empty))
                    objSAddress.PTELEPHONE = txtContactNumber.Text.Trim();


                //NEW DEGREE BRANCH AND ADMISSION BATCH
                objS.College_ID = Convert.ToInt32(ddlCollegeNew.SelectedValue);
                objS.DegreeNo = Convert.ToInt32(ddlDegreeNew.SelectedValue);
                objS.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);

                //objS.AdmBatch = Convert.ToInt32(Session["Admbatch"]);
                objS.PType = Convert.ToInt32(ddlPaymentTypeNew.SelectedValue);

                objS.ExamPtype = Convert.ToInt32(ddlPaymentType.SelectedValue);



                objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
                objS.CountryDomicile = ddlstate.SelectedValue;
                objS.SemesterNo = Convert.ToInt32(ddlSemesterNew.SelectedValue);
                objS.CollegeCode = Session["colcode"].ToString();
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = ViewState["ipAddress"].ToString();
                objS.IdType = Convert.ToInt32(ddlAdmtypeNew.SelectedValue);



                //if (fuPhotoUpload.HasFile)
                //{
                //    objSPhoto.Photo1 = this.ResizePhoto(fuPhotoUpload);
                //}
                //else
                //{
                //    objSPhoto.Photo1 = null;
                //}


                //if (fuSignUpload.HasFile)
                //{
                //    objSPhoto.SignPhoto = this.ResizePhotoSign(fuSignUpload);
                //}
                //else
                //{
                //    objSPhoto.SignPhoto = null;
                //}

                if (fuPhotoUpload.HasFile)
                {
                    objSPhoto.Photo1 = objCommon.GetImageData(fuPhotoUpload);
                }
                else
                {
                    objSPhoto.SignPhoto = null;
                }

                if (fuSignUpload.HasFile)
                {
                    objSPhoto.Photo1 = objCommon.GetImageData(fuSignUpload);
                }
                else
                {
                    objSPhoto.SignPhoto = null;
                }

                // objS.AdmroundNo = Convert.ToInt32(ddlAdmRound.SelectedValue);
                objS.PH = ddlPhyHandicap.SelectedValue;
                objS.ReligionNo = Convert.ToInt32(ddlReligion.SelectedValue);
                objS.NationalityNo = Convert.ToInt32(ddlNationality.SelectedValue);

                objSAddress.PCITY = Convert.ToInt32(objCommon.GetIDNo(txtCity));
                if (objSAddress.PCITY == 0 && txtCity.Text.Trim() != string.Empty)
                    objSAddress.PCITY = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtCity.Text.Trim().ToUpper(), 0);
                if (objSAddress.PCITY == -99)
                    objSAddress.PCITY = 0;


                objS.CategoryNo = Convert.ToInt32(ddlAllotedCat.SelectedValue);

                objSAddress.PSTATE = Convert.ToInt32(objCommon.GetIDNo(txtState));
                if (objSAddress.PSTATE == 0 && txtState.Text.Trim() != string.Empty)
                    objSAddress.PSTATE = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtState.Text.Trim().ToUpper(), 0);
                if (objSAddress.PSTATE == -99)
                    objSAddress.PSTATE = 0;

                objS.StateNo = Convert.ToInt32(ddlstate.SelectedValue);
                objS.City = Convert.ToInt32(ddlCity.SelectedValue);
                objS.ClaimType = Convert.ToInt32(ddlClaimedCat.SelectedValue);
                objS.AdmCategoryNo = Convert.ToInt32(ddlAllotedCat.SelectedValue);
                objS.BloodGroupNo = Convert.ToInt32(ddlBloodGrp.SelectedValue);
                objS.AdmroundNo = Convert.ToInt32(ddladmthrough.SelectedValue);


                //ENTRANCE EXAM DETAILS.

                if (divotherentrance.Visible)
                {
                    objS.QUALIFYNO = txtothetentrance.Text.ToString();
                }
                else
                {
                    objS.QUALIFYNO = ddlExamNo.SelectedValue.Trim();
                }
                //if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) objS.ALLINDIARANK = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
                if (!txtJeeRankNo.Text.Trim().Equals(string.Empty))
                    objS.ALLINDIARANK = Convert.ToInt32(txtJeeRankNo.Text.Trim());
                if (!txtJeeRollNo.Text.Trim().Equals(string.Empty))
                    objS.QexmRollNo = txtJeeRollNo.Text.Trim();
                objS.Qualifyexamname = Convert.ToString(ddlExamNo.SelectedItem.Text.ToString().Trim());

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

                //objS.ADMQUOTANO = Convert.ToInt32(ddlQuota.SelectedValue);

                objSAddress.LCITY = Convert.ToInt32(objCommon.GetIDNo(txtLocalCity));
                if (objSAddress.LCITY == 0 && txtLocalCity.Text.Trim() != string.Empty)
                    objSAddress.LCITY = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtLocalCity.Text.Trim().ToUpper(), 0);
                if (objSAddress.LCITY == -99)
                    objSAddress.LCITY = 0;

                objSAddress.LSTATE = Convert.ToInt32(objCommon.GetIDNo(txtLocalState));
                if (objSAddress.LSTATE == 0 && txtLocalState.Text.Trim() != string.Empty)
                    objSAddress.LSTATE = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtLocalState.Text.Trim().ToUpper(), 0);
                if (objSAddress.LSTATE == -99)
                    objSAddress.LSTATE = 0;
                if (!txtIUEmail.Text.Trim().Equals(string.Empty))
                    IUEmail = txtIUEmail.Text.Trim();

                objS.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);

                //if (txtAadhaarNo.Text != string.Empty)
                //{
                //    int ladhar = (txtAadhaarNo.Text).Length;

                //    if (ladhar < 12 && ladhar != 0)
                //    {
                //        objCommon.DisplayUserMessage(updStudent, "Please Enter 12 digit Aadhar No.", this.Page);
                //        return;
                //    }
                //}

                objS.AddharcardNo = (txtAadhaarNo.Text.Trim() != string.Empty) ? txtAadhaarNo.Text.Trim().ToString() : string.Empty; //Added by Irfan Shaikh on 09/04/2019

                ///---------------------------////
                ///Generate OTP as a Password////
                // ViewState["Otp"] = GeneratePassword();
                //string pwd = string.Empty;

                //pwd = GeneratePassword();

                //if (pwd != null)
                //{
                //    objUa.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                //    //objUa.UA_Pwd = Common.EncryptPassword(ViewState["Otp"].ToString());
                //}
                //int USERNO = 0;
                //int ExistCount = 0;
                //if (Convert.ToInt32(Session["OrgId"]) != 1 || Convert.ToInt32(Session["OrgId"]) != 2 || Convert.ToInt32(Session["OrgId"]) != 6)
                //{
                //    USERNO = Convert.ToInt32(Session["USERNO_OA"]);

                //    ExistCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION UR INNER JOIN ACD_STUDENT S ON (UR.USERNO=S.USERNO)", "COUNT(S.USERNO)", "S.USERNO=" + USERNO));

                //}
                //else
                //{
                //    USERNO = 0;
                //}
                //if (Convert.ToInt32(Session["OrgId"]) != 1 || Convert.ToInt32(Session["OrgId"]) != 2 || Convert.ToInt32(Session["OrgId"]) != 6)
                //{

                //    if (ExistCount > 0)
                //    {
                //        objCommon.DisplayMessage(this.Page, "Admission is Already done for Entered Application ID", this.Page);
                //        return;
                //    }

                //}               
                string output = objSC.AddDualDegreeeNewStudent(objS, objStud, IUEmail, objUa, DualDegreeStatus, idno);
                objCommon.DisplayMessage(this.Page, "You are Successfully Admitted for Dual Degree.", this.Page);
                //CLEARALLFIELD();  
                divSearchInfo.Visible = false;
                EnableFields();
                //clearallfield();

             }            
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void clearallfield()
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
         
        Response.Redirect(Request.Url.ToString());
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
    private void DisableFields()
    {
        txtStudentfullName.Enabled = false;
        txtFatherName.Enabled = false;
        txtStudMobile.Enabled = true;
        txtStudMobile2.Enabled = true;
        txtStudEmail.Enabled = false;
        txtState.Enabled = false;
        ddlCity.Enabled = false;
        ddlSchool.Enabled = false;
        ddlDegree.Enabled = false;
        ddlBranchShow.Enabled = false;
        ddlSemester.Enabled = false;
        ddlBatch.Enabled = false;      
        // txtAge.Attributes.
        ddlBatch.Attributes.Add("readonly", "readonly");
        ddlAdmType.Enabled = false;
        ddlCategory.Enabled = false;
        txtDateOfBirth.Enabled = false;
        //txtDateOfReporting.Enabled = false;
        ddlPaymentType.Enabled = false;
        txtAadhaarNo.Enabled = false;
        ddlAllotedCat.Enabled = false;
        txtStudentfullName.Enabled = false; 
        ddlBloodGrp.Enabled = false;
        ddlAdmType.Enabled = false;
        //btnSave.Enabled = false;
        ddlYear.Enabled = false;           
        ddladmthrough.Enabled = false;
        txtParentmobno.Enabled = false;
        ddlstate.Enabled = false;
        ddlstate.Attributes.Add("readonly", "readonly");
        txtPermanentAddress.Enabled=false;
        txtCity.Enabled=false;
        txtPIN.Enabled=false;
        txtMobileNo.Enabled=false;
        txtContactNumber.Enabled=false;
        txtPostalAddress.Enabled=false;
        txtLocalCity.Enabled=false;
        txtLocalState.Enabled=false;
        txtGuardianMobile.Enabled=false;
        txtGuardianPhone.Enabled=false;
        txtGuardianEmail.Enabled = false;
    }

    private void DisableFieldsSibling()
    {
        txtStudentfullName.Enabled = true;     
        txtFatherName.Enabled = true;
        txtStudMobile.Enabled = true;
        txtStudMobile2.Enabled = true;
        txtStudEmail.Enabled = false;
        txtState.Enabled = false;
        ddlCity.Enabled = false;
        ddlDegree.Enabled = false;
        ddlSchool.Enabled = false;
        ddlDegree.Enabled = false;
        ddlBranchShow.Enabled = false;
        ddlSemester.Enabled = false;
        ddlBatch.Enabled = false;
        // txtAge.Attributes.
        ddlBatch.Attributes.Add("readonly", "readonly");
        ddlAdmType.Enabled = false;
        ddlCategory.Enabled = false;
        txtDateOfBirth.Enabled = false;
        //txtDateOfReporting.Enabled = false;
        ddlPaymentType.Enabled = false;
        txtAadhaarNo.Enabled = false;
        ddlAllotedCat.Enabled = false;
        txtStudentfullName.Enabled = true;
        ddlBloodGrp.Enabled = false;
        ddlAdmType.Enabled = false;
        //btnSave.Enabled = false;
        ddlYear.Enabled = false;
        ddladmthrough.Enabled = false;
        txtParentmobno.Enabled = false;
        ddlstate.Enabled = false;
        ddlstate.Attributes.Add("readonly", "readonly");
        txtPermanentAddress.Enabled = false;
        txtCity.Enabled = false;
        txtPIN.Enabled = false;
        txtMobileNo.Enabled = false;
        txtContactNumber.Enabled = false;
        txtPostalAddress.Enabled = false;
        txtLocalCity.Enabled = false;
        txtLocalState.Enabled = false;
        txtGuardianMobile.Enabled = false;
        txtGuardianPhone.Enabled = false;
        txtGuardianEmail.Enabled = false;
    }

    private void EnableFields()
    {
        txtStudMobile2.Enabled = true;
        txtFatherName.Enabled = true;
        txtStudentfullName.Enabled = true;
        txtStudMobile.Enabled = true;
        txtStudEmail.Enabled = true;
        txtState.Enabled = true;
        ddlCity.Enabled = true;
        ddlSchool.Enabled = true;
        // ddlDegree.Enabled = true;
        ddlBranch.Enabled = true;
        ddlSemester.Enabled = true;
        ddlBatch.Enabled = true;
        ddlAdmType.Enabled = true;
        ddlCategory.Enabled = true;
        txtDateOfBirth.Enabled = true;
       // txtDateOfReporting.Enabled = true;
        ddlPaymentType.Enabled = true;
        txtAadhaarNo.Enabled = true;
        ddlAllotedCat.Enabled = true;
        ddlstate.Enabled = true;
        txtStudentfullName.Enabled = true;      
        ddlBloodGrp.Enabled = true;
        ddlAdmType.Enabled = true;
        //btnSave.Enabled = true;
        ddlYear.Enabled = true;       
        ddladmthrough.Enabled = true;
        txtParentmobno.Enabled = true;
        txtPermanentAddress.Enabled = true;
        txtCity.Enabled = true;
        txtPIN.Enabled = true;
        txtMobileNo.Enabled = true;
        txtContactNumber.Enabled = true;
        txtPostalAddress.Enabled = true;
        txtLocalCity.Enabled = true;
        txtLocalState.Enabled = true;
        txtGuardianMobile.Enabled = true;
        txtGuardianPhone.Enabled = true;
        txtGuardianEmail.Enabled = true;
    }
     
   
    protected void ddlSpotOption_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void ddlDegreeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeNew.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegreeNew.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(B.ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue), "B.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {        
            ddlBranch.SelectedIndex = 0;
            objCommon.DisplayMessage(updStudent, "Please select college/school!", this.Page);
            return;
        }  
        ddlBranch.SelectedIndex = 0;
    }
    protected void ddlCollegeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollegeNew.SelectedIndex > 0)
        {        
            objCommon.FillDropDownList(ddlDegreeNew, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollegeNew.SelectedValue, "D.DEGREENAME");
            ddlDegreeNew.Focus();            
        }
        else
        {     
            ddlDegreeNew.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;          
        }        
        ddlDegreeNew.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "count(1)", "CORE_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegreeNew.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue)));
            if (count > 0)
            {
                divSpecialisation.Visible = true;
                objCommon.FillDropDownList(ddlSpecialisation, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegreeNew.SelectedValue) + " AND ISNULL(B.ISCORE,0)=1 AND ISNULL(ISSPECIALISATION,0) = 1 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue) + " AND CD.CORE_BRANCHNO =" + Convert.ToInt32(ddlBranch.SelectedValue), "B.LONGNAME");

            }
            else
            {
                divSpecialisation.Visible = false;
            }
        }
        else
        {
            divSpecialisation.Visible = false;

        }
        
    }
    


    protected void ddlTypeofStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTypeofStudent.SelectedValue == "1" || ddlTypeofStudent.SelectedValue == "2")
        {
            divSearchInfo.Visible = true;
            this.ShowStudentDetails();
            DisableFields();
        }
        else if (ddlTypeofStudent.SelectedValue == "3")
        {
            divSearchInfo.Visible = true;
            DisableFieldsSibling();
        }
        else
        {
            divSearchInfo.Visible = false;
            EnableFields();
        }
       
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        ViewState["idno"] = Session["stuinfoidno"].ToString();  

       // divdata.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        updEdit.Visible = false;
       // pnlstudetails.Visible = true;
        divtypeofStudent.Visible = true;
       // divSearchInfo.Visible = true;
        pnlbody.Visible = false;
        ShowStudentDetails();
       // ShowDetails(Convert.ToInt32(lnk.CommandArgument));
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            value = ddlDropdown.SelectedValue;
        else
            value = txtStudent.Text;

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtStudent.Text = string.Empty;
      //  divdata.Visible = false;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //divdata.Visible = false;
            //ClearControls();
            if (ddlSearch.SelectedIndex > 0)
            {
                txtStudent.Text = string.Empty;
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
                        txtStudent.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        //lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtStudent.Visible = true;
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
    }
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
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
    private void PopulateDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
            ddlSearch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSemesterNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}