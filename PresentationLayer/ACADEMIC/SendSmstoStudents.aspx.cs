//===============================================//
// MODULE NAME   : RFC ERP Portal (RFC Common Code)
// PAGE NAME     : Send Bulk Email and SMS
// CREATION DATE : 
// CREATED BY    :  
// Modified BY   : Jay Takalkhede
// Modified Date : 25-08-2023
<<<<<<< HEAD
// Version :- 1) RFC.Enhancement.Major.1 (25-08-2023 [Maher])
=======
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
//===============================================//
//----------------------------------------------------------------------------------------------------------------------------------------------
//-- Version                      Modified On        Modified By        Purpose
//-----------------------------------------------------------------------------------------------------------------------------------------------
//--  RFC.Enhancement.Major.1      25-08-2023       Jay Takalkhede      Add Parents Teacher Meeting In Maher Client (Tktno. 47531) [MAHER]
//-----------------------------------------------------------------------------------------------------------------------------------------------
//--  RFC.Enhancement.Major.2      15-02-2024       Jay Takalkhede     Add Parents Teacher Meeting In Maher Client (Tktno. 54075)[TGPCET] SMS  
//----------------------------------------------------------------------------------------------------------------------------------------------
//--  RFC.Enhancement.Major.3      12-04-2024       Jay Takalkhede     Add 	Fees not Paid for All Semester and Classes Commence From  SMS
//                                                                          (Tktno. 57393)[TGPCET]  
//----------------------------------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_SendSmstoStudents : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExamController excol = new ExamController();
    StudentController studinfo = new StudentController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentAttendanceController STT = new StudentAttendanceController();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    User_AccController objUC = new User_AccController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    //CONNECTIONSTRING
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region PAGE_LOAD
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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (Session["dec"].ToString() == "1" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8")
                {
                    //btnHODReport.Visible = true;
                    //branch.Visible = false;
                    faculty.Visible = false;
                    //btnSubjectwise.Enabled = false;
                }
                else
                {
                    faculty.Visible = true;
                    //btnSubjectwise.Enabled = true;
                }

                if (Session["usertype"].ToString() != "3")
                    dvFaculty.Visible = true;

                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                this.FillDropdown();
                HiddenItemSMS();
                btnSmsToParents.Enabled = false;
                btnSmsToStudent.Enabled = true;
                btnEmail.Enabled = false;
                //btnSndSms.Enabled = false;
                ViewState["FileName"] = null;
                fuAttachment.FileContent.Flush();
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            lblBalance.Text = SMSBalance();
            lblBalance.ForeColor = System.Drawing.Color.Red;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 27/12/2021
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 27/12/2021
            HiddenItemForPm();
            employeepanel.Visible = false;
            pnlstud.Visible = false;
            rdbEmailSms.SelectedValue = "-1";
            cancel();
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
                if (Convert.ToInt32(Session["OrgId"]) == 7)
                {
                    btnWhatsapp.Visible = true;
                    //btnWhatsapp.Enabled = true;
                    HiddenItem();
                    btnWhatsAppAtt.Visible = false;
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
        }
        divMsg2.InnerHtml = string.Empty;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }


    public void HiddenItem()
    {

        if (Convert.ToInt32(Session["OrgId"]) == 1 )
        {
            foreach (ListItem item in this.rdbFormat.Items)
            {
                if (item.Value == "4")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "block");
                }
            }
        }
        else
        {
            foreach (ListItem item in this.rdbFormat.Items)
            {
                if (item.Value == "4")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
    }

    public void HiddenItemForPm()
    {
        //RFC.Enhancement.Major.1 (25-08-2023 [Maher]) Add Parents Teacher Meeting In Maher Client (Tktno. 47531)
        if (Convert.ToInt32(Session["OrgId"]) == 2 || Convert.ToInt32(Session["OrgId"]) == 16)
        {
            foreach (ListItem item in this.rdbFormat.Items)
            {
                if (item.Value == "5")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "block");
                }
            }
        }
        else
        {
            foreach (ListItem item in this.rdbFormat.Items)
            {
                if (item.Value == "5")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
    }

    public void HiddenItemSMS()
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
        {

        }
        else if (Convert.ToInt32(Session["OrgId"]) == 21)
        {
            foreach (ListItem item in this.rdbEmailSms.Items)
            {
                if (item.Value == "1")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "block");
                }
            }
        }
        else
        {
            foreach (ListItem item in this.rdbEmailSms.Items)
            {
                if (item.Value == "1")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
    }
<<<<<<< HEAD
=======
    public void HiddenItemFeesNotPaid()
    {
        if (Convert.ToInt32(Session["OrgId"]) == 2 || Convert.ToInt32(Session["OrgId"]) == 21)
        {

        }
        else
        {
            foreach (ListItem item in this.rboStudent.Items)
            {
                if (item.Value == "4")
                {
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
    }
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

    public void HiddenItemParents()
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
        {

        }
        else
        {
            foreach (ListItem item in this.rboStudent.Items)
            {
                if (item.Value == "3")
                {
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }
    }

    public void HiddenItemSMSmark()
    {
        if (Session["OrgId"] == "2")
        {
            foreach (ListItem item in this.rdbEmailSms.Items)
            {
                if (item.Value == "3")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "block");
                }
            }
        }
        else
        {
            foreach (ListItem item in this.rdbEmailSms.Items)
            {
                if (item.Value == "3")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }


    }

    public void HiddenClasses_Commence_From()
    {
        if (Session["OrgId"] == "21")
        {
            foreach (ListItem item in this.rdbEmailSms.Items)
            {
                if (item.Value == "5")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "block");
                }
            }
        }
        else
        {
            foreach (ListItem item in this.rdbEmailSms.Items)
            {
                if (item.Value == "5")
                {
                    // item.Attributes.CssStyle.Add("visibility", "hidden");
                    // Or you can try to use
                    item.Attributes.CssStyle.Add("display", "none");
                }
            }
        }


    }

    protected void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1" && Session["usertype"].ToString() != "8")
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            else

                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");


            if (rdbEmplyeStud.SelectedValue == "1")
            {
                empPanel.Enabled = true;
                Studpanel.Enabled = false;
                txtsub.Text = "";
                txtMessage.Text = "";
                objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SUBDEPTNO");
                HiddenItemForPm();
                HiddenItem();
            }
            else if (rdbEmplyeStud.SelectedValue == "2")
            {
                txtsub.Text = "";
                txtMessage.Text = "";
                Studpanel.Enabled = true;
                empPanel.Enabled = false;
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                HiddenItemForPm();
                HiddenItem();
            }
            else if (rdbEmplyeStud.SelectedValue == "3")
            {
                txtsub.Text = "";
                txtMessage.Text = "";
                Studpanel.Enabled = true;
                empPanel.Enabled = false;
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                HiddenItemForPm();
                HiddenItem();
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
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }
    #endregion

    #region SendBulkEmail and SMS

    #region Bind Listview

    #region Bind Listview - Employee
    protected void btnShowEmploy_Click(object sender, EventArgs e)
    {

        trEmployee.Visible = true;
        lvEmployee.Visible = true;
        trStudent.Visible = false;
        if (ddlDepartment.SelectedValue != "0")
        {
            // DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "FNAME", "EMAILID,PHONENO,IDNO", "SUBDEPTNO=" + ddlDepartment.SelectedValue, "");
            DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_FULLNAME", "UA_EMAIL,UA_MOBILE,UA_NO", "UA_DEPTNO like '%" + ddlDepartment.SelectedValue + "%'", "");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvEmployee.DataSource = ds;
                lvEmployee.DataBind();
                employeepanel.Visible = true;
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents();
<<<<<<< HEAD
=======
                HiddenClasses_Commence_From();
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                lvEmployee.DataSource = null;
                employeepanel.Visible = false;
                lvEmployee.DataBind();
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents();
<<<<<<< HEAD
=======
                //Added By Jay T. On dated 11042024
                HiddenClasses_Commence_From();
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            }

        }
        else
        {
            lvEmployee.DataSource = null;
            lvEmployee.DataBind();
            HiddenItemSMS();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemParents();
<<<<<<< HEAD
=======
            //Added By Jay T. On dated 11042024
            HiddenClasses_Commence_From();
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
    }
    #endregion

<<<<<<< HEAD
=======
    #region Methods
    public DataSet GetAllFeesNotPaidStudent(string collegeid, string degree, string branch, int idno, string recieptcode)
    {
        DataSet ds = null;
        try
        {
            string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_COLLEGE_ID", collegeid);
            objParams[1] = new SqlParameter("@P_DEGREENO", degree);
            objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
            objParams[3] = new SqlParameter("@P_IDNO", idno);
            objParams[4] = new SqlParameter("@P_RECIEPT_CODE", recieptcode);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_OUTSTANDING_FORMAT_EXCEL_CRESCENT_EMAIL", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectWiseAttPer-> " + ex.ToString());
        }
        return ds;
    }

    public DataSet GetAllFeesNotPaidStudent_Parents(string collegeid, string degree, string branch, int idno, string recieptcode)
    {
        DataSet ds = null;
        try
        {
            string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_COLLEGE_ID", collegeid);
            objParams[1] = new SqlParameter("@P_DEGREENO", degree);
            objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
            objParams[3] = new SqlParameter("@P_IDNO", idno);
            objParams[4] = new SqlParameter("@P_RECIEPT_CODE", recieptcode);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_OUTSTANDING_FORMAT_EXCEL_CRESCENT_PARENTS", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectWiseAttPer-> " + ex.ToString());
        }
        return ds;
    }
    //Added By Sakshi M on 20012024
    public int INSERTBULKEMAILSMS_LOG(int userno, string Activity, string mobileno, int usertype, int idno, int MSGTYPE, string emailid, string ipaddress, int Org_id)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = null;

            objParams = new SqlParameter[10];



            objParams[0] = new SqlParameter("@P_USERNO", userno);
            objParams[1] = new SqlParameter("@P_ACTIVITY", Activity);
            objParams[2] = new SqlParameter("@P_MOBILENO", mobileno);
            objParams[3] = new SqlParameter("@P_USERTYPE", usertype);
            objParams[4] = new SqlParameter("@P_IDNO", idno);
            objParams[5] = new SqlParameter("@P_MSGTYPE", MSGTYPE);
            objParams[6] = new SqlParameter("@P_EMAIL_ID", emailid);
            objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
            objParams[8] = new SqlParameter("@P_ORG", Org_id);
            objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
            objParams[9].Direction = ParameterDirection.Output;

            object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EMAILSMS_LOG", objParams, true);
            if (obj != null)
            {
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            else
            {
                retStatus = 0;
            }
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.INSERTBULKEMAILSMS_LOG() --> " + ex.Message + " " + ex.StackTrace);
        }
        return retStatus;
    }
    #endregion

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
    #region Bind Listview- Student , Parents

    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        #region Bind Listview- Student , Parents  (Fees not Paid  )
        if (rboStudent.SelectedValue == "1")
        {
            DataSet ds = null;
            if (rdbEmplyeStud.SelectedValue == "2")
            {
                ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
            }
            else if (rdbEmplyeStud.SelectedValue == "3")
            {
                ds = objAttC.GetFeesNotPaidStudent_Parents(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PnlNotPaidStudent.Visible = true;
                lvnotpaid.DataSource = ds.Tables[0]; ;
                lvnotpaid.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents();
<<<<<<< HEAD
=======
                //Added By Jay T. On dated 11042024
                HiddenClasses_Commence_From();
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                PnlNotPaidStudent.Visible = false;
                lvnotpaid.DataSource = null;
                lvnotpaid.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents();
<<<<<<< HEAD
=======
                //Added By Jay T. On dated 11042024 
                HiddenClasses_Commence_From();
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            }
        }
        #endregion Bind Listview- Student , Parents  (Fees not Paid  )

        #region Bind Listview- Student , Parents  (Installment Wise dues not paid )
        else if (rboStudent.SelectedValue == "2")
        {
            DataSet ds = null;
            if (txtEndDate.Text != string.Empty && txtStartDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtEndDate.Text) <= Convert.ToDateTime(txtStartDate.Text))
                {
                    objCommon.DisplayMessage(this.updCollege, "To date should be greater than From date", this.Page);
                    HiddenItemSMS();
                    HiddenItemForPm();
                    HiddenItem();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {
                    #region Bind Listview- Student (Installment Wise dues not paid )
                    if (rdbEmplyeStud.SelectedValue == "2")
                    {
                        ds = objAttC.GetInstallmentNotpaidStusent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            pnlStudentInstallment.Visible = true;
                            lvPaidStudentInstallment.DataSource = ds.Tables[0];
                            lvPaidStudentInstallment.DataBind();
                            txtsub.Text = "";
                            txtMessage.Text = "";
                            HiddenItemSMS();
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                            pnlStudentInstallment.Visible = false;
                            lvPaidStudentInstallment.DataSource = null;
                            lvPaidStudentInstallment.DataBind();
                            txtsub.Text = "";
                            txtMessage.Text = "";
                            HiddenItemSMS();
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                    }
                 #endregion Bind Listview- Student (Installment Wise dues not paid )

                    #region Bind Listview- Parents  (Installment Wise dues not paid )

            else if (rdbEmplyeStud.SelectedValue == "3")
            {
                ds = objAttC.GetInstallmentNotpaidStusent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));


<<<<<<< HEAD
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    pnlStudentInstallment.Visible = true;
                    lvPaidStudentInstallment.DataSource = ds.Tables[1];
                    lvPaidStudentInstallment.DataBind();
                    txtsub.Text = "";
                    txtMessage.Text = "";
                    HiddenItemSMS();
                    HiddenItemForPm();
                    HiddenItem();
                    HiddenItemParents();
                }
                else
                {
                    objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                    pnlStudentInstallment.Visible = false;
                    lvPaidStudentInstallment.DataSource = null;
                    lvPaidStudentInstallment.DataBind();
                    txtsub.Text = "";
                    txtMessage.Text = "";
                    HiddenItemSMS();
                    HiddenItemForPm();
                    HiddenItem();
                    HiddenItemParents();
=======
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                        {
                            pnlStudentInstallment.Visible = true;
                            lvPaidStudentInstallment.DataSource = ds.Tables[1];
                            lvPaidStudentInstallment.DataBind();
                            txtsub.Text = "";
                            txtMessage.Text = "";
                            HiddenItemSMS();
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                            pnlStudentInstallment.Visible = false;
                            lvPaidStudentInstallment.DataSource = null;
                            lvPaidStudentInstallment.DataBind();
                            txtsub.Text = "";
                            txtMessage.Text = "";
                            HiddenItemSMS();
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
                        }
                    }
                    #endregion Bind Listview- Parents  (Installment Wise dues not paid )
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                }
            }
            #endregion Bind Listview- Parents  (Installment Wise dues not paid )
            }
           }
        }
        #endregion Bind Listview- Student , Parents  (Installment Wise dues not paid )

        #region Bind Listview- Student , Parents (Sem Promotion Admission Form )
        else if (rboStudent.SelectedValue == "3") // Added By jay T. on dated 27/07/2023
        {
            trStudent.Visible = true;
            lvStudent.Visible = true;
            trEmployee.Visible = false;
            pnlstud.Visible = true;
            if (ddlSchool.SelectedValue != "0")
            {
                DataSet ds = null;

                #region Bind Listview- Student (Sem Promotion Admission Form )
                if (rdbEmplyeStud.SelectedValue == "2")
                {
                    ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[1];
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        return;
                    }
                }
                #endregion Bind Listview- Student (Sem Promotion Admission Form )

                #region Bind Listview-Parents (Sem Promotion Admission Form )
                if (rdbEmplyeStud.SelectedValue == "3")
                {
                    ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[2];
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        return;
                    }
                }
                #endregion Bind Listview-Parents (Sem Promotion Admission Form )

            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
<<<<<<< HEAD
                HiddenItemParents();
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                return;
            }
        }
        #endregion Bind Listview- Student , Parents (Sem Promotion Admission Form )

<<<<<<< HEAD
=======
        #region Bind Listview- Student , Parents  (All Semester Fees not Paid  )

        else if (rboStudent.SelectedValue == "4")
        {
            DataSet ds = null;
            if (rdbEmplyeStud.SelectedValue == "2")
            {
                ds = GetAllFeesNotPaidStudent(ddlSchool.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), 0, ddlReceiptType.SelectedValue.ToString());
            }
            else if (rdbEmplyeStud.SelectedValue == "3")
            {
                ds = GetAllFeesNotPaidStudent_Parents(ddlSchool.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), 0, ddlReceiptType.SelectedValue.ToString());
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PnlAllNotPaidStudent.Visible = true;
                lvNotpaidAll.DataSource = ds.Tables[0]; ;
                lvNotpaidAll.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
                PnlAllNotPaidStudent.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                PnlAllNotPaidStudent.Visible = false;
                lvNotpaidAll.DataSource = null;
                lvNotpaidAll.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
                PnlAllNotPaidStudent.Visible = true;
            }
        }
        #endregion Bind Listview- Student , Parents  (All Semester Fees not Paid )

        #region Bind Listview- Student , Parents (Classes Commence From)
        else if (rboStudent.SelectedValue == "5")
        {
            trStudent.Visible = true;
            lvStudent.Visible = true;
            trEmployee.Visible = false;
            pnlstud.Visible = true;
            if (ddlSchool.SelectedValue != "0")
            {
                DataSet ds = null;

                #region Bind Listview- Student (Classes Commence From )
                if (rdbEmplyeStud.SelectedValue == "2")
                {
                    ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[1];
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
                        return;
                    }
                }
                #endregion Bind Listview- Student (Classes Commence From)

                #region Bind Listview-Parents (Classes Commence From)
                if (rdbEmplyeStud.SelectedValue == "3")
                {
                    ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[2];
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
                        return;
                    }
                }
                #endregion Bind Listview-Parents (Classes Commence From)

            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
                return;
            }
        }
        #endregion Bind Listview-Student,Parents(Classes Commence From)

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        #region Bind Listview- Student , Parents (Normal)
        else
        {
            trStudent.Visible = true;
            lvStudent.Visible = true;
            trEmployee.Visible = false;
            pnlstud.Visible = true;
            if (ddlSchool.SelectedValue != "0")
            {
                DataSet ds = null;

                #region Bind Listview- Student (Normal )
                if (rdbEmplyeStud.SelectedValue == "2")
                {
                    ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[1];
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        return;
                    }
                }
                #endregion Bind Listview- Student (Normal)

                #region Bind Listview-Parents (Normal)
                if (rdbEmplyeStud.SelectedValue == "3")
                {
                    ds = objAttC.GetFeesNotPaidStudent(Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                    {
                        lvStudent.DataSource = ds.Tables[2];
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        txtsub.Text = "";
                        txtMessage.Text = "";
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
<<<<<<< HEAD
                        HiddenItemParents();
=======
                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        return;
                    }
                }
                #endregion Bind Listview-Parents (Normal)

            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                txtsub.Text = "";
                txtMessage.Text = "";
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
<<<<<<< HEAD
                HiddenItemParents();
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                return;
            }
        }
        #endregion Bind Listview-Student,Parents(Normal)
<<<<<<< HEAD
=======

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
    }

    #endregion

    #endregion

    #region Send Email Service

    protected void btnSndMessg_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/TempDocument/");
        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        #region Save the File to the Directory (Folder).

        if (fuAttachment.HasFile)
        {
            HttpPostedFile file = fuAttachment.PostedFile;
            int fileSize = fuAttachment.PostedFile.ContentLength;
            string ext = System.IO.Path.GetExtension(fuAttachment.PostedFile.FileName);
            int KB = fileSize / 5120;
            if (ext == ".pdf")
            {
                if (KB <= 5120)
                {

                    //Save the File to the Directory (Folder).
                    ViewState["FileName"] = fuAttachment.FileName;


                    if (ViewState["FileName"] != string.Empty || ViewState["FileName"] != "")
                    {
                        string x = folderPath + Path.GetFileName(fuAttachment.FileName);
                        if (!File.Exists(x))
                        {
                            fuAttachment.SaveAs(folderPath + Path.GetFileName(fuAttachment.FileName));
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCollege, "File too Big, please select a file less than 5 mb!!", this.Page);
                    HiddenItemSMS();
                    HiddenItemForPm();
                    HiddenItem();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Please Upload file with .pdf format only.", this.Page);
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
<<<<<<< HEAD
                HiddenItemParents();
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                return;
            }
        }
        #endregion

        Session["result"] = "0";
        int status = 0;
        string email = string.Empty;

        #region EMAIL - Employee
        if (rdbEmplyeStud.SelectedValue == "1")
        {
            int count = 0;
            foreach (ListViewDataItem dataitem in lvEmployee.Items)
            {

                CheckBox cbRow = dataitem.FindControl("chkSelect") as CheckBox;
                if (cbRow.Checked == true)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(updCollege, "Please Select atleast one Employee!!", this.Page);
                HiddenItemSMS();
                HiddenItemForPm();
                HiddenItem();
<<<<<<< HEAD
                HiddenItemParents();
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                return;
            }
            else
            {
                string Subject = txtsub.Text;
                string message = txtMessage.Text;
                foreach (ListViewDataItem item in lvEmployee.Items)
                {
                    try
                    {
                        CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                        Label lblEmail = item.FindControl("lblEmail") as Label;

                        if (chek.Checked)
                        {
                            if (lblEmail.Text != string.Empty)
                            {
                                email = lblEmail.Text;
                                DataSet dsconfig = null;
                                //sendEmail(message, lblEmailid.Text, Subject);

                                dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();
                                string path = Server.MapPath("~/TempDocument/");

                                try
                                {
                                    int status1 = 0;
                                    string email_type = string.Empty;
                                    string Link = string.Empty;
                                    int sendmail = 0;
                                    string filename = Convert.ToString(ViewState["FileName"]);
                                    string Imgfile = string.Empty;
                                    Byte[] Imgbytes = null;
                                    if (filename != string.Empty)
                                    {
                                        path = path + filename;
                                        string LogoPath = path;
                                        Imgbytes = File.ReadAllBytes(LogoPath);
                                        Imgfile = Convert.ToBase64String(Imgbytes);
                                    }
                                    if (filename == string.Empty)
                                    {
                                        status1 = objSendEmail.SendEmail(email, message, Subject); //Calling Method
                                    }
                                    else
                                    {
                                        status1 = objSendEmail.SendEmail(email, message, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                    }

                                    if (status1 == 1)
                                    {
                                        objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                        HiddenItemSMS();
                                        HiddenItemForPm();
                                        HiddenItem();
<<<<<<< HEAD
                                        HiddenItemParents();
=======
                                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                        //Added By Jay T. On dated 23022024
                                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                        cancel();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                        HiddenItemSMS();
                                        HiddenItemForPm();
                                        HiddenItem();
<<<<<<< HEAD
                                        HiddenItemParents();
<<<<<<< HEAD
=======
                                        //Added By Jay T. On dated 23022024
                                     HiddenItemFeesNotPaid();
>>>>>>> c665535e ([BUGFIX][53298][Maintain_Log_email])
=======
                                        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                        //Added By Jay T. On dated 23022024
                                        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }
                            }
                            else
                                objCommon.DisplayMessage("Sorry..! Dont find Email Id for some Employee", this.Page);
                            HiddenItemSMS();
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                    }

                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                //chek.Checked = false;
                cancel();
                ViewState["FileName"] = string.Empty;
            }
        }

        #endregion

        #region Email - Student
        else if (rdbEmplyeStud.SelectedValue == "2")
        {
            string Subject = txtsub.Text;
            string message = txtMessage.Text;
            //string SessionName = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
            DataSet dsconfig1 = objCommon.FillDropDown("REFF", "USER_PROFILE_SENDERNAME,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            string CollegeName = dsconfig1.Tables[0].Rows[0]["CollegeName"].ToString();
            string College = dsconfig1.Tables[0].Rows[0]["USER_PROFILE_SENDERNAME"].ToString();

            #region Email-Student (Fees not Paid)
            if (rboStudent.SelectedValue == "1")
            {
                int count3 = 0;
                foreach (ListViewDataItem dataitem in lvnotpaid.Items)
                {

                    CheckBox cbRow3 = dataitem.FindControl("chkSelect3") as CheckBox;
                    if (cbRow3.Checked == true)
                    {
                        count3++;
                    }
                }

                if (count3 == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
                    HiddenItemForPm();
                    HiddenItem();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {
                    foreach (ListViewDataItem items in lvnotpaid.Items)
                    {
                        try
                        {
                            CheckBox chek3 = items.FindControl("chkSelect3") as CheckBox;
                            Label lblEmailid3 = items.FindControl("lblEmailid3") as Label;
                            Label lblPaid = items.FindControl("lblPaid") as Label;
                            Label lblNotpaid = items.FindControl("lblNotpaid") as Label;
                            Label lblstudname = items.FindControl("lblStudname") as Label;
                            string PAID = lblPaid.Text.TrimEnd();
                            string Total = lblNotpaid.Text.TrimEnd();
                            string studname = lblstudname.Text.TrimEnd();
                            Label lblOut = items.FindControl("lblOut") as Label;
                            string Outstanding = lblOut.Text.TrimEnd();

                            if (chek3.Checked)
                            {
                                if (lblEmailid3.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    string msg = "<h1>Greetings !!</h1>";
                                    msg += "Dear" + " " + "<b>" + studname + "," + "</b>";   //b
                                    msg += "<br />";
                                    msg += "<br />";
                                    msg += "<b>" + message + "</b>" + "<br/><br/>";
                                    msg += "<b>Total Amount:" + Total + "</b>" + "<br/>";//b
                                    msg += "<b>Paid Amount:" + PAID + "</b>" + "<br/>";//b
                                    msg += "<b>Outstanding Amount:" + Outstanding + "</b>" + "<br/>";//b
                                    msg += "This is an auto generated response to your email. Please do not reply to this mail.";
                                    msg += "<br /><br /><br /><br />Regards,<br />";   //bb
                                    msg += "" + CollegeName + "<br /><br />";   //bb
                                    try
                                    {
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid3.Text, msg, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid3.Text, msg, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            //return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                else
                                    objCommon.DisplayMessage("Sorry..! Dont find Email Id. for some Students", this.Page);
                                HiddenItemSMS();
                                HiddenItemForPm();
                                HiddenItem();
<<<<<<< HEAD
                                HiddenItemParents();
=======
                                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                //Added By Jay T. On dated 23022024
                                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    // chek.Checked = false;
                }
            }
            #endregion

            #region Email- Student (Installment Wise dues not paid)
            else if (rboStudent.SelectedValue == "2")
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvPaidStudentInstallment.Items)
                {

                    CheckBox cbRow2 = dataitem.FindControl("chkSelect2") as CheckBox;
                    if (cbRow2.Checked == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {
                    foreach (ListViewDataItem items in lvPaidStudentInstallment.Items)
                    {
                        try
                        {
                            CheckBox chek2 = items.FindControl("chkSelect2") as CheckBox;
                            Label lblEmailid1 = items.FindControl("lblEmailid1") as Label;
                            Label lblduedate = items.FindControl("lblduedate") as Label;
                            Label lblInstallmentno = items.FindControl("lblInstallmentno") as Label;
                            Label lblInstallmentamount = items.FindControl("lblInstallmentamount") as Label;
                            Label lblstudname = items.FindControl("lblStudname") as Label;
                            string duedate = lblduedate.Text.TrimEnd();
                            string Installmentno = lblInstallmentno.Text.TrimEnd();
                            string Installmentamount = lblInstallmentamount.Text.TrimEnd();
                            string studname = lblstudname.Text.TrimEnd();
                            if (chek2.Checked)
                            {
                                if (lblEmailid1.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    try
                                    {
                                        string msg = "<h1>Greetings !!</h1>";
                                        msg += "Dear" + " " + "<b>" + studname + "," + "</b>";   //b
                                        msg += "<br />";
                                        msg += "<br />";
                                        msg += "<b>" + message + "</b>" + "<br/><br/>";
                                        msg += "<b>Installment Due Date:" + duedate + "</b>" + "<br/>";//b
                                        msg += "<b>Installment No:" + Installmentno + "</b>" + "<br/>";//b
                                        msg += "<b>Installment Amount:" + Installmentamount + "</b>" + "<br/>";//b
                                        msg += "This is an auto generated response to your email. Please do not reply to this mail.";
                                        msg += "<br /><br /><br /><br />Regards,<br />";   //bb
                                        msg += "" + CollegeName + "<br /><br />";   //bb
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid1.Text, msg, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid1.Text, msg, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }

                                        // status1 = objSendEmail.SendEmail(lblEmailid1.Text, msg, Subject); //Calling Method

                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            //return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                else
                                    objCommon.DisplayMessage("Sorry..! Dont find Email Id. for some Students", this.Page);
                                HiddenItemSMS();
                                HiddenItemForPm();
                                HiddenItem();
                            }
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                }

            }
            #endregion

            #region   Email-Student (Sem Promotion Admission Form)
            else if (rboStudent.SelectedValue == "3")
            {
                objCommon.DisplayMessage(updCollege, "Email Service Not Available", this.Page);
                HiddenItemSMS();
<<<<<<< HEAD
                HiddenItemParents();
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                return;
            }
            #endregion

<<<<<<< HEAD
=======
            #region Email-Student (All Semester Fees not Paid)
            else if (rboStudent.SelectedValue == "4")
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvNotpaidAll.Items)
                {

                    CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
                    return;
                }
                else
                {
                    foreach (ListViewDataItem items in lvNotpaidAll.Items)
                    {
                        try
                        {
                            CheckBox chkSelect1 = items.FindControl("chkSelect1") as CheckBox;
                            int idno = Convert.ToInt32(chkSelect1.ToolTip.ToString());
                            Label lblEmailid = items.FindControl("lblEmailid") as Label;
                            Label lblstudname = items.FindControl("lblStudname") as Label;
                            string studname = lblstudname.Text.TrimEnd();

                            if (chkSelect1.Checked)
                            {
                                if (lblEmailid.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    DataSet dsDetails = GetAllFeesNotPaidStudent(ddlSchool.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), idno, ddlReceiptType.SelectedValue.ToString());
                                    DataTable dt = (DataTable)dsDetails.Tables[0];
                                    DataRow[] dr = dt.Select("IDNO='" + idno + "'");
                                    //string msg = "<h1>Greetings !!</h1>";
                                    string msg = "<b>" + message + "</b>" + "<br/>";
                                    msg += "<br />REGISTRAR <br />";   //bb
                                    msg += "<br />";
                                    //msg += "" + CollegeName + "<br /><br />";   //bb
                                    // Changes done by jay takalkhede on dated 03012023
                                    msg += "<table width='1000px' cellspacing='0' style='border: 1px solid #190404'><thead style='background-color: #d3cccc !important;color: #0e0e0e !important;'><tr style='border: 1px solid #190404;'><th style='border: 1px solid #190404;' scope='col'>Name</th><th style='border: 1px solid #190404;' scope='col'>RRNNo.</th>" +
                                                   "<th style='border: 1px solid #190404;' scope='col'>Semester</th><th style='border: 1px solid #190404;' scope='col'>Fee for semester</th>" + "<th style='border: 1px solid #190404;' scope='col'>Paid for Semester</th><th style='border: 1px solid #190404;' scope='col'>Due for Semester</th></tr></thead><tbody>";

                                    for (int j = 0; j < dr.Length; j++)
                                    {
                                        if ((dr[j][19].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>I</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][17].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][18].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][19].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][22].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>II</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][20].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][21].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][22].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][25].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>III</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][23].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][24].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][25].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][28].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>IV</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][26].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][27].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][28].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][31].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>V</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][29].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][30].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][31].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][34].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>VI</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][32].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][33].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][34].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][37].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>VII</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][35].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][36].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][37].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][40].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>VIII</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][38].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][39].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][40].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][43].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>IX</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][41].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][42].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][43].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][46].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>X</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][44].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][45].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][46].ToString() + "</td></tr>";
                                        }

                                    }
                                    try
                                    {
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, msg, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, msg, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024

                                            //Added By Sakshi M on 20012024 to maintain log 
                                            //CustomStatus cs = (CustomStatus)objAttC.INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Student (All Semester Fees not Paid)", "", Convert.ToInt32(Session["usertype"]), idno, 1, lblEmailid.Text, IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                                            CustomStatus cs = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Student (All Semester Fees not Paid)", "", Convert.ToInt32(Session["usertype"]), idno, 1, lblEmailid.Text, IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024

                                            //return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    cancel();
                    HiddenItemFeesNotPaid();
                }
            }
            #endregion

            #region SMS For TGPCET (Student) Classes Commence From (11/04/2024)

            else if (rboStudent.SelectedValue == "5")
            {

                objCommon.DisplayMessage(this.updDetained, "Email Template Not Found For Your Selection!", this.Page);
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
                HiddenItemParents();
                //Added By Jay T. On dated 11042024
                HiddenClasses_Commence_From();
                DivDate1.Visible = false;
                txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
                return;
            }

            #endregion

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            #region Email- Student (Normal)
            else
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvStudent.Items)
                {

                    CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {

                    foreach (ListViewDataItem items in lvStudent.Items)
                    {
                        try
                        {
                            CheckBox chek1 = items.FindControl("chkSelect1") as CheckBox;
                            Label lblEmailid = items.FindControl("lblEmailid") as Label;

                            if (chek1.Checked)
                            {
                                if (lblEmailid.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    try
                                    {
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, message, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, message, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }
                                        status1 = objSendEmail.SendEmail(lblEmailid.Text, message, Subject); //Calling Method
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            // cancel();
                                            // return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                else
                                    objCommon.DisplayMessage("Sorry..! Dont find Email Id. for some Students", this.Page);
                                HiddenItemSMS();
                            }
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    cancel();
                    ViewState["FileName"] = string.Empty;
                }
            }
            #endregion
        }
        #endregion

        #region Email - Parents
        else if (rdbEmplyeStud.SelectedValue == "3")
        {
            string Subject = txtsub.Text;
            string message = txtMessage.Text;
            //string SessionName = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
            DataSet dsconfig1 = objCommon.FillDropDown("REFF", "USER_PROFILE_SENDERNAME,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            string CollegeName = dsconfig1.Tables[0].Rows[0]["CollegeName"].ToString();
            string College = dsconfig1.Tables[0].Rows[0]["USER_PROFILE_SENDERNAME"].ToString();

            #region Email - Parents (Fees not Paid )
            if (rboStudent.SelectedValue == "1")
            {
                int count3 = 0;
                foreach (ListViewDataItem dataitem in lvnotpaid.Items)
                {

                    CheckBox cbRow3 = dataitem.FindControl("chkSelect3") as CheckBox;
                    if (cbRow3.Checked == true)
                    {
                        count3++;
                    }
                }

                if (count3 == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
                    HiddenItemForPm();
                    HiddenItem();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {
                    foreach (ListViewDataItem items in lvnotpaid.Items)
                    {
                        try
                        {
                            CheckBox chek3 = items.FindControl("chkSelect3") as CheckBox;
                            Label lblEmailid3 = items.FindControl("lblEmailid3") as Label;
                            Label lblPaid = items.FindControl("lblPaid") as Label;
                            Label lblNotpaid = items.FindControl("lblNotpaid") as Label;
                            Label lblstudname = items.FindControl("lblStudname") as Label;
                            string PAID = lblPaid.Text.TrimEnd();
                            string Total = lblNotpaid.Text.TrimEnd();
                            string studname = lblstudname.Text.TrimEnd();
                            Label lblOut = items.FindControl("lblOut") as Label;
                            string Outstanding = lblOut.Text.TrimEnd();

                            if (chek3.Checked)
                            {
                                if (lblEmailid3.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    string msg = "<h1>Greetings !!</h1>";
                                    msg += "Dear" + " " + "<b>" + studname + "," + "</b>";   //b
                                    msg += "<br />";
                                    msg += "<br />";
                                    msg += "<b>" + message + "</b>" + "<br/><br/>";
                                    msg += "<b>Total Amount:" + Total + "</b>" + "<br/>";//b
                                    msg += "<b>Paid Amount:" + PAID + "</b>" + "<br/>";//b
                                    msg += "<b>Outstanding Amount:" + Outstanding + "</b>" + "<br/>";//b
                                    msg += "This is an auto generated response to your email. Please do not reply to this mail.";
                                    msg += "<br /><br /><br /><br />Regards,<br />";   //bb
                                    msg += "" + CollegeName + "<br /><br />";   //bb
                                    try
                                    {
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;

                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid3.Text, msg, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid3.Text, msg, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();

                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            //return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                else
                                    objCommon.DisplayMessage("Sorry..! Dont find Email Id. for some Students", this.Page);
                                HiddenItemSMS();
                                HiddenItemForPm();
                                HiddenItem();
<<<<<<< HEAD
                                HiddenItemParents();
=======
                                //Added By Jay T. On dated 23022024
                                HiddenItemFeesNotPaid();
                                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    // chek.Checked = false;
                }
            }
            #endregion

            #region Email - Parents (Installment Wise dues not paid)
            else if (rboStudent.SelectedValue == "2")
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvPaidStudentInstallment.Items)
                {

                    CheckBox cbRow2 = dataitem.FindControl("chkSelect2") as CheckBox;
                    if (cbRow2.Checked == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {
                    foreach (ListViewDataItem items in lvPaidStudentInstallment.Items)
                    {
                        try
                        {
                            CheckBox chek2 = items.FindControl("chkSelect2") as CheckBox;
                            Label lblEmailid1 = items.FindControl("lblEmailid1") as Label;
                            Label lblduedate = items.FindControl("lblduedate") as Label;
                            Label lblInstallmentno = items.FindControl("lblInstallmentno") as Label;
                            Label lblInstallmentamount = items.FindControl("lblInstallmentamount") as Label;
                            Label lblstudname = items.FindControl("lblStudname") as Label;
                            string duedate = lblduedate.Text.TrimEnd();
                            string Installmentno = lblInstallmentno.Text.TrimEnd();
                            string Installmentamount = lblInstallmentamount.Text.TrimEnd();
                            string studname = lblstudname.Text.TrimEnd();
                            if (chek2.Checked)
                            {
                                if (lblEmailid1.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    try
                                    {
                                        string msg = "<h1>Greetings !!</h1>";
                                        msg += "Dear" + " " + "<b>" + studname + "," + "</b>";   //b
                                        msg += "<br />";
                                        msg += "<br />";
                                        msg += "<b>" + message + "</b>" + "<br/><br/>";
                                        msg += "<b>Installment Due Date:" + duedate + "</b>" + "<br/>";//b
                                        msg += "<b>Installment No:" + Installmentno + "</b>" + "<br/>";//b
                                        msg += "<b>Installment Amount:" + Installmentamount + "</b>" + "<br/>";//b
                                        msg += "This is an auto generated response to your email. Please do not reply to this mail.";
                                        msg += "<br /><br /><br /><br />Regards,<br />";   //bb
                                        msg += "" + CollegeName + "<br /><br />";   //bb
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid1.Text, msg, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid1.Text, msg, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }

                                        // status1 = objSendEmail.SendEmail(lblEmailid1.Text, msg, Subject); //Calling Method

                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            //return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                else
                                    objCommon.DisplayMessage("Sorry..! Dont find Email Id. for some Students", this.Page);
                                HiddenItemSMS();
                                HiddenItemForPm();
                                HiddenItem();
                            }
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                }
            }
            #endregion

            #region Email - Parents (Sem Promotion Admission Form)
            else if (rboStudent.SelectedValue == "3")
            {
                objCommon.DisplayMessage(updCollege, "Email Service Not Available", this.Page);
                HiddenItemSMS();
<<<<<<< HEAD
                HiddenItemParents();
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                return;
            }
            #endregion

<<<<<<< HEAD
=======
            #region Email-Student (All Semester Fees not Paid)
            else if (rboStudent.SelectedValue == "4")
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvNotpaidAll.Items)
                {

                    CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
                    return;
                }
                else
                {
                    foreach (ListViewDataItem items in lvNotpaidAll.Items)
                    {
                        try
                        {
                            CheckBox chkSelect1 = items.FindControl("chkSelect1") as CheckBox;
                            int idno = Convert.ToInt32(chkSelect1.ToolTip.ToString());
                            Label lblEmailid = items.FindControl("lblEmailid") as Label;
                            Label lblstudname = items.FindControl("lblStudname") as Label;
                            string studname = lblstudname.Text.TrimEnd();

                            if (chkSelect1.Checked)
                            {
                                if (lblEmailid.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    DataSet dsDetails = GetAllFeesNotPaidStudent_Parents(ddlSchool.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), idno, ddlReceiptType.SelectedValue.ToString());
                                    DataTable dt = (DataTable)dsDetails.Tables[0];
                                    DataRow[] dr = dt.Select("IDNO='" + idno + "'");
                                    //string msg = "<h1>Greetings !!</h1>";
                                    string msg = "<b>" + message + "</b>" + "<br/>";
                                    msg += "<br />REGISTRAR <br />";   //bb
                                    msg += "<br />";
                                    //msg += "" + CollegeName + "<br /><br />";   //bb
                                    // Changes done by jay takalkhede on dated 03012023
                                    msg += "<table width='1000px' cellspacing='0' style='border: 1px solid #190404'><thead style='background-color: #d3cccc !important;color: #0e0e0e !important;'><tr style='border: 1px solid #190404;'><th style='border: 1px solid #190404;' scope='col'>Name</th><th style='border: 1px solid #190404;' scope='col'>RRNNo.</th>" +
                                          "<th style='border: 1px solid #190404;' scope='col'>Semester</th><th style='border: 1px solid #190404;' scope='col'>Fee for semester</th>" + "<th style='border: 1px solid #190404;' scope='col'>Paid for Semester</th><th style='border: 1px solid #190404;' scope='col'>Due for Semester</th></tr></thead><tbody>";

                                    for (int j = 0; j < dr.Length; j++)
                                    {
                                        if ((dr[j][19].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>I</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][17].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][18].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][19].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][22].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>II</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][20].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][21].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][22].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][25].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>III</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][23].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][24].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][25].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][28].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>IV</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][26].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][27].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][28].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][31].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>V</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][29].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][30].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][31].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][34].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>VI</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][32].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][33].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][34].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][37].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>VII</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][35].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][36].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][37].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][40].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>VIII</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][38].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][39].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][40].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][43].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>IX</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][41].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][42].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][43].ToString() + "</td></tr>";
                                        }
                                        if ((dr[j][46].ToString()) != "0.00")
                                        {
                                            msg += "<tr style='border: 1px solid #190404;'><td style='border: 1px solid #190404;text-align:center;'> " + dr[j][5].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][3].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404; text-align:center;'>X</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][44].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][45].ToString() + "</td>";
                                            msg += "<td style='border: 1px solid #190404;text-align:center;'> " + dr[j][46].ToString() + "</td></tr>";
                                        }

                                    }

                                    try
                                    {
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, msg, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, msg, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            //HiddenItemFeesNotPaid();
                                            //cancel();
                                            //Added By Sakshi M on 20012024 to maintain log 
                                            //CustomStatus cs = (CustomStatus)objAttC.INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Student (All Semester Fees not Paid)", "", Convert.ToInt32(Session["usertype"]), idno, 1, lblEmailid.Text, IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                                            CustomStatus cs = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Student (All Semester Fees not Paid)", "", Convert.ToInt32(Session["usertype"]), idno, 1, lblEmailid.Text, IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            //HiddenItemFeesNotPaid();
                                            //return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    HiddenItemFeesNotPaid();
                    cancel();
                }
            }
            #endregion

            #region SMS For TGPCET (Student) Classes Commence From (11/04/2024)

            else if (rboStudent.SelectedValue == "5")
            {

                objCommon.DisplayMessage(this.updDetained, "Email Template Not Found For Your Selection!", this.Page);
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
                HiddenItemParents();
                //Added By Jay T. On dated 11042024
                HiddenClasses_Commence_From();
                DivDate1.Visible = false;
                txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
                return;
            }

            #endregion

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            #region Email - Parents (Normal)
            else
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvStudent.Items)
                {

                    CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                    if (cbRow.Checked == true)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayMessage(updCollege, "Please Select atleast one Student!!", this.Page);
                    HiddenItemSMS();
<<<<<<< HEAD
                    HiddenItemParents();
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    return;
                }
                else
                {

                    foreach (ListViewDataItem items in lvStudent.Items)
                    {
                        try
                        {
                            CheckBox chek1 = items.FindControl("chkSelect1") as CheckBox;
                            Label lblEmailid = items.FindControl("lblEmailid") as Label;

                            if (chek1.Checked)
                            {
                                if (lblEmailid.Text != string.Empty)
                                {
                                    DataSet dsconfig = null;

                                    dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                                    string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                                    string path = Server.MapPath("~/TempDocument/");
                                    try
                                    {
                                        int status1 = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        string filename = Convert.ToString(ViewState["FileName"]);
                                        string Imgfile = string.Empty;
                                        Byte[] Imgbytes = null;
                                        if (filename != string.Empty)
                                        {
                                            path = path + filename;
                                            string LogoPath = path;
                                            Imgbytes = File.ReadAllBytes(LogoPath);
                                            Imgfile = Convert.ToBase64String(Imgbytes);
                                        }
                                        if (filename == string.Empty)
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, message, Subject); //Calling Method
                                        }
                                        else
                                        {
                                            status1 = objSendEmail.SendEmail(lblEmailid.Text, message, Subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                                        }
                                        status1 = objSendEmail.SendEmail(lblEmailid.Text, message, Subject); //Calling Method
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(updCollege, "Mail Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send email", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                else
                                    objCommon.DisplayMessage("Sorry..! Dont find Email Id. for some Students", this.Page);
                                HiddenItemSMS();
                            }
                            HiddenItemForPm();
                            HiddenItem();
<<<<<<< HEAD
                            HiddenItemParents();
=======
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    cancel();
                    ViewState["FileName"] = string.Empty;
                }
            }
            #endregion
        }
        #endregion
    }

    #endregion  Send Email Service

    #region SMS
    //Added By JAY TAKALKHEDE 27/07/2023  For SMS
    protected void btnSndSms_Click(object sender, EventArgs e)
    {
        int MSGTYPE = 0;

        #region SMS For RCPIPER
        if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            #region SMS For RCPIPER (Student)

            if (rdbEmplyeStud.SelectedValue == "2")
            {
                #region SMS For RCPIPER (Student) Sem Promotion Admission Form (27/07/2023 )
                if (rboStudent.SelectedValue == "3")
                {
                    MSGTYPE = 1;
                    string TemplateID = string.Empty;
                    string TEMPLATE = string.Empty;
                    string message = string.Empty;
                    string template = string.Empty;
                    int status1 = 0;
                    int count = 0;
                    foreach (ListViewDataItem dataitem in lvStudent.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                        if (cbRow.Checked == true)
                            count++;
                    }
                    if (count <= 0)
                    {
                        objCommon.DisplayMessage(this.updCollege, "Please Select atleast one Student For Send SMS", this);
                        return;
                        HiddenItemSMS();
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemParents();
                        HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //Added By Jay T. On dated 23022024
                        HiddenItemFeesNotPaid();
                    }
                    else
                    {
                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            try
                            {
                                CheckBox chek = item.FindControl("chkSelect1") as CheckBox;
                                Label lblStudMobile = item.FindControl("lblStudmobile") as Label;

                                if (chek.Checked)
                                {
                                    string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "STUDENTMOBILE", "IDNO=" + chek.ToolTip);
                                    string mobile = "91" + ToMobileNo;
                                    if (ToMobileNo != string.Empty)
                                    {
                                        if (lblStudMobile.Text != string.Empty && lblStudMobile.Text.Length == 10)
                                        {
                                            string templatename = "Sem Promotion Admission Form";
                                            DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                                TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                                HiddenItemForPm();
                                                HiddenItem();
                                                HiddenItemSMS();
                                                return;
                                            }
                                            message = TEMPLATE;
                                            // Create a StringBuilder and append the template
                                            StringBuilder stringBuilder = new StringBuilder();
                                            stringBuilder.Append(message);
                                            // Get the final message string
                                            template = stringBuilder.ToString();
                                            //this.SendSMS(lblStudMobile.Text.Trim(), template, TemplateID);
                                            status1 = SendSMS_Admission(lblStudMobile.Text.Trim(), template, TemplateID);
                                        }
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(this.updCollege, "SMS Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send SMS", this.Page);
                                            HiddenItemSMS();
                                            HiddenItemParents();
<<<<<<< HEAD
=======
                                            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                        }

                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updCollege, "Sorry..! Didn't found Mobile no. for some Students(s)", this.Page);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (Convert.ToBoolean(Session["error"]) == true)
                                    objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSndSms_Click-> " + ex.Message + " " + ex.StackTrace);
                                else
                                {
                                    objCommon.DisplayMessage(this.updCollege, "Server UnAvailable", this.Page);
                                }
                            }
                        }
                    }


                }
                #endregion
                else
                {
                    objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                    HiddenItemForPm();
                    HiddenItem();
                    HiddenItemSMS();
                    HiddenItemParents();
                    //Added By Jay T. On dated 11042024
                    HiddenClasses_Commence_From();
                    return;
                }
            }
            #endregion

            #region SMS For RCPIPER (Parents)
            else if (rdbEmplyeStud.SelectedValue == "3")
            {
                #region SMS For RCPIPER (Parents) Sem Promotion Admission Form (27/07/2023 )
                if (rboStudent.SelectedValue == "3")
                {
                    MSGTYPE = 1;
                    string TemplateID = string.Empty;
                    string TEMPLATE = string.Empty;
                    string message = string.Empty;
                    string template = string.Empty;
                    int status1 = 0;
                    int count = 0;
                    foreach (ListViewDataItem dataitem in lvStudent.Items)
                    {
                        CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                        if (cbRow.Checked == true)
                            count++;
                    }
                    if (count <= 0)
                    {
                        objCommon.DisplayMessage(this.updCollege, "Please Select atleast one Student For Send SMS", this);
                        return;
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                    }
                    else
                    {
                        foreach (ListViewDataItem item in lvStudent.Items)
                        {
                            try
                            {
                                CheckBox chek = item.FindControl("chkSelect1") as CheckBox;
                                Label lblParentsMobile = item.FindControl("lblStudmobile") as Label;

                                if (chek.Checked)
                                {
                                    string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + chek.ToolTip);
                                    string mobile = "91" + ToMobileNo;
                                    if (ToMobileNo != string.Empty)
                                    {
                                        if (lblParentsMobile.Text != string.Empty && lblParentsMobile.Text.Length == 10)
                                        {
                                            string templatename = "Sem Promotion Admission Form";
                                            DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                                TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                                HiddenItemForPm();
                                                HiddenItem();
                                                HiddenItemSMS();
                                                return;
                                            }
                                            message = TEMPLATE;
                                            // Create a StringBuilder and append the template
                                            StringBuilder stringBuilder = new StringBuilder();
                                            stringBuilder.Append(message);
                                            // Get the final message string
                                            template = stringBuilder.ToString();
                                            //this.SendSMS(lblParentsMobile.Text.Trim(), template, TemplateID);
                                            status1 = SendSMS_Admission(lblParentsMobile.Text.Trim(), template, TemplateID);
                                        }
                                        if (status1 == 1)
                                        {
                                            objCommon.DisplayUserMessage(this.updCollege, "SMS Sent Successfully.", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                            cancel();
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Failed To send SMS", this.Page);
                                            HiddenItemSMS();
<<<<<<< HEAD
                                            HiddenItemParents();
=======
                                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                            //Added By Jay T. On dated 23022024
                                            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                        }

                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updCollege, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (Convert.ToBoolean(Session["error"]) == true)
                                    objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSndSms_Click-> " + ex.Message + " " + ex.StackTrace);
                                else
                                {
                                    objCommon.DisplayMessage(this.updCollege, "Server UnAvailable", this.Page);
                                }
                            }
                        }
                    }
                }
                #endregion
                else
                {
                    objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                    HiddenItemForPm();
                    HiddenItem();
                    HiddenItemSMS();
                    HiddenItemParents();
                    //Added By Jay T. On dated 11042024
                    HiddenClasses_Commence_From();
                    return;
                }
            }
            #endregion
            else
            {
                objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
                HiddenItemParents();
                //Added By Jay T. On dated 11042024
                HiddenClasses_Commence_From();
                return;

            }
        }
        #endregion

        #region SMS For TGPCET
        else if (Convert.ToInt32(Session["OrgId"]) == 21)
        {
            #region SMS For TGPCET (Student)
            if (rdbEmplyeStud.SelectedValue == "2")
            {
                #region SMS For TGPCET (Student) Fees not Paid for All Semester (11/04/2024)
                if (rboStudent.SelectedValue == "4")
                {
                    if (txtDate.Text != string.Empty)
                    {
                        if (Convert.ToDateTime(DateTime.Now) > Convert.ToDateTime(txtDate.Text))
                        {
                            objCommon.DisplayMessage(this.updCollege, "Date should be greater than Today's date", this.Page);
                            HiddenItemSMS();
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                            //Added By Jay T. On dated 23022024
                            HiddenItemFeesNotPaid();

                            return;
                        }
                        else
                        {

                            MSGTYPE = 1;
                            string TemplateID = string.Empty;
                            string TEMPLATE = string.Empty;
                            string message = string.Empty;
                            string template = string.Empty;
                            int status1 = 0;
                            int count = 0;
                            foreach (ListViewDataItem dataitem in lvNotpaidAll.Items)
                            {
                                CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                                if (cbRow.Checked == true)
                                    count++;
                            }
                            if (count <= 0)
                            {
                                objCommon.DisplayMessage(this.updCollege, "Please Select atleast one Student For Send SMS", this);
                                HiddenItemSMS();
                                HiddenItemForPm();
                                HiddenItem();
                                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                //Added By Jay T. On dated 23022024
                                HiddenItemFeesNotPaid();
                                return;
                            }
                            else
                            {
                                foreach (ListViewDataItem items in lvNotpaidAll.Items)
                                {
                                    try
                                    {
                                        CheckBox chkSelect1 = items.FindControl("chkSelect1") as CheckBox;
                                        int idno = Convert.ToInt32(chkSelect1.ToolTip.ToString());
                                        Label lblStudMobile = items.FindControl("lblStudmobile") as Label;
                                        Label lblstudname = items.FindControl("lblStudname") as Label;
                                        string studname = lblstudname.Text.TrimEnd();


                                        if (chkSelect1.Checked)
                                        {
                                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "STUDENTMOBILE", "IDNO=" + chkSelect1.ToolTip);
                                            string ACADEMIC_YEAR = objCommon.LookUp("acd_student S inner join ACD_ACADEMIC_YEAR Y on(S.ACADEMIC_YEAR_ID=Y.ACADEMIC_YEAR_ID)", "Y.ACADEMIC_YEAR_NAME", "IDNO=" + chkSelect1.ToolTip);
                                            string date = txtDate.Text;
                                            string mobile = "91" + ToMobileNo;
                                            if (ToMobileNo != string.Empty)
                                            {
                                                if (lblStudMobile.Text != string.Empty)
                                                {
                                                    string templatename = "Fees not Paid for All Semester";
                                                    DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                                    if (ds.Tables[0].Rows.Count > 0)
                                                    {
                                                        TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                                        TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                                    }
                                                    else
                                                    {
                                                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                                        HiddenItemForPm();
                                                        HiddenItem();
                                                        HiddenItemSMS();
                                                        return;
                                                    }
                                                    message = TEMPLATE;
                                                    message = message.Replace("{#var#}", ACADEMIC_YEAR);
                                                    message = message.Replace("{#var1#}", date);

                                                    // Create a StringBuilder and append the template
                                                    StringBuilder stringBuilder = new StringBuilder();
                                                    stringBuilder.Append(message);
                                                    // Get the final message string
                                                    template = stringBuilder.ToString();
                                                    //this.SendSMS(lblStudMobile.Text.Trim(), template, TemplateID);
                                                    status1 = SendSMS_Admission(lblStudMobile.Text.Trim(), template, TemplateID);
                                                }
                                            }
                                            if (status1 == 1)
                                            {
                                                objCommon.DisplayUserMessage(this.updCollege, "SMS Sent Successfully.", this.Page);
                                                HiddenItemSMS();
                                                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                                //Added By Jay T. On dated 23022024
                                                HiddenItemFeesNotPaid();
                                                cancel();
                                                //Added By Sakshi M on 20012024 to maintain log 
                                                CustomStatus cs = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Fees not Paid for All Semester(Student)", lblStudMobile.Text.Trim(), Convert.ToInt32(Session["usertype"]), idno, 2, "", IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(this.updCollege, "Failed To send SMS", this.Page);
                                                HiddenItemSMS();
                                                HiddenItemParents();
                                                HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                                //Added By Jay T. On dated 23022024
                                                HiddenItemFeesNotPaid();
                                            }

                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Sorry..! Didn't found Mobile no. for some Students(s)", this.Page);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        if (Convert.ToBoolean(Session["error"]) == true)
                                            objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSndSms_Click-> " + ex.Message + " " + ex.StackTrace);
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updCollege, "Server UnAvailable", this.Page);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "Please Select Date !", this.Page);
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        return;
                    }
                }
                #endregion

                #region SMS For TGPCET (Student) Classes Commence From (11/04/2024)
                else
                    if (rboStudent.SelectedValue == "5")
                    {
                        if (txtDate.Text != string.Empty)
                        {
                            if (Convert.ToDateTime(DateTime.Now) > Convert.ToDateTime(txtDate.Text))
                            {
                                objCommon.DisplayMessage(this.updCollege, "Date should be greater than Today's date", this.Page);
                                HiddenItemSMS();
                                HiddenItemForPm();
                                HiddenItem();
                                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                //Added By Jay T. On dated 23022024
                                HiddenItemFeesNotPaid();

                                return;
                            }
                            else
                            {

                                MSGTYPE = 1;
                                string TemplateID = string.Empty;
                                string TEMPLATE = string.Empty;
                                string message = string.Empty;
                                string template = string.Empty;
                                int status1 = 0;
                                int count = 0;
                                foreach (ListViewDataItem dataitem in lvStudent.Items)
                                {
                                    CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                                    if (cbRow.Checked == true)
                                        count++;
                                }
                                if (count <= 0)
                                {
                                    objCommon.DisplayMessage(this.updCollege, "Please Select atleast one Student For Send SMS", this);
                                    HiddenItemSMS();
                                    HiddenItemForPm();
                                    HiddenItem();
                                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                    //Added By Jay T. On dated 23022024
                                    HiddenItemFeesNotPaid();
                                    return;
                                }
                                else
                                {
                                    foreach (ListViewDataItem items in lvStudent.Items)
                                    {
                                        try
                                        {
                                            CheckBox chkSelect1 = items.FindControl("chkSelect1") as CheckBox;
                                            int idno = Convert.ToInt32(chkSelect1.ToolTip.ToString());
                                            Label lblStudMobile = items.FindControl("lblStudmobile") as Label;
                                            Label lblstudname = items.FindControl("lblStudname") as Label;
                                            string studname = lblstudname.Text.TrimEnd();


                                            if (chkSelect1.Checked)
                                            {
                                                string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "STUDENTMOBILE", "IDNO=" + chkSelect1.ToolTip);
                                                string Semester = objCommon.LookUp("acd_student S inner join acd_semester Y on(S.semesterno=Y.semesterno)", "Y.SEMESTERNAME", "IDNO=" + chkSelect1.ToolTip);
                                                string date = txtDate.Text;
                                                string mobile = "91" + ToMobileNo;
                                                if (ToMobileNo != string.Empty)
                                                {
                                                    if (lblStudMobile.Text != string.Empty)
                                                    {
                                                        string templatename = "Classes Commence From";
                                                        DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                                        if (ds.Tables[0].Rows.Count > 0)
                                                        {
                                                            TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                                            TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                                        }
                                                        else
                                                        {
                                                            objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                                            HiddenItemForPm();
                                                            HiddenItem();
                                                            HiddenItemSMS();
                                                            return;
                                                        }
                                                        message = TEMPLATE;
                                                        message = message.Replace("{#var#}", Semester);
                                                        message = message.Replace("{#var1#}", date);

                                                        // Create a StringBuilder and append the template
                                                        StringBuilder stringBuilder = new StringBuilder();
                                                        stringBuilder.Append(message);
                                                        // Get the final message string
                                                        template = stringBuilder.ToString();
                                                        //this.SendSMS(lblStudMobile.Text.Trim(), template, TemplateID);
                                                        status1 = SendSMS_Admission(lblStudMobile.Text.Trim(), template, TemplateID);
                                                    }
                                                }
                                                if (status1 == 1)
                                                {
                                                    objCommon.DisplayUserMessage(this.updCollege, "SMS Sent Successfully.", this.Page);
                                                    HiddenItemSMS();
                                                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                                    //Added By Jay T. On dated 23022024
                                                    HiddenItemFeesNotPaid();
                                                    //cancel();
                                                    //Added By Sakshi M on 20012024 to maintain log 
                                                    CustomStatus cs = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Classes Commence From(Student)", lblStudMobile.Text.Trim(), Convert.ToInt32(Session["usertype"]), idno, 2, "", IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                                                }
                                                else
                                                {
                                                    objCommon.DisplayMessage(this.updCollege, "Failed To send SMS", this.Page);
                                                    HiddenItemSMS();
                                                    HiddenItemParents();
                                                    HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                                    //Added By Jay T. On dated 23022024
                                                    HiddenItemFeesNotPaid();
                                                }

                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(this.updCollege, "Sorry..! Didn't found Mobile no. for some Students(s)", this.Page);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            if (Convert.ToBoolean(Session["error"]) == true)
                                                objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSndSms_Click-> " + ex.Message + " " + ex.StackTrace);
                                            else
                                            {
                                                objCommon.DisplayMessage(this.updCollege, "Server UnAvailable", this.Page);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updDetained, "Please Select Date !", this.Page);
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemSMS();
                            return;
                        }
                    }
                #endregion

                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        HiddenItemParents();
                        //Added By Jay T. On dated 11042024
                        HiddenClasses_Commence_From();
                        return;
                    }
            }
            #endregion

            #region SMS For TGPCET (Parents)
            else
                if (rdbEmplyeStud.SelectedValue == "3")
                {

                    if (rboStudent.SelectedValue == "4")
                    {
                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        HiddenItemParents();
                        //Added By Jay T. On dated 11042024
                        HiddenClasses_Commence_From();
                        return;
                        #region SMS For TGPCET (Student) Fees not Paid for All Semester (11/04/2024)
                        //if (txtDate.Text != string.Empty)
                        //{
                        //    if (Convert.ToDateTime(DateTime.Now) > Convert.ToDateTime(txtDate.Text))
                        //    {
                        //        objCommon.DisplayMessage(this.updCollege, "Date should be greater than Today's date", this.Page);
                        //        HiddenItemSMS();
                        //        HiddenItemForPm();
                        //        HiddenItem();
                        //        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //        //Added By Jay T. On dated 23022024
                        //        HiddenItemFeesNotPaid();
                        //        return;
                        //    }
                        //    else
                        //    {

                        //        MSGTYPE = 1;
                        //        string TemplateID = string.Empty;
                        //        string TEMPLATE = string.Empty;
                        //        string message = string.Empty;
                        //        string template = string.Empty;
                        //        int status1 = 0;
                        //        int count = 0;
                        //        foreach (ListViewDataItem dataitem in lvNotpaidAll.Items)
                        //        {
                        //            CheckBox cbRow = dataitem.FindControl("chkSelect1") as CheckBox;
                        //            if (cbRow.Checked == true)
                        //                count++;
                        //        }
                        //        if (count <= 0)
                        //        {
                        //            objCommon.DisplayMessage(this.updCollege, "Please Select atleast one Student For Send SMS", this);
                        //            HiddenItemSMS();
                        //            HiddenItemForPm();
                        //            HiddenItem();
                        //            HiddenItemParents();
                        //            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //            //Added By Jay T. On dated 23022024
                        //            HiddenItemFeesNotPaid();
                        //            return;                              
                        //        }
                        //        else
                        //        {
                        //            foreach (ListViewDataItem items in lvNotpaidAll.Items)
                        //            {
                        //                try
                        //                {
                        //                    CheckBox chkSelect1 = items.FindControl("chkSelect1") as CheckBox;
                        //                    int idno = Convert.ToInt32(chkSelect1.ToolTip.ToString());
                        //                    Label lblStudMobile = items.FindControl("lblStudmobile") as Label;
                        //                    Label lblstudname = items.FindControl("lblStudname") as Label;
                        //                    string studname = lblstudname.Text.TrimEnd();


                        //                    if (chkSelect1.Checked)
                        //                    {
                        //                        string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "STUDENTMOBILE", "IDNO=" + chkSelect1.ToolTip);
                        //                        string ACADEMIC_YEAR = objCommon.LookUp("acd_student S inner join ACD_ACADEMIC_YEAR Y on(S.ACADEMIC_YEAR_ID=Y.ACADEMIC_YEAR_ID)", "Y.ACADEMIC_YEAR_NAME", "IDNO=" + chkSelect1.ToolTip);
                        //                        string date = txtDate.Text;
                        //                        string mobile = "91" + ToMobileNo;
                        //                        if (ToMobileNo != string.Empty)
                        //                        {
                        //                            if (lblStudMobile.Text != string.Empty)
                        //                            {
                        //                                string templatename = "Fees not Paid for All Semester";
                        //                                DataSet ds = objUC.GetSMSTemplate(0, templatename);
                        //                                if (ds.Tables[0].Rows.Count > 0)
                        //                                {
                        //                                    TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                        //                                    TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                        //                                }
                        //                                else
                        //                                {
                        //                                    objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                        //                                    HiddenItemForPm();
                        //                                    HiddenItem();
                        //                                    HiddenItemSMS();
                        //                                    return;
                        //                                }
                        //                                message = TEMPLATE;
                        //                                message = message.Replace("{#var#}", ACADEMIC_YEAR);
                        //                                message = message.Replace("{#var1#}", date);

                        //                                // Create a StringBuilder and append the template
                        //                                StringBuilder stringBuilder = new StringBuilder();
                        //                                stringBuilder.Append(message);
                        //                                // Get the final message string
                        //                                template = stringBuilder.ToString();
                        //                                //this.SendSMS(lblStudMobile.Text.Trim(), template, TemplateID);
                        //                                status1 = SendSMS_Admission(lblStudMobile.Text.Trim(), template, TemplateID);
                        //                            }
                        //                        }
                        //                        if (status1 == 1)
                        //                        {
                        //                            objCommon.DisplayUserMessage(this.updCollege, "SMS Sent Successfully.", this.Page);
                        //                            HiddenItemSMS();
                        //                            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //                            //Added By Jay T. On dated 23022024
                        //                            HiddenItemFeesNotPaid();
                        //                            //Added By Sakshi M on 20012024 to maintain log 
                        //                            CustomStatus cs = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Fees not Paid for All Semester(Parents)", lblStudMobile.Text.Trim(), Convert.ToInt32(Session["usertype"]), idno, 2, "", IPADDRESS, Convert.ToInt32(Session["OrgId"]));
                        //                        }
                        //                        else
                        //                        {
                        //                            objCommon.DisplayMessage(this.updCollege, "Failed To send SMS.", this.Page);
                        //                            HiddenItemSMS();
                        //                            HiddenItemParents();
                        //                            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                        //                            //Added By Jay T. On dated 23022024
                        //                            HiddenItemFeesNotPaid();
                        //                        }

                        //                    }
                        //                    else
                        //                    {
                        //                        objCommon.DisplayMessage(this.updCollege, "Sorry..! Didn't found Mobile no. for some Parents(s)", this.Page);
                        //                    }
                        //                }
                        //                catch (Exception ex)
                        //                {
                        //                    if (Convert.ToBoolean(Session["error"]) == true)
                        //                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSndSms_Click-> " + ex.Message + " " + ex.StackTrace);
                        //                    else
                        //                    {
                        //                        objCommon.DisplayMessage(this.updCollege, "Server UnAvailable", this.Page);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage(this.updDetained, "Please Select Date !", this.Page);
                        //    HiddenItemForPm();
                        //    HiddenItem();
                        //    HiddenItemSMS();
                        //    return;
                        //}
                        #endregion
                    }
                    else if (rboStudent.SelectedValue == "5")
                    {
                        #region SMS For TGPCET (Student) Classes Commence From (11/04/2024)
                        #endregion
                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        HiddenItemParents();
                        //Added By Jay T. On dated 11042024
                        HiddenClasses_Commence_From();
                        return;
                    }

                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        HiddenItemParents();
                        //Added By Jay T. On dated 11042024
                        HiddenClasses_Commence_From();
                        return;
                    }
                }
            #endregion
        }
        #endregion

        #region SMS For Other Client
        else
        {
            objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
            HiddenItemSMS();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            return;

        }
        #endregion
    }
    #endregion

    #region SMS SERVICE METHOD
    public int sendEmail(string Message, string mailId, string Subject)
    {
        int status = 1;
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("REFF", "SUBJECT_OTP", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
            //string Org =Convert.ToString(objCommon.FillDropDown("REFF", "SUBJECT_OTP", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty));
            string Org = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() : string.Empty;
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
            // any address where the email will be sending
            var toAddress = EMAILID.Trim();
            //Password of your gmail address

            var fromPassword = objCommon.LookUp("REFF", "(EMAILSVCPWD)", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
            // Passing the values and make a email formate to display
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            msg.From = new System.Net.Mail.MailAddress(fromAddress, Org);
            msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
            msg.Subject = Subject;

            msg.IsBodyHtml = true;
            msg.Body = Message;
            System.Net.Mail.Attachment attachment;
            string file = ViewState["FileName"].ToString();
            //If want to send attachment in email
            if (file != string.Empty || file != "") //Added By Rishabh B. on 11012022
            {
                if (fuAttachment.HasFile)
                {
                    attachment = new System.Net.Mail.Attachment(Server.MapPath("~/TempDocument/") + "\\" + ViewState["FileName"].ToString());
                    msg.Attachments.Add(attachment);
                }
            }
            //smtp.enableSsl = "true";
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
            //ServicePointManager.ServerCertificateValidationCallback =
            //    delegate(object s, X509Certificate certificate,
            //    X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //    { return true; };
            //smtp.Send(msg);

            ServicePointManager.ServerCertificateValidationCallback =
              delegate(object s, X509Certificate certificate,
              X509Chain chain, SslPolicyErrors sslPolicyErrors)
              { return true; };
            smtp.Send(msg);
            return status = 1;
        }

        catch (Exception ex)
        {
            throw;
        }
        //return status;
    }

    public void SendSMS(string Mobile, string text, string TemplateID)
    {
        string status = "";
        int ret = 0;
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;
                postDate += "&";
                postDate += "TemplateID=" + TemplateID;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";

            }

        }
        catch
        {
            throw;
        }


    }

    public int SendSMS_Admission(string Mobile, string text, string TemplateID)
    {
        string status = "";
        int ret = 0;
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;
                postDate += "&";
                postDate += "TemplateID=" + TemplateID;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
                if (status == "")
                {
<<<<<<< HEAD
                    ret = 0;
=======
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("" + ds.Tables[0].Rows[0]["SMSProvider"].ToString()));
                    request.ContentType = "text/xml; charset=utf-8";
                    request.Method = "POST";

                    string postDate = "username=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                    postDate += "&";
                    postDate += "pass=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                    postDate += "&";
                    postDate += "senderid=GPGNGP";
                    postDate += "&";
                    postDate += "message=" + text;
                    postDate += "&";
                    postDate += "dest_mobileno=91" + Mobile;
                    postDate += "&";
                    postDate += "msgtype=TXT";
                    postDate += "&";
                    postDate += "response=Y";

                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                    request.ContentType = "application/x-www-form-urlencoded";

                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse _webresponse = request.GetResponse();
                    dataStream = _webresponse.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    status = reader.ReadToEnd();
                    if (status == "")
                    {
                        ret = 0;
                    }
                    else
                    {
                        ret = 1;
                    }
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                }
                else
                {
                    ret = 1;
                }

            }
            else
            {
                ret = 0;

            }

        }
        catch
        {
            throw;
        }
        return ret;


    }

    public void SendSMS_today(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                //url = string.Format(ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;


                //WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                //string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;
                //Session["result"] = 1;
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Cancel

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cancel();
    }

    public void cancel()
    {

        if (rdbEmplyeStud.SelectedValue == "1")
        {
            ddlDepartment.SelectedIndex = 0;
            empPanel.Enabled = true;
            Studpanel.Enabled = false;
            lvEmployee.Visible = false;
            trEmployee.Visible = false;
            rboStudent.SelectedValue = "-1";
            pnldate.Visible = false;
            PnlNotPaidStudent.Visible = false;
            pnlStudentInstallment.Visible = false;

            HiddenItemSMS();
        }
        else if (rdbEmplyeStud.SelectedValue == "2")
        {
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlSchool.SelectedIndex = 0;
            Studpanel.Enabled = false;
            empPanel.Enabled = true;
            lvStudent.Visible = false;
            trStudent.Visible = false;
            pnldate.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            HiddenItemSMS();

        }
        else if (rdbEmplyeStud.SelectedValue == "3")
        {
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlSchool.SelectedIndex = 0;
            Studpanel.Enabled = false;
            empPanel.Enabled = true;
            lvStudent.Visible = false;
            trStudent.Visible = false;
            pnldate.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            HiddenItemSMS();
<<<<<<< HEAD
            HiddenItemParents();
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

        }


        txtsub.Text = string.Empty.Trim();
        txtMessage.Text = string.Empty.Trim();
        rdbEmplyeStud.SelectedValue = "1";
        rboStudent.SelectedValue = "-1";
        pnldate.Visible = false;
        pnlStudentInstallment.Visible = false;
        lvPaidStudentInstallment.DataSource = null;
        lvPaidStudentInstallment.DataBind();
        PnlNotPaidStudent.Visible = false;
        lvnotpaid.DataSource = null;
        lvnotpaid.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        rdbEmailSms.SelectedValue = "-1";
        DivDate1.Visible = false;
        txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
        divEmail.Visible = false;
        HiddenItemSMS();
        HiddenItemForPm();
        HiddenItem();
        divmbody.Visible = false;
        divTtype.Visible = false;
        divEmail.Visible = false;
        lvTemplate.Visible = false;
        btnSndMessg.Enabled = false;
        btnSndSms.Enabled = false;
        DivDate1.Visible = false;
        txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
        HiddenItemParents();
<<<<<<< HEAD
=======
        HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
        //Added By Jay T. On dated 23022024
        ddlReceiptType.SelectedIndex = 0;
        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
    }

    #endregion

    #region Bind DDL

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT (CDB.BRANCHNO)", "B.LONGNAME", "CDB.COLLEGE_ID=" + ddlSchool.SelectedValue + " AND CDB.DEGREENO =" + ddlDegree.SelectedValue + " AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CDB.BRANCHNO");
        HiddenItemSMS();
<<<<<<< HEAD
        HiddenItemParents();
=======
        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
        //Added By Jay T. On dated 23022024
        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "A.LONGNAME", "B.DEGREENO="+ddlDegree.SelectedValue+" AND A.BRANCHNO>0", "A.BRANCHNO");
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsemester.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        if (ddlSchool.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "DISTINCT (CDB.DEGREENO)", "D.DEGREENAME", "CDB.COLLEGE_ID=" + ddlSchool.SelectedValue + " AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CDB.DEGREENO");
        }
        HiddenItemSMS();
        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
    }

    #endregion

    #region commit
    static async Task<int> Execute(string Message, string toEmailId, string sub, string filename, string path, int OrgId)
    {
        int ret = 0;
        try
        {
            string Imgfile = string.Empty;
            if (filename != string.Empty)
            {
                path = path + filename;
                string LogoPath = path;
                Byte[] Imgbytes = File.ReadAllBytes(LogoPath);
                Imgfile = Convert.ToBase64String(Imgbytes);

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

                var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
                var attachment = new SendGrid.Helpers.Mail.Attachment()
                {
                    Content = Imgfile,
                    Type = "image/png/pdf",
                    Filename = filename,
                    Disposition = "inline",
                    ContentId = "Logo"
                };
                attachments.Add(attachment);
                msg.AddAttachments(attachments);
                //var response = client.SendEmailAsync(msg);
                //string res = Convert.ToString(response.IsCompleted);
                //if (res == "Accepted")
                //{
                //    ret = 1;
                //}
                //else
                //{
                //    ret = 0;
                //}
                //attachments.Dispose();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                string res = Convert.ToString(response.StatusCode);
                if (res == "Accepted")
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }
            }
            else
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
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                string res = Convert.ToString(response.StatusCode);
                if (res == "Accepted")
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }


            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }
    #endregion

    #region Radio Button
    protected void rdbEmplyeStud_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbEmplyeStud.SelectedValue == "1")
        {
            empPanel.Enabled = true;
            Studpanel.Enabled = false;
            pnlstud.Visible = false;
            PnlNotPaidStudent.Visible = false;
            pnlStudentInstallment.Visible = false;
            rboStudent.SelectedValue = "-1";
            rdbEmailSms.SelectedValue = "-1";
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
            pnldate.Visible = false;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtsub.Text = "";
            txtMessage.Text = "";
            pnldate.Visible = false;
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SUBDEPTNO");
            HiddenItemSMS();
            HiddenItemForPm();
            HiddenItem();
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = false;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = false;
            btnSndSms.Enabled = false;
<<<<<<< HEAD
            HiddenItemParents();

=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            supSem.Visible = true;
            divsemester1.Visible = true;
            divRType.Visible = false;
            ddlsemester.Visible = true;
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rdbEmplyeStud.SelectedValue == "2")
        {
            employeepanel.Visible = false;
            Studpanel.Enabled = true;
            empPanel.Enabled = false;
            ddlDepartment.SelectedIndex = 0;
            PnlNotPaidStudent.Visible = false;
            rboStudent.SelectedValue = "-1";
            rdbEmailSms.SelectedValue = "-1";
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtMessage.Text = "";
            txtsub.Text = "";
            HiddenItemSMS();
            HiddenItemForPm();
            pnldate.Visible = false;
            HiddenItem();
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = false;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = false;
            btnSndSms.Enabled = false;
<<<<<<< HEAD
            HiddenItemParents();
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            supSem.Visible = true;
            divsemester1.Visible = true;
            divRType.Visible = false;
            ddlsemester.Visible = true;
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

        }
        else if (rdbEmplyeStud.SelectedValue == "3")
        {
            employeepanel.Visible = false;
            Studpanel.Enabled = true;
            empPanel.Enabled = false;
            ddlDepartment.SelectedIndex = 0;
            PnlNotPaidStudent.Visible = false;
            rboStudent.SelectedValue = "-1";
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
            rdbEmailSms.SelectedValue = "-1";
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtMessage.Text = "";
            txtsub.Text = "";
            HiddenItemSMS();
            HiddenItemForPm();
            HiddenItem();
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = false;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = false;
            btnSndSms.Enabled = false;
<<<<<<< HEAD
            HiddenItemParents();
            pnldate.Visible = false;
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            pnldate.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            supSem.Visible = true;
            divsemester1.Visible = true;
            divRType.Visible = false;
            ddlsemester.Visible = true;
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

        }

    }

    protected void rdbEmailSms_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbEmailSms.SelectedValue == "1")
        {
            HiddenItemSMS();
            //----------------------------//
            if (Convert.ToInt32(Session["OrgId"]) == 21)
            {
                if (rboStudent.SelectedValue == "4" || rboStudent.SelectedValue == "5")
                {
                    txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
                    DivDate1.Visible = true;
                }
                else
                {
                    DivDate1.Visible = false;
                    txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
                }
            }
            else
            {
                DivDate1.Visible = false;
                txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
            }
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = false;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = false;
            btnSndSms.Enabled = true;
            HiddenItemParents();
<<<<<<< HEAD
=======
            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rdbEmailSms.SelectedValue == "0")
        {
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = true;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = true;
            btnSndSms.Enabled = false;
            HiddenItemSMS();
            HiddenItemParents();
<<<<<<< HEAD
=======
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
            DivDate1.Visible = false;
            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
        }
        else
        {
            HiddenItemSMS();
            //----------------------------//
            if (Convert.ToInt32(Session["OrgId"]) == 21)
            {
                if (rboStudent.SelectedValue == "4")
                {
                    txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
                    DivDate1.Visible = false;
                }
                else
                {
                    DivDate1.Visible = false;
                    txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
                }
            }
            else
            {
                DivDate1.Visible = false;
                txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
            }
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = false;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = false;
            btnSndSms.Enabled = false;
            HiddenItemParents();
            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            divmbody.Visible = false;
            divTtype.Visible = false;
            divEmail.Visible = false;
            lvTemplate.Visible = false;
            btnSndMessg.Enabled = false;
            btnSndSms.Enabled = false;
            HiddenItemSMS();
            HiddenItemParents();
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 23022024
            DivDate1.Visible = false;
            HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }

    }

    protected void ddlTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTemplateType.SelectedValue != "0")
        {
            DataSet ds = new DataSet();
            ds = studinfo.GetTemplateDetails_CRESCENT(Convert.ToInt32(ddlTemplateType.SelectedValue));


            lvTemplate.DataSource = ds;
            lvTemplate.DataBind();
            hftt.Value = lvTemplate.Items.Count.ToString();
            int count = 0;
            foreach (ListViewItem item in lvTemplate.Items)
            {
                count++;
                TextBox txtTemp = (TextBox)item.FindControl("txtVarTemplate");
                if (Convert.ToInt32(count) == 1)
                {
                    txtTemp.Enabled = true;
                }
                else
                {
                    txtTemp.Enabled = false;
                }
            }

        }
        else
        {
        }

        if (ddlTemplateType.SelectedIndex > 0)
        {
            DataSet Bs = new DataSet();
            Bs = studinfo.GetVariableTemplateDetails_CRESCENT(Convert.ToInt32(ddlTemplateType.SelectedValue));
            if (Bs != null && Bs.Tables[0].Rows.Count > 0)
            {
                lblTemplate.Text = Bs.Tables[0].Rows[0]["TEMPLATE"].ToString();
            }
        }
    }

    protected void rboStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rboStudent.SelectedValue == "1")
        {
            pnldate.Visible = false;
            trStudent.Visible = false;
            lvStudent.Visible = false;
            trEmployee.Visible = false;
            pnlStudentInstallment.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            HiddenItemSMS();
<<<<<<< HEAD
            HiddenItemParents();
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            supSem.Visible = true;
            divRType.Visible = false;
            divsemester1.Visible = true;
            rdbEmailSms.SelectedValue = "-1";
            rdbEmailSms_SelectedIndexChanged(rdbEmailSms.SelectedValue, e); //Added By Jay T. On dated 12042024
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
            ddlsemester.Visible = true;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rboStudent.SelectedValue == "2")
        {
            pnldate.Visible = true;
            trStudent.Visible = false;
            lvStudent.Visible = false;
            trEmployee.Visible = false;
            PnlNotPaidStudent.Visible = false;
            HiddenItemSMS();
<<<<<<< HEAD
            HiddenItemParents();
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024

            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            supSem.Visible = true;
            divRType.Visible = false;
            rdbEmailSms.SelectedValue = "-1";
            rdbEmailSms_SelectedIndexChanged(rdbEmailSms.SelectedValue, e); //Added By Jay T. On dated 12042024
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
            divsemester1.Visible = true;
            ddlsemester.Visible = true;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rboStudent.SelectedValue == "3")
        {
            pnldate.Visible = false;
            trStudent.Visible = false;
            lvStudent.Visible = false;
            trEmployee.Visible = false;
            PnlNotPaidStudent.Visible = false;
            pnlStudentInstallment.Visible = false;
            txtStartDate.Text = string.Empty;
            rdbEmailSms.SelectedValue = "-1";
            rdbEmailSms_SelectedIndexChanged(rdbEmailSms.SelectedValue, e); //Added By Jay T. On dated 12042024
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
            txtEndDate.Text = string.Empty;
            HiddenItemSMS();
<<<<<<< HEAD
            HiddenItemParents();
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            supSem.Visible = true;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            divsemester1.Visible = true;
            ddlsemester.Visible = true;
            divRType.Visible = false;
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
        }
        else if (rboStudent.SelectedValue == "4")
        {
            pnldate.Visible = false;
            trStudent.Visible = false;
            lvStudent.Visible = false;
            trEmployee.Visible = false;
            PnlNotPaidStudent.Visible = false;
            HiddenItemSMS();
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            supSem.Visible = false;
            divsemester1.Visible = false;
            rdbEmailSms.SelectedValue = "-1";
            rdbEmailSms_SelectedIndexChanged(rdbEmailSms.SelectedValue, e); //Added By Jay T. On dated 12042024
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
            divRType.Visible = true;
            ddlsemester.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rboStudent.SelectedValue == "5")  //Added By Jay T. On dated 12042024
        {
            pnldate.Visible = false;
            trStudent.Visible = false;
            lvStudent.Visible = false;
            rdbEmailSms.SelectedValue = "-1";
            rdbEmailSms_SelectedIndexChanged(rdbEmailSms.SelectedValue, e); //Added By Jay T. On dated 12042024
            DivDate1.Visible = false;
            txtDate.Text = string.Empty;  //Added By Jay T. On dated 12042024
            trEmployee.Visible = false;
            pnlStudentInstallment.Visible = false;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            HiddenItemSMS();
            HiddenItemParents();
            HiddenClasses_Commence_From(); //Added By Jay T. On dated 12042024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            supSem.Visible = true;
            divRType.Visible = false;
            divsemester1.Visible = true;
            ddlsemester.Visible = true;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvnotpaid.DataSource = null;
            lvnotpaid.DataBind();
            PnlAllNotPaidStudent.Visible = false;
        }

    }

    #endregion

    #endregion SendBulkEmail and SMS

    #region Attendance Email Sending

    #region Cancel

    private void ClearControls()
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        //  ddlSection.Items.Clear();
        lboddlSection.Items.Clear();
        lboddlSection.ClearSelection();
        //  ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlFaculty.Items.Clear();
        ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();

    }

    private void ClearAllAfterSms()
    {
        ddlSession.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        // ddlSection.Items.Clear();
        //  ddlSection.Items.Add(new ListItem("Please Select", "0"));
        lboddlSection.Items.Clear();
        lboddlSection.ClearSelection();
        ddltheorypractical.Items.Clear();
        ddltheorypractical.Items.Add(new ListItem("Please Select", "0"));
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        txtPercentage.Text = "0";
        lvStudents.DataSource = null;
        lvStudents.Visible = false;
        //txtSubject.Text = string.Empty;
        txtMessageAtdEmail.Text = string.Empty;
        lvStudents.Items.Clear();
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();

        if (Convert.ToInt32(Session["OrgId"]) == 7)
        {
            btnWhatsapp.Visible = true;
            btnWhatsapp.Enabled = true;
        }
    }

    protected void btnCancelAtdEmail_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());

        ClearAllAfterSms();
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();
        //ClearControls();
    }

    #endregion Cancel

    #region DDL
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjectType, "ACD_ATTENDANCE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "AND C.SEMESTERNO=" + ddlSem.SelectedValue + " AND C.SESSIONNO=" + ddlSession.SelectedValue, "C.SUBID");
                ddlSubjectType.Focus();
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();

            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSem.SelectedIndex = 0;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubjectType.SelectedIndex > 0)
        {
            lboddlSection.ClearSelection();
            // objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION S ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue+" AND SR.SCHEMENO="+ddlScheme.SelectedValue+" AND SR.SEMESTERNO="+ddlSem.SelectedValue+" AND S.SECTIONNO>0", "S.SECTIONNO");
            objCommon.FillListBox(lboddlSection, "ACD_ATTENDANCE SR INNER JOIN ACD_SECTION S ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue + " AND S.SECTIONNO>0", "S.SECTIONNO");
            lboddlSection.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
        else
        {
            lboddlSection.ClearSelection();
            lboddlSection.Items.Clear();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }

    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddltheorypractical.SelectedValue == "2" || ddltheorypractical.SelectedValue == "3")
        {
            dvBatch.Visible = true;
            //rfvBatch.Visible = true;
            int count = 0;
            string Section = string.Empty;
            foreach (ListItem Item in lboddlSection.Items)
            {
                if (Item.Selected)
                {
                    Section += Item.Value + ",";
                    count++;
                }
            }

            Section = Section.Substring(0, Section.Length - 1);


            if (Convert.ToInt32(Section) > 0 && ddltheorypractical.SelectedValue != "1")
            {
                if (ddltheorypractical.SelectedValue == "2")
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR INNER JOIN ACD_BATCH B ON (B.BATCHNO = SR.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SECTIONNO in (" + Convert.ToInt32(Section) + ")", "B.BATCHNO");
                else
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR INNER JOIN ACD_BATCH B ON (B.BATCHNO = SR.TH_BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + "AND SR.SECTIONNO in (" + Convert.ToInt32(Section) + ")", "B.BATCHNO");

                ddlBatch.Focus();
            }
            else
            {
                // ddlSection.Items.Clear();
                //  ddlSection.SelectedIndex = 0;
                lboddlSection.Items.Clear();
                lboddlSection.ClearSelection();
                ddlBatch.Items.Clear();
                ddlBatch.SelectedIndex = 0;
            }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
        else
        {
            dvBatch.Visible = false;
            //rfvBatch.Visible = false;
            HiddenItemForPm();
            HiddenItemSMS();
            HiddenItem();
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();

        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select Session", this.Page);
            ddlSession.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }
    #endregion

    #region Show
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int count = 0;
            string Section = string.Empty;
            foreach (ListItem Item in lboddlSection.Items)
            {
                if (Item.Selected)
                {
                    Section += Item.Value + ",";
                    count++;
                }
            }
            if (count > 0)
            {
                Section = Section.Substring(0, Section.Length - 1);
            }
            else
            {
                Section = "0";
            }
            ds = STT.GetAttendanceDeailsForSms(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"])), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Section, ddlOperator.SelectedValue, Convert.ToDecimal(txtPercentage.Text), Convert.ToInt32(ddlSubjectType.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.Visible = true;
                lvStudents.DataSource = ds.Tables[0];
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
                btnEmail.Enabled = true;
                if (Convert.ToInt32(Session["OrgId"]) == 7)
                {
                    //btnWhatsapp.Visible = true;
                    btnWhatsapp.Enabled = true;
                }
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
            else
            {
                lvStudents.Visible = false;
                objCommon.DisplayMessage(this.Page, "No Students Found For Current Selections", this.Page);
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region SMS Config (17-08-2023)

    #region Visible false
    protected void btnSmsToParents_Click(object sender, EventArgs e)
    {
        string mobile = string.Empty;
        string smsMessage = string.Empty;
        string idno = string.Empty;
        string TClass = string.Empty;
        string TAttendance = string.Empty;
        string TPercentage = string.Empty;
        string Sregno = string.Empty;
        string SessionName = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
        DataSet ds2 = null;
        try
        {
            foreach (ListViewDataItem item in lvStudents.Items)
            {

                CheckBox chk = item.FindControl("cbRow") as CheckBox;
                Label lblTotalclass = item.FindControl("lblTotalclass") as Label;
                Label lblTotalattendance = item.FindControl("lblTotalattendance") as Label;
                Label lblPercentage = item.FindControl("lblPercentage") as Label;
                Label lblRegno = item.FindControl("lblRegno") as Label;


                if (chk.Checked == true)
                {

                    TClass = lblTotalclass.Text.TrimEnd();
                    TAttendance = lblTotalattendance.Text.TrimEnd();
                    TPercentage = lblPercentage.Text.TrimEnd();
                    Sregno = lblRegno.Text.TrimEnd();
                    idno = chk.ToolTip.TrimEnd();
                    ds2 = STT.AttendenceWiseGetEmailAndMobileForCommunication(idno);

                    string Mobilelist = string.Empty;

                    //foreach (DataRow dr in ds2.Tables[0].Rows)
                    //{
                    //  Mobilelist += dr["FATHERMOBILE"].ToString() + ",";

                    //mobile = dr["FATHERMOBILE"].ToString();
                    mobile = ds2.Tables[0].Rows[0]["FATHERMOBILE"].ToString();

                    smsMessage = "Student Attendance Session: " + SessionName + "\n" + "Enroll No: " + Sregno + "\n" + "Total Class-" + TClass + "\n" + "Total Attendance-" + TAttendance + "\n" + "Percentage-" + TPercentage + "\n" + "Regards\n" + "Sarala Birla University, Ranchi";

                    // this.SendSMSAtdEmail(mobile, smsMessage);//For sending SMS
                    objCommon.DisplayMessage(updReport, "SMS sent Succesfully!", this.Page);


                    //smsMessage = txtSms.Text;
                    if (mobile == string.Empty)
                    {
                        objCommon.DisplayMessage(updReport, "Sorry..! Dont find Some Mobile no.", this.Page);
                    }
                    //dmims comment 18102019//  mobile += ds2.Tables[0].Rows[0]["STUDENTMOBILE"].ToString().TrimEnd() + ",";

                    // }
                }
            }

            if (idno.Length <= 0)
            {
                objCommon.DisplayMessage(updReport, "Please Select atleast one Student for SMS", this.Page);
            }
            ClearAllAfterSms();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion Visible false

    public void SendSMSAtdEmail(string Mobile, string text)
    {

        //OMEGA//

        int result = 0;
        string user = "";
        string Password = "";
        string Msg = text;
        string sender = "ERPSMS";
        string MobileNumber = Mobile;
        string SmsURL = "";
        try
        {
            if (Mobile != string.Empty)
            {
                DataSet ds = objCommon.FillDropDown("Reff", "COMPANY_SMSSVCID", "COMPANY_SMSSVC_TOKEN,COMPANY_SMS_URL", "", "");

                user = ds.Tables[0].Rows[0]["COMPANY_SMSSVCID"].ToString();
                Password = ds.Tables[0].Rows[0]["COMPANY_SMSSVC_TOKEN"].ToString();
                SmsURL = ds.Tables[0].Rows[0]["COMPANY_SMS_URL"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    WebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + SmsURL + "?username=" + user + "&msg_token=" + Password + "&sender_id=" + sender + "&message=" + Msg + "&mobile=" + MobileNumber));
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string urlText = reader.ReadToEnd();
                    result = 1;
                    // return result; //OK
                }
            }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
        catch (Exception ex)
        {
            throw;
        }
        // return result;
        //return result;
    }
    protected void btnSmsToStudent_Click(object sender, EventArgs e)
    {
        string smsMessage = string.Empty;
        string idno = string.Empty;
        string TClass = string.Empty;
        string TAttendance = string.Empty;
        string TPercentage = string.Empty;
        string Sregno = string.Empty;
        string TemplateID = string.Empty;
        string TEMPLATE = string.Empty;
        string message = string.Empty;
        string template = string.Empty;
        int count = 0;
        int MSGTYPE = 1;
        string SessionName = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
        DataSet ds2 = null;
        try
        {
            foreach (ListViewDataItem dataitem in lvStudents.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send SMS", this);
                return;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
            else
            {
                foreach (ListViewDataItem item in lvStudents.Items)
                {

                    CheckBox chk = item.FindControl("cbRow") as CheckBox;
                    Label lblTotalclass = item.FindControl("lblTotalclass") as Label;
                    Label lblTotalattendance = item.FindControl("lblTotalattendance") as Label;
                    Label lblPercentage = item.FindControl("lblPercentage") as Label;
                    Label lblRegno = item.FindControl("lblRegno") as Label;
                    TClass = lblTotalclass.Text.TrimEnd();
                    TAttendance = lblTotalattendance.Text.TrimEnd();
                    TPercentage = lblPercentage.Text.TrimEnd();
                    Sregno = lblRegno.Text.TrimEnd();
                    idno = chk.ToolTip.TrimEnd();

                    string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + idno);
                    string mobile = "91" + ToMobileNo;
                    if (ToMobileNo != string.Empty)
                    {
                            string templatename = "Bulk Email Attendance Sending";
                            DataSet ds = objUC.GetSMSTemplate(0, templatename);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                HiddenItemForPm();
                                HiddenItem();
                                HiddenItemSMS();
                                HiddenItemSMSmark();
                                return;
                            }
                            message = TEMPLATE;
                            message = message.Replace("{#var#}", SessionName);
                            message = message.Replace("{#var1#}", Sregno);
                            message = message.Replace("{#var2#}", TClass);
                            message = message.Replace("{#var3#}", TAttendance);
                            message = message.Replace("{#var4#}", TPercentage);

                            // Create a StringBuilder and append the template
                            StringBuilder stringBuilder = new StringBuilder();
                            stringBuilder.Append(message);
                            // Get the final message string
                            template = stringBuilder.ToString();
                        
                    }
                    if (ToMobileNo != string.Empty)
                    {
                            this.SendSMS(ToMobileNo, template, TemplateID);
                            objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemSMS();
                            HiddenItemSMSmark();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                    }

                }
            }
            ClearAllAfterSms();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Whatsapp Config (17-08-2023)
    protected void btnWhatsapp_Click(object sender, EventArgs e)
    {
        string MailSendStatus = string.Empty;
        string MailNotSendStatus = string.Empty;
        string useremails = string.Empty;
        string name = string.Empty;
        string idno = string.Empty;
        string TClass = string.Empty;
        string TAttendance = string.Empty;
        string TPercentage = string.Empty;
        string Sregno = string.Empty;
        string SessionName = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
        DataSet dsconfig = objCommon.FillDropDown("REFF", "USER_PROFILE_SENDERNAME,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
        string CollegeName = dsconfig.Tables[0].Rows[0]["CollegeName"].ToString();
        string College = dsconfig.Tables[0].Rows[0]["USER_PROFILE_SENDERNAME"].ToString();
        DataSet ds1 = null;
        DataSet ds2 = null;
        string message = txtMessageAtdEmail.Text;
        string subject = txtSubject.Text;
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudents.Items)
        {
            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this.updReport, "Please Select atleast one Student For Send WhatsApp Message", this);
            return;
        }
        else
        {
            if (message != string.Empty)
            {
                try
                {

                    foreach (ListViewDataItem lvItem in lvStudents.Items)
                    {
                        CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                        HiddenField Regno = lvItem.FindControl("Regno") as HiddenField;
                        Label lblStudname = lvItem.FindControl("lblStudname") as Label;
                        Label lblTotalclass = lvItem.FindControl("lblTotalclass") as Label;
                        Label lblTotalattendance = lvItem.FindControl("lblTotalattendance") as Label;
                        Label lblPercentage = lvItem.FindControl("lblPercentage") as Label;
                        Label lblRegno = lvItem.FindControl("lblRegno") as Label;
                        if (chkBox.Checked == true)
                        {
                            if (lblTotalclass.Text == "")
                            {
                                TClass = "0";

                            }
                            else
                            {
                                TClass = lblTotalclass.Text.TrimEnd();
                            }
                            if (lblTotalattendance.Text == "")
                            {
                                TAttendance = "0";

                            }
                            else
                            {
                                TAttendance = lblTotalattendance.Text.TrimEnd();
                            }
                            if (lblPercentage.Text == "")
                            {
                                TPercentage = "0";
                            }
                            else
                            {
                                TPercentage = lblPercentage.Text.TrimEnd();

                            }
                            Sregno = lblRegno.Text.TrimEnd();
                            idno = chkBox.ToolTip.TrimEnd();
                            string studname = lblStudname.Text;
                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "STUDENTMOBILE", "IDNO=" + chkBox.ToolTip);
                            string mobile = "91" + ToMobileNo;
                            if (ToMobileNo != string.Empty)
                            {
                                WhatsappAtt(studname, mobile, SessionName, Sregno, TClass, TAttendance, TPercentage, message);
                                MailSendStatus += chkBox.ToolTip + ',';
                            }
                            else
                            {
                                MailNotSendStatus += chkBox.ToolTip + ',';
                            }
                        }
                    }

                    if (MailNotSendStatus != string.Empty)
                    {
                        ds1 = (objCommon.FillDropDown("ACD_STUDENT", "(STUDNAME + '  #  ' + REGNO) collate DATABASE_DEFAULT  AS STUDNAME", "IDNO", "IDNO IN (" + MailNotSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }

                    if (MailSendStatus != string.Empty)
                    {
                        ds2 = (objCommon.FillDropDown("ACD_STUDENT", "(STUDNAME + '  #  ' + REGNO) collate DATABASE_DEFAULT AS STUDNAME", "IDNO", "IDNO IN (" + MailSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }
                    string MailSendTo = string.Empty;
                    string MailNotSendTo = string.Empty;


                    if (MailNotSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "\n" + ",";
                        }
                    }


                    if (MailSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            MailSendTo += ds2.Tables[0].Rows[i]["STUDNAME"].ToString() + ",";
                        }
                    }
                    if (MailSendTo != string.Empty || MailNotSendTo != string.Empty)
                    {
                        objCommon.DisplayMessage(this.updReport, "Whatsapp Message Sent successfully", this);
                        lblMailNorSendTo.Visible = true;
                        lblMailSendTo.Visible = true;
                        lblMailSendTo.Text = "WhatsApp  Message Send Student List - " + "\n" + MailSendTo.ToString().TrimEnd(',');
                        lblMailNorSendTo.Text = "WhatsApp  Message Not Send Student List - " + "\n" + MailNotSendTo.ToString().TrimEnd(',');
                        txtMessageAtdEmail.Text = string.Empty;
                    }
                    else
                    {
                        lblMailNorSendTo.Visible = false;
                        lblMailSendTo.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
            else
            {
                objCommon.DisplayMessage(this.updReport, "Please Enter WhatsApp Message", this.Page);
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
        }
    }
    #endregion

    #region Email
    protected void btnEmail_Click(object sender, EventArgs e)
    {

        string MailSendStatus = string.Empty;
        string MailNotSendStatus = string.Empty;
        string useremails = string.Empty;
        string name = string.Empty;
        string idno = string.Empty;
        string TClass = string.Empty;
        string TAttendance = string.Empty;
        string TPercentage = string.Empty;
        string Sregno = string.Empty;
        string SessionName = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
        DataSet dsconfig = objCommon.FillDropDown("REFF", "USER_PROFILE_SENDERNAME,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
        string CollegeName = dsconfig.Tables[0].Rows[0]["CollegeName"].ToString();
        string College = dsconfig.Tables[0].Rows[0]["USER_PROFILE_SENDERNAME"].ToString();
        DataSet ds1 = null;
        DataSet ds2 = null;
        string message = txtMessageAtdEmail.Text;
        string subject = txtSubject.Text;
        if (message != string.Empty && subject != string.Empty)
        {
            try
            {

                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                    HiddenField Regno = lvItem.FindControl("Regno") as HiddenField;
                    Label lblStudname = lvItem.FindControl("lblStudname") as Label;
                    Label lblTotalclass = lvItem.FindControl("lblTotalclass") as Label;
                    Label lblTotalattendance = lvItem.FindControl("lblTotalattendance") as Label;
                    Label lblPercentage = lvItem.FindControl("lblPercentage") as Label;
                    Label lblRegno = lvItem.FindControl("lblRegno") as Label;
                    if (chkBox.Checked == true)
                    {
                        TClass = lblTotalclass.Text.TrimEnd();
                        TAttendance = lblTotalattendance.Text.TrimEnd();
                        TPercentage = lblPercentage.Text.TrimEnd();
                        Sregno = lblRegno.Text.TrimEnd();
                        idno = chkBox.ToolTip.TrimEnd();
                        string studname = lblStudname.Text;
                        string useremail = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + chkBox.ToolTip);
                        if (useremail != string.Empty)
                        {
                            string msg = "<h1>Greetings !!</h1>";
                            msg += "Dear" + " " + "<b>" + studname + "," + "</b>";   //b
                            msg += "<br />";
                            msg += "<br />";
                            msg += "<b>" + message + "</b>" + "<br/><br/>";
                            msg += "<b>" + SessionName + "</b>" + "<br/>";//b
                            msg += "<b>Enrollment Number: <b>" + Sregno + "</b>" + "<br/>";//b
                            msg += "<b>Total Classes:" + TClass + "</b>" + "<br/>";//b
                            msg += "<b>Total Attendance:" + TAttendance + "</b>" + "<br/>";//b
                            msg += "<b>Percentage:" + TPercentage + "</b>" + "<br/><br/><br/>";//b
                            msg += "This is an auto generated response to your email. Please do not reply to this mail.";
                            msg += "<br /><br /><br /><br />Regards,<br />";   //bb
                            msg += "" + CollegeName + "<br /><br />";   //bb
                            string nbody = MessageBody(studname, msg, SessionName, Sregno, TClass, TAttendance, TPercentage, useremail);
                            //txtSubject.Text = string.Empty;
                            //txtMessageAtdEmail.Text = string.Empty;
                            //int status = SendMailBYSendgrid(nbody, useremail, subject); for email sending
                            MailSendStatus += chkBox.ToolTip + ',';
                        }
                        else
                        {
                            MailNotSendStatus += chkBox.ToolTip + ',';
                        }
                    }
                }

                if (MailNotSendStatus != string.Empty)
                {
                    ds1 = (objCommon.FillDropDown("ACD_STUDENT", "(STUDNAME + '  #  ' + REGNO) collate DATABASE_DEFAULT  AS STUDNAME", "IDNO", "IDNO IN (" + MailNotSendStatus.TrimEnd(',') + ")", "IDNO"));
                }

                if (MailSendStatus != string.Empty)
                {
                    ds2 = (objCommon.FillDropDown("ACD_STUDENT", "(STUDNAME + '  #  ' + REGNO) collate DATABASE_DEFAULT AS STUDNAME", "IDNO", "IDNO IN (" + MailSendStatus.TrimEnd(',') + ")", "IDNO"));
                }
                string MailSendTo = string.Empty;
                string MailNotSendTo = string.Empty;


                if (MailNotSendStatus != string.Empty)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        //MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "," + "\n";
                        MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "\n" + ",";
                    }
                }


                if (MailSendStatus != string.Empty)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        MailSendTo += ds2.Tables[0].Rows[i]["STUDNAME"].ToString() + ",";
                    }
                }
                if (MailSendTo != string.Empty || MailNotSendTo != string.Empty)
                {
                    objCommon.DisplayMessage("Email Sent successfully", this.Page);
                    lblMailNorSendTo.Visible = true;
                    lblMailSendTo.Visible = true;
                    lblMailSendTo.Text = "Mail Send Student List - " + "\n" + MailSendTo.ToString().TrimEnd(',');
                    lblMailNorSendTo.Text = "Mail Not Send Student List - " + "\n" + MailNotSendTo.ToString().TrimEnd(',');
                    //ClearAllAfterSms();

                }
                else
                {
                    lblMailNorSendTo.Visible = false;
                    lblMailSendTo.Visible = false;
                }
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Email Subject and Message", this.Page);
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }

    }

    public string MessageBody(string studname, string message, string SessionName, string Sregno, string TClass, string TAttendance, string TPercentage, string useremail)
    {
        const string EmailTemplate = "<html><body>" +
                              "<div align=\"center\">" +
                              "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                               "<tr>" +
                               "<td>" + "</tr>" +
                               "<tr>" +
                              "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                              "</tr>" +
                              "</table>" +
                              "</div>" +
                              "</body></html>";
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>Greetings !!</h1>");
        mailBody.AppendFormat("Dear" + " " + "<b>" + studname + "," + "</b>");   //b
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<b>" + message + "</b>" + "<br/><br/>");
        mailBody.AppendFormat("<b>" + SessionName + "</b>" + "<br/>");//b
        mailBody.AppendFormat("<b>Enrollment Number: <b>" + Sregno + "</b>" + "<br/>");//b
        mailBody.AppendFormat("<b>Total Classes:" + TClass + "</b>" + "<br/>");//b
        mailBody.AppendFormat("<b>Total Attendance:" + TAttendance + "</b>" + "<br/>");//b
        mailBody.AppendFormat("<b>Percentage:" + TPercentage + "</b>" + "<br/><br/><br/>");//b
        mailBody.AppendFormat("This is an auto generated response to your email. Please do not reply to this mail.");
        mailBody.AppendFormat("<br /><br /><br /><br />Regards,<br />");   //bb
        mailBody.AppendFormat("Sarala Birla University, Ranchi<br /><br />");   //bb

        string Mailbody = mailBody.ToString();
        string nMailbody = EmailTemplate.Replace("#content", Mailbody);

        //string CCemail = CC_Email;

        //sendEmail(nMailbody, Email, "One-Time Password to Lock Marks", CCemail);
        if (useremail != string.Empty)
        {
            try
            {
                int status = 0;
                string email_type = string.Empty;
                string Link = string.Empty;
                int sendmail = 0;
                // DataSet ds = getModuleConfig();
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                //    Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                //}

                //if (email_type == "1" && email_type != "")
                //{
                //    status = sendEmail(message, useremail, txtSubject.Text);
                //}
                //else if (email_type == "2" && email_type != "")
                //{
                //    Task<int> task = Execute(message, useremail, txtSubject.Text);
                //    status = task.Result;
                //}
                //if (email_type == "3" && email_type != "")
                //{
                //    status = OutLook_Email(message, useremail, txtSubject.Text);
                //}
                status = objSendEmail.SendEmail(useremail, message, txtSubject.Text); //Calling Method
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        return nMailbody;
    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])));
        return ds;
    }
    #endregion

    #endregion Attendance Email Sending

    #region Send Sms/email To Parent

    private string SMSBalance()
    {
        string status = string.Empty, uid = string.Empty, password = string.Empty;
        try
        {
            uid = objCommon.LookUp("REFF", "SMSSVCID", "");
            password = objCommon.LookUp("REFF", "SMSSVCPWD", "");
            HttpStatusCode result = default(HttpStatusCode);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://www.SMSnMMS.co.in/sms.aspx?"));
            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";
            string postDate = "ID=" + uid;   //ghrce4116@gmail.com";
            postDate += "&";
            postDate += "Pwd=" + password; //  GHrcE@544819";
            byte[] byteArray = Encoding.UTF8.GetBytes(postDate);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;  //////
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse _webresponse = request.GetResponse();
            dataStream = _webresponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            status = reader.ReadToEnd();
            return status;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SendSmstoStudents.aspx.cs_btnSndSms_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return status;
    }

    #region Bind Listview

    protected void btnShowStudentlist_Click1(object sender, EventArgs e)
    {
        if (rdbFormat.SelectedValue == "1")
        {
            pnlfirst.Visible = true;
            pnlsecond.Visible = false;
            pnlthird.Visible = false;
            divFirstsms.Visible = true;
            divFirstsms.Visible = true;
            this.GetStudList();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

        }
        else if (rdbFormat.SelectedValue == "2")
        {
            pnlfirst.Visible = false;
            pnlsecond.Visible = true;
            pnlthird.Visible = false;
            divFirstsms.Visible = false;
            divattendancesecondsms.Visible = true;
            GetStudListAttPer();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rdbFormat.SelectedValue == "3")
        {
            pnlfirst.Visible = false;
            pnlsecond.Visible = false;
            pnlthird.Visible = true;
            divFirstsms.Visible = false;
            divattendancesecondsms.Visible = false;
            divexam.Visible = true;
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
            GetMarksList();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])


        }
        else if (rdbFormat.SelectedValue == "4")
        {
            pnlfirst.Visible = false;
            pnlsecond.Visible = false;
            pnlthird.Visible = false;
            pnltoday.Visible = true;
            divFirstsms.Visible = false;
            divattendancesecondsms.Visible = false;
            divexam.Visible = false;
            GetTodayAtt();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

        }
        else if (rdbFormat.SelectedValue == "5")
        {
            pnlfirst.Visible = false;
            pnlsecond.Visible = false;
            pnlthird.Visible = false;
            pnltoday.Visible = false;
            divattendancesecondsms.Visible = false;
            divexam.Visible = false;
            PnlStudentmeeting.Visible = true;
            GetParentsMeeting();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

        }
    }

    private void GetStudList()
    {
        // lblBalance.Text = SMSBalance();
        lblBalance.ForeColor = System.Drawing.Color.Red;

        DataSet ds = null;
        int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
        ds = objAttC.GetDateSlotsAbsentStud(session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlBranches.SelectedValue), Convert.ToInt32(ddlSemestr.SelectedValue), Convert.ToInt32(ddlSect.SelectedValue), Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToDateTime(txtFromDat.Text));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvfirstsms.DataSource = ds;
            lvfirstsms.DataBind();
            hftot.Value = lvfirstsms.Items.Count.ToString();
            pnlfirst.Visible = true;
            divFirstsms.Visible = true;
            lblAttStatus.Text = ds.Tables[0].Rows[0].Field<string>("STATUS_NAME");
            divAttStatus.Visible = true;
            lvfirstsms.Visible = true;
            TODAYATT();
            lstAttSecondsms.DataSource = null;
            lstAttSecondsms.DataBind();
            lstAttSecondsms.Visible = false;
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else
        {
            lvfirstsms.DataSource = null;
            lvfirstsms.DataBind();
            lvfirstsms.Visible = false;
            lstAttSecondsms.DataSource = null;
            lstAttSecondsms.DataBind();
            lstAttSecondsms.Visible = false;
            TODAYATT();
            divAttStatus.Visible = false;
            pnlfirst.Visible = false;
            divFirstsms.Visible = false;
<<<<<<< HEAD
            objCommon.DisplayMessage(this.updDetained, "Attendance Not Mark For Your Selection!", this.Page);
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
            objCommon.DisplayMessage(this.updDetained, "No Absentees Found For Your Selection!", this.Page);
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }

        foreach (ListViewDataItem dataitem in lvfirstsms.Items)
        {
            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            HiddenField hdnStatus = dataitem.FindControl("hdnStatus") as HiddenField;
            if (hdnStatus.Value == "1")
                cbRow.BackColor = System.Drawing.Color.Red;
        }

        #region cmt
        ////if (rdbFormat.SelectedIndex == 0)//Attendance First SMS 
        ////{
        //pnlsecond.Visible = false;
        //pnlthird.Visible = false;
        //pnlfirst.Visible = true;
        //divattendancesecondsms.Visible = false;
        //divIAMarks.Visible = false;
        //divFirstsms.Visible = true;
        //GetStudentDetailsForFirstSMS();
        ////}
        ////else if (rdbFormat.SelectedIndex == 1)//Attendance Second SMS
        ////{

        ////    pnlfirst.Visible = false;
        ////    pnlthird.Visible = false;
        ////    pnlsecond.Visible = true;
        ////    divIAMarks.Visible = false;
        ////    divFirstsms.Visible = false;
        ////    divattendancesecondsms.Visible = true;
        ////    GetStudentAttendanceList();
        ////}
        ////else            //SMS for IA Format
        ////{
        ////    pnlfirst.Visible = false;
        ////    pnlsecond.Visible = false;
        ////    pnlthird.Visible = true;
        ////    divFirstsms.Visible = false;
        ////    divattendancesecondsms.Visible = false;
        ////    divIAMarks.Visible = true;
        ////    GetStudentIAMarks();
        ////}
        #endregion
    }

    private void GetStudListAttPer()
    {
        // lblBalance.Text = SMSBalance();
        lblBalance.ForeColor = System.Drawing.Color.Red;

        DataSet ds = null;
        int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
        ds = objAttC.GetSubjectWiseAttPer(session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlBranches.SelectedValue), Convert.ToInt32(ddlSemestr.SelectedValue), Convert.ToInt32(ddlSect.SelectedValue), Convert.ToDateTime(txtFromDat.Text), Convert.ToDateTime(txtAttToDate.Text));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lstAttSecondsms.Visible = true;
            lstAttSecondsms.DataSource = ds;
            lstAttSecondsms.DataBind();
            hftot.Value = lstAttSecondsms.Items.Count.ToString();
            pnlfirst.Visible = false;
            divFirstsms.Visible = false;

            //lblAttStatus.Text = ds.Tables[0].Rows[0].Field<string>("STATUS_NAME");

            divattendancesecondsms.Visible = true;
            pnlsecond.Visible = true;
            TODAYATT();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else
        {
            lstAttSecondsms.Visible = false;
            lstAttSecondsms.DataSource = null;
            lstAttSecondsms.DataBind();
            divattendancesecondsms.Visible = false;
            pnlsecond.Visible = false;
            TODAYATT();
<<<<<<< HEAD

=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

            pnlfirst.Visible = false;
            divFirstsms.Visible = false;
            objCommon.DisplayMessage(this.updDetained, "Record Not Found For Your Selection!", this.Page);
        }
        divAttStatus.Visible = false;
    }

    private void GetStudentDetailsForFirstSMS()
    {
        string examname = "S1";
        int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
        // DataSet ds = excol.GetStudentIAMarksForSMS(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), examname);
        DataSet ds = excol.GetStudentIAMarksForSMS(session, Convert.ToInt32(1), Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlBranches.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), examname);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvfirstsms.DataSource = ds;
            lvfirstsms.DataBind();
            hftot.Value = lvfirstsms.Items.Count.ToString();
            TODAYATT();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else
        {
            objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
            TODAYATT();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
    }

    private void GetMarksList()
    {
        //string examname = Convert.ToString(ddlexam.SelectedValue);
        //int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
        //DataSet ds = excol.GetStudentCATMarksForSMS(session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlBranches.SelectedValue), Convert.ToInt32(ddlSemestr.SelectedValue), Convert.ToInt32(ddlSect.SelectedValue), examname);


        string examname = Convert.ToString(ddlexam.SelectedValue);

        string sp_procedure = "PKG_ACD_FOR_STUDENT_CATMARKS_FOR_SMS";
        string sp_parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SECTIONNO,@P_MARK";
        string sp_callValues = "" + (ddlsessionexam.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + ddlsemexam.SelectedValue + "," + ddlsectionexam.SelectedValue + "," + ddlexamnew.SelectedValue + "";
        //DataSet dsMainExamnew = null;
        DataSet ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvmarks.DataSource = ds;
            lvmarks.DataBind();
            pnlthird.Visible = true;
            hftot.Value = lvmarks.Items.Count.ToString();
            //TODAYATT();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else
        {
            objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
            //TODAYATT();
            HiddenItemForPm();
            HiddenItem();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            HiddenItemSMS();
        }

    }

    private void GetTodayAtt()
    {
        try
        {

            DataSet dstoday = objAttC.GetTodayAtt(Convert.ToInt32(ddlSessn.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToDateTime(txtFromDat.Text), Convert.ToDateTime(txtAttToDate.Text));

            if (dstoday != null && dstoday.Tables.Count > 0 && dstoday.Tables[0].Rows.Count > 0)
            {

                lvTodayAtt.DataSource = dstoday;
                lvTodayAtt.DataBind();
                hftot.Value = lvTodayAtt.Items.Count.ToString();
<<<<<<< HEAD
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                //  TODAYATT();
            }
            else
            {
                objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
<<<<<<< HEAD
=======
                HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                //Added By Jay T. On dated 23022024
                HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                //  TODAYATT();
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void GetParentsMeeting()
    {
        try
        {

            if (ddlcollege.SelectedValue != "0")
            {
                DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "STUDNAME,REGNO", "EMAILID,STUDENTMOBILE,IDNO,FATHERMOBILE,FATHER_EMAIL", "COLLEGE_ID=" + ddlcollege.SelectedValue + " AND BRANCHNO=" + ddlBranches.SelectedValue + "  and DEGREENO=" + ddlDeg.SelectedValue + " and SEMESTERNO=" + ddlSemPm.SelectedValue + " and ISNULL(CAN,0)=0 AND ISNULL(admcan,0)=0", "");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    lvStudentMeeting.DataSource = ds;
                    lvStudentMeeting.DataBind();
                    hftot.Value = lvStudentMeeting.Items.Count.ToString();
                    PnlStudentmeeting.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                    lvStudentMeeting.DataSource = null;
                    lvStudentMeeting.DataBind();
                    PnlStudentmeeting.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(updCollege, "Record Not Found For Your Selection!", this.Page);
                lvStudentMeeting.DataSource = null;
                lvStudentMeeting.DataBind();
                PnlStudentmeeting.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void TODAYATT()
    {
        HiddenItemSMS();
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            if (rdbFormat.SelectedValue == "4")
            {

                btnWhatsAppAtt.Visible = true;
            }
        }
        else
        {
            btnWhatsAppAtt.Visible = false;
            HiddenItem();
        }
    }

    #endregion

    #region SMS

    protected void btnSubmitSmsEmail_Click(object sender, EventArgs e)
    {
        StudentAttendanceController dsStudentDetails = new StudentAttendanceController();
        int MSGTYPE = 0;
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();
<<<<<<< HEAD
=======
        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
        //Added By Jay T. On dated 23022024
        HiddenItemFeesNotPaid();

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        string slotname = objCommon.LookUp("ACD_TIME_SLOT", "TIMEFROM+' - '+TIMETO AS SLOTNAME", "SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue));
        string SubOtp = objCommon.LookUp("REFF", "SUBJECT_OTP", "");

        #region SMS - First Hour Absent
        if (rdbFormat.SelectedValue == "1")//rdbFormat.SelectedIndex == 0)
        {
            MSGTYPE = 1;//Attendance First SMS
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            string message = string.Empty;
            string template = string.Empty;
            int count = 0;
            foreach (ListViewDataItem dataitem in lvfirstsms.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send SMS", this);
                return;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
            else
            {
                foreach (ListViewDataItem item in lvfirstsms.Items)
                {
                    try
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblParMobile = item.FindControl("lblParMobile") as Label;
                        HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                        HiddenField hdnStuName = item.FindControl("hdnStuName") as HiddenField;
                        HiddenField hdnDeptname = item.FindControl("hdnDeptname") as HiddenField;

                        if (chek.Checked)
                        {
                            // string message = "Dear Parent, Greetings from CRESCENT ! Your Ward " + hdnStuName.Value + " is absent for the time slot " + slotname + " on " + Convert.ToDateTime(txtFromDat.Text).ToString("dd/MM/yyyy") + "." + " -" + "CRESEN" + "";
                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + hdnidno.Value);
                            string mobile = "91" + ToMobileNo;
                            if (ToMobileNo != string.Empty)
                            {
                                if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                                {
                                    string templatename = "First Hour Absent";
                                    DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                        TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                        HiddenItemForPm();
                                        HiddenItem();
                                        HiddenItemSMS();
                                        HiddenItemSMSmark();
                                        return;
                                    }
                                    message = TEMPLATE;
                                    message = message.Replace("{#var#}", hdnStuName.Value);
                                    message = message.Replace("{#var1#}", slotname);
                                    message = message.Replace("{#var2#}", Convert.ToDateTime(txtFromDat.Text).ToString("dd/MM/yyyy"));

                                    // Create a StringBuilder and append the template
                                    StringBuilder stringBuilder = new StringBuilder();
                                    stringBuilder.Append(message);
                                    // Get the final message string
                                    template = stringBuilder.ToString();
                                    // SendSMS_today(lblParMobile.Text.Trim(), template, TemplateID);
                                }
                            }
                            if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                            {
                                CustomStatus cs = (CustomStatus)objAttC.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), MSGTYPE, Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToDateTime(txtFromDat.Text));
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    this.SendSMS(lblParMobile.Text.Trim(), template, TemplateID);
                                    // this.SendSMS(lblParMobile.Text, template, "1707165545797594293");

                                    objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);
                                    HiddenItemForPm();
                                    HiddenItem();
                                    HiddenItemSMS();
                                    HiddenItemSMSmark();
<<<<<<< HEAD
=======
                                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                                    //Added By Jay T. On dated 23022024
                                    HiddenItemFeesNotPaid();

                                    //Added By Sakshi M on 20012024 to maintain log 
                                    string IPaddress = Session["ipAddress"].ToString();
                                    ////CustomStatus cs1 = (CustomStatus)objAttC.INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "First Hour Absent (Parent)", lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), 2, "", IPaddress, Convert.ToInt32(Session["OrgId"]));
                                    CustomStatus cs1 = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "First Hour Absent (Parent)", lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), 2, "", IPaddress, Convert.ToInt32(Session["OrgId"]));

>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updDetained, "Error Occured..!!", this.Page);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Convert.ToBoolean(Session["error"]) == true)
                            objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                        else
                        {
                            objCommon.DisplayMessage(this.updDetained, "Server UnAvailable", this.Page);
                        }
                    }
                }
            }
            this.GetStudList();
            TODAYATT();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        #endregion

        #region SMS- Attendance Percentage (Subject Wise)
        else if (rdbFormat.SelectedValue == "2")//Attendance Second SMS
        {
            MSGTYPE = 2;//Attendance Second SMS    
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            string message = string.Empty;
            string template = string.Empty;
            string firstvar = string.Empty;
            string secondvar = string.Empty;
            string thirdvar = string.Empty;
            string fourtvar = string.Empty;

            int count = 0;
            foreach (ListViewDataItem dataitem in lstAttSecondsms.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send SMS", this);
                return;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
            else
            {
                foreach (ListViewDataItem item in lstAttSecondsms.Items)
                {
                    try
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblParMobile = item.FindControl("lblParMobile") as Label;
                        HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                        HiddenField hdnStuName = item.FindControl("hdnStuName") as HiddenField;
                        HiddenField hdnCode = item.FindControl("hdnCode") as HiddenField;
                        Label lblRegNo = item.FindControl("lblEnrollmentNo") as Label;


                        if (chek.Checked)
                        {
                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + hdnidno.Value);
                            string mobile = "91" + ToMobileNo;
                            if (ToMobileNo != string.Empty)
                            {
                                if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                                {
                                    string inputString = hdnCode.Value.ToString(); // Replace with your own string value


                                    List<string> stringParts = new List<string>();

                                    if (inputString.Length > 120)
                                    {
                                        inputString = inputString.Substring(0, 120);
                                        int count_string = inputString.Length;
                                    }

                                    if (inputString.Length <= 120)
                                    {

                                        if (inputString.Length > 0)
                                        {
                                            int onefourthcountstring = (inputString.Length / 4);
                                            int remainder = inputString.Length % 4;
                                            int chunkSize = onefourthcountstring;
                                            int numChunks = (int)Math.Ceiling((double)inputString.Length / chunkSize);
                                            string[] chunks = new string[4];
                                            if (inputString.Length % 4 == 0)
                                            {
                                                char[] array1 = inputString.Substring(0, Math.Min(chunkSize, inputString.Length)).ToCharArray();
                                                char[] array2 = inputString.Substring(chunkSize, Math.Min(chunkSize, inputString.Length - chunkSize)).ToCharArray();
                                                char[] array3 = inputString.Substring(chunkSize * 2, Math.Min(chunkSize, inputString.Length - chunkSize * 2)).ToCharArray();
                                                char[] array4 = inputString.Substring(chunkSize * 3, Math.Min(chunkSize + remainder, inputString.Length - chunkSize * 3)).ToCharArray();

                                                firstvar = getString(array1);
                                                secondvar = getString(array2);
                                                thirdvar = getString(array3);
                                                fourtvar = getString(array4);
                                            }
                                            else
                                            {

                                                char[] array1 = inputString.Substring(0, Math.Min(chunkSize, inputString.Length)).ToCharArray();
                                                char[] array2 = inputString.Substring(chunkSize, Math.Min(chunkSize, inputString.Length - chunkSize)).ToCharArray();
                                                char[] array3 = inputString.Substring(chunkSize * 2, Math.Min(chunkSize, inputString.Length - chunkSize * 2)).ToCharArray();
                                                char[] array4 = inputString.Substring(chunkSize * 3, Math.Min(chunkSize + remainder, inputString.Length - chunkSize * 3)).ToCharArray();

                                                firstvar = getString(array1);
                                                secondvar = getString(array2);
                                                thirdvar = getString(array3);
                                                fourtvar = getString(array4);
                                            }

                                        }
                                    }
                                    string templatename = "Attendance Percentage Subject Wise";
                                    DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                        TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                        HiddenItemForPm();
                                        HiddenItem();
                                        HiddenItemSMS();
                                        HiddenItemSMSmark();
                                        return;
                                    }
                                    message = TEMPLATE;
                                    message = message.Replace("{#var#}", hdnStuName.Value);
                                    message = message.Replace("{#var1#}", Convert.ToDateTime(txtAttToDate.Text).ToString("dd/MM/yyyy"));
                                    message = message.Replace("{#var2#}", firstvar);
                                    message = message.Replace("{#var3#}", secondvar);
                                    message = message.Replace("{#var4#}", thirdvar);
                                    message = message.Replace("{#var5#}", fourtvar);

                                    // Create a StringBuilder and append the template
                                    StringBuilder stringBuilder = new StringBuilder();
                                    stringBuilder.Append(message);
                                    // Get the final message string
                                    template = stringBuilder.ToString();
                                }
                            }
                            // string message =  is as follows: " + hdnCode.Value.ToString() + "." + "\n" + "Note : The minimum attendance requirement to attend semester end examination in a course is 75%. In case of less than 75% in courses, you are requested to contact Class Advisor / HoD/Dean for further course of action.";


                            if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                            {
                                CustomStatus cs = (CustomStatus)objAttC.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), MSGTYPE, Convert.ToInt32(0), Convert.ToDateTime(txtFromDat.Text));
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    this.SendSMS(lblParMobile.Text.Trim(), template, TemplateID);
                                    objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);
                                    HiddenItemForPm();
                                    HiddenItem();
                                    HiddenItemSMS();
                                    HiddenItemSMSmark();
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updDetained, "Error Occured..!!", this.Page);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Convert.ToBoolean(Session["error"]) == true)
                            objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                        else
                        {
                            objCommon.DisplayMessage(this.updDetained, "Server UnAvailable", this.Page);
                        }
                    }
                }
            }
            this.GetStudListAttPer();
            TODAYATT();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        #endregion

        #region SMS - CAT Marks
        else if (rdbFormat.SelectedValue == "3")
        {
            MSGTYPE = 3;//CAT EXAM SMS
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            string message = string.Empty;
            string template = string.Empty;
            string firstvar = string.Empty;
            string secondvar = string.Empty;
            string thirdvar = string.Empty;
            string fourtvar = string.Empty;
            foreach (ListViewDataItem item in lvmarks.Items)
            {
                try
                {
                    CheckBox chek = item.FindControl("cbRow") as CheckBox;
                    Label lblParMobile = item.FindControl("lblParMobile") as Label;
                    Label lblname = item.FindControl("lblname") as Label;
                    HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                    HiddenField hdnenroll = item.FindControl("hdnenroll") as HiddenField;
                    HiddenField hdnsemesterno = item.FindControl("hdnsemesterno") as HiddenField;
                    HiddenField hdnshortbname = item.FindControl("hdnshortbname") as HiddenField;

                    Label lblIAMarks = item.FindControl("lblIAMarks") as Label;

                    if (chek.Checked)
                    {
                        //string message = "Dear Parents, Kindly note the " + ciemark + " marks of your ward " + lblname.Text + ",(" + hdnenroll.Value.ToString() + ")" + "of Sem " + hdnsemesterno.Value.ToString() + "," + lblIAMarks.Text + " , From Registrar - JSS ST University(SJCE), Mysuru.";

                        // string message = "Dear Parent, Your Ward \n" + lblname.Text + "," + ddlexam.SelectedItem.Text + " " + "Exam Marks are :\n" + lblIAMarks.Text + "\n From: HOD/" + hdnshortbname.Value + " (SVCE)" + "(*Max Mark:50,Min Pass:25,Absent - A,Copycase - UFM)";
                        // string message = "Dear Parent, Kindly be reminded that, maintaining 85% attendance in every subject is mandatory. Please advise your ward to maintain above 85% attendance in every subject to avoid losing a year. From: Registrar, SVCE, Sriperumbudur.";

                        string templatename = "CAT Marks for Parents";
                        DataSet ds = objUC.GetSMSTemplate(0, templatename);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                            TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemSMS();
                            HiddenItemSMSmark();
                            return;
                        }
                        message = TEMPLATE;
                        message = message.Replace("{#var#}", ddlexamnew.SelectedItem.Text);
                        message = message.Replace("{#var1#}", lblname.Text);
                        message = message.Replace("{#var2#}", hdnenroll.Value);
                        message = message.Replace("{#var3#}", lblIAMarks.Text);
                        // message = message.Replace("{#var4#}", thirdvar);
                        // message = message.Replace("{#var5#}", fourtvar);

                        // Create a StringBuilder and append the template
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append(message);
                        // Get the final message string
                        template = stringBuilder.ToString();


                        //string message = "Dear Parents,\n Greetings from BSA Crescent Institute of Science and Technology! \n The " + ddlexamnew.SelectedItem.Text + "" + " Marks (Out of 100) of Your Ward " + lblname.Text + " - " + "" + hdnenroll.Value + " " + "  \n for the courses of this current semester is as follows :" + "\n" + lblIAMarks.Text + " \n Regards COE";


                        if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                        {
                            CustomStatus cs = (CustomStatus)excol.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), MSGTYPE);


                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                //this.SendSMS(lblParMobile.Text, message);

                                //objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);


                                this.SendSMS(lblParMobile.Text.Trim(), template, TemplateID);
                                objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);
                                HiddenItemForPm();
                                HiddenItem();
                                HiddenItemSMS();
                                HiddenItemSMSmark();

                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updDetained, "Error Occured..!!", this.Page);
                                HiddenItemForPm();
                                HiddenItem();
                                HiddenItemSMS();

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                            HiddenItemForPm();
                            HiddenItem();
                            HiddenItemSMS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "Server UnAvailable", this.Page);

                    }
                }
            }
            this.GetMarksList();
            TODAYATT();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        #endregion

        #region SMS- Todays Students Attendance list
        else if (rdbFormat.SelectedValue == "4")
        {
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            MSGTYPE = 3;//CAT EXAM SMS
            int count = 0;
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            foreach (ListViewDataItem dataitem in lvTodayAtt.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send SMS", this);
                return;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
                HiddenItemSMSmark();
            }
            else
            {

                string MailSendStatus = string.Empty;
                string MailNotSendStatus = string.Empty;
                DataSet ds1 = null;
                DataSet ds2 = null;
                try
                {
                    foreach (ListViewDataItem item in lvTodayAtt.Items)
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblParMobile = item.FindControl("lblParMobiletoday") as Label;
                        Label lblname = item.FindControl("lblnametoday") as Label;
                        Label lblAtt = item.FindControl("lbltodayatt") as Label;
                        Label lblregno = item.FindControl("lblregno") as Label;
                        HiddenField hdnDEPT = item.FindControl("hdnDEPT") as HiddenField;
                        HiddenField hdnidno1 = item.FindControl("hdnidno") as HiddenField;

                        string Att = lblAtt.Text;
                        string Dept = hdnDEPT.Value;


                        if (chek.Checked)
                        {

                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + hdnidno1.Value);
                            string mobile = "91" + ToMobileNo;
                            if (ToMobileNo != string.Empty)
                            {
                                if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                                {

                                    string templatename = "Today Attandance";
                                    DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                        TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                        HiddenItemForPm();
                                        HiddenItem();
                                        HiddenItemSMS();
                                        HiddenItemSMSmark();
                                        return;
                                    }
                                    string message = TEMPLATE;
                                    message = message.Replace("{#var#}", Att);
                                    message = message.Replace("{#var1#}", Dept);

                                    // Create a StringBuilder and append the template
                                    StringBuilder stringBuilder = new StringBuilder();
                                    stringBuilder.Append(message);
                                    // Get the final message string
                                    string template = stringBuilder.ToString();
                                    SendSMS_today(lblParMobile.Text.Trim(), template, TemplateID);
                                    //  CustomStatus cs = (CustomStatus)excol.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno1.Value), MSGTYPE);
                                    MailSendStatus += hdnidno1.Value + ',';
<<<<<<< HEAD
=======
                                    //Added By Sakshi M on 20012024 to maintain log 
                                    string IPaddress = Session["ipAddress"].ToString();
                                    CustomStatus cs1 = (CustomStatus)INSERTBULKEMAILSMS_LOG(Convert.ToInt32(Session["userno"]), "Todays Students Attendance list", lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno1.Value), 2, "", IPaddress, Convert.ToInt32(Session["OrgId"]));
>>>>>>> c665535e ([BUGFIX][53298][Maintain_Log_email])

                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                                }
                            }
                            else
                            {
                                MailNotSendStatus += hdnidno1.Value + ',';
                            }
                        }
                    }
                    if (MailNotSendStatus != string.Empty)
                    {
                        ds1 = (objCommon.FillDropDown("ACD_STUDENT", "STUDNAME  ", "IDNO", "IDNO IN (" + MailNotSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }

                    if (MailSendStatus != string.Empty)
                    {
                        ds2 = (objCommon.FillDropDown("ACD_STUDENT", "STUDNAME", "IDNO", "IDNO IN (" + MailSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }
                    string MailSendTo = string.Empty;
                    string MailNotSendTo = string.Empty;


                    if (MailNotSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "\n" + ",";
                        }
                    }

                    if (MailSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            MailSendTo += ds2.Tables[0].Rows[i]["STUDNAME"].ToString() + ",";
                        }
                    }
                    if (MailSendTo != string.Empty || MailNotSendTo != string.Empty)
                    {
                        objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);
                        txtSmsSend.Visible = true;
                        txtSmsNotSend.Visible = true;
                        txtSmsSend.Text = "SMS Message Send Student List - " + "\n" + MailSendTo.ToString().TrimEnd(',');
                        txtSmsNotSend.Text = "SMS Message Not Send Student List - " + "\n" + MailNotSendTo.ToString().TrimEnd(',');
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        HiddenItemSMSmark();
                    }
                    else
                    {
                        lblMailNorSendTo.Visible = false;
                        lblMailSendTo.Visible = false;
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                        HiddenItemSMSmark();
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "Server UnAvailable", this.Page);
                    }
                }
            }
            GetTodayAtt();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }

        #endregion

        #region SMS- Parent Teacher Meeting
        else if (rdbFormat.SelectedValue == "5")
        {
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
            MSGTYPE = 3;//CAT EXAM SMS
            int count = 0;
            string TemplateID = string.Empty;
            string TEMPLATE = string.Empty;
            foreach (ListViewDataItem dataitem in lvStudentMeeting.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send SMS", this);
                return;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
                HiddenItemSMSmark();
            }
            else
            {
                if (txtMdate.Text == "" && txtMTime.Text == "")
                {
                    objCommon.DisplayMessage(this.updDetained, "Please Enter Meeting Date and Time  For Send SMS", this);
                    return;
                }
                else if (txtMdate.Text == "")
                {
                    objCommon.DisplayMessage(this.updDetained, "Please Enter Meeting Date For Send SMS", this);
                    return;
                }
                else if (txtMTime.Text == "")
                {
                    objCommon.DisplayMessage(this.updDetained, "Please Enter Meeting Time For Send SMS", this);
                    return;
                }
                string MailSendStatus = string.Empty;
                string MailNotSendStatus = string.Empty;
                DataSet ds1 = null;
                DataSet ds2 = null;
                try
                {
                    foreach (ListViewDataItem item in lvStudentMeeting.Items)
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblParMobile = item.FindControl("lblParMobile") as Label;
                        HiddenField hdnidno1 = item.FindControl("hdnidno") as HiddenField;
                        string Date = txtMdate.Text;
                        string Time = txtMTime.Text;
                        if (chek.Checked)
                        {

                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + hdnidno1.Value);
                            string mobile = "91" + ToMobileNo;
                            if (ToMobileNo != string.Empty)
                            {
                                if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                                {

                                    string templatename = "Parent Teacher Meeting";
                                    DataSet ds = objUC.GetSMSTemplate(0, templatename);
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        TEMPLATE = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                                        TemplateID = ds.Tables[0].Rows[0]["TEM_ID"].ToString();
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updDetained, "SMS Template Not Found For Your Selection!", this.Page);
                                        HiddenItemForPm();
                                        HiddenItem();
                                        HiddenItemSMS();
                                        HiddenItemSMSmark();
                                        return;
                                    }
                                    string message = TEMPLATE;
                                    message = message.Replace("{#var#}", Date);
                                    message = message.Replace("{#var1#}", Time);

                                    // Create a StringBuilder and append the template
                                    StringBuilder stringBuilder = new StringBuilder();
                                    stringBuilder.Append(message);
                                    // Get the final message string
                                    string template = stringBuilder.ToString();
                                    SendSMS_today(lblParMobile.Text.Trim(), template, TemplateID);
                                    CustomStatus cs = (CustomStatus)excol.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno1.Value), MSGTYPE);
                                    MailSendStatus += hdnidno1.Value + ',';

                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                                }
                            }
                            else
                            {
                                MailNotSendStatus += hdnidno1.Value + ',';
                            }
                        }
                    }
                    if (MailNotSendStatus != string.Empty)
                    {
                        ds1 = (objCommon.FillDropDown("ACD_STUDENT", "STUDNAME  ", "IDNO", "IDNO IN (" + MailNotSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }

                    if (MailSendStatus != string.Empty)
                    {
                        ds2 = (objCommon.FillDropDown("ACD_STUDENT", "STUDNAME", "IDNO", "IDNO IN (" + MailSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }
                    string MailSendTo = string.Empty;
                    string MailNotSendTo = string.Empty;


                    if (MailNotSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "\n" + ",";
                        }
                    }

                    if (MailSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            MailSendTo += ds2.Tables[0].Rows[i]["STUDNAME"].ToString() + ",";
                        }
                    }
                    if (MailSendTo != string.Empty || MailNotSendTo != string.Empty)
                    {
                        objCommon.DisplayUserMessage(this.updDetained, "SMS Successfully Send To Parent(s)", this.Page);
                        txtSmsSend.Visible = true;
                        txtSmsNotSend.Visible = true;
                        txtSmsSend.Text = "SMS Message Send Student List - " + "\n" + MailSendTo.ToString().TrimEnd(',');
                        txtSmsNotSend.Text = "SMS Message Not Send Student List - " + "\n" + MailNotSendTo.ToString().TrimEnd(',');
                        PnlStudentmeeting.Visible = true;
                        GetParentsMeeting();
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                    }
                    else
                    {
                        PnlStudentmeeting.Visible = true;
                        GetParentsMeeting();
                        lblMailNorSendTo.Visible = false;
                        lblMailSendTo.Visible = false;
                        HiddenItemForPm();
                        HiddenItem();
                        HiddenItemSMS();
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "Server UnAvailable", this.Page);
                    }
                }
            }
            PnlStudentmeeting.Visible = true;
            GetParentsMeeting();
            clearcontrols();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        #endregion
    }

    #endregion

    #region Cancel
    protected void butCancelSmsEmail_Click(object sender, EventArgs e)
    {
        this.clearcontrols();
        txtSmsSend.Text = string.Empty;
        txtSmsNotSend.Text = string.Empty;
        txtSmsSend.Visible = false;
<<<<<<< HEAD
=======
        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
        //Added By Jay T. On dated 23022024
        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        txtSmsNotSend.Visible = false;
    }

    public void clearcontrols()
    {
        ddlSessn.SelectedIndex = 0;
        ddlcollege.SelectedIndex = 0;
        ddlDeg.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranches.SelectedIndex = 0;
        ddlSemestr.SelectedIndex = 0;
        ddlSect.SelectedIndex = 0;
        txtFromDat.Text = string.Empty;
        txtAttToDate.Text = string.Empty;
        ddlSemPm.SelectedIndex = 0;
        ddlIAMarks.SelectedIndex = 0;
        ddlexam.SelectedIndex = 0;
        txtTotStud.Text = "0";
        divFirstsms.Visible = false;
        divattendancesecondsms.Visible = false;
        divIAMarks.Visible = true;
        divscheme.Visible = false;
        divexamname.Visible = false;
        divAttStatus.Visible = false;
        txtMdate.Text = string.Empty;
        txtMTime.Text = string.Empty;
        divslots.Visible = false;
        lvStudentMeeting.DataSource = null;
        lvStudentMeeting.DataBind();
        PnlStudentmeeting.Visible = false;
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();
    }

    #endregion

    #region RadioButton

    protected void rdbFormat_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (rdbFormat.SelectedValue == "1")
        {
            txtFromDat.Text = string.Empty;
            txtAttToDate.Text = string.Empty;
            divFrmDate.Visible = true;
            lblFrmDate.Visible = true;
            lblFrmDate1.Visible = false;
            divtodate.Visible = false;
            divexam.Visible = false;
            //divslots.Visible = false;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));

            divatt.Visible = true;   //added by parfull on 18052023
            dvexam.Visible = false;   //added by parfull on 18052023

            ddlSlot.SelectedIndex = 0;
            DIVDEG.Visible = true;
            DivSemPM.Visible = false;
            DivDate.Visible = false;
            DivTime.Visible = false;
            DIVBRANCH.Visible = true;
            DIVSEM.Visible = true;
            DIVSECTION.Visible = true;
            this.clearcontrols();
            btnEmailSms.Visible = true;
            lvStudentMeeting.DataSource = null;
            lvStudentMeeting.DataBind();
            PnlStudentmeeting.Visible = false;
            // btnAttSms.Visible = false;
            btnSubmitSmsEmail.Visible = true;
            btnWhatsAppAtt.Visible = false;
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {
                    btnWhatsAppAtt.Visible = true;
                }
                //  HiddenItemForPm();
            }
            else
            {
                if (Convert.ToInt32(Session["OrgId"]) == 7)
                {
                    btnWhatsapp.Visible = true;
                    //btnWhatsapp.Enabled = true;
                    HiddenItem();
                    HiddenItemSMS();
                    //   HiddenItemForPm();
                    btnWhatsAppAtt.Visible = false;
<<<<<<< HEAD
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                    HiddenItemSMS();
                    //  HiddenItemForPm();
<<<<<<< HEAD
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                }
            }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rdbFormat.SelectedValue == "2")
        {
            txtFromDat.Text = string.Empty;
            txtAttToDate.Text = string.Empty;
            divFrmDate.Visible = true;
            lblFrmDate.Visible = false;
            lblFrmDate1.Visible = true;
            divtodate.Visible = true;
            divexam.Visible = false;
            DIVDEG.Visible = true;

            divatt.Visible = true;   //added by parfull on 18052023
            dvexam.Visible = false;   //added by parfull on 18052023

            DIVBRANCH.Visible = true;
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            DIVSEM.Visible = true;
            btnSubmitSmsEmail.Visible = true;
            DIVSECTION.Visible = true;
            DivDate.Visible = false;
            DivTime.Visible = false;
            this.clearcontrols();
            lvStudentMeeting.DataSource = null;
            lvStudentMeeting.DataBind();
            PnlStudentmeeting.Visible = false;
            divslots.Visible = false;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.SelectedIndex = 0;
            btnEmailSms.Visible = true;
            DivSemPM.Visible = false;
            //  btnAttSms.Visible = false;
            HiddenItemForPm();
            btnWhatsAppAtt.Visible = false;
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
                if (Convert.ToInt32(Session["OrgId"]) == 7)
                {
                    btnWhatsapp.Visible = true;
                    //btnWhatsapp.Enabled = true;
                    HiddenItem();
                    HiddenItemSMS();
                    btnWhatsAppAtt.Visible = false;
<<<<<<< HEAD
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
<<<<<<< HEAD
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    HiddenItemSMS();
                }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rdbFormat.SelectedValue == "3")
        {
            txtFromDat.Text = string.Empty;
            txtAttToDate.Text = string.Empty;
            divFrmDate.Visible = false;
            lblFrmDate.Visible = false;
            lblFrmDate1.Visible = false;
            divtodate.Visible = false;
            btnSubmitSmsEmail.Visible = true;
            //  divexam.Visible = true;

            divatt.Visible = false;   //added by parfull on 18052023
            dvexam.Visible = true;   //added by parfull on 18052023

            HiddenItemForPm();
            DIVDEG.Visible = true;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.SelectedIndex = 0;
            lvStudentMeeting.DataSource = null;
            lvStudentMeeting.DataBind();
            PnlStudentmeeting.Visible = false;
            DIVBRANCH.Visible = true;
            DivDate.Visible = false;
            DivTime.Visible = false;
            DIVSEM.Visible = true;
            DIVSECTION.Visible = true;
            this.clearcontrols();
            btnEmailSms.Visible = true;
            DivSemPM.Visible = false;
            // btnAttSms.Visible = false;
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            btnWhatsAppAtt.Visible = false;
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
                if (Convert.ToInt32(Session["OrgId"]) == 7)
                {
                    btnWhatsapp.Visible = true;
                    //btnWhatsapp.Enabled = true;
                    HiddenItem();
                    HiddenItemSMS();
                    btnWhatsAppAtt.Visible = false;
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                    HiddenItemSMS();
                }
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

            //added by prafull 18052023

            objCommon.FillDropDownList(ddlclg, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

        }
        else if (rdbFormat.SelectedValue == "4")
        {
            txtFromDat.Text = string.Empty;
            txtAttToDate.Text = string.Empty;
            divFrmDate.Visible = true;
            lblFrmDate.Visible = false;
            lblFrmDate1.Visible = true;
            divtodate.Visible = true;
            divexam.Visible = false;

            dvexam.Visible = false;          //added by parfull on 18052023
            divatt.Visible = true;         //added by parfull on 18052023

            DIVDEG.Visible = false;
            DIVBRANCH.Visible = false;
            DIVSEM.Visible = false;
            DivDate.Visible = false;
            lvStudentMeeting.DataSource = null;
            lvStudentMeeting.DataBind();
            PnlStudentmeeting.Visible = false;
            DivTime.Visible = false;
            btnSubmitSmsEmail.Visible = true;
            DIVSECTION.Visible = false;
            btnEmailSms.Visible = false;
            //  btnAttSms.Visible = true;
            divslots.Visible = false;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.SelectedIndex = 0;
            btnWhatsAppAtt.Visible = true;
            this.clearcontrols();
            DivSemPM.Visible = false;
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        }
        else if (rdbFormat.SelectedValue == "5")
        {
            txtFromDat.Text = string.Empty;
            txtAttToDate.Text = string.Empty;
            divFrmDate.Visible = false;
            lblFrmDate.Visible = false;
            lblFrmDate1.Visible = false;
            divtodate.Visible = false;
            divexam.Visible = false;
            DIVDEG.Visible = true;

            dvexam.Visible = false;          //added by parfull on 18052023
            divatt.Visible = true;         //added by parfull on 18052023
<<<<<<< HEAD

=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])

            DIVBRANCH.Visible = true;
            DIVSEM.Visible = false;
            DivSemPM.Visible = true;
            DivDate.Visible = true;
            DivTime.Visible = true;
            HiddenItemForPm();
            btnSubmitSmsEmail.Visible = true;
            DIVSECTION.Visible = false;
            this.clearcontrols();
            divslots.Visible = false;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.SelectedIndex = 0;
            btnEmailSms.Visible = false;
            //  btnAttSms.Visible = false;
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
            HiddenItemSMSmark();
            btnWhatsAppAtt.Visible = false;
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
                if (Convert.ToInt32(Session["OrgId"]) == 7)
                {
                    btnWhatsapp.Visible = true;
                    //btnWhatsapp.Enabled = true;
                    HiddenItem();
                    HiddenItemSMS();
                    btnWhatsAppAtt.Visible = false;
<<<<<<< HEAD
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
        }
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            if (rdbFormat.SelectedValue == "4")
            {

                btnWhatsAppAtt.Visible = true;
            }
        }
        else
            if (Convert.ToInt32(Session["OrgId"]) == 7)
            {
                btnWhatsapp.Visible = true;
                //btnWhatsapp.Enabled = true;
                HiddenItem();
                btnWhatsAppAtt.Visible = false;
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }

        lvfirstsms.DataSource = null;
        lvfirstsms.DataBind();
<<<<<<< HEAD
=======
        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
        //Added By Jay T. On dated 23022024
        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        lvfirstsms.Visible = false;
        lstAttSecondsms.DataSource = null;
        lstAttSecondsms.DataBind();
        lvTodayAtt.DataSource = null;
        lvTodayAtt.DataBind();
        lvStudentMeeting.DataSource = null;
        lvStudentMeeting.DataBind();
        PnlStudentmeeting.Visible = false;
        lstAttSecondsms.Visible = false;
        divAttStatus.Visible = false;
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();
        HiddenItemSMSmark();
    }
    #endregion

    #region FillDropdown

    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            this.objCommon.FillDropDownList(ddlSessn, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SendSmstoParents.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void txtFromDat_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = null;

        if (rdbFormat.SelectedValue == "1")
        {
            int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
            ds = objAttC.GetSelectedDateSlotsForSMS(Convert.ToInt32(session), Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlBranches.SelectedValue), Convert.ToInt32(ddlSemestr.SelectedValue), Convert.ToInt32(ddlSect.SelectedValue), Convert.ToDateTime(txtFromDat.Text));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSlot.Items.Clear();
                ddlSlot.Items.Add(new ListItem("Please Select", "0"));
                ddlSlot.DataSource = ds.Tables[0];
                ddlSlot.DataTextField = ds.Tables[0].Columns["SLOTNAME"].ToString();
                ddlSlot.DataValueField = ds.Tables[0].Columns["SLOTNO"].ToString();
                ddlSlot.DataBind();
                divslots.Visible = true;
                ddlSlot.SelectedIndex = 0;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
            else
            {
                divslots.Visible = false;
                ddlSlot.Items.Clear();
                ddlSlot.Items.Add(new ListItem("Please Select", "0"));
                ddlSlot.SelectedIndex = 0;
                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }
        }
        else
        {
            divslots.Visible = false;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.SelectedIndex = 0;

            lvfirstsms.DataSource = null;
            lvfirstsms.DataBind();
            lvfirstsms.Visible = false;
            lstAttSecondsms.DataSource = null;
            lstAttSecondsms.DataBind();
            lstAttSecondsms.Visible = false;
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDeg, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "DISTINCT (CDB.DEGREENO)", "D.DEGREENAME", "CDB.COLLEGE_ID=" + ddlcollege.SelectedValue + " AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CDB.DEGREENO");

            ddlDeg.Focus();
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
                HiddenItemForPm();
            }
            HiddenItemForPm();
            HiddenItemSMS();
        }
        else
        {
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }
            HiddenItemForPm();
        }
    }

    protected void ddlScheme_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
                objCommon.FillDropDownList(ddlSemestr, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + session + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Scheme!", this.Page);
                ddlScheme.Focus();
            }
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }
            HiddenItemForPm();
            HiddenItemSMS();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDeg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDeg.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranches, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH BR ON (CD.BRANCHNO=BR.BRANCHNO)", "CD.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ddlDeg.SelectedValue), "BRANCHNO");

                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    if (rdbFormat.SelectedValue == "4")
                    {

                        btnWhatsAppAtt.Visible = true;
                    }
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
                //DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                //string BranchNos = "";
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
                //}
                //DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
                ////on faculty login to get only those dept which is related to logged in faculty
                //objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
                ddlBranches.Focus();
                HiddenItemForPm();
                HiddenItemSMS();

            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDeg.Focus();
                ddlBranches.Items.Add(new ListItem("Please Select", "0"));
                ddlBranches.SelectedIndex = 0;
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.SelectedIndex = 0;
                ddlSemestr.Items.Add(new ListItem("Please Select", "0"));
                ddlSemestr.SelectedIndex = 0;
                ddlSect.Items.Add(new ListItem("Please Select", "0"));
                ddlSect.SelectedIndex = 0;
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    if (rdbFormat.SelectedValue == "4")
                    {

                        btnWhatsAppAtt.Visible = true;
                    }
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
                HiddenItemForPm();
                HiddenItemSMS();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SendSmstoParents.ddlDeg_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBranches_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDeg.SelectedIndex > 0)
            {
                if (ddlBranches.SelectedIndex > 0)
                {
                    int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue)));
                    objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranches.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDeg.SelectedValue), "B.BRANCHNO");
                    objCommon.FillDropDownList(ddlSemestr, "ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + session + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "AND B.DEGREENO=" + Convert.ToInt32(ddlDeg.SelectedValue) + "AND B.BRANCHNO=" + Convert.ToInt32(ddlBranches.SelectedValue), "S.SEMESTERNO ASC");
                    objCommon.FillDropDownList(ddlSemPm, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO ASC");
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Branch!", this.Page);
                    ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                    ddlScheme.SelectedIndex = 0;
                    ddlSemestr.Items.Add(new ListItem("Please Select", "0"));
                    ddlSemestr.SelectedIndex = 0;
                    ddlSemPm.Items.Add(new ListItem("Please Select", "0"));
                    ddlSemPm.SelectedIndex = 0;
                    ddlBranches.Focus();
                    ddlSect.Items.Add(new ListItem("Please Select", "0"));
                    ddlSect.SelectedIndex = 0;
                    HiddenItemForPm();
                    HiddenItemSMS();
                }
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    if (rdbFormat.SelectedValue == "4")
                    {

                        btnWhatsAppAtt.Visible = true;
                    }
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDeg.Focus();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.SelectedIndex = 0;
                ddlSemestr.Items.Add(new ListItem("Please Select", "0"));
                ddlSemestr.SelectedIndex = 0;
                ddlSemPm.Items.Add(new ListItem("Please Select", "0"));
                ddlSemPm.SelectedIndex = 0;
                ddlSect.Items.Add(new ListItem("Please Select", "0"));
                ddlSect.SelectedIndex = 0;
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    if (rdbFormat.SelectedValue == "4")
                    {

                        btnWhatsAppAtt.Visible = true;
                    }
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
            }
            HiddenItemForPm();
            HiddenItemSMS();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSemestr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemestr.SelectedIndex > 0)
        {
            //if (ddlBranch.SelectedValue == "99")
            objCommon.FillDropDownList(ddlSect, "ACD_STUDENT_RESULT SR LEFT OUTER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_SCHEME WHERE DEGREENO = " + Convert.ToInt32(ddlDeg.SelectedValue) + ")", "SC.SECTIONNAME");
            //else
            //    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }
        }
        else
        {
            ddlSect.Items.Add(new ListItem("Please Select", "0"));
            ddlSect.SelectedIndex = 0;
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }
        }
        HiddenItemForPm();
    }

    protected void ddlSect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDat.Text = string.Empty;
        divslots.Visible = false;
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.SelectedIndex = 0;
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            if (rdbFormat.SelectedValue == "4")
            {

                btnWhatsAppAtt.Visible = true;
            }
        }
        else
        {
            btnWhatsAppAtt.Visible = false;
            HiddenItem();
        }
        HiddenItemForPm();
    }
    protected void ddlSessn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessn.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDeg, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO)", "DISTINCT (CDB.DEGREENO)", "D.DEGREENAME", "CDB.COLLEGE_ID=" + ddlcollege.SelectedValue + " AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "CDB.DEGREENO");
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_SESSION_MASTER SM ON(CM.COLLEGE_ID = SM.COLLEGE_ID)", "CM.COLLEGE_ID", "CM.COLLEGE_NAME", "SM.SESSIONID=" + Convert.ToInt32(ddlSessn.SelectedValue), "cm.COLLEGE_ID DESC");
            // ddlSessn.Focus();
            ddlcollege.Focus();
            ddlDeg.Focus();
            HiddenItemForPm();
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }
        }
        else
        {
            HiddenItemForPm();
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                if (rdbFormat.SelectedValue == "4")
                {

                    btnWhatsAppAtt.Visible = true;
                }
            }
            else
            {
                btnWhatsAppAtt.Visible = false;
                HiddenItem();
            }

        }
    }

    protected void ddlSemPm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemPm.SelectedIndex > 0)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    if (rdbFormat.SelectedValue == "4")
                    {

                        btnWhatsAppAtt.Visible = true;
                    }
                }
                else
                {
                    btnWhatsAppAtt.Visible = false;
                    HiddenItem();
                }
            }
            HiddenItemForPm();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void txtAttToDate_TextChanged(object sender, EventArgs e)
    {
        HiddenItemForPm();
        HiddenItemSMS();
        HiddenItem();
    }

    #endregion

    #region Email
    protected void btnEmailSms_Click(object sender, EventArgs e)
    {
        try
        {
            HiddenItemForPm();
            string slotname = objCommon.LookUp("ACD_TIME_SLOT", "TIMEFROM+' - '+TIMETO AS SLOTNAME", "SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue));
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "USER_PROFILE_SENDERNAME,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            string CollegeName = dsconfig.Tables[0].Rows[0]["CollegeName"].ToString();
            string College = dsconfig.Tables[0].Rows[0]["USER_PROFILE_SENDERNAME"].ToString();
            int cs = 0;
            string IPaddress = Session["ipAddress"].ToString();

            #region Email - First Hour Absent
            if (rdbFormat.SelectedValue == "1")//rdbFormat.SelectedIndex == 0)
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lvfirstsms.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                    if (cbRow.Checked == true)
                        count++;
                }
                if (count <= 0)
                {
                    objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send Email", this);
                    TODAYATT();
                    HiddenItemForPm();
                    HiddenItemSMS();
                    HiddenItem();
                    return;
                }
                else
                {
                    foreach (ListViewDataItem item in lvfirstsms.Items)
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblParMobile = item.FindControl("lblParMobile") as Label;
                        HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                        HiddenField hdnStuName = item.FindControl("hdnStuName") as HiddenField;
                        HiddenField hdnCode = item.FindControl("hdnCode") as HiddenField;
                        Label lblRegNo = item.FindControl("lblEnrollmentNo") as Label;
                        Label lblParEmail = item.FindControl("lblParEmail") as Label;
                        string useremail = lblParEmail.Text;

                        //  string message = "Dear Parent, Greetings from " + CollegeName + " ! Your Ward " + hdnStuName.Value + " is absent for the time slot " + slotname + " on " + Convert.ToDateTime(txtFromDat.Text).ToString("dd/MM/yyyy") + "." + " -" + CollegeName + "";
                        string subject = " " + College + " || Attendance Status ";
                        string message = "<b>Dear Parent</b><br />";
                        message += "<br/>Greetings from " + CollegeName + " !<br/>";
                        message += "<br /><br /> Your Ward " + hdnStuName.Value + " is absent for the time slot " + slotname + " on " + Convert.ToDateTime(txtFromDat.Text).ToString("dd/MM/yyyy") + "." + CollegeName + "<br />";
                        if (chek.Checked)
                        {
                            if (useremail != string.Empty)
                            {
                                try
                                {
                                    int status1 = 0;
                                    string email_type = string.Empty;
                                    string Link = string.Empty;
                                    int sendmail = 0;
                                    //  DataSet ds = getModuleConfig();
                                    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    //{
                                    //    email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                                    //    Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                                    //}

                                    //if (email_type == "1" && email_type != "")
                                    //{
                                    //    status = sendEmail(message, useremail, subject);
                                    //}
                                    //else if (email_type == "2" && email_type != "")
                                    //{
                                    //    Task<int> task = Execute(message, useremail, subject);
                                    //    status = task.Result;
                                    //}
                                    //if (email_type == "3" && email_type != "")
                                    //{
                                    //    status = OutLook_Email(message, useremail, subject);
                                    //}
                                    status1 = objSendEmail.SendEmail(useremail, message, subject); //Calling Method
                                    if (status1 == 1)
                                    {

                                        // cs = objAttC.INSERTPARENTEMAILLOG(Convert.ToInt32(Session["userno"]), message, useremail, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), IPaddress, Convert.ToDateTime(txtFromDat.Text));
                                        objCommon.DisplayMessage(this.updDetained, "Email Send Successfully", this.Page);
                                        // chek.Checked = false;
                                        pnlfirst.Visible = true;
                                        pnlsecond.Visible = false;
                                        pnlthird.Visible = false;
                                        divFirstsms.Visible = true;
                                        divFirstsms.Visible = true;
                                        this.GetStudList();
                                        //TODAYATT();
                                        HiddenItemForPm();
                                        HiddenItemSMS();
                                        HiddenItem();
                                        //  return;

                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.updDetained, "Failed To send email", this.Page);
                                        // chek.Checked = false;
                                        pnlfirst.Visible = true;
                                        pnlsecond.Visible = false;
                                        pnlthird.Visible = false;
                                        divFirstsms.Visible = true;
                                        divFirstsms.Visible = true;
                                        this.GetStudList();
                                        HiddenItemForPm();
                                        HiddenItemSMS();
                                        HiddenItem();
                                        //TODAYATT();
                                        // return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }
                            }
                        }
                    }
                    HiddenItemForPm();
                    HiddenItemSMS();
                    HiddenItem();
                    //chek.Checked = false;
                }
            }
            #endregion

            #region Email - Attendance Percentage (Subject Wise)
            else if (rdbFormat.SelectedValue == "2")
            {
                int count = 0;
                foreach (ListViewDataItem dataitem in lstAttSecondsms.Items)
                {
                    CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                    if (cbRow.Checked == true)
                        count++;
                }
                if (count <= 0)
                {
                    objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send Email", this);
                    HiddenItemForPm();
                    HiddenItemSMS();
                    HiddenItem();
                    return;
                }
                else
                {
                    foreach (ListViewDataItem item in lstAttSecondsms.Items)
                    {
                        try
                        {
                            CheckBox chek = item.FindControl("cbRow") as CheckBox;
                            Label lblParMobile = item.FindControl("lblParMobile") as Label;
                            HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                            HiddenField hdnStuName = item.FindControl("hdnStuName") as HiddenField;
                            HiddenField hdnCode = item.FindControl("hdnCode") as HiddenField;
                            Label lblRegNo = item.FindControl("lblEnrollmentNo") as Label;
                            Label lblParEmail = item.FindControl("lblParEmail") as Label;
                            string useremail = lblParEmail.Text;
                            // string message = "Dear Parent, Greetings from " + CollegeName + "  ! The attendance percentage of your Ward " + lblRegNo.Text + "-" + hdnStuName.Value + " in various courses in the current semester (as on " + Convert.ToDateTime(txtAttToDate.Text).ToString("dd/MM/yyyy") + " ) is as follows: " + hdnCode.Value.ToString() + "." + "\n" + "Note : The minimum attendance requirement to attend semester end examination in a course is 75%. In case of less than 75% in courses, you are requested to contact Class Advisor / HoD/Dean for further course of action.";
                            string subject = " " + College + " || Attendance Status ";
                            string message = "<b>Dear Parent</b><br />";
                            message += "<br/>Greetings from " + CollegeName + " !<br/>";
                            message += "<br /> The attendance percentage of your Ward " + lblRegNo.Text + "-" + hdnStuName.Value + " in various courses in the current semester (as on " + Convert.ToDateTime(txtAttToDate.Text).ToString("dd/MM/yyyy") + " ) is as follows: " + hdnCode.Value.ToString() + "." + "\n" + "<br />";
                            message += "<br /> Note : The minimum attendance requirement to attend semester end examination in a course is 75%. In case of less than 75% in courses, you are requested to contact Class Advisor / HoD/Dean for further course of action.<br />";

                            if (chek.Checked)
                            {
                                if (useremail != string.Empty)
                                {
                                    try
                                    {
                                        int status = 0;
                                        string email_type = string.Empty;
                                        string Link = string.Empty;
                                        int sendmail = 0;
                                        //DataSet ds = getModuleConfig();
                                        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                        //{
                                        //    email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                                        //    Link = ds.Tables[0].Rows[0]["LINK"].ToString();
                                        //}

                                        //if (email_type == "1" && email_type != "")
                                        //{
                                        //    status = sendEmail(message, useremail, subject);
                                        //}
                                        //else if (email_type == "2" && email_type != "")
                                        //{
                                        //    Task<int> task = Execute(message, useremail, subject);
                                        //    status = task.Result;
                                        //}
                                        //if (email_type == "3" && email_type != "")
                                        //{
                                        //    status = OutLook_Email(message, useremail, subject);
                                        //}
                                        status = objSendEmail.SendEmail(useremail, message, subject); //Calling Method
                                        if (status == 1)
                                        {
                                            cs = objAttC.INSERTPARENTEMAILLOG(Convert.ToInt32(Session["userno"]), message, useremail, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), IPaddress, Convert.ToDateTime(txtFromDat.Text));
                                            objCommon.DisplayMessage(this.updDetained, "Email Send Successfully", this.Page);
                                            // chek.Checked = false;
                                            pnlfirst.Visible = false;
                                            pnlsecond.Visible = true;
                                            pnlthird.Visible = false;
                                            divFirstsms.Visible = false;
                                            divattendancesecondsms.Visible = true;
                                            HiddenItemForPm();
                                            GetStudListAttPer();
                                            TODAYATT();
                                            // return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this.updDetained, "Failed To send email", this.Page);
                                            //chek.Checked = false;
                                            pnlfirst.Visible = false;
                                            pnlsecond.Visible = true;
                                            pnlthird.Visible = false;
                                            divFirstsms.Visible = false;
                                            divattendancesecondsms.Visible = true;
                                            HiddenItemForPm();
                                            GetStudListAttPer();
                                            TODAYATT();
                                            //  return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    // chek.Checked = false;
                }
                HiddenItemForPm();
                HiddenItemSMS();
                HiddenItem();
            }
            #endregion
            else
            {
                objCommon.DisplayMessage(this.updDetained, "Email Service Not Available", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region WhatsApp (06/03/2023)
    //Added By JAY TAKALKHEDE 06/03/2023  For Whatsapp
    protected void AisensyWhatsaapIntergrationForAttendance(string ToMobileNo, string att, string Dept, string course, string Name)
    {
        try
        {
            int Mobile_le = ToMobileNo.Length;
            if (Mobile_le == 10)
            {
                ToMobileNo = "91" + ToMobileNo.ToString();
            }
            DataSet ds = objCommon.FillDropDown("ACD_WHATSAPP_CONFIGURATION", "*", "", "ORGANIZATIONID=1", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string API_URL = ds.Tables[0].Rows[0]["API_URL"].ToString();
                string API_KEY = ds.Tables[0].Rows[0]["API_KEY"].ToString();
                string UserName = ds.Tables[0].Rows[0]["FROM_MOBILENO"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_URL.ToString());
                httpWebRequest.Method = "POST";                 //httpWebRequest.Headers.Add("aftership-api-key:********fdbfd93980b8c5***");
                httpWebRequest.ContentType = "application/json";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var bodys = @"{""apiKey"":" + '"' + API_KEY.ToString() + '"' + "," + "\n" +
                        @"""campaignName"":""erpattendance_rcpit""," + "\n" +
                        @"""destination"":" + '"' + ToMobileNo.ToString() + '"' + "," + "\n" +
                        @"""userName"":" + '"' + UserName.ToString() + '"' + "," + "\n" +
                        @"""templateParams"":[" + '"' + Name.ToString() + '"' + "," + '"' + att.ToString() + '"' + "," + '"' + course.ToString() + '"' + "," + '"' + Dept.ToString() + '"' + "]}";
                    streamWriter.Write(bodys);
                    streamWriter.Flush();
                    streamWriter.Close(); var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    //Added By JAY TAKALKHEDE 06/03/2023  For Whatsapp 
    protected void WhatsappAtt(string studname, string ToMobileNo, string SessionName, string Sregno, string TClass, string TAttendance, string TPercentage, string message)
    {
        bool success = true;
        string Account_SID = "HXIN1700894763IN";
        string api_key = "A2a2b94ce1945b32e4eeb7e784aac9ac8";
        string API_URL = "https://api.kaleyra.io/v1/" + Account_SID.ToString() + "/messages?";
        try
        {
            string from = "919645081287";

            using (WebClient client = new WebClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //InfoData payloadObj = new InfoData() { to = "919503244325", from = from, type ="template", channel = "WhatsApp", template_name = "otperp", lang_code = "en", @params = "Roshan,253132" };

                string Para = '"' + studname.ToString() + '"' + "," + '"' + message.ToString() + '"' + "," + '"' + SessionName + '"' + "," + '"' + Sregno + '"' + "," + '"' + TClass + '"' + "," + '"' + TAttendance + '"' + "," + '"' + TPercentage + '"';
                string myParamet = "from=" + from.ToString() + "&" + "to=" + ToMobileNo.ToString() + "&" + "type=template" + "&" + "channel=WhatsApp" + "&" + "params=" + Para.ToString() + "&template_name=rajagiriattendance" + "&" + "lang_code=en";

                using (WebClient wc = new WebClient())
                {
                    // wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers["api-key"] = api_key.ToString();
                    wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(API_URL, myParamet);
                }
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
            Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String body = reader.ReadToEnd();
            Console.WriteLine(body);
            success = false;
        }

    }


    protected void btnWhatsAppAtt_Click(object sender, EventArgs e)
    {
        #region SMS/Email - Todays Students Attendance list (WhatsApp (06/03/2023))
        if (rdbFormat.SelectedValue == "4")
        {
            HiddenItemForPm();
            HiddenItemSMS();
<<<<<<< HEAD
=======
            HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
            //Added By Jay T. On dated 23022024
            HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
            int count = 0;
            string MailSendStatus = string.Empty;
            string MailNotSendStatus = string.Empty;
            DataSet ds1 = null;
            DataSet ds2 = null;
            foreach (ListViewDataItem dataitem in lvTodayAtt.Items)
            {
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                    count++;
            }
            if (count <= 0)
            {
                objCommon.DisplayMessage(this.updDetained, "Please Select atleast one Student For Send WhtasApp", this);
                return;
            }
            else
            {
                try
                {
                    foreach (ListViewDataItem item in lvTodayAtt.Items)
                    {
                        CheckBox chek = item.FindControl("cbRow") as CheckBox;
                        Label lblParMobile = item.FindControl("lblParMobiletoday") as Label;
                        Label lblname = item.FindControl("lblnametoday") as Label;
                        Label lblAtt = item.FindControl("lbltodayatt") as Label;
                        Label lblregno = item.FindControl("lblregno") as Label;
                        HiddenField hdnDEPT = item.FindControl("hdnDEPT") as HiddenField;
                        HiddenField hdnIDNO = item.FindControl("hdnIDNO") as HiddenField;
                        HiddenField hdnCourse = item.FindControl("hdnCourse") as HiddenField;

                        string Att = lblAtt.Text;
                        string Dept = hdnDEPT.Value;
                        string course = hdnCourse.Value;

                        string name = lblname.Text;
                        if (chek.Checked)
                        {
                            string ToMobileNo = objCommon.LookUp("ACD_STUDENT", "FATHERMOBILE", "IDNO=" + hdnIDNO.Value);
                            string mobile = "91" + ToMobileNo;
                            if (ToMobileNo != string.Empty)
                            {
                                if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                                {
                                    AisensyWhatsaapIntergrationForAttendance(lblParMobile.Text.ToString(), Att.ToString(), Dept.ToString(), course.ToString(), lblname.Text.ToString());
                                    MailSendStatus += hdnIDNO.Value + ',';
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updDetained, "Sorry..! Didn't found Mobile no. for some Parent(s)", this.Page);
                                }
                            }
                            else
                            {
                                MailNotSendStatus += hdnIDNO.Value + ',';
                            }
                        }
                    }
                    if (MailNotSendStatus != string.Empty)
                    {
                        ds1 = (objCommon.FillDropDown("ACD_STUDENT", "STUDNAME  ", "IDNO", "IDNO IN (" + MailNotSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }

                    if (MailSendStatus != string.Empty)
                    {
                        ds2 = (objCommon.FillDropDown("ACD_STUDENT", "STUDNAME", "IDNO", "IDNO IN (" + MailSendStatus.TrimEnd(',') + ")", "IDNO"));
                    }
                    string MailSendTo = string.Empty;
                    string MailNotSendTo = string.Empty;


                    if (MailNotSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            MailNotSendTo += ds1.Tables[0].Rows[i]["STUDNAME"].ToString() + "\n" + ",";
                        }
                    }


                    if (MailSendStatus != string.Empty)
                    {
                        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        {
                            MailSendTo += ds2.Tables[0].Rows[i]["STUDNAME"].ToString() + ",";
                        }
                    }
                    if (MailSendTo != string.Empty || MailNotSendTo != string.Empty)
                    {
                        objCommon.DisplayMessage(this.updDetained, "Whatsapp Message Sent successfully", this);
                        txtSmsSend.Visible = true;
                        txtSmsNotSend.Visible = true;
                        txtSmsSend.Text = "WhatsApp  Message Send Student List - " + "\n" + MailSendTo.ToString().TrimEnd(',');
                        txtSmsNotSend.Text = "WhatsApp  Message Not Send Student List - " + "\n" + MailNotSendTo.ToString().TrimEnd(',');
                    }
                    else
                    {
                        lblMailNorSendTo.Visible = false;
                        lblMailSendTo.Visible = false;
                        //HiddenItemForPm();
                    }
                    HiddenItemForPm();
                    HiddenItemSMS();
<<<<<<< HEAD
=======
                    HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
                    //Added By Jay T. On dated 23022024
                    HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
                    HiddenItem();
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(this.updDetained, "Server UnAvailable", this.Page);
                    }
                }
            }

        }
        #endregion
    }
    #endregion

    #region //ADDED BY PRAFULL ON DT 11042023 FOR CAT MARK SMS


    static string getString(char[] arr)
    {
        // string() is a used to 
        // convert the char array
        // to string
        string s = new string(arr);

        return s;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string TemplateID = string.Empty;
        string TEMPLATE = string.Empty;
        string message = string.Empty;
        string template = string.Empty;
        string firstvar = string.Empty;
        string secondvar = string.Empty;
        string thirdvar = string.Empty;
        string fourtvar = string.Empty;
<<<<<<< HEAD
=======
        HiddenItemParents(); HiddenClasses_Commence_From(); //Added By Jay T. On dated 23022024
        //Added By Jay T. On dated 23022024
        HiddenItemFeesNotPaid();
>>>>>>> 93735f6d ([ENHANCEMENT] [57398] [SMS Integration])
        string inputString = "aDSD"; // Replace with your own string value


        //int numChunks = (int)Math.Ceiling((double)inputString.Length / chunkSize);
        //string[] chunks = new string[4];

        List<string> stringParts = new List<string>();

        if (inputString.Length > 120)
        {
            inputString = inputString.Substring(0, 120);
            int count_string = inputString.Length;
        }

        if (inputString.Length <= 120)
        {

            if (inputString.Length > 0)
            {
                int onefourthcountstring = (inputString.Length / 4);
                int chunkSize = onefourthcountstring;

                if (inputString.Length % 4 == 0)
                {
                    char[] array1 = inputString.Substring(0, Math.Min(chunkSize, inputString.Length)).ToCharArray();
                    char[] array2 = inputString.Substring(chunkSize, Math.Min(chunkSize, inputString.Length - chunkSize)).ToCharArray();
                    char[] array3 = inputString.Substring(chunkSize * 2, Math.Min(chunkSize, inputString.Length - chunkSize * 2)).ToCharArray();
                    char[] array4 = inputString.Substring(chunkSize * 3, Math.Min(chunkSize, inputString.Length - chunkSize * 3)).ToCharArray();

                    firstvar = getString(array1);
                    secondvar = getString(array2);
                    thirdvar = getString(array3);
                    fourtvar = getString(array4);
                }
                else
                {

                    char[] array1 = inputString.Substring(0, Math.Min(chunkSize, inputString.Length)).ToCharArray();
                    char[] array2 = inputString.Substring(chunkSize, Math.Min(chunkSize, inputString.Length - chunkSize)).ToCharArray();
                    char[] array3 = inputString.Substring(chunkSize * 2, Math.Min(chunkSize, inputString.Length - chunkSize * 2)).ToCharArray();
                    char[] array4 = inputString.Substring(chunkSize * 3, Math.Min(chunkSize, inputString.Length - chunkSize * 3)).ToCharArray();

                    firstvar = getString(array1);
                    secondvar = getString(array2);
                    thirdvar = getString(array3);
                    fourtvar = getString(array4);
                }

            }
        }


    }

    protected void ddlclg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlclg.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlclg.SelectedValue));
            //ViewState["degreeno"]
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlsessionexam, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");

                HiddenItemForPm();
                HiddenItem();
                HiddenItemSMS();
            }

        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & scheme", this.Page);
            ddlclg.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }
    protected void ddlsessionexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsessionexam.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlsemexam, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON S.SEMESTERNO=SR.SEMESTERNO", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO=" + ddlsessionexam.SelectedValue, "SR.SEMESTERNO");
            ddlsemexam.Focus();

            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select Session", this.Page);
            ddlsessionexam.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }
    protected void ddlsemexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsemexam.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlsectionexam, "ACD_SECTION S INNER JOIN ACD_STUDENT_RESULT SR ON S.SECTIONNO=SR.SECTIONNO", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO=" + ddlsessionexam.SelectedValue + " AND SR.SEMESTERNO=" + ddlsemexam.SelectedValue, "S.SECTIONNO");

            int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])));
            objCommon.FillDropDownList(ddlexamnew, "ACD_SUBEXAM_NAME SE INNER JOIN ACD_EXAM_NAME EN ON EN.EXAMNO=SE.EXAMNO", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "SE.PATTERNNO=" + patternno + " AND ISNULL(SE.ACTIVESTATUS,0)=1", "SE.SUBEXAMNAME");

            ddlsectionexam.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
        else
        {
            objCommon.DisplayMessage("Please Select Semester", this.Page);
            ddlsemexam.Focus();
            HiddenItemForPm();
            HiddenItem();
            HiddenItemSMS();
        }
    }
    protected void ddlsectionexam_SelectedIndexChanged(object sender, EventArgs e)
    {
        int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])));
        objCommon.FillDropDownList(ddlexamnew, "ACD_SUBEXAM_NAME SE INNER JOIN ACD_EXAM_NAME EN ON EN.EXAMNO=SE.EXAMNO", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "SE.PATTERNNO=" + patternno + " AND ISNULL(SE.ACTIVESTATUS,0)=1", "SE.SUBEXAMNAME");
        HiddenItemForPm();
        HiddenItem();
        HiddenItemSMS();
    }

    #endregion
}
    #endregion














