//=================================================================================
//PROJECT NAME  : RFC-COMMON CODE
//MODULE NAME   : EXAMINATION 
//CREATION DATE : 27-03-2023
//CREATED BY    : SAGAR MANKAR
//MODIFIED BY   : 
//MODIFIED DESC :
//=================================================================================

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_TransferStudCourses : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarkEntryC = new MarksEntryController();
    StudentRegistration objSReg = new StudentRegistration();
    ExamController objExamController = new ExamController();

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
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                btnSubmit.Enabled = false;
                btnSubmit1.Enabled = false;

                ddlEquivalence.Focus();
                ddlEquivalence1.Focus();

                objCommon.FillDropDownList(ddlEquivalence, "ACD_IDTYPE", "DISTINCT IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "IDTYPENO");
                objCommon.FillDropDownList(ddlEquivalence1, "ACD_IDTYPE", "DISTINCT IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "IDTYPENO");

                ViewState["Org_Id"] = Convert.ToInt32(Session["OrgId"]);
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
                Response.Redirect("~/notauthorized.aspx?page=TransferStudCourses.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TransferStudCourses.aspx");
        }
    }

    #region listview

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RANK", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));

        dr = dt.NewRow();
        dr["RANK"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;

        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvCourse.DataSource = dt;
        lvCourse.DataBind();

        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            ImageButton lnkRemove = dataitem.FindControl("lnkRemove") as ImageButton;
            DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
            // DropDownList ddlSession = dataitem.FindControl("ddlSession") as DropDownList;
            DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;
            //objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"].ToString()) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");
            // objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");

            if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
            {
                objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
            }
            else
            {
                //ddlEquivalence.SelectedItem.Text.StartsWith("E");

                objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
            }

            Session["ddlNewCourseCount"] = Convert.ToString((ddlNewCourse.Items.Count) - 1);
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            ViewState["UGPGOT"] = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "UGPGOT", "COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"].ToString()) + " AND ISNULL(ACTIVESTATUS,0)=1");

            //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO"); //ViewState["COLLEGE_ID"]
            //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ddlNewCourse.SelectedValue) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

            lnkRemove.Enabled = false;
            ddlNewCourse.Focus();

            //DropDownList ddlExamType = dataitem.FindControl("ddlExamType") as DropDownList;
            //objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");
        }
    }

    // FOR ADDING COURSE IN LIST VIEW
    protected void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRowToLV();
    }
    //

    private void AddNewRowToLV()
    {
        int rowIndex = 0;
        int k = 0;
        string coursenos = string.Empty;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 20)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values 
                    DropDownList box1 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewCourse");
                    TextBox box2 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldCCode");
                    TextBox box3 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldCourse");
                    TextBox box4 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldGrade");
                    DropDownList box5 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlExamType");
                    DropDownList box6 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewGrade");
                    //  DropDownList box6 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlSession");                    

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RANK"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;
                    //   dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;

                    ViewState["Course"] = box1.Text;

                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                int h = Convert.ToInt32(Session["ddlNewCourseCount"].ToString());
                if (dtCurrentTable.Rows.Count >= Convert.ToInt32(Session["ddlNewCourseCount"].ToString()))
                {
                    ButtonAdd.Enabled = false;
                }

                lvCourse.DataSource = dtCurrentTable;
                lvCourse.DataBind();

                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    coursenos = string.Empty;
                    k++;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList box1 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewCourse");

                            if (i != k - 1)
                            {
                                if (coursenos == string.Empty)
                                {
                                    coursenos += dt.Rows[i]["Column1"].ToString();
                                }
                                else
                                {
                                    coursenos = coursenos + "," + dt.Rows[i]["Column1"].ToString();
                                    coursenos = coursenos.TrimEnd(',');
                                }
                            }
                        }
                    }

                    DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
                    DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;
                    DropDownList ddlExamType = dataitem.FindControl("ddlExamType") as DropDownList;

                    if (coursenos == "" || coursenos == string.Empty)
                    {
                        if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                        }
                        else
                        {
                            //ddlEquivalence.SelectedItem.Text.StartsWith("E");

                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }
                    else
                    {
                        if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ") AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }

                    //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO");

                    //if (ddlNewCourse.SelectedIndex > 0)
                    //{

                    // objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");

                    if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                    {
                        //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["Course"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

                        string proc_name = "PKG_ACD_GRADE_FOR_SUBJECT_EQUIVALENCE";
                        string param = "@COLLEGE_ID,@COURSENO,@UGPGOT";
                        string call_values = "" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + "," + Convert.ToInt32(ViewState["NewCourse"].ToString()) + "," + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "";
                        DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlNewGrade.DataSource = ds;
                            ddlNewGrade.DataTextField = "GRADE";
                            ddlNewGrade.DataValueField = "GRADENO";
                            ddlNewGrade.DataBind();
                        }
                    }

                    //}
                }
            }
            else
            {
                objCommon.DisplayMessage(updprogupdGradePattern, "Maximum Row Limit Reached!", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks 
        SetPreviousData();
    }

    //store previous row data of lv
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //extract the TextBox values 
                    DropDownList box1 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewCourse");
                    TextBox box2 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldCCode");
                    TextBox box3 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldCourse");
                    TextBox box4 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldGrade");
                    DropDownList box55 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlExamType");
                    DropDownList box6 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewGrade");

                    box1.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();
                    box4.Text = dt.Rows[i]["Column4"].ToString();
                    box55.SelectedValue = dt.Rows[i]["Column5"].ToString();
                    box6.SelectedValue = dt.Rows[i]["Column6"].ToString();

                    rowIndex++;

                    //box2.Focus();
                }
            }
        }
    }

    //store current row data of lv
    private void StoreCurrentData()
    {
        int rowIndex = 0;
        int k = 0;
        string coursenos = string.Empty;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 20)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList box1 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewCourse");
                    TextBox box2 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldCCode");
                    TextBox box3 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldCourse");
                    TextBox box4 = (TextBox)lvCourse.Items[rowIndex].FindControl("txtOldGrade");
                    DropDownList box5 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlExamType");
                    DropDownList box6 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewGrade");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RANK"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;

                    ViewState["NewCourse"] = box1.Text;

                    rowIndex++;
                }

                ViewState["CurrentTable"] = dtCurrentTable;

                lvCourse.DataSource = dtCurrentTable;
                lvCourse.DataBind();


                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    ImageButton lnkRemove = dataitem.FindControl("lnkRemove") as ImageButton;
                    if (dtCurrentTable.Rows.Count == 1)
                    {
                        lnkRemove.Enabled = false;
                    }
                    else
                    {
                        lnkRemove.Enabled = true;
                    }

                    coursenos = string.Empty;
                    k++;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i != k - 1)
                            //if (i == k - 1)
                            {
                                if (coursenos == string.Empty)
                                {
                                    coursenos += dt.Rows[i]["Column1"].ToString();
                                }
                                else
                                {
                                    coursenos = coursenos + "," + dt.Rows[i]["Column1"].ToString();
                                    coursenos = coursenos.TrimEnd(',');
                                }
                            }
                        }
                    }

                    DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
                    DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;
                    DropDownList ddlExamType = dataitem.FindControl("ddlExamType") as DropDownList;

                    if (coursenos == "" || coursenos == string.Empty)
                    {
                        if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                        }
                        else
                        {
                            //ddlEquivalence.SelectedItem.Text.StartsWith("E");

                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }
                    else
                    {
                        if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            // objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ") AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }

                    //if (ddlNewCourse.SelectedIndex > 0)
                    //{
                    //  objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");
                    //}

                    if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                    {
                        //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["NewCourse"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

                        string proc_name = "PKG_ACD_GRADE_FOR_SUBJECT_EQUIVALENCE";
                        string param = "@COLLEGE_ID,@COURSENO,@UGPGOT";
                        string call_values = "" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + "," + Convert.ToInt32(ViewState["NewCourse"].ToString()) + "," + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "";
                        DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();

                        if (ds.Tables.Count > 0)
                        {
                            ddlNewGrade.DataSource = ds;
                            ddlNewGrade.DataTextField = "GRADE";
                            ddlNewGrade.DataValueField = "GRADENO";
                            ddlNewGrade.DataBind();
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updprogupdGradePattern, "Maximum Row Limit Reached!", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks 
        SetPreviousData();
    }

    //remove row from lv
    protected void lnkRemove_Click(object sender, ImageClickEventArgs e)
    {
        int rowIndex = 0;
        int k = 0;
        string coursenos = string.Empty;
        ImageButton lb = sender as ImageButton;
        int rowID = (Convert.ToInt32(lb.CommandArgument) - 1);
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                //if (gvRow.RowIndex < dt.Rows.Count )
                if (rowID <= dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            lvCourse.DataSource = dt;
            lvCourse.DataBind();

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                ImageButton lnkRemove = dataitem.FindControl("lnkRemove") as ImageButton;
                if (dt.Rows.Count == 1)
                {
                    lnkRemove.Enabled = false;
                }
                else
                {
                    lnkRemove.Enabled = true;
                }

                coursenos = string.Empty;
                k++;
                // DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList box1 = (DropDownList)lvCourse.Items[rowIndex].FindControl("ddlNewCourse");

                        if (i != k - 1)
                        {
                            if (coursenos == string.Empty)
                            {
                                coursenos += dt.Rows[i]["Column1"].ToString();
                            }
                            else
                            {
                                coursenos = coursenos + "," + dt.Rows[i]["Column1"].ToString();
                                coursenos = coursenos.TrimEnd(',');
                            }
                        }
                    }
                }

                DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
                DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;

                if (coursenos == "" || coursenos == string.Empty)
                {
                    if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
                    {
                        //objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");

                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
                    }
                }
                else
                {
                    if (ddlEquivalence.SelectedItem.Text.ToString() != "ELECTIVE")
                    {
                        //objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");

                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ") AND ISNULL(C.ELECT,0)=1", "COURSE");
                    }
                }

                //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO");

                if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                {
                    //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["NewCourse"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

                    string proc_name = "PKG_ACD_GRADE_FOR_SUBJECT_EQUIVALENCE";
                    string param = "@COLLEGE_ID,@COURSENO,@UGPGOT";
                    string call_values = "" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + "," + Convert.ToInt32(ViewState["NewCourse"].ToString()) + "," + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "";
                    DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();

                    if (ds.Tables.Count > 0)
                    {
                        ddlNewGrade.DataSource = ds;
                        ddlNewGrade.DataTextField = "GRADE";
                        ddlNewGrade.DataValueField = "GRADENO";
                        ddlNewGrade.DataBind();
                    }
                }
            }

            int h = Convert.ToInt32(Session["ddlNewCourseCount"].ToString());
            if (dt.Rows.Count < Convert.ToInt32(Session["ddlNewCourseCount"].ToString()))
            {
                ButtonAdd.Enabled = true;
            }
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    #endregion

    // FOR SHOW DATA IN LISTVIEW WITH DETAILS
    protected void btnShow_Click(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        divlv.Visible = false;

        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            lblMsg.Text = "Please Enter Proper Univ. Reg. No.";
            txtStudent.Focus();
            return;
        }

        lblMsg.Text = string.Empty;

        try
        {
            string searchText = txtStudent.Text.Trim();
            string errorMsg = string.Empty;

            //student details shown
            ShowStudents(searchText, errorMsg);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // FOR CLEAR THE CONTROL DATA
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    /// <summary>

    /// </summary>
    private void Clear()
    {
        ddlSemester.SelectedIndex = 0;
        ddlEquivalence.SelectedIndex = 0;
        txtStudent.Text = string.Empty;
        divlv.Visible = false;
        divdata.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnSubmit.Enabled = false;
        ddlEquivalence.Focus();
        btnReport.Enabled = false;
    }


    //show student details
    private void ShowStudents(string searchText, string errorMsg)
    {
        divMsg.InnerHtml = string.Empty;
        if (txtStudent.Text.Trim() == "" || txtStudent.Text.Trim() == string.Empty || txtStudent.Text == null)
        {
            lblMsg.Text = "Please Enter Univ. Reg. No. / Admission No.";
            txtStudent.Focus();
            return;
        }
        lblMsg.Text = string.Empty;

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";

            //idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtStudent.Text.Trim() + "' OR ENROLLNO='" + txtStudent.Text.Trim() + "') AND IDTYPE=3 AND ISNULL(ADMCAN,0)=" + 0));
            idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtStudent.Text.Trim() + "' OR ENROLLNO='" + txtStudent.Text.Trim() + "') AND IDTYPE='" + ViewState["IDTYPENO"] + "' AND ISNULL(ADMCAN,0)=" + 0));

            if (idno == string.Empty)
            {
                btnSubmit.Enabled = false;
                divdata.Visible = false;
                objCommon.DisplayMessage(updGradePattern, "No Student Found Having Univ. Reg. No. / Admission No. is " + txtStudent.Text, this.Page);
            }
            else
            {
                if (idno != string.Empty)
                {
                    ViewState["idno"] = idno.ToString();
                    DataTableReader dtr = objSRegist.GetStudentDetails_Crescent(Convert.ToInt32(idno));

                    if (dtr.Read())
                    {
                        divdata.Visible = true;
                        ddlSession.SelectedIndex = 0;
                        ddlSemester.SelectedIndex = 0;
                        ddlSession.Focus();
                        btnSubmit.Enabled = false;
                        btnReport.Enabled = true;
                        Session["idno"] = dtr["idno"].ToString();
                        lblName.Text = dtr["STUDNAME"].ToString();
                        lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString();
                        ViewState["REGNO"] = lblRegNo.Text;
                        Session["regno"] = lblRegNo.Text;
                        ViewState["ROLLNO"] = dtr["ROLLNO"].ToString();
                        lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                        lblEnrollno.Text = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString();
                        lblEnrollno.ToolTip = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString(); ;
                        lblScheme.Text = dtr["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dtr["BRANCHNO"].ToString();
                        ViewState["SCHEMENO"] = dtr["SCHEMENO"].ToString();
                        ViewState["SEMESTERNO"] = dtr["SEMESTERNO"].ToString();
                        ViewState["DEGREENO"] = dtr["DEGREENO"].ToString();
                        ViewState["SCHEMETYPE"] = dtr["SCHEMETYPE"].ToString();
                        ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                        ViewState["BRANCHNO"] = dtr["BRANCHNO"].ToString();
                        ViewState["ORGANIZATION_ID"] = dtr["ORGANIZATIONID"].ToString();

                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID='" + ViewState["COLLEGE_ID"] + "' AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                    }
                    else
                    {
                        btnSubmit.Enabled = false;
                        divdata.Visible = false;
                        btnReport.Enabled = false;
                    }
                    dtr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            //int exist = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CANCEL,0)=0") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CANCEL,0)=0"));

            string proc_name = "PKG_ACD_CHECK_COUNT_FOR_SUBJECT_EQUIVALENCE";
            string param = "@P_SEMESTERNO,@P_SESSIONNO,@P_IDNO,@P_TRANSFERED";
            string call_values = "" + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["idno"].ToString()) + "," + Convert.ToInt32(ddlEquivalence.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values); //DataGrid dg = new DataGrid();

            int exist = 0;
            if (ds.Tables.Count > 0)
            {
                exist = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            if (exist == 0)
            {
                SetInitialRow();
                divlv.Visible = true;
                btnSubmit.Enabled = true;
                ButtonAdd.Enabled = true;
            }
            else
            {
                divlv.Visible = false;
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage(this.updGradePattern, "Result data of this student already available for the selected semester!", this.Page);
            }
        }
        else
        {
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedIndex = 0;

            divlv.Visible = false;
            btnSubmit.Enabled = false;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlv.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnSubmit.Enabled = false;

        if (ddlSession.SelectedIndex > 0)
        {
            ddlSemester.Focus();
            // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO NOT IN(SELECT DISTINCT SEMESTERNO FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ") AND SEMESTERNO<=" + Convert.ToInt32(ViewState["SEMESTERNO"].ToString()), "SEMESTERNO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO<(SELECT DISTINCT MIN(SEMESTERNO) FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ")", "SEMESTERNO");

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            //ddlSemester.SelectedIndex = 0;
            ddlSemester.SelectedItem.Value = "0";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentRegist objSR = new StudentRegist();
        Exam objexame = new Exam();
        string SESSIONNOS = string.Empty;
        string EQUIGRADES = string.Empty;
        DateTime TRANSDATE = DateTime.Now;

        int ret = 0;
        int prev_status = 0;

        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
            //  DropDownList ddlSession = dataitem.FindControl("ddlSession") as DropDownList;
            DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;
            DropDownList ddlExamType = dataitem.FindControl("ddlExamType") as DropDownList;
            TextBox txtOldCCode = dataitem.FindControl("txtOldCCode") as TextBox;
            TextBox txtOldCourse = dataitem.FindControl("txtOldCourse") as TextBox;
            TextBox txtOldGrade = dataitem.FindControl("txtOldGrade") as TextBox;

            TextBox txtMinMarks = dataitem.FindControl("txtMinMarks") as TextBox;
            TextBox txtMaxMarks = dataitem.FindControl("txtMaxMarks") as TextBox;
            TextBox txtMarks = dataitem.FindControl("txtMarks") as TextBox;

            SESSIONNOS = ddlSession.SelectedValue;
            objSR.IDNO = Convert.ToInt32(ViewState["idno"].ToString());
            objSR.REGNO = ViewState["REGNO"].ToString();
            objSR.ROLLNO = ViewState["ROLLNO"].ToString();
            objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
            objSR.SubEquivalence = Convert.ToInt32(ddlEquivalence.SelectedValue);
            objSR.SCHEMENO = Convert.ToInt32(ViewState["SCHEMENO"].ToString());
            objSR.COURSENOS = objSR.COURSENOS + ddlNewCourse.SelectedValue + ",";
            objSR.SECTIONNOS = "";
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.GRADE = objSR.GRADE + ddlNewGrade.SelectedItem.Text + ",";
            objSR.ExamType = objSR.ExamType + ddlExamType.SelectedValue + ",";
            objSR.CCODES = objSR.CCODES + txtOldCCode.Text + ",";
            objSR.COURSENAMES = objSR.COURSENAMES + txtOldCourse.Text + ",";
            EQUIGRADES = EQUIGRADES + txtOldGrade.Text + ",";

            objSR.MIN_MARKS = "";
            objSR.MAX_MARKS = "";
            objSR.ACTUAL_MARKS = "";

            objSR.STUDENT_PATTERN = 1;
        }

        ret = objSReg.AddStudentResultData(SESSIONNOS, objSR);

        #region cmt
        //foreach (ListViewDataItem dataitem in lvCourse.Items)
        //{
        //    int operation = 1;
        //    DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
        //    DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;
        //    TextBox txtOldCCode = dataitem.FindControl("txtOldCCode") as TextBox;
        //    TextBox txtOldCourse = dataitem.FindControl("txtOldCourse") as TextBox;
        //    TextBox txtOldGrade = dataitem.FindControl("txtOldGrade") as TextBox;

        //    objexame.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //    objexame.SchemeNo = Convert.ToInt32(ViewState["SCHEMENO"].ToString());
        //    objexame.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        //    objexame.SectionNo = 0;
        //    objexame.Courseno = Convert.ToInt32(ddlNewCourse.SelectedValue);
        //    objexame.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
        //    objexame.Ua_No = Convert.ToInt32(Session["userno"].ToString());

        //    ret = objExamController.GradeAllotmentBySigleStudent(objexame, operation);
        //}    
        #endregion

        //ret = objMarkEntryC.ProcessResultAll(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue),
        //                                    ViewState["idno"].ToString(), Convert.ToInt32(prev_status), Convert.ToString(Session["ipAddress"].ToString()), Convert.ToInt32(1),
        //                                    Convert.ToInt32(0), Convert.ToInt32(Session["userno"].ToString()), 0, string.Empty, string.Empty, string.Empty, string.Empty, 0,
        //                                    string.Empty, string.Empty);

        if (!ret.Equals(-99))
        {
            ret = objMarkEntryC.ProcessResultAll(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue),
                                                ViewState["idno"].ToString(), Convert.ToInt32(prev_status), Convert.ToString(Session["ipAddress"].ToString()), Convert.ToInt32(ViewState["COLLEGE_ID"]),
                                                 Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["Org_Id"]));

            ret = objSReg.AddTransferedStudentRecord(SESSIONNOS, objSR, EQUIGRADES, TRANSDATE);

            //if (!ret.Equals(-99))
            //{
            objCommon.DisplayMessage(this.updGradePattern, "Details Saved Successfully!!", this.Page);
            Clear();
        }
        else
        {
            objCommon.DisplayMessage(this.updGradePattern, "Something went wrong.", this.Page);
            return;
        }
    }

    protected void ddlNewGrade_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
    }

    protected void txtOldGrade_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
    }

    protected void txtOldCourse_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
    }

    protected void txtOldCCode_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
        //TextBox box3 = (TextBox)lvCourse.Items[0].FindControl("txtOldCourse");
        //box3.Focus();
    }

    protected void ddlNewCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
    }

    private void ShowReportForGrade(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string procedure = "PKG_ACD_GET_TRANSFER_COURSE_EQUI_DETAILS";
            string parameter = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO,@P_SUB_EQUI,@P_PATTERN";
            string values = "" + Convert.ToInt32(Session["idno"].ToString()) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlEquivalence.SelectedValue) + "," + 1 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(procedure, parameter, values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "");

                if (reportno == 1)
                {
                    url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_CODE=" + college_id + ",@P_SUB_EQUI=" + Convert.ToInt32(ddlEquivalence.SelectedValue) + ",@P_PATTERN=" + 1 + "";
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updGradePattern, this.updGradePattern.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updGradePattern, "No Data Found for this selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReportForGrade("TransferStudentSubjectGradeEquiDetails", "rptTransferStudentSubjectEquiDetails.rpt", 1);
    }

    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
    }

    protected void ddlEquivalence_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEquivalence.SelectedIndex > 0)
        {
            divlv.Visible = false;
            divdata.Visible = false;

            txtStudent.Text = "";
            txtStudent.Focus();
            ViewState["IDTYPENO"] = ddlEquivalence.SelectedValue;
        }
    }

    //Added BY Suraj Y on 16-03-2024 with TKT No. 56019

    #region Working With Marks Pattern Student Tab

    protected void ddlEquivalence1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEquivalence1.SelectedIndex > 0)
        {
            divlvM.Visible = false;
            divdataM.Visible = false;

            txtstudent1.Text = "";
            txtstudent1.Focus();
            ViewState["IDTYPENO"] = ddlEquivalence1.SelectedValue;
        }
    }


    #region listview MarksPattern
    private void SetInitialRowMarksPattern()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RANK", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));

        dr = dt.NewRow();
        dr["RANK"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;

        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvCourseMarksPattern.DataSource = dt;
        lvCourseMarksPattern.DataBind();

        foreach (ListViewDataItem dataitem in lvCourseMarksPattern.Items)
        {
            ImageButton lnkRemove1 = dataitem.FindControl("lnkRemove1") as ImageButton;
            DropDownList ddlNewCourse1 = dataitem.FindControl("ddlNewCourse1") as DropDownList;
            // DropDownList ddlSession = dataitem.FindControl("ddlSession") as DropDownList;
            DropDownList ddlNewGrade1 = dataitem.FindControl("ddlNewGrade1") as DropDownList;
            //objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"].ToString()) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");
            // objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");

            if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
            {
                objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
            }
            else
            {
                //ddlEquivalence.SelectedItem.Text.StartsWith("E");

                objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
            }

            Session["ddlNewCourseCount"] = Convert.ToString((ddlNewCourse1.Items.Count) - 1);
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            ViewState["UGPGOT"] = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "UGPGOT", "COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"].ToString()) + " AND ISNULL(ACTIVESTATUS,0)=1");

            //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO"); //ViewState["COLLEGE_ID"]
            //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ddlNewCourse.SelectedValue) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

            lnkRemove1.Enabled = false;
            ddlNewCourse1.Focus();

            //DropDownList ddlExamType = dataitem.FindControl("ddlExamType") as DropDownList;
            //objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");
        }
    }

    #endregion

    protected void lnkRemove1_Click(object sender, ImageClickEventArgs e)
    {
        int rowIndex = 0;
        int k = 0;
        string coursenos = string.Empty;
        ImageButton lb = sender as ImageButton;
        int rowID = (Convert.ToInt32(lb.CommandArgument) - 1);
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                //if (gvRow.RowIndex < dt.Rows.Count )
                if (rowID <= dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            lvCourseMarksPattern.DataSource = dt;
            lvCourseMarksPattern.DataBind();

            foreach (ListViewDataItem dataitem in lvCourseMarksPattern.Items)
            {
                ImageButton lnkRemove1 = dataitem.FindControl("lnkRemove1") as ImageButton;
                if (dt.Rows.Count == 1)
                {
                    lnkRemove1.Enabled = false;
                }
                else
                {
                    lnkRemove1.Enabled = true;
                }

                coursenos = string.Empty;
                k++;
                // DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList box1 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewCourse1");

                        if (i != k - 1)
                        {
                            if (coursenos == string.Empty)
                            {
                                coursenos += dt.Rows[i]["Column1"].ToString();
                            }
                            else
                            {
                                coursenos = coursenos + "," + dt.Rows[i]["Column1"].ToString();
                                coursenos = coursenos.TrimEnd(',');
                            }
                        }
                    }
                }

                DropDownList ddlNewCourse1 = dataitem.FindControl("ddlNewCourse1") as DropDownList;
                DropDownList ddlNewGrade1 = dataitem.FindControl("ddlNewGrade1") as DropDownList;
                TextBox txtMaxMarks = dataitem.FindControl("txtMaxMarks") as TextBox;
                TextBox txtMinMarks = dataitem.FindControl("txtMinMarks") as TextBox;
                TextBox txtMarks = dataitem.FindControl("txtMarks") as TextBox;

                if (coursenos == "" || coursenos == string.Empty)
                {
                    if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
                    {
                        //objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");

                        objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
                    }
                }
                else
                {
                    if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
                    {
                        //objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");

                        objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ") AND ISNULL(C.ELECT,0)=1", "COURSE");
                    }
                }

                //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO");

                //if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                //{
                //    //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["NewCourse"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

                //    string proc_name = "PKG_ACD_GRADE_FOR_SUBJECT_EQUIVALENCE";
                //    string param = "@COLLEGE_ID,@COURSENO,@UGPGOT";
                //    string call_values = "" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + "," + Convert.ToInt32(ViewState["NewCourse"].ToString()) + "," + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "";
                //    DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();

                //    if (ds.Tables[1].Rows.Count > 0)
                //    {
                //        //ddlNewGrade1.DataSource = ds;
                //        //ddlNewGrade1.DataTextField = "GRADE";
                //        //ddlNewGrade1.DataValueField = "GRADENO";
                //        //ddlNewGrade1.DataBind();
                //        // dsStudent.Tables[0].Rows[0]["SMAX"]
                //        txtMaxMarks.Text = ds.Tables[1].Rows[0]["MaxMarks"].ToString();
                //        txtMinMarks.Text = ds.Tables[1].Rows[0]["MinMarks"].ToString();

                //    }
                //}
            }

            int h = Convert.ToInt32(Session["ddlNewCourseCount"].ToString());
            if (dt.Rows.Count < Convert.ToInt32(Session["ddlNewCourseCount"].ToString()))
            {
                ButtonAdd1.Enabled = true;
            }
        }

        //Set Previous Data on Postbacks
        SetPreviousDataForMarksPattern();
    }

    //store previous row data of lv
    private void SetPreviousDataForMarksPattern()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //extract the TextBox values 
                    DropDownList box1 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewCourse1");
                    TextBox box2 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldCCode1");
                    //TextBox box3 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldCourse1");
                    TextBox box3 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMaxMarks");
                    //TextBox box4 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldGrade1");
                    TextBox box4 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMinMarks");
                    DropDownList box55 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlExamType1");
                    //DropDownList box6 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewGrade1");
                    TextBox box6 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMarks");


                    box1.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["Column3"].ToString()))
                    {
                        box3.Text = dt.Rows[i]["Column3"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["Column4"].ToString()))
                    {
                        box4.Text = dt.Rows[i]["Column4"].ToString();
                    }
                    box55.SelectedValue = dt.Rows[i]["Column5"].ToString();
                    box6.Text = dt.Rows[i]["Column6"].ToString();

                    rowIndex++;

                    //box2.Focus();

                    if (Convert.ToInt32(box1.SelectedValue) == 0)
                    {
                        box3.Text = "";
                        box4.Text = "";

                    }
                }
            }
        }
    }

    protected void ButtonAdd1_Click(object sender, EventArgs e)
    {
        AddNewRowToLVForMarksPattern();
    }

    protected void AddNewRowToLVForMarksPattern()
    {
        int rowIndex = 0;
        int k = 0;
        string coursenos = string.Empty;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 20)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values 
                    DropDownList box1 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewCourse1");
                    TextBox box2 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldCCode1");
                    //TextBox box3 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldCourse1");
                    TextBox box3 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMaxMarks");
                    //TextBox box4 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldGrade1");
                    TextBox box4 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMinMarks");
                    DropDownList box5 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlExamType1");
                    //DropDownList box6 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewGrade1");
                    TextBox box6 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMarks");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RANK"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;
                    //   dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;

                    ViewState["Course"] = box1.Text;

                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                int h = Convert.ToInt32(Session["ddlNewCourseCount"].ToString());
                if (dtCurrentTable.Rows.Count >= Convert.ToInt32(Session["ddlNewCourseCount"].ToString()))
                {
                    ButtonAdd1.Enabled = false;
                }

                lvCourseMarksPattern.DataSource = dtCurrentTable;
                lvCourseMarksPattern.DataBind();

                foreach (ListViewDataItem dataitem in lvCourseMarksPattern.Items)
                {
                    coursenos = string.Empty;
                    k++;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList box1 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewCourse1");

                            if (i != k - 1)
                            {
                                if (coursenos == string.Empty)
                                {
                                    coursenos += dt.Rows[i]["Column1"].ToString();
                                }
                                else
                                {
                                    coursenos = coursenos + "," + dt.Rows[i]["Column1"].ToString();
                                    coursenos = coursenos.TrimEnd(',');
                                }
                            }
                        }
                    }

                    DropDownList ddlNewCourse1 = dataitem.FindControl("ddlNewCourse1") as DropDownList;
                    DropDownList ddlNewGrade1 = dataitem.FindControl("ddlNewGrade1") as DropDownList;
                    DropDownList ddlExamType1 = dataitem.FindControl("ddlExamType1") as DropDownList;
                    TextBox txtMaxMarks = dataitem.FindControl("txtMaxMarks") as TextBox;
                    TextBox txtMinMarks = dataitem.FindControl("txtMinMarks") as TextBox;
                    TextBox txtMarks = dataitem.FindControl("txtMarks") as TextBox;

                    if (coursenos == "" || coursenos == string.Empty)
                    {
                        if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                        }
                        else
                        {
                            //ddlEquivalence.SelectedItem.Text.StartsWith("E");

                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }
                    else
                    {
                        if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ") AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }


                }
            }
            else
            {
                objCommon.DisplayMessage(updprogupdGradePattern, "Maximum Row Limit Reached!", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks 
        SetPreviousDataForMarksPattern();

    }

    //store current row data of lv
    private void StoreCurrentData1()
    {
        int rowIndex = 0;
        int k = 0;
        string coursenos = string.Empty;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 20)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList box1 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewCourse1");
                    TextBox box2 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldCCode1");
                    //TextBox box3 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldCourse1");
                    TextBox box3 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMaxMarks");
                    //TextBox box4 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtOldGrade1");
                    TextBox box4 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMinMarks");
                    DropDownList box5 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlExamType1");
                    //DropDownList box6 = (DropDownList)lvCourseMarksPattern.Items[rowIndex].FindControl("ddlNewGrade1");
                    TextBox box6 = (TextBox)lvCourseMarksPattern.Items[rowIndex].FindControl("txtMarks");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RANK"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;

                    ViewState["NewCourse1"] = box1.Text;

                    rowIndex++;
                }

                ViewState["CurrentTable"] = dtCurrentTable;

                lvCourseMarksPattern.DataSource = dtCurrentTable;
                lvCourseMarksPattern.DataBind();


                foreach (ListViewDataItem dataitem in lvCourseMarksPattern.Items)
                {
                    ImageButton lnkRemove1 = dataitem.FindControl("lnkRemove1") as ImageButton;
                    if (dtCurrentTable.Rows.Count == 1)
                    {
                        lnkRemove1.Enabled = false;
                    }
                    else
                    {
                        lnkRemove1.Enabled = true;
                    }

                    coursenos = string.Empty;
                    k++;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i != k - 1)
                            //if (i == k - 1)
                            {
                                if (coursenos == string.Empty)
                                {
                                    coursenos += dt.Rows[i]["Column1"].ToString();
                                }
                                else
                                {
                                    coursenos = coursenos + "," + dt.Rows[i]["Column1"].ToString();
                                    coursenos = coursenos.TrimEnd(',');
                                }
                            }
                        }
                    }

                    DropDownList ddlNewCourse1 = dataitem.FindControl("ddlNewCourse1") as DropDownList;
                    DropDownList ddlNewGrade1 = dataitem.FindControl("ddlNewGrade1") as DropDownList;
                    DropDownList ddlExamType1 = dataitem.FindControl("ddlExamType1") as DropDownList;
                    TextBox txtMaxMarks = dataitem.FindControl("txtMaxMarks") as TextBox;
                    TextBox txtMinMarks = dataitem.FindControl("txtMinMarks") as TextBox;
                    TextBox txtMarks = dataitem.FindControl("txtMarks") as TextBox;


                    if (coursenos == "" || coursenos == string.Empty)
                    {
                        if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                        }
                        else
                        {
                            //ddlEquivalence.SelectedItem.Text.StartsWith("E");

                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }
                    else
                    {
                        if (ddlEquivalence1.SelectedItem.Text.ToString() != "ELECTIVE")
                        {
                            // objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlNewCourse1, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ") AND ISNULL(C.ELECT,0)=1", "COURSE");
                        }
                    }

                    //if (ddlNewCourse.SelectedIndex > 0)
                    //{
                    //  objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");
                    //}

                    if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                    {
                        //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["NewCourse"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

                        string proc_name = "PKG_ACD_GRADE_FOR_SUBJECT_EQUIVALENCE";
                        string param = "@COLLEGE_ID,@COURSENO,@UGPGOT";
                        string call_values = "" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + "," + Convert.ToInt32(ViewState["NewCourse1"].ToString()) + "," + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "";
                        DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);         //DataGrid dg = new DataGrid();

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            //ddlNewGrade1.DataSource = ds;
                            //ddlNewGrade1.DataTextField = "GRADE";
                            //ddlNewGrade1.DataValueField = "GRADENO";
                            //ddlNewGrade1.DataBind();
                            // dsStudent.Tables[0].Rows[0]["SMAX"]
                            txtMaxMarks.Text = ds.Tables[1].Rows[0]["MaxMarks"].ToString();
                            txtMinMarks.Text = ds.Tables[1].Rows[0]["MinMarks"].ToString();

                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdprogMarksPattern, "Maximum Row Limit Reached!", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks 
        SetPreviousDataForMarksPattern();
    }

    protected void ddlNewGrade1_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData1();
    }


    protected void txtOldCCode1_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData1();
    }
    protected void txtOldCourse1_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData1();
    }
    protected void txtOldGrade1_TextChanged(object sender, EventArgs e)
    {
        StoreCurrentData1();
    }

    protected void ddlExamType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        StoreCurrentData1();
    }

    protected void btnShow1_Click(object sender, EventArgs e)
    {
        divMsg.InnerHtml = string.Empty;
        divlvM.Visible = false;

        if (txtstudent1.Text.Trim() == "" || txtstudent1.Text.Trim() == string.Empty || txtstudent1.Text == null)
        {
            lblMsg.Text = "Please Enter Proper Univ. Reg. No.";
            txtstudent1.Focus();
            return;
        }

        lblMsg.Text = string.Empty;

        try
        {
            string searchText = txtstudent1.Text.Trim();
            string errorMsg = string.Empty;

            //student details shown
            ShowStudentsM(searchText, errorMsg);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //show student details
    private void ShowStudentsM(string searchText, string errorMsg)
    {
        divMsg.InnerHtml = string.Empty;
        if (txtstudent1.Text.Trim() == "" || txtstudent1.Text.Trim() == string.Empty || txtstudent1.Text == null)
        {
            lblMsg.Text = "Please Enter Univ. Reg. No. / Admission No.";
            txtstudent1.Focus();
            return;
        }
        lblMsg.Text = string.Empty;

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";

            //idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtStudent.Text.Trim() + "' OR ENROLLNO='" + txtStudent.Text.Trim() + "') AND IDTYPE=3 AND ISNULL(ADMCAN,0)=" + 0));
            idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtstudent1.Text.Trim() + "' OR ENROLLNO='" + txtstudent1.Text.Trim() + "') AND IDTYPE='" + ViewState["IDTYPENO"] + "' AND ISNULL(ADMCAN,0)=" + 0));

            if (idno == string.Empty)
            {
                btnSubmit1.Enabled = false;
                divdataM.Visible = false;
                objCommon.DisplayMessage(updMarksPattern, "No Student Found Having Univ. Reg. No. / Admission No. is " + txtstudent1.Text, this.Page);
            }
            else
            {
                if (idno != string.Empty)
                {
                    ViewState["idno"] = idno.ToString();
                    DataTableReader dtr = objSRegist.GetStudentDetails_Crescent(Convert.ToInt32(idno));

                    if (dtr.Read())
                    {
                        divdataM.Visible = true;
                        ddlSession1.SelectedIndex = 0;
                        ddlSemester1.SelectedIndex = 0;
                        ddlSession1.Focus();
                        btnSubmit1.Enabled = false;
                        btnReport1.Enabled = true;
                        Session["idno"] = dtr["idno"].ToString();
                        lblName1.Text = dtr["STUDNAME"].ToString();
                        lblRegNo1.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString();
                        ViewState["REGNO"] = lblRegNo1.Text;
                        Session["regno"] = lblRegNo1.Text;
                        ViewState["ROLLNO"] = dtr["ROLLNO"].ToString();
                        lblRegNo1.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                        lblEnrollno1.Text = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString();
                        lblEnrollno1.ToolTip = dtr["ENROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ENROLLNO"].ToString(); ;
                        lblScheme1.Text = dtr["SCHEMENAME"].ToString();
                        lblScheme1.ToolTip = dtr["BRANCHNO"].ToString();
                        ViewState["SCHEMENO"] = dtr["SCHEMENO"].ToString();
                        ViewState["SEMESTERNO"] = dtr["SEMESTERNO"].ToString();
                        ViewState["DEGREENO"] = dtr["DEGREENO"].ToString();
                        ViewState["SCHEMETYPE"] = dtr["SCHEMETYPE"].ToString();
                        ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                        ViewState["BRANCHNO"] = dtr["BRANCHNO"].ToString();
                        ViewState["ORGANIZATION_ID"] = dtr["ORGANIZATIONID"].ToString();

                        objCommon.FillDropDownList(ddlSession1, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID='" + ViewState["COLLEGE_ID"] + "' AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                    }
                    else
                    {
                        btnSubmit1.Enabled = false;
                        divdataM.Visible = false;
                        btnReport1.Enabled = false;
                    }
                    dtr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        StudentRegist objSR = new StudentRegist();
        Exam objexame = new Exam();
        string SESSIONNOS = string.Empty;
        string EQUIGRADES = string.Empty;
        DateTime TRANSDATE = DateTime.Now;

        int ret = 0;
        int prev_status = 0;

        foreach (ListViewDataItem dataitem in lvCourseMarksPattern.Items)
        {
            DropDownList ddlNewCourse1 = dataitem.FindControl("ddlNewCourse1") as DropDownList;
            //  DropDownList ddlSession = dataitem.FindControl("ddlSession") as DropDownList;
            DropDownList ddlNewGrade1 = dataitem.FindControl("ddlNewGrade1") as DropDownList;
            DropDownList ddlExamType1 = dataitem.FindControl("ddlExamType1") as DropDownList;
            TextBox txtOldCCode1 = dataitem.FindControl("txtOldCCode1") as TextBox;
            TextBox txtOldCourse1 = dataitem.FindControl("txtOldCourse1") as TextBox;
            TextBox txtOldGrade1 = dataitem.FindControl("txtOldGrade1") as TextBox;

            TextBox txtMinMarks = dataitem.FindControl("txtMinMarks") as TextBox;
            TextBox txtMaxMarks = dataitem.FindControl("txtMaxMarks") as TextBox;
            TextBox txtMarks = dataitem.FindControl("txtMarks") as TextBox;

            SESSIONNOS = ddlSession1.SelectedValue;
            objSR.IDNO = Convert.ToInt32(ViewState["idno"].ToString());
            objSR.REGNO = ViewState["REGNO"].ToString();
            objSR.ROLLNO = ViewState["ROLLNO"].ToString();
            objSR.SEMESTERNO = Convert.ToInt32(ddlSemester1.SelectedValue);
            objSR.SubEquivalence = Convert.ToInt32(ddlEquivalence1.SelectedValue);
            objSR.SCHEMENO = Convert.ToInt32(ViewState["SCHEMENO"].ToString());
            objSR.COURSENOS = objSR.COURSENOS + ddlNewCourse1.SelectedValue + ",";
            objSR.SECTIONNOS = "";
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.GRADE = "";
            objSR.ExamType = objSR.ExamType + ddlExamType1.SelectedValue + ",";
            objSR.CCODES = objSR.CCODES + txtOldCCode1.Text + ",";
            objSR.COURSENAMES = "";
            EQUIGRADES = "";

            objSR.MIN_MARKS = objSR.MIN_MARKS + txtMinMarks.Text + ",";
            objSR.MAX_MARKS = objSR.MAX_MARKS + txtMaxMarks.Text + ",";
            objSR.ACTUAL_MARKS = objSR.ACTUAL_MARKS + txtMarks.Text + ",";

            objSR.STUDENT_PATTERN = 2;
        }

        ret = objSReg.AddStudentResultData(SESSIONNOS, objSR);

        #region cmt
        //foreach (ListViewDataItem dataitem in lvCourse.Items)
        //{
        //    int operation = 1;
        //    DropDownList ddlNewCourse = dataitem.FindControl("ddlNewCourse") as DropDownList;
        //    DropDownList ddlNewGrade = dataitem.FindControl("ddlNewGrade") as DropDownList;
        //    TextBox txtOldCCode = dataitem.FindControl("txtOldCCode") as TextBox;
        //    TextBox txtOldCourse = dataitem.FindControl("txtOldCourse") as TextBox;
        //    TextBox txtOldGrade = dataitem.FindControl("txtOldGrade") as TextBox;

        //    objexame.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        //    objexame.SchemeNo = Convert.ToInt32(ViewState["SCHEMENO"].ToString());
        //    objexame.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        //    objexame.SectionNo = 0;
        //    objexame.Courseno = Convert.ToInt32(ddlNewCourse.SelectedValue);
        //    objexame.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
        //    objexame.Ua_No = Convert.ToInt32(Session["userno"].ToString());

        //    ret = objExamController.GradeAllotmentBySigleStudent(objexame, operation);
        //}    
        #endregion

        //ret = objMarkEntryC.ProcessResultAll(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue),
        //                                    ViewState["idno"].ToString(), Convert.ToInt32(prev_status), Convert.ToString(Session["ipAddress"].ToString()), Convert.ToInt32(1),
        //                                    Convert.ToInt32(0), Convert.ToInt32(Session["userno"].ToString()), 0, string.Empty, string.Empty, string.Empty, string.Empty, 0,
        //                                    string.Empty, string.Empty);

        if (!ret.Equals(-99))
        {
            ret = objMarkEntryC.ProcessResultAll(Convert.ToInt32(ddlSession1.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO"].ToString()), Convert.ToInt32(ddlSemester1.SelectedValue),
                                                ViewState["idno"].ToString(), Convert.ToInt32(prev_status), Convert.ToString(Session["ipAddress"].ToString()), Convert.ToInt32(ViewState["COLLEGE_ID"]),
                                                 Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["Org_Id"]));

            ret = objSReg.AddTransferedStudentRecord(SESSIONNOS, objSR, EQUIGRADES, TRANSDATE);

            //if (!ret.Equals(-99))
            //{
            objCommon.DisplayMessage(this.updMarksPattern, "Details Saved Successfully !!", this.Page);
            ClearM();
        }
        else
        {
            objCommon.DisplayMessage(this.updGradePattern, "Something went wrong.", this.Page);
            return;
        }
    }

    protected void btnReport1_Click(object sender, EventArgs e)
    {
        ShowReportForMark("TransferStudentSubjectMarksDetails", "rptTransferStudentSubjectMarkDetails.rpt", 1);
    }

    private void ShowReportForMark(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            string procedure = "PKG_ACD_GET_TRANSFER_COURSE_EQUI_DETAILS";
            string parameter = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO,@P_SUB_EQUI,@P_PATTERN";
            string values = "" + Convert.ToInt32(Session["idno"].ToString()) + "," + Convert.ToInt32(ddlSession1.SelectedValue) + "," + Convert.ToInt32(ddlSemester1.SelectedValue) + "," + Convert.ToInt32(ddlEquivalence1.SelectedValue) + "," + 2 + "";
            DataSet ds = objCommon.DynamicSPCall_Select(procedure, parameter, values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "");

                if (reportno == 1)
                {
                    url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + ",@P_COLLEGE_CODE=" + college_id + ",@P_SUB_EQUI=" + Convert.ToInt32(ddlEquivalence1.SelectedValue) + ",@P_PATTERN=" + 2 + "";
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updGradePattern, this.updGradePattern.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updGradePattern, "No Data Found for this selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ClearM();
    }

    private void ClearM()
    {
        ddlSemester1.SelectedIndex = 0;
        ddlEquivalence1.SelectedIndex = 0;
        txtstudent1.Text = string.Empty;
        divlvM.Visible = false;
        divdataM.Visible = false;
        lvCourseMarksPattern.DataSource = null;
        lvCourseMarksPattern.DataBind();
        btnSubmit1.Enabled = false;
        ddlEquivalence1.Focus();
        btnReport1.Enabled = false;
    }

    protected void ddlSession1_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlvM.Visible = false;
        //divlvM.Visible = true;

        lvCourseMarksPattern.DataSource = null;
        lvCourseMarksPattern.DataBind();
        btnSubmit1.Enabled = false;

        if (ddlSession1.SelectedIndex > 0)
        {
            ddlSemester1.Focus();
            // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO NOT IN(SELECT DISTINCT SEMESTERNO FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ") AND SEMESTERNO<=" + Convert.ToInt32(ViewState["SEMESTERNO"].ToString()), "SEMESTERNO");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO<(SELECT DISTINCT MIN(SEMESTERNO) FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ")", "SEMESTERNO");

            objCommon.FillDropDownList(ddlSemester1, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        }
        else
        {
            ddlSemester1.Items.Clear();
            ddlSemester1.Items.Add("Please Select");
            //ddlSemester1.SelectedIndex = 0;
            ddlSemester1.SelectedItem.Value = "0";
        }
    }
    protected void ddlSemester1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester1.SelectedIndex > 0)
        {
            //int exist = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CANCEL,0)=0") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession1.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CANCEL,0)=0"));

            string proc_name = "PKG_ACD_CHECK_COUNT_FOR_SUBJECT_EQUIVALENCE";
            string param = "@P_SEMESTERNO,@P_SESSIONNO,@P_IDNO,@P_TRANSFERED";
            string call_values = "" + Convert.ToInt32(ddlSemester1.SelectedValue) + "," + Convert.ToInt32(ddlSession1.SelectedValue) + "," + Convert.ToInt32(Session["idno"].ToString()) + "," + Convert.ToInt32(ddlEquivalence1.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values); //DataGrid dg = new DataGrid();

            int exist = 0;
            if (ds.Tables.Count > 0)
            {
                exist = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            if (exist == 0)
            {
                SetInitialRowMarksPattern();
                divlvM.Visible = true;
                btnSubmit1.Enabled = true;
                ButtonAdd1.Enabled = true;
            }
            else
            {
                divlvM.Visible = false;
                btnSubmit1.Enabled = false;
                objCommon.DisplayMessage(this.updMarksPattern, "Result data of this student already available for the selected semester!", this.Page);
            }
        }
        else
        {
            //ddlSemester1.Items.Clear();
            ddlSemester1.Items.Add("Please Select");
            ddlSemester1.SelectedIndex = 0;

            divlvM.Visible = false;
            btnSubmit1.Enabled = false;
        }
    }

    protected void ddlNewCourse1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StoreCurrentData1();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    //protected void txtMaxarks_TextChanged(object sender, EventArgs e)
    //{
    //    StoreCurrentData1();
    //}
    //protected void txtMinMarks_TextChanged(object sender, EventArgs e)
    //{
    //    StoreCurrentData1();
    //}

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GridView GVCreditDef = new GridView();
        string sp_Name = string.Empty; string sp_Paramters = string.Empty; string sp_Values = string.Empty;
        int idtypeNo = Convert.ToInt32(ddlEquivalence.SelectedValue);
        //sp_Name = "PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA_DETAILS_EXCEL";
        sp_Name = "PKG_SP_GET_EQUIVALENCE_STUDENT_DATA";
        sp_Paramters = " @P_IDTYPENO ";//@P_out
        //sp_Values = "" + sessionvalue + "," + CollegeID + "," + ActName + "";//0
        sp_Values = "" + idtypeNo + "";
        DataSet ds = objCommon.DynamicSPCall_Select(sp_Name, sp_Paramters, sp_Values);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GVCreditDef.DataSource = ds;
            GVCreditDef.DataBind();

            string attachment = "attachment; filename=" + "StudentSubjectEquivalenceReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVCreditDef.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found!!!", this.Page);
        }

        ddlEquivalence.Focus();
    }

    protected void btnExcelM_Click(object sender, EventArgs e)
    {
        GridView GVCreditDef = new GridView();
        string sp_Name = string.Empty; string sp_Paramters = string.Empty; string sp_Values = string.Empty;
        int idtypeNo = Convert.ToInt32(ddlEquivalence1.SelectedValue);
        //sp_Name = "PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA_DETAILS_EXCEL";
        sp_Name = "PKG_SP_GET_EQUIVALENCE_STUDENT_DATA";
        sp_Paramters = " @P_IDTYPENO ";//@P_out
        //sp_Values = "" + sessionvalue + "," + CollegeID + "," + ActName + "";//0
        sp_Values = "" + idtypeNo + "";
        DataSet ds = objCommon.DynamicSPCall_Select(sp_Name, sp_Paramters, sp_Values);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GVCreditDef.DataSource = ds;
            GVCreditDef.DataBind();

            string attachment = "attachment; filename=" + "StudentSubjectEquivalenceReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVCreditDef.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found!!!", this.Page);
        }

        ddlEquivalence1.Focus();
    }

    #endregion
}