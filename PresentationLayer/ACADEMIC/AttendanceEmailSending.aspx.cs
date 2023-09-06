//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : AttendanceEmailSending                                                   
// CREATION DATE : 25/10/2019                                                        
// CREATED BY    : Nidhi Gour                                
// MODIFIED DATE : 31/10/2019                                                         
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Mail;
using SendGrid;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_AttendanceEmailSending : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentAttendanceController STT = new StudentAttendanceController();
    //CONNECTIONSTRING
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                    // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();


                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();

                    if (Session["dec"].ToString() == "1" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8")
                    {
                        //btnHODReport.Visible = true;
                        //branch.Visible = false;
                        faculty.Visible = false;
                        //btnSubjectwise.Enabled = false;


                    }
                    else
                    {
                        branch.Visible = true;
                        faculty.Visible = true;
                        //btnSubjectwise.Enabled = true;
                    }

                    if (Session["usertype"].ToString() != "3")
                        dvFaculty.Visible = true;
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -
            }
            divMsg.InnerHtml = string.Empty;
            //// Clear message div
            //divScript.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
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


            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

            if (Session["usertype"].ToString() != "1" && Session["usertype"].ToString() != "8")
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEGREENAME");
            else
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");

            //else
            //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");

        }

        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlFaculty.Items.Clear();
        ddlFaculty.Items.Add(new ListItem("Please Select", "0"));
    }

    private void ClearAllAfterSms()
    {
        ddlClgname.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        txtPercentage.Text = string.Empty;
        lvStudents.DataSource = null;
        lvStudents.Visible = false;
        ddltheorypractical.Items.Clear();
        ddltheorypractical.Items.Add(new ListItem("Please Select", "0"));
        //trShwStd.Visible = false;
        lvStudents.Items.Clear();
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            else
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
        }
        else
        {
            ClearControls();
        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "schemeno", "SCHEMENAME", "BRANCHNO =" + ddlBranch.SelectedValue + "AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO");

            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_ATTENDANCE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue + "AND C.SEMESTERNO=" + ddlSem.SelectedValue + " AND C.SESSIONNO=" + ddlSession.SelectedValue, "C.SUBID");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue", "C.SUBID");

                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSem.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSection.Items.Clear();
            // objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION S ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue+" AND SR.SCHEMENO="+ddlScheme.SelectedValue+" AND SR.SEMESTERNO="+ddlSem.SelectedValue+" AND S.SECTIONNO>0", "S.SECTIONNO");
            objCommon.FillDropDownList(ddlSection, "ACD_ATTENDANCE SR INNER JOIN ACD_SECTION S ON(SR.SECTIONNO=S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue + " AND S.SECTIONNO>0", "S.SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddltheorypractical.SelectedValue == "2" || ddltheorypractical.SelectedValue == "3")
        {
            dvBatch.Visible = true;
            //rfvBatch.Visible = true;

            if (ddlSection.SelectedIndex > 0 && ddltheorypractical.SelectedValue != "1")
            {
                if (ddltheorypractical.SelectedValue == "2")
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR INNER JOIN ACD_BATCH B ON (B.BATCHNO = SR.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SECTIONNO =" + ddlSection.SelectedValue, "B.BATCHNO");
                else
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR INNER JOIN ACD_BATCH B ON (B.BATCHNO = SR.TH_BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + "AND SR.SECTIONNO =" + ddlSection.SelectedValue, "B.BATCHNO");

                ddlBatch.Focus();
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.SelectedIndex = 0;
                ddlBatch.Items.Clear();
                ddlBatch.SelectedIndex = 0;
            }
        }
        else
        {
            dvBatch.Visible = false;
            //rfvBatch.Visible = false;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        try
        {
            //lblMailNorSendTo.Visible = false;
            //lblMailSendTo.Visible = false;
            DataSet ds = new DataSet();
            ds = STT.GetAttendanceDeailsForSms1(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"])), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlSection.SelectedValue), ddlOperator.SelectedValue, Convert.ToDecimal(txtPercentage.Text), Convert.ToInt32(ddlSubjectType.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
            }
            else
            {
                lvStudents.Visible = false;
                objCommon.DisplayMessage("No Students Found For Current Selections", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


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

                    // this.SendSMS(mobile, smsMessage);//For sending SMS
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
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    public void SendSMS(string Mobile, string text)
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

                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        // Mobilelist += dr["STUDENTMOBILE"].ToString() + ",";

                        mobile = dr["STUDENTMOBILE"].ToString();

                        //smsMessage = "Attendance Total Class=" + TClass  + "Total Attendance=" + TAttendance  + "Percentage=" + TPercentage + 
                        //             "Regards," + "DMIMS,NAGPUR," + "Thanking You !";

                        smsMessage = "Student Attendance Session: " + SessionName + "\n" + "Enroll No: " + Sregno + "\n" + "Total Class: " + TClass + "\n" + "Total Attendance: " + TAttendance + "\n" + "Percentage: " + TPercentage + "\n" + "Regards\n" + "Sarala Birla University, Ranchi";


                        // this.SendSMS(mobile, smsMessage);//for sms sending
                        objCommon.DisplayMessage(updReport, "SMS sent Succesfully!", this.Page);


                        //smsMessage = txtSms.Text;
                        if (mobile != string.Empty)
                        {
                            objCommon.DisplayMessage(updReport, "Sorry..! Dont find Some Mobile no.", this.Page);
                        }



                    }

                }

                //if (mobile != string.Empty)
                //{
                //objCommon.DisplayMessage("Sorry..! Dont find Mobile no. of Parents!", this.Page);
            }  //}

            if (idno.Length <= 0)
            {
                objCommon.DisplayMessage(updReport, "Please Select atleast one Student for SMS", this.Page);
            }
            ClearAllAfterSms();
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEmail_Click(object sender, EventArgs e)
    {
        string msg = txtMessage.Text;
        string subject = txtSubject.Text;
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
        DataSet ds1 = null;
        DataSet ds2 = null;

        if (msg != string.Empty && subject != string.Empty)
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
                            string nbody = MessageBody(studname, msg, SessionName, Sregno, TClass, TAttendance, TPercentage);
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
                    ClearAllAfterSms();

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
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Email Subject and Message", this.Page);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    public string MessageBody(string studname, string message, string SessionName, string Sregno, string TClass, string TAttendance, string TPercentage)
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
        return nMailbody;
    }
    public int SendMailBYSendgrid(string message, string emailid, string subject)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD", "COMPANY_EMAILSVCID <> '' AND SENDGRID_USERNAME<> ''", string.Empty);
            string fromAddress = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
            string user = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();
            string pwd = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
            string decrFromPwd = Common.DecryptPassword(pwd);
            //==============================================================
            var myMessage = new SendGridMessage();
            //If want to send attachment in email
            //if (data.attachment != null)
            //{
            //    MemoryStream stream = new MemoryStream(data.attachment);
            //    myMessage.AddAttachment(stream, data.fileName);
            //}
            myMessage.From = new MailAddress(fromAddress);
            myMessage.AddTo(emailid);
            myMessage.Subject = subject;
            myMessage.Html = message;


            var credentials = new NetworkCredential(user, decrFromPwd);
            var transportWeb = new Web(credentials);
            transportWeb.Deliver(myMessage);
            ret = 1;
        }
        catch (Exception)
        {
            throw;
        }
        //return transportWeb.DeliverAsync(myMessage);
        return ret;
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

                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            }
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }
    }
}