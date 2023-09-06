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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Log History                            
// CREATION DATE : 26-3-2014                                                          
// CREATED BY    : TARUN                                              
// MODIFIED DATE : ZUBAIR                                                                     
// MODIFIED DESC : Add update panel and applied date validation.                                                                     
//======================================================================================



public partial class Itle_LogHistory : System.Web.UI.Page
{
    //IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    Common objCommon = new Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();

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

                    PopulateDropdown();
                    lblSession.Text = Session["SESSION_NAME"].ToString();
                    lblSession.ToolTip= Session["SessionNo"].ToString();
                    lblSession.ForeColor = System.Drawing.Color.Green;
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Itle_Student_Roll_List.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int studid = 0;
            if (ddlStudent.SelectedValue == "")
                studid = 0;
            else
                studid =Convert.ToInt32(ddlStudent.SelectedValue);

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Itle," + rptFileName;
            ////url += "&param=SESSIONNAME=" + ddlSessionNo.SelectedItem.Text + ",SCHEMENAME=" + ddlSchemeNo.SelectedItem.Text + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSessionNo.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlSchemeNo.SelectedValue) + ",username=" + Session["userfullname"].ToString();
            //SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
            url += "&param=@IDNO=" + studid + ",@LoginDate=" + txtLogDate.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Itle_Student_Roll_List.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtLogDate.Text != "")
            {
                ShowReport("Itle_Student_LogHistory_Report", "LogHistory.rpt");
            }
            else
            {
                objCommon.DisplayUserMessage(updLogHistory,"Please select Date !",this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LogHistory.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void PopulateDropdown()
    {
        try
        {

            objCommon.FillDropDownList(ddldegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO >0", "DEGREENO");
            //objCommon.FillDropDownList(ddlSchemeNo, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO");
            //objCommon.FillDropDownList(ddlSessionNo, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "SESSIONNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Itle_Student_Roll_List.PopulateDropdown()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Itle_Student_Roll_List.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Student_Roll_List.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
          if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_LogHistory.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlbranch, "acd_branch", "branchno", "LONGNAME as BranchName", "BRANCHNO>0 and DEGREENO=" + ddldegree.SelectedValue , "branchno");  //Previously it was like this
            objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = B.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME ", "DB.DEGREENO =" + ddldegree.SelectedValue, "B.LONGNAME ASC"); //Added on 03/07/2018

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LogHistory..ddldegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSchemeNo, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO >0  and  BRANCHNO=" + ddlbranch.SelectedValue , "SCHEMENO");

    }

    protected void ddlSchemeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SemeterFilter = "SEMESTERNO>0 and SEMESTERNO in ( select distinct SEMESTERNO from ACD_STUDENT where SCHEMENO =" + ddlSchemeNo.SelectedValue + "";
        SemeterFilter += " and (DEGREENO =0 OR DEGREENO =" + ddldegree.SelectedValue + ") " + ")"; //Modified by Saket Singh on 25-01-2017
        //SemeterFilter += " and (DEGREENO =0 OR DEGREENO =" + ddldegree.SelectedValue + ") AND (BRANCHNO=0 OR BRANCHNO=" + ddlbranch.SelectedValue + ")" + ")";
        objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", SemeterFilter, "SEMESTERNO");
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SectionFilter = "SECTIONNO>0  and SECTIONNO in (select distinct SECTIONNO from ACD_STUDENT where SEMESTERNO=" + ddlsemester.SelectedValue + "";
        SectionFilter += " and (DEGREENO=0 OR DEGREENO=" + ddldegree.SelectedValue + ") and SCHEMENO=" + ddlSchemeNo.SelectedValue + " and (BRANCHNO=0 OR BRANCHNO=" + ddlbranch.SelectedValue + ")" + ")";
        objCommon.FillDropDownList(ddlsection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", SectionFilter, "SECTIONNO");
    }

    protected void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StudentFilter = "SEMESTERNO =" + ddlsemester.SelectedValue + " and (DEGREENO=0 OR DEGREENO =" + ddldegree.SelectedValue + ") AND (BRANCHNO=0 OR  BRANCHNO=" + ddlbranch.SelectedValue + ")" + "";
        StudentFilter += " AND SCHEMENO=" + ddlSchemeNo.SelectedValue + " and SECTIONNO =" + ddlsection.SelectedValue + "";
        objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT", "IDNO", "STUDNAME", StudentFilter, "STUDNAME");
    }
}
