//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DEMAND CREATION
// CREATION DATE : 09-JUL-2009
// CREATED BY    : AMIT YADAV
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
public partial class Academic_CreateDemand : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    DemandModificationController objDem = new DemandModificationController();
    AcademinDashboardController objADEController = new AcademinDashboardController();
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

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Load drop down lists
                    //this.objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                    PopulateDropDownListforBulk();
                    //this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
                    //this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    //this.objCommon.FillDropDownList(ddlSelectSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    //this.objCommon.FillDropDownList(ddlForSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    // this.objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO", "PAYTYPENAME", "", "");
                    //add the Load drop down lists degree 24/02/2012
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "degreeno", "DEGREEName", "DEGREENO>0", "DEGREENO"); ;
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO    ");
                    //this.objCommon.FillDropDownList(ddlofSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    ddlSearch.SelectedIndex = 1;
                    ddlSearch_SelectedIndexChanged(sender, e);
                    //PopulateDropDownList();
                }
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
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }

    #endregion

    #region Create Demand

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            //int ExamType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));

        //string Reciept = Convert.ToString(objCommon.LookUp("ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RECIEPT_CODE = " + (ddlReceiptType.SelectedValue.ToString()))); //Added By Nikhil Lambe on 18032020 to get only RECIEPT_CODE
            //if (ExamType == 1)
            //{
            //    demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            //}
            //else
            //{
            //    demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
            //}
            //demandCriteria.SessionNo = Convert.ToInt16(ddlSession.SelectedValue);
           // demandCriteria.ReceiptTypeCode = Reciept.ToString();
            //demandCriteria.ReceiptTypeCode = ddlReceiptType.SelectedValue;
            demandCriteria.BranchNo = (ddlBranch.SelectedIndex > 0 ? Int32.Parse(ddlBranch.SelectedValue) : 0);
            demandCriteria.SemesterNo = (ddlForSemester.SelectedIndex > 0 ? Int32.Parse(ddlForSemester.SelectedValue) : 0);
            demandCriteria.PaymentTypeNo = (ddlPaymentType.SelectedIndex > 0 ? Int32.Parse(ddlPaymentType.SelectedValue) : 0);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.DegreeNo = (ddlDegree.SelectedIndex > 0 ? Int32.Parse(ddlDegree.SelectedValue) : 0);
            //demandCriteria.College_ID = (ddlSchClg.SelectedIndex > 0 ? Int32.Parse(ddlSchClg.SelectedValue) : 0);

            //new 13/09/2022
            if (ddlSchClg.SelectedIndex > 0)
            {
                demandCriteria.SessionNo = Convert.ToInt32(ddlSchClg.SelectedValue.Split('-')[0]);
                demandCriteria.College_ID = Convert.ToInt32(Session["College_id"].ToString());
            }
            else
            {

            }
            //

        }
        catch (Exception ex)
        {
            throw;
        }
        return demandCriteria;
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }
    #endregion

    #region Show Report

    private void ShowReport(FeeDemand demandRpt, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=" + this.GetReportParameters(demandRpt) + (rdoDetailedReport.Checked ? (",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue) : "");
            url += "&param=" + this.GetReportParameters(demandRpt);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetReportParameters(FeeDemand demandRpt)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            // param.Append("COLLEGENAME=" + Session["coll_name"].ToString());

            //param.Append("COLLEGENAME=ARKA JAIN");
            param.Append("UserName=" + Session["userfullname"].ToString());
            param.Append(",@P_COLLEGE_CODE=" + Session["colcode"].ToString());
            param.Append(",@P_SESSIONNO=" + demandRpt.SessionNo);
            param.Append(",@P_RECIEPTCODE=" + demandRpt.ReceiptTypeCode);
            param.Append(",@P_BRANCHNO=" + demandRpt.BranchNo.ToString());
            param.Append(",@P_SEMESTERNO=" + demandRpt.SemesterNo.ToString());
            param.Append(",@P_PAYMENTTYPE=" + demandRpt.PaymentTypeNo.ToString());
            param.Append(",@P_DEGREENO=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedValue : "0"));
            param.Append(",@P_COLLEGE_ID=" + ((ddlSchClg.SelectedIndex > 0) ? ddlSchClg.SelectedValue : "0"));
            param.Append(",ReceiptType=" + ((ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedItem.Text : "0"));
            param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
            param.Append(",Semester=" + ((ddlForSemester.SelectedIndex > 0) ? ddlForSemester.SelectedItem.Text : "0"));
            param.Append(",Session=" + ((ddlSession.SelectedIndex > 0) ? ddlSession.SelectedItem.Text : "0"));

        }
        catch (Exception ex)
        {
            throw;
        }
        return param.ToString();
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            FeeDemand feeDemand = this.GetDemandCriteria();
            string reportTitle = string.Empty;
            string reportFileName = string.Empty;

            if (rdoDetailedReport.Checked)
            {
                reportTitle = "Detailed_Fee_Demand_Report";
                reportFileName = "FeeDemandReport_Detailed.rpt";
            }
            else
            {
                reportTitle = "Fee_Demand_Summary_Report";
                reportFileName = "FeeDemandReport_Summery.rpt";
            }
            ExportExcel(feeDemand, reportTitle);
            //this.ShowReport(feeDemand, reportTitle, reportFileName);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
    private void ExportExcel(FeeDemand feeDemand, string title)
    {
        //FeeDemand feeDemand = this.GetDemandCriteria();

        // string attachment = "attachment; filename=" + title + ".xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/" + "ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);

        //DataSet dsfee = objDem.FeeDemandReport_DetailedExcel(feeDemand);


        //DataGrid dg = new DataGrid();

        //if (dsfee.Tables.Count > 0)
        //{

        //    dg.DataSource = dsfee.Tables[0];
        //    dg.DataBind();
        //}
        //dg.HeaderStyle.Font.Bold = true;
        //dg.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();


        GridView gv = new GridView();
        DataSet ds = objDem.FeeDemandReport_DetailedExcel(feeDemand);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            gv.DataSource = ds;
            gv.DataBind();
            string Attachment = "Attachment; filename=FeeDemandReport.xls";
            ds.Tables[0].TableName = "Detailed_Fee_Demand_Report";
            ds.Tables[1].TableName = "Fee_Demand_Summary_Report";
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=FeeDemandReport.xlsx");
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
            ShowMessage("Record not found.");
        }
    }
    protected void btnCreateDemandForSelStuds_Click(object sender, EventArgs e)
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = this.GetDemandCriteria();
            int selectSemesterNo = Int32.Parse(ddlSelectSemester.SelectedValue);
            string studentIDs = this.GetSelectedStudents();
            char sp = '-';
            string[] collegesession = ddlSchClg.SelectedValue.ToString().Split(sp);
            int SessionNo = Convert.ToInt32(collegesession[0]);
           // int College_Id = Convert.ToInt32(collegesession[1]);


            int College_Id = Convert.ToInt32(Session["College_id"].ToString());


            if (studentIDs.Length == 0)
            {
                ShowMessage("Please select students to create fee demand.");
                return;
            }

            demandCriteria.ReceiptTypeCode = ddlReceiptType.SelectedValue.ToString();
            string AdmBatch = objCommon.LookUp("ACD_STUDENT", "ISNULL(ADMBATCH,0)", "IDNO=" + studentIDs.Split(',')[0]);
            //string rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO=" + Convert.ToInt32(ddlReceiptType.SelectedValue));
            int PaymenttypeNo = (ddlPaymentType.SelectedIndex > 0 ? int.Parse(ddlPaymentType.SelectedValue) : 0);
            string Standardfee = objCommon.LookUp("ACD_STANDARD_FEES", "ISNULL(SUM(SEMESTER" + ddlForSemester.SelectedValue + "),0)", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "  AND COLLEGE_ID=" + College_Id + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + " AND RECIEPT_CODE='" + ddlReceiptType.SelectedValue.ToString() + "' AND BATCHNO=" + Convert.ToInt32(AdmBatch));
            //string response = dmController.CreateDemandForSelectedStudents(studentIDs, demandCriteria, selectSemesterNo, chkOverwrite.Checked, Convert.ToInt32(ddlSchClg.SelectedValue));
            string response = string.Empty;

            if (Convert.ToDouble(Standardfee) > 0)
                response = dmController.CreateDemandForSelectedStudents(studentIDs, demandCriteria, selectSemesterNo, chkOverwrite.Checked, College_Id);
            else
            {
                ShowMessage("Standard Fees is Not Defined.");
                return;
            }
            //if (response != "-99")
            //if (response != "5" || response != "-99")
            if (response == "1")
            {
                if (response.Length > 2)
                    ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                else
                {
                    ShowMessage("Demand sucessfully created for selected students.");
                    BindDemand();
                }
            }
            else if (response == "-99")
                ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
            else if (response == "5")
                ShowMessage("First define the standard fees for selected criteria!");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetSelectedStudents()
    {
        string studentIDs = string.Empty;
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            if ((item.FindControl("chkSelect") as CheckBox).Checked)
            {
                if (studentIDs.Length > 0)
                    studentIDs += ", ";

                studentIDs += (item.FindControl("hidStudentNo") as HiddenField).Value;
            }
        }
        return studentIDs;
    }

    protected void btnShowStudents_Click(object sender, EventArgs e)
    {
        BindDemand();
    }
    private void BindDemand()
    {
        try
        {
            int branchNo = (ddlBranch.SelectedIndex > 0 ? int.Parse(ddlBranch.SelectedValue) : 0);
            int semNo = (ddlSelectSemester.SelectedIndex > 0 ? int.Parse(ddlSelectSemester.SelectedValue) : 0);
            int PaymenttypeNo = (ddlPaymentType.SelectedIndex > 0 ? int.Parse(ddlPaymentType.SelectedValue) : 0);
            string ReceiptType = ddlReceiptType.SelectedValue.ToString();
            char sp = '-';
            

            string college = Convert.ToString(ddlSchClg.SelectedValue.ToString().Split(sp));
            // int College_Idno = (ddlSchClg.SelectedIndex > 0 ? int.Parse(ddlSchClg.SelectedValue) : 0);
            int College_Id = 0;

            AcdAttendanceController acdatt = new AcdAttendanceController();
            DataSet dsCollegeids = acdatt.getselectedcollegewisecollegeid(ddlSchClg.SelectedValue.ToString());

            //DataSet dscollege = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT(COLLEGE_ID)", "", "SESSIONNO IN (" + ViewState["SessionNos"].ToString() + ")", "COLLEGE_ID");
            if (dsCollegeids.Tables.Count > 0)
            {
                if (dsCollegeids.Tables[0].Rows.Count > 0)
                {
                    College_Id = Convert.ToInt32(dsCollegeids.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
                }
                Session["College_id"] = College_Id;
                //dsCollegeids.Tables[0].Columns[1].ToString();

            }

            ViewState["branchNo"] = branchNo;
            ViewState["semNo"] = semNo;
            ViewState["PaymenttypeNo"] = PaymenttypeNo;
            DemandModificationController dmController = new DemandModificationController();
            //string rec_code = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO=" + ddlReceiptType.SelectedValue);
            DataSet ds = dmController.GetStudentsForDemandCreation(Convert.ToInt32(ddlDegree.SelectedValue), branchNo, Convert.ToInt32(ddlSelectSemester.SelectedValue), Convert.ToInt32(ddlForSemester.SelectedValue), PaymenttypeNo, College_Id, ReceiptType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label 
                //CODE ADDED TO  CHECKED CHECKBOX FOR IDNO IF DEMAND IS ALREADY CREATED[02-08-2016]
                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    CheckBox chkb = lvItem.FindControl("chkSelect") as CheckBox;

                    HiddenField hidStudentNo = lvItem.FindControl("hidStudentNo") as HiddenField;

                    //int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1)", "IDNO=" + hidStudentNo.Value));

                    int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D  WITH (NOLOCK) INNER JOIN ACD_RECIEPT_TYPE RT WITH (NOLOCK) ON (D.RECIEPT_CODE=RT.RECIEPT_CODE)", "COUNT(*)", "IDNO=" + hidStudentNo.Value + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlForSemester.SelectedValue) + " AND PAYTYPENO=" + Convert.ToInt32(ddlPaymentType.SelectedValue) + "AND RT.RECIEPT_CODE='" + ddlReceiptType.SelectedValue + "'"));

                    if (IDNO > 0)
                    {
                        chkb.Checked = true;
                        chkb.Enabled = false;
                    }
                    else
                    {
                        chkb.Checked = false;
                        chkb.Enabled = true;
                    }
                }
                //END
                divSelectedStudents.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("No Record Found.", Page);
                divSelectedStudents.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        if (ddlDegree.SelectedIndex > 0)
        {
            // this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME","DEGREENO="+ddlDegree.SelectedValue , "SHORTNAME");
        this.objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH  ACD WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON(ACD.BRANCHNO=B.BRANCHNO) ", "DISTINCT (ACD.BRANCHNO)", "B.LONGNAME", "ACD.DEGREENO>0 AND ACD.DEGREENO=" + ddlDegree.SelectedValue, "ACD.BRANCHNO");
            ddlBranch.Focus();
        }
        else
        {
            objCommon.DisplayMessage("Please Select Degree", Page);
        }

    }
    protected void ddlSchClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSelectedStudents.Visible = false;
        if (ddlSchClg.SelectedIndex > 0)
        {
           // ddlDegree.Items.Clear();
            //this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.DEGREENAME");
        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
            ddlReceiptType.Focus();
            ddlSession.Focus();

            string[] ss = ddlSchClg.SelectedValue.Split('-');

        }
        else
        {
            lvStudents.Visible = false;
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlReceiptType.Items.Clear();
            ddlReceiptType.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSelectSemester.Items.Clear();
            ddlSelectSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlForSemester.Items.Clear();
            ddlForSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSelectedStudents.Visible = false;
        if (ddlBranch.SelectedIndex > 0)
        {
            try
            {
            objCommon.FillDropDownList(ddlForSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT (DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(Session["COLLEGE_ID_NEW"]) + ")", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSelectSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT (DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(Session["COLLEGE_ID_NEW"]) + ")", "SEMESTERNO");
                ddlSelectSemester.Focus();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        else
        {
            ddlForSemester.Items.Clear();
            ddlForSemester.Items.Add(new ListItem("PLease Select", "0"));
            ddlSelectSemester.Items.Clear();
            ddlSelectSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlReceiptType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        divSelectedStudents.Visible = false;
        if (ddlReceiptType.SelectedIndex > 0)
        {

            int College_Id = 0;

            AcdAttendanceController acdatt = new AcdAttendanceController();
            DataSet dsCollegeids = acdatt.getselectedcollegewisecollegeid(ddlSchClg.SelectedValue.ToString());
            
            //DataSet dscollege = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT(COLLEGE_ID)", "", "SESSIONNO IN (" + ViewState["SessionNos"].ToString() + ")", "COLLEGE_ID");
            if (dsCollegeids.Tables.Count > 0)
                {
                if (dsCollegeids.Tables[0].Rows.Count > 0)
                    {
                    College_Id = Convert.ToInt32(dsCollegeids.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
                    Session["COLLEGE_ID_NEW"] = College_Id;
                    }
                }
            this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", " COLLEGE_ID="+College_Id+ "AND A.DEGREENO > 0", "A.DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSelectSemester.Items.Clear();
            ddlSelectSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlForSemester.Items.Clear();
            ddlForSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvStudents.DataSource = null;
        //lvStudents.DataBind();
        if (ddlSession.SelectedIndex > 0)
        {
            ddlReceiptType.Items.Clear();
            this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
            ddlReceiptType.Focus();
        }
        else
        {
            divSelectedStudents.Visible = false;
            ddlReceiptType.Items.Clear();
            ddlReceiptType.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSelectSemester.Items.Clear();
            ddlSelectSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlForSemester.Items.Clear();
            ddlForSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlForSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

        divSelectedStudents.Visible = false;
        if (ddlForSemester.SelectedIndex > 0)
        {
            ddlPaymentType.Items.Clear();
            this.objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "PAYTYPENO");
            ddlPaymentType.Focus();
        }
        else
        {
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        }

    }
    protected void ddlSelectSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSelectedStudents.Visible = false;
        if (ddlSelectSemester.SelectedIndex > 0)
        {
            ddlForSemester.Focus();
        }
        else
        {
            //ddlPaymentType.Items.Clear();
            //ddlPaymentType.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSelectedStudents.Visible = false;
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = string.Empty;
            pnlLV.Visible = false;
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

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divSingleStudDetail.Visible = false;
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
            ViewState["value"] = value;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        //ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        divDemandCreation.Visible = false;
        btnCreateDemand.Visible = false;
       
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
            //txtInstitute.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            //txtInstitute.ToolTip = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            txtDegree.Text = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            txtBranch.Text = ds.Tables[0].Rows[0]["SHORTNAME"].ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            this.objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
            //this.objCommon.FillDropDownList(ddlsessionno, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(txtInstitute.ToolTip), "SESSIONNO DESC");
            //this.objCommon.FillDropDownList(ddlsessionno, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(txtInstitute.ToolTip) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //divSingleStudDetail.Visible = true;  
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            divSingleStudDetail.Visible = false;
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            divDemandCreation.Visible = true;
            btnCreateDemand.Visible = true;
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            string value = string.Empty;

            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;
            FeeCollectionController feeController = new FeeCollectionController();
            // int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
            if (idno > 0)
            {                
                divSingleStudDetail.Visible = true;
                pnlLV.Visible = false;
                lvStudent.Visible = false;
                divDemandCreation.Visible = true;
                StudentDetails();
            }
            else
            {
                //ShowMessage("No student found with given enrollment number.");
                objCommon.DisplayMessage(this.UpdatePanel1, "No student found with given enrollment number.", this.Page);
                divSingleStudDetail.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlofSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.objCommon.FillDropDownList(ddlForSemesterN, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
    }
    protected void btnCreateDemand_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlForSemesterN.SelectedValue != "0")
            {
                if (ddlRecType.SelectedValue != "0")
                {
//
                    DemandModificationController dmController = new DemandModificationController();
                    FeeDemand demandCriteria = this.GetDemandCriteriaforSingleStudent();
                    int selectSemesterNo = Int32.Parse(txtSemesterName.ToolTip);
                    string studentIDs = Convert.ToString(Session["stuinfoidno"]);
                    string ReceiptType = ddlReceiptType.SelectedValue.ToString();
                    int College_Id = Convert.ToInt32(objCommon.LookUp("acd_student", "COLLEGE_ID", "idno=" + Session["stuinfoidno"]));
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "count(idno)", "SESSIONNO=" + demandCriteria.SessionNo + " and DEGREENO = " + demandCriteria.DegreeNo + " and BRANCHNO= " + demandCriteria.BranchNo + "and PAYTYPENO=" + demandCriteria.PaymentTypeNo + " and IDNO= " + Session["stuinfoidno"] + " and SEMESTERNO=" + demandCriteria.SemesterNo));
                    
                    if (count > 0 && CheckBox1.Checked == false)
                    {
                        ShowMessage("Demand is already created for the selected student. Do you want to overwrite the deamand then check the given checkbox");
                        return;
                    }
                    string response = dmController.CreateDemandForSelectedStudents(studentIDs, demandCriteria, selectSemesterNo, CheckBox1.Checked, College_Id);
                    if (response == "1")
                    {
                        if (response.Length > 2)
                            //ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                           objCommon.DisplayMessage(this.UpdatePanel1,"Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.", this.Page);

                            //objCommon.DisplayMessage(this.UpdatePanel1, "Please select semester for demand creation!", this.Page);
                        else
                        {
                           // ShowMessage("Demand sucessfully created for selected students.");
                            objCommon.DisplayMessage(this.UpdatePanel1, "Demand sucessfully created for selected students", this.Page);
                            divSingleStudDetail.Visible = false;
                            pnlLV.Visible = true;
                            lvStudent.Visible = true;
                            divDemandCreation.Visible = false;
                            int branchNo = Convert.ToInt32(ViewState["branchNo"]);
                            int semNo = Convert.ToInt32(ViewState["semNo"]);
                            int PaymenttypeNo = Convert.ToInt32(ViewState["PaymenttypeNo"]);
                            int DEGREENO = Convert.ToInt32(ViewState["DEGREENO"]);
                            DataSet ds = dmController.GetStudentsForDemandCreation(DEGREENO, branchNo, Convert.ToInt32(ddlSelectSemester.SelectedValue), Convert.ToInt32(ddlForSemester.SelectedValue), PaymenttypeNo, College_Id, ReceiptType);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                lvStudents.Visible = true;
                                lvStudents.DataSource = ds;
                                lvStudents.DataBind();
                            }
                        }
                    }
                    else if (response == "-99")
                        //ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
                        objCommon.DisplayMessage(this.UpdatePanel1, "There is an error while creating demands. Please retry and overwrite existing demands while retrying.", this.Page);
                    else if (response == "5")
                        //ShowMessage("First define the standard fees for selected criteria!");
                    objCommon.DisplayMessage(this.UpdatePanel1, "First define the standard fees for selected criteria!", this.Page);
                }
                else
                {
                    //ShowMessage("Please select receipt type for demand creation!");
                    objCommon.DisplayMessage(this.UpdatePanel1, "Please select receipt type for demand creation!", this.Page);
                }
            }
            else
            {
                //ShowMessage("Please select semester for demand creation!");
               objCommon.DisplayMessage(this.UpdatePanel1, "Please select semester for demand creation!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }


    private void PopulateDropDownListforBulk()
    {
        try
        {
            DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
            ddlSchClg.Items.Clear();
            ddlSchClg.Items.Add("Please Select");
            ddlSchClg.SelectedItem.Value = "0";

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSchClg.DataSource = ds;
                //ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSchClg.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSchClg.DataTextField = ds.Tables[0].Columns[4].ToString();

                ddlSchClg.DataBind();
                ddlSchClg.SelectedIndex = 0;
            }

            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC"); //ISNULL(FLOCK,0)=1
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void StudentDetails()
    {
        StudentController objSC = new StudentController();
        DataSet ds = objCommon.FillDropDown(@"ACD_STUDENT A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.COLLEGE_ID=B.COLLEGE_ID) 
INNER JOIN ACD_SESSION_MASTER C ON (A.COLLEGE_ID=C.COLLEGE_ID) 
INNER JOIN ACD_SEMESTER D ON (A.SEMESTERNO=D.SEMESTERNO) 
INNER JOIN ACD_DEGREE E ON (A.DEGREENO=E.DEGREENO) 
INNER JOIN ACD_BRANCH F ON (A.BRANCHNO=F.BRANCHNO) 
INNER JOIN ACD_PAYMENTTYPE G ON (A.PTYPE=G.PAYTYPENO)",
" DISTINCT A.IDNO", " A.STUDNAME,A.SEMESTERNO, D.SEMESTERNAME, A.DEGREENO,E.DEGREENAME,A.BRANCHNO,F.LONGNAME ,A.COLLEGE_ID,B.COLLEGE_NAME,ADMBATCH , A.PTYPE, PAYTYPENAME,ISNULL(A.ADMCAN,0) ADMCAN",
"idno=" + Session["stuinfoidno"] + "", string.Empty);

        if (ds.Tables[0].Rows.Count > 0)
        {
            divSingleStudDetail.Visible = true;
            objCommon.FillDropDownList(ddlForSemesterN, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 ", "SEMESTERNO");
            txtDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtDegree.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["DEGREENO"] = txtDegree.ToolTip;
            txtBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtBranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            txtStudFullname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtSemesterName.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
            txtSemesterName.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            txtCollege.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            txtPaytype.Text = ds.Tables[0].Rows[0]["PAYTYPENAME"].ToString();
            txtPaytype.ToolTip = ds.Tables[0].Rows[0]["PTYPE"].ToString();
            txtCollege.ToolTip = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "FLOCK=1 and COLLEGE_ID=" + txtCollege.ToolTip));
            this.objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
            string Receiptcode = "TF";
            //int status = Convert.ToInt32(objCommon.LookUp("acd_demand", "count(idno)", "idno=" + Session["stuinfoidno"] + "and degreeno=" + txtDegree.ToolTip + " and branchno=" + txtBranch.ToolTip + " and semesterno=" + txtSemesterName.ToolTip + "and PAYTYPENO=" + txtPaytype.ToolTip + " and RECIEPT_CODE='" + Receiptcode + "'"));
            //if (status > 0)
            //{
            
            
            //    lblStatus.Text = "Created";
            //    lblStatus.ForeColor = System.Drawing.Color.Green;
            //    lblStatus.Font.Bold = true;
            //}
            //else
            //{
            //    lblStatus.Text = "Pending";
            //    lblStatus.ForeColor = System.Drawing.Color.Red;
            //    lblStatus.Font.Bold = true;
            //}

            int admStatus = (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ADMCAN"].ToString())) ? Convert.ToInt16(ds.Tables[0].Rows[0]["ADMCAN"].ToString()) : 0;
            if (admStatus > 0)
            {
                lblAdmStatus.Text = "CANCELLED";
                lblAdmStatus.Font.Bold = true;

            }
            else
            {
                lblAdmStatus.Text =  "ADMITTED";
                lblAdmStatus.Font.Bold = true;
            }
            lblAdmStatus.ForeColor = (admStatus > 0) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            btnCreateDemand.Visible = (admStatus > 0) ? false : true;
        }
    }

    private FeeDemand GetDemandCriteriaforSingleStudent()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            string Reciept = Convert.ToString(objCommon.LookUp("ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RCPTTYPENO = " + Convert.ToInt32(ddlRecType.SelectedValue)));

            demandCriteria.ReceiptTypeCode = Reciept.ToString();
            demandCriteria.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "COLLEGE_ID=" + txtCollege.ToolTip));
            ViewState["Sessionnoforsinglestudent"] = demandCriteria.SessionNo;
            demandCriteria.BranchNo = Convert.ToInt32(txtBranch.ToolTip);
            demandCriteria.SemesterNo = Convert.ToInt32(ddlForSemesterN.SelectedValue);
            demandCriteria.PaymentTypeNo = Convert.ToInt32(txtPaytype.ToolTip);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.DegreeNo = Convert.ToInt32(txtDegree.ToolTip);
            demandCriteria.College_ID = Convert.ToInt32(txtCollege.ToolTip);

        }
        catch (Exception ex)
        {
            throw;
        }
        return demandCriteria;
    }

    //amitk
    protected void ddlSchClgNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void ddlRecType_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlForSemesterN.SelectedIndex > 0 && ddlRecType.SelectedIndex>0)
            {
            string Receiptcode = "TF";
            int status = Convert.ToInt32(objCommon.LookUp("acd_demand", "count(idno)", "idno=" + Session["stuinfoidno"] + "and degreeno=" + txtDegree.ToolTip + " and branchno=" + txtBranch.ToolTip + " and semesterno=" + ddlForSemesterN.SelectedValue + "and PAYTYPENO=" + txtPaytype.ToolTip + " and RECIEPT_CODE='" + Receiptcode + "'"));
            if (status > 0)
                {
                lblStatus.Text = "CREATED";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Font.Bold = true;
                }
            else
                {
                lblStatus.Text = "PENDING";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Font.Bold = true;
                }
            }
        else
            {
            ddlForSemesterN.SelectedIndex = 0;            
            }
        }
    protected void ddlForSemesterN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRecType.SelectedIndex = 0;
            lblStatus.Text = "";
        }
}
