//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PROSPECTUS CANCELLATION
// CREATION DATE : 11-NOV-2011
// CREATED BY    : ASHISH DHAKATE
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_ProspectusCancellation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDown();
                   
                }
                
            }
            divMsg.InnerHtml = string.Empty;
            //else
            //{
            //    // Clear message div
            //    divMsg.InnerHtml = string.Empty;

            //    /// Check if postback is caused by reprint receipt or cancel receipt buttons
            //    /// if yes then call corresponding methods
            //    if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            //    {
            //        //if (Request.Params["__EVENTTARGET"].ToString() == "ShowClearance")
            //        //    this.ShowClearance(Request.Params["__EVENTARGUMENT"].ToString());
            //        //else 
            //        if (Request.Params["__EVENTTARGET"].ToString() == "CancelAdmission")
            //            this.CancelAdmission();
            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ProspectusCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string searchBy = string.Empty;
            string searchText = txtSearchText.Text.Trim();
            string errorMsg = string.Empty;

            if (rdoStudName.Checked)
            {
                searchBy = "name";
                errorMsg = "having name: " + txtSearchText.Text.Trim();
                txtSearchText.Text = string.Empty;
                ShowStudents(searchBy, searchText, errorMsg);
                updReapeter.Visible = true;
            }
            else if (rdoRecNo.Checked)
            {
                searchBy = "receipt no";
                errorMsg = "having receipt no.: " + searchText;
                txtSearchText.Text = string.Empty;
                ShowStudents(searchBy, searchText, errorMsg);
                updReapeter.Visible = true;
            }
            else if (rdoDate.Checked)
            {
                 //searchBy = "date";
                //errorMsg = "having date: " + searchText;
                //txtSearchText.Text = string.Empty;
                
                //check the from date always be less than to date
                string[] fromDate = txtFromDate.Text.Split('/');
                string[] tooDate = txtToDate.Text.Split('/');
                DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
                DateTime toodate = Convert.ToDateTime(Convert.ToInt32(tooDate[0]) + "/" + Convert.ToInt32(tooDate[1]) + "/" + Convert.ToInt32(tooDate[2]));
                if (fromdate > toodate)
                {
                    objCommon.DisplayMessage("From Date always be less than To date. Please Enter proper Date range.", this.Page);
                    txtFromDate.Text = string.Empty;
                    txtToDate.Text = string.Empty;
                }
                else
                {
                    string frmdate = Convert.ToDateTime(txtFromDate.Text).ToString();
                    string todate = Convert.ToDateTime(txtToDate.Text).ToString();
                    showstudentDatewise(frmdate, todate);
                    updReapeter.Visible = true;
                }
            }
            //ShowStudents(searchBy, searchText, errorMsg);
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowStudents(string searchBy, string searchText, string errorMsg)
    {
        DataSet ds = admCanController.ProspectusSearchStudents(searchText, searchBy);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvSearchResults.DataSource = ds;
            lvSearchResults.DataBind();
            lvSearchResults.Visible = true;
           //ViewState["StudentId"] = ds.Tables[0].Rows[0]["PROSNO"].ToString();
            //btnShowClearance.Visible = true;

            //Get Course Details
            ////foreach (ListViewDataItem dataitemCourse in lvSearchResults.Items)
            ////{
            ////    if (((dataitemCourse.FindControl("btnCancel")) as RadioButton).Checked == true)
            ////    {
            ////        RadioButton RB = (dataitemCourse.FindControl("btnCancel")) as RadioButton;
            ////        ViewState["StudentId"] = RB;
            ////    }
            ////}
        }
        else
        {
            ShowMessage("No student found " + errorMsg);
            lvSearchResults.Visible = false;
           
        }
    }

   

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    //private void CancelAdmission()
    //{
    //    try
    //    {
    //        int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
    //       // string remark = "This student's admission has been cancelled by " + Session["userfullname"].ToString();
    //        //remark += " on " + DateTime.Now + ". " + txtRemark.Text.Trim();

    //        if (admCanController.CancelProspectus(studId))
    //        {
    //            ShowMessage("Admission cancelled successfully.");
    //        }
    //        else
    //            ShowMessage("Unable to cancel the student\\'s admission.");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_AdmissionCancellation.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    public string GetStatus(object status)
    {
        if(Convert.ToInt32(status)==0)
       
            return "<span style='color:Green'>Active</span>";
        else
            return "<span style='color:Red'>Cancel</span>";

     }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int stuid = int.Parse(btnDel.ToolTip);
            //AdmissionCancellationController admCanController = new AdmissionCancellationController();
            if(admCanController.CancelProspectus(stuid))
            {
                
                Response.Redirect(Request.Url.ToString());
                ShowMessage("Admission cancelled successfully.");
            }
            else
             ShowMessage("Unable to cancel the student\\'s admission.");
         
           // NewsController objNC = new NewsController();
           //CustomStatus cs = (CustomStatus)objNC.Delete(newsid);
            //if (cs.Equals(CustomStatus.RecordDeleted))
            //    Response.Redirect(Request.Url.ToString());

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ProspectusCancellation_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdoDate_CheckedChanged(object sender, EventArgs e)
    {
        //lblDateFormat.Visible = true;
        trDate.Visible = true;
        trText.Visible = false;
    }

    protected void rdoStudName_CheckedChanged(object sender, EventArgs e)
    {
        //lblDateFormat.Visible = false;
        trDate.Visible = false;
        trText.Visible = true;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }

    protected void rdoRecNo_CheckedChanged(object sender, EventArgs e)
    {
        //lblDateFormat.Visible = false;
        trDate.Visible = false;
        trText.Visible = true;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
   
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");

            //COADING FOR AUTOGENERATE THE RECEIPT NO
            //string STR1 = objCommon.LookUp("ACD_PROSPECTUS", "MAX(REC_NO)", " REC_NO IS NOT NULL");
            //string STR2 = (Convert.ToInt32(STR1.Substring(5, 2)) + Convert.ToInt32(1)).ToString();
            //string STR3 = (STR1.Substring(0, 5) + STR2).ToString();
            //txtReciptNo.Text = STR3;
            ddlBranch.Focus();

        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ProspectusCancellation.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

            
        }
    }

    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ProspectusCancel", "rptProspectusCancelStud.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue+",@P_BRANCHNO="+ddlBranch.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updSearchStudent, this.updSearchStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        txtSearchText.Text = string.Empty;
        lvSearchResults.Visible = false;
        Response.Redirect(Request.Url.ToString());
    }

    private void showstudentDatewise(string frmdate,string todate)
    {
        DataSet ds = admCanController.ProspectusSearchStudentsDatewise(frmdate, todate);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvSearchResults.DataSource = ds;
            lvSearchResults.DataBind();
            lvSearchResults.Visible = true;
            //ViewState["StudentId"] = ds.Tables[0].Rows[0]["PROSNO"].ToString();
            //btnShowClearance.Visible = true;

            //Get Course Details
            ////foreach (ListViewDataItem dataitemCourse in lvSearchResults.Items)
            ////{
            ////    if (((dataitemCourse.FindControl("btnCancel")) as RadioButton).Checked == true)
            ////    {
            ////        RadioButton RB = (dataitemCourse.FindControl("btnCancel")) as RadioButton;
            ////        ViewState["StudentId"] = RB;
            ////    }
            ////}
        }
        else
        {
            ShowMessage("No student found ");
            lvSearchResults.Visible = false;

        }
    }

}
