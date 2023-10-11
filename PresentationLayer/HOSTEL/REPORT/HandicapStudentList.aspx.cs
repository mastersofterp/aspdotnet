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
using System.IO;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class HOSTEL_REPORT_HandicapStudentList : System.Web.UI.Page
{
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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                }
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HandicapStudentList.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateDropDownList()
    {
        //objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
        objCommon.FillDropDownList(ddlHostelSessionNo, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND IS_SHOW=1", "FLOCK DESC");
        ddlHostelSessionNo.SelectedIndex = 1;

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HandicapStudentList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HOSTEL_REPORT_HandicapStudentList.aspx");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = B.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HandicapStudentList.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //string check = objCommon.LookUp("ACD_HOSTEL_APPLY_STUDENT HA INNER JOIN ACD_STUDENT S ON(HA.IDNO=S.IDNO)", "count(s.idno)", "CONVERT(NVARCHAR(30),HA.APPLY_DATE,121) BETWEEN CONVERT(datetime,'" + Convert.ToDateTime(txtFromdate.Text) + "',121) AND CONVERT(datetime,'" + Convert.ToDateTime(txtTodate.Text) + "',121) AND (HA.SESSIONNO=" + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + " OR " + Convert.ToInt32(ddlHostelSessionNo.SelectedValue) + "=0) AND (S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0)	AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0)");
        //if (check == "0")
        //    objCommon.DisplayMessage("Record Not Found!!", this.Page);
        //else
        ShowReport("Physically_Handicap_Student", "PhysicallyHandicapStud.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=" + "HandicapStudentListForApplyHostel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSIONNO", Convert.ToInt32(ddlHostelSessionNo.SelectedValue)),
               new SqlParameter("@P_DEGREENO", Convert.ToInt32(ddlDegree.SelectedValue)),
               new SqlParameter("@P_BRANCHNO",Convert.ToInt32(ddlBranch.SelectedValue)),
               new SqlParameter("@P_SEMESTERNO",Convert.ToInt32(ddlSemester.SelectedValue)),
               new SqlParameter("@P_ORGANIZATION_ID",Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_REPORT_APPLY_HANDICAP_STUDENT_EXCEL",objParams);

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
