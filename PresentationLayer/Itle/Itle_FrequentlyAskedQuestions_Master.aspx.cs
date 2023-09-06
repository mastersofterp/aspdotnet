//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : THIS IS A Frequently Asked Question PAGE                                     
// CREATION DATE : 25/02/2014
// CREATED BY    : ZUBAIR AHMAD                              
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Itle_Itle_FrequentlyAskedQuestions_Master : System.Web.UI.Page
{
    IFAQMaster objFrequent = new IFAQMaster();
    IFAQController objFAQ = new IFAQController();

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

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
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
            }
            BindListView();
            TotalQuestions();

            //TotalQuestions();


        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
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
                Response.Redirect("~/notauthorized.aspx?page=Itle_FrequentlyAskedQuestions_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_FrequentlyAskedQuestions_Master.aspx");
        }
    }

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("Itle_FrequentlyAskedQuestions_Master.aspx");
        }

    }

    private void BindListView()
    {
        try
        {

            DataSet ds = objFAQ.GetAllFAQListByIdNo(Convert.ToInt32(Session["SessionNo"]), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["idno"]));
            //Added by Saket Singh on 11-09-2017
            if (ds.Tables[0].Rows.Count == 0)
            {
                lvlinks.DataSource = null;
                lvlinks.DataBind();
                pnlList.Visible = false;
            }
            else
            {
                lvlinks.DataSource = ds;
                lvlinks.DataBind();
                pnlList.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        //txtSubject.Text = string.Empty;
        //txtQuestion.Text = "&nbsp;";
        //Response.Redirect(Request.Url.ToString());

        //Added by Saket Singh on 11-09-2017
        try
        {
            txtSubject.Text = string.Empty;
            txtQuestion.Text = "&nbsp;";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetail(int quesno, int courseno, int sessionno, int idno)
    {
        try
        {

            ViewState["quesno"] = quesno;
            DataTableReader dtr = objFAQ.GetSingleFAQustion(Convert.ToInt32(ViewState["quesno"]), courseno, sessionno, idno);

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //ViewState["assignno"] = int.Parse(dtr["AS_NO"].ToString());
                    txtSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    txtQuestion.Text = dtr["QUESTION"] == null ? "" : dtr["QUESTION"].ToString();
                    //txtSubmitDate.Text = dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("dd-MMM-yyyy");
                    //hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    lblCurrdate.Text = dtr["CREATED_DATE"] == null ? "" : Convert.ToDateTime(dtr["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");

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

    private void ShowAnswer(int quesno, int courseno, int sessionno)
    {
        try
        {

            ViewState["quesno"] = quesno;
            //DataTableReader dtr = objFAQ.GetFAQAnswer(Convert.ToInt32(ViewState["quesno"]), courseno, sessionno);
            DataTableReader dtr = objFAQ.GetFAQAnswer(quesno, courseno, sessionno);

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //ViewState["assignno"] = int.Parse(dtr["AS_NO"].ToString());
                    lblSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    lblQuestion.Text = dtr["QUESTION"] == null ? "" : dtr["QUESTION"].ToString();
                    txtAnswer.Text = dtr["ANSWER"] == null ? "" : dtr["ANSWER"].ToString();
                    //lblCurrdate.Text = DateTime.Today.ToString();
                    tdDate.InnerText = dtr["REPLY_DATE"] == null ? "" : Convert.ToDateTime(dtr["REPLY_DATE"].ToString()).ToString("dd-MMM-yyyy");
                    //txtSubmitDate.Text = dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("dd-MMM-yyyy");
                    linkAssingReplyFile.Text = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    linkAssingReplyFile.NavigateUrl = dtr["ATTACHMENT"] == null ? "" : "upload_files/FAQ/FAQ_" + Convert.ToInt32(ViewState["quesno"]) + System.IO.Path.GetExtension(dtr["ATTACHMENT"].ToString());
                    //LinkButton1.Text = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();

                    lblCurrdate.Text = dtr["CREATED_DATE"] == null ? "" : Convert.ToDateTime(dtr["CREATED_DATE"].ToString()).ToString("dd-MMM-yyyy");

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
            objCommon.DisplayUserMessage(Page, "Itle_FrequentlyAskedQuestions_Master.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {

            int idno;

            idno = Convert.ToInt32(Session["idno"]);

            string filename = string.Empty;

            //if (Request.QueryString["cno"] != null)
            //    Session["CourseNo"] = Request.QueryString["cno"].ToString();

            objFrequent.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objFrequent.IDNO = Convert.ToInt32(Session["idno"]);
            objFrequent.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objFrequent.SESSIONNO = Convert.ToInt32(Session["SessionNo"]);
            objFrequent.SUBJECT = txtSubject.Text;
            objFrequent.QUESTION = txtQuestion.Text.Trim();

            objFrequent.CREATED_DATE = Convert.ToDateTime(lblCurrdate.Text.Trim());

            objFrequent.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Edit Assignment
                if (ViewState["quesno"] != null)
                {
                    //Addtional property 
                    objFrequent.QUES_NO = Convert.ToInt32(ViewState["quesno"]);
                    objFrequent.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    objFrequent.IDNO = Convert.ToInt32(Session["idno"]);
                    objFrequent.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                    objFrequent.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
                    objFrequent.SUBJECT = txtSubject.Text;
                    objFrequent.QUESTION = txtQuestion.Text.Trim();
                    objFrequent.CREATED_DATE = Convert.ToDateTime(lblCurrdate.Text.Trim());
                    //objFrequent.OLDFILENAME = hdnFile.Value;

                    //if (hdnFile.Value != "" && hdnFile.Value != null && fuAssign.HasFile == false)
                    //{
                    //    objAssign.ATTACHMENT = hdnFile.Value;
                    //}

                    CustomStatus cs = (CustomStatus)objFAQ.UpdateFAQustion(objFrequent);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                        lblStatus.Text = "Assignment Modified";
                    //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    else
                        if (cs.Equals(CustomStatus.FileExists))
                            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";

                    ClearControls();
                }
                ViewState["action"] = "add";
            }
            else
            {  //Add Assignment 
                bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objFAQ.AddFAQustion(objFrequent);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        //lblStatus.Text = "Question Sent Succesfully...";
                        objCommon.DisplayUserMessage(updFAQ, "Question Sent Succesfully...", this.Page);
                    // Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    else
                        if (cs.Equals(CustomStatus.FileExists))
                            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                }
            }

            BindListView();
            ClearControls();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ClearControls();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int quesno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(quesno, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["idno"].ToString()));

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
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
            ViewState["quesno"] = quesno;
            trAnswer.Visible = true;
            trCreate.Visible = false;
            txtAnswer.ReadOnly = true;
            int courseno = Convert.ToInt32(Session["ICourseNo"]);
            int sessionno = Convert.ToInt32(lblSession.ToolTip);

            ShowAnswer(quesno, courseno, sessionno);

            //ShowAnswer(quesno, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip));
            ////LinkButton1.Attributes.Add("onclick", "window.open('" + GetFileName(LinkButton1.Text, ViewState["quesno"]) + "" + "','','width=1000,height=1000,scrollbars=yes')");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        trAnswer.Visible = false;
        trCreate.Visible = true;

    }

    protected void btnViewFAQ_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_Forum_Report", "Itle_FAQ_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.btnViewFAQ_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    #endregion

    #region Public Methods

    public Boolean checkEdit(object quesno)
    {
        if (quesno != "")
        {
            return false;

        }
        else
        {
            return true;
        }
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

    public void TotalQuestions()
    {
        try
        {
            objFrequent.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objFrequent.IDNO = Convert.ToInt32(Session["idno"]);
            DataSet ds = objFAQ.TotalQuestions(objFrequent);

            //lblTotalQues.Text = ds.Tables[0].Rows[0]["TOTAL_QUESTION"].ToString();

        }
        catch (Exception ex)
        {

        }
    }

    public string GetFileName(object filename, object quesno)
    {
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");
        //Response.TransmitFile(filename.ToString());
        //Response.End();


        if (filename != null && filename.ToString() != "")
        {

            return "~/Itle/upload_files/FAQ/FAQ_" + Convert.ToInt32(quesno) + System.IO.Path.GetExtension(filename.ToString());
        }
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/Itle/upload_files/" + filename.ToString();
        else
            return "";
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("ACD_IFAQ_MASTER", "*", "", "SUBJECT='" + txtSubject.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    #endregion

    #region Commented Codes

    //private void TotalQuestions()
    //{
    //    try
    //    {

    //        objFrequent.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
    //        objFrequent.UA_NO = Convert.ToInt32(Session["userno"]);
    //        int count = Convert.ToInt32(objCommon.LookUp("ACD_IFAQ_MASTER", "COUNT(QUES_NO) AS TOTAL_QUESTION", "COURSENO=" + objFrequent.COURSENO));
    //        //objIQBC.TotalQuestions(objQuest);

    //        lblTotalQues.Text= count.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    #endregion

}
