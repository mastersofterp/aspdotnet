using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Data;
using System.Web.Script.Serialization;

using System.Data.SqlClient;
using Saplin.Controls;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS;

public partial class OBE_AssignSubjectExamQuestionPattern : System.Web.UI.Page
{
    CommonModel ObjComModel = new CommonModel();
    Common objCommon = new Common();
    CommanController ObjComm = new CommanController();
    ExamQuestionPatternController ObjQP = new ExamQuestionPatternController();
    ExamQuestionPaperController objStatC = new ExamQuestionPaperController();


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
                if ((Convert.ToInt32(Session["usertype"]) != 3))
                {
                    if (Convert.ToInt32(Session["usertype"]) == 8)
                    {

                        ddlSession.Enabled = true;

                    }
                    else
                    {
                        objCommon.DisplayMessage("Only Faculty and HOD has Authority To Work On This Page.", this.Page);
                        //ddlSession.Enabled = false;
                        return;
                    }
                }
                
                else
                {
                    ddlSession.Enabled = true;
                }

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
                Response.Redirect("~/notauthorized.aspx?page=AssignSubjectExamQuestionPattern.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AssignSubjectExamQuestionPattern.aspx");
        }
    }


    private void FillDropDownList()
    {
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 and FLOCK=1", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) ,USER_ACC AC CROSS APPLY (SELECT VALUE FROM [DBO].[SPLIT] (AC.UA_COLLEGE_NOS,','))A WHERE STARTED = 1 AND  SHOW_STATUS =1 AND A.VALUE=SA.COLLEGE_IDs AND AM.UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + ")", "SESSIONNO DESC");

       // objCommon.FillDropDownList(ddlSession, " ACD_SESSION_MASTER S INNER JOIN USER_ACC U ON ','+U.UA_COLLEGE_NOS +',' LIKE '%,'+CAST(S.COLLEGE_ID as NVARCHAR(64))+',%'", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 and AND S.FLOCK=1 AND IS_ACTIVE=1 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "SESSIONNO DESC");

        //objCommon.FillDropDownList(ddlSection, "tblExamQuestionPaper QP INNER JOIN acd_section S ON (QP.sectionId=S.Sectionno)", "SectionId", "Sectionname", "SchemeSubjectId=" + Convert.ToInt32(hdSchemeSubjectIdDDl.Value) + "AND ISNULL(ISLOCK,0)=1 AND SessionId=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND Createdby=" + Convert.ToInt32(Session["userno"].ToString()) + "AND ExamPatternMappingId=" + ExamPatternMappingId, "SessionId");

      

        DataSet ds = objStatC.CheckSessionData(Convert.ToInt32(Session["userno"].ToString()));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlSession.DataSource = ds;
            ddlSession.DataTextField = "SESSION_NAME";
            ddlSession.DataValueField = "SESSIONNO";
            ddlSession.DataBind();

        }


      

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
         ddlCourseno.SelectedIndex = 0;
         divSubject.Visible = false;
         
        if (ddlSession.SelectedIndex > 0)
        { 
            ViewState["sessionId"] = ddlSession.SelectedValue;

            DataSet ds = objStatC.CHECKCOURSE(Convert.ToInt32(ViewState["sessionId"]), Convert.ToInt32(Session["userno"].ToString()));
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourseno.Items.Clear();
                ddlCourseno.DataSource = ds;
                ddlCourseno.DataTextField = "COURSE_NAME";
                ddlCourseno.DataValueField = "COURSENO";
                ddlCourseno.DataBind();
                ddlCourseno.Items.Insert(0, new ListItem("Please select Course", "0")); 
                ddlCourseno.SelectedIndex = 0;

            }
            else 
            {
                 ddlCourseno.DataSource = null;
                 ddlCourseno.DataBind();
                 divSubject.Visible = false;
            }
            
            //BindSubjects(); 
            //objCommon.FillDropDownList(ddlCourseno, " ACD_COURSE_TEACHER ACT inner join ACD_COURSE AC on (act.courseno=ac.courseno)", "DISTINCT ac.COURSENO", "COURSE_NAME", " ua_no=" + Convert.ToInt32(Session["userno"].ToString()) + " and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue), "");
        }
    }
    public void BindSubjects()
    {
       

        DataSet ds = objStatC.GetSubjectData(Convert.ToInt32(ViewState["sessionId"]), Convert.ToInt32(Session["userno"].ToString()),Convert.ToInt32(ddlCourseno.SelectedValue));

        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            rptCourse.DataSource = ds;
            rptCourse.DataBind();
            divSubject.Visible = true;
        }
        else
        {
            rptCourse.DataSource = null;
            rptCourse.DataBind();
            divSubject.Visible = false;

            objCommon.DisplayMessage(updEdit, "Subject Not Assigned to The faculty for this Session Please Check',!", this.Page);
        }
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
            ddlOptionWith.ClearSelection();
        }
    }


    public void BindQuestionPaper()
    {

        DataSet ds = objStatC.GetSubjectData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        BindSubjects();
        divlowerpanel.Visible = false;
        divSubject.Visible = true;
        dvSession.Visible = true;
        dvCourse.Visible = true;
        dvQuestionPaper.Visible = false;
        rptExamQuestion.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rptExamQuestion.DataSource = null;
        rptExamQuestion.DataBind();
        rptCourse.DataSource = null;
        rptCourse.DataBind();
        ddlCourseno.SelectedIndex = 0;
        dvCourse.Visible = true;

        txtQuestionPaperName.Text = string.Empty;
        txtMaximumMarks.Text = string.Empty;
        txtAreaDiscription.Text = string.Empty;

        divlowerpanel.Visible = false;
        dvQuestionPaper.Visible = false;
        rptExamQuestion.Visible = false;

        divSubject.Visible = false;
        dvSession.Visible = true;
        ddlSession.SelectedIndex = 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ExamQuestionPaperController objExmPCon = new ExamQuestionPaperController();

        foreach (RepeaterItem repeaterItem in rptExamQuestion.Items)
        {
            ListBox ddl = (ListBox)repeaterItem.FindControl("ddlBloomCategory");
            int a = ddl.SelectedIndex;
            if (a != -1)
            {
                if (int.Parse(ddl.SelectedItem.Value) > 0)
                {
                    string selectedValue = ddl.SelectedValue;
                    // insert code to add value to listbox here.

                    string selectedText = ddl.SelectedItem.Text;
                    // Insert code to add Text to listbox here.
                }
                else
                {

                    objCommon.DisplayMessage(updEdit, "Atleast one CO and one Bloom should be selected for each question.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Atleast one CO and one Bloom should be selected for each question.", this.Page);
                return;
            }
        }

        try
        {
            Boolean islock, isactive;
            if (IsLock.Checked == true)
            {
                islock = true;
            }
            else
            {
                islock = false;
            }

            if (IsActive.Checked == true)
            {
                isactive = true;
            }
            else
            {
                isactive = true;
            }

            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("tempqueNo");
            dt1.Columns.Add("tempGroupNo");
            dt1.Columns.Add("QuestionPatternSubID");
            dt1.Columns.Add("SubCOId");
            dt1.Columns.Add("Weightage");

            DataTable dt2 = new DataTable();
            dt2.Clear();
            dt2.Columns.Add("tempqueNo");
            dt2.Columns.Add("tempGroupNo");
            dt2.Columns.Add("QuestionPatternSubID");
            dt2.Columns.Add("BloomCatId");
            dt2.Columns.Add("Weightage");


            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("tempqueNo");
            dt.Columns.Add("tempGroupNo");
            dt.Columns.Add("QuestionNo");
            dt.Columns.Add("Description");
            dt.Columns.Add("MaxMarks");
            dt.Columns.Add("BloomCategoryId");
            dt.Columns.Add("levelId");
            dt.Columns.Add("QuestionPatternId");
            dt.Columns.Add("MarkEntry");
            dt.Columns.Add("QuestionPatternSubID");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("SequenceNo");
            dt.Columns.Add("QuePatMaxMarks");
            dt.Columns.Add("ActiveStatus");
            dt.Columns.Add("OptionQuestionWith");
            //Repeater repeater = (Repeater)gvRow.FindControl("repeater1");
            Repeater repeater = rptExamQuestion as Repeater;



            int FindgroupId = 0;
            double MarksGroup = 0.00, Mark = 0.00;
            foreach (RepeaterItem repItem in repeater.Items)
            {
                HiddenField hdLevelId = (HiddenField)repItem.FindControl("hdLevelId");
                HiddenField hdGroupId = (HiddenField)repItem.FindControl("hdGroupId");
                HiddenField hdMarkEntry = (HiddenField)repItem.FindControl("hdMarkEntry");
                HiddenField hdQuestionPatternSubID = (HiddenField)repItem.FindControl("hdQuestionPatternSubID");
                // ListBox OptionWith = (ListBox)repItem.FindControl("ddlOptionWith");
                DropDownList LevelId = (DropDownList)repItem.FindControl("ddlWorkStatus");
                Label Marks = (Label)repItem.FindControl("lblMarks");
                TextBox mark = (TextBox)repItem.FindControl("txtMarks");

                if (FindgroupId == 0)
                {
                    FindgroupId = Convert.ToInt32(hdGroupId.Value);
                }
                else
                {
                }
            }

            int tempqueNo = 1, SequenceNo = 1, tempqueNo1 = 1, tempGroupNo = 1, tempGroupNo1 = 0, tempGrpBlm = 0, tempGrpCo = 0;
            foreach (RepeaterItem repItem in repeater.Items)
            {
                string OptionQue = string.Empty;
                DataRow QueData = dt.NewRow();

                Label lblQuestionPattern = (Label)repItem.FindControl("lblQuestionPattern");

                ListBox ddlCO = (ListBox)repItem.FindControl("ddlBranch");
                ListBox ddlBloomCat = (ListBox)repItem.FindControl("ddlBloomCategory");
                HiddenField hdnQueOrWith = (HiddenField)repItem.FindControl("hdnQueOrWith");
                HiddenField hdParentQue = (HiddenField)repItem.FindControl("hdParentQue");
                HiddenField hdMarkEntry = (HiddenField)repItem.FindControl("hdMarkEntry");
                HiddenField hdQuestionPatternSubID = (HiddenField)repItem.FindControl("hdQuestionPatternSubID");
                HiddenField hdLevelId = (HiddenField)repItem.FindControl("hdLevelId");
                HiddenField hdGroupId = (HiddenField)repItem.FindControl("hdGroupId");
                HiddenField hdQuePatternId = (HiddenField)repItem.FindControl("hdQuePatternId");


                string lblQuetPatt = lblQuestionPattern.Text;
                Label MaxMarks = (Label)repItem.FindControl("lblMarks");
                //TextBox MaxMarks = (TextBox)repItem.FindControl("txtMarks");
                double maxmarks = 0.00;
                if (MaxMarks.Text == "")
                {
                    maxmarks = 0.00;
                }
                else
                {
                    maxmarks = Convert.ToDouble(MaxMarks.Text);
                }

                if (hdParentQue.Value == "0")
                {
                    tempGroupNo1++;
                }

                QueData["tempqueNo"] = Convert.ToInt32(hdQuePatternId.Value);
                QueData["tempGroupNo"] = Convert.ToInt32(hdGroupId.Value);//Convert.ToInt32(tempGroupNo1);
                QueData["QuestionNo"] = lblQuestionPattern.Text;
                QueData["Description"] = txtAreaDiscription.Text;
                QueData["MaxMarks"] = maxmarks;
                QueData["BloomCategoryId"] = Convert.ToString(ddlBloomCat.SelectedValue);

                QueData["levelId"] = Convert.ToInt32(hdLevelId.Value);
                QueData["QuestionPatternId"] = Convert.ToInt32(ddlPattern.SelectedValue);
                QueData["MarkEntry"] = Convert.ToInt32(hdMarkEntry.Value);
                QueData["QuestionPatternSubID"] = Convert.ToInt32(hdQuestionPatternSubID.Value);
                QueData["GroupId"] = Convert.ToInt32(hdGroupId.Value); // Convert.ToInt32(tempGroupNo1);
                QueData["SequenceNo"] = SequenceNo;
                QueData["QuePatMaxMarks"] = Convert.ToDouble(txtMaximumMarks.Text);
                QueData["ActiveStatus"] = isactive;

                QueData["OptionQuestionWith"] = (hdnQueOrWith.Value);

                dt.Rows.Add(QueData);
                tempqueNo++;
                SequenceNo++;
                tempGroupNo++;

            }

            foreach (RepeaterItem repItem in repeater.Items) 
            {
                Label lblQuestionPattern = (Label)repItem.FindControl("lblQuestionPattern");
                HiddenField hdQuestionPatternSubID = (HiddenField)repItem.FindControl("hdQuestionPatternSubID");
                HiddenField hdGroupId = (HiddenField)repItem.FindControl("hdGroupId");
                HiddenField hdQuePatternId = (HiddenField)repItem.FindControl("hdQuePatternId");
                HiddenField hdParentQue = (HiddenField)repItem.FindControl("hdParentQue");
                ListBox ddlCO = (ListBox)repItem.FindControl("ddlBranch");

                //string BranchNos = "";
                if (hdParentQue.Value == "0")
                {
                    tempGrpCo++;
                }

                foreach (ListItem item in ddlCO.Items)
                {
                    DataRow Codata = dt1.NewRow();
                    if (item.Selected == true)
                    {

                        Codata["tempqueNo"] = Convert.ToInt32(hdQuePatternId.Value);
                        Codata["tempGroupNo"] = Convert.ToInt32(hdGroupId.Value); //Convert.ToInt32(tempGrpCo); 
                        Codata["QuestionPatternSubID"] = Convert.ToInt32(hdQuestionPatternSubID.Value);
                        Codata["SubCOId"] = item.Value;
                        Codata["Weightage"] = 0.00;
                        //BranchNos += item.Value + ',';
                        dt1.Rows.Add(Codata);
                    }
                }
            }

            foreach (RepeaterItem repItem in repeater.Items)
            {
                Label lblQuestionPattern = (Label)repItem.FindControl("lblQuestionPattern");
                HiddenField hdGroupId = (HiddenField)repItem.FindControl("hdGroupId");
                HiddenField hdQuestionPatternSubID = (HiddenField)repItem.FindControl("hdQuestionPatternSubID");
                HiddenField hdQuePatternId = (HiddenField)repItem.FindControl("hdQuePatternId");
                ListBox BloomId = (ListBox)repItem.FindControl("ddlBloomCategory");
                HiddenField hdParentQue = (HiddenField)repItem.FindControl("hdParentQue");
                if (hdParentQue.Value == "0")
                {
                    tempGrpBlm++;
                }
                foreach (ListItem item in BloomId.Items)
                {
                    DataRow Bloomdata = dt2.NewRow();
                    if (item.Selected == true)
                    {
                        Bloomdata["tempqueNo"] = Convert.ToInt32(hdQuePatternId.Value);
                        Bloomdata["tempGroupNo"] = Convert.ToInt32(hdGroupId.Value);//Convert.ToInt32(tempGrpBlm);
                        Bloomdata["QuestionPatternSubID"] = Convert.ToInt32(hdQuestionPatternSubID.Value);
                        Bloomdata["BloomCatId"] = item.Value;
                        Bloomdata["Weightage"] = 0.00;
                        dt2.Rows.Add(Bloomdata);
                    }
                }
            }
            int OrgID = Convert.ToInt32(Session["OrgId"]);
            int result = objStatC.SaveQuestionPaper(txtQuestionPaperName.Text, Convert.ToInt32(hdSchemeSubjectIdDDl.Value), Convert.ToInt32(hdExamPatternMappingIdDDl.Value), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToDouble(txtMaximumMarks.Text), txtAreaDiscription.Text, islock, isactive, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(hdnSectionId.Value), dt, dt1, dt2, OrgID);
           // int result = objStatC.SaveQuestionPaper(txtQuestionPaperName.Text, Convert.ToInt32(hdSchemeSubjectIdDDl.Value), Convert.ToInt32(hdExamPatternMappingIdDDl.Value), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToDouble(txtMaximumMarks.Text), txtAreaDiscription.Text, islock, isactive, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(hdnSectionId.Value), dt, dt1, dt2);
            if (result == 1)
            {   
                objCommon.DisplayMessage(updEdit, "Question Paper Saved Successfully..", this.Page);

                //dvQuestionPaper.Visible = false;
                //rptExamQuestion.DataSource = null;
                //rptExamQuestion.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Error occored while saving Question Paper.", this.Page);
            }
        }

        catch (Exception ex)
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
        DataSet ds = objStatC.BindSchemeMappingGridBind(Convert.ToInt32(Session["userno"]));
        ddlPattern.Items.Add(new ListItem("Please Select", "0"));
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //ddlSchemeName.DataSource = ds.Tables[0];
                string name = ds.Tables[0].Rows[i]["QuestionPatternName"].ToString();
                string id = ds.Tables[0].Rows[i]["QuestionPatternId"].ToString();
                ddlPattern.Items.Add(new ListItem(name, id));
            }

        }
    }


    protected void lnkCreate_Click(object sender, EventArgs e)
    {
        int ret = 0;
        DataSet ds = new DataSet();
        //Reference the Repeater Item using Button.
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

        int srno = Convert.ToInt32((item.FindControl("hdnSrno") as HiddenField).Value);
        //(creatno == 0) && srno == 1)
        if (srno == 1)
        {
            objCommon.DisplayMessage(updEdit, "Subject details is already created,you can edit it', '", this.Page);
            divlowerpanel.Visible = false;
            tdStudent.Visible = true;

        }
        else
        {
            int schemesubjectid = Convert.ToInt32((item.FindControl("hdnSchemeSubjectId") as HiddenField).Value);
            int ExamPatternMappingId = Convert.ToInt32((item.FindControl("ExamPatternMappingId") as HiddenField).Value);
            int SectionId = Convert.ToInt32((item.FindControl("hdnSectionId") as HiddenField).Value);
            string subjectname = (item.FindControl("lblSubjectName") as Label).Text;
            string exam = (item.FindControl("lblExamName") as Label).Text;
            string section = (item.FindControl("hdnSectionName") as HiddenField).Value;
            hdSchemeSubjectIdDDl.Value = ((item.FindControl("hdnSchemeSubjectId") as HiddenField).Value);
            hdExamPatternMappingIdDDl.Value = (item.FindControl("ExamPatternMappingId") as HiddenField).Value;
            hdnSectionId.Value = ((item.FindControl("hdnSectionId") as HiddenField).Value);

            ret = Convert.ToInt32(objStatC.GetExamDetails(schemesubjectid));

            if ((int)ret == 2 || (int)ret == 3)
            {

                objCommon.DisplayMessage(updEdit, "CO Is Not Created Or COPO Mapping is not Done..', '", this.Page);
                return;
            }
            else if ((int)ret == 0)
            {
                int OrgID = Convert.ToInt32(Session["OrgId"]);
                ds = objStatC.GetExamData(OrgID, Convert.ToInt32(ddlSession.SelectedValue), schemesubjectid, ExamPatternMappingId, SectionId);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtQuestionPaperName.Text = ds.Tables[0].Rows[0]["QuestionPaperName"].ToString();
                        if (txtQuestionPaperName.Text == "-")
                        {
                            txtQuestionPaperName.Text = subjectname + "-" + exam + "(SEC-" + section + ")";
                        }

                        txtMaximumMarks.Text = (ds.Tables[0].Rows[0]["MaxMarks"].ToString());
                        if ((ds.Tables[0].Rows[0]["IsLock"].ToString()) == "1")
                        {
                            IsLock.Checked = true;
                        }
                        else
                        {
                            IsLock.Checked = false;
                        }
                        if ((ds.Tables[0].Rows[0]["IsLock"].ToString()) == "1")
                        {
                            IsActive.Checked = true;
                        }
                        else
                        {
                            IsActive.Checked = false;
                        }

                        divSubject.Visible = false;
                        dvSession.Visible = false;
                        dvCourse.Visible = false;
                        dvQuestionPaper.Visible = false;
                        divlowerpanel.Visible = true;
                        dvSubmitData.Visible = false;


                    }
                }
                objCommon.FillDropDownList(ddlPattern, "tblQuestionPatternMaster", "QuestionPatternId", "QuestionPatternName", "isnull(ACTIVESTATUS,0)=1 and MARKS=" + txtMaximumMarks.Text + "", "QuestionPatternId");
                objCommon.FillDropDownList(ddlSection, "tblExamQuestionPaper QP INNER JOIN acd_section S ON (QP.sectionId=S.Sectionno)", "SectionId", "Sectionname", "SchemeSubjectId=" + Convert.ToInt32(hdSchemeSubjectIdDDl.Value) + "AND ISNULL(ISLOCK,0)=1 AND SessionId=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND Createdby=" + Convert.ToInt32(Session["userno"].ToString()) + "AND ExamPatternMappingId=" + ExamPatternMappingId, "SessionId");
                  
            }

            pnlStudGrid.Visible = true;
            string message = "Customer Id: " + (item.FindControl("lblSubjectName") as Label).Text;

        }
    }

    protected void ddlPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPattern.SelectedIndex > 0)
        {
            divSection.Visible = true;

            DataSet ds1 = objStatC.GetPatternDataForMapping(Convert.ToInt32(ddlPattern.SelectedValue), Convert.ToInt32(hdSchemeSubjectIdDDl.Value));
            if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["ISLOCKED"] == "1")
                {
                    IsLock.Checked = true;
                   btnSave.Attributes["disabled"] = "disabled";
                   // btnSave.Visible = false;
                }
                else 
                {

                    IsLock.Checked = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "btnSave_" + this.ClientID, "EnableSaveButton();", true);
                   //btnSave.Visible = true;
                    
                }
                rptExamQuestion.DataSource = ds1;
                rptExamQuestion.DataBind();
                dvQuestionPaper.Visible = true;
                rptExamQuestion.Visible = true;
                dvSubmitData.Visible = true;
                //IsLock.Visible = true;
                //BindddlCPSection();
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Pattern Not Created For This Subject, '", this.Page);
                rptExamQuestion.DataSource = null;
                rptExamQuestion.DataBind();
                dvQuestionPaper.Visible = false;
                dvSubmitData.Visible = false;
            }
        }
        else
        {
            rptExamQuestion.DataSource = null;
            rptExamQuestion.DataBind();
            dvQuestionPaper.Visible = false;
            divlowerpanel.Visible = false;
        }

        ddlSection.SelectedIndex = 0;
       
    }

    protected void rptExamQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Find the DropDownList in the Repeater Item.
            Label lblQuestionPattern = (e.Item.FindControl("lblQuestionPattern") as Label);
            Label lblOR = (e.Item.FindControl("lblOR") as Label);

            HiddenField hdPaperQuestionsId = (e.Item.FindControl("hdPaperQuestionsId") as HiddenField);
            HiddenField hdLevelId = (e.Item.FindControl("hdLevelId") as HiddenField);
            ListBox ddlBranch = (e.Item.FindControl("ddlBranch") as ListBox);
            ListBox ddlBloomCategory = (e.Item.FindControl("ddlBloomCategory") as ListBox);
            ListBox ddlOptionWith = (e.Item.FindControl("ddlOptionWith") as ListBox);
            try
            {
                DropDownCheckBoxes ddlCOMulti = (e.Item.FindControl("ddlBranch1") as DropDownCheckBoxes);

                DataSet ds = objStatC.GetSubCO(Convert.ToInt32(hdSchemeSubjectIdDDl.Value));
                if (lblQuestionPattern.Text == "")
                {
                    lblOR.Visible = true;
                    lblOR.Text = "OR";
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                    while (dtr.Read())
                    {
                        if (lblQuestionPattern.Text == "")
                        {
                            ddlBranch.Visible = false;
                        }
                        else
                        {
                            ddlBranch.Items.Add(new ListItem(dtr["SubCOName"].ToString(), dtr["SubCOId"].ToString()));
                        }
                    }
                }
                ddlBranch.SelectedValue = ddlBranch.ToolTip;
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_SectionAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }

            DataSet dsBloom = objStatC.GetBloomCategory();
            if (dsBloom != null && dsBloom.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = dsBloom.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    if (lblQuestionPattern.Text == "")
                    {
                        ddlBloomCategory.Visible = false;
                    }
                    else
                    {

                        ddlBloomCategory.Items.Add(new ListItem(dtr["BloomName"].ToString(), dtr["BloomCategoryId"].ToString()));
                    }

                }
            }
            ddlBloomCategory.SelectedValue = ddlBloomCategory.ToolTip;
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

        int schemesubjectid = Convert.ToInt32((item.FindControl("hdnSchemeSubjectId") as HiddenField).Value);
        int ExamPatternMappingId = Convert.ToInt32((item.FindControl("ExamPatternMappingId") as HiddenField).Value);
        int hdnQuestionPaperId = Convert.ToInt32((item.FindControl("hdnQuestionPaperId") as HiddenField).Value);
        Session["PaperId"] = hdnQuestionPaperId;
        int SectionId = Convert.ToInt32((item.FindControl("hdnSectionId") as HiddenField).Value);
        string subjectname = (item.FindControl("lblSubjectName") as Label).Text;
        string exam = (item.FindControl("lblExamName") as Label).Text;
        string section = (item.FindControl("hdnSectionName") as HiddenField).Value;
        hdSchemeSubjectIdDDl.Value = ((item.FindControl("hdnSchemeSubjectId") as HiddenField).Value);
        hdExamPatternMappingIdDDl.Value = (item.FindControl("ExamPatternMappingId") as HiddenField).Value;
        hdnSectionId.Value = ((item.FindControl("hdnSectionId") as HiddenField).Value);


        DataSet ds = new DataSet();
        //Reference the Repeater Item using Button.

        int srno = Convert.ToInt32((item.FindControl("hdnSrno") as HiddenField).Value);

        if (srno == 0)
        {
            objCommon.DisplayMessage(updEdit, "Subject details can not be edited or viewed,kindly create question paper first', '", this.Page);
        }
        else
        {
            int OrgID = Convert.ToInt32(Session["OrgId"]);
           DataSet ds3 = objStatC.GetExamQuestionForEdit(schemesubjectid, ExamPatternMappingId, Convert.ToInt32(ddlSession.SelectedValue), 0, SectionId, OrgID);
           //DataSet ds3 = objStatC.GetExamQuestionForEdit(schemesubjectid, ExamPatternMappingId, Convert.ToInt32(ddlSession.SelectedValue), 0, SectionId);
            DataSet QuestionPaperId = objStatC.GetQuestionPaperId(Convert.ToInt32(hdSchemeSubjectIdDDl.Value), Convert.ToInt32(ExamPatternMappingId), Convert.ToInt32(hdnSectionId.Value), Convert.ToInt32(ddlSession.SelectedValue));

            DataSet BindDataForEdit = objStatC.GetExamQuestionPaperData(Convert.ToInt32(QuestionPaperId.Tables[0].Rows[0]["QuestionPaperId"].ToString()));

            if (BindDataForEdit.Tables[0] != null && BindDataForEdit.Tables[0].Rows.Count > 0)
            {
                rptExamQuestion.DataSource = BindDataForEdit;
                rptExamQuestion.DataBind();
            }
            else
            {
            }

            ds = objStatC.GetExamData(OrgID, Convert.ToInt32(ddlSession.SelectedValue), schemesubjectid, ExamPatternMappingId, SectionId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtQuestionPaperName.Text = ds.Tables[0].Rows[0]["QuestionPaperName"].ToString();
                   
                    hdQuestionPaperId.Value = ds.Tables[0].Rows[0]["QuestionPaperId"].ToString();
                   
                    if (txtQuestionPaperName.Text == "-")
                    {
                        txtQuestionPaperName.Text = subjectname + "-" + exam + "(SEC-" + section + ")";
                    }
                    txtMaximumMarks.Text = (ds.Tables[0].Rows[0]["MaxMarks"].ToString());
                    
                    if (ddlPattern.SelectedItem != null)
                    {
                        objCommon.FillDropDownList(ddlPattern, "tblQuestionPatternMaster", "QuestionPatternId", "QuestionPatternName", "isnull(ACTIVESTATUS,0)=1 and MARKS=" + txtMaximumMarks.Text + "", "QuestionPatternId");
                        ddlPattern.SelectedValue = Convert.ToString(ds3.Tables[0].Rows[0]["QuestionPatternId"]);
                       
                    }

                    if ((ds.Tables[0].Rows[0]["IsLock"].ToString()) == "True")
                   
                    {
                        IsLock.Checked = true;
                        btnSave.Attributes["disabled"] = "disabled";
                       // btnSave.Visible = false;
                    }
                    else
                    {
                        IsLock.Checked = false;
                        //btnSave.Visible = true;
                       
                    }
                    if ((ds.Tables[0].Rows[0]["IsLock"].ToString()) == "True")
                    {
                        IsActive.Checked = true;
                    }
                    else
                    {
                        IsActive.Checked = false;
                       
                    }
                    divSubject.Visible = false;
                    dvSession.Visible = false;
                    dvCourse.Visible = false;
                    divlowerpanel.Visible = true;
                    dvSubmitData.Visible = true;
                    objCommon.FillDropDownList(ddlSection, "tblExamQuestionPaper QP INNER JOIN acd_section S ON (QP.sectionId=S.Sectionno)", "SectionId", "Sectionname", "SchemeSubjectId=" + Convert.ToInt32(hdSchemeSubjectIdDDl.Value) + "AND ISNULL(ISLOCK,0)=1 AND SessionId=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND Createdby !=" + Convert.ToInt32(Session["userno"].ToString()) + "AND ExamPatternMappingId=" + ExamPatternMappingId, "SessionId");

                }
            }
            pnlStudGrid.Visible = true;

            string message = "Customer Id: " + (item.FindControl("lblSubjectName") as Label).Text;
            //DataSet dsPaper = objStatC.GetQuestionPaperDetails(Convert.ToInt32(hdSchemeSubjectIdDDl.Value), Convert.ToInt32(ExamPatternMappingId), Convert.ToInt32(hdnSectionId.Value));
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            DataSet dsPaper = objStatC.GetQuestionPaperDetails(Convert.ToInt32(hdSchemeSubjectIdDDl.Value), Convert.ToInt32(ExamPatternMappingId), Convert.ToInt32(hdnSectionId.Value), Sessionno);

            DataSet ds1 = ObjQP.GetPatternData(Convert.ToInt32(ddlPattern.SelectedValue), Convert.ToInt32(hdSchemeSubjectIdDDl.Value));

            int i = 0;
            int j = 0;
            int s = 0;
            int t = 0;

            if (dsPaper.Tables[1] != null && dsPaper.Tables[1].Rows.Count > 0)
            {
                foreach (RepeaterItem Items in rptExamQuestion.Items)
                {
                    ListBox ddlBloom = (ListBox)Items.FindControl("ddlBloomCategory");
                    Label lblQuestion = (Label)Items.FindControl("lblQuestionPattern");
                    HiddenField hdPaperQuestionsId = (HiddenField)Items.FindControl("hdPaperQuestionsId");
                    foreach (DataRow dr in dsPaper.Tables[1].Rows)
                    {
                        foreach (ListItem row in ddlBloom.Items)
                        {

                            string optionvalue = Convert.ToString(dr["BLOOM_CAT_ID"]);
                            string quesno = Convert.ToString(dr["QUESTIONNO"]);
                            if (row.Value == Convert.ToString(dr["BLOOM_CAT_ID"]) && hdPaperQuestionsId.Value == Convert.ToString(dr["PAPERQUESTIONSID"]))
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (dsPaper.Tables[2] != null && dsPaper.Tables[2].Rows.Count > 0)
            {
                foreach (RepeaterItem Items in rptExamQuestion.Items)
                {
                    ListBox ddlCO = (ListBox)Items.FindControl("ddlBranch");
                    Label lblQuestion = (Label)Items.FindControl("lblQuestionPattern");
                    HiddenField hdPaperQuestionsId = (HiddenField)Items.FindControl("hdPaperQuestionsId");
                    foreach (DataRow dr in dsPaper.Tables[2].Rows)
                    {
                        foreach (ListItem row in ddlCO.Items)
                        {
                            string optionvalue = Convert.ToString(dr["SUBCOID"]);
                            if (row.Value == Convert.ToString(dr["SUBCOID"]) && hdPaperQuestionsId.Value == Convert.ToString(dr["PAPERQUESTIONSID"]))
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }




            if (dsPaper.Tables[0] != null && dsPaper.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPaper.Tables[0].Rows)
                {

                    DropDownList ddlGroup = (DropDownList)rptExamQuestion.Items[j].FindControl("ddlWorkStatus");
                    ListBox ddlOptionWith = (rptExamQuestion.Items[j].FindControl("ddlOptionWith") as ListBox);
                    TextBox txtMaxMark = (rptExamQuestion.Items[j].FindControl("txtMarks") as TextBox);
                    Label lblMarks = (Label)rptExamQuestion.Items[j].FindControl("lblMarks");
                    lblMarks.Text = (dr["MAXMARKS"].ToString());
                    j++;
                }
            }
        }

        dvQuestionPaper.Visible = true;
        rptExamQuestion.Visible = true;
        //updEdit.Update();
    }


    protected void lnkView_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

        int srno = Convert.ToInt32((item.FindControl("hdnSrno") as HiddenField).Value);

        if (srno == 0)
        {
            objCommon.DisplayMessage(updEdit, "Subject details can not be edited or viewed,kindly create question paper first, '", this.Page);
            objCommon.DisplayMessage("Subject details can not be edited or viewed,kindly create question paper first", this.Page);
        }
        else
        {

        }
    }


    [WebMethod]
    public static List<ExamQuestionPaperModel> FillQuestionPatternDropDown(string ncount, int SchemeSubjectId)
    {
        DataTable dt = new DataTable();
        ExamQuestionPaperController objStatC = new ExamQuestionPaperController();
        List<ExamQuestionPaperModel> objStates = new List<ExamQuestionPaperModel>();

        DataSet ds = objStatC.FillQuestionPatternDropDown(ncount, SchemeSubjectId);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objStates.Add(new ExamQuestionPaperModel
                {
                    QuestionPatternId = int.Parse(dr["QuestionPatternId"].ToString()),
                    QuestionPatternName = (dr["QuestionPatternName"].ToString())
                });
            }
        }
        return objStates;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<ExamQuestionPaperModel> GetSubjectData(int SessionId, int UserId)
    {
        ExamQuestionPaperController objStatC = new ExamQuestionPaperController();
        List<ExamQuestionPaperModel> Status = new List<ExamQuestionPaperModel>();
        try
        {

            DataSet ds = objStatC.GetSubjectData(SessionId, UserId);
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        Status.Add(new ExamQuestionPaperModel()
                     {
                         SchemeSubjectId = int.Parse(dr["SchemeSubjectId"].ToString()),
                         SchemeMappingName = (dr["SchemeMappingName"].ToString()),
                         SectionId = Convert.ToInt32(dr["SectionId"] == DBNull.Value ? 0 : dr["SectionId"]),         //  Added on 29-01-2020
                         SectionName = dr["SectionName"] == null ? "" : dr["SectionName"].ToString(),                //  Added on 29-01-2020
                         ExamPatternMappingId = int.Parse(dr["ExamPatternMappingId"].ToString()),
                         srno = int.Parse(dr["srno"].ToString()),
                         Status = (dr["Status"].ToString()),
                         ExamName = (dr["ExamName"].ToString()),
                         QuestionPaperId = Convert.ToInt32(dr["QuestionPaperId"].ToString()),
                         FilePath = Convert.ToString(dr["DocumentName"])
                     });

                    }
                }
                else
                {
                    Status = null;
                }

            }
            else
            {
                Status = null;
            }

        }
        catch (Exception ex)
        {

        }
        return Status;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "launch", "window.open('/PresentationLayer/OBE/Reports/rptExamPaper.aspx?Exam=" + Session["PaperId"] + "&Pattern=" + ddlPattern.SelectedItem.Value + "', target = '_blank')", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "launch", "window.open('/OBE/Reports/rptExamPaper.aspx?Exam=" + Session["PaperId"] + "&Pattern=" + ddlPattern.SelectedItem.Value + "', target = '_blank')", true); 
        //Response.Redirect("<script> window.open( '" +"~/OBE/Reports/rptExamPaper.aspx?Exam=" + Session["PaperId"] + "&Pattern=" + ddlPattern.SelectedItem.Value + "','_blank' ); </script>");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "launch", "window.open('~/default.aspx', target = '_blank')", true);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        //        try
        //        {   
        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("biometrics")));

        //            url += "Reports/CommonReport.aspx?";
        //            url += "pagetitle=" + reportTitle;
        //            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;            
        //            int idno = 0;


        //            url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtFdate.Text).ToString("yyyy/MM/dd") + ",@P_TODATE=" + Convert.ToDateTime(txtDate.Text).ToString("yyyy/MM/dd") + ",@P_IDNO=" + idno + ",@P_DEPTNO=" + Convert.ToInt32(ddldept.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_TIME_FROM=" + time_from + ",@P_TIME_TO=" + time_to + ",@P_INOUT=" + in_out + ",@P_STNO=" + Convert.ToInt32(ddlStaff.SelectedValue) +",@P_COLLEGE_NO="+Convert.ToInt32(ddlCollege.SelectedValue)+ " ";

        //            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //            divMsg.InnerHtml += " </script>";


        //        }
        //        catch (Exception ex)
        //        {
        //            if (Convert.ToBoolean(Session["error"]) == true)
        //                objUCommon.ShowError(Page, "Login_details_Time_Interval.ShowReport -> " + ex.Message + " " + ex.StackTrace);
        //            else
        //                objUCommon.ShowError(Page, "Server UnAvailable");
        //        }
    }

    // Added By Abhijit 02/03/2022

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt1 = new DataTable();
        dt1.Clear();
        dt1.Columns.Add("SubCOId");


        DataSet ds1 = objStatC.GetPatternDataForMapping(Convert.ToInt32(ddlPattern.SelectedValue), Convert.ToInt32(hdSchemeSubjectIdDDl.Value));

        foreach (RepeaterItem Items in rptExamQuestion.Items)
        {
            ListBox ddlCO = (ListBox)Items.FindControl("ddlBranch");

            HiddenField hdQuePatternId = (HiddenField)Items.FindControl("hdQuePatternId");

            //string test = ddlCO.SelectedItem.Value;  

            //if (ddlCO.SelectedIndex != -1)
            //{

                //For Finding Second Row ddlCo
                foreach (RepeaterItem Items2 in rptExamQuestion.Items)
                {
                    ListBox ddlCO2 = (ListBox)Items2.FindControl("ddlBranch");
                    HiddenField hdnQueOrWith = (HiddenField)Items2.FindControl("HdSecOr");

                    if (Convert.ToInt16(hdQuePatternId.Value) == Convert.ToInt16(hdnQueOrWith.Value))
                    {


                        foreach (ListItem row in ddlCO.Items)
                        {

                            DataRow Codata = dt1.NewRow();
                            if (row.Selected == true)
                            {

                                Codata["SubCOId"] = row.Value;
                                dt1.Rows.Add(Codata);
                                int i = int.Parse(row.Value);
                                ddlCO2.Items.FindByValue(row.Value).Selected = true;
                                //ddlCO2.Enabled = false;
                            }

                            else if (row.Selected == false)
                            {

                                Codata["SubCOId"] = row.Value;
                                dt1.Rows.Add(Codata);
                                int i = int.Parse(row.Value);
                                ddlCO2.Items.FindByValue(row.Value).Selected = false;
                                //ddlCO2.Enabled = false;
                            }
                        }

                    }
                }

           // }

        }
    }


    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ExamPatternMappingId = Convert.ToInt32(hdExamPatternMappingIdDDl.Value);

        DataSet dsPaper = objStatC.GetQuestionPaperDetails(Convert.ToInt32(hdSchemeSubjectIdDDl.Value), Convert.ToInt32(ExamPatternMappingId), Convert.ToInt32(ddlSection.SelectedValue));
        if (dsPaper.Tables[1] != null && dsPaper.Tables[1].Rows.Count > 0)
        {
            foreach (RepeaterItem Items in rptExamQuestion.Items)
            {
                ListBox ddlBloom = (ListBox)Items.FindControl("ddlBloomCategory");
                //Label lblQuestion = (Label)Items.FindControl("lblQuestionPattern");
                Label QuestionNumber = (Label)Items.FindControl("lblQuestionPattern");

                ddlBloom.ClearSelection();
                foreach (DataRow dr in dsPaper.Tables[1].Rows)
                {
                    foreach (ListItem row in ddlBloom.Items)
                    {
                        string optionvalue = Convert.ToString(dr["BLOOM_CAT_ID"]);
                        string quesno = Convert.ToString(dr["QUESTIONNO"]);
                        if (row.Value == Convert.ToString(dr["BLOOM_CAT_ID"]) && QuestionNumber.Text == Convert.ToString(dr["QUESTIONNO"]))
                        {
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        if (dsPaper.Tables[2] != null && dsPaper.Tables[2].Rows.Count > 0)
        {
            foreach (RepeaterItem Items in rptExamQuestion.Items)
            {
                ListBox ddlCO = (ListBox)Items.FindControl("ddlBranch");
                // Label lblQuestion = (Label)Items.FindControl("lblQuestionPattern");
                Label QuestionNumber = (Label)Items.FindControl("lblQuestionPattern");

                ddlCO.ClearSelection();
                foreach (DataRow dr in dsPaper.Tables[2].Rows)
                {
                    foreach (ListItem row in ddlCO.Items)
                    {
                        string optionvalue = Convert.ToString(dr["SUBCOID"]);
                        if (row.Value == Convert.ToString(dr["SUBCOID"]) && QuestionNumber.Text == Convert.ToString(dr["QUESTIONNO"]))
                        { 
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
        }
    }


    protected void txtQuestionPaperName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCourseno_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCourseno.SelectedIndex > 0)
        {
            int schemesubjectid = Convert.ToInt32(objCommon.LookUp("tblacdschemesubjectmapping", "SchemeSubjectId", "SUBJECTID=" + Convert.ToInt32(ddlCourseno.SelectedValue)));

            int ret = Convert.ToInt32(objStatC.GetExamDetails(schemesubjectid));

            if ((int)ret == 2 || (int)ret == 3)
            {

                objCommon.DisplayMessage(updEdit, "CO Is Not Created Or COPO Mapping is not Done..', '", this.Page);
                divSubject.Visible = false;
                return;
            }
            BindSubjects();
            divSubject.Visible = true;
        }
        else { divSubject.Visible = false; }
    }


   }
