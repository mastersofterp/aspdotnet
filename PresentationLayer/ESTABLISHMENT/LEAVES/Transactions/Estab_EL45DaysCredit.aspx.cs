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

public partial class ESTABLISHMENT_LEAVES_Transactions_Estab_EL45DaysCredit : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    LeavesController objleave = new LeavesController();
     

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (ViewState["action"] == null)
                        ViewState["action"] = "add";
                    FillPeriod();
                    FillCollege();
                    this.FillStaff();
                    FillYear();
                   
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");

    }
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
    }
   
    private void FillLeave()
    {
        try
        {
            //objCommon.FillDropDownList(ddlLeave, "payroll_leave", "LNO", "LEAVENAME", "stno="+ Convert.ToInt32(ddlStaffType.SelectedValue)+"AND PERIOD="+ Convert.ToInt32(ddlPeriod.SelectedValue), "LEAVENAME");
           // objCommon.FillDropDownList(ddlLeave, "payroll_leave", "LNO", "LEAVENAME", "stno=" + Convert.ToInt32(ddlStaffType.SelectedValue), "LEAVENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaffType, "payroll_stafftype", "stno", "stafftype", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "stafftype");
            //objCommon.FillDropDownList(ddldept, "payroll_subdept", "subdeptno", "subdept", "subdeptno NOT IN(0)", "subdept");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    private void FillEmployeeListView()
    {
        try
        {
            Leaves objlv = new Leaves();
            objlv.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
            objlv.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objlv.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            DataSet ds = objleave.GetEmpListFor45DayCLCredit(objlv);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvLeave.DataSource = ds;
                    lvLeave.DataBind();
                    pnlList.Visible = true;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    lvLeave.DataSource = null;
                    lvLeave.DataBind();
                    pnlList.Visible = false;
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                lvLeave.DataSource = null;
                lvLeave.DataBind();
                pnlList.Visible = false;
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BulkUpdPermanentRegNo.FillStudentListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = ddldept.SelectedIndex = ddlStaffType.SelectedIndex = 0;
        lvLeave.DataSource = null;
        lvLeave.DataBind();
        pnlList.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = new CustomStatus();
            Leaves objlv = new Leaves();
            int records = 0;
            int updated = 0;
            objlv.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
            objlv.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objlv.YEAR = Convert.ToInt32(ddlYear.SelectedItem.Text);
            foreach (ListViewDataItem dataItem in lvLeave.Items)
            {
                //records += 1;

                TextBox PrNo = dataItem.FindControl("txtCLCredit") as TextBox;
                //txAllocateDate
                TextBox txAllocateDate = dataItem.FindControl("txAllocateDate") as TextBox;
                //Get Hidden Data..
                HiddenField hdfidno = dataItem.FindControl("hdfidno") as HiddenField;
                int idno = Convert.ToInt32(hdfidno.Value);
                objlv.EMPNO = Convert.ToInt32(hdfidno.Value);
                int altyear = Convert.ToInt32(DateTime.Now.Year);
                //====================================
                //Code to set year for last_allocate date
                 string DOJ_str=objCommon.LookUp("PAYROLL_EMPMAS","DOJ","IDNO="+ idno);
                 DateTime DOJ = Convert.ToDateTime(DOJ_str);
                 int year_doj = Convert.ToInt32(DOJ.Year);
                 int month_doj = Convert.ToInt32(DOJ.Month);
                 int year_current = Convert.ToInt32(DateTime.Now.Year);
                 int month_current = Convert.ToInt32(DateTime.Now.Month);
                 //if (year_doj == year_current && month_current < 7)//2015==2015
                 //{
                 //    altyear = altyear - 1;//2014
                 //}
                 //else if (year_doj < year_current && month_current < 7)//2014<2015
                 //{
                 //    altyear = Convert.ToInt32(DateTime.Now.Year);//2015
                 //}
                 if ((year_doj < year_current) && (month_current >= 7) && (month_doj >= 7))
                 {
                     altyear = Convert.ToInt32(DateTime.Now.Year);//2015
                 }
                 else if ((year_doj < year_current) && (month_current >= 7) && (month_doj < 7))
                 {
                     altyear = altyear - 1;//2014
                 }
                 else if ((year_doj < year_current) && (month_current < 7) && (month_doj < 7))
                 {
                     altyear = altyear - 1;//2014
                 }
                 else if ((year_doj < year_current) && (month_current < 7) && (month_doj >=7))//joining date
                 {
                     altyear = altyear - 1;//2014
                 }
                 else if (year_doj == year_current && (month_current < 7))
                 {
                     altyear = Convert.ToInt32(DateTime.Now.Year);//2015
                 }
                 else if (year_doj == year_current && (month_current >= 7))
                 {
                     altyear = Convert.ToInt32(DateTime.Now.Year);//2015
                 }

                //====================================
                //double leave;
                objlv.FROMDT = Convert.ToDateTime(altyear + "-07-01");
                objlv.TODT = Convert.ToDateTime(altyear + "-12-31");
                DateTime doallot = Convert.ToDateTime(altyear + "-07-01");
                
                DateTime doj = Convert.ToDateTime(objCommon.LookUp("PAYROLL_EMPMAS", "DOJ", "IDNO=" + idno));

                
                if (doj > doallot)
                {
                    objlv.APPDT = doj;
                }
                else
                {
                    objlv.APPDT = doallot;
                }

                objlv.APPDT = Convert.ToDateTime(txAllocateDate.Text);      
   
                if (PrNo.Text == string.Empty)
                {
                    PrNo.Text = "0";
                }
                if (PrNo.Text != "0")
                {
                    objlv.NO_DAYS = Convert.ToDouble(PrNo.Text);
                    objlv.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
                    objlv.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);
                    //CL_NO
                    int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "ISNULL(CL_NO,0) AS CL_NO", ""));
                    int lno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LNO", "STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "  AND LEAVENO="+leaveno +""));
                    objlv.LNO = lno;    
                    objlv.LEAVENO = leaveno;                   
                    objlv.FROMDT = Convert.ToDateTime(txtFromDt.Text);
                    objlv.TODT = Convert.ToDateTime(txtToDt.Text);
                    objlv.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                    objlv.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                    int leave_session_srno = objleave.AddGetLeaveSessionDetails(objlv);
                    objlv.SESSION_SRNO = leave_session_srno;
                    cs = (CustomStatus)objleave.Add_CL45DAYS_LEVES(objlv);
                   // objCommon.DisplayMessage("Records Saved Sucessfully!", this);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        records += 1;
                        updated += 1;
                    }
                }
            }

            if (records == updated)
            {
                objCommon.DisplayMessage("Records Saved Sucessfully!", this);
                this.FillEmployeeListView();
            }
            else
                objCommon.DisplayMessage("Error in Leave Opening Entry", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_BulkUpdPermanentRegNo.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillYear()
    {
        int Yr = DateTime.Now.Year;
        ddlYear.Items.Add("Please Select");
        ddlYear.Items.Add(Convert.ToString(Yr - 1));
        ddlYear.Items.Add(Convert.ToString(Yr));
        ddlYear.Items.Add(Convert.ToString(Yr + 1));
        //ddlYear.SelectedValue = (Convert.ToString(Yr));
    }
    
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();

    }
    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            if (ddlStaffType.SelectedIndex > 0 && ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "", "DEPT.SUBDEPT");
            }
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeListView();  
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPeriod.SelectedValue == "1")
        {
            DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 7, 1);
            DateTime todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 12, 31);
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }
        else if (ddlPeriod.SelectedValue == "2")
        {
            DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 1, 1);
            DateTime todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 6, 30);
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }
        else if (ddlPeriod.SelectedValue == "3")
        {
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            year = year + 1;
            DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 7, 1);
            DateTime todt = new DateTime(Convert.ToInt32(year), 6, 30);
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }
        else
        {
            txtFromDt.Text = System.DateTime.Now.ToString();
            txtToDt.Text = System.DateTime.Now.ToString();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {        
        FillEmployeeListView();
    }
}
