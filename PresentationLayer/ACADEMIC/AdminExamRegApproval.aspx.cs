using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_AdminExamRegApproval : System.Web.UI.Page
    
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    StudentController studCont = new StudentController();
    Exam objExam = new Exam();


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
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
             
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                
                else
                {
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    if (Session["usertype"].ToString().Equals("1"))    
                    {
                        FillDropdown();
                        btnsubmit.Enabled = false;
                    }

                    else
                    {
                        div1.Visible = false;
                        objCommon.DisplayUserMessage(this.Page, "You are not authorized to view this page.", this.Page);
                        btnsubmit.Enabled = false;
                    }
                   
                }
            }
            divMsg.InnerHtml = string.Empty;
          // CheckActivity(); COMMENT(1)
          

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    private void CheckActivity()
    {
        string sessionno =string.Empty;
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "ISNULL(SA.SESSION_NO,0)", "AM.ACTIVITY_CODE = 'EXAPP' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        sessionno = sessionno == string.Empty ? "-1" : sessionno;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.Page,"This Activity has been Stopped. Contact Admin.!!", this.Page);
                divExamHalTckt.Visible = false;
                // divNote.Visible = false;
            }
          
        }
        else
        {
            objCommon.DisplayMessage(this.Page,"Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            divExamHalTckt.Visible = false;
            //divNote.Visible = false;
        }
        dtr.Close();
        //return true;
    }

    private void CheckPageAuthorization()
    {       
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration_New.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration_New.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            //rdoDegree.SelectedIndex = 0;
          
            }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   

    
    protected void BindListView()
    {
        try
        {
            DataSet ds = objExamController.GetExamRegStud(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(rdoDegree.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToInt32(Session["OrgId"]) == 1)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#REMARKSDATA1').hide();$('td:nth-child(5)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#REMARKSDATA1').hide();$('td:nth-child(5)').hide();});", true);
                //}
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                lvStudentRecords.Visible = true;
                btnsubmit.Enabled = true;
               
          
            }
            else
            {
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                objCommon.DisplayMessage(this,"Record Not Found", this.Page);
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AdminExamRegistrationApproval.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  



    //private void ShowReport(int param, int Semesterno, int Degreeno, int branchno,int College_id, int prev_status,string examno, string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string Examno = objCommon.LookUp(" ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", " ED.FLDNAME IN('EXTERMARK') AND EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO= " + ddlDegree.SelectedValue);
    //        //int sessionno = Convert.ToInt32(Session["currentsession"]);
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + 
    //            ",@P_IDNO=" + param + 
    //            ",@P_BRANCHNO=" + branchno + 
    //            ",@P_DEGREENO=" + Degreeno + 
    //            ",@P_SEMESTERNO=" + Semesterno + 
    //            ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + 
    //           ",@Examname="+examno+
    //            ",@P_EXAMNO=" + ddlExamname.SelectedValue +
    //            ",@P_COLLEGE_ID="+College_id+
    //           // ",@P_PREV_STATUS=" + prev_status + 
    //            ",@P_USER_FUll_NAME=" + Session["username"];

    //         divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += "</script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlExamname.SelectedIndex > 0)
    //        {
    //            //btnPrint.
    //            //int A = lvStudentRecords.Items.Count;
    //            Button btnPrint = (Button)(sender);
    //            string semesterno = Convert.ToString(ddlSemester.SelectedValue);
    //            string prev_status = string.Empty;
    //            int idno = Convert.ToInt32(Session["idno"]);
    //            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
    //            int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
    //            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
    //            if (semesterno != "")
    //            {
    //                foreach (ListViewDataItem item in lvStudentRecords.Items)
    //                {
    //                    idno = Convert.ToInt32((item.FindControl("hidIdNo") as HiddenField).Value.Trim());
    //                    //int Semesterno = Convert.ToInt32((item.FindControl("hdfsem") as HiddenField).Value.Trim());
    //                    degreeno = Convert.ToInt32((item.FindControl("hdfdegreeno") as HiddenField).Value.Trim());
    //                    branchno = Convert.ToInt32((item.FindControl("hdfbranchno") as HiddenField).Value.Trim());
    //                    //int prev_status = Convert.ToInt32((item.FindControl("hdfprev_status") as HiddenField).Value.Trim());


    //                    //ShowReport(idno, Semesterno, Degreeno, branchno, prev_status, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");
    //                }
    //                int College_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + idno));
    //                string examno = objCommon.LookUp("ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNO = " + ddlExamname.SelectedValue + "");
    //                prev_status = objCommon.LookUp("ACD_STUDENT_RESULT", "TOP(1) PREV_STATUS", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "");
    //                int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(COUNT(CCODE),0)", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + idno + " AND ISNULL(EXAM_REGISTERED,0)=1 AND ISNULL(REGISTERED,0)=1 AND ISNULL(DETAIND,0)=0"));
    //                // int count = Convert.ToInt32(objCommon.LookUp("ACD_NODUES_STATUS", "COUNT(1)", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "AND NODUES_StATUS=1"));
    //                if (count > 0)
    //                {
    //                    // Add 09092022
    //                    if (Convert.ToInt32(Session["OrgId"]) == 9)
    //                    {

    //                        ShowReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), examno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_atlas.rpt");

    //                    }
    //                    else
    //                    {
    //                        ShowReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), examno, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");
    //                    }

    //                    int chkg = studCont.InsAdmitCardLogStudent(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), (Session["idno"].ToString()) + ".", ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), "Student Login", Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToDateTime(DateTime.Now));
    //                    if (chkg == 2)
    //                    {
    //                        objCommon.DisplayMessage("Admit Card Successfully Generated", this.Page);
    //                    }
    //                }
    //                else
    //                {
    //                    //objCommon.DisplayMessage(updAdmit,"No Dues are Pending.", this.Page);
    //                    objCommon.DisplayMessage(updAdmit, "Your Exam Form is not Confirmed yet,Kindly Contact to your Department.", this.Page);
    //                    pnlStudent.Visible = false;
    //                    btnPrint.Visible = false;
    //                    return;
    //                }
    //            }
    //        }
    //        else {
    //               objCommon.DisplayMessage("Please Select Exam Names", this.Page);
            
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}
    protected void lvStudentRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        string examno = objCommon.LookUp("ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNO = " + ddlExamname.SelectedValue + "");
        int selectionindex = lvStudentRecords.SelectedIndex;
        ListViewItem item = lvStudentRecords.Items[selectionindex];
        int idno = Convert.ToInt32((item.FindControl("hidIdNo") as HiddenField).Value.Trim());
        int Semesterno = Convert.ToInt32((item.FindControl("hdfsem") as HiddenField).Value.Trim());
        int Degreeno = Convert.ToInt32((item.FindControl("hdfdegreeno") as HiddenField).Value.Trim());
        int branchno = Convert.ToInt32((item.FindControl("hdfbranchno") as HiddenField).Value.Trim());
        int prev_status = Convert.ToInt32((item.FindControl("hdfprev_status") as HiddenField).Value.Trim());


        //ShowReport(idno, Semesterno, Degreeno, branchno, prev_status,examno, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");
       // ShowReport(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Semesterno), Degreeno, branchno,col, Convert.ToInt32(prev_status),examno, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");

    }
    protected void lvStudentRecords_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Exam_Registered_count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "IDNO=" + Session["idno"].ToString() + "AND SESSIONNO="+ddlSession.SelectedValue+" AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND ISNULL(EXAM_REGISTERED,0)=1 AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0");
        //if (Convert.ToInt32(Exam_Registered_count) == 0)
        //{
        //    objCommon.DisplayMessage(this.updAdmit, "Your Exam Registration is yet not Done. Kindly connect With Examination Department.", this.Page);
        //    lvStudentRecords.DataSource = null;
        //    lvStudentRecords.DataBind();
        //    pnlStudent.Visible = false;
        //    btnPrint.Visible = false;
        //}
        //else
        //{
        //    BindListView();
        //}
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //********************************NEW******************************

        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    if (ddlCollege.SelectedIndex > 0)
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                        ddlSession.Focus();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                    }

                }
            }
            else
            {

                cler();
            
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamDate.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        //*****************************END*******************************
       
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        }
        else {
           // ddlCollege.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            rdoDegree.SelectedValue = null;
            btnsubmit.Enabled = false;
            lvStudentRecords.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
           #region CHECK BOX COUNT
             int cntcourse = 0;
            if (lvStudentRecords.Items.Count > 0)
            {

                foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true)
                        cntcourse++;
                }

            }
            if (cntcourse == 0)
            {
                objCommon.DisplayMessage(this,"Please Select Students..!!", this.Page);
                return;
            }
            #endregion


            foreach (ListViewDataItem dataitem in lvStudentRecords.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;

                if (chk.Checked == true)
                {
                    int idnos = 0; string courseno = string.Empty; int sem;
                    Label idno = dataitem.FindControl("lblstudname") as Label;
                    Label semester = dataitem.FindControl("lblsem") as Label;
                    Label lblCCode = dataitem.FindControl("lblccode") as Label;
                    idnos = Convert.ToInt32(idno.ToolTip);
                    courseno = lblCCode.ToolTip;// +",";
                    // courseno = "3683. 3684. 3685. 3686";
                    sem = Convert.ToInt32(semester.ToolTip);

                    string SP_Name = "PKG_UPDATE_EXAM_REGISTRATION_STUDENT_BYADMIN";
                    string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO,@P_COURSENO,@P_SCHEMENO,@P_UANO,@P_OUT";
                    string Call_Values = "" + Convert.ToInt32(idnos) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(sem) + "," + courseno + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Session["userno"] + ",0";
                    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                    if (que_out == "1")
                    {
                        objCommon.DisplayMessage(this, "Course Registration Done Sucessfully", this.Page);
                        BindListView();
                    }


                }



                //else
                //{

                //    objCommon.DisplayMessage(this, "NO STUDENT FOUND", this.Page);

                //}
            }
    }
    protected void cler()
    {

        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        rdoDegree.SelectedValue = null;
        btnsubmit.Enabled = false;
        lvStudentRecords.Visible = false;
    
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {

        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        rdoDegree.SelectedValue = null;
        btnsubmit.Enabled = false;
        lvStudentRecords.Visible = false;
       //   lvStudentRecords.DataBind();
    }


    protected void lnkbtnPrint_Click(object sender, EventArgs e)
    {
       
        
        
        
       LinkButton lbtn = (LinkButton)(sender);
       Session["idno"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[0]);    
      // ShowReport("CourseRegistration", "rptExam_registrationStudent_UTKAL.rpt");




        if (Convert.ToInt32(Session["OrgId"]) == 9)
        {
            ShowReport("CourseRegistration", "rptExam_registrationStudent.rpt");
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 8)//MIT
        {
            ShowReport("CourseRegistration", "rptExam_registrationStudent_MIT.rpt");
        }
        //
        else if (Convert.ToInt32(Session["OrgId"]) == 7)//RAJAGIRI
        {
            ShowReport("CourseRegistration", "rptExam_registrationStudent_Rajagiri.rpt");
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 6)//RCPIPER
        {
            ShowReport("CourseRegistration", "rptExam_registrationStudent_RCPIPER.rpt");
          
           // HideClm();
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 17) //UTKAL
        {
            ShowReport("CourseRegistration", "rptExam_registrationStudent_UTKAL.rpt");
        }

        else if (Convert.ToInt32(Session["OrgId"]) == 19 || Convert.ToInt32(Session["OrgId"]) == 20) //PRIYADARSHNAI
        {
            ShowReport("CourseRegistration", "rptExam_registrationStudent_PCEN.rpt");
        }
        else {


              ShowReport("CourseRegistration", "rptExam_registrationStudent_MIT.rpt");
        }
        
 //   }










    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = 0;


        int idno = Convert.ToInt32(Session["idno"]);
        try
        {

            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int Collegeid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Collegeid + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + Convert.ToInt32(degreeno) + ",@P_BRANCHNO=" + Convert.ToInt32(branchno) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
            //  tring call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
          //  ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



}