using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Saplin.Controls;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using DynamicAL_v2;

public partial class OBE_ExamQuestionPattern : System.Web.UI.Page
{
    CommonModel ObjComModel = new CommonModel();
    Common objCommon = new Common();
    CommanController ObjComm = new CommanController();
    ExamQuestionPatternController ObjQP = new ExamQuestionPatternController();
    DynamicControllerAL AL = new DynamicControllerAL();


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

                hdUserId.Value = Session["userno"].ToString();
                //Check Faculty
                //if (Convert.ToInt32(Session["usertype"]) != 3)
                //{

                //    if (Convert.ToInt32(Session["usertype"]) == 8)
                //    {

                //    }
                //    else
                //    {
                //        objCommon.DisplayMessage("Only Faculty has Authority To Work On This Page.", this.Page);

                //        return;
                //    }
                //}
                //else
                //{
                    
                //}

                //Page Authorization

                //CheckPageAuthorization();


                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                FillDropDownList();
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
                Response.Redirect("~/notauthorized.aspx?page=ExamQuestionPattern.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamQuestionPattern.aspx");
        }
    }


    private void FillDropDownList()
    {
        BindPattern(0,0);
    }

    protected void ddlWorkStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as DropDownList).NamingContainer as RepeaterItem;
        ListBox ddlOptionWith = (item.FindControl("ddlOptionWith") as ListBox);
        string Status = ((item.FindControl("ddlWorkStatus") as DropDownList).Text);
            if (Status == "2")
            {
               
                ddlOptionWith.Enabled = true;
            }
            else
            {
                ddlOptionWith.Enabled = false;
                //ddlOptionWith.SelectedIndex = 0;
                ddlOptionWith.ClearSelection();
            }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        divMain.Visible = true;
        BindPattern(0, 0);
        divlowerpanel.Visible = false;
        divSubject.Visible = true;
        ViewState["FMARKS"] = "";
        Session["Temp"] = "";
        //dvSession.Visible = true;
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ClearPattern();
        hdnPatternId.Value = "0";
        Session["Temp"] = "";
    }
    public void ClearPattern()
    {
        txtQuestionPatternName.Text = "";
        txtPatternMarks.Text = "";
        BindPattern(0, 0);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearQuestionNo();
        BindPatternDetailsToGridview();
        Session["Temp"] = "";
    }

    public void ClearQuestionNo()
    {
        ddlQueLevel.SelectedIndex = 0;
        hdExamPatternSubId.Value = "";
        ddlParentQuestion.SelectedIndex = 0;
        ddlQueOrWith.SelectedIndex = 0; 
        txtQuestionNo.Text=string.Empty;
        txtQueDescription.Text = string.Empty; 
        txtQuestionMarks.Text = string.Empty; 
        txtAttemptMinimum.Text = string.Empty; 
        txtOutOfQuestion.Text = string.Empty; 

    }
    
    protected void btnSubmitPatten_Click(object sender, EventArgs e)
    {
        try
        {

            //string Chk = objCommon.LookUp("tblQuestionPatternMaster", "COUNT(1)", "QuestionPatternName ='" + txtQuestionPatternName.Text  + "'");

            //if ((Chk != null || Chk != string.Empty) && Chk != "0")
            //{
            //    objCommon.DisplayMessage(updEdit, " Record Already Exist..", this.Page);
            //    return;  

            //}
           
            if(hdnPatternId.Value == "")
            {
                hdnPatternId.Value = "0";
            }
            int result = ObjQP.SaveQuestionPattern(Convert.ToInt32(hdnPatternId.Value), txtQuestionPatternName.Text, txtPatternMarks.Text);

            if (result == 1)
            {
                objCommon.DisplayMessage(updEdit, "Question Pattern Saved Successfully..", this.Page);

                dvQuestionPaper.Visible = false;
                ClearPattern();
               
            }
            if (result == 2)
            {
                objCommon.DisplayMessage(updEdit, "Question Pattern Update Successfully..", this.Page);
                hdnPatternId.Value = "0";
                dvQuestionPaper.Visible = false;
                ClearPattern();
                
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Question Pattern Not Updated/Saved..", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamQuestionPattern.btnSubmitPatten_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
     // Session["QuestionPatternId"]
       
        if (ddlQueLevel.SelectedValue == "1")
        {
            if (ddlQueOrWith.SelectedValue == "0")
            {
                int txtmin = txtAttemptMinimum.Text == string.Empty ? 0 : Convert.ToInt32(txtAttemptMinimum.Text);
                int txtoutof = txtOutOfQuestion.Text == string.Empty ? 0 : Convert.ToInt32(txtOutOfQuestion.Text);
                if (txtmin > txtoutof)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Attempt Minimum(" + txtmin + ")Should not greater than out of (" + txtoutof + ")questions')", true);
                    return;
                }
                if (txtAttemptMinimum.Text == string.Empty)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please Enter Attempt Minimum');", true);
                    return;

                }
                if (txtOutOfQuestion.Text == string.Empty)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please Enter Out Of Questions');", true);
                    return;
                }
            }
            
        
        }
        if (ddlQueLevel.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please Select Question Level.');", true);
            ddlQueLevel.Focus();
            return;
        }
        else if (ddlParentQuestion.Items.Count > 1 && Convert.ToInt32(ddlQueLevel.SelectedValue) != 1 && ddlParentQuestion.SelectedValue == "0")
        {
            
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please Select Parent Question.');", true);
                ddlParentQuestion.Focus();
                return;
        }
        else if (txtQuestionNo.Text==string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please Enter Display Question Number');", true);
            txtQuestionNo.Focus();
            return;
        }
        else if (txtQuestionMarks.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please Enter Question Marks.');", true);
            txtQuestionMarks.Focus();
            return;
        }


        #region For checking Questions can not more than Out of questions for level 2 questions added on 03082023
        if (ddlQueLevel.SelectedValue == "2" && Session["Temp"] != "T")
        {
            string parent = Convert.ToString(ddlParentQuestion.SelectedItem);
            decimal QMarks = Convert.ToDecimal(txtQuestionMarks.Text);
            int NO_of_question = Convert.ToInt32(objCommon.LookUp("tblQuestionPatternDetails", "NO_of_question", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and QuestionNumber='" + parent + "'"));
            int QuestionPatternSubID = Convert.ToInt32(objCommon.LookUp("tblQuestionPatternDetails", "QuestionPatternSubID", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and QuestionNumber='" + parent + "'"));
            int Count = Convert.ToInt32(objCommon.LookUp("tblQuestionPatternDetails", "count(*)", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and Parent_Question=" + QuestionPatternSubID));
            Decimal PARENT_QUESTION_MARKS = Convert.ToDecimal(objCommon.LookUp("tblQuestionPatternDetails", "QUESTION_MARKS", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and QuestionNumber='" + parent + "'"));

            decimal Pmarks = Convert.ToDecimal(txtQuestionMarks.Text);
            
            if (ddlQueOrWith.SelectedValue == "0")
            {
                
                if (Count == NO_of_question)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Questions Should not more than Out of questions');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Only (" + NO_of_question + ") Questions Should Allowed');", true);
                    return;

                }
                if (QMarks > PARENT_QUESTION_MARKS)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Question Marks (" + QMarks + " )Should not more than of Parent question Marks(" + PARENT_QUESTION_MARKS + ")');", true);
                    return;

                }
            }
        }

       #endregion


        ExamQuestionPaperController objExmPCon = new ExamQuestionPaperController();
        int MarkEntry = 0;

        try
         {

             //*************Added on 14072023*****************
             //int TMARK = Convert.ToInt32(txtQuestionMarks.Text);
             //string num = ViewState["FMARKS"].ToString();
             //decimal Fmark = Convert.ToDecimal(num);
             //decimal trmark1 = Convert.ToDecimal(TMARK);
             //if (trmark1 > Fmark)
             //{
             //    objCommon.DisplayMessage(this.Page, "Question Marks can not greater than Max Marks", this.Page);
             //    return;
             //}
             //***********************************************
             string descript = string.Empty;
             string SP = "PKG_ACD_PATTERN_DETAILS_INSERT_UPDATE";
             string PR = "@P_QuestionPatternSubID, @P_QuestionNumber, @P_Level_Id, @P_No_of_question, @P_Solve_no_of_question, @P_Question_Pattern_ID, @P_QUESTION_MARKS, @P_Que_Or_With, @P_Que_Description, @P_Parent_Question, @P_MarkEntry, @P_ISLOCKED, @P_OUTPUT";
             string VL = "";

             if (txtOutOfQuestion.Text == "")
             {
                 txtOutOfQuestion.Text = "0";
             }

             if (txtAttemptMinimum.Text == "")
             {
                 txtAttemptMinimum.Text = "0";
             }
             if (txtQueDescription.Text == "")
             {
                 descript = "Question Description";
             }
             else
             {
                 descript = txtQueDescription.Text;

             }
             if (hdExamPatternSubId.Value == "")
             {
                 hdExamPatternSubId.Value = "0";
             }
             if (txtOutOfQuestion.Text == "" || txtOutOfQuestion.Text == "0" || txtAttemptMinimum.Text == "" || txtAttemptMinimum.Text == "0")
             {
                 MarkEntry = 1;
             }
             else
             {
                 MarkEntry = 0;
             }


             if (ddlQueOrWith.SelectedValue != "0")
            {
                VL = "" + Convert.ToInt32(-1) + ", 0, " + Convert.ToInt32(0) + ", " + Convert.ToInt32(0) + ", " + Convert.ToInt32(0) + ", " + Convert.ToInt32(hdPatternId.Value) + ", 0, " + Convert.ToInt32(-1) + ", OR, " + Convert.ToInt32(0) + "," + MarkEntry + ",0,0";

                int result1 = Convert.ToInt32(AL.DynamicSPCall_IUD(SP, PR, VL, true, 1));

                //int result1 = ObjQP.SaveQuestionPatternDetails(Convert.ToInt32(-1), "0", Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(hdPatternId.Value), "0", Convert.ToInt32(-1), "OR", Convert.ToInt32(0),MarkEntry);
            }

             VL = "" + Convert.ToInt32(hdExamPatternSubId.Value) + ", " + Convert.ToString(txtQuestionNo.Text) + ", " + Convert.ToInt32(ddlQueLevel.SelectedValue) + "," + Convert.ToInt32(txtOutOfQuestion.Text) + ", " + Convert.ToInt32(txtAttemptMinimum.Text) + ", " + Convert.ToInt32(hdPatternId.Value) + ", " + Convert.ToString(txtQuestionMarks.Text) + ", " + Convert.ToInt32(ddlQueOrWith.SelectedValue) + ", " + Convert.ToString(descript) + ", " + Convert.ToInt32(ddlParentQuestion.SelectedValue) + "," + Convert.ToString(MarkEntry) + ",0,0";

             int result = Convert.ToInt32(AL.DynamicSPCall_IUD(SP, PR, VL, true, 1));

             //int result = ObjQP.SaveQuestionPatternDetails(Convert.ToInt32(hdExamPatternSubId.Value), txtQuestionNo.Text, Convert.ToInt32(txtOutOfQuestion.Text), Convert.ToInt32(txtAttemptMinimum.Text), Convert.ToInt32(ddlQueLevel.SelectedValue), Convert.ToInt32(hdPatternId.Value), txtQuestionMarks.Text, Convert.ToInt32(ddlQueOrWith.SelectedValue), descript, Convert.ToInt32(ddlParentQuestion.SelectedValue), MarkEntry);
              
            if (result == 1)
            {
                objCommon.DisplayMessage(updEdit, "Question Added Successfully..", this.Page);

                dvQuestionPaper.Visible = false;
                ClearQuestionNo();
                BindPatternDetailsToGridview();
                Session["Temp"] = "";
            }
            if (result == 2)
            {
                objCommon.DisplayMessage(updEdit, "Question Updated Successfully..", this.Page);
                hdnSectionId.Value = "0";
                dvQuestionPaper.Visible = false;
                ClearQuestionNo();
                BindPatternDetailsToGridview();
                Session["Temp"] = "";
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Question Not Updated/Saved..", this.Page);
                Session["Temp"] = "";
            }
        }

        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamQuestionPaper.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Added by Swapnil Thakare on dated 06-05-2021
    public void BindPattern(int userno, int session)
    {
        DataSet ds = ObjQP.GetPattern();
        
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rptCourse.DataSource = ds;
                rptCourse.DataBind();
                divSubject.Visible =  true;
            }

        }
    }

    public void BindPatternDetailsToGridview()
    {

        DataSet ds1 = ObjQP.GetPatternData(Convert.ToInt32(hdPatternId.Value), Convert.ToInt32(0));
        if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
        {

            string SP = "OBE_FILL_DROPDOWN";
            string PR = "@P_WHICH_ONE, @P_VAL_1, @P_VAL_2, @P_VAL_3, @P_VAL_4"; ;
            string VL = "1," + Convert.ToInt32(hdPatternId.Value) + ",0,0,0";
            DataSet ds = AL.DynamicSPCall_Select(SP, PR, VL);
            if (ds.Tables[0].Rows.Count > 0)
            {

                //objCommon.FillDropDownList(ddlParentQuestion, "tblQuestionPatternDetails", "QuestionPatternSubID", "QuestionNumber", "QuestionPatternSubID>0 AND Question_Pattern_ID=" + hdPatternId.Value + " AND Level_Id IN(1,2)", "QuestionPatternSubID");
                //objCommon.FillDropDownList(ddlQueOrWith, "tblQuestionPatternDetails", "QuestionPatternSubID", "QuestionNumber", "QuestionPatternSubID>0 AND Question_Pattern_ID="+ hdPatternId.Value +" AND Level_Id IN(1,2)", "QuestionPatternSubID");

                ddlParentQuestion.Items.Clear();
                ddlParentQuestion.Items.Add(new ListItem("Please Select", "0"));
                ddlQueOrWith.Items.Clear();
                ddlQueOrWith.Items.Add(new ListItem("Please Select", "0"));

                ddlParentQuestion.DataSource = ds.Tables[0];
                ddlParentQuestion.DataTextField = "QuestionNumber";
                ddlParentQuestion.DataValueField = "QuestionPatternSubID";
                ddlParentQuestion.DataBind();

                ddlQueOrWith.DataSource = ds.Tables[0];
                ddlQueOrWith.DataTextField = "QuestionNumber";
                ddlQueOrWith.DataValueField = "QuestionPatternSubID";
                ddlQueOrWith.DataBind();


              
                rptExamQuestion.DataSource = ds1;
                rptExamQuestion.DataBind();
                if (Convert.ToInt32(ds1.Tables[0].Rows[0]["ISLOCKED"]) == 1)
                    btnSave.Enabled = btnLock.Enabled = false;
                else
                    btnSave.Enabled = btnLock.Enabled = true;

             
                dvQuestionPaper.Visible = true;
                rptExamQuestion.Visible = true;
                dvSubmitData.Visible = true;
            }

        }
        else
        {
            objCommon.DisplayMessage(updEdit, "Pattern Not Created For This Subject, '", this.Page);
            rptExamQuestion.DataSource = null;
            rptExamQuestion.DataBind();
            ddlParentQuestion.Items.Clear();
            ddlParentQuestion.Items.Add(new ListItem("Please Select", "0"));
            ddlQueOrWith.Items.Clear();
            ddlQueOrWith.Items.Add(new ListItem("Please Select", "0"));
            btnSave.Enabled = btnLock.Enabled = true;
        }
    }

    protected void rptCourse_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
       

       // var duplicates = ds1.Tables[0].Columns[0]["GroupID"].GroupBy(r => r[0]).Where(gr => gr.Count() > 1).ToList();

        //DataTable tableBackColor;
        //foreach (DataColumn column in ds1.Tables[0].Columns)
        //{
            
        //}
    }

    protected void lnkConfigure(object sender, EventArgs e)
    {
        divMain.Visible = false;
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
        ddlQueLevel.SelectedIndex = 0;
        ddlParentQuestion.SelectedIndex = 0;
        ddlQueOrWith.SelectedIndex = 0;
        txtQuestionNo.Text = string.Empty;
        txtQueDescription.Text = string.Empty;
        txtQuestionMarks.Text = string.Empty;
        divSubject.Visible = false;
        //dvSession.Visible = false;
        divlowerpanel.Visible = true;
        lblPatternName.Text = (item.FindControl("lblSubjectName") as Label).Text;
        lblMarks.Text = (item.FindControl("lblMarks") as Label).Text;

       // ViewState["FMARKS"] = lblMarks.Text; //added on 14072023
       

        hdPatternId.Value = ((item.FindControl("hdnSrno1") as HiddenField).Value);
        Session["QuestionPatternId"] = hdPatternId.Value;
        BindPatternDetailsToGridview();
    }

    protected void lnkPatternEdit(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
        string SubQuePatternId = ((item.FindControl("hdSubQuePatternId") as HiddenField).Value);
        DataSet QuestionData = ObjQP.GetQuestionPatternDetails(Convert.ToInt32(SubQuePatternId));
        if (QuestionData != null && QuestionData.Tables[0].Rows.Count > 0)   
        {


            hdExamPatternSubId.Value = ((item.FindControl("hdSubQuePatternId") as HiddenField).Value);
            ddlQueLevel.SelectedValue = QuestionData.Tables[0].Rows[0]["Level_Id"].ToString();
            ddlParentQuestion.SelectedValue = QuestionData.Tables[0].Rows[0]["Parent_Question"].ToString();
            ddlQueOrWith.SelectedValue = QuestionData.Tables[0].Rows[0]["Que_Or_With"].ToString();
            txtQuestionNo.Text = QuestionData.Tables[0].Rows[0]["QuestionNumber"].ToString();
            txtQueDescription.Text = QuestionData.Tables[0].Rows[0]["Que_Description"].ToString();
            txtQuestionMarks.Text = QuestionData.Tables[0].Rows[0]["QUESTION_MARKS"].ToString();
            if (ddlQueLevel.SelectedValue == "1")
            {
                idminimum.Visible = true;
                idoutof.Visible = true;
                txtAttemptMinimum.Visible = true;
                txtOutOfQuestion.Visible = true;
                txtQuestionMarks.Enabled = true;
                ddlParentQuestion.Enabled = true;
                txtAttemptMinimum.Text = QuestionData.Tables[0].Rows[0]["Solve_no_of_question"].ToString();
                txtOutOfQuestion.Text = QuestionData.Tables[0].Rows[0]["No_of_question"].ToString();

            }
            else
            {
                idminimum.Visible = false;
                idoutof.Visible = false;
                txtAttemptMinimum.Visible = false;
                txtOutOfQuestion.Visible = false;
                txtQuestionMarks.Enabled = false;
                ddlParentQuestion.Enabled = false;
                txtAttemptMinimum.Text = QuestionData.Tables[0].Rows[0]["Solve_no_of_question"].ToString();
                txtOutOfQuestion.Text = QuestionData.Tables[0].Rows[0]["No_of_question"].ToString();
            }
            //txtAttemptMinimum.Text = QuestionData.Tables[0].Rows[0]["Solve_no_of_question"].ToString();
            //txtOutOfQuestion.Text = QuestionData.Tables[0].Rows[0]["No_of_question"].ToString();

            Session["Temp"] = "T";
        }
        else
        {

        }
    }
   
    protected void rptExamQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
            //Find the DropDownList in the Repeater Item.
            Label lblQuestionPattern = (e.Item.FindControl("lblQuestionPattern") as Label);
            Label lblExamName = (e.Item.FindControl("lblExamName") as Label);
            LinkButton LinkButton1 = (e.Item.FindControl("LinkButton1") as LinkButton);
            LinkButton LinkButton2 = (e.Item.FindControl("LinkButton2") as LinkButton);
            LinkButton LinkButton3 = (e.Item.FindControl("LinkButton3") as LinkButton);
            Label lblSolveNoOfQuestion = (e.Item.FindControl("lblSolveNoOfQuestion") as Label);
            Label lblQuestionMarks = (e.Item.FindControl("lblQuestionMarks") as Label);

            if (lblExamName.Text == "OR")
            {
                
                lblExamName.ForeColor = System.Drawing.Color.Red;
                lblQuestionMarks.Visible = true;//added on 10112022 BY Abhijeet
                
            }
            if (LinkButton1.Text == "" && LinkButton2.Text == "" && LinkButton3.Text == "")
            {
                lblSolveNoOfQuestion.Text = "";
            }

            HiddenField hdLevelId = (e.Item.FindControl("hdLevelId") as HiddenField);
            ListBox ddlBranch = (e.Item.FindControl("ddlBranch") as ListBox);
            ListBox ddlBloomCategory = (e.Item.FindControl("ddlBloomCategory") as ListBox);
            ListBox ddlOptionWith = (e.Item.FindControl("ddlOptionWith") as ListBox);
            try
            {
                DropDownCheckBoxes ddlCOMulti = (e.Item.FindControl("ddlBranch1") as DropDownCheckBoxes);
              
                //DataSet ds = objCommon.FillDropDown("tblMapCOPO TM INNER JOIN TBLMAPCOPODETAILS SUBCOPO ON(TM.COPOID=SUBCOPO.COPOID) INNER JOIN TBLSUBCODETAILS SBCO ON(SUBCOPO.SUBCOID=SBCO.SUBCOID)", "DISTINCT sbco.SubCOId", "sbco.SubCOName", "sbco.SubCOId > 0 AND tm.SchemeSubjectId=" + Convert.ToInt32(0) + " ", "sbco.SubCOId");
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_SectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        //string Chk = objCommon.LookUp("tblQuestionPatternMaster", "COUNT(1)", "QuestionPatternName ='" + txtQuestionPatternName.Text + "'");

        //if ((Chk != null || Chk != string.Empty) && Chk != "0")
        //{
        //    objCommon.DisplayMessage(updEdit, " Record Already Exist..", this.Page);
        //    return;

        //}
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
      
        int patternid = Convert.ToInt32((item.FindControl("hdnSchemeSubjectId") as HiddenField).Value);

        int Patternno = Convert.ToInt32(objCommon.LookUp("TBLQUESTIONSOBTAINEDMARKS TQ inner join TBLEXAMPAPERQUESTIONS TE on (TQ.PaperQuestionsId=TE.PaperQuestionsId)", "Count(*)", " isnull(TQ.islock,0)=1 AND questionpatternid=" + patternid));
        if (Patternno > 0)
        {
            objCommon.DisplayMessage(this.Page, "STOP !!! Exam Mark Entry Already Lock you can not modify.", this.Page);
            txtQuestionPatternName.Text = "";
            txtPatternMarks.Text = "";
            return;

        }
        hdnPatternId.Value =((item.FindControl("hdnSchemeSubjectId") as HiddenField).Value);
        
        txtQuestionPatternName.Text = (item.FindControl("lblSubjectName") as Label).Text;

        txtPatternMarks.Text = (item.FindControl("lblMarks") as Label).Text;
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

        int srno = Convert.ToInt32((item.FindControl("hdnSrno") as HiddenField).Value);
        //(creatno == 1 || creatno == 2) && srno == 0)
        if (srno == 0)
        {
            objCommon.DisplayMessage(updEdit, "Subject details can not be edit or view,kindly create question Pattern first, '", this.Page);
        }
        else
        {

        }    
    }

    protected void btnDeleteQuestion_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        if (txtconformmessageValue.Value == "Yes")
        {
            CustomStatus cs = (CustomStatus)ObjQP.DeleteQuestion(Convert.ToInt32(btnDelete.CommandArgument));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(updEdit, "Question Deleted Successfully.", this.Page);
                BindPatternDetailsToGridview();
            }
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        string descript = string.Empty;
        string SP = "PKG_ACD_PATTERN_DETAILS_INSERT_UPDATE";
        string PR = "@P_QuestionPatternSubID, @P_QuestionNumber, @P_Level_Id, @P_No_of_question, @P_Solve_no_of_question, @P_Question_Pattern_ID, @P_QUESTION_MARKS, @P_Que_Or_With, @P_Que_Description, @P_Parent_Question, @P_MarkEntry, @P_ISLOCKED, @P_OUTPUT";
        string VL = "0,0,0,0,0," + Convert.ToInt32(hdPatternId.Value) + ",0,0,0,0,0,1,0";

        int result = Convert.ToInt32(AL.DynamicSPCall_IUD(SP, PR, VL, true, 1));

        if (result == 1)
        {
            objCommon.DisplayMessage(updEdit, "Pattern Locked Successfully..", this.Page);
            dvQuestionPaper.Visible = false;
            ClearQuestionNo();
            BindPatternDetailsToGridview();
        }
        else if (result == 0)
        {
            objCommon.DisplayMessage(updEdit, "Please create atleast one Question before locking.", this.Page);  
        }
    }

    protected void ddlQueLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQuestionMarks.Text = string.Empty;
        txtQuestionNo.Text = string.Empty;
        if (ddlQueLevel.SelectedValue == "1")
        {
            idminimum.Visible = true;
            idoutof.Visible = true;
            txtAttemptMinimum.Visible = true;
            txtOutOfQuestion.Visible = true;
            ddlParentQuestion.Enabled = false;
            txtQuestionMarks.Enabled = true;

        }
        else
        {
            idminimum.Visible = false;
            idoutof.Visible = false;
            txtAttemptMinimum.Visible = false;
            txtOutOfQuestion.Visible = false;
            ddlParentQuestion.Enabled = true;
            txtQuestionMarks.Enabled = false;
        }


    }


    protected void ddlQueOrWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlQueOrWith.SelectedIndex) > 0)
        {
            idminimum.Visible = false;
            idoutof.Visible = false;

        }
        else
        {
            idminimum.Visible = true;
            idoutof.Visible = true;
        }

    }

    protected void ddlParentQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlParentQuestion.SelectedValue) > 0)
        {
            string parent = Convert.ToString(ddlParentQuestion.SelectedItem);
         //   decimal QMarks = Convert.ToDecimal(txtQuestionMarks.Text);
            int Solve_NO_of_question = Convert.ToInt32(objCommon.LookUp("tblQuestionPatternDetails", "Solve_no_of_question", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and QuestionNumber='" + parent + "'"));
            int QuestionPatternSubID = Convert.ToInt32(objCommon.LookUp("tblQuestionPatternDetails", "QuestionPatternSubID", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and QuestionNumber='" + parent + "'"));
            int Count = Convert.ToInt32(objCommon.LookUp("tblQuestionPatternDetails", "count(*)", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and Parent_Question=" + QuestionPatternSubID));
            Decimal PARENT_QUESTION_MARKS = Convert.ToDecimal(objCommon.LookUp("tblQuestionPatternDetails", "QUESTION_MARKS", "Question_Pattern_Id=" + Session["QuestionPatternId"] + "and QuestionNumber='" + parent + "'"));
            if (ddlQueLevel.SelectedValue == "2")
            {
                idQmark.Disabled = false;
                decimal A = PARENT_QUESTION_MARKS / Convert.ToDecimal(Solve_NO_of_question);
               // Convert.ToDecimal(txtQuestionMarks.Text)= A;


                txtQuestionMarks.Text = A.ToString();
                txtQuestionMarks.Enabled = false;

            }
            else
            {
                idQmark.Disabled = true;
            
            }


        
        }
    }
}