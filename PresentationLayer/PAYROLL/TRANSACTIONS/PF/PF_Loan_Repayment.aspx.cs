//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_Loan_Repayment.ASPX                                                    
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

public partial class PAYROLL_TRANSACTIONS_PF_PF_Loan_Repayment : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,GPF_CONTROLLER,GPF
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    GPF_CONTROLLER objpf = new GPF_CONTROLLER();

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropDown();
                ViewState["action"] = "add";
                ViewState["processStatus"] = 'N';
                ViewState["getpfno"] = "0";

            }
        }

    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PF_Loan_Repayment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_Loan_Repayment.aspx");
        }
    }

    protected void BindListViewPfTrans()
    {
        DataSet ds = null;
        try
        {

            objpftran.STATUS = "LR";
            objpftran.IDNO = Convert.ToInt32(ddlemployee.SelectedValue);
            ds = objpf.GetPfTran(objpftran, ViewState["processStatus"].ToString());
            lvLoanRepayment.DataSource = ds;
            lvLoanRepayment.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_Loan_Repayment.isertdelete()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string monYear;

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
            //objpftran.H1 = 0;
            //objpftran.H2 = 0;
            //objpftran.H4 = 0;
            //objpftran.OB = 0;
            //objpftran.LOANBAL = 0;
            //objpftran.PROGRESSIVEBAL = 0;
            //objpftran.MONTHDATE = null;
            objpftran.PFNO = Convert.ToInt32(ViewState["getpfno"].ToString());
            objpftran.STATUS = "LR";
            objpftran.IDNO = Convert.ToInt32(ddlemployee.SelectedValue);
            objpftran.COLLEGE_CODE = Session["colcode"].ToString();
            objpftran.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

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
                        objCommon.DisplayMessage(UpdatePanelMain, "Record Saved Successfully", this);
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
                                Clear();
                                objCommon.DisplayMessage(UpdatePanelMain, "Record Saved Successfully", this);
                            }
                        }
                    }
                }
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_Loan_Repayment.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");             
           //objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND (EM.STATUS IS NULL OR EM.STATUS <>'')  AND isnull(EM.PFNO,0)=1 ", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }

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
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_Loan_Repayment.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
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
            txtMonthYear.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FSDATE"]).ToString("MM") + "/" + Convert.ToDateTime(ds.Tables[0].Rows[0]["FSDATE"]).ToString("yyyy");
            txtamount.Text = ds.Tables[0].Rows[0]["H3"].ToString();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_Loan_Repayment.ShowPfTranByTrxNO()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            ViewState["getpfno"] = objCommon.LookUp("payroll_empmas", "isnull(pfno,0)", "(status is null or status='') and idno=" + Convert.ToInt32(ddlemployee.SelectedValue));
            string shortname = objCommon.LookUp("payroll_pf_mast", "shortname", "pfno=" + Convert.ToInt32(ViewState["getpfno"].ToString()));
            lbleligibleFor.Text = shortname;
            this.BindListViewPfTrans();
        }
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
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_Loan_Repayment.FinancialYearStartDateEndDate()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return finDate;
    }

    private void Clear()
    {
        Response.Redirect(Request.Url.ToString());
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
                // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND EM.STATUS IS NULL OR EM.STATUS <>'' AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue,"EM.IDNO");

                // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') AND isnull(EM.PFNO,0)=1 ", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");

                objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND isnull(EM.PFNO,0)=1 AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
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
