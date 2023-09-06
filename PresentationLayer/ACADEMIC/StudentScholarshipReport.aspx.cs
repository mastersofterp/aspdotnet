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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_REPORTS_StudentScholarshipReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeCntrl = new FeeCollectionController();
    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                PopulateDropDown();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            //divMsg.InnerHtml = string.Empty;
        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
        trSchool.Visible = false;
        //trAdmbatch.Visible = true;
        trDegree.Visible = false;
        trBranch.Visible = false;
        trSemester.Visible = false;
        trReport.Visible = false;
        //btnReport.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentScholarshipReport.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        this.ExportinExcelforStudentScholarship();
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    private void ExportinExcelforStudentScholarship()
    {
        string attachment = "attachment; filename=" + "StudentScholershipReport.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //DataSet dsfee = feeCntrl.Get_Student_Scholership_Report(Convert.ToInt32(ddlAdmBatch.SelectedValue));   // Amit 2020 Feb 15  0 is session temporary
        DataSet dsfee = feeCntrl.Get_Student_Scholership_Report(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));   //added by saurabh  
        DataGrid dg = new DataGrid();

        if (dsfee.Tables.Count > 0)
        {
            dsfee.Tables[0].Columns.Remove("IDNO");
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnSummaryReport_Click(object sender, EventArgs e)
    {


        DataSet dsfee = feeCntrl.Get_Student_Scholership_Summary_Report(Convert.ToInt32(ddlAdmBatch.SelectedValue));   // Amit 2020 Feb 15  0 is session temporary

        DataGrid dg = new DataGrid();

        if (dsfee.Tables.Count > 0)
        {
            //dsfee.Tables[0].Columns.Remove("IDNO");
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
            string attachment = "attachment; filename=" + "SummaryCountOfStudents.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
    //protected void btnSummaryReport_Click(object sender, EventArgs e)
    //{

    //    //int version = 2;
    //    //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //    // url += "Reports/CommonReport.aspx?";
    //    // url += "exporttype=xls";
    //    //url += "&filename=ScholarshipSummeryReport.xls";
    //    // url += "&path=~,Reports,Academic,rptScholerShip.rpt";
    //    // url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) ;
    //    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //    // string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //    //sb.Append(@"window.open('" + url + "','','" + features + "');");

    //    // ScriptManager.RegisterClientScriptBlock(this.updProg, this.updProg.GetType(), "controlJSScript", sb.ToString(), true);




       


    //    string tab = "\t";
    //    DataSet dsfee = feeCntrl.Get_Student_Scholership_Summary_Report(Convert.ToInt32(ddlAdmBatch.SelectedValue));   // Amit 2020 Feb 15  0 is session temporary
    //    DataTable dt = new DataTable();
    //    GridView dg = new GridView();
    //    GridView ds = new GridView();

    //    if (dsfee.Tables.Count > 0)
    //    {

    //        dt = dsfee.Tables[0];
    //        dg.DataSource = dt;
    //        dg.DataBind();
           

    //        ds.DataSource = dsfee.Tables[0];
    //        ds.DataBind();

    //        GridViewRow GR = new GridViewRow(0, 1, DataControlRowType.Footer, DataControlRowState.Insert);

    //        TableCell FooterCell = new TableCell();
    //        FooterCell.Text = "TOTAL ";
    //        GR.Cells.Add(FooterCell);
    //        ds.Controls[0].Controls.AddAt(2, GR);

    //        string attachment = "attachment; filename=" + "SummaryCountOfStudents.xls";
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", attachment);
    //        Response.ContentType = "application/" + "vnd.MS-excel";
    //        dg.FooterRow.Cells[1].Text = "Total";
    //        dg.FooterRow.Cells[1].Visible = true;
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        dg.HeaderStyle.Font.Bold = true;
    //        dg.RenderControl(htw);
    //        ds.RenderControl(htw);
           
    //        Response.Write(sw.ToString());
    //       // HttpContext.Current.Response.Write("Total:" + tab + "  " + dg.FooterRow.Cells[2].Text);
    //        Response.End();
    //        ///////////////////////////////////////////////////////////////////////////////
    //    }
    //}

    }
