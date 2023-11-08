using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Data;


public partial class ACADEMIC_Academic_SummeryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                if (Session["userno"] == null || Session["username"] == null ||
                      Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    // this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    ViewState["ReportType"] = null;
                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
                    //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONID DESC"); // ADDED BY Nehal N. ON DATED 30.06.2023
                    
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO desc");
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 03/01/2022
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Export()
    {
        ViewState["sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue));

        string attachment = "attachment; filename=" + "Student_Strength_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = objSC.GetStudentStrengthDetails(Convert.ToInt32(ViewState["sessionno"].ToString()), ViewState["ReportType"].ToString(), Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }


    protected void btnShowReports_Click(object sender, EventArgs e)
    {
        try
        {
            string ReportType = string.Empty;
            foreach (ListItem item in chkReportType.Items)
            {
                if (item.Selected == true)
                {
                    ReportType = ReportType + item.Text.ToString() + ",";
                }
            }

            if (ReportType != "")
            {
                if (ReportType.Substring(ReportType.Length - 1) == ",")
                {
                    ReportType = ReportType.Substring(0, ReportType.Length - 1);

                }
            }
            else
            {
                dvGrid.Visible = false;
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Atleast One Report Type!", this.Page);
                btnShowReports.Visible = false;
                lstDetails.DataSource = null;
                lstDetails.DataBind();
                return;
            }
            ViewState["ReportType"] = ReportType + " ";
            dvGrid.Visible = true;
            Export();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ViewState["sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue)+" AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue));

        string ReportType = string.Empty;
        foreach (ListItem item in chkReportType.Items)
        {
            if (item.Selected == true)
            {
                ReportType = ReportType + item.Text.ToString() + ",";
            }
        }
        if (ReportType != "")
        {
            if (ReportType.Substring(ReportType.Length - 1) == ",")
            {
                ReportType = ReportType.Substring(0, ReportType.Length - 1);

            }
        }
        else
        {
            dvGrid.Visible = false;
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Atleast One Report Type!", this.Page);
            btnShowReports.Visible = false;
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            return;
        }
        ViewState["ReportType"] = ReportType + " ";

        DataSet ds = objSC.GetStudentStrengthDetails(Convert.ToInt32(ViewState["sessionno"].ToString()), ViewState["ReportType"].ToString(), Convert.ToInt32(ddlAcdYear.SelectedValue));

        if (ds.Tables.Count > 0 && ds.Tables != null)
        {
            // panell.Visible = true;
            btnShowReports.Visible = true;
            lstDetails.DataSource = ds;
            lstDetails.DataBind();
            dvGrid.Visible = true;
        }
        else
        {
            //  panell.Visible = false;
            btnShowReports.Visible = false;
            lstDetails.DataSource = null;
            lstDetails.DataBind();
            dvGrid.Visible = false;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCollege.SelectedIndex > 0)
        //{
        //    this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
        //    ddlSession.Focus();
        //}
        //else
        //{
        //    ddlSession.Items.Clear();
        //    ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //}
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_SESSION_MASTER SM ON (C.COLLEGE_ID = SM.COLLEGE_ID)", "C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND SM.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "C.COLLEGE_ID");
            
            //this.objCommon.FillDropDownList(ddlCollege, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            ddlCollege.Focus();
        }
        else
        {
            ddlCollege.Items.Clear();
            ddlCollege.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}