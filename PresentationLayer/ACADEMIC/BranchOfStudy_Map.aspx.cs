using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
public partial class ACADEMIC_BranchOfStudy_Map: System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //OnlineAdmission objAd = new OnlineAdmission();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        //ViewState["Action"] = "add";
                    PopulateDropDown();
                    BindBranchStudyList();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchOfStudy_Map.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchOfStudy_Map.aspx.aspx");
        }
    }
    protected void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH DB", "DISTINCT DB.DEGREENO", "DBO.FN_DESC('DEGREENAME',DB.DEGREENO) DEGREENAME", "UGPGOT=2", "DEGREENAME");
            objCommon.FillDropDownList(ddlQualDegree, "ACD_COLLEGE_DEGREE_BRANCH DB", "DISTINCT DB.DEGREENO", "DBO.FN_DESC('DEGREENAME',DB.DEGREENO) DEGREENAME", "UGPGOT=1", "DEGREENAME");
            ddlQualDegree.Items.Add(new ListItem("BE", "901"));
            ddlQualDegree.Items.Add(new ListItem("Others", "902"));
            ddlQualDegree.Items.Add(new ListItem("BCA.LLB(Hons.)", "903"));
            ddlQualDegree.Items.Add(new ListItem("BTech.LLB", "904"));
            ddlQualDegree.Items.Add(new ListItem("LLB", "905"));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string ipAddress=string.Empty;
            if (hfdStat.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            ipAddress=Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (btnSubmit.Text.Equals("Submit"))
            {
                CustomStatus cs = (CustomStatus)Admcontroller.Add_Branch_Study(Convert.ToInt32(ddlDegree.SelectedValue),txtBranchStudy.Text.ToString(),status,Convert.ToInt32(Session["OrgId"]),ipAddress,Convert.ToInt32(Session["userno"]),Convert.ToInt32(ddlQualDegree.SelectedValue));              
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                    ClearFields();
                    BindBranchStudyList();
                    return;
                }
            }
            else if (btnSubmit.Text.Equals("Update"))
            {
                CustomStatus cs = (CustomStatus)Admcontroller.Update_Branch_Study(Convert.ToInt32(ViewState["branchStudy"]), Convert.ToInt32(ddlDegree.SelectedValue), txtBranchStudy.Text.ToString(), status, ipAddress, Convert.ToInt32(Session["userno"]),Convert.ToInt32(ddlQualDegree.SelectedValue));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                    ClearFields();
                    BindBranchStudyList();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            txtBranchStudy.Text = string.Empty;
            ddlQualDegree.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ClearFields()
    {
        ddlDegree.SelectedIndex = 0;
        txtBranchStudy.Text = string.Empty;
        //ViewState["action"] = null;
        btnSubmit.Text = "Submit";
        ddlQualDegree.SelectedIndex = 0;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = sender as ImageButton;
            int branch_Study = Convert.ToInt32(imgButton.CommandArgument.ToString());
            //ViewState["Action"] = "edit";
            ViewState["branchStudy"] = branch_Study;
            btnSubmit.Text = "Update";
            BindBranchStudyList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BindBranchStudyList()
    {
        DataSet ds = null;
        if (btnSubmit.Text.Equals("Update"))
        {
            ds = Admcontroller.GetBranchStudy(Convert.ToInt32(ViewState["branchStudy"]));
             ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
             txtBranchStudy.Text = ds.Tables[0].Rows[0]["STUDY_OF_BRANCH"].ToString().Equals(string.Empty) ? "" : ds.Tables[0].Rows[0]["STUDY_OF_BRANCH"].ToString();
             if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString().Equals("1"))
             {
                 ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
             }
             else
             {
                 ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
             }
             ddlQualDegree.SelectedValue = ds.Tables[0].Rows[0]["QUAL_DEGREENO"].ToString();
        }
        else
        {
            ds = Admcontroller.GetBranchStudy(0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBranchStudy.DataSource = ds;
                lvBranchStudy.DataBind();
                lvBranchStudy.Visible = true;
            }
            else
            {
                lvBranchStudy.DataSource = null;
                lvBranchStudy.DataBind();
                lvBranchStudy.Visible = false;
            }
        }
       
    }
}