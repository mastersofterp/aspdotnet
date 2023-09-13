using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;


public partial class ESTABLISHMENT_SERVICEBOOK_EmployeeServiceBookDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    ServiceBook objSM = new ServiceBook();
    BlobController objBlob = new BlobController();

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
        if (!Page.IsPostBack)
        {
            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    Page.Title = Session["coll_name"].ToString();

                    // CheckPageAuthorization();
                }
            }
            //blank div tag
            divMsg.InnerHtml = string.Empty;

            ddlEmployee.Enabled = true;
            ddlorderby.Enabled = true;
            this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
            BlobDetails();
            ShowDetails();
        }
        else
        {
            //
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LeaveAllotmentReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LeaveAllotmentReport.aspx");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["idno"] = ddlEmployee.SelectedValue;
    }
    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
    }
    private void FillDropDown(int val)
    {
        try
        {
            int user_type = 0;
            user_type = Convert.ToInt32(Session["usertype"].ToString());
            if (user_type == 3 || user_type == 4)
            {
                string ua_idno = Session["username"].ToString();
                string username = Session["username"].ToString();
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                if (val == 1)
                    // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + " AND EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=(SELECT UA_IDNO FROM USER_ACC WHERE UA_NAME= '" + username + "' AND UA_NO=" + ua_no + ") AND EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=(SELECT UA_IDNO FROM USER_ACC WHERE UA_NAME= '" + username + "' AND UA_NO=" + ua_no + ") AND EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");
            }
            else
            {
                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");
            }

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //ShowDetails();
    }

    private void ShowDetails()
    {
        DataSet ds = null;
        try
        {
            int idno = Convert.ToInt32(ViewState["idno"]);
            objSM.IDNO = idno;

            imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + idno + "&type=emp";

            #region Personal Memoranda
            ds = objServiceBook.GetSingleEmployeePersonalDetails(idno);
            //To show Employee details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                string Title = ds.Tables[0].Rows[0]["TITLE"].ToString();
                string Fname = ds.Tables[0].Rows[0]["FNAME"].ToString();
                string Mname = ds.Tables[0].Rows[0]["MNAME"].ToString();
                string Lname = ds.Tables[0].Rows[0]["LNAME"].ToString();

                lblName.Text = Title + " " + Fname + " " + Mname + " " + Lname;

                string DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                DateTime DateOfBirth = Convert.ToDateTime(DOB);
                lblBirthDate.Text = DateOfBirth.ToString("dd/MM/yyyy");

                //lblSpouseName.Text = ds.Tables[0].Rows[0]["SPOUCENAME"].ToString();

                //lblemergencyNo.Text = ds.Tables[0].Rows[0]["EMERGENCY_NO"].ToString();

                lblDesignation.Text = objCommon.LookUp("PAYROLL_SUBDESIG", "SUBDESIG", "SUBDESIGNO=" + ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString());
                lblDeparteMent.Text = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPT", "SUBDEPTNO=" + ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString());
                lblFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
                lblMotherName.Text = ds.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                string DOJ = ds.Tables[0].Rows[0]["DOJ"].ToString();
                DateTime JoiningDT = Convert.ToDateTime(DOJ);
                lblDOJ.Text = JoiningDT.ToString("dd/MM/yyyy");
                lblHeight.Text = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                lblMarksofIdentification1.Text = ds.Tables[0].Rows[0]["IDMARK1"].ToString();
                lblMarksofIdentification2.Text = ds.Tables[0].Rows[0]["IDMARK2"].ToString();
                lblPresentAddress.Text = ds.Tables[0].Rows[0]["RESADD1"].ToString();
                lblPhoneNumber.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                //STC_CODE
                //lblStdCode.Text = ds.Tables[0].Rows[0]["STD_NO"].ToString();
                //lblLandLine.Text = ds.Tables[0].Rows[0]["LANDLINENO"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                //lblOffiEmail.Text = ds.Tables[0].Rows[0]["OFFICIAL_EMAILID"].ToString();
                lblCaste.Text = ds.Tables[0].Rows[0]["CASTE"].ToString();
                lblCategory.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                lblReligion.Text = ds.Tables[0].Rows[0]["RELIGION"].ToString();
                lblNationality.Text = ds.Tables[0].Rows[0]["NATIONALITYNM"].ToString();
                lblStaffType.Text = ds.Tables[0].Rows[0]["STAFFTYPE"].ToString();
                lblAppointmentCategory.Text = ds.Tables[0].Rows[0]["ACA_CATEGORY"].ToString();
                lblCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                lblPresentState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                lblPresentCity.Text = ds.Tables[0].Rows[0]["CITY"].ToString();
                lblDistrict.Text = ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                lblTaluka.Text = ds.Tables[0].Rows[0]["TALUKA"].ToString();
                lblPresentPncode.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                lblBloodGroup.Text = ds.Tables[0].Rows[0]["BLOODGRPNAME"].ToString();
                lblAadharno.Text = ds.Tables[0].Rows[0]["aadharno"].ToString();
                lblAicteno.Text = ds.Tables[0].Rows[0]["AICTE_NO"].ToString();
                lblPanno.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                lblPassport.Text = ds.Tables[0].Rows[0]["PASSPORTNO"].ToString();
                lblWhatsAppno.Text = ds.Tables[0].Rows[0]["AlternateMobileNo"].ToString();
                lblOffiEmail.Text = ds.Tables[0].Rows[0]["OfficialMail"].ToString();

                //if (ddlAppointmentCategory.SelectedItem.Text == "OTHER")
                //{
                //    txtOtherAppointment.Text = ds.Tables[0].Rows[0]["OTHER_APPOINT_NAME"].ToString();
                //    trOtherApp.Visible = true;
                //}
                //else
                //{
                //    txtOtherAppointment.Text = string.Empty;
                //    trOtherApp.Visible = false;
                //}
                //txtAuthenticated.Text = ds.Tables[0].Rows[0]["AUTHENREF"].ToString();
                //txtFnAn.Text = ds.Tables[0].Rows[0]["ANFN"].ToString();
                //lblBloodGp.Text = ds.Tables[0].Rows[0]["BLOODGRPNAME"].ToString();	//added by-swati ghate
                //lblState.Text = ds.Tables[0].Rows[0]["STATE_NO_NAME"].ToString();	//added by-swati ghate


                //*****ADDED BY: M. REHBAR SHEIKH ON 24-08-2019*****
                //lblPresentAddress.Text = ds.Tables[0].Rows[0]["PRESENT_ADDRESS"].ToString();
                //lblPresentState.Text = ds.Tables[0].Rows[0]["PRESENT_STATE"].ToString();
                //lblPresentCity.Text = ds.Tables[0].Rows[0]["PRESENT_CITY"].ToString();                
                //lblPresentPncode.Text = ds.Tables[0].Rows[0]["PRESENT_PINCODE"].ToString();

            }
            #endregion

            #region Family Particulars
            //DataSet ds1 = objServiceBook.GetAllFamilyDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    Rep_Familyinfo.DataSource = ds1.Tables[0];
            //    Rep_Familyinfo.DataBind();
            //}
            //else
            //{
            //    Rep_Familyinfo.DataSource = null;
            //    Rep_Familyinfo.DataBind();
            //}

            DataSet dsFam = objServiceBook.GetAllFamilyDetailsCount();

            if (dsFam.Tables[0].Rows.Count > 0)
            {
                lblFamily.Text = dsFam.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_Familyinfo.DataSource = dsFam.Tables[0];
                Rep_Familyinfo.DataBind();

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_Familyinfo.FindControl("divFolder");
                    Control ctrHead1 = Rep_Familyinfo.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_Familyinfo.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_Familyinfo.FindControl("divFolder");
                    Control ctrHead1 = Rep_Familyinfo.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_Familyinfo.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblFamily.Text = "0";
                Rep_Familyinfo.DataSource = null;
                Rep_Familyinfo.DataBind();
            }

            #endregion

            #region Nomination
            //DataSet ds2 = objServiceBook.GetAllNominiDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds2.Tables[0].Rows.Count > 0)
            //{
            //    Rep_Nomination.DataSource = ds2.Tables[0];
            //    Rep_Nomination.DataBind();
            //}
            //else
            //{
            //    Rep_Nomination.DataSource = null;
            //    Rep_Nomination.DataBind();
            //}

            DataSet dsNom = objServiceBook.GetAllNominiPendingCount();
            if (dsNom.Tables[0].Rows.Count > 0)
            {
                lblNom.Text = dsNom.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_Nomination.DataSource = dsNom.Tables[0];
                Rep_Nomination.DataBind();
                
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_Nomination.FindControl("divFolder");
                    Control ctrHead1 = Rep_Nomination.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_Nomination.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_Nomination.FindControl("divFolder");
                    Control ctrHead1 = Rep_Nomination.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_Nomination.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblNom.Text = "0";
                Rep_Nomination.DataSource = null;
                Rep_Nomination.DataBind();
            }

            #endregion

            // #region Certificate Upload
            //DataSet dsphoto = objServiceBook.GetAllEmpImageDetails(idno);

            //if (dsphoto.Tables[0].Rows.Count > 0)
            //{
            //    Rep_ImageUpload.DataSource = dsphoto.Tables[0];
            //    Rep_ImageUpload.DataBind();
            //}
            //else
            //{
            //    Rep_ImageUpload.DataSource = null;
            //    Rep_ImageUpload.DataBind();
            //}
            //#endregion

            #region Qualification
            //DataSet ds3 = objServiceBook.GetAllQualificationDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds3.Tables[0].Rows.Count > 0)
            //{
            //    Rep_Qualification.DataSource = ds3.Tables[0];
            //    Rep_Qualification.DataBind();
            //}
            //else
            //{
            //    Rep_Qualification.DataSource = null;
            //    Rep_Qualification.DataBind();
            //}

            DataSet dsQuali = objServiceBook.GetAllQualificationDetailsCount();
            if (dsQuali.Tables[0].Rows.Count > 0)
            {
                lblQuali.Text = dsQuali.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_Qualification.DataSource = dsQuali.Tables[0];
                Rep_Qualification.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_Qualification.FindControl("divFolder");
                    Control ctrHead1 = Rep_Qualification.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_Qualification.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_Qualification.FindControl("divFolder");
                    Control ctrHead1 = Rep_Qualification.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_Qualification.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblQuali.Text = "0";
                Rep_Qualification.DataSource = null;
                Rep_Qualification.DataBind();
            }
            #endregion

            #region Department Examination
            //DataSet ds4 = objServiceBook.GetAllDeptExamDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds4.Tables[0].Rows.Count > 0)
            //{
            //    Rep_DeptExam.DataSource = ds4.Tables[0];
            //    Rep_DeptExam.DataBind();
            //}
            //else
            //{
            //    Rep_DeptExam.DataSource = null;
            //    Rep_DeptExam.DataBind();
            //}

            DataSet dsDeptExam = objServiceBook.GetAllDeptExamCount();
            if (dsDeptExam.Tables[0].Rows.Count > 0)
            {
                lblDept.Text = dsDeptExam.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_DeptExam.DataSource = dsDeptExam.Tables[0];
                Rep_DeptExam.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_DeptExam.FindControl("divFolder");
                    Control ctrHead1 = Rep_DeptExam.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_DeptExam.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_DeptExam.FindControl("divFolder");
                    Control ctrHead1 = Rep_DeptExam.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_DeptExam.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblDept.Text = "0";
                Rep_DeptExam.DataSource = null;
                Rep_DeptExam.DataBind();
            }
            #endregion

            #region PreviousService
            //DataSet ds5 = objServiceBook.GetAllPreServiceDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds5.Tables[0].Rows.Count > 0)
            //{
            //    Rep_PrevExp.DataSource = ds5.Tables[0];
            //    Rep_PrevExp.DataBind();
            //}
            //else
            //{
            //    Rep_PrevExp.DataSource = null;
            //    Rep_PrevExp.DataBind();
            //}

            DataSet dsPrevExp = objServiceBook.GetAllPreServiceCount();
            if (dsPrevExp.Tables[0].Rows.Count > 0)
            {
                lblPrevExp.Text = dsPrevExp.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_PrevExp.DataSource = dsPrevExp.Tables[0];
                Rep_PrevExp.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_PrevExp.FindControl("divFolder");
                    Control ctrHead1 = Rep_PrevExp.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_PrevExp.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_PrevExp.FindControl("divFolder");
                    Control ctrHead1 = Rep_PrevExp.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_PrevExp.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_PrevExp.FindControl("divFolder1");
                    Control ctrHead1 = Rep_PrevExp.FindControl("divBlob1");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_PrevExp.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_PrevExp.FindControl("divFolder1");
                    Control ctrHead1 = Rep_PrevExp.FindControl("divBlob1");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_PrevExp.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_PrevExp.FindControl("divFolder2");
                    Control ctrHead1 = Rep_PrevExp.FindControl("divBlob2");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_PrevExp.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder2");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob2");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_PrevExp.FindControl("divFolder2");
                    Control ctrHead1 = Rep_PrevExp.FindControl("divBlob2");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_PrevExp.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder2");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob2");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }

            }
            else
            {
                lblPrevExp.Text = "0";
                Rep_PrevExp.DataSource = null;
                Rep_PrevExp.DataBind();
            }
            #endregion

            #region Admin_Responsibilities
            //DataSet ds6 = objServiceBook.GetAllAdminResponsibilities(idno);
            ////To show employee family details 
            //if (ds6.Tables[0].Rows.Count > 0)
            //{
            //    Rep_AdminResponse.DataSource = ds6.Tables[0];
            //    Rep_AdminResponse.DataBind();
            //}
            //else
            //{
            //    Rep_AdminResponse.DataSource = null;
            //    Rep_AdminResponse.DataBind();
            //}

            DataSet dsAdminResp = objServiceBook.GetAllAdminResponsibilitiesCount();
            if (dsAdminResp.Tables[0].Rows.Count > 0)
            {
                lblAdminResp.Text = dsAdminResp.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_AdminResponse.DataSource = dsAdminResp.Tables[0];
                Rep_AdminResponse.DataBind();

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_AdminResponse.FindControl("divFolder");
                    Control ctrHead1 = Rep_AdminResponse.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_AdminResponse.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_AdminResponse.FindControl("divFolder");
                    Control ctrHead1 = Rep_AdminResponse.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_AdminResponse.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblAdminResp.Text = "0";
                Rep_AdminResponse.DataSource = null;
                Rep_AdminResponse.DataBind();
            }
            #endregion

            #region Publication Details
            //DataSet ds20 = objCommon.FillDropDown("PAYROLL_SB_PUBLICATION_DETAILS_NEW", "*", "", "IDNO=" + idno + " ", "PUBNO");
            //DataSet ds7 = objServiceBook.GetAllPublicationDetails(idno);
            //if (ds7.Tables[0].Rows.Count > 0)
            //{
            //    Rep_Publication.DataSource = ds7.Tables[0];
            //    Rep_Publication.DataBind();
            //}
            //else
            //{
            //    Rep_Publication.DataSource = null;
            //    Rep_Publication.DataBind();
            //}

            DataSet dsPubDetail = objServiceBook.GetAllPublicationDetailsCount();
            if (dsPubDetail.Tables[0].Rows.Count > 0)
            {
                lblPubDetails.Text = dsPubDetail.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_Publication.DataSource = dsPubDetail.Tables[0];
                Rep_Publication.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_Publication.FindControl("divFolder");
                    Control ctrHead1 = Rep_Publication.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_Publication.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_Publication.FindControl("divFolder");
                    Control ctrHead1 = Rep_Publication.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_Publication.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblPubDetails.Text = "0";
                Rep_Publication.DataSource = null;
                Rep_Publication.DataBind();
            }

            #endregion

            #region Guest Lecture
            //DataSet ds20 = objCommon.FillDropDown("PAYROLL_SB_PUBLICATION_DETAILS_NEW", "*", "", "IDNO=" + idno + " ", "PUBNO");
            //DataSet ds8 = objServiceBook.GetAllInvitedTalk(idno);
            //if (ds8.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Guest.DataSource = ds8.Tables[0];
            //    Repeater_Guest.DataBind();
            //}
            //else
            //{
            //    Repeater_Guest.DataSource = null;
            //    Repeater_Guest.DataBind();
            //}

            DataSet dsGuestLec = objServiceBook.GetAllInvitedTalkCount();
            if (dsGuestLec.Tables[0].Rows.Count > 0)
            {
                lblGuestLec.Text = dsGuestLec.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Guest.DataSource = dsGuestLec.Tables[0];
                Repeater_Guest.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_Guest.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Guest.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_Guest.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_Guest.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Guest.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_Guest.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblGuestLec.Text = "0";
                Repeater_Guest.DataSource = null;
                Repeater_Guest.DataBind();
            }
            #endregion

            #region Training
            //DataSet ds9 = objServiceBook.GetAllTrainingDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds9.Tables[0].Rows.Count > 0)
            //{
            //    Rep_Tarining.DataSource = ds9.Tables[0];
            //    Rep_Tarining.DataBind();
            //}
            //else
            //{
            //    Rep_Tarining.DataSource = null;
            //    Rep_Tarining.DataBind();
            //}

            DataSet dsTrainAtten = objServiceBook.GetAllTrainingDetailsCount();
            if (dsTrainAtten.Tables[0].Rows.Count > 0)
            {
                lblTrainingAttend.Text = dsTrainAtten.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_Tarining.DataSource = dsTrainAtten.Tables[0];
                Rep_Tarining.DataBind();

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Rep_Tarining.FindControl("divFolder");
                    Control ctrHead1 = Rep_Tarining.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Rep_Tarining.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Rep_Tarining.FindControl("divFolder");
                    Control ctrHead1 = Rep_Tarining.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Rep_Tarining.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblTrainingAttend.Text = "0";
                Rep_Tarining.DataSource = null;
                Rep_Tarining.DataBind();
            }
            #endregion

            #region Training_Conducted
            //DataSet ds10 = objServiceBook.GetAllTrainingConductedDetailsOfEmployee(idno);
            ////To show employee family details 
            //if (ds10.Tables[0].Rows.Count > 0)
            //{
            //    Rep_TrainingConduct.DataSource = ds10.Tables[0];
            //    Rep_TrainingConduct.DataBind();
            //}
            //else
            //{
            //    Rep_TrainingConduct.DataSource = null;
            //    Rep_TrainingConduct.DataBind();
            //}

            DataSet dsTrainCond = objServiceBook.GetAllTrainingConductedCount();
            if (dsTrainCond.Tables[0].Rows.Count > 0)
            {
                lblTrainCond.Text = dsTrainCond.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_TrainingConduct.DataSource = dsTrainCond.Tables[0];
                Rep_TrainingConduct.DataBind();
            }
            else
            {
                lblTrainCond.Text = "0";
                Rep_TrainingConduct.DataSource = null;
                Rep_TrainingConduct.DataBind();
            }
            #endregion

            #region Invited_Talks
            DataSet ds11 = objServiceBook.GetAllInvitedTalk(idno);
            //To show employee family details 
            if (ds11.Tables[0].Rows.Count > 0)
            {
                Rep_InvitedTalks.DataSource = ds11.Tables[0];
                Rep_InvitedTalks.DataBind();
            }
            else
            {
                Rep_InvitedTalks.DataSource = null;
                Rep_InvitedTalks.DataBind();
            }
            #endregion

            #region Research & Consultancy
            //DataSet ds12 = objServiceBook.GetAllStaffConsultancyEmployee(idno);
            ////To show employee family details 
            //if (ds12.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Consultancy.DataSource = ds12.Tables[0];
            //    Repeater_Consultancy.DataBind();
            //}
            //else
            //{
            //    Repeater_Consultancy.DataSource = null;
            //    Repeater_Consultancy.DataBind();
            //}

            DataSet dsConsultancy = objServiceBook.GetAllStaffConsultancyCount();
            if (dsConsultancy.Tables[0].Rows.Count > 0)
            {
                lblConsultancy.Text = dsConsultancy.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Consultancy.DataSource = dsConsultancy.Tables[0];
                Repeater_Consultancy.DataBind();
            }
            else
            {
                lblConsultancy.Text = "0";
                Repeater_Consultancy.DataSource = null;
                Repeater_Consultancy.DataBind();
            }
            #endregion

            #region Accomplishment
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds13 = objServiceBook.GetAllAccomplishmentEmployee(idno);
            //if (ds13.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Accomplishment.DataSource = ds13.Tables[0];
            //    Repeater_Accomplishment.DataBind();
            //}
            //else
            //{
            //    Repeater_Accomplishment.DataSource = null;
            //    Repeater_Accomplishment.DataBind();
            //}

            DataSet dsAccomplishment = objServiceBook.GetAllAccomplishmentCount();
            if (dsAccomplishment.Tables[0].Rows.Count > 0)
            {
                lblAccomplishment.Text = dsAccomplishment.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Accomplishment.DataSource = dsAccomplishment.Tables[0];
                Repeater_Accomplishment.DataBind();
            }
            else
            {
                lblAccomplishment.Text = "0";
                Repeater_Accomplishment.DataSource = null;
                Repeater_Accomplishment.DataBind();
            }
            #endregion

            #region MembershipinProfessional
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds14 = objServiceBook.GetAllMembershipinProfessional(idno);
            //if (ds14.Tables[0].Rows.Count > 0)
            //{
            //    Rep_Membership.DataSource = ds14.Tables[0];
            //    Rep_Membership.DataBind();
            //}
            //else
            //{
            //    Rep_Membership.DataSource = null;
            //    Rep_Membership.DataBind();
            //}

            DataSet dsMemProf = objServiceBook.GetAllMembershipinProfessionalCount();
            if (dsMemProf.Tables[0].Rows.Count > 0)
            {
                lblMem.Text = dsMemProf.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Rep_Membership.DataSource = dsMemProf.Tables[0];
                Rep_Membership.DataBind();
            }
            else
            {
                lblMem.Text = "0";
                Rep_Membership.DataSource = null;
                Rep_Membership.DataBind();
            }
            #endregion

            #region FundedProject
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds15 = objServiceBook.GetAllStaffFundedEmployee(idno);
            //if (ds15.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Funded.DataSource = ds15.Tables[0];
            //    Repeater_Funded.DataBind();
            //}
            //else
            //{
            //    Repeater_Funded.DataSource = null;
            //    Repeater_Funded.DataBind();
            //}

            DataSet dsFund = objServiceBook.GetAllStaffFundedEmployeeCount();
            if (dsFund.Tables[0].Rows.Count > 0)
            {
                lblFund.Text = dsFund.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Funded.DataSource = dsFund.Tables[0];
                Repeater_Funded.DataBind();
            }
            else
            {
                lblFund.Text = "0";
                Repeater_Funded.DataSource = null;
                Repeater_Funded.DataBind();
            }
            #endregion

            #region Patent
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds16 = objServiceBook.GetAllStaffPatent(idno);
            //if (ds16.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Patent.DataSource = ds16.Tables[0];
            //    Repeater_Patent.DataBind();
            //}
            //else
            //{
            //    Repeater_Patent.DataSource = null;
            //    Repeater_Patent.DataBind();
            //}

            DataSet dsPatent = objServiceBook.GetAllStaffPatentCount();
            if (dsPatent.Tables[0].Rows.Count > 0)
            {
                lblPatent.Text = dsPatent.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Patent.DataSource = dsPatent.Tables[0];
                Repeater_Patent.DataBind();
            }
            else
            {
                lblPatent.Text = "0";
                Repeater_Patent.DataSource = null;
                Repeater_Patent.DataBind();
            }
            #endregion

            #region InstituteExperience
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds17 = objServiceBook.GetAllExperiencesDetails(idno);
            //if (ds17.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_InstituteExperiences.DataSource = ds17.Tables[0];
            //    Repeater_InstituteExperiences.DataBind();
            //}
            //else
            //{
            //    Repeater_InstituteExperiences.DataSource = null;
            //    Repeater_InstituteExperiences.DataBind();
            //}

            DataSet dsInst = objServiceBook.GetAllExperiencesCount();
            if (dsInst.Tables[0].Rows.Count > 0)
            {
                lblInst.Text = dsInst.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_InstituteExperiences.DataSource = dsInst.Tables[0];
                Repeater_InstituteExperiences.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_InstituteExperiences.FindControl("divFolder");
                    Control ctrHead1 = Repeater_InstituteExperiences.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_InstituteExperiences.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_InstituteExperiences.FindControl("divFolder");
                    Control ctrHead1 = Repeater_InstituteExperiences.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_InstituteExperiences.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblInst.Text = "0";
                Repeater_InstituteExperiences.DataSource = null;
                Repeater_InstituteExperiences.DataBind();
            }
            #endregion

            #region Loans
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds18 = objServiceBook.GetAllLoanDetailsOfEmployee(idno);
            //if (ds18.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Loans.DataSource = ds18.Tables[0];
            //    Repeater_Loans.DataBind();
            //}
            //else
            //{
            //    Repeater_Loans.DataSource = null;
            //    Repeater_Loans.DataBind();
            //}

            DataSet dsLoan = objServiceBook.GetAllLoanDetailsCount();
            if (dsLoan.Tables[0].Rows.Count > 0)
            {
                lblLoan.Text = dsLoan.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Loans.DataSource = dsLoan.Tables[0];
                Repeater_Loans.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_Loans.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Loans.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_Loans.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_Loans.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Loans.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_Loans.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_Loans.FindControl("divFolder1");
                    Control ctrHead1 = Repeater_Loans.FindControl("divBlob1");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_Loans.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_Loans.FindControl("divFolder1");
                    Control ctrHead1 = Repeater_Loans.FindControl("divBlob1");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_Loans.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder1");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob1");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblLoan.Text = "0";
                Repeater_Loans.DataSource = null;
                Repeater_Loans.DataBind();
            }
            #endregion

            #region PayRevision
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds19 = objServiceBook.GetAllPayRevisionOfEmployee(idno);
            //if (ds19.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Revision.DataSource = ds19.Tables[0];
            //    Repeater_Revision.DataBind();
            //}
            //else
            //{
            //    Repeater_Revision.DataSource = null;
            //    Repeater_Revision.DataBind();
            //}

            DataSet dsPayRev = objServiceBook.GetAllPayRevisionCount();
            if (dsPayRev.Tables[0].Rows.Count > 0)
            {
                lblPayRev.Text = dsPayRev.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Revision.DataSource = dsPayRev.Tables[0];
                Repeater_Revision.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_Revision.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Revision.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_Revision.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_Revision.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Revision.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_Revision.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblPayRev.Text = "0";
                Repeater_Revision.DataSource = null;
                Repeater_Revision.DataBind();
            }
            #endregion

            #region Increment
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds20 = objServiceBook.GetAllServiceBookDetailsOfEmployee(idno);
            //if (ds20.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Increment.DataSource = ds20.Tables[0];
            //    Repeater_Increment.DataBind();
            //}
            //else
            //{
            //    Repeater_Increment.DataSource = null;
            //    Repeater_Increment.DataBind();
            //}

            DataSet dsIncrem = objServiceBook.GetAllServiceBookIncrementCount();
            if (dsIncrem.Tables[0].Rows.Count > 0)
            {
                lblIncrement.Text = dsIncrem.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Increment.DataSource = dsIncrem.Tables[0];
                Repeater_Increment.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_Increment.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Increment.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_Increment.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_Increment.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Increment.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_Increment.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblIncrement.Text = "0";
                Repeater_Increment.DataSource = null;
                Repeater_Increment.DataBind();
            }
            #endregion

            #region Matters
            //DataSet ds14 = objCommon.FillDropDown("PAYROLL_SB_MEMBERSHIP", "*", "", "IDNO=" + idno + " ", "SRNO");
            //DataSet ds21 = objServiceBook.GetAllMatterDetailsOfEmployee(idno);
            //if (ds21.Tables[0].Rows.Count > 0)
            //{
            //    Repeater_Matters.DataSource = ds21.Tables[0];
            //    Repeater_Matters.DataBind();
            //}
            //else
            //{
            //    Repeater_Matters.DataSource = null;
            //    Repeater_Matters.DataBind();
            //}

            DataSet dsMatters = objServiceBook.GetAllMatterCount();
            if (dsMatters.Tables[0].Rows.Count > 0)
            {
                lblMatters.Text = dsMatters.Tables[0].Rows[0]["PENDING COUNT"].ToString();
                Repeater_Matters.DataSource = dsMatters.Tables[0];
                Repeater_Matters.DataBind();
                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = Repeater_Matters.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Matters.FindControl("divBlob");
                    ctrHeader.Visible = false;
                    ctrHead1.Visible = true;

                    foreach (ListViewItem lvRow in Repeater_Matters.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = false;
                        ckattach.Visible = true;
                    }
                }
                else
                {
                    Control ctrHeader = Repeater_Matters.FindControl("divFolder");
                    Control ctrHead1 = Repeater_Matters.FindControl("divBlob");
                    ctrHeader.Visible = true;
                    ctrHead1.Visible = false;

                    foreach (ListViewItem lvRow in Repeater_Matters.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdFolder");
                        Control ckattach = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckattach.Visible = false;
                    }
                }
            }
            else
            {
                lblMatters.Text = "0";
                Repeater_Matters.DataSource = null;
                Repeater_Matters.DataBind();
            }
            #endregion



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PersonalMemoranda.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetFileNamePath(object filename)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
        {
            return ("~/IMAGES/" + filename.ToString());
        }
        else
        {
            return "";
        }
    }

    #region FamilyDocument
    public string GetFileNameFamilyPath(object filename, object FNNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/FAMILY_INFO/" + idno.ToString() + "/FAI_" + FNNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region NomineeDocument
    public string GetFileNameNomineePath(object filename, object NFNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/NOMINATION/" + idno.ToString() + "/NOM_" + NFNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region QualificationDocument
    public string GetFileNameQualificationPath(object filename, object QNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/QUALIFICATION/" + idno.ToString() + "/QUA_" + QNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region DepartmentDocument
    public string GetFileNameDepartmentPath(object filename, object DENO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/DEPARTMENT_EXAMINATION/" + idno.ToString() + "/DEX_" + DENO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region PreviousExperience
    public string GetFileNamePreviousPath(object filename, object PSNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PREVIOUS_SERVICE/" + idno.ToString() + "/PRE_" + PSNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region AdminResponsibiltyDocument
    public string GetFileNameAdminResPath(object filename, object ADMINTRXNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/ADMIN_RESPONSIBLITY/" + idno.ToString() + "/ADM_" + ADMINTRXNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region PublicationDocument
    public string GetFileNamePublicationPath(object filename, object pubtrxno, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PUBLICATION/" + idno.ToString() + "/PUB_" + pubtrxno + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region GuestLecDocument
    public string GetFileNameGuestLecPath(object filename, object INVTRXNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/INVITED_TALK/" + idno.ToString() + "/INV_" + INVTRXNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region TrainingDocument
    public string GetFileNameTrainingPath(object filename, object TNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/TRAINING/" + idno.ToString() + "/TRA_" + TNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion

    #region MatterDocument
    public string GetFileNameMatterPath(object filename, object MNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/MATTER/" + idno.ToString() + "/MAT_" + MNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion

    #region IncrementTerDocument
    public string GetFileNameIncrementTerPath(object filename, object TRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/INCREMENT_N_TERMINATION/" + idno.ToString() + "/INT_" + TRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region PayRevisionDocument
    public string GetFileNamePayRevisionPath(object filename, object PRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PAY_REVISION/" + idno.ToString() + "/PAY_" + PRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region LoanDocument
    public string GetFileNameLoanPath(object filename, object LNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/LOAN_N_ADVANCE/" + idno.ToString() + "/LNA_" + LNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion
    #region ExperienceDocument
    public string GetFileNameExperiencePath(object filename, object SVCNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/EXPERIENCE/" + idno.ToString() + "/SCVE_" + SVCNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    #endregion



    public string GetFileNamePath(object filename, object PSNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PREVIOUS_SERVICE/" + idno.ToString() + "/PRE_" + PSNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    #region Family Particulars Approval
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        Button btnApproval = sender as Button;
        int IDNO = int.Parse(btnApproval.CommandName);
        int FNNO = int.Parse(btnApproval.CommandArgument);
        ViewState["IDNO"] = IDNO;
        ViewState["FNNO"] = FNNO;
        string type = "FamilyParticular";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.FamilyParticularApproval(FNNO, IDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(FNNO, IDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            Clear();
            ShowDetails();
        }
    }

    private void Clear()
    {
        ViewState["FNNO"] = null;
        ViewState["IDNO"] = null;
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        Button btnReject = sender as Button;
        int IDNO = int.Parse(btnReject.CommandName);
        int FNNO = int.Parse(btnReject.CommandArgument);
        ViewState["IDNO"] = IDNO;
        ViewState["FNNO"] = FNNO;
        string type = "FamilyParticular";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.FamilyParticularReject(FNNO, IDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(FNNO, IDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            Clear();
            ShowDetails();
        }
    }
    #endregion

    #region Nomination approval
    protected void btnNomiApproval_Click(object sender, EventArgs e)
    {
        Button btnNomiApproval = sender as Button;
        int NOMIDNO = int.Parse(btnNomiApproval.CommandName);
        int nfno = int.Parse(btnNomiApproval.CommandArgument);
        ViewState["NOMIDNO"] = NOMIDNO;
        ViewState["NFNO"] = nfno;
        string type = "Nomination";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.NominationDetailsApproval(nfno, NOMIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(nfno, NOMIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            NominiClear();
            ShowDetails();
        }
    }

    protected void btnNomiReject_Click(object sender, EventArgs e)
    {
        Button btnNomiReject = sender as Button;
        int NOMIDNO = int.Parse(btnNomiReject.CommandName);
        int nfno = int.Parse(btnNomiReject.CommandArgument);
        ViewState["NOMIDNO"] = NOMIDNO;
        ViewState["NFNO"] = nfno;
        string type = "Nomination";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.NominationDetailsReject(nfno, NOMIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(nfno, NOMIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            NominiClear();
            ShowDetails();
        }
    }

    private void NominiClear()
    {
        ViewState["NOMIDNO"] = null;
        ViewState["NFNO"] = null;
    }
    #endregion

    #region Qualification approval
    protected void btnQualiApproval_Click(object sender, EventArgs e)
    {
        Button btnQualiApproval = sender as Button;
        int QUAIDNO = int.Parse(btnQualiApproval.CommandName);
        int QNO = int.Parse(btnQualiApproval.CommandArgument);
        ViewState["QUAIDNO"] = QUAIDNO;
        ViewState["QNO"] = QNO;
        string type = "Qualification";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.QualificationDetailsApproval(QNO, QUAIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(QNO, QUAIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            QualiClear();
            ShowDetails();
        }
    }

    protected void btnQualiReject_Click(object sender, EventArgs e)
    {
        Button btnQualiReject = sender as Button;
        int QUAIDNO = int.Parse(btnQualiReject.CommandName);
        int QNO = int.Parse(btnQualiReject.CommandArgument);
        ViewState["QUAIDNO"] = QUAIDNO;
        ViewState["QNO"] = QNO;
        string type = "Qualification";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.QualificationDetailsReject(QNO, QUAIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(QNO, QUAIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            QualiClear();
            ShowDetails();
        }
    }

    private void QualiClear()
    {
        ViewState["QUAIDNO"] = null;
        ViewState["QNO"] = null;
    }
    #endregion

    #region Department Examination
    protected void btnDepartApproval_Click(object sender, EventArgs e)
    {
        Button btnDepartApproval = sender as Button;
        int DENO = int.Parse(btnDepartApproval.CommandArgument);
        int DEPTIDNO = int.Parse(btnDepartApproval.CommandName);

        ViewState["DEPTIDNO"] = DEPTIDNO;
        ViewState["DENO"] = DENO;
        string type = "DepartmentExam";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.DeptExamDetailsApproval(DENO, DEPTIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(DENO, DEPTIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            DeptClear();
            ShowDetails();
        }
    }
    protected void btnDepartReject_Click(object sender, EventArgs e)
    {
        Button btnDepartReject = sender as Button;
        int DENO = int.Parse(btnDepartReject.CommandArgument);
        int DEPTIDNO = int.Parse(btnDepartReject.CommandName);

        ViewState["DEPTIDNO"] = DEPTIDNO;
        ViewState["DENO"] = DENO;
        string type = "DepartmentExam";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.DeptExamDetailsReject(DENO, DEPTIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(DENO, DEPTIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            DeptClear();
            ShowDetails();
        }
    }

    private void DeptClear()
    {
        ViewState["DEPTIDNO"] = null;
        ViewState["DENO"] = null;
    }

    #endregion

    #region Previous Exp
    protected void btnprevexpApproval_Click(object sender, EventArgs e)
    {
        Button btnprevexpApproval = sender as Button;
        int psno = int.Parse(btnprevexpApproval.CommandArgument);
        int PREVIDNO = int.Parse(btnprevexpApproval.CommandName);

        ViewState["PREVIDNO"] = PREVIDNO;
        ViewState["psno"] = psno;
        string type = "PreviousExp";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.PrevExpDetailsApproval(psno, PREVIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(psno, PREVIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            PreviousClear();
            ShowDetails();
        }
    }

    protected void btnprevexpReject_Click(object sender, EventArgs e)
    {
        Button btnprevexpReject = sender as Button;
        int psno = int.Parse(btnprevexpReject.CommandArgument);
        int PREVIDNO = int.Parse(btnprevexpReject.CommandName);

        ViewState["PREVIDNO"] = PREVIDNO;
        ViewState["psno"] = psno;
        string type = "PreviousExp";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.PrevExpDetailsReject(psno, PREVIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(psno, PREVIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            PreviousClear();
            ShowDetails();
        }
    }

    private void PreviousClear()
    {
        ViewState["PREVIDNO"] = null;
        ViewState["psno"] = null;
    }
    #endregion

    #region Administrative Responsibilities
    protected void btnadmrespApproval_Click(object sender, EventArgs e)
    {
        Button btnadmrespApproval = sender as Button;
        int ADMINTRXNO = int.Parse(btnadmrespApproval.CommandArgument);
        int ADMIDNO = int.Parse(btnadmrespApproval.CommandName);

        ViewState["ADMIDNO"] = ADMIDNO;
        ViewState["ADMINTRXNO"] = ADMINTRXNO;
        string type = "AdminResp";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.AdminResponDetailsApproval(ADMINTRXNO, ADMIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(ADMINTRXNO, ADMIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            AdmRespClear();
            ShowDetails();
        }
    }
    protected void btnadmrespReject_Click(object sender, EventArgs e)
    {
        Button btnadmrespReject = sender as Button;
        int ADMINTRXNO = int.Parse(btnadmrespReject.CommandArgument);
        int ADMIDNO = int.Parse(btnadmrespReject.CommandName);

        ViewState["ADMIDNO"] = ADMIDNO;
        ViewState["ADMINTRXNO"] = ADMINTRXNO;
        string type = "AdminResp";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.AdminResponDetailsReject(ADMINTRXNO, ADMIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(ADMINTRXNO, ADMIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            AdmRespClear();
            ShowDetails();
        }
    }

    private void AdmRespClear()
    {
        ViewState["ADMINTRXNO"] = null;
        ViewState["ADMIDNO"] = null;
    }

    #endregion

    #region Publication Details

    protected void btnpublicationApproval_Click(object sender, EventArgs e)
    {
        Button btnpublicationApproval = sender as Button;
        int PUBTRXNO = int.Parse(btnpublicationApproval.CommandArgument);
        int PUBIDNO = int.Parse(btnpublicationApproval.CommandName);

        ViewState["PUBIDNO"] = PUBIDNO;
        ViewState["PUBTRXNO"] = PUBTRXNO;
        string type = "PublicationDetail";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.PublicationDetailsApproval(PUBTRXNO, PUBIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(PUBTRXNO, PUBIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            PublicationClear();
            ShowDetails();
        }
    }

    protected void btnpublicationReject_Click(object sender, EventArgs e)
    {
        Button btnpublicationReject = sender as Button;
        int PUBTRXNO = int.Parse(btnpublicationReject.CommandArgument);
        int PUBIDNO = int.Parse(btnpublicationReject.CommandName);

        ViewState["PUBIDNO"] = PUBIDNO;
        ViewState["PUBTRXNO"] = PUBTRXNO;
        string type = "PublicationDetail";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.PublicationDetailsReject(PUBTRXNO, PUBIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(PUBTRXNO, PUBIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            PublicationClear();
            ShowDetails();
        }
    }

    private void PublicationClear()
    {
        ViewState["PUBTRXNO"] = null;
        ViewState["PUBIDNO"] = null;
    }

    #endregion

    #region Training Attended
    protected void btntrainingApproval_Click(object sender, EventArgs e)
    {
        Button btntrainingApproval = sender as Button;
        int tno = int.Parse(btntrainingApproval.CommandArgument);
        int TRATTIDNO = int.Parse(btntrainingApproval.CommandName);

        ViewState["tno"] = tno;
        ViewState["TRATTIDNO"] = TRATTIDNO;
        string type = "TrainingAttended";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.TrainingAttendedDetailsApproval(tno, TRATTIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(tno, TRATTIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            TrainingAttendClear();
            ShowDetails();
        }
    }

    protected void btntrainingReject_Click(object sender, EventArgs e)
    {

        Button btntrainingReject = sender as Button;
        int tno = int.Parse(btntrainingReject.CommandArgument);
        int TRATTIDNO = int.Parse(btntrainingReject.CommandName);

        ViewState["tno"] = tno;
        ViewState["TRATTIDNO"] = TRATTIDNO;
        string type = "TrainingAttended";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.TrainingAttendedDetailsReject(tno, TRATTIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(tno, TRATTIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            TrainingAttendClear();
            ShowDetails();
        }
    }

    private void TrainingAttendClear()
    {
        ViewState["tno"] = null;
        ViewState["TRATTIDNO"] = null;
    }

    #endregion

    #region Guest Lecture
    protected void btnGuestApproval_Click(object sender, EventArgs e)
    {
        Button btnGuestApproval = sender as Button;
        int INVTRXNO = int.Parse(btnGuestApproval.CommandArgument);
        int INVTIDNO = int.Parse(btnGuestApproval.CommandName);

        ViewState["INVTRXNO"] = INVTRXNO;
        ViewState["INVTIDNO"] = INVTIDNO;
        string type = "GuestLecture";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.GuestLectureDetailsApproval(INVTRXNO, INVTIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(INVTRXNO, INVTIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            GuestLectureClear();
            ShowDetails();
        }
    }

    protected void btnGuessReject_Click(object sender, EventArgs e)
    {
        Button btnGuessReject = sender as Button;
        int INVTRXNO = int.Parse(btnGuessReject.CommandArgument);
        int INVTIDNO = int.Parse(btnGuessReject.CommandName);

        ViewState["INVTRXNO"] = INVTRXNO;
        ViewState["INVTIDNO"] = INVTIDNO;
        string type = "GuestLecture";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.GuestLectureDetailsReject(INVTRXNO, INVTIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(INVTRXNO, INVTIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            GuestLectureClear();
            ShowDetails();
        }
    }

    private void GuestLectureClear()
    {
        ViewState["INVTRXNO"] = null;
        ViewState["INVTIDNO"] = null;
    }

    #endregion

    #region TrainingConducted
    protected void btntraincondApproval_Click(object sender, EventArgs e)
    {
        Button btntraincondApproval = sender as Button;
        int tno = int.Parse(btntraincondApproval.CommandArgument);
        int TRCONIDNO = int.Parse(btntraincondApproval.CommandName);

        ViewState["tno"] = tno;
        ViewState["TRCONIDNO"] = TRCONIDNO;
        string type = "TrainingConducted";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.TrainingConductedDetailsApproval(tno, TRCONIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(tno, TRCONIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            TrainingConductedClear();
            ShowDetails();
        }
    }

    protected void btntrainingcondReject_Click(object sender, EventArgs e)
    {
        Button btntrainingcondReject = sender as Button;
        int tno = int.Parse(btntrainingcondReject.CommandArgument);
        int TRCONIDNO = int.Parse(btntrainingcondReject.CommandName);

        ViewState["tno"] = tno;
        ViewState["TRCONIDNO"] = TRCONIDNO;
        string type = "TrainingConducted";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.TrainingConductedDetailsReject(tno, TRCONIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(tno, TRCONIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            TrainingConductedClear();
            ShowDetails();
        }
    }

    private void TrainingConductedClear()
    {
        ViewState["tno"] = null;
        ViewState["TRCONIDNO"] = null;
    }
    #endregion

    #region Consultancy
    protected void btnConsultancyApproval_Click(object sender, EventArgs e)
    {
        Button btnConsultancyApproval = sender as Button;
        int SCNO = int.Parse(btnConsultancyApproval.CommandArgument);
        int SCIDNO = int.Parse(btnConsultancyApproval.CommandName);

        ViewState["SCNO"] = SCNO;
        ViewState["SCIDNO"] = SCIDNO;
        string type = "Consultancy";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.ConsultancyDetailsApproval(SCNO, SCIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(SCNO, SCIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            ConsultancyClear();
            ShowDetails();
        }
    }

    protected void btnConsultancyReject_Click(object sender, EventArgs e)
    {
        Button btnConsultancyReject = sender as Button;
        int SCNO = int.Parse(btnConsultancyReject.CommandArgument);
        int SCIDNO = int.Parse(btnConsultancyReject.CommandName);

        ViewState["SCNO"] = SCNO;
        ViewState["SCIDNO"] = SCIDNO;
        string type = "Consultancy";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.ConsultancyDetailsReject(SCNO, SCIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(SCNO, SCIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            ConsultancyClear();
            ShowDetails();
        }
    }

    private void ConsultancyClear()
    {
        ViewState["SCNO"] = null;
        ViewState["SCIDNO"] = null;
    }

    #endregion

    #region Accomplishment
    protected void btnAccomplishmentApproval_Click(object sender, EventArgs e)
    {
        Button btnAccomplishmentApproval = sender as Button;
        int ACNO = int.Parse(btnAccomplishmentApproval.CommandArgument);
        int ACIDNO = int.Parse(btnAccomplishmentApproval.CommandName);

        ViewState["ACNO"] = ACNO;
        ViewState["ACIDNO"] = ACIDNO;
        string type = "Accomplishment";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.AccomplishmentDetailsApproval(ACNO, ACIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(ACNO, ACIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            ConsultancyClear();
            ShowDetails();
        }
    }

    protected void btnAccomplishmentReject_Click(object sender, EventArgs e)
    {
        Button btnAccomplishmentReject = sender as Button;
        int ACNO = int.Parse(btnAccomplishmentReject.CommandArgument);
        int ACIDNO = int.Parse(btnAccomplishmentReject.CommandName);

        ViewState["ACNO"] = ACNO;
        ViewState["ACIDNO"] = ACIDNO;
        string type = "Accomplishment";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.AccomplishmentDetailsReject(ACNO, ACIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(ACNO, ACIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            ConsultancyClear();
            ShowDetails();
        }
    }

    private void AccomplishmentClear()
    {
        ViewState["ACNO"] = null;
        ViewState["ACIDNO"] = null;
    }

    #endregion

    #region Professional Membership
    protected void btnmemberApproval_Click(object sender, EventArgs e)
    {
        Button btnmemberApproval = sender as Button;
        int MPNO = int.Parse(btnmemberApproval.CommandArgument);
        int MPIDNO = int.Parse(btnmemberApproval.CommandName);

        ViewState["MPNO"] = MPNO;
        ViewState["MPIDNO"] = MPIDNO;
        string type = "ProfessionalMembership";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.ProfMemberDetailsApproval(MPNO, MPIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(MPNO, MPIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            MembershipClear();
            ShowDetails();
        }
    }

    protected void btnmemberReject_Click(object sender, EventArgs e)
    {
        Button btnmemberReject = sender as Button;
        int MPNO = int.Parse(btnmemberReject.CommandArgument);
        int MPIDNO = int.Parse(btnmemberReject.CommandName);

        ViewState["MPNO"] = MPNO;
        ViewState["MPIDNO"] = MPIDNO;
        string type = "ProfessionalMembership";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.ProfMemberDetailsReject(MPNO, MPIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(MPNO, MPIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            MembershipClear();
            ShowDetails();
        }
    }

    private void MembershipClear()
    {
        ViewState["MPNO"] = null;
        ViewState["MPIDNO"] = null;
    }

    #endregion

    #region Funded Project
    protected void btnFundedApproval_Click(object sender, EventArgs e)
    {
        Button btnFundedApproval = sender as Button;
        int SFNO = int.Parse(btnFundedApproval.CommandArgument);
        int SFIDNO = int.Parse(btnFundedApproval.CommandName);

        ViewState["SFNO"] = SFNO;
        ViewState["SFIDNO"] = SFIDNO;
        string type = "FundedProject";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.StaffFundedDetailsApproval(SFNO, SFIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(SFNO, SFIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            FundedProjectClear();
            ShowDetails();
        }
    }

    protected void btnFundedReject_Click(object sender, EventArgs e)
    {
        Button btnFundedReject = sender as Button;
        int SFNO = int.Parse(btnFundedReject.CommandArgument);
        int SFIDNO = int.Parse(btnFundedReject.CommandName);

        ViewState["SFNO"] = SFNO;
        ViewState["SFIDNO"] = SFIDNO;
        string type = "FundedProject";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.StaffFundedDetailsReject(SFNO, SFIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(SFNO, SFIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            FundedProjectClear();
            ShowDetails();
        }
    }

    private void FundedProjectClear()
    {
        ViewState["SFNO"] = null;
        ViewState["SFIDNO"] = null;
    }

    #endregion

    #region Patent
    protected void btnPatentApproval_Click(object sender, EventArgs e)
    {
        Button btnPatentApproval = sender as Button;
        int PCNO = int.Parse(btnPatentApproval.CommandArgument);
        int PCIDNO = int.Parse(btnPatentApproval.CommandName);

        ViewState["PCNO"] = PCNO;
        ViewState["PCIDNO"] = PCIDNO;
        string type = "Patent";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.PatentDetailsApproval(PCNO, PCIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(PCNO, PCIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            PatentClear();
            ShowDetails();
        }
    }

    protected void btnPatentReject_Click(object sender, EventArgs e)
    {
        Button btnPatentReject = sender as Button;
        int PCNO = int.Parse(btnPatentReject.CommandArgument);
        int PCIDNO = int.Parse(btnPatentReject.CommandName);

        ViewState["PCNO"] = PCNO;
        ViewState["PCIDNO"] = PCIDNO;
        string type = "Patent";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.PatentDetailsReject(PCNO, PCIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(PCNO, PCIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            PatentClear();
            ShowDetails();
        }
    }

    private void PatentClear()
    {
        ViewState["PCNO"] = null;
        ViewState["PCIDNO"] = null;
    }

    #endregion

    #region Institute Experiences
    protected void btnInstituteExperiencesApproval_Click(object sender, EventArgs e)
    {
        Button btnInstituteExperiencesApproval = sender as Button;
        int SVCNO = int.Parse(btnInstituteExperiencesApproval.CommandArgument);
        int SVCIDNO = int.Parse(btnInstituteExperiencesApproval.CommandName);

        ViewState["SVCNO"] = SVCNO;
        ViewState["SVCIDNO"] = SVCIDNO;
        string type = "InstituteExp";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.InstituteExpDetailsApproval(SVCNO, SVCIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(SVCNO, SVCIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            InsExpClear();
            ShowDetails();
        }
    }

    protected void btnIntituteExpreiencesReject_Click(object sender, EventArgs e)
    {
        Button btnIntituteExpreiencesReject = sender as Button;
        int SVCNO = int.Parse(btnIntituteExpreiencesReject.CommandArgument);
        int SVCIDNO = int.Parse(btnIntituteExpreiencesReject.CommandName);

        ViewState["SVCNO"] = SVCNO;
        ViewState["SVCIDNO"] = SVCIDNO;
        string type = "InstituteExp";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.InstituteExpDetailsReject(SVCNO, SVCIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(SVCNO, SVCIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            InsExpClear();
            ShowDetails();
        }
    }

    private void InsExpClear()
    {
        ViewState["SVCNO"] = null;
        ViewState["SVCIDNO"] = null;
    }
    #endregion

    #region Loans and Advance
    protected void btnLoansApproval_Click(object sender, EventArgs e)
    {
        Button btnLoansApproval = sender as Button;
        int lno = int.Parse(btnLoansApproval.CommandArgument);
        int LADIDNO = int.Parse(btnLoansApproval.CommandName);

        ViewState["lno"] = lno;
        ViewState["LADIDNO"] = LADIDNO;
        string type = "LoansAdvance";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.LoansAdvDetailsApproval(lno, LADIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(lno, LADIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            LoansAdvanceClear();
            ShowDetails();
        }
    }

    protected void btnLoansReject_Click(object sender, EventArgs e)
    {
        Button btnLoansReject = sender as Button;
        int lno = int.Parse(btnLoansReject.CommandArgument);
        int LADIDNO = int.Parse(btnLoansReject.CommandName);

        ViewState["lno"] = lno;
        ViewState["LADIDNO"] = LADIDNO;
        string type = "LoansAdvance";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.LoansAdvDetailsReject(lno, LADIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(lno, LADIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            LoansAdvanceClear();
            ShowDetails();
        }
    }

    private void LoansAdvanceClear()
    {
        ViewState["lno"] = null;
        ViewState["LADIDNO"] = null;
    }

    #endregion

    #region Pay Revision
    protected void btnRevisionApproval_Click(object sender, EventArgs e)
    {
        Button btnRevisionApproval = sender as Button;
        int PRNO = int.Parse(btnRevisionApproval.CommandArgument);
        int PRIDNO = int.Parse(btnRevisionApproval.CommandName);

        ViewState["PRNO"] = PRNO;
        ViewState["PRIDNO"] = PRIDNO;
        string type = "PayRevision";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.PayRevisionDetailsApproval(PRNO, PRIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(PRNO, PRIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            PayRevisionClear();
            ShowDetails();
        }
    }

    protected void btnRevisionReject_Click(object sender, EventArgs e)
    {
        Button btnRevisionReject = sender as Button;
        int PRNO = int.Parse(btnRevisionReject.CommandArgument);
        int PRIDNO = int.Parse(btnRevisionReject.CommandName);

        ViewState["PRNO"] = PRNO;
        ViewState["PRIDNO"] = PRIDNO;
        string type = "PayRevision";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.PayRevisionDetailsReject(PRNO, PRIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(PRNO, PRIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            PayRevisionClear();
            ShowDetails();
        }
    }

    private void PayRevisionClear()
    {
        ViewState["PRNO"] = null;
        ViewState["PRIDNO"] = null;
    }
    #endregion

    #region Increment/Termination
    protected void btnIncrementApproval_Click(object sender, EventArgs e)
    {
        Button btnIncrementApproval = sender as Button;
        int TRNO = int.Parse(btnIncrementApproval.CommandArgument);
        int TRIDNO = int.Parse(btnIncrementApproval.CommandName);

        ViewState["TRNO"] = TRNO;
        ViewState["TRIDNO"] = TRIDNO;
        string type = "IncrementTermination";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.IncrementDetailsApproval(TRNO, TRIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(TRNO, TRIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            IncreClear();
            ShowDetails();
        }
    }

    protected void btnIncrementReject_Click(object sender, EventArgs e)
    {
        Button btnIncrementReject = sender as Button;
        int TRNO = int.Parse(btnIncrementReject.CommandArgument);
        int TRIDNO = int.Parse(btnIncrementReject.CommandName);

        ViewState["TRNO"] = TRNO;
        ViewState["TRIDNO"] = TRIDNO;
        string type = "IncrementTermination";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.IncrementDetailsReject(TRNO, TRIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(TRNO, TRIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            IncreClear();
            ShowDetails();
        }
    }

    private void IncreClear()
    {
        ViewState["TRNO"] = null;
        ViewState["TRIDNO"] = null;
    }

    #endregion

    #region Matters
    protected void btnMattersApproval_Click(object sender, EventArgs e)
    {
        Button btnMattersApproval = sender as Button;
        int mno = int.Parse(btnMattersApproval.CommandArgument);
        int MIDNO = int.Parse(btnMattersApproval.CommandName);

        ViewState["mno"] = mno;
        ViewState["MIDNO"] = MIDNO;
        string type = "Matters";
        string STATUS = "A";

        //CustomStatus cs = (CustomStatus)objServiceBook.MattersDetailsApproval(mno, MIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(mno, MIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Approved Successfully");
            MatterClear();
            ShowDetails();
        }
    }

    protected void btnMattersReject_Click(object sender, EventArgs e)
    {
        Button btnMattersApproval = sender as Button;
        int mno = int.Parse(btnMattersApproval.CommandArgument);
        int MIDNO = int.Parse(btnMattersApproval.CommandName);

        ViewState["mno"] = mno;
        ViewState["MIDNO"] = MIDNO;
        string type = "Matters";
        string STATUS = "R";

        //CustomStatus cs = (CustomStatus)objServiceBook.MattersDetailsReject(mno, MIDNO);
        CustomStatus cs = (CustomStatus)objServiceBook.ServiceBookstatusUpdate(mno, MIDNO, type, STATUS);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Rejected Successfully");
            MatterClear();
            ShowDetails();
        }
    }

    private void MatterClear()
    {
        ViewState["mno"] = null;
        ViewState["MIDNO"] = null;
    }

    #endregion

    #region Blob
    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameEmployee";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Download from blob
    protected void imgbtnFamilyParticularPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnNominPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnQualiPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtndeptExamPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnprevExpPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnAdminPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnPublicationPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnTrainingPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnGuestPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnInstExpPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnPayRevPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnIncrementPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnMattersPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnprevExpUniPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnprevExpPGPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender,e);
    }
    protected void imgbtnLoanUndPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }
    protected void imgbtnLoanAffidPreview_Click(object sender, ImageClickEventArgs e)
    {
        DownloadBlob(sender, e);
    }

    private void DownloadBlob(object sender, EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blobpath = dtBlobPic.Rows[0]["Uri"].ToString();
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string Script = string.Empty;
                string DocLink = blobpath;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
   
    #endregion
   
}