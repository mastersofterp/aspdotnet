//=================================================================================
// PROJECT NAME  : U-AIMS [RAIPUR]                                                         
// MODULE NAME   : ACADEMIC - MARK ENTRY FS                                          
// CREATION DATE : 11 MAY 2013                                                 
// CREATED BY    : YAKIN UTANE                                              
// MODIFIED BY   :                                                     
// MODIFIED DESC : 
//=================================================================================
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
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


public partial class ACADEMIC_MarkEntryFs : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int cs =0,a = 0;
    decimal s1mark=0;
    MarksEntryController objMarksEntry = new MarksEntryController();

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
        if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
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
                CheckPageAuthorization();
                
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                {
                    if (ViewState["action"] == null)
                    {
                        pnlStudGrid.Visible = false;
                    }
                    else if (ViewState["action"].ToString().Equals("markentry"))
                    {
                        pnlStudGrid.Visible = true;
                    }
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP 2 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
                    ddlSession.SelectedIndex = 1;
                    objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT R,dbo.FS_STUDENT_61 FS", "DISTINCT  R.COURSENO", "R.COURSENAME", "R.IDNO=FS.IDNO AND R.COURSENO=FS.COURSENO AND R.SESSIONNO=" + Convert.ToInt32(Session["currentsession"].ToString()) + " AND R.EXAM_REGISTERED=1 AND (R.DETAIND=0 OR R.DETAIND IS NULL) AND (R.CANCEL=0 OR R.CANCEL IS NULL)", string.Empty);
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    objCommon.DisplayMessage("You are not authorized to view this page!!", this.Page);
                    ddlSession.Items.Clear();
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            Label lblid = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
            int id = Convert.ToInt32(lblid.ToolTip.ToString());
            int session = Convert.ToInt32(ddlSession.SelectedValue);
            int course  = Convert.ToInt32(ddlCourse.SelectedValue);
            TextBox txtmark = gvStudent.Rows[i].FindControl("txtFSMarks") as TextBox;
            if (txtmark.Text != "")
            {
                s1mark = Convert.ToDecimal(txtmark.Text);
                cs = objMarksEntry.UpdateFsMarkEntry(session, course, id, s1mark);
                a++;
            }
        }
        if (a > 0)
        {
            objCommon.DisplayMessage("Mark Entry Done Successfully !!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage("Mark Entry Not Done!!", this.Page);
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT R,DBO.FS_STUDENT_61 FS", "DISTINCT  R.COURSENO", "R.COURSENAME", "R.IDNO=FS.IDNO AND R.COURSENO=FS.COURSENO AND R.SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND R.EXAM_REGISTERED=1 AND (R.DETAIND=0 OR R.DETAIND IS NULL) AND (R.CANCEL=0 OR R.CANCEL IS NULL)", string.Empty);
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            Label lblid = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
            int id = Convert.ToInt32(lblid.ToolTip.ToString());
            int session = Convert.ToInt32(ddlSession.SelectedValue);
            int course = Convert.ToInt32(ddlCourse.SelectedValue);
            TextBox txtmark = gvStudent.Rows[i].FindControl("txtFSMarks") as TextBox;
            if (txtmark.Text != "")
            {
                cs = objMarksEntry.UpdateLockMarkEntryFs(session, course, id);
                a++;
            }
        }
        if (a > 0)
        {
            objCommon.DisplayMessage("Mark Entry is Lock Successfully !!", this.Page);
            btnSave.Enabled = false;
            btnLock.Enabled = false;
        }
        else
        {
            objCommon.DisplayMessage("Mark Entry Lock Not Done!!", this.Page);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {  
        pnlStudGrid.Visible = true;
        int subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
        if (subid == 1)
        {
            DataSet DS = objCommon.FillDropDown("ACD_STUDENT_RESULT R,DBO.FS_STUDENT_61 FS,ACD_STUDENT B,ACD_COURSE C", "B.IDNO,R.ROLL_NO", "B.STUDNAME,DBO.FN_DESC('SEMESTER',R.SEMESTERNO)AS SEMESTER,ISNULL(C.S1MAX,0)S1MAX,R.S1MARK,ISNULL(C.S1MIN,0)S1MIN,R.LOCKS1", "R.IDNO=FS.IDNO AND R.COURSENO=FS.COURSENO AND R.IDNO=B.IDNO AND R.COURSENO=C.COURSENO AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND R.EXAM_REGISTERED=1 AND (R.DETAIND=0 OR R.DETAIND IS NULL) AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND R.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "R.ROLL_NO");
            if (DS.Tables[0].Rows.Count > 0)
            {
                gvStudent.Columns[3].HeaderText = "TH" + "[Max : " + DS.Tables[0].Rows[0]["S1MAX"].ToString() + "]" + " <br>" + "[Min : " + DS.Tables[0].Rows[0]["S1MIN"].ToString() + "]";
                gvStudent.DataSource = DS;
                gvStudent.DataBind();
                btnSave.Enabled = true;
                btnLock.Enabled = true;
                this.BindJS();
                int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND LOCKS1=1"));
                if (count > 0)
                {
                    for (int i = 0; i < gvStudent.Rows.Count; i++)
                    {
                        TextBox txt = (TextBox)gvStudent.Rows[i].FindControl("txtFSMarks");
                        txt.Enabled = false;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnReport.Enabled = true;
                    }
                }
            }
        }
        else
        {
            DataSet DS = objCommon.FillDropDown("ACD_STUDENT_RESULT R,DBO.FS_STUDENT_61 FS,ACD_STUDENT B,ACD_COURSE C", "B.IDNO,R.ROLL_NO", "B.STUDNAME,DBO.FN_DESC('SEMESTER',R.SEMESTERNO)AS SEMESTER,ISNULL(C.S4MAX,0)S1MAX,R.S4MARK AS S1MARK,ISNULL(C.S4MIN,0)S1MIN", "R.IDNO=FS.IDNO AND R.COURSENO=FS.COURSENO AND R.IDNO=B.IDNO AND R.COURSENO=C.COURSENO AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND R.EXAM_REGISTERED=1 AND (R.DETAIND=0 OR R.DETAIND IS NULL) AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND R.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "R.ROLL_NO");
            if (DS.Tables[0].Rows.Count > 0)
            {
                gvStudent.Columns[3].HeaderText = "PR" + "[Max : " + DS.Tables[0].Rows[0]["S1MAX"].ToString() + "]" + " <br>" + "[Min : " + DS.Tables[0].Rows[0]["S1MIN"].ToString() + "]";
                gvStudent.DataSource = DS;
                gvStudent.DataBind();
                btnSave.Enabled = true;
                btnLock.Enabled = true;
                this.BindJS();
                int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND LOCKS4=1"));
                if (count > 0)
                {
                    for (int i = 0; i < gvStudent.Rows.Count; i++)
                    {
                        TextBox txt = (TextBox)gvStudent.Rows[i].FindControl("txtFSMarks");
                        txt.Enabled = false;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnReport.Enabled = true;
                    }
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void BindJS()
    {
        try
        {
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                    TextBox txtTAMarks = gvRow.FindControl("txtFSMarks") as TextBox;
                    Label lblTAMarks = gvRow.FindControl("lblFSMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblFSMinMarks") as Label;

                    if (lblTAMarks.ToolTip.ToUpper().Equals("TRUE")) txtTAMarks.Enabled = false;
                    txtTAMarks.Attributes.Add("onblur", "validateMark(" + txtTAMarks.ClientID + "," + lblTAMarks.Text + "," + lblESMinMarks.Text + ")");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.BindJS --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("MarkEntryForFailedinSessional", "rpMarkEntrySessional.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_ConsolidatedInterMarks.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
}
