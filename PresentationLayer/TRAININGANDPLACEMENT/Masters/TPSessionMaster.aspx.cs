using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class TRAININGANDPLACEMENT_Masters_TPSessionMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();

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

                    // Set form mode equals to -1(New Mode).
                    //ViewState["activityno"] = "0";                                   

                    ViewState["action"] = "add";
                    BindListViewSession();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPSessionMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPSessionMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPSessionMaster.aspx");
        }
    }
    private void BindListViewSession()
    {
        try
        {
            DataSet ds = objTP.GetAllSession();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSession.DataSource = ds;
                lvSession.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPSessionMaster.BindListViewSession -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Compare(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text)) == 1)
            {
                objCommon.DisplayMessage(updActivity, "From Date Can Not Be Greater Than To Date. ", this);
                txtEndDate.Focus();
                return;
            }
            string Sname = txtSession.Text.Trim();
            DateTime FDt= Convert.ToDateTime(txtStartDate.Text.Trim());
            DateTime TDt= Convert.ToDateTime(txtEndDate.Text.Trim());
            string  CollegeCode = Convert.ToString(Session["colcode"]);
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objTP.AddSession(Sname, FDt, TDt, CollegeCode);
                    if (Convert.ToInt32(cs) != -99)
                    {
                        objCommon.DisplayMessage(this.updActivity, "Record saved successfully", this.Page);
                        BindListViewSession();
                        //ViewState["action"] = "add";
                        Clear();
                        //Response.Redirect(Request.Url.ToString());
                    }                    
                }
                else
                {
                    //Edit  Session
                    if (ViewState["SessionNo"] != null)
                    {
                        int SessionNO = Convert.ToInt32(ViewState["SessionNo"].ToString());

                        CustomStatus cs = (CustomStatus)objTP.UpdateSession(SessionNO, Sname, FDt, TDt);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewSession();
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updActivity, "Record Updated successfully", this.Page);
                            //Response.Redirect(Request.Url.ToString());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPSessionMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SNo = int.Parse(btnEdit.CommandArgument);
            ViewState["SessionNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(SNo);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPSessionMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int SesNo)
    {
        try
        {
            DataSet ds = objTP.GetSessionByNO(SesNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSession.Text = ds.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["FROMDATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["TODATE"].ToString();                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPSessionMaster.ShowDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtSession.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        //txtSessioncode.Text = string.Empty;
        ViewState["SessionNo"] = null;        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        Response.Redirect(Request.Url.ToString());
    }
}
