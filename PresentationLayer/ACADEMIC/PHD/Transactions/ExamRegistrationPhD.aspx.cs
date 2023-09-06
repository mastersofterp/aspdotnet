//======================================================================================
// PROJECT NAME  : RFC-COMMON                                                                
// MODULE NAME  : ACADEMIC
// PAGE NAME        : EXAM REGISTRATION                                      
// ADDED DATE      : 23-07-2022
// ADDED BY          : NIKHIL LAMBE                                      
// ADDED DATE     : 
// MODIFIED BY    : 
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
using System.Net;
using System.IO;
using AddressFamily = System.Net.Sockets.AddressFamily;
public partial class ACADEMIC_ExamRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    string oddeven = string.Empty;
    StudentController objStudent = new StudentController();
    string Sessionexam = string.Empty;

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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
               // this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;


                //---- add for  check max session and  exam fess ---// -- dipali -- 27022018
                Sessionexam = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'examregphd' AND STARTED=1");
                if (Sessionexam != "")
                {
                    ViewState["Sessionexam"] = Sessionexam;
                }
                else
                {
                    ViewState["Sessionexam"] = Session["currentsession"].ToString();
                }
                // check for exam fees --//
                int flag = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEES", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ViewState["Sessionexam"].ToString())));
                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    CheckActivity();
                    //if (flag > 0)
                    //{

                    //    // CheckActivity();
                    //    divCourses.Visible = false;
                    //    pnlSearch.Visible = false;
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage("Exam Fees is not define yet please define fees first..!", this.Page);
                    //    pnlStart.Visible = false;
                    //    return;
                    //}
                    Panellistview.Visible = false;
                    divCourses.Visible = false;
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //if (flag > 0)
                    //{
                        CheckActivity();
                        divNote.Visible = false;
                        //pnlSearch.Visible = true;
                        Panellistview.Visible = true;
                        this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                    //}
                    //else
                    //{
                        //objCommon.DisplayMessage("Exam Fees is not define yet please define fees first..!", this.Page);
                    //    pnlStart.Visible = false;
                    //    return;
                    //}

                }
                else
                {
                    pnlStart.Enabled = false;
                }
                
                btnSubmit.Visible = false;
                btnReport.Visible = false;
                string IPADDRESS = string.Empty;

             
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        divMsg.InnerHtml = string.Empty;
    }



    private void CheckActivity()
    {
        string sessionno = string.Empty;

        sessionno = ViewState["Sessionexam"].ToString();
        Sessionexam = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'examregphd' AND STARTED=1");
        if (Sessionexam == "")
        {
            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
            pnlStart.Visible = false;
        }


       
    }
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panellistview.Visible = true;
            // divReceiptType.Visible = false;
            //  divStudSemester.Visible = false;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    private void FillDropdown()
    {
        

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSubmit.Enabled = false;
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }

        //************************************
        int previous = Convert.ToInt32(objCommon.LookUp("acd_student_result", "prev_status", "sessionno=(select max(sessionno) from acd_student_result where semesterno='" + ddlBackLogSem.SelectedValue + "' and idno='" + idno + "')" + "AND IDNO=" + idno + "and semesterno=" + ddlBackLogSem.SelectedValue));
        ViewState["previous"] = previous;
        //************************************

        string schemeno = objCommon.LookUp("(SELECT TOP(1) SCHEMENO FROM ACD_STUDENT_RESULT T WHERE  IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_STUDENT_RESULT WHERE SEMESTERNO = T.SEMESTERNO AND IDNO = T.IDNO))A", "SCHEMENO", "");
        string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
       
        objSR.SESSIONNO = Convert.ToInt32(ViewState["Sessionexam"].ToString());
        objSR.IDNO = idno;
        objSR.REGNO = Regno;
        objSR.SCHEMENO = Convert.ToInt32(schemeno);
        objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        objSR.IPADDRESS = ViewState["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"]);
        objSR.COURSENOS = string.Empty;
        int status = 0;
        int dstatus = 0;


        if (lvFailCourse.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true)
                    status++;
            }
        }
        else
        {
            status = -1;
        }


        if (status > 0)
        {
          

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                }
            }

            CustomStatus cs = new CustomStatus();
            // added for course registration logic
            string IsRegular = objCommon.LookUp("ACD_TRRESULT", "COUNT(*)", "IDNO=" + Convert.ToInt32(objSR.IDNO) + " AND SEMESTERNO=" + objSR.SEMESTERNO + "");
            if (IsRegular == "")
                IsRegular = "0";

            if ((ddlBackLogSem.SelectedValue == "1" || ddlBackLogSem.SelectedValue == "2" || ddlBackLogSem.SelectedValue == "3") && Convert.ToInt32(IsRegular) == 0)
            {
                cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsPhD(objSR);
            }
            else if ((ddlBackLogSem.SelectedValue == "1" || ddlBackLogSem.SelectedValue == "2" || ddlBackLogSem.SelectedValue == "3") && Convert.ToInt32(IsRegular) > 0)
            {
                cs = (CustomStatus)objSRegist.AddExamRegisteredSubjects(objSR);
            }

            if (cs == CustomStatus.RecordSaved)
            {
                if (previous == 0)
                {
                    objCommon.DisplayMessage("Student Exam Registered Successfully!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage("Your Exam Registration has been done.Now Collect the Fee Challan and Paid the fees and Your Exam Registration will be successfull after final confirmation from HOD !!", this.Page);
                }
                divReceipts.Visible = true;

            }

        }

       

        if (ViewState["lvFailInaggre"].ToString() == "1")
        {
            if (lvFailInaggre.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailInaggre.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAcceptSub") as CheckBox;
                    if (chk.Checked == true)
                        dstatus++;
                }

            }
            else
            {
                dstatus = -1;
            }


            if (dstatus > 0)
            {                
                foreach (ListViewDataItem dataitem in lvFailInaggre.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkAcceptSub") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    }
                }

                CustomStatus cs = new CustomStatus();
                if (ddlBackLogSem.SelectedValue == "1")
                {
                    cs = (CustomStatus)objSRegist.AddExamRegisteredSubjects(objSR);
                }
                else
                {
                    cs = (CustomStatus)objSRegist.AddExamRegisteredSubjectsPhD(objSR);
                }
                if (cs == CustomStatus.RecordSaved)
                {
                    if (Convert.ToInt32(lblAdmBatch.ToolTip.ToString()) >= 30 && previous == 0)
                    {
                        objCommon.DisplayMessage("Student Exam Registered Successfully!!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Your Exam Registration has been done.Now Collect the Fee Challan and Paid the fees and Your Exam Registration will be successfull after final confirmation from HOD !!", this.Page);
                    }

                    int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT(SCHEMENO)", "IDNO = " + objSR.IDNO + " AND SEMESTERNO = " + objSR.SEMESTERNO));
                    int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));
                }
            }
        }

        if (Convert.ToInt32(lblAdmBatch.ToolTip.ToString()) < 30 || previous != 0)
        {
            //COUNT THE HOW MANY SUBJECTS
            status = dstatus + status + 1;
            if (status > 0)
            {

                int branchno = Convert.ToInt32(lblBranch.ToolTip);
                int admbatch = Convert.ToInt32(lblAdmBatch.ToolTip);
                int degreeno = Convert.ToInt32(hdfDegreeno.Value);
                int categoryno = Convert.ToInt32(hdfCategory.Value);
                int SubCount;
                if (categoryno == 0)
                {
                    categoryno = 4;
                }
                if (status > 3)
                {
                    SubCount = 2;
                }
                else
                {
                    SubCount = 1;
                }
                int semesterno = Convert.ToInt32(lblSemester.ToolTip);
                //double ExamAmt = Convert.ToDouble(objCommon.LookUp("ACD_EXAM_FEES", "AMOUNT", "DEGREENO='" + degreeno + "' AND CATEGORYNO='" + categoryno + "' AND SUB_LIMIT_NO='" + SubCount + "' AND SESSIONNO=" + objSR.SESSIONNO));
                ///Added by Nikhil Lambe on 25/07/2022 for dummy entry of exam amount.
                double ExamAmt = 1.00;
                int studentIDs = idno;

                bool overwriteDemand = true;

                string receiptno = this.GetNewReceiptNo();
                FeeDemand dcr = this.GetDcrCriteria();

                ///new code for late fees 03/04/2013
                DateTime today = DateTime.Now.Date;
                DateTime LastExamdate = Convert.ToDateTime(objCommon.LookUp("REFF", "Exam_Last_Date", "")).Date;
                double CalLateExmAmt;

                int day = Convert.ToInt32((today - LastExamdate).TotalDays);
                if (day > 0)
                {
                    //This code for the not count the sunday and saturday.
                    int holidayCount = 0;

                    for (DateTime dt = LastExamdate; dt < today; dt = dt.AddDays(1.0))
                    {
                        if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                        {
                            holidayCount++;
                        }
                    }

                    day = day - holidayCount;
                    double LateExamAmt = Convert.ToDouble(objCommon.LookUp("REFF", "Exam_Late_Fee_Amt", ""));
                    CalLateExmAmt = 0;
                }
                else
                {
                    CalLateExmAmt = 0;
                }
                string dcritem = dmController.CreateDcrForBacklogStudents(studentIDs, dcr, 1, overwriteDemand, receiptno, ExamAmt, CalLateExmAmt);

                string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + semesterno + " AND SESSIONNO=" + objSR.SESSIONNO);

                DisplayInformation(idno);

            }

            else
            {
                objCommon.DisplayMessage("Please Select atleast one course selected in course list", this.Page);
                btnSubmit.Enabled = true;
            }

        }

    }
    #region

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistrationPhd.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistrationPhd.aspx");
        }
    }
    private void ShowDetails()
    {
        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();
        lvFailInaggre.DataSource = null;
        lvFailInaggre.DataBind();
        btnSubmit.Visible = false;
        btnReport.Visible = false;
        divReceipts.Visible = false;

        int idno = 0;
        int idno1 = 0;

        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSIONNO > 0 AND FLOCK=0  AND EXAMTYPE=1")); //AND ODD_EVEN=1

        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
            idno1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(IDNO)", "IDNO=" + idno + " AND ADMCAN = 0"));
            if (idno1 == 0)
            {
                idno = 0;
            }
            else
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtSearch.Text);
            //idno = feeController.GetStudentIdByEnrollmentNo("7220");
        }
        try
        {
            if (idno > 0)
            {
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        Session["idno"] = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();  ///-- add this line pass idno 
                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                        objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT R LEFT JOIN ACD_TRRESULT T ON R.IDNO=T.IDNO WHERE R.IDNO= " + idno + " AND R.SESSIONNO= (SELECT MAX(SESSIONNO) FROM ACD_STUDENT_RESULT WHERE SEMESTERNO=R.SEMESTERNO AND IDNO=R.IDNO) AND  R.IDNO NOT IN (SELECT IDNO FROM ACD_DETENTION_INFO WHERE IDNO=R.IDNO AND SESSIONNO=R.SESSIONNO AND SEMESTERNO=R.SEMESTERNO) AND R.SEMESTERNO  NOT IN(SELECT SEMESTERNO FROM ACD_TRRESULT WHERE RESULT='P' AND IDNO=" + idno + "AND SEMESTERNO=1)", " DISTINCT R.SEMESTERNO", "DBO.FN_DESC('SEMESTER',R.SEMESTERNO)SEMESTER", "", "R.SEMESTERNO");
                        //objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT R LEFT JOIN ACD_TRRESULT T ON R.IDNO=T.IDNO WHERE R.IDNO= " + 7220 + " AND R.SESSIONNO= (SELECT MAX(SESSIONNO) FROM ACD_STUDENT_RESULT WHERE SEMESTERNO=R.SEMESTERNO AND IDNO=R.IDNO) AND  R.IDNO NOT IN (SELECT IDNO FROM ACD_DETENTION_INFO WHERE IDNO=R.IDNO AND SESSIONNO=R.SESSIONNO AND SEMESTERNO=R.SEMESTERNO) AND R.SEMESTERNO  NOT IN(SELECT SEMESTERNO FROM ACD_TRRESULT WHERE RESULT='P' AND IDNO=" + 7220 + "AND SEMESTERNO=1)", " DISTINCT R.SEMESTERNO", "DBO.FN_DESC('SEMESTER',R.SEMESTERNO)SEMESTER", "", "R.SEMESTERNO");
                        //divCourses.Visible = true;
                        //divPanelSearch.Visible = false;
                        //divReceipts.Visible = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    protected void btnPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = Session["idno"].ToString();
            ImageButton btnPrint = sender as ImageButton;
            if (btnPrint.CommandArgument != string.Empty && idno != string.Empty)
            {
                if (btnPrint.ToolTip == "SEF") //-- this condition for reavlauation 
                {
                    this.ShowReport("FeeCollectionReceiptForExam.rpt", Int32.Parse(btnPrint.CommandArgument), Int32.Parse(idno), "2");
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.btnPrintReceipt_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamRegistrationSlip.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT(SCHEMENO)", "IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
        int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));

        ShowReport("ExamRegistrationSlip", "rptExamRegslipNitPhD.rpt");


    }
 
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            //**************************
            string status = string.Empty;
            //DataSet ds = null;
            //ds = objStudent.RetreiveBlockStudentInfo(txtEnrollno.Text.Trim());
            //bool flag = false;
            //if (ds.Tables[0].Rows.Count <= 0)
            //{
            //    flag = true;
            //}
            //else
            //{
            //    if (ds.Tables[0].Rows[0]["STATUS1"].ToString() == "0")
            //    {
            //        flag = true;
            //    }
            //    else
            //    {
            //        flag = false;
            //    }
            //}
            //*********************

            //if (flag == true)
            //{
                divNote.Visible = false;
                divCourses.Visible = true;
                ShowDetails();
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Student having roll no. " + txtEnrollno.Text.Trim() + " has been blocked because " + ds.Tables[0].Rows[0]["Remark"].ToString(), this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamRegistration.btnProceed_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'examregphd' AND STARTED=1"));

        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEMESTERNO=" + semesterno + ",@UserName=" + Session["username"].ToString();

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

    protected void btnShow_Click1(object sender, EventArgs e)
    {
        try
        {
            //Fail subjects List
            int idno = 0;
            int sessionno = Convert.ToInt32(objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'examregphd' AND STARTED=1"));
            int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            int count = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT  TR INNER JOIN ACD_STUDENT ST ON TR.IDNO = ST.IDNO", "COUNT(TR.IDNO)", "TR.IDNO=" + Convert.ToInt32(Session["idno"]) + "AND TR.SEMESTERNO= 1 AND TR.RESULT='P'"));
            //if(count <0

            StudentController objSC = new StudentController();
            if (ViewState["usertype"].ToString() != "1")
            {
                idno = Convert.ToInt32(Session["idno"]);
                int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + sessionno + " AND SEMESTERNO=" + semesterno + " AND  REGISTERED=1 AND SUBID=1 AND IDNO=" + idno));

                if (accept <= 0)
                {
                    lvFailCourse.Enabled = true;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    lvFailCourse.Enabled = false;
                    btnSubmit.Enabled = false;
                }
            }
            else if (ViewState["usertype"].ToString() == "1")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
                //idno = feeController.GetStudentIdByEnrollmentNo("211371601001");
                //btnSubmit.Visible = false;
            }
            int exreg;


            //*************
            int previous = Convert.ToInt32(objCommon.LookUp("acd_student_result", "prev_status", "sessionno=(select max(sessionno) from acd_student_result where semesterno='" + ddlBackLogSem.SelectedValue + "' and idno='" + idno + "')" + "AND IDNO=" + idno + "and semesterno=" + ddlBackLogSem.SelectedValue));
            ViewState["previous"] = previous;

            if (ViewState["previous"].ToString() == "1")
            {
                exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + sessionno + " AND SEMESTERNO=" + semesterno + " AND PREV_STATUS=1 AND REGISTERED=1 AND SUBID=1 and IDNO=" + idno));

                ViewState["CountRegistered"] = exreg;
            }
            else
            {
                //temp prevstatus

                exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + sessionno + " AND SEMESTERNO=" + semesterno + " AND PREV_STATUS=0 AND REGISTERED=1 AND SUBID=1  AND IDNO=" + idno));
                ViewState["CountRegistered"] = exreg;
            }

            int flag = 0;
            //----------------------------
            if (ViewState["previous"].ToString() == "1")
            {
                if (Convert.ToInt32(ViewState["CountRegistered"]) > 0)
                {
                    objCommon.DisplayMessage("Selected Semester Exam Registration Already Done", this.Page);
                    lvFailCourse.Enabled = false;
                    btnSubmit.Enabled = false;
                    flag = 1;
                }
                else
                    btnReport.Visible = false;
            }
            else
            {
                if (Convert.ToInt32(ViewState["CountRegistered"]) > 0)
                {
                    btnReport.Visible = true;
                    lvFailCourse.Enabled = true;
                    btnSubmit.Enabled = true;
                    flag = 0;
                }
                else
                    btnReport.Visible = false;
            }           
            DataSet dsFailSubjects;
            DataSet dsDetainedStudent = null;

            int maxsessionno = Convert.ToInt32(objCommon.LookUp("acd_student_result", "max(sessionno)", "semesterno='" + ddlBackLogSem.SelectedValue + "' and idno='" + idno + "'"));
            ViewState["maxsession"] = maxsessionno;
            if (ViewState["previous"].ToString() == "1")
            {
                dsFailSubjects = objSC.GetStudentFailExamSubjectsPhD(idno, maxsessionno, semesterno);
                if (dsFailSubjects.Tables[0].Rows.Count > 0)
                {
                    lvFailCourse.DataSource = dsFailSubjects;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = true;
                    divCourses.Visible = true;
                    btnSubmit.Visible = true;
                }
                else
                {
                    lvFailCourse.DataSource = null;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = false;
                    divCourses.Visible = true;

                }
            }
            else
            {
                dsFailSubjects = objSC.GetStudentFailExamSubjectsPhD(idno, Convert.ToInt32(ViewState["maxsession"].ToString()), semesterno);
                if (dsFailSubjects.Tables[0].Rows.Count > 0)
                {
                    lvFailCourse.DataSource = dsFailSubjects;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = true;
                    divCourses.Visible = true;
                    btnSubmit.Visible = true;
                }
                else
                {
                    lvFailCourse.DataSource = null;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = false;
                    divCourses.Visible = true;
                    btnSubmit.Visible = false;
                }
            }

            string check = objCommon.LookUp("ACD_TRRESULT", "count(IDNO)", "SEMESTERNO = " + semesterno + " AND IDNO=" + idno + " AND PASSFAIL='FAIL IN AGGREGATE'");

            if (check != "0")
            {
                dsDetainedStudent = objSC.GetStudentDetained(idno, sessionno, semesterno);

                if (dsDetainedStudent.Tables[0].Rows.Count > 0)
                {
                    lvFailInaggre.DataSource = dsDetainedStudent;
                    lvFailInaggre.DataBind();
                    lvFailInaggre.Visible = true;
                    //----------------------------------------------
                    ViewState["lvFailInaggre"] = "1";
                    //----------------------------------------------
                }
            }
            else
            {
                lvFailInaggre.DataSource = null;
                lvFailInaggre.DataBind();
                lvFailInaggre.Visible = false;
                //----------------------------------------------
                ViewState["lvFailInaggre"] = "0";
                //----------------------------------------------
            }

            // added on 14032016
            //  -- remove exammregister  add register  -- 19112018
            string exam_reg_curr = objCommon.LookUp("acd_student_result", "ISNULL(EXAM_REGISTERED,0)", "sessionno=" + sessionno + " AND SEMESTERNO=" + semesterno + "AND IDNO=" + idno);
            if (exam_reg_curr == string.Empty)
                exam_reg_curr = "0";
            if (Convert.ToInt32(exam_reg_curr) > 0)
            {
                btnReport.Visible = true;
                objCommon.DisplayMessage("Selected Semester Exam Registration Already Done", this.Page);
                lvFailCourse.Enabled = false;
                btnSubmit.Enabled = false;
                flag = 1;
            }
            else
            {
                btnReport.Visible = false;
            }
            DisplayInformation(idno);

            //---  check fees is paid or not  if fees paid then display receipt btn  -- 27022018 -- dipali 
            int flag1 = 0;
            flag1 = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "count(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " and sessionno=" + Convert.ToInt32(ViewState["Sessionexam"].ToString()) + " and SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " and RECIEPT_CODE='SEF' AND RECON=1"));
            if (flag1 == 1)
            {
                DisplayInformation(idno);
                btnReport.Visible = true;
                divReceipts.Visible = true;
            }
            else
            {
                btnReport.Visible = false;
            }

        }
        catch (Exception ex)
        { }
    }

    private void DisplayInformation(int studentId)
    {
        try
        {
            /// Display Previous Receipts Information
            /// Display student paid only Exam receipt information.
            /// These are the receipts(i.e. Exam Fee) paid by the student during 

            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
            DataSet ds = feeController.GetPaidReceiptsExamInfoByStudId(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Bind list view with exam paid receipt data 
                divReceipts.Visible = true;
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
            }
            else
            {
                divHidPreviousReceipts.InnerHtml = "No Supplimentary Exam Receipt Found.<br/>";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamregistrationSlip.DisplayInformation() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //get the new receipt No.
    private string GetNewReceiptNo()
    {
        string receiptNo = string.Empty;

        try
        {
            string demandno = objCommon.LookUp("ACD_DCR", "MAX(DCR_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'examregphd' AND STARTED=1"));
            dcrCriteria.ReceiptTypeCode = "SEF";
            dcrCriteria.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            dcrCriteria.PaymentTypeNo = 1;
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.GetDcrCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrCriteria;
    }

    public string getIPAddress()
    {
        string direction;
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        WebResponse response = request.GetResponse();
        StreamReader stream = new StreamReader(response.GetResponseStream());
        direction = stream.ReadToEnd();
        stream.Close();
        response.Close(); //Search for the ip in the html
        int first = direction.IndexOf("Address: ") + 9;
        int last = direction.LastIndexOf("</b");
        direction = direction.Substring(first, last - first);
        return direction.ToString();
    }

    private bool CheckConnection()
    {
        try
        {
            HttpWebRequest request = WebRequest.Create("http://www.google.com/") as HttpWebRequest;
            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
        catch (Exception)
        {
            return false;
        }
    }
     protected void btnSearch_Click(object sender, EventArgs e)
    {
       //// Panel1.Visible = true;
       // lblNoRecords.Visible = true;
       // //divbranch.Attributes.Add("style", "display:none");
       // //divSemester.Attributes.Add("style", "display:none");
       // //divtxt.Attributes.Add("style", "display:none");
       // string value = string.Empty;
       // if (ddlDropdown.SelectedIndex > 0)
       // {
       //     value = ddlDropdown.SelectedValue;
       // }
       // else
       // {
       //     value = txtSearch.Text;
       // }

       // //ddlSearch.ClearSelection();

       // bindlist(ddlSearch.SelectedItem.Text, value);
       // ddlDropdown.ClearSelection();
       // txtSearch.Text = string.Empty;
       //// div_Studentdetail.Visible = false;
       // //divMSG.Visible = false;
       // //btnPayment.Visible = false;
       //// btnReciept.Visible = false;
       //// divPreviousReceipts.Visible = false;
       // //if (value == "BRANCH")
       // //{
       // //    divbranch.Attributes.Add("style", "display:block");

       // //}
       // //else if (value == "SEM")
       // //{
       // //    divSemester.Attributes.Add("style", "display:block");
       // //}
       // //else
       // //{
       // //    divtxt.Attributes.Add("style", "display:block");
       // //}

       // //ShowDetails();
       // Panel3.Visible = true;

        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        
    }

//-------------------------------------------------------------
 protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;


                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
        txtSearch.Text = string.Empty;
    }
//-------------------------------------------------------------
protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ClearControl();
        Response.Redirect(Request.Url.ToString());
    }
protected void lnkId_Click(object sender, EventArgs e)
{
    LinkButton lnk = sender as LinkButton;
    //string url = string.Empty;
    //if (Request.Url.ToString().IndexOf("&id=") > 0)
    //    url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
    //else
    //    url = Request.Url.ToString();

    Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

    Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
    Session["stuinfofullname"] = lnk.Text.Trim();

    //if (lnk.CommandArgument == null)
    //{
    //    string number = Session["StudId"].ToString();
    //    Session["stuinfoidno"] = Convert.ToInt32 (number);
    //}
    //else
    //{
    Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
    //}
    Session["idno"] = Session["stuinfoidno"].ToString();

    //DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

    //Server.Transfer("PersonalDetails.aspx", false);
    // DisplayInformation(Convert.ToInt32(Session["stuinfoidno"]));
    lvStudent.Visible = false;
    lvStudent.DataSource = null;
    lblNoRecords.Visible = false;
    //divmain.Visible = true;

    ShowDetails();
    btnShow.Visible = true;
    divCourses.Visible = true;
    //divmain.Visible = true;
    //DivSutLog.Visible = true;
    //divGeneralInfo.Visible = true;
    updEdit.Visible = false;
    Panellistview.Visible = false;

}

}

