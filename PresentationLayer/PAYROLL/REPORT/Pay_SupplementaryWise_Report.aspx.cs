//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL Supplementary Wise Report                       
// CREATION DATE : 20-September-2018                                                          
// CREATED BY    : ROHIT MASKE                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic;
using System.IO;

public partial class PAYROLL_REPORT_Pay_SupplementaryWise_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //ConnectionStrings
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
                    int usertype = Convert.ToInt32(Session["usertype"]);
                                   
                   
                    if (usertype == 3 || usertype == 5)
                    {
                        ddlDept.Enabled = true;
                        int deptno = Convert.ToInt32(objCommon.LookUp("User_acc", "UA_DEPTNO", "UA_TYPE=" + usertype + ""));                       
                        objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO="+deptno+"", "SUBDEPTNO ASC");
                    }
                    else
                    {
                        ddlDept.Enabled = true;
                        objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPTNO ASC");
                    }
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
                //Populate DropdownList
                PopulateDropDownList();
              //  FillListBoxStaff();

            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Abstract_Salary.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?Abstract_Salary.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Abstract_Salary.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //FILL STAFF
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaffNo, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            int college_code = Convert.ToInt32(objCommon.LookUp("reff", "College_code",""));
            objCommon.FillDropDownList(ddlSuppliHead, "PAYROLL_SUPPLIMENTARY_HEAD", "SUPLHNO", "SUPLHEAD", "COLLEGE_CODE IN(" + college_code + ")", "SUPLHNO");
            objCommon.FillDropDownList(ddlMonthYear, "PAYROLL_SALFILE", "distinct(convert(datetime,monyear,103)) as mon", "MONYEAR", "SALNO>0", "convert(datetime,monyear,103) DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_SupplementryWise.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSuppWiseRpt_Click(object sender, EventArgs e)
    {
        ShowSupplementaryReport("Employee Supplementary Report", "Pay_SupplementaryBillwiseReport.rpt");
    }
    private void ShowSupplementaryReport(string reportTitle, string rptFileName)
    {
        try
        {
            string str = string.Empty;
            if (ddlOrderBy.Items != null || ddlOrderBy.Items.Count != 0)
            {
                str = ddlOrderBy.Text;
            }
            string monthyear = ddlMonthYear.SelectedItem.Text;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SUPPLIHEADNO=" + Convert.ToInt32(ddlSuppliHead.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + ",@P_ORDERNO=" + str + ",@P_DEPARTMENTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_SBDATE=" + monthyear;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentAttendanceReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSuppliHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlOrderBy, "PAYROLL_SUPPLIMENTARY_BILL", " DISTINCT ORDNO as ORDERNUMBER", "ORDNO", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND SUPLHNO=" + Convert.ToInt32(ddlSuppliHead.SelectedValue) + " ", "ORDNO DESC");       
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        string colname = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
        string ContentType = string.Empty;
        string monyear = ddlMonthYear.SelectedItem.ToString();
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int StaffNo = Convert.ToInt32(ddlStaffNo.SelectedValue);
        int IDNO = 0;
        DataSet ds = EmployeeSupplementaryReport_exporttoexcel(monyear, StaffNo, CollegeNo, IDNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string collename = objCommon.LookUp("reff with (nolock)", "collegename", string.Empty);
            string StaffName = objCommon.LookUp("PAYROLL_STAFF", "STAFF", "STAFFNO=" + StaffNo);
            string Month = objCommon.LookUp(monyear, "(CAST( DATENAME(month, MON) AS nvarchar(50) ))", "MON='" + monyear + "'");
            string Year = objCommon.LookUp(monyear, "cast (YEAR( MON) AS nvarchar(50 )) ", "MON='" + monyear + "'");
            GridView GVEmpChallan = new GridView();
            //ds.Tables[0].Columns.RemoveAt(3);
            GVEmpChallan.DataSource = ds;
            GVEmpChallan.DataBind();


            // Header Row 1
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = collename;
            HeaderCell.ColumnSpan = 68;
            HeaderCell.BackColor = System.Drawing.Color.Yellow;
            HeaderCell.ForeColor = System.Drawing.Color.Red;
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 16;
            HeaderCell.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow.Cells.Add(HeaderCell);
            GVEmpChallan.Controls[0].Controls.AddAt(0, HeaderGridRow);


            // Header Row 2
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = StaffName + "  SUPPLEMENTARY BILL FOR THE  " + Month + "    " + Year + "    ";
            HeaderCell1.ColumnSpan = 68;
            HeaderCell1.BackColor = System.Drawing.Color.Yellow;
            HeaderCell1.ForeColor = System.Drawing.Color.Red;
            HeaderCell1.Font.Bold = true;
            HeaderCell1.Font.Size = 14;
            HeaderCell1.Attributes.Add("style", "text-align:center !important;");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            GVEmpChallan.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            string attachment = "attachment; filename=SupplementaryBillReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVEmpChallan.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('No Record Found for Selected Options')", true);
        }
    }
    public DataSet EmployeeSupplementaryReport_exporttoexcel(string monyear, int StaffNo, int CollegeNo, int IDNO)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_MON_YEAR", monyear);
            objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
            //objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
            objParams[2] = new SqlParameter("@P_IDNO ", IDNO);
            objParams[3] = new SqlParameter("@P_COLLEGENO", CollegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_SUPPLEMENTARY_REPORT_EXPORTTOEXCEL", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
        }
        return ds;
    }
}