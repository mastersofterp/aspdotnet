//=====================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 25-SEP-2015                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//=====================================================================
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

using System.Net.Mail;
using System.Net;
using System.Text;


public partial class FileMovementTracking_Master_FileMaster : System.Web.UI.Page
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
                    //Page Authorization
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["FILE_ID"] = null;
                    Session["dtTable"] = null;
                    ViewState["FILE_NAME"] = null;
                    ViewState["FILE_PATH"] = null;
                    ViewState["FILE"] = null;
                    Session["dtTable"] = null;
                    divMovement.Visible = true;
                    BindListViewFile();
                    txtCreationDate.Text = System.DateTime.Now.ToString();
                    objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0", "SUBDEPT");
                   // objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTCODE", "DEPTNO<>0", "DEPTNO");
                    txtOwnerName.Text = Session["userfullname"].ToString();
                    ddlDepartment.SelectedValue = Session["userEmpDeptno"].ToString();
                   //ddlDepartment.SelectedValue = Session["userdeptno"].ToString();
                    objCommon.FillDropDownList(ddlCategory, "ADMN_DC_DOCUMENT_TYPE_DOC", "DNO", "DOCUMENTNAME", "DNO>0 and DNO IN (SELECT VALUE FROM FN_SPLIT((SELECT UA_CAT FROM USER_ACC WHERE UA_NO=" + Session["userno"].ToString() + "),','))", "DNO");
                    objCommon.FillDropDownList(ddlDocType, "FILE_DOCUMENT_TYPE", "DOCUMENT_TYPE_ID", "DOCUMENT_TYPE", "STATUS=0", "DOCUMENT_TYPE");

                    GenerateFileNo();

                    objCommon.FillDropDownList(ddlCreatorRole, "FILE_SECTIONMASTER 	CROSS APPLY dbo.split(SECTION_USER_ROLE, ',') s	INNER JOIN FILE_ROLE_MASTER RM ON (s.Value = RM.ROLE_ID)", "S.Value AS SECTION_USER_ROLE", "RM.ROLENAME", "RECEIVER_ID = " + Convert.ToInt32(Session["userno"]), "");
                    divDocSaccanning.Visible = false; 
                }
            }
            HiddenGetSuName.Value = "0";
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSectionName(string prefixText)
    {
        List<string> SectionUserName = new List<string>();
        DataSet ds = new DataSet();
       
        try
        {
            FileMovementController objFController = new FileMovementController();
            ds = objFController.FillSUserNamesInFileMaster(prefixText);

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

    // This method is used to generate file no.
    private void GenerateFileNo()
    {
        try
        {
            DataSet ds = objFController.GenerateFileNo(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFileCode.Text = ds.Tables[0].Rows[0]["FILENO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.GenerateFileNo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this method is used to clear the controls.
    private void Clear()
    {
        txtFileCode.Text = string.Empty;
        txtFileName.Text = string.Empty;
        txtDesc.Text = string.Empty;
        txtCreationDate.Text = System.DateTime.Now.ToString();
        ddlDocType.SelectedIndex = 0;
        rdoStatus.SelectedValue = "0";
        ViewState["FILE_ID"] = null;
        lvFinalList.DataSource = null;
        lvFinalList.DataBind();
        pnlFinalList.Visible = false;
        chkFile.Checked = false;
        ViewState["FILE_NAME"] = null;
        ViewState["FILE_PATH"] = null;
        txtFileName.Enabled = true;
        chkFile.Enabled = true;
        lvNewFiles.DataSource = null;
        lvNewFiles.DataBind();       
        txtSectionUserName.Text = string.Empty;
        hfSectionId.Value = "";
        txtRemark.Text = string.Empty;
        PanelEnabledTrue();
        ddlFileCategory.Visible = false;
        btnAddCategoryFiles.Visible = false;
        pnlFinalList.Visible = false;
        ddlCategory.SelectedIndex = 0;
        lvfileUpload.DataSource = null;
        lvfileUpload.DataBind();
        pnlFile.Visible = false;    
        ViewState["FILE"] = null;
        Session["dtTable"] = null;
        txtKeywords.Text = string.Empty;
        GenerateFileNo();
        ddlCreatorRole.SelectedIndex = 0;
        ddlReceiverRole.SelectedIndex = 0;
    }

    private void PanelEnabledTrue()
    {
        pnlDetails.Enabled = true;
        pnlCategoryFileUpload.Enabled = true;
        pnlUploadNewDocuments.Enabled = true;
        pnlFinalList.Enabled = true;
        divMovement.Enabled = true;
        pnlNewFiles.Enabled = true;
        btnSubmit.Enabled = true;
        FileUpload1.Enabled = true;
    }

    private void PanelEnabledFalse()
    {
        pnlDetails.Enabled = false;
        pnlCategoryFileUpload.Enabled = false;
        pnlUploadNewDocuments.Enabled = false;
        pnlFinalList.Enabled = false;
        divMovement.Enabled = false;
        pnlNewFiles.Enabled = false;
        btnSubmit.Enabled = false;
        FileUpload1.Enabled = false;

    }

    // this method is used to display the entry list.
    private void BindListViewFile()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["userno"]) == 1)
            {
                ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID)", "FILE_ID, FILE_CODE,FILE_NAME", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE , (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS", "U.UA_TYPE IN (3,4) ", "FILE_ID DESC");
            }
            else
            {
                ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID)", "FILE_ID, FILE_CODE,FILE_NAME", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE , (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS", "U.UA_TYPE  IN (3,4) AND USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_ID DESC");
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFile.DataSource = ds;
                lvFile.DataBind();
            }
            else
            {
                lvFile.DataSource = null;
                lvFile.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this method is used to show the details fetch from database.
    private void ShowDetails(int file_no)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER FM INNER JOIN FILE_MOVEMENTPATH FP ON (FM.FILE_ID = FP.FILE_ID) INNER JOIN FILE_SECTIONMASTER S ON (FP.SECTIONID = S.SECTION_ID)", "FM.*", " FP.FILEPATH, FP.REMARK, FP.SECTIONID, S.DEPTNO, S.SECTION_NAME", "FM.FILE_ID=" + file_no, "");
            //to show created user details
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["FILE_ID"] = ds.Tables[0].Rows[0]["FILE_ID"].ToString();
                    txtFileCode.Text = ds.Tables[0].Rows[0]["FILE_CODE"].ToString();
                    txtFileName.Text = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                    txtCreationDate.Text = ds.Tables[0].Rows[0]["CREATION_DATE"].ToString();
                    txtDesc.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                    ddlDocType.SelectedValue = ds.Tables[0].Rows[0]["DOCUMENT_TYPE"].ToString();
                    rdoStatus.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["EMPDEPTNO"].ToString();
                    lblFilePath.Text = ds.Tables[0].Rows[0]["FOLDER_PATH"].ToString();
                    ViewState["UplNo"] = ds.Tables[0].Rows[0]["UPLNO"].ToString();
                    txtFileName.Enabled = false;                  
                    hfSectionId.Value = ds.Tables[0].Rows[0]["SECTIONID"].ToString();

                    objCommon.FillDropDownList(ddlReceiverRole, "FILE_SECTIONMASTER CROSS APPLY dbo.split(SECTION_USER_ROLE, ',') s	INNER JOIN FILE_ROLE_MASTER RM ON (s.Value = RM.ROLE_ID)", "S.Value AS SECTION_USER_ROLE", "RM.ROLENAME", "SECTION_ID = " + Convert.ToInt32(hfSectionId.Value), "");

                    txtSectionUserName.Text = ds.Tables[0].Rows[0]["SECTION_NAME"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();                 
                    txtKeywords.Text = ds.Tables[0].Rows[0]["FILE_KEYWORDS"].ToString();
                    ddlReceiverRole.SelectedValue = ds.Tables[0].Rows[0]["RECEIVER_ROLE"].ToString();
                    //ddlReceiverRole.SelectedItem.Text = ds.Tables[0].Rows[0]["RECEIVER_ROLE"].ToString();
                    ddlCreatorRole.SelectedValue = ds.Tables[0].Rows[0]["SELF_ROLE"].ToString();

                    if (ds.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "1")
                    {
                        chkFile.Checked = true;
                        chkFile.Enabled = false;
                        path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();

                        DataSet dFile = objFController.RetrieveDocumentFiles(file_no, Convert.ToInt32(Session["userno"]));
                        if (dFile.Tables[0].Rows.Count > 0)
                        {
                            Session["dtTable"] = dFile.Tables[0];
                            lvFinalList.DataSource = dFile;
                            lvFinalList.DataBind();
                            pnlFinalList.Visible = true;
                            divMovement.Visible = true;
                        }
                        else
                        {
                            lvFinalList.DataSource = null;
                            lvFinalList.DataBind();
                            pnlFinalList.Visible = false;
                            divMovement.Visible = false;
                        }
                    }
                    else
                    {
                        chkFile.Checked = false;
                        path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                        BindListViewFiles(path);
                    }                   
                    PanelEnabledFalse();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Actions
    // this button is used to insert and update the section name.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;
            string fileName = string.Empty;
            string destFile = string.Empty;
            string path = string.Empty;
            string FileNameString = string.Empty;

            if (checkFileNameExist())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This File Name Already Exist.');", true);
                txtFileName.Focus();
                return;
            }

            objFMov.FILE_CODE = txtFileCode.Text.Trim();
            if (chkFile.Checked == true)
            {
                if (chkFile.Enabled == true)
                {
                    objFMov.FILE_NAME = ddlFileCategory.SelectedItem.Text;
                }
                else
                {
                    objFMov.FILE_NAME = txtFileName.Text.Trim();
                }
                objFMov.UPLNO = Convert.ToInt32(ViewState["UplNo"]);
            }
            else
            {
                objFMov.FILE_NAME = txtFileName.Text.Trim();
                objFMov.UPLNO = 0;
            }

            objFMov.DESCRIPTION = txtDesc.Text.Trim() == string.Empty ? "" : txtDesc.Text.Trim();
            objFMov.CREATION_DATE = System.DateTime.Now; // Convert.ToDateTime(txtCreationDate.Text.Trim());
            objFMov.DOC_TYPE = Convert.ToInt32(ddlDocType.SelectedValue);
            objFMov.STATUS = Convert.ToInt32(rdoStatus.SelectedValue);
            objFMov.USERNO = Convert.ToInt32(Session["userno"]);
            objFMov.EMPDEPTNO = Convert.ToInt32(Session["userEmpDeptno"]);  //Convert.ToInt32(ddlDepartment.SelectedValue);
            if (chkFile.Checked == true)
            {
                objFMov.LINK_STATUS = 1;
                objFMov.FOLDER_PATH = lblFilePath.Text;
            }
            else
            {
                objFMov.LINK_STATUS = 0;
                objFMov.FOLDER_PATH = "";
            }

            DataTable dt;
            dt = (DataTable)Session["dtTable"];
            objFMov.FILE_TABLE = dt;

            objFMov.SECTIONNO = Convert.ToInt32(hfSectionId.Value); //Convert.ToInt32(ddlSection.SelectedValue);
            objFMov.REMARK = txtRemark.Text.Trim();
            objFMov.FILEPATH = Session["userfullname"].ToString() + " - " + txtSectionUserName.Text; //ddlSection.SelectedItem.Text;
            objFMov.SECTIONPATH = hfSectionId.Value; //ddlSection.SelectedValue;
            objFMov.FILE_KEYWORDS = txtKeywords.Text.Trim();

            objFMov.SELF_ROLE = Convert.ToInt32(ddlCreatorRole.SelectedValue);
            objFMov.RECEIVER_ROLE = Convert.ToInt32(ddlReceiverRole.SelectedValue);


            if (lvFinalList.Items.Count > 0)
            {
                if (chkFile.Checked == true)
                {
                    path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\";
                    targetPath = path + lblFilePath.Text;
                }
                else
                {
                    path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\";
                    targetPath = path + txtFileName.Text;
                }

                sourcePath = path + lblPathOfSelectedCategory.Text;

                // when files upload from other categories
                if (lvFinalList.Items.Count > 0)
                {
                    if (System.IO.Directory.Exists(sourcePath))
                    {
                        foreach (ListViewItem i in lvFinalList.Items)
                        {
                            HiddenField hdnFinalUplNo = (HiddenField)i.FindControl("hdnFinalUPLNO");
                            ImageButton imgFinalBtn = (ImageButton)i.FindControl("imgFile");
                            HiddenField hdnSourcePath = (HiddenField)i.FindControl("hdnSourcePath");
                            HiddenField hdnFIdNo = (HiddenField)i.FindControl("hdnFinalIDNO");

                            if (hdnFinalUplNo.Value != ViewState["UplNo"].ToString() && hdnFIdNo.Value != "0")
                            {
                                if (FileNameString == "")
                                {
                                    FileNameString = hdnSourcePath.Value + "\\" + imgFinalBtn.AlternateText;
                                }
                                else
                                {
                                    FileNameString += "," + hdnSourcePath.Value + "\\" + imgFinalBtn.AlternateText;
                                }

                                string[] files = FileNameString.Split(',');
                                // Copy the files and overwrite destination files if they already exist.
                                foreach (string s in files)
                                {
                                    // Use static Path methods to extract only the file name from the path.
                                    fileName = System.IO.Path.GetFileName(s);
                                    destFile = System.IO.Path.Combine(targetPath, fileName);
                                    System.IO.File.Copy(s, destFile, true);
                                }
                            }
                        }
                    }
                }
            }

            if (ViewState["FILE_ID"] == null)
            {
                CustomStatus cs = (CustomStatus)objFController.AddUpdateFileName(objFMov);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    Showmessage("This File Name Already Exist.");
                    return;
                }
                else if (cs.Equals(CustomStatus.RecordSaved))
                {
                   ViewState["FILE_ID"]  =  objFMov.FILE_ID;              
                   SendMailToSectionMember(Convert.ToInt32(hfSectionId.Value), Convert.ToInt32(ViewState["FILE_ID"]));
                    Clear();
                    BindListViewFile();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Created Successfully.');", true);
                }                
            }
            else
            {
                objFMov.FILE_ID = Convert.ToInt32(ViewState["FILE_ID"].ToString());
                CustomStatus cs = (CustomStatus)objFController.AddUpdateFileName(objFMov);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    Showmessage("Record Already Exist.");
                    return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListViewFile();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Updated Successfully.');", true);
                }
            }
            BindListViewFile();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
                        //mobilenum = "91" + ds.Tables[0].Rows[i]["UA_MOBILE"].ToString();
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

                sendmail("mis.srinagar@iitms.co.in", "pass@iitms", receiver, "Related To File Movement", "Dear Sir", "nitegov", "nitegov434");                    
               // sendmail("recruitment@iitms.co.in", "IITMS@123", receiver, "Related To File Movement", "Dear Sir");       
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string fromSMSId, string fromSMSPwd)
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
                MailBody.AppendLine(@"<br />File Code : " + ds.Tables[0].Rows[0]["FILE_CODE"]) ;
                MailBody.AppendLine(@"<br />File Name : " + ds.Tables[0].Rows[0]["FILE_NAME"]);
                MailBody.AppendLine(@"<br />Creation Date : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MM-yyyy"));
                MailBody.AppendLine(@"<br />Description : " + ds.Tables[0].Rows[0]["DESCRIPTION"]);  
                MailBody.AppendLine(@"<br />The above mentioned file is moving to you. Kindly refer the file.");
                MailBody.AppendLine(@"<br /> ");
                MailBody.AppendLine(@"<br /> ");
                MailBody.AppendLine(@"<br /> ");
                MailBody.AppendLine(@"<br />Thanks And Regards");  
                MailBody.AppendLine(@"<br />" + ds.Tables[0].Rows[0]["UA_FULLNAME"]);


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
                msg += " Dear Sir," + "File Code: " + ds.Tables[0].Rows[0]["FILE_CODE"] + " File Name: " + ds.Tables[0].Rows[0]["FILE_NAME"] +" Creation Date: " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MM-yyyy") + " File is forwarded.Kindly receive. Regards " + Session["userfullname"].ToString();            

                int a = msg.Length;
                objSms.Msg_content = msg;
             

                int smsStatus = objFController.SENDMSG_PASS(objSms.Msg_content, objSms.Mobileno);

               // objSmsController.SendBulkSms(objSms); 
            //====================== Message for SMS ======================//

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }          

    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {  //delete temporary file which is added before submit 24/01/2022
       //----------------------------start -------------------------------------- 
        if (txtFileName.Text != "")
        {
            DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "FILE_NAME='" + txtFileName.Text + "'", "");
            if (ds.Tables[0].Rows.Count>0)
            {

            }
            else
            {
                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;

                //if (File.Exists(path))
                //{
                if (System.IO.Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                       
                        File.Delete(file);
                       

                    }
                    System.IO.Directory.Delete(path);

                }


                    //DELETING THE FILE
                    //File.Delete(path);

                //}
            }
        }
       //--------------------------------end-----------------//
       

        Clear();
        HiddenGetSuName.Value = "0";
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    // this button is used to brings you in modify mode.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int file_no = int.Parse(btnEdit.CommandArgument);
            ViewState["FILE_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(file_no);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("File Details", "rptFileMaster.rpt");
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
            //  ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void lvFile_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ImageButton edit = e.Item.FindControl("btnEdit") as ImageButton;
        if (Convert.ToInt32(Session["userno"]) == 1)
        {
            edit.Visible = false;
        }
        else
        {
            edit.Visible = true;
        }
    }

    private bool checkFileNameExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "FILE_NAME='" + txtFileName.Text +"'" , "");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    #endregion

    #region Other Methods

    protected void ddlFileCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string SourcePathOfSelectedCategory = string.Empty;
            if (ddlFileCategory.SelectedIndex > 0)
            {
                string path1 = "";
                DataSet ds = objFController.GetCategoryPath(Convert.ToInt32(ddlFileCategory.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    path1 = path1 + ds.Tables[0].Rows[0]["PATH"].ToString();
                    divLbL.Visible = true;
                }

                lblFilePath.Text = path1;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    ViewState["UplNo"] = ds.Tables[1].Rows[0]["UPLNO"].ToString();
                }

                SourcePathOfSelectedCategory = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
                BindDocOfDestinationCategory(Convert.ToInt32(ddlFileCategory.SelectedValue), SourcePathOfSelectedCategory);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.ddlFileCategory_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindDocOfDestinationCategory(int category, string SourcePath)
    {
        try
        {
            objFMov.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objFMov.DNO = category;
            objFMov.SOURCEPATH = SourcePath;
            DataSet ds = objFController.GetDocumentsByCategory(objFMov);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["dtTable"] = ds.Tables[0];
                lvFinalList.DataSource = ds.Tables[0];
                lvFinalList.DataBind();
                pnlFinalList.Visible = true;
                //divMovement.Visible = true;
            }
            else
            {
                pnlFinalList.Visible = false;
                //divMovement.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.BindDocOfDestinationCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string path1 = "";
            string SourcePathOfSelectedCategory = "";
            DataSet ds = objFController.GetCategoryPath(Convert.ToInt32(ddlCategory.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                path1 = path1 + ds.Tables[0].Rows[0]["PATH"].ToString();
                divLbL.Visible = true;
            }
            lblPathOfSelectedCategory.Text = path1;
            SourcePathOfSelectedCategory = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblPathOfSelectedCategory.Text;
            BindDocCategorywise(Convert.ToInt32(ddlCategory.SelectedValue), SourcePathOfSelectedCategory);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.ddlCategory_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void BindDocCategorywise(int category, string SourcePath)
    {
        try
        {
            objFMov.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objFMov.DNO = category;
            objFMov.SOURCEPATH = SourcePath;
            DataSet ds = objFController.GetDocumentsByCategory(objFMov);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvfileUpload.DataSource = ds;
                lvfileUpload.DataBind();
                pnlFile.Visible = true;
                btnAddCategoryFiles.Visible = true;
            }
            else
            {
                lvfileUpload.DataSource = null;
                lvfileUpload.DataBind();
                pnlFile.Visible = false;
                btnAddCategoryFiles.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DOCUMENT_DocumentUpload.BindDocCategorywise-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind files when select from Category
    private void BindListViewFiles(string PATH)
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
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Upload Files

    //Check for Valid File 
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
        //ImageButton btn = sender as ImageButton;  // 21/01/2022
       // DownloadFile(btn.AlternateText);

        ImageButton btn = sender as ImageButton;
        DownloadFileNew(btn.AlternateText);
    }

    public void DownloadFileNew(string fileName)
    {
        try
        {
            path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
            //path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + ViewState["FILE_NAME"].ToString();
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

    public void DownloadFile(string fileName)
    {
        try
        {
           // path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
            if (chkFile.Checked == true)
            {
                path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
            }
            else
            {
                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
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
            Response.AddHeader("Content-Disposition", "inline; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + path + "\\" + fileName + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);



        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }



    protected void imgPreview_Click(object sender, ImageClickEventArgs e)
    {
        // ImageButton lnkbtn = sender as ImageButton;
        //string  fileName = lnkbtn.AlternateText;
        //DownloadFile(fileName);

        string filepath = "";
        ImageButton lnkbtn = sender as ImageButton;

        ///LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        //string filePath = "~//FriendPhoto//" + gvFriend.DataKeys[gvrow.RowIndex].Value.ToString();
        if (chkFile.Checked == true)
        {
            path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
        }
        else
        {
            path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
        }
        filepath = path + "\\" + lnkbtn.AlternateText;
        Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "viewImage.aspx?fn=" + filepath + "&file=" + lnkbtn.AlternateText));


        video.Attributes.Add("src", filepath + "\\viewImage.aspx");
        // video.Attributes.Add("src", "DOCUMENTANDSCANNING\\FILEUPLOAD\\ACCOUNTS\\SALARY DOC\\Employee information Form new.docx");
        //video.Attributes.Add("src", "Employee information Form new.docx");

        ModalPopupExtender1.Show();

    }



    protected void imgdownloadFirst_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFileFirst(btn.AlternateText);
    }

    public void DownloadFileFirst(string fileName)
    {
        try
        {
            //path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblPathOfSelectedCategory.Text;
            if (chkFile.Checked == true)
            {
                path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
            }
            else
            {
                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
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

    protected string GetFileName(object obj)  //24/01/2022
    {
        //string f_name = string.Empty;

        //f_name = obj.ToString();
        //return f_name;
        string f_name = string.Empty;

        string[] fileName = obj.ToString().Split('_');
        f_name = fileName[0]; // obj.ToString();         
        return f_name;
    }

    protected string GetUserName(object obj)
    {
        string f_name = string.Empty;
        string userno = string.Empty;
        string username = string.Empty;   //03/12/2021
        DataSet ds = null;
        //  f_name = obj.ToString();
        string[] fileName = obj.ToString().Split('_');
        userno = fileName[1];
        if (Convert.ToInt32(userno) > 0)
        {
             ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(userno), "");
        }
        else
        {
            objCommon.DisplayMessage("Note: Your File Name Should Not Contain  _,-", this.Page);
        }
        username = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
        //ataSet ds = objCommon.FillDropDown("USER_ACC", "UA-NO", "FILE_NAME", "FILE_NAME='" + txtFileName.Text + "' AND USERNO = " + Convert.ToInt32(Session["userno"]), "");
        return username;
    }


    // This action is used to add categorywise selected documents list in final list.
    protected void btnAddCategoryFiles_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkFile.Checked == true)
            {
                path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
            }
            else
            {
                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
            }


            DocFinalListTbl.Columns.Add("IDNO", typeof(int));
            DocFinalListTbl.Columns.Add("UPLNO", typeof(int));
            DocFinalListTbl.Columns.Add("FILENAME", typeof(string));
            DocFinalListTbl.Columns.Add("FILEPATH", typeof(string));
            DocFinalListTbl.Columns.Add("UA_NO", typeof(int));
            DocFinalListTbl.Columns.Add("SOURCEPATH", typeof(string));

            if (Session["dtTable"] != null)
            {
                DocFinalListTbl = (DataTable)Session["dtTable"];
            }

            DataRow dr = null;
            foreach (ListViewItem i in lvfileUpload.Items)
            {
                CheckBox chkS = (CheckBox)i.FindControl("chkSelect");
                HiddenField hdnUplNo = (HiddenField)i.FindControl("hdnUPLNO");
                Label lbFileName = (Label)i.FindControl("lblFileName");
                ImageButton imgBtn = (ImageButton)i.FindControl("imgFile");
                HiddenField hdnSourcePath = (HiddenField)i.FindControl("hdnSourcePath");
                HiddenField hdnUaNo = (HiddenField)i.FindControl("hdnUANO");
                if (chkS.Checked == true)
                {
                    dr = DocFinalListTbl.NewRow();
                    dr["IDNO"] = chkS.ToolTip;
                    dr["UPLNO"] = hdnUplNo.Value;
                    dr["FILENAME"] = lbFileName.Text;
                    dr["FILEPATH"] = path + "\\" + lbFileName.Text;//imgBtn.CommandArgument;
                    dr["UA_NO"] = hdnUaNo.Value;
                    dr["SOURCEPATH"] = hdnSourcePath.Value;


                    DocFinalListTbl.Rows.Add(dr);
                }
            }

            Session["dtTable"] = DocFinalListTbl;

            if (DocFinalListTbl.Rows.Count > 0)
            {
                lvFinalList.DataSource = DocFinalListTbl;
                lvFinalList.DataBind();
                pnlFinalList.Visible = true;
            }
            else
            {
                lvFinalList.DataSource = null;
                lvFinalList.DataBind();
                pnlFinalList.Visible = false;
            }

            lvfileUpload.DataSource = null;
            lvfileUpload.DataBind();
            pnlFile.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btnAddCategoryFiles_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    // This is used to delete files from the list.
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string file = string.Empty;
            ImageButton btnDel = sender as ImageButton;
            int IDNO = int.Parse(btnDel.CommandName);
            int UANO = Convert.ToInt32(Session["userno"].ToString());
            CustomStatus cs = (CustomStatus)objFController.DeleteDocument(IDNO, UANO);

            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
            }

            if (chkFile.Checked == true)
            {
                file = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + txtFileName.Text;
            }
            else
            {
                file = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
            }

            if (File.Exists(file))
            {
                //DELETING THE FILE
                File.Delete(file);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This is used to add new documents if any.
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string file = string.Empty;

            if (txtFileName.Text == "")
            {
                Showmessage("Please Enter File Name.");
                return;
            }
           

            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (chkFile.Checked == true)
                    {
                        file = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + lblFilePath.Text;
                    }
                    else
                    {
                        file = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;
                    }

                    if (!System.IO.Directory.Exists(file))
                    {
                        System.IO.Directory.CreateDirectory(file);
                    }
                    //else  //24/01/2022
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This File Name Already Exist. Please Enter Another File Name');", true);
                    //    txtFileName.Focus();
                    //    return;


                    //}

                    ViewState["FILE_PATH"] = file;
                    dbPath = file;
                    string filename = FileUpload1.FileName;
                    ViewState["FILE_NAME"] = filename;

                    path = file + " \\ " + FileUpload1.FileName + "_" + Session["userno"].ToString();   // 3/12/2021


                    //CHECKING FOLDER EXISTS OR NOT file
                    if (!System.IO.Directory.Exists(path))
                    {
                        if (!File.Exists(path))
                        {
                            FileUpload1.PostedFile.SaveAs(path);
                        }
                        else
                        {
                            Showmessage("This Document Already Exists.");
                        }                        
                    }

                    if (Session["dtTable"] != null)
                    {
                        DocFinalListTbl = (DataTable)Session["dtTable"];
                        DataRow dr = DocFinalListTbl.NewRow();

                        dr["IDNO"] = 0;
                        if (chkFile.Checked == true)
                        {
                            dr["UPLNO"] = Convert.ToInt32(ViewState["UplNo"]);
                        }
                        else
                        {
                            dr["UPLNO"] = 0;
                        }
                        dr["FILENAME"] = FileUpload1.FileName;
                        dr["FILEPATH"] = ViewState["FILE_PATH"].ToString() + "\\" + FileUpload1.FileName;

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

    private void BindNewFiles(string PATH)
    {
        try
        {
            pnlFile.Visible = true;
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
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.BindListViewFiles-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDeleteNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //ImageButton btnDelete = sender as ImageButton;
            //string fname = btnDelete.CommandArgument;
            //path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;

            //if (File.Exists(path + "\\" + fname))
            //{
            //    //DELETING THE FILE
            //    File.Delete(path + "\\" + fname);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
            //}

            //24/01/2022
            ImageButton btnDelete = sender as ImageButton;
            string fname = btnDelete.CommandArgument;
            path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileName.Text;    // 03/12/2021
            //path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + txtFileCode.Text;  // 03/12/2021

            if (File.Exists(path + "\\" + fname))
            {
                //DELETING THE FILE
                File.Delete(path + "\\" + fname);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Deleted Successfully.');", true);
            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btnDeleteNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        BindNewFiles(path);
    }


    protected void chkFile_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkFile.Checked == true)
            {
                ddlFileCategory.Visible = true;
                txtFileName.Visible = false;
                objCommon.FillDropDownList(ddlFileCategory, "ADMN_DC_DOCUMENT_TYPE_DOC  D INNER JOIN ADMN_DC_DOCUMENT_UPLOAD U ON (D.DNO = U.DNO)", "D.DNO", "D.DOCUMENTNAME", "D.DNO > 0 AND CATEGORY_TYPE='F' AND D.DNO IN (SELECT VALUE FROM FN_SPLIT((SELECT UA_CAT FROM USER_ACC WHERE UA_NO=" + Session["userno"].ToString() + "),','))", "D.DNO");
                pnlCategoryFileUpload.Visible = true;
            }
            else
            {
                ddlFileCategory.Visible = false;
                txtFileName.Visible = true;
                pnlCategoryFileUpload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.chkFile_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion


    protected void lvFinalList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ImageButton del = e.Item.FindControl("btnDelete") as ImageButton;

        if (del.CommandName != "0")
        {
            del.Visible = false;
        }
        else
        {
            del.Visible = true;
        }
    }    

    protected void txtSectionUserName_TextChanged(object sender, EventArgs e)
    {
       try
        {

           objCommon.FillDropDownList(ddlReceiverRole, "FILE_SECTIONMASTER CROSS APPLY dbo.split(SECTION_USER_ROLE, ',') s	INNER JOIN FILE_ROLE_MASTER RM ON (s.Value = RM.ROLE_ID)", "S.Value AS SECTION_USER_ROLE", "RM.ROLENAME", "SECTION_ID = " + Convert.ToInt32(hfSectionId.Value), "");
            //TextBox txtSUserName = sender as TextBox;
            //string[] Value = txtSectionUserName.Text.Split('*');
            //txtSUserName.Text = Value[1].ToString();
            //string[] DOSES_NAME = Value[0].Split('-');           
            //txtSUserName.Focus();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
   
}