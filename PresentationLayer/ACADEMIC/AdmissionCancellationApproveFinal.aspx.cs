using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using SendGrid;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;

public partial class ACADEMIC_AdmissionCancellationApproveFinal : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AdmissionCancellationController admCanController = new AdmissionCancellationController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                //Page Authorization
              //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    //lblSession.Text = Session["sessionname"].ToString();
                    PopulateDropDown();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                btnSubmit.Enabled = false;
            }

           
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
                Response.Redirect("~/notauthorized.aspx?page=AdmissionCancellationApproveFinal.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AdmissionCancellationApproveFinal.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {

            objCommon.FillDropDownList(ddlStudentList, "ACD_STUDENT S INNER JOIN ACD_ADM_CANCEL BR ON S.IDNO = BR.IDNO", "DISTINCT S.IDNO", "(CASE WHEN S.REGNO IS NOT NULL THEN S.STUDNAME+'('+ISNULL(S.REGNO,'')+')' ELSE S.STUDNAME END)STUDENT_NAME", "ISNULL(IS_FINAL_LEVEL_APPROVE,0) != 1 AND ISNULL(IS_FIRST_LEVEL_APPROVE,0) = 1  ", "IDNO");
            objCommon.FillDropDownList(ddlDegree, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreename");
            //session
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND BRANCHNO<>99", "LONGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowStudentDetails(int idno)
    {
        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            DataTableReader dtr = objSRegist.GetStudentDetails(idno);

            if (dtr.Read())
            {
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString();
                lblRollNo.Text = dtr["ROLLNO"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            }
            dtr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text == string.Empty)
        {
            ShowMessage("Please enter some remarks.");
            txtRemark.Focus();
            return;
        }
      
        Common objcommon = new Common();
        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_Student", "IDNO", "IDNO=" + Session["idno"].ToString()));                // + txtStudent.Text.Trim()));
        
        int IdNo = Convert.ToInt32(IDNO);
        int ua_no = Convert.ToInt32(Session["userno"]);
        string Remark = txtRemark.Text.Trim();
        string ipAddress = Request.ServerVariables["REMOTE_HOST"];
        string remark = "This student's admission has been cancelled by " + Session["userfullname"].ToString();
            remark += " on " + DateTime.Now + ". " + txtRemark.Text.Trim();

        
        CustomStatus cs = (CustomStatus)admCanController.AdmissionCancel_FinalApprove(IdNo, ua_no, Remark, ipAddress);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(UpdatePanel1, "Admission Cancelled Successfully!", this.Page);
            admCanController.CancelAdmission(IdNo, remark);
            btnCancelAdmissionSlip.Enabled = true;
            btnCancelAdmissionSlip.Visible = true;
            //TransferToEmail(lblName.Text, lblRegNo.Text, lblBranch.Text, lblDegree.Text, txtRemark.Text,lblColg.Text);
        }
    }

    protected void ddlStudentList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        divMsg.InnerHtml = string.Empty;
        lblNote.Visible = false;
        btnCancelAdmissionSlip.Visible = true;
        
        lblMsg.Text = string.Empty;

        try
        {
            StudentRegistration objSRegist = new StudentRegistration();
            StudentController objSC = new StudentController();
            string idno = "0";
            string category = "";
            string admcan = "0";
            string FirstLevelApprove = "0";
           
            idno = ddlStudentList.SelectedValue;
            if (idno == string.Empty)
            {
                lblName.Text = string.Empty;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Student Not Found!!";
                this.ClearControls();
                btnSubmit.Enabled = false;
                btnCancelAdmissionSlip.Visible = false;
                return;
            }
            if (txtStudent.Text.Trim() != string.Empty)
            {
                admcan = objCommon.LookUp("acd_student", "isnull(admcan,0)", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
                if (admcan == "True")
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Entered student admission has been cancelled!!", this.Page);
                    txtStudent.Text = "";
                    txtStudent.Focus();
                    return;
                }

                FirstLevelApprove = objCommon.LookUp("ACD_ADM_CANCEL", "IS_FIRST_LEVEL_APPROVE", "IDNO=" + idno);

                if (String.IsNullOrEmpty(FirstLevelApprove))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);
                    return;
                }

                if (!String.IsNullOrEmpty(FirstLevelApprove))
                {
                    if (FirstLevelApprove.ToLower() == "false")
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Student Not Found!", this.Page);
                        return;
                    }
                }

                if (txtStudent.Text.Contains("("))
                {
                    if (txtStudent.Text.Contains("["))
                    {
                        char[] ct = { '(' };
                        string[] cat = txtStudent.Text.Trim().Split(ct);
                        //idno value
                        category = cat[1].Replace(")", "");
                        cat = category.Split('[');
                        category = cat[0].Replace("]", "");
                        char[] sp = { '[' };
                        string[] data = txtStudent.Text.Trim().Split(sp);
                        //idno value
                        idno = data[1].Replace("]", "");
                        ViewState["idno"] = Convert.ToInt32(idno);
                    }

                }
            }
            string degreeNo = objCommon.LookUp("acd_student", "degreeno", "regno='" + txtStudent.Text.Trim() + "' AND CAN=0 AND ADMCAN=0");
            ViewState["idno"] = idno.ToString();


            imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + idno.ToString() + "&type=student";
            DataTableReader dtr = objSRegist.GetStudentDetailsFinalApprove(Convert.ToInt32(idno));

            if (dtr.Read())
            {
                Session["idno"] = dtr["idno"].ToString();
                lblName.Text = dtr["STUDNAME"].ToString();
                lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString(); ;
                lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                ViewState["OldRollNo"] = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                lblRollNo.Text = ViewState["OldRollNo"].ToString();
                lblBranch.Text = dtr["LONGNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                lblDegree.Text = dtr["DEGREENAME"].ToString();
                lblDegree.ToolTip = dtr["DEGREENO"].ToString();

                lblColg.Text = dtr["COLLEGE_NAME"].ToString();
                lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();

                ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
                ViewState["degreeNo"] = dtr["DEGREENO"].ToString();

                ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
                ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                ViewState["BranchNoOld"] = dtr["BRANCHNO"].ToString();

                ViewState["PTYPENO"] = dtr["PTYPE"].ToString();
                lblRequestRemark.Text = dtr["REQUEST_REMARK"].ToString();
                lblApprovedRemark.Text = dtr["APPROVE_REMARK"].ToString();
                lblAcademicRemark.Text = dtr["ACADEMIC_LEVEL_APPROVE_REMARK"].ToString();
                ViewState["File_path"] = dtr["PREVIEW_PATH"].ToString();
                if (ViewState["File_path"].ToString() == string.Empty)
                {
                    lblPreview.Text = "Preview Not Available";
                    lblPreview.ForeColor = System.Drawing.Color.Red;
                    lnkView.Visible = false;
                    lblPreview.Visible = true;
                }
                else
                {
                    lblPreview.Visible = false;
                    lnkView.Visible = true;
                }
                btnSubmit.Enabled = true;
                divdata.Visible = true;
                //}

              DataSet ds1=  objCommon.FillDropDown("ACD_DEMAND D LEFT JOIN (SELECT IDNO,ISNULL(SUM(TOTAL_AMT),0)PAID_AMOUNT,SEMESTERNO,RECIEPT_CODE FROM ACD_DCR WHERE IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"].ToString()) + " AND ISNULL(RECON,0)=1 AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='TF' GROUP BY IDNO,SEMESTERNO,RECIEPT_CODE)A ON A.IDNO=D.IDNO AND A.SEMESTERNO=D.SEMESTERNO AND A.RECIEPT_CODE=D.RECIEPT_CODE ", "D.IDNO", "TOTAL_AMT APPLIED_AMOUNT,PAID_AMOUNT", "D.IDNO=" + idno + " AND D.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterNo"].ToString()) + " AND ISNULL(CAN,0)=0 AND D.RECIEPT_CODE='TF'", "");
              if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count>0)
              {
                  lblAppliedAmount.Text = ds1.Tables[0].Rows[0]["APPLIED_AMOUNT"].ToString() == string.Empty ? Convert.ToString(0) : ds1.Tables[0].Rows[0]["APPLIED_AMOUNT"].ToString();
                  lblPaidAmount.Text = ds1.Tables[0].Rows[0]["PAID_AMOUNT"].ToString() == string.Empty ? Convert.ToString(0) : ds1.Tables[0].Rows[0]["PAID_AMOUNT"].ToString();
              }

                imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["idno"].ToString() + "&type=student";
                

            }
            else
            {
                //lblRegNo.Text = string.Empty;
                btnSubmit.Enabled = false;
            }
            dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BranchChange.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        lblName.Text = string.Empty;
        lblRegNo.Text = string.Empty;
        lblRollNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        txtStudent.Text = string.Empty;
        txtRemark.Text = string.Empty;
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        lblNote.Visible = false;
        ddlStudentList.SelectedIndex = 0;
        divdata.Visible = false; ;
        btnCancelAdmissionSlip.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ExportinExcelAdmissionCancel();
    }

    private void ExportinExcelAdmissionCancel()
    {
        BranchController objBrn = new BranchController();
        string attachment = "attachment; filename=" + "AdmissionCancelExcel.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = admCanController.GetAdmCancelFirstLevel();

        DataGrid dg = new DataGrid();

        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkView = (LinkButton)(sender);
        
        string path = ViewState["File_path"].ToString();
          
        iframeView.Attributes.Add("src", path);

        mpeViewDocument.Show();
        
    }

    protected void btnCancelAdmissionSlip_Click(object sender, EventArgs e)
    {
        ShowReport_Adm_Cancel("StudentAdmissionCancel", "StudentAdmissionCancellationSlip.rpt");
    }

    private void ShowReport_Adm_Cancel(string reportTitle, string rptFileName)
    {
        try
        {
            int studId = Convert.ToInt32(Session["idno"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + studId + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    protected void rdoFinalApprove_CheckedChanged(object sender, EventArgs e)
    {
        divApprove.Visible = true;
        divReport.Visible = false;
    }
    protected void rdoReport_CheckedChanged(object sender, EventArgs e)
    {
        divApprove.Visible = false;
        divReport.Visible = true; ;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Fill Branch;
        // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnCanReport_Click(object sender, EventArgs e)
    {
        try
        {
            //check the from date always be less than to date
            string[] fromDate = txtFromDate.Text.Split('/');
            string[] toDate = txtToDate.Text.Split('/');
            DateTime fromdate = Convert.ToDateTime(Convert.ToInt32(fromDate[0]) + "/" + Convert.ToInt32(fromDate[1]) + "/" + Convert.ToInt32(fromDate[2]));
            DateTime todate = Convert.ToDateTime(Convert.ToInt32(toDate[0]) + "/" + Convert.ToInt32(toDate[1]) + "/" + Convert.ToInt32(toDate[2]));
            if (fromdate > todate)
            {
                objCommon.DisplayMessage("From Date always be less than To date. Please Enter proper Date range.", this.Page);
                clearControl();
            }
            else
            {
                ShowReport("BranchCancelAdmission", "rptBranchwiseAdmissionCancel.rpt", 0);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Admission_Cancellation.Report --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void clearControl()
    {
        ddlDegree.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }

    private void ShowReport(string reportTitle, string rptFileName, int reporttype)
    {
       
            DataSet ds = admCanController.GetCancelledAdmissionStudentReport(Convert.ToDateTime(txtFromDate.Text), (Convert.ToDateTime(txtToDate.Text)).AddDays(1), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                        url += "Reports/CommonReport.aspx?";
                        url += "pagetitle=" + reportTitle;
                        url += "&path=~,Reports,Academic," + rptFileName;
                        DateTime dtStart = Convert.ToDateTime(txtFromDate.Text);
                        DateTime dtEnd = (Convert.ToDateTime(txtToDate.Text)).AddDays(1);
                        string username = Session["username"].ToString();
                        string colcode = Session["colcode"].ToString();
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + dtStart.ToString("yyyy-mm-dd hh:mm:ss.mmm") + ",@P_END_DATE=" + dtEnd.ToString("yyyy-mm-dd hh:mm:ss.mmm") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString();
                        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + dtStart.ToString("dd/MM/yyyy") + ",@P_END_DATE=" + dtEnd.ToString("dd/MM/yyyy") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString() + ",@P_REPORTTYPE=" + reporttype;
                        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_START_DATE=" + txtFromDate.Text.Trim() + ",@P_END_DATE=" + txtToDate.Text.Trim() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@UserName=" + Session["username"].ToString();
                        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                        //divMsg.InnerHtml += " </script>";

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                        sb.Append(@"window.open('" + url + "','','" + features + "');");
                        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
                    }
                    catch (Exception ex)
                    {
                        if (Convert.ToBoolean(Session["error"]) == true)
                            objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
                        else
                            objCommon.ShowError(Page, "Server Unavailable.");
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(UpdatePanel1, "No Record Found.", this.Page);
                    ddlDegree.SelectedIndex = 0;
                    ddlBranch.SelectedIndex = 0;
                    txtFromDate.Text = string.Empty;
                    txtToDate.Text = string.Empty;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "No Record Found.", this.Page);
                ddlDegree.SelectedIndex = 0;
                ddlBranch.SelectedIndex = 0;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
            }
        
       
    }

    public void TransferToEmail(string studname, string Regno, string oldbranch, string Degree, string remark,string College)
    {
        try
        {
            int ret = 0;
            //  string Session = ddlSession.SelectedItem.Text;
            // string sem = ddlSem.SelectedItem.Text;//kare.dileep@mastersofterp.co.in
            string useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=4");
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();



                msg.From = new MailAddress(fromAddress, "ABBS - Admission Cancellation");
                msg.To.Add(new MailAddress(useremail));



                msg.Subject = "Regarding Admission Cancellation Approval";
                //FOR MANISH : ERR: AT HTML TAGS :
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT …!!!</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>We've created a new LNMIIT user account for you. Please use the following application ID and password to sign in & complete the application.The application ID will be treated as your unique registration ID for all further proceedings.</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Your account details are :</td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2014</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail from an unattended mailbox. Please do not reply to this email.For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                // msg.Body = "<table width='500px' cellspacing='0' style='background-color: #F2F2F2'><tr><td>Dear " + firstname.ToString() + " " + lastname.ToString() + ',' + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Greetings from the LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>Thanks for registering with LNMIIT. </td></tr><tr><td></td></tr><tr><td></td></tr><tr><td >Use </td></tr></tr><tr><td></td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Application ID : " + username.ToString() + "</td></tr><tr><td></td></tr><tr><td style='font-weight:bold'>Password : " + password.ToString() + "</td></tr><tr><td>for further processing.</td></tr><tr><td></td></tr><tr><td>Thanking you,</td></tr><tr><td></td></tr>Sincerely,<tr><td></td></tr><tr><td></td></tr><tr>Convener<td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td>UG Admissions – 2015</td></tr><tr><td></td></tr><tr><td>This is an automated e-mail. Please do not reply to this email. For any further communication please write to : <a  href='ugadmissions@lnmiit.ac.in'>ugadmissions@lnmiit.ac.in</a></td></tr></table>";
                const string EmailTemplate = "<html><body>" +
                                           "<div align=\"center\">" +
                                           "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                            "<tr>" +
                                            "<td>" + "</tr>" +
                                            "<tr>" +
                                           "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                           "</tr>" +
                                           "<tr>" +
                                           "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b><br/></td>" +
                                           "</tr>" +
                                           "</table>" +
                                           "</div>" +
                                           "</body></html>";
                StringBuilder mailBody = new StringBuilder();
                //  mailBody.AppendFormat("<h1>Greetings !!</h1>");
                mailBody.AppendFormat("Dear Sir/Madam <b>" + "" + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("Below Student has opted for a Admission Cancellation that required your approval with Comments.");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b>Student Details </b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Reg. No. : </b> " + Regno + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicant Name : </b>" + studname + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Applicantion Date :</b> " + DateTime.Now.ToString("dd/MM/yyyy") + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> College Name  : </b>" + College + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Department  : </b>" + Degree + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Currenct Program : </b>" + oldbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<br />");
                //  mailBody.AppendFormat("<b>Your new Login Password is </b>");
                mailBody.AppendFormat("<b>Comments :" + remark + " </b>");
                mailBody.AppendFormat("<br />");

                string Mailbody = mailBody.ToString();
                string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                msg.IsBodyHtml = true;
                msg.Body = nMailbody;


                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);


                smtp.EnableSsl = true;
                smtp.Port = 587; // 587
                smtp.Host = "smtp.gmail.com";

                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };




                smtp.Send(msg);

                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                {
                    ret = 1;
                    //    objCommon.DisplayMessage(updSession, "Email Sent Successfully.", this.Page);
                    //Storing the details of sent email
                }

            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PresentationLayer_NewRegistration.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
