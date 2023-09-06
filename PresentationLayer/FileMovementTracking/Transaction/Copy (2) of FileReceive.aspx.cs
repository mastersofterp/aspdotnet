//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 26-SEP-2015                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.FileMovement;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;


public partial class FileMovementTracking_Transaction_FileReceive : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FileMovement objFMov = new FileMovement();
    FileMovementController objFController = new FileMovementController();
    Sms objSms = new Sms();
    SmsController objSmsController = new SmsController();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";
    public string path = string.Empty;
    public string dbPath = string.Empty;
    DataTable DocFinalListTbl = new DataTable("DocTbl");



    #region PageLoad Events
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
                    Session["idno"] = Session["userno"];
                    //Page Authorization
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    txtDesc.Attributes.Add("readonly", "true");
                    ViewState["FILE_MOVID"] = null;
                    Session["dtTable"] = null;

                    FillUserRole();    
                    BindListOfFileMovement(0);
                    //objCommon.FillDropDownList(ddlSection, "FILE_SECTIONMASTER", "SECTION_ID", "SECTION_NAME", "POST_TO_DATE IS NULL AND RECEIVER_ID !=" + Convert.ToInt32(Session["userno"]), "SECTION_ID");
                    trDept.Visible = false;
                    divButtons.Visible = false;
                    Session["OperType"] = null;
                    Session["FILE_ID"] = null;                      
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Web Method


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSectionName(string prefixText)
    {
        List<string> SectionUserName = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            FileMovementController objFController = new FileMovementController();
            ds = objFController.FillSectionUserNames(prefixText);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SectionUserName.Add(ds.Tables[0].Rows[i]["SECTION_ID"].ToString() + "---------*" + ds.Tables[0].Rows[i]["SECTION_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return SectionUserName;
    }




    #endregion

    #region User-Define Methods

    // This method is used to check page authorization.
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

    // this method is used to clear the controls.
    private void Clear()
    {
        ViewState["FILE_MOVID"] = null;
        Session["RecTbl"] = null;
        txtRemark.Text = string.Empty;
        pnlDetails.Visible = false;
        trDept.Visible = false;
        divButtons.Visible = false;
        pnlList.Visible = true;
        pnlRadioButton.Visible = true;
        pnlDropDownUserRole.Visible = true;
        pnlUploadFiles.Visible = false;
        lvFinalList.Visible = false;
        lvNewFiles.DataSource = null;
        lvNewFiles.DataBind();
        pnlNewFiles.Visible = false;
        rbtForward.Checked = false;
        rbtReturn.Checked = false;
        rbtComplete.Checked = false;
        Session["FILE_ID"] = null;
        rdbApproved.Checked = false;
        rdbNotApproved.Checked = false;
    }

    private void FillUserRole()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("FILE_SECTIONMASTER CROSS APPLY dbo.split(SECTION_USER_ROLE, ',') s	INNER JOIN FILE_ROLE_MASTER RM ON (s.Value = RM.ROLE_ID)", "S.Value AS SECTION_USER_ROLE", "RM.ROLENAME", "RECEIVER_ID = " + Convert.ToInt32(Session["userno"]), "");
            ddlUserRole.DataSource = ds;
            ddlUserRole.DataValueField = ds.Tables[0].Columns["SECTION_USER_ROLE"].ToString();
            ddlUserRole.DataTextField = ds.Tables[0].Columns["ROLENAME"].ToString();
            ddlUserRole.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.FillUserRole -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    // this method is used to display the file received entry list.
    private void BindListOfFileMovement(int ROLE_ID)
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["userno"]) == 1)
            {
                //ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON (FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID)", "FT.FMTRAN_ID, FP.FILE_MOVID, FS.SECTION_ID, FM.FILE_CODE, FP.FILEPATH,FM.FILE_ID, FM.FILE_NAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING' WHEN 'F' THEN 'FORWARD' WHEN 'R' THEN 'RETURN' WHEN 'I' THEN 'IN PROCESS' END) AS STATUS, FT.STATUS AS STAT, FT.RECEIVER_ID , FT.REMARK ", "", "", "FMTRAN_ID");
                ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON (FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID) INNER JOIN User_Acc U ON (FM.USERNO = U.UA_NO)", "FT.FMTRAN_ID, FP.FILE_MOVID, FS.SECTION_ID, FM.FILE_CODE, FP.FILEPATH,FM.FILE_ID, FM.FILE_NAME,  FM.USERNO,U.UA_FULLNAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING' WHEN 'F' THEN 'FORWARD' WHEN 'R' THEN 'RETURN' WHEN 'I' THEN 'IN PROCESS' END) AS STATUS, FT.STATUS AS STAT, FT.RECEIVER_ID , FT.REMARK_RECEIVE, FT.RECEIVED_DATE, FM.FILE_MASTER as F_NAME,  FT.REMARK_FOR_RET, RETURN_DATE ", "", "", "FMTRAN_ID");
            }
            else
            {
                ds = objFController.GetReceiveDetails(Convert.ToInt32(Session["userno"]), ROLE_ID);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFileMovement.DataSource = ds.Tables[0];
                lvFileMovement.DataBind();
                pnlList.Visible = true;
                pnlRadioButton.Visible = true;
            }
            else
            {
                lvFileMovement.DataSource = null;
                lvFileMovement.DataBind();
                pnlList.Visible = false;
                pnlRadioButton.Visible = true;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                lvInactiveFile.DataSource = ds.Tables[1];
                lvInactiveFile.DataBind();                
            }
            else
            {
                lvInactiveFile.DataSource = null;
                lvInactiveFile.DataBind();              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This button is used to show the details of the particular selected record.
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
      
        try
        {
            ViewState["FILE_MOVID"] = Convert.ToInt32(btn.ToolTip);
            ViewState["SECTIONNO"] = Convert.ToInt32(btn.CommandArgument);
            ViewState["FILE_ID"] = Convert.ToInt32(btn.CommandName);
            Session["FILE_ID"] = Convert.ToInt32(btn.CommandName);

            // to receive file automatically on details click
            objFMov.FILE_MOVID = Convert.ToInt32(btn.ToolTip);
            objFMov.SECTIONNO = Convert.ToInt32(btn.CommandArgument);
            objFMov.FILE_ID = Convert.ToInt32(btn.CommandName);
            objFMov.FSTATUS = "I";
            objFMov.USERNO = Convert.ToInt32(Session["userno"]);
            objFMov.REMARK = "Received By Section User " + Session["userfullname"].ToString();
            objFController.AddUpdateFileReceiveDetails(objFMov);



            ShowDetails(Convert.ToInt32(ViewState["FILE_ID"]));
            pnlDetails.Visible = true;
            divButtons.Visible = true;
            pnlList.Visible = false;          
            pnlDropDownUserRole.Visible = false;
            pnlUploadFiles.Visible = true;
            lvFinalList.Visible = true;
            rbtForward.Enabled = false;
            rbtReturn.Enabled = false;
            rbtComplete.Enabled = false;
            BindListOfFileMovement(0);
            pnlRadioButton.Visible = false;
            pnlList.Visible = false;          
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.UpdateTrainingDetailsConfirm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // This method is used to show the details fetch from database.
    private void ShowDetails(int file_no)
    {
        try
        {
            string fStatus = string.Empty;
            string path1 = "";
            // DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER FM INNER JOIN FILE_MOVEMENTPATH MP ON (FM.FILE_ID = MP.FILE_ID) INNER JOIN FILE_FILEMOVEMENT_TRANSACTION FT ON (MP.FILE_MOVID = FT.FILE_MOVID)", "FM.FILE_ID, FM.FILE_CODE, FM.FILE_NAME, FM.DESCRIPTION, FM.CREATION_DATE, FM.DOCUMENT_TYPE", "FM.STATUS, FM.USERNO, FM.MOVEMENT_STATUS, MP.REMARK, MP.FILEPATH, FT.STATUS AS FSTATUS, FT.RECEIVED_DATE", "FT.SECTION_ID=" + ViewState["SECTIONNO"] + "AND FM.FILE_ID=" + file_no, "");
            DataSet ds = objFController.GetDetailsTran(Convert.ToInt32(ViewState["SECTIONNO"]), file_no, Convert.ToInt32(ViewState["FILE_MOVID"])); //("FILE_FILEMASTER FM INNER JOIN FILE_MOVEMENTPATH MP ON (FM.FILE_ID = MP.FILE_ID) INNER JOIN FILE_FILEMOVEMENT_TRANSACTION FT ON (MP.FILE_MOVID = FT.FILE_MOVID)", "FM.FILE_ID, FM.FILE_CODE, FM.FILE_NAME, FM.DESCRIPTION, FM.CREATION_DATE, FM.DOCUMENT_TYPE", "FM.STATUS, FM.USERNO, FM.MOVEMENT_STATUS, MP.REMARK, MP.FILEPATH, FT.STATUS AS FSTATUS, FT.RECEIVED_DATE", "FT.SECTION_ID=" + ViewState["SECTIONNO"] + "AND FM.FILE_ID=" + file_no, "");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["FILE_ID"] = ds.Tables[0].Rows[0]["FILE_ID"].ToString();
                    lblFCode.Text = ds.Tables[0].Rows[0]["FILE_CODE"].ToString();
                    lblFName.Text = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                    lblDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy");
                    lblMovRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    lblFilePath.Text = ds.Tables[0].Rows[0]["FILEPATH"].ToString();
                    hdnSectionIDS.Value = ds.Tables[0].Rows[0]["SECTIONIDS"].ToString();
                    txtDesc.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                    fStatus = ds.Tables[0].Rows[0]["FSTATUS"].ToString();
                    hdnLinkStatus.Value = ds.Tables[0].Rows[0]["LINK_STATUS"].ToString();
                }
                


                DataSet dFile = objFController.RetrieveDocumentsAtReceiver(file_no);
                if (dFile.Tables.Count > 0)
                {
                    lvFinalList.DataSource = dFile;
                    lvFinalList.DataBind();
                    lvFinalList.Visible = true;
                    Session["dtTable"] = dFile.Tables[0];
                }
                else
                {
                    lvFinalList.DataSource = null;
                    lvFinalList.DataBind();
                    lvFinalList.Visible = false;
                    string file = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + lblFName.Text;
                    BindNewFiles(file);
                }


                DataSet dsPath = objFController.GetDestinationFilePath(file_no);
                if (dsPath.Tables[0].Rows.Count > 0)
                {
                    path1 = path1 + dsPath.Tables[0].Rows[0]["PATH"].ToString();
                    ViewState["UplNo"] = dsPath.Tables[0].Rows[0]["UPLNO"].ToString();
                }
                lblFilesDestinationPath.Text = path1;



                if (fStatus == "P")               // when file not receive
                {
                    rbtReceive.Enabled = true;
                    rbtForward.Enabled = false;
                    rbtReturn.Enabled = false;
                }
                else if (fStatus == "I")          // when file only receive and next to do forward and return.
                {
                    rbtReceive.Enabled = false;
                    rbtReceive.Checked = false;
                    rbtForward.Enabled = true;
                    // rbtForward.Checked = true;
                    rbtReturn.Enabled = true;
                }
                else if (fStatus == "R")
                {
                    //rbtReceive.Enabled = false;                    
                    //rbtForward.Checked = true;
                    //rbtReturn.Checked = false;                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Actions
    // This button is used to receive the file.
    protected void btnReceive_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdbApproved.Checked == false && rdbNotApproved.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Approved/Not Approved.');", true);
                return;
            
            }

            if (rdbApproved.Checked==true)
            {
                if (rbtForward.Checked==false && rbtReturn.Checked == false &&  rbtComplete.Checked==false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Forward/Return/Complete.');", true);
                    return;
                }
               
            }

            if (rdbNotApproved.Checked == true)
            {
                if (rbtForward.Checked == false && rbtReturn.Checked == false && rbtComplete.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Forward/Return/Complete.');", true);
                    return;
                }

            }

            objFMov.FILE_MOVID = Convert.ToInt32(ViewState["FILE_MOVID"]);
            objFMov.SECTIONNO = Convert.ToInt32(ViewState["SECTIONNO"]);      //EXISTING
            objFMov.SECTION_TO_SEND = Convert.ToInt32(hfSectionId.Value);  //Convert.ToInt32(ddlSection.SelectedValue);           
            objFMov.FILE_ID = Convert.ToInt32(ViewState["FILE_ID"]);
            objFMov.USERNO = Convert.ToInt32(Session["userno"]);
            objFMov.REMARK = txtRemark.Text.Trim();
           
            objFMov.FILEPATH = lblFilePath.Text.Trim() + " - " + txtSectionUserName.Text; //ddlSection.SelectedItem.Text;
            objFMov.SECTIONPATH = hdnSectionIDS.Value + " - " + Convert.ToInt32(hfSectionId.Value);  //Convert.ToInt32(ddlSection.SelectedValue);   

           

            if (rbtReceive.Checked == true)
            {
                objFMov.FSTATUS = "I";
            }
            else if (rbtForward.Checked == true)
            {
                objFMov.FSTATUS = "F";
            }
            else if (rbtReturn.Checked == true)
            {
                objFMov.FSTATUS = "R";
            }
            else if (rbtComplete.Checked == true)
            {
               objFMov.FSTATUS = "F";
            }

            if (Convert.ToInt32(hfSectionId.Value) != 0)
            {
                if (Convert.ToInt32(ddlReceiverRole.SelectedValue) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Send To User Role.');", true);
                    return;
                }
                else
                {
                    objFMov.RECEIVER_ROLE = Convert.ToInt32(ddlReceiverRole.SelectedValue);
                }
            }
            else
            {
                objFMov.RECEIVER_ROLE = 0;
            }

           
               
           

            DataTable dt;
            dt = (DataTable)Session["dtTable"];
            objFMov.FILE_TABLE = dt;

            CustomStatus cs = (CustomStatus)objFController.AddUpdateFileReceiveDetails(objFMov);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                Clear();
                BindListOfFileMovement(0);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Received Successfully.');", true);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                SendMailToSectionMember(Convert.ToInt32(hfSectionId.Value), Convert.ToInt32(ViewState["FILE_ID"]));
                Clear();
                BindListOfFileMovement(0);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Forward To Next Section.');", true);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                SendMailToSectionMember(Convert.ToInt32(hfSectionId.Value), Convert.ToInt32(ViewState["FILE_ID"]));
                Clear();
                BindListOfFileMovement(0);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Return To Selected User.');", true);
            }
            else
            {                
                SendMailToAllMembers(Convert.ToInt32(hfSectionId.Value), Convert.ToInt32(ViewState["FILE_ID"]));
                Clear();
                BindListOfFileMovement(0);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Completed Move Back To Owner.');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.btnReceive_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    // This button is use to send emails to committee members
    private void SendMailToSectionMember(int SectionId, int File_Id)
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;
            string fromSMSId = string.Empty;
            string fromSMSPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = objFController.GetFromDataForEmail(SectionId, File_Id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                fromSMSId = ds.Tables[1].Rows[0]["SMSSVCID"].ToString();
                fromSMSPwd = ds.Tables[1].Rows[0]["SMSSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                        // mobilenum = "91" + ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
                        mobilenum = ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
                        objSms.Mobileno = mobilenum;
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                        mobilenum = mobilenum + "," + ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
                        objSms.Mobileno = mobilenum;
                    }
                }
                //sendmail(fromEmailId, fromEmailPwd, receiver, "Related To File Movement", "Dear Sir");                    
                sendmail("mis.srinagar@iitms.co.in", "pass@iitms", receiver, "Related To File Movement", "Dear Sir");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;

            DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />File Code   : " + ds.Tables[0].Rows[0]["FILE_CODE"]);
            MailBody.AppendLine(@"<br />File Name : " + ds.Tables[0].Rows[0]["FILE_NAME"]);
            MailBody.AppendLine(@"<br />Creation Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br />Description : " + ds.Tables[0].Rows[0]["DESCRIPTION"]);
            MailBody.AppendLine(@"<br />The above mentioned file is forwarded to you. Kindly receive the file.");
            MailBody.AppendLine(@"<br /> ");
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

            //====================== Message for SMS ======================//
            msg += " Dear Sir," + "File Code: " + ds.Tables[0].Rows[0]["FILE_CODE"] + " File Name: " + ds.Tables[0].Rows[0]["FILE_NAME"] + " Creation Date: " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MM-yyyy") + " File is forwarded.Kindly receive. Regards " + Session["userfullname"].ToString();
            int a = msg.Length;
            objSms.Msg_content = msg;

            int smsStatus = objFController.SENDMSG_PASS(objSms.Msg_content, objSms.Mobileno);

            //====================== Message for SMS ======================//
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }



    // This button is use to send emails to committee members
    private void SendMailToAllMembers(int SectionId, int File_Id)
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;
            string fromSMSId = string.Empty;
            string fromSMSPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = objFController.GetEmailDataForAll(File_Id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                fromSMSId = ds.Tables[1].Rows[0]["SMSSVCID"].ToString();
                fromSMSPwd = ds.Tables[1].Rows[0]["SMSSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                        // mobilenum = "91" + ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
                        mobilenum = ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
                        objSms.Mobileno = mobilenum;
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                        mobilenum = mobilenum + "," + ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
                        objSms.Mobileno = mobilenum;
                    }
                }
                //sendmail(fromEmailId, fromEmailPwd, receiver, "Related To File Movement", "Dear Sir");                    
                sendFinalmail("mis.srinagar@iitms.co.in", "pass@iitms", receiver, "Related To File Movement", "Dear Sir");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public void sendFinalmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;

            DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />File Code   : " + ds.Tables[0].Rows[0]["FILE_CODE"]);
            MailBody.AppendLine(@"<br />File Name : " + ds.Tables[0].Rows[0]["FILE_NAME"]);
            MailBody.AppendLine(@"<br />Creation Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MM-yyyy"));
            MailBody.AppendLine(@"<br />Description : " + ds.Tables[0].Rows[0]["DESCRIPTION"]);
            MailBody.AppendLine(@"<br />The above mentioned file is completed.");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            MailBody.AppendLine(@"<br />" + ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString());
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

            //====================== Message for SMS ======================//
            msg += " Dear Sir," + "File Code: " + ds.Tables[0].Rows[0]["FILE_CODE"] + " File Name: " + ds.Tables[0].Rows[0]["FILE_NAME"] + " Creation Date: " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MM-yyyy") + " File is forwarded.Kindly receive. Regards " + Session["userfullname"].ToString();
            int a = msg.Length;
            objSms.Msg_content = msg;

            // after file movement complete no need to send SMS.
            // int smsStatus = objFController.SENDMSG_PASS(objSms.Msg_content, objSms.Mobileno);
            //====================== Message for SMS ======================//
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }



    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    // This button is used to brings you in modify mode.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int file_movno = int.Parse(btnEdit.CommandArgument);
            ViewState["FILE_MOVID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(file_movno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Received File Details", "rptFileMovement.rpt");

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + Session["userno"];

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
            // ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void rbtForward_CheckedChanged(object sender, EventArgs e)
    {
        rbtReturn.Checked = false;
        rbtComplete.Checked = false;
        trDept.Visible = true;      
        txtSectionUserName.Text = string.Empty;
        hfSectionId.Value = "";
        Session["OperType"] = 1;
        txtSectionUserName.Enabled = true;
        divSendToUserRole.Visible = false;

    }

    protected void rbtReturn_CheckedChanged(object sender, EventArgs e)
    {
        rbtForward.Checked = false;
        rbtComplete.Checked = false;
        trDept.Visible = true;      
        txtSectionUserName.Text = string.Empty;
        hfSectionId.Value = "";
        Session["OperType"] = 2;
        txtSectionUserName.Enabled = true;
        divSendToUserRole.Visible = false;

    }

    protected void rbtComplete_CheckedChanged(object sender, EventArgs e)
    {
        rbtReturn.Checked = false;
        rbtForward.Checked = false;
        txtSectionUserName.Text = string.Empty;
        hfSectionId.Value = "";
        Session["OperType"] = 3;
        trDept.Visible = true;
        DataSet ds = null;
        ds = objFController.FillSectionUserNames("");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtSectionUserName.Text = ds.Tables[0].Rows[0]["SECTION_NAME"].ToString();
            hfSectionId.Value = ds.Tables[0].Rows[0]["SECTION_ID"].ToString();
            txtSectionUserName.Enabled = false;
        }
        divSendToUserRole.Visible = false;
    }

    #endregion



    #region Upload Documents

    // This is used to add new documents if any.
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string file = string.Empty;

            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (hdnLinkStatus.Value == "1")
                    {
                        file = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilesDestinationPath.Text;
                    }
                    else
                    {
                        file = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + lblFName.Text;
                    }

                    if (!System.IO.Directory.Exists(file))
                    {
                        System.IO.Directory.CreateDirectory(file);
                    }

                    ViewState["FILE_PATH"] = file;
                    dbPath = file;
                    string filename = FileUpload1.FileName;
                    ViewState["FILE_NAME"] = filename;

                    if (hdnLinkStatus.Value == "1")
                    {
                        path = file + FileUpload1.FileName;
                    }
                    else
                    {
                        path = file + "\\" + FileUpload1.FileName;
                    }

                    //CHECKING FOLDER EXISTS OR NOT 
                    if (!System.IO.Directory.Exists(path))
                    {
                        if (!File.Exists(path))
                        {
                            FileUpload1.PostedFile.SaveAs(path);
                        }
                        else
                        {
                            Showmessage("This Document Already Exists.");
                            return;
                        }
                    }

                    if (Session["dtTable"] != null)
                    {
                        DocFinalListTbl = (DataTable)Session["dtTable"];
                        DataRow dr = DocFinalListTbl.NewRow();

                        dr["IDNO"] = 0;
                        dr["UPLNO"] = ViewState["UplNo"];
                        dr["FILENAME"] = FileUpload1.FileName;
                        dr["FILEPATH"] = ViewState["FILE_PATH"].ToString() + "\\" + FileUpload1.FileName;
                        dr["UA_NO"] = Convert.ToInt32(Session["userno"]);

                        DocFinalListTbl.Rows.Add(dr);

                        Session["dtTable"] = DocFinalListTbl;

                        lvFinalList.DataSource = DocFinalListTbl;
                        lvFinalList.DataBind();
                    }
                    else
                    {
                        BindNewFiles(file);
                    }
                }
                else
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt,.zip]", this.Page);
                    FileUpload1.Focus();
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please Select File", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".bmp", ".BMP", ".gif", ".GIF", ".png", ".PNG", ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".docx", ".xlsx", ".DOCX", ".XLSX", ".zip", ".ZIP", ".txt", ".TXT", ".rar", ".RAR" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText, btn.CommandArgument);
    }


    public void DownloadFile(string fileName, string filePath)
    {
        try
        {
            //if (hdnLinkStatus.Value == "1")
            //{
            //    path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFName.Text;
            //}
            //else
            //{
            //    path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + lblFName.Text;
            //}

            // path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFName.Text;
            FileStream sourceFile = new FileStream((filePath), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }


    protected void imgdownloadNew_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFileNew(btn.AlternateText);
    }

    public void DownloadFileNew(string fileName)
    {
        try
        {
            if (ViewState["FILENAME"] == null)
            {
                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + lblFName.Text;
            }
            else
            {
                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + ViewState["FILENAME"].ToString();
            }
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }


    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    #endregion

    #region Direct Files Upload


    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;

        f_name = obj.ToString();
        return f_name;
    }

    private void BindNewFiles(string PATH)
    {
        try
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(PATH);
            if (System.IO.Directory.Exists(PATH))
            {
                System.IO.FileInfo[] files = dir.GetFiles();

                if (Convert.ToBoolean(files.Length))
                {
                    lvNewFiles.DataSource = files;
                    lvNewFiles.DataBind();
                    ViewState["FILE"] = files;
                    pnlNewFiles.Visible = true;
                }
                else
                {
                    lvNewFiles.DataSource = null;
                    lvNewFiles.DataBind();
                    pnlNewFiles.Visible = false;
                }
            }
            else
            {
                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                pnlNewFiles.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.BindNewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDeleteNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + lblFName.Text;

            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted.');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        BindNewFiles(path);
    }

    protected void lvNewFiles_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ImageButton del = e.Item.FindControl("btnDeleteNew") as ImageButton;

        if (del.CommandName != "0")
        {
            del.Visible = false;
        }
        else
        {
            del.Visible = true;
        }
    }

    #endregion


    protected void rdoFile_CheckedChanged(object sender, EventArgs e)
    {

        RadioButton rdbBtn = sender as RadioButton;
        int file_id = int.Parse(rdbBtn.ToolTip);
        ViewState["FILE_ID"] = int.Parse(rdbBtn.ToolTip);
        string FilePath = Docpath + "FILEMOVEMENT\\UPLOADFILES\\";

        DataSet dFile = objFController.RetrieveDocumentsAtReceiver(file_id, Convert.ToInt32(Session["userno"]), FilePath);
        if (dFile.Tables.Count > 0)
        {
            ViewState["FILENAME"] = dFile.Tables[0].Rows[0]["FILENAME"].ToString();
            if (dFile.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "1")
            {

                //lvDocumentList.DataSource = dFile;
                //lvDocumentList.DataBind();
                //lvDocumentList.Visible = true;

                lvFinalList.DataSource = dFile;
                lvFinalList.DataBind();
                lvFinalList.Visible = true;


                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                pnlNewFiles.Visible = false;
            }
            else
            {
                //lvDocumentList.DataSource = null;
                //lvDocumentList.DataBind();
                //lvDocumentList.Visible = false;

                lvFinalList.DataSource = null;
                lvFinalList.DataBind();
                lvFinalList.Visible = false;



                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + dFile.Tables[0].Rows[0]["FILENAME"].ToString();
                BindNewFiles(path);
            }
        }
    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }  

    protected void rdbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbList.SelectedValue == "0")
            {
                pnlList.Visible = true;
                pnlInActive.Visible = false;

                lvFinalList.DataSource = null;
                lvFinalList.DataBind();

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();

                ViewState["FILE_ID"] = null;

                foreach (ListViewItem i in lvFileMovement.Items)
                {
                    RadioButton rdbFile = (RadioButton)i.FindControl("rdoFile");
                    rdbFile.Checked = false;
                }
            }
            else
            {
                pnlList.Visible = false;
                pnlInActive.Visible = true;

                lvFinalList.DataSource = null;
                lvFinalList.DataBind();

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                ViewState["FILE_ID"] = null;

                foreach (ListViewItem i in lvInactiveFile.Items)
                {
                    RadioButton rdbFileInActive = (RadioButton)i.FindControl("rdoFile");
                    rdbFileInActive.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.rdbList_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdbApproved_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            rbtForward.Enabled = true;
            rbtComplete.Enabled = true;

            rbtReturn.Enabled = false;
           
            rdbNotApproved.Checked = false;
            rbtForward.Visible = true;
            rbtComplete.Visible = true;
            txtSectionUserName.Text = string.Empty;
            hfSectionId.Value = "0";
            rbtReturn.Checked = false;
            txtSectionUserName.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.rdbApproved_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void rdbNotApproved_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
           // rbtForward.Enabled = false;
         

            rbtComplete.Enabled = false;
            rdbApproved.Checked = false;

            rbtForward.Checked = false;
            rbtComplete.Checked = false;

            rbtReturn.Visible = true;
            rbtReturn.Enabled = true;

            rbtForward.Enabled = true;

            txtSectionUserName.Text = string.Empty;
            hfSectionId.Value = "0";
            txtSectionUserName.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.rdbNotApproved_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void txtSectionUserName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(hfSectionId.Value) != 0)
            {
                objCommon.FillDropDownList(ddlReceiverRole, "FILE_SECTIONMASTER CROSS APPLY dbo.split(SECTION_USER_ROLE, ',') s	INNER JOIN FILE_ROLE_MASTER RM ON (s.Value = RM.ROLE_ID)", "S.Value AS SECTION_USER_ROLE", "RM.ROLENAME", "SECTION_ID = " + Convert.ToInt32(hfSectionId.Value), "");
                divSendToUserRole.Visible = true;
            }
            else
            {
                divSendToUserRole.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.txtSectionUserName_TextChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
       try
        {
            BindListOfFileMovement(Convert.ToInt32(ddlUserRole.SelectedValue));
        }
       catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.ddlUserRole_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
             else
                 objCommon.ShowError(Page, "Server UnAvailable");
         }

    }
}