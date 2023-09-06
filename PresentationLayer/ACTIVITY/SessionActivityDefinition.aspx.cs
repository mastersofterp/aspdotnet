//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : DEFINE SESSION ACTIVITY                                              
// CREATION DATE : 15-JUN-2009                                                          
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI                                    
// MODIFIED DATE : 22-Aug-2009 (NIRAJ D. PHALKE)                                        
// MODIFIED DESC : ADDED FACILITY FOR LINKS                                             
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class Activity_SessionActivityDefinition : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    NotificationController objNF = new NotificationController();
    NotificationEntity NF = new NotificationEntity();
    NotificationComponent notificationComponent = new NotificationComponent();
    NotificationAndroid notificationAndroid = new NotificationAndroid();
    SessionActivityController activityController = new SessionActivityController();
    SessionActivity sessionActivity = new SessionActivity();
    AcademinDashboardController AcadDash = new AcademinDashboardController(); // add by maithili [07-09-2022]

    List<SessionActivity> sessionActivityList = new List<SessionActivity>();

    private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string branches = string.Empty;
    string semester = string.Empty;
    string degrees = string.Empty;
    string UserTypes = string.Empty;
    string College_Ids = string.Empty;

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["sessionactivityno"] = "0";
                    ViewState["ipAddress"] = GetUserIPAddress();
                    ViewState["action"] = "add";
                    Session["OrgId"] = objCommon.LookUp("reff with (nolock)", "OrganizationId", string.Empty);
                    // Fill drop down lists
                    //this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND IS_ACTIVE=1 ", "SESSIONNO DESC");
                    //objCommon.FillListBox(ddlActivity, "ACTIVITY_MASTER WITH (NOLOCK)", "ACTIVITY_NO", "ACTIVITY_NAME",string.Empty, "ACTIVITY_NO");
                    PopulateActivity();
                    //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");  //commented on 2022 Aug 30
                    MultipleCollegeBind();
                    objCommon.FillDropDownList(ddlExamPattern, "ACD_EXAM_PATTERN WITH (NOLOCK)", "PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0", "PATTERNNO");
                }


                PopulatedInstituteList();
                //PopulateDegreeList(); //Comment by Mahesh Malve on Dated 27-01-2021

                PopulateSemesterList();
                PopulatUserRightsList();
                PopulateDropDownList();
                FillQuestion();
            }

            this.LoadDefinedSessionActivities();
            //ddlCollege.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //////////////////////////////////////////////////////////this is for notification master tab created by pankaj nakhale 26 feb 2020 ///////////start////////////////

    private void FillQuestion()
    {

        DataSet ds = objNF.GetNotificationDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvNotification.DataSource = ds;
            lvNotification.DataBind();
        }
        else
        {
            lvNotification.DataSource = null;
            lvNotification.DataBind();
        }
    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;
        }
        return User_IPAddress;
    }

    protected void btnSubmitNM_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != string.Empty)
            {
                if (Convert.ToInt32(txtDetailsNM.Text.Length) > 300)
                {
                    objCommon.DisplayMessage("Notification Details Can Not Be Greater Than 300", this.Page);
                    txtRemain1.Text = txtDetailsNM.Text.Length.ToString();
                    return;
                }
                NF.Title = txttitle.Text.Trim().ToString();
                NF.Details = WebUtility.HtmlDecode(txtDetailsNM.Text.Trim().ToString());
                NF.CollegeCode = Session["colcode"].ToString();
                NF.UANO = Convert.ToInt32(Session["userno"].ToString());
                NF.Ipaddress = ViewState["ipAddress"].ToString();
                NF.Expirydate = Convert.ToDateTime(txtDate.Text);
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    //Edit 
                    NF.Idno = Convert.ToInt32(ViewState["NOTIFICATIONID"]);
                    CustomStatus cs = (CustomStatus)objNF.AddNotification(NF);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearControl();
                        PopulateDropDownList();
                        objCommon.DisplayMessage(this.updnotificationmaster, "Notification Updated sucessfully", this.Page);

                        //objCommon.DisplayMessage(this.updNotificationDetails, "Please Check Atleast One !!", this.Page);
                        FillQuestion();
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(this.updnotificationmaster, "Transaction Failed", this.Page);

                    }
                }
                else
                {
                    //Add New
                    NF.Idno = 0;
                    CustomStatus cs = (CustomStatus)objNF.AddNotification(NF);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearControl();
                        PopulateDropDownList();
                        objCommon.DisplayMessage(this.updnotificationmaster, "Notification Submited Successfully", this.Page);
                        FillQuestion();
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(this.updnotificationmaster, "Transaction Failed", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updnotificationmaster, "Please Enter Notification Expiry Date.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnEditNM_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            NF.Idno = int.Parse(btnEdit.CommandArgument);
            ViewState["NOTIFICATIONID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails();
            //btnSubmit.Text = "Edit";
            //get details remain count
            txtRemain1.Text = "300";
            int detailscopunt = Convert.ToInt32(txtDetailsNM.Text.Length);
            int remain1 = Convert.ToInt32(txtRemain1.Text);
            txtRemain1.Text = (remain1 - detailscopunt).ToString();

            //get details remain count
            txtRemain.Text = "150";
            int titlecount = Convert.ToInt32(txttitle.Text.Length);
            int remain = Convert.ToInt32(txtRemain.Text);
            txtRemain.Text = (remain - titlecount).ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails()
    {
        try
        {
            SqlDataReader dr = objNF.GetNotificationDetailsByIDNO(NF.Idno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txttitle.Text = dr["TITLE"] == null ? string.Empty : dr["TITLE"].ToString();
                    txtDetailsNM.Text = dr["MESSAGE"] == null ? string.Empty : dr["MESSAGE"].ToString();
                    txtDate.Text = dr["EXPIRYDATE"] == null ? string.Empty : dr["EXPIRYDATE"].ToString();
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControl()
    {
        txttitle.Text = string.Empty;
        txtDetailsNM.Text = string.Empty;
        txtDate.Text = string.Empty;
        ViewState["action"] = "add";
        txtRemain.Text = "150";
        txtRemain1.Text = "300";
    }

    protected void btnCancelNM_Click(object sender, EventArgs e)
    {
        // Response.Redirect(Request.Url.ToString());
        ClearControl();
    }

    protected void txtDetailsNM_TextChanged(object sender, EventArgs e)
    {
        int maxSize = 300;
        int remain = Convert.ToInt32(txtRemain1.Text);
        int count = Convert.ToInt32(txtDetailsNM.Text.Length);

        if (txtDetailsNM.Text != string.Empty)
        {
            if (count <= maxSize)
            {
                txtRemain1.Text = (remain - count).ToString();
            }
            if (count > maxSize)
            {
                objCommon.DisplayMessage("Notification Details Can Not Be Greater Than 300", this.Page);
                return;
            }
        }

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkBranchList.Items.Clear();
        chkSemesterList.Items.Clear();
        chkDegreeList.Items.Clear();
        if (ddlCollege.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");  //Commented on 2022 Aug 30
            ddlSession.Focus();
            PopulateDegreeList();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    //////////////////////////////////////////////////////////this is for notification master tab created by pankaj nakhale 26 feb 2020 ///////////end////////////////

    ///////////////////////////////////////////this is for notification details tab created by pankaj nakhale 26 feb 2020 ///////////start/////

    // FIll DropDown List 
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlUser, "USER_RIGHTS WITH (NOLOCK)", "USERTYPEID", "USERDESC", "USERTYPEID>0 AND USERTYPEID IN (2,3,5)", "USERTYPEID");

        // objCommon.FillDropDownList(ddlfactype, "USER_FAC_TYPE", "UA_FAC_NO", "UA_FAC_NAME", "UA_FAC_NO>0", "UA_FAC_NO");

        objCommon.FillDropDownList(ddltitle, "TBL_ANDROID_NOTIFICATION WITH (NOLOCK)", "NOTIFICATIONID", "TITLE", "NOTIFICATIONID>0  and CAST (EXPIRYDATE AS DATE) >= CAST(GETDATE() AS DATE)", "NOTIFICATIONID");
    }

    //  -- Submit Notification
    protected void btnSubmitMD_Click(object sender, EventArgs e)
    {
        btnSubmitMD.Enabled = false;
        string ua_nos = string.Empty;
        foreach (ListViewDataItem item in lvDetail.Items)
        {
            CheckBox chk = item.FindControl("cbRow") as CheckBox;
            if (chk.Checked)
                ua_nos += chk.ToolTip + "$";
        }
        if (ua_nos != string.Empty)
        {
            NF.NotificationID = Convert.ToInt32(ddltitle.SelectedValue.ToString());
            NF.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue.ToString());
            NF.Branchno = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
            NF.Semesterno = Convert.ToInt32(ddlSemester.SelectedValue.ToString());
            NF.Deptno = Convert.ToInt32(ddlDept.SelectedValue.ToString());
            NF.UA_Type = Convert.ToInt32(ddlUser.SelectedValue.ToString());
            NF.UANO = Convert.ToInt32(Session["userno"].ToString());
            NF.UserNo = ua_nos.Trim('$');

            NF.Title = ddltitle.SelectedItem.Text;
            NF.Details = txtdetails.Text.Trim().ToString();
            //DataSet ds = objCommon.FillDropDown("TBL_ANDROID_NOTICE S INNER JOIN TBL_ANDROID_NOTICETRANSACTION A ON S.NOTIFICATIONID=A.NOTIFICATIONID", "S.NOTICEID", "S.NOTIFICATIONID", "A.TOUSERNO in('"+ ua_nos.ToString().TrimEnd('$') +"') AND A.NOTIFICATIONID="+ddltitle.SelectedValue, "");
            //if (ds.Tables[0].Rows[0]["NOTIFICATIONID"].ToString() == ddltitle.SelectedValue)
            //{
            //    objCommon.DisplayMessage(this.up1, "Your are already send that notification to the students !!", this.Page);
            //}
            CustomStatus cs = (CustomStatus)objNF.SubmitNotificationDetails(NF);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updNotificationDetails, "Notification Send Successfully", this.Page);
                BindListView();
                clearMD();
                ddlUserMD_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updNotificationDetails, "Please Check Atleast One !!", this.Page);
        }
        btnSubmit.Enabled = true;
    }

    public void clearMD()
    {
        ddlUser.SelectedIndex = 0;
        ddltitle.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        txtdetails.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;

        divut.Visible = true;
        Div1.Visible = true;
        Div3.Visible = true;
        trDept.Visible = false;
        divfactype.Visible = false;
        pnlStudent.Visible = false;
    }

    protected void btnCancelMD_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        clearMD();
    }

    protected void btnShowMD_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    private void BindListView()
    {
        DataSet ds = null;
        if (Session["usertype"].ToString() == "1")
        {
            if (ddlUser.SelectedValue != "2")
            {
                if (ddlUser.SelectedValue == "3")
                {
                    if (ddlfactype.SelectedValue == "1")
                    {
                        ds = objCommon.FillDropDown("USER_ACC U WITH (NOLOCK) INNER JOIN  PAYROLL_EMPMAS E WITH (NOLOCK) ON U.UA_IDNO= E.IDNO INNER JOIN PAYROLL_PAYMAS P WITH (NOLOCK) ON E.IDNO = P.IDNO", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND ISNULL(P.DESIGNATURENO,0) = 1  AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                    else if (ddlfactype.SelectedValue == "2")
                    {
                        ds = objCommon.FillDropDown("USER_ACC U WITH (NOLOCK) INNER JOIN  PAYROLL_EMPMAS E WITH (NOLOCK) ON U.UA_IDNO= E.IDNO INNER JOIN PAYROLL_PAYMAS P WITH (NOLOCK) ON E.IDNO = P.IDNO", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND ISNULL(P.DESIGNATURENO,0) = 2  AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                    else if (ddlfactype.SelectedValue == "3")
                    {
                        ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND ISNULL(UA_DEC,0) = 1 AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                    else if (ddlfactype.SelectedValue == "4")
                    {
                        ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + "  AND DRCSTATUS = 'S' AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                    else if (ddlfactype.SelectedValue == "5")
                    {
                        ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND DRCSTATUS = 'D'  AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                    else if (ddlfactype.SelectedValue == "6")
                    {
                        ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND DRCSTATUS = 'SD' AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                    else
                    {
                        ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND (UA_DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                    }
                }
                else
                {
                    ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND (UA_EMPDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " OR " + Convert.ToInt32(ddlDept.SelectedValue) + "=0)", "UA_FULLNAME");
                }
            }
            else
            {
                ds = objCommon.FillDropDown("USER_ACC U WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (U.UA_IDNO=S.IDNO)", "UA_NO", "UA_NAME,UA_FULLNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND CAN=0 AND ADMCAN=0 AND (DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0)", "UA_FULLNAME");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDetail.DataSource = ds;
                lvDetail.DataBind();
                pnlDetail.Visible = true;
                btnSubmitMD.Enabled = true;
                pnlDetail.Visible = true;
            }
            else
            {
                lvDetail.DataSource = null;
                lvDetail.DataBind();
                pnlDetail.Visible = false;
                btnSubmitMD.Enabled = false;
                objCommon.DisplayMessage(this.updNotificationDetails, "Record Not Found!!", this.Page);
            }
        }
    }

    protected void ddlUserMD_SelectedIndexChanged(object sender, EventArgs e)
    {
        trDept.Visible = false;
        ddlfactype.SelectedIndex = 0;
        lvDetail.DataSource = null;
        lvDetail.DataBind();
        pnlDetail.Visible = false;

        if (ddlUser.SelectedValue == "5")
        {
            objCommon.FillDropDownList(ddlDept, "[PAYROLL_SUBDEPT] D INNER JOIN USER_ACC U ON (D.SUBDEPTNO=U.UA_EMPDEPTNO)", "DISTINCT SUBDEPTNO  DEPTNO", "SUBDEPT DEPTNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND SUBDEPTNO>0", "DEPTNAME");
        }
        else
        {
            DataSet ds = objCommon.FillDropDownDepartmentUserWise(Convert.ToInt32(ddlUser.SelectedValue), "0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDept.DataSource = ds;
                ddlDept.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlDept.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlDept.DataBind();
                ddlDept.SelectedIndex = 0;
            }
            //objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN USER_ACC U ON (D.DEPTNO=U.UA_DEPTNO)", "DISTINCT DEPTNO", "DEPTNAME", "UA_TYPE=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND DEPTNO>0", "DEPTNO");
        }

        if (ddlUser.SelectedValue == "2")
        {
            pnlStudent.Visible = true;
            trDept.Visible = false;
            divfactype.Visible = false;
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            ddlBranch.Items.Clear(); ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear(); ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            //  trDept.Visible = false;
            if (ddlUser.SelectedValue == "3")
            {
                divfactype.Visible = true;
            }
            else
            {
                divfactype.Visible = false;
            }
            pnlStudent.Visible = false;
            trDept.Visible = true;
        }

    }

    protected void ddlDegreeMD_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

    }

    private DataTable GetDemandDraftDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("COLLEGENO", typeof(int)));
        dt.Columns.Add(new DataColumn("SCHEMENO", typeof(int)));
        dt.Columns.Add(new DataColumn("BRANCHNO", typeof(int)));
        dt.Columns.Add(new DataColumn("SEMESTERNO", typeof(int)));
        dt.Columns.Add(new DataColumn("UA_TYPE", typeof(int)));
        dt.Columns.Add(new DataColumn("TOUSERNO", typeof(int)));
        return dt;
    }

    protected void ddltitleMD_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtdetails.Text = (objCommon.LookUp("TBL_ANDROID_NOTIFICATION WITH (NOLOCK)", "MESSAGE", "NOTIFICATIONID =" + Convert.ToInt32(ddltitle.SelectedValue))).ToString();
    }

    //////////////////////////////////////////////////////////this is for notification details tab created by pankaj nakhale 26 feb 2020 ///////////start////////////////

    private void PopulatedInstituteList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER WITH (NOLOCK) ", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID<>0", "COLLEGE_ID");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkInstituteList.DataTextField = "COLLEGE_NAME";
                    chkInstituteList.DataValueField = "COLLEGE_ID";
                    chkInstituteList.ToolTip = "COLLEGE_ID";
                    chkInstituteList.DataSource = ds.Tables[0];
                    chkInstituteList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateDegreeList()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "CODE", "DEGREENO<>0", "DEGREENO");
            //int count = 0;

            //string values = string.Empty;
            //foreach (ListItem Item in chkInstituteList.Items)
            //{
            //    if (Item.Selected)
            //    {
            //        values += Item.Value + ",";
            //        count++;
            //    }
            //}

            //if (count > 0)
            //{
            //    string College_Ids = values.TrimEnd(',');

            //    DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE C WITH (NOLOCK) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON (C.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.CODE", "C.COLLEGE_ID<>0 AND (C.COLLEGE_ID IN (" + College_Ids + ") or 0 IN(" + College_Ids + "))", "C.COLLEGE_ID");
            //    if (ds.Tables.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            chkDegreeList.Visible = true;

            //            chkDegreeList.DataTextField = "CODE";
            //            chkDegreeList.DataValueField = "DEGREENO";
            //            chkDegreeList.ToolTip = "DEGREENO";
            //            chkDegreeList.DataSource = ds.Tables[0];
            //            chkDegreeList.DataBind();
            //        }
            //    }
            //}
            //else
            //{
            //    chkDegreeList.Visible = false;
            //    chkDegree.Checked = false;
            //    chkBranchList.Visible = false;
            //    chkBranch.Checked = false;
            //}

            string collegeId = "";
            if (ddlCollege.SelectedValue == "0" || ddlCollege.SelectedValue == "")
            {
                collegeId = Session["College_IDS"].ToString();
            }
            else
            {
                collegeId = ddlCollege.SelectedValue;
                if (collegeId.Contains("-"))
                {
                    collegeId = collegeId.Split('-')[0];
                }
            }

            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID=" + collegeId, "D.DEGREENO");//ddlCollege.SelectedValue

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkDegreeList.DataTextField = "CODE";
                    chkDegreeList.DataValueField = "DEGREENO";
                    chkDegreeList.ToolTip = "DEGREENO";
                    //ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ds.Tables[0].Rows[0]["DEGREENO"].ToString()));
                    chkDegreeList.DataSource = ds.Tables[0];
                    chkDegreeList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulatUserRightsList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("USER_RIGHTS WITH (NOLOCK)", "USERTYPEID", "USERDESC", "USERTYPEID<>0", "USERTYPEID");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkUserRightsList.DataTextField = "USERDESC";
                    chkUserRightsList.DataValueField = "USERTYPEID";
                    chkUserRightsList.ToolTip = "USERTYPEID";
                    chkUserRightsList.DataSource = ds.Tables[0];
                    chkUserRightsList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionActivityDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionActivityDefinition.aspx");
        }
    }

    private void LoadDefinedSessionActivities()
    {
        int flg = 0;
        try
        {
            //DataSet ds = activityController.GetDefinedSessionActivities();
             DataSet ds = activityController.GetDefinedSessionActivitiesFlag(flg);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvSessionActivities.DataSource = ds;
                lvSessionActivities.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Clear()
    {
        ddlExamPattern.SelectedIndex = 0;
        ddlExamNo.SelectedIndex = 0;
        ddlSubExamNo.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;

        ddlSession.SelectedIndex = 0;
        //ddlActivity.Items.Clear();
        PopulateActivity();
        MultipleCollegeBind();
        ddlCollege.ClearSelection();
        ddlSession.Items.Clear();
        //rdoNo.Checked = true;
        //rdoYes.Checked = false;
        //rdoStart.Checked = false;
        //rdoStop.Checked = true;
        chkDegreeList.Items.Clear();
        chkDegree.Checked = false;
        ViewState["sessionactivityno"] = "0";
        chkBranchList.Items.Clear();
        chkBranch.Checked = false;
        chkSemesterList.ClearSelection();
        chkSemester.Checked = false;
        chkUserRightsList.ClearSelection();
        chkUserRights.Checked = false;
        ViewState["action"] = "add";

    }
    // List<string> regIds come from database.
    // notification class below mention
    //  time =  24000
    //private void ANDROID_NOTIFICATION(string UserTypes)
    //{
    //   // string ua_no = objCommon.LookUp("user_Acc", "ua_no", "ua_type = " + UserTypes);
    //    //string fcm_regid = objCommon.LookUp("TBL_ANDROID_FCM", "fcm_regid", "ua_no = '" + UserTypes+"'");
    //     SendToFCMServer(List<string> regIds, Notification notification, int time);
    //}

    string regIds = "cPpIdXYCCOw:APA91bHHbHslRvJJv53XPmjE9lHP7DNYNxp8TX5qy_Cb8h2CbYbMbjNmTsupM_765x1W33gDPucf8pE_HCc-wRdL4mng2LEiiN51mUwVIC5sIpI_ntdm0asFzK5MxRd09LLPQToX8b1r";

    private void user_chk()
    {
        string UserTypes1 = string.Empty;
        for (int i = 0; i < chkUserRightsList.Items.Count; i++)
        {
            if (chkUserRightsList.Items[i].Selected)
            {
                UserTypes1 += chkUserRightsList.Items[i].Value + ",";
            }
        }

        if (!string.IsNullOrEmpty(UserTypes1))
            UserTypes1 = UserTypes1.Substring(0, UserTypes1.Length - 1);
    }

    // for android 31 03 2020 by pankaj nakhale
    private NotificationAndroid BindNotification_forAndroid(int activityno, string UserTypes)
    {

        try
        {
            sessionActivity = this.BindData();

            //for (int i = 0; i < chkUserRightsList.Items.Count; i++)
            //{
            //    if (chkUserRightsList.Items[i].Selected)
            //    {
            //        UserTypes += chkUserRightsList.Items[i].Value + ",";
            //    }
            //}

            // getChk();
            DataSet ds = activityController.GetFCM_ForAndroid_Details(UserTypes, activityno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string msg = ds.Tables[0].Rows[0][0].ToString();
                var charsToRemove = new string[] { "</br>", "<span style=", "color:#3c8dbc; font-weight:bold;", "</span>", ">", "\"" };
                foreach (var c in charsToRemove)
                {
                    msg = msg.Replace(c, string.Empty);
                }
                //notificationAndroid.Message = ds.Tables[0].Rows[0][0].ToString();
                notificationAndroid.Message = msg;
                notificationAndroid.MessageTitle = ds.Tables[0].Rows[0][1].ToString();
                notificationAndroid.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[0][2]).ToString();
                notificationAndroid.Action = "0";
                notificationAndroid.ImageUrl = null;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return notificationAndroid;
    }

    private List<NotificationEntity> GetFCM_forAndroid()
    {
        List<NotificationEntity> notification = new List<NotificationEntity>();
        try
        {

            int time = 24000;
            //getChk();
            user_chk();
            DataSet regIds = activityController.GetFCMRegID(UserTypes, degrees, branches, semester);
            if (regIds != null && regIds.Tables.Count > 0)
            {
                notification = (from DataRow dr in regIds.Tables[0].Rows
                                select new NotificationEntity
                                {
                                    RegID = dr["fcm_regid"].ToString()
                                }).ToList();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        return notification;
    }
    private List<SessionActivity> BindDataMultiple()
    {
        try
        {
            string sessionnos = string.Empty;
            //foreach (ListItem Sessionitem in ddlSession.Items)
            foreach (ListItem Sessionitem in ddlCollege.Items) //Add by maithili [08-09-2022]
            {
                SessionActivity sessionActivity = new SessionActivity();
                if (Sessionitem.Selected == true)
                {
                    //sessionnos += Sessionitem.Value + ',';
                    sessionnos += Sessionitem.Value.Split('-')[1] + ','; //Add by maithili [08-09-2022]

                    sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());

                    //sessionActivity.SessionNo = Convert.ToInt32(Sessionitem.Value); //comment  by maithili [08-09-2022]
                    sessionActivity.SessionNo = Convert.ToInt32((Sessionitem.Value).Split('-')[1]); //Add by maithili [08-09-2022]

                    string activitynos = string.Empty;
                    string activitynames = string.Empty;
                    //foreach (ListItem items in ddlActivity.Items)
                    //{
                    //    if (items.Selected == true)
                    //    {
                    //        activitynos += items.Value + ',';
                    //        activitynames += items.Text + ',';
                    //    }
                    //}
                    ////department = department.Length - 1;
                    //if (activitynos.Length > 1)
                    //{
                    //    activitynos = activitynos.Remove(activitynos.Length - 1);
                    //}
                    //if (activitynames.Length > 1)
                    //{
                    //    activitynames = activitynames.Remove(activitynames.Length - 1);
                    //}
                    activitynos = ddlActivity.SelectedValue;
                    activitynames = ddlActivity.SelectedItem.Text;

                    //sessionActivity.ActivityNo = ((ddlActivity.SelectedIndex > 0) ? int.Parse(ddlActivity.SelectedValue) : 0);
                    sessionActivity.ActivityNos = activitynos;
                    sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
                    sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
                    //sessionActivity.IsStarted = (rdoStart.Checked ? true : false);
                    //sessionActivity.ShowStatus = (rdoYes.Checked ? 1 : 0);

                    //------   Added By Rishabh ON 29/10/2021
                    if (hfdActive.Value == "true")
                    {
                        sessionActivity.IsStarted = true;
                    }
                    else
                    {
                        sessionActivity.IsStarted = false;
                    }

                    if (hfdShowStatus.Value == "true")
                    {
                        sessionActivity.ShowStatus = 1;
                    }
                    else
                    {
                        sessionActivity.ShowStatus = 0;
                    }
                    //sessionActivity.Session_Name = ddlSession.SelectedItem.Text;//
                    sessionActivity.Session_Name = Sessionitem.Text;
                    sessionActivity.Activity_Name = activitynames;
                }
                sessionActivityList.Add(sessionActivity);
            }
            if (sessionnos.Length > 1)
            {
                sessionnos = sessionnos.Remove(sessionnos.Length - 1);
            }
            if (sessionnos.Length > 0)
            {

            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return sessionActivityList;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            branches = string.Empty; semester = string.Empty; degrees = string.Empty; UserTypes = string.Empty;// College_Ids = string.Empty;
            int flg = 0; //Added by Injamam 28-2-23
            DataSet ds = new DataSet();

            //ddlCollege.Attributes.Add("disabled", "");
            ////ddlCollege.Attributes.Remove("disabled");
            if (getChk() == false)
            {
                return;
            }
            //     getChk();

            ds = (DataSet)ViewState["CollegeSession"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //ddlSession.SelectedValue = dr["SESSION_NO"].ToString();

                //objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + dr["College_IDS"].ToString() + "',',')) AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

                MultipleCollegeBind(); //add by maithili [08-09-2022]

                //string College_Ids = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();


                string College_Ids = ds.Tables[0].Rows[0]["SESSION_NO"].ToString();

                if (!College_Ids.Contains("-"))
                {
                    College_Ids = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString() + "-" + ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                }


                for (int j = 0; j < College_Ids.ToString().Length; j++)
                {
                    for (int i = 0; i < ddlCollege.Items.Count; i++)
                    {
                        if (College_Ids.ToString() == ddlCollege.Items[i].Value)
                        {
                            ddlCollege.Items[i].Selected = true;
                            //ddlCollege.Items[i].Attributes.Add("disabled", "disabled") ;
                        }
                    }
                }
            }
            //Added on 2022 Aug 30 for checking atleast one session checked.
            if (!CheckSessionSelected())
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Please Select Session!", this.Page);
                return;
            }
            //Added on 2022 Aug 30 for checking atleast one session checked End. 
            //sessionActivity = this.BindData(); //Commeted on 2022 Sep 01
            sessionActivityList = this.BindDataMultiple();
            ////getChk();
            // for android added by pankaj nakhale 28 march 2020///
            // string regIds = "cPpIdXYCCOw:APA91bHHbHslRvJJv53XPmjE9lHP7DNYNxp8TX5qy_Cb8h2CbYbMbjNmTsupM_765x1W33gDPucf8pE_HCc-wRdL4mng2LEiiN51mUwVIC5sIpI_ntdm0asFzK5MxRd09LLPQToX8b1r";
            int time = 24000;

            //   ANDROID_NOTIFICATION(UserTypes);
            string Notification_Template = objCommon.LookUp("ACTIVITY_MASTER WITH (NOLOCK)", "ACTIVITYTEMPLATE", "ACTIVITY_NO = " + sessionActivity.ActivityNo);
            int userno = Convert.ToInt32(Session["userno"]);
            // this.objCommon.FillDropDownList(Notification_Template, "ACTIVITY_MASTER", "ACTIVITY_NO", "ActivityTemplate", "ACTIVITY_NO=ACT");
            CustomStatus cs = CustomStatus.Others;
            CustomStatus css = CustomStatus.Others;

            //Added on 2022 Sep 01
            string CollegeIds = GetSelectedCollegeIds();

            //#region  Check Existing Collgege
            string existingCollgeName = string.Empty;


            //if (Convert.ToString(ViewState["action"]).Equals("add"))
            //{
            //    //DataSet dsActivityNo = objCommon.FillDropDown("SESSION_ACTIVITY", "ACTIVITY_NO", "", "ACTIVITY_NO = " + ddlActivity.SelectedValue, string.Empty);
            //    //if (dsActivityNo != null && dsActivityNo.Tables[0].Rows.Count > 0)
            //    //{
            //    foreach (string clg in CollegeIds.Split(','))
            //    {
            //        DataSet dsExistIDs = objCommon.FillDropDown("SESSION_ACTIVITY", "TOP 1 COLLEGE_IDS", "ACTIVITY_NO", "ACTIVITY_NO = " + ddlActivity.SelectedValue + " AND COLLEGE_IDS='" + clg + "'", string.Empty);
            //        if (dsExistIDs.Tables[0] != null && dsExistIDs.Tables[0].Rows.Count > 0)
            //        {
            //            DataSet dsClgName = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID=" + clg, string.Empty);
            //            //SELECT COLLEGE_ID,COLLEGE_NAME FROM ACD_COLLEGE_MASTER WHERE COLLEGE_ID=
            //            if (dsClgName.Tables[0] != null && dsClgName.Tables[0].Rows.Count > 0)
            //            {
            //                existingCollgeName += dsClgName.Tables[0].Rows[0]["COLLEGE_NAME"].ToString() + ",";
            //            }
            //        }
            //    }
            //    //}
            //}

            //if (existingCollgeName.Length > 1)
            //{
            //    existingCollgeName = existingCollgeName.Substring(0, existingCollgeName.Length - 1);
            //    objCommon.DisplayUserMessage(this.updSesActivity, "Selected Activity Already Exist:" + existingCollgeName, this.Page);
            //    return;
            //}
            //#endregion

            ViewState["dsSessionData"] = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_CODE,IS_ACTIVE,ORGANIZATIONID,COLLEGE_ID", "OrganizationId = " + Session["OrgId"].ToString(), "");
            ViewState["dsClgDegBrn"] = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH", "CDBNO", "COLLEGE_ID,DEGREENO,BRANCHNO,OrganizationId", "OrganizationId = " + Session["OrgId"].ToString(), "");

            //Added on 2022 Sep 01 End

            if (ViewState["sessionactivityno"].ToString() != string.Empty && ViewState["sessionactivityno"].ToString() == "0")
            {
               

                //cs = (CustomStatus)activityController.AddSessionActivity(sessionActivity, branches, semester, degrees, UserTypes, Convert.ToInt32(ddlCollege.SelectedValue));   //Commented on 2022 Aug 30
                //css = (CustomStatus)activityController.AddSessionActivity_FOR_NOTIFICATION(sessionActivity, branches, semester, degrees, UserTypes, Notification_Template, userno, Convert.ToInt32(ddlCollege.SelectedValue)); //Commented on 2022 Aug 30
                //ref branches, ref semester, ref degrees, ref UserTypes
                string branchesO = branches.TrimEnd(','), Exists = ""; string semesterO = semester.TrimEnd(','); string degreesO = degrees.TrimEnd(','); string UserTypesO = UserTypes.TrimEnd(',');  //holding original Selected Values
                //sessionActivityList

                int isSaved = 0;

                foreach (SessionActivity sessionActivityItem in sessionActivityList)
                {
                    foreach (string clg in CollegeIds.Split(','))
                    {
                        //sessionActivity = sessionActivityItem;
                        int CollegeId = Convert.ToInt32(clg);
                        Boolean IsvalidForInsert = PreparingForInsertRecord(sessionActivityItem, ref branches, ref semester, ref degrees, ref UserTypes, CollegeId);
                        if (IsvalidForInsert)
                        {
                            DataSet ds1 = activityController.AddSessionActivity(sessionActivityItem, branches.TrimEnd(','), semester.TrimEnd(','), degrees.TrimEnd(','), UserTypes.TrimEnd(','), CollegeId,flg);  //flg added by injamam 28-2-23

                            if (ds1.Tables.Count > 0)
                            {
                                for (int i = 0; i < ds1.Tables.Count; i++)
                                {
                                    if (ContainColumn("STATUS", ds1.Tables[i]))
                                    {
                                        if (ds1.Tables[i] != null && ds1.Tables[i].Rows.Count > 0)
                                        {
                                            for (int j = 0; j < ds1.Tables[i].Rows.Count; j++)
                                            {
                                                if (ds1.Tables[i].Rows[j]["STATUS"].ToString().Equals("2"))
                                                {
                                                    isSaved++;
                                                }
                                            }
                                        }
                                    }
                                    if (ContainColumn("COLLEGE_NAME", ds1.Tables[i]))
                                    {
                                        if (ds1.Tables[i] != null && ds1.Tables[i].Rows.Count > 0)
                                        {
                                            for (int j = 0; j < ds1.Tables[i].Rows.Count; j++)
                                            {
                                                existingCollgeName += ds1.Tables[i].Rows[j]["COLLEGE_NAME"].ToString() + ",";
                                            }
                                        }
                                    }
                                }
                            }

                            //// cs = (CustomStatus)activityController.AddSessionActivity(sessionActivityItem, branches, semester, degrees, UserTypes, CollegeId);
                            //css = (CustomStatus)activityController.AddSessionActivity_FOR_NOTIFICATION(sessionActivityItem, branches, semester, degrees, UserTypes, Notification_Template, userno, Convert.ToInt32(ddlCollege.SelectedValue)); //Commented on 2022 Aug 30
                            css = (CustomStatus)activityController.AddSessionActivity_FOR_NOTIFICATION(sessionActivityItem, branches, semester, degrees, UserTypes, Notification_Template, userno, CollegeId);
                        }
                    }
                }

                if (existingCollgeName.Length > 1)
                {
                    existingCollgeName = existingCollgeName.Substring(0, existingCollgeName.Length - 1);
                    objCommon.DisplayUserMessage(this.updSesActivity, "Selected Activity Already Exist:" + existingCollgeName, this.Page);
                    return;
                }

                if (isSaved > 0)
                {
                    objCommon.DisplayUserMessage(this.updSesActivity, "Sessional Activity Definition Saved Successfully!", this.Page);
                    LoadDefinedSessionActivities();
                    Clear();
                    return;
                }
                
                branches = branchesO; semester = semesterO; degrees = degreesO; UserTypes = UserTypesO;  //ReAssiging originalValues;
                this.LoadDefinedSessionActivities();
            }
            else if (int.Parse(ViewState["sessionactivityno"].ToString()) > 0)
            {

                string ii = ViewState["sessionactivityno"].ToString();
                int CollegeId = 0;
                if (Session["College_IDS"] != null)
                {
                    CollegeId = (int)Session["College_IDS"];
                }
                //sessionActivity = sessionActivityList[0];

                for (int i = 0; i < sessionActivityList.Count; i++)
                {
                    if (sessionActivityList[i].SessionActivityNo == Convert.ToInt32(ii))
                    {
                        sessionActivity = sessionActivityList[i];
                    }
                }


                cs = (CustomStatus)activityController.UpdateSessionActivity(sessionActivity, branches.TrimEnd(','), semester.TrimEnd(','), degrees.TrimEnd(','), UserTypes.TrimEnd(','), CollegeId,flg);  //Commented on 2022 Aug 30  //flg added on 28-2-23 by Injamam
                //css = (CustomStatus)activityController.AddSessionActivity_FOR_NOTIFICATION(sessionActivity, branches, semester, degrees, UserTypes, Notification_Template, userno, Convert.ToInt32(ddlCollege.SelectedValue));  //Commented on 2022 Aug 30

                this.LoadDefinedSessionActivities();

                // Clear();
            }
            List<NotificationEntity> RegID = this.GetFCM_forAndroid();
            notificationAndroid = this.BindNotification_forAndroid(sessionActivity.ActivityNo, UserTypes);  //Commented on 2022 Aug 30

            bool status = notificationComponent.SendToFCMServer(RegID, notificationAndroid, time); //Commented on 2022 Aug 30

            //  notificationComponent.SendNotification();
            Clear();

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Sessional Activity Definition Saved Successfully!", this.Page);

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Record Already Exists!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Sessional Activity Definition Updated Successfully!", this.Page);
               
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSesActivity, "Error!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool ContainColumn(string columnName, DataTable table)
    {
        DataColumnCollection columns = table.Columns;
        if (columns.Contains(columnName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private Boolean getChk1()
    //{

    //    if (!string.IsNullOrEmpty(degrees))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.Page, "Please Select Degree", Page); 
    //        return false;
    //    }

    //    if (!string.IsNullOrEmpty(branches))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.Page, "Please Select Branch", Page);         
    //        return false;
    //    }

    //}


    private Boolean getChk()
    {
        for (int i = 0; i < chkBranchList.Items.Count; i++)
        {
            if (chkBranchList.Items[i].Selected)
            {
                branches += chkBranchList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkSemesterList.Items.Count; i++)
        {
            if (chkSemesterList.Items[i].Selected)
            {
                semester += chkSemesterList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkDegreeList.Items.Count; i++)
        {
            if (chkDegreeList.Items[i].Selected)
            {
                degrees += chkDegreeList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkUserRightsList.Items.Count; i++)
        {
            if (chkUserRightsList.Items[i].Selected)
            {
                UserTypes += chkUserRightsList.Items[i].Value + ",";
            }
        }

        for (int i = 0; i < chkInstituteList.Items.Count; i++)
        {
            if (chkInstituteList.Items[i].Selected)
            {
                College_Ids += chkInstituteList.Items[i].Value + ",";
            }
        }

        if (string.IsNullOrEmpty(degrees))
        {
            objCommon.DisplayMessage(this.Page, "Please Select Degree", Page);
            return false;
        }

        else if (string.IsNullOrEmpty(branches))
        {
            objCommon.DisplayMessage(this.Page, "Please Select Branch", Page);
            return false;
        }
        else
        {
            return true;
        }

        if (!string.IsNullOrEmpty(branches))
            branches = branches.Substring(0, branches.Length - 1);
        if (!string.IsNullOrEmpty(semester))
            semester = semester.Substring(0, semester.Length - 1);
        if (!string.IsNullOrEmpty(degrees))
            degrees = degrees.Substring(0, degrees.Length - 1);
        if (!string.IsNullOrEmpty(UserTypes))
            UserTypes = UserTypes.Substring(0, UserTypes.Length - 1);
        if (!string.IsNullOrEmpty(College_Ids))
            College_Ids = College_Ids.Substring(0, College_Ids.Length - 1);
        return true;
    }

    private SessionActivity BindData()
    {
        //SessionActivity sessionActivity = new SessionActivity();
        try
        {
            sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());

            //sessionActivity.SessionNo = ((ddlSession.SelectedIndex > 0) ? int.Parse(ddlSession.SelectedValue) : 0);  //comment by maithili [08-09-2022]

            foreach (ListItem Sessionitem in ddlCollege.Items) //Add by maithili [08-09-2022]
            {
                if (Sessionitem.Selected == true)
                {
                    sessionActivity.SessionNo = Convert.ToInt32((Sessionitem.Value).Split('-')[1]);
                }
            }


            string activitynos = string.Empty;
            string activitynames = string.Empty;
            foreach (ListItem items in ddlActivity.Items)
            {
                if (items.Selected == true)
                {
                    activitynos += items.Value + ',';
                    activitynames += items.Text + ',';
                }
            }
            //department = department.Length - 1;
            if (activitynos.Length > 1)
            {
                activitynos = activitynos.Remove(activitynos.Length - 1);
            }
            if (activitynames.Length > 1)
            {
                activitynames = activitynames.Remove(activitynames.Length - 1);
            }
            //sessionActivity.ActivityNo = ((ddlActivity.SelectedIndex > 0) ? int.Parse(ddlActivity.SelectedValue) : 0);
            sessionActivity.ActivityNos = activitynos;
            sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
            sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
            //sessionActivity.IsStarted = (rdoStart.Checked ? true : false);
            //sessionActivity.ShowStatus = (rdoYes.Checked ? 1 : 0);

            //------   Added By Rishabh ON 29/10/2021
            if (hfdActive.Value == "true")
            {
                sessionActivity.IsStarted = true;
            }
            else
            {
                sessionActivity.IsStarted = false;
            }

            if (hfdShowStatus.Value == "true")
            {
                sessionActivity.ShowStatus = 1;
            }
            else
            {
                sessionActivity.ShowStatus = 0;
            }
            //added for get notification table data BY PANKAJ NAKHALE 27 JAN 2020

            //sessionActivity.Session_Name = ddlSession.SelectedItem.Text;  //comment  by maithili [08-09-2022]
            sessionActivity.Activity_Name = activitynames;
        }
        catch (Exception ex)
        {
            throw;
        }
        return sessionActivity;
    }

    ////added for get notification table data
    //private SessionActivity BindData_notification()
    //{
    //    SessionActivity sessionActivity = new SessionActivity();
    //    try
    //    {
    //       // sessionActivity.SessionActivityNo = int.Parse(ViewState["sessionactivityno"].ToString());
    //        sessionActivity.SessionNo = ((ddlSession.SelectedIndex > 0) ? int.Parse(ddlSession.SelectedValue) : 0);
    //        sessionActivity.Session_Name = ddlSession.SelectedItem.Text;
    //        sessionActivity.ActivityNo = ((ddlActivity.SelectedIndex > 0) ? int.Parse(ddlActivity.SelectedValue) : 0);
    //        sessionActivity.Activity_Name = ddlActivity.SelectedItem.Text;
    //        sessionActivity.StartDate = (txtStartDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtStartDate.Text.Trim()) : DateTime.MinValue);
    //        sessionActivity.EndDate = (txtEndDate.Text.Trim() != string.Empty ? Convert.ToDateTime(txtEndDate.Text.Trim()) : DateTime.MinValue);
    //        sessionActivity.IsStarted = (rdoStart.Checked ? true : false);
    //        //sessionActivity.ShowStatus = (rdoYes.Checked ? 1 : 0);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //    return sessionActivity;
    //}

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEditRecord = sender as ImageButton;
    //        int recordId = int.Parse(btnEditRecord.CommandArgument);
    //        DataSet ds = activityController.GetDefinedSessionActivities(recordId);
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataRow dr = ds.Tables[0].Rows[0];
    //            //this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND IS_ACTIVE=1 ", "SESSIONNO DESC"); //Commented on 2022 Aug 30
    //            //this.objCommon.FillDropDownList(ddlActivity, "ACTIVITY_MASTER WITH (NOLOCK)", "ACTIVITY_NO", "ACTIVITY_NAME", string.Empty, "ACTIVITY_NO");
    //            ddlSession.SelectedValue = dr["SESSION_NO"].ToString();
    //            //string aa=ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
    //            //ddlSession.SelectedValue =Convert.ToInt32(aa);
    //            //ddlActivity.SelectedValue = dr["ACTIVITY_NO"].ToString();

    //            ddlExamPattern.SelectedValue = dr["PATTERNNO"].ToString();
    //            ddlExamPattern_SelectedIndexChanged(sender, e);
    //            ddlExamNo.SelectedValue = dr["EXAMNO"].ToString();
    //            ddlExamNo_SelectedIndexChanged(sender, e);
    //            ddlSubExamNo.SelectedValue = dr["SUBEXAMNO"].ToString();
    //            txtStartDate.Text = ((dr["START_DATE"].ToString() != string.Empty) ? ((DateTime)dr["START_DATE"]).ToShortDateString() : string.Empty);
    //            txtEndDate.Text = ((dr["END_DATE"].ToString() != string.Empty) ? ((DateTime)dr["END_DATE"]).ToShortDateString() : string.Empty);
    //            ddlCollege.SelectedValue = dr["COLLEGE_IDS"].ToString();

    //            for (int i = 0; i < ddlActivity.Items.Count; i++)
    //            {
    //                if (ddlActivity.Items[i].Value.ToString() == dr["ACTIVITY_NO"].ToString())
    //                {
    //                    ddlActivity.Items[i].Selected = true;
    //                }
    //            }
    //            char delimiterChars = ',';

    //            int count = 0;
    //            int vcount = 0;
    //            PopulateDegreeList();
    //            //---------------------------added by Aayushi-------------------------------------------------------------------
    //            degrees = dr["DEGREENO"].ToString();
    //            string[] deg = degrees.Split(delimiterChars);

    //            count = 0;
    //            vcount = 0;

    //            for (int j = 0; j < deg.Length; j++)
    //            {
    //                for (int i = 0; i < chkDegreeList.Items.Count; i++)
    //                {
    //                    if (deg[j] == chkDegreeList.Items[i].Value)
    //                    {
    //                        chkDegreeList.Items[i].Selected = true;
    //                    }
    //                }
    //            }
    //            foreach (ListItem Item in chkDegreeList.Items)
    //            {
    //                vcount++;
    //                if (Item.Selected == true)
    //                {
    //                    count++;
    //                }
    //            }
    //            if (count == vcount)
    //            {
    //                chkDegree.Checked = true;
    //            }

    //            PopulateBranchList();
    //            PopulateSemesterList();
    //            PopulatUserRightsList();


    //            branches = dr["BRANCH"].ToString();
    //            string[] branch = branches.Split(delimiterChars);
    //            count = 0;
    //            vcount = 0;
    //            for (int j = 0; j < branch.Length; j++)
    //            {
    //                for (int i = 0; i < chkBranchList.Items.Count; i++)
    //                {
    //                    if (branch[j] == chkBranchList.Items[i].Value)
    //                    {
    //                        chkBranchList.Items[i].Selected = true;
    //                    }
    //                }
    //            }

    //            foreach (ListItem Item in chkBranchList.Items)
    //            {
    //                vcount++;
    //                if (Item.Selected == true)
    //                {
    //                    count++;
    //                }
    //            }

    //            if (count == vcount)
    //            {
    //                chkBranch.Checked = true;
    //            }

    //            semester = dr["SEMESTER"].ToString();
    //            string[] sem = semester.Split(delimiterChars);
    //            count = 0;
    //            vcount = 0;
    //            for (int j = 0; j < sem.Length; j++)
    //            {
    //                for (int i = 0; i < chkSemesterList.Items.Count; i++)
    //                {
    //                    if (sem[j] == chkSemesterList.Items[i].Value)
    //                    {
    //                        chkSemesterList.Items[i].Selected = true;
    //                    }
    //                }
    //            }
    //            foreach (ListItem Item in chkSemesterList.Items)
    //            {
    //                vcount++;
    //                if (Item.Selected == true)
    //                {
    //                    count++;
    //                }
    //            }
    //            if (count == vcount)
    //            {
    //                chkSemester.Checked = true;
    //            }

    //            UserTypes = dr["UserType"].ToString();
    //            string[] utype = UserTypes.Split(delimiterChars);
    //            count = 0;
    //            vcount = 0;
    //            for (int j = 0; j < utype.Length; j++)
    //            {
    //                for (int i = 0; i < chkUserRightsList.Items.Count; i++)
    //                {
    //                    if (utype[j] == chkUserRightsList.Items[i].Value)
    //                    {
    //                        chkUserRightsList.Items[i].Selected = true;
    //                    }
    //                }
    //            }
    //            foreach (ListItem Item in chkUserRightsList.Items)
    //            {
    //                vcount++;
    //                if (Item.Selected == true)
    //                {
    //                    count++;
    //                }
    //            }
    //            if (count == vcount)
    //            {
    //                chkUserRights.Checked = true;
    //            }

    //            //------  Added By Rishabh ON 02/11/2021
    //            if (dr["STARTED"].ToString() == "Started")
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(true);", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(false);", true);
    //            }

    //            if (dr["SHOW_STATUS"].ToString() == "1")
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(true);", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
    //            }
    //            //--------------------------------------------------------------------------------------
    //            //if (dr["SHOW_STATUS"].ToString().Equals("1"))
    //            //{
    //            //    rdoYes.Checked = true;
    //            //    rdoNo.Checked = false;
    //            //}
    //            //else
    //            //{
    //            //    rdoYes.Checked = false;
    //            //    rdoNo.Checked = true;
    //            //}

    //            //if (dr["STARTED"].ToString().ToLower().Equals("true"))
    //            //{
    //            //    rdoStart.Checked = true;
    //            //    rdoStop.Checked = false;
    //            //}
    //            //else
    //            //{
    //            //    rdoStart.Checked = false;
    //            //    rdoStop.Checked = true;
    //            //}

    //            ViewState["sessionactivityno"] = dr["SESSION_ACTIVITY_NO"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void PopulateBranchList()
    {
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkDegreeList.Items)
            {

                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }

            if (count > 0)
            {
                //string Degreenos = values.TrimEnd(',');
                //ViewState["values"] += values;
                //string degreenos = ViewState["values"].ToString();
                string degNos = values.TrimEnd(',');
                //DataSet ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "B.BRANCHNO", "B.SHORTNAME +' ('+D.CODE+')' SHORTNAME", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "CD.DEGREENO");   //Commented on 2022 Aug 30

                DataSet ds = BindBranchWithMultipleCollege(degNos);
                int organizatiionid = Convert.ToInt32(Session["OrgId"]);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkBranchList.Visible = true;
                        if (organizatiionid == 2)//Added by crescent sachin 06-12-2022 
                        {
                            chkBranchList.DataTextField = "CODE";
                        }
                        else
                        {
                            chkBranchList.DataTextField = "SHORTNAME";
                        }
                        //chkBranchList.DataTextField = "SHORTNAME";
                        chkBranchList.DataValueField = "BRANCHNO";
                        chkBranchList.ToolTip = "DURATION";
                        chkBranchList.DataSource = ds.Tables[0];
                        chkBranchList.DataBind();
                    }

                }
                else
                {
                    chkBranchList.Visible = false;
                }
                //else
                //{
                //    chkBranchList.DataSource = null;
                //    chkBranchList.DataBind();
                //}            
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void PopulateSemesterList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <>0", "SEMESTERNO");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkSemesterList.DataTextField = "SEMESTERNAME";
                    chkSemesterList.DataValueField = "SEMESTERNO";
                    chkSemesterList.DataSource = ds.Tables[0];
                    chkSemesterList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void chkInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateDegreeList();
    }

    protected void chkDegreeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkBranchList.Items.Clear();
        chkSemesterList.Items.Clear();
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkDegreeList.Items)
            {

                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }
            ViewState["Degnos"] = values;
            if (count > 0)
            {
                PopulateBranchList();
                PopulateSemesterList();
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateExam();
    }
    protected void ddlExamNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateSubExam();
    }
    private void PopulateActivity()
    {

        try
        {
            // objCommon.FillListBox(ddlActivity, "ACTIVITY_MASTER A LEFT JOIN ACD_SUBEXAM_NAME B ON(A.SUBEXAMNO=B.SUBEXAMNO) LEFT JOIN ACD_EXAM_PATTERN C ON (C.PATTERNNO=B.PATTERNNO) LEFT JOIN ACD_EXAM_NAME D ON (A.EXAMNO=D.EXAMNO) LEFT JOIN ACD_EXAM_NAME E ON (A.EXAMNO=E.EXAMNO)", "DISTINCT A.ACTIVITY_NO", "CONCAT(ACTIVITY_NAME,' - ',C.PATTERN_NAME ,' - ',B.SUBEXAMNAME) ACTIVITY_NAME", "(A.EXAMNO=" + Convert.ToInt32(ddlExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlExamNo.SelectedValue) + "=0) AND (A.SUBEXAMNO = " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + "=0) AND (D.PATTERNNO = " + Convert.ToInt32(ddlExamPattern.SelectedValue) + " OR " + Convert.ToInt32(ddlExamPattern.SelectedValue) + "=0)", "A.ACTIVITY_NO");

            //objCommon.FillDropDownList(ddlActivity, "ACTIVITY_MASTER A LEFT JOIN ACD_SUBEXAM_NAME B ON(A.SUBEXAMNO=B.SUBEXAMNO) LEFT JOIN ACD_EXAM_PATTERN C ON (C.PATTERNNO=B.PATTERNNO) LEFT JOIN ACD_EXAM_NAME D ON (A.EXAMNO=D.EXAMNO) LEFT JOIN ACD_EXAM_NAME E ON (A.EXAMNO=E.EXAMNO)", "DISTINCT A.ACTIVITY_NO", "CONCAT(ACTIVITY_NAME,' - ',C.PATTERN_NAME ,' - ',B.SUBEXAMNAME) ACTIVITY_NAME", "(A.EXAMNO=" + Convert.ToInt32(ddlExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlExamNo.SelectedValue) + "=0) AND (A.SUBEXAMNO = " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + "=0) AND (D.PATTERNNO = " + Convert.ToInt32(ddlExamPattern.SelectedValue) + " OR " + Convert.ToInt32(ddlExamPattern.SelectedValue) + "=0)", "A.ACTIVITY_NO");  //Commened On 28-2-23 by Injamam


            objCommon.FillDropDownList(ddlActivity, "ACTIVITY_MASTER A LEFT JOIN ACD_SUBEXAM_NAME B ON(A.SUBEXAMNO=B.SUBEXAMNO) LEFT JOIN ACD_EXAM_PATTERN C ON (C.PATTERNNO=B.PATTERNNO) LEFT JOIN ACD_EXAM_NAME D ON (A.EXAMNO=D.EXAMNO) LEFT JOIN ACD_EXAM_NAME E ON (A.EXAMNO=E.EXAMNO)", "DISTINCT A.ACTIVITY_NO", "CONCAT(ACTIVITY_NAME,' - ',C.PATTERN_NAME ,' - ',B.SUBEXAMNAME) ACTIVITY_NAME", "(A.EXAMNO=" + Convert.ToInt32(ddlExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlExamNo.SelectedValue) + "=0) AND (A.SUBEXAMNO = " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + " OR " + Convert.ToInt32(ddlSubExamNo.SelectedValue) + "=0) AND (D.PATTERNNO = " + Convert.ToInt32(ddlExamPattern.SelectedValue) + " OR " + Convert.ToInt32(ddlExamPattern.SelectedValue) + "=0) AND (ISNULL(ASSIGN_TO,0)=0)", "A.ACTIVITY_NO");
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    private void PopulateSubExam()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubExamNo, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "EXAMNO = " + Convert.ToInt32(ddlExamNo.SelectedValue) + " AND SUBEXAMNAME <> ''", "SUBEXAMNO");
            ddlSubExamNo.Focus();

            PopulateActivity();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void PopulateExam()
    {
        try
        {
            if (ddlExamPattern.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlExamNo, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "PATTERNNO=" + ddlExamPattern.SelectedValue + "AND EXAMNAME <> ''", "EXAMNO");
                ddlExamNo.Focus();
                ViewState["ExamPattern"] = Convert.ToInt32(ddlExamPattern.SelectedValue);

                PopulateActivity();
            }
            else
            {
                ddlSubExamNo.Items.Clear();
                ddlSubExamNo.Items.Add(new ListItem("Please Select", "0"));
                ddlExamNo.Items.Clear();
                ddlExamNo.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSubExamNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopulateActivity();

            ddlActivity.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    //Code Added for multiple College selection and session selection on 2022 Aug 30

    private void MultipleCollegeBind()
    {
        try
        {
            // add by maithili [07-09-2022]
            DataSet ds = null;
            ds = AcadDash.Get_CollegeID_Sessionno(1, "");

            ddlCollege.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.DataSource = ds;
                ddlCollege.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlCollege.DataTextField = ds.Tables[0].Columns[4].ToString();
                ddlCollege.DataBind();
            }
            //end 

            //objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch
        {
            throw;
        }
    }

    private void MultipleSessionBind(string CollgeIds)
    {
        try
        {
            objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollgeIds + "',',')) AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCollege_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["EDIT_HIT"]) == "1")
        {

        }
        else
        {
            chkBranchList.Items.Clear();
            chkSemesterList.Items.Clear();
            chkDegreeList.Items.Clear();
            string collegenose = string.Empty;
            //
            string collegenos = string.Empty;
            //string collegenames = string.Empty;
            foreach (ListItem items in ddlCollege.Items)
            {
                if (items.Selected == true)
                {
                    collegenos += (items.Value).Split('-')[0] + ','; //Add by maithili [08-09-2022]

                    //collegenos += items.Value + ','; 
                    //collegenames += items.Text + ',';
                }
            }
            if (collegenos.Length > 1)
            {
                collegenos = collegenos.Remove(collegenos.Length - 1);
            }

            if (collegenos.Length > 0)
            {
                MultipleSessionBind(collegenos);
                ddlSession.Focus();
                //PopulateDegreeList();
                BindDegreeWithMultipleCollege(collegenos);
            }
            else
            {
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
            }
        }
    }

    private void BindDegreeWithMultipleCollege(string CollegeIds)
    {
        //DataSet ds = new DataSet();
        //DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        //objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollgeIds + "',',')) AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
        DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "DISTINCT CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "CD.DEGREENO");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkDegreeList.DataTextField = "CODE";
                chkDegreeList.DataValueField = "DEGREENO";
                chkDegreeList.ToolTip = "DEGREENO";
                //ViewState["YearWise"] = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "ISNULL(YEARWISE,0)YEARWISE", "DEGREENO=" + ds.Tables[0].Rows[0]["DEGREENO"].ToString()));
                chkDegreeList.DataSource = ds.Tables[0];
                chkDegreeList.DataBind();
            }
        }
    }

    private DataSet BindBranchWithMultipleCollege(string degNos)
    {
        string CollegeIds = GetSelectedCollegeIds();

        DataSet ds = new DataSet();
        int OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (OrganizationId == 2)   //Added by crescent sachin 06-12-2022
        {
            ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "DISTINCT B.BRANCHNO", "CD.CODE", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "CODE");
        }
        else
        {
            ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "DISTINCT B.BRANCHNO", "B.SHORTNAME +' ('+D.CODE+')' SHORTNAME", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "SHORTNAME");
        }

        return ds;
    }

    private Boolean CheckSessionSelected()
    {
        Boolean IsSessionSelected = false;

        string SessionNos = string.Empty;
        //foreach (ListItem items in ddlSession.Items)
        foreach (ListItem items in ddlCollege.Items)//add by maithili [08-09-2022]
        {
            if (items.Selected == true)
            {
                //SessionNos += items.Value + ',';
                SessionNos += (items.Value).Split('-')[1] + ','; //add by maithili [08-09-2022]
            }
        }
        if (SessionNos.Length < 1)
        {
            IsSessionSelected = false;
        }
        else if (SessionNos.Length > 0)
        {
            IsSessionSelected = true;
        }
        return IsSessionSelected;
    }


    private string GetSelectedCollegeIds()
    {
        string CollegeIds = string.Empty;
        foreach (ListItem items in ddlCollege.Items)
        {
            if (items.Selected == true)
            {
                //CollegeIds += items.Value + ',';
                CollegeIds += (items.Value).Split('-')[0] + ','; // Add by maithili [08-09-2022]
            }
        }
        if (CollegeIds.Length > 1)
        {
            CollegeIds = CollegeIds.Remove(CollegeIds.Length - 1);
        }
        return CollegeIds;
    }

    private Boolean PreparingForInsertRecord(SessionActivity sessionActiity, ref string branch, ref string semester, ref string degreeno, ref string UserTypes, int College_Ids)
    {
        Boolean IsValid = false;
        //checking for College_Ids session Active for specific College
        DataSet ds = new DataSet();
        DataSet dsCDB = new DataSet();
        DataRow[] drSessionforClg;
        DataRow[] drCDB;
        if (ViewState["dsSessionData"] != null)
        {
            ds = (DataSet)ViewState["dsSessionData"];
        }
        if (ViewState["dsClgDegBrn"] != null)
        {
            dsCDB = (DataSet)ViewState["dsClgDegBrn"];
        }

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //dr = ds.Tables[0].Select("OrganizationId = " + College_Ids + " AND COLLEGE_ID = " + College_Ids);
                drSessionforClg = ds.Tables[0].Select("OrganizationId = " + Session["OrgId"].ToString() + " and COLLEGE_ID = " + College_Ids + " AND SESSIONNO = " + sessionActiity.SessionNo);
                if (drSessionforClg.Length > 0)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                    return IsValid;
                }
            }
        }
        degrees = "";
        for (int i = 0; i < chkDegreeList.Items.Count; i++)
        {
            if (chkDegreeList.Items[i].Selected)
            {
                drCDB = dsCDB.Tables[0].Select("OrganizationId = " + Session["OrgId"].ToString() + " and COLLEGE_ID = " + College_Ids + " AND DEGREENO = " + chkDegreeList.Items[i].Value);
                if (drCDB.Length > 0)
                {
                    degrees += chkDegreeList.Items[i].Value + ",";
                }
            }
        }
        branches = "";
        string degreess = degrees.TrimEnd(',');
        for (int i = 0; i < chkBranchList.Items.Count; i++)
        {
            if (chkBranchList.Items[i].Selected)
            {
                drCDB = dsCDB.Tables[0].Select("OrganizationId = " + Session["OrgId"].ToString() + " and COLLEGE_ID = " + College_Ids + " AND DEGREENO IN (" + degreess.TrimEnd(',') + ") AND BRANCHNO = " + chkBranchList.Items[i].Value);
                if (drCDB.Length > 0)
                {
                    branches += chkBranchList.Items[i].Value + ",";
                }
            }
        }

        if (!string.IsNullOrEmpty(branches))
        {
            branches = branches.Substring(0, branches.Length - 1);
        }
        if (!string.IsNullOrEmpty(degrees))
        {
            degrees = degrees.Substring(0, degrees.Length - 1);
        }

        return IsValid;
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //  Dictionary<string,string> listCollege = new Dictionary<string,string>();

            ImageButton btnEditRecord = sender as ImageButton;
            int recordId = int.Parse(btnEditRecord.CommandArgument);
            DataSet ds = activityController.GetDefinedSessionActivities(recordId);
            ViewState["CollegeSession"] = ds;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //ddlSession.SelectedValue = dr["SESSION_NO"].ToString();

                //objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillListBox(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + dr["College_IDS"].ToString() + "',',')) AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

                MultipleCollegeBind(); //add by maithili [08-09-2022]

                //9-58 session_Id:->CollegeID-SESSIONNO


                //  string College_Ids = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();

                string College_Ids = ds.Tables[0].Rows[0]["SESSION_NO"].ToString();

                if (!College_Ids.Contains("-"))
                {
                    College_Ids = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString() + "-" + ds.Tables[0].Rows[0]["SESSION_NO"].ToString();
                }

                //string College_Ids = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();

                for (int j = 0; j < College_Ids.ToString().Length; j++)
                {
                    for (int i = 0; i < ddlCollege.Items.Count; i++)
                    {
                        if (College_Ids.ToString() == ddlCollege.Items[i].Value)
                        {
                            ddlCollege.Items[i].Selected = true;
                            //ddlCollege.Items[i].Attributes.Add("disabled", "disabled") ;
                        }
                        // listCollege.Add(ddlCollege.Items[i].Value, ddlCollege.Items[i].Selected.ToString());
                    }
                }

                string collegenos = College_Ids.Split('-')[0];

                if (collegenos.Length > 0)
                {
                    MultipleSessionBind(collegenos);
                    ddlSession.Focus();
                    BindDegreeWithMultipleCollege(collegenos);
                }
                else
                {
                    ddlSession.Items.Clear();
                    ddlSession.Items.Add(new ListItem("Please Select", "0"));
                }

                Session["College_IDS"] = Convert.ToInt32(collegenos); //add by maithili [08-09-2022s]

                // comment by maithili [08-09-2022]
                //foreach (ListItem items in ddlSession.Items)
                //{
                //    if (items.Value == dr["SESSION_NO"].ToString())
                //    {
                //        items.Selected = true;
                //    }
                //}
                //foreach (ListItem items in ddlCollege.Items)
                //{
                //    if (items.Value == dr["College_IDS"].ToString())
                //    {
                //        items.Selected = true;
                //        Session["College_IDS"] = Convert.ToInt32(items.Value);
                //        Session["College_IDS"] = Convert.ToInt32((items.Value).Split('-')[0]); //add by maithili [08-09-2022s]
                //    }
                //}



                ddlExamPattern.SelectedValue = dr["PATTERNNO"].ToString();
                ddlExamPattern_SelectedIndexChanged(sender, e);
                ddlExamNo.SelectedValue = dr["EXAMNO"].ToString();
                ddlExamNo_SelectedIndexChanged(sender, e);

                string ss = string.IsNullOrEmpty(dr["SUBEXAMNO"].ToString()) ? "" : dr["SUBEXAMNO"].ToString();
                if (!string.IsNullOrEmpty(ss))
                {
                    if (ddlSubExamNo.Items.Count == 1)
                    {
                        ddlSubExamNo.SelectedValue = "0";
                    }
                    else
                    {
                        ddlSubExamNo.SelectedValue = string.IsNullOrEmpty(dr["SUBEXAMNO"].ToString()) ? "0" : dr["SUBEXAMNO"].ToString();

                        ddlSubExamNo.SelectedValue = dr["SUBEXAMNO"].ToString();
                    }
                }
                txtStartDate.Text = ((dr["START_DATE"].ToString() != string.Empty) ? ((DateTime)dr["START_DATE"]).ToShortDateString() : string.Empty);
                txtEndDate.Text = ((dr["END_DATE"].ToString() != string.Empty) ? ((DateTime)dr["END_DATE"]).ToShortDateString() : string.Empty);
                //ddlCollege.SelectedValue = dr["COLLEGE_IDS"].ToString();

                /*for (int i = 0; i < ddlActivity.Items.Count; i++)
                {
                    if (ddlActivity.Items[i].Value.ToString() == dr["ACTIVITY_NO"].ToString())
                    {
                        ddlActivity.Items[i].Selected = true;
                    }
                }*/
                if (!string.IsNullOrEmpty(dr["ACTIVITY_NO"].ToString()))
                {
                    ddlActivity.SelectedValue = dr["ACTIVITY_NO"].ToString();
                }

                char delimiterChars = ',';

                int count = 0;
                int vcount = 0;
                PopulateDegreeList();
                //---------------------------added by Aayushi-------------------------------------------------------------------
                degrees = dr["DEGREENO"].ToString();
                string[] deg = degrees.Split(delimiterChars);

                count = 0;
                vcount = 0;

                for (int j = 0; j < deg.Length; j++)
                {
                    for (int i = 0; i < chkDegreeList.Items.Count; i++)
                    {
                        if (deg[j] == chkDegreeList.Items[i].Value)
                        {
                            chkDegreeList.Items[i].Selected = true;
                        }
                    }
                }
                foreach (ListItem Item in chkDegreeList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }
                if (count == vcount)
                {
                    chkDegree.Checked = true;
                }

                PopulateBranchList();
                PopulateSemesterList();
                PopulatUserRightsList();


                branches = dr["BRANCH"].ToString();
                string[] branch = branches.Split(delimiterChars);
                count = 0;
                vcount = 0;
                for (int j = 0; j < branch.Length; j++)
                {
                    for (int i = 0; i < chkBranchList.Items.Count; i++)
                    {
                        if (branch[j] == chkBranchList.Items[i].Value)
                        {
                            chkBranchList.Items[i].Selected = true;
                        }
                    }
                }

                foreach (ListItem Item in chkBranchList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }

                if (count == vcount)
                {
                    chkBranch.Checked = true;
                }

                semester = dr["SEMESTER"].ToString();
                string[] sem = semester.Split(delimiterChars);
                count = 0;
                vcount = 0;
                for (int j = 0; j < sem.Length; j++)
                {
                    for (int i = 0; i < chkSemesterList.Items.Count; i++)
                    {
                        if (sem[j] == chkSemesterList.Items[i].Value)
                        {
                            chkSemesterList.Items[i].Selected = true;
                        }
                    }
                }
                foreach (ListItem Item in chkSemesterList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }
                if (count == vcount)
                {
                    chkSemester.Checked = true;
                }

                UserTypes = dr["UserType"].ToString();
                string[] utype = UserTypes.Split(delimiterChars);
                count = 0;
                vcount = 0;
                for (int j = 0; j < utype.Length; j++)
                {
                    for (int i = 0; i < chkUserRightsList.Items.Count; i++)
                    {
                        if (utype[j] == chkUserRightsList.Items[i].Value)
                        {
                            chkUserRightsList.Items[i].Selected = true;
                        }
                    }
                }
                foreach (ListItem Item in chkUserRightsList.Items)
                {
                    vcount++;
                    if (Item.Selected == true)
                    {
                        count++;
                    }
                }
                if (count == vcount)
                {
                    chkUserRights.Checked = true;
                }
                if (dr["STARTED"].ToString() == "Started")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "SetStatActive(false);", true);
                }

                if (dr["SHOW_STATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
                }

                ViewState["sessionactivityno"] = dr["SESSION_ACTIVITY_NO"].ToString();
            }
            //   ddlCollege.Enabled = false;
            ddlCollege.Attributes.Add("disabled", "disabled");
            ddlActivity.Attributes.Add("disabled", "disabled");
            ddlExamPattern.Enabled = false;
            ddlExamNo.Enabled = false;
            ddlSubExamNo.Enabled = false;

            ViewState["action"] = "edit";
            ViewState["EDIT_HIT"] = "1";
        }
        catch (Exception ex)
        {
            throw;
        }
    }


}