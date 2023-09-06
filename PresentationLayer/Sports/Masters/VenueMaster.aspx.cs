//===========================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 28-09-2014
// DESCRIPTION   : USED TO CREATE VENUE NAME
//===========================================
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

public partial class Sports_Masters_VenueMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport = new Sport();

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
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_VenueMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private bool checkPostExist()
    {
        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_VENUE_MASTER", "VENUEID", "VENUENAME", "VENUENAME='" + txtVenueName.Text + "'", "VENUEID");
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
            objSport.VENUENAME = txtVenueName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVenueName.Text);
            objSport.VENUEADDRESS = txtVenueAddress.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtVenueAddress.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["VENUEID"] == null)
            {
                if (checkPostExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Venue Name Already Exist...!!');", true);
                    Clear();
                    return;
                }
                else
                {
                    objSportC.AddUpdateVenueMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Venue Name Submitted Successfully...!!');", true);
                    Clear();
                    BindlistView();
                }
            }
            else
            {
                objSport.VENUEID = Convert.ToInt32(ViewState["VENUEID"].ToString());
                objSportC.AddUpdateVenueMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Venue Name Updated Successfully...!!');", true);
                Clear();
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_VenueMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        BindlistView();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int venue_id = int.Parse(btnEdit.CommandArgument);
            ViewState["VENUEID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(venue_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_VenueMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_VENUE_MASTER", "*", "", "", "VENUEID DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvVenue.DataSource = ds;
                lvVenue.DataBind();
            }
            else
            {
                lvVenue.DataSource = null;
                lvVenue.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_VenueMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int venue_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_VENUE_MASTER", "*", "", "VENUEID=" + venue_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtVenueName.Text = ds.Tables[0].Rows[0]["VENUENAME"].ToString();
                txtVenueAddress.Text = ds.Tables[0].Rows[0]["VENUE_ADDRESS"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_VenueMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtVenueName.Text = string.Empty;
        txtVenueAddress.Text = string.Empty;
        //lvVenue.DataSource = null;
        //lvVenue.DataBind();
        ViewState["VENUEID"] = null;
    }
}
