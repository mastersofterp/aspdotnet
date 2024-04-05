using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;
using System.Net;


public partial class PAYROLL_TRANSACTIONS_empinfo_crescent : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpCreateController ECC = new EmpCreateController();

    #region Page Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {

            ClearControls();
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null) //
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                rdodrugno.Checked = true;
                imgEmpSign.ImageUrl = "~/images/nophoto.jpg";
                //Populate all the DropDownLists
                FillDropDown();
                GetEmployeeCode();
                ddlBank.SelectedValue = "1";
                ddlBankPlace.SelectedValue = "1";
                ddlStatus.SelectedValue = "4";
                //Generate Max IDNo
                GenerateIdno();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                string userno = Session["userno"].ToString();
                string usertype = Session["usertype"].ToString();


                if (Request.QueryString["id"] != null)
                {
                    //imgSearch.Visible = true;
                    ViewState["action"] = "edit";
                    ShowEmpDetails(Convert.ToInt32(Request.QueryString["id"].ToString()));
                }
            }

            if (rdoMarried.Checked == true)
            {
                txtMaleChild.Enabled = true;
                txtFemaleChild.Enabled = true;
                //txtMaleChild.Text = "0";
                //txtFemaleChild.Text = "0";
            }
            else
            {
                txtMaleChild.Text = string.Empty;
                txtFemaleChild.Text = string.Empty;
                txtMaleChild.Enabled = false;
                txtFemaleChild.Enabled = false;
            }


        }
        else
        {


            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    string Category = string.Empty;
                    if (rbName.Checked == true) { Category = "name"; }
                    if (rbDept.Checked == true) { Category = "department"; }
                    if (rbDesig.Checked == true) { Category = "designation"; }
                    if (rbEmpId.Checked == true) { Category = "idno"; }
                    if (rbRFId.Checked == true) { Category = "RFIdno"; }
                    if (rbEmpNo.Checked == true) { Category = "EmployeeNo"; }
                    if (rbEmpCode.Checked == true) { Category = "PFILENO"; }

                    bindlist(Category, arg[1]);
                }
            }
        }
        //if (ViewState["action"] == "edit")
        //{
        //    ddlDesignation.SelectedValue = Session["designo"].ToString();
        //    ddlNuDesig.SelectedValue = Session["NUDesig"].ToString();
        //}
        //txtPersonalFileNo.Focus = true;
        divMsg.InnerHtml = string.Empty;
    }

    #endregion

    #region Private Method

    private void GetEmployeeCode()
    {
         DataSet ds = null;
         ds = ECC.GetEmployeeCode();

         txtPersonalFileNo.Text = ds.Tables[0].Rows[0]["EMPLOYEECODE"].ToString();
    }
    private void GenerateIdno()
    {
        try
        {
            EmpCreateController objECC = new EmpCreateController();
            txtIdNo.Text = objECC.GenerateId().ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.GenerateIdno-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEmpDetails(int idno)
    {
        try
        {
            EmpCreateController objECC = new EmpCreateController();

            DataTableReader dtr = objECC.ShowEmpDetails(idno);
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    txtIdNo.Text = dtr["idno"].ToString();
                    txtSeqNo.Text = dtr["seq_no"].ToString();
                    txtFirstName.Text = dtr["fname"].ToString();
                    txtMiddleName.Text = dtr["mname"].ToString();
                    txtLastName.Text = dtr["lname"].ToString();
                    txtFatherName.Text = dtr["fathername"].ToString();
                    txtMaidenName.Text = dtr["maidenname"].ToString();
                    txtHusbandName.Text = dtr["husbandname"].ToString();
                    txtAge.Text = dtr["AGE"].ToString();
                    //objCommon.FillDropDownList(ddlTitle, "PAYROLL_TITLE", "TITLENO", "TITLE", "TITLENO > 0", "TITLENO ASC");
                    ddlTitle.SelectedItem.Text = dtr["title"] == null ? string.Empty : dtr["title"].ToString();
                    // ddlTitle.Items.Insert(0, "Please Select");
                    if (Convert.ToChar(dtr["sex"].ToString()[0]) == 'M')
                        rdbMale.Checked = true;
                    else if (Convert.ToChar(dtr["sex"].ToString()[0]) == 'F')
                        rdbFemale.Checked = true;
                    txtRFIDno.Text = dtr["RFIDNO"] == null ? string.Empty : dtr["RFIDNO"].ToString();
                    txtBirthDate.Text = dtr["DOB"] == null ? string.Empty : dtr["DOB"].ToString();
                    txtIncrDate.Text = dtr["DOI"] == null ? string.Empty : dtr["DOI"].ToString();
                    txtJoinDate.Text = dtr["DOJ"] == null ? string.Empty : dtr["DOJ"].ToString();
                    txtRetireDate.Text = dtr["RDT"] == null ? string.Empty : dtr["RDT"].ToString();

                    if (dtr["ANFN"].ToString() == "AN")
                        rdbAN.Checked = true;
                    if (dtr["ANFN"].ToString() == "FN")
                        rdbFN.Checked = true;
                    //unicode ADD BY Rohit maske 25-09-2018
                    //txtFirstNameKannada.Text = dtr["FNAME_UNICODE"].ToString();
                    //txtMiddleNameKannada.Text = dtr["MNAME_UNICODE"].ToString();
                    //txtLastNameKannada.Text = dtr["LNAME_UNICODE"].ToString();
                    //txtFatherNameKannada.Text = dtr["FATHERNAME_UNICODE"].ToString();

                    ddlDesigNature.SelectedValue = dtr["DESIGNATURENO"] == null ? string.Empty : dtr["DESIGNATURENO"].ToString();
                    ddlDepartment.SelectedValue = dtr["SUBDEPTNO"] == null ? string.Empty : dtr["SUBDEPTNO"].ToString();
                    ddlDesignation.SelectedValue = dtr["SUBDESIGNO"] == null ? string.Empty : dtr["SUBDESIGNO"].ToString();
                    ddlNuDesig.SelectedValue = dtr["NUDESIG"] == null ? string.Empty : dtr["NUDESIG"].ToString();
                    //Session["designo"] = dtr["SUBDESIGNO"].ToString();
                    //Session["NUDesig"] = dtr["NUDESIG"].ToString();
                    //ddlDesignation.SelectedValue = dtr["SUBDESIGNO"] == null ? string.Empty : dtr["SUBDESIGNO"].ToString();
                    //ddlNuDesig.SelectedValue = dtr["NUDESIG"] == null ? string.Empty : dtr["NUDESIG"].ToString();
                    ddlBank.SelectedValue = dtr["BANKNO"] == null ? string.Empty : dtr["BANKNO"].ToString();
                    ddlVacational.SelectedValue = dtr["STNO"] == null ? string.Empty : dtr["STNO"].ToString();
                    ddlStaff.SelectedValue = dtr["STAFFNO"] == null ? string.Empty : dtr["STAFFNO"].ToString();
                    ddlAppointment.SelectedValue = dtr["APPOINTNO"] == null ? string.Empty : dtr["APPOINTNO"].ToString();
                    ddlPayRule.SelectedValue = dtr["PAYRULE"] == null ? string.Empty : dtr["PAYRULE"].ToString();
                    FillScale(Convert.ToInt32(ddlPayRule.SelectedValue));
                    ddlPayScale.SelectedValue = dtr["SCALENO"] == null ? string.Empty : dtr["SCALENO"].ToString();
                    ddlClassification.SelectedValue = dtr["CLNO"] == null ? string.Empty : dtr["CLNO"].ToString();
                    ddlBankPlace.SelectedValue = dtr["BANKCITYNO"] == null ? string.Empty : dtr["BANKCITYNO"].ToString();
                    ddlQuarter.SelectedValue = dtr["QTRNO"] == null ? string.Empty : dtr["QTRNO"].ToString();
                    txtAccNo.Text = dtr["BANKACC_NO"] == null ? string.Empty : dtr["BANKACC_NO"].ToString();
                    txtBasic.Text = dtr["BASIC"] == null ? string.Empty : dtr["BASIC"].ToString();
                    txtIFSCCode.Text = dtr["IFSC_CODE"] == null ? string.Empty : dtr["IFSC_CODE"].ToString();
                    txtPanNo.Text = dtr["PAN_NO"] == null ? string.Empty : dtr["PAN_NO"].ToString();
                    txtPFNo.Text = dtr["GPF_NO"] == null ? string.Empty : dtr["GPF_NO"].ToString();
                    txtPPFNo.Text = dtr["PPF_NO"] == null ? string.Empty : dtr["PPF_NO"].ToString();
                    txtRemark.Text = dtr["REMARK"] == null ? string.Empty : dtr["REMARK"].ToString();
                    txtGradePay.Text = dtr["GRADEPAY"] == null ? string.Empty : dtr["GRADEPAY"].ToString();
                    ddlPF.SelectedValue = dtr["PFNO"] == null ? string.Empty : dtr["PFNO"].ToString();
                    ddlCollege.SelectedValue = dtr["COLLEGE_NO"] == null ? string.Empty : dtr["COLLEGE_NO"].ToString();
                    FillShiftno(Convert.ToInt32(ddlCollege.SelectedValue));
                    ddlshiftno.SelectedValue = dtr["SHIFTNO"] == null ? string.Empty : dtr["SHIFTNO"].ToString();
                    txtPersonalFileNo.Text = dtr["PFILENO"] == null ? string.Empty : dtr["PFILENO"].ToString();
                    txtNationalUniqueIDNo.Text = dtr["NUNIQUEID"] == null ? string.Empty : dtr["NUNIQUEID"].ToString();

                    txtLocalAddress.Text = dtr["RESADD1"] == null ? string.Empty : dtr["RESADD1"].ToString();
                    txtPermanentAddress.Text = dtr["TOWNADD1"] == null ? string.Empty : dtr["TOWNADD1"].ToString();
                    txtPhoneNumber.Text = dtr["PHONENO"] == null ? string.Empty : dtr["PHONENO"].ToString();
                    txtAlterPhoneNumber.Text = dtr["ALTERNATE_PHONENO"] == null ? string.Empty : dtr["ALTERNATE_PHONENO"].ToString();
                    txtEmailId.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                    txtAlternateEmailId.Text = dtr["ALTERNATE_EMAILID"] == null ? string.Empty : dtr["ALTERNATE_EMAILID"].ToString();
                    ddlStatus.SelectedValue = dtr["STATUSNO"] == null ? string.Empty : dtr["STATUSNO"].ToString();
                    txtStatusDT.Text = dtr["STDATE"] == null ? string.Empty : dtr["STDATE"].ToString();
                    txtmothername.Text = dtr["MOTHERNAME"] == null ? string.Empty : dtr["MOTHERNAME"].ToString();

                    txtRelievingDate.Text = dtr["RELIEVING_DATE"] == null ? string.Empty : dtr["RELIEVING_DATE"].ToString();
                    txtExpiryDtExt.Text = dtr["Expire_dateof_Extention"] == null ? string.Empty : dtr["Expire_dateof_Extention"].ToString();

                   

                    ddlUserType.SelectedValue = dtr["UA_TYPE"] == null ? string.Empty : dtr["UA_TYPE"].ToString();
                    ddlEmpType.SelectedValue = dtr["EMPTYPENO"] == null ? string.Empty : dtr["EMPTYPENO"].ToString();
                    ddlPGDept.SelectedValue = dtr["PGSUBDEPTNO"] == null ? string.Empty : dtr["PGSUBDEPTNO"].ToString();

                    txtEmployeeId.Text = dtr["EmployeeId"] == null ? string.Empty : dtr["EmployeeId"].ToString();
                    txtUANNO.Text = dtr["UAN"] == null ? string.Empty : dtr["UAN"].ToString();
                    txtConsPay.Text = dtr["I8"] == null ? string.Empty : dtr["I8"].ToString();

                    DdlBloodGroup.SelectedValue = dtr["BLOODGRPNO"] == null ? string.Empty : dtr["BLOODGRPNO"].ToString();


                    if (Convert.ToBoolean(dtr["IS_SHIFT_MANAGMENT"]) == false)
                    {
                        isShiftManagement.Checked = false;
                    }
                    else
                    {
                        isShiftManagement.Checked = true;
                    }
                    if (Convert.ToBoolean(dtr["ISMARITALSTATUS"]) == false)
                    {
                        rdoMarried.Checked = false;
                        rdounMarried.Checked = true;
                        txtMaleChild.Enabled = false;
                        txtFemaleChild.Enabled = false;
                        txtMaleChild.Text = "0";
                        txtFemaleChild.Text = "0";
                    }
                    else
                    {
                        rdoMarried.Checked = true;
                        rdounMarried.Checked = false;
                        txtMaleChild.Enabled = true;
                        txtFemaleChild.Enabled = true;
                        txtMaleChild.Text = dtr["CHILDMALE"] == null ? string.Empty : dtr["CHILDMALE"].ToString();
                        txtFemaleChild.Text = dtr["CHILDFEMALE"] == null ? string.Empty : dtr["CHILDFEMALE"].ToString();
                    }

                    if (Convert.ToBoolean(dtr["ISPHYSICALLYCHALLENGED"]) == false)
                    {
                        chkHandicap.Checked = false;
                        ddlHandicap.SelectedIndex = 0;
                    }
                    else
                    {
                        chkHandicap.Checked = true;
                        divHandicapList.Visible = true;
                        ddlHandicap.SelectedValue = dtr["HANDICAPTYPEID"] == null ? string.Empty : dtr["HANDICAPTYPEID"].ToString();
                    }

                    txtcolRoom.Text = dtr["COLLEGEROOMNO"] == null ? string.Empty : dtr["COLLEGEROOMNO"].ToString();
                    txtColIntcomNo.Text = dtr["COLLEGEINTERCOMNO"] == null ? string.Empty : dtr["COLLEGEINTERCOMNO"].ToString();
                    txtDisplayQualification.Text = dtr["QUALFORDISPLAY"] == null ? string.Empty : dtr["QUALFORDISPLAY"].ToString();
                    txtEmployement.Text = dtr["EMPLOYMENT"] == null ? string.Empty : dtr["EMPLOYMENT"].ToString();
                    txtQuaterAltDate.Text = dtr["QUARTERSALLOTMENTDATE"] == null ? string.Empty : dtr["QUARTERSALLOTMENTDATE"].ToString();


                    // Amol sawarkar
                    if (Convert.ToBoolean(dtr["IsServicemen"]) == false)
                    {
                        chkexservicemen.Checked = false;
                    }
                    else
                    {
                        chkexservicemen.Checked = true;
                    }


                    if (Convert.ToBoolean(dtr["IsBusFas"]) == false)
                    {
                        isbusfac.Checked = false;
                    }
                    else
                    {
                        isbusfac.Checked = true;
                    }

                    if (Convert.ToBoolean(dtr["isCabFac"]) == false)
                    {
                        isCabFac.Checked = false;
                    }
                    else
                    {
                        isCabFac.Checked = true;
                    }

                    if (Convert.ToBoolean(dtr["ISNEFT"]) == false)
                    {
                        chkNEFT.Checked = false;
                    }
                    else
                    {
                        chkNEFT.Checked = true;
                    }


                    if (Convert.ToBoolean(dtr["isTelguMin"]) == false)
                    {
                        rdotelMin.Checked = true;
                        rdotelMinNo.Checked = false;
                    }
                    else if (Convert.ToBoolean(dtr["isTelguMin"]) == true)
                    {
                        rdotelMin.Checked = false;
                        rdotelMinNo.Checked = true;
                    }



                    if (Convert.ToBoolean(dtr["isDrugAlrg"]) == false)
                    {
                        //rdodrug.Checked = false;
                        rdodrugno.Checked = false;
                    }
                    else if (Convert.ToBoolean(dtr["isDrugAlrg"]) == true)
                    {
                        rdodrug.Checked = true;
                        //rdodrugno.Checked = true;
                    }
                    if (Convert.ToBoolean(dtr["HP"]) == false)
                    {
                        rdbHpNo.Checked = true;
                        rdbHpYes.Checked = false;
                    }
                    else if (Convert.ToBoolean(dtr["HP"]) == true)
                    {
                        rdbHpYes.Checked = true;
                        rdbHpNo.Checked = false;
                    }

                    if (Convert.ToBoolean(dtr["SENIOR_CITIZEN"]) == false)
                    {
                        rdbSeniorCitizenNo.Checked = true;
                        rdbSeniorCitizenYes.Checked = false;
                    }
                    else if (Convert.ToBoolean(dtr["SENIOR_CITIZEN"]) == true)
                    {
                        rdbSeniorCitizenYes.Checked = true;
                        rdbSeniorCitizenNo.Checked = false;
                    }


                    if (Convert.ToBoolean(dtr["QRENT_YN"]) == true)
                    {
                        rdbRentYes.Checked = true;
                        rdbRentNo.Checked = false;
                    }
                    else if (Convert.ToBoolean(dtr["QRENT_YN"]) == false)
                    {
                        rdbRentYes.Checked = false;
                        rdbRentNo.Checked = true;
                    }


                    if (Convert.ToChar(dtr["TA"]) == 'Y')
                    {
                        rdbTAyes.Checked = true;
                        rdbTAno.Checked = false;
                    }
                    else if (Convert.ToChar(dtr["TA"]) == 'N')
                    {
                        rdbTAno.Checked = true;
                        rdbTAyes.Checked = false;
                    }
                    if (Convert.ToChar(dtr["TA"]) == null)
                    {
                        rdbTAno.Checked = true;
                    }
                    if (Convert.ToBoolean(dtr["QUARTER"]) == true)
                    {
                        rdbQtrYes.Checked = true;
                        rdbQtrNo.Checked = false;
                    }
                    else if (Convert.ToBoolean(dtr["QUARTER"]) == false)
                    {
                        rdbQtrNo.Checked = true;
                        rdbQtrYes.Checked = false;
                    }
                    if (Convert.ToChar(dtr["PSTATUS"]) == 'Y')
                    {
                        rdbPayYes.Checked = true;
                        rdbPayNo.Checked = false;
                    }
                    else if (Convert.ToChar(dtr["PSTATUS"]) == 'N')
                    {
                        rdbPayNo.Checked = true;
                        rdbPayYes.Checked = false;

                    }

                    if (Convert.ToBoolean(dtr["EPF_EXTRA_STATUS"]) == false)
                    {
                        rdbExtraEPFYes.Checked = false;
                        rdbExtraEPFNo.Checked = true;
                    }
                    else if (Convert.ToBoolean(dtr["EPF_EXTRA_STATUS"]) == true)
                    {
                        rdbExtraEPFNo.Checked = false;
                        rdbExtraEPFYes.Checked = true;
                    }

                    ddlmaindeptname.SelectedValue = dtr["MAINDEPTNO"] == null ? string.Empty : dtr["MAINDEPTNO"].ToString();
                    ddlDAheadCalculation.SelectedValue = dtr["DA_HEADID"] == null ? string.Empty : dtr["DA_HEADID"].ToString();
                    imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=EMP";
                    imgEmpSign.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=EMPSIGN";
                    bool CheckApp = Convert.ToBoolean(objCommon.LookUp("PAYROLL_APPOINT", "isnull(IsI8Applicable,1)", "APPOINTNO=" + Convert.ToInt32(ddlAppointment.SelectedValue)));
                    if (CheckApp == true)
                    {
                        DivConPay.Visible = true;

                    }
                    else
                    {
                        DivConPay.Visible = false;

                    }

                    txtESICNo.Text = dtr["ESICNO"] == null ? string.Empty : dtr["ESICNO"].ToString();

                    if (Convert.ToBoolean(dtr["UA_STATUS"]) == false)
                    {
                        chkuserstatus.Checked = false;
                    }
                    else
                    {
                        chkuserstatus.Checked = true;
                    }
                    if (Convert.ToBoolean(dtr["IsBioAuthorityPerson"]) == false)  //23-03-2023
                    {
                        chkIsBioAuthorityPerson.Checked = false;
                    }
                    else
                    {
                        chkIsBioAuthorityPerson.Checked = true;
                    }
                }
                dtr.Close();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Invalid Record", this.Page);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowEmpDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDropDown()
    {
        try
        {
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            objCommon.FillDropDownList(ddlClassification, "PAYROLL_EMPLOYEE_CLASSIFICATION", "CLNO", "CLNAME", "", "");
            objCommon.FillDropDownList(ddlBankPlace, "PAYROLL_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
            objCommon.FillDropDownList(ddlTitle, "PAYROLL_TITLE", "TITLENO", "TITLE", "TITLENO > 0", "TITLENO ASC");
            objCommon.FillDropDownList(ddlBank, "PAYROLL_BANK", "BANKNO", "BANKNAME+' ('+ isnull(BRANCHNAME,'-')+')'", "BANKNO > 0", "BANKNO ASC");
            objCommon.FillDropDownList(ddlAppointment, "PAYROLL_APPOINT", "APPOINTNO", "APPOINT", "APPOINTNO > 0", "APPOINTNO ASC");
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPTNO ASC");
            objCommon.FillDropDownList(ddlDesigNature, "PAYROLL_DESIGNATURE", "DESIGNATURENO", "DESIGNATURE", "DESIGNATURENO > 0", "DESIGNATURENO ASC");
            objCommon.FillDropDownList(ddlPF, "PAYROLL_PF_MAST", "PFNO", "shortname", "PFNO > 0", "PFNO ASC");
            objCommon.FillDropDownList(ddlVacational, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO > 0", "STNO ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO > 0", "STAFFNO ASC");
            objCommon.FillDropDownList(ddlPayRule, "PAYROLL_RULE", "RULENO", "PAYRULE", "RULENO > 0", "RULENO ASC");
            objCommon.FillDropDownList(ddlQuarter, "PAYROLL_QUARTERMAS", "QTRNO", "QTRNAME", "QTRNO > 0", "QTRNO ASC");
            objCommon.FillDropDownList(ddlshiftno, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0 and COLLEGE_NO=" + CollegeNo, "SHIFTNAME");
            objCommon.FillDropDownList(ddlStatus, "PAYROLL_STATUS", "STATUSNO", "STATUS", "STATUSNO>0", "STATUS");

            objCommon.FillDropDownList(ddlDesignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "", "SUBDESIGNO ASC");

            objCommon.FillDropDownList(ddlNuDesig, "PAYROLL_NUDESIG", "NUDESIGNO", "NUDESIG", "", "NUDESIGNO ASC");

            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

            objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID in(3,4,5,6)", "USERTYPEID");

            objCommon.FillDropDownList(ddlEmpType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO > 0", "EMPTYPENO ASC");
            objCommon.FillDropDownList(ddlPGDept, "PAYROLL_PGCOURSEDEPARTMENT", "PGSUBDEPTNO", "PGSUBDEPT", "PGSUBDEPTNO > 0", "PGSUBDEPTNO ASC");

            objCommon.FillDropDownList(DdlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "COLLEGE_CODE>0", "BLOODGRPNAME");

            objCommon.FillDropDownList(ddlmaindeptname, "PAYROLL_MAINDEPT", "MAINDEPTNO", "MAINDEPT", "MAINDEPTNO > 0", "MAINDEPTNO ASC");
            objCommon.FillDropDownList(ddlDAheadCalculation, "DA_HEAD", "DA_HEADID", "DA_HEAD_DESCRIPTION", "DA_HEADID > 0", "DA_HEADID ASC");
            //if (ddlStaff.SelectedIndex != 0)
            //{

            //    objCommon.FillDropDownList(ddlNuDesig, "PAYROLL_NUDESIG", "NUDESIGNO", "NUDESIG", "STAFFNO=" + ddlStaff.SelectedValue, "NUDESIGNO ASC");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=empinfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=empinfo.aspx");
        }
    }

    private void ClearControls()
    {
        chkRetagecal.Checked = false;
        txtIFSCCode.Text = string.Empty;
        txtAlternateEmailId.Text = string.Empty;
        txtAlterPhoneNumber.Text = string.Empty;
        txtAge.Text = "0";
        isbusfac.Checked = false;
        ViewState["action"] = "add";
        txtIdNo.Text = string.Empty;
        txtIncrDate.Text = string.Empty;
        txtJoinDate.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtAccNo.Text = string.Empty;
        txtBasic.Text = string.Empty;
        txtBirthDate.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtMiddleName.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtPFNo.Text = string.Empty;
        txtPPFNo.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRetireDate.Text = string.Empty;
        txtSeqNo.Text = string.Empty;
        txtGradePay.Text = string.Empty;
        txtNationalUniqueIDNo.Text = string.Empty;
        txtPersonalFileNo.Text = string.Empty;
        ddlAppointment.SelectedIndex = 0;
        ddlPF.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlDesigNature.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        ddlNuDesig.SelectedIndex = 0;
        ddlPayRule.SelectedIndex = 0;
        ddlPayScale.SelectedIndex = 0;
        ddlQuarter.SelectedIndex = 0;
        ddlStaff.SelectedIndex = 0;
        ddlTitle.SelectedIndex = 0;
        ddlClassification.SelectedIndex = 0;
        ddlBankPlace.SelectedIndex = 0;
        ddlVacational.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlmaindeptname.SelectedIndex = 0;
        ddlDAheadCalculation.SelectedIndex = 0;
        rdotelMin.Checked = true;
        rdotelMinNo.Checked = false;
        rdodrug.Checked = true;
        rdodrugno.Checked = false;
        isCabFac.Checked = false;
        chkNEFT.Checked = false;
        rdbFN.Checked = true;
        rdbHpNo.Checked = true;
        rdbMale.Checked = true;
        rdbPayYes.Checked = true;
        rdbQtrYes.Checked = true;
        rdbRentNo.Checked = true;
        rdbTAyes.Checked = true;
        rdbQtrYes.Checked = true;
        imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
        imgEmpSign.ImageUrl = "~/images/sign11.jpg";
        GenerateIdno();
        txtRFIDno.Text = string.Empty;
        txtRFIDno.Text = string.Empty;
        txtSeqNo.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtMiddleName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtLocalAddress.Text = string.Empty;
        txtPermanentAddress.Text = string.Empty;
        txtEmailId.Text = string.Empty;
        txtRelievingDate.Text = string.Empty;
        txtmothername.Text = string.Empty;
        txtExpiryDtExt.Text = string.Empty;
        txtMaidenName.Text = string.Empty;
        txtHusbandName.Text = string.Empty;
        ddlTitle.SelectedIndex = 0;
        txtStatusDT.Text = string.Empty;
        ddlshiftno.SelectedIndex = 0;
        ddlPGDept.SelectedIndex = 0;
        ddlEmpType.SelectedIndex = 0;
        //txtIdNo.Text = string.Empty;
        txtEmployeeId.Text = string.Empty;
        txtUANNO.Text = string.Empty;
        txtConsPay.Text = "0";
        DivConPay.Visible = false;
        //txtFirstNameKannada.Text = string.Empty;
        //txtMiddleNameKannada.Text = string.Empty;
        //txtLastNameKannada.Text = string.Empty;
        //txtFatherNameKannada.Text = string.Empty;
        rdoMarried.Checked = true;
        rdounMarried.Checked = false;
        txtMaleChild.Text = string.Empty;
        txtFemaleChild.Text = string.Empty;
        txtcolRoom.Text = string.Empty;
        txtColIntcomNo.Text = string.Empty;
        txtDisplayQualification.Text = string.Empty;
        chkHandicap.Checked = false;
        ddlHandicap.SelectedIndex = 0;
        txtEmployement.Text = string.Empty;
        txtQuaterAltDate.Text = string.Empty;
        DdlBloodGroup.SelectedIndex = 0;
        txtESICNo.Text = string.Empty;
        chkexservicemen.Checked = false;  //Amol sawarkar 
        chkIsBioAuthorityPerson.Checked = false;
    }

    private void bindlist(string category, string searchtext)
    {
        try
        {
            EmpCreateController objECC = new EmpCreateController();
            string collegeno = Session["college_nos"].ToString();
            DataTable dt = objECC.RetrieveEmpDetails(searchtext.Trim(), category, collegeno);
            if (dt.Rows.Count > 0)
            {

                lvEmp.DataSource = dt;
                lvEmp.DataBind();
            }
            else
            {
                lvEmp.DataSource = null;
                lvEmp.DataBind();
                MessageBox("Invalid Record");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnkbut = sender as LinkButton;
        int idno = Convert.ToInt32(lnkbut.CommandArgument);
        string status = objCommon.LookUp("PAYROLL_EMPMAS", "ACTIVE", "IDNO=" + idno);
        if (status == "Y")
        {

            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Response.Redirect(url + "&id=" + lnk.CommandArgument);
        }
        else
        {
            //objCommon.DisplayMessage("Employee is Inactive", this);
            MessageBox("Employee is Inactive");
        }
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {

        //objCommon.FillDropDownList(ddlDesignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "STAFFNO=" + ddlStaff.SelectedValue, "SUBDESIGNO ASC");
        //objCommon.FillDropDownList(ddlNuDesig, "PAYROLL_NUDESIG", "NUDESIGNO", "NUDESIG", "STAFFNO=" + ddlStaff.SelectedValue, "NUDESIGNO ASC");
        //EmpCreateController objECC = new EmpCreateController();
        //DateTime birthdate = DateTime.MinValue;
        //if (!txtBirthDate.Text.Trim().Equals(string.Empty))
        //{
        //    birthdate = Convert.ToDateTime(txtBirthDate.Text);
        //    txtRetireDate.Text = objECC.RetirementDate(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(birthdate)).ToString("dd/MM/yyyy");
        //    DateTime dt = Convert.ToDateTime(txtRetireDate.Text.Trim());
        //    int day = dt.Day;
        //    int month = dt.Month;
        //    string mon = Convert.ToString(dt.Month);
        //    int year = dt.Year;
        //    string yr = Convert.ToString(dt.Year);
        //    string date = objCommon.LookUp("payroll_monfile", "TDAY", "MCODE =" + month);
        //    if (day != 1)
        //    {
        //        txtRetireDate.Text = date + "/" + 0 + mon + "/" + yr;
        //    }
        //    else
        //    {
        //        txtRetireDate.Text = objECC.RetirementDate(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(birthdate)).ToString("dd/MM/yyyy");
        //    }
        //}

        EmpCreateController objECC = new EmpCreateController();

        DateTime birthdate = DateTime.MinValue;
        if (!txtBirthDate.Text.Trim().Equals(string.Empty))
        {
            DateTime RetDate = DateTime.MinValue;
            birthdate = Convert.ToDateTime(txtBirthDate.Text);
            RetDate = Convert.ToDateTime(objECC.RetirementDateNew(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(birthdate)).ToString("dd/MM/yyyy"));
            if (RetDate == Convert.ToDateTime("9999-12-31"))
            {
            }
            else
            {
                txtRetireDate.Text = Convert.ToString(RetDate);
            }
            //birthdate = Convert.ToDateTime(txtBirthDate.Text);
            //txtRetireDate.Text = objECC.RetirementDate(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(birthdate)).ToString("dd/MM/yyyy");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PayRoll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            //@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",
            url += "&param=@P_IDNO=" + Convert.ToInt32(txtIdNo.Text.Trim()) + ",username=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",IP=" + IP;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportBusList(string reportTitle, string rptFileName)
    {
        try
        {

            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            url += "&param=@P_COLLEGE_NO=" + ddlCollege.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.ShowReportEmployeeAbstractSalary() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlPayRule_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillScale(Convert.ToInt32(ddlPayRule.SelectedValue));

    }

    protected void ddlPayScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGradePay.Text = objCommon.LookUp("payroll_scale", "gradepay", "scaleno=" + Convert.ToInt32(ddlPayScale.SelectedValue));
    }

    protected void ddlAppointmnet_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool CheckApp = Convert.ToBoolean(objCommon.LookUp("PAYROLL_APPOINT", "isnull(IsI8Applicable,0)", "APPOINTNO=" + Convert.ToInt32(ddlAppointment.SelectedValue)));
        if (CheckApp == true)
        {
            DivConPay.Visible = true;

        }
        else
        {
            DivConPay.Visible = false;

        }
    }

    private void FillShiftno(int Collegeno)
    {
        objCommon.FillDropDownList(ddlshiftno, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0 and COLLEGE_NO=" + Collegeno, "SHIFTNAME");
    }
    private void FillScale(int roleno)
    {
        try
        {
            objCommon.FillDropDownList(ddlPayScale, "PAYROLL_SCALE", "SCALENO", "SCALE", "SCALENO > 0 AND ruleno=" + roleno, "SCALENO ASC");
            txtGradePay.Text = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.FillScale-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void IncrementDate()
    {
        DateTime dt = Convert.ToDateTime(txtJoinDate.Text.Trim());
        int year = dt.Year + 1;
        string yr = Convert.ToString(year);
        int day = dt.Day;
        string dy = Convert.ToString(day);
        int month = dt.Month;
        string mon = Convert.ToString(month);
        txtIncrDate.Text = dy + "/" + mon + "/" + yr;

    }

    protected void txtPFNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPFNo.Text.Trim() != string.Empty)
            {
                //  txtPFNo.Text.Split("/");
                string gpfcode;

                gpfcode = objCommon.LookUp("PAYROLL_EMPMAS", "case when '" + txtPFNo.Text.Trim() + "' Not like '%/%'  then '" + txtPFNo.Text.Trim() + "' else SUBSTRING( '" + txtPFNo.Text.Trim() + "' , LEN('" + txtPFNo.Text.Trim() + "') -  CHARINDEX('/',REVERSE('" + txtPFNo.Text.Trim() + "')) + 2  , LEN('" + txtPFNo.Text.Trim() + "')  ) end", "IDNO=1");
                if (gpfcode != string.Empty)
                {
                    txtEmployeeId.Text = gpfcode;
                }
                else
                {

                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.txtPFNo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    

    protected void txtBirthDate_TextChanged(object sender, EventArgs e)
    {
        //EmpCreateController objECC = new EmpCreateController();
        //DateTime RetDate = DateTime.MinValue;
        //DateTime birthdate = DateTime.MinValue;
        //DateTime selectedBirthData = Convert.ToDateTime(txtBirthDate.Text);
        //if (selectedBirthData > DateTime.Now)
        //{
        //    objCommon.DisplayUserMessage(this.Page, "You cannot select a day future than today!", this.Page);
        //    txtBirthDate.Text = string.Empty;
        //    return;
        //}

        //if (ddlStaff.SelectedValue != "0")
        //{
        //    if (!txtBirthDate.Text.Trim().Equals(string.Empty))
        //    {
        //        //  DateTime RetDate = DateTime.MinValue;
        //        birthdate = Convert.ToDateTime(txtBirthDate.Text);
        //        RetDate = Convert.ToDateTime(objECC.RetirementDate(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(birthdate)).ToString("dd/MM/yyyy"));
        //        if (RetDate == Convert.ToDateTime("9999-12-31"))
        //        {
        //        }
        //        else
        //        {
        //            txtRetireDate.Text = Convert.ToString(RetDate);
        //        }
        //    }
        //}

        if (!txtBirthDate.Text.Trim().Equals(string.Empty))
        {
            DateTime dob = Convert.ToDateTime(txtBirthDate.Text);
            DateTime PresentYear = DateTime.Now;

            TimeSpan ts = PresentYear - dob;
            int Age = ts.Days / 365;

            txtAge.Text = Age.ToString();
        }
        else
        {
            txtAge.Text = "0";
        }
    }

    protected void txtJoinDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtBirthDate.Text != string.Empty)
        //{
        //    DateTime dt_dateofBirth = Convert.ToDateTime(txtBirthDate.Text);
        //    DateTime dt_joiningdt = Convert.ToDateTime(txtJoinDate.Text);
        //    if (dt_dateofBirth > dt_joiningdt)
        //    {
        //        objCommon.DisplayMessage(this.Page, "Joining date should be greater than Birth Date", this.Page);
        //        txtJoinDate.Text = string.Empty;

        //        return;
        //    }
        //}

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlTitle.SelectedItem.Text == "Please Select")
            {
                MessageBox("Please select Title");
                return;
            }
            //added by suraj on dt 30-05-2016
            int idnocount = 0;
            bool chklockunlock;
            idnocount = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COUNT(1)", "IDNO=" + Convert.ToInt32(txtIdNo.Text)));
            if (idnocount == 0)
            {
                chklockunlock = false;
            }
            else
            {
                chklockunlock = Convert.ToBoolean(objCommon.LookUp("PAYROLL_EMPMAS", "ISNULL(EMPLOYEE_LOCK,1)", "IDNO=" + Convert.ToInt32(txtIdNo.Text)));
            }

            if (idnocount == 0 || chklockunlock == false)
            {

                string strSex = string.Empty;
                string dayStatus = string.Empty;
                string taStatus = string.Empty;

                EmpCreateController objECC = new EmpCreateController();
                EmpMaster objEM = new EmpMaster();
                PayMaster objPM = new PayMaster();
                ITMaster objIT = new ITMaster();
                int RFID = 0;
                if (!txtRFIDno.Text.Trim().Equals(string.Empty)) RFID = Convert.ToInt32(txtRFIDno.Text);

                //;TO CHECK RECORD EXISIT FOR RFIDNO OR NOT
                int chkPFILENO = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "COUNT(1)", "P.PSTATUS='Y' AND E.PFILENO ='" + txtPersonalFileNo.Text.Trim() + "' AND E.IDNO <> " + Convert.ToInt32(txtIdNo.Text) + " "));

                //if (chkPFILENO > 0)
                //{
                //    // objCommon.DisplayMessage("RFIDNO already exists!", this.Page);
                //    MessageBox("Staff ID No.already exists!");
                //    return;
                //}

                //;TO CHECK RECORD EXISIT FOR RFIDNO OR NOT
                //int chkRFIDNO = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "COUNT(1)", "P.PSTATUS='Y' AND E.RFIDNO =" + Convert.ToInt32(txtRFIDno.Text) + " AND E.IDNO <> " + Convert.ToInt32(txtIdNo.Text) + " "));

                //if (chkRFIDNO > 0)
                //{
                //    // objCommon.DisplayMessage("RFIDNO already exists!", this.Page);
                //    MessageBox("BioId already exists!");
                //    return;
                //}
                //;TO CHECK RECORD EXISIT FOR RFIDNO OR NOT END
                if (!txtIdNo.Text.Trim().Equals(string.Empty)) objEM.IDNO = Convert.ToInt32(txtIdNo.Text);
                objEM.TITLE = ddlTitle.SelectedItem.Text;
                objEM.FNAME = txtFirstName.Text.Trim();
                objEM.MNAME = txtMiddleName.Text.Trim();
                objEM.LNAME = txtLastName.Text.Trim();
                objEM.FATHERNAME = txtFatherName.Text.Trim();


                //UNICODE
                //objEM.FNAME_UNICODE = txtFirstNameKannada.Text;
                //objEM.MNAME_UNICODE = txtMiddleNameKannada.Text.Trim();
                //objEM.LNAME_UNICODE = txtLastNameKannada.Text.Trim();
                //objEM.FATHERNAME_UNICODE = txtFatherNameKannada.Text.Trim();


                //if (!txtSeqNo.Text.Trim().Equals(string.Empty)) objEM.SEQ_NO = Convert.ToInt32(txtSeqNo.Text);
                if (txtSeqNo.Text.Trim().Equals(string.Empty))
                {
                    objEM.SEQ_NO = 0;
                }
                else
                {
                    objEM.SEQ_NO = Convert.ToInt32(txtSeqNo.Text);
                }
                if (!txtBirthDate.Text.Trim().Equals(string.Empty)) objEM.DOB = Convert.ToDateTime(txtBirthDate.Text);
                if (!txtIncrDate.Text.Trim().Equals(string.Empty)) objEM.DOI = Convert.ToDateTime(txtIncrDate.Text);
                if (!txtJoinDate.Text.Trim().Equals(string.Empty)) objEM.DOJ = Convert.ToDateTime(txtJoinDate.Text);
                if (!txtRetireDate.Text.Trim().Equals(string.Empty)) objEM.RDT = Convert.ToDateTime(txtRetireDate.Text);

                if (!txtRelievingDate.Text.Trim().Equals(string.Empty)) objEM.RELIEVINGDATE = Convert.ToDateTime(txtRelievingDate.Text);
                if (!txtExpiryDtExt.Text.Trim().Equals(string.Empty)) objEM.EXPDATEOFEXT = Convert.ToDateTime(txtExpiryDtExt.Text);


                objEM.SUBDEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
                objEM.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);

                objEM.CLNO = Convert.ToInt32(ddlClassification.SelectedValue);
                objEM.BANKCITYNO = Convert.ToInt32(ddlBankPlace.SelectedValue);
                objEM.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
                objEM.BANKACC_NO = txtAccNo.Text;
                objEM.IFSC_CODE = txtIFSCCode.Text;
                objEM.STAFFNO = Convert.ToInt32(ddlStaff.SelectedValue);
                objEM.STNO = Convert.ToInt32(ddlVacational.SelectedValue);
                objEM.PFNO = Convert.ToInt32(ddlPF.SelectedValue);
                objEM.PPF_NO = txtPPFNo.Text.Trim();
                objEM.PAN_NO = txtPanNo.Text.Trim();
                objEM.GPF_NO = txtPFNo.Text.Trim();
                objEM.REMARK = txtRemark.Text.Trim();
                objEM.PFILENO = txtPersonalFileNo.Text.Trim();
                objEM.NUNIQUEID = txtNationalUniqueIDNo.Text.Trim();
                objEM.SHIFTNO = Convert.ToInt32(ddlshiftno.SelectedValue);
                objEM.NUDESIGNO = Convert.ToInt32(ddlNuDesig.SelectedValue);
                objEM.MAIDENNAME = txtMaidenName.Text.ToUpper().Trim();
                objEM.HUSBANDNAME = txtHusbandName.Text.ToUpper().Trim();

                if (rdbFN.Checked == true)
                    dayStatus = "FN";
                else
                    dayStatus = "AN";

                if (rdbTAyes.Checked == true)
                    taStatus = "Y";
                else
                    taStatus = "N";

                objEM.SEX = rdbMale.Checked ? 'M' : 'F';
                objEM.ANFN = dayStatus;
                objEM.HP = rdbHpYes.Checked ? true : false;
                objEM.IS_SHIFT_MANAGMENT = isShiftManagement.Checked ? true : false;
                objEM.SENIOR_CIIZEN = rdbSeniorCitizenYes.Checked ? true : false;
                objEM.QRENT_YN = rdbRentYes.Checked ? true : false;
                objEM.QUARTER = rdbQtrYes.Checked ? true : false;
                objEM.EPF_EXTRA = rdbExtraEPFYes.Checked ? true : false;
                objEM.QTRNO = Convert.ToInt32(ddlQuarter.SelectedValue);
                objEM.COLLEGE_CODE = Session["colcode"].ToString();
                objEM.SACTIVE = "Y";
                objEM.IsNEFT = chkNEFT.Checked ? true : false;
                objEM.IsBusFac = isbusfac.Checked ? true : false;
                objEM.IsCabfac = isCabFac.Checked ? true : false;
                objEM.IsTelguMin = rdotelMin.Checked ? true : false;
                objEM.IsDrugAlrg = rdodrug.Checked ? true : false;

                objEM.STATUSNO = Convert.ToInt32(ddlStatus.SelectedValue);
                if (!txtStatusDT.Text.Trim().Equals(string.Empty)) objEM.STDATE = Convert.ToDateTime(txtStatusDT.Text);

                if (!txtPhoneNumber.Text.Trim().Equals(string.Empty))
                {
                    objEM.PHONENO = txtPhoneNumber.Text;
                }
                else
                {
                    objEM.PHONENO = "0";
                }


                if (!txtAlterPhoneNumber.Text.Trim().Equals(string.Empty))
                {
                    objEM.ALTERNATEPHONENO = txtAlterPhoneNumber.Text;
                }
                else
                {
                    objEM.ALTERNATEPHONENO = "0";
                }

                objEM.RESADD1 = txtLocalAddress.Text;
                objEM.TOWNADD1 = txtPermanentAddress.Text;
                objEM.EMAILID = txtEmailId.Text;
                objEM.ALTERNATEEMAILID = txtAlternateEmailId.Text;

                objPM.IDNO = Convert.ToInt32(txtIdNo.Text);
                if (txtSeqNo.Text.Trim().Equals(string.Empty))
                {
                    objPM.SEQ_NO = 0;
                }
                else
                {
                    objPM.SEQ_NO = Convert.ToInt32(txtSeqNo.Text);
                }
                objPM.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
                objPM.DESIGNATURENO = Convert.ToInt32(ddlDesigNature.SelectedValue);
                objPM.SUBDEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
                objPM.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);

                if (!txtJoinDate.Text.Trim().Equals(string.Empty)) objPM.DOJ = Convert.ToDateTime(txtJoinDate.Text);
                objPM.PAYRULE = Convert.ToInt32(ddlPayRule.SelectedValue);

                //if (!txtBasic.Text.Trim().Equals(string.Empty))
                //{
                //    objPM.BASIC = 0;
                //}
                //else
                //{
                //    objPM.BASIC = Convert.ToInt32(txtBasic.Text);
                //}
                //if (!txtBasic.Text.Trim().Equals(string.Empty))
                //{
                //    objPM.OBASIC = 0;
                //}
                //else
                //{
                //    objPM.OBASIC = Convert.ToInt32(txtBasic.Text);
                //}

                if (!txtBasic.Text.Trim().Equals(string.Empty)) objPM.BASIC = Convert.ToInt32(txtBasic.Text);
                if (!txtBasic.Text.Trim().Equals(string.Empty)) objPM.OBASIC = Convert.ToInt32(txtBasic.Text);

                objPM.SCALENO = Convert.ToInt32(ddlPayScale.SelectedValue);
                objPM.APPOINTNO = Convert.ToInt32(ddlAppointment.SelectedValue);
                objPM.HP = rdbHpYes.Checked ? true : false;
                objPM.TA = rdbTAyes.Checked ? 'Y' : 'N';
                objPM.PSTATUS = rdbPayYes.Checked ? 'Y' : 'N';
                objPM.REMARK = txtRemark.Text;
                objPM.COLLEGE_CODE = Session["colcode"].ToString();
                if (!txtGradePay.Text.Trim().Equals(string.Empty)) objPM.GRADEPAY = Convert.ToDecimal(txtGradePay.Text);
                if (!txtIdNo.Text.Trim().Equals(string.Empty)) objIT.IDNO = Convert.ToInt32(txtIdNo.Text);
                objIT.COLLEGE_CODE = Session["colcode"].ToString();

                objEM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

                objEM.EMPLOYEE_LOCK = true;

                string mothername = Convert.ToString(txtmothername.Text);
                objEM.UA_TYPE = Convert.ToInt32(ddlUserType.SelectedValue);

                objEM.EMPTYPENO = Convert.ToInt32(ddlEmpType.SelectedValue);
                objEM.PGDEPTNO = Convert.ToInt32(ddlPGDept.SelectedValue);

                // to check for user identification who save employee info.
                objEM.UA_NO = Convert.ToInt32(Session["userno"]);
                objEM.USER_UATYPE = Convert.ToInt32(Session["usertype"]);

                objEM.UAN1 = txtUANNO.Text.ToUpper().Trim();
                objEM.EmployeeId = txtEmployeeId.Text.ToUpper().Trim();

                bool CheckApp = Convert.ToBoolean(objCommon.LookUp("PAYROLL_APPOINT", "isnull(IsI8Applicable,0)", "APPOINTNO=" + Convert.ToInt32(ddlAppointment.SelectedValue)));
                
                if (CheckApp == true)
                {
                    objPM.I8 = Convert.ToDecimal(txtConsPay.Text);
                }
                else
                {
                    objPM.I8 = 0;
                }
              
               
                objEM.Age = Convert.ToInt32(txtAge.Text);
                objEM.BLOODGRPNO = Convert.ToInt32(DdlBloodGroup.SelectedValue);
                objEM.MaritalStatus = rdoMarried.Checked ? true : false;
                //objEM.MaritalStatus = chkIsMarried.Checked ? true : false;
                if (rdoMarried.Checked == true)
                {
                    if (txtMaleChild.Text == string.Empty) { txtMaleChild.Text = "0"; }
                    if (txtFemaleChild.Text == string.Empty) { txtFemaleChild.Text = "0"; }
                    objEM.ChildMale = Convert.ToInt32(txtMaleChild.Text);
                    objEM.ChildFemale = Convert.ToInt32(txtFemaleChild.Text);

                }
                else
                {
                    objEM.ChildMale = 0;
                    objEM.ChildFemale = 0;
                }

                objEM.IsPhysicallyChallenged = chkHandicap.Checked ? true : false;

                if (chkHandicap.Checked == true)
                {
                    objEM.HandicapTypeID = Convert.ToInt32(ddlHandicap.SelectedValue);
                }
                else
                {
                    objEM.HandicapTypeID = 0;
                }
                objEM.CollegeRoomNo = txtcolRoom.Text.Trim();
                objEM.CollegeIntercomNo = txtColIntcomNo.Text.Trim();
                objEM.QualForDisplay = txtDisplayQualification.Text.Trim();
                objEM.Employment = txtEmployement.Text.Trim();
                //  objEM.QuartersAllotmentDate =Convert.ToDateTime(txtQuaterAltDate.Text.Trim());
                if (!txtQuaterAltDate.Text.Trim().Equals(string.Empty)) objEM.QuartersAllotmentDate = Convert.ToDateTime(txtQuaterAltDate.Text);
                objEM.ESICNO = txtESICNo.Text.Trim();
                if (ddlmaindeptname.SelectedIndex > 0)
                {
                    objEM.MAINDEPTNO = Convert.ToInt32(ddlmaindeptname.SelectedValue);

                }
                else
                {
                    objEM.MAINDEPTNO = 0;
                }

                if (ddlDAheadCalculation.SelectedIndex > 0)
                {
                    objEM.DAHEADID = Convert.ToInt32(ddlDAheadCalculation.SelectedValue);
                }
                else
                {
                    objEM.DAHEADID = 0;
                }
                if (chkuserstatus.Checked   == true)
                {
                   
                    objEM.UserStatus = false;
                }
                else
                {
                    objEM.UserStatus = true;
                }

                //Amol sawarkar
                objEM.ExServicemen = chkexservicemen.Checked ? true : false;

                // Amol sawarkar 01-03-2022
                if (chkexservicemen.Checked == true)
                {
                    objEM.ExServicemen = true;
                }
                else
                {
                    objEM.ExServicemen = false;
                }
                objEM.IsBioAuthorityPerson = chkIsBioAuthorityPerson.Checked ? true : false;  // 23-03-2023
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //by default zero is sent as it is not a compulsaory field
                        int ckeckPFILENO = 0;// Convert.ToInt32(objCommon.LookUp("payroll_empmas", "count(*)", "PFILENO='" + txtPersonalFileNo.Text + "'"));
                        //int checkNUNIQUEID = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "count(*)", "NUNIQUEID='" + txtNationalUniqueIDNo.Text + "'")); ;
                        //if (ckeckPFILENO == 0 && checkNUNIQUEID == 0)
                        int chkPFILENO1 = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E", "COUNT(1)", "E.PFILENO ='" + txtPersonalFileNo.Text.Trim() + "'"));
                        if (chkPFILENO1 > 0)
                        {
                            objCommon.DisplayMessage("Employee Code already exists!", this.Page);
                            //  MessageBox("Staff ID No.already exists!");
                            return;
                        }
                        if (txtPanNo.Text != "")
                        {
                            int chkPanNo1 = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E", "COUNT(1)", "E.PAN_NO ='" + txtPanNo.Text.Trim() + "'"));
                            if (chkPanNo1 > 0)
                            {
                                objCommon.DisplayMessage("Pan Card No already exists!", this.Page);
                                //  MessageBox("Staff ID No.already exists!");
                                return;
                            }
                        }
                        else
                        {

                        }
                        if (ckeckPFILENO == 0)
                        {
                            objEM.Photo = objCommon.GetImageData(fuplEmpPhoto);
                            objEM.PhotoSign = objCommon.GetImageData(fuplEmpSign);

                            CustomStatus cs = (CustomStatus)objECC.AddNewEmployee_CRES(objEM, objPM, objIT, RFID, mothername);

                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                lblMsg.Text = "Record Saved Successfully";
                                //objCommon.DisplayMessage("Record Saved Successfully", this);
                                MessageBox("Record Saved Successfully");
                                ViewState["action"] = "add";
                            }

                            else
                            {
                                //lblMsg.Text = "Transaction Failed...";
                                MessageBox("Transaction Failed...");
                                ViewState["action"] = "add";
                                return;
                            }

                        }

                        else
                        {

                            //if (ckeckPFILENO > 0)
                            //objCommon.DisplayMessage("Personal file number already exists", this);
                            MessageBox("StaffId number already exists");
                            //else
                            //bjCommon.DisplayMessage("National unique ID number already exists", this);

                            return;
                        }

                    }
                    else if (ViewState["action"].ToString().Equals("edit"))
                    {

                        //int ckeckPFILENO = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "count(*)","PFILENO='" + txtPersonalFileNo.Text + "' and idno <> "+Convert.ToInt32(txtIdNo.Text)));
                        // int checkNUNIQUEID = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "count(*)", "NUNIQUEID='" + txtNationalUniqueIDNo.Text + "' and idno <> " + Convert.ToInt32(txtIdNo.Text)));
                        //if (ckeckPFILENO == 0 && checkNUNIQUEID == 0)
                        //if (ckeckPFILENO == 0)
                        //{

                        //int chkPanNo1 = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E", "COUNT(1)", "E.PAN_NO ='" + txtPanNo.Text.Trim() + "'"));
                        //if (chkPanNo1 > 0)
                        //{
                        //    objCommon.DisplayMessage("Pan Card No already exists!", this.Page);
                        //    //  MessageBox("Staff ID No.already exists!");
                        //    return;
                        //}

                        if (fuplEmpPhoto.HasFile)
                        {
                            objEM.Photo = objCommon.GetImageData(fuplEmpPhoto);
                        }
                        else
                        {
                            objEM.Photo = null;
                        }

                        if (fuplEmpSign.HasFile)
                        {
                            objEM.PhotoSign = objCommon.GetImageData(fuplEmpSign);
                        }
                        else
                        {
                            objEM.PhotoSign = null;
                        }

                        string Name = ddlTitle.SelectedItem.Text + " " + txtFirstName.Text.Trim() + " " + txtMiddleName.Text.Trim() + " " + txtLastName.Text.Trim();
                        string fromMailId = objCommon.LookUp("Payroll_Email_Config", "FromEmailId", "");
                        string frommailPass = objCommon.LookUp("Payroll_Email_Config", "FromEmailPass", "");
                        string ToMailid = objCommon.LookUp("Payroll_Email_Config", "ToEmailId", "");

                        //string DOBDATE = objCommon.LookUp("payroll_empmas", "convert(datetime,DOB,103)", "idno=" + objEM.IDNO);
                        //if (DOBDATE != txtBirthDate.Text.Trim())
                        //{
                        //    sendmail(ToMailid, "DOB CHANGES", "" + Name + " DOB CHANGE IN ERP " + DOBDATE + " TO " + txtBirthDate.Text.Trim() + " ");
                        //}

                        CustomStatus cs = (CustomStatus)objECC.UpdateEmployee_CRES(objEM, objPM, objIT, RFID, mothername);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            lblMsg.Text = "Record Updated Successfully";
                            //objCommon.DisplayMessage("Record Updated Successfully", this);
                            MessageBox("Record Updated Successfully");
                            ViewState["action"] = "edit";
                            //ClearControls();
                        }
                        else
                        {
                            //lblMsg.Text = "Transaction Failed...";
                            MessageBox("Transaction Failed...");
                            ViewState["action"] = "edit";
                            return;
                        }


                        //}
                        //else
                        //{
                        //    //if (ckeckPFILENO > 0)
                        //        objCommon.DisplayMessage("Personal file number already exists", this);
                        //   //else
                        //        //objCommon.DisplayMessage("National unique ID number already exists", this);

                        //    return;
                        //}
                    }
                }

                ClearControls();
                GetEmployeeCode();

                //GenerateIdno();
            }//ADDED BY SURAJ 
            else
            {
                //objCommon.DisplayMessage("Employee Details are locked. Please unlock it!", this);
                MessageBox("Employee Details are locked. Please unlock it!");
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void sendmail(string toEmailId, string Sub, string body)
    {

        try
        {

            var fromAddress = new MailAddress("erpadmin@svce.ac.in", "");
            var toAddress = new MailAddress(toEmailId, "prashant");
            string fromPassword = "erpadmin@$vcpwd";
            //string subject = "Hi This Is Prashant ";
            //string body = "Your Registration Succesfull. Pls login again.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = Sub,
                Body = body,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                IsBodyHtml = true
            })
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(message);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }



    }




    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("EMployee_Report", "rptEmployee_Report.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnBusFac_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportBusList("EMployee_Report", "rptEmployee_Report.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect(Request.Url.ToString());
            ClearControls();
            GetEmployeeCode();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

    protected void chkHandicap_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHandicap.Checked == true)
        {
            divHandicapList.Visible = true;
        }
        else
        {
            ddlHandicap.SelectedIndex = 0;
            divHandicapList.Visible = false;
        }
    }

    protected void chkRetirementAgeCal_CheckedChanged(object sender, EventArgs e)
    {
        if (!txtBirthDate.Text.Trim().Equals(string.Empty) && chkRetagecal.Checked == true)
        {
            EmpCreateController objECC = new EmpCreateController();
            DateTime RetDate = DateTime.MinValue;
            DateTime birthdate = DateTime.MinValue;
            DateTime selectedBirthData = Convert.ToDateTime(txtBirthDate.Text);
            if (selectedBirthData > DateTime.Now)
            {
                objCommon.DisplayUserMessage(this.Page, "You cannot select a day future than today!", this.Page);
                txtBirthDate.Text = string.Empty;
                return;
            }
            if (ddlStaff.SelectedValue != "0")
            {
                if (!txtBirthDate.Text.Trim().Equals(string.Empty))
                {
                    //  DateTime RetDate = DateTime.MinValue;
                    birthdate = Convert.ToDateTime(txtBirthDate.Text);
                    RetDate = Convert.ToDateTime(objECC.RetirementDateNew(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(birthdate)).ToString("dd/MM/yyyy"));
                    if (RetDate == Convert.ToDateTime("9999-12-31"))
                    {
                    }
                    else
                    {
                        txtRetireDate.Text = Convert.ToString(RetDate);
                    }
                }
            }

            if (!txtBirthDate.Text.Trim().Equals(string.Empty))
            {
                DateTime dob = Convert.ToDateTime(txtBirthDate.Text);
                DateTime PresentYear = DateTime.Now;

                TimeSpan ts = PresentYear - dob;
                int Age = ts.Days / 365;

                txtAge.Text = Age.ToString();
            }
        }
        else
        {
            txtRetireDate.Text = string.Empty;
        }
        
    }

    protected void chkIsMarried_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoMarried.Checked == true)
        {
            txtMaleChild.Enabled = true;
            txtFemaleChild.Enabled = true;
        }
        else
        {
            txtMaleChild.Text = string.Empty;
            txtFemaleChild.Text = string.Empty;
            txtMaleChild.Enabled = false;
            txtFemaleChild.Enabled = false;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);

        objCommon.FillDropDownList(ddlshiftno, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SHIFTNO>0 and COLLEGE_NO=" + CollegeNo, "SHIFTNAME");
    }
}