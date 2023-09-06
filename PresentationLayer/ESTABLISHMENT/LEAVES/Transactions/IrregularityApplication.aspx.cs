using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using IITMS.SQLServer.SQLDAL;
using System.Globalization;

public partial class ESTABLISHMENT_LEAVES_Transactions_IrregularityApplication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLeaves = new Leaves();
    static int reason_no = 0;
    int count_record = 0;

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
                CheckPageAuthorization();
            }
            txtMonthYear.Text = System.DateTime.Now.ToString("MM/yyyy");
            int useridno = Convert.ToInt32(Session["idno"]);
            Session["deptno"] = objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + useridno + " ");
            Session["COLLEGE_NO"] = objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + useridno + " ");
            pnlAdd.Visible = false;
        }
        else
        {

        }
        //blank div tag
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IrregularityApplication.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IrregularityApplication.aspx");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void BindListView()
    {
        try
        {
            string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
            DateTime dt = Convert.ToDateTime(ToDate);
            int month = dt.Month;
            int year = dt.Year;
            string frmdt = null;

            if (month == 1)
            {
                frmdt = "01" + "/" + month + "/" + year.ToString();
            }
            else
            {
                frmdt = "01" + "/" + month + "/" + year.ToString();
            }
            string todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();


            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(frmdt)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(todt)));
            Tdate = Tdate.Substring(0, 10);
            DataSet ds = null;
            int idno = Convert.ToInt32(Session["idno"].ToString());
            int deptno = 0;
            int collegeno = 0;
            int stno = 0;
            int count = 0;
            deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + idno + " "));
            collegeno = Convert.ToInt32(Session["COLLEGE_NO"].ToString());
            stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno + " "));
            Boolean ISDAILYWORKINGHOURS = Convert.ToBoolean(objCommon.LookUp("PAYROLL_STAFFTYPE", "ISNULL(ISDAILYWORKINGHOURS,0)AS ISDAILYWORKINGHOURS", "STNO=" + stno + " "));
            if (ISDAILYWORKINGHOURS == true)
            {
                ds = objApp.GetIrregularityApplyList(Fdate, Tdate, idno, deptno, collegeno, stno, count);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvLateEarly.DataSource = ds.Tables[0];
                    lvLateEarly.DataBind();
                    lvLateEarly.Visible = true;
                    pnlLeaveCard.Visible = true;
                    pnlDetail.Visible = false;
                }
                else
                {
                    lvLateEarly.DataSource = null;
                    lvLateEarly.DataBind();
                    lvLateEarly.Visible = false;
                    pnlLeaveCard.Visible = false;
                    pnlDetail.Visible = false;
                }
            }
            else
            {
                ds = objApp.GetIrregularityApplyListForTeaching(Fdate, Tdate, idno, deptno, collegeno, stno, count);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvInfo.DataSource = ds.Tables[0];
                    lvInfo.DataBind();
                    lvInfo.Visible = true;
                    pnlDetail.Visible = true;
                    pnlLeaveCard.Visible = false;
                }
                else
                {
                    lvInfo.DataSource = null;
                    lvInfo.DataBind();
                    lvInfo.Visible = false;
                    pnlDetail.Visible = false;
                    pnlLeaveCard.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        Button btnApply = sender as Button;
        pnlMonth.Visible = false;
        pnlLeaveCard.Visible = false;
        string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
        DateTime dt = Convert.ToDateTime(ToDate);
        int month = dt.Month;
        int year = dt.Year;
        string frmdt = null;

        if (month == 1)
        {
            frmdt = "01" + "/" + month + "/" + year.ToString();
        }
        else
        {
            frmdt = "01" + "/" + month + "/" + year.ToString();
        }
        string todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();


        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(frmdt)));
        Fdate = Fdate.Substring(0, 10);
        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(todt)));
        Tdate = Tdate.Substring(0, 10);
        DataSet ds = null;
        int idno = Convert.ToInt32(Session["idno"].ToString());
        int deptno = 0;
        int collegeno = 0;
        int stno = 0;
        int count = 0;
        deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + idno + " "));
        collegeno = Convert.ToInt32(Session["COLLEGE_NO"].ToString());
        stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno + " "));
        int SRNO = Convert.ToInt32(btnApply.CommandArgument);
        Session["SRNO"] = SRNO;
        DataSet dsApply = new DataSet();
        dsApply = objCommon.FillDropDown("PAYROLL_IRREGULARITY_APP_ENTRY", "*", "", "SERIALNO=" + SRNO, "");
        if (dsApply.Tables[0].Rows.Count > 0)
        {
            string status = dsApply.Tables[0].Rows[0]["STATUS"].ToString();

            if (status == "A" || status == "P" || status == "R")
            {                
                MessageBox("Record is already applied,you cannot apply again.");
                pnlMonth.Visible = true;
                pnlLeaveCard.Visible = true;
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }
        ds = objApp.GetIrregularityApplyBySrno(Fdate, Tdate, idno, deptno, collegeno, stno, count, SRNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtEmpName.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            txtDept.Text = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();
            txtIntime.Text = ds.Tables[0].Rows[0]["INTIME"].ToString();
            txtOutTime.Text = ds.Tables[0].Rows[0]["OUTTIME"].ToString();
            txtShiftIn.Text = ds.Tables[0].Rows[0]["SHIFTINTIME"].ToString();
            txtShiftOut.Text = ds.Tables[0].Rows[0]["SHIFTOUTTIME"].ToString();
            txtStatus.Text = ds.Tables[0].Rows[0]["IrrugulaityStatus"].ToString();
            txtWorkingHours.Text = ds.Tables[0].Rows[0]["HOURS"].ToString();
            DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTDATE"].ToString());
            txtDate.Text = date.ToString("dd/MM/yyyy");
            int shiftno = Convert.ToInt32(ds.Tables[0].Rows[0]["SHIFTNO"].ToString());
            Session["SHIFTNO"] = shiftno;
            pnlAdd.Visible = true;
            GetPAPath1();
            divInTime.Visible = true;
            divOutTime.Visible = true;
        }
        else
        {
            pnlAdd.Visible = false;
        }
        ViewState["action"] = "add";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlMonth.Visible = true;
        pnlLeaveCard.Visible = true;
        pnlAdd.Visible = false;
        txtEmpName.Text = string.Empty;
        txtDept.Text = string.Empty;
        txtIntime.Text = string.Empty;
        txtOutTime.Text = string.Empty;
        txtShiftIn.Text = string.Empty;
        txtShiftOut.Text = string.Empty;
        txtStatus.Text = string.Empty;
        txtWorkingHours.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtReason.Text = string.Empty;
    }

    protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();
            //DataSet dsAuth = new DataSet();
            int useridno = Convert.ToInt32(Session["idno"]);
            //int collegeno =Convert.ToInt32( objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + useridno + ""));
            int collegeno = Convert.ToInt32(Session["COLLEGE_NO"]);

            DataSet dspath = new DataSet();

            if (Convert.ToBoolean(ViewState["IsLeaveWisePassingPath"]) == true)
            {
                dspath = null;
                dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " AND Leavevalue=" + ViewState["LEAVENO"] + " ", "");
            }
            else
            {
                dspath = null;
                dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " AND isnull(Leavevalue,0) =" + 0 + " ", "");
            }
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
                txtPath.Text = string.Empty;
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

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objLeaves.EMPNO = Convert.ToInt32(Session["idno"]);
            objLeaves.APPDT = Convert.ToDateTime(DateTime.Now.Date);
           
            objLeaves.DATE = Convert.ToDateTime(txtDate.Text);
            objLeaves.STNO = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + objLeaves.EMPNO + " "));
            objLeaves.REASON = Convert.ToString(txtReason.Text);
            if (txtPath.Text.Equals(string.Empty))
            {
                objLeaves.PAPNO = 0;
            }
            else
            {
                objLeaves.PAPNO = Convert.ToInt32(ViewState["papno"]);
            }
            objLeaves.STATUS = "P";
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeaves.IRREGULARITYSTATUS = Convert.ToString(txtStatus.Text);
            Boolean ISDAILYWORKINGHOURS = Convert.ToBoolean(objCommon.LookUp("PAYROLL_STAFFTYPE", "ISNULL(ISDAILYWORKINGHOURS,0)AS ISDAILYWORKINGHOURS", "STNO=" + objLeaves.STNO + " "));
            if (ISDAILYWORKINGHOURS == true)
            {
            //if (objLeaves.STNO == 2)
            //{
                objLeaves.ISWEEKWISE = false;
                objLeaves.SERIALNO = Convert.ToInt32(Session["SRNO"]);
                objLeaves.INTIME = Convert.ToString(txtIntime.Text);
                objLeaves.OUTTIME = Convert.ToString(txtOutTime.Text);
                objLeaves.HOURS = Convert.ToString(txtWorkingHours.Text);
                objLeaves.SHIFTNO = Convert.ToInt32(Session["SHIFTNO"].ToString());
            }
            else
            {
                objLeaves.ISWEEKWISE = true;
                objLeaves.SERIALNO = Convert.ToInt32(Session["SERIALNO"]);
                objLeaves.INTIME = string.Empty;
                objLeaves.OUTTIME = string.Empty;
                objLeaves.HOURS = Convert.ToString(txtWorkingHours.Text);
                objLeaves.SHIFTNO = Convert.ToInt32(Session["SHIFTNO"].ToString());
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int ret_letrno = Convert.ToInt32(objApp.AddIrregularityEntry(objLeaves));
                    if (ret_letrno > 0)
                    {
                        MessageBox("Record Saved Successfully.");
                        ViewState["action"] = null;
                        BindListView();
                        Clear();
                    }
                    else
                    {
                        MessageBox("Record Already Exists!");
                        BindListView();
                        Clear();
                        return;
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                }
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void Clear()
    {
        pnlMonth.Visible = true;
        //pnlLeaveCard.Visible = false;
        pnlAdd.Visible = false;
        txtEmpName.Text = string.Empty;
        txtDept.Text = string.Empty;
        txtIntime.Text = string.Empty;
        txtOutTime.Text = string.Empty;
        txtShiftIn.Text = string.Empty;
        txtShiftOut.Text = string.Empty;
        txtStatus.Text = string.Empty;
        txtWorkingHours.Text = string.Empty;
        txtDate.Text = string.Empty;
        Session["SRNO"] = null;
        txtReason.Text = string.Empty;
        Session["SHIFTNO"] = null;
        Session["SERIALNO"] = null;
        //pnlDetail.Visible = false;
    }
    protected void btnIrregularApply_Click(object sender, EventArgs e)
    {
        Button btnIrregularApply = sender as Button;
        pnlMonth.Visible = false;
        pnlLeaveCard.Visible = false;
        pnlDetail.Visible = false;
        string IrregularStatus = Convert.ToString(btnIrregularApply.ToolTip);
        string ToDate = Convert.ToDateTime(txtMonthYear.Text).ToString("yyyy-MM-dd");
        DateTime dt = Convert.ToDateTime(ToDate);
        int month = dt.Month;
        int year = dt.Year;
        string frmdt = null;

        if (month == 1)
        {
            frmdt = "01" + "/" + month + "/" + year.ToString();
        }
        else
        {
            frmdt = "01" + "/" + month + "/" + year.ToString();
        }
        string todt = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).ToString();


        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(frmdt)));
        Fdate = Fdate.Substring(0, 10);
        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(todt)));
        Tdate = Tdate.Substring(0, 10);
        DataSet ds = null;
        int idno = Convert.ToInt32(Session["idno"].ToString());
        int deptno = 0;
        int collegeno = 0;
        int stno = 0;
        int count = 0;
        deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + idno + " "));
        collegeno = Convert.ToInt32(Session["COLLEGE_NO"].ToString());
        stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + idno + " "));
        int SERIALNO = Convert.ToInt32(btnIrregularApply.CommandArgument);
        Session["SERIALNO"] = SERIALNO;
        DataSet dsApply = new DataSet();
        dsApply = objCommon.FillDropDown("PAYROLL_IRREGULARITY_APP_ENTRY", "*", "", "SERIALNO=" + SERIALNO, "");
        if (dsApply.Tables[0].Rows.Count > 0)
        {
            string status = dsApply.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A" || status == "P" || status == "R")
            {               
                MessageBox("Record is already applied,you cannot apply again.");
                pnlMonth.Visible = true;
                pnlLeaveCard.Visible = false;
                pnlDetail.Visible = true;
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }
        ds = objApp.GetIrregularityApplyForTeaching(Fdate, Tdate, idno, deptno, collegeno, stno, count, SERIALNO);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtEmpName.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            txtDept.Text = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();
            txtShiftIn.Text = ds.Tables[0].Rows[0]["SHIFTINTIME"].ToString();
            txtShiftOut.Text = ds.Tables[0].Rows[0]["SHIFTOUTTIME"].ToString();
            txtStatus.Text = ds.Tables[0].Rows[0]["IrrugulaityStatus"].ToString();
            txtWorkingHours.Text = ds.Tables[0].Rows[0]["TOTALWORKINGHOUR"].ToString();
            DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENTDATE"].ToString());
            DateTime weekDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FROMDATE"].ToString());

            if (IrregularStatus == "Weekly Less Working")
            {
                txtDate.Text = weekDate.ToString("dd/MM/yyyy");
                divrequiredhours.Visible = true;
                txtrequiredhours.Text = "42.50";
            }
            else
            {
                txtDate.Text = date.ToString("dd/MM/yyyy");
                divrequiredhours.Visible = false;
            }
            int shiftno = Convert.ToInt32(ds.Tables[0].Rows[0]["SHIFTNO"].ToString());
            Session["SHIFTNO"] = shiftno;
            pnlAdd.Visible = true;
            GetPAPath1();
            //divInTime.Visible = false;
            //divOutTime.Visible = false;

            if (IrregularStatus == "Late Coming")
            {
                divInTime.Visible = true;
                txtIntime.Text = ds.Tables[0].Rows[0]["INTIME"].ToString();
            }
            else
            {
                divInTime.Visible = false;

            }
        }
        else
        {
            pnlAdd.Visible = false;
        }
        ViewState["action"] = "add";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtReason.Text = string.Empty;
    }
}