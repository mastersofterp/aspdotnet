﻿using System;
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

public partial class ACADEMIC_ExamRegistration_Summer : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentController studCont = new StudentController();
    Student_Acd objSA = new Student_Acd();
    StudentFees objStudentFees = new StudentFees();
    StudentController sc = new StudentController();
    bool IsNotActivitySem = false;
    bool flag = true;
    decimal Amt = 0;
    decimal CourseAmtt = 0;


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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    
                    int cid = 0;
                    int idno = 0;
                    int semesterno = 0;
                    idno = Convert.ToInt32(Session["idno"]);
               
                    cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));
                    semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno));

                    if (CheckActivityCollege(cid))
                    {

                        oldsem = string.Empty;
                        this.ShowDetails();
                        bindcourses();


                        //int uatype = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "isnull(FEES_COLLECTION,0)", ""));
                        //if (uatype == 1)
                        //{
                        //    int CheckAcademicActivity = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ISNULL(FEES_COLLECTION,0)", ""));
                        //    if (CheckAcademicActivity == 1)
                        //    {
                        //        DataSet ds = studCont.AdmfessDues(Convert.ToInt32(Session["idno"]), Convert.ToInt32(semesterno));

                        //        if (ds.Tables.Count == 0 || ds.Tables.Count==null)
                        //        {
                        //            objCommon.DisplayMessage("Academic Fees Not Paid Please contact to Admin!", this.Page);
                        //            divbtn.Visible = false;
                        //            return;
                        //        }

                                
                        //    }
                        //}

                  

                    }
                }
                else
                {
                    pnlSearch.Visible = true;
                    btnSubmit.Visible = false;
                    btnPay.Visible = false;
                    btnPrintRegSlip.Visible = false;
                    btnSubmit_WithDemand.Visible = false;
                }

                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];              
            }

            divMsg.InnerHtml = string.Empty;

        
        }
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
                objCommon.ShowError(Page, "ExamRegistration_Summer.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }
    #region GET_IP_ADDRESS
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
    #endregion
    #region CHECK CONNECTION
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
    #endregion
    #region CHECK PAGE AUTHORIZATION
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration_Summer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration_Summer.aspx");
        }
    }
    #endregion
    #region SHOW DATAILS OF STUDENT CURRENT EDUCATION
    private void ShowDetails()
    {
        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();
        btnSubmit.Visible = false;
        btnPrintRegSlip.Visible = true;
        //btnReport.Visible = false;
        int idno = 0;
        // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }

        try
        {
            if (idno > 0)
            {
                divCourses.Visible = true;
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {


                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        ViewState["regno"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString(); // temp
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        ViewState["EmailID"] = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        ViewState["MOBILENO"] = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        ViewState["IDNO"] = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                    }
                    else
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;
                        flag = false;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;
                    flag = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                divCourses.Visible = false;
                flag = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            sessionno = Convert.ToInt32(ViewState["sessionnonew"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ViewState["sessionnonew"]);//(ddlSession.SelectedValue);
        }

        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {

            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int Collegeid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Collegeid + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_DEGREENO=" + Convert.ToInt32(degreeno) + ",@P_BRANCHNO=" + Convert.ToInt32(branchno) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + semesterno; ;
            //  tring call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamRegistration_Summer.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #region CHECK ACTIVITY COLLEGE_ID WISE
    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + " AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        ViewState["sessionnonew"] = sessionno;
        Session["sessionnonew"] = sessionno;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
                divbtn.Visible = false;

            }
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
                divbtn.Visible = false;

            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);

            ret = false;
            divbtn.Visible = false;
        }
        dtr.Close();
        return ret;
    }
    #endregion
    #region BIND COURSE ON LIST VIEW FOR REGULAR EXAM REG
    //protected void bindcourses()
    //{

    //    int idno = 0;
    //    int sessionno = Convert.ToInt32(ViewState["sessionnonew"]);
    //    int ORG = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
    //    StudentController objSC = new StudentController();
    //    DataSet dsSubjects;

    //    //if (ViewState["usertype"].ToString() == "1")
    //    //{

    //    //}


    //    idno = Convert.ToInt32(Session["idno"]);
    //    if (idno > 0)
    //    {
    //        divCourses.Visible = true;
    //        DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

    //        if (idno == null || idno == 0)
    //        {
    //            objCommon.DisplayMessage(updatepnl, "No Record Found...", this.Page);

    //        }
    //        else
    //        {
    //            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
    //            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
    //            int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
    //            ViewState["clg_id"] = clg_id;
    //            string para_name, call_values;
    //            string proc_name = "PKG_EXAM_GET_SUBJECTS_LIST_FOR_EXAM_REGISTARTION_SUMMER";

    //            para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO";
    //            call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "";
    //            dsSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);




    //            if (dsSubjects.Tables[0].Rows.Count > 0)
    //            {

    //                if (Session["usertype"].ToString() == "2")
    //                {


    //                    lvFailCourse.DataSource = dsSubjects;
    //                    lvFailCourse.DataBind();
    //                    lvFailCourse.Visible = true;
    //                    divCourses.Visible = true;
    //                    btnSubmit.Visible = true;
    //                    pnlFailCourse.Visible = true;

    //                }
    //                else
    //                {
    //                    // lblfessapplicable.Text = string.Empty;
    //                    // lblCertificateFee.Text = string.Empty;
    //                    //lblTotalExamFee.Text = "";
    //                    // lblLateFee.Text = "";
    //                    // FinalTotal.Text = "";

    //                    //lblCertificateFee.Visible = false;
    //                    //lblfessapplicable.Visible = false;
    //                    //lblLateFee.Visible = false;
    //                    //lblTotalExamFee.Visible = false;
    //                    //FinalTotal.Visible = false;

    //                    //lvFailCourse.DataSource = dsSubjects;
    //                    //lvFailCourse.DataBind();
    //                    //lvFailCourse.Visible = true;
    //                    //divCourses.Visible = true;
    //                    //btnSubmit.Visible = true;
    //                    //btnSubmit.Enabled = true;
    //                    //pnlFailCourse.Visible = true;

    //                    lvFailCourse.DataSource = dsSubjects;
    //                    lvFailCourse.DataBind();
    //                    lvFailCourse.Visible = true;
    //                    divCourses.Visible = true;
    //                    btnSubmit.Visible = true;
    //                    btnPay.Visible = true;
    //                    btnPay.Enabled = true;
    //                    pnlFailCourse.Visible = true;
                        
    //                    //btnPay.Enabled = false;
    //                    btnPrintRegSlip.Visible = true;
    //                    //btnPrintRegSlip.Enabled = false;
    //                    btnSubmit_WithDemand.Visible=true;


    //                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(8)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(8)').hide();});", true);
    //                }


    //            }
    //            else
    //            {
    //                //lvFailCourse.DataSource = null;
    //                //lvFailCourse.DataBind();
    //                //lvFailCourse.Visible = false;
    //                //divCourses.Visible = true;
    //                //objCommon.DisplayMessage("No Courses found...!!!", this.Page);
    //                //btnSubmit.Visible = false;
    //                //btnSubmit.Enabled = false;
    //                //pnlFailCourse.Visible = true;
    //                //btnPay.Visible = false;
    //                //btnPay.Enabled = false;
    //                //btnPrintRegSlip.Visible = false;
    //                //btnPrintRegSlip.Enabled = false;
    //                //return;

    //                lvFailCourse.Visible = false;
    //                divbtn.Visible = false;
    //            }


    //            #region Added For Atlas Attendance Greater than 50% Average Added by gaurav 31-03-2023
    //            if (Convert.ToInt32(Session["OrgId"]) == 9)//FOR ATLAS ADDED BY GAURAV 31-3-2023
    //            {
    //                int sum;


    //                DataSet ds = sc.GetStudentAttPerDashboard(Convert.ToInt32(Session["idno"]));

    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PER"]) < 50)
    //                    {

    //                        sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + lblSemester.ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

    //                        if (sum <= 0)
    //                        {
    //                            objCommon.DisplayMessage(updatepnl, "Dear Student," + "\\r\\n" + "Please note your attendance is low. You are not eligible to register for the final exam till such a time that you meet your advisor or admin and submit the explanation for low attendance.", this.Page);
    //                        }

    //                    }

    //                }
    //                else
    //                {
    //                    sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + lblSemester.ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

    //                    if (sum <= 0)
    //                    {
    //                        objCommon.DisplayMessage(updatepnl, "Dear Student," + "\\r\\n" + "Please Note your attendance is low. You are not eligible to register for the final exam till such a time that you meet your advisor or admin and submit the explanation for low attendance.", this.Page);

    //                    }//return;
    //                    // btnPrintRegSlip.Visible = false;
    //                    // btnSubmit.Visible = false;
    //                    // btnPay.Visible = false;
    //                    // return;

    //                }

    //                // }
    //            }
    //            #endregion
    //            #region Get Which type Fee Config.
    //            // 1-Course Type Wise//  2-Credit Wise// 3-Course Wise// 4-Fix// 5-Credit Range Wise   fee structure          
    //            // check fees type COURSE type wise, fix, Credit WIse,Course Wise,
    //            //FEETYPE=5-- Summer Term Added by gaurav  ACD_EXAM_TYPE
    //            string CHECKFEESTYPE = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEESTRUCTURE_TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0");

    //            #endregion
    //            #region Get Button Online/offline/Both
    //            int checkpaymentmode = Convert.ToInt16(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(PAYMENT_MODE,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0  UNION ALL SELECT 0 AS PAYMENT_MODE"));
    //            #endregion
    //            #region ADDED FOR With fee but only demand create
    //            //ADDED FOR With fee but only demand create
    //            Session["PaymentMode"] = checkpaymentmode;
    //            #endregion

    //            ViewState["FEESTYPE"] = CHECKFEESTYPE;
    //            if (CHECKFEESTYPE == string.Empty || CHECKFEESTYPE == null)
    //            {
    //                ViewState["FEESTYPE"] = 0;
    //            }
    //            #region NO FEE
    //            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
    //            {
    //                HideClm();
    //                btnPay.Visible = false;
    //                btnPay.Enabled = false;
    //                btnSubmit_WithDemand.Visible = false;
    //                btnSubmit_WithDemand.Enabled = false;
    //                btnSubmit.Visible = true;
    //                btnSubmit.Enabled = true;
    //                btnPrintRegSlip.Visible = true;
    //                lblfessapplicable.Text = "";
    //                lblCertificateFee.Text = "";
    //                lblTotalExamFee.Text = "";
    //                lblLateFee.Text = "";
    //                FinalTotal.Text = "";

    //                Allfeesitems.Visible = false;
    //                lvTotalAmount.Visible = false;

    //            }
    //            #endregion
    //            #region FIXTYPE
    //            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)//FIX  
    //            {

    //                foreach (ListViewItem item in lvFailCourse.Items)
    //                {
    //                    CheckBox chkAccept = (CheckBox)item.FindControl("chkAccept");
    //                    chkAccept.Checked = true;
    //                }

    //                CheckBox chkAll = (CheckBox)lvFailCourse.FindControl("chkAll");
    //                chkAll.Checked = true;

    //                CalculateTotalFixFee();//FIX               
    //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
    //                HideClm();


    //                int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
    //                if (paysuccess > 0)
    //                {

    //                    decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));

    //                    btnPrintRegSlip.Visible = true;
    //                    btnSubmit.Visible = false;
    //                    btnSubmit_WithDemand.Visible = false;
    //                    btnSubmit_WithDemand.Enabled = false;
    //                    btnPay.Visible = false;
    //                    lblLateFee.Text = "";
    //                    lblfessapplicable.Text = "";
    //                    lblCertificateFee.Text = "";
    //                    lblTotalExamFee.Text = "";
    //                    FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
    //                    lvFailCourse.Enabled = false;
    //                }
    //                else
    //                {
    //                    btnhideshow();
    //                }
    //            }
    //            #endregion
    //            #region CREDIT RANGE WISE
    //            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
    //            {

    //                //CHECK FEES APPlCABLE OR NOT 
    //                int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND FEESTRUCTURE_TYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

    //                if (CheckExamfeesApplicableOrNot >= 1)
    //                {
    //                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
    //                    if (paysuccess > 0)
    //                    {
    //                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
    //                        CalculateTotalCredit();
    //                        btnPrintRegSlip.Visible = true;
    //                        btnSubmit.Visible = false;
    //                        btnPay.Visible = false;
    //                        btnSubmit_WithDemand.Visible = false;
    //                        btnSubmit_WithDemand.Enabled = false;
    //                        lblLateFee.Text = "";
    //                        lblfessapplicable.Text = "";
    //                        lblCertificateFee.Text = "";
    //                        lblTotalExamFee.Text = "";
    //                        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
    //                        lvFailCourse.Enabled = false;
    //                    }
    //                    else
    //                    {
    //                        CalculateTotalCredit();
    //                        // btnPrintRegSlip.Visible = false;
    //                        // btnPay.Visible = true;
    //                        // btnSubmit.Visible = false;
    //                        btnhideshow();
    //                    }
    //                }
    //                else
    //                {

    //                    btnPay.Visible = false;
    //                    btnPay.Enabled = false;
    //                }
    //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
    //                HideClm();
    //            }
    //            #endregion
    //            #region COURSEWISE
    //            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)
    //            {
    //                CalculateTotalCredit();
                   

    //                if (Session["usertype"].ToString()=="2")
    //                {
    //                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='STF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
    //                    if (paysuccess > 0)
    //                    {
    //                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='STF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));

    //                        btnPrintRegSlip.Visible = true;
    //                        btnSubmit.Visible = false;
    //                        btnSubmit.Enabled = false;
    //                        btnPay.Visible = false;
    //                        btnPay.Enabled = false;
    //                        btnSubmit_WithDemand.Enabled = false;
    //                        btnSubmit_WithDemand.Visible = false;
    //                        lblLateFee.Text = "";
    //                        lblfessapplicable.Text = "";
    //                        lblCertificateFee.Text = "";
    //                        lblTotalExamFee.Text = "";
    //                        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
    //                        lvFailCourse.Enabled = false;

    //                    }
    //                    else
    //                    {
    //                        btnhideshow();
    //                    }
    //                }
    //                else
    //                {
    //                    btnhideshow();
    //                }
                    
    //            }
    //            #endregion
    //            else
    //            {
    //                //CHECK FEES APPlCABLE OR NOT 
    //                int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

    //                if (CheckExamfeesApplicableOrNot >= 1)
    //                {
    //                    CalculateTotal();//ALL Course Fees
                       
    //                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
    //                    if (paysuccess > 0)
    //                    {
    //                        btnPrintRegSlip.Visible = true;
    //                        btnSubmit.Visible = false;
    //                        btnPay.Visible = false;
    //                        btnSubmit_WithDemand.Visible = false;
    //                        btnSubmit_WithDemand.Enabled = false;
    //                        lvFailCourse.Enabled = false;
    //                    }
    //                    else
    //                    {
    //                        btnhideshow();
    //                    }
    //                }
    //                else
    //                {
    //                    HideClm();
    //                    btnPay.Visible = false;
    //                    btnSubmit_WithDemand.Visible = false;
    //                }
    //            }
    //        }

    //    }
    //}

    protected void bindcourses()
    {
        
        int idno = 0;
        int sessionno = Convert.ToInt32(ViewState["sessionnonew"]);
        int ORG = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        StudentController objSC = new StudentController();
        DataSet dsSubjects;

        //if (ViewState["usertype"].ToString() == "2")
        //{

        //}
        idno = Convert.ToInt32(Session["idno"]);
        if (idno > 0)
        {
            divCourses.Visible = true;
            DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

            if (idno == null || idno == 0)
            {
                objCommon.DisplayMessage(updatepnl, "No Record Found...", this.Page);

            }
            else
            {
                int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
                int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
                ViewState["clg_id"] = clg_id;
                string para_name, call_values;
                string proc_name = "PKG_EXAM_GET_SUBJECTS_LIST_FOR_EXAM_REGISTARTION_SUMMER";

                para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO";
                call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "";
                dsSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);



                oldsem = string.Empty; ;
                if (dsSubjects.Tables[0].Rows.Count > 0)
                {
                    lvFailCourse.DataSource = dsSubjects;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = true;
                    divCourses.Visible = true;
                    btnSubmit.Visible = true;
                    pnlFailCourse.Visible = true;
                    Findsemester();


                    //   ListView1.Visible = true;

                }
                else
                {
                    lvFailCourse.DataSource = null;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = false;
                    divCourses.Visible = true;
                    objCommon.DisplayMessage("No Courses found...!!!", this.Page);
                    btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;
                    pnlFailCourse.Visible = true;
                    btnPay.Visible = false;
                    btnPay.Enabled = false;
                    btnPrintRegSlip.Visible = false;
                    btnPrintRegSlip.Enabled = false;
                    return;
                }


                #region Added For Atlas Attendance Greater than 50% Average Added by gaurav 31-03-2023
                if (Convert.ToInt32(Session["OrgId"]) == 9)//FOR ATLAS ADDED BY GAURAV 31-3-2023
                {
                    int sum;


                    DataSet ds = sc.GetStudentAttPerDashboard(Convert.ToInt32(Session["idno"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PER"]) < 50)
                        {

                            sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + lblSemester.ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

                            if (sum <= 0)
                            {
                                objCommon.DisplayMessage(updatepnl, "Dear Student," + "\\r\\n" + "Please note your attendance is low. You are not eligible to register for the final exam till such a time that you meet your advisor or admin and submit the explanation for low attendance.", this.Page);
                            }

                        }

                    }
                    else
                    {
                        sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + lblSemester.ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

                        if (sum <= 0)
                        {
                            objCommon.DisplayMessage(updatepnl, "Dear Student," + "\\r\\n" + "Please Note your attendance is low. You are not eligible to register for the final exam till such a time that you meet your advisor or admin and submit the explanation for low attendance.", this.Page);

                        }//return;
                        // btnPrintRegSlip.Visible = false;
                        // btnSubmit.Visible = false;
                        // btnPay.Visible = false;
                        // return;

                    }

                    // }
                }
                #endregion

                // 1-Course Type Wise//  2-Credit Wise// 3-Course Wise// 4-Fix// 5-Credit Range Wise            
                // check fees type COURSE type wise, fix, Credit WIse,Course Wise,
                //string CHECKFEESTYPE = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEESTRUCTURE_TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0");
                string CHECKFEESTYPE = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEESTRUCTURE_TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0");

                int checkpaymentmode = Convert.ToInt16(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(PAYMENT_MODE,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0  UNION ALL SELECT 0 AS PAYMENT_MODE"));

                #region ADDED FOR With fee but only demand create
                //ADDED FOR With fee but only demand create
                Session["PaymentMode"] = checkpaymentmode;
                #endregion

                ViewState["FEESTYPE"] = CHECKFEESTYPE;
                if (CHECKFEESTYPE == string.Empty || CHECKFEESTYPE == null)
                {
                    ViewState["FEESTYPE"] = 0;
                }
                #region NO FEE
                if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
                {
                    HideClm();
                    btnPay.Visible = false;
                    btnPay.Enabled = false;
                    btnSubmit_WithDemand.Visible = false;
                    btnSubmit_WithDemand.Enabled = false;
                    btnSubmit.Visible = true;
                    btnSubmit.Enabled = true;
                    btnPrintRegSlip.Visible = true;
                    lblfessapplicable.Text = "";
                    lblCertificateFee.Text = "";
                    lblTotalExamFee.Text = "";
                    lblLateFee.Text = "";
                    FinalTotal.Text = "";

                }
                #endregion
                #region FIXTYPE
                else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)//FIX  
                {
                    CalculateTotalFixFee();//FIX               
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                    HideClm();
                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO IN (" + oldsem + ") AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='STF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                    if (paysuccess > 0)
                    {

                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));

                        btnPrintRegSlip.Visible = true;
                        btnSubmit.Visible = false;
                        btnSubmit_WithDemand.Visible = false;
                        btnSubmit_WithDemand.Enabled = false;
                        btnPay.Visible = false;
                        lblLateFee.Text = "";
                        lblfessapplicable.Text = "";
                        lblCertificateFee.Text = "";
                        lblTotalExamFee.Text = "";
                        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                        lvFailCourse.Enabled = false;
                    }
                    else
                    {
                        btnhideshow();
                    }
                }
                #endregion
                #region CREDIT RANGE WISE
                else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
                {

                    //CHECK FEES APPlCABLE OR NOT 
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND FEESTRUCTURE_TYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

                    if (CheckExamfeesApplicableOrNot >= 1)
                    {
                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (paysuccess > 0)
                        {
                            decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                            CalculateTotalCredit();
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            btnSubmit_WithDemand.Visible = false;
                            btnSubmit_WithDemand.Enabled = false;
                            lblLateFee.Text = "";
                            lblfessapplicable.Text = "";
                            lblCertificateFee.Text = "";
                            lblTotalExamFee.Text = "";
                            FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                            lvFailCourse.Enabled = false;
                        }
                        else
                        {
                            CalculateTotalCredit();
                            // btnPrintRegSlip.Visible = false;
                            // btnPay.Visible = true;
                            // btnSubmit.Visible = false;
                            btnhideshow();
                        }
                    }
                    else
                    {

                        btnPay.Visible = false;
                        btnPay.Enabled = false;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                    HideClm();
                }
                #endregion
                #region COURSEWISE
                else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)
                {
                    CalculateTotalCredit();
                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='EF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));


                    if (paysuccess > 0)
                    {
                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));

                        btnPrintRegSlip.Visible = true;
                        btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;
                        btnPay.Visible = false;
                        btnPay.Enabled = false;
                        btnSubmit_WithDemand.Enabled = false;
                        btnSubmit_WithDemand.Visible = false;
                        lblLateFee.Text = "";
                        lblfessapplicable.Text = "";
                        lblCertificateFee.Text = "";
                        lblTotalExamFee.Text = "";
                        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                        lvFailCourse.Enabled = false;

                    }
                    else
                    {
                        btnhideshow();
                    }
                }
                #endregion
                else
                {
                    //CHECK FEES APPlCABLE OR NOT 
                    //int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(oldsem) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
                    if (CheckExamfeesApplicableOrNot >= 1)
                    {
                        CalculateTotal();//ALL Course Fees

                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='EF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (paysuccess > 0)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            btnSubmit_WithDemand.Visible = false;
                            btnSubmit_WithDemand.Enabled = false;
                            lvFailCourse.Enabled = false;
                        }
                        else
                        {
                            btnhideshow();

                        }
                    }
                    else
                    {
                        HideClm();
                        btnPay.Visible = false;
                        btnSubmit_WithDemand.Visible = false;
                    }
                }
            }

        }
    } 
    #region Commented Code
    //protected void bindcourses()
    //{

    //    int idno = 0;
    //    int sessionno = Convert.ToInt32(Session["sessionnonew"]);
    //    StudentController objSC = new StudentController();
    //    DataSet dsFailSubjects;
    //    idno = Convert.ToInt32(Session["idno"]);
    //    int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
    //    int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
    //    int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
    //    ViewState["clg_id"] = clg_id;


    //    dsFailSubjects = objSC.GetStudentFailExamSubjectSNew_sem(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno, Convert.ToInt32(ddlBackLogSem.SelectedValue));

    //    if (dsFailSubjects.Tables[0].Rows.Count > 0)
    //    {
    //        ViewState["oldsession"] = dsFailSubjects.Tables[0].Rows[0]["SESSIONNO"].ToString();
    //    }
    //    if (dsFailSubjects.Tables[0].Rows.Count > 0)
    //    {

    //        string CHECKFEESTYPE = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEESTRUCTURE_TYPE", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=1 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0");

    //        int checkpaymentmode = Convert.ToInt16(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(PAYMENT_MODE,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=1 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0  UNION ALL SELECT 0 AS PAYMENT_MODE"));
    //        //  and ISNULL(IsFeesWithDemand)=0
    //        #region ADDED FOR With fee but only demand create
    //        //ADDED FOR With fee but only demand create
    //        Session["PaymentMode"] = checkpaymentmode;
    //        #endregion
    //        ViewState["FEESTYPE"] = CHECKFEESTYPE;
    //        if (CHECKFEESTYPE == string.Empty || CHECKFEESTYPE == null)
    //        {
    //            ViewState["FEESTYPE"] = 0;

    //        }
    //        #region NO FEE
    //        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 0)//NO_ FEE
    //        {
    //            HideClm();
    //            btnSubmit.Visible = false;
    //            btnSubmit.Enabled = false;
    //            btnSubmit_WithDemand.Visible = false;
    //            btnSubmit_WithDemand.Enabled = false;
    //            btnPay.Visible = true;
    //            btnPay.Enabled = true;
    //            btnPrintRegSlip.Visible = true;
    //            lblfessapplicable.Text = "";
    //            lblCertificateFee.Text = "";
    //            lblTotalExamFee.Text = "";
    //            lblLateFee.Text = "";
    //            FinalTotal.Text = "";

    //        }
    //        #endregion
    //        #region  COURSE WISE/TYPE FEE
    //        else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)
    //        {

    //            lvFailCourse.DataSource = dsFailSubjects;
    //            lvFailCourse.DataBind();
    //            CalculateTotalCredit();

    //            int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + ddlBackLogSem.SelectedValue + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='AEF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));

    //            HideClmAdmin();
    //            if (paysuccess > 0)
    //            {
    //                decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + ddlBackLogSem.SelectedValue + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='AEF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));

    //                btnPrintRegSlip.Visible = true;
    //                btnSubmit.Visible = false;
    //                //  btnPay.Visible = false;
    //                btnSubmit_WithDemand.Visible = false;
    //                lblLateFee.Text = "";
    //                lblfessapplicable.Text = "";
    //                lblCertificateFee.Text = "";
    //                lblTotalExamFee.Text = "";
    //                FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();

    //                //  lvFailCourse.Enabled = false;


    //                if (Session["usertype"].ToString() == "2")
    //                {
    //                    lvFailCourse.Enabled = false;
    //                }
    //                else
    //                {
    //                    btnPay.Visible = true;
    //                }
    //            }
    //            else
    //            {
    //                btnhideshow();


    //            }
    //            //else
    //            //{
    //            //    btnPrintRegSlip.Visible = false;
    //            //    btnPay.Visible = false;
    //            //    btnSubmit_WithDemand.Visible = false;                      
    //            //    btnSubmit.Visible = true;
    //            //    btnSubmit.Enabled = true;

    //            //}
    //        }
    //        #endregion
    //        #region CREDIT RANGE WISE
    //        else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
    //        {
    //            //HideClmAdmin();
    //            //CHECK FEES APPlCABLE OR NOT 
    //            int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + ddlBackLogSem.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=1 AND FEESTRUCTURE_TYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

    //            if (CheckExamfeesApplicableOrNot >= 1)
    //            {
    //                CalculateTotalCredit();
    //                btnSubmit.Visible = true;
    //                btnSubmit.Enabled = true;
    //                btnPrintRegSlip.Visible = false;

    //            }
    //            else
    //            {

    //                btnPay.Visible = false;
    //                btnPay.Enabled = false;
    //                btnSubmit_WithDemand.Visible = false;
    //                btnSubmit_WithDemand.Enabled = false;
    //            }

    //            HideClm();
    //        }
    //        #endregion
    //        #region FIXTYPE


    //        else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)//FIX  
    //        {
    //            CalculateTotalFixFee();//FIX               

    //            HideClm();
    //            // HideClmAdmin();

    //            int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='AEF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
    //            if (paysuccess > 0)
    //            {



    //                decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='AEF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));

    //                btnPrintRegSlip.Visible = true;
    //                btnSubmit.Visible = false;
    //                btnPay.Visible = false;
    //                lblLateFee.Text = "";
    //                lblfessapplicable.Text = "";
    //                lblCertificateFee.Text = "";
    //                lblTotalExamFee.Text = "";
    //                btnSubmit_WithDemand.Visible = false;
    //                btnSubmit_WithDemand.Enabled = false;
    //                FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
    //                if (Session["usertype"].ToString() == "2")
    //                {
    //                    lvFailCourse.Enabled = false;
    //                }
    //                else
    //                {
    //                    btnPay.Visible = true;
    //                }






    //            }
    //            else
    //            {
    //                btnhideshow();
    //                //lvFailCourse.Enabled = true;
    //                //if (Session["PaymentMode"].ToString() == "1")// online only 
    //                //{                            

    //                //    btnPrintRegSlip.Visible = false;
    //                //    btnPay.Visible = false;
    //                //    btnPay.Enabled = false;
    //                //    btnSubmit_WithDemand.Visible = false;
    //                //    btnSubmit_WithDemand.Enabled = false;
    //                //    btnSubmit.Visible = true;
    //                //    btnSubmit.Enabled = true;  
    //                //}
    //                //else if (Session["PaymentMode"].ToString() == "2")//offline With demand 
    //                //{

    //                //    btnPrintRegSlip.Visible = true;
    //                //    btnPay.Visible = false;
    //                //    btnPay.Enabled = false;
    //                //    btnSubmit.Visible = false;
    //                //    btnSubmit.Enabled = false;  
    //                //    btnSubmit_WithDemand.Visible = true;
    //                //    btnSubmit_WithDemand.Enabled = true;
    //                //}
    //                //else if (Session["PaymentMode"].ToString() == "3")//Online Ofline Both
    //                //{

    //                //    btnPrintRegSlip.Visible = true;
    //                //    btnSubmit.Visible = true;
    //                //    btnSubmit.Enabled = true;
    //                //    btnSubmit_WithDemand.Visible = true;
    //                //    btnSubmit_WithDemand.Enabled = true;
    //                //    btnPay.Visible = false;
    //                //    btnPay.Enabled = false;
    //                //}
    //                //else {


    //                //    btnPrintRegSlip.Visible = true;
    //                //    btnSubmit.Visible = false;
    //                //    btnSubmit.Enabled = false;
    //                //    btnSubmit_WithDemand.Visible = false;
    //                //    btnSubmit_WithDemand.Enabled = false;
    //                //    btnPay.Visible = true;
    //                //    btnPay.Enabled = true;


    //                //}

    //            }
    //        }
    //        #endregion
    //        else
    //        {
    //            CalculateTotal();
    //        }

    //        lvFailCourse.DataSource = dsFailSubjects;
    //        lvFailCourse.DataBind();
    //        lvFailCourse.Visible = true;
    //        divCourses.Visible = true;
    //        pnlFailCourse.Visible = true;
    //    }
    //    else
    //    {

    //        objCommon.DisplayMessage(updatepnl, "No Courses found...!! !!", this.Page);
    //        lvFailCourse.DataSource = null;
    //        lvFailCourse.DataBind();
    //        lvFailCourse.Visible = false;
    //        divCourses.Visible = true;
    //        btnSubmit.Visible = false;
    //        btnPrintRegSlip.Visible = false;
    //        btnSubmit.Visible = false;
    //        btnPrintRegSlip.Visible = false;
    //        lblTotalExamFee.Text = string.Empty;
    //        FinalTotal.Text = string.Empty;
    //        return;


    //    }

    //}
    #endregion
    #endregion
    String FindSem;

    String oldsem;
    
    protected void Findsemester()
    {
        try
        {
            #region For Find semesters
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                Label FindSems = dataitem.FindControl("lblsemester") as Label;
                FindSem += FindSems.ToolTip + ",";
            }

            #region For Spliting FindSem
            // ---------------------------------------------try----------------------------
            // string semestern = "1,1,1,1,1,2,2,2,3,44";


            // Split the string into an array of items
            string[] semesters1 = FindSem.Split(',');

            // Create a dictionary to store the counts
            Dictionary<string, int> counts = new Dictionary<string, int>();

            // Loop through the items and count them
            foreach (string semester1 in semesters1)
            {
                // Trim any leading/trailing whitespace in case there are spaces around the commas
                string trimmedSemester = semester1.Trim();

                if (!string.IsNullOrEmpty(trimmedSemester))
                {
                    if (counts.ContainsKey(trimmedSemester))
                    {
                        counts[trimmedSemester]++;
                    }
                    else
                    {
                        counts[trimmedSemester] = 1;
                    }
                }
            }

            //Display the counts
            foreach (var kvp in counts)
            {
                string semester = kvp.Key;
                int count = kvp.Value;
                String seme = String.Empty;
                oldsem += "'"+semester+"'" + ",";

            }
            oldsem = oldsem.TrimEnd(',');
            ViewState["oldsem"] = oldsem;

            //--------------------------------------------------------------------------
            #endregion

            #endregion
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.Findsemester() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }


    protected void btnPay_Click(object sender, EventArgs e)
    {
        StudentRegist objSR = new StudentRegist();
        //InsertBacklopgCourse();
        FeeCollectionController objFee = new FeeCollectionController();
        ExamController objEC = new ExamController();
        StudentRegistration objAddStud = new StudentRegistration();
        StudentRegistration objSRegist = new StudentRegistration();

        StudentController objSC1 = new StudentController();
        try
        {
            #region For Gathering Student Information

            int cntcourse = 0;

            //int ifPaidAlready = 0;
            //ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'EF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + lblSemester.ToolTip));
            //if (ifPaidAlready > 0)
            //{
            //    objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
            //    return;
            //}
            #region CheckBox  Count
            if (lvFailCourse.Items.Count > 0)
            {

                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    if (chk.Checked == true)
                        cntcourse++;
                }

            }


            if (cntcourse == 0)
            {
                objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
                bindcourses();
                return;
            }
            #endregion

            // int OrganizationId = Convert.ToInt32(Session["OrgId"]), degreeno = 0, college_id = 0;
            int idno = 0;
            
            idno = Convert.ToInt32(Session["idno"]);
            String Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
            objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
            objSR.IDNO = idno;
            objSR.REGNO = Regno;
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "IDNO=" + idno);
            String coursenos = string.Empty;
            String semesternos = string.Empty;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {

                    Label courseno = dataitem.FindControl("lblCCode") as Label;
                    Label semesterno = dataitem.FindControl("lblsemester") as Label;
                    coursenos += courseno.ToolTip + ",";
                    semesternos += semesterno.ToolTip+",";
                }

            }
            objSR.COURSENOS = coursenos.TrimEnd(',');

         

            objSR.SEMESTERNOS = semesternos.TrimEnd(',');
            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));

            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;
            objSA.IpAddress = ViewState["ipAddress"].ToString();
            CreateStudentPayOrderId();
            int ret = 0;
            #endregion

            #region For Spliting Semesternos
            // ---------------------------------------------try----------------------------
            // string semestern = "1,1,1,1,1,2,2,2,3,44";


            // Split the string into an array of items
            string[] semesters = semesternos.Split(',');

            // Create a dictionary to store the counts
            Dictionary<string, int> counts = new Dictionary<string, int>();

            // Loop through the items and count them
            foreach (string semester in semesters)
            {
                // Trim any leading/trailing whitespace in case there are spaces around the commas
                string trimmedSemester = semester.Trim();

                if (!string.IsNullOrEmpty(trimmedSemester))
                {
                    if (counts.ContainsKey(trimmedSemester))
                    {
                        counts[trimmedSemester]++;
                    }
                    else
                    {
                        counts[trimmedSemester] = 1;
                    }
                }
            }

            //Display the counts
            //foreach (var kvp in counts)
            //{
            //    string semester = kvp.Key;
            //    int count = kvp.Value;
            //    String seme = String.Empty;
            //    for (int i = 0; i < count; i++)
            //    {
            //        seme += semester + ",";
            //    }

            //}

            //--------------------------------------------------------------------------
            #endregion
        
            #region  For Registration
            foreach (var kvp in counts)
            {
                string semester = kvp.Key;
                int count = kvp.Value;
                String seme = String.Empty;
                objSR.SEMESTERNO = Convert.ToInt32( semester);

                //for (int i = 0; i < count; i++)
                //{
                //    seme += semester + ",";
                //}
               
                ret = objAddStud.AddExamRegistered_Summer(objSR);
               
            }
            
            
            if (ret == -99)
            {

                objCommon.DisplayMessage(updatepnl, "SOMETHING WENT WRONG!!!!!!!!!!", this.Page);
                return;
            }

            #endregion

            #region CREATE DEMAND
            // Demand Creation
            //string Amt = FinalTotal.Text;

            if (ViewState["CheckProcFee"] == string.Empty || ViewState["CheckProcFee"] == null)
            {
                ViewState["CheckProcFee"] = "0";
            }
            if (ViewState["CrettificateFee"] == string.Empty || ViewState["CrettificateFee"] == null)
            {
                ViewState["CrettificateFee"] = "0";
            }
            // = ProFess;//FESS HEAD F2
            // ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3

            if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"] == null)
            {
                ViewState["TotalSubFee"] = "0";
            }
            if (ViewState["latefee"] == string.Empty || ViewState["latefee"] == null)
            {
                ViewState["latefee"] = "0";
            }
            if (FinalTotal.Text == string.Empty || FinalTotal.Text == null)
            {
                FinalTotal.Text = "0";
            }
            //string Amt = ViewState["TotalSubFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;//commented by gaurav 21_08_2023

            //string Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;

            string totalamt = FinalTotal.Text;
            int retStatus = 0;
            foreach (var kvp in counts)
            {
                string semester = kvp.Key;
                int count = kvp.Value;
                String seme = String.Empty;

                //for (int i = 0; i < count; i++)
                //{
                //    seme += semester + ",";
                //}
                decimal PaperFees = 0;
                if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
                {
                    PaperFees = 0;
                }
                else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)
                {
                    //int Fee = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEE", "SEMESTERNO IN ('" + ViewState["oldsem"] + "')"));
                    String Fee = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEE", "SEMESTERNO IN (" + ViewState["oldsem"] + ")");
                    PaperFees = Convert.ToDecimal(Fee);


                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {

                        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && (dataitem.FindControl("lblsemester") as Label).Text == semester)
                        {
                            Label PaperFee = dataitem.FindControl("lblAmt") as Label;
                            String PaperFee1 = PaperFee.Text;
                            PaperFees = Convert.ToDecimal(PaperFee1) + PaperFees;
                        }

                    }
                }


                //objSR.SEMESTERNO = Convert.ToInt32(semester);
                //string Amt = PaperFees.ToString();
                string Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + PaperFees.ToString();


                objSR.SEMESTERNOS = semester;
                retStatus = objEC.AddStudentExamRegistrationDetails_Summer(objSR, Amt, ViewState["OrderId"].ToString());
            }

            //int retStatus = objEC.AddStudentExamRegistrationDetails_Summer(objSR, Amt, ViewState["OrderId"].ToString());
            //objCommon.DisplayMessage("Demand Created Successfull", this.Page);
            if (retStatus == -99)
            {
                objCommon.ShowError(Page, "ExamRegistration_Summert.aspx.btnPay_Click() --> ");
                return;
                //objCommon.DisplayMessage("Something Went Wrong", this.Page);
                //return;
            }
            #endregion


            #region For Update Student_Result_Table Column txt_ind=1
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    //if (chk.Checked == true)
                    //{
                    //    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    int flag;


                    if (chk.Checked == true)
                    {
                        flag = 1;

                    }
                    else
                    {
                        flag = 0;

                    }
                    int Idno = Convert.ToInt32(Session["idno"]);
                    int ccode = Convert.ToInt32(lblCCode.ToolTip);
                    if (Idno > 0)
                    {
                        string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSE_APPLY_SUMMERTERM";
                        //string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_OUT";
                        string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
                        // string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + ",0";
                        string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + flag + "," + ",0";
                        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

                        if (que_out == "1")
                        {
                            objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Done Sucessfully.", this.Page);

                            HideClm();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Update Sucessfully.", this.Page);
                            HideClm();
                        }

                        bindcourses();

                    }

                    //}
                }
            }
            #endregion

            #region Redirect to Payment Gateway

            double TotalAmount;
            int degreeno = 0;
            int college_id = 0;
            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = totalamt;
            ViewState["studAmt"] = totalamt;
            //TotalAmount = Convert.ToDouble(Amt);//Convert.ToDouble(ViewState["studAmt"].ToString());
            TotalAmount = Convert.ToDouble(totalamt);
            Session["studName"] = lblName.Text;
            Session["studPhone"] = ViewState["MOBILENO"].ToString();
            Session["studEmail"] = ViewState["EmailID"].ToString();
            Session["paysemester"] = lblSemester.ToolTip;
            Session["YEARNO"] = lblAdmBatch.Text;
            Session["ReceiptType"] = "EF";
            Session["idno"] = Convert.ToInt32(Session["idno"].ToString());
            Session["paysession"] = Convert.ToInt32(ViewState["sessionnonew"]);
            Session["homelink"] = "ExamRegistration_CC.aspx";
            Session["regno"] = lblEnrollNo.Text;
            Session["payStudName"] = lblName.Text;
            Session["paymobileno"] = ViewState["MOBILENO"].ToString();
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblBranch.Text;


            if (Session["OrgId"].ToString() == "6")// added by gaurav S. Rcpiper 19_07_2023
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }



            string payactivityno = objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVESTATUS=1 AND ACTIVITYNAME like '%SUM%'");
            if (payactivityno == string.Empty || payactivityno == null)
            {
                objCommon.DisplayMessage(updatepnl, "Payment Activity Master is not define.", this.Page);
                return;
            }
            Session["payactivityno"] = payactivityno;  // 
            //int PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%PAYU%'"));
            //  Session["PAYID"] = PAYID;
            //Session["PAYID"] = 0;
            string PAYID;
            PAYID = objCommon.LookUp("ACD_PAYMENT_GATEWAY", "TOP (1) PAYID", "ACTIVE_STATUS=1 ");
            Session["PAYID"] = PAYID;
            if (PAYID == string.Empty || PAYID == null)
            {
                objCommon.DisplayMessage(updatepnl, "Payment GATEWAY is Not Define...! Please Contact To Admin", this.Page);
                bindcourses();
                return;
            }

            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, Convert.ToInt32(Session["PAYID"]), Convert.ToInt32(Session["payactivityno"]), Convert.ToInt32(degreeno), Convert.ToInt32(college_id));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {
                    objCommon.DisplayMessage(updatepnl, "Payment Gatway is Define More then one time...! Please Contact To Admin", this.Page);
                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Session["AccessCode"] = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }
            else
            {

                objCommon.DisplayMessage(updatepnl, "Payment configuration is not done for this session.", this.Page);
                bindcourses();
                return;

            }
            #endregion



            #region Commented Code

            //if (lvFailCourse.Items.Count > 0)
            //{
            //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //    {
            //        int flag;
            //        CheckBox chk = dataitem.FindControl("chkaccept") as CheckBox;
            //        Label lblccode = dataitem.FindControl("lblccode") as Label;
            //        if (chk.Checked == true)
            //        {
            //            flag = 1;
            //        }
            //        else
            //        {
            //            flag = 0;
            //        }


            //        int ccode = Convert.ToInt32(lblccode.ToolTip);
            //        if (idno > 0)
            //        {
            //            foreach (var kvp in counts)
            //            {
            //                string semester = kvp.Key;
            //                int count = kvp.Value;
            //                //String seme = String.Empty;
            //                //for (int i = 0; i < count; i++)
            //                //{
            //                //    seme += semester + ",";
            //                //}
            //                string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSES_APPLY";
            //                string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_SEMESTERNO,@P_STATUS,@P_OUT";
            //                string Call_Values = "" + idno + "," + Convert.ToInt32(ViewState["sessionnonew"]) + ","+Convert.ToInt32(semester)+"," + ccode + "," + flag + ",0";
            //                string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            //                if (que_out == "1")
            //                {
            //                    // objCommon.DisplayMessage("Course Registration done Sucessfully, Wait for the final approval from the Head of Department", this.Page);
            //                }
            //                else
            //                {
            //                    //objCommon.DisplayMessage("Course Registration Update Sucessfully, Wait for the final approval from the Head of Department", this.Page);
            //                }
            //            }
                       
            //        }

            //    }
            //}
            #endregion

            #region Commented Code
            // end comment
            //    // return;

            //    if (lvFailCourse.Items.Count > 0)
            //    {
            //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //        {
            //            int flag;
            //            CheckBox chk = dataitem.FindControl("chkaccept") as CheckBox;
            //            Label lblccode = dataitem.FindControl("lblccode") as Label;
            //            if (chk.Checked == true)
            //            {
            //                flag = 1;
            //            }
            //            else
            //            {
            //                flag = 0;
            //            }


            //            int ccode = Convert.ToInt32(lblccode.ToolTip);
            //            if (idno > 0)
            //            {
            //                string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION";
            //                string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
            //                string Call_Values = "" + idno + "," + Convert.ToInt32(ViewState["sessionnonew"]) + "," + ccode + "," + flag + ",0";
            //                string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            //                if (que_out == "1")
            //                {
            //                    // objCommon.DisplayMessage("Course Registration done Sucessfully, Wait for the final approval from the Head of Department", this.Page);
            //                }
            //                else
            //                {
            //                    //objCommon.DisplayMessage("Course Registration Update Sucessfully, Wait for the final approval from the Head of Department", this.Page);
            //                }
            //            }

            //        }
            //    }


            //    double TotalAmount;
            //    int degreeno = 0;
            //    int college_id = 0;
            //    Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            //    int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            //    string PaymentMode = "ONLINE FEES COLLECTION";
            //    Session["PaymentMode"] = PaymentMode;
            //    Session["studAmt"] = totalamt;
            //    ViewState["studAmt"] = totalamt;
            //    //TotalAmount = Convert.ToDouble(Amt);//Convert.ToDouble(ViewState["studAmt"].ToString());
            //    TotalAmount = Convert.ToDouble(totalamt);
            //    Session["studName"] = lblName.Text;
            //    Session["studPhone"] = ViewState["MOBILENO"].ToString();
            //    Session["studEmail"] = ViewState["EmailID"].ToString();
            //    Session["paysemester"] = lblSemester.ToolTip;
            //    Session["YEARNO"] = lblAdmBatch.Text;
            //    Session["ReceiptType"] = "EF";
            //    Session["idno"] = Convert.ToInt32(Session["idno"].ToString());
            //    Session["paysession"] = Convert.ToInt32(ViewState["sessionnonew"]);
            //    Session["homelink"] = "ExamRegistration_CC.aspx";
            //    Session["regno"] = lblEnrollNo.Text;
            //    Session["payStudName"] = lblName.Text;
            //    Session["paymobileno"] = ViewState["MOBILENO"].ToString();
            //    Session["Installmentno"] = "0";
            //    Session["Branchname"] = lblBranch.Text;


            //    if (Session["OrgId"].ToString() == "6")// added by gaurav S. Rcpiper 19_07_2023
            //    {
            //        degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            //    }



            //    string payactivityno = objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVESTATUS=1 AND ACTIVITYNAME like '%Exam%'");
            //    if (payactivityno == string.Empty || payactivityno == null)
            //    {
            //        objCommon.DisplayMessage(updatepnl, "Payment Activity Master is not define.", this.Page);
            //        return;
            //    }
            //    Session["payactivityno"] = payactivityno;  // 
            //    //int PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%PAYU%'"));
            //    //  Session["PAYID"] = PAYID;
            //    //Session["PAYID"] = 0;
            //    string PAYID;
            //    PAYID = objCommon.LookUp("ACD_PAYMENT_GATEWAY", "TOP (1) PAYID", "ACTIVE_STATUS=1 ");
            //    Session["PAYID"] = PAYID;
            //    if (PAYID == string.Empty || PAYID == null)
            //    {
            //        objCommon.DisplayMessage(updatepnl, "Payment GATEWAY is Not Define...! Please Contact To Admin", this.Page);
            //        bindcourses();
            //        return;
            //    }

            //    DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, Convert.ToInt32(Session["PAYID"]), Convert.ToInt32(Session["payactivityno"]), Convert.ToInt32(degreeno), Convert.ToInt32(college_id));
            //    if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            //    {
            //        if (ds1.Tables[0].Rows.Count > 1)
            //        {
            //            objCommon.DisplayMessage(updatepnl, "Payment Gatway is Define More then one time...! Please Contact To Admin", this.Page);
            //        }
            //        else
            //        {
            //            Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
            //            string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            //            Session["AccessCode"] = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
            //            Response.Redirect(RequestUrl);
            //        }
            //    }
            //    else
            //    {

            //        objCommon.DisplayMessage(updatepnl, "Payment configuration is not done for this session.", this.Page);
            //        bindcourses();
            //        return;

            //    }

            #endregion

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.btnPay_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }



    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        try
        {

            StudentRegistration objAddStud = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();
            StudentController objSC1 = new StudentController();

            #region For Gathering Student Information
            int idno = 0;
            idno = Convert.ToInt32(Session["idno"]);
            string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
            objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);

            objSR.IDNO = idno;
            objSR.REGNO = Regno;
            objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());

            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));

            objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "IDNO=" + idno);
            
            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;
            objSA.SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
            objSA.IpAddress = ViewState["ipAddress"].ToString();
            objSR.EXAM_REGISTERED = 0;
            //objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
            objSR.TotalFee = objSR.Backlogfees;
            String coursenos = string.Empty;
            String semesternos = string.Empty;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {

                    Label courseno = dataitem.FindControl("lblCCode") as Label;
                    Label semesterno = dataitem.FindControl("lblsemester") as Label;
                    coursenos += courseno.ToolTip + ",";
                    semesternos += semesterno.ToolTip + ",";
                }

            }
            objSR.COURSENOS = coursenos.TrimEnd(',');
            objSR.SEMESTERNOS = semesternos.TrimEnd(',');

            #endregion

            #region For spliting semesternos
            // ---------------------------------------------try----------------------------
            // string semestern = "1,1,1,1,1,2,2,2,3,44";


            // Split the string into an array of items
            string[] semesters = semesternos.Split(',');

            // Create a dictionary to store the counts
            Dictionary<string, int> counts = new Dictionary<string, int>();

            // Loop through the items and count them
            foreach (string semester in semesters)
            {
                // Trim any leading/trailing whitespace in case there are spaces around the commas
                string trimmedSemester = semester.Trim();

                if (!string.IsNullOrEmpty(trimmedSemester))
                {
                    if (counts.ContainsKey(trimmedSemester))
                    {
                        counts[trimmedSemester]++;
                    }
                    else
                    {
                        counts[trimmedSemester] = 1;
                    }
                }
            }

            #endregion

            #region For Registration
            int ret = 0;

            foreach (var kvp in counts)
            {
                string semester = kvp.Key;
                int count = kvp.Value;
                String seme = String.Empty;
                objSR.SEMESTERNO = Convert.ToInt32(semester);


                ret = objAddStud.AddExamRegistered_Summer(objSR);
            }

            if (ret == -99)
            {

                objCommon.DisplayMessage(updatepnl, "SOMETHING WENT WRONG!!!!!!!!!!", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(updatepnl, "Exam Registered Successfull", this.Page);
            }
            #endregion

            #region For Update Student_Result_Table Column txt_ind=1
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    //if (chk.Checked == true)
                    //{
                    //    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    int flag;
                    int Exreg;

                    if (chk.Checked == true)
                    {
                        flag = 1;
                        Exreg = 1;
                    }
                    else
                    {
                        flag = 0;
                        Exreg = 0;
                    }
                    int Idno = Convert.ToInt32(Session["idno"]);
                    int ccode = Convert.ToInt32(lblCCode.ToolTip);
                    if (Idno > 0)
                    {
                        string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSE_APPLY_SUMMER";
                        //string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_OUT";
                        string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_EXAM_REGISTERED,@P_OUT";
                        // string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + ",0";
                        string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + flag + "," + Exreg + ",0";
                        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

                        if (que_out == "1")
                        {
                            objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Done Sucessfully.", this.Page);

                            HideClm();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Update Sucessfully.", this.Page);
                            HideClm();
                        }

                        bindcourses();

                    }

                    //}
                }
            }
            #endregion
           

        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.btnSubmit_Click() " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("CourseRegistration", "rptExam_Reg_Summer.rpt");


    }
    protected void CalculateTotal()
    {
        lblTotalExamFee.Text = "0.00";
        lblfessapplicable.Text = "0.00";
        decimal ProFess;
        ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(SUM(APPLICABLEFEE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

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
                string TotalAmt = Amt.ToString();
                lblTotalExamFee.Text = TotalAmt.ToString();
                lblfessapplicable.Text = ProFess.ToString();

                if (lblfessapplicable.Text == string.Empty)
                {
                    lblfessapplicable.Text = "0.00";
                }


            }
        }
        FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
    }


    protected void chkaccept_checkedchanged(object sender, EventArgs e)
    {
        try
        {
            #region FIX
            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)//FIX
            {
                CheckBox litText = lvFailCourse.FindControl("chkAll") as CheckBox;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    if (cbRow.Checked == false)
                    {
                        litText.Checked = false;

                    }
                }

               // HideClm();

            }
            #endregion
            #region COURSE WISE FEE
            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)
            {
                CheckBox litText = lvFailCourse.FindControl("chkAll") as CheckBox;// added for listview header True/False.

                int count = 0;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    // CheckBox cbRowhead = dataitem.FindControl("chkAll") as CheckBox;
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {

                        Label lblAmt = dataitem.FindControl("lblAmt") as Label;
                        HiddenField hdfExamRegistered = dataitem.FindControl("hdfExamRegistered") as HiddenField;
                        HiddenField hdfStudRegistered = dataitem.FindControl("hdfStudRegistered") as HiddenField;
                        HiddenField hdfSubid = dataitem.FindControl("hdfSubid") as HiddenField;
                        decimal CourseAmt = Convert.ToDecimal(lblAmt.Text);
                        if (cbRow.Checked == true)
                        {
                            Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                            count++;
                        }

                    }
                    else if (cbRow.Checked == false)
                    {
                        litText.Checked = false;

                    }
                }

                string TotalAmt = Amt.ToString();
                lblTotalExamFee.Text = TotalAmt.ToString();

                if (lblfessapplicable.Text == string.Empty)
                {
                    lblfessapplicable.Text = "0";
                }

                //  ViewState["TotalSubFee"]= (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();// commented by gaurav 21_08_2023
                ViewState["TotalSubFee"] = Convert.ToDecimal(TotalAmt);
                FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"])).ToString();
                Amt = 0;
                CourseAmtt = 0;

            }
            #endregion
            #region Credit Range Wise
            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)//credit wise
            {

                CalculateTotalCredit();
                HideClm();

            }
            #endregion
            #region without fee
            else
            {

                CheckBox litText = lvFailCourse.FindControl("chkAll") as CheckBox;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    if (cbRow.Checked == false)
                    {
                        litText.Checked = false;

                    }
                }

                HideClm();


            }
            #endregion
            //  }
            // else { }

            //#endregion 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.chkAccept_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            #region FIXFEE
            if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)//FIX FEE
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
                        //lblTotalExamFee.Text = "0.00";
                    }
                }


                HideClm();

            }
            #endregion
            #region COURSE WISE FEE
            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)// COURSE WISE FEE           
            {
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
                    lblTotalExamFee.Text = TotalAmt.ToString();
                    if (lblfessapplicable.Text == string.Empty || lblfessapplicable.Text == null)
                    {
                        lblfessapplicable.Text = "0";
                    }
                    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text) +
                        Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"])).ToString();

                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = false;
                        string TotalAmt = Amt.ToString();
                        lblTotalExamFee.Text = TotalAmt.ToString();

                        if (lblfessapplicable.Text == string.Empty || lblfessapplicable.Text == null)
                        {
                            lblfessapplicable.Text = "0";
                        }
                        FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"])).ToString();
                    }

                }
            }
            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)// Credit wise   
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
                CalculateTotalCredit();

                HideClm();
            }


            #endregion
            #region Without Fee
            else
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

                HideClm();

            }
            #endregion


        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.chkAll_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
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
    #region CREATE TOKEn
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
    #endregion


    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            int applycourse = 0;
            #region MIT OR RCPIPER
            //if (Convert.ToInt32(Session["OrgId"]) == 6 || Convert.ToInt32(Session["OrgId"]) == 8 || Convert.ToInt32(Session["OrgId"]) == 3)//RCPIPER
            //{
            //    if (e.Item.ItemType == ListViewItemType.DataItem)
            //    {
            //        CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
            //        CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
            //        HiddenField hdftxn = (HiddenField)e.Item.FindControl("hdfextind");

            //        chk.Checked = true;
            //        // chk.Enabled = false;
            //        chkhead.Checked = true;
            //        //chkhead.Enabled = false;
            //        if (hdftxn.Value == "1")
            //        {
            //            chk.Checked = true;
            //            chkhead.Checked = false;
            //        }
            //        else
            //        {
            //            chk.Checked = false;
            //            chkhead.Checked = false;


            //        }
            //    }
            //}
            //else
            #endregion MIT OR RCPIPER

            #region Check For Admin

            if (ViewState["usertype"].ToString() == "1")
            {
                CheckBox chk1 = (CheckBox)e.Item.FindControl("chkAccept");
                CheckBox chkhead1 = lvFailCourse.FindControl("chkAll") as CheckBox;
                applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(EXT_IND,0)=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                if (applycourse > 0)
                {
                    if (e.Item.ItemType == ListViewItemType.DataItem)
                    {
                        //CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                        //HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                        //CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                        HiddenField hdf2 = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                        HiddenField hdf1 = (HiddenField)e.Item.FindControl("hdfextind");
                        CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                        if (hdf1.Value == "1")
                        {

                            if (hdf2.Value == "1")
                            {
                                chk.Checked = true;
                                chk.Enabled = false;
                                chkhead.Checked = false;
                                chkhead.Enabled = false;

                            }
                            else
                            {
                                chk.Checked = true;
                                chk.Enabled = true;
                                chkhead.Checked = false;
                                chkhead.Enabled = false;
                            }
                        }

                        else
                        {
                            chk.Checked = false;
                            chk.Enabled = true;

                        }

                    }
                }
                else
                {
                    chk1.Checked = false;
                    chk1.Enabled = true;
                    chkhead1.Checked = false;
                    chkhead1.Enabled = true;
                }
            }
            #endregion

            #region For Student
            if (ViewState["usertype"].ToString() == "2")
            {
                int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
                //CHECK FEES APPlCABLE OR NOT 
                int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=5 AND COLLEGE_ID=" + cid + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));


                if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)
                {
                    applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(EXT_IND,0)=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                    #region For Check Fixed Fees Type
                    if (e.Item.ItemType == ListViewItemType.DataItem)
                    {
                        //CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                        //HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                        //CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;

                        CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                        HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                        HiddenField hdf1 = (HiddenField)e.Item.FindControl("hdfextind");
                        CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;


                        if (hdf1.Value == "1")
                        {
                            if (hdf1.Value == "1")
                            {
                                chk.Checked = true;
                                chk.Enabled = false;
                                chkhead.Checked = false;
                                chkhead.Enabled = false;
                            }
                            else 
                            {
                                chk.Checked = true;
                                chk.Enabled = true;
                                chkhead.Checked = false;
                                chkhead.Enabled = true;
                            }
                           
                        }

                        else
                        {
                            chk.Checked = false;
                            chk.Enabled = true;

                        }
                    }
                    #endregion
                }


                else
                {
                    #region CHeckFeesApplicable or Not
                    if (CheckExamfeesApplicableOrNot >= 1)
                    {

                        applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(EXT_IND,0)=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                        if (applycourse > 0)
                        {
                            if (e.Item.ItemType == ListViewItemType.DataItem)
                            {
                                //CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                                //HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                                //CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                                HiddenField hdf2 = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                                HiddenField hdf1 = (HiddenField)e.Item.FindControl("hdfextind");

                                CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                                if (hdf1.Value == "1")
                                {

                                    if (hdf2.Value == "1")
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = false;
                                        chkhead.Checked = false;
                                        chkhead.Enabled = false;
                                    }
                                    else
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = true;
                                        chkhead.Checked = false;
                                        chkhead.Enabled = true;
                                    }
                                }
                                else
                                {
                                    chk.Checked = false;
                                    chk.Enabled = false;

                                }
                            }
                        }



                    }

                    else
                    {

                        applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(EXT_IND,0)=1 AND  SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                        if (applycourse > 0)
                        {
                            if (e.Item.ItemType == ListViewItemType.DataItem)
                            {
                                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                                HiddenField hdf = (HiddenField)e.Item.FindControl("hdfExamRegistered");
                                HiddenField hdf1 = (HiddenField)e.Item.FindControl("hdfextind");

                                CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                                if (hdf1.Value == "1")
                                {
                                    if (hdf.Value == "1")
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = false;
                                        chkhead.Checked = false;
                                        chkhead.Enabled = false;
                                    }
                                    chk.Checked = true;
                                    chk.Enabled = true;
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
                    #endregion
                }

            }
        }
            #endregion



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.aspx.lvFailCourse_ItemDataBound() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }


    #region Commented Code
    //protected void CalculateTotalFixFee()// FEESTRUCTURE_TYPE=4 fix
    //{

    //    Calculate_All_HEADS();
    
    //    //lblTotalExamFee.Text = "0.00";
    //    //  lblfessapplicable.Text = "0.00";
    //    // decimal ProFess;
    //    decimal ApplFess;
    //    // decimal CrettificateFee;
    //    // decimal latefees;
    //    // decimal FeeHead4FEE; decimal FeeHead5FEE; decimal FeeHead6FEE; decimal FeeHead7FEE; decimal FeeHead8FEE;
    //    // decimal FeeHead9FEE; decimal FeeHead10FEE;
    //    #region not in use
    //    //#region ChkProcessing Fee Head 2

    //    //bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    ProFess = 0;
    //    //    ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2+
    //    //    lvitemH2.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Certificate Fee Applicable  HEAD 3
    //    //bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckCrettificateFee == true)
    //    //{

    //    //    CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3

    //    //}
    //    //else
    //    //{
    //    //    CrettificateFee = 0;
    //    //    ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
    //    //    lvitemH3.Visible = false;
    //    //}
    //    //#endregion    
    //    //#region Fee Head 4

    //    //bool FeeHead4 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H4,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead4FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H4_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead4FEE"] = FeeHead4FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead4FEE = 0;
    //    //    ViewState["FeeHead4FEE"] = FeeHead4FEE;//FESS HEAD F2+
    //    //    lvitemH4.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Fee Head 5

    //    //bool FeeHead5 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H5,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead5FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H5_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead4FEE"] = FeeHead4FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead5FEE = 0;
    //    //    ViewState["FeeHead5FEE"] = FeeHead5FEE;//FESS HEAD F2+
    //    //    lvitemH5.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Fee Head 6

    //    //bool FeeHead6 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H6,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead6FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H6_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead6FEE"] = FeeHead6FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead6FEE = 0;
    //    //    ViewState["FeeHead6FEE"] = FeeHead6FEE;//FESS HEAD F2+
    //    //    lvitemH6.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Fee Head 7

    //    //bool FeeHead7 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H7,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead7FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H7_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead7FEE"] = FeeHead7FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead7FEE = 0;
    //    //    ViewState["FeeHead7FEE"] = FeeHead7FEE;//FESS HEAD F2+
    //    //    lvitemH7.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Fee Head 8

    //    //bool FeeHead8 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H8,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead8FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H8_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead8FEE"] = FeeHead8FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead8FEE = 0;
    //    //    ViewState["FeeHead8FEE"] = FeeHead8FEE;//FESS HEAD F2+
    //    //    lvitemH8.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Fee Head 9

    //    //bool FeeHead9 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H9,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead9FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H9_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead9FEE"] = FeeHead9FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead9FEE = 0;
    //    //    ViewState["FeeHead9FEE"] = FeeHead9FEE;//FESS HEAD F2+
    //    //    lvitemH9.Visible = false;
    //    //}
    //    //#endregion
    //    //#region Fee Head 10

    //    //bool FeeHead10 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H10,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (CheckProcFee == true)
    //    //{
    //    //    FeeHead10FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H10_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //    ViewState["FeeHead10FEE"] = FeeHead10FEE;//FESS HEAD F2
    //    //}
    //    //else
    //    //{

    //    //    FeeHead10FEE = 0;
    //    //    ViewState["FeeHead10FEE"] = FeeHead10FEE;//FESS HEAD F2+
    //    //    lvitemH10.Visible = false;
    //    //}
    //    //#endregion 
    //    //#region LATE FEE HEAD
    //    //DataSet dsStudent = null;
    //    //string date = string.Empty;
    //    //string strNewDate = string.Empty;
    //    //string format = "dd/mm/yyyy";
    //    //int type = 0;
    //    //string dateString = string.Empty;
    //    //decimal calculatelatefee = 0;
    //    //string totalsubfee = string.Empty;
    //    //bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //if (Latefee == true)
    //    //{

    //    //    latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


    //    //    DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
    //    //    if (ds.Tables[0].Rows.Count > 0)
    //    //    {
    //    //        date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
    //    //        dateString = date.Substring(0, 11);
    //    //        DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
    //    //        strNewDate = dateTime.ToString("yyyy-mm-dd");
    //    //        type = Convert.ToInt32(ds.Tables[0].Rows[0]["TYPE"]);
    //    //    }
    //    //    string SP_Name = "ACD_CALCULATE_DAY_WEEK_MONTHLY";
    //    //    string SP_Parameters = "@P_DATE, @P_TYPE";
    //    //    string Call_Values = "" + strNewDate + "," + Convert.ToInt32(type) + "";// +"," + Convert.ToInt16(ViewState["sem"]) + "," + 
    //    //    dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);


    //    //    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"])>0)
    //    //    {
    //    //        calculatelatefee = latefees * Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

    //    //    }
    //    //    else
    //    //    {
    //    //        calculatelatefee = 0;
    //    //    }
    //    //    // decimal abc = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);
    //    //    //calculatelatefee = latefees * abc;
    //    //    if (type == 1)
    //    //    {
    //    //        lblLateFee.Text = "<b style='color:red;'>PERDAY </b> : " + dateString +"  "+ latefees.ToString();
    //    //    }
    //    //    else if (type == 1)
    //    //    {
    //    //        lblLateFee.Text = "<b style='color:red;'>WEEKLY </b> : " + dateString + "  " + latefees.ToString();
    //    //       // lblLateFee.Text = "WEEKLY   " + latefees.ToString();
    //    //    }
    //    //    else if (type == 3)
    //    //    {
    //    //        lblLateFee.Text = "<b style='color:red;'>MONTHLY </b> : " + dateString + "  " + latefees.ToString();             

    //    //    }



    //    //}
    //    //else
    //    //{
    //    //    latefees = 0;
    //    //    lvLatefeesHead.Visible=false;
    //    //}
    //    //#endregion    
    //    #endregion

    //    ApplFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(CANCEL,0)=0 and FEESTRUCTURE_TYPE=4 AND    ISNULL(IsFeesApplicable,0)=1 and  COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

    //    //  lblfessapplicable.Text = ProFess.ToString();
    //    lblTotalExamFee.Text = ApplFess.ToString();
    //    //  lblCertificateFee.Text = CrettificateFee.ToString();
    //    // lblLateFee.Text = latefees.ToString();


    //    //if (lblfessapplicable.Text == string.Empty)
    //    //   {
    //    //       lblfessapplicable.Text = "0.00";
    //    //   }
    //    //  else if(lblTotalExamFee.Text == string.Empty)
    //    //  {
    //    //              lblTotalExamFee.Text = "0.00";
    //    //  }
    //    //  else if (lblCertificateFee.Text == string.Empty)
    //    //  {
    //    //      lblCertificateFee.Text = "0.00";
    //    //  }
    //    //  else if (lblLateFee.Text == string.Empty)
    //    //  {
    //    //      lblLateFee.Text = "0.00";
    //    //  }


    //    // totalsubfee = (ProFess + ApplFess + CrettificateFee).ToString();//added for calculate seprate fee course 18_02_20235

    //    //  ViewState["TotalSubFee"] = totalsubfee;//Commented by Gaurav 21_08_2023
    //    // ViewState["TotalSubFee"] = lblTotalExamFee.Text;
    //    ViewState["TotalSubFee"] = ApplFess.ToString();

    //    // ViewState["latefee"] = calculatelatefee;
    //    // FinalTotal.Text = (ProFess + ApplFess + CrettificateFee + calculatelatefee).ToString();
    //    FinalTotal.Text = (Convert.ToInt32(ViewState["all_head_total"]) + ApplFess).ToString();
    //}

      #endregion

    protected void CalculateTotalCredit()//FEESTRUCTURE_TYPE=5  CREDITWISE
    {
        string PAYID = string.Empty;
        lblTotalExamFee.Text = string.Empty;
        lblfessapplicable.Text = string.Empty;
        string TotalAmt = string.Empty;
        //Processing Fee Applicable
        decimal ProFess;
        decimal CrettificateFee;
        decimal latefees;

        #region ChkProcessing Fee

        bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckProcFee == true)
        {
            ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        else
        {
            ProFess = 0;
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        #endregion
        #region Certificate Fee Applicable
        bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckCrettificateFee == true)
        {

            CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
        }
        else
        {
            CrettificateFee = 0;
            ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
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
        bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (Latefee == true)
        {

            latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


            DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
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
        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
        {

            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCredit = dataitem.FindControl("lblcredits") as Label;
                    // HiddenField hdfCreditTotal = dataitem.FindControl("hdfCreditTotal") as HiddenField;
                    decimal CourseCredit = Convert.ToDecimal(lblCredit.Text.ToString());
                    if (lblCredit.Text == string.Empty)
                    {
                        lblCredit.Text = "0.00";
                    }
                    if (cbRow.Checked == true)
                    {
                        Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseCredit);
                    }
                    TotalAmt = Amt.ToString();
                    //lblTotalExamFee.Text = TotalAmt.ToString();

                    if (lblfessapplicable.Text == string.Empty)
                    {
                        lblfessapplicable.Text = "0.00";
                    }


                }
            }
            if (TotalAmt == string.Empty || TotalAmt == null)
            {
                TotalAmt = "0";
            }
            hdfCreditTotal.Value = Convert.ToDecimal(TotalAmt).ToString();
            #region FOR CREDIT COURSR WISE CALCULATION
            //COMPARE CREDIT TOTAL WISE FEE
            PAYID = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "CREDIT_RANGE_AMOUNT", "MINRANGE<=" + hdfCreditTotal.Value + " AND MAXRANGE>=" + hdfCreditTotal.Value + " AND SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "and FEETYPE=5 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]) + " and ISNULL(CANCEL,0)=0 AND  FEESTRUCTURE_TYPE=5  AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'");
            if (PAYID == string.Empty || PAYID == null)
            {
                PAYID = "0";
            }

            ViewState["TotalSubFee"] = PAYID; // added by gaurav 21_08_2023
            #endregion


        }
        #endregion
        #region 3/1-Course Wise
        else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3 || Convert.ToInt32(ViewState["FEESTYPE"]) == 1)//3-Course Wise
        {

            //foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //{

            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {
            //        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            //        Label lblAmt = dataitem.FindControl("lblAmt") as Label;

            //        if (lblAmt.Text == string.Empty)
            //        {
            //            lblAmt.Text = "0.00";
            //        }
            //        decimal CourseAmt = Convert.ToDecimal(lblAmt.Text.ToString());
            //        if (cbRow.Checked == true)
            //        {
            //            Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
            //        }
            //        TotalAmt = Amt.ToString();
            //        lblTotalExamFee.Text = TotalAmt.ToString();
            //        lblfessapplicable.Text = ProFess.ToString();

            //        if (lblfessapplicable.Text == string.Empty)
            //        {
            //            lblfessapplicable.Text = "0.00";
            //        }


            //    }
            //}

            //TotalAmt = Amt.ToString();


            //ViewState["TotalSubFee"] = TotalAmt.ToString();
            ////lblTotalExamFee.Text = TotalAmt.ToString();
        }
        #endregion

        lblfessapplicable.Text = ProFess.ToString();//proc         
        lblTotalExamFee.Text = PAYID;//total sub       
        lblCertificateFee.Text = CrettificateFee.ToString();
        lblTotalExamFee.Text = Amt.ToString();


        if (PAYID == string.Empty || PAYID == null)
        {
            PAYID = "0";
        }
        if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
        {
            lblTotalExamFee.Text = PAYID.ToString();
            // FinalTotal.Text = (Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString(); }
            //ViewState["TotalSubFee"] = (Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();//Commented by gaurav 21_08_2023
            ViewState["TotalSubFee"] = Convert.ToDecimal(PAYID);// + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();

            FinalTotal.Text = (Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + calculatelatefee).ToString();

        }
        else
        {

            //FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
            //FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + calculatelatefee).ToString();
        }



    }

    protected void HideClm()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(8)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(8)').hide();});", true);
    } 

    protected void btnSubmit_WithDemand_Click(object sender, EventArgs e)
    {

       
         #region commented code
            //int cntcourse = 0;

            //int ifPaidAlready = 0;
            //ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'EF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + lblSemester.ToolTip));
            //if (ifPaidAlready > 0)
            //{
            //    objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
            //    return;
            //}

            //if (lvFailCourse.Items.Count > 0)
            //{

            //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //    {
            //        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            //        Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            //        if (chk.Checked == true)
            //            cntcourse++;
            //    }

            //}
            //if (cntcourse == 0)
            //{
            //    objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
            //    bindcourses();
            //    return;
            //}
            #endregion
            //----
            StudentRegist objSR = new StudentRegist();
            //InsertBacklopgCourse();
            FeeCollectionController objFee = new FeeCollectionController();
            ExamController objEC = new ExamController();
            StudentRegistration objAddStud = new StudentRegistration();
            StudentRegistration objSRegist = new StudentRegistration();

            StudentController objSC1 = new StudentController();
            try
            {
                #region For Gathering Student Information

                int cntcourse = 0;

                //int ifPaidAlready = 0;
                //ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'EF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + lblSemester.ToolTip));
                //if (ifPaidAlready > 0)
                //{
                //    objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                //    return;
                //}
                #region CheckBox  Count
                if (lvFailCourse.Items.Count > 0)
                {

                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                        if (chk.Checked == true)
                            cntcourse++;
                    }

                }


                if (cntcourse == 0)
                {
                    objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
                    bindcourses();
                    return;
                }
                #endregion

                // int OrganizationId = Convert.ToInt32(Session["OrgId"]), degreeno = 0, college_id = 0;
                int idno = 0;

                idno = Convert.ToInt32(Session["idno"]);
                String Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
                objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
                objSR.IDNO = idno;
                objSR.REGNO = Regno;
                objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objSR.ROLLNO = objCommon.LookUp("ACD_STUDENT", "ROLLNO", "IDNO=" + idno);
                String coursenos = string.Empty;
                String semesternos = string.Empty;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {

                        Label courseno = dataitem.FindControl("lblCCode") as Label;
                        Label semesterno = dataitem.FindControl("lblsemester") as Label;
                        coursenos += courseno.ToolTip + ",";
                        semesternos += semesterno.ToolTip + ",";
                    }

                }
                objSR.COURSENOS = coursenos.TrimEnd(',');



                objSR.SEMESTERNOS = semesternos.TrimEnd(',');
                int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
                int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));

                objSA.DegreeNo = degreenos;
                objSA.BranchNo = branchnos;
                objSA.IpAddress = ViewState["ipAddress"].ToString();
                CreateStudentPayOrderId();
                int ret = 0;
                #endregion

                #region For Spliting Semesternos
                // ---------------------------------------------try----------------------------
                // string semestern = "1,1,1,1,1,2,2,2,3,44";


                // Split the string into an array of items
                string[] semesters = semesternos.Split(',');

                // Create a dictionary to store the counts
                Dictionary<string, int> counts = new Dictionary<string, int>();

                // Loop through the items and count them
                foreach (string semester in semesters)
                {
                    // Trim any leading/trailing whitespace in case there are spaces around the commas
                    string trimmedSemester = semester.Trim();

                    if (!string.IsNullOrEmpty(trimmedSemester))
                    {
                        if (counts.ContainsKey(trimmedSemester))
                        {
                            counts[trimmedSemester]++;
                        }
                        else
                        {
                            counts[trimmedSemester] = 1;
                        }
                    }
                }

                // Display the counts
                //foreach (var kvp in counts)
                //{
                //    string semester = kvp.Key;
                //    int count = kvp.Value;
                //    String seme = String.Empty;
                //    for (int i = 0; i < count; i++)
                //    {
                //        seme += semester + ",";
                //    }

                //}

                //--------------------------------------------------------------------------
                #endregion

                #region  For Registration
                foreach (var kvp in counts)
                {
                    string semester = kvp.Key;
                    int count = kvp.Value;
                    String seme = String.Empty;
                    objSR.SEMESTERNO = Convert.ToInt32(semester);

                    //for (int i = 0; i < count; i++)
                    //{
                    //    seme += semester + ",";
                    //}

                    ret = objAddStud.AddExamRegistered_Summer(objSR);
                }

                if (ret == -99)
                {

                    objCommon.DisplayMessage(updatepnl, "SOMETHING WENT WRONG!!!!!!!!!!", this.Page);
                    return;
                }

                #endregion

                #region CREATE DEMAND
                // Demand Creation
                //string Amt = FinalTotal.Text;

                if (ViewState["CheckProcFee"] == string.Empty || ViewState["CheckProcFee"] == null)
                {
                    ViewState["CheckProcFee"] = "0";
                }
                if (ViewState["CrettificateFee"] == string.Empty || ViewState["CrettificateFee"] == null)
                {
                    ViewState["CrettificateFee"] = "0";
                }
                // = ProFess;//FESS HEAD F2
                // ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3

                if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"] == null)
                {
                    ViewState["TotalSubFee"] = "0";
                }
                if (ViewState["latefee"] == string.Empty || ViewState["latefee"] == null)
                {
                    ViewState["latefee"] = "0";
                }
                if (FinalTotal.Text == string.Empty || FinalTotal.Text == null)
                {
                    FinalTotal.Text = "0";
                }
                //string Amt = ViewState["TotalSubFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;//commented by gaurav 21_08_2023

                //string Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;

                string totalamt = FinalTotal.Text;
                int retStatus = 0;
                foreach (var kvp in counts)
                {
                    string semester = kvp.Key;
                    int count = kvp.Value;
                    String seme = String.Empty;

                    //for (int i = 0; i < count; i++)
                    //{
                    //    seme += semester + ",";
                    //}
                    decimal PaperFees = 0;
                    if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)
                    {
                        PaperFees = 0;
                    }
                    else
                    {
                        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        {

                            if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true && (dataitem.FindControl("lblsemester") as Label).Text == semester)
                            {


                                Label PaperFee = dataitem.FindControl("lblAmt") as Label;
                                String PaperFee1 = PaperFee.Text;
                                PaperFees = Convert.ToDecimal(PaperFee1) + PaperFees;
                            }

                        }
                    }


                    //objSR.SEMESTERNO = Convert.ToInt32(semester);
                    //string Amt = PaperFees.ToString();
                    string Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + PaperFees.ToString();


                    objSR.SEMESTERNOS = semester;
                    retStatus = objEC.AddStudentExamRegistrationDetails_Summer(objSR, Amt, ViewState["OrderId"].ToString());
                }

                //int retStatus = objEC.AddStudentExamRegistrationDetails_Summer(objSR, Amt, ViewState["OrderId"].ToString());
                objCommon.DisplayMessage("Demand Created Successfull", this.Page);
                if (retStatus == -99)
                {
                    objCommon.ShowError(Page, "ExamRegistration_Summert.aspx.btnPay_Click() --> ");
                    return;
                    //objCommon.DisplayMessage("Something Went Wrong", this.Page);
                    //return;
                }
                #endregion


                #region For Update Student_Result_Table Column txt_ind=1
                if (lvFailCourse.Items.Count > 0)
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                        //if (chk.Checked == true)
                        //{
                        //    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                        int flag;


                        if (chk.Checked == true)
                        {
                            flag = 1;

                        }
                        else
                        {
                            flag = 0;

                        }
                        int Idno = Convert.ToInt32(Session["idno"]);
                        int ccode = Convert.ToInt32(lblCCode.ToolTip);
                        if (Idno > 0)
                        {
                            string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_COURSE_APPLY_SUMMERTERM";
                            //string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_OUT";
                            string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
                            // string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + ",0";
                            string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + flag + "," + ",0";
                            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

                            if (que_out == "1")
                            {
                                objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Done Sucessfully.", this.Page);

                                HideClm();
                            }
                            else
                            {
                                objCommon.DisplayMessage(updatepnl, "Provisional Exam Registration Update Sucessfully.", this.Page);
                                HideClm();
                            }

                            bindcourses();

                        }

                        //}
                    }
                }
                #endregion

                
              

            
          

           
            #region commented code
            //StudentRegistration objSRegist = new StudentRegistration();
            //StudentRegist objSR = new StudentRegist(); 
            //StudentController objSC1 = new StudentController();
            //int idno = 0;
            //idno = Convert.ToInt32(Session["idno"]);
            //string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
            //objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
            //objSR.IDNO = idno;
            //objSR.REGNO = Regno;
            //objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
            //objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
            //objSR.COLLEGE_CODE = Session["colcode"].ToString();
            //objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            //string coursenos = string.Empty;

            //if (lvFailCourse.Items.Count > 0)
            //{
            //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //    {
            //        int flag;
            //        CheckBox chk = dataitem.FindControl("chkaccept") as CheckBox;
            //        Label lblccode = dataitem.FindControl("lblccode") as Label;
            //        if (chk.Checked == true)
            //        {
            //            flag = 1;
            //        }
            //        else
            //        {
            //            flag = 0;
            //        }


            //        int ccode = Convert.ToInt32(lblccode.ToolTip);
            //        if (idno > 0)
            //        {
            //            string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION";
            //            string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
            //            string Call_Values = "" + idno + "," + Convert.ToInt32(ViewState["sessionnonew"]) + "," + ccode + "," + flag + ",0";
            //            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            //            if (que_out == "1")
            //            {
            //                //  objCommon.DisplayMessage("Course Registration done Sucessfully, Wait for the final approval from the Head of Department", this.Page);
            //            }
            //            else
            //            {
            //                //objCommon.DisplayMessage("Course Registration Update Sucessfully, Wait for the final approval from the Head of Department", this.Page);
            //            }
            //        }


            //    }
            //}



            //foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            //{
            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {

            //        Label courseno = dataitem.FindControl("lblCCode") as Label;
            //        coursenos += courseno.ToolTip + ",";
            //    }

            //}
            //objSR.COURSENOS = coursenos;
            //objSR.SEMESTERNOS = lblSemester.ToolTip;
            //int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            //int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            //objSA.DegreeNo = degreenos;
            //objSA.BranchNo = branchnos;
            //objSA.IpAddress = ViewState["ipAddress"].ToString();
            //CreateStudentPayOrderId();
            ////CREATE DEMAND              
            //ExamController objEC = new ExamController();
            //// string Amt = FinalTotal.Text;
            ////if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"] == null)
            ////{
            ////    ViewState["TotalSubFee"] = "0";
            ////}
            ////if (ViewState["latefee"] == string.Empty || ViewState["latefee"] == null)
            ////{
            ////    ViewState["latefee"] = "0";
            ////}
            ////if (FinalTotal.Text == string.Empty || FinalTotal.Text == null)
            ////{
            ////    FinalTotal.Text = "0";
            ////}
            ////string Amt = ViewState["TotalSubFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;

            ////
            //if (ViewState["CheckProcFee"] == string.Empty || ViewState["CheckProcFee"] == null)
            //{
            //    ViewState["CheckProcFee"] = "0";
            //}
            //if (ViewState["CrettificateFee"] == string.Empty || ViewState["CrettificateFee"] == null)
            //{
            //    ViewState["CrettificateFee"] = "0";
            //}
            //// = ProFess;//FESS HEAD F2
            //// ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3

            //if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"] == null)
            //{
            //    ViewState["TotalSubFee"] = "0";
            //}
            //if (ViewState["latefee"] == string.Empty || ViewState["latefee"] == null)
            //{
            //    ViewState["latefee"] = "0";
            //}
            //if (FinalTotal.Text == string.Empty || FinalTotal.Text == null)
            //{
            //    FinalTotal.Text = "0";
            //}
            ////string Amt = ViewState["TotalSubFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;//commented by gaurav 21_08_2023
            //string Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;

            ////

            //string totalamt = FinalTotal.Text;
            //int retStatus = objEC.AddStudentExamRegistrationDetails(objSR, Amt, ViewState["OrderId"].ToString());
            //if (retStatus == -99)
            //{

            //    objCommon.ShowError(Page, "ExamRegistration_Summer.btnSubmit_WithDemand_Click() --> ");
            //    return;
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updatepnl, "Exam Registration Demand Create Successfully!", this.Page);
            //    bindcourses();
            //    return;
            //}

            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamRegistration_Summer.btnSubmit_WithDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    //protected void InsertBacklopgCourse()
    //{
    //    int session = 0;
    //    int schemeno = 0;
    //    String semesternos = string.Empty;
    //    String coursenos = string.Empty;
    //    String Ipaddress = string.Empty;
    //    int idno = 0;
    //    String regno = string.Empty;
    //    String rollno = string.Empty; // remaining
    //    int ua_no = 0;
    //    String collegecode = string.Empty;

    //    int count = 0;
        

    //    //String semester = string.Empty; 
        
       
        
        
       

    //    session = Convert.ToInt32(ViewState["sessionnonew"]);
    //    schemeno = Convert.ToInt32(lblScheme.ToolTip);
    //    idno = Convert.ToInt32(ViewState["IDNO"]);
    //    Ipaddress = Session["ipAddress"].ToString();
    //    regno = ViewState["regno"].ToString();
    //    rollno = regno;

    //    ua_no=Convert.ToInt32(Session["userno"]);
    //    collegecode = Session["colcode"].ToString();

    //    //semester = lblSemester.ToolTip;

      
    //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
    //    {

    //        CheckBox chk = dataitem.FindControl("chkaccept") as CheckBox;
    //        Label lblccode =dataitem.FindControl("lblccode") as Label;
    //        Label lblsemester = dataitem.FindControl("lblsemester") as Label;
    //        if (chk.Checked == true)
    //        {
    //            coursenos += ((dataitem.FindControl("lblccode")) as Label).ToolTip + ",";
    //            semesternos += ((dataitem.FindControl("lblsemester")) as Label).Text + ",";
    //            count++;
                
    //        }
    //        if (count == 0)
    //        {
    //            objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
    //            bindcourses();
    //            return;;
    //        }
            
    //    }


    //    //StudentRegist objSR = new StudentRegist();
    //    StudentController sc = new StudentController();

    //    //objSR.SESSIONNO = session;
    //    //objSR.SCHEMENO = schemeno;
    //    //objSR.SEMESTERNO = Convert.ToInt32(semesternos);
    //    //objSR.COURSENNO = coursenos;
    //    //objSR.IPADDRESS = Ipaddress;
    //    //objSR.IDNO = idno;
    //    //objSR.REGNO = regno;
    //    //objSR.ROLL_NO = regno;
    //    //objSR.UA_NO = ua_no;
    //    //objSR.COLLEGE_CODE = collegecode;
        
    //    int check = sc.AddExamRegisteredBacklaog_Summer( session, schemeno,semesternos.Remove(semesternos.Length-1,1),coursenos.Remove(coursenos.Length-1,1),Ipaddress,idno,regno,rollno,ua_no,collegecode);
    //    if (check == 1)
    //    {
    //        //objCommon.DisplayMessage( "Student Details submitted sucessfully!!", this.Page);
          
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage( "Failed To submit Student Details !!", this.Page);
    //        return;
    //    }

    //}

    protected void btnhideshow()
    {
        try
        {
          lvFailCourse.Enabled = true;
        //if (Session["usertype"].ToString() == "1")
        //{
        //    btnPrintRegSlip.Visible = true;
        //    btnPay.Visible = true;
        //    btnPay.Enabled = true;
        //    btnSubmit_WithDemand.Visible = true;
        //    btnSubmit_WithDemand.Enabled = true;
        //    btnSubmit.Visible = true;
        //    btnSubmit.Enabled = true;
        //}
        //else
        //{
            if (Session["PaymentMode"].ToString() == "1")// online only 
            {

                btnPrintRegSlip.Visible = false;
                btnPay.Visible = true;

                btnPay.Enabled = true;
                btnSubmit_WithDemand.Visible = false;
                btnSubmit_WithDemand.Enabled = false;
                btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
            }
            else if (Session["PaymentMode"].ToString() == "2")//offline With demand 
            {

                btnPrintRegSlip.Visible = true;
                btnPay.Visible = false;
                btnPay.Enabled = false;
                btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
                btnSubmit_WithDemand.Visible = true;
                btnSubmit_WithDemand.Enabled = true;
            }
            else if (Session["PaymentMode"].ToString() == "3")//Online Offline Both
            {

                btnPrintRegSlip.Visible = true;
                btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
                btnSubmit_WithDemand.Visible = true;
                btnSubmit_WithDemand.Enabled = true;
                btnPay.Visible = true;
                btnPay.Enabled = true;
            }
            else
            {


                btnPrintRegSlip.Visible = true;
                btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
                btnSubmit_WithDemand.Visible = false;
                btnSubmit_WithDemand.Enabled = false;
                btnPay.Visible = true;
                btnPay.Enabled = true;


            }
        }
        catch(Exception ex)
        {
          if (Convert.ToBoolean(Session["error"]) == true)
                  objCommon.ShowError(Page, "ExamRegistration_Summer.btnSearch_Click() --> " + ex.Message + " " + ex.StackTrace);
              else
                  objCommon.ShowError(Page, "Server Unavailable.");
        }
       
       
       
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

        bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckProcFee == true)
        {
            ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        else
        {
            ProFess = 0;
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        #endregion
        #region Certificate Fee Applicable

        bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckCrettificateFee == true)
        {

            CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3

        }
        else
        {
            CrettificateFee = 0;
            ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
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
        bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (Latefee == true)
        {

            latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


            DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
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
            else if (type == 2)
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


        ApplFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "sum(ISNULL(FEE,0))", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO IN (" + oldsem + ") AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(CANCEL,0)=0 and FEESTRUCTURE_TYPE=4 AND    ISNULL(IsFeesApplicable,0)=1 and  COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

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

        //  ViewState["TotalSubFee"] = totalsubfee;//Commented by Gaurav 21_08_2023
        ViewState["TotalSubFee"] = lblTotalExamFee.Text;

        ViewState["latefee"] = calculatelatefee;
        FinalTotal.Text = (ProFess + ApplFess + CrettificateFee + calculatelatefee).ToString();
    }

    #region Commented Code
    //protected void Calculate_All_HEADS()
    //{

    //    lblTotalExamFee.Text = "0.00";
    //    lblfessapplicable.Text = "0.00";
    //    decimal ProFess;
    //    decimal ApplFess;
    //    decimal CrettificateFee;
    //    decimal latefees;
    //    decimal FeeHead4FEE; decimal FeeHead5FEE; decimal FeeHead6FEE; decimal FeeHead7FEE; decimal FeeHead8FEE;
    //    decimal FeeHead9FEE; decimal FeeHead10FEE;

    //    #region ChkProcessing Fee Head 2

    //    bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (CheckProcFee == true)
    //    {
    //        ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
    //        lblfessapplicable.Text = ProFess.ToString();
    //    }
    //    else
    //    {

    //        ProFess = 0;
    //        ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2+
    //        lvitemH2.Visible = false;
    //    }
    //    #endregion
    //    #region Certificate Fee Applicable  HEAD 3
    //    bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (CheckCrettificateFee == true)
    //    {

    //        CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
    //        lblCertificateFee.Text = CrettificateFee.ToString();
    //    }
    //    else
    //    {
    //        CrettificateFee = 0;
    //        ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
    //        lvitemH3.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 4

    //    bool FeeHead4 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H4,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead4 == true)
    //    {
    //        FeeHead4FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H4_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead4FEE"] = FeeHead4FEE;//FESS HEAD F2
    //        lblhead4amount.Text = FeeHead4FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead4FEE = 0;
    //        ViewState["FeeHead4FEE"] = FeeHead4FEE;//FESS HEAD F2+
    //        lvitemH4.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 5

    //    bool FeeHead5 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H5,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead5 == true)
    //    {
    //        FeeHead5FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H5_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead5FEE"] = FeeHead5FEE;//FESS HEAD F2
    //        lblhead5amount.Text = FeeHead5FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead5FEE = 0;
    //        ViewState["FeeHead5FEE"] = FeeHead5FEE;//FESS HEAD F2+
    //        lvitemH5.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 6

    //    bool FeeHead6 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H6,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead6 == true)
    //    {
    //        FeeHead6FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H6_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead6FEE"] = FeeHead6FEE;//FESS HEAD F2
    //        lblhead6amount.Text = FeeHead6FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead6FEE = 0;
    //        ViewState["FeeHead6FEE"] = FeeHead6FEE;//FESS HEAD F2+
    //        lvitemH6.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 7

    //    bool FeeHead7 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H7,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead7 == true)
    //    {
    //        FeeHead7FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H7_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead7FEE"] = FeeHead7FEE;//FESS HEAD F2
    //        lblhead7amount.Text = FeeHead7FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead7FEE = 0;
    //        ViewState["FeeHead7FEE"] = FeeHead7FEE;//FESS HEAD F2+
    //        lvitemH7.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 8

    //    bool FeeHead8 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H8,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead8 == true)
    //    {
    //        FeeHead8FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H8_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead8FEE"] = FeeHead8FEE;//FESS HEAD F2
    //        lblhead8amount.Text = FeeHead8FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead8FEE = 0;
    //        ViewState["FeeHead8FEE"] = FeeHead8FEE;//FESS HEAD F2+
    //        lvitemH8.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 9

    //    bool FeeHead9 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H9,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead9 == true)
    //    {
    //        FeeHead9FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H9_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead9FEE"] = FeeHead9FEE;//FESS HEAD F2
    //        lblhead9amount.Text = FeeHead9FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead9FEE = 0;
    //        ViewState["FeeHead9FEE"] = FeeHead9FEE;//FESS HEAD F2+
    //        lvitemH9.Visible = false;
    //    }
    //    #endregion
    //    #region Fee Head 10

    //    bool FeeHead10 = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H10,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (FeeHead10 == true)
    //    {
    //        FeeHead10FEE = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(H10_FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //        ViewState["FeeHead10FEE"] = FeeHead10FEE;//FESS HEAD F2
    //        lblhead10amount.Text = FeeHead10FEE.ToString();
    //    }
    //    else
    //    {

    //        FeeHead10FEE = 0;
    //        ViewState["FeeHead10FEE"] = FeeHead10FEE;//FESS HEAD F2+
    //        lvitemH10.Visible = false;
    //    }
    //    #endregion
    //    #region LATE FEE HEAD
    //    DataSet dsStudent = null;
    //    string date = string.Empty;
    //    string strNewDate = string.Empty;
    //    string format = "dd/mm/yyyy";
    //    int type = 0;
    //    string dateString = string.Empty;
    //    decimal calculatelatefee = 0;
    //    string totalsubfee = string.Empty;
    //    bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    if (Latefee == true)
    //    {

    //        latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


    //        DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=5 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
    //            dateString = date.Substring(0, 11);
    //            DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
    //            strNewDate = dateTime.ToString("yyyy-mm-dd");
    //            type = Convert.ToInt32(ds.Tables[0].Rows[0]["TYPE"]);
    //        }
    //        string SP_Name = "ACD_CALCULATE_DAY_WEEK_MONTHLY";
    //        string SP_Parameters = "@P_DATE, @P_TYPE";
    //        string Call_Values = "" + strNewDate + "," + Convert.ToInt32(type) + "";// +"," + Convert.ToInt16(ViewState["sem"]) + "," + 
    //        dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);


    //        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"]) > 0)
    //        {
    //            calculatelatefee = latefees * Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

    //        }
    //        else
    //        {
    //            calculatelatefee = 0;
    //        }
    //        // decimal abc = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);
    //        //calculatelatefee = latefees * abc;
    //        if (type == 1)
    //        {
    //            lblLateFee.Text = "<b style='color:red;'>PERDAY </b> : " + dateString + "  " + latefees.ToString();
    //        }
    //        else if (type == 1)
    //        {
    //            lblLateFee.Text = "<b style='color:red;'>WEEKLY </b> : " + dateString + "  " + latefees.ToString();
    //            // lblLateFee.Text = "WEEKLY   " + latefees.ToString();
    //        }
    //        else if (type == 3)
    //        {
    //            lblLateFee.Text = "<b style='color:red;'>MONTHLY </b> : " + dateString + "  " + latefees.ToString();

    //        }



    //    }
    //    else
    //    {
    //        latefees = 0;
    //        lvLatefeesHead.Visible = false;
    //    }
    //    #endregion
    //    int all_head_total = Convert.ToInt32(ViewState["FeeHead10FEE"]) + Convert.ToInt32(ViewState["FeeHead9FEE"]) + Convert.ToInt32(ViewState["FeeHead8FEE"]) + Convert.ToInt32(ViewState["FeeHead7FEE"]) + Convert.ToInt32(ViewState["FeeHead6FEE"]) + Convert.ToInt32(ViewState["FeeHead5FEE"]) + Convert.ToInt32(ViewState["FeeHead4FEE"]) + Convert.ToInt32(ViewState["CrettificateFee"]) + Convert.ToInt32(ViewState["CheckProcFee"]);

    //    ViewState["all_head_total"] = all_head_total;

    //}
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      
      try {
          int cid = 0;
          int idno = 0;
        
          idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtEnrollno.Text.Trim() + "'  UNION ALL SELECT 0 AS IDNO"));
          if (idno == 0 || idno == null)
          {
              objCommon.DisplayMessage(updatepnl, "Please Search Correct Registration Numbar...!", this.Page);
              divCourses.Visible = false;

          
          }
          Session["idno"] =  Convert.ToInt32(idno);
          cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));   


          if (CheckActivityCollege(cid))
          {

              lblfessapplicable.Text = string.Empty;
              lblCertificateFee.Text = string.Empty;
              lblTotalExamFee.Text = "";
              lblLateFee.Text = "";
              FinalTotal.Text = "";
              this.ShowDetails();  
              bindcourses();
              divbtn.Visible = true;
              //btnhideshow();

              //btnSubmit.Visible = true;
              //btnSubmit.Enabled = true;
              //btnPrintRegSlip.Visible = true;
              //btnSubmit_WithDemand.Visible = true;
              //btnSubmit_WithDemand.Enabled = true;
              //btnPay.Visible = true;
             
            
          }

          else
          {
            


          }

    }
        catch(Exception ex)
      {
          if (Convert.ToBoolean(Session["error"]) == true)
              objCommon.ShowError(Page, "ExamRegistration_Summer.btnSearch_Click() --> " + ex.Message + " " + ex.StackTrace);
          else
              objCommon.ShowError(Page, "Server Unavailable.");
      }
    


}

    #region Not use
    protected void btnSearch_Click1(object sender, EventArgs e)
    {
       
    }
    #endregion

    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        txtEnrollno.Text = string.Empty;
        divCourses.Visible = false;
        lvFailCourse.Visible = false;
        divbtn.Visible = false;
    }
}
