//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : RE EXAM REGISTRATION BY STUDENT                                      
// CREATION DATE : 26-02-2013
// ADDED BY      : Sneha Doble                                           
// ADDED DATE    : 
// MODIFIED BY   : 
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

using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using System.Configuration;
using ICSharpCode.SharpZipLib.Zip;
using System.Drawing.Imaging;
using DynamicAL_v2;


public partial class ACADEMIC_EXAMINATION_Re_ExamRegistration : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Exam ObjE = new Exam();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReExam"].ToString();

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
                this.PopulateDropDownList();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                lblreExamfees.Text = objCommon.LookUp("ACD_REVAL_FEES_MASTER", "REVAL_FEES", "FEES_ID=2 AND FEETYPE='REEXAM'");
                ViewState["REVALFEES"] = lblreExamfees.Text;


                if (Session["usertype"].ToString() == "2")
                {
                    if (CheckActivity())
                    {
                        divCourses.Visible = false;
                        DivReExamReglist.Visible = false;
                        // txttransdate.Text = DateTime.Now.ToString();
                        //  txttransdate.Enabled = false;
                    }
                }
                else
                {
                    if (Session["IDNONEW"].ToString() != null)
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + Convert.ToInt32(Session["Sessionno"].ToString()), "SESSIONNO DESC");

                        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=50", "SESSIONNO DESC");
                        ddlSession.SelectedIndex = 1;
                        ddlSession.Focus();
                        Session["idno"] = Convert.ToInt32(Session["IDNONEW"].ToString());
                        ViewState["action"] = "add";
                        ViewState["idno"] = "0";
                        divNote.Visible = false;
                        divCourses.Visible = false;
                        Divpersonaldetails.Visible = false;
                        divStud.Visible = true;
                        txtRollNo.Text = string.Empty;
                        DivSubmit.Visible = false;
                        DivRegCourse.Visible = false;
                        //LoadFacultyPanel();
                        this.ShowDetails();
                        btnSubmit.Visible = false;
                        BindCourseListForReExam();
                        txtRollNo.Enabled = false;
                        divStud.Visible = false;
                        DivReExamReglist.Visible = false;
                        btnBack.Visible = false;
                    }
                }
                // }
                //if (Session["usertype"].ToString() == "2")
                //{
                //    divCourses.Visible = false;

                //}
                //else
                //{
                //    //pnlSearch.Visible = true;
                //}

                //CHECK ACTIVITY FOR EXAM REGISTRATION
                //CheckActivity();
                //
                //int idno = Convert.ToInt32(Session["idno"]);
                //fill semesterdsStudent.Tables[0].Rows[0]opdown from acd_trresult table
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
                //ddlSession.SelectedIndex = 2;
                if (Session["usertype"].ToString() == "2")
                {
                    btnSubmit.Visible = false;
                }
                else
                {
                    btnSubmit.Visible = true;
                }
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }


            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }

        divMsg.InnerHtml = string.Empty;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Focus();


        string sem = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'");

        if (sem == " " || sem == string.Empty)
        {
            sem = "0";
        }

        objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO IN (" + sem + ")", "SEMESTERNO DESC");
    }

    private bool CheckActivity()
    {
        bool ret = true;
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RevaluationRegistrationByStudent.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RevaluationRegistrationByStudent.aspx");
        }
    }

    private void ShowDetails()
    {
        string idno;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();

        try
        {
            //if (txtRollNo.Text.Trim() == string.Empty)
            //{
            //    objCommon.DisplayMessage("Please Enter Student Roll No.", this.Page);
            //    divCourses.Visible = false;
            //    return;
            //}

            idno = Session["idno"].ToString();
            //objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

            //if (string.IsNullOrEmpty(idno))
            //{
            //    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            //    divCourses.Visible = false;
            //    return;
            //}

            if (Convert.ToInt32(idno) > 0)
            {
                //DataSet dsStudent = objSReg.GetStudentReExamDetails(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), 0);

                DataSet dsStudent = objSReg.GetStudentReExamDetails(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(Session["semesterre_exam"]));

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        divCourses.Visible = true;
                        Divpersonaldetails.Visible = true;
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = "<b>Fathers/Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        lblMotherName.Text = "<b>/ </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                        lblMobile.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        txttranid.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                        txttransamount.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"].ToString().TrimEnd('.');
                        txttransdate.Text = dsStudent.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                        ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                        txtremark.Text = dsStudent.Tables[0].Rows[0]["REMARK"].ToString();
                        if (dsStudent.Tables[0].Rows[0]["DOC_PATH"].ToString() == "Blob Storage")
                        {
                            DivDocument.Visible = true;
                            lnkTransDoc.ToolTip = dsStudent.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["DOC_NAME"].ToString();
                            btnDownloadFile.Text = dsStudent.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["DOC_NAME"].ToString();
                            btnDownloadFile.ToolTip = dsStudent.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["DOC_NAME"].ToString();
                        }
                        else
                        {
                            DivDocument.Visible = false;
                        }

                        // imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        //objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT T", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "IDNO = " + idno + " AND RESULT = 'F' AND SESSIONNO =" + sessionno, "SEMESTERNO");
                        // objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT T", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "IDNO = " + idno + " AND  SESSIONNO =" + sessionno, "SEMESTERNO");


                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "No Record Found", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "No Record Found", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetailsStud()
    {
        string idno;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();

        try
        {
            idno = Session["idno"].ToString();

            if (Convert.ToInt32(idno) > 0)
            {
                DataSet dsStudent = objSReg.GetStudentReExamDetails(Convert.ToInt32(idno), 0, 0);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        DivSubmit.Visible = true;
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblFatherName.Text = "<b>Fathers/Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        lblMotherName.Text = "<b>/ </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        lblMobile.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
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

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "2")
        {
            divStud.Visible = false;
            divCourses.Visible = true;
            divNote.Visible = false;
            ShowDetailsStud();
        }
        else
        {
            divNote.Visible = false;
            divCourses.Visible = false;
            Divpersonaldetails.Visible = false;
            divStud.Visible = true;
            txtRollNo.Text = string.Empty;
            DivSubmit.Visible = false;
            DivRegCourse.Visible = false;
        }
    }

    protected void btnShowstud_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }

        Session["idno"] = idno;
        ViewState["idno"] = idno;

        if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
        {
            objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            return;
        }

        string Count = objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "Count(*)", "IDNO=" + Convert.ToInt32(idno) + " and SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        txttransdate.Text = DateTime.Now.ToShortDateString();
        txttransdate.Enabled = false;

        if (Count == string.Empty || Count == "0")
        {
            objCommon.DisplayMessage(this, "No Record found...!", this.Page);
            txtRollNo.Text = string.Empty;
            return;
        }
        else
        {
            this.ShowDetails();
            btnSubmit.Visible = false;
            ViewState["action"] = "edit";
            BindCourseListForReExam();
            txtRollNo.Enabled = false;
        }
    }

    private void BindCourseListForReExam()
    {

        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsCurrCourses = null;

        string sp_procedure = string.Empty;
        string sp_parameters = string.Empty;
        string sp_callValues = string.Empty;


        //Show Courses for Revaluation
        //dsCurrCourses = objSReg.GetStudentDetailsforReExam(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip));



        //commented on dt 07102022
        //string sp_procedure = "PKG_ACAD_GET_FAIL_COURSE_LIST_FOR_RE_EXAM_TEST";
        //string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO,@P_IDNO";
        //string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + Convert.ToInt32(Session["idno"].ToString()) + "";
        //dsCurrCourses = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

        //added by prafull on dt 07102022

        if (Convert.ToInt32(Session["usertype"].ToString()) == 2)
        {
             sp_procedure = "PKG_ACAD_GET_FAIL_COURSE_LIST_FOR_RE_EXAM_TEST";
             sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO,@P_IDNO";
             sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + Convert.ToInt32(Session["idno"].ToString()) + "";
            dsCurrCourses = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
        }
        else
        {
            //
            sp_procedure = "PKG_ACAD_GET_FAIL_COURSE_LIST_FOR_RE_EXAM_RCPIT";
            sp_parameters = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO";
            sp_callValues = "" + Convert.ToInt32(Session["idno"].ToString()) + "," + Convert.ToInt32(Session["Sessionno"]) + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + Convert.ToInt32(Session["semesterre_exam"]) + "";
            dsCurrCourses = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

           // int SESSIONNOOOO = Convert.ToInt32(Session["Sessionno"]);
           //dsCurrCourses = objSReg.GetStudentDetailsforReExam(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip));
        }


        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            int revalcount;
            DivRegCourse.Visible = true;
            //btnSubmit.Visible = true;
            lvFailCourse.DataSource = dsCurrCourses.Tables[0];
            lvFailCourse.DataBind();
            lvFailCourse.Visible = true;
            Totalamt.Visible = true;


            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                //revalcount = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"])));


                revalcount = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO="+Convert.ToInt32(Session["semesterre_exam"])+" AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"])));

              
            }
            else
            {
                revalcount = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"])));
            }

            if (revalcount > 0)
            {
                Totalamt.Visible = false;
                if (Convert.ToInt32(Session["usertype"].ToString()) == 2)
                {
                    btnprintpayslip.Visible = true;
                    btnPrintRegSlip.Visible = false;
                    TransactionDiv.Visible = true;
                    btntransactiondetails.Visible = true;
                    txttransdate.Text = DateTime.Now.ToString();
                    txttransdate.Enabled = false;
                    ShowDetailsfinalStud();
                    if (lvFailCourse.Items.Count > 0)
                    {
                        int i = 0;
                        int sum = 0;

                        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        {
                            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
                            if (dsCurrCourses.Tables[0].Rows[i]["REEXAM_AMT"].ToString() != string.Empty)
                            {
                                sum += Convert.ToInt32(dsCurrCourses.Tables[0].Rows[i]["REEXAM_AMT"]);
                                chkAccept.Enabled = false;
                            }
                            i++;
                            //for (i = 0; i < dsCurrCourses.Tables[0].Rows.Count; i++)
                            //{
                            //    if (dsCurrCourses.Tables[0].Rows[i]["TOTAL_AMT"].ToString() != string.Empty)
                            //    {
                            //        sum += Convert.ToInt32(dsCurrCourses.Tables[0].Rows[i]["TOTAL_AMT"]);
                            //        chk_reval.Enabled = false;
                            //        chk_photocopy.Enabled = false;
                            //    }
                            //}
                        }
                        txttransamount.Text = Convert.ToString(sum);
                        txttransamount.Enabled = false;
                    }
                }
                else
                {
                    foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                    {
                        CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
                        if (chkAccept.Checked == true)
                        {
                            chkAccept.Enabled = false;
                        }
                        else
                        {
                            chkAccept.Enabled = true;
                        }
                    }
                    ShowDetailsfinalStud();
                    TransactionDiv.Visible = true;
                    btnPrintRegSlip.Visible = true;
                    // btntransactiondetails.Visible = false;
                    TransactionDiv.Visible = true;
                }
            }
            else
            {
                btnPrintRegSlip.Visible = false;
                btnprintpayslip.Visible = false;
                btntransactiondetails.Visible = false;
                //TransactionDiv.Visible = false;
                btnSubmit.Visible = true;
            }
            // checkSubject();

            //if (Convert.ToInt32(dsCurrCourses.Tables[0].Rows[0]["FAIL_MORE_THAN_2_SUB"]) == 1)
            //{
            //    lblErrorMsg.Text = Session["usertype"].ToString().Equals("2") ? "Sorry you cannot apply because you are failed in more than two papers." : "Sorry this student is failed in more than two papers.";
            //}
        }

        else
        {
            DivRegCourse.Visible = false;
            btnSubmit.Visible = false;
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
            Totalamt.Visible = false;
            TransactionDiv.Visible = false;
            btntransactiondetails.Visible = false;
            btnprintpayslip.Visible = false;
            objCommon.DisplayMessage(this, "No Course found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
        }
        //  txttransdate.Text = DateTime.Now.ToString();
        //  txttransdate.Enabled = false;
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string sp_procedure = "PKG_ACD_GET_COURSE_FOR_REEXAM_OVERALL_REPORT";
            string sp_parameters = "@P_SESSIONNO";
            string sp_callValues = "" + (ddlSession.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment ; filename=ReExamStatusReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.HeaderStyle.Font.Bold = true;
                GV.HeaderStyle.Font.Name = "Times New Roman";
                GV.RowStyle.Font.Name = "Times New Roman";
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTransDoc_Click(object sender, EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReExam"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.Button)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {

                // objCommon.DisplayMessage(this, "Image not Found...", this);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";

            }
            else
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ddlsemester.Visible = true;
            string idno = string.Empty;
            if (Session["usertype"].ToString() == "2")
            {
                idno = Session["idno"].ToString();
                txtRollNo.Text = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = " + Convert.ToInt32(idno.ToString()) + "");
            }
            else
            {
                idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            }

            if (idno == "")
            {
                objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
            }
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            //   txttransdate.Text = DateTime.Now.ToShortDateString();
            //  txttransdate.Enabled = false;
            btnSubmit.Visible = false;
            ViewState["action"] = "edit";
            BindCourseListForReExam();
            txtRollNo.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.btnShow_Click1() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }

    protected void bntCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int idno;
            //int ApprovalFlag = 0;
            string retestamt = string.Empty;
            string Semesternos = string.Empty;
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();

            if (Session["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
                // ApprovalFlag = 1;
            }
            else
            {
                idno = Convert.ToInt32(Session["IDNONEW"].ToString());
            }
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = idno;
            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            objSR.COURSENOS = string.Empty;
            int status = 0;
            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;

                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        if (chk.Checked == true && chk.Enabled == true)
                        {
                            status++;
                        }
                    }
                    else
                    {
                        if (chk.Checked == true)
                        {
                            status++;
                        }
                    }
                }
            }
            else
            {
                status = -1;
            }
            //int noOfSub = 0;

            if (status == 0)
            {
                objCommon.DisplayMessage(this, "Please select atleast One subject Course list.", this.Page);
                return;
            }

            if (status > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    //Get Student Details from lvStudent
                    CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                    HiddenField hdfretest = dataitem.FindControl("hdfretest") as HiddenField;
                    hdfretest.Value = ViewState["REVALFEES"].ToString();
                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {

                        if (cbRow.Checked == true)
                        {
                            objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                            Semesternos += ((dataitem.FindControl("lblSEMSCHNO")) as Label).ToolTip + "$";
                            objSR.SUBIDS += ((dataitem.FindControl("lblsubid")) as Label).ToolTip + "$";
                            retestamt += hdfretest.Value + "$";
                        }
                        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    }
                    else
                    {
                        if (cbRow.Checked == true && cbRow.Enabled == true)
                        {
                            objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                            Semesternos += ((dataitem.FindControl("lblSEMSCHNO")) as Label).ToolTip + "$";
                            objSR.SUBIDS += ((dataitem.FindControl("lblsubid")) as Label).ToolTip + "$";
                            retestamt += "0";
                        }
                        objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    }
                }
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                Semesternos = Semesternos.TrimEnd('$');
                objSR.SUBIDS = objSR.SUBIDS.TrimEnd('$');
                retestamt = retestamt.TrimEnd('$');
                //if (noOfSub > 2)
                //{
                //    objCommon.DisplayMessage(this, "Only 2 subjects will be allow for revaluation process", this.Page);
                //    return;
                //}
                CustomStatus cs = (CustomStatus)objSRegist.AddReExamRegisteredSubjects(objSR, Semesternos, Convert.ToInt32(Session["OrgId"].ToString()), retestamt);

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(this, "You are Successfully Registration for Re Exam. Now please paid the fees & fill the Transaction Details given below.", this.Page);
                    BindCourseListForReExam();
                    btnSubmit.Visible = false;

                    if (Convert.ToInt32(Session["usertype"].ToString()) == 2)
                    {
                        btntransactiondetails.Visible = true;
                        btnprintpayslip.Visible = true;
                        btnPrintRegSlip.Visible = false;
                        btnSubmit.Visible = false;
                        TransactionDiv.Visible = true;
                    }
                    else
                    {
                        btntransactiondetails.Visible = false;
                        btnPrintRegSlip.Visible = true;
                        btnprintpayslip.Visible = false;
                        //btnSubmit.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }
    private void ShowDetailsfinalStud()
    {
        string idno;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();
        DataSet dsStudent;
        try
        {

            idno = Session["idno"].ToString();

            if (Convert.ToInt32(idno) > 0)
            {
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    dsStudent = objSReg.GetStudentReExamDetails(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["semesterre_exam"]));
                }
                else
                {
                    dsStudent = objSReg.GetStudentReExamDetails(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                }

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        TransactionDiv.Visible = true;
                        txttranid.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                        txttransamount.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"].ToString().TrimEnd('.');
                        //txttransdate.Text = dsStudent.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANS_DATE"].ToString();

                        txttransdate.Text = dsStudent.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                        ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                        txtremark.Text = dsStudent.Tables[0].Rows[0]["REMARK"].ToString();
                        if (dsStudent.Tables[0].Rows[0]["DOC_PATH"].ToString() == "Blob Storage")
                        {
                            DivDocument.Visible = true;
                            lnkTransDoc.ToolTip = dsStudent.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["DOC_NAME"].ToString();
                            btnDownloadFile.Text = dsStudent.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["DOC_NAME"].ToString();
                            btnDownloadFile.ToolTip = dsStudent.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["DOC_NAME"].ToString();
                        }
                        else
                        {
                            DivDocument.Visible = false;
                        }

                        int revalcount = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(*)", "IDNO=" + (Convert.ToInt32(idno) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue))));

                        if (revalcount > 0)
                        {
                            if (Session["usertype"].ToString().Equals("2"))     //Student 
                            {
                                if (dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString() == "1")
                                {
                                    //objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
                                    txttranid.Enabled = false;
                                    txttransamount.Enabled = false;
                                    txttransdate.Enabled = false;
                                    fuUpload.Enabled = false;
                                    btntransactiondetails.Enabled = false;
                                    btntransactiondetails.Visible = false;
                                    btnCancel.Visible = false;
                                    DivAdminapproval.Visible = true;
                                    txtremark.Enabled = false;
                                    ddlstatus.Enabled = false;
                                    Divstatus.Visible = true;
                                    btnprintpayslip.Visible = false;
                                    DocUploadDiv.Visible = false;
                                    btnPrintRegSlip.Visible = true;
                                    btnShow.Visible = false;
                                    bntCancel1.Visible = false;
                                    btnBack.Visible = false;
                                }
                                else if (dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString() == "2")
                                {
                                    // objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
                                    txttranid.Enabled = true;
                                    txttransamount.Enabled = true;
                                    txttransdate.Enabled = true;
                                    fuUpload.Enabled = true;
                                    btntransactiondetails.Enabled = true;
                                    btntransactiondetails.Visible = true;
                                    btnCancel.Visible = true;
                                    DivAdminapproval.Visible = true;
                                    txtremark.Enabled = false;
                                    ddlstatus.Enabled = false;
                                    ddlstatus.Visible = false;
                                    Divstatus.Visible = false;
                                    btnprintpayslip.Visible = false;
                                    DocUploadDiv.Visible = true;
                                    btnBack.Visible = false;
                                    txttransdate.Text = DateTime.Now.ToShortDateString();
                                    txttransdate.Enabled = false;
                                }
                                else
                                {
                                    txttranid.Enabled = true;
                                    txttransamount.Enabled = true;
                                    txttransdate.Enabled = true;
                                    fuUpload.Enabled = true;
                                    btntransactiondetails.Enabled = true;
                                    btntransactiondetails.Visible = true;
                                    btnCancel.Visible = true;
                                    DivAdminapproval.Visible = false;
                                    btnprintpayslip.Visible = true;
                                    btnBack.Visible = false;
                                    txttransdate.Text = DateTime.Now.ToShortDateString();
                                    txttransdate.Enabled = false;
                                }
                            }
                            else
                            {
                                if (dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString() == "1")
                                {
                                    DivSubmit.Visible = true;
                                    DivAdminapproval.Visible = true;
                                    DocUploadDiv.Visible = false;
                                    txttranid.Enabled = false;
                                    txttransamount.Enabled = false;
                                    txttransdate.Enabled = false;
                                    fuUpload.Enabled = false;
                                    ddlstatus.Enabled = false;
                                    txtremark.Enabled = false;
                                    btntransactiondetails.Enabled = false;
                                    btntransactiondetails.Visible = false;
                                    btnCancel.Visible = false;
                                    bntCancel1.Visible = false;
                                    btnShow.Visible = false;
                                    btnPrintRegSlip.Visible = true;
                                    ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                                    txtremark.Text = dsStudent.Tables[0].Rows[0]["REMARK"].ToString();
                                    btnBack.Visible = true;
                                }
                                else
                                {
                                    DivSubmit.Visible = true;
                                    btnShow.Visible = false;
                                    btnCancel.Visible = true;
                                    btntransactiondetails.Enabled = true;
                                    btntransactiondetails.Visible = true;
                                    btntransactiondetails.Text = "Approval";
                                    DivAdminapproval.Visible = true;
                                    DocUploadDiv.Visible = false;
                                    txttranid.Enabled = false;
                                    txttransamount.Enabled = false;
                                    txttransdate.Enabled = false;
                                    fuUpload.Enabled = false;
                                    ddlstatus.Enabled = true;
                                    txtremark.Enabled = true;
                                    btnBack.Visible = true;
                                    //btntransactiondetails.Enabled = false;
                                    //btntransactiondetails.Visible = false;
                                    bntCancel1.Visible = false;
                                    ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                                    txtremark.Text = dsStudent.Tables[0].Rows[0]["REMARK"].ToString();
                                }
                            }
                        }
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

    protected void btntransactiondetails_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = string.Empty;
            string FilePath = string.Empty;
            int ret = 0;

            if (ddlSession.SelectedIndex > 0)
            {
                ObjE.Idno = Convert.ToInt32(Session["idno"]);
                ObjE.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                ObjE.Transaction_no = txttranid.Text.Trim();
                ObjE.trans_amt = Convert.ToDecimal(txttransamount.Text.Trim());
                if (!txttransdate.Text.Trim().Equals(string.Empty)) ObjE.Transaction_date = Convert.ToDateTime(txttransdate.Text.Trim());
                ObjE.OrgId = Convert.ToInt32(Session["OrgId"]);
                ObjE.Approvedby = Convert.ToInt32(Session["userno"].ToString());
                ObjE.Approvedstatus = Convert.ToInt32(ddlstatus.SelectedValue);
                ObjE.Remark = txtremark.Text.Trim();

                if (Session["usertype"].ToString() == "2")
                {
                    int Count = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"])));
                    if (Count > 0)
                    {
                        if (fuUpload.HasFile)
                        {
                            string contentType = contentType = fuUpload.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
                            HttpPostedFile file = fuUpload.PostedFile;
                            filename = ObjE.Idno + "_retestpaymentdocuments_" + lblEnrollNo.Text.Trim() + ext;
                            ObjE.file_name = filename;
                            ObjE.file_path = "Blob Storage";

                            if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".PDF")
                            {

                                if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                {
                                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, ObjE.Idno + "_retestpaymentdocuments_" + lblEnrollNo.Text.Trim() + "", fuUpload);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }

                                    ret = Convert.ToInt32(objSReg.AddReExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"]), ViewState["ipAddress"].ToString(),Convert.ToInt32(ddlsemester.SelectedValue)));
                                    if (ret > 0)
                                    {
                                        BindCourseListForReExam();
                                        objCommon.DisplayMessage(this, "Re Exam Transaction Details Updated Sucessfully.!", this);
                                        return;
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Please Upload Transaction Receipt Below or Equal to 512 Kb only !", this);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Upload the Transaction Receipt only with following formats: .jpg, .jpeg, .pdf!", this);
                                return;
                            }
                        }
                        else
                        {
                            int DocCount = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(DOC_NAME)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"])));
                            if (DocCount > 0)
                            {
                                ObjE.file_name = btnDownloadFile.ToolTip;
                                ObjE.file_path = "Blob Storage";
                                ret = Convert.ToInt32(objSReg.AddReExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"]), ViewState["ipAddress"].ToString(),Convert.ToInt32(ddlsemester.SelectedValue)));
                                if (ret > 0)
                                {
                                    BindCourseListForReExam();
                                    objCommon.DisplayMessage(this, "Re Exam Transaction Details Updated Sucessfully. !", this);
                                    return;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Please Upload the Transaction Receipt.", this.Page);
                                return;
                            }
                        }

                    }
                    else
                    {
                        if (fuUpload.HasFile)
                        {
                            string contentType = contentType = fuUpload.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
                            HttpPostedFile file = fuUpload.PostedFile;
                            filename = ObjE.Idno + "_retestpaymentdocuments_" + lblEnrollNo.Text.Trim() + ext;
                            ObjE.file_name = filename;
                            ObjE.file_path = "Blob Storage";

                            if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".PDF")
                            {

                                if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                {
                                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, ObjE.Idno + "_retestpaymentdocuments_" + lblEnrollNo.Text.Trim() + "", fuUpload);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }

                                    ret = Convert.ToInt32(objSReg.AddReExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue)));
                                    if (ret > 0)
                                    {
                                        BindCourseListForReExam();
                                        objCommon.DisplayMessage(this, "Revaluation & Photocopy Transaction Details Updated Sucessfully. !", this);
                                        return;
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                        return;
                                    }

                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Please Upload Transaction Receipt Below or Equal to 512 Kb only !", this);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Upload the Transaction Receipt only with following formats: .jpg, .jpeg, .pdf!", this);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Please Upload the Transaction Receipt.", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    //return;
                    ObjE.file_name = btnDownloadFile.ToolTip;
                    ObjE.file_path = "Blob Storage";
                    ret = Convert.ToInt32(objSReg.AddReExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["semesterre_exam"])));
                    if (ret > 0)
                    {
                        BindCourseListForReExam();
                        objCommon.DisplayMessage(this, "Student Re Exam Transaction " + ddlstatus.SelectedItem.Text + " Sucessfully.", this);
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnprintpayslip_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Re Exam Payment Slip", "rptReExamPaymentSlip.rpt");
        }
        catch
        {
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int idno;
        //int sessionno = Convert.ToInt32(Session["currentsession"])-1 ;

        if (Session["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(Session["idno"]);
            //idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
        }

        int semesterno = Convert.ToInt32(ddlsemester.SelectedValue);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + semesterno + ",@P_SCHEMENO=" + lblScheme.ToolTip;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
                return;
            }
        }
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Re Exam Registration Slip", "rptReExamRegistrationSlip.rpt");
        }
        catch
        {
        }
    }

    protected void txttransdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txttransdate.Text) > DateTime.Now)
        {
            objCommon.DisplayMessage(this, "Please Select valid Date.", this.Page);
            txttransdate.Text = string.Empty;
            txttransdate.Focus();
            return;
        }
    }

    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();

        string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
        string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
        string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReExam"].ToString();

        StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
        CloudBlobClient client = acc.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        CloudBlob blob = container.GetBlobReference(filename);
        MemoryStream ms = new MemoryStream();
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        blob.DownloadToStream(ms);
        Response.ContentType = blob.Properties.ContentType;
        Response.AddHeader("Content-Disposition", "Attachment; filename=" + filename.ToString());
        Response.AddHeader("Content-Length", blob.Properties.Length.ToString());
        Response.BinaryWrite(ms.ToArray());
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
         

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    protected void bntCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lnkrollno_Click(object sender, EventArgs e)
    {
        LinkButton lnkrollno = sender as LinkButton;
        string idno = lnkrollno.CommandName;

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + lnkrollno.Text.Trim() + "]", this.Page);
        }

        Session["idno"] = idno;
        ViewState["idno"] = idno;

        if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
        {
            objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            return;
        }

        string Count = objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "Count(*)", "IDNO=" + Convert.ToInt32(idno) + " and SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Count == string.Empty || Count == "0")
        {
            objCommon.DisplayMessage(this, "No Record found...!", this.Page);
            txtRollNo.Text = string.Empty;
            return;
        }
        else
        {
            this.ShowDetails();
            btnSubmit.Visible = false;
            ViewState["action"] = "edit";
            BindCourseListForReExam();
            txtRollNo.Enabled = false;
            divStud.Visible = false;
            DivReExamReglist.Visible = false;
        }
    }

    private void LoadFacultyPanel()
    {
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet dsCERT = objCommon.DynamicSPCall_Select("PKG_ACD_GET_REEXAM_STUD_LIST", "@P_SESSIONNO", "" + Convert.ToInt32(ddlSession.SelectedValue));
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvlstudlist.DataSource = dsCERT;
                lvlstudlist.DataBind();
                DivReExamReglist.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                lvlstudlist.DataSource = null;
                lvlstudlist.DataBind();
                DivReExamReglist.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updLists, "Please Select Session", this.Page);
            return;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void txttranid_TextChanged(object sender, EventArgs e)
    {
        string trans_no = txttranid.Text;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS", "COUNT(1)", "TRANSACTION_NO='" + trans_no + "'"));
        if (count > 0)
        {
            objCommon.DisplayMessage("Transcation_id already Exists....", this.Page);
            txttranid.Text = string.Empty;
            return;
        }
    }
    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        TransactionDiv.Visible = false;
        btntransactiondetails.Visible = false;
        btnprintpayslip.Visible = false;

        string sp_procedure = "PKG_ACTIVITY_CHECK_ACTIVITY_TEST";
        string sp_parameters = "@P_SESSIONNO,@P_UA_TYPE,@P_PAGE_LINK,@P_SEMESTERNO";
        string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["usertype"]) + "," + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "";

        DataSet dsCurrCourses = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

        //dsCurrCourses = objSReg.GetStudentDetailsforReExam(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblScheme.ToolTip));

        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has not been Started for Selected Semester..Contact Admin !!", this.Page);
            return;
        }

        //, 
    }
}