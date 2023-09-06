using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_Admission_ACD_ADM_ApplicationStages : System.Web.UI.Page
{
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    Common objCommon = new Common();
    ApplicationStagesController objAppStages = new ApplicationStagesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtDescription.Attributes.Add("maxlength", "200");

        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ViewState["action"] = "add";
                BindApplicationStage();
            }
        }
    }
    
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int USERNO = Convert.ToInt32(Session["userno"].ToString());
            string Stage_Name = txtStageName.Text;
            string Description = txtDescription.Text;
            int Stage_Id = 0;
            if (ViewState["action"].ToString() == "add")
            {
                Stage_Id = 0;
            }
            else
            {
                Stage_Id = Convert.ToInt32(ViewState["STAGEID"]);
            }
            int status = objAppStages.InsertApplicationStageData(Stage_Id,Stage_Name, Description,USERNO);
            if (status == 1)
            {
                objCommon.DisplayMessage(this.updapplicationstage, "Record Saved Sucessfully !", this.Page);
                BindApplicationStage();
                ClearControl();
            }
            else if (status == 2)
            {
                objCommon.DisplayMessage(this.updapplicationstage, "Record Updated Sucessfully!", this.Page);
                BindApplicationStage();
                ClearControl();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationStages.aspx.BindApplicationStage() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
      
    }

    public void ClearControl()
    {
        txtStageName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int Stage_Id = int.Parse(btnEdit.CommandArgument);
        ViewState["STAGEID"] = int.Parse(btnEdit.CommandArgument);
        ShowDetails(Stage_Id);
        ViewState["action"] = "edit";
    }

    private void ShowDetails(int Stage_Id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ADM_ApplicationStages", "STAGEID", "STAGENAME, DESCRIPTION", "STAGEID =" + Stage_Id + "", "STAGEID");

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                txtStageName.Text = ds.Tables[0].Rows[0]["STAGENAME"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationStages.BindApplicationStage() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BindApplicationStage()               
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ADM_ApplicationStages", "STAGEID", "STAGENAME, DESCRIPTION", "", "STAGEID");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvApplicationStages.DataSource = ds;
                lvApplicationStages.DataBind();
            }
            else
            {
                lvApplicationStages.DataSource = null;
                lvApplicationStages.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ApplicationStages.BindApplicationStage() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }
}