//=======================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS AND EVENT MANAGEMENT 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 25-04-2017
// DESCRIPTION   : USED TO CREATE EVENT NAME
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

public partial class Sports_Masters_EventTypeMaster : System.Web.UI.Page
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
                objUCommon.ShowError(Page, "Sports_Masters_EventTypeMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private bool checkEventTypeExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "EVENT_TYPE_NAME='" + txtEventTypeName.Text + "'", "ETID");
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
            objSport.EVENT_TYPE_NAME = txtEventTypeName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtEventTypeName.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["ETID"] == null)
            {
                if (checkEventTypeExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Event Type Name Already Exist...!!');", true);
                    Clear();
                    return;
                }
                else
                {
                    objSportC.AddUpdateEventType(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully...!!');", true);
                    Clear();
                    BindlistView();
                }
            }
            else
            {
                objSport.ETID = Convert.ToInt32(ViewState["ETID"].ToString());
                objSportC.AddUpdateEventType(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully...!!');", true);
                Clear();
                BindlistView();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventTypeMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int etid = int.Parse(btnEdit.CommandArgument);
            ViewState["ETID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(etid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventTypeMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_TYPE_MASTER", "*", "", "", "ETID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventType.DataSource = ds;
                lvEventType.DataBind();
                lvEventType.Visible = true;
            }
            else
            {
                lvEventType.DataSource = null;
                lvEventType.DataBind();
                lvEventType.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventTypeMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int etid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_TYPE_MASTER", "*", "", "ETID=" + etid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEventTypeName.Text = ds.Tables[0].Rows[0]["EVENT_TYPE_NAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventTypeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtEventTypeName.Text = string.Empty;
        ViewState["ETID"] = null;
    }

}