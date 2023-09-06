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
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class HOSTEL_GuestRoomAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    RoomAllotmentController raController = new RoomAllotmentController();
    GuestInfoController guestcon = new GuestInfoController(); // methods of stud day wise room allotment are here (by sonali on 20/01/2023)

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

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    /// Fill Dropdown lists                
                    ViewState["action"] = null;
                
                }
                string date = Convert.ToString(DateTime.Now);
                this.objCommon.FillDropDownList(ddlGuestList, "ACD_HOSTEL_GUEST_INFO", "GUEST_NO", "GUEST_NAME", "TO_DATE>=Convert(nvarchar(20),' " + Convert.ToDateTime(date).ToString("yyyy/MM/dd") + " ',103)", "GUEST_NAME");
                this.objCommon.FillDropDownList(ddlResidentType, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "IS_STUDENT=0", string.Empty);
                this.objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
                objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");

                int check = Convert.ToInt32(this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "HostelDayWise_Booking", "OrganizationId=" + Session["OrgId"] + ""));
                if (check != 1)
                {
                    //objCommon.DisplayMessage(this.updRoomAllot, "Sorry You Have Not Access of this Page !", this.Page);
                    //return;

                    divStudentSelect.Visible = false;
                }

                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                ddlSearch.SelectedIndex = 0;
                this.PopulateDropDown();

                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowGuest_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string check = string.Empty;
            ds = objCommon.FillDropDown("ACD_HOSTEL_GUEST_INFO", "GUEST_NO", "RESIDENT_TYPE_NO,GUEST_NAME,GUEST_ADDRESS,CONTACT_NO,PURPOSE,COMPANY_NAME,COMPANY_ADDRESS,COMPANY_CONTACT_NO,FROM_DATE,TO_DATE", "GUEST_NO=" + Convert.ToInt32(ddlGuestList.SelectedValue), "GUEST_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                divGuestInfo.Visible = true;
                //divRoomAllotment.Visible = true;
                //divSelectRoom.Visible = true;
                //UpdatePanel1.Visible = true;
                divButtonEvent.Visible = true;
                lblGuestName.Text = ds.Tables[0].Rows[0]["GUEST_NAME"].ToString();
                lblGuestName.ToolTip = ds.Tables[0].Rows[0]["GUEST_NO"].ToString();
                lblGuestAdd.Text = ds.Tables[0].Rows[0]["GUEST_ADDRESS"].ToString();
                lblGuestContact.Text = ds.Tables[0].Rows[0]["CONTACT_NO"].ToString();
                lblGuestPurpose.Text = ds.Tables[0].Rows[0]["PURPOSE"].ToString();
                ViewState["action"] = "add";
                lvAllotmentDetails.DataSource = raController.GetGuestRoomAllot(Convert.ToInt32(ddlGuestList.SelectedValue));
                lvAllotmentDetails.DataBind();
                //check = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "RESIDENT_NO", "CAN=0 AND RESIDENT_TYPE_NO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["RESIDENT_TYPE_NO"].ToString()) + " AND RESIDENT_NO=" + Convert.ToInt32(lblGuestName.ToolTip));
                //if (check != "")
                //{
                //    lvAllotmentDetails.Visible = true;
                //    //lvAllotmentDetails.DataSource = raController.GetRoomAllotmentInfoByResidentNo(Convert.ToInt32(lblGuestName.ToolTip));
                //    //lvAllotmentDetails.DataBind();  

                //    lvAllotmentDetails.DataSource = raController.GetGuestRoomAllot(Convert.ToInt32(ddlGuestList.SelectedValue));
                //    lvAllotmentDetails.DataBind();
                //}
                //else
                //{
                //    lvAllotmentDetails.DataSource = null;
                //    lvAllotmentDetails.DataBind();
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.btnShowGuest_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GuestRoomAllotment.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GuestRoomAllotment.aspx");
        }
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex > 0)
        {
            ddlFloor.Enabled = true;
            this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLK_NO=" + ddlBlock.SelectedValue, "FLOOR_NO");
            ddlFloor.Focus();
        }
        else
        {
            ddlFloor.Enabled = false;
        }
    }

    protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFloor.SelectedIndex > 0)
            {
                Room roomSearchCriteria = new Room();
                roomSearchCriteria.BlockNo = Int32.Parse(ddlBlock.SelectedValue);
                roomSearchCriteria.FloorNo = Int32.Parse(ddlFloor.SelectedValue);
                roomSearchCriteria.ResidentTypeNo = Int32.Parse(ddlResidentType.SelectedValue);
                /// Remove below commeted code if rooms have been resevered 
                /// for specific branch and/or semesmter. 
                /// Uncommenting below code will result into fiteration of rooms 
                /// based on student's branchno and/or semseterno
                //roomSearchCriteria.BranchNo = (GetViewStateItem("BranchNo") != string.Empty ? Int32.Parse(GetViewStateItem("BranchNo")) : 0);
                //roomSearchCriteria.SemesterNo = (GetViewStateItem("SemesterNo") != string.Empty ? Int32.Parse(GetViewStateItem("SemesterNo")) : 0); ;

                ddlRoom.Items.Clear();
                DataSet ds = raController.GetAvailableRooms(roomSearchCriteria);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRoom.DataSource = ds;
                    ddlRoom.DataTextField = "ROOM_NAME";
                    ddlRoom.DataValueField = "ROOM_NO";
                    ddlRoom.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(this.updRoomAllot, "Room capacity is full, please update room capacity from (Create Room) form OR check for another Room.", this.Page);
                }

               
                ddlRoom.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlRoom.Enabled = true;
                ddlFloor.Focus();
            }
            else
                ddlRoom.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.ddlFloor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHostel.SelectedIndex > 0)
        {
            ddlBlock.Enabled = true;
            //this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "BL_NO > 0", "BL_NO");
            //ADDED FOR PROPER BLOCK NAME AT ITS  HOSTEL BY SHUBHAM B ON 25/03/22
            this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + ddlHostel.SelectedValue, "BLOCK_NAME");
        }
        else
        {

            ddlBlock.Enabled = false;
        }
    }

    protected void btnAllotRoom_Click(object sender, EventArgs e)
    {
        try
        {
            RoomAllotment roomAllotment = this.BindDataFromControls();

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                CustomStatus cs = new CustomStatus();
                int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                /// Add Room
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string check = string.Empty;
                    check = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "RESIDENT_NO", "CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlResidentType.SelectedValue) + " AND RESIDENT_NO=" + Convert.ToInt32(lblGuestName.ToolTip));
                    if (check != "")
                    {
                        // ADDED this.updRoomAllot IN MSG BY SONALI ON 19-12-2022 AS MSG WAS NOT COMING
                      objCommon.DisplayMessage(this.updRoomAllot,"Room Allotment already done for session " + ddlSession.SelectedItem.Text, this.Page);           
                        return;
                    }
                    cs = (CustomStatus)raController.AllotGuestRoom(roomAllotment, Convert.ToInt32(ddlHostel.SelectedValue), 0, "", "", "");

                    if (cs.Equals(CustomStatus.RecordSaved))
                        objCommon.DisplayMessage(this.updRoomAllot, "Record Saved Successfully.", this.Page);

                }
                /// Update Room
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    roomAllotment.RoomAllotmentNo = (GetViewStateItem("RoomAllotmentNo") != string.Empty ? int.Parse(GetViewStateItem("RoomAllotmentNo")) : 0);
                    //cs = (CustomStatus)raController.UpdateRoomAllotment(roomAllotment, Convert.ToInt32(ddlHostel.SelectedValue), 0, "", "", "",0,"","",0,"",DateTime.Now,"","",0);
                    //if (cs.Equals(CustomStatus.RecordSaved))
                    //    objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);

                }
                ClearControls();
                //GetShowInfo();
                ShowAllotInfo();
                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.btnAllotRoom_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private RoomAllotment BindDataFromControls()
    {
        RoomAllotment roomAllotment = new RoomAllotment();
        try
        {

            if (ddlRoom.SelectedValue != null && ddlRoom.SelectedIndex > 0)
                roomAllotment.RoomNo = (ddlRoom.SelectedValue != string.Empty ? int.Parse(ddlRoom.SelectedValue) : 0);

            if (ddlResidentType.SelectedValue != null && ddlResidentType.SelectedIndex > 0)
                roomAllotment.ResidentTypeNo = (ddlResidentType.SelectedValue != string.Empty ? int.Parse(ddlResidentType.SelectedValue) : 0);

            if (lblGuestName.ToolTip != "")
                roomAllotment.ResidentNo = Convert.ToInt32(lblGuestName.ToolTip);

            if (ddlSession.SelectedValue != null && ddlSession.SelectedIndex > 0)
                roomAllotment.HostelSessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            if (GetViewStateItem("RoomAllotmentNo") != string.Empty)
                roomAllotment.RoomAllotmentNo = int.Parse(GetViewStateItem("RoomAllotmentNo"));

            if (txtAllotmentDate.Text.Trim() != string.Empty)
                roomAllotment.AllotmentDate = Convert.ToDateTime(txtAllotmentDate.Text.Trim());

            // roomAllotment.UserNo = int.Parse(Session["userno"].ToString());
            roomAllotment.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.BindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
        return roomAllotment;
    }

    //private void ShowReport()
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Hostel")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=Room_Allotment_Slip";
    //        url += "&path=~,Reports,Hostel,rptRoomAllotmentReceipt.rpt";
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_IDNO=" + GetViewStateItem("ResidentNo") + ",@P_RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlResidentType.SelectedValue) + ",@P_USERNAME=" + Session["userfullname"].ToString();
    //        url += "&param=@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_IDNO=" + GetViewStateItem("ResidentNo") + ",@P_RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlResidentType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString();
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','Room_Allotment_Slip','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HOSTEL")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,HOSTEL," + rptFileName;
            
    //        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_IDNO=" + GetViewStateItem("ResidentNo") + ",@P_RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlResidentType.SelectedValue) + ",@P_USERNAME=" + Session["userfullname"].ToString();
    //        url += "&param=@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_IDNO=" + GetViewStateItem("ResidentNo") + ",@P_RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlResidentType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString();
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HOSTEL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HOSTEL," + rptFileName;
            url += "&param=@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(ViewState["Sessionno"].ToString()) + ",@P_GUESTNO=" + Convert.ToInt32(ViewState["Guestno"].ToString()) + ",@P_RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlResidentType.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString();  // Done Changes In parameters
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void ClearControls()
    {
        ddlResidentType.SelectedIndex = 0;
        //ddlStandardFee.SelectedIndex = 0;
        //ddlSession.SelectedIndex = 0;
        txtAllotmentDate.Text = string.Empty;
        //txtRemark.Text = string.Empty;
        ddlHostel.SelectedIndex = 0;
        ddlBlock.Items.Clear();
        ddlBlock.Enabled = false;
        ddlFloor.Items.Clear();
        ddlFloor.Enabled = false;
        ddlRoom.Items.Clear();
        ddlRoom.Enabled = false;

        //GUEST INFO
        ddlGuestList.SelectedIndex = 0;
        divGuestSearch.Visible = true;
        divGuestInfo.Visible = false;
        divRoomAllotment.Visible = false;
        divButtonEvent.Visible = false;

    }

    public void StudClearControls()
    {
     

        txtStudStayDays.Text = "0";
        txtStudDateAllot.Text = string.Empty;
        ddlStudHostel.SelectedIndex = 0;
        ddlStudBlock.Items.Clear();
        ddlStudBlock.Enabled = false;
        ddlStudFloor.Items.Clear();
        ddlStudFloor.Enabled = false;
        ddlStudAvailRoom.Items.Clear();
        ddlStudAvailRoom.Enabled = false;
        txtStudFromDate.Text = string.Empty;
        txtStudToDate.Text = string.Empty;
        ddlStudRoomType.SelectedIndex = 0;
       

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

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnModifyAllotment_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int roomAllotmentNo = Int32.Parse(editButton.CommandArgument);
            //int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            DataSet ds = raController.GetRoomAllotmentByNo(roomAllotmentNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                BindDataToControls(ds.Tables[0].Rows[0]);
                ViewState["action"] = "edit";
                ViewState["RoomAllotmentNo"] = ds.Tables[0].Rows[0]["ROOM_ALLOTMENT_NO"].ToString();
                // Show room allotment section
                divRoomAllotment.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.btnModifyAllotment_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindDataToControls(DataRow dr)
    {
        try
        {
            if (dr["HOSTEL_SESSION_NO"] != null && ddlSession.Items.FindByValue(dr["HOSTEL_SESSION_NO"].ToString()) != null)
                ddlSession.SelectedValue = dr["HOSTEL_SESSION_NO"].ToString();

            if (dr["RESIDENT_TYPE_NO"] != null && ddlResidentType.Items.FindByValue(dr["RESIDENT_TYPE_NO"].ToString()) != null)
                ddlResidentType.SelectedValue = dr["RESIDENT_TYPE_NO"].ToString();

            txtAllotmentDate.Text = dr["ALLOTMENT_DATE"].ToString();

            if (dr["GUEST_NO"] != null && ddlGuestList.Items.FindByValue(dr["GUEST_NO"].ToString()) != null)
                ddlGuestList.SelectedValue = dr["GUEST_NO"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.BindDataToControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void ddlGuestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string check = string.Empty;
            ViewState["Guest"] = Convert.ToInt32(ddlGuestList.SelectedValue);
            ds = objCommon.FillDropDown("ACD_HOSTEL_GUEST_INFO", "GUEST_NO", "RESIDENT_TYPE_NO,GUEST_NAME,GUEST_ADDRESS,CONTACT_NO,PURPOSE,COMPANY_NAME,COMPANY_ADDRESS,COMPANY_CONTACT_NO,FROM_DATE,TO_DATE", "GUEST_NO=" + Convert.ToInt32(ddlGuestList.SelectedValue), "GUEST_NO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                divGuestSearch.Visible = false;
                divGuestInfo.Visible = true;
                //divRoomAllotment.Visible = true;
                //btnAllotRoom.Visible = false;
                //divButtonEvent.Visible = true;
                lblGuestName.Text = ds.Tables[0].Rows[0]["GUEST_NAME"].ToString();
                lblGuestName.ToolTip = ds.Tables[0].Rows[0]["GUEST_NO"].ToString();
                lblGuestAdd.Text = ds.Tables[0].Rows[0]["GUEST_ADDRESS"].ToString();
                lblGuestContact.Text = ds.Tables[0].Rows[0]["CONTACT_NO"].ToString();
                lblGuestPurpose.Text = ds.Tables[0].Rows[0]["PURPOSE"].ToString();
                ViewState["action"] = "add";
                GetShowInfo();

            }
            else 
            {
                lvAllotmentDetails.Visible = false;
                divGuestInfo.Visible = false;
                divRoomAllotment.Visible = false;
                divButtonEvent.Visible = false;
                divGuestSearch.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.ddlGuestList_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void GetShowInfo()
    {

        //int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        DataSet ds = raController.GetGuestRoomAllot(Convert.ToInt32(ddlGuestList.SelectedValue));
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Sessionno"] = ds.Tables[0].Rows[0]["HOSTEL_SESSION_NO"];
            ViewState["Guestno"] = ds.Tables[0].Rows[0]["GUEST_NO"];
            divGuestInfo.Visible = true;
            divButtonEvent.Visible = true;
            btnAllotRoom.Visible = false;
            //divRoomAllotment.Visible = true;
            btnReport.Enabled = true;
            divGuestSearch.Visible = false;
            lvAllotmentDetails.Visible = true;
            lvAllotmentDetails.DataSource = ds;
            lvAllotmentDetails.DataBind();
        }
        else
        {
            divGuestInfo.Visible = true;
            divButtonEvent.Visible = true;
            divRoomAllotment.Visible = true;
            btnAllotRoom.Visible = true;
            btnReport.Enabled = false;
            lvAllotmentDetails.DataSource = null;
            lvAllotmentDetails.DataBind();
            lvAllotmentDetails.Visible = false;
            divGuestSearch.Visible = false;

        }
    }

    private void ShowAllotInfo()
    {
        //int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        int guest = Convert.ToInt32(ViewState["Guest"]);
        DataSet ds = raController.GetGuestRoomAllot(guest);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Sessionno"] = ds.Tables[0].Rows[0]["HOSTEL_SESSION_NO"];
            ViewState["Guestno"] = ds.Tables[0].Rows[0]["GUEST_NO"];
            divGuestInfo.Visible = true;
            divButtonEvent.Visible = true;
            btnAllotRoom.Visible = false;
            //divRoomAllotment.Visible = true;
            btnReport.Enabled = true;
            divGuestSearch.Visible = false;
            lvAllotmentDetails.Visible = true;
            lvAllotmentDetails.DataSource = ds;
            lvAllotmentDetails.DataBind();
        }
        else
        {
            divGuestInfo.Visible = true;
            divButtonEvent.Visible = true;
            divRoomAllotment.Visible = true;
            btnAllotRoom.Visible = true;
            btnReport.Enabled = false;
            lvAllotmentDetails.DataSource = null;
            lvAllotmentDetails.DataBind();
            lvAllotmentDetails.Visible = false;
            divGuestSearch.Visible = false;

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

       // ShowReport("Guest Room Report", "rptRoomAllotmentReceipt.rpt");
        ShowReport("Guest Room Report", "rptGuestAllotmentReceipt.rpt");
        
    }

    #region Student Search

    private RoomAllotment StudBindDataFromControls()
    {
        RoomAllotment roomAllotment = new RoomAllotment();
        try
        {

            if (ddlStudAvailRoom.SelectedValue != null && ddlStudAvailRoom.SelectedIndex > 0)
                roomAllotment.RoomNo = (ddlStudAvailRoom.SelectedValue != string.Empty ? int.Parse(ddlStudAvailRoom.SelectedValue) : 0);

            if (ddlStudResidentType.SelectedValue != null && ddlStudResidentType.SelectedIndex > 0)
                roomAllotment.ResidentTypeNo = (ddlStudResidentType.SelectedValue != string.Empty ? int.Parse(ddlStudResidentType.SelectedValue) : 0);

            if (ddlStuSession.SelectedValue != null && ddlStuSession.SelectedIndex > 0)
                roomAllotment.HostelSessionNo = Convert.ToInt32(ddlStuSession.SelectedValue);
            ViewState["studSession"] = roomAllotment.HostelSessionNo;

            if (txtStudDateAllot.Text.Trim() != string.Empty)
                roomAllotment.AllotmentDate = Convert.ToDateTime(txtStudDateAllot.Text.Trim());

            roomAllotment.CollegeCode = Session["colcode"].ToString();

            if (GetViewStateItem("idno") != string.Empty)
                roomAllotment.ResidentNo = int.Parse(GetViewStateItem("idno"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.StudBindDataFromControls() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
        return roomAllotment;
    }
    protected void chkSearchStudent_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSearchStudent.Checked == true)
        {
            divGuestDdl.Visible = false;
            divStudSearch.Visible = true;
            chkSearchStudent.Visible = false;
            lblStuSelect.Visible = false;
           // divpanel.Visible = false;
            pnltextbox.Visible = false;
            pnlDropdown.Visible = false;
        }
        else
        {
            divGuestDdl.Visible = true;
            divStudSearch.Visible = false;
            ddlSearch.SelectedIndex = 0;
            chkSearchStudent.Visible = true;
            lblStuSelect.Visible = true;
        }
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Panel3.Visible = false;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
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
       // div_Studentdetail.Visible = false;
      
       
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

    private void bindlist(string category, string searchtext)
    {
       // StudentController objSC = new StudentController();
        DataSet ds = guestcon.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            //divReceiptType.Visible = false;
            //divStudSemester.Visible = false;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
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
    protected void lnkId_Click(object sender, EventArgs e)
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

        //if (lnk.CommandArgument == null)
        //{
        //    string number = Session["StudId"].ToString();
        //    Session["stuinfoidno"] = Convert.ToInt32 (number);
        //}
        //else
        //{
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        ViewState["idno"] = Session["stuinfoidno"].ToString();

        DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

        ViewState["studaction"] = "add";

        string check = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_HOSTEL_STUD_DAYWISE_ROOM_ALLOT_LOG AL ON (A.RESIDENT_NO=AL.RESIDENT_NO)", "A.RESIDENT_NO", "A.CAN=0 AND A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlStuSession.SelectedValue) + " AND A.RESIDENT_NO=" + Convert.ToInt32(ViewState["idno"].ToString()));
        if (check != "")
        {

            objCommon.DisplayMessage(this.updRoomAllot, "Room Allotment already done for session " + ddlStuSession.SelectedItem.Text + ", if you still wish to update please edit again.", this.Page);
            this.StudShowAllotInfo();
            //return;
        }

        if (ViewState["norecord"] == "norecord")
        {

            
            divpanel.Visible = true;
            divSearchCriteria.Visible = true;
            divBtnSearchRegion.Visible = true;
        }
        else
        {
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lblNoRecords.Visible = false;
            divpanel.Visible = false;
            divSearchCriteria.Visible = false;
            divBtnSearchRegion.Visible = false;
        }


    }

    public void DisplayStudentInfo(int idno)
    {
        #region Display Student Information
        DataSet ds;

        ds = guestcon.GetStudentInfoById(idno, Convert.ToInt32(Session["OrgId"].ToString()));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            div_Studentdetail.Visible = true;
            lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblStudBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
            lblStudRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
            lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            //lblMailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
            //hdfState.Value = ds.Tables[0].Rows[0]["STATENAME"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STATENAME"].ToString();
            //hdfZipCode.Value = ds.Tables[0].Rows[0]["PPINCODE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["PPINCODE"].ToString();
            //hdfIdno.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
            //hdfName.Value = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            //hdfEmailId.Value = ds.Tables[0].Rows[0]["EMAILID"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
            //hdfMobileNo.Value = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            //hdfCity.Value = ds.Tables[0].Rows[0]["CITY"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["CITY"].ToString();// ds.Tables[0].Rows[0]["CITYNAME"].ToString();
            ////hdfSessioNo.Value = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            //divReceiptType.Visible = true;
            //divStudSemester.Visible = true;
          //  PopulateDropDown();

            ViewState["norecord"] = null;
        }
        else
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Sorry,No Record Found For Selected Student.", this.Page);
            ViewState["norecord"] = "norecord";
      
        }
        //divPreviousReceipts.Visible = true;
        #endregion
    }

    private void PopulateDropDown()
    {
        this.objCommon.FillDropDownList(ddlStudResidentType, "ACD_HOSTEL_RESIDENT_TYPE", "RESIDENT_TYPE_NO", "RESIDENT_TYPE_NAME", "RESIDENT_TYPE_NO>0", string.Empty);
        ddlStudResidentType.SelectedIndex = 1;
        this.objCommon.FillDropDownList(ddlStudHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
        objCommon.FillDropDownList(ddlStuSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
        ddlStuSession.SelectedIndex = 1;
    }


    #endregion
    protected void ddlStudHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStudHostel.SelectedIndex > 0)
        {
            ddlStudBlock.Enabled = true;
            //this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "BL_NO > 0", "BL_NO");
            this.objCommon.FillDropDownList(ddlStudBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "HOSTEL_NO = " + ddlStudHostel.SelectedValue, "BLOCK_NAME");
            objCommon.FillDropDownList(ddlStudRoomType, "ACD_HOSTEL_ROOMTYPE_MASTER", "TYPE_NO", "ROOMTYPE_NAME", "HOSTEL_NO = " + Convert.ToInt32(ddlStudHostel.SelectedValue), "TYPE_NO");
        }
        else
        {

            ddlStudBlock.Enabled = false;
        }
    }
    protected void ddlStudBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStudBlock.SelectedIndex > 0)
        {
            ddlStudFloor.Enabled = true;
            this.objCommon.FillDropDownList(ddlStudFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlStudHostel.SelectedValue + " AND BLK_NO=" + ddlStudBlock.SelectedValue, "FLOOR_NO");
            ddlStudFloor.Focus();
        }
        else
        {
            ddlStudFloor.Enabled = false;
        }
    }
    protected void ddlStudFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStudFloor.SelectedIndex > 0)
            {
                Room roomSearchCriteria = new Room();
                roomSearchCriteria.BlockNo = Int32.Parse(ddlStudBlock.SelectedValue);
                roomSearchCriteria.FloorNo = Int32.Parse(ddlStudFloor.SelectedValue);
                roomSearchCriteria.ResidentTypeNo = Int32.Parse(ddlStudResidentType.SelectedValue);
                int roomtype = Convert.ToInt32(ddlStudRoomType.SelectedValue);

                ddlStudAvailRoom.Items.Clear();
                DataSet ds = guestcon.GetAvailableRooms(roomSearchCriteria, roomtype);
                ddlStudAvailRoom.DataSource = ds;
                ddlStudAvailRoom.DataTextField = "ROOM_NAME";
                ddlStudAvailRoom.DataValueField = "ROOM_NO";
                ddlStudAvailRoom.DataBind();
                ddlStudAvailRoom.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlStudAvailRoom.Enabled = true;
                ddlStudFloor.Focus();
            }
            else
                ddlStudAvailRoom.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.ddlFloor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void txtStudFromDate_TextChanged(object sender, EventArgs e)
    {
        if (txtStudToDate.Text != string.Empty && txtStudFromDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtStudToDate.Text) < Convert.ToDateTime(txtStudFromDate.Text))
            {

                objCommon.DisplayMessage(this.updRoomAllot,"From Date should be less than To Date.", this.Page);
                txtStudFromDate.Text = string.Empty;
                txtStudFromDate.Focus();
                txtStudStayDays.Text = "0";
                return;
            }
            txtStudStayDays.Text = Convert.ToString( 1 + (Convert.ToDateTime(txtStudToDate.Text) - Convert.ToDateTime(txtStudFromDate.Text)).TotalDays);
            CalculateAmount();
        }
        else if (txtStudToDate.Text != string.Empty)
        {
        }
        else if (txtStudToDate.Text == string.Empty)
        {
        }
        else
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Please select from date.", this.Page);
            txtStudFromDate.Text = string.Empty;
            txtStudFromDate.Focus();
            return;
        }
    }

    protected void txtStudToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtStudToDate.Text != string.Empty && txtStudFromDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtStudToDate.Text) >= Convert.ToDateTime(txtStudFromDate.Text))
            {
                txtStudStayDays.Text = Convert.ToString(1 + (Convert.ToDateTime(txtStudToDate.Text) - Convert.ToDateTime(txtStudFromDate.Text)).TotalDays);
                CalculateAmount();
            }
            else
            {
                objCommon.DisplayMessage(this.updRoomAllot, "To Date should be Greater than From Date.", this.Page);
                txtStudToDate.Text = string.Empty;
                txtStudToDate.Focus();
                txtStudStayDays.Text = "0";
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Please select from date First.", this.Page);
            txtStudToDate.Text = string.Empty;
            txtStudToDate.Focus();
            return;
        }
    }

    protected void btnStudAllotRoom_Click(object sender, EventArgs e)
    {
        try
        {
            RoomAllotment roomAllotment = this.StudBindDataFromControls();
            int roomallotmentno = 0;
            DateTime fromdate = Convert.ToDateTime(txtStudFromDate.Text);
            DateTime todate = Convert.ToDateTime(txtStudToDate.Text);
            int hostelno = Convert.ToInt32(ddlStudHostel.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"].ToString());
            string ipaddress = Session["ipAddress"].ToString();
            decimal demandcharges = Convert.ToDecimal(txtTotalAmount.Text);
            int staydays = Convert.ToInt32(txtStudStayDays.Text);
            int roomtype = Convert.ToInt32(ddlStudRoomType.SelectedValue);

            //string charges = objCommon.LookUp("ACD_HOSTEL_ROOM_CHARGE", "CHARGES", "SESSIONNO =" + Convert.ToInt32(ddlStuSession.SelectedValue) + " AND HOSTEL_NO =" + Convert.ToInt32(ddlStudHostel.SelectedValue) + " AND ROOM_TYPE=" + Convert.ToInt32(ddlStudRoomType.SelectedValue) + " AND  RESIDENT_TYPE =" + Convert.ToInt32(ddlStudResidentType.SelectedValue) + " AND ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "");
            //if (charges != null && charges != "")
            //{
            //    demandcharges = Convert.ToInt32(txtStudStayDays.Text) * Convert.ToDecimal(charges);
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updRoomAllot, "You Have not declared charges for the selected room type ,Please assign through Room Charges Master!", this.Page);
            //    return;
            //}

            /// check form action whether add or update
            if (ViewState["studaction"] != null)
            {
                CustomStatus cs = new CustomStatus();
                int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                /// Add Room
                if (ViewState["studaction"].ToString().Equals("add"))
                {
                    string check = string.Empty;
                    check = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "RESIDENT_NO", "CAN=0 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlStuSession.SelectedValue) + "AND RESIDENT_TYPE_NO=" + Convert.ToInt32(ddlStudResidentType.SelectedValue) + " AND RESIDENT_NO=" + Convert.ToInt32(ViewState["idno"].ToString()));
                    if (check != "")
                    {

                        objCommon.DisplayMessage(this.updRoomAllot, "Room Allotment already done for session " + ddlStuSession.SelectedItem.Text, this.Page);
                        return;
                    }
                    cs = (CustomStatus)guestcon.AddUpdateStudDayWiseRoomAllot(roomAllotment, fromdate, todate, demandcharges, hostelno, userno, ipaddress, roomallotmentno, staydays, roomtype);

                    if (cs.Equals(CustomStatus.RecordSaved))
                        objCommon.DisplayMessage(this.updRoomAllot, "Record Saved Successfully.", this.Page);

                }
                /// Update Room
                if (ViewState["studaction"].ToString().Equals("edit"))
                {
                    roomallotmentno = (GetViewStateItem("ROOM_ALLOTMENT_NO") != string.Empty ? int.Parse(GetViewStateItem("ROOM_ALLOTMENT_NO")) : 0);

                    cs = (CustomStatus)guestcon.AddUpdateStudDayWiseRoomAllot(roomAllotment, fromdate, todate, demandcharges, hostelno, userno, ipaddress, roomallotmentno, staydays, roomtype);

                    if (cs.Equals(CustomStatus.RecordSaved))
                        objCommon.DisplayMessage(this.updRoomAllot, "Record Updated Successfully.", this.Page);

                }
                StudClearControls();
                StudShowAllotInfo();
                if (cs.Equals(CustomStatus.Error) || cs.Equals(CustomStatus.TransactionFailed))
                    this.ShowMessage("Unable to complete the operation.");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_RoomAllotment.btnAllotRoom_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }


    private void StudShowAllotInfo()
    {
        //int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        int student = Convert.ToInt32(ViewState["idno"]);
        int session = Convert.ToInt32(ViewState["studSession"]);

        if (session == 0)
        {
            session = Convert.ToInt32(ddlStuSession.SelectedValue);
        }

        DataSet ds = guestcon.GetStudentRoomAllot(student, session);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["StudSessionno"] = ds.Tables[0].Rows[0]["HOSTEL_SESSION_NO"];         
            divStudInfo.Visible = true;
            divStudRoomAllotRegion.Visible = false;
           // divStudRegionButton.Visible = false;
           // divGuestSearch.Visible = false;
            pnlStudRoomAllot.Visible = true;
            lvStudRoomAllot.Visible = true;
            lvStudRoomAllot.DataSource = ds;
            lvStudRoomAllot.DataBind();
            
            btnStudAllotRoom.Visible = false;
            btnStudReport.Visible = true;
            btnStudReport.Enabled = true;
            BtnStudCancel.Visible = true;
            btnStudBack.Visible = false;
        }
        else
        {
            divStudInfo.Visible = true;
            divButtonEvent.Visible = true;
            divRoomAllotment.Visible = true;
            btnStudAllotRoom.Visible = true;           
            lvStudRoomAllot.DataSource = null;
            lvStudRoomAllot.DataBind();
            lvStudRoomAllot.Visible = false;
            

        }
    }
    protected void btnStudReport_Click(object sender, EventArgs e)
    {
        this.ShowStudReport("Student Day Wise Room Allotment Report", "rptStudDayWiseRoomAllotmentReceipt.rpt");
    }

    private void ShowStudReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HOSTEL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HOSTEL," + rptFileName;
            url += "&param=@P_SESSIONNO="+Convert.ToInt32(ViewState["StudSessionno"].ToString())+",@P_IDNO="+Convert.ToInt32(ViewState["idno"].ToString())+",@P_ORGANIZATION_ID="+Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])+",@P_COLLEGE_CODE="+Session["colcode"].ToString();  
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BtnStudCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnStudEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int idno = Int32.Parse(editButton.CommandArgument);
            int sessionno = Convert.ToInt32(ddlStuSession.SelectedValue);

            DataSet ds = guestcon.GetStudInfoByNo(idno, sessionno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //PopulateDropDown();
                //ddlStudResidentType.SelectedValue = dr["RESIDENT_TYPE_NO"] == null ? string.Empty : dr["RESIDENT_TYPE_NO"].ToString();
                //ddlStuSession.SelectedValue = dr["HOSTEL_SESSION_NO"] == null ? string.Empty : dr["HOSTEL_SESSION_NO"].ToString();

                txtStudDateAllot.Text = dr["ALLOTMENT_DATE"] == null ? string.Empty : dr["ALLOTMENT_DATE"].ToString();
                txtStudFromDate.Text = dr["FROMDATE"] == null ? string.Empty : dr["FROMDATE"].ToString();
                txtStudToDate.Text = dr["TODATE"] == null ? string.Empty : dr["TODATE"].ToString();

                this.objCommon.FillDropDownList(ddlStudHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
                ddlStudHostel.SelectedValue = dr["HOSTEL_NO"] == null ? string.Empty : dr["HOSTEL_NO"].ToString();
                ddlStudHostel_SelectedIndexChanged(sender, e);
                ddlStudRoomType.SelectedValue = dr["ROOM_TYPE"] == null ? string.Empty : dr["ROOM_TYPE"].ToString();
                ddlStudRoomType_SelectedIndexChanged(sender, e);
                ddlStudBlock.SelectedValue = dr["BL_NO"] == null ? string.Empty : dr["BL_NO"].ToString();
                ddlStudBlock_SelectedIndexChanged(sender, e);
                ddlStudFloor.SelectedValue = dr["FLOOR_NO"] == null ? string.Empty : dr["FLOOR_NO"].ToString();
               //ddlStudFloor_SelectedIndexChanged(sender, e);
                this.objCommon.FillDropDownList(ddlStudAvailRoom, "ACD_HOSTEL_ROOM", "ROOM_NO", "ROOM_NAME", "ROOM_NO>0", "ROOM_NO");
                ddlStudAvailRoom.SelectedValue = dr["ROOM_NO"] == null ? string.Empty : dr["ROOM_NO"].ToString();
                txtStudStayDays.Text = dr["STAY_DAYS"] == null ? string.Empty : dr["STAY_DAYS"].ToString();
                txtTotalAmount.Text = dr["DEMAND_CHARGE"] == null ? string.Empty : dr["DEMAND_CHARGE"].ToString();
                //rdoActive.SelectedValue = dr["FLOCK"].ToString();
                ViewState["studaction"] = "edit";
                ViewState["ROOM_ALLOTMENT_NO"] = dr["ROOM_ALLOTMENT_NO"] == null ? string.Empty : dr["ROOM_ALLOTMENT_NO"].ToString();
                divStudRoomAllotRegion.Visible = true;
                divStudRegionButton.Visible = true;
                btnStudAllotRoom.Visible = true;
                btnStudReport.Enabled = false;
                pnlStudRoomAllot.Visible = false;

                btnStudBack.Visible = true;
                //lvStudRoomAllot.DataSource = null;
                //lvStudRoomAllot.DataBind();
                //lvStudRoomAllot.Visible = false;
              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Masters_HostelSession.btnEdit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnStudBack_Click(object sender, EventArgs e)
    {
        divStudRoomAllotRegion.Visible = false;
        divStudRegionButton.Visible = true;
        btnStudAllotRoom.Visible = false;
        BtnStudCancel.Visible = true;
        btnStudReport.Enabled = true;
        pnlStudRoomAllot.Visible = true;
        btnStudBack.Visible = false;
    }
    private void CalculateAmount()
    {
        string charges = objCommon.LookUp("ACD_HOSTEL_ROOM_CHARGE", "CHARGES", "SESSIONNO =" + Convert.ToInt32(ddlStuSession.SelectedValue) + " AND HOSTEL_NO =" + Convert.ToInt32(ddlStudHostel.SelectedValue) + " AND ROOM_TYPE=" + Convert.ToInt32(ddlStudRoomType.SelectedValue) + " AND  RESIDENT_TYPE =" + Convert.ToInt32(ddlStudResidentType.SelectedValue) + " AND ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "");
        if (charges != null && charges != "")
        {
            txtTotalAmount.Text = (Convert.ToInt32(txtStudStayDays.Text) * Convert.ToDecimal(charges)).ToString();
        }
        else
        {
            if (ddlStudAvailRoom.SelectedValue=="")
            {
            }
            else
            {
                objCommon.DisplayMessage(this.updRoomAllot, "You Have not declared charges for the selected room type ,Please assign through Room Charges Master.", this.Page);
                return;
            }
        }
    }
    protected void chkMonthlyAmount_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMonthlyAmount.Checked)
        {
            txtTotalAmount.Enabled = true;
        }
        else
        {
            txtTotalAmount.Enabled = false;
            CalculateAmount();
        }
    }

    protected void ddlStudAvailRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateAmount();
       
    }
    protected void ddlStudRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }
}