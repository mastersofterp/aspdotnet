//=======================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : Meeting Management
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 03-06-2017
// DESCRIPTION   : USED TO CREATE MEETING NAME
//========================================================================

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class MEETING_MANAGEMENT_MASTER_MeetingMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();

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
        try
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                objCommon.FillDropDownList(ddlCommittee, "TBL_MM_COMITEE", "ID", "NAME", "", "ID");
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_MASTER_MeetingMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    private bool checkMeetingNameExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("TBL_MM_MEETING_MASTER", "MEETING_NO", "MEETING_NAME", "MEETING_NAME='" + txtMeetingName.Text + "'", "MEETING_NO");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objMM.MEETING_NO = 0;
            objMM.MEETING_NAME = txtMeetingName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtMeetingName.Text);
            objMM.ID = Convert.ToInt32(ddlCommittee.SelectedValue);
            objMM.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["MEETING_NO"] == null)
            {
                if (checkMeetingNameExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Meeting Name Already Exist.');", true);
                    Clear();
                    return;
                }
                else
                {
                    objMC.AddUpdateMeetingName(objMM);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                    Clear();
                    BindlistView();
                }
            }
            else
            {
                objMM.MEETING_NO = Convert.ToInt32(ViewState["MEETING_NO"].ToString());
                objMC.AddUpdateMeetingName(objMM);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                Clear();
                BindlistView();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_MASTER_MeetingMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int MeetingNo = int.Parse(btnEdit.CommandArgument);
            ViewState["MEETING_NO"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(MeetingNo);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_MASTER_MeetingMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("TBL_MM_MEETING_MASTER MM INNER JOIN Tbl_MM_COMITEE MC ON (MM.ID = MC.ID)", "MEETING_NO, MEETING_NAME, MM.ID, MC.code +' - '+ MC.NAME AS NAME", "", "", "MEETING_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvMeetingName.DataSource = ds;
                lvMeetingName.DataBind();
                lvMeetingName.Visible = true;
            }
            else
            {
                lvMeetingName.DataSource = null;
                lvMeetingName.DataBind();
                lvMeetingName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_MASTER_MeetingMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int MeetingNo)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("TBL_MM_MEETING_MASTER", "*", "", "MEETING_NO=" + MeetingNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtMeetingName.Text = ds.Tables[0].Rows[0]["MEETING_NAME"].ToString();
                ddlCommittee.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_MASTER_MeetingMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtMeetingName.Text = string.Empty;
        ViewState["MEETING_NO"] = null;
        ddlCommittee.SelectedIndex = 0;
    }
}