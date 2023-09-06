//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_Process.ASPX                                                    
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

public partial class PAYROLL_TRANSACTIONS_PF_PF_Process : System.Web.UI.Page
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropDown();
                ViewState["action"] = "add";
                ViewState["processStatus"] = 'N';
                ViewState["getpfno"] = "0";
                tblEmployee.Visible = false;

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
                Response.Redirect("~/notauthorized.aspx?page=PF_Process.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_Process.aspx");
        }
    }

    protected void BindListViewPfTrans()
    {
        DataSet ds = null;
        try
        {
            ds = objpf.GetPfTran(objpftran, "P");
            lvProcessPF.DataSource = ds;
            lvProcessPF.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label pf = (Label)lvProcessPF.FindControl("lblPf");
                Label pfAdd = (Label)lvProcessPF.FindControl("lblPfAdd");
                Label pfLoan = (Label)lvProcessPF.FindControl("lblPfLoan");
                if (ddlemployee.SelectedIndex > 0)
                {
                    pf.Text = txteligiblefor.Text;
                    pfAdd.Text = txteligiblefor.Text + " Add";
                    pfLoan.Text = txteligiblefor.Text + " Loan";
                }
                else
                {
                    pf.Text = "PF";
                    pfAdd.Text = "PF Add";
                    pfLoan.Text = "PF Loan";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PF_Process.isertdelete()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //protected void chkTransfer_CheckChanged(object sender, EventArgs e)
    //{
    //    if (chkTransfer.Checked == true)
    //    {
    //        divtranfer.Visible = true;
    //    }
    //    else
    //    {
    //        divtranfer.Visible = false;
    //    }
    //}


    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string monYear;
            int staffNoOfEmployee = 0;

            if (txtMonYearProcess.Text != string.Empty)
                monYear = Convert.ToDateTime(txtMonYearProcess.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtMonYearProcess.Text).ToString("yyyy").ToUpper();
            else
                monYear = null;

            objpftran.MONTHDATE = Convert.ToDateTime("1/" + txtMonYearProcess.Text);
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
            if (trEmployee.Visible)
            {
                objpftran.IDNO = Convert.ToInt32(ddlemployee.SelectedValue);
                staffNoOfEmployee = Convert.ToInt32(objCommon.LookUp("Payroll_empmas", "staffno", "idno=" + objpftran.IDNO));
            }
            else
            {
                objpftran.IDNO = 0;
            }

            if (trStaff.Visible)
            {
                objpftran.STAFFNO = Convert.ToInt32(ddlStaff.SelectedValue);
                staffNoOfEmployee = Convert.ToInt32(ddlStaff.SelectedValue);
            }
            else
            {
                objpftran.STAFFNO = 0;

            }

            objpftran.COLLEGE_CODE = Session["colcode"].ToString();

            if (chkTransfer.Checked == true)
            {

                if (txtTransferAmount.Text == string.Empty || txtTransferAmount.Text == "")
                {
                    txtTransferAmount.Text = "0";
                }
                if (Convert.ToDecimal(txtTransferAmount.Text) == 0)
                {
                    objCommon.DisplayMessage(UpdatePanelMain, "Please Enter Transfer Amount", this);
                    return;
                }
                else if (txtTransferDate.Text == string.Empty || txtTransferDate.Text == "")
                {
                    objCommon.DisplayMessage(UpdatePanelMain, "Please Select Transfer Date", this);
                    return;
                }

                objpftran.TRANSFERAMOUNT = Convert.ToDecimal(txtTransferAmount.Text);
                objpftran.TRANSFERDATE = Convert.ToDateTime(txtTransferDate.Text);
            }
            else
            {
                objpftran.TRANSFERAMOUNT = 0;
                //if (!txtTransferDate.Text.Trim().Equals(string.Empty)) objpftran.TRANSFERDATE = Convert.ToDateTime(txtTransferDate.Text);
                objpftran.TRANSFERDATE = Convert.ToDateTime("9999-12-31");
            }

            objpftran.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

            int checkSalaryLocked = Convert.ToInt32(objCommon.LookUp("Payroll_salfile", "count(*)", "SALLOCK=1 AND STATUS='Y' and monyear='" + objpftran.MONYEAR + "' and staffno=" + staffNoOfEmployee));

            if (checkSalaryLocked == 1)
            {

                CustomStatus cs = (CustomStatus)objpf.ProcessPfTran(objpftran);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["action"] = "add";
                    this.BindListViewPfTrans();
                    objCommon.DisplayMessage(UpdatePanelMain, "Record Saved Successfully", this);
                    clearttext();
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanelMain, "Salary Not Locked for the Month " + objpftran.MONYEAR, this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PF_Process.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void clearttext()
    {
        txteligiblefor.Text = "";
        txtMonYearProcess.Text = "";
        txtTransferAmount.Text = "";
        txtTransferDate.Text = "";
        chkTransfer.Checked = false;
        divtranfer.Visible = false;
        ddlCollege.SelectedIndex = 0;
        ddlemployee.SelectedIndex = 0;
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
          //  objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') AND isnull(EM.PFNO,0)=1 ", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PF_Process.FillDropDown-> " + ex.ToString());
        }

    }

    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            ViewState["getpfno"] = objCommon.LookUp("payroll_empmas", "isnull(pfno,0)", "(status is null or status='') and idno=" + Convert.ToInt32(ddlemployee.SelectedValue));
            string shortname = objCommon.LookUp("payroll_pf_mast", "shortname", "pfno=" + Convert.ToInt32(ViewState["getpfno"].ToString()));
            txteligiblefor.Text = shortname;
            //this.BindListViewPfTrans();
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

            if (date.Length > 2)
            {
                //finDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_PF_REF", "" + colname + "", "IDNO=" + Convert.ToInt32(ddlemployee.SelectedValue) + " AND STATUS='OB' AND CONVERT(DATETIME,'" + date + "',103) BETWEEN FSDATE AND FEDATE"));
                finDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_pay_REF", "" + colname + "", string.Empty));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PF_Process.FinancialYearStartDateEndDate()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return finDate;
    }

    protected void radStaff_CheckedChanged(object sender, EventArgs e)
    {
        trEligibleFor.Visible = false;
        trEmployee.Visible = false;
        trStaff.Visible = true;
        tblEmployee.Visible = true;
    }

    protected void radEmployee_CheckedChanged(object sender, EventArgs e)
    {
        trEligibleFor.Visible = true;
        trEmployee.Visible = true;
        trStaff.Visible = false;
        tblEmployee.Visible = true;
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlStaff.SelectedIndex > 0)
        {
            // this.BindListViewPfTrans();
        }
    }
    protected void chkTransfer_CheckedChanged(object sender, EventArgs e)
    {
         if (chkTransfer.Checked == true)
         {
             divtranfer.Visible = true;
         }
         else
         {
             divtranfer.Visible = false;
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
            if(ddlCollege.SelectedIndex > 0)
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
