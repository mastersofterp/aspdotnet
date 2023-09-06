using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Text;


public partial class GrievanceRedressal_Transaction_GrievanceReply : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();

    #region Page Load Events
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
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //txtDate.Text = Convert.ToString(System.DateTime.Now);
                    string STATUS = objCommon.LookUp("GRIV_GR_REDRESSAL_CELL_TRAN", "[STATUS]", "UA_NO=" + Session["userno"].ToString());
                    if (STATUS == "D")
                    {
                        objCommon.DisplayMessage(this.upGrievanceReply, "You are not authorized user for GrienvceReply.", this.Page);
                        return;
                    }
                    else
                    {

                        Session["RecTblGrCell"] = null;
                        ViewState["action"] = "add";
                        DisplayMessage();
                        Reply();
                        BindlvDept();
                        BindlvInstituteL();
                        BindlvCentralL();
                        Session["RecTblGrCellI"] = null;
                        Session["RecTblGrCellC"] = null;
                        ViewState["REPLY_IDI"] = null;
                        ViewState["GAIDI"] = null;
                        ViewState["GAIDC"] = null;
                        ViewState["REPLY_IDC"] = null;
                        //ViewState["REPLY_IDG"] = null;
                        string committeetype = objCommon.LookUp("GRIV_GRIEVANCE_APPLICATION", "ISCOMMITTEETYPE", "");
                        if (committeetype == "1")
                        {
                            pnlComWise.Visible = true;
                            pnlSubGrivWise.Visible = false;
                        }
                        else
                        {
                            pnlSubGrivWise.Visible = true;
                            pnlComWise.Visible = false;
                            BindSubList();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion


    void DisplayMessage()
    {
        string GRCT_ID = string.Empty;
        DataSet ds = objGrivC.GetTabData(Convert.ToInt32(Session["userno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                if (ds.Tables[0].Rows[0]["GRCT_ID"].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "DeptLevel()", true);
                }
                else if (ds.Tables[0].Rows[0]["GRCT_ID"].ToString() == "2")
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "InstituteLevel()", true);
                }
                else if (ds.Tables[0].Rows[0]["GRCT_ID"].ToString() == "3")
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "CentralLevel()", true);
                }
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (GRCT_ID == string.Empty)
                    {
                        GRCT_ID = ds.Tables[0].Rows[i]["GRCT_ID"].ToString();
                    }
                    else
                    {
                        GRCT_ID += "," + ds.Tables[0].Rows[i]["GRCT_ID"].ToString();
                    }
                }

                if (GRCT_ID != string.Empty)
                {
                    if (GRCT_ID == "1,2")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "DeptInstiLevel()", true);
                    }
                    else if (GRCT_ID == "2,3")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "InstiCentral()", true);
                    }
                    else if (GRCT_ID == "1,3")
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "DeptCentral()", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), "key", "DeptInstiCentralLevel()", true);
                    }
                }
            }
        }
    }



    #region UserDefined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    #endregion

    #region Department Level Committee

    private void BindlvDept()
    {
        try
        {
            DataSet ds = null;
            int userno = Convert.ToInt32(Session["userno"]);
            ds = objGrivC.GetStudentGrievancesList(userno, 1);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGrReplyDept.DataSource = ds;
                lvGrReplyDept.DataBind();
            }
            else
            {
                lvGrReplyDept.DataSource = null;
                lvGrReplyDept.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.BindlvDept -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEditRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int GAID = int.Parse(btnEdit.CommandArgument);
            ViewState["GAID"] = int.Parse(btnEdit.CommandArgument);
            int STUD_IDNO = int.Parse(btnEdit.CommandName);
            string replyId = btnEdit.AlternateText;
            if (replyId != "")
            {
                ViewState["REPLY_ID"] = replyId;
            }
            else
            {
                ViewState["REPLY_ID"] = null;
            }

            ShowDetails(GAID, STUD_IDNO);
            Reply();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnEditRecord_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int GAID, int STUD_IDNO)
    {

        // DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION GA INNER JOIN ACD_STUDENT S  ON (GA.STUD_IDNO = S.IDNO) INNER JOIN ACD_DEGREE  D ON (S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SEM ON (S.SEMESTERNO = SEM.SEMESTERNO) INNER JOIN GRIV_GRIEVANCE_TYPE GT ON (GT.GRIV_ID =  GA.GRIV_ID) ", "GA.STUD_IDNO, S.STUDNAME, S.DEGREENO, S.BRANCHNO, S.SEMESTERNO, GA.MOBILE_NO,	GA.EMAIL_ID", "D.DEGREENAME, B.LONGNAME, SEM.SEMFULLNAME, GA.GRIV_ID, GA.GAID,GA.GRIEVANCE, GA.GR_APPLICATION_DATE , GT.GT_NAME", " GA.STUD_IDNO=" + STUD_IDNO + " AND GA.GAID=" + GAID, "");
        DataSet ds = null;

        ds = objGrivC.GetStudentDetailsBy(STUD_IDNO, GAID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtstudname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtsemester.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtGrievanceType.Text = ds.Tables[0].Rows[0]["GT_NAME"].ToString();
            txtGADate.Text = ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"].ToString();
            txtGrievance.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
            txtGAD.Text = ds.Tables[0].Rows[0]["GRIV_CODE"].ToString();
            txtAdmissionNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();

            string filename = ds.Tables[0].Rows[0]["GRIV_ATTACHMENT"].ToString();
            ViewState["GRIV_ATTACHMENT"] = filename.ToString();

            int idno = Convert.ToInt32(STUD_IDNO);

            if (filename != string.Empty)
            {
                lvDownload.DataSource = ds.Tables[0];
                lvDownload.DataBind();
                GetFileNamePath(filename, GAID, idno);
                divDocument.Visible = true;
            }
            else
            {
                divDocument.Visible = false;
            }

            pnlStudentDetails.Visible = true;
            pnlGrievanceReply.Visible = false;
            ViewState["GAID"] = GAID;

            if (ViewState["REPLY_ID"] != null)
            {
                DataSet dsR = objCommon.FillDropDown("GRIV_APPLICATION_REPLY", "REPLY_ID,GAID,REPLY_UANO,REPLY", "REPLY_DATE,GRCT_ID", "GAID=" + GAID + " AND REPLY_UANO=" + Convert.ToInt32(Session["userno"]), "");
                if (dsR.Tables[0].Rows.Count > 0)
                {
                    txtGrReply.Text = dsR.Tables[0].Rows[0]["REPLY"].ToString();
                }
            }
        }
        else
        {
            pnlStudentDetails.Visible = false;
            pnlGrievanceReply.Visible = true;
        }

        if (pnlStudentDetails.Visible == true)
        {
            string chkDesig = objCommon.LookUp("GRIV_GR_REDRESSAL_CELL_TRAN", "GRTRAN_ID", "DESIGNATION_ID IN (1,3) AND UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (chkDesig != "")
            {
                divReply.Visible = true;
                divButton.Visible = true;
            }
            else
            {
                divReply.Visible = false;
                divButton.Visible = false;
            }
        }
    }

    public string GetFileNamePath(object filename, object GAID, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/GrievanceRedressal/upload_documents/GrievanceApplicationDoc/" + idno.ToString() + "/GAID_" + GAID + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    //public string GetFileName(object filename, object REPLY_ID, object idno)
    //{
    //    string[] extension = filename.ToString().Split('.');
    //    if (filename != null && filename.ToString() != string.Empty)
    //        return ("~/GrievanceRedressal/upload_documents/GrievanceApplicationDoc/" + idno.ToString() + "/REPLY_ID_" + REPLY_ID + "." + extension[1].ToString().Trim());
    //    else
    //        return "";
    //}


    private void Reply()
    {
        DataSet dsReply = null;
        dsReply = objCommon.FillDropDown("GRIV_APPLICATION_REPLY AR INNER JOIN GRIV_GR_COMMITTEE_TYPE CT  ON AR.GRCT_ID=CT.GRCT_ID", "AR.REPLY,AR.GRCT_ID", "CT.GR_COMMITTEE", "AR.GAID=" + Convert.ToInt32(ViewState["GAID"]), "");
        if (dsReply.Tables[0].Rows.Count > 0)
        {
            lvCommiteeReply.DataSource = dsReply;
            lvCommiteeReply.DataBind();
            lvCommiteeReply.Visible = true;
            Session["RecTblGrCell"] = dsReply.Tables[0];
        }
        else
        {
            lvCommiteeReply.DataSource = null;
            lvCommiteeReply.DataBind();
            lvCommiteeReply.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".png", ".PNG", ".pdf", ".PDF", ".doc", ".DOC", ".txt", ".TXT", ".docx", ".DOCX" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objGrivE.REPLY_ID = 0;
            objGrivE.REPLY = txtGrReply.Text == string.Empty ? "" : txtGrReply.Text.Trim();
            objGrivE.GR_GAID = Convert.ToInt32(ViewState["GAID"]);
            objGrivE.REPLY_UANO = Convert.ToInt32(Session["userno"]);

            if (Session["idno"] != null)
            {
                if (ViewState["REPLY_ID"] == null)
                {
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlvDept();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Saved Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        Clear();
                    }
                }
                else
                {
                    objGrivE.REPLY_ID = Convert.ToInt32(ViewState["REPLY_ID"]);
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlvDept();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Updated Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        Clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This button is use to send email to student.
    private void SendMailToStudent(string Reply)
    {
        try
        {
            DataSet dsReff = objCommon.FillDropDown("REFF", "IS_GRIEVANCE_EMAIL", "EMAILSVCID, EMAILSVCPWD", "", "");
            if (dsReff.Tables[0].Rows.Count > 0)
            {
                if (dsReff.Tables[0].Rows[0]["IS_GRIEVANCE_EMAIL"].ToString() == "1")
                {
                    string fromEmailId = dsReff.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                    string fromEmailPwd = dsReff.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

                    DataSet ds = objGrivC.GetStudentEmail(Convert.ToInt32(ViewState["GAID"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string receiver = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                        sendmail(fromEmailId, fromEmailPwd, receiver, "SVCE Grievance Reply", Reply);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.SendMailToStudent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string Reply)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;
            DataSet ds = objGrivC.GetDataGrAppDeptDetails(Convert.ToInt32(ViewState["GAID"]));

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            // MailBody.AppendFormat("Dear Student, {0}\n", " ");
            MailBody.AppendFormat("Dear " + ds.Tables[0].Rows[0]["STUDNAME"].ToString() + ", {0}\n", " ");
            MailBody.AppendLine(@"<br /> Application No : " + ds.Tables[0].Rows[0]["GRIV_CODE"]);
            MailBody.AppendLine(@"<br /> Grievance Type : " + ds.Tables[0].Rows[0]["GT_NAME"]);
            MailBody.AppendLine(@"<br /> Application Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br /> Application By : " + ds.Tables[0].Rows[0]["STUDNAME"].ToString());
            MailBody.AppendLine(@"<br /> Details : " + ds.Tables[0].Rows[0]["GRIEVANCE"]);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> Reply : " + Reply);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            MailBody.AppendLine(@"<br />" + Session["userfullname"].ToString());


            mailMessage.Body = MailBody.ToString();

            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mailMessage);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }



    protected void Clear()
    {
        pnlStudentDetails.Visible = false;
        pnlGrievanceReply.Visible = true;
        ViewState["GAID"] = null;
        ViewState["action"] = "add";
        ViewState["REPLY_ID"] = null;
        txtGrReply.Text = string.Empty;
    }

    #endregion Department Level Commiitee

    #region Institute Level Committee

    private void BindlvInstituteL()
    {
        try
        {
            DataSet ds = null;
            int userno = Convert.ToInt32(Session["userno"]);
            ds = objGrivC.GetStudentGrievancesList(userno, 2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGrReplyDeptI.DataSource = ds;
                lvGrReplyDeptI.DataBind();
            }
            else
            {
                lvGrReplyDeptI.DataSource = null;
                lvGrReplyDeptI.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.BindlvInstituteL -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSaveI_Click(object sender, EventArgs e)
    {
        try
        {
            objGrivE.REPLY_ID = 0;
            objGrivE.REPLY = txtGrReplyI.Text == string.Empty ? "" : txtGrReplyI.Text.Trim();
            objGrivE.GR_GAID = Convert.ToInt32(ViewState["GAIDI"]);
            objGrivE.REPLY_UANO = Convert.ToInt32(Session["userno"]);

            if (Session["idno"] != null)
            {
                if (ViewState["REPLY_IDI"] == null)
                {
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        BindlvInstituteL();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Saved Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        ClearI();
                    }
                }
                else
                {
                    objGrivE.REPLY_ID = Convert.ToInt32(ViewState["REPLY_IDI"]);
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlvInstituteL();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Updated Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        ClearI();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelI_Click(object sender, EventArgs e)
    {
        ClearI();
    }

    private void ReplyI()
    {
        DataSet dsReplyI = null;
        dsReplyI = objCommon.FillDropDown("GRIV_APPLICATION_REPLY AR INNER JOIN GRIV_GR_COMMITTEE_TYPE CT  ON AR.GRCT_ID=CT.GRCT_ID", "AR.REPLY,AR.GRCT_ID", "CT.GR_COMMITTEE", "AR.GAID=" + Convert.ToInt32(ViewState["GAIDI"]), "");
        if (dsReplyI.Tables[0].Rows.Count > 0)
        {
            lvCommiteeReplyI.DataSource = dsReplyI;
            lvCommiteeReplyI.DataBind();
            lvCommiteeReplyI.Visible = true;
            Session["RecTblGrCellI"] = dsReplyI.Tables[0];
        }
        else
        {
            lvCommiteeReplyI.DataSource = null;
            lvCommiteeReplyI.DataBind();
            lvCommiteeReplyI.Visible = false;
        }
    }

    protected void ClearI()
    {
        pnlStudentDetailsI.Visible = false;
        pnlGrievanceReplyI.Visible = true;
        ViewState["GAIDI"] = null;
        ViewState["action"] = "add";
        ViewState["REPLY_IDI"] = null;
        txtGrReplyI.Text = string.Empty;
    }

    protected void btnEditRecordI_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int GAIDI = int.Parse(btnEdit.CommandArgument);
            ViewState["GAIDI"] = int.Parse(btnEdit.CommandArgument);
            int STUD_IDNOI = int.Parse(btnEdit.CommandName);
            string replyIdI = btnEdit.AlternateText;
            if (replyIdI != "")
            {
                ViewState["REPLY_IDI"] = replyIdI;
            }
            else
            {
                ViewState["REPLY_IDI"] = null;
            }

            ShowDetailsI(GAIDI, STUD_IDNOI);
            ReplyI();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnEditRecord_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsI(int GAIDI, int STUD_IDNOI)
    {

        // DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION GA INNER JOIN ACD_STUDENT S  ON (GA.STUD_IDNO = S.IDNO) INNER JOIN ACD_DEGREE  D ON (S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SEM ON (S.SEMESTERNO = SEM.SEMESTERNO) INNER JOIN GRIV_GRIEVANCE_TYPE GT ON (GT.GRIV_ID =  GA.GRIV_ID) ", "GA.STUD_IDNO, S.STUDNAME, S.DEGREENO, S.BRANCHNO, S.SEMESTERNO, GA.MOBILE_NO,	GA.EMAIL_ID", "D.DEGREENAME, B.LONGNAME, SEM.SEMFULLNAME, GA.GRIV_ID, GA.GAID,GA.GRIEVANCE, GA.GR_APPLICATION_DATE , GT.GT_NAME", " GA.STUD_IDNO=" + STUD_IDNO + " AND GA.GAID=" + GAID, "");
        DataSet ds = null;

        ds = objGrivC.GetStudentDetailsBy(STUD_IDNOI, GAIDI);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtstudnameI.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDegreeI.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtbranchI.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtsemesterI.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtMobileI.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtEmailI.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtGrievanceTypeI.Text = ds.Tables[0].Rows[0]["GT_NAME"].ToString();
            txtGADateI.Text = ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"].ToString();
            txtGrievanceI.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
            txtGAI.Text = ds.Tables[0].Rows[0]["GRIV_CODE"].ToString();
            txtAdmissionNoI.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();

            string filename = ds.Tables[0].Rows[0]["GRIV_ATTACHMENT"].ToString();
            ViewState["GRIV_ATTACHMENT"] = filename.ToString();

            int idno = Convert.ToInt32(STUD_IDNOI);

            if (filename != string.Empty)
            {
                lvDownloadI.DataSource = ds.Tables[0];
                lvDownloadI.DataBind();
                GetFileNamePath(filename, GAIDI, idno);
                divDocumentI.Visible = true;
            }
            else
            {
                divDocumentI.Visible = false;
            }

            pnlStudentDetailsI.Visible = true;
            pnlGrievanceReplyI.Visible = false;
            ViewState["GAIDI"] = GAIDI;


            if (ViewState["REPLY_IDI"] != null)
            {
                DataSet dsRI = objCommon.FillDropDown("GRIV_APPLICATION_REPLY", "REPLY_ID,GAID,REPLY_UANO,REPLY", "REPLY_DATE,GRCT_ID", "GAID=" + GAIDI + " AND REPLY_UANO=" + Convert.ToInt32(Session["userno"]), "");
                if (dsRI.Tables[0].Rows.Count > 0)
                {
                    txtGrReplyI.Text = dsRI.Tables[0].Rows[0]["REPLY"].ToString();
                }
            }
        }
        else
        {
            pnlStudentDetailsI.Visible = false;
            pnlGrievanceReplyI.Visible = true;
        }

        if (pnlStudentDetailsI.Visible == true)
        {
            string chkDesig = objCommon.LookUp("GRIV_GR_REDRESSAL_CELL_TRAN", "GRTRAN_ID", "DESIGNATION_ID IN (1,3) AND UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (chkDesig != "")
            {
                divReplyI.Visible = true;
                divButtonI.Visible = true;
            }
            else
            {
                divReplyI.Visible = true;
                divButtonI.Visible = true;
            }
        }
    }


    #endregion



    #region Central Level Committee

    private void BindlvCentralL()
    {
        try
        {
            DataSet ds = null;
            int userno = Convert.ToInt32(Session["userno"]);
            ds = objGrivC.GetStudentGrievancesList(userno, 3);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGrReplyDeptC.DataSource = ds;
                lvGrReplyDeptC.DataBind();
            }
            else
            {
                lvGrReplyDeptC.DataSource = null;
                lvGrReplyDeptC.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.BindlvCentralL -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSaveC_Click(object sender, EventArgs e)
    {
        try
        {
            objGrivE.REPLY_ID = 0;
            objGrivE.REPLY = txtGrReplyC.Text == string.Empty ? "" : txtGrReplyC.Text.Trim();
            objGrivE.GR_GAID = Convert.ToInt32(ViewState["GAIDC"]);
            objGrivE.REPLY_UANO = Convert.ToInt32(Session["userno"]);

            if (Session["idno"] != null)
            {
                if (ViewState["REPLY_IDC"] == null)
                {
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        BindlvCentralL();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Saved Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        ClearC();
                    }
                }
                else
                {
                    objGrivE.REPLY_ID = Convert.ToInt32(ViewState["REPLY_IDC"]);
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateGReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindlvCentralL();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Updated Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        ClearC();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelC_Click(object sender, EventArgs e)
    {
        ClearC();
    }

    private void ReplyC()
    {
        DataSet dsReplyC = null;
        dsReplyC = objCommon.FillDropDown("GRIV_APPLICATION_REPLY AR INNER JOIN GRIV_GR_COMMITTEE_TYPE CT  ON AR.GRCT_ID=CT.GRCT_ID", "AR.REPLY,AR.GRCT_ID", "CT.GR_COMMITTEE", "AR.GAID=" + Convert.ToInt32(ViewState["GAIDC"]), "");
        if (dsReplyC.Tables[0].Rows.Count > 0)
        {
            lvCommiteeReplyC.DataSource = dsReplyC;
            lvCommiteeReplyC.DataBind();
            lvCommiteeReplyC.Visible = true;
            Session["RecTblGrCellC"] = dsReplyC.Tables[0];
        }
        else
        {
            lvCommiteeReplyC.DataSource = null;
            lvCommiteeReplyC.DataBind();
            lvCommiteeReplyC.Visible = false;
        }
    }

    protected void ClearC()
    {
        pnlStudentDetailsC.Visible = false;
        pnlGrievanceReplyC.Visible = true;
        ViewState["GAIDC"] = null;
        ViewState["action"] = "add";
        ViewState["REPLY_IDC"] = null;
        txtGrReplyC.Text = string.Empty;
    }

    protected void btnEditRecordC_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int GAIDC = int.Parse(btnEdit.CommandArgument);
            ViewState["GAIDC"] = int.Parse(btnEdit.CommandArgument);
            int STUD_IDNOC = int.Parse(btnEdit.CommandName);
            string replyIdC = btnEdit.AlternateText;
            if (replyIdC != "")
            {
                ViewState["REPLY_IDC"] = replyIdC;
            }
            else
            {
                ViewState["REPLY_IDC"] = null;
            }

            ShowDetailsC(GAIDC, STUD_IDNOC);
            ReplyC();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnEditRecordC_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsC(int GAIDC, int STUD_IDNOC)
    {
        // DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION GA INNER JOIN ACD_STUDENT S  ON (GA.STUD_IDNO = S.IDNO) INNER JOIN ACD_DEGREE  D ON (S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SEM ON (S.SEMESTERNO = SEM.SEMESTERNO) INNER JOIN GRIV_GRIEVANCE_TYPE GT ON (GT.GRIV_ID =  GA.GRIV_ID) ", "GA.STUD_IDNO, S.STUDNAME, S.DEGREENO, S.BRANCHNO, S.SEMESTERNO, GA.MOBILE_NO,	GA.EMAIL_ID", "D.DEGREENAME, B.LONGNAME, SEM.SEMFULLNAME, GA.GRIV_ID, GA.GAID,GA.GRIEVANCE, GA.GR_APPLICATION_DATE , GT.GT_NAME", " GA.STUD_IDNO=" + STUD_IDNO + " AND GA.GAID=" + GAID, "");
        DataSet ds = null;

        ds = objGrivC.GetStudentDetailsBy(STUD_IDNOC, GAIDC);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtstudnameC.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDegreeC.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtbranchC.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtsemesterC.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtMobileC.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtEmailC.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtGrievanceTypeC.Text = ds.Tables[0].Rows[0]["GT_NAME"].ToString();
            txtGADateC.Text = ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"].ToString();
            txtGrievanceC.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
            txtGAC.Text = ds.Tables[0].Rows[0]["GRIV_CODE"].ToString();
            txtAdmissionNoC.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();

            string filename = ds.Tables[0].Rows[0]["GRIV_ATTACHMENT"].ToString();
            ViewState["GRIV_ATTACHMENT"] = filename.ToString();

            int idno = Convert.ToInt32(STUD_IDNOC);

            if (filename != string.Empty)
            {
                lvDownloadC.DataSource = ds.Tables[0];
                lvDownloadC.DataBind();
                GetFileNamePath(filename, GAIDC, idno);
                divDocumentC.Visible = true;
            }
            else
            {
                divDocumentC.Visible = false;
            }


            pnlStudentDetailsC.Visible = true;
            pnlGrievanceReplyC.Visible = false;
            ViewState["GAIDC"] = GAIDC;


            if (ViewState["REPLY_IDC"] != null)
            {
                // DataSet dsRC = objCommon.FillDropDown("GRIV_APPLICATION_REPLY", "REPLY_ID,GAID,REPLY_UANO,REPLY", "REPLY_DATE,GRCT_ID", "GAID=" + GAIDC + " AND REPLY_UANO=" + Convert.ToInt32(Session["userno"]), "");
                DataSet dsRC = objCommon.FillDropDown("GRIV_APPLICATION_REPLY", "REPLY_ID,GAID,REPLY_UANO,REPLY", "REPLY_DATE,GRCT_ID", "GAID=" + GAIDC + " AND REPLY_UANO=" + Convert.ToInt32(Session["userno"]) + "AND REPLY_ID=" + Convert.ToInt32(ViewState["REPLY_IDC"]), "");
                if (dsRC.Tables[0].Rows.Count > 0)
                {
                    txtGrReplyC.Text = dsRC.Tables[0].Rows[0]["REPLY"].ToString();
                }
            }
        }
        else
        {
            pnlStudentDetailsC.Visible = false;
            pnlGrievanceReplyC.Visible = true;
        }

        if (pnlStudentDetailsC.Visible == true)
        {
            string chkDesig = objCommon.LookUp("GRIV_GR_REDRESSAL_CELL_TRAN", "GRTRAN_ID", "DESIGNATION_ID IN (1,3) AND UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (chkDesig != "")
            {
                divReplyC.Visible = true;
                divButtonC.Visible = true;
            }
            else
            {
                divReplyC.Visible = true;
                divButtonC.Visible = true;
            }
        }
    }

    #endregion

    #region SubGrivReply
    protected void btnGrivReply_Click(object sender, EventArgs e)
    {
        try
        {
            objGrivE.REPLY_ID = 0;
            objGrivE.REPLY = txtSubReply.Text == string.Empty ? "" : txtSubReply.Text.Trim();
            objGrivE.GR_GAID = Convert.ToInt32(ViewState["GAID"]);
            objGrivE.REPLY_UANO = Convert.ToInt32(Session["userno"]);
            objGrivE.GAT_ID = Convert.ToInt32(ViewState["GAT_ID"]);
            if (rbtSatified.Checked == true)
            {
                objGrivE.GR_STATUS = 'C';
            }
            else
            {
                objGrivE.GR_STATUS = 'P';
            }
            if (Session["idno"] != null)
            {
                if (ViewState["REPLY_IDG"] == null)
                {
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSubGrivReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindSubList();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Saved Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        SubClear();
                    }
                }
                else
                {
                    objGrivE.REPLY_ID = Convert.ToInt32(ViewState["REPLY_IDG"]);
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSubGrivReply(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindSubList();
                        objCommon.DisplayMessage(this.upGrievanceReply, "Record Updated Successfully.", this.Page);
                        SendMailToStudent(objGrivE.REPLY);
                        SubClear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReplyCancel_Click(object sender, EventArgs e)
    {
        SubClear();
    }
    protected void btnReplyBack_Click(object sender, EventArgs e)
    {
        SubClear();
    }

    private void BindSubList()
    {
        try
        {
            DataSet ds = null;
            int userno = Convert.ToInt32(Session["userno"]);
            ds = objGrivC.GetSubGrivList(userno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStuDetail.DataSource = ds;
                lvStuDetail.DataBind();
            }
            else
            {
                lvStuDetail.DataSource = null;
                lvStuDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.BindlvDept -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    protected void btnEditGriv_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditGriv = sender as ImageButton;
            int GAID = int.Parse(btnEditGriv.CommandArgument);
            ViewState["GAID"] = int.Parse(btnEditGriv.CommandArgument);
            int STUD_IDNO = int.Parse(btnEditGriv.CommandName);
            int GAT_ID = int.Parse(btnEditGriv.ToolTip.ToString());
            ViewState["GAT_ID"] = int.Parse(btnEditGriv.ToolTip.ToString());
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION_TRAN", "*", "", "GAT_ID=" + GAT_ID, "");
            string status = ds.Tables[0].Rows[0]["COMMITTEE_STATUS"].ToString();

            if (status == "C")
            {
                btnSave.Enabled = false;
                MessageBox("This reply cannot be modified as Student has replied on this.");
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }
            string replyIdG = btnEditGriv.AlternateText;
            if (replyIdG != "")
            {
                ViewState["REPLY_IDG"] = replyIdG;
            }
            else
            {
                ViewState["REPLY_IDG"] = null;
            }

            ShowGrivDetails(GAID, STUD_IDNO);
            //Reply();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceReply.btnEditRecord_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowGrivDetails(int GAID, int STUD_IDNO)
    {

        // DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION GA INNER JOIN ACD_STUDENT S  ON (GA.STUD_IDNO = S.IDNO) INNER JOIN ACD_DEGREE  D ON (S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SEM ON (S.SEMESTERNO = SEM.SEMESTERNO) INNER JOIN GRIV_GRIEVANCE_TYPE GT ON (GT.GRIV_ID =  GA.GRIV_ID) ", "GA.STUD_IDNO, S.STUDNAME, S.DEGREENO, S.BRANCHNO, S.SEMESTERNO, GA.MOBILE_NO,	GA.EMAIL_ID", "D.DEGREENAME, B.LONGNAME, SEM.SEMFULLNAME, GA.GRIV_ID, GA.GAID,GA.GRIEVANCE, GA.GR_APPLICATION_DATE , GT.GT_NAME", " GA.STUD_IDNO=" + STUD_IDNO + " AND GA.GAID=" + GAID, "");
        DataSet ds = null;

        ds = objGrivC.GetSubGrivStudentDetailsBy(STUD_IDNO, GAID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtSubStud.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDeg.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtBranchS.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtSem.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtCont.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
            txtEmailIds.Text = ds.Tables[0].Rows[0]["EMAIL_ID"].ToString();
            txtGrivT.Text = ds.Tables[0].Rows[0]["GT_NAME"].ToString();
            txtAppDt.Text = ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"].ToString();
            txtGrivDe.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
            txtAppNo.Text = ds.Tables[0].Rows[0]["GRIV_CODE"].ToString();
            txtStuAdm.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();

            string filename = ds.Tables[0].Rows[0]["GRIV_ATTACHMENT"].ToString();
            ViewState["GRIV_ATTACHMENT"] = filename.ToString();

            int idno = Convert.ToInt32(STUD_IDNO);

            if (filename != string.Empty)
            {
                lvSubGriv.DataSource = ds.Tables[0];
                lvSubGriv.DataBind();
                GetFileNamePath(filename, GAID, idno);
                divDoc.Visible = true;
            }
            else
            {
                lvSubGriv.DataSource = null;
                lvSubGriv.DataBind();
                divDoc.Visible = false;
            }

            pnlStuList.Visible = true;
            pnldetail.Visible = false;
            ViewState["GAID"] = GAID;

           // if (ViewState["REPLY_IDG"] != null)
            //{
                // DataSet dsR = objCommon.FillDropDown("GRIV_APPLICATION_REPLY", "REPLY_ID,GAID,REPLY_UANO,REPLY", "REPLY_DATE,GRCT_ID", "GAID=" + GAID + " AND REPLY_UANO=" + Convert.ToInt32(Session["userno"]), "");
                DataSet dsR = objCommon.FillDropDown("GRIV_APPLICATION_REPLY", "REPLY_ID,GAID,REPLY_UANO,REPLY", "REPLY_DATE,GRCT_ID", "GAID=" + GAID, "");
                if (dsR.Tables[0].Rows.Count > 0)
                {
                    //txtSubReply.Text = dsR.Tables[0].Rows[0]["REPLY"].ToString();

                    lvReply.DataSource = dsR;
                    lvReply.DataBind();
                    lvReply.Visible = true;

                }
                else
                {
                    lvReply.DataSource = null;
                    lvReply.DataBind();
                    lvReply.Visible = false;
                }
            //}
            //else
            //{
            //    lvReply.DataSource = null;
            //    lvReply.DataBind();
            //    lvReply.Visible = false;
            //}

            if (ds.Tables[1].Rows.Count > 0)
            //if (ds.Tables[1].Rows[0]["STUD_REMARK"] != string.Empty)
            {
                DataSet dsStuRply = new DataSet();
                dsStuRply = objCommon.FillDropDown("GRIV_GRIEVANCE_APPLICATION GA inner join GRIV_GRIEVANCE_APPLICATION_TRAN GAT ON (GA.GAID = GAT.GAID)", "ISNULL(STUD_REMARK,'') AS STUD_REMARK", "GAT.GAID", "GAT.GAID=" + GAID, "");
                if (dsStuRply.Tables[0].Rows.Count > 0)
                {
                    lvStuRply.DataSource = dsStuRply;
                    lvStuRply.DataBind();
                    lvStuRply.Visible = true;
                    pnlstudent.Visible = true;
                }
            }
            else
            {
                lvStuRply.DataSource = null;
                lvStuRply.DataBind();
                lvStuRply.Visible = false;
                pnlstudent.Visible = false;
            }
        }
        else
        {
            pnlStuList.Visible = false;
            pnldetail.Visible = true;
        }

        if (pnlStuList.Visible == true)
        {
            string chkDesig = objCommon.LookUp("GRIV_SUB_REDRESSAL_CELL_TRAN", "SUBTRAN_ID", "SUB_DESIG_ID IN (1,3) AND SUB_UA_NO=" + Convert.ToInt32(Session["userno"]));
            if (chkDesig != "")
            {
                divGrivReply.Visible = true;
                divGrivbutton.Visible = true;
                DataSet dsRep = new DataSet();
                dsRep = objCommon.FillDropDown("GRIV_SUB_REDRESSAL_CELL_TRAN", "SUBTRAN_ID", "SUB_DESIG_ID", "SUB_DESIG_ID IN (1,3) AND SUB_UA_NO=" + Convert.ToInt32(Session["userno"]),"");
                if (Convert.ToInt32(dsRep.Tables[0].Rows[0]["SUB_DESIG_ID"].ToString()) == 1)
                {
                    rbtSatified.Enabled = true;
                }
                else
                {
                    rbtSatified.Enabled = false;
                }
            }
            else
            {
                divGrivReply.Visible = false;
                divGrivbutton.Visible = false;
            }
        }
    }

    protected void SubClear()
    {
        pnlStuList.Visible = false;
        pnldetail.Visible = true;
        ViewState["GAID"] = null;
        ViewState["action"] = "add";
        ViewState["REPLY_IDG"] = null;
        txtSubReply.Text = string.Empty;
        lvSubGriv.DataSource = null;
        lvSubGriv.DataBind();
        divDoc.Visible = false;
        ViewState["GAT_ID"] = null;
        rbtSatified.Checked = false;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

}
