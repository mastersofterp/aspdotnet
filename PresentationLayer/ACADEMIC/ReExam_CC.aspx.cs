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
using IITMS.SQLServer.SQLDAL;

public partial class Academic_ReExam_CC : System.Web.UI.Page
{


    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    Student_Acd objSA = new Student_Acd();
    StudentFees objStudentFees = new StudentFees();
    StudentRegist objSR = new StudentRegist();
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
                int cid = 0;
                int idno = 0;
                idno = Convert.ToInt32(Session["idno"]);
                cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));
                if (CheckActivityCollege(cid))
                {
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        divCourses.Visible = true;
                        pnlSearch.Visible = false;
                        this.ShowDetails();
                        bindcourses();//for retest 
                        // added for KOTA 09_06_2023 START
                        btnPay.Visible = false;
                        btnPay.Enabled = false;
                        btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;
                        btnPrintRegSlip.Visible = true;
                        btnPrintRegSlip.Enabled = true;
                        // added for KOTA 09_06_2023 END
                        //Check for PAYMENT OR NOT 

                      

                        Disible_listview();
                        #region FOR ATLAS PAY BUTTON
                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND D.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));

                        if (paysuccess > 0)
                        {
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            btnSubmit.Enabled = false;
                            btnPay.Enabled = false;
                            btnPrintRegSlip.Visible = true;

                            decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));


                            lblfessapplicable.Text = "";
                            lblTotalExamFee.Text = "";
                            FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();





                        }

                        #endregion
                    }
                }

                else
                {
                    objCommon.DisplayMessage("Something Went Wrong !!", this.Page);
                    return;
                }

                ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];

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
    
    private void FillDropdown()
    {



        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + ViewState["ACTIVITY_NO"], "");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
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
    //get the new receipt No.
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
                        if (ViewState["semesternos"].ToString().Contains(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()))
                        {

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
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
          

            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'"));
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + Convert.ToInt32(Session["idno"]) + "'"));          
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"]) + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO IN (" + Session["Semester"].ToString()+")"; 
            url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"]) + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + 0;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
       
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT(SCHEMENO)", "IDNO = " + idno + " AND SEMESTERNO = " + lblSemester.ToolTip.ToString()));
        int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));

    }
    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;
        string semester = string.Empty;
        #region ADDED Conditions for check multiple Activity
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS =" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + " AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        ViewState["sessionnonew"] = sessionno;
        Session["sessionnonew"] = sessionno;
        string sessionnoname = objCommon.LookUp("ACD_SESSION_MASTER", "TOP (1)SESSION_NAME", "SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"]));
        lblsessionname.Text = sessionnoname;
        lblsessionname.ToolTip = sessionno;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);

            ret = false;
        }
        dtr.Close();
        return ret;
    }
    protected void bindcourses()
    {
        int idno = 0;
        int sessionno = Convert.ToInt32(Session["sessionnonew"]);

        //StudentController objSC = new StudentController();
        ExamController objEC = new ExamController();
        DataSet dsFailSubjects;
        idno = Convert.ToInt32(Session["idno"]);

        if (idno == null || idno == 0)
        {
            objCommon.DisplayMessage("No Record Found...", this.Page);

        }
        else
        {
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            ViewState["degreeno"] = degreeno;
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            ViewState["branchno"] = branchno;
                      
          //  dsFailSubjects = objEC.GetStudentFailExamSubList(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno, Session["Semester"].ToString());
            dsFailSubjects = objEC.GetStudentFailExamSubList(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno, 0);
            //need to remove semester static
            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                objCommon.DisplayMessage(updatepnl,"No Record Found...", this.Page);

            }
            else
            {
                int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
                int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
                int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SCHEMENO", "IDNO=" + idno));
                ViewState["clg_id"] = clg_id;
             
                #region Comment by gaurav for 
                //  dsSubjects = objEC.GetStudentFailList_ReExam(idno, sessionno, Convert.ToInt32(lblScheme.ToolTip), degreeno, branchno, 0);

              

                #endregion

                string SP_Name = "PKG_EXAM_GET_SUBJECTS_LIST_FOR_REEXAM";
                string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_DEGREENO, @P_BRANCHNO, @P_SCHEMENO,@P_SEMESTERNO";
                // string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + cntcourse + ",1";
                string Call_Values = idno + "," + sessionno + "," + degreeno + "," + branchno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + 0 ;

                // return;

              //  string que_out = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, true);

                dsSubjects = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);



                if (dsSubjects.Tables[0].Rows.Count > 0)
                {
                    lvFailCourse.DataSource = dsSubjects;
                    lvFailCourse.DataBind();
                    lvFailCourse.Visible = true;
                    divCourses.Visible = true;
                    btnSubmit.Visible = true;
                    pnlFailCourse.Visible = true;
                    Session["semesterNO"]=dsSubjects.Tables[0].Rows[0]["SEMESTER"];

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
                    btnPrintRegSlip.Enabled=false;
                    divbtn.Visible = false;
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

                                sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

                                if (sum <= 0)
                                {
                                    objCommon.DisplayMessage(updatepnl, "Dear Student," + "\\r\\n" + "Please note your attendance is low. You are not eligible to register for the final exam till such a time that you meet your advisor or admin and submit the explanation for low attendance.", this.Page);
                                }                                

                            }

                        }
                        else
                        {
                            sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

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
                string CHECKFEESTYPE = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "FEESTRUCTURE_TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0");

                int checkpaymentmode = Convert.ToInt16(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(PAYMENT_MODE,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0  UNION ALL SELECT 0 AS PAYMENT_MODE"));

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
                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (paysuccess > 0)
                        {
                            
                            //decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "SUM( AD.TOTAL_AMT)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                            decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO = AD.IDNO AND D.SESSIONNO = AD.SESSIONNO  AND AD.RECIEPT_CODE=D.RECIEPT_CODE)", "SUM( AD.TOTAL_AMT)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                            if (ViewState["usertype"].ToString() == "2")
                            {
                                btnPrintRegSlip.Visible = true;
                                btnSubmit.Visible = false;
                                btnSubmit_WithDemand.Visible = false;
                                btnSubmit_WithDemand.Enabled = false;
                                btnPay.Visible = false;
                                lblLateFee.Text = "";
                                lblfessapplicable.Text = "";
                                lblCertificateFee.Text = "";
                                lblTotalExamFee.Text = "";
                                FinalTotal.Text = "";
                                PaidTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                                lvFailCourse.Enabled = false;
                            }
                            else 
                            {
                                lblLateFee.Text = "";
                                lblfessapplicable.Text = "";
                                lblCertificateFee.Text = "";
                                lblTotalExamFee.Text = "";
                                FinalTotal.Text = "";
                                PaidTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                                //lvFailCourse.Enabled = false;

                                int UnChk = 0;
                                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                                {
                                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                                    if (chk.Enabled == true)
                                    {
                                        UnChk++;
                                    }
                                }
                                if (UnChk >= 1)
                                {
                                    btnhideshow();
                                    btnPrintRegSlip.Visible = true;
                                }
                                else
                                {
                                    btnPrintRegSlip.Visible = true;
                                    btnSubmit.Visible = false;
                                    btnSubmit.Enabled = false;
                                    btnPay.Visible = false;
                                    btnPay.Enabled = false;
                                    btnSubmit_WithDemand.Enabled = false;
                                    btnSubmit_WithDemand.Visible = false;
                                }
                            }
                          
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
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2 AND FEESTRUCTURE_TYPE=5 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

                int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2    and ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
                
                    if (CheckExamfeesApplicableOrNot >= 1)
                    {
                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                           if (paysuccess > 0)
                           {
                               //decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                               decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO = AD.IDNO AND D.SESSIONNO = AD.SESSIONNO  AND AD.RECIEPT_CODE=D.RECIEPT_CODE)", "SUM( AD.TOTAL_AMT)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                               if (Convert.ToInt32(ViewState["usertype"]) == 2)
                               {
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
                                   FinalTotal.Text = "";
                                   PaidTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                                   lvFailCourse.Enabled = false;
                               }
                               else 
                               {
                                   CalculateTotalCredit();
                                   btnPrintRegSlip.Visible = true;
                                   //btnSubmit.Visible = true;
                                   btnPay.Visible = false;
                                   //btnSubmit_WithDemand.Visible = false;
                                   //btnSubmit_WithDemand.Enabled = false;
                                   lblLateFee.Text = "";
                                   lblfessapplicable.Text = "";
                                   lblCertificateFee.Text = "";
                                   lblTotalExamFee.Text = "";
                                   FinalTotal.Text = "";
                                   PaidTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                                   //lvFailCourse.Enabled = true;

                                   int UnChk = 0;
                                   foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                                   {
                                       CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                                       if (chk.Enabled == true)
                                       {
                                           UnChk++;
                                       }
                                   }
                                   if (UnChk >= 1)
                                   {
                                       btnhideshow();
                                       btnPrintRegSlip.Visible = true;
                                   }
                                   else
                                   {
                                       btnPrintRegSlip.Visible = true;
                                       btnSubmit.Visible = false;
                                       btnSubmit.Enabled = false;
                                       btnPay.Visible = false;
                                       btnPay.Enabled = false;
                                       btnSubmit_WithDemand.Enabled = false;
                                       btnSubmit_WithDemand.Visible = false;
                                   }
                               
                               }
                               
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
                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Session["semesterNO"].ToString() + "   AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                    //int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                       
                    
                    if (paysuccess > 0)
                    {
                        //decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 D.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND D.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        //decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "SUM(AD.TOTAL_AMT)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                       // decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO = AD.IDNO AND D.SESSIONNO = AD.SESSIONNO  AND AD.RECIEPT_CODE=D.RECIEPT_CODE)", "SUM( AD.TOTAL_AMT)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO = AD.IDNO AND D.SESSIONNO = AD.SESSIONNO  AND AD.RECIEPT_CODE=D.RECIEPT_CODE)", "SUM( AD.TOTAL_AMT)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + "  AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                                
                        //decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if(Convert.ToInt32(ViewState["usertype"])==2)
                        {
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
                        FinalTotal.Text = "";
                        PaidTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                        lvFailCourse.Enabled = false;
                        }
                        else
                        {
                                  CalculateTotalCredit();
                                   btnPrintRegSlip.Visible = true;
                                   //btnSubmit.Visible = true;
                                   btnPay.Visible = false;
                                   //btnSubmit_WithDemand.Visible = false;
                                   //btnSubmit_WithDemand.Enabled = false;
                                   lblLateFee.Text = "";
                                   lblfessapplicable.Text = "";
                                   lblCertificateFee.Text = "";
                                   lblTotalExamFee.Text = "";
                                   FinalTotal.Text = "";
                                   PaidTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                                   //lvFailCourse.Enabled = true;

                                   int UnChk = 0;
                                   foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                                   {
                                       CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                                       if (chk.Enabled == true)
                                       {
                                           UnChk++;
                                       }
                                   }
                                   if (UnChk >= 1)
                                   {
                                       btnhideshow();
                                       btnPrintRegSlip.Visible = true;
                                   }
                                   else
                                   {
                                       btnPrintRegSlip.Visible = true;
                                       btnSubmit.Visible = false;
                                       btnSubmit.Enabled = false;
                                       btnPay.Visible = false;
                                       btnPay.Enabled = false;
                                       btnSubmit_WithDemand.Enabled = false;
                                       btnSubmit_WithDemand.Visible = false;
                                   }
                               
                        }
                       

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
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"].ToString() + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

                    if (CheckExamfeesApplicableOrNot >= 1)
                    {
                        CalculateTotal();//ALL Course Fees

                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (Convert.ToInt32(ViewState["usertype"]) == 1)
                        {
                            if (paysuccess > 0)
                            {
                                btnPrintRegSlip.Visible = true;
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                btnPrintRegSlip.Visible = false;
                                btnSubmit.Visible = true;
                           
                            }
                    }
                 else{
                     
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                       // objCommon.DisplayMessage("RESIT EXAM COURSES APPLY SUCESSFULLY...", this.Page);
                         btnSubmit.Enabled = false;
                        btnSubmit.Visible = false;
                        btnPay.Visible = false;
                        btnPay.Enabled = false;
                        btnSubmitnopayment.Enabled = false;
                        objCommon.DisplayMessage(updatepnl, "Exam Fees Configuration is not complete. Please contact the Admin", this.Page);
                    
                    
                    }

            }
            else
            {

                objCommon.DisplayMessage("No Courses found...!! !!", this.Page);
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                divCourses.Visible = true;
                btnSubmit.Visible = false;
                btnPrintRegSlip.Visible = false;
                btnSubmit.Visible = false;
                btnPay.Visible = false;                
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
           StudentController objSC = new StudentController();
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
            objSR.COURSENOS = string.Empty;
            objSR.SEMESTERNOS = string.Empty;
            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int cntcourse = 0;         
            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;           
            objSA.IpAddress = ViewState["ipAddress"].ToString();           

            int A = lvFailCourse.Items.Count;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PER"]) < 50)
                    {
                        sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

            }
            if (cntcourse == 0)
            {
                objCommon.DisplayMessage(updatepnl,"Please Select Courses..!!", this.Page);
              //  ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);

                return;
            }
            else
            {
                int ifPaidAlready = 0;
                ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + lblSemester.ToolTip));
                if (ifPaidAlready > 0)
                {
                    objCommon.DisplayMessage(updatepnl,"Re - Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                    return;
                }
            #endregion                         
          #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG               
                    if (lvFailCourse.Items.Count > 0)
                    {
                        int count = 0;
                        foreach (ListViewDataItem item in lvFailCourse.Items)
                        {
                            int Courseapply = 0;
                            CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                            Label lblCCode = item.FindControl("lblCCode") as Label;
                            Label fees = item.FindControl("lblAmt") as Label;
                            Label Sem = item.FindControl("lblsem") as Label;
                            HiddenField ExistingMark = item.FindControl("hdfExistingMark") as HiddenField;
                            //if (CheckId.Checked == true && CheckId.Enabled==true)
                            //if (CheckId.Checked == true )
                            //{
                              // Session["resitsem"] = Sem.ToolTip;

                                int Idno = Convert.ToInt32(Session["idno"]);
                                count++;
                                if (CheckId.Checked == true)
                                {
                                    Courseapply = 1;
                                }
                                else
                                {
                                    Courseapply = 0;
                                }          
                                #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
                                //if (CheckId.Checked == true)
                                //{
                                    if (Idno > 0)
                                    {
                                        string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW_ATLAS";
                                        string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";
                                        //string Call_Values = "" + Idno + "," + Session["sessionnonew"] + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(lblSemester.ToolTip) + "," + Convert.ToInt32(0) + "," + 0 + "," +0 + "," + 0 + ",'" + 0 + "'," + 0 + "," + 1 + "," + fees.ToolTip + ",1";
                                        string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + Courseapply + ",1";

                                        // return;

                                        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                                        if (que_out == "0")
                                        {
                                            //objCommon.DisplayMessage(updatepnl,"Course Apply Sucessfully", this.Page);

                                        }
                                        else
                                        {
                                            //objCommon.DisplayMessage(updatepnl,"Course Update Sucessfully", this.Page);                                            
                                        }

                                    }
                         
                                #endregion

                           // }
                        }
                        if (count == 0)
                        {
                            objCommon.DisplayMessage(updatepnl,"Please Select Atleast one Student from the list", this.Page);
                            return;
                        }
                       }
                     
                   // }
                 //   return;
                #endregion                
          #region CREATE DEMAND
                    string coursenos = string.Empty;
                    string semesterno = string.Empty;
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                        {

                            Label courseno = dataitem.FindControl("lblCCode") as Label;
                             Label semesternos = dataitem.FindControl("lblsem") as Label;
                            coursenos += courseno.ToolTip + ",";
                            semesterno = semesternos.ToolTip;
                        }

                    }
                    coursenos = coursenos.TrimEnd(',');
                    //StudentController objSC1 = new StudentController();
                    DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
                    string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
                    objSR.COURSENOS = coursenos;
                    objSR.IDNO = Convert.ToInt32(Session["idno"]);
                    objSR.REGNO = RegNo;
                    objSR.SCHEMENO = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString());
                    //objSR.SEMESTERNOS = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    objSR.SEMESTERNOS = semesterno;
                    objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    string Amt = FinalTotal.Text;
                    CreateStudentPayOrderId();
                    //create Demand
                   // StudentController objSC = new StudentController();
                    ExamController objEC = new ExamController();
                    int retStatus = objEC.AddStudentResitExamRegistrationDetails(objSR, Amt, ViewState["OrderId"].ToString());
                    if (retStatus == -99)
                    {
                        // retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        objCommon.DisplayMessage(updatepnl,"Something Went Worong", this.Page);
                        return;
                    }
                    else
                    {
                      
                    }
            

                }
           // return;
                    #endregion
    
         #region RAZOR


            int PAYID = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%Razor%'"));
                Session["PAYID"] = PAYID;

                int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVESTATUS=1 AND ACTIVITYNAME like '%Re Major%'"));

            //Session["payactivityno"] = "2";
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            Session["payactivityno"] = payactivityno; 
            // OrganizationId = Convert.ToInt32(Session["OrgId"]);
            int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
            int DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"])));
            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = FinalTotal.Text;
            ViewState["studAmt"] = FinalTotal.Text;//hdnTotalCashAmt.Value;
            // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            DataSet dsStudent2 = objSC.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));         
            //string Amount = "1.00";
            Session["studName"] = dsStudent2.Tables[0].Rows[0]["STUDNAME"].ToString();
            Session["studPhone"] = dsStudent2.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            Session["studEmail"] = dsStudent2.Tables[0].Rows[0]["EMAILID"].ToString();
            Session["ReceiptType"] = "REF";        
            Session["paysession"] = Convert.ToInt32(Session["sessionnonew"]);
            //Session["paysemester"] = lblSemester.ToolTip;
           // Session["paysemester"] = Session["Semester"].ToString();
            Session["paysemester"] = lblSemester.ToolTip;
            Session["homelink"] = "ReExam_CC.aspx";
            Session["regno"] = dsStudent2.Tables[0].Rows[0]["REGNO"].ToString();
            Session["payStudName"] = dsStudent2.Tables[0].Rows[0]["STUDNAME"].ToString();
            Session["paymobileno"] = dsStudent2.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblBranch.Text;
           
                DEGREENO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
           
            FeeCollectionController objFee = new FeeCollectionController();
            DataSet ds2 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, Convert.ToInt16(Session["PAYID"]), Convert.ToInt32(Session["payactivityno"]), DEGREENO, cid);
            if (ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    sum = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(SUM(LOW_ATTENDANCE_APPROVE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "AND  IDNO =" + Convert.ToInt32(Session["idno"]) + "AND SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + "AND ISNULL(CANCEL,0)=0 AND REGISTERED=1"));

                      if (sum <= 0)
                      {
                          objCommon.DisplayMessage(updatepnl, "Dear Student," + "\\r\\n" + "Please note your attendance is low. You are not eligible to register for the final exam till such a time that you meet your advisor or admin and submit the explanation for low attendance.", this.Page);
                          
                          HideClm();
                          return;
                      }                   

                }
            }
            else
            {
                int flag;
                double fees1 = 0.00;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    Label fees = dataitem.FindControl("lblAmt") as Label;
                    int Idno = Convert.ToInt32(Session["idno"]);
                    int ccode = Convert.ToInt32(lblCCode.ToolTip);
                    HiddenField ExistingMark = dataitem.FindControl("hdfExistingMark") as HiddenField;    
                     if (chk.Checked == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                    
                     if (fees.Text == "")
                     {
                         fees1 = 0.00;
                     }
                     else
                     {
                         fees1 = Convert.ToDouble(fees.Text);
                     }

                     string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_REEXAM";
                     string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";
                     // string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + cntcourse + ",1";
                     string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Session["semesterNO"]) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + fees1 + "," + flag + ",1";

                     // return;

                     string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                       
                        if (Idno > 0)
                        {
                            string SP_Name1 = "PKG_ACD_INSERT_EXAMREGISTRATION_MIDTERM_FREE";
                            string SP_Parameters1 = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
                            string Call_Values1 = "" + Idno + "," + Convert.ToInt32(ViewState["sessionnonew"]) + "," + ccode + "," + flag + ",0";
                            string que_out1 = objCommon.DynamicSPCall_IUD(SP_Name1, SP_Parameters1, Call_Values1, true);                           
                            if (que_out1 == "1")
                            {
                                objCommon.DisplayMessage("Course Registration done Sucessfully", this.Page); 

               
              
                objCommon.DisplayMessage( updatepnl,"Payment configuration is not done for this session.", this.Page);
               // bindcourses();
                return;

                                HideClm();
                            }
                            else
                            {
                                objCommon.DisplayMessage("Course Registration Update Sucessfully", this.Page);
                               
                                HideClm();
                            }
                        }

                    }
                bindcourses();
                }
            

        else
        {

            string semester = ddlBackLogSem.SelectedValue;
            string sem = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  AND SEMESTER LIKE  '%" + semester + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'");

            if (sem == " " || sem == string.Empty)
            {
                objCommon.DisplayMessage(updatepnl,"Activity Not Started For Selected Semester !!", this.Page);
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                btnSubmit.Visible = false;
                btnPrintRegSlip.Visible = false;
                //lblfessapplicable.Visible = false;
                lblTotalExamFee.Text = "0.00";
                FinalTotal.Text = "0.00";
                //CalculateTotal();               
                return;
            }
}
}
#endregion 
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        // ShowReport("BacklogRegistration", "rptOnlineReceiptbBacklog_ATLAS.rpt");
        if (Convert.ToInt32(Session["OrgId"]) == 9)//FOR ATLAS ADDED BY GAURAV 31-3-2023
        {
            ShowReport("RESIT", "rptRESIT_Reg_ATLAS.rpt");
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
        }
        else
        {

            ShowReport("RESIT", "rptRESIT_Reg_CC.rpt");

        }

      


    }
    #region Added for the Payment Calculations as per the checked Courses on 25052022
    decimal Amt = 0;
    decimal CourseAmtt = 0;
    #endregion Added for the Payment Calculations as per the checked Courses on 25052022
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
                      
            #region GET STUDENT DETALS
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();
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
            objSR.COURSENOS = string.Empty;
            objSR.SEMESTERNOS = string.Empty;
            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int cntcourse = 0;         
            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;           
            objSA.IpAddress = ViewState["ipAddress"].ToString();           

            int A = lvFailCourse.Items.Count;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                     Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    if (chk.Checked == true) //if (chk.Enabled == true)
                        cntcourse++;
                }

            }
            if (cntcourse == 0)
            {
                objCommon.DisplayMessage(updatepnl,"Please Select Courses..!!", this.Page);
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);

                return;
            }
           
            #endregion                         

            #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG            
            if (lvFailCourse.Items.Count > 0)
            {
                int count = 0;
                foreach (ListViewDataItem item in lvFailCourse.Items)
                {
                    CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = item.FindControl("lblCCode") as Label;
                    Label fees = item.FindControl("lblAmt") as Label;
                    Label Sem = item.FindControl("lblsem") as Label;
                    HiddenField ExistingMark = item.FindControl("hdfExistingMark") as HiddenField;                   
                    if (CheckId.Checked == true)
                    {
                        int Idno = Convert.ToInt32(Session["idno"]);
                        count++;
                        int Courseapply = 1;
                       // double fee = fees.ToolTip;
                        if (fees.ToolTip ==string.Empty)
                        {
                            fees.ToolTip = "0";
                        }
                       
                        #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
                        if (Idno > 0)
                        {
                            string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW_WITHOUT_PAYMENT_ATLAS";
                            string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";
                            string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + Courseapply + ",1";

                            // return;

                            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                            if (que_out == "0")
                            {
                                //objCommon.DisplayMessage("Courses Update Sucessfully", this.Page);
                               //return;

                                objCommon.DisplayMessage(updatepnl,"ReExam Course Registration done Sucessfully", this.Page);                                         ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                            }
                            else
                            {
                                objCommon.DisplayMessage(updatepnl, "ReExam Course Registration done Sucessfully", this.Page);
                                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                            }
                            bindcourses();
                            //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                        }

                        #endregion

                    }
                }
                if (count == 0)
                {
                    objCommon.DisplayMessage("Please Select Atleast one Student from the list", this.Page);

                    return;
                }
            }
          
            #endregion                



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.btnPay_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
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
            //int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["Semester"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND SUBID>0 AND FEETYPE=2  AND ISNULL(IsProFeesApplicable,0)=1 AND ISNULL(IsFeesApplicable,0)=1"));
            int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " +
Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
            if (CheckExamfeesApplicable >= 1)
            {
                int applycourse = 0;
                applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Convert.ToInt32(Session["sessionnonew"]))));
                //if (applycourse > 0)
                //{
                   

                //}
                //else
                //{
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

                    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                    Amt = 0;
                    CourseAmtt = 0;
               // }
            }
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
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.chkAccept_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
           // int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["Semester"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND SUBID>0 AND FEETYPE=2  AND ISNULL(IsProFeesApplicable,0)=1 AND ISNULL(IsFeesApplicable,0)=1"));
            int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
            if (CheckExamfeesApplicable >= 1)
            {//ListViewDataItem dataitem in lvFailCourse.ItemTemplate)
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
                    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();

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
                        FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                    }

                }

            }
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
                        //lblTotalExamFee.Text = "0.00";
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.chkAll_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    protected void checkboxEnable()
    {
        int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Convert.ToInt32(lblSemester.ToolTip) + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND SUBID>0 and FEETYPE=1"));
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
   
    //protected void CalculateTotal()
    //{
    //    lblTotalExamFee.Text = "0.00";
    //    lblfessapplicable.Text = "0.00";
    //    decimal ProFess;
    //    ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["Semester"].ToString() + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0 "));//and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
    //    //int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Convert.ToInt32(Session["Semester"]) + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND ISNULL(IsFeesApplicable,0)=1 
    //    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
    //    {
    //        if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
    //        {
    //            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
    //            Label lblAmt = dataitem.FindControl("lblAmt") as Label;

    //            if (lblAmt.Text == string.Empty)
    //            {
    //                lblAmt.Text = "0.00";
    //            }
    //            decimal CourseAmt = Convert.ToDecimal(lblAmt.Text.ToString());
    //            if (cbRow.Checked == true)
    //            {
    //                Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
    //            }
    //            string TotalAmt = Amt.ToString();
    //            lblTotalExamFee.Text = TotalAmt.ToString();
    //            lblfessapplicable.Text = ProFess.ToString();

    //            if (lblfessapplicable.Text == string.Empty)
    //            {
    //                lblfessapplicable.Text = "0.00";
    //            }


    //        }
    //    }
    //    FinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
    //}

    protected void CalculateTotal()
    {
        lblTotalExamFee.Text = "0.00";
        lblfessapplicable.Text = "0.00";
        decimal ProFess;
        ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(SUM(APPLICABLEFEE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

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
    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int ifPaidAlready = 0;
        int applycourse = 0;

        ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO IN (" + lblSemester.ToolTip.ToString() + ")"));
        if (ifPaidAlready > 0)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                HiddenField hdf = (HiddenField)e.Item.FindControl("hdfapplycourse");
              CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
              HiddenField hdfapplycoursedone = (HiddenField)e.Item.FindControl("hdfapplycoursedone");
              if (hdf.Value == "1" && hdfapplycoursedone.Value=="1")
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
                  chkhead.Checked = false;
                  chkhead.Enabled=false;

              }
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
                                if (ViewState["usertype"].ToString() == "2")
                                {
                                    if (cbRow.Checked == true)
                                    {
                                        Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                                        count++;
                                    }
                                }
                                else
                                {
                                    if (cbRow.Checked == true && cbRow.Enabled == true)
                                    {
                                        Amt = Convert.ToDecimal(Amt) + Convert.ToDecimal(CourseAmt);
                                        count++;
                                    }

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
                        //ViewState["TotalSubFee"] = Convert.ToDecimal(TotalAmt);
                        //FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"])).ToString();
                        //Amt = 0;
                        //CourseAmtt = 0;

                        if (ViewState["usertype"].ToString() == "2")
                        {
                            //  ViewState["TotalSubFee"] = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(lblpapervalMax.Text)).ToString();
                            ViewState["TotalSubFee"] = Convert.ToDecimal(TotalAmt);
                            FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"])).ToString();
                            Amt = 0;
                            CourseAmtt = 0;
                        }
                        else
                        {
                            int IfDemandCreated = 0;
                            IfDemandCreated = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(DISTINCT 1) _COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(CAN,0)=0 and SEMESTERNO=" + Convert.ToInt32(Session["semesterNO"])));

                            if (IfDemandCreated > 0)
                            {
                                ViewState["TotalSubFee"] = (Convert.ToDecimal(TotalAmt)).ToString();
                                FinalTotal.Text = (Convert.ToDecimal(TotalAmt)).ToString();// + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"]) + valuationfee).ToString();
                                Amt = 0;
                                CourseAmtt = 0;
                            }
                            else
                            {
                                ViewState["TotalSubFee"] = Convert.ToDecimal(TotalAmt);
                                FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text) + Convert.ToDecimal(ViewState["latefee"])).ToString();
                                Amt = 0;
                                CourseAmtt = 0;
                            }

                        }
                  
                }
                 #endregion 
            else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)//credit wise
            {

                CalculateTotalCredit();               
                HideClm();
              
            }
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
        else
        {

            applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"])));
            if (applycourse > 0)
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                    HiddenField hdf = (HiddenField)e.Item.FindControl("hdfapplycourse");
                    CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                    HiddenField hdfapplycoursedone = (HiddenField)e.Item.FindControl("hdfapplycoursedone");

                    if (hdfapplycoursedone.Value == "1" && hdf.Value=="1")
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        chkhead.Checked = false;
                        chkhead.Enabled = false;
                    }
                    else if (hdf.Value == "1")
                    {
                          chk.Checked = true;                      
                         chkhead.Checked = false;
                        

                          }
                    else
                    {
                        chk.Checked = false;

                    }
                }
            }

        }


    }

    protected void btnSubmitnopayment_Click(object sender, EventArgs e)
    {
        try
        {
                #region GET STUDENT DETALS
          // StudentRegistration objSRegist = new StudentRegistration();
          // StudentRegist objSR = new StudentRegist();
          // StudentController objSC = new StudentController();
          // int idno = 0;
          // idno = Convert.ToInt32(Session["idno"]);
          // string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);
          // objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
          //  objSR.IDNO = idno;
           // objSR.REGNO = Regno;
           // objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
          //  objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
          //  objSR.COLLEGE_CODE = Session["colcode"].ToString();
         //   objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
          //  objSR.COURSENOS = string.Empty;
        ////    objSR.SEMESTERNOS = string.Empty;
           // int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
     //       int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            int cntcourse = 0;
           /// objSA.DegreeNo = degreenos;
           /// objSA.BranchNo = branchnos;
           // objSA.IpAddress = ViewState["ipAddress"].ToString();

            int ifPaidAlready = 0;
            ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + Convert.ToInt32(Session["semesterNO"])));
            if (ifPaidAlready > 0)
            {
                if (ViewState["usertype"].ToString() == "2")
                {
                    objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                    return;
                }
                else 
                { 
                
                }
               
            }
          
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    Label lblsemester = dataitem.FindControl("lblsemester") as Label;
                    //Session["semesterNO"] = lblsemester.ToolTip;
                    if (chk.Checked == true)
                        cntcourse++;
                         

                }

            }
            if (cntcourse == 0)
            {
                objCommon.DisplayMessage(updatepnl, "Please Select Courses..!!", this.Page);            
                return;
            }
          
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();
            StudentController objSC1 = new StudentController();
           // int OrganizationId = Convert.ToInt32(Session["OrgId"]), degreeno = 0, college_id = 0;
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
            string coursenos = string.Empty;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {

                    Label courseno = dataitem.FindControl("lblCCode") as Label;
                    coursenos += courseno.ToolTip + ",";
                }

            }
            objSR.COURSENOS = coursenos;
           // objSR.SEMESTERNOS = string.Empty;
            //objSR.SEMESTERNOS = lblSemester.ToolTip;
            objSR.SEMESTERNOS = Session["semesterNO"].ToString();
            int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));

            objSA.DegreeNo = degreenos;
            objSA.BranchNo = branchnos;
            objSA.IpAddress = ViewState["ipAddress"].ToString();
            CreateStudentPayOrderId();

            //CREATE DEMAND              
            ExamController objEC = new ExamController();
           // string Amt = FinalTotal.Text;

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

            if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"]== null)
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
            //string Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] +","+ViewState["CrettificateFee"] +","+ ViewState["latefee"] + "," + FinalTotal.Text;

            string Amt = string.Empty;
            string F1 = string.Empty;
            string Total = string.Empty;
            decimal TotalSubFee;
            decimal Final;
            F1 = objCommon.LookUp("ACD_DCR", "SUM(f1)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND RECIEPT_CODE = 'REF'" + "  AND  SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]));
            Total = objCommon.LookUp("ACD_DCR", "SUM(TOTAL_AMT)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND RECIEPT_CODE = 'REF'" + "  AND  SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]));
            if (ViewState["usertype"].ToString() == "1" && F1 != string.Empty)
            {
                //decimal a = Convert.ToDecimal(ViewState["TotalSubFee"]);
                TotalSubFee = Convert.ToDecimal(ViewState["TotalSubFee"].ToString()) + Convert.ToDecimal(F1);
                Final = Convert.ToDecimal(ViewState["TotalSubFee"]) + Convert.ToDecimal(Total);
                Amt = TotalSubFee + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + Final;
                #region for update paid fix fee
                if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)
                {
                    if (lvFailCourse.Items.Count > 0)
                    {
                        int flag;
                        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        {
                            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
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
                                string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_MIDTERM_FREE";
                                string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
                                string Call_Values = "" + Idno + "," + Convert.ToInt32(ViewState["sessionnonew"]) + "," + ccode + "," + flag + ",0";
                                string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                                if (que_out == "1")
                                {
                                    objCommon.DisplayMessage("Course Registration Update Sucessfully", this.Page);


                                    HideClm();
                                }
                                else
                                {
                                    objCommon.DisplayMessage("Course Registration Update Sucessfully", this.Page);

                                    HideClm();
                                }
                            }

                        }
                        bindcourses();
                    }
                    return;
                }
                #endregion

            }

            else
            {
                Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;

            }

            string totalamt = FinalTotal.Text;


            //  return;
            #region COMMENT BY GAURAV FOR HOT FIX 
            //int retStatus = objEC.AddStudentExamRegistration_ReExam(objSR, Amt, ViewState["OrderId"].ToString());
            int retStatus = AddStudentExamRegistration_ReExam(objSR, Amt, ViewState["OrderId"].ToString());
            if (retStatus == -99)
            {
                objCommon.ShowError(Page, "Academic_ReExam_CC.btnPay_Click() --> ");
                return;

            }        



            #endregion

           
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    int flag;
                    CheckBox chk = dataitem.FindControl("chkaccept") as CheckBox;
                    Label lblccode = dataitem.FindControl("lblccode") as Label;
                    int Idno = Convert.ToInt32(Session["idno"]);
                    CheckBox CheckId = dataitem.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    Label fees = dataitem.FindControl("lblAmt") as Label;

                    HiddenField ExistingMark = dataitem.FindControl("hdfExistingMark") as HiddenField;    
                    if (chk.Checked == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }


                    int ccode = Convert.ToInt32(lblccode.ToolTip);
                    if (idno > 0)
                    {
                        //int sem = Convert.ToInt32(lblSemester.ToolTip);
                        int user = Convert.ToInt32(Session["userno"]);
                        double fees1 = 0.00;
                        if (fees.Text == "")
                        {
                            fees1 = 0.00;
                        }
                        else
                        {
                            fees1 = Convert.ToDouble(fees.Text);
                        }
                        
                        string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_REEXAM";
                        string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";
                        // string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + cntcourse + ",1";

                        string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Session["semesterNO"]) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + fees1 + "," + flag + ",1";

                        // return;

                        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                        if (que_out == "0")
                        {
                            //objCommon.DisplayMessage(updatepnl,"Course Apply Sucessfully", this.Page);

                        }
                        else
                        {
                            //objCommon.DisplayMessage(updatepnl,"Course Update Sucessfully", this.Page);                                            
                        }
                    }

                }
            }


            double TotalAmount;
            int degreeno = 0;
            int college_id = 0;
            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);         
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;          
             Session["studAmt"] = totalamt;           
            ViewState["studAmt"]= totalamt;
            //TotalAmount = Convert.ToDouble(Amt);//Convert.ToDouble(ViewState["studAmt"].ToString());
            TotalAmount = Convert.ToDouble(totalamt);
            Session["studName"] = lblName.Text;
            Session["studPhone"] = ViewState["MOBILENO"].ToString();
            Session["studEmail"] = ViewState["EmailID"].ToString();
            Session["paysemester"] = Session["semesterNO"];
            Session["YEARNO"] = lblAdmBatch.Text;
            Session["ReceiptType"] = "REF";        
            Session["idno"] = Convert.ToInt32(Session["idno"].ToString());
            Session["paysession"] = Convert.ToInt32(ViewState["sessionnonew"]);           
            Session["homelink"] = "ReExam_CC.aspx";
            Session["regno"] = lblEnrollNo.Text;
            Session["payStudName"] = lblName.Text;
            Session["paymobileno"] = ViewState["MOBILENO"].ToString();
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblBranch.Text;


            if (Session["OrgId"].ToString() == "6")// added by gaurav S. Rcpiper 19_07_2023
            {
                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            }

       

           string payactivityno = objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVESTATUS=1 AND ACTIVITYNAME like '%Remajor%'");
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
                if (PAYID ==string.Empty || PAYID == null)
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

                objCommon.DisplayMessage(updatepnl,"Payment configuration is not done for this session.", this.Page);
                bindcourses();       
                return;

            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.btnPay_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

     

    }
    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            int applycourse = 0;
            
            if ( Convert.ToInt32(Session["OrgId"]) == 6 || Convert.ToInt32(Session["OrgId"]) == 8)//RCPIPER
            {
                #region MIT OR RCPIPER
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                    CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                    chk.Checked = true;
                   // chk.Enabled = false;
                    chkhead.Checked = true;
                    //chkhead.Enabled = false;
                }
                #endregion MIT OR RCPIPER
            }
       
            else
            {



                if (ViewState["usertype"].ToString() == "1")
                {
                    #region Check For Admin
                    CheckBox chk1 = (CheckBox)e.Item.FindControl("chkAccept");
                    CheckBox chkhead1 = lvFailCourse.FindControl("chkAll") as CheckBox;
                    //applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND ISNULL(EXT_IND,0)=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));

                    //applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ISNULL(STUD_EXAM_REGISTERED,0)=1 AND ISNULL(EXAM_REGISTERED,0)=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                    applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"])));
                    //applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ISNULL(ApplyCourse,0)=1 AND  SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                    if (applycourse > 0)
                    {
                        if (e.Item.ItemType == ListViewItemType.DataItem)
                        {
                            CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                            HiddenField hdfapplycourse = (HiddenField)e.Item.FindControl("hdfapplycourse");

                            CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                            HiddenField hdfabsentlog = (HiddenField)e.Item.FindControl("hdfabsentlog");
                            HiddenField hdfStudRegistered = (HiddenField)e.Item.FindControl("hdfStudRegistered");

                            if (hdfapplycourse.Value == "1")
                            {

                                if (hdfabsentlog.Value == "1")
                                {
                                    chk.Checked = true;
                                    chk.Enabled = false;
                                    chkhead.Checked = false;
                                    chkhead.Enabled = false;
                                    if (hdfStudRegistered.Value == "1")
                                    {
                                        chk.BackColor = System.Drawing.Color.Green;
                                    }
                                    

                                }
                                else if (hdfabsentlog.Value != "1" && hdfapplycourse.Value=="1")
                                {
                                    chk.Checked = true;
                                    chk.Enabled = true;
                                    chkhead.Checked = false;
                                    chkhead.Enabled = false;
                                }
                                else
                                {
                                    chk.Checked = false;
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
                        chk1.Checked = true;
                        chk1.Enabled = true;
                        chkhead1.Checked = false;
                        chkhead1.Enabled = true;
                    }
                    #endregion
                }
                else
                {
                    #region  For student
                    int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    //CHECK FEES APPlCABLE OR NOT 
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2 AND COLLEGE_ID=" + cid + "  AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
                    //if (CheckExamfeesApplicableOrNot >= 2) //temp update  
               
                    if (CheckExamfeesApplicableOrNot >= 1)   
                    {
                        applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"])));
                        //applycourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND ISNULL(STUD_EXAM_REGISTERED,0)=1 AND ISNULL(EXAM_REGISTERED,0)=1 AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"])));
                        if (applycourse > 0)
                        {
                            if (e.Item.ItemType == ListViewItemType.DataItem)
                            {
                                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                                HiddenField hdfapplycourse = (HiddenField)e.Item.FindControl("hdfapplycourse");
                                CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                                HiddenField hdfabsentlog = (HiddenField)e.Item.FindControl("hdfabsentlog");
                                HiddenField hdfStudRegistered = (HiddenField)e.Item.FindControl("hdfStudRegistered");

                                if (hdfapplycourse.Value == "1")
                                {
                                    if (hdfabsentlog.Value == "1")
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = false;
                                        chkhead.Checked = false;
                                        chkhead.Enabled = false;
                                        if (hdfStudRegistered.Value == "1")
                                        {
                                            chk.BackColor = System.Drawing.Color.Green;
                                        }
                                    }
                                    else if (hdfabsentlog.Value != "1" && hdfapplycourse.Value == "1")
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = true;
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



                    }
                    else
                    {

                        applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"])));
                        if (applycourse > 0)
                        {
                            if (e.Item.ItemType == ListViewItemType.DataItem)
                            {
                                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                                HiddenField hdfapplycourse = (HiddenField)e.Item.FindControl("hdfapplycourse");
                                CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                                HiddenField hdfabsentlog = (HiddenField)e.Item.FindControl("hdfabsentlog");

                                if (hdfapplycourse.Value == "1")
                                {
                                    if (hdfabsentlog.Value == "1")
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = false;
                                        chkhead.Checked = false;
                                        chkhead.Enabled = false;
                                    }
                                    else if (hdfabsentlog.Value != "1" && hdfapplycourse.Value == "1")
                                    {
                                        chk.Checked = true;
                                        chk.Enabled = true;
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
                    }
                    #endregion
                }
               
               

                //if (ViewState["usertype"].ToString() == "2")
                //{
                   
                //}

                
            }  
        }
       
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.lvFailCourse_ItemDataBound() --> " + ex.Message + " " + ex.StackTrace);
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
        //string latefees=string.Empty;

        #region ChkProcessing Fee

        bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckProcFee == true)
        {
            ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        else
        {
            ProFess = 0;
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        #endregion
        #region Certificate Fee Applicable
        bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        if (CheckCrettificateFee == true)
        {

            CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
        
        }
        else
        {
            CrettificateFee = 0;
            ViewState["CrettificateFee"] = CrettificateFee;//FESS HEAD F3
        }
        #endregion    
        #region LATE FEE commented
        //DataSet dsStudent = null;
        //string date = string.Empty;
        //string strNewDate = string.Empty;
        //string format = "dd/mm/yyyy";
        //int type = 0;
        //string dateString = string.Empty;
        //decimal calculatelatefee = 0;
        //string totalsubfee = string.Empty;
        //bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        //if (Latefee == true)
        //{

        //    latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            

        //    DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "Top(1)CAST(ISNULL(LATEFEEDATE,0) as date) AS DATE", "ISNULL(LateFeeMode,0) AS TYPE", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
        //        dateString = date.Substring(0, 11);
        //        DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
        //        strNewDate = dateTime.ToString("yyyy-mm-dd");
        //        type = Convert.ToInt32(ds.Tables[0].Rows[0]["TYPE"]);
        //    }
        //    string SP_Name = "ACD_CALCULATE_DAY_WEEK_MONTHLY";
        //    string SP_Parameters = "@P_DATE, @P_TYPE";
        //    string Call_Values = "" + strNewDate + "," + Convert.ToInt32(type) + "";// +"," + Convert.ToInt16(ViewState["sem"]) + "," + 
        //    dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
           
            
        //    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"])>0)
        //    {
        //        calculatelatefee = latefees * Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

        //    }
        //    else
        //    {
        //        calculatelatefee = 0;
        //    }
        //    // decimal abc = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);
        //    //calculatelatefee = latefees * abc;
        //    if (type == 1)
        //    {
        //        lblLateFee.Text = "<b style='color:red;'>PERDAY </b> : " + dateString +"  "+ latefees.ToString();
        //    }
        //    else if (type == 1)
        //    {
        //        lblLateFee.Text = "<b style='color:red;'>WEEKLY </b> : " + dateString + "  " + latefees.ToString();
        //       // lblLateFee.Text = "WEEKLY   " + latefees.ToString();
        //    }
        //    else if (type == 3)
        //    {
        //        lblLateFee.Text = "<b style='color:red;'>MONTHLY </b> : " + dateString + "  " + latefees.ToString();             
             
        //    }

            
            
        //}
        //else
        //{
        //    latefees = 0;
        //}
        #endregion    
        #region  LATE FEE Checklate fee applicable

        DataSet dsStudent = null;
        string date = string.Empty;
        string strNewDate = string.Empty;
        string format = "dd/mm/yyyy";
        string dateString = string.Empty;
        decimal calculatelatefee = 0;
        string totalsubfee = string.Empty;

        string Latefee = objCommon.LookUp("ACD_LATE_FEE_EXAM", "top 1 (LATE_FEE_NO)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNOS LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' and ISNULL(ISACTIVE,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]));

        if (Latefee != "")
        {



            DataSet ds = objCommon.FillDropDown("ACD_LATE_FEE_EXAM", "CAST(ISNULL(LAST_DATE,0) as date) AS DATE", "DEGREENO", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNOS LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND  ISNULL(ISACTIVE,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
                dateString = date.Substring(0, 11);
                DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
                strNewDate = dateTime.ToString("yyyy-mm-dd");
             
            }
            string SP_Name = "PKG_CALCULATE_DAY_FORLATEFEE";
            string SP_Parameters = "@P_DATE";
            string Call_Values = "" + strNewDate + "";
            dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            
            latefees = Convert.ToDecimal(objCommon.LookUp("ACD_MASTER_LATE_FEE_EXAM", " TOP (1) AMOUNT", " LATE_FEE_NO=" + Latefee + "  AND  " + Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]) + " BETWEEN DAY_NO_FROM AND DAY_NO_TO "+" union all select  0 as AMOUNT"));
            //Decimal ApplicablelateFee;
            //if (latefees == "")
            //{
            //    ApplicablelateFee = 00;
            //}
            //else 
            //{
            //    ApplicablelateFee = Convert.ToDecimal(latefees);
            //}
            if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"]) > 0)
            {
                calculatelatefee = latefees; //* Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

            }
            else
            {
                calculatelatefee = 0;
            }
            ViewState["latefee"] = calculatelatefee;
            lblLateFee.Text = "<b style='color:red;'>Late Fee </b> : " + dateString + "  " + latefees.ToString();

        }
        else
        {
            latefees = 0;
        }
        #endregion    


        ApplFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(FEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(CANCEL,0)=0 and FEESTRUCTURE_TYPE=4 AND    ISNULL(IsFeesApplicable,0)=1 and  COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        
         lblfessapplicable.Text = ProFess.ToString();
         lblTotalExamFee.Text = ApplFess.ToString();
         lblCertificateFee.Text = CrettificateFee.ToString();
        // lblLateFee.Text = latefees.ToString();


             if (lblfessapplicable.Text == string.Empty)// || lblTotalExamFee.Text == string.Empty || lblCertificateFee.Text == string.Empty )
                {
                    lblfessapplicable.Text = "0.00";
                }
               else if(lblTotalExamFee.Text == string.Empty)
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

        bool CheckProcFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsProFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
        //CheckProcFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(SUM(APPLICABLEFEE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

        if (CheckProcFee == true)
        {
            ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(APPLICABLEFEE,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        else
        {
            ProFess = 0;
            ViewState["CheckProcFee"] = ProFess;//FESS HEAD F2
        }
        #endregion
       #region Certificate Fee Applicable
        bool CheckCrettificateFee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsCertiFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
       if (CheckCrettificateFee == true)
       {

           CrettificateFee = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(CertificateFee,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsCertiFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));
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
       //bool Latefee = Convert.ToBoolean(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Top(1) ISNULL(IsLateFeesApplicable,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

       string Latefee = objCommon.LookUp("ACD_LATE_FEE_EXAM", "top 1 (LATE_FEE_NO)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNOS LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' and ISNULL(ISACTIVE,0)=0  and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]));
       
        if (Latefee !="")
       {

           //latefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", " Top(1)ISNULL(LateFeeAmount,0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsLateFeesApplicable,0)=1 and FEESTRUCTURE_TYPE=" + Convert.ToInt32(ViewState["FEESTYPE"]) + " and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));


           DataSet ds = objCommon.FillDropDown("ACD_LATE_FEE_EXAM", "CAST(ISNULL(LAST_DATE,0) as date) AS DATE", "DEGREENO", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNOS LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND  ISNULL(ISACTIVE,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
          // DataSet ds = objCommon.FillDropDown1("ACD_LATE_FEE_EXAM", "CAST(ISNULL(LAST_DATE,0) as date) AS DATE","DEGREENO", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNOS LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND  ISNULL(ISACTIVE,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]), "");
           if (ds.Tables[0].Rows.Count > 0)
           {
               date = Convert.ToString(ds.Tables[0].Rows[0]["DATE"]);
               dateString = date.Substring(0, 11);
               DateTime dateTime = DateTime.ParseExact(dateString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture);
               strNewDate = dateTime.ToString("yyyy-mm-dd");
               //type = Convert.ToInt32(ds.Tables[0].Rows[0]["TYPE"]);
           }
           //string SP_Name = "ACD_CALCULATE_DAY_WEEK_MONTHLY";
           //string SP_Parameters = "@P_DATE, @P_TYPE";
           //string Call_Values = "" + strNewDate + "," + Convert.ToInt32(type) + "";// +"," + Convert.ToInt16(ViewState["sem"]) + "," + 
           string SP_Name = "PKG_CALCULATE_DAY_FORLATEFEE";
           string SP_Parameters = "@P_DATE";
           string Call_Values = "" + strNewDate + "";
           dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

           //latefees = Convert.ToDecimal(objCommon.LookUp("ACD_MASTER_LATE_FEE_EXAM", " TOP (1) AMOUNT", " LATE_FEE_NO=" + Latefee + "  AND  " + Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]) + " BETWEEN DAY_NO_FROM AND DAY_NO_TO "));

           latefees = Convert.ToDecimal(objCommon.LookUp("ACD_MASTER_LATE_FEE_EXAM", " TOP (1) AMOUNT", " LATE_FEE_NO=" + Latefee + "  AND  " + Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]) + " BETWEEN DAY_NO_FROM AND DAY_NO_TO " + " union all select  0 as AMOUNT"));

           if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["COUNT"]) > 0)
           {
               calculatelatefee = latefees; // *Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);

           }
           else
           {
               calculatelatefee = 0;
           }
           ViewState["latefee"] = calculatelatefee;
           lblLateFee.Text = "<b style='color:red;'>Late Fee </b> : " + dateString + "  " + latefees.ToString();
       
           // decimal abc = Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["COUNT"]);
           //calculatelatefee = latefees * abc;
           //if (type == 1)
           //{
           //    lblLateFee.Text = "<b style='color:red;'>PERDAY </b> : " + dateString + "  " + latefees.ToString();
           //}
           //else if (type == 1)
           //{
           //    lblLateFee.Text = "<b style='color:red;'>WEEKLY </b> : " + dateString + "  " + latefees.ToString();
           //    // lblLateFee.Text = "WEEKLY   " + latefees.ToString();
           //}
           //else if (type == 3)
           //{
           //    lblLateFee.Text = "<b style='color:red;'>MONTHLY </b> : " + dateString + "  " + latefees.ToString();

           //}



       }
       else
       {
           latefees = 0;
       }
       #endregion    
       #region  CREDITWISE=4
       if (Convert.ToInt32(ViewState["FEESTYPE"]) == 5)
        {
            int paid = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));

            if (Convert.ToInt32(ViewState["usertype"]) == 1 && paid > 0)
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
                        if (cbRow.Enabled == true)
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
            }
            else
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
            }

           

            if (TotalAmt == string.Empty || TotalAmt == null)
            {
                TotalAmt = "0";
            }
            hdfCreditTotal.Value = Convert.ToDecimal(TotalAmt).ToString();
            #region FOR CREDIT COURSR WISE CALCULATION
            //COMPARE CREDIT TOTAL WISE FEE
            PAYID = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "CREDIT_RANGE_AMOUNT", "MINRANGE<=" + hdfCreditTotal.Value + " AND MAXRANGE>=" + hdfCreditTotal.Value + " AND SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + "and FEETYPE=2 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"]) + " and ISNULL(CANCEL,0)=0 AND  FEESTRUCTURE_TYPE=5  AND SEMESTERNO LIKE '%" + Session["semesterNO"] + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'");
             if (PAYID == string.Empty || PAYID == null)
             {
                 PAYID = "0";
             }

             ViewState["TotalSubFee"] = PAYID; // added by gaurav 21_08_2023
            #endregion


        }
       #endregion
       #region 3/1-Course Wise
       else if (Convert.ToInt32(ViewState["FEESTYPE"]) == 3||Convert.ToInt32(ViewState["FEESTYPE"]) == 1)//3-Course Wise
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
       int paidf = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(Session["semesterNO"]) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND AD.IDNO=" + Convert.ToInt32(Session["idno"])));

       if (Convert.ToInt32(ViewState["usertype"]) == 1 && paidf > 0)
       {
           lblfessapplicable.Text = "0";     
           lblTotalExamFee.Text = PAYID;//total sub       
           lblCertificateFee.Text = "0";
           lblTotalExamFee.Text = Amt.ToString();
       }
       else
       {
           lblfessapplicable.Text = ProFess.ToString();//proc         
           lblTotalExamFee.Text = PAYID;//total sub       
           lblCertificateFee.Text = CrettificateFee.ToString();
           lblTotalExamFee.Text = Amt.ToString();
       }
        
     

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
          
             FinalTotal.Text = (Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)+calculatelatefee).ToString();
        
        }
        else
        {

            //FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)).ToString();
            FinalTotal.Text = (Convert.ToDecimal(TotalAmt) + Convert.ToDecimal(PAYID) + Convert.ToDecimal(lblfessapplicable.Text) + Convert.ToDecimal(lblCertificateFee.Text)+calculatelatefee).ToString();
        }
   


    }
    protected void HideClm()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
    }
    protected void btnSubmit_WithDemand_Click(object sender, EventArgs e)
    {
        
            FeeCollectionController objFee = new FeeCollectionController();
            try
            {
                int cntcourse = 0;

                int ifPaidAlready = 0;
                ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + Convert.ToInt32(Session["semesterNO"])));
                if(Convert.ToInt32(ViewState["usertype"])==2)
                {
                    objCommon.DisplayMessage(updatepnl, "Re - Major Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                    return;
                }
            #endregion
                #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
                if (lvFailCourse.Items.Count > 0)
                {
                   
                    foreach (ListViewDataItem item in lvFailCourse.Items)
                    {
                        int Courseapply = 0;
                        CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                        Label lblCCode = item.FindControl("lblCCode") as Label;
                        Label fees = item.FindControl("lblAmt") as Label;
                        Label Sem = item.FindControl("lblsem") as Label;
                        HiddenField ExistingMark = item.FindControl("hdfExistingMark") as HiddenField;                     

                        int Idno = Convert.ToInt32(Session["idno"]);
                      
                        if (CheckId.Checked == true)
                        {
                            Courseapply = 1;
                        }
                        else
                        {
                            Courseapply = 0;
                        }
                        #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
                        //if (CheckId.Checked == true)
                        //{
                        if (Idno > 0)
                        {
                            string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW_ATLAS";
                            string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";                          
                            string Call_Values = "" + Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + Courseapply + ",1";

                            string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_REEXAM";
                            string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";
                           // string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + cntcourse + ",1";

                           // string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(lblSemester.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.Text)+"," + flag + ",1";
                            string Call_Values = Idno + "," + Convert.ToInt32(Session["sessionnonew"]) + "," + ccode + "," + Convert.ToInt32(Session["semesterNO"]) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + fees1 + "," + flag + ",1";

                            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                            if (que_out == "0")
                            {
                                //objCommon.DisplayMessage(updatepnl,"Course Apply Sucessfully", this.Page);

                            }
                            else
                            {
                                //objCommon.DisplayMessage(updatepnl,"Course Update Sucessfully", this.Page);                                            
                            }

                        }

                        #endregion

                        // }
                    }
                }



                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                 {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {
                        Label courseno = dataitem.FindControl("lblCCode") as Label;
                        coursenos += courseno.ToolTip + ",";
                    }

                }
                objSR.COURSENOS = coursenos;
                objSR.SEMESTERNOS = Session["semesterNO"].ToString();
                int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
                int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
                objSA.DegreeNo = degreenos;
                objSA.BranchNo = branchnos;
                objSA.IpAddress = ViewState["ipAddress"].ToString();
                CreateStudentPayOrderId();
                //CREATE DEMAND              
                ExamController objEC = new ExamController();
                // string Amt = FinalTotal.Text;
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
                string Amt = string.Empty;
                string F1 = string.Empty;
                string Total = string.Empty;
                decimal TotalSubFee;
                decimal Final;
                F1 = objCommon.LookUp("ACD_DCR", "SUM(f1)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND RECIEPT_CODE = 'REF'" + "  AND  SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]));
                Total = objCommon.LookUp("ACD_DCR", "SUM(TOTAL_AMT)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()) + "AND RECIEPT_CODE = 'REF'" + "  AND  SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]));
                if (ViewState["usertype"].ToString() == "1" && F1 != string.Empty)
                {
                    //decimal a = Convert.ToDecimal(ViewState["TotalSubFee"]);
                    TotalSubFee = Convert.ToDecimal(ViewState["TotalSubFee"].ToString()) + Convert.ToDecimal(F1);
                    Final = Convert.ToDecimal(ViewState["TotalSubFee"]) + Convert.ToDecimal(Total);
                    Amt = TotalSubFee + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + Final;
                    #region for update paid fix fee
                    if (Convert.ToInt32(ViewState["FEESTYPE"]) == 4)
                    {
                        if (lvFailCourse.Items.Count > 0)
                        {
                            int flag;
                            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                            {
                                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                                Label lblCCode = dataitem.FindControl("lblCCode") as Label;
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
                                    string SP_Name = "PKG_ACD_INSERT_EXAMREGISTRATION_FREE";
                                    string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_COURSENO,@P_STATUS,@P_OUT";
                                    string Call_Values = "" + Idno + "," + Convert.ToInt32(ViewState["sessionnonew"]) + "," + ccode + "," + flag + ",0";
                                    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                                    if (que_out == "1")
                                    {
                                        objCommon.DisplayMessage("Course Registration Update Sucessfully", this.Page);


                                        HideClm();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage("Course Registration Update Sucessfully", this.Page);

                                        HideClm();
                                    }
                                }

                            }
                            bindcourses();
                        }
                        return;
                    }
                    #endregion

                }
                else
                {
                    Amt = ViewState["TotalSubFee"] + "," + ViewState["CheckProcFee"] + "," + ViewState["CrettificateFee"] + "," + ViewState["latefee"] + "," + FinalTotal.Text;
                } 
                string totalamt = FinalTotal.Text;



                #region Comment by gaurav for hot fix

                int retStatus = AddStudentExamRegistration_ReExam(objSR, Amt, ViewState["OrderId"].ToString());
                #endregion 


             
                if (retStatus == -99)
                {

                    objCommon.ShowError(Page, "Academic_ReExam_cc.btnSubmit_WithDemand_Click() --> ");
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(updatepnl, "Exam Registration Demand Create Successfully!", this.Page);
                    bindcourses();
                    return;
                }

            
                #endregion
                #region CREATE DEMAND
                string coursenos = string.Empty;
                string semesterno = string.Empty;
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {

                        Label courseno = dataitem.FindControl("lblCCode") as Label;
                        Label semesternos = dataitem.FindControl("lblsem") as Label;
                        coursenos += courseno.ToolTip + ",";
                        semesterno = semesternos.ToolTip;
                    }

                }
                coursenos = coursenos.TrimEnd(',');
                //StudentController objSC1 = new StudentController();
                DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
                string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
                objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
                objSR.COURSENOS = coursenos;
                objSR.IDNO = Convert.ToInt32(Session["idno"]);
                objSR.REGNO = RegNo;
                objSR.SCHEMENO = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString());
                //objSR.SEMESTERNOS = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                objSR.SEMESTERNOS = semesterno;
                objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                string Amt = FinalTotal.Text;
                CreateStudentPayOrderId();
                //create Demand
                // StudentController objSC = new StudentController();
                ExamController objEC = new ExamController();
                int retStatus = objEC.AddStudentResitExamRegistrationDetails(objSR, Amt, ViewState["OrderId"].ToString());
                if (retStatus == -99)
                {
                    // retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    objCommon.DisplayMessage(updatepnl, "Something Went Worong", this.Page);
                    Disible_listview();
                    return;

                }
                else
                {
                    objCommon.DisplayMessage(updatepnl, "Demand is Created for this subjects. Please go to counter for Payment Registration.", this.Page);                  
                    Disible_listview();
                    return;
                }
              

            }
            // return;
                #endregion
        
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReExam_CC.btnSubmit_Click()--> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void Disible_listview()
    {

        #region FOR DISABLE

        int Registered = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "count(1)", "SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"]) + "  AND IDNO=" + Convert.ToInt32(Session["idno"])));
        if (Registered > 0)
        {
            lvFailCourse.Enabled = false;
            btnSubmitnopayment.Enabled = false;

        }
        else
        {
            lvFailCourse.Enabled = true;
        }
        #endregion
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            int cid = 0;
            int idno = 0;
            PaidTotal.Text = string.Empty;
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtEnrollno.Text.Trim() + "'  UNION ALL SELECT 0 AS IDNO"));
            if (idno == 0 || idno == null)
            {
                objCommon.DisplayMessage(updatepnl, "Please Search Correct Registration Numbar...!", this.Page);
                divCourses.Visible = false;
                divCourses.Visible = false;
                lvFailCourse.Visible = false;
                divbtn.Visible = false;

            }
            Session["idno"] = Convert.ToInt32(idno);
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
        catch (Exception ex)
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



    public int AddStudentExamRegistration_ReExam(StudentRegist objSR, string Amt, string order_id)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {

            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = null;

            //Add New eXAM Registered Subject Details
            objParams = new SqlParameter[12];

            objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
            objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
            objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
            objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
            objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
            objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
            objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
            objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
            objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
            objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
            objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
            objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[11].Direction = ParameterDirection.Output;



            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTRATION_FOR_REEXAM", objParams, true);

            if (Convert.ToInt32(ret) == -99)
                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            else
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
        }

        return retStatus;

    }



}
    
