using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;
using System.IO;



public partial class ACADEMIC_StudentAchievement_Organised_Activity : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OrganisedActivityEntity objOAE = new OrganisedActivityEntity();
    OrganisedActivityController objOAC = new OrganisedActivityController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDropDown();
            BindListView();
        }
    }


    public void FillDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID>0", "ACADMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlEventLevel, "ACD_ACHIEVEMENT_EVENT_LEVEL", "EVENT_LEVEL_ID", "EVENT_LEVEL", "EVENT_LEVEL_ID>0", "EVENT_LEVEL_ID");
            objCommon.FillDropDownList(ddlTypeofActivity, "ACD_ACHIEVEMENT_TECHNICALACTIVITY_TYPE", "TECHNICALACTIVITY_TYPE_ID", "TECHNICALACTIVITY_TYPE", "TECHNICALACTIVITY_TYPE_ID>0", "TECHNICALACTIVITY_TYPE_ID");
            objCommon.FillDropDownList(ddlDuration, "ACD_ACHIEVEMENT_DURATION", "DURATION_ID", "DURATION", "DURATION_ID>0", "DURATION_ID");
            objCommon.FillListBox(lstbxConvener, "User_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 ", "UA_NO");
            objCommon.FillListBox(lstbxCoordinator, "User_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 ", "UA_NO");
            objCommon.FillListBox(lstbxMemberOrg, "User_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 ", "UA_NO");


        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitOrganisedActivity_Click(object sender, EventArgs e)
    {
         
        objOAE.acadamic_year_id = Convert.ToInt32(ddlAcademicYear.SelectedValue);
        objOAE.activity_title = txtTitleofActivity.Text.Trim();
        objOAE.technical_type_id = Convert.ToInt32(ddlTypeofActivity.SelectedValue);
        objOAE.organize_by = txtOrganizeBy.Text.Trim();
        objOAE.conduct_by = txtConductBy.Text.Trim();
        objOAE.event_level_id = Convert.ToInt32(ddlEventLevel.SelectedValue);
        DataTable dt;
        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage("Please select FROM Date TO Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];
        //DateTime dateTime10 = Convert.ToDateTime(a);
        DateTime dtStartDate = DateTime.Parse(StartDate);
        objOAE.SDate = DateTime.Parse(StartDate);
        DateTime dtEndDate = DateTime.Parse(EndDate);
        objOAE.EDate = DateTime.Parse(StartDate);
        objOAE.venue = txtVenue.Text.Trim();
        objOAE.event_mode = ddlMode.SelectedValue;
        objOAE.duration_id = Convert.ToInt32(ddlDuration.SelectedValue);
        objOAE.student_participants_no = Convert.ToInt32(txtStudentParticipants.Text);
        objOAE.teachers_staff_participants_no = Convert.ToInt32(txtTeacherStaffParticipants.Text);
        objOAE.funded_by = txtFundedBy.Text.Trim();
        objOAE.sanctioned_amount = Convert.ToDecimal(txtSanctionedAmount.Text);
        
        objOAE.OrganizationId = Convert.ToInt32(Session["OrgId"]);
       

        string _ua_no = string.Empty;
        int ck = 0;


        foreach (ListItem items in lstbxConvener.Items)
        {
            if (items.Selected == true)
            {

                _ua_no += items.Value + ",";

            }    
        }
        _ua_no = _ua_no.Remove(_ua_no.Length - 1);

        string coordinator = string.Empty;


            foreach (ListItem items in lstbxCoordinator.Items)
            {
                
                    if (items.Selected == true)
                    {

                        coordinator += items.Value + ",";

                    }    
                    
                }
            coordinator = coordinator.Remove(coordinator.Length - 1);


            string member = string.Empty;

           foreach (ListItem items in lstbxMemberOrg.Items)
            {
                    
                    if (items.Selected == true)
                    {

                        member += items.Value + ",";

                    } 
                }
        member = member.Remove(member.Length - 1);
        
        
        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objOAE.organised_activity_id = Convert.ToInt32(ViewState["orid"]);
            CustomStatus cs = (CustomStatus)objOAC.UpdateOrganisedActivity(objOAE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))

              
            BindListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ClearOrganisedActivityData();
            
        }

        else
        {

            CustomStatus cs = (CustomStatus)objOAC.InsertOrganisedActivityData(objOAE, _ua_no, coordinator, member);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListView();
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearOrganisedActivityData();
                
            }
        }
     }
   
    
     
          protected void BindListView()
          {
              try
              {

                  DataSet ds = objOAC.ActivityOrganisedListView();

           if (ds.Tables[0].Rows.Count > 0)
                  {
                      pnlOrg.Visible = true;
                      lvOrg.DataSource = ds.Tables[0];
                      lvOrg.DataBind();
                   }
                  else
                  {
                      pnlOrg.Visible = true;
                      lvOrg.DataSource = null;
                      lvOrg.DataBind();

                  }
              }
              catch (Exception ex)
              {
                  throw;
              }
          }



          protected void lvOrg_ItemEditing(object sender, ListViewEditEventArgs e)
              {

              }
         
          private void ShowDetail(int ACTIVITY_ORGANISED_ID)
              {
                  DataSet ds = objOAC.EditOrganisedActivityData(ACTIVITY_ORGANISED_ID);

           if (ds != null && ds.Tables[0].Rows.Count > 0)
             {
          
            ddlAcademicYear.SelectedValue = ds.Tables[0].Rows[0]["ACADMIC_YEAR_ID"].ToString();
            txtTitleofActivity.Text = ds.Tables[0].Rows[0]["TITLE_OF_ACITIVITY"].ToString();
            ddlTypeofActivity.SelectedValue = ds.Tables[0].Rows[0]["TECHNICALACTIVITY_TYPE_ID"].ToString();
            txtOrganizeBy.Text = ds.Tables[0].Rows[0]["ORGANIZE_BY"].ToString();
            txtConductBy.Text = ds.Tables[0].Rows[0]["CONDUCT_BY"].ToString();
            ddlEventLevel.SelectedValue = ds.Tables[0].Rows[0]["EVENT_LEVEL_ID"].ToString();
            txtVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
            ddlMode.SelectedValue = ds.Tables[0].Rows[0]["EVENT_MODE"].ToString();
            ddlDuration.SelectedValue = ds.Tables[0].Rows[0]["DURATION_ID"].ToString();
            txtStudentParticipants.Text = ds.Tables[0].Rows[0]["STUDENTS_PARTICIPANTS_NO"].ToString();
            txtTeacherStaffParticipants.Text = ds.Tables[0].Rows[0]["TEACHERS_STAFF_PARTICIPATIONS_NO"].ToString();
            txtFundedBy.Text = ds.Tables[0].Rows[0]["FUNDED_BY"].ToString();
            txtSanctionedAmount.Text = ds.Tables[0].Rows[0]["SANCTIONED_AMOUNT"].ToString();
            
               
               string[] cov = ds.Tables[0].Rows[0]["CONVERNER"].ToString().Split(',');
               
                   string converner = string.Empty;
                   foreach (ListItem items in lstbxConvener.Items)
                   {
                       foreach (string s in cov)
                       {
                           if (items.Value == s)
                           {
                               items.Selected = true;
                           }
                       }
                       
                       
                   }


                   string[] mem = ds.Tables[0].Rows[0]["MEMBERS"].ToString().Split(',');

                   string members = string.Empty;
                   foreach (ListItem items in lstbxMemberOrg.Items)
                   {
                       foreach (string m in mem)
                       {
                           if (items.Value == m)
                           {
                               items.Selected = true;
                           }
                       }


                   }


                   string[] ord = ds.Tables[0].Rows[0]["CO_ORDINATOR"].ToString().Split(',');

                   string coordinator = string.Empty;
                   foreach (ListItem items in lstbxCoordinator.Items)
                   {
                       foreach (string o in ord)
                       {
                           if (items.Value == o)
                           {
                               items.Selected = true;
                           }
                       }


                   }

                  // lstbxCoordinator.SelectedValue = ds.Tables[0].Rows[0]["CO_ORDINATOR"].ToString();
          //  lstbxMemberOrg.SelectedValue = ds.Tables[0].Rows[0]["MEMBERS"].ToString();
            
               txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy");
            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            hdnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        }
 }
                   protected void btnEditACtivityOrganised_Click(object sender, EventArgs e)
                   {
                       try
                       {

                           LinkButton btnACtivityOrganised = sender as LinkButton;
                           int ACTIVITY_ORGANISED_ID = Convert.ToInt32(btnACtivityOrganised.CommandArgument);
                           ViewState["orid"] = Convert.ToInt32(btnACtivityOrganised.CommandArgument);
                           ShowDetail(ACTIVITY_ORGANISED_ID);
                           ViewState["action"] = "edit";

                       }
                       catch (Exception ex)
                       {
                           throw;
                       }

                   }
                   protected void btnCancelOrganisedActivity_Click(object sender, EventArgs e)
                   {
                       ClearOrganisedActivityData();
                   }

                   public void ClearOrganisedActivityData()
                   {
                       ViewState["action"] = null;
                       ddlAcademicYear.SelectedIndex = 0;
                       txtTitleofActivity.Text = "";
                       ddlTypeofActivity.SelectedIndex = 0;
                       txtOrganizeBy.Text = "";
                       txtConductBy.Text = "";
                       ddlEventLevel.SelectedIndex = 0;
                       txtVenue.Text = "";
                       ddlMode.SelectedIndex = 0;
                       txtStudentParticipants.Text = "";
                       txtTeacherStaffParticipants.Text = "";
                      lstbxConvener.SelectedIndex = 0;
                      lstbxCoordinator.SelectedIndex = 0;
                       lstbxMemberOrg.SelectedIndex = 0;
                       ddlDuration.SelectedIndex = 0;
                       txtFundedBy.Text = "";
                       txtSanctionedAmount.Text = "";
                       foreach (ListItem items in lstbxConvener.Items)
                       {
                           if (items.Selected == true)
                           {
                               items.Selected = false;

                           }
                       }
                       foreach (ListItem items in lstbxCoordinator.Items)
                       {
                           if (items.Selected == true)
                           {
                               items.Selected = false;

                           }
                       }
                       foreach (ListItem items in lstbxMemberOrg.Items)
                       {
                           if (items.Selected == true)
                           {
                               items.Selected = false;

                           }
                       }
                       
         
     }
                   protected void btnReport_Click(object sender, EventArgs e)
                   {
                       DataGrid Gr = new DataGrid();
                       DataSet ds = new DataSet();
                       ds = objOAC.ActivityOrganisedReport();
                       if (ds.Tables.Count > 0)
                       {
                           if (ds.Tables[0].Rows.Count > 0)
                           {
                               Gr.DataSource = ds;
                               Gr.DataBind();
                               string Attachment = "Attachment; FileName=OrganisedActivityReport.xls";
                               Response.ClearContent();
                               Response.AddHeader("content-disposition", Attachment);
                               StringWriter sw = new StringWriter();
                               HtmlTextWriter htw = new HtmlTextWriter(sw);
                               Gr.HeaderStyle.Font.Bold = true;
                               Gr.RenderControl(htw);
                               Response.Write(sw.ToString());
                               Response.End();
                           }

                       }
                   }
}

