//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : OD MASTER                                                    
// CREATION DATE : 20-Mar-2023                                                          
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

public partial class ACADEMIC_OD_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();

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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                BindListViewOD();
                BindListViewLeave();
            }
            //BindListView();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OD_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OD_Master.aspx");
        }
    }
    private void BindListViewOD()
    {
        try
        {
            DataSet ds = objAttC.GetAllOD_Modified(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlOD.Visible = true;
                lvOD.DataSource = ds;
                lvOD.DataBind();
            }
            else
            {
                pnlOD.Visible = false;
                lvOD.DataSource = null;
                lvOD.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindListViewLeave()
    {
        try
        {
            DataSet ds = objAttC.GetAllLeave_Modified(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlLeave.Visible = true;
                lvLeave.DataSource = ds;
                lvLeave.DataBind();
            }
            else
            {
                pnlLeave.Visible = false;
                lvLeave.DataSource = null;
                lvLeave.DataBind();
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
            string OD = txtOD.Text.Trim();
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
            if (Session["actionod"] != null && Session["actionod"].ToString().Equals("editod"))
            {
                //Edit 
                int ODID = Convert.ToInt32(Session["ODID"]);
                CustomStatus cs = (CustomStatus)objAttC.Add_ODMaster_Modified(ODID, OD, IsActive);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearControls();
                    BindListViewOD();
                    Session["actionod"] = null;
                    objCommon.DisplayMessage(this.updOD, "Record Updated sucessfully", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updOD, "Record Already Exist", this.Page);
                }
            }

            else
            {
                //Add New
                int ODID = 0;
                CustomStatus cs = (CustomStatus)objAttC.Add_ODMaster_Modified(ODID, OD, IsActive);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updOD, "Record Added sucessfully", this.Page);
                    ClearControls();
                    BindListViewOD();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updOD, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updOD, "Record Already Exist", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControls()
    {
        txtOD.Text = string.Empty;
        txtLeave.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        //Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int ODID = int.Parse(btnEdit.CommandArgument);
        Session["ODID"] = int.Parse(btnEdit.CommandArgument);
        ViewState["editod"] = "editod";

        this.ShowDetailsOD(ODID);
        txtOD.Focus();
    }
    private void ShowDetailsOD(int ODID)
    {
        try
        {
            DataSet ds = objAttC.GetAllOD_Modified(ODID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtOD.Text = ds.Tables[0].Rows[0]["OD_NAME"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["OD_NAME"].ToString();
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
            if (ds != null)

                Session["actionod"] = "editod";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancelLeave_Click(object sender, EventArgs e)
    {
        ClearControls();
        //Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmitLeave_Click(object sender, EventArgs e)
    {
        try
        {
            string SPECIALLEAVETYPE = txtLeave.Text.Trim();
            bool IsActive;

            if (hfdActiveLeave.Value == "true")
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
            //Check for add or edit
            if (Session["actionleave"] != null && Session["actionleave"].ToString().Equals("editleave"))
            {
                //Edit 
                int SPECIALLEAVETYPENO = Convert.ToInt32(Session["SPECIALLEAVETYPENO"]);
                CustomStatus cs = (CustomStatus)objAttC.Add_LeaveMaster_Modified(SPECIALLEAVETYPENO, SPECIALLEAVETYPE, IsActive);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearControls();
                    BindListViewLeave();
                    Session["actionleave"] = null;
                    objCommon.DisplayMessage(this.updLeave, "Record Updated sucessfully", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updLeave, "Record Already Exist", this.Page);
                }
            }

            else
            {
                //Add New
                int SPECIALLEAVETYPENO = 0;
                CustomStatus cs = (CustomStatus)objAttC.Add_LeaveMaster_Modified(SPECIALLEAVETYPENO, SPECIALLEAVETYPE, IsActive);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updLeave, "Record Added sucessfully", this.Page);
                    ClearControls();
                    BindListViewLeave();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updLeave, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updLeave, "Record Already Exist", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEditLeave_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEditLeave = sender as ImageButton;
        int SPECIALLEAVETYPENO = int.Parse(btnEditLeave.CommandArgument);
        Session["SPECIALLEAVETYPENO"] = int.Parse(btnEditLeave.CommandArgument);
        ViewState["editleave"] = "editleave";

        this.ShowDetailsLeave(SPECIALLEAVETYPENO);
        txtLeave.Focus();
    }
    private void ShowDetailsLeave(int SPECIALLEAVETYPENO)
    {
        try
        {
            DataSet ds = objAttC.GetAllLeave_Modified(SPECIALLEAVETYPENO);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtLeave.Text = ds.Tables[0].Rows[0]["SPECIALLEAVETYPE"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["SPECIALLEAVETYPE"].ToString();
                string IS_ACTIVE = ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString();

                if (IS_ACTIVE == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActiveLeave(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActiveLeave(false);", true);
                }
            }
            if (ds != null)

                Session["actionleave"] = "editleave";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}