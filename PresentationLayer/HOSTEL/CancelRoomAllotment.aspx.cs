//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : CANCEL ROOM ALLOTMENT                                                
// CREATION DATE : 26-NOV-2010                                                          
// CREATED BY    : GAURAV SONI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Hostel_CancelRoomAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    RoomAllotmentController raController = new RoomAllotmentController();
    RoomAllotment objRM = new RoomAllotment();

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
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    
                    //Fill Session Dropdown
                    objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
                    ddlSession.SelectedIndex = 1;
                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                       // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
            else
            {
                if (Request.Params["__EVENTTARGET"]!=null && 
                    Request.Params["__EVENTTARGET"].ToString() != "" &&
                    Request.Params["__EVENTTARGET"].ToString() == "CancelRoomAllotment")
                {
                    this.CancelRoomAllotment();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Hostel_CancelRoomAllotment.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Hostel_CancelRoomAllotment.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string studentId = string.Empty;
        try
        {
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                FeeCollectionController feeController = new FeeCollectionController();
                //studentId = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_STUDENT S ON (S.IDNO=H.RESIDENT_NO)", "H.RESIDENT_NO", "H.HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND S.REGNO='" + txtEnrollNo.Text.Trim() + "'");
                studentId = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_STUDENT S ON (S.IDNO=H.RESIDENT_NO)", "H.RESIDENT_NO", "H.CAN=0 AND H.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.REGNO='" + txtEnrollNo.Text.Trim() + "' OR S.ROLLNO='" + txtEnrollNo.Text.Trim() + "'");
                
                if (studentId !="")
                {
                    this.DisplayInformation(Convert.ToInt32(studentId));
                    DisplayRefundFeeInfo(Convert.ToInt32(studentId));
                }
                else
                {
                    string check=string.Empty;
                    check = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "RESIDENT_NO", "HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND CAN=1 AND RESIDENT_NO=(SELECT IDNO FROM ACD_STUDENT where regno='"+txtEnrollNo.Text.Trim()+"')");
                    if (check != "")
                    {
                        objCommon.DisplayMessage("Admission already canceled, you can generate report only", this.Page);
                        //
                        studentId = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtEnrollNo.Text.Trim()+"'");
                        //this.DisplayInformation(Convert.ToInt32(studentId));
                        btnSlip.Visible = true;
                        btnCancel.Visible = false;
                    }
                    else
                        objCommon.DisplayMessage("No Student Found Having Reg. No.: " + txtEnrollNo.Text.Trim() + " in Session " + ddlSession.SelectedItem.Text, this.Page);
                }
            }
            else
                objCommon.DisplayMessage("Please Enter Regisration No.",this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void DisplayRefundFeeInfo(int idno)
    {
        DataSet ds = null;
        int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        ds = raController.GetHostelFeeRefundInfo(idno, Convert.ToInt32(ddlSession.SelectedValue), OrganizationId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            trRefund.Visible = true;
            lblBankName.Text = ds.Tables[0].Rows[0]["BANK"].ToString();
            lblBAccNo.Text = ds.Tables[0].Rows[0]["ACC_NO"].ToString();
            lblBBranch.Text = ds.Tables[0].Rows[0]["BANK_BRANCH"].ToString();
            lblHostelPaid.Text = ds.Tables[0].Rows[0]["HOSTEL_AMT"].ToString();
            lblMessPaid.Text = ds.Tables[0].Rows[0]["MESS_AMT"].ToString();
            lblHostel.Text = ds.Tables[0].Rows[0]["HOSTEL"].ToString();
            lblMess.Text= ds.Tables[0].Rows[0]["MESS_NAME"].ToString();
            lblAllotDate.Text =Convert.ToDateTime(ds.Tables[0].Rows[0]["ALLOTMENT_DATE"].ToString()).ToString("dd-MMM-yyyy");
            txtMessUse.Text = ds.Tables[0].Rows[0]["MESS_USE"].ToString();
            lblTotalPaid.Text = Convert.ToString(Convert.ToDecimal(lblHostelPaid.Text) + Convert.ToDecimal(lblMessPaid.Text));
            hfdTotalPaid.Value = lblTotalPaid.Text;
            txtHAmt.Text = ds.Tables[0].Rows[0]["HOSTEL_AMT"].ToString();
            lblStudName.ToolTip = idno.ToString();
            //No. of Days
            DateTime start = Convert.ToDateTime(ds.Tables[0].Rows[0]["ALLOTMENT_DATE"].ToString());
            int days = DateTime.Now.Subtract(start).Days;
            txtMday.Text = Convert.ToString(days);

            // Refund Rules 
            // 7 days -->10% of Hostel Fee, 1 month-->25% of Hostel Fee, Above 1 month --> No refund 
            if (days <= 7) txtPerc.Text = "10";
            else if (days > 7 && days < 31) txtPerc.Text = "25";
            else txtPerc.Text = "100";
            
            var script = "HostelFee();";
            ClientScript.RegisterStartupScript(typeof(Page), "HostelFee", script, true);
            trRefund.Visible = true;
        }

    }

    private void DisplayInformation(int residentNo)
    {
        try
        {
            
            DataSet ds = null;
            /// Display student's personal and academic data in 
            /// student information section
            //FeeCollectionController feeController = new FeeCollectionController();
            //passing OrganizationId for data Shubham 
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            ds = raController.GetStudentInfoById(residentNo, OrganizationId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                /// show student information
                this.PopulateStudentInfoSection(dr);
            }
            else
            {
                ShowMessage("Unable to retrieve student's record.");
                return;
            }

            // Save resident no. for further transaction
            ViewState["ResidentNo"] = residentNo;

            /// Show room allotment details (if exists) for the student
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            ds = raController.GetRoomAllotmentInfoByResidentNo(residentNo, OrganizationId,sessionno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvAllotmentDetails.DataSource = ds;
                lvAllotmentDetails.DataBind();
                divRemark.Visible = true;
                btnCancelRoom.Visible = true;
                btnReport.Visible = true;
                btnCancel.Visible = true;
                /// save room allotment no to be used while saving the record
                ViewState["RoomAllotmentNo"] = ds.Tables[0].Rows[0]["ROOM_ALLOTMENT_NO"].ToString();
            }
            else
            {
                divRemark.Visible = false;
                btnCancelRoom.Visible = false;
                btnCancel.Visible = false;
                btnReport.Visible = false;
                this.ShowMessage("No room allotment found for this student.");
                btnCancel.Visible = true;
                btnCancel.Text = "Back";
            }

            /// Hide search student div and student list so that
            /// once a student's data is populated, user should not select
            /// other student to show information, unless and until user 
            /// clicks the cancel button.
            divStudentSearch.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.DisplayStudentInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            /// Bind data with labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();

            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the record.
            ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();

            divStudInfo.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void CancelRoomAllotment()
    {
       
    }

    private void ShowRoomAllomentDetails(int roomAllotmentNo)
    {
        try
        {
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            if (roomAllotmentNo > 0)
            {
                lvAllotmentDetails.DataSource = raController.GetRoomAllotmentByNo(roomAllotmentNo);
                lvAllotmentDetails.DataBind();
            }
            else
            {
                lvAllotmentDetails.DataSource = raController.GetAllRoomAllotments(OrganizationId);
                lvAllotmentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.ShowAllRoomAlloments() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
   
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString().Trim() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    private void ShowReport()
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Room_Allotment_Cancel_Slip";
            url += "&path=~,Reports,Hostel,rptRoomCancelReceipt.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ROOM_ALLOTMENT_NO=" + GetViewStateItem("RoomAllotmentNo") + ",@P_IDNO=" + GetViewStateItem("ResidentNo") + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','Cancel_Room_Allotment_Slip','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.btnSlip_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancelRoom_Click(object sender, EventArgs e)
    {
         try
        {
            int raNo = (GetViewStateItem("RoomAllotmentNo") != string.Empty ? int.Parse(GetViewStateItem("RoomAllotmentNo")) : 0);
            int CancelHostel = 0;
            string remark = "This room allotment has been canceled by " + Session["userfullname"].ToString()+". ";
            remark += txtRemark.Text.Trim();
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            if (txtPerc.Text != "") objRM.Percentage = Convert.ToDecimal(txtPerc.Text);
            if(txtHostelAmount.Text!="") objRM.Hostel_Charge=Convert.ToDecimal(txtHostelAmount.Text);
            if(txtMCharge.Text!="") objRM.PerDayCharge=Convert.ToDecimal(txtMCharge.Text);
            if(txtMessAmount.Text!="") objRM.MessCharge=Convert.ToDecimal(txtMessAmount.Text);
            if(txtMessUse.Text!="") objRM.MessMonthCharge=Convert.ToDecimal(txtMessUse.Text);
            if(txtOtherCharge.Text!="") objRM.OtherCharge=Convert.ToDecimal(txtOtherCharge.Text);
            if(txtRefundAmt.Text != "") objRM.RefundAmt = Convert.ToDecimal(txtRefundAmt.Text);
            if(txtTotal.Text != "") objRM.TotalAmt = Convert.ToDecimal(txtTotal.Text);
            if (txtMday.Text != "") objRM.Days = Convert.ToInt32(txtMday.Text);
            if (txtChequeNo.Text != "") objRM.ChequeNo = txtChequeNo.Text;
            if (txtCDate.Text != "") objRM.ChequeDate = Convert.ToDateTime(txtCDate.Text); 
            objRM.UserNo = Convert.ToInt32(Session["userno"].ToString());
            objRM.HostelSessionNo = Convert.ToInt32(ddlSession.SelectedValue);
           // objRM.ResidentNo = Convert.ToInt32(lblStudName.ToolTip);
            //string studentId = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_STUDENT S ON (S.IDNO=H.RESIDENT_NO)", "H.RESIDENT_NO", "H.CAN=0 AND H.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.REGNO='" + txtEnrollNo.Text.Trim() + "'");
            string studentId = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_STUDENT S ON (S.IDNO=H.RESIDENT_NO)", "H.RESIDENT_NO", "H.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.REGNO='" + txtEnrollNo.Text.Trim() + "' OR S.ROLLNO='" + txtEnrollNo.Text.Trim() + "'");
               
            objRM.ResidentNo = Convert.ToInt32(studentId);
            objRM.IP_Address = Request.ServerVariables["REMOTE_HOST"].ToString();
            objRM.CollegeCode = Session["colcode"].ToString();

            if (chkCancelHostel.Checked) //// Added for solution BUG_ID 164055 
            {
                CancelHostel = 1; 
            }

            if (raController.CancelRoomAllotment(objRM, raNo, remark,CancelHostel, OrganizationId))
            {
                if (CancelHostel == 1) 
                {
                    objCommon.DisplayMessage("Hostel Room And Registration Cancel Successfully.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage("Hostel Room Cancel Successfully.", this.Page);
                }
                //this.ShowReport();
                this.DisplayInformation(Convert.ToInt32(studentId));
                btnSlip.Visible = true;
                btnCancelRoom.Visible  = false;
            }
            else
                this.ShowMessage("Unable to cancel room allotment.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport();
    }
    protected void btnSlip_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_HOSTEL_ROOM_ALLOTMENT", "ROOM_NO, ROOM_ALLOTMENT_NO", "RESIDENT_NO", "HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CAN=1 AND RESIDENT_NO=(SELECT IDNO FROM ACD_STUDENT where regno='" + txtEnrollNo.Text.Trim() + "')", "RESIDENT_NO");
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Room_Allotment_Cancel_Slip";
            url += "&path=~,Reports,Hostel,rptRoomCancelReceipt.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ROOM_ALLOTMENT_NO=" + ds.Tables[0].Rows[0]["ROOM_ALLOTMENT_NO"].ToString() + ",@P_IDNO=" + ds.Tables[0].Rows[0]["RESIDENT_NO"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','Cancel_Room_Allotment_Slip','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_CancelRoomAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}