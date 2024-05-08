//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : HOSTEL
// PAGE NAME     : VIEW ROOM STATUS AND ALLOT ROOM
// CREATION DATE : 24-JULY-2015
// ADDED BY      : MR. MANISH WALDE
// DESCERIPTION  : THIS FORM SHOWS THE CURRENT VACANT STATUS OF ROOMS IN GRAPHICAL DESIGN VIEW AND USER CAN ALLOT ROOM TO STUDENTS DIRECTLY.
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC : 
//======================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using HostelBusinessLogicLayer.BusinessLogic;
using System.Text;

public partial class HOSTEL_ViewRoomStatusAndAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    RoomAllotmentController raController = new RoomAllotmentController();
    RoomAllotment roomAllotment = new RoomAllotment();

    HostelFeeCollectionController objFee = new HostelFeeCollectionController();
    //The number of Columns to be generated
    const int colsCount = 3;    //You can changed the value of 8 based on you requirements

    #region Page Load and Initialisation
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
                ViewState["TableRoomStatus"] = null;
                ViewState["RowsCount"] = 0;

                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //// Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    txtSearchStud.Text = string.Empty;
                    lblStudSearchStatus.Text = string.Empty; //Added by Saurabh L on 09/08/2022

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (Session["usertype"].ToString().Equals("2"))
                    {
                        int StudIDNO = Convert.ToInt32(Session["idno"]);

                        string Hosteler = this.objCommon.LookUp("ACD_STUDENT", "HOSTELER", "IDNO=" + StudIDNO);

                        if (Hosteler == "False")  // Added Hosteler condition for Ticket 46288
                        {
                            objCommon.DisplayMessage("You are not eligible for Hostel Room Booking, Contact Hostel admin.", this.Page);
                            divAdmin.Visible = false;
                            btnBlock.Visible = false;
                            plnAdmin.Visible = false;
                            plnAdmin1.Visible = false;
                            divStudent.Visible = true;
                            btnShow.Visible = false;
                            btnCancel.Visible = false;
                            return;
                        }
                        else
                        {

                            //Below code Added by Saurabh L on 27/02/2023 Purpose: check for Allow Hostel Disciplinary Action 
                            string Allow_HostelDisciplinary = this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_HostelDisciplinaryAction", "OrganizationId=" + Session["OrgId"] + "");

                            if (Allow_HostelDisciplinary == "1")
                            {
                                string DisciplinaryRecordEndDate = this.objCommon.LookUp("ACD_HOSTEL_DESCIPLINARY_ACTIONS_ENTRY", "Convert(varchar, TODATE, 103)", "IDNO=" + StudIDNO + " AND  DATEADD(DAY, 1, Convert (DATE, TODATE )) > GETDATE() AND DSTATUS=0 AND OrganizationId=" + Session["OrgId"] + "");

                                if (DisciplinaryRecordEndDate != "")
                                {
                                    btnShow.Visible = false;
                                    btnCancel.Visible = false;
                                    objCommon.DisplayMessage("You have Disciplinary Action upto Date : " + DisciplinaryRecordEndDate + " .", this.Page);
                                }
                            }
                            //------------ End by Saurabh L on 27/02/2023 ------------------------------

                            //DataSet ds = null;
                            divAdmin.Visible = false;
                            btnBlock.Visible = false;
                            plnAdmin.Visible = false;
                            plnAdmin1.Visible = false;
                            divStudent.Visible = true;

                            string Gender = objCommon.LookUp("ACD_STUDENT", "SEX", "IDNO =" + StudIDNO);

                            int studCurrentSemeterNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO =" + StudIDNO));

                            if (Gender == "M")
                            {
                                Session["payactivityno"] = "8";
                                this.objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "hostel_no", "HOSTEL_NAME", "hostel_no > 0 and HOSTEL_TYPE =1", "HOSTEL_NAME");
                            }
                            else
                            {
                                Session["payactivityno"] = "9";
                                this.objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "hostel_no", "HOSTEL_NAME", "hostel_no > 0 and HOSTEL_TYPE =2", "HOSTEL_NAME");
                            }

                            txtStudREGNO.Text = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO =" + StudIDNO);
                            this.DisplayStudentInfo(StudIDNO);
                            lvStudent.Visible = false;

                            this.objCommon.FillDropDownList(ddlDemandSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "ODD_EVEN=1 AND SEMESTERNO >=" + studCurrentSemeterNo, "SEMESTERNO");

                            string SessionNo = objCommon.LookUp("ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "HOSTEL_SESSION_NO > 0 AND FLOCK=1");

                            // AND CAN=0 is added in below line by Saurabh L on 19/01/2023
                            String RoomName = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT RA INNER JOIN ACD_HOSTEL_ROOM HR ON (RA.ROOM_NO = HR.ROOM_NO)", "ROOM_NAME", "RESIDENT_NO =" + StudIDNO + " AND CAN=0 AND HOSTEL_SESSION_NO =" + Convert.ToInt32(SessionNo));
                            //String RoomName = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT RA INNER JOIN ACD_HOSTEL_ROOM HR ON (RA.ROOM_NO = HR.ROOM_NO)", "ROOM_NAME", "RESIDENT_NO =" + StudIDNO + " AND HOSTEL_SESSION_NO =" + Convert.ToInt32(Session["hostel_session"]));
                            if (RoomName != "")
                            {
                                lblRoomAllot.Text = "Note :  " + RoomName + " Room Already Allotted to You For Above Hostel Session.";
                                //objCommon.DisplayMessage(udpInnerUpdatePanel, "" + RoomName + " Room Already Alloted.", this.Page);               
                                ddlHostel.Enabled = false;
                                ddlBlock.Enabled = false;
                                ddlDemandSem.Enabled = false;
                                btnShow.Visible = false;
                                btnCancel.Visible = false;
                            }

                        }
                    }
                    else
                    {
                        this.objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "hostel_no", "HOSTEL_NAME", "hostel_no > 0", "HOSTEL_NAME");
                        this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                        this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0 AND ACTIVESTATUS=1", "YEAR");

                        this.objCommon.FillDropDownList(ddlDemandSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND ODD_EVEN=1", "SEMESTERNO");  //ddlDemandSem show for both student and admin usertype

                        plnStudent.Visible = false;
                        plnStudent1.Visible = false;
                        lkbtnStudInfo.Visible = false;                        
                    }

                    this.objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");

                    //  this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "BLOCK_NAME", "BL_NO > 0", "BL_NO");

                    this.objCommon.FillDropDownList(ddlDeg, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

                    this.objCommon.FillDropDownList(ddlSemester, "ACD_Semester", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                    //this.objCommon.FillDropDownList(ddlSem, "acd_semester", "SEMESTERNO", "semestername", "Yearno > 0" ,"SEMESTERNO");


                    if (ddlSession.Items.Count > 1)
                    {
                        ddlSession.SelectedIndex = 1;
                        //btnShow.Enabled = true;
                    }
                    //else
                    //    btnShow.Enabled = false;

                }
            }
            else
            {
                lblStudSearchStatus.Text = string.Empty; //Added by Saurabh L on 09/08/2022
                /// if postback has been done implicitly
                /// then call correspinding methods.
                /// 
                if (Session["usertype"].ToString().Equals("2"))
                {

                }
                else
                {
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    if (Request.Params["__EVENTTARGET"] != null &&
                        Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                    {
                        lblCapacity.Text = hidCapacity.Value;
                        lblVacancy.Text = hidVacancy.Value;
                        string getEvent = Request.Params["__EVENTTARGET"].ToString();
                        switch (getEvent)
                        {
                            case "btnClearModalSearch":
                                this.ClearModalSearch();
                                break;
                            case "ctl00_ContentPlaceHolder1_ddlDegree":
                                //objCommon.FillDropDownList(ddlBranch, "acd_branch", "branchno", "longname", "degreeno =" + ddlDegree.SelectedValue, "longname");
                                this.fillBranch();
                                break;
                            case "ctl00_ContentPlaceHolder1_ddlYear":
                                this.fillSemester();
                                break;
                            case "btnSearchStud":
                                this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                                break;
                            case "btnAllotRoom":
                                this.AllotRoomToStudents();
                                break;
                            case "btnReport":
                                this.PrintReport();
                                break;
                            case "ClosePopUp":
                                this.ClearModalSearch();
                                break;
                            case "ctl00_ContentPlaceHolder1_imbClose":
                                ShowRooms();
                                break;

                            default:
                                break;
                        }
                    }

                }
            }
            //if (ViewState["TableRoomStatus"] != null && Convert.ToInt32(ViewState["RowsCount"]) > 0)
            //{
            //    GenerateTable(Convert.ToInt32(ViewState["RowsCount"]), colsCount, ViewState["TableRoomStatus"] as DataTable);
            //}
            divMsg.InnerHtml = string.Empty;
            //btnClear.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_ViewRoomStatusAndAllotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ViewRoomStatusAndAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ViewRoomStatusAndAllotment.aspx");
        }
    }
    #endregion

    #region Room Allot Functionality

    private void AllotRoomToStudents()
    {
        // ShowMessage("Postback successfull");
        if (ddlSession.SelectedValue != null && ddlSession.SelectedIndex > 0)
            roomAllotment.HostelSessionNo = (ddlSession.SelectedValue != string.Empty ? int.Parse(ddlSession.SelectedValue) : 0);

        roomAllotment.RoomNo = Convert.ToInt32(HiddenRoomno.Value);
        roomAllotment.UserNo = int.Parse(Session["userno"].ToString());
        roomAllotment.CollegeCode = Session["colcode"].ToString();
        roomAllotment.AllotmentDate = DateTime.Now;
        int hostel_no = Convert.ToInt32(ddlHostel.SelectedValue);
        int mess_no = 0;
        string vehicle_type = string.Empty;
        string vehicle_name = string.Empty;
        string vehicle_no = string.Empty;
        int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);


        int count = 0, cnt = 0;
        foreach (ListViewDataItem datarow in lvStudent.Items)
        {
            CheckBox chkselect = (CheckBox)datarow.FindControl("chkselect");
            if (chkselect.Checked == true && chkselect.Enabled == true)
            {
                count++;
            }
        }

        if (count > Convert.ToInt32(HiddenVacancy.Value))
        {
            //ShowMessage("Only (" + HiddenVacancy.Value + ") seats are Available for this room. You can select maximum (" + HiddenVacancy.Value + ") students to allot this room.");
            objCommon.DisplayMessage(udpInnerUpdatePanel, "Only (" + HiddenVacancy.Value + ") seats are Available for this room. You can select maximum (" + HiddenVacancy.Value + ") students to allot this room.", this.Page);
        }
        else
        {
            string Allow_DemandCreation = this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_Create_Demand_On_RoomAllotment", "OrganizationId=" + Session["OrgId"] + "");
            if (Allow_DemandCreation == "1")
            {
                foreach (ListViewDataItem datarow in lvStudent.Items)
                {
                    CheckBox chkselect = (CheckBox)datarow.FindControl("chkselect");
                    if (chkselect.Checked == true && chkselect.Enabled == true)
                    {
                        HiddenField hdnIdno = (HiddenField)datarow.FindControl("hdnIdno");
                        roomAllotment.ResidentNo = Convert.ToInt32(hdnIdno.Value);
                        if (!string.IsNullOrEmpty(txtJoinDate.Text))
                        {
                            roomAllotment.Joindate = Convert.ToDateTime(txtJoinDate.Text);
                        }
                        CustomStatus cs = (CustomStatus)raController.AllotRoom(roomAllotment, hostel_no, mess_no, vehicle_type, vehicle_name, vehicle_no, OrganizationId);
                        if (cs.Equals(CustomStatus.RecordSaved))
                            cnt++;

                    }
                }
                if (cnt > 0)
                {
                    //ShowMessage("Room Alloted Successfully.");
                    objCommon.DisplayMessage(udpInnerUpdatePanel, "Room Alloted Successfully With Demand Creation.", this.Page);
                    //PrintReport();
                    //this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                    lblVacancy.Text = (Convert.ToInt32(HiddenVacancy.Value) - cnt).ToString();
                    hidVacancy.Value = lblVacancy.Text;
                }
            }
            else
            {
                foreach (ListViewDataItem datarow in lvStudent.Items)
                {
                    CheckBox chkselect = (CheckBox)datarow.FindControl("chkselect");
                    if (chkselect.Checked == true && chkselect.Enabled == true)
                    {
                        HiddenField hdnIdno = (HiddenField)datarow.FindControl("hdnIdno");
                        roomAllotment.ResidentNo = Convert.ToInt32(hdnIdno.Value);

                        CustomStatus cs = (CustomStatus)raController.AllotRoomWithoutDemand(roomAllotment, hostel_no, mess_no, vehicle_type, vehicle_name, vehicle_no, OrganizationId);
                        if (cs.Equals(CustomStatus.RecordSaved))
                            cnt++;

                    }
                }
                if (cnt > 0)
                {
                    //ShowMessage("Room Alloted Successfully.");
                    objCommon.DisplayMessage(udpInnerUpdatePanel, "Room Alloted Successfully.", this.Page);
                    //PrintReport();
                    //this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                    lblVacancy.Text = (Convert.ToInt32(HiddenVacancy.Value) - cnt).ToString();
                    hidVacancy.Value = lblVacancy.Text;
                }
            }
        }
    }

    private void PrintReport()
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Credit_Debit_Report";
            url += "&path=~,Reports,Hostel,rptHostelRoomAllotmentSlip.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=2,@P_HOSTEL_SESSION_NO=6";


            ////To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.udpInnerUpdatePanel, this.udpInnerUpdatePanel.GetType(), "controlJSScript", sb.ToString(), true);



            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','Credit_Debit_Report','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "HOSTEL_REPORT_Credit_DebitReport.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

    #region Modal Popup Functionality
    private void ClearModalSearch()
    {
        // txtsearchstr.Text = string.Empty;
        txtSearchStud.Text = string.Empty;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlHostel.SelectedValue = "0";
        ddlBlock.SelectedValue = "0";
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lblDisciplinary.Text = string.Empty; // Added by Saurabh L on 03/03/2023
        lblDisciplinary.Visible = false;  // Added by Saurabh L on 03/03/2023
    }

    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 1)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                        case "RegNo":
                            objSearch.RegNo = paramValue;
                            break;
                        case "DegreeNo":
                            objSearch.DegreeNo = int.Parse(paramValue);
                            break;
                        case "BranchNo":
                            objSearch.BranchNo = int.Parse(paramValue);
                            break;
                        case "YearNo":
                            objSearch.YearNo = int.Parse(paramValue);
                            break;
                        case "SemNo":
                            objSearch.SemesterNo = int.Parse(paramValue);
                            break;
                        default:
                            break;
                    }
                }
            }

            lvStudent.DataSource = null;
            lvStudent.DataBind();
            BindStudentListforAllotment(objSearch);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_ViewRoomStatusAndAllotment.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindStudentListforAllotment(StudentSearch objS)
    {
        DataSet dsStud = null;
        int sessionNo = ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
        objS.ApplicationID = "";
        objS.DivisionNo = "";
        string gender;

        //objS.DivisionNo = txtsearchstr.Text;
        objS.OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);

        if (Convert.ToInt32(ViewState["HostelType"]) == 1)
        {
            gender = "M";
        }
        else
        {
            gender = "F";
        }


        // Commented by shubham as per changes that Roomtype and gender function are put common for all the clients. on 03/10/2022
        //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
        //{
        //int roomtype = Convert.ToInt32(HiddenRoomtypeno.Value);
        //dsStud = raController.GetStudentsForHostelAndRoomAllotCpu(sessionNo, objS, roomtype, gender);
        //}
        //else
        //{
        //    dsStud = raController.GetStudentsForHostelAndRoomAllot(sessionNo, objS);
        //}

        int roomtype = Convert.ToInt32(HiddenRoomtypeno.Value);
        dsStud = raController.GetStudentsForHostelAndRoomAllot(sessionNo, objS, roomtype, gender);

        lblDisciplinary.Text = string.Empty; // Added by Saurabh L on 03/03/2023
        lblDisciplinary.Visible = false;  // Added by Saurabh L on 03/03/2023

        if (dsStud != null)
        {
            if (dsStud.Tables.Count > 0 && dsStud.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = dsStud;
                lvStudent.DataBind();

                //Below code Added by Saurabh L on 28/02/2023 Purpose: check for Allow Hostel Disciplinary Action 
                string Allow_HostelDisciplinary = this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_HostelDisciplinaryAction", "OrganizationId=" + Session["OrgId"] + "");

                if (Allow_HostelDisciplinary == "1")
                {
                    int i = 0;
                    int StudIdno = 0;
                    string REGNO = string.Empty;
                    string DisciplinaryRecordEndDate = string.Empty;
                    foreach (ListViewDataItem item in lvStudent.Items)
                    {
                        CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
                        StudIdno = Convert.ToInt32(dsStud.Tables[0].Rows[i]["IDNO"]);

                        DisciplinaryRecordEndDate = this.objCommon.LookUp("ACD_HOSTEL_DESCIPLINARY_ACTIONS_ENTRY", "Convert(varchar, TODATE, 103)", "IDNO=" + StudIdno + " AND  DATEADD(DAY, 1, Convert (DATE, TODATE )) > GETDATE() AND DSTATUS=0 AND OrganizationId=" + Session["OrgId"] + "");

                        if (DisciplinaryRecordEndDate != "")
                        {
                            chkSelect.Enabled = false;
                        }

                        i++;
                    }

                    if (DisciplinaryRecordEndDate != "")
                    {
                        lblDisciplinary.Visible = true;
                        lblDisciplinary.Text = "Note: Disciplinary Students has found in search and their checkbox selection disabled in below Table.";
                    }

                }
                //----------- End by Saurabh L on 28/02/2023 ------------------------
            }
            else
            {
                // lblStudSearchStatus.Text = "Note: Room Already Allotted to Selected Student.";
                lblStudSearchStatus.Text = "Note: No Hosteller Student Found For Selected Criteria. ";

            }
        }


        dsStud.Dispose();
    }

    private void fillBranch()
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            // this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "LONGNAME");

            this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = B.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue, "B.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add("Please Select");
            ddlBranch.SelectedItem.Value = "0";
        }
    }

    private void fillSemester()
    {
        if (ddlYear.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSem, "acd_semester", "SEMESTERNO", "semestername", "Yearno =" + ddlYear.SelectedValue, "SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSem.SelectedItem.Value = "0";
        }
    }
    protected void btnAllotRoom_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("Entered");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_ViewRoomStatusAndAllotment.btnAllotRoom --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //protected void btnSearchStud_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        StudentSearch objSearch = new StudentSearch();
    //        objSearch.RegNo = txtSearchStud.Text.Trim();
    //        objSearch.DegreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
    //        objSearch.BranchNo = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
    //        objSearch.YearNo = ddlYear.SelectedIndex > 0 ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
    //        objSearch.SemesterNo = ddlSem.SelectedIndex > 0 ? Convert.ToInt32(ddlSem.SelectedValue) : 0;

    //        lvStudent.DataSource = null;
    //        lvStudent.DataBind();
    //        BindStudentListforAllotment(objSearch);

    //        mpe.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnClearModalSearch_Click(object sender, EventArgs e)
    //{
    //    ClearModalSearch();
    //}
    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillBranch();
    //    mpe.Show();
    //}

    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillSemester();
    //    mpe.Show();
    //}
    #endregion

    #region Form Runtime Change Events
    // Uncomment 'ddlHostel_SelectedIndexChanged' by Saurabh L on 14/09/2022
    // Purpose: To get Blocks as per Hostel
    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHostel.SelectedIndex > 0)
        {
            ddlBlock.Enabled = true;

            this.objCommon.FillDropDownList(ddlBlock, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_BLOCK_MASTER HB ON B.BLK_NO=HB.BL_NO", "DISTINCT B.BLK_NO", "HB.BLOCK_NAME", "HB.HOSTEL_NO = " + Convert.ToInt32(ddlHostel.SelectedValue), "HB.BLOCK_NAME");

            // Purpose: To get Student as per Hostel Selection Added by Shubham on 03/10/2022.
            ViewState["HostelType"] = this.objCommon.LookUp("ACD_HOSTEL", "HOSTEL_TYPE", " HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue));
        }
        else
        {
            ddlBlock.Enabled = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        //Response.Redirect(Request.Url.ToString());
        //ViewState["RowsCount"] = 0;
        //ViewState["TableRoomStatus"] = null;
    }
    #endregion

    #region Private Methods
    public void ClearControls()
    {
        ddlHostel.SelectedIndex = 0;
        // ddlBlock.Items.Clear();
        ddlBlock.SelectedIndex = 0;
        ddlDeg.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlDemandSem.SelectedIndex = 0;
        //ddlBlock.Enabled = false;
        //ddlFloor.Items.Clear();
        //ddlFloor.Enabled = false;
        //ddlDeg.Items.Clear();
        //ddlSemester.Items.Clear();
        //ddlDemandSem.Items.Clear();

        // added by sonali on 17/01/2023
        ddlDeg.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlDemandSem.SelectedIndex = 0;

        pnlRoomsTable.Visible = false;
        ViewState["RowsCount"] = 0;
        ViewState["TableRoomStatus"] = null;
        ViewState["HostelType"] = null;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    #endregion

    #region Show Rooms Status Functionality
    protected void btnShow_Click(object sender, EventArgs e)
    {
        // Below Server side validation added by Saurabh L on 04 May 2023
        if (Session["usertype"].ToString().Equals("2"))
        {
            if (ddlHostel.SelectedValue == "0")
                objCommon.DisplayMessage("Please Select Hostel", this.Page);
            else if (ddlBlock.SelectedValue == "0")
                objCommon.DisplayMessage("Please Select Block", this.Page);
            else if (ddlDemandSem.SelectedValue == "0")
                objCommon.DisplayMessage("Please Select Semester for Demand", this.Page);
            else
                ShowRooms();
        }
        else
        {
            if (ddlHostel.SelectedValue == "0")
                objCommon.DisplayMessage("Please Select Hostel", this.Page);
            else if (ddlDeg.SelectedValue == "0")
                objCommon.DisplayMessage("Please Select Degree", this.Page);
            else if (ddlSemester.SelectedValue == "0")
                objCommon.DisplayMessage("Please Select Semester", this.Page);
            else
                ShowRooms();
        }

    }

    public void ShowRooms()
    {
        try
        {
            DataSet dsRooms = null;

            int Hostel_session = Convert.ToInt32(Session["hostel_session"]);

            int count = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES S INNER JOIN ACD_HOSTEL_STD_FEE H ON S.STD_FEE_NO=H.STD_FEE_NO", "isnull(count(*),0)count", "H.HOSTEL_SESSION_NO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND H.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue)));   //Added by Himanshu Tamrakar 28082023

            if (count == 0)
            {
                objCommon.DisplayMessage("Please Define Hostel Standerd Fees for this Hostel Room Type.", this.Page);
                return;
            }

            if (Session["usertype"].ToString().Equals("2"))
            {
                DataSet ds = new DataSet();
                ds = this.objCommon.FillDropDown("ACD_STUDENT", "DEGREENO", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()), "");
                int DegreeNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]);
                int SemesterNo = Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"]);

                dsRooms = raController.GetRoomAvailabilityStatus(Convert.ToInt32(ddlHostel.SelectedValue), Hostel_session, DegreeNo, SemesterNo, Convert.ToInt32(ddlBlock.SelectedValue));

                ///  OrganizationId=" + Convert.ToInt32(Session["OrgId"])

            }
            else
            {
                //Below If -- else condition commited by Saurabh L on 17 April 2023
                // Purpose: To get all orgId have RoomType to show Admin side rooms
                //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
                //{
                dsRooms = raController.GetRoomAvailabilityStatusAdminCpuK(Convert.ToInt32(ddlHostel.SelectedValue), Hostel_session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue));

                //}
                //else
                //{
                //    dsRooms = raController.GetRoomAvailabilityStatusAdmin(Convert.ToInt32(ddlHostel.SelectedValue), Hostel_session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue));
                //}
            }

            if (dsRooms != null && dsRooms.Tables[0].Rows.Count > 0)
            {
                if (dsRooms.Tables[0].Rows.Count > 0)
                {
                    ////The number of Columns to be generated
                    //const int colsCount = 8;    //You can changed the value of 3 based on you requirements
                    double rows = Convert.ToDouble(dsRooms.Tables[0].Rows.Count) / Convert.ToDouble(colsCount);

                    //Below floorcount code Added By Shivam kale 07-05-2024
                    int floorcount = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_BLOCK b inner join ACD_HOSTEL_BLOCK_MASTER bm  on b.BLK_NO=bm.BL_NO inner join ACD_HOSTEL_FLOOR f on b.NO_OF_FLOORS=f.FLOOR_NO", " count(FLOOR_NO)", " BM.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)));
                    //Store the Total Rows Count in ViewState
                    //Dataset rows divide by no. of columns in dymanic table is Total No of rows in Dynamic table
                    if (ddlBlock.SelectedIndex > 0)
                    {
                        ViewState["RowsCount"] = Math.Ceiling(rows) + 1;
                    }
                    else
                    {
                        //Below floorcount code Added and Commented By Shivam kale 07-05-2024
                        //ViewState["RowsCount"] = Math.Ceiling(rows) + Convert.ToInt32(ddlBlock.Items.Count - 1);
                        ViewState["RowsCount"] = Math.Ceiling(rows) + Convert.ToInt32(ddlBlock.Items.Count - 1 + floorcount);
                    }

                    ViewState["TableRoomStatus"] = dsRooms.Tables[0];

                    GenerateTable(Convert.ToInt32(ViewState["RowsCount"]), colsCount, dsRooms.Tables[0]);

                    // Added by Saurabh L on 24 Feb 2023 Purpose: To clear ListAllotedStudent and lblAllotStudInfo
                    ListAllotedStudent.DataSource = null;
                    ListAllotedStudent.DataBind();
                    lblAllotStudInfo.Text = string.Empty;
                    //-------------End by  Saurabh L on 24 Feb 2023 ------------------
                    //Added By Himanshu Tamrakar 19-04-2024
                    if (Convert.ToInt32(Session["usertype"]) != 2)
                    {
                        divAdmin.Visible = true;
                        btnBlock.Visible = true;
                    }
                    else
                    {
                        divStudent.Visible = true;
                    }
                    //End
                    pnlRoomsTable.Visible = true;
                }
                dsRooms.Dispose();
            }
            else
            {
                objCommon.DisplayMessage("Rooms Not Found..!!", this.Page);
                pnlRoomsTable.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_ViewRoomStatusAndAllotment.ShowRooms --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    public void DisplayStudentInfo(int idno)
    {
        DataSet ds;

        ds = objFee.GetStudentInfoById(idno, Convert.ToInt32(Session["OrgId"].ToString()));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblStudBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        }
    }

    // Below method Added by Saurabh L on 24 Feb 2023 Purpose: To show student info present in room
    private void StudentInfoInRoom()
    {
        try
        {
            dvstudetails.Visible = true;
            lblCapacity.Text = hidCapacity.Value;
            lblVacancy.Text = hidVacancy.Value;
            DataSet dsStudInfo = null;

            dsStudInfo = this.objCommon.FillDropDown("ACD_STUDENT S INNER JOIN  ACD_HOSTEL_ROOM_ALLOTMENT A ON (S.IDNO=A.RESIDENT_NO) INNER JOIN ACD_DEGREE D ON (S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO)", "S.REGNO, S.STUDNAME, D.DEGREENAME", "B.LONGNAME AS BRANCH", "A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.ROOM_NO =" + Convert.ToInt32(hdnroomno.Value) + " AND A.CAN=0 AND A.OrganizationId =" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "S.REGNO");

            if (dsStudInfo != null)
            {
                if (dsStudInfo.Tables.Count > 0 && dsStudInfo.Tables[0].Rows.Count > 0)
                {
                    ListAllotedStudent.DataSource = dsStudInfo;
                    ListAllotedStudent.DataBind();
                    //below cond added By Himanshu tamrakar 04/04/2024
                    divStudent.Visible = false;
                    divAdmin.Visible = false;
                    btnBlock.Visible = false;
                    btninfoback.Visible = true;
                    dvstudetails.Visible = true;
                    ListAllotedStudent.Visible = true;
                    if (Convert.ToInt32(lblVacancy.Text) > 0)
                    {
                        btninfoback.Visible = true;
                    }
                    else
                    {
                        btninfoback.Visible = false;
                    }
                    //End
                }
                else
                {
                    ListAllotedStudent.Visible = false;
                    //lblAllotStudInfo.Text = "Note: No Hosteller Student Found in this Room. ";
                    if (Convert.ToInt32(Session["usertype"]) != 2)
                    {
                        divAdmin.Visible = true;
                        btnBlock.Visible = true;
                    }
                    else
                    {
                        divStudent.Visible = true;
                    }
                    dvstudetails.Visible = false;
                    string errorMessage = "Note: No Hosteller Student Found in this Room. ";
                    string script = "displayError('" + errorMessage + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "DisplayErrorScript", script, true);
                }
            }

            //dsStudInfo.Dispose();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_ViewRoomStatusAndAllotment.StudentInfoInRoom() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }
    //---------------End by Saurabh L on 24 Feb 2023-------------------------------------

    // Rename this function from GenerateTable to GenerateTableOld on 23Feb 2023 by Saurabh L 
    private void GenerateTableOld(int rowsCount, int colsCount, DataTable dt)
    {
        //Create the Table and Add it to the Page
        HtmlTable table = new HtmlTable();
        string strDcrNo = string.Empty;

        table.ID = "TableRooms";
        table.Width = "100%";
        //table.Width = Unit.Percentage(100);
        pnlRoomsTable.Controls.Add(table);
        //Page.Form.Controls.Add(table);

        int dataPosition = 0;
        int OldBlockNo = 0;
        int currentBlockNo = 0;
        int flag = 1;
        // Now iterate through the table and add your controls
        for (int i = 0; i < rowsCount; i++)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableRow rowBlock = new HtmlTableRow();
            for (int j = 0; j < colsCount; j++)
            {
                if (dataPosition < dt.Rows.Count)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    currentBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);

                    if (OldBlockNo == currentBlockNo)
                    {
                        //TableCell cell = new TableCell();
                        Button btn = new Button();
                        HiddenField hdnCapacity = new HiddenField();
                        HiddenField hdnVacant = new HiddenField();
                        HiddenField hdnRoomName = new HiddenField();
                        HiddenField hdnRoomNo = new HiddenField();
                        HiddenField hdnBlockName = new HiddenField();
                        HiddenField hdnBlockNo = new HiddenField();

                        cell.Attributes.Add("align", "center");
                        cell.Attributes.Add("padding", "10px");
                        cell.Attributes.Add("width", "12%");
                        //cell.Width = Unit.Percentage(15);

                        // Set a unique ID for each TextBox added
                        btn.ID = "btnRow_" + i + "_Col_" + j;
                        btn.Text = dt.Rows[dataPosition]["ROOM_NAME"] + " Cap(" + dt.Rows[dataPosition]["CAPACITY"] + ") Vac(" + dt.Rows[dataPosition]["VACANT"] + ")";
                        btn.OnClientClick = "showRoomAllotPopup(this); return false;";
                        btn.Style.Add("white-space", "normal");
                        btn.Style.Add("width", "auto");

                        hdnRoomNo.ID = "hdnRoomNoRow_" + i + "_Col_" + j;
                        hdnRoomNo.Value = dt.Rows[dataPosition]["ROOM_NO"].ToString();

                        hdnVacant.ID = "hdnVacRow_" + i + "_Col_" + j;
                        hdnVacant.Value = dt.Rows[dataPosition]["VACANT"].ToString();

                        hdnRoomName.ID = "hdnRoomNameRow_" + i + "_Col_" + j;
                        hdnRoomName.Value = dt.Rows[dataPosition]["ROOM_NAME"].ToString();

                        hdnCapacity.ID = "hdnCapRow_" + i + "_Col_" + j;
                        hdnCapacity.Value = dt.Rows[dataPosition]["CAPACITY"].ToString();


                        hdnBlockName.ID = "hdnBlockNameRow_" + i + "_Col_" + j;
                        hdnBlockName.Value = dt.Rows[dataPosition]["BLOCK_NAME"].ToString();

                        hdnBlockNo.ID = "hdnBlockNoRow_" + i + "_Col_" + j;
                        hdnBlockNo.Value = dt.Rows[dataPosition]["BLOCK_NO"].ToString();



                        //btn.Attributes.Add("width", "100px");
                        //btn.Attributes.Add("height", "100px");
                        btn.Width = Unit.Percentage(100);

                        btn.Height = Unit.Pixel(70);

                        if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) == Convert.ToInt32(dt.Rows[dataPosition]["CAPACITY"]))
                        {
                            //cell.Attributes.Add("style", "background-color:Green;");
                            // btn.Attributes.Add("style", "background-color:#cafdca;");
                            btn.Attributes.Add("style", "background-color:#67C68F;");
                        }
                        else if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) == 0)
                        {
                            //cell.Attributes.Add("style", "background-color:Red;");
                            // btn.Attributes.Add("style", "background-color:Red;");
                            btn.Attributes.Add("style", "background-color:#fd625e;");
                        }
                        else if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) < Convert.ToInt32(dt.Rows[dataPosition]["CAPACITY"]))
                        {
                            //cell.Attributes.Add("style", "background-color:Orange;");
                            // btn.Attributes.Add("style", "background-color:Orange;");
                            btn.Attributes.Add("style", "background-color:#ffe253;");
                        }

                        // Add the control to the TableCell
                        cell.Controls.Add(btn);
                        cell.Controls.Add(hdnCapacity);
                        cell.Controls.Add(hdnVacant);

                        cell.Controls.Add(hdnRoomName);
                        cell.Controls.Add(hdnRoomNo);
                        cell.Controls.Add(hdnBlockName);
                        cell.Controls.Add(hdnBlockNo);





                        // Add the TableCell to the TableRow
                        row.Cells.Add(cell);

                        OldBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);
                        dataPosition++;
                        flag = 1;
                    }
                    else
                    {
                        HtmlTableCell cellBlock = new HtmlTableCell();
                        Label lblBlock = new Label();
                        lblBlock.ID = "lblRow_" + i + "_Col_" + j;
                        lblBlock.Text = dt.Rows[dataPosition]["BLOCK_NAME"].ToString();
                        lblBlock.Style.Add("font-weight", "bold");

                        cellBlock.Attributes.Add("align", "center");
                        cellBlock.Attributes.Add("padding", "20px");
                        cellBlock.Attributes.Add("width", "auto");
                        cellBlock.Attributes.Add("height", "20px");
                        cellBlock.Style.Add("background-color", "#f5e4ca");
                        cellBlock.ColSpan = colsCount;

                        // Add the control to the TableCell 
                        cellBlock.Controls.Add(lblBlock);
                        //Add the TableCell to the TableRow
                        rowBlock.Cells.Add(cellBlock);
                        OldBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);
                        //flag = 1;

                        if (OldBlockNo != 0)
                        {
                            table.Rows.Add(row);
                            table.Rows.Add(rowBlock);

                            //for (int k = j; k < colsCount; k++)
                            //{
                            //    row.Cells.Add(cell);
                            //}
                            //table.Rows.Add(row);
                            //cell.Controls.Clear();
                            //row.Cells.Clear();
                            flag = 0;
                        }
                        else
                        {
                            table.Rows.Add(rowBlock);
                        }
                        break;
                    }
                }
            }
            if (flag == 1)
            {
                // And finally, add the TableRow to the Table
                table.Rows.Add(row);
            }
        }

        //Set Previous Data on PostBacks
        //SetPreviousData(rowsCount, colsCount);

        dt.Dispose();
    }

    private void GenerateTable(int rowsCount, int colsCount, DataTable dt)
    {
        //Create the Table and Add it to the Page
        HtmlTable table = new HtmlTable();
        string strDcrNo = string.Empty;

        table.ID = "TableRooms";
        table.Width = "100%";
        //table.Attributes.Add("class", "table table-striped table-bordered nowrap display");
        //table.Attributes.Add("CssClass", "table nowrap display");
        //table.Width = Unit.Percentage(100);
        pnlRoomsTable.Controls.Add(table);
        //Page.Form.Controls.Add(table);

        int dataPosition = 0;
        int OldBlockNo = 0;
        int currentBlockNo = 0;
        int flag = 1;

        //Floor Added By Himanshu Tamrakar 30042024
        int currentFloorNo = 0;
        int oldFloorNo = 0;
        // Now iterate through the table and add your controls
        for (int i = 0; i < rowsCount; i++)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableRow rowBlock = new HtmlTableRow();
            for (int j = 0; j < colsCount; j++)
            {
                if (dataPosition < dt.Rows.Count)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    currentBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);

                    if (OldBlockNo == currentBlockNo)
                    {
                        //TableCell cell = new TableCell();
                        Button btn = new Button();
                        HiddenField hdnCapacity = new HiddenField();
                        HiddenField hdnVacant = new HiddenField();
                        HiddenField hdnRoomName = new HiddenField();
                        HiddenField hdnRoomNo = new HiddenField();
                        HiddenField hdnBlockName = new HiddenField();
                        HiddenField hdnBlockNo = new HiddenField();


                        //change
                        HiddenField hdnRoomType = new HiddenField();
                        HiddenField hdnRoomTypeno = new HiddenField();


                        cell.Attributes.Add("align", "center");
                        cell.Attributes.Add("padding", "10px");
                        cell.Attributes.Add("width", "12%");
                        //cell.Width = Unit.Percentage(15);

                        // Set a unique ID for each TextBox added
                        btn.ID = "btnRow_" + i + "_Col_" + j;
                        btn.Text = dt.Rows[dataPosition]["ROOM_NAME"] + " Cap(" + dt.Rows[dataPosition]["CAPACITY"] + ") Vac(" + dt.Rows[dataPosition]["VACANT"] + ") \n Type(" + dt.Rows[dataPosition]["ROOMTYPE_NAME"] + ")";
                        btn.OnClientClick = "showRoomAllotPopup(this); return false;";
                        btn.Style.Add("white-space", "normal");
                        btn.Style.Add("width", "auto");

                        ViewState["ROOMTYPE"] = dt.Rows[dataPosition]["TYPE_NO"].ToString(); // ADDED BY SONALI ON 01/09/2022

                        hdnRoomNo.ID = "hdnRoomNoRow_" + i + "_Col_" + j;
                        hdnRoomNo.Value = dt.Rows[dataPosition]["ROOM_NO"].ToString();

                        hdnVacant.ID = "hdnVacRow_" + i + "_Col_" + j;
                        hdnVacant.Value = dt.Rows[dataPosition]["VACANT"].ToString();

                        hdnRoomName.ID = "hdnRoomNameRow_" + i + "_Col_" + j;
                        hdnRoomName.Value = dt.Rows[dataPosition]["ROOM_NAME"].ToString();

                        hdnCapacity.ID = "hdnCapRow_" + i + "_Col_" + j;
                        hdnCapacity.Value = dt.Rows[dataPosition]["CAPACITY"].ToString();

                        hdnBlockName.ID = "hdnBlockNameRow_" + i + "_Col_" + j;
                        hdnBlockName.Value = dt.Rows[dataPosition]["BLOCK_NAME"].ToString();

                        hdnBlockNo.ID = "hdnBlockNoRow_" + i + "_Col_" + j;
                        hdnBlockNo.Value = dt.Rows[dataPosition]["BLOCK_NO"].ToString();


                        hdnRoomType.ID = "hdnRoomTypeRow_" + i + "_Col_" + j;
                        hdnRoomType.Value = dt.Rows[dataPosition]["ROOMTYPE_NAME"].ToString();

                        hdnRoomTypeno.ID = "hdnRoomTypenoRow_" + i + "_Col_" + j;
                        hdnRoomTypeno.Value = dt.Rows[dataPosition]["TYPE_NO"].ToString();


                        //btn.Attributes.Add("width", "100px");
                        //btn.Attributes.Add("height", "100px");
                        btn.Width = Unit.Percentage(100);

                        btn.Height = Unit.Pixel(70);

                        if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) == Convert.ToInt32(dt.Rows[dataPosition]["CAPACITY"]))
                        {

                            //TableCell cell = new TableCell();
                            Button btn = new Button();
                            HiddenField hdnCapacity = new HiddenField();
                            HiddenField hdnVacant = new HiddenField();
                            HiddenField hdnRoomName = new HiddenField();
                            HiddenField hdnRoomNo = new HiddenField();
                            HiddenField hdnBlockName = new HiddenField();
                            HiddenField hdnBlockNo = new HiddenField();


                            //change
                            HiddenField hdnRoomType = new HiddenField();
                            HiddenField hdnRoomTypeno = new HiddenField();
                            HiddenField hdnuatype = new HiddenField();      //Added By Himanshu tamrakar 04/04/2024

                            cell.Attributes.Add("align", "center");
                            cell.Attributes.Add("padding", "10px");
                            cell.Attributes.Add("width", "12%");
                            //cell.Width = Unit.Percentage(15);

                            // Set a unique ID for each TextBox added
                            btn.ID = "btnRow_" + i + "_Col_" + j;
                            btn.Text = dt.Rows[dataPosition]["ROOM_NAME"] + " Cap(" + dt.Rows[dataPosition]["CAPACITY"] + ") Vac(" + dt.Rows[dataPosition]["VACANT"] + ") \n Type(" + dt.Rows[dataPosition]["ROOMTYPE_NAME"] + ")";
                            btn.OnClientClick = "showRoomAllotPopup(this); return false;";
                            btn.Style.Add("white-space", "normal");
                            btn.Style.Add("width", "auto");

                            ViewState["ROOMTYPE"] = dt.Rows[dataPosition]["TYPE_NO"].ToString(); // ADDED BY SONALI ON 01/09/2022

                            hdnRoomNo.ID = "hdnRoomNoRow_" + i + "_Col_" + j;
                            hdnRoomNo.Value = dt.Rows[dataPosition]["ROOM_NO"].ToString();

                            hdnVacant.ID = "hdnVacRow_" + i + "_Col_" + j;
                            hdnVacant.Value = dt.Rows[dataPosition]["VACANT"].ToString();

                            hdnRoomName.ID = "hdnRoomNameRow_" + i + "_Col_" + j;
                            hdnRoomName.Value = dt.Rows[dataPosition]["ROOM_NAME"].ToString();
                            
                            hdnCapacity.ID = "hdnCapRow_" + i + "_Col_" + j;
                            hdnCapacity.Value = dt.Rows[dataPosition]["CAPACITY"].ToString();

                            hdnBlockName.ID = "hdnBlockNameRow_" + i + "_Col_" + j;
                            hdnBlockName.Value = dt.Rows[dataPosition]["BLOCK_NAME"].ToString();

                            hdnBlockNo.ID = "hdnBlockNoRow_" + i + "_Col_" + j;
                            hdnBlockNo.Value = dt.Rows[dataPosition]["BLOCK_NO"].ToString();


                            hdnRoomType.ID = "hdnRoomTypeRow_" + i + "_Col_" + j;
                            hdnRoomType.Value = dt.Rows[dataPosition]["ROOMTYPE_NAME"].ToString();

                            hdnRoomTypeno.ID = "hdnRoomTypenoRow_" + i + "_Col_" + j;
                            hdnRoomTypeno.Value = dt.Rows[dataPosition]["TYPE_NO"].ToString();

                            hdnuatype.ID = "hdnUaTypenoRow_" + i + "_Col_" + j;   //Added By Himanshu tamrakar 04/04/2024
                            hdnuatype.Value = Session["usertype"].ToString();
                            //btn.Attributes.Add("width", "100px");
                            //btn.Attributes.Add("height", "100px");
                            btn.Width = Unit.Percentage(100);

                            btn.Height = Unit.Pixel(70);

                            if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) == Convert.ToInt32(dt.Rows[dataPosition]["CAPACITY"]))
                            {
                                //cell.Attributes.Add("style", "background-color:Green;");
                                //btn.Attributes.Add("style", "background-color:#cafdca;");
                                btn.Attributes.Add("style", "background-color:#67C68F;");
                            }
                            else if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) == 0)
                            {
                                //cell.Attributes.Add("style", "background-color:Red;");
                                //btn.Attributes.Add("style", "background-color:Red;");
                                btn.Attributes.Add("style", "background-color:#fd625e;");
                            }
                            else if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) < Convert.ToInt32(dt.Rows[dataPosition]["CAPACITY"]))
                            {
                                //cell.Attributes.Add("style", "background-color:Orange;");
                                //btn.Attributes.Add("style", "background-color:Orange;");
                                btn.Attributes.Add("style", "background-color:#ffe253;");
                            }

                            // Add the control to the TableCell
                            cell.Controls.Add(btn);
                            cell.Controls.Add(hdnCapacity);
                            cell.Controls.Add(hdnVacant);

                            cell.Controls.Add(hdnRoomName);
                            cell.Controls.Add(hdnRoomNo);
                            cell.Controls.Add(hdnBlockName);
                            cell.Controls.Add(hdnBlockNo);


                            // change
                            cell.Controls.Add(hdnRoomType);
                            cell.Controls.Add(hdnRoomTypeno);
                            cell.Controls.Add(hdnuatype);

                            // Add the TableCell to the TableRow
                            row.Cells.Add(cell);

                            oldFloorNo = Convert.ToInt32(dt.Rows[dataPosition]["FLOOR_NO"]);
                            OldBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);
                            dataPosition++;
                            flag = 1;
                        }
                        else if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) == 0)
                        {
                            //cell.Attributes.Add("style", "background-color:Red;");
                            //btn.Attributes.Add("style", "background-color:Red;");
                            btn.Attributes.Add("style", "background-color:#fd625e;");
                        }
                        else if (Convert.ToInt32(dt.Rows[dataPosition]["VACANT"]) < Convert.ToInt32(dt.Rows[dataPosition]["CAPACITY"]))
                        {
                            //cell.Attributes.Add("style", "background-color:Orange;");
                            //btn.Attributes.Add("style", "background-color:Orange;");
                            btn.Attributes.Add("style", "background-color:#ffe253;");
                        }

                        // Add the control to the TableCell
                        cell.Controls.Add(btn);
                        cell.Controls.Add(hdnCapacity);
                        cell.Controls.Add(hdnVacant);

                        cell.Controls.Add(hdnRoomName);
                        cell.Controls.Add(hdnRoomNo);
                        cell.Controls.Add(hdnBlockName);
                        cell.Controls.Add(hdnBlockNo);


                        // change
                        cell.Controls.Add(hdnRoomType);
                        cell.Controls.Add(hdnRoomTypeno);


                        // Add the TableCell to the TableRow
                        row.Cells.Add(cell);

                        OldBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);
                        dataPosition++;
                        flag = 1;
                    }
                    else
                    {
                        HtmlTableCell cellBlock = new HtmlTableCell();
                        Label lblBlock = new Label();
                        lblBlock.ID = "lblRow_" + i + "_Col_" + j;
                        lblBlock.Text = dt.Rows[dataPosition]["BLOCK_NAME"].ToString();
                        lblBlock.Style.Add("font-weight", "bold");

                        cellBlock.Attributes.Add("align", "center");
                        cellBlock.Attributes.Add("padding", "20px");
                        cellBlock.Attributes.Add("width", "auto");
                        cellBlock.Attributes.Add("height", "20px");
                        cellBlock.Style.Add("background-color", "#f5e4ca");
                        cellBlock.ColSpan = colsCount;

                        // Add the control to the TableCell 
                        cellBlock.Controls.Add(lblBlock);
                        //Add the TableCell to the TableRow
                        rowBlock.Cells.Add(cellBlock);
                        OldBlockNo = Convert.ToInt32(dt.Rows[dataPosition]["BLOCK_NO"]);
                        //flag = 1;

                        if (OldBlockNo != 0)
                        {
                            table.Rows.Add(row);
                            table.Rows.Add(rowBlock);

                            //for (int k = j; k < colsCount; k++)
                            //{
                            //    row.Cells.Add(cell);
                            //}
                            //table.Rows.Add(row);
                            //cell.Controls.Clear();
                            //row.Cells.Clear();
                            flag = 0;
                        }
                        else
                        {
                            table.Rows.Add(rowBlock);
                        }
                        break;
                    }
                }
            }
            if (flag == 1)
            {
                // And finally, add the TableRow to the Table
                table.Rows.Add(row);
            }
        }

        //Set Previous Data on PostBacks
        //SetPreviousData(rowsCount, colsCount);

        dt.Dispose();
    }

    private void SetPreviousData(int rowsCount, int colsCount)
    {
        Table table = (Table)Page.FindControl("TableRooms");
        if (table != null)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < colsCount; j++)
                {
                    //Extracting the Dynamic Controls from the Table
                    Button btn = (Button)table.Rows[i].Cells[j].FindControl("btnRow_" + i + "_Col_" + j);
                    //Use Request objects for getting the previous data of the dynamic textbox
                    btn.Text = Request.Form["btnRow_" + i + "_Col_" + j];
                }
            }
        }
    }
    #endregion

    #region Commented Block and Floors Logic
    //protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlFloor.SelectedIndex > 0)
    //        {
    //            Room roomSearchCriteria = new Room();
    //            roomSearchCriteria.BlockNo = Int32.Parse(ddlBlock.SelectedValue);
    //            roomSearchCriteria.FloorNo = Int32.Parse(ddlFloor.SelectedValue);
    //            ddlFloor.Focus();
    //        }

    //        //ddlRoom.Enabled = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "HOSTEL_ViewRoomStatusAndAllotment.ddlFloor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}

    //protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlBlock.SelectedIndex > 0)
    //    {
    //        ddlFloor.Enabled = true;
    //        this.objCommon.FillDropDownList(ddlFloor, "ACD_HOSTEL_BLOCK B INNER JOIN ACD_HOSTEL_FLOOR F ON B.NO_OF_FLOORS=F.FLOOR_NO", "DISTINCT F.FLOOR_NO", "F.FLOOR_NAME", "B.HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLK_NO=" + ddlBlock.SelectedValue, "FLOOR_NO");
    //        ddlFloor.Focus();
    //    }
    //    else
    //    {
    //        ddlFloor.Enabled = false;
    //    }
    //}
    #endregion

    protected void btnPay_Click(object sender, EventArgs e)
    {
        string studentIDs = Session["idno"].ToString();
        int SemesterNo = Convert.ToInt32(ViewState["SEMESTERNO"]);
        String ReceiptCode = "HF";
        bool overwrite = false;
        int roomno = Convert.ToInt32(HiddenRoomno.Value);

        Session["StudentSelectedRoom"] = roomno.ToString();
        Session["HostelSessionNo"] = ddlSession.SelectedValue.ToString();
        Session["HostelNo"] = ddlHostel.SelectedValue.ToString();

        string Allow_DemandCreation = this.objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_Create_Demand_On_RoomAllotment", "OrganizationId=" + Session["OrgId"] + "");
        if (Allow_DemandCreation == "1")
        {
            // (string studentIDs,String ReceiptTypeCode,string UserNo, int sessionno,string CollegeCode,int SemesterNo, bool overwrite, int ForSemester, int PayType)
            string response = objFee.CreateHostelFeeDemand(studentIDs, ReceiptCode, Session["usertype"].ToString(), Convert.ToInt32(ddlSession.SelectedValue), Session["colcode"].ToString(), SemesterNo, overwrite, Convert.ToInt32(ddlDemandSem.SelectedValue), 1, roomno);
            //string response = dmController.CreateHostelFeeDemand(demandCriteria, Convert.ToInt32(ddlSession.SelectedValue), chkOverwrite.Checked, studentIDs, Convert.ToInt32(rdoDemand.SelectedValue), Convert.ToInt32(ddlForSemester.SelectedValue), Convert.ToInt32(rdoDemand.SelectedValue));

            if (response != "-99")
            {
                if (response.Length > 2)
                    ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                else
                    ShowMessage("Demand sucessfully created for Selected students.");


            }
            else
            {
                ShowMessage("There is an error while creating demands. Please retry and overwrite existing demands while retrying.");
            }
        }
        else { }

        Response.Redirect("~/HOSTEL/ONLINEFEECOLLECTION/HostelFeeOnlinePayment.aspx?pageno=2886"); //live link for crescent

        //  Response.Redirect("~/HOSTEL/ONLINEFEECOLLECTION/HostelFeeOnlinePayment.aspx?pageno=3016"); //Test link for crescent

    }


    protected void lkbtnStudInfo_Click(object sender, EventArgs e)
    {
        this.StudentInfoInRoom();
    }
}
