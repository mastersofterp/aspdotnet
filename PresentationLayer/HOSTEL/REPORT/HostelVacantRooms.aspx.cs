//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                       
// PAGE NAME     : HOSTEL VACANT ROOMS                                                  
// CREATION DATE : 26-DEC-2010                                                       
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

public partial class Hostel_Report_HostelVacantRooms : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=HoselVacantRooms.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HoselVacantRooms.aspx");
        }
    }


    #endregion

    #region Form Ecents
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Hostel_Vacant_Rooms", "VacantRooms.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

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
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlHostelNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlHostelNo.SelectedIndex > 0)
            {
                //FILL DROPDOWN BLOCK NO
                objCommon.FillDropDownList(ddlBlockNo, "ACD_HOSTEL_BLOCK_MASTER", "DISTINCT BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlHostelNo.SelectedValue) + " and organizationid=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "BLOCK_NAME");
                //objCommon.FillDropDownList(ddlBlockNo, "ACD_HOSTEL_BLOCK_MASTER", "DISTINCT BL_NO", "BLOCK_NAME", "", "BLOCK_NAME");
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
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=" + "Hostelvacantroom.xls";
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
               new SqlParameter("@P_BLOCK_NO",Convert.ToInt32(ddlBlockNo.SelectedValue)),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_REPORT_VACANT_ROOMS_EXCEL", objParams);

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
    #endregion

    #region Private Methods

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_NO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",username=" + Session["username"].ToString() + ",@P_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + ",@P_BLOCK_NO=" + Convert.ToInt32(ddlBlockNo.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlockNo.SelectedIndex > 0)
            {
                this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostelNo.SelectedValue + " AND BLK_NO=" + ddlBlockNo.SelectedValue, "FLOOR_NO");
                ddlFloor.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.ddlBlockNo_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0 and flock=1", "HOSTEL_SESSION_NO desc");
            //objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND IS_SHOW=1", "FLOCK DESC");

            ddlSession.SelectedIndex = 1;

            // FILL DROPDOWN SEMESTER
            // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            //FILL DROPDOWN BRANCH
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "BRANCHNO");
            //FILL DROPDOWN HOSTEL NO
            objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
            //FILL DROPDOWN RESIDENT TYPE NO
            //objCommon.FillDropDownList(ddlResidentTypeNo, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "RESIDENT_TYPE_NO>0", "RESIDENT_TYPE_NO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion
    
}
    

