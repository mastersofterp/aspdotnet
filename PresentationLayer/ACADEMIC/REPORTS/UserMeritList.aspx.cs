//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Applicant data verify
// CREATION DATE : 19-MAY-2014
// CREATED BY    : RENUKA ADULKAR
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class Academic_UserMeritList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
   
    DailyFeeCollectionController objdfcc = new DailyFeeCollectionController();

    #region page

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
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                
                objCommon.FillDropDownList(ddlSession, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                ddlSession.Items.RemoveAt(0);
                objCommon.FillDropDownList(ddlApplicantType, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO DESC");
            }
            divMsg.InnerHtml = string.Empty;
        }
    }

    #endregion

    #region User Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page          

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UserMeritList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UserMeritList.aspx");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlApplicantType.SelectedValue + ",@P_QUALIFYNO=" + ddlAdmcat.SelectedValue + ",@P_CUTOFFMARKS=" + Convert.ToInt32(txtCutOff.Text.Trim()) + ",@P_APPDATE=" + txtAppDate.Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updLists, this.updLists.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentReconsilReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    #region button

    protected void btnexport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet ds = null;
            DataSet dsfee = objdfcc.GetMeritList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlApplicantType.SelectedValue), Convert.ToInt32(ddlAdmcat.SelectedValue), Convert.ToInt32(txtCutOff.Text.Trim()), txtAppDate.Text);

            if (dsfee.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsfee;
                GV.DataBind();
                string attachment = "attachment; filename=MeritList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_UserMeritList.btnexport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue == string.Empty)
        {
            objCommon.DisplayMessage("Please Select Admission Batch", this.Page);
            return;

        }
        else if (ddlApplicantType.SelectedValue == string.Empty)
        {
            objCommon.DisplayMessage("Please Select Degree", this.Page);
            return;
        }
        else if (ddlAdmcat.SelectedValue == string.Empty)
        {
            objCommon.DisplayMessage("Please Select Exam Type", this.Page);
            return;
        }
        else if (txtCutOff.Text == string.Empty)
        {
            objCommon.DisplayMessage("Please Enter Cut Off", this.Page);
            return;
        }
        else if (txtAppDate.Text == string.Empty)
        {
            objCommon.DisplayMessage("Please Enter Application Date", this.Page);
            return;
        }
        else
        {
            ShowReport("Merit List", "rptUserJEEMeritlist.rpt");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlApplicantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlApplicantType.SelectedIndex > 0)
        {
           
            objCommon.FillDropDownList(ddlAdmcat, "ACD_ENTRE_DEGREE ED INNER JOIN ACD_QUALEXM Q ON(ED.QUALIFYNO=Q.QUALIFYNO)", "ED.QUALIFYNO", "Q.QUALIEXMNAME", "ED.DEGREENO=" + ddlApplicantType.SelectedValue + " AND QEXAMSTATUS='E'", "QUALIFYNO DESC");
        }
        else
        {
            ddlAdmcat.Items.Clear();
            ddlAdmcat.Items.Insert(0, "Please Select");
        }
    }

    #endregion

   


    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        DataSet ds = null;
        ds = objSC.GetStudentformeritList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlApplicantType.SelectedValue), Convert.ToInt32(ddlAdmcat.SelectedValue), Convert.ToInt32(txtCutOff.Text.Trim()), txtAppDate.Text);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvMeritList.DataSource = ds;
            lvMeritList.DataBind();


        }
    }
}
