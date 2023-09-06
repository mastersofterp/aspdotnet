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

public partial class ESTABLISHMENT_LEAVES_Reports_Leave_OD_LWP_DetailReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DataTable dtLDTimeInt = new DataTable();
    LeavesController objApp = new LeavesController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (!Page.IsPostBack)
        { 
            FillDepartment();
            tr1.Visible = false;
            dtLDTimeInt.TableName.Equals("LDTimeInterval");
            dtLDTimeInt.Columns.Add("IDNO");
            dtLDTimeInt.Columns.Add("USERID");
            dtLDTimeInt.Columns.Add("USERNAME");
            dtLDTimeInt.Columns.Add("DATE");
            dtLDTimeInt.Columns.Add("SHIFTINTIME");
            dtLDTimeInt.Columns.Add("INTIME");
            dtLDTimeInt.Columns.Add("OUTTIME");
            dtLDTimeInt.Columns.Add("HOURS");
            dtLDTimeInt.Columns.Add("LEAVETYPE");
            ViewState["data"] = dtLDTimeInt;


            if (ua_type != 1 && IDNO != 22)
            {
                lblEmployee.Visible = true;
                ddlEmployee.Visible = true;
                trdept.Visible = false;
                //trsearchtype.Visible = false;
                this.FillEmployeeIdno();
                ddlEmployee.SelectedIndex = 1;
            }
        }

    }

    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            objCommon.FillDropDownList(ddlstafftype, "payroll_stafftype", "stno", "stafftype", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "stafftype");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillEmployeeIdno()
    {
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

     
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME", "IDNO=" + IDNO, "");
    }
    protected void rblSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelect.SelectedValue == "1")
        {
            tr1.Visible = true;
            ddlEmployee.Visible = true;
        }
        else
        {
            tr1.Visible = false;
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    protected void FillEmployee()
    {
        //created dataset object

        DataSet ds = new DataSet();
        ds = objApp.GetEmployee(Convert.ToInt32(ddlDept.SelectedValue));
        //to bind data source to Employee dropdown list
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlEmployee.Items.Clear();

            ddlEmployee.DataSource = ds;
            ddlEmployee.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlEmployee.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlEmployee.DataBind();
            ddlEmployee.SelectedItem.Text = "Please Select";
            //ddlEmployee.SelectedIndex = 0;
        }
    }
    protected void ddlstafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmpDeptStaffWise();
    }
    protected void FillEmpDeptStaffWise()
    {
        DataSet ds = new DataSet();
        ds = objApp.GetEmployeeByDeptStaff(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlstafftype.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlEmployee.Items.Clear();

            ddlEmployee.DataSource = ds;
            //ddlEmployee.DataValueField = ds.Tables[0].Columns[0].ToString();
            //ddlEmployee.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlEmployee.DataBind();
            ddlEmployee.SelectedItem.Text = "Please Select";
            //ddlEmployee.SelectedIndex = 0;
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("LeaveDetails", "ESTB_Leave_Derails_Report.rpt");
    }
    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text)));
            Tdate = Tdate.Substring(0, 10);

            

            int deptno = 0;
            int staffno = 0;
            int empno = 0;
          
            deptno =Convert.ToInt32(ddlDept.SelectedValue);
            staffno = Convert.ToInt32(ddlstafftype.SelectedValue);

            if (rblSelect.SelectedIndex == 0)
                empno = 0;
            else
                empno =Convert.ToInt32(ddlEmployee.SelectedValue);

            
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Fdate.ToString().Trim() + ",@P_TODATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + ",@P_EMPNO="+empno+"";

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
}
