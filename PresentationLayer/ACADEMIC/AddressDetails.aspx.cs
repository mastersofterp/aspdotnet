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
using System.Collections.Generic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

public partial class ACADEMIC_AddressDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    ModuleConfigController objConfig = new ModuleConfigController();

    List<string> validationErrors = new List<string>();

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
                this.FillDropDown();
                StudentConfiguration();
                if (ViewState["usertype"].ToString() == "2")
                {
                    ShowStudentDetails();                    
                    divadmissiondetails.Visible = false;
                    divAdmissionApprove.Visible = false;
                    divhome.Visible = false;
                    //divPrintReport.Visible = true;                   

                    int FinalSubmit = 0;
                    if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                    {
                        FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    }
                    if (FinalSubmit == 1)
                    { divPrintReport.Visible = true; }
                    else
                    { divPrintReport.Visible = false; }
                    // btnGohome.Visible = false;

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
                    }
                    //if (status.ToString() != "")
                    //{
                    //    if (status == "1")
                    //    {
                    //        btnSubmit.Visible = false;
                    //    }
                    //    else if (status == "2")
                    //    {
                    //        btnSubmit.Visible = true;
                    //    }
                    //}
                    CheckFinalSubmission(); // Added by Bhagyashree on 30052023
                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    divadmissiondetails.Visible = false;
                    divAdmissionApprove.Visible = true;
                    divhome.Visible = true;
                    ShowStudentDetails();
                    btnSubmit.Visible = false;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                }
                else
                {
                    ShowStudentDetails();
                    divadmissiondetails.Visible = true;
                    divAdmissionApprove.Visible = true;
                    divhome.Visible = true;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                    // btnGohome.Visible = true;

                    // pnlId.Visible = true;
                }
                hdn_Pdistrict.Value = "";
              
            }
        }
    }

    #region Student Related Configuration
    protected void StudentConfiguration()
    {
        DataSet ds = null;

        int orgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        string pageNo = "";
        string pageName = "AddressDetails.aspx";
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
        string pageName = "AddressDetails.aspx";
        ds = objConfig.GetStudentConfigData(orgID, pageNo, pageName);

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
    #endregion Student Related Configuration     // Added By Shrikant W. on 05-09-2023
    
    public void FillDropDown()
    {
        try
        {
            
            objCommon.FillDropDownList(ddlPermCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlLocalCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO>0", "COUNTRYNAME");
            objCommon.FillDropDownList(ddlLocalCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CITY");
            objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "STATENAME");
            objCommon.FillDropDownList(ddlPermCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "CITY");
            objCommon.FillDropDownList(ddlPermState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "STATENAME");
            objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "DISTRICTNAME");
            objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "DISTRICTNAME");
            ddlPdistrict.SelectedItem.Text = "Please Select";
            ddlLdistrict.SelectedItem.Text = "Please Select";
            ddlPermCountry.SelectedItem.Text = "Please Select";
            ddlLocalCountry.SelectedItem.Text = "Please Select";
            ddlPermState.SelectedItem.Text = "Please Select";
            ddlLocalState.SelectedItem.Text = "Please Select";

          
            //objCommon.FillDropDownList(ddlptaluka, "ACD_TALUKA", "TALUKANO", "TALUKANAME", "TALUKANO>0", "TALUKANAME");
            //objCommon.FillDropDownList(ddlLtaluka, "ACD_TALUKA", "TALUKANO", "TALUKANAME", "TALUKANO>0", "TALUKANAME");
           // ddlptaluka.SelectedItem.Text = "Please Select";
            //ddlLtaluka.SelectedItem.Text = "Please Select";

        }
        catch (Exception Ex)
        {
            throw;
        }
    }
    private void CheckFinalSubmission()
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        DataSet dsallowprocess = objSC.GetAllowProcess(Convert.ToInt32(Session["idno"]), 2, 'E');
        int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        if (finalsubmit == "1" && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
        {
            btnSubmit.Visible = true;
        }
    }
    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["idno"]));

        }

        else
        {
            dtr = objSC.GetStudentDetails(Convert.ToInt32(Session["stuinfoidno"]));

        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtCorresAddress.Text = dtr["CORRESPONDANCE_ADDRESS"] == null ? string.Empty : dtr["CORRESPONDANCE_ADDRESS"].ToString();
                txtCorresPin.Text = dtr["CORRESPONDANCE_PIN"] == null ? string.Empty : dtr["CORRESPONDANCE_PIN"].ToString();
                txtCorresMob.Text = dtr["CORRESPONDANCE_MOB"] == null ? string.Empty : dtr["CORRESPONDANCE_MOB"].ToString();
                txtLocalAddress.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                ddlLocalCity.SelectedValue = dtr["LCITY"] == null ? "0" : dtr["LCITY"].ToString();

                // ADDED CHANGES ON 19-05-2023 BY KAJAL JAISWAL FOR BINDING LCOUNTRY.
                if (dtr["LCOUNTRYNO"].ToString() == "0" || dtr["LCOUNTRYNO"] == null)
                {
                }
                else
                {
                    ddlLocalCountry.SelectedValue = dtr["LCOUNTRYNO"].ToString();
                }


                
                if (dtr["LSTATE"].ToString() == "0" || dtr["LSTATE"] == null)
                {
                }
                else
                {
                    ddlLocalState.SelectedValue = dtr["LSTATE"].ToString();
                }


                if (dtr["LDISTRICT"].ToString() == "0" || dtr["LDISTRICT"] == null)
                {
                }
                else
                {
                    ddlLdistrict.SelectedValue = dtr["LDISTRICT"].ToString();
                }

                //if (dtr["LTEHSIL"].ToString() == "0" || dtr["LTEHSIL"] == null)
                //{
                //}
                //else
                //{
                //    //ddlLtaluka.SelectedValue = dtr["LTEHSIL"].ToString();
                //}

                txtLTaluka.Text = dtr["LTEHSIL"] == null ? string.Empty : dtr["LTEHSIL"].ToString();        //chnages by Ruchika Dhakate on 16.09.2022
                txtLocalLandlineNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                txtLocalEmail.Text = dtr["LEMAIL"] == null ? string.Empty : dtr["LEMAIL"].ToString();
                txtLocalPIN.Text = dtr["LPINCODE"] == null ? string.Empty : dtr["LPINCODE"].ToString();
                txtLocalMobileNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                txtPermAddress.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                ddlPermCity.SelectedValue = dtr["PCITY"] == null ? "0" : dtr["PCITY"].ToString();

                // ADDED CHANGES ON 19-05-2023 BY KAJAL JAISWAL FOR BINDING PCOUNTRY.
                if (dtr["PCOUNTRYNO"].ToString() == "0" || dtr["PCOUNTRYNO"] == null)
                {
                }
                else
                {
                    
                    ddlPermCountry.SelectedValue = dtr["PCOUNTRYNO"].ToString();
                }





                if (dtr["PSTATE"].ToString() == "0" || dtr["PSTATE"] == null)
                {
                }
                else
                {
                    ddlPermState.SelectedValue = dtr["PSTATE"].ToString();
                }

                if (dtr["PDISTRICT"].ToString() == "0" || dtr["PDISTRICT"] == null)
                {
                }
                else
                {
                    ddlPdistrict.SelectedValue = dtr["PDISTRICT"].ToString();
                }
                //if (dtr["PTEHSIL"].ToString() == "0" || dtr["PTEHSIL"] == null)
                //{
                //}
                //else
                //{
                //    //ddlptaluka.SelectedValue = dtr["PTEHSIL"].ToString();
                //}
                txtPTaluka.Text = dtr["PTEHSIL"] == null ? string.Empty : dtr["PTEHSIL"].ToString();           //chnages by Ruchika Dhakate on 16.09.2022
                txtLocalNo.Text = dtr["PTELEPHONE"] == null ? string.Empty : dtr["PTELEPHONE"].ToString();
                txtPermEmailId.Text = dtr["PEMAIL"] == null ? string.Empty : dtr["PEMAIL"].ToString();
                txtPermPIN.Text = dtr["PPINCODE"] == null ? string.Empty : dtr["PPINCODE"].ToString();
                txtpostoff.Text = dtr["LPOSTOFF"] == null ? string.Empty : dtr["LPOSTOFF"].ToString();
                txtpolicestation.Text = dtr["LPOLICESTATION"] == null ? string.Empty : dtr["LPOLICESTATION"].ToString();
              //  txtTehsil.Text = dtr["PTEHSIL"] == null ? string.Empty : dtr["PTEHSIL"].ToString();
                txtpermpostOff.Text = dtr["PPOSTOFF"] == null ? string.Empty : dtr["PPOSTOFF"].ToString();
                txtPermPoliceStation.Text = dtr["PPOLICEOFF"] == null ? string.Empty : dtr["PPOLICEOFF"].ToString();
                // txtPermTehsil.Text = dtr["PTEHSIL"] == null ? string.Empty : dtr["PTEHSIL"].ToString();
                txtMobileNo.Text = dtr["PMOBILE"] == null ? string.Empty : dtr["PMOBILE"].ToString();
                txtGuardianAddress.Text = dtr["GADDRESS"] == null ? string.Empty : dtr["GADDRESS"].ToString();
                txtGoccupationName.Text = dtr["GOCCUPATIONNAME"] == null ? string.Empty : dtr["GOCCUPATIONNAME"].ToString();
                txtGuardianLandline.Text = dtr["GPHONE"] == null ? string.Empty : dtr["GPHONE"].ToString();
                txtguardianEmail.Text = dtr["GEMAIL"] == null ? string.Empty : dtr["GEMAIL"].ToString();
                txtGuardianName.Text = dtr["GUARDIANNAME"] == null ? string.Empty : dtr["GUARDIANNAME"].ToString();
                txtAnnualIncome.Text = dtr["ANNUAL_INCOME"] == null ? string.Empty : dtr["ANNUAL_INCOME"].ToString();
                txtRelationWithGuardian.Text = dtr["RELATION_GUARDIAN"] == null ? string.Empty : dtr["RELATION_GUARDIAN"].ToString();
                txtGDesignation.Text = dtr["GUARDIAN_DESIG"] == null ? string.Empty : dtr["GUARDIAN_DESIG"].ToString();
                txtOtherInfo.Text = dtr["GUARDIAN_OTHER_INFO"] == null ? string.Empty : dtr["GUARDIAN_OTHER_INFO"].ToString();
            }
        }
    }

    protected void ddlLocalState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");
            //and stateno=" + ddlLocalState.SelectedValue
            //objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlLocalState.SelectedValue, "DISTRICTNAME");    //Added by sachin on 29-07-2022
            //if (ddlLocalState.SelectedValue == "0")
            //{
            //    ddlLdistrict.SelectedIndex = -1;
            //}
            //else
            //    if (hdnldistrict.Value != "")//*****************
            //    {
            //        ddlLdistrict.SelectedIndex = Convert.ToInt32(hdnldistrict.Value);
            //    }

        string ParamValue = (objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME = 'ALLOW_LOAD_DISTRICT_ON_STATE_SELECTION_ONLY'"));
        if (ParamValue == "1")
        {
          //  objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlPermState.SelectedValue, "DISTRICTNAME");


            if (chkcopypermanentadress.Checked == true)
            {
                objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlPermState.SelectedValue, "DISTRICTNAME");

                ddlLdistrict.SelectedValue = ddlPdistrict.SelectedValue;
            }

            else
            {
                objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlPermState.SelectedValue, "DISTRICTNAME");

            }


        }
        if (ParamValue == "0")
        {
           // objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");


            if (chkcopypermanentadress.Checked == true)
            {
                 objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");

                ddlLdistrict.SelectedValue = ddlPdistrict.SelectedValue;
            }

            else
            {
                 objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");

            }

        }

        if (hdnldistrict.Value != "")//*****************
        {
            ddlLdistrict.SelectedIndex = Convert.ToInt32(hdnldistrict.Value);
        }

        }
        catch (Exception EX)
        {

        }

    }

    protected void ddlPermState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlPermState.SelectedValue, "DISTRICTNAME");    //Added by sachin on 29-07-2022
        // objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");


        //Added By Ruchika Dhakate on 18.10.2022 
        string ParamValue = (objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME = 'ALLOW_LOAD_DISTRICT_ON_STATE_SELECTION_ONLY'"));
        if (ParamValue == "1")
        {
            objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlPermState.SelectedValue, "DISTRICTNAME");
            objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0 and ISNULL (stateno,0) =" + ddlPermState.SelectedValue, "DISTRICTNAME");

        }
        if (ParamValue == "0")
        {
            objCommon.FillDropDownList(ddlPdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");
            objCommon.FillDropDownList(ddlLdistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO>0", "DISTRICTNAME");
        }

        if (hdn_Pdistrict.Value != "")//*****************
        {
            ddlPdistrict.SelectedIndex = Convert.ToInt32(hdn_Pdistrict.Value);
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string errorString = ValidationAlert();      // Added By Shrikant W. on 25-12-2023

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
            try
            {
                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5" || ViewState["usertype"].ToString() == "8")
                {

                    //17-05-2018 visible false (Not Necesscary for Indus Uni)
                    // if (!txtLocalEmail.Text.Trim().Equals(string.Empty)) objSAddress.LEMAIL = txtLocalEmail.Text.Trim();
                    //if (!txtCorresAddress.Text.Trim().Equals(string.Empty)) objS.Corres_address = txtCorresAddress.Text.Trim();
                    //if (!txtCorresPin.Text.Trim().Equals(string.Empty)) objS.Corres_pin = txtCorresPin.Text.Trim();
                    //if (!txtCorresMob.Text.Trim().Equals(string.Empty)) objS.Corres_mob = txtCorresMob.Text.Trim();
                    //if (!txtPermTehsil.Text.Trim().Equals(string.Empty)) objSAddress.PTEHSIL = txtPermTehsil.Text.Trim();             
                    //if (!txtPermEmailId.Text.Trim().Equals(string.Empty)) objSAddress.PEMAIL = txtPermEmailId.Text.Trim();


                    //Local Address
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        objS.IdNo = Convert.ToInt32(Session["idno"]);
                    }
                    else
                    {
                        objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                    }

                    if (!txtLocalAddress.Text.Trim().Equals(string.Empty)) objSAddress.LADDRESS = Convert.ToString(txtLocalAddress.Text.Trim());
                    objSAddress.LCITY = Convert.ToInt32(ddlLocalCity.SelectedValue);
                    objSAddress.LSTATE = Convert.ToInt32(ddlLocalState.SelectedValue);
                    objSAddress.LCOUNTRY = Convert.ToString(ddlLocalCountry.SelectedValue);

                    if (!txtLocalPIN.Text.Trim().Equals(string.Empty)) objSAddress.LPINCODE = txtLocalPIN.Text.Trim();
                    if (!txtLocalLandlineNo.Text.Trim().Equals(string.Empty)) objSAddress.LTELEPHONE = txtLocalLandlineNo.Text.Trim();
                    if (!txtLocalMobileNo.Text.Trim().Equals(string.Empty)) objSAddress.LMOBILE = txtLocalMobileNo.Text.Trim();
                    if (!txtpostoff.Text.Trim().Equals(string.Empty)) objSAddress.LPOSTOFF = txtpostoff.Text.Trim();
                    if (!txtpolicestation.Text.Trim().Equals(string.Empty)) objSAddress.LPOLICESTATION = txtpolicestation.Text.Trim();

                    //Permenent Address
                    if (!txtPermAddress.Text.Trim().Equals(string.Empty)) objSAddress.PADDRESS = txtPermAddress.Text.Trim();
                    objSAddress.PCOUNTRY = Convert.ToString(ddlPermCountry.SelectedValue);
                    objSAddress.PCITY = Convert.ToInt32(ddlPermCity.SelectedValue);
                    objSAddress.PSTATE = Convert.ToInt32(ddlPermState.SelectedValue);
                    objSAddress.PDISTRICT = ddlPdistrict.SelectedValue;
                    objSAddress.PTEHSIL = txtPTaluka.Text.Trim();
                    //objSAddress.PTEHSIL = ddlptaluka.SelectedValue;
                    if (chkcopypermanentadress.Checked == true)
                    {
                        objSAddress.LDISTRICT = ddlPdistrict.SelectedValue;
                        //  objSAddress.LTEHSIL = ddlLtaluka.SelectedValue;
                        objSAddress.LTEHSIL = txtLTaluka.Text.Trim();                         //Added By Ruchika dhakate on 16.09.2022
                    }
                    else
                    {
                        objSAddress.LDISTRICT = ddlLdistrict.SelectedValue;
                        //objSAddress.LTEHSIL = ddlLtaluka.SelectedValue;
                        objSAddress.LTEHSIL = txtLTaluka.Text.Trim();
                    }
                    if (!txtPermPIN.Text.Trim().Equals(string.Empty)) objSAddress.PPINCODE = txtPermPIN.Text.Trim();
                    if (!txtLocalNo.Text.Trim().Equals(string.Empty)) objSAddress.PTELEPHONE = txtLocalNo.Text.Trim();
                    if (!txtMobileNo.Text.Trim().Equals(string.Empty)) objSAddress.PMOBILE = txtMobileNo.Text.Trim();
                    if (!txtTehsil.Text.Trim().Equals(string.Empty)) objSAddress.LTEHSIL = txtTehsil.Text.Trim();
                    if (!txtpermpostOff.Text.Trim().Equals(string.Empty)) objSAddress.PPOSTOFF = txtpermpostOff.Text.Trim();
                    if (!txtPermPoliceStation.Text.Trim().Equals(string.Empty)) objSAddress.PPOLICESTATION = txtPermPoliceStation.Text.Trim();

                    //Guardian's Address
                    if (!txtGuardianAddress.Text.Trim().Equals(string.Empty)) objSAddress.GADDRESS = txtGuardianAddress.Text.Trim();
                    if (!txtGuardianName.Text.Trim().Equals(string.Empty)) objSAddress.GUARDIANNAME = txtGuardianName.Text.Trim();
                    if (!txtGuardianLandline.Text.Trim().Equals(string.Empty)) objSAddress.GPHONE = txtGuardianLandline.Text.Trim();
                    objSAddress.ANNUAL_INCOME = txtAnnualIncome.Text.Trim();
                    if (!txtRelationWithGuardian.Text.Trim().Equals(string.Empty)) objSAddress.RELATION_GUARDIAN = txtRelationWithGuardian.Text.Trim();
                    if (!txtGoccupationName.Text.Trim().Equals(string.Empty)) objSAddress.GOCCUPATIONNAME = txtGoccupationName.Text.Trim();
                    if (!txtGDesignation.Text.Trim().Equals(string.Empty)) objSAddress.GUARDIANDESIGNATION = txtGDesignation.Text.Trim();
                    if (!txtOtherInfo.Text.Trim().Equals(string.Empty)) objSAddress.GUARDIAN_OTHER_INFO = txtOtherInfo.Text.Trim();
                    int ua_no = Convert.ToInt32(Session["userno"]);// Added by Kajal J. on 15-03-2024 for maintaining log

                    CustomStatus cs = (CustomStatus)objSC.UpdateStudentAddressDetails(objS, objSAddress, Convert.ToInt32(Session["usertype"]), ua_no);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ShowStudentDetails();
                        // objCommon.DisplayMessage(updAddressDetails, "Address Details Updated Successfully!!", this.Page);

                        if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "8")
                        {
                            // divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Address Details Updated Successfully!!'); </script>";

                            //string strScript = "<SCRIPT language='javascript'>window.location='DASAStudentInformation.aspx';</SCRIPT>";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "strScript", strScript);
                            Response.Redirect("~/academic/UploadDocument.aspx");
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Address Details Updated Successfully!!'); location.href='UploadDocument.aspx';", true);
                        }
                        else
                        {
                            //divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Address Details Updated Successfully!!'); </script>";
                            //string strScript = "<SCRIPT language='javascript'>window.location='AdmissionDetails.aspx';</SCRIPT>";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "strScript", strScript);
                            Response.Redirect("~/academic/AdmissionDetails.aspx");
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "alert('Address Details Updated Successfully!!'); location.href='AdmissionDetails.aspx';", true);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updAddressDetails, "Error Occured While Updating Address Details!!", this.Page);
                    }

                }
                else
                {
                    objCommon.DisplayMessage("You Are Not Authorised Person For This Form.Contact To Administrator.", this.Page);

                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
    }

    protected void btnGohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/StudentInfoEntryNew.aspx?pageno=2219");
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
                objCommon.DisplayMessage(this.updAddressDetails, "Please Search Enrollment No!!", this.Page);
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
            ScriptManager.RegisterClientScriptBlock(this.updAddressDetails, this.updAddressDetails.GetType(), "controlJSScript", sb.ToString(), true);
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
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
        else
        {
            Response.Redirect("~/academic/StudentInfoEntry.aspx?pageno=74");
        }
    }

    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }

    protected void ddlPermCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommon.FillDropDownList(ddlPermState, "ACD_STATE", "STATENO", "STATENAME", "COUNTRYNO>0 and ISNULL (COUNTRYNO,0) =" + ddlPermCountry.SelectedValue, "STATENAME");
        
    }
       
    protected void ddlLocalCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkcopypermanentadress.Checked == true)
        {
            objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "COUNTRYNO>0 and ISNULL (COUNTRYNO,0) =" + ddlLocalCountry.SelectedValue, "STATENAME");
            ddlLocalState.SelectedValue = ddlPermState.SelectedValue;
        }

        else
        {
           objCommon.FillDropDownList(ddlLocalState, "ACD_STATE", "STATENO", "STATENAME", "COUNTRYNO>0 and ISNULL (COUNTRYNO,0) =" + ddlLocalCountry.SelectedValue, "STATENAME");

        }
    }
}

   