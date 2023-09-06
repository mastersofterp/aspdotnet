//======================================================================================
// PROJECT NAME  : SVCE
// MODULE NAME   : ACADEMIC
// PAGE NAME     : New Student Approval
// CREATION DATE : 19/06/19
// CREATED BY    : Dipali N
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
//using ClosedXML.Excel;
using System.Net.Mail;
using CrystalDecisions.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using mastersofterp_MAKAUAT;

public partial class Academic_NewStudentApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController ObjNuc = new FeeCollectionController();
    StudentController studcon = new StudentController();

    Student objs = new Student();
    public string docname = string.Empty;
    #region Page Events

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    //USED FOR BYDEFAULT LOADING THE default PAGE
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
                    //DivStudDetails.Visible = true;
                    Page.Title = Session["coll_name"].ToString();
                    PopulateDropDown();
                    ViewState["ipAddress"] = GetUserIPAddress();
                    this.objCommon.FillDropDownList(ddlpaytype, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "", "");
                    //  this.objCommon.FillDropDownList(ddlAdmQuota, "ACD_ADMISSION_QUOTA", "DISTINCT ADMQUOTANO", "QUOTANAME", "ADMQUOTANO>0", "ADMQUOTANO");
                    objCommon.FillDropDownList(ddlAdmQuota, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0", "CATEGORYNO");
                    objCommon.FillDropDownList(ddlcategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0", "CATEGORYNO");
                    this.objCommon.FillDropDownList(ddlhostel, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "BELONGS_TO = 'H'", "RECIEPT_CODE");
                }
            }
            
            divMsg.InnerHtml = string.Empty;
            pnlBind.Visible = false;
            pnlrdb.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // IPADRESS  DETAILS ..
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

    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }

    #endregion


    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE DR ON(DR.DEGREENO=D.DEGREENO)", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO>0", "D.DEGREENO");
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void BindListView()
    {
        DataSet ds = studcon.GetAdmittedStudents(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnllist.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lvStudent.Visible = true;
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
        }
        else
        {
            objCommon.DisplayMessage(this, "No Record found", this.Page);
            pnllist.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudent.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName, string userno)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "filename=" + "StudentAdmissionConfirmation" + ".pdf";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + userno;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void AfterConfirmation(int Idno)
    {
        objs.AdmDate = txtAdmdate.Text.ToString() == string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtAdmdate.Text);
        DataSet ds = studcon.GetApprovedStudentDetails(Idno, 1, Convert.ToDateTime(objs.AdmDate), "", 0, 0, 0, "");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ViewState["STUDNAME"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblapp.Text = ds.Tables[0].Rows[0]["APPLICATIONID"].ToString();
                lblenrollno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lbldegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lbldegree.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                lblbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblbranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                lblSession.Text = ds.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                lblSession.ToolTip = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                // lblcategory.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                hdfadmbatch.Value = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                lbladmbatch.Text = ds.Tables[0].Rows[0]["ADMBATCHNAME"].ToString();
                lblsem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblsem.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                txtAdmdate.Text = ds.Tables[0].Rows[0]["ADMDATE"].ToString();
                string ptype = ds.Tables[0].Rows[0]["PTYPE"].ToString();
                lblAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                lblFeeStatus.Text = ds.Tables[0].Rows[0]["FEESTATUS"].ToString();
                lblpaidamt.Text = ds.Tables[0].Rows[0]["PAID_AMT"].ToString();
                string REMARK = objCommon.LookUp("ACD_STUDENT", "REMARK", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()));
                int status = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "isnull(ADM_CONFIRM,0)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));

                if (status == 0)
                {
                    rdopending.Checked = true;
                    TxtRemark.Enabled = true;
                }
                else if (status == 1)
                {
                    rdoconfirm.Checked = true;
                    TxtRemark.Enabled = false;
                    rdopending.Enabled = false;
                    rdoconfirm.Enabled = false;
                    rdoreject.Enabled = false;

                }
                else if (status == 2)
                {
                    rdoreject.Checked = true;
                    TxtRemark.Enabled = false;
                    rdopending.Enabled = false;
                    rdoconfirm.Enabled = false;
                    rdoreject.Enabled = false;
                }

                TxtRemark.Text = REMARK;

                if (Convert.ToInt32(ptype) > 0)
                {
                    ddlpaytype.SelectedValue = ptype;
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORYNO"].ToString()) > 0)
                {
                    ddlcategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString()) > 0)
                {
                    ddlAdmQuota.SelectedValue = ds.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString();
                }

                string hostel = (ds.Tables[0].Rows[0]["HOSTELER"].ToString());
                int Transport = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORT"].ToString());
                int scholarship = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHOLORSHIP"].ToString());
                if (Convert.ToBoolean(hostel) == false)
                {
                    rdbhostelyes.Checked = false;
                    rdbhostelNo.Checked = true;
                }
                else
                {
                    rdbhostelyes.Checked = true;
                    rdbhostelNo.Checked = false;
                }

                if (Transport == 0)
                {
                    rdbtransportyes.Checked = false;
                    rdbtransportNo.Checked = true;
                }
                else
                {
                    rdbtransportyes.Checked = true;
                    rdbtransportNo.Checked = false;
                }

                if (scholarship == 0)
                {
                    rdoscholyes.Checked = false;
                    rdoscholno.Checked = true;
                }
                else
                {
                    rdoscholyes.Checked = true;
                    rdoscholno.Checked = false;
                }

                string Status = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();

                if (Convert.ToInt32(Status) == 1 || Convert.ToInt32(Status) == 2)
                {
                    txtStudName.Enabled = false;
                    ddlAdmQuota.Enabled = false;
                    ddlcategory.Enabled = false;
                    txtAdmdate.Enabled = false;
                    btnapproval.Enabled = false;
                    ddlpaytype.Enabled = false;
                    ddlAdmQuota.Enabled = false;
                    rdbhostelNo.Enabled = false;
                    rdbhostelyes.Enabled = false;
                    rdbtransportNo.Enabled = false;
                    rdbtransportyes.Enabled = false;
                }
            }
        }
    }

    protected void lvStudent_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string admcan = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "ISNULL(ADM_CONFIRM,0)", "IDNO = " + (e.CommandArgument) + ""));
        ViewState["IDNO"] = Convert.ToInt32(e.CommandArgument);
        if (admcan == "1")
        {
            objCommon.DisplayMessage(this, "Admission Confirmed.", this.Page);
            BindListView();
            BindListViewDocumentList();
            DivDetail.Visible = false;
            AfterConfirmation(Convert.ToInt32(e.CommandArgument));
            BindLV();
            DivStudDetails.Visible = true;
            divdoc.Visible = false;
            pnlrdb.Visible = false;
           // divStatus.visible = false;
            return;
        }
        else if (admcan == "2")
        {
            if (e.CommandArgument != "0")
            {
                DivDetail.Visible = false;
                AfterConfirmation(Convert.ToInt32(e.CommandArgument));
                BindLV();
                DivStudDetails.Visible = true;
            }
            else
            {
                DivDetail.Visible = true;
                DivStudDetails.Visible = false;
            }
        }
        else
        {
            if (e.CommandArgument != "0")
            {
                DivDetail.Visible = false;
                GetStudentDetails(Convert.ToInt32(e.CommandArgument));
                BindLV();
                DivStudDetails.Visible = true;
            }
            else
            {
                DivDetail.Visible = true;
                DivStudDetails.Visible = false;
            }
        }
    }

    //button approve is used for to Approve the register student details.
    protected void btnapproval_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAdmdate.Text != string.Empty)
            {
                int status = 0;
                if (rdopending.Checked)
                    status = 0;
                else if (rdoconfirm.Checked)
                    status = 1;
                else if (rdoreject.Checked)
                    status = 2;
                else
                    objCommon.DisplayMessage(this, "Please Select Admission Status.", this.Page);

                if (TxtRemark.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this, "Please Enter Remark.", this.Page);
                    return;
                }


                DataSet ds = studcon.GetApprovedStudentDetails(Convert.ToInt32(ViewState["IDNO"].ToString()), 2, Convert.ToDateTime(txtAdmdate.Text), txtStudName.Text.Trim(), Convert.ToInt32(ddlcategory.SelectedValue), Convert.ToInt32(ddlAdmQuota.SelectedValue), status, TxtRemark.Text.Trim());

                objCommon.DisplayMessage(this.pnlFeeTable, "Student Approved Sucessfully! \\n Admission No. is : " + lblenrollno.Text + "\\n", this);
                

               
                //TO SEND MAIL TO THE STUDENT//
                //string subject = "Admission Confirmation";
                ////string message = "Thanks for showing interest in RCPIT. Your Account has been created successfully!<br/>Login with Username : <b>" + srnno + "</b> and Password : <b>" + srnno.ToString() + "</b><br/> Follow this link for further process <b>https://mnr.mastersofterp.in/</b>";
                //string message = "Dear " + txtStudName.Text + " your Admission has been approved successfully! Your Admission No. is : " + lblenrollno.Text + "";

                //// int status = TransferToEmail(message, objS.EmailID, subject);
                //Execute(message, objS.EmailID, subject).Wait();

                //if (txtMobile.Text != "")
                //{
                //    objCommon.SendSMS(txtMobile.Text, "Dear " + txtStudName.Text + " your Admission has been approved successfully! Your Admission No. is : " + lblenrollno.Text + "");
                //}
               
                btnapproval.Enabled = false;
                ddlpaytype.Enabled = false;
                ddlAdmQuota.Enabled = false;
                rdbhostelNo.Enabled = false;
                rdbhostelyes.Enabled = false;
                rdbtransportNo.Enabled = false;
                rdbtransportyes.Enabled = false;
                
            }
            else
            {
                objCommon.DisplayMessage(this.pnlFeeTable, "Please Enter Student Admission Date !!", this);
            }

            BindListViewDocumentList();
            pnlBind.Visible = true;
            pnlrdb.Visible = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //refresh current page or reload current page
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //bind Registerd Student deatils on lables and and image  and showing demand details.
    public void GetStudentDetails(int idno)
    {
        if (idno > 0)
        {
            //if (!txtAdmdate.Text.Trim().Equals(string.Empty)) objs.AdmDate = Convert.ToDateTime(txtAdmdate.Text.Trim());

            objs.AdmDate = txtAdmdate.Text.ToString() == string.Empty ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(txtAdmdate.Text);
            DataSet ds = studcon.GetApprovedStudentDetails(idno, 1, Convert.ToDateTime(objs.AdmDate), "", 0, 0, 0, "");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                    txtStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    ViewState["STUDNAME"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblapp.Text = ds.Tables[0].Rows[0]["APPLICATIONID"].ToString();
                    lblenrollno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lbldegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    lbldegree.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                    lblbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblbranch.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    ViewState["EMAILID"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblSession.Text = ds.Tables[0].Rows[0]["SESSIONNAME"].ToString();
                    lblSession.ToolTip = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                    // lblcategory.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                    hdfadmbatch.Value = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lbladmbatch.Text = ds.Tables[0].Rows[0]["ADMBATCHNAME"].ToString();
                    lblsem.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblsem.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    txtAdmdate.Text = ds.Tables[0].Rows[0]["ADMDATE"].ToString();
                    string ptype = ds.Tables[0].Rows[0]["PTYPE"].ToString();
                    lblAmount.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
                    lblFeeStatus.Text = ds.Tables[0].Rows[0]["FEESTATUS"].ToString();
                    lblpaidamt.Text = ds.Tables[0].Rows[0]["PAID_AMT"].ToString();
                    string REMARK = objCommon.LookUp("ACD_STUDENT", "REMARK", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()));
                    int status = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(ADM_CONFIRM,0)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));

                    if (status == 0)
                    {
                        rdopending.Checked = true;
                        TxtRemark.Enabled = true;
                    }
                    else if (status == 1)
                    {
                        rdoconfirm.Checked = true;
                        TxtRemark.Enabled = false;
                        rdopending.Enabled = false;
                        rdoconfirm.Enabled = false;
                        rdoreject.Enabled = false;

                    }
                    else if (status == 2)
                    {
                        rdoreject.Checked = true;
                        TxtRemark.Enabled = false;
                        rdopending.Enabled = false;
                        rdoconfirm.Enabled = false;
                        rdoreject.Enabled = false;
                    }

                    TxtRemark.Text = REMARK;

                    if (Convert.ToInt32(ptype) > 0)
                    {
                        ddlpaytype.SelectedValue = ptype;
                    }

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORYNO"].ToString()) > 0)
                    {
                        ddlcategory.SelectedValue = ds.Tables[0].Rows[0]["CATEGORYNO"].ToString();
                    }

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString()) > 0)
                    {
                        ddlAdmQuota.SelectedValue = ds.Tables[0].Rows[0]["ADMCATEGORYNO"].ToString();
                    }

                    string hostel = (ds.Tables[0].Rows[0]["HOSTELER"].ToString());
                    int Transport = Convert.ToInt32(ds.Tables[0].Rows[0]["TRANSPORT"].ToString());
                    int Scholarship = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHOLORSHIP"].ToString());

                    if (Convert.ToBoolean(hostel) == false)
                    {
                        rdbhostelyes.Checked = false;
                        rdbhostelNo.Checked = true;
                    }
                    else
                    {
                        rdbhostelyes.Checked = true;
                        rdbhostelNo.Checked = false;
                    }

                    if (Transport == 0)
                    {
                        rdbtransportyes.Checked = false;
                        rdbtransportNo.Checked = true;
                    }
                    else
                    {
                        rdbtransportyes.Checked = true;
                        rdbtransportNo.Checked = false;
                    }

                    if (Scholarship == 0)
                    {
                        rdoscholyes.Checked = false;
                        rdoscholno.Checked = true;
                    }
                    else
                    {
                        rdoscholyes.Checked = true;
                        rdoscholno.Checked = false;
                    }

                    string Status = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();

                    if (Convert.ToInt32(Status) == 1)
                    {
                        objCommon.DisplayMessage(this.pnlFeeTable, "Student Approved Sucessfully! \\n Admission No. is : " + lblenrollno.Text + "\\n.", this);

                        txtStudName.Enabled = false;
                        ddlAdmQuota.Enabled = false;
                        ddlcategory.Enabled = false;
                        txtAdmdate.Enabled = false;
                        btnapproval.Enabled = false;
                        ddlpaytype.Enabled = false;
                        ddlAdmQuota.Enabled = false;
                        rdbhostelNo.Enabled = false;
                        rdbhostelyes.Enabled = false;
                        rdbtransportNo.Enabled = false;
                        rdbtransportyes.Enabled = false;
                    }
                    else
                    {
                        btnapproval.Enabled = true;
                    }

                    GetCategory(ds.Tables[0].Rows[0]["TEMP_CATEGORY"].ToString());

                }
                else
                {
                    objCommon.DisplayMessage(this.pnlFeeTable, "Student Registration is Not Done or Student Completed all Registration Process!", this);
                    clear();
                    txtEnrollmentSearch.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.pnlFeeTable, "Please Enter Valid Admission ID.!", this);
            clear();
            return;
        }
    }

    //used for sending email for addmissiuon approve successfuly.
    public void SendEmail(string mailId, string name)
    {
        try
        {
            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", "");

            // any address where the email will be sending
            var toAddress = EMAILID.Trim();

            //Password of your gmail address
            var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");

            // Passing the values and make a email formate to display
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "MNR");
            msg.To.Add(new MailAddress(toAddress));
            msg.Subject = "Admission Approved Successfully";

            using (StreamReader reader = new StreamReader(Server.MapPath("~/approval_template.html")))
            {
                msg.Body = reader.ReadToEnd();
            }

            msg.Body = msg.Body.Replace("{Name}", name);
            msg.Body = msg.Body.Replace("{AdmissionNumber}", lblenrollno.Text);

            msg.IsBodyHtml = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    //used for reset all the labels controllers.
    public void clear()
    {
        lblname.Text = string.Empty;
        lblapp.Text = string.Empty;
        lbldegree.Text = string.Empty;
        lblbranch.Text = string.Empty;
        lblmobile.Text = string.Empty;
        lblEmail.Text = string.Empty;
        lblSession.Text = string.Empty;
        //lblcategory.Text = string.Empty;
        lbladmbatch.Text = string.Empty;
        lblsem.Text = string.Empty;
        txtAdmdate.Text = string.Empty;
        rdbhostelNo.Checked = false;
        rdbhostelyes.Checked = false;
        rdbtransportyes.Checked = false;
        rdbtransportNo.Checked = false;
        ddlpaytype.SelectedIndex = 0;
        ddlAdmQuota.SelectedIndex = 0;
        lbladmquota.Text = string.Empty;
        lbllastname.Text = string.Empty;
        lblenrollno.Text = string.Empty;
        //imgpreview.Dispose();
        //imgpreview.ImageUrl = null;
        txtStudName.Text = string.Empty;
        txtStudInitial.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
    }

    protected void rdbhostelyes_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbhostelyes.Checked == true) { ddlhostel.Enabled = true; ddlhostel.SelectedIndex = 0; } else { ddlhostel.Enabled = false; ddlhostel.SelectedIndex = 0; }
    }

    static async Task Execute(string Message, string toEmailId, string idno, string Colcode, string College_id)
    {
        ////string LogoPath = System.Web.HttpContext.Current.Server.MapPath("~/IMAGES/MNRLogo.jpg");
        //Byte[] Imgbytes = File.ReadAllBytes(LogoPath);
        //string Imgfile = Convert.ToBase64String(Imgbytes);

        MemoryStream oAttachment = ShowGeneralExportReportForMail("Reports,Academic,rptStudentConfirmReport.rpt", "@P_IDNO=" + idno + ",@P_COLLEGE_CODE=" + Colcode + ",@P_COLLEGE_ID=" + College_id);


        var bytesRpt = oAttachment.ToArray();
        var fileRpt = Convert.ToBase64String(bytesRpt);

        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var toAddress = new MailAddress(toEmailId, "");
            //var apiKey = "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";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var subject = "Student Registration Approval";
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
            //var attachment = new SendGrid.Helpers.Mail.Attachment();
            //{
            //    Content = Imgfile,
            //    Type = "image/png",
            //    Filename = "Logo.png",
            //    Disposition = "inline",
            //    ContentId = "Logo"
            //};
            //attachments.Add(attachment);
            //msg.AddAttachments(attachments);

            msg.AddAttachment("AdmissionSlip.pdf", fileRpt);

            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    static private MemoryStream ShowGeneralExportReportForMail(string path, string paramString)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string paramName = string.Empty;
        string value = string.Empty;
        string reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptStudentConfirmReport.rpt");

        customReport.Load(reportPath);

        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');

                paramName = string.Empty;
                value = string.Empty;
                string reportName = "MainRpt";

                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        /// set login details & db details for report document
        ConfigureCrystalReports(customReport);

        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }
        customReport.SetParameterValue(paramName, value);

        oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        return oStream;
    }

    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string reportTitle = "AdmissionSlip";
        string rptFileName = "rptStudAdmSlip_New.rpt";
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.pnlFeeTable, this.pnlFeeTable.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    static async Task ParentExecute(string Message, string toEmailId)
    {
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var toAddress = new MailAddress(toEmailId, "");
            //var apiKey = "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";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["MASTERSOFT_GRID_MAILID"].ToString(), "MNR");
            var subject = "Parent Login Details";
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlbranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0", "B.LONGNAME");
            ddlbranch.Focus();
        }
        else
        {
            ddlbranch.SelectedIndex = 0;
            objCommon.DisplayMessage(this, "Please Select Degree!", this.Page);
            return;
        }

        ddlbranch.SelectedIndex = 0;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btncancel1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    protected void btnreport1_Click(object sender, EventArgs e)
    {

        try
        {
            StudentCampExcel();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void StudentCampExcel()
    {
        DataSet ds = studcon.GetAdmittedStudentsReport(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + "ConfirmedStudentList.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
                return;
            }

        }
        else
        {
            objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
            return;
        }
    }

    private void BindLV()
    {
        string isUploaded = string.Empty;
        // get list of select document......
        //DataSet ds = objstud.GetAllselectDocument(Convert.ToString(txtTempIdno.Text));


        DataSet ds = null;
        if (Session["usertype"].ToString() != "2")
        {
            ds = studcon.GetUploadedDoclist(Convert.ToInt32(ViewState["IDNO"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    isUploaded += dr["DOCUMENTNO"].ToString() + ", ";
                }

                string realUploaded = isUploaded.TrimEnd().TrimEnd(',');
                ViewState["UploadedString"] = realUploaded;

                lvDocumentsAdmin.DataSource = ds;
                lvDocumentsAdmin.DataBind();

            }

            foreach (ListViewItem Item in lvDocumentsAdmin.Items)
            {
                LinkButton lnkDownloadDoc = Item.FindControl("lnkDownloadDoc") as LinkButton;
                ImageButton imgbtnPrevDoc = Item.FindControl("imgbtnPrevDoc") as ImageButton;
            }

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                ImageButton ImgPhoto = lvDocumentsAdmin.Items[i].FindControl("lnkDownloadDoc") as ImageButton;
            }

        }
    }

    protected void lvDocumentsAdmin_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;
                string compareString = ViewState["UploadedString"].ToString();
                string stringToCompare = dr["DOCUMENTNO"].ToString();
                docname = dr["DOCUMENT_NAME"].ToString();
                int consist = 0;

                consist = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + "") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENTNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + ""));

                if (compareString.Contains(stringToCompare) && consist != 0)
                {
                    if (dr["IDNO"].ToString() != string.Empty || dr["IDNO"] != DBNull.Value)
                    {
                        ((Label)e.Item.FindControl("lblStatus")).Text = "Submitted";
                        ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                        ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                        //((LinkButton)e.Item.FindControl("lnkDownloadDoc")).Visible = true;
                        ((ImageButton)e.Item.FindControl("imgbtnPrevDoc1")).Visible = true;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                        ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                        ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                    }
                }
                else
                {
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void imgbtnPrevDoc1_Click(object sender, ImageClickEventArgs e)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

        ImageButton lnkView = (ImageButton)(sender);
        string path = url + "UPLOAD_FILES/STUDENT_DOCUMENT/" + lnkView.CommandArgument;

        //  iframeView.Attributes.Add("src", path);
        iframeView.Src = path;
    }

    protected void lnkDownloadDoc_Click(object sender, EventArgs e)
    {
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;
        string studid = ViewState["IDNO"].ToString();
        string documentname = objCommon.LookUp("ACD_ADM_DOCUMENT_LIST", "DOCUMENT_NAME", "DOCUMENTNO='" + filename + "' AND IDNO=" + studid);
        string filepath = Server.MapPath("~//UPLOAD_FILES//STUDENT_DOCUMENT/");

        string file = filepath + documentname;

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath + documentname);

        // Checking if file exists
        if (myfile.Exists)
        {
            Response.Clear();
            Response.ClearHeaders();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + documentname);
            Response.TransmitFile(file);
            Response.Flush();
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Document Not Uploaded by Student.", this.Page);
        }
    }

    private void GetCategory(string Category)
    {
        string cateno = objCommon.LookUp("ACD_CATEGORY", "CATEGORYNO", "CATEGORY='" + Category.Trim() + "'");
        if (cateno == "" || cateno == null || cateno == string.Empty)
        {
            ddlAdmQuota.SelectedIndex = 0;
        }
        else
        {
            ddlAdmQuota.SelectedValue = cateno;
        }
    }

     protected void BindListViewDocumentList()
        {

        //string Enrollment = txtEnrollment.Text.Trim();
        //string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "(ENROLLNO='" + Enrollment + "' OR APPLICATIONID='" + txtEnrollment.Text.Trim() + "')");
        DataSet ds = null;
        string idno = ViewState["IDNO"].ToString();
        //string idno = Session["stuinfoidno"].ToString();
        ds = studcon.GetDocument(Convert.ToString(idno));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

            lvBinddata.DataSource = ds;
            lvBinddata.DataBind();
            lvBinddata.Visible = true;

            }
        else
            {
            lvBinddata.Visible = false;
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();


            }
        //foreach (ListViewDataItem lvitem in lvBinddata.Items)
        //{
        //    //TextBox remark = lvitem.FindControl("txtDoc") as TextBox;
        //   // remark.Enabled = true;
        //   // remark.Enabled = false;
        //}

        }

     protected void lvBinddata_ItemDataBound(object sender, ListViewItemEventArgs e)
         {
         //foreach (ListViewDataItem lvitem in lvBinddata.Items)
         //{
         //    TextBox date = lvitem.FindControl("txtDate") as TextBox;
         //    DateTime dt = DateTime.Now;
         //    date.Text = dt.ToString();
         //   //Convert.ToDateTime(date.Text);



         ListViewDataItem dataItem = (ListViewDataItem)e.Item;
         TextBox date = (TextBox)e.Item.FindControl("txtDate");
         DateTime dt = DateTime.Now.Date;
         date.TextMode = TextBoxMode.SingleLine;
         date.Text = string.Format("{0:dd/MM/yyyy}", dt);

         }

     protected void btnSubmit_Click(object sender, EventArgs e)
         {
         int documentstatus = 0;
         int status = 0;
         int count = 0;
         if (rdoSubmit.Checked == true)
             {
             foreach (ListViewDataItem lvitem in lvBinddata.Items)
                 {
                 CheckBox chkBox = lvitem.FindControl("chkDocsingle") as CheckBox;
                 string hdnf = (lvitem.FindControl("HiddenField1") as HiddenField).Value;
                 string Remark = (lvitem.FindControl("txtDoc") as TextBox).Text;
                 string docname = (lvitem.FindControl("lblDocument") as Label).Text;

                 string docno = hdnf;
                 string date = (lvitem.FindControl("txtDate") as TextBox).Text;

                 if (rdoSubmit.Checked)
                     {
                     status = 1;
                     documentstatus = 1;
                    // div1.Visible = true;
                     lvBinddata.Visible = true;
                     }

                 if (chkBox.Checked == true)
                     {
                     int retval = studcon.UpdateAcd_Document(Convert.ToString(Remark), Convert.ToString(docno), Convert.ToInt32(status), Convert.ToDateTime(date), Convert.ToInt32(ViewState["IDNO"].ToString()), documentstatus);
                     if (retval == 0)
                         {
                         objCommon.DisplayMessage(this, "Data Not Saved!!", this.Page);
                         }
                     else
                         {
                         //objCommon.DisplayMessage(this, "Data save Successfully...!!", this.Page);
                         count++;
                         }

                     }
                 //else if (chkBox.Checked == false)
                 //{

                 //    objCommon.DisplayMessage(this, "Please Select Atleast One Document...!!", this.Page);
                 //    return;
                 //}
                 }
             if (count > 0)
                 {
                 objCommon.DisplayMessage(this, "Data save Successfully...!!", this.Page);
                // div1.Visible = true;
                 lvBinddata.Visible = true;
                 //return;

                 }
             //else if (count < 0)
             //{
             //    objCommon.DisplayMessage(this, "Please Select Atleast One Document...!!", this.Page);
             //    return;
             //}
           // BindListView();]
             BindListViewDocumentList();
             //div1.Visible = true;
             //lvBinddata.Visible = true;                

             }
         else
             {
             foreach (ListViewDataItem lvitem in lvBinddata.Items)
                 {
                 CheckBox chkBox = lvitem.FindControl("chkDocsingle") as CheckBox;
                 string hdnf = (lvitem.FindControl("HiddenField1") as HiddenField).Value;
                 string Remark = (lvitem.FindControl("txtDoc") as TextBox).Text;
                 string docname = (lvitem.FindControl("lblDocument") as Label).Text;
                 string docno = hdnf;

                 string date = (lvitem.FindControl("txtDate") as TextBox).Text;

                 if (rdoReturn.Checked)
                     {
                     status = 0;
                     documentstatus = 2;
                   //  div1.Visible = true;
                     lvBinddata.Visible = true;
                     }


                 if (chkBox.Checked == true)
                     {

                     int retval = studcon.UpdateAcd_Document(Convert.ToString(Remark), Convert.ToString(docno), Convert.ToInt32(status), Convert.ToDateTime(date), Convert.ToInt32(ViewState["IDNO"].ToString()), documentstatus);

                     // if (retval == 0)
                     //{
                     //    objCommon.DisplayMessage(this.Page, "Data Not Saved!!", this.Page);
                     //    return;
                     //}
                     //else
                     //{
                     objCommon.DisplayMessage(this.Page, "Data save Successfully...!!", this.Page);

                     //    return;
                     //}

                     }

                 }
             //BindListView();
             BindListViewDocumentList();
             //div1.Visible = true;
             lvBinddata.Visible = true;

             }

         //div1.Visible = false;
         //lvBinddata.Visible = false;
         //divDetails.Visible = false;
         //pnlrdb.Visible = false;
         //div1.Visible = false;
         //pnlbtn.Visible = false;
         //divpanel.Visible = false;
         //div1.Visible = true;
         //lvBinddata.Visible = true;
         // ddlSearch.SelectedIndex = 0;
         // upnllvBinddata.Visible = false;
         pnlBind.Visible = true;
         pnlrdb.Visible = true;
         }
     protected void btnReportdoc_Click(object sender, EventArgs e)
         {
         try
             {
             ShowReportdoc("Document_Status", "rptStudDocumentStatus_New.rpt");

           //  div1.Visible = false;
             //lvBinddata.Visible = true;
             //divDetails.Visible = false;
             pnlrdb.Visible = true;
             pnlBind.Visible = true;
             pnlrdb.Visible = true;
             lvBinddata.Visible = true;
           //  div1.Visible = false;
            // pnlbtn.Visible = false;
            // ddlSearch.SelectedIndex = 0;
            // upnllvBinddata.Visible = false;
             }
         catch (Exception ex)
             {
             throw;
             }
         }

     private void ShowReportdoc(string reportTitle, string rptFileName)
         {
         try
             {

             string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
             url += "Reports/CommonReport.aspx?";
             url += "pagetitle=" + reportTitle;
             url += "&path=~,Reports,Academic," + rptFileName;
             url += "&param=@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",Username=" + Session["username"].ToString() + ",@P_IDNO=" + ViewState["IDNO"].ToString();

             //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
             //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
             //divMsg.InnerHtml += " </script>";
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
             sb.Append(@"window.open('" + url + "','','" + features + "');");
             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "controlJSScript", sb.ToString(), true);
             pnlBind.Visible = true;
             pnlrdb.Visible = true;
             lvBinddata.Visible = true;
             }
         catch (Exception ex)
             {
             throw;
             }
         }
     protected void btnCanceldoc_Click(object sender, EventArgs e)
         {
         //txtEnrollment.Text = string.Empty;
         //txtEnrollment.Focus();
         lvBinddata.DataSource = null;
         lvBinddata.DataBind();
        // div1.Visible = false;
         lvBinddata.Visible = false;
        // divDetails.Visible = false;
         pnlrdb.Visible = false;
         lvBinddata.Visible = false;
        // div1.Visible = false;
        // pnlbtn.Visible = false;
        // ddlSearch.SelectedIndex = 0;
         pnlBind.Visible = true;
         pnlrdb.Visible = true;
         }
}