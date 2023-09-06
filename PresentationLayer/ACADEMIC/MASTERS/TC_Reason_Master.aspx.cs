//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : TC REASON MASTER                                                    
// CREATION DATE : 15-MAR-2023                                                          
// CREATED BY    : NEHAL                                                                    
//======================================================================================

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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_MASTERS_TC_Reason_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavingCertificateController objTC = new LeavingCertificateController();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                BindListView();
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
                Response.Redirect("~/notauthorized.aspx?page=TC_Reason_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TC_Reason_Master.aspx");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objTC.GetAllTCReason(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlReason.Visible = true;
                lvReason.DataSource = ds;
                lvReason.DataBind();
            }
            else
            {
                pnlReason.Visible = false;
                lvReason.DataSource = null;
                lvReason.DataBind();
            }
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
            string Reason = txtReason.Text.Trim();
            bool IsActive;

            if (hfdActive.Value == "true")
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
            //Check for add or edit
            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                //Edit 
                int id = Convert.ToInt32(Session["id"]);
                CustomStatus cs = (CustomStatus)objTC.Add_TCReasonMaster(id, Reason, IsActive);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    txtReason.Text = string.Empty; 
                    BindListView();
                    Session["action"] = null;
                    objCommon.DisplayMessage(this.UPDROLE, "Record Updated sucessfully", this.Page);
                }
            }

            else
            {
                //Add New
                int id = 0;
                CustomStatus cs = (CustomStatus)objTC.Add_TCReasonMaster(id, Reason, IsActive);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Added sucessfully", this.Page);
                    txtReason.Text = string.Empty;
                    BindListView();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
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
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int id = int.Parse(btnEdit.CommandArgument);
        Session["id"] = int.Parse(btnEdit.CommandArgument);
        ViewState["edit"] = "edit";

        this.ShowDetails(id);
        txtReason.Focus();
    }
    private void ShowDetails(int id)
    {
        try
        {
            DataSet ds = objTC.GetAllTCReason(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["REASON"].ToString();
                string IS_ACTIVE = ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString();

                if (IS_ACTIVE == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
            }
            if (ds != null) ;

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}