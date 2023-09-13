//========================================
// CREATED BY    : MRUNAL SINGH
// CREATION DATE : 05-08-2019
// DESCRIPTION   : USER TO RECEIVE REPLYS BY STUDENT.
//========================================
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

public partial class GrievanceRedressal_Transaction_GrievReceiveReply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();


    #region   Page Load Event

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
                    ViewState["action"] = "add";

                    DataSet ds = objCommon.FillDropDown("GRIV_GRIEVANCE_CONFIG", "ISCOMMITEETYPE", "", "", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        char committeetype = Convert.ToChar(ds.Tables[0].Rows[0]["ISCOMMITEETYPE"].ToString());
                        if (committeetype == 'Y')
                        {
                            pnlRply.Visible = true;
                            pnlGrivTypeRply.Visible = false;
                            BindlistView();
                        }
                        else
                        {
                            pnlGrivTypeRply.Visible = true;
                            pnlRply.Visible = false;
                            BindliSubGrivListView();

                        }
                    }
                    else
                    {
                        pnlRply.Visible = true;
                        pnlGrivTypeRply.Visible = false;
                        BindlistView();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceApplication.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion
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

    protected void Clear()
    {
        txtRemark.Text = string.Empty;
        rbtSatified.Checked = true;
        rbtNotSatisfied.Checked = false;
        divButton.Visible = false;
        divGR.Visible = true;
        ViewState["GAID"] = null;
        ViewState["action"] = "add";
        ViewState["GAT_ID"] = null;
        ViewState["REPLY_ID"] = null;
        pnlreply.Visible = false;
        pnlGrievance.Visible = true;
        pnlAdd.Visible = true;

    }

    private void BindlistView()
    {
        try
        {
            DataSet ds = objGrivC.GetGrievanceReplyList(Convert.ToInt32(Session["idno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvGrApplication.DataSource = ds;
                lvGrApplication.DataBind();
            }
            else
            {
                lvGrApplication.DataSource = null;
                lvGrApplication.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion


    #region Page Event

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["GRCT_ID"].ToString() == "3")
            {

                objGrivE.GAID = Convert.ToInt32(ViewState["GAID"]);
                objGrivE.GAT_ID = Convert.ToInt32(ViewState["GAT_ID"]);
                int GRCT_ID = Convert.ToInt32(ViewState["GRCT_ID"]);
                objGrivE.GR_REMARKS = txtRemark.Text == string.Empty ? "" : txtRemark.Text.Trim();
                CustomStatus cs = (CustomStatus)objGrivC.UpdateStudentRemark(objGrivE, GRCT_ID);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (rbtNotSatisfied.Checked == true)
                    {
                        rbtSatified.Checked = false;
                        btnForward.Visible = false;
                        btnSave.Enabled = true;
                    }

                    //objCommon.DisplayMessage(this.updGrievReceiveReply, "Record Saved Successfully.", this.Page);
                    //BindlistView();

                    objCommon.DisplayMessage(this.updGrievReceiveReply, "Record Saved Successfully.", this.Page);
                    Clear();
                    BindlistView();
                }

            }
            else
            {
                objGrivE.GR_STATUS = 'C';
                objGrivE.GR_REMARKS = txtRemark.Text == string.Empty ? "" : txtRemark.Text.Trim();
                objGrivE.GAID = Convert.ToInt32(ViewState["GAID"]);

                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        if (rbtSatified.Checked == true)
                        {
                            CustomStatus cs = (CustomStatus)objGrivC.AddUpGrReceiveReply(objGrivE);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindlistView();
                                objCommon.DisplayMessage(this.updGrievReceiveReply, "Record Saved Successfully.", this.Page);
                                Clear();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    #endregion



    protected void btnDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            divButton.Visible = false;
            pnlAdd.Visible = false;
            pnlreply.Visible = true;
            divDetails.Visible = true;
            pnlGrievance.Visible = false;
            divButton.Visible = true;

            ViewState["GAID"] = Convert.ToInt32(btn.CommandArgument);
            ViewState["REPLY_ID"] = Convert.ToInt32(btn.CommandName);
            ViewState["GAT_ID"] = Convert.ToInt32(btn.ToolTip);
            DataSet ds = null;
            ds = objGrivC.GetReplyDetails(Convert.ToInt32(ViewState["GAID"]), Convert.ToInt32(ViewState["REPLY_ID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGrievance.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
                lblReply.Text = ds.Tables[0].Rows[0]["REPLY"].ToString();
                lblGTtype.Text = ds.Tables[0].Rows[0]["GT_NAME"].ToString();
                ViewState["GRCT_ID"] = ds.Tables[0].Rows[0]["GRCT_ID"].ToString();
            }

            if (ViewState["GRCT_ID"].ToString() == "3")
            {
                rbtNotSatisfied.Visible = false;
            }
            else
            {
                rbtNotSatisfied.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.btnDetails_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rbtSatified_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtSatified.Checked == true)
            {
                rbtNotSatisfied.Checked = false;
                btnForward.Visible = false;
                btnSave.Enabled = true;
            }
            else
            {
                rbtSatified.Checked = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.rbtSatified_CheckedChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rbtNotSatisfied_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["GRCT_ID"].ToString() == "3")
            {
                btnForward.Visible = false;
                btnSave.Visible = true;
                rbtSatified.Checked = false;
                rbtNotSatisfied.Checked = true;

            }
            else
            {
                if (rbtNotSatisfied.Checked == true)
                {
                    rbtSatified.Checked = false;
                    btnForward.Visible = true;
                    btnSave.Enabled = false;
                }
                else
                {
                    rbtNotSatisfied.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.rbtNotSatisfied_CheckedChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnForward_Click(object sender, EventArgs e)
    {

        if (rbtNotSatisfied.Checked == true)
        {
            objGrivE.GAID = Convert.ToInt32(ViewState["GAID"]);
            objGrivE.GAT_ID = Convert.ToInt32(ViewState["GAT_ID"]);
            objGrivE.GR_REMARKS = txtRemark.Text == string.Empty ? "" : txtRemark.Text.Trim();
            CustomStatus cs = (CustomStatus)objGrivC.AddUpGrEntryAuthority(objGrivE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindlistView();
                objCommon.DisplayMessage(this.updGrievReceiveReply, "Forwarded to Next Level Committee.", this.Page);
                SendMailToDean(objGrivE.GR_REMARKS);
                Clear();
            }
        }
    }


    // This methode is use to send email to Dean/Principal.
    private void SendMailToDean(string Remark)
    {
        try
        {
            string CommitteeReply = string.Empty;
            string receiver = string.Empty;

            DataSet dsReff = objCommon.FillDropDown("REFF", "IS_GRIEVANCE_EMAIL", "EMAILSVCID, EMAILSVCPWD", "", "");
            if (dsReff.Tables[0].Rows.Count > 0)
            {
                if (dsReff.Tables[0].Rows[0]["IS_GRIEVANCE_EMAIL"].ToString() == "1")
                {
                    string fromEmailId = dsReff.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                    string fromEmailPwd = dsReff.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();


                    DataSet ds = objGrivC.GetAuthorityEmailID(Convert.ToInt32(ViewState["GRCT_ID"]), Convert.ToInt32(ViewState["GAID"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            CommitteeReply = ds.Tables[1].Rows[0]["REPLY"].ToString();
                        }
                        receiver = ds.Tables[0].Rows[0]["EMAIL_DEAN_PRINCIPAL"].ToString();

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            receiver = receiver + "," + ds.Tables[2].Rows[0]["UA_EMAIL"].ToString();
                        }
                        sendmail(fromEmailId, fromEmailPwd, receiver, "SVCE Grievance Student Remark", Remark, CommitteeReply);

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


    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string Remark, string CommitteeReply)
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

            MailBody.AppendFormat(" Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br /> Application No : " + ds.Tables[0].Rows[0]["GRIV_CODE"]);
            MailBody.AppendLine(@"<br /> Grievance Type : " + ds.Tables[0].Rows[0]["GT_NAME"]);
            MailBody.AppendLine(@"<br /> Application Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["GR_APPLICATION_DATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br /> Application By : " + ds.Tables[0].Rows[0]["STUDNAME"].ToString());
            MailBody.AppendLine(@"<br /> Details : " + ds.Tables[0].Rows[0]["GRIEVANCE"]);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> Committee Reply : " + CommitteeReply);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> Remark : " + Remark);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> Thanks And Regards");
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

    private void BindliSubGrivListView()
    {
        try
        {
            DataSet ds = objGrivC.GetSubGrievanceReplyList(Convert.ToInt32(Session["idno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSubGT.DataSource = ds;
                lvSubGT.DataBind();
            }
            else
            {
                lvSubGT.DataSource = null;
                lvSubGT.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSaveReply_Click(object sender, EventArgs e)
    {
        try
        {
            objGrivE.GR_STATUS = 'C';
            objGrivE.GR_REMARKS = txtGMark.Text == string.Empty ? "" : txtGMark.Text.Trim();
            objGrivE.GAID = Convert.ToInt32(ViewState["GAID"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (rdSat.Checked == true)
                    {
                        CustomStatus cs = (CustomStatus)objGrivC.AddUpSubGrivReceiveReply(objGrivE);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindliSubGrivListView();
                            objCommon.DisplayMessage(this.updGrievReceiveReply, "Record Saved Successfully.", this.Page);
                            GrivClear();
                        }
                    }
                }
                else
                {

                    objGrivE.GAID = Convert.ToInt32(ViewState["GAID"]);
                    objGrivE.GAT_ID = Convert.ToInt32(ViewState["GAT_ID"]);
                    int SUB_GR_ID = Convert.ToInt32(ViewState["SUB_GR_ID"]);
                    objGrivE.GR_REMARKS = txtGMark.Text == string.Empty ? "" : txtGMark.Text.Trim();
                    CustomStatus cs = (CustomStatus)objGrivC.UpdateSubGrivStudentRemark(objGrivE, SUB_GR_ID);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (rbtNotSatisfied.Checked == true)
                        {
                            rbtSatified.Checked = false;
                            btnForward.Visible = false;
                            btnSave.Enabled = true;
                        }

                        //objCommon.DisplayMessage(this.updGrievReceiveReply, "Record Saved Successfully.", this.Page);
                        //BindlistView();

                        objCommon.DisplayMessage(this.updGrievReceiveReply, "Record Saved Successfully.", this.Page);
                        GrivClear();
                        BindliSubGrivListView();


                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnForwrdReply_Click(object sender, EventArgs e)
    {
        if (rdUnSat.Checked == true)
        {
            objGrivE.GAID = Convert.ToInt32(ViewState["GAID"]);
            objGrivE.GAT_ID = Convert.ToInt32(ViewState["GAT_ID"]);
            objGrivE.GR_REMARKS = txtGMark.Text == string.Empty ? "" : txtGMark.Text.Trim();
            CustomStatus cs = (CustomStatus)objGrivC.AddUpSubGrivAuthority(objGrivE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindliSubGrivListView();
                objCommon.DisplayMessage(this.updGrievReceiveReply, "Your Application has been Forwarded.", this.Page);
                SendMailToDean(objGrivE.GR_REMARKS);
                GrivClear();
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        GrivClear();
    }
    protected void btnReplyDet_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            divbtn.Visible = false;
            pnlSt.Visible = false;
            pnlSub.Visible = true;
            divGriv.Visible = true;
            pnlTyp.Visible = false;
            divbtn.Visible = true;

            ViewState["GAID"] = Convert.ToInt32(btn.CommandArgument);
            ViewState["REPLY_ID"] = Convert.ToInt32(btn.CommandName);
            ViewState["GAT_ID"] = Convert.ToInt32(btn.ToolTip);
            DataSet ds = null;
            ds = objGrivC.GetSubGrivReplyDetails(Convert.ToInt32(ViewState["GAID"]), Convert.ToInt32(ViewState["REPLY_ID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGDetail.Text = ds.Tables[0].Rows[0]["GRIEVANCE"].ToString();
                lblGReply.Text = ds.Tables[0].Rows[0]["REPLY"].ToString();
                lblGT.Text = ds.Tables[0].Rows[0]["GT_NAME"].ToString();
                lblSubGt.Text = ds.Tables[0].Rows[0]["SUBGTNAME"].ToString();
                //ViewState["GRCT_ID"] = ds.Tables[0].Rows[0]["GRCT_ID"].ToString();
                //txtGMark.Text = ds.Tables[0].Rows[0]["STUD_REMARK"].ToString();
                char STATUS = Convert.ToChar(ds.Tables[0].Rows[0]["AUTH_STATUS"].ToString());
                if (STATUS == 'C')
                {
                    MessageBox("Your Application is Closed by Authority.");
                    btnForwrdReply.Visible = false;
                    rdUnSat.Enabled = false;
                }
                else
                {
                    btnForwrdReply.Visible = true;
                    rdUnSat.Enabled = true;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.btnDetails_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdSat_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdSat.Checked == true)
            {
                rdUnSat.Checked = false;
                btnForwrdReply.Visible = false;
                btnSaveReply.Enabled = true;
            }
            else
            {
                rdSat.Checked = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.rbtSatified_CheckedChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdUnSat_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdUnSat.Checked == true)
            {
                rdSat.Checked = false;
                btnForwrdReply.Visible = true;
                btnSaveReply.Enabled = false;
            }
            else
            {
                rdUnSat.Checked = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievReceiveReply.rbtSatified_CheckedChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GrivClear()
    {
        txtGMark.Text = string.Empty;
        rdSat.Checked = true;
        rdUnSat.Checked = false;
        divbtn.Visible = false;
        divGriv.Visible = true;
        ViewState["GAID"] = null;
        ViewState["action"] = "add";
        ViewState["GAT_ID"] = null;
        ViewState["REPLY_ID"] = null;
        pnlSub.Visible = false;
        pnlTyp.Visible = true;
        pnlSt.Visible = true;

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}