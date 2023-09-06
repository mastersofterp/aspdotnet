// PROJECT NAME  : RAIPUR                                                       
// MODULE NAME   : Exam Topper List                                       
// CREATION DATE : 07/09/2012                                                    
// CREATED BY    : ASHISH DHAKATE                                
// MODIFIED BY   :                                                     
// MODIFIED DESC :        
//=================================================================================





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
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_REPORTS_StudentTopperList : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DataSet dsShowData = null;


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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                Populatedropdownlist();
                ddlSession.Focus();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentTopperList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentTopperList.aspx");
        }
    }



    private void Populatedropdownlist()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlAdmBatch,"ACD_ADMBATCH","BATCHNO","BATCHNAME","BATCHNO>0","BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResultList.Populatedropdownlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


      
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO=" + ddlDegree.SelectedValue, "BRANCHNO");
            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));         
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));           
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0 AND DEGREENO=" + ddlDegree.SelectedValue + " AND  BRANCHNO=" + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
           
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_STUDENT_RESULT B ON (A.SEMESTERNO = B.SEMESTERNO)", "DISTINCT B.SEMESTERNO", "A.SEMESTERNAME", "B.SEMESTERNO > 0 AND B.SESSIONNO=" + ddlSession.SelectedValue + " AND B.SCHEMENO=" + ddlScheme.SelectedValue + " AND PREV_STATUS=0", "B.SEMESTERNO");
            ddlSem.Focus();
            
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
           
        }
    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if(Convert.ToInt32(rdoPurpose.SelectedValue)==0)
            {

            }
            else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 1)
            {
 
            }
            else if (Convert.ToInt32(rdoPurpose.SelectedValue) == 2)
            {
                ShowReport(rdoReportType.SelectedValue, "rptTopperList.rpt");
            }

        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_REPORT_STUDENTTOPPERLIST.btnReport_Click->" +  ex.Message + " " + ex.Message);
            }
            else
            {
                objCommon.ShowError(Page, "Server UnAvailable");

            }
        }
    }

    //Method for Show Report 
    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue +",@P_ORDER_BY=" +ddlMeritList.SelectedValue+",@P_MERIT="+ddlMeritList.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updGradeAllotment, this.updGradeAllotment.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void rdoReportType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
