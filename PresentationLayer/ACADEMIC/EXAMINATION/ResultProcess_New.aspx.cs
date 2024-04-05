using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_ResultProcess_New : System.Web.UI.Page
{
    MarksEntryController objMark = new MarksEntryController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int IS_lockStatusCount = 0;
    int IS_ProcessStatusCount = 0;

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
                //  CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["ipAddress"] = GetUserIPAddress();
                PopulateDropDownList();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        //else////with Proxy detection
        //{
        //    string[] splitter = { "," };
        //    string[] IP_Array = User_IPAddressRange.Split(splitter,
        //                                                  System.StringSplitOptions.None);

        //    int LatestItem = IP_Array.Length - 1;
        //    User_IPAddress = IP_Array[LatestItem - 1];

        //}
        return User_IPAddress;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.DisplayMessage(this, "" + Session["userdeptno"].ToString() + "", this.Page);
            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");


            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "");

            //*********************
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            ////objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_ID");  //  AND CD.UGPGOT IN (" + Session["ua_section"] + ") AND CD.UGPGOT = 1
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
            //// objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");         
            ddlSession.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RESULTPROCESSING.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Fill DropDownList

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                ddlDegree.SelectedIndex = 0;
            }
            divStudentRecord.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            btnLock.Enabled = false;
            btnUnlock.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }


    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //    if (ddlBranch.SelectedIndex > 0)
        //    {
        //        divStudentRecord.Visible = false;
        //        lvStudent.DataSource = null;
        //        lvStudent.DataBind();
        //        lvCourse.DataSource = null;
        //        lvCourse.DataBind();
        //        btnLock.Enabled = false;
        //        btnUnlock.Enabled = false;

        //        ddlScheme.Items.Clear();
        //        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO");
        //        ddlScheme.Focus();
        //        ddlSem.Items.Clear();
        //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        //        //ddlStudent.Items.Clear();
        //        //ddlStudent.Items.Add(new ListItem("Please Select", "0"));
        //    }
        //    else
        //    {
        //        ddlScheme.Items.Clear();
        //        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        //        ddlSem.Items.Clear();
        //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        //        divStudentRecord.Visible = false;
        //        lvStudent.DataSource = null;
        //        lvStudent.DataBind();
        //        lvCourse.DataSource = null;
        //        lvCourse.DataBind();
        //        btnLock.Enabled = false;
        //        btnUnlock.Enabled = false;
        //        //ddlStudent.Items.Clear();
        //        //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        //    }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            ddlSem.Focus();
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnLock.Enabled = false;
        btnUnlock.Enabled = false;
        btnProcessResult.Enabled = false;    // added on 22-02-2020 by Vaishali
    }

    #endregion

    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add("Please Select");
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add("Please Select");
        ddlSem.Items.Clear();
        ddlSem.Items.Add("Please Select");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private string GetIDNO()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;
            if (chk.Checked && ddlExamType.SelectedValue == "0")
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "," + lblStudname.ToolTip.ToString();
            }
            else if (chk.Checked && ddlExamType.SelectedValue == "1")
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "," + lblStudname.ToolTip.ToString();
            }
            else if (chk.Checked)
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "," + lblStudname.ToolTip.ToString();
            }
        }
        if (retIDNO != "")
        {
            if (retIDNO.Substring(retIDNO.Length - 1) == ",")
                retIDNO = retIDNO.Substring(0, retIDNO.Length - 1);
        }
        if (retIDNO.Equals(""))
        {
            objCommon.DisplayMessage(updresult, "Please select at least one student.", this.Page);
            //ShowMessage("updresult,Please select at least one student.");
            return "";
        }
        else return retIDNO;
    }

    protected void btnProcessResult_Click(object sender, EventArgs e)
    {
        try
        {
            //**********ADDED BY: M. REHBAR SHEIKH ON 29-05-2019 | RESULT PROCESSING AJU***********

            int OrgID = Convert.ToInt32(Session["OrgId"]);

            int exam = 0;
            int covidFlag = 0;
            MarksEntryController objProcessResult = new MarksEntryController();
            string idno = GetIDNO();
            if (idno != string.Empty)
            {
                int Check = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT WITH (NOLOCK)", "COUNT(LOCK)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND IDNO IN (" + idno + ") AND LOCK = 1 AND RESULT_TYPE=" + Convert.ToInt32(ddlStudentType.SelectedValue)));  //
                if (Check > 0)
                {
                    objCommon.DisplayMessage(updresult, "Already locked You can not reprocess the result of selected student.", this.Page);
                    return;
                }

                if (ddlStudentType.SelectedValue == "0")
                {
                    exam = 0;
                }
                else if (ddlStudentType.SelectedValue == "1")
                {
                    exam = 1;
                }
                else if (ddlStudentType.SelectedValue == "2")      //Added By Sachin A dt on 20032023 Revaluation/PhotoCopy
                {
                    exam = 2;
                }
                else if (ddlStudentType.SelectedValue == "3")      //Redo/Resit/Retest
                {
                    exam = 3;
                }
                else
                {
                    exam = -1;
                }


                if (radSelectOptions.SelectedValue == "0")
                {
                    covidFlag = 0;
                }
                if (radSelectOptions.SelectedValue == "1")
                {
                    covidFlag = 1;
                }
                CustomStatus cs = (CustomStatus)objProcessResult.ProcessResultAll(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), idno, exam, ViewState["ipAddress"].ToString(), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["OrgId"].ToString()));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updresult, "Result Processed Successfully!", this.Page);
                    //btnShow_Click(sender, e);
                    BindListView();
                }
                else
                    objCommon.DisplayMessage(updresult, "Error in Processing Result!", this.Page);
            }
            //ADDED ON 21-02-2020 BY VAISHALI
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RESULTPROCESSING.btnProcessResult_Click() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void BindListView()
    {
        if (ddlStudentType.SelectedIndex > 0)
        {
            divStudentRecord.Visible = false;

            //DataSet ds = objMark.GetresultprocessStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue));

            DataSet ds = objMark.GetresultprocessStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlStudentType.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                divStudentRecord.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                CheckBox chk = lvStudent.FindControl("chkheader") as CheckBox;
                chk.Checked = false;
                btnProcessResult.Enabled = true;
                //btnLock.Enabled = true;
                //btnUnlock.Enabled = true;
                // added on 22-02-2020 by Vaishali
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                // ADDED BY SHUBHAM FOR HIDE LISTVIEW COLUMN (ONLY SHOW ON CRESECENT)
                if (Convert.ToInt32(Session["OrgId"]) != 2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#studcount').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#studcount').hide();$('td:nth-child(7)').hide();});", true);
                }
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                btnLock.Enabled = false;
                btnUnlock.Enabled = false;
                ShowMessage("No Student Found.");
                return;
            }
        }
        else
        {
            ShowMessage("Please Select Student Type.");
            ddlStudentType.Focus();
            return;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {


            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            divStudentRecord.Visible = false;

            if (ddlStudentType.SelectedIndex > 0)
            {
                //DataSet ds = objMark.GetresultprocessStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue));
               
                //DataSet ds = objMark.GetresultprocessStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlStudentType.SelectedValue));
                DataSet ds = GetresultprocessStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlStudentType.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue));

                #region Show Patch added by rohit.d
                //DataSet ds;
                //string para_name, call_values;
                //string proc_name = "PKG_GET_RESULT_PROCESS_STUDENT_DATA";
                //para_name = "@P_SESSIONNO,@P_DEGREENO,@P_SCHEMENO,@P_SEMESTERNO,@P_COLLEGEID,@P_StudentType,@P_BRANCHNO";
                //call_values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSem.SelectedValue) + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(ddlStudentType.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue);
                //ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
                #endregion


                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DataSet dsIncompleteMarksEntry = objMark.GetAllIncompleteMarkEntry_ForResultProcess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
                    DataSet dsIncompleteMarksEntry = GetAllIncompleteMarkEntry_ForResultProcess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlBranch.SelectedValue));
                    //------Added Mahesh on Dated 03-05-2021 for Makaut to check Marks entry is completed or not, if completed then this college,semester,session,scheme,Student Type Marks Entry Lock.
                    if (dsIncompleteMarksEntry.Tables[0].Rows.Count == 0)
                    {
                       // objMark.UpdateMarksEntryLockStatusAfterMarksEntryCompleted(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
                        UpdateMarksEntryLockStatusAfterMarksEntryCompleted(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlBranch.SelectedValue));
                    }
                    // ---------------------------End------------ 

                    //DataSet dsIncompleteLockStatus = objMark.GetAllIncompleteLockStatus_ForResultProcess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
                    DataSet dsIncompleteLockStatus = GetAllIncompleteLockStatus_ForResultProcess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlBranch.SelectedValue));

                    // ADDED BY SHUBHAM FOR CHECKING GRADE ENTRY DONE OR NOT ON 13-12-2022
                    //DataSet dsIncompleteGradeEntry = objMark.GetAllIncompleteGradeStatus_ForResultProcess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
                    DataSet dsIncompleteGradeEntry = GetAllIncompleteGradeStatus_ForResultProcess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlBranch.SelectedValue));
                    string GRADEMARKS = objCommon.LookUp("ACD_SCHEME", "DISTINCT GRADEMARKS", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));

                    //added on 25-04-2020 by Vaishali
                    DataSet dsIncompleteOP2MarkEntry = null;
                    DataSet dsIncompleteOP2LockStatus = null;
                    //DataSet dsIncompleteOP2MarkEntry = objMark.GetMarkEntryNotDoneByOP2(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue));
                    //DataSet dsIncompleteOP2LockStatus = objMark.GetLockEntryNotDoneByOP2(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue));

                    if (dsIncompleteMarksEntry.Tables[0].Rows.Count > 0 || dsIncompleteLockStatus.Tables[0].Rows.Count > 0)
                    {
                        if (dsIncompleteMarksEntry.Tables[0].Rows.Count > 0)
                        {
                            lvCourse.Visible = true;
                            lvCourse.DataSource = dsIncompleteMarksEntry;
                            lvCourse.DataBind();
                            //(lvCourse.FindControl("divmark") as HtmlGenericControl).InnerText = "Below Course(s) shows Incomplete Mark Entry List. Please Complete Mark Entry Before Result Processing.";
                            HtmlGenericControl divmark = (HtmlGenericControl)lvCourse.FindControl("divmark");
                            divmark.Style.Add("font-weight", "bold");
                            divmark.InnerText = "Below Course(s) shows Incomplete Mark Entry List. Please Complete Mark Entry Before Result Processing.";
                            (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name"; //added on 25-04-2020 by Vaishali
                            lvStudent.DataSource = null;
                            lvStudent.DataBind();
                            objCommon.DisplayMessage(updresult, "Mark Entry Not Done For Current Selection !!", this.Page);
                            return;
                        }
                        else
                        {
                            //Marks Entry Completed.. Proceed
                            if (dsIncompleteLockStatus.Tables[0].Rows.Count > 0)
                            {
                                lvCourse.Visible = true;
                                lvCourse.DataSource = dsIncompleteLockStatus;
                                lvCourse.DataBind();
                                //(lvCourse.FindControl("divmark") as HtmlGenericControl).InnerText = "Below Course(s) Shows Incomplete Lock Status. Please Lock Mark Entry Before Result Processing.";
                                HtmlGenericControl divmark = (HtmlGenericControl)lvCourse.FindControl("divmark");
                                divmark.Style.Add("font-weight", "bold");
                                divmark.InnerText = "Below Course(s) Shows Incomplete Lock Status. Please Lock Mark Entry Before Result Processing.";
                                (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name"; //added on 25-04-2020 by Vaishali
                                lvStudent.DataSource = null;
                                lvStudent.DataBind();
                                objCommon.DisplayMessage(updresult, "Lock Entry Not Done For Current Selection !!", this.Page);
                                return;
                            }
                            else
                            {
                                // added by shubham on 29/05/2023 for checking Grade TYPE OR MARK TYPE 
                                if (GRADEMARKS == "G")
                                {
                                    // added by shubham on 06/02/2023 for checking Grade Entry
                                    if (dsIncompleteGradeEntry.Tables[0].Rows.Count > 0)
                                    {
                                        lvCourse.Visible = true;
                                        lvCourse.DataSource = dsIncompleteGradeEntry;
                                        lvCourse.DataBind();
                                        //(lvCourse.FindControl("divmark") as HtmlGenericControl).InnerText = "Below Course(s) Shows Incomplete Grade Entry List. Please Lock Grade Entry Before Result Processing.";
                                        HtmlGenericControl divmark = (HtmlGenericControl)lvCourse.FindControl("divmark");
                                        divmark.Style.Add("font-weight", "bold");
                                        divmark.InnerText = "Below Course(s) Shows Incomplete Grade Entry List. Please Enter Grade Entry Before Result Processing.";
                                        (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name";
                                        lvStudent.DataSource = null;
                                        lvStudent.DataBind();
                                        objCommon.DisplayMessage(updresult, "Grade Entry Not Done For Current Selection Students !!", this.Page);
                                        return;
                                    }
                                }

                                divStudentRecord.Visible = true;
                                lvCourse.DataSource = null;
                                lvCourse.DataBind();
                                lvStudent.DataSource = ds;
                                lvStudent.DataBind();
                                CheckBox chk = lvStudent.FindControl("chkheader") as CheckBox;
                                chk.Checked = false;
                                btnProcessResult.Enabled = true;
                                //btnLock.Enabled = true;
                                //btnUnlock.Enabled = true;
                                // ADDED BY SHUBHAM FOR HIDE LISTVIEW COLUMN (ONLY SHOW ON CRESECENT)
                                if (Convert.ToInt32(Session["OrgId"]) != 2)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#studcount').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#studcount').hide();$('td:nth-child(7)').hide();});", true);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (rbMarksEntryStatus.SelectedValue == "2")
                        {
                            if ((dsIncompleteOP2MarkEntry.Tables[0].Rows.Count > 0 && dsIncompleteOP2MarkEntry != null) || (dsIncompleteOP2LockStatus.Tables[0].Rows.Count > 0 && dsIncompleteOP2LockStatus != null))
                            {
                                if (dsIncompleteOP2MarkEntry.Tables[0].Rows.Count > 0 && dsIncompleteOP2MarkEntry != null)
                                {
                                    lvCourse.Visible = true;
                                    lvCourse.DataSource = dsIncompleteOP2MarkEntry;
                                    lvCourse.DataBind();
                                    //(lvCourse.FindControl("divmark") as HtmlGenericControl).InnerText = "Below Course(s) shows Incomplete Mark Entry List by Operator 2. Please Complete Mark Entry Before Result Processing.";
                                    HtmlGenericControl divmark = (HtmlGenericControl)lvCourse.FindControl("divmark");
                                    divmark.Style.Add("font-weight", "bold");
                                    divmark.InnerText = "Below Course(s) shows Incomplete Mark Entry List by Operator 2. Please Complete Mark Entry Before Result Processing.";
                                    (lvCourse.FindControl("lblFacName") as Label).Text = "Operator Name"; //added on 25-04-2020 by Vaishali
                                    lvStudent.DataSource = null;
                                    lvStudent.DataBind();
                                    objCommon.DisplayMessage(updresult, "Mark Entry Not Done by Operator 2 for the Current Selection !!", this.Page);
                                    return;
                                }
                                else
                                {
                                    //Marks Entry Completed.. Proceed
                                    if (dsIncompleteOP2LockStatus.Tables[0].Rows.Count > 0)
                                    {
                                        lvCourse.Visible = true;
                                        lvCourse.DataSource = dsIncompleteOP2LockStatus;
                                        lvCourse.DataBind();
                                        //(lvCourse.FindControl("divmark") as HtmlGenericControl).InnerText = "Below Course(s) Shows Incomplete Lock Status by Operator 2. Please Lock Mark Entry Before Result Processing.";
                                        HtmlGenericControl divmark = (HtmlGenericControl)lvCourse.FindControl("divmark");
                                        divmark.Style.Add("font-weight", "bold");
                                        divmark.InnerText = "Below Course(s) Shows Incomplete Lock Status by Operator 2. Please Lock Mark Entry Before Result Processing.";
                                        (lvCourse.FindControl("lblFacName") as Label).Text = "Operator Name"; //added on 25-04-2020 by Vaishali
                                        lvStudent.DataSource = null;
                                        lvStudent.DataBind();
                                        objCommon.DisplayMessage(updresult, "Lock Entry Not Done by Operator 2 for the Current Selection !!", this.Page);
                                        return;
                                    }
                                    else
                                    {
                                        divStudentRecord.Visible = true;
                                        lvCourse.DataSource = null;
                                        lvCourse.DataBind();
                                        lvStudent.DataSource = ds;
                                        lvStudent.DataBind();
                                        CheckBox chk = lvStudent.FindControl("chkheader") as CheckBox;
                                        chk.Checked = false;
                                        btnProcessResult.Enabled = true;
                                        // ADDED BY SHUBHAM FOR HIDE LISTVIEW COLUMN (ONLY SHOW ON CRESECENT)
                                        if (Convert.ToInt32(Session["OrgId"]) != 2)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#studcount').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#studcount').hide();$('td:nth-child(7)').hide();});", true);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //int count = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_COMP_LOG A", "COUNT(1)", "SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue + " AND EXISTS (SELECT * FROM ACD_MARKS_COMP_LOG B WHERE B.SESSIONNO = A.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND EXAMTYPE = 1) AND EXISTS (SELECT * FROM ACD_MARKS_COMP_LOG B WHERE B.SESSIONNO = A.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND EXAMTYPE = 2)")); //commented on 25-07-2020 by Vaishali as per the changes discussed to allow checking only for end sem
                                int count = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_COMP_LOG A WITH (NOLOCK)", "COUNT(1)", "SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ViewState["schemeno"] + " AND SEMESTERNO = " + ddlSem.SelectedValue + " AND EXISTS (SELECT * FROM ACD_MARKS_COMP_LOG B WHERE B.SESSIONNO = A.SESSIONNO AND A.SCHEMENO = B.SCHEMENO AND A.SEMESTERNO = B.SEMESTERNO AND EXAMTYPE = 2)"));
                                if (count > 0)
                                {
                                    divStudentRecord.Visible = true;
                                    lvCourse.DataSource = null;
                                    lvCourse.DataBind();
                                    lvStudent.DataSource = ds;
                                    lvStudent.DataBind();
                                    CheckBox chk = lvStudent.FindControl("chkheader") as CheckBox;
                                    chk.Checked = false;
                                    btnProcessResult.Enabled = true;
                                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#printreport').hide();$('td:nth-child(10)').hide();var prm =Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#printreport').hide();$('td:nth-child(10)').hide();});", true);
                                    // ADDED BY SHUBHAM FOR HIDE LISTVIEW COLUMN (ONLY SHOW ON CRESECENT)
                                    if (Convert.ToInt32(Session["OrgId"]) != 2)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#studcount').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#studcount').hide();$('td:nth-child(7)').hide();});", true);
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updresult, "Mark Entry Comparison is not done for the selection !!", this.Page);
                                    lvStudent.DataSource = null;
                                    lvStudent.DataBind();
                                    lvCourse.DataSource = null;
                                    lvCourse.DataBind();

                                    return;
                                }
                            }
                        }
                        else
                        {
                            // added by shubham on 29/05/2023 for checking Grade TYPE OR MARK TYPE 
                            if (GRADEMARKS == "G")
                            {
                                // added by shubham on 06/02/2023 for checking Grade Entry
                                if (dsIncompleteGradeEntry.Tables[0].Rows.Count > 0)
                                {
                                    lvCourse.Visible = true;
                                    lvCourse.DataSource = dsIncompleteGradeEntry;
                                    lvCourse.DataBind();
                                    //(lvCourse.FindControl("divmark") as HtmlGenericControl).InnerText = "Below Course(s) Shows Incomplete Grade Entry List. Please Lock Grade Entry Before Result Processing.";
                                    HtmlGenericControl divmark = (HtmlGenericControl)lvCourse.FindControl("divmark");
                                    divmark.Style.Add("font-weight", "bold");
                                    divmark.InnerText = "Below Course(s) Shows Incomplete Grade Entry List. Please Enter Grade Entry Before Result Processing.";

                                    (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name"; //added on 25-04-2020 by Vaishali
                                    lvStudent.DataSource = null;
                                    lvStudent.DataBind();
                                    objCommon.DisplayMessage(updresult, "Grade Entry Not Done For Current Selection Students !!", this.Page);
                                    return;
                                }
                            }

                            divStudentRecord.Visible = true;
                            lvCourse.DataSource = null;
                            lvCourse.DataBind();
                            lvStudent.DataSource = ds;
                            lvStudent.DataBind();
                            CheckBox chk = lvStudent.FindControl("chkheader") as CheckBox;
                            chk.Checked = false;
                            btnProcessResult.Enabled = true;
                            //btnLock.Enabled = true;
                            //btnUnlock.Enabled = true;
                            // ADDED BY SHUBHAM FOR HIDE LISTVIEW COLUMN (ONLY SHOW ON CRESECENT)
                            if (Convert.ToInt32(Session["OrgId"]) != 2)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#studcount').hide();$('td:nth-child(7)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#studcount').hide();$('td:nth-child(7)').hide();});", true);
                            }


                        }
                    }
                }
                else
                {
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    btnLock.Enabled = false;
                    btnUnlock.Enabled = false;
                    //ShowMessage("No Student Found.");
                    objCommon.DisplayMessage(updresult, "No Student Found !!", this.Page); //added on 25-04-2020 by Vaishali
                    return;
                }
            }
            else
            {
                //ShowMessage("Please Select Student Type.");
                objCommon.DisplayMessage(updresult, "Please Select Student Type !!", this.Page); //added on 25-04-2020 by Vaishali
                ddlStudentType.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divStudentRecord.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            btnLock.Enabled = false;
            btnUnlock.Enabled = false;
            ////objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", " D.DEGREENAME", " CD.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENO");//+ "AND CDB.UGPGOT IN (" + Session["ua_section"] + ") AND CDB.UGPGOT = 1"
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENO");
            //ddlDegree.Focus();

            //Added Mahesh on Dated 01-05-2021 For Remove Validation of Degree,Branch-Only For Makaut Project.
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT R WITH (NOLOCK) ON(S.SCHEMENO=R.SCHEMENO) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (R.IDNO=ST.IDNO)", "DISTINCT S.SCHEMENO", "S.SCHEMENAME", "ST.COLLEGE_ID=" + ddlColg.SelectedValue + " AND R.SESSIONNO=" + ddlSession.SelectedValue, "S.SCHEMENO");
            ddlScheme.Focus();

            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = GetIDNO();
            if (idno != string.Empty)
            {
                int cs = objMark.UpdateResultlockStatus(idno, Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Session["username"].ToString(), ViewState["ipAddress"].ToString(), 1, Convert.ToInt32(ddlStudentType.SelectedValue));
                if (cs == 1)
                {
                    objCommon.DisplayMessage(updresult, "Result Locked Successfully !!!!", this.Page);
                    btnUnlock.Enabled = true;
                    //btnShow_Click(sender, e);
                    BindListView();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ResultProcess.UpdateResultlockStatus-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    //added by reena
    protected void btnUnlock_Click(object sender, EventArgs e)
    {

        try
        {
            string idno = GetIDNO();
            if (idno != string.Empty)
            {
                int cs = objMark.UpdateResultlockStatus(idno, Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Session["username"].ToString(), ViewState["ipAddress"].ToString(), 0, Convert.ToInt32(ddlStudentType.SelectedValue));
                if (cs == 0)
                {
                    objCommon.DisplayMessage(updresult, "Result UnLocked Successfully !!!!", this.Page);
                    btnUnlock.Enabled = false;
                    //btnShow_Click(sender, e);
                    BindListView();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ResultProcess.UpdateResultlockStatus-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        //  string CHKPROCESS = objCommon.LookUp("ACD_TRRESULT", "COUNT(1)", "IDNO IN (" + idno + ")");
        //foreach (ListViewDataItem item in lvStudent.Items)
        //{
        //    int i = 0;
        //    string idno = string.Empty;
        //    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
        //    Label lblStudname = item.FindControl("lblStudname") as Label;
        //    if (chk.Checked == true)
        //    {
        //        idno = lblStudname.ToolTip.ToString();
        //        string CHKPROCESS = objCommon.LookUp("ACD_TRRESULT", "COUNT(1)", "IDNO IN (" + idno + ")");
        //        if (CHKPROCESS == "0")
        //        {
        //            objCommon.DisplayMessage(updresult, lblStudname.Text + " student result is not processed,so cant not lock it.", this.Page);
        //            return;
        //        }
        //        else
        //        {
        //            int cs = objMark.UpdateResultlockStatus(idno, Convert.ToInt32(ddlSem.SelectedValue));
        //            if (cs == 2)
        //            {
        //                objCommon.DisplayMessage(updresult, "Result locked succesfully", this.Page);
        //                btnShow_Click(sender, e);
        //                return;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        i++;
        //    }
        //    if (i > 0)
        //    {
        //        objCommon.DisplayMessage(updresult, "Please select at least one student.", this.Page);
        //        return;
        //    }
        //}
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSession.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();

            
            //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            ddlSem.Focus();
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnLock.Enabled = false;
        btnUnlock.Enabled = false;
        btnProcessResult.Enabled = false;    // added on 22-02-2020 by Vaishali


        //*divStudentRecord.Visible = false;
        //lvStudent.DataSource = null;
        //lvStudent.DataBind();
        //lvCourse.DataSource = null;
        //lvCourse.DataBind();
        //btnLock.Enabled = false;
        //*btnUnlock.Enabled = false;
    }

    protected void ddlStudentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnLock.Enabled = false;
        btnUnlock.Enabled = false;
    }

    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblLockStatus = e.Item.FindControl("lblLockstatus") as Label;
        Label lblProcessStatus = e.Item.FindControl("lblPstatus") as Label;
        //if (e.Item.ItemType == ListViewItemType.DataItem)
        //{
        //    // Find the TableCell for the column you want to hide
        //    TableCell cellToHide = e.Item.FindControl("Srno") as TableCell;

        //    // Check if the cell is found
        //    //if (cellToHide != null)
        //    //{
        //        // Set the Visible property to false to hide the column
        //        cellToHide.Visible = false;
        //    //}
        //}
        if (lblProcessStatus.ToolTip == "1")
        {
            IS_ProcessStatusCount += 1;
            //btnLock.Enabled = true;
        }

        if (lblLockStatus.ToolTip == "1")
        {
            IS_lockStatusCount += 1;
            //btnUnlock.Enabled = true;
        }
        if (IS_ProcessStatusCount > 0)
        {
            btnLock.Enabled = true;
        }
        else
        {
            btnLock.Enabled = false;
        }
        if (IS_lockStatusCount > 0)
        {
            btnUnlock.Enabled = true;
        }
        else
        {
            btnUnlock.Enabled = false;
        }
        //else if (lblProcessStatus.ToolTip == "0")
        //{
        //    btnLock.Enabled = false;
        //}
        //else if (lblLockStatus.ToolTip == "0")
        //{
        //    btnUnlock.Enabled = false;
        //}
    }

    protected void rbMarksEntryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnProcessResult.Enabled = false;
        btnLock.Enabled = false;
        btnUnlock.Enabled = false;
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {
            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]


            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "ISNULL(ACTIVESTATUS,0)=1 AND BRANCHNO>0", "LONGNAME asc");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            }
        }
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        //lvStudent.Visible = false;

    }



    #region -----------------------------------------Controllers-----------------------------------------------
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


    public DataSet GetresultprocessStudentData(int sessionno, int degreeno, int schemeno, int semesterno, int colgid, int StudentType,int branchno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_collegeid", colgid),
                            new SqlParameter("@P_StudentType", StudentType),
                            new SqlParameter("@P_BRANCHNO", branchno),
                        };
            ds = objDataAccess.ExecuteDataSetSP("PKG_GET_RESULT_PROCESS_STUDENT_DATA_USING_BRANCH", sqlParams);

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
        }
        return ds;
    }

    public DataSet GetAllIncompleteMarkEntry_ForResultProcess(int sessionno,int schemeno, int semesterno, int prev_status, int College_Id,int branchno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),  
                            new SqlParameter("@P_COLLEGE_ID", College_Id), 
                            new SqlParameter("@P_BRANCHNO", branchno) 
                            //new SqlParameter("@P_COURSENO", courseno) 
                        };

            ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_MARK_ENTRY_NOT_DONE_FOR_RESULTPROCESS_USING_BRANCH", sqlParams);

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetAllIncompleteMarkEntry_ForResultProcess --> " + ex.Message);
        }
        return ds;
    }


    public int UpdateMarksEntryLockStatusAfterMarksEntryCompleted(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id, int branchno)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),  
                            new SqlParameter("@P_COLLEGE_ID", College_Id),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

            objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_MARKSENTRYLOCKSTATUS_AFTERMARKSENTRY_COMPLETED_USING_BRANCH", objParams, true);
            if (ret != null && ret.ToString() == "1")
            {
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            else
                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarksEntryLockStatusAfterMarksEntryCompleted --> " + ex.ToString());
        }
        return retStatus;
    }

    public DataSet GetAllIncompleteLockStatus_ForResultProcess(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id, int branchno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),
                            new SqlParameter("@P_COLLEGE_ID", College_Id),
                            new SqlParameter("@P_BRANCHNO",branchno)
                        };

            ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_LOCK_ENTRY_NOT_DONE_FOR_RESULTPROCESS_USING_BRANCH", sqlParams);

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetAllIncompleteLockStatus_ForResultProcess --> " + ex.Message);
        }
        return ds;
    }


    public DataSet GetAllIncompleteGradeStatus_ForResultProcess(int sessionno, int schemeno, int semesterno, int prev_status, int College_Id, int branchno)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno) ,                   
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),
                            new SqlParameter("@P_COLLEGE_ID", College_Id),
                            new SqlParameter ("@P_BRANCHNO", branchno)
                        };

            ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_GRADE_ENTRY_NOT_DONE_FOR_RESULTPROCESS_USING_BRANCH", sqlParams);

        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetAllIncompleteLockStatus_ForResultProcess --> " + ex.Message);
        }
        return ds;
    }
    #endregion


}
