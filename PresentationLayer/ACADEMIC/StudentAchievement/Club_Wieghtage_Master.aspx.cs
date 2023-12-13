//======================================================================================
// PROJECT NAME  :RFC_CODE                                                                
// MODULE NAME   : Student Achievement                     
// CREATION DATE : 20-11-2023                                                       
// CREATED BY    : SAKSHI MAKWANA
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//=============================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;

public partial class ACADEMIC_StudentAchievement_Club_Wieghtage_Master : System.Web.UI.Page
{

    #region PageLoad

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ClubController OBJCLUB = new ClubController()
        ;
    protected void Page_Load(object sender, EventArgs e)
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
              
            }
            //WeightageList();
            //CampusList();
            Bind();
            ViewState["action"] = "Submit";
            ViewState["action1"] = "Submit"; 
            ViewState["action2"] = "Submit";
            ViewState["action3"] = "Submit";


        }
    }

    #endregion PageLoad

    #region Bind 
    private void Bind()
    {
        int value = 0;
        DataSet ds = null;
        ds = OBJCLUB.PointMapping(0);

        #region lvphdweightage
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvphdweightage.DataSource = ds.Tables[0];
            lvphdweightage.DataBind();
        }
        else
        {
            lvphdweightage.DataSource = null;
            lvphdweightage.DataBind();
        }

        foreach (ListViewDataItem dataitem in lvphdweightage.Items)
        {
            Label Status = dataitem.FindControl("lblStatus") as Label;

            string Statuss = (Status.Text);

            if (Statuss == "InActive")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }

        }
        #endregion lvphdweightage

        #region lvCampus
        if (ds.Tables[2].Rows.Count > 0)
        {
            lvCampus.DataSource = ds.Tables[2];
            lvCampus.DataBind();
        }
        else
        {
            lvCampus.DataSource = null;
            lvCampus.DataBind();
        }

        foreach (ListViewDataItem dataitem in lvCampus.Items)
        {
            Label Status = dataitem.FindControl("lblEStatus") as Label;

            string Statuss = (Status.Text);

            if (Statuss == "InActive")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }

        }
        #endregion lvCampus

        #region lvHourCount
        if (ds.Tables[4].Rows.Count > 0)
        {
            lvHourCount.DataSource = ds.Tables[4];
            lvHourCount.DataBind();
        }
        else
        {
            lvHourCount.DataSource = null;
            lvHourCount.DataBind();
        }

        foreach (ListViewDataItem dataitem in lvHourCount.Items)
        {
            Label Status = dataitem.FindControl("lblCStatus") as Label;

            string Statuss = (Status.Text);

            if (Statuss == "InActive")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }

        }
        #endregion lvHourCount

        #region lvActivity

        if (ds.Tables[9].Rows.Count > 0)
        {
            lvActivity.DataSource = ds.Tables[9];
            lvActivity.DataBind();
        }
        else
        {
            lvActivity.DataSource = null;
            lvActivity.DataBind();
        }

        foreach (ListViewDataItem dataitem in lvActivity.Items)
        {
            Label Status = dataitem.FindControl("lblAStatus") as Label;

            string Statuss = (Status.Text);

            if (Statuss == "InActive")
            {
                Status.CssClass = "badge badge-danger";
            }
            else
            {
                Status.CssClass = "badge badge-success";
            }

        }

        #endregion lvActivity

        #region BindDropDown

        #region ddlWeightage

        ddlWeightage.DataSource = null;
        ddlWeightage.DataBind();

        if (ds.Tables[6].Rows.Count > 0)
        {
           
            ddlWeightage.DataSource = ds.Tables[6];
            ddlWeightage.DataTextField = "WEIGHTAGE_NAME";
            ddlWeightage.DataValueField = "WEIGHTAGE_NO";
            ddlWeightage.DataBind();
        }
        else
        {
            ddlWeightage.DataSource = null;
            ddlWeightage.DataBind();
        }
        ddlWeightage.Items.Insert(0, new ListItem("Please Select ", ""));
        
        #endregion ddlWeightage

        #region ddlCampus

        ddlCampus.DataSource = null;
        ddlCampus.DataBind();
        if (ds.Tables[7].Rows.Count > 0)
        {
          
            ddlCampus.DataSource = ds.Tables[7];
            ddlCampus.DataTextField = "CAMPUS_NAME";
            ddlCampus.DataValueField = "CAMPUS_NO";
            ddlCampus.DataBind();
        }
        else
        {
            ddlCampus.DataSource = null;
            ddlCampus.DataBind();
        }
        ddlCampus.Items.Insert(0, new ListItem("Please Select ", ""));

        #endregion ddlCampus

        #region ddlCount

        ddlCount.DataSource = null;
        ddlCount.DataBind();
        if (ds.Tables[8].Rows.Count > 0)
        {
           
            ddlCount.DataSource = ds.Tables[8];
            ddlCount.DataTextField = "COUNT_NAME";
            ddlCount.DataValueField = "HC_NO";
            ddlCount.DataBind();
        }
        else
        {
            ddlCount.DataSource = null;
            ddlCount.DataBind();
        }
        ddlCount.Items.Insert(0, new ListItem("Please Select ", ""));
        #endregion ddlCount

        #endregion BindDropDown

    }
    #endregion Bind
  
    #region PDA Hr Weightage Tab

    protected void btnSubmitWeight_Click(object sender, EventArgs e)
    {
        string weightage = txtWeitage.Text;
        int Status;
        if (hfdWeightage.Value == "true")
        {
           Status = 1;
        }
        else
        {
            Status = 0;
        }
        string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
       int  createdby =Convert.ToInt32(Session["userno"]);
       int result=0;
       if (ViewState["action"].ToString().Equals("Submit") == true)
       {
           result = OBJCLUB.InsertUpdatePhdHrWeightage(weightage, Status, IPADDRESS, createdby, 1, 0);
       }
       else if(ViewState["action"].ToString().Equals("Update") == true)
       {
           result = OBJCLUB.InsertUpdatePhdHrWeightage(weightage, Status, IPADDRESS, createdby, 2, Convert.ToInt32(ViewState["wightno"]));
           ViewState["action"] ="Submit";
           btnSubmitWeight.Text = ViewState["action"].ToString();
       }
        if (result == 1)
        {
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);       
        }
        else if (result == 2)
        {
            objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);  
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Already Exist !!", this.Page);
        }
        Bind();
        clear();
        
    }

    protected void btnWeightageLink_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnWeightageLink = sender as LinkButton;
            int weightageno = Convert.ToInt32(btnWeightageLink.CommandArgument);
            ViewState["wightno"] = Convert.ToInt32(btnWeightageLink.CommandArgument);
            ShowDetails(weightageno);
            ViewState["action"] = "Update";
            btnSubmitWeight.Text = ViewState["action"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int weightageno)
    {
        DataSet ds = OBJCLUB.PointMapping(weightageno);
        if (ds != null && ds.Tables[1].Rows.Count > 0)
        {
            txtWeitage.Text = ds.Tables[1].Rows[0]["WEIGHTAGE_NAME"].ToString();

            if (ds.Tables[1].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(false);", true);
            }
        }
    }

    protected void lvphdweightage_ItemEditing(object sender, ListViewEditEventArgs e){}

    protected void btnCancelweight_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        txtWeitage.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
        ViewState["action"] = "Submit";
        btnSubmitWeight.Text = ViewState["action"].ToString();
    }

    #endregion

    #region Campus Details Tab

    protected void lvCampus_ItemEditing(object sender, ListViewEditEventArgs e){}

    protected void btnSubmitCampus_Click(object sender, EventArgs e)
    {
        string campus = txtCampus.Text;
        int Status;
        if (hfdCampusDetails.Value == "true")
        {
            Status = 1;
        }
        else
        {
            Status = 0;
        }
        string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        int createdby = Convert.ToInt32(Session["userno"]);
        int result = 0;
        if (ViewState["action1"].ToString().Equals("Submit") == true)
        {
            result = OBJCLUB.InsertUpdateCourseDeatils(campus, Status, IPADDRESS, createdby, 1, 0);
        }
        else if (ViewState["action1"].ToString().Equals("Update") == true)
        {
            result = OBJCLUB.InsertUpdateCourseDeatils(campus, Status, IPADDRESS, createdby, 2, Convert.ToInt32(ViewState["campusno"]));
            ViewState["action1"] = "Submit";
            btnSubmitCampus.Text = ViewState["action1"].ToString();
        }
        if (result == 1)
        {
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
        }
        else if (result == 2)
        {
            objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Already Exist !!", this.Page);
        }
        Bind();
        clearCampus();
    }

    protected void btnCampusEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnCampusEdit = sender as LinkButton;
            int campusno = Convert.ToInt32(btnCampusEdit.CommandArgument);
            ViewState["campusno"] = Convert.ToInt32(btnCampusEdit.CommandArgument);
            ShowDetails1(campusno);
            ViewState["action1"] = "Update";
            btnSubmitCampus.Text = ViewState["action1"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails1(int campusno)
    {
        DataSet ds = OBJCLUB.PointMapping(campusno);
        if (ds != null && ds.Tables[3].Rows.Count > 0)
        {
            txtCampus.Text = ds.Tables[3].Rows[0]["CAMPUS_NAME"].ToString();
            if (ds.Tables[3].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventNature(true);", true);  
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventNature(false);", true);
            }
        }  
    }

    private void clearCampus()
    {
        txtCampus.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventNature(true);", true);
        ViewState["action1"] = "Submit";
        btnSubmitWeight.Text = ViewState["action1"].ToString();
    }

    protected void btnCancelCampus_Click(object sender, EventArgs e)
    {
        clearCampus();
    }

    #endregion

    #region Hours Count Tab

    protected void lvCategory_ItemEditing(object sender, ListViewEditEventArgs e){}

    protected void btnCountSubmit_Click(object sender, EventArgs e)
    {
        string count = txtCount.Text;
        int Status;
        if (hfvCount.Value == "true")
        {
            Status = 1;
        }
        else
        {
            Status = 0;
        }
        string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
        int createdby = Convert.ToInt32(Session["userno"]);
        int result = 0;
        if (ViewState["action2"].ToString().Equals("Submit") == true)
        {
            result = OBJCLUB.InsertUpdateHoursCountDeatils(count, Status, IPADDRESS, createdby, 1, 0);
        }
        else if (ViewState["action2"].ToString().Equals("Update") == true)
        {
            result = OBJCLUB.InsertUpdateHoursCountDeatils(count, Status, IPADDRESS, createdby, 2, Convert.ToInt32(ViewState["countno"]));
            ViewState["action2"] = "Submit";
            btnCountSubmit.Text = ViewState["action2"].ToString();
        }
        if (result == 1)
        {
            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
        }
        else if (result == 2)
        {
            objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this, "Record Already Exist !!", this.Page);
        }
        Bind();
        ClearCountHour();  
    }

    protected void btneditCountHour_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btneditCountHour = sender as LinkButton;
            int countno = Convert.ToInt32(btneditCountHour.CommandArgument);
            ViewState["countno"] = Convert.ToInt32(btneditCountHour.CommandArgument);
            ShowDetails2(countno);
            ViewState["action2"] = "Update";
            btnCountSubmit.Text = ViewState["action2"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails2(int countno)
    {
        DataSet ds = OBJCLUB.PointMapping(countno);
        if (ds != null && ds.Tables[5].Rows.Count > 0)
        {
            txtCount.Text = ds.Tables[5].Rows[0]["COUNT_NAME"].ToString();

            if (ds.Tables[5].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(false);", true);
            }
        }  
    }

    private void ClearCountHour()
    {
        txtCount.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(true);", true);
        ViewState["action2"] = "Submit";
        btnCountSubmit.Text = ViewState["action2"].ToString();
    }

    protected void btnCountCancel_Click(object sender, EventArgs e)
    {
        ClearCountHour();
    }

    protected void lvActivity_ItemEditing(object sender, ListViewEditEventArgs e){}

    #endregion Hours Count Tab

    #region  Point Mapping

    protected void btnSubmitPoints_Click(object sender, EventArgs e)
    {
        if (ddlWeightage.SelectedIndex != 0)
        {
            if (ddlCampus.SelectedIndex != 0)
            {
                if (ddlCount.SelectedIndex != 0)
                {
                    if (txtPoints.Text != string.Empty)
                    {
                        int Status;
                        int weightno = Convert.ToInt32(ddlWeightage.SelectedValue);
                        int campusno = Convert.ToInt32(ddlCampus.SelectedValue);
                        int countno = Convert.ToInt32(ddlCount.SelectedValue);
                        int points = Convert.ToInt32(txtPoints.Text);
                        if (hfvActivity.Value == "true")
                        {
                            Status = 1;
                        }
                        else
                        {
                            Status = 0;
                        }
                        string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];
                        int createdby = Convert.ToInt32(Session["userno"]);
                        int result = 0;
                        if (ViewState["action3"].ToString().Equals("Submit") == true)
                        {

                            result = OBJCLUB.InsertUpdate_WeightageCampusHours_Mapping(weightno, campusno, countno, points, Status, IPADDRESS, createdby, 1, 0);
                        }
                        else if (ViewState["action3"].ToString().Equals("Update") == true)
                        {
                            result = OBJCLUB.InsertUpdate_WeightageCampusHours_Mapping(weightno, campusno, countno, points, Status, IPADDRESS, createdby, 2, Convert.ToInt32(ViewState["wccno"]));
                            ViewState["action3"] = "Submit";
                            btnSubmitPoints.Text = ViewState["action3"].ToString();
                        }
                        if (result == 1)
                        {
                            objCommon.DisplayMessage(this, "Record Saved Successfully !!", this.Page);
                        }
                        else if (result == 2)
                        {
                            objCommon.DisplayMessage(this, "Record Updated Successfully !!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Record Already Exist !!", this.Page);
                        }
                        Bind();
                        clearMapping();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Please Enter Hour Points !!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Please Select Hours Count !!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select Campus Details !!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select Hours Weightage !!", this.Page);
        }
    }

    protected void btnEditActivity_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditActivity = sender as LinkButton;
            int wccno = Convert.ToInt32(btnEditActivity.CommandArgument);
            ViewState["wccno"] = Convert.ToInt32(btnEditActivity.CommandArgument);
            ShowDetails4(wccno);
            ViewState["action3"] = "Update";
            btnSubmitPoints.Text = ViewState["action3"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails4(int wccno)
    {
        DataSet ds = OBJCLUB.PointMapping(wccno);
        int count = 0;
        if (ds != null && ds.Tables[10].Rows.Count > 0)
        {
            if (ddlWeightage.Items.FindByValue(ds.Tables[10].Rows[0]["WEIGHTAGE_NO"].ToString()) != null)
            {
                ddlWeightage.SelectedValue = ds.Tables[10].Rows[0]["WEIGHTAGE_NO"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this, "Selected PDA Hour Weightage is not Active!!", this.Page);
                count += 1;
            }

            if (ddlCampus.Items.FindByValue(ds.Tables[10].Rows[0]["CAMPUS_NO"].ToString()) != null)
            {
                ddlCampus.SelectedValue = ds.Tables[10].Rows[0]["CAMPUS_NO"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this, "Selected Campus Detail is not Active!!", this.Page);
                count += 1;
            }

            if (ddlCount.Items.FindByValue(ds.Tables[10].Rows[0]["COUNT_NO"].ToString()) != null)
            {
                ddlCount.SelectedValue = ds.Tables[10].Rows[0]["COUNT_NO"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this, "Selected Hours Count is not Active!!", this.Page);
                count += 1;
            }

            txtPoints.Text =ds.Tables[10].Rows[0]["POINTS"].ToString();
            if (ds.Tables[10].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActivity(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActivity(false);", true);
            }
            if (count > 0)
            {
                clearMapping();
            }
        }
    }

    protected void btnCancelPoints_Click(object sender, EventArgs e)
    {
        clearMapping();
    }

    private void clearMapping()
    {
        Bind();
        txtPoints.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActivity(true);", true);
        ViewState["action3"] = "Submit";
        btnSubmitPoints.Text = ViewState["action3"].ToString();
    }

    #endregion  Point Mapping

}