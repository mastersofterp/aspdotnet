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
using System.Data.SqlClient;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;



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
                txtStudent.Focus();
                //StoreCurrentData();

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
            objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
            Session["ddlNewCourseCount"] = Convert.ToString((ddlNewCourse.Items.Count) - 1);
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            ViewState["UGPGOT"] = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "UGPGOT", "COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"].ToString()) + " AND ISNULL(ACTIVESTATUS,0)=0");

            //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO"); //ViewState["COLLEGE_ID"]
            //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ddlNewCourse.SelectedValue) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");

            lnkRemove.Enabled = false;
            ddlNewCourse.Focus();

            //DropDownList ddlExamType = dataitem.FindControl("ddlExamType") as DropDownList;
            //objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");
        }
    }

    protected void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRowToLV();
    }

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
                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                    }

                    //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO");

                    //if (ddlNewCourse.SelectedIndex > 0)
                    //{

                    // objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");

                    if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                    {
                        objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["Course"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");
                    }

                    //}
                }
            }
            else
            {
                objCommon.DisplayMessage(updProg, "Maximum Row Limit Reached!", this.Page);
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
                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2)", "COURSE");
                    }
                    else
                    {
                        // objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                        objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND OC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.SUBID IN(1,2) AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                    }

                    //if (ddlNewCourse.SelectedIndex > 0)
                    //{
                    //  objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "ISNULL(ACTIVESTATUS,0)=1", "EXAM_TYPENO");
                    //}

                    if (!string.IsNullOrEmpty(ViewState["UGPGOT"].ToString()))
                    {
                        objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_COURSE C INNER JOIN ACD_GRADE A ON C.SUBID=A.SUBID", "DISTINCT A.GRADENO", "A.GRADE", "A.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"].ToString()) + " AND A.SUBID = (SELECT SUBID FROM ACD_COURSE WHERE COURSENO=" + Convert.ToInt32(ViewState["NewCourse"].ToString()) + ") AND A.UGPGOT=" + Convert.ToInt32(ViewState["UGPGOT"].ToString()) + "", "GRADENO");
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updProg, "Maximum Row Limit Reached!", this.Page);
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
                    objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()), "COURSE");
                }
                else
                {
                    objCommon.FillDropDownList(ddlNewCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO) INNER JOIN ACD_OFFERED_COURSE OC ON (OC.COURSENO=C.COURSENO AND OC.COURSENO=CT.COURSENO AND CT.SCHEMENO=OC.SCHEMENO AND CT.SEMESTERNO=OC.SEMESTERNO)", "DISTINCT C.COURSENO", "(C.CCODE+' - '+C.COURSE_NAME) AS COURSE", "ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()) + " AND CT.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND OC.DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND C.COURSENO NOT IN(" + coursenos + ")", "COURSE");
                }

                //objCommon.FillDropDownList("Please Select", ddlNewGrade, "ACD_GRADE", "DISTINCT GRADENO", "GRADE", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + " AND GRADE_TYPE=" + Convert.ToInt32(ViewState["SCHEMETYPE"].ToString()), "GRADENO");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlSemester.SelectedIndex = 0;
        txtStudent.Text = string.Empty;
        divlv.Visible = false;
        divdata.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnSubmit.Enabled = false;
        txtStudent.Focus();
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

            idno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "IDNO", "(REGNO='" + txtStudent.Text.Trim() + "' OR ENROLLNO='" + txtStudent.Text.Trim() + "') AND IDTYPE=3 AND ISNULL(ADMCAN,0)=" + 0));

            if (idno == string.Empty)
            {
                btnSubmit.Enabled = false;
                divdata.Visible = false;
                objCommon.DisplayMessage(UpdatePanel1, "No Transfered Student Found Having Univ. Reg. No. / Admission No. is " + txtStudent.Text, this.Page);
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
            int exist = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CANCEL,0)=0") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + " AND ISNULL(CANCEL,0)=0"));
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
                objCommon.DisplayMessage(this.UpdatePanel1, "Result data of this student already available for the selected semester!", this.Page);
            }
        }
        else
        {
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

            SESSIONNOS = ddlSession.SelectedValue;
            objSR.IDNO = Convert.ToInt32(ViewState["idno"].ToString());
            objSR.REGNO = ViewState["REGNO"].ToString();
            objSR.ROLLNO = ViewState["ROLLNO"].ToString();
            objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
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

        ret = objMarkEntryC.ProcessResultAll(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue),
                                            ViewState["idno"].ToString(), Convert.ToInt32(prev_status), Convert.ToString(Session["ipAddress"].ToString()), Convert.ToInt32(ViewState["COLLEGE_ID"]),
                                             Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["Org_Id"]));

        ret = objSReg.AddTransferedStudentRecord(SESSIONNOS, objSR, EQUIGRADES, TRANSDATE);

        if (!ret.Equals(-99))
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Details Saved Successfully !!", this.Page);
            Clear();
        }
        else
        {

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

    private void ShowReport(string reportTitle, string rptFileName, int reportno)
    {
        try
        {
            //string procedure = "PKG_ACD_GET_TRANSFER_COURSE_EQUI_DETAILS";
            //string parameter = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO";
            //string values = "" + Convert.ToInt32(Session["idno"].ToString()) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "";
            //DataSet ds = objCommon.DynamicSPCall_Select(procedure, parameter, values);
            //return;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (reportno == 1)
            {
                url += "&param=@P_IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
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
        ShowReport("TransferStudentSubjectEquiDetails", "rptTransferStudentSubjectEquiDetails.rpt", 1);
    }

    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        StoreCurrentData();
    }
}