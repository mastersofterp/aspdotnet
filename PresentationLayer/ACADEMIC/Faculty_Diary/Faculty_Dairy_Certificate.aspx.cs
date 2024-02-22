//=================================================================================
// PROJECT NAME  : RFC Common Code                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : Faculty_Dairy_Certificate.aspx.cs                                          
// CREATION DATE : 06/01/2024                                                
// CREATED BY    : Vipul Tichakule                             
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================



using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Faculty_Dairy_Certificate : System.Web.UI.Page
{
      Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Session objSession = new Session();
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();

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
                BindFacultyData();

            }
        }
    }

    protected void BindFacultyData()
    {
        DataSet ds = objTeachingPlanController.BindFacultyDairyData(Session["userno"].ToString());
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblNameNew.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
            lblMobileNew.Text = ds.Tables[0].Rows[0]["UA_MOBILE"].ToString();
            lblDeptName.Text = ds.Tables[0].Rows[0]["DEPTNAME"].ToString();
            lblClgName.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
        }
        else
        {
            objCommon.DisplayMessage(this.updFaculty, "Record not found", this.Page);  
        }
       //dr.Close();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Faculty_Dairy_Certificate", "Faculty_Dairy_Certificate1.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + clgcode + ",@P_UANO=" + Session["userno"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updFaculty, this.updFaculty.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}