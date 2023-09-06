using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using mastersofterp_MAKAUAT;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
//using mastersofterp_MAKAUAT ;
//using System.IO;
using System.Net.Security;
//using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using RestSharp;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using BusinessLogicLayer.BusinessLogic;



public partial class ACADEMIC_StudentRegistration_Jecrc : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    StudentRegistration objRegistration = new StudentRegistration();
    User_AccController objUC = new User_AccController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string collegeCode = string.Empty;
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
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

        try
            {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
                {
                Response.Redirect("~/default.aspx");
                }

            if (!Page.IsPostBack)
                {
                ////********** Added by Rahul Moraskar 2022-07-26
                ControlInit();
                RequiredFieldValidatorEnableDisable();
                DefaultHideContorls();
                ////********** END by Rahul Moraskar 2022-07-26

                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                PopulateDropDownList();
                if (Request.Params["__EVENTTARGET"] != null &&
                                Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                    {
                    if (Request.Params["__EVENTTARGET"].ToString() == "savedata")
                        {
                        this.savedata();

                        }
                    }


                ViewState["Otp"] = null;
                txtREGNo.Enabled = ((objCommon.LookUp("REFF", "ENROLLMENTNO", "") == "False") ? false : true);
                if (txtREGNo.Enabled == true)
                    {
                    watREGNo.WatermarkText = "Enter Application ID";
                    }
                else
                    {
                    // watREGNo.WatermarkText = "Automatic Registration No. Generation";
                    }

                //txtDateOfAdmission.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateOfReporting.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //btnReport.Enabled = false;

                //Check Student registration Activityp
                // CheckActivity();
                txtapplicationid.Enabled = false;

                if (ViewState["stuinfoidno"].ToString() != "")
                    {
                    ShowStudentDetails();
                    ///  myModal2.Visible = true;
                    }
                else
                    {

                    }

                }

            else
                {
                    //PopulateDropDownList();
                if (Request.Params["__EVENTTARGET"] != null &&
                               Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                    {
                    if (Request.Params["__EVENTTARGET"].ToString() == "savedata")
                        {
                        this.savedata();

                        }
                    }
                if (Page.Request.Params["__EVENTTARGET"] != null)
                    {
                    if (rbName.Checked)
                        {
                        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                            {
                            string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                            bindlist(arg[0], arg[1]);
                            }
                        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                            {
                            txtSearch.Text = string.Empty;
                            lvStudent.DataSource = null;

                            lvStudent.DataBind();
                            lblNoRecords.Text = string.Empty;
                            }
                        }

                    else
                        {
                        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch1"))
                            {
                            string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                            bindlistsearchprospectusno(arg[0], arg[1]);
                            //bindlistsearchTempIDNo(arg[0], arg[1]);

                            }
                        if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                            {
                            txtSearch.Text = string.Empty;
                            lvstudentprospectusno.DataSource = null;
                            LVtempid.DataSource = null;
                            LVtempid.DataBind();
                            lvstudentprospectusno.DataBind();
                            lblNoRecords.Text = string.Empty;
                            }
                        }

                    }


                if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 2 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    divMsg.InnerHtml = string.Empty;

                    DataSet dsGet = (DataSet)Session["STUD_DETAILS"];
                    if (dsGet != null)//&& dsGet.Tables[0].Rows.Count>0
                        {
                        getSessionDetails(dsGet);
                        }
                    }
                else
                    {
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    divMsg.InnerHtml = string.Empty;

                    DataSet dsGet = (DataSet)Session["STUD_ONLINEADMDETAILS"];
                    if (dsGet != null)//&& dsGet.Tables[0].Rows.Count>0
                        {
                        getSessionDetails(dsGet);
                        }

                    }
                }





            }
        catch (Exception ex)
            {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentRegistration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
            }
        }

    private void DefaultHideContorls()
        {
        divschmode.Visible = false;
        divAmt.Visible = false;
        divHostel.Visible = false;
        divSpecialisation.Visible = false;

        divinstaltype.Visible = false;
        DivDuedate1.Visible = false;
        DivDuedate2.Visible = false;
        DivDuedate3.Visible = false;
        DivDuedate4.Visible = false;
        DivdueDate5.Visible = false;
        }
    private void CheckPageAuthorization()
        {
        if (Request.QueryString["pageno"] != null)
            {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                Response.Redirect("~/notauthorized.aspx?page=StudentRegistration.aspx");
                }
            }
        else
            {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentRegistration.aspx");
            }
        }

    private void CheckActivity()
        {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'STUDENTRY'");

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
            {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                dvMain.Visible = false;


                }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                dvMain.Visible = false;
                }

            }
        else
            {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            dvMain.Visible = false;
            }
        dtr.Close();
        }

    protected void PopulateDropDownList()
        {
        try
            {

            // objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')COLLEGE_NAME", "COLLEGE_ID > 0 AND ActiveStatus=1", "COLLEGE_NAME");
            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                {
                ddlSchool.SelectedValue = "1";
                }
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1", "BATCHNO DESC");
            //ddlBatch.SelectedValue = "1";
            // ddlBatch.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
            // ddlYear.SelectedValue = "1";
            objCommon.FillDropDownList(ddlClaimedCat, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORY");

            // objCommon.FillDropDownList(ddlCategory, "ACD_SRCATEGORY", "srcategoryno", "srcategory", "srcategoryno > 0", "srcategory");

            // FILL DROPDOWN SEMESTER
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
            //ddlSemester.SelectedValue = "1";
            // FILL DROPDOWN PAYMENT TYPE
            objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");
            // ddlPaymentType.SelectedValue = "1";
            //objCommon.FillDropDownList(ddlclaim, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
            //fill dropdown id type
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
            //ddlAdmType.SelectedValue = "1";

            //fill dropdown adm round
            objCommon.FillDropDownList(ddlAdmRound, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0", "ROUNDNAME");
            //fill dropdown adm quota
            objCommon.FillDropDownList(ddlQuota, "ACD_QUOTA", "QUOTANO", "QUOTA", "QUOTANO>0", "QUOTANO");

            objCommon.FillDropDownList(ddlReligion, "ACD_RELIGION", "RELIGIONNO", "RELIGION", "RELIGIONNO > 0 AND ACTIVESTATUS=1", "RELIGION");
            objCommon.FillDropDownList(ddlNationality, "ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO > 0 AND ACTIVESTATUS=1", "NATIONALITY");
            objCommon.FillDropDownList(ddlAllotedCat, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORY");
            objCommon.FillDropDownList(ddlBloodGrp, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO > 0 AND ACTIVESTATUS=1", "BLOODGRPNAME");
            objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORYNO");
            //fill dropdown adm quota
            //objCommon.FillDropDownList(ddlBloodGroup, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO");
            this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "ACTIVESTATUS=1", "BANKNAME");
            //objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "QUALIFYNO");
            objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIEXMNAME");

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0 AND ACTIVESTATUS=1", "SECTIONNO ASC");
            objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");
            //ddlstate.SelectedValue = "5";
            objCommon.FillDropDownList(ddlSchType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO>0", "SCHOLORSHIPTYPENO");

            objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
            //ddladmthrough.SelectedValue = "1";
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnSave_Click(object sender, EventArgs e)
        {
        //ddlBatch.Enabled = true;
        try
            {


            int checkSeatCapacity = Convert.ToInt32(objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(CHECK_SEAT_CAPACITY_NEW_STUD,0) CHECK_SEAT_CAPACITY_NEW_STUD", ""));

            if (checkSeatCapacity == 1)
                {
                int Intake = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH_INTAKE", "ISNULL(MAX(INTAKE),0) BRANCH_INTAKE", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue)));

                if (Intake > 0)
                    {
                    int ActualSeats = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COUNT(IDNO) IDNO", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue)));
                    if (ActualSeats > Intake || ActualSeats == Intake)
                        {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmhostel();", true);
                        }
                    else
                        {
                        savedata();
                        }
                    }
                else
                    {
                    savedata();
                    }
                }
            else
                {
                savedata();
                }

            }
        catch (Exception ex)
            {
            throw;
            }

        }

    private DataSet getModuleConfig()
        {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
        }

    private int OutLook_Email(string Message, string toEmailId, string sub)
        {

        int ret = 0;
        try
            {
            Common objCommon = new Common();
            DataSet dsconfig = null;

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oMail.To = toEmailId;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            Console.WriteLine("start to send email over TLS...");
            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
            Console.WriteLine("email sent successfully!");
            ret = 1;
            }
        catch (Exception ep)
            {
            Console.WriteLine("failed to send email with the following error:");
            Console.WriteLine(ep.Message);
            ret = 0;
            }
        return ret;
        }

    private string GeneratePassword()
        {
        string allowedChars = "";
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 7; i++)
            {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
            }
        return passwordString;

        //-----------------OR---------------------
        //Random randNum = new Random();
        //int PasswordLength = 10;
        //char[] chars = new char[PasswordLength];
        //int allowedCharCount = allowedChars.Length;
        //for (int i = 0; i < PasswordLength; i++)
        //{
        //    chars[i] = allowedChars[(int)((allowedChars.Length) * randNum.NextDouble())];
        //}
        //return new string(chars);
        //--------------------------------------
        }

    private void DisableControlsRecursive(Control root)
        {
        if (root is TextBox)
            {
            ((TextBox)root).Text = string.Empty;
            }
        if (root is DropDownList)
            {
            ((DropDownList)root).SelectedIndex = 0;
            }
        foreach (Control child in root.Controls)
            {
            DisableControlsRecursive(child);
            }
        ddlBatch.SelectedIndex = 0;
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        // clearcontrols();
        Response.Redirect(Request.Url.ToString());
        }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlSchool.SelectedIndex > 0)
            {
            //if (Convert.ToInt32(Session["OrgId"]) == 1)
            //{
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENAME");
            ddlDegree.Focus();
            }
        else
            {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            //ddlBranch.Items.Clear();

            }
        //ddlDegree.Items.Clear();
        ddlDegree.SelectedIndex = 0;
        // ddlBranch.Items.Clear();
        ddlBranch.SelectedIndex = 0;
        }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlSchool.SelectedIndex > 0)
            {

            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(B.ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
            ddlBranch.Focus();
            }
        else
            {
            //ddlBranch.Items.Clear();
            ddlBranch.SelectedIndex = 0;
            objCommon.DisplayMessage(updStudent, "Please select college/school!", this.Page);
            return;
            }
        //ddlBranch.Items.Clear();
        ddlBranch.SelectedIndex = 0;

        }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
        {
        try
            {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + "Admission_Slip_Report";
            url += "&path=~,Reports,Academic," + "rptStudAdmSlip_New.rpt";//"rptStudentRegistrationSlip.rpt";

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(Session["output"]);

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");


            //To open report with update panel
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);

            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private void ShowReportnew(string reportTitle, string rptFileName, string regno)
        {
        try
            {

            int IDNO = 0;
            if (txtREGNo.Text == string.Empty)
                {
                IDNO = Convert.ToInt32(Session["output"]);
                }
            else
                {

                IDNO = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "IDNO", "APPLICATIONID='" + Convert.ToString(txtREGNo.Text) + "'"));
                }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(IDNO);

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

    private DataTable GetDemandDraftDataTable()
        {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("IDNO", typeof(int)));
        dt.Columns.Add(new DataColumn("BRANCHNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ADMBATCH", typeof(int)));
        dt.Columns.Add(new DataColumn("DDNO", typeof(string)));
        dt.Columns.Add(new DataColumn("DDDATE", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("DDAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("BANKNO", typeof(int)));
        dt.Columns.Add(new DataColumn("CITYNO", typeof(int)));
        dt.Columns.Add(new DataColumn("BANKNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CITYNAME", typeof(string)));

        dt.Columns.Add(new DataColumn("RECEIPTNO", typeof(int)));
        dt.Columns.Add(new DataColumn("RECEIPTAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("RECEIPTDATE", typeof(DateTime)));
        return dt;
        }

    private DataRow GetEditableDataRow(DataTable dt, string value)
        {
        DataRow dataRow = null;
        try
            {
            foreach (DataRow dr in dt.Rows)
                {
                if (dr["DDNO"].ToString() == value)
                    {
                    dataRow = dr;
                    break;
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return dataRow;
        }

    private void BindDDInfo(ref GEC_Student[] gecStudent)
        {
        DataTable dt;
        if (Session["DDINFO"] != null && ((DataTable)Session["DDINFO"]) != null)
            {
            int index = 0;
            dt = (DataTable)Session["DDINFO"];
            gecStudent = new GEC_Student[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
                {
                GEC_Student objGecStud = new GEC_Student();
                //objGecStud.IdNo = Convert.ToInt32(txtIDNo.Text.Trim());
                objGecStud.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                objGecStud.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
                objGecStud.DdNo = dr["DDNO"].ToString();
                objGecStud.Dddate = Convert.ToDateTime(dr["DDDATE"]);
                objGecStud.DdAmount = dr["DDAMOUNT"].ToString();
                objGecStud.BankNo = Convert.ToInt32(dr["BANKNO"]);
                objGecStud.cityNo = Convert.ToInt32(dr["CITYNO"]);
                objGecStud.ReceiptNo = Convert.ToInt32(dr["RECEIPTNO"]);
                objGecStud.ReceiptAmount = dr["RECEIPTAMOUNT"].ToString();
                objGecStud.ReceiptDate = Convert.ToDateTime(dr["RECEIPTDATE"]);
                objGecStud.CollegeCode = Session["colcode"].ToString();
                gecStudent[index] = objGecStud;
                index++;
                }
            }

        }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
        this.ClearControls_DemandDraftDetails();
        objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "DEGREENO > 0", "QUALIFYNO");
        if (txtREGNo.Text.Trim() == string.Empty)
            {
            objCommon.DisplayMessage("Enter Registration No. to Modify!", this.Page);
            return;
            }

        rdoMale.Checked = true; // ADDED 05122021
        DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A,ACD_STU_ADDRESS B", "A.IDNO", "A.REGNO,A.IDNO,A.ROLLNO,A.STUDFIRSTNAME,A.STUDMIDDLENAME,A.STUDLASTNAME,A.FATHERFIRSTNAME,A.FATHERMIDDLENAME,A.FATHERLASTNAME,A.MOTHERNAME,A.DOB,A.SEX,A.RELIGIONNO,A.MARRIED,ISNULL(A.NATIONALITYNO,0)NATIONALITYNO,ISNULL(A.CATEGORYNO,0)CATEGORYNO,A.CASTE,A.ADMDATE,A.DEGREENO,A.BRANCHNO,A.YEAR,A.STUDENTMOBILE, A.SEMESTERNO,A.PTYPE,A.STATENO,A.ADMBATCH,A.IDTYPE,A.YEAR_OF_EXAM,A.ALL_INDIA_RANK,A.STATE_RANK,A.PERCENTAGE,A.PERCENTILE,A.QEXMROLLNO,A.ADMCATEGORYNO,A.QUALIFYNO,A.SCHOLORSHIPTYPENO,A.PHYSICALLY_HANDICAPPED,A.ADMROUNDNO,A.COLLEGE_CODE,B.STADDNO, B.IDNO, B.PADDRESS, B.PCITY, B.PSTATE, B.PPINCODE,A.EMAILID,B.PTELEPHONE,B.LADDRESS,B.LTELEPHONE,B.LMOBILE,B.LEMAIL,A.ADMQUOTANO,A.BLOODGRPNO,B.LCITY,B.LSTATE", "ISNULL(A.ADMCAN,0)=0 AND A.IDNO=B.IDNO AND A.IDNO = " + txtREGNo.Text.Trim(), string.Empty);
        if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
            if (dsStudent.Tables[0].Rows.Count > 0)
                {

                //txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                txtStudentName.Text = dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                txtStudentMiddleName.Text = dsStudent.Tables[0].Rows[0]["STUDMIDDLENAME"].ToString();
                txtStudentLastName.Text = dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERFIRSTNAME"].ToString();
                txtFatherMiddleName.Text = dsStudent.Tables[0].Rows[0]["FATHERMIDDLENAME"].ToString();
                txtFatherLastName.Text = dsStudent.Tables[0].Rows[0]["FATHERLASTNAME"].ToString();
                txtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                    {
                    rdoMale.Checked = true;
                    rdoFemale.Checked = false;
                    }
                else
                    {
                    rdoFemale.Checked = true;
                    rdoMale.Checked = false;
                    }

                //txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));//Commented by Irfan Shaikh on 20190405
                txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString());
                if (dsStudent.Tables[0].Rows[0]["MARRIED"].ToString().Trim().Equals("Y"))
                    {
                    rdoMarriedYes.Checked = true;
                    rdoMarriedNo.Checked = false;
                    }
                else
                    {
                    rdoMarriedYes.Checked = false;
                    rdoMarriedNo.Checked = true;
                    }
                ddlNationality.SelectedValue = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString());

                ddlCategory.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                txtPermanentAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                txtCity.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                txtState.Text = (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                txtPIN.Text = dsStudent.Tables[0].Rows[0]["PPINCODE"].ToString();
                txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                txtDateOfAdmission.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));
                ddlDegree.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()), "ACD_DEGREE", "DEGREENO", "DEGREENAME"));
                ddlBranch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), "ACD_BRANCH", "BRANCHNO", "LONGNAME"));
                ddlBatch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                ddlYear.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["YEAR"].ToString()), "ACD_YEAR", "YEAR", "YEARNAME"));
                ddlPaymentType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()), "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME"));
                txtStateOfEligibility.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                ddlAdmType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()), "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION"));
                txtAllIndiaRank.Text = dsStudent.Tables[0].Rows[0]["ALL_INDIA_RANK"].ToString();
                txtYearOfExam.Text = dsStudent.Tables[0].Rows[0]["YEAR_OF_EXAM"].ToString();
                txtStateRank.Text = dsStudent.Tables[0].Rows[0]["STATE_RANK"].ToString();
                txtPer.Text = dsStudent.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                txtQExamRollNo.Text = dsStudent.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                txtPercentile.Text = dsStudent.Tables[0].Rows[0]["PERCENTILE"].ToString();

                ddlAllotedCat.SelectedValue = (dsStudent.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString());
                ddlExamNo.SelectedValue = (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString());
                txtStudMobile.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                txtPostalAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                txtGuardianPhone.Text = dsStudent.Tables[0].Rows[0]["LTELEPHONE"].ToString();
                txtGuardianMobile.Text = dsStudent.Tables[0].Rows[0]["LMOBILE"].ToString();
                txtGuardianEmail.Text = dsStudent.Tables[0].Rows[0]["LEMAIL"].ToString();
                ddlBloodGrp.SelectedValue = (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString());

                ddlQuota.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString()), "ACD_QUOTA", "QUOTANO", "QUOTA"));
                txtLocalCity.Text = (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                txtLocalState.Text = (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                //PHYSICAL HADICAPP
                ddlPhyHandicap.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString());
                //ROUND FOR MCA
                ddladmthrough.SelectedValue = (dsStudent.Tables[0].Rows[0]["ADMROUNDNO"].ToString());
                ddlSemester.SelectedValue = (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                txtMobileNo.Text = (dsStudent.Tables[0].Rows[0]["PMOBILE"].ToString());
                //show the student photo and sign code
                int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "IDNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString())));
                if (idno > 0)
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=student";
                ImgSign.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString().ToString() + "&type=studentsign";

                //ADD SPOT OPTION FOR MTECH SPOT OPTION
                ddlSpotOption.SelectedValue = (dsStudent.Tables[0].Rows[0]["SCHOLORSHIPTYPENO"].ToString());

                //M.TECH SPOT ADMISSION AMOUNT TO BE PAID FOR DD

                //Condition for MCA only
                int degreeno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());
                if (degreeno == 4)
                    {
                    trMca.Visible = true;
                    }
                else
                    {
                    trMca.Visible = false;
                    }

                //Condition for the M.Tech(Appl. Geo) only

                if (degreeno == 2)
                    {
                    tblExam.Visible = false;
                    trExam.Visible = false;
                    }
                else
                    {
                    tblExam.Visible = true;
                    trExam.Visible = true;
                    }

                btnSave.Enabled = false;

                }
            else
                {
                objCommon.DisplayMessage("Please Enter Valid Registration No.!", this.Page);
                }
            }
        else
            {
            objCommon.DisplayMessage("Please Enter Valid Registration No.!", this.Page);
            }
        }

    protected void btnReport_Click(object sender, EventArgs e)
        {

        if (Session["OrgId"].ToString().Equals("1") || Session["OrgId"].ToString().Equals("6"))
            {
            this.ShowReport("Admission_Slip_Report", "rptStudAdmSlip_New.rpt", "0");
            }
        else
            {
            this.ShowReportnew("Admission_Slip_Report", "AllotmentOfferLetter.rpt", "0");
            }
        }///Added by Irfan Shaikh on 2019/04/08 ////End

    private string GetNewReceiptNo()
        {
        string receiptNo = string.Empty;

        try
            {
            string demandno = objCommon.LookUp("ACD_DEMAND", "MAX(DM_NO)", "");
            DataSet ds = feeController.GetNewReceiptData("B", Int32.Parse(Session["userno"].ToString()), "TF");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                DataRow dr = ds.Tables[0].Rows[0];
                //dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString()) + 1;
                dr["FIELD"] = Int32.Parse(dr["FIELD"].ToString());
                receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + DateTime.Today.Year.ToString().Substring(2, 2) + "/" + dr["FIELD"].ToString() + demandno;

                // save counter no in hidden field to be used while saving the record
                ViewState["CounterNo"] = dr["COUNTERNO"].ToString();
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return receiptNo;
        }

    private FeeDemand GetDcrCriteria()
        {
        FeeDemand dcrCriteria = new FeeDemand();
        Student objS = new Student();
        try
            {
            dcrCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            dcrCriteria.ReceiptTypeCode = "TF";
            dcrCriteria.BranchNo = Convert.ToInt16(ddlBranch.SelectedValue);
            dcrCriteria.SemesterNo = 1;
            dcrCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            dcrCriteria.UserNo = int.Parse(Session["userno"].ToString());
            //dcrCriteria.UserNo = 7;
            if (txtDDAmountPaid.Text == "")
                {
                dcrCriteria.ExcessAmount = 0;
                }
            else
                {
                dcrCriteria.ExcessAmount = Convert.ToDouble(txtDDAmountPaid.Text.Trim());
                }

            if (Convert.ToInt32(ddlDegree.SelectedValue) == 4)
                {
                if (Convert.ToInt32(ddlRound.SelectedValue) == 1)
                    {
                    dcrCriteria.ExcessAmount = 10000;
                    }
                else
                    {
                    dcrCriteria.ExcessAmount = 0;
                    }
                }
            dcrCriteria.CollegeCode = Session["colcode"].ToString();
            }
        catch (Exception ex)
            {
            throw;
            }
        return dcrCriteria;
        }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
        {
        try
            {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
        {

        string collegeCode = "33";

        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + collegeCode + "";
        return param;
        }

    private FeeDemand GetDemandCriteria()
        {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();
        try
            {
            int ExamType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            if (ExamType == 1)
                {
                demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
                }
            else
                {
                demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
                }
            demandCriteria.ReceiptTypeCode = "TF";
            demandCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            demandCriteria.SemesterNo = 1;
            demandCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            }
        catch (Exception ex)
            {
            throw;
            }
        return demandCriteria;
        }

    private FeeDemand GetDemandCriteriaForHostel()
        {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();
        try
            {
            int ExamType = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "FLOCK=1"));
            if (ExamType == 1)
                {
                demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
                }
            else
                {
                demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]) + 1;
                }
            demandCriteria.ReceiptTypeCode = "HF";
            demandCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            demandCriteria.SemesterNo = 1;
            demandCriteria.PaymentTypeNo = int.Parse(ddlPaymentType.SelectedValue);
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            }
        catch (Exception ex)
            {
            throw;
            }
        return demandCriteria;
        }

    protected void ddlExamNo_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlExamNo.SelectedValue == "9")
            {
            trSpotOption.Visible = true;
            trCCMT.Visible = false;
            trGetScore.Visible = false;
            }
        else if (ddlExamNo.SelectedValue == "7")
            {
            trSpotOption.Visible = false;
            trCCMT.Visible = true;
            trGetScore.Visible = true;
            }
        else
            {
            trSpotOption.Visible = false;
            trCCMT.Visible = false;
            trGetScore.Visible = false;
            }
        }

    private void ShowReportchallan(string rptName, int dcrNo, int studentNo, string copyNo)
        {
        try
            {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnChallan_Click(object sender, EventArgs e)
        {
        string studentIDs = txtREGNo.Text.Trim();
        string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(txtREGNo.Text.Trim()) + " AND SEMESTERNO=1");

        if (dcrNo != string.Empty)
            {
            this.ShowReportchallan("FeeCollectionReceiptForCourseRegister1.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
            }
        }

    public byte[] ResizePhoto(FileUpload fu)
        {

        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
            {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);
            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
                {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
                }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
            }
        return image;
        }

    public byte[] ResizePhotoSign(FileUpload fu)
        {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
            {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);
            //string strExtension = System.IO.Path.GetExtension(hdfSignUpload.Value);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
                {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
                }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
            }
        return image;
        }

    private void ClearControls_DemandDraftDetails()
        {
        txtDDNo.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDCity.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtPayType.Text = string.Empty;
        txtCashDate.Text = string.Empty;
        btnpayment.Visible = false;
        }

    protected void ddlSpotOption_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlSpotOption.SelectedValue == "1")
            {
            trGetScore.Visible = true;
            }
        else
            {
            trGetScore.Visible = false;
            }

        }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlYear.SelectedValue, "SEMESTERNO");
        }



    protected void ddlExamNo_SelectedIndexChanged1(object sender, EventArgs e)
        {
        if (ddlExamNo.SelectedItem.Text == "OTHERS")
            {
            divotherentrance.Visible = true;
            }
        else
            {
            divotherentrance.Visible = false;
            }
        }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlstate.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");

            }
        else
            {
            ddlCity.SelectedIndex = 0;
            }
        }

    protected void rdoHosteler_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            if (rdoHosteler.SelectedValue == "1")
                {
                rdbTransport.Visible = true;
                rdbTransport.Enabled = false;
                rdoHosteler.Visible = true;
                lbtransport.Visible = true;

                if (CheckControlToHide("divHostel") == true)
                    {
                    //added for JECRC ONLY
                    objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL_FESS_TYPE", "HID", "HOSTELTYPE", "HID>0 ", "HID");
                    divHostel.Visible = true;
                    }

                //////********** Start Change by Rahul Moraskar 2022-07-26
                //if (Session["OrgId"].ToString().Equals("5"))
                //{
                //    //added for JECRC ONLY
                //    objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL_FESS_TYPE", "HID", "HOSTELTYPE", "HID>0 ", "HID");
                //    divHostel.Visible = true;
                //}
                //////********** END Change by Rahul Moraskar 2022-07-26

                }
            if (rdoHosteler.SelectedValue == "0")
                {
                lbtransport.Visible = true;
                rdbTransport.Visible = true;
                rdbTransport.Enabled = true;
                rdoHosteler.Visible = true;
                rdoHosteler.Enabled = false;
                lbhosteller.Visible = true;

                if (CheckControlToHide("divHostel") == true)
                    {
                    //added for JECRC ONLY
                    divHostel.Visible = false;
                    rdoHosteler.Enabled = true;
                    }
                ////********** Start Change by Rahul Moraskar 2022-07-26
                //if (Session["OrgId"].ToString().Equals("5"))
                //{
                //    //added for JECRC ONLY
                //    divHostel.Visible = false;
                //    rdoHosteler.Enabled = true;
                //}
                ////********** END Change by Rahul Moraskar 2022-07-26

                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void rdbTransport_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {

            if (rdbTransport.SelectedValue == "1")
                {
                rdoHosteler.Visible = true;
                rdoHosteler.Enabled = false;
                rdbTransport.Visible = true;

                }
            if (rdbTransport.SelectedValue == "0")
                {
                lbhosteller.Visible = true;
                rdoHosteler.Visible = true;
                rdoHosteler.Enabled = true;
                rdbTransport.Visible = true;
                lbtransport.Visible = true;
                //rdbTransport.Enabled = false; //Change by Rahul Moraskar
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void txtStudMobile_TextChanged(object sender, EventArgs e)
        {

        int l = (txtStudMobile.Text).Length;

        if (l < 10 && l != 0)
            {
            objCommon.DisplayUserMessage(updStudent, "Please enter 10 digit mobile Number", this.Page);
            }
        txtStudEmail.Focus();
        }

    protected void txtAadhaarNo_TextChanged(object sender, EventArgs e)
        {
        int l = (txtAadhaarNo.Text).Length;

        if (l < 12 && l != 0)
            {
            objCommon.DisplayUserMessage(updStudent, "Please enter 12 digit Aadhar No.", this.Page);
            }
        }

    protected void lnkId_Click(object sender, EventArgs e)
        {
        updEdit.Visible = false;
        //divGeneralInfo.Visible = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "Close();", true);
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        ViewState["stuinfoidno"] = lnk.CommandArgument.ToString();
        txtREGNo.Text = ViewState["stuinfoidno"].ToString();


        ShowStudentDetails();
        if (Convert.ToInt32(Session["OrgId"]) != 1)
            {
            ddlSchool.Enabled = true;
            ddlDegree.Enabled = true;
            }

        Session["STUD_DETAILS"] = null;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "Close();", true);
        }

    private void bindlist(string category, string searchtext)
        {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStuddetpros(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
            {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
        else
            lblNoRecords.Text = "Total Records : 0";


        }

    private void bindlistsearchprospectusno(string category, string searchtext)
        {

        DataSet ds = objSC.RetrieveStuddetprospectusno(searchtext, category);

        if (category == "IDNO")
            {
            if (ds.Tables[0].Rows.Count > 0)
                {
                LVtempid.DataSource = ds;
                LVtempid.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
                }
            else
                {
                lblNoRecords.Text = "Total Records : 0";
                }
            }

        else
            {
            if (ds.Tables[0].Rows.Count > 0)
                {
                lvstudentprospectusno.DataSource = ds;
                lvstudentprospectusno.DataBind();
                lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
                LVtempid.Visible = false;
                }
            else
                lblNoRecords.Text = "Total Records : 0";
            }
        }

    //private void bindlistsearchTempIDNo(string category, string searchtext)
    //    {

    //    DataSet ds = objSC.RetrieveStuddetTempIDNO(searchtext, category);

    //    if (ds.Tables[0].Rows.Count > 0)
    //        {
    //        LVtempid.DataSource = ds;
    //        LVtempid.DataBind();
    //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
    //        }
    //    else
    //        lblNoRecords.Text = "Total Records : 0";
    //    }



    private void getSessionDetails(DataSet dsSessionDeatails)
        {
        if (dsSessionDeatails.Tables[0].Rows.Count > 0)
            {
            txtIDNo.Text = dsSessionDeatails.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
            txtIDNo.ToolTip = dsSessionDeatails.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
            txtStudentfullName.Text = dsSessionDeatails.Tables[0].Rows[0]["STUDENT_NAME"] == null ? string.Empty : dsSessionDeatails.Tables[0].Rows[0]["STUDENT_NAME"].ToString();
            txtStudentfullName.Text = txtStudentfullName.Text.ToUpper();
            txtStudEmail.Text = dsSessionDeatails.Tables[0].Rows[0]["EMAIL"] == null ? string.Empty : dsSessionDeatails.Tables[0].Rows[0]["EMAIL"].ToString();
            txtStudMobile.Text = dsSessionDeatails.Tables[0].Rows[0]["MOBILE"] == null ? string.Empty : dsSessionDeatails.Tables[0].Rows[0]["MOBILE"].ToString();
            ddlSchool.SelectedValue = dsSessionDeatails.Tables[0].Rows[0]["COLLEGE_ID"] == null ? "0" : dsSessionDeatails.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENO");
            ddlDegree.SelectedValue = dsSessionDeatails.Tables[0].Rows[0]["DEGREENO"] == null ? "0" : dsSessionDeatails.Tables[0].Rows[0]["DEGREENO"].ToString();
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
            ddlBranch.SelectedValue = dsSessionDeatails.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : dsSessionDeatails.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ddlBatch.SelectedValue = dsSessionDeatails.Tables[0].Rows[0]["ADMISSION_BATCH"] == null ? "0" : dsSessionDeatails.Tables[0].Rows[0]["ADMISSION_BATCH"].ToString();

            }
        //else 
        //{
        //    objCommon.DisplayMessage(this.Page, "Entered Application ID is not verified", this.Page);
        //}
        Session["STUD_DETAILS"] = null;
        }

    private void ShowStudentDetails()
        {

        Session["STUD_DETAILS"] = null;

        StudentController objSC = new StudentController();
        DataSet ds = new DataSet();

        if (Session["usertype"].ToString() == "2")
            {
            ds = objSC.GetStuddetpros_JECRC(Convert.ToInt32(ViewState["stuinfoidno"]));
            txtStudentName.ReadOnly = false;
            txtStudentName.Visible = true;
            }
        else
            {
            //ds = objSC.GetStuddetpros(Convert.ToInt32(ViewState["stuinfoidno"]));

            btnpayment.Visible = false;
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A,ACD_STU_ADDRESS B ,ACD_DEMAND C", "A.IDNO", "A.STUDNAME,A.REGNO,A.IDNO,A.ROLLNO,A.STUDFIRSTNAME,A.STUDMIDDLENAME,A.STUDLASTNAME,A.FATHERFIRSTNAME,A.FATHERMIDDLENAME,A.FATHERLASTNAME,A.MOTHERNAME,A.DOB,A.SEX,A.RELIGIONNO,A.MARRIED,A.NATIONALITYNO,A.CATEGORYNO,A.CASTE,A.ADMDATE,A.DEGREENO,A.BRANCHNO,A.YEAR,A.STUDENTMOBILE,A.STUDENTMOBILE2,A.SEMESTERNO,A.PTYPE,A.STATENO,A.ADMBATCH,A.IDTYPE,A.YEAR_OF_EXAM,A.ALL_INDIA_RANK,A.STATE_RANK,A.PERCENTAGE,A.PERCENTILE,A.QEXMROLLNO,A.ADMCATEGORYNO,A.QUALIFYNO,A.SCHOLORSHIPTYPENO,A.PHYSICALLY_HANDICAPPED,A.ADMROUNDNO,A.COLLEGE_CODE,A.MERITNO,A.APPLICATIONID,A.SCORE,B.STADDNO, B.IDNO, B.PADDRESS, ISNULL(A.SCHOLORSHIP,0),A.COLLEGE_ID,A.FATHERMOBILE,SCHOLORSHIP,A.ADDHARCARDNO,A.TRANSPORT,A.HOSTELER,B.PCITY,ISNULL(A.INSTALLMENT,0)INSTALLMENT,B.PSTATE,B.PPINCODE,A.EMAILID,B.PTELEPHONE,B.LADDRESS,B.LTELEPHONE,B.LMOBILE,B.LEMAIL,A.ADMQUOTANO,A.BLOODGRPNO,B.LCITY,B.LSTATE,C.TOTAL_AMT", "ISNULL(ADMCAN,0)=0 AND (A.IDNO=B.IDNO AND A.IDNO= C.IDNO ) AND  A.APPLICATIONID = '" + ViewState["stuinfoidno"].ToString() + "'", string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    PopulateDropDownList();
                    //txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    string srnno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()));
                    Session["Enrollno"] = srnno;
                    ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    txtStudentfullName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    txtStudentName.Text = dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                    txtStudentMiddleName.Text = dsStudent.Tables[0].Rows[0]["STUDMIDDLENAME"].ToString();
                    txtStudentLastName.Text = dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                    txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERFIRSTNAME"].ToString();
                    txtFatherMiddleName.Text = dsStudent.Tables[0].Rows[0]["FATHERMIDDLENAME"].ToString();
                    txtFatherLastName.Text = dsStudent.Tables[0].Rows[0]["FATHERLASTNAME"].ToString();
                    txtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                        {
                        rdoMale.Checked = true;
                        rdoFemale.Checked = false;
                        }
                    else
                        {
                        rdoFemale.Checked = true;
                        rdoMale.Checked = false;
                        }

                    //txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));//Commented by Irfan Shaikh on 20190405
                    txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                    //ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString()), "ACD_RELIGION", "RELIGIONNO", "RELIGION"));
                    ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString());
                    if (dsStudent.Tables[0].Rows[0]["MARRIED"].ToString().Trim().Equals("Y"))
                        {
                        rdoMarriedYes.Checked = true;
                        rdoMarriedNo.Checked = false;
                        }
                    else
                        {
                        rdoMarriedYes.Checked = false;
                        rdoMarriedNo.Checked = true;
                        }
                    ddlSchool.SelectedValue = (dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
                    ddlNationality.SelectedValue = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString());
                    ddlCategory.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                    txtPermanentAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                    txtCity.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                    txtState.Text = (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                    txtPIN.Text = dsStudent.Tables[0].Rows[0]["PPINCODE"].ToString();
                    txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                    txtDateOfReporting.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));




                    ddlDegree.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()), "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME"));

                    ddlBranch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), "ACD_BRANCH", "BRANCHNO", "LONGNAME"));

                    objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1", "BATCHNO DESC");
                    //ddlBatch.SelectedIndex = 1;
                    ddlBatch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                    ddlYear.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["YEAR"].ToString()), "ACD_YEAR", "YEAR", "YEARNAME"));
                    ddlPaymentType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()), "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME"));
                    txtStateOfEligibility.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                    ddlAdmType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()), "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION"));
                    txtAllIndiaRank.Text = dsStudent.Tables[0].Rows[0]["ALL_INDIA_RANK"].ToString();
                    txtYearOfExam.Text = dsStudent.Tables[0].Rows[0]["YEAR_OF_EXAM"].ToString();
                    txtStateRank.Text = dsStudent.Tables[0].Rows[0]["STATE_RANK"].ToString();
                    txtPer.Text = dsStudent.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                    txtQExamRollNo.Text = dsStudent.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                    txtPercentile.Text = dsStudent.Tables[0].Rows[0]["PERCENTILE"].ToString();
                    ddlAllotedCat.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                    //ddlExamNo.SelectedValue = (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString());
                    txtStudMobile.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    txtStudMobile2.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE2"].ToString();
                    txtParentmobno.Text = dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString();
                    txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                    txtPostalAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                    txtGuardianPhone.Text = dsStudent.Tables[0].Rows[0]["LTELEPHONE"].ToString();
                    txtGuardianMobile.Text = dsStudent.Tables[0].Rows[0]["LMOBILE"].ToString();
                    txtGuardianEmail.Text = dsStudent.Tables[0].Rows[0]["LEMAIL"].ToString();
                    ddlBloodGrp.SelectedValue = (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString());
                    ddlQuota.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString()), "ACD_QUOTA", "QUOTANO", "QUOTA"));
                    txtLocalCity.Text = (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                    txtLocalState.Text = (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                    //PHYSICAL HADICAPP
                    ddlPhyHandicap.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString());
                    //ROUND FOR MCA
                    ddladmthrough.SelectedValue = (dsStudent.Tables[0].Rows[0]["ADMROUNDNO"].ToString());
                    ddlSemester.SelectedValue = (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                    //show the student photo and sign code              
                    txtmerirtno.Text = dsStudent.Tables[0].Rows[0]["MERITNO"].ToString();
                    txtscore.Text = dsStudent.Tables[0].Rows[0]["SCORE"].ToString();
                    txtapplicationid.Text = dsStudent.Tables[0].Rows[0]["APPLICATIONID"].ToString();
                    txtAadhaarNo.Text = dsStudent.Tables[0].Rows[0]["ADDHARCARDNO"].ToString();
                    txtAppliedFees.Text = dsStudent.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                    rdoInstallment.SelectedValue = dsStudent.Tables[0].Rows[0]["INSTALLMENT"].ToString();



                    if (Convert.ToInt32(Session["OrgId"]) == 1)
                        {
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0", "D.DEGREENO");
                        ddlDegree.SelectedValue = "1";
                        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                        }


                    if (rdoInstallment.SelectedValue.Equals("0"))
                        {
                        divinstaltype.Visible = false;
                        DivDuedate1.Visible = false;
                        DivDuedate2.Visible = false;
                        DivDuedate3.Visible = false;
                        DivDuedate4.Visible = false;
                        DivdueDate5.Visible = false;
                        }
                    else
                        {
                        divinstaltype.Visible = true;

                        objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
                        ddlinstallmenttype.SelectedValue = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DISTINCT ISNULL(INSTALLMENT_TYPE,0) as INSTALLMENT_TYPE", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                        string Installmenttype = string.Empty;
                        Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
                        Installmenttype = Installmenttype + '-';
                        int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));
                        if (count == 2)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = false;
                            DivDuedate4.Visible = false;
                            DivdueDate5.Visible = false;
                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            }

                        if (count == 3)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = true;
                            DivDuedate4.Visible = false;
                            DivdueDate5.Visible = false;
                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                            }

                        if (count == 4)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = true;
                            DivDuedate4.Visible = true;
                            DivdueDate5.Visible = false;
                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date4 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=4 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate4.Text = (Due_date4.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date4.ToString()).ToString("dd/MM/yyyy"));
                            }

                        if (count == 5)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = true;
                            DivDuedate4.Visible = true;
                            DivdueDate5.Visible = true;

                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date4 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=4 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date5 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=5 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate4.Text = (Due_date4.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date4.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate5.Text = (Due_date5.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date5.ToString()).ToString("dd/MM/yyyy"));
                            }

                        }


                    ddlstate.SelectedValue = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString());

                    //if()



                    if (Convert.ToBoolean(dsStudent.Tables[0].Rows[0]["HOSTELER"]) == true)
                        {
                        rdoHosteler.SelectedValue = "1";
                        }
                    else
                        {
                        rdoHosteler.SelectedValue = "0";
                        }

                    rdbTransport.SelectedValue = dsStudent.Tables[0].Rows[0]["TRANSPORT"].ToString();
                    rdoscholarship.SelectedValue = dsStudent.Tables[0].Rows[0]["SCHOLORSHIP"].ToString();
                    //Condition for MCA only
                    int degreeno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());
                    if (degreeno == 4)
                        {
                        trMca.Visible = true;
                        }
                    else
                        {
                        trMca.Visible = false;
                        }

                    //Condition for the M.Tech(Appl. Geo) only

                    if (degreeno == 2)
                        {
                        tblExam.Visible = false;
                        trExam.Visible = false;
                        }
                    else
                        {
                        tblExam.Visible = true;
                        trExam.Visible = true;
                        }

                    DisableFields();
                    }
                else
                    {
                    DataSet dstempStudent = objCommon.FillDropDown("TEMP_STUDENT", "[Merit No],Score,ApplicationID", "Name,Degreeno,Admbatch,GENDER,CATEGORY", "ApplicationID = '" + ViewState["stuinfoidno"].ToString() + "'", string.Empty);
                    if (dstempStudent != null && dstempStudent.Tables.Count > 0)
                        {
                        if (dstempStudent.Tables[0].Rows.Count > 0)
                            {
                            //DisableControlsRecursive(Page);
                            clearcontrols();
                            EnableFields();
                            txtStudentfullName.Text = dstempStudent.Tables[0].Rows[0]["Name"].ToString();
                            txtStudentfullName.Enabled = false;
                            txtmerirtno.Text = dstempStudent.Tables[0].Rows[0]["Merit No"].ToString();
                            txtscore.Text = dstempStudent.Tables[0].Rows[0]["Score"].ToString();
                            txtapplicationid.Text = dstempStudent.Tables[0].Rows[0]["ApplicationID"].ToString();
                            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0", "D.DEGREENO");
                            ddlDegree.SelectedItem.Text = (dstempStudent.Tables[0].Rows[0]["Degreeno"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dstempStudent.Tables[0].Rows[0]["Degreeno"].ToString()), "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME"));

                            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1", "BATCHNO DESC");
                            //Session["Admbatch"] = dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString();
                            ddlBatch.SelectedItem.Text = (dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1", "BATCHNO DESC");
                            ddlBatch.SelectedValue = dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString();
                            ddlBatch.SelectedIndex = 1;
                            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");

                            if (dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "M" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "Male" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "MALE" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "male")
                                {
                                rdoMale.Checked = true;
                                }
                            else if (dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "F" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "Female" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "FEMALE" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "female")
                                {
                                rdoFemale.Checked = true;
                                }
                            else if (dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "O" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "Other" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "OTHER" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "other")
                                {
                                rdoTransGender.Checked = true;
                                }

                            if (Convert.ToInt32(Session["OrgId"]) == 1)
                                {
                                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0", "D.DEGREENO");
                                ddlDegree.SelectedValue = "1";
                                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                                }


                            }
                        else
                            {
                            objCommon.DisplayMessage(this, "This Application ID is not found in the System", this.Page);
                            Response.Redirect(Request.Url.ToString());
                            }
                        }
                    else
                        {
                        objCommon.DisplayMessage(this, "This Application ID is not found in the System", this.Page);
                        Response.Redirect(Request.Url.ToString());
                        }
                    }
                }
            else
                {
                objCommon.DisplayMessage(this, "This Application ID is not found in the System", this.Page);
                Response.Redirect(Request.Url.ToString());
                }

            }
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    txtIDNo.Text = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
        //    txtIDNo.ToolTip = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
        //    txtStudentfullName.Text = ds.Tables[0].Rows[0]["STUDENT_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENT_NAME"].ToString();
        //    txtStudEmail.Text = ds.Tables[0].Rows[0]["EMAIL"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAIL"].ToString();
        //    txtStudMobile.Text = ds.Tables[0].Rows[0]["MOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["MOBILE"].ToString();
        //    ddlSchool.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? "0" : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue , "D.DEGREENO");
        //    ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == null ? "0" : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
        //    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");            
        //    ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString(); 
        //    ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMISSION_BATCH"] == null ? "0" : ds.Tables[0].Rows[0]["ADMISSION_BATCH"].ToString();           
        //}
        Session["STUD_DETAILS"] = ds;
        }

    public void clearcontrols()
        {

        Session["STUD_DETAILS"] = null;
        txtIDNo.Text = string.Empty;
        txtStudentfullName.Text = string.Empty;
        txtStudMobile.Text = string.Empty;
        txtStudEmail.Text = string.Empty;
        txtState.Text = string.Empty;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        // ddlAdmType.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        txtDateOfBirth.Text = string.Empty;
        ddlPaymentType.SelectedIndex = 0;
        txtAadhaarNo.Text = string.Empty;
        ddlAllotedCat.SelectedIndex = 0;
        //ddlstate.SelectedIndex = 0;
        //ddlstate.SelectedValue = "5";
        ddlBatch.SelectedIndex = 0;
        rdoHosteler.SelectedValue = "0";
        rdoInstallment.SelectedValue = "0";
        rdbTransport.SelectedValue = "0";
        rdoMale.Checked = true;
        ddlBloodGrp.SelectedIndex = 0;
        txtDateOfReporting.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtParentmobno.Text = string.Empty;
        }

    protected void ddlAdmType_TextChanged(object sender, EventArgs e)
        {
        try
            {
            if (ddlAdmType.SelectedValue.Equals("1"))
                {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                ddlYear.SelectedValue = "1";
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP (1) SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlAdmType.SelectedValue, "SEMESTERNO");
                ddlSemester.SelectedValue = "1";
                }
            else if (ddlAdmType.SelectedValue.Equals("2"))
                {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                ddlYear.SelectedValue = "2";
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP (1) SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 and yearno=" + ddlAdmType.SelectedValue, "SEMESTERNO");
                ddlSemester.SelectedIndex = 1;
                }
            else
                {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "TOP (8) SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0", "SEMESTERNO");

                }

            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnSearch_Click1(object sender, EventArgs e)
        {

        this.ClearControls_DemandDraftDetails();
        //btnpayment.Visible = true;

        objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "DEGREENO > 0", "QUALIFYNO");
        if (txtREGNo.Text.Trim() == string.Empty)
            {
            objCommon.DisplayMessage("Enter Application ID to Modify!", this.Page);
            return;
            }

        PopulateDropDownList();
        if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 2 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A,ACD_STU_ADDRESS B", "A.IDNO", "A.STUDNAME,A.REGNO,A.IDNO,A.ROLLNO,A.STUDFIRSTNAME,A.STUDMIDDLENAME,A.STUDLASTNAME,A.FATHERFIRSTNAME,A.FATHERMIDDLENAME,A.FATHERLASTNAME,A.MOTHERNAME,A.DOB,A.SEX,ISNULL(A.RELIGIONNO,0)RELIGIONNO,A.MARRIED,A.NATIONALITYNO,ISNULL(A.CATEGORYNO,0)CATEGORYNO,A.CASTE,A.ADMDATE,A.DEGREENO,A.BRANCHNO,A.YEAR,A.STUDENTMOBILE,A.STUDENTMOBILE2,A.SEMESTERNO,A.PTYPE,A.STATENO,A.ADMBATCH,A.IDTYPE,A.YEAR_OF_EXAM,A.ALL_INDIA_RANK,A.STATE_RANK,A.PERCENTAGE,A.PERCENTILE,A.QEXMROLLNO,A.ADMCATEGORYNO,A.QUALIFYNO,A.SCHOLORSHIPTYPENO,A.PHYSICALLY_HANDICAPPED,A.ADMROUNDNO,A.COLLEGE_CODE,A.MERITNO,A.APPLICATIONID,A.SCORE,B.STADDNO, B.IDNO, B.PADDRESS, ISNULL(A.SCHOLORSHIP,0),A.COLLEGE_ID,A.FATHERMOBILE,SCHOLORSHIP,A.ADDHARCARDNO,ISNULL(A.TRANSPORT,0)TRANSPORT,A.HOSTELER,B.PCITY,ISNULL(A.INSTALLMENT,0)INSTALLMENT,B.PSTATE,B.PPINCODE,A.EMAILID,B.PTELEPHONE,B.LADDRESS,B.LTELEPHONE,B.LMOBILE,B.LEMAIL,A.ADMQUOTANO,A.BLOODGRPNO,B.LCITY,B.LSTATE", "ISNULL(A.ADMCAN,0)=0 AND A.IDNO=B.IDNO AND A.APPLICATIONID = '" + txtREGNo.Text.Trim() + "'", string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                    //PopulateDropDownList();
                    //txtRegNo.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                    string srnno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()));
                    Session["Enrollno"] = srnno;
                    Session["output"] = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    ViewState["REGNO"] = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    txtStudentfullName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    txtStudentName.Text = dsStudent.Tables[0].Rows[0]["STUDFIRSTNAME"].ToString();
                    txtStudentMiddleName.Text = dsStudent.Tables[0].Rows[0]["STUDMIDDLENAME"].ToString();
                    txtStudentLastName.Text = dsStudent.Tables[0].Rows[0]["STUDLASTNAME"].ToString();
                    txtFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERFIRSTNAME"].ToString();
                    txtFatherMiddleName.Text = dsStudent.Tables[0].Rows[0]["FATHERMIDDLENAME"].ToString();
                    txtFatherLastName.Text = dsStudent.Tables[0].Rows[0]["FATHERLASTNAME"].ToString();
                    txtMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    if (dsStudent.Tables[0].Rows[0]["SEX"].ToString().Trim().Equals("M"))
                        {
                        rdoMale.Checked = true;
                        rdoFemale.Checked = false;
                        }
                    else
                        {
                        rdoFemale.Checked = true;
                        rdoMale.Checked = false;
                        }

                    //txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));//Commented by Irfan Shaikh on 20190405
                    txtDateOfBirth.Text = (dsStudent.Tables[0].Rows[0]["DOB"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy"));

                    //ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString()), "ACD_RELIGION", "RELIGIONNO", "RELIGION"));
                    ddlReligion.SelectedValue = (dsStudent.Tables[0].Rows[0]["RELIGIONNO"].ToString());
                    if (dsStudent.Tables[0].Rows[0]["MARRIED"].ToString().Trim().Equals("Y"))
                        {
                        rdoMarriedYes.Checked = true;
                        rdoMarriedNo.Checked = false;
                        }
                    else
                        {
                        rdoMarriedYes.Checked = false;
                        rdoMarriedNo.Checked = true;
                        }
                    ddlSchool.SelectedValue = (dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
                    ddlNationality.SelectedValue = (dsStudent.Tables[0].Rows[0]["NATIONALITYNO"].ToString());
                    ddlCategory.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                    txtPermanentAddress.Text = dsStudent.Tables[0].Rows[0]["PADDRESS"].ToString();
                    txtCity.Text = (dsStudent.Tables[0].Rows[0]["PCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                    txtState.Text = (dsStudent.Tables[0].Rows[0]["PSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                    txtPIN.Text = dsStudent.Tables[0].Rows[0]["PPINCODE"].ToString();
                    txtContactNumber.Text = dsStudent.Tables[0].Rows[0]["PTELEPHONE"].ToString();
                    txtDateOfReporting.Text = (dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));
                    ddlDegree.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString()), "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME"));
                    ddlBranch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString()), "ACD_BRANCH", "BRANCHNO", "LONGNAME"));
                    ddlBatch.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                    ddlYear.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["YEAR"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["YEAR"].ToString()), "ACD_YEAR", "YEAR", "YEARNAME"));
                    ddlPaymentType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["PTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"].ToString()), "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME"));
                    txtStateOfEligibility.Text = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["STATENO"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                    ddlAdmType.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDTYPE"].ToString()), "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION"));
                    txtAllIndiaRank.Text = dsStudent.Tables[0].Rows[0]["ALL_INDIA_RANK"].ToString();
                    txtYearOfExam.Text = dsStudent.Tables[0].Rows[0]["YEAR_OF_EXAM"].ToString();
                    txtStateRank.Text = dsStudent.Tables[0].Rows[0]["STATE_RANK"].ToString();
                    txtPer.Text = dsStudent.Tables[0].Rows[0]["PERCENTAGE"].ToString();
                    txtQExamRollNo.Text = dsStudent.Tables[0].Rows[0]["QEXMROLLNO"].ToString();
                    txtPercentile.Text = dsStudent.Tables[0].Rows[0]["PERCENTILE"].ToString();
                    ddlAllotedCat.SelectedValue = (dsStudent.Tables[0].Rows[0]["CATEGORYNO"].ToString());
                    //ddlExamNo.SelectedValue = (dsStudent.Tables[0].Rows[0]["QUALIFYNO"].ToString());
                    txtStudMobile.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    txtStudMobile2.Text = dsStudent.Tables[0].Rows[0]["STUDENTMOBILE2"].ToString();
                    txtParentmobno.Text = dsStudent.Tables[0].Rows[0]["FATHERMOBILE"].ToString();
                    txtStudEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                    txtPostalAddress.Text = dsStudent.Tables[0].Rows[0]["LADDRESS"].ToString();
                    txtGuardianPhone.Text = dsStudent.Tables[0].Rows[0]["LTELEPHONE"].ToString();
                    txtGuardianMobile.Text = dsStudent.Tables[0].Rows[0]["LMOBILE"].ToString();
                    txtGuardianEmail.Text = dsStudent.Tables[0].Rows[0]["LEMAIL"].ToString();
                    ddlBloodGrp.SelectedValue = (dsStudent.Tables[0].Rows[0]["BLOODGRPNO"].ToString());
                    ddlQuota.SelectedItem.Text = (dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMQUOTANO"].ToString()), "ACD_QUOTA", "QUOTANO", "QUOTA"));
                    txtLocalCity.Text = (dsStudent.Tables[0].Rows[0]["LCITY"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LCITY"].ToString()), "ACD_CITY", "CITYNO", "CITY"));
                    txtLocalState.Text = (dsStudent.Tables[0].Rows[0]["LSTATE"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LSTATE"].ToString()), "ACD_STATE", "STATENO", "STATENAME"));
                    //PHYSICAL HADICAPP
                    ddlPhyHandicap.SelectedValue = (dsStudent.Tables[0].Rows[0]["PHYSICALLY_HANDICAPPED"].ToString());
                    //ROUND FOR MCA
                    ddladmthrough.SelectedValue = (dsStudent.Tables[0].Rows[0]["ADMROUNDNO"].ToString());
                    ddlSemester.SelectedValue = (dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                    //show the student photo and sign code              
                    txtmerirtno.Text = dsStudent.Tables[0].Rows[0]["MERITNO"].ToString();
                    txtscore.Text = dsStudent.Tables[0].Rows[0]["SCORE"].ToString();
                    txtapplicationid.Text = dsStudent.Tables[0].Rows[0]["APPLICATIONID"].ToString();
                    txtAadhaarNo.Text = dsStudent.Tables[0].Rows[0]["ADDHARCARDNO"].ToString();
                    rdoInstallment.SelectedValue = dsStudent.Tables[0].Rows[0]["INSTALLMENT"].ToString();
                    if (rdoInstallment.SelectedValue.Equals("0"))
                        {
                        divinstaltype.Visible = false;
                        DivDuedate1.Visible = false;
                        DivDuedate2.Visible = false;
                        DivDuedate3.Visible = false;
                        DivDuedate4.Visible = false;
                        DivdueDate5.Visible = false;
                        }
                    else
                        {
                        divinstaltype.Visible = true;

                        objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
                        ddlinstallmenttype.SelectedValue = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DISTINCT INSTALLMENT_TYPE", "IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                        string Installmenttype = string.Empty;
                        Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
                        Installmenttype = Installmenttype + '-';
                        int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));
                        if (count == 2)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = false;
                            DivDuedate4.Visible = false;
                            DivdueDate5.Visible = false;
                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            }

                        if (count == 3)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = true;
                            DivDuedate4.Visible = false;
                            DivdueDate5.Visible = false;
                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                            }

                        if (count == 4)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = true;
                            DivDuedate4.Visible = true;
                            DivdueDate5.Visible = false;
                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date4 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=4 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate4.Text = (Due_date4.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date4.ToString()).ToString("dd/MM/yyyy"));
                            }

                        if (count == 5)
                            {
                            DivDuedate1.Visible = true;
                            DivDuedate2.Visible = true;
                            DivDuedate3.Visible = true;
                            DivDuedate4.Visible = true;
                            DivdueDate5.Visible = true;

                            string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date4 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=4 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                            string Due_date5 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=5 AND IDNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                            txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate4.Text = (Due_date4.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date4.ToString()).ToString("dd/MM/yyyy"));
                            txtduedate5.Text = (Due_date5.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date5.ToString()).ToString("dd/MM/yyyy"));
                            }
                        }

                    objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
                    ddlstate.SelectedValue = (dsStudent.Tables[0].Rows[0]["STATENO"].ToString());
                    objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");
                    ddlCity.SelectedValue = dsStudent.Tables[0].Rows[0]["PCITY"].ToString();
                    if (Convert.ToBoolean(dsStudent.Tables[0].Rows[0]["HOSTELER"]) == true)
                        {
                        rdoHosteler.SelectedValue = "1";
                        }
                    else
                        {
                        rdoHosteler.SelectedValue = "0";
                        }

                    rdbTransport.SelectedValue = dsStudent.Tables[0].Rows[0]["TRANSPORT"].ToString();
                    rdoscholarship.SelectedValue = dsStudent.Tables[0].Rows[0]["SCHOLORSHIP"].ToString();
                    //Condition for MCA only
                    int degreeno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());
                    if (degreeno == 4)
                        {
                        trMca.Visible = true;
                        }
                    else
                        {
                        trMca.Visible = false;
                        }

                    //Condition for the M.Tech(Appl. Geo) only

                    if (degreeno == 2)
                        {
                        tblExam.Visible = false;
                        trExam.Visible = false;
                        }
                    else
                        {
                        tblExam.Visible = true;
                        trExam.Visible = true;
                        }

                    DisableFields();
                    }
                else
                    {
                    DataSet dstempStudent = objCommon.FillDropDown("TEMP_STUDENT", "[Merit No],Score,ApplicationID", "Name,Degreeno,Admbatch,GENDER,CATEGORY", "ApplicationID = '" + txtREGNo.Text.Trim() + "'", string.Empty);
                    if (dstempStudent != null && dstempStudent.Tables.Count > 0)
                        {
                        if (dstempStudent.Tables[0].Rows.Count > 0)
                            {
                            //DisableControlsRecursive(Page);
                            clearcontrols();
                            EnableFields();
                            txtStudentfullName.Text = dstempStudent.Tables[0].Rows[0]["Name"].ToString();
                            txtStudentfullName.Enabled = false;
                            txtmerirtno.Text = dstempStudent.Tables[0].Rows[0]["Merit No"].ToString();
                            txtscore.Text = dstempStudent.Tables[0].Rows[0]["Score"].ToString();
                            txtapplicationid.Text = dstempStudent.Tables[0].Rows[0]["ApplicationID"].ToString();
                            ddlDegree.SelectedItem.Text = (dstempStudent.Tables[0].Rows[0]["Degreeno"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dstempStudent.Tables[0].Rows[0]["Degreeno"].ToString()), "ACD_DEGREE", "DEGREENO", "DEGREENAME"));
                            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0", "D.DEGREENO");
                            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1", "BATCHNO DESC");


                            ddlDegree.SelectedValue = dstempStudent.Tables[0].Rows[0]["Degreeno"].ToString();


                            ddlBatch.SelectedItem.Text = (dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString() == string.Empty ? string.Empty : objCommon.GetDataByIDNo(Convert.ToInt32(dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString()), "ACD_ADMBATCH", "BATCHNO", "BATCHNAME"));
                            ddlBatch.SelectedValue = dstempStudent.Tables[0].Rows[0]["Admbatch"].ToString();

                            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");

                            if (dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "M" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "Male" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "MALE" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "male")
                                {
                                rdoMale.Checked = true;
                                }
                            else if (dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "F" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "Female" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "FEMALE" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "female")
                                {
                                rdoFemale.Checked = true;
                                }
                            else if (dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "O" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "Other" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "OTHER" || dstempStudent.Tables[0].Rows[0]["GENDER"].ToString() == "other")
                                {
                                rdoTransGender.Checked = true;
                                }

                            if (Convert.ToInt32(Session["OrgId"]) == 2)
                                {
                                ddlSchool.Enabled = true;
                                ddlDegree.Enabled = true;
                                }
                            }
                        else
                            {
                            objCommon.DisplayMessage(this, "This Application ID is not found in the System", this.Page);
                            Response.Redirect(Request.Url.ToString());
                            }
                        }
                    else
                        {
                        objCommon.DisplayMessage(this, "This Application ID is not found in the System", this.Page);
                        Response.Redirect(Request.Url.ToString());
                        }
                    }
                }
            }
        else if (Convert.ToInt32(Session["OrgId"]) != 1 || Convert.ToInt32(Session["OrgId"]) != 2 || Convert.ToInt32(Session["OrgId"]) != 6)
            {
            ShowStudentOnlineAdmDetails();


            }
        else
            {
            objCommon.DisplayMessage(this, "This Application ID is not found in the System", this.Page);
            Response.Redirect(Request.Url.ToString());
            }
        }

    private void DisableFields()
        {
        txtStudentfullName.Enabled = false;
        txtStudMobile.Enabled = false;
        txtStudEmail.Enabled = false;
        txtState.Enabled = false;
        ddlSchool.Enabled = false;
        ddlDegree.Enabled = false;
        ddlBranch.Enabled = false;
        ddlSemester.Enabled = false;
        ddlBatch.Enabled = false;
        // txtAge.Attributes.
        ddlBatch.Attributes.Add("readonly", "readonly");
        ddlAdmType.Enabled = false;
        ddlCategory.Enabled = false;
        txtDateOfBirth.Enabled = false;
        txtDateOfReporting.Enabled = false;
        ddlPaymentType.Enabled = false;
        txtAadhaarNo.Enabled = false;
        ddlAllotedCat.Enabled = false;

        txtStudentfullName.Enabled = false;
        rdoHosteler.Enabled = false;
        rdoInstallment.Enabled = false;
        rdbTransport.Enabled = false;
        ddlBloodGrp.Enabled = false;
        ddlAdmType.Enabled = false;
        btnSave.Enabled = false;
        ddlYear.Enabled = false;
        ddlinstallmenttype.Enabled = false;
        rdoscholarship.Enabled = false;
        txtStudMobile2.Enabled = false;
        txtduedate1.Enabled = false;
        txtduedate2.Enabled = false;
        txtduedate3.Enabled = false;
        txtduedate4.Enabled = false;
        txtduedate5.Enabled = false;
        ddladmthrough.Enabled = false;
        txtParentmobno.Enabled = false;
        ddlstate.Enabled = false;
        ddlstate.Attributes.Add("readonly", "readonly");
        }

    private void EnableFields()
        {
        txtStudMobile2.Enabled = true;
        txtStudentfullName.Enabled = true;
        txtStudMobile.Enabled = true;
        txtStudEmail.Enabled = true;
        txtState.Enabled = true;
        // ddlSchool.Enabled = true;
        // ddlDegree.Enabled = true;
        ddlBranch.Enabled = true;
        ddlSemester.Enabled = true;
        //ddlBatch.Enabled = true;
        ddlAdmType.Enabled = true;
        ddlCategory.Enabled = true;
        txtDateOfBirth.Enabled = true;
        txtDateOfReporting.Enabled = true;
        ddlPaymentType.Enabled = true;
        txtAadhaarNo.Enabled = true;
        ddlAllotedCat.Enabled = true;
        ddlstate.Enabled = true;
        txtStudentfullName.Enabled = true;
        rdoHosteler.Enabled = true;
        rdoInstallment.Enabled = true;
        rdbTransport.Enabled = true;
        ddlBloodGrp.Enabled = true;
        ddlAdmType.Enabled = true;
        btnSave.Enabled = true;
        ddlYear.Enabled = true;
        ddlinstallmenttype.Enabled = true;
        rdoscholarship.Enabled = true;
        txtduedate1.Enabled = true;
        txtduedate2.Enabled = true;
        txtduedate3.Enabled = true;
        txtduedate4.Enabled = true;
        txtduedate5.Enabled = true;
        ddladmthrough.Enabled = true;
        txtParentmobno.Enabled = true;
        }

    public int TransferToEmail(string useremail, string message, string subject)
        {
        int ret = 0;
        try
            {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
                {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                // fromPassword = Common.DecryptPassword(fromPassword);
                msg.From = new System.Net.Mail.MailAddress(fromAddress, "RCPIPER");
                msg.To.Add(new System.Net.Mail.MailAddress(useremail));
                msg.Subject = subject;
                msg.Body = message;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                    return true;
                    };

                smtp.Send(msg);
                if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                    {
                    return ret = 1;
                    //Storing the details of sent email
                    }
                else
                    {
                    return ret = 0;
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        return ret;

        }

    private void GetCategory(string category, int type)
        {
        if (category.Contains("E") && category.Contains("N"))
            if (type == 1)
                ddlCategory.SelectedValue = "4";
            else
                ddlCategory.SelectedValue = "4";

        else if (category.Contains("O") && category.Contains("B"))
            if (type == 1)
                ddlCategory.SelectedValue = "3";
            else
                ddlCategory.SelectedValue = "3";

        else if (category.Contains("S") && category.Contains("C"))
            if (type == 1)
                ddlCategory.SelectedValue = "2";
            else
                ddlCategory.SelectedValue = "2";

        else if (category.ToString().Contains("S") && category.Contains("T"))
            if (type == 1)
                ddlCategory.SelectedValue = "1";
            else
                ddlCategory.SelectedValue = "1";
        }

    protected void rdoInstallment_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            if (rdoInstallment.SelectedValue.Equals("0"))
                {
                divinstaltype.Visible = false;
                DivDuedate1.Visible = false;
                DivDuedate2.Visible = false;
                DivDuedate3.Visible = false;
                DivDuedate4.Visible = false;
                DivdueDate5.Visible = false;
                txtduedate1.Text = string.Empty;
                txtduedate2.Text = string.Empty;
                txtduedate3.Text = string.Empty;
                txtduedate4.Text = string.Empty;
                txtduedate5.Text = string.Empty;
                ddlinstallmenttype.SelectedIndex = 0;
                }
            else
                {
                divinstaltype.Visible = true;
                objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void ddlinstallmenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlinstallmenttype.SelectedIndex > 0)
            {
            string Installmenttype = string.Empty;
            Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
            Installmenttype = Installmenttype + '-';
            int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));
            if (count == 2)
                {
                DivDuedate1.Visible = true;
                DivDuedate2.Visible = true;
                DivDuedate3.Visible = false;
                DivDuedate4.Visible = false;
                DivdueDate5.Visible = false;
                }

            if (count == 3)
                {
                DivDuedate1.Visible = true;
                DivDuedate2.Visible = true;
                DivDuedate3.Visible = true;
                DivDuedate4.Visible = false;
                DivdueDate5.Visible = false;
                }

            if (count == 4)
                {
                DivDuedate1.Visible = true;
                DivDuedate2.Visible = true;
                DivDuedate3.Visible = true;
                DivDuedate4.Visible = true;
                DivdueDate5.Visible = false;
                }


            if (count == 5)
                {
                DivDuedate1.Visible = true;
                DivDuedate2.Visible = true;
                DivDuedate3.Visible = true;
                DivDuedate4.Visible = true;
                DivdueDate5.Visible = true;
                }
            txtduedate1.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtduedate1.Enabled = false;
            }
        else
            {
            DivDuedate1.Visible = false;
            DivDuedate2.Visible = false;
            DivDuedate3.Visible = false;
            DivDuedate4.Visible = false;
            DivdueDate5.Visible = false;
            }
        }

    protected void btnpayment_Click(object sender, EventArgs e)
        {
        //string Recon = objCommon.LookUp("ACD_DCR", "ISNULL(RECON,0)", "ENROLLNMENTNO='" + Session["Enrollno"].ToString() + "'");
        //if (Recon == "")
        //{
        //    Session["Newvalue"] = "1";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open('FeeCollection.aspx?PaymentMode=C&pageno=151&Title=Collection%20by%20Counter&RecTitle=Admission%20Fees&RecType=TF','_blank');", true);
        //}
        //else
        //{
        //    if (Convert.ToBoolean(Recon) == true)
        //    {
        //        Session["Newvalue"] = null;
        //        objCommon.DisplayMessage(this.updStudent, "Payment Already Done for this student via New Student Entry form for more details please visit Fee Collection form.", this.Page);
        //        btnpayment.Visible = false;
        //        return;
        //    }
        //    else
        //    {
        Session["Newvalue"] = "1";
        btnpayment.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open('FeeCollection.aspx?PaymentMode=C&pageno=151&Title=Collection%20by%20Counter&RecTitle=Admission%20Fees&RecType=TF','_blank');", true);
        //}
        // }

        }


    private void ClearAllFields()
        {
        txtFatherName.Text = string.Empty;
        txtStudentfullName.Text = string.Empty;
        txtStudMobile.Text = string.Empty;
        txtStudEmail.Text = string.Empty;
        txtState.Text = string.Empty;
        ddlSchool.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlSpecialisation.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        txtDateOfBirth.Text = string.Empty;
        //txtDateOfReporting.Text = string.Empty;
        ddlPaymentType.SelectedIndex = 0;
        txtAadhaarNo.Text = string.Empty;
        ddlAllotedCat.SelectedIndex = 0;
        //ddlstate.SelectedIndex = 0;
        //ddlstate.SelectedValue = "5";
        txtStudentfullName.Text = string.Empty;
        rdoHosteler.SelectedValue = "0";
        rdoInstallment.SelectedValue = "0";
        rdbTransport.SelectedValue = "0";
        ddlBloodGrp.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlinstallmenttype.SelectedIndex = 0;
        rdoscholarship.SelectedValue = "0";
        txtStudMobile2.Text = string.Empty;
        txtduedate1.Text = string.Empty;
        txtduedate2.Text = string.Empty;
        txtduedate3.Text = string.Empty;
        txtduedate4.Text = string.Empty;
        txtduedate5.Text = string.Empty;
        //ddladmthrough.SelectedIndex = 0;
        txtParentmobno.Text = string.Empty;


        divinstaltype.Visible = false;
        DivDuedate1.Visible = false;
        DivDuedate2.Visible = false;
        DivDuedate3.Visible = false;
        DivDuedate4.Visible = false;
        DivdueDate5.Visible = false;
        txtduedate1.Text = string.Empty;
        txtduedate2.Text = string.Empty;
        txtduedate3.Text = string.Empty;
        txtduedate4.Text = string.Empty;
        txtduedate5.Text = string.Empty;
        ddlinstallmenttype.SelectedIndex = 0;

        txtDateOfReporting.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtmerirtno.Text = string.Empty;
        txtscore.Text = string.Empty;
        ddlPaymentType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        //ddlBatch.SelectedIndex = 0;

        //  Response.Redirect(Request.Url.ToString());
        //}

        }

    public void DisableEnable()
        {
        dvMain.Visible = true;
        dvStudent.Visible = false;
        tempid.Visible = false;
        ddlDegree.Enabled = true;
        ddlSchool.Enabled = true;
        ddlBatch.Enabled = true;
        txtscore.Enabled = true;
        txtmerirtno.Enabled = true;

        }

    protected void btnNewStu_Click(object sender, EventArgs e)
        {
        dvMain.Visible = true;
        ClearAllFields();
        DisableEnable();
        }
    protected void btnSearchStu_Click(object sender, EventArgs e)
        {
        btnNewStudentS.Visible = false;
        dvMain.Visible = true;
        dvStudent.Visible = false;
        }

    protected void btnNewStudentS_Click(object sender, EventArgs e)
        {
        //ViewState["stuinfoidno"] = null;
        //Session["STUD_DETAILS"] = null;
        //ViewState["stuinfoidno"] = null;
        ClearAllFields();

        //DisableEnable();
        divGeneralInfo.Visible = false;
        }

    public void clearnew()
        {

        txtStudentfullName.Text = string.Empty;
        txtStudMobile.Text = string.Empty;
        txtStudEmail.Text = string.Empty;
        txtState.Text = string.Empty;
        ddlSchool.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        txtDateOfBirth.Text = string.Empty;
        //txtDateOfReporting.Text = string.Empty;
        ddlPaymentType.SelectedIndex = 0;
        txtAadhaarNo.Text = string.Empty;
        ddlAllotedCat.SelectedIndex = 0;
        //ddlstate.SelectedIndex = 0;
        // ddlstate.SelectedValue = 0
        txtStudentfullName.Text = string.Empty;
        rdoHosteler.SelectedIndex = 0;
        //  rdoInstallment.SelectedValue = "0";
        rdbTransport.SelectedIndex = 0;
        ddlBloodGrp.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlinstallmenttype.SelectedIndex = 0;
        rdoscholarship.SelectedValue = "0";
        txtStudMobile2.Text = string.Empty;
        txtduedate1.Text = string.Empty;
        txtduedate2.Text = string.Empty;
        txtduedate3.Text = string.Empty;
        txtduedate4.Text = string.Empty;
        txtduedate5.Text = string.Empty;
        //ddladmthrough.SelectedIndex = 0;
        txtParentmobno.Text = string.Empty;


        divinstaltype.Visible = false;
        DivDuedate1.Visible = false;
        DivDuedate2.Visible = false;
        DivDuedate3.Visible = false;
        DivDuedate4.Visible = false;
        DivdueDate5.Visible = false;
        txtduedate1.Text = string.Empty;
        txtduedate2.Text = string.Empty;
        txtduedate3.Text = string.Empty;
        txtduedate4.Text = string.Empty;
        txtduedate5.Text = string.Empty;
        ddlinstallmenttype.SelectedIndex = 0;

        txtDateOfReporting.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtmerirtno.Text = string.Empty;
        txtscore.Text = string.Empty;
        ddlPaymentType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;

        }

    //added by pooja
    protected void lnkIdpros_Click(object sender, EventArgs e)
        {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        ViewState["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        txtIDNo.Text = ViewState["stuinfoidno"].ToString();
        ShowStudentbyprosnoDetails();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#myModal2').modal('hide')", true);
        //myModal2.Visible = false;
        // Response.Redirect("~/academic/StudentRegistration.aspx");
        // Response.Redirect(Request.RawUrl, false);
        }


    private void ShowStudentbyprosnoDetails()
        {
        Session["STUD_DETAILS"] = null;

        StudentController objSC = new StudentController();
        DataSet ds = new DataSet();

        if (Session["usertype"].ToString() == "2")
            {
            ds = objSC.GetStuddetpros_JECRC(Convert.ToInt32(ViewState["stuinfoidno"]));
            txtStudentName.ReadOnly = false;
            txtStudentName.Visible = true;
            }
        else
            {
            ds = objSC.GetStuddetpros_JECRC(Convert.ToInt32(ViewState["stuinfoidno"]));
            }

        if (ds.Tables[0].Rows.Count > 0)
            {
            txtIDNo.Text = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
            txtIDNo.ToolTip = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
            txtStudentfullName.Text = ds.Tables[0].Rows[0]["STUDENT_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENT_NAME"].ToString();
            txtStudEmail.Text = ds.Tables[0].Rows[0]["EMAIL"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAIL"].ToString();
            txtStudMobile.Text = ds.Tables[0].Rows[0]["MOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["MOBILE"].ToString();
            ddlSchool.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? "0" : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENO");
            ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == null ? "0" : ds.Tables[0].Rows[0]["DEGREENO"].ToString();

            if (ds.Tables[0].Rows[0]["ISCORE"].ToString() == "1")
                {
                divSpecialisation.Visible = true;
                objCommon.FillDropDownList(ddlSpecialisation, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=1 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlSpecialisation.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["CORE_BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["CORE_BRANCHNO"].ToString();
                }
            else
                {
                divSpecialisation.Visible = false;
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                }
            ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMISSION_BATCH"] == null ? "0" : ds.Tables[0].Rows[0]["ADMISSION_BATCH"].ToString();

            objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
            objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
            objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0 AND ACTIVESTATUS=1", "SECTIONNO ASC");
            }
        Session["STUD_DETAILS"] = ds;
        }




    private void ShowStudentbyTempIDNODetails()
        {
        Session["STUD_DETAILS"] = null;

        StudentController objSC = new StudentController();
        DataSet ds = new DataSet();

        if (Session["usertype"].ToString() == "2")
            {
            ds = objSC.GetStuddetIDNO_JECRC(Convert.ToInt32(ViewState["stuinfoidno"]));
            txtStudentName.ReadOnly = false;
            txtStudentName.Visible = true;
            }
        else
            {
            ds = objSC.GetStuddetIDNO_JECRC(Convert.ToInt32(ViewState["stuinfoidno"]));
            }

        if (ds.Tables[0].Rows.Count > 0)
            {
            //txtIDNo.Text = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
            // txtIDNo.ToolTip = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
            txtStudentfullName.Text = ds.Tables[0].Rows[0]["STUDNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
            txtStudEmail.Text = ds.Tables[0].Rows[0]["EMAILID"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtStudMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            txtStudMobile2.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE2"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENTMOBILE2"].ToString();


            txtParentmobno.Text = ds.Tables[0].Rows[0]["FATHERMOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["FATHERMOBILE"].ToString();

            objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
            ddlstate.SelectedValue = ds.Tables[0].Rows[0]["STATENO"] == null ? "0" : ds.Tables[0].Rows[0]["STATENO"].ToString();
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");
            //ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"] == null ? "0" : ds.Tables[0].Rows[0]["CITYNO"].ToString();

            string Gender = ds.Tables[0].Rows[0]["GENDER"] == null ? "0" : ds.Tables[0].Rows[0]["GENDER"].ToString();

            if (Gender == "M")
                {
                rdoMale.Checked = true;
                }
            else if (Gender == "F")
                {
                rdoFemale.Checked = true;
                }
            else
                {
                rdoMale.Checked = false;
                rdoFemale.Checked = false;
                }


            ddlSchool.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? "0" : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENO");
            ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == null ? "0" : ds.Tables[0].Rows[0]["DEGREENO"].ToString();

            if (ds.Tables[0].Rows[0]["ISCORE"].ToString() == "1")
                {
                divSpecialisation.Visible = true;
                objCommon.FillDropDownList(ddlSpecialisation, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=1 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlSpecialisation.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["CORE_BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["CORE_BRANCHNO"].ToString();
                }
            else
                {
                divSpecialisation.Visible = false;
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                }


            objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
            ddladmthrough.SelectedValue = ds.Tables[0].Rows[0]["ADMROUNDNO"] == null ? "0" : ds.Tables[0].Rows[0]["ADMROUNDNO"].ToString();

            if (ds.Tables[0].Rows[0]["IDTYPE"].ToString() == "2")
                {
                //ddlYear.SelectedValue=0;
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
                ddlYear.SelectedValue = "2";
                objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
                ddlAdmType.SelectedValue = "2";
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
                ddlSemester.SelectedValue = "2";

                }
            else
                {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
                ddlYear.SelectedValue = "1";
                objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
                ddlAdmType.SelectedValue = "1";
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
                ddlSemester.SelectedValue = "1";

                }


            ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"] == null ? "0" : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();

            objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
            // objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
            // objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0 AND ACTIVESTATUS=1", "SECTIONNO ASC");

            objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0 AND ACTIVESTATUS=1", "CATEGORYNO");
            ddlAllotedCat.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"] == null ? "0" : ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
            objCommon.FillDropDownList(ddlPaymentType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0 AND ACTIVESTATUS=1", "PAYTYPENAME");
            ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["PTYPE"] == null ? "0" : ds.Tables[0].Rows[0]["PTYPE"].ToString();
            txtAppliedFees.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();

            txtDateOfReporting.Text = (ds.Tables[0].Rows[0]["ADMDATE"].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd/MM/yyyy"));

            }
        Session["STUD_DETAILS"] = ds;
        }

    private void ShowStudentOnlineAdmDetails()
        {
        Session["STUD_DETAILS"] = null;

        StudentController objSC = new StudentController();
        DataSet ds = new DataSet();

        if (Session["usertype"].ToString() == "2")
            {
            ds = objSC.getstudbyUsernameOA_JECRC(txtREGNo.Text);
            txtStudentName.ReadOnly = false;
            txtStudentName.Visible = true;
            }
        else
            {
            ds = objSC.getstudbyUsernameOA_JECRC(txtREGNo.Text);
            }

        if (ds.Tables[0].Rows.Count > 0)
            {
            txtIDNo.Text = ds.Tables[0].Rows[0]["USERNO"].ToString();
            Session["USERNO_OA"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
            txtIDNo.ToolTip = ds.Tables[0].Rows[0]["USERNO"].ToString();
            txtStudentfullName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
            txtFatherName.Text = ds.Tables[0].Rows[0]["FATHERNAME"].ToString();
            ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["PTYPE"] == null ? "0" : ds.Tables[0].Rows[0]["PTYPE"].ToString();
            txtStudEmail.Text = ds.Tables[0].Rows[0]["EMAILID"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtStudMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"] == null ? string.Empty : ds.Tables[0].Rows[0]["MOBILENO"].ToString();
            txtParentmobno.Text = ds.Tables[0].Rows[0]["F_MOBILENO"] == null ? string.Empty : ds.Tables[0].Rows[0]["F_MOBILENO"].ToString();
            objCommon.FillDropDownList(ddlstate, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0", "STATENAME");
            ddlstate.SelectedValue = ds.Tables[0].Rows[0]["STATENO"] == null ? "0" : ds.Tables[0].Rows[0]["STATENO"].ToString();
            objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0 and STATENO=" + ddlstate.SelectedValue, "CITY");
            ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"] == null ? "0" : ds.Tables[0].Rows[0]["CITYNO"].ToString();
            ddlSchool.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"] == null ? "0" : ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENO");
            ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"] == null ? "0" : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            if (ds.Tables[0].Rows[0]["ISCORE"].ToString() == "1")
                {
                divSpecialisation.Visible = true;
                objCommon.FillDropDownList(ddlSpecialisation, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=1 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlSpecialisation.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                try
                    {
                    ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["CORE_BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["CORE_BRANCHNO"].ToString();
                    }
                catch (Exception ex)
                    {
                    objCommon.DisplayMessage(this.Page, "Core Branch Mapping is not found for searched Application ID.", this.Page);
                    ClearAllFields();
                    return;
                    }
                }
            else
                {
                divSpecialisation.Visible = false;
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? "0" : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                }
            if (ds.Tables[0].Rows[0]["GENDER"].ToString() == "M" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "Male" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "MALE" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "male")
                {
                rdoMale.Checked = true;
                }
            else if (ds.Tables[0].Rows[0]["GENDER"].ToString() == "F" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "Female" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "FEMALE" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "female")
                {
                rdoFemale.Checked = true;
                }
            else if (ds.Tables[0].Rows[0]["GENDER"].ToString() == "O" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "Other" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "OTHER" || ds.Tables[0].Rows[0]["GENDER"].ToString() == "other")
                {
                rdoTransGender.Checked = true;
                }
            string exist1 = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "EMAILID='" + txtStudEmail.Text.Trim().ToString() + "'");
            string exist2 = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "STUDENTMOBILE='" + txtStudMobile.Text.Trim().ToString() + "'");

            if (Convert.ToInt32(exist1) > 0 || Convert.ToInt32(exist2) > 0)
                {
                objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1 ", "BATCHNO DESC");
                ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"] == null ? "0" : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                }
            else
                {

                ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"] == null ? "0" : ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                }
            ddlAllotedCat.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"] == null ? "0" : ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
            objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
            ddladmthrough.SelectedValue = "1";
            if (ds.Tables[0].Rows[0]["ADMTYPE"].ToString() == "2")
                {
                //ddlYear.SelectedValue=0;
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
                ddlYear.SelectedValue = "2";
                objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
                ddlAdmType.SelectedValue = "2";
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
                ddlSemester.SelectedValue = "2";

                }
            else
                {
                objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "YEAR");
                ddlYear.SelectedValue = "1";
                objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO > 0 AND ACTIVESTATUS=1", "IDTYPENO");
                ddlAdmType.SelectedValue = "1";
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO>0 AND ACTIVESTATUS=1", "SEMESTERNO");
                ddlSemester.SelectedValue = "1";

                }
            //ddlAdmType.SelectedValue = ds.Tables[0].Rows[0]["IDTYPENO"] == null ? "0" : ds.Tables[0].Rows[0]["IDTYPENO"].ToString();
            objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
            //objCommon.FillDropDownList(ddladmthrough, "ACD_ADMISSION_ROUND", "ADMROUNDNO", "ROUNDNAME", "ADMROUNDNO > 0 AND ACTIVESTATUS=1", "ADMROUNDNO");
            objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO >0 AND ACTIVESTATUS=1", "SECTIONNO ASC");
            if (Convert.ToInt32(exist1) > 0 || Convert.ToInt32(exist1) > 0)
                {

                if (ds.Tables[0].Rows[0]["APPLICATIONID_STUD"].ToString() == "")
                    {
                    objCommon.DisplayMessage(this.Page, "Mobile or Emailid already Found for Entered Application Number.", this.Page);
                    //clearcontrols();
                    return;
                    }
                txtmerirtno.Text = ds.Tables[0].Rows[0]["MERITNO"].ToString();
                txtscore.Text = ds.Tables[0].Rows[0]["SCORE"].ToString();
                txtapplicationid.Text = ds.Tables[0].Rows[0]["APPLICATIONID"].ToString();
                //   txtAadhaarNo.Text = ds.Tables[0].Rows[0]["ADDHARCARDNO"].ToString();
                rdoInstallment.SelectedValue = ds.Tables[0].Rows[0]["INSTALLMENT"].ToString();
                if (rdoInstallment.SelectedValue.Equals("0"))
                    {
                    divinstaltype.Visible = false;
                    DivDuedate1.Visible = false;
                    DivDuedate2.Visible = false;
                    DivDuedate3.Visible = false;
                    DivDuedate4.Visible = false;
                    DivdueDate5.Visible = false;
                    }
                else
                    {
                    divinstaltype.Visible = true;

                    objCommon.FillDropDownList(ddlinstallmenttype, "ACD_INSTALLMENT_MASTER", "INSTALLMENT_NO", "INSTALLMENT_TYPE", "INSTALLMENT_NO>0", "INSTALLMENT_NO");
                    ddlinstallmenttype.SelectedValue = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DISTINCT INSTALLMENT_TYPE", "IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                    string Installmenttype = string.Empty;
                    Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
                    Installmenttype = Installmenttype + '-';
                    int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));
                    if (count == 2)
                        {
                        DivDuedate1.Visible = true;
                        DivDuedate2.Visible = true;
                        DivDuedate3.Visible = false;
                        DivDuedate4.Visible = false;
                        DivdueDate5.Visible = false;
                        string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                        txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                        }

                    if (count == 3)
                        {
                        DivDuedate1.Visible = true;
                        DivDuedate2.Visible = true;
                        DivDuedate3.Visible = true;
                        DivDuedate4.Visible = false;
                        DivdueDate5.Visible = false;
                        string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                        txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                        }

                    if (count == 4)
                        {
                        DivDuedate1.Visible = true;
                        DivDuedate2.Visible = true;
                        DivDuedate3.Visible = true;
                        DivDuedate4.Visible = true;
                        DivdueDate5.Visible = false;
                        string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date4 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=4 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                        txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate4.Text = (Due_date4.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date4.ToString()).ToString("dd/MM/yyyy"));
                        }

                    if (count == 5)
                        {
                        DivDuedate1.Visible = true;
                        DivDuedate2.Visible = true;
                        DivDuedate3.Visible = true;
                        DivDuedate4.Visible = true;
                        DivdueDate5.Visible = true;

                        string Due_date1 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=1 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date2 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=2 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date3 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=3 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date4 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=4 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");
                        string Due_date5 = objCommon.LookUp("ACD_FEES_INSTALLMENT", "DUE_DATE", "INSTALMENT_NO=5 AND IDNO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()) + "");

                        txtduedate1.Text = (Due_date1.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date1.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate2.Text = (Due_date2.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date2.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate3.Text = (Due_date3.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date3.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate4.Text = (Due_date4.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date4.ToString()).ToString("dd/MM/yyyy"));
                        txtduedate5.Text = (Due_date5.ToString() == string.Empty ? string.Empty : Convert.ToDateTime(Due_date5.ToString()).ToString("dd/MM/yyyy"));
                        }
                    }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["HOSTELER"]) == true)
                    {
                    rdoHosteler.SelectedValue = "1";
                    divHostel.Visible = true;
                    rdbTransport.Visible = true;
                    rdbTransport.Enabled = false;
                    objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL_FESS_TYPE", "HID", "HOSTELTYPE", "HID>0 ", "HID");
                    ddlHostel.SelectedValue = ds.Tables[0].Rows[0]["HOSTEL_STATUS"] == null ? "0" : ds.Tables[0].Rows[0]["HOSTEL_STATUS"].ToString();
                    }
                else
                    {
                    rdoHosteler.SelectedValue = "0";
                    }

                rdbTransport.SelectedValue = ds.Tables[0].Rows[0]["TRANSPORT"].ToString();
                rdoscholarship.SelectedValue = ds.Tables[0].Rows[0]["SCHOLORSHIP"].ToString();
                if (rdoscholarship.SelectedValue == "1")
                    {
                    divschmode.Visible = CheckControlToHide("divschmode");
                    divschtype.Visible = CheckControlToHide("divschtype");
                    ddlSchMode.SelectedValue = ds.Tables[0].Rows[0]["SCH_MODE"].ToString();
                    ddlSchType.SelectedValue = ds.Tables[0].Rows[0]["SCHOLARSHIP_ID"].ToString();
                    if (ddlSchMode.SelectedValue == "1")
                        {
                        divAmt.Visible = CheckControlToHide("divAmt");
                        lblamt.Text = "Enter Scholarship Percentage";
                        txtschAmt.MaxLength = 2;
                        rfvschamt.ErrorMessage = "Please Enter Scholarship Percentage";
                        txtschAmt.Text = ds.Tables[0].Rows[0]["SCH_AMT_PER"].ToString();
                        }
                    else if (ddlSchMode.SelectedValue == "2")
                        {

                        divAmt.Visible = CheckControlToHide("divAmt");
                        lblamt.Text = "Enter Scholarship Amount";
                        txtschAmt.MaxLength = 12;
                        rfvschamt.ErrorMessage = "Please Enter Scholarship Amount";
                        txtschAmt.Text = ds.Tables[0].Rows[0]["SCH_AMT_PER"].ToString();
                        }
                    else
                        {
                        divAmt.Visible = false;
                        lblamt.Text = "";
                        txtschAmt.MaxLength = 2;
                        rfvschamt.ErrorMessage = "";
                        }
                    }
                else
                    {
                    divschmode.Visible = false;
                    divschtype.Visible = false;
                    divAmt.Visible = false;
                    lblamt.Text = "";
                    txtschAmt.MaxLength = 2;
                    rfvschamt.ErrorMessage = "";
                    ddlSchMode.SelectedIndex = 0;
                    }
                }

            else
                {
                divinstaltype.Visible = false;
                DivDuedate1.Visible = false;
                DivDuedate2.Visible = false;
                DivDuedate3.Visible = false;
                DivDuedate4.Visible = false;
                DivdueDate5.Visible = false;
                divschmode.Visible = false;
                divAmt.Visible = false;
                lblamt.Text = "";
                txtschAmt.MaxLength = 2;
                rfvschamt.ErrorMessage = "";
                ddlSchMode.SelectedIndex = 0;
                ddlSchType.SelectedIndex = 0;
                rdoHosteler.SelectedValue = "0";
                }
            }
        else
            {
            objCommon.DisplayMessage(this.Page, "Entered Application ID is not Verified.", this.Page);
            clearcontrols();
            return;
            }
        Session["STUD_ONLINEADMDETAILS"] = ds;
        }


    static async Task<int> Execute(string Message, string toEmailId, string sub)
        {
        int ret = 0;
        try
            {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
                {
                ret = 1;
                Console.WriteLine("Email Sent successfully!");

                }
            else
                {
                ret = 0;
                Console.WriteLine("Fail to send Mail!");
                }
            //attachments.Dispose();
            }
        catch (Exception ex)
            {
            ret = 0;
            }
        return ret;
        }
    static async Task<int> Execute(string Message, string toEmailId, string sub, string BccEmail)
        {
        int ret = 0;
        try
            {

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;

            var emails = toEmailId.Split(',');
            var bccemails = BccEmail.Split(',');

            var to = new List<EmailAddress>();
            foreach (var i in emails)
                {
                to.Add(new EmailAddress(i));
                }
            var bcc = new List<EmailAddress>();
            foreach (var i in bccemails)
                {
                bcc.Add(new EmailAddress(i));
                }

            var plainTextContent = "";
            var htmlContent = Message;
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
            //var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent,htmlContent);
            msg.AddBccs(bcc);
            //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
                {
                ret = 1;
                Console.WriteLine("Email Sent successfully!");

                }
            else
                {
                ret = 0;
                Console.WriteLine("Fail to send Mail!");
                }
            //attachments.Dispose();
            }
        catch (Exception ex)
            {
            ret = 0;
            }
        return ret;
        }



    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
        int branchno = 0;
        if (ddlSpecialisation.SelectedIndex > 0)
            {
            branchno = Convert.ToInt32(ddlSpecialisation.SelectedValue);
            }
        else
            {
            branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
        DataSet ds = objSC.GetStandardFees(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), branchno, Convert.ToInt32(ddlSchool.SelectedValue));
        txtAppliedFees.Text = ds.Tables[0].Rows[0]["STD_FEES"].ToString();
        }
    protected void rdoscholarship_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (rdoscholarship.SelectedValue == "1")
            {
            divschmode.Visible = CheckControlToHide("divschmode");
            divschtype.Visible = CheckControlToHide("divschtype");
            divAmt.Visible = false;
            }
        else
            {
            divschmode.Visible = false;
            divAmt.Visible = false;
            lblamt.Text = "";
            txtschAmt.MaxLength = 2;
            rfvschamt.ErrorMessage = "";
            ddlSchMode.SelectedIndex = 0;
            ddlSchType.SelectedIndex = 0;
            divschtype.Visible = false;
            }
        }
    protected void ddlSchMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        if (ddlSchMode.SelectedValue == "1")
            {
            divAmt.Visible = CheckControlToHide("divAmt");
            lblamt.Text = "Enter Scholarship Percentage";
            txtschAmt.MaxLength = 2;
            rfvschamt.ErrorMessage = "Please Enter Scholarship Percentage";
            }
        else if (ddlSchMode.SelectedValue == "2")
            {

            divAmt.Visible = CheckControlToHide("divAmt");
            lblamt.Text = "Enter Scholarship Amount";
            txtschAmt.MaxLength = 12;
            rfvschamt.ErrorMessage = "Please Enter Scholarship Amount";
            }
        else
            {
            divAmt.Visible = false;
            lblamt.Text = "";
            txtschAmt.MaxLength = 2;
            rfvschamt.ErrorMessage = "";
            }

        }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlBranch.SelectedIndex > 0)
            {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "count(1)", "CORE_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue)));
            if (count > 0)
                {
                divSpecialisation.Visible = true;
                objCommon.FillDropDownList(ddlSpecialisation, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND ISNULL(B.ISCORE,0)=1 AND ISNULL(ISSPECIALISATION,0) = 1 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue) + " AND CD.CORE_BRANCHNO =" + Convert.ToInt32(ddlBranch.SelectedValue), "B.LONGNAME");

                }
            else
                {
                divSpecialisation.Visible = false;
                }
            }
        else
            {
            divSpecialisation.Visible = false;

            }

        int branchno = 0;
        if (ddlSpecialisation.SelectedIndex > 0)
            {
            branchno = Convert.ToInt32(ddlSpecialisation.SelectedValue);
            }
        else
            {
            branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            }
        DataSet ds = objSC.GetStandardFees(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), branchno, Convert.ToInt32(ddlSchool.SelectedValue));
        txtAppliedFees.Text = ds.Tables[0].Rows[0]["STD_FEES"].ToString();
        }
    protected void lnkIdamonline_Click(object sender, EventArgs e)
        {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        ViewState["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        txtIDNo.Text = ViewState["stuinfoidno"].ToString();
        ShowStudentOnlineAdmDetails();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#myModal2').modal('hide')", true);
        }

    ////********** Added by Rahul Moraskar 2022-07-26
    private void ControlInit()
        {
        DataSet ControlRights = objSC.GetStudentConfig(Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        for (int i = 0; i < ControlRights.Tables[0].Rows.Count; i++)
            {
            ViewState["ControlRights"] = ControlRights.Tables[0];
            DataTable DTControlRights = ControlRights.Tables[0];

            var ctrlToHIDE = FindControlRecursive(this.Page, string.Format(DTControlRights.Rows[i]["CONTROL_TO_HIDE"].ToString()));
            if (ctrlToHIDE != null)
                {
                if (Convert.ToInt32(DTControlRights.Rows[i]["ISACTIVE"]) == 1)
                    ctrlToHIDE.Visible = true;
                else
                    ctrlToHIDE.Visible = false;
                }

            var ctrlToMANDATORY = FindControlRecursive(this.Page, string.Format(DTControlRights.Rows[i]["CONTROL_TO_MANDATORY"].ToString()));
            if (ctrlToMANDATORY != null)
                {
                if (Convert.ToInt32(DTControlRights.Rows[i]["ISMANDATORY"]) == 1)
                    ctrlToMANDATORY.Visible = true;
                else
                    ctrlToMANDATORY.Visible = false;
                }
            }


        DataTable dtControlRights = (DataTable)ViewState["ControlRights"];

        foreach (DataRow ActScholarship in dtControlRights.Select("ORGANIZATION_ID = " + Convert.ToInt32(Session["OrgId"]) + " and PAGE_NO = " + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + " AND CONTROL_TO_HIDE = 'divScholarship'", "ORGANIZATION_ID ASC"))
            {
            divScholarshipDetails.Visible = (bool)ActScholarship["ISACTIVE"];
            }
        foreach (DataRow ActScholarship in dtControlRights.Select("ORGANIZATION_ID = " + Convert.ToInt32(Session["OrgId"]) + " and PAGE_NO = " + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + " AND CONTROL_TO_HIDE = 'divinstallment'", "ORGANIZATION_ID ASC"))
            {
            divFeeInstallmentDetails.Visible = (bool)ActScholarship["ISACTIVE"];
            }
        foreach (DataRow ActScholarship in dtControlRights.Select("ORGANIZATION_ID = " + Convert.ToInt32(Session["OrgId"]) + " and PAGE_NO = " + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + " AND CONTROL_TO_HIDE = 'divhosteller'", "ORGANIZATION_ID ASC"))
            {
            divHostellerDetails.Visible = (bool)ActScholarship["ISACTIVE"];
            }
        foreach (DataRow ActScholarship in dtControlRights.Select("ORGANIZATION_ID = " + Convert.ToInt32(Session["OrgId"]) + " and PAGE_NO = " + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + " AND CONTROL_TO_HIDE = 'divTransportation'", "ORGANIZATION_ID ASC"))
            {
            divTransportationDetails.Visible = (bool)ActScholarship["ISACTIVE"];
            }

        }

    ////********** Added by Rahul Moraskar 2022-07-26
    private void RequiredFieldValidatorEnableDisable()
        {
        rfvStudent.Enabled = CheckControlToMandatory("supStudentfullName");
        rfvStudMobile.Enabled = CheckControlToMandatory("supStudMobile");
        rfvStudMobile2.Enabled = CheckControlToMandatory("supStudentMoblieNo2");
        rfvparentmobno.Enabled = CheckControlToMandatory("supParentMobileNo");
        rfvStudEmail.Enabled = CheckControlToMandatory("supPersonalEmail");
        rfvstate.Enabled = CheckControlToMandatory("supstate");
        rfvCity.Enabled = CheckControlToMandatory("supCity");
        rfvSchool.Enabled = CheckControlToMandatory("supSchool");
        rfvDegree.Enabled = CheckControlToMandatory("supDegree");
        rfvddlBranch.Enabled = CheckControlToMandatory("supBranch");
        rfvAdmission.Enabled = CheckControlToMandatory("supAdmType");
        rfvYear.Enabled = CheckControlToMandatory("supYear");
        rfvSemester.Enabled = CheckControlToMandatory("supSemester");
        rfvBatch.Enabled = CheckControlToMandatory("supBatch");
        rfvSpecialisation.Enabled = CheckControlToMandatory("supSpecialisation");
        rfvadmthrough.Enabled = CheckControlToMandatory("supadmthrough");
        rfvothetentrance.Enabled = CheckControlToMandatory("supotherentrance");
        rfvAllotedCategory.Enabled = CheckControlToMandatory("supAllotedCat");
        rfvDateOfReporting.Enabled = CheckControlToMandatory("supDateOfReporting");
        rfvPaymentType.Enabled = CheckControlToMandatory("supPaymentType");
        rfvmerirtno.Enabled = CheckControlToMandatory("supmerirtno");
        rfvscore.Enabled = CheckControlToMandatory("supScore");
        rfvSchMode.Enabled = CheckControlToMandatory("supschmode");
        rfvschamt.Enabled = CheckControlToMandatory("supAmt");
        rfvduedate1.Enabled = CheckControlToMandatory("supDuedate1");
        rfvduedate2.Enabled = CheckControlToMandatory("supDuedate2");
        rfvduedate3.Enabled = CheckControlToMandatory("supDuedate3");
        rfvduedate4.Enabled = CheckControlToMandatory("supDuedate4");
        rfvduedate5.Enabled = CheckControlToMandatory("supDuedate5");
        rfvHostel.Enabled = CheckControlToMandatory("supHostel");
        rfvMobileNo.Enabled = CheckControlToMandatory("supHostel");
        rfvFatherName.Enabled = CheckControlToMandatory("supFatherName");

        }

    ////********** Added by Rahul Moraskar 2022-07-26
    private static Control FindControlRecursive(Control root, string id)
        {
        if (root != null && !string.IsNullOrWhiteSpace(id))
            {
            if (root.ID == id)
                {
                return root;
                }

            foreach (Control c in root.Controls)
                {
                Control t = FindControlRecursive(c, id);

                if (t != null)
                    {
                    return t;
                    }
                }
            }

        return null;
        }
    ////********** Added by Rahul Moraskar 2022-07-26
    private bool CheckControlToHide(string ControlName)
        {
        //ControlRights
        DataTable dtControlRights = (DataTable)ViewState["ControlRights"];

        foreach (DataRow ActScholarship in dtControlRights.Select("ORGANIZATION_ID = " + Convert.ToInt32(Session["OrgId"]) + " and PAGE_NO = " + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + " AND CONTROL_TO_HIDE = '" + ControlName + "'", "ORGANIZATION_ID ASC"))
            {
            return (bool)ActScholarship["ISACTIVE"];
            }
        return true;

        }

    ////********** Added by Rahul Moraskar 2022-07-26
    private bool CheckControlToMandatory(string ControlName)
        {
        //ControlRights
        DataTable dtControlRights = (DataTable)ViewState["ControlRights"];

        foreach (DataRow ActScholarship in dtControlRights.Select("ORGANIZATION_ID = " + Convert.ToInt32(Session["OrgId"]) + " and PAGE_NO = " + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + " AND CONTROL_TO_MANDATORY = '" + ControlName + "'", "ORGANIZATION_ID ASC"))
            {
            return (bool)ActScholarship["ISMANDATORY"];
            }
        return true;

        }
    ////********** END by Rahul Moraskar 2022-07-26


    protected void btnsendmail_Click(object sender, EventArgs e)
        {
        try
            {

            StudentController objSC = new StudentController();
            UserAcc objUa = new UserAcc();
            Student objS = new Student();

            string email_type = string.Empty;
            string Link = string.Empty;
            int sendmail = 0;
            string subject = string.Empty;
            string srnno = string.Empty;
            string pwd = string.Empty;
            int status = 0;
            string templateText = string.Empty;
            string IDNO = Session["IDNO"].ToString();

            int IDNOnew = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "APPLICATIONID='" + Convert.ToString(txtREGNo.Text) + "'"));

            string DAmount = Convert.ToString(objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0) TOTAL_AMT", "IDNO=" + IDNOnew));

            string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);

            string Username = string.Empty;
            string Password = string.Empty;

            string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + IDNOnew);
            string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "B.LONGNAME", "IDNO=" + IDNOnew);

            string Userno = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO=" + IDNOnew);
            string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + IDNOnew);
            string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + IDNOnew);
            Username = objCommon.LookUp("USER_ACC", "UA_NAME", " UA_TYPE=2 AND UA_IDNO=" + Convert.ToInt32(IDNOnew));
            Password = objCommon.LookUp("USER_ACC", "UA_PWD", " UA_TYPE=2 AND UA_IDNO=" + Convert.ToInt32(IDNOnew));


            Password = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Password.ToString());
           // int TemplateTypeId = 0;
           // int TemplateId = 0;
            string ApplicationId=string.Empty;
            //Assign by Nikhil L. on 12-06-2023 hard coded for JECRC offer letter.(temporary commented)
            //TemplateTypeId=6;
           // TemplateId=7;

            //DataSet ds_mstQry = objUC.GetEmailTemplateConfigData(TemplateTypeId, TemplateId, IDNOnew,ApplicationId);
            //if (ds_mstQry != null && ds_mstQry.Tables.Count == 3)
            //{
            //    if (ds_mstQry.Tables[0].Rows.Count > 0 && ds_mstQry.Tables[1].Rows.Count > 0 && ds_mstQry.Tables[2].Rows.Count > 0)
            //    {
            //        DataTable dt_DataField = ds_mstQry.Tables[0];
            //        subject = ds_mstQry.Tables[1].Rows[0]["EMAILSUBJECT"].ToString();
            //        templateText = ds_mstQry.Tables[1].Rows[0]["TEMPLATETEXT"].ToString();
            //        for (int i = 0; i < dt_DataField.Rows.Count; i++)
            //        {
            //            if (templateText.Contains(dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString()))
            //            {
            //                string dataFieldDisp = dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString();
            //                if(dataFieldDisp=="PASSWORD")
            //                {
            //                    ds_mstQry.Tables[2].Rows[0][dataFieldDisp] = clsTripleLvlEncyrpt.ThreeLevelDecrypt(ds_mstQry.Tables[2].Rows[0][dataFieldDisp].ToString());
            //                }
            //                templateText = templateText.Replace("[" + dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString() + "]", ds_mstQry.Tables[2].Rows[0][dataFieldDisp].ToString());
            //            }
            //        }
            //    }
            //}
            //return;

            //Session["Enrollno"] = srnno;
            DataSet ds = getModuleConfig();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
               // email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

                if (sendmail == 1)
                    {
                 
                    subject = "Provisional Admission Confirmation Payment Link.";
                    string message = "";
                    message = templateText;
                    message += "<p>Dear :<b>" + Name + "</b> </p>";
                    message += "<p></p>You are shortlisted for Provisional Allotment of seat in <b>" + college + "</b> in <b>" + Branchname + "</b><br/>You Can Pay First Installment Fees using the following Link and credentials. </td><p style=font-weight:bold;> " + MISLink + " </p><p>Username   : " + Username + " <br/>Password: " + Password + "</p><p style=font-weight:bold;>Fee Details: <br/><b>Note:</b> You can find your fee details after login in your portal using above login credentials.</p><p>The provisional admission shall be confirmed after establishing you are eligibility in the qualifying exam after declaration of the result by the examining Board/University as per the schedule notified by the University.</p><p>You will be required to submit self attested photo copies of the following documents at the time of final admission,once the University calls for the same.</p><p style=color:blue;>Documents required at the time of Confirmation of Admission: Originals along with one set of Self attested Photocopy.</p>";
                    message += " <table style='border: 1px solid black;border-collapse: collapse;'>";
                    message += " <tr >";
                    message += " <td style='border: 1px solid black;'>";
                    message += "JEE Mains Score Card (if appeared/ applicable)</td>";
                    message += "<td style='border: 1px solid black;'> Migration Certificate</td></tr>";
                    message += " <tr style='border: 1px solid black;'><td style='border: 1px solid black;'> 10th Marksheet & 12th Marksheet </td> <td style='border: 1px solid black;'> Transfer Certificate</td> </tr>";
                    message += "  <td style='border: 1px solid black;'>12th Mark-sheet</td><td style='border: 1px solid black;'>Character Certificate</td>";
                    message += "<tr></tr><tr><td style='border: 1px solid black;'>Graduation Final Year Mark-sheet (for PG Admissions)</td>";
                    message += "<td style='border: 1px solid black;'>Caste Certificate (if applicable)</td></tr>";
                    message += "<tr><td style='border: 1px solid black;'>Copy of Aadhar Card (UID)</td><td>OBC Non Creamy layer Certificate (if applicable)</td></tr>";
                    message += "<tr><td style='border: 1px solid black;'>3 Passport Size Photographs</td>";
                    message += "<td style='border: 1px solid black;'>Printout of application form (if filled online)</td></tr></table>";
                    message += "<p>The final admission is subject to clearing the document verification. In case you found ineligible for admission in the said program and denied by admission department after verification at any stage, the complete registration fee shall be refunded and admission will be cancelled.</p><p><b>Note:</b></p>";
                    message += "<p>1.All the documents must be uploaded on URL:<b>  " + MISLink + " </b> <br/>";
                    message += "2.After submission of registration fees, new user ID will be sent to your registered mail id separately.<br/>";
                    message += "3.Process of fee payment: Login using above credentials in <b> " + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.<br/>(Any query regarding admission send email to admission@jecrcu.edu.in)";
                    message += "<p style=font-weight:bold;>Thanks<br>Team Admissions <br>JECRC University, Jaipur</p>";

                    // +MISLink + " Username: " + Username + " Password: " + Password;
                    //message = "
                    objS.EmailID = txtStudEmail.Text.ToString();
                    string BCCEmailID = string.Empty;
                    string BCCEMAIL = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(BBC_MAIL_NEW_STUD_ENTRY,0) as BBC_MAIL_NEW_STUD_ENTRY", "");
                    if (BCCEMAIL != "0")
                        {
                        BCCEmailID = BCCEMAIL;
                        }
                    else
                        {
                        BCCEmailID = "abc@gmail.com";
                        }

                    //------------Code for sending email,It is optional---------------
                    // int status = sendEmail(message, useremail, subject);
                    //int reg = TransferToEmail(objS.EmailID, message, subject); //--tempoary Commented

                    //if (email_type == "1" && email_type != "")
                    //    {
                    //    int reg = TransferToEmail(objS.EmailID, message, subject);
                    //    }
                    //else if (email_type == "2" && email_type != "")
                    //    {
                    //    Task<int> task = Execute(message, objS.EmailID, subject, BCCEmailID);
                    //    status = task.Result;
                    //    }
                    //if (email_type == "3" && email_type != "")
                    //    {
                    //    OutLook_Email(message, objS.EmailID, subject);
                    //    }


                    status = objSendEmail.SendEmail(objS.EmailID, message, subject); //Calling Method
                    }
                }

            if (status == 1)
                {
                objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
                }
            else
                {
                objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
                }

            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            string Att = string.Empty;
            string Dept = string.Empty;
            string templatename = "Provisonal Admission";
            //DataSet ds1 = null;
            // DataSet ds = new DataSet();
            DataSet ds1 = objUC.GetWhatsappTemplate(0, templatename);
            if (ds1.Tables[0].Rows.Count > 0)
                {
                TEMPLATE = ds1.Tables[0].Rows[0]["TEMPLATE"].ToString();

                }

            string Whatsappmessage = TEMPLATE;

            string MOBILENO = objCommon.LookUp("ACD_STUDENT", "STUDENTMOBILE", "IDNO=" + IDNOnew);
            // message = message.Replace("{#var#}", Att);
            //message = message.Replace("{#var1#}", Dept);

            // Create a StringBuilder and append the template
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append(message);
            // Get the final message string
            //string template = stringBuilder.ToString();
            //SendSMS_today(lblParMobile.Text.Trim(), template, TemplateID);
            //MailSendStatus += hdnidno1.Value + ',';
            Whatsaapjecrc(Whatsappmessage, MOBILENO);

            }
        catch (Exception ex)
            {

            throw;
            }
        }


    protected void CreateUser(string student_Name, string RRNO, string Email)
        {
        try
            {
            string UA_PWD = string.Empty;
            string UA_ACC = string.Empty;
            string password = string.Empty;
            int IDNO = 0;
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 3)
                {
                string PasswordName = CommonComponent.GenerateRandomPassword.GenearteFourLengthPassword();
                Session["UAR_PASS"] = PasswordName;
                UA_PWD = PasswordName;
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(UA_PWD);
                UA_ACC = "0,500,76";
                IDNO = Convert.ToInt32(Session["IDNO"]);
                }
            else if (Convert.ToInt32(Session["OrgId"].ToString()) == 5)
                {

                IDNO = Convert.ToInt32(Session["IDNO"]);
                string Username = string.Empty;
                string Password = string.Empty;
                string Userno = objCommon.LookUp("ACD_STUDENT", "USERNO", "IDNO=" + IDNO);
                string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + IDNO);
                //Username = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + Userno);
                //Password = objCommon.LookUp("ACD_USER_REGISTRATION", "User_Password", "USERNO=" + Userno);
                Username = Session["IDNO"].ToString();

                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(IDNO.ToString());
                UA_ACC = "0,500,76";

                RRNO = Username.ToString();
                }
            else
                {
                IDNO = Convert.ToInt32(Session["IDNO"]);
                UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(IDNO.ToString());
                UA_ACC = "0,500,76";
                RRNO = IDNO.ToString();
                }

            CustomStatus CS = (CustomStatus)feeController.CreateUser_JECRC(RRNO, UA_PWD, student_Name, Email, UA_ACC, IDNO);
            }
        catch (Exception ex)
            {
            throw;
            }
        }



    protected void CreateUserRCPITPIPER(string USERNAME, string PASSWORD, string EMAIL, string student_Name)
        {
        try
            {
            string UA_PWD = string.Empty;
            string UA_ACC = string.Empty;
            string password = string.Empty;
            int IDNO = 0;

            IDNO = Convert.ToInt32(Session["IDNO"]);
            UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(PASSWORD.ToString());
            UA_ACC = "0,500,76";
            IDNO = Convert.ToInt32(Session["IDNO"]);
            CustomStatus CS = (CustomStatus)feeController.CreateUser(USERNAME, UA_PWD, student_Name, EMAIL, UA_ACC, IDNO);
            }
        catch (Exception ex)
            {
            throw;
            }
        }
    protected void ddlSchType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    protected void Whatsaapjecrc(string Message, string ToMobileNo)
        {
        try
            {
            var client = new RestClient("http://cp.sendwpsms.com/api/sendwp");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "ci_session=ig64ncijtbeakcvklsa79bren5dobnn9");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("username", "jecrc");
            request.AddParameter("password", "jecrc");
            request.AddParameter("message", Message.ToString());
            request.AddParameter("registerednumber", "8108221708");
            request.AddParameter("to", ToMobileNo.ToString());
            request.AddParameter("type", "m");
            request.AddParameter("uuid", "b4bd3cdd-c959-4596-ad92-ce8a1cabd777");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            }
        catch (WebException webEx)
            {
            Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
            Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String body = reader.ReadToEnd();
            Console.WriteLine(body);
            }
        }
    protected void lnkTempID_Click(object sender, EventArgs e)
        {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        ViewState["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        txtIDNo.Text = ViewState["stuinfoidno"].ToString();
        ShowStudentbyTempIDNODetails();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#myModal2').modal('hide')", true);
        }

    bool ReturnValue()
        {
        return false;
        }


    public void savedata()
        {
        string regNo = string.Empty;
        string IUEmail = string.Empty;
        StudentController objSC = new StudentController();
        Student objS = new Student();
        StudentAddress objSAddress = new StudentAddress();
        StudentQualExm objSQualExam = new StudentQualExm();
        GEC_Student objStud = new GEC_Student();
        StudentPhoto objSPhoto = new StudentPhoto();
        UserAcc objUa = new UserAcc();
        int SchType = 0;



        if (rdoMale.Checked == false && rdoFemale.Checked == false && rdoFemale.Checked == false)
            {
            objCommon.DisplayMessage(this.Page, "Please Select Gender!", this.Page);
            return;
            }

        //if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
        //{
        //    if (ddladmthrough.SelectedIndex == 0)
        //    {
        //        objCommon.DisplayMessage(this.Page, "Please select Admission Through!", this.Page);
        //        return;
        //    }
        //}


        ////********** End Change by Rahul Moraskar 2022-07-26

        if (ddlSpecialisation.SelectedIndex > 0)
            {
            objS.Specialization = "1";
            objS.BranchNo = Convert.ToInt32(ddlSpecialisation.SelectedValue);
            }
        else
            {
            objS.Specialization = "0";
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            }

        DataSet dsstandardfees = objSC.GetStandardFeesDetails(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(objS.BranchNo), "TF", Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlPaymentType.SelectedValue), Convert.ToInt32(Session["OrgId"]));
        int Count = 520000;
        if (dsstandardfees.Tables[0].Rows.Count > 0)
            {
            Count = Convert.ToInt32(dsstandardfees.Tables[0].Rows[0]["COUNT"].ToString());
            }
        if (Count == 0)
            {
            objCommon.DisplayMessage(this.Page, "Standard Fees is Not Defined Please Define Standard Fees First.", this.Page);
            }
        else
            {
            DateTime dtmin = new DateTime(1753, 1, 1);
            DateTime dtmax = new DateTime(9999, 12, 31);
            if (!string.IsNullOrEmpty(txtDateOfBirth.Text.Trim()))
                {
                if ((DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmin) <= 0) ||
                    (DateTime.Compare(Convert.ToDateTime(txtDateOfBirth.Text.Trim()), dtmax) >= 0))
                    {
                    //1/1/1753 12:00:00 AM and 12/31/9999
                    objCommon.DisplayMessage(this.updStudent, "Please Enter Valid Date!!", this.Page);
                    return;
                    }
                }

            int lmobile = (txtStudMobile.Text).Length;

            if (lmobile < 10 && lmobile != 0)
                {
                objCommon.DisplayUserMessage(updStudent, "Please enter 10 digit mobile Number", this.Page);
                return;
                }


            int Pmobile = (txtParentmobno.Text).Length;

            if (Pmobile < 10 && Pmobile != 0)
                {
                objCommon.DisplayUserMessage(updStudent, "Please enter 10 digit Parent mobile Number", this.Page);
                return;
                }

            string exist1 = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "EMAILID='" + txtStudEmail.Text.Trim().ToString() + "'");
            string exist2 = objCommon.LookUp("ACD_STUDENT", "COUNT(1)", "STUDENTMOBILE='" + txtStudMobile.Text.Trim().ToString() + "'");

            if (Convert.ToInt32(exist1) > 0)
                {
                objCommon.DisplayMessage(this.updStudent, "Student record already saved with entered Email id or mobile no.", this.Page);
                txtStudEmail.Text = string.Empty;
                txtStudEmail.Focus();
                return;
                }

            if (Convert.ToInt32(exist2) > 0)
                {
                objCommon.DisplayMessage(this.updStudent, "Student record already saved with entered Email id or mobile no.", this.Page);
                txtStudMobile.Text = string.Empty;
                txtStudMobile.Focus();
                return;
                }

            if (ddlBranch.SelectedValue == "0")
                {
                objCommon.DisplayMessage(this.updStudent, "Please select branch!", this.Page);
                return;
                }

            if (!txtStudentfullName.Text.Trim().Equals(string.Empty))
                objS.StudName = txtStudentfullName.Text.Trim();
            if (!txtStudentMiddleName.Text.Trim().Equals(string.Empty))
                objS.MiddleName = txtStudentMiddleName.Text.Trim();
            if (!txtStudentLastName.Text.Trim().Equals(string.Empty))
                objS.LastName = txtStudentLastName.Text.Trim();
            if (!txtFatherName.Text.Trim().Equals(string.Empty))
                objS.FatherName = txtFatherName.Text.Trim();
            if (!txtFatherMiddleName.Text.Trim().Equals(string.Empty))
                objS.FatherMiddleName = txtFatherMiddleName.Text.Trim();
            if (!txtFatherLastName.Text.Trim().Equals(string.Empty))
                objS.FatherLastName = txtFatherLastName.Text.Trim();
            if (!txtMotherName.Text.Trim().Equals(string.Empty))
                objS.MotherName = txtMotherName.Text.Trim();
            if (!txtDateOfBirth.Text.Trim().Equals(string.Empty))
                objS.Dob = Convert.ToDateTime(txtDateOfBirth.Text.Trim());
            if (!txtDateOfReporting.Text.Trim().Equals(string.Empty))
                objS.DOR = Convert.ToDateTime(txtDateOfReporting.Text.Trim());
            if (!txtStudMobile.Text.Trim().Equals(string.Empty))
                objS.StudentMobile = txtStudMobile.Text.Trim();
            if (!txtStudMobile2.Text.Trim().Equals(string.Empty))
                objS.StudMobileno2 = txtStudMobile2.Text.Trim();
            if (!txtParentmobno.Text.Trim().Equals(string.Empty))
                objS.FatherMobile = txtParentmobno.Text.Trim();
            if (!txtcetcomorederno.Text.Trim().Equals(string.Empty))
                objS.Cetorderno = txtcetcomorederno.Text.Trim();
            if (!txtcetcomdate.Text.Trim().Equals(string.Empty))
                objS.Cetdate = txtcetcomdate.Text.Trim();
            if (!txtfeepaid.Text.Trim().Equals(string.Empty))
                objS.Cetamount = Convert.ToDecimal(txtfeepaid.Text.Trim());
            //      if (!txtPer.Text.Trim().Equals(string.Empty)) objS.Percentage = Convert.ToDecimal(txtPer.Text.Trim());
            objS.CollegeJss = ddlcolcode.SelectedItem.Text;
            if (!txtapplicationid.Text.Trim().Equals(string.Empty))
                objS.ApplicationID = txtapplicationid.Text.Trim();
            if (!txtmerirtno.Text.Trim().Equals(string.Empty))
                objS.MeritNo = txtmerirtno.Text.Trim();
            if (!txtscore.Text.Trim().Equals(string.Empty))
                objS.Score = Convert.ToDecimal(txtscore.Text.Trim());

            if (rdoMale.Checked)
                objS.Sex = 'M';
            else if (rdoFemale.Checked)
                objS.Sex = 'F';
            else if (rdoTransGender.Checked)
                objS.Sex = 'T';
            else
                objCommon.DisplayMessage(this.updStudent, "Please Select Gender.", this.Page);

            if (rdoMarriedNo.Checked)
                objS.Married = 'N';
            else
                objS.Married = 'Y';

            if (!txtPermanentAddress.Text.Trim().Equals(string.Empty))
                objSAddress.PADDRESS = txtPermanentAddress.Text.Trim();
            if (!txtMobileNo.Text.Trim().Equals(string.Empty))
                objSAddress.PMOBILE = txtMobileNo.Text.Trim();

            if (!txtPIN.Text.Trim().Equals(string.Empty))
                objSAddress.PPINCODE = txtPIN.Text.Trim();
            if (!txtContactNumber.Text.Trim().Equals(string.Empty))
                objSAddress.PTELEPHONE = txtContactNumber.Text.Trim();
            if (!txtDateOfReporting.Text.Trim().Equals(string.Empty))
                objS.AdmDate = Convert.ToDateTime(txtDateOfReporting.Text.Trim());

            objS.College_ID = Convert.ToInt32(ddlSchool.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);

            objS.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
            //objS.AdmBatch = Convert.ToInt32(Session["Admbatch"]);
            objS.PType = Convert.ToInt32(ddlPaymentType.SelectedValue);
            objS.ExamPtype = Convert.ToInt32(ddlPaymentType.SelectedValue); // only for gec project


            int HostelType = 0;

            if (Session["OrgId"].ToString().Equals("5"))
                {
                if (rdoHosteler.SelectedValue == "1")
                    {


                    objS.HostelSts = 1;
                    HostelType = Convert.ToInt32(ddlHostel.SelectedValue.ToString());
                    }

                }
            else
                {
                if (rdoHosteler.SelectedValue == "1")
                    {
                    objS.HostelSts = 1;
                    }
                else
                    {
                    objS.HostelSts = 0;
                    }
                }
            if (rdbTransport.SelectedValue == "1")
                {
                objS.Transportation = 1;
                }
            else
                {
                objS.Transportation = 0;
                // HostelType = 0;
                }


            if (rdoInstallment.SelectedValue == "1")
                {
                objS.Installment = 1;
                }
            else
                {
                objS.Installment = 0;
                }

            if (rdoscholarship.SelectedValue == "1")
                {
                objS.Scholorship = 1;
                if (txtschAmt.Text == string.Empty)
                    {
                    objS.SchAmtOrPercentage = 0;
                    }
                else
                    {
                    objS.SchAmtOrPercentage = Convert.ToDouble(txtschAmt.Text);
                    }
                objS.SchMode = Convert.ToInt32(ddlSchMode.SelectedValue);
                SchType = Convert.ToInt32(ddlSchType.SelectedValue);
                }
            else
                {
                objS.Scholorship = 0;
                }

            objS.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objS.CountryDomicile = ddlstate.SelectedValue;
            objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objS.CollegeCode = Session["colcode"].ToString();
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = ViewState["ipAddress"].ToString();
            objS.IdType = Convert.ToInt32(ddlAdmType.SelectedValue);



            if (fuPhotoUpload.HasFile)
                {
                objSPhoto.Photo1 = this.ResizePhoto(fuPhotoUpload);
                }
            else
                {
                objSPhoto.Photo1 = null;
                }

            if (fuSignUpload.HasFile)
                {
                objSPhoto.SignPhoto = this.ResizePhotoSign(fuSignUpload);
                }
            else
                {
                objSPhoto.SignPhoto = null;
                }

            objS.PH = ddlPhyHandicap.SelectedValue;
            objS.ReligionNo = Convert.ToInt32(ddlReligion.SelectedValue);
            objS.NationalityNo = Convert.ToInt32(ddlNationality.SelectedValue);

            objSAddress.PCITY = Convert.ToInt32(objCommon.GetIDNo(txtCity));
            if (objSAddress.PCITY == 0 && txtCity.Text.Trim() != string.Empty)
                objSAddress.PCITY = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtCity.Text.Trim().ToUpper(), 0);
            if (objSAddress.PCITY == -99)
                objSAddress.PCITY = 0;


            objS.CategoryNo = Convert.ToInt32(ddlAllotedCat.SelectedValue);

            objSAddress.PSTATE = Convert.ToInt32(objCommon.GetIDNo(txtState));
            if (objSAddress.PSTATE == 0 && txtState.Text.Trim() != string.Empty)
                objSAddress.PSTATE = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtState.Text.Trim().ToUpper(), 0);
            if (objSAddress.PSTATE == -99)
                objSAddress.PSTATE = 0;

            objS.StateNo = Convert.ToInt32(ddlstate.SelectedValue);
            objS.City = Convert.ToInt32(ddlCity.SelectedValue);
            objS.ClaimType = Convert.ToInt32(ddlClaimedCat.SelectedValue);
            objS.AdmCategoryNo = Convert.ToInt32(ddlAllotedCat.SelectedValue);
            objS.BloodGroupNo = Convert.ToInt32(ddlBloodGrp.SelectedValue);
            objS.AdmroundNo = Convert.ToInt32(ddladmthrough.SelectedValue);


            //ENTRANCE EXAM DETAILS..
            if (divotherentrance.Visible)
                {
                objS.QUALIFYNO = txtothetentrance.Text.ToString();
                }
            else
                {
                objS.QUALIFYNO = ddlExamNo.SelectedValue.Trim();
                }
            if (!txtJeeRankNo.Text.Trim().Equals(string.Empty))
                objS.ALLINDIARANK = Convert.ToInt32(txtJeeRankNo.Text.Trim());
            if (!txtJeeRollNo.Text.Trim().Equals(string.Empty))
                objS.QexmRollNo = txtJeeRollNo.Text.Trim();
            objS.Qualifyexamname = Convert.ToString(ddlExamNo.SelectedItem.Text.ToString().Trim());

            if (!txtYearOfExam.Text.Trim().Equals(string.Empty))
                objS.YearOfExam = txtYearOfExam.Text.Trim();


            if (!txtPer.Text.Trim().Equals(string.Empty))
                objS.Percentage = Convert.ToDecimal(txtPer.Text.Trim());
            if (!txtPercentile.Text.Trim().Equals(string.Empty))
                objS.PERCENTILE = Convert.ToDecimal(txtPercentile.Text.Trim());


            //ADD THE CODE FOR ONLY M.TECH SPOT ADMISSION
            if (ddlExamNo.SelectedValue == "9")
                {
                objS.GetScholarship = Convert.ToInt32(ddlSpotOption.SelectedValue);
                }
            else
                {
                objS.GetScholarship = 0;
                }

            //ADD THE CONTAIN TO RELATED LOCAL ADDRESS
            if (!txtStudEmail.Text.Trim().Equals(string.Empty))
                objS.EmailID = txtStudEmail.Text.Trim();
            if (!txtPostalAddress.Text.Trim().Equals(string.Empty))
                objSAddress.LADDRESS = txtPostalAddress.Text.Trim();
            if (!txtGuardianPhone.Text.Trim().Equals(string.Empty))
                objSAddress.LTELEPHONE = txtGuardianPhone.Text.Trim();
            if (!txtGuardianMobile.Text.Trim().Equals(string.Empty))
                objSAddress.LMOBILE = txtGuardianMobile.Text.Trim();
            if (!txtGuardianEmail.Text.Trim().Equals(string.Empty))
                objSAddress.LEMAIL = txtGuardianEmail.Text.Trim();

            objS.ADMQUOTANO = Convert.ToInt32(ddlQuota.SelectedValue);

            objSAddress.LCITY = Convert.ToInt32(objCommon.GetIDNo(txtLocalCity));
            if (objSAddress.LCITY == 0 && txtLocalCity.Text.Trim() != string.Empty)
                objSAddress.LCITY = objCommon.AddMasterTableData("ACD_CITY", "CITYNO", "CITY", txtLocalCity.Text.Trim().ToUpper(), 0);
            if (objSAddress.LCITY == -99)
                objSAddress.LCITY = 0;

            objSAddress.LSTATE = Convert.ToInt32(objCommon.GetIDNo(txtLocalState));
            if (objSAddress.LSTATE == 0 && txtLocalState.Text.Trim() != string.Empty)
                objSAddress.LSTATE = objCommon.AddMasterTableData("ACD_STATE", "STATENO", "STATENAME", txtLocalState.Text.Trim().ToUpper(), 0);
            if (objSAddress.LSTATE == -99)
                objSAddress.LSTATE = 0;
            if (!txtIUEmail.Text.Trim().Equals(string.Empty))
                IUEmail = txtIUEmail.Text.Trim();

            objS.SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
            objS.ApplicationID = txtREGNo.Text;

            if (txtAadhaarNo.Text != string.Empty)
                {
                int ladhar = (txtAadhaarNo.Text).Length;

                if (ladhar < 12 && ladhar != 0)
                    {
                    objCommon.DisplayUserMessage(updStudent, "Please Enter 12 digit Aadhar No.", this.Page);
                    return;
                    }
                }

            objS.AddharcardNo = (txtAadhaarNo.Text.Trim() != string.Empty) ? txtAadhaarNo.Text.Trim().ToString() : string.Empty; //Added by Irfan Shaikh on 09/04/2019
            ///---------------------------////
            ///Generate OTP as a Password////
            // ViewState["Otp"] = GeneratePassword();
            string pwd = string.Empty;

            pwd = GeneratePassword();

            if (pwd != null)
                {
                objUa.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                //objUa.UA_Pwd = Common.EncryptPassword(ViewState["Otp"].ToString());
                }
            int USERNO = 0;
            int ExistCount = 0;
            if (Convert.ToInt32(Session["OrgId"]) != 1 || Convert.ToInt32(Session["OrgId"]) != 2 || Convert.ToInt32(Session["OrgId"]) != 6)
                {
                USERNO = Convert.ToInt32(Session["USERNO_OA"]);

                ExistCount = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION UR INNER JOIN ACD_STUDENT S ON (UR.USERNO=S.USERNO)", "COUNT(S.USERNO)", "S.USERNO=" + USERNO));

                }
            else
                {
                USERNO = 0;
                }
            if (Convert.ToInt32(Session["OrgId"]) != 1 || Convert.ToInt32(Session["OrgId"]) != 2 || Convert.ToInt32(Session["OrgId"]) != 6)
                {
                //if (txtREGNo.Text != string.Empty)
                //{
                if (ExistCount > 0)
                    {
                    objCommon.DisplayMessage(this.Page, "Admission is Already done for Entered Application ID", this.Page);
                    return;
                    }
                //}
                }

            //int Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            //int Admbatch = Convert.ToInt32(ddlBatch.SelectedValue);


            string output = objSC.AddNewStudent_JECRC(objS, objStud, IUEmail, objUa, USERNO, HostelType, SchType);
            if (output != "-99")
                {
                if (rdoInstallment.SelectedValue == "1")
                    {
                    int InstallmentNO = Convert.ToInt32(ddlinstallmenttype.SelectedValue);
                    objS.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0"));
                    objS.IdNo = Convert.ToInt32(output);

                    string Installmenttype = string.Empty;
                    Installmenttype = objCommon.LookUp("ACD_INSTALLMENT_MASTER", "INSTALLMENT_TYPE", "INSTALLMENT_NO=" + Convert.ToInt32(ddlinstallmenttype.SelectedValue));
                    Installmenttype = Installmenttype + '-';
                    int count = Convert.ToInt32(objCommon.LookUp("dbo.split('" + Installmenttype + "','%-')", "count(id)-1", ""));
                    string Date_string = string.Empty;

                    if (count == 2)
                        {
                        Date_string = txtduedate1.Text.Trim() + ',' + txtduedate2.Text.Trim();
                        }
                    if (count == 3)
                        {
                        Date_string = txtduedate1.Text.Trim() + ',' + txtduedate2.Text.Trim() + ',' + txtduedate3.Text.Trim();
                        }
                    if (count == 4)
                        {
                        Date_string = txtduedate1.Text.Trim() + ',' + txtduedate2.Text.Trim() + ',' + txtduedate3.Text.Trim() + ',' + txtduedate4.Text.Trim();
                        }
                    if (count == 5)
                        {

                        Date_string = txtduedate1.Text.Trim() + ',' + txtduedate2.Text.Trim() + ',' + txtduedate3.Text.Trim() + ',' + txtduedate4.Text.Trim() + ',' + txtduedate5.Text.Trim();
                        }



                    string ret = objSC.AddInstallmentDetails_JECRC(objS, "TF", InstallmentNO, Date_string);
                    }
                GEC_Student objGecStud = new GEC_Student();
                txtREGNo.Text = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + output);
                Session["Regno"] = txtREGNo.Text.ToString();
                string srnno = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + output);
                string RECEIPTCODE = "TF";
                string Semester = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + output);


                string demandTamount = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)TOTAL_AMT", "IDNO=" + output + " AND SEMESTERNO=" + Semester + "AND RECIEPT_CODE= 'TF'");

                Session["IDNO"] = output;
                Session["DemandAmount"] = demandTamount;
                objCommon.DisplayMessage(updStudent, "Student Data Saved And Demand Created for Amount  " + demandTamount + "  Successfully.Your Temporary ID No is " + output + "" + " Now you can print Admission Slip.", this.Page);

                Session["Enrollno"] = srnno;
                Session["output"] = output;
                objGecStud.IdNo = Convert.ToInt32(output);
                objGecStud.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                objGecStud.AdmBatch = Convert.ToInt32(ddlBatch.SelectedValue);
                objGecStud.CollegeCode = Session["colcode"].ToString();
                objCommon.FillDropDownList(ddlExamNo, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO >0 AND QEXAMSTATUS='E'", "QUALIFYNO");

                collegeCode = objCommon.LookUp("REFF", "CODE_STANDARD", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
                ViewState["COLCODE"] = collegeCode;
                string linkName = string.Empty;

                string email_type = string.Empty;
                string Link = string.Empty;
                int sendmail = 0;
                DataSet ds = getModuleConfig();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                    email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                    Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                    sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["NEW_STUD_EMAIL_SEND"].ToString());
                    }

                if (sendmail == 1)
                    {
                    string message = "Thanks for showing interest in " + collegeCode + ". Your Account has been created successfully! Login with Username : " + srnno + " and Password : " + pwd + " Follow this link for further process " + Link + "";
                    string subject = "MIS Login Credentials";

                    //------------Code for sending email,It is optional---------------                       

                    if (email_type == "1" && email_type != "")
                        {
                        int reg = TransferToEmail(objS.EmailID, message, subject);
                        }
                    else if (email_type == "2" && email_type != "")
                        {
                        Task<int> task = Execute(message, objS.EmailID, subject);
                        }
                    if (email_type == "3" && email_type != "")
                        {
                        OutLook_Email(message, objS.EmailID, subject);
                        }
                    }

                string USERCREATIONCONFIG = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(NEW_STUD_USER_CREATION,0) NEW_STUD_USER_CREATION", "OrganizationId='" + Session["OrgId"].ToString() + "'");

                string PARENTUSERCREATIONCONFIG = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(CREATE_PARENT_USER_NEWSTUD,0) NEW_STUD_USER_CREATION", "OrganizationId='" + Session["OrgId"].ToString() + "'");

                if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                    CreateUserRCPITPIPER(srnno, pwd, objS.EmailID, objS.StudName);
                    }
                else
                    {
                    string StudName = txtStudentfullName.Text.Trim();
                    string REGNO = Session["Regno"].ToString();

                    string EMAIL = txtStudEmail.Text.Trim();
                    if (USERCREATIONCONFIG == "1")
                        {
                        CreateUser(StudName, REGNO, EMAIL);
                        }
                    if (PARENTUSERCREATIONCONFIG == "1")
                        {
                        CreateUserPARENT(txtParentmobno.Text,"", txtStudEmail.Text, objS.StudName);
                        }
                    }

                DisableControlsRecursive(Page);
                string App_id = objCommon.LookUp("ACD_STUDENT", "APPLICATIONID", "IDNO=" + output);
                ViewState["stuinfoidno"] = App_id;

                if (Session["OrgId"].ToString().Equals("1") || Session["OrgId"].ToString().Equals("6"))
                    {
                    this.ShowReport("Admission_Slip_Report", "rptStudAdmSlip_New.rpt", output);
                    }

                if (Session["OrgId"].ToString().Equals("1") || Session["OrgId"].ToString().Equals("6"))
                    {
                    ClearAllFields();
                    }
                //Response.Redirect(Request.Url.ToString());
                }
            }
        }
    protected void CreateUserPARENT(string USERNAME, string PASSWORD, string EMAIL, string student_Name)
        {
        try
            {
            string UA_PWD = string.Empty;
            string UA_ACC = string.Empty;
            string password = string.Empty;

            string PARENT_USERNAME = string.Empty;
            string PARENT_PASS = string.Empty;
            PARENT_USERNAME = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + Session["output"]);
            PARENT_PASS = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + Session["output"]);

            int IDNO = 0;

            IDNO = Convert.ToInt32(Session["IDNO"]);
            UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(PASSWORD.ToString());
            PARENT_PASS = clsTripleLvlEncyrpt.ThreeLevelEncrypt(PARENT_PASS.ToString());
            UA_ACC = "0,500,76";
            IDNO = Convert.ToInt32(Session["IDNO"]);
            CreateUser_PARENT(USERNAME, UA_PWD, student_Name, EMAIL, UA_ACC, IDNO, PARENT_USERNAME, PARENT_PASS);
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    public int CreateUser_PARENT(string UA_NAME, string UA_PWD, string UA_FULLNAME, string UA_EMAIL, string UA_ACC, int IDNO, string PARENT_USERNAME, string PARENT_PASS)
        {
        int retStatus = 0;
        try
            {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[9];
            objParams[0] = new SqlParameter("@P_UA_NAME", UA_NAME);
            objParams[1] = new SqlParameter("@P_UA_PWD", UA_PWD);
            objParams[2] = new SqlParameter("@P_UA_FULLNAME", UA_FULLNAME);
            objParams[3] = new SqlParameter("@P_UA_EMAIL", UA_EMAIL);
            objParams[4] = new SqlParameter("@P_UA_ACC", UA_ACC);
            objParams[5] = new SqlParameter("@P_IDNO", IDNO);
            objParams[6] = new SqlParameter("@P_PARENT_USERNAME", PARENT_USERNAME);
            objParams[7] = new SqlParameter("@P_PARENT_PASS", PARENT_PASS);
            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[8].Direction = ParameterDirection.Output;
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_USER_CREATION_OA_PARENT_LOGIN", objParams, true);

            if (Convert.ToInt32(ret) == 1)
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            else if (Convert.ToInt32(ret) == 1234)
                {
                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
            else
                retStatus = Convert.ToInt32(CustomStatus.Error);
            }
        catch (Exception ex)
            {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
            }
        return retStatus;
        }


    }