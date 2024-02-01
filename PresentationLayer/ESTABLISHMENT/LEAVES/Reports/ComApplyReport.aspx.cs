using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Globalization;
using System.Collections;
using System.Web;

public partial class ESTABLISHMENT_LEAVES_Reports_ComApplyReport : System.Web.UI.Page
{

    string date = "";
    int counter = 0;
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;                
                //this.FillEmployee();
                FillCollege();
                FillDepartment();
                FillStaffType();
            }
        }

    }

    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");

            //objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0", "DEPT.SUBDEPT");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffTypeType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillStaffType()
    {
        try
        {

            // objCommon.FillDropDownList(ddlstafftype, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_STAFFTYPE S ON (S.STNO = E.STNO)", " DISTINCT S.STNO", "S.STAFFTYPE", "S.STNO<>0 ", "STAFFTYPE");
            objCommon.FillDropDownList(ddlstafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffTypeType ->" + ex.Message + "  " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}
    }


    //Function to Generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);
            int empno = 0;
            int deptno = 0;
            int staffno = 0;
            string status;

            int collegeno = 0;

            if (ddlCollege.SelectedIndex > 0)
            {
                collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                collegeno = 0;
            }

            if (ddlstafftype.SelectedIndex > 0)
            {
                staffno = Convert.ToInt32(ddlstafftype.SelectedValue);
            }
            else
            {
                staffno = 0;
            }
            if (ddldept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }



            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_NO=" + collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            string college_no = Session["college_nos"].ToString();
            string[] values = college_no.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@P_COLLEGE_NO=" + collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + "," + "@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + "";
            }
            else
            {
                url += "&param=@P_COLLEGE_NO=" + collegeno + ",@P_FDATE=" + Fdate.ToString().Trim() + ",@P_TDATE=" + Tdate.ToString().Trim() + ",@P_DEPTNO=" + deptno + ",@P_STNO=" + staffno + "," + "@P_COLLEGE_CODE=" + Session["college_nos"].ToString() + "";
            }
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MONTH=" + txtMonthYear.Text.ToString().Trim()+",@P_EMPNO=" + empno + ",@P_DEPTNO=" + deptno ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";



        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_REPORT.ShowReport->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void Clear()
    {
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;

        ddlCollege.SelectedIndex = 0;
        ddlstafftype.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Comoff Application", "ComoffCredit.rpt");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        LeavesController objleave = new LeavesController();
        try
        {
            int collegeno, stno, deptno = 0;
            if (ddlCollege.SelectedIndex > 0)
            {
                collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                collegeno = 0;
            }
            if (ddlstafftype.SelectedIndex > 0)
            {
                stno = Convert.ToInt32(ddlstafftype.SelectedValue);
            }
            else
            {
                stno = 0;
            }
            if (ddldept.SelectedIndex > 0)
            {
                deptno = Convert.ToInt32(ddldept.SelectedValue);
            }
            else
            {
                deptno = 0;
            }

            DataSet ds = objleave.GetComOffCreditDataForExport(collegeno, stno, deptno, Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView gv_ExcelData = new GridView();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_ExcelData.DataSource = ds;
                    gv_ExcelData.DataBind();
                    string attachment = "attachment; filename=ComofCredited.xls";
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
                MessageBox("No Records found.");
                return;
            }

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
}