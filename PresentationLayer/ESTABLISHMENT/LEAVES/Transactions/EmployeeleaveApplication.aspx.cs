using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_EmployeeleaveApplication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLeaveMaster = new Leaves();

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
                //CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //  FillUser();
                objCommon.FillDropDownList(ddlChargeHanded, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "isnull(E.PFILENO,'')+'-'+ISNULL(E.FNAME,'') + ' ' +ISNULL(E.MNAME,'') + ' ' + ISNULL(LNAME,'')AS ENAME", "", "ENAME");

                ViewState["action"] = "add";

                FillCollege();
                FillStaffType();
                FillUser();
            }
            //
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        //{
        //    ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //    ddlCollege.Items.Remove(removeItem);
        //}

    }

    private void FillStaffType()
    {
        try
        {
            objCommon.FillDropDownList(ddlStafftype, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STAFFTYPE");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {

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
        ddlCollege.SelectedIndex = 0;


        ddlChargeHanded.SelectedIndex = 0;
        txtPath.Text = string.Empty;

        //fillorder();


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public DateTime findfix(DateTime dt, double i)
    {
        string date = string.Empty; int counter = 0;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt; int counter = 0;
            //DataSet dsh = objApp.RetrieveSingleHolydate(Convert.ToDateTime(txtJoindt.Text));
            // if (dsh.Tables[0].Rows.Count > 0)
            // {
            //     dt = Convert.ToDateTime(dsh.Tables[0].Rows[0]["DT"].ToString());
            //     objCommon.DisplayMessage("Joining Date is Holiday",this);
            //     txtJoindt.Focus();
            //     return;
            // }

            Boolean leavefnan = false;
            string date = Convert.ToDateTime(txtFromdt.Text).ToString("dd/MM/yyyy");
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
            ViewState["LNO"] = Convert.ToInt32(ddlLName.SelectedValue);
            objLeaves.LNO = Convert.ToInt32(ViewState["LNO"].ToString());

            //2-1-2015
            int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + Convert.ToInt32(ViewState["LNO"].ToString())));
            objLeaves.LEAVENO = leaveno;

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
            //objLeaves.STATUS = "A";
            objLeaves.STATUS = "P";
            objLeaves.ADDRESS = "";
            objLeaves.REASON = Convert.ToString(txtReason.Text);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeaves.PREFIX = PrefixDays;
            objLeaves.SUFFIX = SuffixDays;
            //objLeaves.PAPNO = 0;
            objLeaves.PAPNO = Convert.ToInt32(ViewState["papno"]);
            if (ddlChargeHanded.SelectedIndex > 0)
            {
                objLeaves.CHARGE_HANDED = ddlChargeHanded.SelectedItem.Text;
            }
            else
            {
                objLeaves.CHARGE_HANDED = string.Empty;

            }
            if (rblleavetype.SelectedValue == "1")//half day
            {
                objLeaves.ISFULLDAYLEAVE = false;
            }
            else
            {
                objLeaves.ISFULLDAYLEAVE = true;
            }
            if (ddlNoon.SelectedValue == Convert.ToString(1))
            {
                leavefnan = true;
            }

            objLeaves.LEAVEFNAN = leavefnan;
            objLeaves.ENTRY_STATUS = "Leave Entry";
            int chidno = Convert.ToInt32(ddlChargeHanded.SelectedValue);
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //2-1-15--add leaveno field to update leaveno 
                    int ret_letrno = Convert.ToInt32(objApp.AddAPP_ENTRYDIRECT(objLeaves, prefix, suffix, chidno));
                    if (ret_letrno > 0)
                    {
                        MessageBox("Record Saved Successfully");

                        ViewState["action"] = "add";
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["LETRN0"] != null)
                    {
                        //objLeaves.LETRNO = Convert.ToInt32(ViewState["LETRN0"].ToString());
                        //CustomStatus cs = (CustomStatus)objApp.UpdateAppEntry(objLeaves);
                        //if (cs.Equals(CustomStatus.RecordUpdated))
                        //{
                        //    MessageBox("Record Updated Successfully");

                        //    ViewState["action"] = "add";
                        //    Clear();
                        //}
                    }
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.FetchBalleave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____" && txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____")
            {
                //calcdiff();
                DateTime fromDate = Convert.ToDateTime(txtFromdt.Text.ToString());

                DateTime toDate = Convert.ToDateTime(txtTodt.Text.ToString());
                int empidno = Convert.ToInt32(ddlEmp.SelectedValue);
                int ret = CheckLeaveExists();
                if (ret == 1)
                {
                    return;
                }
                if (toDate < fromDate)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtNodays.Text = string.Empty;
                    Clear();
                    return;
                }
                else if (rblleavetype.SelectedValue == "1" && fromDate < toDate)
                {
                    MessageBox("Please select single day for Half Day Leave");
                    return;
                }

                int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue)));
                int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue))); //Added by Saket Singh on 13-Dec-2016


                if (Convert.ToDateTime(txtFromdt.Text) == Convert.ToDateTime(txtTodt.Text))
                {
                    // DataSet dsHoliday = objCommon.FillDropDown("payroll_holidays_vacation", "DATE", "STNO", "DATE='" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + "' and isnull(RESTRICT_STATUS,'N')='N' AND STNO IN(0," + stno + ")", "");
                    if (ViewState["LEAVE"] != null)
                    {
                        if (ViewState["LEAVE"].ToString() != "RH")
                        {

                            DataSet dsHoliday = objCommon.FillDropDown("payroll_holidays_vacation", "DATE", "STNO", "DATE='" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + "' and isnull(RESTRICT_STATUS,'N')='N' AND STNO IN(0," + stno + ")" + "AND college_no =" + Convert.ToInt32(collegeno), "");
                            if (dsHoliday.Tables[0].Rows.Count > 0)
                            {
                                MessageBox("Sorry! Leave Not allowed on Holiday");
                                btnSave.Enabled = false;
                                Clear();
                                return;
                            }
                            string dayname = Convert.ToDateTime(txtFromdt.Text).DayOfWeek.ToString();
                            if (dayname == "Sunday")
                            {
                                MessageBox("Sorry! Leave Not allowed on Sunday");
                                btnSave.Enabled = false;
                                return;
                            }
                        }
                        else if (ViewState["LEAVE"].ToString() == "RH")
                        {
                            DataSet dsHoliday1 = objCommon.FillDropDown("payroll_holidays_vacation", "DATE", "STNO", "DATE='" + Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd") + "' and isnull(RESTRICT_STATUS,'N')='Y'  AND STNO IN(0," + stno + ")" + "AND college_no =" + Convert.ToInt32(collegeno), "");
                            if (dsHoliday1.Tables[0].Rows.Count > 0)
                            {

                            }
                            else
                            {
                                MessageBox("This is not a Restricted Holiday!!Please Check");
                                btnSave.Enabled = false;
                                return;
                            }
                            string dayname1 = Convert.ToDateTime(txtFromdt.Text).DayOfWeek.ToString();
                            if (dayname1 == "Sunday")
                            {
                                MessageBox("Sorry! Leave Not allowed on Sunday");
                                btnSave.Enabled = false;
                                return;
                            }
                        }
                    }
                }


                //DataSet  ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue,stno);
                DataSet ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, stno, Convert.ToInt32(ViewState["CAL_HOLIDAYS"]), collegeno, Convert.ToInt32(ddlNoon.SelectedValue)); //Added by Saket Singh on 13-Dec-2016
                if (ds.Tables[0].Rows.Count > 0)
                {

                    double totdays = Convert.ToDouble(ds.Tables[0].Rows[0]["TOT_DAYS"].ToString());
                    txtNodays.Text = totdays.ToString();

                    txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                    txtNodays.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    if (totdays == 0)
                    {
                        btnSave.Enabled = false;
                        MessageBox("Sorry! Total Days are Zero. Not Allow");
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                }

                Chkvalid();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    public int CheckLeaveExists()
    {
        int cntleaveapp;
        int ret = 0;
        string from_date, to_Date;
        int empid = Convert.ToInt32(ddlEmp.SelectedValue);
        from_date = Convert.ToDateTime(txtFromdt.Text).ToString("yyyy-MM-dd");
        to_Date = Convert.ToDateTime(txtTodt.Text).ToString("yyyy-MM-dd");
        cntleaveapp = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_APP_ENTRY", "COUNT(1)", "( '" + from_date + "' BETWEEN FROM_DATE AND TO_DATE  OR  '" + to_Date + "' BETWEEN FROM_DATE AND TO_DATE) AND EMPNO=" + empid + " AND STATUS NOT IN('R','D')"));

        if (cntleaveapp > 0)
        {
            MessageBox("Leave Already Exists For Selected Date!");

            txtFromdt.Text = string.Empty;
            txtTodt.Text = string.Empty;
            txtJoindt.Text = string.Empty;
            txtNodays.Text = string.Empty;
            btnSave.Enabled = false;
            ret = 1;
        }
        else
        {
            btnSave.Enabled = true;
            ret = 0;
        }
        return ret;
    }
    //public void calcdiff()
    //{
    //    DataSet ds = null;
    //    try
    //    {
    //        if (txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____" && txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____")
    //        {
    //            int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue)));
    //            int collegeno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue)));
    //             ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, stno, Convert.ToInt32(ViewState["CAL_HOLIDAYS"]), collegeno, Convert.ToInt32(ddlNoon.SelectedValue)); //Added by Saket Singh on 13-Dec-2016
    //            //ds = objApp.GetNoofdays(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), rblleavetype.SelectedValue, stno);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                txtNodays.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
    //                txtJoindt.Text = Convert.ToString(ds.Tables[1].Rows[0][0]);
    //                Chkvalid();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.calcdiff->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server.UnAvailable");
    //    }
    //}
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
    protected void txtNodays_TextChanged(object sender, EventArgs e)
    {
        if (txtNodays.Text.ToString() != string.Empty)
        {
            Chkvalid();
        }
    }
    private void FillUser()
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "ENAME");
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex == 0 && ddlStafftype.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue), "ENAME");
            }
            else if (ddlCollege.SelectedIndex > 0 && ddlStafftype.SelectedIndex == 0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "ENAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "isnull(E.PFILENO,'')+' - '+ ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "", "ENAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Cancel.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlLName.SelectedIndex = 0;

        FillLeave(Convert.ToInt32(ddlEmp.SelectedValue));
        txtLeavebal.Text = string.Empty;

        GetPAPath1();

    }


    private void FillLeave(int EmpId)
    {
        try
        {
            //objCommon.FillDropDownList(ddlLName, "payroll_leavetran lt INNER JOIN payroll_leave l on(l.lno=lt.lno)", "lt.LNO", "l.LEAVENAME", "lt.st iN(1,2) AND lt.idno=(*) AND lt.year=year(getdate())", "");
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                FillUser();
            }
            else
            {
                FillUser();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_DirectLeaveApplication.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlNoon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNoon.SelectedValue == "1")
        {
            txtJoindt.Text = txtTodt.Text;
            ddlNoon.SelectedValue = "1";
        }
        else
        {
            if (txtTodt.Text != string.Empty)
            {

                DateTime joindate;
                joindate = Convert.ToDateTime(txtTodt.Text);

                txtJoindt.Text = joindate.AddDays(1).ToString();
                ddlNoon.SelectedValue = "0";
            }
            else
            {
                MessageBox("Please Select From Date and To Date");
            }



        }
    }

    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillUser();
    }


    protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();
           
            //int useridno = Convert.ToInt32(Session["idno"]);
            int useridno = Convert.ToInt32(ddlEmp.SelectedValue);
            //int collegeno =Convert.ToInt32( objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + useridno + ""));
            //int collegeno = Convert.ToInt32(ViewState["COLLEGE_NO"]);
            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            //dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno+"  AND COLLEGE_NO="+ collegeno +" ", "");
            //string pano = dsAuth.Tables[0].Rows[0]["PANO"].ToString();

            //DataSet dsdept = new DataSet();
            //dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
            //string dept = dsdept.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();

            DataSet dspath = new DataSet();
            
            //dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " ", "");
            dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " ", "");
            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();


                string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
                string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
                string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
                string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
                string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();


                string uano1 = string.Empty;
                string uano2 = string.Empty;
                string uano3 = string.Empty;
                string uano4 = string.Empty;
                string uano5 = string.Empty;
                string paname1 = string.Empty;
                string paname2 = string.Empty;
                string paname3 = string.Empty;
                string paname4 = string.Empty;
                string paname5 = string.Empty;

                DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
                if (dsauth1.Tables[0].Rows.Count > 0)
                {
                    uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
                if (dsauth2.Tables[0].Rows.Count > 0)
                {
                    uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth3 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
                if (dsauth3.Tables[0].Rows.Count > 0)
                {
                    uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth4 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
                if (dsauth4.Tables[0].Rows.Count > 0)
                {
                    uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth5 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
                if (dsauth5.Tables[0].Rows.Count > 0)
                {
                    uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();
                }


                if (userno == uano1)
                {
                    path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano2)
                {
                    path = paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano3)
                {
                    path = paname4 + "->" + paname5;
                }
                else if (userno == uano4)
                {
                    path = paname5;
                }
                else if (userno == uano5)
                {
                    path = paname5;
                }
                else
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                txtPath.Text = path;

            }
            else
            {
                MessageBox("Sorry! Authority Not found");
                return;
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }
}