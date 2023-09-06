//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ANSWERSHEET RECEIVE                                                     
// CREATION DATE : 20-DEC-2015                                                          
// CREATED BY    : SUMIT WADASKAR                                 
// MODIFIED DATE :                                                          
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_ANSWERSHEET__AnswersheetRecieveHOD : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AnswerSheetController objAnsSheetController = new AnswerSheetController();


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
        try
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
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    this.FillDropdown();
                    btnReport.Enabled = false;
                    //Calendar1.EndDate = DateTime.Now;  
                  
                }
            }
            //this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_RECIEVE_AnswersheetRecieve_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AnswersheetRecieve.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AnswersheetRecieve.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSessionrpt, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
           // objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "EXAMNO >5", "EXAMNO DESC");
            objCommon.FillDropDownList(ddlBranchrpt, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0", "BRANCHNO");
       
            
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_RECIEVE_AnswersheetRecieve.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    # region Other Events
    private void BindListView()
    {
        try
        {

            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            DataSet ds = objAnsSheetController.GetAnswerSheetCourses(objAnsSheet);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();

                foreach (ListViewDataItem items in lvStudents.Items)
                {
                    RangeValidator rvCount = items.FindControl("rvansered") as RangeValidator;
                    TextBox txtAnsRced = items.FindControl("txtAnsRced") as TextBox;
                    TextBox txtExamDate = items.FindControl("txtExamDate") as TextBox;
                    TextBox txtAnsSub = items.FindControl("txtAnsSub") as TextBox;
                    TextBox txtRemark = items.FindControl("txtRemark") as TextBox;
                    DropDownList ddlExamtime = items.FindControl("ddlExamtime") as DropDownList;


                    if (txtAnsRced.ToolTip.ToString().Trim() != string.Empty || txtAnsRced.ToolTip.ToString().Trim() != "")
                    {
                        rvCount.MaximumValue = txtAnsRced.ToolTip.ToString().Trim();
                    }
                    else
                    {
                        rvCount.MaximumValue = "0";
                    }

                    if (txtRemark.ToolTip.ToString().Trim() != string.Empty || txtRemark.ToolTip.ToString().Trim() != "")
                    {
                        txtAnsRced.Enabled = false;
                        txtAnsSub.Enabled = false;
                        txtExamDate.Enabled = false;
                        ddlExamtime.Enabled = false;
                    }


                    ddlSession.Enabled = false;
                    ddlDegree.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlScheme.Enabled = false;
                }
            }

            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.uptPnl, "Records not found.", this.Page);
                //  btnSubmit.Enabled = false;lvStudents
                btnReport.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindListView();
        btnReport.Enabled = true;
        pnlStudent.Visible = true;

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO =" + ddlDegree.SelectedValue, "BRANCHNO");
        //    ddlBranch.Focus();
        //}

        //lvStudents.DataSource = null;
        //lvStudents.DataBind();

     
        if (ddlDegree.SelectedIndex > 0)
        {
            // this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME","DEGREENO="+ddlDegree.SelectedValue , "SHORTNAME");
            this.objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH  ACD INNER JOIN ACD_BRANCH B ON(ACD.BRANCHNO=B.BRANCHNO) ", " distinct(acd.BRANCHNO)", "B.LONGNAME", "DEGREENO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "ACD.BRANCHNO");
        }
        else
        {
            objCommon.DisplayMessage("Please select degree", Page);
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
   
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0 && ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ddlBranch.SelectedValue + "AND DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO");
            pnlStudent.Visible = false;
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER SE ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ddlScheme.SelectedValue, "SE.SEMESTERNO");

        //objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME EN INNER JOIN ACD_SCHEME SC ON EN.PATTERNNO = SC.PATTERNNO ", "EN.EXAMNO", "EN.EXAMNAME", "SC.SCHEMENO = " + ddlScheme.SelectedValue + " AND (EN.EXAMNAME <>'')", "EXAMNO DESC");
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();

    }
    # endregion

    #region Click Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string coursename = string.Empty;
            string uanoRecd = string.Empty;
            string uanoSub = string.Empty;
            string AnsRecd = string.Empty;
            string Date = string.Empty;
           // string Remark = string.Empty;
            string Slot = string.Empty;
            string recstaffuano = Session["userno"].ToString();

            int countduties = 0;


            foreach (ListViewDataItem items in lvStudents.Items)
            {
                //if (Convert.ToInt32((items.FindControl("ddlRcedStaff") as DropDownList).SelectedValue) > 0 && Convert.ToInt32((items.FindControl("txtAnsSub") as DropDownList).SelectedValue) > 0)
                if ((items.FindControl("txtAnsSub") as TextBox).Text != string.Empty && recstaffuano.ToString() != string.Empty && (items.FindControl("ddlExamtime") as DropDownList).Text != string.Empty)
                {
                    coursename += (items.FindControl("hdfcoursename") as HiddenField).Value + ",";
                    AnsRecd += (items.FindControl("txtAnsRced") as TextBox).Text + ",";
                    uanoRecd += recstaffuano.ToString() + ",";
                    uanoSub += (items.FindControl("txtAnsSub") as TextBox).Text + ",";

                    Date += (items.FindControl("txtExamDate") as TextBox).Text + ",";
                    //Remark += (items.FindControl("txtRemark") as TextBox).Text + ",";
                    Slot += (items.FindControl("ddlExamtime") as DropDownList).Text + ",";
                    countduties++;
                }

            }
            if (uanoSub.Length <= 0)
            {
                objCommon.DisplayMessage(this.uptPnl, "Please Insert Total Answersheet and Its Submission Date.", this.Page);

                return;
            }


            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objAnsSheet.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objAnsSheet.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            //objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
            objAnsSheet.ExamNo = 0;
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            objAnsSheet.CourseName = coursename.TrimEnd(',');
            objAnsSheet.SplitAnsRecd = AnsRecd.TrimEnd(',');
            objAnsSheet.SplitUanoRecd = uanoRecd.TrimEnd(',');
            objAnsSheet.SplitUanoSub = uanoSub.TrimEnd(',');
            objAnsSheet.SplitReportTime = Date.TrimEnd(',');
            objAnsSheet.SplitSlot = Slot.TrimEnd(',');
            //objAnsSheet.SplitRemark = Remark.ToString();
          //  objAnsSheet.ExamSlot = Convert.ToInt32(ddlExamtime.SelectedValue);

            CustomStatus cs = (CustomStatus)objAnsSheetController.InsertAnswerSheetMarkHOD(objAnsSheet);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.uptPnl, "Answersheet Save Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.uptPnl, "Server Error...", this.Page);
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lvStudents_ItemDataBound(Object sender, ListViewItemEventArgs e)
    {
        try
        {
            //DropDownList ddlRcedStaff = e.Item.FindControl("ddlRcedStaff") as DropDownList;
            DropDownList ddlExamtime = e.Item.FindControl("ddlExamtime") as DropDownList;

           

            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_STAFF", "DISTINCT(EXAM_STAFF_NO)", "STAFF_NAME", "(STAFF_TYPE='S' OR STAFF_TYPE='B') AND ACTIVE=1 AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "EXAM_STAFF_NO");
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTableReader dtr = ds.Tables[0].CreateDataReader();
            //    while (dtr.Read())
            //    {
            //        ddlRcedStaff.Items.Add(new ListItem(dtr["STAFF_NAME"].ToString(), dtr["EXAM_STAFF_NO"].ToString()));
                
            //    }
            //}
            

            DataSet ds1 = objCommon.FillDropDown("ACD_EXAM_TT_SLOT", "DISTINCT(SLOTNO)", "SLOTNAME", "SLOTNO >0", "SLOTNO");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds1.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlExamtime.Items.Add(new ListItem(dtr["SLOTNAME"].ToString(), dtr["SLOTNO"].ToString()));

                }
            }
            //ddlRcedStaff.SelectedValue = ddlRcedStaff.ToolTip;
            ddlExamtime.SelectedValue = ddlExamtime.ToolTip;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ANSWERSHEETRECD", "rptanswersheetRecieveHOD.rpt");
    }

    protected void btnPacking_Click(object sender, EventArgs e)
    {
        ShowReport("ANSWERSHEETPACK", "rptanswersheetRecievePacking.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic,Answersheet," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue+",@username=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.uptPnl, this.uptPnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnReportReport_Click(object sender, EventArgs e)
    {
        ShowReportReport("ANSWERSHEETRECD", "rptanswersheetRecieveHODReport.rpt");
    }

    protected void btnPackingReport_Click(object sender, EventArgs e)
    {
        ShowReportReport("ANSWERSHEETPACK", "rptanswersheetRecievePackingReport.rpt");
    }

    private void ShowReportReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic,Answersheet," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSessionrpt.SelectedValue + ",@P_DEGREENO=" + 0 + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + 0 + ",@P_SCHEMENO=" + 0 + ",@username=" + Session["username"].ToString() + ",@P_EXAMDATE=" + Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd");
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.uptPnl, this.uptPnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        Clear();

    }

    private void Clear()
    {
        ddlBranch.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSessionrpt.SelectedIndex = 0;
        ddlBranchrpt.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        //ddlExam.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        txtExamDate.Text = string.Empty;
        ddlBranch.Enabled = true;
        ddlSession.Enabled = true;
        ddlDegree.Enabled = true;
        //ddlExam.Enabled = true;
        ddlScheme.Enabled = true;
        ddlSem.Enabled = true;
        btnReport.Enabled = false;
    }
    # endregion
}
