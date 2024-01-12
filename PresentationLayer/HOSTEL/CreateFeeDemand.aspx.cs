//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : CREATE FEE DEMAND
// CREATION DATE : 30-OCT-2009
// CREATED BY    : AMIT YADAV
// MODIFIED BY   : GAURAV SONI
// MODIFIED DATE : 6-DEC-2010
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Hostel_CreateFeeDemand : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    DemandModificationController dmController = new DemandModificationController();
    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                     // START   // COMMENTED BY SONALI ON 12/12/2022 (AS THIS PAGE WILL BE SAME FOR ALL CLIENTS)  

                    //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
                    //{
                        filter.Visible=false;
                    //}
                    //else
                    //{
                    //    filter.Visible=true;
                    //}

                    //END

                    // Load drop down lists
                    PopulateDropdownList();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateFeeDemand.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateFeeDemand.aspx");
        }
    }

    private void PopulateDropdownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO desc");
        objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE = 'HF' OR RECIEPT_CODE = 'MF'", "RECIEPT_CODE");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlForSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }

    #endregion

    #region Create Demand

    protected void btnCreateDemand_Click(object sender, EventArgs e)
    {
        try
        {
            FeeDemand demandCriteria = this.GetDemandCriteria();
            //string response = string.Empty;
            string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, "0", Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));

            //string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, "0", Convert.ToInt32(rdoDemand.SelectedValue), Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));
            //string response = dmController.CreateHostelFeeDemand(demandCriteria,Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked,"0",Convert.ToInt32(rdoDemand.SelectedValue),Convert.ToInt32(ddlForSemester.SelectedValue));

            if (response != "-99")
            {
                if (response.Length > 2)
                    ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                else
                    ShowMessage("Demand successfully created for all students.");
                FillStudent(); //Added By Himanshu Tamrakar for tkt 51949 on date 30122023

                //this.ShowReport(demandCriteria, "Fee_Demand_Report", "FeeDemandReport_Detailed.rpt");
            }
            else
            {
                ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
            }
            //FillStudentList();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandParams = new FeeDemand();
        demandParams.ReceiptTypeCode = ddlReceiptType.SelectedValue;
        demandParams.UserNo = Convert.ToInt32(Session["userno"].ToString());
        demandParams.CollegeCode = Session["colcode"].ToString();
        demandParams.SemesterNo = Convert.ToInt32(ddlsemester.SelectedValue);
        return demandParams;
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    } 
    #endregion

    #region Show Report

    private void ShowReport(FeeDemand demandRpt, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=" + this.GetReportParameters(demandRpt) + (rdoDetailedReport.Checked ? (",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue) : "");
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(FeeDemand demandRpt)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            param.Append("CollegeName=" + Session["coll_name"].ToString());
            param.Append(",UserName=" + Session["userfullname"].ToString());
            param.Append(",@P_RECIEPTCODE=" + demandRpt.ReceiptTypeCode);
            param.Append(",@P_BRANCHNO=" + ViewState["BRANCHNO"].ToString());
            param.Append(",@P_SEMESTERNO=" + ViewState["SEMESTERNO"].ToString());
            param.Append(",@P_PAYMENTTYPE=" + ViewState["PAYMENTTYPE"].ToString());
            param.Append(",ReceiptType=" + ((ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedItem.Text : "0"));
            //param.Append(",Branch=" + ((ddlRoomCapacity.SelectedIndex > 0) ? ddlRoomCapacity.SelectedItem.Text : "0"));
            //param.Append(",Semester=" + ((ddlForSemester.SelectedIndex > 0) ? ddlForSemester.SelectedItem.Text : "0"));
            param.Append(",Branch=" + ViewState["BRANCHNO"].ToString());
            param.Append(",Semester=" + ViewState["SEMESTERNO"].ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.GetReportParameters() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return param.ToString();
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            FeeDemand feeDemand = this.GetDemandCriteria();
            string reportTitle = string.Empty;
            string reportFileName = string.Empty;

            if (rdoDetailedReport.Checked){
                reportTitle = "Detailed_Fee_Demand_Report";
                reportFileName = "FeeDemandReport_Detailed.rpt";
            }
            else{
                reportTitle = "Fee_Demand_Summary_Report";
                reportFileName = "FeeDemandReport_Summery.rpt";
            }
            this.ShowReport(feeDemand, reportTitle, reportFileName);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.btnShowReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    } 
    #endregion

    #region Action
    protected void btnCreateDemandForSelStuds_Click(object sender, EventArgs e)
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            FeeDemand demandCriteria = this.GetDemandCriteria();
            string studentIDs = this.GetSelectedStudents();
            string branchNos = this.GetSelectedBranch();
            string semesterNos = this.GetSelectedSemester();
            string Ptype = this.GetSelectedPtype();
            ViewState["BRANCHNO"] = branchNos.ToString();
            ViewState["SEMESTERNO"] = semesterNos.ToString();
            ViewState["PAYMENTTYPE"] = Ptype.ToString();
            if (studentIDs.Length == 0){
                ShowMessage("Please select students to create fee demand.");
                return;
            }
           // string response = string.Empty;
            string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, studentIDs, Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));
            //string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, studentIDs, Convert.ToInt32(rdoDemand.SelectedValue), Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));

            if (response != "-99"){
                if (response.Length > 2)
                    ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                else
                    ShowMessage("Demand successfully created for Selected students.");
                FillStudent();
                ShowMessage("Demand successfully created for Selected students.");
                //this.ShowReport(demandCriteria, "Fee_Demand_Report", "FeeDemandReport_Detailed.rpt");
            }
            else
                ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetSelectedStudents()
    {
        string studentIDs = string.Empty;
       
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            if ((item.FindControl("chkSelect") as CheckBox).Checked)
            {
                if (studentIDs.Length > 0)
                    studentIDs += ", ";
                string id = (item.FindControl("hidStudentNo") as HiddenField).Value;
                if (id != string.Empty)
                {
                    studentIDs += id;
                }
            }
        }
        return studentIDs;
    }
    
    private string GetSelectedBranch()
    {
       
        string branchNos = string.Empty;
       
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            if ((item.FindControl("chkSelect") as CheckBox).Checked)
            {
               
                if (branchNos.Length > 0)
                    branchNos += ", ";
              
                string branch = (item.FindControl("HidBranchNo") as HiddenField).Value;

                if (branch != string.Empty)
                {
                    branchNos += branch;
                }
              
            }
        }
        return branchNos;
    }

    private string GetSelectedSemester()
    {

        string semesterNos = string.Empty;
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            if ((item.FindControl("chkSelect") as CheckBox).Checked)
            {

                if (semesterNos.Length > 0)
                    semesterNos += ", ";


                string sem = (item.FindControl("HidSemesterNo") as HiddenField).Value;

                if (sem != string.Empty)
                {
                    semesterNos += sem;
                }

            }
        }
        return semesterNos;
    }

    private string GetSelectedPtype()
    {

        string Ptype = string.Empty;
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            if ((item.FindControl("chkSelect") as CheckBox).Checked)
            {

                if (Ptype.Length > 0)
                    Ptype += ", ";

                string ptype = (item.FindControl("HidPtypeNo") as HiddenField).Value;

                if (ptype != string.Empty)
                {
                    Ptype += ptype;
                }
            }
        }
        return Ptype;
    }

    protected void btnShowStudents_Click(object sender, EventArgs e)
    {
        FillStudentList();
        //pnlFalse.Visible = true;
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
        //changed by shubham reason condition not match.
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = B.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
        if (ddlDegree.SelectedValue!="0")
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER ST INNER JOIN ACD_STUDENT S ON (S.SEMESTERNO=ST.SEMESTERNO)", "DISTINCT ST.SEMESTERNO", "ST.SEMESTERNAME", "ST.SEMESTERNO>0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "ST.SEMESTERNO");
        else
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    }

    protected void btnSlip_Click(object sender, EventArgs e)
    {
        string StudentIds = "0";
        foreach (ListViewDataItem items in lvStudents.Items)
        {
            CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked)
                StudentIds += chkSelect.ToolTip + ".";
        }
        if(ddlReceiptType.SelectedValue=="MF")
            ShowReport(StudentIds, "Fee_Receipt_Hostel", "FeeCollectionReceipt_Mess.rpt", "MF");
        else
            ShowReport(StudentIds, "Fee_Receipt_Hostel", "FeeCollectionReceipt_Hostel.rpt", "HF");
    }

    private void ShowReport(string param, string reportTitle, string rptFileName, string Receipt_code)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + param.ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlForSemester.SelectedValue + ",@P_RECEIPT_CODE=" + Receipt_code.ToString() + ",@P_PAYMENT_TYPE=0";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdoDemand_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnabledTextbox();
        divSelectedStudents.Visible = false;
    }

    private void EnabledTextbox()
    {
        if (rdoDemand.SelectedValue == "1")
        {
            btnCreateDemand.Visible = true;
            btnDemand.Visible = false;
            btnCreateDemandForSelStuds.Visible = true;
        }
        else
        {
            btnCreateDemand.Visible = false;
            btnDemand.Visible = true;
            btnCreateDemandForSelStuds.Visible = false;
        }
        foreach (ListViewDataItem items in lvStudents.Items)
        {
            TextBox txtAmount = items.FindControl("txtAmount") as TextBox;
            if (rdoDemand.SelectedValue == "1")
                txtAmount.Enabled = false;
            else
            {
                txtAmount.Enabled = false;
                decimal amount = Convert.ToDecimal(txtAmount.Text) / 2;
                txtAmount.Text = amount.ToString();
            }
        }   
    }

    protected void btnDemand_Click(object sender, EventArgs e)
    {
        try
        {
            FeeDemand demandCriteria = this.GetDemandCriteria();
            string studentIDs = this.GetSelectedStudents();
            if (studentIDs == "")
            {
                objCommon.DisplayMessage("Please Select Atleast one student", this.Page);
                return;
            }
            //string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, studentIDs, Convert.ToInt32(rdoDemand.SelectedValue),Convert.ToInt32(ddlForSemester.SelectedValue));
            //string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, studentIDs, Convert.ToInt32(rdoDemand.SelectedValue), Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));
            string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, studentIDs, Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));

            if (response != "-99")
            {
                if (response.Length > 2)
                    ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                else
                    ShowMessage("Demand successfully created for Selected students.");
            }
            else
                ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.btnCreateDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillStudentList()
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetApplyHostelerStudents(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), ddlReceiptType.SelectedValue, Convert.ToInt32(ddlForSemester.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                divSelectedStudents.Visible = true;
                btnCreateDemand.Visible = true;
                EnabledTextbox();
            }
            else
            {
                divSelectedStudents.Visible = false;
                btnCreateDemand.Visible = false;
                ShowMessage("No student found.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.FillStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void FillStudent()  //Created By Himanshu Tamrakar on date 05012024
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetApplyHostelerStudents(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), ddlReceiptType.SelectedValue, Convert.ToInt32(ddlForSemester.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                divSelectedStudents.Visible = true;
                btnCreateDemand.Visible = true;
                EnabledTextbox();
            }
            else
            {
                divSelectedStudents.Visible = false;
                btnCreateDemand.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CreateFeeDemand.FillStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlForSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStudentList();
        //pnlFalse.Visible = true;
    }
}