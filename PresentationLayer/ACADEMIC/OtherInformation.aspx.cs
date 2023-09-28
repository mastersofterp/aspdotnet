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


public partial class ACADEMIC_OtherInformation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                this.FillDropDown();
                //ViewState["usertype"] = Session["usertype"];
                ViewState["usertype"] = SessionHelper.Users.UserType;

                string param = objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME='ALLOW_ANTIRAGGING_MANDATORY'");   //Added by sachin on 22-07-2022                                      
                hfdParamValue.Value = param.ToString();

                Session["OrgId"] = objCommon.LookUp("reff with (nolock)", "OrganizationId", string.Empty);

                //string param = objCommon.LookUp("ACD_PARAMETER", "PARAM_VALUE", "PARAM_NAME='ALLOW_ANTIRAGGING_MANDATORY'");   //Added by sachin on 22-07-2022   
                //  Session["colcode"].ToString()
                lblCollegeName.Text = Session["coll_name"].ToString();

                if (ViewState["usertype"].ToString() == "2")
                {
                    divadmissiondetailstreeview.Visible = false;
                    divAdmissionApprove.Visible = false;
                    divhome.Visible = false;

                    //btnGohome.Visible = false;

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

                        //divPrintReport.Visible = true;
                        int FinalSubmit = 0;
                        if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                        {
                            FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                        }
                        if (FinalSubmit == 1)
                        {
                            divPrintReport.Visible = true;
                            chkAntiRagging.Enabled = false;

                        }
                        else
                        {
                            divPrintReport.Visible = false;
                            chkAntiRagging.Visible = true;

                        }

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
                            btnAddSport.Visible = false;
                            btnadd.Visible = false;
                        }
                    }
                    CheckFinalSubmission(); // Added By Bhagyashree on 30052023
                    //if (status.ToString() != "")
                    //{
                    //    if (status == "1")
                    //    {
                    //        btnSubmit.Visible = false;
                    //        btnAddSport.Visible = false;
                    //        btnadd.Visible = false;
                    //    }
                    //    else if (status == "2")
                    //    {
                    //        btnSubmit.Visible = true;
                    //        btnAddSport.Visible = true;
                    //        btnadd.Visible = true;
                    //    }
                    //}
                }
                else if (ViewState["usertype"].ToString() == "8") //HOD
                {
                    divadmissiondetailstreeview.Visible = false;
                    divAdmissionApprove.Visible = true;
                    divhome.Visible = true;
                    btnSubmit.Visible = false;
                    btnAddSport.Visible = false;
                    btnadd.Visible = false;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                }
                else
                {
                    // btnGohome.Visible = true;
                    divadmissiondetailstreeview.Visible = true;
                    // divAdmissionApprove.Visible = false;
                    divhome.Visible = true;
                    lnkAddressDetail.Enabled = true;
                    lnkUploadDocument.Enabled = true;
                    lnkQualificationDetail.Enabled = true;
                    lnkotherinfo.Enabled = true;
                    lnkprintapp.Enabled = true;
                }

                this.ShowStudentDetails();
                this.bindexpdetails();
                this.bindSportDetails();

               
            }
        }
        //  this.FillDropDown();
    }

    private void CheckFinalSubmission()
    {
        string finalsubmit = objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"]) + "");
        DataSet dsallowprocess = objSC.GetAllowProcess(Convert.ToInt32(Session["idno"]), 6, 'E');
        int allowprocess = Convert.ToInt32(dsallowprocess.Tables[0].Rows[0]["COUNTPROCESS"].ToString());
        if (finalsubmit == "1" && Convert.ToInt32(Session["usertype"].ToString()) == 2 && allowprocess > 0)
        {
            btnSubmit.Visible = true;
            btnAddSport.Visible = true;
            btnadd.Visible = true;
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
                objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNAME");
                txtBirthPlace.Text = dtr["BIRTH_PLACE"] == null ? string.Empty : dtr["BIRTH_PLACE"].ToString();
                objCommon.FillDropDownList(ddlMotherToungeNo, "ACD_MTONGUE", "MTONGUENO", "MTONGUE", "MTONGUENO>0", "MTONGUE");
                ddlMotherToungeNo.SelectedValue = dtr["MTOUNGENO"] == null ? "0" : dtr["MTOUNGENO"].ToString();
                txtOtherLangauge.Text = dtr["OTHER_LANG"] == null ? string.Empty : dtr["OTHER_LANG"].ToString();
                txtBirthVillage.Text = dtr["BIRTH_VILLAGE"] == null ? string.Empty : dtr["BIRTH_VILLAGE"].ToString();

                txtBirthTaluka.Text = dtr["BIRTH_TALUKA"] == null ? string.Empty : dtr["BIRTH_TALUKA"].ToString();
                txtBirthDistrict.Text = dtr["BIRTH_DISTRICT"] == null ? string.Empty : dtr["BIRTH_DISTRICT"].ToString();
                //txtBirthState.Text = dtr["BIRTH_STATE"] == null ? string.Empty : dtr["BIRTH_STATE"].ToString();

                if (dtr["BIRTH_STATE"].ToString() == null || dtr["BIRTH_STATE"].ToString() == string.Empty || dtr["BIRTH_STATE"].ToString() == "")
                {
                    ddlState.Items.Add(new ListItem("Please Select", "0"));
                }
                else
                {
                    ddlState.SelectedItem.Text = dtr["BIRTH_STATE"].ToString();
                }
                txtBirthPinCode.Text = dtr["BIRTH_PINCODE"] == null ? string.Empty : dtr["BIRTH_PINCODE"].ToString();
                txtHeight.Text = dtr["HEIGHT"] == null ? "0" : dtr["HEIGHT"].ToString();
                txtWeight.Text = dtr["WEIGHT"] == null ? "0" : dtr["WEIGHT"].ToString();

                txtIdentiMark.Text = dtr["IDENTI_MARK"] == null ? string.Empty : dtr["IDENTI_MARK"].ToString();
                ddlBank.SelectedValue = dtr["BANKNO"] == DBNull.Value ? "0" : dtr["BANKNO"].ToString();
                txtAccNo.Text = dtr["ACC_NO"] == null ? string.Empty : dtr["ACC_NO"].ToString();
                txtPassportNo.Text = dtr["PASSPORTNO"] == null ? string.Empty : dtr["PASSPORTNO"].ToString();
                txtIFSC.Text = dtr["IFSCCODE"] == null ? string.Empty : dtr["IFSCCODE"].ToString();
                txtBankAddress.Text = dtr["BANKADDRESS"] == null ? string.Empty : dtr["BANKADDRESS"].ToString();


                //txtCountryDomicile.Text = dtr["COUNTRYDOMICILE"] == null ? string.Empty : dtr["COUNTRYDOMICILE"].ToString();


                //IF ragging = Convert.ToBoolean(dtr.["ANTI_RAGGING"].ToString());

                if (Convert.ToBoolean(dtr["ANTI_RAGGING"]) == true)    //Added by sachin on 23-07-2022
                {

                    chkAntiRagging.Checked = true;

                }
                else
                {
                    chkAntiRagging.Checked = false;

                }


                if (dtr["URBAN"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dtr["URBAN"]) == true)
                    {
                        rdobtn_urban.SelectedValue = "Y";
                    }
                    else
                    {
                        rdobtn_urban.SelectedValue = "N";
                    }
                }

                //}           
            }
        }
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlMotherToungeNo, "ACD_MTONGUE", "MTONGUENO", "MTONGUE", "MTONGUENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "MTONGUE");
            ddlMotherToungeNo.SelectedIndex = 11;
            objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "BANKNAME");
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "STATENAME");

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }


                string status = objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "STATUS", "IDNO=" + Convert.ToInt32(Session["idno"]));    //Added by sachin on 27-07-2022
                DataSet dsinfo = objCommon.FillDropDown("ACD_ADM_STUD_INFO_SUBMIT_LOG", "ISNULL(PERSONAL_INFO,0)PERSONAL_INFO, ISNULL(ADDRESS_INFO,0)ADDRESS_INFO, ISNULL(DOC_INFO,0)DOC_INFO, ISNULL(QUAL_INFO, 0)QUAL_INFO, ISNULL(OTHER_INFO,0)OTHER_INFO, ISNULL(COVID_INFO, 0)COVID_INFO, ISNULL(FINAL_SUBMIT,0)FINAL_SUBMIT", "ADMBATCH", "IdNo=" + Convert.ToInt32(Session["stuinfoidno"]) + "", string.Empty);
                if (dsinfo != null && dsinfo.Tables[0].Rows.Count > 0)
                {
                    string personal_info = dsinfo.Tables[0].Rows[0]["PERSONAL_INFO"].ToString();
                    string address_info = dsinfo.Tables[0].Rows[0]["ADDRESS_INFO"].ToString();
                    string doc_info = dsinfo.Tables[0].Rows[0]["DOC_INFO"].ToString();
                    string qual_info = dsinfo.Tables[0].Rows[0]["QUAL_INFO"].ToString();
                    string other_info = dsinfo.Tables[0].Rows[0]["OTHER_INFO"].ToString();
                    string covid_info = dsinfo.Tables[0].Rows[0]["COVID_INFO"].ToString();
                    string final_submit = dsinfo.Tables[0].Rows[0]["FINAL_SUBMIT"].ToString();

                    //divPrintReport.Visible = true;


                    if (personal_info == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Submit Personal Details.... !", this.Page);
                        return;
                    }
                    if (address_info == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Submit Address Details.... !", this.Page);
                        return;
                    }
                    if (doc_info == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Submit Upload Document.... !", this.Page);
                        return;
                    }
                    if (qual_info == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Submit Qualification Details.... !", this.Page);
                        return;
                    }
                    if (covid_info == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Submit Covid Vaccination Details.... !", this.Page);
                        return;
                    }
                    if (other_info == "0")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Submitted Other Information... !", this.Page);
                        return;
                    }

                }
                DataSet dscheckdocuments = null;
                if (ViewState["usertype"].ToString() == "2")
                {
                    dscheckdocuments = objSC.GetDocumentList(Convert.ToInt32(Session["idno"]));
                    string mandatorycount = dscheckdocuments.Tables[0].Rows[0]["MANDATORYCOUNT"].ToString();
                    string uploadcount = dscheckdocuments.Tables[0].Rows[0]["UPLOADCOUNT"].ToString();
                    if (mandatorycount != uploadcount)
                    {
                        objCommon.DisplayMessage(this.Page, "Please submit all mandatory documents from Upload Document tab.... !", this.Page);
                        return;
                    }
                }

                CustomStatus cs = (CustomStatus)objSC.UpdateStudentFinalInformation(objS);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();
                }


                //int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                //if (OrganizationId != 6)   //Added by crescent sachin 06-12-2022
                //{
                //    if (ddlBank.SelectedIndex == 0)
                //    {
                //        objCommon.DisplayMessage(this.Page, "Please select Bank Name!", this.Page);
                //        return;
                //    }
                //    if (txtAccNo.Text == string.Empty)
                //    {
                //        objCommon.DisplayMessage(this.Page, "Please Enter Account No!", this.Page);
                //        return;
                //    }
                //    if (txtIFSC.Text == string.Empty)
                //    {
                //        objCommon.DisplayMessage(this.Page, "Please Enter IFSC Code!", this.Page);
                //        return;
                //    }
                //    if (txtBankAddress.Text == string.Empty)
                //    {
                //        objCommon.DisplayMessage(this.Page, "Please Enter Bank Address!", this.Page);
                //        return;
                //    }
                //}


                //if (chkAntiRagging.Checked == true)
                //{
                //    btnSubmit.Enabled=true;
                //}
                //else
                //{
                //    btnSubmit.Enabled = false;
                //} 

                //objS.Anti_Ragging = chkAntiRagging.Checked;                            //Added by sachin on 23-07-2022        
                //if (!txtBirthPlace.Text.Trim().Equals(string.Empty)) objS.BirthPlace = txtBirthPlace.Text.Trim();
                //objS.MToungeNo = Convert.ToInt32(ddlMotherToungeNo.SelectedValue);
                //if (!txtOtherLangauge.Text.Trim().Equals(string.Empty)) objS.OtherLanguage = txtOtherLangauge.Text.Trim();
                //if (!txtBirthVillage.Text.Trim().Equals(string.Empty)) objS.Birthvillage = txtBirthVillage.Text.Trim();
                //if (!txtBirthTaluka.Text.Trim().Equals(string.Empty)) objS.Birthtaluka = txtBirthTaluka.Text.Trim();
                //if (!txtBirthDistrict.Text.Trim().Equals(string.Empty)) objS.Birthdistrict = txtBirthDistrict.Text.Trim();
                ////if (!txtBirthState.Text.Trim().Equals(string.Empty)) objS.Birthdistate = txtBirthState.Text.Trim();
                //objS.Birthdistate = ddlState.SelectedItem.Text;
                //if (ddlState.SelectedIndex == 0)
                //{
                //    objS.Birthdistate = "";
                //}
                //else
                //{
                //    objS.Birthdistate = ddlState.SelectedItem.Text;
                //}
                //if (!txtBirthPinCode.Text.Trim().Equals(string.Empty)) objS.BirthPinCode = txtBirthPinCode.Text.Trim();
                //if (!txtHeight.Text.Trim().Equals(string.Empty)) objS.Height = Convert.ToDecimal(txtHeight.Text.Trim());
                //if (!txtWeight.Text.Trim().Equals(string.Empty)) objS.Weight = Convert.ToDecimal(txtWeight.Text.Trim());
                //if (rdobtn_urban.SelectedValue == "Y")//**********
                //    objS.Urban = true;
                //else
                //    objS.Urban = false;
                //if (!txtIdentiMark.Text.Trim().Equals(string.Empty)) objS.IdentyMark = txtIdentiMark.Text.Trim();
                //objS.BankNo = Convert.ToInt32(ddlBank.SelectedValue);
                //if (!txtAccNo.Text.Trim().Equals(string.Empty)) objS.AccNo = txtAccNo.Text.Trim();
                //if (!txtPassportNo.Text.Trim().Equals(string.Empty)) objS.PassportNo = txtPassportNo.Text.Trim();
                ////if (!txtCountryDomicile.Text.Trim().Equals(string.Empty)) objS.CountryDomicile = txtCountryDomicile.Text.Trim();
                //if (!txtworkexp.Text.Trim().Equals(string.Empty)) objS.WorkExp = txtworkexp.Text.Trim();
                //if (!txtdesignation.Text.Trim().Equals(string.Empty)) objS.Designation = txtdesignation.Text.Trim();
                //if (!txtorgwork.Text.Trim().Equals(string.Empty)) objS.OrgLastWork = txtorgwork.Text.Trim();
                //if (!txttotalexp.Text.Trim().Equals(string.Empty)) objS.TotalWorkExp = txttotalexp.Text.Trim();
                ////Added By Nikhil V.Lambe on 10/02/2021
                //if (!txtIFSC.Text.ToUpper().Equals(string.Empty)) objS.IfscCode = txtIFSC.Text.Trim();
                //if (!txtBankAddress.Text.Trim().Equals(string.Empty)) objS.BankAddress = txtBankAddress.Text.Trim();



                ////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //CustomStatus cs = (CustomStatus)objSC.UpdateStudentOtherInformation(objS, Convert.ToInt32(Session["usertype"]));
                //if (cs.Equals(CustomStatus.RecordUpdated))
                //{
                //    ShowStudentDetails();

                //    //  if (ViewState["usertype"].ToString() == "8")
                //    //  {
                //    //      divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Other Information Updated Successfully!!'); </script>";

                //    //      //string strScript = "<SCRIPT language='javascript'>window.location='DASAStudentInformation.aspx';</SCRIPT>";
                //    //      //Page.ClientScript.RegisterStartupScript(this.GetType(), "strScript", strScript);
                //    //      Response.Redirect("~/academic/ApproveAdmission.aspx");
                //    //  }

                //    ////  divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('Other Information Updated Successfully!!'); </script>";
                //    //  objCommon.DisplayMessage(updotherinformation, "Information Updated Successfully!!", this.Page);






                if (ViewState["usertype"].ToString() == "1")                  //Added by sachin on 14-07-2022
                {
                    Response.Redirect("~/academic/ApproveAdmission.aspx");

                }
                else
                {
                    btnAddSport.Visible = false;
                    btnSubmit.Visible = false;
                    divPrintReport.Visible = true;     //Added by sachin on 18-07-2022
                    chkAntiRagging.Enabled = false;

                    CheckFinalSubmission();
                }


                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Information Updated Successfully!!');window.location ='ApproveAdmission.aspx';", true);
                //lnkprintapp.Enabled = true;
                //}
                //else
                // {
                //objCommon.DisplayMessage(updotherinformation, "Error Occured While Updating Other Information!!", this.Page);
                //}
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
                objCommon.DisplayMessage(this.updotherinformation, "Please Search Enrollment No!!", this.Page);
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
            ScriptManager.RegisterClientScriptBlock(this.updotherinformation, this.updotherinformation.GetType(), "controlJSScript", sb.ToString(), true);
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        int expno = 0;
        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                if (!txtworkexp.Text.Trim().Equals(string.Empty)) objS.WorkExp = txtworkexp.Text.Trim();
                if (!txtdesignation.Text.Trim().Equals(string.Empty)) objS.Designation = txtdesignation.Text.Trim();
                if (!txtorgwork.Text.Trim().Equals(string.Empty)) objS.OrgLastWork = txtorgwork.Text.Trim();
                if (!txttotalexp.Text.Trim().Equals(string.Empty)) objS.TotalWorkExp = txttotalexp.Text.Trim();
                if (!txtepfno.Text.Trim().Equals(string.Empty)) objS.EpfNo = txtepfno.Text.Trim();
                if (txtworkexp.ToolTip != "")
                {
                    expno = Convert.ToInt32(txtworkexp.ToolTip);
                }
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentWorkexp(objS, expno);
                if (cs.Equals(CustomStatus.RecordSaved) || cs.Equals(CustomStatus.RecordUpdated))
                {
                    this.bindexpdetails();
                }
                txtworkexp.Text = string.Empty;
                txtdesignation.Text = string.Empty;
                txtorgwork.Text = string.Empty;
                txtworkexp.ToolTip = string.Empty;
                txtepfno.Text = string.Empty;
                btnadd.Text = "Add";
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void bindexpdetails()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = null;
            DataTable dt = null;
            if (ViewState["usertype"].ToString() == "2")
            {
                ds = objSC.GetStudentExpDetails(Convert.ToInt32(Session["idno"]));

            }
            else
            {
                ds = objSC.GetStudentExpDetails(Convert.ToInt32(Session["stuinfoidno"]));
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                Session["expdetails"] = dt;
                lvExperience.DataSource = ds;
                lvExperience.DataBind();
            }
            else
            {
                lvExperience.DataSource = null;
                lvExperience.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnEditexpDetail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;
        //DataTable dt1;//***************
        if (Session["expdetails"] != null && ((DataTable)Session["expdetails"]) != null)
        {
            dt = ((DataTable)Session["expdetails"]);
            //dt1 = dt.Copy();//**********************************
            DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
            txtworkexp.Text = dr["WORK_EXP"].ToString();
            txtorgwork.Text = dr["ORG_LAST_WORK"].ToString();
            txtdesignation.Text = dr["DESIGNATION"].ToString();
            txtworkexp.ToolTip = dr["EXP_INC"].ToString();
            txtepfno.Text = dr["EPFNO"].ToString();

            dt.Rows.Remove(dr);
            //Session["expdetails"] = dt;
            this.bindexpdetails();
            btnadd.Text = "Update";
        }
    }
    //protected void btnDeleteexpDetail_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelete = sender as ImageButton;
    //        DataTable dt;
    //        if (Session["expdetails"] != null && ((DataTable)Session["expdetails"]) != null)
    //        {
    //            dt = ((DataTable)Session["expdetails"]);
    //            dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
    //            Session["qualifyTbl"] = dt;
    //            this.BindListView_DemandDraftDetails(dt);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic.btnDeleteQualDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EXP_INC"].ToString() == value)
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
    protected void btnDeleteworkDetail_Click(object sender, ImageClickEventArgs e)
    {
        StudentController objSC = new StudentController();
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;
        //DataTable dt1;//***************
        if (Session["expdetails"] != null && ((DataTable)Session["expdetails"]) != null)
        {
            dt = ((DataTable)Session["expdetails"]);
            //dt1 = dt.Copy();//**********************************

            CustomStatus cs = (CustomStatus)objSC.GetdeleteDataRow(Convert.ToInt32(btnEdit.ToolTip), Convert.ToInt32(btnEdit.CommandArgument));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                this.bindexpdetails();
            }


        }
    }

    private void bindSportDetails()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = null;
            DataTable dt = null;
            if (ViewState["usertype"].ToString() == "2")
            {
                ds = objSC.GetStudentSportDetails(Convert.ToInt32(Session["idno"]));

            }
            else
            {
                ds = objSC.GetStudentSportDetails(Convert.ToInt32(Session["stuinfoidno"]));
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                Session["sportdetails"] = dt;
                lvSport.DataSource = ds;
                lvSport.DataBind();
            }
            else
            {
                lvSport.DataSource = null;
                lvSport.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnAddSport_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();
        int srno = 0;
        try
        {
            if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7" || ViewState["usertype"].ToString() == "5")
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }
                if (!txtSportName.Text.Trim().Equals(string.Empty)) objS.SportName = txtSportName.Text.Trim();
                objS.SportLevel = Convert.ToInt32(ddlSportLevel.SelectedValue);
                if (!txtSportAchieve.Text.Trim().Equals(string.Empty)) objS.SportAchieve = txtSportAchieve.Text.Trim();
                if (txtSportName.ToolTip != "")
                {
                    srno = Convert.ToInt32(txtSportName.ToolTip);
                }
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentSportDetails(objS, srno);
                if (cs.Equals(CustomStatus.RecordSaved) || cs.Equals(CustomStatus.RecordUpdated))
                {
                    this.bindSportDetails();
                }
                txtSportName.Text = string.Empty;
                ddlSportLevel.SelectedIndex = 0;
                txtSportAchieve.Text = string.Empty;
                txtSportName.ToolTip = string.Empty;
                btnAddSport.Text = "Add";
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEditSportDetail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;
        //DataTable dt1;//***************
        if (Session["sportdetails"] != null && ((DataTable)Session["sportdetails"]) != null)
        {
            dt = ((DataTable)Session["sportdetails"]);
            //dt1 = dt.Copy();//**********************************
            DataRow dr = this.GetEditableSportDataRow(dt, btnEdit.CommandArgument);
            txtSportName.Text = dr["SPORT_NAME"].ToString();
            if (dr["SPORT_LEVEL"].ToString() == "District")
            {
                ddlSportLevel.SelectedValue = "1";
            }
            else if (dr["SPORT_LEVEL"].ToString() == "State")
            {
                ddlSportLevel.SelectedValue = "2";
            }
            else if (dr["SPORT_LEVEL"].ToString() == "National")
            {
                ddlSportLevel.SelectedValue = "3";
            }
            else if (dr["SPORT_LEVEL"].ToString() == "International")
            {
                ddlSportLevel.SelectedValue = "4";
            }
            else
            {
                ddlSportLevel.SelectedValue = "0";
            }
            txtSportAchieve.Text = dr["ACHIEVEMENT_DETAILS"].ToString();
            txtSportName.ToolTip = dr["SPORT_SRNO"].ToString();

            dt.Rows.Remove(dr);
            this.bindSportDetails();
            btnAddSport.Text = "Update";
        }
    }

    private DataRow GetEditableSportDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SPORT_SRNO"].ToString() == value)
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
    protected void btnDeleteSportDetail_Click(object sender, ImageClickEventArgs e)
    {
        StudentController objSC = new StudentController();
        ImageButton btnEdit = sender as ImageButton;
        DataTable dt;
        //DataTable dt1;//***************
        if (Session["sportdetails"] != null && ((DataTable)Session["sportdetails"]) != null)
        {
            dt = ((DataTable)Session["sportdetails"]);
            //dt1 = dt.Copy();//**********************************

            CustomStatus cs = (CustomStatus)objSC.DeleteSport(Convert.ToInt32(btnEdit.ToolTip), Convert.ToInt32(btnEdit.CommandArgument));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                this.bindSportDetails();
            }


        }
    }


    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/CovidVaccinationDetails.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
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
                if (ViewState["usertype"].ToString() == "2")
                {
                    objS.IdNo = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                }


                int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                if (OrganizationId != 6)   //Added by crescent sachin 06-12-2022
                {
                    if (ddlBank.SelectedIndex == 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Please select Bank Name!", this.Page);
                        return;
                    }
                    if (txtAccNo.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Account No!", this.Page);
                        return;
                    }
                    if (txtIFSC.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter IFSC Code!", this.Page);
                        return;
                    }
                    if (txtBankAddress.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Bank Address!", this.Page);
                        return;
                    }
                }

                objS.Anti_Ragging = chkAntiRagging.Checked;                            //Added by sachin on 23-07-2022        
                if (!txtBirthPlace.Text.Trim().Equals(string.Empty)) objS.BirthPlace = txtBirthPlace.Text.Trim();
                objS.MToungeNo = Convert.ToInt32(ddlMotherToungeNo.SelectedValue);
                if (!txtOtherLangauge.Text.Trim().Equals(string.Empty)) objS.OtherLanguage = txtOtherLangauge.Text.Trim();
                if (!txtBirthVillage.Text.Trim().Equals(string.Empty)) objS.Birthvillage = txtBirthVillage.Text.Trim();
                if (!txtBirthTaluka.Text.Trim().Equals(string.Empty)) objS.Birthtaluka = txtBirthTaluka.Text.Trim();
                if (!txtBirthDistrict.Text.Trim().Equals(string.Empty)) objS.Birthdistrict = txtBirthDistrict.Text.Trim();
                //if (!txtBirthState.Text.Trim().Equals(string.Empty)) objS.Birthdistate = txtBirthState.Text.Trim();
                objS.Birthdistate = ddlState.SelectedItem.Text;
                if (ddlState.SelectedIndex == 0)
                {
                    objS.Birthdistate = "";
                }
                else
                {
                    objS.Birthdistate = ddlState.SelectedItem.Text;
                }
                if (!txtBirthPinCode.Text.Trim().Equals(string.Empty)) objS.BirthPinCode = txtBirthPinCode.Text.Trim();
                if (!txtHeight.Text.Trim().Equals(string.Empty)) objS.Height = Convert.ToDecimal(txtHeight.Text.Trim());
                if (!txtWeight.Text.Trim().Equals(string.Empty)) objS.Weight = Convert.ToDecimal(txtWeight.Text.Trim());
                if (rdobtn_urban.SelectedValue == "Y")//**********
                    objS.Urban = true;
                else
                    objS.Urban = false;
                if (!txtIdentiMark.Text.Trim().Equals(string.Empty)) objS.IdentyMark = txtIdentiMark.Text.Trim();
                objS.BankNo = Convert.ToInt32(ddlBank.SelectedValue);
                if (!txtAccNo.Text.Trim().Equals(string.Empty)) objS.AccNo = txtAccNo.Text.Trim();
                if (!txtPassportNo.Text.Trim().Equals(string.Empty)) objS.PassportNo = txtPassportNo.Text.Trim();
                //if (!txtCountryDomicile.Text.Trim().Equals(string.Empty)) objS.CountryDomicile = txtCountryDomicile.Text.Trim();
                if (!txtworkexp.Text.Trim().Equals(string.Empty)) objS.WorkExp = txtworkexp.Text.Trim();
                if (!txtdesignation.Text.Trim().Equals(string.Empty)) objS.Designation = txtdesignation.Text.Trim();
                if (!txtorgwork.Text.Trim().Equals(string.Empty)) objS.OrgLastWork = txtorgwork.Text.Trim();
                if (!txttotalexp.Text.Trim().Equals(string.Empty)) objS.TotalWorkExp = txttotalexp.Text.Trim();
                //Added By Nikhil V.Lambe on 10/02/2021
                if (!txtIFSC.Text.ToUpper().Equals(string.Empty)) objS.IfscCode = txtIFSC.Text.Trim();
                if (!txtBankAddress.Text.Trim().Equals(string.Empty)) objS.BankAddress = txtBankAddress.Text.Trim();



                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                CustomStatus cs = (CustomStatus)objSC.UpdateStudentOtherInformation(objS, Convert.ToInt32(Session["usertype"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ShowStudentDetails();
                    objCommon.DisplayMessage(this.Page, "Other Information Saved Successfully!", this.Page);
                    lnkprintapp.Enabled = true;
                }
                else
                {
                    objCommon.DisplayMessage(updotherinformation, "Error Occured While Updating Other Information!!", this.Page);
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