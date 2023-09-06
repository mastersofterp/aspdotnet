using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Data.Linq;

public partial class TRAININGANDPLACEMENT_Masters_TP_Masters : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    TPTraining ENttrp = new TPTraining();

    TPController conttrp = new TPController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
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
                CheckPageAuthorization();
                FillDropDown();
             
                
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/TP_Masters.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=TP_Masters.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=TP_Masters.aspx");
                }

            }

        }
        else
        {

            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TP_Masters.aspx");
            }

        }
    }
    private void FillDropDown()
    {
        //tab1
        objCommon.FillDropDownList(ddlAccYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        //tab2
        //objCommon.FillDropDownList(DropDownList1, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(DropDownList1, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(DropDownList2, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        objCommon.FillDropDownList(ddlLevel, "acd_tp_level", "LEVELNO", "LEVELS", "STATUS = 1", "");
        objCommon.FillDropDownList(ddlClass, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");
        //tab3
        //objCommon.FillDropDownList(ddlAcademicYear3, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(ddlAcademicYear3, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartment3, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");

        //tab4
        //objCommon.FillDropDownList(DropDownList5, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(DropDownList5, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(DropDownList6, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        objCommon.FillDropDownList(DropDownList7, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");

        //tab5

       // objCommon.FillDropDownList(ddlAcademicYeartab5, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(ddlAcademicYeartab5, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartmenttab5, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        objCommon.FillDropDownList(ddlClassTab5, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");

        //tab6
      //  objCommon.FillDropDownList(ddlAcademicYear6, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(ddlAcademicYear6, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartment6, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");

        //tab7
       // objCommon.FillDropDownList(ddlAcademicYear7, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(ddlAcademicYear7, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartment7, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        objCommon.FillDropDownList(ddlLevel7, "acd_tp_level", "LEVELNO", "LEVELS", "STATUS = 1", "");
        objCommon.FillDropDownList(ddlForeignLanguage, "ACD_TP_Language", "LANGUAGENO", "LANGUAGES", "", "");

        //tab8

        //objCommon.FillDropDownList(ddlAcademicYear8, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(ddlAcademicYear8, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartment8, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");
        objCommon.FillDropDownList(ddlClass8, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");

        //tab9

      //  objCommon.FillDropDownList(ddlAcademicYear9, "ACD_YEAR", "[YEAR]", "YEARNAME", "[YEAR] > 0", "");
        objCommon.FillDropDownList(ddlAcademicYear9, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACTIVE_STATUS > 0", "");
        objCommon.FillDropDownList(ddlDepartment9, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "");

    }

    #region First Tab
    private void fillListview()
    {

        DataSet ds = conttrp.GetallTPMasterdata();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTPDmaster.DataSource = ds;
            lvTPDmaster.DataBind();
        }

        foreach (ListViewDataItem dataitem in lvTPDmaster.Items)
        {
            Label Status = dataitem.FindControl("Label8") as Label;
            string Statuss = (Status.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            ENttrp.TP_ID = 0;
            ENttrp.ACADEMIC_YEAR_ID = Convert.ToInt32(ddlAccYear.SelectedValue);
            ENttrp.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            ENttrp.NAME_OF_ORGNIZATION = txtOrganization.Text.Trim().ToString();
            ENttrp.NAME_OF_MOUS = txtNatureMOU.Text.Trim().ToString();
            ENttrp.BENEFITS_TO_STUDENTS_AND_STAFF = txtBenefits.Text.Trim().ToString();
            ENttrp.COLLABORATION_YEAR = Convert.ToInt32(txtCollaborationYear.Text);
            ENttrp.COLLABORATION_EXPIRED_YEAR = Convert.ToInt32(txtCollaborationExpYear.Text);
            ENttrp.CLAIM = txtClaim.Text.Trim().ToString();
            int chklinkstatus = 0;
            if (hfdActive.Value == "true")
            {
                chklinkstatus = 1;
            }
            else
            {
                chklinkstatus = 0;
            }


            if (ViewState["TP_ID"] == null)
            {
                ENttrp.TP_ID = 0;
                ret = conttrp.AddUpdateTpmaster(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
            }
            else
            {
                ENttrp.TP_ID = Convert.ToInt32(ViewState["TP_ID"].ToString());
                ret = conttrp.AddUpdateTpmaster(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
            }

            switch (ret)
            {
                case 1:
                    {
                        objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                        lvTPDmaster.Visible = false;
                        clear();
                        //fillListview();
                        break;
                    }
                case 2:
                    {
                        objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                        lvTPDmaster.Visible = false;
                        clear();
                       // fillListview();
                        break;
                    }
                default:
                    {
                        objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                        break;
                    }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
    }
    protected void btnFDREdit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnFDREdit = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
        ViewState["TP_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPMaster_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAccYear.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartment.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtOrganization.Text = dt.Tables[0].Rows[0]["NAME_OF_ORGNIZATION"].ToString();
            txtNatureMOU.Text = dt.Tables[0].Rows[0]["NAME_OF_MOUS"].ToString();
            txtBenefits.Text = dt.Tables[0].Rows[0]["BENEFITS_TO_STUDENTS_AND_STAFF"].ToString();
            txtCollaborationYear.Text = dt.Tables[0].Rows[0]["COLLABORATION_YEAR"].ToString();
            txtCollaborationExpYear.Text = dt.Tables[0].Rows[0]["COLLABORATION_EXPIRED_YEAR"].ToString();
            txtClaim.Text = dt.Tables[0].Rows[0]["CLAIM"].ToString();
           // if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
            }


        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillDropDown();
        ddlAccYear.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        txtOrganization.Text = null;
        txtNatureMOU.Text = null;
        txtBenefits.Text = null;
        txtCollaborationYear.Text = null;
        txtCollaborationExpYear.Text = null;
        txtClaim.Text = null;
        lvTPDmaster.DataSource = null;
        lvTPDmaster.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView7.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;
    }
    public void clear()
    {
        FillDropDown();
        ddlAccYear.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        txtOrganization.Text = null;
        txtNatureMOU.Text = null;
        txtBenefits.Text = null;
        txtCollaborationYear.Text = null;
        txtCollaborationExpYear.Text = null;
        txtClaim.Text = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
    }
    #endregion

    #region second Tab
    private void fillTab2Listview()
    {

        DataSet ds = conttrp.GetallTab2Masterdata();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView2.DataSource = ds;
            ListView2.DataBind();
        }
        foreach (ListViewDataItem dataitem in ListView2.Items)
        {
            Label Status = dataitem.FindControl("LabelIS") as Label;
            string Statuss = (Status.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ret = 0;
        ENttrp.Acadyr_ID = Convert.ToInt32(DropDownList1.SelectedValue);
        ENttrp.Tab2Dept_ID = Convert.ToInt32(DropDownList2.SelectedValue);
        ENttrp.NAME_OF_stud = txtNameStudent.Text;
        ENttrp.Company = txtCompany.Text;
        ENttrp.Address = txtAddress.Text;
        ENttrp.ContactPerson = txtContactPerson.Text;
        ENttrp.tEmailID = txtEmailID.Text;
        ENttrp.tMobileNo = txtMobileNo.Text;
        ENttrp.ttxtCompanyWebiste = txtCompanyWebiste.Text;
        ENttrp.ttxtInternshipDuration = txtInternshipDuration.Text;
        ENttrp.tClass = Convert.ToInt32(ddlClass.SelectedValue);
        ENttrp.tLevel = Convert.ToInt32(ddlLevel.SelectedValue);
        ENttrp.tModeInternship = Convert.ToInt32(ddlModeInternship.SelectedValue);
        int chklinkstatus;
        if (hfdActive2.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ViewState["TAB2_ID"] == null)
        {
            ENttrp.TAB2_ID = 0;
            ret = conttrp.AddUpdateTpmastertabtwo(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB2_ID = Convert.ToInt32(ViewState["TAB2_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabtwo(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    ListView2.Visible = false;
                    clear2();
                   // fillTab2Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    ListView2.Visible = false;
                    clear2();
                   // fillTab2Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }
    }

    protected void btnFDREdit_Click1(object sender, ImageClickEventArgs e)
    {

        ImageButton btnFDREdit = sender as ImageButton;
         int Id = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
        ViewState["TAB2_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab2Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            DropDownList1.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            DropDownList2.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtNameStudent.Text = dt.Tables[0].Rows[0]["NAME_OF_STUDENT"].ToString();
            txtCompany.Text = dt.Tables[0].Rows[0]["NAME_OF_COMPANY_INSTITUTE"].ToString();
            txtAddress.Text = dt.Tables[0].Rows[0]["COMPANY_ADDRESS"].ToString();
            txtContactPerson.Text = dt.Tables[0].Rows[0]["CONTACT_PERSON"].ToString();
            txtEmailID.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtMobileNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtCompanyWebiste.Text = dt.Tables[0].Rows[0]["COMPANY_WEBSITE"].ToString();
            txtInternshipDuration.Text = dt.Tables[0].Rows[0]["INTERNSHIP_DURATION"].ToString();
            ddlClass.SelectedValue = dt.Tables[0].Rows[0]["CLASS_NO"].ToString();
            ddlLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
            ddlModeInternship.SelectedValue = dt.Tables[0].Rows[0]["MODE_OF_INTERNSHIP"].ToString();


            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive2(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive2(false);", true);
            }

        }
    }

    public void clear2()
    {
        FillDropDown();
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        txtNameStudent.Text = null;
        txtCompany.Text = null;
        txtAddress.Text = null;
        txtContactPerson.Text = null;
        txtEmailID.Text = null;
        txtMobileNo.Text = null;
        txtCompanyWebiste.Text = null;
        txtInternshipDuration.Text = null;
        ddlClass.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlModeInternship.SelectedIndex = 0;

        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive2(true);", true);
    }

    protected void btntab2Cancel_Click(object sender, EventArgs e)
    {
        FillDropDown();
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        txtNameStudent.Text = null;
        txtCompany.Text = null;
        txtAddress.Text = null;
        txtContactPerson.Text = null;
        txtEmailID.Text = null;
        txtMobileNo.Text = null;
        txtCompanyWebiste.Text = null;
        txtInternshipDuration.Text = null;
        ddlClass.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlModeInternship.SelectedIndex = 0;
        ListView2.DataSource = null;
        ListView2.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive2(true);", true);
        lvTPDmaster.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView7.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;

    }
    #endregion

    #region Third Tab

    private void fillTab3Listview()
    {

        DataSet ds = conttrp.GetallTPMasterdatatab3();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView3.DataSource = ds;
            ListView3.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView3.Items)
        {
            Label Status = dataitem.FindControl("Label42") as Label;
            string Statuss = (Status.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }
        }
    }


    protected void btntabSubmit3_Click(object sender, EventArgs e)
    {
        {
            int ret = 0;
            ENttrp.Acadyr3_ID = Convert.ToInt32(ddlAcademicYear3.SelectedValue);
            ENttrp.Tab3Dept_ID = Convert.ToInt32(ddlDepartment3.SelectedValue);
            ENttrp.NAME_OF_Advisor = txtAdvisor.Text;
            ENttrp.Designation_of_Advisor = txtDesignationAdvisor.Text;
            ENttrp.Advisor_Company_Name = txtAdvisorCampanyName.Text;
            ENttrp.Location = txtLocation.Text;
            ENttrp.tEmailID3 = txtEmailID3.Text;
            ENttrp.tMobileNo3 = txtMobileNo3.Text;
            ENttrp.Expertee_Domain = txtExperteeDomain.Text;
            ENttrp.Credit_Claim = txtCreditClaim.Text;
            ENttrp.Staff_Coordinator = txtStaffCoordinator.Text;

            int chklinkstatus;
            if (hfdActive3.Value == "true")
            {
                chklinkstatus = 1;
            }
            else
            {
                chklinkstatus = 0;
            }
            if (ViewState["TAB3_ID"] == null)
            {
                ENttrp.TAB3_ID = 0;
                ret = conttrp.AddUpdateTpmastertabthree(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
            }
            else
            {
                ENttrp.TAB3_ID = Convert.ToInt32(ViewState["TAB3_ID"].ToString());
                ret = conttrp.AddUpdateTpmastertabthree(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
            }

            switch (ret)
            {
                case 1:
                    {
                        objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                        clear3();
                      //  fillTab3Listview();
                        break;
                    }
                case 2:
                    {
                        objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                        clear3();
                      //  fillTab3Listview();
                        break;
                    }
                default:
                    {
                        objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                        break;
                    }
            }
        }

    }
    protected void btnFDREdittab3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnFDREdittab3 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab3.CommandArgument.ToString());
        ViewState["TAB3_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab3Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAcademicYear3.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartment3.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtAdvisor.Text = dt.Tables[0].Rows[0]["NAME_OF_ADVISOR"].ToString();
            txtDesignationAdvisor.Text = dt.Tables[0].Rows[0]["DESIGNATION_OF_ADVISOR"].ToString();
            txtAdvisorCampanyName.Text = dt.Tables[0].Rows[0]["ADVISOR_COMPANY_NAME"].ToString();
            txtLocation.Text = dt.Tables[0].Rows[0]["LOCATION"].ToString();
            txtEmailID3.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtMobileNo3.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtExperteeDomain.Text = dt.Tables[0].Rows[0]["EXPERT_DOMAIN"].ToString();
            txtCreditClaim.Text = dt.Tables[0].Rows[0]["CREDIT_CLAIM"].ToString();
            txtStaffCoordinator.Text = dt.Tables[0].Rows[0]["MODE_OF_STAFF_COORDINATOR"].ToString();
            // ddlLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
            //  ddlModeInternship.SelectedValue = dt.Tables[0].Rows[0]["MODE_OF_INTERNSHIP"].ToString();


            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive3(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive3(false);", true);
            }

        }

    }
    protected void clear3()
    {
        ddlAcademicYear3.SelectedIndex = 0;
        ddlDepartment3.SelectedIndex = 0;
        txtAdvisor.Text = null;
        txtDesignationAdvisor.Text = null;
        txtAdvisorCampanyName.Text = null;
        txtLocation.Text = null;
        txtEmailID3.Text = null;
        txtMobileNo3.Text = null;
        txtExperteeDomain.Text = null;
        txtCreditClaim.Text = null;
        txtStaffCoordinator.Text = null;
        ListView3.DataSource = null;
        ListView3.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive3(true);", true);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        clear3();
        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView7.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;
    }
    #endregion

    #region tab4
    protected void Button5_Click(object sender, EventArgs e)
    {
        int ret = 0;
        ENttrp.TAB4_ID = 0;
        ENttrp.Acadyr4_ID = Convert.ToInt32(DropDownList5.SelectedValue);
        ENttrp.Tab4Dept_ID = Convert.ToInt32(DropDownList6.SelectedValue);
        ENttrp.NameOfExpert = txtExpert.Text;
        ENttrp.DesignationExpert = txtDesignationExpert.Text;
        ENttrp.ExpertCompanyName = txtExpertCompanyName.Text;
        ENttrp.t4email = TextBox7.Text;
        ENttrp.t4mobile = TextBox8.Text;
        ENttrp.TopicInteraction = txtTopicInteraction.Text;
        // ENttrp.DateInteraction = txtDateInteraction.Text;

        string DateTo = (String.Format("{0:u}", Convert.ToDateTime(txtDateInteraction.Text)));
        DateTo = DateTo.Substring(0, 10);

        ENttrp.DateInteraction = DateTo;
        ENttrp.t4Mode = Convert.ToInt32(ddlMode.SelectedValue);
        ENttrp.t4class = Convert.ToInt32(DropDownList7.SelectedValue);
        ENttrp.StdBenefitted = txtStdBenefitted.Text;
        ENttrp.t4StaffCoordinator = TextBox11.Text;
        int chklinkstatus;
        if (hfdActive4.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ViewState["TAB4_ID"] == null)
        {
            ENttrp.TAB4_ID = 0;
            ret = conttrp.AddUpdateTpmastertabfour(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB4_ID = Convert.ToInt32(ViewState["TAB4_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabfour(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    clear4();
                  //  fillTab4Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    clear4();
                   // fillTab4Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        clear4();
        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView6.Visible = false;
        ListView7.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;
    }

    protected void btnFDREdittab4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnFDREdittab4 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab4.CommandArgument.ToString());
        ViewState["TAB4_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab4Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            DropDownList5.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            DropDownList6.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtExpert.Text = dt.Tables[0].Rows[0]["NAME_OF_EXPERT"].ToString();
            txtDesignationExpert.Text = dt.Tables[0].Rows[0]["DESIGNATION_OF_EXPERT"].ToString();
            txtExpertCompanyName.Text = dt.Tables[0].Rows[0]["EXPERT_COMPANY_NAME"].ToString();
            TextBox7.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            TextBox8.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtTopicInteraction.Text = dt.Tables[0].Rows[0]["TOPIC_OF_INTERACTION"].ToString();
            txtDateInteraction.Text = dt.Tables[0].Rows[0]["DATE_OF_INTERACTION"].ToString();
            ddlMode.SelectedValue = dt.Tables[0].Rows[0]["MODE"].ToString();
            DropDownList7.SelectedValue = dt.Tables[0].Rows[0]["CLASS_NO"].ToString();
            txtStdBenefitted.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT_BENEFITTED"].ToString();
            TextBox11.Text = dt.Tables[0].Rows[0]["NAME_STAFF_COORDINATOR"].ToString();

            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive4(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive4(false);", true);
            }
        }
    }

    protected void clear4()
    {
        DropDownList5.SelectedIndex = 0;
        DropDownList6.SelectedIndex = 0;
        txtExpert.Text = null;
        txtDesignationExpert.Text = null;
        txtExpertCompanyName.Text = null;
        TextBox7.Text = null;
        TextBox8.Text = null;
        txtTopicInteraction.Text = null;
        txtDateInteraction.Text = null;
        ddlMode.SelectedIndex = 0;
        DropDownList7.SelectedIndex = 0;
        txtStdBenefitted.Text = null;
        TextBox11.Text = null;
        ListView4.DataSource = null;
        ListView4.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive4(true);", true);
    }

    protected void fillTab4Listview()
    {
        DataSet ds = conttrp.GetallTPMasterdatatab4();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView4.DataSource = ds;
            ListView4.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView4.Items)
        {
            Label Status = dataitem.FindControl("Label43") as Label;
            string Statuss = (Status.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }
        }
    }
    #endregion


    #region tab5
    protected void btnSubmitTab5_Click(object sender, EventArgs e)
    {
        int ret = 0;
        ENttrp.TAB5_ID = 0;
        ENttrp.Acadyr5_ID = Convert.ToInt32(ddlAcademicYeartab5.SelectedValue);
        ENttrp.Tab5Dept_ID = Convert.ToInt32(ddlDepartmenttab5.SelectedValue);
        ENttrp.NameOfAlumni = txtNameAlumni.Text;
        ENttrp.DesignationofAlumni = txtDesignationAlumni.Text;

        //year of pass out 
        ENttrp.YearofPassout = Convert.ToInt32(txtYearPassout.Text);
        ENttrp.CompanyName5 = txtCompanyName.Text;
        ENttrp.t5email = txtemailidtab5.Text;
        ENttrp.t5mobile = txtmobilenotab5.Text;
        ENttrp.TopicInteraction5 = txtTopicOfInteractiontab5.Text;
        // ENttrp.DateInteraction = txtDateInteraction.Text;

        string DateTo = (String.Format("{0:u}", Convert.ToDateTime(txtDateOfInteractiontab5.Text)));
        DateTo = DateTo.Substring(0, 10);

        ENttrp.DateInteraction5 = DateTo;
        ENttrp.t5Mode = Convert.ToInt32(ddlModetab5.SelectedValue);
        ENttrp.t5class = Convert.ToInt32(ddlClassTab5.SelectedValue);
        ENttrp.StdBenefitted5 = txtStudentBenefittedtab5.Text;
        ENttrp.StaffCoordinator5 = txtStaffCoordinatortab5.Text;
        int chklinkstatus;
        if (hfdActive5.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ViewState["TAB5_ID"] == null)
        {
            ENttrp.TAB5_ID = 0;
            ret = conttrp.AddUpdateTpmastertabfive(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB5_ID = Convert.ToInt32(ViewState["TAB5_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabfive(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    cleartab5();
                   // fillTab5Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    cleartab5();
                    //fillTab5Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }

    }

    protected void btnCancelTab5_Click(object sender, EventArgs e)
    {
        cleartab5();
        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView7.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;
    }


    protected void cleartab5()
    {
        ddlAcademicYeartab5.SelectedIndex = 0;
        ddlDepartmenttab5.SelectedIndex = 0;
        txtNameAlumni.Text = null;
        txtDesignationAlumni.Text = null;
        txtYearPassout.Text = null;
        //year
        txtCompanyName.Text = null;
        txtemailidtab5.Text = null;
        txtmobilenotab5.Text = null;
        txtTopicOfInteractiontab5.Text = null;
        txtDateOfInteractiontab5.Text = null;
        ddlModetab5.SelectedIndex = 0;
        ddlClassTab5.SelectedIndex = 0;
        txtStudentBenefittedtab5.Text = null;
        txtStaffCoordinatortab5.Text = null;
        ListView5.DataSource = null;
        ListView5.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive5(true);", true);
    }

    protected void fillTab5Listview()
    {
        DataSet ds = conttrp.GetallTPMasterdatatab5();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView5.DataSource = ds;
            ListView5.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView5.Items)
        {
            Label Label75 = dataitem.FindControl("Label75") as Label;
            string Statuss = (Label75.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Label75.CssClass = "badge badge-danger";
            }
            else
            {
                Label75.CssClass = "badge badge-success";
            }
        }
    }




    protected void btnFDREdittab5_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnFDREdittab5 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab5.CommandArgument.ToString());
        ViewState["TAB5_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab5Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAcademicYeartab5.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartmenttab5.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtNameAlumni.Text = dt.Tables[0].Rows[0]["NAME_OF_ALUMNI"].ToString();
            txtDesignationAlumni.Text = dt.Tables[0].Rows[0]["DESIGNATION_OF_ALUMNI"].ToString();
            txtYearPassout.Text = dt.Tables[0].Rows[0]["YEAR_OF_PASSOUT"].ToString();
            txtCompanyName.Text = dt.Tables[0].Rows[0]["COMPANY_NAME"].ToString();
            txtemailidtab5.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtmobilenotab5.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtTopicOfInteractiontab5.Text = dt.Tables[0].Rows[0]["TOPIC_OF_INTERACTION"].ToString();
            txtDateOfInteractiontab5.Text = dt.Tables[0].Rows[0]["DATE_OF_INTERACTION"].ToString();
            ddlModetab5.SelectedValue = dt.Tables[0].Rows[0]["MODE_OF_INTERACTION"].ToString();
            ddlClassTab5.SelectedValue = dt.Tables[0].Rows[0]["CLASS_NO"].ToString();
            txtStudentBenefittedtab5.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT_BENEFITTED"].ToString();
            txtStaffCoordinatortab5.Text = dt.Tables[0].Rows[0]["NAME_STAFF_COORDINATOR"].ToString();

            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive5(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive5(false);", true);
            }
        }


    }

    #endregion

    #region tab6





    protected void btnSubmittab6_Click(object sender, EventArgs e)
    {
        int ret = 0;
        ENttrp.TAB6_ID = 0;
        ENttrp.Acadyr6_ID = Convert.ToInt32(ddlAcademicYear6.SelectedValue);
        ENttrp.Tab6Dept_ID = Convert.ToInt32(ddlDepartment6.SelectedValue);
        ENttrp.NameOfEvent = txtEvent.Text;
        ENttrp.NumberofStudentsParticipated = Convert.ToInt32(txtNoStdParticipated.Text);
        ENttrp.Achievement = (txtAchievement.Text);
        ENttrp.AwardAmountinRs = Convert.ToDecimal(txtAmountRS.Text);
        ENttrp.NumberofStudentPlaced = Convert.ToInt32(txtNoStdPlaced.Text);
        ENttrp.FinancialAssistancefromInstitute = Convert.ToInt32(ddlFinancialAss.SelectedValue);
        if (txtFinancialAss.Text!=string.Empty)
        {
        ENttrp.FinancialAssistancefromInstituteinRs = Convert.ToDecimal(txtFinancialAss.Text);
        }
        else
        {
            ENttrp.FinancialAssistancefromInstituteinRs = 0;
        }
        ENttrp.Remark = txtRemark.Text;
        ENttrp.StaffCoordinator6 = txtStaffCoordinator6.Text;
        int chklinkstatus;
        if (hfdActive6.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ddlFinancialAss.SelectedValue == "1")
        {
            if (txtFinancialAss.Text==string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Financial Assistance from Institute in Rs.", this.Page);
                return;
            }
        }
        if (ViewState["TAB6_ID"] == null)
        {
            ENttrp.TAB6_ID = 0;
            ret = conttrp.AddUpdateTpmastertabsix(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB6_ID = Convert.ToInt32(ViewState["TAB6_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabsix(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    cleartab6();
                  //  fillTab6Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    cleartab6();
                  //  fillTab6Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }

    }
    protected void btncancelTab6_Click(object sender, EventArgs e)
    {
        cleartab6();
        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;
    }

    protected void cleartab6()
    {
        ddlAcademicYear6.SelectedIndex = 0;
        ddlDepartment6.SelectedIndex = 0;
        txtEvent.Text = null;
        txtNoStdParticipated.Text = null;
        txtAchievement.Text = null;
        txtAmountRS.Text = null;
        txtNoStdPlaced.Text = null;
        ddlFinancialAss.SelectedIndex = 0;
        txtFinancialAss.Text = null;
        txtRemark.Text = null;
        txtStaffCoordinator6.Text = null;
        ListView6.DataSource = null;
        ListView6.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive6(true);", true);
    }

    protected void fillTab6Listview()
    {
        DataSet ds = conttrp.GetallTPMasterdatatab6();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView6.DataSource = ds;
            ListView6.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView6.Items)
        {
            Label Label75 = dataitem.FindControl("Label68") as Label;
            string Statuss = (Label75.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Label75.CssClass = "badge badge-danger";
            }
            else
            {
                Label75.CssClass = "badge badge-success";
            }
        }
    }

    protected void btnFDREdittab6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnFDREdittab6 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab6.CommandArgument.ToString());
        ViewState["TAB6_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab6Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAcademicYear6.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartment6.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtEvent.Text = dt.Tables[0].Rows[0]["NAME_OF_EVENT"].ToString();
            txtNoStdParticipated.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT_PARTICIPATED"].ToString();
            txtAchievement.Text = dt.Tables[0].Rows[0]["ACHIEVEMENT"].ToString();
            txtAmountRS.Text = dt.Tables[0].Rows[0]["AWARD_AMOUNT"].ToString();
            txtNoStdPlaced.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT_PLACED"].ToString();
            ddlFinancialAss.SelectedValue = Convert.ToInt32(dt.Tables[0].Rows[0]["FINANCIAL_ASSISTANCE_FROM_INSTITUTE"]).ToString();
            txtFinancialAss.Text = dt.Tables[0].Rows[0]["FINANCIAL_ASSISTANCE_FROM_INSTITUTE_AMOUNT"].ToString();
            txtRemark.Text = dt.Tables[0].Rows[0]["REMARK"].ToString();
            txtStaffCoordinator6.Text = dt.Tables[0].Rows[0]["NAME_STAFF_COORDINATOR"].ToString();

            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive6(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive6(false);", true);
            }
        }

    }

    #endregion

    #region tab7



    protected void btnsubmittab7_Click(object sender, EventArgs e)
    {

        int ret = 0;
        ENttrp.TAB7_ID = 0;
        ENttrp.Acadyr7_ID = Convert.ToInt32(ddlAcademicYear7.SelectedValue);
        ENttrp.Tab7Dept_ID = Convert.ToInt32(ddlDepartment7.SelectedValue);
        ENttrp.NameOfStudent = txtNameStd.Text;
        ENttrp.ForeignLanguage = Convert.ToInt32(ddlForeignLanguage.SelectedValue);
        ENttrp.Certification = Convert.ToInt32(ddlCertification.SelectedValue);
        ENttrp.Level7 = Convert.ToInt32(ddlLevel7.SelectedValue);
        ENttrp.LevelofCertification = txtLevelCertification.Text;
        ENttrp.StaffCoordinator7 = txtStaffCoordinator7.Text;
        int chklinkstatus;
        if (hfdActive7.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ViewState["TAB7_ID"] == null)
        {
            ENttrp.TAB7_ID = 0;
            ret = conttrp.AddUpdateTpmastertabseven(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB7_ID = Convert.ToInt32(ViewState["TAB7_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabseven(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    ListView7.Visible = false;
                    cleartab7();
                   // fillTab7Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    ListView7.Visible = false;
                    cleartab7();
                  //  fillTab7Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }



    }
    protected void btncanceltab7_Click(object sender, EventArgs e)
    {

        ddlAcademicYear7.SelectedIndex = 0;
        ddlDepartment7.SelectedIndex = 0;
        txtNameStd.Text = null;
        ddlForeignLanguage.SelectedIndex = 0;
        ddlCertification.SelectedIndex = 0;
        ddlLevel7.SelectedIndex = 0;
        txtLevelCertification.Text = null;
        txtStaffCoordinator7.Text = null;
        ListView7.DataSource = null;
        ListView7.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive7(true);", true);
        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView8.Visible = false;
        ListView9.Visible = false;

    }

    protected void cleartab7()
    {
        ddlAcademicYear7.SelectedIndex = 0;
        ddlDepartment7.SelectedIndex = 0;
        txtNameStd.Text = null;
        ddlForeignLanguage.SelectedIndex = 0;
        ddlCertification.SelectedIndex = 0;
        ddlLevel7.SelectedIndex = 0;
        txtLevelCertification.Text = null;
        txtStaffCoordinator7.Text = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive7(true);", true);
    }

    protected void fillTab7Listview()
    {
        DataSet ds = conttrp.GetallTPMasterdatatab7();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView7.DataSource = ds;
            ListView7.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView7.Items)
        {
            Label Label75 = dataitem.FindControl("Label688") as Label;
            string Statuss = (Label75.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Label75.CssClass = "badge badge-danger";
            }
            else
            {
                Label75.CssClass = "badge badge-success";
            }
        }
    }




    protected void btnFDREdittab7_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnFDREdittab7 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab7.CommandArgument.ToString());
        ViewState["TAB7_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab7Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAcademicYear7.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartment7.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtNameStd.Text = dt.Tables[0].Rows[0]["NAME_OF_STUDENT"].ToString();
            ddlForeignLanguage.SelectedValue = dt.Tables[0].Rows[0]["FOREGIN_LANGUAGE"].ToString();
            ddlCertification.SelectedValue = dt.Tables[0].Rows[0]["CERTIFICATION"].ToString();
            ddlLevel7.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
            txtLevelCertification.Text = dt.Tables[0].Rows[0]["LEVEL_OF_CERTIFICATION"].ToString();
            txtStaffCoordinator7.Text = dt.Tables[0].Rows[0]["NAME_STAFF_COORDINATOR"].ToString();

            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive7(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive7(false);", true);
            }
        }


    }
    #endregion

    #region tab8

    protected void btnsubmittab8_Click(object sender, EventArgs e)
    {

        int ret = 0;
        ENttrp.TAB8_ID = 0;
        ENttrp.Acadyr8_ID = Convert.ToInt32(ddlAcademicYear8.SelectedValue);
        ENttrp.Tab8Dept_ID = Convert.ToInt32(ddlDepartment8.SelectedValue);
        ENttrp.NameOfCompany8 = txtCompanyInstitute.Text;
        ENttrp.Location8 = txtLocations.Text;
        ENttrp.Address8 = txtAddres.Text;
        ENttrp.EmailID8 = txtemailid8.Text;
        ENttrp.MobileNo8 = txtmobileno8.Text;
        ENttrp.Website8 = txtWebsite.Text;
        //ENttrp.DateOfVisit8 = txtDateVisit.Text;
        string DateToVi = (String.Format("{0:u}", Convert.ToDateTime(txtDateVisit.Text)));
        DateToVi = DateToVi.Substring(0, 10);
        ENttrp.DateOfVisit8 = DateToVi;

        ENttrp.Class8 = Convert.ToInt32(ddlClass8.SelectedValue);
        ENttrp.NoofStudentVisited8 = Convert.ToInt32(txtStdVisited.Text);
        ENttrp.StaffCoordinator8 = txtStaffCoordinator8.Text;

        int chklinkstatus;
        if (hfdActive8.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ViewState["TAB8_ID"] == null)
        {
            ENttrp.TAB8_ID = 0;
            ret = conttrp.AddUpdateTpmastertabeight(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB8_ID = Convert.ToInt32(ViewState["TAB8_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabeight(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    ListView8.Visible = false;
                    cleartab8();
                   // fillTab8Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    ListView8.Visible = false;
                    cleartab8();
                 //   fillTab8Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }

    }
    protected void btncanceltab8_Click(object sender, EventArgs e)
    {

        ddlAcademicYear8.SelectedIndex = 0;
        ddlDepartment8.SelectedIndex = 0;
        txtCompanyInstitute.Text = null;
        txtLocations.Text = null;
        txtAddres.Text = null;
        txtemailid8.Text = null;
        txtmobileno8.Text = null;
        txtWebsite.Text = null;
        txtDateVisit.Text = null;
        ddlClass8.SelectedIndex = 0;
        txtStdVisited.Text = null;
        txtStaffCoordinator8.Text = null;
        ListView8.DataSource = null;
        ListView8.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive8(true);", true);

        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView7.Visible = false;
        ListView9.Visible = false;

    }

    protected void cleartab8()
    {
        ddlAcademicYear8.SelectedIndex = 0;
        ddlDepartment8.SelectedIndex = 0;
        txtCompanyInstitute.Text = null;
        txtLocations.Text = null;
        txtAddres.Text = null;
        txtemailid8.Text = null;
        txtmobileno8.Text = null;
        txtWebsite.Text = null;
        txtDateVisit.Text = null;
        ddlClass8.SelectedIndex = 0;
        txtStdVisited.Text = null;
        txtStaffCoordinator8.Text = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive8(true);", true);
    }

    protected void fillTab8Listview()
    {
        DataSet ds = conttrp.GetallTPMasterdatatab8();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView8.DataSource = ds;
            ListView8.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView8.Items)
        {
            Label Label75 = dataitem.FindControl("Label013") as Label;
            string Statuss = (Label75.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Label75.CssClass = "badge badge-danger";
            }
            else
            {
                Label75.CssClass = "badge badge-success";
            }
        }
    }



    protected void btnFDREdittab8_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnFDREdittab8 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab8.CommandArgument.ToString());
        ViewState["TAB8_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab8Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAcademicYear8.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartment8.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtCompanyInstitute.Text = dt.Tables[0].Rows[0]["NAME_OF_COMPANY"].ToString();
            txtLocations.Text = dt.Tables[0].Rows[0]["LOCATION"].ToString();
            txtAddres.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
            txtemailid8.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtmobileno8.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
            txtDateVisit.Text = dt.Tables[0].Rows[0]["DATE_OF_VISIT"].ToString();
            ddlClass8.SelectedValue = dt.Tables[0].Rows[0]["CLASS_NO"].ToString();
            txtStdVisited.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT_VISITED"].ToString();
            txtStaffCoordinator8.Text = dt.Tables[0].Rows[0]["NAME_STAFF_COORDINATOR"].ToString();

            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive8(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive8(false);", true);
            }
        }

    }


    #endregion

    #region tab9
    protected void btnsubmittab9_Click(object sender, EventArgs e)
    {

        int ret = 0;
        ENttrp.TAB9_ID = 0;
        ENttrp.Acadyr9_ID = Convert.ToInt32(ddlAcademicYear9.SelectedValue);
        ENttrp.Tab9Dept_ID = Convert.ToInt32(ddlDepartment9.SelectedValue);
        ENttrp.NameOfCompany9 = txtNameCompany9.Text;
        ENttrp.Location9 = txtLocation9.Text;
        string DateToVi9 = (String.Format("{0:u}", Convert.ToDateTime(txtDateofInteraction9.Text)));
        DateToVi9 = DateToVi9.Substring(0, 10);
        ENttrp.DateOfInteraction9 = DateToVi9;
        ENttrp.Mode9 = Convert.ToInt32(ddlMode9.SelectedValue);
        ENttrp.NoofStudentBenefitted9 = txtNoofStudentBenefitted.Text;
        ENttrp.StaffCoordinator9 = txtStaffCoordinator9.Text;
        ENttrp.Claim9 = txtClaim9.Text;
        int chklinkstatus;
        if (hfdActive9.Value == "true")
        {
            chklinkstatus = 1;
        }
        else
        {
            chklinkstatus = 0;
        }
        if (ViewState["TAB9_ID"] == null)
        {
            ENttrp.TAB9_ID = 0;
            ret = conttrp.AddUpdateTpmastertabnine(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }
        else
        {
            ENttrp.TAB9_ID = Convert.ToInt32(ViewState["TAB9_ID"].ToString());
            ret = conttrp.AddUpdateTpmastertabnine(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
        }

        switch (ret)
        {
            case 1:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                    ListView9.Visible = false;
                    cleartab9();
                   // fillTab9Listview();
                    break;
                }
            case 2:
                {
                    objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                    ListView9.Visible = false;
                    cleartab9();
                  //  fillTab9Listview();
                    break;
                }
            default:
                {
                    objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                    break;
                }
        }

    }
    protected void btncanceltab9_Click(object sender, EventArgs e)
    {
        ddlAcademicYear9.SelectedIndex = 0;
        ddlDepartment9.SelectedIndex = 0;
        txtNameCompany9.Text = null;
        txtLocation9.Text = null;
        txtDateofInteraction9.Text = null;
        ddlMode9.SelectedIndex = 0;
        txtNoofStudentBenefitted.Text = null;
        txtStaffCoordinator9.Text = null;
        txtClaim9.Text = null;
        ListView9.DataSource = null;
        ListView9.DataBind();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive8(true);", true);
        lvTPDmaster.Visible = false;
        ListView2.Visible = false;
        ListView3.Visible = false;
        ListView4.Visible = false;
        ListView5.Visible = false;
        ListView6.Visible = false;
        ListView7.Visible = false;
        ListView8.Visible = false;
    }
    protected void cleartab9()
    {
        ddlAcademicYear9.SelectedIndex = 0;
        ddlDepartment9.SelectedIndex = 0;
        txtNameCompany9.Text = null;
        txtLocation9.Text = null;
        txtDateofInteraction9.Text = null;
        ddlMode9.SelectedIndex = 0;
        txtNoofStudentBenefitted.Text = null;
        txtStaffCoordinator9.Text = null;
        txtClaim9.Text = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive8(true);", true);

    }
    protected void btnFDREdittab9_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnFDREdittab9 = sender as ImageButton;
        int Id = Convert.ToInt32(btnFDREdittab9.CommandArgument.ToString());
        ViewState["TAB9_ID"] = Id;
        DataSet dt = null;
        dt = conttrp.GetTPTab9Master_data(Id);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FillDropDown();
            ddlAcademicYear9.SelectedValue = dt.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
            ddlDepartment9.SelectedValue = dt.Tables[0].Rows[0]["DEPTNO"].ToString();
            txtNameCompany9.Text = dt.Tables[0].Rows[0]["NAME_OF_COMPANY"].ToString();
            txtLocation9.Text = dt.Tables[0].Rows[0]["LOCATIONOF_COMPANY"].ToString();
            txtDateofInteraction9.Text = dt.Tables[0].Rows[0]["DATE_OF_INTERACTION"].ToString();
            ddlMode9.SelectedValue = dt.Tables[0].Rows[0]["MODE"].ToString();
            txtNoofStudentBenefitted.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT_BENEFITTED"].ToString();
            txtStaffCoordinator9.Text = dt.Tables[0].Rows[0]["NAME_STAFF_COORDINATOR"].ToString();
            txtClaim9.Text = dt.Tables[0].Rows[0]["CLAIM"].ToString();

            if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive9(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive9(false);", true);
            }
        }


    }

    protected void fillTab9Listview()
    {
        DataSet ds = conttrp.GetallTPMasterdatatab9();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ListView9.DataSource = ds;
            ListView9.DataBind();
        }

        foreach (ListViewDataItem dataitem in ListView9.Items)
        {
            Label Label75 = dataitem.FindControl("Label211") as Label;
            string Statuss = (Label75.Text.ToString());
            if (Statuss == "INACTIVE")
            {
                Label75.CssClass = "badge badge-danger";
            }
            else
            {
                Label75.CssClass = "badge badge-success";
            }
        }
    }

    #endregion
    protected void btnShow_Click(object sender, EventArgs e)
    {
        fillListview();
        lvTPDmaster.Visible = true;
    }
    protected void btnShow_Click1(object sender, EventArgs e)
    {
        fillTab2Listview();
        ListView2.Visible = true;
    }
    protected void btnShow_Click2(object sender, EventArgs e)
    {
        fillTab3Listview();
        ListView3.Visible = true;
    }
    protected void btnShow4_Click(object sender, EventArgs e)
    {
        fillTab4Listview();
        ListView4.Visible = true;
    }
    protected void btnShow5_Click(object sender, EventArgs e)
    {
        fillTab5Listview();
        ListView5.Visible = true;
    }
    protected void btnShow6_Click(object sender, EventArgs e)
    {
        fillTab6Listview();
        ListView6.Visible = true;
    }
    protected void btnShow7_Click(object sender, EventArgs e)
    {
        fillTab7Listview();
        ListView7.Visible = true;
    }
    protected void btnShow8_Click(object sender, EventArgs e)
    {
        fillTab8Listview();
        ListView8.Visible = true;
    }
    protected void btnShow9_Click(object sender, EventArgs e)
    {
        fillTab9Listview();
        ListView9.Visible = true;
    }
   
}