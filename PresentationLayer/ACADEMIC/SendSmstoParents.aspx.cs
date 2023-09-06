using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Net;


public partial class ACADEMIC_SendSmstoParents : System.Web.UI.Page
{
    ExamController excol = new ExamController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                //Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();                            
                    this.FillDropdown();               
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
     
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SendSmstoParents.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SendSmstoParents.aspx");
        }
    }
    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            rdbFormat.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SendSmstoParents.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH BR ON (CD.BRANCHNO=BR.BRANCHNO)", "CD.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
                ddlBranch.Focus();
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_SendSmstoParents.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    if (rdbFormat.SelectedIndex == 1)
                    {
                        objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
                    }
                    else
                    { 
                    objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT A INNER JOIN ACD_STUDENT B ON (A.IDNO=B.IDNO) INNER JOIN ACD_SEMESTER S ON(S.SEMESTERNO=A.SEMESTERNO)", "DISTINCT B.SEMESTERNO", "SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "AND B.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND B.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue), "B.SEMESTERNO ASC");
                    }
                    }
                else
                {
                    objCommon.DisplayMessage("Please Select Branch!", this.Page);
                    ddlBranch.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnShowStudentlist_Click(object sender, EventArgs e)
    {

        if (rdbFormat.SelectedIndex == 0)//Attendance First SMS 
        {
            pnlsecond.Visible = false;
            pnlthird.Visible = false;
            pnlfirst.Visible = true;
            divattendancesecondsms.Visible = false;
            divIAMarks.Visible = false;
            divFirstsms.Visible = true;
            GetSTudentDetailsForFirstSMS();
        }
        else if (rdbFormat.SelectedIndex == 1)//Attendance Second SMS
        {
            
            pnlfirst.Visible = false;
            pnlthird.Visible = false;
            pnlsecond.Visible = true;
            divIAMarks.Visible = false;
            divFirstsms.Visible = false;
            divattendancesecondsms.Visible = true;
            GetStudentAttendanceList();
        }
        //SMS for IA Format
        else      
        {
            pnlfirst.Visible = false;            
            pnlsecond.Visible = false;
            pnlthird.Visible = true;
            divFirstsms.Visible = false;
            divattendancesecondsms.Visible = false;
            divIAMarks.Visible = true;
            GetStudentIAMarks();
        }

        
    }
    private void GetSTudentDetailsForFirstSMS()
    {
        string examname="S1";
        DataSet ds = excol.GetStudentIAMarksForSMS(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), examname);
         if (ds.Tables[0].Rows.Count > 0)
        {
            lvfirstsms.DataSource = ds;
            lvfirstsms.DataBind();
            hftot.Value = lvfirstsms.Items.Count.ToString();
    }
    else
    {
      objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
    }

    
    }
    private void GetStudentIAMarks()
    {
        string examname = Convert.ToString(ddlIAMarks.SelectedValue);
        DataSet ds = excol.GetStudentIAMarksForSMS(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue),examname);
    if (ds.Tables[0].Rows.Count > 0)
        {
        lvmarks.DataSource=ds;
        lvmarks.DataBind();
         hftot.Value = lvmarks.Items.Count.ToString();
    }
    else
    {
      objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
    }

    }
    private void GetStudentAttendanceList()
    {
        DataTable dt = new DataTable();
        try
        {
            DataSet ds = excol.GetStudentAttendanceData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                lvattendancesecondsms.DataSource = dt;
                lvattendancesecondsms.DataBind();
                hftot.Value = lvattendancesecondsms.Items.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.updDetained, "No Record Found For Your Selection!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AcademicCalenderMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objCommon.DisplayMessage(updDetained, "Server UnAvailable", this.Page);
            }
        }
    }
    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

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

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentAttendanceController dsStudentDetails = new StudentAttendanceController();
        if (rdbFormat.SelectedIndex == 0)
        {
            int MSGTYPE=1;//Attendance First SMS
            foreach (ListViewDataItem item in lvfirstsms.Items)
            {
                try
                {
                    CheckBox chek = item.FindControl("cbRow") as CheckBox;
                    Label lblParMobile = item.FindControl("lblParMobile") as Label;
                    HiddenField hdnidno=item.FindControl("hdnidno") as HiddenField;


                    if (chek.Checked)
                    {
                        string message = "Dear Parent, Kindly be reminded that, maintaining 85% attendance in every subject is mandatory. Please advise your ward to maintain above 85% attendance in every subject to avoid losing a year. From: Principal, JSS ST University(SJCE), Mysuru.";

                        if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length==10)
                            {
                            CustomStatus cs = (CustomStatus)excol.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), MSGTYPE);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                this.SendSMS(lblParMobile.Text, message);

                                objCommon.DisplayUserMessage(updDetained, "SMS send succesfully Send To Parent(s)", this.Page);
                            }
                                else
                        {
                            objCommon.DisplayMessage(updDetained, "Error Occured..!!", this.Page);
                        }                            
                       }
                            else
                            {
                                objCommon.DisplayMessage(updDetained, "Sorry..! Don't find Mobile no. for some Parent(s)", this.Page);
                            }
                        
                        
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(updDetained, "Server UnAvailable", this.Page);
                    }
                }
            }
        }
        else if (rdbFormat.SelectedIndex == 1)//Attendance Second SMS
        {
            int MSGTYPE = 2;//Attendance Second SMS
            foreach (ListViewDataItem item in lvattendancesecondsms.Items)
            {
                try
                {
                    CheckBox chek = item.FindControl("cbRow") as CheckBox;
                    Label lblParMobile = item.FindControl("lblParMobile") as Label;
                    Label lblstuattendance = item.FindControl("lblstuattendance") as Label;                
                    HiddenField hdnenroll = item.FindControl("hdnenroll") as HiddenField;
                    HiddenField hdnsemesterno = item.FindControl("hdnsemesterno") as HiddenField;
                     HiddenField hdndeptname = item.FindControl("hdndeptname") as HiddenField;
                     HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                    Label lblname = item.FindControl("lblname") as Label;
                    
                    DateTime date = DateTime.Now;
                    if (chek.Checked)
                    {
                        string message = "Dear Parents, Kindly note the attendance status of your ward " + lblname.Text + ", Sem " + hdnsemesterno.Value.ToString() 
            + " as on " + date + " in % is " + lblstuattendance.Text + ",If the attendance in any subject is below 85%, you are required to meet HOD-"+hdndeptname.Value.ToString()+". From : Principal , JSS ST University(SJCE), Mysuru.";
                        if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                        {
                            CustomStatus cs = (CustomStatus)excol.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), MSGTYPE);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                this.SendSMS(lblParMobile.Text, message);
                                objCommon.DisplayUserMessage(updDetained, "SMS send succesfully Send To Parent(s)", this.Page);
                            }

                            else
                            {
                                objCommon.DisplayMessage(updDetained, "Error Occured..!!", this.Page);
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(updDetained, "Sorry..! Dont find Mobile no. for some Parent(s)", this.Page);
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(updDetained, "Server UnAvailable", this.Page);
                    }
                }
            }
        }
        //SMS for IA Format
        else
        {
            foreach (ListViewDataItem item in lvmarks.Items)
            {
                int MSGTYPE = 3;//SMS for IA Format
                try
                {
                    CheckBox chek = item.FindControl("cbRow") as CheckBox;
                    Label lblParMobile = item.FindControl("lblParMobile") as Label;
                    Label lblIAMarks = item.FindControl("lblIAMarks") as Label;
                    HiddenField hdnenroll = item.FindControl("hdnenroll") as HiddenField;
                    HiddenField hdnsemesterno = item.FindControl("hdnsemesterno") as HiddenField;
                    HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                    Label lblname = item.FindControl("lblname") as Label;
                    if (chek.Checked)
                    {                
                        string message = "Dear Parents, Kindly note the "+ddlIAMarks.SelectedItem.Text+" marks of your ward " + lblname.Text + ",(" + hdnenroll.Value.ToString() + ")" + "of Sem " + hdnsemesterno.Value.ToString() + "," + lblIAMarks.Text + " , From Principal - JSS ST University(SJCE), Mysuru.";
                        if (lblParMobile.Text != string.Empty && lblParMobile.Text.Length == 10)
                        {
                            CustomStatus cs = (CustomStatus)excol.INSERTPARENTSMSLOG(Convert.ToInt32(Session["userno"]), message, lblParMobile.Text.ToString(), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdnidno.Value), MSGTYPE);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                //dsStudentDetails.SENDMSG(message, lblParMobile.Text);
                                this.SendSMS(lblParMobile.Text, message);
                                objCommon.DisplayUserMessage(updDetained, "SMS send succesfully Send To Parent(s)", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(updDetained, "Error Occured..!!", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Sorry..! Dont find Mobile no. for some Parent(s)", this.Page);
                        }
                        }
                    }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "Academic_SendSmstoParents.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                    {
                        objCommon.DisplayMessage(updDetained, "Server UnAvailable", this.Page);
                    }
                }
            }
        }

    }
    public void clearcontrols()
    {
        ddlSession.SelectedIndex = 0;
        ddlcollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;   
        txtTotStud.Text = "0";
        divFirstsms.Visible = false;
        divattendancesecondsms.Visible = false;
        divIAMarks.Visible = false;
        divscheme.Visible = false;
        divexamname.Visible = false;

    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
        this.clearcontrols();
    }
    protected void rdbFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbFormat.SelectedIndex == 1)
        {
            this.clearcontrols();
            divscheme.Visible = true;
            rfvScheme.Visible = true;
            rfvBranch.Visible = true;
            rfvIAMark.Visible = false;
            rfvIAMark.Enabled = false;
            divexamname.Visible = false;
            rfvSem.Visible = true;
            rfvScheme.Enabled = true;            
            rfvBranch.Enabled = true;            
            rfvSem.Enabled = true;
        }
        else
        {
            this.clearcontrols();
            if (rdbFormat.SelectedIndex == 2)
            {
                divexamname.Visible = true;
                rfvIAMark.Visible = true;
                rfvIAMark.Enabled = true;
            }
            else
            {
                divexamname.Visible = false;
                rfvIAMark.Visible = false;
                rfvIAMark.Enabled = false;
            }
            divscheme.Visible = false;
            rfvScheme.Visible = false;
            rfvBranch.Visible = false;
            rfvSem.Visible = false;
            rfvBranch.Enabled = false;
            rfvScheme.Enabled = false;
            rfvSem.Enabled = false;
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Scheme!", this.Page);
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}