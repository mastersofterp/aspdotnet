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
using System.Globalization;


public partial class ESTABLISHMENT_LEAVES_Master_Holidays : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objHoliday = new LeavesController();

    //ArrayList datesList;// = new ArrayList();
    //string[,] schedDay = new string[13, 32];

    //ArrayList ;

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
        //txtto.Attributes.Add("onblur", "return caldiff(this);");
         //Protected datesArray As ArrayList;
         // 

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
                pnlList.Visible = true;
                FillPeriod();
                FillCollege();
                BindListViewHolidays();
                this.FillType();
                FillStaff();

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;

                btnAdd.Visible = true;
                btnShowReport.Visible = true;
                CheckPageAuthorization();
                //Set Report Parameters
               // objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");



            }
            
           
        }
        //blank div tag
        divMsg.InnerHtml = string.Empty;

        //if (ViewState["selectedDates"] == null)
        //{
        //    datesList = new ArrayList();
        //}
        //else
        //{
        //   datesList=(ArrayList) ViewState["selectedDates"] ;          
        //}

        // Prepare day(schedule) list before using it.

        //schedDay[1, 1] = "1st  Jan";
        //schedDay[1, 14] = "Makar Sankrant";
        //schedDay[1, 26] = "Republic Day";
        //schedDay[2, 14] = "Valentine Day";       
        //schedDay[4, 1] = "Fool Day";   
       
        //schedDay[5,1 ] = "Maharashtra Din";
        
        //schedDay[8, 15] = "Independence Day";
        //schedDay[9, 18] = "Sep -18";

        //schedDay[11, 2] = "Gurunanak Jayanti";
        //schedDay[11, 14] = "Children's Day";
        //schedDay[11, 28] = "Bakri Id";
        //schedDay[12, 25] = "Chirstmas";
        //schedDay[12, 31] = "The - End";

        //Set Report Parameters
        //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Holidays.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Holidays.aspx");
        }
    }
   
    protected void BindListViewHolidays()   //Modified By Saket Singh on 08/12/2016
    {
        try
        {
            DataSet ds = objHoliday.RetrieveAllHoliday(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                //dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
               // dpPager.Visible = true;
            }
            lvHoliday.DataSource = ds.Tables[0];
            lvHoliday.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillType()
    {
        try
        {
            objCommon.FillDropDownList(ddlHoliDayType, "PAYROLL_HOLIDAYTYPE", "HTNO", "HOLIDAYTYPE", "", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindListViewHolidays();
    //}
    private void Clear()
    {
        txtHoliday.Text = string.Empty;
        txtFromDt.Text = string.Empty;
        txtToDt.Text = string.Empty;
        txtYear.Text = string.Empty;
        ddlPeriod.SelectedIndex = 0;       
        ViewState["selectedDates"] = null;
        ddlCollege.SelectedIndex = ddlHoliDayType.SelectedIndex = ddlPeriod.SelectedIndex = ddlstaff.SelectedIndex = 0;
        chkRestrict.Checked = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        //Calendar1.SelectedDate="
        btnAdd.Visible = false;
        btnShowReport.Visible = false;

        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;

        //txtFromDt.Text = System.DateTime.Today.ToString(); //    System.DateTime.Now.ToString();
        ViewState["action"] = "add";
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
        //ListItem removeItem = ddlCollege.Items.FindByValue("0");
        //ddlCollege.Items.Remove(removeItem);

    }
    private void FillPeriod()
    {
        objCommon.FillDropDownList(ddlPeriod, "PAYROLL_LEAVE_PERIOD", "PERIOD", "PERIOD_NAME", "", "PERIOD");
    }
    public void FillStaff()
    {
        objCommon.FillDropDownList(ddlstaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "stno");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = CheckPurpose();

            Leaves objleaves = new Leaves();
            objleaves.HOLIDAYNAME = Convert.ToString(txtHoliday.Text);
            if (!txtFromDt.Text.Trim().Equals(string.Empty)) objleaves.HDT = Convert.ToDateTime(txtFromDt.Text);
            if (!txtToDt.Text.Trim().Equals(string.Empty)) objleaves.TODT = Convert.ToDateTime(txtToDt.Text);
            objleaves.HTNO =Convert.ToInt32(ddlHoliDayType.SelectedValue);
            //objleaves.HDT = Convert.ToDateTime(txtFromDt.Text);
            DateTime dt = Convert.ToDateTime (txtFromDt.Text);
            int yr = dt.Year;
            objleaves.YEAR = yr;
            objleaves.PERIOD = Convert.ToInt32(ddlPeriod.SelectedValue);
            objleaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objleaves.STNO = Convert.ToInt32 (ddlstaff.SelectedValue);
            objleaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            if (chkRestrict.Checked == true)
            {
                objleaves.STATUS = "Y";
            }
            else
            {
                objleaves.STATUS = "N ";
            }
            //Added by Sonal Banode on 25042023 
            objleaves.UANO = Convert.ToInt32(Session["userno"]);
            //
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        Clear();
                        return;
                    }
                    else
                    {
                        //CustomStatus cs = (CustomStatus)objHoliday.AddHoliday(objleaves);
                        CustomStatus cs = (CustomStatus)objHoliday.AddHolidayTest(objleaves);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                            BindListViewHolidays();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;
                        }
                    }
                }
                else
                {
                    if (ViewState["HNO"] != null)
                    {
                        objleaves.HNO = Convert.ToInt32(ViewState["HNO"].ToString());
                        CustomStatus cs = (CustomStatus)objHoliday.UpdateHoliday(objleaves);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                            BindListViewHolidays();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
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
       
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Int32 HNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(HNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

            btnAdd.Visible = false;
            btnShowReport.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 HNo = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objHoliday.DeleteHoliday(HNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Succesfully.");
                //return;
            }
            ViewState["action"] = null;
            BindListViewHolidays();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(Int32 HNO)
    {
        DataSet ds = null;
        try
        {
            ds = objHoliday.RetrieveSingleHoliday(HNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["HNO"] = HNO;
                txtHoliday.Text = ds.Tables[0].Rows[0]["HOLIDAYNAME"].ToString();
                txtFromDt.Text = ds.Tables[0].Rows[0]["DT"].ToString();
                txtYear.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                txtToDt.Text = ds.Tables[0].Rows[0]["TODATE"].ToString();
                ddlHoliDayType .SelectedValue = ds.Tables [0].Rows [0]["HTNO"].ToString ();
                ddlPeriod.SelectedValue = ds.Tables[0].Rows[0]["PERIOD"].ToString();
                ddlstaff.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                string status = ds.Tables[0].Rows[0]["RESTRICT_STATUS"].ToString();
                if (status == "Y")
                {
                    chkRestrict.Checked = true;
                }
                else
                {
                    chkRestrict.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Holidays_Entry", "ESTB_Holidays.rpt");
        }
        catch (Exception ex)
        {
          if (Convert.ToBoolean(Session["error"]) == true)
              objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");   
            
        }
    }



    //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    //{
    //    int index = -1;
    //    index = datesList.IndexOf(Calendar1.SelectedDate);
       
    //    if (index >= 0)
    //    {
    //      datesList.RemoveAt( index );
    //    }
    //    else
    //    { 
    //      datesList.Add(Calendar1.SelectedDate);  
    //    }
    //    ViewState["selectedDates"] = datesList;
    //    DisplaySelectedDates();
       
    //}
    //protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    //{
    //    CalendarDay day = (CalendarDay)e.Day;
    //    TableCell cell = (TableCell)e.Cell;
    //    if (!day.IsOtherMonth)
    //    {
    //        String dayStr = schedDay[day.Date.Month, day.Date.Day];
    //        if (dayStr != null)
    //        {
    //            // Format the Cell

    //            cell.BackColor = System.Drawing.Color.Tan;

    //            //Write some description about day

    //            cell.Controls.Add(new LiteralControl("<BR>" + dayStr));
                
    //        }
    //    }

    //}

    //protected void DisplaySelectedDates()
    //{
    //      Calendar1.SelectedDates.Clear();
    //      int i;
    //      for(i=0;i<=datesList.Count - 1;i++)
    //      {
    //          Calendar1.SelectedDates.Add(Convert.ToDateTime(datesList[i]));
    //      }
    //}
    // protected void Button1_Click(object sender, EventArgs e)
    // {
    //   String  s= "";
    //   foreach(DateTime dt1 in Calendar1.SelectedDates)
    //   { 
    //        s = s + dt1.ToString("dd/MM/yyyy") + "<br>";
    //   }
    //   labelOutput.Text = s;
    //   //Calendar1.DayRender
    //}
     //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
     //{
        
         
     //    int index = -1;
     //    index = datesList.IndexOf(Calendar1.SelectedDate);

     //    if (index >= 0)
     //    {
     //        datesList.RemoveAt(index);
     //    }
     //    else
     //    {
     //        //cell.Controls.Add(new LiteralControl("<BR>" + dayStr));   
     //        //cell.Controls.Add (new TextBox()  );
             
     //        datesList.Add(Calendar1.SelectedDate);
     //    }
     //    ViewState["selectedDates"] = datesList;
     //    DisplaySelectedDates();
     //}
    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        //if (txtFromDt.Text.ToString() != string.Empty && txtFromDt.Text.ToString() != "__/__/____" && txtToDt.Text.ToString() != string.Empty && txtToDt.Text.ToString() != "__/__/____")
        //{
        //    DateTime fromDate = Convert.ToDateTime(txtFromDt.Text.ToString());

        //    DateTime toDate = Convert.ToDateTime(txtToDt.Text.ToString());

        //    if (toDate < fromDate)
        //    {
        //        MessageBox("To Date Should Be Larger Than Or Equals To From Date");
        //        //txtTodt.Text = string.Empty;
        //        txtToDt.Text = string.Empty;
        //        return;
        //    }
        //}

        DateTime Test;
        if (DateTime.TryParseExact(txtToDt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtToDt.Text != string.Empty && txtToDt.Text != "__/__/____" && txtFromDt.Text != string.Empty && txtFromDt.Text != "__/__/____")
            {
                DateTime fromDate = Convert.ToDateTime(txtFromDt.Text.ToString());
                DateTime toDate = Convert.ToDateTime(txtToDt.Text.ToString());
               
                if (toDate < fromDate)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtToDt.Text = string.Empty;
                    return;
                }
            }
        }
    }


    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        string STATUS;
        if (chkRestrict.Checked == true)
        {
            STATUS = "Y";
        }
        else
        {
            STATUS = "N ";
        }

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDt.Text).ToString("yyyy-MM-dd")));
        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
        Fdate = Fdate.Substring(0, 10);
        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDt.Text).ToString("yyyy-MM-dd")));
        Tdate = Tdate.Substring(0, 10);

        //dsPURPOSE = objHoliday.CHECKSingleHoliday(txtFromDt.Text.ToString(), txtToDt.ToString(), ddlCollege.SelectedValue, ddlstaff.SelectedValue, txtHoliday.Text,);


        dsPURPOSE = objCommon.FillDropDown("PAYROLL_HOLIDAYS", "*", "", "HOLIDAYNAME='" + txtHoliday.Text + "' AND DT='" + Fdate +
           "' AND TODATE='" + Tdate + "' AND  COLLEGE_NO=" + ddlCollege.SelectedValue + " AND  STNO=" + ddlstaff.SelectedValue + " AND HTNO=" + ddlHoliDayType.SelectedValue
       + " AND PERIOD=" + ddlPeriod.SelectedValue + " AND RESTRICT_STATUS='" + STATUS + "'", "");

        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
}
