using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_BtechAdmVerification : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objS = new Student();

    #region Page Event

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

                    this.PopulateDropDownList();

                    //txtToDate.Text = DateTime.Now.ToShortDateString();
                    //Focus on From Date
                    //txtFromDate.Focus();
                }
            }

            //Blank Div
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
        }
    }

    #endregion

    private void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN COLLEGE
            //if (Session["usertype"].ToString() != "1")
            //{
            //    objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //    //objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
            //}
        
            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D", "D.DEGREENO", "DEGREENAME", "D.DEGREENO = 2", "D.DEGREENO");

              //  objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
              
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

   

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        ddlBranch.SelectedIndex = 0;
        btnSubmit.Visible = false;
        if (ddlDegree.SelectedIndex > 0)
        {
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + ddlDepartment.SelectedValue, "A.LONGNAME");
        //if (Session["usertype"].ToString() != "1")
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
        //else
           objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
           ddlBranch.Focus();

        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
        
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindStudentListForApprove();
    }
    private void BindStudentListForApprove()
    {
        try
        {
            StudentController objSC = new StudentController();

            DataSet ds = objSC.GetStudentForAdmApproveByAccount(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvStudentDetail.DataSource = ds;
                lvStudentDetail.DataBind();
                divstudentdetail.Visible = true;
                btnSubmit.Visible = true;
                int i = 0;
                foreach (ListViewDataItem item in lvStudentDetail.Items)
                {
                    DropDownList ddlPayType = item.FindControl("ddlPayType") as DropDownList;
                    //TextBox txtAmount = item.FindControl("txtAmount") as TextBox;
                    if (ds.Tables[0].Rows[i]["PAYTYPENAME"].ToString() == string.Empty)
                        ddlPayType.SelectedIndex = 0;
                    else
                        ddlPayType.SelectedValue = ddlPayType.Items.FindByText(ds.Tables[0].Rows[i]["PAYTYPENAME"].ToString()).Value;
                    i++;
                }

            }
            else
            {
                lvStudentDetail.DataSource = null;
                lvStudentDetail.DataBind();
                divstudentdetail.Visible = false;
                objCommon.DisplayMessage(updStudent, "No Record Found.", this);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "User_Status_Report.DisplayAllCount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lvStudentDetail_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlPayType = e.Item.FindControl("ddlPayType") as DropDownList;
            objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsorptionStudentMarkEntry.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
       string semester=string.Empty;
        //objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENAME");
        foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
        {
            CheckBox chkBox = (dataitem.FindControl("cbRow")) as CheckBox;
            //Label label = (dataitem.FindControl("lblCourse")) as Label;
            DropDownList ddlPayType=(dataitem.FindControl("ddlPayType")) as DropDownList;
            TextBox txtAmount = (dataitem.FindControl("txtAmount")) as TextBox;
            int semesterno = Convert.ToInt32((dataitem.FindControl("HidSemesterNo") as HiddenField).Value);
            if (semesterno==1)
            {
                semester="SEMESTER1";
            }
            else if (semesterno==2)
            {
                semester="SEMESTER2";
            }
            else if (semesterno==3)
            {
                semester="SEMESTER3";
            }

            string Amount = objCommon.LookUp("ACD_STANDARD_FEES", "SUM(" + semester + ")", "BATCHNO=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + "AND PAYTYPENO=" + Convert.ToInt32(ddlPayType.SelectedValue) + "AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND COLLEGE_ID=" + Convert.ToInt32(ddlBranch.SelectedValue) + "AND RECIEPT_CODE='TF'");
            txtAmount.Text = Amount;
        
        }
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        DemandModificationController dmController = new DemandModificationController();
        FeeDemand demandCriteria = new FeeDemand(); //this.GetDemandCriteria();
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
        {

            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            if (cbRow.Checked == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(updStudent, "Please Select atleast One Student!!", this.Page);
            return;
        }
        try
        {
            //if (ViewState["usertype"].ToString() == "5")
            //{
            foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
            {

                CheckBox chkBox = (dataitem.FindControl("cbRow")) as CheckBox;
                DropDownList ddlPayType = (dataitem.FindControl("ddlPayType") as DropDownList);
                if (chkBox.Checked == true)
                {
                    string StudentName = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(chkBox.ToolTip));
                    objS.IdNo = Convert.ToInt32(chkBox.ToolTip);
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                    objS.IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

                    demandCriteria.StudentId = Convert.ToInt32(chkBox.ToolTip);
                    demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
                    demandCriteria.UANO = Convert.ToInt32(Session["userno"]);
                    demandCriteria.CollegeCode = Session["colcode"].ToString();
                    demandCriteria.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    demandCriteria.ReceiptTypeCode = "TF";
                    demandCriteria.PaymentTypeNo = Convert.ToInt32(ddlPayType.SelectedValue);
                 
                    string response = dmController.CreateDemandForStudentByFinance(demandCriteria);
                    if (response == "1")
                    {
                        if (response.Length > 2)
                        {
                            objCommon.DisplayMessage(this.updStudent, "Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.", this.Page);
                            //ShowMessage("Unable to create demand for following students.\\nEnrollment No.: " + response + "\\nStandard fees is not defined for fees criteria applicable to these students.");
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objSC.UpdateStudentAdmissionStatusByFinance(objS);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                int AdStatus = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "FINANCE_STATUS", "IDNO=" + Convert.ToInt32(chkBox.ToolTip)));

                                if (AdStatus == 1)
                                {
                                    //send SMS and Email to student for login credentials
                                    string StudName = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(chkBox.ToolTip));
                                    string StudEmail = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_IDNO=" + Convert.ToInt32(chkBox.ToolTip) + "AND UA_TYPE=2");
                                    string StudMobile = objCommon.LookUp("USER_ACC", "UA_MOBILE", "UA_IDNO=" + Convert.ToInt32(chkBox.ToolTip) + "AND UA_TYPE=2");
                                    string subject = "Online Admission Fee Payment";
                                    string message = "<b>Dear " + StudName + "," + "</b><br />";
                                    message += "Your Admission Registration is approved. Please Visit https://makaut.mastersofterp.in for Admission Fee Payment." + "</b>";
                                    message += "<br />Link Path: Academic -> Student Related -> Online Payment Or Admission Fee Offline/Online Payment <br />";
                                    message += "<br /><br /><br />Thank You<br />";
                                    message += "<br />Team MAKAUT, WB<br />";
                                    message += "<br /><br />Note : This is system generated email. Please do not reply to this email.<br />";

                                    Task<int> task = Execute(message, StudEmail, subject);

                                    if ((StudMobile != "" || StudMobile != null))
                                    {
                                        string msg = "Dear " + StudName + ",\r\n\r\nYour Admission Registration is approved.\r\n\r\nPlease Visit https://makaut.mastersofterp.in for Admission Fee Payment.\r\n\r\nLink Path: Academic -> Student Related -> Online Payment Or Admission Fee Offline/Online Payment.\r\n\r\nMAKAUT, WB";
                                        SendSMS(StudMobile, msg, "1007047803646131765");
                                    }
                                }
                            }
                        }
                        objCommon.ConfirmMessage(this.updStudent, "Admission Approved Successfully!!", this.Page);
                        BindStudentListForApprove();
                    }
                else if (response == "-99")
                        objCommon.DisplayMessage(this.updStudent, "There is an error occured while creating the demands for Student ." + StudentName, this.Page);
                else if (response == "5")
                    //objCommon.DisplayMessage(this.updApproveAdmission, "Define standard fees first for selected criteria!", this.Page);
                        objCommon.DisplayMessage(this.updStudent, "Fee Defination not defined for Student " + StudentName, this.Page);
                }
            }
                
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }
    }

    private string GetSelectedStudents()
    {
        string studentIDs = string.Empty;
        foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
        {
            if ((dataitem.FindControl("cbRow") as CheckBox).Checked)
            {
                if (studentIDs.Length > 0)
                    studentIDs += ", ";
                   
                studentIDs += (dataitem.FindControl("hidStudentNo") as HiddenField).Value;
            }
        }
        return studentIDs;
    }

    private string GetSelectedSemester()
    {
        string SemesterNos = string.Empty;
       
        foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
        {
            if ((dataitem.FindControl("cbRow") as CheckBox).Checked)
            {
                if (SemesterNos.Length > 0)
                SemesterNos += ",";
                SemesterNos += (dataitem.FindControl("HidSemesterNo") as HiddenField).Value;
            }
        }
        return SemesterNos;
    }

    private string GetSelectedPaymentType()
    {
        string PayTypes = string.Empty;
        foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
        {
            if ((dataitem.FindControl("cbRow") as CheckBox).Checked)
            {
                if (PayTypes.Length > 0)
                 PayTypes += ",";
                PayTypes += (dataitem.FindControl("ddlPayType") as DropDownList).SelectedValue;
            }
        }
        return PayTypes;
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
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
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    public void SendSMS(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url = string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;
            }
        }
        catch (Exception)
        {
        }
    }

    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        btnSubmit.Visible = false;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        btnSubmit.Visible = false;
       
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowdStudentData("Overall_Admission_Status");
    }

    private void ShowdStudentData(string reportName)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string admbatch = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + ddlAdmbatch.SelectedValue);

            DataSet ds = new DataSet();

            ds = objSC.GetNewAdmissionStudentDetailsForFinance(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));


            if (ds.Tables[0].Rows.Count > 0)
            {

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + admbatch.Replace(" ", "_") + "_" + reportName + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.ShowAllRooms() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }
  
}