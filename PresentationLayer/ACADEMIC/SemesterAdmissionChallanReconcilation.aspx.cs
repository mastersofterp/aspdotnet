using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Data;




public partial class ACADEMIC_SemesterAdmissionChallanReconcilation : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ChalanReconciliationController crController = new ChalanReconciliationController();

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
                    PopulateDropDown();
                    //BindListView();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        }

                    //objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 and ActiveStatus=1", "COLLEGE_ID");
                    //objCommon.FillListBox(lstbxSchool, "ACD_COLLEGE_MASTER ", "COLLEGE_ID", "COLLEGE_NAME ", "COLLEGE_ID>0 and ActiveStatus=1", "COLLEGE_ID ");
                    }
                }

            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=SemesterAdmissionChallanReconcilation.aspx");
                }
            }
        else
            {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SemesterAdmissionChallanReconcilation.aspx");
            }
        }
    #endregion

    private void PopulateDropDown()
        {

        try
            {
            //Fill Dropdown Session 
            //objCommon.FillListBox(lstbxSchool, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "SESSIONNO DESC");

            AcademinDashboardController objADEController = new AcademinDashboardController();
            DataSet ds = objADEController.Get_College_Session(1, Session["college_nos"].ToString());
            ViewState["CollegeId"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
                {
                lstbxSchool.SelectedValue = "";
                lstbxSchool.DataSource = ds;
                lstbxSchool.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstbxSchool.DataTextField = ds.Tables[0].Columns[4].ToString();
                lstbxSchool.DataBind();
                lstbxSchool.SelectedIndex = 0;
                }
            //ddlSession.Items.Clear();
            //ddlSession.Items.Add("Please Select");
            //ddlSession.SelectedItem.Value = "0";




            //if (ds.Tables.Count > 0)
            //    {
            //    if (ds.Tables[0].Rows.Count > 0)
            //        {
            //        ddlDegree.DataTextField = "DEGREENAME";
            //        ddlDegree.DataValueField = "DEGREENO";
            //        ddlDegree.ToolTip = "DEGREENO";
            //        ddlDegree.DataSource = ds.Tables[0];
            //        ddlDegree.DataBind();
            //        }

            //    }

            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC"); //ISNULL(FLOCK,0)=1
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        //Refresh Page url  
        Response.Redirect(Request.Url.ToString());
        }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
        //if (lstbxSchool.s != "")
        //    {
            //this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(lstbxSchool.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            ddlSession.Focus();
            }
        //else
        //    {
        //    ddlSession.Items.Clear();
        //    ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //    }
        //lvChallan.DataSource = null;
        //lvChallan.DataBind();
        //lvChallan.Visible = false;
        //}

    private void BindListView()
        {
        try
            {

            string sessionnos = "";
            foreach (ListItem items in lstbxSchool.Items)
                {
                if (items.Selected == true)
                    {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    sessionnos += items.Value + ',';
                    }
                }
            if (sessionnos != "")
                {
                sessionnos = sessionnos.Remove(sessionnos.Length - 1);
                }
            ViewState["SessionNos"] = sessionnos;

            AcdAttendanceController acdatt = new AcdAttendanceController();
            DataSet dsCollegeids = acdatt.getselectedcollegewisecollegeid(ViewState["SessionNos"].ToString());
            string collegeidnos = string.Empty;
            //DataSet dscollege = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT(COLLEGE_ID)", "", "SESSIONNO IN (" + ViewState["SessionNos"].ToString() + ")", "COLLEGE_ID");
            if (dsCollegeids.Tables.Count > 0)
                {
                if (dsCollegeids.Tables[0].Rows.Count > 0)
                    {
                    collegeidnos = dsCollegeids.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    }
                //dsCollegeids.Tables[0].Columns[1].ToString();

                }



            ChalanReconciliationController crController = new ChalanReconciliationController();
            DataSet ds = crController.BindAllChallanReconNew(collegeidnos, sessionnos, Convert.ToInt32(ddlPaymode.SelectedValue.ToString()), Convert.ToInt32(ddlStatus.SelectedValue.ToString()));

            if (ds.Tables[0].Rows.Count > 0)
                {
                lvChallan.DataSource = ds;
                lvChallan.DataBind();
                lvChallan.Visible = true;
                pnlChallan.Visible = true;

                }
            else
                {
                lvChallan.Visible = false;
                lvChallan.DataSource = null;
                lvChallan.DataBind();

                objCommon.DisplayUserMessage(pnlFeeTable, "Data Not Found...!", this);
                }


            //foreach (ListViewDataItem item in lvChallan.Items)
            //    {

            //    ImageButton btnprint = item.FindControl("btnPrintReceipt") as ImageButton;

            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //        //foreach (DataRow dr in ds.Tables[0].Rows)
            //            {

            //            string ConfStatus = ds.Tables[0].Rows[i]["APPROVED_STATUS"].ToString();


            //            if (ConfStatus == "PENDING")
            //                {
            //                btnprint.Enabled = false;
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKeynew", "$('#printrec').hide();$('td:nth-child(19)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#printrec').hide();$('td:nth-child(19)').hide();});", true); //For Hide Status Column for 

            //                // btnPrintReceipt.ena
            //                //ScriptManager.RegisterStartupScript(this, GetType(), "myfun1", "$('#printrec').hide();$('td:nth-child(19)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#printrec').hide();$('td:nth-child(19)').hide();});", true);

            //                }
            //            }
            //        }
            //    }   
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.ViewChalan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }

        }
    protected void btnShow_Click(object sender, EventArgs e)
            {
        try
            {
            BindListView();
            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ChalanReconciliation.ViewChalan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }

        }
    protected void imgbtnPrevDoc_Click(object sender, ImageClickEventArgs e)
        {
        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var ImageName = img;
        ImageButton lnkView = (ImageButton)(sender);
        string urlpath = System.Configuration.ConfigurationManager.AppSettings["paymentslip"].ToString();
        iframeView.Src = urlpath + ImageName;
        mpeViewDocument.Show();

        //objCommon.DisplayMessage(this.Page, "successs", this.Page);
        }
    protected void chkCourseno_CheckedChanged(object sender, EventArgs e)
        {

        }
    #region not in use
    //protected void lnkId_Click(object sender, EventArgs e)
    //    {
    //    LinkButton lnk = sender as LinkButton;
    //    Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);

    //    //FeeCollectionController feeController = new FeeCollectionController();      
    //    //string fieldName = string.Empty;
    //    //string errorMsg = string.Empty;
    //    //string value=string.Empty;
    //    ////DataSet ds = feeController.FindReceipts(fieldName, value);
    //    //fieldName = "IDNO";
    //    //value = (Session["stuinfoidno"].ToString());
    //    //DataSet ds = crController.FindChalan(fieldName, value);
    //    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    //    {
    //    //    lvSearchResults.DataSource = ds;
    //    //    lvSearchResults.DataBind();
    //    //    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSearchResults);//Set label - 
    //    //    lvSearchResults.Visible = true;
    //    //   // btnDelete.Disabled = false;
    //    //    //btnReconcile.Disabled = false;
    //    //    //divRemark.Visible = true;
    //    //    //btnReconcile.Visible = true;
    //    //    //btnDelete.Visible = true;
    //    //    }
    //    //else
    //    //    {
    //    //    objCommon.DisplayUserMessage(pnlFeeTable, "No Chalan found " + errorMsg, this.Page);
    //    //    lvSearchResults.Visible = false;
    //    //    //btnReconcile.Disabled = true;
    //    //    //btnDelete.Disabled = true;
    //    //    //divRemark.Visible = false;
    //    //    }
    //    }
    #endregion
    protected void btnApprove_Click(object sender, EventArgs e)
        {
        try
            {
             DailyCollectionRegister dcr = new DailyCollectionRegister();
             int Status = 0;
             int Count = 0;
             if (ddlStatus.SelectedIndex > 0)
                 {
                 Status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                 }
            foreach (ListViewDataItem dataitem in lvChallan.Items)
                {
                CheckBox chkapproved = dataitem.FindControl("chkapproved") as CheckBox;
                DropDownList ddlstatuslv = dataitem.FindControl("ddlStatuslv") as DropDownList;
                Label lblrecno = dataitem.FindControl("lblRecDT") as Label;
                Label recdt = dataitem.FindControl("lblRecDT") as Label;
                TextBox txtremark = dataitem.FindControl("txtRemark") as TextBox;
                HiddenField hdnTotalAmount = dataitem.FindControl("hdnTotalAmount") as HiddenField;
                TextBox txtAmount = dataitem.FindControl("txtAmount") as TextBox;
                HiddenField hfinstallment = dataitem.FindControl("hdninstallmentno") as HiddenField;     

               // string strDcrNo = (dataitem.FindControl("hidDcrNo") as HiddenField).Value;
                int DcrNo=0;
                HiddenField strDcrNo = dataitem.FindControl("hidDcrNo") as HiddenField;
                HiddenField idno = dataitem.FindControl("hidIdNo") as HiddenField;

                string Colleges = "";

                foreach (ListItem items in lstbxSchool.Items)
                    {
                    if (items.Selected == true)
                        {
                        //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        Colleges += items.Value + ',';
                        }
                    }
                Colleges = Colleges.Remove(Colleges.Length - 1);



                if (chkapproved.Checked == true && txtremark.Text!=string.Empty && chkapproved.Enabled==true )
                    {
                    Count++;                    
                    dcr.DcrNo = Convert.ToInt32(strDcrNo.Value);
                    int semesterno = Convert.ToInt32((dataitem.FindControl("hidDcrSemesterNo") as HiddenField).Value);
                    dcr.SemesterNo = semesterno;
                    dcr.Remark = txtremark.Text.ToString();
                    dcr.ReceiptDate = Convert.ToDateTime(recdt.Text.ToString());
                    dcr.StudentId = Convert.ToInt32(idno.Value.ToString());
                    decimal RecievedAmount=0;
                   
                    if (chkapproved.Enabled == true && chkapproved.Checked == true)
                        {

                        if (ddlstatuslv.SelectedIndex > 0)
                            {
                            Status = Convert.ToInt32(ddlstatuslv.SelectedValue.ToString());
                            }
                        if (Status == 0)
                            {
                            objCommon.DisplayMessage(this.Page, "Please Select Status.", this.Page);
                            return;
                            }
                        RecievedAmount = Convert.ToDecimal(txtAmount.Text);
                        int installno=0;
                        installno = Convert.ToInt32(hfinstallment.Value);
                        //if (this.GetRecieptData(Convert.ToInt32(DcrNo)))
                        //    {
                        if (crController.ReconcileChalanNew(dcr, Status, RecievedAmount, installno))
                            {
                            string enroll = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(dcr.StudentId));
                            //this.ShowMessage("Chalan reconciled successfully.");
                            objCommon.DisplayMessage(this.Page, "Challan reconciled successfully.", this.Page);

                            //HideControles();
                            //divRemark.Visible = false;
                            //ddlSearch.SelectedIndex = 0;
                            //lblNoRecords.Visible = false;
                            //btnReconcile.Visible = false;
                            //btnDelete.Visible = false;
                           // ClearControl();
                            }
                        else
                            {
                            objCommon.DisplayMessage(this.Page, "Unable to complete the operation.", this.Page);
                            // this.ShowMessage("Unable to complete the operation.");
                            // HideControles();
                            }
                        }
                    
                       // }
                    }
                else if (chkapproved.Checked == true &&  txtremark.Text == string.Empty)
                    {
                    objCommon.DisplayMessage(this.Page, "Please Enter Remark for Approval.", this.Page);
                    return;
                    }
                }
            if (Count == 0)
                {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student For Approval.", this.Page);
                return;
                //this.ShowMessage("");
                }
            ClearControl();
            }
        catch (Exception ex)
            {

            }
        }
    //private bool GetRecieptData(int DcrNo)
    //    {
    //    try
    //        {
    //        foreach (ListViewDataItem item in lvSearchResults.Items)
    //            {
    //            string strDcrNo = (item.FindControl("hidDcrNo") as HiddenField).Value;
    //            int semesterno = Convert.ToInt32((item.FindControl("hidDcrSemesterNo") as HiddenField).Value);
    //            /// "Receipts" is a redio button list name. Request.Form["Receipts"] contains
    //            /// the value of selected radio button. it is a replacement of radio.checked.
    //            if (strDcrNo == Request.Form["Receipts"].ToString())
    //                {
    //                dcr.DcrNo = ((strDcrNo != null && strDcrNo != string.Empty) ? int.Parse(strDcrNo) : 0);

    //                string strIdNo = (item.FindControl("hidIdNo") as HiddenField).Value;
    //                dcr.StudentId = ((strIdNo != null && strIdNo != string.Empty) ? int.Parse(strIdNo) : 0);

    //                dcr.ReceiptDate = DateTime.Today;
    //                dcr.UserNo = int.Parse(Session["userno"].ToString());
    //                dcr.SemesterNo = semesterno;
    //                if (Request.Params["__EVENTTARGET"].ToString() == "ReconcileChalan")
    //                    dcr.Remark = "This chalan has been reconciled by " + Session["userfullname"].ToString() + ". ";
    //                else if (Request.Params["__EVENTTARGET"].ToString() == "DeleteChalan")
    //                    dcr.Remark = "This receipt has been deleted by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";

    //               // dcr.Remark += txtRemark.Text.Trim();
    //                return true;
    //                }
    //            }
    //        }
    //    catch (Exception ex)
    //        {
    //        throw;
    //        }
    //    return false;
    //    }
    private void ShowMessage(string msg)
        {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert(\"" + msg + "\"); </script>";
        }
    public void ClearControl()
        {

        lstbxSchool.ClearSelection();
        ddlSession.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlPaymode.SelectedIndex = 0;
        lvChallan.DataSource = null;
        lvChallan.DataBind();
        lvChallan.Visible = false;
        }

    protected void lvChallan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {


        DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatuslv");
        HiddenField hdnRemark = (HiddenField)e.Item.FindControl("hdnRemark");
        HiddenField hdnStatus = (HiddenField)e.Item.FindControl("hdnStatus");
        CheckBox chkapproved = (CheckBox)e.Item.FindControl("chkapproved");
        TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
        TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");

        HiddenField hdnTotalAmount = (HiddenField)e.Item.FindControl("hdnTotalAmount");

        if (hdnStatus.Value == "APPROVED")
            {

            ddlStatus.SelectedIndex = 1;
            txtRemark.Text = hdnRemark.Value;
            txtAmount.Text = hdnTotalAmount.Value.ToString();
            chkapproved.Checked = true;
            chkapproved.Enabled = false;
            ddlStatus.Enabled = false;
            txtRemark.Enabled = false;
            txtAmount.Enabled = false;
            }
        txtRemark.Text = hdnRemark.Value;
        }
    protected void ddlPaymode_SelectedIndexChanged(object sender, EventArgs e)
        {
        lvChallan.DataSource = null;
        lvChallan.DataBind();
        lvChallan.Visible = false;
        ddlStatus.SelectedIndex = 0;
        }
    //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //    lvChallan.DataSource = null;
    //    lvChallan.DataBind();
    //    lvChallan.Visible = false;
    //    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        {
        lvChallan.DataSource = null;
        lvChallan.DataBind();
        lvChallan.Visible = false;
        }
    protected void btnPrintReceipt_Click(object sender, ImageClickEventArgs e)
        {
        int StudId = Convert.ToInt32(Session["idno"]);
       
        ImageButton btnPrint = sender as ImageButton;
        int IDNO = Convert.ToInt32(btnPrint.ToolTip);
        Session["idno"] = IDNO;
        Session["DCRNO"] = int.Parse(btnPrint.CommandArgument);
        Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
       // Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;
      //  int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        if (Convert.ToInt32(Session["OrgId"]) == 8)
            {
            ShowReport("Semester Registration", "FeeCollectionReceiptForCash_MIT.rpt");
            }
        else if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4"))
            {
            this.ShowReport_ForCash("FeeCollectionReceiptForCash_cpukota.rpt", Session["UAFULLNAME"].ToString(),Convert.ToInt32(Session["CAN_REC"]));
            }
        else
            {
                this.ShowReport_ForCash("FeeCollectionReceiptForCash.rpt", Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CAN_REC"]));
            }

        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "$('#idremark').hide();$('td:nth-child(11)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#idremark').hide();$('td:nth-child(11)').hide();});", true);
        //this.ShowReport("SemesterRegistrationMIT.rpt", Convert.ToInt32(StudId), Convert.ToInt32(SemesterNo),(DcrNO.ToString()));
        }
    private void ShowReport(string reportTitle, string rptFileName)
        {
        try
            {
           // int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"] + ",@P_DCRNO=" + Session["DCRNO"];
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
    private void ShowReport_ForCash(string rptName, string UA_FULLNAME, int Cancel)
        {
        try
            {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Session["idno"] + ",@P_DCRNO=" + Session["DCRNO"] + "," + "@P_UA_NAME=" + Session["username"].ToString() +","+ "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

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

    
