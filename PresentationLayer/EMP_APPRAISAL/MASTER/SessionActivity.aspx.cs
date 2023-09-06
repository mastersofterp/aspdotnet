using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


public partial class EMP_APPRAISAL_MASTER_SessionActivity : System.Web.UI.Page
{
    Common objCommon = new Common();
    EmpSessionEnt objESEnt = new EmpSessionEnt();
    EmpSessionCon objESCon = new EmpSessionCon();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["action"] = "add";

                    // Fill drop down lists
                    this.objCommon.FillDropDownList(ddlSession, "APPRAISAL_SESSION_MASTER", "SESSION_ID", "SESSION_NAME", "IS_ACTIVE =1 ", "SESSION_ID DESC");
                    //this.objCommon.FillDropDownList(ddlSessionType, "APPRAISAL_APPRAISALTYPE", "AT_ID", "APPRAISAL_TYPE", "AT_ID>0", "AT_ID");
                    this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
                    BindListViewSessionActivity();
                }
            }

            //this.LoadDefinedSessionActivities();
            ddlSession.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalSessionActivity.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Feedback_Activity.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Feedback_Activity.aspx");
        }
    }
    // this method used to show massage
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    //this method used to clear controll
    private void Clear()
    {
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlSession.SelectedIndex = 0;
        ddlSessionType.SelectedIndex = 0;
        rdoStart.Checked = false;
        rdoStop.Checked = true;
        ddlCollege.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["SESSION_ACTIVITY_NO"] = null;
    }

    //this method is used to bind data in grid
    private void BindListViewSessionActivity()
    {
        try
        {
            DataSet ds = objESCon.GetAllActivitySessionEntry(Convert.ToInt32(ddlCollege.SelectedValue));
            lvActivity.DataSource = ds;
            lvActivity.DataBind();

            foreach (ListViewItem item in lvActivity.Items)
            {
                Label lblactinestatus = item.FindControl("lblactinestatus") as Label;
                if (lblactinestatus.Text == "ACTIVE")
                {
                    lblactinestatus.Text = "Active";
                    lblactinestatus.Style.Add("color", "Green");
                }
                else
                {
                    lblactinestatus.Text = "InActive";
                    lblactinestatus.Style.Add("color", "Red");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalSessionActivity.BindListViewLwpEntry()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //this method used to save data
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objESEnt.SESSION_NO = Convert.ToInt32(ddlSession.SelectedValue);
            objESEnt.SESSION_TYPE_NO = Convert.ToInt32(ddlSessionType.SelectedValue);
            objESEnt.STARTDATE = Convert.ToDateTime(txtStartDate.Text);
            objESEnt.ENDDATE = Convert.ToDateTime(txtEndDate.Text);
            objESEnt.IS_STARTED = rdoStart.Checked ? true : false;
            objESEnt.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objESEnt.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            objESEnt.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objESEnt.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objESEnt.SESSION_ACTIVITY_NO = 0;

                    DataSet ds = objESCon.GetSingleSessionEntryDetail(objESEnt);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox("Record Already Exist.");
                        Clear();
                        return;
                    }
                    CustomStatus cs = (CustomStatus)objESCon.AddUpdSessionActivity(objESEnt);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        ViewState["action"] = null;

                        Clear();
                    }
                }
                else
                {
                    if (ViewState["SESSION_ACTIVITY_NO"] != null)
                    {
                        objESEnt.SESSION_ACTIVITY_NO = Convert.ToInt32(ViewState["SESSION_ACTIVITY_NO"].ToString());

                        CustomStatus CS = (CustomStatus)objESCon.AddUpdSessionActivity(objESEnt);
                        if (CS.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Updated Successfully");
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
                BindListViewSessionActivity();
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRIASAL_AppraisalSessionCreation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //this method used to show data for modification
    private void ShowDetails(Int32 SessionActivityNo)
    {

        DataSet ds = null;
        try
        {
            ds = objESCon.GetSingleActivitySessionEntry(SessionActivityNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SESSION_ACTIVITY_NO"] = SessionActivityNo.ToString();
                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                ddlSessionType.SelectedValue = ds.Tables[0].Rows[0]["ATID"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["START_DATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["END_DATE"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STARTED"].ToString()) == false)
                {
                    rdoStop.Checked = true;
                    rdoStart.Checked = false;
                }
                else
                {
                    rdoStart.Checked = true;
                    rdoStop.Checked = false;
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalSessionActivity.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    //this is used to modify the data
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SessionActivityNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(SessionActivityNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalSessionActivity.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //this method used to cancle transaction
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int SESSION_ACTIVITY_NO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objESCon.DeleteSessionActivity(SESSION_ACTIVITY_NO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
                ViewState["action"] = null;
                BindListViewSessionActivity();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpAppraisal_AppraisalSessionActivity.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain            
    //    BindListViewSessionActivity();

    //}


    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {

        if (txtStartDate.Text.ToString() != string.Empty && txtStartDate.Text.ToString() != "__/__/____" && txtEndDate.Text.ToString() != string.Empty && txtEndDate.Text.ToString() != "__/__/____")
        {
            DateTime startDate = Convert.ToDateTime(txtStartDate.Text.ToString());

            DateTime endDate = Convert.ToDateTime(txtEndDate.Text.ToString());

            if (endDate < startDate)
            {
                MessageBox("End Date Should Be Larger Than Or Equals To Start Date");
                //txtTodt.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                return;
            }

        }
    }
}