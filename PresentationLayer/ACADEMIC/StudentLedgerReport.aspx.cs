//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT LEDGER REPORT
// CREATION DATE : 24-JUN-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_StudentLedgerReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    #region Page Events

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
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    string College_code = objCommon.LookUp("REFF", "College_code", "OrganizationId = '" + Session["OrgId"].ToString() + "'");
                    ViewState["college_id"] = College_code;

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
                    ddlSearch.SelectedIndex = 0;

                }
            }
            divMsg.InnerHtml = "";
          
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
                Response.Redirect("~/notauthorized.aspx?page=StudentLedgerReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentLedgerReport.aspx");
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        StudentController studCont = new StudentController();
        string searchText = txtSearchText.Text.Trim();
        if (searchText != string.Empty)
        {
            string searchBy = (rdoEnrollmentNo.Checked ? "enrollmentno" : (rdoStudentName.Checked ? "name" : "idno"));
            DataSet ds = studCont.RetrieveStudentDetails(searchText, searchBy); ;
            if (ds != null && ds.Tables.Count > 0)
            {
                lvStudentRecords.DataSource = ds.Tables[0];
                lvStudentRecords.DataBind();
            }
        }
        else
            ShowMessage("Please enter text to search.");

    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        string reportTitle = "Student_Ledger_Report";
        string rptFileName = "StudentLedgerReport.rpt";
        try
        {

            //int dcrcount = 0;
            //dcrcount = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(DCR_NO)", "IDNO=" + Convert.ToInt16(ViewState["StudentId"]) + " and SEMESTERNO=" + Convert.ToInt16(ViewState["SEMESTERNO"])));
            //if (dcrcount > 0)
            //{
                this.ShowReport(reportTitle, rptFileName);
            //}
            //else
            //{

            //    objCommon.DisplayMessage(this.updFee, "No Record Found.", this.Page);
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport( string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle; 
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + ViewState["StudentId"]
                + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",UserName=" + Session["userfullname"].ToString();// +",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            
            
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //protected void btnShowInfo_Click(object sender, EventArgs e)
    //{
    //    int studentId = feeController.GetStudentIdByEnrollmentNoforLedgerreport(txtEnrollNo.Text.Trim());
    //    ViewState["idno"] = studentId;
    //    int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studentId + ""));
    //    ViewState["SEMESTERNO"] = semester;
    //    DataSet ds = feeController.GetStudentInfoByIdforLedgerReport(studentId);
    //     if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //     {
    //         DataRow dr = ds.Tables[0].Rows[0];
    //         // show student information
    //         this.PopulateStudentInfoSection(dr);
    //         btnShowReport.Visible = true;
    //         btnCancel.Visible = true;
    //     }
    //     else objCommon.DisplayUserMessage(updFee, "No student found with given enrollment number.", this.Page);
    //}
    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            #region Bind data to labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            lblAdmStatus.Text = dr["ADMISSIONSTATUS"].ToString();
            #endregion

            #region Show Student's Data Selected in DDLs
           
            ViewState["StudentId"] = dr["IDNO"].ToString();
            ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["YearNo"] = dr["YEAR"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();
            //ddlSemester.SelectedValue;
            ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
            #endregion
            divStudInfo.Style["display"] = "block";
                 }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSearchCriteria_Click(object sender, EventArgs e)
    {
            divStudInfo.Visible = false;
            divButton.Visible = false;
            lblNoRecords.Visible = true;
            string value = string.Empty;
            if (ddlDropdown.SelectedIndex > 0)
            {
                value = ddlDropdown.SelectedValue;
            }
            else
            {
                value = txtSearch.Text;
            }

            bindlist(ddlSearch.SelectedItem.Text, value);
            ddlDropdown.ClearSelection();
            txtSearch.Text = string.Empty;
      
        
    }

    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void btnCloseCriteria_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //pnlLV.Visible = false;
            divStudInfo.Visible = false;
            divButton.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;
                        rfvDDL.Enabled = true;
                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        rfvSearchtring.Enabled = true;
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;
                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        divStudInfo.Visible = true;
        divButton.Visible = true;
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        //Response.Redirect(url + "&id=" + lnk.CommandArgument);
        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        //objSR.IDNO = idno;
        ViewState["idno"] = Session["stuinfoidno"].ToString();
        int Organizationid = Convert.ToInt32(Session["OrgId"]);

        //int studentId = feeController.GetStudentIdByEnrollmentNoforLedgerreport(txtEnrollNo.Text.Trim());
        int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + lblenrollno.Text.Trim() + "'"));


        ViewState["idno"] = studentId;
        int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno + ""));
        ViewState["SEMESTERNO"] = semester;

        DataSet ds = feeController.GetStudentInfoByIdforLedgerReport(idno);
 
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            // show student information
            this.PopulateStudentInfoSection(dr);
            btnShowReport.Visible = true;
            if (Session["OrgId"].ToString() == "3" || Session["OrgId"].ToString() == "4")// For CPUK and CPUH
            {
                btnShowReportFormat2.Visible = true;
            }
            else
            {
                btnShowReportFormat2.Visible = false;
            }          
            btnCancel.Visible = true;
            pnlLV.Visible = false;

        }
        else objCommon.DisplayUserMessage(updFee, "No student found with given enrollment number.", this.Page);
    }

    protected void btnShowReportFormat2_Click(object sender, EventArgs e)
    {
        string reportTitle = "Student_Ledger_Report_Format-II";
        string rptFileName = "StudentLedgerReportCreditDebit.rpt";
        try
        {
            this.ShowReportFormatII(reportTitle, rptFileName);
           
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportFormatII(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + ViewState["StudentId"];// +",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);


            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}