//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : HOSTEL ROOM ALLOTMENT STATUS                                         
// CREATION DATE : 02-DEC-2010                                                    
// CREATED BY    : GAURAV SONI                                                      
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;
using System.IO;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;


public partial class Academic_RoomAllotmentStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    #region Page Evnets
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
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Fill DropDown List
                PopulateDropDownList();
                ddlRoomNo.SelectedIndex = 0;
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_RoomAllotmentStatus.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=RoomAllotmentStatus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RoomAllotmentStatus.aspx");
        }
    }

    #endregion

    #region Form Methods
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page

            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_RoomAllotmentStatus.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Room_Allotment_Status", "rptBlockWiseRoomAllotmentStatus.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_RoomAllotmentStatus.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnstud_Click(object sender, EventArgs e)
    {
        ShowReport("Room_Allotment_Status", "rptHostelStudentlist.rpt");
    }
    protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlockNo.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostelNo.SelectedValue + " AND BLK_NO=" + ddlBlockNo.SelectedValue, "FLOOR_NO");
               
                ddlRoomNo.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.ddlFloor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        } 
    }

    protected void ddlHostelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlHostelNo.SelectedIndex > 0)
            {
                //FILL DROPDOWN BLOCK NO
               objCommon.FillDropDownList(ddlBlockNo, "ACD_HOSTEL_BLOCK_MASTER", "DISTINCT BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostelNo.SelectedValue)+" and organizationid="+Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])+"", "BLOCK_NAME");
                ddlBlockNo.Focus();
            }
            else
            {
                ddlHostelNo.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.ddlHostelNo_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    #endregion

    #region Private Methods
    protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFloor.SelectedIndex > 0)
            {
                //FILL DROPDOWN ROOM NO
                objCommon.FillDropDownList(ddlRoomNo, "ACD_HOSTEL_ROOM HR INNER JOIN ACD_HOSTEL H ON H.HOSTEL_NO=HR.HBNO", "DISTINCT ROOM_NO", "ROOM_NAME", "BLOCK_NO=" + Convert.ToInt32(ddlBlockNo.SelectedValue) + " AND H.HOSTEL_NO=" + ddlHostelNo.SelectedValue, "ROOM_NAME");
                ddlRoomNo.Focus();
            }
            else
            {
                ddlFloor.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_RoomAllotmentStatus.ddlFloor_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND IS_SHOW=1", "FLOCK DESC");
            ddlSession.SelectedIndex = 1;
            //FILL DROPDOWN HOSTEL NO
            objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
            objCommon.FillDropDownList(ddlBlockNo, "ACD_HOSTEL_BLOCK_MASTER", "DISTINCT BL_NO", "BLOCK_NAME", "","BLOCK_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int orderby = 1;
        int vacant = 0;
        if (rdoAdmissionNo.Checked)
        {
            orderby = 2;
        }
        if (rdovacant.Checked)
        {
            vacant = 1;
        }
        else if (rdonotvacant.Checked)
        {
            vacant = 2;
        }

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_NO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_BLOCK_NO=" + Convert.ToInt32(ddlBlockNo.SelectedValue) + ",@P_ROOM_NO=" + Convert.ToInt32(ddlRoomNo.SelectedValue) + ",username=" + Session["username"].ToString(); 
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_NO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_BLOCK_NO=" + Convert.ToInt32(ddlBlockNo.SelectedValue) + ",@P_FLOOR_NO=" + Convert.ToInt32(ddlFloor.SelectedValue) + ",@P_ROOM_NO=" + Convert.ToInt32(ddlRoomNo.SelectedValue) + ",@P_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_ORDERBY=" + orderby + ",@P_VACANT=" + vacant + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_NO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_RoomAllotmentStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnvacantroom_Click(object sender, EventArgs e)
    {
        ShowReport("Room_Allotment_Status", "rptHostelWiseVacantRoom.rpt");
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        int orderby = 1;
        int vacant = 0;
        if (rdoAdmissionNo.Checked)
        {
            orderby = 2;
        }
        if (rdovacant.Checked)
        {
            vacant = 1;
        }
        else if (rdonotvacant.Checked)
        {
            vacant = 2;
        }
        try
        {
            string attachment = "attachment; filename=" + "RoomAllotmentStatus.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSION_NO", Convert.ToInt32(ddlSession.SelectedValue)),
               new SqlParameter("@P_HOSTEL_NO", Convert.ToInt32(ddlHostelNo.SelectedValue)),
               new SqlParameter("@P_BLOCK_NO", Convert.ToInt32(ddlBlockNo.SelectedValue)),
               new SqlParameter("@P_FLOOR_NO",Convert.ToInt32(ddlFloor.SelectedValue)),
               new SqlParameter("@P_ROOM_NO",Convert.ToInt32(ddlRoomNo.SelectedValue)),
               new SqlParameter("@P_ORDERBY",orderby),
               new SqlParameter("@P_VACANT",vacant),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_REPORT_ROOM_ALLOTMENT_STATUS_EXCEL",objParams);

            DataTable dt = dsfee.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {

            }
            DataGrid dg = new DataGrid();

            if (dsfee.Tables.Count > 0)
            {
                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();

            }
            dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnstudExcel_Click(object sender, EventArgs e)
    {
        int orderby = 1;
        int vacant = 0;
        if (rdoAdmissionNo.Checked)
        {
            orderby = 2;
        }
        if (rdovacant.Checked)
        {
            vacant = 1;
        }
        else if (rdonotvacant.Checked)
        {
            vacant = 2;
        }
        try
        {
            string attachment = "attachment; filename=" + "StudentListForHostel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSION_NO", Convert.ToInt32(ddlSession.SelectedValue)),
               new SqlParameter("@P_HOSTEL_NO", Convert.ToInt32(ddlHostelNo.SelectedValue)),
               new SqlParameter("@P_BLOCK_NO", Convert.ToInt32(ddlBlockNo.SelectedValue)),
               new SqlParameter("@P_FLOOR_NO",Convert.ToInt32(ddlFloor.SelectedValue)),
               new SqlParameter("@P_ROOM_NO",Convert.ToInt32(ddlRoomNo.SelectedValue)),
               new SqlParameter("@P_ORDERBY",orderby),
               new SqlParameter("@P_VACANT",vacant),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_REPORT_ROOM_ALLOTMENT_STUDENT_LIST_EXCEL",objParams);

            DataTable dt = dsfee.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {

            }
            DataGrid dg = new DataGrid();

            if (dsfee.Tables.Count > 0)
            {
                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();

            }
            dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnvacantroomExcel_Click(object sender, EventArgs e)
    {
        int orderby = 1;
        int vacant = 0;
        if (rdoAdmissionNo.Checked)
        {
            orderby = 2;
        }
        if (rdovacant.Checked)
        {
            vacant = 1;
        }
        else if (rdonotvacant.Checked)
        {
            vacant = 2;
        }
        try
        {
            string attachment = "attachment; filename=" + "VacantReportOfRoom.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSION_NO", Convert.ToInt32(ddlSession.SelectedValue)),
               new SqlParameter("@P_HOSTEL_NO", Convert.ToInt32(ddlHostelNo.SelectedValue)),
               new SqlParameter("@P_BLOCK_NO", Convert.ToInt32(ddlBlockNo.SelectedValue)),
               new SqlParameter("@P_FLOOR_NO",Convert.ToInt32(ddlFloor.SelectedValue)),
               new SqlParameter("@P_ROOM_NO",Convert.ToInt32(ddlRoomNo.SelectedValue)),
               new SqlParameter("@P_ORDERBY",orderby),
               new SqlParameter("@P_VACANT",vacant),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_REPORT_ROOM_ALLOTMENT_STATUS_VACANT_EXCEL", objParams);

            DataTable dt = dsfee.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {

            }
            DataGrid dg = new DataGrid();

            if (dsfee.Tables.Count > 0)
            {
                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();

            }
            dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
}

