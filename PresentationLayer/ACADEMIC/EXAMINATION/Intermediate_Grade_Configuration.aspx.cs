//=================================================================================
// PROJECT NAME  : RFC-Common Code                                                         
// MODULE NAME   : ACADEMIC - Grade Configuration                                          
// CREATION DATE : 12-SEPT-2022                                                   
// CREATED BY    : Lalit Gaikwad                                               
// MODIFIED BY   : 02-NOV-2022                                                    
// MODIFIED DESC : 
//=================================================================================

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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Intermediate_Grade_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string th_pr = string.Empty;
    int subid;
    string schemeno = string.Empty;
    string value;
    int id1;
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

                Page.Title = Session["coll_name"].ToString();
                BindListView();
            }
            SetInitialRow();
            lvAssessment.Visible = true;
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            PopulateDropDown();
            ViewState["edit"] = null; 
            Session["action"] = null;


        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void PopulateDropDown()
    {
        try
        {


            //DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "COLLEGE_IDS,DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ViewState["College_ids"] = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();
            //    ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //    ViewState["Branchno"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            //    ViewState["Semesterno"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
            //}
            if (Session["usertype"].ToString().Equals("1"))
            {

                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COL_SCHEME_NAME asc");
            }
            else
            {
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COL_SCHEME_NAME asc");
            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_EndSemExamMarkEntry.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetDetails();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "SESSIONNO DESC");
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Configuration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourse.Items.Clear();
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE SR ON C.COURSENO = SR.COURSENO", "C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "C.COURSE_NAME DESC");
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Configuration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }



    }
    private void ShowDetails(int ID)
    {
        try
        {
            ConfigController objconfig = new ConfigController();
            DataSet ds = new DataSet();
            ds = objconfig.GetIdData(ID);
            if (ds.Tables[0].Rows.Count > 0)
            {

              
                //if (Convert.ToInt32(ddlSchool.Items.FindByValue(ds.Tables[0].Rows[0]["COSCHNO"].ToString().Trim())) != null)
                //{

                    ddlSchool.SelectedValue = ds.Tables[0].Rows[0]["COSCHNO"].ToString();
                   
                //}
                ddlSchool_SelectedIndexChanged(new object(), new EventArgs());
                //if (Convert.ToInt32(ddlSession.Items.FindByValue(ds.Tables[0].Rows[0]["SESSIONNO"].ToString().Trim())) != null)
                //{
                    ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString().Trim();
                //}
                ddlSession_SelectedIndexChanged(new object(), new EventArgs());
                //if (Convert.ToInt32(ddlCourse.Items.FindByValue(ds.Tables[0].Rows[0]["COURSENO"].ToString().Trim())) != null)
                //{
                    ddlCourse.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"].ToString().Trim();
                //}
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("txtGradeReleaseName", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("lstAss", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("txtOutOfMarks", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("ddlGradeType", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("ID", typeof(string)));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                   DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                   DataRow drCurrentRow = null;
                    
                   drCurrentRow = dtNewTable.NewRow();
                   drCurrentRow["txtGradeReleaseName"] = ds.Tables[0].Rows[i]["GRADEREALSE"].ToString().Trim();
                   drCurrentRow["lstAss"] = ds.Tables[0].Rows[i]["SUBMENUEXAM"].ToString().Trim();
                   drCurrentRow["txtOutOfMarks"] = ds.Tables[0].Rows[i]["OUTOFMARK"].ToString().Trim();
                   drCurrentRow["ddlGradeType"] = ds.Tables[0].Rows[i]["GRADE_COLUMN_NAME"].ToString().Trim();
                   drCurrentRow["ID"] = 0;
                           
                  dtNewTable.Rows.Add(drCurrentRow);
                                
                     

               }
                    ViewState["CurrentTable"] = dtNewTable;
                    lvAssessment.DataSource = dtNewTable;
                    lvAssessment.DataBind();
                    showdetails123();
                   
                }

                Session["action"] = "edit";

            }
        
        catch (NullReferenceException ex)
        {
            throw;
        }
    }
    private void showdetails123()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
               
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdValue");
                        TextBox txtGradeReleaseName = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtGradeReleaseName");
                        ListBox lstAss = (ListBox)lvAssessment.Items[rowIndex].FindControl("lstAss");
                        TextBox txtOutOfMarks = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
                        DropDownList ddlGradeType = (DropDownList)lvAssessment.Items[rowIndex].FindControl("ddlGradeType");
                        txtGradeReleaseName.Text = dt.Rows[i]["txtGradeReleaseName"].ToString();
                        ddlCourse_SelectedIndexChanged(new object(), new EventArgs());
                        //lstAss.SelectedValue= dt.Rows[i]["lstAss"].ToString();
                        txtOutOfMarks.Text = dt.Rows[i]["txtOutOfMarks"].ToString();
                        // ddlGradeType.SelectedValue= dt.Rows[i]["ddlGradeType"].ToString();
                        //ddlGradeType.ClearSelection();
                        ddlGradeType.Items.FindByText(dt.Rows[i]["ddlGradeType"].ToString()).Selected=true;
                        //lstAss.Enabled = false;

                        string[] Tempsemester = dt.Rows[i]["lstAss"].ToString().Split(',');
                        foreach (ListItem items in lstAss.Items)
                        {
                            foreach (string Semester in Tempsemester)
                            {
                                if (items.Value == Semester)
                                {
                                    items.Selected = true;
                                    //items.Enabled = false;
                                }
                            }
                           
                        }
                       
                        rowIndex++;
                        txtGradeReleaseName.Enabled = false;
                        lstAss.Enabled = false;
                        txtOutOfMarks.Enabled = false;
                        ddlGradeType.Enabled = false;

                    }
                    
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        // lvAssessment.Visible = true;
        foreach (ListViewDataItem item in lvAssessment.Items)
        {
            ListBox lstAss = item.FindControl("lstAss") as ListBox;

            objCommon.FillListBox(lstAss, "ACD_SUBEXAM_NAME SB INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AEC ON SB.SUBEXAMNO=AEC.SUBEXAMNO", "SB.SUBEXAMNO", "SB.SUBEXAMNAME", "AEC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AEC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SB.SUBEXAMNAME DESC");
        }
    }

    private void BindListBox()
    {
        try
        {
            foreach (ListViewDataItem item in lvAssessment.Items)
            {
                ListBox lstAss = item.FindControl("lstAss") as ListBox;
                
                objCommon.FillListBox(lstAss, "ACD_SUBEXAM_NAME SB INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AEC ON SB.SUBEXAMNO=AEC.SUBEXAMNO", "SB.SUBEXAMNO", "SB.SUBEXAMNAME", "AEC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AEC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SB.SUBEXAMNAME DESC");
               
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Configuration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void txtGradeReleaseName_TextChanged(object sender, EventArgs e)
    {
            //BindListBox();
        //foreach (ListViewDataItem item in lvAssessment.Items)
        //{
        //    ListBox lstAss = item.FindControl("lstAss") as ListBox;
           
        //    objCommon.FillListBox(lstAss, "ACD_SUBEXAM_NAME SB INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AEC ON SB.SUBEXAMNO=AEC.SUBEXAMNO", "SB.SUBEXAMNO", "SB.SUBEXAMNAME", "AEC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AEC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SB.SUBEXAMNAME DESC");
        //}
     
       
    }
    private void SetInitialRow()
    {
        //new 
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("txtGradeReleaseName", typeof(string)));
        dt.Columns.Add(new DataColumn("lstAss", typeof(string)));
        dt.Columns.Add(new DataColumn("txtOutOfMarks", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlGradeType", typeof(string)));

        dr = dt.NewRow();
        dr["txtGradeReleaseName"] = string.Empty;
        dr["lstAss"] = string.Empty;
        dr["txtOutOfMarks"] = string.Empty;
        dr["ddlGradeType"] = string.Empty;
       
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvAssessment.DataSource = dt;
        lvAssessment.DataBind();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0; int count = 0;
            if (ViewState["CurrentTable"] != null)
            {

                if (ddlSession.SelectedIndex == 0 && ddlSchool.SelectedIndex == 0 && ddlCourse.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updGradeConfiguration, "Please Select  College, Session and Course/Subject", this.Page);
                }
                else
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (lvAssessment.Items.Count > 0)
                    {

                        DataTable dtNewTable = new DataTable();
                        dtNewTable.Columns.Add(new DataColumn("txtGradeReleaseName", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("lstAss", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("txtOutOfMarks", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("ddlGradeType", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("ID", typeof(string)));
                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["txtGradeReleaseName"] = string.Empty;
                        drCurrentRow["lstAss"] = string.Empty;
                        drCurrentRow["txtOutOfMarks"] = string.Empty;
                        drCurrentRow["ddlGradeType"] = string.Empty;
                        drCurrentRow["ID"] = 0;
                        //dtNewTable.Rows.Add(drCurrentRow);
                        for (int i = 0; i < lvAssessment.Items.Count; i++)
                        {
                            
                            HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdValue");
                            TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtGradeReleaseName");
                            ListBox lstAss = (ListBox)lvAssessment.Items[rowIndex].FindControl("lstAss");
                            TextBox box3 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
                            DropDownList box4 = (DropDownList)lvAssessment.Items[rowIndex].FindControl("ddlGradeType");

                            if (box1.Text.Trim() == string.Empty)
                            {
                                objCommon.DisplayMessage(updGradeConfiguration, "Please Enter Grade Release Name", this.Page);
                                return;
                            }
                            else if (box3.Text.Trim() == string.Empty)
                            {
                                objCommon.DisplayMessage(updGradeConfiguration, "Please Enter Out of Marks Name", this.Page);
                                return;
                            }
                            else
                            {

                                drCurrentRow = dtNewTable.NewRow();
                                drCurrentRow["txtGradeReleaseName"] = box1.Text;

                                //txtGradeReleaseName_TextChanged(new object(), new EventArgs());
                                string lstvalue = "";
                                foreach (ListItem items in lstAss.Items)
                                {
                                    if (items.Selected == true)
                                    {
                                        lstvalue += items.Value.ToString() + ',';//drCurrentRow["lstAss"].ToString().Split(',');
                                    }
                                }
                                //foreach (ListItem items in lstAss.Items)
                                //{
                                //    foreach (string Semester in Tempsemester)
                                //    {
                                //        if (items.Value == Semester)
                                //        {
                                //            items.Selected = true;
                                //        }
                                //    }
                                //}
                                lstvalue = lstvalue.TrimEnd(',');
                                drCurrentRow["lstAss"] = lstvalue;
                                txtGradeReleaseName_TextChanged(new object(), new EventArgs());
                                drCurrentRow["txtOutOfMarks"] = box3.Text;
                                drCurrentRow["ddlGradeType"] = box4.SelectedValue;

                                //drCurrentRow = dtNewTable.NewRow();
                                drCurrentRow["ID"] = hdfid.Value;

                              
                                rowIndex++;
                               
                                dtNewTable.Rows.Add(drCurrentRow);

                            }



                        }
                       

                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["txtGradeReleaseName"] = string.Empty;
                        drCurrentRow["lstAss"] = string.Empty;
                        drCurrentRow["txtOutOfMarks"] = string.Empty;
                        drCurrentRow["ID"] = 0;
                        dtNewTable.Rows.Add(drCurrentRow);

                        ViewState["CurrentTable"] = dtNewTable;
                        lvAssessment.DataSource = dtNewTable;
                        lvAssessment.DataBind();
                      ///  int index1 = 0;
                       
                    }
                    else
                    {

                        objCommon.DisplayMessage(updGradeConfiguration, "Maximum Options Limit Reached", this.Page);

                    }

                }


            }
            else
            {
                objCommon.DisplayMessage(updGradeConfiguration, "Error!!!", this.Page);
            }


        }
        catch(Exception ex)
        {

        }
        SetPreviousDataGrades();

    }
    private void SetPreviousDataGrades()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                    foreach (ListViewDataItem item in lvAssessment.Items)
                    {
                        ListBox lstAss = item.FindControl("lstAss") as ListBox;

                        objCommon.FillListBox(lstAss, "ACD_SUBEXAM_NAME SB INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AEC ON SB.SUBEXAMNO=AEC.SUBEXAMNO", "SB.SUBEXAMNO", "SB.SUBEXAMNAME", "AEC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AEC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SB.SUBEXAMNAME DESC");
                    }
                //}
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdValue");
                    TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtGradeReleaseName");
                    ListBox box2 = (ListBox)lvAssessment.Items[rowIndex].FindControl("lstAss");
                    TextBox box3 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
                    DropDownList box4 = (DropDownList)lvAssessment.Items[rowIndex].FindControl("ddlGradeType");
                    box1.Text = dt.Rows[i]["txtGradeReleaseName"].ToString();
                    //txtGradeReleaseName_TextChanged(new object(), new EventArgs());
                    string[] Tempsemester = dt.Rows[i]["lstAss"].ToString().Split(',');
                    foreach (ListItem items in box2.Items)
                    {
                        for (int s = 0; s < Tempsemester.Length; s++)
                        {
                            if (items.Value == Tempsemester[s])
                            {
                                items.Selected = true;
                            }
                        }
                        //foreach (string Semester in Tempsemester)
                        //{
                        //    if (items.Value == Semester)
                        //    {
                        //        items.Selected = true;
                        //    }
                        //}
                    }
                    box3.Text = dt.Rows[i]["txtOutOfMarks"].ToString();
                    box4.SelectedValue = dt.Rows[i]["ddlGradeType"].ToString();
                    
                    rowIndex++;
                    //if (box1.Text!="" && box2.SelectedIndex>0 && box3.Text!="" && box4.SelectedValue!=0)
                    //{

                    //}
                    //else
                    //{


                    //}

                }
                rowIndex = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int count = 0;
                    DropDownList ddlExistingModule = lvAssessment.Items[rowIndex].FindControl("ddlGradeType") as DropDownList;
                    //objCommon.FillDropDownList(ddlExistingModule, "ACD_COURSE", "DISTINCT COURSENO", "COURSE_NAME", "ISNULL(ACTIVE,0)=1", "");
                    //objCommon.FillDropDownList(ddlExistingModule, "ACD_MODULE_REVISION", "COURSENO", "(NEW_CCODE  +'-'+ NEW_COURSENAME)AS COURSENAME", "ISNULL(REVISION_STATUS,0)=1 AND DEPTNO=" + ddlDepartment.SelectedValue, "NEW_COURSENAME");

                    ddlExistingModule.SelectedValue = dt.Rows[i]["ddlGradeType"].ToString();
                    foreach (ListViewDataItem item in lvAssessment.Items)
                    {
                        DropDownList ddlExistingModule1 = lvAssessment.Items[count].FindControl("ddlGradeType") as DropDownList;
                        count++;
                        if (Convert.ToInt32(ddlExistingModule1.SelectedValue) == 0)
                        {
                            ListItem itemToRemove = ddlExistingModule.Items.FindByValue(Convert.ToString(dt.Rows[i]["ddlGradeType"].ToString()));
                            ddlExistingModule1.Items.Remove(itemToRemove);
                        }
                        else
                        {
                            if (Convert.ToString(ddlExistingModule1.SelectedValue) != Convert.ToString(dt.Rows[i]["ddlGradeType"].ToString()))
                            {
                                ListItem itemToRemove = ddlExistingModule.Items.FindByValue(Convert.ToString(dt.Rows[i]["ddlGradeType"].ToString()));
                                ddlExistingModule1.Items.Remove(itemToRemove);
                            }
                        }
                    }
                    rowIndex++;
                }
            }

        }

        else
        {
            SetInitialRow();
        }
    }
    #region Clear Data
    protected void Clear()
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Clear Data
    private void showdetailsGrades()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdValue");
                        TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtGradeReleaseName");
                        ListBox box2 = (ListBox)lvAssessment.Items[rowIndex].FindControl("lstAss");
                        TextBox box3 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtOutOfMarks");
                        DropDownList box4 = (DropDownList)lvAssessment.Items[rowIndex].FindControl("ddlGradeType");
                        box1.Text = dt.Rows[i]["txtGradeReleaseName"].ToString();
                        BindListBox();
                        foreach (ListViewDataItem item in lvAssessment.Items)
                        {
                            ListBox lstAss = item.FindControl("lstAss") as ListBox;

                            foreach (ListItem items in lstAss.Items)
                            {

                                box2.SelectedValue += dt.Rows[i]["lstAss"].ToString();
                            }
                        }
                        box3.Text = dt.Rows[i]["txtOutOfMarks"].ToString();
                        box4.SelectedValue = dt.Rows[i]["ddlGradeType"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {

    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {



    }
    private void BindListView()
    {
        try
        {
            ConfigController objconfig = new ConfigController();
            DataSet ds = objconfig.GetAllSession();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvComponent.DataSource = ds;
                lvComponent.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvComponent);//Set label - 
            }
            else
            {
                pnlSession.Visible = false;
                lvComponent.DataSource = null;
                lvComponent.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void cleartext()
    {
        ddlSchool.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        Session["action"] = null;
        foreach (ListViewDataItem item in lvAssessment.Items)
        {
            HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
            TextBox txtGradeReleaseName = item.FindControl("txtGradeReleaseName") as TextBox;
            TextBox txtOutOfMarks = item.FindControl("txtOutOfMarks") as TextBox;
            DropDownList ddlGradeType = item.FindControl("ddlGradeType") as DropDownList;
            ListBox lstAss = item.FindControl("lstAss") as ListBox;
            txtGradeReleaseName.Text = "";
            txtOutOfMarks.Text = "";
            ddlGradeType.SelectedIndex = 0;
          
        }
        
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        ConfigController objconfig = new ConfigController();
        if (ddlSchool.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updGradeConfiguration, "Please Select College ", this.Page);
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updGradeConfiguration, "Please Select  Session.", this.Page);
            return;
        }
        else if (ddlCourse.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updGradeConfiguration, "Please Select Course / Subject ", this.Page);
            return;
        }
        CustomStatus cs = 0;
        string graderealse = "";
        
        string outofmark = "";
        
        string id = "";
        DateTime DTS;
        string GradenameType=string.Empty;
        foreach (ListViewDataItem item in lvAssessment.Items)
        //  for (int i = 0; i < lvAssessment.Rows.Count; i++)
        {
            string _ua_no = string.Empty;
            HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
            TextBox txtGradeReleaseName = item.FindControl("txtGradeReleaseName") as TextBox;
            TextBox txtOutOfMarks = item.FindControl("txtOutOfMarks") as TextBox;
            ListBox lstAss = item.FindControl("lstAss") as ListBox;
            DropDownList ddlGradeType = item.FindControl("ddlGradeType") as DropDownList;


            foreach (ListItem items in lstAss.Items)
            {
                if (items.Selected == true)
                {

                    _ua_no += items.Value + ",";
                }
            }
            graderealse = (txtGradeReleaseName.Text);
            outofmark = (txtOutOfMarks.Text);
            id = Convert.ToString(hfdValue.Value);
            DTS = DateTime.Now;
            GradenameType = (ddlGradeType.SelectedItem.Text);
            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {

                int ID = Convert.ToInt32(Session["ID"]);
                cs = (CustomStatus)objconfig.Updateinetrmediate(ID, Convert.ToString(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["SchemeNo"].ToString()), Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(1));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {

                    BindListView();

                    //ddlSchool.SelectedIndex = 0;
                    //ddlSession.SelectedIndex = 0;
                    //ddlCourse.SelectedIndex = 0;
                    //Session["action"] = null;
                  
                    objCommon.DisplayMessage(this.updGradeConfiguration, "Intermediate Grade Configuration Updated sucessfully", this.Page);


                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeConfiguration, "Failed To Update Record ", this.Page);
                }

            }

            else
            {
                cs = (CustomStatus)objconfig.Insert_Intermediate_Grade_Configuration_Details(Convert.ToString(graderealse), Convert.ToString(_ua_no), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToString(outofmark), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["SchemeNo"].ToString()), Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToString(GradenameType));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindListView();
                    // cleartext();
                    objCommon.DisplayMessage(this.updGradeConfiguration, "Intermediate Grade Configuration Added Successfully.", this.Page);


                }
                else
                {
                    objCommon.DisplayMessage(this.updGradeConfiguration, "Failed To Save Record ", this.Page);
                }


            }
            
        }
        cleartext();
         
     //Edit 
}


    private void GetDetails()
    {
        try
        {
            ViewState["SchemeNo"] = string.Empty;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchool.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["SchemeNo"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = int.Parse(btnEdit.CommandArgument);
            Session["ID"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails(ID);
            ViewState["edit"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void SetInitialRownew()
    {
        //new 
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("txtgradenew", typeof(string)));
        dt.Columns.Add(new DataColumn("lstAssnew", typeof(string)));
        dt.Columns.Add(new DataColumn("txtOutOfMarksnew", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlGradeType1", typeof(string)));
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dr = dt.NewRow();
        dr["txtgradenew"] = string.Empty;
        dr["lstAssnew"] = string.Empty;
        dr["txtOutOfMarksnew"] = string.Empty;
        dr["ddlGradeType1"] = string.Empty;
        dr["ID"] = 0;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        
      
            lvlist.DataSource = dt;
            lvlist.DataBind();
            lvlist.Visible = true;
    }

    private void showdetails1234()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    foreach (ListViewDataItem item in lvlist.Items)
                    {
                        ListBox lstAssnew = item.FindControl("lstAssnew") as ListBox;

                        objCommon.FillListBox(lstAssnew, "ACD_SUBEXAM_NAME SB INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AEC ON SB.SUBEXAMNO=AEC.SUBEXAMNO", "SB.SUBEXAMNO", "SB.SUBEXAMNAME", "AEC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AEC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "SB.SUBEXAMNAME DESC");
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdfid = (HiddenField)lvlist.Items[rowIndex].FindControl("hfdValue");
                        TextBox txtgradenew = (TextBox)lvlist.Items[rowIndex].FindControl("txtgradenew");
                        ListBox lstAss = (ListBox)lvlist.Items[rowIndex].FindControl("lstAssnew");
                       
                        TextBox txtOutOfMarksnew = (TextBox)lvlist.Items[rowIndex].FindControl("txtOutOfMarksnew");
                        DropDownList ddlGradeType1 = (DropDownList)lvlist.Items[rowIndex].FindControl("ddlGradeType1");
                        txtgradenew.Text = dt.Rows[i]["txtgradenew"].ToString();
                        //ddlCourse_SelectedIndexChanged(new object(), new EventArgs());
                        string[] Tempsemester = dt.Rows[i]["lstAssnew"].ToString().Split(',');
                        foreach (ListItem items in lstAss.Items)
                        {
                           foreach (string semester in Tempsemester)
                            {
                                if (items.Value == semester)
                                {
                                    items.Selected = true;
                                   
                                }
                            }
                        }

                        txtOutOfMarksnew.Text = dt.Rows[i]["txtOutOfMarksnew"].ToString();
                        ddlGradeType1.Items.FindByText(dt.Rows[i]["ddlGradeType1"].ToString()).Selected = true;
                       
                        rowIndex++;
                        txtgradenew.Enabled = false;
                        txtOutOfMarksnew.Enabled = false;
                       // lstAss.Enabled = false;
                        ddlGradeType1.Enabled = false;
                        //lvlist.DataSource = dt;
                        //lvlist.DataBind();
                    }


                }
            }
            else
            {
                SetInitialRownew();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
            SetInitialRownew();
            lvlist.Visible = true;
            ConfigController objconfig = new ConfigController();
            DataSet ds = new DataSet();
           
                int id1 = Convert.ToInt32(Session["ID"].ToString());
            
                ds = objconfig.GetIdData(id1);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("txtgradenew", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("lstAssnew", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("txtOutOfMarksnew", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("ddlGradeType1", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                        DataRow drCurrentRow = null;

                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["txtgradenew"] = ds.Tables[0].Rows[i]["GRADEREALSE"].ToString().Trim();
                        drCurrentRow["lstAssnew"] = ds.Tables[0].Rows[i]["SUBMENUEXAM"].ToString().Trim();
                        drCurrentRow["txtOutOfMarksnew"] = ds.Tables[0].Rows[i]["OUTOFMARK"].ToString().Trim();
                         drCurrentRow["ddlGradeType1"] = ds.Tables[0].Rows[i]["GRADE_COLUMN_NAME"].ToString().Trim();
                        drCurrentRow["ID"] = 0;

                        dtNewTable.Rows.Add(drCurrentRow);



                    }

                    //showdetails1234();
                    ViewState["CurrentTable"] = dtNewTable;
                    lvlist.DataSource = dtNewTable;
                    lvlist.DataBind();
                    showdetails1234();
                }

               
            

           
    }
}
