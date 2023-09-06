//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_SCANCTION_LOAN.aspx                                                  
// CREATION DATE : 07-12-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using AjaxControlToolkit;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_PF_PF_SCANCTION_LOAN : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objpay = new PayController();

    PFCONTROLLER objPFController = new PFCONTROLLER();

    PF objpf = new PF();

    public Int32 empno;

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Making panels visible
                pnlSelection.Visible = true;
                pnlList.Visible = true;

                //Making panels invisible 
                pnlEmpDetails.Visible = false;
                pnlPFLoanEntry.Visible = false;

                this.FillCollege();
              //  this.FillEmployee();
                this.GetFinYearSdateEdate();

            }
        }


        if (!(ddlEmployee.SelectedValue == "-1" || ddlEmployee.SelectedValue == "0" || ddlEmployee.SelectedValue == null || ddlEmployee.SelectedValue == ""))
            empno = Convert.ToInt32(ddlEmployee.SelectedValue);
        else
            empno = 0;
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PF_SCANCTION_LOAN.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_SCANCTION_LOAN.aspx");
        }
    }

    private void BindListView(Int32 idno)
    {
        try
        {
            DataSet ds = objPFController.GetPfLoanByIdNo(idno);
            lvPFLoanEntry.DataSource = ds;
            lvPFLoanEntry.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void EmpInfo(Int32 idno)
    {
        try
        {
            DataSet ds = objpay.GetIncreEmp(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblDepartment.Text = Convert.ToString(ds.Tables[0].Rows[0]["DEPT"]);
                lblDesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESIG"]);
                lblIdnoName.Text = Convert.ToString(ds.Tables[0].Rows[0]["ENAME"]);
                lblDOB.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                lblDOA.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOJ"]).ToString("dd/MM/yyyy");
                lblBasic.Text = Convert.ToString(ds.Tables[0].Rows[0]["BASIC"]);
                lblPfNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["GPF_NO"]);
                lblEligiblePFType.Text = objCommon.LookUp("PAYROLL_PF_MAST A,PAYROLL_EMPMAS B", "A.SHORTNAME", "A.PFNO=B.PFNO AND B.IDNO=" + idno);
    
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.EmpInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objpf.IDNO = Convert.ToInt32(empno);
            objpf.FDATE = Convert.ToDateTime(txtFromDate.Text);
            objpf.TDATE = Convert.ToDateTime(txtToDate.Text);
            objpf.SANNO = txtScanctionNumber.Text;
            objpf.SANDT = Convert.ToDateTime(txtScanctionDate.Text);
            if (chkScanction.Checked)
                objpf.SANCTION = "S";
            else
                objpf.SANCTION = "N";

            objpf.SANAMT = Convert.ToDecimal(txtScanctionAmount.Text);
            objpf.COLLEGE_CODE = Session["colcode"].ToString();
            objpf.PFLOANTYPENO = Convert.ToInt32(ViewState["PFLOANTYPENO"].ToString());
            objpf.CURSANAMT = Convert.ToDecimal(ViewState["CURSANAMT"].ToString());
            objpf.COLLEGE_NO = Convert.ToInt32(HDNCOLLEGENO.Value);

            if (ViewState["pfLTNo"] != null)
            {
                objpf.PFLTNO = Convert.ToInt32(ViewState["pfLTNo"].ToString());
                CustomStatus cs = (CustomStatus)objPFController.UpdatePfLoan(objpf);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ViewState["action"] = "";
                    this.visiblestatus();
                    BindListView(empno);
                    objCommon.DisplayMessage(UpdatePanel1, "Record saved successfully", this);
                    //this.InsertInstallment(Convert.ToInt32(ViewState["pfLTNo"].ToString()));        
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnScanction_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnEdit = sender as Button;
            ViewState["pfLTNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "scanction";
            ShowEditDetails(Convert.ToInt32(ViewState["pfLTNo"].ToString()));
            this.visiblestatus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowEditDetails(int pfLTNo)
    {
        DataSet ds = null;
        try
        {
            ds = objPFController.GetPfLoanByPFLNO(pfLTNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFromDate.Text = ds.Tables[0].Rows[0]["FDATE"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["TDATE"].ToString();
                txtScanctionAmount.Text = ds.Tables[0].Rows[0]["SANAMT"].ToString();
                txtScanctionNumber.Text = ds.Tables[0].Rows[0]["SANNO"].ToString();
                txtScanctionDate.Text = ds.Tables[0].Rows[0]["SANDT"].ToString();
                txtAdvanceAmount.Text = ds.Tables[0].Rows[0]["ADVAMT"].ToString();
                HDNCOLLEGENO.Value = Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_NO"]);
                ViewState["CURSANAMT"] = ds.Tables[0].Rows[0]["CURSANAMT"].ToString();
                ViewState["PFLOANTYPENO"] = ds.Tables[0].Rows[0]["PFLOANTYPENO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.ShowEditDetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void FillCollege()
    {
        try
        {
            //if (ddlorderby.SelectedValue == "1")
            //    //  objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
            //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1", "EM.IDNO");

            //if (ddlorderby.SelectedValue == "2")
            //    // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");
            //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.FillCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillEmployee()
    {
        try
        {
            if (ddlorderby.SelectedValue == "1")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM,PAYROLL_PF_LOAN PFLOAN", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND EM.IDNO = PFLOAN.IDNO AND PFLOAN.SANCTION='N' AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND isnull(EM.PFNO,0)=1", "EM.IDNO");

            if (ddlorderby.SelectedValue == "2")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM,PAYROLL_PF_LOAN PFLOAN", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND EM.IDNO = PFLOAN.IDNO AND PFLOAN.SANCTION='N' AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_LOAN_ENTRY.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView(empno);
            EmpInfo(empno);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.ddlEmployee_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void visiblestatus()
    {
        if (ViewState["action"].ToString() == "scanction")
        {
            pnlSelection.Visible = false;
            pnlList.Visible = false;
            pnlEmpDetails.Visible = true;
            pnlPFLoanEntry.Visible = true;


        }
        else
        {
            pnlSelection.Visible = true;
            pnlList.Visible = true;
            pnlEmpDetails.Visible = false;
            pnlPFLoanEntry.Visible = false;
        }

    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillEmployee();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = Convert.ToString(Convert.ToDateTime(txtFromDate.Text).AddMonths(12).AddDays(-1));
    }

    private void GetFinYearSdateEdate()
    {
        string Fsdate = null;
        string Fedate = null;
        Fsdate = objCommon.LookUp("PAYROLL_PAY_REF", "PFFSDATE", string.Empty);
        Fedate = objCommon.LookUp("PAYROLL_PAY_REF", "PFFEDATE", string.Empty);
        txtFromDate.Text = Fsdate;
        txtToDate.Text = Fedate;
    }

    private void InsertInstallment(int pfLTNo)
    {
        DataSet ds = null;
        try
        {

            int PFLOANTYPENO = Convert.ToInt32(objCommon.LookUp("PAYROLL_PF_LOAN", "PFLOANTYPENO", "PFLTNO=" + pfLTNo));
            int PFNO = Convert.ToInt32(objCommon.LookUp("PAYROLL_PF_LOAN_TYPE", "PFNO", "PFLOANTYPENO=" + PFLOANTYPENO));
            string PAYHEAD = objCommon.LookUp("PAYROLL_PF_MAST", "H3", "PFNO=" + PFNO);
            string PAYSHORT = objCommon.LookUp("PAYROLL_PAYHEAD", "PAYSHORT", "PAYHEAD='" + PAYHEAD + "'");

            ds = objPFController.GetPfLoanByPFLNO(pfLTNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Payroll objinstall = new Payroll();
                objinstall.IDNO = Convert.ToInt32(empno);
                objinstall.INSTALNO = Convert.ToInt32(ds.Tables[0].Rows[0]["INSTALMENT"].ToString());
                objinstall.MONAMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["INSTAMT"].ToString());
                objinstall.TOTAMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["SANAMT"].ToString());
                objinstall.BAL_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["SANAMT"].ToString());
                objinstall.PAYHEAD = PAYHEAD;
                objinstall.CODE = PAYSHORT;
                objinstall.SUBHEADNO = 0;
                objinstall.STOP = false;
                objinstall.START_DT = Convert.ToDateTime("09/09/9999");
                objinstall.EXPDT = Convert.ToDateTime("09/09/9999");
                objinstall.PAIDNO = 0;
                objinstall.MON = null;
                objinstall.NEW = false;
                objinstall.ACCNO = "0";
                objinstall.REF_NO = null;
                objinstall.DESP_NO = null;
                objinstall.DESP_DT = null;
                objinstall.DEFA_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["SANAMT"].ToString());
                objinstall.PRO_AMT = null;
                objinstall.STOP1 = null;
                objinstall.REGULAR = false;
                objinstall.LTNO = pfLTNo;
                objinstall.REMARK = "Nothing";
                objinstall.COLLEGE_CODE = Session["colcode"].ToString();
                CustomStatus cs = (CustomStatus)objPFController.AddPFLoanInstallMent(objinstall);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["action"] = "";
                    this.visiblestatus();
                    BindListView(empno);
                    objCommon.DisplayMessage(UpdatePanel1, "Loan scanction successfully", this);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.InsertInstall()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                FillEmployee(Convert.ToInt32(ddlCollege.SelectedValue));
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void FillEmployee(int CollegeId)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                if(ddlorderby.SelectedValue == "1")
                {
                   // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM,PAYROLL_PF_LOAN PFLOAN", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND EM.IDNO = PFLOAN.IDNO AND PFLOAN.SANCTION='N' AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND isnull(EM.PFNO,0)=1", "EM.IDNO");

      
                }
                else if (ddlorderby.SelectedValue == "2")  
                {
                   // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM,PAYROLL_PF_LOAN PFLOAN", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND EM.IDNO = PFLOAN.IDNO AND PFLOAN.SANCTION='N' AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
                }
                // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND EM.STATUS IS NULL OR EM.STATUS <>'' AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue,"EM.IDNO");
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
