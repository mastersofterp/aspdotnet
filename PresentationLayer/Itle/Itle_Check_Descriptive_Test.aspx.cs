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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Itle_Itle_Check_Descriptive_Test : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestResultController objRC = new ITestResultController();
    TestResult objTR = new TestResult();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Load

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
                if (Session["Page"] == null)
                {
                    CheckPageAuthorization();
                    Session["Page"] = 1;
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lblCurrdate.ForeColor = System.Drawing.Color.Green;

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
            }
            BindListView();

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
                Response.Redirect("~/notauthorized.aspx?page=Itle_Check_Descriptive_Test.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Check_Descriptive_Test.aspx");
        }
    }

    private void BindListView()
    {
        try
        {

            DataSet ds = objRC.GetAllDescriptiveTestByCourseNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));

            lvCheckTest.DataSource = ds;
            lvCheckTest.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindQuestions()
    {

        DataSet ds = objRC.GetAllQuestionsByIdNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["Test_Number"]), Convert.ToInt32(Session["Student_Idno"]));

        lvQuestions.DataSource = ds;
        lvQuestions.DataBind();

    }

    private void ShowDetail(int ques_no)
    {
        try
        {
            DataTableReader dtr = objRC.GetSingleAnswerByQuestionNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]),
                                    Convert.ToInt32(Session["Test_Number"]), Convert.ToInt32(Session["Student_Idno"]), ques_no);

            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblQuestion.Text = dtr["QUESTIONTEXT"] == null ? "" : dtr["QUESTIONTEXT"].ToString();
                    txtMarks.Text = dtr["MARKS_OBTAINED"].ToString();
                    //txtAnswer.Text = dtr["DESCRIPTIVE_ANSWER"] == null ? "" : dtr["DESCRIPTIVE_ANSWER"].ToString();
                    divRepDesc.InnerHtml = dtr["DESCRIPTIVE_ANSWER"] == null ? "" : dtr["DESCRIPTIVE_ANSWER"].ToString();
                    txtRemark.Text = dtr["REMARKS"] == null ? "" : dtr["REMARKS"].ToString();
                    chkChecked.Checked = dtr["CHECKED"].ToString() == "0" ? false : true;
                    //lblTotalMarks.Text = dtr["ASSIGNMENT_MARKS"] == null ? "" : dtr["ASSIGNMENT_MARKS"].ToString();
                    //if (dtr["CHECKED"] != DBNull.Value)
                    //{
                    //    if (Convert.ToInt32(dtr["CHECKED"].ToString()) == 0)
                    //    {
                    //        chkChecked.Checked = false;
                    //    }
                    //}

                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void SubmitMarkEntry()
    {
        try
        {
            //if (chkChecked.Checked != true)
            //{
            //    objCommon.DisplayUserMessage(UpdatePanel1, "Check box is not checked, Please check it then submit!", this.Page);
            //}
            //else
            //{
            if (Convert.ToDecimal(txtMarks.Text) > Convert.ToDecimal(Session["Question_Marks"].ToString()))
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Obtained Marks must be Less than or Equal to the Marks assign to each Question !", this.Page);

            }
            else
            {
                objTR.IDNO = Convert.ToInt32(Session["Student_Idno"].ToString());
                objTR.TESTNO = Convert.ToInt32(Session["Test_Number"].ToString());

                objTR.REMARKS = txtRemark.Text.Trim();


                objTR.MARKS_OBTAINED = Convert.ToDecimal(txtMarks.Text.Trim());
                objTR.COURSENO = Convert.ToInt32(Session["ICourseNo"].ToString());
                //objTR.CHECKED = chkChecked.Checked == true ? '1' : '0';
                if (txtMarks.Text != "")
                {
                    objTR.CHECKED = '1';
                }
                else
                {
                    objTR.CHECKED = '0';
                }
                CustomStatus cs = (CustomStatus)objRC.MarkEntryForTest(objTR, Convert.ToInt32(ViewState["ques_no"].ToString()));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Marks Submitted Succesfully.", this.Page);
                }
                BindQuestions();
                ClearControl();
                txtMarks.Enabled = true;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.submitAssignReplyRemark-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ClearControl()
    {
        chkChecked.Checked = false;
        lblQuestion.Text = string.Empty;
        //txtAnswer.Text = string.Empty;
        divRepDesc.InnerHtml = string.Empty;
        txtMarks.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ViewState["ques_no"] = null;
    }

    #endregion

    #region Page Events

    protected void imgbtnCheckTest_Click(object sender, ImageClickEventArgs e)
    {
        //DataTableReader dtr = null;
        try
        {

            ImageButton btnTest = sender as ImageButton;
            int testno = int.Parse(btnTest.CommandArgument);
            Session["Test_Number"] = testno;

            //DateTime endTime = Convert.ToDateTime(btnTest.ToolTip); //Convert.ToDateTime(hidEndDate.Value);
            //string bal = btnApply.ToolTip.ToString();
            //if (Convert.ToDateTime(endTime.ToShortDateString()) >= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            //{
            //    objCommon.DisplayUserMessage(UpdatePanel1, "Test Is Going On Cant Check Test Now.", this.Page);
            //}
            //else
            //{
                DataSet ds = objRC.GetAllStudentByTestNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), testno);
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                linkBtnBack.Visible = true;
                lblTestName.Text = btnTest.ToolTip;
                dvTestList.Visible = false;
                trTestName.Visible = true;
                //trTotalMarks.Visible = true;
                pnlStudentList.Visible = true;
           // }

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    dtr = ds.Tables[0].CreateDataReader();
            //    if (dtr != null)
            //    {
            //        if (dtr.Read())
            //        {
            //            lblTotalMarks.Text = dtr["TOTALMARKS"].ToString() == null ? "" : dtr["TOTALMARKS"].ToString();
            //        }
            //    }
            //    if (dtr != null) dtr.Close();

            //}



            //BindMonitorInfo();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnStudCheck_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnStudId = sender as ImageButton;
            int stud_id = int.Parse(btnStudId.CommandArgument);
            Session["Student_Idno"] = stud_id;
            BindQuestions();
            pnlQuestions.Visible = true;
            pnlStudentList.Visible = false;
            trStudName.Visible = true;
            linkBtnBack.Visible = false;
            lblStudName.Text = btnStudId.ToolTip;
            trButtons.Visible = true;
            btnSubmit.Visible = false;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnQuestion_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnQuestion = sender as ImageButton;
            int ques_no = int.Parse(btnQuestion.CommandArgument);
            ViewState["ques_no"] = ques_no;
            ShowDetail(ques_no);

            Session["Question_Marks"] = btnQuestion.AlternateText;

            //txtAnswer.ReadOnly = true;
            trQuestion.Visible = true;
            trAnswer.Visible = true;
            trMarkObtained.Visible = true;
            if (!string.IsNullOrEmpty(divRepDesc.InnerHtml))
            {
                txtMarks.Enabled = true;
            }
            else
            {
                txtMarks.Enabled = false;
                txtRemark.Text = "NOT ATTEMPTED";
                txtMarks.Text = "0.0";
                chkChecked.Checked = true;
                //btnSubmit_Click(this, new EventArgs());



            }

            trRemark.Visible = true;
            trButtons.Visible = true;
            btnSubmit.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trQuestion.Visible = false;
        trAnswer.Visible = false;
        trMarkObtained.Visible = false;
        trRemark.Visible = false;
        trButtons.Visible = false;
        pnlQuestions.Visible = false;
        pnlStudentList.Visible = true;
        linkBtnBack.Visible = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SubmitMarkEntry();
        //setControl(false);

        //ViewState["ques_no"] = null;
        return;
    }

    protected void lvQuestions_ItemDataBound(object sender, ListViewItemEventArgs e)
    {



        ImageButton imgedit;
        Label lblMarksObtainded;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            // use to hide edit button on not attempted questions
            lblMarksObtainded = (Label)e.Item.FindControl("lblMarksObtained");
            imgedit = (ImageButton)e.Item.FindControl("btnQuestion");

            if (lblMarksObtainded.Text == "--")
            {
                imgedit.Visible = false;
            }
            else
            {
                imgedit.Visible = true;
            }


        }

    }

    #endregion
}
