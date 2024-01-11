//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REVALUATION REGISTRATION BY STUDENT                                      
// CREATION DATE : 26-02-2013
// ADDED BY      : SANJAY S RATNAPARKHI                                             
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

public partial class ACADEMIC_RevaluationRegistrationByStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Exam ObjE = new Exam();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
    ActivityController objActController = new ActivityController();


    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReVal"].ToString();

    //string revalday = System.Configuration.ConfigurationManager.AppSettings["revaluation"].ToString();
    // string revalday = "15";

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
                this.CheckPageAuthorization();
                this.PopulateDropDownList();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //CHECK THE STUDENT LOGIN
                //string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                //Session["usertype"] = ua_type;

                lblrevalfees.Text = objCommon.LookUp("ACD_REVAL_FEES_MASTER", "REVAL_FEES", "FEES_ID=1 AND FEETYPE='REVAL'");
                ViewState["REVALFEES"] = lblrevalfees.Text;
                lblphotofees.Text = objCommon.LookUp("ACD_REVAL_FEES_MASTER", "PHOTOCOPY_FEES", "FEES_ID=1 AND FEETYPE='REVAL'");
                ViewState["PHOTOFEES"] = lblphotofees.Text;
                totalfees.Text = Convert.ToString(Convert.ToDecimal(lblrevalfees.Text) + Convert.ToDecimal(lblphotofees.Text));

                //if (CheckActivity())
                //{
                if (Session["usertype"].ToString() == "2")
                {
                    if (CheckActivity())
                    {
                        divCourses.Visible = false;
                    }
                }
                else
                {
                    if (Session["IDNONEW"].ToString() != null)
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + Convert.ToInt32(Session["Sessionno"].ToString()), "SESSIONNO DESC");
                        ddlSession.SelectedIndex = 1;
                        ddlSession.Focus();
                        Session["idno"] = Convert.ToInt32(Session["IDNONEW"].ToString());
                        ViewState["idno"] = Convert.ToInt32(Session["IDNONEW"].ToString());
                        this.ShowDetails();
                        btnSubmit.Visible = false;

                        // btnPrintRegSlip.Visible = false;
                        ViewState["action"] = "edit";
                        FillSemester();
                        ddlBackLogSem.Enabled = true;
                        if (ddlBackLogSem.Items.Count == 2)
                        {
                            ddlBackLogSem.SelectedIndex = 1;
                            BindCourseListForReval();
                            //checkSubject();
                            IsRevaluationApproved();
                        }
                        else
                        {
                            lvFailCourse.DataSource = null;
                            lvFailCourse.DataBind();
                        }
                        divNote.Visible = false;
                        divStud.Visible = false;
                    }
                    //LoadFacultyPanel();
                }
                //}
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

                //btnSubmit.Visible = false;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }


            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }

        divMsg.InnerHtml = string.Empty;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    //private void LoadFacultyPanel()
    //{

    //    divCourses.Visible = false;
    //    divStud.Visible = true;
    //    txtRollNo.Text = string.Empty;
    //}

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        ddlSession.Focus();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int idno;
            int ApprovalFlag = 0;
            string revalstatus = string.Empty;
            string Photocopystatus = string.Empty;
            string revalamt = string.Empty;
            string Photocopyamt = string.Empty;
            StudentRegistration objSRegist = new StudentRegistration();
            StudentRegist objSR = new StudentRegist();

            if (Session["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
                ApprovalFlag = 1;
            }
            else
            {
                //idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
                idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + lblEnrollNo.Text.Trim() + "'"));
            }


            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = idno;
            objSR.SEMESTERNO = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            objSR.IPADDRESS = ViewState["ipAddress"].ToString();
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);

            objSR.COURSENOS = string.Empty;
            int status = 0;
            //int dstatus = 0;

            if (lvFailCourse.Items.Count > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    // CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                    CheckBox chk_reval = dataitem.FindControl("chk_reval") as CheckBox;
                    CheckBox chk_photocopy = dataitem.FindControl("chk_photocopy") as CheckBox;
                    //if (chk.Checked == true)
                    if (chk_photocopy.Checked == true)
                    {
                        status++;
                    }
                    if (chk_reval.Checked == true)
                    {
                        status++;
                        // count++;
                    }
                }

                //if (count <= 3)
                //{

                //}
                //else
                //{
                //    objCommon.DisplayMessage(this.updLists, "Please Select only 3 Subjects for Revaluation !! You Selected : " + count + " Subjects !!", this.Page);
                //    return;
                //}
            }
            else
            {
                status = -1;
            }
            //int noOfSub = 0;

            if (status == 0)
            {
                objCommon.DisplayMessage(this, "Please select atleast One subject from Revaluation Course list.", this.Page);
                return;
            }

            if (status > 0)
            {
                foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                {
                    //Get Student Details from lvStudent
                    //CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;

                    CheckBox chk_reval = dataitem.FindControl("chk_reval") as CheckBox;
                    CheckBox chk_photocopy = dataitem.FindControl("chk_photocopy") as CheckBox;
                    HiddenField hdfreval = dataitem.FindControl("hdfreval") as HiddenField;
                    hdfreval.Value = ViewState["REVALFEES"].ToString();
                    HiddenField hdfphotocopy = dataitem.FindControl("hdfphotocopy") as HiddenField;
                    hdfphotocopy.Value = ViewState["PHOTOFEES"].ToString();

                    //if (cbRow.Checked == true)
                    //{
                    //    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                    //    objSR.CCODES += ((dataitem.FindControl("lblCCode")) as Label).Text + "$";
                    //    objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                    //   // noOfSub++;
                    //}

                    if (chk_reval.Checked == true || chk_photocopy.Checked == true)
                    {
                        if (Session["usertype"].ToString() == "2")
                        {
                            int count = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(*)", "IDNO=" + idno + " and SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(((dataitem.FindControl("lblCCode")) as Label).ToolTip) + "AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));

                            if (count > 0)
                            {

                            }
                            else
                            {


                                objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                                objSR.CCODES += ((dataitem.FindControl("lblCCode")) as Label).Text + "$";
                                objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                                // noOfSub++;
                                if (chk_reval.Checked == true)
                                {
                                    revalstatus += 1 + "$";
                                    revalamt += hdfreval.Value + "$";

                                }
                                else
                                {
                                    revalstatus += 0 + "$";
                                    revalamt += 0 + "$";
                                }
                                if (chk_photocopy.Checked == true)
                                {
                                    Photocopystatus += 1 + "$";
                                    Photocopyamt += hdfphotocopy.Value + '$';
                                }
                                else
                                {
                                    Photocopystatus += 0 + "$";
                                    Photocopyamt += 0 + "$";
                                }
                            }
                        }
                        else
                        {
                            objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                            objSR.CCODES += ((dataitem.FindControl("lblCCode")) as Label).Text + "$";
                            objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                            // noOfSub++;
                            if (chk_reval.Checked == true)
                            {
                                revalstatus += 1 + "$";
                                revalamt += hdfreval.Value + "$";

                            }
                            else
                            {
                                revalstatus += 0 + "$";
                                revalamt += 0 + "$";
                            }
                            if (chk_photocopy.Checked == true)
                            {
                                Photocopystatus += 1 + "$";
                                Photocopyamt += hdfphotocopy.Value + '$';
                            }
                            else
                            {
                                Photocopystatus += 0 + "$";
                                Photocopyamt += 0 + "$";
                            }
                        }

                        //int count = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(((dataitem.FindControl("lblCCode")) as Label).ToolTip) + "AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));

                        //if (count > 0)
                        //{

                        //}
                        //else
                        //{


                        //    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                        //    objSR.CCODES += ((dataitem.FindControl("lblCCode")) as Label).Text + "$";
                        //    objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                        //    // noOfSub++;
                        //    if (chk_reval.Checked == true)
                        //    {
                        //        revalstatus += 1 + "$";
                        //        revalamt += hdfreval.Value + "$";

                        //    }
                        //    else
                        //    {
                        //        revalstatus += 0 + "$";
                        //        revalamt += 0 + "$";
                        //    }
                        //    if (chk_photocopy.Checked == true)
                        //    {
                        //        Photocopystatus += 1 + "$";
                        //        Photocopyamt += hdfphotocopy.Value + '$';
                        //    }
                        //    else
                        //    {
                        //        Photocopystatus += 0 + "$";
                        //        Photocopyamt += 0 + "$";
                        //    }
                        //}
                    }
                    else
                    {
                        //if (Session["usertype"].ToString() == "1")
                        //{
                        //    objSR.COURSENOS += ((dataitem.FindControl("lblCCode")) as Label).ToolTip + "$";
                        //    objSR.CCODES += ((dataitem.FindControl("lblCCode")) as Label).Text + "$";
                        //    objSR.EXTERMARKS += ((dataitem.FindControl("lblExtermark")) as Label).Text + "$";
                        //    // noOfSub++;
                        //    if (chk_reval.Checked == true)
                        //    {
                        //        revalstatus += 1 + "$";
                        //        revalamt += hdfreval.Value + "$";

                        //    }
                        //    else
                        //    {
                        //        revalstatus += 0 + "$";
                        //        revalamt += 0 + "$";
                        //    }
                        //    if (chk_photocopy.Checked == true)
                        //    {
                        //        Photocopystatus += 1 + "$";
                        //        Photocopyamt += hdfphotocopy.Value + '$';
                        //    }
                        //    else
                        //    {
                        //        Photocopystatus += 0 + "$";
                        //        Photocopyamt += 0 + "$";
                        //    }

                        //}
                    }


                    objSR.SCHEMENO = Convert.ToInt32((dataitem.FindControl("lblCourseName") as Label).ToolTip);

                    //  lblSEMSCHNO
                    //if (chk_reval.Checked == true && chk_photocopy.Checked == true)
                    //{
                    //    revalstatus = 3;  
                    //}
                }

                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
                objSR.CCODES = objSR.CCODES.TrimEnd('$');
                objSR.EXTERMARKS = objSR.EXTERMARKS.TrimEnd('$');
                revalstatus = revalstatus.TrimEnd('$');
                Photocopystatus = Photocopystatus.TrimEnd('$');
                revalamt = revalamt.TrimEnd('$');
                Photocopyamt = Photocopyamt.TrimEnd('$');
                //if (noOfSub > 2)
                //{
                //    objCommon.DisplayMessage(this, "Only 2 subjects will be allow for revaluation process", this.Page);
                //    return;
                //}
                CustomStatus cs = (CustomStatus)objSRegist.AddRevalautionRegisteredSubjects(objSR, ApprovalFlag, revalstatus, Photocopystatus, Convert.ToInt32(Session["OrgId"].ToString()), revalamt, Photocopyamt);

                if (cs == CustomStatus.RecordSaved)
                {
                    objCommon.DisplayMessage(this, "Student Revaluation & Photocopy Registration Successfully done!!", this.Page);
                    BindCourseListForReval();
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
                        btnSubmit.Visible = true;
                    }

                    //ShowReport("RevaluationRegistrationSlip", "rptRevaluation.rpt");
                }
            }

            #region FOR Demand Creadtion for revaluation commented of now by Sneha G. on 30/03/2022
            //COUNT THE HOW MANY SUBJECTS
            //status = dstatus + status + 1;
            //if (status > 0)
            //{

            //    int branchno = Convert.ToInt32(lblBranch.ToolTip);
            //    int admbatch = Convert.ToInt32(lblAdmBatch.ToolTip);
            //    int degreeno = Convert.ToInt32(hdfDegreeno.Value);
            //    int categoryno = Convert.ToInt32(hdfCategory.Value);
            //    //int SubCount;
            //    if (categoryno == 0)
            //    {
            //        categoryno = 4;
            //    }
            //    //    //= Convert.ToInt32(lvFailCourse.Items.Count.ToString());
            //    //    if (status > 3)
            //    //    {
            //    //        SubCount = 2;
            //    //    }
            //    //    else
            //    //    {
            //    //        SubCount = 1;
            //    //    }
            //    int semesterno = Convert.ToInt32(lblSemester.ToolTip);
            //    double ExamAmt = Convert.ToDouble(500 * noOfSub);
            //    //double ExamAmt = Convert.ToDouble(objCommon.LookUp("ACD_EXAM_FEES", "AMOUNT", "DEGREENO='" + degreeno + "' AND CATEGORYNO='" + categoryno + "' AND SUB_LIMIT_NO='" + SubCount + "' AND SESSIONNO=" + objSR.SESSIONNO));
            //    int studentIDs = idno;
            //    bool overwriteDemand = false;

            //    string receiptno = this.GetNewReceiptNo();
            //    FeeDemand dcr = this.GetDcrCriteria();
            //    //SELECT *  FROM ACD_DCR WHERE IDNO=5333 AND SESSIONNO=61  AND RECIEPT_CODE='EFR' AND SEMESTERNO=3
            //    string dcritem = string.Empty;
            //    double CalLateExmAmt = 0;
            //    dcritem = dmController.CreateDcrForBacklogStudents(studentIDs, dcr, Convert.ToInt32(ddlBackLogSem.SelectedValue), overwriteDemand, receiptno, ExamAmt, CalLateExmAmt);

            //    if (dcritem != "-99")
            //    {
            //        objCommon.DisplayMessage(this, "Record Saved Successfully", this.Page);
            //        ddlBackLogSem.SelectedIndex = 0;
            //        lvFailCourse.DataSource = null;
            //        lvFailCourse.DataBind();
            //        btnSubmit.Visible = false;
            //    }
            //    // string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SEMESTERNO=" + semesterno + " AND SESSIONNO=" + objSR.SESSIONNO);
            //}

            #endregion

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
        // int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;
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

            //idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");

            //if (string.IsNullOrEmpty(idno))
            //{
            //    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            //    divCourses.Visible = false;
            //    return;
            //}
            idno = Session["idno"].ToString();
            if (Convert.ToInt32(idno) > 0)
            {
                //DataSet dsStudent = objSReg.GetStudentDetailsForRevaluationExam(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), 0);

                DataSet dsStudent = objCommon.DynamicSPCall_Select("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_REVAL_REGISTRATION", "@P_IDNO,@P_SESSIONNO, @P_SEMESTERNO", "" + Convert.ToInt32(idno) + "," + Convert.ToInt32(ddlSession.SelectedValue) + ",0 ");

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
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblMobile.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        txttranid.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                        txttransamount.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"].ToString().TrimEnd('.');
                        txttransdate.Text = dsStudent.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                        ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString();
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
        //int sessionno = Convert.ToInt32(Session["currentsession"])-1;
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

            //idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            idno = Session["idno"].ToString();

            //if (string.IsNullOrEmpty(idno))
            //{
            //    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
            //    divCourses.Visible = false;
            //    return;
            //}

            if (Convert.ToInt32(idno) > 0)
            {
                //DataSet dsStudent =  objSReg.GetStudentDetailsExam(Convert.ToInt32(idno),0,0);
                DataSet dsStudent = objCommon.DynamicSPCall_Select("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_REVAL_REGISTRATION", "@P_IDNO,@P_SESSIONNO, @P_SEMESTERNO", "" + Convert.ToInt32(idno) + ", 0, 0");

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        DivSubmit.Visible = true;
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        // lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        // lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";
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
                        hdfCategory.Value = dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                        hdfDegreeno.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        // lblemail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                        lblMobile.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                        // imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

                        //objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT T", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "IDNO = " + idno + "  AND SESSIONNO =" + sessionno, "SEMESTERNO");

                        //objCommon.FillDropDownList(ddlBackLogSem, "ACD_TRRESULT T", "SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTERNAME", "IDNO = " + idno + "  AND SESSIONNO <" +Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");

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
            FillSemester();
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
            //FillSemester();

        }
    }

    protected void btnShow_Click1(object sender, EventArgs e)
    {
        //int sessionno=Convert.ToInt32(Session["currentsession"])-1;
        try
        {

            #region commented by Sneha G.
            //string chkpublish = string.Empty;
            // chkpublish = objCommon.LookUp("RESULT_PROCESS_LOG", "isnull(RES_DECL_STATUS,0)RES_DECL_STATUS", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO = " + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO = " + Convert.ToInt32(ddlBackLogSem.SelectedValue));
            // if (chkpublish == "0" || chkpublish == "")
            // {
            //     objCommon.DisplayMessage(this,"Result Not Published yet!!", this.Page);
            // }
            // else
            // {
            //     if (Session["usertype"].ToString() == "2")
            //     {

            //         DateTime today = DateTime.Now;
            //         //DateTime publishdate = Convert.ToDateTime(objCommon.LookUp("PUBLISH_DATA", "DISTINCT PUBLISH_DATE", "SESSIONNO=" + (Convert.ToInt32(Session["currentsession"].ToString())).ToString() + " AND SCHEMENO=" + lblScheme.ToolTip + " AND SEMESTERNO=" + ddlBackLogSem.SelectedValue + " AND IDNO=" + Session["idno"].ToString()));
            //         //DateTime publishdate = Convert.ToDateTime(objCommon.LookUp("RESULT_PROCESS_LOG", "DISTINCT res_decl_date", "SESSIONNO=" + (Convert.ToInt32(Session["currentsession"].ToString())).ToString() + " AND SCHEMENO=" + lblScheme.ToolTip + " AND SEMESTERNO=" + ddlBackLogSem.SelectedValue));
            //         string publishdate = objCommon.LookUp("RESULT_PROCESS_LOG", "DISTINCT res_decl_date", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + lblScheme.ToolTip + " AND SEMESTERNO=" + ddlBackLogSem.SelectedValue);
            //         if(publishdate == "")
            //        // if (publishdate == DateTime.MinValue || publishdate.ToString() == DBNull.Value.ToString()|| publishdate.ToString() == "")
            //         {
            //            objCommon.DisplayMessage("Result Not Published Yet!!", this.Page);
            //         }
            //         else
            //         {
            //             if (Convert.ToInt32((today - Convert.ToDateTime(publishdate)).TotalDays) <= Convert.ToInt32(revalday))
            //             {
            //                 this.showCourses();
            //             }
            //             else
            //             {
            //                 objCommon.DisplayMessage("Revaluation Registration Date is Closed. Please Contact Examination Section", this.Page);
            //                 divNote.Visible = true;
            //                 divCourses.Visible = false;
            //             }
            //         }

            //     }
            //     else
            //         this.showCourses();
            // }
            #endregion

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
            // this.ShowDetails();
            btnSubmit.Visible = false;
            // btnPrintRegSlip.Visible = false;
            ViewState["action"] = "edit";
            //FillSemester();
            ddlBackLogSem.Enabled = true;
            if (ddlBackLogSem.Items.Count >= 1)
            {
                ///ddlBackLogSem.SelectedIndex = 1;
                BindCourseListForReval();
                //checkSubject();
                IsRevaluationApproved();
            }
            else
            {
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
            }
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

    private void FillSemester()
    {
        objCommon.FillDropDownList(ddlBackLogSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO=S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()), "SR.SEMESTERNO");
    }

    private void IsRevaluationApproved()
    {
        //string ApproveStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "(ISNULL(REV_APPROVE_STAT,0)=1 OR ISNULL(REV_APPROVE_STAT,0)=1) AND APP_TYPE = 'REVAL' AND ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue));

        string ApproveStatus = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(CCODE)", "(ISNULL(REV_APPROVE_STAT,0)=1 OR ISNULL(REV_APPROVE_STAT,0)=2) AND ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue));
        if (ApproveStatus != "0")
        {
            btnPrintRegSlip.Visible = true;
        }
        else
        {
            btnPrintRegSlip.Visible = false;
        }
    }

    public void showCourses()
    {
        //Fail subjects List
        int idno;
        //temp solution given because current session will be define in default page
        int sessionno = Convert.ToInt32(Session["currentsession"]) - 1;
        StudentController objSC = new StudentController();
        DataSet dsFailSubjects;
        DataSet dsDetainedStudent = null;

        if (Session["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'"));
        }

        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
        string checks = objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(REVALNO)", "IDNO=" + idno + " AND SESSIONNO=" + sessionno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip));
        if (checks == "2")
        {
            objCommon.DisplayMessage("Selected Semester Revaluation Already Done", this.Page);
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            return;
        }
        dsFailSubjects = objSC.GetStudentFailExamSubjects_For_Revalution(idno, sessionno, semesterno);
        if (dsFailSubjects.Tables[0].Rows.Count > 0)
        {
            lvFailCourse.DataSource = dsFailSubjects;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = true;
            btnSubmit.Visible = true;
            // checkSubject();
        }
        else
        {
            lvFailCourse.DataSource = null;
            lvFailCourse.DataBind();
            lvFailCourse.Visible = false;
        }

        string check = objCommon.LookUp("ACD_TRRESULT", "count(IDNO)", "SEMESTERNO = " + semesterno + " AND IDNO=" + idno + " AND PASSFAIL='FAIL IN AGGREGATE'");

        if (check != "0")
        {
            dsDetainedStudent = objSC.GetStudentDetained(idno, sessionno, semesterno);
            if (dsDetainedStudent.Tables[0].Rows.Count > 0)
            {
                //lvFailInaggre.DataSource = dsDetainedStudent;
                //lvFailInaggre.DataBind();
                //lvFailInaggre.Visible = true;
            }
        }
        else
        {
            //lvFailInaggre.DataSource = null;
            //lvFailInaggre.DataBind();
            //lvFailInaggre.Visible = false;
        }

        btnSubmit.Visible = true;
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
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
            }
        }
        return receiptNo;
    }

    private FeeDemand GetDcrCriteria()
    {
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
        {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) - 1;
            dcrCriteria.ReceiptTypeCode = "EFR";
            dcrCriteria.BranchNo = Convert.ToInt32(lblBranch.ToolTip);
            dcrCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            dcrCriteria.PaymentTypeNo = 1;
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.GetDcrCriteria() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
            }
        }
        return dcrCriteria;
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        try
        {
            demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) - 1;
            demandCriteria.ReceiptTypeCode = "EFR";
            demandCriteria.BranchNo = int.Parse(lblBranch.ToolTip);
            demandCriteria.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
            demandCriteria.PaymentTypeNo = 6;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Session["usertype"].ToString() == "1")
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_RevaluationRegistrationByStudent.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server Unavailable.");
            }
            else
            {
                objCommon.DisplayMessage("Transaction Failed...", this.Page);
            }
        }
        return demandCriteria;
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

        string Count = objCommon.LookUp("ACD_REVAL_RESULT", "Count(*)", "IDNO=" + Convert.ToInt32(idno) + " and SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
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
            // btnPrintRegSlip.Visible = false;
            ViewState["action"] = "edit";
            //FillSemester();
            ddlBackLogSem.Enabled = true;
            if (ddlBackLogSem.Items.Count >= 1)
            {
                // ddlBackLogSem.SelectedIndex = 1;
                BindCourseListForReval();
                //checkSubject();
                IsRevaluationApproved();
            }
            else
            {
                lvFailCourse.DataSource = null;
                lvFailCourse.DataBind();
            }
            txtRollNo.Enabled = false;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //divCourses.Visible = false;
        DivRegCourse.Visible = false;
        Divpersonaldetails.Visible = false;
        txtRollNo.Enabled = true;
        ddlBackLogSem.SelectedIndex = 0;
        txtRollNo.Text = string.Empty;
        btnSubmit.Visible = false;
        DivAdminapproval.Visible = false;
        TransactionDiv.Visible = false;
        btnSubmit.Visible = false;
        btntransactiondetails.Visible = false;
        btnPrintRegSlip.Visible = false;
        btnprintpayslip.Visible = false;
        btnSubmit.Visible = false;
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void checkSubject()
    {
        //int sessionno = (Convert.ToInt32(Session["currentsession"]) - 1);
        int sessionno = (Convert.ToInt32(ddlSession.SelectedValue));
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_REVAL_RESULT", "COURSENO", "idno", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " and sessionno=" + sessionno + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip), "courseno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            // int i = 0;
            foreach (ListViewDataItem item in lvFailCourse.Items)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                    //CheckBox chk_reval = item.FindControl("chk_reval") as CheckBox;
                    //CheckBox chk_photocopy = item.FindControl("chk_photocopy") as CheckBox;

                    if (chkAccept.ToolTip == ds.Tables[0].Rows[i]["courseno"].ToString())
                    {
                        chkAccept.Checked = true;
                        i++;
                    }
                }
            }
        }
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Revaluation Registration Slip", "rptRevaluation.rpt");
        }
        catch
        {
        }
    }

    protected void btnprintpayslip_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Revaluation Registration Slip", "rptRevaluationwithpayment.rpt");
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

        int semesterno = Convert.ToInt32(ddlBackLogSem.SelectedValue);
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

    protected void lnkTransDoc_Click(object sender, EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReVal"].ToString();

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

    protected void chk_reval_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = sender as CheckBox;
            int CourseCount_reval = 0;
            int CourseCount_photo = 0;
            decimal revalamt = 0;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {
                CheckBox chk_reval = dataitem.FindControl("chk_reval") as CheckBox;
                HiddenField hdfreval = dataitem.FindControl("hdfreval") as HiddenField;
                hdfreval.Value = ViewState["REVALFEES"].ToString();
                CheckBox chk_photocopy = dataitem.FindControl("chk_photocopy") as CheckBox;
                HiddenField hdfphotocopy = dataitem.FindControl("hdfphotocopy") as HiddenField;
                hdfphotocopy.Value = ViewState["PHOTOFEES"].ToString();

                if (chk_reval.Checked == true)
                {
                    CourseCount_reval++;
                }

                if (chk_photocopy.Checked == true)
                {
                    CourseCount_photo++;
                }

                revalamt = (Convert.ToDecimal(hdfreval.Value.ToString()) * CourseCount_reval) + (Convert.ToDecimal(hdfphotocopy.Value.ToString()) * CourseCount_photo);
            }
            txttotalamt.Text = Convert.ToString(Convert.ToDecimal(revalamt));
        }
        catch
        {
        }

    }

    protected void chk_photocopy_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = sender as CheckBox;
            int CourseCount_reval = 0;
            int CourseCount_photo = 0;
            decimal revalamt = 0;
            foreach (ListViewDataItem dataitem in lvFailCourse.Items)
            {

                CheckBox chk_photocopy = dataitem.FindControl("chk_photocopy") as CheckBox;
                HiddenField hdfphotocopy = dataitem.FindControl("hdfphotocopy") as HiddenField;
                hdfphotocopy.Value = ViewState["PHOTOFEES"].ToString();

                CheckBox chk_reval = dataitem.FindControl("chk_reval") as CheckBox;
                HiddenField hdfreval = dataitem.FindControl("hdfreval") as HiddenField;
                hdfreval.Value = ViewState["REVALFEES"].ToString();

                if (chk_photocopy.Checked == true)
                {
                    CourseCount_photo++;
                }
                if (chk_reval.Checked == true)
                {
                    CourseCount_reval++;
                }
                revalamt = (Convert.ToDecimal(hdfphotocopy.Value) * CourseCount_photo) + (Convert.ToDecimal(hdfreval.Value) * CourseCount_reval);
            }
            txttotalamt.Text = Convert.ToString(Convert.ToDecimal(revalamt));
        }
        catch
        {
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
                ObjE.SemesterNo = Convert.ToInt32(ddlBackLogSem.SelectedValue);
                ObjE.Transaction_no = txttranid.Text.Trim();
                ObjE.trans_amt = Convert.ToDecimal(txttransamount.Text.Trim());
                if (!txttransdate.Text.Trim().Equals(string.Empty)) ObjE.Transaction_date = Convert.ToDateTime(txttransdate.Text.Trim());
                ObjE.OrgId = Convert.ToInt32(Session["OrgId"]);
                ObjE.Approvedby = Convert.ToInt32(Session["userno"].ToString());
                ObjE.Approvedstatus = Convert.ToInt32(ddlstatus.SelectedValue);
                ObjE.Remark = txtremark.Text.Trim();

                if (Session["usertype"].ToString() == "2")
                {
                    int Count = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(*)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
                    if (Count > 0)
                    {

                        if (fuUpload.HasFile)
                        {
                            string contentType = contentType = fuUpload.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
                            HttpPostedFile file = fuUpload.PostedFile;
                            filename = ObjE.Idno + "_revaluationpaymentdocuments_" + lblEnrollNo.Text.Trim() + ext;
                            ObjE.file_name = filename;
                            ObjE.file_path = "Blob Storage";

                            if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".PDF")
                            {

                                if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                {
                                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, ObjE.Idno + "_revaluationpaymentdocuments_" + lblEnrollNo.Text.Trim() + "", fuUpload);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }

                                    ret = Convert.ToInt32(objSReg.AddRevalTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                                    if (ret > 0)
                                    {
                                        BindCourseListForReval();
                                        objCommon.DisplayMessage(this, "Revaluation & Photocopy Transaction Details Updated Sucessfully.!", this);
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
                            int DocCount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(DOC_NAME)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));
                            if (DocCount > 0)
                            {
                                ObjE.file_name = btnDownloadFile.ToolTip;
                                ObjE.file_path = "Blob Storage";
                                ret = Convert.ToInt32(objSReg.AddRevalTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                                if (ret > 0)
                                {
                                    BindCourseListForReval();
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
                            filename = ObjE.Idno + "_revaluationpaymentdocuments_" + lblEnrollNo.Text.Trim() + ext;
                            ObjE.file_name = filename;
                            ObjE.file_path = "Blob Storage";

                            if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".PDF")
                            {

                                if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                {
                                    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, ObjE.Idno + "_revaluationpaymentdocuments_" + lblEnrollNo.Text.Trim() + "", fuUpload);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }

                                    ret = Convert.ToInt32(objSReg.AddRevalTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                                    if (ret > 0)
                                    {
                                        BindCourseListForReval();
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
                    //objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    //return;
                    ObjE.file_name = btnDownloadFile.ToolTip;
                    ObjE.file_path = "Blob Storage";
                    ret = Convert.ToInt32(objSReg.AddRevalTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                    if (ret > 0)
                    {
                        BindCourseListForReval();
                        objCommon.DisplayMessage(this, "Student Revaluation & Photocopy Transaction " + ddlstatus.SelectedItem.Text + " Sucessfully.", this);
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

    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();

        string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
        string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
        string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReVal"].ToString();

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

    private void BindCourseListForReval()
    {

        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsCurrCourses = null;

        //Show Courses for Revaluation
        dsCurrCourses = objSC.GetCourseFor_Reval(Convert.ToInt32(Session["idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBackLogSem.SelectedValue), 1);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            DivRegCourse.Visible = true;
            //btnSubmit.Visible = true;
            lvFailCourse.DataSource = dsCurrCourses.Tables[0];
            lvFailCourse.DataBind();
            lvFailCourse.Visible = true;
            Totalamt.Visible = true;

            int revalcount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(*)", "ISNULL(CANCEL,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue)));

            if (revalcount > 0)
            {
                Totalamt.Visible = false;
                if (Convert.ToInt32(Session["usertype"].ToString()) == 2)
                {
                    btnprintpayslip.Visible = true;
                    btnPrintRegSlip.Visible = false;
                    TransactionDiv.Visible = true;
                    btntransactiondetails.Visible = true;
                    ShowDetailsfinalStud();
                    if (lvFailCourse.Items.Count > 0)
                    {
                        int i = 0;
                        int sum = 0;

                        foreach (ListViewDataItem dataitem in lvFailCourse.Items)
                        {
                            // CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                            CheckBox chk_reval = dataitem.FindControl("chk_reval") as CheckBox;
                            CheckBox chk_photocopy = dataitem.FindControl("chk_photocopy") as CheckBox;
                            if (dsCurrCourses.Tables[0].Rows[i]["TOTAL_AMT"].ToString() != string.Empty)
                            {
                                sum += Convert.ToInt32(dsCurrCourses.Tables[0].Rows[i]["TOTAL_AMT"]);
                                chk_reval.Enabled = false;
                                chk_photocopy.Enabled = false;
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
                    TransactionDiv.Visible = true;
                    btnPrintRegSlip.Visible = true;
                    // btntransactiondetails.Visible = false;
                    TransactionDiv.Visible = true;
                    ShowDetailsfinalStud();
                }
            }
            else
            {
                btnPrintRegSlip.Visible = false;
                btnprintpayslip.Visible = false;
                btntransactiondetails.Visible = false;
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
            objCommon.DisplayMessage(this, "No Course found in Allotted Scheme and Semester.\\nIn case of any query contact administrator.", this.Page);
        }
    }

    private void ShowDetailsfinalStud()
    {
        string idno;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();

        try
        {

            idno = Session["idno"].ToString();

            if (Convert.ToInt32(idno) > 0)
            {
                // DataSet dsStudent = objSReg.GetStudentDetailsExam(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBackLogSem.SelectedValue));

                DataSet dsStudent = objCommon.DynamicSPCall_Select("PKG_STUDENT_SP_RET_STUDENT_BYID_FOR_REVAL_REGISTRATION", "@P_IDNO,@P_SESSIONNO, @P_SEMESTERNO", "" + Convert.ToInt32(idno) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlBackLogSem.SelectedValue) + "");
                //DataSet dsStudent = objSReg.GetStudentDetailsForRevaluationExam(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBackLogSem.SelectedValue));
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        TransactionDiv.Visible = true;
                        txttranid.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                        txttransamount.Text = dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANSACTION_AMT"].ToString().TrimEnd('.');
                        txttransdate.Text = dsStudent.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : dsStudent.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                        ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString();
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

                        int revalcount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(*)", "ISNULL(CANCEL,0)=0 AND IDNO=" + (Convert.ToInt32(idno) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlBackLogSem.SelectedValue))));

                        if (revalcount > 0)
                        {
                            if (Session["usertype"].ToString().Equals("2"))     //Student 
                            {
                                if (dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString() == "1")
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
                                }
                                else if (dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString() == "2")
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
                                }
                            }
                            else
                            {
                                if (dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString() == "1")
                                {
                                    DivSubmit.Visible = true;
                                    btnPrintRegSlip.Visible = true;
                                    btnprintpayslip.Visible = false;
                                    btnShow.Visible = false;
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
                                    bntCancel.Visible = false;
                                    ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString();
                                    txtremark.Text = dsStudent.Tables[0].Rows[0]["REMARK"].ToString();
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
                                    //btntransactiondetails.Enabled = false;
                                    //btntransactiondetails.Visible = false;
                                    btnCancel.Visible = false;
                                    bntCancel.Visible = false;
                                    ddlstatus.SelectedValue = dsStudent.Tables[0].Rows[0]["REV_APPROVE_STAT"].ToString();
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

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            //int Sessionno = (ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0);
            //string Examno = ddlExam.SelectedValue;
            GridView GV = new GridView();
            string sp_procedure = "PKG_ACD_GET_COURSE_FOR_REVALUATION_OVERALL_REPORT";
            string sp_parameters = "@P_SESSIONNO";
            string sp_callValues = "" + (ddlSession.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {

                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = "Attachment ; filename=Revaluatin_PhotocopyEntryStatus.xls";
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


}
