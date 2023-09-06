//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_Pf_OpeningBal.ASPX                                                    
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

public partial class PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,GPF_CONTROLLER,GPF
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    GPF_CONTROLLER objpf = new GPF_CONTROLLER();
    PFCONTROLLER objPFController = new PFCONTROLLER();

    GPF objpftran = new GPF();

    public int getpfno = 0;

    #region PageEvents

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
                ViewState["getpfno"] = "0";
                this.GetFinYearSdateEdate();
            }
        }
    }

    #endregion

    #region FormEvents

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pf_OpeningBal.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pf_OpeningBal.aspx");
        }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if(txtFromDate.Text == "")
            {
                objCommon.DisplayMessage(UpdatePanelMain, "Enter From Date", this);
            }
            else if (txtToDate.Text == "")
            {
                objCommon.DisplayMessage(UpdatePanelMain, "Enter To Date", this);
            }
            string monYear;

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
            {
                objpftran.FSDATE = null;
                objpftran.MONTHDATE = null;
            }
            else
            {
                objpftran.FSDATE = Convert.ToDateTime(txtFromDate.Text);
                objpftran.MONTHDATE = Convert.ToDateTime(txtFromDate.Text);
            }

            if (txtToDate.Text == string.Empty)
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
            objpftran.IDNO = Convert.ToInt32(ddlemployee.SelectedValue);
            objpftran.COLLEGE_CODE = Session["colcode"].ToString();
            objpftran.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int DuplicateCheckOpeningBalance = Convert.ToInt32(objCommon.LookUp("payroll_pf_tran", "count(*)", "STATUS='OB' and IDNO=" + Convert.ToInt32(ddlemployee.SelectedValue) + " and FSDATE=convert(datetime,'" + txtFromDate.Text + "',103) and FEDATE=convert(datetime,'" + txtToDate.Text + "',103)"));

                    if (DuplicateCheckOpeningBalance == 0)
                    {
                        CustomStatus cs = (CustomStatus)objpf.AddPfTran(objpftran);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            this.BindListViewPfTrans();
                            objCommon.DisplayMessage(UpdatePanelMain, "Record Saved Successfully", this);
                            ClearText();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanelMain, "Record Already Exists", this);
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
                                objCommon.DisplayMessage(UpdatePanelMain, "Record Updated Successfully", this);
                                ClearText();
                            }
                        }
                    }
                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ClearText()
    {
        ddlCollege.SelectedIndex = 0;
        ddlemployee.SelectedIndex = 0;
        txteligiblefor.Text = "";
        txtOB.Text = "";
        txtLoanOB.Text="";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ViewState["action"] = "add";
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
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
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
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
            //lbleligibleFor.Text = shortname;
            txteligiblefor.Text = shortname;
            this.BindListViewPfTrans();
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = Convert.ToString(Convert.ToDateTime(txtFromDate.Text).AddMonths(12).AddDays(-1));
    }

    #endregion

    #region PrivateMethods 
    
    private void BindListViewPfTrans()
    {
        DataSet ds = null;
        try
        {
            objpftran.MONYEAR = "OB";
            objpftran.STATUS = "OB";
            objpftran.IDNO = Convert.ToInt32(ddlemployee.SelectedValue);
            ds = objpf.GetPfTran(objpftran, "N");
            lvOpeningBalance.DataSource = ds;
            lvOpeningBalance.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal.isertdelete()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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

    }

    private void ShowPfTranByTrxNO(int pfTrxNo)
    {
        try
        {
            DataSet ds = null;
            ds = objpf.GetPfTranByTrxno(pfTrxNo);
            txtFromDate.Text = ds.Tables[0].Rows[0]["FSDATE"].ToString();
            txtToDate.Text = ds.Tables[0].Rows[0]["FEDATE"].ToString();
            txtOB.Text = ds.Tables[0].Rows[0]["OB"].ToString();
            txtLoanOB.Text = ds.Tables[0].Rows[0]["LOANBAL"].ToString();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal.ShowPfTranByTrxNO()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
        Fsdate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PFFSDATE"]).ToString("dd/MM/yyyy");
        Fedate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PFFEDATE"]).ToString("dd/MM/yyyy");
        txtFromDate.Text = Fsdate;
        txtToDate.Text = Fedate;
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
                finDate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_pay_REF", "" + colname + "", string.Empty));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal.FinancialYearStartDateEndDate()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        return finDate;
    }

    #endregion
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlCollege.SelectedIndex > 0)
            {
                   FillEmployee();
            }   
        }
        catch (Exception ex)
        {
        }
    }
    private void FillEmployee( )
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
