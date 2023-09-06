//======================================================================================
// PROJECT NAME   : NITRAIPUR                                                        
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : PHD STUDENT LIST ALLOT SUPERVISOR
// CREATION DATE  : 17-DEC-2012                                                          
// CREATED BY     : ASHISH DHAKATE                                                   
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class Academic_REPORTS_PhdStudentList : System.Web.UI.Page
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

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO = 6", "DEGREENO DESC");
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR < 5", "YEAR");
            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnReport1_Click(object sender, EventArgs e)
    {
        ShowReport("REPORT", "rptPhdStudentList.rpt"); 
    }
   


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_YEAR=" + ddlYear.SelectedValue + ",@P_BRANCHNO="+ddlBranch.SelectedValue+",@P_YEARNAME=" + ddlYear.SelectedItem.Text + ",@P_DEGREENAME=" + ddlDegree.SelectedItem.Text;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlYear.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
        if (deptno > 0)
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + deptno, "BRANCHNO");
            }
        }
        else
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            }
        }
    }
    protected void btnEnrollReport_Click(object sender, EventArgs e)
    {
        int check = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(*)", "DEGREENO=" + ddlDegree.SelectedValue + " AND ENROLLNO IS NOT NULL AND ENROLLNO!='' AND YEAR=" + ddlYear.SelectedValue));
        if (check > 0)
        {
            ShowReport("REPORT", "rptPhdStudentEnrollList.rpt");
        }
        else
        {
            objCommon.DisplayMessage("Enrollment No. Not Generated, Please Generate the Enrollment No. ", this.Page);
        }
    }
}
