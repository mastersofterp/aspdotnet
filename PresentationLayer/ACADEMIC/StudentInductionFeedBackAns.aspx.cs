//=================================================================================
// PROJECT NAME  : RFC SVCE                                                         
// MODULE NAME   : Academic                                                                
// PAGE NAME     : Student Feed Back Ans                                          
// CREATION DATE : 16/08/19                                                   
// CREATED BY    : Neha B                         
// MODIFIED BY   : 

// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_StudentInductionFeedBackAns : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
    string sessionno = string.Empty;
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                //Check for Activity On/Off
                GetStudentDeatlsForEligibilty();
                if (CheckActivity() == false)
                    return;

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                if (Session["usertype"].ToString() == "2")
                {
                    pnlStudInfo.Visible = true;
                    FillLabel();
                    string count = "-1";
                    //to check feedback is already done or not

                    //ViewState["FEEDBACK_NO"] = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
                    ViewState["FEEDBACK_NO"] = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "( CASE WHEN (SELECT COUNT(1) FROM ACD_FEEDBACK_MASTER WHERE (FEEDBACK_NAME like '%Induction%'))>0 THEN (SELECT FEEDBACK_NO FROM ACD_FEEDBACK_MASTER WHERE (FEEDBACK_NAME like '%Induction%')) ELSE 0 END ) as FEEDBACK_NO", ""));

                    count =  objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND isnull(COURSENO,0)=0 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND isnull(UA_NO,0)=0 AND CTID=" + Convert.ToInt32(ViewState["FEEDBACK_NO"]));
                    string date = "";
                    date = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "distinct Convert(varchar(10),DATE,103)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND isnull(COURSENO,0)=0 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND isnull(UA_NO,0)=0 AND CTID=" + Convert.ToInt32(ViewState["FEEDBACK_NO"]));
                        if (Convert.ToInt32(count) > 0)//entry already done
                        {
                            lblMsg.Text = "FeedBack is already COMPLETED for " + lblName.Text + " on DATE "+ date;
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                            pnlFeedback.Visible = false;
                            btnInductionFeedbackCertificate.Visible = false;
                        }
                        else if (Convert.ToInt32(count) == 0)//new entry
                        {
                            lblMsg.Text = "";
                            lblMsg.Visible = false;
                            FillInductionFeedbackQuestion();
                            //FillTeacherQuestion();
                            pnlFeedback.Visible = true;
                            btnInductionFeedbackCertificate.Visible = false;
                        }
                }
                else
                {
                    pnlSearch.Visible = true;
                    pnlStudInfo.Visible = false;
                }
            }

           // txtAnySuggestions.Attributes.Add("onblur", "checkLength(this)");
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    //get student details like DEGREENO,BRANCHNO,SEMESTERNO,STUDNAME from ACD_STUDENT tables
    protected void GetStudentDeatlsForEligibilty()
    {
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                CheckActivity();
            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);
                pnlStudInfo.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentInductionFeedbackAns.GetStudentDeatlsForEligibilty() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //used for to check activity is ON
    private bool CheckActivity()
    {
        try
        {
            sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            Session["sessionno"] = sessionno;

            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
            if (dtr.Read())
            {
                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    pnlSearch.Visible = false;
                    pnlStudInfo.Visible = false;
                }
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    pnlSearch.Visible = false;
                    pnlStudInfo.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                pnlSearch.Visible = false;
                pnlStudInfo.Visible = false;

            }

            dtr.Close();
            return true;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_StudentInductionFeedbackAns.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }
               
    }
     
    //used for to get sessionno
    public int GetSession()
    {
        int sessionno = 0;
        string act_code = string.Empty;
        //get UA_IDNO
        int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_TYPE = 2 AND UA_NO=" + Session["userno"]));
        //get BRANCHNO
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno));
        //get SESSION_NO
        string session = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.STARTED = 1 AND (AM.ACTIVITY_CODE='Feedback' OR AM.ACTIVITY_CODE='FBM') AND " + branchno + " IN (SELECT VALUE FROM DBO.Split(BRANCH,','))");
        if (session != string.Empty)
        {
            sessionno = Convert.ToInt32(session);
        }
        return sessionno;
    }
    //get student details for perticular student and Bind this details on label
    private void FillLabel()
    {
        Course objCourse = new Course();
        CourseController objCC = new CourseController();
        SqlDataReader dr = null;
        if (Session["usertype"].ToString() == "2")
        {
            //get student details for perticuler id 
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            //get student details for perticuler id 
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
        }
        if (dr != null)
        {
            if (dr.Read())
            {//Bind student details on label
                int sessionno = 0;
                lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
                lblSession.ToolTip = Session["sessionno"].ToString();
                lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", " SESSIONNO = " + Convert.ToInt32(Session["sessionno"]));
                lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=STUDENT";
            }
        }
        if (dr != null) dr.Close();
       
    }

    //used for fill Induction program question
    private void FillInductionFeedbackQuestion()
    {
        lblInductionProgram.Visible = true;
        lblInductionProgram.Text = "Induction Program Feedback Questions : ";
        objSEB.CTID = Convert.ToInt32(ViewState["FEEDBACK_NO"]);
        objSEB.SemesterNo = Convert.ToInt32(lblSemester.ToolTip);
        //objSEB.CTID = 2;
        try
        {
            //get feedback question 
            DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB, 0);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {//bind feedback question  in list view
                lblInductionProgram.Visible = true;
                lvInductionProgram.DataSource = ds;
                lvInductionProgram.DataBind();

                foreach (ListViewDataItem dataitem in lvInductionProgram.Items)
                {
                    RadioButtonList rblInductionProgram = dataitem.FindControl("rblInductionProgram") as RadioButtonList;
                    HiddenField hdnInductionProgram = dataitem.FindControl("hdnInductionProgram") as HiddenField;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(hdnInductionProgram.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
                        {
                            string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
                            string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

                            if (ansOptions.Contains(","))
                            {
                                string[] opt;
                                string[] val;

                                opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
                                val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

                                int itemindex = 0;
                                for (int j = 0; j < opt.Length; j++)
                                {
                                    for (int k = 0; k < val.Length; k++)
                                    {
                                        if (j == k)
                                        {
                                            RadioButtonList lst;
                                            lst = new RadioButtonList();

                                            rblInductionProgram.Items.Add(opt[j]);
                                            rblInductionProgram.SelectedIndex = itemindex;
                                            rblInductionProgram.SelectedItem.Value = val[j];

                                            itemindex++;
                                            break;
                                        }
                                    }
                                }
                            }
                            rblInductionProgram.SelectedIndex = -1;
                            break;
                        }
                    }
                }
            }
            else
                {
                
                lvInductionProgram.Items.Clear();
                lblInductionProgram.Visible = false;
                lvInductionProgram.DataSource = null;
                lvInductionProgram.DataBind();
                Panel1.Visible = true;
                pnlFeedback.Visible = false;
                objCommon.DisplayMessage("No Feedback Questions are found for this semester student.", this.Page);
                return;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentInductionFeedBackAns.FillInductionFeedbackQuestion()-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }


    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInductionFeedBackAns.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInductionFeedBackAns.aspx");
        }
    }
    //used for to Submitting the Feedback Induction program form
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() == "2")
            {
                foreach (ListViewDataItem dataitem in lvInductionProgram.Items)
                {
                    //find Radio button list ID and Induction program label ID in list view.
                    RadioButtonList rblInductionProgram = dataitem.FindControl("rblInductionProgram") as RadioButtonList;
                    Label lblInductionProgramQuestions = dataitem.FindControl("lblInductionProgramQuestions") as Label;
                    if (rblInductionProgram.SelectedValue == "")
                    {
                        objCommon.DisplayMessage("You must have answer all the questions", this.Page);
                        return;
                    }
                    else
                    {
                        objSEB.AnswerIds += rblInductionProgram.SelectedValue + ",";
                        objSEB.QuestionIds += lblInductionProgramQuestions.Text + ",";
                    }
                }
                objSEB.AnswerIds = objSEB.AnswerIds.TrimEnd(',');
                objSEB.QuestionIds = objSEB.QuestionIds.TrimEnd(',');
                int retFlag = this.FillAnswers();
                if (retFlag == 1)
                {
                    objCommon.DisplayMessage("Your FeedBack Saved Successfully !", this.Page);
                    txtAnySuggestions.Text = "";
                    this.ClearControls();
                    //pnlFeedback.Visible = true;
                    btnInductionFeedbackCertificate.Visible = false;
                }
                else
                {
                    this.ClearControls();
                    objCommon.DisplayMessage("Something Went Wrong !", this.Page);
                }

               // this.CheckSubjectAssign();

               
            }
            else
            {
                objCommon.DisplayMessage("Only Students fills this form!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentInductionFeedBackAns.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //used for to cleare all the controles
    private void ClearControls()
    {
        lblMsg.Text = string.Empty;
        txtRemark.Text = string.Empty;
        pnlFeedback.Visible = false;
        txtAnySuggestions.Text = "";
    }
    //used for fill Student feedback question answer
    private int FillAnswers()
    {
        objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
        objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
        objSEB.Date = DateTime.Now;
        objSEB.CollegeCode = Session["colcode"].ToString();
        objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
        objSEB.CourseNo = 0;
        objSEB.UA_NO = 0;
        objSEB.FB_Status = true;
        objSEB.OverallImpression = "0";
        objSEB.Suggestion_A = lblAnySuggestions.Text;
        objSEB.Suggestion_B = txtAnySuggestions.Text;
        //objSEB.Suggestion_C = lblAnyComments.Text;
        //objSEB.Suggestion_D = txtAnyComments.Text;
        objSEB.CTID = Convert.ToInt32(ViewState["FEEDBACK_NO"]); // 2 - Induction Program
        //objSEB.ExitQuestionBestTeacher = lblNameTheBestTeachers.Text;
        //objSEB.FromDepartment = txtFromYourDepartment.Text;
        //objSEB.OtherDepartment = txtFromOtherDepartment.Text;
        if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();
        int ret = objSFBC.AddStudentFeedBackAns(objSEB);
        return ret;
    }
    //refresh current page or reload current page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString()));
        //if (Convert.ToInt32(count) != 0)
        //    ShowReport("Student_FeedBack", "StudentFeedBackAns.rpt");
        //else
        //    objCommon.DisplayMessage("Record Not Found", this.Page);
    }

   
    //search IDNO using entering  REGNO
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + txtSearch.Text.Trim() + "'");
        if (idno != "")
        {
            ViewState["Id"] = Convert.ToInt32(idno);
            FillLabel();
            pnlStudInfo.Visible = true;
            btnClear.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found", this.Page);
        }
    }
    //refresh current page or reload current page
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //showing the Student_Induction_Certificate report in rptStudentInductionProgramCertificate.rpt file .
    protected void btnInductionFeedbackCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Student_Induction_Certificate", "rptStudentInductionProgramCertificate.rpt");
        }
        catch { }
       
    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(lblName.ToolTip);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentInductionFeedBackAns.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
