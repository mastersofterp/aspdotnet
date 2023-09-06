using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.Globalization;
using System.IO;
using IITMS.SQLServer.SQLDAL;

public partial class ESTABLISHMENT_LEAVES_Reports_EmployeeDetailsExcel : System.Web.UI.Page
{


    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objShift = new LeavesController();
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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;
                //pnlList.Visible = true;
                //BindListViewEmployees();
                FillCollege();
                FillDepartment();
                //this.FillShift();
                this.FillStaffType();

            }


        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
    }
    private void FillDepartment()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListViewEmployees(int collegeno, int deptno, int StNo, int tranno)
    {
        try
        {

            if (ddlCollege.SelectedIndex >= 0 && ddlStaffType.SelectedIndex >= 0)
            {
                DataSet ds = objShift.RetrieveAllEmployee(collegeno, deptno, StNo, tranno);
                lvEmpList.DataSource = ds.Tables[0];
                lvEmpList.DataBind();
                lvEmpList.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        ddlDept.SelectedIndex = ddlCollege.SelectedIndex = ddlStaffType.SelectedIndex = 0;
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();
        lvEmpList.Visible = false;


    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0 && ddlStaffType.SelectedIndex >= 0)
            {
                BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue), 3);
                pnlList.Visible = true;
            }
            else
            {
                lvEmpList.DataSource = null;
                lvEmpList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0 && ddlStaffType.SelectedIndex >= 0)
            {
                BindListViewEmployees(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue), 3);
            }
            else
            {
                lvEmpList.DataSource = null;
                lvEmpList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            DataSet ds = null;

            int DEPT, STNO, Collegeno = 0;
            int checkcount = 0;
            string selectedIDs = string.Empty;

            if (ddlCollege.SelectedIndex > 0)
            {
                Collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                Collegeno = 0;
            }
            if (ddlStaffType.SelectedIndex > 0)
            {
                STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            }
            else
            {
                STNO = 0;
            }
            if (ddlDept.SelectedIndex > 0)
            {
                DEPT = Convert.ToInt32(ddlDept.SelectedValue);
            }
            else
            {
                DEPT = 0;
            }


            foreach (RepeaterItem ri in lvEmpList.Items)
            {
                CheckBox chk = ri.FindControl("chkID") as CheckBox;
                string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
                if (chk.Checked == true)
                {
                    checkcount += 1;
                    selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
                }
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Employee");
                return;
            }

            selectedIDs = selectedIDs.TrimEnd('$');


            ds = EmployeeDataExcel(Collegeno, STNO, DEPT, selectedIDs);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView gv_ExcelData = new GridView();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_ExcelData.DataSource = ds;
                    gv_ExcelData.DataBind();
                    string attachment = "attachment; filename=EmployeeAllDetails.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gv_ExcelData.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            else
            {
                // objCommon.DisplayMessage(this.Page, "No Data Found", this.Page);
                MessageBox("No Data Found");
                return;
            }


        }
        catch (Exception ex)
        {

        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    public DataSet EmployeeDataExcel(int Collegeno, int STNO, int DEPT, string selectedIDs)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objparams = null;
            objparams = new SqlParameter[4];
            objparams[0] = new SqlParameter("@P_COLLEGE_NO", Collegeno);
            objparams[1] = new SqlParameter("@P_STNO ", STNO);
            objparams[2] = new SqlParameter("@P_DEPT", DEPT);
            objparams[3] = new SqlParameter("@P_IDNOS", selectedIDs);

            ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAYROLL_EMPLOYEEALL_DATA_EXCEL", objparams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.ServicebookDataExcel->" + ex.ToString());
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
        }
    }
}