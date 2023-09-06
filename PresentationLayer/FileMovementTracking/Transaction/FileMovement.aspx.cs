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
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;

public partial class FileMovementTracking_Transaction_FileMovement : System.Web.UI.Page
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
                    ViewState["FILE_MOVID"] = null;
                    BindListViewFile();
                    objCommon.FillDropDownList(ddlSection, "FILE_SECTIONMASTER", "SECTION_ID", "SECTION_NAME", "POST_TO_DATE IS NULL AND SECTION_ID <> 0", "SECTION_ID");
                    
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = null;
                    lvMovRoute.Visible = false;
                    BindListOfFileMovement(0);
                    btnSubmit.Text = "Submit";
                    lvFileMovement.Visible = true;
                    btnAdd.Enabled = true;
                    Session["FILE_ID"] = null;

                    ViewState["LINK_STATUS"] = null;
                    Session["dtTable"] = null;
                }
            }
            HiddenGetSuName.Value = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region User-Define Methods

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
        ddlSection.SelectedIndex = 0;

        ddlReceiverRole.Items.Clear();
        
        lblPath.Text = string.Empty;
        lvMovRoute.DataSource = null;
        lvMovRoute.DataBind();
        BindListViewFile();
        lvMovRoute.Visible = false;
        Session["RecTbl"] = null;
        txtRemark.Text = string.Empty;
        btnSubmit.Text = "Submit";
        btnAdd.Enabled = true;
        rbtForward.Checked = false;        
        rbtComplete.Checked = false;
        Session["FILE_ID"] = null;
        txtSectionUserName.Text = string.Empty;
        txtSectionUserName.Enabled = true;
        txtNote.Text = string.Empty;
        ViewState["FILE_ID"] = null;
        divSendToUserRole.Visible = false;   //28/01/2022

        pnlView.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;

        ViewState["LINK_STATUS"] = null;

        pnlUploadFiles.Visible = false;
    }

    // this method is used to display the file movement entry list.
    private void BindListOfFileMovement(int FILE_ID)
        {
        try
        {
             DataSet ds = null;
             if (Convert.ToInt32(Session["userno"]) == 1)
             {
                 ds = objFController.GetMovementTrans(FILE_ID, Convert.ToInt32(Session["userno"]));
                 //ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON ( FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID) INNER JOIN PAYROLL_EMPMAS E ON(FT.RECEIVER_ID = E.IDNO)", "FT.FMTRAN_ID, FM.FILE_CODE, FP.FILEPATH, FM.FILE_NAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING'+ ' (' +  SM.SECTION_NAME + ')'  WHEN 'F' THEN 'FORWARD'+ ' (' +  SM.SECTION_NAME +'==>'+ ( SELECT SM.SECTION_NAME  FROM  FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_SECTIONMASTER SM ON (SM.SECTION_ID = FT.SECTION_ID) WHERE FT.FILE_MOVID =FT.FILE_MOVID AND POST_TO_DATE IS NULL AND FT.FMTRAN_ID=(SELECT MAX(FMTRAN_ID) FROM FILE_FILEMOVEMENT_TRANSACTION WHERE FILE_MOVID=FILE_MOVID))+ ')' WHEN 'R' THEN 'RETURN'  WHEN 'I' THEN 'IN PROCESS'+ ' (' +  SM.SECTION_NAME + ')' END) AS STATUS, E.FNAME, FT.REMARK_FOR_RET ", "", "", "FMTRAN_ID ");
             }
             else
             {
                 if (FILE_ID == 0)
                 {                    
                     ds = objFController.GetMovementTrans(FILE_ID, Convert.ToInt32(Session["userno"]));
                 }
                 else
                 {                    
                     ds = objFController.GetMovementTrans(FILE_ID, Convert.ToInt32(Session["userno"]));
                 }
             }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFileMovement.DataSource = ds.Tables[0];
                lvFileMovement.DataBind();               
            }
            else
            {
                lvFileMovement.DataSource = null;
                lvFileMovement.DataBind();              
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                lvMovementInActive.DataSource = ds.Tables[1];
                lvMovementInActive.DataBind();              
            }
            else
            {
                lvMovementInActive.DataSource = null;
                lvMovementInActive.DataBind();              
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

    // this method is used to display the list of Files which are used for movement.
    private void BindListViewFile()
    {
        try
        {          
            DataSet ds = null;
            if (Convert.ToInt32(Session["userno"]) == 1)
            {
                ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE , (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE'  END) AS MOVEMENT_STATUS, MP.FILE_MOVID, MP.SECTIONIDS, F.LINK_STATUS", "F.MOVEMENT_STATUS != 'C' AND U.UA_TYPE IN (3,4,5) ", "F.FILE_ID DESC");
            }
            else
            {
                ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE , (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID, MP.SECTIONIDS, F.LINK_STATUS", "F.MOVEMENT_STATUS != 'C' AND U.UA_TYPE  IN (3,4,5) AND F.USERNO=" + Convert.ToInt32(Session["userno"]), "F.FILE_ID DESC");
              
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
                lvFileMovement.Visible = false;
            }
            DataSet ds1 = null;
            if (Convert.ToInt32(Session["userno"]) == 1)
            {
                ds1 = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE , (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID, MP.SECTIONIDS, F.LINK_STATUS", "F.MOVEMENT_STATUS = 'C' AND U.UA_TYPE  IN (3,4,5)", "F.FILE_ID DESC");
            }
            else
            {
                ds1 = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) INNER JOIN FILE_DOCUMENT_TYPE D ON (F.DOCUMENT_TYPE = D.DOCUMENT_TYPE_ID) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, D.DOCUMENT_TYPE , (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID, MP.SECTIONIDS, F.LINK_STATUS", "F.MOVEMENT_STATUS = 'C' AND U.UA_TYPE  IN (3,4,5) AND F.USERNO=" + Convert.ToInt32(Session["userno"]), "F.FILE_ID DESC");
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                lvInactiveFile.DataSource = ds1;
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

    #endregion  



    #region Page Actions
    // this button is used to insert and update the section name.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtForward.Checked == false && rbtComplete.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Forward Or Complete Option');", true);
                return;
            }

            objFMov.FILE_MOVID = Convert.ToInt32(ViewState["FILE_MOVID"]);
            objFMov.SECTIONNO = Convert.ToInt32(ViewState["SECTIONNO"]);      //EXISTING
            objFMov.SECTION_TO_SEND = Convert.ToInt32(hfSectionId.Value);  //Convert.ToInt32(ddlSection.SelectedValue);           
            objFMov.FILE_ID = Convert.ToInt32(ViewState["FILE_ID"]);
            objFMov.USERNO = Convert.ToInt32(Session["userno"]);
            objFMov.REMARK = txtNote.Text.Trim();

            foreach (ListViewItem i in lvFile.Items)
            {
                HiddenField HdnContentNo = (HiddenField)i.FindControl("hdnSectionIDS");
                Label LblFilePath = (Label)i.FindControl("lblFilePath");
                HiddenField HdnFileNo = (HiddenField)i.FindControl("hdnFileId");

                if (Convert.ToInt32(HdnFileNo.Value) == Convert.ToInt32(ViewState["FILE_ID"]))
                {
                    objFMov.FILEPATH = LblFilePath.Text + " - " + txtSectionUserName.Text;
                    objFMov.SECTIONPATH = HdnContentNo.Value + " - " + Convert.ToInt32(hfSectionId.Value);
                }               
            }
           

            if (rbtForward.Checked == true)
            {
                objFMov.FSTATUS = "F";
                objFMov.RECEIVER_ROLE = Convert.ToInt32(ddlReceiverRole.SelectedValue);
            }         
            else //if (rbtComplete.Checked == true)
            {
                objFMov.FSTATUS = "C";
                objFMov.RECEIVER_ROLE = 0;
            }


            

            CustomStatus cs = (CustomStatus)objFController.AddUpdateCloseFile(objFMov);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                Clear();
                BindListViewFile();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Received Successfully.');", true);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                SendMailToSectionMember(Convert.ToInt32(hfSectionId.Value), Convert.ToInt32(ViewState["FILE_ID"]));
                Clear();
                BindListViewFile();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Forward To Next Section.');", true);
                pnlRadioButton.Visible = true;   //28/01/2022
                pnlList.Visible = true;    //28/01/2022
                pnlNewFiles.Visible = false;
                //pnlFinalList.Visible = false;//28/01/2022
            }           
            else
            {               
                SendMailToAllMembers(Convert.ToInt32(hfSectionId.Value), Convert.ToInt32(ViewState["FILE_ID"]));
                Clear();
                BindListViewFile();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Completed Successfully.');", true);

                pnlRadioButton.Visible = true;   //28/01/2022
                pnlList.Visible = true;    //28/01/2022
                pnlNewFiles.Visible = false;

                //pnlFinalList.Visible = false;//28/01/2022
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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

                    fromEmailId = ds.Tables[1].Rows[0]["SMSSVCID"].ToString();
                    fromEmailPwd = ds.Tables[1].Rows[0]["SMSSVCPWD"].ToString();

                    string receiver = string.Empty;
                    string userid = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (receiver == string.Empty)
                        {
                            receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                            userid = ds.Tables[0].Rows[i]["userid"].ToString();
                        }
                        else
                        {
                            receiver = receiver + "," + ds.Tables[0].Rows[i]["EMAIL"].ToString();
                            userid = userid + "," + ds.Tables[0].Rows[i]["userid"].ToString();
                        }
                    }

                   // sendmail("mis.srinagar@iitms.co.in", "pass@iitms", receiver, "Regarding File Movement", "Dear Sir");                    
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
        
   


    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain
        BindListOfFileMovement(Convert.ToInt32(ViewState["FILE_ID"]));
    }
    // This button is used to show the details of the particular selected record.
    protected void btnReceive_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        try
        {
            ViewState["FILE_MOVID"] = Convert.ToInt32(btn.CommandArgument);  
            ViewState["FILE_ID"] = Convert.ToInt32(btn.CommandName);
            Session["FILE_ID"] = Convert.ToInt32(btn.CommandName);
            ViewState["LINK_STATUS"] = Convert.ToInt32(btn.ToolTip);
            pnlUploadFiles.Visible = true;
           ShowDetailsOnReturn(Convert.ToInt32(ViewState["FILE_ID"]));

           GetSelectedFileDetails(Convert.ToInt32(ViewState["FILE_ID"]));
         
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileReceive.UpdateTrainingDetailsConfirm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void GetSelectedFileDetails(int file_no)
    {
        try
        {
            DataSet ds = objFController.GetSelectedFileDetails(file_no);
             if (ds.Tables[0].Rows.Count > 0)
             {               
                 ViewState["UplNo"] = ds.Tables[0].Rows[0]["UPLNO"].ToString();                
                 lblFilesDestinationPath.Text = ds.Tables[0].Rows[0]["FOLDER_PATH"].ToString();
                 lblFName.Text = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
             }

             if (ds.Tables[1].Rows.Count > 0)
             {
                 lvFinalList.DataSource = ds.Tables[1];
                 lvFinalList.DataBind();
                 lvFinalList.Visible = true;
                 Session["dtTable"] = ds.Tables[1];
             }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.GetSelectedFileDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rbtForward_CheckedChanged(object sender, EventArgs e)
    {
       
        rbtComplete.Checked = false;     
        txtSectionUserName.Text = string.Empty;
        hfSectionId.Value = "";
        Session["OperType"] = 1;
        txtSectionUserName.Enabled = true;
        divSendToUserRole.Visible = true;
    }

    protected void rbtComplete_CheckedChanged(object sender, EventArgs e)
    {       
        rbtForward.Checked = false;
        txtSectionUserName.Text = string.Empty;
        hfSectionId.Value = "";
        Session["OperType"] = 3;        
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

    protected void txtSectionUserName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlReceiverRole, "FILE_SECTIONMASTER CROSS APPLY dbo.split(SECTION_USER_ROLE, ',') s	INNER JOIN FILE_ROLE_MASTER RM ON (s.Value = RM.ROLE_ID)", "S.Value AS SECTION_USER_ROLE", "RM.ROLENAME", "SECTION_ID = " + Convert.ToInt32(hfSectionId.Value), "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Master_FileReceive.txtSectionUserName_TextChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to show the details fetch from database.
    private void ShowDetailsOnReturn(int file_no)
    {
        try
        {
            string fStatus = string.Empty;
                 
              CustomStatus cs = (CustomStatus)objFController.GetFileToReceive(Convert.ToInt32(Session["userno"]), file_no);
                if (cs.Equals(CustomStatus.RecordNotFound))
                {
                    Clear();
                    Showmessage("This File is in Moving Condition.");                   
                    return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    pnlView.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    pnlList.Visible = false;
                    pnlMovemnt.Visible = false;
                    pnlRadioButton.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('File Received Successfully.');", true);
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

       
    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        pnlList.Visible = true;
        pnlMovemnt.Visible = true;
        pnlRadioButton.Visible = true;
        HiddenGetSuName.Value = "0";
    }   

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ViewState["FILE_ID"] != null)
        {
            ShowReport("File Movement Details", "rptFileMovement.rpt");
        }
        else
        {
            Showmessage("Please Select At Least One File.");
            return;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
            if( ViewState["FILE_ID"]!=null)
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + Session["userno"] + "," + "@P_FILE_ID=" + ViewState["FILE_ID"] + "," + "@P_FDATE=null,@P_TDATE=null";
            }
            else
            {
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + Session["idno"] + "," + "@P_FILE_ID=0" + "," + "@P_FDATE='" + DateTime.Now.ToString("dd/mm/yyyy") + "'" + "," + "@P_TDATE='" + DateTime.Now.ToString("dd/mm/yyyy") + "'";
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + Session["userno"] + "," + "@P_FILE_ID=0" + "," + "@P_FDATE=null,@P_TDATE=null";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
            //ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);     
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    



    private DataTable CreateTabel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("SECTIONNAME", typeof(string)));        
        dt.Columns.Add(new DataColumn("SECTIONID", typeof(int)));
        return dt;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (lvMovRoute.Items.Count != 0)
            {
                btnAdd.Enabled = false;
                ddlSection.SelectedIndex = 0;
                return;
            }
            //if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            //{
            //    btnAdd.Enabled = false;
            //    ddlSection.SelectedIndex = 0;
            //    return;
            //}

            lvMovRoute.Visible = true;
           
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();
                if (CheckDuplicateSectionName(dt, ddlSection.SelectedItem.Text))
                {
                    ClearRec();
                    Showmessage("This Section Name Already Exist.");
                    //objCommon.DisplayMessage(this.updActivity, "This Section Name Already Exist.", this.Page);
                    return;                    
                }
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["SECTIONNAME"] = ddlSection.SelectedItem.Text == null ? string.Empty : ddlSection.SelectedItem.Text;               
                dr["SECTIONID"] = ddlSection.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlSection.SelectedValue);

                if (lblPath.Text == string.Empty || lblPath.Text == "")
                {
                    lblPath.Text = Session["userfullname"].ToString() + " - " + ddlSection.SelectedItem.Text;          // Session["userfullname"].ToString() + " - " +       
                }
                else
                {
                    lblPath.Text = lblPath.Text + " - " + ddlSection.SelectedItem.Text;                  
                }
                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvMovRoute.DataSource = dt;
                lvMovRoute.DataBind();
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                
                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();

                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["SECTIONNAME"] = ddlSection.SelectedItem.Text == null ? string.Empty : ddlSection.SelectedItem.Text;
                dr["SECTIONID"] = ddlSection.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlSection.SelectedValue);

                lblPath.Text = ddlSection.SelectedItem.Text;
                
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvMovRoute.DataSource = dt;
                lvMovRoute.DataBind();
            }
        }        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {

             DataTable dt = (DataTable)Session["RecTbl"];   
             dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
             lblPath.Text = string.Empty;
             foreach (DataRow dr in dt.Rows)
             {
                 if (lblPath.Text == string.Empty)
                 {
                     lblPath.Text = dr["SECTIONNAME"].ToString();
                 }
                 else
                 {
                     lblPath.Text = lblPath.Text + " - " + dr["SECTIONNAME"].ToString();
                 }
             }

             Session["RecTbl"] = dt;
             lvMovRoute.DataSource = dt;
             lvMovRoute.DataBind();
             lvMovRoute.Visible = true;
             if (lvMovRoute.Items.Count == 0)
             {
                 btnAdd.Enabled = true;
                 ddlSection.SelectedIndex = 0;
                 return;
             }
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {

        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {                    
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    private bool CheckDuplicateSectionName(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SECTIONNAME"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }
    protected void ClearRec()
    {
        ddlSection.SelectedIndex = 0;        
 
    }

   
    protected void rdoFile_CheckedChanged(object sender, EventArgs e)
    {

        RadioButton rdbBtn = sender as RadioButton;
        int file_id = int.Parse(rdbBtn.ToolTip);
        ViewState["FILE_ID"] = int.Parse(rdbBtn.ToolTip);
        string FilePath = Docpath + "FILEMOVEMENT\\UPLOADFILES\\";
        BindListOfFileMovement(file_id);

        DataSet dFile = objFController.RetrieveDocuments(file_id, Convert.ToInt32(Session["userno"]), FilePath);
        if (dFile.Tables.Count > 0)
        {
            ViewState["FILE_NAME"] = dFile.Tables[0].Rows[0]["FILENAME"].ToString();
            if (dFile.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "1")
            {               
                lvFinalList.DataSource = dFile;
                lvFinalList.DataBind();
                pnlFinalList.Visible = true;

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                pnlNewFiles.Visible = false;
            }
            else
            {
                lvFinalList.DataSource = null;
                lvFinalList.DataBind();
                pnlFinalList.Visible = false;

                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + dFile.Tables[0].Rows[0]["FILE_NAME"].ToString();
                BindNewFiles(path);

            }
        }
    }

    protected void rdoFileInactive_CheckedChanged(object sender, EventArgs e)
    {

        RadioButton rdbBtn = sender as RadioButton;
        int file_id = int.Parse(rdbBtn.ToolTip);
        ViewState["FILE_ID"] = int.Parse(rdbBtn.ToolTip);
        string FilePath = Docpath + "FILEMOVEMENT\\UPLOADFILES\\";
        BindListOfFileMovement(file_id);

        DataSet dFile = objFController.RetrieveDocuments(file_id, Convert.ToInt32(Session["userno"]), FilePath);
        if (dFile.Tables.Count > 0)
        {
            ViewState["FILE_NAME"] = dFile.Tables[0].Rows[0]["FILENAME"].ToString();
            if (dFile.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "1")
            {
                lvFinalList.DataSource = dFile;
                lvFinalList.DataBind();
                pnlFinalList.Visible = true;

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                pnlNewFiles.Visible = false;
            }
            else
            {
                lvFinalList.DataSource = null;
                lvFinalList.DataBind();
                pnlFinalList.Visible = false;

                path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + dFile.Tables[0].Rows[0]["FILE_NAME"].ToString();
                BindNewFiles(path);

            }
        }
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

    protected string GetFileName(object obj)
    {
        string f_name = string.Empty;

        f_name = obj.ToString();
        return f_name;
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

            path = Docpath + "FILEMOVEMENT\\UPLOADFILES\\" + ViewState["FILE_NAME"].ToString();
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

    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.AlternateText);
    }

    public void DownloadFile(string fileName)
    {
        try
        {
            path = Docpath + "DOCUMENTANDSCANNING\\FILEUPLOAD\\" + ViewState["FOLDER_PATH"].ToString();
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

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
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

    protected void rdbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbList.SelectedValue == "0")
            {
                pnlList.Visible = true;
                pnlInActive.Visible = false;
                pnlInActiveMovement.Visible = false;
                lvFileMovement.Visible = true;
                BindListOfFileMovement(0);

                lvFinalList.DataSource = null;
                lvFinalList.DataBind();

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();

                ViewState["FILE_ID"] = null;

                foreach (ListViewItem i in lvFile.Items)
                {
                    RadioButton rdbFile = (RadioButton)i.FindControl("rdoFile");
                    rdbFile.Checked = false;
                }

                
            }
            else
            {
                pnlList.Visible = false;
                pnlInActive.Visible = true;
                pnlInActiveMovement.Visible = true;
                lvFileMovement.Visible = false;
                BindListOfFileMovement(0);

                pnlView.Visible = false;
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                pnlUploadFiles.Visible = false;

                lvFinalList.DataSource = null;
                lvFinalList.DataBind();             

                lvNewFiles.DataSource = null;
                lvNewFiles.DataBind();
                ViewState["FILE_ID"] = null;

                foreach (ListViewItem i in lvInactiveFile.Items)
                {
                    RadioButton rdbFileInActive = (RadioButton)i.FindControl("rdoFileInactive");
                    rdbFileInActive.Checked = false;
                }
                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.rdbList_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion


    #region Upload Documents

    // This is used to add new documents if any.
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string file = string.Empty;

            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (ViewState["LINK_STATUS"].ToString() == "1")
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

                    if (ViewState["LINK_STATUS"].ToString() == "1")
                    {
                        path = file + FileUpload1.FileName + "_" + Session["userno"].ToString();       ///20/06/2022;
                    }
                    else
                    {
                        path = file + "\\" + FileUpload1.FileName + "_" + Session["userno"].ToString();    ///20/06/2022;
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
                        BindNewFilesSelected(file);
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
    #endregion

    #region Direct Files Upload  

    private void BindNewFilesSelected(string PATH)
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


   
}