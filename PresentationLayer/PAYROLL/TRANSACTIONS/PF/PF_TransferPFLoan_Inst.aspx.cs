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

public partial class PAYROLL_TRANSACTIONS_PF_PF_TransferPFLoan_Inst : System.Web.UI.Page
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

                BindListView();

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
                Response.Redirect("~/notauthorized.aspx?page=PF_TransferPFLoan_Inst.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_TransferPFLoan_Inst.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objPFController.GetAllScanctionLoans();

            if (ds.Tables[0].Rows.Count > 0)
            {
                butSubmit.Visible = true;
                butCancel.Visible = true;
            }
            else
            {
                butSubmit.Visible = false;
                butCancel.Visible = false;
            }

            lvSanctionLoan.DataSource = ds;
            lvSanctionLoan.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_PF_PF_LOAN_ENTRY.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (ListViewDataItem lvitem in lvSanctionLoan.Items)
            {
                CheckBox chk = lvitem.FindControl("ChkPFloan") as CheckBox;
                DataSet ds = null;
                HiddenField hdnlonno = lvitem.FindControl("hdnloanno") as HiddenField;
                int loanno = Convert.ToInt32(hdnlonno.Value);
                ds = objPFController.GetPfLoanByPFLNO(Convert.ToInt32(chk.ToolTip));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objpf.IDNO = Convert.ToInt32(ds.Tables[0].Rows[0]["idno"].ToString());
                    objpf.FDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["FDATE"].ToString());
                    objpf.TDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["TDATE"].ToString());
                    objpf.SANNO = ds.Tables[0].Rows[0]["SANNO"].ToString();
                    objpf.SANDT = Convert.ToDateTime(ds.Tables[0].Rows[0]["SANDT"].ToString());
                    objpf.SANCTION = "T";
                    objpf.SANAMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["SANAMT"].ToString());
                    objpf.CURSANAMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CURSANAMT"].ToString());
                    objpf.COLLEGE_CODE = Session["colcode"].ToString();
                    objpf.PFLOANTYPENO = Convert.ToInt32(ds.Tables[0].Rows[0]["PFLOANTYPENO"].ToString());
                    objpf.PFLTNO = Convert.ToInt32(ds.Tables[0].Rows[0]["PFLTNO"].ToString());
                    objpf.COLLEGE_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString());
                    CustomStatus cs = (CustomStatus)objPFController.UpdatePfLoan(objpf);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        this.InsertInstallment(ds.Tables[0].Rows[0],objpf.COLLEGE_NO);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void InsertInstallment(DataRow dr,int collegeno)
    {

        try
        {

            int PFLOANTYPENO = Convert.ToInt32(objCommon.LookUp("PAYROLL_PF_LOAN", "PFLOANTYPENO", "PFLTNO=" + Convert.ToInt32(dr["PFLTNO"].ToString())));
            int PFNO = Convert.ToInt32(objCommon.LookUp("PAYROLL_PF_LOAN_TYPE", "PFNO", "PFLOANTYPENO=" + PFLOANTYPENO));
            string PAYHEAD = objCommon.LookUp("PAYROLL_PF_MAST", "H3", "PFNO=" + PFNO);
            string PAYSHORT = objCommon.LookUp("PAYROLL_PAYHEAD", "PAYSHORT", "PAYHEAD='" + PAYHEAD + "'");
            Payroll objinstall = new Payroll();
            objinstall.IDNO = Convert.ToInt32(dr["idno"].ToString());
            objinstall.INSTALNO = Convert.ToInt32(dr["INSTALMENT"].ToString());
            objinstall.MONAMT = Convert.ToDecimal(dr["INSTAMT"].ToString());
            objinstall.TOTAMT = Convert.ToDecimal(dr["SANAMT"].ToString());
            objinstall.BAL_AMT = Convert.ToDecimal(dr["SANAMT"].ToString());
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
            objinstall.DEFA_AMT = Convert.ToDecimal(dr["SANAMT"].ToString());
            objinstall.PRO_AMT = null;
            objinstall.STOP1 = null;
            objinstall.REGULAR = false;
            objinstall.LTNO = Convert.ToInt32(dr["PFLTNO"].ToString());
            objinstall.REMARK = "Nothing";
            objinstall.COLLEGE_CODE = Session["colcode"].ToString();
            objinstall.COLLEGENO = collegeno;
            CustomStatus cs = (CustomStatus)objPFController.AddPFLoanInstallMent(objinstall);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListView();
                objCommon.DisplayMessage(UpdatePanel1, "Successfully transfer to installment", this);
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
}
