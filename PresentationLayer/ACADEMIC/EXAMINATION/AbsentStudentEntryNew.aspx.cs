using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls.Adapters;
using DynamicAL_v2;
using System.Drawing;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class ACADEMIC_EXAMINATION_AbsentStudentEntryNew : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    Exam objExam = new Exam();
    MarksEntryController objMarksEntry = new MarksEntryController();
    DynamicControllerAL AL = new DynamicControllerAL();

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.FillDropdown();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    if (Session["usertype"].ToString() != "1")
                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
                    else

                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

                }
            }
            divMsg.InnerHtml = string.Empty;
            btnLock.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
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
            objCommon.FillDropDownList(ddlsessionforabsent, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc"); //--AND FLOCK = 1
            btnLock.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntryNew.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillStudentList()
    {
        try
        {
            string examname = string.Empty;
            string subexamname = string.Empty;
            examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + (ddlexamnameabsentstudent.SelectedValue));
            subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));
            //subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "FLDNAME", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));


            DataSet ds = null;
            ds = objExamController.GetAbsentEntryDetails(Convert.ToInt32(ddlsessionforabsent.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlcourseforabset.SelectedValue), Convert.ToInt32(ViewState["schemeno"]),examname,subexamname);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               
                lvabsent.Visible = true;
                lvabsent.DataSource = ds;
                lvabsent.DataBind();
                
            }

            else
            {
                objCommon.DisplayMessage(this, "No Record found", this.Page);
                lvabsent.DataSource = ds;
                lvabsent.DataBind();
                //pnlStudents.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.FillStudentList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    ////private void FillStudentMidsem()
    ////{
    ////    try
    ////    {
    ////        DataSet ds = null;
    ////        //fill list view control

    ////        int subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + ddlCourse.SelectedValue));
    ////        //AND S.CAN=0 condition removed by Anand dt17 April 2013
    ////        if (subid == 1)
    ////        {
    ////            ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT B ON B.IDNO = S.IDNO", "B.IDNO", "B.REGNO,CAST(B.S2MARK AS INT)S2MARK,CAST(B.EXTERMARK AS INT)EXTERMARK,B.SEATNO,B.AB_CC_LOCK,S.STUDNAME,(CASE B.PREV_STATUS WHEN 0 THEN 'Regular' ELSE 'Backlog' END)EXAMTYPE", "B.SESSIONNO =" + ddlSession.SelectedValue + " AND B.COURSENO =" + ddlCourse.SelectedValue + " AND S.ADMCAN=0 AND B.REGISTERED = 1 AND (B.CANCEL IS NULL OR B.CANCEL = 0) AND (B.DETAIND IS NULL OR B.DETAIND=0)", "B.PREV_STATUS,B.REGNO");

    ////        }


    ////        if (ds != null && ds.Tables[0].Rows.Count > 0)
    ////        {
    ////            lvMidsem.DataSource = ds;
    ////            lvMidsem.DataBind();

    ////        }
    ////        else
    ////        {
    ////            lvMidsem.DataSource = ds;
    ////            lvMidsem.DataBind();

    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        if (Convert.ToBoolean(Session["error"]) == true)
    ////            objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.FillStudentList --> " + ex.Message + " " + ex.StackTrace);
    ////        else
    ////            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    ////    }
    ////}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        btnLock.Enabled = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            int count = 0;
            objExam.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objExam.Sessionno = Convert.ToInt32(ddlsessionforabsent.SelectedValue);
            objExam.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objExam.Courseno = Convert.ToInt32(ddlcourseforabset.SelectedValue);
            objExam.ExamID = Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue);
            objExam.SubExamNo = Convert.ToInt32(ddlsubexamname.SelectedValue);
            objExam.Ua_No = Convert.ToInt32(Session["userno"].ToString());

            if (lvabsent.Items.Count > 0)
            {
                foreach (ListViewDataItem item in lvabsent.Items)
                {
                    CheckBox CheckId = item.FindControl("CheckId") as CheckBox;
                    if (CheckId.Checked == true && CheckId.BackColor != Color.Green)
                    {
                        count++;
                        int Idno = Convert.ToInt32(CheckId.ToolTip.Trim());
                        if (Idno > 0)
                        {	
                            string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG";
                            string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_OUT";
                            string Call_Values = "" + Idno + "," + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + Convert.ToInt32(ddlcourseforabset.SelectedValue) + "," + Convert.ToInt32(ddlSem.SelectedValue) + "," + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + "," + Convert.ToInt32(ddlsubexamname.SelectedValue) + "," + objExam.Ua_No + ",1";

                          string  que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);



                            //ret = Convert.ToInt32(objExamController.GetUpdateAbsentEntry(Convert.ToInt32(ddlsessionforabsent.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlcourseforabset.SelectedValue), Convert.ToInt32(Idno)));
                            ret++;
                        }
                    }
                }
                if (count == 0)
                {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Select Atleast one Student from the list", this.Page);
                    return;
                }
                if (ret > 0)
                {
                    FillStudentList();
                    objCommon.DisplayMessage(this.updpnlExam, "Data saved Successfully...!", this.Page);
                    return; 
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntryNew.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    //protected void btnReport_Click1(object sender, EventArgs e)
    //{
    
    //  //  ShowAnswerPaperReport("Absent", "AbsentStudentEntryReport.rpt");
    //    ExportOptions exopt = default(ExportOptions);

    //    //create object for destination option - for set path of your pdf file save     
    //    DiskFileDestinationOptions dfdopt = new DiskFileDestinationOptions();
    //    ReportDocument RptDoc = new ReportDocument();

    //    //Map your crystal report path    
    //    RptDoc.Load(Server.MapPath("~/Reports/AbsentStudentEntryReport.rpt"));

    //    //Set database Connection    
    //    // setDbInfo(RptDoc,"IITMSPC111\MSSQLSERVER2012","Test","sa","iitms!123");    
    //    //Report pfd name    
    //    string fname = "AbsentStudentEntryReport.pdf";
    //    dfdopt.DiskFileName = Server.MapPath(fname);

    //    exopt = RptDoc.ExportOptions;
    //    exopt.ExportDestinationType = ExportDestinationType.DiskFile;

    //    //for PDF select PortableDocFormat for excel select ExportFormatType.Excel    
    //    exopt.ExportFormatType = ExportFormatType.PortableDocFormat;
    //    exopt.DestinationOptions = dfdopt;

    //    //finally export your report document    
    //    RptDoc.Export();

    //    //To open your PDF after save it from crystal report    

    //    string Path = Server.MapPath(fname);
    //    FileInfo file = new FileInfo(Path);
    //    Response.ClearContent();
    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
    //    Response.AddHeader("Content-Length", file.Length.ToString());
    //    Response.ContentType = "application/pdf";
    //    Response.TransmitFile(file.FullName);
    //    Response.Flush();
    //    RptDoc.Dispose();
    //    RptDoc.Close();
    //    RptDoc = null;  
    //}


    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            //int lck = 1;
            //string idno = "";
            //string ExamNo = Convert.ToString(ddlExTTType.SelectedValue);
            //MarksEntryController objMarkController = new MarksEntryController();
            //CustomStatus ret = (CustomStatus)objMarkController.LockAB_CC(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ExamNo, idno, lck);
            //if (ret == CustomStatus.RecordSaved)
            //{
            //    ////objCommon.DisplayMessage(this.updpnlExam, "Entries Saved and Locked Successfully", this.Page);
            //    FillStudentList();
            //    btnLock.Enabled = false;
            //}
            //else
            //objCommon.DisplayMessage(this.updpnlExam, "Error in Saving Record...", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        FillStudentList();
        //btnLock.Enabled = false;
    }

    protected void btnBlankDocket_Click(object sender, EventArgs e)
    {
        ////ShowDocketReport("BlankDocketFormat", "DocketBlankReport.rpt");
    }

    protected void btnDocket_Click(object sender, EventArgs e)
    {
        ////ShowDocketReport("DocketFormat", "Docket_AbsentReport.rpt");

    }

    private void ShowAbsentReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/Commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + ",@P_EXAMNO=1,@P_COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue) + "";
           
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowDocketReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlsessionforabsent_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlSem.Items.Clear();
        string odd_even = objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue));
        string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue));
        string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION",  "");
        if (exam_type == "1" && odd_even != "3")
        {
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

        }
        else
        {
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

        }
       
        ddlexamnameabsentstudent.Items.Clear();
        ddlexamnameabsentstudent.Items.Add(new ListItem("Please Select", "0"));
        ddlcourseforabset.Items.Clear();
        ddlcourseforabset.Items.Add(new ListItem("Please Select", "0"));
        lvdetails.DataSource = null;
        lvdetails.DataBind();
    }
  
    protected void ddlcourseforabset_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcourseforabset.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME E WITH (NOLOCK) INNER JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (E.EXAMNO=AM.EXAMNO)", "DISTINCT E.EXAMNO", "E.EXAMNAME", "E.EXAMNO > 0 AND ISNULL(E.EXAMNAME,'')<>'' AND ISNULL(E.ACTIVESTATUS,0)=1", "E.EXAMNO");
           
        }
        else
        {
            //ddlblock.Items.Clear();
            //ddlblock.Items.Add(new ListItem("Please Select", "0"));
            lvdetails.DataSource = null;
            lvdetails.DataBind();
        }
    }

    //aayushi gupta
    public void BindSeatPlan()
    {
        try
        {
            // DataSet ds;
            int session = 0;
            int course = 0;
            int roomno = 0;
            session = Convert.ToInt32(ddlsessionforabsent.SelectedValue);
            course = Convert.ToInt32(ddlcourseforabset.SelectedValue);
           // roomno = Convert.ToInt32(ddlblock.SelectedValue);


            DataSet ds = objExamController.GetAllSeatPlan(roomno, session, course);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lvdetails.DataSource = ds;
                    lvdetails.DataBind();
                    pnldetails.Visible = true;
                }
                else
                {
                    lvdetails.DataSource = null;
                    lvdetails.DataBind();
                    //   pnldetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.BindSeatPlan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
 
    protected void btnAbsentReport1_Click(object sender, EventArgs e)
    {
        ShowAbsentReport("Absent_Student_Report", "AbsentStudentEntryFormReport.rpt");
    }

    protected void ddlexamnameabsentstudent_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (ddlexamnameabsentstudent.SelectedIndex > 0)
        {
            int Subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue)));
            objCommon.FillDropDownList(ddlsubexamname, "ACD_SUBEXAM_NAME", "DISTINCT SUBEXAMNO", "SUBEXAMNAME", "SUBEXAMNO > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + " AND SUBEXAM_SUBID=" + Subid, "SUBEXAMNO");
          ddlsubexamname.Focus();
        }
         else
         {
             //ddlblock.Items.Clear();
             //ddlblock.Items.Add(new ListItem("Please Select", "0"));
             lvdetails.DataSource = null;
             lvdetails.DataBind();
         }


    }   
    
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {

            DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", "@P_COLSCHEMENO", "" + Convert.ToInt32(ddlClgname.SelectedValue) + "");
            //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
       
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                //objCommon.FillDropDownList(ddldegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB WITH (NOLOCK) ON CDB.DEGREENO=A.DEGREENO", "DISTINCT A.DEGREENO", "A.DEGREENAME", "A.DEGREENO >0", "A.degreeno");
                
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
           
            objCommon.FillDropDownList(ddlcourseforabset, "ACD_COURSE ACO", "ACO.COURSENO", "ACO.CCODE+'-'+COURSE_NAME", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND SEMESTERNO=" + ddlSem.SelectedValue, "");
            ddlcourseforabset.Focus();
        }
        else
        {
            ddlcourseforabset.Items.Clear();
            ddlcourseforabset.Items.Add(new ListItem("Please Select", "0"));
            ddlexamnameabsentstudent.Items.Clear();
            ddlexamnameabsentstudent.Items.Add(new ListItem("Please Select", "0"));
        }
        lvdetails.DataSource = null;
        lvdetails.DataBind();


    }

    protected void ddlsubexamname_SelectedIndexChanged(object sender, EventArgs e)
    {

        lvdetails.DataSource = null;
        lvdetails.DataBind();

    }

    protected void lvabsent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataSet ds = null;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            CheckBox chk = e.Item.FindControl("CheckId") as CheckBox;

            string examname = string.Empty;
            string subexamname = string.Empty;
            examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + (ddlexamnameabsentstudent.SelectedValue));
            subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));

            ds = objExamController.GetAbsentEntryDetails(Convert.ToInt32(ddlsessionforabsent.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlcourseforabset.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), examname, subexamname);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[i]["LOGID"].ToString()) > 0)
                {
                    if (chk.ToolTip == ds.Tables[0].Rows[i]["IDNO_LOG"].ToString())
                    {
                        chk.BackColor = Color.Green;
                        chk.Enabled = false;
                    }                  
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }
    }

    protected void ShowAnswerPaperReport()
    { 
    
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {


            string examname = string.Empty;
            string subexamname = string.Empty;
            examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + (ddlexamnameabsentstudent.SelectedValue));
            subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_EXAM=" + examname + ",@P_SUB_EXAM=" + subexamname;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

        
    protected void btnReport_Click1(object sender, EventArgs e)
    {

        ShowReport("RetestExamStatus_Report", "RetestExamStatusReport.rpt");


    }
     
   
}

