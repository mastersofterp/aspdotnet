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

public partial class Academic_ReExam_CC : System.Web.UI.Page
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
            //ViewState["degreenos"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //ViewState["branchnos"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
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
                            lblBacklogFine.Text = "0";
                            lblTotalFee.Text = "0";


                            #region ADD FOR BACKLOG SEMESTER dropdown

                            // int oddevensem = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(Session["sessionnonew"])));                                                    
                            objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT_HIST H INNER JOIN ACD_SEMESTER S ON (H.SEMESTERNO=S.SEMESTERNO)", "DISTINCT H.SEMESTERNO", "DBO.FN_DESC('SEMESTER',H.SEMESTERNO)SEMESTER", " idno =" + Convert.ToInt32(Session["idno"]) + "  AND S.SEMESTERNO > 0  and isnull(CANCEL,0)=0 ", "SEMESTERNO");//AND  GDPOINT=0 
                            #endregion
                            #region NOT IN USE
                            int Duration = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO= CDB.DEGREENO)", "DISTINCT DURATION", "D.DEGREENO=" + hdfDegreeno.Value));
                            Duration = Convert.ToInt32(Duration) * 2;
                            //hdfDuration.Value = Duration.ToString();
                            //hdfSemester.Value = (lblSemester.ToolTip).ToString();
                            #endregion

                        }
                        else
                        {
                            IsNotActivitySem = true;
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
      // COMMENT FOR MULTIPLE SESSION 
       // sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO) INNER JOIN ACD_TRRESULT T ON (T.SESSIONNO=SA.SESSION_NO AND T.IDNO="+Convert.ToInt32(Session["idno"])+")", "MAX(T.SESSIONNO)", "am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%'");
       #endregion
        if (sessionno == string.Empty || sessionno == null || sessionno == "0")
        {
            sessionno = "0";        
        }
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + cid + " UNION ALL SELECT 0 AS SESSION_NO");
        Session["sessionnonew"] = sessionno;
        //if (Session["sessionnonew"] != null & Session["sessionnonew"] != string.Empty)
        //{//histry prev=0
            semester = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SEMESTER", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS=" + cid + "AND SA.SESSION_NO=" + Convert.ToInt32(Session["sessionnonew"]));

            Session["Semester"] = semester.TrimEnd(',');

            if (semester == string.Empty || semester == null || semester == "0")
            {
                semester = "0";//no use bcoz we are fetchind sessionno from ACD_TRRESULT
            }
        //}
       
        
      
  
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
                lvFailCourse.DataSource = dsFailSubjects;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                pnlFailCourse.Visible = true;
                
                ViewState["Semesternos"] = dsFailSubjects.Tables[0].Rows[0]["SEMESTER"].ToString();

                int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(Session["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2    and ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
                
                    if (CheckExamfeesApplicableOrNot >= 1)
                    {              
                            CalculateTotal();                     

                            int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND (TRANSACTIONSTATUS='Success' or TRANSACTIONSTATUS='1') AND AD.RECIEPT_CODE='REF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
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
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                     Label lblCCode = dataitem.FindControl("lblCCode") as Label;
                    if (chk.Checked == true) //if (chk.Enabled == true)
                        cntcourse++;
                }

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
                    Session["paymentId"] = ds2.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds2.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }
            else
            {

               
              
                objCommon.DisplayMessage( updatepnl,"Payment configuration is not done for this session.", this.Page);
               // bindcourses();
                return;

            }
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
    #region ddlBackLogSem_SelectedIndexChanged NO USE
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
            lblTotalExamFee.Text = "0.00";
            FinalTotal.Text = "0.00";
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
        ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(SUM(APPLICABLEFEE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

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
                objCommon.DisplayMessage(updatepnl, "Please Select Courses..!!", this.Page);            
                return;
            }
            else
            {
                int ifPaidAlready = 0;
                ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + lblSemester.ToolTip));
                if (ifPaidAlready > 0)
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

}
    
