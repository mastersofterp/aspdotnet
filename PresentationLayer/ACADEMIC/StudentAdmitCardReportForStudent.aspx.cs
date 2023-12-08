using System;
using System.Data;
using System.Web.UI;
using IITMS;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_StudentAdmitCardReportForStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
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
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        BindListView();
                    }

                    else
                    {
                        objCommon.DisplayUserMessage(this.Page, "You are not authorized to view this page.", this.Page);
                    }

                }
            }
            CheckActivity();
            divMsg.InnerHtml = string.Empty;


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    //private void CheckActivity()
    //{
    //    string sessionno = string.Empty;
    //    sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "ISNULL(SA.SESSION_NO,0)", "AM.ACTIVITY_CODE = 'HallTicket' AND SA.STARTED = 1");
    //    //sessionno = Session["currentsession"].ToString();
    //    sessionno = sessionno == string.Empty ? "-1" : sessionno;
    //    ActivityController objActController = new ActivityController();
    //    DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

    //    if (dtr.Read())
    //    {
    //        if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //        {
    //            objCommon.DisplayMessage(this.Page, "This Activity has been Stopped. Contact Admin.!!", this.Page);
    //            //divExamHalTckt.Visible = false;
    //            // divNote.Visible = false;
    //        }
    //        //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        //if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        //{
    //        //    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //        //    divExamHalTckt.Visible = false;
    //        //    // divNote.Visible = false;
    //        //}
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //        //divExamHalTckt.Visible = false;
    //        //divNote.Visible = false;
    //    }
    //    dtr.Close();
    //    //return true;
    //}

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

        //Added Gaurav 16-11-2022 FOr Check Activity SEMESTERNO,COLLEGE_ID,BRANCHNO,DEGREENO Wise. 
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like'%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "ISNULL(SA.SESSION_NO,0)", "AM.ACTIVITY_CODE = 'HallTicket' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        sessionno = sessionno == string.Empty ? "-1" : sessionno;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.Page, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                divExamHalTckt.Visible = false;
                // divNote.Visible = false;
            }
            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            //if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            //{
            //    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
            //    divExamHalTckt.Visible = false;
            //    // divNote.Visible = false;
            //}
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            divExamHalTckt.Visible = false;
            //divNote.Visible = false;
        }
        dtr.Close();
        //return true;
    }

    private void BindListView()
    {

        DataSet ds = studCont.GetHallTicketDetails(Convert.ToInt32(Session["idno"]));
        if (ds != null && ds.Tables.Count > 0)
        {
            lvHallTicketDetails.DataSource = ds.Tables[0];
            lvHallTicketDetails.DataBind();
            ViewState["SESSIONNO"] = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            ViewState["SEMESTERNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"].ToString());
            //ViewState["EXAMNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"].ToString());
            ViewState["DEGREENO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
            ViewState["BRANCHNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"].ToString());
            ViewState["EXAMNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EXAMNO"].ToString());
            //lvStudentRecords.DataSource = ds.Tables[0];
            //lvStudentRecords.DataBind();
        }
    }

    protected void btnHallTicket_Click(object sender, System.EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "COUNT(*)", "PHOTO IS NULL AND IDNO=" + Session["idno"]));

            if (Count > 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Upload Photo First to Generate Exam Hall Ticket.", this.Page);
                return;
            }
            else
            {
                int Fees_Paid = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "COUNT(*)", "FEES_PAID=1 AND EXAM_REGISTRATION=1"));
                DataSet ds = studCont.AdmfessDues(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SEMESTERNO"]));
                //Count = feeController.AdmfessDues(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ViewState["SEMESTERNO"]));

                if (Fees_Paid == 1)
                {

                    if (ds.Tables.Count > 0)
                    {
                        ViewState["status"] = ds.Tables[0].Rows[0]["DUES"].ToString();

                        // if (ds.Tables[0].Rows[0]["DUES"])
                        if (Convert.ToInt32(ViewState["status"]) == 0)
                        {


                            if (Convert.ToInt32(lnk.CommandName) > 0)
                            {
                                int degreeno = 0;
                                int branchno = 0;
                                int Sessionno = 0;
                                int Semesterno = 0;
                                int Examno = 0;
                                int prevstatus = 0;
                                int sectionno = 0;
                                int schemeno = 0;
                                //string semesterno = Convert.ToString(ViewState["SEMESTERNO"]);
                                //string prev_status = string.Empty;
                                //int idno = Convert.ToInt32(Session["idno"]);

                                //btnPrint.
                                //int A = lvStudentRecords.Items.Count;
                                LinkButton btnHallTicket = (LinkButton)(sender);
                                string semesterno = Convert.ToString(ViewState["SEMESTERNO"]);
                                string prev_status = string.Empty;
                                int idno = Convert.ToInt32(Session["idno"]);
                                //degreeno = Convert.ToInt32(ViewState["DEGREENO"]);
                                branchno = Convert.ToInt32(ViewState["BRANCHNO"]);
                                string examname = string.Empty;


                                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


                                if (semesterno != "")
                                {
                                    //foreach (ListViewDataItem item in lvHallTicketDetails.Items)
                                    //{
                                    idno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hidIdNo") as HiddenField).Value.Trim());
                                    Sessionno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdSessionno") as HiddenField).Value.Trim());
                                    Semesterno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdfsem") as HiddenField).Value.Trim());
                                    degreeno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdfdegreeno") as HiddenField).Value.Trim());
                                    branchno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdfbranchno") as HiddenField).Value.Trim());
                                    //prevstatus = Convert.ToInt32((item.FindControl("hdfprev_status") as HiddenField).Value.Trim());
                                    Examno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdexamno") as HiddenField).Value.Trim());
                                    examname = (lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdExamName") as HiddenField).Value.Trim();
                                    sectionno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdSection") as HiddenField).Value.Trim());
                                    schemeno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdSchemeno") as HiddenField).Value.Trim());

                                    ViewState["Examaname"] = examname;


                                    //ShowReport(idno, Semesterno, Degreeno, branchno, prev_status, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");
                                    //}

                                    int College_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + idno));
                                    string examno = objCommon.LookUp("ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNO = " + Convert.ToInt32(Examno) + "");
                                    prev_status = objCommon.LookUp("ACD_STUDENT_RESULT", "TOP(1) PREV_STATUS", "SESSIONNO=" + Convert.ToInt32(Sessionno) + " AND IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(Semesterno) + "");
                                    int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(COUNT(CCODE),0)", "SESSIONNO=" + Convert.ToInt32(Sessionno) + " AND IDNO=" + idno + " AND ISNULL(EXAM_REGISTERED,0)=1 AND ISNULL(REGISTERED,0)=1 AND ISNULL(DETAIND,0)=0"));
                                    //int count = Convert.ToInt32(objCommon.LookUp("ACD_NODUES_STATUS", "COUNT(1)", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "AND NODUES_StATUS=1"));
                                    if (count > 0)
                                    {
                                        // Add 09092022
                                        if (Convert.ToInt32(Session["OrgId"]) == 9)  //For Atlas
                                        {

                                            ShowReport_Atlas(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, schemeno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_atlas.rpt");

                                        }
                                        else if ((Convert.ToInt32(Session["OrgId"]) == 7)) //For Rajagiri
                                        {

                                            ShowReport_Rajagiri((Convert.ToInt32(Session["idno"])), Sessionno, Convert.ToInt32(Semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, schemeno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Rajagiri.rpt");

                                            //ShowReport_Rajagiri((Convert.ToInt32(Session["idno"])), Convert.ToInt32(semesterno), Convert.ToInt32(ViewState["DEGREENO"]),Convert.ToInt32(ViewState["BRANCHNO"]), College_id, Convert.ToInt32(prev_status), examno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Rajagiri.rpt");

                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 3)//for CPUK CLIENT
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPU.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 4)//for CPUH CLIENT
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPUH.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 8) //for MIT CLIENT
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_MIT.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 1) //for RCPIT
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_RCPIT.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 6) //for RCPIPER
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_RCPIPER.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 2) //for CRESCENT
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CRESCENT.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 16)//for Maher
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Maher.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 18) //HITS  Added By Injamam 29_11_2023 
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_HITS.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 19)  //PCEN   Added By Injamam 20_10_2023
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_PCEN.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 20)  //PJLCOE   Added By Injamam 30_11_2023
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_PJLCEN.rpt");
                                        }

                                        else if (Convert.ToInt32(Session["OrgId"]) == 21) //TGPCET Added By Injamam 29_11_2023 
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_TGPCET.rpt");
                                        }
                                        else if (Convert.ToInt32(Session["OrgId"]) == 5) //JECRC Added By Injamam 29_11_2023 
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_JECRC.rpt");
                                        }
                                        else
                                        {
                                            ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");
                                        }

                                        int chkg = studCont.InsAdmitCardLogStudent(Convert.ToInt32(degreeno), Convert.ToInt32(branchno), (Session["idno"].ToString()) + ".", ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), "Student Login", Convert.ToInt32(Sessionno), Convert.ToInt32(Semesterno), Convert.ToDateTime(DateTime.Now));
                                        if (chkg == 2)
                                        {
                                            objCommon.DisplayMessage("Admit Card Successfully Generated", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        //objCommon.DisplayMessage(updAdmit,"No Dues are Pending.", this.Page);
                                        objCommon.DisplayMessage(updAdmit, "Your Exam Form is not Confirmed yet,Kindly Contact to your Department.", this.Page);
                                        pnHallTicKet.Visible = false;
                                        //pnlStudent.Visible = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage("Please Select Exam Names", this.Page);

                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "You have outstanding amount. Clear the dues to download the Hall Ticket.", this.Page);
                            return;
                        }

                    }
                }
                else
                {
                    if (Convert.ToInt32(lnk.CommandName) > 0)
                    {
                        int degreeno = 0;
                        int branchno = 0;
                        int Sessionno = 0;
                        int Semesterno = 0;
                        int Examno = 0;
                        int sectionno = 0;
                        int prevstatus = 0;
                        int schemeno = 0;
                        //string semesterno = Convert.ToString(ViewState["SEMESTERNO"]);
                        //string prev_status = string.Empty;
                        //int idno = Convert.ToInt32(Session["idno"]);

                        //btnPrint.
                        //int A = lvStudentRecords.Items.Count;
                        LinkButton btnHallTicket = (LinkButton)(sender);
                        string semesterno = Convert.ToString(ViewState["SEMESTERNO"]);
                        string prev_status = string.Empty;
                        int idno = Convert.ToInt32(Session["idno"]);
                        //degreeno = Convert.ToInt32(ViewState["DEGREENO"]);
                        branchno = Convert.ToInt32(ViewState["BRANCHNO"]);
                        string examname = string.Empty;

                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];


                        if (semesterno != "")
                        {
                            //foreach (ListViewDataItem item in lvHallTicketDetails.Items)
                            //{
                            idno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hidIdNo") as HiddenField).Value.Trim());
                            Sessionno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdSessionno") as HiddenField).Value.Trim());
                            Semesterno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdfsem") as HiddenField).Value.Trim());
                            degreeno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdfdegreeno") as HiddenField).Value.Trim());
                            branchno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdfbranchno") as HiddenField).Value.Trim());
                            //prevstatus = Convert.ToInt32((item.FindControl("hdfprev_status") as HiddenField).Value.Trim());
                            Examno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdexamno") as HiddenField).Value.Trim());
                            examname = (lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdExamName") as HiddenField).Value.Trim();
                            sectionno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdSection") as HiddenField).Value.Trim());
                            schemeno = Convert.ToInt32((lvHallTicketDetails.Items[Convert.ToInt32(btnHallTicket.ToolTip) - 1].FindControl("hdSchemeno") as HiddenField).Value.Trim());

                            ViewState["Examaname"] = examname;


                            //ShowReport(idno, Semesterno, Degreeno, branchno, prev_status, "Student_Admit_Card_Report", "rptBulkExamRegslip.rpt");
                            //}

                            int College_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(COLLEGE_ID,0)", "IDNO=" + idno));
                            string examno = objCommon.LookUp("ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNO = " + Convert.ToInt32(Examno) + "");
                            prev_status = objCommon.LookUp("ACD_STUDENT_RESULT", "TOP(1) PREV_STATUS", "SESSIONNO=" + Convert.ToInt32(Sessionno) + " AND IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(Semesterno) + "");
                            int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(COUNT(CCODE),0)", "SESSIONNO=" + Convert.ToInt32(Sessionno) + " AND IDNO=" + idno + " AND ISNULL(EXAM_REGISTERED,0)=1 AND ISNULL(REGISTERED,0)=1 AND ISNULL(DETAIND,0)=0"));
                            //int count = Convert.ToInt32(objCommon.LookUp("ACD_NODUES_STATUS", "COUNT(1)", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "AND NODUES_StATUS=1"));
                            if (count > 0)
                            {
                                // Add 09092022
                                if (Convert.ToInt32(Session["OrgId"]) == 9)  //For Atlas
                                {

                                    ShowReport_Atlas(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, schemeno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_atlas.rpt");

                                }
                                else if ((Convert.ToInt32(Session["OrgId"]) == 7)) //For Rajagiri
                                {

                                    ShowReport_Rajagiri((Convert.ToInt32(Session["idno"])), Sessionno, Convert.ToInt32(Semesterno), degreeno, branchno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, schemeno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Rajagiri.rpt");

                                    //ShowReport_Rajagiri((Convert.ToInt32(Session["idno"])), Convert.ToInt32(semesterno), Convert.ToInt32(ViewState["DEGREENO"]),Convert.ToInt32(ViewState["BRANCHNO"]), College_id, Convert.ToInt32(prev_status), examno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Rajagiri.rpt");

                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 3)//for CPUK CLIENT
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPU.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 4)//for CPUH CLIENT
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CPUH.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 8) //for MIT CLIENT
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_MIT.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 1) //for RCPIT
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_RCPIT.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 6) //for RCPIPER
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_RCPIPER.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 2) //for CRESCENT
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_CRESCENT.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 16)//for Maher
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_Maher.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 19)  //PCEN   Added By Injamam 20_10_2023
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_PCEN.rpt");
                                }
                                else if (Convert.ToInt32(Session["OrgId"]) == 20)  //PJLCOE   Added By Injamam 30_11_2023
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_PJLCEN.rpt");
                                }

                                else if (Convert.ToInt32(Session["OrgId"]) == 21) //TGPCET Added By Injamam 29_11_2023 
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket_TGPCET.rpt");
                                }
                                else
                                {
                                    ShowReport(Convert.ToInt32(Session["idno"]), Sessionno, Convert.ToInt32(Semesterno), degreeno, schemeno, College_id, Convert.ToInt32(prev_status), Examno, sectionno, "Student_Admit_Card_Report", "rptBulkExamHallTicket.rpt");
                                }

                                int chkg = studCont.InsAdmitCardLogStudent(Convert.ToInt32(degreeno), Convert.ToInt32(branchno), (Session["idno"].ToString()) + ".", ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]), "Student Login", Convert.ToInt32(Sessionno), Convert.ToInt32(Semesterno), Convert.ToDateTime(DateTime.Now));
                                if (chkg == 2)
                                {
                                    objCommon.DisplayMessage("Admit Card Successfully Generated", this.Page);
                                }
                            }
                            else
                            {
                                //objCommon.DisplayMessage(updAdmit,"No Dues are Pending.", this.Page);
                                objCommon.DisplayMessage(updAdmit, "Your Exam Form is not Confirmed yet,Kindly Contact to your Department.", this.Page);
                                pnHallTicKet.Visible = false;
                                //pnlStudent.Visible = false;
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Select Exam Names", this.Page);

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }

    private void ShowReport(int param, int Sessionno, int Semesterno, int Degreeno, int schemeno, int College_id, int prev_status, int Examno, int Sectionno, string reportTitle, string rptFileName)
    {
        try
        {
            //string Examname = objCommon.LookUp(" ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", " ED.FLDNAME IN('EXTERMARK') AND EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " AND S.DEGREENO= " + Convert.ToInt32(ViewState["DEGREENO"]));
            //string Examno = objCommon.LookUp(" ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", " ED.FLDNAME IN('EXTERMARK') AND EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + " AND S.DEGREENO= " + Convert.ToInt32(ViewState["DEGREENO"]));
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +
            url += "&param=@P_COLLEGE_CODE=" + College_id +
                ",@P_IDNO=" + param +
                ",@P_BRANCHNO=" + schemeno +
                ",@P_DEGREENO=" + Degreeno +
                ",@P_SEMESTERNO=" + Semesterno +
                ",@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
                ",@Examname=" + ViewState["Examaname"] +
                ",@P_EXAMNO=" + Examno +
                ",@P_SECTIONNO=" + Sectionno +
                ",@P_COLLEGE_ID=" + College_id +
                // ",@P_PREV_STATUS=" + prev_status + 
                ",@P_USER_FUll_NAME=" + Session["username"];

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += "</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport_Atlas(int param, int Sessionno, int Semesterno, int Degreeno, int branchno, int College_id, int prev_status, int Examno, int Sectionno, int Schemeno, string reportTitle, string rptFileName)
    {
        try
        {

            //string Examno = objCommon.LookUp(" ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", " ED.FLDNAME IN('EXTERMARK') AND EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(branchno) + " AND S.DEGREENO= " + Convert.ToInt32(ViewState["DEGREENO"]));
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + College_id.ToString() +
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(College_id) +
           ",@P_IDNO=" + param +
           ",@P_BRANCHNO=" + branchno +
           ",@P_DEGREENO=" + Degreeno +
           ",@P_SEMESTERNO=" + Semesterno +
           ",@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
           ",@Examname=" + ViewState["Examaname"] +
           ",@P_EXAMNO=" + Examno +
           ",@P_SECTIONNO=" + Sectionno +
           ",@P_SCHEMENO=" + Schemeno +
           ",@P_COLLEGE_ID=" + College_id +
                // ",@P_PREV_STATUS=" + prev_status + 
           ",@P_USER_FUll_NAME=" + Session["username"];

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += "</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowReport_Rajagiri(int param, int Sessionno, int Semesterno, int Degreeno, int branchno, int College_id, int prev_status, int Examno, int Sectionno, int Schemeno, string reportTitle, string rptFileName)
    {
        try
        {

            //string Examno = objCommon.LookUp(" ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", " ED.FLDNAME IN('EXTERMARK') AND EXAMNAME<>'' AND S.BRANCHNO=" + Convert.ToInt32(branchno) + " AND S.DEGREENO= " + Convert.ToInt32(ViewState["DEGREENO"]));
            //int sessionno = Convert.ToInt32(Session["currentsession"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + College_id.ToString() +
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(College_id) +
           ",@P_IDNO=" + param +
           ",@P_BRANCHNO=" + branchno +
           ",@P_DEGREENO=" + Degreeno +
           ",@P_SEMESTERNO=" + Semesterno +
           ",@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
           ",@Examname=" + ViewState["Examaname"] +
           ",@P_EXAMNO=" + Examno +
           ",@P_SECTIONNO=" + Sectionno +
           ",@P_SCHEMENO=" + Schemeno +
           ",@P_COLLEGE_ID=" + College_id +
                // ",@P_PREV_STATUS=" + prev_status + 
           ",@P_USER_FUll_NAME=" + Session["username"];

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += "</script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentIDCardReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}