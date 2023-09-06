using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Data;
public partial class ACADEMIC_REPORTS_RegistrationReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CertificateMasterController objcmsc = new CertificateMasterController();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


                    //fill dropdown method
                    objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CertificateMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CertificateMaster.aspx");
        }
    }



    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + ddlAdmBatch.SelectedValue + ",@P_REGSTATUS=" + ddlRegStatus.SelectedValue + "";
           
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_RegistrationReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("RegistrationReport", "rptRegistrationReport.rpt");
    }


      protected void btnExcel_Click(object sender, EventArgs e)
        {
          Export("xls", "rptRegistrationReport.rpt");
            //try
            //{
            //    GridView GVReport = new GridView();
            //    string ContentType = string.Empty;
            //    DataSet ds = objcmsc.getstudentinfo(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlRegStatus.SelectedValue));
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ////ds.Tables[0].Columns.RemoveAt(3);
            //        ds.Tables[0].Columns.Remove("IDNO");
            //        ds.Tables[0].Columns.Remove("REGNO");
            //        ds.Tables[0].Columns.Remove("COLLEGE_ID");
            //        ds.Tables[0].Columns.Remove("DEGREENO");
            //        ds.Tables[0].Columns.Remove("BRANCHNO");
            //        ds.Tables[0].Columns.Remove("FATHERNAME");
            //        ds.Tables[0].Columns.Remove("MOTHERNAME");
            //        ds.Tables[0].Columns.Remove("DOB");
            //        ds.Tables[0].Columns.Remove("REGSTATUS");
            //        ds.Tables[0].Columns.Remove("MIGRATIONSTATUS");

            //        //BoundField bf = new BoundField();
            //        //bf.DataField = "IDNO";
            //        //bf.SortExpression = "IDNO";
            //        //bf.HeaderText = "IDNO";

            //        //GVDayWiseAtt.Columns.Add(bf);

            //        GVReport.DataSource = ds;
            //        GVReport.DataBind();
                   
                    

            //        string attachment = "attachment; filename=abc.xls";
            //        Response.ClearContent();
            //        Response.AddHeader("content-disposition", attachment);
            //        Response.ContentType = "application/vnd.MS-excel";
            //        StringWriter sw = new StringWriter();
            //        HtmlTextWriter htw = new HtmlTextWriter(sw);
            //        GVReport.RenderControl(htw);
            //        Response.Write(sw.ToString());
            //        Response.End();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (Convert.ToBoolean(Session["error"]) == true)
            //        objUCommon.ShowError(Page, "Academic_RegistrationReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //    else
            //        objUCommon.ShowError(Page, "Server Unavailable.");
            //}
       }


      private void Export(string exporttype, string rptFileName)
      {
          try
          {
              string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
              url += "Reports/CommonReport.aspx?";
              url += "exporttype=" + exporttype;
              url += "&filename=" + "Registration Report";
              url += "&path=~,Reports,Academic," + rptFileName;
              url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCHNO=" + ddlAdmBatch.SelectedValue + ",@P_REGSTATUS=" + ddlRegStatus.SelectedValue + "";
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
              sb.Append(@"window.open('" + url + "','','" + features + "');");

              ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
          }
          catch (Exception ex)
          {
              if (Convert.ToBoolean(Session["error"]) == true)
                  objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.Export() --> " + ex.Message + " " + ex.StackTrace);
              else
                  objUCommon.ShowError(Page, "Server Unavailable.");
          }
      }
      protected void btnCancel_Click(object sender, EventArgs e)
      {
          Response.Redirect(Request.Url.ToString());
      }
}