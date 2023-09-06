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

public partial class Sports_Masters_EventMaster : System.Web.UI.Page
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
                    FillCollege();
                    objCommon.FillDropDownList(ddlEventType, "SPRT_EVENT_TYPE_MASTER", "ETID", "EVENT_TYPE_NAME", "", "ETID DESC");
                }                
                BindlistView(0);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }

    }

    private bool checkEventExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "EVENTNAME='" + txtEventName.Text + "' AND COLLEGE_NO = " + Convert.ToInt32(ddlCollege.SelectedValue), "EVENTID");
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
            objSport.EVENTNAME = txtEventName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtEventName.Text);
            objSport.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSport.ETID = Convert.ToInt32(ddlEventType.SelectedValue);
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["EVENTID"] == null)
            {
                if (checkEventExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Event Name Already Exist...!!');", true);
                    Clear();
                    return;
                }
                else
                {
                    objSportC.AddUpdateEventMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully...!!');", true);
                    Clear();
                    BindlistView(Convert.ToInt32(ddlCollege.SelectedValue));
                }
            }
            else
            {
                objSport.EVENTID = Convert.ToInt32(ViewState["EVENTID"].ToString());
                objSportC.AddUpdateEventMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully...!!');", true);
                Clear();
                BindlistView(Convert.ToInt32(ddlCollege.SelectedValue));
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int eventid = int.Parse(btnEdit.CommandArgument);
            ViewState["EVENTID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(eventid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView(int CollegeNo)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_MASTER EM INNER JOIN SPRT_EVENT_TYPE_MASTER ETM ON (EM.ETID = ETM.ETID) INNER JOIN ACD_COLLEGE_MASTER CM ON (EM.COLLEGE_NO = CM.COLLEGE_ID)", "EM.EVENTID, EM.EVENTNAME, ETM.EVENT_TYPE_NAME", "CM.COLLEGE_NAME", "EM.COLLEGE_NO = " + CollegeNo + " OR " + CollegeNo + "= 0", "EM.EVENTID DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEvent.DataSource = ds;
                lvEvent.DataBind();
                lvEvent.Visible = true;
            }
            else
            {
                lvEvent.DataSource = null;
                lvEvent.DataBind();
                lvEvent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int eventid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_EVENT_MASTER", "*", "", "EVENTID=" + eventid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEventName.Text = ds.Tables[0].Rows[0]["EVENTNAME"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["ETID"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtEventName.Text = string.Empty;
       ddlCollege.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        ViewState["EVENTID"] = null;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Event List", "EventListReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Masters_EventMaster.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=EventListReport.pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_EVENTID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_EventMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindlistView(Convert.ToInt32(ddlCollege.SelectedValue));
    }
}