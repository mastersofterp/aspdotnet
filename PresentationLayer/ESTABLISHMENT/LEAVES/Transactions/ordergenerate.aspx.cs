using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;



public partial class ESTABLISHMENT_LEAVES_Transactions_ordergenerate : System.Web.UI.Page
{
    string date = "";
    int counter = 0;
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
        int IDNO = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);
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

                if (ua_type != 1)
                {
                    pnllist.Visible = false;
                    pnlAdd.Visible = false;
                    pnlOrder.Visible = true;
                    btnbac.Visible = false;
                    objCommon.FillDropDownList(ddlEname, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') as ENAME", "IDNO=" + IDNO + "", "IDNO");
                    ddlEname.SelectedIndex = 1;
                    fillorder(Convert.ToInt32(ddlEname.SelectedValue));
                }
                else
                {
                    pnlAdd.Visible = false;
                    pnllist.Visible = true;
                }
                //FillLeave();
                ViewState["action"] = "add";
                FillUser();
                FillCollege();
                FillStaffType();
                bindOrderNo();
                BindLVLeaveApplApprovedList();
                ViewState["rblLeaveOD"] = "0";
                CheckPageAuthorization();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ordergenerate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ordergenerate.aspx");
        }
    }
    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
            objCommon.FillDropDownList(ddlstafftype2, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        objCommon.FillDropDownList(ddlcollege2, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

    }
    private void bindOrderNo()
    {
        int type = 0;
        if (rblLeaveOD.SelectedValue == "1")
        {

            type = 1;
        }
        else if (rblLeaveOD.SelectedValue == "0")
        {
            type = 0;
        }
        DataSet ds = objApp.GetOrderNO(type);
        txtOrderNo.Text = ds.Tables[0].Rows[0]["ORDERNO"].ToString();
    }
    private void FillUser()
    {
        try
        {
            objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') as ENAME", "", "IDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void fillorder(int empno)
    {
        try
        {
            int rbllod = Convert.ToInt32(ViewState["rblLeaveOD"]);
            if (rbllod == 0)
                objCommon.FillDropDownList(ddlOrder, "PAYROLL_LEAVE_APP_ENTRY L,payroll_leave_ORDER_ENTRY O", "distinct(L.ordtrno)", "O.ORDNO", "LEAVETYPE=" + Convert.ToInt32(ViewState["rblLeaveOD"]) + "and L.ORDTRNO = O.ORDTRNO and L.ordtrno is not null AND EMPNO=" + empno, "L.ordtrno");
            else if (rbllod == 1)
                objCommon.FillDropDownList(ddlOrder, "PAYROLL_OD_APP_ENTRY L,payroll_leave_ORDER_ENTRY O", "distinct(L.ordtrno)", "O.ORDNO", "LEAVETYPE=" + Convert.ToInt32(ViewState["rblLeaveOD"]) + "and L.ORDTRNO = O.ORDTRNO and L.ordtrno is not null AND EMPNO=" + empno, "L.ordtrno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.FilloRDER ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindLVLeaveApplApprovedList()
    {
        try
        {
            //            DataSet ds = objApp.GetLvApplApprovedListforOrder(Convert.ToInt32(ddlCollege.SelectedValue));
            DataSet ds = objApp.GetLvApplApprovedListforOrder(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                // dpPager.Visible = false;
            }
            else
            {
                // dpPager.Visible = true;
            }
            lvPendingList.DataSource = ds.Tables[0];
            lvPendingList.DataBind();

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.BindLVLeaveApplApprovedList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void BindODLeaveApplApprovedList()
    {
        try
        {
            DataSet ds = objApp.GetODApplApprovedListforOrder(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffType.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                // dpPager.Visible = false;
            }
            else
            {
                // dpPager.Visible = true;
            }
            lvPendingList.DataSource = ds.Tables[0];
            lvPendingList.DataBind();

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.BindLVLeaveApplApprovedList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void rblLeaveOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblLeaveOD.SelectedValue == "1")
        {
            //ddlEname.SelectedIndex = 0;
            ddlOrder.SelectedIndex = 0;
            txtText.Text = string.Empty;
            // this.BindODLeaveApplApprovedList();
            BindODLeaveApplApprovedList();
            ViewState["rblLeaveOD"] = "1";
            fillorder(Convert.ToInt32(ddlEname.SelectedValue));
        }
        else if (rblLeaveOD.SelectedValue == "0")
        {
            // ddlEname.SelectedIndex = 0;
            ddlOrder.SelectedIndex = 0;
            txtText.Text = string.Empty;
            this.BindLVLeaveApplApprovedList();
            ViewState["rblLeaveOD"] = "0";
            fillorder(Convert.ToInt32(ddlEname.SelectedValue));

        }
    }

    protected void btnOrder_Click(object sender, EventArgs e)
    {
        try
        {

            int checkcount = 0;
            foreach (ListViewDataItem lvitem in lvPendingList.Items)
            {
                CheckBox chk = lvitem.FindControl("chkSelect") as CheckBox;
                if (chk.Checked)
                {
                    checkcount += 1;
                }

            }

            if (checkcount == 0)
            {
                MessageBox("Please Select Employee to generate order");
                return;
            }

            //if (checkcount > 1)
            //{
            //    MessageBox("Please Select Only Single Payhead");
            //    return;
            //}

            string ordno = Convert.ToString(txtOrderNo.Text);
            DateTime dt = Convert.ToDateTime(txtOrderDt.Text);
            int ordtype = Convert.ToInt32(ddltype.SelectedValue);
            int leavetype = Convert.ToInt32(rblLeaveOD.SelectedValue);
            string user = Convert.ToString(Session["username"].ToString());
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    for (int i = 0; i < lvPendingList.Items.Count; )
                    {

                        CheckBox chk = (CheckBox)lvPendingList.Items[i].FindControl("chkSelect");
                        if (chk.Checked)
                        {
                            CustomStatus cs = (CustomStatus)objApp.ORDER_ENTRY(ordno, dt, ordtype, user, leavetype);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                int odrtrno = Convert.ToInt32(objCommon.LookUp("payroll_leave_order_entry", "ORDTRNO", "ordno='" + ordno + "'"));
                                int letrno = Convert.ToInt32(chk.ToolTip);
                                if (rblLeaveOD.SelectedValue == "0")
                                {
                                    CustomStatus cs1 = (CustomStatus)objApp.UpdateAppEntry_order(odrtrno, letrno, dt);
                                    //CustomStatus cs3 = (CustomStatus)objApp.AddLeaveTran(letrno.ToString(), odrtrno.ToString(), dt);
                                }
                                else if (rblLeaveOD.SelectedValue == "1")
                                {
                                    CustomStatus cs2 = (CustomStatus)objApp.UpdateODEntry_order(odrtrno, letrno);
                                }
                                bindOrderNo();
                                ordno = Convert.ToString(txtOrderNo.Text);

                            }
                        }
                        i++;
                        ViewState["action"] = "add";


                        //objCommon.DisplayMessage("Record Saved Sucessfully", this);
                        MessageBox("Record Saved Successfully");
                    }
                }
            }
            Clear();
            BindLVLeaveApplApprovedList();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnOrder_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Visible = true;
            pnllist.Visible = false;
            ddlEmp.Enabled = true;
            ViewState["action"] = "add";
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnAdd_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }
    private void Clear()
    {
        ViewState["COLLEGE_NO"] = null; ViewState["SESSION_SRNO"] = null;
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        txtJoindt.Text = string.Empty;
        ddlNoon.SelectedIndex = 0;
        txtReason.Text = string.Empty;
        chkUFit.Checked = false;
        ChkFit.Checked = false;
        txtNodays.Text = string.Empty;
        txtLeavebal.Text = string.Empty;
        ddlEmp.SelectedIndex = 0;
        ddlLName.SelectedIndex = 0;
        
        //fillorder();
        bindOrderNo();

    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt;
            date = Convert.ToDateTime(txtFromdt.Text).ToString("dd/MM/yyyy");
            dt = findfix(Convert.ToDateTime(txtFromdt.Text), -1);
            int PrefixDays = counter;
            string prefix;
            if (PrefixDays > 0)
                prefix = date.Remove(0, 11);
            else
                prefix = "";
            counter = 0;
            date = Convert.ToDateTime(txtTodt.Text).ToString("dd/MM/yyyy");
            dt = findfix(Convert.ToDateTime(txtTodt.Text), 1);
            int SuffixDays = counter;
            string suffix;
            if (SuffixDays > 0)
                suffix = date.Remove(0, 11);
            else
                suffix = "";
            Boolean fit = false;
            if (ChkFit.Checked) fit = true;
            Boolean Unfit = false;
            if (chkUFit.Checked) Unfit = true;
            Boolean noonbit = false;
            if (ddlNoon.SelectedValue == Convert.ToString(1))
                noonbit = true;

            Leaves objLeaves = new Leaves();
            objLeaves.LNO = Convert.ToInt32(ViewState["LNO"].ToString());
            objLeaves.EMPNO = Convert.ToInt32(ddlEmp.SelectedValue);//Session["idno"]
            objLeaves.APPDT = Convert.ToDateTime(DateTime.Now.Date);
            if (!txtFromdt.Text.Trim().Equals(string.Empty)) objLeaves.FROMDT = Convert.ToDateTime(txtFromdt.Text);
            if (!txtTodt.Text.Trim().Equals(string.Empty)) objLeaves.TODT = Convert.ToDateTime(txtTodt.Text);
            objLeaves.NO_DAYS = Convert.ToDouble(txtNodays.Text);
            if (!txtJoindt.Text.Trim().Equals(string.Empty)) objLeaves.JOINDT = Convert.ToDateTime(txtJoindt.Text);
            objLeaves.FIT = fit;
            objLeaves.UNFIT = Unfit;
            objLeaves.FNAN = noonbit;
            objLeaves.PERIOD = Convert.ToInt32(3);
            objLeaves.STATUS = "A";
            objLeaves.ADDRESS = "";
            objLeaves.REASON = Convert.ToString(txtReason.Text);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeaves.PREFIX = PrefixDays;
            objLeaves.SUFFIX = SuffixDays;
            objLeaves.PAPNO = 0;
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objApp.AddORDERAPP_ENTRY(objLeaves, prefix, suffix, Convert.ToInt32(Session["RNO"]));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        pnlAdd.Visible = false;
                        pnllist.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["LETRN0"] != null)
                    {
                        objLeaves.LETRNO = Convert.ToInt32(ViewState["LETRN0"].ToString());
                        CustomStatus cs = (CustomStatus)objApp.UpdateOrderAppEntry(objLeaves);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;
                            pnllist.Visible = true;


                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
                BindLVLeaveApplApprovedList();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        pnlSelection.Visible = true;
        ViewState["action"] = null;
        Clear();
    }

    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____" && txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____")
            // calcdiff();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 LETRNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objApp.DeleteLeaveAppEntry(LETRNO ,Convert.ToInt32(Session["userno"]));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Int32 LETRNO = int.Parse(btnEdit.CommandArgument);

            ShowDetails(LETRNO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnllist.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }
    private void FillLeave(int EmpId)
    {
        try
        {

            DataSet ds = objApp.FillLeaveName(EmpId);
            ddlLName.Items.Clear();
            ddlLName.Items.Add("Please Select");
            ddlLName.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlLName.DataSource = ds;
                ddlLName.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlLName.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlLName.DataBind();
                ddlLName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ShowDetails(int LETRNo)
    {
        DataSet ds = null;
        try
        {

            ds = objApp.GetLeaveAppEntryByNO(LETRNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["LETRN0"] = LETRNo;
                ddlEmp.SelectedValue = ds.Tables[0].Rows[0]["EMPNO"].ToString();
                FillLeave(Convert.ToInt32(ddlEmp.SelectedValue));
                ddlLName.SelectedValue = ds.Tables[0].Rows[0]["LNO"].ToString();
                txtFromdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                txtTodt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                txtNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ViewState["SESSION_SRNO"] = ds.Tables[0].Rows[0]["SESSION_SRNO"].ToString();

                ddlNoon.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["FNAN"]);
                txtReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["FIT"]) == true)
                    ChkFit.Checked = true;
                else
                    ChkFit.Checked = false;

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["UNFIT"]) == true)
                    chkUFit.Checked = true;
                else
                    chkUFit.Checked = false;

                int ENo = Convert.ToInt32(ddlEmp.SelectedValue);
                int YR = Convert.ToDateTime(txtFromdt.Text).Year;
                int Lno = Convert.ToInt32(ddlLName.SelectedValue);
                FetchBalleave(ENo, YR, Lno);
                ddlEmp.Enabled = false;

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void FetchBalleave(int Eno, int Year, int Lno)
    {
        DataSet ds = null;
        try
        {
            ds = objApp.GetLeavesStatus(Eno, Year, Lno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtLeavebal.Text = ds.Tables[0].Rows[0]["bal"].ToString();
            }
            else
            {
                txtLeavebal.Text = Convert.ToString(0); ;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.FetchBalleave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{

    //    if (rblLeaveOD.SelectedValue == "1")
    //    {
    //        this.BindODLeaveApplApprovedList();
    //    }
    //    else if (rblLeaveOD.SelectedValue == "0")
    //    {
    //        this.BindLVLeaveApplApprovedList();
    //    }
    //}
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlLName.SelectedIndex = 0;

        FillLeave(Convert.ToInt32(ddlEmp.SelectedValue));
        txtLeavebal.Text = string.Empty;

    }
    protected void ddlLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLName.SelectedIndex > 0)
        {
            int ENo = Convert.ToInt32(ddlEmp.SelectedValue);
            int YR = DateTime.Now.Year;
            int Lno = Convert.ToInt32(ddlLName.SelectedValue);
            FetchBalleave(ENo, YR, Lno);
            ViewState["LNO"] = Convert.ToInt32(ddlLName.SelectedValue);
        }
    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLVLeaveApplApprovedList();
    }
    protected void txtNodays_TextChanged(object sender, EventArgs e)
    {
        if (txtNodays.Text.ToString() != string.Empty)
        {
            Chkvalid();
        }
    }
    protected void Chkvalid()
    {
        if (txtLeavebal.Text != string.Empty && txtNodays.Text != string.Empty)
        {
            if (Convert.ToDouble(txtLeavebal.Text) <= 0)
            {
                objCommon.DisplayMessage("Leave Days Can not be 0 or leass than 0", this);

                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();
            }
            if (Convert.ToDouble(txtLeavebal.Text) < Convert.ToDouble(txtNodays.Text))
            {

                objCommon.DisplayMessage("Leave Days Can not be greater than Balance Days", this);
                txtTodt.Text = string.Empty;
                txtNodays.Text = string.Empty;
                txtTodt.Focus();

            }
            else
            {
                txtNodays.Focus();
            }
        }

    }
    protected void txtOrderDt_TextChanged(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReportIndividual(string reportTitle, string rptFileName)
    {
        try
        {
            string text = string.Empty;
            if (ViewState["rblLeaveOD"].ToString() == "1")
            {
                text = txtText.Text.Replace(",", "$");
            }
            string name = string.Empty;

            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(ddlEname.SelectedValue) + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue) + "," + "@P_TEXT=" + text + "," + "@username=" + name;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ddlcollege2.SelectedValue) + "," + "@P_EMPNO=" + Convert.ToInt32(ddlEname.SelectedValue) + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue) + "," + "@P_TEXT=" + text + "," + "@username=" + name;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        pnlSelection.Visible = false;
        pnlOrder.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        ViewState["rblLeaveOD"] = rblLeaveOD.SelectedValue;
        if (rblLeaveOD.SelectedValue == "0")
        {
            trtext.Visible = false;
        }
        else if (rblLeaveOD.SelectedValue == "1")
        {
            trtext.Visible = true;
        }
        fillemployee();
    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        ShowReport("Order", "orderGroup.rpt");
    }
    //ShowReportIndividual("ODorder", "ESTB_ODOfficeOrder.rpt");
    protected void btnInd_Click(object sender, EventArgs e)
    {
        DataSet dsrpt = null; string rptname = string.Empty;
        dsrpt = objCommon.FillDropDown("PAYROLL_LEAVE_ORDERGENERATION_REPORT_FORMAT", "SRNO", "REPORTNAME", "", "");
        if (dsrpt != null && dsrpt.Tables[0].Rows.Count > 0)
        {
            rptname = dsrpt.Tables[0].Rows[0]["REPORTNAME"].ToString();
        }
        else
        {
            rptname = "OfficeOrder.rpt";
        }


        if (ViewState["rblLeaveOD"].ToString() == "0")
            if (rptname != string.Empty)
            {
               // ShowReportOrderGen("Leaveorder", rptname);
                ShowReportIndividual("Leaveorder", rptname);
            }
            else
            {
                ShowReportIndividual("Leaveorder", "OfficeOrder.rpt");
            }
        else if (ViewState["rblLeaveOD"].ToString() == "1")
            ShowReportIndividual("ODorder", "ESTB_Outdoor_Duty_Order.rpt");
    }
    protected void btnbac_Click(object sender, EventArgs e)
    {
        ddlcollege2.SelectedIndex = 0;
        ddlstafftype2.SelectedIndex = 0;
        ddlOrder.SelectedIndex = 0;
        ddlEname.SelectedIndex = 0;
        pnllist.Visible = true;
        pnlOrder.Visible = false;
        pnlAdd.Visible = false;
        pnlSelection.Visible = true;
    }
    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillemp(Convert.ToInt32(ddlOrder.SelectedValue));
    }

    protected void fillemployee()
    {

        if (ViewState["rblLeaveOD"].ToString() == "0")
            // objCommon.FillDropDownList(ddlEname, "payroll_leave_app_entry E,PAYROLL_EMPMAS EI", "distinct(empno)", "(Fname+' '+ mname+' '+ lname)as Ename", "EI.IDNO= E.EMPNO", "empno");
            objCommon.FillDropDownList(ddlEname, " PAYROLL_LEAVE_APP_ENTRY E INNER JOIN PAYROLL_EMPMAS EI  ON (EI.IDNO= E.EMPNO)INNER JOIN  PAYROLL_STAFFTYPE S ON (S.STNO = EI.STNO)INNER JOIN ACD_COLLEGE_MASTER CC ON (CC.COLLEGE_ID=EI.COLLEGE_NO)", "DISTINCT(EMPNO)", "(FNAME+' '+ MNAME+' '+ LNAME)AS ENAME", " (EI.STNO=" + Convert.ToInt32(ddlstafftype2.SelectedValue) + "OR  EI.STNO = 0)" + "AND (CC.COLLEGE_ID =" + Convert.ToInt32(ddlcollege2.SelectedValue) + "OR CC.COLLEGE_ID = 0)", "");
        else if (ViewState["rblLeaveOD"].ToString() == "1")
            //  objCommon.FillDropDownList(ddlEname, "payroll_OD_app_entry e inner join PAYROLL_EMPMAS as EI on (EI.IDNO= E.EMPNO)", "distinct(empno)", "(Fname+' '+ mname+' '+ lname)as Ename", "", "empno");
            objCommon.FillDropDownList(ddlEname, "payroll_OD_app_entry e inner join PAYROLL_EMPMAS as EI on (EI.IDNO= E.EMPNO)", "distinct(empno)", "(Fname+' '+ mname+' '+ lname)as Ename", "", "empno");

    }


    protected void fillemp(int ordno)
    {
        try
        {
            if (ViewState["rblLeaveOD"].ToString() == "0")
                objCommon.FillDropDownList(ddlEname, "payroll_leave_app_entry e inner join PAYROLL_EMPMAS as EI on (EI.IDNO= E.EMPNO)", "empno", "(Fname+' '+ mname+' '+ lname)as Ename", "ordtrno=" + ordno, "empno");
            else if (ViewState["rblLeaveOD"].ToString() == "1")
                objCommon.FillDropDownList(ddlEname, "payroll_OD_app_entry e inner join PAYROLL_EMPMAS as EI on (EI.IDNO= E.EMPNO)", "empno", "(Fname+' '+ mname+' '+ lname)as Ename", "ordtrno=" + ordno, "empno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.FilloRDER ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public DateTime findfix(DateTime dt, double i)
    {
        // cnt = 0;
        //int count = 0;
        //int check = 1;
        //do
        //{
        //    dt = dt.AddDays(-1);
        //    int check = Convert.ToInt32(objCommon.LookUp("PAYROLL_HOLIDAYS", "COUNT(*)", "DT=" + dt).ToString());
        //    if (Convert.ToString(dt.DayOfWeek) != "Sunday" || Convert.ToString(dt.DayOfWeek) != "Saturday")
        //    cnt++;
        //}
        //while (check != 0);

        //if (Convert.ToString(dt.DayOfWeek) == "Sunday")
        //{
        //    count =2;

        //}
        //else if (Convert.ToString(dt.DayOfWeek) == "Saturday")
        //{

        //   count= 1;
        //}
        //else 
        //{
        //    count =0;
        //}

        //return count;
        dt = dt.AddDays(i);
        int check = Convert.ToInt32(objCommon.LookUp("payroll_holidays", "count(*)", "dt=CONVERT(DATETIME,'" + dt + "',103)"));
        string day = Convert.ToString(dt.DayOfWeek);
        if (check == 1)
        {
            date = date + ", " + dt.Date.ToString("dd/MM/yyyy");
            findfix(dt, i);
            counter = counter + 1;
        }
        else if (day == "Sunday")
        {
            date = date + ", " + dt.Date.ToString("dd/MM/yyyy");
            findfix(dt, i);
            counter = counter + 1;
        }
        else if (day == "Saturday")
        {
            date = date + ", " + dt.Date.ToString("dd/MM/yyyy");
            findfix(dt, i);
            counter = counter + 1;
        }
        else
            day = (Convert.ToString(dt));

        return (dt);
    }

    protected void ddlEname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillorder(Convert.ToInt32(ddlEname.SelectedValue));
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ESTABLISHMENT/LEAVES/Transactions/Leave_Aprval_Estb.aspx");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLVLeaveApplApprovedList();
    }
    protected void ddlcollege2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }
    protected void ddlstafftype2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }

    private void ShowReportOrderGen(string reportTitle, string rptFileName)
    {
        try
        {
            string text = string.Empty;
            if (ViewState["rblLeaveOD"].ToString() == "1")
            {
                text = txtText.Text.Replace(",", "$");
            }
            string name = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(ddlEname.SelectedValue) + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}

