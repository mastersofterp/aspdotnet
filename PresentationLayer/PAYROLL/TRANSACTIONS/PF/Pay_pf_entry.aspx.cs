//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PAY_PF_ENTRY.ASPX                                                    
// CREATION DATE : 25-Nov-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,GPF_CONTROLLER,GPF
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    GPF_CONTROLLER objpf = new GPF_CONTROLLER();
    PFCONTROLLER objPFController = new PFCONTROLLER();

    GPF objpftran = new GPF();
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
                //  CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropDown();
                ViewState["action"] = "add";
                ViewState["processStatus"] = 'N';
                ViewState["getpfno"] = "0";
                this.GetFinYearSdateEdate();
            }
        }
        objpftran = GetPfTran();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_pf_entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_pf_entry.aspx");
        }
    }
    private void FillDropDown()
    {
        try
        {
            //  objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND (EM.STATUS IS NULL OR EM.STATUS <>'') ", "EM.IDNO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
        //try
        //{
        //    string wherecondition = string.Empty;
        //    if (chkpfcontribution.Checked)
        //    {
        //        wherecondition = "PM.PSTATUS = 'N'";
        //    }
        //    else
        //    {
        //        wherecondition = "PM.PSTATUS = 'Y'";
        //        objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') AND isnull(EM.PFNO,0)=1 AND " + wherecondition + " ", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
        //        // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND " + wherecondition + " and EM.IDNO > 0 AND (EM.STATUS IS NULL OR EM.STATUS <>'') ", "EM.IDNO");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        //}
    }
    protected void BindListViewPfTrans()
    {
        DataSet ds = null;
        try
        {
            ds = objpf.GetPfTran(objpftran, ViewState["processStatus"].ToString());
            if (chkpfcontribution.Checked)
            {
                lvItemGpfCpfContibutionEntry.DataSource = ds;
                lvItemGpfCpfContibutionEntry.DataBind();
            }
            else
            {
                switch (Tabs.ActiveTabIndex)
                {
                    case 0: lvOpeningBalance.DataSource = ds;
                        lvOpeningBalance.DataBind();
                        break;
                    case 1:
                        lvProcessPF.DataSource = ds;
                        lvProcessPF.DataBind();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Label pf = (Label)lvProcessPF.FindControl("lblPf");
                            Label pfAdd = (Label)lvProcessPF.FindControl("lblPfAdd");
                            Label pfLoan = (Label)lvProcessPF.FindControl("lblPfLoan");
                            pf.Text = lbleligibleFor.Text;
                            pfAdd.Text = lbleligibleFor.Text + " Add";
                            pfLoan.Text = lbleligibleFor.Text + " Loan";
                        }
                        break;
                    case 2: lvLoanRepayment.DataSource = ds;
                        lvLoanRepayment.DataBind();
                        break;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.isertdelete()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objpftran = GetPfTran();

            if (Tabs.ActiveTabIndex == 1)
            {
                int staffNoOfEmployee = Convert.ToInt32(objCommon.LookUp("Payroll_empmas", "staffno", "idno=" + objpftran.IDNO));
                int checkSalaryLocked = Convert.ToInt32(objCommon.LookUp("Payroll_salfile", "count(*)", "SALLOCK=1 AND STATUS='Y' and monyear='" + objpftran.MONYEAR + "' and staffno=" + staffNoOfEmployee));

                if (checkSalaryLocked == 1)
                {

                    CustomStatus cs = (CustomStatus)objpf.ProcessPfTran(objpftran);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        this.BindListViewPfTrans();
                        objCommon.DisplayMessage(updProcessPF, "Record Saved Successfully", this);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updProcessPF, "Salary Not Locked for the Month " + objpftran.MONYEAR, this);
                }

            }
            else
            {
                if (Checkvalidations())
                {
                    //Check whether to add or update
                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString().Equals("add"))
                        {

                            CustomStatus cs = (CustomStatus)objpf.AddPfTran(objpftran);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["action"] = "add";
                                this.BindListViewPfTrans();
                                objCommon.DisplayMessage(updOpeningBalance, "Record Saved Successfully", this);
                            }
                        }
                        else
                        {
                            if (ViewState["pfTrxNo"] != null)
                            {
                                CustomStatus cs = (CustomStatus)objpf.DeletePfTran(Convert.ToInt32(ViewState["pfTrxNo"].ToString()));
                                if (cs.Equals(CustomStatus.RecordDeleted))
                                {
                                    CustomStatus csadd = (CustomStatus)objpf.AddPfTran(objpftran);
                                    if (csadd.Equals(CustomStatus.RecordSaved))
                                    {
                                        ViewState["action"] = "add";
                                        this.BindListViewPfTrans();
                                        objCommon.DisplayMessage(updOpeningBalance, "Record Saved Successfully", this);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updOpeningBalance, "Record Already Exists", this);
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ActiveTabChanged(object sender, EventArgs e)
    {
        this.BindListViewPfTrans();
        ViewState["action"] = "add";
        this.ChangeButtonCaption();
        switch (Tabs.ActiveTabIndex)
        {
            case 0:
                rfvFromDate.Enabled = true;
                rfvtxtOB.Enabled = true;
                mevFromDate.Enabled = true;
                cvtxtOB.Enabled = true;
                rfvLoanOB.Enabled = true;
                CvLoanOB.Enabled = true;
                rvftxtamount.Enabled = false;
                rfvMonthYear.Enabled = false;
                cvtxtamount.Enabled = false;
                rfvtxtMonthYearContributionAmount.Enabled = false;
                rfvtxtDeductionAmount1.Enabled = false;
                rfvtxtDeductionAmount2.Enabled = false;
                rfvtxtDeductionAmount3.Enabled = false;
                rfvtxtDeductionAmount4.Enabled = false;
                cvtxtDeductionAmount1.Enabled = false;
                cvtxtDeductionAmount2.Enabled = false;
                cvtxtDeductionAmount3.Enabled = false;
                cvtxtDeductionAmount4.Enabled = false;
                rfvtxtMonYearProcess.Enabled = false;
                break;
            case 1:
                rfvFromDate.Enabled = false;
                rfvtxtOB.Enabled = false;
                mevFromDate.Enabled = false;
                cvtxtOB.Enabled = false;
                rfvLoanOB.Enabled = false;
                CvLoanOB.Enabled = false;
                rvftxtamount.Enabled = false;
                rfvMonthYear.Enabled = false;
                cvtxtamount.Enabled = false;
                rfvtxtMonthYearContributionAmount.Enabled = false;
                rfvtxtDeductionAmount1.Enabled = false;
                rfvtxtDeductionAmount2.Enabled = false;
                rfvtxtDeductionAmount3.Enabled = false;
                rfvtxtDeductionAmount4.Enabled = false;
                cvtxtDeductionAmount1.Enabled = false;
                cvtxtDeductionAmount2.Enabled = false;
                cvtxtDeductionAmount3.Enabled = false;
                cvtxtDeductionAmount4.Enabled = false;
                rfvtxtMonYearProcess.Enabled = true;
                break;
            case 2:
                rfvFromDate.Enabled = false;
                rfvtxtOB.Enabled = false;
                mevFromDate.Enabled = false;
                cvtxtOB.Enabled = false;
                rfvLoanOB.Enabled = false;
                CvLoanOB.Enabled = false;
                rvftxtamount.Enabled = true;
                rfvMonthYear.Enabled = true;
                cvtxtamount.Enabled = true;
                rfvtxtMonthYearContributionAmount.Enabled = false;
                rfvtxtDeductionAmount1.Enabled = false;
                rfvtxtDeductionAmount2.Enabled = false;
                rfvtxtDeductionAmount3.Enabled = false;
                rfvtxtDeductionAmount4.Enabled = false;
                cvtxtDeductionAmount1.Enabled = false;
                cvtxtDeductionAmount2.Enabled = false;
                cvtxtDeductionAmount3.Enabled = false;
                cvtxtDeductionAmount4.Enabled = false;
                rfvtxtMonYearProcess.Enabled = false;
                break;
        }

    }
    protected void ChangeButtonCaption()
    {
        if (Tabs.ActiveTabIndex == 1)
            butSubmit.Text = "UptoDate";
        else
            butSubmit.Text = "Submit";
    }
    private void GetFinYearSdateEdate()
    {
        //string Fsdate = null;
        //string Fedate = null;
        //Fsdate = objCommon.LookUp("PAYROLL_PAY_REF", "PFFSDATE", string.Empty);
        //Fedate = objCommon.LookUp("PAYROLL_PAY_REF", "PFFEDATE", string.Empty);
        //txtFromDate.Text = Fsdate;
        //txtToDate.Text = Fedate;
        DataSet DS;
        DS = objPFController.GETPFFINANCEYAER();
        string Fsdate = string.Empty;
        string Fedate = string.Empty;
        Fsdate = Convert.ToDateTime(DS.Tables[0].Rows[0]["FDATE"]).ToString("dd/MM/yyyy");
        Fedate = Convert.ToDateTime(DS.Tables[0].Rows[0]["TDATE"]).ToString("dd/MM/yyyy");
        txtFromDate.Text = Fsdate;
        txtToDate.Text = Fedate;
    }
    private GPF GetPfTran()
    {
        try
        {
            string monYear;
            switch (Tabs.ActiveTabIndex)
            {
                case 0:
                    if (txtFromDate.Text != string.Empty)
                        monYear = Convert.ToDateTime(txtFromDate.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy").ToUpper();
                    else
                        monYear = null;

                    if (txtOB.Text == string.Empty)
                        objpftran.OB = 0;
                    else
                        objpftran.OB = Convert.ToDecimal(txtOB.Text);

                    if (txtLoanOB.Text == string.Empty)
                        objpftran.LOANBAL = 0;
                    else
                        objpftran.LOANBAL = Convert.ToDecimal(txtLoanOB.Text);

                    if (txtFromDate.Text == string.Empty)
                        objpftran.FSDATE = null;
                    else
                        objpftran.FSDATE = Convert.ToDateTime(txtFromDate.Text);

                    if (txtFromDate.Text == string.Empty)
                        objpftran.FEDATE = null;
                    else
                        objpftran.FEDATE = Convert.ToDateTime(txtToDate.Text);

                    objpftran.MONYEAR = "OB";
                    objpftran.H1 = 0;
                    objpftran.H2 = 0;
                    objpftran.H3 = 0;
                    objpftran.H4 = 0;
                    objpftran.PROGRESSIVEBAL = 0;
                    objpftran.PFNO = Convert.ToInt32(ViewState["getpfno"].ToString());
                    objpftran.STATUS = "OB";
                    ViewState["processStatus"] = 'N';
                    break;
                case 1:

                    if (txtMonYearProcess.Text != string.Empty)
                        monYear = Convert.ToDateTime(txtMonYearProcess.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonYearProcess.Text).ToString("yyyy").ToUpper();
                    else
                        monYear = null;

                    objpftran.FSDATE = FinancialYearStartDateEndDate("1/" + txtMonYearProcess.Text, "FS");
                    objpftran.FEDATE = FinancialYearStartDateEndDate("1/" + txtMonYearProcess.Text, "FE");
                    objpftran.MONYEAR = monYear;
                    objpftran.H1 = 0;
                    objpftran.H2 = 0;
                    objpftran.H3 = 0;
                    objpftran.H4 = 0;
                    objpftran.OB = 0;
                    objpftran.LOANBAL = 0;
                    objpftran.PROGRESSIVEBAL = 0;
                    objpftran.PFNO = Convert.ToInt32(ViewState["getpfno"].ToString());
                    objpftran.STATUS = "MONFILE";
                    ViewState["processStatus"] = 'P';
                    break;
                case 2:
                    if (txtMonthYear.Text != string.Empty)
                        monYear = Convert.ToDateTime(txtMonthYear.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy").ToUpper();
                    else
                        monYear = null;

                    if (txtamount.Text == string.Empty)
                        objpftran.H3 = null;
                    else
                        objpftran.H3 = Convert.ToDecimal(txtamount.Text);


                    objpftran.FSDATE = FinancialYearStartDateEndDate("1/" + txtMonthYear.Text, "FS");
                    objpftran.FEDATE = FinancialYearStartDateEndDate("1/" + txtMonthYear.Text, "FE");
                    objpftran.MONYEAR = monYear;
                    objpftran.H1 = 0;
                    objpftran.H2 = 0;
                    objpftran.H4 = 0;
                    objpftran.OB = 0;
                    objpftran.LOANBAL = 0;
                    objpftran.PROGRESSIVEBAL = 0;
                    objpftran.PFNO = Convert.ToInt32(ViewState["getpfno"].ToString());
                    objpftran.STATUS = "LR";
                    ViewState["processStatus"] = 'N';
                    break;
            }

            if (chkpfcontribution.Checked)
            {
                if (txtMonthYearContributionAmount.Text != string.Empty)
                    monYear = Convert.ToDateTime(txtMonthYearContributionAmount.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonthYearContributionAmount.Text).ToString("yyyy").ToUpper();
                else
                    monYear = null;

                objpftran.FSDATE = FinancialYearStartDateEndDate("1/" + txtMonthYearContributionAmount.Text, "FS");
                objpftran.FEDATE = FinancialYearStartDateEndDate("1/" + txtMonthYearContributionAmount.Text, "FE");
                objpftran.MONYEAR = monYear;
                objpftran.H1 = Convert.ToDecimal(txtDeductionAmount1.Text);
                objpftran.H2 = Convert.ToDecimal(txtDeductionAmount3.Text);
                objpftran.H3 = Convert.ToDecimal(txtDeductionAmount2.Text);
                objpftran.H4 = Convert.ToDecimal(txtDeductionAmount4.Text);
                objpftran.PROGRESSIVEBAL = 0;
                objpftran.PFNO = Convert.ToInt32(ViewState["getpfno"].ToString());
                objpftran.STATUS = lbleligibleFor.Text + ".C";
                objpftran.OB = 0;
                objpftran.LOANBAL = 0;
                ViewState["processStatus"] = 'N';
            }
            objpftran.IDNO = Convert.ToInt32(ddlemployee.SelectedValue);
            objpftran.COLLEGE_CODE = Session["colcode"].ToString();
            objpftran.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.GetPfTran()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return objpftran;
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            ViewState["getpfno"] = objCommon.LookUp("payroll_empmas", "isnull(pfno,0)", "(status is null or status='') and idno=" + Convert.ToInt32(ddlemployee.SelectedValue));
            string shortname = objCommon.LookUp("payroll_pf_mast", "shortname", "pfno=" + Convert.ToInt32(ViewState["getpfno"].ToString()));
            lbleligibleFor.Text = shortname;
            chkpfcontribution.Text = shortname + " Contribution Entry";
            OpeningBalance.HeaderText = shortname + " Opening Balance";
            LoanRepayment.HeaderText = shortname + " Loan Repayment";
            ProcessPF.HeaderText = "Process " + shortname;
            this.BindListViewPfTrans();

        }
    }
    protected void chkpfcontribution_CheckedChanged(object sender, EventArgs e)
    {
        if (chkpfcontribution.Checked)
        {
            Tabs.Visible = false;
            divPfContribution.Visible = true;
            this.FillDropDown();
            this.BindListViewPfTrans();
        }
        else
        {
            divPfContribution.Visible = false;
            Tabs.Visible = true;
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = Convert.ToString(Convert.ToDateTime(txtFromDate.Text).AddMonths(12).AddDays(-1));
    }

    protected bool Checkvalidations()
    {
        bool flag = true;
        int count;
        try
        {
            switch (Tabs.ActiveTabIndex)
            {
                case 0:
                    count = Convert.ToInt32(objCommon.LookUp("payroll_pf_tran", "count(*)", "STATUS='OB' and IDNO=" + Convert.ToInt32(ddlemployee.SelectedValue) + " and FSDATE=convert(datetime,'" + txtFromDate.Text + "',103) and FEDATE=convert(datetime,'" + txtToDate.Text + "',103)"));
                    if (count > 0)
                        flag = false;
                    else
                        flag = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.Checkvalidations()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return flag;
    }

    private DateTime FinancialYearStartDateEndDate(string date, string flag)
    {
        DateTime finDate = Convert.ToDateTime("09/09/9999");

        try
        {
            string colname = string.Empty;
            if (flag == "FS")
                colname = "PFFSDATE";
            else
                colname = "PFFEDATE";

            if (ddlemployee.SelectedIndex > 0 && date.Length > 2)
            {
                //finDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_PF_REF", "" + colname + "", "IDNO=" + Convert.ToInt32(ddlemployee.SelectedValue) + " AND STATUS='OB' AND CONVERT(DATETIME,'" + date + "',103) BETWEEN FSDATE AND FEDATE"));
                finDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_pay_REF", "" + colname + "", string.Empty));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.FinancialYearStartDateEndDate()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return finDate;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["pfTrxNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowPfTranByTrxNO(Convert.ToInt32(ViewState["pfTrxNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ShowPfTranByTrxNO(int pfTrxNo)
    {
        try
        {
            DataSet ds = null;
            ds = objpf.GetPfTranByTrxno(pfTrxNo);

            if (chkpfcontribution.Checked)
            {
                txtMonthYearContributionAmount.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FSDATE"]).ToString("MM") + "/" + Convert.ToDateTime(ds.Tables[0].Rows[0]["FSDATE"]).ToString("yyyy"); ;
                txtDeductionAmount1.Text = ds.Tables[0].Rows[0]["H1"].ToString();
                txtDeductionAmount2.Text = ds.Tables[0].Rows[0]["H2"].ToString();
                txtDeductionAmount3.Text = ds.Tables[0].Rows[0]["H3"].ToString();
                txtDeductionAmount4.Text = ds.Tables[0].Rows[0]["H4"].ToString();
            }
            else
            {

                switch (Tabs.ActiveTabIndex)
                {
                    case 0: txtFromDate.Text = ds.Tables[0].Rows[0]["FSDATE"].ToString();
                        txtToDate.Text = ds.Tables[0].Rows[0]["FEDATE"].ToString();
                        txtOB.Text = ds.Tables[0].Rows[0]["OB"].ToString();
                        txtLoanOB.Text = ds.Tables[0].Rows[0]["LOANBAL"].ToString();
                        break;
                    case 2: txtMonthYear.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FSDATE"]).ToString("MM") + "/" + Convert.ToDateTime(ds.Tables[0].Rows[0]["FSDATE"]).ToString("yyyy");
                        txtamount.Text = ds.Tables[0].Rows[0]["H3"].ToString();
                        break;

                }
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_GPFCPF_Pay_pf_entry.ShowPfTranByTrxNO()-> " + ex.Message + " " + ex.StackTrace);
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
                FillEmployee();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void FillEmployee()
    {

        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                string wherecondition = string.Empty;
                if (chkpfcontribution.Checked)
                {
                    wherecondition = "PM.PSTATUS = 'N'";
                    objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='N' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
         
                }
                else
                {
                    wherecondition = "PM.PSTATUS = 'Y'";
                    // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') AND isnull(EM.PFNO,0)=1 AND " + wherecondition + " ", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
                    // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND " + wherecondition + " and EM.IDNO > 0 AND (EM.STATUS IS NULL OR EM.STATUS <>'') ", "EM.IDNO");
                    objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
                }
            }
           
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND EM.STATUS IS NULL OR EM.STATUS <>'' AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue,"EM.IDNO");
                objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
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
