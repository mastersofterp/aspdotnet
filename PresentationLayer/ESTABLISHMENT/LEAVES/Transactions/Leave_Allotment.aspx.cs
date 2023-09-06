using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    Leaves objLM = new Leaves();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }
    protected void Page_Load(object sender, EventArgs e)
    {// btnSave.Attributes.Add("onClick", "fun();");
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
                FillPeriod();
                FillStaffType();
                FillYear();
                //FillLeave();
                FillUser();
                ddlEmp.SelectedIndex = 0;
                lvEmployees.Visible = false;
                FillCollege();
                CheckPageAuthorization();
            }
            //
        }
       
        //btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Leave_Allotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Leave_Allotment.aspx");
        }
    }
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");         
        //ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //ddlCollege.Items.Remove(removeItem);
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
    private void FillDepartment()
    {
        try
        {
            //select distinct E.SUBDEPTNO,DEPT.SUBDEPT from PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO) where E.COLLEGE_NO=1
            if (ddlStafftype.SelectedIndex > 0 && ddlCollege.SelectedIndex>=0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO="+Convert.ToInt32(ddlStafftype.SelectedValue) +"", "DEPT.SUBDEPT");
            }
            if (ddlCollege.SelectedIndex >= 0)
            {
                objCommon.FillDropDownList(ddldept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "DEPT.SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillYear()
    {
        int Yr = DateTime.Now.Year;
        //ddlYear.Items.Add("Please Select");
        ddlYear.Items.Add(Convert.ToString(Yr - 1));
        ddlYear.Items.Add(Convert.ToString(Yr));
        ddlYear.Items.Add(Convert.ToString(Yr + 1));
        //ddlYear.SelectedValue = (Convert.ToString(Yr));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int cs=0;
        Leaves objLM = new Leaves();
        string leave_name = string.Empty;
        string leave_code = string.Empty;
        bool service_period_status = false;
        int leave_session_srno = 0, leave_session_service_srno = 0;

        objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
        DataTable dtAppRecord = new DataTable();
        dtAppRecord.Columns.Add("IDNO");
        dtAppRecord.Columns.Add("LNO");
        dtAppRecord.Columns.Add("LEAVENO");
        dtAppRecord.Columns.Add("SESSION_SRNO");
        dtAppRecord.Columns.Add("LEAVES");

        try
        {
            if (radlTransfer.SelectedValue.ToString().Equals("S") && ddlEmp.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select Employee", this);
                ddlEmp.Focus();
                return;
            }
            else if (radlTransfer.SelectedValue.ToString().Equals("S"))
            {
               DataSet dsStNo = objCommon.FillDropDown("PAYROLL_EMPMAS", "isnull(STNO,0) as STNO", "IDNO", "IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue) + "", "");
               if (dsStNo.Tables[0].Rows.Count > 0)
               {
                   int stno = Convert.ToInt32(dsStNo.Tables[0].Rows[0]["STNO"].ToString());
                   if (stno == 0)
                   {
                       objCommon.DisplayMessage("Sorry! Employee Not Have Staff Type", this);
                       return;
                   }
               }
               else
               {
                   objCommon.DisplayMessage("Sorry! Employee Not Have Staff Type", this);
                   return;
               }
            }

            int Period = Convert.ToInt32(ddlPeriod.SelectedValue);
            int Year = Convert.ToInt32(ddlYear.SelectedItem.Text);
            int StNo = Convert.ToInt32(ddlStafftype.SelectedValue);
            int Lno = Convert.ToInt32(ddlLeavename.SelectedValue);

            int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + Lno)); 

            int Empid = Convert.ToInt32(ddlEmp.SelectedValue);

           
            DateTime ad = Convert.ToDateTime(txtFromDt.Text);
            DateTime pd = Convert.ToDateTime(txtToDt.Text);
           
            string lvnm = ddlLeavename.SelectedItem.Text;

          
            DataSet ds1 = objLeave.GetLnoThroughWord(leave_name, StNo, Period);//15-12-2014
           
            int prvlno = Convert.ToInt32(ds1.Tables[0].Rows[0]["LNO"]);
            int deptno = Convert.ToInt32 (ddldept.SelectedValue);
            DataSet dsLeave = objCommon.FillDropDown("Payroll_Leave_Name", "*", "", "LVNO=" + leaveno + "", "");
            if (dsLeave.Tables[0].Rows.Count > 0)
            {
                leave_code = (dsLeave.Tables[0].Rows[0]["Leave_ShortName"]).ToString();
                leave_name = (dsLeave.Tables[0].Rows[0]["Leave_Name"]).ToString();
            }
            //====================CODE TO TAKE SESSION_SRNO
            if (Convert.ToInt32(ViewState["yearly"]) == 2)
            {
                service_period_status = true;
            }
            objLM.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
            objLM.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);
            objLM.LEAVENO = leaveno;
            objLM.FROMDT = ad;
            objLM.TODT = pd;
            objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
             leave_session_srno = objLeave.AddGetLeaveSessionDetails(objLM);
         
            objLM.SESSION_SRNO = leave_session_srno;
            ViewState["SESSION_SRNO"] = leave_session_srno.ToString();
            objLM.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);
            objLM.APPDT = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            //========================
            if (service_period_status == false)
            {

                leave_session_srno = objLeave.AddGetLeaveSessionDetails(objLM);
                objLM.SESSION_SRNO = leave_session_srno;
                ViewState["SESSION_SRNO"] = leave_session_srno.ToString();
                ViewState["SESSION_SERVICE_SRNO"] = "0";
                objLM.SESSION_SERVICE_SRNO = 0;
                objLM.SERVICE_PERIOD_STATUS = false;
            }
            else if (radlTransfer.SelectedValue.ToString().Equals("S"))
            {
                DataSet dsRecord = objCommon.FillDropDown("PAYROLL_LEAVETRAN LT INNER JOIN PAYROLL_LEAVE L ON(LT.LNO=L.LNO) INNER JOIN PAYROLL_LEAVE_SESSION S ON(L.LEAVENO=S.LEAVENO) ", "DISTINCT L.LNO", "L.LEAVE", "LT.IDNO=" + Empid + "  AND ST=2 AND L.LEAVENO=" + leaveno + "  AND LT.SESSION_SRNO IN(SELECT SESSION_SRNO  FROM PAYROLL_LEAVE_SESSION S INNER JOIN PAYROLL_LEAVE L ON(L.LEAVENO = S.LEAVENO) where  '" + Convert.ToDateTime(txtFromDt.Text).ToString("yyyy-MM-dd") + "' =S.FDT AND '" + Convert.ToDateTime(txtToDt.Text).ToString("yyyy-MM-dd") + "' =S.TDT )", "L.LEAVE");
                int count = dsRecord.Tables[0].Rows.Count;
                if (dsRecord.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        string strShort = Convert.ToString(dsRecord.Tables[0].Rows[i]["LEAVE"]);
                        if (strShort == leave_code)
                        {
                            objCommon.DisplayMessage("Sorry! Allotment for this Leave Name already exists", this);
                            return;
                        }
                    }
                }
                string rdt = objCommon.LookUp("PAYROLL_EMPMAS", "RDT", "IDNO=" + Empid + "");
                if (rdt == string.Empty)
                {
                    //objCommon.DisplayUserMessage(updEntry, " Sorry ! Retirement date not found. Please contact to administrator", this);
                    //MessageBox("Sorry ! Retirement date not found for Employee Please contact to administrator");
                    MessageBox("Sorry ! Retirement date not found for Employee " + ddlEmp.SelectedItem.Text);

                    btnSave.Enabled = false;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }
                string doj = objCommon.LookUp("PAYROLL_EMPMAS", "DOJ", "IDNO=" + Empid + "");
                if (rdt == string.Empty)
                {
                    // objCommon.DisplayUserMessage(updEntry, " Sorry ! Joining date not found. Please contact to administrator", this);
                    // MessageBox("Sorry ! Joining date not found. Please contact to administrator");
                    MessageBox("Sorry ! Joining date not found for Employee " + ddlEmp.SelectedItem.Text);

                    btnSave.Enabled = false;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }
                leave_session_service_srno = 0;
                leave_session_srno = 0;
                objLM.SESSION_SERVICE_SRNO = leave_session_service_srno;
                ViewState["SESSION_SERVICE_SRNO"] = leave_session_srno.ToString();
                objLM.SERVICE_PERIOD_STATUS = true;
                objLM.SESSION_SRNO = 0;
                ViewState["SESSION_SRNO"] = "0";

            }


            if (radlTransfer.SelectedValue.ToString().Equals("M"))
            {
                int i = 0;
                foreach (ListViewDataItem items in lvEmployees.Items)
                {
                     CheckBox chkSelect1 = items.FindControl("chkSelect") as CheckBox;
                     if (chkSelect1.Checked)
                     {
                         i = 1;
                         break;
                     }
                    
                }
                if (i == 0)
                {
                    objCommon.DisplayMessage("Please Select Atleast One Employee", this.Page);
                    return;
                }
                objLM.PERIOD = Period;
                objLM.YEAR = Year;
                objLM.STNO = StNo;

                objLM.APPDT = ad;
                objLM.FROMDT = ad;
                objLM.TODT = pd;
                objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                objLM.SERVICE_PERIOD_STATUS = service_period_status;
                objLM.SESSION_SERVICE_SRNO = 0;
             
                foreach (ListViewDataItem items in lvEmployees.Items)
                {
                    CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
                    string name = string.Empty;
                    TextBox txtCreditLeaves = items.FindControl("txtCreditLeaves") as TextBox;
                    if (chkSelect.Checked)
                    {
                        Empid = Convert.ToInt32(chkSelect.ToolTip);
                        int StaffNo = 0; int STNO = 0;
                        DataSet dsStaff = objCommon.FillDropDown("PAYROLL_EMPMAS", "ISNULL(STNO,0) AS STNO", "ISNULL(STAFFNO,0) AS STAFFNO,FNAME+' '+MNAME+' '+LNAME AS NAME", "IDNO=" + Empid + "", "");
                        if (dsStaff.Tables[0].Rows.Count > 0)
                        {
                            STNO = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["STNO"]);
                            StaffNo = Convert.ToInt32(dsStaff.Tables[0].Rows[0]["STAFFNO"]);
                            if (STNO == 0)
                            {
                                objCommon.DisplayMessage("Sorry ! StaffType Not Found for Employee->" + dsStaff.Tables[0].Rows[0]["NAME"].ToString(), this);
                                return;
                            }
                            if (StaffNo == 0)
                            {
                                objCommon.DisplayMessage("Sorry ! Staff Not Found for Employee->" + dsStaff.Tables[0].Rows[0]["NAME"].ToString(), this);
                                return;
                            }
                        }
                        if (service_period_status == true)
                        {
                            DataRow dr = dtAppRecord.NewRow();
                            dr["IDNO"] = Empid;
                            dr["LNO"] = Lno;
                            dr["LEAVENO"] = leaveno;
                            dr["SESSION_SRNO"] = "0";
                            dr["LEAVES"] = Convert.ToDouble(txtCreditLeaves.Text);
                            dtAppRecord.Rows.Add(dr);
                            dtAppRecord.AcceptChanges();
                        }
                        else
                        {
                            DataRow dr = dtAppRecord.NewRow();
                            dr["IDNO"] = Empid;
                            dr["LNO"] = Lno;
                            dr["LEAVENO"] = leaveno;
                            dr["SESSION_SRNO"] = Convert.ToInt32(ViewState["SESSION_SRNO"]);//already got from show click event
                            dr["LEAVES"] = Convert.ToDouble(txtCreditLeaves.Text);
                            dtAppRecord.Rows.Add(dr);
                            dtAppRecord.AcceptChanges();
                        }
                        if (service_period_status == true)
                        {
                            string rdt = objCommon.LookUp("PAYROLL_EMPMAS", "RDT", "IDNO=" + Empid + "");
                            if (rdt == string.Empty)
                            {
                                // objCommon.DisplayUserMessage(updEntry, " Sorry ! Retirement date not found. Please contact to administrator", this);
                                MessageBox("Sorry ! Retirement date not found for Employee " + name);
                                btnSave.Enabled = false;
                                return;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }
                            string doj = objCommon.LookUp("PAYROLL_EMPMAS", "DOJ", "IDNO=" + Empid + "");
                            if (rdt == string.Empty)
                            {
                                //objCommon.DisplayUserMessage(updEntry, " Sorry ! Joining date not found. Please contact to administrator", this);
                                // MessageBox("Sorry ! Joining date not found. Please contact to administrator");
                                MessageBox("Sorry ! Joining date not found for Employee " + name);
                                btnSave.Enabled = false;
                                return;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                            }
                            leave_session_srno = 0;
                            btnSave.Enabled = true;
                        }       

                  }
                }
                cs = Convert.ToInt32(objLeave.CreditLeaves(objLM, dtAppRecord));//here Session_service_srno will create & insert through this procedure
                //cs = Convert.ToInt32(objLeave.CreditLeaves(Period, Year, StNo, Lno, ad, ad, pd, Convert.ToString(Session["colcode"]), Empid, prvlno, deptno, leaveno, Convert.ToInt32(ddlCollege.SelectedValue), StaffNo, leave_session_srno));

               }
            else
            {
                //int StaffNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "isnull(STAFFNO,0)", "IDNO=" + Empid)) ;//09-12-2014
                //objLM.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
                //objLM.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);               
                //objLM.LEAVENO = leaveno;
                //objLM.FROMDT = ad;
                //objLM.TODT = pd;
                //objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                //leave_session_srno = objLeave.AddGetLeaveSessionDetails(objLM);

                //cs = Convert.ToInt32(objLeave.CreditLeaves(Period, Year, StNo, Lno, ad, ad, pd, Convert.ToString(Session["colcode"]), Empid, prvlno, deptno, leaveno, Convert.ToInt32(ddlCollege.SelectedValue), StaffNo, leave_session_srno));
                int StaffNo = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "isnull(STAFFNO,0)", "IDNO=" + Empid));//09-12-2014
                objLM.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
                objLM.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);
                objLM.STNO = StNo;

                objLM.APPDT = ad;
                objLM.FROMDT = ad;
                objLM.TODT = pd;
                objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                objLM.SERVICE_PERIOD_STATUS = service_period_status;
                //Single with & without service base leave record
                DataRow dr = dtAppRecord.NewRow();
                dr["IDNO"] = Empid;
                dr["LNO"] = Convert.ToInt32(ddlLeavename.SelectedValue);
                dr["LEAVENO"] = leaveno;
                dr["SESSION_SRNO"] = Convert.ToInt32(ViewState["SESSION_SRNO"]);
                dr["LEAVES"] = Convert.ToDouble(txtLeaves.Text);
                dtAppRecord.Rows.Add(dr);
                dtAppRecord.AcceptChanges();


                //cs = Convert.ToInt32(objLeave.CreditLeaves(Period, Year, StNo, Lno, ad, ad, pd, Convert.ToString(Session["colcode"]), Empid, prvlno, deptno, leaveno, Convert.ToInt32(ddlCollege.SelectedValue), StaffNo, leave_session_srno, service_period_status, leave_session_service_srno, leaves));

                cs = Convert.ToInt32(objLeave.CreditLeaves(objLM, dtAppRecord));
            }

            if (cs == -99)
            {
                return;
            }
            if (cs == 2)
            {
                objCommon.DisplayMessage("Records Already Present", this);
                return;
            }

            if (cs == 1)
            {
                objCommon.DisplayMessage("Records Saved Successfully", this);
                Clear();

            } 
        }
        catch (Exception ex)
        {
          if(Convert.ToBoolean(Session["error"]) == true)
              objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.btnSave_click->" + ex.Message + " " + ex.StackTrace);
          else
                objUCommon.ShowError(Page,"Server UnAvailable");  
            
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void Clear()
    {
        ddlLeavename.SelectedIndex = 0;
        ddlStafftype.SelectedIndex = 0;
        ddlPeriod.SelectedIndex = 0;
        txtFromDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        ddlYear.SelectedIndex = 0;
        ddldept.SelectedIndex = 0;

        //radlTransfer.SelectedIndex = 0;
        if (radlTransfer.SelectedValue == "1")
        {
            btnSave.Visible = false;
        }
        ddlEmp.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
        radlTransfer.SelectedValue = "M";
        trSingleLeaveToCredit.Visible = false;
        tremp.Visible = false;
        btnShow.Visible = true;
        txtLeaves.Text = string.Empty;
        divnote.Visible = false;
        ddlStafftype.Enabled = true;
        ddldept.Enabled = true;
        btnSave.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlYear.Items.Clear();
        FillYear();
        Clear();
    }
    protected void ddlStafftype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        if (ddlStafftype.SelectedIndex != 0)
        {
            
            FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue));
            lvEmployees.Visible = false;
        }
        else
        {
            ddlLeavename.Items.Clear();
        }
        if (radlTransfer.SelectedValue == "S".ToString().Trim())
        {
            FillUser();
        }
        //function will call here
       // btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");

    }

    
    protected void FillLeaves(Int32 SType)
    {
        DataSet ds = null;
        int period = Convert.ToInt32(ddlPeriod.SelectedValue);
        try
        {
            ds = objLeave.GetLeavesSType(SType, period, Convert.ToChar(ddlGender.SelectedValue));
            ddlLeavename.Items.Clear();
            //ddlLeavename.Items.Add("ALL");
            ddlLeavename.Items.Add("Please Select");
            ddlLeavename.SelectedItem.Value = "0";

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLeavename.DataSource = ds.Tables[0];
                ddlLeavename.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlLeavename.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlLeavename.DataBind();
                ddlLeavename.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if(Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillLeaves->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page,"Server UnAvailable");  
            
        }
    }
    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //function will call here
    //   // btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");
    //}
    //protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //function will call here
    //   // btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");
    //}
    //protected void ddlLeavename_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //function will call here
    //   // btnSave.Attributes.Add("onClick", "ReceiveServerData(0);");
    //}

    //#region EventArgs
    //private string EventArgs
    //{
    //    get
    //    {
    //        return Request.Form["__EVENTARGUMENT"].ToString().ToUpper();
    //    }
    //}
    //#endregion EventArgs

    //#region EventTarget
    //private string EventTarget
    //{
    //    get
    //    {
    //        return Request.Form["__EVENTTARGET"].ToString().ToUpper();
    //    }
    //}
    //#endregion EventTarget

    private void FillUser()
    {
        try
        {
            if (ddlCollege.SelectedIndex >= 0 && ddlStafftype.SelectedIndex>0)
            {
                //SELECT STNO,STAFFNO,* FROM PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO) WHERE  
                    //P.APPOINTNO NOT IN(SELECT DISTINCT APPOINTNO FROM PAYROLL_LEAVE_CL_APPOINTMENT  WHERE COLLEGE_NO=@P_COLLEGE_NO)
                //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') as ENAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STNO="+Convert.ToInt32(ddlStafftype.SelectedValue) +"", "ENAME");
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", " ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.ACTIVE='Y' AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND E.STNO=" + Convert.ToInt32(ddlStafftype.SelectedValue) + " AND P.APPOINTNO NOT IN(SELECT DISTINCT APPOINTNO FROM PAYROLL_LEAVE_CL_APPOINTMENT  WHERE COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ")", "ENAME");
            }
            else if(ddlCollege.SelectedIndex>=0)
            {
                objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO AND P.PSTATUS='Y')", "E.IDNO", "ISNULL(E.FNAME,'')+' '+ISNULL(E.MNAME,'')+' '+ISNULL(E.LNAME,'') as ENAME", "E.ACTIVE='Y' AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND P.APPOINTNO NOT IN(SELECT DISTINCT APPOINTNO FROM PAYROLL_LEAVE_CL_APPOINTMENT  WHERE COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ")", "ENAME");
                //objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') as ENAME", "", "FNAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlEmp.SelectedIndex != 0)
            {
                ddlStafftype.SelectedValue = objLeave.GetStaffType(Convert.ToInt32(ddlEmp.SelectedValue)).ToString();
                FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue));
                lvEmployees.Visible = false;
                ddldept.SelectedValue = objCommon.LookUp("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO=" + Convert.ToInt32(ddlEmp.SelectedValue) + "");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.ddlEmp_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void radlTransfer_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (radlTransfer.SelectedValue.ToString().Equals("M"))
            {
                lvEmployees.Visible = false;
                btnShow.Visible = true;
                Button1.Visible = true;
                btnSave.Visible = false;
                tremp.Visible = false;
                ddlEmp.SelectedIndex = 0;
                ddldept.SelectedIndex = 0;
                ddlStafftype.SelectedIndex = 0;
                ddlPeriod.SelectedIndex = 0;
                //ddlStafftype.e
                trSingleLeaveToCredit.Visible = false;

                ddlStafftype.Enabled = true;
                ddldept.Enabled = true;
                divnote.Visible = false;
            }
            else
            {
                //trShow.Visible = false;
                divnote.Visible = true;
                lvEmployees.Visible = false;
                btnSave.Visible = true;
                btnShow.Visible = false;
                Button1.Visible = true;
                tremp.Visible = true;
                ddlEmp.SelectedIndex = 0;
                ddldept.SelectedIndex = 0;
                ddlStafftype.SelectedIndex = 0;
                ddlPeriod.SelectedIndex = 0;
                FillUser();
                trSingleLeaveToCredit.Visible = true;
                ddlStafftype.Enabled = false;
                ddldept.Enabled = false;
            }
            

        }
        catch (Exception ex)
        {
           if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.ddlEmp_SelectedIndexChanged ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        if (radlTransfer.SelectedValue.ToString().Equals("S"))
        {
            FillUser();
        }
        lvEmployees.Visible = false;
        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
    }
    
    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPeriod.SelectedIndex != 0)
        {
            FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue));
            lvEmployees.Visible = false;
        }
        else
        {
            ddlLeavename.Items.Clear();
        }
        if (ddlPeriod.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
        {
            DateTime frmdt; DateTime todt;
            int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
            int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), to);
            //int days = DateTime.DaysInMonth(year, month);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            year = year + 1;
            frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), from, 1);
            if (to < from)
            {
                todt = new DateTime(Convert.ToInt32(year), to, days);
            }
            else
            {
                todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), to, days);
            }
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }
       
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPeriod.SelectedValue == "1")
        //{
        //    DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 7, 1);
        //    DateTime todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 12, 31);
        //    txtFromDt.Text = frmdt.ToString();
        //    txtToDt.Text = todt.ToString();
        //}
        //else if (ddlPeriod.SelectedValue == "2")
        //{
        //    DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 1, 1);
        //    DateTime todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 6, 30);
        //    txtFromDt.Text = frmdt.ToString();
        //    txtToDt.Text = todt.ToString();
        //}
        //else if (ddlPeriod.SelectedValue == "3")
        //{
        //    int year = Convert.ToInt32(ddlYear.SelectedValue);
        //    year = year + 1;
        //    DateTime frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), 7, 1);
        //    DateTime todt = new DateTime(Convert.ToInt32(year), 6, 30);
        //    txtFromDt.Text = frmdt.ToString();
        //    txtToDt.Text = todt.ToString();
        //}
        //else
        //{
        //    txtFromDt.Text = System.DateTime.Now.ToString();
        //    txtToDt.Text = System.DateTime.Now.ToString();
        //}
        if (ddlPeriod.SelectedIndex > 0)
        {
            DateTime frmdt; DateTime todt;
            int from = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_from", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
            int to = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_PERIOD", "period_to", "period=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ""));
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), to);
            //int days = DateTime.DaysInMonth(year, month);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            year = year + 1;
            frmdt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), from, 1);
            if (to < from)
            {
                todt = new DateTime(Convert.ToInt32(year), to, days);
            }
            else
            {
                todt = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), to, days);
            }
            txtFromDt.Text = frmdt.ToString();
            txtToDt.Text = todt.ToString();
        }

         lvEmployees.Visible = false;
       
    }
    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLeaves(Convert.ToInt32(ddlStafftype.SelectedValue));
        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
        lvEmployees.Visible = false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {                    
         try
         {
             //ds = objLeave.GetEmployeeList(Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue),Convert.ToInt32(ddlCollege.SelectedValue));
             //if (ds.Tables[0].Rows.Count > 0)
             //{
             //    lvEmployees.Visible = true;
             //    lvEmployees.DataSource = ds;
             //    lvEmployees.DataBind();
             //}
             //else
             //{
             //    lvEmployees.DataSource = null;
             //    lvEmployees.DataBind();
             //    objCommon.DisplayMessage("Sorry!Record Not Found", this);
             //    return;
                 
             //}            

             int leave_session_srno = 0;
             bool service_period_status = false;
             btnSave.Visible = true;
             DataSet ds = null;
             int Period = Convert.ToInt32(ddlPeriod.SelectedValue);
             int Year = Convert.ToInt32(ddlYear.SelectedItem.Text);
             int StNo = Convert.ToInt32(ddlStafftype.SelectedValue);
             int Lno = Convert.ToInt32(ddlLeavename.SelectedValue);

             int leaveno = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE", "LEAVENO", "LNO=" + Lno));

             int Empid = Convert.ToInt32(ddlEmp.SelectedValue);


             DateTime ad = Convert.ToDateTime(txtFromDt.Text);
             DateTime pd = Convert.ToDateTime(txtToDt.Text);
             objLM.YEAR = Convert.ToInt32(ddlYear.SelectedValue);
             objLM.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);

             objLM.LEAVENO = leaveno;
             objLM.LNO = Lno;
             objLM.FROMDT = ad;
             objLM.TODT = pd;
             objLM.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
             if (ddlCollege.SelectedIndex > 0)
             {
                 objLM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
             }
             else
             {
                 objLM.COLLEGE_NO = 0;
             }
             leave_session_srno = objLeave.AddGetLeaveSessionDetails(objLM);
             ViewState["SESSION_SRNO"] = leave_session_srno;
             objLM.DEPTNO = Convert.ToInt32(ddldept.SelectedValue);
             objLM.STNO = Convert.ToInt32(ddlStafftype.SelectedValue);

             objLM.SEX = Convert.ToChar(ddlGender.SelectedValue);
             objLM.SESSION_SRNO = leave_session_srno;
             //ds = objLeave.GetEmployeeList(objLM);
             int yearly = 0;
             DataSet dsLeaveInfo = objCommon.FillDropDown("PAYROLL_LEAVE L INNER JOIN PAYROLL_LEAVE_NAME LV ON(L.LEAVENO=LV.LVNO)", "L.LEAVENO", "LV.Yearly", "L.LNO=" + Lno + "", "");
             if (dsLeaveInfo.Tables[0].Rows.Count > 0)
             {
                 yearly = Convert.ToInt32(dsLeaveInfo.Tables[0].Rows[0]["Yearly"]);
                 leaveno = Convert.ToInt32(dsLeaveInfo.Tables[0].Rows[0]["LEAVENO"]);
                 ViewState["LEAVENO"] = leaveno;
                 ViewState["yearly"] = yearly;
             }

             if (yearly == 2)//check service period
             {
                 service_period_status = true;
             }
             if (service_period_status == false)
             {

                 leave_session_srno = objLeave.AddGetLeaveSessionDetails(objLM);
                 ViewState["SESSION_SRNO"] = leave_session_srno;
             }
             else
             {
                 ViewState["SESSION_SRNO"] = "0";
             }
             
             
             ds = objLeave.GetEmployeeList_Allotment(objLM);
             // ds = objLeave.GetEmployeeList(Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlStafftype.SelectedValue),Convert.ToInt32(ddlCollege.SelectedValue));
             if (ds.Tables[0].Rows.Count > 0)
             {
                 lvEmployees.Visible = true;
                 lvEmployees.DataSource = ds;
                 lvEmployees.DataBind();
             }
             else
             {
                 lvEmployees.DataSource = null;
                 lvEmployees.DataBind();
                 objCommon.DisplayMessage("Sorry!Record Not Found", this);
                 return;
             }
             //Added by Sonal Banode on 20-09-2022 to check if leave is auto credit 
             Boolean IsCreditslotwise = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE", "isnull(IsCreditslotwise,0) as IsCreditslotwise", "LNO=" + Lno + "AND STNO=" + StNo));

             if (IsCreditslotwise == true)
             {
                 MessageBox("Leave Will Be Credited On Monthly Basis You Can Not Allot.");
                 ddlLeavename.SelectedIndex = 0;
                 ddlStafftype.SelectedIndex = 0;
                 ddlPeriod.SelectedIndex = 0;
                 // ddlYear.SelectedIndex = 0;
                 ddldept.SelectedIndex = 0;
                 ddlYear.SelectedIndex = 0;
                 txtFromDt.Text = txtToDt.Text = string.Empty;
                 btnSave.Visible = false;
                 return;
             }
             //
         }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment.btnShow->" + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");

         }
    }

    protected void ddlLeavename_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
        lvEmployees.Visible = false;
    }
}
