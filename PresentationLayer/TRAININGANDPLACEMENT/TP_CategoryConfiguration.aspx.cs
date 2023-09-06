using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

public partial class TRAININGANDPLACEMENT_TP_CategoryConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
    BlobController objBlob = new BlobController();
    TPTraining objtptraining = new TPTraining();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

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

              //  this.CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlDegree, " ACD_DEGREE", "DISTINCT(DEGREENO)", "DEGREENAME", "", "DEGREENO");
                BindListViewCategory();
            }


            ViewState["userBranch"] = null;
            ViewState["CAID"] = null;
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TP_Reg_Approval.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TP_Reg_Approval.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, " (SELECT DISTINCT DEGREENO,DURATION FROM ACD_COLLEGE_DEGREE_BRANCH)A INNER JOIN ACD_SEMESTER SEM ON (SEM.YEARNO<=A.DURATION AND SEM.SEMESTERNO>0)", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "A.DEGREENO='" + Convert.ToInt32(ddlDegree.SelectedValue) + "'", "SEM.SEMESTERNO,SEM.SEMESTERNAME");
    }
    protected void Submit_Click(object sender, EventArgs e)
    {

        if (ddlDegree.SelectedValue=="0")
        {
            objCommon.DisplayMessage(this.Page, "Please Select Degree.", this.Page);
            return;
        }
        if (ddlSemester.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.Page, "Please Select Activity Start From Semester.", this.Page);
            return;
        }
        int status = 0;
        if (hfCategory.Value == "true")
        {
            objtptraining.CAStatus = 1;
        }
        else
        {
            objtptraining.CAStatus = 0;
        }
        objtptraining.CADegree = Convert.ToInt32(ddlDegree.SelectedValue);
        objtptraining.CASemester = Convert.ToInt32(ddlSemester.SelectedValue);
        objtptraining.CAUA_NO = Convert.ToInt32(Session["userno"]);
        if (ViewState["CAID"] == null)
        {
            objtptraining.CAID = 0;
        }
        else
        {
            objtptraining.CAID = Convert.ToInt32(ViewState["CAID"]);
        }

        if (ViewState["CAID"] != null)
        {
            DataSet ds = objCommon.FillDropDown("TP_CATEGORY_CONFIGURATION_STATUS", "C_CONF", "DEGREENO", "DEGREENO='" + Convert.ToInt32(ddlDegree.SelectedValue) + "' and SEMESTERNO='" + Convert.ToInt32(ddlSemester.SelectedValue) + "' and C_CONF!='" + Convert.ToInt32(ViewState["CAID"].ToString()) + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {

                objCommon.DisplayMessage(this.Page, "Configuration Is Already Exist.", this.Page);
                return;
            }
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("TP_CATEGORY_CONFIGURATION_STATUS", "C_CONF", "DEGREENO", "DEGREENO='" + Convert.ToInt32(ddlDegree.SelectedValue) + "' and SEMESTERNO='" + Convert.ToInt32(ddlSemester.SelectedValue) + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {

                objCommon.DisplayMessage(this.Page, "Configuration Is Already Exist.", this.Page);
                return;
            }
        }

        CustomStatus cs = (CustomStatus)objTP.AddCategoryConf(objtptraining);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            ViewState["action"] = "add";
            objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
             ViewState["action"] = "add";
            objCommon.DisplayMessage(this.Page, "Record Update Successfully.", this.Page);
        }
        BindListViewCategory();
        ddlDegree.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        ViewState["CAID"] = null;
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        ddlDegree.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        ViewState["CAID"] = null;
    }

    private void BindListViewCategory()
    {
        try
        {
            DataSet ds = objTP.GetCategoryconf();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCategoryconf.DataSource = ds;
                lvCategoryconf.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvCategoryconf.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPreview_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CAID = int.Parse(btnEdit.CommandArgument);
            ViewState["CAID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsCategoryConf(CAID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetailsCategoryConf(int CAID)
    {
        try
        {
            DataSet ds = objTP.EditCategoryConfByID(CAID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                objCommon.FillDropDownList(ddlDegree, " ACD_DEGREE", "DISTINCT(DEGREENO)", "DEGREENAME", "", "DEGREENO");
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ddlDegree_SelectedIndexChanged(null, null);
              
                ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

               if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}