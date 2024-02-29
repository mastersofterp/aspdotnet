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
using System.Data.SqlClient;

public partial class ACADEMIC_AdminExamRegDec_Approval : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    StudentController studCont = new StudentController();
    Exam objExam = new Exam();
        StudentRegistration objSReg = new StudentRegistration();
        private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
          
            //.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

           objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_ID");
          
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
            DataSet ds = null;

            ds = GetExamRegStud_Discipline(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue));
            
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               
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
        protected void lvStudentRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string examno = objCommon.LookUp("ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNO = " + ddlExamname.SelectedValue + "");
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

        #region Added by  Hitesh
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
              
            }
            else
            {
                // ddlCollege.SelectedIndex = 0;
                ddlCollege.SelectedIndex = 0;
                ddlSession.SelectedIndex = 0;

                btnsubmit.Enabled = false;
                lvStudentRecords.Visible = false;
            }
            lvStudentRecords.Visible = false;
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        #endregion










    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //********************************NEW******************************

        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
               // DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

               // if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                //{
                   // ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                   // ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                   // ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                   // ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    if (ddlCollege.SelectedIndex > 0)
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO DESC");
                        ddlSession.Focus();
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                    }

                     
           // ddlCollege.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
           
            btnsubmit.Enabled = false;
            lvStudentRecords.Visible = false;
        
        lvStudentRecords.Visible = false;

            //    }
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
           // objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH A WITH (NOLOCK) ", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND A.BRANCHNO = " + Convert.ToInt32(ViewState["branchno"]), "A.LONGNAME");
           // objCommon.FillDropDownList(ddlbranch, "ACD_STUDENT  S INNER JOIN ACD_STUDENT_RESULT SR ON (S.IDNO=SR.IDNO ) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO  AND ISNULL(ACTIVESTATUS,0)=1) ", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND ISNULL(ADMCAN,0)=0 and COLLEGE_ID="+Convert.ToInt32(ddlCollege.SelectedValue) +" AND   SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue) , "B.LONGNAME");
            objCommon.FillDropDownList(ddlDegree, "ACD_STUDENT  S INNER JOIN ACD_STUDENT_RESULT SR ON (S.IDNO=SR.IDNO ) INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO  AND ISNULL(ACTIVESTATUS,0)=1) ", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", " ISNULL(ADMCAN,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND   SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "D.DEGREENAME");

            

           // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
          

        }
        else {
           // ddlCollege.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlbranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
           
            btnsubmit.Enabled = false;
            lvStudentRecords.Visible = false;
        }
        lvStudentRecords.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
         objCommon.FillDropDownList(ddlbranch, "ACD_STUDENT  S INNER JOIN ACD_STUDENT_RESULT SR ON (S.IDNO=SR.IDNO ) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO  AND ISNULL(ACTIVESTATUS,0)=1) ", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND ISNULL(ADMCAN,0)=0 and COLLEGE_ID="+Convert.ToInt32(ddlCollege.SelectedValue) +" AND   SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue) +" AND S.DEGREENO="+Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
         objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO) INNER JOIN  ACD_STUDENT SS  ON (SS.IDNO=SR.IDNO ) ", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SS.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "S.SEMESTERNO");
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedIndex > 0)
        {
            // objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH A WITH (NOLOCK) ", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND A.BRANCHNO = " + Convert.ToInt32(ViewState["branchno"]), "A.LONGNAME");
            //objCommon.FillDropDownList(ddlbranch, "ACD_STUDENT  S INNER JOIN ACD_STUDENT_RESULT SR ON (S.IDNO=SR.IDNO ) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO  AND ISNULL(ACTIVESTATUS,0)=1) ", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND ISNULL(ADMCAN,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND   SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "B.LONGNAME");


            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO) INNER JOIN  ACD_STUDENT SS  ON (SS.IDNO=SR.IDNO ) ", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND BRANCHNO =" + ddlbranch.SelectedValue +"AND SS.DEGREENO="+Convert.ToInt32(ddlDegree.SelectedValue), "S.SEMESTERNO");


        }
        else
        {
            // ddlCollege.SelectedIndex = 0;
           // ddlSession.SelectedIndex = 0;
        //    ddlSemester.SelectedIndex = 0;

            btnsubmit.Enabled = false;
            lvStudentRecords.Visible = false;
        }
        lvStudentRecords.Visible = false;
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
                    if (chk.Checked == true && chk.Enabled==true)
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
                if (chk.Enabled == true)
                {

                    int flag = 0;
                    if (chk.Checked == true)
                    {
                        flag = 1;
                    }

                    int idnos = 0; string courseno = string.Empty; int sem; 
                    //string DISCIPLINE_REMARK = string.Empty;
                    Label idno = dataitem.FindControl("lblstudname") as Label;
                    Label semester = dataitem.FindControl("lblsem") as Label;
                    Label lblCCode = dataitem.FindControl("lblccode") as Label;
                    Label lblregno = dataitem.FindControl("REGNO") as Label;
                   // Label lblflag = dataitem.FindControl("FLAG") as Label;
                   
                    idnos = Convert.ToInt32(idno.ToolTip);
                    //courseno = lblCCode.ToolTip;// +",";
                    // courseno = "3683. 3684. 3685. 3686";
                    //sem = Convert.ToInt32(semester.ToolTip);
                  //  string txt = DISCIPLINE_REMARK.Text.ToString(); ; 

                    string SP_Name = "PKG_UPDATE_EXAM_REGISTRATION_STUDENT_BYADMIN_DISCIPLINE";
                    string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO,@P_FLAG,@P_OUT";
                    string Call_Values = "" + Convert.ToInt32(idnos) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," +  flag + ",0";
                    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                    if (que_out == "1")
                    {
                        objCommon.DisplayMessage(this, "Students Approveed Sucessfully!!", this.Page);
                        BindListView();
                    }
                }
                }               
            }
    protected void cler()
    {
        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        btnsubmit.Enabled = false;
        lvStudentRecords.Visible = false;
        ddlbranch.SelectedIndex = 0;
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {

        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlbranch.SelectedIndex = 0;
      
        btnsubmit.Enabled = false;
        lvStudentRecords.Visible = false;
       
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
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlStudetType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
       
    }
    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
            if (chk.Checked == true)
            {
                chk.BackColor = System.Drawing.Color.Green;
            }
        }
    
    }

    #region ADDED BY GS FOR DECIPLINE APPROVAL
    //ds = GetExamRegStud_Discipline(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue));
    public DataSet GetExamRegStud_Discipline(int semesterno, int session, int branch, int degreeno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_SEMESTERNO", semesterno);
            objParams[1] = new SqlParameter("@P_BRANCHNO", branch);
            objParams[2] = new SqlParameter("@P_SESSIONNO", session);
            objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);


            ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_ELIGIBILITY_REPORT", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    #endregion
    }