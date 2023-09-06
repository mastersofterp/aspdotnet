//==============================================
//MODIFIED BY: Swati Ghate
//MODIFIED DATE:10-11-2014
//PURPOSE: TO UPDATE THE DESIGN & DISPLAY STATUS DATA
//==============================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;
using System.Globalization;
public partial class ESTABLISHMENT_LEAVES_Transactions_OD_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnllist.Visible = true;
                // pnlBudget.Visible = false;
                //   pnlShowBudg.Visible = false;
                BindLVLeaveApplPendingList();
                btnHidePanel.Visible = false;
                //checkAuthority();
                // trfrmto.Visible = false;
                // trbutshow.Visible = false;
                pnlENtry.Visible = false;
                btnShow.Visible = false;
                txtFromdt.Text = System.DateTime.Now.ToString();
                txtTodt.Text = System.DateTime.Now.ToString();
                CheckPageAuthorization();

            }
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OD_Approval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OD_Approval.aspx");
        }
    }

    //To popup the messagebox.
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    /// <summary>
    /// 
    /// </summary>
    //private int checkAuthority()
    //{
    //    int count = 0;
    //    string pano1 = string.Empty;
    //    string pano2 = string.Empty;
    //    string userno = Session["userno"].ToString();
    //    DataSet dsdept = new DataSet();
    //    dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
    //    string dept = dsdept.Tables[0].Rows[0]["UA_DEPTNO"].ToString();
    //    string uafullname = dsdept.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
    //    ViewState["deptno"] = dept;
    //    ViewState["UA_FULLNAME"] = uafullname;

    //    DataSet dspath = new DataSet();
    //    dspath = objCommon.FillDropDown("PAYROLL_OD_PASSING_AUTHORITY_PATH", "*", "", "DEPTNO=" + dept, "");
    //    if (dspath.Tables[0].Rows.Count > 0)
    //    {
    //        ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();
    //        pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
    //        pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
    //    }

    //    DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "UA_NO=" + userno, "");
    //    string uano1 = string.Empty;
    //    if (dsauth1.Tables[0].Rows.Count > 0)
    //    {
    //        uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
    //        ViewState["UANO"] = userno.ToString();
    //        string paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();
    //    }

    //    count = dsauth1.Tables[0].Rows.Count;


    //    //DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
    //    //string uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
    //    //ViewState["uano2"] = uano2;
    //    if (userno == uano1)
    //    {
    //        btnBudget.Visible = true;
    //    }
    //    else
    //    {
    //        btnBudget.Visible = false;
    //    }
    //    return count;
    //}


    private void checkAuthority()
    {
        string pano1 = string.Empty;
        string pano2 = string.Empty;
        string userno = Session["userno"].ToString();
        DataSet dsdept = new DataSet();
        dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
        string dept = dsdept.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();
        string uafullname = dsdept.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
        ViewState["deptno"] = dept;
        ViewState["UA_FULLNAME"] = uafullname;

        DataSet dspath = new DataSet();
        dspath = objCommon.FillDropDown("PAYROLL_OD_PASSING_AUTHORITY_PATH", "*", "", "DEPTNO=" + dept, "");
        if (dspath.Tables[0].Rows.Count > 0)
        {
            ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();
            pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
            pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
        }

        DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
        string uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
        ViewState["UANO"] = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
        string paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();

        //DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
        //string uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
        //ViewState["uano2"] = uano2;
        if (userno == uano1)
        {
            //btnBudget.Visible = true;
        }
        else
        {
            //btnBudget.Visible = false;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    protected void BindLVLeaveApplPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforODApproval(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
               // dpPager.Visible = true;
            }
            lvPendingList.DataSource = ds;
            lvPendingList.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{

    //    BindLVLeaveApplPendingList();
    //}
    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        txtRegAmt.Text = txtTADAamt.Text = string.Empty;
        lblRegAmt.Text = lblTADAAmt.Text = string.Empty;
    }
    private void clear_lblvalue()
    {
        lblEmpName.Text = string.Empty;
        lblInstr.Text = string.Empty;
        lblInTime.Text = string.Empty;
        lblOutTime.Text = string.Empty;
        lblPlace.Text = string.Empty;
        lblPurpose.Text = string.Empty;
        lblFromDate.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        //  pnlShowBudg.Visible = false;
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
        lnkbut.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            double tadaAmount = 0;
            double modRegAmt = 0;
            int ODTRNO = Convert.ToInt32(ViewState["ODTRNO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
            string Remarks = txtRemarks.Text.ToString();
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
            if (txtTADAamt.Text.Equals(string.Empty))
            {
                tadaAmount = 0;
            }
            else
            {
                tadaAmount = Convert.ToDouble(txtTADAamt.Text);
            }
            if (txtRegAmt.Text.Equals(string.Empty))
            {
                modRegAmt = 0;
            }
            else
            {
                modRegAmt = Convert.ToDouble(txtRegAmt.Text);
            }


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    CustomStatus cs = (CustomStatus)objApp.UpdateODAppPassEntry(ODTRNO, UA_NO, Status, Remarks, Aprdate, 0);


                    CustomStatus cs2 = (CustomStatus)objApp.UpdateODAppEntry(ODTRNO, tadaAmount, modRegAmt);


                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        MessageBox("Record Updated Successfully");
                        pnllist.Visible = true;
                        BindLVLeaveApplPendingList();
                        pnlAdd.Visible = false;
                        ViewState["action"] = null;
                        clear_lblvalue();
                        clear();
                        lnkbut.Visible = true;
                        //  pnlShowBudg.Visible = false;
                    }
                    // string UANO = ViewState["UANO"].ToString();
                    //Session["userno"]
                    //  string UANO = Session["userno"].ToString();//swati 
                    //string user = Session["userno"].ToString();
                    //string uano2 = ViewState["UANO2"].ToString();
                    //int deptno = Convert.ToInt32(ViewState["deptno"]);
                    //double regamt =Convert.ToDouble(ViewState["regamt"]);
                    //int tadaamt =Convert.ToInt32( ViewState["tADAamt"]);
                    //if (UANO == user)
                    //{
                    //    if (regamt == 0.00 && tadaamt == 0)
                    //    {
                    //        SkipAccountApproval();
                    //    }


                    //}
                    //if (uano2 == user)
                    //{
                    //    DataSet dsodbudg = objCommon.FillDropDown("PAYROLL_OD_BUDGET_ENTRY", "*", "USER", "DEPTNO="+deptno, "");
                    //    double budgallot = Convert.ToDouble (dsodbudg.Tables[0].Rows[0]["BUDG_ALLOT"]);
                    //    double budgutil = Convert.ToDouble(dsodbudg.Tables[0].Rows[0]["BUDG_UTILIZED"]);
                    //    budgutil = budgutil + regamt;

                    //    double budgbal = Convert.ToDouble(dsodbudg.Tables[0].Rows[0]["BUDG_BAL"]);
                    //    budgbal = budgallot - budgutil;
                    //    CustomStatus cs1 = (CustomStatus)objApp.UpdateODBudget(deptno, budgallot, budgutil, budgbal);
                    //}
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int ODTRNO = int.Parse(btnApproval.CommandArgument);
            ShowDetails(ODTRNO);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            // pnlShowBudg.Visible = false ;
            ViewState["action"] = "edit";
            lnkbut.Visible = false;
            // GetBudgetEmp();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void SkipAccountApproval()
    {
        int ODTRNO = Convert.ToInt32(ViewState["ODTRNO"].ToString());
        string Status = "A";
        string Remarks = "Approved";
        DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
        int idno = Convert.ToInt32(hidIdno.Value);
        int deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + idno));
        DataSet dspano = objCommon.FillDropDown("PAYROLL_OD_PASSING_AUTHORITY_PATH", "*", "PAN02", "DEPTNO=" + deptno, "");
        int pano1 = Convert.ToInt32(dspano.Tables[0].Rows[0]["PAN01"]);
        string uano = objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + pano1);

        int pano2 = Convert.ToInt32(dspano.Tables[0].Rows[0]["PAN02"]);

        int pano3 = Convert.ToInt32(dspano.Tables[0].Rows[0]["PAN03"]);
        CustomStatus cs = (CustomStatus)objApp.UpdateODAppPassEntryForAccountant(ODTRNO, Status, Remarks, Aprdate, pano2, pano3);


    }

    private void ShowDetails(Int32 ODTRNO)
    {
        DataSet ds = new DataSet();

        try
        {
            int UA_NO = Convert.ToInt32(Session["userno"]);
            ds = objApp.GetODApplDetail(ODTRNO);
            //ds.Tables[1] = objApp.GetLeaveApplStatus(LETRNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ODTRNO"] = ODTRNO;
                //
                string odType = ds.Tables[0].Rows[0]["ODTYPE"].ToString();
                if (odType == "ODS")
                {
                    trTime.Visible = true;
                }
                else
                {
                    trTime.Visible = false;
                }
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                lblInstr.Text = ds.Tables[0].Rows[0]["INSTRUCTED_BY"].ToString();
                lblInTime.Text = ds.Tables[0].Rows[0]["IN_TIME"].ToString();
                DateTime dtfrom = Convert.ToDateTime(ds.Tables[0].Rows[0]["FROM_DATE"]);
                lblFromDate.Text = dtfrom.ToShortDateString();

                DateTime dtTodate = Convert.ToDateTime(ds.Tables[0].Rows[0]["TO_DATE"]);
                lblToDate.Text = dtTodate.ToShortDateString();
                lblOutTime.Text = ds.Tables[0].Rows[0]["OUT_TIME"].ToString();
                lblPlace.Text = ds.Tables[0].Rows[0]["PLACE"].ToString();
                lblPurpose.Text = ds.Tables[0].Rows[0]["PURPOSE"].ToString();
                lblEvent.Text = ds.Tables[0].Rows[0]["PROGRAM_TYPE"].ToString();
                lblTopic.Text = ds.Tables[0].Rows[0]["TOPIC"].ToString();

                lblRegAmt.Text = ds.Tables[0].Rows[0]["REG_AMT"].ToString();
                ViewState["regamt"] = ds.Tables[0].Rows[0]["REG_AMT"].ToString();
                lblTADAAmt.Text = ds.Tables[0].Rows[0]["TADA_AMT"].ToString();
                ViewState["tADAamt"] = ds.Tables[0].Rows[0]["TADA_AMT"].ToString();
                hidIdno.Value = ds.Tables[0].Rows[0]["EMPNO"].ToString();


                string filename = ds.Tables[0].Rows[0]["filename"].ToString();
                ViewState["filename"] = filename.ToString();

                int idno = Convert.ToInt32(hidIdno.Value);

                if (filename != string.Empty)
                {
                    lvDownload.DataSource = ds.Tables[0];
                    lvDownload.DataBind();
                    GetFileNamePath(filename, ODTRNO, idno);//Screen shot-swati.png
                    divDocument.Visible = true;
                }
                else
                {
                    divDocument.Visible = false;
                }
                

                int deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + idno));
                ViewState["deptno"] = deptno.ToString();
                DataSet dspano = objCommon.FillDropDown("PAYROLL_OD_PASSING_AUTHORITY_PATH", "*", "PAN02", "DEPTNO=" + deptno, "");
                int pano1 = Convert.ToInt32(dspano.Tables[0].Rows[0]["PAN01"]);
                int pano2 = Convert.ToInt32(dspano.Tables[0].Rows[0]["PAN02"]);
                string uano2 = objCommon.LookUp("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO=" + pano1);
                ViewState["UANO2"] = uano2;
                int pano3 = Convert.ToInt32(dspano.Tables[0].Rows[0]["PAN03"]);

                if (Convert.ToInt32(uano2) == UA_NO)
                {
                    int tadaamt = Convert.ToInt32(ViewState["tADAamt"]);
                    if (tadaamt == 1)
                    {
                        //trtada.Visible = true;
                        txtTADAamt.Enabled = true;
                    }
                    else
                    {
                        txtTADAamt.Enabled = false;
                    }
                }
                else
                {
                    // trtada.Visible = false;
                    txtTADAamt.Enabled = false;
                }
            }
            //pnlODStatusList.Visible = true;
            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    //protected void btnBudget_Click(object sender, EventArgs e)
    //{
    //   // pnlBudget.Visible = true;
    //    pnllist.Visible = false;
    //    GetBudget();
    //}

    //private void GetBudget()
    //{
    //    DataSet dsbudg = null;
    //    dsbudg = objApp.GetBudget(Convert.ToInt32(ViewState["deptno"]));
    //    txtBudgetAllot.Text = dsbudg.Tables[0].Rows[0]["BUDG_ALLOT"].ToString();
    //    txtBudgetUtil.Text = dsbudg.Tables[0].Rows[0]["BUDG_UTILIZED"].ToString();
    //    txtBudgetBal.Text = dsbudg.Tables[0].Rows[0]["BUDG_BAL"].ToString();
    //}

    private void GetBudgetEmp()
    {
        DataSet dsbudg = null;
        int deptno = 0;
        foreach (ListViewDataItem lvItem in lvPendingList.Items)
        {
            HiddenField hidempno = lvItem.FindControl("hidEmpno") as HiddenField;
            int empno = Convert.ToInt32(hidempno.Value);
            deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + empno));
        }
        dsbudg = objApp.GetBudget(deptno);
        //lblBudgAllot.Text = dsbudg.Tables[0].Rows[0]["BUDG_ALLOT"].ToString();
        //lblBudgUtil.Text = dsbudg.Tables[0].Rows[0]["BUDG_UTILIZED"].ToString();
        //lblBudgBal.Text = dsbudg.Tables[0].Rows[0]["BUDG_BAL"].ToString();

    }
    protected void btnBackBudget_Click(object sender, EventArgs e)
    {
        // pnlBudget.Visible = false;
        pnllist.Visible = true;
    }
    //protected void btnSaveBudget_Click(object sender, EventArgs e)
    //{
    //    int deptno = Convert.ToInt32(ViewState["deptno"]);
    //    double budgallot = Convert.ToDouble(txtBudgetAllot .Text);
    //    double budgutil = Convert.ToDouble(txtBudgetUtil.Text);
    //    double budgbal = Convert.ToDouble(txtBudgetBal.Text);
    //    string user = ViewState["UA_FULLNAME"].ToString();
    //    DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
    //    string collegecode = Session["colcode"].ToString();
    //    CustomStatus cs = (CustomStatus)objApp.AddODBudget(deptno,budgallot ,budgutil ,budgbal ,user,Aprdate ,collegecode);
    //    if (cs.Equals(CustomStatus.RecordSaved))
    //    {
    //        objCommon.DisplayMessage("Record Saved Successfully", this);

    //    }
    //}


    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = false;

        pnlAdd.Visible = false;
        pnllist.Visible = true;
        //  trfrmto.Visible = false;
        // trbutshow.Visible = false;
        pnlENtry.Visible = false;
        btnShow.Visible = false;
        lnkbut.Visible = true;
    }

    protected void lnkbut_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        // trfrmto.Visible = true ;
        // trbutshow.Visible = true;
        pnlENtry.Visible = true;
        btnShow.Visible = true;
        lnkbut.Visible = false;
        this.BindLVLeaveApprStatusAll();
    }

    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            DataSet ds = objApp.GetPendListforODApprovalStatusALL(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
               // dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void BindLVLeaveApprStatusParticular()
    {
        try
        {
            //DateTime DT = Convert.ToDateTime (txtFromdt.Text);
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);


            DataSet ds = objApp.GetPendListforODApprovalStatusParticular(Convert.ToInt32(Session["userno"]), Fdate, Tdate);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object ODTRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/LEAVE_CERTIFICATE_DOCUMENT/" + idno.ToString() + "/ODTRNO_" + ODTRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVLeaveApprStatusParticular();
    }

    protected void txtTADAamt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtTodt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtTodt.Text != string.Empty && txtTodt.Text != "__/__/____" && txtFromdt.Text != string.Empty && txtFromdt.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromdt.Text.ToString());

                DtTo = Convert.ToDateTime(txtTodt.Text.ToString());

                if (DtFrom > DtTo)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtTodt.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtTodt.Text = string.Empty;
        }
    }
}
