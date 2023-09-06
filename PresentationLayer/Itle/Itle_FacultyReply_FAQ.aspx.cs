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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

public partial class Itle_Itle_FacultyReply_FAQ : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    IFAQController objFAQ = new IFAQController();
    IFAQMaster objFrequent = new IFAQMaster();

    #region Page Load

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                lblReplyDate.Text = DateTime.Today.ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblReplyDate.ForeColor = System.Drawing.Color.Green;
                lblStudId1.ForeColor = System.Drawing.Color.Green;
                lblStudName1.ForeColor = System.Drawing.Color.Green;
                BindListView();
            }
        }

    }

    #endregion

    #region Private Method
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_FacultyReply_FAQ.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_FacultyReply_FAQ.aspx");
        }
    }


    private void BindListView()
    {
        try
        {

            // DataSet ds = objAM.GetAllAssignmentListByCourseNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]));
            DataSet ds = objFAQ.GetAllFAQByCourseNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));
            lvFAQ.DataSource = ds;
            lvFAQ.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void ShowDetail(int ques_no, int courseno, int sessionno, int ua_no)
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            ViewState["ques_no"] = ques_no;
            //Object objCheckStatus;
            DataTableReader dtr = objFAQ.GetSingleFAQForFaculty(Convert.ToInt32(ViewState["ques_no"]), Convert.ToInt32(Session["userno"].ToString()));
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    // txtTopic.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    lblSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    lblStudId1.Text = dtr["IDNO"] == null ? "" : dtr["IDNO"].ToString();
                    //txtDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    lblStudName1.Text = dtr["STUDNAME"] == null ? "" : dtr["STUDNAME"].ToString();
                    tdDescription.InnerHtml = dtr["QUESTION"] == null ? "" : dtr["QUESTION"].ToString();
                    lblRecieveDate1.Text = dtr["CREATED_DATE"] == null ? "" : Convert.ToDateTime(dtr["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    linkAssingFile.Text = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    linkAssingFile.NavigateUrl = dtr["ATTACHMENT"] == null ? "" : "upload_files/assignment/assignment_" + Convert.ToInt32(ViewState["ques_no"]) + System.IO.Path.GetExtension(dtr["ATTACHMENT"].ToString());
                    //lblSubmtDate.Text = DateTime.Today.ToString();
                    lblAssignFile.Visible = linkAssingFile.Text == "" ? lblAssignFile.Visible = false : lblAssignFile.Visible = true;


                    btnSubmit.Enabled = true;
                    trMessage.Visible = false;


                    //objCheckStatus = objFAQ.GetSingleFAQCheckStatusForFaculty(Convert.ToInt32(ViewState["ques_no"]), Convert.ToInt32(Session["userno"].ToString()));

                    //if (objCheckStatus != null)
                    //{
                    //    if (Convert.ToInt32(objCheckStatus.ToString()) == 1)
                    //    {
                    //        lblMessage.Text = "Your Assignment Reply Is Checked By Faculty.You Can Not Reply Any More.";
                    //        btnSubmit.Enabled = false;
                    //        trMessage.Visible = true;
                    //        //CHECKED
                    //    }
                    //}

                }
            }
            if (dtr != null) dtr.Close();
            ViewState["vsSubmit"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowAnswer(int quesno, int courseno, int sessionno)
    {
        try
        {

            ViewState["quesno"] = quesno;

            DataTableReader dtr = objFAQ.GetFAQAnswer(quesno, courseno, sessionno);
            
            //DataTableReader dtr = objFAQ.GetFAQAnswer(Convert.ToInt32(ViewState["quesno"]), courseno, sessionno);

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //ViewState["assignno"] = int.Parse(dtr["AS_NO"].ToString());
                    lblSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    tdDescription.InnerText = dtr["QUESTION"] == null ? "" : dtr["QUESTION"].ToString();
                    txtReplyDescription.Text = dtr["ANSWER"] == null ? "" : dtr["ANSWER"].ToString();
                    //lblCurrdate.Text = DateTime.Today.ToString();
                    lblReplyDate.Text = dtr["REPLY_DATE"] == null ? "" : Convert.ToDateTime(dtr["REPLY_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    //txtSubmitDate.Text = dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("dd-MMM-yyyy");
                    lblPreAttach.Text = dtr["ATTACHMENT"] == null ? "" : "Attached File : " + dtr["ATTACHMENT"].ToString();
                    //lblCurrdate.Text = dtr["CREATED_DATE"] == null ? "" : Convert.ToDateTime(dtr["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    //txtDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();

                    //Added by Saket Singh on 11-09-2017
                    lblRecieveDate1.Text = dtr["REPLY_DATE"] == null ? "" : Convert.ToDateTime(dtr["REPLY_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    lblStudId1.Text = dtr["IDNO"] == null ? "" : dtr["IDNO"].ToString();                    
                    lblStudName1.Text = dtr["STUDNAME"] == null ? "" : dtr["STUDNAME"].ToString();

                    //lblPreAttach.Visible = false;
                    //lblPreAttach.Text = "";
                    //if (dtr["ATTACHMENT"] != null)
                    //{
                    //    lblPreAttach.Visible = true;
                    //    lblPreAttach.Text = dtr["ATTACHMENT"].ToString();
                    //    //  }

                    //}
                }
                if (dtr != null) dtr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "AddForum.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ViewState["vsSubmit"] == null)
        {
            ControlReset(true);
        }

        if (Convert.ToInt16(ViewState["vsSubmit"]) == 1)
        {
            submitFaqReply();
            ViewState["vsSubmit"] = null;
            ViewState["ques_no"] = null;
            pnlAssignDetail.Visible = false;
            return;
        }
        ViewState["vsSubmit"] = 1;


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtReplyDescription.Text = "";
        pnlAssignDetail.Visible = false;
        ViewState["ques_no"] = null;

        //Response.Redirect("Itle_FacultyReply_FAQ.aspx");
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["ques_no"] = null;
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ques_no = int.Parse(btnEdit.CommandArgument);
            ShowDetail(ques_no, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));
            ViewState["action"] = "edit";

            pnlAssignDetail.Visible = true;
            ControlReset(false);
            btnSubmit.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int quesno = int.Parse(btnDel.CommandArgument);



            objFrequent.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objFrequent.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objFrequent.UA_NO = Convert.ToInt32(Session["userno"]);
            objFrequent.QUES_NO = quesno;



            if (Convert.ToInt16(objFAQ.DeleteFAQ(objFrequent)) == Convert.ToInt16(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayUserMessage(updViewFAQ, "Record Deleted successfully", this.Page);
               // lblStatus.Text = "Question Deleted Successfully...";
                // MessageBox("Record Deleted Successfully")
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReplyAnswer_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = sender as LinkButton;
            int quesno = int.Parse(btnEdit.CommandArgument);
            //int ques_no = Convert.ToInt32(ViewState["ques_no"]);
            //ShowDetail(ques_no, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));
            //trAnswer.Visible = true;
            txtReplyDescription.Visible = true;

            pnlAssignDetail.Visible = true;
            btnSubmit.Visible = false;
            linkAssingFile.Visible = false;
            txtReplyDescription.ReadOnly = true;
            //ControlReset(false);
            int courseno = Convert.ToInt32(Session["ICourseNo"]);
            int sessionno = Convert.ToInt32(lblSession.ToolTip);

            ShowAnswer(quesno, courseno, sessionno);

            //ShowAnswer(quesno, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnViewFAQ_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_FAQ_Report", "Itle_FAQ_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AddForum.btnViewFAQ_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    #endregion

    #region Public Methods

    public void ControlReset(Boolean status)
    {
        lblReply.Visible = status;
        lblFlUpld.Visible = status;
        txtReplyDescription.Visible = status;
        fuAssign.Visible = status;
        btnSubmit.Text = status == true ? "Submit" : "Reply";

    }

    public string GetFileName(object filename, object assingmentno)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/FAQ/FAQ_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/" + filename.ToString();
        else
            return "";
    }

    void submitFaqReply()
    {
        try
        {


            string filename = string.Empty;
            objFrequent.QUES_NO = Convert.ToInt32(ViewState["ques_no"].ToString());
            objFrequent.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objFrequent.IDNO = Convert.ToInt32(Session["idno"].ToString());
            objFrequent.REPLY_DATE = DateTime.Today;
            objFrequent.ANSWER_REPLY = txtReplyDescription.Text.Trim();
            objFrequent.ATTACHMENT = fuAssign.FileName;
            objFrequent.STATUS = '1';

            CustomStatus cs = (CustomStatus)objFAQ.ReplyAssignment(objFrequent, fuAssign);
            if (cs.Equals(CustomStatus.RecordSaved))
                //lblStatus.Text = "Answer Replied Successfully..";
                MessageBox("Answer Replied Successfully..");
            else
                if (cs.Equals(CustomStatus.FileExists))
                    lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        txtReplyDescription.Text = "";
        BindListView();

    }

    public Boolean checkeEnable(object quesno)
    {
        if (quesno != null)
        {
            return true;

        }
        else
        {
            return false;
        }
    }


   

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
    #endregion

}
