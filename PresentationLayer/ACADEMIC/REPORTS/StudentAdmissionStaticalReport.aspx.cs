using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using System.IO;
using System.Data;

public partial class ACADEMIC_StudentAdmissionStaticalReport : System.Web.UI.Page
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
                }
                //Fill DropDown List
                PopulateDropDownList();

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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLocalAddressLabel.aspx");
        }
    }

    //Fill DropdownList
    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN COLLEGE
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            //Fill Dropdown Degree
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "DEGREENAME");
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "BRANCHNO>0", "SHORTNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentStrength.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedValue == "0")
        {
            //Fill Dropdown Degree
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "DEGREENAME");
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB WITH (NOLOCK) ON CDB.DEGREENO=A.DEGREENO", "DISTINCT A.DEGREENO", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + "AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.degreeno");
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            ddlDegree.Focus();
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "LONGNAME");
        ddlBranch.Focus();
    }
    protected void btnSumrSessn_Click(object sender, EventArgs e)
    {
        string check = objCommon.LookUp("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT R WITH (NOLOCK) ON (S.IDNO=R.IDNO)", "COUNT(DISTINCT S.IDNO)", "PREV_STATUS=0 AND EXAM_REGISTERED=1 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "  OR " + Convert.ToInt32(ddlBranch.SelectedValue) + " =0)");
        if (check == "0")
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
        else
            ShowReport("StudentStrength", "StudentStrengthDetailsCount.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PREV_STATUS=" + ddlStudType.SelectedValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSumrySem_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport2("Admission_Register_Report", "StudentAdmisionBatchRegMFCountReport.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }


    }

    private void ShowReport2(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=username=" + Session["username"].ToString() + ",From_Date=" + (txtFromDate.Text.Trim()) + ",To_Date=" + (txtToDate.Text.Trim()) + ",@P_IDNO=0" + ",@P_FROM_DATE=" + txtFromDate.Text.Trim() + ",@P_TO_DATE=" + txtToDate.Text.Trim() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",admbatch=" + ddlAdmbatch.SelectedItem.Text.Trim();

            url += "&param=@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "";


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSumrcollege_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport3("Student Strength By College", "StudentStrengthDetailsByCollege.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void ShowReport3(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGEID=" + (Convert.ToInt32(ddlCollege.SelectedValue)) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_PREV_STATUS=" + ddlStudType.SelectedValue;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlUser, this.updpnlUser.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Cancel();
    }

    private void Cancel()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlStudType.SelectedIndex = 0;
        ddlAdmbatch.SelectedIndex = 0;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue != "0")
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "SM.SEMESTERNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue, "SM.SEMESTERNAME");
        }
        else
        {
            ddlSemester.SelectedIndex = 0;

        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlBranch.SelectedValue != "0")
        //    {
        //        objCommon.FillDropDownList(ddlAdmbatch, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_ADMBATCH SM WITH (NOLOCK) ON(S.ADMBATCH=SM.BATCHNO)", " DISTINCT S.ADMBATCH", "SM.BATCHNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue + " AND S.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SM.BATCHNAME");
        //    }
        //    else
        //    {
        //        ddlAdmbatch.SelectedIndex = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}
    }

    protected void btnSummaryrpt_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.GetAllAdmissionSummeryforExcel(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AdmissionSummaryReport.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage("No record found!", this.Page);
        }
    }
}