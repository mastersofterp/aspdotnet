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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Data;
using System.IO;
public partial class ACADEMIC_EVENT_EventReports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventCreationController objEC = new EventCreationController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                //if (Session["usertype"].Equals(14))
                //{
                //}
                //else
                //{
                //    Response.Redirect("~/notauthorized.aspx?page=AffiliatedProfileInstitute.aspx");
                //}
            }
            PopulateDropDown();
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EventReports.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EventReports.aspx.aspx");
        }
    }
    protected void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlEventType, "ACD_EVENT_TYPE", "EVENT_ID", "EVENT_TYPE", "EVENT_ID > 0", "EVENT_ID");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objEC.GetEventDetailsList(Convert.ToInt32(ddlEventType.SelectedValue), Convert.ToInt32(ddlEventTitle.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventDetails.DataSource = ds;
                lvEventDetails.DataBind();
                pnlEvent.Visible = true;
                lvEventDetails.Visible = true;
                pnlParticipant.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventReports.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnParticipant_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GvStudent = new GridView();
            DataSet ds = objEC.GetParticipantList_Excel(Convert.ToInt32(ddlEventType.SelectedValue), Convert.ToInt32(ddlEventTitle.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GvStudent.DataSource = ds;
                GvStudent.DataBind();
                string attachment = "attachment; filename=EventParticipantList.xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/xlsx";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GvStudent.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventReports.btnParticipant_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEventType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEventTitle, "ACD_EVENT_CREATION", "EVENT_TITLE_ID", "EVENT_TITLE", "EVENT_TITLE_ID > 0 AND EVENT_TYPE=" + Convert.ToInt32(ddlEventType.SelectedValue), "EVENT_TITLE");
            }
            pnlEvent.Visible = false;
            lvEventDetails.Visible = false;
            pnlParticipant.Visible = false;
            lvParticipant.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventReports.ddlEventType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlEventTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlEvent.Visible = false;
            lvEventDetails.Visible = false;
            pnlParticipant.Visible = false;
            lvParticipant.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventReports.ddlEventTitle_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnParticipantDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objEC.GetParticipantList(Convert.ToInt32(ddlEventType.SelectedValue), Convert.ToInt32(ddlEventTitle.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvParticipant.DataSource = ds;
                lvParticipant.DataBind();
                pnlParticipant.Visible = true;
                lvParticipant.Visible = true;
                pnlEvent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventReports.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}