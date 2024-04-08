//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ADMISSION CANCELLATION
// CREATION DATE : 08-AUG-2009
// CREATED BY    : AMIT YADAV
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
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using SendGrid;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;

public partial class Academic_AdmissionCancellation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();
    StudentRegist objSR = new StudentRegist();

    string FileName = string.Empty;
    public string Docpath = string.Empty;
    string DirPath = string.Empty;
    public int count = 0;
    public string studid = string.Empty;
    public string filename = string.Empty;
    public string enrollno = string.Empty;
    public string fname = string.Empty;
    public int i = 0;
    public string btnfilename = string.Empty;

    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    //PATH TO EXTRACT IMAGES
    private string DirPath1 = string.Empty;

    #region Page Events

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
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //Populate all the DropDownLists
                    //FillDropDown();
                }
                FillDropDown();
                YearBind();
                //Search Pannel Dropdown Added by Swapnil
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                ddlSearch.SelectedIndex = 1;
                ddlSearch_SelectedIndexChanged(sender, e);
                //End Search Pannel Dropdown
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by reprint receipt or cancel receipt buttons
                /// if yes then call corresponding methods
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    ////if (Request.Params["__EVENTTARGET"].ToString() == "ShowClearance")
                    ////    this.ShowClearance(Request.Params["__EVENTARGUMENT"].ToString());
                    ////else if (Request.Params["__EVENTTARGET"].ToString() == "CancelAdmission")
                    ////    this.CancelAdmission();
                    if (Request.Params["__EVENTTARGET"].ToString() == "CancelAdmission")
                        this.CancelAdmission();

                    if (Request.Params["__EVENTTARGET"].ToString() == "ReAdmission")
                        this.ReAdmission();


                    int count = 0;
                    if (Page.Request.Params["__EVENTTARGET"] != null)
                    {
                        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                        {
                            string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                            bindlist(arg[0], arg[1]);
                        }

                        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                        {
                            // lblMsg.Text = string.Empty;

                        }
                    }

                }
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ReAdmission()
    {
        try
        {

            int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            int admBatchno = Convert.ToInt32(ddlAdmBatch2.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree2.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege2.SelectedValue);
            int branchno = Convert.ToInt32(ddlBranch2.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSemester2.SelectedValue);
            int reAdmit_Uano = Convert.ToInt32(Session["userno"]);
            string remark = "This student was Re-admitted by " + Session["userfullname"].ToString();
            remark += " on " + DateTime.Now + ". " + txtRemark1.Text.Trim();

            //if (admCanController.ReAdmissionStudent(studId, admBatchno, collegeid, degreeno, branchno, semesterno, reAdmit_Uano, remark))
            //{
            //    objCommon.DisplayUserMessage(this.updReAdmit, "Request sent for Re-Admission successfully !", this.Page);
            //    btnReAdmissionSlip.Enabled = true;
            //    btnReAdmit.Enabled = false;
            //    clearAdmit();
            //}
            //else
            //    objCommon.DisplayUserMessage(this.updReAdmit, "Unable to ReAdmit this Student\\'s.", this.Page);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void clearAdmit()
    {
        foreach (Control c in divReAdmit.Controls)
        {
            DropDownList d = c as DropDownList;
            if (d != null)
            {
                d.SelectedIndex = 0;
            }
        }
        txtRemark1.Text = string.Empty;
        lblAmtPaid.Text = string.Empty;
        lblTAmt.Text = string.Empty;
        lvReAdmit.DataSource = null;
        lvReAdmit.DataBind();
        txtSearchText1.Text = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }
    #endregion

    #region SearchPannel
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        lvSearchResults.Visible = false;
        divTotalAmount.Visible = false;
        divPaidAmount.Visible = false;
        divbreakof.Visible = false;
        //if (value == "BRANCH")
        //{
        //    divbranch.Attributes.Add("style", "display:block");

        //}
        //else if (value == "SEM")
        //{
        //    divSemester.Attributes.Add("style", "display:block");
        //}
        //else
        //{
        //    divtxt.Attributes.Add("style", "display:block");
        //}

        //ShowDetails();

    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;
            objSR.IDNO = idno;
            ViewState["idno"] = Session["stuinfoidno"].ToString();
            objSR.REGNO = txtSearchText.Text;

            int organizationid = Convert.ToInt32(Session["OrgId"]);
            DataSet ds = admCanController.GetStudentsForCancelAdmission(objSR, organizationid);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                hdcount.Value = ds.Tables[0].Rows.Count.ToString();
                ViewState["StudentId"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                int i = Convert.ToInt32(ViewState["StudentId"]);
                string count = objCommon.LookUp("ACD_ADM_CANCEL", "isnull(COUNT(IDNO),0)IDNO", "IDNO=" + Convert.ToInt32(idno) + " AND ORGANIZATIONID=" + organizationid);
                if (count != "0")
                {
                    objCommon.DisplayMessage(this.Page, "Admission already Cancelled.", this.Page);
                    txtSearchText.Text = string.Empty;
                    lvSearchResults.Visible = false;
                    btnCancelAdmission.Visible = false;
                    btnCancelAdmissionSlip.Visible = false;
                    divRemark.Visible = false;
                    divFileUpd.Visible = false;
                    divTotalAmount.Visible = false;
                    divPaidAmount.Visible = false;
                    lvStudent.Visible = true;
                    lblNoRecords.Visible = false;
                    chkdiscontinue.Checked = true;
                }
                else
                {
                    lvSearchResults.DataSource = ds;
                    lvSearchResults.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSearchResults);//Set label 
                    // hdcount.Value = ds.Tables[0].Rows.Count.ToString();
                    lvSearchResults.Visible = true;
                    btnCancelAdmission.Visible = true;
                    btnCancelAdmission.Enabled = true;
                    btnCancelAdmissionSlip.Enabled = false;
                    btnCancelAdmissionSlip.Visible = false;
                    divRemark.Visible = true;
                    divFileUpd.Visible = true;
                    txtRemark.Text = string.Empty;

                    divTotalAmount.Visible = true;
                    divPaidAmount.Visible = true;

                    lvStudent.Visible = false;
                    lvStudent.DataSource = null;
                    lblNoRecords.Visible = false;
                    if (Convert.ToInt32(organizationid) == 2 ? divbreakof.Visible = true : divbreakof.Visible = false)
                    
                    //RadioButton rb = sender as RadioButton;
                    idno = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString());

                    if (idno != 0)
                    {
                        DataSet dsinfo = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,COLLEGE_ID,SEMESTERNO,ADMBATCH", "PTYPE", "idno=" + idno + "", string.Empty);
                        DataSet ds1 = admCanController.GetFeesDetailsForAdmissionCan(idno, Convert.ToInt32(dsinfo.Tables[0].Rows[0]["DEGREENO"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["ADMBATCH"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["PTYPE"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["COLLEGE_ID"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["BRANCHNO"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["SEMESTERNO"]));

                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            lblAmtTotal.Text = ds1.Tables[0].Rows[0]["TOTAL_FEES"].ToString();
                            lblAmtPaids.Text = ds1.Tables[0].Rows[0]["FEES_PAID"].ToString();
                        }
                        else
                        {
                            divTotalAmount.Visible = false;
                            divPaidAmount.Visible = false;
                        }
                    }


                    ////btnShowClearance.Visible = true;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Admission already Cancelled.", this.Page);
                txtSearchText.Text = string.Empty;
                lvSearchResults.Visible = false;
                btnCancelAdmission.Visible = false;
                btnCancelAdmissionSlip.Visible = false;
                divRemark.Visible = false;
                divFileUpd.Visible = false;
                ////btnShowClearance.Visible = false;
            }
        }


        catch (Exception ex)
        {
            throw;
        }


    }
    #endregion

    private void CancelAdmission() //Deepali
    {
        try
        {
            int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            string remark = "This student's admission has been cancelled by " + Session["userfullname"].ToString();
            remark += " on " + DateTime.Now + ". " + txtRemark.Text.Trim();

            string Remark = txtRemark.Text.Trim();
            int Uano = Convert.ToInt32(Session["userno"]);
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            int organizationid = Convert.ToInt32(Session["OrgId"]);

            if (admCanController.CancelAdmission(studId, remark, organizationid))
            {
                // admCanController.InsertAdmissionCancelInfo(studId, Remark, Uano, ipAddress);
                ShowMessage("Admission cancelled successfully.");
                btnCancelAdmissionSlip.Enabled = false;
                btnCancelAdmission.Enabled = false;
                lvSearchResults.Visible = false;
                ////btnCancelAdmission.Visible = false;
                ////btnCancelAdmissionSlip.Visible = false;
                divRemark.Visible = false;
                divFileUpd.Visible = false;
            }
            else
                ShowMessage("Unable to cancel the student\\'s admission.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private string GetViewStateItem(string itemName) //Deepali
    {
        try
        {
            if (ViewState.Count > 0 &&
                ViewState[itemName] != null &&
                ViewState[itemName].ToString() != null &&
                ViewState[itemName].ToString() != string.Empty)
                return ViewState[itemName].ToString();
            else
                return string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlMonth, "ACD_MONTH", "MONTHNO", "MONTH_NAME", "MONTHNO>0", "MONTHNO");
            //fill degree
            objCommon.FillDropDownList(ddlDegree, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreename");
            objCommon.FillDropDownList(ddlDegree1, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreename"); //added by srikanth P 13-04-2020
            // objCommon.FillDropDownList(ddlDegree2, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreename");
            objCommon.FillDropDownList(ddlAdmBatch2, "acd_admbatch", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO desc");
            //fill semester
            // objCommon.FillDropDownList(ddlsem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

            objCommon.FillDropDownList(ddlCollege2, "ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND ACTIVESTATUS=1", "COLLEGE_ID");

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "B.ACTIVESTATUS=1", "LONGNAME");//Added by Dileep for Searching Student. 04.01.2022
        }
        catch
        {
            throw;
        }
    }

    private void YearBind()
    {
        //ddlYear.Items.Add("Please Select");
        //ddlYear.SelectedItem.Value = "0";
        for (int jLoop = 2016; jLoop <= DateTime.Now.Year + 4; jLoop++)   //need to add +4 in order to increase Year
        {
            ddlYear.Items.Add(jLoop.ToString());
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Fill Branch;
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            objCommon.FillDropDownList(ddlAdmBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND B.ACTIVESTATUS=1", "LONGNAME");
        }
        catch
        {
            throw;
        }
    }

    protected void ddlDegree1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////added by srikanth P 13-04-2020
        // Fill Branch;
        try
        {
            objCommon.FillDropDownList(ddlBranch1, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree1.SelectedValue), "LONGNAME");
        }
        catch
        {
            throw;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //check the from date always be less than to date
            string[] fromDate = txtFromDate.Text.Split('/');
            string[] toDate = txtToDate.Text.Split('/');
            DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
            DateTime todate = Convert.ToDateTime(Convert.ToInt32(toDate[0]) + "/" + Convert.ToInt32(toDate[1]) + "/" + Convert.ToInt32(toDate[2]));
            if (fromdate > todate)
            {
                objCommon.DisplayMessage("From Date always be less than To date. Please Enter proper Date range.", this.Page);
                clearControl();
            }
            else
            {
                this.Export();
                // ShowReport("BranchCancelAdmission", "rptBranchwiseAdmissionCancel.rpt", 0);
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void ShowReport(string reportTitle, string rptFileName, int reporttype)
    {
        int Organizationid = Convert.ToInt32(Session["OrgId"]);
        if (reporttype == 0)
        {
            DataSet ds = admCanController.GetCancelledAdmissionStudentReport(Convert.ToDateTime(txtFromDate.Text), (Convert.ToDateTime(txtToDate.Text)).AddDays(1), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlAdmBranch.SelectedValue), reporttype, Organizationid);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/CommonReport.aspx?";
                        url += "pagetitle=" + reportTitle;
                        url += "&path=~,Reports,Academic," + rptFileName;
                        DateTime dtStart = Convert.ToDateTime(txtFromDate.Text);
                        DateTime dtEnd = (Convert.ToDateTime(txtToDate.Text)).AddDays(1);
                        string username = Session["username"].ToString();
                        string colcode = Session["colcode"].ToString();
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + dtStart.ToString("yyyy-mm-dd hh:mm:ss.mmm") + ",@P_END_DATE=" + dtEnd.ToString("yyyy-mm-dd hh:mm:ss.mmm") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString();
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + dtStart.ToString("dd/MM/yyyy") + ",@P_END_DATE=" + dtEnd.ToString("dd/MM/yyyy") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlAdmBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString() + ",@P_REPORTTYPE=" + reporttype;
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtFromDate.Text.Trim() + ",@P_END_DATE=" + txtToDate.Text.Trim() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString();
                        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        //divMsg.InnerHtml += " </script>";

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                        sb.Append(@"window.open('" + url + "','','" + features + "');");
                        ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(pnlFeeTable, "No Record Found.", this.Page);
                    ddlDegree.SelectedIndex = 0;
                    ddlAdmBranch.SelectedIndex = 0;
                    txtFromDate.Text = string.Empty;
                    txtToDate.Text = string.Empty;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(pnlFeeTable, "No Record Found.", this.Page);
                ddlDegree.SelectedIndex = 0;
                ddlAdmBranch.SelectedIndex = 0;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
            }
        }
        else if (reporttype == 1)
        {
            DataSet ds = admCanController.GetCancelledAdmissionStudentReport(Convert.ToDateTime(txtFromDate1.Text), (Convert.ToDateTime(txtToDate1.Text)).AddDays(1), Convert.ToInt32(ddlDegree1.SelectedValue), Convert.ToInt32(ddlBranch1.SelectedValue), reporttype, Organizationid);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/CommonReport.aspx?";
                        url += "pagetitle=" + reportTitle;
                        url += "&path=~,Reports,Academic," + rptFileName;
                        DateTime dtStart = Convert.ToDateTime(txtFromDate1.Text);
                        DateTime dtEnd = (Convert.ToDateTime(txtToDate1.Text)).AddDays(1);
                        string username = Session["username"].ToString();
                        string colcode = Session["colcode"].ToString();
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + dtStart.ToString("yyyy-mm-dd hh:mm:ss.mmm") + ",@P_END_DATE=" + dtEnd.ToString("yyyy-mm-dd hh:mm:ss.mmm") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString();
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + dtStart.ToString("dd/MM/yyyy") + ",@P_END_DATE=" + dtEnd.ToString("dd/MM/yyyy") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree1.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue) + ",@UserName=" + Session["username"].ToString() + ",@P_REPORTTYPE=" + reporttype;
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtFromDate.Text.Trim() + ",@P_END_DATE=" + txtToDate.Text.Trim() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString();
                        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        //divMsg.InnerHtml += " </script>";

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                        sb.Append(@"window.open('" + url + "','','" + features + "');");
                        ScriptManager.RegisterClientScriptBlock(this.updReAdmit, this.updReAdmit.GetType(), "controlJSScript", sb.ToString(), true);
                    }
                    catch (Exception ex)
                    {
                        if (Convert.ToBoolean(Session["error"]) == true)
                            objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                        else
                            objCommon.ShowError(Page, "Server Unavailable.");
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(updReAdmit, "No Record Found.", this.Page);
                    ddlDegree1.SelectedIndex = 0;
                    ddlBranch1.SelectedIndex = 0;
                    txtFromDate1.Text = string.Empty;
                    txtToDate1.Text = string.Empty;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updReAdmit, "No Record Found.", this.Page);
                ddlDegree1.SelectedIndex = 0;
                ddlBranch1.SelectedIndex = 0;
                txtFromDate1.Text = string.Empty;
                txtToDate1.Text = string.Empty;
            }
        }
    }

    private void ShowReport_Re_Admission_Slip(string reportTitle, string rptFileName)
    {
        try
        {
            int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + studId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            //divMsg1.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg1.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg1.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updReAdmit, this.updReAdmit.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport_Adm_Cancel(string reportTitle, string rptFileName)
    {
        try
        {
            int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + studId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void clearControl()
    {
        ddlDegree.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlDegree.SelectedIndex = 0;
        ////ddlBranch.SelectedIndex = 0;
        //txtFromDate.Text = string.Empty;
        //txtToDate.Text = string.Empty;
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnCancelAdmissionSlip_Click(object sender, EventArgs e)
    {
        ShowReport_Adm_Cancel("StudentAdmissionCancel", "StudentAdmissionCancellationSlip.rpt");
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            //check the from date always be less than to date
            string[] fromDate = txtFromDate.Text.Split('/');
            string[] toDate = txtToDate.Text.Split('/');
            DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
            DateTime todate = Convert.ToDateTime(Convert.ToInt32(toDate[0]) + "/" + Convert.ToInt32(toDate[1]) + "/" + Convert.ToInt32(toDate[2]));
            if (fromdate > todate)
            {
                objCommon.DisplayMessage("From Date always be less than To date. Please Enter proper Date range.", this.Page);
                clearControl();
            }
            else
            {
                Export();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoStudName1.Checked || rdoRegno1.Checked)
            {
                string searchBy = string.Empty;
                string searchText = txtSearchText1.Text.Trim();
                string errorMsg = string.Empty;

                if (rdoStudName1.Checked)
                {
                    searchBy = "name";
                    errorMsg = "having name: " + txtSearchText1.Text.Trim();
                }
                else if (rdoRegno1.Checked)
                {
                    searchBy = "REGNO";
                    errorMsg = "having Reg no.: " + searchText;
                }
                ShowStudents1(searchBy, searchText, errorMsg);
            }
            else
            {
                objCommon.DisplayMessage(this.updReAdmit, "Please Select Reg No. or Name", this.Page);
                txtSearchText1.Text = string.Empty;
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void Export()
    {
        try
        {

            int Organizationid = Convert.ToInt32(Session["OrgId"]);
            DataSet ds = admCanController.GetCancelledAdmissionStudentReport(Convert.ToDateTime(txtFromDate.Text), (Convert.ToDateTime(txtToDate.Text)).AddDays(1), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlAdmBranch.SelectedValue), 0, Organizationid);
            GridView gv_Admission_Cancel_students = new GridView();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string attachment = "attachment; filename=Admission_Cancelled_Student_Details.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    StringWriter sw = new StringWriter();
                    Html32TextWriter htw = new Html32TextWriter(sw);
                    gv_Admission_Cancel_students.HeaderStyle.Font.Bold = true;
                    gv_Admission_Cancel_students.DataSource = ds;
                    gv_Admission_Cancel_students.DataBind();
                    gv_Admission_Cancel_students.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.pnlFeeTable, "No data found.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.pnlFeeTable, "No data found.", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }
    private void ShowStudents1(string searchBy, string searchText, string errorMsg)
    {
        try
        {
            DataSet ds = admCanController.SearchStudents1(searchText, searchBy);
            //  hdrecount.Value = ds.Tables[0].Rows.Count.ToString();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvReAdmit.DataSource = ds;
                lvReAdmit.DataBind();
                lvReAdmit.Visible = true;
                ViewState["StudentId"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                btnReAdmit.Visible = true;
                btnReAdmit.Enabled = true;
                btnReAdmissionSlip.Visible = true;
                btnReAdmissionSlip.Enabled = false;
                divReAdmit.Visible = true;
                txtRemark1.Text = string.Empty;
                ////btnShowClearance.Visible = true;
                // btnCancel_ReAdm.Visible = true;
                ddlDegree2.SelectedValue = "0";
                ddlAdmBatch2.SelectedValue = "0";
                ddlSemester2.SelectedValue = "0";
                ddlCollege2.SelectedValue = "0";

                int idno = 0;
                //RadioButton rb = sender as RadioButton;
                idno = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString());

                if (idno != 0)
                {
                    DataSet dsinfo = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,COLLEGE_ID,SEMESTERNO,ADMBATCH", "PTYPE", "idno=" + idno + "", string.Empty);
                    DataSet ds1 = admCanController.GetFeesDetailsForReadmission(idno, Convert.ToInt32(dsinfo.Tables[0].Rows[0]["DEGREENO"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["ADMBATCH"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["PTYPE"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["COLLEGE_ID"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["BRANCHNO"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["SEMESTERNO"]));

                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        lblTAmt.Text = ds1.Tables[0].Rows[0]["TOTAL_FEES"].ToString();
                        lblAmtPaid.Text = ds1.Tables[0].Rows[0]["FEES_PAID"].ToString();
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(this.updReAdmit, "No student found " + errorMsg, this.Page);
                txtSearchText1.Text = string.Empty;
                lvReAdmit.Visible = false;
                btnReAdmit.Visible = false;
                btnReAdmissionSlip.Visible = false;
                divReAdmit.Visible = false;
                ////btnShowClearance.Visible = false;
                // btnCancel_ReAdm.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btbnReport1_Click(object sender, EventArgs e)
    {

        try
        {
            //check the from date always be less than to date
            string[] fromDate = txtFromDate1.Text.Split('/');
            string[] toDate = txtToDate1.Text.Split('/');
            DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
            DateTime todate = Convert.ToDateTime(Convert.ToInt32(toDate[0]) + "/" + Convert.ToInt32(toDate[1]) + "/" + Convert.ToInt32(toDate[2]));
            if (fromdate > todate)
            {
                objCommon.DisplayMessage("From Date always be less than To date. Please Enter proper Date range.", this.Page);
                clearControl();
            }
            else
            {
                ShowReport("BranchWiseRe_Admission", "rptBranchwiseRe_Admission.rpt", 1);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnReAdmissionSlip_Click(object sender, EventArgs e)
    {
        ShowReport_Re_Admission_Slip("StudentRe_Admission", "StudentReAdmissionSlip.rpt");
    }

    protected void btnReAdmit_Click(object sender, EventArgs e)
    {


        // ReAdmission();

        foreach (ListViewDataItem item in lvReAdmit.Items)
        {
            RadioButton rdb = item.FindControl("rdoSelectRecord") as RadioButton;
            if (rdb.Checked)
                ReAdmission();
            else
            {
                objCommon.DisplayMessage(this.pnlFeeTable, "Please Select a Student", this.Page);
                return;
            }
        }
    }

    //protected void btnCancelAdmission_Click(object sender, EventArgs e)
    //{
    //    foreach (ListViewDataItem item in lvSearchResults.Items)
    //    {
    //        RadioButton rdb = item.FindControl("rdoSelectRecord") as RadioButton;
    //        if (rdb.Checked)
    //            CancelAdmission();
    //        else
    //        {
    //            objCommon.DisplayMessage(this.pnlFeeTable, "Please Select a Student", this.Page);
    //            return;
    //        }
    //    }


    //}

    protected void btnCancelAdmission_Click(object sender, EventArgs e) //Deepali
    {
        #region comment
        //  int count = 0;


        //foreach (ListViewDataItem item in lvSearchResults.Items)
        //{
        //CheckBox cbRow = item.FindControl("rdoSelectRecord") as CheckBox;
        //hdcount.Value = Convert.ToInt32(item.FindControl("rdoSelectRecord") as CheckBox).ToString();

        //if (cbRow.Checked)
        //{




        //}


        //CheckBox cbRow = item.FindControl("rdoSelectRecord") as CheckBox;
        //if (cbRow.Checked == true)
        //{
        //    count++;
        //}
        //  }

        //if (count <= 0)
        //{
        //   // objCommon.DisplayMessage("Please Select Student!!", this.Page);
        //  //  return;
        //}
        #endregion
        int Breakflag = 0;
        string Month, year;
        try
        {
            Label lblDegree = new Label();
            Label lblRegno = new Label();
            Label lblCollege = new Label();
            HiddenField hdnBranchno = new HiddenField();
            foreach (ListViewDataItem item in lvSearchResults.Items)
            {
                lblDegree = item.FindControl("lblDegree") as Label;
                lblRegno = item.FindControl("lblRegno") as Label;
                lblCollege = item.FindControl("lblCollege") as Label;
                hdnBranchno = item.FindControl("hdnBranchno") as HiddenField;
                // int studId = (GetViewStateItem("StudentId") != string.Empty ? int.Parse(GetViewStateItem("StudentId")) : 0);
                string remark = "This student's admission has been cancelled by " + Session["userfullname"].ToString();
                remark += " on " + DateTime.Now + ". " + txtRemark.Text.Trim();

                string Remark = txtRemark.Text.Trim();
                int Uano = Convert.ToInt32(Session["userno"]);
                string ipAddress = Request.ServerVariables["REMOTE_HOST"];
                string CollegeCode = Session["colcode"].ToString();
                //RadioButton rdb = item.FindControl("rdoSelectRecord") as RadioButton;
                CheckBox rdb = item.FindControl("rdoSelectRecord") as CheckBox;
                int studId = Convert.ToInt32(rdb.ToolTip);
                Byte[] UserManual = null;
                string fileName = string.Empty;
                string fileType = string.Empty;
                string path = string.Empty;
                //int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO=" + Session["idno"].ToString()));


                if (fuFile.HasFile)
                {
                    path = MapPath("~/UPLOAD_FILES/ADMCANCEL_UPLOADFILES");

                    if (!(Directory.Exists(MapPath("~/PresentationLayer/UPLOAD_FILES/ADMCANCEL_UPLOADFILES"))))

                        Directory.CreateDirectory(path);

                    fileName = "idno_" + studId + "_" + Path.GetFileName(fuFile.PostedFile.FileName);
                    string existpath = path + "\\" + fileName;
                    string[] array1 = Directory.GetFiles(path);
                    foreach (string str in array1)
                    {
                        if ((existpath).Equals(str))
                        {
                            objCommon.DisplayMessage("File with similar name already exists!", this);
                            return;
                        }
                    }

                    string[] validFileTypes = { "pdf" };
                    string ext = System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                    bool isValidFile = false;
                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    if (!isValidFile)
                    {
                        objCommon.DisplayMessage(this.Page, "Upload the Documents only with following formats:  .pdf", this);
                        return;
                    }
                    else
                    {
                        UserManual = GetImageDataForDocumentation(fuFile);
                        //  fileName ="idno_"+IDNO+"_"+Path.GetFileName(fuFile.PostedFile.FileName);
                        double UserManualLength = UserManual.Length;
                        if ((UserManualLength / 1024) > 100.0 || (UserManualLength / 1024) < 0.001)
                        {
                            objCommon.DisplayMessage(this.Page, "File Size Required Between 0 KB - 100 KB!!", this.Page);
                            fuFile.Focus();
                            return;
                        }
                        fuFile.PostedFile.SaveAs(Server.MapPath("~//UPLOAD_FILES//ADMCANCEL_UPLOADFILES//") + fileName);

                        //     path = Server.MapPath("~//UPLOAD_FILES//BRANCHCHANGE_UPLOADFILES/");
                        fileType = System.IO.Path.GetExtension(fileName);
                    }
                }

                if (rdb.Checked)
                {
                    if (chkbreak.Checked == true)
                    {
                        Breakflag = 1;
                        Month = ddlMonth.SelectedValue;
                        year = ddlYear.SelectedValue;
                    }
                    else
                    {
                        Breakflag = 0;
                        Month = "0";
                        year = "0";
                    }
                    int Discontinue = chkdiscontinue.Checked == true ? 1 : 0;

                    //if (admCanController.CancelAdmission(studId, remark))// DEEPALI
                    //{
                    // admCanController.InsertAdmissionCancelInfo(studId, Remark, Uano, ipAddress, CollegeCode);
                    int Organizationid = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                    admCanController.AdmissionCancel_Request(studId, Remark, Uano, ipAddress, CollegeCode, fileType, fileName, path, Organizationid, Breakflag, Convert.ToInt32(Month), Convert.ToInt32(year));

                    ////lvSearchResults.Visible = false;
                    ////btnCancelAdmission.Visible = false;
                    ////btnCancelAdmissionSlip.Visible = false;
                    ////divRemark.Visible = false;
                    //}

                }
                ShowMessage("Admission cancelled successfully.");
                btnCancelAdmissionSlip.Enabled = false;
                btnCancelAdmission.Enabled = false;
                lvSearchResults.Visible = false;
                btnCancelAdmission.Visible = false;
                btnCancelAdmissionSlip.Visible = false;
                divRemark.Visible = false;
                divFileUpd.Visible = false;
                txtSearchText.Text = string.Empty;
                divPaidAmount.Visible = false;
                divTotalAmount.Visible = false;
                divbreakof.Visible = false;
                ddlSearch.SelectedIndex = 0;
                //  TransferToEmail(lblRegno.ToolTip, lblRegno.Text, lblDegree.ToolTip, lblDegree.Text, txtRemark.Text,lblCollege.Text,Convert.ToInt32(lblCollege.ToolTip),Convert.ToInt32(hdnBranchno.Value));
                //CancelAdmission();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }




    protected void ddlDegree2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////added by srikanth P 13-04-2020
        try
        {
            // Fill Branch;
            objCommon.FillDropDownList(ddlBranch2, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree2.SelectedValue), "LONGNAME");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlCollege2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree2, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON D.DEGREENO=CD.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + Convert.ToInt32(ddlCollege2.SelectedValue), "D.DEGREENO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_ReAdm_Click(object sender, EventArgs e)
    {
        ddlAdmBatch2.SelectedIndex = 0;
        ddlDegree2.SelectedIndex = 0;
        ddlCollege2.SelectedIndex = 0;
        ddlBranch2.SelectedIndex = 0;
        ddlSemester2.SelectedIndex = 0;
        txtRemark1.Text = string.Empty;
    }
    //private void ClearCancellationTab()
    //{
    //    txtSearchText.Text = string.Empty;
    //    txtSearchText1.Text = string.Empty;
    //    lvSearchResults.DataSource = null;
    //    lvSearchResults.DataBind();
    //    lvSearchResults.Visible = false;
    //    divRemark.Visible = false;
    //    btnCancelAdmission.Visible = false;
    //    btnCancelAdmissionSlip.Visible = false;
    //    ddlDegree.SelectedIndex = 0;
    //    ddlBranch.SelectedIndex = 0;
    //    txtFromDate.Text = string.Empty;
    //    txtToDate.Text = string.Empty;
    //    divReAdmit.Visible = false;
    //    btnReAdmit.Visible = false;
    //    btnReAdmissionSlip.Visible = false;
    //    lvReAdmit.Visible = false;
    //    lvReAdmit.DataSource = null;
    //    lvReAdmit.DataBind();
    //}

    private void ClearCancellationTab() //Deepali
    {
        txtSearchText.Text = string.Empty;
        txtSearchText1.Text = string.Empty;
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        lvSearchResults.Visible = false;
        divRemark.Visible = false;
        divFileUpd.Visible = false;
        btnCancelAdmission.Visible = false;
        btnCancelAdmissionSlip.Visible = false;
        ddlDegree.SelectedIndex = 0;
        ddlAdmBranch.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        divReAdmit.Visible = false;
        btnReAdmit.Visible = false;
        btnReAdmissionSlip.Visible = false;
        lvReAdmit.Visible = false;
        lvReAdmit.DataSource = null;
        lvReAdmit.DataBind();
        txtSearchText1.Text = string.Empty;
    }

    protected void rdoRegno1_CheckedChanged(object sender, EventArgs e)
    {
        ClearCancellationTab();
    }
    protected void rdoStudName1_CheckedChanged(object sender, EventArgs e)
    {
        ClearCancellationTab();
    }

    private void ClearReAdmitTab()
    {
        txtSearchText1.Text = string.Empty;
        txtSearchText.Text = string.Empty;
        lvReAdmit.DataSource = null;
        lvReAdmit.DataBind();
        lvReAdmit.Visible = false;
        ddlDegree1.SelectedIndex = 0;
        ddlBranch1.SelectedIndex = 0;
        txtFromDate1.Text = string.Empty;
        txtToDate1.Text = string.Empty;
        divReAdmit.Visible = false;
        btnReAdmit.Visible = false;
        btnReAdmissionSlip.Visible = false;
        divRemark.Visible = false;
        lvSearchResults.Visible = false;
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        btnCancelAdmission.Visible = false;
        btnCancelAdmissionSlip.Visible = false;
    }
    protected void rdoRegno_CheckedChanged(object sender, EventArgs e)
    {
        ClearReAdmitTab();
    }
    protected void rdoStudName_CheckedChanged(object sender, EventArgs e)
    {
        ClearReAdmitTab();
    }

    protected void rdoSelectRecord_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int idno = 0;
            RadioButton rb = sender as RadioButton;
            idno = Convert.ToInt32(rb.ToolTip);

            if (idno != 0)
            {
                DataSet dsinfo = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,COLLEGE_ID,SEMESTERNO,ADMBATCH", "PTYPE", "idno=" + idno + "", string.Empty);
                DataSet ds = admCanController.GetFeesDetailsForReadmission(idno, Convert.ToInt32(dsinfo.Tables[0].Rows[0]["DEGREENO"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["ADMBATCH"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["PTYPE"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["COLLEGE_ID"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["BRANCHNO"]), Convert.ToInt32(dsinfo.Tables[0].Rows[0]["SEMESTERNO"]));

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {



                    lblTAmt.Text = ds.Tables[0].Rows[0]["TOTAL_FEES"].ToString();
                    lblAmtPaid.Text = ds.Tables[0].Rows[0]["FEES_PAID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public bool FileTypeValid(string FileExtention)
    {
        try
        {
            bool retVal = false;
            //string[] Ext = { ".JPEG", ".BMP", ".GIF", ".PDF", ".PNG", ".TIFF", "ICO", ".JPG" };
            string[] Ext = { ".PDF", ".PNG", ".JPEG", ".JPG" };
            foreach (string ValidExt in Ext)
            {
                if (FileExtention.ToUpper() == ValidExt)
                {
                    retVal = true;
                }
            }
            return retVal;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //public byte[] GetImageDataForDocumentation(FileUpload fu)
    //{
    //    if (fu.HasFile)
    //    {
    //        int ImageSize = fu.PostedFile.ContentLength;
    //        Stream ImageStream = fu.PostedFile.InputStream as Stream;
    //        byte[] ImageContent = new byte[ImageSize];
    //        int intStatus = ImageStream.Read(ImageContent, 0, ImageSize);
    //        //ImageStream.Close();
    //        // ImageStream.Dispose();
    //        return ImageContent;
    //    }
    //    else
    //    {
    //        FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
    //        int ImageSize = (int)ff.Length;
    //        byte[] ImageContent = new byte[ff.Length];
    //        ff.Read(ImageContent, 0, ImageSize);
    //        ff.Close();
    //        ff.Dispose();
    //        return ImageContent;
    //    }
    //}

    public byte[] GetImageDataForDocumentation(FileUpload fu) //Deepali
    {
        try
        {
            if (fu.HasFile)
            {
                int ImageSize = fu.PostedFile.ContentLength;
                Stream ImageStream = fu.PostedFile.InputStream as Stream;
                byte[] ImageContent = new byte[ImageSize];
                int intStatus = ImageStream.Read(ImageContent, 0, ImageSize);
                //ImageStream.Close();
                // ImageStream.Dispose();
                return ImageContent;
            }
            else
            {
                FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
                int ImageSize = (int)ff.Length;
                byte[] ImageContent = new byte[ff.Length];
                ff.Read(ImageContent, 0, ImageSize);
                ff.Close();
                ff.Dispose();
                return ImageContent;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckFileAndSave(string filename, string pth)
    {
        try
        {
            int idno = 0;
            string filenames = string.Empty;

            int flag = 0;

            foreach (ListViewDataItem dataitem in lvReAdmit.Items)
            {
                //ListView lv = dataitem.FindControl("lvUserManualList") as ListView;
                //foreach (ListViewDataItem items in lv.Items)
                //{

                //filenames = (dataitem.FindControl("lblUMNo") as Label).Text;

                //if (filenames == btnfilename)
                //{
                FileUpload fuStudPhoto = dataitem.FindControl("fuFile") as FileUpload;
                string name = fuStudPhoto.ToolTip;

                //HiddenField hdno = dataitem.FindControl("HiddenField1") as HiddenField;
                RadioButton rb = dataitem.FindControl("rdoSelectRecord") as RadioButton;
                idno = Convert.ToInt32(rb.ToolTip);
                //HiddenField hdnSMID = dataitem.FindControl("HiddenField2") as HiddenField;
                //int SMID = Convert.ToInt32(hdnSMID.Value);

                string fileType = System.IO.Path.GetExtension(fname);
                byte[] image;

                if (fuStudPhoto.HasFile)
                {
                    if (i == 1)
                    {
                        i = i + 1;
                        image = null;//objCommon.GetImageData(fuStudPhoto);
                        flag = 2;
                        CustomStatus cs = (CustomStatus)admCanController.InsertReadmissionFileDetails(idno, fileType, filename, pth);
                        objCommon.DisplayMessage("Document Uploaded Successfully !", this);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updReAdmit, "Please select only one file at a time.", this.Page);
                        //BindLVDetails();
                        return;
                    }
                }
                else
                {
                    image = null;
                    flag = 1;
                }
                //}
                //else
                //{

                //}
                //}
            }

            //BindLVDetails();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;

            foreach (ListViewDataItem dataitem in lvReAdmit.Items)
            {
                //ListView lv = dataitem.FindControl("lvUserManualList") as ListView;
                //foreach (ListViewDataItem Items in lv.Items)
                //{
                //HiddenField hdno = dataitem.FindControl("HiddenField1") as HiddenField;

                //    int docid1 = Convert.ToInt32(hdno.Value);

                //string filename = (dataitem.FindControl("lblUMNo") as Label).Text;
                //btnfilename = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
                FileUpload Fu1 = dataitem.FindControl("fuFile") as FileUpload;

                //if (filename == btnfilename)
                //{
                if (Fu1.HasFile)
                {
                    //if (filename == btnfilename)
                    //{
                    FileUpload Fu = new FileUpload();
                    Byte[] UserManual = null;
                    string path = MapPath("~/UPLOAD_FILES/READMISSION");
                    Fu = dataitem.FindControl("fuFile") as FileUpload;
                    //int umno = Convert.ToInt32(objCommon.LookUp("ACC_SECTION A INNER JOIN ACD_SUBMODULE_MASTER B ON A.AS_NO = B.MID", "SMID", "SUB_MODULE_NAME='" + filename + "'"));

                    if (Fu.HasFile)
                    {
                        //filename = (dataitem.FindControl("lblUMNo") as Label).Text;
                        Button submit = dataitem.FindControl("btnSubmit") as Button;
                        //string filesname = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
                        //if (filename == filesname)
                        //{
                        if (i == 0)
                        {
                            i = i + 1;
                            count++;
                            //fname = umno + "_" + Fu.FileName;
                            fname = Fu.FileName;
                            Session["FileUpload1"] = fname;

                            //var ext = Fu1.value;
                            string fileType = System.IO.Path.GetExtension(fname);
                            if (!FileTypeValid(fileType))                           
                            {
                                // objCommon.DisplayMessage(this.Page, "Please Upload Valid Files like .pdf, .PNG, .JPEG, .JPG file format", this.Page);
                                objCommon.DisplayMessage(this.Page, "Please Upload Valid Files like .pdf file format", this.Page);
                                Fu.Focus();
                                return;
                            }
                            else
                            {
                                UserManual = GetImageDataForDocumentation(Fu);
                            }

                            string existpath = path + "\\" + fname;

                            string[] array1 = Directory.GetFiles(path);
                            foreach (string str in array1)
                            {
                                if ((existpath).Equals(str))
                                {
                                    objCommon.DisplayMessage("File with similar name already exists!", this);
                                    return;
                                }
                            }

                            double UserManualLength = UserManual.Length;
                            //if ((UserManualLength / 1024) > 10000.0 || (UserManualLength / 1024) < 0.01)
                            if ((UserManualLength / 1024) > 100.0 || (UserManualLength / 1024) < 0.01)
                            {
                                objCommon.DisplayMessage(this.Page, "File Size Required Between 0 kb - 100 KB!!", this.Page);
                                Fu.Focus();
                                return;
                            }
                            if (!(Directory.Exists(MapPath("~/PresentationLayer/UPLOAD_FILES/READMISSION"))))
                                Directory.CreateDirectory(path);

                            string fileName = Path.GetFileName(fname);
                            Fu.PostedFile.SaveAs((Server.MapPath("~//UPLOAD_FILES//READMISSION//")) + fname);

                            ViewState["FileName"] = fname;

                            CheckFileAndSave(fname, path);
                        }
                        else
                        {

                        }
                        //}
                        //else
                        //{
                        //    //BindLV();
                        //    BindLVDetails();
                        //}
                    }
                    //}
                    //else
                    //{

                    //}
                }
                else
                {
                    objCommon.DisplayMessage(updReAdmit, "Please Select File To Upload", this);
                    return;
                }
                //}
                //else
                //{
                //    //objCommon.DisplayMessage(updUserManual, "Please Select File To Upload", this);
                //    //return;
                //}

                //}
            }
            return;

            if (count <= 0)
            {
                objCommon.DisplayMessage(updReAdmit, "Please Select File For Uploaded", this);
            }
            else
            {
                objCommon.DisplayMessage(updReAdmit, "Document Uploaded Successfully", this);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #region Email_Sending
    public void TransferToEmail(string studname, string Regno, string oldbranch, string Degree, string remark, string college, int College_id, int branchno)
    {
        try
        {
            int ret = 0;
            string useremail = "";
            //  string Session = ddlSession.SelectedItem.Text;
            // string sem = ddlSem.SelectedItem.Text;//kare.dileep@mastersofterp.co.in
            if (College_id == Convert.ToInt32(1) && branchno == Convert.ToInt32(8))
            {
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=6");
            }
            else if (College_id == Convert.ToInt32(2))
            {
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=7");
            }
            else if (College_id == Convert.ToInt32(4))
            {
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=8");
            }
            else
            {
                useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=1");
            }


            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();



                msg.From = new MailAddress(fromAddress, "ABBS - Admission Cancllation ");
                msg.To.Add(new MailAddress(useremail));



                msg.Subject = "Regarding Admission Cancellation Approval";
                //FOR MANISH : ERR: AT HTML TAGS :
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                const string EmailTemplate = "<html><body>" +
                                         "<div align=\"center\">" +
                                         "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                          "<tr>" +
                                          "<td>" + "</tr>" +
                                          "<tr>" +
                                         "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                         "</tr>" +
                                         "<tr>" +
                                         "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b><br/></td>" +
                                         "</tr>" +
                                         "</table>" +
                                         "</div>" +
                                         "</body></html>";
                StringBuilder mailBody = new StringBuilder();
                //  mailBody.AppendFormat("<h1>Greetings !!</h1>");
                mailBody.AppendFormat("Dear Sir/Madam <b>" + "" + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Below Student has opted for a Admission Cancellation  that required your approval with Comments.");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>Student Details </b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Reg. No. : </b> " + Regno + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Name : </b>" + studname + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicantion Date :</b> " + DateTime.Now.ToString("dd/MM/yyyy") + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> College Name  : </b>" + college + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Department  : </b>" + Degree + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Currenct Program : </b>" + oldbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                //  mailBody.AppendFormat("<b>Your new Login Password is </b>");
                mailBody.AppendFormat("<b>Comments :" + remark + " </b>");
                mailBody.AppendFormat("<br />");

                string Mailbody = mailBody.ToString();
                string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                msg.IsBodyHtml = true;
                msg.Body = nMailbody;


                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);


                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };




                smtp.Send(msg);

                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                {
                    ret = 1;
                    //    objCommon.DisplayMessage(updSession, "Email Sent Successfully.", this.Page);
                    //Storing the details of sent email
                }

            }




        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    protected void rdoSelectRecord_CheckedChanged1(object sender, EventArgs e)
    {

    }
}