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
using System.Collections.Generic;

public partial class ACADEMIC_REPORTS_Re_medial_ExamRegistration_CC : System.Web.UI.Page
{
    #region Class
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    Student_Acd objSA = new Student_Acd();
    StudentFees objStudentFees = new StudentFees();
    StudentRegist objSR = new StudentRegist();
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
                //Page Authorization
                this.CheckPageAuthorization();
                // Session["payactivityno"] = "2";                
                Page.Title = Session["coll_name"].ToString();
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;
                // LATE FEE PATCH BY NARESH BEERLA ON DT 18062022 FOR USING IN JAVASCRIPT 
                DateTime ExamLateDate = Convert.ToDateTime(objCommon.LookUp("reff", "Exam_Last_Date", ""));
                hdfExamLastDate.Value = ExamLateDate.ToString("dd/MM/yyyy");
                decimal ExamLateFee = Convert.ToDecimal(objCommon.LookUp("reff", "Exam_Late_Fee_Amt", ""));
                hdfExamLateFee.Value = ExamLateFee.ToString();
                int cid = 0;
                int idno = 0;

                idno = Convert.ToInt32(Session["idno"]);
                cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));


                if (CheckActivityCollege(cid))
                {
                    if (ViewState["usertype"].ToString() == "2")
                    {

                        //txtEnrollno.Text = string.Empty;
                        //btnSearch.Visible = false;
                        //btnCancel.Visible = false;
                        //divCourses.Visible = true;
                        //pnlSearch.Visible = false;
                        this.ShowDetails();
                        // this.bindcalculationfees();
                        #region FOR ATLAS PAY BUTTON


                        int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Convert.ToInt32(lblSemester.ToolTip) + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND SUBID>0 AND FEETYPE=1 AND ISNULL(IsProFeesApplicable,0)=1 AND ISNULL(CANCEL,0)=0"));
                        if (CheckExamfeesApplicable > 0)
                        {
                            //for Total exam Fees Lable Sum
                            lblTotalExamFee.Text = "0.00";
                            lblfessapplicable.Text = "0.00";
                            decimal TotalApplicablefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "SUM(ApplicableFee)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip.ToString() + "%' AND DEGREENO LIKE '%" + hdfDegreeno.Value.ToString() + "%' AND SUBID>0 AND FEETYPE=1 AND ISNULL(CANCEL,0)=0"));


                            lblfessapplicable.Text = TotalApplicablefees.ToString();
                            decimal totalfees = Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text);
                            FinalTotal.Text = totalfees.ToString();

                        }
                        else
                        {

                        }
                        #endregion
                        btnSubmit.Visible = false;
                        btnPrintRegSlip.Visible = false;

                    }
                }

                else
                {

                }

                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];

                ViewState["CHK_All_TotalAmtTheory"] = 0;
                ViewState["CHK_All_TotalAmtPractical"] = 0;
                ViewState["CHK_All_FinalTotal_T"] = 0;
                ViewState["TotalAmtTheory"] = 0;
                ViewState["TotalAmtPractical"] = 0;
                ViewState["CHK_FinalTotal_TP"] = 0;
                ViewState["CHK_All_FinalTotal_P"] = 0;
            }
        }

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
            int col_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));

            sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%') ");
            // sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%' AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + col_id + " UNION ALL SELECT 0 AS SESSION_NO");

            if (sessionno == string.Empty)
            {
                Session["sessionnonew"] = 0;
            }
            // sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)  INNER JOIN ACD_SESSION_MASTER SM ON (SA.SESSION_NO = SM.SESSIONNO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1  AND SM.COLLEGE_ID IN(" + col_id + ")"); //UNION ALL SELECT 0 AS SESSION_NO


            Session["sessionnonew"] = sessionno;

            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;

                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }

            }
            else
            {

                //divenroll.Visible = false;
                //btnSearch.Visible = false;
                //btnCancel.Visible = false;
                // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
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
            // ViewState["sessionno"] = sessionno;
            Session["sessionno"] = sessionno;
            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

            if (dtr.Read())
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;

                }

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    //dvMain.Visible = false;
                    ret = false;
                }

            }
            else
            {
                // objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // dvMain.Visible = false;
                ret = false;
            }
            dtr.Close();
            return ret;

        }
    }
    private void FillDropdown()
    {

        //objCommon.FillDropDownList(ddlclgname, "ACD_COLLEGE_MASTER SM", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "SM.COLLEGE_ID DESC");


        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + ViewState["ACTIVITY_NO"], "");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //ViewState["degreenos"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //ViewState["branchnos"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }
        //ddlSession.Focus();
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
        //lvFailInaggre.DataSource = null;
        //lvFailInaggre.DataBind();      
        btnPrintRegSlip.Visible = true;
        int idno = 0;
        // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        try
        {
            if (idno > 0)
            {
                divCourses.Visible = true;
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);
                ViewState["semesternos"] = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        //if (ViewState["semesternos"].ToString().Contains(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()))
                        // {

                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
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
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblBacklogFine.Text = "0";
                        lblTotalFee.Text = "0";


                        #region ADD FOR BACKLOG SEMESTER dropdown

                        // int oddevensem = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"])));                                                    
                        objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT_HIST H INNER JOIN ACD_SEMESTER S ON (H.SEMESTERNO=S.SEMESTERNO)", "DISTINCT H.SEMESTERNO", "DBO.FN_DESC('SEMESTER',H.SEMESTERNO)SEMESTER", " idno =" + Convert.ToInt32(Session["idno"]) + "  AND S.SEMESTERNO > 0  and isnull(CANCEL,0)=0 ", "SEMESTERNO");//AND  GDPOINT=0 
                        #endregion



                        // ADDED BY NARESH BEERLA FOR CALCULATING THE FINAL SEMESTER AMOUNT IN JAVASCRIPT ON DT 18062022 
                        #region NOT IN USE
                        int Duration = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO= CDB.DEGREENO)", "DISTINCT DURATION", "D.DEGREENO=" + hdfDegreeno.Value));
                        Duration = Convert.ToInt32(Duration) * 2;
                        hdfDuration.Value = Duration.ToString();
                        hdfSemester.Value = (lblSemester.ToolTip).ToString();
                        #endregion

                        //  }
                        //  else
                        // {
                        // IsNotActivitySem = true;
                        //  }
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;
                        flag = false;
                        //ddlclgname.ClearSelection();
                        //ddlSession.ClearSelection();
                        //txtEnrollno.Text = "";
                        //ddlSession.Enabled = true;
                        //txtEnrollno.Enabled = true;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;
                    flag = false;

                    //ddlclgname.ClearSelection();
                    //ddlSession.ClearSelection();
                    //txtEnrollno.Text = "";

                    //ddlSession.Enabled = true;
                    //txtEnrollno.Enabled = true;


                }
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                divCourses.Visible = false;
                flag = false;

                //ddlclgname.ClearSelection();
                //ddlSession.ClearSelection();
                //txtEnrollno.Text = "";

                //ddlSession.Enabled = true;
                //txtEnrollno.Enabled = true;


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
        try
        {
            //string orderid = objCommon.LookUp("ACD_DCR", "ORDER_ID", "idno=" + Convert.ToInt32(Session["idno"]) + " AND RECIEPT_CODE='AEF' AND SEMESTERNO=" + ddlBackLogSem.SelectedValue);
            // int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(orderid) + "'"));
            int IDNO = Convert.ToInt32(Convert.ToInt32(Session["idno"]));
            //int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COLLEGE_CODE", "ORDER_ID='" + Convert.ToString(orderid) + "'"));
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + IDNO));
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + IDNO));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + IDNO));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"]) + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + ddlBackLogSem.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {

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
            //// idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            //string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            //if (REGNO != null && REGNO != string.Empty && REGNO != "")
            //{
            //    idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            //}
            //else
            //{
            //    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
            //    return;
            //}

        }
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT(SCHEMENO)", "IDNO = " + idno + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
        int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));
        ShowReport("ExamRegistrationSlip", "rptExamRegslipNit.rpt");
    }


    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + cid + " UNION ALL SELECT 0 AS SESSION_NO");

        //sessionno = Session["currentsession"].ToString();
        Session["sessionnonew"] = sessionno;
        //  Session["sessionnonew"]
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                //dvMain.Visible = false;
                ret = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                //dvMain.Visible = false;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            //txtEnrollno.Text = string.Empty;
            // dvMain.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }


    protected void bindcourses()
    {

        int idno = 0;
        int sessionno = Convert.ToInt32(Session["sessionnonew"]);
        StudentController objSC = new StudentController();
        DataSet dsFailSubjects;
        idno = Convert.ToInt32(Session["idno"]);

        if (idno == null || idno == 0)
        {
            objCommon.DisplayMessage("No Record Found...", this.Page);
            return;

        }
        else
        {
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
            ViewState["clg_id"] = clg_id;


            dsFailSubjects = objSC.GetStudentFailExamSubjectSNew_sem_REMEDAL(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno, Convert.ToInt32(ddlBackLogSem.SelectedValue));

            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                ViewState["oldsession"] = dsFailSubjects.Tables[0].Rows[0]["SESSIONNO"].ToString();
            }
            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                //lvFailCourse.DataSource = dsFailSubjects;
                //lvFailCourse.DataBind();
                //lvFailCourse.Visible = true;
                //divCourses.Visible = true;             
                //pnlFailCourse.Visible = true;



                //###########################

                string CHECKFEESTYPE = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEESTRUCTURE_TYPE", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=1 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0");
                ViewState["FEESTYPE"] = CHECKFEESTYPE;
                if (CHECKFEESTYPE == string.Empty || CHECKFEESTYPE == null)
                {
                    ViewState["FEESTYPE"] = 0;
                }
                if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

                    btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;
                    btnPay.Visible = true;
                    btnPay.Enabled = true;
                    btnPrintRegSlip.Visible = true;
                    lblfessapplicable.Text = "";
                    lblCertificateFee.Text = "";
                    lblTotalExamFee.Text = "";
                    lblLateFee.Text = "";
                    FinalTotal.Text = "";

                }

                else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)
                {

                    lvFailCourse.DataSource = dsFailSubjects;
                    lvFailCourse.DataBind();
                    CalculateTotalCredit();


                    //int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='EF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + ddlBackLogSem.SelectedValue + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='AEF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));


                    if (paysuccess > 0)
                    {
                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + ddlBackLogSem.SelectedValue + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='AEF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));

                        btnPrintRegSlip.Visible = true;
                        btnSubmit.Visible = false;
                        btnPay.Visible = false;
                        lblLateFee.Text = "";
                        lblfessapplicable.Text = "";
                        lblCertificateFee.Text = "";
                        lblTotalExamFee.Text = "";
                        //lblpapervalMax.Text = "";
                        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();


                    }
                    else
                    {
                        btnPrintRegSlip.Visible = false;
                        btnPay.Visible = false;
                        btnSubmit.Visible = false;
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;

                    }
                }

                else
                {
                    CalculateTotal();

                }
                //#region FIXTYPE
                //if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)//FIX  
                //{
                //    CalculateTotalFixFee();//FIX               
                //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                //    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                //    if (paysuccess > 0)
                //    {
                //        //btnPrintRegSlip.Visible = true;
                //        //btnSubmit.Visible = false;
                //        //btnPay.Visible = false;



                //        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));

                //        btnPrintRegSlip.Visible = true;
                //        btnSubmit.Visible = false;
                //        btnPay.Visible = false;
                //        lblLateFee.Text = "";
                //        lblfessapplicable.Text = "";
                //        lblCertificateFee.Text = "";
                //        lblTotalExamFee.Text = "";
                //        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                //    }
                //    else
                //    {
                //        btnPrintRegSlip.Visible = false;
                //        btnPay.Visible = true;
                //        btnSubmit.Visible = false;

                //    }
                //}
                //#endregion
                //return;
                //##############################
                //else
                //{
                //CalculateTotal();

                //}
                //int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND TRANSACTIONSTATUS='Success' AND  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                //if (paysuccess > 0)
                //{
                //    btnPrintRegSlip.Visible = true;
                //    btnSubmit.Visible = false;
                //}
                //else
                //{
                //    btnPrintRegSlip.Visible = false;
                //    btnSubmit.Visible = true;

                //}
                lvFailCourse.DataSource = dsFailSubjects.Tables[0];
                lvFailCourse.DataBind();
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                pnlFailCourse.Visible = true;

                lvPractical.DataSource = dsFailSubjects.Tables[1];
                lvPractical.DataBind();
                lvPractical.Visible = true;
                pnlFailCoursePractical.Visible = true;

            }
            else
            {

                objCommon.DisplayMessage(updatepnl, "No Courses found...!! !!", this.Page);
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                lvPractical.DataSource = null;
                lvPractical.DataBind();
                lvPractical.Visible = false;
                divCourses.Visible = true;
                btnSubmit.Visible = false;
                btnPrintRegSlip.Visible = false;
                btnSubmit.Visible = false;
                btnPrintRegSlip.Visible = false;
                lblTotalExamFee.Text = "0.00";
                FinalTotal.Text = "0.00";
                return;


            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            #region GET STUDENT DETALS

            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();
            StudentController objSC1 = new StudentController();
            int OrganizationId = 0, degreeno = 0, college_id = 0;
            int idno = 0;
            idno = Convert.ToInt32(Session["idno"]);
            string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
            objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
            objSR.IDNO = idno;
            objSR.REGNO = Regno;
            objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "IDNO=" + idno);
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.COURSENOS = string.Empty;
            objSR.SEMESTERNOS = string.Empty;
            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int cntcourse = 0;
            int cntPractical = 0;
            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;
            objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
            objSA.IpAddress = ViewState["ipAddress"].ToString();
            objSR.EXAM_REGISTERED = 0;
            objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
            objSR.TotalFee = objSR.Backlogfees;

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                Label sem = dataitem.FindControl("lblsem") as Label;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
                    objSR.SEMESTERNOS += ((dataitem.FindControl("lblsem")) as Label).ToolTip + ",";//
                    objSR.SEMESTERNO = Convert.ToInt32(sem.ToolTip);
                }
            }
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
            //  objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd();

            foreach (ListViewDataItem dataitem in lvPractical.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                Label sem = dataitem.FindControl("lblsemPractical") as Label;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCodePractical")) as Label).ToolTip + ",";
                    objSR.SEMESTERNOS += ((dataitem.FindControl("lblsemPractical")) as Label).ToolTip + ",";//
                    objSR.SEMESTERNO = Convert.ToInt32(sem.ToolTip);
                }
            }
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
            //  objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd();


            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && (dataitem.FindControl("chkAccept") as CheckBox).Enabled == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                }
            }

            foreach (ListViewDataItem dataitem in lvPractical.Items)
            {
                if ((dataitem.FindControl("chkAcceptPractical") as CheckBox).Checked == true && (dataitem.FindControl("chkAcceptPractical") as CheckBox).Enabled == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCodePractical") as Label).ToolTip + "$";
                }
            }

            int A = lvFailCourse.Items.Count;
            int B = lvPractical.Items.Count;

            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true) //if (chk.Enabled == true)
                        cntcourse++;
                }
            }
            if (lvPractical.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvPractical.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                    if (chk.Checked == true) //if (chk.Enabled == true)
                        cntPractical++;
                }
            }

            if (cntcourse == 0 && cntPractical == 0)
            {
                //objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
                objCommon.DisplayMessage(updatepnl, "Please select atleast one course..!!", this.Page);
                return;
            }
            //else if (cntPractical == 0)
            //{
            //    //objCommon.DisplayMessage("Please Select Practical..!!", this.Page);
            //    objCommon.DisplayMessage(updatepnl, "Please Select Practical Courses..!!", this.Page);
            //    return;
            //}
            else
            {
                int ifPaidAlready = 0;
                ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'AEF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
                if (ifPaidAlready > 0)
                {
                    objCommon.DisplayMessage("RE-MEDAL Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                    return;
                }

            #endregion

                #region Add data in result

                int Total_Count = cntcourse + cntPractical;

                if (Total_Count > 4) //|| Total_Count < 4)
                {
                    objCommon.DisplayMessage(updatepnl, "Students can register for a maximum of 4 courses.", this.Page);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

                    return;
                }
                else
                {
                    //if (cntcourse > 3)
                    //{
                    //    objCommon.DisplayMessage(updatepnl, "Students can register for a maximum of 3 theory courses.", this.Page);
                    //    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

                    //    return;
                    //}
                    //else
                    //{
                    //if (lvFailCourse.Items.Count > 0)
                    //{
                    //    if (lvPractical.Items.Count > 0)
                    //    {
                    //int ret = objSReg.AddExamRegisteredBacklaog_CC(objSR);
                    int ret = objSReg.AddExamRegisteredBacklaog_CC_REMEDAL(objSR);

                    //if (ret == 1)
                    //{

                    //objCommon.DisplayMessage("Please Contact Admin!!", this.Page);

                    //}
                    //else if (ret==-99)
                    //{

                    //    objCommon.DisplayMessage("SOMETHING WENT WRONG!!!", this.Page);
                    //    return;
                    //}

                #endregion

                    #region FOr Udtate Student_Result_Table Column EXT_IND=1

                    if (lvFailCourse.Items.Count > 0)
                    {
                        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        {
                            //CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                            //int flag;

                            //Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                            //if (chk.Checked == true)
                            //{
                            //    flag = 1;
                            //}
                            //else
                            //{
                            //    flag = 0;
                            //}
                            //int Idno = Convert.ToInt32(Session["idno"]);
                            //int ccode = Convert.ToInt32(lblCCode.ToolTip);
                            ////falg=1;


                            //if (Idno > 0)
                            //{
                            //    string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSES_APPLY";
                            //    string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
                            //    // string Call_Values = "" + Idno + "," + Convert.ToInt32(ViewState["oldsession"]) + "," + ccode + ",0";
                            //    string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + flag + ",0";

                            //    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                            //}

                            // }
                        }
                    }
                    //   return;
                    #endregion

                    #region CREATE DEMAND

                    string coursenos = string.Empty;

                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        {
                            Label courseno = dataitem.FindControl("lblCCode") as Label;
                            coursenos += courseno.ToolTip + ",";
                        }
                    }
                    coursenos = coursenos.TrimEnd(',');

                    foreach (ListViewDataItem dataitem in lvPractical.Items)
                    {
                        if ((dataitem.FindControl("chkAcceptPractical") as CheckBox).Checked == true)
                        {
                            Label courseno = dataitem.FindControl("lblCCodePractical") as Label;
                            coursenos += courseno.ToolTip + ",";
                        }
                    }
                    coursenos = coursenos.TrimEnd(',');

                    StudentController objSC = new StudentController();
                    DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
                    string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
                    objSR.COURSENOS = coursenos;
                    objSR.IDNO = Convert.ToInt32(Session["idno"]);
                    objSR.REGNO = RegNo;
                    objSR.SCHEMENO = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString());
                    //objSR.SEMESTERNOS = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    objSR.SEMESTERNOS = ddlBackLogSem.SelectedValue;
                    objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    string Amt = FinalTotal.Text;
                    CreateStudentPayOrderId();
                    //CREATE DEMAND

                    int ret1 = objSReg.AddStudentBacklogExamRegistrationDetails(objSR, Amt, ViewState["OrderId"].ToString());
                    if (ret == -99)
                    {
                        objCommon.DisplayMessage("!!!", this.Page);
                        return;
                    }
                    //if (ret == 1)
                    //{

                    //    objCommon.DisplayMessage("Backlog Exam Registration Done Successfully!!!", this.Page);

                    //}
                    //else if (ret == -99)
                    //{

                    //    objCommon.DisplayMessage("SOMETHING WENT WRONG!!!", this.Page);
                    //    return;
                    //}
                    //}
                    //}

                    //  return;
                    #endregion

                    #region NOT IN USE



                    // DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 0, Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);
                    // if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    // {
                    //     if (ds1.Tables[0].Rows.Count > 1)
                    //     {
                    //
                    //     }
                    //     else
                    //     {
                    //         Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    //         string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    //         Response.Redirect(RequestUrl);
                    //         //Response.Redirect("http://localhost:55403/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/PayUOnlinePaymentRequest.aspx");
                    //
                    //     }
                    // }
                    #endregion

                    OrganizationId = Convert.ToInt32(Session["OrgId"]);
                    // Add For UAT 
                    //Session["payactivityno"] = "2";
                    int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    int DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    //COMMENT by GAurav 08_12_2022 for new pg 
                    //  DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, 1, Convert.ToInt32(Session["payactivityno"]), DEGREENO, cid);
                    // return;
                    #region NOT IN USE
                    //DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 1, Convert.ToInt32(Session["payactivityno"]));
                    // return;
                    //    if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (ds1.Tables[0].Rows.Count > 1)
                    //        {

                    //        }
                    //        else
                    //        {
                    //            string url = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    //            string secret = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    //            string Merchantkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    //            string Responseurl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();




                    //            DataSet dsStudent1 = objSC1.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
                    //            string orderID = ViewState["OrderId"].ToString();
                    //            string Amount = FinalTotal.Text;
                    //            //string Amount = "1.00";
                    //            string Name = dsStudent1.Tables[0].Rows[0]["STUDNAME"].ToString(); ;
                    //            string Phone_no = dsStudent1.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    //            string Email = dsStudent1.Tables[0].Rows[0]["EMAILID"].ToString();

                    //            //string url = ConfigurationManager.AppSettings["CashFree_URL"];
                    //            //string secret = ConfigurationManager.AppSettings["CashFree_secret"];
                    //            // string Merchantkey = ConfigurationManager.AppSettings["CashFree_Merchantkey"];


                    //            string data = "";
                    //            SortedDictionary<string, string> formParams = new SortedDictionary<string, string>();
                    //            formParams.Add("appId", Merchantkey);
                    //            formParams.Add("orderId", orderID);
                    //            formParams.Add("orderAmount", Amount);
                    //            formParams.Add("customerName", Name);
                    //            formParams.Add("customerPhone", Phone_no);
                    //            formParams.Add("customerEmail", Email);

                    //            formParams.Add("returnUrl", Responseurl);
                    //            // formParams.Add("returnUrl", "http://localhost:60066/PresentationLayer/Atlas_Payment_Response.aspx");
                    //            // formParams.Add("returnUrl", "http://atlasuniversityuat.mastersofterp.in/PresentationLayer/Atlas_Payment_Response.aspx");
                    //            foreach (var kvp in formParams)
                    //            {
                    //                data = data + kvp.Key + kvp.Value;
                    //            }

                    //            string signature = CreateToken(data, secret);
                    //            //Console.Write(signature);
                    //            string outputHTML = "<html>";
                    //            outputHTML += "<head>";
                    //            outputHTML += "<title>Merchant Check Out Page</title>";
                    //            outputHTML += "</head>";
                    //            outputHTML += "<body>";
                    //            outputHTML += "<center>Please do not refresh this page...</center>";  // you can put h1 tag here
                    //            //outputHTML += "<form id='redirectForm' method='post' action='https://www.gocashfree.com/checkout/post/submit'>";
                    //            // outputHTML += "<form id='redirectForm' method='post' action='https://test.cashfree.com/billpay/checkout/post/submit'>";
                    //            outputHTML += "<form id='redirectForm' method='post' action='" + url + "'>";
                    //            //outputHTML += "<form id='redirectForm' method='post' action='"+ url+"'>";

                    //            outputHTML += "<input type='hidden' name='appId' value='" + Merchantkey + "'/>";
                    //            outputHTML += "<input type='hidden' name='orderId' value='" + orderID + "'/>";
                    //            outputHTML += "<input type='hidden' name='orderAmount' value='" + Amount + "'/>";
                    //            outputHTML += "<input type='hidden' name='customerName' value='" + Name + "'/>";
                    //            outputHTML += "<input type='hidden' name='customerEmail' value='" + Email + "'/>";
                    //            outputHTML += "<input type='hidden' name='customerPhone' value='" + Phone_no + "'/>";
                    //            outputHTML += "<input type='hidden' name='returnUrl' value='" + Responseurl + "'/>";
                    //            // outputHTML += "<input type='hidden' name='returnUrl' value='http://localhost:60066/PresentationLayer/Atlas_Payment_Response.aspx'/>";//59566
                    //            //outputHTML += "<input type='hidden' name='returnUrl' value='http://atlasuniversityuat.mastersofterp.in/PresentationLayer/Atlas_Payment_Response.aspx'/>";//59566

                    //            outputHTML += "<input type='hidden' name='signature' value='" + signature + "'/>";
                    //            outputHTML += "<table border='1'>";
                    //            outputHTML += "<tbody>";
                    //            foreach (string keys in formParams.Keys)
                    //            {
                    //                outputHTML += "<input type='hidden' name='" + keys + "' value='" + formParams[keys] + "'>";
                    //            }
                    //            outputHTML += "</tbody>";
                    //            outputHTML += "</table>";
                    //            outputHTML += "<script type='text/javascript'>";
                    //            outputHTML += "document.getElementById('redirectForm').submit();";
                    //            outputHTML += "</script>";
                    //            outputHTML += "</form>";
                    //            outputHTML += "</body>";
                    //            outputHTML += "</html>";
                    //            Response.Write(outputHTML);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        objCommon.DisplayMessage("Something Went Wrong !!", this.Page);

                    //    }
                    #endregion

                    //}
                }
            }

            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = FinalTotal.Text;
            ViewState["studAmt"] = FinalTotal.Text;//hdnTotalCashAmt.Value;
            // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            DataSet dsStudent2 = objSC1.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
            // string orderID = ViewState["OrderId"].ToString();

            //string Amount = "1.00";
            Session["studName"] = dsStudent2.Tables[0].Rows[0]["STUDNAME"].ToString();
            Session["studPhone"] = dsStudent2.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            Session["studEmail"] = dsStudent2.Tables[0].Rows[0]["EMAILID"].ToString();

            Session["ReceiptType"] = "AEF";
            //Session["idno"] = hdfIdno.Value;
            Session["paysession"] = Convert.ToInt32(Session["sessionnonew"]);
            Session["paysemester"] = ddlBackLogSem.SelectedValue;
            Session["homelink"] = "Backlog_ExamRegistration_CC.aspx";
            Session["regno"] = dsStudent2.Tables[0].Rows[0]["REGNO"].ToString();
            Session["payStudName"] = dsStudent2.Tables[0].Rows[0]["STUDNAME"].ToString();
            Session["paymobileno"] = dsStudent2.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblBranch.Text;
            Session["YEARNO"] = lblAdmBatch.Text;
            //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.

            if (Session["OrgId"].ToString() == "6")
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }
            if (Session["OrgId"].ToString() == "8")
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }
            if (Session["OrgId"].ToString() == "9")
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }

            //**********************************End by Nikhil L.********************************************//
            FeeCollectionController objFee = new FeeCollectionController();

            //**********************************End by Nikhil L.********************************************//
            // cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));
            //int active=Convert.ToInt32(Session["payactivityno"]);
            // int payid = Convert.ToInt32(objCommon.LookUp("ACD_PG_CONFIGURATION", "PAY_ID", "ACTIVITY_NO=" + active ));
            int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVESTATUS=1 AND ACTIVITYNAME like '%Exam%'"));
            Session["payactivityno"] = payactivityno;  // 
            //int PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%PAYU%'"));
            //  Session["PAYID"] = PAYID;
            //Session["PAYID"] = 0;
            int PAYID = 0;

            PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "ISNULL(PAYID,0)", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%CC%'"));

            if (!string.IsNullOrEmpty(PAYID.ToString()))
            {
                Session["PAYID"] = PAYID;
            }
            else
            {
                objCommon.DisplayMessage(updatepnl, "Payment Gateway Configuration is not done..!!", this.Page);
                return;
            }


            DataSet ds2 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, Convert.ToInt32(Session["PAYID"]), Convert.ToInt32(Session["payactivityno"]), 0, 0);
            if (ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds2.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds2.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Session["AccessCode"] = ds2.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }
            else
            {
                objCommon.DisplayMessage(updatepnl, "Payment Configuration is not done for this session..!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamRegistration_CC.btnPay_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    #region ddlBackLogSem_SelectedIndexChanged

    protected void ddlBackLogSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBackLogSem.SelectedItem.Value == "0")
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            btnReport.Visible = false;
            btnShow.Visible = false;
            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = false;
            btnSubmit.Visible = false;
            btnPrintRegSlip.Visible = false;
            //lblfessapplicable.Visible = false;
            lblfessapplicable.Text = "";
            lblCertificateFee.Text = "";
            lblTotalExamFee.Text = "";
            lblLateFee.Text = "";
            FinalTotal.Text = "";
            btnPay.Enabled = false;
            btnPay.Visible = false;

            lvPractical.DataSource = null;
            lvPractical.DataBind();
            lvPractical.Visible = false;
        }
        else
        {

            string semester = ddlBackLogSem.SelectedValue;
            string sem = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  AND SEMESTER LIKE  '%" + semester + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'");

            if (sem == " " || sem == string.Empty)
            {
                objCommon.DisplayMessage(updatepnl, "Activity Not Started For Selected Semester !!", this.Page);
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                btnSubmit.Visible = false;
                btnPay.Enabled = false;
                btnPay.Visible = false;
                btnPrintRegSlip.Visible = false;
                //lblfessapplicable.Visible = false;
                lblfessapplicable.Text = "";
                lblCertificateFee.Text = "";
                lblTotalExamFee.Text = "";
                lblLateFee.Text = "";
                FinalTotal.Text = "";
                return;
            }


            bindcourses();


        }

    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        // ShowReport("BacklogRegistration", "rptOnlineReceiptbBacklog_ATLAS.rpt");
        //if (Convert.ToInt16(Session[""]))
        if (Session["OrgId"].ToString() == "6")
        {
            ShowReport("BacklogRegistration", "rptBacklog_Reg_ATLAS.rpt");
        }
        else
        {
            ShowReport("BacklogRegistration", "rptBacklog_Reg.rpt");
        }
    }



    #region Added for the Payment Calculations as per the checked Courses on 25052022
    decimal Amt = 0;
    decimal CourseAmtt = 0;
    //decimal C_Amt = 0;
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
        additional_info7 = Session["SESSIONNO"].ToString();




        ReturnURL = requestUrl; // "ttps://svce.mastersofterp.in/Academic/PhotoReval_Response.aspx";

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
    private void CreateStudentPayOrderId()
    {
        ViewState["OrderId"] = null;
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        //string Orderid = Convert.ToString((Convert.ToInt32(Session["IDNO"].ToString())) + (Convert.ToString(ViewState["Branch"].ToString())) + (Convert.ToString(ViewState["Semester"].ToString())) + ir);
        string Orderid = Convert.ToString((Convert.ToInt32(Session["IDNO"].ToString())) + (Convert.ToString(10)) + (Convert.ToString(2)) + ir);


        ViewState["OrderId"] = Orderid;
        Session["Order_id"] = Orderid;
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {

        try
        {

            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();
            StudentController objSC1 = new StudentController();
            int OrganizationId = 0, degreeno = 0, college_id = 0; int idno = 0;
            idno = Convert.ToInt32(Session["idno"]);
            string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
            objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);

            objSR.IDNO = idno;
            objSR.REGNO = Regno;
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.COURSENOS = string.Empty;
            objSR.SEMESTERNOS = string.Empty;
            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));

            objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "IDNO=" + idno);
            int cntcourse = 0;
            int cntcourse_practical = 0;
            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;
            objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
            objSA.IpAddress = ViewState["ipAddress"].ToString();
            objSR.EXAM_REGISTERED = 0;
            objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
            objSR.TotalFee = objSR.Backlogfees;

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                Label sem = dataitem.FindControl("lblsem") as Label;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + ",";
                    objSR.SEMESTERNOS += ((dataitem.FindControl("lblsem")) as Label).ToolTip + ",";//
                    objSR.SEMESTERNO = Convert.ToInt32(sem.ToolTip);
                }
            }
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
            //  objSR.SEMESTERNOS = objSR.SEMESTERNOS.TrimEnd();

            foreach (ListViewDataItem dataitem in lvPractical.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                Label sem = dataitem.FindControl("lblsemPractical") as Label;
                if (cbRow.Checked == true && cbRow.Enabled == true)
                {
                    objSR.COURSENOS += ((dataitem.FindControl("lblCCodePractical")) as Label).ToolTip + ",";
                    objSR.SEMESTERNOS += ((dataitem.FindControl("lblsemPractical")) as Label).ToolTip + ",";//
                    objSR.SEMESTERNO = Convert.ToInt32(sem.ToolTip);
                }
            }
            objSR.COURSENOS = objSR.COURSENOS.TrimEnd();

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && (dataitem.FindControl("chkAccept") as CheckBox).Enabled == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";


                }
            }

            foreach (ListViewDataItem dataitem in lvPractical.Items)
            {
                if ((dataitem.FindControl("chkAcceptPractical") as CheckBox).Checked == true && (dataitem.FindControl("chkAcceptPractical") as CheckBox).Enabled == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCodePractical") as Label).ToolTip + "$";


                }
            }

            #region CHECK BOX COUNT
            if (lvFailCourse.Items.Count > 0)
            {

                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    HiddenField hdREMEDAL = (HiddenField)dataitem.FindControl("hdREMEDAL");
                    if (chk.Checked == true && hdREMEDAL.Value == "0")
                        cntcourse++;
                }

            }

            if (lvPractical.Items.Count > 0)
            {

                foreach (ListViewDataItem dataitem in lvPractical.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                    HiddenField hdREMEDALPractical = (HiddenField)dataitem.FindControl("hdREMEDALPractical");
                    if (chk.Checked == true && hdREMEDALPractical.Value == "0")
                        cntcourse_practical++;
                }

            }

            if (cntcourse == 0 && cntcourse_practical == 0)
            {
                objCommon.DisplayMessage(updatepnl, "Please select atleast one course..!!", this.Page);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

                return;
            }

            //if (cntcourse_practical == 0)
            //{
            //    objCommon.DisplayMessage(updatepnl, "Please Select Practical Courses..!!", this.Page);
            //    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

            //    return;
            //}

            #endregion

            #region Add data in result

            int Total_Count = cntcourse + cntcourse_practical;

            if (Total_Count > 4) //|| Total_Count < 4)
            {
                objCommon.DisplayMessage(updatepnl, "Please Select maximum 4 Courses..!!", this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

                return;
            }
            else
            {
                //if (cntcourse > 3) // || cntcourse < 3
                //{
                //    objCommon.DisplayMessage(updatepnl, "Please Select only 3 Theory Courses..!!", this.Page);
                //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);

                //    return;
                //}
                //else
                //{
                //if (lvFailCourse.Items.Count > 0)
                //{
                //if (lvPractical.Items.Count > 0)
                //{
                int REMEDAL_COUNT = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*) COUNT", "ISNULL(REMEDAL,0)=1 AND IDNO=" + idno + " AND REGNO=" + Regno + " AND SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"]) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND PREV_STATUS=1 AND ISNULL(CANCEL,0)=0"));
                int REMEDAL_COUNT_TOTAL = REMEDAL_COUNT + Total_Count;

                if (REMEDAL_COUNT == 0)
                {
                    int ret = objSReg.AddExamRegisteredBacklaog_CC_REMEDAL(objSR);
                    //}

                    if (ret == 1)
                    {

                        // objCommon.DisplayMessage("Please Contact Admin!!", this.Page);
                        objCommon.DisplayMessage(updatepnl, "Re-medial Exam Registration Done Sucessfully.", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                    }
                    else if (ret == -99)
                    {

                        objCommon.DisplayMessage(updatepnl, "SOMETHING WENT WRONG!!!", this.Page);
                        return;
                    }
                }
                else if (REMEDAL_COUNT_TOTAL > 4)
                {
                    objCommon.DisplayMessage(updatepnl, "You can register only 4 subjects for Re-medial Exam Registration", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                }
                else if (cntcourse <= REMEDAL_COUNT_TOTAL) // REMEDAL_COUNT
                {
                    if (cntcourse_practical <= REMEDAL_COUNT_TOTAL) // REMEDAL_COUNT
                    {
                        if (REMEDAL_COUNT <= 4)
                        {
                            int ret = objSReg.AddExamRegisteredBacklaog_CC_REMEDAL(objSR);
                            //}

                            if (ret == 1)
                            {

                                // objCommon.DisplayMessage("Please Contact Admin!!", this.Page);
                                objCommon.DisplayMessage(updatepnl, "Re-medial Exam Registration Done Sucessfully.", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                            }
                            else if (ret == -99)
                            {

                                objCommon.DisplayMessage(updatepnl, "SOMETHING WENT WRONG!!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatepnl, "Already you have 4 courses registration for Re-medial. You can not have register another course", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                        }
                        //}
                        //}
                        //}
                    }
                    else
                    {
                        int Count_P = 4;
                        int Total_Course_P = Count_P - REMEDAL_COUNT;
                        objCommon.DisplayMessage(updatepnl, "You have select '" + Total_Course_P + "' course only", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                    }
                }
                else
                {
                    int Count_T = 4;
                    int Total_Course_T = Count_T - REMEDAL_COUNT;
                    objCommon.DisplayMessage(updatepnl, "You have select '" + Total_Course_T + "' course only", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                }
            }

            #endregion

            string coursenos = string.Empty;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {

                    Label courseno = dataitem.FindControl("lblCCode") as Label;
                    coursenos += courseno.ToolTip + ",";
                }

            }
            coursenos = coursenos.TrimEnd(',');

            foreach (ListViewDataItem dataitem in lvPractical.Items)
            {
                if ((dataitem.FindControl("chkAcceptPractical") as CheckBox).Checked == true)
                {

                    Label courseno = dataitem.FindControl("lblCCodePractical") as Label;
                    coursenos += courseno.ToolTip + ",";
                }

            }
            coursenos = coursenos.TrimEnd(',');


            //#region FOr Udtate Student_Result_Table Column txt_ind=1
            //if (lvFailCourse.Items.Count > 0)
            //{
            //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //    {
            //        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            //        Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            //        //if (chk.Checked == true)
            //        //{
            //        //    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            //        int flag;

            //        if (chk.Checked == true)
            //        {
            //            flag = 1;
            //        }
            //        else
            //        {
            //            flag = 0;
            //        }
            //        int Idno = Convert.ToInt32(Session["idno"]);
            //        int ccode = Convert.ToInt32(lblCCode.ToolTip);
            //        if (Idno > 0)
            //        {
            //            string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSES_APPLY_REMEDAL";
            //            //string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_OUT";
            //            string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
            //            // string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + ",0";
            //            string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + flag + ",0";
            //            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

            //            if (que_out == "1")
            //            {
            //                objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Done Sucessfully.", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //            }
            //            else
            //            {
            //                objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Update Sucessfully.", this.Page);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //            }

            //            //bindcourses();

            //        }

            //        //}
            //    }
            //}
            //#endregion

            //if (lvPractical.Items.Count > 0)
            //{
            //    foreach (ListViewDataItem dataitem in lvPractical.Items)
            //    {
            //        CheckBox chk = dataitem.FindControl("chkAcceptPractical") as CheckBox;
            //        Label lblCCode = dataitem.FindControl("lblCCodePractical") as Label;
            //        //if (chk.Checked == true)
            //        //{
            //        //    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            //        int flag;

            //        if (chk.Checked == true)
            //        {
            //            flag = 1;
            //        }
            //        else
            //        {
            //            flag = 0;
            //        }
            //        int Idno = Convert.ToInt32(Session["idno"]);
            //        int ccode = Convert.ToInt32(lblCCode.ToolTip);
            //        if (Idno > 0)
            //        {
            //            string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSES_APPLY_REMEDAL";
            //            //string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_OUT";
            //            string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
            //            // string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + ",0";
            //            string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + flag + ",0";
            //            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

            //            if (que_out == "1")
            //            {
            //                objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Done Sucessfully.", this.Page); ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //            }
            //            else
            //            {
            //                objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Update Sucessfully.", this.Page);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
            //                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            //            }

            //            //bindcourses();

            //        }

            //        //}
            //    }

            //    //bindcourses();
            //}

            bindcourses();

    #endregion
        }

        catch
        {

        }

    }

    private string CreateToken(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }

    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //tring lblfessapplicable= 
            //  lblfessapplicable.Text = "0";

            //ViewState["TotalAmtTheory"] = 0;
            //ViewState["TotalAmtPractical"] = 0;

            //ViewState["TotalAmtTheory"] = 0;
            //ViewState["TotalAmtPractical"] = 0;

            if (ViewState["TotalAmtTheory"] == string.Empty || ViewState["TotalAmtTheory"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtTheory"].ToString()))
            {
                ViewState["TotalAmtTheory"] = 0;
            }

            if (ViewState["TotalAmtPractical"] == string.Empty || ViewState["TotalAmtPractical"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["TotalAmtPractical"] = 0;
            }

            if (ViewState["CHK_FinalTotal_TP"] == string.Empty || ViewState["CHK_FinalTotal_TP"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_FinalTotal_TP"] = 0;
            }

            if (ViewState["CHK_All_FinalTotal_T"] == string.Empty || ViewState["CHK_All_FinalTotal_T"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_All_FinalTotal_T"] = 0;
            }

            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            }
            else
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        Label lblAmt = dataitem.FindControl("lblAmt") as Label;
                        HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                        HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                        HiddenField hdfSubid = dataitem.FindControl("hdfSubid") as HiddenField;
                        decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
                        if (cbRow.Checked == true)
                        {
                            Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                        }
                    }
                    //else
                    //{
                    //    Label lblAmt = dataitem.FindControl("lblAmt") as Label; // SM
                    //    C_Amt = Convert.ToDecimal(lblAmt.Text); // SM
                    //}
                }

                string TotalAmt = Amt.ToString();
                ViewState["TotalAmtTheory"] = TotalAmt; // SM
                //lblTotalExamFee.Text = ViewState["TotalAmtTheory"] + ViewState["TotalAmtPractical"].ToString(); //SM

                decimal TP_Amount_T = Convert.ToDecimal(ViewState["TotalAmtTheory"]) + Convert.ToDecimal(ViewState["TotalAmtPractical"]); // SM
                lblTotalExamFee.Text = TP_Amount_T.ToString(); // SM
                if (lblfessapplicable.Text == string.Empty)
                {
                    lblfessapplicable.Text = "0";
                }

                FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
                ViewState["CHK_FinalTotal_TP"] = FinalTotal.Text; // SM

                //if (C_Amt != 0)
                //{
                //    FinalTotal.Text = (Convert.ToDecimal(ViewState["FinalTotal"]) - Convert.ToDecimal(C_Amt)).ToString();
                //}

                //if (ViewState["CHK_All_FinalTotal_T"] != string.Empty || ViewState["CHK_All_FinalTotal_T"] != null || ViewState["CHK_All_FinalTotal_T"] != "0")
                //if (!string.IsNullOrEmpty(ViewState["CHK_All_FinalTotal_T"].ToString()))
                if ((ViewState["CHK_All_FinalTotal_T"]).ToString() != "0")
                {
                    decimal CHK_All_FinalTotal_T = 0;
                    decimal TotalAmtTheory = 0;
                    decimal AmtTheory = 0;
                    decimal CHK_All_FinalTotal_T_P = 0;
                    decimal FinalTotal_T_P = 0;

                    CHK_All_FinalTotal_T = Convert.ToDecimal(ViewState["CHK_All_FinalTotal_T"]);
                    TotalAmtTheory = Convert.ToDecimal(ViewState["TotalAmtTheory"]);
                    CHK_All_FinalTotal_T_P = Convert.ToDecimal(ViewState["CHK_All_FinalTotal_T_P"]);
                    AmtTheory = Convert.ToDecimal(CHK_All_FinalTotal_T) - Convert.ToDecimal(TotalAmtTheory);
                    FinalTotal_T_P = CHK_All_FinalTotal_T_P - AmtTheory;
                    FinalTotal.Text = FinalTotal_T_P.ToString();
                    lblTotalExamFee.Text = FinalTotal_T_P.ToString();
                }

                Amt = 0;
                CourseAmtt = 0;
            }
        }
        catch
        {

        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["CHK_All_TotalAmtTheory"] == string.Empty || ViewState["CHK_All_TotalAmtTheory"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtTheory"].ToString()))
            {
                ViewState["CHK_All_TotalAmtTheory"] = 0;
            }

            if (ViewState["CHK_All_TotalAmtPractical"] == string.Empty || ViewState["CHK_All_TotalAmtPractical"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_All_TotalAmtPractical"] = 0;
            }

            if (ViewState["CHK_All_FinalTotal_T"] == string.Empty || ViewState["CHK_All_FinalTotal_T"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_All_FinalTotal_T"] = 0;
            }

            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
            {
                CheckBox chckheader = (CheckBox)lvFailCourse.FindControl("chkAll");
                if (chckheader.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = true;
                    }
                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = false;
                    }
                }

                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            }
            else
            {
                //ListViewDataItem dataitem in lvFailCourse.ItemTemplate)
                CheckBox chckheader = (CheckBox)lvFailCourse.FindControl("chkAll");
                if (chckheader.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = true;
                    }

                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        {
                            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                            Label lblAmt = dataitem.FindControl("lblAmt") as Label;
                            HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                            HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                            decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
                            if (cbRow.Checked == true)
                            {
                                Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                            }
                        }
                    }
                    string TotalAmt = Amt.ToString();
                    ViewState["CHK_All_TotalAmtTheory"] = TotalAmt; // SM
                    decimal CHK_All_TP_Amount_T = Convert.ToDecimal(ViewState["CHK_All_TotalAmtTheory"]) + Convert.ToDecimal(ViewState["CHK_All_TotalAmtPractical"]); // SM
                    //lblTotalExamFee.Text = TotalAmt.ToString();
                    lblTotalExamFee.Text = CHK_All_TP_Amount_T.ToString(); // SM
                    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
                    ViewState["CHK_All_FinalTotal_T"] = FinalTotal.Text; // SM

                    decimal CHK_All_TP_Final_Amount_T = Convert.ToDecimal(ViewState["CHK_All_FinalTotal_T"]) + Convert.ToDecimal(ViewState["CHK_All_FinalTotal_P"]); // SM
                    ViewState["CHK_All_FinalTotal_T_P"] = CHK_All_TP_Final_Amount_T;

                    ViewState["TotalAmtTheory"] = 0; // SM
                    ViewState["TotalAmtPractical"] = 0; // SM
                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = false;
                        string TotalAmt = Amt.ToString();
                        lblTotalExamFee.Text = TotalAmt.ToString();
                        if (lblfessapplicable.Text == string.Empty)
                        {
                            lblfessapplicable.Text = "0";
                        }
                        decimal CHK_All_TP_Amount_T = Convert.ToDecimal(ViewState["CHK_All_TotalAmtPractical"]) - Convert.ToDecimal(ViewState["CHK_All_TotalAmtTheory"]); // SM
                        lblTotalExamFee.Text = CHK_All_TP_Amount_T.ToString(); // SM
                        FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                        ViewState["CHK_All_TotalAmtTheory"] = 0; // SM
                        ViewState["CHK_All_FinalTotal_T"] = 0;
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void checkboxEnable()
    {
        int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Convert.ToInt32(lblSemester.ToolTip) + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND SUBID>0 and FEETYPE=1 AND ISNULL(CANCEL,0)=0"));
        // int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + 5 + "%' AND DEGREENO LIKE '%" + 1 + "%' AND SUBID>0 AND FEETYPE=1"));
        if (CheckExamfeesApplicable > 0)
        {
            //need to add condition for without fee
            int cntcourse = 0;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {

                    Label cps = dataitem.FindControl("lblCourseName") as Label;
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (Convert.ToInt32(cps.ToolTip) == 1)
                        cntcourse++;

                }
                if (cntcourse == 0)
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        Label cps = dataitem.FindControl("lblCourseName") as Label;
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        if (Convert.ToInt32(cps.ToolTip) == 1)
                        {
                            chk.Checked = true;
                            // chk.Enabled = false;

                        }
                        else
                        {
                            chk.Checked = false;
                            //chk.Enabled = false;
                        }


                    }

                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        Label cps = dataitem.FindControl("lblCourseName") as Label;
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        CheckBox chkhead = dataitem.FindControl("chkAll") as CheckBox;

                        if (Convert.ToInt32(cps.ToolTip) == 1)
                        {
                            chk.Checked = true;
                            chk.Enabled = false;

                        }
                        else
                        {
                            chk.Enabled = false;

                        }
                    }

                }


            }
        }
        else
        {

            int cntcourse = 0;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    CheckBox chkhead = dataitem.FindControl("chkAll") as CheckBox;

                    HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                    HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;

                    //CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (Convert.ToInt32(hdfExamRegistered.Value) == 1)
                        cntcourse++;

                }
                if (cntcourse == 0)
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                        HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        if (Convert.ToInt32(hdfStudRegistered.Value) == 1)
                        {
                            chk.Checked = true;
                            // chk.Enabled = false;

                        }
                        else
                        {
                            chk.Checked = false;
                            //chk.Enabled = false;
                        }


                    }

                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                        HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        CheckBox chkhead = dataitem.FindControl("chkAll") as CheckBox;

                        if (Convert.ToInt32(hdfExamRegistered.Value) == 1)
                        {
                            chk.Checked = true;
                            chk.Enabled = false;

                        }
                        else
                        {
                            chk.Enabled = false;

                        }
                    }

                }


            }



        }


    }
    protected void CalculateTotal()
    {
        int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND SUBID>0 AND FEETYPE=1 AND ISNULL(IsFeesApplicable,0)=1 "));

        if (CheckExamfeesApplicable > 0)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblAmt = dataitem.FindControl("lblAmt") as Label;
                    HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                    HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                    if (lblAmt.Text == string.Empty)
                    {
                        lblAmt.Text = "0.00";
                    }
                    decimal CourseAmt = Convert.ToDecimal(lblAmt.Text.ToString());
                    if (cbRow.Checked == true)
                    {
                        Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                    }
                    CheckBox chckheader = (CheckBox)lvFailCourse.FindControl("chkAll");
                    //chckheader.Enabled = false;
                    string TotalAmt = Amt.ToString();

                    lblTotalExamFee.Text = TotalAmt.ToString();
                    if (lblfessapplicable.Text == string.Empty)
                    {
                        lblfessapplicable.Text = "0.00";
                    }

                    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();

                }
            }
        }


        else
        {
            string TotalAmt = Amt.ToString();
            decimal CourseAmt = Convert.ToDecimal(Amt.ToString());
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblAmt = dataitem.FindControl("lblAmt") as Label;
                    HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudentPaid") as HiddenField;
                    if (lblAmt.Text == string.Empty)
                    {
                        lblAmt.Text = "0.00";
                    }
                    CourseAmt = Convert.ToDecimal(lblAmt.Text);
                    // lblfessapplicable.Text="0.00";

                    if (cbRow.Checked == true)
                    {

                        Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                    }
                    CheckBox chckheader = (CheckBox)lvFailCourse.FindControl("chkAll");
                    //chckheader.Enabled = true;

                    lblTotalExamFee.Text = Amt.ToString();
                    // FinalTotal.Text = Convert.ToDecimal(lblTotalExamFee.Text);// + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                }
            }
            // lblTotalExamFee.Text = TotalAmt.ToString();
            FinalTotal.Text = lblTotalExamFee.Text;// + Convert.ToDecimal(lblfessapplicable.Text)).ToString();



        }

    }
    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0 || Convert.ToInt32(ViewState["FEESTYPE"]) == null)//NO_ FEE
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                HiddenField hdfexreg = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                HiddenField hdftxn = (HiddenField)e.Item.FindControl("hdfextind");
                HiddenField hdREMEDAL = (HiddenField)e.Item.FindControl("hdREMEDAL");
                CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;

                if (hdREMEDAL.Value == "1")
                {
                    chk.Checked = true;
                    //chk.Enabled = false;
                    chkhead.Checked = false;
                    //chkhead.Enabled = false;
                }
                else
                {
                    chk.Checked = false;
                    // chk.Enabled = false;
                }
            }
        }
        else
        {
            int ifPaidAlready = 0;
            ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'AEF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));

            if (ifPaidAlready > 0)
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                    HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                    CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;

                    if (hdf.Value == "1")
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        chkhead.Checked = false;
                        chkhead.Enabled = false;
                    }
                    else
                    {
                        chk.Checked = false;
                        chk.Enabled = false;

                    }
                }
            }
        }

        //int applycourse = 0;

        //int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
        ////CHECK FEES APPlCABLE OR NOT 
        //int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=0 AND COLLEGE_ID=" + cid + "  AND ISNULL(IsFeesApplicable,0)=1"));
        //if (CheckExamfeesApplicableOrNot >= 1)
        //{

        //    applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND STUD_EXAM_REGISTERED=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
        //    if (applycourse > 0)
        //    {
        //        if (e.Item.ItemType == ListViewItemType.DataItem)
        //        {
        //            CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
        //            HiddenField hdf = (HiddenField)e.Item.FindControl("hdfStudRegistered");
        //            CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
        //            if (hdf.Value == "1")
        //            {
        //                chk.Checked = true;
        //                chk.Enabled = false;
        //                chkhead.Checked = false;
        //                chkhead.Enabled = false;
        //            }
        //            else
        //            {
        //                chk.Checked = false;
        //                // chk.Enabled = false;

        //            }
        //        }
        //    }



        //}
        //else
        //{

        //    applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND STUD_EXAM_REGISTERED=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
        //    if (applycourse > 0)
        //    {
        //        if (e.Item.ItemType == ListViewItemType.DataItem)
        //        {
        //            CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
        //            HiddenField hdf = (HiddenField)e.Item.FindControl("hdfStudRegistered");
        //            CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
        //            if (hdf.Value == "1")
        //            {
        //                chk.Checked = true;
        //                chk.Enabled = false;
        //                chkhead.Checked = false;
        //                chkhead.Enabled = false;
        //            }
        //            else
        //            {
        //                chk.Checked = false;
        //                chk.Enabled = true;

        //            }
        //        }
        //    }
        //  }
    }

    protected void CalculateTotalFixFee()// FEESTRUCTURE_TYPE=4 fix
    {
        lblTotalExamFee.Text = "0.00";
        lblfessapplicable.Text = "0.00";
        decimal ProFess;
        decimal ApplFess;
        decimal CrettificateFee;
        decimal latefees;

        #region ChkProcessing Fee

        bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckProcFee == true)
        {
            ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        }
        else
        {
            ProFess = 0;
        }
        #endregion
        #region Certificate Fee Applicable
        bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckCrettificateFee == true)
        {

            CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        }
        else
        {
            CrettificateFee = 0;
        }
        #endregion
        #region LATE FEE
        DataSet dsStudent = null;
        string date = string.Empty;
        string strNewDate = string.Empty;
        string format = "dd/mm/yyyy";
        int type = 0;
        string dateString = string.Empty;
        decimal calculatelatefee = 0;
        string totalsubfee = string.Empty;
        bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (Latefee == true)
        {

            latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


            DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
                dateString = date.Substring(0, 11);
                DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
                strNewDate = dateTime.ToString("yyyy-mm-dd");
                type = Convert.ToInt32(ds.Tables[0].Rows[0]["TYPE"]);
            }
            string SP_Name = "ACD_CALCULATE_DAY_WEEK_MONTHLY";
            string SP_Parameters = "@P_DATE, @P_TYPE";
            string Call_Values = "" + strNewDate + "," + Convert.ToInt32(type) + "";// +"," + Convert.ToInt16(ViewState["sem"]) + "," + 
            dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);


            if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"]) > 0)
            {
                calculatelatefee = latefees * Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

            }
            else
            {
                calculatelatefee = 0;
            }
            // decimal abc = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);
            //calculatelatefee = latefees * abc;
            if (type == 1)
            {
                lblLateFee.Text = "<b style='color:red;'>PERDAY </b> : " + dateString + "  " + latefees.ToString();
            }
            else if (type == 1)
            {
                lblLateFee.Text = "<b style='color:red;'>WEEKLY </b> : " + dateString + "  " + latefees.ToString();
                // lblLateFee.Text = "WEEKLY   " + latefees.ToString();
            }
            else if (type == 3)
            {
                lblLateFee.Text = "<b style='color:red;'>MONTHLY </b> : " + dateString + "  " + latefees.ToString();

            }



        }
        else
        {
            latefees = 0;
        }
        #endregion


        ApplFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0 AND  ISNULL(CANCEL,0)=0 and FEESTRUCTURE_TYPE=4 AND    ISNULL(IsFeesApplicable,0)=1 and  COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

        lblfessapplicable.Text = ProFess.ToString();
        lblTotalExamFee.Text = ApplFess.ToString();
        lblCertificateFee.Text = CrettificateFee.ToString();
        // lblLateFee.Text = latefees.ToString();


        if (lblfessapplicable.Text == string.Empty)// || lblTotalExamFee.Text == string.Empty || lblCertificateFee.Text == string.Empty )
        {
            lblfessapplicable.Text = "0.00";
        }
        else if (lblTotalExamFee.Text == string.Empty)
        {
            lblTotalExamFee.Text = "0.00";
        }
        else if (lblCertificateFee.Text == string.Empty)
        {
            lblCertificateFee.Text = "0.00";
        }
        else if (lblLateFee.Text == string.Empty)
        {
            lblLateFee.Text = "0.00";
        }

        // }
        //FinalTotal.Text = (ProFess + ApplFess + CrettificateFee + latefees).ToString();
        // FinalTotal.Text = (ProFess + ApplFess + CrettificateFee).ToString();
        totalsubfee = (ProFess + ApplFess + CrettificateFee).ToString();//added for calculate seprate fee course 18_02_20235

        ViewState["TotalSubFee"] = totalsubfee;

        ViewState["latefee"] = calculatelatefee;
        FinalTotal.Text = (ProFess + ApplFess + CrettificateFee + calculatelatefee).ToString();
    }


    protected void CalculateTotalCredit()//FEESTRUCTURE_TYPE=5  CREDITWISE
    {
        string PAYID = string.Empty;
        // lblTotalExamFee.Text = "0.00";
        lblTotalExamFee.Text = string.Empty;
        lblfessapplicable.Text = string.Empty;
        //lblfessapplicable.Text = "0.00";
        string TotalAmt = string.Empty;
        //Processing Fee Applicable
        decimal ProFess;
        decimal CrettificateFee;
        decimal latefees;

        //decimal valuationfee;
        //decimal evaltotal;


        #region ChkPaper Valuation


        //  valuationfee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(ValuationMaxFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0  AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        //if (dsSubjects.Tables[0].Rows.Count > 3)
        //{
        //    valuationfee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(ValuationMaxFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0  AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


        //}
        //else
        //{

        //    evaltotal = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(ValuationFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=0  AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        //    valuationfee = evaltotal * dsSubjects.Tables[0].Rows.Count;

        //}
        #endregion

        #region ChkProcessing Fee

        bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckProcFee == true)
        {
            ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        }
        else
        {
            ProFess = 0;
        }
        #endregion
        #region Certificate Fee Applicable
        bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckCrettificateFee == true)
        {

            CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        }
        else
        {
            CrettificateFee = 0;
        }
        #endregion
        #region  LATE FEE Checklate fee applicable

        DataSet dsStudent = null;
        string date = string.Empty;
        string strNewDate = string.Empty;
        string format = "dd/mm/yyyy";
        int type = 0;
        string dateString = string.Empty;
        decimal calculatelatefee = 0;
        string totalsubfee = string.Empty;
        bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (Latefee == true)
        {

            latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


            DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=1 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
                dateString = date.Substring(0, 11);
                DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
                strNewDate = dateTime.ToString("yyyy-mm-dd");
                type = Convert.ToInt32(ds.Tables[0].Rows[0]["TYPE"]);
            }
            string SP_Name = "ACD_CALCULATE_DAY_WEEK_MONTHLY";
            string SP_Parameters = "@P_DATE, @P_TYPE";
            string Call_Values = "" + strNewDate + "," + Convert.ToInt32(type) + "";// +"," + Convert.ToInt16(ViewState["sem"]) + "," + 
            dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);


            if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"]) > 0)
            {
                calculatelatefee = latefees * Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

            }
            else
            {
                calculatelatefee = 0;
            }
            ViewState["latefee"] = calculatelatefee;
            // decimal abc = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);
            //calculatelatefee = latefees * abc;
            if (type == 1)
            {
                lblLateFee.Text = "<b style='color:red;'>PERDAY </b> : " + dateString + "  " + latefees.ToString();
            }
            else if (type == 1)
            {
                lblLateFee.Text = "<b style='color:red;'>WEEKLY </b> : " + dateString + "  " + latefees.ToString();
                // lblLateFee.Text = "WEEKLY   " + latefees.ToString();
            }
            else if (type == 3)
            {
                lblLateFee.Text = "<b style='color:red;'>MONTHLY </b> : " + dateString + "  " + latefees.ToString();

            }



        }
        else
        {
            latefees = 0;
        }
        #endregion
        #region  CREDITWISE=4
        //if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
        //{

        //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //    {
        //        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
        //        {
        //            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
        //            Label lblCredit = dataitem.FindControl("lblcredits") as Label;
        //            // HiddenField hdfCreditTotal = dataitem.FindControl("hdfCreditTotal") as HiddenField;
        //            decimal CourseCredit = Convert.ToDecimal(lblCredit.Text.ToString());
        //            if (lblCredit.Text == string.Empty)
        //            {
        //                lblCredit.Text = "0.00";
        //            }
        //            if (cbRow.Checked == true)
        //            {
        //                Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseCredit);
        //            }
        //            TotalAmt = Amt.ToString();
        //            //lblTotalExamFee.Text = TotalAmt.ToString();

        //            if (lblfessapplicable.Text == string.Empty)
        //            {
        //                lblfessapplicable.Text = "0.00";
        //            }


        //        }
        //    }
        //    if (TotalAmt == string.Empty || TotalAmt == null)
        //    {
        //        TotalAmt = "0";
        //    }
        // //   hdfCreditTotal.Value = Convert.ToDecimal(TotalAmt).ToString();
        //    #region FOR CREDIT COURSR WISE CALCULATION
        //    //COMPARE CREDIT TOTAL WISE FEE
        //    PAYID = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "CREDIT_RANGE_AMOUNT", "MINRANGE<=" + hdfCreditTotal.Value + " AND                    MAXRANGE>=" + hdfCreditTotal.Value + " AND SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "and FEETYPE=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]) + " and ISNULL(CANCEL,0)=0 AND  FEESTRUCTURE_TYPE=5  AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'");
        //    if (PAYID == string.Empty || PAYID == null)
        //    {
        //        PAYID = "0";
        //    }
        #endregion


        //}
        //#endregion
        #region 3/1-Course Wise
        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)//3-Course Wise
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {

                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblAmt = dataitem.FindControl("lblAmt") as Label;

                    if (lblAmt.Text == string.Empty)
                    {
                        lblAmt.Text = "0.00";
                    }
                    decimal CourseAmt = Convert.ToDecimal(lblAmt.Text.ToString());
                    if (cbRow.Checked == true)
                    {
                        Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                    }
                    TotalAmt = Amt.ToString();
                    lblTotalExamFee.Text = TotalAmt.ToString();
                    lblfessapplicable.Text = ProFess.ToString();

                    if (lblfessapplicable.Text == string.Empty)
                    {
                        lblfessapplicable.Text = "0.00";
                    }


                }
            }

            TotalAmt = Amt.ToString();


            ViewState["TotalSubFee"] = TotalAmt.ToString();
            //lblTotalExamFee.Text = TotalAmt.ToString();
        }
        #endregion

        lblfessapplicable.Text = ProFess.ToString();//proc         
        lblTotalExamFee.Text = PAYID;//total sub       
        lblCertificateFee.Text = CrettificateFee.ToString();
        lblTotalExamFee.Text = Amt.ToString();
        //lblpapervalMax.Text = valuationfee.ToString();


        if (PAYID == string.Empty || PAYID == null)
        {
            PAYID = "0";
        }
        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
        {
            lblTotalExamFee.Text = PAYID.ToString();
            // FinalTotal.Text = (Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString(); }
            FinalTotal.Text = (Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + calculatelatefee).ToString();
        }
        else
        {

            //FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
            FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + calculatelatefee).ToString();
            // FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + calculatelatefee + valuationfee).ToString();
            totalsubfee = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();

            ViewState["TotalSubFee"] = totalsubfee;
        }
    }

    protected void lvPractical_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0 || Convert.ToInt32(ViewState["FEESTYPE"]) == null)//NO_ FEE
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chk = (CheckBox)e.Item.FindControl("chkAcceptPractical");
                HiddenField hdfexreg = (HiddenField)e.Item.FindControl("hdfExamRegisteredPractical");
                HiddenField hdftxn = (HiddenField)e.Item.FindControl("hdfextindPractical");
                HiddenField hdREMEDALPractical = (HiddenField)e.Item.FindControl("hdREMEDALPractical");
                CheckBox chkhead = lvPractical.FindControl("chkAllPractical") as CheckBox;

                if (hdREMEDALPractical.Value == "1")
                {
                    chk.Checked = true;
                    //chk.Enabled = false;
                    chkhead.Checked = false;
                    //chkhead.Enabled = false;
                }
                else
                {
                    chk.Checked = false;
                    // chk.Enabled = false;
                }
            }
        }
        else
        {
            int ifPaidAlready = 0;
            ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'AEF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));

            if (ifPaidAlready > 0)
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAcceptPractical");
                    HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegisteredPractical");
                    CheckBox chkhead = lvPractical.FindControl("chkAllPractical") as CheckBox;

                    if (hdf.Value == "1")
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        chkhead.Checked = false;
                        chkhead.Enabled = false;
                    }
                    else
                    {
                        chk.Checked = false;
                        chk.Enabled = false;
                    }
                }
            }
        }
    }

    protected void chkAllPractical_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["CHK_All_TotalAmtTheory"] == string.Empty || ViewState["CHK_All_TotalAmtTheory"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtTheory"].ToString()))
            {
                ViewState["CHK_All_TotalAmtTheory"] = 0;
            }

            if (ViewState["CHK_All_TotalAmtPractical"] == string.Empty || ViewState["CHK_All_TotalAmtPractical"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_All_TotalAmtPractical"] = 0;
            }

            if (ViewState["CHK_All_FinalTotal_P"] == string.Empty || ViewState["CHK_All_FinalTotal_P"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_All_FinalTotal_P"] = 0;
            }

            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
            {
                CheckBox chckheader = (CheckBox)lvPractical.FindControl("chkAllPractical");
                if (chckheader.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvPractical.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                        cbRow.Checked = true;
                    }
                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvPractical.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                        cbRow.Checked = false;
                    }
                }

                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            }
            else
            {
                //ListViewDataItem dataitem in lvFailCourse.ItemTemplate)
                CheckBox chckheader = (CheckBox)lvPractical.FindControl("chkAllPractical");
                if (chckheader.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvPractical.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                        cbRow.Checked = true;
                    }

                    foreach (ListViewDataItem dataitem in lvPractical.Items)
                    {
                        if ((dataitem.FindControl("chkAcceptPractical") as CheckBox).Checked == true)
                        {
                            CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                            Label lblAmt = dataitem.FindControl("lblAmtPractical") as Label;
                            HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegisteredPractical") as HiddenField;
                            HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                            decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
                            if (cbRow.Checked == true)
                            {
                                Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                            }
                        }
                    }
                    string TotalAmt = Amt.ToString();
                    ViewState["CHK_All_TotalAmtPractical"] = TotalAmt; // SM
                    decimal CHK_All_TP_Amount_P = Convert.ToDecimal(ViewState["CHK_All_TotalAmtTheory"]) + Convert.ToDecimal(ViewState["CHK_All_TotalAmtPractical"]); // SM
                    //lblTotalExamFee.Text = TotalAmt.ToString();
                    lblTotalExamFee.Text = CHK_All_TP_Amount_P.ToString(); // SM
                    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
                    ViewState["CHK_All_FinalTotal_P"] = FinalTotal.Text; // SM

                    decimal CHK_All_TP_Final_Amount_P = Convert.ToDecimal(ViewState["CHK_All_FinalTotal_P"]); // SM
                    ViewState["CHK_All_FinalTotal_T_P"] = CHK_All_TP_Final_Amount_P;
                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvPractical.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                        cbRow.Checked = false;
                        string TotalAmt = Amt.ToString();
                        lblTotalExamFee.Text = TotalAmt.ToString();
                        if (lblfessapplicable.Text == string.Empty)
                        {
                            lblfessapplicable.Text = "0";
                        }
                        decimal CHK_All_TP_Amount_P = Convert.ToDecimal(ViewState["CHK_All_TotalAmtTheory"]) - Convert.ToDecimal(ViewState["CHK_All_TotalAmtPractical"]); // SM
                        lblTotalExamFee.Text = CHK_All_TP_Amount_P.ToString(); // SM
                        FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                        ViewState["CHK_All_TotalAmtPractical"] = 0; // SM
                        ViewState["CHK_All_FinalTotal_P"] = 0;
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void chkAcceptPractical_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //tring lblfessapplicable= 
            //  lblfessapplicable.Text = "0";

            //ViewState["TotalAmtTheory"] = 0;
            //ViewState["TotalAmtPractical"] = 0;

            if (ViewState["TotalAmtTheory"] == string.Empty || ViewState["TotalAmtTheory"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtTheory"].ToString()))
            {
                ViewState["TotalAmtTheory"] = 0;
            }

            if (ViewState["TotalAmtPractical"] == string.Empty || ViewState["TotalAmtPractical"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["TotalAmtPractical"] = 0;
            }

            if (ViewState["CHK_FinalTotal_TP"] == string.Empty || ViewState["CHK_FinalTotal_TP"] == null)
            //if (!string.IsNullOrEmpty(ViewState["TotalAmtPractical"].ToString()))
            {
                ViewState["CHK_FinalTotal_TP"] = 0;
            }

            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();$('#BatchTheoryP1').hide();$('td:nth-child(7)').hide();});", true);
            }
            else
            {
                foreach (ListViewDataItem dataitem in lvPractical.Items)
                {
                    if ((dataitem.FindControl("chkAcceptPractical") as CheckBox).Checked == true)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAcceptPractical") as CheckBox;
                        Label lblAmt = dataitem.FindControl("lblAmtPractical") as Label;
                        HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegisteredPractical") as HiddenField;
                        HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                        HiddenField hdfSubid = dataitem.FindControl("hdfSubid") as HiddenField;
                        decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
                        if (cbRow.Checked == true)
                        {
                            Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                        }
                    }
                }

                string TotalAmt = Amt.ToString();
                ViewState["TotalAmtPractical"] = TotalAmt; // SM
                decimal TP_Amount_P = Convert.ToDecimal(ViewState["TotalAmtTheory"]) + Convert.ToDecimal(ViewState["TotalAmtPractical"]); // SM
                lblTotalExamFee.Text = TP_Amount_P.ToString(); // SM
                if (lblfessapplicable.Text == string.Empty)
                {
                    lblfessapplicable.Text = "0";
                }

                FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
                ViewState["CHK_FinalTotal_TP"] = FinalTotal.Text; // SM

                if ((ViewState["CHK_All_FinalTotal_P"]).ToString() != "0")
                {
                    decimal CHK_All_FinalTotal_P = 0;
                    decimal TotalAmtPractical = 0;
                    decimal AmtPractical = 0;
                    decimal CHK_All_FinalTotal_T_P = 0;
                    decimal FinalTotal_T_P = 0;

                    CHK_All_FinalTotal_P = Convert.ToDecimal(ViewState["CHK_All_FinalTotal_P"]);
                    TotalAmtPractical = Convert.ToDecimal(ViewState["TotalAmtPractical"]);
                    CHK_All_FinalTotal_T_P = Convert.ToDecimal(ViewState["CHK_All_FinalTotal_T_P"]);
                    AmtPractical = Convert.ToDecimal(CHK_All_FinalTotal_P) - Convert.ToDecimal(TotalAmtPractical);
                    FinalTotal_T_P = CHK_All_FinalTotal_T_P - AmtPractical;
                    FinalTotal.Text = FinalTotal_T_P.ToString();
                    lblTotalExamFee.Text = FinalTotal_T_P.ToString();
                }

                Amt = 0;
                CourseAmtt = 0;
            }
        }
        catch
        {

        }
    }
}
