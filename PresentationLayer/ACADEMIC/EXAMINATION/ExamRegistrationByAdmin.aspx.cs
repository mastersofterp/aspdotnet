//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : EXAM REGISTRATION                                      
// CREATION DATE : 09-OCT-2011
// ADDED BY      : ASHISH DHAKATE                                                
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

public partial class ACADEMIC_EXAMINATION_ExamRegistrationByAdmin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();

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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //CHECK THE STUDENT LOGIN
                //string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                //ViewState["usertype"] = ua_type;

                //if (ViewState["usertype"].ToString() == "2")
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
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                tblInfo.Visible = false;
            }


            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }

        divMsg.InnerHtml = string.Empty;
    }

    //private void CheckActivity()
    //{
    //    string sessionno = string.Empty;
    //    sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

    //    ActivityController objActController = new ActivityController();
    //    DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

    //    if (dtr.Read())
    //    {
    //        if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //        {
    //            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //            pnlStart.Visible = false;

    //        }

    //        //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        {
    //            objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //            pnlStart.Visible = false;
    //        }

    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //        pnlStart.Visible = false;
    //    }
    //    dtr.Close();
    //}
  
    #region

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

        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO = '" + txtEnrollno.Text.Trim() + "'");

        int sessionno = Convert.ToInt32(Session["currentsession"]);
        StudentController objSC = new StudentController();
        DataSet dsregistration;
         int a = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT","DISTINCT(ACCEPTED)","SESSIONNO= " + sessionno + " AND IDNO =" + idno + " AND  PREV_STATUS=1"));
         if (a == 1)
         {
             btnSubmit.Visible = false;
             btnCancel.Visible = false;
         }
         else
         {
             btnCancel.Visible = true;
             btnSubmit.Visible = true;
         }
        try
        {
            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage("Student with Roll. No. " + txtEnrollno.Text.Trim() + " Not Exists!", this.Page);
                tblInfo.Visible = false;
                return;
            }
            else
            {

                DataSet dsStudent = objSC.GetStudentDetailsExam(Convert.ToInt32(idno));

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["ENROLLNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";


                        //Students backlog subjects Registration Details
                        dsregistration = objSC.RetrieveStudentrRegisteredBackExamList(sessionno, Convert.ToInt32(idno));
                        if (dsregistration.Tables[0].Rows.Count > 0)
                        {
                            lvFailSubjects.DataSource = dsregistration;
                            lvFailSubjects.DataBind();
                        }
                        else
                        {
                            lvFailSubjects.DataSource = null;
                            lvFailSubjects.DataBind();
                        }

                        dsregistration = objSC.RetrieveStudentrConfirmedBackExamList(sessionno, Convert.ToInt32(idno));
                        if (dsregistration.Tables[0].Rows.Count > 0)
                        {
                            lvConfirm.DataSource = dsregistration;
                            lvConfirm.DataBind();
                            
                        }
                        else
                        {
                            lvConfirm.DataSource = null;
                            lvConfirm.DataBind();
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
    #endregion

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        //divNote.Visible = false;
        //divCourses.Visible = true;
        //ShowDetails();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(Session["currentsession"]);
        int idno = Convert.ToInt32(Session["idno"]);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();

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

    protected void btnShow_Click1(object sender, EventArgs e)
    {
        this.BindDetails();
    }

    public void BindDetails()
    {
        if (txtEnrollno.Text != string.Empty)
        {
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO = '" + txtEnrollno.Text.Trim() + "'");
            //int idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text.Trim());

            if (Convert.ToInt32(idno) != 0)
            {
                this.ShowDetails();
                tblInfo.Visible = true;
                DisplayInformation(Convert.ToInt32(idno));
            }
            else
            {
                objCommon.DisplayMessage("No Student Found having Roll No." + txtEnrollno.Text, this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Enter the Roll No.", this.Page);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            int sessionno = Convert.ToInt32(Session["currentsession"]);
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtEnrollno.Text +"'");
            
            ImageButton btnDelete = sender as ImageButton;

            ListViewDataItem lst = btnDelete.NamingContainer as ListViewDataItem;
            HiddenField hdnExamS = lst.FindControl("hdfExamSem") as HiddenField;
            int sem =Convert.ToInt32(hdnExamS.Value);

            CustomStatus cs = (CustomStatus)objSC.UpdateRegisteredCourse(Convert.ToInt32(idno), sessionno, Convert.ToInt32(btnDelete.CommandArgument),sem);
            if (cs.Equals(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
            else
                objCommon.DisplayMessage("Error in deleting record...", this.Page);

           this.ShowDetails();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        tblInfo.Visible = false;
        txtEnrollno.Text = string.Empty;
    }

    private void DisplayInformation(int studentId)
    {
        try
        {
            /// Display Previous Receipts Information
            /// Display student paid only Exam receipt information.
            /// These are the receipts(i.e. Exam Fee) paid by the student during 
            
            DataSet ds = feeController.GetPaidReceiptsExamInfoByStudId(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Bind list view with exam paid receipt data 
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                //btnSubmit.Enabled = false;
                //btnCancel.Enabled = false;
            }
            else
            {
                //divHidPreviousReceipts.InnerHtml = "No Supplimentary Exam Receipt Found.<br/>" ;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ExamregistrationSlip.DisplayInformation() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {  
        StudentController objSC = new StudentController();
        int sessionno = Convert.ToInt32(Session["currentsession"]);

        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtEnrollno.Text+"'");
        string dcrno=string.Empty;

        int status=0;
       
         if (lvPaidReceipts.Items.Count > 0)
        {

            foreach (ListViewDataItem dataitem in lvPaidReceipts.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true)
                    status++;

            }
        }
        else 
        {
            status = -1;
        }


        if (status != 0)
        {

            foreach (ListViewDataItem dataitem in lvPaidReceipts.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
                if (cbRow.Checked == true)
                {
                    dcrno += ((dataitem.FindControl("chkAccept")) as CheckBox).ToolTip + "$";
                }
            }
        }
        CustomStatus cs = (CustomStatus)objSC.UpdateStudentExamRegister(Convert.ToInt32(idno), sessionno, dcrno);

        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage("Record Updated Successfully", this.Page);
            this.BindDetails();
            //btnCancel.Enabled = false;
            //btnSubmit.Enabled = false;
        }
        else
            objCommon.DisplayMessage("Error in Updated record...", this.Page);

    }
}

