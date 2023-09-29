//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TIME TABLE CREATION                                                 
// CREATION DATE : 1-JUNE-2011
// CREATED BY    : GAURAV S SONI                               
// MODIFIED BY   : NEHA BARANWAL  - Rishabh Bajirao
// MODIFIED DESC : 20-NOV-2019    - 29032023
//=================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web;

public partial class ACADEMIC_TIMETABLE_TimeTable_Create : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttE = new AcdAttendanceModel();

    #region Declaration
    string Message = string.Empty;
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    int id = 0;
    int count = 0;
    string value = string.Empty.Trim();
    string value1 = string.Empty.Trim();
    string value2, value3, value4 = string.Empty.Trim();
    int sessionsrno, coursesrno, subjectsrno, teachersrno, slotsrno, no, thpr = 0;
    int flag, flag1, flag2, flag3, flag4, flag5, flag6, flag7 = 0;
    int flag301 = 0, flag402 = 0;
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

    static string sessionuid = string.Empty;
    static string IpAddress = string.Empty;

    static string session, scheme, semester, section, slottype, clgID, OrgID;


    int totlength, monlen, tuelen, wedlen, thurslen, frilen, satlen = 0;
    #endregion Declaration

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //to fill slots
                this.FillSlots();

                //to load all dropdown list
                this.PopulateDropDownList();

                //assign session values to static variables
                sessionuid = System.Web.HttpContext.Current.Session["userno"].ToString();
                IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                //Session["ipAddress"].ToString();
                Session["transferTbl"] = null;
                OrgID = Session["OrgId"].ToString();
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
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
                Response.Redirect("~/notauthorized.aspx?page=TimeTable.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TimeTable.aspx");
        }
    }


    //load all dropdown
    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

            ddlSession.SelectedIndex = 0;

            if (Session["usertype"].ToString() != "1")
            {
                //string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO")
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
                ; //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString()+"", "DEPTNAME ASC");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO IN(" + Session["userdeptno"].ToString() + ")" + "", "DEPTNAME ASC");
            }
            else
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
            }
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ", "DEGREENO");
        }
        catch
        {
            throw;
        }
    }

    //to fill slots
    protected void FillSlots()
    {
        //if (ddlDegree.SelectedIndex > 0 && ddlSession.SelectedIndex > 0)
        //{
        DataSet ds = objAttC.GetSlots(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt16(ddlSlotType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvSlots.DataSource = ds;
            lvSlots.DataBind();
            pnlSlots.Visible = true;
        }
        //}
    }

    //toshow faculties0
    public void ShowFaculty(DateTime startDate, DateTime endDate)
    {
        try
        {
            Boolean lockCT;
            lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + " AND SECTIONNO =" + ddlSection.SelectedValue + " AND ORGANIZATIONID=" + Session["OrgId"] + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ViewState["college_id"]) + " AND ORGANIZATIONID=" + Session["OrgId"].ToString() == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ORGANIZATIONID=" + Session["OrgId"] + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ViewState["college_id"]));
            DataSet Dsfac = objAttC.GetRegularFacultyWiseCourses(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(ddlSection.SelectedValue), Convert.ToInt16(ddlSlotType.SelectedValue), startDate, endDate, Convert.ToInt32(Session["OrgId"].ToString()));
            if (lvSlots.Items.Count > 0)
            {
                for (int i = 0; i < lvSlots.Items.Count; i++)
                {
                    Label LblTime = lvSlots.Items[i].FindControl("lbltime") as Label;
                    HtmlControl tdmon = (HtmlControl)lvSlots.Items[i].FindControl("drp1");

                    //hdnTue
                    HtmlControl tdtue = (HtmlControl)lvSlots.Items[i].FindControl("drp2");

                    //hdnWed
                    HtmlControl tdwed = (HtmlControl)lvSlots.Items[i].FindControl("drp3");

                    //hdnThurs
                    HtmlControl tdths = (HtmlControl)lvSlots.Items[i].FindControl("drp4");

                    //hdnFri
                    HtmlControl tdfri = (HtmlControl)lvSlots.Items[i].FindControl("drp5");

                    //hdnSat
                    HtmlControl tdsat = (HtmlControl)lvSlots.Items[i].FindControl("drp6");

                    //hdnSun
                    HtmlControl tdsun = (HtmlControl)lvSlots.Items[i].FindControl("drp7");

                    if (Dsfac.Tables.Count > 0)
                    {
                        for (int a = 0; a < Dsfac.Tables[0].Rows.Count; a++)
                        {
                            string CTNO = Dsfac.Tables[0].Rows[a]["CT_NO"].ToString();
                            string Faculty = Dsfac.Tables[0].Rows[a]["FACNAME"].ToString();
                            string Slot = Dsfac.Tables[0].Rows[a]["SLOTNO"].ToString();
                            string DayNo = Dsfac.Tables[0].Rows[a]["DAYNO"].ToString();
                            string FacultyName = Dsfac.Tables[0].Rows[a]["FACULTYNAME"].ToString();
                            string roomid = Dsfac.Tables[0].Rows[a]["ROOMNO"].ToString() == null || Dsfac.Tables[0].Rows[a]["ROOMNO"].ToString() == "" ? "0" : Dsfac.Tables[0].Rows[a]["ROOMNO"].ToString();
                            // string TTNO = Dsfac.Tables[0].Rows[a]["TTNO"].ToString();
                            string TTNO = "0";
                            if (Slot == LblTime.ToolTip.ToString())
                            {
                                #region mon
                                //  mon
                                if (DayNo == "1")
                                {
                                    Label lblmon = new Label();
                                    DropDownList ddlRoom = new DropDownList();
                                    ddlRoom.ID = a + "ddlRoom";
                                    lblmon.ID = "lblmon" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdnmon" + a.ToString();
                                    //select2-li
                                    //#('.select2-li').select2() -jquery
                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("runat", "server");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");

                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "limon" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdmon.Controls.Add(limonday);
                                    //limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(ddlRoom);
                                    limonday.Controls.Add(hdnmon);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    //limonday.Attributes.Add("Class", "droppable");
                                    if (lockCT == true)
                                    {
                                        //imgbtn.Visible = false;
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdmon.Attributes.Add("Class", "dropc");
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        // imgbtn.Visible = true;
                                    }


                                    DataSet dt1 = (DataSet)Session["RoomData"];
                                    ddlRoom.Items.Clear();

                                    //ddlRoom.CssClass = "form-control";
                                    ddlRoom.DataSource = dt1.Tables[0];
                                    ddlRoom.DataValueField = "ROOMNO";
                                    ddlRoom.DataTextField = "ROOMNAME";
                                    ddlRoom.DataBind();
                                    ddlRoom.Items.Insert(0, "Select Room");
                                    ddlRoom.SelectedItem.Value = "0";
                                    ddlRoom.Attributes.Add("onchange", "MyDropDownList_OnChange(this)");
                                    if (roomid != "0")
                                        ddlRoom.SelectedValue = roomid;
                                    //  limonday.Attributes.Add("onclick", "(function(_this){_this.parentNode.removeChild(_this);})(this);");    
                                    //BindRoom(CTNO, Slot,roomid); // Added By Rishabh on 27/10/2022
                                }
                                #endregion mon

                                #region Tue
                                //Tue
                                if (DayNo == "2")
                                {
                                    Label lblmon = new Label();
                                    DropDownList ddlRoom = new DropDownList();
                                    ddlRoom.ID = a + "ddlRoom";
                                    lblmon.ID = "lbltue" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdntue" + a.ToString();

                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");

                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "litue" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdtue.Controls.Add(limonday);
                                    // limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(ddlRoom);
                                    limonday.Controls.Add(hdnmon);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    //limonday.Attributes.Add("Class", "droppable");
                                    if (lockCT == true)
                                    {
                                        //imgbtn.Visible = false;
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdtue.Attributes.Add("Class", "dropc");
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        //imgbtn.Visible = true;
                                    }

                                    DataSet dt1 = (DataSet)Session["RoomData"];
                                    ddlRoom.Items.Clear();
                                    ddlRoom.CssClass = "form-control";
                                    ddlRoom.DataSource = dt1.Tables[0];
                                    ddlRoom.DataValueField = "ROOMNO";
                                    ddlRoom.DataTextField = "ROOMNAME";
                                    ddlRoom.DataBind();
                                    ddlRoom.Items.Insert(0, "Select Room");
                                    ddlRoom.SelectedItem.Value = "0";
                                    ddlRoom.Attributes.Add("onchange", "MyDropDownList_OnChange(this)");
                                    if (roomid != "0")
                                        ddlRoom.SelectedValue = roomid;
                                    // BindRoom(CTNO, Slot, roomid);
                                }
                                #endregion Tue

                                #region wed
                                //wed
                                if (DayNo == "3")
                                {
                                    Label lblmon = new Label();
                                    DropDownList ddlRoom = new DropDownList();
                                    ddlRoom.ID = a + "ddlRoom";
                                    lblmon.ID = "lblwed" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdnwed" + a.ToString();

                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");

                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "liwed" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdwed.Controls.Add(limonday);
                                    //limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(ddlRoom);
                                    limonday.Controls.Add(hdnmon);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    //limonday.Attributes.Add("Class", "droppable");
                                    if (lockCT == true)
                                    {
                                        //imgbtn.Visible = false;
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdwed.Attributes.Add("Class", "dropc");
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        //imgbtn.Visible = true;
                                    }
                                    DataSet dt1 = (DataSet)Session["RoomData"];
                                    ddlRoom.Items.Clear();
                                    ddlRoom.CssClass = "form-control";
                                    ddlRoom.DataSource = dt1.Tables[0];
                                    ddlRoom.DataValueField = "ROOMNO";
                                    ddlRoom.DataTextField = "ROOMNAME";
                                    ddlRoom.DataBind();
                                    ddlRoom.Items.Insert(0, "Select Room");
                                    ddlRoom.SelectedItem.Value = "0";
                                    ddlRoom.Attributes.Add("onchange", "MyDropDownList_OnChange(this)");
                                    if (roomid != "0")
                                        ddlRoom.SelectedValue = roomid;
                                }
                                #endregion wed

                                #region ths
                                // ths
                                if (DayNo == "4")
                                {
                                    Label lblmon = new Label();
                                    DropDownList ddlRoom = new DropDownList();
                                    ddlRoom.ID = a + "ddlRoom";
                                    lblmon.ID = "lblths" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdnths" + a.ToString();

                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");

                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "liths" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdths.Controls.Add(limonday);
                                    // limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(ddlRoom);
                                    limonday.Controls.Add(hdnmon);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    //limonday.Attributes.Add("Class", "droppable");
                                    if (lockCT == true)
                                    {
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdths.Attributes.Add("Class", "dropc");
                                        // imgbtn.Visible = false;
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        //imgbtn.Visible = true;
                                    }
                                    //ddlThursday.SelectedValue = roomid;  // Added By Rishabh on 27/10/2022
                                    DataSet dt1 = (DataSet)Session["RoomData"];
                                    ddlRoom.Items.Clear();
                                    ddlRoom.CssClass = "form-control";
                                    ddlRoom.DataSource = dt1.Tables[0];
                                    ddlRoom.DataValueField = "ROOMNO";
                                    ddlRoom.DataTextField = "ROOMNAME";
                                    ddlRoom.DataBind();
                                    ddlRoom.Items.Insert(0, "Select Room");
                                    ddlRoom.SelectedItem.Value = "0";
                                    ddlRoom.Attributes.Add("onchange", "MyDropDownList_OnChange(this)");
                                    if (roomid != "0")
                                        ddlRoom.SelectedValue = roomid;
                                }
                                #endregion ths

                                #region fri
                                // fri 
                                if (DayNo == "5")
                                {
                                    Label lblmon = new Label();
                                    DropDownList ddlRoom = new DropDownList();
                                    ddlRoom.ID = a + "ddlRoom";
                                    lblmon.ID = "lblfri" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdnfri" + a.ToString();

                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");

                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "lifri" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdfri.Controls.Add(limonday);
                                    //limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(hdnmon);
                                    limonday.Controls.Add(ddlRoom);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    //limonday.Attributes.Add("Class", "droppable");
                                    if (lockCT == true)
                                    {
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdfri.Attributes.Add("Class", "dropc");
                                        //imgbtn.Visible = false;
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        //imgbtn.Visible = true;
                                    }

                                    DataSet dt1 = (DataSet)Session["RoomData"];
                                    ddlRoom.Items.Clear();
                                    ddlRoom.CssClass = "form-control";
                                    ddlRoom.DataSource = dt1.Tables[0];
                                    ddlRoom.DataValueField = "ROOMNO";
                                    ddlRoom.DataTextField = "ROOMNAME";
                                    ddlRoom.DataBind();
                                    ddlRoom.Items.Insert(0, "Select Room");
                                    ddlRoom.SelectedItem.Value = "0";
                                    ddlRoom.Attributes.Add("onchange", "MyDropDownList_OnChange(this)");
                                    if (roomid != "0")
                                        ddlRoom.SelectedValue = roomid;
                                }
                                #endregion fri

                                #region sat
                                // sat
                                if (DayNo == "6")
                                {
                                    Label lblmon = new Label();
                                    DropDownList ddlRoom = new DropDownList();
                                    ddlRoom.ID = a + "ddlRoom";
                                    lblmon.ID = "lblsat" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdnsat" + a.ToString();

                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");
                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "lisat" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdsat.Controls.Add(limonday);
                                    // limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(ddlRoom);
                                    limonday.Controls.Add(hdnmon);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    //limonday.Attributes.Add("Class", "droppable");
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    if (lockCT == true)
                                    {
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdsat.Attributes.Add("Class", "dropc");
                                        //imgbtn.Visible = false;
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        //imgbtn.Visible = true;
                                    }
                                    DataSet dt1 = (DataSet)Session["RoomData"];
                                    ddlRoom.Items.Clear();
                                    ddlRoom.CssClass = "form-control";
                                    ddlRoom.DataSource = dt1.Tables[0];
                                    ddlRoom.DataValueField = "ROOMNO";
                                    ddlRoom.DataTextField = "ROOMNAME";
                                    ddlRoom.DataBind();
                                    ddlRoom.Items.Insert(0, "Select Room");
                                    ddlRoom.SelectedItem.Value = "0";
                                    ddlRoom.Attributes.Add("onchange", "MyDropDownList_OnChange(this)");
                                    if (roomid != "0")
                                        ddlRoom.SelectedValue = roomid;
                                }
                                #endregion sat

                                #region sun
                                // sat
                                if (DayNo == "7")
                                {
                                    Label lblmon = new Label();
                                    lblmon.ID = "lblsun" + a.ToString();
                                    HiddenField hdnmon = new HiddenField();
                                    hdnmon.ID = "hdnsun" + a.ToString();

                                    //ImageButton imgbtn = new ImageButton();
                                    //imgbtn.ID = "imgbtn" + a.ToString();
                                    //imgbtn.Attributes.Add("src", "../../images/delete.gif");
                                    //imgbtn.Attributes.Add("Style", "cursor: pointer; font-size: 3px; ");
                                    //imgbtn.Attributes.Add("ImageAlign", "Left");
                                    //imgbtn.Attributes.Add("onclick", "return removediv(this);");
                                    HtmlGenericControl limonday = new HtmlGenericControl("div");
                                    limonday.ID = "lisun" + a.ToString();
                                    limonday.Attributes.Add("class", "SaveData");
                                    tdsun.Controls.Add(limonday);
                                    //limonday.Controls.Add(imgbtn);
                                    limonday.Controls.Add(lblmon);
                                    limonday.Controls.Add(hdnmon);
                                    hdnmon.Value = TTNO;
                                    lblmon.Text = Faculty; lblmon.ToolTip = CTNO; limonday.Visible = true;
                                    lblmon.Attributes.Add("alternatetext", CTNO);
                                    //limonday.Attributes.Add("Class", "droppable");
                                    if (lockCT == true)
                                    {
                                        lblmon.Attributes.Add("Class", "dropb");
                                        tdsun.Attributes.Add("Class", "dropc");
                                        //imgbtn.Visible = false;
                                    }
                                    else
                                    {
                                        limonday.Attributes.Add("Class", "droppable");
                                        //imgbtn.Visible = true;
                                    }
                                }
                                #endregion sun
                            }
                        }
                    }
                    else
                    {
                        // btnLock.Visible = false;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion "Page Event"

    #region "Selected Index Changed"

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlDegree.SelectedIndex = 0;
            //ddlBranch.SelectedIndex = 0;
            //ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divcourses.Visible = false;
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            divStartDate.Visible = true;
            divEndDate.Visible = true;

            session = "";
            session = ddlSession.SelectedValue;
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlCourseType.Items.Clear();
            ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
            lstbxCourse.Items.Clear();
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            if (Session["usertype"].ToString() != "1")
            {
                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN(" + Session["userdeptno"].ToString() + ")" + "", "DEPTNAME ASC");
                ddlDepartment.Focus();
            }
            else
            {
                // clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + clgID + "", "DEPTNAME ASC");
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "DEPTNAME ASC");
                ddlDepartment.Focus();
            }
            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlBranch.SelectedIndex = 0;
            //ddlScheme.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            lstbxCourse.Items.Clear();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            divStartDate.Visible = true;
            divEndDate.Visible = true;
            //if (ddlDegree.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " +Convert.ToInt32(ViewState["degreeno"]) + " AND COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue, "B.BRANCHNO");
            //    ddlBranch.Focus();
            //}
            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            divStartDate.Visible = true;
            divEndDate.Visible = true;
            lstbxCourse.Items.Clear();
            //if (ddlBranch.SelectedIndex > 0)
            //{
            this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"]));
            //}
            //if (ddlBranch.SelectedValue == "99")    //FIRST YEAR
            //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "REPLACE(SCHEMENAME,'CIVIL ENGINEERING','FIRST YEAR') SCHEMENAME", "DEGREENO = 1 AND BRANCHNO = 1 AND SCHEMENO IN (1,24)", "SCHEMENO DESC");
            // else
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND BRANCHNO =" + Convert.ToInt32(ViewState["branchno"]), "SCHEMENO DESC");

            //ddlScheme.Focus();

            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            ddlSem.Focus();
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            divStartDate.Visible = true;
            divEndDate.Visible = true;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            scheme = "";
            scheme = ViewState["schemeno"].ToString();
            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divcourses.Visible = false;
            lvSlots.DataSource = null;
            lvSlots.DataBind();
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            divStartDate.Visible = true;
            divEndDate.Visible = true;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlCourseType.Items.Clear();
            ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
            lstbxCourse.Items.Clear();
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            if (ddlSem.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedValue == "99")
                //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR LEFT OUTER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_SCHEME WHERE DEGREENO = 1) AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                //else
                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0 AND SR.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND SR.ORGANIZATIONID=" + Session["OrgId"], "SC.SECTIONNAME");

                //to show session of attendnace date
                AttendanceConfigDate();

            }
            ddlSection.Focus();
            semester = "";
            semester = ddlSem.SelectedValue;

            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);

        }
        catch
        {
            throw;
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divcourses.Visible = false;
            ddlCourseType.Items.Clear();
            ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
            lstbxCourse.Items.Clear();
            ddlSlotType.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            ddlSlotType.Focus();
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            divStartDate.Visible = true;
            divEndDate.Visible = true;

            section = "";
            section = ddlSection.SelectedValue;
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            //to enable the save btn
            objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0 AND ACTIVESTATUS=1", "SLOTTYPENO");
            // added Active status condition for slot type for ticket no 49038


            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);


            DateTime SDate = Convert.ToDateTime(objAttC.GetAttendanceStartDate(Convert.ToInt32(session), Convert.ToInt32(scheme), Convert.ToInt32(semester), Convert.ToInt32(Session["TimeTable_College_id"]), Convert.ToInt32(Session["OrgId"])));

            DateTime EDate = Convert.ToDateTime(objAttC.GetAttendanceEndDate(Convert.ToInt32(session), Convert.ToInt32(scheme), Convert.ToInt32(semester), Convert.ToInt32(Session["TimeTable_College_id"]), Convert.ToInt32(Session["OrgId"])));



            //DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "ISNULL(CONVERT(VARCHAR(10),A.START_DATE,103),'01/01/0001')", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND COLLEGE_ID=" + Convert.ToInt32(clgID) + " AND A.OrganizationId=" +Session["OrgId"]));
            //DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "ISNULL(CONVERT(VARCHAR(10),A.END_DATE,103),'01/01/0001')", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND COLLEGE_ID=" + Convert.ToInt32(clgID) + " AND A.OrganizationId=" + Session["OrgId"]));

            if (SDate == Convert.ToDateTime("01/01/0001") || EDate == Convert.ToDateTime("01/01/0001"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Attendance Configuration is not Done.')", true);
                ddlSection.Focus();
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSem_SelectedIndexChanged(sender, e);
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSlotType.SelectedIndex > 0)
            {
                slottype = "";
                slottype = ddlSlotType.SelectedValue;

                ddlExistingDates.SelectedIndex = 0;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                divStartDate.Visible = true;
                divEndDate.Visible = true;
                ddlExistingDates.Focus();
                pnlSlots.Visible = false;
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                //to enable the save btn
                LoadExisitingDates();
                OrgID = Session["OrgId"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);
                pnlSlots.Visible = false;
                if (ddlSem.SelectedIndex > 0)
                {
                    Boolean lockCT;
                    lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0") == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND ORGANIZATIONID=" + Session["OrgId"]));
                    Session["lockCT"] = lockCT;

                    //to load all faculties to drag and drop
                    // DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN User_Acc A ON (A.UA_NO = T.UA_NO OR A.UA_NO = T.ADTEACHER) INNER JOIN ACD_COURSE AC ON(T.COURSENO = AC.COURSENO) INNER JOIN ACD_SUBJECTTYPE ST ON(ST.SUBID = AC.SUBID)", "UPPER(CONCAT(COURSE_NAME,' - ',A.UA_FULLNAME)) AS UA_FULLNAME,T.CCODE+' - '+UPPER(A.UA_FULLNAME)+(CASE WHEN ISNULL(T.UA_NO,0)<>0 THEN '<SPAN STYLE=" + "COLOR:#f20943;FONT-WEIGHT:BOLD" + "> $</SPAN>' WHEN ISNULL(T.UA_NO,0)=0 AND ISNULL(T.ADTEACHER,0)<>0 THEN ' <SPAN STYLE=" + "COLOR:#f8036b;FONT-WEIGHT:BOLD" + "> #</SPAN>' END)+' [SEC: '+ISNULL(DBO.FN_DESC('SECTIONNAME',T.SECTIONNO)COLLATE DATABASE_DEFAULT,'-')+']'+(CASE WHEN (ST.TH_PR=2 AND ST.SEC_BATCH=2) THEN +'[BAT: '+ISNULL(DBO.FN_DESC('BATCH',T.BATCHNO)COLLATE DATABASE_DEFAULT,'-')+']' ELSE '' END) FACULTY", "A.UA_NO,CT_NO,T.COURSENO,T.SUBID", "T.SESSIONNO = " + ddlSession.SelectedValue + " AND T.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND T.SEMESTERNO=" + ddlSem.SelectedValue + " AND T.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(T.CANCEL,0)=0" + " AND T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND T.ORGANIZATIONID=" + Session["OrgId"], "A.UA_FULLNAME");

                    DataSet ds = objAttC.GetFacultyForTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
                    if (ds.Tables[0].Rows.Count > 0 && ds != null && ds.Tables.Count > 0)
                    {
                        if (lockCT == false)
                        {
                            // btnLock.Visible = true;
                            dListFaculty.DataSource = ds;
                            dListFaculty.DataBind();
                            divcourses.Visible = true;
                        }
                        else
                        {
                            // btnLock.Visible = false;
                            objCommon.DisplayMessage(this, "Time Table already locked! you cannot modified it.", this);
                            dListFaculty.DataSource = null;
                            dListFaculty.DataBind();
                            divcourses.Visible = false;
                        }
                        FillSlots();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Course Teacher allotment not Found for this selection !", this);
                        dListFaculty.DataSource = null;
                        dListFaculty.DataBind();
                        divcourses.Visible = false;
                    }
                }

                AttendanceConfigDate();
            }
            else
            {
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                pnlSlots.Visible = false;
                divcourses.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlExistingDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSlotType.SelectedIndex > 0)
            {
                if (ddlExistingDates.SelectedIndex > 0)
                {
                    divDateDetails.Visible = false;
                    txtStartDate.Text = "";
                    startdate = "";
                    enddate = "";
                    txtEndDate.Text = "";
                    divStartDate.Visible = false;
                    divEndDate.Visible = false;

                    //to disable the save btn
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "disableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {disableBtn();});", true);

                    pnlSlots.Visible = false;
                    if (ddlSem.SelectedIndex > 0)
                    {
                        Boolean lockCT;
                        lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0") == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ORGANIZATIONID=" + Session["OrgId"] + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));

                        Session["lockCT"] = lockCT;

                        //DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN User_Acc A ON (A.UA_NO = T.UA_NO OR A.UA_NO = T.ADTEACHER) INNER JOIN ACD_COURSE AC ON(T.COURSENO = AC.COURSENO)", "UPPER(CONCAT(COURSE_NAME,' - ',A.UA_FULLNAME)) AS UA_FULLNAME,T.CCODE+' - '+UPPER(A.UA_FULLNAME)+(CASE WHEN ISNULL(T.UA_NO,0)<>0 THEN '<SPAN STYLE=" + "COLOR:#f20943;FONT-WEIGHT:BOLD" + "> $</SPAN>' WHEN ISNULL(T.UA_NO,0)=0 AND ISNULL(T.ADTEACHER,0)<>0 THEN ' <SPAN STYLE=" + "COLOR:#f8036b;FONT-WEIGHT:BOLD" + "> #</SPAN>' END)+' [SEC: '+ISNULL(DBO.FN_DESC('SECTIONNAME',T.SECTIONNO)COLLATE DATABASE_DEFAULT,'-')+']'+(CASE WHEN ((T.SUBID=2) OR (T.ORGANIZATIONID=2 AND T.SUBID IN (4,12,15)) OR (T.SUBID=1 AND  IS_TUTORIAL=1)) THEN +'[BAT: '+ISNULL(DBO.FN_DESC('BATCH',T.BATCHNO)COLLATE DATABASE_DEFAULT,'-')+']' ELSE '' END) FACULTY", "A.UA_NO,CT_NO,T.COURSENO,T.SUBID", "T.SESSIONNO = " + ddlSession.SelectedValue + " AND T.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND T.SEMESTERNO=" + ddlSem.SelectedValue + " AND T.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(T.CANCEL,0)=0" + " AND T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND T.ORGANIZATIONID=" + Session["OrgId"], "A.UA_FULLNAME");
                        DataSet ds = objAttC.GetFacultyForTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
                        if (ds.Tables[0].Rows.Count > 0 && ds != null)
                        {
                            if (lockCT == false)
                            {
                                // btnLock.Visible = true;
                                dListFaculty.DataSource = ds;
                                dListFaculty.DataBind();
                            }
                            else
                            {
                                //btnLock.Visible = false;
                                objCommon.DisplayMessage(this, "Time Table already locked! you cannot modified it.", this);
                                dListFaculty.DataSource = null;
                                dListFaculty.DataBind();
                                divcourses.Visible = false;
                            }
                            FillSlots();

                            string myStr = ddlExistingDates.SelectedItem.ToString();
                            string[] ssizes = myStr.Split(' ');
                            string startdate1 = ssizes[0].ToString();
                            string enddate1 = ssizes[2].ToString();

                            ShowFaculty(Convert.ToDateTime(startdate1), Convert.ToDateTime(enddate1));
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Course Teacher allotment not Found for this selection !", this);
                            dListFaculty.DataSource = null;
                            dListFaculty.DataBind();
                            divcourses.Visible = false;
                        }
                    }
                    AttendanceConfigDate();
                }
                else
                {
                    //to enable the save btn
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);
                    txtStartDate.Text = "";
                    startdate = "";
                    enddate = "";
                    txtEndDate.Text = "";
                    divStartDate.Visible = true;
                    divEndDate.Visible = true;
                    divDateDetails.Visible = true;
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select SlotType", this);
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                pnlSlots.Visible = false;
                ddlSlotType.Focus();
                ddlExistingDates.SelectedIndex = 0;
                divDateDetails.Visible = true;
                divcourses.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    //to  load exisitng dates
    public void LoadExisitingDates()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlClgname.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0)
            {
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;

                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TT INNER JOIN  ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES", "START_DATE,END_DATE,MONTH(START_DATE) AS STARTDATEMONTH", "ISNULL(CT.CANCEL,0)=0 AND ISNULL(ISEXTERNAL,0)=0 AND ISNULL(TT.CANCEL,0)=0 and CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + "and SLOTTYPE=" + ddlSlotType.SelectedValue + " AND CT.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND CT.OrganizationId=" + Session["OrgId"].ToString(), "MONTH(START_DATE) ");
                if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                {
                    ddlExistingDates.DataSource = dsGetExisitingDates.Tables[0];
                    ddlExistingDates.DataTextField = "EXISTINGDATES";
                    ddlExistingDates.DataBind();
                }
                else
                {
                    ddlExistingDates.DataSource = null;
                    ddlExistingDates.DataBind();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    static string startdate, enddate;

    //to check time table dates already exist or not
    public int AlreadyExistDates(DateTime date)
    {
        int flag = 0;

        DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG T INNER JOIN ACD_COURSE_TEACHER C ON C.CT_NO=T.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "COUNT(DISTINCT 1)", "", "ISNULL(T.CANCEL,0)=0 AND ISNULL(ISEXTERNAL,0)=0 AND ISNULL(C.CANCEL,0)=0 AND (CONVERT(DATE,'" + Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "',103) BETWEEN  START_DATE AND END_DATE ) and C.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + " AND  TTS.SLOTTYPE = " + ddlSlotType.SelectedValue + "AND T.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND C.OrganizationId=" + Session["OrgId"].ToString(), "");
        if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
        {
            if (dsGetExisitingDates.Tables[0].Rows[0][0].ToString() == "1")
            {
                flag = 1;
            }
        }
        return flag;
    }

    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);
            txtEndDate.Text = string.Empty;

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this, "Please Select Dates Properly", this);
                    txtStartDate.Text = string.Empty;
                    //startdate = string.Empty;
                    txtStartDate.Focus();
                }
            }

            if (AlreadyExistDates(Convert.ToDateTime(txtStartDate.Text)) == 1)
            {
                objCommon.DisplayMessage(this, "Time Table Already Exist For this Start Date", this);
                txtStartDate.Text = string.Empty;
                //startdate = string.Empty;
                txtStartDate.Focus();
            }

            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND A.OrganizationId=" + Session["OrgId"].ToString()));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND A.OrganizationId=" + Session["OrgId"].ToString()));

            if (Convert.ToDateTime(txtStartDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                txtStartDate.Text = string.Empty;
                //startdate = string.Empty;
                txtStartDate.Focus();
            }
            else if (Convert.ToDateTime(txtStartDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                txtStartDate.Text = string.Empty;
                //startdate = string.Empty;
                txtStartDate.Focus();
            }


            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                startdate = string.Empty;
                startdate = txtStartDate.Text;
            }
        }
        catch
        {
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                startdate = string.Empty;
                startdate = txtStartDate.Text;
            }
        }
    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //to enable the save btn
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "enableBtn();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {enableBtn();});", true);

            if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this, "Please Select Dates Properly", this);
                    txtEndDate.Text = string.Empty;
                    //enddate = string.Empty;
                    txtEndDate.Focus();
                }
            }
            if (AlreadyExistDates(Convert.ToDateTime(txtEndDate.Text)) == 1)
            {
                objCommon.DisplayMessage(this, "Time Table Already Exist For this End Date", this);
                txtEndDate.Text = string.Empty;
                //enddate = string.Empty;
                txtEndDate.Focus();
            }



            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND A.OrganizationId=" + Session["OrgId"].ToString()));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND A.OrganizationId=" + Session["OrgId"].ToString()));

            if (Convert.ToDateTime(txtEndDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                //  objCommon.DisplayMessage(this.Page, "End date should be greater than session start date", this.Page);
                txtEndDate.Text = string.Empty;
                // enddate = string.Empty;
                txtEndDate.Focus();
            }
            else if (Convert.ToDateTime(txtEndDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                // objCommon.DisplayMessage(this.Page, "End date should be less than session end date", this.Page);
                txtEndDate.Text = string.Empty;
                //enddate = string.Empty;
                txtEndDate.Focus();
            }


            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                enddate = string.Empty;
                enddate = txtEndDate.Text;
            }
        }
        catch
        {
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                enddate = string.Empty;
                enddate = txtEndDate.Text;
            }
        }
    }

    #endregion "Selected Index Changed"

    #region General
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSlotType.SelectedIndex = 0;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ddlExistingDates.SelectedIndex = 0;
        lvSlots.DataSource = null;
        lvSlots.DataBind();
        dListFaculty.DataSource = null;
        dListFaculty.DataBind();
        divDateDetails.Visible = false;
        ddlSchoolInstitute.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        session = "";
        scheme = "";
        semester = "";
        section = "";
        slottype = "";
        startdate = "";
        enddate = "";
        clgID = "";
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlExistingDates.Items.Clear();
        ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));

        ddlClgname.SelectedIndex = 0;
        divcourses.Visible = false;

    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        objAttE.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        objAttE.DEGREENO = Convert.ToInt32(ViewState["degreeno"]);
        objAttE.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
        objAttE.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
        objAttE.SECTIONNO = Convert.ToInt32(ddlSection.SelectedValue);
        objAttE.LOCK_DATE = Convert.ToDateTime(DateTime.Now);
        objAttE.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        ret = objAttC.AddTTLock(objAttE, ref Message);

        if (ret <= 0)
        {
            if (Message.ToString().Trim().Trim() == "")
            {
                objCommon.DisplayMessage(this.Page, "No Modified", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Exception Occured", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Time Table locked Successfully!", this.Page);
            // btnLock.Visible = false;
        }
        ddlSection_SelectedIndexChanged(new object(), new EventArgs());
    }

    /// <summary>
    /// GETS AND RETURNS THE SLOT, DAY AND DAYNAME IN ARRAY STRING.
    /// </summary>
    /// <param name="Slotname"></param>
    /// <returns></returns>

    private string[] GetSlot_Day(string Slotname)
    {
        char sp = '-';
        string[] data = Slotname.Split(sp);

        return data;
    }

    private DataTable GetEntranceDataTable1()
    {
        DataTable objEntrance = new DataTable();
        objEntrance.Columns.Add(new DataColumn("DAYNO_EMPTY", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("DAY_EMPTY", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("SLOTNO_EMPTY", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("SLOT_EMPTY", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("SLOT_NAME_EMPTY", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("INDEX_EMPTY", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("DAYNO_LECT", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("DAY_LECT", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("SLOTNO_LECT", typeof(int)));
        objEntrance.Columns.Add(new DataColumn("SLOT_LECT", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("SLOT_NAME_LECT", typeof(string)));
        objEntrance.Columns.Add(new DataColumn("INDEX_LECT", typeof(int)));
        return objEntrance;
    }

    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add("Please Select");
        ddlsemester.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsemester.DataSource = ds;
            ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlsemester.DataBind();
            ddlsemester.SelectedIndex = 0;
        }
    }

    #endregion General

    DateTime ATT_StartDate, ATT_EndDate;
    public void AttendanceConfigDate()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0)
            {
                ATT_StartDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND A.OrganizationId=" + Session["OrgId"].ToString()));
                ATT_EndDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND A.OrganizationId=" + Session["OrgId"].ToString()));
                divDateDetails.Visible = true;
                lblTitleDate.Text = "Selected Session Start Date : " + ATT_StartDate.ToShortDateString() + " End Date : " + ATT_EndDate.ToShortDateString();

            }
        }
        catch { lblTitleDate.Text = "Selected Session Start Date : - End Date : -"; }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    #region WebMethod
    //web method to create time table
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveTimetableAjax(List<IITMS.UAIMS.AcdAttendanceModel.AttendanceJsonModel> AllotmentData)
    {
        try
        {
            Common objCommon = new Common();
            IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel objE = new IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel();
            AcdAttendanceController objC = new AcdAttendanceController();
            string json1 = JsonConvert.SerializeObject(AllotmentData);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json1);

            string TempStartDate = dt.Rows[0]["StartDate1"].ToString();
            string TempEndDate = dt.Rows[0]["EndDate1"].ToString();


            int outval = 0;
            if (!string.IsNullOrEmpty(sessionuid))
            {
                if (string.IsNullOrEmpty(session) && string.IsNullOrEmpty(scheme) && string.IsNullOrEmpty(semester) && string.IsNullOrEmpty(section) && string.IsNullOrEmpty(slottype) && string.IsNullOrEmpty(clgID))
                {
                    outval = 0;
                }
                if (string.IsNullOrEmpty(TempStartDate))
                {
                    outval = 3;
                }
                else if (string.IsNullOrEmpty(TempEndDate))
                {
                    outval = 4;
                }
                else
                {
                    int startdatecount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND A.COLLEGE_ID = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["TimeTable_College_id"].ToString()) + " AND A.OrganizationId=" + OrgID));
                    int enddatecount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND A.COLLEGE_ID = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["TimeTable_College_id"].ToString()) + " AND A.OrganizationId=" + OrgID));

                    if (startdatecount == 0 && enddatecount == 0)
                    {
                        outval = 9;
                        return JsonConvert.SerializeObject(outval.ToString());
                    }

                    DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND A.COLLEGE_ID = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["TimeTable_College_id"].ToString()) + " AND A.OrganizationId=" + OrgID));
                    DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND A.COLLEGE_ID = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["TimeTable_College_id"].ToString()) + " AND A.OrganizationId=" + OrgID));



                    if (!string.IsNullOrEmpty(TempStartDate))
                    {
                        //to check end date is under session dates or not
                        if (Convert.ToDateTime(TempStartDate) < SDate)
                        {
                            outval = 7;
                            return JsonConvert.SerializeObject(outval.ToString());
                        }
                        else if (Convert.ToDateTime(TempStartDate) > EDate)
                        {
                            outval = 8;
                            return JsonConvert.SerializeObject(outval.ToString());
                        }

                        //to check time table already exist or not
                        DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG T INNER JOIN ACD_COURSE_TEACHER C ON C.CT_NO=T.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "COUNT(DISTINCT 1)", "", "ISNULL(T.CANCEL,0)=0 AND ISNULL(ISEXTERNAL,0)=0 AND ISNULL(C.CANCEL,0)=0 AND  (CONVERT(DATE,'" + Convert.ToDateTime(TempStartDate).ToString("dd-MM-yyyy") + "',103) BETWEEN  START_DATE AND END_DATE ) and C.SESSIONNO=" + session + " and SCHEMENO=" + scheme + " and SEMESTERNO=" + semester + " and SECTIONNO=" + section + " AND  TTS.SLOTTYPE = " + slottype + " AND T.COLLEGE_ID = " + Convert.ToInt32(System.Web.HttpContext.Current.Session["TimeTable_College_id"].ToString()) + " AND T.ORGANIZATIONID=" + OrgID, "");
                        if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                        {
                            if (dsGetExisitingDates.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                outval = 5;
                                return JsonConvert.SerializeObject(outval.ToString());
                            }
                        }


                    }
                    if (!string.IsNullOrEmpty(TempEndDate))
                    {
                        //to check end date is under session dates or not
                        if (Convert.ToDateTime(TempEndDate) < SDate)
                        {
                            outval = 7;
                            return JsonConvert.SerializeObject(outval.ToString());
                        }
                        else if (Convert.ToDateTime(TempEndDate) > EDate)
                        {
                            outval = 8;
                            return JsonConvert.SerializeObject(outval.ToString());
                        }
                        //to check time table already exist or not
                        DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG T INNER JOIN ACD_COURSE_TEACHER C ON C.CT_NO=T.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "COUNT(DISTINCT 1)", "", "ISNULL(T.CANCEL,0)=0 AND ISNULL(C.CANCEL,0)=0 AND (CONVERT(DATE,'" + Convert.ToDateTime(TempEndDate).ToString("dd-MM-yyyy") + "',103) BETWEEN  START_DATE AND END_DATE ) and C.SESSIONNO=" + session + " and SCHEMENO=" + scheme + " and SEMESTERNO=" + semester + " and SECTIONNO=" + section + " AND  TTS.SLOTTYPE = " + slottype + " AND T.COLLEGE_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["TimeTable_College_id"].ToString()) + " AND T.ORGANIZATIONID=" + OrgID, "");
                        if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                        {
                            if (dsGetExisitingDates.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                outval = 6;
                                return JsonConvert.SerializeObject(outval.ToString());
                            }
                        }
                    }


                    if (Convert.ToDateTime(TempStartDate) <= Convert.ToDateTime(TempEndDate))
                    {

                        //objE.DTTimeTable = dt;

                        objE.StartDate = Convert.ToDateTime(TempStartDate);
                        objE.EndDate = Convert.ToDateTime(TempEndDate);
                        objE.DTTimeTable = dt.DefaultView.ToTable(true);
                        //to get only 4 columns in time table
                        objE.DTTimeTable.Columns.Remove("StartDate1");
                        objE.DTTimeTable.Columns.Remove("EndDate1");
                        objE.DTTimeTable.Columns.Remove("Revised_Remark");

                        objE.UserId = Convert.ToInt32(sessionuid);
                        objE.IPADDRESS = IpAddress;
                        objE.CollegeId = clgID;
                        objE.OrgId = OrgID;
                        // objE.EFFECTIVEDATE = DateTime.Now;


                        outval = objC.AddTimeTable(objE);

                        if (outval != null || outval != 0)
                        {
                            outval = 1;
                        }
                        else
                        {
                            outval = 0;
                        }
                    }
                    else
                    {
                        outval = 2;
                    }
                }
            }
            else
            {
                outval = 0;
            }
            return JsonConvert.SerializeObject(outval.ToString());
        }
        catch
        {
            throw;
        }
    }

    [WebMethod()]
    public static string DeleteFaculty(string Id)
    {
        AcdAttendanceModel objAtt = new AcdAttendanceModel();
        AcdAttendanceController objAttC = new AcdAttendanceController();
        objAtt.TTNO = Convert.ToInt32(Id);
        int outval = objAttC.DeleteTimeTableFaculty(objAtt);
        if (outval != 0)
        {
            outval = 1;
            return "1";
        }
        else
        {
            outval = 0;
            return "1";
        }
    }
    #endregion WebMethod
    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clgID = ViewState["college_id"].ToString();
        clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
        if (Session["usertype"].ToString() != "1")
        {
            //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN(" + Session["userdeptno"].ToString() + ")" + "", "DEPTNAME ASC");

        }
        else
        {
            // clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
            //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + clgID + "", "DEPTNAME ASC");
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + clgID + "", "DEPTNAME ASC");

        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string dept = string.Empty;
            ddlSem.Items.Clear();
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divcourses.Visible = false;
            lvSlots.DataSource = null;
            lvSlots.DataBind();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlCourseType.Items.Clear();
            ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            if (ddlDepartment.SelectedValue != "0")
            {
                FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"]));
                ddlSem.Focus();
                //foreach (ListItem items in ddlDepartment.Items)
                //{
                //    if (items.Selected == true)
                //    {
                //        dept += items.Value + ',';
                //    }
                //}
                if (!dept.ToString().Equals(string.Empty) || !dept.ToString().Equals(""))
                    dept = dept.Remove(dept.Length - 1);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        // if (Session["usertype"].ToString() != "1")
        // 
        //int Deptno = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        //if(ddlDepartment.SelectedIndex>0)
        //{
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID="+clgID, "D.DEGREENO");
        //}
        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");

    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divcourses.Visible = false;
            lvSlots.DataSource = null;
            lvSlots.DataBind();
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlCourseType.Items.Clear();
            ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
            lstbxCourse.Items.Clear();
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            if (ddlClgname.SelectedIndex > 0)
            {

                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    Session["TimeTable_College_id"] = ViewState["college_id"].ToString();
                    //clgID = ViewState["college_id"].ToString();
                    scheme = ViewState["schemeno"].ToString();
                    //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    ddlSession.Focus();
                }
            }


        }
        catch
        {
            throw;
        }
    }
    protected void ddlCourseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstbxCourse.Items.Clear();
        //ddlSlotType.SelectedIndex = -1;
        //ddlExistingDates.SelectedIndex = -1;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        //dListFaculty.DataSource = null;
        //dListFaculty.DataBind();
        //lvSlots.DataSource = null;
        //lvSlots.DataBind();
        if (ddlCourseType.SelectedIndex > 0)
        {

        }
    }
    //protected void lstbxCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlSlotType.SelectedIndex = -1;
    //    ddlExistingDates.SelectedIndex = -1;
    //    txtStartDate.Text = string.Empty;
    //    txtEndDate.Text = string.Empty;
    //    dListFaculty.DataSource = null;
    //    dListFaculty.DataBind();
    //    lvSlots.DataSource = null;
    //    lvSlots.DataBind();
    //    objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0", "SLOTTYPENO");

    //    ddlSlotType.Focus();
    //}

    [WebMethod]
    public static string Get_Dynamic_Controls(int Sessionno, int Collegeid, int Sectionno, int Slotno, int ctno, int dayno, string startdate, string enddate, int roomno)
    {
        string yourHTMLstring = ""; string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        Common objCommon = new Common();

        try
        {
            DataSet ds = new DataSet();
            SP_Name = "PKG_ACD_TIMETABLE_CHECK_CLASHES_SINGLE_SLOTWISE";
            SP_Parameters = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SECTIONNO,@P_SLOTNO,@P_CTNO,@P_DAYNO,@P_STARTDATE,@P_ENDDATE,@P_Roomid";
            Call_Values = "" + Sessionno + "," + Collegeid + "," + Sectionno + "," + Slotno + "," + ctno + "," + dayno + "," + startdate + "," + enddate + "," + roomno + "";
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    yourHTMLstring += ds.Tables[0].Rows[i]["UA_FULLNAME"].ToString() + ',';
                }
            }
            else
            {
                yourHTMLstring = "0";
            }
        }
        catch (Exception ex)
        {

        }
        return yourHTMLstring.Replace(System.Environment.NewLine, string.Empty);
    }

    protected void dListFaculty_DataBound(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_ACADEMIC_ROOMMASTER RM INNER JOIN ACD_FLOOR_MASTER FM ON(RM.FLOORNO=FM.FLOORNO)", "ROOMNO", "CONCAT(ROOMNAME,'-',FLOORNAME) AS ROOMNAME", "ISNULL(RM.ACTIVESTATUS,0)=1", "ROOMNO");
        Session["RoomData"] = ds;
        foreach (ListViewDataItem lvItem in dListFaculty.Items)
        {
            DropDownList ddlRoom = lvItem.FindControl("ddlRoom") as DropDownList;

            ddlRoom.Items.Clear();
            ddlRoom.Items.Add("Select Room");
            ddlRoom.SelectedItem.Value = "0";
            ddlRoom.DataSource = ds.Tables[0];
            ddlRoom.DataValueField = "ROOMNO";
            ddlRoom.DataTextField = "ROOMNAME";
            ddlRoom.DataBind();
        }
    }
}
