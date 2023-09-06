//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC 
// PAGE NAME     : CLUB ACTIVITY MASTER                                          
// CREATION DATE : 02-09-2022                                                      
// CREATED BY    : NIKHIL SHENDE                                                  
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
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

public partial class ACADEMIC_StudentAchievement_Club_Activity_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Club_ActivityController obja = new Club_ActivityController();

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    //To Set the MasterPage
    //    if (Session["masterpage"] != null)
    //        objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
    //    else
    //        objCommon.SetMasterPage(Page, "");
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindListView();
            ViewState["Action"] = "add";

            txtDescription.Attributes.Add("maxlength", txtDescription.MaxLength.ToString());
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Club_Activity_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Club_Activity_Master.aspx");
        }
    }


    protected void BindListView()
    {
        try
        {
            //DataSet ds = obja.BindListForClubActivity();
            DataSet ds = objCommon.FillDropDown("ACD_CLUB_ACTIVITY_MASTER", "ACTIVITYID,ACTIVITY_NAME", "ACTIVITY_DESCRIPTION", "ACTIVITYID>0", "ACTIVITYID DESC");

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlClub.Visible = true;
                lvClubActivity.DataSource = ds.Tables[0];
                lvClubActivity.DataBind();
            }
            else
            {
                pnlClub.Visible = true;
                lvClubActivity.DataSource = null;
                lvClubActivity.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitClub_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (ViewState["Action"] != null)
            {
                if (ViewState["Action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)obja.InsertClubActivity(txtName.Text, txtDescription.Text);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.pnlClub, "Record Saved Successfully..", this.Page);
                        clearData();
                    }
                    else
                    {
                        objCommon.DisplayMessage(pnlClub, "Record Already Exist", this);
                    }
                }
                else
                {

                    if (ViewState["Action"] != null)
                    {
                        CustomStatus cs = (CustomStatus)obja.UpdateClubActivity(Convert.ToInt32(ViewState["Edit"]), txtName.Text, txtDescription.Text);
                        //Check for add or edit
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.pnlClub, "Record Updated Successfully", this);
                            BindListView();
                            ViewState["Action"] = "add";
                            clearData();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(pnlClub, "Record Already Exist", this);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Club_Activity_Master.btnSubmitClub_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelClub_Click(object sender, System.EventArgs e)
    {
        clearData();
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEditCreateEvent_Click(object sender, System.EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            //LinkButton btnEdit = sender as LinkButton;
            int ActivityID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["Edit"] = ActivityID;
            ViewState["Action"] = "Edit";
            btnSubmitClub.Text = "Update";
            ShowDetail(ActivityID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Club_Activity_Master.btnEditCreateEvent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetail(int ActivityID)
    {
        try
        {
            DataSet ds = obja.EditClubActivityData(ActivityID);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();

                txtDescription.Text = ds.Tables[0].Rows[0]["ACTIVITY_DESCRIPTION"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Club_Activity_Master.ShowDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clearData()
    {
        txtName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        btnSubmitClub.Text = "Submit";
    }
}