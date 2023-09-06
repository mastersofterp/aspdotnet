using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Itle_StudQuestionBank : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IQuestionbankController objIQBC = new IQuestionbankController();
    IQuestionbank objQuest = new IQuestionbank();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

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

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                imgQuestions.ImageUrl = "~/images/nophoto.jpg";
                imgAnswer1.ImageUrl = "~/images/nophoto.jpg";
                imgAnswer2.ImageUrl = "~/images/nophoto.jpg";
                imgAnswer4.ImageUrl = "~/images/nophoto.jpg";
                imgAnswer3.ImageUrl = "~/images/nophoto.jpg";
                imgAnswer5.ImageUrl = "~/images/nophoto.jpg";
                imgAnswer6.ImageUrl = "~/images/nophoto.jpg";
                HideControls();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCourseName.ForeColor = System.Drawing.Color.Green;

                pnllvView.Visible = true;
                pnlAdd.Visible = true;
                BindListView();
                
                ViewState["action"] = null;
                //txtQuestion.config.toolbarStartupExpanded = true;


            }
        }
        pnlAdd.Visible = true;

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    #endregion


    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=QuestionBankMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QuestionBankMaster.aspx");
        }
    }


    private void BindListView()
    {
        try
        {
            objQuest.OBJECTIVE_DESCRIPTIVE = Convert.ToChar(rbnObjectiveDescriptive.SelectedValue);
            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objQuest.UA_NO = Convert.ToInt32(Session["userno"]);
            DataSet ds = objIQBC.GetAllStudentQuestion(objQuest);
            lvQuestions.DataSource = ds.Tables[0];
            lvQuestions.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
            Response.Redirect("StudQuestionBank.aspx");
        }

    }

    private void InsertImage(int no)
    {
        try
        {
            string DOCFOLDER = file_path + "ITLE\\upload_files\\IMAGE_QUESTION";

            if (!System.IO.Directory.Exists(DOCFOLDER))
            {
                System.IO.Directory.CreateDirectory(DOCFOLDER);

            }

            HttpFileCollection files = Request.Files;
            string filename = string.Empty;
            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objQuest.QUESTIONNO = Convert.ToInt32(no);
            objQuest.UA_NO = Convert.ToInt32(Session["userno"]);


            if (fuAnswer1.HasFile)
            {
                //objQuest.ANS1IMAGE = objCommon.GetImageData(fuAnswer1);
                objQuest.ANS1IMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuAnswer1.FileName)));
            }
            else
            {
                objQuest.ANS1IMGNAME = null;
            }
            if (fuAnswer2.HasFile)
            {
                //objQuest.ANS2IMAGE = objCommon.GetImageData(fuAnswer2);
                objQuest.ANS2IMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuAnswer2.FileName)));
            }
            else
            {
                objQuest.ANS2IMGNAME = null;
            }

            if (fuAnswer3.HasFile)
            {
                //objQuest.ANS3IMAGE = objCommon.GetImageData(fuAnswer3);
                objQuest.ANS3IMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuAnswer3.FileName)));
            }
            else
            {
                objQuest.ANS3IMGNAME = null;
            }
            if (fuAnswer4.HasFile)
            {
                //objQuest.ANS4IMAGE = objCommon.GetImageData(fuAnswer4);
                objQuest.ANS4IMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuAnswer4.FileName)));
            }
            else
            {
                objQuest.ANS4IMGNAME = null;
            }

            if (fuAnswer5.HasFile)
            {
                //objQuest.ANS4IMAGE = objCommon.GetImageData(fuAnswer4);
                objQuest.ANS5IMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuAnswer5.FileName)));
            }
            else
            {
                objQuest.ANS5IMGNAME = null;
            }

            if (fuAnswer6.HasFile)
            {
                //objQuest.ANS4IMAGE = objCommon.GetImageData(fuAnswer4);
                objQuest.ANS6IMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuAnswer6.FileName)));
            }
            else
            {
                objQuest.ANS6IMGNAME = null;
            }

            if (fuQuestion.HasFile)
            {
                //objQuest.QUESTIONIMAGE = objCommon.GetImageData(fuQuestion);
                objQuest.QUEIMGNAME = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(fuQuestion.FileName)));

            }
            else
            {
                objQuest.QUESTIONIMAGE = null;
            }

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                if (file.ContentLength > 0)
                {
                    string path = file_path + "ITLE/upload_files/IMAGE_QUESTION/";
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string F = Convert.ToString(Convert.ToInt32(Session["userno"].ToString()) + "_" + objQuest.QUESTIONNO + "_" + Convert.ToString(System.IO.Path.GetFileName(file.FileName)));
                    // now save the file to the disk
                    file.SaveAs(path + F);
                }
            }
            CustomStatus cs = (CustomStatus)objIQBC.UpdateImage(objQuest, Convert.ToInt32(Session["OrgId"]));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
            }
        }

        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.InsertImage-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        rdbtnList.SelectedValue = "T";
        txtNewTopic.Text = string.Empty;
        //txtTopic.Text = string.Empty;
        lblCourseName.Text = string.Empty;
        txtQuestion.Text = "&nbsp;";
        txtAnswer1.Text = string.Empty;
        txtAnswer2.Text = string.Empty;
        txtAnswer3.Text = string.Empty;
        txtAnswer4.Text = string.Empty;
        txtAnswer5.Text = string.Empty;
        txtAnswer6.Text = string.Empty;
        ViewState["action"] = "add";

        //ddlQuestionMarks.SelectedIndex = -1;
        //ddlCorrectAns.SelectedIndex = -1;
        imgQuestions.ImageUrl = "~/images/nophoto.jpg";
        imgAnswer1.ImageUrl = "~/images/nophoto.jpg";
        imgAnswer2.ImageUrl = "~/images/nophoto.jpg";
        imgAnswer3.ImageUrl = "~/images/nophoto.jpg";
        imgAnswer4.ImageUrl = "~/images/nophoto.jpg";
        imgAnswer5.ImageUrl = "~/images/nophoto.jpg";
        imgAnswer6.ImageUrl = "~/images/nophoto.jpg";

        HideControls();
        HideObjectiveControls();
        ShowObjectiveControls();
        rbnObjectiveDescriptive.SelectedValue = "O";
    }

    private void HideControls()
    {
        imgAnswer1.Visible = false;
        imgAnswer2.Visible = false;
        imgAnswer3.Visible = false;
        imgAnswer4.Visible = false;
        imgAnswer5.Visible = false;
        imgAnswer6.Visible = false;
        imgQuestions.Visible = false;
        fuAnswer1.Visible = false;
        fuAnswer2.Visible = false;
        fuAnswer3.Visible = false;
        fuAnswer4.Visible = false;
        fuAnswer5.Visible = false;
        fuAnswer6.Visible = false;
        fuQuestion.Visible = false;
    }

    private void ShowDetail(int Question_no)
    {
        try
        {
            ViewState["Question_no"] = Question_no;
            objQuest.QUESTIONNO = Convert.ToInt32(Session["QUESNO"].ToString());
            objQuest.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"].ToString());
            DataSet ds = objIQBC.GetAllStudQuestionByUaNo(objQuest);
            if (ds.Tables[0].Rows.Count != null)
            {
                rdbtnList.SelectedValue = ds.Tables[0].Rows[0]["TYPE"].ToString();
                if (rdbtnList.SelectedValue == "I")
                {
                    ShowControls();
                }
                lblCourseName.Text = Session["ICourseName"].ToString();
                txtQuestion.Text = ds.Tables[0].Rows[0]["QUESTIONTEXT"].ToString();
                txtNewTopic.Text = ds.Tables[0].Rows[0]["TOPIC"].ToString();

                //hfTopic.Value = ds.Tables[0].Rows[0]["TOPIC"].ToString();

                if (ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() != string.Empty)
                {
                    imgQuestions.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ANS1IMG"].ToString() != string.Empty)
                {
                    imgAnswer1.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["ANS1IMG"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ANS2IMG"].ToString() != string.Empty)
                {
                    imgAnswer2.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["ANS2IMG"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ANS3IMG"].ToString() != string.Empty)
                {
                    imgAnswer3.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["ANS3IMG"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ANS4IMG"].ToString() != string.Empty)
                {
                    imgAnswer4.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["ANS4IMG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ANS5IMG"].ToString() != string.Empty)
                {
                    imgAnswer5.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["ANS5IMG"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ANS6IMG"].ToString() != string.Empty)
                {
                    imgAnswer6.ImageUrl = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[0]["ANS6IMG"].ToString();
                }

                txtAnswer1.Text = ds.Tables[0].Rows[0]["ANS1TEXT"].ToString();
                txtAnswer2.Text = ds.Tables[0].Rows[0]["ANS2TEXT"].ToString();
                txtAnswer3.Text = ds.Tables[0].Rows[0]["ANS3TEXT"].ToString();
                txtAnswer4.Text = ds.Tables[0].Rows[0]["ANS4TEXT"].ToString();
                txtAnswer5.Text = ds.Tables[0].Rows[0]["ANS5TEXT"].ToString();
                txtAnswer6.Text = ds.Tables[0].Rows[0]["ANS6TEXT"].ToString();
                //ddlQuestionMarks.SelectedValue = ds.Tables[0].Rows[0]["MARKS_FOR_QUESTION"].ToString();
                // ddlCorrectAns.SelectedValue = ds.Tables[0].Rows[0]["CORRECTANS"].ToString();
                pnlAdd.Visible = true;
                //pnllvView.Visible = false;
                //imgQuestions.Visible = true;
                //imgAnswer1.Visible = true;
                //imgAnswer2.Visible = true;
                //imgAnswer3.Visible = true;
                //imgAnswer4.Visible = true;
                //imgAnswer5.Visible = true;
                //imgAnswer6.Visible = true;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void HideObjectiveControls()
    {
        Answer1.Visible = false;
        Answer2.Visible = false;
        Answer3.Visible = false;
        Answer4.Visible = false;
        Answer5.Visible = false;
        Answer6.Visible = false;
        // CorrectAnwer.Visible = false;
        //MarksOnQuestion.Visible = true;
        trTextImage.Visible = false;


    }

    private void ShowControls()
    {
        imgAnswer1.Visible = true;
        imgAnswer2.Visible = true;
        imgAnswer3.Visible = true;
        imgAnswer4.Visible = true;
        imgAnswer5.Visible = true;
        imgAnswer6.Visible = true;
        imgQuestions.Visible = true;
        fuAnswer1.Visible = true;
        fuAnswer2.Visible = true;
        fuAnswer3.Visible = true;
        fuAnswer4.Visible = true;
        fuAnswer5.Visible = true;
        fuAnswer6.Visible = true;
        fuQuestion.Visible = true;
    }

    private void ShowObjectiveControls()
    {

        Answer1.Visible = true;
        Answer2.Visible = true;
        Answer3.Visible = true;
        Answer4.Visible = true;
        Answer5.Visible = true;
        Answer6.Visible = true;
        // CorrectAnwer.Visible = true;
        //MarksOnQuestion.Visible = false;
        trTextImage.Visible = true;

    }

    #endregion


    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            if (rbnObjectiveDescriptive.SelectedValue == "O")
            {
                #region Validation For Images
                //Added By Saket Singh on 21-August-2017
                //To Validate the Images

                if (ViewState["action"] == null && rdbtnList.SelectedValue == "I")
                {
                    if (fuQuestion.HasFile == false && fuAnswer1.HasFile == false && fuAnswer2.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Images", this.Page);
                        return;
                    }
                    else if (fuQuestion.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Question", this.Page);
                        return;
                    }
                    else if (fuAnswer1.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Answer 1", this.Page);
                        return;
                    }
                    else if (fuAnswer2.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Answer 2", this.Page);
                        return;
                    }
                    else
                    {
                    }

                    if (fuAnswer3.HasFile == true && txtAnswer3.Text == "")
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Answer 3", this.Page);
                        return;
                    }
                    else if (txtAnswer3.Text != "" && fuAnswer3.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Answer 3", this.Page);
                        return;
                    }
                    else
                    {
                    }

                    if (fuAnswer4.HasFile == true && txtAnswer4.Text == "")
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Answer 4", this.Page);
                        return;
                    }
                    else if (txtAnswer4.Text != "" && fuAnswer4.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Answer 4", this.Page);
                        return;
                    }
                    else
                    {
                    }

                    if (fuAnswer5.HasFile == true && txtAnswer5.Text == "")
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Answer 5", this.Page);
                        return;
                    }
                    else if (txtAnswer5.Text != "" && fuAnswer5.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Answer 5", this.Page);
                        return;
                    }
                    else
                    {
                    }

                    if (fuAnswer6.HasFile == true && txtAnswer6.Text == "")
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Answer 6", this.Page);
                        return;
                    }
                    else if (txtAnswer6.Text != "" && fuAnswer6.HasFile == false)
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Please Upload the Image for Answer 6", this.Page);
                        return;
                    }
                    else
                    {
                    }
                }
                else
                {

                }

                #endregion

                ///To Check Empty option in between...
                if (txtAnswer3.Text == "" & txtAnswer4.Text != "")
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Valid Option...!", this.Page);
                    //ddlCorrectAns.SelectedIndex = 0;
                    return;
                }
                else if (txtAnswer4.Text == "" & txtAnswer5.Text != "")
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Valid Option...!", this.Page);
                    //ddlCorrectAns.SelectedIndex = 0;
                    return;
                }
                else if (txtAnswer5.Text == "" & txtAnswer6.Text != "")
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Valid Option...!", this.Page);
                    //ddlCorrectAns.SelectedIndex = 0;
                    return;
                }


                ///To Check Selected Correct Ans...
                //if (ddlCorrectAns.SelectedIndex == 3 & txtAnswer3.Text == "")
                //{
                //    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Valid Option...!", this.Page);
                //    ddlCorrectAns.SelectedIndex = 0;
                //    return;
                //}
                //else if (ddlCorrectAns.SelectedIndex == 4 & txtAnswer4.Text == "")
                //{
                //    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Valid Option...!", this.Page);
                //    ddlCorrectAns.SelectedIndex = 0;
                //    return;
                //}
                //else if (ddlCorrectAns.SelectedIndex == 5 & txtAnswer5.Text == "")
                //{
                //    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Valid Option...!", this.Page);
                //    ddlCorrectAns.SelectedIndex = 0;
                //    return;
                //}
                //else if (ddlCorrectAns.SelectedIndex == 6 & txtAnswer6.Text == "")
                //{
                //    objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Valid Option...!", this.Page);
                //    ddlCorrectAns.SelectedIndex = 0;
                //    return;
                //}

            }
            {
                objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);


                objQuest.TOPIC = txtNewTopic.Text.Trim();
                objQuest.QUESTIONTEXT = txtQuestion.Text;
                objQuest.ANS1TEXT = txtAnswer1.Text;
                objQuest.ANS2TEXT = txtAnswer2.Text;
                objQuest.ANS3TEXT = txtAnswer3.Text;
                objQuest.ANS4TEXT = txtAnswer4.Text;
                objQuest.ANS5TEXT = txtAnswer5.Text;
                objQuest.ANS6TEXT = txtAnswer6.Text;
                //objQuest.MARKS_FOR_QUESTION = Convert.ToInt32(ddlQuestionMarks.SelectedValue);


                objQuest.OBJECTIVE_DESCRIPTIVE = Convert.ToChar(rbnObjectiveDescriptive.SelectedValue);
                objQuest.TYPE = Convert.ToChar(rdbtnList.SelectedValue);
               // objQuest.CORRECTANS = ddlCorrectAns.SelectedValue;
                objQuest.COLLEGE_CODE = Session["colcode"].ToString();
                objQuest.UA_NO = Convert.ToInt32(Session["userno"].ToString());

                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    if (ViewState["Question_no"] != null)

                        //objQuest.MARKS_FOR_QUESTION = Convert.ToInt32(ddlQuestionMarks.SelectedValue);
                    objQuest.QUESTIONNO = Convert.ToInt32(ViewState["Question_no"]);
                    if (rdbtnList.SelectedValue == "I")
                    {
                        InsertImage(objQuest.QUESTIONNO);
                    }
                    CustomStatus cs = (CustomStatus)objIQBC.UpdateStudQuestionBank(objQuest);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                        objCommon.DisplayMessage(UpdatePanel1, "Question Modified Successfully.....", this.Page);
                  

                }
                else //if (ViewState["action"] != null && ViewState["action"].ToString().Equals("add"))
                {
                    int cs = Convert.ToInt32(objIQBC.AddIStudentQuestionBank(objQuest));
                    if (rdbtnList.SelectedValue == "I")
                    {
                        InsertImage(cs);
                    }
                    //CustomStatus cs = (CustomStatus)objIQBC.AddIQuestionBank(objQuest);
                    if (cs == -99) //Transaction error
                    {
                        //objCommon.DisplayMessage(UpdatePanel1, "Question Saved Successfully", this.Page);
                        //lblStatus.Text = ("Record Saved Sucessfully");
                        //objCommon.DisplayMessage("Record Saved Sucessfully", this.Page);
                    }
                    else if (cs == -1001)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Question Already Exist.....", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Question Saved Successfully.....", this.Page);
                    }
                }

                pnllvView.Visible = true;
                pnlAdd.Visible = true;
                //pnlAdd.Visible = false;
                ClearControls();
               BindListView();
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdbtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbtnList.SelectedValue == "T")
        {
            HideControls();
            //tdlblAns1.Visible = true;
            //tdlblAns2.Visible = true;
            //tdlblAns3.Visible = true;
            //tdlblAns4.Visible = true;
            //tdlblAns5.Visible = true;
            //tdlblAns6.Visible = true;
            //BindListView();
            txtAnswer1.Visible = true;
            txtAnswer2.Visible = true;
            txtAnswer3.Visible = true;
            txtAnswer4.Visible = true;
            txtAnswer5.Visible = true;
            txtAnswer6.Visible = true;
        }
        else
        {
            ShowControls();
            //tdlblAns1.Visible = false;
            //tdlblAns2.Visible = false;
            //tdlblAns3.Visible = false;
            //tdlblAns4.Visible = false;
            //tdlblAns5.Visible = false;
            //tdlblAns6.Visible = false;

            //BindListView();
            //txtAnswer1.Visible = false;
            //txtAnswer2.Visible = false;
            //txtAnswer3.Visible = false;
            //txtAnswer4.Visible = false;
            //txtAnswer5.Visible = false;
            //txtAnswer6.Visible = false;

            txtAnswer1.Visible = true;
            txtAnswer2.Visible = true;
            txtAnswer3.Visible = true;
            txtAnswer4.Visible = true;
            txtAnswer5.Visible = true;
            txtAnswer6.Visible = true;

        }
    }
    protected void rbnObjectiveDescriptive_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindListView();
        if (rbnObjectiveDescriptive.SelectedValue == "D")
        {

            HideObjectiveControls();
            BindListView();
            imgQuestions.Visible = false;
            fuQuestion.Visible = false;
        }
        else
        {
            ShowObjectiveControls();
            BindListView();
            if (rdbtnList.SelectedValue == "I")
            {
                ShowControls();

            }
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
            int Question_no = int.Parse(btnEdit.CommandArgument);
            Session["QUESNO"] = Convert.ToInt32(Question_no);
            ShowDetail(Question_no);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        CheckPageRefresh();
        try
        {
            ImageButton btnDel = sender as ImageButton;
            objQuest.QUESTIONNO = int.Parse(btnDel.CommandArgument);
            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objQuest.UA_NO = Convert.ToInt32(Session["userno"].ToString());

            //IAnnouncementController objAC = new IAnnouncementController();

            CustomStatus cs = (CustomStatus)objIQBC.DeleteStudQuestion(objQuest);
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayMessage(UpdatePanel1, "Question Deleted Successfully", this.Page);
            //lblStatus.Text = ("Record Deleted Sucessfully");

            BindListView();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "QuestionBankMaster.aspx.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion


    
}