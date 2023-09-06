using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;

using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;

public partial class ACADEMIC_Condonation_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    ConfigController Confi = new ConfigController();
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
            }
            //objCommon.FillListBox(lstSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
            PopulateDropDownList();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void PopulateDropDownList()
    {
        try
        {
            AcademinDashboardController objADEController = new AcademinDashboardController();
            DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
            lstSession.Items.Clear();        
            lstSession.ClearSelection();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lstSession.DataSource = ds;
                lstSession.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstSession.DataTextField = ds.Tables[0].Columns[4].ToString();
                lstSession.DataBind();
                lstSession.ClearSelection();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Excel_Click(object sender, EventArgs e)
    {
        try
        {
            string session = "";
            foreach (ListItem item in lstSession.Items)
            {
                if (item.Selected == true)
                {
                    session += item.Value + '$';
                }
               
            }
            if (!string.IsNullOrEmpty(session))
            {
                session = session.Substring(0, session.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdReport, "Please Select Session!", this.Page);
                return;
            }
            string SP_Name2 = "PKG_ACD_GET_STUDENTS_FOR_CONDOLANCE_APPROVAL_EXCEL";
            string SP_Parameters2 = "@P_SESSIONNO";
            string Call_Values2 = "" + session + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = dsStudList;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=CondonationStatusList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.UpdReport, "Record Not Found!", this.Page);
                return;
            }
        }

        catch(Exception)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lstSession.ClearSelection();
    }
    protected void BtnPdf_Click(object sender, EventArgs e)
    {
        ShowReport("Student Condonation Report", "StudentCondonationBulk.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string session = "";
            foreach (ListItem item in lstSession.Items)
            {
                if (item.Selected == true)
                {
                    session += item.Value + '$';
                }

            }
            if (!string.IsNullOrEmpty(session))
            {
                session = session.Substring(0, session.Length - 1);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdReport, "Please Select Session!", this.Page);
                return;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ""

                   + ",@P_SESSIONNO=" + session;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdReport, this.UpdReport.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}