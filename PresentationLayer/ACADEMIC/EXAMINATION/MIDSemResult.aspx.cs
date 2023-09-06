using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_EXAMINATION_MIDSemResult : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //added on 24-04-2020 by Vaishali
                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    CheckPageAuthorization();
                    PopulateDropDownList();
                    ddlSession.Focus();
                    divDetails.Visible = true;
                }

                else
                {
                    divDetails.Visible = false;
                    objCommon.DisplayUserMessage(this.Page, "You are not authorized to view this page.", this.Page);
                }

               
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
                Response.Redirect("~/notauthorized.aspx?page=MIDSemResult.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MIDSemResult.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {  
            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO = B.SESSIONNO", "DISTINCT A.SESSIONNO", "SESSION_PNAME", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND A.SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_STUDENT_RESULT AA WHERE AA.IDNO = A.IDNO) AND B.SESSIONNO > 0 ", "A.SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSession, "RESULT_PUBLISH_DATA A INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO = B.SESSIONNO", "DISTINCT A.SESSIONNO", "SESSION_PNAME", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND IS_MIDS_ENDS = 1 AND B.SESSIONNO > 0 ", "A.SESSIONNO DESC"); //modified on 15-06-2020 by Vaishali
            int count = ddlSession.Items.Count;
            if (ddlSession.Items.Count == 1)
                lblNote.Visible = true;
            else
                lblNote.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_MIDSemResult.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex != 0)
        {
            btnPrint.Visible = true;
        }
        else
        {
            btnPrint.Visible = false;
            divStudDetails.Visible = false;
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            spanNote.Visible = false;
            lblNote.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO ="+ Convert.ToInt32(Session["idno"]) +" AND ISNULL(ACCEPTED,0) = 1  AND ISNULL(REGISTERED,0) = 1 AND ISNULL(CANCEL,0) = 0 AND SUBID = 1 AND S3MARK IS NOT NULL AND ISNULL(LOCKS3,0) = 1"));

            if (count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=MIDSemStudentResult";
                url += "&path=~,Reports,Academic,rptStudentMIDSemMarksheet.rpt";

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updUpdate, this.updUpdate.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updUpdate, "Marks are not locked !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MIDSemMarksheet.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}