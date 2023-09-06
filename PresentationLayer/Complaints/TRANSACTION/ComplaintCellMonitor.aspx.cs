//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : REPAIR AND MAINTANANCE                                          
// PAGE NAME     : TO ALLOT COMPLAINT TO SELECTED EMPLOYEE                         
// CREATION DATE : 16-April-2006                                                   
// CREATED BY    : SANJAY RATNPARKHI & G.V.S KIRAN                                 
// MODIFIED BY   : MRUNAL SINGH 
// MODIFIED DATE : 10-SEP-2015
// MODIFIED DESC : CREATE COMPLAINT CELL FOR MIZO UNIVERSITY. NOW IT WILL BE GIVEN TO DEPT INCHARGE FOR LNMIIT
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;

public partial class Complaints_TRANSACTION_ComplaintCellMonitor : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    Complaint objCT = new Complaint();
    ComplaintController objCC = new ComplaintController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Check browser and set table width
                //if (Request.Browser.Browser.ToLower().Equals("opera"))
                //    tblMain.Width = "100%";
                //else if (Request.Browser.Browser.ToLower().Equals("ie"))
                //    tblMain.Width = "100%";
                //else
                //    tblMain.Width = "100%";

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                else
                {
                    //lblHelp.Text = "No Help Added";
                }
                if (Convert.ToInt32(Session["userno"]) != 0)
                {
                    DataSet dsCU = null;
                    dsCU = objCommon.FillDropDown("COMPLAINT_COMMITTEE_USER", "USERNO", "EMPDEPTNO", "EMPID=" + Convert.ToInt32(Session["userno"]), "");
                    if (dsCU.Tables[0].Rows.Count == 0)
                    {
                        pnlDetails.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are not authorized for complaint cell details.');", true);
                        return;
                    }
                    else
                    {
                        ViewState["DEPTID"] = dsCU.Tables[0].Rows[0]["EMPDEPTNO"].ToString();
                    }
                }

                BindListView();
            }
        }

    }


    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_REGISTER CR INNER JOIN COMPLAINT_PRIORITY_WORK CP ON (CR.PWID = CP.PWID) INNER JOIN COMPLAINT_TYPE CT ON (CR.TYPEID = CT.TYPEID)	 INNER JOIN COMPLAINT_AREA CA ON (CR.AREAID = CA.AREAID)", "COMPLAINTID,COMPLAINTNO,COMPLAINTDATE,COMPLAINTEE_NAME,COMPLAINTEE_ADDRESS,	ALLOTMENTSTATUS, CR.AREAID", "CR.PWID, CP.PWNAME,CR.TYPEID, CT.TYPENAME,CA.AREANAME,(CASE COMPLAINTSTATUS WHEN 'P' THEN 'PENDING' WHEN 'I' THEN 'IN PROCESS' WHEN 'C' THEN 'COMPLETE' END) AS COMPLAINTSTATUS", "CR.DEPTID =" + Convert.ToInt32(ViewState["DEPTID"]), "COMPLAINTID DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvComplaint.DataSource = ds;
                lvComplaint.DataBind();
                ds.Dispose();
            }
            else
            {
                lvComplaint.DataSource = null;
                lvComplaint.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    private void ClearAll()
    {
        pnlDetails.Visible = false;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        // ComplaintController objCC = new ComplaintController();
        BindListView();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=complaint.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=complaint.aspx");
        }
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ComplaintId = int.Parse(btnEdit.CommandArgument);
            ViewState["ComplaintId"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(ComplaintId);
            pnlDetails.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int ComplaintId)
    {
        try
        {
            DataSet ds = objCC.GetCompletedComplaints(ComplaintId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblComplaintNo.Text = ds.Tables[0].Rows[0]["COMPLAINTNO"].ToString();
                lblComplaintDate.Text = ds.Tables[0].Rows[0]["COMPLAINTDATE"].ToString();
                lblComplaint.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                lblComplainteeName.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"].ToString();
                lblComplainteeAddress.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_ADDRESS"].ToString();
                lblContactNo.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_PHONENO"].ToString();
                lblOtherContactNo.Text = ds.Tables[0].Rows[0]["OTHER_CONTACTNO"].ToString();
                lblComplaintStatus.Text = ds.Tables[0].Rows[0]["COMPLAINTSTATUS"].ToString();
                lblWorkPriority.Text = ds.Tables[0].Rows[0]["PWNAME"].ToString();
                lblArea.Text = ds.Tables[0].Rows[0]["AREANAME"].ToString();
                lblComplaintNature.Text = ds.Tables[0].Rows[0]["TYPENAME"].ToString();
                lblWorkoutDetails.Text = ds.Tables[0].Rows[0]["WORKOUT"].ToString();
                lblWorkDate.Text = ds.Tables[0].Rows[0]["WORKDATE"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
          
}