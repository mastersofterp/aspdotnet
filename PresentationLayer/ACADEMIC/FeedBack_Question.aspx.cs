//=================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : Academic
// PAGE NAME     : FeedBack_Question.aspx
// CREATION DATE : 27-03-2015
// CREATED BY    : Mr.Manish Walde
// MODIFIED BY   : Neha Baranwal
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
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class ACADEMIC_FeedBack_Question : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSBC = new StudentFeedBackController();
    StudentFeedBack SFB = new StudentFeedBack();

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
                //Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                //Fill DropDownList
                objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");
                objCommon.FillDropDownList(ddlCT, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO > 0", "FEEDBACK_NAME");
                ddlCT.SelectedIndex = 0;

                objCommon.FillDropDownList(ddlHeaderQue, "ACD_FEEDBACK_HEADRER_QUESTION_MASTER", "HEADER_ID", "QUESTION_HEADER", "HEADER_ID > 0", "HEADER_ID");
                

                //to load all semester in listbox
                ddlSemester.DataSource = null;
                ddlSemester.DataBind();
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
                //bind sem
                // DataSet ds = objsql.ExecuteDataSet("SELECT 0 AS SEMESTERNO, 'SELECT ALL' AS SEMESTERNAME UNION select SEMESTERNO,SEMESTERNAME from ACD_SEMESTER where SEMESTERNO > 0 order by SEMESTERNO");
                DataSet ds = objsql.ExecuteDataSet("SELECT SEMESTERNO,SEMESTERNAME from ACD_SEMESTER where SEMESTERNO > 0 order by SEMESTERNO");

                // ListBox lstbxSections = e.Item.FindControl("ddlSemester") as ListBox;
                ddlSemester.DataValueField = "SEMESTERNO";
                ddlSemester.DataTextField = "SEMESTERNAME";
                ddlSemester.DataSource = ds.Tables[0];
                ddlSemester.DataBind();

                //load all questions
                FillQuestion();
                ViewState["action"] = "add";

                SetInitialRow();
                // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");  // Comment Shikant Ramekar
            }
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Feedback_Question.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Feedback_Question.aspx");
        }
    }
    #endregion

    #region Bind Questions List
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void ddlCT_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillQuestion();
        if (ddlCT.SelectedIndex > 0)
        {
            int feedbackmode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlCT.SelectedValue)));

            if (feedbackmode == 3)
            {
                divHeaderque.Visible = true;
            }
            else
            {
                divHeaderque.Visible = false;
            }
        }
        try
        {
            if (ddlCT.SelectedIndex > 0)
            {
                //to get subject type dropdown according to feedback type
                if (ddlCT.SelectedItem.Text == "Subject Faculty")
                {
                    divSubjectType.Visible = true;

                }
                else
                {
                    divSubjectType.Visible = false;
                    ddlSubType.SelectedIndex = 0;
                }
            }
        }
        catch { }
    }

    //function to  get all question from db
    private void FillQuestion()
    {
        SFB.CTID = Convert.ToInt32(ddlCT.SelectedValue);
        // SFB.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        SFB.SubId = Convert.ToInt32(ddlSubType.SelectedValue);
        string selectedItem = "";
        for (int i = 0; i < ddlSemester.Items.Count; i++)
        {
            if (ddlSemester.Items[i].Selected)
            {
                selectedItem += ddlSemester.Items[i].Value + ",";
                //insert command
            }
        }
        if (selectedItem == "")
        {
            selectedItem = "0";
        }

        DataSet ds = objSBC.GetAllFeedBackQuestionForMaster(Convert.ToInt32(ddlCT.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), selectedItem);

        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvQuestion.DataSource = null;
                lvQuestion.DataBind();
                lvQuestion.Items.Clear();
                lvQuestion.Visible = true;
                lvQuestion.DataSource = ds;
                lvQuestion.DataBind();

                foreach (ListViewDataItem dataitem in lvQuestion.Items)
                {
                    RadioButtonList rblOptions = dataitem.FindControl("rblOptions") as RadioButtonList;
                    HiddenField hdnOptions = dataitem.FindControl("hdnOptions") as HiddenField;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(hdnOptions.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
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

                                            rblOptions.Items.Add(opt[j]);
                                            rblOptions.SelectedIndex = itemindex;
                                            rblOptions.SelectedItem.Value = val[j];

                                            itemindex++;
                                            break;
                                        }
                                    }
                                }
                            }
                            rblOptions.SelectedIndex = -1;
                            break;
                        }
                    }
                }
            }
            else
            {
                lvQuestion.Visible = false;
                lvQuestion.DataSource = null;
                lvQuestion.DataBind();
            }
            ds.Dispose();
        }
        else
        {
            lvQuestion.Visible = false;
            lvQuestion.DataSource = null;
            lvQuestion.DataBind();
        }
    }
    #endregion

    //funtion to check duplicate values in string
    private bool HasDuplicateValues(string[] arrayList)
    {
        List<string> vals = new List<string>();
        bool returnValue = false;
        foreach (string s in arrayList)
        {
            if (vals.Contains(s))
            {
                returnValue = true;
                break;
            }
            vals.Add(s);
        }
        return returnValue;
    }
    //funtion to check duplicate values in string
    private bool HasDuplicateAnswers(string[] arrayList)
    {
        List<string> vals = new List<string>();
        bool returnValue = false;
        foreach (string s in arrayList)
        {
            if (vals.Contains(s))
            {
                returnValue = true;
                break;
            }
            vals.Add(s);
        }
        return returnValue;
    }


    //question added 
    #region Submit functionality
    CustomStatus cs;
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtQuestion.Text.Length > 125)
            {
                objCommon.DisplayMessage("Maximum characters for feedback question should be less than 125 !", this.Page);
                return;
            }

            int seqcount = 0;
            string ansoptiontype = string.Empty;
            for (int isem = 0; isem < ddlSemester.Items.Count; isem++)
            {
                if ((ddlSemester.SelectedValue) == "")
                {

                    objCommon.DisplayUserMessage(updQuestion, "Please Select Semester", this.Page);
                    FillQuestion();
                    return;

                }
                else
                {

                    if (ddlSemester.Items[isem].Selected == true)
                    {
                        //SFB.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);

                        SFB.SemesterNo = Convert.ToInt32(ddlSemester.Items[isem].Value);

                        SFB.SubId = Convert.ToInt32(ddlSubType.SelectedValue);
                        SFB.CTID = Convert.ToInt32(ddlCT.SelectedValue);


                        if (!txtQuestion.Text.Trim().Equals(string.Empty))
                            SFB.QuestionName = txtQuestion.Text.Trim();
                        SFB.CollegeCode = Session["colcode"].ToString();

                        if (chkActiveStatus.Checked == true)
                        { SFB.ActiveStatus = 1; }
                        else { SFB.ActiveStatus = 0; }

                        int calcstatus = 0;

                        if (chkCalculation.Checked == true)
                        { calcstatus = 1; }
                        else { calcstatus = 0; }

                        int rowIndex = 0;
                        string ansOptions = string.Empty;
                        string ansValue = string.Empty;
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                            if (dtCurrentTable.Rows.Count > 0)
                            {
                                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                                {
                                    if (ddlAnsoption.SelectedValue == "1")
                                    {
                                        if (dtCurrentTable.Rows.Count > 1)
                                        {
                                            //extract the TextBox values
                                            TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                                            TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");
                                            ansoptiontype = "R";
                                            if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                                            {
                                                //get the values from the TextBoxes
                                                ansOptions += box1.Text.Trim() + ",";
                                                ansValue += box2.Text.Trim() + ",";
                                                rowIndex++;
                                            }
                                            else
                                            {
                                                objCommon.DisplayUserMessage(updQuestion, "Please Enter Answer Options in Data Row " + i, this.Page);
                                                FillQuestion();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            objCommon.DisplayUserMessage(updQuestion, "Please Enter More Answer Options in Data Row ", this.Page);
                                            FillQuestion();
                                            return;
                                        }
                                    }
                                    else if (ddlAnsoption.SelectedValue == "2")
                                    {
                                        ansoptiontype = "T";
                                    }
                                    //else if (ddlAnsoption.SelectedValue == "2")
                                    //{
                                    //    if (dtCurrentTable.Rows.Count == 1  )
                                    //    {
                                    //        //extract the TextBox values
                                    //        TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                                    //        TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");
                                    //        ansoptiontype = "T";
                                    //        if (ddlAnsoption.SelectedValue == "2" && box1.Text.Trim() != string.Empty)
                                    //        {
                                    //            objCommon.DisplayMessage(updQuestion, " Answer For Added Option Should be Empty", this.Page);
                                    //            return;
                                    //        }
                                    //        if (ddlAnsoption.SelectedValue == "2" && box2.Text.Trim() != string.Empty)
                                    //        {
                                    //            //get the values from the TextBoxes
                                    //            ansOptions += box1.Text.Trim() + ",";
                                    //            ansValue += box2.Text.Trim() + ",";
                                    //            rowIndex++;
                                    //        }
                                    //        else if (ddlAnsoption.SelectedValue == "2" && box2.Text.Trim() == string.Empty)
                                    //        {
                                    //            objCommon.DisplayMessage(updQuestion, "Please Enter Value For Added Option", this.Page);
                                    //            return;
                                    //        }


                                    //    }
                                    //    else
                                    //    {
                                    //        objCommon.DisplayUserMessage(updQuestion, "Only One Answer Options in Data Row is Allowed", this.Page);
                                    //        FillQuestion();
                                    //        return;
                                    //    }
                                    //}
                                }
                            }

                        }

                        SFB.AnsOptions = ansOptions.TrimEnd(',');
                        SFB.Value = ansValue.TrimEnd(',');

                        int Coursetype = 0;
                        int Choisefor = 0;
                        if (rdoTheory.Checked == true || rdoPractical.Checked == true)
                        {
                            Coursetype = (rdoTheory.Checked ? 1 : 2);
                        }
                        else
                        {
                            Coursetype = 0;
                        }
                        if (rdoStudent.Checked == true || rdoFaculty.Checked == true)
                        {
                            Choisefor = (rdoStudent.Checked ? 1 : 2);
                        }
                        else
                        {
                            Choisefor = 0;
                        }

                        string[] str1 = SFB.Value.ToString().Split(',');
                        string[] str2 = SFB.AnsOptions.ToString().Split(',');
                        bool valuestatus = HasDuplicateValues(str1);
                        bool answerstatus = HasDuplicateAnswers(str2);
                        if (valuestatus == true)
                        {
                            objCommon.DisplayUserMessage(updQuestion, "Please Enter Unique Answer Values", this.Page);
                            FillQuestion();
                            return;
                        }
                        else if (answerstatus == true)
                        {
                            objCommon.DisplayUserMessage(updQuestion, "Please Enter Unique Options in Answers", this.Page);
                            FillQuestion();
                            return;
                        }
                        else
                        {
                            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                            {
                                seqcount = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_QUESTION", "count(*)", "SEQ_NO='" + Convert.ToInt32(txtseqno.Text) + "' AND CTID='" + Convert.ToInt32(ddlCT.SelectedValue) + "' AND SEMESTERNO='" + Convert.ToInt32(ddlSemester.Items[isem].Value) + "' AND QUESTIONID<>'" + Convert.ToInt32(ViewState["QuestionId"]) + "'"));

                                //if (seqcount > 0)
                                //{
                                //    objCommon.DisplayMessage(updQuestion, "Sequence No. is Already exists.", this.Page);
                                //    return;
                                //}

                                int QuestionNameCount = Convert.ToInt32((objCommon.LookUp("ACD_FEEDBACK_QUESTION", "count(*)", "SEQ_NO='" + Convert.ToInt32(txtseqno.Text) + "' AND CTID='" + Convert.ToInt32(ddlCT.SelectedValue) + "' AND SEMESTERNO='" + Convert.ToInt32(ddlSemester.Items[isem].Value) + "'")));

                                //Edit 
                                if (QuestionNameCount > 0)
                                {
                                    SFB.QuestionId = Convert.ToInt32(ViewState["QuestionId"]);
                                    cs = (CustomStatus)objSBC.UpdateFeedbackQuestion(SFB, calcstatus, ansoptiontype, Convert.ToInt32(txtseqno.Text), Convert.ToInt32(ddlHeaderQue.SelectedValue), Coursetype, Choisefor);
                                }
                                else
                                {
                                    int SeqenceNo = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_QUESTION", "MAX(SEQ_NO) AS SEQ_NO ", ""));
                                    int maxSeqenceNo = SeqenceNo + 1;
                                    cs = (CustomStatus)objSBC.AddFeedbackQuestion(SFB, calcstatus, ansoptiontype, maxSeqenceNo, Convert.ToInt32(ddlHeaderQue.SelectedValue), Coursetype, Choisefor);
                                }
                            }
                            else
                            {
                                seqcount = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_QUESTION", "count(*)", "SEQ_NO='" + Convert.ToInt32(txtseqno.Text) + "' AND CTID='" + Convert.ToInt32(ddlCT.SelectedValue) + "' AND SEMESTERNO='" + Convert.ToInt32(ddlSemester.Items[isem].Value) + "'"));
                                if (seqcount > 0)
                                {
                                    objCommon.DisplayMessage(updQuestion, "Sequence No. is Already exists.", this.Page);
                                    return;
                                }
                                //save
                                cs = (CustomStatus)objSBC.AddFeedbackQuestion(SFB, calcstatus, ansoptiontype, Convert.ToInt32(txtseqno.Text), Convert.ToInt32(ddlHeaderQue.SelectedValue), Coursetype, Choisefor);
                            }
                        }
                    }
                }
            }

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Edit 
                //SFB.QuestionId = Convert.ToInt32(ViewState["QuestionId"]);
                //CustomStatus cs = (CustomStatus)objSBC.UpdateFeedbackQuestion(SFB);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Question Updated successfully", this.Page);
                    ClearControl();
                    divHeaderque.Visible = false;
                    FillQuestion();

                }
                else if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Question Updated successfully", this.Page);
                    ClearControl();
                    divHeaderque.Visible = false;
                    FillQuestion();

                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Question Already Added !!!!", this.Page);
                    FillQuestion();
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Transaction Failed", this.Page);
                    //FillQuestion();
                }
            }
            else
            {
                //Add New

                // CustomStatus cs = (CustomStatus)objSBC.AddFeedbackQuestion(SFB);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Feedback Question Saved Successfully", this.Page);
                    ClearControl();
                    divHeaderque.Visible = false;
                    FillQuestion();


                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Question Already Added !!!!", this.Page);
                    FillQuestion();
                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayUserMessage(updQuestion, "Transaction Failed", this.Page);
                    //FillQuestion();
                }
            }
            FillQuestion();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FeedBack_Question.btnSubmit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

    #region Edit Functionality
    //question update 
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int CTID;
        int SemesterNo;
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            SFB.QuestionId = int.Parse(btnEdit.CommandArgument);
            ViewState["QuestionId"] = int.Parse(btnEdit.CommandArgument);
            //foreach (ListViewDataItem itm in lvQuestion.Items)
            //{
            ListViewDataItem itm = btnEdit.NamingContainer as ListViewDataItem;
            HiddenField hfdCtid = itm.FindControl("hfdCtid") as HiddenField;
            HiddenField hfdSemesterNo = itm.FindControl("hfdSemesterNo") as HiddenField;
            CTID = Convert.ToInt32(hfdCtid.Value);
            SemesterNo = Convert.ToInt32(hfdSemesterNo.Value);
            //}


            int CountOnlinefeedback = Convert.ToInt32(objCommon.LookUp("ACD_ONLINE_FEEDBACK F INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=F.IDNO AND R.SESSIONNO=F.SESSIONNO AND R.COURSENO=F.COURSENO)", "Count(*)", "F.QUESTIONID='" + SFB.QuestionId + "' AND F.CTID='" + CTID + "' AND R.SEMESTERNO='" + SemesterNo + "'"));
            if (CountOnlinefeedback > 0)
            {
                objCommon.DisplayUserMessage(updQuestion, "Feedback Has been Already Submitted For This Question And Semester", this.Page);
                return;
            }
            else
            {
                ViewState["action"] = "edit";
                btnSubmit.Text = "Update";
                this.ShowDetails();
                int feedbackmode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlCT.SelectedValue)));

                if (feedbackmode == 3)
                {
                    divHeaderque.Visible = true;
                }
                else
                {
                    divHeaderque.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FeedBack_Question.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }


    //function to show question details 
    private void ShowDetails()
    {
        try
        {
            DataSet ds = objSBC.GetEditFeedBack(SFB);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    txtQuestion.Text = ds.Tables[0].Rows[0]["QUESTIONNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["QUESTIONNAME"].ToString();

                    ddlCT.SelectedValue = ds.Tables[0].Rows[0]["CTID"] == null ? "0" : ds.Tables[0].Rows[0]["CTID"].ToString();

                    ddlHeaderQue.SelectedValue = ds.Tables[0].Rows[0]["QUESTION_HEADER_ID"] == null ? "0" : ds.Tables[0].Rows[0]["QUESTION_HEADER_ID"].ToString();

                    if (ddlCT.SelectedItem.Text == "Subject Faculty")
                    {
                        divSubjectType.Visible = true;
                        ddlSubType.SelectedValue = ds.Tables[0].Rows[0]["SUBID"] == null ? "0" : ds.Tables[0].Rows[0]["SUBID"].ToString();
                    }
                    else
                    {
                        divSubjectType.Visible = false;
                        //ddlSubType.SelectedValue = ds.Tables[0].Rows[0]["SUBID"] == null ? "0" : ds.Tables[0].Rows[0]["SUBID"].ToString();
                    }
                    // SFB.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);



                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVE_STATUS"]) == 1)
                    {
                        chkActiveStatus.Checked = true;
                    }
                    else
                    {
                        chkActiveStatus.Checked = false;
                    }

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_ALLOW_TO_CALC"]) == 1)
                    {
                        chkCalculation.Checked = true;
                    }
                    else
                    {
                        chkCalculation.Checked = false;
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["OPTION_TYPE"]) == "R")
                    {
                        ddlAnsoption.SelectedValue = "1";
                        divansoption.Visible = true;
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[0]["OPTION_TYPE"]) == "T")
                    {
                        ddlAnsoption.SelectedValue = "2";
                        divansoption.Visible = false;
                    }
                    txtseqno.Text = ds.Tables[0].Rows[0]["SEQ_NO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEQ_NO"].ToString();

                    if (ds.Tables[0].Rows[0]["COURSE_TYPE"].ToString().Equals("1"))
                    {
                        rdoTheory.Checked = true;
                        rdoPractical.Checked = false;
                        rdoNone1.Checked = false;
                    }
                    else if (ds.Tables[0].Rows[0]["COURSE_TYPE"].ToString().Equals("2"))
                    {
                        rdoTheory.Checked = false;
                        rdoPractical.Checked = true;
                        rdoNone1.Checked = false;
                    }
                    else
                    {
                        rdoTheory.Checked = false;
                        rdoPractical.Checked = false;
                        rdoNone1.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["CHOISE_FOR"].ToString().Equals("1"))
                    {
                        rdoStudent.Checked = true;
                        rdoFaculty.Checked = false;
                        rdoNone2.Checked = false;
                    }
                    else if (ds.Tables[0].Rows[0]["CHOISE_FOR"].ToString().Equals("2"))
                    {
                        rdoStudent.Checked = false;
                        rdoFaculty.Checked = true;
                        rdoNone2.Checked = false;
                    }
                    else
                    {
                        rdoStudent.Checked = false;
                        rdoFaculty.Checked = false;
                        rdoNone2.Checked = true;
                    }

                    ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? "0" : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    DataTable dtCurrentTable = new DataTable();

                    DataRow drCurrentRow = null;
                    dtCurrentTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column2", typeof(string)));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["RowNumber"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ROWNUMBER"].ToString());
                        drCurrentRow["Column1"] = ds.Tables[0].Rows[i]["ANS_OPTIONS"];
                        drCurrentRow["Column2"] = ds.Tables[0].Rows[i]["ANS_VALUE"];

                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvAnswers.DataSource = dtCurrentTable;
                    gvAnswers.DataBind();





                }
            }
            if (ds != null) ds.Dispose();

            //Set Previous Data on Postbacks
            BindDataonEdit();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FeedBack_Question.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }


    private void BindDataonEdit()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                    TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();

                    rowIndex++;
                }
            }

        }
    }
    #endregion

    //to clear controls
    private void ClearControl()
    {
        int feedbackmode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlCT.SelectedValue)));

        if (feedbackmode == 3)
        {
            divHeaderque.Visible = true;
        }
        else
        {
            divHeaderque.Visible = false;
        }
        ddlSubType.SelectedIndex = 0;
        ddlHeaderQue.SelectedIndex = 0;
        ddlCT.SelectedIndex = 0;

        txtQuestion.Text = string.Empty;

        //ddlSemester.DataSource = null;
        //ddlSemester.DataBind();
        chkCalculation.Checked = true;
        //ddlSemester.Items.Clear();
        ddlSemester.ClearSelection();
        btnSubmit.Text = "Submit";
        ddlAnsoption.SelectedIndex = 0;
        txtseqno.Text = string.Empty;
        ViewState["action"] = "add";
        SetInitialRow();
        rdoFaculty.Checked = false;
        rdoStudent.Checked = false;
        rdoPractical.Checked = false;
        rdoTheory.Checked = false;
        rdoNone1.Checked = true;
        rdoNone2.Checked = true;
    }

    //to cancel the page and refresh all controls
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // ClearControl();
        Response.Redirect(Request.Url.ToString());
    }

    #region Answer Options Functionality
    //to set initial row in grid
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        gvAnswers.DataSource = dt;
        gvAnswers.DataBind();
    }

    //add new row to grid
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
        FillQuestion();
    }
    //function to add new row to grid
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        int feedbackmode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlCT.SelectedValue)));

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow drCurrentRow = null;
            if (feedbackmode == 3)
            {
                if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 6)
                {
                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    drCurrentRow = dtNewTable.NewRow();

                    drCurrentRow["RowNumber"] = 1;
                    drCurrentRow["Column1"] = string.Empty;
                    drCurrentRow["Column2"] = string.Empty;

                    dtNewTable.Rows.Add(drCurrentRow);

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                        TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                        if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                        {
                            drCurrentRow = dtNewTable.NewRow();

                            drCurrentRow["RowNumber"] = i + 1;
                            drCurrentRow["Column1"] = box1.Text;
                            drCurrentRow["Column2"] = box2.Text;

                            rowIndex++;
                            dtNewTable.Rows.Add(drCurrentRow);
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(updQuestion, "Please Enter Answer Options in Sr.No. " + i, this.Page);
                            return;
                        }
                    }


                    ViewState["CurrentTable"] = dtNewTable;
                    gvAnswers.DataSource = dtNewTable;
                    gvAnswers.DataBind();

                    SetPreviousData();
                }
                else
                {
                    objCommon.DisplayUserMessage(updQuestion, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 5)
                {
                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                    drCurrentRow = dtNewTable.NewRow();

                    drCurrentRow["RowNumber"] = 1;
                    drCurrentRow["Column1"] = string.Empty;
                    drCurrentRow["Column2"] = string.Empty;

                    dtNewTable.Rows.Add(drCurrentRow);

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                        TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                        if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                        {
                            drCurrentRow = dtNewTable.NewRow();

                            drCurrentRow["RowNumber"] = i + 1;
                            drCurrentRow["Column1"] = box1.Text;
                            drCurrentRow["Column2"] = box2.Text;

                            rowIndex++;
                            dtNewTable.Rows.Add(drCurrentRow);
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(updQuestion, "Please Enter Answer Options in Sr.No. " + i, this.Page);
                            return;
                        }
                    }


                    ViewState["CurrentTable"] = dtNewTable;
                    gvAnswers.DataSource = dtNewTable;
                    gvAnswers.DataBind();

                    SetPreviousData();
                }
                else
                {
                    objCommon.DisplayUserMessage(updQuestion, "Maximum Options Limit Reached", this.Page);
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }


    }


    //to set previous details in grid row
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                    TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();

                    rowIndex++;
                }
            }

        }
    }

    // Hide the Remove Button at the last row of the GridView
    protected void gvAnswers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            // LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemove");
            ImageButton lb = (ImageButton)e.Row.FindControl("lnkRemove");


            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }


    //remove row from grid
    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                //if (gvRow.RowIndex < dt.Rows.Count )
                if (gvRow.RowIndex < dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            gvAnswers.DataSource = dt;
            gvAnswers.DataBind();
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    #endregion

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillQuestion();
        }
        catch { }
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillQuestion();
    }
    protected void ddlAnsoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAnsoption.SelectedIndex > 0)
        {
            if (ddlAnsoption.SelectedIndex == 1)
            {
                divansoption.Visible = true;
            }
            else
            {
                divansoption.Visible = false;
            }
        }
    }
}
