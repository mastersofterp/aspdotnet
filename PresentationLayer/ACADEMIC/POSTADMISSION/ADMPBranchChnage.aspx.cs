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
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using SendGrid;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using BusinessLogicLayer.BusinessLogic.PostAdmission;

public partial class ACADEMIC_POSTADMISSION_ADMPBranchChnage : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string app_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            pnltextbox.Visible = true;
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
              
                   // PopulateDropDown();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];          
                btnSubmit.Enabled = false;

                search();
            }

            ddlBranch.Attributes.Add("onChange", "return ShowConfirm(this);");
        }

        divMsg.InnerHtml = string.Empty;

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
     
        lblNoRecords.Visible = true;
        string value = string.Empty;
        //if (ddlDropdown.SelectedIndex > 0)
        //    value = ddlDropdown.SelectedValue;
        //else
            value = txtStudent.Text;
            if (Convert.ToInt32(ddlSearch.SelectedValue) != 0 && value == "")
            {
                objCommon.DisplayMessage(updChangeBranch, "Please Insert Search value.", this.Page);
            }
            else
            {
                bindlist(ddlSearch.SelectedItem.Text, value);
            }
       // ddlDropdown.ClearSelection();
        txtStudent.Text = string.Empty;
        divdata.Visible = false;
    }

    private void bindlist(string category, string searchtext)
    {
        ADMPStudentController objSC = new ADMPStudentController();
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
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
            objCommon.DisplayMessage(updChangeBranch, "No Recored Found.", this.Page);
            //ShowMessage("No Recored Found.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewAdmBranchChange.aspx");
        }
    }
    

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    public byte[] GetImageDataForDocumentation(FileUpload fu)
    {
        if (fu.HasFile)
        {
            int ImageSize = fu.PostedFile.ContentLength;
            Stream ImageStream = fu.PostedFile.InputStream as Stream;
            byte[] ImageContent = new byte[ImageSize];
            int intStatus = ImageStream.Read(ImageContent, 0, ImageSize);      
            return ImageContent;
        }
        else
        {
            FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
            int ImageSize = (int)ff.Length;
            byte[] ImageContent = new byte[ff.Length];
            ff.Read(ImageContent, 0, ImageSize);
            ff.Close();
            ff.Dispose();
            return ImageContent;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int UserNo = 0;
        int AdmBatch = 0;
        int DegreeNo = 0;
        int NewBranchNo = 0;
        int BranchNo = 0;
        int PaymentType = 0;
        string StudeName=string.Empty;
        int CreatedBy = 0;

        if (!rdWithoutFee.Checked && !rdWithFee.Checked)
        {
            objCommon.DisplayMessage(updChangeBranch, "Branch change with fees collection Or without fees collection? Please specify with radio button selection.", this.Page);
            //ShowMessage("Branch change with fees collection Or without fees collection? Please specify with radio button selection.");
            return;
        }

        if (ddlBranch.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updChangeBranch, "Please select new branch selection.", this.Page);
            //ShowMessage("Please select new branch selection.");
            ddlBranch.Focus();
            return;
        }


        //UserNo =   Convert.ToInt32(lblUserNo.Text.ToString());
        UserNo = Convert.ToInt32(hdnUserNo.Value);
        AdmBatch= Convert.ToInt32(hdnAdmbatch.Value.ToString());      
        NewBranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        BranchNo = Convert.ToInt32(hdnBranchNo.Value);
        DegreeNo = Convert.ToInt32(hdnDegreeNo.Value);
        PaymentType = Convert.ToInt32(hdnPaymentType.Value);
        StudeName = lblName.Text;
        CreatedBy = Convert.ToInt32(Session["UseNo"]);
        ADMPStudentController objBranch = new ADMPStudentController();
        Student objStudent = new Student();
        Common objcommon = new Common();


        CustomStatus cs = (CustomStatus)objBranch.ChangeBranch(UserNo, AdmBatch, DegreeNo, BranchNo, NewBranchNo, StudeName, CreatedBy);


        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(updChangeBranch, "Branch Change Process Completed Successfully!", this.Page);
            //ShowMessage("Branch Change Process Completed Successfully!.");
           //lblMsg.Text = "Branch Change Process Completed Successfully!";      
        }
        else
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Error...";
            divdata.Visible = false;
        }
    }

    private void ClearControls()
    {
        lblName.Text = string.Empty;
        //lblRegNo.Text = string.Empty;
        //lblRollNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        txtStudent.Text = string.Empty;
        //txtNewRegNo.Text = string.Empty;
        ddlBranch.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
       // lblNewBranchFee.Text = "0";
        lblPaidFees.Text = "0";
        //lblExcessAmt.Text = "0";
        lblCurrentfeess.Text = "0";
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        lblNote.Visible = false;
        lblDegree.Text = string.Empty;
        //lblColg.Text = string.Empty;
        //chkRegno.Checked = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

  
    protected void rdWithoutFee_CheckedChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        ddlBranch.Enabled = true;
        //ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        //divnewbranchfee.Visible = dvCurrentBranchFees.Visible = true; //dvProgram.Visible=dvPaidFees.Visible = true;
    }

    protected void rdWithFee_CheckedChanged(object sender, EventArgs e)
    {
        pnlBranchChange.Visible = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        lblNote.Visible = false;
        lblMsg.Text = string.Empty;
        ddlBranch.Enabled = true;
        //ddlDegree.SelectedIndex = 0;
        //ddlCollege.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
       // divnewbranchfee.Visible = dvCurrentBranchFees.Visible = dvNewRollNo.Visible = dvPaidFees.Visible = true;
    }

    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string College_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Session["idno"].ToString());
    //        //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN vw_acd_college_degree_branch B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT (CD.BRANCHNO)", "B.LONGNAME", "CD.BRANCHNO > 0 AND ISNULL(B.ISCORE,0)=0  AND CD.DEGREENO=" + ddlProgramType.SelectedValue, "B.LONGNAME");
            
    //       // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    public void TransferToEmail(string studname, string Regno, string oldbranch, string newbranch, string remark)
    {
        try
        {
            int ret = 0;
            string useremail = "";
            //if (lblColg.ToolTip == "1" && lblBranch.ToolTip == "8")
            //    useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=6");
            //else if (lblColg.ToolTip == "2")
            //    useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=7");
            //else if (lblColg.ToolTip == "4")
            //    useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=8");
            //else
            //    useremail = objCommon.LookUp("ACD_BRANCHCHANGE_EMAIL_CONFIG", "EMAIL_ID", "CONFIG_NO=1");

            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                msg.From = new MailAddress(fromAddress, "ABBS - Branch Change");
                msg.To.Add(new MailAddress(useremail));
                msg.Subject = "Regarding Branch Change Approval";
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
                mailBody.AppendFormat("Below Student has opted for a Program change that required your approval with Comments.");
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
                //mailBody.AppendFormat("<b> College Name  : </b>" + lblColg.Text + ",</b>");
                //mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Department  : </b>" + lblDegree.Text + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> Currenct Program : </b>" + oldbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<b> New Program  : </b>" + newbranch + ",</b>");
                mailBody.AppendFormat("<br />");
                //mailBody.AppendFormat("<b>New College Name  : </b>" + lblColg.Text + ",</b>");
                //mailBody.AppendFormat("<br />");
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
                    ret = 1;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
  
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
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

        //Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        //Session["stuinfofullname"] = lnk.Text.Trim();
        //Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //ViewState["idno"] = Session["stuinfoidno"].ToString();

        divdata.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        ShowDetails(Convert.ToInt32(lnk.CommandArgument));
    }

    private void ShowDetails(int UserNo = 0)
    {
        pnlBranchChange.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        divMsg.InnerHtml = string.Empty;
        rdWithFee.Checked = false;
        rdWithoutFee.Checked = false;
        rdWithFee.Enabled = true;
        rdWithoutFee.Enabled = true;
        lblNote.Visible = false;
        ddlBranch.Enabled = true;
        lblCurrentfeess.Text = "0";
        lblPaidFees.Text = "0";
        //lblNewBranchFee.Text = "0";
        //lblExcessAmt.Text = "0";
        ViewState["IS_DEMAND_CREATED"] = null;
        pnlLV.Visible = false;
        try
        {
            //idno = Convert.ToInt32(Session["idno"]);
            ADMPStudentController objSRegist = new ADMPStudentController();
            StudentController objSC = new StudentController();
            string admcan = "0";
            decimal paidfees = 0;

            if (UserNo > 0)
            {
                

                ViewState["UserNo"] = UserNo.ToString();
                //imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + UserNo.ToString() + "&type=student";
                DataTableReader dtr = objSRegist.GetStudentDetailsForBranchChnage(UserNo);

                if (dtr.Read())
                {
                    //Session["idno"] = dtr["idno"].ToString();
                    lblName.Text = dtr["FIRSTNAME"].ToString();
                    //lblRegNo.Text = dtr["REGNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["REGNO"].ToString(); ;
                    //lblRegNo.ToolTip = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                   // ViewState["OldRollNo"] = dtr["ROLLNO"].ToString().Equals(DBNull.Value) ? "0" : dtr["ROLLNO"].ToString();
                    //lblRollNo.Text = ViewState["OldRollNo"].ToString();
                    lblUserNo.Text = dtr["ApplicationId"].ToString();
                    lblBranch.Text = dtr["LONGNAME"].ToString();
                    lblBranch.ToolTip = dtr["BRANCHNO"].ToString();
                    lblDegree.Text = dtr["DEGREENAME"].ToString();
                    lblCurrentfeess.Text = dtr["STANDARDFEE"].ToString();
                    if( Convert.ToBoolean(dtr["IsPaid"]) ==true)
                    {
                        lblPaidFees.Text = dtr["AMOUNT"].ToString();
                    }
                    else
                    {
                        lblPaidFees.Text = "0";
                    }

                    hdnBranchNo.Value = dtr["BRANCHNO"].ToString();
                    hdnUserNo.Value = dtr["USERNO"].ToString();
                    hdnAdmbatch.Value = dtr["ADMBATCH"].ToString();
                    hdnPaymentType.Value = dtr["PAYMENTTYPENO"].ToString();
                    hdnAmount.Value = dtr["AMOUNT"].ToString();
                    hdnDegreeNo.Value = dtr["DEGREENO"].ToString();
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND A.ACTIVESTATUS=1 AND A.BRANCHNO !=" + dtr["BRANCHNO"] + " AND B.DEGREENO = " + dtr["DEGREENO"], "A.LONGNAME");
                    //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_USER_BRANCH_PREF B ON (A.BRANCHNO=B.BRANCH_PREF1 OR A.BRANCHNO=B.BRANCH_PREF2 OR A.BRANCHNO=B.BRANCH_PREF3)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND A.ACTIVESTATUS=1 AND B.USERNO=" + ViewState["UserNo"] + " AND A.BRANCHNO !=" + dtr["BRANCHNO"] + " AND B.DEGREENO = " + dtr["DEGREENO"], "A.LONGNAME");

                    //lblColg.Text = dtr["COLLEGE_NAME"].ToString();
                    //lblColg.ToolTip = dtr["COLLEGE_ID"].ToString();

                    //ViewState["semesterNo"] = dtr["SEMESTERNO"].ToString();
                    //ViewState["degreeNo"] = dtr["DEGREENO"].ToString();

                    //ViewState["batchNo"] = dtr["ADMBATCH"].ToString();
                    //ViewState["COLLEGE_ID"] = dtr["COLLEGE_ID"].ToString();
                    //ViewState["BranchNoOld"] = dtr["BRANCHNO"].ToString();

                    btnSubmit.Enabled = true;
                    divdata.Visible = true;

                    //New added on 2020Nov20
                    pnlBranchChange.Visible = false;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    lblNote.Visible = false;
                    lblMsg.Text = string.Empty;
                    ddlBranch.Enabled = true;

                    //imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["idno"].ToString() + "&type=student";

                    //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "ISNULL(ActiveStatus,0)= 1 AND COLLEGE_ID > 0", "COLLEGE_ID");
                    //lblCurrentfeess.Text = objCommon.LookUp("ACD_DEMAND", "ISNULL(TOTAL_AMT,0)", "IDNO=" + Convert.ToInt32(UserNo) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");
                    //lblPaidFees.Text = objCommon.LookUp("ACD_DCR", "SUM(ISNULL(TOTAL_AMT,0))", "IDNO=" + Convert.ToInt32(UserNo) + " AND BRANCHNO=" + Convert.ToInt32(dtr["BRANCHNO"].ToString()) + "AND DEGREENO=" + Convert.ToInt32(dtr["DEGREENO"].ToString()) + " AND SEMESTERNO=" + Convert.ToInt32(dtr["SEMESTERNO"].ToString()) + "");

                    //if (lblPaidFees.Text.Equals(string.Empty))
                    //    lblPaidFees.Text = Convert.ToString(paidfees);

                    //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND A.BRANCHNO !=" + ViewState["BranchNoOld"].ToString(), "A.LONGNAME");
                }
                else
                    btnSubmit.Enabled = false;

                dtr.Close();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

  
    public void search()
    {
        txtStudent.Text = string.Empty;
        DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
            string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
            string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
            string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
            if (ddltype == "ddl")
            {
                pnltextbox.Visible = false;
                txtStudent.Visible = false;
                //pnlDropdown.Visible = true;

                divtxt.Visible = false;
                //lblDropdown.Text = ddlSearch.SelectedItem.Text;
                //objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
            }
            else
            {
                pnltextbox.Visible = true;
                divtxt.Visible = true;
                txtStudent.Visible = true;
                //pnlDropdown.Visible = false;
            }
        }
    }
    //protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //     try
    //    {
    //        if (ddlProgramType.SelectedIndex > 0)
    //        {
    //            // objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCH_CODE", "BRANCH_CODE CODE", "BRANCH_CODE <>'' AND UGPGOT=" + Convert.ToInt32(ddlProgramType.SelectedValue), "BRANCH_CODE");
    //            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
    //            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
                
    //        }
    //        ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
    //        ddlDegree.SelectedIndex = 0;
    //        ddlDegree.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ADMPBranchChnage.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

}
