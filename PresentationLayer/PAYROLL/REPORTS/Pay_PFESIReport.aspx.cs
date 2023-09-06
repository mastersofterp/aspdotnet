using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Data.SqlClient;

public partial class PAYROLL_REPORTS_Pay_PFESIReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
   

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
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                 PopulateDropDownList();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_PaySlip.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PaySlip.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {

            //FILL MONTH YEAR 

            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");
           
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

            //FILL STAFF
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

            //FILL EMPLOYEE
           // objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

            objCommon.FillDropDownList(ddlEmployeeType, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowPFESIReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitleForEmployeePaySlip=" + reportTitle;
            //url += "&pathForEmployeePaySlip=~,Reports,Payroll," + rptFileName + "&@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + "&@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + "&@P_COLLEGE_CODE=" + 33;

            //url += "&paramForEmployeePaySlip=username=" + Session["username"].ToString() + ",Bank_Name=" + txtBankName.Text.ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;

            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_TABNAME=" + (ddlMonthYear.SelectedItem.Text) + ",@P_STAFF_NO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_COLL_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_EMPTYPENO=" + Convert.ToInt32(ddlEmployeeType.SelectedValue) + ",@P_SCHEME_NAME=" + ddlStaffNo.SelectedItem.Text + ",@EMP_TYPE=" + ddlEmployeeType.SelectedItem.Text + ",@COLLEGE_NAME=" + ddlCollege.SelectedItem.Text + ",@P_COLLEGE_CODE=33";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PFESIReport.ShowPFESIReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnESIReport_Click(object sender, EventArgs e)
    {
        ShowPFESIReport("Employee ESI Report", "Monthly_ESIC.rpt");
    }

    //protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

    //}
    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlEmployeeNo, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 AND STAFFNO=" + ddlStaffNo.SelectedValue + " AND COLLEGE_NO=" + ddlCollege.SelectedValue, "FNAME");

    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlCollege.SelectedIndex = 0;
            ddlStaffNo.SelectedIndex = 0;
            ddlEmployeeType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.btnCancel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnESICExcelReport_Click(object sender, EventArgs e)
    {
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.Text;
        int staffno = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int EmployeeTypeNo=0;
        //int bankno = Convert.ToInt32(ddlBank.SelectedValue);


        DataSet ds = ESIReportwithExcel(monyear, staffno, collegeNo, EmployeeTypeNo);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string Collegeaddress = objCommon.LookUp("reff with (nolock)", "College_address", string.Empty);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");


            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell = new TableCell();e
            //HeaderCell = new TableCell();
            //HeaderCell.Text = collename;
            //HeaderCell.ColumnSpan = 5;
            //HeaderCell.Font.Bold = true;
            //HeaderCell.Font.Size = 16;
            //HeaderCell.Attributes.Add("style", "text-align:center !important;");
            //HeaderGridRow.Cells.Add(HeaderCell);
            //GVDayWiseAtt.Controls[0].Controls.AddAt(0, HeaderGridRow);


            //GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell2 = new TableCell();
            //HeaderCell2 = new TableCell();
            //HeaderCell2.Text = Collegeaddress;
            //HeaderCell2.ColumnSpan = 5;
            //HeaderCell2.Font.Bold = true;
            //HeaderCell2.Font.Size = 16;
            //HeaderCell2.Attributes.Add("style", "text-align:center !important;");
            //HeaderGridRow2.Cells.Add(HeaderCell2);
            //GVDayWiseAtt.Controls[0].Controls.AddAt(1, HeaderGridRow2);


            //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell1 = new TableCell();
            //HeaderCell1.Text = "  BANK STATEMEMENT  " + Month + "    " + Year + "    ";
            //HeaderCell1.ColumnSpan = 5;
            //HeaderCell1.Font.Bold = true;
            //HeaderCell1.Font.Size = 14;
            //HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            //HeaderGridRow1.Cells.Add(HeaderCell1);
            //GVDayWiseAtt.Controls[0].Controls.AddAt(2, HeaderGridRow1);


            string attachment = "attachment; filename=ESIReportwithExcel.xls";
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
    public DataSet ESIReportwithExcel(string MonthYear, int collegeNo, int staffNo, int EmployeeTypeNo)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", staffNo);
            objParams[2] = new SqlParameter("@P_EMPTYPENO", EmployeeTypeNo);
            objParams[3] = new SqlParameter("@P_COLL_NO", collegeNo);
            //objParams[4] = new SqlParameter("@P_IDNOS", idnos);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ESIC_REPORT_EXCEL", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }

}