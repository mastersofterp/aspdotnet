//=================================================
// CREATED : GAUEAV 
// Date : 22-06-2023
//=================================================

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

public partial class ACADEMIC_AdminExamResitApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    StudentController studCont = new StudentController();
    Exam objExam = new Exam();
    FeeCollectionController feeController = new FeeCollectionController();
    Student_Acd objSA = new Student_Acd();
    StudentRegist objSR = new StudentRegist();
    //StudentController objSC = new StudentController();
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

                    }

                    else
                    {
                        div1.Visible = false;
                        objCommon.DisplayUserMessage(this.Page, "You are not authorized to view this page.", this.Page);

                    }
                    ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];

                }
            }
            divMsg.InnerHtml = string.Empty;
            // CheckActivity(); COMMENT(1)



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }     
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.ShowDetails();
        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();
        lvFailCourse.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        btnapprove.Visible = false;
        btnapprove.Enabled = false;
        btnsubmitwithnofee.Visible = false;
        btnsubmitwithnofee.Enabled = false;
        btnreport.Visible = false;
        btnreport.Enabled = false;
        Clear_Amount();


    }
    private void ShowDetails()
    {
        int idno = 0;
        StudentController objSC = new StudentController();
        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + txtEnrollno.Text.Trim() + "' ");
        ViewState["REGNO"] = REGNO;
        if (REGNO != null && REGNO != string.Empty && REGNO != "")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(REGNO);
            ViewState["IDNO"] = idno;
            Session["idno"] = idno;
        }
        else
        {
            objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
            txtEnrollno.Enabled = true;
            txtEnrollno.Text = string.Empty;
            return;

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

                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        //lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                       // lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblYear.Text = dsStudent.Tables[0].Rows[0]["YEARNAME"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();

                        #region ADD SEMESTER dropdown

                        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT_HIST H INNER JOIN ACD_SEMESTER S ON (H.SEMESTERNO=S.SEMESTERNO)", "DISTINCT H.SEMESTERNO", "DBO.FN_DESC('SEMESTER',H.SEMESTERNO)SEMESTER", " idno =" + Convert.ToInt32(idno) + "  AND S.SEMESTERNO > 0  and isnull(CANCEL,0)=0 ", "SEMESTERNO");//AND  GDPOINT=0 
                        #endregion
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divCourses.Visible = false;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    divCourses.Visible = false;

                }
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                divCourses.Visible = false;




            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                BindCourse();
                divbtn.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("Please select Semester.", this.Page);
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                divbtn.Visible = false;
                Clear_Amount();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.ddlSemester_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                StudentController objSC = new StudentController();
                #region GET STUDENT DETALS               
                int cntcourse = 0;             

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
                    BindCourse();
                    return;
                }
                else
                {
                    int ifPaidAlready = 0;
                    ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionno"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO=" + ddlSemester.SelectedValue));
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
                                string Call_Values = "" + Idno + "," + Convert.ToInt32(ViewState["sessionno"]) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + Courseapply + ",1";

                                // return;

                                string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                                if (que_out == "-99")
                                {
                                   
                                    objCommon.DisplayMessage(updatepnl,"Something Went Wrong", this.Page);   
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
                    objSR.SESSIONNO = Convert.ToInt32(ViewState["sessionno"]);
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
                       // Disible_listview();
                        return;

                    }
                    else
                    {
                        objCommon.DisplayMessage(updatepnl, "Demand is Created for this subjects. Please go to counter for Payment Registration.", this.Page);
                       // Disible_listview();
                        return;
                    }


                }
                // return;
                    #endregion

            }


            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.btnSubmit_Click()--> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BindCourse()
    {
        try
        {
            int idno = Convert.ToInt32(ViewState["IDNO"]);
            int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DISTINCT SESSIONNO", " idno =" + Convert.ToInt32(idno) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " and isnull(CANCEL,0)=0 and EXAM_REGISTERED=1 "));
            ViewState["sessionno"] = sessionno;
            int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "DISTINCT SCHEMENO", " idno =" + Convert.ToInt32(idno) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " and isnull(CANCEL,0)=0 and EXAM_REGISTERED=1 "));
            ViewState["SCHEMENO"] = Schemeno;
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO='" + idno + "'"));
            ViewState["degreeno"] = degreeno;
            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO='" + idno + "'"));
            ViewState["branchno"] = branchno;
            string proc_name = "PKG_GET_RESIT_REGISTARTION_STUDENT_COURSES_ATLAS";
            string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO";
            string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(ViewState["SCHEMENO"]) + "";
            DataSet dsFailSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
            if (dsFailSubjects.Tables[0].Rows.Count > 0)
            {
                lvFailCourse.DataSource = dsFailSubjects;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = true;
                divCourses.Visible = true;
                pnlFailCourse.Visible = true;
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                btnreport.Visible = true;
                btnreport.Enabled = true;


                // added by gaurav for fees 




                ViewState["Semesternos"] = ddlSemester.SelectedValue;

                int CheckExamfeesApplicableOrNot = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionno"]) + " AND SEMESTERNO LIKE '%" + ddlSemester.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%'  AND FEETYPE=2    and ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));

                if (CheckExamfeesApplicableOrNot >= 1)
                {
                    CalculateTotal();

                    int paysuccess = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "COUNT(AD.idno)", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionno"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND  D.RECIEPT_CODE='REF' and  AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                    if (paysuccess > 0)
                    {
                        decimal ToalPaidAmount = Convert.ToDecimal(objCommon.LookUp("ACD_DEMAND D INNER JOIN   ACD_DCR AD ON (D.IDNO=AD.IDNO AND D.SESSIONNO=AD.SESSIONNO)", "TOP 1 AD.TOTAL_AMT", "AD.SESSIONNO= " + Convert.ToInt32(ViewState["sessionno"]) + " AND ad.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND D.RECIEPT_CODE='REF'  AND ISNULL(AD.RECON,0)=1 AND ISNULL(AD.CAN,0)=0 AND   AD.IDNO=" + Convert.ToInt32(Session["idno"])));
                     
                        lvFailCourse.Enabled = false;
                        btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;                                                                                     
                        lblfessapplicable.Text = "";                   
                        lblTotalExamFee.Text = "";
                        FinalTotal.Text = "<b style='color:green;'>PAID AMOUNT: </b> " + ToalPaidAmount.ToString();
                        int approval = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(ABSENT_LOG)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionno"]) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND  EXAM_REGISTERED=1 and isnull(cancel,0)=0 AND ABSENT_LOG=1 and isnull(prev_status,0)=0 AND IDNO=" + Convert.ToInt32(Session["idno"])));
                        if (approval > 0)
                        {
                            btnapprove.Enabled = false;
                            btnapprove.Visible = true;

                        }
                        else {

                            btnapprove.Enabled = true;
                            btnapprove.Visible = true;
                        }

                    }
                    else
                    {
                       
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                        btnCancel.Visible = true;
                        btnsubmitwithnofee.Visible = false;
                        btnsubmitwithnofee.Enabled = false;
                      // btnapprove.Visible = false;
                      // btnapprove.Enabled = false;
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                 
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;//g3
                    btnsubmitwithnofee.Visible = true;
                    btnsubmitwithnofee.Enabled = true;
                    lvFailCourse.Enabled = true;
                    objCommon.DisplayMessage(updatepnl, "Exam Fees Configuration is not complete !!", this.Page);
                   



                }






















            }
            else
            {
                objCommon.DisplayMessage("No Courses found...!! !!", this.Page);
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
                lvFailCourse.Visible = false;
                btnSubmit.Visible = false;//g4
                btnCancel.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.BindCourse() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

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
    protected void lvFailCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int ifPaidAlready = 0;
        int applycourse = 0;

        ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO =" + Convert.ToInt32(ViewState["sessionno"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0 and SEMESTERNO IN (" + ddlSemester.SelectedValue + ")"));
        if (ifPaidAlready > 0)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                HiddenField hdf = (HiddenField)e.Item.FindControl("hdfapplycourse");
                CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                HiddenField hdfapplycoursedone = (HiddenField)e.Item.FindControl("hdfapplycoursedone");
                //if (hdf.Value == "1" && hdfapplycoursedone.Value == "1")
                if (hdf.Value == "1")

                {
                    chk.Checked = true;                   
                    chkhead.Checked = false;
                    chkhead.Enabled = false;
                }
                else
                {
                    chk.Checked = false;                 
                    chkhead.Checked = false;                 

                }
            }
        }
        else
        {

            applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"])));
            if (applycourse > 0)
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkAccept");
                    HiddenField hdf = (HiddenField)e.Item.FindControl("hdfapplycourse");
                    CheckBox chkhead = lvFailCourse.FindControl("chkAll") as CheckBox;
                    HiddenField hdfapplycoursedone = (HiddenField)e.Item.FindControl("hdfapplycoursedone");

                    if (hdf.Value == "1")
                    
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
            else {
                lvFailCourse.Enabled = true;
            
            }

        }

        #region old
       //if (e.Item.ItemType == ListViewItemType.DataItem)
       //{
       //    // Get the data item for the current ListViewItem
       //    DataRowView rowView = (DataRowView)e.Item.DataItem;
       //
       //    // Find the checkbox control in the current ListViewItem
       //    CheckBox checkbox = (CheckBox)e.Item.FindControl("chkAccept");
       //
       //    // Get the flag value from the data item
       //    object flagObj = rowView["ABSENT_LOG"];
       //    int flagValue;
       //
       //    // Check if the flag value is DBNull
       //    if (flagObj != DBNull.Value)
       //    {
       //        flagValue = Convert.ToInt32(flagObj);
       //    }
       //    else
       //    {
       //        // Assign a default value if flag value is DBNull
       //        flagValue = 0; // Or any other default value you want
       //    }
       //
       //    // Disable the checkbox if the flag value is 1
       //    if (flagValue == 1)
       //    {
       //        checkbox.Enabled = false;
       //    }
       //}
        #endregion
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {

        try
        {

            int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionno"]) + " AND SEMESTERNO LIKE '%" + ddlSemester.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
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
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.chkAll_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        #region old
       // try
        //{
        //    CheckBox chckheader = (CheckBox)lvFailCourse.FindControl("chkAll");
        //    CheckBox chck = (CheckBox)lvFailCourse.FindControl("chkAccept");

        //    if (chckheader.Checked == true)
        //    {
        //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //        {
        //            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
        //            cbRow.Checked = true;
        //        }
        //    }
        //    else
        //    {
        //        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
        //        {
        //            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
        //            if (cbRow.Enabled == false)
        //            {

        //                cbRow.Checked = true;
        //            }
        //            else
        //            {

        //                cbRow.Checked = false;
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.chkAll_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objCommon.ShowError(Page, "Server Unavailable.");
        //}
        #endregion
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void Clear()
    {
        txtEnrollno.Text = string.Empty;
        lvFailCourse.DataSource = null;
        lvFailCourse.DataBind();
        lvFailCourse.Visible = false;
        btnSubmit.Visible = false;//g5
        btnCancel.Visible = false;
        divCourses.Visible = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
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
    protected void Disible_listview()
    {

        #region FOR DISABLE

        int Registered = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "count(1)", "SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"]) + "  AND IDNO=" + Convert.ToInt32(Session["idno"])));
        if (Registered > 0)
        {
            lvFailCourse.Enabled = false;
            btnSubmit.Enabled = false;

        }
        else
        {
            lvFailCourse.Enabled = true;
        }
        #endregion
    }
    protected void CalculateTotal()
    {
        lblTotalExamFee.Text = "0.00";
        lblfessapplicable.Text = "0.00";
        decimal ProFess;
        ProFess = Convert.ToDecimal(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "ISNULL(SUM(APPLICABLEFEE),0)", "SESSIONNO= " + Convert.ToInt32(ViewState["sessionno"]) + " AND SEMESTERNO LIKE '%" + ddlSemester.SelectedValue+ "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND  ISNULL(IsProFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["clg_id"])));

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
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
         
            int CheckExamfeesApplicable = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(FID)", "SESSIONNO= " +
Convert.ToInt32(ViewState["sessionno"]) + " AND SEMESTERNO LIKE '%" + ddlSemester.SelectedValue + "%' AND DEGREENO LIKE '%" + Convert.ToInt32(hdfDegreeno.Value) + "%' AND FEETYPE=2 AND ISNULL(IsFeesApplicable,0)=1 and ISNULL(CANCEL,0)=0"));
            if (CheckExamfeesApplicable >= 1)
            {
                int applycourse = 0;
                applycourse = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "COUNT(idno)", "IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SESSIONNO=" + Convert.ToInt32(Convert.ToInt32(ViewState["sessionno"]))));
                
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
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.chkAccept_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void Clear_Amount()
    {
               FinalTotal.Text=string.Empty;
                lblTotalExamFee.Text=string.Empty;
                lblfessapplicable.Text = string.Empty;
    
    }
    protected void btnsubmitwithnofee_Click(object sender, EventArgs e)
    {        
        try
        {
          
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();          
     

            #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
            if (lvFailCourse.Items.Count > 0)
            {
                int count = 0;
                int Courseapply = 0; 
                foreach (ListViewDataItem item in lvFailCourse.Items)
                {
                    CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = item.FindControl("lblCCode") as Label;
                    Label fees = item.FindControl("lblAmt") as Label;
                    Label Sem = item.FindControl("lblsem") as Label;
                    HiddenField ExistingMark = item.FindControl("hdfExistingMark") as HiddenField;

                    if (CheckId.Checked == true)
                    {
                        Courseapply = 1;
                    }
                    else
                    {
                        Courseapply = 0;
                    }
                    //if (CheckId.Checked == true)
                    //{
                        int Idno = Convert.ToInt32(Session["idno"]);
                        count++;
                         
                      //  int Courseapply = 1;
                        // double fee = fees.ToolTip;
                        if (fees.ToolTip == string.Empty)
                        {
                            fees.ToolTip = "0";
                        }

                        #region  INSERT INTO ACD_ABSENT_STUD_EXAM_REG_LOG
                        if (Idno > 0)
                        {
                            string SP_Name = "PKG_ACD_INSERT_ABSENT_STUD_EXAM_REG_LOG_NEW_WITHOUT_PAYMENT_ATLAS";
                            string SP_Parameters = "@P_IDNO, @P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_EXAMNO, @P_SUBEXAMNO, @P_UANO,@P_EXAM,@P_SUB_EXAM,@P_EXISTS_MARK,@P_STUDENT_REQUEST,@P_FEES,@P_COURSE_APPLY,@P_OUT";
                            string Call_Values = "" + Idno + "," + Convert.ToInt32(ViewState["sessionno"]) + "," + Convert.ToInt32(lblCCode.ToolTip) + "," + Convert.ToInt32(Sem.ToolTip) + "," + 0 + "," + 0 + "," + Convert.ToInt32(Session["userno"]) + "," + "0" + "," + "0" + "," + Convert.ToDouble(ExistingMark.Value) + "," + 1 + "," + Convert.ToDouble(fees.ToolTip) + "," + Courseapply + ",1";

                            // return;

                            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                            if (que_out == "-99")
                            {
                                objCommon.DisplayMessage(updatepnl,"Something Went Wrong", this.Page);
                                return;

                             //   objCommon.DisplayMessage(updatepnl, "Re Major Course Registration done Sucessfully", this.Page); 
                              //  ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                            }
                            else
                            {
                                objCommon.DisplayMessage(updatepnl, "Re Major  Course Registration done Sucessfully", this.Page);
                                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(7)').hide();});", true);
                            }
                          }

                        #endregion

                 //   }
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
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.btnsubmitwithnofee_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        
        }
    
    
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try {
           
                    string SP_Name = "PKG_UPDATE_REEXAM_REGISTRATION_STUDENT_BYADMIN";
                    string SP_Parameters = "@P_IDNO,@P_SESSIONNO,@P_SEMESTERNO,@P_OUT";
                    string Call_Values = "" + Convert.ToInt32(Session["idno"]) + "," + Convert.ToInt32(ViewState["sessionno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) +  ",0";
                    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                    if (que_out == "1")
                    {
                        objCommon.DisplayMessage(this, "RE Major Registration Done Sucessfully", this.Page);
                        btnapprove.Enabled = false;
                    }
                    else{
                    
                       objCommon.DisplayMessage(this, "Someting Went Wrong", this.Page);
                        return;
                    }


                }   
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.btnApprove_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        
        }
    
    
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {

       

            ShowReport("RESIT", "rptRESIT_Reg_CC.rpt");

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
            url += "&param=@P_COLLEGE_CODE=" + collegecode + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"]) + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AdminExamResitApproval.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}