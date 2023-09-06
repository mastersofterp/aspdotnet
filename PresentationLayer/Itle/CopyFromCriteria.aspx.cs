using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;


public partial class Itle_CopyFromCriteria : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CopyQuestionToNextSession objIQBC = new CopyQuestionToNextSession();
    CopyQuestionToNextSessionEnt objQuest = new CopyQuestionToNextSessionEnt();
   

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
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                FillDropdownSession();
            }
          
        }
           
         
    }
    private void FillDropdownSession()
    {
        objCommon.FillDropDownList(ddlFromSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");

        objCommon.FillDropDownList(ddlToSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");
         
    }

    private void FillCourse()
    {
        objCommon.FillDropDownList(ddlFromCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (CT.COURSENO = C.COURSENO)", "distinct CT.COURSENO", "isnull(C.CCODE,'')+' '+isnull(C.COURSE_NAME,'') AS COURSENAME", "CT.SESSIONNO=" + Convert.ToInt32(ddlFromSession.SelectedValue) + "", "CT.COURSENO");   
         
    }
    private void BindListView()
    {
        try
        {
            objQuest.OBJECTIVE_DESCRIPTIVE = Convert.ToChar(rbnObjectiveDescriptive.SelectedValue);
            objQuest.COURSENO = Convert.ToInt32(ddlFromCourse.SelectedValue);
            objQuest.UA_NO = Convert.ToInt32(Session["userno"]);
            if (ddlTopic.SelectedIndex > 0)
            {
                objQuest.TOPIC = ddlTopic.SelectedItem.Text;
            }
            else
            {
                objQuest.TOPIC = "0";
            }

            DataSet ds = objIQBC.GetQuestion(objQuest);

            if (ds == null)
            {
                pnllvquestion.Visible = false;

            }
            else
            {
                pnllvquestion.Visible = true;
                lvQuestions.DataSource = ds.Tables[0];
                lvQuestions.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rbnObjectiveDescriptive_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        pnllvquestion.Visible = true;
    }
    protected void ddlFromSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFromSession.SelectedIndex > 0)
        {
            FillCourse();
        }
        else
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Session ...!", this.Page);
        }

       
    }
    protected void ddlFromCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFromCourse.SelectedIndex > 0)
        {         


            objCommon.FillDropDownList(ddlTopic, "ACD_IQUESTIONBANK", "DISTINCT TOPIC", "TOPIC as TOPIC1", "COURSENO=" + Convert.ToInt32(ddlFromCourse.SelectedValue) + "", "TOPIC");
        
        }
        else
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Session ...!", this.Page);
        }

    }
    protected void ddlToSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((ddlFromSession.SelectedIndex ) == (ddlToSession.SelectedIndex ))       
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Another Session ...!", this.Page);
            ddlToSession.SelectedValue = "0";
        }
        if (ddlToSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlToCourses, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (CT.COURSENO = C.COURSENO)", "distinct CT.COURSENO", "isnull(C.CCODE,'')+' '+isnull(C.COURSE_NAME,'') AS COURSENAME", "CT.SESSIONNO=" + Convert.ToInt32(ddlFromSession.SelectedValue) + "", "CT.COURSENO");   
        
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlToCourses.SelectedIndex > 0)
            {
                objQuest.COURSENO = Convert.ToInt32(ddlToCourses.SelectedValue);
                DataTable QuestionTbl = new DataTable("QuestionTbl");
                QuestionTbl.Columns.Add("COURSENO", typeof(int));
                QuestionTbl.Columns.Add("QUESTIONNO", typeof(int));
                QuestionTbl.Columns.Add("QUESTIONTEXT", typeof(string));
                QuestionTbl.Columns.Add("TOPIC", typeof(string));
                QuestionTbl.Columns.Add("MARKS_FOR_QUESTION", typeof(int));



                DataRow dr = null;
                foreach (ListViewItem i in lvQuestions.Items)
                {
                    CheckBox chkQueNo = (CheckBox)i.FindControl("chkQueNo");
                    HiddenField hidQueNo = (HiddenField)i.FindControl("hidQueNo");
                    Label LblQUESTIONTEXT = (Label)i.FindControl("LblQUESTIONTEXT");
                    Label LblTOPIC = (Label)i.FindControl("LblTOPIC");
                    Label LblMARKS_FOR_QUESTION = (Label)i.FindControl("LblMARKS_FOR_QUESTION");


                    if (chkQueNo.Checked == true)
                    {
                        dr = QuestionTbl.NewRow();
                        dr["COURSENO"] = ddlFromCourse.SelectedValue;
                        dr["QUESTIONNO"] = Convert.ToInt32(hidQueNo.Value);
                        dr["QUESTIONTEXT"] = LblQUESTIONTEXT.Text;
                        dr["TOPIC"] = LblTOPIC.Text;
                        dr["MARKS_FOR_QUESTION"] = LblMARKS_FOR_QUESTION.Text;

                        QuestionTbl.Rows.Add(dr);
                    }
                }


                objQuest.QuestionTbl_TRAN = QuestionTbl;


                CustomStatus cs = (CustomStatus)objIQBC.AddIQuestionToCourse(objQuest);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Transfer Successfully.');", true);
                    pnllvquestion.Visible = false;
                    lvQuestions.DataSource = null;
                    lvQuestions.DataBind();
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Enter Another Course ...!", this.Page);
            
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_QuestionBankMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
        pnllvquestion.Visible = true;
    }
}
  
