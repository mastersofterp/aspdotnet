//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Access_Link_Payroll.ASPX                                                    
// CREATION DATE : 22-JULY-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_Access_Link_Payroll : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayRefLinkController objPayRef = new PayRefLinkController();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                FillModuleName();
                BindListViewList();
              
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
                Response.Redirect("~/notauthorized.aspx?page=Access_Link_Payroll.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Access_Link_Payroll.aspx");
        }
    }

    private void BindListViewList()
    {
        try
        {
            DataSet ds = objPayRef.GetLinkName();
            lvPayRef.DataSource = ds;
            lvPayRef.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page,"PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page,"Server UnAvailable");
        }
    }



    protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlLinkName.SelectedValue = "0";
        FillLinkName(Convert.ToInt32(ddlModuleName.SelectedValue.ToString()));
    }

    protected void FillModuleName()
    {
        try
        {
            objCommon.FillDropDownList(ddlModuleName,"ACC_SECTION","AS_NO","AS_TITLE","AS_NO NOT IN (0)","AS_TITLE");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Access_Link_Payroll.FillModuleName-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillLinkName(int moduleno)
    {
        try
        {

            objCommon.FillDropDownList(ddlLinkName, "ACCESS_LINK", "AL_NO", "AL_LINK", "AL_ASNO=" + moduleno, "AL_LINK");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Access_Link_Payroll.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSub_Click(object sender, EventArgs e)
    {

        try
        {
            int count = 0;
            CustomStatus cs = (CustomStatus)objPayRef.UpdateModuleNoLinkNo(Convert.ToInt32(ddlModuleName.SelectedValue), Convert.ToInt32(ddlLinkName.SelectedValue));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                    count = 1;
            }
            if (count == 1)
            {
                lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage(UpdatePanel1, "Record Saved Successfully", this);
            }
            BindListViewList();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Access_Link_Payroll.btnSub_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlLinkName.SelectedValue = "0";
        ddlModuleName.SelectedValue = "0";
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty; 

    }
    protected void lvPayRef_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
