using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class TRAININGANDPLACEMENT_TP_Companies_Details : System.Web.UI.Page
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
                fillListview();
                fillListview1();
                fillListview2();
                fillListview3();
                fillListview4();
                fillListview5();
                fillListview6();
                fillListview7();
                fillListview8();
                fillListview9();
                fillListview10();
                fillListview11();
                fillListview12();
                fillListview13();
                fillListview14();
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
               //tab0
               objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");

               //tab1
               objCommon.FillDropDownList(ddllevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab2
               objCommon.FillDropDownList(ddlcomp2, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlsector2, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlCurrDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlCurrlevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab3
               objCommon.FillDropDownList(ddlVISCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlVISSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlVISDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlVISLevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab4
               objCommon.FillDropDownList(ddlIndcompny, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlInvSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlIndDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlIndLevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");
               
               //tab5
               objCommon.FillDropDownList(ddlGuestlectCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlGuestlectsector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlGuestlectDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlGuestlectlevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");


               //tab6
               objCommon.FillDropDownList(ddlFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlfacultyCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlFacultyDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlFacultyLevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab7
               objCommon.FillDropDownList(ddlFPTIFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlFPTICompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlFPTISector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlFPTIDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlFPTILevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab8
               objCommon.FillDropDownList(ddlFOOIFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlFOOICompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlFOOFSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlFOOIDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlFOOILevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab9
               objCommon.FillDropDownList(ddlEPAIFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlEPAICompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlEPAISector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlEPAIDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlEPAILevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab10
               objCommon.FillDropDownList(ddlFTIFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlFTICompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlFTISector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlFTIDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlFTILevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab11
               objCommon.FillDropDownList(ddlFPPFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlFPPCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlFPPSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlFPPDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlFPPLevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab12
               objCommon.FillDropDownList(ddlPAIFFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlPAIFCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlPAIFSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlPAIFDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlPAIFLevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab13
               objCommon.FillDropDownList(ddlSevFaculty, "PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "UA_TYPE=3", "IDNO");
               objCommon.FillDropDownList(ddlSevCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlSevSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS = 1", "");
               objCommon.FillDropDownList(ddlSevDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlSevLevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");

               //tab14
               objCommon.FillDropDownList(ddlSSECompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "");
               objCommon.FillDropDownList(ddlSSEDiscipline, "ACD_TP_RCPIT_DISCRIPONLINE", "ID", "DISCRIPONLINE", "ISACTIVE=1", "");
               objCommon.FillDropDownList(ddlSSELevel, "ACD_TP_LEVEL", "LEVELNO", "LEVELS", "STATUS = 1", "");
           }

    #region Companies Tab 0
           protected void btnSubmitCompanies_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_CMPID = 0;
                   ENttrp.COMPANY_ID = Convert.ToInt32(ddlCompanyName.SelectedValue);
                   ENttrp.SECTOR_ID = Convert.ToInt32(ddlSector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS = txtIncorpStatus.Text.Trim().ToString();
                   ENttrp.ADDRESS = txtAddress.Text.Trim().ToString();
                   ENttrp.WEBSITE = txtWebsite.Text.Trim().ToString();
                   ENttrp.MOBILE_NO = txtMobno.Text.ToString();
                   ENttrp.MANAGER_CONTACT_PERSON_NAME = txtManager.Text.ToString();
                   ENttrp.EMAIL_ID = txtEmailid.Text.Trim().ToString();
                   int chklinkstatus = 0;
                   if (hfdActive.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_CMPID"] == null)
                   {
                       ENttrp.TP_CMPID = 0;
                       ret = conttrp.AddUpdateTpCompanies(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_CMPID = Convert.ToInt32(ViewState["TP_CMPID"].ToString());
                       ret = conttrp.AddUpdateTpCompanies(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearcompany();
                               fillListview();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearcompany();
                               fillListview();
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
          
            private void fillListview()
           {

               DataSet ds = conttrp.GetallTPCompanydata();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvTPCompanies.DataSource = ds;
                   lvTPCompanies.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvTPCompanies.Items)
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
           public void clearcompany()
           {
               ddlCompanyName.SelectedValue = "0";
               ddlSector.SelectedValue = "0";
               txtIncorpStatus.Text = string.Empty;
               txtAddress.Text= string.Empty;
               txtWebsite.Text= string.Empty;
               txtMobno.Text= string.Empty;
               txtManager.Text= string.Empty;
               txtEmailid.Text = string.Empty;
           }
           protected void btnCancelCompanies_Click(object sender, EventArgs e)
           {
               clearcompany();
           }
           protected void btnTPCompaniesEdit_Click(object sender, ImageClickEventArgs e)
           {
               ImageButton btnFDREdit = sender as ImageButton;
               int TP_CMPID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
               ViewState["TP_CMPID"] = TP_CMPID;
               DataSet dt = null;
               dt = conttrp.GetTPCompanies_data(TP_CMPID);
               if (dt.Tables[0].Rows.Count > 0)
               {
                   FillDropDown();
                   ddlCompanyName.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_ID"].ToString();
                   ddlSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_ID"].ToString();
                   txtIncorpStatus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                   txtAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                   txtWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                   txtMobno.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                   txtManager.Text = dt.Tables[0].Rows[0]["MANAGER_CONTACT_PERSON_NAME"].ToString();
                   txtEmailid.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
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
    #endregion

    #region Insert Discipline Tab 1
       
           protected void btnSubmitDiscipline_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.DIS_ID = 0;
                   ENttrp.DISCIPLINE = txtDiscipline.Text.ToString();
                   ENttrp.LEVEL = Convert.ToInt32(ddllevel.SelectedValue);
                   ENttrp.YEAR_OF_INCEPTION = TxtYearOfInception.Text.Trim().ToString();
                   if (txtNumberOfSubstream.Text == "")
                   {
                       ENttrp.NUMBER_OF_SUBDTREAM = 0;
                   }
                   else
                   {
                       ENttrp.NUMBER_OF_SUBDTREAM = Convert.ToInt32(txtNumberOfSubstream.Text.Trim());
                   }
                   if (txtFaculties.Text == "")
                   {
                       ENttrp.NUMBER_OF_FACULTIES = 0;
                   }
                   else
                   {
                       ENttrp.NUMBER_OF_FACULTIES = Convert.ToInt32(txtFaculties.Text.Trim());
                   }
                   if (txtStudenteligible.Text=="")
                   {
                       ENttrp.ELIGIBLE_STUDENT_1 = 0;
                   }
                   else
                   {
                   ENttrp.ELIGIBLE_STUDENT_1 =Convert.ToInt32( txtStudenteligible.Text);
                   }
                   if (txtDiscStudBatch.Text == "")
                   {
                       ENttrp.ELIGIBLE_STUDENT_2 = 0;
                   }
                   else
                   {
                       ENttrp.ELIGIBLE_STUDENT_2 = Convert.ToInt32(txtDiscStudBatch.Text);
                   }
                   int chklinkstatus = 0;
                   if (hfDiscipline.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_DISID"] == null)
                   {
                       ENttrp.TP_CMPID = 0;
                       ret = conttrp.AddUpdateTpDiscipline(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.DIS_ID = Convert.ToInt32(ViewState["TP_DISID"].ToString());
                       ret = conttrp.AddUpdateTpDiscipline(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               ClearDis();
                               fillListview1();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               ClearDis();
                               fillListview1();
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
           protected void btnCancelDiscipline_Click(object sender, EventArgs e)
           {
               ClearDis();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);

           }
           public void ClearDis()
           {
            txtDiscipline.Text=string.Empty;
            ddllevel.SelectedValue="0";
            TxtYearOfInception.Text=string.Empty;
            txtNumberOfSubstream.Text=string.Empty;
             txtFaculties.Text=string.Empty;
             txtStudenteligible.Text=string.Empty;
             txtDiscStudBatch.Text=string.Empty;
           }

           protected void btnTPDisciplineEdit_Click(object sender, ImageClickEventArgs e)
           {
               ImageButton btnFDREdit = sender as ImageButton;
               int TP_DISID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
               ViewState["TP_DISID"] = TP_DISID;
               DataSet dt = null;
               dt = conttrp.GetTPDiscipline_data(TP_DISID);
               if (dt.Tables[0].Rows.Count > 0)
               {
                   FillDropDown();
                   txtDiscipline.Text = dt.Tables[0].Rows[0]["DISCRIPONLINE"].ToString();
                   ddllevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL"].ToString();
                   TxtYearOfInception.Text = dt.Tables[0].Rows[0]["YEAR_OF_INCEPTION"].ToString();
                   txtNumberOfSubstream.Text = dt.Tables[0].Rows[0]["NUMBER_OF_SUBSTREAM"].ToString();
                   txtFaculties.Text = dt.Tables[0].Rows[0]["NUMBER_OF_FACULTIES"].ToString();
                   txtStudenteligible.Text = dt.Tables[0].Rows[0]["ELIGIBLE_STUDENT_1"].ToString();
                   txtDiscStudBatch.Text = dt.Tables[0].Rows[0]["ELIGIBLE_STUDENT_2"].ToString();
                   if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                   {
                       ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat2(true);", true);
                   }
                   else
                   {
                       ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat2(false);", true);
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
               }
           }
           private void fillListview1()
           {

               DataSet ds = conttrp.GetallTPDisciplinedata();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvDiscipline.DataSource = ds;
                   lvDiscipline.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvDiscipline.Items)
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
    #endregion


           #region Curr Tab 2
           protected void btnCurrSubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_CUR_ID = 0;
                   ENttrp.COMPANY_NAME =Convert.ToInt32( ddlcomp2.SelectedValue);
                   ENttrp.COMPANY_SACTOR = Convert.ToInt32(ddlsector2.SelectedValue);
                   ENttrp.INCORPORATION_SACTOR = txtIncSector.Text.Trim().ToString();
                   ENttrp.ADDRESS_CURR =txtCurriculamAddress.Text.Trim();
                   ENttrp.WEBSITE_CURR = txtCurriculamWebsite.Text.Trim();
                   ENttrp.MOBILE_NO_CURR = txtMobileNo.Text.Trim();
                   ENttrp.MANAGER_NAME_CURR = txtManger.Text.Trim();
                   ENttrp.EMAIL_ID_CURR=txtEmailId1.Text.Trim();
                   ENttrp.DISCIPLINE_CURR=Convert.ToInt32(ddlCurrDiscipline.SelectedValue);
                   ENttrp.LEVEL_CURR=Convert.ToInt32(ddlCurrlevel.SelectedValue);
                   ENttrp.FROM_DATE_CURR=Convert.ToDateTime(txtFromDate.Text);
                   ENttrp.TO_DATE_CURR=Convert.ToDateTime(txtToDate.Text);
                   ENttrp.NO_OF_STUDENTS = Convert.ToInt32(txtNoofStudent.Text);

                   int chklinkstatus = 0;
                   if (hfCurriculum.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_CUR_ID"] == null)
                   {
                       ENttrp.TP_CUR_ID = 0;
                       ret = conttrp.AddUpdateTpCurriculum(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_CUR_ID = Convert.ToInt32(ViewState["TP_CUR_ID"].ToString());
                       ret = conttrp.AddUpdateTpCurriculum(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearCurr();
                               fillListview2();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearCurr();
                               fillListview2();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
               }
           }
           protected void btnCurrCancel_Click(object sender, EventArgs e)
           {
               clearCurr();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
           }
           public void clearCurr()
           {
                ddlcomp2.SelectedValue="0";
                ddlsector2.SelectedValue="0";
                txtIncSector.Text=string.Empty;
                txtCurriculamAddress.Text=string.Empty;
                txtCurriculamWebsite.Text=string.Empty;
                txtMobileNo.Text=string.Empty;
                txtManger.Text = string.Empty;
                txtEmailId1.Text = string.Empty;
                ddlCurrDiscipline.SelectedValue = "0";
                ddlCurrlevel.SelectedValue="0";
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                txtNoofStudent.Text = string.Empty;
           }

           private void fillListview2()
           {

               DataSet ds = conttrp.GetallTPCurriculumdata();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvCurriculum.DataSource = ds;
                   lvCurriculum.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvCurriculum.Items)
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

           protected void btnTPCurreculumEdit_Click(object sender, ImageClickEventArgs e)
           {
               ImageButton btnFDREdit = sender as ImageButton;
               int TP_CUR_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
               ViewState["TP_CUR_ID"] = TP_CUR_ID;
               DataSet dt = null;
               dt = conttrp.GetTPCurriculum_data(TP_CUR_ID);
               if (dt.Tables[0].Rows.Count > 0)
               {
                   FillDropDown();
                   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                   ddlsector2.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                   txtIncSector.Text = dt.Tables[0].Rows[0]["INSORPORATION_SECTOR"].ToString();
                   txtCurriculamAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                   txtCurriculamWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                   txtMobileNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                   txtManger.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                   txtEmailId1.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                   ddlCurrDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                   ddlCurrlevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                   txtFromDate.Text = dt.Tables[0].Rows[0]["FROM_DATE"].ToString();
                   txtToDate.Text = dt.Tables[0].Rows[0]["TO_DATE"].ToString();
                   txtNoofStudent.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT"].ToString();
                   

                   if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                   {
                       ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat3(true);", true);
                   }
                   else
                   {
                       ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat3(false);", true);
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
               }
           }
           #endregion

    
    #region Tab 3
           protected void btnSubmitVisVisitingFaculties_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_VIS_ID = 0;
                   ENttrp.COMPANY_NAME_VIS = Convert.ToInt32(ddlVISCompany.SelectedValue);
                   ENttrp.COMPANY_SACTOR_VIS = Convert.ToInt32(ddlVISSector.SelectedValue);
                   ENttrp.INCORPORATION_SACTOR_VIS = txtIncorpSector.Text.Trim().ToString();
                   ENttrp.DESIGNATION = txtDesignation.Text.Trim().ToString();
                   ENttrp.FIRST_NAME = txtFirstName.Text.Trim().ToString();
                   ENttrp.LAST_NAME = txtLastName.Text.Trim().ToString();
                   ENttrp.ADDRESS_VIS = txtVisitingAddress.Text.Trim();
                   ENttrp.WEBSITE_VIS = txtVisitingWebsite.Text.Trim();
                   ENttrp.MOBILE_NO_VIS = txtMobileNo2.Text.Trim();
                   ENttrp.MANAGER_NAME_VIS = txtVisitingManger.Text.Trim();
                   ENttrp.EMAIL_ID_VIS = txtVisitingEmailId.Text.Trim();
                   ENttrp.DISCIPLINE_VIS = Convert.ToInt32(ddlVISDiscipline.SelectedValue);
                   ENttrp.LEVEL_VIS = Convert.ToInt32(ddlVISLevel.SelectedValue);
                   ENttrp.LECTURE_DATE = Convert.ToDateTime(txtVisitingDateOfLecture.Text);
                   ENttrp.NO_OF_STUDENTS_VIS = Convert.ToInt32(txtNoOFStud.Text);

                   int chklinkstatus = 0;
                   if (hfVisFac.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["VIS_ID"] == null)
                   {
                       ENttrp.TP_VIS_ID = 0;
                       ret = conttrp.AddUpdateTpVisitingFaculties(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_VIS_ID = Convert.ToInt32(ViewState["VIS_ID"].ToString());
                       ret = conttrp.AddUpdateTpVisitingFaculties(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearVis();
                               fillListview3();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearVis();
                               fillListview3();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
               }
           }
           protected void btnCancelVisitingFaculties_Click(object sender, EventArgs e)
           {
               clearVis();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
           }

           public void clearVis()
           {
            ddlVISCompany.SelectedValue="0";
               ddlVISSector.SelectedValue="0";
             txtIncorpSector.Text=string.Empty;
                 txtDesignation.Text=string.Empty;
                txtFirstName.Text=string.Empty;
                txtLastName.Text=string.Empty;
                 txtVisitingAddress.Text=string.Empty;
               txtVisitingWebsite.Text=string.Empty;
                txtMobileNo2.Text=string.Empty;
               txtVisitingManger.Text=string.Empty;
               txtVisitingEmailId.Text=string.Empty;
              ddlVISDiscipline.SelectedValue="0";
               ddlVISLevel.SelectedValue="0";
             txtVisitingDateOfLecture.Text=string.Empty;
               txtNoOFStud.Text=string.Empty;



           }

           private void fillListview3()
           {

               DataSet ds = conttrp.GetallTPVisitingdata();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvVisiting.DataSource = ds;
                   lvVisiting.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvVisiting.Items)
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


           protected void btnTPVisitingEdit_Click(object sender, ImageClickEventArgs e)
           {
               ImageButton btnFDREdit = sender as ImageButton;
               int VIS_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
               ViewState["VIS_ID"] = VIS_ID;
               DataSet dt = null;
               dt = conttrp.GetTPVisiting_data(VIS_ID);
               if (dt.Tables[0].Rows.Count > 0)
               {
                   FillDropDown();
                //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                   ddlVISCompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                   ddlVISSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                   txtIncorpSector.Text = dt.Tables[0].Rows[0]["INSORPORATION_SECTOR"].ToString();
                   txtDesignation.Text = dt.Tables[0].Rows[0]["DESIGNATION"].ToString();
                   txtFirstName.Text = dt.Tables[0].Rows[0]["FIRST_NAME"].ToString();
                   txtLastName.Text = dt.Tables[0].Rows[0]["LAST_NAME"].ToString();
                   txtVisitingAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                   txtVisitingWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                   txtMobileNo2.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                   txtVisitingManger.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                   txtVisitingEmailId.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                   ddlVISDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                   ddlVISLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                   txtVisitingDateOfLecture.Text = dt.Tables[0].Rows[0]["LECTURE_DATE"].ToString();
                   txtNoOFStud.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT"].ToString();




                   if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                   {
                       ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat4(true);", true);
                   }
                   else
                   {
                       ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat4(false);", true);
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
               }
           }
    #endregion



           #region Tab 4
           protected void btnSubmitIndVisit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_IV_ID = 0;
                   ENttrp.COMPANY_NAME_IV = Convert.ToInt32(ddlIndcompny.SelectedValue);
                   ENttrp.COMPANY_SACTOR_IV = Convert.ToInt32(ddlInvSector.SelectedValue);
                   ENttrp.INCORPORATION_SACTOR_IV = txtIndustrialIncorpSector.Text.Trim().ToString();
                   ENttrp.ADDRESS_IV = txtAddress3.Text.Trim();
                   ENttrp.WEBSITE_IV = txtIndustrialWebsite.Text.Trim();
                   ENttrp.MOBILE_NO_IV = txtMobNo3.Text.Trim();
                   ENttrp.MANAGER_NAME_IV = txtIndustrialManger.Text.Trim();
                   ENttrp.EMAIL_ID_IV = txtIndustrialEmailId.Text.Trim();
                   ENttrp.DISCIPLINE_IV = Convert.ToInt32(ddlIndDiscipline.SelectedValue);
                   ENttrp.LEVEL_IV = Convert.ToInt32(ddlIndLevel.SelectedValue);
                   ENttrp.FROM_DATE_IV = Convert.ToDateTime(txtIndfromdate.Text);
                   ENttrp.TO_DATE_IV = Convert.ToDateTime(txtIndtodate.Text);
                   ENttrp.NO_OF_STUDENTS_IV = Convert.ToInt32(txtIndustrialNoOfStud.Text);

                   int chklinkstatus = 0;
                   if (hfIndVisit.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_IV_ID"] == null)
                   {
                       ENttrp.TP_VIS_ID = 0;
                       ret = conttrp.AddUpdateTpIndustrialVisit(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_IV_ID = Convert.ToInt32(ViewState["TP_IV_ID"].ToString());
                       ret = conttrp.AddUpdateTpIndustrialVisit(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearIndvisit();
                               fillListview4();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearIndvisit();
                               fillListview4();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
               }
           }
           protected void btnSubmitCancle_Click(object sender, EventArgs e)
           {
               clearIndvisit();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
           }
           public void clearIndvisit()
           {
                ddlIndcompny.SelectedValue ="0";
                ddlInvSector.SelectedValue ="0";
                txtIndustrialIncorpSector.Text =string.Empty;
                txtAddress3.Text =string.Empty;
                txtIndustrialWebsite.Text =string.Empty;
                txtMobNo3.Text =string.Empty;
                txtIndustrialManger.Text =string.Empty;
                txtIndustrialEmailId.Text =string.Empty;
                ddlIndDiscipline.SelectedValue ="0";
                ddlIndLevel.SelectedValue ="0";
                txtIndfromdate.Text =string.Empty;
                txtIndtodate.Text =string.Empty;
                txtIndustrialNoOfStud.Text = string.Empty;

           }
           private void fillListview4()
           {

               DataSet ds = conttrp.GetallTPIndVisit();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvIndVisit.DataSource = ds;
                   lvIndVisit.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvIndVisit.Items)
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
           protected void btnTPIndVisitEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int TP_IV_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["TP_IV_ID"] = TP_IV_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPIndVisit_data(TP_IV_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlIndcompny.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlInvSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtIndustrialIncorpSector.Text = dt.Tables[0].Rows[0]["INSORPORATION_SECTOR"].ToString();
                       txtAddress3.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                       txtIndustrialWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                       txtMobNo3.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                       txtIndustrialManger.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                       txtIndustrialEmailId.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                       ddlIndDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlIndLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtIndfromdate.Text = dt.Tables[0].Rows[0]["FROM_DATE"].ToString();
                       txtIndtodate.Text = dt.Tables[0].Rows[0]["TO_DATE"].ToString();
                       txtIndustrialNoOfStud.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT"].ToString();




                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat5(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat5(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
               }
           }
           #endregion

    #region Tab 5
           protected void btnSubmitGuestlect_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_GL_ID = 0;
                   ENttrp.COMPANY_NAME_GUL = Convert.ToInt32(ddlGuestlectCompany.SelectedValue);
                   ENttrp.COMPANY_SACTOR_GUL = Convert.ToInt32(ddlGuestlectsector.SelectedValue);
                   ENttrp.INCORPORATION_SACTOR_GUL = txtGuestIncorpSector.Text.Trim().ToString();
                   ENttrp.DESIGNATION_GUL=txtGuestDesignation.Text;
                   ENttrp.FIRST_NAME_GUL=txtGuestFirstName.Text;
                   ENttrp.LAST_NAME_GUL = txtGuestLastName.Text;
                   ENttrp.ADDRESS_GUL = txtAddress4.Text.Trim();
                   ENttrp.WEBSITE_GUL = txtGuestWebsite.Text.Trim();
                   ENttrp.MOBILE_NO_GUL = txtGuestMobNo.Text.Trim();
                   ENttrp.MANAGER_NAME_GUL = txtGuestManager.Text.Trim();
                   ENttrp.EMAIL_ID_GUL = txtEmailId4.Text.Trim();
                   ENttrp.DISCIPLINE_GUL = Convert.ToInt32(ddlGuestlectDiscipline.SelectedValue);
                   ENttrp.LEVEL_GUL = Convert.ToInt32(ddlGuestlectlevel.SelectedValue);
                   ENttrp.LECTURE_DATE_GUL = Convert.ToDateTime(txtGuestDateofLecture.Text);
                   ENttrp.NO_OF_STUDENTS_IV = Convert.ToInt32(txtGuestNoOfStudent.Text);

                   int chklinkstatus = 0;
                   if (hfGuestlect.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_GL_ID"] == null)
                   {
                       ENttrp.TP_GL_ID = 0;
                       ret = conttrp.AddUpdateTpGuestLecture(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_GL_ID = Convert.ToInt32(ViewState["TP_GL_ID"].ToString());
                       ret = conttrp.AddUpdateTpGuestLecture(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearguestlect();
                               fillListview5();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearguestlect();
                               fillListview5();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
               }
           }
           protected void btnClearguestlect_Click(object sender, EventArgs e)
           {
               clearguestlect();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
           }

           public void clearguestlect()
           {
               ddlGuestlectCompany.SelectedValue ="0";
              ddlGuestlectsector.SelectedValue ="0";
               txtGuestIncorpSector.Text =string.Empty;
               txtGuestDesignation.Text=string.Empty;
               txtGuestFirstName.Text=string.Empty;
                txtGuestLastName.Text=string.Empty;
               txtAddress4.Text=string.Empty;
               txtGuestWebsite.Text=string.Empty;
              txtGuestMobNo.Text=string.Empty;
                txtGuestManager.Text=string.Empty;
               txtEmailId4.Text=string.Empty;
              ddlGuestlectDiscipline.SelectedValue ="0";
              ddlGuestlectlevel.SelectedValue = "0";
               txtGuestDateofLecture.Text=string.Empty;
              txtGuestNoOfStudent.Text=string.Empty;
           }
           private void fillListview5()
           {

               DataSet ds = conttrp.GetallTPGuestLect();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvguestlect.DataSource = ds;
                   lvguestlect.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvguestlect.Items)
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
           protected void btnTPGuestLectEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int TP_GL_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["TP_GL_ID"] = TP_GL_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPGUESTLECTUR_data(TP_GL_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
   
                       ddlGuestlectCompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlGuestlectsector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtGuestIncorpSector.Text = dt.Tables[0].Rows[0]["INSORPORATION_SECTOR"].ToString();
                       txtGuestDesignation.Text = dt.Tables[0].Rows[0]["DESIGNATION"].ToString();
                       txtGuestFirstName.Text = dt.Tables[0].Rows[0]["FIRST_NAME"].ToString();
                       txtGuestLastName.Text = dt.Tables[0].Rows[0]["LAST_NAME"].ToString();
                       txtAddress4.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                       txtGuestWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                       txtGuestMobNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                       txtGuestManager.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                       txtEmailId4.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                       ddlGuestlectDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlGuestlectlevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtGuestDateofLecture.Text = dt.Tables[0].Rows[0]["LECTURE_DATE"].ToString();
                       txtGuestNoOfStudent.Text = dt.Tables[0].Rows[0]["NUMBER_OF_STUDENT"].ToString();



                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat6(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat6(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);
               }
           }
    #endregion


           #region Tab 6
           protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlFaculty.SelectedValue + "' and UA_TYPE=3", "");
               txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtlinkIndustryID.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);

           }
           protected void btnSubmitfacultylinkindustry_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_FLI_ID = 0;
                   ENttrp.EXTERNAL_FACULTY_FLI = txtExFaculty.Text;
                   ENttrp.FACULTY_ID_FLI =Convert.ToInt32( txtlinkIndustryID.Text);
                   ENttrp.COMPANY_NAME_FLI = Convert.ToInt32(ddlfacultyCompany.SelectedValue);
                   ENttrp.ADDRESS_FLI = txtIndustryAddress.Text.Trim();
                   ENttrp.WEBSITE_FLI = txtWebsite5.Text.Trim();
                   ENttrp.MOBILE_NO_FLI = txtIndustryMobNo.Text.Trim();
                   ENttrp.MANAGER_NAME_FLI = txtIndustryManger.Text.Trim();
                   ENttrp.EMAIL_ID_FLI = txtIndustryEmaiId.Text.Trim();
                   ENttrp.DISCIPLINE_FLI = Convert.ToInt32(ddlFacultyDiscipline.SelectedValue);
                   ENttrp.LEVEL_FLI = Convert.ToInt32(ddlFacultyLevel.SelectedValue);

                   int chklinkstatus = 0;
                   if (hffacultylinkind.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_FLI_ID"] == null)
                   {
                       ENttrp.TP_FLI_ID = 0;
                       ret = conttrp.AddUpdateTpFacultyLinkIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_FLI_ID = Convert.ToInt32(ViewState["TP_FLI_ID"].ToString());
                       ret = conttrp.AddUpdateTpFacultyLinkIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearFacultylinkind();
                               fillListview6();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearFacultylinkind();
                               fillListview6();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
               }
           }
           protected void btncancelfacultylinkindustry_Click(object sender, EventArgs e)
           {
               clearFacultylinkind();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
           }

           public void clearFacultylinkind()
           {
               ddlFaculty.SelectedValue = "0";
               txtExFaculty.Text = string.Empty;
               txtlinkIndustryID.Text = string.Empty;
               ddlfacultyCompany.SelectedValue = "0";
               txtIndustryAddress.Text = string.Empty;
               txtWebsite5.Text = string.Empty;
               txtIndustryMobNo.Text = string.Empty;
               txtIndustryManger.Text = string.Empty;
               txtIndustryEmaiId.Text = string.Empty;
               ddlFacultyDiscipline.SelectedValue = "0";
               ddlFacultyLevel.SelectedValue = "0";
           }
           private void fillListview6()
           {

               DataSet ds = conttrp.GetallTPfacultylink();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvfacultylink.DataSource = ds;
                   lvfacultylink.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvfacultylink.Items)
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
           protected void btnTPfacultyEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int TP_FLI_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["TP_FLI_ID"] = TP_FLI_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPfacultylink_data(TP_FLI_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();

                       txtExFaculty.Text = dt.Tables[0].Rows[0]["EXTERNAL_FACULTY"].ToString();
                       txtlinkIndustryID.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       ddlfacultyCompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       txtIndustryAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                       txtWebsite5.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                       txtIndustryMobNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                       txtIndustryManger.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                       txtIndustryEmaiId.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                       ddlFacultyDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlFacultyLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlFaculty.SelectedValue = txtlinkIndustryID.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat7(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat7(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);
               }
           }
           #endregion

           #region Tab 7
           protected void ddlFPTIFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlFPTIFaculty.SelectedValue + "' and UA_TYPE=3", "");
              // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtFacultyId.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);

           }
           protected void btnsubmitFPTI_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_FPTI_ID = 0;
                   ENttrp.COMPANY_NAME_FPTI = Convert.ToInt32(ddlFPTICompany.SelectedValue);
                   ENttrp.SECTOR_NAME_FPTI = Convert.ToInt32(ddlFPTISector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_FPTI = txtFacltyIncorpStatus.Text;
                   ENttrp.FACULTY_ID_FPIT = Convert.ToInt32(txtFacultyId.Text.Trim());
                   ENttrp.ADDRESS_FPTI = txtFacultyAddress.Text.Trim();
                   ENttrp.WEBSITE_FPTI = txtFacultyWebsite.Text.Trim();
                   ENttrp.MOBILE_NO_FPTI = txtFacultyMobNo.Text.Trim();
                   ENttrp.MANAGER_NAME_FPTI = txtFacultymanger.Text.Trim();
                   ENttrp.EMAIL_ID_FPTI = txtFacultyEmailid.Text;
                   ENttrp.DISCIPLINE_FPTI = Convert.ToInt32(ddlFPTIDiscipline.SelectedValue);
                   ENttrp.LEVEL_FPTI = Convert.ToInt32(ddlFPTILevel.SelectedValue);
                   ENttrp.DATE_OF_LECTURE_FPTI = Convert.ToDateTime(txtFacultyDateofLecture.Text);

                   int chklinkstatus = 0;
                   if (hfFPTI.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["FPTI_ID"] == null)
                   {
                       ENttrp.TP_FPTI_ID = 0;
                       ret = conttrp.AddUpdateTpFacultyProvTranToIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_FPTI_ID = Convert.ToInt32(ViewState["FPTI_ID"].ToString());
                       ret = conttrp.AddUpdateTpFacultyProvTranToIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearFPTI();
                               fillListview7();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearFPTI();
                               fillListview7();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
               }
           }
           protected void btnCancleFPTI_Click(object sender, EventArgs e)
           {
               clearFPTI();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
           }
           public void clearFPTI()
           {
               ddlFPTICompany.SelectedValue = "0";
               ddlFPTISector.SelectedValue = "0";
               txtFacltyIncorpStatus.Text = string.Empty;
               txtFacultyId.Text = string.Empty;
               txtFacultyAddress.Text = string.Empty;
               txtFacultyWebsite.Text = string.Empty;
               txtFacultyMobNo.Text = string.Empty;
               txtFacultymanger.Text = string.Empty;
               txtFacultyEmailid.Text = string.Empty;
               ddlFPTIDiscipline.SelectedValue = "0";
               ddlFPTILevel.SelectedValue = "0";
               txtFacultyDateofLecture.Text = string.Empty;
               ddlFPTIFaculty.SelectedValue = "0";
           }
           private void fillListview7()
           {

               DataSet ds = conttrp.GetallTPFPTI();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvFPTI.DataSource = ds;
                   lvFPTI.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvFPTI.Items)
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

           protected void btnTPFPTIEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int FPTI_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["FPTI_ID"] = FPTI_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPFPTI_data(FPTI_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();


                       ddlFPTICompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlFPTISector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtFacltyIncorpStatus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                       txtFacultyId.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       txtFacultyAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                       txtFacultyWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                       txtFacultyMobNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                       txtFacultymanger.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                       txtFacultyEmailid.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                       ddlFPTIDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlFPTILevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtFacultyDateofLecture.Text = dt.Tables[0].Rows[0]["DATE_OF_LECTURE"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlFPTIFaculty.SelectedValue = txtFacultyId.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat8(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat8(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);
               }
           }
           #endregion

           #region Tab 8
           protected void ddlFOOIFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlFOOIFaculty.SelectedValue + "' and UA_TYPE=3", "");
               // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtOnboardFacultyID.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
           }
           protected void btnFOOISubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_FOOI_ID = 0;
                   ENttrp.COMPANY_NAME_FOOI = Convert.ToInt32(ddlFOOICompany.SelectedValue);
                   ENttrp.SECTOR_NAME_FOOI = Convert.ToInt32(ddlFOOFSector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_FOOI = txtOnboardIncorpStatus.Text;
                   ENttrp.TYPE_OF_BOARD_FOOT = txtOnboardCompany.Text;
                   ENttrp.FACULTY_ID_FOOT = Convert.ToInt32(txtOnboardFacultyID.Text.Trim());
                   ENttrp.ADDRESS_FOOI = txtOnboardAddress.Text.Trim();
                   ENttrp.WEBSITE_FOOI = txtOnaboardWebsite.Text.Trim();
                   ENttrp.MOBILE_NO_FOOI = txtOnboardMobNo.Text.Trim();
                   ENttrp.MANAGER_NAME_FOOI = txtOnboardManger.Text.Trim();
                   ENttrp.EMAIL_ID_FOOI = txtOnboardEmailId.Text;
                   ENttrp.DISCIPLINE_FOOI = Convert.ToInt32(ddlFOOIDiscipline.SelectedValue);
                   ENttrp.LEVEL_FOOI = Convert.ToInt32(ddlFOOILevel.SelectedValue);
                   ENttrp.MEMBER_FOOI = Convert.ToInt32(txtOnboardLevel.Text);

                   int chklinkstatus = 0;
                   if (hfFOOI.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_FOOI_ID"] == null)
                   {
                       ENttrp.TP_FOOI_ID = 0;
                       ret = conttrp.AddUpdateTpFacultyOnboardOfIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_FOOI_ID = Convert.ToInt32(ViewState["TP_FOOI_ID"].ToString());
                       ret = conttrp.AddUpdateTpFacultyOnboardOfIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               FOOIClear(); ;
                               fillListview8();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               FOOIClear(); ;
                               fillListview8();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
               }
           }
           protected void btnFOOICancle_Click(object sender, EventArgs e)
           {
               FOOIClear();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
           }
           public void FOOIClear()
           {
               ddlFOOICompany.SelectedValue = "0";
               ddlFOOFSector.SelectedValue =  "0";
               txtOnboardIncorpStatus.Text = string.Empty;
               txtOnboardCompany.Text =  string.Empty;
               txtOnboardFacultyID.Text = string.Empty;
               txtOnboardAddress.Text =  string.Empty;
               txtOnaboardWebsite.Text = string.Empty;
               txtOnboardMobNo.Text =  string.Empty;
               txtOnboardManger.Text =  string.Empty;
               txtOnboardEmailId.Text =  string.Empty;
               ddlFOOIDiscipline.SelectedValue= "0";
               ddlFOOILevel.SelectedValue = "0";
               txtOnboardLevel.Text = string.Empty;
               ddlFOOIFaculty.SelectedValue = "0";
           }
           private void fillListview8()
           {

               DataSet ds = conttrp.GetallTPFOOI();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   LvFOOI.DataSource = ds;
                   LvFOOI.DataBind();
               }

               foreach (ListViewDataItem dataitem in LvFOOI.Items)
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

           protected void btnTPFOOIEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int TP_FOOI_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["TP_FOOI_ID"] = TP_FOOI_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPFOOI_data(TP_FOOI_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();



                       ddlFOOICompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlFOOFSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtOnboardIncorpStatus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                       txtOnboardCompany.Text = dt.Tables[0].Rows[0]["TYPE_OF_BOARD"].ToString();
                       txtOnboardFacultyID.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       txtOnboardAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                       txtOnaboardWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();
                       txtOnboardMobNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                       txtOnboardManger.Text = dt.Tables[0].Rows[0]["MANAGER_NAME"].ToString();
                       txtOnboardEmailId.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                       ddlFOOIDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlFOOILevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtOnboardLevel.Text = dt.Tables[0].Rows[0]["MEMBER"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlFOOIFaculty.SelectedValue = txtOnboardFacultyID.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat9(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat9(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);
               }
           }
           #endregion

           #region tab 9
           protected void ddlEPAIFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlEPAIFaculty.SelectedValue + "' and UA_TYPE=3", "");
               // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               TxtProgFacId.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
           }
           protected void btnEPAISubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_EPAI_ID = 0;
                   ENttrp.COMPANY_NAME_EPAI = Convert.ToInt32(ddlEPAICompany.SelectedValue);
                   ENttrp.SECTOR_NAME_EPAI = Convert.ToInt32(ddlEPAISector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_EPAI = txtProgIncorpStatus.Text;
                   ENttrp.FACULTY_ID_EPAI =Convert.ToInt32( TxtProgFacId.Text);
                   ENttrp.DISCIPLINE_EPAI = Convert.ToInt32(ddlEPAIDiscipline.SelectedValue);
                   ENttrp.LEVEL_EPAI = Convert.ToInt32(ddlEPAILevel.SelectedValue);
                   ENttrp.PROGRAM_NAME_EPAI = txtProgramName.Text.Trim();
                   ENttrp.FROM_DATE_EPAI =Convert.ToDateTime( txtEPAIFromDate.Text.Trim());
                   ENttrp.TO_DATE_EPAI = Convert.ToDateTime(txtEPAITODate.Text.Trim());
                   ENttrp.NO_OF_EXECUTIVE_ATTEND_COURSES = txtProgExecutive.Text;

                   int chklinkstatus = 0;
                   if (hfEPAI.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_EPAI_ID"] == null)
                   {
                       ENttrp.TP_EPAI_ID = 0;
                       ret = conttrp.AddUpdateTpExecutiveProgramAttendIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_EPAI_ID = Convert.ToInt32(ViewState["TP_EPAI_ID"].ToString());
                       ret = conttrp.AddUpdateTpExecutiveProgramAttendIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearEPAI();
                               fillListview9();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearEPAI();
                               fillListview9();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
               }
           }
           protected void btnEPAICancle_Click(object sender, EventArgs e)
           {
               clearEPAI();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
           }
           public void clearEPAI()
           {
               ddlEPAICompany.SelectedValue ="0";
               ddlEPAISector.SelectedValue ="0";
               txtProgIncorpStatus.Text = string.Empty;
               TxtProgFacId.Text = string.Empty;
               ddlEPAIDiscipline.SelectedValue ="0";
               ddlEPAILevel.SelectedValue ="0";
               txtProgramName.Text = string.Empty;
               txtEPAIFromDate.Text = string.Empty;
               txtEPAITODate.Text = string.Empty;
               txtProgExecutive.Text = string.Empty;
               ddlEPAIFaculty.SelectedValue = "0";
           }
           private void fillListview9()
           {

               DataSet ds = conttrp.GetallTPEPAI();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvEPAI.DataSource = ds;
                   lvEPAI.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvEPAI.Items)
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
           protected void btnTPEPAIEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int TP_EPAI_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["TP_EPAI_ID"] = TP_EPAI_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPEPAI_data(TP_EPAI_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();


                       ddlEPAICompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlEPAISector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtProgIncorpStatus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                       TxtProgFacId.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       ddlEPAIDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlEPAILevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtProgramName.Text = dt.Tables[0].Rows[0]["PROGRAM_NAME"].ToString();
                       txtEPAIFromDate.Text = dt.Tables[0].Rows[0]["FROM_DATE"].ToString();
                       txtEPAITODate.Text = dt.Tables[0].Rows[0]["TO_DATE"].ToString();
                       txtProgExecutive.Text = dt.Tables[0].Rows[0]["NO_OF_EXEC_ATTEND_COURSES"].ToString();


                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlEPAIFaculty.SelectedValue = TxtProgFacId.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat10(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat10(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
               }
           }
           #endregion

           #region tab 10
           protected void ddlFTIFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlFTIFaculty.SelectedValue + "' and UA_TYPE=3", "");
               // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtIndFacultyId.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
           }
           protected void btnFTISubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.TP_FTI_ID = 0;
                   ENttrp.COMPANY_NAME_FTI = Convert.ToInt32(ddlFTICompany.SelectedValue);
                   ENttrp.SECTOR_NAME_FTI = Convert.ToInt32(ddlFTISector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_FTI = txtIndCompIncorp.Text;
                   ENttrp.FACULTY_ID_FTI = Convert.ToInt32(txtIndFacultyId.Text);
                   ENttrp.DISCIPLINE_FTI = Convert.ToInt32(ddlFTIDiscipline.SelectedValue);
                   ENttrp.LEVEL_FTI = Convert.ToInt32(ddlFTILevel.SelectedValue);
                   ENttrp.FROM_DATE_FTI = Convert.ToDateTime(txtETIfromDate.Text.Trim());
                   ENttrp.TO_DATE_FTI = Convert.ToDateTime(txtFTIToDate.Text.Trim());


                   int chklinkstatus = 0;
                   if (hfFTI.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["TP_FTI_ID"] == null)
                   {
                       ENttrp.TP_FTI_ID = 0;
                       ret = conttrp.AddUpdateTpFacultyTrainedByIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.TP_FTI_ID = Convert.ToInt32(ViewState["TP_FTI_ID"].ToString());
                       ret = conttrp.AddUpdateTpFacultyTrainedByIndustry(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearFTI();
                               fillListview10();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearFTI();
                               fillListview10();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
               }
           }

           public void clearFTI()
           {
               ddlFTICompany.SelectedValue = "0";
               ddlFTISector.SelectedValue = "0";
               txtIndCompIncorp.Text = string.Empty;
               ddlFTIFaculty.SelectedValue = "0";
               txtIndFacultyId.Text = string.Empty;
               ddlFTIDiscipline.SelectedValue = "0";
               ddlFTILevel.SelectedValue = "0";
               txtETIfromDate.Text = string.Empty;
               txtFTIToDate.Text = string.Empty;

           }
           protected void btnFTICancle_Click(object sender, EventArgs e)
           {
               clearFTI();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
           }
           private void fillListview10()
           {

               DataSet ds = conttrp.GetallTPFTI();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvFTI.DataSource = ds;
                   lvFTI.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvFTI.Items)
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
           protected void btnTPFTIEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int TP_FTI_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["TP_FTI_ID"] = TP_FTI_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPFTI_data(TP_FTI_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();


                       ddlFTICompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlFTISector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtIndCompIncorp.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                       txtIndFacultyId.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       ddlFTIDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlFTILevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtETIfromDate.Text = dt.Tables[0].Rows[0]["FROM_DATE"].ToString();
                       txtFTIToDate.Text = dt.Tables[0].Rows[0]["TO_DATE"].ToString();


                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlFTIFaculty.SelectedValue = txtIndFacultyId.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat11(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat11(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
               }
           }
           #endregion

           #region tab 11
           protected void ddlFPPFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlFPPFaculty.SelectedValue + "' and UA_TYPE=3", "");
               // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtLeadFacultyId.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
           }
           protected void btnFPPSubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.FPP_ID = 0;
                   ENttrp.COMPANY_NAME_FPP = Convert.ToInt32(ddlFPPCompany.SelectedValue);
                   ENttrp.SECTOR_NAME_FPP = Convert.ToInt32(ddlFPPSector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_FPP = txtLeadIncStaus.Text;
                   ENttrp.FACULTY_ID_FPP = Convert.ToInt32(txtLeadFacultyId.Text);
                   ENttrp.DISCIPLINE_FPP = Convert.ToInt32(ddlFPPDiscipline.SelectedValue);
                   ENttrp.LEVEL_FPP = Convert.ToInt32(ddlFPPLevel.SelectedValue);
                   ENttrp.PATENT_ADOPTION_DATE_FPP = Convert.ToDateTime(txtFPPPatentDate.Text.Trim());
                   if (txtleadPatentno.Text== "")
                   {
                       ENttrp.PATENT_NO_FPP = 0;
                   }
                   else
                   {
                   ENttrp.PATENT_NO_FPP = Convert.ToInt32(txtleadPatentno.Text.Trim());
                   }
                   ENttrp.GRANTED_FPP = txtleadgranted.Text;
                   ENttrp.PATENT_OWNER_EPP =txtLeadPetOwner.Text.Trim();
                   ENttrp.YEAR_EPP = txtLeadYear.Text.Trim();


                   int chklinkstatus = 0;
                   if (hfFPP.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["FPP_ID"] == null)
                   {
                       ENttrp.FPP_ID = 0;
                       ret = conttrp.AddUpdateTpFacultyPatentsLeadingToIndustryProducts(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.FPP_ID = Convert.ToInt32(ViewState["FPP_ID"].ToString());
                       ret = conttrp.AddUpdateTpFacultyPatentsLeadingToIndustryProducts(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearEPP();
                               fillListview11();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearEPP();
                               fillListview11();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }
                   ViewState["FPP_ID"] = null;
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
               }
           }
           protected void btnFPPCancle_Click(object sender, EventArgs e)
           {
               clearEPP();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
           }
           public void clearEPP()
           {
               ddlFPPCompany.SelectedValue = "0";
               ddlFPPSector.SelectedValue = "0";
               txtLeadIncStaus.Text = string.Empty;
               txtLeadFacultyId.Text = string.Empty;
               ddlFPPDiscipline.SelectedValue ="0";
               ddlFPPLevel.SelectedValue ="0";
               txtFPPPatentDate.Text = string.Empty;
               txtleadPatentno.Text =string.Empty;
               txtleadgranted.Text = string.Empty;
               txtLeadPetOwner.Text = string.Empty;
               txtLeadYear.Text = string.Empty;
               ddlFPPFaculty.SelectedValue = "0";

           }
           protected void btnTPFPPEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int FPP_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["FPP_ID"] = FPP_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPFPP_data(FPP_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();



                      ddlFPPCompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                      ddlFPPSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                      txtLeadIncStaus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                      txtLeadFacultyId.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                      ddlFPPDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                      ddlFPPLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                      txtFPPPatentDate.Text = dt.Tables[0].Rows[0]["PATENT_ADOPTION_DATE"].ToString();
                      txtleadPatentno.Text = dt.Tables[0].Rows[0]["PATENT_NO"].ToString();
                      txtleadgranted.Text = dt.Tables[0].Rows[0]["GRANTED"].ToString();
                      txtLeadPetOwner.Text = dt.Tables[0].Rows[0]["PATENT_OWNER"].ToString();
                      txtLeadYear.Text = dt.Tables[0].Rows[0]["YEAR"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                      ddlFPPFaculty.SelectedValue = txtLeadFacultyId.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat12(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat12(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
               }
           }
           private void fillListview11()
           {

               DataSet ds = conttrp.GetallTPFPP();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvFPP.DataSource = ds;
                   lvFPP.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvFPP.Items)
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
           #endregion

          #region tab 12
           protected void ddlPAIFFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlPAIFFaculty.SelectedValue + "' and UA_TYPE=3", "");
               // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtAuthFacultyId.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
           }

           protected void btnPAIFSubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.PAIF_ID = 0;
                   ENttrp.COMPANY_NAME_PAIF = Convert.ToInt32(ddlPAIFCompany.SelectedValue);
                   ENttrp.SECTOR_NAME_PAIF = Convert.ToInt32(ddlPAIFSector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_PAIF = txtAuthIncStatus.Text;
                   ENttrp.FACULTY_ID_PAIF = Convert.ToInt32(txtAuthFacultyId.Text);
                   ENttrp.DISCIPLINE_PAIF = Convert.ToInt32(ddlPAIFDiscipline.SelectedValue);
                   ENttrp.LEVEL_PAIF = Convert.ToInt32(ddlPAIFLevel.SelectedValue);
                   ENttrp.PRESENTED_DATE_PAIF = Convert.ToDateTime(txtAuthDate.Text.Trim());
                   ENttrp.PAPER_TITLE_PAIF = txtAuthPapertitle.Text.Trim();
                   ENttrp.ASSIGNMENT_TYPE_PAIF = txtAuthAssignment.Text;


                   int chklinkstatus = 0;
                   if (hfPAIF.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["PAIF_ID"] == null)
                   {
                       ENttrp.PAIF_ID = 0;
                       ret = conttrp.AddUpdateTpPapersAuthoredToIndustryByFaculty(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.PAIF_ID = Convert.ToInt32(ViewState["PAIF_ID"].ToString());
                       ret = conttrp.AddUpdateTpPapersAuthoredToIndustryByFaculty(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearPAIF();
                               fillListview12();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearPAIF();
                               fillListview12();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
               }
           }
           protected void btnPAIFCancle_Click(object sender, EventArgs e)
           {
               clearPAIF();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
           }
           public void clearPAIF()
           {
               ddlPAIFCompany.SelectedValue = "0";
               ddlPAIFSector.SelectedValue = "0";
               txtAuthIncStatus.Text = string.Empty;
               ddlPAIFFaculty.SelectedValue="0";
               txtAuthFacultyId.Text=string.Empty;
               ddlPAIFDiscipline.SelectedValue="0";
               ddlPAIFLevel.SelectedValue="0";
               txtAuthDate.Text=string.Empty;
               txtAuthAssignment.Text = string.Empty;

           }
           private void fillListview12()
           {

               DataSet ds = conttrp.GetallTPPAIF();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvPAIF.DataSource = ds;
                   lvPAIF.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvPAIF.Items)
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

           protected void btnTPPAIFEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int PAIF_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["PAIF_ID"] = PAIF_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPPAIF_data(PAIF_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();



                       ddlPAIFCompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlPAIFSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtAuthIncStatus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                       txtAuthFacultyId.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       ddlPAIFDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlPAIFLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtAuthDate.Text = dt.Tables[0].Rows[0]["PRESENTED_DATE"].ToString();
                       txtAuthPapertitle.Text = dt.Tables[0].Rows[0]["PAPER_TITLE"].ToString();
                       txtAuthAssignment.Text = dt.Tables[0].Rows[0]["ASSIGNMENT_TYPE"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlPAIFFaculty.SelectedValue = txtAuthFacultyId.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat13(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat13(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
               }
           }
          #endregion

    #region tab 14
           protected void ddlSevFaculty_SelectedIndexChanged(object sender, EventArgs e)
           {
               DataSet EmpDetails = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + ddlSevFaculty.SelectedValue + "' and UA_TYPE=3", "");
               // txtExFaculty.Text = EmpDetails.Tables[0].Rows[0]["FName"].ToString();
               txtSerFacultyId.Text = EmpDetails.Tables[0].Rows[0]["IDNO"].ToString();

               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_13');</script>", false);
           }

           protected void btnSevSubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.SEV_ID = 0;
                   ENttrp.COMPANY_NAME_SEV = Convert.ToInt32(ddlSevCompany.SelectedValue);
                   ENttrp.SECTOR_NAME_SEV = Convert.ToInt32(ddlSevSector.SelectedValue);
                   ENttrp.INCORPORATION_STATUS_SEV = txtSerIncstaus.Text;
                   ENttrp.TYPE_OF_SERVICES_SEV=txtSertype.Text;
                   ENttrp.TITLE_OF_SERVICES_SEV=txtSerTitleser.Text;
                   ENttrp.YEAR_SEV = txtSerYear.Text;
                   ENttrp.FACULTY_ID_SEV = Convert.ToInt32(txtSerFacultyId.Text);
                   ENttrp.DISCIPLINE_SEV = Convert.ToInt32(ddlSevDiscipline.SelectedValue);
                   ENttrp.LEVEL_SEV = Convert.ToInt32(ddlSevLevel.SelectedValue);
                   ENttrp.START_DATE_SEV = Convert.ToDateTime(txtSevStartdate.Text.Trim());
                   ENttrp.FINISH_DATE_SEV = Convert.ToDateTime(txtSevFinishDate.Text);
                   ENttrp.FEE_RECEIVED_FROM_INDUSTRY_SEV = txtAuthfeereceive.Text;


                   int chklinkstatus = 0;
                   if (hfSev.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["SEV_ID"] == null)
                   {
                       ENttrp.SEV_ID = 0;
                       ret = conttrp.AddUpdateTpServices(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.SEV_ID = Convert.ToInt32(ViewState["SEV_ID"].ToString());
                       ret = conttrp.AddUpdateTpServices(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearSev();
                               fillListview13();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearSev();
                               fillListview13();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_13');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_13');</script>", false);
               }
           }
           protected void btnSevCancel_Click(object sender, EventArgs e)
           {
               clearSev();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_13');</script>", false);
           }
           public void clearSev()
           {
                    ddlSevCompany.SelectedValue="0";
                    ddlSevSector.SelectedValue="0";
                    txtSerIncstaus.Text=string.Empty;
                    txtSertype.Text=string.Empty;
                    txtSerTitleser.Text=string.Empty;
                    txtSerYear.Text=string.Empty;
                    ddlSevFaculty.SelectedValue="0";
                    txtSerFacultyId.Text=string.Empty;
                    ddlSevDiscipline.SelectedValue="0";
                    ddlSevLevel.SelectedValue="0";
                    txtSevStartdate.Text=string.Empty;
                    txtSevFinishDate.Text=string.Empty;
                    txtAuthfeereceive.Text = string.Empty;
           }
           private void fillListview13()
           {

               DataSet ds = conttrp.GetallTPSEV();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   LVSEV.DataSource = ds;
                   LVSEV.DataBind();
               }

               foreach (ListViewDataItem dataitem in LVSEV.Items)
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

           protected void btnTPSEVEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int SEV_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["SEV_ID"] = SEV_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPSEV_data(SEV_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();




                       ddlSevCompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       ddlSevSector.SelectedValue = dt.Tables[0].Rows[0]["SECTOR_NO"].ToString();
                       txtSerIncstaus.Text = dt.Tables[0].Rows[0]["INCORPORATION_STATUS"].ToString();
                       txtSertype.Text = dt.Tables[0].Rows[0]["TYPE_OF_SERVICES"].ToString();
                       txtSerTitleser.Text = dt.Tables[0].Rows[0]["TITLE_OF_SERVICES"].ToString();
                       txtSerYear.Text = dt.Tables[0].Rows[0]["YEAR"].ToString();
                       txtSerFacultyId.Text = dt.Tables[0].Rows[0]["FACULTY_ID"].ToString();
                       ddlSevDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlSevLevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtSevStartdate.Text = dt.Tables[0].Rows[0]["START_DATE"].ToString();
                       txtSevFinishDate.Text = dt.Tables[0].Rows[0]["FINISH_DATE"].ToString();
                       txtAuthfeereceive.Text = dt.Tables[0].Rows[0]["FEE_RECEIVED_FROM_INDUSTRY"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                       ddlSevFaculty.SelectedValue = txtSerFacultyId.Text;

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat14(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat14(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_13');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_13');</script>", false);
               }
           }
    #endregion





           #region tab 15
           protected void btnSSESubmit_Click(object sender, EventArgs e)
           {
               try
               {
                   int ret = 0;
                   ENttrp.SSE_ID = 0;
                   ENttrp.STUDENT_FIRST_NAME_SSE = txtstudFName.Text.Trim();
                   ENttrp.STUDENT_LAST_NAME_SSE = txtstudLName.Text.Trim();
                   ENttrp.TYPE_OF_SELF_EMPLOYMENT_SSE = txtstudSelfEmp.Text.Trim();
                   ENttrp.DISCIPLINE_SSE =Convert.ToInt32( ddlSSEDiscipline.SelectedValue);
                   ENttrp.LEVEL_SSE =Convert.ToInt32( ddlSSELevel.SelectedValue);
                   ENttrp.YEAR_SSE = txtStudYear.Text.Trim();
                   ENttrp.COMPANY_NO_SSE = Convert.ToInt32(ddlSSECompany.SelectedValue);
                   ENttrp.ADDRESS_SSE = txtStudAddress.Text.Trim();
                   ENttrp.EMAIL_ID_SSE = txtStudemailid.Text.Trim();
                   ENttrp.MOBILE_NO_SSE = txStudtMonNo.Text.Trim();
                   ENttrp.WEBSITE_SSE = txtStudWebsite.Text.Trim();


                   int chklinkstatus = 0;
                   if (hfSSE.Value == "true")
                   {
                       chklinkstatus = 1;
                   }
                   else
                   {
                       chklinkstatus = 0;
                   }


                   if (ViewState["SSE_ID"] == null)
                   {
                       ENttrp.SSE_ID = 0;
                       ret = conttrp.AddUpdateTpSSE(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }
                   else
                   {
                       ENttrp.SSE_ID = Convert.ToInt32(ViewState["SSE_ID"].ToString());
                       ret = conttrp.AddUpdateTpSSE(ENttrp, Convert.ToInt32(Session["OrgId"].ToString()), chklinkstatus);
                   }

                   switch (ret)
                   {
                       case 1:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Saved Successfully", this.Page);
                               clearSSE();
                               fillListview14();
                               break;
                           }
                       case 2:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Record Update Successfully", this.Page);
                               clearSSE();
                               fillListview14();
                               break;
                           }
                       default:
                           {
                               objCommon.DisplayUserMessage(this.Page, "Error Occurred", this.Page);

                               break;
                           }
                   }

                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_14');</script>", false);
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_14');</script>", false);
               }
           }
           protected void btnSSECancle_Click(object sender, EventArgs e)
           {
               clearSSE();
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_14');</script>", false);
           }
           public void clearSSE()
           {
               txtstudFName.Text = string.Empty;
               txtstudLName.Text = string.Empty;
               txtstudSelfEmp.Text = string.Empty;
               ddlSSEDiscipline.SelectedValue = "0";
               ddlSSELevel.SelectedValue = "0";
               txtStudYear.Text = string.Empty;
               ddlSSECompany.SelectedValue = "0";
               txtStudAddress.Text = string.Empty;
               txtStudemailid.Text = string.Empty;
               txStudtMonNo.Text = string.Empty;
               txtStudWebsite.Text = string.Empty;
             
           }
           private void fillListview14()
           {

               DataSet ds = conttrp.GetallTPSSE();
               if (ds.Tables[0].Rows.Count > 0)
               {
                   lvSSE.DataSource = ds;
                   lvSSE.DataBind();
               }

               foreach (ListViewDataItem dataitem in lvSSE.Items)
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
           protected void btnTPSSEEdit_Click(object sender, ImageClickEventArgs e)
           {
               try
               {
                   ImageButton btnFDREdit = sender as ImageButton;
                   int SSE_ID = Convert.ToInt32(btnFDREdit.CommandArgument.ToString());
                   ViewState["SSE_ID"] = SSE_ID;
                   DataSet dt = null;
                   dt = conttrp.GetTPSSE_data(SSE_ID);
                   if (dt.Tables[0].Rows.Count > 0)
                   {
                       FillDropDown();
                       //   ddlcomp2.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();


                       txtstudFName.Text = dt.Tables[0].Rows[0]["STUDENT_FIRST_NAME"].ToString();
                       txtstudLName.Text = dt.Tables[0].Rows[0]["STUDENT_LAST_NAME"].ToString();
                       txtstudSelfEmp.Text = dt.Tables[0].Rows[0]["TYPE_OF_SELF_EMPLOYMENT"].ToString();
                       ddlSSEDiscipline.SelectedValue = dt.Tables[0].Rows[0]["DISCIPLINE_NO"].ToString();
                       ddlSSELevel.SelectedValue = dt.Tables[0].Rows[0]["LEVEL_NO"].ToString();
                       txtStudYear.Text = dt.Tables[0].Rows[0]["YEAR"].ToString();
                       ddlSSECompany.SelectedValue = dt.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                       txtStudAddress.Text = dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                       txtStudemailid.Text = dt.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                       txStudtMonNo.Text = dt.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                       txtStudWebsite.Text = dt.Tables[0].Rows[0]["WEBSITE"].ToString();

                       //DataSet facultyId =objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "concat(FNAME ,' ',LNAME) as FName", "IDNO='" + txtlinkIndustryID.Text + "'", "IDNO");

                     

                       if (dt.Tables[0].Rows[0]["ISACTIVE"].ToString() == "True")
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat15(true);", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat15(false);", true);
                       }

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_14');</script>", false);
                   }
               }
               catch (Exception ex)
               {

                   if (Convert.ToBoolean(Session["error"]) == true)
                       objCommon.ShowError(Page, "Stud_Search_tp.btnAddProject_Click --> " + ex.Message + " " + ex.StackTrace);
                   else
                       objCommon.ShowError(Page, "Server Unavailable.");
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_14');</script>", false);
               }
           }
           #endregion
          
}
