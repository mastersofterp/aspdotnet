//=======================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 09-09-2014
// DESCRIPTION   : USED TO CREATE DIFFERENT DESIGNATIONS OF THE STAFF
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

public partial class Sports_Masters_StaffMaster : System.Web.UI.Page
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
                objUCommon.ShowError(Page, "Sports_Masters_StaffMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
        DataSet ds = objCommon.FillDropDown("SPRT_STAFF_MASTER", "POSTID", "POSTNAME", "POSTNAME='" + txtPostName.Text + "'", "POSTID");
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
            objSport.POSTNAME = txtPostName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPostName.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["POSTID"] == null)
            {
                if (checkPostExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Designation Name Already Exist...!!');", true);
                    Clear();
                    return;
                }
                else
                {
                    objSportC.AddUpdateStaffMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Submitted Successfully...!!');", true);
                    Clear();
                    BindlistView();
                }
            }
            else
            {
                objSport.POSTID = Convert.ToInt32(ViewState["POSTID"].ToString());
                objSportC.AddUpdateStaffMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Updated Successfully...!!');", true);
                Clear();
                BindlistView();
           }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_StaffMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int post_id = int.Parse(btnEdit.CommandArgument);
            ViewState["POSTID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(post_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_StaffMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_STAFF_MASTER", "*", "", "", "POSTID DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPost.DataSource = ds;
                lvPost.DataBind();
            }
            else
            {
                lvPost.DataSource = null;
                lvPost.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_StaffMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int post_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_STAFF_MASTER", "*", "", "POSTID=" + post_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPostName.Text = ds.Tables[0].Rows[0]["POSTNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_StaffMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtPostName.Text = string.Empty;
        //lvPost.DataSource = null;
        //lvPost.DataBind();
        ViewState["POSTID"] = null;
    }
   
}
