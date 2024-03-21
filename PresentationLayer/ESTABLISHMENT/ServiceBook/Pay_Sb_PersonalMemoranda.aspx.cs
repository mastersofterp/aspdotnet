using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_PersonalMemoranda : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    static string title, fname, mname, lname, dob, doj = string.Empty;
    static bool Istitle, Isfname, Ismname, Islname, IsDOB, IsDOJ,IsDesignation,IsDepartment = false;
    static int  designation, department = 0;

    public int _idnoEmp;
    public int _usertype;

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
                // CheckPageAuthorization();
            }
           
            ViewState["_idnoEmp"] = "0";
           // Session["serviceIdNo"] = "0";
            FillDropDown();           
        }


        //DropDownList ddlempno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //if (ddlempno.SelectedIndex > 0)
        //    _idnoEmp = Convert.ToInt16(ddlempno.SelectedValue);

        if (Session["usertype"] != null)
        {
            _usertype = Convert.ToInt32(Session["usertype"].ToString().Trim());
        }

        if (_usertype != 1)
        {
            ddlTitle.Enabled = false;
            txtFirstName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtLastName.Enabled = false;
            txtFatherName.Enabled = false;
            txtMotherName.Enabled = false;
            ddlDesignation.Enabled = false;
            ddlDept.Enabled = false;
            txtFnAn.Enabled = false;
            txtDateofAppointment.Enabled = false;
            //Image2.Attributes.Add("readonly", "true");
            Image2.Visible = false;
        }
        else
        {
            ddlTitle.Enabled = true;
            txtFirstName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtLastName.Enabled = false;
            txtFatherName.Enabled = true;
            txtMotherName.Enabled = true;
            ddlDesignation.Enabled = true;
            ddlDept.Enabled = true;
            txtFnAn.Enabled = true;
            txtDateofAppointment.Enabled = true;
            //Image2.Attributes.Add("readonly", "false");
            Image2.Visible = true;
            
        }

        
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }

        if (_idnoEmp != 0)
        {
            if (ViewState["_idnoEmp"].ToString() != _idnoEmp.ToString())
            {
                ShowDetails(_idnoEmp);
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_PersonalMemoranda.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PersonalMemoranda.aspx");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            EmpMaster objEmpMas = new EmpMaster();
            objEmpMas.IDNO = _idnoEmp;
            objEmpMas.FATHERNAME = txtFatherName.Text;
            // objEmpMas.DOB =Convert.ToDateTime(txtBirthDate.Text);
            //if (!txtBirthDate.Text.Trim().Equals(string.Empty)) objEmpMas.DOB = Convert.ToDateTime(txtBirthDate.Text);
            //objEmpMas.DOJ = Convert.ToDateTime(txtDateofAppointment.Text); 		
            objEmpMas.HEIGHT = txtHeight.Text;
            objEmpMas.IDMARK1 = txtMarksofIdentification1.Text;
            objEmpMas.IDMARK2 = txtMarksofIdentification2.Text;
            objEmpMas.RESADD1 = txtPresentAddress.Text;
            objEmpMas.TOWNADD1 = txtPermanentAddress.Text;
            string mothername = Convert.ToString(txtMotherName.Text);
            objEmpMas.PHONENO = txtPhoneNumber.Text;
            objEmpMas.EMAILID = txtEmail.Text;
            objEmpMas.CASTENO = Convert.ToInt32(ddlCaste.SelectedValue);
            objEmpMas.CATEGORYNO = Convert.ToInt32(ddlCategory.SelectedValue);
            objEmpMas.RELIGIONNO = Convert.ToInt32(ddlReligion.SelectedValue);
            objEmpMas.NATIONALITYNO = Convert.ToInt32(ddlNationality.SelectedValue);
            objEmpMas.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objEmpMas.ACATNO = Convert.ToInt32(ddlAppointmentCategory.SelectedValue);
            objEmpMas.AUTHENREF = txtAuthenticated.Text;
            objEmpMas.ANFN = txtFnAn.Text;

            objEmpMas.STATUSNO = Convert.ToInt32(ddlStatus.SelectedValue);
            if (!txtStatusDT.Text.Trim().Equals(string.Empty)) objEmpMas.STDATE = Convert.ToDateTime(txtStatusDT.Text);

            //=====================================
            if (Istitle == false)
            {
                objEmpMas.TITLE = ddlTitle.SelectedItem.Text; 
            }
            else
            {
                objEmpMas.TITLE = title;
            }
            if (Isfname == false)
            {
                objEmpMas.FNAME = fname;
            }
            else
            {
                objEmpMas.FNAME = txtFirstName.Text;
            }
            if (Ismname == false)
            {
                objEmpMas.MNAME = mname;
            }
            else
            {
                objEmpMas.MNAME = txtMiddleName.Text;
            }
            if (Islname == false)
            {
                objEmpMas.LNAME = lname;
            }
            else
            {
                objEmpMas.LNAME = txtLastName.Text;
            }
            if (IsDOB == false)
            {
                objEmpMas.DOB = dob.Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(dob.Trim());//dob.ToString.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtBirthDate.Text.Trim()); ;
            }
            else
            {
                objEmpMas.DOB = txtBirthDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtBirthDate.Text.Trim());

            }
            // objEmpMas.DOJ = Convert.ToDateTime(txtDateofAppointment.Text); 		
            if (IsDOJ == false)
            {
                objEmpMas.DOJ = doj.Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(doj.Trim());//dob.ToString.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtBirthDate.Text.Trim()); ;
            }
            else
            {
                objEmpMas.DOJ = txtDateofAppointment.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDateofAppointment.Text.Trim());

            }

            if (ddlBloodGroup.SelectedIndex > 0)
            {
                objEmpMas.BLOODGRPNO = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            }
            else
            {
                objEmpMas.BLOODGRPNO = 0;
            }

            

            if (txtPan.Text != string.Empty)
            {
                objEmpMas.PAN_NO = txtPan.Text;
            }
            else
            {
                objEmpMas.PAN_NO = string.Empty;
            }

            



            if (txtAdhar.Text != string.Empty)
            {
                objEmpMas.ADHAR = txtAdhar.Text;
            }
            else
            {
                objEmpMas.ADHAR = string.Empty;
            }

            if (txtWhats.Text != string.Empty)
            {
                objEmpMas.ALTERNATEPHONENO = txtWhats.Text;
            }
            else
            {
                objEmpMas.ALTERNATEPHONENO = string.Empty;
            }

            if (txtPassport.Text != string.Empty)
            {
                objEmpMas.PASSPORT = txtPassport.Text;
            }
            else
            {
                objEmpMas.PASSPORT = string.Empty;
            }

            if (txtPersonalEmail.Text != string.Empty)
            {
                objEmpMas.ALTERNATEEMAILID = txtPersonalEmail.Text;
            }
            else
            {
                objEmpMas.ALTERNATEEMAILID = string.Empty;
            }

            if (IsDesignation == false)
            {
                objEmpMas.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);
                   
            }
            else
            {
                objEmpMas.SUBDESIGNO = designation;
            }

            if (IsDepartment == false)
            {
                objEmpMas.SUBDEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
                   
            }
            else
            {
                objEmpMas.SUBDEPTNO = department;
            }
            //added on 11-04-2022

            if (txtaicte.Text != string.Empty)
            {
                objEmpMas.AICTE_NO = txtaicte.Text;
            }
            else
            {
                objEmpMas.AICTE_NO = string.Empty;
            }
            //
            //if (txtCountry.Text != string.Empty)
            //{
            //    objEmpMas.COUNTRY = txtCountry.Text;
            //}
            //else
            //{
            //    objEmpMas.COUNTRY = string.Empty;
            //}

            //if (txtState.Text != string.Empty)
            //{
            //    objEmpMas.STATE = txtState.Text;
            //}
            //else
            //{
            //    objEmpMas.STATE = string.Empty;
            //}

            if (txtCity.Text != string.Empty)
            {
                objEmpMas.CITY = txtCity.Text;
            }
            else
            {
                objEmpMas.CITY = string.Empty;
            }

            if (txtTaluka.Text != string.Empty)
            {
                objEmpMas.TALUKA = txtTaluka.Text;
            }
            else
            {
                objEmpMas.TALUKA = string.Empty;
            }

            if (txtDistrict.Text != string.Empty)
            {
                objEmpMas.DISTRICT = txtDistrict.Text;
            }
            else
            {
                objEmpMas.DISTRICT = string.Empty;
            }

            

            if (txtPincode.Text != string.Empty)
            {
                objEmpMas.PINCODE = txtPincode.Text;
            }
            else
            {
                objEmpMas.PINCODE = string.Empty;
            }

            objEmpMas.UA_NO = Convert.ToInt32(Session["userno"].ToString());

            if (ddlCountry.SelectedIndex > 0)
            {
                objEmpMas.COUNTRYNO = Convert.ToInt32(ddlCountry.SelectedValue);
                objEmpMas.COUNTRY = ddlCountry.SelectedItem.Text;
            }
            else
            {
                objEmpMas.COUNTRYNO = 0;
                objEmpMas.COUNTRY = string.Empty;
            }

            if (ddlState.SelectedIndex > 0)
            {
                objEmpMas.STATENO = Convert.ToInt32(ddlState.SelectedValue);
                objEmpMas.STATE = ddlState.SelectedItem.Text;
            }
            else
            {
                objEmpMas.STATENO = 0;
                objEmpMas.STATE = string.Empty;
            }           

            CustomStatus cs = (CustomStatus)objServiceBook.UpdatePersonalMemorandam(objEmpMas, mothername);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                //lblmsg.Text = "Record Updated Successfully";
                //UpdatePanel updpersonaldetails=(UpdatePanel)this.Parent.FindControl("upWebUserControl");
                //UpdatePanel updpersonaldetails = (UpdatePanel)this.Parent.FindControl("PnlPersonalMemorandam");
               // objCommon.DisplayMessage(PnlPersonalMemorandam, "Record Updated Successfully", this.Page);
                MessageBox("Record Updated Successfully");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PersonalMemoranda.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void ShowDetails(int idno)
    {
        DataSet ds = null;
        try
        {

            ViewState["_idnoEmp"] = idno;
            ds = objServiceBook.GetSingleEmployeeDetails(idno);
            //To show Employee details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                //txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                //txtFirstName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                //txtMiddleName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                //txtMotherName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                //txtLastName.Text = ds.Tables[0].Rows[0]["LNAME"].ToString();
               // txtDesignation.Text = objCommon.LookUp("PAYROLL_SUBDESIG", "SUBDESIG", "SUBDESIGNO=" + ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString());
               // txtDeparteMent.Text = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPT", "SUBDEPTNO=" + ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString());
                txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                txtMotherName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                txtBirthDate.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                txtDateofAppointment.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                txtHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                txtMarksofIdentification1.Text = ds.Tables[0].Rows[0]["IDMARK1"].ToString();
                txtMarksofIdentification2.Text = ds.Tables[0].Rows[0]["IDMARK2"].ToString();
                txtPresentAddress.Text = ds.Tables[0].Rows[0]["RESADD1"].ToString();
                txtPermanentAddress.Text = ds.Tables[0].Rows[0]["TOWNADD1"].ToString();
                txtPhoneNumber.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                ddlCaste.SelectedValue = ds.Tables[0].Rows[0]["CASTENO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["CASTENO"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                ddlReligion.SelectedValue = ds.Tables[0].Rows[0]["RELIGIONNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["RELIGIONNO"].ToString();
                ddlNationality.SelectedValue = ds.Tables[0].Rows[0]["NATIONALITYNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["NATIONALITYNO"].ToString();
                ddlStaffType.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["STNO"].ToString();
                ddlAppointmentCategory.SelectedValue = ds.Tables[0].Rows[0]["ACATNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["ACATNO"].ToString();
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                ddlTitle.SelectedItem.Text = ds.Tables[0].Rows[0]["TITLE"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["TITLE"].ToString();

                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["STATUSNO"].ToString() == string.Empty ? "0" : ds.Tables[0].Rows[0]["STATUSNO"].ToString();
                txtStatusDT.Text = ds.Tables[0].Rows[0]["STDATE"] == null ? string.Empty : ds.Tables[0].Rows[0]["STDATE"].ToString();

                txtAuthenticated.Text = ds.Tables[0].Rows[0]["AUTHENREF"].ToString();
                txtFnAn.Text = ds.Tables[0].Rows[0]["ANFN"].ToString();
                //===========================
                //if (txtTitle.Enabled == false)
                //{
                //    title = ds.Tables[0].Rows[0]["TITLE"].ToString();
                //    Istitle = false;
                //}
                //else
                //{
                //    Istitle = true;
                //}
                //txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                if (txtFirstName.Enabled == false)
                {
                    fname = ds.Tables[0].Rows[0]["FNAME"].ToString();
                    Isfname = false;
                }
                else
                {
                    Isfname = true;
                }
                txtFirstName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                txtMiddleName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                if (txtMiddleName.Enabled == false)
                {
                    mname = ds.Tables[0].Rows[0]["MNAME"].ToString();
                    Ismname = false;
                }
                else
                {
                    Ismname = true;
                }
                txtLastName.Text = ds.Tables[0].Rows[0]["LNAME"].ToString();
                if (txtLastName.Enabled == false)
                {
                    lname = ds.Tables[0].Rows[0]["LNAME"].ToString();
                    Islname = false;
                }
                else
                {
                    Islname = true;
                }
                txtBirthDate.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                if (txtBirthDate.Enabled == false)
                {
                    dob = ds.Tables[0].Rows[0]["DOB"].ToString();
                    IsDOB = false;
                }
                else
                {
                    IsDOB = true;
                }
                txtDateofAppointment.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                if (txtDateofAppointment.Enabled == false)
                {
                    doj = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    IsDOJ = false;
                }
                else
                {
                    IsDOJ = true;
                }
                //===============================

                ddlBloodGroup.SelectedValue = ds.Tables[0].Rows[0]["BLOODGRPNO"].ToString();
                txtPan.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                txtaicte.Text = ds.Tables[0].Rows[0]["AICTE_NO"].ToString();
                txtAdhar.Text = ds.Tables[0].Rows[0]["NUNIQUEID"].ToString();
                txtWhats.Text = ds.Tables[0].Rows[0]["AlternateMobileNo"].ToString();
                txtPassport.Text = ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                txtPersonalEmail.Text = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();
                //txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                //txtState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                txtCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
                txtTaluka.Text = ds.Tables[0].Rows[0]["TALUKA"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                txtPincode.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYNO"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PersonalMemoranda.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        //Response.Redirect(Request.Url.ToString());
       Clear();
    }

    private void Clear()
    {
        lblmsg.Text = "";
        txtFatherName.Text = string.Empty;
        txtBirthDate.Text = string.Empty;
        txtDateofAppointment.Text = string.Empty;
        txtHeight.Text = string.Empty;
        txtMarksofIdentification1.Text = string.Empty;
        txtMarksofIdentification2.Text = string.Empty;
        txtPresentAddress.Text = string.Empty;
        txtPermanentAddress.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlCaste.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        ddlReligion.SelectedValue = "0";
        ddlNationality.SelectedValue = "0";
        ddlStaffType.SelectedValue = "0";
        ddlAppointmentCategory.SelectedValue = "0";
        txtAuthenticated.Text = string.Empty;
        txtFnAn.Text = string.Empty;
        txtMotherName.Text = string.Empty;
        ShowDetails(0);
        ddlBloodGroup.SelectedIndex = 0;
        txtPassport.Text = string.Empty;
        txtPan.Text = string.Empty;
        txtaicte.Text = string.Empty;
        txtAdhar.Text = string.Empty;
        txtWhats.Text = string.Empty;
        txtPersonalEmail.Text = string.Empty;
        txtCountry.Text = string.Empty;
        txtState.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtTaluka.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtDistrict.Text = string.Empty;
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCaste, "PAYROLL_CASTE", "CASTENO", "CASTE", "CASTENO > 0 AND ACTIVESTATUS =" + 1 , "CASTE");
            objCommon.FillDropDownList(ddlReligion, "PAYROLL_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO > 0 AND ACTIVESTATUS =" + 1 , "RELIGION");
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "stno", "stafftype", "stno > 0 AND ACTIVESTATUS =" + 1, "stafftype");
            objCommon.FillDropDownList(ddlCategory, "PAYROLL_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS =" + 1 , "CATEGORY");
            objCommon.FillDropDownList(ddlAppointmentCategory, "PAYROLL_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS =" + 1, "CATEGORY");
            objCommon.FillDropDownList(ddlNationality, "PAYROLL_NATIONALITY", "NATIONALITYNO", "NATIONALITYNM", "NATIONALITYNO > 0 AND ACTIVESTATUS =" + 1, "NATIONALITYNM");
            objCommon.FillDropDownList(ddlStatus, "PAYROLL_STATUS", "STATUSNO", "STATUS", "STATUSNO>0", "STATUS");
            objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "COLLEGE_CODE>0 AND ACTIVESTATUS =" + 1, "BLOODGRPNAME");
            objCommon.FillDropDownList(ddlTitle,"PAYROLL_TITLE","TITLENO" , "TITLE" , "TITLENO>0 AND ACTIVESTATUS =" + 1 ,"TITLE");
            objCommon.FillDropDownList(ddlDesignation, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIG");
            objCommon.FillDropDownList(ddlDept, "payroll_subdept", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPT");
            //Added by Sonal Banode for Country and State Dropdown
            objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0 AND ACTIVE_STATUS =" + 1, "COUNTRYNAME");
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0 AND ACTIVESTATUS =" + 1, "STATENAME"); 
            //
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PersonalMemoranda.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0 AND COUNTRYNO = " + Convert.ToInt32(ddlCountry.SelectedValue) + "AND ACTIVESTATUS =" + 1, "STATENAME"); 
    }
}