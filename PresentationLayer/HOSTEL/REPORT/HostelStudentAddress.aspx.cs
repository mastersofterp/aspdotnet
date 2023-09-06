//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                                      
// PAGE NAME     : HOSTEL STUDENT ADDRESS                                               
// CREATION DATE : 28-DEC-2010                                                          
// CREATED BY    : GAURAV SONI                                                      
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================


using System;
using System.Web.UI;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

public partial class Academic_HostelStudentAddress : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = "";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentAddress.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=HostelStudentAddress.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelStudentAddress.aspx");
        }
    }
    #endregion

    #region Private Methods
    protected void PopulateDropDownList()
    {
        try
        {
            //FILL DROPDOWN HOSTEL SESSION NO.

            objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
            ddlHostelSessionNo.SelectedIndex = 1;

           // if (Session["usertype"].ToString() == "1")
            objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HBNO");
           // else
               // objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
            //FILL DROPDOWN CITY
             objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");

            //FILL DROPDOWN STATE

            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_HostelStudentAddress.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_HBNO=" + Convert.ToInt32(ddlHostelNo.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_CITYNO=" + Convert.ToInt32(ddlCity.SelectedValue) + ",@P_STATENO=" + Convert.ToInt32(ddlState.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
         divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    #endregion

    #region Form Actions
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Hostel_Student_Address", "HostelStudentAddress.rpt");

            //if (ddlHostelSessionNo.SelectedIndex == 0 && ddlCity.SelectedIndex == 0 && ddlState.SelectedIndex == 0)
            //{
            //    ShowMessage("Please Select atleast one criteria.");
            //}
            //else
            //{
            //    ShowReport("Hostel_Student_Address", "HostelStudentAddress.rpt");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_HostelStudentAddress.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion


    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=" + "HostelStudentAddress.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_HBNO", Convert.ToInt32(ddlHostelNo.SelectedValue)),
               new SqlParameter("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedValue)),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
               new SqlParameter("@P_HOSTEL_SESSION_NO",Convert.ToInt32(ddlHostelSessionNo.SelectedValue)),
               new SqlParameter("@P_CITYNO",Convert.ToInt32(ddlCity.SelectedValue)),
               new SqlParameter("@P_STATENO",Convert.ToInt32(ddlState.SelectedValue)),
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_REPORT_STUDENT_ADDRESS_EXCEL", objParams);

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
