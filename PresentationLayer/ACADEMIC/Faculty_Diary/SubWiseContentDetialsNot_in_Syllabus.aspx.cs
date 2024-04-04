//=================================================================================
// PROJECT NAME  : RFC Common Code                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : SubWiseContentDetialsNot_in_Syllabus.aspx                                            
// CREATION DATE : 01/01/2024                                                
// CREATED BY    : Vipul Tichakule                             
// MODIFIED BY   : GUNESH MOHANE
// MODIFIED DATE : 30/03/2024 
// MODIFIED DESC : 
//=================================================================================

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_SubWiseContentDetialsNot_in_Syllabus : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();
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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
             
                ViewState["batch"] = null;
                PopulateCollege();

               
            }

            ViewState["college_id"] = null;
            string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
            ViewState["college_id"] = clgcode;
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
        }
    }
#endregion

    #region Dropdown Events
    protected void PopulateCollege() 
    {
        try
        {
            if (Session["usertype"].ToString() == "3")
            {
                if (Session["usertype"].ToString() != "1")
                {
                    //objCommon.FillDropDownList(ddlClgname, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT SC.COSCHNO", "SC.COL_SCHEME_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "SC.COSCHNO");
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.PopulateCollege-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PoupulateSession()
    {
        try 
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SESSION_MASTER C ON (CT.SESSIONNO=C.SESSIONNO)", "DISTINCT C.SESSIONNO", "C.SESSION_NAME", "C.SESSIONNO > 0 AND ISNULL(C.IS_ACTIVE,0)=1 AND C.COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND C.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + "AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "C.SESSIONNO DESC");
                    ddlSession.Focus();
                    bindList(0, Convert.ToInt32(ddlClgname.SelectedValue), 0);
                }
            }
            else
            {
                lvSubWiseContent.DataSource = null;
                lvSubWiseContent.DataBind();
                Panel1.Visible = false;
                
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                ddlCourseName.Items.Clear();
                ddlCourseName.Items.Add(new ListItem("Please Select", "0"));
                ddlClgname.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.PoupulateSession-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateCourse() 
    {
        try 
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourseName, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (CT.COURSENO=C.COURSENO)", "DISTINCT CT.COURSENO", "C.COURSE_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")" + "AND CT.SESSIONNO=" + ddlSession.SelectedValue, "CT.COURSENO");
                
            }
            bindList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), 0);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.PopulateCourse-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            PoupulateSession();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.ddlClgname_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopulateCourse();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            bindList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue), Convert.ToInt32(ddlCourseName.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.ddlCourseName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Button Events
    protected void bindList(int sessionno, int schemeno, int courseno) 
    {
        Panel1.Visible = true;
        string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
        string uano = Session["userno"].ToString();
        try
        {
            DataSet ds = objTeachingPlanController.BindSubWiseNotinContentCourse(clgcode,uano,sessionno,schemeno,courseno);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvSubWiseContent.DataSource = ds;
                lvSubWiseContent.DataBind();
                Panel1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.bindList-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try 
        {
            string result = string.Empty;
            int id = 0;
            if (hfid.Value != string.Empty)
            {
                id = Convert.ToInt32(hfid.Value);
            }
            string topicName = txtTopicName.Text;
            string content = txtContentOfTopic.Text;
            string mappPEO = txtMappingLevel.Text;
            string date = txtDate.Text;
            string schemeno = ddlClgname.SelectedValue;
            string courseno = ddlCourseName.SelectedValue;
            string sessionno = ddlSession.SelectedValue;
            string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
            string orgid = objCommon.LookUp("Reff", "organizationid", "organizationid >0");
            string uano = Session["userno"].ToString();

            if (hfid.Value == string.Empty)
            {
                result = objTeachingPlanController.InsertSubWiseNotinContentCourse(topicName, content, mappPEO, date, schemeno, courseno, sessionno, clgcode, orgid, uano);
                if (result == "1")
                {
                    objCommon.DisplayMessage("Record Insert Succesfully", this.Page);
                    txtContentOfTopic.Text = string.Empty;
                    txtDate.Text = string.Empty;
                    txtMappingLevel.Text = string.Empty;
                    txtTopicName.Text = string.Empty;
                    ViewState["edit"] = null;

                }
                else
                    objCommon.DisplayMessage("Record Not Inserted", this.Page);
            }
            else
            {
                result = objTeachingPlanController.UpdateSubWiseNotinContentCourse(id, topicName, content, mappPEO, date, schemeno, courseno, sessionno, clgcode, orgid, uano);
                if (result == "2")
                {
                    objCommon.DisplayMessage("Record Updated Succesfully", this.Page);
                    hfid.Value = string.Empty;
                    txtContentOfTopic.Text = string.Empty;
                    txtDate.Text = string.Empty;
                    txtMappingLevel.Text = string.Empty;
                    txtTopicName.Text = string.Empty;
                    ViewState["edit"] = null;
                }
                else
                    objCommon.DisplayMessage("Record Not Updated", this.Page);
            }
            bindList(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(courseno));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        try 
        {
            if (ddlClgname.SelectedIndex != 0)
                ShowReport("Record_Of_Content_Beyond_Syllabus", "SubWiseContentNotin_Syllabus.rpt");
            else
                objCommon.DisplayMessage("Please Select College & Scheme", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.btnReport_Click1-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
        string uano = Session["userno"].ToString();
        int schemeno = Convert.ToInt32(ddlClgname.SelectedValue);
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int courseno = Convert.ToInt32(ddlCourseName.SelectedValue);

        DataSet ds = objTeachingPlanController.CheckDatainReportSubjectWiseContent(Convert.ToInt32(clgcode),Convert.ToInt32(uano),schemeno,sessionno,courseno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_COLLEGE_CODE=" + clgcode; // ViewState["college_id"].ToString();
                url += "&param=@P_COLLEGE_CODE=" + clgcode + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_SCHEMENO=" + ddlClgname.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + ddlCourseName.SelectedValue; // ViewState["college_id"].ToString();
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);
            }

            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.ShowReport-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        else
        {
            objCommon.DisplayMessage("Record not found", this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    protected void ClearControl()
    {
        hfid.Value = string.Empty;
        ddlClgname.SelectedIndex = 0;
        ddlCourseName.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        txtContentOfTopic.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtMappingLevel.Text = string.Empty;
        txtTopicName.Text = string.Empty;
        ViewState["edit"] = null;
        Panel1.Visible = false;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try 
        {
            ImageButton btnEditRecord = sender as ImageButton;
            int id = int.Parse(btnEditRecord.CommandArgument);
            ViewState["edit"] = "edit";
            DataSet ds = objTeachingPlanController.GetSubWiseNotinContentCourseByID(id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PopulateCollege();
                ddlClgname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SCHEMENO"]);
                PoupulateSession();
                ddlSession.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SESSIONNO"]);
                PopulateCourse();
                ddlCourseName.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["COURSENO"]);
                txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"]).ToString("dd/MM/yyyy");
                txtTopicName.Text = Convert.ToString(ds.Tables[0].Rows[0]["TOPIC_NAME"]);
                txtContentOfTopic.Text = Convert.ToString(ds.Tables[0].Rows[0]["CONTENT"]);
                txtMappingLevel.Text = Convert.ToString(ds.Tables[0].Rows[0]["MAPP_PEO"]);
                hfid.Value = id.ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        try 
        {
            ImageButton btnRemoveRecord = sender as ImageButton;
            int id = int.Parse(btnRemoveRecord.CommandArgument);
            string result = objTeachingPlanController.DeleteSubWiseNotinContentCourse(id);

            string schemeno = ddlClgname.SelectedValue;
            string courseno = ddlCourseName.SelectedValue;
            string sessionno = ddlSession.SelectedValue;
            bindList(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(courseno));
            if (result == "3")
            {
                objCommon.DisplayMessage(this, "Record Deleted Succesfully", this.Page);
            }
            else
                objCommon.DisplayMessage(this, "Record Not Deleted", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_SubWiseContentDetialsNot_in_Syllabus.btnRemove_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    #endregion

}