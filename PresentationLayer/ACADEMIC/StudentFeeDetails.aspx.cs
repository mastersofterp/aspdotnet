
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
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;


using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using AjaxControlToolkit;

public partial class FEESCOLLECTION_Transaction_FeeDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();

    FeeCollectionController feeController = new FeeCollectionController();



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
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\INCLUDES\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\INCLUDES\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\INCLUDES\modalbox.js"));

        if (!Page.IsPostBack)
        {

            //pnlFeesDetails.Visible = false;
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
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                if (Request.QueryString["id"] != null)
                {
                    ViewState["action"] = "edit";
                    BindData(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    //txtSearchText.Text = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO =" + Convert.ToInt32(Request.QueryString["id"].ToString()));
                }


                // Added by sumit-- 28-Nov-2019


                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    // int idno;
                    //SearchPanel.Visible = false;
                    myModal2.Visible = false;

                    if (Session["IDNO"] != string.Empty)
                    {
                        //  string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtSearchText.Text.Trim() + "'");

                        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO = '" + Session["IDNO"] + "'");

                        //if (idno != "")
                        //{
                        //    string semester = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO = '" + Session["IDNO"] + "'");

                        int studentId = Convert.ToInt32(idno.ToString());

                        ViewState["idno"] = studentId;
                        //    int semesterno = Convert.ToInt32(semester.ToString());
                        //    //ddlSemester.SelectedItem.Text = objCommon.LookUp("ACD_SEMESTER", "SEMFULLNAME", "SEMESTERNO = " + semesterno + "");
                        if (idno == string.Empty)
                        {
                            ViewState["idno"] = null;
                            objCommon.DisplayMessage("Student Name. Not Found", this.Page);
                            //divReceiptType.Visible = true;
                            return;
                        }
                        else
                        {
                            BindDataOnLoad(studentId);
                            divStudInfo.Visible = false;
                            //divFeeReceipts.Visible = true;
                            //BalanceFessDiv.Visible = false;
                            // divFeesDetails.Visible = false;
                        }

                    }
                    else
                    {
                        // ShowMessage("Please Enter Correct Student Name..");
                        //objCommon.DisplayMessage("Please Enter Correct Student Name..",this.Page);
                    }


                }

            }
            //Search Pannel Dropdown Added by Swapnil

            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
            ddlSearch.SelectedIndex = 1;
            ddlSearch_SelectedIndexChanged(sender, e);
            //End Search Pannel Dropdown
            //this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
        }
        else
        {

            // int usertype = 2;
            //if (Session["usertype"] == 2)
            //{
            //    fieldset1.Visible = false;
            //}
            //if (Page.Request.Params["__EVENTTARGET"] != null)
            //{
            //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
            //    {
            //        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
            //        bindlist(arg[0], arg[1]);
            //    }

            //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
            //    {
            //        //txtSearch.Text = string.Empty;
            //        //lvStudent.DataSource = null;
            //        //lvStudent.DataBind();
            //        //lblNoRecords.Text = string.Empty;
            //    }
            //}
        }
        divMsg.InnerHtml = string.Empty;
        //txtSearchText.Focus();
        //  objCommon.DisplayMessage("1st step ", this.Page);
    }

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
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
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

        //DataSet ds = BindListView(Convert.ToInt32(ViewState["idno"]));

        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    // Bind list view with paid receipt data 
        //    lvPaidReceipts.DataSource = ds;
        //    lvPaidReceipts.DataBind();
        //    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaidReceipts);//Set label - 
        //}
        //else
        //{
        //    divFeeReceipts.InnerHtml = "No Previous Receipt Found.<br/>";

        //}


        divFeeReceipts.Visible = false;



    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;


            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;
            FeeCollectionController feeController = new FeeCollectionController();

            DataSet ds = BindListView(Convert.ToInt32(idno));

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    // Bind list view with paid receipt data 
            //    lvPaidReceipts.DataSource = ds;
            //    lvPaidReceipts.DataBind();
            //    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaidReceipts);//Set label - 
            //}
            //else
            //{
            //    divFeeReceipts.InnerHtml = "No Previous Receipt Found.<br/>";

            //}



            if (idno > 0)
            {
                if (idno > 0)
                {

                    int studentId = Convert.ToInt32(idno.ToString());
                    ViewState["idno"] = studentId;

                    if (idno == 0)
                    {
                        ViewState["idno"] = null;
                        objCommon.DisplayMessage("Student Name. Not Found", this.Page);
                        return;
                    }
                    else
                    {


                        lvPaidReceipts.DataSource = ds;
                        lvPaidReceipts.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaidReceipts);//Set label - 
                        BindDataOnLoad(studentId);
                        DisplayInformation(idno);
                        //  BindData(studentId);
                        divFeeReceipts.Visible = true;
                        lvPaidReceipts.Visible = true;


                    }

                }
                else
                {
                    ShowMessage("Please Enter Correct Student Name..");
                    //objCommon.DisplayMessage("Please Enter Correct Student Name..",this.Page);
                }
            }
            else
                ShowMessage("Please Enter Student Name..");
            //objCommon.DisplayMessage("Please Enter Correct Student Name..", this.Page);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion


    //private void bindlist(string category, string searchtext)
    //{
    //    StudentController objSC = new StudentController();
    //    DataSet ds = objSC.RetrieveStudentDetailsForFeesCollection(searchtext, category);

    //    //if (ds.Tables[0].Rows.Count > 0)
    //    //{
    //    //    lvStudent.DataSource = ds;
    //    //    lvStudent.DataBind();
    //    //    lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
    //    //}
    //    //else
    //    //    lblNoRecords.Text = "Total Records : 0";
    //}
    private void ShowStudents(string searchBy, string searchText, string errorMsg)
    {
        DataSet ds = admCanController.SearchStudents(searchText, searchBy);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["StudentId"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
            ViewState["StudentName"] = ds.Tables[0].Rows[0]["NAME"].ToString();
            ViewState["StudentBranch"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            lblStudName.Text = ViewState["StudentName"].ToString();
            //lblBranchName.Text = ViewState["StudentBranch"].ToString();
            //pnlFeesDetails.Visible = true;
        }
        else
        {
            ShowMessage("No student found " + errorMsg);
            //objCommon.DisplayMessage("No student found",this.Page);
        }
    }

    private void BindData(int idno)
    {

        DataSet ds = BindListView(idno);



    }



    // Method Added By Sumit-- 28-Nov-2019

    private void BindDataOnLoad(int idno)
    {
        this.DisplayInformation(idno);
        //  divReceiptType.Visible = true;
        //this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
        //ddlReceiptType.SelectedIndex = 1;
        //string recCode = ddlReceiptType.SelectedValue;
        //DataSet ds = BindListView(idno, recCode);
        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    // Bind list view with paid receipt data 
        //    lvPaidReceipts.DataSource = ds;
        //    lvPaidReceipts.DataBind();
        //}
        //else
        //{
        //    divFeeReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
        //}
        divFeeReceipts.Visible = true;
        //  BalanceFessDiv.Visible = false;

        // BalanceFeesOut.Visible = false;
        //HeadingFees.Visible = true;
        //lvFeesDetails.Visible = true;

        ////changes on--30112019
       




        DataSet ds = feeController.GetPaidReceiptsInfoByStudIdAndReceiptCode(idno);
        lvPaidReceipts.DataSource = ds;
        lvPaidReceipts.DataBind();
        lvPaidReceipts.Visible = true;
        divFeeReceipts.Visible = true;
        //divFeesDetails.Visible = false;

    }



    private void DisplayInformation(int studentId)
    {
        try
        {
            #region Display Student Information
            /// Display student's personal and academic data in 
            /// student information section
            DataSet ds = feeController.GetStudentInfoByIdNew(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                // show student information
                this.PopulateStudentInfoSection(dr);
                //divStudInfo.Style["display"] = "block";
                divStudInfo.Visible = true;
                lvStudent.Visible = false;
                lblNoRecords.Visible = false;
                lvPaidReceipts.Visible = true;
            }
            else
            {
                ShowMessage("Unable to retrieve student's record.");
                return;
            }
            #endregion
            #region Display Fees Information

          


            #endregion

            //  txtTuitionFees.Text = objCommon.LookUp("ACD_STUDENT", "OLD_DUES", "IDNO =" + studentId);
            //  txtOpSchFees.Text = objCommon.LookUp("ACD_STUDENT", "OLD_SCH", "IDNO =" + studentId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            #region Bind data to labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            //lblRegNo.Text = dr["REGNO"].ToString();
            lblRegNo.Text = dr["ROLLNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            if (dr["DEGREENO"].ToString() == "1")
            {
                lblDegree.Text = (dr["DEGREENAME"] + "(" + dr["SHIFT"] + ")").ToString();
            }
            else
            {
                lblDegree.Text = dr["DEGREENAME"].ToString();
            }
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblSemester.ToolTip = dr["SEMESTERNO"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            lblBatch.ToolTip = dr["ADMBATCH"].ToString();
            //lblRoll.Text = dr["ROLLNO"].ToString();
            lblEnroll.Text = dr["REGNO"].ToString();
            // lblConcession.Text = dr["CONCESSION"].ToString();
            lblCategory.Text = dr["CATEGORY"].ToString();
            // lblMgmtConcession.Text = dr["MGM_CONCESSION"].ToString();
            // lblAdmCategory.Text = dr["ADM_CATEGORY"].ToString();
            //  lblAdmMode.Text = dr["ADM_MODE"].ToString();
            lblMobile.Text = dr["STUDENTMOBILE"].ToString();
            lblDOB.Text = dr["DOB"].ToString();

            //   lblStatus.Text = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_SEM_PROMOTION SP ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "(CASE ISNULL(PROV_SEM_STATUS,0) WHEN 0 THEN 'REGULAR' WHEN 1 THEN 'RE-ADMISSION' WHEN 2 THEN 'RE-REGISTERED' END) [STATUS]", "S.IDNO=" + Convert.ToInt32(dr["IDNO"].ToString()));
            //   imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            #endregion

            #region Secure imporatant data
            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the fee collection record.
            ViewState["StudentId"] = dr["IDNO"].ToString();
            ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["YearNo"] = dr["YEAR"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();
            //ddlSemester.SelectedValue;
            ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
            #endregion
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string recCode = ddlReceiptType.SelectedValue;
        //// string recCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE = '" + recCode1 + "'");
        //DataSet ds = BindListView(Convert.ToInt32(ViewState["idno"]), recCode);
        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    // Bind list view with paid receipt data 
        //    lvPaidReceipts.DataSource = ds;
        //    lvPaidReceipts.DataBind();
        //    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaidReceipts);//Set label - 
        //}
        //else
        //{
        //    divFeeReceipts.InnerHtml = "No Previous Receipt Found.<br/>";
        //}
        //divFeeReceipts.Visible = true;
    }

    private DataSet BindListView(int idno)
    {
        int studentId = Convert.ToInt32(idno.ToString());
        ViewState["idno"] = studentId;
        DataSet ds;

        ds = feeController.GetPaidReceiptsInfoByStudIdAndReceiptCode(idno);
        lvPaidReceipts.DataSource = ds;
        lvPaidReceipts.DataBind();
        lvPaidReceipts.Visible = true;
        divHidPreviousReceipts.Visible = true;
        return ds;
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    //string msg = string.Empty;
    //    //    //this.ValidateSubmissionData(ref msg);
    //    //    //if (msg != string.Empty)
    //    //    //{
    //    //    //    this.ShowMessage(msg);
    //    //    //    return;
    //    //    //}
    //    //    //else
    //    //    //{
    //    //        decimal studAmount = (txtTuitionFees.Text.Trim() != string.Empty ? Convert.ToDecimal(txtTuitionFees.Text.Trim()) : 0);
    //    //        decimal schAmount = (txtOpSchFees.Text.Trim() != string.Empty ? Convert.ToDecimal(txtOpSchFees.Text.Trim()) : 0);
    //    //        int idno = Convert.ToInt32(ViewState["idno"]);
    //    //        FeeCollectionController feeController = new FeeCollectionController();
    //    //        CustomStatus cs = (CustomStatus)feeController.UpdateOldDues(idno, studAmount, schAmount);
    //    //        if (cs.Equals(CustomStatus.RecordUpdated))
    //    //        {
    //    //            ShowMessage("Transaction Saved Successfully.");
    //    //        }
    //    //        else
    //    //        {
    //    //            ShowMessage("Transaction Failed.");
    //    //        }
    //    //        //Response.Redirect(Request.Url.ToString());
    //    //        BindData(Convert.ToInt32(ViewState["idno"]));
    //    //    //}
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    if (Convert.ToBoolean(Session["error"]) == true)
    //    //        objUaimsCommon.ShowError(Page, "Academic_ChargeLateFee.btnSave_Click() --> " + ex.Message + " " + ex.StackTrace);
    //    //    else
    //    //        objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    //}

    //}

    //private string ValidateSubmissionData(ref string msg)
    //{
    //    if (txtTuitionFees.Text.Trim() == string.Empty)
    //    {
    //        if (msg.Length > 0) msg += "\\n";
    //        msg += "Please Enter Tuition Fee Amount.";
    //    }
    //    if (txtTuitionFees.Text.Trim() != string.Empty && Convert.ToDouble(txtTuitionFees.Text.Trim()) < 1)
    //    {
    //        if (msg.Length > 0) msg += "\\n";
    //        msg += "Enter Tuition Fee Amount can not be zero or less than zero.";
    //    }

    //    return msg;
    //}

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }
    }
    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ViewState["idno"]
            ImageButton btnPrint = sender as ImageButton;
            if (btnPrint.CommandArgument != string.Empty)
            //&& GetViewStateItem("StudentId") != string.Empty)
            {
                int DCR = Convert.ToInt32(btnPrint.CommandArgument);
                ViewState["DCR"] = DCR;
                //if (Convert.ToString(ViewState["PaymentMode"]) == "C")
                //{
                //    this.ShowReport("FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Int32.Parse(GetViewStateItem("StudentId")), "2");
                //}
                //else
                //{
                //    this.ShowReport("FeeCollectionReceipt.rpt", Int32.Parse(btnPrint.CommandArgument), Int32.Parse(GetViewStateItem("StudentId")), "2");
                //}
                ShowReport("StudentFeeCollection", "StudentFeeCollectionReceipt.rpt");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string instituteno = ViewState["insti"].ToString();
            //string Code = Session["userno"].ToString();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + ViewState["idno"] + "," + "@P_DCRNO=" + ViewState["DCR"];
            //+ "@P_ADMYEAR= " + Convert.ToInt32(ddlAcdYear.SelectedValue) + "," + "@P_FEE_CATEGORY_NO= " + Convert.ToInt32(ddlFee.SelectedValue) + "," + "@P_STARTDATE=" + txtStartdate.Text + "," + "@P_ENDDATE=" + txtEnddate.Text + "," + "@P_SESSIONNO= " + Convert.ToInt32(ddlSession.SelectedValue) + "," + "@P_COLLEGE_ID= " + instituteno;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            ////        //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
