//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Monthly_Regular_deducation.aspx                                                  
// CREATION DATE : 09-09-2009                                                        
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

public partial class PayRoll_Pay_Monthly_Regular_deducation : System.Web.UI.Page
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Making panels visible
                pnlSelection.Visible = true;
                pnlList.Visible = true;
                btnBulkSave.Visible = false;
                btnCancle.Visible = false;

                //Making panels invisible 
                pnlEmpDetails.Visible = false;
                pnldeducationentry.Visible = false;

                //Filling the dropDown pay head
                this.FillDropDownPayHead();
               // FillEmployee();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Monthly_Regular_deducation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Monthly_Regular_deducation.aspx");
        }
    }

    private void BindListView(Int32 idno, Int32 stop)
    {
        try
        {
            DataSet ds = objpay.GetStopInstallMent(idno, stop,"R");


            if (ds.Tables[0].Rows.Count <= 0)
                dpPager.Visible = false;
            else
                dpPager.Visible = true;

            lvMonthlyIstallment.DataSource = ds;
            lvMonthlyIstallment.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSumMonAmount.Visible = true;
                div_footer.Visible = true;
                lblSumMonAmount.Text = ds.Tables[1].Rows[0]["TOTAMT"].ToString();
                btnBulkSave.Visible = true;
                btnCancle.Visible = true;
            }
            else
            {
                lblSumMonAmount.Visible = false;
                div_footer.Visible = false;
                btnBulkSave.Visible = false;
                btnCancle.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.BindListView-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.EmpInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            objinstall.INSTALNO = 0;
            objinstall.MONAMT = Convert.ToDecimal(txtMonthlyDedAmt.Text);
            objinstall.TOTAMT = 0;
            objinstall.BAL_AMT = 0;
            objinstall.PAYHEAD = ddlPayhead.SelectedValue;
            objinstall.CODE = ddlPayhead.SelectedItem.Text;
            if (!(ddlSubpayhead.SelectedValue == "-1" || ddlSubpayhead.SelectedValue == "0" || ddlSubpayhead.SelectedValue == "" || ddlSubpayhead.SelectedValue == null))
                objinstall.SUBHEADNO = Convert.ToInt32(ddlSubpayhead.SelectedValue.ToString());
            else
                objinstall.SUBHEADNO = null;

            if (chkStop.Checked == true)
                objinstall.STOP = Convert.ToBoolean(1);
            else
                objinstall.STOP = Convert.ToBoolean(0);

        
            objinstall.START_DT = null;
            objinstall.EXPDT = Convert.ToDateTime("01/01/1900");
            objinstall.PAIDNO =0;
            objinstall.MON = null;
            objinstall.NEW = Convert.ToBoolean(0);
            objinstall.ACCNO = txtAccNo.Text;
            objinstall.REF_NO = null;
            objinstall.DESP_NO = null;
            objinstall.DESP_DT = null;
            objinstall.DEFA_AMT = 0;
            objinstall.PRO_AMT = null;
            objinstall.STOP1 = null;
            objinstall.REGULAR = Convert.ToBoolean(1);
            objinstall.LTNO = null;
            objinstall.REMARK = txtRemarks.Text;
            objinstall.COLLEGE_CODE = Session["colcode"].ToString();
            objinstall.DRAWN_DATE = Convert.ToDateTime("01/01/1753");
            objinstall.BANKNO = Convert.ToInt32(ddlBank.SelectedValue);
            objinstall.BANKCITYNO = Convert.ToInt32(ddlBankPlace.SelectedValue);
            objinstall.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);

            int staffno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "ISNULL(STAFFNO,0)", "IDNO=" + empno));
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
                            }

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
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
                //EnabledDisabled(instPaidCount);
                ddlPayhead.Text = ds.Tables[0].Rows[0]["PAYHEAD"].ToString();
                string subHeadNo = ds.Tables[0].Rows[0]["SUBHEADNO"].ToString();
                FillSubPayHead();
                CheckLicRdField();
                if (subHeadNo != "")
                {
                    ddlSubpayhead.Text = ds.Tables[0].Rows[0]["SUBHEADNO"].ToString();
                }
                txtMonthlyDedAmt.Text = ds.Tables[0].Rows[0]["MONAMT"].ToString();
                txtAccNo.Text = ds.Tables[0].Rows[0]["ACCNO"].ToString();
                
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STOP"].ToString()))
                    chkStop.Checked = true;
                else
                    chkStop.Checked = false;

                ddlBank.SelectedValue = ds.Tables[0].Rows[0]["BANKNO"].ToString();
                ddlBankPlace.SelectedValue = ds.Tables[0].Rows[0]["BANKCITYNO"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
               
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.ShowEditDetails-> " + ex.Message + " " + ex.StackTrace);
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
            CustomStatus cs = (CustomStatus)objpay.DeleteInstallMent(ino);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                visiblestatus();
                BindListView(empno, stop());

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
            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE = 'E' AND CAL_ON IS NULL OR CAL_ON ='' AND SRNO>15 AND PAYSHORT IS NOT NULL AND PAYSHORT<>''", "PAYSHORT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void FillEmployee()
    //{
    //    try
    //    {
    //        if (ddlorderby.SelectedValue == "1")
    //            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO IN(" + Session["college_nos"] + ")", "EM.IDNO");

    //        if (ddlorderby.SelectedValue == "2")
    //            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO IN(" + Session["college_nos"] + ")", "EM.FNAME,EM.MNAME,EM.LNAME");

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

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
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.ddlEmployee_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
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
        BindListView(empno, stop());
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
                objUCommon.ShowError(Page, "Pay_Monthly_Regular_deducation.FillSubPayHead-> " + ex.Message + " " + ex.StackTrace);
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
            tdPolicyno.Visible = false;
            tdRdNo.Visible = false;
            tdAccountNO.Visible = true;
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
       
    protected void Clear()
    {
        txtAccNo.Text = string.Empty;    
        txtMonthlyDedAmt.Text = string.Empty;      
        txtRemarks.Text = string.Empty;       
        ddlPayhead.SelectedValue = "0";
        ddlSubpayhead.SelectedValue = "0";
        ddlBankPlace.SelectedValue = "0";
        ddlBank.SelectedValue = "0";


    }

    protected void EnabledDisabled(int count)
    {
        if (count > 0)
        {
            ddlPayhead.Enabled = false;
            ddlSubpayhead.Enabled = false;
            txtMonthlyDedAmt.Enabled = false;          
            txtAccNo.Enabled = false;           
            txtRemarks.Enabled = false;
            
        }
        else
        {
            ddlPayhead.Enabled = true;
            ddlSubpayhead.Enabled = true;
            txtMonthlyDedAmt.Enabled = true;
            txtAccNo.Enabled = true;
            txtRemarks.Enabled = true;
        }
    }

    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
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
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillEmployee();
    }
    private void FillEmployee()
    {
        try
        {
            if (ddlorderby.SelectedValue == "1")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ''+ CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");

            else if (ddlorderby.SelectedValue == "2")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.FNAME,EM.MNAME,EM.LNAME");

            else if (ddlorderby.SelectedValue == "3")                       
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ''+ CONVERT(NVARCHAR(20),EM.PFILENO) +'-'+ ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.PFILENO");
                       
            else if (ddlorderby.SelectedValue == "4")
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ''+ CONVERT(NVARCHAR(20),EM.SEQ_NO) +'-'+ ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.SEQ_NO");
            else
                objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ''+ CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCollege()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ") AND COLLEGE_NO>0", "COLLEGE_NO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    // 26 March 2018
    protected void btnBulkSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;


            foreach (ListViewDataItem lvitem in lvMonthlyIstallment.Items)
            {
                bool stop;
                CheckBox chkSelect = lvitem.FindControl("chkSelect") as CheckBox;
                TextBox txtAmount = lvitem.FindControl("txtMonAmt") as TextBox;


                if (chkSelect.Checked)
                {
                    stop = true;
                }
                else
                {
                    stop = false;
                }

                CustomStatus cs = (CustomStatus)objpay.UpdateRegularDed(Convert.ToInt32(chkSelect.ToolTip),txtAmount.Text, stop);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }
            if (count == 1)
            {
                Showmessage("Record Updated Successfully");
                //BindListViewList();

            }

            ddlEmployee_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //For Message Box
    private void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
   
}
