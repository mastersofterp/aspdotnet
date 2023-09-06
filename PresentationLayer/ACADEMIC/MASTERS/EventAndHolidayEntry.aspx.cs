//======================================================================================
// PROJECT NAME  : UAIMS [GHREC]                                                         
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Event And Holidays Entry From.                                      
// CREATION DATE : 04-FEB-2011                                                          
// CREATED BY    : ANUP V. KSHIRSAGAR                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_MASTERS_EventAndHolidayEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    Exam objExam = new Exam();
    TeachingPlanController objTPC = new TeachingPlanController();
    string spName = string.Empty; string spParameters = string.Empty; string spValue = string.Empty;
    Session objSession = new Session();
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
        try
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
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //this.BindListView();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    mandatory.Visible = true;
                    bindHolidayList();
                    objCommon.FillDropDownList(ddlHolidayList, "ACD_ACADEMIC_HOLIDAY_LIST", "HOLIDAY_LISTNO", "HOLIDAY_NAME", "HOLIDAY_FOR='H'", "HOLIDAY_LISTNO");
                    this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
                    BindListView();
                }
            }
            //this.FillDropdown();
            //this.BindListView();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID DESC");
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ChkDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDate.Checked)
        {
            //tdlblToDate.Visible = true; tdlblToDate.ColSpan = 2;
            tdToDate.Visible = true; //tdToDate.ColSpan = 2;
            lblFromDate.Text = "From Date";
            mvFromDate.EmptyValueMessage = "Please Enter From Date";
            mvFromDate.ErrorMessage = "Please Enter From Date";
        }
        else
        {
            if (rbnHoliday.Checked)
            {
                mvFromDate.EmptyValueMessage = "Please Enter Holiday Date";
                mvFromDate.ErrorMessage = "Please Enter Holiday Date";
            }
            else if (rbnEvent.Checked)
            {
                mvFromDate.EmptyValueMessage = "Please Enter Event Date";
                mvFromDate.ErrorMessage = "Please Enter Event Date";
            }
            else
            {
                mvFromDate.EmptyValueMessage = "Please Enter Suspend Class Date";
                mvFromDate.ErrorMessage = "Please Enter Suspend Class Date";
            }
            //tdlblToDate.Visible = false;
            tdToDate.Visible = false;
            if (rbnHoliday.Checked)
            {
                lblFromDate.Text = "Holiday Date";
            }
            else
            {
                lblFromDate.Text = "Event Date";
            }

        }
        //this.BindListView();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != string.Empty | txtToDate.Text != string.Empty)
            {
                hiddenfieldfromDt.Value = txtFromDate.Text;
                hiddenFieldToDt.Value = txtToDate.Text;
                if (ChkDate.Checked)
                {
                    if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value) | (Convert.ToDateTime(txtToDate.Text) > Convert.ToDateTime(hiddenFieldToDt.Value)) | (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value))))
                    {
                        objCommon.DisplayMessage(updHoliday, "Select Date in Proper Range", this.Page);
                        return;
                    }
                    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                    {
                        objCommon.DisplayMessage(updHoliday, "From Date should be Lesser than To Date", this.Page);
                        return;
                    }
                }
                else
                {
                    if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(hiddenfieldfromDt.Value)))
                    {
                        objCommon.DisplayMessage(updHoliday, "Select Date in Proper Range", this.Page);
                        return;
                    }
                }
                string collegenos = string.Empty;
                foreach (ListItem itm in ddlCollege.Items)
                {
                    if (itm.Selected != true)
                        continue;
                    collegenos += itm.Value + ",";
                }
                //Set all properties
                objSession.Session_SDate = Convert.ToDateTime(txtFromDate.Text);//HOLIDAY START DT
                if (ChkDate.Checked)
                    objSession.Session_EDate = Convert.ToDateTime(txtToDate.Text);///HOLIDAY END DT
                else
                    objSession.Session_EDate = DateTime.MinValue;
                objSession.College_code = Session["colcode"].ToString();//
                objSession.CurrentSession = Convert.ToInt32(ddlSession.SelectedValue);//
                objSession.Event_Detail = txtEventDetail.Text;
                objSession.Event_Name = ddlHolidayList.SelectedItem.Text;//txtEventName.Text;
                //objSession.College_Id = Convert.ToInt32(ddlCollege.SelectedValue);
                objSession.SessionNo = Convert.ToInt32(ddlHolidayList.SelectedValue);

                if (rbnHoliday.Checked)
                {
                    objSession.Holiday_Event = 1;
                }
                if (rbnEvent.Checked)
                {
                    objSession.Holiday_Event = 2;
                    ChkDate.Checked = false;
                }
                //Added by Dileep on 20032020
                if (rbnSuspendClass.Checked)
                {
                    objSession.Holiday_Event = 3;
                    ChkDate.Checked = false;
                }
                int cancelstatus = 0;
                if (chkcancel.Checked)
                {
                    cancelstatus = 1;
                }
                else
                {
                    cancelstatus = 0;
                }

                //Check for add or edit
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    //if (CheckDuplicateEntry() == true)
                    //{
                    //    objCommon.DisplayMessage(updHoliday, "Entry for this Date Already Done!", this.Page);
                    //    return;
                    //}
                    //Edit 
                    //objSession.SessionNo = Convert.ToInt32(ViewState["sessionno"]);//ACADEMIC SESSIONNO
                    //objSession.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                    //objSession.Holiday_No = Convert.ToInt32(ViewState["groupid"]);//Holiday no.
                    int groupid = Convert.ToInt32(ViewState["groupid"]);

                    CustomStatus cs = (CustomStatus)objTPC.UpdateHolidayDetails(objSession, collegenos, groupid, cancelstatus);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Updated Successfully!", this.Page);
                    }
                }
                else
                {
                    //if (CheckDuplicateEntry() == true)
                    //{
                    //    objCommon.DisplayMessage(updHoliday, "Entry for this Date Already Done!", this.Page);
                    //    return;
                    //}



                    //Add New
                    CustomStatus cs = (CustomStatus)objTPC.AddHolidayDetails(objSession, collegenos, cancelstatus);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(updHoliday, "Record Already Exists", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(updHoliday, "Transaction Failed", this.Page);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(updHoliday, "Please Enter Date", this.Page);
                return;
            }

            this.BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string Academic_Session;
            if (ChkDate.Checked)
                //Academic_Session = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "CONVERT(nvarchar,ACADEMIC_HOLIDAY_STDATE,103)= dbo.DateOnly('" + Convert.ToDateTime(txtFromDate.Text.Trim()) + "')  AND CONVERT(nvarchar,ACADEMIC_HOLIDAY_ENDDATE,103) = dbo.DateOnly('" + Convert.ToDateTime(txtToDate.Text.Trim()) + "') ");
                Academic_Session = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "ACADEMIC_HOLIDAY_STDATE ='" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy/MM/dd") + "' and ACADEMIC_HOLIDAY_ENDDATE ='" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy/MM/dd") + "' AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + "");
            else

                Academic_Session = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "ACADEMIC_HOLIDAY_STDATE='" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy/MM/dd") + "' AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + "");

            //else
            //Academic_Session = objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", " CONVERT(nvarchar,ACADEMIC_HOLIDAY_STDATE,103) = dbo.DateOnly('" + Convert.ToDateTime(txtFromDate.Text.Trim()) + "')");

            if (Academic_Session != null && Academic_Session != string.Empty)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return flag;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int holidayno = int.Parse(btnEdit.CommandArgument);
            ViewState["groupid"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(holidayno);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            TeachingPlanController objTPC = new TeachingPlanController();
            ImageButton btnDelete = sender as ImageButton;
            int Sessionno = int.Parse(btnDelete.CommandArgument);



            //Delete 

            CustomStatus cs = (CustomStatus)objTPC.DeleteAcademicHoliday(Convert.ToInt32(Sessionno));
            objCommon.DisplayMessage(updHoliday, "Entry Deleted Successfully !!", this.Page);
            this.BindListView();
            this.ClearControls();
            return;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int Holidayno)
    {
        try
        {
            TeachingPlanController objTP = new TeachingPlanController();
            SqlDataReader dr = objTP.GetSingleAcademicHoliday(Holidayno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtEventDetail.Text = dr["ACADEMIC_HOLIDAY_DETAIL"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_DETAIL"].ToString();
                    //txtEventName.Text = dr["ACADEMIC_HOLIDAY_NAME"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_NAME"].ToString();\
                    ddlHolidayList.SelectedValue = dr["HOLIDAYLIST_NO"].ToString();
                    txtFromDate.Text = dr["ACADEMIC_HOLIDAY_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtToDate.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    ddlSession.SelectedValue = dr["SESSIONID"].ToString();
                    //ddlAcademicSession.SelectedValue = dr["ACADEMIC_SESSIONNO"].ToString();

                    CourseController objStud = new CourseController();
                    DataSet dsCollegeSession = objStud.GetCollegeBySessionid(1, Convert.ToInt32(ddlSession.SelectedValue));
                    ddlCollege.Items.Clear();
                    ddlCollege.DataSource = dsCollegeSession;
                    ddlCollege.DataValueField = "COLLEGE_ID";
                    ddlCollege.DataTextField = "COLLEGE_NAME";
                    ddlCollege.DataBind();

                    char delimiterChars = ',';
                    string collegeids = dr["COLLEGE_ID"].ToString();
                    string[] college = collegeids.Split(delimiterChars);
                    for (int j = 0; j < college.Length; j++)
                    {
                        for (int i = 0; i < ddlCollege.Items.Count; i++)
                        {
                            if (college[j] == ddlCollege.Items[i].Value)
                            {
                                ddlCollege.Items[i].Selected = true;
                            }
                        }
                    }
                    if (dr["ACADEMIC_HOLIDAY_ENDDATE"] == null | dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString().Equals(""))
                    {
                        //tdlblToDate.Visible = false;
                        tdToDate.Visible = false;
                        ChkDate.Checked = false;
                    }
                    else
                    {
                        //tdlblToDate.Visible = true; tdlblToDate.ColSpan = 2;
                        ChkDate.Checked = true;
                        tdToDate.Visible = true;
                        //tdToDate.ColSpan = 2;
                        lblFromDate.Text = "From Date";
                    }
                }
            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControls()
    {
        //ddlAcademicSession.SelectedIndex = 0;
        //ddlCollege.SelectedIndex = 0;
        //ddlSession.Items.Clear();
        //ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.SelectedIndex = 0;
        ddlCollege.Items.Clear();
        ddlHolidayList.SelectedIndex = 0;
        txtEventDetail.Text = string.Empty;
        txtEventName.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ViewState["action"] = null;
        lvExamday.DataSource = null;
        tdToDate.Visible = false;
        chkcancel.Checked = false;
        ChkDate.Checked = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Fills Academic Session Name
        //objCommon.FillDropDownList(ddlAcademicSession, "ACD_ACADEMIC_SESSION_MASTER", "ACADEMIC_SESSIONNO", "ACADEMIC_SESSION_PNAME", "ACADEMIC_SESSIONNO > 0 AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "ACADEMIC_SESSIONNO");        
        //this.BindListView();
        //ddlAcademicSession.Focus();
        CourseController objStud = new CourseController();
        DataSet dsCollegeSession = objStud.GetCollegeBySessionid(1, Convert.ToInt32(ddlSession.SelectedValue));
        ddlCollege.Items.Clear();
        ddlCollege.DataSource = dsCollegeSession;
        ddlCollege.DataValueField = "COLLEGE_ID";
        ddlCollege.DataTextField = "COLLEGE_NAME";
        ddlCollege.DataBind();
    }

    private void BindListView()
    {
        try
        {
            int Holiday_Event = 0;
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            if (rbnHoliday.Checked)
            {
                Holiday_Event = 1;
            }
            if (rbnEvent.Checked)
            {
                Holiday_Event = 2;
            }
            //added by Dileep 20032020
            if (rbnSuspendClass.Checked)
            {
                Holiday_Event = 3;
            }
            // DataSet ds = objTPC.GetAllHOLIDAY(Convert.ToInt32(ddlSession.SelectedValue), Holiday_Event, Convert.ToInt32(ddlCollege.SelectedValue),OrgId);
            string spName = string.Empty; string spParamaters = string.Empty; string spValue = string.Empty;
            DataSet ds = null;
            spName = "PKG_ACADEMIC_SESSION_SP_ALL_HOLIDAY_MAPPING";
            spParamaters = "@P_IS_HOLIDAY";
            spValue = "" + Holiday_Event + "";
            ds = objCommon.DynamicSPCall_Select(spName, spParamaters, spValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvExamday.Visible = true;
                lvExamday.DataSource = ds;
                lvExamday.DataBind();
            }
            else
            {
                lvExamday.Visible = false;
                lvExamday.DataSource = null;
                lvExamday.DataBind();
            }


            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvExamday);//Set label 
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        this.BindListView();
    }

    protected void SelectDate(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsWeekend)
        {
            e.Day.IsSelectable = false;
        }
    }

    //protected void ddlAcademicSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TeachingPlanController objTP = new TeachingPlanController();
    //        SqlDataReader dr = objTP.GetSingleAcademicSession(Convert.ToInt32(ddlAcademicSession.SelectedValue));
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {

    //                hiddenfieldfromDt.Value = dr["ACADEMIC_SESSION_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_SESSION_STDATE"].ToString()).ToString("dd/MM/yyyy");
    //                hiddenFieldToDt.Value = dr["ACADEMIC_SESSION_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_SESSION_ENDDATE"].ToString()).ToString("dd/MM/yyyy");

    //                frmDt.Text = hiddenfieldfromDt.Value;
    //                toDt.Text = hiddenFieldToDt.Value;
    //            }
    //        }
    //        this.BindListView();
    //        if (dr != null) dr.Close();
    //    //use hungarian date format, for example 2006. 07. 13. 
    //      //  System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hu-HU");

    //        //rvFromDt.MinimumValue = hiddenfieldfromDt.Value.ToString();
    //        //rvFromDt.MaximumValue = hiddenFieldToDt.Value.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("rptAcademicCalendar", "rptAcademicCalendar.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //Shows report
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updHoliday, this.updHoliday.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void rbnHoliday_CheckedChanged(object sender, EventArgs e)
    {
        lblFromDate.Text = "Holiday Date";
        lblholiday.Text = "Holiday List";
        mvFromDate.EmptyValueMessage = "Please Enter Holiday Date";
        mvFromDate.ErrorMessage = "Please Enter Holiday Date";
        RequiredFieldValidator4.ErrorMessage = "Please Select Holiday List";
        
        lblmultiple.InnerText = "For Multiple Days Holiday";
        lblDetails.InnerText = "Holiday Detail (if any)";
        ClearControls();
        ChkDate.Checked = false;
        mandatory.Visible = true;
        lvExamday.DataSource = null;
        lvExamday.DataBind();
        divholidayMapping.Visible = true;
        divholidaymaster.Visible = false;
        objCommon.FillDropDownList(ddlHolidayList, "ACD_ACADEMIC_HOLIDAY_LIST", "HOLIDAY_LISTNO", "HOLIDAY_NAME", "HOLIDAY_FOR='H'", "HOLIDAY_LISTNO");
        BindListView();
        if (ChkDate.Checked)
        {
            mvFromDate.EmptyValueMessage = "Please Enter From Date";
            mvFromDate.ErrorMessage = "Please Enter From Date";
        }
    }
    protected void rbnEvent_CheckedChanged(object sender, EventArgs e)
    {
        lblFromDate.Text = "Event Date";
        lblholiday.Text = "Event List";
        mvFromDate.EmptyValueMessage = "Please Enter Event Date";
        mvFromDate.ErrorMessage = "Please Enter Event Date";
        
        RequiredFieldValidator4.ErrorMessage = "Please Select Event List";
        lblmultiple.InnerText = "For Multiple Days Event";
        lblmultiple.Attributes.Add("style", "font-weight:bold");
        lblDetails.InnerText = "Event Detail (if any)";
        lblDetails.Attributes.Add("style", "font-weight:bold");
        ClearControls();
        ChkDate.Checked = false;
        mandatory.Visible = true;
        lvExamday.DataSource = null;
        lvExamday.DataBind();
        divholidayMapping.Visible = true;
        divholidaymaster.Visible = false;
        objCommon.FillDropDownList(ddlHolidayList, "ACD_ACADEMIC_HOLIDAY_LIST", "HOLIDAY_LISTNO", "HOLIDAY_NAME", "HOLIDAY_FOR='E'", "HOLIDAY_LISTNO");
        BindListView();
        if (ChkDate.Checked)
        {
            mvFromDate.EmptyValueMessage = "Please Enter From Date";
            mvFromDate.ErrorMessage = "Please Enter From Date";
        }
    }
    //added By Dileep on 20032020
    protected void rbnSuspendClass_CheckedChanged(object sender, EventArgs e)
    {
        lblFromDate.Text = "Suspend Class Date";
        lblholiday.Text = "Suspend Class List";
        RequiredFieldValidator4.ErrorMessage = "Please Select Suspend Class List";
        mvFromDate.EmptyValueMessage = "Please Enter Suspend Class Date";
        mvFromDate.ErrorMessage = "Please Enter Suspend Class Date";
        
        lblmultiple.InnerText = "For Multiple Days Suspend Class";
        lblDetails.InnerText = "Suspend Class Details (if any)";
        ClearControls();
        ChkDate.Checked = false;
        mandatory.Visible = true;
        lvExamday.DataSource = null;
        lvExamday.DataBind();
        divholidayMapping.Visible = true;
        divholidaymaster.Visible = false;
        objCommon.FillDropDownList(ddlHolidayList, "ACD_ACADEMIC_HOLIDAY_LIST", "HOLIDAY_LISTNO", "HOLIDAY_NAME", "HOLIDAY_FOR='S'", "HOLIDAY_LISTNO");
        BindListView();
        if (ChkDate.Checked)
        {
            mvFromDate.EmptyValueMessage = "Please Enter From Date";
            mvFromDate.ErrorMessage = "Please Enter From Date";
        }
    }
    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCollege.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK = 1 AND COLLEGE_ID =" + ddlCollege.SelectedValue, "SESSIONNO DESC");
    //    }
    //    else
    //    {
    //        ddlSession.Items.Clear();
    //        ddlSession.Items.Add(new ListItem("Please Select", "0"));
    //    }
    //}
    protected void rbnHolidayMaster_CheckedChanged(object sender, EventArgs e)
    {
        divholidayMapping.Visible = false;
        divholidaymaster.Visible = true;
    }
    protected void btnSubmitHoliday_Click(object sender, EventArgs e)
    {
        if (rbnHolidayMaster.Checked)
        {//Check for add or edit
            if (ViewState["HolidatAction"] != null && ViewState["HolidatAction"].ToString().Equals("edit"))
            {
                string holidatname = txtEventName.Text;
                string holidayfor = ddlHolidayfor.SelectedValue;
                int holidatlistno = Convert.ToInt32(ViewState["HolidayListNo"]);
                string path = string.Empty;
                spName = "PKG_ACD_UPD_HOLIDAY_LIST";
                spParameters = "@P_HOLIDAY_NAME,@P_HOLIDAY_FOR,@P_HOLIDAYID";
                spValue = "" + holidatname + "," + holidayfor + "," + holidatlistno + "";
                DataSet ds = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                if (ds.Tables[0].Rows[0]["OUTPUT"].ToString() == "1")
                {
                    objCommon.DisplayMessage(updHoliday, "Record Update Successfully!", this.Page);
                }
                else if (ds.Tables[0].Rows[0]["OUTPUT"].ToString() == "2")
                {
                    objCommon.DisplayMessage(updHoliday, "Record already Exists!", this.Page);
                }
                bindHolidayList();
            }
            else
            {
                string holidatname = txtEventName.Text;
                string holidayfor = ddlHolidayfor.SelectedValue;
                string path = string.Empty;
                spName = "AKG_ACD_ADD_HOLIDAY";
                spParameters = "@P_HOLIDAY_NAME,@P_HOLIDAY_FOR";
                spValue = "" + holidatname + "," + holidayfor + "";
                DataSet ds = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                if (ds.Tables[0].Rows[0]["OUTPUT"].ToString() == "1")
                {
                    objCommon.DisplayMessage(updHoliday, "Record Saved Successfully!", this.Page);
                }
                else if (ds.Tables[0].Rows[0]["OUTPUT"].ToString() == "2")
                {
                    objCommon.DisplayMessage(updHoliday, "Record already Exists!", this.Page);
                }
                bindHolidayList();
            }

        }
    }

    public void bindHolidayList()
    {
        spName = "PKG_ACD_SHOW_HOLIDY_LIST";
        spParameters = "@P_HOLIDAYID";
        spValue = "" + 0 + "";
        DataSet ds = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvHolidayList.DataSource = ds;
            lvHolidayList.DataBind();
        }
        else
        {
            lvHolidayList.DataSource = null;
            lvHolidayList.DataBind();
        }
    }
    protected void btnEditHoliday_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int holidayno = int.Parse(btnEdit.CommandArgument);
        ViewState["HolidayListNo"] = int.Parse(btnEdit.CommandArgument);
        ViewState["HolidatAction"] = "edit";
        spName = "PKG_ACD_SHOW_HOLIDY_LIST";
        spParameters = "@P_HOLIDAYID";
        spValue = "" + holidayno + "";
        DataSet ds = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtEventName.Text = ds.Tables[0].Rows[0]["HOLIDAY_NAME"].ToString();
            ddlHolidayfor.SelectedValue = ds.Tables[0].Rows[0]["HOLIDAY_FOR"].ToString();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEventName.Text = string.Empty;
        ddlHolidayfor.SelectedIndex = 0;
    }
}
