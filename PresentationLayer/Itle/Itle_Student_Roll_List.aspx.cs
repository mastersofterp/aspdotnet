//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STUDENT ROLL LIST                              
// CREATION DATE : 14-Sep-2009                                                          
// CREATED BY    : MANGESH BARMATE                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Itle_Student_Roll_List : System.Web.UI.Page
{
    Common objCommon = new Common();
    //IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                    //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                    if (Session["ICourseNo"] == null)
                    {
                        Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                    }


                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PopulateDropdown();

                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Itle," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSessionNo.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlSchemeNo.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]); //+",@P_SEMESTERNO="+Convert.ToInt32(ddlSem.SelectedValue);
            //SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSessionNo.SelectedValue != "0" && ddlSchemeNo.SelectedValue != "0")
            {
                ShowReport("Student_RollList_Report", "Itle_Student_Roll_List.rpt");
            }
            else
            {
                //objCommon.DisplayUserMessage(upnlRollList,"Please select session",this.Page);
                MessageBox("Please select session");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void PopulateDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSessionNo, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

           // objCommon.FillDropDownList(ddlSessionNo, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND EXAMTYPE=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "BRANCHNO");

            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.PopulateDropdown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
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
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSchemeNo, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND BRANCHNO=" + ddlBranch.SelectedValue, "SCHEMENO");

    }

    protected void ddlSchemeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_Semester SC ON SR.SEmesterNO = SC.SEmesterNO", "DISTINCT SR.SEMESTERNO", "SC.SEMESTERNAME", "SR.SCHEMENO =" + ddlSchemeNo.SelectedValue, "SR.SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Student_Roll_List.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    protected void ddlSessionNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, " ACD_STUDENT_RESULT A  INNER JOIN ACD_SCHEME S ON (A.SCHEMENO = S.SCHEMENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO)", " DISTINCT B.BRANCHNO", " B.LONGNAME", "B.BRANCHNO>0  AND SESSIONNO =" + ddlSessionNo.SelectedValue + "", "B.BRANCHNO");

    }
}
