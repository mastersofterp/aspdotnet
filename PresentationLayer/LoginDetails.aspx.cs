using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
public partial class LoginDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FetchDataController fetch = new FetchDataController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // sssssssssSet MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                    // this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID >0", "USERTYPEID");
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_LoginAnalysis.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void bntShow_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();


            FromDate = Convert.ToDateTime(txtFromDate.Text);
            ToDate = Convert.ToDateTime(txtToDate.Text);
            int UType = Convert.ToInt32(ddlUserType.SelectedValue);
            if (FromDate >= ToDate)
            {
                objCommon.DisplayMessage(UpdatePanel1, "From Date Should Be Smaller Than To Date", this.Page);
                return;
            }
            DataSet ds = fetch.GetLoginAnalysis(FromDate, ToDate, UType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvUserLists.DataSource = ds;
                lvUserLists.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "No Record Found", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoginAnalysis.btnShow_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvUserLists.DataSource = null;
        lvUserLists.DataBind();
    }
}