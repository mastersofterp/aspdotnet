//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Monthly_Installment_Entry.aspx                                                  
// CREATION DATE : 19-May-2009                                                        
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


public partial class PAYROLL_TRANSACTIONS_PayMedicalInsurance : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objpay = new PayController();

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                pnldeducationentry.Visible = false;

                //Filling the dropDown pay head
                this.FillDropDownPayHead();
                //this.FillEmployee();
                this.FillCollege();
                this.FillBank();
                this.FillBankPlace();
                //chkStop.BackColor = System.Drawing.Color.Red;
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
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Monthly_Installment_Entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Monthly_Installment_Entry.aspx");
        }
    }

    private void BindListView(Int32 idno, Int32 stop)
    {
        try
        {

            DataSet ds = objpay.GetStopInstallMent(idno, stop, "T");

            //if (ds.Tables[0].Rows.Count <= 0)
            //    dpPager.Visible = false;
            //else
            //    dpPager.Visible = true;

            lvMonthlyIstallment.DataSource = ds;
            lvMonthlyIstallment.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void EmpInfo(Int32 idno)
    {
        //PKG_PAY_INST_EMPINFO
        try
        {
            DataSet ds = objpay.GetIncreEmp(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                lblDepartment.Text = Convert.ToString(ds.Tables[0].Rows[0]["DEPT"]);
                lblDesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESIG"]);
                lblIdnoName.Text = Convert.ToString(ds.Tables[0].Rows[0]["ENAME"]);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.EmpInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSaveVari_Click(object sender, EventArgs e)
    {

        int count = 0;
        int Chkcount = 0;
        foreach (ListViewDataItem lvitem in lvMonthlyIstallment.Items)
        {
            Payroll objinstall = new Payroll();

            TextBox txtYearlyPre = lvitem.FindControl("txtYearlyPre") as TextBox;
            TextBox txtEMPRCONT = lvitem.FindControl("txtEMPRCONT") as TextBox;
            TextBox txtMONAMT = lvitem.FindControl("txtMONAMT") as TextBox;
            TextBox txtStartDate = lvitem.FindControl("txtStartDate") as TextBox;
            TextBox txtTotalEmpCon = lvitem.FindControl("txtTotalEmpCon") as TextBox;
            TextBox txtRemark = lvitem.FindControl("txtRemark") as TextBox;
            CheckBox chkIno = lvitem.FindControl("chkIno") as CheckBox;

            if (chkIno.Checked == true)
            {
                Chkcount = Chkcount + 1;
                objinstall.IDNO = Convert.ToInt32(txtYearlyPre.ToolTip);
                objinstall.INSTALNO = Convert.ToInt32(12);
                objinstall.MONAMT = Convert.ToDecimal(txtMONAMT.Text);
                objinstall.TOTAMT = Convert.ToDecimal(txtTotalEmpCon.Text);
                objinstall.BAL_AMT = Convert.ToDecimal(txtTotalEmpCon.Text);
                objinstall.PAYHEAD = "I3";
                objinstall.CODE = "MEDINS";
                objinstall.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
                objinstall.BANKCITYNO = Convert.ToInt32(ddlBankPlace.SelectedValue);
                objinstall.DRAWN_DATE = Convert.ToDateTime(txtStartDate.Text);

                objinstall.SUBHEADNO = null;

                objinstall.STOP = Convert.ToBoolean(0);

                if (!(txtStartDate.Text == string.Empty || txtStartDate.Text == "" || txtStartDate.Text == null))
                    objinstall.START_DT = Convert.ToDateTime(txtStartDate.Text);
                else
                    objinstall.START_DT = null;

                objinstall.EXPDT = Convert.ToDateTime(txtStartDate.Text).AddMonths(11);
                objinstall.PAIDNO = Convert.ToInt32(12);
                objinstall.MON = null;
                objinstall.NEW = Convert.ToBoolean(0);
                objinstall.ACCNO = "MD00";
                objinstall.REF_NO = null;
                objinstall.DESP_NO = null;
                objinstall.DESP_DT = null;
                objinstall.DEFA_AMT = Convert.ToDecimal(txtYearlyPre.Text);
                objinstall.PRO_AMT = null;
                objinstall.STOP1 = null;
                objinstall.REGULAR = Convert.ToBoolean(0);
                objinstall.LTNO = null;
                objinstall.REMARK = txtRemark.Text;
                objinstall.COLLEGE_CODE = Session["colcode"].ToString();

                //Collegeno and Staffno of employee
                int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "ISNULL(COLLEGE_NO,0)", "IDNO=" + Convert.ToInt32(txtYearlyPre.ToolTip)));
                int staffno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "ISNULL(STAFFNO,0)", "IDNO=" + Convert.ToInt32(txtYearlyPre.ToolTip)));
                objinstall.EMP_CONAMT = Convert.ToDecimal(txtEMPRCONT.Text);
                objinstall.COLLEGENO = collegeno;
                objinstall.STAFFNO = staffno;

                CustomStatus cs = (CustomStatus)objpay.AddMedicalInstallMent(objinstall);

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    count = count + 1;
                }
            }
        }
        if (count == 1)
        {
            objCommon.DisplayUserMessage(this.Page, "Record saved successfully!", this.Page);
        }
        if (Chkcount == 0)
        {
            objCommon.DisplayUserMessage(this.Page, "Please Select Atleast Single Record!", this.Page);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {//(int idno, int stop, int CollNo, int Satffno, DateTime FromDate, DateTime ToDate)
        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
        Fdate = Fdate.Substring(0, 10);

        string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text)));
        Ldate = Ldate.Substring(0, 10);

        DataSet ds = objpay.GetAllInstallMent(Convert.ToInt32(ddlEmployee.SelectedValue), 0, Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToDateTime(Fdate), Convert.ToDateTime(Ldate));

        //if (ds.Tables[0].Rows.Count <= 0)
        //    dpPager.Visible = false;
        //else
        //    dpPager.Visible = true;

        lvMonthlyIstallment.DataSource = ds;
        lvMonthlyIstallment.DataBind();

        foreach (ListViewDataItem lvitem in lvMonthlyIstallment.Items)
        {
            TextBox txtStartDate = lvitem.FindControl("txtStartDate") as TextBox;
            if (txtStartDate.Text == "")
            {
                txtStartDate.Text = txtFromDate.Text;
            }

        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlSelection.Visible = false;
        pnlList.Visible = false;
        pnlEmpDetails.Visible = true;
        pnldeducationentry.Visible = true;
        Clear();
        ViewState["action"] = "add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {

            Payroll objinstall = new Payroll();
            objinstall.IDNO = Convert.ToInt32(empno);
            objinstall.INSTALNO = Convert.ToInt32(txtNoofInstallMent.Text);
            objinstall.MONAMT = Convert.ToDecimal(txtMonthlyDedAmt.Text);
            objinstall.TOTAMT = Convert.ToDecimal(txtTotalAmount.Text);
            objinstall.BAL_AMT = Convert.ToDecimal(txtOutStandingAmt.Text);
            objinstall.PAYHEAD = ddlPayhead.SelectedValue;
            objinstall.CODE = ddlPayhead.SelectedItem.Text;
            objinstall.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
            objinstall.BANKCITYNO = Convert.ToInt32(ddlBankPlace.SelectedValue);
            objinstall.DRAWN_DATE = Convert.ToDateTime(txtInstallmentDrawnDate.Text);
            if (!(ddlSubpayhead.SelectedValue == "-1" || ddlSubpayhead.SelectedValue == "0" || ddlSubpayhead.SelectedValue == "" || ddlSubpayhead.SelectedValue == null))
                objinstall.SUBHEADNO = Convert.ToInt32(ddlSubpayhead.SelectedValue.ToString());
            else
                objinstall.SUBHEADNO = null;

            if (chkStop.Checked == true)
                objinstall.STOP = Convert.ToBoolean(1);
            else
                objinstall.STOP = Convert.ToBoolean(0);

            if (!(txtStartDate.Text == string.Empty || txtStartDate.Text == "" || txtStartDate.Text == null))
                objinstall.START_DT = Convert.ToDateTime(txtStartDate.Text);
            else
                objinstall.START_DT = null;

            objinstall.EXPDT = Convert.ToDateTime(txtExpiryDate.Text);
            objinstall.PAIDNO = Convert.ToInt32(txtNoofInstPaid.Text);
            objinstall.MON = null;
            objinstall.NEW = Convert.ToBoolean(0);
            objinstall.ACCNO = txtAccNo.Text;
            objinstall.REF_NO = null;
            objinstall.DESP_NO = null;
            objinstall.DESP_DT = null;
            objinstall.DEFA_AMT = Convert.ToDecimal(txtOutStandingAmt.Text);
            objinstall.PRO_AMT = null;
            objinstall.STOP1 = null;
            objinstall.REGULAR = Convert.ToBoolean(0);
            objinstall.LTNO = null;
            objinstall.REMARK = txtRemarks.Text;
            objinstall.COLLEGE_CODE = Session["colcode"].ToString();

            //Collegeno and Staffno of employee
            int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "ISNULL(COLLEGE_NO,0)", "IDNO=" + empno));
            int staffno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "ISNULL(STAFFNO,0)", "IDNO=" + empno));

            objinstall.COLLEGENO = collegeno;
            objinstall.STAFFNO = staffno;

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objpay.AddInstallMent(objinstall);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "";
                        visiblestatus();
                        BindListView(empno, stop());
                        //objCommon.DisplayMessage(UpdatePanel1, "Record saved successfully", this);
                        //Clear();                       
                    }
                }
                else
                {
                    if (ViewState["ino"] != null)
                    {
                        //objinstall.INO = Convert.ToInt32(ViewState["ino"].ToString());
                        CustomStatus cs = (CustomStatus)objpay.DeleteInstallMent(Convert.ToInt32(ViewState["ino"].ToString()));
                        if (cs.Equals(CustomStatus.RecordDeleted))
                        {
                            CustomStatus csadd = (CustomStatus)objpay.AddInstallMent(objinstall);
                            if (csadd.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["action"] = "";
                                visiblestatus();
                                BindListView(empno, stop());
                                //objCommon.DisplayMessage(UpdatePanel1, "Record saved successfully", this);
                            }

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["ino"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetails(Convert.ToInt32(ViewState["ino"].ToString()));
            visiblestatus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowEditDetails(int ino)
    {
        DataSet ds = null;

        try
        {
            ds = objpay.GetInstallMent(ino);
            if (ds.Tables[0].Rows.Count > 0)
            {

                int instPaidCount = Convert.ToInt32(objCommon.LookUp("PAYROLL_INSTALMENT", "PAIDNO", "INO=" + ino));
                EnabledDisabled(instPaidCount);

                ddlPayhead.Text = ds.Tables[0].Rows[0]["PAYHEAD"].ToString();
                string subHeadNo = ds.Tables[0].Rows[0]["SUBHEADNO"].ToString();
                FillSubPayHead();
                CheckLicRdField();
                if (subHeadNo != "")
                {
                    ddlSubpayhead.Text = ds.Tables[0].Rows[0]["SUBHEADNO"].ToString();
                }
                txtMonthlyDedAmt.Text = ds.Tables[0].Rows[0]["MONAMT"].ToString();
                txtNoofInstallMent.Text = ds.Tables[0].Rows[0]["INSTALNO"].ToString();
                txtNoofInstPaid.Text = ds.Tables[0].Rows[0]["PAIDNO"].ToString();
                txtOutStandingAmt.Text = ds.Tables[0].Rows[0]["BAL_AMT"].ToString();
                txtTotalAmount.Text = ds.Tables[0].Rows[0]["TOTAMT"].ToString();
                //Int32 amountPaidTillNow = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAMT"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["BAL_AMT"]);
                // txtAmountPaidTillNow.Text = Convert.ToString(amountPaidTillNow);
                txtAccNo.Text = ds.Tables[0].Rows[0]["ACCNO"].ToString();
                cetxtExpiryDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["expdt"].ToString());

                ddlBank.SelectedValue = ds.Tables[0].Rows[0]["BANKNO"].ToString();

                ddlBankPlace.SelectedValue = ds.Tables[0].Rows[0]["BANKCITYNO"].ToString();

                txtInstallmentDrawnDate.Text = ds.Tables[0].Rows[0]["DRAWN_DATE"].ToString();


                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STOP"].ToString()))
                    chkStop.Checked = true;
                else
                    chkStop.Checked = false;

                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["REGULAR"].ToString()))
                //    chkRegularDed.Checked = true;
                //else
                //    chkRegularDed.Checked = false;

                txtRemarks.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                cetxtStartDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["start_dt"].ToString());

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.ShowEditDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnDel = sender as ImageButton;
            int ino = int.Parse(btnDel.CommandArgument);
            int checkInstallmentStarted = Convert.ToInt32(objCommon.LookUp("PAYROLL_INSTALMENT", "COUNT(*)", "PAIDNO > 0 AND INO=" + ino + " AND REGULAR=0"));
            if (checkInstallmentStarted == 0)
            {
                CustomStatus cs = (CustomStatus)objpay.DeleteInstallMent(ino);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    visiblestatus();
                    BindListView(empno, stop());
                    //objCommon.DisplayMessage(UpdatePanel1,"Record deleted successfully", this);
                }
            }
            else
            {

                //objCommon.DisplayMessage(UpdatePanel1, "You can not delete the installment because installment is started" , this);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Earn_Dedu_Rule.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListView(empno, stop());
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "";
        Response.Redirect(Request.Url.ToString());
    }

    private void FillDropDownPayHead()
    {
        try
        {
            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE = 'E' AND (CAL_ON IS NULL or CAL_ON='') AND PAYSHORT IS NOT NULL AND SRNO > 15 AND PAYSHORT<>'' and PAYSHORT <> '-'", "PAYSHORT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCollege()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEmployee()
    {
        try
        {
            if (ddlorderby.SelectedValue == "1")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.PFILENO) +']'+ ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue+" AND (EM.STAFFNO=" + ddlStaff.SelectedValue+" OR " + ddlStaff.SelectedValue+"=0)", "EM.IDNO");

            if (ddlorderby.SelectedValue == "2")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL  AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue + " AND (EM.STAFFNO=" + ddlStaff.SelectedValue + " OR " + ddlStaff.SelectedValue + "=0)", "EM.FNAME,EM.MNAME,EM.LNAME");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           this.FillEmployee();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.ddlEmployee_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView(empno, stop());
            EmpInfo(empno);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.ddlEmployee_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private int stop()
    {
        Int32 stop;
        if (chkStopInstall.Checked == true)
            stop = 1;
        else
            stop = 0;
        return stop;
    }

    private void visiblestatus()
    {
        if (ViewState["action"].ToString() == "edit" || ViewState["action"].ToString() == "add")
        {
            pnlSelection.Visible = false;
            pnlList.Visible = false;
            pnlEmpDetails.Visible = true;
            pnldeducationentry.Visible = true;
        }
        else
        {
            pnlSelection.Visible = true;
            pnlList.Visible = true;
            pnlEmpDetails.Visible = false;
            pnldeducationentry.Visible = false;
            ViewState["action"] = "";
        }

    }

    protected void chkStopInstall_CheckedChanged(object sender, EventArgs e)
    {
        //BindListView(empno, stop());
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckLicRdField();
        this.FillSubPayHead();
    }

    private void FillSubPayHead()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubpayhead, "PAYROLL_PAY_SUBPAYHEAD", "SUBHEADNO", "SHORTNAME", "SHORTNAME IS NOT NULL AND PAYHEAD='" + ddlPayhead.SelectedValue + "'", "SUBHEADNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillSubPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillBank()
    {
        try
        {

            objCommon.FillDropDownList(ddlBank, "PAYROLL_BANK", "BANKNO", "BANKNAME+'['+BANKADDR+']' AS BANKNAME", "BANKNO>0", "BANKNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillBank-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillBankPlace()
    {
        try
        {

            objCommon.FillDropDownList(ddlBankPlace, "PAYROLL_CITY", "CITYNO", "CITY", "CITYNO>0", "");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillBankPlace-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckLicRdField()
    {
        string licField;
        string rdField;
        licField = objCommon.LookUp("PAYROLL_PAY_REF", "LIC_FIELD", "");
        rdField = objCommon.LookUp("PAYROLL_PAY_REF", "RD_FIELD", "");

        if (licField == ddlPayhead.SelectedValue)
        {
            tdPolicyno.Visible = true;
            tdRdNo.Visible = false;
            tdAccountNO.Visible = false;
        }
        else if (rdField == ddlPayhead.SelectedValue)
        {
            tdPolicyno.Visible = false;
            tdRdNo.Visible = true;
            tdAccountNO.Visible = false;
        }
        else
        {
            tdPolicyno.Visible = false;
            tdRdNo.Visible = false;
            tdAccountNO.Visible = true;
        }


    }

    protected string Monyear()
    {
        string monyear = string.Empty;
        try
        {
            int staffno = Convert.ToInt32(objCommon.LookUp("payroll_empmas", "staffno", "idno=" + empno));
            monyear = objCommon.LookUp("payroll_salfile", "monyear", "sallock=0 and staffno=" + staffno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.Monyear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return monyear;
    }

    protected void Clear()
    {
        txtAccNo.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        txtMonthlyDedAmt.Text = string.Empty;
        txtNoofInstallMent.Text = string.Empty;
        txtNoofInstPaid.Text = string.Empty;
        txtOutStandingAmt.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtTotalAmount.Text = string.Empty;
        ddlPayhead.SelectedValue = "0";
        ddlSubpayhead.SelectedValue = "0";
        ddlBank.SelectedValue = "0";
        ddlBankPlace.SelectedValue = "0";
        txtInstallmentDrawnDate.Text = string.Empty;
        // cetxtExpiryDate.s

    }

    protected void EnabledDisabled(int count)
    {
        if (count > 0)
        {
            ddlPayhead.Enabled = true;
            ddlSubpayhead.Enabled = true;
            txtMonthlyDedAmt.Enabled = true;
            txtNoofInstallMent.Enabled = true;
            txtAccNo.Enabled = true;
            txtExpiryDate.Enabled = true;
            txtStartDate.Enabled = true;
            txtRemarks.Enabled = true;
            imgCalExpiryDate.Visible = true;
            ImaCalStartDate.Visible = true;
            ddlBank.Enabled = true;
            ddlBankPlace.Enabled = true;
            txtInstallmentDrawnDate.Enabled = true;
        }
        else
        {
            ddlPayhead.Enabled = true;
            ddlSubpayhead.Enabled = true;
            txtMonthlyDedAmt.Enabled = true;
            txtNoofInstallMent.Enabled = true;
            txtAccNo.Enabled = true;
            txtExpiryDate.Enabled = true;
            txtStartDate.Enabled = true;
            txtRemarks.Enabled = true;
            imgCalExpiryDate.Visible = true;
            ImaCalStartDate.Visible = true;
            ddlBank.Enabled = true;
            ddlBankPlace.Enabled = true;
            txtInstallmentDrawnDate.Enabled = true;
        }
    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dt1 = Convert.ToDateTime(txtStartDate.Text);
        int totnoinst = Convert.ToInt32(txtNoofInstallMent.Text);
        DateTime dt2 = dt1.AddMonths(totnoinst - 1);

        DateTime startOfMonth = new DateTime(dt2.Year, dt2.Month, 1);
        DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
        txtExpiryDate.Text = Convert.ToDateTime(endOfMonth.ToString()).ToString();
    }
}