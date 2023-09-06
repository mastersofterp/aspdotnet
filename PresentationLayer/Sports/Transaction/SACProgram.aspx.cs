//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 16-DEC-2014
//DESCRIPTION   : THIS FORM IS USED FOR SAC PROGRAM ENTRY.    
//================================================================================================  
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


public partial class Sports_Transaction_SACProgram : System.Web.UI.Page
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

                    objCommon.FillDropDownList(ddlClub, "SPRT_CLUB_MASTER", "CLUBID", "CLUBNAME", "", "CLUBNAME");
                    objCommon.FillDropDownList(ddlEvent, "SPRT_EVENT_MASTER", "EVENTID", "EVENTNAME", "", "EVENTNAME");
                    objCommon.FillDropDownList(ddlVenue, "SPRT_VENUE_MASTER", "VENUEID", "VENUENAME", "", "VENUENAME");

                }
                BindlistView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SACProgram.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!txtFrmDt.Text.Equals(string.Empty))
            {
                if (DateTime.Compare(Convert.ToDateTime(txtFrmDt.Text), Convert.ToDateTime(txtToDt.Text)) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date...!!');", true);
                    txtFrmDt.Text = string.Empty;
                    txtToDt.Text = string.Empty;
                    txtFrmDt.Focus();
                    return;
                }
            }


            objSport.EVENTID = Convert.ToInt32(ddlEvent.SelectedValue.ToString());
            objSport.EVENT_FROMDATE = Convert.ToDateTime(txtFrmDt.Text.Trim());
            objSport.EVENT_TODATE = Convert.ToDateTime(txtToDt.Text.Trim());
            objSport.CLUBID = Convert.ToInt32(ddlClub.SelectedValue.ToString());
            objSport.VENUEID = Convert.ToInt32(ddlVenue.SelectedValue.ToString());
            objSport.USERID = Convert.ToInt32(Session["userno"]);


            if (ViewState["SACID"] == null)
            {
                objSport.SACID = 0;
                CustomStatus cs = (CustomStatus)objSportC.AddUpdate_SAC_Program(objSport);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Already Exist", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);

            }
            else
            {
                objSport.SACID = Convert.ToInt32(ViewState["SACID"].ToString());
                objSportC.AddUpdate_SAC_Program(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);

            }
            Clear();
            BindlistView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SACProgram.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int sacid = int.Parse(btnEdit.CommandArgument);
            ViewState["SACID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(sacid);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SACProgram.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SAC_PROGRAM SP INNER JOIN SPRT_CLUB_MASTER CM ON(CM.CLUBID = SP.CLUBID) INNER JOIN SPRT_VENUE_MASTER VM ON(VM.VENUEID = SP.VENUEID)INNER JOIN SPRT_EVENT_MASTER EC ON(EC.EVENTID = SP.EVENTID)", "SP.SACID,SP.SAC_FROM_DATE,SP.SAC_TO_DATE", "CM.CLUBNAME, VM.VENUENAME, EC.EVENTNAME", "", "SACID");
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSAC.DataSource = ds;
                lvSAC.DataBind();
            }
            else
            {
                lvSAC.DataSource = null;
                lvSAC.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SACProgram.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int sacid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SAC_PROGRAM", "SACID,SAC_FROM_DATE,SAC_TO_DATE", "CLUBID, EVENTID, VENUEID", "SACID=" + sacid, ""); 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEvent.SelectedValue = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                txtFrmDt.Text = ds.Tables[0].Rows[0]["SAC_FROM_DATE"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["SAC_TO_DATE"].ToString();
                ddlClub.SelectedValue = ds.Tables[0].Rows[0]["CLUBID"].ToString();
                ddlVenue.SelectedValue = ds.Tables[0].Rows[0]["VENUEID"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Transaction_SACProgram.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlEvent.SelectedIndex = 0;
        ddlClub.SelectedIndex = 0;
        ddlVenue.SelectedIndex = 0;
        txtFrmDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        lvSAC.DataSource = null;
        lvSAC.DataBind();
        ViewState["SACID"] = null;
    }

}
   
   
  
   
   
  
 
    
