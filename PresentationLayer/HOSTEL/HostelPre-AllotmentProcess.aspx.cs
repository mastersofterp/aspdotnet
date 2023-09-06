using System;
using System.Collections;
using System.Configuration;
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
public partial class HostelPreAllotmentProcess: System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MessBillController objMbc = new MessBillController();
    RoomAllotmentController raController = new RoomAllotmentController();
    #region Page Events
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelFineReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HostelFineReport.aspx");
        }
    }
    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL DROPDOWN HOSTEL SESSION NO.
            this.objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 6 ", "HOSTEL_SESSION_NO DESC");
            this.objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HBNO", "HOSTEL_NAME", "HBNO > 0", "HOSTEL_NAME");
            //ddlHostelNo.Items.Add(new ListItem("All Hostels", "99"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
            int status = 0;
            status = raController.HostelPreApplyStudentInsert(Convert.ToInt32(ddlHostelNo.SelectedValue), Convert.ToInt32(ddlHostelSessionNo.SelectedValue));
            if (status > 1)
            {
                objCommon.DisplayMessage("Process Done Successfully for hostel session  " + ddlHostelSessionNo.SelectedItem.Text, this.Page);
            }
            else
            {
                objCommon.DisplayMessage("No Data found for hostel session  " + ddlHostelSessionNo.SelectedItem.Text, this.Page);
            }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear(); 
    }
    private void Clear()
    {
        ddlHostelNo.SelectedIndex = 0;
        ddlHostelSessionNo.SelectedIndex = 0;
    }

    protected void btnallotlastallotement_Click(object sender, EventArgs e)
    {
        int status = 0;
        status = raController.HostelLastSessionAllotementInsert(Convert.ToInt32(ddlHostelNo.SelectedValue), 10);
        if (status == 1)
        {
            objCommon.DisplayMessage("Hostel Last Session Allotement Insert Successfully for hostel session  " + ddlHostelSessionNo.SelectedItem.Text, this.Page);
        }
        else
        {
            objCommon.DisplayMessage("No Data found for hostel session  " + ddlHostelSessionNo.SelectedItem.Text, this.Page);
        }
    }
}

