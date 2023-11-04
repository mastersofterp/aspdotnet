//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DOCUMENT SUBMISSION REPORT
// CREATION DATE : 18-SEPT-2019
// CREATED BY    : RITA MUNDE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Text;
using System.Data;

public partial class DocumentSubmissionReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objdfc = new StudentController();

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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_DocumentSubmissionReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=DocumentSubmissionReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DocumentSubmissionReport.aspx");
        }
    }

    public void PopulateDropDownList()
    {
        //Fill drop down list of school....
        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        //Fill drop down list of degree....
       // this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME DESC");
        //Fill drop down list of Admission batch....
            
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddlDegree.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        if (ddlSchool.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "DISTINCT (CDB.DEGREENO)", "D.DEGREENAME", "CDB.COLLEGE_ID=" + ddlSchool.SelectedValue, "CDB.DEGREENO");
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC"); 
        }


    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "A.SHORTNAME");
    }


    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        Student dcrReport = GetReportCriteria();
        DataSet ds = objdfc.GetDocumentSubmissionReport(dcrReport);
        if (ds.Tables[0].Rows.Count > 0)
        {

            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment;filename=DocumentSubmissionReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
        }
    }

    private Student GetReportCriteria()
    {
        Student objS = new Student();
        try
        {
            objS.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            objS.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            objS.BatchNo = (ddlBatch.SelectedIndex > 0) ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
            objS.College_ID = (ddlSchool.SelectedIndex > 0) ? Convert.ToInt32(ddlSchool.SelectedValue) : 0;
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Student_ReportUI.GetReportCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return objS;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;

    }
}