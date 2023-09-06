using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS.NITPRM;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Itle_ITLEResult : System.Web.UI.Page
{
    int ctr = 0;
    long timerStartValue = 1800;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITLETestController objIETestc = new ITLETestController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    int t = 0;
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Head1.DataBind();
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

                lblUrname.Text = Convert.ToString(Session["userfullname"].ToString());

                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title

                Page.Title = Session["coll_name"].ToString();



                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();

                //lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());
                lblTotQue.Text = objCommon.LookUp("ACD_ITEST_RESULT", "TOTQUESTION", "TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND COURSENO=" + Convert.ToInt32(Session["COURSENO_OBJ"]) + " AND IDNO=" + Session["idno"].ToString() + "");
                lblAnsQue.Text = objCommon.LookUp("ACD_ITLE_RESULTCOPY", "COUNT(QUESTIONNO) AS ANSWER", "ANS_STAT IN ('S','R') AND TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND COURSENO=" + Convert.ToInt32(Session["COURSENO_OBJ"]) + " AND IDNO=" + Session["idno"].ToString() + "");
                lblUnAnsQue.Text = objCommon.LookUp("ACD_ITLE_RESULTCOPY", "COUNT(QUESTIONNO) AS NOT_ANSWER", "ANS_STAT != 'S' AND ANS_STAT != 'R' AND TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND COURSENO=" + Convert.ToInt32(Session["COURSENO_OBJ"]) + " AND IDNO=" + Session["idno"].ToString() + "");
                lblRightAns.Text = objCommon.LookUp("ACD_ITEST_RESULT", "CORRECTANS", "TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND COURSENO=" + Convert.ToInt32(Session["COURSENO_OBJ"]) + " AND IDNO=" + Session["idno"].ToString() + "");
                int wrongAns = Convert.ToInt32(lblAnsQue.Text) - Convert.ToInt32(lblRightAns.Text);
                lblWrongAns.Text = wrongAns.ToString();
                lblScore.Text = lblRightAns.Text.ToString() + "/" + lblTotQue.Text.ToString();

                string STATUS = objCommon.LookUp("ACD_ITESTMASTER", "trim(SHOWKEY)", "TESTNO=" + Session["Test_No_OBJ"].ToString());
                if (STATUS == "Y")
                {
                    btnshowkey.Visible = true;

                }
                else
                {
                    btnshowkey.Visible = false;
                }

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //cancelButton.Attributes.Add("onclick", "window.close();");

            }

        }
        divCollegename.InnerText = objCommon.LookUp("REFF", "COLLEGENAME", "");

    }


    protected void btnOK_Click(object sender, EventArgs e)
    {

        Session["TOTSCORE"] = 0;
        Session["TOTALMARKS"] = 0;
        Session["NOCORRANS"] = 0;
        Session["TOTANSQUE"] = 0;
        Session["Test_No_OBJ"] = string.Empty;
        Session["COURSENO_OBJ"] = string.Empty;
        Session["CURRTESTENDTIME"] = string.Empty;
        Session["TDNO_OBJ"] = string.Empty;
        Session["CurQuesIndex"] = string.Empty;
        Session["CurQuesNo_OBJ"] = string.Empty;
        Session["NextQuesNo"] = string.Empty;
        Session["PrevQuesNo"] = string.Empty;
        Session["TOTQUES"] = string.Empty;
        Session["ANSWER_TYPE"] = string.Empty;
        Response.Redirect("~/Itle/StudTest.aspx?pageno=1476");

    }


    protected void Page_Unload(object sender, EventArgs e)
    {
        Session["TOTSCORE"] = 0;
        Session["TOTALMARKS"] = 0;
        Session["NOCORRANS"] = 0;
        Session["TOTANSQUE"] = 0;
    }

    private void BindShowQues()
    {
        try
        {
            //SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);


            RadioButtonList rdBtnQuesList = (RadioButtonList)(RpCourse.Items[0]).FindControl("rdBtnQuesList");
            CheckBoxList ckBtnQuesList = (CheckBoxList)(RpCourse.Items[0]).FindControl("ckBtnQuesList");
            Label Label2 = (Label)(RpCourse.Items[0]).FindControl("lblPerson");
            Image imgQuesImage = (Image)(RpCourse.Items[0]).FindControl("imgQuesImage");


            if (Session["qNoList_OBJ"] == null)
            {
                DataSet qNods = objIETestc.GetQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));

                List<int> qNoList = new List<int>();
                foreach (DataRow dtr in qNods.Tables[0].Rows)
                {
                    qNoList.Add(Convert.ToInt32(dtr["QUESTIONNO"]));
                }
                Session["qNoList_OBJ"] = qNoList;
            }



            int[] _myArrOp = (int[])Session["RandAnsSrList_OBJ"];
            List<int> QuesNoList = (List<int>)Session["qNoList_OBJ"];
            for (int i = 0; i < QuesNoList.Count; i++)
            {
                if (Session["CurQuesNo_OBJ"] == null)
                {
                    Session["CurQuesIndex"] = i + 1;
                    Session["CurQuesNo_OBJ"] = QuesNoList[i];
                    Session["NextQuesNo"] = QuesNoList[i + 1];
                    //  btnPrev.Visible = true;
                    // btnNext.Visible = true;
                    break;
                }

            }








            DataSet ds = objIETestc.GetNextQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()));


            int TotQue = QuesNoList.Count;
            Session["TOTQUES"] = TotQue;
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["COURSENO"].ToString()))
            {

                // Label1.Text = Session["CurQuesIndex"].ToString();
                Label2.Text = ds.Tables[0].Rows[0]["QUESTIONTEXT"].ToString().Replace("<img", @"<img class='img-responsive'");
                //  Label3.Text = Session["CurQuesIndex"].ToString();
                // Label4.Text = Session["TOTQUES"].ToString();
                string imageBankFolder = "ITLE_QuestionImage.aspx?FileName=";
                if (ds.Tables[0].Rows[0]["TYPE"].ToString().Equals("T"))
                {

                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString()) || ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() == "")
                    {
                        imgQuesImage.Visible = false;
                    }
                    else
                    {
                        imgQuesImage.ImageUrl = imageBankFolder + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                        imgQuesImage.Visible = true;
                    }
                    rdBtnQuesList.Items.Clear();
                    ckBtnQuesList.Items.Clear();
                    for (int j = 0; j < 6; j++)
                    {
                        int g = Convert.ToInt32(_myArrOp.GetValue(j));

                        if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {
                                rdBtnQuesList.Items.Add(new ListItem(ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Replace("<img", @"<img class='img-responsive'"), g.ToString().Trim()));

                            }

                        }
                        else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {

                                ckBtnQuesList.Items.Add(new ListItem(ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Replace("<img", @"<img class='img-responsive'"), g.ToString().Trim()));

                            }

                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString()) || ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() == "")
                    {
                        imgQuesImage.Visible = false;
                    }
                    else
                    {
                        imgQuesImage.ImageUrl = imageBankFolder + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                        imgQuesImage.Visible = true;
                    }

                    rdBtnQuesList.Items.Clear();
                    ckBtnQuesList.Items.Clear();



                    for (int j = 0; j < 6; j++)
                    {
                        int g = Convert.ToInt32(_myArrOp.GetValue(j));
                        if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString() != "")
                                rdBtnQuesList.Items.Add(new ListItem("<img class='img-responsive' src=" + imageBankFolder + ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString().Trim() + " >" + ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Trim(), g.ToString().Trim()));



                        }
                        else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {

                                ckBtnQuesList.Items.Add(new ListItem("<img class='img-responsive' src=" + imageBankFolder + ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString().Trim() + " >" + ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Trim(), g.ToString().Trim()));

                            }

                        }

                    }
                }

                if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                {
                    Session["ANSWER_TYPE"] = "R";
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CORRECTANS"].ToString()))
                    {

                        rdBtnQuesList.SelectedValue = ds.Tables[0].Rows[0]["CORRECTANS"].ToString().Trim();
                        // rdBtnQuesList.SelectedValue = ConsoleColor.Green.ToString();

                        foreach (ListItem li in rdBtnQuesList.Items)
                        {
                            if (li.Selected)
                            {
                                li.Attributes.Add("style", "color: Green");
                            }
                        }
                    }
                    else
                    {

                        foreach (ListItem li in rdBtnQuesList.Items)
                        {
                            li.Selected = false;
                        }

                        rdBtnQuesList.SelectedValue = null;
                    }

                    ckBtnQuesList.Visible = false;
                    rdBtnQuesList.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                {

                    Session["ANSWER_TYPE"] = "C";
                    foreach (ListItem li in ckBtnQuesList.Items)
                    {
                        if (ds.Tables[0].Rows[0]["CORRECTANS"].ToString().Contains(li.Value))
                        {
                            li.Selected = true;
                        }

                    }
                    rdBtnQuesList.Visible = false;
                    ckBtnQuesList.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Test.BindShowQues -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void DisplayPreview()
    {
        //DataSet ds = objIETestc.DisplayPreview(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));


        //lvPreview.DataSource = ds;
        //lvPreview.DataBind();


        //DataSet ds1 = objIETestc.BindPreviewQuestionCount(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));

        //lblAnsCount.Text = ds1.Tables[0].Rows[0]["ANSWER"].ToString();
        //lblRevCount.Text = ds1.Tables[1].Rows[0]["REVIEW"].ToString();
        //lblSkipCount.Text = ds1.Tables[2].Rows[0]["SKIP"].ToString();
        //lblNotAnsCount.Text = ds1.Tables[3].Rows[0]["NOT_ANSWER"].ToString();
    }
    protected void btnQuesStatus_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btnQuesStatus = sender as LinkButton;
            int selectQuesNo = Convert.ToInt32(int.Parse(btnQuesStatus.CommandArgument));
            int selectedSrNo = Convert.ToInt32(btnQuesStatus.CommandName);
            int curQuesIndex = Convert.ToInt32(Session["CurQuesIndex"].ToString());
            string selPrevQAnsStat = string.Empty;
            string ansStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), selectQuesNo, Convert.ToInt32(Session["idno"].ToString()));

            //objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS_STAT", "TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND QUESTIONNO=" + selectQuesNo + "AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
            List<int> QuesNoList = (List<int>)Session["qNoList_OBJ"];
            for (int i = 0; i < QuesNoList.Count; i++)
            {
                if (selectQuesNo == QuesNoList[i])
                {
                    if (i != 0)
                    {
                        selPrevQAnsStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(QuesNoList[i - 1].ToString()), Convert.ToInt32(Session["idno"].ToString()));
                        //objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS_STAT", "QUESTIONNO=" + QuesNoList[i - 1].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + "AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
                    }
                    break;
                }
            }
            //if (curQuesIndex + 1 < selectedSrNo && ansStat == "U" && selPrevQAnsStat == "U")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
            //    return;
            //}
            string CURansStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()), Convert.ToInt32(Session["idno"].ToString()));
            //if (curQuesIndex != selectedSrNo)
            //{

            if (CURansStat.Trim() == "U")
            {
                int questionno = 0;
                int testno = 0;
                int idno = 0;
                questionno = Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString());
                testno = Convert.ToInt32(Session["Test_No_OBJ"].ToString());
                idno = Convert.ToInt32(Session["idno"].ToString());
                // CustomStatus cs = (CustomStatus)objIETestc.UpdateQuestionStatus(questionno, testno, idno);
            }
            //}
            Session["CurQuesNo_OBJ"] = selectQuesNo;
            BindShowQues();
            //  DisplayPreview();
            //dvIntermediate.Visible = false;
            // dvShowTestStatus.Visible = false;
        }
        //  }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnQuesStatus_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnshowkey_Click(object sender, EventArgs e)
    {
        //BindShowQues();
        //DisplayPreview();
        BindQuestion();
        divanskye.Visible = true;
        perinfo.Visible = false;
    }
    protected void linkbtnback_Click(object sender, EventArgs e)
    {
        perinfo.Visible = true;
        divanskye.Visible = false;
    }

    private void BindQuestion()
    {

        //if (Session["qNoList_OBJ"] != null)
        //{
        DataSet qNods = objIETestc.GetQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));

        //List<int> qNoList = new List<int>();
        //foreach (DataRow dtr in qNods.Tables[0].Rows)
        //{
        //    qNoList.Add(Convert.ToInt32(dtr["QUESTIONNO"]));
        //}
        //Session["qNoList_OBJ"] = qNoList;

        if (qNods != null)
        {

            RpCourse.DataSource = qNods;
            RpCourse.DataBind();

            //string answer = objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS1TEXT,ANS1IMG,ANS2TEXT,ANS2IMG,ANS3TEXT,ANS3IMG,ANS4TEXT,ANS4IMG,ANS5TEXT,ANS5IMG,ANS6TEXT,ANS6IMG", "TESTNO=" + Convert.ToInt32(Session["Test_No_OBJ"].ToString() + "and IDNO =" + Convert.ToInt32(Session["idno"])+ " AND COURSENO="+Convert.ToInt32(Session["COURSENO_OBJ"].ToString())));
            //answer = objCommon.FillDropDown("dbo.split('" + ViewState["DeptNo"].ToString() + "','$')", "distinct value as DeptNo", "", "", "");

        }
        // }


    }
    protected void RpCourse_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {



            if (Session["qNoList_OBJ"] == null)
            {
                DataSet qNods = objIETestc.GetQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));
                List<int> qNoList = new List<int>();
                foreach (DataRow dtr in qNods.Tables[0].Rows)
                {
                    qNoList.Add(Convert.ToInt32(dtr["QUESTIONNO"]));
                }
                Session["qNoList_OBJ"] = qNoList;
            }



            int[] _myArrOp = (int[])Session["RandAnsSrList_OBJ"];

            HiddenField hdnQuestioId = e.Item.FindControl("hdnQuestioId") as HiddenField;
            Label Label3 = e.Item.FindControl("Label3") as Label;
            Label Label4 = e.Item.FindControl("Label4") as Label;
            Label Label2 = e.Item.FindControl("Label2") as Label;
            Image imgQuesImage = e.Item.FindControl("imgQuesImage") as Image;
            RadioButtonList rdBtnQuesList = e.Item.FindControl("rdBtnQuesList") as RadioButtonList;
            CheckBoxList ckBtnQuesList = e.Item.FindControl("ckBtnQuesList") as CheckBoxList;
            ListView lvanswer = e.Item.FindControl("lvanswer") as ListView;


            DataSet ds = objIETestc.GetNextQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(hdnQuestioId.Value));



            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["COURSENO"].ToString()))
            {

                Label2.Text = ds.Tables[0].Rows[0]["QUESTIONTEXT"].ToString().Replace("<img", @"<img class='img-responsive'");

                string imageBankFolder = "ITLE_QuestionImage.aspx?FileName=";
                if (ds.Tables[0].Rows[0]["TYPE"].ToString().Equals("T"))
                {

                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString()) || ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() != "")
                    {
                        imgQuesImage.Visible = false;
                    }
                    else
                    {
                        imgQuesImage.ImageUrl = imageBankFolder + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                        imgQuesImage.Visible = true;
                    }
                    DataSet dans = null;
                    dans = new DataSet();
                    DataTable dt1 = new DataTable();
                    dans.Tables.Add(dt1);

                    dans.Tables[0].Columns.Add("Answer", System.Type.GetType("System.String"));


                    for (int j = 0; j < 6; j++)
                    {
                        int g = Convert.ToInt32(_myArrOp.GetValue(j));

                        if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {

                                dans.Tables[0].Rows.Add((ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Replace("<img", @"<img class='img-responsive'")));
                            }

                        }
                        else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {
                                dans.Tables[0].Rows.Add((ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Replace("<img", @"<img class='img-responsive'")));

                            }

                        }
                    }
                    lvanswer.DataSource = dans;
                    lvanswer.DataBind();

                }
                else
                {
                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString()) || ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() == "")
                    {
                        imgQuesImage.Visible = false;
                    }
                    else
                    {
                        imgQuesImage.ImageUrl = imageBankFolder + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                        imgQuesImage.Visible = true;
                    }
                    //rdBtnQuesList.Items.Clear();
                   // ckBtnQuesList.Items.Clear();

                    DataSet dans = null;
                    dans = new DataSet();
                    DataTable dt1 = new DataTable();
                    dans.Tables.Add(dt1);

                    dans.Tables[0].Columns.Add("Answer", System.Type.GetType("System.String"));


                    for (int j = 0; j < 6; j++)
                    {
                        int g = Convert.ToInt32(_myArrOp.GetValue(j));
                        if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString() != "")
                            {
                                dans.Tables[0].Rows.Add(("<img class='img-responsive' src=" + imageBankFolder + ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString().Trim() + " >" + ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Trim()));

                            }
                        }
                        else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {
                                dans.Tables[0].Rows.Add(("<img class='img-responsive' src=" + imageBankFolder + ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString().Trim() + " >" + ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Trim()));

                            }

                        }

                    }
                    lvanswer.DataSource = dans;
                    lvanswer.DataBind();
                }


                // DataSet ds1 = objIETestc.GetNextQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(hdnQuestioId.Value));



                if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                {
                    Session["ANSWER_TYPE"] = "R";
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CORRECTANS"].ToString()))
                    {

                        //rdBtnQuesList.SelectedValue = ds.Tables[0].Rows[0]["CORRECTANS"].ToString().Trim();
                        //rdBtnQuesList.ForeColor = System.Drawing.Color.Red;
                        foreach (ListViewDataItem lvItem in lvanswer.Items)
                        {
                            Label labelanswer = lvItem.FindControl("labelanswer") as Label;
                            Image imgLogo = lvItem.FindControl("imgLogo") as Image;
                            HiddenField hdrow = lvItem.FindControl("hdrow") as HiddenField;
                            if (hdrow.Value.ToString() == ds.Tables[0].Rows[0]["CORRECTANS"].ToString().Trim())
                            {
                                imgLogo.ImageUrl = "~/Images/correctansr.png";
                            }
                            else
                            {
                                imgLogo.ImageUrl = "~/Images/ansbtn.png";
                            }
                            // when answer not save
                            //if (ds.Tables[0].Rows[0]["SELECTED"].ToString().Trim() == "")
                            //{
                            //    imgLogo.ImageUrl = "~/Images/ansbtn.png";
                            //}

                            if (hdrow.Value.ToString() == ds.Tables[0].Rows[0]["SELECTED"].ToString().Trim())
                            {

                                if (ds.Tables[0].Rows[0]["CORRECTANS"].ToString().Trim() == ds.Tables[0].Rows[0]["SELECTED"].ToString().Trim())
                                {
                                    imgLogo.ImageUrl = "~/Images/correctansr.png";
                                }
                                else
                                {
                                    imgLogo.ImageUrl = "~/Images/wrongasnwer.png";
                                }
                                
                            }



                        }
                    }

                }
                else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                {

                    Session["ANSWER_TYPE"] = "C";
                    //foreach (ListItem li in ckBtnQuesList.Items)
                    //{
                    //    if (ds.Tables[0].Rows[0]["CORRECTANS"].ToString().Contains(li.Value))
                    //    {
                    //        li.Selected = true;
                    //    }

                    //}
                    //rdBtnQuesList.Visible = false;
                    //ckBtnQuesList.Visible = true;
                }





            }

        }






        //  ListView lvanswer = e.Item.FindControl("lvanswer") as ListView;
        // lvanswer.DataSource = ds;
        // lvanswer.DataBind();
    }
}

