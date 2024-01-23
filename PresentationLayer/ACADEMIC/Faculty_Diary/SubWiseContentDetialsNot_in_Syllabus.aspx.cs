//=================================================================================
// PROJECT NAME  : RFC Common Code                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : SubWiseContentDetialsNot_in_Syllabus.aspx                                            
// CREATION DATE : 01/01/2024                                                
// CREATED BY    : Vipul Tichakule                             
// MODIFIED BY   : 
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
                if (Session["usertype"].ToString() == "3")
                {
                    if (Session["usertype"].ToString() != "1")
                    {
                        objCommon.FillDropDownList(ddlClgname, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT SC.COSCHNO", "SC.COL_SCHEME_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "SC.COSCHNO");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                    }

                 
                }

               
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
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
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
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));     
            ddlClgname.Focus();
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourseName, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (CT.COURSENO=C.COURSENO)", "CT.COURSENO", "C.COURSE_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "CT.COURSENO");
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string result = string.Empty;

        string topicName = txtTopicName.Text;
        string content = txtContentOfTopic.Text;
        string mappPEO = txtMappingLevel.Text;
        string date = txtDate.Text;
        string schemeno = ddlClgname.SelectedValue;
        string courseno = ddlCourseName.SelectedValue;
        string sessionno = ddlSession.SelectedValue;
        string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
        string orgid = objCommon.LookUp("Reff", "organizationid", "organizationid >0");

        result = objTeachingPlanController.InsertSubWiseNotinContentCourse(topicName, content, mappPEO, date, schemeno, courseno, sessionno, clgcode, orgid);
        if (result == "1")
        {
            objCommon.DisplayMessage("Record insert succesfully", this.Page);
            ClearControl();
        
        }
        else
        {
            objCommon.DisplayMessage("Record not inserted", this.Page);

        }
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        ShowReport("Record_Of_Content_Beyond_Syllabus", "SubWiseContentNotin_Syllabus.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + clgcode; // ViewState["college_id"].ToString();
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
            throw;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    protected void ClearControl()
    {
        ddlClgname.SelectedIndex = 0;
        ddlCourseName.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        txtContentOfTopic.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtMappingLevel.Text = string.Empty;
        txtTopicName.Text = string.Empty;
    }

  
}