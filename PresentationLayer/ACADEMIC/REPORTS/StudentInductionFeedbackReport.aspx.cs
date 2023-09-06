//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE ALLOTMENT                                      
// CREATION DATE : 15-OCT-2011
// CREATED BY    :                                                 
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
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

public partial class ACADEMIC_REPORTS_StudentInductionFeedbackReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "distinct SESSIONNO", "SESSION_PNAME", "SESSIONNO>0", "SESSIONNO desc");
                ddlSession.SelectedIndex = 0;

                DataSet ds = objCommon.FillDropDown("ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 ", "A.DEGREENO");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
                }
                //this.PopulateDropDownList();
               // ddlFeedbackType.SelectedIndex = 1;

               // PopulateDegree();
            }
        }
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

   private void PopulateDegree()
    {
        DataSet ds = objCommon.FillDropDown("ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 ", "A.DEGREENO");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
    }

    //private void PopulateDropDownList()
    //{
    //    try
    //    {
    //        if (Session["usertype"].ToString() != "1")
    //        {
    //            string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
    //            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
    //        }
    //        else
    //        {
    //            ViewState["DEPTNO"] = "0";
    //            ///objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "StudentInductionFeedbackReport.aspx.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //        {
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentInductionFeedbackReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInductionFeedbackReport.aspx");
        }
    }

    //private void FillTeacher()
    //{
    //    objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "DISTINCT CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.th_pr =" + ddltheorypractical.SelectedValue, "UA.UA_FULLNAME");
    //}

    //private void BindListView()
    //{
    //    try
    //    {
    //        //Fill Teacher DropDown
    //       // this.FillTeacher();

    //        StudentFeedBackController objSFC = new StudentFeedBackController();
    //        DataSet ds = new DataSet();

    //        ds = objSFC.GetStudentDetailsForFeedback(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDegree.SelectedValue), Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToInt32(rbSortBy.SelectedValue));

    //        if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvStudents.DataSource = ds;
    //            lvStudents.DataBind();
    //            btnStudentFeedbackReport.Visible = true;
    //        }
    //        else
    //        {
    //            lvStudents.DataSource = null;
    //            lvStudents.DataBind();
    //            objCommon.DisplayMessage(this.UpdatePanel1, "No Record Found for Current Selection!!", this.Page);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_StudentInductionFeedbackReport.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void rbRollNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    //this.BindListView();
    //}
    //protected void rbUSNNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    //this.BindListView();
    //}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
       // this.BindListView();
    }


    private string GetDegree()
    {
        string degreeNo = "";
        string degreeno = string.Empty;
        //  degreeNo = hdndegreeno.Value;
        //pnlFeeTable.Update();
        foreach (ListItem item in ddlDegree.Items)
        {
            if (item.Selected == true)
            {
                degreeNo += item.Value + '$';
            }
        }

        degreeno = degreeNo.Substring(0, degreeNo.Length - 1);
        if (degreeno != "")
        {
            string[] degValue = degreeno.Split('$');
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return degreeno;

    }

    private string GetBranch()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;
        //  degreeNo = hdndegreeno.Value;
        //pnlFeeTable.Update();
        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }
        if (X == 0)
        {
            branchNo = "0";
        }
        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');

        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return branchno;

    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                ddlDegree.Items.Clear();
                ddlBranch.Items.Clear();
                //ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlSchClg.SelectedValue), "A.DEGREENAME"
                DataSet ds = objCommon.FillDropDown("ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 ", "A.DEGREENO");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
                }
            }
            else
                objCommon.DisplayMessage(this.Page, "Please Select Session", this.Page);
        }
        catch { }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string deg = GetDegree();
        deg = deg.Replace('$', ',');
        ViewState["DegreeNo"] = deg;
        string[] DegreeNo = deg.Split(',');

        DataSet ds = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "B.BRANCHNO", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "B.DEGREENO in(" + deg + ")", "B.DEGREENO,B.BRANCHNO");
        ddlBranch.Items.Clear();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
    }



    protected void btnStudentFeedbackReport_Click(object sender, EventArgs e)
    {
        try
        {
                string param = string.Empty;
                int CTID = 0;

                string degreeno = GetDegree();
                string branchno = GetBranch();

                if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
                {
                CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
                   // param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_CTID=" + CTID;
                    param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID;
                   
                    ShowReport("Induction Feedback Report", "StudentInductionFeedbackCount.rpt", param);     //done
                }
               
                
               //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

                hideShowButtons();  
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnStudentFeedbackReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnStudentExitFeedbackReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;

            string degreeno = GetDegree();
            string branchno = GetBranch();

            if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
                param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID;
                ShowReport("Exit Feedback Report", "StudentExitFeedbackCount.rpt", param);
            }

            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnStudentExitFeedbackReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        ////To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }


    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDegree.SelectedIndex > 0)
    //    {
    //        //if (Session["usertype"].ToString() != "1")
    //        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
    //        //else
    //            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
    //            DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
    //            string BranchNos = "";
    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
    //            }
    //            DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
    //            //on faculty login to get only those dept which is related to logged in faculty
    //            objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
    //            ddlBranch.Focus();

    //            ddlBranch.SelectedIndex = 0;
    //    }
    //    else
    //    {
    //       // ddlBranch.Items.Clear();
    //        ddlDegree.SelectedIndex = 0;
    //        ddlBranch.SelectedIndex = 0;
    //    }
    //}

   
    protected void btnStudentFeedbackGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }
            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID;

            ShowReport("Induction Feedback Graph Report", "StudentInductionFeedbackCountGraph.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnStudentFeedbackGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBridgeCourseGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }

            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=1";

            ShowReport("Bridge Course Graph Report", "StudentInductionFeedbackQualityBridge.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnBridgeCourseGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnConductionOfInductionGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }

            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno+ ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=2";

            ShowReport("Conduction Of Induction Graph Report", "StudentInductionFeedbackConductionOfInduction.rpt", param);
            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnConductionOfInductionGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnEminentSpeakersGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }
            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=3";

            ShowReport("Eminent Speakers Graph Report", "StudentInductionFeedbackEminentSpeakers.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnEminentSpeakersGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void btnExtraCurriGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }
            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=4";

            ShowReport("Extra Curricular Graph Report", "StudentInductionFeedbackExtraCurricular.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnExtraCurriGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCalibrationEventsGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }

            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=5";

            ShowReport("Calibration Events Graph Report", "StudentInductionFeedbackCalibrationEvents.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnCalibrationEventsGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnYogaTrainingGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }
            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=6";

            ShowReport("Yoga Training Graph Report", "StudentInductionFeedbackYogaTraining.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnYogaTrainingGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void btnConductionOf3WeeksGraphReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }
            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + ",@P_QUESTIONNAME=7";

            ShowReport("3 Weeks Induction Graph Report", "StudentInductionFeedback3WeeksInduction.rpt", param);

            hideShowButtons();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnConductionOf3WeeksGraphReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void ddlFeedbackType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hideShowButtons();

        if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
        {
            btnExitFeedbackReport.Text = "Induction FeedBack Report";
        }
        else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
        {
            btnExitFeedbackReport.Text = "Exit FeedBack Report";
        }
    }



    public void hideShowButtons()
    {
        if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
        {
            btnStudentFeedbackReport.Visible = true;
            btnStudentExitFeedbackReport.Visible = false;
            btnStudentFeedbackGraphReport.Visible = true;

            btnExitFeedbackComment.Visible = false;

            btnBridgeCourseGraphReport.Visible = false;
            btnConductionOfInductionGraphReport.Visible = true;
            btnEminentSpeakersGraphReport.Visible = true;
            btnExtraCurriGraphReport.Visible = true;
            btnCalibrationEventsGraphReport.Visible = true;
            btnYogaTrainingGraphReport.Visible = true;
            btnConductionOf3WeeksGraphReport.Visible = true;

            btnExitFeedbackConsolidated.Visible = false;
            
            btnClear.Visible = true;
            btnExitClear.Visible = false;

            btnExitFeedbackReport.Visible = true;
        }
        else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
        {
            btnStudentFeedbackReport.Visible = false;
            btnStudentExitFeedbackReport.Visible = true;
            btnStudentFeedbackGraphReport.Visible = false;

            btnExitFeedbackComment.Visible = true;

            btnBridgeCourseGraphReport.Visible = false;
            btnConductionOfInductionGraphReport.Visible = false;
            btnEminentSpeakersGraphReport.Visible = false;
            btnExtraCurriGraphReport.Visible = false;
            btnCalibrationEventsGraphReport.Visible = false;
            btnYogaTrainingGraphReport.Visible = false;
            btnConductionOf3WeeksGraphReport.Visible = false;

            btnExitFeedbackConsolidated.Visible = true;
            btnExitFeedbackReport.Visible = true;
            btnClear.Visible = false;
            btnExitClear.Visible = true;
        }
        else if (ddlFeedbackType.SelectedIndex == 0)
        {
            btnStudentFeedbackReport.Visible = true;
            btnStudentExitFeedbackReport.Visible = true;
            btnStudentFeedbackGraphReport.Visible = true;
            btnExitFeedbackComment.Visible = true;
            btnBridgeCourseGraphReport.Visible = false;
            btnConductionOfInductionGraphReport.Visible = true;
            btnEminentSpeakersGraphReport.Visible = true;
            btnExtraCurriGraphReport.Visible = true;
            btnCalibrationEventsGraphReport.Visible = true;
            btnYogaTrainingGraphReport.Visible = true;
            btnConductionOf3WeeksGraphReport.Visible = true;

            btnExitFeedbackConsolidated.Visible = true;
            btnExitFeedbackReport.Visible = true;
            btnClear.Visible = true;
            btnExitClear.Visible = false;
        }
    }

    protected void btnExitFeedbackConsolidated_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
            }
            
            string degreeno = GetDegree();
            string branchno = GetBranch();
            //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

            param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + "";

            ShowReport("Exit Consolidated Report", "StudentExitFeedbackConsolidated.rpt", param);
            
            hideShowButtons();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnExitFeedbackConsolidated_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExitClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnExitFeedbackReport_Click(object sender, EventArgs e)
    {
        //objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_ONLINE_FEEDBACK F ON (S.IDNO=F.IDNO)"," DISTINCT S.IDNO,S.REGNO,S.STUDNAME","DEGREENO="+ddlDegree.SelectedValue+ "AND BRANCHNO="+ddlBranch.SelectedValue+"AND CTID=4");
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Induction Feedback")
            {
            CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Induction%'"));
                string degreeno = GetDegree();
                string branchno = GetBranch();
                //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

                param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + "";

                ShowReport("Induction FeedBack Report", "rptInduction_Feedback.rpt", param);
            }
            else if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
                string degreeno = GetDegree();
                string branchno = GetBranch();
                //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

                param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + "";

                ShowReport("Exit FeedBack Report", "RptExit_Feedback.rpt", param);
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Reports_InductionFeedbackReport.btnExitFeedbackReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }




    protected void btnExitFeedbackComment_Click(object sender, EventArgs e)
    {
        try
        {
            string param = string.Empty;
            int CTID = 0;
            if (ddlFeedbackType.SelectedItem.Text == "Exit Feedback")
            {
                 CTID = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
                string degreeno = GetDegree();
                string branchno = GetBranch();
                //CTID=2 for ACADEMIC_REPORTS_StudentInductionFeedbackReport Induction Program

                param = "@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_CTID=" + CTID + "";

                ShowReport("Exit FeedBack Report", "RptExit_FeedbackWithComments.rpt", param);
            }

        }
        catch { }
    }
}
