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
using IITMS.NITPRM;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_Feedback_Activity : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


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
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["sessionactivityno"] = "0";

                    // Fill drop down lists
                    //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONID DESC"); // ADDED ORDER BY DESC CLAUSE BY Nehal N. ON DATED 30.06.2023

                    objCommon.FillDropDownList(ddlFeedbackType, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0", "FEEDBACK_NO");

                    ddlSemester.DataSource = null;
                    ddlSemester.DataBind();
                    populatedropdown();

                }
            }

            this.LoadDefinedSessionActivities();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Feedback_Activity.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void populatedropdown()
    {
        ddlDegree.Items.Clear();
        DataSet ds = objCommon.FillDropDown("ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 ", "A.DEGREENO");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }

        ddlSemester.Items.Clear();
        SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
        DataSet dssem = objsql.ExecuteDataSet("SELECT SEMESTERNO,SEMESTERNAME from ACD_SEMESTER where SEMESTERNO > 0 order by SEMESTERNO");

        ddlSemester.DataValueField = "SEMESTERNO";
        ddlSemester.DataTextField = "SEMESTERNAME";
        ddlSemester.DataSource = dssem.Tables[0];
        ddlSemester.DataBind();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Feedback_Activity.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Feedback_Activity.aspx");
        }
    }


    private void LoadDefinedSessionActivities()
    {
        ViewState["DYNAMIC_DATASET"] = null;
        try
        {
            SessionActivityController activityController = new SessionActivityController();
            DataSet ds = activityController.GetDefinedFeedbackActivities();
            if (ds != null && ds.Tables.Count > 0)
            {
                lvFeedbackType.DataSource = ds;
                lvFeedbackType.DataBind();
                ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_SessionActivityDefinition.LoadDefinedSessionActivities() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        ddlFeedbackType.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        rdoNo.Checked = false;
        rdoYes.Checked = true;
        rdoStart.Checked = false;
        rdoStop.Checked = true;
        ddlClgname.SelectedIndex = 0;
        ViewState["sessionactivityno"] = "0";
        ViewState["Edit"] = null;
        populatedropdown();
        ddlBranch.Items.Clear();
        txtStartTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;
    }
    // #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SessionActivityController sa_Controller = new SessionActivityController();
            SessionActivity sessionActivity = new SessionActivity();
            sessionActivity = this.BindData();
            int ActivityCount = 0;
            // int DATETIME_STATUS = Convert.ToInt32(rbddatetime.SelectedValue);
            int DATETIME_STATUS = 0;


            if (ViewState["sessionactivityno"].ToString() != string.Empty && ViewState["sessionactivityno"].ToString() == "0")
            {
                string degreenos = GetDegree();
                string branchnos = GetBranch();
                string semesternos = GetSemester();
                CustomStatus cs = (CustomStatus)sa_Controller.AddFeedbackActivity(sessionActivity, degreenos, branchnos, semesternos, DATETIME_STATUS);
                objCommon.DisplayMessage(this.updSesActivity, "Sessional Activity Definition Saved Successfully!", this.Page);
                this.LoadDefinedSessionActivities();
                Clear();
            }
            else if (int.Parse(ViewState["sessionactivityno"].ToString()) > 0)
            {
                DataSet dsedit = (DataSet)(ViewState["Edit"]);
                DataRow dr = dsedit.Tables[0].Rows[0];
                ActivityCount = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_ACTIVITY", "COUNT(*)", "ISNULL(SHOW_STATUS,0)=1 AND FEEDBACK_TYPENO=" + Convert.ToInt32(dr["FEEDBACK_TYPENO"]) + " AND SESSION_NO=" + Convert.ToInt32(dr["SESSION_NO"]) + " AND SEMESTERNO=" + Convert.ToInt32(dr["SEMESTERNO"]) + " AND DEGREENO=" + Convert.ToInt32(dr["DEGREENO"]) + " AND BRANCHNO=" + Convert.ToInt32(dr["BRANCHNO"]) + " AND COLLEGE_ID=" + Convert.ToInt32(dr["COLLEGE_ID"]) + " AND SESSION_ACTIVITY_NO<>" + Convert.ToInt32(dr["SESSION_ACTIVITY_NO"])));
                if (ActivityCount > 0)
                {
                    objCommon.DisplayMessage(this.updSesActivity, "Activity is Already Defined for Selected Session and Feedback Type !", this.Page);
                    return;

                }
                sa_Controller.UpdateFeedbackActivity(sessionActivity, DATETIME_STATUS);

                objCommon.DisplayMessage(this.updSesActivity, "Sessional Activity Definition Updated Successfully!", this.Page);
                this.LoadDefinedSessionActivities();
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_SessionActivityDefinition.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private SessionActivity BindData()
    {
        SessionActivity sessionActivity = new SessionActivity();
        try
        {

            sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());

            sessionActivity.College_Id = ((ddlClgname.SelectedIndex > 0) ? int.Parse(ddlClgname.SelectedValue) : 0);

            int SessionId = 0;
            if (ddlSession.SelectedValue.ToString() != string.Empty)
            {
                SessionId = Convert.ToInt32(ddlSession.SelectedValue);
            }
            sessionActivity.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(sessionActivity.College_Id) + " AND SESSIONID=" + SessionId));

            //sessionActivity.SessionNo = ((ddlSession.SelectedIndex > 0) ? int.Parse(ddlSession.SelectedValue) : 0);

            sessionActivity.Feedbacktypeno = ((ddlFeedbackType.SelectedIndex > 0) ? int.Parse(ddlFeedbackType.SelectedValue) : 0);
            sessionActivity.IsStarted = (rdoStart.Checked ? true : false);
            sessionActivity.ShowStatus = (rdoYes.Checked ? 1 : 0);
            //if (rbddatetime.SelectedValue == "1")
            //{
            //   // sessionActivity.End_Time ="00:00 AM";
            //  //  sessionActivity.Start_Time ="00:00 AM";

            //    if (txtStartDate.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage(updSesActivity, "Please Enter Start Date.", this.Page);
            //    }
            //    else
            //    {
            //        sessionActivity.StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());//).ToString("yyyy/MM/dd");
            //    }
            //    if (txtEndDate.Text == string.Empty)
            //    {
            //        objCommon.DisplayMessage(updSesActivity, "Please Enter End Date.", this.Page);
            //    }
            //    else
            //    {
            //        sessionActivity.EndDate = Convert.ToDateTime(txtEndDate.Text.ToString());//.ToString("yyyy/MM/dd");
            //    }
            //}
            //else
            //{
            if (txtStartDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(updSesActivity, "Please Enter Start Date.", this.Page);
            }
            else
            {
                sessionActivity.StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());//).ToString("yyyy/MM/dd");
            }
            if (txtEndDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(updSesActivity, "Please Enter End Date.", this.Page);
            }
            else if ((Convert.ToDateTime(txtStartDate.Text)) > (Convert.ToDateTime(txtEndDate.Text)))
            {
                objCommon.DisplayMessage(updSesActivity, "End Date should be greater than Start Date", this.Page);
                txtEndDate.Text = "";
            }
            else
            {
                sessionActivity.EndDate = Convert.ToDateTime(txtEndDate.Text.ToString());//.ToString("yyyy/MM/dd");
            }
            if (txtStartTime.Text == string.Empty && txtEndTime.Text == string.Empty)
            {
                sessionActivity.Start_Time = "12:00 AM";
                sessionActivity.End_Time = "11:59 PM";
            }
            else
            {
                sessionActivity.Start_Time = txtStartTime.Text;
                sessionActivity.End_Time = txtEndTime.Text;
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            sessionActivity.Ipaddress = ViewState["ipAddress"].ToString();
            sessionActivity.User_no = Convert.ToInt32(Session["userno"]);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_SessionActivityDefinition.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return sessionActivity;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;
            int recordId = int.Parse(btnEditRecord.CommandArgument);
            SessionActivityController sa_controller = new SessionActivityController();
            DataSet ds = sa_controller.GetDefinedFeedbackActivities(recordId);
            ViewState["Edit"] = ds;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                //ddlClgname.SelectedValue = dr["COLLEGE_ID"].ToString();


                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlClgname.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

                //ddlSession.SelectedValue = dr["SESSION_NO"].ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONID DESC"); // ADDED ORDER BY DESC CLAUSE BY Nehal N. ON DATED 30.06.2023
                string sessionno = dr["SESSION_NO"].ToString();
                string sessionid = objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON (S.SESSIONID = SM.SESSIONID)", "S.SESSIONID", "ISNULL(S.IS_ACTIVE,0)=1 AND SM.SESSIONNO=" + Convert.ToInt32(sessionno));

                ddlSession.SelectedValue = sessionid;

                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER C INNER JOIN ACD_SESSION_MASTER SM ON (C.COLLEGE_ID = SM.COLLEGE_ID)", "C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND SM.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "C.COLLEGE_ID");
                ddlClgname.SelectedValue = dr["COLLEGE_ID"].ToString();

                ddlDegree.Items.Clear();
                DataSet dsdegree = objCommon.FillDropDown("ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 ", "A.DEGREENO");

                for (int i = 0; i < dsdegree.Tables[0].Rows.Count; i++)
                {
                    ddlDegree.Items.Add(new ListItem(Convert.ToString(dsdegree.Tables[0].Rows[i][1]), Convert.ToString(dsdegree.Tables[0].Rows[i][0])));
                }
                foreach (ListItem item in ddlDegree.Items)
                {
                    if (item.Value == dr["DEGREENO"].ToString())
                        item.Selected = true;
                }
                ddlBranch.Items.Clear();
                DataSet dsbranch = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "B.BRANCHNO", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "B.DEGREENO=" + Convert.ToInt32(dr["DEGREENO"]), "B.DEGREENO,B.BRANCHNO");

                for (int i = 0; i < dsbranch.Tables[0].Rows.Count; i++)
                {
                    ddlBranch.Items.Add(new ListItem(Convert.ToString(dsbranch.Tables[0].Rows[i][1]), Convert.ToString(dsbranch.Tables[0].Rows[i][0])));
                }
                foreach (ListItem item in ddlBranch.Items)
                {
                    if (item.Value == dr["BRANCHNO"].ToString())
                        item.Selected = true;
                }
                SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);

                DataSet dssem = objsql.ExecuteDataSet("SELECT SEMESTERNO,SEMESTERNAME from ACD_SEMESTER where SEMESTERNO > 0 order by SEMESTERNO");
                ddlSemester.Items.Clear();
                // ListBox lstbxSections = e.Item.FindControl("ddlSemester") as ListBox;
                ddlSemester.DataValueField = "SEMESTERNO";
                ddlSemester.DataTextField = "SEMESTERNAME";
                ddlSemester.DataSource = dssem.Tables[0];
                ddlSemester.DataBind();
                foreach (ListItem item in ddlSemester.Items)
                {
                    if (item.Value == dr["SEMESTERNO"].ToString())
                        item.Selected = true;
                }
                //ddlsemester.SelectedValue = dr["SEMESTERNO"].ToString();
                ddlFeedbackType.SelectedValue = dr["FEEDBACK_TYPENO"].ToString();
                //if (dr["DATETIME_STATUS"].ToString().Equals("1"))
                //{
                //    rbddatetime.SelectedValue = "1";
                //    PnlDate.Visible = true;
                //}
                //else if (dr["DATETIME_STATUS"].ToString().Equals("2"))
                //{
                //   rbddatetime.SelectedValue = "2";
                txtStartTime.Text = ds.Tables[0].Rows[0]["START_TIME"].ToString();
                txtEndTime.Text = ds.Tables[0].Rows[0]["END_TIME"].ToString();
                //  }

                txtStartDate.Text = ((dr["START_DATE"].ToString() != string.Empty) ? ((DateTime)dr["START_DATE"]).ToShortDateString() : string.Empty);
                txtEndDate.Text = ((dr["END_DATE"].ToString() != string.Empty) ? ((DateTime)dr["END_DATE"]).ToShortDateString() : string.Empty);

                if (dr["SHOW_STATUS"].ToString().Equals("1"))
                {
                    rdoYes.Checked = true;
                    rdoNo.Checked = false;
                }
                else
                {
                    rdoYes.Checked = false;
                    rdoNo.Checked = true;
                }

                if (dr["STARTED"].ToString().ToLower().Equals("true"))
                {
                    rdoStart.Checked = true;
                    rdoStop.Checked = false;
                }
                else
                {
                    rdoStart.Checked = false;
                    rdoStop.Checked = true;
                }

                ViewState["sessionactivityno"] = dr["SESSION_ACTIVITY_NO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_SessionActivityDefinition.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }



    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            ////ViewState["degreeno"]

            //if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            //{
            //    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

            //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //    ddlSession.Focus();
            //}
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlClgname.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //ddlSession.Focus();
            ddlDegree.Items.Clear();
            DataSet ds = objCommon.FillDropDown("ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND COLLEGE_ID= " + Convert.ToInt32(ddlClgname.SelectedValue), "A.DEGREENO");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlDegree.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
            ddlDegree.Focus();
        }
        else
        {

            ddlClgname.Focus();
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string deg = GetDegree();
        deg = deg.Replace('$', ',');
        if (deg == "")
        {
            deg = "0";
        }
        else
        {
            ViewState["DegreeNo"] = deg;
        }
        string[] DegreeNo = deg.Split(',');

        DataSet ds = objCommon.FillDropDown("ACD_BRANCH a INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON a.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE DE ON (DE.DEGREENO = B.DEGREENO)", "B.BRANCHNO", "DE.DEGREENAME+'-'+A.LONGNAME AS LONGNAME", "B.DEGREENO in(" + deg + ") AND COLLEGE_ID=" + Convert.ToInt32(ddlClgname.SelectedValue), "B.DEGREENO,B.BRANCHNO");
        ddlBranch.Items.Clear();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
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
        if (degreeNo != "")
        {
            degreeno = degreeNo.Substring(0, degreeNo.Length - 1);
        }


        if (degreeno != "")
        {
            string[] degValue = degreeno.Split('$');
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return degreeno;

    }
    private string GetSemester()
    {
        string SemesterNo = "";
        string semesterno = string.Empty;
        //  degreeNo = hdndegreeno.Value;
        //pnlFeeTable.Update();
        foreach (ListItem item in ddlSemester.Items)
        {
            if (item.Selected == true)
            {
                SemesterNo += item.Value + '$';
            }
        }

        semesterno = SemesterNo.Substring(0, SemesterNo.Length - 1);
        if (semesterno != "")
        {
            string[] degValue = semesterno.Split('$');
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return semesterno;

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
        return branchno;

    }
    protected void rbddatetime_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbddatetime.SelectedValue == "1")
            {
                PnlDate.Visible = true;
                PnlDateTime.Visible = false;
            }
            else if (rbddatetime.SelectedValue == "2")
            {
                PnlDate.Visible = true;
                PnlDateTime.Visible = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentDocumentList.rbddatetime_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER C INNER JOIN ACD_SESSION_MASTER SM ON (C.COLLEGE_ID = SM.COLLEGE_ID)", "C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND SM.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "C.COLLEGE_ID");
            ddlClgname.Focus();
            ddlDegree.Items.Clear();
        }
        else
        {

            ddlSession.Focus();
        }

    }
    protected void lvFeedbackType_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (lvFeedbackType.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DYNAMIC_DATASET"];
            lvFeedbackType.DataSource = dt;
            lvFeedbackType.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void FilterData2_TextChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox searchTextBox = (System.Web.UI.WebControls.TextBox)lvFeedbackType.FindControl("FilterData2");
        string searchText = searchTextBox.Text.Trim();

        try
        {
            System.Data.DataTable dt = ViewState["DYNAMIC_DATASET"] as System.Data.DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                if (searchText != string.Empty)
                {
                    string searchedData = "FEEDBACK_NAME LIKE '%" + searchText + "%' OR SESSION_NAME LIKE '%" + searchText + "%' OR START_DATE LIKE '%" + searchText + "%' OR END_DATE LIKE '%" + searchText + "%' OR START_TIME LIKE '%" + searchText + "%' OR END_TIME LIKE '%" + searchText + "%' OR STATUS LIKE '%" + searchText + "%'  OR SHOWSTATUS LIKE '%" + searchText + "%'  OR COLLEGE_NAME LIKE '%" + searchText + "%' OR DEGREENAME LIKE '%" + searchText + "%' OR BRANCH LIKE '%" + searchText + "%' OR SEMESTERNAME LIKE '%" + searchText + "%'";
                    dv.RowFilter = searchedData;
                    if (dv != null && dv.ToTable().Rows.Count > 0)
                    {
                        lvFeedbackType.DataSource = dv;
                        lvFeedbackType.DataBind();
                    }

                }
                else
                {
                    lvFeedbackType.DataSource = dt;
                    lvFeedbackType.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
        }

    }
}
