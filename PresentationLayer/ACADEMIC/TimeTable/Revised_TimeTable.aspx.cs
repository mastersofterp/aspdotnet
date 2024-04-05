//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TIME TABLE CREATION                                                 
// CREATION DATE : 24-NOV-2019
// CREATED BY    : NEHA BARANWAL                        
// MODIFIED BY   : Rishabh Bajirao
// MODIFIED DESC : 29032023
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

public partial class ACADEMIC_TIMETABLE_Revised_TimeTable : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttE = new AcdAttendanceModel();

    #region Declaration
    string Message = string.Empty;
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    static string _nitprm_constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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

    static string session = "", scheme = "", semester = "", section = "", slotype = "", ExisitingDates = "", Startdate = "", Enddate = "", clgID = "", OrgId = "", Degreeno = "",
        tempid = "";

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

                this.FillSlots();
                this.PopulateDropDownList();
                sessionuid = Session["userno"].ToString();
                IpAddress = Request.ServerVariables["REMOTE_ADDR"]; //Session["ipAddress"].ToString();
                Session["transferTbl"] = null;
            }
            txtRemark.Attributes.Add("maxlength", txtRemark.MaxLength.ToString());
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 30/01/2022
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 30/01/2022
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


    //to load all dropdown list
    private void PopulateDropDownList()
    {
        try
        {
            ddlSession.SelectedIndex = 0;
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            if (Session["usertype"].ToString() != "1")
            {
                //string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString() +"AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "C.COLLEGE_ID");
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND  COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN (" + Session["userdeptno"].ToString() + " )", "DEPTNAME ASC");
            }
            else
            {
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
                //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME ASC");
            }
        }
        catch
        {
            throw;
        }
    }


    //to fill all slots
    protected void FillSlots()
    {
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet ds = objAttC.GetSlots(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["degreeno_revisedTT"]), Convert.ToInt16(ddlSlotType.SelectedValue), Convert.ToInt32(Session["college_id_revisedTT"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSlots.DataSource = ds;
                lvSlots.DataBind();
                pnlSlots.Visible = true;
            }
            //if (ds.Tables[1].Rows.Count > 0) If room selection is mandatory then uncomment this patch and add [MANDATE_ROOM] in ACD_MODULE_CONFIG
            //{
            //    hdnRoomMandate.Value = ds.Tables[1].Rows[0]["MANDATE_ROOM"].ToString();
            //}
        }
    }

    //protected void BindRoom(DropDownList ddlroom)
    //{
    //    DataSet dt1 = (DataSet)Session["RoomData"];

    //    foreach (ListViewDataItem lvItem in lvSlots.Items)
    //    {
    //        ddlRoom.Items.Clear();
    //        ddlRoom.Items.Add("Select Room");
    //        ddlRoom.SelectedItem.Value = "0";
    //        ddlRoom.DataSource = dt1.Tables[0];
    //        ddlRoom.DataValueField = "ROOMNO";
    //        ddlRoom.DataTextField = "ROOMNAME";
    //        ddlRoom.DataBind();
    //    }

    //}

    //to show faculties 
    public void ShowFaculty(DateTime startdate, DateTime enddate, DateTime date)
    {
        Boolean lockCT;
        lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + " AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0") == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0"));
        int college_id = Convert.ToInt32(Session["college_id_revisedTT"]);
        int Org_Id = Convert.ToInt32(Session["OrgId"]);
        DataSet Dsfac = objAttC.GetAllFacultyWiseCourses(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(Session["schemeno_revisedTT"]), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(ddlSection.SelectedValue), Convert.ToInt16(ddlSlotType.SelectedValue), startdate, enddate, date, college_id, Org_Id);
        DataTable dtrevised = Dsfac.Tables[0];
        Session["Revised_Faculty"] = dtrevised; //Storing Faculty in viewstate
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
                        //ddlRoom.SelectedValue = roomid == null ? "0" : roomid
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
                    //btnLock.Visible = false;
                }
            }
            //ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Src", "DisableOldList();", true);
        }
    }

    #endregion "Page Event"

    #region "Selected Index Changed"

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                pnlSlots.Visible = false;
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                divCourses.Visible = false;
                session = "";
                session = ddlSession.SelectedValue;
                //ddlRevisedNo.SelectedIndex = 0;  
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSlotType.Items.Clear();
                ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlDepartment.SelectedIndex = -1;
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                Session["Revised_Session"] = Convert.ToInt32(ddlSession.SelectedValue);
                ddlDepartment.Focus();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divCourses.Visible = false;
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            //ddlRevisedNo.SelectedIndex = 0;
            //if (ddlDegree.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " +Convert.ToInt32(Session["degreeno_revisedTT"]) + " AND COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue, "B.BRANCHNO");
            //    ddlBranch.Focus();
            //}
            // degree = ddlDegree.SelectedValue;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divCourses.Visible = false;
            ddlSlotType.SelectedIndex = 0;
            ddlExistingDates.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            if (ddlBranch.SelectedIndex > 0)
            {
            }
            //if (ddlBranch.SelectedValue == "99")    //FIRST YEAR
            //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "REPLACE(SCHEMENAME,'CIVIL ENGINEERING','FIRST YEAR') SCHEMENAME", "DEGREENO = 1 AND BRANCHNO = 1 AND SCHEMENO IN (1,24)", "SCHEMENO DESC");
            //else
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + Convert.ToInt32(Session["degreeno_revisedTT"]) + " AND BRANCHNO =" + Convert.ToInt32(Session["branchno_revisedTT"]), "SCHEMENO DESC");

            ddlScheme.Focus();
            //branch = ddlBranch.SelectedValue;
        }
        catch
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
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divCourses.Visible = false;
            //ddlRevisedNo.SelectedIndex = 0;
            scheme = "";
            scheme = Session["schemeno_revisedTT"].ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divCourses.Visible = false;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            Session["Revised_Semesterno"] = Convert.ToInt32(ddlSem.SelectedValue);
            //ddlRevisedNo.SelectedIndex = 0;
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0 AND ISNULL(SR.CANCEL,0)=0", "SC.SECTIONNAME");
            }
            ddlSection.Focus();

            semester = "";
            semester = ddlSem.SelectedValue;

            AttendanceConfigDate();
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
            pnlSlots.Visible = false;
            dListFaculty.DataSource = null;
            dListFaculty.DataBind();
            divCourses.Visible = false;
            ddlSlotType.Focus();
            ddlSlotType.Items.Clear();
            ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlExistingDates.Items.Clear();
            ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
            //ddlRevisedNo.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";

            section = "";
            section = ddlSection.SelectedValue;
            OrgId = Session["OrgId"].ToString();
            Session["Revised_Sectionno"] = Convert.ToInt32(ddlSection.SelectedValue);
            if (ddlSection.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SLOTTYPENO");
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
                LoadExisitingDates();
                ddlExistingDates.SelectedIndex = 0;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                ddlExistingDates.Focus();
                pnlSlots.Visible = false;
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                divCourses.Visible = false;
                //ddlRevisedNo.SelectedIndex = 0;
                slotype = "";
                slotype = ddlSlotType.SelectedValue;
                OrgId = Session["OrgId"].ToString();
                Session["Revised_slottype"] = Convert.ToInt32(ddlSlotType.SelectedValue);
                AttendanceConfigDate();
            }
            else
            {
                pnlSlots.Visible = false;
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                divCourses.Visible = false;
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                txtStartDate.Text = "";
                txtEndDate.Text = "";
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlExistingDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AcdAttendanceController objC = new AcdAttendanceController();
            if (ddlSlotType.SelectedIndex > 0)
            {
                if (ddlExistingDates.SelectedIndex > 0)
                {
                    string[] ssizes = null;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "test1();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {test1();});", true);
                    string myStr = ddlExistingDates.SelectedItem.ToString();
                    ssizes = myStr.Split(' ');
                    string startdate = ssizes[0].ToString();
                    string enddate = ssizes[2].ToString();
                    txtStartDate.Text = startdate;
                    txtEndDate.Text = enddate;
                    Enddate = enddate;
                    Startdate = startdate;

                    Session["Revised_startdate"] = startdate;
                    Session["Revised_enddate"] = enddate;
                    //txtDate.Text = "";
                    //date = "";
                    ExisitingDates = "";
                    ExisitingDates = ddlExistingDates.SelectedItem.ToString();

                    pnlSlots.Visible = false;
                    if (ddlSem.SelectedIndex > 0)
                    {
                        Boolean lockCT;
                        lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ORGANIZATIONID=" + Session["OrgId"].ToString()) == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ORGANIZATIONID=" + Session["OrgId"].ToString()));

                        Session["lockCT"] = lockCT;
                        //DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN User_Acc A ON (A.UA_NO = T.UA_NO OR A.UA_NO = T.ADTEACHER) INNER JOIN ACD_COURSE AC ON(T.COURSENO = AC.COURSENO) INNER JOIN ACD_SUBJECTTYPE ST ON(ST.SUBID = AC.SUBID)", "UPPER(CONCAT(COURSE_NAME,' - ',A.UA_FULLNAME)) AS UA_FULLNAME,T.CCODE+' - '+UPPER(A.UA_FULLNAME)+(CASE WHEN ISNULL(T.UA_NO,0)<>0 THEN '<SPAN STYLE=" + "COLOR:#f20943;FONT-WEIGHT:BOLD" + "> $</SPAN>' WHEN ISNULL(T.UA_NO,0)=0 AND ISNULL(T.ADTEACHER,0)<>0 THEN ' <SPAN STYLE=" + "COLOR:#f8036b;FONT-WEIGHT:BOLD" + "> #</SPAN>' END)+' [SEC: '+ISNULL(DBO.FN_DESC('SECTIONNAME',T.SECTIONNO)COLLATE DATABASE_DEFAULT,'-')+']'+(CASE WHEN (ST.TH_PR=2 AND ST.SEC_BATCH=2) THEN +'[BAT: '+ISNULL(DBO.FN_DESC('BATCH',T.BATCHNO)COLLATE DATABASE_DEFAULT,'-')+']' ELSE '' END) FACULTY", "A.UA_NO,CT_NO,T.COURSENO,T.SUBID", "T.SESSIONNO = " + ddlSession.SelectedValue + " AND T.SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND T.SEMESTERNO=" + ddlSem.SelectedValue + " AND T.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(T.CANCEL,0)=0" + " AND T.COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND T.ORGANIZATIONID=" + Session["OrgId"], "A.UA_FULLNAME");
                        DataSet ds = objAttC.GetFacultyForTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["schemeno_revisedTT"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(Session["college_id_revisedTT"]));
                        if (ds.Tables[0].Rows.Count > 0 && ds != null)
                        {
                            if (lockCT == false)
                            {
                                //btnLock.Visible = true;
                                dListFaculty.DataSource = ds;
                                dListFaculty.DataBind();
                                divCourses.Visible = true;
                            }
                            else
                            {
                                //btnLock.Visible = false;
                                objCommon.DisplayMessage(this, "Time Table already locked! you cannot modified it.", this);
                                dListFaculty.DataSource = null;
                                dListFaculty.DataBind();
                                divCourses.Visible = false;
                                divCourses.Visible = false;
                            }
                            FillSlots();

                            //string revisedno = objCommon.LookUp("ACD_TIME_TABLE_CONFIG TT INNER JOIN ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE ", "DISTINCT max(isnull(REVISED_NO,0))", "ISNULL(TT.CANCEL,0)=0 AND TT.TIME_TABLE_DATE BETWEEN convert(Date,'" + (startdate) + "',103) and convert(Date,'" + (enddate) + "',103) AND REVISED_NO IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND SLOTTYPE=" + ddlSlotType.SelectedValue);
                            //(string.IsNullOrEmpty(txtDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtDate.Text))

                            DateTime date1 = DateTime.MinValue;
                            ShowFaculty(Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), date1);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Course Teacher allotment Not Found for this selection !", this);
                            dListFaculty.DataSource = null;
                            dListFaculty.DataBind();
                            divCourses.Visible = false;
                        }
                    }
                    AttendanceConfigDate();
                }
                else
                {
                    //txtDate.Text = "";
                    // date = "";
                    ExisitingDates = "";
                    dListFaculty.DataSource = null;
                    dListFaculty.DataBind();
                    divCourses.Visible = false;
                    divCourses.Visible = false;
                    pnlSlots.Visible = false;
                    ddlSlotType.Focus();
                    //ClearDays();
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select SlotType", this);
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                divCourses.Visible = false;
                pnlSlots.Visible = false;
                ddlSlotType.Focus();
                ddlExistingDates.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }

    //to load existing dates
    public void LoadExisitingDates()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0)
            {
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;
                // MODIFIED  BY SAFAL GUPTA ON 28042021
                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TT INNER JOIN  ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES ", "START_DATE,END_DATE,MONTH(START_DATE) as STARTDATEMONTH", "ISNULL(TT.CANCEL,0)=0 AND ISNULL(ISEXTERNAL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + "and SLOTTYPE=" + ddlSlotType.SelectedValue + " AND TT.COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND TT.ORGANIZATIONID=" + Session["OrgId"], "MONTH(START_DATE) ");
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

    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToDateTime(txtStartDate.Text) < DateTime.Now.Date)
            //{
            //    objCommon.DisplayMessage(this.Page, "Can not select past date", this.Page);
            //    txtStartDate.Text = "";
            //    txtStartDate.Focus();
            //    return;
            //}
            if (ddlExistingDates.SelectedIndex > 0)
            {
                Startdate = "";
                Startdate = txtStartDate.Text;
                LoadSlotsandALLFaculties();
                string myStr = ddlExistingDates.SelectedItem.ToString();
                string[] ssizes = myStr.Split(' ');
                string startdate = ssizes[0].ToString();
                string enddate = ssizes[2].ToString();
            }
            else
            {
                Startdate = "";
                Startdate = txtStartDate.Text;
                LoadSlotsandALLFaculties();
                Startdate = txtStartDate.Text;
            }

            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND A.ORGANIZATIONID=" + Session["OrgId"]));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND A.ORGANIZATIONID=" + Session["OrgId"]));

            if (Convert.ToDateTime(txtStartDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                txtStartDate.Text = string.Empty;
                txtStartDate.Focus();
                return;
            }
            else if (Convert.ToDateTime(txtStartDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                txtStartDate.Text = string.Empty;
                txtStartDate.Focus();
                return;
            }


            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                Session["Revised_startdate"] = txtStartDate.Text;
                if (txtEndDate.Text != "")
                {
                    objCommon.DisplayMessage(this, "Please Select End Date", this);
                    txtEndDate.Text = "";
                    txtEndDate.Focus();
                    return;
                }
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this, "Please Select Dates Properly", this);
                    txtStartDate.Text = string.Empty;
                    txtEndDate.Text = string.Empty;
                    txtStartDate.Focus();
                    return;
                }
                DateTime date1 = DateTime.MinValue;
                ShowFaculty(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), date1);
            }
            Session["Revised_startdate"] = txtStartDate.Text;
        }
        catch
        {
            // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "test1();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {test1();});", true);
            if (!string.IsNullOrEmpty(txtStartDate.Text))
                Startdate = txtStartDate.Text;
        }
    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlExistingDates.SelectedIndex > 0)
            {
                Enddate = "";
                Enddate = txtEndDate.Text;
                LoadSlotsandALLFaculties();
            }
            else
            {
                Enddate = "";
                Enddate = txtEndDate.Text;
                LoadSlotsandALLFaculties();
                Enddate = txtEndDate.Text;
            }


            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND A.ORGANIZATIONID=" + Session["OrgId"]));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND A.ORGANIZATIONID=" + Session["OrgId"]));

            if (Convert.ToDateTime(txtEndDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                txtEndDate.Text = string.Empty;
                txtEndDate.Focus();
                return;
            }
            else if (Convert.ToDateTime(txtEndDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                txtEndDate.Text = string.Empty;
                txtEndDate.Focus();
                return;
            }


            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    objCommon.DisplayMessage(this, "Please Select Dates Properly", this);
                    //txtStartDate.Text = string.Empty;
                    txtEndDate.Text = string.Empty;
                    txtEndDate.Focus();
                    return;
                }

                DateTime date1 = DateTime.MinValue;
                ShowFaculty(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), date1);
            }
            Session["Revised_startdate"] = txtStartDate.Text;
            Session["Revised_enddate"] = txtEndDate.Text;
        }
        catch
        {
            if (!string.IsNullOrEmpty(txtEndDate.Text))
                Enddate = txtEndDate.Text;
        }
    }

    public void LoadSlotsandALLFaculties()
    {

        pnlSlots.Visible = false;
        if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0)
        {
            Boolean lockCT;
            lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0") == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0"));

            Session["lockCT"] = lockCT;
            DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN User_Acc A ON (A.UA_NO = T.UA_NO OR A.UA_NO = T.ADTEACHER) INNER JOIN ACD_COURSE AC ON(T.COURSENO = AC.COURSENO)", "UPPER(CONCAT(COURSE_NAME,' - ',A.UA_FULLNAME)) AS UA_FULLNAME,T.CCODE+' - '+UPPER(A.UA_FULLNAME)+(CASE WHEN ISNULL(T.UA_NO,0)<>0 THEN '<SPAN STYLE=" + "COLOR:#f20943;FONT-WEIGHT:BOLD" + "> $</SPAN>' WHEN ISNULL(T.UA_NO,0)=0 AND ISNULL(T.ADTEACHER,0)<>0 THEN ' <SPAN STYLE=" + "COLOR:#f8036b;FONT-WEIGHT:BOLD" + "> #</SPAN>' END)+' [SEC: '+ISNULL(DBO.FN_DESC('SECTIONNAME',T.SECTIONNO)COLLATE DATABASE_DEFAULT,'-')+']'+(CASE WHEN ((T.SUBID=2) OR (T.ORGANIZATIONID=2 AND T.SUBID IN (4,12,15)) OR (T.SUBID=1 AND  IS_TUTORIAL=1)) THEN +'[BAT: '+ISNULL(DBO.FN_DESC('BATCH',T.BATCHNO)COLLATE DATABASE_DEFAULT,'-')+']' ELSE '' END) FACULTY", "A.UA_NO,CT_NO,T.COURSENO,T.SUBID", "T.SESSIONNO = " + ddlSession.SelectedValue + " AND T.SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND T.SEMESTERNO=" + ddlSem.SelectedValue + " AND T.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(T.CANCEL,0)=0" + " AND T.COLLEGE_ID=" + Convert.ToInt32(Session["college_id_revisedTT"]) + " AND T.ORGANIZATIONID=" + Session["OrgId"], "A.UA_FULLNAME");

            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                if (lockCT == false)
                {
                    //btnLock.Visible = true;
                    dListFaculty.DataSource = ds;
                    dListFaculty.DataBind();
                    divCourses.Visible = true;
                }
                else
                {
                    //btnLock.Visible = false;
                    objCommon.DisplayMessage(this, "Time Table already locked! you cannot modified it.", this);
                    dListFaculty.DataSource = null;
                    dListFaculty.DataBind();
                    divCourses.Visible = false;
                }
                FillSlots();
            }
            else
            {
                objCommon.DisplayMessage(this, "Course Teacher allotment not Found for this selection !", this);
                dListFaculty.DataSource = null;
                dListFaculty.DataBind();
                divCourses.Visible = false;
            }

            //if (!string.IsNullOrEmpty(txtDate.Text))
            //    date = txtDate.Text;
        }
    }


    #endregion "Selected Index Changed"

    #region General
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlSchoolInstitute.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSlotType.SelectedIndex = 0;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtRemark.Text = "";
        ddlExistingDates.SelectedIndex = 0;
        lvSlots.DataSource = null;
        lvSlots.DataBind();
        dListFaculty.DataSource = null;
        dListFaculty.DataBind();
        divCourses.Visible = false;
        divDateDetails.Visible = false;

        session = "";
        scheme = "";
        semester = "";
        section = "";
        slotype = "";
        Startdate = "";
        Enddate = "";
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        objAttE.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
        objAttE.DEGREENO = Convert.ToInt32(Session["degreeno_revisedTT"]);
        objAttE.SCHEMENO = Convert.ToInt32(Session["schemeno_revisedTT"]);
        objAttE.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
        objAttE.SECTIONNO = Convert.ToInt32(ddlSection.SelectedValue);
        objAttE.LOCK_DATE = Convert.ToDateTime(DateTime.Now);
        objAttE.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        ret = objAttC.AddTTLock(objAttE, ref Message);

        if (ret <= 0)
        {
            if (Message.ToString().Trim().Trim() == "")
            {
                objCommon.DisplayMessage(this.updTimeTable, "No Modified", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updTimeTable, "Exception Occured", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updTimeTable, "Time Table locked Successfully!", this.Page);
            //btnLock.Visible = false;
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
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlExistingDates.Items.Clear();
        ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsemester.DataSource = ds;
            ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlsemester.DataBind();
            ddlsemester.SelectedIndex = 0;
            ddlsemester.Focus();
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
                ATT_StartDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND  A.COLLEGE_ID=" + Session["college_id_revisedTT"] + " AND A.ORGANIZATIONID=" + Session["OrgId"].ToString()));
                ATT_EndDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(Session["schemeno_revisedTT"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND  A.COLLEGE_ID=" + Session["college_id_revisedTT"] + "AND A.ORGANIZATIONID=" + Session["OrgId"].ToString()));
                divDateDetails.Visible = true;
                lblTitleDate.Text = "Selected Session Start Date : " + ATT_StartDate.ToShortDateString() + " End Date : " + ATT_EndDate.ToShortDateString();

            }
        }
        catch { lblTitleDate.Text = "Selected Session Start Date : - End Date : -"; }
    }

    //public int ValidateTeachingPlan()
    //{
    //    int sessionno=Convert.ToInt32(ddlSession.SelectedValue);
    //    int courseno = Convert.ToInt32(ddlSection.SelectedValue);
    //      //int count = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(*)", "SESSIONNO =" + sessionno + " AND UA_NO =" + uano + " AND COURSENO =" + courseno + " AND PERIOD =" + lectureno + " AND (BATCHNO =" + batchno + " OR " + batchno + " = 0) AND SUBID = " + subid + " AND (SECTIONNO = " + sectionno + " OR SECTIONNO IS NULL ) AND SCHEMENO =" + schemeno + " AND CONVERT(DATETIME,ATT_DATE,103)=CONVERT(DATETIME,'" + date.ToShortDateString() + "',103)"));
    //}

    #region WebMethod
    //web method to save revised time tablec for paticular date
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveTimetableAjax(List<IITMS.UAIMS.AcdAttendanceModel.AttendanceJsonModel> AllotmentData)
    {
        try
        {
            Common objCommon1 = new Common();

            //string SP_Name1 = "PKG_ACD_INSERT_PARAMETERS";
            //string SP_Parameters1 = "@P_DEGREENO,@P_BRANCHNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_MAPPINGID,@P_OUTPUT";
            //string Call_Values1 = "" + 0 + "," + 0 + "," +
            //0 + "," + 0 + "," + tempid + ",0";

            //string que_out1 = objCommon1.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true);

            //DataSet dsGetCurrentvalue = objCommon1.GetCollegeSchemeMappingDetails(Convert.ToInt32(tempid));

            Common objCommon = new Common();
            int outval = 0;

            IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel objE = new IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel();
            AcdAttendanceController objC = new AcdAttendanceController();
            string json1 = JsonConvert.SerializeObject(AllotmentData);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json1);
            if (dt == null)
            {
                outval = 9;
                return JsonConvert.SerializeObject(outval.ToString());
            }
            string TempStartDate = dt.Rows[0]["StartDate1"].ToString();
            string TempEndDate = dt.Rows[0]["EndDate1"].ToString();
            objE.Remark = dt.Rows[0]["Revised_Remark"].ToString();
            DataSet ds1 = objC.Get_Revised_Time_Table_Validation(session, clgID, Degreeno, scheme, semester, section, OrgId, TempStartDate, TempEndDate);
            if (!string.IsNullOrEmpty(sessionuid))
            {
                if (string.IsNullOrEmpty(session) && string.IsNullOrEmpty(scheme) && string.IsNullOrEmpty(semester) && string.IsNullOrEmpty(section) && string.IsNullOrEmpty(slotype) && string.IsNullOrEmpty(clgID))
                {
                    outval = 0;
                }
                //if (string.IsNullOrEmpty(TempStartDate))
                //{
                //    outval = 3;
                //}
                //else if (string.IsNullOrEmpty(Enddate))
                //{
                //    outval = 4;
                //}
                else
                {
                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[2].Rows.Count > 0)
                        {
                            DateTime SDate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["START_DATE"]);
                            DateTime EDate = Convert.ToDateTime(ds1.Tables[2].Rows[0]["END_DATE"]);

                            //DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND COLLEGE_ID=" + Convert.ToInt32(clgID)+" AND A.ORGANIZATIONID="+OrgId));
                            //DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(session) + " AND SCHEMENO=" + Convert.ToInt32(scheme) + " AND A.SEMESTERNO=" + Convert.ToInt32(semester) + " AND COLLEGE_ID=" + Convert.ToInt32(clgID) + " AND A.ORGANIZATIONID=" + OrgId));

                            if (!string.IsNullOrEmpty(TempStartDate))
                            {
                                //to check end date is under session dates or not
                                if (Convert.ToDateTime(TempStartDate) < SDate)
                                {
                                    outval = 5;
                                    return JsonConvert.SerializeObject(outval.ToString());
                                }
                                else if (Convert.ToDateTime(TempStartDate) > EDate)
                                {
                                    outval = 6;
                                    return JsonConvert.SerializeObject(outval.ToString());
                                }
                            }


                            if (!string.IsNullOrEmpty(TempEndDate))
                            {
                                //to check end date is under session dates or not
                                if (Convert.ToDateTime(TempEndDate) < SDate)
                                {
                                    outval = 5;
                                    return JsonConvert.SerializeObject(outval.ToString());
                                }
                                else if (Convert.ToDateTime(TempEndDate) > EDate)
                                {
                                    outval = 6;
                                    return JsonConvert.SerializeObject(outval.ToString());
                                }

                            }
                        }
                    }
                    if (Convert.ToDateTime(TempStartDate) <= Convert.ToDateTime(TempEndDate))
                    {
                        #region "Commented"
                        //string selecteddate = Startdate.ToString();
                        //string[] ssizes = selecteddate.Split('/');
                        //int date1 = Convert.ToInt32(ssizes[0].ToString());
                        //int month = Convert.ToInt32(ssizes[1].ToString());
                        //int year = Convert.ToInt32(ssizes[2].ToString());

                        //DateTime dateValue = new DateTime(year, month, date1);
                        //int dayno = ((int)dateValue.DayOfWeek);

                        //if (dayno == 0)
                        //{
                        //    dayno = 7;
                        //}
                        //for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        //{
                        //    DataRow dr = dt.Rows[i];
                        //    if (dr["DayNo"].ToString() != dayno.ToString())
                        //        dr.Delete();
                        //}
                        //dt.AcceptChanges();
                        //if (ExisitingDates != "" && ExisitingDates != null)
                        //{
                        //    string myStr = ExisitingDates;
                        //    string[] ssizes1 = myStr.Split(' ');
                        //    string startdate1 = ssizes1[0].ToString();
                        //    string enddate1 = ssizes1[2].ToString();

                        //    objE.StartDate = Convert.ToDateTime(startdate1);
                        //    objE.EndDate = Convert.ToDateTime(enddate1);
                        //}
                        //else
                        //{
                        //    Common objCommon = new Common();
                        //    DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG T INNER JOIN ACD_COURSE_TEACHER C ON C.CT_NO=T.CTNO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=T.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "START_DATE", "END_DATE", "ISNULL(T.CANCEL,0)=0 AND (CONVERT(DATE,'" + Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "',103) BETWEEN START_DATE AND END_DATE ) and C.SESSIONNO=" + session + " and SCHEMENO=" + scheme + " and SEMESTERNO=" + semester + " and SECTIONNO=" + section + " AND  TTS.SLOTTYPE = " + slotype, "");
                        //    if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                        //    {
                        //        objE.StartDate = Convert.ToDateTime(dsGetExisitingDates.Tables[0].Rows[0]["START_DATE"].ToString());
                        //        objE.EndDate = Convert.ToDateTime(dsGetExisitingDates.Tables[0].Rows[0]["END_DATE"].ToString());
                        //    }
                        //    else
                        //    {
                        //        objE.StartDate = Convert.ToDateTime(date);
                        //        objE.EndDate = Convert.ToDateTime(date);
                        //    }
                        //}
                        #endregion
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds1.Tables[0].Rows[0]["IS_TEACHINGPLAN"].ToString()) > 0)
                            {
                                outval = 7;
                            }

                            if (ds1.Tables[1].Rows.Count > 0 && Convert.ToInt32(ds1.Tables[1].Rows[0]["IS_ATTENDANCE"].ToString()) > 0)
                            {
                                outval = 8;
                            }
                        }

                        objE.UserId = Convert.ToInt32(sessionuid);
                        objE.IPADDRESS = IpAddress;
                        objE.StartDate = Convert.ToDateTime(TempStartDate);
                        objE.EndDate = Convert.ToDateTime(TempEndDate);

                        objE.DTTimeTable = dt.DefaultView.ToTable(true);
                        objE.DTTimeTable.Columns.Remove("StartDate1");
                        objE.DTTimeTable.Columns.Remove("EndDate1");
                        objE.DTTimeTable.Columns.Remove("Revised_Remark");
                        objE.CollegeId = clgID;
                        objE.OrgId = OrgId;
                        objE.Degreeno = Convert.ToInt32(Degreeno);
                        objE.Schemeno = Convert.ToInt32(scheme);
                        objE.Semesterno = Convert.ToInt32(semester);
                        objE.Sectionno = Convert.ToInt32(section);
                        objE.Slottype = Convert.ToInt32(slotype);
                        //objE.dtRevisedFac = (DataTable)HttpContext.Current.Session["Revised_Faculty"];
                        //objE.dtRevisedFac.Columns.Remove("FACNAME");
                        //objE.dtRevisedFac.Columns.Remove("FACULTYNAME");

                        if (outval != 7 && outval != 8)
                        {
                            outval = objC.InsertRevisedTimeTable(objE);

                            if (outval != null || outval != 0)
                            {
                                outval = 1;
                            }
                            else
                            {
                                outval = 0;
                            }
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
        catch (Exception ex)
        {
            throw ex;
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
        //clgID = Session["college_id_revisedTT"].ToString();
        dListFaculty.DataSource = null;
        dListFaculty.DataBind();
        divCourses.Visible = false;
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlExistingDates.Items.Clear();
        ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        lvSlots.DataSource = null;
        lvSlots.DataBind();
        DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
        //Session["degreeno_revisedTT"]
        tempid = ddlSchoolInstitute.SelectedValue.ToString();
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            //string SP_Name1 = "PKG_ACD_INSERT_PARAMETERS";
            //string SP_Parameters1 = "@P_DEGREENO,@P_BRANCHNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_MAPPINGID,@P_OUTPUT";
            //string Call_Values1 = "" + Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString() + "," + Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString() + "," +
            //Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString() + "," + Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString() + "," + Convert.ToInt32(ddlSchoolInstitute.SelectedValue) + ",0";

            //string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true);


            Session["ddlSchoolInstitute_revisedTT"] = Convert.ToInt32(ddlSchoolInstitute.SelectedValue).ToString();
            Session["degreeno_revisedTT"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            Session["branchno_revisedTT"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            Session["college_id_revisedTT"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            Session["schemeno_revisedTT"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            scheme = Session["schemeno_revisedTT"].ToString();
            clgID = Session["college_id_revisedTT"].ToString();
            Degreeno = Session["degreeno_revisedTT"].ToString();
        }
        ddlSession.Focus();
        //clgID = Session["college_id_revisedTT"].ToString();
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID IN (" + Convert.ToInt32(Session["college_id_revisedTT"]) + " )", "SESSIONNO DESC");
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
        }
        else
        {
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + clgID + "", "DEPTNAME ASC");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if (Session["usertype"].ToString() != "1")
        // 
        int Deptno = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        if (ddlDepartment.SelectedIndex > 0)
        {
            this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["branchno_revisedTT"]));

            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID=" +Convert.ToInt32(Session["college_id_revisedTT"]), "D.DEGREENO");
        }
        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
    }

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


    /// <summary>
    /// Added By Rishabh on 01/11/2023 to get validation, if attendance already marked for that perticular period.
    /// </summary>
    /// <param name="ctno"></param>
    /// <param name="Slotno"></param>
    /// <param name="dayno"></param>
    /// <param name="startdate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    [WebMethod]
    public static string AttendanceCheck(int ctno, int Slotno, int dayno, string startdate, string enddate)
    {
        string yourHTMLstring = ""; string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        Common objCommon = new Common();

        try
        {
            DataSet ds = new DataSet();
            SP_Name = "PKG_ACD_CHECK_ATTENDANCE_FOR_REVISE";
            SP_Parameters = "@P_CTNO,@P_SLOTNO,@P_DAYNO,@P_STARTDATE,@P_ENDDATE";
            Call_Values = "" + ctno + "," + Slotno + "," + dayno + "," + startdate + "," + enddate + "";
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    yourHTMLstring += ds.Tables[0].Rows[i]["ATTENDANCE_MARKED_DATES"].ToString();
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
}
