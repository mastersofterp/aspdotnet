//===========================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 06-02-2015
// DESCRIPTION   : USED TO CREATE GAME TYPE.
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

public partial class Sports_Masters_SportTypeMaster : System.Web.UI.Page
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
                objUCommon.ShowError(Page, "Sports_Masters_SportTypeMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
           
            objSport.GAME_TYPE = txtSportTyp.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtSportTyp.Text);
            objSport.USERID = Convert.ToInt32(Session["userno"]);

            if (ViewState["TYPID"] == null)
            {
                   objSport.TYPID = 0;
                   CustomStatus cs = (CustomStatus)objSportC.AddUpdateSportTypeMaster(objSport);

                   if (cs.Equals(CustomStatus.RecordExist))
                   {
                       Clear();
                       objCommon.DisplayMessage(this.updActivity, "This Sport Type Already Exist.", this.Page);
                       return;
                   }
                   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sport Type Submitted Successfully.');", true);
                   Clear();
                   BindlistView();
            }
            else
            {
                objSport.TYPID = Convert.ToInt32(ViewState["TYPID"].ToString());
                objSportC.AddUpdateSportTypeMaster(objSport);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sport Type Updated Successfully.');", true);
                Clear();
                BindlistView();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportTypeMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            int typid = int.Parse(btnEdit.CommandArgument);
            ViewState["TYPID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(typid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportTypeMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SPORT_TYPE ", "TYPID", "GAME_TYPE", "", "TYPID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSprtTyp.DataSource = ds;
                lvSprtTyp.DataBind();
            }
            else
            {
                lvSprtTyp.DataSource = null;
                lvSprtTyp.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportTypeMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int typid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SPRT_SPORT_TYPE", "*", "", "TYPID=" + typid, "");
            if (ds.Tables[0].Rows.Count > 0)
            {               
                txtSportTyp.Text = ds.Tables[0].Rows[0]["GAME_TYPE"].ToString();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportTypeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtSportTyp.Text = string.Empty;        
        ViewState["TYPID"] = null;
    }
  
}
