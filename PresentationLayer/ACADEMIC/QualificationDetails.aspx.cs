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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Collections.Generic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
/*                                                  
---------------------------------------------------------------------------------------------------------------------------                                                          
Created By :                                                      
Created On :                         
Purpose    :                                     
Version    : 1.0.0                                                
---------------------------------------------------------------------------------------------------------------------------                                                            
Version     Modified On     Modified By            Purpose                                                            
---------------------------------------------------------------------------------------------------------------------------                                                            
1.0.1      28-03-2024      Ashutosh Dhobe        Added  CheckDisplaySection                
------------------------------------------- -------------------------------------------------------------------------------                             
*/

public partial class ACADEMIC_QualificationDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    ModuleConfigController objConfig = new ModuleConfigController();

    List<string> validationErrors = new List<string>();

    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
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
                //  CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];
                StudentConfiguration();
                int orgID = Convert.ToInt32(objCommon.LookUp("REFF", "OrganizationId", ""));

                hdnOrgId.Value = orgID.ToString();
                //if (orgID == 5)
                //{ rfvMarksObtainedHssc.Visible = false;}
                //else
                //{ rfvMarksObtainedHssc.Visible = true;}


                //if (orgID == 12)
                //{
                //    trBiology.Attributes.Add("style", "display:table-row");
                //}
                //else
                //{
                //    trBiology.Attributes.Add("style", "display:none");
                //}


                if (orgID == 6 || orgID == 12)
                {
                    trBiology.Attributes.Add("style", "display:table-row");
                    //trBiology.Attributes.Add("style", "display:block");
                    
                }
                else
                {
                    trBiology.Attributes.Add("style", "display:none");
                }

                if (ViewState["usertype"].ToString() == "2")
                {
                    //btnGohome.Visible = false;
                    divadmissiondetailstreeview.Visible = false;
                    divAdmissionApprove.Visible = false;
                    lvQualExm.DataSource = null;
                    lvQualExm.DataBind();
                    lvEntranceExm.DataSource = null;
                    lvEntranceExm.DataBind();
                    Session["entranceTbl"] = null;
                    Session["qualifyTbl"] = null;
                    FillDropDown();
                    ShowStudentDetails();
                    divhome.Visible = false;
                    

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
                            btnSubmit.Visible = false;
                            btnAdd.Visible = false;
                            btnAddEntranceExam.Visible = false;
                        }
                    }
                    CheckFinalSubmission(); // Added By Bhagyashree on 30052023
                    //if (status.ToString() != "")
                    //{
                    //    if (status == "1")
                    //    {
                    //        btnSubmit.Visible = false;
                    //        btnAdd.Visible = false;
                    //        btnAddEntranceExam.Visible = false;
                    //    }
                    //    else if (status == "2")
                    //    {
                    //        btnSubmit.Visible = true;
                    //        btnAdd.Visible = true;
                    //        btnAddEntranceExam.Visible = true;
                    //    }
                    //}
                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    divhome.Visible = true;
                    // btnGohome.Visible = true ;
                    divadmissiondetailstreeview.Visible = false;
                    divAdmissionApprove.Visible = true;
                    ddlExamNo.Enabled = true;
                    ddlpgentranceno.Enabled = true;
                    txtpgrollno.Enabled = true;
                    txtQExamRollNo.Enabled = true;
                    btnSubmit.Visible = false;
                    btnAdd.Visible = false;
                    lvQualExm.DataSource = null;
                    lvQualExm.DataBind();
                    lvEntranceExm.DataSource = null;
                    lvEntranceExm.DataBind();
                    Session["entranceTbl"] = null;
                    Session["qualifyTbl"] = null;
                    FillDropDown();
                    ShowStudentDetails();
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;

                    if (Convert.ToInt32(Session["stuinfoidno"]) != null)
                    {
                        lvQualExm.DataSource = null;
                        lvQualExm.DataBind();
                        lvEntranceExm.DataSource = null;
                        lvEntranceExm.DataBind();
                        Session["entranceTbl"] = null;
                        Session["qualifyTbl"] = null;
                        ViewState["action"] = "edit";
                        FillDropDown();
                        ShowStudentDetails();
                    }
                }
                else
                {
                    divhome.Visible = true;
                    // btnGohome.Visible = true ;
                    divadmissiondetailstreeview.Visible = true;
                    divAdmissionApprove.Visible = true;
                    ddlExamNo.Enabled = true;
                    ddlpgentranceno.Enabled = true;
                    txtpgrollno.Enabled = true;
                    txtQExamRollNo.Enabled = true;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;

                    if (Convert.ToInt32(Session["stuinfoidno"]) != null)
                    {
                        lvQualExm.DataSource = null;
                        lvQualExm.DataBind();
                        lvEntranceExm.DataSource = null;
                        lvEntranceExm.DataBind();
                        ViewState["action"] = "edit";
                        FillDropDown();
                        ShowStudentDetails();
                    }
                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                    {
                        divNataMarks.Visible = true;
                    }   
                }
               
            }

            //CheckDisplaySection();
        }
        //divStudentLastQualification.Visible=false;
        //SSC_10TH_QUALIFICATION();   
        //HSC_12TH_QUALIFICATION();
    }

    

    

    #region FilterData

    private DataSet FilterDataByKeyword(DataSet originalDataSet, string keyword)
    {
        DataSet filteredDataSet = new DataSet();
        DataTable filteredTable = originalDataSet.Tables[0].Clone();

        foreach (DataRow row in originalDataSet.Tables[0].Rows)
        {
            string captionName = row["CAPTION_NAME"].ToString();

            if (captionName.ToLower().Contains(keyword.ToLower()))
            {
                filteredTable.ImportRow(row);
            }
        }

        filteredDataSet.Tables.Add(filteredTable);
        return filteredDataSet;
    }

    #endregion FilterData

    #region Student Related Configuration

    protected void StudentConfiguration()
    {
        DataSet ds = null;

        int orgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        string pageNo = "";
        string pageName = "QualificationDetails.aspx";
        string section = string.Empty;
        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName); //, section

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
    //<1.0.1>
    //private void CheckDisplaySection()
    //{
    //        DataSet ds = null;
    //        string section = string.Empty;
    //        int orgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
    //        string pageNo = "";
    //        string pageName = "QualificationDetails.aspx";
    //        section = "Entrance Exam Scores";
    //        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName, section);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_DISPLAY_SECTION_NAME"]) == true)
    //            {
    //                divEntranceExamScores.Visible = true;
    //            }
    //            else 
    //            {
    //                divEntranceExamScores.Visible = false;
    //            }
    //        }
    //        section = "Student Last Qualification Details (Only for PG students)";
    //        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName, section);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_DISPLAY_SECTION_NAME"]) == true)
    //            {
    //                upEditQualExm.Visible = true;
    //                trLastQual.Visible = true;

    //            }
    //            else 
    //            {
    //                upEditQualExm.Visible = false;
    //                trLastQual.Visible = false;
    //            }
    //        }
    //        section = "Higher Secondary/12th Marks / Diploma Marks";
    //        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName, section);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_DISPLAY_SECTION_NAME"]) == true)
    //            {
    //                DivHigherEdu.Visible = true;
    //            }
    //            else 
    //            {
    //                DivHigherEdu.Visible = false;
    //            }

    //        }

    //        section = "Secondary/10th Marks";
    //        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName, section);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_DISPLAY_SECTION_NAME"]) == true)
    //            {
    //                divSecMArks.Visible = true;
    //            }
    //            else
    //            {
    //                divSecMArks.Visible = false;
    //            }
    //        }
    //}
    //</1.0.1>
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

    #endregion Student Related Configuration

    #region ValidationAlert

    public string ValidationAlertForDetailsByKeyword(string keyword)
    {
        DataSet ds = null;
        List<string> validationErrors = new List<string>();

        int orgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        string pageNo = "";
        string pageName = "QualificationDetails.aspx";
        string section = string.Empty;

        // Filter data based on the provided keyword
        ds = FilterDataByKeyword(objConfig.GetStudentConfigData(orgID, pageNo, pageName), keyword);  //,section

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
            }
        }

        if (validationErrors.Count > 0)
        {
            string errorMessage = string.Join(",", validationErrors);
            return errorMessage;
        }

        return string.Empty;
    }

    #endregion ValidationAlert

    private void CheckFinalSubmission()
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        DataSet dsallowprocess = objSC.GetAllowProcess(Convert.ToInt32(Session["idno"]), 4, 'E');
        int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        if (finalsubmit == "1" && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
        {
            btnSubmit.Visible = true;
            btnAdd.Visible = true;
            btnAddEntranceExam.Visible = true;
        }
    }

    //private void SSC_10TH_QUALIFICATION()
    //{
    //    string ParamValue = (objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME = 'ALLOW_STUD_INFO_VALIDATE_10TH_QUALIFICATION'"));
    //    if (ParamValue == "1")
    //    {
    //        rfvSchoolName.Enabled = true;
    //        rfvBoardSsc.Enabled = true;
    //        rfvYearOfExamssc.Enabled = true;
    //        rfvOutOfMarksSsc.Enabled = true;

    //    }
    //    else
    //    {
    //        if (ParamValue == "0")
    //        {
    //            rfvSchoolName.Enabled = false;
    //            rfvBoardSsc.Enabled = false;
    //            rfvYearOfExamssc.Enabled = false;
    //            rfvOutOfMarksSsc.Enabled = false;

    //        }
       
        
    //    }
       
    //}

    //private void HSC_12TH_QUALIFICATION()
    //{
    //    string ParamValue = (objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME = 'ALLOW_STUD_INFO_VALIDATED_12TH_QUALIFICATION'"));
    //    if (ParamValue == "1")
    //    {
    //        rfvCollageName.Enabled = true;
    //        rfvBoard.Enabled = true;
    //        rfvExamYear.Enabled = true;
    //        rfvMarksObtainedHssc.Enabled = true;
    //    }
    //    else
    //    {
    //        if (ParamValue == "0")
    //        {
    //            rfvCollageName.Enabled = false;
    //            rfvBoard.Enabled = false;
    //            rfvExamYear.Enabled = false;
    //            rfvMarksObtainedHssc.Enabled = false;
    //        }

        
    //    }

    //}



    private void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddldegree, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='Q'", "QUALIEXMNAME");
            //fill dropdown adm quota
            objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");
            //objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS='Q'", "QUALIEXMNAME");
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIEXMNAME");
            objCommon.FillDropDownList(ddlpgentranceno, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIEXMNAME");
            //objCommon.FillDropDownList(ddlBoardSsc, "ACD_BOARD", "BOARDNO", "BOARD", "BOARDNO>0", "BOARD");
            //objCommon.FillDropDownList(ddlBoardHssc, "ACD_BOARD", "BOARDNO", "BOARD", "BOARDNO>0", "BOARD");
            //objCommon.FillDropDownList(ddlBoardDiploma, "ACD_BOARD", "BOARDNO", "BOARD", "BOARDNO>0", "BOARD");
            //objCommon.FillDropDownList(ddlBoardQualifying, "ACD_BOARD", "BOARDNO", "BOARD", "BOARDNO>0", "BOARD");
            //ddlBoardSsc.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string errorStringSSC = ValidationAlertForDetailsByKeyword("ssc");

        string combinedErrors = string.Empty;

        if (!string.IsNullOrEmpty(errorStringSSC))
        {
            combinedErrors += errorStringSSC;
        }

        string keywordToValidate = string.Empty;

        if (rdoHsc.Checked)
        {
            keywordToValidate = "hsc";
        }
        else if (rdoDiploma.Checked)
        {
            keywordToValidate = "diploma";
        }

        if (!string.IsNullOrEmpty(keywordToValidate))
        {
            string errorString = ValidationAlertForDetailsByKeyword(keywordToValidate);

            if (!string.IsNullOrEmpty(errorString))
            {
                if (!string.IsNullOrEmpty(combinedErrors))
                {
                    combinedErrors += ","; 
                }
                combinedErrors += errorString;
            }
        }

        if (!string.IsNullOrEmpty(combinedErrors))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "alertmessage('" + combinedErrors + "');", true);
            return;
        }
        else
        {

            StudentController objSC = new StudentController();
            Student objS = new Student();
            StudentPhoto objSPhoto = new StudentPhoto();
            StudentAddress objSAddress = new StudentAddress();
            StudentQualExm objSQualExam = new StudentQualExm();

            try
            {
                //if (Convert.ToDecimal(txtOutOfMarksSsc.Text) < Convert.ToDecimal(txtMarksObtainedSsc.Text))     //Added by sachin on 29-07-2022
                //{
                //    objCommon.DisplayMessage(this, "  Marks/GPA Obtained Should not be greater than Out Of Marks/GPA!!", this.Page);
                //    return;
                //    return;
                //}
                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5" || ViewState["usertype"].ToString() == "8")
                {

                    if (ViewState["usertype"].ToString() == "2")
                    {
                        objS.IdNo = Convert.ToInt32(Session["idno"]);
                    }
                    else
                    {
                        objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                    }
                    //SSC 
                    if (!txtSchoolCollegeNameSsc.Text.Trim().Equals(string.Empty)) objSQualExam.SchoolCollegeNameSsc = txtSchoolCollegeNameSsc.Text.Trim();
                    if (!txtBoardSsc.Text.Trim().Equals(string.Empty)) objSQualExam.BoardSsc = txtBoardSsc.Text.Trim();

                    //if (ddlBoardSsc.SelectedIndex == 0)
                    //{
                    //    objSQualExam.BoardSsc = "";
                    //}
                    //else
                    //{
                    //    objSQualExam.BoardSsc = ddlBoardSsc.SelectedItem.Text;
                    //}
                    if (!txtYearOfExamSsc.Text.Trim().Equals(string.Empty)) objSQualExam.YearOfExamSsc = txtYearOfExamSsc.Text.Trim();
                    if (!txtSSCMedium.Text.Trim().Equals(string.Empty)) objSQualExam.SSC_medium = txtSSCMedium.Text.Trim();
                    if (!txtMarksObtainedSsc.Text.Trim().Equals(string.Empty)) objSQualExam.MarksObtainedSsc = Convert.ToDecimal(txtMarksObtainedSsc.Text.Trim());
                    if (!txtOutOfMarksSsc.Text.Trim().Equals(string.Empty)) objSQualExam.OutOfMarksSsc = Convert.ToInt32(txtOutOfMarksSsc.Text.Trim());
                    if (!txtPercentageSsc.Text.Trim().Equals(string.Empty)) objSQualExam.PercentageSsc = Convert.ToDecimal(txtPercentageSsc.Text.Trim());
                    if (!txtExamRollNoSsc.Text.Trim().Equals(string.Empty)) objSQualExam.QEXMROLLNOSSC = txtExamRollNoSsc.Text.Trim();
                    if (!txtPercentileSsc.Text.Trim().Equals(string.Empty)) objSQualExam.PercentileSsc = Convert.ToDecimal(txtPercentileSsc.Text.Trim());
                    if (!txtGradeSsc.Text.Trim().Equals(string.Empty)) objSQualExam.GradeSsc = txtGradeSsc.Text.Trim();
                    if (!txtAttemptSsc.Text.Trim().Equals(string.Empty)) objSQualExam.AttemptSsc = txtAttemptSsc.Text.Trim();
                    if (!txtSSCSchoolColgAdd.Text.Trim().Equals(string.Empty)) objSQualExam.colg_address_SSC = txtSSCSchoolColgAdd.Text.Trim();
                    objSQualExam.DivisionSsc = Convert.ToInt32(ddlDivisionSsc.SelectedValue);
                    if (!txtMarksheetNoSsc.Text.Trim().Equals(string.Empty)) objSQualExam.MarksheetNoSsc = txtMarksheetNoSsc.Text.Trim();

                    //HSSC
                    if (!txtSchoolCollegeNameHssc.Text.Trim().Equals(string.Empty)) objSQualExam.SCHOOL_COLLEGE_NAME = txtSchoolCollegeNameHssc.Text.Trim();
                    if (!txtBoardHssc.Text.Trim().Equals(string.Empty)) objSQualExam.BOARD = txtBoardHssc.Text.Trim();

                    //if (ddlBoardHssc.SelectedIndex == 0)
                    //{
                    //    objSQualExam.BOARD = "";
                    //}
                    //else
                    //{
                    //    objSQualExam.BOARD = ddlBoardHssc.SelectedItem.Text;
                    //}
                    if (!txtYearOfExamHssc.Text.Trim().Equals(string.Empty)) objSQualExam.YEAR_OF_EXAMHSSC = txtYearOfExamHssc.Text.Trim();
                    if (!txtHSSCMedium.Text.Trim().Equals(string.Empty)) objSQualExam.HSSC_medium = txtHSSCMedium.Text.Trim();
                    txtMarksObtainedHssc.Enabled = true;

                    if (!txtMarksObtainedHssc.Text.Trim().Equals(string.Empty)) objSQualExam.MARKOBTAINED = Convert.ToDecimal(txtMarksObtainedHssc.Text.Trim());

                    txtMarksObtainedHssc.Enabled = false;
                    //if (txtVocationalmarktotal.Text == "0" || txtVocationalmarktotal.Text == string.Empty)
                    //    objSQualExam.OUTOFMARK = 300;
                    //else
                    //{
                    //    if (!txtOutOfMarksHssc.Text.Trim().Equals(string.Empty)) objSQualExam.OUTOFMARK = Convert.ToInt32(txtOutOfMarksHssc.Text.Trim());
                    //}

                    if (!txtOutOfMarksHssc.Text.Trim().Equals(string.Empty)) objSQualExam.OUTOFMARK = Convert.ToInt32(txtOutOfMarksHssc.Text.Trim());
                    if (!txtPercentageHssc.Text.Trim().Equals(string.Empty)) objSQualExam.PERCENTAGE = Convert.ToDecimal(txtPercentageHssc.Text.Trim());
                    if (!txtExamRollNoHssc.Text.Trim().Equals(string.Empty)) objSQualExam.QEXMROLLNOHSSC = txtExamRollNoHssc.Text.Trim();
                    if (!txtPercentileHssc.Text.Trim().Equals(string.Empty)) objSQualExam.PercentileHSsc = Convert.ToDecimal(txtPercentileHssc.Text.Trim());
                    if (!txtGradeHssc.Text.Trim().Equals(string.Empty)) objSQualExam.GRADE = txtGradeHssc.Text.Trim();
                    if (!txtAttemptHssc.Text.Trim().Equals(string.Empty)) objSQualExam.ATTEMPT = txtAttemptHssc.Text.Trim();
                    if (!txtHSCColgAddress.Text.Trim().Equals(string.Empty)) objSQualExam.colg_address_HSSC = txtHSCColgAddress.Text.Trim();
                    objSQualExam.DivisionHsc = Convert.ToInt32(ddlDivisionHsc.SelectedValue);
                    if (!txtMarksheetNoHsc.Text.Trim().Equals(string.Empty)) objSQualExam.MarksheetNoHsc = txtMarksheetNoHsc.Text.Trim();
                    
                    //Subject Wise Marks

                    //if (!txtHscChe.Text.Trim().Equals(string.Empty)) objSQualExam.HSCCHE = Convert.ToInt32(txtHscChe.Text.Trim());
                    //if (!txtHscCheMax.Text.Trim().Equals(string.Empty)) objSQualExam.HSCCHEMAX1 = Convert.ToInt32(txtHscCheMax.Text.Trim());
                    ////if (!txtHscPhy.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPHY1 = Convert.ToInt32(txtHscPhy.Text.Trim());
                    //if (!txtHscPhyMax.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPHYMAX1 = Convert.ToInt32(txtHscPhyMax.Text.Trim());
                    //if (!txtHscEngHssc.Text.Trim().Equals(string.Empty)) objSQualExam.ENG = Convert.ToInt32(txtHscEngHssc.Text.Trim());
                    //if (!txtHscEngMaxHssc.Text.Trim().Equals(string.Empty)) objSQualExam.HSCENGMAX = Convert.ToInt32(txtHscEngMaxHssc.Text.Trim());
                    //if (!txtHscMaths.Text.Trim().Equals(string.Empty)) objSQualExam.MATHS = Convert.ToInt32(txtHscMaths.Text.Trim());
                    //if (!txtHscMathsMax.Text.Trim().Equals(string.Empty)) objSQualExam.MATHSMAX = Convert.ToInt32(txtHscMathsMax.Text.Trim());

                    if (!txtchem.Text.Trim().Equals(string.Empty)) objSQualExam.HSCCHE = Convert.ToInt32(txtchem.Text.Trim());
                    if (!txtchemtot.Text.Trim().Equals(string.Empty)) objSQualExam.HSCCHEMAX1 = Convert.ToInt32(txtchemtot.Text.Trim());
                    if (!txtphymark.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPHY1 = Convert.ToInt32(txtphymark.Text.Trim());
                    if (!txtphymarktotal.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPHYMAX1 = Convert.ToInt32(txtphymarktotal.Text.Trim());
                    if (!txtVocationalmark.Text.Trim().Equals(string.Empty)) objSQualExam.ENG = Convert.ToInt32(txtVocationalmark.Text.Trim());
                    if (!txtVocationalmarktotal.Text.Trim().Equals(string.Empty)) objSQualExam.HSCENGMAX = Convert.ToInt32(txtVocationalmarktotal.Text.Trim());
                    if (!txtmaths.Text.Trim().Equals(string.Empty)) objSQualExam.MATHS = Convert.ToInt32(txtmaths.Text.Trim());
                    if (!txtmathstot.Text.Trim().Equals(string.Empty)) objSQualExam.MATHSMAX = Convert.ToInt32(txtmathstot.Text.Trim());
                    if (!txtbiology.Text.Trim().Equals(string.Empty)) objSQualExam.HSCBIO = Convert.ToInt32(txtbiology.Text.Trim());
                    if (!txtbiologytot.Text.Trim().Equals(string.Empty)) objSQualExam.HSCBIOMAX = Convert.ToInt32(txtbiologytot.Text.Trim());

                    //if (!txtPcmMarks.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPCM = Convert.ToInt32(txtPcmMarks.Text.Trim());



                    //if (!txtPcmPerct.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPCMPercentage = Convert.ToDecimal(txtPcmPerct.Text.Trim());
                    // if (!txtPcmPerct.Text.Trim().Equals(string.Empty)) objSQualExam.HSCPCMPercentage = Convert.ToDecimal(txtPcmPerct.Text.Trim());
                    if (!hfdPcmMarks.Value.Trim().Equals(string.Empty))
                    {
                        objSQualExam.HSCPCM = Convert.ToInt32(hfdPcmMarks.Value.Trim()); // Added by hfdPcmPer sachin on 20-07-2022
                    }
                    if (!hfdPcmPer.Value.Trim().Equals(string.Empty)) objSQualExam.HSCPCMPercentage = Convert.ToDecimal(hfdPcmPer.Value.Trim()); // Added by hfdPcmPer sachin on  20-07-2022



                    string Vocationalsub = string.Empty;
                    Vocationalsub = txtVocsub.Text.Trim();



                    objSQualExam.QUALIFYNO = Convert.ToInt32(ddlExamNo.SelectedValue);
                    if (!txtQExamRollNo.Text.Trim().Equals(string.Empty)) objS.QexmRollNo = txtQExamRollNo.Text.Trim();
                    if (!txtYearOfExam.Text.Trim().Equals(string.Empty)) objS.YearOfExam = txtYearOfExam.Text.Trim();
                    if (!txtPer.Text.Trim().Equals(string.Empty)) objS.Percentage = Convert.ToDecimal(txtPer.Text.Trim());
                    if (!txtPercentile.Text.Trim().Equals(string.Empty)) objSQualExam.PERCENTILE = Convert.ToDecimal(txtPercentile.Text.Trim());
                    if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) objSQualExam.ALLINDIARANK = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
                    if (!txtScore.Text.Trim().Equals(string.Empty)) objS.Score = Convert.ToDecimal(txtScore.Text.Trim());

                    int diplomastatus = 0;
                    if (rdoDiploma.Checked)
                    {
                        diplomastatus = 1;
                    }
                    else
                    {
                        diplomastatus = 0;
                    }
                    //DIPLOMA
                    if (!txtSchoolCollegeNameDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.SchoolCollegeNameDiploma = txtSchoolCollegeNameDiploma.Text.Trim();
                    if (!txtBoardDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.BoardDiploma = txtBoardDiploma.Text.Trim();

                    //if (ddlBoardDiploma.SelectedIndex == 0)
                    //{
                    //    objSQualExam.BoardDiploma = "";
                    //}
                    //else
                    //{
                    //    objSQualExam.BoardDiploma = ddlBoardDiploma.SelectedItem.Text;
                    //}
                    if (!txtYearOfExamDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.YearOfExamDiploma = txtYearOfExamDiploma.Text.Trim();
                    if (!txtDiplomaMedium.Text.Trim().Equals(string.Empty)) objSQualExam.Diploma_medium = txtDiplomaMedium.Text.Trim();
                    if (!txtMarksObtainedDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.MarksObtainedDiploma = Convert.ToDecimal(txtMarksObtainedDiploma.Text.Trim());
                    if (!txtOutOfMarksDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.OutOfMarksDiploma = Convert.ToInt32(txtOutOfMarksDiploma.Text.Trim());
                    if (!txtPercentageDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.PercentageDiploma = Convert.ToDecimal(txtPercentageDiploma.Text.Trim());
                    if (!txtExamRollNoDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.QEXMROLLNODiploma = txtExamRollNoDiploma.Text.Trim();
                    if (!txtPercentileDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.PercentileDiploma = Convert.ToDecimal(txtPercentileDiploma.Text.Trim());
                    if (!txtGradeDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.GradeDiploma = txtGradeDiploma.Text.Trim();
                    if (!txtAttemptDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.AttemptDiploma = txtAttemptDiploma.Text.Trim();
                    if (!txtDiplomaColgAddress.Text.Trim().Equals(string.Empty)) objSQualExam.colg_address_Diploma = txtDiplomaColgAddress.Text.Trim();
                    objSQualExam.DivisionDiploma = Convert.ToInt32(ddlDivisionDiploma.SelectedValue);
                    if (!txtMarksheetNoDiploma.Text.Trim().Equals(string.Empty)) objSQualExam.MarksheetNoDiploma = txtMarksheetNoDiploma.Text.Trim();
                    
                    objS.PGQUALIFYNO = Convert.ToInt32(ddlpgentranceno.SelectedValue);
                    if (!txtpgrollno.Text.Trim().Equals(string.Empty)) objS.PGENTROLLNO = txtpgrollno.Text.Trim();
                    if (!txtpgexamyear.Text.Trim().Equals(string.Empty)) objS.pgyearOfExam = txtpgexamyear.Text.Trim();
                    if (!txtpgpercentage.Text.Trim().Equals(string.Empty)) objS.pgpercentage = Convert.ToDecimal(txtpgpercentage.Text.Trim());
                    if (!txtpgpercentile.Text.Trim().Equals(string.Empty)) objS.pgpercentile = Convert.ToDecimal(txtpgpercentile.Text.Trim());
                    if (!txtpgrank.Text.Trim().Equals(string.Empty)) objS.PGRANK = Convert.ToInt32(txtpgrank.Text.Trim());
                    if (!txtpgscore.Text.Trim().Equals(string.Empty)) objS.pgscore = Convert.ToDecimal(txtpgscore.Text.Trim());

                    if (!txtNataMarks.Text.Trim().Equals(string.Empty)) objS.NataMarks = Convert.ToDecimal(txtNataMarks.Text.Trim());


                    QualifiedExam[] qualExams = null;
                    this.BindLastQualifiedExamData(ref qualExams);
                    objS.LastQualifiedExams = qualExams;

                    QualifiedExam[] EntranceExams = null;
                    this.BindEntranceExamData(ref EntranceExams);
                    objS.EntranceExams = EntranceExams;
                    int uano = Convert.ToInt32(Session["userno"]); // Added By Kajal J. on 20032024 for maintaining log
                    CustomStatus cs = (CustomStatus)objSC.UpdateStudentQualifyingExamInformation(objS, objSQualExam, Convert.ToInt32(Session["usertype"]), Vocationalsub, diplomastatus, uano);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ShowStudentDetails();
                        Session["entranceTbl"] = null;
                        //objCommon.DisplayMessage(this,"Qualification Details Updated Successfully!!", this.Page);

                        // divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Qualification Details Updated Successfully!!'); </script>";

                        //string strScript = "<SCRIPT language='javascript'>window.location='OtherInformation.aspx';</SCRIPT>";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "strScript", strScript);

                        Response.Redirect("~/academic/CovidVaccinationDetails.aspx", false);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Qualification Details Updated Successfully!!'); location.href='CovidVaccinationDetails.aspx';", true);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Error Occured While Updating Qualification Details!!", this.Page);
                    }


                }
            }
            catch (Exception Ex)
            {
                throw;
            }


            this.fillDataTable();
            Session["qualifyTbl"] = null;
            this.fillEntranceDataTable();
            Session["entranceTbl"] = null;

        }
    }

    protected void btnGohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/StudentInfoEntryNew.aspx?pageno=2219");
    }
    protected void fillDataTable()
    {
        lvQualExm.DataSource = Session["qualifyTbl"]; ;
        lvQualExm.DataBind();
        ClearControls_QualDetails();

    }
    protected void fillEntranceDataTable()
    {
        lvEntranceExm.DataSource = Session["entranceTbl"]; ;
        lvEntranceExm.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvEntranceExm);//Set label 
        ClearControls_Entrance();

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string errorString = ValidationAlertForDetailsByKeyword("last qualification");

        if (errorString != string.Empty)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ErrorMessage('" + errorString + "')", true);
        }
        else
        {

            ////if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)//***************
            if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]).Rows.Count > 0 && ((DataTable)Session["qualifyTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["qualifyTbl"];
                //DataTable dt = new DataTable();
                DataRow dr = dt.NewRow();
                //DataRow [] dr1;
                if (btnAdd.Text != "Update")
                {
                    string expression = string.Empty;
                    expression = "QUALIFYNAME='" + ddldegree.SelectedItem.Text + "'";
                    DataRow[] dr1 = dt.Select(expression);
                    //dr1 = dt.Rows.Find(expression);
                    if (dr1.Length > 0)
                    {
                        lvQualExm.DataSource = dt;
                        lvQualExm.DataBind();
                        ClearControls_QualDetails();
                        objCommon.DisplayMessage(this, "Data for selected exam already exist!", this.Page);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022
                        return;
                    }
                }
                ////dr["QUALIFYNO"] = Convert.ToInt32(ddlQualifyingNo.SelectedValue);
                ////dr["QUALIFYNAME"] = ddlQualifyingNo.SelectedItem.Text;
                ////dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim() == null ? string.Empty : txtSchoolCollegeNameQualifying.Text.Trim();
                ////dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim() == "" ? string.Empty : txtYearOfExamQualifying.Text.Trim();
                ////dr["MARKS_OBTAINED"] = txtMarksObtainedQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtMarksObtainedQualifying.Text.Trim());
                ////dr["OUT_OF_MRKS"] = txtOutOfMarksQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtOutOfMarksQualifying.Text.Trim());
                ////dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                ////dr["PERCENTILE"] = txtPercentileQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentileQualifying.Text.Trim());
                ////dr["ATTEMPT"] = txtAttemptQualifying.Text.Trim();
                ////dr["BOARD"] = txtBoardQualifying.Text.Trim();
                ////dr["GRADE"] = txtGradeQualifying.Text.Trim();
                ////dr["RES_TOPIC"] = txtResearchTopic.Text.Trim();
                ////dr["SUPERVISOR_NAME1"] = txtSupervisorName1.Text.Trim();
                ////dr["SUPERVISOR_NAME2"] = txtSupervisorName2.Text.Trim();

                ////dr["COLLEGE_ADDRESS"] = txtQualExmAddress.Text.Trim();
                ////dr["QUAL_MEDIUM"] = txtQualiMedium.Text.Trim();
                ////dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();
                ////dt.Rows.Add(dr);
                ////Session["qualifyTbl"] = dt;
                ////lvQualExm.DataSource = dt;
                ////lvQualExm.DataBind();
                ////ClearControls_QualDetails();
                ////objCommon.DisplayMessage(updEdit,"Data saved successfully!", this.Page);

                if (ddldegree.SelectedIndex > 0 && (txtSchoolCollegeNameQualifying.Text != string.Empty || txtSchoolCollegeNameQualifying.Text != "") && (txtYearOfExamQualifying.Text != string.Empty || txtYearOfExamQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtQualExamRollNo.Text != string.Empty || txtQualExamRollNo.Text != "") && (txtBoardQualifying.Text != string.Empty || txtBoardQualifying.Text != ""))
                {
                    dr["QUALIFYNO"] = Convert.ToInt32(ddldegree.SelectedValue);
                    dr["QUALIFYNAME"] = ddldegree.SelectedItem.Text;
                    dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim() == null ? string.Empty : txtSchoolCollegeNameQualifying.Text.Trim();
                    dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim() == "" ? string.Empty : txtYearOfExamQualifying.Text.Trim();
                    dr["MARKS_OBTAINED"] = txtMarksObtainedQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtMarksObtainedQualifying.Text.Trim());
                    dr["OUT_OF_MRKS"] = txtOutOfMarksQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtOutOfMarksQualifying.Text.Trim());
                    dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                    dr["PERCENTILE"] = txtPercentileQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentileQualifying.Text.Trim());
                    dr["ATTEMPT"] = txtAttemptQualifying.Text.Trim();
                    dr["BOARD"] = txtBoardQualifying.Text.Trim();
                    dr["GRADE"] = txtGradeQualifying.Text.Trim();
                    dr["RES_TOPIC"] = txtResearchTopic.Text.Trim();
                    dr["SUPERVISOR_NAME1"] = txtSupervisorName1.Text.Trim();
                    dr["SUPERVISOR_NAME2"] = txtSupervisorName2.Text.Trim();

                    dr["COLLEGE_ADDRESS"] = txtQualExmAddress.Text.Trim();
                    dr["QUAL_MEDIUM"] = txtQualiMedium.Text.Trim();
                    dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();
                    dt.Rows.Add(dr);
                    Session["qualifyTbl"] = dt;
                    lvQualExm.DataSource = dt;
                    lvQualExm.DataBind();
                    ClearControls_QualDetails();
                    objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022

                }
                else
                {
                    //objCommon.DisplayMessage(this, " Please enter all below details \\n \\n School/College Name \\n Board Name \\n Qualifying Exam Name \\n Exam Roll No. \\n Year of Exam \\n Percentage", this.Page);
                    objCommon.DisplayMessage(this, "Please Enter all Student Last Qualification Details", this.Page);
                }
            }
            else
            {
                DataTable dt = this.GetDataTable();
                DataRow dr = dt.NewRow();
                ////dr["QUALIFYNO"] = Convert.ToInt32(ddlQualifyingNo.SelectedValue);
                ////dr["QUALIFYNAME"] = Convert.ToString(ddlQualifyingNo.SelectedItem.Text);
                ////dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim();
                ////dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim();
                ////dr["MARKS_OBTAINED"] = txtMarksObtainedQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtMarksObtainedQualifying.Text.Trim());
                ////dr["OUT_OF_MRKS"] = txtOutOfMarksQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtOutOfMarksQualifying.Text.Trim());
                ////dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                ////dr["PERCENTILE"] = txtPercentileQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentileQualifying.Text.Trim());
                ////dr["ATTEMPT"] = txtAttemptQualifying.Text.Trim();
                ////dr["BOARD"] = txtBoardQualifying.Text.Trim();
                ////dr["GRADE"] = txtGradeQualifying.Text.Trim();
                ////dr["RES_TOPIC"] = txtResearchTopic.Text.Trim();
                ////dr["SUPERVISOR_NAME1"] = txtSupervisorName1.Text.Trim();
                ////dr["SUPERVISOR_NAME2"] = txtSupervisorName2.Text.Trim();
                ////dr["COLLEGE_ADDRESS"] = txtQualExmAddress.Text.Trim();
                ////dr["QUAL_MEDIUM"] = txtQualiMedium.Text.Trim();
                ////dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();

                ////bool flag = false;//***************
                ////if (ddlQualifyingNo.SelectedIndex > 0)
                ////{
                ////    dr["QUALIFYNO"] = Convert.ToInt32(ddlQualifyingNo.SelectedValue);
                ////    dr["QUALIFYNAME"] = Convert.ToString(ddlQualifyingNo.SelectedItem.Text);
                ////    flag = true;
                ////}
                ////if (txtSchoolCollegeNameQualifying.Text != string.Empty || txtSchoolCollegeNameQualifying.Text != "")
                ////{
                ////    dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim();
                ////    flag = true;
                ////}
                ////if (txtYearOfExamQualifying.Text != string.Empty || txtYearOfExamQualifying.Text != "")
                ////{
                ////    dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim();
                ////    flag = true;
                ////}

                ////if (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "")
                ////{
                ////    dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                ////    flag = true;
                ////}
                ////if (txtQualExamRollNo.Text != string.Empty || txtQualExamRollNo.Text != "")
                ////{
                ////    dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();
                ////    flag = true;
                ////}

                if (ddldegree.SelectedIndex > 0 && (txtSchoolCollegeNameQualifying.Text != string.Empty || txtSchoolCollegeNameQualifying.Text != "") && (txtYearOfExamQualifying.Text != string.Empty || txtYearOfExamQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtPercentageQualifying.Text != string.Empty || txtPercentageQualifying.Text != "") && (txtQualExamRollNo.Text != string.Empty || txtQualExamRollNo.Text != "") && (txtBoardQualifying.Text != string.Empty || txtBoardQualifying.Text != ""))
                {
                    dr["QUALIFYNO"] = Convert.ToInt32(ddldegree.SelectedValue);
                    dr["QUALIFYNAME"] = Convert.ToString(ddldegree.SelectedItem.Text);
                    dr["SCHOOL_COLLEGE_NAME"] = txtSchoolCollegeNameQualifying.Text.Trim();
                    dr["YEAR_OF_EXAMHSSC"] = txtYearOfExamQualifying.Text.Trim();
                    dr["MARKS_OBTAINED"] = txtMarksObtainedQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtMarksObtainedQualifying.Text.Trim());
                    dr["OUT_OF_MRKS"] = txtOutOfMarksQualifying.Text.Trim() == "" ? 0 : Convert.ToInt32(txtOutOfMarksQualifying.Text.Trim());
                    dr["PER"] = txtPercentageQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentageQualifying.Text.Trim());
                    dr["PERCENTILE"] = txtPercentileQualifying.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentileQualifying.Text.Trim());
                    dr["ATTEMPT"] = txtAttemptQualifying.Text.Trim();
                    dr["BOARD"] = txtBoardQualifying.Text.Trim();
                    dr["GRADE"] = txtGradeQualifying.Text.Trim();
                    dr["RES_TOPIC"] = txtResearchTopic.Text.Trim();
                    dr["SUPERVISOR_NAME1"] = txtSupervisorName1.Text.Trim();
                    dr["SUPERVISOR_NAME2"] = txtSupervisorName2.Text.Trim();
                    dr["COLLEGE_ADDRESS"] = txtQualExmAddress.Text.Trim();
                    dr["QUAL_MEDIUM"] = txtQualiMedium.Text.Trim();
                    dr["QEXMROLLNO"] = txtQualExamRollNo.Text.Trim();

                    dt.Rows.Add(dr);
                    Session["qualifyTbl"] = dt;
                    lvQualExm.DataSource = dt;
                    lvQualExm.DataBind();
                    ClearControls_QualDetails();
                    objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022

                }
                else
                {
                    //objCommon.DisplayMessage(this, " Please enter all below details \\n \\n School/College Name \\n Board Name \\n Qualifying Exam Name \\n Exam Roll No. \\n Year of Exam \\n Percentage", this.Page);
                    objCommon.DisplayMessage(this, "Please Enter all Student Last Qualification Details", this.Page);
                }


            }

            btnAdd.Text = "Add";
        }

    }
    private DataTable GetDataTable()
    {
        DataTable objQualify = new DataTable();
        objQualify.Columns.Add(new DataColumn("QUALIFYNO", typeof(int)));
        objQualify.Columns.Add(new DataColumn("QUALIFYNAME", typeof(string)));
        objQualify.Columns.Add(new DataColumn("SCHOOL_COLLEGE_NAME", typeof(string)));
        objQualify.Columns.Add(new DataColumn("YEAR_OF_EXAMHSSC", typeof(string)));
        objQualify.Columns.Add(new DataColumn("MARKS_OBTAINED", typeof(int)));
        objQualify.Columns.Add(new DataColumn("OUT_OF_MRKS", typeof(int)));
        objQualify.Columns.Add(new DataColumn("PER", typeof(decimal)));
        objQualify.Columns.Add(new DataColumn("PERCENTILE", typeof(decimal)));
        objQualify.Columns.Add(new DataColumn("ATTEMPT", typeof(string)));
        objQualify.Columns.Add(new DataColumn("BOARD", typeof(string)));
        objQualify.Columns.Add(new DataColumn("GRADE", typeof(string)));
        objQualify.Columns.Add(new DataColumn("RES_TOPIC", typeof(string)));
        objQualify.Columns.Add(new DataColumn("SUPERVISOR_NAME1", typeof(string)));
        objQualify.Columns.Add(new DataColumn("SUPERVISOR_NAME2", typeof(string)));
        objQualify.Columns.Add(new DataColumn("COLLEGE_ADDRESS", typeof(string)));
        objQualify.Columns.Add(new DataColumn("QUAL_MEDIUM", typeof(string)));
        objQualify.Columns.Add(new DataColumn("QEXMROLLNO", typeof(string)));
        return objQualify;
    }

    private DataTable GetEntranceDataTable()
    {
        DataTable objEntrance = new DataTable();
        objEntrance.Columns.Add(new DataColumn("QUALIFYNO", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("QUALIFYNAME", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("YEAR_OF_EXAM", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("PER", typeof(decimal)));
        objEntrance.Columns.Add(new DataColumn("CGPA", typeof(decimal)));
        objEntrance.Columns.Add(new DataColumn("ALL_INDIA_RANK", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("SCORE", typeof(decimal)));
        objEntrance.Columns.Add(new DataColumn("EXMROLLNO", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("LAST_SCHOOL_NAME", typeof(string)));

        return objEntrance;
    }

    private void ClearControls_QualDetails()
    {
        ddldegree.SelectedIndex = 0;
        txtYearOfExamQualifying.Text = string.Empty;
        txtMarksObtainedQualifying.Text = string.Empty;
        txtOutOfMarksQualifying.Text = string.Empty;
        txtPercentageQualifying.Text = string.Empty;
        txtAttemptQualifying.Text = string.Empty;
        txtSupervisorName1.Text = string.Empty;
        txtSchoolCollegeNameQualifying.Text = string.Empty;
        txtBoardQualifying.Text = string.Empty;
        txtGradeQualifying.Text = string.Empty;
        txtPercentileQualifying.Text = string.Empty;
        txtResearchTopic.Text = string.Empty;
        txtSupervisorName2.Text = string.Empty;
        txtQExamRollNo.Text = string.Empty;
        txtQualExmAddress.Text = string.Empty;
        txtQualiMedium.Text = string.Empty;
        txtQualExamRollNo.Text = string.Empty;
    }

    private void ClearControls_Entrance()
    {
        ddlExamNo.SelectedIndex = 0;
        txtAllIndiaRank.Text = string.Empty;
        txtYearOfExam.Text = string.Empty;
        txtStateRank.Text = string.Empty;
        txtPer.Text = string.Empty;
        txtQExamRollNo.Text = string.Empty;
        txtPercentile.Text = string.Empty;
        txtScore.Text = string.Empty;
        txtLastSchoolName.Text = string.Empty;
    }
    private void BindLastQualifiedExamData(ref QualifiedExam[] qualifiedExams)
    {
        DataTable dt;
        if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)
        {
            int index = 0;
            dt = (DataTable)Session["qualifyTbl"];
            qualifiedExams = new QualifiedExam[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                QualifiedExam objQualExam = new QualifiedExam();
                if (ViewState["usertype"].ToString() == "2")
                {
                    objQualExam.Idno = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objQualExam.Idno = Convert.ToInt32(Session["stuinfoidno"]);
                }

                objQualExam.Qualifyno = Convert.ToInt32(dr["QUALIFYNO"]);
                objQualExam.School_college_name = dr["SCHOOL_COLLEGE_NAME"].ToString();
                objQualExam.Year_of_exam = dr["YEAR_OF_EXAMHSSC"].ToString();

                if (dr["MARKS_OBTAINED"].ToString() == "0" || dr["MARKS_OBTAINED"].ToString() == null || dr["MARKS_OBTAINED"].ToString() == "")
                {
                    objQualExam.MarksObtained = 0;
                }
                else
                {
                    objQualExam.MarksObtained = Convert.ToInt32(dr["MARKS_OBTAINED"]);
                }

                if (dr["OUT_OF_MRKS"].ToString() == "0" || dr["OUT_OF_MRKS"].ToString() == null || dr["OUT_OF_MRKS"].ToString() == "")
                {
                    objQualExam.Out_of_marks = 0;
                }
                else
                {
                    objQualExam.Out_of_marks = Convert.ToInt32(dr["OUT_OF_MRKS"]);
                }

                if (dr["PER"].ToString() == "0" || dr["PER"].ToString() == null || dr["PER"].ToString() == "")
                {
                    objQualExam.Per = 0;
                }
                else
                {
                    objQualExam.Per = Convert.ToDecimal(dr["PER"]);
                }

                objQualExam.Percentile = Convert.ToDecimal(dr["PERCENTILE"]);
                objQualExam.Attempt = dr["ATTEMPT"].ToString();
                objQualExam.Board = dr["BOARD"].ToString();
                objQualExam.Grade = dr["GRADE"].ToString();
                objQualExam.Res_topic = dr["RES_TOPIC"].ToString();
                objQualExam.Supervisor_name1 = dr["SUPERVISOR_NAME1"].ToString();
                objQualExam.Supervisor_name2 = dr["SUPERVISOR_NAME2"].ToString();
                objQualExam.College_code = Session["colcode"].ToString();
                //objQualExam.College_address = txtQualExmAddress.Text.Trim();
                //objQualExam.Qual_medium = txtQualiMedium.Text.Trim();
                objQualExam.College_address = dr["COLLEGE_ADDRESS"].ToString();
                objQualExam.Qual_medium = dr["QUAL_MEDIUM"].ToString();
                objQualExam.Qexmrollno = dr["QEXMROLLNO"].ToString();
                qualifiedExams[index] = objQualExam;
                index++;
            }
        }
    }

    protected void btnEditQualDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            //DataTable dt1;//***************
            if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)
            {
                dt = ((DataTable)Session["qualifyTbl"]);
                //dt1 = dt.Copy();//**********************************
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                //DataRow dr = this.GetEditableDataRow(dt1, btnEdit.CommandArgument);//**********

                ddldegree.SelectedValue = dr["QUALIFYNO"].ToString();
                txtSchoolCollegeNameQualifying.Text = dr["SCHOOL_COLLEGE_NAME"].ToString();
                txtYearOfExamQualifying.Text = dr["YEAR_OF_EXAMHSSC"].ToString();
                txtMarksObtainedQualifying.Text = dr["MARKS_OBTAINED"].ToString();
                txtOutOfMarksQualifying.Text = dr["OUT_OF_MRKS"].ToString();
                txtPercentageQualifying.Text = dr["PER"].ToString();
                txtPercentileQualifying.Text = dr["PERCENTILE"].ToString();
                txtAttemptQualifying.Text = dr["ATTEMPT"].ToString();
                txtBoardQualifying.Text = dr["BOARD"].ToString();
                //if (dr["BOARD"] == null || dr["BOARD"] == string.Empty || dr["BOARD"] == "")
                //{
                //    //ddlBoardSsc.Items.Add(new ListItem("Please Select", "0"));
                //    ddlBoardQualifying.SelectedIndex = 0;
                //}

                //else
                //{
                //    string boardno = objCommon.LookUp("ACD_BOARD", "BOARDNO", "BOARD='" + dr["BOARD"].ToString() + "'");
                //    //ddlBoardSsc.SelectedItem.Text = dtr["BOARDSSC"].ToString();
                //    ddlBoardQualifying.SelectedValue = boardno;
                //}
                txtGradeQualifying.Text = dr["GRADE"].ToString();
                txtResearchTopic.Text = dr["RES_TOPIC"].ToString();
                txtSupervisorName1.Text = dr["SUPERVISOR_NAME1"].ToString();
                txtSupervisorName2.Text = dr["SUPERVISOR_NAME2"].ToString();
                txtQualExmAddress.Text = dr["COLLEGE_ADDRESS"].ToString();
                txtQualiMedium.Text = dr["QUAL_MEDIUM"].ToString();

                txtQualExamRollNo.Text = dr["QEXMROLLNO"].ToString();
                ////if (dt1.Rows.Count > 1)//*********
                ////    dt1.Rows.Remove(dr);
                dt.Rows.Remove(dr);
                Session["qualifyTbl"] = dt;
                this.BindListView_DemandDraftDetails(dt);
                btnAdd.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //Added By Nikhil Vinod Lambe on 29022020
    protected void btnDeleteQualDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            StudentController objSC = new StudentController();
            Student objS = new Student();
            if (ViewState["usertype"].ToString() == "2")
            {
                objS.IdNo = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
            }

            objS.QUALIFYNO = Convert.ToString(btnDelete.CommandArgument);
            DataTable dt;
            if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)
            {
                dt = ((DataTable)Session["qualifyTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["qualifyTbl"] = dt;
                objSC.DeleteStudentQual(objS);
                this.BindListView_DemandDraftDetails(dt);
                // objCommon.DisplayMessage(this, "Data deleted successfully!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //*****************************************************************************************************************
    private void BindListView_DemandDraftDetails(DataTable dt)
    {
        try
        {
            lvQualExm.DataSource = dt;
            lvQualExm.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QUALIFYNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return dataRow;
    }
    protected void btnEtranceCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls_Entrance();
    }
    //protected void btnEntranceSave_Click(object sender, EventArgs e)
    //{
    //    if (Session["entranceTbl"] != null && ((DataTable)Session["entranceTbl"]) != null)
    //    {
    //        DataTable dt = (DataTable)Session["entranceTbl"];
    //        DataRow dr = dt.NewRow();
    //        dr["QUALIFYNO"] = Convert.ToInt32(ddlExamNo.SelectedValue);
    //        dr["QUALIFYEXAMNAME"] = ddlExamNo.SelectedItem.Text;
    //        //if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) dr["ALLINDIARANK"] = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
    //        if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) dr["ALLINDIARANK"] = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
    //        if (!txtYearOfExam.Text.Trim().Equals(string.Empty)) dr["YEAROFEXAM"] = txtYearOfExam.Text.Trim();
    //        if (!txtStateRank.Text.Trim().Equals(string.Empty)) dr["STATERANK"] = Convert.ToInt32(txtStateRank.Text.Trim());
    //        if (!txtPer.Text.Trim().Equals(string.Empty)) dr["PERCENTAGE"] = Convert.ToDecimal(txtPer.Text.Trim());
    //        dr["QEXMROLLNO"] = txtQExamRollNo.Text.Trim();
    //        if (!txtPercentile.Text.Trim().Equals(string.Empty)) dr["PERCENTILE"] = Convert.ToDecimal(txtPercentile.Text.Trim());
    //        dr["QUOTA"] = ddlQuota.Text.Trim();
    //        dt.Rows.Add(dr);
    //        Session["entranceTbl"] = dt;
    //      //  lvEntrance.DataSource = dt;
    //      //  lvEntrance.DataBind();
    //        this.ClearControls_Entrance();
    //    }
    //    else
    //    {
    //        DataTable dt = this.GetEntranceDataTable();
    //        DataRow dr = dt.NewRow();
    //        dr["QUALIFYNO"] = Convert.ToInt32(ddlExamNo.SelectedValue);
    //        dr["QUALIFYEXAMNAME"] = ddlExamNo.SelectedItem.Text;
    //        if (!txtAllIndiaRank.Text.Trim().Equals(string.Empty)) dr["ALLINDIARANK"] = Convert.ToInt32(txtAllIndiaRank.Text.Trim());
    //        if (!txtYearOfExam.Text.Trim().Equals(string.Empty)) dr["YEAROFEXAM"] = txtYearOfExam.Text.Trim();
    //        if (!txtStateRank.Text.Trim().Equals(string.Empty)) dr["STATERANK"] = Convert.ToInt32(txtStateRank.Text.Trim());
    //        if (!txtPer.Text.Trim().Equals(string.Empty)) dr["PERCENTAGE"] = Convert.ToDecimal(txtPer.Text.Trim());
    //        dr["QEXMROLLNO"] = txtQExamRollNo.Text.Trim();
    //        if (!txtPercentile.Text.Trim().Equals(string.Empty)) dr["PERCENTILE"] = Convert.ToDecimal(txtPercentile.Text.Trim());
    //        dt.Rows.Add(dr);
    //        Session["entranceTbl"] = dt;
    //      //  lvEntrance.DataSource = dt;
    //      //  lvEntrance.DataBind();
    //        this.ClearControls_Entrance();
    //    }
    //   // btnEntranceSave.Text = "Add Entrance Exam";
    //}


    private void ShowStudentDetails()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataTableReader dtr = null;
            int stuidno = 0;
            if (ViewState["usertype"].ToString() == "2")
            {
                dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));
                stuidno = Convert.ToInt32(Session["idno"]);
                Session["stuinfoidno"] = stuidno;
            }
            else
            {
                dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));
                stuidno = Convert.ToInt32(Session["stuinfoidno"]);

            }
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //SSC MARKS
                    txtSchoolCollegeNameSsc.Text = dtr["SCHOOL_COLLEGE_NAMESSC"] == null ? string.Empty : dtr["SCHOOL_COLLEGE_NAMESSC"].ToString();
                    txtBoardSsc.Text = dtr["BOARDSSC"] == null ? string.Empty : dtr["BOARDSSC"].ToString();
                    //if (dtr["BOARDSSC"] == null || dtr["BOARDSSC"] == string.Empty || dtr["BOARDSSC"] == "")
                    //{
                    //    //ddlBoardSsc.Items.Add(new ListItem("Please Select", "0"));
                    //    ddlBoardSsc.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    string boardno = objCommon.LookUp("ACD_BOARD", "BOARDNO", "BOARD='" + dtr["BOARDSSC"].ToString()+"'");
                    //    //ddlBoardSsc.SelectedItem.Text = dtr["BOARDSSC"].ToString();
                    //    ddlBoardSsc.SelectedValue = boardno;
                    //}
                    hdnDegree.Value= dtr["DEGREE"].ToString();
                    txtYearOfExamSsc.Text = dtr["YEAR_OF_EXAMSSC"] == null ? string.Empty : dtr["YEAR_OF_EXAMSSC"].ToString();
                    txtSSCMedium.Text = dtr["SSC_MEDIUM"] == null ? string.Empty : dtr["SSC_MEDIUM"].ToString();
                    txtMarksObtainedSsc.Text = dtr["MARKS_OBTAINEDSSC"].ToString() == "0" ? string.Empty : dtr["MARKS_OBTAINEDSSC"].ToString();
                    txtOutOfMarksSsc.Text = dtr["OUT_OF_MRKSSSC"].ToString() == "0" ? string.Empty : dtr["OUT_OF_MRKSSSC"].ToString();
                    txtPercentageSsc.Text = dtr["PERSSC"] == null ? string.Empty : dtr["PERSSC"].ToString();
                    txtExamRollNoSsc.Text = dtr["QEXMROLLNOSSC"] == null ? string.Empty : dtr["QEXMROLLNOSSC"].ToString();
                    txtPercentileSsc.Text = dtr["PERCENTILESSC"] == null ? string.Empty : dtr["PERCENTILESSC"].ToString();
                    txtGradeSsc.Text = dtr["GRADESSC"] == null ? string.Empty : dtr["GRADESSC"].ToString();
                    txtAttemptSsc.Text = dtr["ATTEMPTSSC"] == null ? string.Empty : dtr["ATTEMPTSSC"].ToString();
                    txtSSCSchoolColgAdd.Text = dtr["SSC_COLLEGE_ADDRESS"] == null ? string.Empty : dtr["SSC_COLLEGE_ADDRESS"].ToString();
                    ddlDivisionSsc.SelectedValue = dtr["DIVISION_SSC"] == null ? "0" : dtr["DIVISION_SSC"].ToString();
                    txtMarksheetNoSsc.Text = dtr["MARKSHEET_NO_SSC"] == null ? string.Empty : dtr["MARKSHEET_NO_SSC"].ToString();

                    //HSSC Marks

                    txtSchoolCollegeNameHssc.Text = dtr["SCHOOL_COLLEGE_NAMEHSSC"] == null ? string.Empty : dtr["SCHOOL_COLLEGE_NAMEHSSC"].ToString();
                    txtBoardHssc.Text = dtr["BOARDHSSC"] == null ? string.Empty : dtr["BOARDHSSC"].ToString();
                    //if (dtr["BOARDHSSC"] == null || dtr["BOARDHSSC"] == string.Empty || dtr["BOARDHSSC"] == "")
                    //{
                    //    //ddlBoardSsc.Items.Add(new ListItem("Please Select", "0"));
                    //    ddlBoardHssc.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    string boardno = objCommon.LookUp("ACD_BOARD", "BOARDNO", "BOARD='" + dtr["BOARDHSSC"].ToString()+"'");
                    //    //ddlBoardSsc.SelectedItem.Text = dtr["BOARDSSC"].ToString();
                    //    ddlBoardHssc.SelectedValue = boardno;
                    //}

                    txtYearOfExamHssc.Text = dtr["YEAR_OF_EXAM_GCET"] == null ? string.Empty : dtr["YEAR_OF_EXAM_GCET"].ToString();
                    txtHSSCMedium.Text = dtr["HSSC_MEDIUM"] == null ? string.Empty : dtr["HSSC_MEDIUM"].ToString();
                    txtMarksObtainedHssc.Text = dtr["MARKS_OBTAINEDHSSC"].ToString() == "0" ? string.Empty : dtr["MARKS_OBTAINEDHSSC"].ToString();
                    txtOutOfMarksHssc.Text = dtr["OUT_OF_MRKSHSSC"].ToString() == "0" ? string.Empty : dtr["OUT_OF_MRKSHSSC"].ToString();
                    txtPercentageHssc.Text = dtr["PERHSSC"] == null ? string.Empty : dtr["PERHSSC"].ToString();
                    txtExamRollNoHssc.Text = dtr["QEXMROLLNO_GET"] == null ? string.Empty : dtr["QEXMROLLNO_GET"].ToString();
                    txtPercentileHssc.Text = dtr["PERCENTILEHSSC"] == null ? "0" : dtr["PERCENTILEHSSC"].ToString();
                    txtGradeHssc.Text = dtr["GRADEHSSC"] == null ? string.Empty : dtr["GRADEHSSC"].ToString();
                    txtAttemptHssc.Text = dtr["ATTEMPTHSSC"] == null ? string.Empty : dtr["ATTEMPTHSSC"].ToString();
                    txtHSCColgAddress.Text = dtr["HSSC_COLLEGE_ADDRESS"] == null ? string.Empty : dtr["HSSC_COLLEGE_ADDRESS"].ToString();
                    ddlDivisionHsc.SelectedValue = dtr["DIVISION_HSC"] == null ? "0" : dtr["DIVISION_HSC"].ToString();
                    txtMarksheetNoHsc.Text = dtr["MARKSHEET_NO_HSC"] == null ? string.Empty : dtr["MARKSHEET_NO_HSC"].ToString();
                    //Subject Wise Marks

                    //txtHscChe.Text = dtr["HSC_CHE_GCET"] == null ? "0" : dtr["HSC_CHE_GCET"].ToString();
                    //txtHscCheMax.Text = dtr["HSC_CHE_MAX_GCET"] == null ? "0" : dtr["HSC_CHE_MAX_GCET"].ToString();
                    //txtHscPhy.Text = dtr["HSC_PHY_GCET"] == null ? "0" : dtr["HSC_PHY_GCET"].ToString();
                    //txtHscPhyMax.Text = dtr["HSC_PHY_MAX_GCET"] == null ? "0" : dtr["HSC_PHY_MAX_GCET"].ToString();
                    //txtHscEngHssc.Text = dtr["HSC_ENG_GCET"] == null ? string.Empty : dtr["HSC_ENG_GCET"].ToString();
                    //txtHscEngMaxHssc.Text = dtr["HSC_ENG_MAX_GCET"] == null ? string.Empty : dtr["HSC_ENG_MAX_GCET"].ToString();
                    //txtHscMaths.Text = dtr["HSC_MAT_GCET"] == null ? string.Empty : dtr["HSC_MAT_GCET"].ToString();
                    //txtHscMathsMax.Text = dtr["HSC_MAT_MAX_GCET"] == null ? string.Empty : dtr["HSC_MAT_MAX_GCET"].ToString();


                    //txtchem.Text = dtr["HSC_PHY"] == null ? "0" : dtr["HSC_PHY"].ToString();
                    ////txtchemtot.Text = dtr["HSC_PHY_MAX"] == "0" ? "100" : dtr["HSC_PHY_MAX"].ToString();
                    //txtphymark.Text = dtr["HSC_CHE"] == null ? "0" : dtr["HSC_CHE"].ToString();
                    ////txtphymarktotal.Text = dtr["HSC_PHY_MAX_GCET"] == "0" ? "100" : dtr["HSC_PHY_MAX_GCET"].ToString();
                    ////txtVocationalmarktotal.Text = dtr["HSC_ENG_MAX_GCET"] == "0" ? "100" : dtr["HSC_ENG_MAX_GCET"].ToString();
                    //txtmaths.Text = dtr["HSC_MAT"] == null ? string.Empty : dtr["HSC_MAT"].ToString();
                    //txtVocationalmark.Text = dtr["OTHERS_1"] == null ? string.Empty : dtr["OTHERS_1"].ToString();
                    //ss.Text = dtr["OTHERS_1_MAX"] == null ? string.Empty : dtr["OTHERS_1_MAX"].ToString();
                    //txtVocsub.Text = dtr["OTHER_SUB"] == null ? string.Empty : dtr["OTHER_SUB"].ToString(); 
                    ////txtmathstot.Text = dtr["HSC_MAT_MAX_GCET"] == "0" ? "100" : dtr["HSC_MAT_MAX_GCET"].ToString();


                    DataSet ds = objCommon.FillDropDown("ACD_STU_LAST_QUALEXM", "HSC_PHY,HSC_CHE", "HSC_MAT,HSC_BIO,HSC_PCM_PERCENTAGE,HSC_PCM,OTHERS_1,MARKS_OBTAINED as MARKS_OBTAINEDHSSC,OTHERS_1_MAX,OTHER_SUB", "IDNO = " + Convert.ToInt32(Session["stuinfoidno"]) + "AND QUALIFYNO=2", string.Empty);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtphymark.Text = ds.Tables[0].Rows[0]["HSC_PHY"].ToString();
                        txtchem.Text = ds.Tables[0].Rows[0]["HSC_CHE"].ToString();
                        txtmaths.Text = ds.Tables[0].Rows[0]["HSC_MAT"].ToString();
                        txtbiology.Text = ds.Tables[0].Rows[0]["HSC_BIO"].ToString();
                        
                        //------------------------------------------------------------------------------
                        txtPcmMarks.Text = ds.Tables[0].Rows[0]["HSC_PCM"].ToString();
                        hfdPcmMarks.Value = ds.Tables[0].Rows[0]["HSC_PCM"].ToString();                 //Added by sachin on 20-07-2022
                        txtPcmPerct.Text = ds.Tables[0].Rows[0]["HSC_PCM_PERCENTAGE"].ToString();
                        hfdPcmPer.Value = ds.Tables[0].Rows[0]["HSC_PCM_PERCENTAGE"].ToString();        //Added by sachin on 20-07-2022
                        //------------------------------------------------------------------------------

                        txtVocationalmark.Text = ds.Tables[0].Rows[0]["OTHERS_1"].ToString();
                        txtVocationalmarktotal.Text = ds.Tables[0].Rows[0]["OTHERS_1_MAX"].ToString();
                        txtVocsub.Text = ds.Tables[0].Rows[0]["OTHER_SUB"].ToString();
                    }
                    ////txtMarksObtainedHssc.Text = ds.Tables[0].Rows[0]["MARKS_OBTAINEDHSSC"].ToString();

                    //txtphymark.Text = dtr["HSC_PHY"] == null ? "0" : dtr["HSC_PHY"].ToString();
                    //txtchem.Text = dtr["HSC_CHE"] == null ? "0" : dtr["HSC_CHE"].ToString();               
                    //txtmaths.Text = dtr["HSC_MAT"] == null ? string.Empty : dtr["HSC_MAT"].ToString();
                    //txtVocationalmark.Text = dtr["OTHERS_1"] == null ? string.Empty : dtr["OTHERS_1"].ToString();
                    //txtVocationalmarktotal.Text = dtr["OTHERS_1_MAX"] == null ? string.Empty : dtr["OTHERS_1_MAX"].ToString();
                    //txtVocsub.Text = dtr["OTHER_SUB"] == null ? string.Empty : dtr["OTHER_SUB"].ToString(); 
                    //DIPLOMA MARKS
                    txtSchoolCollegeNameDiploma.Text = dtr["SCHOOL_COLLEGE_NAME_DIPLOMA"] == null ? string.Empty : dtr["SCHOOL_COLLEGE_NAME_DIPLOMA"].ToString();
                    // txtBoardSsc.Text = dtr["BOARDSSC"] == null ? string.Empty : dtr["BOARDSSC"].ToString();
                    txtBoardDiploma.Text = dtr["BOARDDIPLOMA"] == null ? "0" : dtr["BOARDDIPLOMA"].ToString();
                    //if (dtr["BOARDDIPLOMA"] == null || dtr["BOARDDIPLOMA"] == string.Empty || dtr["BOARDDIPLOMA"] == "")
                    //{
                    //    //ddlBoardSsc.Items.Add(new ListItem("Please Select", "0"));
                    //    ddlBoardDiploma.SelectedIndex = 0;
                    //}

                    //else
                    //{
                    //    string boardno = objCommon.LookUp("ACD_BOARD", "BOARDNO", "BOARD='" + dtr["BOARDDIPLOMA"].ToString()+"'");
                    //    //ddlBoardSsc.SelectedItem.Text = dtr["BOARDSSC"].ToString();
                    //    ddlBoardDiploma.SelectedValue = boardno;
                    //}

                    txtYearOfExamDiploma.Text = dtr["YEAR_OF_EXAMDIPLOMA"] == null ? string.Empty : dtr["YEAR_OF_EXAMDIPLOMA"].ToString();
                    txtDiplomaMedium.Text = dtr["DIPLOMA_MEDIUM"] == null ? string.Empty : dtr["DIPLOMA_MEDIUM"].ToString();
                    txtMarksObtainedDiploma.Text = dtr["MARKS_OBTAINEDDIPLOMA"] == null ? string.Empty : dtr["MARKS_OBTAINEDDIPLOMA"].ToString();
                    txtOutOfMarksDiploma.Text = dtr["OUT_OF_MRKSDIPLOMA"] == null ? string.Empty : dtr["OUT_OF_MRKSDIPLOMA"].ToString();
                    txtPercentageDiploma.Text = dtr["PERDIPLOMA"] == null ? string.Empty : dtr["PERDIPLOMA"].ToString();
                    txtExamRollNoDiploma.Text = dtr["QEXMROLLNODIPLOMA"] == null ? string.Empty : dtr["QEXMROLLNODIPLOMA"].ToString();
                    txtPercentileDiploma.Text = dtr["PERCENTILEDIPLOMA"] == null ? string.Empty : dtr["PERCENTILEDIPLOMA"].ToString();
                    txtGradeDiploma.Text = dtr["GRADEDIPLOMA"] == null ? string.Empty : dtr["GRADEDIPLOMA"].ToString();
                    txtAttemptDiploma.Text = dtr["ATTEMPTDIPLOMA"] == null ? string.Empty : dtr["ATTEMPTDIPLOMA"].ToString();
                    txtDiplomaColgAddress.Text = dtr["DIPLOMA_COLLEGE_ADDRESS"] == null ? string.Empty : dtr["DIPLOMA_COLLEGE_ADDRESS"].ToString();
                    ddlDivisionDiploma.SelectedValue = dtr["DIVISION_DIPLOMA"] == null ? "0" : dtr["DIVISION_DIPLOMA"].ToString();
                    txtMarksheetNoDiploma.Text = dtr["MARKSHEET_NO_DIPLOMA"] == null ? string.Empty : dtr["MARKSHEET_NO_DIPLOMA"].ToString();

                    //Entrance Exam Scores
                    //ddlExamNo.SelectedValue = dtr["QUALIFYNO"].ToString();
                    //txtQExamRollNo.Text = dtr["QEXMROLLNOHSSC"] == null ? string.Empty : dtr["QEXMROLLNOHSSC"].ToString();
                    //txtYearOfExam.Text = dtr["YEAR_OF_EXAM"] == null ? string.Empty : dtr["YEAR_OF_EXAM"].ToString();
                    //txtPer.Text = dtr["PERCENTAGE"] == null ? string.Empty : dtr["PERCENTAGE"].ToString();
                    //txtPercentile.Text = dtr["PERCENTILE"] == null ? string.Empty : dtr["PERCENTILE"].ToString();
                    //txtAllIndiaRank.Text = dtr["ALL_INDIA_RANK"] == null ? string.Empty : dtr["ALL_INDIA_RANK"].ToString();
                    //txtScore.Text = dtr["SCORE"] == null ? string.Empty : dtr["SCORE"].ToString();

                    // txtPaper.Text = dtr["PAPER"] == null ? string.Empty : dtr["PAPER"].ToString();
                    // txtpaperCode.Text = dtr["PAPER_CODE"] == null ? string.Empty : dtr["PAPER_CODE"].ToString();
                    ddlpgentranceno.SelectedValue = dtr["OTHER_QUALIFYNO"].ToString();
                    txtpgrollno.Text = dtr["OTHER_QEXMROLLNO"] == null ? string.Empty : dtr["OTHER_QEXMROLLNO"].ToString();
                    txtpgexamyear.Text = dtr["OTHER_YEAR_OF_EXAM"] == null ? string.Empty : dtr["OTHER_YEAR_OF_EXAM"].ToString();
                    txtpgpercentage.Text = dtr["OTHER_PERCENTAGE"] == null ? string.Empty : dtr["OTHER_PERCENTAGE"].ToString();
                    txtpgpercentile.Text = dtr["OTHER_PERCENTILE"] == null ? string.Empty : dtr["OTHER_PERCENTILE"].ToString();
                    txtpgrank.Text = dtr["OTHER_PG_RANK"] == null ? string.Empty : dtr["OTHER_PG_RANK"].ToString();
                    txtpgscore.Text = dtr["OTHER_SCORE"] == null ? string.Empty : dtr["OTHER_SCORE"].ToString();

                    txtHscBioHssc.Text = dtr["HSC_BIO"] == null ? string.Empty : dtr["HSC_BIO"].ToString();

                    txtHscBioMaxHssc.Text = dtr["HSC_BIO_MAX"] == null ? string.Empty : dtr["HSC_BIO_MAX"].ToString();
                    txtHscPcmMaxHssc.Text = dtr["HSC_PCM_MAX"] == null ? string.Empty : dtr["HSC_PCM_MAX"].ToString();
                    //txtQExamRollNo.Text = dtr["QEXMROLLNO"] == null ? string.Empty : dtr["QEXMROLLNO"].ToString();
                    txtNataMarks.Text = dtr["NATA_MARKS"] == null ? string.Empty : dtr["NATA_MARKS"].ToString();
                    //Added By HEmanth G For Percentage Calculation
                    if (txtHscChe.Text.Trim() != "0" && txtHscCheMax.Text.Trim() != "0" && txtHscChe.Text.Trim() != string.Empty && txtHscCheMax.Text.Trim() != "")
                    {
                        //  txtHscPhy.Text = (Convert.ToDecimal(((Convert.ToInt32(txtHscChe.Text.Trim())) * 100) / (Convert.ToInt32(txtHscCheMax.Text.Trim())))).ToString();
                        txtHscPhy.Text = (Double.Parse(txtHscChe.Text.Trim()) / Double.Parse(txtHscCheMax.Text.Trim()) * 100.0).ToString("#0.00");
                    }
                    else
                    {
                        txtHscPhy.Text = string.Empty;
                    }
                    //Display Item in Listview Control by Calling this Method
                    BindListViewQualifyExamDetails(stuidno);

                    BindListView_EntranceExamDetails(stuidno);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListViewQualifyExamDetails(int idno)
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet dsCT = objSC.GetAllQualifyExamDetails(Convert.ToInt32(idno));

            if (dsCT != null && dsCT.Tables.Count > 0 && dsCT.Tables[0].Rows.Count > 0)
            {
                lvQualExm.DataSource = dsCT;
                lvQualExm.DataBind();
                Session["qualifyTbl"] = dsCT.Tables[0];

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
        Session["entranceTbl"] = null;
        Response.Redirect("~/academic/PersonalDetails.aspx");

        // HttpContext.Current.RewritePath("PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/academic/AddressDetails.aspx", false);
        Session["entranceTbl"] = null;
        Response.Redirect("~/academic/AddressDetails.aspx");
    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {
        Session["entranceTbl"] = null;
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
        Session["entranceTbl"] = null;
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
                objCommon.DisplayMessage("Please Search Enrollment No!!", this.Page);
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
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.updAddressDetails, this.updAddressDetails.GetType(), "controlJSScript", sb.ToString(), true);
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
            Session["stuinfoidno"] = null;
            Session["stuinfoenrollno"] = null;
            Session["stuinfofullname"] = null;
            Session["entranceTbl"] = null;
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }
    protected void rdoHsc_CheckedChanged(object sender, EventArgs e)
    {
        pnlHsc.Visible = true;
        pnlDiploma.Visible = false;
    }
    protected void rdoDiploma_CheckedChanged(object sender, EventArgs e)
    {
        pnlHsc.Visible = false;
        pnlDiploma.Visible = true;
    }
    protected void btnAddEntranceExam_Click(object sender, EventArgs e)
    {
        string errorString = ValidationAlertForDetailsByKeyword("entrance");      // Added By Shrikant W. on 27-12-2023

        if (errorString != string.Empty)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "alertmessage('" + errorString + "');", true);
        }
        else
        {

            ////if (Session["qualifyTbl"] != null && ((DataTable)Session["qualifyTbl"]) != null)//***************        
            if (Session["entranceTbl"] != null && ((DataTable)Session["entranceTbl"]).Rows.Count > 0 && ((DataTable)Session["entranceTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["entranceTbl"];
                //DataTable dt = new DataTable();
                DataRow dr = dt.NewRow();
                //DataRow [] dr1;
                if (btnAddEntranceExam.Text != "Update")
                {
                    string expression = string.Empty;
                    expression = "QUALIFYNAME='" + ddlExamNo.SelectedItem.Text + "'";
                    DataRow[] dr1 = dt.Select(expression);
                    //dr1 = dt.Rows.Find(expression);
                    if (dr1.Length > 0)
                    {
                        lvEntranceExm.DataSource = dt;
                        lvEntranceExm.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvEntranceExm);//Set label 
                        //ClearControls_QualDetails();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022
                        objCommon.DisplayMessage(this, "Data for selected exam already exist!", this.Page);
                        return;
                    }
                }


                if (ddlExamNo.SelectedIndex > 0 && (txtQExamRollNo.Text != string.Empty || txtQExamRollNo.Text != "") && (txtYearOfExam.Text != string.Empty || txtYearOfExam.Text != "") && (txtAllIndiaRank.Text != string.Empty || txtAllIndiaRank.Text != "") && (txtLastSchoolName.Text!=string.Empty || txtLastSchoolName.Text != ""))
                {
                    dr["QUALIFYNO"] = Convert.ToInt32(ddlExamNo.SelectedValue);
                    dr["QUALIFYNAME"] = ddlExamNo.SelectedItem.Text;
                    dr["YEAR_OF_EXAM"] = txtYearOfExam.Text.Trim() == "" ? string.Empty : txtYearOfExam.Text.Trim();
                    dr["PER"] = txtPer.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPer.Text.Trim());
                    dr["CGPA"] = txtPercentile.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentile.Text.Trim());
                    dr["ALL_INDIA_RANK"] = txtAllIndiaRank.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtAllIndiaRank.Text.Trim());
                    dr["SCORE"] = txtScore.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtScore.Text.Trim());
                    dr["EXMROLLNO"] = txtQExamRollNo.Text.Trim();
                    dr["LAST_SCHOOL_NAME"] = txtLastSchoolName.Text.Trim();
                    dt.Rows.Add(dr);
                    Session["entranceTbl"] = dt;
                    lvEntranceExm.DataSource = dt;
                    lvEntranceExm.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvEntranceExm);//Set label 
                    ClearControls_Entrance();
                    // objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please enter all details.", this.Page);
                }
            }
            else
            {
                DataTable dt = this.GetEntranceDataTable();
                DataRow dr = dt.NewRow();

                if (ddlExamNo.SelectedIndex > 0 && (txtQExamRollNo.Text != string.Empty) && (txtYearOfExam.Text != string.Empty || txtYearOfExam.Text != "") && (txtAllIndiaRank.Text != string.Empty || txtAllIndiaRank.Text != ""))
                {
                    dr["QUALIFYNO"] = Convert.ToInt32(ddlExamNo.SelectedValue);
                    dr["QUALIFYNAME"] = ddlExamNo.SelectedItem.Text;
                    dr["YEAR_OF_EXAM"] = txtYearOfExam.Text.Trim() == "" ? string.Empty : txtYearOfExam.Text.Trim();
                    dr["PER"] = txtPer.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPer.Text.Trim());
                    dr["CGPA"] = txtPercentile.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtPercentile.Text.Trim());
                    dr["ALL_INDIA_RANK"] = txtAllIndiaRank.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtAllIndiaRank.Text.Trim());
                    dr["SCORE"] = txtScore.Text.Trim() == "" ? 0.0m : Convert.ToDecimal(txtScore.Text.Trim());
                    dr["EXMROLLNO"] = txtQExamRollNo.Text.Trim();
                    dr["LAST_SCHOOL_NAME"] = txtLastSchoolName.Text.Trim();
                    dt.Rows.Add(dr);
                    Session["entranceTbl"] = dt;
                    lvEntranceExm.DataSource = dt;
                    lvEntranceExm.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvEntranceExm);//Set label 
                    ClearControls_Entrance();
                    // objCommon.DisplayMessage(this, "Data saved successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this, " Please enter all details.", this.Page);
                }
            }
            btnAddEntranceExam.Text = "Add";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022
        }
    }
    protected void btnEditEntranceDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            //DataTable dt1;//***************
            if (Session["entranceTbl"] != null && ((DataTable)Session["entranceTbl"]) != null)
            {
                dt = ((DataTable)Session["entranceTbl"]);
                //dt1 = dt.Copy();//**********************************
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                //DataRow dr = this.GetEditableDataRow(dt1, btnEdit.CommandArgument);//**********

                ddlExamNo.SelectedValue = dr["QUALIFYNO"].ToString();
                txtYearOfExam.Text = dr["YEAR_OF_EXAM"].ToString();
                txtPer.Text = dr["PER"].ToString();
                txtPercentile.Text = dr["CGPA"].ToString();
                txtAllIndiaRank.Text = dr["ALL_INDIA_RANK"].ToString();
                txtScore.Text = dr["SCORE"].ToString();
                txtQExamRollNo.Text = dr["EXMROLLNO"].ToString();
                txtLastSchoolName.Text = dr["LAST_SCHOOL_NAME"].ToString();
                ////if (dt1.Rows.Count > 1)//*********
                ////    dt1.Rows.Remove(dr);
                dt.Rows.Remove(dr);
                Session["entranceTbl"] = dt;
                this.BindListView_EntranceDetails(dt);
                btnAddEntranceExam.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnDeleteEntranceDetail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            StudentController objSC = new StudentController();
            Student objS = new Student();
            if (ViewState["usertype"].ToString() == "2")
            {
                objS.IdNo = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
            }
            objS.QUALIFYNO = Convert.ToString(btnDelete.CommandArgument);
            DataTable dt;
            if (Session["entranceTbl"] != null && ((DataTable)Session["entranceTbl"]) != null)
            {
                dt = ((DataTable)Session["entranceTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
                Session["entranceTbl"] = dt;
                objSC.DeleteStudentEntranceExam(objS);
                this.BindListView_EntranceDetails(dt);
                // objCommon.DisplayMessage(this, "Data deleted successfully!", this.Page);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "function()", "PCMTotal()", true);  //Added by sachin 28-10-2022

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListView_EntranceExamDetails(int idno)
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet dsCT = objSC.GetAllEntranceExamDetails(Convert.ToInt32(idno));

            if (dsCT != null && dsCT.Tables.Count > 0 && dsCT.Tables[0].Rows.Count > 0)
            {
                lvEntranceExm.DataSource = dsCT;
                lvEntranceExm.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvEntranceExm);//Set label 
                Session["entranceTbl"] = dsCT.Tables[0];

            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void BindEntranceExamData(ref QualifiedExam[] EntranceExams)
    {
        DataTable dt;
        if (Session["entranceTbl"] != null && ((DataTable)Session["entranceTbl"]) != null)
        {
            int index = 0;
            dt = (DataTable)Session["entranceTbl"];
            EntranceExams = new QualifiedExam[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                QualifiedExam objQualExam = new QualifiedExam();
                if (ViewState["usertype"].ToString() == "2")
                {
                    objQualExam.Idno = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objQualExam.Idno = Convert.ToInt32(Session["stuinfoidno"]);
                }

                objQualExam.Qualifyno = Convert.ToInt32(dr["QUALIFYNO"]);
                objQualExam.Year_of_exam = dr["YEAR_OF_EXAM"].ToString();

                if (dr["PER"].ToString() == "0" || dr["PER"].ToString() == null || dr["PER"].ToString() == "")
                {
                    objQualExam.Per = 0;
                }
                else
                {
                    objQualExam.Per = Convert.ToDecimal(dr["PER"]);
                }

                objQualExam.Percentile = Convert.ToDecimal(dr["CGPA"]);
                objQualExam.Meritno = Convert.ToInt32(dr["ALL_INDIA_RANK"]);
                objQualExam.Score = Convert.ToDecimal(dr["SCORE"]);
                objQualExam.Qexmrollno = dr["EXMROLLNO"].ToString();
                objQualExam.LastSchoolName = dr["LAST_SCHOOL_NAME"].ToString();
                EntranceExams[index] = objQualExam;
                index++;
            }
        }
    }

    private void BindListView_EntranceDetails(DataTable dt)
    {
        try
        {
            lvEntranceExm.DataSource = dt;
            lvEntranceExm.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvEntranceExm);//Set label 
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void txtOutOfMarksSsc_TextChanged(object sender, EventArgs e)
    {
        if (txtMarksObtainedSsc.Text != "" && txtMarksObtainedSsc.Text != string.Empty && txtOutOfMarksSsc.Text != "" && txtOutOfMarksSsc.Text != string.Empty)
        {
            if (decimal.Parse(txtMarksObtainedSsc.Text.Trim()) > decimal.Parse(txtOutOfMarksSsc.Text.Trim()))
            {
                objCommon.DisplayMessage(this, "Please Enter Secondary/10th Total marks less than Secondary/10th out of marks.", this.Page);
                txtMarksObtainedSsc.Text = string.Empty;
                txtOutOfMarksSsc.Text = string.Empty;
                return;
            }
            else
            {
                decimal per = decimal.Parse(txtMarksObtainedSsc.Text) * 100 / decimal.Parse(txtOutOfMarksSsc.Text);
                txtPercentageSsc.Text = Math.Round(per, 2).ToString();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Enter Secondary/10th Total marks and Secondary/10th out of marks.", this.Page);
            return;
        }

    }
    protected void txtMarksObtainedSsc_TextChanged(object sender, EventArgs e)
    {
        if (txtMarksObtainedSsc.Text != "" && txtMarksObtainedSsc.Text != string.Empty && txtOutOfMarksSsc.Text != "" && txtOutOfMarksSsc.Text != string.Empty)
        {
            if (decimal.Parse(txtMarksObtainedSsc.Text.Trim()) > decimal.Parse(txtOutOfMarksSsc.Text.Trim()))
            {
                objCommon.DisplayMessage(this, "Please Enter Secondary/10th Total marks less than Secondary/10th out of marks.", this.Page);
                txtMarksObtainedSsc.Text = string.Empty;
                txtOutOfMarksSsc.Text = string.Empty;
                return;
            }
            else
            {
                decimal per = decimal.Parse(txtMarksObtainedSsc.Text) * 100 / decimal.Parse(txtOutOfMarksSsc.Text);
                txtPercentageSsc.Text = Math.Round(per, 2).ToString();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Enter Secondary/10th Total marks and Secondary/10th out of marks.", this.Page);
            return;
        }
    }
    protected void txtOutOfMarksHssc_TextChanged(object sender, EventArgs e)
    {
        if (txtMarksObtainedHssc.Text != "" && txtMarksObtainedHssc.Text != string.Empty && txtOutOfMarksHssc.Text != "" && txtOutOfMarksHssc.Text != string.Empty)
        {
            if (decimal.Parse(txtMarksObtainedHssc.Text.Trim()) > decimal.Parse(txtOutOfMarksHssc.Text.Trim()))
            {
                objCommon.DisplayMessage(this, "Please Enter Higher Secondary/12th Total marks less than Higher Secondary/12th out of marks.", this.Page);
                txtMarksObtainedHssc.Text = string.Empty;
                txtOutOfMarksHssc.Text = string.Empty;
                return;
            }
            else
            {
                decimal per = decimal.Parse(txtMarksObtainedHssc.Text) * 100 / decimal.Parse(txtOutOfMarksHssc.Text);
                txtPercentageHssc.Text = Math.Round(per, 2).ToString();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Enter Higher Secondary/12th Total marks and Higher Secondary/12th out of marks.", this.Page);
            return;
        }
    }


    protected void txtMarksObtainedHssc_TextChanged(object sender, EventArgs e)
    {
        if (txtMarksObtainedHssc.Text != "" && txtMarksObtainedHssc.Text != string.Empty && txtOutOfMarksHssc.Text != "" && txtOutOfMarksHssc.Text != string.Empty)
        {
            if (decimal.Parse(txtMarksObtainedHssc.Text.Trim()) > decimal.Parse(txtOutOfMarksHssc.Text.Trim()))
            {
                objCommon.DisplayMessage(this, "Please Enter Higher Secondary/12th Total marks less than Higher Secondary/12th out of marks.", this.Page);
                txtMarksObtainedHssc.Text = string.Empty;
                txtOutOfMarksHssc.Text = string.Empty;
                return;
            }
            else
            {
                decimal per = decimal.Parse(txtMarksObtainedHssc.Text) * 100 / decimal.Parse(txtOutOfMarksHssc.Text);
                txtPercentageHssc.Text = Math.Round(per, 2).ToString();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Enter Secondary/12th Total marks and Secondary/12th out of marks.", this.Page);
            return;
        }
    }

    protected void txtOutOfMarksDiploma_TextChanged(object sender, EventArgs e)
    {
        if (txtMarksObtainedDiploma.Text != "" && txtMarksObtainedDiploma.Text != string.Empty && txtOutOfMarksDiploma.Text != "" && txtOutOfMarksDiploma.Text != string.Empty)
        {
            if (decimal.Parse(txtMarksObtainedDiploma.Text.Trim()) > decimal.Parse(txtOutOfMarksDiploma.Text.Trim()))
            {
                objCommon.DisplayMessage(this, "Please Enter Diploma Total marks less than Diploma out of marks.", this.Page);
                txtMarksObtainedDiploma.Text = string.Empty;
                txtOutOfMarksDiploma.Text = string.Empty;
                return;
            }
            else
            {
                decimal per = decimal.Parse(txtMarksObtainedDiploma.Text) * 100 / decimal.Parse(txtOutOfMarksDiploma.Text);
                txtPercentageDiploma.Text = Math.Round(per, 2).ToString();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Enter Diploma Total marks and Diploma out of marks.", this.Page);
            return;
        }
    }

    protected void txtMarksObtainedDiploma_TextChanged(object sender, EventArgs e)
    {
        if (txtMarksObtainedDiploma.Text != "" && txtMarksObtainedDiploma.Text != string.Empty && txtOutOfMarksDiploma.Text != "" && txtOutOfMarksDiploma.Text != string.Empty)
        {
            if (decimal.Parse(txtMarksObtainedDiploma.Text.Trim()) > decimal.Parse(txtOutOfMarksDiploma.Text.Trim()))
            {
                objCommon.DisplayMessage(this, "Please Enter Diploma Total marks less than Diploma out of marks.", this.Page);
                txtMarksObtainedDiploma.Text = string.Empty;
                txtOutOfMarksDiploma.Text = string.Empty;
                return;
            }
            else
            {
                decimal per = decimal.Parse(txtMarksObtainedDiploma.Text) * 100 / decimal.Parse(txtOutOfMarksDiploma.Text);
                txtPercentageDiploma.Text = Math.Round(per, 2).ToString();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Enter Diploma Total marks and Diploma out of marks.", this.Page);
            return;
        }
    }
    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }

    protected void txtOutOfMarksDiploma_TextChanged1(object sender, EventArgs e)
    {
        if (txtOutOfMarksDiploma.Text != string.Empty && txtOutOfMarksDiploma.Text != string.Empty && txtOutOfMarksDiploma.Text != string.Empty)
        {
            txtPercentageDiploma.Text = Math.Round(((Convert.ToDecimal(txtMarksObtainedDiploma.Text.Trim()) / Convert.ToDecimal(txtOutOfMarksDiploma.Text.Trim())) * 100), 2).ToString();
        }
    }
}