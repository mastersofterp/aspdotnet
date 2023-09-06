//===========================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 01-09-2014
// DESCRIPTION   : USED TO CREATE SPORT NAME
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

public partial class Sports_Masters_SportMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport= new Sport();

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
                    objCommon.FillDropDownList(ddlSportType, "SPRT_SPORT_TYPE", "TYPID", "GAME_TYPE", "", "GAME_TYPE");

                }
                //BindlistView();

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    private bool checkSportExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER", "SPID", "SNAME", "SNAME='" + txtSportName.Text + "'", "SPID");
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

            objSport.TYPID = Convert.ToInt32(ddlSportType.SelectedValue.ToString());
            objSport.SNAME = txtSportName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSportName.Text);
            objSport.NO_OF_PLAYERS = txtNoOfplayers.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtNoOfplayers.Text);
            objSport.NO_OF_RESERVE = txtNoOfReserve.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtNoOfReserve.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);
            if (Convert.ToInt32(objSport.NO_OF_RESERVE) > Convert.ToInt32(objSport.NO_OF_PLAYERS))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Number Of Players Should Be Greater Than Number Of Reserve Players');", true);
                return;
            }
           
            if (ViewState["SPID"] == null)
            {

                if (checkSportExist())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Sport Name Already Exist...!!');", true);
                    Clear();

                }
                else
                {
                    objSportC.AddUpdateSportMaster(objSport);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sport Name Submitted Successfully...!!');", true);
                    Clear();
                }
              }
            else
            {

                objSport.SPID = Convert.ToInt32(ViewState["SPID"].ToString());

                objSportC.AddUpdateSportMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sport Name Updated Successfully...!!');", true);
                Clear();
               
              
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int sport_id= int.Parse(btnEdit.CommandArgument);
            ViewState["SPID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(sport_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER SM INNER JOIN SPRT_SPORT_TYPE ST ON (SM.TYPID=ST.TYPID)", "*", "SM.SNAME,SM.SPID", "SM.TYPID='" + Convert.ToInt32(ddlSportType.SelectedValue) + "'", "SPID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSport.DataSource = ds;
                lvSport.DataBind();
            }
            else
            {
                lvSport.DataSource = null;
                lvSport.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int sport_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SPORT_MASTER", "*", "", "SPID=" + sport_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSportType.SelectedValue = ds.Tables[0].Rows[0]["TYPID"].ToString();
                txtSportName.Text = ds.Tables[0].Rows[0]["SNAME"].ToString();
                txtNoOfplayers.Text = ds.Tables[0].Rows[0]["NO_OF_PLAYERS"].ToString();
                txtNoOfReserve.Text = ds.Tables[0].Rows[0]["NO_OF_RESERVE"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlSportType.SelectedIndex = 0;
        txtSportName.Text = string.Empty;
        txtNoOfplayers.Text = string.Empty;
        txtNoOfReserve.Text = string.Empty;
        lvSport.DataSource = null;
        lvSport.DataBind();
        ViewState["SPID"] = null;

    }
    protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSportType.SelectedIndex > 0)
        {

            BindlistView();
        }
        if (ddlSportType.SelectedIndex == 0)
        {
            lvSport.DataSource=null;
            lvSport.DataBind();
            //lvSport.Visible = false;
        }
    }

    protected void txtSportName_TextChanged(object sender, EventArgs e)
    {

    }
    
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + ",@P_SPID=0";


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("SportsList", "SportsList.rpt");
    }
}
