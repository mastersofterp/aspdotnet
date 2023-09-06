//======================================================================================
// PROJECT NAME   : NITGOA                                                 
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : LATE FEES REPORT
// CREATION DATE  : 22-JAN-2014                                                          
// CREATED BY     : UMESH GANORKAR                                                   
// MODIFIED DATE  :                                                                      
// MODIFIED DESC  :                                                                      
//======================================================================================

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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;
using System.IO;

public partial class ACADEMIC_Bulk_Receipt : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeCntrl = new FeeCollectionController();

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    To Set the MasterPage
    //    if (Session["masterpage"] != null)
    //        objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
    //    else
    //        objCommon.SetMasterPage(Page, "");
    //}

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                objCommon.FillDropDownList(ddlSchClg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
                objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO>0", "RCPTTYPENO");
                objCommon.FillDropDownList(ddlCounter, "ACD_COUNTER_REF", "COUNTERNO", "PRINTNAME", "COUNTERNO > 0", "PRINTNAME ASC");
                objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6") // For RCPIT and RCPIPER
                {
                    btnReportExcel.Visible = true;
                }
                else
                {
                    divTotal.Visible = true;
                    Panel1.Visible = true;
                    btnShow.Visible = true;
                }
            }
        }

        //if (Session["userno"] == null || Session["username"] == null ||
        //       Session["usertype"] == null || Session["userfullname"] == null)
        //{
        //    Response.Redirect("~/default.aspx");
        //}

        //divMsg.InnerHtml = string.Empty;
    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=Bulk_Receipt.aspx");
    //        }
    //    }
    //    else
    //    {
    //        Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=Bulk_Receipt.aspx");
    //    }
    //}

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += "$";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    //GenerateQrCode(studentIds);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }

    private string GetStudentDcrNos()
    {
        string studentDcrnos = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentDcrnos.Length > 0)
                        studentDcrnos += "$";
                    studentDcrnos += (item.FindControl("hidDcrno") as HiddenField).Value.Trim();               
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentDcrnos;
    }

    protected void BindListView()
    {
        try
        {
            StudentController studCont = new StudentController();
            DataSet ds;
            string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
            DateTime FromDate = txtFromDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = txtToDate.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);

            ds = studCont.GetStudentForBulkRecieptEntry(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlSchClg.SelectedValue), Convert.ToInt32(ddlReceiptstatus.SelectedValue), rectype, Convert.ToInt32(ddlBranch.SelectedValue), ToDate, FromDate);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label -
                hftot.Value = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSchClg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            if (ddlSchClg.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.DEGREENAME");
                ddlDegree.Focus();
            }
            else
            {
                //ShowMessage("Please select college/school");
                ddlSchClg.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ddlSchClg_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        if (ddlDegree.SelectedIndex > 0)
        {
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
            this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON a.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "A.SHORTNAME");
            ddlBranch.Focus();
        }
        else
        {
           // ShowMessage("Please select degree");
            ddlDegree.Focus();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string ids = GetStudentIDs();
        string DcrNos = GetStudentDcrNos();

        if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6") // For RCPIT and RCPIPER
        {
            ShowReport("REPORT", "BulkFeesReceipt.rpt");

            //ShowReportMIT("Fee Reciept", "FeeCollectionReceiptForCash_MIT_FEECOLL_BULK_RECIEPT.rpt", ids, DcrNos);
        }
        else
        {
            btnShow.Visible = true;
            divTotal.Visible = true;
            Panel1.Visible = true;
            ShowReportMIT("Fee Reciept", "FeeCollectionReceiptForCash_MIT_FEECOLL_BULK_RECIEPT.rpt", ids, DcrNos);
        }
       

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string paymodeCode;
            if (rbRegEx.SelectedValue == "1")
            {
                paymodeCode = "O";
            }
            else
            {
                paymodeCode = "";
            }
            string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_RECEIPT_STATUS=" + Convert.ToInt32(ddlReceiptstatus.SelectedValue) + ",@P_RECEIPT_TYPE=" + rectype + ",@P_COUNTER=" + Convert.ToInt32(ddlCounter.SelectedValue) + ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text ;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_DEGREE=" + ddlDegree.SelectedValue + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_RECEIPT_STATUS=" + Convert.ToInt32(ddlReceiptstatus.SelectedValue) + ",@P_RECEIPT_TYPE=" + rectype + ",@P_COUNTER=" + Convert.ToInt32(ddlCounter.SelectedValue) + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + ",username=" + Session["username"].ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_ACADEMIC_YEAR_ID=" + Convert.ToInt32(ddlAcdYear.SelectedValue) + ",@P_PAYMODECODE=" + paymodeCode ;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

      private void ShowReportMIT(string reportTitle, string rptFileName , string Ids ,string DcrNos)
        {
        try
            {
            //int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_IDNO=" + Ids + ",@P_DCRNO=" + DcrNos + ",@P_UA_NAME=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);
            }
        catch (Exception ex)
            {
            throw;
            }
        }
    protected void btnReportExcel_Click(object sender, EventArgs e)
    {
        this.ExportinExcelforCancelRecieptReport();
    }

    private void ExportinExcelforCancelRecieptReport()
    {
         //string rectype = this.GetRecType();

        //if (string.IsNullOrEmpty(rectype))
        //{
        //    objCommon.DisplayUserMessage(this.Page, "Please Select At least One Receipt Type !", this.Page);
        //    return;
        //}

       // rectype = rectype.Substring(0, rectype.Length - 1);

        string  reccode = ddlRecType.SelectedValue;
        int semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno   = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno   = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
        DateTime ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;
       
        //int year = Convert.ToInt32(ddlYear.SelectedValue);
        //int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        DataSet dsfeestud = feeCntrl.GetBulkCancelRecieptExcelReport(reccode, semesterNo, FromDate, ToDate, degreeno, branchno, Convert.ToInt32(ddlAcdYear.SelectedValue));

        if (dsfeestud != null && dsfeestud.Tables.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "Cancel_Reciept_Report";   
         
            //dsfeestud.Tables[1].TableName = "Balance Report";

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
                Response.AddHeader("content-disposition", "attachment;filename= CurrentStudent & SchoolDetailsFeeReport.xlsx");
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
            objCommon.DisplayUserMessage( this.Page, "No Record Found", this.Page);
            return;
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        divTotal.Visible = true;
        Panel1.Visible = true;
        this.BindListView();
    }
    protected void rbRegEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbRegEx.SelectedValue == "0")
        {
            divCounter.Visible = true;
        }
        else if (rbRegEx.SelectedValue == "1")
        {
            divCounter.Visible = false;
        }
    }
}
