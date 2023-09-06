//======================================================================================
// PROJECT NAME  : UAIMS [GEC]                                                          
// MODULE NAME   : EXAMINATION                                                             
// PAGE NAME     : COLLATORS     
// CREATION DATE : 11-FEB-2013                                                         
// CREATED BY    : ASHISH DHAKATE                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_REPORTS_Collators : System.Web.UI.Page
{
    Common objCommon = new Common();
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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                objCommon.FillDropDownList(ddlCheckers1, "ACD_CHECKERS", "CHECKERNO", "CHECKERNAME", "CHECKERNO > 0", "CHECKERNO");
                objCommon.FillDropDownList(ddlCheckers2, "ACD_CHECKERS", "CHECKERNO", "CHECKERNAME", "CHECKERNO > 0", "CHECKERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
                

            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnReport2_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student objStud = new Student();

            objStud.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStud.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objStud.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objStud.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            objStud.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objStud.CheckerNo1 = Convert.ToInt32(ddlCheckers1.SelectedValue);
            objStud.CheckerNo2 = Convert.ToInt32(ddlCheckers2.SelectedValue);
            objStud.CheckerName1 = ddlCheckers1.SelectedItem.Text.ToString();
            objStud.CheckerName2 = ddlCheckers2.SelectedItem.Text.ToString();
            objStud.CollatorNo1 = Convert.ToInt32(ddlCollators1.SelectedValue);
            objStud.CollatorNo2 = Convert.ToInt32(ddlCollators2.SelectedValue);
            objStud.CollegeCode = Session["colcode"].ToString();

            string check = "-100";
            check = objCommon.LookUp("ACD_CHECKERS_DETAILS", "CHECKERDETAILNO", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SEMESTERNO="+Convert.ToInt32(ddlSemester.SelectedValue )+" AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            //int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SCHEMETYPE=" + ddlScheme.SelectedValue));
            //objStud.SchemeNo = schemeno;

                if (check == "")
                {
                    //Add Checkers Detail
                    //int appeared = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT A LEFT OUTER JOIN ACD_COPYCASE B ON(A.SESSIONNO = B.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.IDNO = B.IDNO)", "COUNT(1)APPEARED", "A.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
                    //int passed = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT A LEFT OUTER JOIN ACD_COPYCASE B ON(A.SESSIONNO = B.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.IDNO = B.IDNO)", "COUNT(1)PASS", "A.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.RESULT = 'P' AND A.PASSFAIL = 'PASS' AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));

                    //if (appeared > 0)
                    //{
                        int checkersno = objSC.AddCheckersDetail(objStud);
                        if (checkersno != -99)
                        {
                            BindListView();
                            ShowReport("REPORT", "rptCollatorsReport.rpt");
                        }
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage(this.UpdatePanel1, "Result Cannot Process Please process First!!", this.Page);
                    //}
                      
                }
                 else
                    objCommon.DisplayMessage(this.UpdatePanel1, "Already Exists!!", this.Page);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
       
        try
        {
            string  schemetype = objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)+" AND SCHEMENO="+ddlScheme.SelectedValue);
            int appeared= Convert.ToInt32( objCommon.LookUp("ACD_TRRESULT A LEFT OUTER JOIN ACD_COPYCASE B ON(A.SESSIONNO = B.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.IDNO = B.IDNO)","COUNT(1)APPEARED","A.SESSIONNO ="+ Convert.ToInt32(ddlSession.SelectedValue)+" AND A.SCHEMENO="+ Convert.ToInt32(ddlScheme.SelectedValue)+" AND A.SEMESTERNO="+Convert.ToInt32(ddlSemester.SelectedValue)));
            int passed = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT A LEFT OUTER JOIN ACD_COPYCASE B ON(A.SESSIONNO = B.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND A.IDNO = B.IDNO)", "COUNT(1)PASS", "A.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.RESULT = 'P' AND A.PASSFAIL = 'PASS' AND A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));

            if (schemetype =="1")
            {
                schemetype = "CSVTU";
            }
            else 
            {
                schemetype = "NIT";
            }
            
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_APPEARED=" + appeared + ",@P_PASSED=" + passed + ",@P_SCHEMETYPE= "+schemetype;

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";

                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME","DEGREENO="+ ddlDegree.SelectedValue +" AND BRANCHNO=" + ddlBranch.SelectedValue,"SCHEMENO");
        ddlSemester.SelectedIndex = 0;
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {      
       
    }

    private void BindListView()
    {
        try
        {
            //int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND SCHEMETYPE=" + ddlScheme.SelectedValue));
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetChekers(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlScheme.SelectedValue));
            lvChecker.DataSource = ds;
            lvChecker.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_schememaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int deptno = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH", "DEPTNO", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue));
        objCommon.FillDropDownList(ddlCollators1, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 AND UA_DEPTNO=" + deptno, "UA_NO");
        objCommon.FillDropDownList(ddlCollators2, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 AND UA_DEPTNO=" + deptno, "UA_NO");
        BindListView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
