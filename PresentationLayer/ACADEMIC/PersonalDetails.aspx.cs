using System;
using System.Data;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Collections.Generic;

public partial class ACADEMIC_PersonalDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    ModuleConfigController objConfig = new ModuleConfigController();

    List<string> validationErrors = new List<string>();

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
                //Page Authorization
                //  CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];

                this.FillDropDown();
                ShowStudentDetails();
                StudentConfiguration();
                rdoParents.Visible = true;                         //Added by sachin on 19-07-2022

                int orgID = Convert.ToInt32(objCommon.LookUp("REFF", "ORGANIZATIONID", ""));

                if (rdofatheralive.SelectedValue == "1")
                {
                    MotherSection.Visible = true;
                    FatherSection.Visible = true;
                }
                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetails.Visible = false;
                    divAdmissionApprove.Visible = false;

                    int FinalSubmit = 0;
                    if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                    {
                        FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    }
                    if (FinalSubmit == 1)
                    { divPrintReport.Visible = true; }
                    else
                    { divPrintReport.Visible = false; }

                    if (orgID == 15)
                    {
                        txtDateOfBirth.Enabled = false;
                        ddlBloodGroupNo.Enabled = false;
                        ddladmthrough.Enabled = false;
                        rdobtn_Gender.Enabled = false;
                        rdbTransport.SelectedValue = "0";
                        rdbTransport.Enabled = false;
                        fuPhotoUpload.Enabled = false;
                        btnPhotoUpload.Visible = false;
                        fuSignUpload.Enabled = false;
                        btnSignUpload.Visible = false;
                    }
                    else if (orgID == 3)
                    {
                        fuPhotoUpload.Enabled = false;
                        btnPhotoUpload.Visible = false;
                        fuSignUpload.Enabled = false;
                        btnSignUpload.Visible = false;
                    }

                    //divPrintReport.Visible = true;
                    // btnGohome.Visible = false;
                    divhome.Visible = false;
                    divtxtidno.Visible = false;
                    txtIDNo.Visible = false;
                    txtStudMobile.Enabled = false;
                    txtStudentEmail.Enabled = false;
                    txtStudFullname.Enabled = false;
                    //txtPaymentType.Enabled = false;
                    ddlPayType.Enabled = false;

                    string status = objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "STATUS", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    DataSet dsinfo = objCommon.FillDropDown("ACD_ADM_STUD_INFO_SUBMIT_LOG", "PERSONAL_INFO,ADDRESS_INFO,DOC_INFO,QUAL_INFO,OTHER_INFO,FINAL_SUBMIT", "ADMBATCH", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", string.Empty);
                    if (dsinfo != null && dsinfo.Tables[0].Rows.Count > 0)
                    {
                        string personal_info = dsinfo.Tables[0].Rows[0]["PERSONAL_INFO"].ToString();
                        string address_info = dsinfo.Tables[0].Rows[0]["ADDRESS_INFO"].ToString();
                        string doc_info = dsinfo.Tables[0].Rows[0]["DOC_INFO"].ToString();
                        string qual_info = dsinfo.Tables[0].Rows[0]["QUAL_INFO"].ToString();
                        string other_info = dsinfo.Tables[0].Rows[0]["OTHER_INFO"].ToString();
                        string final_submit = dsinfo.Tables[0].Rows[0]["FINAL_SUBMIT"].ToString();
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
                            btnSubmit.Visible = false;
                        }

                        DataSet ds = objCommon.FillDropDown("ACD_STUD_PHOTO", "PHOTO", "STUD_SIGN", "IDNO=" + Convert.ToInt32(Session["idno"]), "");

                        if (ds != null && ds.Tables[0].Rows.Count > 0 && final_submit == "1")
                        {
                            string photo = ds.Tables[0].Rows[0]["PHOTO"].ToString();
                            string sign = ds.Tables[0].Rows[0]["STUD_SIGN"].ToString();

                            if (photo != string.Empty)
                            {
                                fuPhotoUpload.Enabled = false;
                                btnPhotoUpload.Visible = false;
                            }

                            if (sign != string.Empty)
                            {
                                fuSignUpload.Enabled = false;
                                btnSignUpload.Visible = false;
                            }
                        }                 
                    }
                    CheckFinalSubmission(); // Added by Bhagyashree on 30052023
                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    txtIDNo.Visible = true;
                    divtxtidno.Visible = true;
                    divadmissiondetails.Visible = false;
                    divAdmissionApprove.Visible = true;
                    // btnGohome.Visible = true;
                    divhome.Visible = true;
                    //txtEnrollno.Enabled = true;
                    txtStudMobile.Enabled = true;
                    txtStudentEmail.Enabled = true;
                    txtStudFullname.Enabled = true;
                    //txtPaymentType.Enabled = true;
                    ddlPayType.Enabled = true;
                    btnSubmit.Visible = false;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                }
                else
                {
                    txtStudFullname.Enabled = true;
                    txtIDNo.Visible = true;
                    divtxtidno.Visible = true;
                    divadmissiondetails.Visible = true;
                    divAdmissionApprove.Visible = true;
                    // btnGohome.Visible = true;
                    divhome.Visible = true;
                    // txtEnrollno.Enabled = true;
                    txtStudMobile.Enabled = true;
                    txtStudentEmail.Enabled = true;                    
                    // txtPaymentType.Enabled = false;
                    ddlPayType.Enabled = false;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;

                    
                }
                //if (ViewState["usertype"].ToString() == "2")
                //{
                //    CheckFinalSubmission();
                //}
            }
            string filepath = Server.MapPath("~//XML//PersonalDetails.xml");
            if (System.IO.File.Exists(filepath) == true)
            {
                ManagePageControlsModule.ManagePageControls(Page, filepath);
            }
        }
    }

    #region Student Related Configuration
    protected void StudentConfiguration()
    {
        DataSet ds = null;

        int orgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        string pageNo = "";
        string pageName = "PersonalDetails.aspx";
        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            string captionName = row["CAPTION_NAME"].ToString();
            string isActive = row["ISACTIVE"].ToString();
            string controlToHide = row["CONTROL_TO_HIDE"].ToString();
            string controlToMandatory = row["CONTROL_TO_MANDATORY"].ToString();
            string isMandatory = row["ISMANDATORY"].ToString();
            string controlID = string.Empty;
            string divID = string.Empty;
            Control control = null, control2 = null, control3 = null;
            string[] values = controlToHide.Split(',');

            if (values.Length == 2)
            {
                controlID = values[0].Trim();
                divID = values[1].Trim();

            }

            if (values.Length == 2)
            {
                control = FindControlRecursive(Page, divID);
                control3 = FindControlRecursive(Page, controlID);
            }
            else
            {
                control = FindControlRecursive(Page, controlToHide);
            }

            control2 = FindControlRecursive(Page, controlToMandatory);


            if (control != null)
            {
                if (isActive == "checked" && isMandatory == "checked")
                {
                    control.Visible = true;
                    control2.Visible = true;

                }
                else if (isActive == "checked" && controlToMandatory != null)
                {
                    control.Visible = true;
                    control2.Visible = false;
                }
                else
                {
                    control.Visible = false;
                    control2.Visible = false;

                    if (values.Length == 2)
                    {
                        ClearControlValue(control3);
                    }

                }
            }
        }
    }

    private Control FindControlRecursive(Control parentControl, string controlId)
    {
        if (parentControl == null)
        {
            return null;
        }

        Control control = parentControl.FindControl(controlId);

        if (control == null)
        {
            foreach (Control childControl in parentControl.Controls)
            {
                control = FindControlRecursive(childControl, controlId);
                if (control != null)
                {
                    return control;
                }
            }
        }
        return control;
    }

    private void ClearControlValue(Control control)
    {
        if (control is TextBox)
        {
            ((TextBox)control).Text = string.Empty;
        }
        else if (control is DropDownList)
        {
            ((DropDownList)control).SelectedIndex = 0;
        }

    }

    public string ValidationAlert()
    {
        DataSet ds = null;

        int orgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        string pageNo = "";
        string pageName = "PersonalDetails.aspx";
        string idno = string.Empty;
        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName);

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = (Session["idno"]).ToString();
        }
        else
        {
            idno = (Session["stuinfoidno"]).ToString();
        }

        DataSet dsConfig = objCommon.FillDropDown("ACD_STUD_PHOTO", "PHOTO", "STUD_SIGN", "IDNO=" + Convert.ToInt32(idno), "");

        string photo = dsConfig.Tables[0].Rows[0]["PHOTO"].ToString();
        string sign = dsConfig.Tables[0].Rows[0]["STUD_SIGN"].ToString();

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            string captionName = row["CAPTION_NAME"].ToString();
            string controlToHide = row["CONTROL_TO_HIDE"].ToString();
            string isMandatory = row["ISMANDATORY"].ToString();
            string controlID = string.Empty;
            string[] values = controlToHide.Split(',');

            if (values.Length == 2)
            {
                controlID = values[0].Trim();
            }

            if (isMandatory == "checked" && !string.IsNullOrEmpty(controlID))
            {
                Control control = FindControlRecursive(Page, controlID);

                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    if (string.IsNullOrEmpty(textBox.Text.Trim()))
                    {
                        validationErrors.Add("Please Enter " + captionName);
                    }
                }

                if (control is DropDownList)
                {
                    DropDownList dropdownlist = (DropDownList)control;
                    if (dropdownlist.SelectedIndex == 0)
                    {
                        validationErrors.Add("Please Select " + captionName);
                    }
                }

                if (ViewState["usertype"].ToString() == "2" && control is FileUpload)
                {
                    FileUpload fileUploadControl = (FileUpload)control;

                    if (captionName == "Photo" && string.IsNullOrEmpty(photo))
                    {
                        validationErrors.Add("Please Upload a File for " + captionName);
                    }

                    if (captionName == "Signature" && string.IsNullOrEmpty(sign))
                    {
                        validationErrors.Add("Please Upload a File for " + captionName);
                    }
                }
            }

        }

        if (validationErrors.Count > 0)
        {
            string errorMessage = string.Join(",", validationErrors);
            return errorMessage;
        }
        return string.Empty;
    }

    #endregion Student Related Configuration     // Added By Shrikant W. on 05-09-2023

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
        }
    }
    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "NATIONALITY ASC");
            ddlNationality.SelectedValue = "1";
            objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "RELIGION");
            objCommon.FillDropDownList(ddlOccupationNo, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0 AND ISNULL(ACTIVESTATUS,0) = 1", "OCCNAME");
            objCommon.FillDropDownList(ddlMotherOccupation, "ACD_OCCUPATION", "OCCUPATION", "OCCNAME", "OCCUPATION>0 AND ISNULL(ACTIVESTATUS,0) = 1", "OCCNAME");
            objCommon.FillDropDownList(ddlCasteCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CATEGORY");

            objCommon.FillDropDownList(ddlClaimedcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CATEGORY");
            objCommon.FillDropDownList(ddlCaste, "ACD_CASTE", "CASTENO", "CASTE", "CASTENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CASTE");
            objCommon.FillDropDownList(ddlHandicap, "ACD_PHYSICAL_HANDICAPPED", "HANDICAP_NO", "HANDICAP_NAME", "HANDICAP_NO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "HANDICAP_NO");
            objCommon.FillDropDownList(ddlBloodGroupNo, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "BLOODGRPNAME");
            objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "PAYTYPENAME");
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "IDTYPEDESCRIPTION");
            objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO"); //Added by sachin on 28-07-2022 
            //ddlHandicap.SelectedValue = "1";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckFinalSubmission()
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        DataSet dsallowprocess = objSC.GetAllowProcess(Convert.ToInt32(Session["idno"]), 1, 'E');
        int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        if (finalsubmit == "1" && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
        {
            btnSubmit.Visible = true;
            fuPhotoUpload.Enabled = true;
            btnPhotoUpload.Visible = true;
            fuSignUpload.Enabled = true;
            btnSignUpload.Visible = true;

        }
    }
    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
            txtStudentName.ReadOnly = false;
            txtStudentName.Visible = true;
            ddlHandicap.Enabled = true;
        }

        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
            ddlHandicap.Enabled = true;
        }


        if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6 || Convert.ToInt32(Session["OrgId"]) == 8)    //Added by sachin on 28-07-2022
        {

            if (ViewState["usertype"].ToString() == "2")
            {
                ddladmthrough.Enabled = false;
            }
            else
            {
                ddladmthrough.Enabled = true;
            }
        }
        else
        {

            ddladmthrough.Enabled = true;

        }

        if (dtr != null)
        {
            if (dtr.Read())
            {
                Session["stuinfoenrollno"] = dtr["REGNO"].ToString();

                txtIDNo.Text = dtr["IDNO"].ToString();
                txtIDNo.ToolTip = dtr["REGNO"].ToString();
                txtRegNo.ToolTip = dtr["REGNO"].ToString();

                txtEnrollno.ToolTip = dtr["ENROLLNO"].ToString();
                txtEnrollno.Text = dtr["ENROLLNO"].ToString();
                //txtsrno.Text = dtr["ENROLLNO"].ToString();
                //txtsrno.ToolTip = dtr["ENROLLNO"].ToString();
                txtsrno.Text = dtr["REGNO"].ToString();
                txtsrno.ToolTip = dtr["REGNO"].ToString();
                txtRegNo.Text = dtr["REGNO"].ToString();
                txtStudFullname.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                txtStudentName.Text = dtr["STUDFIRSTNAME"] == null ? string.Empty : dtr["STUDFIRSTNAME"].ToString();
                txtStudMiddleName.Text = dtr["STUDMIDDLENAME"] == null ? string.Empty : dtr["STUDMIDDLENAME"].ToString();
                txtStudLastName.Text = dtr["STUDLASTNAME"] == null ? string.Empty : dtr["STUDLASTNAME"].ToString();
                txtFatherName.Text = dtr["FATHERFIRSTNAME"] == null ? string.Empty : dtr["FATHERFIRSTNAME"].ToString().ToUpper();
                txtFatherFullName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();// ADDED CHANGES ON 19-05-2023 BY KAJAL JAISWAL FOR SHOWING FATHER FULL NAME.
                txtFatherMiddleName.Text = dtr["FATHERMIDDLENAME"] == null ? string.Empty : dtr["FATHERMIDDLENAME"].ToString().ToUpper();
                txtFatherLastName.Text = dtr["FATHERLASTNAME"] == null ? string.Empty : dtr["FATHERLASTNAME"].ToString().ToUpper();
                txtFatherMobile.Text = dtr["FATHERMOBILE"] == null ? string.Empty : dtr["FATHERMOBILE"].ToString();
                //aayushi
                txtfatheremailid.Text = dtr["FATHER_EMAIL"] == null ? string.Empty : dtr["FATHER_EMAIL"].ToString();
                txtmotheremailid.Text = dtr["MOTHER_EMAIL"] == null ? string.Empty : dtr["MOTHER_EMAIL"].ToString();
                txtAnnualIncome.Text = dtr["ANNUAL_INCOME"] == null ? string.Empty : dtr["ANNUAL_INCOME"].ToString();
                txtFathersOfficeNo.Text = dtr["FATHEROFFICENO"] == null ? string.Empty : dtr["FATHEROFFICENO"].ToString();
                txtMotherName.Text = dtr["MOTHERNAME"] == null ? string.Empty : dtr["MOTHERNAME"].ToString().ToUpper();
                txtMotherMobile.Text = dtr["MOTHERMOBILE"] == null ? string.Empty : dtr["MOTHERMOBILE"].ToString();
                txtMothersOfficeNo.Text = dtr["MOTHEROFFICENO"] == null ? string.Empty : dtr["MOTHEROFFICENO"].ToString();
                txtDateOfBirth.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                txtStudentEmail.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                txtInstituteEmail.Text = dtr["EMAILID_INS"] == null ? string.Empty : dtr["EMAILID_INS"].ToString();
                txtStudMobile.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                //27-06-2014
                txtSubCaste.Text = dtr["SUB_CASTE"] == null ? string.Empty : dtr["SUB_CASTE"].ToString();
                txtBirthPlace.Text = dtr["BIRTH_PLACE"] == null ? string.Empty : dtr["BIRTH_PLACE"].ToString();
                txtCaste.Text = dtr["CASTENAME"] == null ? string.Empty : dtr["CASTENAME"].ToString();    // Added By Shrikant W. on 25-09-2023

                rdobtn_Gender.SelectedValue = dtr["SEX"].ToString();//*****************


                rdobtn_marital.SelectedValue = dtr["MARRIED"].ToString();//****************

                ddlBloodGroupNo.SelectedValue = dtr["BLOODGRPNO"] == null ? "0" : dtr["BLOODGRPNO"].ToString();

                ddladmthrough.SelectedValue = dtr["ADMROUNDNO"] == null ? "0" : dtr["ADMROUNDNO"].ToString();   //Added by sachin on 28-07-2022

                ddlNationality.SelectedValue = dtr["NATIONALITYNO"] == null ? "1" : dtr["NATIONALITYNO"].ToString();
                if (Convert.ToInt32(ddlNationality.SelectedValue) == 0)
                {
                    ddlNationality.SelectedValue = "1";
                }

                ddlCaste.SelectedValue = dtr["CASTE"] == null ? "0" : dtr["CASTE"].ToString();

                if (dtr["CATEGORYNO"].ToString() == "0" || dtr["CATEGORYNO"].ToString() == null)
                {
                    objCommon.FillDropDownList(ddlCasteCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
                    ddlCasteCategory.Enabled = true;
                }
                else
                {
                    ddlCasteCategory.SelectedValue = dtr["CATEGORYNO"] == null ? "0" : dtr["CATEGORYNO"].ToString();
                    ////**********ddlCasteCategory.Enabled = false;
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        ddlCasteCategory.Enabled = false;
                    }
                    else if (ViewState["usertype"].ToString() == "1")
                    {
                        ddlCasteCategory.Enabled = true;
                    }
                }
                if (dtr["PAYTYPENO"].ToString() == "0" || dtr["PAYTYPENO"].ToString() == null)
                {
                    objCommon.FillDropDownList(ddlClaimedcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO>0", "CATEGORY");
                    ddlClaimedcategory.Enabled = true;
                }
                else
                {
                    ddlClaimedcategory.SelectedValue = dtr["CATEGORYNO"] == null ? "0" : dtr["CATEGORYNO"].ToString();
                    ////*******ddlAdmcategory.Enabled = false;
                    if (ViewState["usertype"].ToString() == "2" && Session["OrgId"].ToString() == "1")          //Added by sachin on 19-07-2022
                    {
                        ddlClaimedcategory.Enabled = false;
                    }
                    else if (ViewState["usertype"].ToString() == "1")
                    {
                        ddlClaimedcategory.Enabled = true;
                    }

                }
                ddlReligion.SelectedValue = dtr["RELIGIONNO"] == null ? "0" : dtr["RELIGIONNO"].ToString();
                ddlPayType.SelectedValue = dtr["PAYTYPENO"] == null ? "0" : dtr["PAYTYPENO"].ToString();
                txtPassportNo.Text = dtr["PASSPORTNO"] == null ? string.Empty : dtr["PASSPORTNO"].ToString();
                txtCitizenshipNo.Text = dtr["CITIZENSHIP"] == null ? string.Empty : dtr["CITIZENSHIP"].ToString();
                ddlAdmType.SelectedValue = dtr["IDTYPE"] == null ? "0" : dtr["IDTYPE"].ToString();
                //txtPaymentType.Text = dtr["PAYTYPENAME"] == null ? string.Empty : dtr["PAYTYPENAME"].ToString();
                //ddlHandicap.SelectedValue = dtr["TYPE_OF_PHYSICALLY_HANDICAP"] == null ? "0" : dtr["TYPE_OF_PHYSICALLY_HANDICAP"].ToString();
                ddlHandicap.SelectedValue = dtr["PHYSICALLY_HANDICAPPED"] == null ? "0" : dtr["PHYSICALLY_HANDICAPPED"].ToString();
                if (ddlHandicap.SelectedValue == "1")
                {
                    ddlHandicap.SelectedValue = "1";
                }
                else if (ddlHandicap.SelectedValue == "2")
                {
                    ddlHandicap.SelectedValue = "2";
                }
                else
                {
                    ddlHandicap.SelectedValue = "0";
                }
                txtAddharCardNo.Text = dtr["ADDHARCARDNO"] == null ? string.Empty : dtr["ADDHARCARDNO"].ToString();
                txtClgRank.Text = dtr["COLLEGE_RANK_ID"] == null ? string.Empty : dtr["COLLEGE_RANK_ID"].ToString();
                txtFatherDesignation.Text = dtr["FATHER_DESIG"] == null ? string.Empty : dtr["FATHER_DESIG"].ToString();
                ddlOccupationNo.SelectedValue = dtr["OCCUPATIONNO"] == null ? "0" : dtr["OCCUPATIONNO"].ToString();
                ddlMotherOccupation.SelectedValue = dtr["MOTHER_OCCUPATIONNO"] == null ? "0" : dtr["MOTHER_OCCUPATIONNO"].ToString();
                txtMotherDesignation.Text = dtr["MOTHER_DESIG"] == null ? string.Empty : dtr["MOTHER_DESIG"].ToString();
                txtMAnnualIncome.Text = dtr["MOTHER_ANNUAL_INCOME"] == null ? "0" : dtr["MOTHER_ANNUAL_INCOME"].ToString();
                // ddlReligion.Enabled = false;
                if (Convert.ToBoolean(dtr["HOSTELER"]) == true)
                {
                    rdoHosteler.SelectedValue = "1";
                }
                else
                {
                    rdoHosteler.SelectedValue = "0";
                }

                rdbTransport.SelectedValue = dtr["TRANSPORT"].ToString();
                rdoFather.SelectedValue = dtr["FATHER_ALIVE"].ToString();
                rdoMother.SelectedValue = dtr["MOTHER_ALIVE"].ToString();
                rdofatheralive.SelectedValue = dtr["PARENTS_ALIVE"].ToString();
                if (dtr["MOTHER_ALIVE"].ToString() == "0" || dtr["FATHER_ALIVE"].ToString() == "0")
                {
                    rdofatheralive.SelectedValue = "0";
                }
                else
                {
                    rdofatheralive.SelectedValue = "1";

                }
                if (rdofatheralive.SelectedValue == "0")
                {
                    rdoParents.Visible = true;
                }
                else
                {
                    rdoParents.Visible = false;

                }
                if (rdoMother.SelectedValue == "0")
                {
                    MotherSection.Visible = false;
                }
                else
                {
                    MotherSection.Visible = true;

                }
                if (rdoFather.SelectedValue == "0")
                {
                    FatherSection.Visible = false;
                }
                else
                {
                    FatherSection.Visible = true;

                }

                if (Convert.ToString(dtr["COUNTRYDOMICILE"]) == "Yes")
                {
                    rdoInternationalStu.SelectedValue = "1";
                }
                else
                {
                    rdoInternationalStu.SelectedValue = "0";
                }

                txtAlternateNoStud.Text = dtr["STUDENTMOBILE_ALTERNATE"] == null ? string.Empty : dtr["STUDENTMOBILE_ALTERNATE"].ToString();
                txtFatherAlterateNo.Text = dtr["FATHERMOBILE_ALTERNATE"] == null ? string.Empty : dtr["FATHERMOBILE_ALTERNATE"].ToString();
                txtMotherAlternateNo.Text = dtr["MOTHERMOBILE_ALTERNATE"] == null ? string.Empty : dtr["MOTHERMOBILE_ALTERNATE"].ToString();

                txtABCCId.Text = dtr["ABCC_ID"] == null ? string.Empty : dtr["ABCC_ID"].ToString();
                txtDTEAppId.Text = dtr["DTE_APPLICATION_ID"] == null ? string.Empty : dtr["DTE_APPLICATION_ID"].ToString();
                txtUEN.Text = dtr["ELIGIBILITY_NO"] == null ? string.Empty : dtr["ELIGIBILITY_NO"].ToString();

                if (dtr["ENROLLED_IN_ELECTION"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dtr["ENROLLED_IN_ELECTION"]) == 0)
                    {
                        rdoElection.SelectedValue = "N";
                    }
                    else if (Convert.ToInt32(dtr["ENROLLED_IN_ELECTION"]) == 1)
                    {
                        rdoElection.SelectedValue = "Y";
                    }
                }
                else
                {
                    rdoElection.SelectedValue = "N";
                }

                    txtDrivingLicenseNo.Text = dtr["DRIVING_LICENSE_NO"] == null ? string.Empty : dtr["DRIVING_LICENSE_NO"].ToString();
                txtStudPanNo.Text = dtr["STUDENT_PANCARD_NO"] == null ? string.Empty : dtr["STUDENT_PANCARD_NO"].ToString();
                txtFatherPanNo.Text = dtr["FATHER_PANCARD_NO"] == null ? string.Empty : dtr["FATHER_PANCARD_NO"].ToString();
                txtMotherPanNo.Text = dtr["MOTHER_PANCARD_NO"] == null ? string.Empty : dtr["MOTHER_PANCARD_NO"].ToString();

                string idno = objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
                if (idno != "")
                {
                    string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
                    string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
                    if (imgphoto == string.Empty)
                    {
                        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
                        ViewState["StudPhoto"] = 0;
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENT";
                        ViewState["StudPhoto"] = 1;

                    }
                    if (signphoto == string.Empty)
                    {
                        ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
                        ViewState["StudSign"] = 0;
                    }
                    else
                    {
                        ImgSign.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENTSIGN";
                        ViewState["StudSign"] = 1;
                    }
                    // 
                }
                else
                {
                    imgPhoto.ImageUrl = null;
                    ImgSign.ImageUrl = null;
                }


            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string errorString = ValidationAlert();      // Added By Shrikant W. on 04-09-2023

        if (errorString != string.Empty)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "alertmessage('" + errorString + "');", true);
        }
        else
        {
            StudentController objSC = new StudentController();
            Student objS = new Student();
            StudentPhoto objSPhoto = new StudentPhoto();
            StudentAddress objSAddress = new StudentAddress();
            StudentQualExm objSQualExam = new StudentQualExm();
            string IndusEmail = string.Empty;
            string idno = string.Empty;
            int father_alive = 0;
            int mother_alive = 0;
            int parent_alive = 0;
            try
            {               

                if (Convert.ToDateTime(txtDateOfBirth.Text) > DateTime.Now.Date)
                {
                    objCommon.DisplayMessage(this.Page, "Date of Birth Should not be greater than Current Date !", this.Page);
                    return;
                }

                string paramvalue = CommonComponent.Parameters.ParameterValue(CommonComponent.Parameters.ALLOW_PHOTO_SIGN_MANDATORY_ON_STUD_PROFILE);

                if (paramvalue == "1") //onliy for MITA
                {
                    if (!fuPhotoUpload.HasFile && ViewState["StudPhoto"].ToString() == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please upload Photo !", this.Page);
                        return;
                    }
                    else if (!fuSignUpload.HasFile && ViewState["StudSign"].ToString() == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please upload Signature !", this.Page);
                        return;
                    }
                }

                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5" || ViewState["usertype"].ToString() == "8")
                {
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    }
                    else
                    {
                        idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]));
                    }

                    //if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6 || Convert.ToInt32(Session["OrgId"]) == 8)    //Added by sachin on 28-07-2022
                    if (Convert.ToInt32(Session["OrgId"]) == 8)    //Remove orgId not show Admission Through messes
                    {

                        if (ddladmthrough.SelectedIndex == 0 && ViewState["usertype"].ToString() != "2")
                        {
                            objCommon.DisplayMessage(this.Page, "Please select Admission Through!", this.Page);
                            return;
                        }
                    }


                    if (ddlNationality.SelectedIndex > 0 && ddlNationality.SelectedValue == "1" && txtAddharCardNo.Text == string.Empty && txtAddharCardNo.Text == "")
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Aadhar Card No.!!", this.Page);
                        return;
                    }
                    //else if (ddlNationality.SelectedIndex > 0 && ddlNationality.SelectedValue == "2" && txtCitizenshipNo.Text == "" && txtCitizenshipNo.Text == string.Empty)
                    //{
                    //    objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Citizenship No.!!", this.Page);
                    //    return;
                    //}
                    //else if (ddlNationality.SelectedIndex > 0 && ddlNationality.SelectedValue == "10" && txtCitizenshipNo.Text == "" && txtCitizenshipNo.Text == string.Empty)
                    //{
                    //    objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Citizenship No.!!", this.Page);
                    //    return;
                    //}
                    else if (ddlNationality.SelectedIndex > 0 && ddlNationality.SelectedValue != "1" && ddlNationality.SelectedValue != "2" && ddlNationality.SelectedValue != "10" && txtPassportNo.Text == string.Empty && txtPassportNo.Text == "")
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Passport No.!!", this.Page);
                        return;
                    }
                    if (idno != "")
                    {
                        string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(idno));
                        string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(idno));

                        if (imgphoto == string.Empty)
                        {
                            //objCommon.DisplayMessage(this.updpersonalinformation, "Please Upload Photo.", this.Page);
                            //return;
                        }
                        if (signphoto == string.Empty)
                        {
                            //objCommon.DisplayMessage(this.updpersonalinformation, "Please Upload Signature.", this.Page);
                            //return;
                        }

                    }



                    //if (HasValidationErrors())
                    //{
                    //    //objCommon.DisplayMessage(errorString, this.Page);
                    //  //  ScriptManager.RegisterStartupScript(this, this.GetType(), "validationAlert", errorString, true);   
                    //    ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert(" + errorString + ");</script>", false);
                    //    return;
                    //}



                    objS.IdNo = Convert.ToInt32(txtIDNo.Text);
                    objS.EnrollNo = txtEnrollno.Text.Trim();
                    objS.RegNo = txtRegNo.Text.Trim();
                    if (!txtStudFullname.Text.Trim().Equals(string.Empty)) objS.StudName = txtStudFullname.Text.Trim();
                    if (!txtStudentName.Text.Trim().Equals(string.Empty)) objS.firstName = txtStudentName.Text.Trim();
                    if (!txtStudMiddleName.Text.Trim().Equals(string.Empty)) objS.MiddleName = txtStudMiddleName.Text.Trim();
                    if (!txtStudLastName.Text.Trim().Equals(string.Empty)) objS.LastName = txtStudLastName.Text.Trim();
                    if (!txtFatherFullName.Text.Trim().Equals(string.Empty)) objS.FatherName = txtFatherFullName.Text.Trim();// ADDED CHANGES ON 19-05-2023 BY KAJAL JAISWAL FOR SAVING FATHER FULL NAME.
                    if (!txtFatherName.Text.Trim().Equals(string.Empty)) objS.fatherfirstName = txtFatherName.Text.Trim();
                    if (!txtFatherMiddleName.Text.Trim().Equals(string.Empty)) objS.FatherMiddleName = txtFatherMiddleName.Text.Trim();

                    if (!txtFatherLastName.Text.Trim().Equals(string.Empty)) objS.FatherLastName = txtFatherLastName.Text.Trim();
                    if (!txtFatherMobile.Text.Trim().Equals(string.Empty)) objS.FatherMobile = txtFatherMobile.Text.Trim();
                    if (!txtFathersOfficeNo.Text.Trim().Equals(string.Empty)) objS.FatherOfficeNo = txtFathersOfficeNo.Text.Trim();
                    if (!txtFatherDesignation.Text.Trim().Equals(string.Empty)) objSAddress.FATHER_DESIG = txtFatherDesignation.Text.Trim();//Father Qualification
                    objSAddress.OCCUPATION = Convert.ToInt32(ddlOccupationNo.SelectedValue);

                    if (!txtfatheremailid.Text.Trim().Equals(string.Empty)) objS.Fatheremail = txtfatheremailid.Text.Trim();
                    if (!txtMotherName.Text.Trim().Equals(string.Empty)) objS.MotherName = txtMotherName.Text.Trim();
                    string MotherMobile = txtMotherMobile.Text.Trim();
                    if (!txtmotheremailid.Text.Trim().Equals(string.Empty)) objS.Motheremail = txtmotheremailid.Text.Trim();
                    if (!txtMotherDesignation.Text.Trim().Equals(string.Empty)) objSAddress.MOTHERDESIGNATION = txtMotherDesignation.Text.Trim();
                    if (!txtMAnnualIncome.Text.Trim().Equals(string.Empty)) objS.MotherAnnualIncome = Convert.ToInt32(txtMAnnualIncome.Text.Trim());

                    string MotherOfficeNo = txtMothersOfficeNo.Text.Trim();
                    objSAddress.MOTHEROCCUPATION = Convert.ToInt32(ddlMotherOccupation.SelectedValue);
                    objS.Caste = Convert.ToInt32(ddlCaste.SelectedValue);
                    objS.Subcaste = txtSubCaste.Text.Trim();
                    if (!txtDateOfBirth.Text.Trim().Equals(string.Empty)) objS.Dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
                    objS.Annual_income = txtAnnualIncome.Text.Trim();
                    objS.BloodGroupNo = Convert.ToInt32(ddlBloodGroupNo.SelectedValue);

                    objS.AdmroundNo = Convert.ToInt32(ddladmthrough.SelectedValue);   //Added by sachin on 28-07-2022

                    objS.ClaimType = Convert.ToInt32(ddlClaimedcategory.SelectedValue);//for student we are showing claimed category
                    objS.PayTypeNO = Convert.ToInt32(ddlPayType.SelectedValue);
                    objS.ReligionNo = Convert.ToInt32(ddlReligion.SelectedValue);
                    objS.NationalityNo = Convert.ToInt32(ddlNationality.SelectedValue);
                    objS.CategoryNo = Convert.ToInt32(ddlCasteCategory.SelectedValue);
                    if (!txtPassportNo.Text.Trim().Equals(string.Empty)) objS.PassportNo = txtPassportNo.Text.Trim();
                    if (!txtCitizenshipNo.Text.Trim().Equals(string.Empty)) objS.Citizenship = txtCitizenshipNo.Text.Trim();
                    objS.Married = Convert.ToChar(rdobtn_marital.SelectedValue);
                    if (!txtAddharCardNo.Text.Trim().Equals(string.Empty)) objS.AddharcardNo = txtAddharCardNo.Text.Trim();
                    objS.Physical_Handicap = Convert.ToInt32(ddlHandicap.SelectedValue);
                    objS.Sex = Convert.ToChar(rdobtn_Gender.SelectedValue);
                    if (!txtInstituteEmail.Text.Trim().Equals(string.Empty)) IndusEmail = txtInstituteEmail.Text.Trim();

                    if (!txtStudMobile.Text.Trim().Equals(string.Empty)) objS.StudentMobile = txtStudMobile.Text.Trim();
                    if (!txtAlternateNoStud.Text.Trim().Equals(string.Empty)) objS.StudentAlternateMobile = txtAlternateNoStud.Text.Trim(); //Student Alternate Number added by Rishabh - 11/02/2022
                    if (!txtFatherAlterateNo.Text.Trim().Equals(string.Empty)) objS.FatherAlternateMobile = txtFatherAlterateNo.Text.Trim();
                    if (!txtMotherAlternateNo.Text.Trim().Equals(string.Empty)) objS.MotherAlternateMobile = txtMotherAlternateNo.Text.Trim();
                    if (!txtStudentEmail.Text.Trim().Equals(string.Empty)) objS.EmailID = txtStudentEmail.Text.Trim();
                    //if (!txtAlternateEmailId.Text.Trim().Equals(string.Empty)) objS.AlternateEmailID = txtAlternateEmailId.Text.Trim(); //Added By Rishabh on 13/04/2022
                    if (!txtBirthPlace.Text.Trim().Equals(string.Empty)) objS.BirthPlace = txtBirthPlace.Text.Trim();
                    if (!txtCaste.Text.Trim().Equals(string.Empty)) objS.CasteName = txtCaste.Text.Trim();   // Added By Shrikant W. on 25-09-2023
                    objS.Age = "";

                    if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
                    {
                        objS.Uano = Convert.ToInt32(Session["userno"]);
                    }
                    else
                    {
                        objS.Uano = 0;
                    }



                    if (fuPhotoUpload.HasFile)
                    {
                        //objSPhoto.Photo1 = objCommon.GetImageData(fuPhotoUpload);
                        objSPhoto.Photo1 = this.ResizePhoto(fuPhotoUpload);

                    }
                    else
                    {

                        objSPhoto.Photo1 = null;

                    }

                    if (rdoHosteler.SelectedValue == "1")
                    {
                        objS.HostelSts = 1;
                    }
                    else
                    {
                        objS.HostelSts = 0;
                    }
                    if (rdbTransport.SelectedValue == "1")
                    {
                        objS.Transportation = 1;
                    }
                    else
                    {
                        objS.Transportation = 0;
                    }

                    if (rdofatheralive.SelectedValue == "1")
                    {
                        parent_alive = 1;
                        father_alive = 1;
                        mother_alive = 1;
                    }
                    else
                    {
                        if (rdoFather.SelectedValue == "1")
                        {
                            father_alive = 1;
                        }
                        else
                        {
                            father_alive = 0;
                        }
                        if (rdoMother.SelectedValue == "1")
                        {
                            mother_alive = 1;
                        }
                        else
                        {
                            mother_alive = 0;
                        }
                    }

                    if (rdoInternationalStu.SelectedValue == "1") //Added By Rishabh on 13/04/2022
                    {
                        objS.InternationalStu = "Yes";
                    }
                    else
                    {
                        objS.InternationalStu = "No";
                    }
                    if (Convert.ToInt32(Session["OrgId"]) == 8) // For MIT 
                    {
                        if (txtABCCId.Text != string.Empty && txtDTEAppId.Text != string.Empty)
                        {
                            if (ViewState["usertype"].ToString() == "2")
                            {

                                string abccidcount = objCommon.LookUp("ACD_STUDENT", "count(idno)idno", "ABCC_ID='" + txtABCCId.Text + "' AND IDNO <>" + Convert.ToInt32(Session["idno"]) + "");
                                if (abccidcount != string.Empty)
                                {
                                    if (Convert.ToInt32(abccidcount) > 0)
                                    {
                                        objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Another ABCC ID", this.Page);
                                        return;
                                    }
                                }
                                string dteappidcount = objCommon.LookUp("ACD_STUDENT", "count(idno)idno", "DTE_APPLICATION_ID='" + txtDTEAppId.Text + "' AND IDNO<>" + Convert.ToInt32(Session["idno"]) + "");
                                if (dteappidcount != string.Empty)
                                {
                                    if (Convert.ToInt32(dteappidcount) > 0)
                                    {
                                        objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Another DTE Application ID", this.Page);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                string abccidcount = objCommon.LookUp("ACD_STUDENT", "count(idno)idno", "ABCC_ID='" + txtABCCId.Text + "' AND IDNO <>" + Convert.ToInt32(Session["stuinfoidno"]) + "");
                                if (abccidcount != string.Empty)
                                {
                                    if (Convert.ToInt32(abccidcount) > 0)
                                    {
                                        objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Another ABCC Id", this.Page);
                                        return;
                                    }
                                }
                                string dteappidcount = objCommon.LookUp("ACD_STUDENT", "count(idno)idno", "DTE_APPLICATION_ID='" + txtDTEAppId.Text + "' AND IDNO <>" + Convert.ToInt32(Session["stuinfoidno"]) + "");
                                if (dteappidcount != string.Empty)
                                {
                                    if (Convert.ToInt32(dteappidcount) > 0)
                                    {
                                        objCommon.DisplayMessage(this.updpersonalinformation, "Please Enter Another DTE Application Id", this.Page);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    objS.AbccId = txtABCCId.Text;
                    objS.DteAppId = txtDTEAppId.Text;
                    objS.EligibilityNo = txtUEN.Text;

                    if (rdoElection.SelectedValue == "Y")
                    {
                        objS.ElectionEnrolled = 1;
                    }
                    else if (rdoElection.SelectedValue == "N")
                    {
                        objS.ElectionEnrolled = 0;
                    }

                    objS.DrivingLicenseNo = txtDrivingLicenseNo.Text;
                    objS.StudentPanNo = txtStudPanNo.Text;
                    objS.FatherPanNo = txtFatherPanNo.Text;
                    objS.MotherPanNo = txtMotherPanNo.Text;

                    CustomStatus cs = (CustomStatus)objSC.UpdateStudentPersonalInformation(objS, objSAddress, objSPhoto, objSQualExam, MotherMobile, MotherOfficeNo, IndusEmail, Convert.ToInt32(Session["usertype"]), father_alive, mother_alive, parent_alive);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        Response.Redirect("~/academic/AddressDetails.aspx");
                        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Student Personal Information Updated Successfully!!'); location.href='AddressDetails.aspx';", true);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "Error Occured While Updating Personal Information!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);

                }
            }

            catch (Exception ex)
            {
                throw;
                //this.ClearControl();
            }

        }
    }
    protected void btnGohome_Click(object sender, EventArgs e)
    {



        //Response.Redirect("~\\academic\\StudentInfoEntryNew.aspx");
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
    protected void btnPhotoUpload_Click(object sender, EventArgs e)
    {
        try
        {
            // Initialize variables
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            byte[] image = null;
            byte[] imageaftercompress = null;
            string ext = System.IO.Path.GetExtension(fuPhotoUpload.PostedFile.FileName);

            // Check if file is uploaded
            if (fuPhotoUpload.HasFile)
            {
                // Check file extension and size
                if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG" || ext.ToUpper().Trim() == ".GIF")
                {
                    if (fuPhotoUpload.PostedFile.ContentLength < 150000)
                    {
                        using (Stream fs = fuPhotoUpload.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                image = br.ReadBytes((Int32)fs.Length);
                                imageaftercompress = ImageCompression.CompressImage(image, 150);
                            }
                        }

                        // Check compressed image size
                        if (imageaftercompress != null && imageaftercompress.LongLength >= 150000)
                        {
                            objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 150kb", this.Page);
                            return;
                        }

                        // Assign the compressed image to objstud.StudPhoto
                        objstud.StudPhoto = imageaftercompress;
                        objstud.IdNo = Convert.ToInt32(txtIDNo.Text);

                        // Update Student Photo
                        CustomStatus cs = (CustomStatus)objEc.UpdateStudPhoto(objstud);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.updpersonalinformation, "Photo uploaded Successfully!!", this.Page);
                            ViewState["StudPhoto"] = 1;
                            showstudentphoto();
                        }
                        else
                        {
                            ViewState["StudPhoto"] = 0;
                            objCommon.DisplayMessage("Error!!", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 150kb", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "Only JPG, JPEG, PNG files are allowed!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpersonalinformation, "Please select a file!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void showstudentphoto()
    {

        string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
        if (idno != "")
        {
            string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));

            if (imgphoto == string.Empty)
            {
                imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            }
            else
            {
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + txtIDNo.Text.Trim().ToString() + "&type=STUDENT";
            }

        }
        else
        {
            imgPhoto.ImageUrl = null;

        }
    }

    private void showstudentsignature()
    {
        string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));
        if (idno != "")
        {
            string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(txtIDNo.Text.Trim()));

            if (signphoto == string.Empty)
            {

                ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
            }
            else
            {
                ImgSign.ImageUrl = "~/showimage.aspx?id=" + txtIDNo.Text.Trim().ToString() + "&type=STUDENTSIGN";
            }
        }
        else
        {
            ImgSign.ImageUrl = null;
        }
    }

    protected void btnSignUpload_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objEc = new StudentController();
            Student objstud = new Student();
            byte[] image = null;
            byte[] imageaftercompress = null;
            string ext = System.IO.Path.GetExtension(this.fuSignUpload.PostedFile.FileName);

            // string ext = System.IO.Path.GetExtension(this.fuSignUpload.FileName);
            ////if (fuSignUpload.HasFile)
            ////{
            ////    objstud.StudSign = this.ResizePhoto(fuSignUpload);
            ////    objstud.IdNo = Convert.ToInt32(txtIDNo.Text);
            ////}
            if (fuSignUpload.HasFile)
            {
                if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG")
                {
                    //objstud.StudSign = this.ResizePhoto(fuSignUpload);
                    //objstud.IdNo = Convert.ToInt32(txtIDNo.Text);


                    //if (fuSignUpload.PostedFile.ContentLength < 25600)
                    if (fuSignUpload.PostedFile.ContentLength < 150000)
                    {

                        //byte[] resizephoto = ResizePhoto(fuSignUpload);

                        using (Stream fs = fuSignUpload.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                image = br.ReadBytes((Int32)fs.Length);
                                imageaftercompress = ImageCompression.CompressImage(image, 150);
                            }
                        }

                        if (imageaftercompress != null && imageaftercompress.LongLength >= 150000)
                        {
                            objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 150kb", this.Page);
                            return;
                        }

                        objstud.StudSign = imageaftercompress;
                        objstud.IdNo = Convert.ToInt32(txtIDNo.Text);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 150kb", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "Only JPG,JPEG,PNG files are allowed!", this.Page);
                    return;
                }

            }
            else
            {
                //System.IO.FileStream ff = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/logo.gif"), System.IO.FileMode.Open);
                //int ImageSize = (int)ff.Length;
                //byte[] ImageContent = new byte[ff.Length];
                //ff.Read(ImageContent, 0, ImageSize);
                //ff.Close();
                //ff.Dispose();
                //objstud.StudSign = ImageContent;
                objCommon.DisplayMessage(this.updpersonalinformation, "Please select file!", this.Page);
                return;
            }

            CustomStatus cs = (CustomStatus)objEc.UpdateStudSign(objstud);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updpersonalinformation, "Signature uploaded Successfully!!", this.Page);
                ViewState["StudSign"] = 1;
                showstudentsignature();
            }
            else
            {
                ViewState["StudSign"] = 0;
                objCommon.DisplayMessage("Error!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/PersonalDetails.aspx", false);

        Response.Redirect("~/academic/PersonalDetails.aspx");

        // HttpContext.Current.RewritePath("PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AddressDetails.aspx", false);

        Response.Redirect("~/academic/AddressDetails.aspx");
    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AdmissionDetails.aspx", false);
        Response.Redirect("~/academic/AdmissionDetails.aspx");

    }

    protected void lnkDasaStudentInfo_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/DASAStudentInformation.aspx", false);
        Response.Redirect("~/academic/DASAStudentInformation.aspx");
    }

    protected void lnkUploadDocument_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/UploadDocument.aspx");
    }

    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {

        //Server.Transfer("~/academic/QualificationDetails.aspx", false);
        Response.Redirect("~/academic/QualificationDetails.aspx");
    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/OtherInformation.aspx", false);
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
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
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
            url += "pagetitle=Admission Form Report " + txtRegNo.Text;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt16(txtIDNo.Text.Trim().ToString()) + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpersonalinformation, this.updpersonalinformation.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkGoHome_Click(object sender, EventArgs e)
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


    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }
    protected void rdofatheralive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdofatheralive.SelectedValue == "0")
        {
            rdoParents.Visible = true;
            //MotherSection.Visible = false;
            //FatherSection.Visible = false;
            if (rdoFather.SelectedValue == "1")
            {
                FatherSection.Visible = true;
            }
            else
            {
                FatherSection.Visible = false;
            }
            if (rdoMother.SelectedValue == "1")
            {
                MotherSection.Visible = true;
            }
            else
            {
                MotherSection.Visible = false;
            }
        }
        else
        {
            //rdoParents.Visible = false;
            //MotherSection.Visible = true;
            //FatherSection.Visible = true;
            //rdoMother.SelectedValue = "0";
            //rdoFather.SelectedValue = "0";
        }
    }
    protected void rdoFather_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoFather.SelectedValue == "1")
        {
            FatherSection.Visible = true;
        }
        else
        {
            FatherSection.Visible = false;
        }
    }
    protected void rfoMother_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMother.SelectedValue == "1")
        {
            MotherSection.Visible = true;
        }
        else
        {
            MotherSection.Visible = false;
        }
    }
}