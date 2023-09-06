using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ESTABLISHMENT_LEAVES_Master_leaves : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

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
                    //lblHelp.Text=objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillPeriod();
                FillStaffType();
                FillLeaveName();
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                BindListViewLeaves();

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                CheckPageAuthorization();
                //divtxtNoslotDay.Visible = false;
                //divtxtafterCompletion.Visible = false;
                //divtxtstartDT.Visible = false;
                //divtxtcreditDT.Visible = false;
                //Set Report Parameters
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_LeaveMaster.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");

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
                Response.Redirect("~/notauthorized.aspx?page=leaves.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=leaves.aspx");
        }
    }

    private void BindListViewLeaves()
    {
        try
        {
            DataSet ds = objLeave.GetAllLeave();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                //dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
                //dpPager.Visible = true;
            }
            lvLeave.DataSource = ds;
            lvLeave.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.BindListViewLeaves -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListViewLeaves();
    }
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
    }
    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillLeaveName()
    {
        try
        {
            objCommon.FillDropDownList(ddlleaveshrtname, "Payroll_Leave_Name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void Clear()
    {
        ddlStafftype.SelectedIndex = 0;
        ddlPeriod.SelectedIndex = 0;
        ddlSex.SelectedIndex = 0;
        //ddlleaveshrtname.SelectedIndex =0;
        //ddlleaveshrtname.SelectedItem.Value = "0";
        //txtLeave.Text = string.Empty;
        txtShortname.Text = string.Empty;
        txtMaxdays.Text = string.Empty;
        ddlCarry.SelectedIndex = 0;
        txtmutiplewith.Text = "1";
        BindListViewLeaves();
        txtServiceLimit.Text = txtServiceComplete.Text = string.Empty;
        txtAllowdt.Text = string.Empty;
        ddlleaveshrtname.SelectedIndex = 0;
        chkBeforeAfterCapping.Checked = false;
        txtMaxDayApply.Text = string.Empty;
        txtMinDaysApply.Text = string.Empty;
        txttimeleave.Text = string.Empty;
        txtPostdated.Text = string.Empty;
        txtPredated.Text = string.Empty;
        txtpdlMaxDaysApply.Text = string.Empty;
        txtpdMaxDayApply.Text = string.Empty;
        txtMaxOccPD.Text = string.Empty;
        txtMaxOccPDL.Text = string.Empty;
        ddlOccPD.SelectedValue = "0";
        ddlOccPDL.SelectedValue = "0";
        txtNoslotDay.Text = txtafterCompletion.Text = string.Empty;
        chkLeaveSlot.Checked = false;
        txtDate.Text = string.Empty;
        txtCreditDT.Text = string.Empty;

        divtxtNoslotDay.Visible = false;
        divtxtafterCompletion.Visible = false;
        divtxtstartDT.Visible = false;
        //divtxtcreditDT.Visible = false;
        chkIsclassArragnment.Checked = false;
        chkIsclassArrangmentAcceptance.Checked = false;
        divisdate.Visible = false;
        chkisdate.Checked = false;
        txtDayCarry.Text = string.Empty;
        divCarry.Visible = false;
        chkLoad.Checked = false;
        txtLeaveMonth.Text = string.Empty;
        chkValid.Checked = false;
        divLeaveValid.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";

        btnAdd.Visible = false;
        btnShowReport.Visible = false;

        btnSave.Visible = true;
        btnBack.Visible = true;
        btnCancel.Visible = true;

        divtxtNoslotDay.Visible = false;
        divtxtafterCompletion.Visible = false;
        divtxtstartDT.Visible = false;
        divtxtcreditDT.Visible = false;
        divisdate.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean carrybit = false;
            if (ddlCarry.SelectedValue == Convert.ToString(1))
                carrybit = true;
            Leaves objLeaves = new Leaves();
            objLeaves.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            objLeaves.LEAVE = Convert.ToString(txtShortname.Text);
            objLeaves.LEAVENAME = Convert.ToString(ddlleaveshrtname.SelectedItem.Text);
            //objLeaves.LNO = Convert.ToInt32(ddlleaveshrtname.SelectedValue);
            objLeaves.MAX = Convert.ToDouble(txtMaxdays.Text);
            objLeaves.CARRY = carrybit;
            objLeaves.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);
            objLeaves.SEX = Convert.ToChar(ddlSex.SelectedValue);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeaves.LEAVENO = Convert.ToInt32(ddlleaveshrtname.SelectedValue);
            if (txtServiceComplete.Text != string.Empty)
            {
                objLeaves.SERVICE_COMPLETE_LIMIT = Convert.ToInt32(txtServiceComplete.Text);
            }
            else
            {
                objLeaves.SERVICE_COMPLETE_LIMIT = 0;
            }
            if (txtServiceLimit.Text != string.Empty)
            {
                objLeaves.SERVICE_LIMIT = Convert.ToInt32(txtServiceLimit.Text);
            }
            else
            {
                objLeaves.SERVICE_LIMIT = 0;
            }
            if (txtPredated.Text != string.Empty)
            {
                objLeaves.MAXPREDATED = Convert.ToInt32(txtPredated.Text);
            }
            else
            {
                objLeaves.MAXPREDATED = 0;
            }
            if (txtPostdated.Text != string.Empty)
            {
                objLeaves.MAXPOSTDATED = Convert.ToInt32(txtPostdated.Text);
            }
            else
            {
                objLeaves.MAXPOSTDATED = 0;
            }
            if (txtpdMaxDayApply.Text != string.Empty)
            {
                objLeaves.PDMAXDAYS = Convert.ToInt32(txtpdMaxDayApply.Text);
            }
            else
            {
                objLeaves.PDMAXDAYS = 0;
            }
            if (txtpdlMaxDaysApply.Text != string.Empty)
            {
                objLeaves.PDLMAXDAYS = Convert.ToInt32(txtpdlMaxDaysApply.Text);
            }
            else
            {
                objLeaves.PDLMAXDAYS = 0;
            }
            if (txtMaxOccPD.Text != string.Empty)
            {
                objLeaves.MAXOCCASIONPD = Convert.ToInt32(txtMaxOccPD.Text);
            }
            else
            {
                objLeaves.MAXOCCASIONPD = 0;
            }
            objLeaves.OCCPERIODPD = ddlOccPD.SelectedValue;

            if (txtMaxOccPDL.Text != string.Empty)
            {
                objLeaves.MAXOCCASIONPDL = Convert.ToInt32(txtMaxOccPDL.Text);
            }
            else
            {
                objLeaves.MAXOCCASIONPDL = 0;
            }
            objLeaves.OCCPERIODPDL = ddlOccPDL.SelectedValue;
            if (chkLeaveSlot.Checked == true)
            {
                objLeaves.IsCreditSlotWise = true;
                if (txtNoslotDay.Text == string.Empty)
                {
                    MessageBox("Please Enter No. of Slot of Days.");
                    txtNoslotDay.Focus();
                    return;
                }
                else
                {
                    objLeaves.SlotOFDays = Convert.ToInt32(txtNoslotDay.Text);
                }
                //objLeaves.LeaveCreditAfterSlot = Convert.ToDouble(txtafterCompletion.Text);
                if (txtafterCompletion.Text == string.Empty)
                {
                    MessageBox("Total Credit Leaves after Completion of Slot.");
                    txtafterCompletion.Focus();
                    return;
                }
                else
                {
                    objLeaves.LeaveCreditedAfterSlot = Convert.ToDecimal(txtafterCompletion.Text);
                }
            }
            else
            {
                objLeaves.IsCreditSlotWise = false;
            }
            objLeaves.CREDITDT = txtDate.Text;
            //if (txtCreditDT.Text != string.Empty)
            //{
            //    objLeaves.ALLOTEDDATE = Convert.ToDateTime(txtCreditDT.Text);
            //}
            //else
            //{
            //    //objLeaves.ALLOTEDDATE = null;
            //}
            objLeaves.ALLOTEDDATE = txtCreditDT.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtCreditDT.Text.Trim());



            if (txtAllowdt.Text != string.Empty)
            {
                objLeaves.DAYS = Convert.ToInt32(txtAllowdt.Text);
            }
            else
            {
                objLeaves.DAYS = 0;
            }
            if (txtMaxDayApply.Text != string.Empty)
            {
                objLeaves.MAXDAYS = Convert.ToDouble(txtMaxDayApply.Text);
            }
            else
            {
                objLeaves.MAXDAYS = 0;
            }
            if (txtMinDaysApply.Text != string.Empty)
            {
                objLeaves.NO_DAYS = Convert.ToDouble(txtMinDaysApply.Text);
            }
            else
            {
                objLeaves.NO_DAYS = 0;
            }
            if (txtmutiplewith.Text != string.Empty)
            {
                objLeaves.CAL = Convert.ToDouble(txtmutiplewith.Text);
            }
            else
            {
                objLeaves.CAL = 0;
            }
            if (!txttimeleave.Text.Trim().Equals(string.Empty)) objLeaves.LeaveTime = txttimeleave.Text;
            if (chkBeforeAfterCapping.Checked == true)
            {
                objLeaves.IsAllowBeforeApplication = true;
            }
            else
            {
                objLeaves.IsAllowBeforeApplication = false;
            }
            int calhlyday = 0;
            //if (ddlcalholiday.SelectedIndex == 0)
            //{
            //    objLeaves.CALHOLIDAY = calhlyday + 1;
            //}
            //else if (ddlcalholiday.SelectedIndex == 1)
            //{
            //    objLeaves.CALHOLIDAY = 0;
            //}
            // Added by sagar on 25/04/2017
            if (ddlcalholiday.SelectedIndex == 1)
            {
                objLeaves.CALHOLIDAY = 1;
            }
            else if (ddlcalholiday.SelectedIndex == 2)
            {
                objLeaves.CALHOLIDAY = 0;
            }

            if (chkIsclassArragnment.Checked == true)
            {
                objLeaves.IsClassArrangeRequired = true;
            }
            else
            {
                objLeaves.IsClassArrangeRequired = false;
            }

            if (chkIsclassArrangmentAcceptance.Checked == true)
            {
                objLeaves.IsClassArrangeAcceptanceRequired = true;
            }
            else
            {
                objLeaves.IsClassArrangeAcceptanceRequired = false;
            }
            //Added by Sonal Banode on 20-09-2022 for saving ISDOJ
            if (chkisdate.Checked == true)
            {
                objLeaves.IsDOJ = true;
            }
            else
            {
                objLeaves.IsDOJ = false;
            }

            if (txtDayCarry.Text != string.Empty)
            {
                objLeaves.CARRYDAYS = Convert.ToDouble(txtDayCarry.Text);
            }
            else
            {
                objLeaves.CARRYDAYS = 0;
            }
            //
            //Added by Sonal Banode on 23-03-2023
            if (chkLoad.Checked == true)
            {
                objLeaves.ISREQUIREDLOAD = true;
            }
            else
            {
                objLeaves.ISREQUIREDLOAD = false;
            }
            //if (txtLeaveMonth.Text != string.Empty)
            //{
            //    objLeaves.LEAVEVALIDMONTH = Convert.ToDecimal(txtLeaveMonth.Text);
            //}
            //else
            //{
            //    objLeaves.LEAVEVALIDMONTH = 0;
            //}

            if (chkValid.Checked == true)
            {
                divLeaveValid.Visible = true;
                //objLeaves.LEAVEVALIDMONTH = Convert.ToDecimal(txtLeaveMonth.Text);
                objLeaves.IsLeaveValid = true;
                if (txtLeaveMonth.Text != string.Empty)
                {
                    objLeaves.LEAVEVALIDMONTH = Convert.ToDecimal(txtLeaveMonth.Text);
                }
                else
                {
                    MessageBox("Please Enter Leave Valid Month");
                    return;
                }
            }
            else
            {
                divLeaveValid.Visible = false;
                objLeaves.LEAVEVALIDMONTH = 0;
                objLeaves.IsLeaveValid = false;
            }

            objLeaves.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objLeaves.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            //
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objLeave.AddLeave(objLeaves);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Added Successfully");
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        this.BindListViewLeaves();
                        ViewState["action"] = null;
                        //ddlleaveshrtname.SelectedIndex = 0;
                        Clear();
                        btnAdd.Visible = true;
                        btnShowReport.Visible = true;

                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnBack.Visible = false;
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exist!");
                    }
                }
                else
                {
                    if (ViewState["LNO"] != null)
                    {
                        objLeaves.LNO = Convert.ToInt32(ViewState["LNO"].ToString());
                        CustomStatus cs = (CustomStatus)objLeave.UpdateLeave(objLeaves);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            MessageBox("Record Update Successfully");
                            this.BindListViewLeaves();
                            ddlleaveshrtname.SelectedIndex = 0;
                            ViewState["action"] = null;
                            //Clear();
                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;

                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;
                        }
                        else if (cs.Equals(CustomStatus.RecordFound))
                        {
                            MessageBox("Record Already Exist!");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnSave_click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlleaveshrtname.SelectedIndex = 0;
        Clear();
        btnShowReport.Visible = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;

        btnAdd.Visible = true;
        btnShowReport.Visible = true;

        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        divisdate.Visible = false;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int LNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(LNO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnAdd.Visible = false;
            btnShowReport.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnEdit_click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int LNO = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objLeave.DeleteLeave(LNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnDelete_Click->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(Int32 LNO)
    {
        DataSet ds = null;
        try
        {
            ds = objLeave.GetSingleLeave(LNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["LNO"] = LNO;
                //ddlleaveshrtname.SelectedItem.Text = ds.Tables[0].Rows[0]["LEAVENAME"].ToString();
                ddlleaveshrtname.SelectedValue = ds.Tables[0].Rows[0]["LEAVENO"].ToString();
                txtShortname.Text = ds.Tables[0].Rows[0]["LEAVE"].ToString();
                double MaxDay;
                MaxDay = Convert.ToDouble(ds.Tables[0].Rows[0]["MAX"]);
                txtMaxdays.Text = MaxDay.ToString();

                // txtMaxdays.Text = ds.Tables[0].Rows[0]["MAX"].ToString();
                ddlStafftype.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                ddlPeriod.SelectedValue = ds.Tables[0].Rows[0]["PERIOD"].ToString();
                ddlCarry.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["CARRY"]);
                ddlSex.SelectedValue = ds.Tables[0].Rows[0]["SEX"].ToString();
                txtmutiplewith.Text = ds.Tables[0].Rows[0]["CAL"].ToString();
                txtAllowdt.Text = ds.Tables[0].Rows[0]["ALLOWED_DAYS"].ToString();
                ddlcalholiday.SelectedValue = ds.Tables[0].Rows[0]["CAL_HOLIDAYS"].ToString();

                // txtMinDaysApply.Text = ds.Tables[0].Rows[0]["MIN_DAYS_TO_APPLY"].ToString();
                double MIN_DAYS_TO_APPLY = Convert.ToDouble(ds.Tables[0].Rows[0]["MIN_DAYS_TO_APPLY"].ToString());
                txtMinDaysApply.Text = MIN_DAYS_TO_APPLY.ToString();
                //txtMaxDayApply.Text = ds.Tables[0].Rows[0]["MAX_DAYS_TO_APPLY"].ToString();
                double MAX_DAYS_TO_APPLY = Convert.ToDouble(ds.Tables[0].Rows[0]["MAX_DAYS_TO_APPLY"].ToString());
                txtMaxDayApply.Text = MAX_DAYS_TO_APPLY.ToString();

                //txttimeleave.Text = ds.Tables[0].Rows[0]["TimeforLeave"].ToString();
                txtPostdated.Text = ds.Tables[0].Rows[0]["MAXPOSTDATED"].ToString();
                txtPredated.Text = ds.Tables[0].Rows[0]["MAXPREDATED"].ToString();
                txtpdMaxDayApply.Text = ds.Tables[0].Rows[0]["PDMAXDAYS"].ToString();
                txtpdlMaxDaysApply.Text = ds.Tables[0].Rows[0]["PDLMAXDAYS"].ToString();
                //txtMaxOccPD.Text = ds.Tables[0].Rows[0]["MAXOCCASIONPD"].ToString();
                // txtMaxOccPDL.Text = ds.Tables[0].Rows[0]["MAXOCCASIONPDL"].ToString();               
                //ddlOccPD.SelectedValue = ds.Tables[0].Rows[0]["OCCPERIODPD"].ToString();
                // ddlOccPDL.SelectedValue = ds.Tables[0].Rows[0]["OCCPERIODPDL"].ToString();

                chkLeaveSlot.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCreditSlotWise"]);
                chkisdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISDOJ"]);
                if (chkLeaveSlot.Checked == true)
                {
                    //divtxtNoslotDay.Visible = true;
                    // divtxtafterCompletion.Visible = true;
                    txtNoslotDay.Text = ds.Tables[0].Rows[0]["SlotOFDays"].ToString();
                    txtafterCompletion.Text = ds.Tables[0].Rows[0]["LeaveCreditAfterSlot"].ToString();
                    divtxtNoslotDay.Visible = true;
                    divtxtafterCompletion.Visible = true;
                    divtxtstartDT.Visible = true;
                    //divtxtcreditDT.Visible = true;
                    //  txtNoslotDay.Enabled = txtafterCompletion.Enabled = true;
                    divisdate.Visible = true;
                }
                else
                {
                    // txtNoslotDay.Enabled = txtafterCompletion.Enabled = false;
                    // divtxtNoslotDay.Visible = false;
                    //divtxtafterCompletion.Visible = false;

                    divtxtNoslotDay.Visible = false;
                    divtxtafterCompletion.Visible = false;
                    divtxtstartDT.Visible = false;
                    divisdate.Visible = false;
                }

                txtDate.Text = ds.Tables[0].Rows[0]["CREDITDT"].ToString();
                txtCreditDT.Text = ds.Tables[0].Rows[0]["ALLOTEDDATE"].ToString();


                int yearly = Convert.ToInt32(ds.Tables[0].Rows[0]["Yearly"].ToString());
                //if (yearly == 2)
                //{
                txtServiceComplete.Enabled = txtServiceLimit.Enabled = true;
                txtServiceComplete.Text = ds.Tables[0].Rows[0]["MAX_SERVICE_COMPLETE"].ToString();
                txtServiceLimit.Text = ds.Tables[0].Rows[0]["SERVICE_LIMIT"].ToString();
                //}
                //else
                //{
                //    txtServiceComplete.Enabled = txtServiceLimit.Enabled = false;
                //    txtServiceComplete.Text = txtServiceLimit.Text = string.Empty;
                //}
                chkBeforeAfterCapping.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAllowBeforeApplication"]);
                chkIsclassArragnment.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsClassArrangeRequired"]);
                chkIsclassArrangmentAcceptance.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsClassArrangeAcceptanceRequired"]);
                if (ddlCarry.SelectedIndex == 1)
                {
                    divCarry.Visible = true;
                    double MAXDAYSCARRY = Convert.ToDouble(ds.Tables[0].Rows[0]["MAXDAYSCARRY"].ToString());
                    txtDayCarry.Text = MAXDAYSCARRY.ToString();
                }
                else
                {
                    divCarry.Visible = false;
                }
                chkLoad.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsRequiredLoad"].ToString());
                //txtLeaveMonth.Text = ds.Tables[0].Rows[0]["LEAVEVALIDMONTH"].ToString();
                chkValid.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLeaveValid"].ToString());
                if (chkValid.Checked == true)
                {
                    divLeaveValid.Visible = true;
                    txtLeaveMonth.Text = ds.Tables[0].Rows[0]["LEAVEVALIDMONTH"].ToString();
                }
                else
                {
                    divLeaveValid.Visible = false;
                    txtLeaveMonth.Text = string.Empty;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowDetails->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            //string url=Request.Url.ToString().Substring(0,(Request.Url.ToString().IndexOf("Establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowReport->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnShowReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReport("Leave_Type_Master", "ESTB_LeaveMaster.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnShowReport_Click->" + ex.Message + ' ' + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}

    protected void btnShowReport_Click1(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Leave_Type_Master", "ESTB_LeaveMaster.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnShowReport_Click->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlleaveshrtname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet ds = objCommon.FillDropDown("Pay_Leave_Name", "Leave_ShortName", "LVNO", "LVNO=" + ddlleaveshrtname.SelectedValue, "LVNO");
        DataSet ds = objCommon.FillDropDown("PayROLL_Leave_Name", "Leave_ShortName,cast(MAX_DAYS as int) as MAX_DAYS", "LVNO,yearly", "LVNO=" + ddlleaveshrtname.SelectedValue, "LVNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtShortname.Text = ds.Tables[0].Rows[0]["Leave_ShortName"].ToString();
            //
            //int maxDays= Convert.ToInt32(ds.Tables[0].Rows[0]["MAX_DAYS"].ToString());
            txtMaxdays.Text = ds.Tables[0].Rows[0]["MAX_DAYS"].ToString();
            int yearly = Convert.ToInt32(ds.Tables[0].Rows[0]["YEARLY"].ToString());
            if (yearly == 2)
            {
                // trPeriod.Visible = false;
                //txtServiceLimit.Enabled = true;
                //txtServiceComplete.Enabled = true;
            }
            else
            {
                //trPeriod.Visible = true;
                //txtServiceLimit.Enabled = false;
                //txtServiceComplete.Enabled = false;
            }
        }
        else
        {
            MessageBox("Leave Name not available of this type");
            txtShortname.Text = string.Empty;
            Clear();
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void ChckedChanged(object sender, EventArgs e)
    {
        if (chkLeaveSlot.Checked == true)
        {
            divtxtNoslotDay.Visible = true;
            divtxtafterCompletion.Visible = true;
            divtxtstartDT.Visible = true;
            divisdate.Visible = true;
            // divtxtcreditDT.Visible = true;
        }
        else
        {
            divtxtNoslotDay.Visible = false;
            divtxtafterCompletion.Visible = false;
            divtxtstartDT.Visible = false;
            //divtxtcreditDT.Visible = false;
            divisdate.Visible = false;
            txtNoslotDay.Text = string.Empty;
            txtafterCompletion.Text = string.Empty;
            txtCreditDT.Text = string.Empty;
            chkisdate.Checked = false;
        }
    }


    protected void ddlCarry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCarry.SelectedValue == "1")
        {
            divCarry.Visible = true;
        }
        else
        {
            divCarry.Visible = false;
            txtDayCarry.Text = string.Empty;
        }
    }
    protected void chkValid_CheckedChanged(object sender, EventArgs e)
    {
        if (chkValid.Checked == true)
        {
            divLeaveValid.Visible = true;
        }
        else
        {
            divLeaveValid.Visible = false;
        }
    }
}
