//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_PersonalMemoranda.ascx                                                
// CREATION DATE : 23-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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

public partial class PayRoll_Pay_PersonalMemoranda : System.Web.UI.UserControl
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    static string title, fname, mname, lname, dob, doj = string.Empty;
    static bool Istitle, Isfname, Ismname, Islname, IsDOB, IsDOJ = false;

    public int _idnoEmp;
   
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
            FillDropDown();
            ViewState["_idnoEmp"] = "0";
        }


        DropDownList ddlempno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        if (ddlempno.SelectedIndex > 0)
            _idnoEmp = Convert.ToInt16(ddlempno.SelectedValue);
        

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {  
             EmpMaster objEmpMas =new EmpMaster();
             objEmpMas.IDNO = _idnoEmp;   	
             objEmpMas.FATHERNAME = txtFatherName.Text;
            // objEmpMas.DOB =Convert.ToDateTime(txtBirthDate.Text);
             //if (!txtBirthDate.Text.Trim().Equals(string.Empty)) objEmpMas.DOB = Convert.ToDateTime(txtBirthDate.Text);
             //objEmpMas.DOJ = Convert.ToDateTime(txtDateofAppointment.Text); 		
             objEmpMas.HEIGHT =txtHeight.Text; 	
             objEmpMas.IDMARK1=txtMarksofIdentification1.Text;	
             objEmpMas.IDMARK2 =txtMarksofIdentification2.Text;	
             objEmpMas.RESADD1=txtPresentAddress.Text;	
             objEmpMas.TOWNADD1=txtPermanentAddress.Text;
             string mothername = Convert.ToString(txtMotherName.Text);
             objEmpMas.PHONENO = txtPhoneNumber.Text;
             objEmpMas.EMAILID=txtEmail.Text;	
             objEmpMas.CASTENO=Convert.ToInt32(ddlCaste.SelectedValue);	
             objEmpMas.CATEGORYNO= Convert.ToInt32(ddlCategory.SelectedValue);
             objEmpMas.RELIGIONNO =Convert.ToInt32(ddlReligion.SelectedValue);
             objEmpMas.NATIONALITYNO = Convert.ToInt32(ddlNationality.SelectedValue);	
             objEmpMas.STNO	=Convert.ToInt32(ddlStaffType.SelectedValue);
             objEmpMas.ACATNO = Convert.ToInt32(ddlAppointmentCategory.SelectedValue); 
             objEmpMas.AUTHENREF=txtAuthenticated.Text;
             objEmpMas.ANFN=txtFnAn.Text;


             //=====================================
             if (Istitle == false)
             {
                 objEmpMas.TITLE = title;
             }
             else
             {
                 objEmpMas.TITLE = txtTitle.Text;
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

             CustomStatus cs = (CustomStatus)objServiceBook.UpdatePersonalMemorandam(objEmpMas, mothername);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                
                lblmsg.Text = "Record Updated Successfully";
                //UpdatePanel updpersonaldetails=(UpdatePanel)this.Parent.FindControl("upWebUserControl");
                UpdatePanel updpersonaldetails = (UpdatePanel)this.Parent.FindControl("PnlPersonalMemorandam");
                objCommon.DisplayMessage(PnlPersonalMemorandam, "Record Updated Successfully", this.Page);
                            
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
                  txtDesignation.Text = objCommon.LookUp("PAYROLL_SUBDESIG","SUBDESIG","SUBDESIGNO=" + ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString());
                  txtDeparteMent.Text = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPT", "SUBDEPTNO=" + ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString());
                  txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
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
                  txtAuthenticated.Text = ds.Tables[0].Rows[0]["AUTHENREF"].ToString();
                  txtFnAn.Text = ds.Tables[0].Rows[0]["ANFN"].ToString();
                  //===========================
                  if (txtTitle.Enabled == false)
                  {
                      title = ds.Tables[0].Rows[0]["TITLE"].ToString();
                      Istitle = false;
                  }
                  else
                  {
                      Istitle = true;
                  }
                  txtTitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
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

        Response.Redirect(Request.Url.ToString());
        //Clear();
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
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCaste, "PAYROLL_CASTE", "CASTENO", "CASTE", "CASTENO > 0", "CASTE");
            objCommon.FillDropDownList(ddlReligion, "PAYROLL_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO > 0", "RELIGION");
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "stno", "stafftype", "stno > 0", "stafftype");
            objCommon.FillDropDownList(ddlCategory, "PAYROLL_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 ", "CATEGORY");
            objCommon.FillDropDownList(ddlAppointmentCategory, "PAYROLL_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0", "CATEGORY");
            objCommon.FillDropDownList(ddlNationality, "PAYROLL_NATIONALITY", "NATIONALITYNO", "NATIONALITYNM","NATIONALITYNO > 0", "NATIONALITYNM");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PersonalMemoranda.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

}
