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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_ANSWERSHEET_CreateBill : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AnswerSheetController objAnsSheetController = new AnswerSheetController();

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
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    this.FillDropdown();
                    btnReport.Enabled = false;
                    btnShow.Enabled = false;
                    btnSubmit.Visible = false;
                    lvStudentsIssuer.DataSource = null;
                    lvStudentsIssuer.DataBind();
                    //AjaxControlToolkit.CalendarExtender CalID = new AjaxControlToolkit.CalendarExtender();
                    //CalID1.StartDate = DateTime.Now;


                }
                divMsg.InnerHtml = string.Empty;
            }

            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

            //this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_RECIEVE_AnswersheetRecieve_Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=AnswersheetIssuer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AnswersheetIssuer.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
           // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ANSWERSHEET_CreateBill.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {
            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
           // objAnsSheet.FacultyType = Convert.ToInt32(ddlEvalutorType.SelectedValue);
            objAnsSheet.FacultyNo = Convert.ToInt32(ddlFaculty.SelectedValue);

            DataSet ds = objAnsSheetController.GetAnswerSheetIssueCourseByEvaluatorWise(objAnsSheet);
            if (ds != null)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //pnlDays.Visible = true;
                    lvStudentsIssuer.DataSource = ds.Tables[0];
                    lvStudentsIssuer.DataBind();
                    ddlSession.Enabled = false;                
                 
                    btnReport.Enabled = true;

                    string count = objCommon.LookUp("ACD_ANS_ISSUE WITH (NOLOCK)", "RECEIVER_DATE", "EXAM_STAFF_NO=" + ddlFaculty.SelectedValue);
                    if (count == "")
                    {
                        btnSubmit.Visible = false;

                    }
                    else
                    {
                       btnSubmit.Visible= true;
                    }
                }               
            }          

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ANSWERSHEET_CreateBill.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
      //  ClearData();
        this.BindListView(); 

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AnswerSheet objAnsSheet = new AnswerSheet();

            if (Convert.ToInt32(ddlEvalutorType.SelectedValue) > 0 && Convert.ToInt32(ddlFaculty.SelectedValue) > 0)
            {
                objAnsSheet.FacultyNo = Convert.ToInt32(ddlFaculty.SelectedValue);
                objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

                foreach(ListViewDataItem lsv in lvStudentsIssuer.Items) 
                {

                    HiddenField hdfCourseno = lsv.FindControl("hdCourseno") as HiddenField;
                    objAnsSheet.CourseNo = Convert.ToInt32(hdfCourseno.Value);

                    HiddenField hdfQuantity = lsv.FindControl("hdQty") as HiddenField;
                    objAnsSheet.Quantity = Convert.ToInt32(hdfQuantity.Value);

                    TextBox txtPerPaperRate = lsv.FindControl("txtAmountEachPaper") as TextBox;
                    objAnsSheet.PerPapeRate = Convert.ToInt32(txtPerPaperRate.Text);

                    TextBox txtTotAmount = lsv.FindControl("txtAmount") as TextBox;
                    objAnsSheet.Amount = Convert.ToInt32(txtTotAmount.Text);

                    CustomStatus cs = (CustomStatus)objAnsSheetController.InsertEvaluatorBill(objAnsSheet);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Server Error...", this.Page);
                    }
                  // ClearData();
                    BindListView();
                  }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ANSWERSHEET_CreateBill.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("pdf", "CreateBill.rpt");      
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlFaculty.SelectedItem.Text  + "-Bill" + ".pdf";
            url += "&path=~,Reports,Academic,Answersheet," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_EVALUATORNO=" + ddlFaculty.SelectedValue ;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";    

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ANSWERSHEET_CreateBill.ShowReport() --> " + ex.Message + " " + ex.StackTrace);              
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
  


    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ClearSelection();
     
    }

    private void ClearSelection()
    {
        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();     
        ddlSession.SelectedIndex = 0;

        ddlSession.Enabled = true;
      
        ddlFaculty.SelectedItem.Text = string.Empty;
        ddlEvalutorType.SelectedIndex = 0;

    }
 
 

    protected void ddlEvalutorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlFaculty, " ACD_EXAM_EVALUATOR E WITH (NOLOCK) INNER JOIN ACD_EXAM_STAFF S WITH (NOLOCK) ON (E.EXAM_STAFF_NO=S.EXAM_STAFF_NO)", " DISTINCT S.EXAM_STAFF_NO", "S.STAFF_NAME", " S.ACTIVE=1 AND STAFF_TYPE=" + ddlEvalutorType.SelectedValue + " AND E.SESSIONNO=" + ddlSession.SelectedValue, "S.EXAM_STAFF_NO");

        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlFaculty.SelectedValue) > 0)
        {
            lvStudentsIssuer.DataSource = null;
            lvStudentsIssuer.DataBind();
            this.BindListView();
            string count = objCommon.LookUp("ACD_ANS_ISSUE WITH (NOLOCK)", "RECEIVER_DATE", "EXAM_STAFF_NO=" + ddlFaculty.SelectedValue);
            if (count == "")
            {
                btnSubmit.Visible = false;
            }
            else
            {
                btnSubmit.Visible = true;
            }
        }
        else
        {
            lvStudentsIssuer.DataSource = null;
            lvStudentsIssuer.DataBind();
        } 

    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex > 0)
        {

            Common objCommon = new Common();

            if (ddlcollege.SelectedIndex > 0)
            {
                //Common objCommon = new Common();
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                }
                ddlSession.SelectedIndex = 0;
                ddlEvalutorType.SelectedIndex = 0;
                ddlFaculty.SelectedIndex = 0;

            }
        }
    }
}