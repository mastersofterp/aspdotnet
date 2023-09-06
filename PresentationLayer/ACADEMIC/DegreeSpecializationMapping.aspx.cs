using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_DegreeSpecializationMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmission objAd = new OnlineAdmission();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
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
            if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                pnlListView.Visible = true;
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    ViewState["Action"] = "add";
                PopulatedropDown();
            }
            BindListViewDegree();
        }
    }

    private void BindListViewDegree()
    {
        try
        {
            OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
            DataSet ds = Admcontroller.GetAllDegree();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvSpecialization.DataSource = ds;
                lvSpecialization.DataBind();
                pnlListView.Visible = true;
                lvSpecialization.Visible = true;
            }
            else
            {
                lvSpecialization.DataSource = null;
                lvSpecialization.DataBind();
                lvSpecialization.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineAdmission.BindListViewDegree()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    private void PopulatedropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "COLLEGE_CODE>0", "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineAdmission.PopulatedropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlDegree.SelectedIndex = 0;
        txtspecialization.Text = string.Empty;
        chkActive.Checked = false;
        ViewState["action"] = null;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.OnlineAdmission objAd = new IITMS.UAIMS.BusinessLayer.BusinessEntities.OnlineAdmission();
            objAd.DEGREE_NO = Convert.ToInt32(ddlDegree.SelectedValue);
            objAd.DEGREE = ddlDegree.SelectedValue;
            objAd.SPECIALIZATION = txtspecialization.Text.Trim();
            objAd.CREATED_BY = 1;
            objAd.IP_ADDRESS = "::1";
            int activeStatus = chkActive.Checked == true ? 1 : 0;                   //Added by Nikhil L. on 21/02/2022
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objAd.DEGREE_NO = Convert.ToInt32(Session["SPEC_NO"]);
                CustomStatus cs = (CustomStatus)Admcontroller.UpdateSpecialization(objAd, activeStatus);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    clear();
                    BindListViewDegree();
                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                }
            }
            else
            {
                CustomStatus cs = (CustomStatus)Admcontroller.AddSpecialization(objAd, activeStatus);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    clear();
                    BindListViewDegree();
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                }
                else
                {
                    clear();
                    objCommon.DisplayMessage(this.Page, "Record Already Exist.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlineAdmission.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int specNo = int.Parse(btnEdit.CommandArgument);
            Session["SPEC_NO"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails(specNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetails(Int32 specNo)
    {
        DataSet ds = null;
        try
        {
            ds = Admcontroller.GetSingleSpecialization(specNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SPEC_NO"] = specNo;   
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                txtspecialization.Text = ds.Tables[0].Rows[0]["SPECIALIZATION"].ToString();
                string activeStatus = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
                if (Convert.ToInt32(activeStatus) == 1)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlineAdmission.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}