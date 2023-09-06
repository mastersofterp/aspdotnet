using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class STORES_Masters_Str_Recommend_passing_path : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    StoreMasterController objPAPath = new StoreMasterController();

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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               
                FillPAuthority();
                BindListViewPAPath();                
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    protected void BindListViewPAPath()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Store_Recom_Approval_Path a inner join User_Acc b on (a.UA_NO=b.UA_NO)", "a.RECOM_APP_NO", "b.UA_FULLNAME", "", "");
            lvPAPath.DataSource = ds;
            lvPAPath.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }   

    private void Clear()
    {
        ddlUser.SelectedValue = "0";
        ViewState["RECOM_APP_NO"] = null;
    }
  
    private void FillPAuthority()
    {
        try
        {
            objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE in (1,3,4)", "UA_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
               if (ViewState["RECOM_APP_NO"]==null)
                {
                    CustomStatus cs = (CustomStatus)objPAPath.INSUPDATERECOMPATH(Convert.ToInt32(0), Convert.ToInt32(ddlUser.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(pnlMessage, "Record Saved Succesfully", this);

                        ViewState["RECOM_APP_NO"] = null;
                        Clear();
                        BindListViewPAPath();
                    }
                    else
                    {
                        objCommon.DisplayMessage(pnlMessage, "Transaction Failed", this);
                    }
                }
                else
                {
                    if (ViewState["RECOM_APP_NO"] != null)
                    {                      
                        CustomStatus CS = (CustomStatus)objPAPath.INSUPDATERECOMPATH(Convert.ToInt32(ViewState["RECOM_APP_NO"].ToString()),Convert.ToInt32(ddlUser.SelectedValue),Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(pnlMessage, "Record Updated Succesfully", this);
                            ViewState["RECOM_APP_NO"] = null;
                            Clear();
                            BindListViewPAPath();
                        }
                        else
                        {
                            objCommon.DisplayMessage(pnlMessage, "Transaction Failed", this);
                        }
                    }
                }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int RECOM_APP_NO = int.Parse(btnEdit.CommandArgument);
            DataSet ds = objCommon.FillDropDown("Store_Recom_Approval_Path", "UA_NO", "USERID", "RECOM_APP_NO=" + RECOM_APP_NO, "");
            ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
            ViewState["RECOM_APP_NO"] = RECOM_APP_NO;           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int RECOM_APP_NO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objPAPath.deleteRecom_Approval_Path(RECOM_APP_NO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["RECOM_APP_NO"] = null;
                objCommon.DisplayMessage(pnlMessage, "Record Deleted Successfully", this);
                BindListViewPAPath();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}