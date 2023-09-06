//======================================================================================
// PROJECT NAME  : UAIMss                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_PF_LOAN_ENTRY .aspx                                                  
// CREATION DATE : O7-12-2009                                                        
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

public partial class PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY : System.Web.UI.Page
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
                this.FillEmployee();
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
                Response.Redirect("~/notauthorized.aspx?page=PF_LOAN_ENTRY.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_LOAN_ENTRY.aspx");
        }
    }



    private void BindListView(Int32 idno)
    {
        try
        {
            DataSet ds = objPFController.GetPfLoanByIdNo(idno);

            if (ds.Tables[0].Rows.Count <= 0)
                dpPager.Visible = false;
            else
                dpPager.Visible = true;

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
                this.FillLaonType(idno);
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlSelection.Visible = false;
        pnlList.Visible = false;
        pnlEmpDetails.Visible = true;
        pnlPFLoanEntry.Visible = true;
        //this.FillLaonType();
        ViewState["action"] = "add";
        this.GET_PrograsiveBalance_LoanBalance();
        btnAdd.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            objpf.IDNO = Convert.ToInt32(empno);
            objpf.FDATE = Convert.ToDateTime(txtFromDate.Text);
            objpf.TDATE = Convert.ToDateTime(txtToDate.Text);
            objpf.ADVAMT = Convert.ToDecimal(txtAdvAmt.Text);
            objpf.ADVDT = Convert.ToDateTime(txtAdvDate.Text);
            objpf.PFLOANTYPENO = Convert.ToInt32(ddlLoanTakenAs.SelectedValue);
            objpf.SANNO = "001/G1/2009";
            objpf.SANDT = Convert.ToDateTime(txtAdvDate.Text);
            objpf.PER = Convert.ToDecimal(txtsanctioper.Text);
            objpf.SANCTION = "N";
            objpf.SANAMT = Convert.ToDecimal(txtAmtToBeSanction.Text);
            objpf.PREBAL = Convert.ToDecimal(txtPerLaonBalanceAmt.Text);
            objpf.WDT = Convert.ToDateTime(txtAdvDate.Text);
            objpf.CURSANAMT = Convert.ToDecimal(txtAdvAmt.Text);
            objpf.COLLEGE_CODE = Session["colcode"].ToString();
            objpf.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objpf.REMARK = txtRemark.Text;
            if (Convert.ToBoolean(ViewState["DEDUCTED"]))
            {
                objpf.INSTALMENT = Convert.ToInt32(txtLoanRecoverablein.Text);
                objpf.INSTAMT = Convert.ToDecimal(txtInsatllmentAmount.Text);
            }
            else
            {
                objpf.INSTALMENT = 0;
                objpf.INSTAMT = 0;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int checkscanctioncount = Convert.ToInt32(objCommon.LookUp("PAYROLL_PF_LOAN", "COUNT(*)", "IDNO=" + empno + " and SANCTION='N'"));
                    if (checkscanctioncount == 0)
                    {
                        CustomStatus cs = (CustomStatus)objPFController.AddPfLoan(objpf);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            visiblestatus();
                            BindListView(empno);
                            objCommon.DisplayMessage(UpdatePanel1, "Record saved successfully", this);
                            ddlCollege.SelectedIndex = 0;
                            ddlEmployee.SelectedIndex = 0;
                            ddlorderby.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Can not apply for new loan because Previous Applied Loan was not scanction", this);

                    }
                }
                else
                {
                    if (ViewState["pfLTNo"] != null)
                    {
                        objpf.PFLTNO = Convert.ToInt32(ViewState["pfLTNo"].ToString());
                        CustomStatus cs = (CustomStatus)objPFController.DeletePfLoan(Convert.ToInt32(ViewState["pfLTNo"].ToString()));
                        if (cs.Equals(CustomStatus.RecordDeleted))
                        {
                            CustomStatus csadd = (CustomStatus)objPFController.AddPfLoan(objpf);
                            if (csadd.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["action"] = "add";
                                visiblestatus();
                                BindListView(empno);
                                objCommon.DisplayMessage(UpdatePanel1, "Record Updated successfully", this);
                                ddlCollege.SelectedIndex = 0;
                                ddlEmployee.SelectedIndex = 0;
                                ddlorderby.SelectedIndex = 0;
                            }
                        }
                    }
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["pfLTNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.FillLaonType(empno);
            ShowEditDetails(Convert.ToInt32(ViewState["pfLTNo"].ToString()));
            visiblestatus();
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
                txtAdvAmt.Text = ds.Tables[0].Rows[0]["ADVAMT"].ToString();
                txtAdvDate.Text = ds.Tables[0].Rows[0]["ADVDT"].ToString();
                ddlLoanTakenAs.SelectedValue = ds.Tables[0].Rows[0]["PFLOANTYPENO"].ToString();
                txtLoanRecoverablein.Text = ds.Tables[0].Rows[0]["INSTALMENT"].ToString();
                txtAmtToBeSanction.Text = ds.Tables[0].Rows[0]["SANAMT"].ToString();
                txtInsatllmentAmount.Text = ds.Tables[0].Rows[0]["INSTAMT"].ToString();
                txtAdvDate.Text = ds.Tables[0].Rows[0]["SANDT"].ToString();
                txtsanctioper.Text = ds.Tables[0].Rows[0]["PER"].ToString();
                txtPerLaonBalanceAmt.Text = ds.Tables[0].Rows[0]["PREBAL"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
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

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnDel = sender as ImageButton;
            int pfLTNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objPFController.DeletePfLoan(pfLTNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                visiblestatus();
                BindListView(empno);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListView(empno);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        Response.Redirect(Request.Url.ToString());
    }

    private void FillEmployee()
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
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
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
        if (ViewState["action"].ToString() == "add")
        {

            pnlSelection.Visible = true;
            pnlList.Visible = true;
            pnlEmpDetails.Visible = false;
            pnlPFLoanEntry.Visible = false;
            ViewState["action"] = "";
        }
        else
        {
            pnlSelection.Visible = false;
            pnlList.Visible = false;
            pnlEmpDetails.Visible = true;
            pnlPFLoanEntry.Visible = true;
        }

    }

    private void FillLaonType(int empno)
    {
        try
        {
            int pfNo = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "pfno", "idno=" + empno));
            pfNo = 0;
            objCommon.FillDropDownList(ddlLoanTakenAs, "PAYROLL_PF_LOAN_TYPE", "PFLOANTYPENO", "SHORTNAME", "pfNo=" + pfNo + " or " + pfNo + "=0", "PFLOANTYPENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.FillLaonType()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = Convert.ToString(Convert.ToDateTime(txtFromDate.Text).AddMonths(12).AddDays(-1));
    }

    private void GetFinYearSdateEdate()
    {
        DataSet DS;
        DS = objPFController.GETPFFINANCEYAER();
        string Fsdate = string.Empty;
        string Fedate = string.Empty;
        Fsdate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PFFSDATE"]).ToString("dd/MM/yyyy");
        Fedate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PFFEDATE"]).ToString("dd/MM/yyyy");
        txtFromDate.Text = Fsdate;
        txtToDate.Text = Fedate;

    }

    protected void ddlLoanTakenAs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string per = objCommon.LookUp("PAYROLL_PF_LOAN_TYPE", "AMT", "PFLOANTYPENO=" + Convert.ToInt32(ddlLoanTakenAs.SelectedValue));
            txtsanctioper.Text = per;
            if (txtProgressiveBalance.Text == "" || txtProgressiveBalance.Text == null)
                txtProgressiveBalance.Text = "0";
            decimal perSanction = Math.Round((Convert.ToDecimal(txtProgressiveBalance.Text) * Convert.ToDecimal(per) / 100));
            txtsanction.Text = Convert.ToString(perSanction);

            ViewState["DEDUCTED"] = objCommon.LookUp("PAYROLL_PF_LOAN_TYPE", "DEDUCTED", "PFLOANTYPENO=" + Convert.ToInt32(ddlLoanTakenAs.SelectedValue));

            if (Convert.ToBoolean(ViewState["DEDUCTED"]))
            {
                //tdlblAmtToBeSanction.Visible = true;
                //tdtxtAmtToBeSanction.Visible = true;
                trpfLRMI.Visible = true;
            }
            else
            {
                //tdlblAmtToBeSanction.Visible = false;
                //tdtxtAmtToBeSanction.Visible = false;
                trpfLRMI.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.ddlLoanTakenAs_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GET_PrograsiveBalance_LoanBalance()
    {

        try
        {
            string GetPrograsiveBalanceLoanBalance = string.Empty;
            string[] prograsivebalanceLoanBalance;
            GetPrograsiveBalanceLoanBalance = objPFController.GetPrograsiveBalanceLoanBalance(empno, txtFromDate.Text, txtToDate.Text);
            prograsivebalanceLoanBalance = GetPrograsiveBalanceLoanBalance.Split('#');
            txtProgressiveBalance.Text = prograsivebalanceLoanBalance[0].ToString();
            txtPerLaonBalanceAmt.Text = prograsivebalanceLoanBalance[1].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.GET_PrograsiveBalance_LoanBalance()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void butCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        Response.Redirect(Request.Url.ToString());
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
                      // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND EM.STATUS IS NULL OR EM.STATUS <>'' AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue,"EM.IDNO");
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1 AND  isnull(EM.PFNO,0)=1 AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
                }
                else if(ddlorderby.SelectedValue == "2")
                {
                    //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND  isnull(EM.PFNO,0)=1 AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
           
                }
              
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
