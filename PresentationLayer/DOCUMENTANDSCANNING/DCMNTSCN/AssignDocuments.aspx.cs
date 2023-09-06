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
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
public partial class DOCUMENTANDSCANNING_DCMNTSCN_AssignDocuments : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DocumentController objC = new DocumentController();
    //ConnectionStrings
    private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
             //   else
                   // lblHelp.Text = "No Help Added";

                objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0", "SUBDEPT");
            }
        }
       
        divMsg.InnerHtml = string.Empty;
    }

    private void BindListView(int usertype)
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDESIG D ON (E.SUBDESIGNO = D.SUBDESIGNO)", "E.IDNO, E.PFILENO", "ISNULL(E.TITLE,'')+''+ISNULL(E.FNAME,'')+''+ISNULL(E.MNAME,'')+''+ISNULL(E.LNAME,'')  AS NAME, D.SUBDESIG", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue), "E.IDNO");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvUsers.DataSource = ds;
                    lvUsers.DataBind();
                    pnlListMain.Visible = true;        
                }
                else
                {
                    lvUsers.DataSource = null;
                    lvUsers.DataBind();
                    pnlListMain.Visible = false;      
                }
            }
            else
            {
                lvUsers.DataSource = null;
                lvUsers.DataBind();
                pnlListMain.Visible = false;    
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DOCUMENTANDSCANNING_DCMNTSCN_AssignDocuments.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    protected void btnShow_Click(object sender, EventArgs e)
    {
        //BindListView(Convert.ToInt32(ddlDept.SelectedValue));
        //ViewState["EditUserId"] = ddlDept.SelectedValue;

        ShowReport("AssignedDocuments", "rptAssignDocuments.rpt");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lvUsers.DataSource = null;
        lvUsers.DataBind();
        pnlListMain.Visible = false;
        ddlDept.SelectedIndex = 0;
     
        ViewState["EditUserId"] = null;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createexamname.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createexamname.aspx");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Employee Category", "rptAssignDocuments.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENTANDSCANNING_DCMNTSCN_AssignDocuments.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("DOCUMENTANDSCANNING")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,DOCUMENTANDSCANNING," + rptFileName;
            url += "&param=@P_DEPTNO=" + ddlDept.SelectedValue + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENTANDSCANNING_DCMNTSCN_AssignDocuments.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}