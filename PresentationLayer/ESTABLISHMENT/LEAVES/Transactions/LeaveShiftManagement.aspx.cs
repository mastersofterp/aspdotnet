//======================================================================================
// PROJECT NAME  : UIAMS
// MODULE NAME   : 
// PAGE NAME     : LeaveShiftManagement.aspx                                                    
// CREATION DATE : 
// CREATED BY    :                                      
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using IITMS.UAIMS;

public partial class ESTABLISHMENT_LEAVES_Transactions_LeaveShiftManagement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ShiftManagement objSME = new ShiftManagement();
    ShiftManagementController objSMC = new ShiftManagementController();

    ShiftInchargeEntity objSE = new ShiftInchargeEntity();
    ShiftInchargeController objSC = new ShiftInchargeController();
    string UsrStatus = string.Empty;

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillCollege();
                //FillIncharge();

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
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);


            //objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "I.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND INCHARGEEMPLOYEEIDNO>0", "E.FNAME");

        }
    }
    private void FillIncharge()
    {

        try
        {
            // objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "I.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND INCHARGEEMPLOYEEIDNO>0", "E.FNAME");
            if (ddlCollege.SelectedValue != string.Empty)
            {
                objSE.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                DataSet ds = objSC.GetInchargeList(objSE);

                ddlIncharge.Items.Clear();
                ddlIncharge.Items.Add("Please Select");
                ddlIncharge.SelectedItem.Value = "0";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlIncharge.DataSource = ds;
                    ddlIncharge.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlIncharge.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlIncharge.DataBind();
                    ddlIncharge.SelectedIndex = 0;
                }
                int empidno = Convert.ToInt32(Session["idno"]);

                ds = objCommon.FillDropDown("PAYROLL_SHIFT_INCHARGEMASTER", "*", "", "INCHARGEEMPLOYEEIDNO=" + empidno + "", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // ddlIncharge.SelectedValue = empidno.ToString();
                    objCommon.FillDropDownList(ddlIncharge, "PAYROLL_SHIFT_INCHARGEMASTER I INNER JOIN PAYROLL_EMPMAS E ON(I.INCHARGEEMPLOYEEIDNO = E.IDNO)", "INCHARGEEMPLOYEEIDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "INCHARGEEMPLOYEEIDNO=" + empidno + "", "E.FNAME");
                    ListItem removeItem = ddlIncharge.Items.FindByValue("0");
                    ddlIncharge.Items.Remove(removeItem);
                    //BindShiftAllocation();
                }
                lvIncharge.DataSource = null;
                lvIncharge.DataBind();
                pnlIncharge.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_LeaveShiftManagement.FillIncharge ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    private void BindShiftAllocation()
    {
        if (txtToDate.Text.ToString() != string.Empty && txtToDate.Text.ToString() != "__/__/____" && txtFromDate.Text.ToString() != string.Empty && txtFromDate.Text.ToString() != "__/__/____")
        {
            objSME.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
            objSME.TODATE = Convert.ToDateTime(txtToDate.Text);

            objSME.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSME.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);

            DataSet ds = objSMC.GetEmployeesForShiftManagement(objSME);
            if (ds.Tables[0].Rows.Count > 0)
            {
                trNote.Visible = true;
                lvIncharge.DataSource = ds;
                lvIncharge.DataBind();
                pnlIncharge.Visible = true;
                btnSave.Visible = true;
                DataSet dslm = objSMC.GetWeekDates(objSME);
                int i = 1;
                foreach (DataRow dr in dslm.Tables[0].Rows)
                {
                    Label lblHead = (Label)lvIncharge.Controls[0].Controls[0].FindControl("lbldate" + i);
                    // HiddenField hdnField = (HiddenField)lvIncharge.Controls[0].Controls[0].FindControl("hdnDay" + i + "Srno");
                    if (lblHead != null)
                    {
                        lblHead.Text = Convert.ToString(dr["THEDATE"]);
                        lblHead.ToolTip = dr["SHIFTDATE"].ToString();
                        //hdnField.Value = Convert.ToString(dr["DayId"]);
                    }
                    i = i + 1;
                }
            }
            else
            {
                trNote.Visible = false;
                lvIncharge.DataSource = null;
                lvIncharge.DataBind();
                pnlIncharge.Visible = false;
                btnSave.Visible = false;

            }
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillIncharge();
    }
    protected void ddlIncharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindShiftAllocation();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_LeaveShiftManagement.BindShiftAllocation ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //void lvIncharge_DataBound(object sender, EventArgs e)
    //{
    //    ((HtmlTableRow)lvIncharge.FindControl("lbldate1")).DataBind();
    //}

    //    Protected Sub ListView2_ItemDataBound1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) _
    //Handles ListView2.ItemDataBound
    //    If e.Item.ItemType = ListViewItemType.DataItem Then
    //        Dim dataitem As ListViewDataItem = DirectCast(e.Item, ListViewDataItem)
    //        Dim mstorename As String = DataBinder.Eval(dataitem.DataItem, "Store")
    //        If mstorename = "A1" Then
    //            Dim cell As HtmlTableRow = DirectCast(e.Item.FindControl("MainTableRow"), mlTableRow)
    //            cell.BgColor = #E0E0E0
    //        End If
    //    End If
    //End Sub
    //protected void lvIncharge_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        ListViewDataItem dataitem = (e.Item as ListViewDataItem);
    //    }

    //}


    protected void lvIncharge_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HiddenField hdnLock = (HiddenField)e.Item.FindControl("hdnLock");


            DropDownList ddlMon = (DropDownList)e.Item.FindControl("ddlDay1");
            DropDownList ddlTue = (DropDownList)e.Item.FindControl("ddlDay2");
            DropDownList ddlWed = (DropDownList)e.Item.FindControl("ddlDay3");
            DropDownList ddlThu = (DropDownList)e.Item.FindControl("ddlDay4");
            DropDownList ddlFri = (DropDownList)e.Item.FindControl("ddlDay5");
            DropDownList ddlSat = (DropDownList)e.Item.FindControl("ddlDay6");
            DropDownList ddlSun = (DropDownList)e.Item.FindControl("ddlDay7");

            HiddenField hdnday1 = (HiddenField)e.Item.FindControl("hdnday1");
            HiddenField hdnday2 = (HiddenField)e.Item.FindControl("hdnday2");//hdndayOff2
            HiddenField hdnday3 = (HiddenField)e.Item.FindControl("hdnday3");
            HiddenField hdnday4 = (HiddenField)e.Item.FindControl("hdnday4");
            HiddenField hdnday5 = (HiddenField)e.Item.FindControl("hdnday5");
            HiddenField hdnday6 = (HiddenField)e.Item.FindControl("hdnday6");
            HiddenField hdnday7 = (HiddenField)e.Item.FindControl("hdnday7");

            HiddenField hdndayOff2 = (HiddenField)e.Item.FindControl("hdndayOff2");//hdndayOff2
            HiddenField hdndayOff1 = (HiddenField)e.Item.FindControl("hdndayOff1");//hdndayOff2

            DropDownList ddlDayOff1 = (DropDownList)e.Item.FindControl("ddlDayOff1");
            DropDownList ddlDayOff2 = (DropDownList)e.Item.FindControl("ddlDayOff2");
            DropDownList ddlDayOff3 = (DropDownList)e.Item.FindControl("ddlDayOff3");
            DropDownList ddlDayOff4 = (DropDownList)e.Item.FindControl("ddlDayOff4");
            DropDownList ddlDayOff5 = (DropDownList)e.Item.FindControl("ddlDayOff5");
            DropDownList ddlDayOff6 = (DropDownList)e.Item.FindControl("ddlDayOff6");
            DropDownList ddlDayOff7 = (DropDownList)e.Item.FindControl("ddlDayOff7");

            if (ddlDayOff1.SelectedValue == "0")
            {

                //ddlDayOff1.BackColor = System.Drawing.Color.Blue;
                //ddlDayOff1.ForeColor = System.Drawing.Color.White;

                ddlDayOff1.BackColor = System.Drawing.Color.White;
                ddlDayOff1.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                ddlDayOff1.BackColor = System.Drawing.Color.Blue;
                ddlDayOff1.ForeColor = System.Drawing.Color.White;
            }

            //objCommon.FillDropDownList(ddlMon, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlMon, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlMon.SelectedValue = hdnday1.Value;
            if (ddlMon.SelectedValue == "0")
            {
                ddlMon.ForeColor = System.Drawing.Color.Red;
                //ddlMon.Enabled = false;
            }
            else
            {
                ddlMon.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlMon.Enabled = false; ddlDayOff1.Enabled = false;
            }

           // objCommon.FillDropDownList(ddlTue, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlTue, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlTue.SelectedValue = hdnday2.Value;

            if (ddlTue.SelectedValue == "0")
            {
                ddlTue.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlTue.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlTue.Enabled = false; ddlDayOff2.Enabled = false;
            }
            //hdndayOff2
            ddlDayOff2.SelectedValue = hdndayOff2.Value;
            ddlDayOff1.SelectedValue = hdndayOff1.Value;


            //objCommon.FillDropDownList(ddlWed, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlWed, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlWed.SelectedValue = hdnday3.Value;
            if (ddlWed.SelectedValue == "0")
            {
                ddlWed.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlWed.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlWed.Enabled = false; ddlDayOff3.Enabled = false;
            }
            //objCommon.FillDropDownList(ddlThu, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlThu, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlThu.SelectedValue = hdnday4.Value;
            if (ddlThu.SelectedValue == "0")
            {
                ddlThu.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlThu.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlThu.Enabled = false; ddlDayOff4.Enabled = false;
            }
            //objCommon.FillDropDownList(ddlFri, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlFri, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlFri.SelectedValue = hdnday5.Value;
            if (ddlFri.SelectedValue == "0")
            {
                ddlFri.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlFri.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlFri.Enabled = false; ddlDayOff5.Enabled = false;
            }
            //objCommon.FillDropDownList(ddlSat, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlSat, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlSat.SelectedValue = hdnday6.Value;
            if (ddlSat.SelectedValue == "0")
            {
                ddlSat.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlSat.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlSat.Enabled = false; ddlDayOff6.Enabled = false;
            }
            //objCommon.FillDropDownList(ddlSun, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "SHIFTNO>0", "SHIFTNAME");
            objCommon.FillDropDownList(ddlSun, "PAYROLL_SHIFTMASTER", "SHIFTNO", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedIndex), "SHIFTNAME");
            ddlSun.SelectedValue = hdnday7.Value;
            if (ddlSun.SelectedValue == "0")
            {
                ddlSun.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlSun.ForeColor = System.Drawing.Color.Black;
            }
            if (hdnLock.Value == "Y")
            {
                ddlSun.Enabled = false; ddlDayOff7.Enabled = false;
            }
        }
    }

    //protected void FillDropDownList()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO>0", "COLLEGE_NAME");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_LeaveInchargeMaster.FillDropDownList ->" + ex.Message + "  " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //btnCancel_Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = txtToDate.Text = string.Empty;
        ddlCollege.SelectedIndex = ddlIncharge.SelectedIndex = 0;
        lvIncharge.DataSource = null;
        lvIncharge.DataBind();
        pnlIncharge.Visible = false;
        trNote.Visible = false;
    }
    //btnReport_Click
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Shift_Mgt", "Estb_ShiftMgtReport.rpt");//ShiftMgtReportFormat2
    }
    protected void btnReportFormat2_Click(object sender, EventArgs e)
    {
        ShowReport("Shift_Mgt", "ShiftMgtReportFormat2.rpt");//ShiftMgtReportFormat2
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string frmdt = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
            string todt = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,SHIFT," + rptFileName;


            url += "&param=@P_FROMDT=" + frmdt + ",@P_TODT=" + todt + ",@P_INCHARGEEMPLOYEEIDNO=" + Convert.ToInt32(ddlIncharge.SelectedValue) + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            // url += "&param=@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_PERIOD=" + Convert.ToInt32(ddlPeriod.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_LEAVENO=" + Convert.ToInt32(ddlLeaveName.SelectedValue) + ",@P_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + ",@P_STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            /*Insert Event Log Method*/
            CustomStatus css = (CustomStatus)objSMC.InsertEventlog(System.IO.Path.GetFileName(Request.Url.AbsolutePath), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["username"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["ipAddress"].ToString(), Session["macAddress"].ToString());

            GetDateRange();
            DateTime frmdate = Convert.ToDateTime(txtFromDate.Text);
            DateTime todate = Convert.ToDateTime(txtToDate.Text);

            //string month=frmdate.Month
            //string monthName = new DateTime(2010, 8, 1).ToString("MMM", CultureInfo.InvariantCulture);


            DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("INCHARGEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("EMPLOYEEIDNO", typeof(int)));
            dt.Columns.Add(new DataColumn("SHIFTDATE", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SHIFTNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COLLEGE_NO", typeof(int)));
            dt.Columns.Add(new DataColumn("IsDayOff", typeof(bool)));

            int empid = 0;


            foreach (ListViewDataItem lvitem in lvIncharge.Items)
            {
                DateTime shiftDate;
                shiftDate = Convert.ToDateTime(txtFromDate.Text);

                for (int count = 1; count <= 7; count++)
                {
                    DropDownList ddlDay = lvitem.FindControl("ddlDay" + count) as DropDownList;
                    DropDownList ddlDayOff = lvitem.FindControl("ddlDayOff" + count) as DropDownList;

                    Label lblEmp = lvitem.FindControl("lblEmp") as Label;


                    if (count == 1)
                    {
                        empid = Convert.ToInt32(lblEmp.ToolTip);
                    }

                    if (count > 1 && count <= 7)
                    {
                        shiftDate = shiftDate.AddDays(1);
                    }


                    objSME.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);
                    objSME.EMPLOYEEIDNO = empid;
                    objSME.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

                    DataRow dr = dt.NewRow();
                    objSME.INCHARGEEMPLOYEEIDNO = Convert.ToInt32(ddlIncharge.SelectedValue);

                    dr["EMPLOYEEIDNO"] = empid;
                    dr["SHIFTDATE"] = shiftDate;
                    dr["SHIFTNO"] = ddlDay.SelectedValue;
                    dr["COLLEGE_NO"] = Convert.ToInt32(ddlCollege.SelectedValue);

                    if (ddlDayOff.SelectedValue == "1")
                    {
                        dr["IsDayOff"] = true;
                    }
                    else
                    {
                        dr["IsDayOff"] = false;
                    }

                    dt.Rows.Add(dr);

                }
            }
            //txtFromDate.Text

            CustomStatus cs = (CustomStatus)objSMC.InsertShiftManagementDetails(dt, objSME);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                // BindListView();
                objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_LeaveShiftManagement.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private List<DateTime> GetDateRange()
    {
        if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
        {
            return null;
        }
        List<DateTime> rv = new List<DateTime>();
        DateTime tmpDate = Convert.ToDateTime(txtFromDate.Text);
        do
        {
            rv.Add(tmpDate);
            tmpDate = tmpDate.AddDays(1);
        } while (tmpDate <= Convert.ToDateTime(txtToDate.Text));
        return rv;
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text.ToString() != string.Empty && txtFromDate.Text.ToString() != "__/__/____")
            {

                DateTime frmdate = Convert.ToDateTime(txtFromDate.Text);
                DateTime todate = frmdate.AddDays(6);
                txtToDate.Text = Convert.ToDateTime(todate).ToString("dd/MM/yyyy");//dd/MM/yyyy
                BindShiftAllocation();
                //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

                //string frmMonthName = mfi.GetMonthName(frmdate.Month).ToString();
                //frmMonthName = frmMonthName.Substring(0, 3);
                //frmMonthName = frmMonthName + "" + frmdate.Year;
                //DataTable tbl = new DataTable();              
                //tbl.Columns.Add(new DataColumn("MonYear", typeof(string)));


                //DataRow dr = tbl.NewRow();
                //dr["MonYear"] = frmMonthName;
                //tbl.Rows.Add(dr);
                //tbl.AcceptChanges();


                //string toMonthName = mfi.GetMonthName(todate.Month).ToString();
                //toMonthName = toMonthName.Substring(0, 3);
                //toMonthName = toMonthName + "" + todate.Year;               
                ////tbl.Columns.Add(new DataColumn("MonYear", typeof(string)));


                //dr = tbl.NewRow();
                //dr["MonYear"] = toMonthName;
                //tbl.Rows.Add(dr);
                //tbl.AcceptChanges();

                //objCommon.FillDropDown("PAYROLL_ATTENDFILE ATT INNER JOIN "+tbl+" T ON(ATT.MONYEAR=T.MonYear)",""

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_LeaveShiftManagement.txtFromDate_TextChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
}