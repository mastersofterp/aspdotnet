//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL DISCIPLINARY ACTION                                               
// CREATION DATE : 21-FEB-2023                                                        
// CREATED BY    : SONALI BHOR                                                   
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

public partial class HOSTEL_Default : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    GuestInfoController guestcon = new GuestInfoController();
    RoomAllotmentController raController = new RoomAllotmentController();


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
                   // this.CheckPageAuthorization();
                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();       
                    ViewState["action"] = "add";

                    int checkDisciplinary = Convert.ToInt32(objCommon.LookUp("ACD_HOSTEL_MODULE_CONFIG", "Allow_HostelDisciplinaryAction", "OrganizationId=" + System.Web.HttpContext.Current.Session["OrgId"] + ""));
                    if (checkDisciplinary == 0)
                    {
                        objCommon.DisplayMessage(Page, "Sorry, You Do Not Have Access To This Page !", this.Page);
                        divStudSearch.Visible = false;
                        return;
                    }
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                    ddlSearch.SelectedIndex = 0;
                    objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
                    ddlSession.SelectedIndex = 1;
                    this.GetdisciplinarydataAll("GETALL");
                    ViewState["action"] = "add";
                    
                }             
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GuestInfo.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=HostelDisciplinaryAction.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelDisciplinaryAction.aspx");
        }
    }
    #endregion

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != string.Empty && txtFromDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
            {

                objCommon.DisplayMessage(this.updRoomAllot, "From Date should be less than To Date.", this.Page);
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
                
                return;
            }           
        }
        else if (txtToDate.Text != string.Empty)
        {
        }
        else if (txtToDate.Text == string.Empty)
        {
        }
        else
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Please select from date.", this.Page);
            txtFromDate.Text = string.Empty;
            txtFromDate.Focus();
            return;
        }
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != string.Empty && txtFromDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtToDate.Text) > Convert.ToDateTime(txtFromDate.Text))
            {
               
            }
            else
            {
                objCommon.DisplayMessage(this.updRoomAllot, "To Date should be Greater than From Date.", this.Page);
                txtToDate.Text = string.Empty;
                txtToDate.Focus();
                
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Please select from date first.", this.Page);
            txtToDate.Text = string.Empty;
            txtToDate.Focus();
            return;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus cs = new CustomStatus();
        try
        {
            int hostelSession = Convert.ToInt32(ddlSession.SelectedValue);
            int idno = Convert.ToInt32(ViewState["idno"].ToString());
            string rollno = txtRegistrationNo.Text;
            int curr_hostel =  Convert.ToInt32(ViewState["Hostel"].ToString());
            int curr_room = Convert.ToInt32(ViewState["Room"].ToString());
            DateTime fromdate = Convert.ToDateTime(txtFromDate.Text);
            DateTime todate = Convert.ToDateTime(txtToDate.Text);
            string remark = txtRemark.Text;
            int userno = Convert.ToInt32(Session["userno"].ToString());
            string ipaddress = Session["ipAddress"].ToString();
            int college = Convert.ToInt32(Session["colcode"].ToString());
            int orgid = Convert.ToInt32(Session["OrgId"].ToString());
            int disci_id = 0;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    cs = (CustomStatus)guestcon.AddUpdateStudDisciplinaryAction(disci_id,idno, rollno, curr_hostel, curr_room, hostelSession, fromdate, todate, remark, userno, ipaddress, college, orgid);

                    if (cs.Equals(CustomStatus.RecordSaved))
                        objCommon.DisplayMessage(this.updRoomAllot, "Record Saved Successfully!!!", this.Page);
                  
                }
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    disci_id = Convert.ToInt32(ViewState["disci_id"].ToString());
                    cs = (CustomStatus)guestcon.AddUpdateStudDisciplinaryAction(disci_id,idno, rollno, curr_hostel, curr_room, hostelSession, fromdate, todate, remark, userno, ipaddress, college, orgid);

                    if (cs.Equals(CustomStatus.RecordSaved))
                        objCommon.DisplayMessage(this.updRoomAllot, "Record Updated Successfully!!!", this.Page);
                }
                this.GetSingledisciplinarydata("GETSINGLE");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_Default.ddlSearch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ViewState["action"] = "add";
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
                        divpanel.Visible = true;
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                    }
                    else
                    {
                        divpanel.Visible = true;
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_Default.ddlSearch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
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
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument); 
        ViewState["idno"] = Session["stuinfoidno"].ToString();

        // added to check wheather student has room allotment or not
        string check = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT", "RESIDENT_NO", "RESIDENT_NO = " + ViewState["idno"] + " AND HOSTEL_SESSION_NO = (SELECT HOSTEL_SESSION_NO FROM ACD_HOSTEL_SESSION WHERE FLOCK=1 AND HOSTEL_SESSION_NO>0)  AND CAN = 0");

        if (check == "" || check == null)
        {
            objCommon.DisplayMessage(this.updRoomAllot, "No room allotment found for this student in current session.", this.Page);
            return;
        }
        DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));
        string disci_check = objCommon.LookUp("ACD_HOSTEL_DESCIPLINARY_ACTIONS_ENTRY", "IDNO", "IDNO = " + ViewState["idno"] + "  AND CURR_SESSION =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND  DSTATUS = 0 AND OrganizationId=" + System.Web.HttpContext.Current.Session["OrgId"] + "");
        if (disci_check != null && disci_check != "")
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Disciplinary action already applied for session " + ddlSession.SelectedItem.Text + ", if you still wish to update please edit again!", this.Page);
            this.GetSingledisciplinarydata("GETSINGLE");
        }
        ViewState["studaction"] = "add";
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
    private void bindlist(string category, string searchtext)
    {
        // StudentController objSC = new StudentController();
        DataSet ds = guestcon.RetrieveStudentDetailsNew(searchtext, category);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            pnLDisciStudList.Visible = false;
            btnReport.Visible = false;
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
            pnLDisciStudList.Visible = true;
            btnReport.Visible = true;
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
            divDisciEntry.Visible = true;
            divBtnDisciEntry.Visible = true;
            lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblStudBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
            lblStudRollNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
            lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString() == string.Empty ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            //lblRoomName.Text = ds.Tables[0].Rows[0]["ROOM_NAME"].ToString();
            //lblHostelName.Text = ds.Tables[0].Rows[0]["HOSTEL_NAME"].ToString();
            ViewState["norecord"] = null;
            txtRegistrationNo.Text = lblRegno.Text;
            if (txtRegistrationNo.Text != "" || txtRegistrationNo.Text != null)
            {
                txtRegistrationNo.Enabled = false;
            }
            else
            {
                txtRegistrationNo.Enabled = true;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.updRoomAllot, "Sorry,No Record Found For Selected Student !", this.Page);
            ViewState["norecord"] = "norecord";          

        }

        //divPreviousReceipts.Visible = true;
        #endregion
        #region student hostel info
        int orgid = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"].ToString());
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

        ds = raController.GetRoomAllotmentInfoByResidentNo(idno, orgid, sessionno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            btnCancel.Visible = true;

            lblHostelName.Text = ds.Tables[0].Rows[0]["HOSTEL_NAME"].ToString();
            lblRoomName.Text = ds.Tables[0].Rows[0]["ROOM_NAME"].ToString();

            ViewState["Hostel"] = ds.Tables[0].Rows[0]["HOSTEL_NO"].ToString();
            ViewState["Room"] = ds.Tables[0].Rows[0]["ROOM_NO"].ToString();
            
        }
        else
        {
                     
            //this.ShowMessage("No room allotment found for this student.");
            objCommon.DisplayMessage(this.updRoomAllot, "No room allotment found for this student in current session.", this.Page);
           
        }
        #endregion 
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       // Panel1.Visible = true;
        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text.Trim();
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());          
    }
    private void GetdisciplinarydataAll(string command)
    {
        int idno = 0;
        int disci_id = 0;
        string commandtype = command;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = guestcon.Getdisciplinarydata(idno, disci_id, commandtype, sessionno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnLDisciStudList.Visible = true;
            btnReport.Visible = true;
            lvDisciplinary.DataSource = ds;
            lvDisciplinary.DataBind();
        }
        else
        {
            pnLDisciStudList.Visible = false;
            btnReport.Visible = false;
            lvDisciplinary.DataSource = null;
            lvDisciplinary.DataBind();      
        }
    }

    private void GetSingledisciplinarydata(string command)
    {
        int idno = Convert.ToInt32(ViewState["idno"].ToString());
        int disci_id = 0;
        string commandtype = command;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = guestcon.Getdisciplinarydata(idno, disci_id, commandtype, sessionno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            divDisciEntry.Visible = false;
            divBtnDisciEntry.Visible = true;
            btnSubmit.Visible = false;
            btnCancel.Visible = true;
            pnlStudDisciSingle.Visible = true;
            lvStudDisciAct.DataSource = ds;
            lvStudDisciAct.DataBind();
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int disciplinary_id = Int32.Parse(btnDelete.CommandArgument);
        int orgid = Convert.ToInt32(Session["OrgId"].ToString());
        int retstatus = guestcon.DeleteDiscipliStudentDetail(disciplinary_id, orgid);
        if (retstatus == 3)
        {
            objCommon.DisplayMessage(this.updRoomAllot,"Record Delete Successfully..!!!", this.Page);
            this.GetdisciplinarydataAll("GETALL");
            divpanel.Visible = false;
        }
        else
        {       
        }
    }
    protected void btnStudDisciEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int disci_id = Int32.Parse(editButton.CommandArgument);
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int idno = Convert.ToInt32(ViewState["idno"].ToString());
            string commandtype = "BYID";
            ViewState["action"] = "edit";
            ViewState["disci_id"] = disci_id; 

            DataSet ds = guestcon.Getdisciplinarydata(idno, disci_id, commandtype, sessionno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                divDisciEntry.Visible = true;
                divBtnDisciEntry.Visible = true;
                btnSubmit.Visible = true;
                pnlStudDisciSingle.Visible = false;
                txtFromDate.Text = dr["FROMDATE"] == null ? string.Empty : dr["FROMDATE"].ToString();
                txtToDate.Text = dr["TODATE"] == null ? string.Empty : dr["TODATE"].ToString();
                txtRemark.Text = dr["REMARK"] == null ? string.Empty : dr["REMARK"].ToString();              
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
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Hostel DisciplinaryAction Report", "HostelDisciplinaryActionReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HOSTEL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HOSTEL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ORGID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
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
    protected void ddlDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}