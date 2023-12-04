//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADAMIC
// PAGE NAME     : EXAM_REGISTRATION.ASPX                                                   
// CREATION DATE : 13-APRIL-2019                                                       
// CREATED BY    : GAURAV SONPAROTE                                                     
// MODIFIED DATE :  GAURAV SONPAROTE
// MODIFIED DESC : ADDED THE BILLDESK PAYMENT GATEWAY INTEGRATION & SAVING THE REGULAR,BACKLOG & REDO COURSES.
//=======================================================================================
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
using System.Net;
using System.IO;
using Dns = System.Net.Dns;
using AddressFamily = System.Net.Sockets.AddressFamily;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Security.Cryptography;

public partial class Academic_Examination_ExamRegistration : System.Web.UI.Page
{

    #region Class
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    Student_Acd objSA = new Student_Acd();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentFees objStudentFees = new StudentFees();
    StudentController objSC = new StudentController();
    bool IsNotActivitySem = false;
    bool flag = true;
    #endregion

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

                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 and college_id=" + ddlclgname.SelectedValue); // ADD BY ROSHAN PANNASE 
                Session["payactivityno"] = "13";  // Substitute Exam live and uat/test
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();



                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                // LATE FEE PATCH BY NARESH BEERLA ON DT 18062022 FOR USING IN JAVASCRIPT 
                DateTime ExamLateDate = Convert.ToDateTime(objCommon.LookUp("reff", "Exam_Last_Date", ""));
                hdfExamLastDate.Value = ExamLateDate.ToString("dd/MM/yyyy");

                decimal ExamLateFee = Convert.ToDecimal(objCommon.LookUp("reff", "Exam_Late_Fee_Amt", ""));
                hdfExamLateFee.Value = ExamLateFee.ToString();

                // LATE FEE PATCH BY NARESH BEERLA ON DT 18062022 FOR USING IN JAVASCRIPT 

                if (CheckActivity())
                {
                  //  FillDropdown(); comment by gaurav not in use 16_11_2023
                    if (ViewState["usertype"].ToString() == "2")
                    {

                        int cid = 0;
                        int idno = 0;

                        idno = Convert.ToInt32(Session["idno"]);
                        cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

                        if (CheckActivityCollege(cid))
                        {
                            //CHECK ACTIVITY FOR EXAM REGISTRATION
                            //CheckActivity();
                            txtEnrollno.Text = string.Empty;
                            btnSearch.Visible = false;

                            btnCancel.Visible = false;
                            divCourses.Visible = true;
                            pnlSearch.Visible = false;
                            this.ShowDetails();

                            objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME E WITH (NOLOCK) INNER JOIN ACD_SCHEME S ON (S.SCHEMENO =" + lblScheme.ToolTip + " ) INNER                            JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (E.EXAMNO=AM.EXAMNO AND S.PATTERNNO = E.PATTERNNO)", "DISTINCT E.EXAMNO", "E.EXAMNAME", "E.EXAMNO > 0 AND ISNULL                                  (E.EXAMNAME,'')<>'' AND ISNULL(E.ACTIVESTATUS,0)=1 AND  FLDNAME<>'S6' AND EXAMTYPE=1", "E.EXAMNAME");


                            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                            {
                                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                                {

                                    CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                                    HiddenField hdfexam = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                                    Label lblExamType = dataitem.FindControl("lblExamType") as Label;
                                    if (hdfexam.Value == "1")
                                    {
                                        chkacc.Enabled = false;
                                    }
                                    else if (lblExamType.ToolTip == "0")
                                    {
                                        chkacc.Enabled = false;
                                    }
                                    else
                                    {
                                        chkacc.Enabled = true;
                                    }

                                }
                            }

                            int a = lvFailCourse.Items.Count;
                            int b = 0;
                            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                            {

                                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                                if (chk.Enabled == false)
                                    b++;

                            }

                        }
                    }
                    else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
                    {
                        //divNote.Visible = false;
                        pnlSearch.Visible = true;
                    }
                    else
                    {
                        //pnlStart.Visible = false;
                    }
                }

                else
                {

                }
                string IPADDRESS = string.Empty;

                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];
                //  string status = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "idno="+4168+" and STUDENT_REQUEST= 1 and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1", );

            }

        }
      //  paybutton();
        divMsg.InnerHtml = string.Empty;
    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            //or
            //Client_IPAddress = Request.UserHostAddress;
            //or
            //User_IPAddress = Request.ServerVariables["REMOTE_HOST"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter,
                                                          System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
            User_IPAddress = IP_Array[LatestItem - 1];
            //User_IPAddress = IP_Array[0];
        }
        return User_IPAddress;
    }
    private bool CheckActivity()
    {
        if (Convert.ToInt32(ViewState["usertype"]) == 2)
        {
            bool ret = true;
            string sessionno = string.Empty;

           // int col_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));          
            
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

            //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)  INNER JOIN ACD_SESSION_MASTER SM ON (SA.SESSION_NO = SM.SESSIONNO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1  AND SM.COLLEGE_ID IN(" + col_id + ")"); //UNION ALL SELECT 0 AS SESSION_NO
            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + " AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
            ViewState["sessionnonew"] = sessionno;
            if (sessionno == string.Empty || sessionno == null || sessionno == "0")
            {
                sessionno = "0";
            }


            // sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            Session["sessionno_current"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage(pnlFailCourse,"This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                    return false;

                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(pnlFailCourse,"Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                    return false;
                }

            }
            else
            {

                divenroll.Visible = false;
                btnSearch.Visible = false;
                btnCancel.Visible = false;
                objCommon.DisplayMessage(pnlFailCourse,"Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
                return false;
            }
            dtr.Close();
            return ret;
        }
        else
        {
            bool ret = true;
            string sessionno = string.Empty;

            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            ViewState["sessionno"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage(pnlFailCourse,"This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;

                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage(pnlFailCourse,"Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }

            }
            else
            {
                // objCommon.DisplayMessage(pnlFailCourse,"Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
            }
            dtr.Close();
            return ret;

        }
    }
    private void FillDropdown()
    {

        objCommon.FillDropDownList(ddlclgname, "ACD_COLLEGE_MASTER SM", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "SM.COLLEGE_ID DESC");


        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + ViewState["ACTIVITY_NO"], "");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }



        //mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        ddlSession.Focus();
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
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
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
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
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
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
        }
    }
    private void ShowDetails()
    {
        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();
        lvFailInaggre.DataSource = null;
        lvFailInaggre.DataBind();
        btnSubmit.Visible = false;
        btnsave.Visible = false;
        btnPrintRegSlip.Visible = false;
        //btnReport.Visible = false;
        int idno = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                ddlSession.Enabled = true;
                txtEnrollno.Enabled = true;
                ddlclgname.ClearSelection();
                ddlSession.ClearSelection();
                txtEnrollno.Text = string.Empty;

                return;

            }
        }

        try
        {
            if (idno > 0)
            {
                // divCourses.Visible = true;
                //DataSet dsStudent = objSC.GetStudentDetailsExam(idno);
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);
                //DataSet dsStudent = objSC.GetStudentDetailsExamRetest(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        //if (ViewState["semesternos"].ToString().Contains(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()))
                        //{

                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        //lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                        //     imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        lblBacklogFine.Text = "0";
                        lblTotalFee.Text = "0";

                        //      bindcourses();

                        // ADDED BY NARESH BEERLA FOR CALCULATING THE FINAL SEMESTER AMOUNT IN JAVASCRIPT ON DT 18062022 

                        int Duration = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO= CDB.DEGREENO)", "DISTINCT DURATION", "D.DEGREENO=" + hdfDegreeno.Value));
                        Duration = Convert.ToInt32(Duration) * 2;
                        hdfDuration.Value = Duration.ToString();
                        hdfSemester.Value = (lblSemester.ToolTip).ToString();


                    }
                    else
                    {
                        objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;
                        flag = false;
                        ddlclgname.ClearSelection();
                        ddlSession.ClearSelection();
                        txtEnrollno.Text = "";
                        ddlSession.Enabled = true;
                        txtEnrollno.Enabled = true;

                    }
                }
                else
                {
                    objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;
                    flag = false;

                    ddlclgname.ClearSelection();
                    ddlSession.ClearSelection();
                    txtEnrollno.Text = "";

                    ddlSession.Enabled = true;
                    txtEnrollno.Enabled = true;


                }
            }
            else
            {
                objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                divCourses.Visible = false;
                flag = false;

                ddlclgname.ClearSelection();
                ddlSession.ClearSelection();
                txtEnrollno.Text = "";

                ddlSession.Enabled = true;
                txtEnrollno.Enabled = true;


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
    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            // sessionno = Convert.ToInt32(Session["currentsession"]);
            sessionno = Convert.ToInt32(ViewState["sessionnonew"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ViewState["sessionnonew"]);//(ddlSession.SelectedValue);
        }

        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEMESTERNO=" + Convert.ToInt32(0);

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

    #region Event
    protected void btnReport_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                return;
            }

        }
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT(SCHEMENO)", "IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
        int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));
        ShowReport("ExamRegistrationSlip", "rptExamRegslipNit.rpt");
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        //divNote.Visible = false;
        //divCourses.Visible = true;

        int idno = 0;
        int clgid = 0;

        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

        if (REGNO != null && REGNO != string.Empty && REGNO != "")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
        }
        else
        {
            objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
            ddlSession.Enabled = true;
            txtEnrollno.Enabled = true;
            ddlclgname.ClearSelection();
            ddlSession.ClearSelection();
            txtEnrollno.Text = string.Empty;
            divCourses.Visible = false;
            return;

        }



        //idno = feeController.GetStudentIdByEnrollmentNo(REGNO);

        clgid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

        if (CheckActivityCollege(clgid))
        {
            btnShow.Visible = false;
            // txtEnrollno.Enabled = false;
            ddlSession.Enabled = false;
            ShowDetails();
            if (IsNotActivitySem == true)
            {
                objCommon.DisplayMessage(pnlFailCourse,"Activity Is Not Started For This Semester Student.", this.Page);
                divCourses.Visible = false;
                return;
            }
            else
            {
                if (flag.Equals(false))
                {
                    objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;
                }
                else
                {
                    // divCourses.Visible = true;
                }
            }
            //bindcourses();


            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {

                    CheckBox chkacc = dataitem.FindControl("chkAccept") as CheckBox;
                    HiddenField hdfexam = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                    Label lblExamType = dataitem.FindControl("lblExamType") as Label;
                    if (hdfexam.Value == "1")
                    {
                        chkacc.Enabled = false;
                    }
                    else if (lblExamType.ToolTip == "0")
                    {
                        chkacc.Enabled = false;
                    }
                    else
                    {
                        chkacc.Enabled = true;
                    }
                    //chkacc.Enabled = false;

                }
            }

            int a = lvFailCourse.Items.Count;
            int b = 0;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {

                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Enabled == false)
                    b++;

            }
            //if (a == b)
            //{
            //    btnSubmit.Enabled = false;
            //}
            //else
            //{
            //    btnSubmit.Enabled = true;
            //}

        }

        else
        { }


    }

    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        //ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        //ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        //ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        //ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + cid + " UNION ALL SELECT 0 AS SESSION_NO");
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + " AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        ViewState["sessionnonew"] = sessionno;
        //sessionno = Session["currentsession"].ToString();
        ViewState["sessionnonew"] = sessionno;
        Session["Sessionforretest"] = sessionno;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(pnlFailCourse,"This Activity has been Stopped. Contact Admin.!!", this.Page);
                //dvMain.Visible = false;
                ret = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(pnlFailCourse,"Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                //dvMain.Visible = false;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage(pnlFailCourse,"Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            txtEnrollno.Text = string.Empty;
            // dvMain.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }


    protected void btnShow_Click1(object sender, EventArgs e)
    {
        //Fail subjects List
        int idno = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        // int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        StudentController objSC = new StudentController();
        DataSet dsSubjects;
        // DataSet dsDetainedStudent = null;
        if (ViewState["usertype"].ToString() != "1" && ViewState["usertype"].ToString() != "3" && ViewState["usertype"].ToString() != "7")
        {
            idno = Convert.ToInt32(Session["idno"]);
            //int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + semesterno + " AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));

            int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "  AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));
            if (accept > 0)
            {
                lvFailCourse.Enabled = false;
                btnSubmit.Enabled = false;
                btnsave.Enabled = false;
                btnPrintRegSlip.Enabled = false;
            }
            else
            {
                lvFailCourse.Enabled = true;
                btnSubmit.Enabled = true;
                btnsave.Enabled = true;
                btnPrintRegSlip.Enabled = false;
            }
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
                return;
            }
        }

        //int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + semesterno + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        if (exreg > 0)
        {
            btnReport.Visible = true;
            objCommon.DisplayMessage(pnlFailCourse,"Selected Semester Exam Registration Already Done", this.Page);
            lvFailCourse.Enabled = true;
            btnSubmit.Enabled = false;
            btnsave.Enabled = false;
            btnPrintRegSlip.Enabled = false;
        }
        else
        {
            btnReport.Visible = false;

        }


        // dsFailSubjects = objSC.GetStudentFailExamSubjects(idno, sessionno, semesterno);
        int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
        //dsFailSubjects = objSC.GetStudentFailExamSubjectSNew(idno, sessionno, semesterno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);

        //dsSubjects = objSC.GetStudentSubjectforExamRegistration(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);
        string proc_name = "PKG_EXAM_GET_SUBJECTS_LIST_FOR_REEXAM_REGISTARTION";//PKG_EXAM_GET_SUBJECTS_LIST_FOR_REEXAM_REGISTARTION
        string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO";
        string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "";

        dsSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);

        if (dsSubjects.Tables[0].Rows.Count > 0)
        {
            lvFailCourse.DataSource = dsSubjects;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = true;
            divCourses.Visible = true;
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            divCourses.Visible = true;
            objCommon.DisplayMessage(pnlFailCourse,"No Courses found for this semester...!!!", this.Page);
        }


        if (dsSubjects != null && dsSubjects.Tables.Count > 0)
        {
            if (dsSubjects.Tables[0].Rows.Count > 0)
            {


                int sum = 0;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == false)
                    {
                        btnPrintRegSlip.Enabled = false;
                    }
                    else
                        btnPrintRegSlip.Enabled = false;

                    //string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + semesterno + "  AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");


                    string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1");

                    if (count == "0")
                    {
                        if (chk.Checked == true)
                            sum++;
                        chk.Enabled = true;
                    }
                    else
                    {
                        if (Session["usertype"].ToString() == "2")
                        {
                            chk.Enabled = false;
                            btnPrintRegSlip.Enabled = false;
                            btnSubmit.Enabled = false;
                            btnsave.Enabled = false;
                        }
                        else
                        {
                            chk.Enabled = true;
                            btnSubmit.Enabled = true;
                            btnsave.Enabled = true;
                        }
                    }
                }

                if (sum.Equals(Convert.ToInt32(1)))
                {
                    lblBacklogFine.Text = "200";
                    hdnBacklogFine.Value = "200";
                    lblTotalFee.Text = "200";
                    hdnTotalFee.Value = "200";
                    objCommon.DisplayMessage(pnlFailCourse,"Backlog Registration done for this semester...!!!", this.Page);
                }
                else if (sum.Equals(Convert.ToInt32(2)))
                {
                    lblBacklogFine.Text = "400";
                    hdnBacklogFine.Value = "400";
                    lblTotalFee.Text = "400";
                    hdnTotalFee.Value = "400";
                    objCommon.DisplayMessage(pnlFailCourse,"Backlog Registration done for this semester...!!!", this.Page);
                }
                else if (sum.Equals(Convert.ToInt32(0)))
                {
                    lblBacklogFine.Text = "0";
                    hdnBacklogFine.Value = "0";
                    lblTotalFee.Text = "0";
                    hdnTotalFee.Value = "0";
                }
                else
                {
                    lblBacklogFine.Text = "500";
                    hdnBacklogFine.Value = "500";
                    lblTotalFee.Text = "500";
                    hdnTotalFee.Value = "500";
                }
            }
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            divCourses.Visible = true;
            objCommon.DisplayMessage(pnlFailCourse,"No Courses found for this semester...!!!", this.Page);
        }
        btnSubmit.Visible = true;
        btnsave.Visible = true;
        btnPrintRegSlip.Visible = false;


        // ADDED BY NARESH BEERLA ON DT 11062022 FOR THE PRINT RECEIPT REQUIREMENT AS PER GOWDHAM

        int Ispaid = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'EF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0"));
        if (Ispaid > 0)
        {
            btnSubmit.Enabled = false;
            btnsave.Enabled = false;
            btnPrintRegSlip.Visible = false;
            btnPrintRegSlip.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = true;
            btnsave.Enabled = true;
            btnPrintRegSlip.Visible = false;
        }

        // ENDS HERE BY NARESH BEERLA ON DT 11062022 FOR THE PRINT RECEIPT REQUIREMENT AS PER GOWDHAM


        if (ViewState["usertype"].ToString() == "2")
        {
            //btnPrintRegSlip.Enabled = true;
        }
    }


    protected void bindcourses()
    {




        int idno = Convert.ToInt32(Session["idno"]);
        int sessionno = Convert.ToInt32(Session["sessionno_current"]);
        StudentController objSC = new StudentController();
        DataSet dsSubjects;

        #region NOT IN USE
        //if (ViewState["usertype"].ToString() != "1" && ViewState["usertype"].ToString() != "3" && ViewState["usertype"].ToString() != "7")
        //{
        //    idno = Convert.ToInt32(Session["idno"]);
        //    int accept = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(Session["sessionno_current"]) + "  AND PREV_STATUS=1 AND ACCEPTED=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND IDNO=" + idno));


        //    //string REGNO = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "");
        //    if (accept > 0)
        //    {
        //        //lvFailCourse.Enabled = false;
        //        //btnSubmit.Enabled = false;
        //        btnsave.Enabled = false;
        //        btnPrintRegSlip.Enabled = false;
        //    }
        //    else
        //    {
        //        lvFailCourse.Enabled = true;
        //        // btnSubmit.Enabled = true;
        //        // btnsave.Enabled = true;
        //        btnPrintRegSlip.Enabled = false;
        //    }
        //}
        //else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        //{
        //    // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

        //    string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

        //    if (REGNO != null && REGNO != string.Empty && REGNO != "")
        //    {
        //        idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
        //        ddlsubexamname.SelectedIndex = 0;
        //        return;
        //    }
        //}
        #endregion
        #region
        //int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + semesterno + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        //int exreg = Convert.ToInt32(objCommon.LookUp("acd_student_result", "count(1)", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=1 AND REGISTERED=1 AND EXAM_REGISTERED=1 AND IDNO=" + idno));

        //if (exreg > 0)
        //{


        //    btnReport.Visible = true;
        //    objCommon.DisplayMessage(pnlFailCourse,"Selected Semester Exam Registration Already Done", this.Page);
        //    lvFailCourse.Enabled = true;
        //   // btnSubmit.Enabled = false;
        //    btnPrintRegSlip.Enabled = false;






        //}
        //else
        //{
        //    btnReport.Visible = false;

        //}
        #endregion
      //  if (idno == null || idno == 0)
       // {
       //     objCommon.DisplayMessage(pnlFailCourse,"No Record Found...", this.Page);

       // }
       // else
       // {

            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            string examname = string.Empty;
            examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + (ddlexamnameabsentstudent.SelectedValue));
            string subexamname = string.Empty;
            subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));
            string proc_name = "PKG_EXAM_GET_SUBJECTS_LIST_FOR_REEXAM_REGISTARTION";
            string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO,@P_EXAMNAME,@P_SUBEXAM";
            string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "," + examname + "," + subexamname;

            dsSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);


            //objSC.GetStudentSubjectforExamRegistration(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno);
            #region commented code
            if (dsSubjects.Tables[0].Rows.Count > 0)
            {
                lvFailCourse.DataSource = dsSubjects;
                lvFailCourse.DataBind();

                 

                //check Student Approv by admin

                int adminapprov = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ADMIN_APPROVE=1 AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO=" + ddlsubexamname.SelectedValue));
                if (adminapprov > 0)
                {
                    //foreach (ListViewDataItem item in lvFailCourse.Items)
                    //{
                    //    //lblCCode
                    //    CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                    //    Label lblstudent = item.FindControl("lblCourseName") as Label;
                    //    //int abc = Convert.ToInt32(lblstudent.ToolTip;
                    //    if (lblstudent.ToolTip == "1")
                    //    {
                    //        CheckId.Checked = true;
                    //    }
                    //    else
                    //    {
                    //        CheckId.Checked = false;
                    //    }

                    //}
                }
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                pnlFailCourse.Visible = true;

            }
            else
            {
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                divCourses.Visible = true;
                objCommon.DisplayMessage(pnlFailCourse,"No Courses found...!!!", this.Page);
                //btnSubmit.Visible = false;
                btnsave.Visible = false;
                pnlFailCourse.Visible = true;
            }
            #endregion
            #region not in use
            //foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //{
            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {
            //        // objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            //        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            //        Label lblAmt = dataitem.FindControl("lblAmt") as Label;
            //        HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
            //        HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
            //        decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
            //        //if (cbRow.Checked == true && hdfStudRegistered.Value !="1")
            //        if (cbRow.Checked == true)
            //        {
            //            Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
            //        }



            //        // objSR.SEMESTERNOS = objSR.SEMESTERNOS + (dataitem.FindControl("lblsemester") as Label).ToolTip + "$";
            //        //string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
            //        //  objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
            //        //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
            //    }
            //}
            #endregion
            #region for button hide and show
            btnPrintRegSlip.Visible = false;
            int count = 0;
            foreach (ListViewDataItem item in lvFailCourse.Items)
            {
                CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                if (CheckId.Enabled == true || CheckId.Checked==false)

                    count++;
            }
            int AdmiNotApprov = 0;
            if (count > 0)
            {
                int adminapprov1 = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ADMIN_APPROVE=1 AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO=" + ddlsubexamname.SelectedValue));
                String UnpaidApprove = string.Empty;
                UnpaidApprove = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL inner join ACD_STUDENT_RESULT SR on (AL.IDNO=SR.IDNO and AL.COURSENO=SR.COURSENO and AL.SEMESTERNO=SR.SEMESTERNO and AL.SESSIONNO=SR.SESSIONNO)", "COUNT(AL.IDNO)", "AL.IDNO='" + Session["idno"] + "' and AL.EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and AL.SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and AL.ADMIN_APPROVE=1 and AL.ADVISOR_APPROVE=1 and SR.ABSENT_LOG=0");
                if (Convert.ToInt32(UnpaidApprove) > 0)
                {
                    btnsave.Visible = false;
                    btnSubmit.Visible = true;
                }
                else
                {
                    //btnsave.Visible = true;
                    //btnSubmit.Visible = false;

                    //AdmiNotApprov = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ISNULL(ADMIN_APPROVE,0)=0 AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO=" + ddlsubexamname.SelectedValue));
                    //if (AdmiNotApprov > 0)
                    //{

                    //    btnsave.Visible = true;
                    //    btnSubmit.Visible = false;

                    //}
                    //else
                    //{
                    //    btnsave.Visible = true;
                    //    btnSubmit.Visible = false;
                    //}


                    AdmiNotApprov = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ISNULL(ADMIN_APPROVE,0)=0 AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO=" + ddlsubexamname.SelectedValue));

                    if (AdmiNotApprov > 0)
                    {
                        btnsave.Visible = true;
                        btnSubmit.Visible = false;
                        //btnsave.Visible = false;
                        //btnSubmit.Visible = true;
                        //btnSubmit.Enabled = true;
                    }
                    else
                    {
                        //btnsave.Visible = true;
                        //btnSubmit.Visible = false;
                        btnsave.Visible = true;
                        btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;
                        //btnSubmit.Visible = true;
                        //btnSubmit.Enabled = false;
                        //btnPrintRegSlip.Visible = true;
                    }
                }
            }
            else
            {
                AdmiNotApprov = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ISNULL(ADMIN_APPROVE,0)=0 AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO=" + ddlsubexamname.SelectedValue));

                if (AdmiNotApprov > 0)
                {
                    btnsave.Visible = true;
                    btnSubmit.Visible = false;
                    //btnsave.Visible = false;
                    //btnSubmit.Visible = true;
                    //btnSubmit.Enabled = true;
                }
                else {


                    if (lvFailCourse.Items.Count > 0)
                    {
                        foreach (ListViewDataItem item in lvFailCourse.Items)
                        {
                            CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                       //     CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;

                            HiddenField hdfStudRegistered = item.FindControl("hdfAbsentLog") as HiddenField;
                            if (CheckId.Checked == true && CheckId.Enabled == false && hdfStudRegistered.Value == "1")

                                count++;
                        }
                    }
                 
                    //int Ispaid1 = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_ABSENT_STUD_EXAM_REG_LOG B ON (a.idno=b.idno and a.semesterno=b.semesterno and a.semesterno=b.semesterno AND ISNULL(ApplyCourse,0)=1) INNER JOIN ACD_STUDENT_RESULT C ON (B.IDNO=C.IDNO AND B.SEMESTERNO=C.SEMESTERNO AND B.SEMESTERNO=B.SEMESTERNO AND B.COURSENO=C.COURSENO)", "COUNT(DISTINCT 1) PAY_COUNT", "A.IDNO=" + Convert.ToInt32(Session["idno"]) + " AND A.SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' and  ISNULL(ADMIN_APPROVE,0)=1 AND 	ISNULL(ADVISOR_APPROVE,0)=1 AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 AND REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND ISNULL(ABSENT_LOG,0)=1 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue)));

                    int Ispaid1 = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_ABSENT_STUD_EXAM_REG_LOG B ON (a.idno=b.idno and a.semesterno=b.semesterno and a.semesterno=b.semesterno AND ISNULL(ApplyCourse,0)=1) INNER JOIN ACD_STUDENT_RESULT C ON (B.IDNO=C.IDNO AND B.SEMESTERNO=C.SEMESTERNO AND B.SEMESTERNO=B.SEMESTERNO AND B.COURSENO=C.COURSENO)", "COUNT(B.IDNO) IDNO", "A.IDNO=" + Convert.ToInt32(Session["idno"]) + " AND A.SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' and  ISNULL(ADMIN_APPROVE,0)=1 AND 	ISNULL(ADVISOR_APPROVE,0)=1 AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 AND REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND ISNULL(ABSENT_LOG,0)=1 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue)));

                    if (count == lvFailCourse.Items.Count)

                    {

                        btnSubmit.Enabled = false;
                        btnsave.Enabled = false;//Comment by gaurav 02_11_2022
                        btnPrintRegSlip.Visible = true;
                        btnPrintRegSlip.Enabled = true;
                      


                    }

                    //if (Ispaid1 > 0)
                    //{
                    //    btnSubmit.Enabled = false;
                    //    btnsave.Enabled = false;//Comment by gaurav 02_11_2022
                    //    btnPrintRegSlip.Visible = true;
                    //    btnPrintRegSlip.Enabled = true;
                      

                    //}
                    else
                    {
                        //btnsave.Visible = true;
                        //btnSubmit.Visible = false;
                        btnsave.Visible = false;
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                    }
                    //btnSubmit.Visible = true;
                    //btnSubmit.Enabled = false;
                    //btnPrintRegSlip.Visible = true;
                }
                
                
            }
            #endregion

            int Duartion = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO= CDB.DEGREENO)", "DISTINCT DURATION", "D.DEGREENO=" + degreeno));

            if (Convert.ToInt32(Duartion) * 2 == Convert.ToInt32(lblSemester.ToolTip))///lblSemester.ToolTip))
            {
                //notefinal.Visible = true;
                // Amt = Amt + Convert.ToDecimal(1400);
            }
            else
            {
                //notefinal.Visible = false;
            }

            DateTime TodaysDate = Convert.ToDateTime(objCommon.LookUp("reff", "GETDATE()", ""));
            DateTime ExamLateDate = Convert.ToDateTime(objCommon.LookUp("reff", "Exam_Last_Date", ""));
            decimal ExamLateFee = Convert.ToDecimal(objCommon.LookUp("reff", "Exam_Late_Fee_Amt", ""));
            //if (dbDate.Date >= Convert.ToDateTime("2022/06/21").Date)
            if (TodaysDate.Date >= Convert.ToDateTime(ExamLateDate).Date)
            {
                //   objCommon.DisplayMessage(pnlFailCourse,"Activity has been closed !", this);
                // btnPAY.Visible = false;
                Amt = Amt + Convert.ToDecimal(ExamLateFee);
            }
            else
            {
                Amt = Amt + Convert.ToDecimal(0);
            }

            string TotalAmt = Amt.ToString();
            //lblTotalExamFee.Text = "300.00";
            // objCommon.DisplayMessage(pnlFailCourse,"Examination Fees :-" + Amt.ToString() + "/-", this.Page);
            Amt = 0;
            CourseAmtt = 0;

        //}

        //int Ispaid = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0"));
            //int Ispaid = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_ABSENT_STUD_EXAM_REG_LOG B ON (a.idno=b.idno and a.semesterno=b.semesterno and a.semesterno=b.semesterno AND ISNULL(ApplyCourse,0)=1)", "COUNT(DISTINCT 1) PAY_COUNT", "A.IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND A.SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue)));.
            int Ispaid = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_ABSENT_STUD_EXAM_REG_LOG B ON (a.idno=b.idno and a.semesterno=b.semesterno and a.semesterno=b.semesterno AND ISNULL(ApplyCourse,0)=1) INNER JOIN ACD_STUDENT_RESULT C ON (B.IDNO=C.IDNO AND B.SEMESTERNO=C.SEMESTERNO AND B.SEMESTERNO=B.SEMESTERNO AND B.COURSENO=C.COURSENO)", "COUNT(DISTINCT 1) PAY_COUNT", "A.IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND A.SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 AND REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND ISNULL(ABSENT_LOG,0)=1 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue)));





        if (Ispaid > 0)
        {
             //btnSubmit.Enabled = false;
            //  btnsave.Enabled = false;//Comment by gaurav 02_11_2022
            btnPrintRegSlip.Visible = true;
            btnPrintRegSlip.Enabled = true;
        }
        else
        {
            //btnSubmit.Enabled = true;
            //btnsave.Enabled = true;
            //btnPrintRegSlip.Visible = false;
        }
        if (ViewState["usertype"].ToString() == "2")
        {
            //btnPrintRegSlip.Enabled = true;
        }
    }

    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            int adminapprov = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ADMIN_APPROVE=1 AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO=" + ddlsubexamname.SelectedValue));
            if (adminapprov > 0)
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    //CheckBox CheckId = e.item.FindControl("chkAccept") as CheckBox;
                    //Label lblstudent = item.FindControl("lblCourseName") as Label;

                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                    HiddenField studentapply = (HiddenField)e.Item.FindControl("hdfapplycourses");
                    HiddenField adminapprove = (HiddenField)e.Item.FindControl("hdfapprovecourses");
                    HiddenField absentlogregistered = (HiddenField)e.Item.FindControl("hdfAbsentLog");

                    if (studentapply.Value == "1")
                    {
                        if (adminapprove.Value == "1" && studentapply.Value == "1" && absentlogregistered.Value != "1")
                        {
                            chk.Checked = true;
                            chk.Enabled = false;
                            //chk.BackColor = System.Drawing.Color.Green;
                        }
                        else if (adminapprove.Value == "1" && absentlogregistered.Value == "1")
                        {
                            chk.Checked = true;
                            chk.Enabled = false;
                            chk.BackColor = System.Drawing.Color.Green;
                        }

                        else
                        {
                            chk.Checked = true;
                            chk.Enabled = true;

                            //chk.BackColor = System.Drawing.Color.Red;
                        }


                    }
                    else
                    {
                        chk.Checked = false;
                        chk.Enabled = true;
                        //chk.BackColor = System.Drawing.Color.Red;

                    }
                }
            }
            else {


                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                    HiddenField studentapply = (HiddenField)e.Item.FindControl("hdfapplycourses");
                    HiddenField adminapprove = (HiddenField)e.Item.FindControl("hdfapprovecourses");
                    HiddenField absentlogregistered = (HiddenField)e.Item.FindControl("hdfAbsentLog");

                    if (studentapply.Value == "1")
                    {
                        chk.Checked = true;
                        chk.Enabled = true;


                    }
                    else if (studentapply.Value == "1" && adminapprove.Value == "1")
                    {

                        chk.Checked = true;
                        chk.Enabled = false;

                    }
                    else if (studentapply.Value == "1" && adminapprove.Value == "1" && absentlogregistered.Value == "1")
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        chk.BackColor = System.Drawing.Color.Green;
                    }

                }


            
            }
               
        }
       

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamRegistration_CC.lvFailCourse_ItemDataBound() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        int idno = 0;        
        idno = Convert.ToInt32(Session["idno"]);    
        string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
        objSR.SESSIONNO = Convert.ToInt32(Session["sessionno_current"]);
        objSR.IDNO = idno;
        ViewState["IDNO"] = idno;
        objSR.REGNO = Regno;
        objSR.ROLLNO = txtEnrollno.Text;
        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
        objSR.IPADDRESS = Session["ipAddress"].ToString(); ;// ----ViewState["ipAddress"].ToString();
        objSR.COLLEGE_CODE = Session["colcode"].ToString();
        objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
        objSR.COURSENOS = string.Empty;
        int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
        int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
        int status = 0;
        int cntcourse = 0;
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.DegreeNo = degreenos;
        objSA.BranchNo = branchnos;
        objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
        objSA.IpAddress = ViewState["ipAddress"].ToString();
        objSR.EXAM_REGISTERED = 0;
        objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
                #region faliCourse
        int A = lvFailCourse.Items.Count;
        if (lvFailCourse.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true)
                    cntcourse++;
            }

        }
        if (cntcourse == 0)
        {
            objCommon.DisplayMessage(pnlFailCourse,"Please Select Atleast one Course..!!", this.Page);
            return;
        }
        else
        {

            if (lvFailCourse.Items.Count > 0)
            {
                //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "  AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1) AND PREV_STATUS=1"));
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true)//if (chk.Checked == true && chk.Enabled == true)
                        status++;
                }
            }
            else
            {
                status = -1;
            }

            string CourseSemAmt = string.Empty;
            if (status > 0)
            {

                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                    HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                    // if (cbRow.Checked == true && hdfStudRegistered.Value != "1") // if (cbRow.Checked == true && cbRow.Enabled == true)
                    if (cbRow.Checked == true)
                    {
                        objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
                        objSR.SEMESTERNOS += ((dataitem.FindControl("lblsemester")) as Label).ToolTip + ",";
                        CourseSemAmt += ((dataitem.FindControl("lblAmt")) as Label).Text + ",";
                    }
                }
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd(',');
                objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd(',');
                CourseSemAmt = CourseSemAmt.TrimEnd(',');



                // ADDED BELOW CODE ONLINE PAYMENT GATEWAY ON DT 30052022
        #endregion
                 #region UDATE FLAG FOR APPLY COURSE
//      foreach (ListViewDataItem dataitem in lvFailCourse.Items)
//            {
//                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
//                if (chk.Checked == true && chk.Enabled=false
//)
//                {
               
//                string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW";
//                string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_OUT";
//                string Call_Values = "" + idno + "," + Convert.ToInt32(objSR.SESSIONNO) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(lblSemester.ToolTip) + "," + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + "," + Convert.ToInt32(ddlsubexamname.SelectedValue) + "," + Session["userno"] + "," + examname + ",'" + subexamname + "'," + hdfexistMarks.Value + "," + studentreq + "," + fees.ToolTip + ",1";

//                // return;

//                string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
//                if (que_out == "0")
//                {
//                    objCommon.DisplayMessage(pnlFailCourse,"Course Update Sucessfully", this.Page);
//                    paybutton();
//                }
//                else
//                {
//                    objCommon.DisplayMessage(pnlFailCourse,"Course Apply Sucessfully", this.Page);
//                    paybutton();
//                }

//                }
               #endregion


                #region ONLINE PAYMENT GATEWAY modiFy by GAurav S 12_12_2022

                try
                {
                    int ifdemandexist = 0;
                    ifdemandexist = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(DISTINCT 1) DEMAND_COUNT", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(CAN,0)=0"));
                    if (ifdemandexist == 0)
                    {
                        objCommon.DisplayMessage(pnlFailCourse,"DEMAND is Not Created Properly !", this.Page);
                        return;
                    }


                    //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR A INNER JOIN ACD_ABSENT_STUD_EXAM_REG_LOG B ON (a.idno=b.idno and a.semesterno=b.semesterno and a.semesterno=b.semesterno AND ISNULL(ApplyCourse,0)=1) INNER JOIN ACD_STUDENT_RESULT C ON (B.IDNO=C.IDNO AND B.SEMESTERNO=C.SEMESTERNO AND B.SEMESTERNO=B.SEMESTERNO AND B.COURSENO=C.COURSENO)", "COUNT(DISTINCT 1) PAY_COUNT", "A.IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND A.SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 AND REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND ISNULL(ABSENT_LOG,0)=1 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue)));
                   // int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0"));
                    //if (ifPaidAlready > 0)
                    //{
                    //    objCommon.DisplayMessage(pnlFailCourse,"Exam Fee has been paid already. Can not proceed with the transaction !", this.Page);
                    //    return;
                    //}



                    ViewState["Final_Amt"] = lblTotalExamFee.Text.ToString();

             


                    if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
                    {
                        objCommon.DisplayMessage(pnlFailCourse,"You are not eligible for Fee Payment !", this);
                        return;
                    }


                    if (ViewState["Final_Amt"].ToString() != string.Empty)
                     {
                        //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["IDNO"] + "'", "");
                        DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME,ISNULL(SEMESTERNO,0)SEMESTERNO", "IDNO = '" + Convert.ToInt32(ViewState["IDNO"]) + "'", "");
                        ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                        ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                        ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                        ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                        ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                        ViewState["SESSIONNO"] = Convert.ToInt32(Session["sessionno_current"]);
                        ViewState["SEM"] = objCommon.LookUp("ACD_STUDENT_RESULT", "distinct SEMESTERNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND sessionno=" + Convert.ToInt32(Session["sessionno_current"]));
                        ViewState["RECIEPT"] = "REF";
                        ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
                        ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());
                        ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                        ViewState["basicinfo"] = ViewState["ENROLLNO"];
                        PostOnlinePayment();
                        string amount = string.Empty;
                        amount = Convert.ToString(ViewState["Final_Amt"]);
                         //amount = "1";
                        try
                        {
                            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                            //    DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
                            // string PaymentMode = "ONLINE EXAM FEES";
                            string PaymentMode = "RETEST EXAM FEE";
                            Session["PaymentMode"] = PaymentMode;
                            Session["studAmt"] = amount;
                            ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
                            // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
                            Session["studName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
                            Session["studPhone"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
                            Session["studEmail"] = ViewState["EMAILID"].ToString(); // lblMailId.Text;

                            Session["ReceiptType"] = "REF";
                            Session["idno"] = Convert.ToInt32(ViewState["IDNO"].ToString()); //hdfIdno.Value;
                            Session["paysession"] = Convert.ToInt32(Session["sessionno_current"]); // hdfSessioNo.Value;
                            //Session["paysemester"] = ViewState["SEM"].ToString(); // ddlSemester.SelectedValue;
                            Session["paysemester"] = lblSemester.Text;
                            Session["homelink"] = "RetestExamRegistration_All.aspx";
                            Session["regno"] = ViewState["REGNO"].ToString(); // lblRegno.Text;
                            Session["payStudName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
                            Session["paymobileno"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
                            Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
                            Session["Branchname"] = ViewState["SHORTNAME"].ToString(); //lblBranchName.Text;
                            Session["EXAMNO"] = ddlexamnameabsentstudent.SelectedValue.ToString();

                            
                            DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]));
                            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows.Count > 1)
                                {

                                }
                                else
                                {
                                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                                    Response.Redirect(RequestUrl, false);

                                }
                            }
                            else
                            {

                                objCommon.DisplayMessage(pnlFailCourse,"Payment Configuration Not done. Contact Admin.!! ", this.Page);
                            }

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(pnlFailCourse,"Transaction Failed !.", this.Page);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }


                #endregion


         
              


            }

          //  }
            
            else
            {
                objCommon.DisplayMessage(pnlFailCourse,"Please Select atleast one course", this.Page);
                return;
            }
           
        }
    }
    #endregion
    #region ddl
    protected void ddlBackLogSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBackLogSem.SelectedItem.Value == "0")
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            btnSubmit.Visible = false;
            btnReport.Visible = false;
            btnShow.Visible = false;
            btnPrintRegSlip.Visible = false;
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
        }

    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();
        int branchno = Convert.ToInt32(objCommon.LookUp("acd_student", "branchno", "regno='" + lblEnrollNo.Text + "'"));
        int degreeno = Convert.ToInt32(objCommon.LookUp("acd_student", "degreeno", "regno='" + lblEnrollNo.Text + "'"));
        //  int semesterno = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", "regno='" + lblEnrollNo.Text + "'"));
        int admbatch = Convert.ToInt32(objCommon.LookUp("acd_student", "ADMBATCH", "regno='" + lblEnrollNo.Text + "'"));
        int paymenttypeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
        //lblEnrollNo


        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            demandCriteria.StudentId = Convert.ToInt32(idno);
            demandCriteria.StudentName = lblName.Text;
            //demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.SessionNo = Convert.ToInt32(ViewState["sessionno"]);
            demandCriteria.ReceiptTypeCode = "REF";
            demandCriteria.BranchNo = branchno;
            demandCriteria.DegreeNo = degreeno;
            demandCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            demandCriteria.AdmBatchNo = admbatch;
            demandCriteria.PaymentTypeNo = paymenttypeno;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.Remark = "BackLog Fees";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EaxmRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("SubstituteExamRegistrationSlip", "Substitute_PaymentReceipt_Exam_Registered_Courses.rpt");
    }


    //private void ShowDetails()
    //{

    //    lvFailCourse.DataSource = null;
    //    lvFailCourse.DataBind();

    //    int idno = 0;
    //    int sessionno = Convert.ToInt32(Session["currentsession"]);
    //    StudentController objSC = new StudentController();

    //    // Commented below code because we added drop dwon list for session

    //    string Session_Name = string.Empty;
    //    Session_Name = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
    //    lblsession.Text = Convert.ToString(Session_Name);

    //    string Session_No = string.Empty;
    //    Session_No = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
    //    Session["SESSIONNO"] = Convert.ToInt32(Session_No);

    //    if (ViewState["usertype"].ToString() == "2")
    //    {
    //        idno = Convert.ToInt32(Session["idno"]);
    //    }
    //    else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || Session["usertype"].ToString() == "12")
    //    {
    //        idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
    //    }
    //    try
    //    {
    //        if (idno > 0)
    //        {
    //            DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

    //            if (dsStudent != null && dsStudent.Tables.Count > 0)
    //            {
    //                if (dsStudent.Tables[0].Rows.Count > 0)
    //                {
    //                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
    //                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

    //                    lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
    //                    lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

    //                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
    //                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
    //                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
    //                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
    //                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
    //                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
    //                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
    //                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
    //                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
    //                    //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
    //                    hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
    //                    hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
    //                    lblCollege.Text = dsStudent.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
    //                    hdnCollege.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
    //                    int admbatchno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"]);
    //                    // lblCollege.ToolTip = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
    //                    ViewState["IDNO"] = lblName.ToolTip;

    //                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
    //                    DataSet dasem;
    //                    // dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL'  AND SEMESTERNO NOT IN (SELECT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO <=" + lblSemester.ToolTip + " AND  IDNO  =" + ViewState["IDNO"] + "   AND SEMESTERNO > 0  AND  PASSFAIL = 'PASS' )", "SEMESTERNO");
    //                    dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL' ", "SEMESTERNO");
    //                    ddlBackLogSem.Items.Clear();
    //                    ddlBackLogSem.Items.Add("Please Select");
    //                    ddlBackLogSem.SelectedItem.Value = "0";

    //                    int oddeven = 0;
    //                    if (Convert.ToInt32(lblSemester.ToolTip) == 1 || Convert.ToInt32(lblSemester.ToolTip) == 3 || Convert.ToInt32(lblSemester.ToolTip) == 5 || Convert.ToInt32(lblSemester.ToolTip) == 7 || Convert.ToInt32(lblSemester.ToolTip) == 9)
    //                    {
    //                        oddeven = 1;
    //                    }
    //                    else if (Convert.ToInt32(lblSemester.ToolTip) == 2 || Convert.ToInt32(lblSemester.ToolTip) == 4 || Convert.ToInt32(lblSemester.ToolTip) == 6 || Convert.ToInt32(lblSemester.ToolTip) == 8 || Convert.ToInt32(lblSemester.ToolTip) == 10)
    //                    {
    //                        oddeven = 2;
    //                    }
    //                    if (dasem != null && dasem.Tables[0].Rows.Count > 0)
    //                    {
    //                        if (hdfDegreeno.Value == "1")//For B.Arch Student Can Apply for oddbacklog to oddcurrent and evenbacklog to even semesters only others can apply all semesters
    //                        {
    //                            //  DataSet dasem;
    //                            // dasem = objCommon.FillDropDown("ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL'  AND SEMESTERNO NOT IN (SELECT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO <=" + lblSemester.ToolTip + " AND  IDNO  =" + ViewState["IDNO"] + "   AND SEMESTERNO > 0  AND  PASSFAIL = 'PASS' )", "SEMESTERNO");
    //                            //ddlBackLogSem.Items.Clear();
    //                            //ddlBackLogSem.Items.Add("Please Select");// if ( ViewState["addeven"] !=null && Convert.ToInt32(ViewState["addeven"]) == 2)
    //                            if (oddeven == 2)
    //                            {
    //                                //if ((Convert.ToInt32(ddlSession.SelectedValue)) % 2 == 0)
    //                                //{
    //                                for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
    //                                {
    //                                    if (Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 == 0)
    //                                    {
    //                                        ListItem newlist = new ListItem(dasem.Tables[0].Rows[i]["SEMESTER"].ToString(), dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString());
    //                                        ddlBackLogSem.Items.Add(newlist);
    //                                    }
    //                                }
    //                                //}
    //                            }
    //                            else if (oddeven == 1)  // if ( ViewState["addeven"] !=null && Convert.ToInt32(ViewState["addeven"]) == 1)
    //                            {
    //                                for (int i = 0; i <= dasem.Tables[0].Rows.Count - 1; i++)
    //                                {
    //                                    if (Convert.ToInt32(dasem.Tables[0].Rows[i]["SEMESTERNO"]) % 2 != 0)
    //                                    {
    //                                        ListItem newlist = new ListItem(dasem.Tables[0].Rows[i]["SEMESTER"].ToString(), dasem.Tables[0].Rows[i]["SEMESTERNO"].ToString());
    //                                        ddlBackLogSem.Items.Add(newlist);

    //                                    }
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (dasem != null && dasem.Tables[0].Rows.Count > 0)
    //                            {
    //                                ddlBackLogSem.DataSource = dasem;
    //                                ddlBackLogSem.DataValueField = dasem.Tables[0].Columns[0].ToString();
    //                                ddlBackLogSem.DataTextField = dasem.Tables[0].Columns[1].ToString();
    //                                ddlBackLogSem.DataBind();
    //                                ddlBackLogSem.SelectedIndex = 0;
    //                            }
    //                            // objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT", "distinct SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO <= " + lblSemester.ToolTip + " and  idno =" + ViewState["IDNO"] + "  AND SEMESTERNO > 0  AND  PASSFAIL = 'FAIL'  AND SEMESTERNO NOT IN (SELECT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO <=" + lblSemester.ToolTip + " AND  IDNO  =" + ViewState["IDNO"] + "   AND SEMESTERNO > 0  AND  PASSFAIL = 'PASS' )", "SEMESTERNO");

    //                        }
    //                    }                     
    //                }
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void ddlexamnameabsentstudent_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlexamnameabsentstudent.SelectedIndex > 0)
        {
            // int Subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue)));
            objCommon.FillDropDownList(ddlsubexamname, "ACD_SUBEXAM_NAME", "DISTINCT SUBEXAMNO", "SUBEXAMNAME", "SUBEXAMNO > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=1 AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue), "SUBEXAMNO");
            ddlsubexamname.Focus();
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            btnSubmit.Visible = false;
            btnsave.Visible = false;
            btnPrintRegSlip.Visible = false;


        }
        else
        {
            // lvFailCourse.DataSource = null;
            //lvFailCourse.DataBind();
            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = false;
            //objCommon.DisplayMessage(pnlFailCourse,"Please Select Exam Name", this.Page);
            ddlsubexamname.SelectedIndex = 0;
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            btnsave.Visible = false;
            lblTotalExamFee.Text = string.Empty;
        }


    }

    protected void ddlsubexamname_SelectedIndexChanged(object sender, EventArgs e)
    {
       int  Course=0;
       string UnpaidCourse = string.Empty; string PaidCourse = string.Empty; string UnPaidCourse1 = string.Empty;
        if (ddlexamnameabsentstudent.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(pnlFailCourse,"Please Select Exam Name", this.Page);
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            return;
        }
        if (ddlsubexamname.SelectedIndex > 0)
        {
            string REGNO2 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"])+"AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);
            string REGNO = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"])+"AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);
            //String PaidCourse = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL INNER JOIN ACD_STUDENT_RESULT SR ON (AL.COURSENO=SR.COURSENO AND AL.SEMESTERNO=SEMESTERNO AND AL.SESSIONNO=SR.SESSIONNO)", "COUNT(AL.IDNO)", "AL.IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and SR.ABSENT_LOG=1");
            ////int fees = int.Parse(REGNO2) * 300;

            //if (Convert.ToInt32(REGNO) > 0)
            //{
                
            //    int fees = int.Parse(REGNO) * 350;
            //     btnsave.Visible = false;
            //    btnSubmit.Visible = true;
            //    lblTotalExamFee.Text = fees.ToString() + ".00";
            //}

            UnpaidCourse = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);
            PaidCourse = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL inner join ACD_STUDENT_RESULT SR on (AL.IDNO=SR.IDNO and AL.COURSENO=SR.COURSENO and AL.SEMESTERNO=SR.SEMESTERNO and AL.SESSIONNO=SR.SESSIONNO)", "COUNT(AL.IDNO)", "AL.IDNO='" + Session["idno"] + "' and AL.EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and AL.SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and AL.ADMIN_APPROVE=1 and AL.ADVISOR_APPROVE=1 and SR.ABSENT_LOG=1 AND SR.SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]));
            //int fees = int.Parse(REGNO2) * 300;
            UnPaidCourse1 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL inner join ACD_STUDENT_RESULT SR on (AL.IDNO=SR.IDNO and AL.COURSENO=SR.COURSENO and AL.SEMESTERNO=SR.SEMESTERNO and AL.SESSIONNO=SR.SESSIONNO)", "COUNT(AL.IDNO)", "AL.IDNO='" + Session["idno"] + "' and AL.EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and AL.SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and AL.ADMIN_APPROVE=1 and AL.ADVISOR_APPROVE=1 and SR.ABSENT_LOG=0 AND SR.SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]));
            Course = Convert.ToInt32(PaidCourse);
            int fees1 = Course * 350;
            lblPaidFees.Text = fees1.ToString() + ".00";
            if (Convert.ToInt32(UnpaidCourse) > 0)
            {
                if (PaidCourse != "")
                {
                    Course = Convert.ToInt32(UnPaidCourse1);
                    int fees = Course * 350;
                    btnsave.Visible = false;
                    btnsave.Enabled = false;
                    btnSubmit.Visible = true;
                    btnSubmit.Enabled = true;
                    lblTotalExamFee.Text = fees.ToString() + ".00";
                }
                else
                {
                    int fees = int.Parse(UnpaidCourse) * 350;
                    btnsave.Visible = false;
                    btnSubmit.Visible = true;
                    lblTotalExamFee.Text = fees.ToString() + ".00";
                }
            }
            else
            {
                string REGNO3 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);
                if (Convert.ToInt32(REGNO3) > 0)
                {
                    int fees = int.Parse(REGNO3) * 350;
                    lblTotalExamFee.Text = fees.ToString() + ".00";
                    btnsave.Visible = true;
                    //btnsave.Enabled = true;
                    btnSubmit.Visible = false;
                }
                else
                {
                    //string REGNO4 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1");


                    //    if (Convert.ToInt32(REGNO4) > 0)
                    //    {
                    //        int fees = int.Parse(REGNO4) * 300;
                    //        lblTotalExamFee.Text = fees.ToString() + ".00";
                    //        btnsave.Visible = true;
                    //        //btnsave.Enabled = true;
                    //        btnSubmit.Visible = false;
                    //    }


                    //else{
                    // int fees = int.Parse(REGNO2) * 300;
                    // lblTotalExamFee.Text = fees.ToString()+".00";
                    // btnsave.Visible = true;
                    // //btnsave.Enabled = true;
                    // btnSubmit.Visible = false;
                    string REGNO4 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);

                    if (Convert.ToInt32(REGNO4) > 0)
                    {
                        int fees = int.Parse(REGNO4) * 350;
                        lblTotalExamFee.Text = fees.ToString() + ".00";
                        btnsave.Visible = true;
                        //btnsave.Enabled = true;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        int fees = int.Parse(REGNO2) * 350;
                        lblTotalExamFee.Text = fees.ToString() + ".00";
                        btnsave.Visible = true;
                        btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;
                    }
                    //}
                }

            }

            bindcourses();


        }

        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            // objCommon.DisplayMessage(pnlFailCourse,"Please Select Exam Name", this.Page);
            btnsave.Visible = false;
            btnSubmit.Visible = false;
            lblTotalExamFee.Text = string.Empty;
            btnPrintRegSlip.Visible = false;
            lblTotalExamFee.Text = string.Empty;
            

        }
    }

    protected void paybutton()
    {

        string REGNO2 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and STUDENT_REQUEST=1  and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);
        // string REGNO = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1");
        int fees = int.Parse(REGNO2) * 350;
        if (Convert.ToInt32(REGNO2) > 0)
        {
            // btnsave.Visible = false;
            // Button1.Visible = true;
            lblTotalExamFee.Text = fees.ToString() + ".00";
            // btnSubmit.Visible = true;
            //lblTotalExamFee.Text = fees.ToString() + ".00";
        }
        else
        {
            string REGNO = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and STUDENT_REQUEST=1 and SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"])+" AND EXAMNO=" + ddlexamnameabsentstudent.SelectedValue);

            int feess = int.Parse(REGNO) * 350;
            if (Convert.ToInt32(REGNO) > 0)
            {
                lblTotalExamFee.Text = feess.ToString() + ".00";


            }
            else
            {
                lblTotalExamFee.Text = fees.ToString() + ".00";
            }
        }



    }
    protected void ddlclgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlclgname.SelectedValue), "SESSIONNO desc");
        ddlSession.Enabled = true;
        txtEnrollno.Enabled = true;
        txtEnrollno.Text = string.Empty;
        divCourses.Visible = false;
        pnlFailCourse.Visible = false;

    }


    #region Added for the Payment Calculations as per the checked Courses on 25052022
    decimal Amt = 0;
    decimal CourseAmtt = 0;
    //protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
    //        {
    //            if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
    //            {
    //                // objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
    //                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
    //                Label lblAmt = dataitem.FindControl("lblAmt") as Label;
    //                HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
    //                HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
    //                decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
    //                // if (cbRow.Checked == true && hdfStudRegistered.Value != "1") // changed to hdfexamregistered!=1 to hdfStudRegistered!=1
    //                if (cbRow.Checked == true)
    //                {
    //                    //Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
    //                }



    //                // objSR.SEMESTERNOS = objSR.SEMESTERNOS + (dataitem.FindControl("lblsemester") as Label).ToolTip + "$";
    //                //string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
    //                //  objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
    //                //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
    //            }
    //        }

    //        int Duartion = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO= CDB.DEGREENO)", "DISTINCT DURATION", "D.DEGREENO=" + hdfDegreeno.Value));

    //        if (Convert.ToInt32(Duartion) * 2 == Convert.ToInt32(lblSemester.ToolTip))///lblSemester.ToolTip))
    //        {
    //            notefinal.Visible = true;
    //            Amt = Amt + Convert.ToDecimal(1400);
    //        }
    //        else
    //        {
    //            notefinal.Visible = false;
    //        }

    //        DateTime TodaysDate = Convert.ToDateTime(objCommon.LookUp("reff", "GETDATE()", ""));
    //        DateTime ExamLateDate = Convert.ToDateTime(objCommon.LookUp("reff", "Exam_Last_Date", ""));
    //        decimal ExamLateFee = Convert.ToDecimal(objCommon.LookUp("reff", "Exam_Late_Fee_Amt", ""));
    //        //if (dbDate.Date >= Convert.ToDateTime("2022/06/21").Date)
    //        if (TodaysDate.Date >= Convert.ToDateTime(ExamLateDate).Date)
    //        {
    //            //   objCommon.DisplayMessage(pnlFailCourse,"Activity has been closed !", this);
    //            // btnPAY.Visible = false;
    //            Amt = Amt + Convert.ToDecimal(ExamLateFee);
    //        }
    //        else
    //        {
    //            Amt = Amt + Convert.ToDecimal(0);
    //        }

    //        string TotalAmt = Amt.ToString();
    //        lblTotalExamFee.Text = TotalAmt.ToString();
    //        // objCommon.DisplayMessage(pnlFailCourse,"Examination Fees :-" + Amt.ToString() + "/-", this.Page);
    //        Amt = 0;
    //        CourseAmtt = 0;
    //    }
    //    catch
    //    {
    //    }
    //}
    #endregion Added for the Payment Calculations as per the checked Courses on 25052022



    #region NOT in USE
    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        //lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["IDNO"]) + Convert.ToString(ir));
        //3 for arrear exam
        lblOrderID.Text = Convert.ToString(Convert.ToString(ViewState["IDNO"]) + Convert.ToString(ViewState["SESSIONNO"]) + Convert.ToString(ir) + "3");
    }
    #region FOR GET PG_IDS ADDED BY GAURAV S 08_12_2022
    protected void PostOnlinePayment()
    {

        int orgId = 2; int payId = 2; string merchId = string.Empty; string checkSumKey = string.Empty; string requestUrl = string.Empty; string responseUrl = string.Empty;
        string pgPageUrl = string.Empty; string accCode = string.Empty;
        DataSet dsGetPayConfig = feeController.GetOnlinePaymentConfigurationDetails(orgId, 0, payId);
        if (dsGetPayConfig.Tables[0].Rows.Count > 0)
        {
            merchId = dsGetPayConfig.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
            checkSumKey = dsGetPayConfig.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
            requestUrl = dsGetPayConfig.Tables[0].Rows[0]["REQUEST_URL"].ToString();
            responseUrl = dsGetPayConfig.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
            pgPageUrl = dsGetPayConfig.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            accCode = dsGetPayConfig.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
        }
    #endregion

        #region Declarations
        string feeAmount = string.Empty;
        string Transacionid = "NA";
        string TransactionFor = string.Empty;
        string TSPLTxnCode = string.Empty;
        string TSPLtxtITC = string.Empty;
        #endregion

        #region Get Payment Details
        feeAmount = (ViewState["Final_Amt"]).ToString();
        #endregion

        #region Payment Log for Different Transaction Id
        string TransactionCode = string.Empty;
        TransactionCode = lblOrderID.Text; // This may be configured from Database for Different Running Number
        #endregion

        #region BillDesk Data Declaration
        string MerchantID = string.Empty;
        string UniTranNo = string.Empty;
        string NA1 = string.Empty;
        string txn_amount = string.Empty;
        string NA2 = string.Empty;
        string NA3 = string.Empty;
        string NA4 = string.Empty;
        string CurrencyType = string.Empty;
        string NA5 = string.Empty;
        string TypeField1 = string.Empty;
        string SecurityID = string.Empty;
        string NA6 = string.Empty;
        string NA7 = string.Empty;
        string TypeField2 = string.Empty;
        string additional_info1 = string.Empty;
        string additional_info2 = string.Empty;
        string additional_info3 = string.Empty;
        string additional_info4 = string.Empty;
        string additional_info5 = string.Empty;
        string additional_info6 = string.Empty;
        string additional_info7 = string.Empty;
        string ReturnURL = string.Empty;
        string ChecksumKey = string.Empty;
        #endregion

        #region Set Bill Desk Param Data
        MerchantID = merchId;//ConfigurationManager.AppSettings["MerchantID"];
        UniTranNo = TransactionCode;
        txn_amount = feeAmount;
        CurrencyType = "INR";
        SecurityID = accCode;//ConfigurationManager.AppSettings["SecurityCode"];
        additional_info1 = ViewState["STUDNAME"].ToString(); // Project Name
        additional_info2 = ViewState["IDNO"].ToString();  // Project Code
        additional_info3 = ViewState["RECIEPT"].ToString(); // Transaction for??
        additional_info4 = ViewState["info"].ToString(); // Payment Reason
        additional_info5 = feeAmount; // Amount Passed
        additional_info6 = ViewState["basicinfo"].ToString();  // basic details like regno/enroll no/branchname
        additional_info7 = ViewState["SESSIONNO"].ToString();




        ReturnURL = requestUrl; // "
        //  ReturnURL=  "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx";

        // ReturnURL = "https://localhost:63976/PresentationLayer/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "https://localhost:50070/PresentationLayer/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "https://localhost:52072/PresentationLayer/Academic/PhotoReval_Response.aspx";
        //ReturnURL = "https://localhost:50070/PresentationLayer/Academic/ResponseStatus.aspx";
        //ReturnURL = "https://svcetest.mastersofterp.in/Academic/ResponseStatus.aspx";
        //ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "https://svcetest.mastersofterp.in/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "https://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx?id=" + ViewState["IDNO"].ToString();
        //ReturnURL = "http://localhost:52072/PresentationLayer/Academic/PhotoReval_Response.aspx"; 
        //ReturnURL = "https://svcetest.mastersofterp.in/Academic/PhotoReval_Response.aspx";
        ChecksumKey = checkSumKey;//ConfigurationManager.AppSettings["ChecksumKey"];
        #endregion

        #region Generate Bill Desk Check Sum

        StringBuilder billRequest = new StringBuilder();
        billRequest.Append(MerchantID).Append("|");
        billRequest.Append(UniTranNo).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(txn_amount).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append(CurrencyType).Append("|");
        billRequest.Append("DIRECT").Append("|");
        billRequest.Append("R").Append("|");
        billRequest.Append(SecurityID).Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("NA").Append("|");
        billRequest.Append("F").Append("|");
        billRequest.Append(additional_info1).Append("|");
        billRequest.Append(additional_info2).Append("|");
        billRequest.Append(additional_info3).Append("|");
        billRequest.Append(additional_info4).Append("|");
        billRequest.Append(additional_info5).Append("|");
        billRequest.Append(additional_info6).Append("|");
        billRequest.Append(additional_info7).Append("|");
        billRequest.Append(ReturnURL);

        string data = billRequest.ToString();

        String hash = String.Empty;

        hash = GetHMACSHA256(data, ChecksumKey);
        hash = hash.ToUpper();

        string msg = data + "|" + hash;

        #endregion

        #region Post to BillDesk Payment Gateway

        string PaymentURL = responseUrl + msg; //ConfigurationManager.AppSettings["BillDeskURL"] + msg;

        //Response.Redirect(PaymentURL, false);
        Response.Write("<form name='s1_2' id='s1_2' action='" + PaymentURL + "' method='post'> ");
        Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
        Response.Write("</script>");
        Response.Write("<script language='javascript' >");
        Response.Write("</script>");
        Response.Write("</form> ");
        Response.Write("<script>window.open(" + PaymentURL + ",'_blank');</script>");
        #endregion
    }

    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    #endregion

    protected void txtEnroll_TextChanged(object sender, EventArgs e)
    {
        int idno = 0;
        int clgid = 0;

        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

        if (REGNO != null && REGNO != string.Empty && REGNO != "")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
        }
        else
        {
            objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
            ddlSession.Enabled = true;
            txtEnrollno.Enabled = true;
            ddlclgname.ClearSelection();
            ddlSession.ClearSelection();
            txtEnrollno.Text = string.Empty;
            divCourses.Visible = false;
            return;

        }



        //idno = feeController.GetStudentIdByEnrollmentNo(REGNO);

        clgid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

        if (CheckActivityCollege(clgid))
        {
            btnShow.Visible = false;
            // txtEnrollno.Enabled = false;
            ddlSession.Enabled = false;
            //  ShowDetails();
            // if (IsNotActivitySem == true)
            //{
            //      objCommon.DisplayMessage(pnlFailCourse,"Activity Is Not Started For This Semester Student.", this.Page);
            //     divCourses.Visible = false;
            //      return;
            //  }
            // else
            // {
            //    if (flag.Equals(false))
            //    {
            //       objCommon.DisplayMessage(pnlFailCourse,"No Records Found for this Student.!!", this.Page);
            //      divCourses.Visible = false;
            //  }
            // else
            // {
            //      // divCourses.Visible = true;
            //  }
            // }
            objCommon.FillDropDownList(ddlsessionnew, "SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO) INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONNO=SA.SESSION_NO)", "SA.SESSION_NO", "SESSION_NAME", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + clgid, "");

        }
    }


    //=============================add by gaurav for save student course 13072022
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem item in lvFailCourse.Items)
                {
                    CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                    if (CheckId.Checked == true && CheckId.Enabled==true)

                        count++;
                }
            }

            if (count == 0)
            {

                objCommon.DisplayMessage(pnlFailCourse,"Please Select Atleast one Student from the list", this.Page);
                bindcourses();
                return;
            }
            //For Change Subject 

            //if (lvFailCourse.Items.Count > 0)

            //int changeSub = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL LEFT JOIN ACD_DCR D ON (AL.idno=D.idno)", "Count(AL.idno)", "((ADMIN_APPROVE=1 AND ADVISOR_APPROVE=1) or (RECIEPT_CODE='REF' and TRANSACTIONSTATUS='Success')) AND AL.idno=" + Convert.ToInt32(Session["idno"])));


            int changeSub = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL LEFT JOIN ACD_DCR D ON (al.idno=d.idno and al.semesterno=d.semesterno and al.sessionno=d.sessionno and  al.semesterno=d.semesterno AND ISNULL(ApplyCourse,0)=1 and RECIEPT_CODE='REF' )", "Count(AL.idno)", "(ADMIN_APPROVE=1 AND ADVISOR_APPROVE=1)   and recon=1 and RECIEPT_CODE='REF' AND AL.idno=" + Convert.ToInt32(Session["idno"]) + "AND AL.SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND AL.SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "AND EXAMNO="+Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue)));

         // string REGNO3 = objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + " and SUBEXAMNO=" + ddlsubexamname.SelectedValue + " and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 and STUDENT_REQUEST=1 AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"]) + "");
//select * from  ACD_ABSENT_STUD_EXAM_REG_LOG AL --where AL.idno=3280 and recon=1
// LEFT JOIN ACD_DCR D ON  (al.idno=d.idno and al.semesterno=d.semesterno and al.sessionno=d.sessionno and  al.semesterno=d.semesterno AND ISNULL(ApplyCourse,0)=1 and RECIEPT_CODE='REF' )
//where  al.idno=3280 and recon=1 and RECIEPT_CODE='REF' and(ADMIN_APPROVE=1 AND ADVISOR_APPROVE=1)
            int Idno = Convert.ToInt32(Session["idno"]);

            #region for change subject commented bescuse of requirement change
            //if (changeSub >= 1)
            //{

            //    if (changeSub > 1 || lvFailCourse.Items.Count > 1)
            //    {

            //        objCommon.DisplayMessage(pnlFailCourse,"Student is eligible only same subject Change!", this.Page);
            //        foreach (ListViewDataItem item in lvFailCourse.Items)
            //        {
            //            CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
            //            CheckId.Checked = false;
            //        }
            //        return;

            //    }
            //    else
            //    {


            //        StudentController objSC1 = new StudentController();
            //        DataSet dsStudent1 = objSC1.GetStudentDetailsExamRetest(Convert.ToInt32(Session["idno"]));

            //        if (dsStudent1 != null && dsStudent1.Tables.Count > 0)
            //        {
            //            if (dsStudent1.Tables[0].Rows.Count > 0)
            //            {
            //                lblScheme.ToolTip = dsStudent1.Tables[0].Rows[0]["SCHEMENO"].ToString();
            //                lblSemester.ToolTip = dsStudent1.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            //                int Ua_No = Convert.ToInt32(Session["userno"].ToString());
            //                string examname = string.Empty;
            //                examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + (ddlexamnameabsentstudent.SelectedValue));
            //                string subexamname = string.Empty;
            //                subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));
            //                int Sessionno = Convert.ToInt32(Session["sessionno_current"]);
            //                // int count = 0;
            //                int studentreq = 1;
            //                // return;
            //                if (lvFailCourse.Items.Count > 0)
            //                {
            //                    foreach (ListViewDataItem item in lvFailCourse.Items)
            //                    {
            //                        //lblCCode
            //                        CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
            //                        HiddenField hdfexistMarks = item.FindControl("hdfexistMarks") as HiddenField;
            //                        Label lblCCode = item.FindControl("lblCCode") as Label;
            //                        Label fees = item.FindControl("lblAmt") as Label;

            //                        if (CheckId.Checked == true)
            //                        {

            //                            int courseapply = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND isnull(ADMIN_APPROVE,0)=0 and isnull(ADVISOR_APPROVE,0)=0"));
            //                            if (courseapply >= 1)
            //                            {

            //                                foreach (ListViewDataItem items in lvFailCourse.Items)
            //                                {
            //                                    CheckId.Checked = false;

            //                                }
            //                                objCommon.DisplayMessage(pnlFailCourse,"Student is eligible only for anyone CAT EXAM!", this.Page);
            //                                return;
            //                            }

            //                            int COURSENO = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COURSENO", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND isnull(STUDENT_REQUEST,0)=1 and isnull(ADMIN_APPROVE,0)=1 AND  isnull(ADVISOR_APPROVE,0)=1"));
            //                            if (COURSENO != Convert.ToInt32(lblCCode.ToolTip))
            //                            {
            //                                objCommon.DisplayMessage(pnlFailCourse,"Student is eligible only same subject Change!", this.Page);
            //                                return;
            //                            }

            //                            //return; 
            //                            count++;

            //                            string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW_course_change";
            //                            string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_OUT";
            //                            string Call_Values = "" + Idno + "," + Convert.ToInt32(Sessionno) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(lblSemester.ToolTip) + "," + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + "," + Convert.ToInt32(ddlsubexamname.SelectedValue) + "," + Session["userno"] + "," + examname + ",'" + subexamname + "'," + hdfexistMarks.Value + "," + studentreq + "," + fees.ToolTip + ",1";

            //                            // return;

            //                            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            //                            if (que_out == "0")
            //                            {
            //                                UpdateLock_status();
            //                                objCommon.DisplayMessage(pnlFailCourse,"Course Change Sucessfully", this.Page);
            //                                CheckId.Checked = true;
            //                                CheckId.Enabled = false;
            //                                btnsave.Visible = false;
            //                                btnSubmit.Visible = true;
            //                                return;
            //                            }
            //                            else
            //                            {
            //                                UpdateLock_status();
            //                                objCommon.DisplayMessage(pnlFailCourse,"Course Change Sucessfully", this.Page);
            //                                CheckId.Checked = true;
            //                                CheckId.Enabled = false;
            //                                btnsave.Visible = false;
            //                                btnSubmit.Visible = true;
            //                                return;

            //                            }


            //                        }
            //                    }



            //                }
            //                if (count == 0)
            //                {
            //                    objCommon.DisplayMessage(pnlFailCourse,"Please Select Atleast one Student from the list", this.Page);
            //                    return;
            //                }

            //            }
            //        }
            //    }

            //}

            //else
            //{
            //    //not sub change            
            //}


            #endregion
            StudentController objSC = new StudentController();
            int idno = 0;
            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }

            // Get Student Details
            DataSet dsStudent = objSC.GetStudentDetailsExamRetest(idno);

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();//SCHEMENO
                    //  lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();//SemesterNo                                    
                    int Ua_No = Convert.ToInt32(Session["userno"].ToString());
                    string examname = string.Empty;
                    examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + (ddlexamnameabsentstudent.SelectedValue));
                    string subexamname = string.Empty;
                    subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + (ddlsubexamname.SelectedValue));
                    int Sessionno = Convert.ToInt32(Session["sessionno_current"]);
                    // STUDENT_REQUEST
                    // int count = 0;
                    int studentreq = 1;

                    if (lvFailCourse.Items.Count > 0)
                    {
                        foreach (ListViewDataItem item in lvFailCourse.Items)
                        {
                            //lblCCode
                            CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                            HiddenField hdfexistMarks = item.FindControl("hdfexistMarks") as HiddenField;
                            Label lblCCode = item.FindControl("lblCCode") as Label;
                            Label fees = item.FindControl("lblAmt") as Label;
                            if (CheckId.Checked == true)
                            {

                                int courseapply = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND isnull(ADMIN_APPROVE,0)=0 and isnull(ADVISOR_APPROVE,0)=0 and EXAMNO!=" + ddlexamnameabsentstudent.SelectedValue + "And SUBEXAMNO!=" + ddlsubexamname.SelectedValue));
                                if (courseapply >= 1)
                                {
                                    // ADDED NEW GAURAV 13_11_2023

                                    //if (courseapply == 1)
                                    //{
                                    int CHECKSAMECOURSE = 0;
                                    int countappl = 0;
                                    foreach (ListViewDataItem item1 in lvFailCourse.Items)
                                    {
                                         CHECKSAMECOURSE = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND isnull(ADMIN_APPROVE,0)=0 and isnull(ADVISOR_APPROVE,0)=0 and EXAMNO!=" + ddlexamnameabsentstudent.SelectedValue + "And SUBEXAMNO!=" + ddlsubexamname.SelectedValue + " AND COURSENO=" + Convert.ToInt32(lblCCode.ToolTip)));
                                         if (CHECKSAMECOURSE == 1)
                                         countappl++;
                                    }

                                    if (countappl >= 1)
                                    {
                                        objCommon.DisplayMessage(pnlFailCourse,"Student is eligible only for anyone CAT EXAM!", this.Page);
                                        CheckId.Checked = false;
                                        return;
                                    
                                    }


                                  //  }





                                    //END
                                  // objCommon.DisplayMessage(pnlFailCourse,"Student is eligible only for anyone CAT EXAM!", this.Page);
                                  //
                                  // CheckId.Checked = false;
                                  // return;
                                }

                                #region NOT IN USE
                                // int checkpayment = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "Count(idno)", "RECIEPT_CODE='REF' AND idno=" + Convert.ToInt32(Session["idno"]) + " AND  TRANSACTIONSTATUS='Success'"));


                                //int checkApply = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "Count(idno)", "STUDENT_REQUEST=1 and ADMIN_APPROVE=1 and ADVISOR_APPROVE=1 AND idno=" + Convert.ToInt32(Session["idno"])));


                                //int Idno = Convert.ToInt32(Session["idno"]);



                                //if (checkpayment != 0 ||checkpayment !=null)
                                //{
                                //    string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW_bk_15_09_2022";
                                //    string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_APPLYCOURSE,@P_OUT";
                                //    string Call_Values = "" + Idno + "," + Convert.ToInt32(Sessionno) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(lblSemester.ToolTip) + "," + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + "," + Convert.ToInt32(ddlsubexamname.SelectedValue) + "," + Session["userno"] + "," + examname + ",'" + subexamname + "'," + hdfexistMarks.Value + "," + studentreq + "," + fees.ToolTip + "," + checkApply +",1";



                                //    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

                                //}
                                //return;
                                #endregion
                                //int Idno = Convert.ToInt32(Session["idno"]);

                                count++;
                                //return;
                                #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
                                if (Idno > 0)
                                {
                                    string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW";
                                    string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_OUT";
                                    string Call_Values = "" + Idno + "," + Convert.ToInt32(Sessionno) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(lblSemester.ToolTip) + "," + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + "," + Convert.ToInt32(ddlsubexamname.SelectedValue) + "," + Session["userno"] + "," + examname + ",'" + subexamname + "'," + hdfexistMarks.Value + "," + studentreq + "," + fees.ToolTip + ",1";

                                    // return;

                                    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                                    if (que_out == "0")
                                    {
                                        objCommon.DisplayMessage(pnlFailCourse,"Course Update Sucessfully", this.Page);
                                        paybutton();
                                        bindcourses();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(pnlFailCourse,"Course Apply Sucessfully", this.Page);
                                        paybutton();
                                        bindcourses();
                                    }
                                    //ret = Convert.ToInt32(objExamController.GetUpdateAbsentEntry(Convert.ToInt32(ddlsessionforabsent.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlcourseforabset.SelectedValue), Convert.ToInt32(Idno)));
                                    // ret++;
                                }
                                //foreach (ListViewDataItem item1 in lvFailCourse.Items)
                                //{

                                //    HiddenField courseapplied = item.FindControl("hdfstudapplied") as HiddenField;
                                //    CheckBox CheckId1 = item.FindControl("chkAccept") as CheckBox;
                                //    if (CheckId1.Checked == true)
                                //    {

                                //        CheckId1.Enabled = false;
                                //    }
                                //    else
                                //    {
                                //        CheckId1.Enabled = true;
                                //    }

                                //}


                                #endregion

                            }
                        }
                        if (count == 0)
                        {
                            objCommon.DisplayMessage(pnlFailCourse,"Please Select Atleast one Student from the list", this.Page);
                            return;
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objUaimsCommon.ShowError(Page, "Academic_Examination_ExamRegistration.btnsave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ChckedTotal_change(object sender, EventArgs e)
    {
        //  static
        double fees = 0;
        double amtfinal;
        lblTotalExamFee.Text = "0.00";
        int courseno = 0;
        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            Label amt = dataitem.FindControl("lblAmt") as Label;
            int  course = Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip);
           // int  courseno1 = course.ToolTip
            if (chk.Checked == true)
            {



                 courseno = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COURSENO", " isnull(STUDENT_REQUEST,0)=1 AND   isnull(ApplyCourse,0)=1 AND idno=" + Convert.ToInt32(Session["idno"]) + "and EXAMNO!=" + ddlexamnameabsentstudent.SelectedValue + "AND SUBEXAMNO!=" + ddlsubexamname.SelectedValue + " AND COURSENO=" + course + " UNION ALL SELECT 0 AS SESSION_NO"));
                if (courseno == Convert.ToInt32(course))
                {
                    objCommon.DisplayMessage(pnlFailCourse,"Can not apply for same subject !!!", this.Page);
                    chk.Checked = false;
                    return;
                }
               

            }


            if (chk.Checked == true && chk.Enabled==true)
            {           
            

                   // int REGNO2 = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(IDNO)", "IDNO='" + Session["idno"] + "' and STUDENT_REQUEST=1 and EXAMNO=" + ddlexamnameabsentstudent.SelectedValue + "and SUBEXAMNO=" + ddlsubexamname.SelectedValue+ "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionno_current"])+""));

                   // if (REGNO2 > 0)
                  //  {
                   //     fees = REGNO2 * 350;
                   //     lblTotalExamFee.Text = fees.ToString() + ".00";
                   // }
                   // else
                   // {
                        fees = 350;
                        lblTotalExamFee.Text = (Convert.ToDouble(lblTotalExamFee.Text) + Convert.ToDouble(fees)).ToString() + ".00";                       
                        btnsave.Visible = true;
                        btnSubmit.Visible = false;

                 //   }
              

            }
            else
            {



            }
        }







    }

    protected void UpdateLock_status()
    {
        int changeSub = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG AL LEFT JOIN ACD_DCR D ON (AL.idno=D.idno)", "Count(AL.idno)", "ADMIN_APPROVE=1 AND ADVISOR_APPROVE=1 and  RECIEPT_CODE='REF' and TRANSACTIONSTATUS='Success' AND AL.idno=" + Convert.ToInt32(Session["idno"])));
        if (changeSub >= 1)
        {

            //int session = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "TOP 1 (SESSIONNO)", "IDNO=" + Convert.ToInt32(Session["idno"])));
            DataSet dsstudent = null;
            dsstudent = objSC.GetRetestStudentDetailsExam(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["sessionno_current"]));


        }



    }

}



