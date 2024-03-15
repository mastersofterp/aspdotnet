//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REPRINT OR CANCEL RECEIPTS
// CREATION DATE : 15-JUN-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE : 22-JUL-2009 BY AMIT YADAV
// MODIFIED DESC : ADDED RECEIPT CANCELLATION FUNCTIONALITY
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

public partial class Academic_ReprintReceipts : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string strDcrNo;
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
                    // divreason.Visible = false;
                    divfooter.Visible = false;
                    pnlmain.Visible = false;
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO > 0", "BANKNO");

                    //this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                    //ddlSearch.SelectedIndex = 0;

                    DataSet dssearch = objCommon.FillDropDown("ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME,SRNO,ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0 UNION ALL select ID,CRITERIANAME,SRNO,ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED FROM ACD_SEARCH_CRITERIA WHERE ID=13", "SRNO");


                    ViewState["dssearch"] = dssearch;
                    ddlSearch.DataSource = dssearch;
                    ddlSearch.DataTextField = dssearch.Tables[0].Columns[1].ToString();
                    ddlSearch.DataValueField = dssearch.Tables[0].Columns[0].ToString();
                    ddlSearch.DataBind();

                    for (int i = 0; i < dssearch.Tables[0].Rows.Count; i++)
                    {
                        ddlSearch.Items[i + 1].Attributes.Add("title", dssearch.Tables[0].Rows[i][3].ToString());
                    }
                    ddlSearch.SelectedIndex = 0;
                }
                ddlSearch.SelectedIndex = 1;
                ddlSearch_SelectedIndexChanged(sender, e);

            }
            else
            {

                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// Check if postback is caused by reprint receipt or cancel receipt buttons
                /// if yes then call corresponding methods
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "ReprintReceipt")
                        this.ReprintReceipt();
                    else if (Request.Params["__EVENTTARGET"].ToString() == "CancelReceipt")
                        this.CancelReceipt();
                    //add for the editing
                    else if (Request.Params["__EVENTTARGET"].ToString() == "EditReceipt")
                        this.EditReceipt();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=ReprintReceipts.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReprintReceipts.aspx");
        }
    }
    #endregion

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string fieldName = string.Empty;
    //        ////string searchText = "'" + txtSearchText.Text.Trim() + "'";
    //        string searchText = txtSearchText.Text.Trim() ;
    //        string errorMsg = string.Empty;

    //        if (rdoReceiptNo.Checked)
    //        {
    //            fieldName = "REC_NO";
    //            errorMsg = "having receipt no.: " + txtSearchText.Text.Trim();
    //        }
    //        else if (rdoEnrollmentNo.Checked)
    //        {
    //            fieldName = "ENROLLNMENTNO";     //"IDNO"
    //            errorMsg = "for student having enrollment no.: " + txtSearchText.Text.Trim();
    //        }

    //        FeeCollectionController feeController = new FeeCollectionController();
    //        DataSet ds = feeController.FindReceipts(fieldName, searchText);
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {

    //            lvPaidReceipts.DataSource = ds;
    //            lvPaidReceipts.DataBind();
    //            lvPaidReceipts.Visible = true;
    //            btnCancel.Disabled = false;
    //            btnReprint.Disabled = false;
    //            btnEdit.Disabled = false;
    //        }
    //        else
    //        {
    //            //ShowMessage("No receipt found " + errorMsg);
    //            //lvPaidReceipts.Visible = false;
    //            //btnReprint.Disabled = true;
    //            //btnCancel.Disabled = true;

    //            ShowMessage("No receipt found " + errorMsg);
    //            lvPaidReceipts.DataSource = ds;
    //            lvPaidReceipts.DataBind();
    //            lvPaidReceipts.Visible = false;
    //            btnCancel.Disabled = true;
    //            btnReprint.Disabled = true;
    //            btnEdit.Disabled = true;
    //        }
    //        txtRemark.Text = string.Empty;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ReprintReceipt()
    {
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            int ReportFlag = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(DISPLAY_HTML_REPORT,0) AS DISPLAY_HTML_REPORT", "OrganizationId=" + Session["OrgId"].ToString()));

            //if (Session["CAN_RECP"].Equals("True"))
            //        {
            //        Session["CAN_RECP"] = 1;
            //        }
            //else if (Session["CAN_RECP"].Equals("False"))
            //        {
            //        Session["CAN_REC"] = 0;
            //        }
            //    else
            //        {
            Session["CAN_REC"] = 0;
            //}
            if (this.GetRecieptData(ref dcr))
            {
                if (Session["OrgId"].ToString().Equals("2"))
                {
                    this.ShowReport_ForCash("FeeCollectionReceiptForCash_crescent.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                    return;
                }
                else if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4"))
                {
                    this.ShowReport_ForCash("FeeCollectionReceiptForCash_cpukota.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                    return;
                }
                else if (Session["OrgId"].ToString().Equals("8"))
                {

                    //this.ShowReport("Fee Receipt", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Convert.ToString(Session["UAFULLNAME"]));
                    this.ShowReport_MIT("Semester Registration", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Session["UAFULLNAME"].ToString());
                }
                else if (Session["OrgId"].ToString().Equals("5"))
                {
                    if (ReportFlag == 1)
                    {
                        //// Below Code added by Rohit M. on dated 29.05.2023 
                        //string url = Request.Url.ToString();
                        //url = Request.ApplicationPath + "/Reports/Academic/Fees/FeeReceipt.html";
                        //// Response.Redirect(url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["username"].ToString() +"&Idno=" + Int32.Parse(GetViewStateItem("StudentId")) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]));
                        //string urlForReceipt = string.Empty;
                        //urlForReceipt = url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(dcr.StudentId) + "&DcrNo=" + Convert.ToInt32(dcr.DcrNo) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + urlForReceipt + "');", true);


                        // // Below Code added by ROHIT M. on dated 01.06.2023 
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/Academic/Fees/FeeReceipt.html?";
                        url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(dcr.StudentId) + "&DcrNo=" + Convert.ToInt32(dcr.DcrNo) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                        //  // Above Code added by ROHIT M. on dated 01.06.2023 

                    }
                    else
                    {
                        this.ShowReport_ForCash("FeeCollectionReceiptForCash_JECRC.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                    }
                }

                if (dcr.PaymentModeCode == "C")
                {
                    if (Session["OrgId"].ToString().Equals("2"))
                    {
                        this.ShowReport_ForCash("FeeCollectionReceiptForCash_crescent.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                    }
                    if (Session["OrgId"].ToString().Equals("8"))
                    {

                        //this.ShowReport("Fee Receipt", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Convert.ToString(Session["UAFULLNAME"]));
                        this.ShowReport_MIT("Semester Registration", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Session["UAFULLNAME"].ToString());
                    }
                    if (Session["OrgId"].ToString().Equals("5"))
                    {
                        if (ReportFlag == 1)
                        {
                            // Below Code added by Rohit M. on dated 29.05.2023 
                            //string url = Request.Url.ToString();
                            //url = Request.ApplicationPath + "/Reports/Academic/Fees/FeeReceipt.html";
                            //// Response.Redirect(url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["username"].ToString() +"&Idno=" + Int32.Parse(GetViewStateItem("StudentId")) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]));
                            //string urlForReceipt = string.Empty;
                            //urlForReceipt = url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(dcr.StudentId) + "&DcrNo=" + Convert.ToInt32(dcr.DcrNo) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + urlForReceipt + "');", true);
                            // // Below Code added by ROHIT M. on dated 01.06.2023 
                            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                            url += "Reports/Academic/Fees/FeeReceipt.html?";
                            url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(dcr.StudentId) + "&DcrNo=" + Convert.ToInt32(dcr.DcrNo) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                            //  // Above Code added by ROHIT M. on dated 01.06.2023

                        }
                        else
                        {
                            this.ShowReport_ForCash("FeeCollectionReceiptForCash_JECRC.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                        }
                    }
                    else if (Session["OrgId"].ToString().Equals("18"))  //PCEN RECIPT ADDED ON 23_11_2023 DATED ON 50439
                    {
                        this.ShowReport_ForCash_HITS("rpt_Feecollection_HITS.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                    else if (Session["OrgId"].ToString().Equals("19"))  //PCEN RECIPT ADDED ON 23_11_2023 DATED ON 50439
                    {
                        this.ShowReport_ForCash_PCEN("FeeCollectionReceiptForCash_PCEN.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                    else
                    {
                        this.ShowReport_ForCash("FeeCollectionReceiptForCash.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_RECP"]));
                    }
                }
                else if (dcr.PaymentModeCode == "O")
                {
                    if (Session["OrgId"].ToString().Equals("8"))
                    {

                        // this.ShowReport("Semester Registration", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Convert.ToString(Session["UAFULLNAME"]));
                        this.ShowReport_MIT("Semester Registration", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Session["UAFULLNAME"].ToString());
                    }
                    if (Session["OrgId"].ToString().Equals("5"))
                    {
                        if (ReportFlag == 1)
                        {
                            //// Below Code added by Rohit M. on dated 25.05.2023 
                            //string url = Request.Url.ToString();
                            //url = Request.ApplicationPath + "/Reports/Academic/Fees/FeeReceipt.html";
                            //// Response.Redirect(url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["username"].ToString() +"&Idno=" + Int32.Parse(GetViewStateItem("StudentId")) + "&DcrNo=" + Int32.Parse(btnPrint.CommandArgument) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]));
                            //string urlForReceipt = string.Empty;
                            //urlForReceipt = url + "?ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(dcr.StudentId) + "&DcrNo=" + Convert.ToInt32(dcr.DcrNo) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + urlForReceipt + "');", true);
                            // // Below Code added by ROHIT M. on dated 01.06.2023 
                            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                            url += "Reports/Academic/Fees/FeeReceipt.html?";
                            url += "ClgID=" + Session["colcode"].ToString() + "&UA_NAME=" + Session["UAFULLNAME"].ToString() + "&Idno=" + Convert.ToInt32(dcr.StudentId) + "&DcrNo=" + Convert.ToInt32(dcr.DcrNo) + "&Cancel=" + Convert.ToInt32(Session["CANCEL_REC"]);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + url + "');", true);

                            //  // Above Code added by ROHIT M. on dated 01.06.2023
                        }
                        else
                        {
                            this.ShowReport_ForCash("FeeCollectionReceiptForCash_JECRC.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                        }
                    }
                    else if (Session["OrgId"].ToString().Equals("18"))  //PCEN RECIPT ADDED ON 23_11_2023 DATED ON 50439
                    {
                        this.ShowReport_ForCash_HITS("rpt_Feecollection_HITS.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                    else if (Session["OrgId"].ToString().Equals("19"))  //PCEN RECIPT ADDED ON 23_11_2023 DATED ON 50439
                    {
                        this.ShowReport_ForCash_PCEN("FeeCollectionReceiptForCash_PCEN.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                    }
                    else
                    {
                        this.ShowReport_ForCash("FeeCollectionReceiptForCash.rpt", Convert.ToInt32(dcr.DcrNo), Convert.ToInt32(dcr.StudentId), "1", Convert.ToString(Session["UAFULLNAME"]), Convert.ToInt32(Session["CAN_REC"]));
                    }
                }

                //this.ShowReport("FeeCollectionReceipt_crescent.rpt", dcr.DcrNo, dcr.StudentId, "2");
                // this.ShowReport("FeeCollectionReceipt.rpt", dcr.DcrNo, dcr.StudentId, "1");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnReprint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CancelReceipt()
    {
        try
        {
            //string Receipt_Cancel_Auth = objCommon.LookUp("REFF", "RECEIPT_CANCEL", "");
            foreach (ListViewDataItem item in lvPaidReceipts.Items)
            {
                RadioButton rdoSelectRecord = item.FindControl("rdoSelectRecord") as RadioButton;

                Label lblReceiptDt = item.FindControl("lblReceiptDt") as Label;
                HiddenField hdDcr = item.FindControl("hidDcrNo") as HiddenField;
                string selectedDcr_no = Request.Form["Receipts"]; //hidDcrNo
                //if (rdoSelectRecord.Checked.Equals(true))
                //{
                DateTime dt = DateTime.Now;
                DateTime dateOnly = dt.Date;

                //if (Convert.ToDateTime(lblReceiptDt.Text) == dateOnly)
                //    {
                if (hdDcr.Value == selectedDcr_no)
                {
                    DailyCollectionRegister dcr = new DailyCollectionRegister();
                    //if (Session["userno"].ToString() == "282")
                    // if (Session["userno"].ToString() == Receipt_Cancel_Auth)
                    //{
                    if (this.GetRecieptData(ref dcr))
                    {
                        FeeCollectionController feeController = new FeeCollectionController();
                        if (feeController.CancelReceipt(dcr))
                        {
                            this.ShowMessage("Receipt has been cancelled successfully.");
                            lvPaidReceipts.DataSource = null;
                            lvPaidReceipts.DataBind();
                            lvPaidReceipts.Visible = false;
                            btnCancel.Disabled = true;
                            btnReprint.Disabled = true;
                            btnEdit.Disabled = true;
                            pnlmain.Visible = false;
                            txtRemark.Text = "";
                            //txtSearchText.Text = "";
                        }
                    }
                    return;
                    //}
                    //else
                    //{
                    //    this.ShowMessage("Receipt Can be cancelled only for Accounts User.");
                    //    return;
                    //}
                }
                else
                {
                    this.ShowMessage("Receipt Can be cancelled is not valid.");
                }
            }
        }
        // }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool GetRecieptData(ref DailyCollectionRegister dcr)
    {
        try
        {
            foreach (ListViewDataItem item in lvPaidReceipts.Items)
            {
                string strDcrNo = (item.FindControl("hidDcrNo") as HiddenField).Value;
                if (strDcrNo == Request.Form["Receipts"].ToString())
                {
                    dcr.DcrNo = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

                    string strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                    dcr.PaymentModeCode = (item.FindControl("hidPaymodecode") as HiddenField).Value;
                    dcr.StudentId = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);
                    // Session["CAN_RECP"] = (item.FindControl("hidcan") as HiddenField).Value;
                    dcr.UserNo = int.Parse(Session["userno"].ToString());
                    dcr.Remark = "This receipt has been canceled by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
                    dcr.Remark += txtRemark.Text.Trim();

                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.GetRecordIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return false;
    }

    //private void ShowReport(int dcrNo, int studentNo, string copyNo)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=Tuition Fee Receipt";
    //        //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2)
    //        //{
    //        //    url += "&path=~,Reports,Academic,ReprintFeeCollectionReceipt_crescent.rpt";
    //        //}
    //        //else
    //        //{
    //        url += "&path=~,Reports,Academic,FeeCollectionReceipt_crescent.rpt";
    //        //}

    //        url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
    //        divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
    //        divMsg.InnerHtml += " window.open('" + url + "','Tuition_Fee_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);} </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo, string username)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, copyNo);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void ShowReport_ForCash(string rptName, int dcrNo, int studentNo, string copyNo,string Username)
    //{
    //    try
    //    {
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=Fee_Collection_Receipt";
    //        url += "&path=~,Reports,Academic," + rptName;
    //        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, copyNo) + ",username=" + Session["username"].ToString();

    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() +
    //     "," + this.GetReportParameters(dcrNo, studentNo, copyNo);
    //        divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
    //        divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}


    private void ShowReport_ForCash(string rptName, int dcrNo, int studentNo, string copyNo, string Username, int Cancel)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) +
         "," + this.GetReportParameters(dcrNo, studentNo, copyNo);

            //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel
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


    //private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    //{
    //    /// This report requires nine parameters. 
    //    /// Main report takes three params and three subreport takes two
    //    /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
    //    /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
    //    /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
    //    string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,@CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
    //    param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
    //    param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
    //    param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
    //    return param;
    //}

    // 30-06-2014 
    //private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    //{
    //    /// This report requires nine parameters. 
    //    /// Main report takes three params and three subreport takes two
    //    /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
    //    /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
    //    /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
    //    /// ADD THE PARAMETER COLLEGE CODE
    //    string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
    //    if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 1)
    //    {
    //        param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
    //    }

    //    param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
    //    param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
    //    return param;

    //}

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        /// 

        ////string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        ////param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        ////param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        ////param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        ////return param;

        //string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt";
        return param;

    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    private void EditReceipt()
    {
        panelEditing.Visible = true;
        try
        {
            DailyCollectionRegister dcr = new DailyCollectionRegister();
            if (this.GetRecieptDataEdit(ref dcr))
            {
                //FeeCollectionController feeController = new FeeCollectionController();
                //if (feeController.CancelReceipt(dcr))
                //{
                //    this.ShowMessage("Receipt has been Update successfully.");
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    private bool GetRecieptDataEdit(ref DailyCollectionRegister dcr)
    {
        try
        {

            foreach (ListViewDataItem item in lvPaidReceipts.Items)
            {
                //This is accept the only radiobutton checked value
                strDcrNo = Request.Form["Receipts"].ToString();
                if (strDcrNo != null)
                //if ((item.FindControl("rdoSelect") as RadioButton).Checked == true)
                {
                    string DcrNo = strDcrNo;
                    //(item.FindControl("hidDcrNo") as HiddenField).Value;
                    DataSet dsRecord = objCommon.FillDropDown("ACD_DCR_TRAN", "DCRNO,DD_NO", "IDNO,DD_DT,DD_BANK,DD_CITY,DD_AMOUNT,BANKNO", "DCRNO = " + DcrNo, string.Empty);
                    if (dsRecord.Tables[0].Rows.Count > 0)
                    {
                        txtDDNo.Text = dsRecord.Tables[0].Rows[0]["DD_NO"].ToString();
                        txtDDAmount.Text = dsRecord.Tables[0].Rows[0]["DD_AMOUNT"].ToString();
                        txtDDDate.Text = dsRecord.Tables[0].Rows[0]["DD_DT"].ToString();
                        txtDDCity.Text = dsRecord.Tables[0].Rows[0]["DD_CITY"].ToString();
                        ddlBank.SelectedValue = dsRecord.Tables[0].Rows[0]["BANKNO"].ToString();
                        hfDcrNum.Value = dsRecord.Tables[0].Rows[0]["DCRNO"].ToString();
                        hfIdno.Value = dsRecord.Tables[0].Rows[0]["IDNO"].ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage("This is not DD receipt record", this.Page);
                        panelEditing.Visible = false;
                    }

                    //if (strDcrNo == Request.Form["Receipts"].ToString())
                    //{
                    //    dcr.DcrNo = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

                    //    string strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
                    //    dcr.StudentId = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);

                    //    dcr.UserNo = int.Parse(Session["userno"].ToString());
                    //    dcr.Remark = "This receipt has been canceled by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
                    //    dcr.Remark += txtRemark.Text.Trim();

                    //    return true;
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.GetRecordIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return false;
    }

    protected void btnDD_Info_Click(object sender, EventArgs e)
    {
        //Update the DD information
        FeeCollectionController feeController = new FeeCollectionController();

        int DCRNO = Convert.ToInt32(hfDcrNum.Value.Trim());
        string DDNO = txtDDNo.Text.Trim();
        string DDBANK = ddlBank.SelectedItem.Text.Trim();
        int BANKNO = Convert.ToInt32(ddlBank.SelectedValue.Trim());
        string DDCITY = txtDDCity.Text;
        DateTime DDDATE = Convert.ToDateTime(txtDDDate.Text.Trim());
        int IDNO = Convert.ToInt32(hfIdno.Value.Trim());
        string output = feeController.UpdateDDInfo(DCRNO, DDNO, DDDATE, DDBANK, DDCITY, BANKNO, IDNO);

        if (output != "-99")
        {
            objCommon.DisplayMessage("DD information Updated Successfully", this.Page);
        }
        DDclear();
    }
    protected void BtnCancelDD_Click(object sender, EventArgs e)
    {
        panelEditing.Visible = false;
        DDclear();
    }

    private void DDclear()
    {
        txtDDNo.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtDDCity.Text = string.Empty;
    }

    //protected void rdoReceiptNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lvPaidReceipts.DataSource = null;
    //        lvPaidReceipts.DataBind();
    //        lvPaidReceipts.Visible = false;
    //        btnCancel.Disabled = true;
    //        btnReprint.Disabled = true;
    //        btnEdit.Disabled = true;
    //        txtRemark.Text = string.Empty;
    //        txtSearchText.Text = string.Empty;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.rdoReceiptNo_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void rdoEnrollmentNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lvPaidReceipts.DataSource = null;
    //        lvPaidReceipts.DataBind();
    //        lvPaidReceipts.Visible = false;
    //        btnCancel.Disabled = true;
    //        btnReprint.Disabled = true;
    //        btnEdit.Disabled = true;
    //        txtRemark.Text = string.Empty;
    //        txtSearchText.Text = string.Empty;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.rdoEnrollmentNo_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
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
        if (ddlSearch.ToolTip == "1")// Is fee related fields
        {

            string fieldName = string.Empty;
            ////string searchText = "'" + txtSearchText.Text.Trim() + "'";
            string searchText = value;
            string errorMsg = string.Empty;

            if (ddlSearch.SelectedItem.Text == "Challan/Receipt No")
            {
                fieldName = "REC_NO";
                errorMsg = "having receipt no.:" + value;
            }
            //if (ddlSearch.SelectedItem.Text == "Challan/Receipt No")
            //{
            //    fieldName = "ENROLLNMENTNO";     //"IDNO"
            //    errorMsg = "for student having enrollment no.: " + value;
            //}

            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.FindReceipts(fieldName, value);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                lvPaidReceipts.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaidReceipts);//Set label - 
                btnCancel.Disabled = false;
                btnReprint.Disabled = false;
                btnEdit.Disabled = false;
            }
            else
            {
                //ShowMessage("No receipt found " + errorMsg);
                //lvPaidReceipts.Visible = false;
                //btnReprint.Disabled = true;
                //btnCancel.Disabled = true;

                ShowMessage("No receipt found " + errorMsg);
                lvPaidReceipts.DataSource = null;
                lvPaidReceipts.DataBind();
                lvPaidReceipts.Visible = false;
                btnCancel.Disabled = true;
                btnReprint.Disabled = true;
                btnEdit.Disabled = true;
            }
            txtRemark.Text = string.Empty;

        }
        else
        {
            bindlist(ddlSearch.SelectedItem.Text, value);
            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();
        }
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
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
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            lvPaidReceipts.DataSource = null;
            lvPaidReceipts.DataBind();

            txtSearch.Text = String.Empty;
            ddlDropdown.SelectedIndex = 0;

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
                if (ViewState["dssearch"] != null)
                {
                    DataSet dsDDL = ViewState["dssearch"] as DataSet;
                    for (int i = 0; i < dsDDL.Tables[0].Rows.Count; i++)
                    {
                        ddlSearch.Items[i + 1].Attributes.Add("title", dsDDL.Tables[0].Rows[i][3].ToString());
                        if (ddlSearch.SelectedValue == dsDDL.Tables[0].Rows[i][0].ToString())
                        {
                            ddlSearch.ToolTip = dsDDL.Tables[0].Rows[i][3].ToString();
                            return;
                        }

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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            StudentRegist objSR = new StudentRegist();
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            //Response.Redirect(url + "&id=" + lnk.CommandArgument);
            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            Session["stuinfoidno"] = idno;
            objSR.IDNO = idno;
            ViewState["idno"] = Session["stuinfoidno"].ToString();
            int Organizationid = Convert.ToInt32(Session["OrgId"]);
            //HiddenField hdfcrno = lnk.Parent.FindControl("hdfcrno") as HiddenField;
            //Session["DRCNO"] = hdfcrno.Value;


            string fieldName = string.Empty;
            ////string searchText = "'" + txtSearchText.Text.Trim() + "'";
            string searchText = idno.ToString();// lblenrollno.Text.Trim();
            string errorMsg = string.Empty;

            fieldName = "IDNO";

            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.FindReceipts(fieldName, searchText);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                lvPaidReceipts.Visible = true;
                btnCancel.Disabled = false;
                btnReprint.Disabled = false;
                //btnEdit.Disabled = false;
                lvStudent.Visible = false;
                pnlmain.Visible = true;
                divfooter.Visible = true;
                // pnldetails.Visible = true;
                //panelEditing.Visible = true;
            }
            else
            {
                lvPaidReceipts.DataSource = ds;
                lvPaidReceipts.DataBind();
                lvPaidReceipts.Visible = false;
                btnCancel.Disabled = true;
                btnReprint.Disabled = true;
                btnEdit.Disabled = true;
                lvStudent.Visible = true;
                objCommon.DisplayUserMessage(pnlFeeTable, "No receipt found " + errorMsg, this.Page);
            }
            txtRemark.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ReprintReceipts.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName, string Username)
    {
        try
        {

            int dcrno = 0;
            foreach (ListViewDataItem dataitem in lvPaidReceipts.Items)
            {
                HiddenField hfdcrno = dataitem.FindControl("hidDcrNo") as HiddenField;
                RadioButton rdb = dataitem.FindControl("rdoSelectRecord") as RadioButton;
                //if (rdb.Checked == true)
                //    {
                dcrno = Convert.ToInt32(hfdcrno.Value);
                //}
            }
            int SemesterNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"])));
            int DCRNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(SemesterNo + 1)));

            // SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["stuinfoidno"] + ",@P_DCRNO=" + dcrno + "@P_UA_NAME=" + Session["username"].ToString();

            //   url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() +
            //"," + this.GetReportParameters(dcrNo, studentNo, copyNo);
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + this.GetReportParameters(dcrNo, studentNo, "");

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
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

    private void ShowReport_MIT(string reportTitle, string rptFileName, string UANAME)
    {
        try
        {


            int dcrno = 0;
            foreach (ListViewDataItem dataitem in lvPaidReceipts.Items)
            {
                HiddenField hfdcrno = dataitem.FindControl("hidDcrNo") as HiddenField;
                RadioButton rdb = dataitem.FindControl("rdoSelectRecord") as RadioButton;
                //if (rdb.Checked == true)
                //    {
                dcrno = Convert.ToInt32(hfdcrno.Value);
                //}
            }
            //int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + ",@P_DCRNO=" + dcrno + ",@P_UA_NAME=" + Session["UAFULLNAME"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
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

    private void ShowReport_ForCash_PCEN(string rptName, int dcrNo, int studentNo, string copyNo, string UA_FULLNAME, int Cancel)
    {
        try
        {
            int dcrno = 0;
            foreach (ListViewDataItem dataitem in lvPaidReceipts.Items)
            {
                HiddenField hfdcrno = dataitem.FindControl("hidDcrNo") as HiddenField;
                RadioButton rdb = dataitem.FindControl("rdoSelectRecord") as RadioButton;
                //if (rdb.Checked == true)
                //    {
                dcrno = Convert.ToInt32(hfdcrno.Value);
                //}
            }
            int SemesterNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"])));
            //int DCRNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + " AND SEMESTERNO=" + Convert.ToInt32(SemesterNo + 1)));

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + studentNo + ",@P_DCRNO=" + dcrNo + ",@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]);


            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["UAFULLNAME"].ToString() +
            //"," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(Session["IDNO"].ToString(), studentNo, "0");
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport_ForCash_HITS(string rptName, int dcrNo, int studentNo, string copyNo, string UA_FULLNAME, int Cancel)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            int college_id = 0;
            college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(studentNo)));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + college_id.ToString() + ",@P_IDNO=" + studentNo + ",@P_DCRNO=" + dcrNo + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]);



            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["UAFULLNAME"].ToString() +
            //"," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(Session["IDNO"].ToString(), studentNo, "0");
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}


