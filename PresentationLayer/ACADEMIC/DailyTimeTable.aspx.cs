//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Daily Time Table                                    
// CREATION DATE : 15-OCT-2011
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 28-DEC-2011
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_DailyTimeTable : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    string Message = string.Empty;
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
    int id = 0;//to receive the UANO i.e. user login id
    int count = 0;
    string value = string.Empty.Trim();
    string value1 = string.Empty.Trim();
    string value2, value3, value4 = string.Empty.Trim();
    int sessionsrno, coursesrno, subjectsrno, teachersrno, slotsrno, no, thpr = 0;
    int flag, flag1, flag2, flag3, flag4, flag5, flag6, flag7 = 0;
    int flag301, flag402 = 0;
    string str = string.Empty.Trim();
    string str1 = string.Empty.Trim();
    string str2 = string.Empty.Trim();
    string str3 = string.Empty.Trim();
    string str4 = string.Empty.Trim();
    string str5 = string.Empty.Trim();
    string str6 = string.Empty.Trim();
    long ret;
    long ret1;
    string slot1, slot2, slot3, slot4, slot5, slot6, slot7 = string.Empty.Trim();
    int[] mon = new int[7];
    int[] tue = new int[7];
    int[] wed = new int[7];
    int[] thurs = new int[7];
    int[] fri = new int[7];
    int[] sat = new int[7];

    int totlength, monlen, tuelen, wedlen, thurslen, frilen, satlen = 0;
    CourseTeacherAllotController objCTA = new CourseTeacherAllotController();
    AllotmentMaster objAM = new AllotmentMaster();
    string deptno = string.Empty;

    #region "Page Event"

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
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                PopulateDropDownList();
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
                Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
        }
    }

    #endregion "Page Event"

    #region "General"

    protected void FillDropdown()
    {
        //GetHorizental or Vertical Timetable

        //GetTeacher();       
        DataSet ds = objCTA.GetSlots(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDegree.SelectedValue));

        lvSlots.DataSource = ds;
        lvSlots.DataBind();
        pnlSlots.Visible = true;

        foreach (ListViewDataItem lvHead in lvSlots.Items)
        {
            DropDownList ddlroommon = lvHead.FindControl("ddlroommon") as DropDownList;
            DropDownList ddlroomtue = lvHead.FindControl("ddlroomtue") as DropDownList;
            DropDownList ddlroomwed = lvHead.FindControl("ddlroomwed") as DropDownList;
            DropDownList ddlroomthur = lvHead.FindControl("ddlroomthur") as DropDownList;
            DropDownList ddlroomfri = lvHead.FindControl("ddlroomfri") as DropDownList;
            DropDownList ddlroomsat = lvHead.FindControl("ddlroomsat") as DropDownList;

            DataSet ds1 = objCTA.Getroomsdepartment(Convert.ToInt32(ddlDegree.SelectedValue));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlroommon.Visible = true;
                ddlroomtue.Visible = true;
                ddlroomwed.Visible = true;
                ddlroomthur.Visible = true;
                ddlroomfri.Visible = true;
                ddlroomsat.Visible = true;
                ddlroommon.DataSource = ds1;
                ddlroommon.DataTextField = "ROOMNAME";
                ddlroommon.DataValueField = "Roomno";
                ddlroommon.DataBind();
                //ddlroommon.Items.Add(new ListItem("Please Select", "0"));
                ddlroommon.SelectedValue = "0";
                //ddlroommon.SelectedValue = "p";
                ddlroomtue.DataSource = ds1;
                ddlroomtue.DataTextField = "ROOMNAME";
                ddlroomtue.DataValueField = "Roomno";
                ddlroomtue.DataBind();
                //ddlroomtue.Items.Add(new ListItem("Please Select", "0"));
                ddlroomtue.SelectedValue = "0";
                //  ddlroomtue.SelectedValue = hdn_fld.Value;
                ddlroomwed.DataSource = ds1;
                ddlroomwed.DataTextField = "ROOMNAME";
                ddlroomwed.DataValueField = "Roomno";
                ddlroomwed.DataBind();
               // ddlroomwed.Items.Add(new ListItem("Please Select", "0"));
                ddlroomwed.SelectedValue = "0";
                ddlroomthur.DataSource = ds1;
                ddlroomthur.DataTextField = "ROOMNAME";
                ddlroomthur.DataValueField = "Roomno";
                ddlroomthur.DataBind();
               // ddlroomthur.Items.Add(new ListItem("Please Select", "0"));
                ddlroomthur.SelectedValue = "0";
                ddlroomfri.DataSource = ds1;
                ddlroomfri.DataTextField = "ROOMNAME";
                ddlroomfri.DataValueField = "Roomno";
                ddlroomfri.DataBind();
               // ddlroomfri.Items.Add(new ListItem("Please Select", "0"));
                ddlroomfri.SelectedValue = "0";
                ddlroomsat.DataSource = ds1;
                ddlroomsat.DataTextField = "ROOMNAME";
                ddlroomsat.DataValueField = "Roomno";
                ddlroomsat.DataBind();
                //ddlroomsat.Items.Add(new ListItem("Please Select", "0"));
                ddlroomsat.SelectedValue = "0";
            }
            else
            {
                ddlroommon.Visible = false;
                ddlroomtue.Visible = false;
                ddlroomwed.Visible = false;
                ddlroomthur.Visible = false;
                ddlroomfri.Visible = false;
                ddlroomsat.Visible = false;
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND FLOCK=1", "SESSIONNO desc");
            //ddlSession.SelectedIndex = 1;
            if (Session["usertype"].ToString() != "1")
            {
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillTeacher()
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[0];
            DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES", objParams).Tables[0];

            //DropDownList
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            ddlTeacher.DataSource = dtTeacher;
            ddlTeacher.DataTextField = dtTeacher.Columns["UA_FULLNAME"].ToString();
            ddlTeacher.DataValueField = dtTeacher.Columns["UA_NO"].ToString();
            ddlTeacher.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void ClearControls()
    {
        //ddlBranch.Items.Clear();
        //ddlBranch.Items.Add("Please Select");
        //ddlScheme.Items.Clear();
        //ddlScheme.Items.Add("Please Select");
        //ddlSem.Items.Clear();
        //ddlSem.Items.Add("Please Select");
    }

    private void BindList()
    {
        try
        {
            CourseController objCC = new CourseController();
            if (ddlScheme.SelectedIndex > 0)
            {
                DataSet ds = objCC.GetCourseAllotment(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add("Select");
                    ddlCourse.SelectedItem.Value = "0";
                    ddlCourse.DataTextField = "COURSE_NAME";
                    ddlCourse.DataValueField = "COURSENO";
                    ddlCourse.DataSource = ds;
                    ddlCourse.DataBind();
                    ddlCourse.SelectedIndex = 0;
                }
                else
                {
                    // ShowMessage("Course Teacher Allotment not done");
                    objCommon.DisplayMessage(this.updTimetable, "Course Teacher Allotment not done.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindTimeTable()
    {
        DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvView.DataSource = ds;
            lvView.DataBind();
            //pnlView.Visible = false;
            pnlView.Visible = true;
            lvView.Visible = false;
           

            if (lvSlots.Items.Count > 0)
            {
                for (int i = 0; i < lvSlots.Items.Count; i++)
                {
                    CheckBox chkMon = lvSlots.Items[i].FindControl("chkMon") as CheckBox;
                    HiddenField hdnMon = lvSlots.Items[i].FindControl("hdnMon") as HiddenField;
                    //chkTues
                    //hdnTue
                    CheckBox chkTues = lvSlots.Items[i].FindControl("chkTues") as CheckBox;
                    HiddenField hdnTue = lvSlots.Items[i].FindControl("hdnTue") as HiddenField;

                    //chkWed
                    //hdnWed
                    CheckBox chkWed = lvSlots.Items[i].FindControl("chkWed") as CheckBox;
                    HiddenField hdnWed = lvSlots.Items[i].FindControl("hdnWed") as HiddenField;
                    //chkThurs
                    //hdnThurs
                    CheckBox chkThurs = lvSlots.Items[i].FindControl("chkThurs") as CheckBox;
                    HiddenField hdnThurs = lvSlots.Items[i].FindControl("hdnThurs") as HiddenField;
                    //chkFri
                    //hdnFri
                    CheckBox chkFri = lvSlots.Items[i].FindControl("chkFri") as CheckBox;
                    HiddenField hdnFri = lvSlots.Items[i].FindControl("hdnFri") as HiddenField;
                    //chkSat
                    //hdnSat
                    CheckBox chkSat = lvSlots.Items[i].FindControl("chkSat") as CheckBox;
                    HiddenField hdnSat = lvSlots.Items[i].FindControl("hdnSat") as HiddenField;

                    string mo = ds.Tables[0].Rows[i]["MONDAY"].ToString().Trim();
                    //if (ds.Tables[0].Rows[i]["NO"].ToString().Trim() == "1")
                    //{
                    //if (mo == "" || mo == "--")
                    //{
                    //    chkMon.Checked = false;
                    //}
                    //else
                    //{
                    //    // if (ds.Tables[0].Rows[i]["MONDAY"].ToString().Trim() != "--" || mo.Trim() != string.Empty.Trim())
                    //    chkMon.Checked = true;
                    //}
                    //string tu = ds.Tables[0].Rows[i]["TUESDAY"].ToString().Trim();
                    //if (tu == "" || tu == "--")
                    //{
                    //    chkTues.Checked = false;
                    //}
                    //else
                    //{
                    //    chkTues.Checked = true;
                    //}
                    //string we = ds.Tables[0].Rows[i]["WEDNESDAY"].ToString().Trim();
                    //if (we == "" || we == "--")
                    ////if (ds.Tables[0].Rows[i]["WEDNESDAY"].ToString().Trim() != "--")
                    //{
                    //    chkWed.Checked = false;

                    //}
                    //else
                    //{
                    //    chkWed.Checked = true;

                    //}
                    //string th = ds.Tables[0].Rows[i]["THURSDAY"].ToString().Trim();
                    //if (th == "" || th == "--")
                    //// if (ds.Tables[0].Rows[i]["THURSDAY"].ToString().Trim() != "--")
                    //{
                    //    chkThurs.Checked = false;

                    //}
                    //else
                    //{
                    //    chkThurs.Checked = true;
                    //}
                    //string fr = ds.Tables[0].Rows[i]["FRIDAY"].ToString().Trim();
                    //if (fr == "" || fr == "--")
                    ////if (ds.Tables[0].Rows[i]["FRIDAY"].ToString().Trim() != "--")
                    //{
                    //    chkFri.Checked = false;

                    //}
                    //else
                    //{
                    //    chkFri.Checked = true;
                    //}
                    //string st = ds.Tables[0].Rows[i]["SATURDAY"].ToString().Trim();
                    //if (st == "" || st == "--")
                    //// if (ds.Tables[0].Rows[i]["SATURDAY"].ToString().Trim() != "--")
                    //{
                    //    chkSat.Checked = false;

                    //}
                    //else
                    //{
                    //    chkSat.Checked = true;
                    //}
                    //}
                }
            }
        }
        else
        {
            lvView.DataSource = null;
            lvView.DataBind();
            pnlView.Visible = true;
        }
    }

    private void save()
    {
        try
        {
            ViewState["idno"] = "null";
            string id = string.Empty.Trim();
            objAM.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objAM.COURSESRNO = Convert.ToInt32(ddlCourse.SelectedValue);
            objAM.SECTIONNO = Convert.ToInt32(ddlSection.SelectedValue);
            objAM.THEORYPRAC = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objAM.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);

            ViewState["slotno"] = "null";
            string slotmon = string.Empty.Trim();
            string slottue = string.Empty.Trim();
            string slotwed = string.Empty.Trim();
            string slotthurs = string.Empty.Trim();
            string slotfri = string.Empty.Trim();
            string slotsat = string.Empty.Trim();


            string roommon = string.Empty.Trim();
            string roomtue = string.Empty.Trim();
            string roomwed = string.Empty.Trim();
            string roomthurs = string.Empty.Trim();
            string roomfri = string.Empty.Trim();
            string roomsat = string.Empty.Trim();


            string batchmon = string.Empty.Trim();
            string batchtue = string.Empty.Trim();
            string batchwed = string.Empty.Trim();
            string batchthurs = string.Empty.Trim();
            string batchfri = string.Empty.Trim();
            string batchsat = string.Empty.Trim();

            str = string.Empty.Trim();
            if (lvSlots.Items.Count > 0)
            {
                int h1 = 1;
                int h2 = 1;
                int h3 = 1;
                int h4 = 1;
                int h5 = 1;
                for (int i = 0; i < lvSlots.Items.Count; i++)
                {
                    CheckBox chkMon = lvSlots.Items[i].FindControl("chkMon") as CheckBox;
                    HiddenField hdnMon = lvSlots.Items[i].FindControl("hdnMon") as HiddenField;
                    //chkTues
                    //hdnTue
                    CheckBox chkTues = lvSlots.Items[i].FindControl("chkTues") as CheckBox;
                    HiddenField hdnTue = lvSlots.Items[i].FindControl("hdnTue") as HiddenField;

                    //chkWed
                    //hdnWed
                    CheckBox chkWed = lvSlots.Items[i].FindControl("chkWed") as CheckBox;
                    HiddenField hdnWed = lvSlots.Items[i].FindControl("hdnWed") as HiddenField;
                    //chkThurs
                    //hdnThurs
                    CheckBox chkThurs = lvSlots.Items[i].FindControl("chkThurs") as CheckBox;
                    HiddenField hdnThurs = lvSlots.Items[i].FindControl("hdnThurs") as HiddenField;
                    //chkFri
                    //hdnFri
                    CheckBox chkFri = lvSlots.Items[i].FindControl("chkFri") as CheckBox;
                    HiddenField hdnFri = lvSlots.Items[i].FindControl("hdnFri") as HiddenField;
                    //chkSat
                    //hdnSat
                    CheckBox chkSat = lvSlots.Items[i].FindControl("chkSat") as CheckBox;
                    HiddenField hdnSat = lvSlots.Items[i].FindControl("hdnSat") as HiddenField;


                    CheckBoxList chkBatchMon = lvSlots.Items[i].FindControl("chkBatchMon") as CheckBoxList;

                    CheckBoxList chkBatchTue = lvSlots.Items[i].FindControl("chkBatchTue") as CheckBoxList;

                    CheckBoxList chkBatchWed = lvSlots.Items[i].FindControl("chkBatchWed") as CheckBoxList;

                    CheckBoxList chkBatchThurs = lvSlots.Items[i].FindControl("chkBatchThurs") as CheckBoxList;

                    CheckBoxList chkBatchFri = lvSlots.Items[i].FindControl("chkBatchFri") as CheckBoxList;

                    CheckBoxList chkBatchSat = lvSlots.Items[i].FindControl("chkBatchSat") as CheckBoxList;


                    DropDownList ddlroommon = lvSlots.Items[i].FindControl("ddlroommon") as DropDownList;
                    DropDownList ddlroomtue = lvSlots.Items[i].FindControl("ddlroomtue") as DropDownList;
                    DropDownList ddlroomwed = lvSlots.Items[i].FindControl("ddlroomwed") as DropDownList;
                    DropDownList ddlroomthur = lvSlots.Items[i].FindControl("ddlroomthur") as DropDownList;
                    DropDownList ddlroomfri = lvSlots.Items[i].FindControl("ddlroomfri") as DropDownList;
                    DropDownList ddlroomsat = lvSlots.Items[i].FindControl("ddlroomsat") as DropDownList;
               


                    #region Monday
                    if (chkMon.Checked == true)
                    {
                        count++;
                        if (hdnMon.Value.ToString().Trim() != null)
                        {
                            if (slotmon != string.Empty)
                            {
                                slotmon = slotmon + "$" + hdnMon.Value.ToString().Trim();
                            }
                            else
                            {
                                slotmon = hdnMon.Value.ToString();
                            }
                            if (roommon != string.Empty)
                            {
                                roommon = roommon + "$" + ddlroommon.SelectedValue.Trim();
                            }
                            else
                            {
                                roommon = ddlroommon.SelectedValue.Trim();
                            }
                        }
                        //if (ddlroommon.SelectedValue.Trim() != "0")
                        //{
                        //    if (roommon != string.Empty)
                        //    {
                        //        roommon = roommon + "$" + ddlroommon.SelectedValue.Trim();
                        //    }
                        //    else
                        //    {
                        //        roommon = ddlroommon.SelectedValue.Trim();
                        //    }
                        //}
                        //else
                        //{

                        //}
                        int l1 = 0;
                        int j1 = 1;
                        foreach (ListItem item in chkBatchMon.Items)
                        {
                            if (item.Selected)
                            {
                                if (batchmon.Length > 0)
                                {
                                    if (j1 != 1)
                                    {
                                        batchmon = batchmon + "," + item.Value; //changed by reena
                                    }
                                    else
                                    {
                                        if (h1 == 1)
                                        {
                                            batchmon = batchmon + "," + item.Value;  //changed by reena
                                        }
                                        else
                                        {
                                            batchmon = batchmon + item.Value;
                                        }

                                    }
                                    h1 = h1 + 1;
                                    j1++;
                                }
                                else
                                {
                                    batchmon = item.Value;

                                }
                                l1 = l1 + 1;
                            }
                            else
                            {
                                l1 = l1 + 0;

                            }

                        }
                        if (l1 == 0)
                        {
                            batchmon = batchmon + "0$";
                            h1 = h1 + 1;
                        }

                        int k1 = 1;
                        foreach (ListItem item in chkBatchMon.Items)
                        {

                            if (item.Selected)
                            {

                                if (k1 == 1)
                                {
                                    batchmon = batchmon + "$";
                                    h1 = h1 + 1;
                                }
                                k1 = k1 + 1;
                            }
                        }
                    }

                    //slotmon = slotmon.Replace("$$", "$");
                    #endregion
                    #region Tuesday
                    if (chkTues.Checked == true)
                    {
                        count++;
                        if (hdnTue.Value.ToString().Trim() != null)
                        {
                            if (slottue != string.Empty)
                            {
                                slottue = slottue + "$" + hdnTue.Value.ToString().Trim();
                            }
                            else
                            {
                                slottue = hdnTue.Value.ToString();
                            }
                            if (roomtue != string.Empty)
                            {
                                roomtue = roomtue + "$" + ddlroomtue.SelectedValue.Trim();
                            }
                            else
                            {
                                roomtue = ddlroomtue.SelectedValue.Trim();
                            }

                        }
                        //if (ddlroomtue.SelectedValue.Trim() != "0")
                        //{
                        //    if (roomtue != string.Empty)
                        //    {
                        //        roomtue = roomtue + "$" + ddlroomtue.SelectedValue.Trim();
                        //    }
                        //    else
                        //    {
                        //        roomtue = ddlroomtue.SelectedValue.Trim();
                        //    }
                        //}
                        //else
                        //{
                        //    //if (roomtue != string.Empty)
                        //    //{
                        //    //    roomtue = roommon + "$" + "0";
                        //    //}
                        //    //else
                        //    //{
                        //    //    roomtue = "0";
                        //    //}
                        //}
                        int l2 = 0;
                        int j2 = 1;
                        foreach (ListItem item in chkBatchTue.Items)
                        {
                            if (item.Selected)
                            {
                                if (batchtue.Length > 0)
                                {
                                    if (j2 != 1)
                                    {
                                        batchtue = batchtue + "," + item.Value;
                                    }
                                    else
                                    {
                                        if (h2 == 1)
                                        {
                                            batchtue = batchtue + "," + item.Value;
                                        }
                                        else
                                        {
                                            batchtue = batchtue + item.Value;
                                        }

                                    }
                                    h2 = h2 + 1;
                                    j2++;
                                }
                                else
                                {
                                    batchtue = item.Value;

                                }
                                l2 = l2 + 1;
                            }
                            else
                            {
                                l2 = l2 + 0;

                            }

                        }
                        if (l2 == 0)
                        {
                            batchtue = batchtue + "0$";
                            h2 = h2 + 1;
                        }

                        int k2 = 1;
                        foreach (ListItem item in chkBatchTue.Items)
                        {

                            if (item.Selected)
                            {

                                if (k2 == 1)
                                {
                                    batchtue = batchtue + "$";
                                    h2 = h2 + 1;
                                }
                                k2 = k2 + 1;
                            }
                        }

                    }

                    #endregion
                    #region Wednesday
                    if (chkWed.Checked == true)
                    {
                        count++;
                        if (hdnWed.Value.ToString().Trim() != null)
                        {
                            if (slotwed != string.Empty)
                            {
                                slotwed = slotwed + "$" + hdnWed.Value.ToString().Trim();
                            }
                            else
                            {
                                slotwed = hdnWed.Value.ToString();
                            }
                            if (roomwed != string.Empty)
                            {
                                roomwed = roomwed + "$" + ddlroomwed.SelectedValue.Trim();
                            }
                            else
                            {
                                roomwed = ddlroomwed.SelectedValue.Trim();
                            }
                        }
                        //if (ddlroomwed.SelectedValue.Trim() != "0")
                        //{
                        //    if (roomwed != string.Empty)
                        //    {
                        //        roomwed = roomwed + "$" + ddlroomwed.SelectedValue.Trim();
                        //    }
                        //    else
                        //    {
                        //        roomwed = ddlroomwed.SelectedValue.Trim();
                        //    }
                        //}
                        //else
                        //{
                        //    if (roomwed != string.Empty)
                        //    {
                        //        roomwed = roommon + "$" + "0";
                        //    }
                        //    else
                        //    {
                        //        roomwed = "0";
                        //    }
                        //}
                        int l3 = 0;
                        int j3 = 1;
                        foreach (ListItem item in chkBatchWed.Items)
                        {
                            if (item.Selected)
                            {
                                if (batchwed.Length > 0)
                                {
                                    if (j3 != 1)
                                    {
                                        batchwed = batchwed + "," + item.Value;
                                    }
                                    else
                                    {
                                        if (h3 == 1)
                                        {
                                            batchwed = batchwed + "," + item.Value;
                                        }
                                        else
                                        {
                                            batchwed = batchwed + item.Value;
                                        }

                                    }
                                    h3 = h3 + 1;
                                    j3++;
                                }
                                else
                                {
                                    batchwed = item.Value;

                                }
                                l3 = l3 + 1;
                            }
                            else
                            {
                                l3 = l3 + 0;

                            }

                        }
                        if (l3 == 0)
                        {
                            batchwed = batchwed + "0$";
                            h3 = h3 + 1;
                        }

                        int k3 = 1;
                        foreach (ListItem item in chkBatchWed.Items)
                        {

                            if (item.Selected)
                            {

                                if (k3 == 1)
                                {
                                    batchwed = batchwed + "$";
                                    h3 = h3 + 1;
                                }
                                k3 = k3 + 1;
                            }
                        }

                    }

                    #endregion
                    #region Thursday
                    if (chkThurs.Checked == true)
                    {
                        count++;
                        if (hdnThurs.Value.ToString().Trim() != null)
                        {
                            if (slotthurs != string.Empty)
                            {
                                slotthurs = slotthurs + "$" + hdnThurs.Value.ToString().Trim();
                            }
                            else
                            {
                                slotthurs = hdnThurs.Value.ToString();
                            }
                            if (roomthurs != string.Empty)
                            {
                                roomthurs = roomthurs + "$" + ddlroomthur.SelectedValue.Trim();
                            }
                            else
                            {
                                roomthurs = ddlroomthur.SelectedValue.Trim();
                            }
                        }
                        //if (ddlroomthur.SelectedValue.Trim() != "0")
                        //{
                        //    if (roomthurs != string.Empty)
                        //    {
                        //        roomthurs = roomthurs + "$" + ddlroomthur.SelectedValue.Trim();
                        //    }
                        //    else
                        //    {
                        //        roomthurs = ddlroomthur.SelectedValue.Trim();
                        //    }
                        //}
                        //else
                        //{
                        //    if (roomthurs != string.Empty)
                        //    {
                        //        roomthurs = roommon + "$" + "0";
                        //    }
                        //    else
                        //    {
                        //        roomthurs = "0";
                        //    }
                        //}

                        int l4 = 0;
                        int j4 = 1;
                        foreach (ListItem item in chkBatchThurs.Items)
                        {
                            if (item.Selected)
                            {
                                if (batchthurs.Length > 0)
                                {
                                    if (j4 != 1)
                                    {
                                        batchthurs = batchthurs + "," + item.Value;
                                    }
                                    else
                                    {
                                        if (h4 == 1)
                                        {
                                            batchthurs = batchthurs + "," + item.Value;
                                        }
                                        else
                                        {
                                            batchthurs = batchthurs + item.Value;
                                        }

                                    }
                                    h4 = h4 + 1;
                                    j4++;
                                }
                                else
                                {
                                    batchthurs = item.Value;

                                }
                                l4 = l4 + 1;
                            }
                            else
                            {
                                l4 = l4 + 0;

                            }

                        }
                        if (l4 == 0)
                        {
                            batchthurs = batchthurs + "0$";
                            h4 = h4 + 1;
                        }

                        int k4 = 1;
                        foreach (ListItem item in chkBatchThurs.Items)
                        {

                            if (item.Selected)
                            {

                                if (k4 == 1)
                                {
                                    batchthurs = batchthurs + "$";
                                    h4 = h4 + 1;
                                }
                                k4 = k4 + 1;
                            }
                        }
                    }


                    #endregion
                    #region Friday
                    if (chkFri.Checked == true)
                    {
                        count++;
                        if (hdnFri.Value.ToString().Trim() != null)
                        {
                            if (slotfri != string.Empty)
                            {
                                slotfri = slotfri + "$" + hdnFri.Value.ToString().Trim();
                            }
                            else
                            {
                                slotfri = hdnFri.Value.ToString();
                            }
                            if (roomfri != string.Empty)
                            {
                                roomfri = roomfri + "$" + ddlroomfri.SelectedValue.Trim();
                            }
                            else
                            {
                                roomfri = ddlroomfri.SelectedValue.Trim();
                            }
                        }
                        //if (ddlroomfri.SelectedValue.Trim() != "0")
                        //{
                        //    if (roomfri != string.Empty)
                        //    {
                        //        roomfri = roomfri + "$" + ddlroomfri.SelectedValue.Trim();
                        //    }
                        //    else
                        //    {
                        //        roomfri = ddlroomfri.SelectedValue.Trim();
                        //    }
                        //}
                        //else
                        //{
                        //    if (roomfri != string.Empty)
                        //    {
                        //        roomfri = roommon + "$" + "0";
                        //    }
                        //    else
                        //    {
                        //        roomfri = "0";
                        //    }
                        //}

                        int l5 = 0;
                        int j5 = 1;
                        foreach (ListItem item in chkBatchFri.Items)
                        {
                            if (item.Selected)
                            {
                                if (batchfri.Length > 0)
                                {
                                    if (j5 != 1)
                                    {
                                        batchfri = batchfri + "," + item.Value;
                                    }
                                    else
                                    {
                                        if (h5 == 1)
                                        {
                                            batchfri = batchfri + "," + item.Value;
                                        }
                                        else
                                        {
                                            batchfri = batchfri + item.Value;
                                        }

                                    }
                                    h5 = h5 + 1;
                                    j5++;
                                }
                                else
                                {
                                    batchfri = item.Value;

                                }
                                l5 = l5 + 1;
                            }
                            else
                            {
                                l5 = l5 + 0;

                            }

                        }
                        if (l5 == 0)
                        {
                            batchfri = batchfri + "0$";
                            h5 = h5 + 1;
                        }

                        int k5 = 1;
                        foreach (ListItem item in chkBatchFri.Items)
                        {

                            if (item.Selected)
                            {

                                if (k5 == 1)
                                {
                                    batchfri = batchfri + "$";
                                    h5 = h5 + 1;
                                }
                                k5 = k5 + 1;
                            }
                        }

                    }


                    #endregion

                    #region Saturday
                    if (chkSat.Checked == true)
                    {
                        count++;
                        if (hdnSat.Value.ToString().Trim() != null)
                        {
                            if (slotsat != string.Empty)
                            {
                                slotsat = slotsat + "$" + hdnSat.Value.ToString().Trim();
                            }
                            else
                            {
                                slotsat = hdnSat.Value.ToString();
                            }
                            if (roomsat != string.Empty)
                            {
                                roomsat = roomsat + "$" + ddlroomsat.SelectedValue.Trim();
                            }
                            else
                            {
                                roomsat = ddlroomsat.SelectedValue.Trim();
                            }
                        }
                        //if (ddlroomsat.SelectedValue.Trim() != "0")
                        //{
                        //    if (roomsat != string.Empty)
                        //    {
                        //        roomsat = roomsat + "$" + ddlroomsat.SelectedValue.Trim();
                        //    }
                        //    else
                        //    {
                        //        roomsat = ddlroomsat.SelectedValue.Trim();
                        //    }
                        //}
                        //else
                        //{
                        //    if (roomsat != string.Empty)
                        //    {
                        //        roomsat = roommon + "$" + "0";
                        //    }
                        //    else
                        //    {
                        //        roomsat = "0";
                        //    }
                        //}
                        int l5 = 0;
                        int j5 = 1;
                        foreach (ListItem item in chkBatchSat.Items)
                        {
                            if (item.Selected)
                            {
                                if (batchsat.Length > 0)
                                {
                                    if (j5 != 1)
                                    {
                                        batchsat = batchsat + "," + item.Value;
                                    }
                                    else
                                    {
                                        if (h5 == 1)
                                        {
                                            batchsat = batchsat + "," + item.Value;
                                        }
                                        else
                                        {
                                            batchsat = batchsat + item.Value;
                                        }

                                    }
                                    h5 = h5 + 1;
                                    j5++;
                                }
                                else
                                {
                                    batchsat = item.Value;

                                }
                                l5 = l5 + 1;
                            }
                            else
                            {
                                l5 = l5 + 0;

                            }

                        }
                        if (l5 == 0)
                        {
                            batchsat = batchsat + "0$";
                            h5 = h5 + 1;
                        }

                        int k5 = 1;
                        foreach (ListItem item in chkBatchSat.Items)
                        {

                            if (item.Selected)
                            {

                                if (k5 == 1)
                                {
                                    batchsat = batchsat + "$";
                                    h5 = h5 + 1;
                                }
                                k5 = k5 + 1;
                            }
                        }

                    }


                    #endregion

                    //if ((ddlroommon.SelectedValue != "0" && chkMon.Checked == false)||(chkMon.Checked == true && ddlroommon.SelectedValue == "0"))
                    //{
                       
                    //       goto displayInfo;
                           
                        
                    //}
                    //if ((ddlroomtue.SelectedValue != "0" && chkTues.Checked == false) || (chkTues.Checked == true && ddlroomtue.SelectedValue == "0"))
                    //{
                        
                    //        goto displayInfo;

                       
                    //}
                    //if ((ddlroomwed.SelectedValue != "0" && chkWed.Checked == false) || (chkWed.Checked == true && ddlroomwed.SelectedValue == "0"))
                    //{
                    //        goto displayInfo;
                    //}
                    //if ((ddlroomthur.SelectedValue != "0" && chkThurs.Checked == false) || (chkThurs.Checked == true && ddlroomthur.SelectedValue == "0"))
                    //{
                       
                    //        goto displayInfo;

                    //}
                    //if ((ddlroomfri.SelectedValue != "0" && chkFri.Checked == false)||(chkFri.Checked == true && ddlroomfri.SelectedValue == "0"))
                    //{
                    //      goto displayInfo;

                       
                    //}

                    //if ((ddlroomsat.SelectedValue != "0" && chkSat.Checked == false) || (chkSat.Checked == true && ddlroomsat.SelectedValue == "0"))
                    //{
                    //    goto displayInfo;


                    //}

             //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if ((ddlroommon.SelectedValue != "0" && chkMon.Checked == false) )
                    {
                        goto displayerrmsg;
                    }
                    if ((ddlroomtue.SelectedValue != "0" && chkTues.Checked == false) )
                    {
                        goto displayerrmsg;
                    }
                    if ((ddlroomwed.SelectedValue != "0" && chkWed.Checked == false) )
                    {
                        goto displayerrmsg;
                    }
                    if ((ddlroomthur.SelectedValue != "0" && chkThurs.Checked == false) )
                    {
                        goto displayerrmsg;
                    }
                    if ((ddlroomfri.SelectedValue != "0" && chkFri.Checked == false) )
                    {
                        goto displayerrmsg;
                    }

                    if ((ddlroomsat.SelectedValue != "0" && chkSat.Checked == false) )
                    {
                        goto displayerrmsg;
                    }


                }

            }

            if (slotmon == "")
            {
                objAM.DAY1 = 0;
                objAM.SLOT1 = "";
                objAM.BATCH1 = "";
                objAM.ROOM1 = "";
            }
            else
            {
                //int count1 = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "count(day1)", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and sectionno=" + (ddlSection.SelectedValue) +" and  day1= 1"));
                //if (count1 == 1)
                //{
                //objCommon.DisplayMessage("Slot is Already Alloted");
                //ShowMessage("Slot is Already Alloted");
                //FillDropdown();
                //return;

                // }
                //else
                //{
                objAM.DAY1 = 1;
                objAM.SLOT1 = slotmon.Trim();
                objAM.BATCH1 = batchmon.Trim();
                objAM.ROOM1 = roommon;


                //}
            }

            if (slottue == "")
            {
                objAM.DAY2 = 0;
                objAM.SLOT2 = "";
                objAM.BATCH2 = "";
                objAM.ROOM2 = "";
            }
            else
            {
                //int count1 = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "count(day2)", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and sectionno=" + (ddlSection.SelectedValue) + " AND SLOT2 = 1 and day2= 1"));
                //if (count1 == 1)
                //{
                //    ShowMessage("Slot is Already Alloted");
                //   // FillDropdown();
                //    return;
                //}
                //else
                //{
                objAM.DAY2 = 1;
                objAM.SLOT2 = slottue.Trim();
                objAM.BATCH2 = batchtue.Trim();
                objAM.ROOM2 = roomtue;
                //}
            }
            if (slotwed == "")
            {
                objAM.DAY3 = 0;
                objAM.SLOT3 = "";
                objAM.BATCH3 = "";
                objAM.ROOM3 = "";
            }
            else
            {
                //int count1 = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "count(day3)", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and sectionno=" + (ddlSection.SelectedValue) + " AND SLOT3 = 1 and day3=1"));
                //if (count1 == 1)
                //{
                //    ShowMessage("Slot is Already Alloted");
                //   // FillDropdown();
                //    return;
                //}
                //else
                //{

                objAM.DAY3 = 1;
                objAM.SLOT3 = slotwed.Trim();
                objAM.BATCH3 = batchwed.Trim();
                objAM.ROOM3 = roomwed;
                // }
            }
            if (slotthurs == "")
            {

                objAM.DAY4 = 0;
                objAM.SLOT4 = "";
                objAM.BATCH4 = "";
                objAM.ROOM4 = "";
            }
            else
            {
                //int count1 = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "count(day4)", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and sectionno=" + (ddlSection.SelectedValue) + " AND SLOT4 = 1 and day4=1"));
                //if (count1 == 1)
                //{
                //    ShowMessage("Slot is Already Alloted");
                //    //FillDropdown();
                //    return;
                //}
                //else
                //{

                objAM.DAY4 = 1;
                objAM.SLOT4 = slotthurs.Trim();
                objAM.BATCH4 = batchthurs.Trim();
                objAM.ROOM4 = roomthurs;
                //}
            }
            if (slotfri == "")
            {
                objAM.DAY5 = 0;
                objAM.SLOT5 = "";
                objAM.BATCH5 = "";
                objAM.ROOM5 = "";
            }
            else
            {
                //int count1 = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "day5)", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and sectionno=" + (ddlSection.SelectedValue) + " AND SLOT5 = 1 and day5=1"));
                //if (count1 == 1)
                //{
                //    ShowMessage("Slot is Already Alloted");
                //   // FillDropdown();
                //    return;

                //}
                //else
                //{
                objAM.DAY5 = 1;
                objAM.SLOT5 = slotfri.Trim();
                objAM.BATCH5 = batchfri.Trim();
                objAM.ROOM5 = roomfri;

                //}
            }
            if (slotsat == "")
            {
                objAM.DAY6 = 0;
                objAM.SLOT6 = "";
                objAM.BATCH6 = "";
                objAM.ROOM6 = "";
            }
            else
            {
                //int count1 = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "count(day6)", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and sectionno=" + (ddlSection.SelectedValue) + " AND SLOT6 = 1 and day6=1"));
                //if (count1 == 1)
                //{
                //    ShowMessage("Slot is Already Alloted");
                //    //FillDropdown();
                //    return;
                //}
                //else
                //{
                objAM.DAY6 = 1;
                objAM.SLOT6 = slotsat.Trim();
                objAM.BATCH6 = batchsat.Trim();
                objAM.ROOM6 = roomsat;
                //}
            }




             int c = 0;
             ret = objCTA.AddUpdateAllotment(objAM, ref Message);
            if (ret <= 0)
            {

                if (Message.ToString().Trim().Trim() == "")
                {
                    // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NoModify, Common.MessageType.Alert);
                    //ShowMessage("No Modified");
                    objCommon.DisplayMessage(this.updTimetable, "No Modified!!", this.Page);
                }
                else
                {
                    // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                    // ShowMessage("ExceptionOccured");
                    objCommon.DisplayMessage(this.updTimetable, "ExceptionOccured", this.Page);
                }
            }
            else
            {
                
                // ShowMessage("Record Updated Successfully");
                objCommon.DisplayMessage(this.updTimetable, "Time Table Updated Successfully", this.Page);
                c++;

            }

            if (c != 0)
            {
                goto lab;
            }

        //displayInfo:
        //    {

        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Cant select Room without selecting Slot');", true);
        //    }
        displayerrmsg:
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Cant select Room without selecting Slot');", true);
            }

    lab:
        { 
    }
            BindTimeTable();
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ExamRegistration.ShowRegisteredCourses-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion "General"

    #region "Selected Index Changed"

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnExcel.Enabled = true;
        btnPrint.Enabled = true;
        try
        {

            if (ddlSubjectType.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + '-' + COURSE_NAME) as COURSENAME", "SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND OFFERED=1", "CCODE");
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO)", "DISTINCT C.COURSENO", "CASE WHEN isnull(c.ELECT,0)=0 then CCODE + ' - ' + COURSE_NAME else (C.CCODE + ' - ' + COURSE_NAME +'['+ E.GROUPNAME +']') END AS COURSE_NAME", " OFFERED=1 AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue, "");
                // ADD BY ROSHAN 10-02-2020
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO) INNER JOIN ACD_STUDENT_RESULT R ON (C.COURSENO=R.COURSENO AND C.SCHEMENO=R.SCHEMENO)", "DISTINCT C.COURSENO", "CASE WHEN isnull(c.ELECT,0)=0 then C.CCODE + ' - ' + COURSE_NAME else (C.CCODE + ' - ' + COURSE_NAME +'['+ E.GROUPNAME +']') END AS COURSE_NAME", " R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SUBID = " + ddlSubjectType.SelectedValue + " AND R.SEMESTERNO = " + ddlSem.SelectedValue + "AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(R.CANCEL,0) = 0", "");
                ddlCourse.Focus();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlSubjectType.SelectedIndex = 0;
            }
            if (ddlHorVerTimetable.SelectedIndex == 1)
            {
                DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), 1);
                pnlSlots.Visible = true;
                pnlView.Visible = true;
                lvView.Visible = true;
                trVer.Visible = false;
                trHor.Visible = true;
                btnPrint.Enabled = true;
                btnExcel.Enabled = true;
                btnHorPrint.Enabled = false;
                btnHorExcel.Enabled = false;
                lvView.DataSource = ds;
                lvView.DataBind();
            }
            else
            {
            DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue), 2);
                pnlSlots.Visible = true;
                pnlView.Visible = true;
                lvView.Visible = true;
                trHor.Visible = false;
                trVer.Visible = true;
                btnPrint.Enabled = false;
                btnExcel.Enabled = false;
                btnHorPrint.Enabled = true;
                btnHorExcel.Enabled = true;
                lvVertical.DataSource = ds;
                lvVertical.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ExamRegistration.ShowRegisteredCourses-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            pnlSlots.Visible = false;
            pnlView.Visible = false;
            if (ddlDegree.SelectedIndex > 0)
            {
                int slotcount = Convert.ToInt32(objCommon.LookUp("ACD_TIME_SLOT", "ISNULL(COUNT(*),0)CNT", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue)));
                if (slotcount > 0)
                {
                    if (Session["usertype"].ToString() != "1")
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                    else
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
                    ddlBranch.Focus();
                }
                else
                {
                    objCommon.DisplayMessage(this.updTimetable, "Time Slot(s) are not created for: " + ddlDegree.SelectedItem.Text + " Degree, please Create the Time Slot!!", this.Page);
                    ddlDegree.SelectedIndex = 0;
                    ddlDegree.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            pnlSlots.Visible = false;
            pnlView.Visible = false;

            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            pnlSlots.Visible = false;
            pnlView.Visible = false;
            lblStatus.Text = string.Empty;
            //lvCourse.DataSource = null;
            //lvCourse.DataBind();
            //lvCourse.Visible = false;
            //btnPrint.Enabled = false;
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            ddlSem.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            pnlSlots.Visible = false;
            pnlView.Visible = false;
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue + " AND C.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");
            }
            ddlSection.Focus();           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            pnlSlots.Visible = false;
            pnlView.Visible = false;
            if (ddlSection.SelectedIndex > 0)
            {
                BindTimeTable();
                FillDropdown();//added by reena
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSection_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvSlots_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Page.IsPostBack)
        {
            DataSet dsBatch = objCTA.GetBatchs(Convert.ToInt32(ddlSection.SelectedValue));//changed by reena 
            CheckBoxList chkBatchMon = e.Item.FindControl("chkBatchMon") as CheckBoxList;
            chkBatchMon.DataSource = dsBatch;
            chkBatchMon.DataTextField = "BATCHNAME";
            chkBatchMon.DataValueField = "BATCHNO";
            chkBatchMon.DataBind();
            CheckBoxList chkBatchTue = e.Item.FindControl("chkBatchTue") as CheckBoxList;
            chkBatchTue.DataSource = dsBatch;
            chkBatchTue.DataTextField = "BATCHNAME";
            chkBatchTue.DataValueField = "BATCHNO";
            chkBatchTue.DataBind();
            CheckBoxList chkBatchWed = e.Item.FindControl("chkBatchWed") as CheckBoxList;
            chkBatchWed.DataSource = dsBatch;
            chkBatchWed.DataTextField = "BATCHNAME";
            chkBatchWed.DataValueField = "BATCHNO";
            chkBatchWed.DataBind();
            CheckBoxList chkBatchThurs = e.Item.FindControl("chkBatchThurs") as CheckBoxList;
            chkBatchThurs.DataSource = dsBatch;
            chkBatchThurs.DataTextField = "BATCHNAME";
            chkBatchThurs.DataValueField = "BATCHNO";
            chkBatchThurs.DataBind();
            CheckBoxList chkBatchFri = e.Item.FindControl("chkBatchFri") as CheckBoxList;
            chkBatchFri.DataSource = dsBatch;
            chkBatchFri.DataTextField = "BATCHNAME";
            chkBatchFri.DataValueField = "BATCHNO";
            chkBatchFri.DataBind();
            CheckBoxList chkBatchSat = e.Item.FindControl("chkBatchSat") as CheckBoxList;
            chkBatchSat.DataSource = dsBatch;
            chkBatchSat.DataTextField = "BATCHNAME";
            chkBatchSat.DataValueField = "BATCHNO";
            chkBatchSat.DataBind();
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlTeacher.SelectedIndex = 0;
        //BindTimeTable(ddlCourse.SelectedValue.Trim(), ddlSession.SelectedValue.Trim());
        if (ddlCourse.SelectedIndex > 0)
        {
            //DataSet ds = objCTA.GetTeacher(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue.Trim()), Convert.ToInt32(ddlSection.SelectedValue));

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string adteacher = ds.Tables[0].Rows[0]["ADTEACHER"].ToString().Trim();
            //    string[] stradteacher = adteacher.Trim().Split(',');
            //    int len = stradteacher.Length;
            //    int indexno = 0;
            //    ddlTeacher.Items.Clear();

            //    for (int i = 0; i < len; i++)
            //    {
            //        string no = stradteacher[i].Trim();
            //        DataSet ds1 = objCTA.GetTeacherName(no);
            //        string name = ds1.Tables[0].Rows[0]["UA_FULLNAME"].ToString().Trim();
            //        string uano = ds1.Tables[0].Rows[0]["UA_NO"].ToString().Trim();
            //        ddlTeacher.Items.Add(name);
            //        ddlTeacher.DataTextField = name;
            //        ddlTeacher.DataValueField = uano;
            //        //practical
            //        chkTeacher.Items.Add(name);
            //        chkTeacher.DataTextField = name;
            //        chkTeacher.DataValueField = uano;
            //    }
            //    ddlTeacher.SelectedIndex = 0;
            //}
            //else
            //{
            //    ddlTeacher.Items.Clear();
            //    chkTeacher.Items.Clear();
            //    ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            //}
            //BindTimeTable();
            //  objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.TH_PR =" + ddlSubjectType.SelectedValue, "UA.UA_FULLNAME");//commented by reena on 26_7_16
            //objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", " (" + "UA.UA_FULLNAME + ' (' + (CASE CT.TH_PR WHEN 1 THEN 'TH' WHEN 2 THEN 'PR' ELSE 'TU' END) + ') '" + ")" + "AS UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.subid =" + ddlSubjectType.SelectedValue, "UA.UA_FULLNAME");
            //objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO",  "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.subid =" + ddlSubjectType.SelectedValue, "UA.UA_FULLNAME");
            objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO OR UA.UA_NO IN (SELECT * FROM  DBO.UF_CSVTOTABLE(CT.ADTEACHER)))", "DISTINCT UA.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.SUBID =" + ddlSubjectType.SelectedValue, "UA.UA_FULLNAME");

            //objCommon.FillDropDownList(ddlTeacher, " ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.subid =" + ddlSubjectType.SelectedValue + "AND CT.TH_PR =" + ddltheoryPractical.SelectedValue, "UA.UA_FULLNAME");
            //ddlTeacher.Focus();
        }
        FillDropdown();//**************
        ddltheoryPractical.SelectedIndex = 0;
        //ddlTeacher.SelectedIndex = 0;
        //for (int i = 0; i < lvSlots.Items.Count; i++)
        //{
        //    foreach (ListViewDataItem lvitem in lvSlots.Items)
        //    {
        //        CheckBox chkMon = lvitem.FindControl("chkMon") as CheckBox;
        //        CheckBox chkTues = lvitem.FindControl("chkTues") as CheckBox;
        //        CheckBox chkWed = lvitem.FindControl("chkWed") as CheckBox;
        //        CheckBox chkSat = lvitem.FindControl("chkSat") as CheckBox;
        //        CheckBox chkThurs = lvitem.FindControl("chkThurs") as CheckBox;
        //        CheckBox chkFri = lvitem.FindControl("chkFri") as CheckBox;
        //        chkMon.Checked = false;
        //        chkTues.Checked = false;
        //        chkWed.Checked = false;
        //        chkThurs.Checked = false;
        //        chkFri.Checked = false;
        //        chkSat.Checked = false;
        //    }
        //}
    }

    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[0];
            DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES", objParams).Tables[0];

            DataView dv = new DataView(dtTeacher, "UA_NO <> " + ddlTeacher.SelectedValue, string.Empty, DataViewRowState.CurrentRows);

            //Listview
            //lvAdTeacher.DataSource = dv;
            //lvAdTeacher.DataBind();

            //foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            //{
            //    (dataitem.FindControl("chkIDNo") as CheckBox).Checked = false;
            //}
            FillDropdown();  //commented by reena
            EnableChkForFaculty(Convert.ToInt32(ddlTeacher.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlTeacher_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddltheoryPractical_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltheoryPractical.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", " (" + "UA.UA_FULLNAME + ' (' + (CASE CT.TH_PR WHEN 1 THEN 'TH' WHEN 2 THEN 'PR' ELSE 'TU' END) + ') '" + ")" + "AS UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.subid =" + ddlSubjectType.SelectedValue + "AND CT.TH_PR =" + ddltheoryPractical.SelectedValue, "UA.UA_FULLNAME");
             //objCommon.FillDropDownList(ddlTeacher, " ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.subid =" + ddlSubjectType.SelectedValue + "AND CT.TH_PR =" + ddltheoryPractical.SelectedValue, "UA.UA_FULLNAME");
            objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO OR UA.UA_NO IN (SELECT * FROM  DBO.UF_CSVTOTABLE(CT.ADTEACHER)))", "UA.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.SUBID =" + ddlSubjectType.SelectedValue, "UA.UA_FULLNAME");

            //objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT Cross apply dbo.Split(cast(ct.UA_NO as varchar(max))+','+cast(CT.ADTEACHER as varchar(max)),',') u inner JOIN USER_ACC UA1 on UA1.UA_NO  = u.Value ", "distinct UA1.UA_NO", "UA1.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.subid =" + ddlSubjectType.SelectedValue + "AND CT.TH_PR =" + ddltheoryPractical.SelectedValue, "UA1.UA_FULLNAME");
            ddlTeacher.Focus();
        }
    }

    private void EnableChkForFaculty(int UANO)
    {

        string batchmon = string.Empty.Trim();
        string batchtue = string.Empty.Trim();
        string batchwed = string.Empty.Trim();
        string batchthurs = string.Empty.Trim();
        string batchfri = string.Empty.Trim();
        string batchsat = string.Empty.Trim();
        ////***********DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER ", "SLOT1,SLOT2,SLOT3,SLOT4,SLOT5,SLOT6,BATCH1,BATCH2,BATCH3,BATCH4,BATCH5", "SCHEMENO", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND  SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue) + "AND  COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND UA_NO =" + UANO + " AND TH_PR =" + Convert.ToInt32(ddltheoryPractical.SelectedValue), "");
        //DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER ", "SLOT1,SLOT2,SLOT3,SLOT4,SLOT5,SLOT6,BATCH1,BATCH2,BATCH3,BATCH4,BATCH5,BATCH6,ROOM1,ROOM2,ROOM3,ROOM4,ROOM5,ROOM6", "SCHEMENO", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND  SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue) + "AND  COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND UA_NO =" + UANO + " AND TH_PR =" + Convert.ToInt32(ddltheoryPractical.SelectedValue) + " AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue), "");
        DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER ", "SLOT1,SLOT2,SLOT3,SLOT4,SLOT5,SLOT6,BATCH1,BATCH2,BATCH3,BATCH4,BATCH5,BATCH6,ROOM1,ROOM2,ROOM3,ROOM4,ROOM5,ROOM6", "SCHEMENO", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND  SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue) + " AND  COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND (UA_NO =" + UANO + " OR ADTEACHER=" + UANO + ") AND TH_PR =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue), "");

        string mon = (ds.Tables[0].Rows[0]["SLOT1"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["SLOT1"].ToString()));
        string tue = (ds.Tables[0].Rows[0]["SLOT2"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["SLOT2"].ToString()));
        string wed = (ds.Tables[0].Rows[0]["SLOT3"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["SLOT3"].ToString()));
        string thu = (ds.Tables[0].Rows[0]["SLOT4"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["SLOT4"].ToString()));
        string fri = (ds.Tables[0].Rows[0]["SLOT5"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["SLOT5"].ToString()));
        string sat = (ds.Tables[0].Rows[0]["SLOT6"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["SLOT6"].ToString()));

        string roommon = (ds.Tables[0].Rows[0]["ROOM1"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["ROOM1"].ToString()));
        string roomtue = (ds.Tables[0].Rows[0]["ROOM2"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["ROOM2"].ToString()));
        string roomwed = (ds.Tables[0].Rows[0]["ROOM3"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["ROOM3"].ToString()));
        string roomthur = (ds.Tables[0].Rows[0]["ROOM4"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["ROOM4"].ToString()));
        string roomfri = (ds.Tables[0].Rows[0]["ROOM5"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["ROOM5"].ToString()));
        string roomsat = (ds.Tables[0].Rows[0]["ROOM6"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["ROOM6"].ToString()));

        batchmon = (ds.Tables[0].Rows[0]["BATCH1"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["BATCH1"].ToString()));
        batchtue = (ds.Tables[0].Rows[0]["BATCH2"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["BATCH2"].ToString()));
        batchwed = (ds.Tables[0].Rows[0]["BATCH3"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["BATCH3"].ToString()));
        batchthurs = (ds.Tables[0].Rows[0]["BATCH4"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["BATCH4"].ToString()));
        batchfri = (ds.Tables[0].Rows[0]["BATCH5"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["BATCH5"].ToString()));
        batchsat = (ds.Tables[0].Rows[0]["BATCH6"].ToString() == "" ? "0" : (ds.Tables[0].Rows[0]["BATCH6"].ToString()));



        int u = 1;
        if (lvSlots.Items.Count > 0)
        {
            for (int i = 0; i < lvSlots.Items.Count; i++)
            {
                CheckBoxList chkBatchMon = lvSlots.Items[i].FindControl("chkBatchMon") as CheckBoxList;

                CheckBoxList chkBatchTue = lvSlots.Items[i].FindControl("chkBatchTue") as CheckBoxList;

                CheckBoxList chkBatchWed = lvSlots.Items[i].FindControl("chkBatchWed") as CheckBoxList;

                CheckBoxList chkBatchThurs = lvSlots.Items[i].FindControl("chkBatchThurs") as CheckBoxList;

                CheckBoxList chkBatchFri = lvSlots.Items[i].FindControl("chkBatchFri") as CheckBoxList;

                CheckBoxList chkBatchSat = lvSlots.Items[i].FindControl("chkBatchSat") as CheckBoxList;

                CheckBox chkMon = lvSlots.Items[i].FindControl("chkMon") as CheckBox;
                HiddenField hdnMon = lvSlots.Items[i].FindControl("hdnMon") as HiddenField;
                //chkTues
                //hdnTue
                CheckBox chkTues = lvSlots.Items[i].FindControl("chkTues") as CheckBox;
                HiddenField hdnTue = lvSlots.Items[i].FindControl("hdnTue") as HiddenField;
                //chkWed
                //hdnWed
                CheckBox chkWed = lvSlots.Items[i].FindControl("chkWed") as CheckBox;
                HiddenField hdnWed = lvSlots.Items[i].FindControl("hdnWed") as HiddenField;
                //chkThurs
                //hdnThurs
                CheckBox chkThurs = lvSlots.Items[i].FindControl("chkThurs") as CheckBox;
                HiddenField hdnThurs = lvSlots.Items[i].FindControl("hdnThurs") as HiddenField;
                //chkFri
                //hdnFri
                CheckBox chkFri = lvSlots.Items[i].FindControl("chkFri") as CheckBox;
                HiddenField hdnFri = lvSlots.Items[i].FindControl("hdnFri") as HiddenField;
                //chkSat
                //hdnSat
                CheckBox chkSat = lvSlots.Items[i].FindControl("chkSat") as CheckBox;
                HiddenField hdnSat = lvSlots.Items[i].FindControl("hdnSat") as HiddenField;



                DropDownList ddlroommon = lvSlots.Items[i].FindControl("ddlroommon") as DropDownList;
                DropDownList ddlroomtue = lvSlots.Items[i].FindControl("ddlroomtue") as DropDownList;
                DropDownList ddlroomwed = lvSlots.Items[i].FindControl("ddlroomwed") as DropDownList;
                DropDownList ddlroomthur = lvSlots.Items[i].FindControl("ddlroomthur") as DropDownList;
                DropDownList ddlroomfri = lvSlots.Items[i].FindControl("ddlroomfri") as DropDownList;
                DropDownList ddlroomsat = lvSlots.Items[i].FindControl("ddlroomsat") as DropDownList;

                    HiddenField hdnroommon = lvSlots.Items[i].FindControl("hdnroommon") as HiddenField;
                    HiddenField hdnroomtue = lvSlots.Items[i].FindControl("hdnroomtue") as HiddenField;
                    HiddenField hdnroomwed = lvSlots.Items[i].FindControl("hdnroomwed") as HiddenField;
                    HiddenField hdnroomthur = lvSlots.Items[i].FindControl("hdnroomthur") as HiddenField;
                    HiddenField hdnroomfri = lvSlots.Items[i].FindControl("hdnroomfri") as HiddenField;
                    HiddenField hdnroomsat = lvSlots.Items[i].FindControl("hdnroomsat") as HiddenField;
                {

                    if (mon.Contains(hdnMon.Value))
                    {
                        char ch = ',';
                        int j = 0;
                        int K = 0;
                        string[] array = mon.Split(ch);
                        for (j = 0; j < array.Length; j++)
                        {

                            //string[] batcharray = batchmon.Split(chb);

                            //chkMon.Enabled = true;
                            if (hdnMon.Value == array[j])
                            {
                                //chkMon.Enabled = false;
                                chkMon.Checked = true;
                               
                                   // ddlroommon.Enabled = true;
                                
                                   
                               

                                if (batchmon.Length > 0)   //batchmon != null &&
                                {
                                    string[] batcharray = batchmon.Split(new char[] { '$' });
                                    for (K = 0; K < batcharray[j].Length; K++)
                                    {
                                        foreach (ListItem item in chkBatchMon.Items)
                                        {
                                            string[] batcharray1 = batcharray[j].Split(new char[] { ',' });
                                            foreach (string str in batcharray1)
                                            {
                                                if (str.Trim() == item.Value)
                                                {
                                                    item.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if faculty is not assigned to this slot, revert back the color to white.
                        //chkMon.Enabled = true;
                        chkMon.Checked = false;
                       // ddlroommon.Enabled = false;
                        foreach (ListItem item in chkBatchMon.Items)
                        {
                            item.Selected = false;
                        }
                        //chkMon.BackColor = System.Drawing.Color.White;

                        //ddlBatchMon.BackColor = System.Drawing.Color.White;

                    }

                    if (tue.Contains(hdnTue.Value))
                    {
                        char ch = ',';
                        int j = 0;
                        int K = 0;
                        char[] chl = { '$' };
                        char[] chb = { '$' };
                        char[] chr = { '$' };
                        string[] array = tue.Split(ch);
                        for (j = 0; j < array.Length; j++)
                        {
                            //chkMon.Enabled = true;
                            if (hdnTue.Value == array[j])
                            {
                                //chkTues.Enabled = false;
                                chkTues.Checked = true;
                              //  ddlroomtue.Enabled = true;
                                if (batchtue.Length > 0)  //batchtue != null && 
                                {
                                    string[] batcharray = batchtue.Split(new char[] { '$' });
                                    for (K = 0; K < batcharray[j].Length; K++)
                                    {
                                        foreach (ListItem item in chkBatchTue.Items)
                                        {
                                            string[] batcharray1 = batcharray[j].Split(new char[] { ',' });
                                            foreach (string str in batcharray1)
                                            {
                                                if (str.Trim() == item.Value)
                                                {

                                                    item.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if faculty is not assigned to this slot, revert back the color to white.
                        //chkTues.Enabled = true;
                        chkTues.Checked = false;
                       // ddlroomtue.Enabled = false;
                        foreach (ListItem item in chkBatchTue.Items)
                        {
                            item.Selected = false;
                        }
                    }

                    if (wed.Contains(hdnWed.Value))
                    {
                        char ch = ',';
                        int j = 0;
                        int K = 0;
                        char[] chl = { '$' };
                        char[] chb = { '$' };
                        char[] chr = { '$' };
                        string[] array = wed.Split(ch);
                        for (j = 0; j < array.Length; j++)
                        {
                            //chkMon.Enabled = true;
                            if (hdnWed.Value == array[j])
                            {
                                //chkWed.Enabled = false;
                                chkWed.Checked = true;
                            //    ddlroomwed.Enabled = true;
                                if (batchwed.Length > 0)  //batchwed != null && 
                                {
                                    string[] batcharray = batchwed.Split(new char[] { '$' });
                                    for (K = 0; K < batcharray[j].Length; K++)
                                    {
                                        foreach (ListItem item in chkBatchWed.Items)
                                        {
                                            string[] batcharray1 = batcharray[j].Split(new char[] { ',' });
                                            foreach (string str in batcharray1)
                                            {
                                                if (str.Trim() == item.Value)
                                                {

                                                    item.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if faculty is not assigned to this slot, revert back the color to white.
                        //chkWed.Enabled = true;
                        chkWed.Checked = false;
                       // ddlroomwed.Enabled = false;
                        foreach (ListItem item in chkBatchWed.Items)
                        {
                            item.Selected = false;
                        }
                    }

                    if (thu.Contains(hdnThurs.Value))
                    {
                        char ch = ',';
                        int j = 0;
                        int K = 0;
                        char[] chl = { '$' };
                        char[] chb = { '$' };
                        char[] chr = { '$' };
                        string[] array = thu.Split(ch);

                        for (j = 0; j < array.Length; j++)
                        {
                            //chkMon.Enabled = true;
                            if (hdnThurs.Value == array[j])
                            {
                                //chkThurs.Enabled = false;
                                chkThurs.Checked = true;
                               // ddlroomthur.Enabled = true;
                                if (batchthurs.Length > 0)  //batchthurs != null && 
                                {
                                    string[] batcharray = batchthurs.Split(new char[] { '$' });
                                    for (K = 0; K < batcharray[j].Length; K++)
                                    {
                                        foreach (ListItem item in chkBatchThurs.Items)
                                        {
                                            string[] batcharray1 = batcharray[j].Split(new char[] { ',' });
                                            foreach (string str in batcharray1)
                                            {
                                                if (str.Trim() == item.Value)
                                                {

                                                    item.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if faculty is not assigned to this slot, revert back the color to white.
                        //chkThurs.Enabled = true;
                        chkThurs.Checked = false;
                       // ddlroomthur.Enabled = false;
                        foreach (ListItem item in chkBatchThurs.Items)
                        {
                            item.Selected = false;
                        }

                    }

                    if (fri.Contains(hdnFri.Value))
                    {
                        char ch = ',';
                        int j = 0;
                        int K = 0;
                        char[] chl = { '$' };
                        char[] chb = { '$' };
                        char[] chr = { '$' };
                        string[] array = fri.Split(ch);
                        for (j = 0; j < array.Length; j++)
                        {
                            //chkMon.Enabled = true;
                            if (hdnFri.Value == array[j])
                            {
                                //chkFri.Enabled = false;
                                chkFri.Checked = true;
                             //   ddlroomfri.Enabled = true;
                                if (batchfri.Length > 0)  //batchfri != null && 
                                {
                                    string[] batcharray = batchfri.Split(new char[] { '$' });
                                    for (K = 0; K < batcharray[j].Length; K++)
                                    {
                                        foreach (ListItem item in chkBatchFri.Items)
                                        {
                                            string[] batcharray1 = batcharray[j].Split(new char[] { ',' });
                                            foreach (string str in batcharray1)
                                            {
                                                if (str.Trim() == item.Value)
                                                {

                                                    item.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if faculty is not assigned to this slot, revert back the color to white.
                        //chkFri.Enabled = true;
                        chkFri.Checked = false;
                      //  ddlroomfri.Enabled = false;
                        foreach (ListItem item in chkBatchFri.Items)
                        {
                            item.Selected = false;
                        }

                    }
                    if (sat.Contains(hdnSat.Value))
                    {
                        char ch = ',';
                        int j = 0;
                        int K = 0;
                        char[] chl = { '$' };
                        char[] chb = { '$' };
                        char[] chr = { '$' };
                        string[] array = sat.Split(ch);
                        for (j = 0; j < array.Length; j++)
                        {
                            //chkMon.Enabled = true;
                            if (hdnSat.Value == array[j])
                            {
                                //chkFri.Enabled = false;
                                chkSat.Checked = true;
                                //   ddlroomfri.Enabled = true;
                                if (batchsat.Length > 0)  //batchfri != null && 
                                {
                                    string[] batcharray = batchsat.Split(new char[] { '$' });
                                    for (K = 0; K < batcharray[j].Length; K++)
                                    {
                                        foreach (ListItem item in chkBatchSat.Items)
                                        {
                                            string[] batcharray1 = batcharray[j].Split(new char[] { ',' });
                                            foreach (string str in batcharray1)
                                            {
                                                if (str.Trim() == item.Value)
                                                {

                                                    item.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //if faculty is not assigned to this slot, revert back the color to white.
                        //chkFri.Enabled = true;
                        chkSat.Checked = false;
                        //  ddlroomfri.Enabled = false;
                        foreach (ListItem item in chkBatchSat.Items)
                        {
                            item.Selected = false;
                        }

                    }
                    //if (sat.Contains(hdnSat.Value))
                    //{
                    //    char ch = ',';
                    //    int j = 0;
                    //    string[] array = sat.Split(ch);
                    //    for (j = 0; j < array.Length; j++)
                    //    {
                    //        //chkMon.Enabled = true;
                    //        if (hdnSat.Value == array[j])
                    //        {
                    //            //chkSat.Enabled = false;
                    //            chkSat.Checked = true;
                    //            chkSat.BackColor = System.Drawing.Color.Lime;
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    //if faculty is not assigned to this slot, revert back the color to white.
                    //    //chkSat.Enabled = true;
                    //    chkSat.Checked = false;
                    //    chkSat.BackColor = System.Drawing.Color.White;
                    //}
                    //roommon 
                    //roomtue 
                    //roomwed 
                    //roomthur
                    //roomfri 
                    //hdnroommon 
                    //hdnroomtue 
                    //hdnroomwed 
                    //hdnroomthur
                    //hdnroomfri 
                    char ph = ',';
                    int r = 0;
                    int s = 0;
                    int x=0;
                    char qh = ',';
                    string[] array1;
                    string[] array2;
                    if (chkMon.Checked)
                    {
                        array2 = mon.Split(qh);
                        array1 = roommon.Split(ph);
                     
                            for (x = 0; x < array2.Length; x++)
                            {
                                for (r = 0; r < array1.Length; r++)
                                {
                                if (Convert.ToInt32(array2[x]) == u)
                                {
                                    ddlroommon.SelectedValue = array1[x];
                                    goto lab;
                                }

                               
                            }
                                
                        }
                        lab:
                            { }
                    }
                    if (chkTues.Checked)
                    {
                        array2 = tue.Split(qh);
                        array1 = roomtue.Split(ph);

                        for (x = 0; x < array2.Length; x++)
                        {
                            for (r = 0; r < array1.Length; r++)
                            {
                                if (Convert.ToInt32(array2[x]) == u)
                                {
                                    ddlroomtue.SelectedValue = array1[x];
                                    goto lab;
                                }


                            }

                        }
                    lab:
                        { }

                    }
                    if (chkWed.Checked)
                    {
                        array2 = wed.Split(qh);
                        array1 = roomwed.Split(ph);

                        for (x = 0; x < array2.Length; x++)
                        {
                            for (r = 0; r < array1.Length; r++)
                            {
                                if (Convert.ToInt32(array2[x]) == u)
                                {
                                    ddlroomwed.SelectedValue = array1[x];
                                    goto lab;
                                }


                            }

                        }
                    lab:
                        { }

                    }
                    
                    if (chkThurs.Checked)
                    {
                        array2 = thu.Split(qh);
                        array1 = roomthur.Split(ph);

                        for (x = 0; x < array2.Length; x++)
                        {
                            for (r = 0; r < array1.Length; r++)
                            {
                                if (Convert.ToInt32(array2[x]) == u)
                                {
                                    ddlroomthur.SelectedValue = array1[x];
                                    goto lab;
                                }


                            }

                        }
                    lab:
                        { }

                    }
                    if (chkFri.Checked)
                    {

                        array2 = fri.Split(qh);
                        array1 = roomfri.Split(ph);

                        for (x = 0; x < array2.Length; x++)
                        {
                            for (r = 0; r < array1.Length; r++)
                            {
                                if (Convert.ToInt32(array2[x]) == u)
                                {
                                    ddlroomfri.SelectedValue = array1[x];
                                    goto lab;
                                }


                            }

                        }
                    lab:
                        { }
                    }
                    if (chkSat.Checked)
                    {

                        array2 = sat.Split(qh);
                        array1 = roomsat.Split(ph);

                        for (x = 0; x < array2.Length; x++)
                        {
                            for (r = 0; r < array1.Length; r++)
                            {
                                if (Convert.ToInt32(array2[x]) == u)
                                {
                                    ddlroomsat.SelectedValue = array1[x];
                                    goto lab;
                                }


                            }

                        }
                    lab:
                        { }
                    }
                }
                u++;
            }
        }
    }

    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try 
    //    {

    //        if (ddlSession.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
    //        }
    //        else 
    //        {
    //            Response.Redirect(Request.Url.ToString());
    //        }
    //    }
    //    catch(Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_ExamRegistration.ShowRegisteredCourses-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    #endregion "Selected Index Changed"

    #region "Submit"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        sessionsrno = Convert.ToInt32(ddlSession.SelectedValue.Trim());
        coursesrno = Convert.ToInt32(ddlCourse.SelectedValue.Trim());
        //DataSet dsno = objCTA.GetTeacherNo(ddlTeacher.SelectedItem.Text.Trim());
        //if (dsno.Tables[0].Rows.Count > 1)
        //{
        //    teachersrno = Convert.ToInt32(dsno.Tables[0].Rows[1]["UA_NO"]);
        //    objAM.UA_NO = teachersrno;
        //}
        //if (dsno.Tables[0].Rows.Count <= 1)
        //{
        //    teachersrno = Convert.ToInt32(dsno.Tables[0].Rows[0]["UA_NO"]);
        //    objAM.UA_NO = teachersrno;
        //}
        //teachersrno = Convert.ToInt32(ddlTeacher.SelectedValue.Trim());

        objAM.UA_NO = Convert.ToInt32(ddlTeacher.SelectedValue);

        thpr = ddlSubjectType.SelectedIndex;
        if (flag301 == 1)
        {
            objAM.SRNO = 1;
        }
        else
        {
            objAM.SRNO = 0;
        }

        save();


        if (ddlHorVerTimetable.SelectedIndex == 1)
        {
            DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue), 1);
              
            pnlSlots.Visible = true;
            pnlView.Visible = true;
            lvView.Visible = true;
            trVer.Visible = false;
            trHor.Visible = true;
            btnPrint.Enabled = true;
            btnExcel.Enabled = true;
            btnHorPrint.Enabled = false;
            btnHorExcel.Enabled = false;

            lvView.DataSource = ds;
            lvView.DataBind();

        }
        else
        {
            DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), 2);
            pnlSlots.Visible = true;
            pnlView.Visible = true;
            lvView.Visible = true;
            trHor.Visible = false;
            trVer.Visible = true;
            btnPrint.Enabled = false;
            btnExcel.Enabled = false;
            btnHorPrint.Enabled = true;
            btnHorExcel.Enabled = true;
            lvVertical.DataSource = ds;
            lvVertical.DataBind();
        }
        ////FillDropdown();//************************
   
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Time", "Cry_Atd_TimeTable.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_CourseTeacherAllotment.btnPrint_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int version = 1;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
         
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["username"].ToString() + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_VERSION=" + version+",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim();
                //+ ",@P_COURSESRNO=" + ddlCourse.SelectedValue.ToString().Trim() + ",@P_SESSIONNO=" + ddlSession.SelectedValue.ToString().Trim()
                //+ ",@P_CNAME=" + ddlCourse.SelectedItem.Text.Trim()
               // + ",@P_DEGREE=" + ddlDegree.SelectedItem.Text.Trim()
              
               // + ",@P_BRANCH=" + ddlBranch.SelectedItem.Text.Trim().Replace("&", "and")
                    //+ ",@P_SCHEME=" + (ddlScheme.SelectedItem).ToString().Trim().Replace("&", "and")
                   // + ",@P_SCHEME=" + Convert.ToInt32(ddlScheme.SelectedValue)
                    //+ ",@P_SEM=" + (ddlSem.SelectedItem).ToString().Trim()
                    //+ ",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim()
                    
            //+",@P_DISPLAYNO=" + 2;
            //+ ",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim();

            //@@P_BRANCH

            //url += "&param=@P_COLLEGE_CODE="+6+",@P_UserId=" + Session["useridname"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString()
            //+ ",@P_COURSESRNO=" + ddlCourse.SelectedValue.ToString().Trim() + ",@P_SESSIONNO=" + ddlSession.SelectedValue.ToString().Trim()
            //+ ",@P_CNAME=" + ddlCourse.SelectedItem.Text.ToString().Trim();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTimetable, this.updTimetable.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_CourseTeacherAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlCourse.SelectedIndex = 0;
        //ddlTeacher.SelectedIndex = 0;
        //pnlView.Visible = false;
        Response.Redirect(Request.Url.ToString());
    }

    #endregion "Submit"

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int version = 1;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=xls";
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "_" + ddlSection.SelectedItem.Text + ".xls";
            url += "&path=~,Reports,Academic,Cry_Atd_TimeTable.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["username"].ToString() + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_VERSION=" + version + ",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim();
            //+ ",@P_DISPLAYNO=" + 2;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTimetable, this.updTimetable.GetType(), "controlJSScript", sb.ToString(), true);

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnHorPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportHorizontal("TIME", "Cry_Atd_TimeTable_Horizontal.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_CourseTeacherAllotment.btnPrint_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportHorizontal(string reportTitle, string rptFileName)
    {
        try
        {
            int version = 2;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + 
                    ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + 
                    ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + 
                    ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + 
                    ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + 
                    ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) +
                    ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) +
                    //",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim() +
                    ",@P_VERSION=" + version + "";
            

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTimetable, this.updTimetable.GetType(), "controlJSScript", sb.ToString(), true);
       
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_CourseTeacherAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnHorExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int version = 2;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=xls";
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "_" + ddlSection.SelectedItem.Text + ".xls";
            url += "&path=~,Reports,Academic,Cry_Atd_TimeTable_Horizontal.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +
                ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) +
                ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) +
                ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) +
                ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) +
                ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) +
                ",@P_VERSION=" + version;
                //",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim();
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTimetable, this.updTimetable.GetType(), "controlJSScript", sb.ToString(), true);
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updAttReport,this.updAttReport.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlHorVerTimetable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            if (ddlHorVerTimetable.SelectedIndex == 1)
            {
                DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue), 1);
                pnlSlots.Visible = true;
                pnlView.Visible = true;
                lvView.Visible = true;
                trVer.Visible = false;
                trHor.Visible = true;
                btnPrint.Enabled = true;
                btnExcel.Enabled = true;
                btnHorPrint.Enabled = false;
                btnHorExcel.Enabled = false;
                lvView.DataSource = ds;
                lvView.DataBind();
            }
            else
            {
                DataSet ds = objCTA.DisplayTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue), 2);
                pnlSlots.Visible = true;
                pnlView.Visible = true;
                lvView.Visible = true;
                trHor.Visible = false;
                trVer.Visible = true;
                btnPrint.Enabled = false;
                btnExcel.Enabled = false;
                btnHorPrint.Enabled = true;
                btnHorExcel.Enabled = true;
                lvVertical.DataSource = ds;
                lvVertical.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSection_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvVertical_DataBound(object sender, EventArgs e)
    {
        DataSet ds1 = null;
        int i = 0;
        ds1 = objCommon.FillDropDown("ACD_TIME_SLOT", "SLOTNO", "SLOTNAME", "DEGREENO= " + Convert.ToInt16(ddlDegree.SelectedValue), "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            if (lvVertical.Visible == false)
                return;
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl1 = (sender as ListView).FindControl("lbl1") as Label;
                lbl1.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl1.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl2 = (sender as ListView).FindControl("lbl2") as Label;
                lbl2.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl2.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl3 = (sender as ListView).FindControl("lbl3") as Label;
                lbl3.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl3.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl4 = (sender as ListView).FindControl("lbl4") as Label;
                lbl4.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl4.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl5 = (sender as ListView).FindControl("lbl5") as Label;
                lbl5.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl5.Visible = true;
            }

            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl6 = (sender as ListView).FindControl("lbl6") as Label;
                lbl6.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl6.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl7 = (sender as ListView).FindControl("lbl7") as Label;
                lbl7.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl7.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl8 = (sender as ListView).FindControl("lbl8") as Label;
                lbl8.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl8.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl9 = (sender as ListView).FindControl("lbl9") as Label;
                lbl9.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl9.Visible = true;
            }
            if (ds1.Tables[0].Rows.Count > i)
            {
                Label lbl10 = (sender as ListView).FindControl("lbl10") as Label;
                lbl10.Text = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                i++;
                lbl10.Visible = true;
            }
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lvSlots.DataSource = null;
        //lvSlots.DataBind();
        //FillDropdown();
    }

}
