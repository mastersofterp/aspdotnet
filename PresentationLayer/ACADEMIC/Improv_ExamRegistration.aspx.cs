//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : EXAMINATION                                                             
// PAGE NAME     : IMPROVEMENT EXAM REGISTARTION                              
// CREATION DATE : 14 DEC 2022                                                          
// CREATED BY    : SHUBHAM BARKE                                         
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Improv_ExamRegistration : System.Web.UI.Page
{


    Common objCommon = new Common();
    FeeCollectionController feeController = new FeeCollectionController();
    bool flag = true;
    bool IsNotActivitySem = false;
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
        divMsg.InnerHtml = "";
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
                 
                // this.CheckPageAuthorization();
                int payid = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_GATEWAY", "PAYID", "ACTIVE_STATUS=1 AND PAY_GATEWAY_NAME like '%PayU%'"));
                Session["payid"] = payid; // Used to save payment Id 
                int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PAYMENT_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVESTATUS=1 AND ACTIVITYNAME like '%Improvement%'"));
                Session["payactivityno"] = payactivityno;  // // Used to save payment Activity Number 
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //File Dropdown Box


                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

               
                DateTime ExamLateDate = Convert.ToDateTime(objCommon.LookUp("reff", "Exam_Last_Date", ""));
                //  hdfExamLastDate.Value = ExamLateDate.ToString("dd/MM/yyyy");

                decimal ExamLateFee = Convert.ToDecimal(objCommon.LookUp("reff", "Exam_Late_Fee_Amt", ""));
                int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                if (CheckActivity())
                {
                    //CHECK ACTIVITY FOR PAGE
                    FillDropdown();
                    if (ViewState["usertype"].ToString() == "2")
                    {

                        int cid = 0;
                        int idno = 0;

                        idno = Convert.ToInt32(Session["idno"]);
                        cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

                        if (CheckActivityCollege(cid))
                        {
                            //CHECK ACTIVITY FOR EXAM REGISTRATION FOR COLLEGE
                            //CheckActivity();
                            txtEnrollno.Text = string.Empty;
                            btnSearch.Visible = false;
                            btnClear.Visible = false;
                            divCourses.Visible = true;
                            pnlSearch.Visible = false;
                            //btnShow.Visible = false;
                            this.ShowDetails();
                            lblfessapplicable.Text = "0.00";
                            lblTotalExamFee.Text = "0.00";
                            lblFinalTotal.Text = "0.00";
                            bindcourses();

                        }
                    }
                    else
                    {
                        btnSubmit.Visible = false;
                        btnPay.Visible = false;
                        btnPrintRegSlip.Visible = false;
                        btnCancel.Visible = false;
                    }

                    //ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];

                }
                else 
                {
                    divenroll.Visible = false;
                    btnSearch.Visible = false;
                    btnClear.Visible = false;
                }


            }

        }
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

    //CHECK ACTIVITY FOR PAGE
    private bool CheckActivity()
    {
        if (Convert.ToInt32(ViewState["usertype"]) == 2)
        {
            bool ret = true;
            string sessionno = string.Empty;
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");

            ViewState["sessionno"] = sessionno;
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

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    ret = false;   // Temp Comment 
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
        else
        {
            bool ret = true;
            string sessionno = string.Empty;

            //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
            sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "MAX(SA.SESSION_NO)", "am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
            //sessionno = Session["currentsession"].ToString();
            ViewState["sessionno"] = sessionno;
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

                //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    ret = false;   // Temp Comment 
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
    }

    private void FillDropdown()
    {
        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + ViewState["ACTIVITY_NO"], "");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            
            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }
    }

    //CHECK ACTIVITY FOR EXAM REGISTRATION FOR COLLEGE
    private bool CheckActivityCollege(int cid)
    {
        bool ret = true;
        string sessionno = string.Empty;
        DataSet ds;
        if (Convert.ToInt32(ViewState["usertype"]) == 2)
        {
             ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        }
        else 
        {
             ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "BRANCHNO,SEMESTERNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]), "");
        }
        ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
        ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 AND COLLEGE_IDS="+ cid +" UNION ALL SELECT 0 AS SESSION_NO");
        //sessionno = Session["currentsession"].ToString();
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.SEMESTER like '%" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "%' AND am.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'  AND SA.STARTED = 1 AND COLLEGE_IDS like '%" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "%' AND SA.DEGREENO like '%" + Convert.ToInt32(ViewState["DEGREENO"]) + "%'  AND SA.BRANCH LIKE '%" + Convert.ToInt32(ViewState["BRANCHNO"]) + "%' UNION ALL SELECT 0 AS SESSION_NO");
        ViewState["sessionnonew"] = sessionno;
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
                //dvMain.Visible = false;
                ret = false;   // Temp Comment 
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            txtEnrollno.Text = string.Empty;
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    //STUDENT DETAILS GET BY THIS
    private void ShowDetails()
    {
        try
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            int idno = 0;
            StudentController objSC = new StudentController();
            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else
            {

                string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + ViewState["REGNO"].ToString() + "' ");

                if (REGNO != null && REGNO != string.Empty && REGNO != "")
                {
                    idno = feeController.GetStudentIdByEnrollmentNo(REGNO);

                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    txtEnrollno.Enabled = true;
                    txtEnrollno.Text = string.Empty;
                    return;

                }
            }

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
                        //lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                        ViewState["Mobile"] = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        ViewState["EMAILID"] = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        lblYear.Text = dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString();
                        Session["YEARNO"] =  dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString();
                        Session["paysemester"] = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        //int Duration = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (D.DEGREENO= CDB.DEGREENO)", "DISTINCT DURATION", "D.DEGREENO=" + hdfDegreeno.Value));
                        //Duration = Convert.ToInt32(Duration) * 2;
                        lblfessapplicable.Text = "0.00";
                        lblTotalExamFee.Text = "0.00";
                        lblFinalTotal.Text = "0.00";
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;
                        flag = false;
                      
                        txtEnrollno.Text = "";
                      
                        txtEnrollno.Enabled = true;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;
                    flag = false;

                    txtEnrollno.Text = "";
                    txtEnrollno.Enabled = true;


                }
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                divCourses.Visible = false;
                flag = false;

                txtEnrollno.Text = "";
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

    // GET OR BIND THE ALL PASS SUBJECT 
    protected void bindcourses()
    {

        int idno = 0;
        int sessionno = Convert.ToInt32(ViewState["sessionnonew"]);
        StudentController objSC = new StudentController();
        DataSet dsSubjects;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else 
        {
            idno = Convert.ToInt32(ViewState["IDNO"]);
        }
        if (idno > 0)
        {
            divCourses.Visible = true;

                int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
                int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO='" + idno + "'"));
                ViewState["clg_id"] = clg_id;
                int SESSION = Convert.ToInt32(ViewState["sessionnonew"]);

                string proc_name = "PKG_EXAM_GET_SUBJECTS_LIST_FOR_IMPROVE_EXAM_REGISTARTION";
                string para_name = "@P_IDNO,@P_SCHEMENO,@P_SESSIONNO";
                string call_values = "" + idno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + SESSION + "";
                dsSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
                if (dsSubjects.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = dsSubjects;
                    lvCourse.DataBind();
                    lvCourse.Visible = true;
                    divCourses.Visible = true;
                    //btnSubmit.Visible = true;
                    pnlCourse.Visible = true;

                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    divCourses.Visible = true;
                    objCommon.DisplayMessage("No Courses found...!!!", this.Page);
                    //btnSubmit.Visible = false;
                    pnlCourse.Visible = true;
                    btnPay.Visible = false;
                    btnSubmit.Visible = false;
                    //btnReport.Visible = false;
                    return;
                }

                //CHECK FEES APPlCABLE OR NOT 
                if (ViewState["usertype"].ToString() == "2")
                {
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "'  AND FEETYPE=6 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1"));

                    if (CheckExamfeesApplicableOrNot >= 1)
                    {
                        //CalculateTotal();//ALL Course Fees
                        lblfessapplicable.Text = "0.00";
                        decimal TotalApplicablefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "SUM(ApplicableFee)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip.ToString() + "%' AND DEGREENO LIKE '%" + hdfDegreeno.Value.ToString() + "' AND SUBID>0 AND FEETYPE=6 AND COLLEGE_ID="+ clg_id));
                        lblfessapplicable.Text = TotalApplicablefees.ToString();

                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='IEF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (paysuccess > 0)
                        {
                            decimal Amount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='IEF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                            lblFinalTotal.Text = Amount.ToString();
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                            lvCourse.Enabled = false;
                            return;
                        }
                        int Subject = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND IDNO = " + Convert.ToInt32(idno) + " AND IMPROVEMENT = 1 AND PREV_STATUS = 1 AND EXAM_REGISTERED = 1"));
                        if (Subject > 0)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            //btnShow.Visible = false;
                            //btnReport.Visible = false;
                            objCommon.DisplayMessage("Exam Registration Done. Can not proceed with the transaction !", this.Page);
                            lvCourse.Enabled = false;
                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = true;
                            return;
                        }

                        else
                        {
                            btnPrintRegSlip.Visible = false;
                            btnPay.Visible = true;
                            btnSubmit.Visible = false;
                        }
                    }
                    else
                    {

                        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                        int Subject = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND IDNO = " + Convert.ToInt32(idno) + " AND IMPROVEMENT = 1 AND PREV_STATUS = 1 AND EXAM_REGISTERED = 1"));
                        if (Subject > 0)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            //btnShow.Visible = false;
                            //btnReport.Visible = false;
                            objCommon.DisplayMessage("Exam Registration Done. Can not proceed with the transaction !", this.Page);
                            lvCourse.Enabled = false;
                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = true;
                            return;
                        }
                        btnSubmit.Visible = true;
                        btnPay.Visible = false;
                        btnPrintRegSlip.Visible = false;
                    }

                }
                else 
                {
                    int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "'  AND FEETYPE=6 AND COLLEGE_ID=" + clg_id + "  AND ISNULL(IsFeesApplicable,0)=1"));
                    if (CheckExamfeesApplicableOrNot >= 1)
                    {
                        //CalculateTotal();//ALL Course Fees
                        lblfessapplicable.Text = "0.00";
                        decimal TotalApplicablefees = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "SUM(ApplicableFee)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip.ToString() + "%' AND DEGREENO LIKE '%" + hdfDegreeno.Value.ToString() + "' AND SUBID>0 AND FEETYPE=6 AND COLLEGE_ID=" + clg_id));
                        lblfessapplicable.Text = TotalApplicablefees.ToString();

                        int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(lblSemester.ToolTip) + " AND TRANSACTIONSTATUS='Success' AND AD.RECIEPT_CODE='IEF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (paysuccess > 0)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                            lvCourse.Enabled = false;
                            return;
                        }
                        int Subject = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND IDNO = " + Convert.ToInt32(idno) + " AND IMPROVEMENT = 1 AND PREV_STATUS = 1 AND EXAM_REGISTERED = 1"));
                        if (Subject > 0)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            //btnShow.Visible = false;
                            //btnReport.Visible = false;
                            objCommon.DisplayMessage("Exam Registration Done. Can not proceed with the transaction !", this.Page);
                            lvCourse.Enabled = false;
                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = true;
                            return;
                        }
                        else
                        {
                            //btnShow.Visible = false;
                            //btnReport.Visible = false;
                           // btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = true;
                        }
                    }

                }
        }
    }

    // SEARCH STUDENT BY ADMIN SIDE 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try 
        {
            
            int idno = 0;
            int clgid = 0;

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");
            ViewState["REGNO"] = REGNO; 
            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
                ViewState["IDNO"] = idno;
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                txtEnrollno.Enabled = true;
                txtEnrollno.Text = string.Empty;
                divCourses.Visible = false;
                return;

            }
            clgid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno));

            if (CheckActivityCollege(clgid))
            {
                //btnShow.Visible = false;
                // txtEnrollno.Enabled = false;
                
                ShowDetails();
                bindcourses();

                if (IsNotActivitySem == true)
                {
                    objCommon.DisplayMessage("Activity Is Not Started For This Semester Student.", this.Page);
                    divCourses.Visible = false;
                    return;
                }
                else
                {
                    if (flag.Equals(false))
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;
                        return;
                    }
                    else
                    {
                        pnlSearch.Visible = false;
                        btnCancel.Visible = true;
                        int Subject = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(COURSENO)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND IDNO = " + Convert.ToInt32(idno) + " AND IMPROVEMENT = 1 AND PREV_STATUS = 1"));
                        if (Subject > 0)
                        {
                            btnPrintRegSlip.Visible = true;
                            btnSubmit.Visible = false;
                            btnPay.Visible = false;
                            //btnShow.Visible = false;
                            //btnReport.Visible = false;
                            objCommon.DisplayMessage("Exam Registration Done. Can not proceed with the transaction !", this.Page);
                            lvCourse.Enabled = false;
                            btnSubmit.Visible = false;
                            btnPrintRegSlip.Visible = true;
                            return;
                        }
                    }
                }
                
            }

        }catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.btnSearch_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // BIND THE STUDENT COURSE BY ADMIN SIDE 
    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    try 
    //    {

    //        bindcourses();

    //    }catch(Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    // SUBMITT SLEECTED COURSE BY ADMIN SIDE /  STUDENT SIDE.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            #region Comment
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();
            
            int idno = 0;
            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                idno = Convert.ToInt32(ViewState["IDNO"]);
            }

            objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionnonew"]);

            int cntcourse = 0;
            int A = lvCourse.Items.Count;
            if (lvCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true) //if (chk.Enabled == true)
                        cntcourse++;
                }

            }
            if (cntcourse == 0)
            {
                objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
                return;
            }
            else
            {
                if (lvCourse.Items.Count > 0)
                {
                    
                    //int idno = 0;
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        idno = Convert.ToInt32(Session["idno"]);
                    }
                    else
                    {
                        idno = Convert.ToInt32(ViewState["IDNO"]);
                    }
                    string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno);

                    objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionnonew"]);

                    objSR.IDNO = idno;
                    objSR.REGNO = Regno;
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        objSR.ROLLNO = Regno;
                    }
                    else 
                    {
                        objSR.ROLLNO = txtEnrollno.Text;
                    }
                    objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    objSR.COURSENOS = string.Empty;
                    objSR.SEMESTERNOS = string.Empty;
                    int degreenos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
                    int branchnos = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
                    //objSR.Backlogfees = Convert.ToDecimal(lblBacklogFine.Text);
                    objSR.TotalFee = objSR.Backlogfees;
                    int user = Convert.ToInt32(ViewState["usertype"]);
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        //Get Student Details from lvStudent
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        Label sem = dataitem.FindControl("lblsem") as Label;
                        if (cbRow.Checked == true && cbRow.Enabled == true)
                        {
                            objSR.COURSENOS = ((dataitem.FindControl("lblCourseName")) as Label).ToolTip;
                            objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
                            objSR.SEMESTERNO = Convert.ToInt32(ViewState["SEMESTERNO"].ToString());
                            int ret = objSRegist.AddExamRegisteredImprove_CC(objSR, user);
                            if(ret == 1)
                            {
                                objCommon.DisplayMessage("Course Registration done Sucessfully.", this.Page);
                                
                            }
                            else
                            {
                                objCommon.DisplayMessage("Course Registration Update Sucessfully.", this.Page);
                                
                            }
                        }
                    }
                    lvCourse.Enabled = false;
                    btnSubmit.Visible = false;
                    btnPrintRegSlip.Visible = true;
                    return;
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Courses", this.Page);
                    bindcourses();
                    return;
                }

            }
            #endregion 
                   
       
            

        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=6 AND ISNULL(IsFeesApplicable,0)=1"));
            if (CheckExamfeesApplicable >= 1)
            {//ListViewDataItem dataitem in lvFailCourse.ItemTemplate)
                CheckBox chckheader = (CheckBox)lvCourse.FindControl("chkAll");
                if (chckheader.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = true;
                    }

                    foreach (ListViewDataItem dataitem in lvCourse.Items)
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
                        lblfessapplicable.Text = "0.00";
                    }
                    lblFinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();

                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = false;
                        string TotalAmt = Amt.ToString();
                        lblTotalExamFee.Text = TotalAmt.ToString();
                        if (lblfessapplicable.Text == string.Empty || lblfessapplicable.Text == null)
                        {
                            lblfessapplicable.Text = "0.00";
                        }
                        //lblFinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                        lblFinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text)).ToString();
                    }

                }

            }
            else
            {
                CheckBox chckheader = (CheckBox)lvCourse.FindControl("chkAll");
                if (chckheader.Checked == true)
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = true;
                    }
                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        cbRow.Checked = false;
                        lblTotalExamFee.Text = "0.00";
                    }
                }

                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.chkAll_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // CHECK EVENT FOR SELECT SUBJECT AND CALCULATION
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " +
            Convert.ToInt32(ViewState["sessionnonew"]) + " AND SEMESTERNO LIKE '%" + lblSemester.ToolTip + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=6  AND ISNULL(IsFeesApplicable,0)=1"));
            if (CheckExamfeesApplicable >= 1)
            {
                int applycourse = 0;
                applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Convert.ToInt32(ViewState["sessionnonew"]))));
                if (applycourse > 0)
                {


                }
                else
                {
                    CheckBox litText = lvCourse.FindControl("chkAll") as CheckBox;// added for listview header True/False.

                    int count = 0;
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                        CheckBox cbRowhead = dataitem.FindControl("chkAll") as CheckBox;
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
                    if (lblTotalExamFee.Text == "0")
                    {

                        lblFinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text)).ToString();
                        Amt = 0;
                        CourseAmtt = 0;
                    }
                    else
                    {
                        if (lblfessapplicable.Text == string.Empty)
                        {
                            lblfessapplicable.Text = "0";
                        }


                        lblFinalTotal.Text = (Convert.ToDecimal(lblTotalExamFee.Text) + Convert.ToDecimal(lblfessapplicable.Text)).ToString();
                        Amt = 0;
                        CourseAmtt = 0;
                    }
                }
            }
            else
            {

                CheckBox litText = lvCourse.FindControl("chkAll") as CheckBox;
                foreach (ListViewDataItem dataitem in lvCourse.Items)
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
                objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.chkAccept_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // MAKE A PAYMENT AND APPLY FOR EXAM EVENT
    protected void btnPay_Click(object sender, EventArgs e)
    {
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        FeeCollectionController objFee = new FeeCollectionController();

        try
        {
            #region Check payment
            int ifPaidAlready = 0;
            ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionnonew"]) + " AND RECIEPT_CODE = 'IEF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0"));
            if (ifPaidAlready > 0)
            {
                objCommon.DisplayMessage("Exam Registration Fee has been paid already. Can not proceed with the transaction !", this.Page);
                return;
            }
            #endregion

            int cntcourse = 0;
            #region CHECK BOX COUNT
            if (lvCourse.Items.Count > 0)
            {

                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    if (chk.Checked == true)
                        cntcourse++;
                }

            }
            if (cntcourse == 0)
            {
                objCommon.DisplayMessage("Please Select Courses..!!", this.Page);
                return;
            }
            #endregion

            #region CREATE DEMAND
            StudentController objSC = new StudentController();
            DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(Session["idno"]));
            string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
            //objSR.SESSIONNO = Convert.ToInt32(Session["sessionnonew"]);
            string coursenos = string.Empty;
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {

                    Label courseno = dataitem.FindControl("lblCourseName") as Label;
                    coursenos += courseno.ToolTip + ",";
                }

            }
            objSR.COURSENOS = coursenos;
            objSR.IDNO = Convert.ToInt32(Session["idno"]);
            objSR.REGNO = Regno;
            if (ViewState["usertype"].ToString() == "2")
            {
                objSR.ROLLNO = Regno;
            }
            else
            {
                objSR.ROLLNO = txtEnrollno.Text;
            }
            objSR.SCHEMENO = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString());
            objSR.SEMESTERNOS = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            objSR.IPADDRESS = Session["ipAddress"].ToString(); ;
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            int user = Convert.ToInt32(ViewState["usertype"]);
            string Amt = lblFinalTotal.Text;
          
            objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionnonew"]);
            CreateStudentPayOrderId();
            //create Demand

            int ret = objSRegist.AddStudentImproveExamRegistrationDetails(objSR, Amt, ViewState["OrderId"].ToString());
            #endregion

            #region Update table 
            if (lvCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    Label sem = dataitem.FindControl("lblsem") as Label;
                    if (cbRow.Checked == true && cbRow.Enabled == true)
                    {
                        objSR.COURSENOS = ((dataitem.FindControl("lblCourseName")) as Label).ToolTip;
                        objSR.COURSENOS = objSR.COURSENOS.TrimEnd();
                        objSR.SEMESTERNO = Convert.ToInt32(ViewState["SEMESTERNO"].ToString());
                        int retn = objSRegist.AddExamRegisteredImprove_CC(objSR, user);
                        if (retn == 1)
                        {
                            objCommon.DisplayMessage("Course Registration done Sucessfully, Wait for the final approval from the Head of Department", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage("Course Registration Update Sucessfully, Wait for the final approval from the Head of Department", this.Page);
                        }
                    }
                }
            }
            #endregion 

            #region Payment
            double TotalAmount;
            int degreeno;
            int college_id;
            Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            string PaymentMode = "ONLINE FEES COLLECTION";
            Session["PaymentMode"] = PaymentMode;
            Session["studAmt"] = Amt;
            ViewState["studAmt"] = Amt;   
            TotalAmount = Convert.ToDouble(Amt);
            Session["studName"] = lblName.Text;
            Session["studPhone"] = ViewState["Mobile"].ToString();
            Session["studEmail"] = ViewState["EMAILID"].ToString();

            Session["ReceiptType"] = "IEF";
           
            Session["idno"] = Convert.ToInt32(Session["idno"].ToString());
            Session["paysession"] = Convert.ToInt32(ViewState["sessionnonew"]);
           
            Session["homelink"] = "Improv_ExamRegistration.aspx";
            Session["regno"] = lblEnrollNo.Text;
            Session["payStudName"] = lblName.Text;
            Session["paymobileno"] = ViewState["Mobile"].ToString();
            Session["Installmentno"] = "0";
            Session["Branchname"] = lblBranch.Text;
            

           
                //degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
            degreeno = 0;
            college_id =0;

                //college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));

      

            DataSet ds1 = objFee.GetOnlinePaymentConfigurationDetails_WithDegree(OrganizationId, Convert.ToInt32(Session["payid"]), Convert.ToInt32(Session["payactivityno"]), degreeno, college_id);
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 1)
                {

                }
                else
                {
                    Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    Response.Redirect(RequestUrl);
                }
            }
            #endregion

        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.btnPay_Click() --> " + ex.Message + " " + ex.StackTrace);
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

    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    int idno = 0;
    //    if (ViewState["usertype"].ToString() == "2")
    //    {
    //        idno = Convert.ToInt32(Session["idno"]);
    //    }
    //    else 
    //    {
    //        // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

    //        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

    //        if (REGNO != null && REGNO != string.Empty && REGNO != "")
    //        {
    //            idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
    //            return;
    //        }

    //    }
    //    int scheme = Convert.ToInt32(lblScheme.ToolTip);
    //    int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));
    //    ShowReport("ImproveExamRegistration", "rptImproveExam_registrationStudent.rpt");
    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO;
            if (ViewState["usertype"].ToString() == "2")
            {
                IDNO = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                IDNO = Convert.ToInt32(ViewState["IDNO"]);
            }
            // int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COLLEGE_CODE", "ORDER_ID='" + Convert.ToString(orderid) + "'"));
            // int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_CODE", "IDNO=" + IDNO));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["sessionnonew"]) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_IDNO=" + IDNO;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void ShowReportReceipt(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        int IDNO;
    //        if (ViewState["usertype"].ToString() == "2")
    //        {
    //            IDNO = Convert.ToInt32(Session["idno"]);
    //        }
    //        else
    //        {
    //            IDNO = Convert.ToInt32(ViewState["IDNO"]);
    //        }
    //        // int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COLLEGE_CODE", "ORDER_ID='" + Convert.ToString(orderid) + "'"));
    //        // int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_CODE", "IDNO=" + IDNO));
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        // url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + Convert.ToInt32(ViewState["DCR_NO"]) + ",@P_IDNO=" + IDNO;

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_Improv_ExamRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            // idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);

            string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");

            if (REGNO != null && REGNO != string.Empty && REGNO != "")
            {
                idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                return;
            }

        }
        int scheme = Convert.ToInt32(lblScheme.ToolTip);
        int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "SCHEMETYPE", "SCHEMENO =" + scheme));
        
            ShowReport("ImproveExamRegistration", "rptImproveExam_registrationStudent.rpt");
        
        
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}