//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT ADMISSION REGISTER
// CREATION DATE : 12-oCT-2009
// CREATED BY    : MANGESH BARMATE
// ADDED BY      : ASHISH DHAKATE
// ADDED DATE    : 19 DEC 2011
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using ClosedXML.Excel;

public partial class ACADEMIC_StudentHorizontalReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    #region Page Event

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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (Convert.ToInt32(Session["OrgId"]) == 2)
                    {
                        divd.Visible = true;
                        divadmrounf.Visible = false;
                        btnBranchcount.Visible = false;
                    }
                    else
                    {
                        divd.Visible = false;
                        divadmrounf.Visible = true;
                        //btnBranchcount.Visible = true;
                    }

                    this.PopulateDropDownList();

                    if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                        btnMothFathNotAlive.Visible = true;
                        btnTotalApplifeereport.Visible = true;
                        btnAdmissionRegReport.Visible = true;
                        btnAdmissionRegStuDataExcelReport.Visible = true;
                        rfcvacdyear.Visible = true;
                        btnBranchcount.Visible = true;
                    }   
               
                    //txtToDate.Text = DateTime.Now.ToShortDateString();
                    //Focus on From Date
                    //txtFromDate.Focus();
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header 
            }

            //Blank Div
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
        }
    }

    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_COLLEGEID=" + Convert.ToInt32(ddlClg.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_ADMBATCHNO=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",admbatch=" + ddlAdmbatch.SelectedItem.Text.Trim();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport2(string reportTitle, string rptFileName)
    {
        try
        {
            int degree = 0, branch = 0;
            if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
            {
                degree = Convert.ToInt32(ddlDegree.SelectedValue);
                branch = Convert.ToInt32(ddlBranch.SelectedValue);
            }
            else if (ddlDegree.SelectedIndex > 0)
            {
                degree = Convert.ToInt32(ddlDegree.SelectedValue);
            }
            else
            {
                degree = 0;
                branch = 0;
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            // url += "&param=@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text + "";
            url += "&param=@P_COLLEGE_CODE=" + ddlClg.SelectedValue + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_ID=" + ddlClg.SelectedValue +
             ",@P_DEGREENO=" + degree + ",@P_BRANCHNO=" + branch + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",username=" + Session["username"].ToString() +
             ",@P_DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + ",@P_ACADEMIC_YEAR_ID=" + Convert.ToInt32(ddlAcdYear.SelectedValue) +
             ",@P_SORTBY=" + Convert.ToInt32(ddlAdm.SelectedValue);
            //url += "&param=@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue + ",@P_COLLEGE_ID=" + ddlClg.SelectedValue + ",@P_DEGREENO=" + degree + ",@P_BRANCHNO=" + branch + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",username=" + Session["username"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Admission_Register", "Stud_Admission_Register.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN DEGREE
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ISNULL(ACTIVESTATUS,0) = 1 ", "DEGREENO");
            //ddlBranch.Items.Insert(0, "Please Select");
            objCommon.FillDropDownList(ddlAdmRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ISNULL(ACTIVESTATUS,0) = 1 ", "ROUNDNAME");

            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
            // FILL DROPDOWN COLLEGE
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            }

            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ISNULL(ACTIVESTATUS,0) = 1", "YEAR");
            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FILL DROPDOWN BATCH
        //  objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");'

        if (Session["usertype"].ToString() != "1")
        {
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND (B.DEPTNO =" + Session["userdeptno"].ToString() + " OR " + Session["userdeptno"].ToString() + " = 0) AND B.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue), "A.LONGNAME");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND (B.DEPTNO IN(SELECT VALUE FROM dbo.Split('"+  Session["userdeptno"].ToString()+"',',')) OR"+"'0' IN(SELECT VALUE FROM dbo.Split('"+ Session["userdeptno"].ToString()+"',','))) AND B.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue), "A.LONGNAME");
            //(B.DEPTNO IN(SELECT VALUE FROM dbo.Split( Session["userdeptno"].ToString()))or '0' in(SELECT VALUE FROM dbo.Split( Session["userdeptno"].ToString())))
            ddlBranch.Focus();
            //ddlAdmbatch.SelectedIndex = 0;
        }
        else
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
    }

    protected void btnRport1_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Admission_Register", "Stud_Admission_RegisterLegal.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.btnReport1_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //report
    protected void btnRegReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport2("Admission_Register_Report", "Stud_Admission_Register_new.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.btnReport1_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void btnShowCount_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = null;
    //    if (Convert.ToInt16(ddlDegree.SelectedValue) > 0)
    //    {
    //        ds = objCommon.FillDropDown("ACD_STUDENT S ", "COUNT(*) AS STUDCOUNT", "SUM(CASE WHEN S.SEX = 'M' THEN 1 ELSE 0 END)TOT_M,SUM(CASE WHEN SEX = 'F'THEN 1 ELSE 0 END)TOT_F", "S.IDNO IN (SELECT IDNO FROM ACD_DEMAND WHERE IDNO=S.IDNO AND ADMBATCH= S.ADMBATCH) AND S.ADMCAN=0 AND S.CAN <>1 AND ADMBATCH=" + ddlAdmbatch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " OR " + ddlBranch.SelectedValue + " =0 AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND ADMDATE BETWEEN  (CONVERT(DATETIME,'" + txtFromDate.Text + "',103)) AND (CONVERT(DATETIME,'" + txtToDate.Text + "',103))", "");
    //    }
    //    else
    //    {
    //        ds = objCommon.FillDropDown("ACD_STUDENT", "COUNT(*) AS STUDCOUNT", "SUM(CASE WHEN SEX = 'M' THEN 1 ELSE 0 END)TOT_M,SUM(CASE WHEN SEX = 'F'THEN 1 ELSE 0 END)TOT_F", "S.ADMCAN=0 ANDADMBATCH=" + ddlAdmbatch.SelectedValue + " ", "");
    //    }
    //    lvStudentCount.DataSource = ds;
    //    lvStudentCount.DataBind();
    //}

    //btn for excel


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet ds = null;

            int degreeno = 0, branchno = 0;
            if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
            else if (ddlDegree.SelectedIndex > 0)
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            }
            else
            {
                degreeno = 0;
                branchno = 0;
            }

            DataSet dsfee = null;

            if (Convert.ToInt32(Session["OrgId"]) == 2) 
            {             
                dsfee = objCommon.GetAdmRegisteredCountForEXCEL_Crescent(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]));
            }
            else
            {
                dsfee = objCommon.GetAdmRegisteredCountForEXCEL(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]),Convert.ToInt32(ddlAcdYear.SelectedValue));
            }
            //DataSet dsfee = objCommon.GetAdmRegisteredCountForEXCEL(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]));

            // Added By Vipul Tichakule on date 14-02-2024
            if (dsfee != null && dsfee.Tables[0].Rows.Count > 0)
            {
                dsfee.Tables[0].TableName = "AdmissionRegisterStudents";
                dsfee.Tables[1].TableName = "BranchWiseData ";
        
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsfee.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= AdmissionRegisterStudents_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_UserMeritList.btnexport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnBranchcount_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet ds = null;

            int degreeno = 0, branchno = 0;
            if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
            else if (ddlDegree.SelectedIndex > 0)
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            }
            else
            {
                degreeno = 0;
                branchno = 0;
            }

            DataSet dsfee = objCommon.GetAdmRegisteredCountForEXCEL(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlAdmRound.SelectedValue),Convert.ToInt32(ddlAcdYear.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlClg.SelectedValue),Convert.ToInt32(ddlSemester.SelectedValue));

            //DataSet dsfee = objCommon.GetAdmRegisteredCountForEXCEL(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]));

            if (dsfee != null && dsfee.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsfee;
                GV.DataBind();
                string attachment = "attachment; filename=AdmissionRegisterStudents_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();



            }
            else
            {
                objCommon.DisplayMessage("No record found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_UserMeritList.btnexport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnstudcount_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet ds = null;

            int degreeno = 0, branchno = 0;
            if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
                branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
            else if (ddlDegree.SelectedIndex > 0)
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            }
            else
            {
                degreeno = 0;
                branchno = 0;
            }

            DataSet dsfee = objCommon.GetAdmCountForEXCEL(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmRound.SelectedValue));

            //DataSet dsfee = objCommon.GetAdmRegisteredCountForEXCEL(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]));

            if (dsfee != null && dsfee.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsfee;
                GV.DataBind();
                string attachment = "attachment; filename=AdmissionRegisterStudents_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();



            }
            else
            {
                objCommon.DisplayMessage("No record found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_UserMeritList.btnexport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue != "0")
        {
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "SM.SEMESTERNAME", "S.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue) + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue + " AND S.ADMBATCH = " + ddlAdmbatch.SelectedValue, "SM.SEMESTERNAME");
            objCommon.FillDropDownList(ddlSemester, "  ACD_SEMESTER ", "SEMESTERNO", "SEMESTERNAME", "ACTIVESTATUS=1", "SEMESTERNAME");
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }
    }

    protected void ddlClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            //string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
            if (divd.Visible == true)
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "B.DEPTNO");
                // COLLEGE_ID IN(" + Session["college_nos"] + ")
            }
        }
        else
        {
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlClg.SelectedValue, "B.DEPTNO");
        }

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Session["usertype"].ToString() != "1")
        //{

        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO =" + Convert.ToInt32(ddlDepartment.SelectedValue), "D.DEGREENO");

        //}
        //else
        //{
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlClg.SelectedValue, "D.DEGREENO");
        //}

    }
    protected void btnAdmissionBatchWiseReport_Click(object sender, EventArgs e)
    {

        GridView GV = new GridView();
        DataSet dsStudData = null;

        dsStudData = objCommon.Get_Admission_Batch_Wise_Student_Data(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (dsStudData != null && dsStudData.Tables.Count > 0 && dsStudData.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = dsStudData;
            GV.DataBind();
            string attachment = "attachment; filename=AdmissionRegisterStudents_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }
    }

    protected void btnMothFathNotAlive_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
        {
            GridView GV = new GridView();
            DataSet ds = null;
            ds = objCommon.Get_Mother_Father_Alive_Excel_Report(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string attachment = "attachment; filename=IsMotherFatherAlive_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("No record found!", this.Page);
            }
        }
        
    }

    private void ShowTotalApplicableFeeReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",@ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcdYear.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnTotalApplifeereport_Click(object sender, EventArgs e)
    {
        ShowTotalApplicableFeeReport("Total_Fee_Applicable_Report", "TotalFeeApplicableReport.rpt");
    }
 
    protected void btnAdmissionRegReport_Click(object sender, EventArgs e)
    {
        this.ExportinExcelforStudentAdmissionRegisterDetails();
    }

    private void ExportinExcelforStudentAdmissionRegisterDetails()
    {
        FeeCollectionController feeCntrl = new FeeCollectionController();

        int Collegeid = Convert.ToInt32(ddlClg.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);       
        int Year =  Convert.ToInt32(ddlYear.SelectedValue);
        int semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int Academicyear = Convert.ToInt32(ddlAcdYear.SelectedValue);

        DataSet dsfeestud = feeCntrl.Get_Student_Admission_Register_Adademic_Report_Excel(Collegeid, degreeno, branchno, Academicyear, Year, semesterNo);

        if (dsfeestud != null && dsfeestud.Tables.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "AdmissionRegisterStudents";         
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= AdmissionRegisterStudents_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
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
            objCommon.DisplayUserMessage(this.Page, "No Record Found", this.Page);
            return;
        }

    }

    //Added  On Dated 22-11-2023 as per T-No : - 50465

    private void ExcelReportAdmissionRegisterStudent()
    {
        FeeCollectionController feeCntrl = new FeeCollectionController();

        int batchname = Convert.ToInt32(ddlAdmbatch.SelectedValue);
        int collegeid = Convert.ToInt32(ddlClg.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        int Year = Convert.ToInt32(ddlYear.SelectedValue);
        int semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int Academicyear = Convert.ToInt32(ddlAcdYear.SelectedValue);

        DataSet dsadmstud = feeCntrl.Get_Student_Admission_Register_Adademic_Report_Excel_Format_II(batchname, collegeid, degreeno, branchno, Year, semesterNo, Academicyear);

        if (dsadmstud != null && dsadmstud.Tables[0].Rows.Count > 0)
        {
            dsadmstud.Tables[0].TableName = "AdmissionRegStudent(Format II)";
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsadmstud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AdmissionRegStudent(Format II)_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");

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
            objCommon.DisplayUserMessage(this.Page, "No Record Found", this.Page);
            return;
        }

    }
    protected void btnAdmissionRegStuDataExcelReport_Click(object sender, EventArgs e)
    {
        ExcelReportAdmissionRegisterStudent();
    }
}