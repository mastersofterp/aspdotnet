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
using System.Collections.Generic;

using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;


public partial class Itle_StudAssignment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    BlobController objBlob = new BlobController();

    AssignmentController objAM = new AssignmentController();
    Assignment objAssign = new Assignment();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];
    string PageId;
    static decimal File_size;

    #region Page Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                //if (Session["ICourseNo"] == null)
                //    Response.Redirect("~/Itle/selectCourse.aspx");

                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourse.ForeColor = System.Drawing.Color.Green;
                BindListView();
                GetAttachmentSize();
                BlobDetails();
                Session["Attachments"] = null;
                // Used to insert id,date and courseno in Log_History table
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));

            }
        }

        // Used to get maximum size of file attachment
        GetAttachmentSize();
       
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudAssignment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudAssignment.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            // DataSet ds = objAM.GetAllAssignmentListByCourseNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]));
            DataSet ds = objAM.GetAllAssignmentListByCourseNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["idno"]));



            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAssignment.DataSource = ds;
                lvAssignment.DataBind();
                DivAssignmentList.Visible = true;
            }
            else
            {
                lvAssignment.DataSource = null;
                lvAssignment.DataBind();
                DivAssignmentList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    private void checkfiletype()
    {
        string doc_type = objCommon.LookUp("ACD_IASSIGNMASTER", "DOC_TYPE_ID", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + "");
        if (doc_type == "2")
        {
            divreply.Visible = false;
        }
        else
        {
            divreply.Visible = true;
        }

    }
    private void SaveAssignment()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            Assignment objAssign = new Assignment();
            List<AssignmentAttachment> attachments = new List<AssignmentAttachment>();

            string filename = string.Empty;
            objAssign.AS_NO = Convert.ToInt32(ViewState["asno"].ToString());
            objAssign.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objAssign.IDNO = Convert.ToInt32(Session["idno"].ToString());
            objAssign.REPLY_DATE = DateTime.Now;
            objAssign.DESCRIPTION = txtReplyDescription.Text.Trim();
            objAssign.STATUS = '1';


            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                DataTable dt = ((DataTable)Session["Attachments"]);
                foreach (DataRow dr in dt.Rows)
                {
                    AssignmentAttachment attach = new AssignmentAttachment();
                    attach.AttachmentId = Convert.ToInt32(dr["ATTACH_ID"]);
                    attach.FileName = dr["FILE_NAME"].ToString();
                    attach.FilePath = dr["FILE_PATH"].ToString();
                    attach.Size = Convert.ToInt32(dr["SIZE"]);
                    attachments.Add(attach);
                }
            }
            objAssign.Attachments = attachments;

            CustomStatus cs = (CustomStatus)objAM.ReplyAssignment(objAssign, fuAssign);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                lblStatus.Text = "Record Saved";
            }
            else
                if (cs.Equals(CustomStatus.FileExists))
                {
                    lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.SaveAssignment -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetail(int assignno, int courseno, int sessionno, int idno)
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            ViewState["asno"] = assignno;
            Object objCheckStatus;
            DateTime serverDate;
            DateTime serverTime;

            DataTableReader dtr = objAM.GetSingleAssignmentForStudent(Convert.ToInt32(ViewState["asno"]), Convert.ToInt32(Session["userno"].ToString()));
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    tdTopic.InnerText = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();

                    //txtDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    divDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    Session["txtDescription"] = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    lblCurrdate.Text = dtr["ASSIGNDATE"] == null ? "" : Convert.ToDateTime(dtr["ASSIGNDATE"].ToString()).ToString("dd-MMM-yyyy");
                    lblSubmitDate.Text = dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("dd-MMM-yyyy");
                    string EndTime = dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("HH:mm:ss");

                    //serverDate = Convert.ToDateTime(dtr["SERVER_DATE"] == null ? "" : Convert.ToDateTime(dtr["SERVER_DATE"].ToString()).ToString("dd-MMM-yyyy"));
                    //serverTime = Convert.ToDateTime(dtr["SERVER_DATE"] == null ? "" : Convert.ToDateTime(dtr["SERVER_DATE"].ToString()).ToString("HH:mm:ss"));
                    serverDate = Convert.ToDateTime(dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("dd-MMM-yyyy"));
                    serverTime = Convert.ToDateTime(dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("HH:mm:ss"));

                    btnSubmit.Enabled = true;
                    trMessage.Visible = false;
                    //txtDescription.ReadOnly = true;

                    //To return replied assignment for edit purpose
                    RepliedAssignment();

                    if (lblSubmitDate.Text.Trim() != "")
                    {

                        if (serverDate >= Convert.ToDateTime(lblSubmitDate.Text.Trim()))
                        {
                            if (Convert.ToDateTime(DateTime.Now.ToShortDateString()) <= Convert.ToDateTime(lblSubmitDate.Text.Trim()))
                            {
                                if (Convert.ToDateTime(serverTime).TimeOfDay > Convert.ToDateTime(EndTime).TimeOfDay)
                                //if (Convert.ToDateTime(DateTime.Now.ToShortDateString()) == Convert.ToDateTime(lblSubmitDate.Text.Trim()))
                                {
                                    if (DateTime.Now.TimeOfDay > Convert.ToDateTime(EndTime).TimeOfDay)
                                    {
                                        lblMessage.Text = "Submission Time of This Assignment Is Over. You Can Not Reply Now.";
                                        btnSubmit.Visible = false;
                                        btnSubmit.Enabled = false;
                                        trMessage.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Submission Date & Time of This Assignment Is Over. You Can Not Reply Now.";
                                btnSubmit.Visible = false;
                                btnSubmit.Enabled = false;
                                trMessage.Visible = true;
                            }
                        }

                    }

                    objCheckStatus = objAM.GetSingleAssignmentCheckStatusForStudent(Convert.ToInt32(ViewState["asno"]), Convert.ToInt32(Session["userno"].ToString()));

                    if (objCheckStatus != null)
                    {
                        if (Convert.ToInt32(objCheckStatus.ToString()) == 1)
                        {
                            lblMessage.Text = "Your Assignment Reply Is Checked By Faculty.You Can Not Reply Any More.";
                            btnSubmit.Visible = false;
                            btnSubmit.Enabled = false;
                            trMessage.Visible = true;
                            //CHECKED
                        }
                    }

                }
            }
            if (dtr != null) dtr.Close();
            ViewState["vsSubmit"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Attachments

    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));
                ViewState["FILESIZE"] = File_size;
            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_STUDENT,'Bytes')AS FILE_SIZE_STUDENT", "PAGE_ID=" + PageId);
                    ViewState["FILESIZE"] = lblFileSize.Text;
                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);
                    ViewState["FILESIZE"] = lblFileSize.Text;
                }



        }
        catch (Exception ex)
        {

        }

    }

    void GetAttachmentByAsNo(int assignno)
    {

        try
        {
            DataSet ds;
            ds = objAM.GetAssignmentAttachments(assignno);
            lvAttachments.DataSource = ds;
            lvAttachments.DataBind();

            
            DataSet DS1 = objCommon.FillDropDown("ACD_IASSIGNMASTER", "ATTACHMENT", "", "AS_NO=" + assignno, "");
            string blob = DS1.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            if (blob == "1")
            {
                foreach (ListViewItem lvRow in lvAttachments.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckBox1 = (Control)lvRow.FindControl("tdDownloadLink");

                    ckBox.Visible = true;
                    ckBox1.Visible = true;
                }
            }
            else
            {
              
                foreach (ListViewItem lvRow in lvAttachments.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownload");
                    
                    ckBox.Visible = true;
                }
            
            }

        }

        catch (Exception ex)
        {

        }

    }

    void GetAttachmentAssignmenet(int assignno)
    {

        try
        {
            DataSet ds;
            ds = objAM.GetAssignmentAttachments(assignno);
            lvDoc.DataSource = ds;
            lvDoc.DataBind();

        }
        catch (Exception ex)
        {

        }

    }

    private DataTable GetAttachmentDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ATTACH_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("FILE_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FILE_PATH", typeof(string)));
        dt.Columns.Add(new DataColumn("SIZE", typeof(int)));
        return dt;
    }

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();

            if (dt.Rows.Count > 0)
            {

                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = lvCompAttach.FindControl("divattachblob");
                    Control ctrHeader1 = lvCompAttach.FindControl("divpreview");
                    ctrHeader.Visible = true;
                    ctrHeader1.Visible = true;

                    foreach (ListViewItem lvRow in lvCompAttach.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdattachblob");
                        Control ckbox1 = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckbox1.Visible = true;
                    }

                }
                else
                {

                    Control ctrHeader2 = lvCompAttach.FindControl("divattach");
                    ctrHeader2.Visible = true;


                    foreach (ListViewItem lvRow in lvCompAttach.Items)
                    {
                        Control ckBox2 = (Control)lvRow.FindControl("tdattach");

                        ckBox2.Visible = true;

                    }


                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ATTACH_ID"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void RepliedAssignment()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();

            //used to access attachments
            DataSet ds = objAM.GetAllAtachmentRepliedByStud(Convert.ToInt32(ViewState["asno"]), Convert.ToInt32(Session["idno"]));
            DataTable dt = new DataTable();
            //int totFiles = ds.Tables[0].Rows.Count;
            // if (ds.Tables[0].Rows.Count > 0)                                 
            //{
            dt = this.GetAttachmentDataTable();
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                //dt = this.GetAttachmentDataTable();
                DataRow dr = dt.NewRow();
                dr["ATTACH_ID"] = ds.Tables[0].Rows[j]["ATTACHMENT_ID"];
                dr["FILE_NAME"] = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_PATH"] = ds.Tables[0].Rows[j]["FILE_PATH"].ToString();
                dr["SIZE"] = ds.Tables[0].Rows[j]["SIZE"];
                dt.Rows.Add(dr);
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

            if (dt.Rows.Count > 0)
            {
                divAttch.Style["display"] = "block";
                lvCompAttach.DataSource = dt;
                lvCompAttach.DataBind();



                if (lblBlobConnectiontring.Text != "")
                {
                    Control ctrHeader = lvCompAttach.FindControl("divattachblob");
                    Control ctrHeader1 = lvCompAttach.FindControl("divpreview");
                    ctrHeader.Visible = true;
                    ctrHeader1.Visible = true;

                    foreach (ListViewItem lvRow in lvCompAttach.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdattachblob");
                        Control ckbox1 = (Control)lvRow.FindControl("tdBlob");
                        ckBox.Visible = true;
                        ckbox1.Visible = true;
                    }

                }
                else
                {

                    Control ctrHeader2 = lvCompAttach.FindControl("divattach");
                    ctrHeader2.Visible = true;


                    foreach (ListViewItem lvRow in lvCompAttach.Items)
                    {
                        Control ckBox2 = (Control)lvRow.FindControl("tdattach");

                        ckBox2.Visible = true;

                    }


                }

            }



            DataTableReader dtr = objAM.GetAssignmentRepliedByStudent(Convert.ToInt32(ViewState["asno"]), Convert.ToInt32(Session["idno"].ToString()));
            if (dtr != null)
            {
                if (dtr.Read())
                {

                    txtReplyDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    Session["txtReplyDescription"] = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    //hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    //lblCurrdate.Text = dtr["ASSIGNDATE"] == null ? "" : Convert.ToDateTime(dtr["ASSIGNDATE"].ToString()).ToString("dd-MMM-yyyy");

                    btnSubmit.Enabled = true;
                    trMessage.Visible = false;
                    //txtDescription.ReadOnly = true;

                }
            }
            if (dtr != null) dtr.Close();
            ViewState["vsSubmit"] = null;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudAssignment.RepliedAssignment -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Page Events

    private void Clear()
    {
        try
        {
            txtReplyDescription.Text = "";
            pnlAssignDetail.Visible = false;
            ViewState["asno"] = null;
            Session["Attachments"] = null;
            DivAssignmentList.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ViewState["vsSubmit"] == null)
        {
            //  ControlReset(true);
            ControlResetTrue();
            //txtDescription.Text = Session["txtDescription"].ToString();
            divDescription.Text = Session["txtDescription"].ToString();
            txtReplyDescription.Visible = true;

            //if (ViewState["FILESIZE"].ToString() != null)
            //{
            //    lblFileSize.Text = ViewState["FILESIZE"].ToString();
            //}
        }

        if (Convert.ToInt16(ViewState["vsSubmit"]) == 1)
        {
            if (!ValidDocType())
            {
                return;
            }
            submitAssignReply();
            ViewState["vsSubmit"] = null;
            ViewState["asno"] = null;
            pnlAssignDetail.Visible = false;
            Session["Attachments"] = null;
            //if (ViewState["FILESIZE"].ToString() != null)
            //{
            //    lblFileSize.Text = ViewState["FILESIZE"].ToString();
            //}

            return;
        }
        ViewState["vsSubmit"] = 1;
        checkfiletype();

        string file = objCommon.LookUp("ACD_IASSIGNMASTER", "File_Type", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + "");

        //if (file == string.Empty)
        //{
        //    divAttch.Visible = false;
        //}
        //else
        //{
        //    divAttch.Visible = true;
        //}

        string file_name = "";

        string[] value = file.Split(',');
        string temp;
        for (int j = 0; j < value.Length; j++)
        {

            temp = value[j];
            string ext = objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "EXTENTION_ID='" + temp + "'");
            file_name += "," + ext;
        }

        lblFiletype.Text = file_name.ToString();

        DataTable dt;
        dt = ((DataTable)Session["Attachments"]);
        if (dt != null)
        {
            dt.Dispose();
            divAttch.Visible = false;
        }
        else
        {
            divAttch.Visible = false;
        }
        if (file != string.Empty)
        {
            divAttch.Visible = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveAssignment();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnDesplayMarks_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnView = sender as LinkButton;
            int as_no = int.Parse(btnView.CommandArgument);
            ViewState["sa_no"] = as_no;
            pnlDisplayMarks.Visible = true;
            pnlAssignDetail.Visible = false;
            DivAssignmentList.Visible = false;
            //txtRepliedAnswer.ReadOnly = true;

            DataTableReader dtr = objAM.DisplayAssignMarks(Convert.ToInt32(ViewState["sa_no"]), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["SessionNo"]), Convert.ToInt32(Session["idno"]));

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {

                    lblSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    //txtRepliedAnswer.Text = dtr["REPLIED_ANSWER"] == null ? "" : dtr["REPLIED_ANSWER"].ToString();
                    divCheckedAssign.InnerHtml = dtr["REPLIED_ANSWER"] == null ? "" : dtr["REPLIED_ANSWER"].ToString();
                    lblMarks.Text = dtr["STUDENT_MARKS"] == null ? "" : dtr["STUDENT_MARKS"].ToString();
                    //lblCurrdate.Text = DateTime.Today.ToString();
                    lblRemark.Text = dtr["REMARKS"] == null ? "" : dtr["REMARKS"].ToString();



                }
                if (dtr != null) dtr.Close();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_FrequentlyAskedQuestions_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

   

    protected void btnAttachFile_Click(object sender, EventArgs e)
    {
        try
        {

            bool result;
            GetAttachmentSize();

            string filename = string.Empty;
            string FilePath = string.Empty;

            if (fuAssign.HasFile)
            {
                string DOCFOLDER = file_path + "ITLE\\upload_files\\student_assignment_reply";

                if (!System.IO.Directory.Exists(DOCFOLDER))
                {
                    System.IO.Directory.CreateDirectory(DOCFOLDER);

                }
                string contentType = contentType = fuAssign.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(fuAssign.PostedFile.FileName);
                 int SRNO = Convert.ToInt32(objCommon.LookUp("ACD_IASSIGNMENT_STUDREPLY_ATTACHMENTS", "(ISNULL(MAX(SR_NO),0))+1 AS SR_NO", ""));

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    DataTable dt1;
                    dt1 = ((DataTable)Session["Attachments"]);
                    int maxnofile = Convert.ToInt32(objCommon.LookUp("ACD_IASSIGNMASTER", "MAX_NO_OF_FILE", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + ""));

                    if (dt1.Rows.Count >= maxnofile)
                    {
                        objCommon.DisplayMessage("You Can Upload Max No Of File " + maxnofile, this);
                        return;
                    }
                    // string attachentId = dt1.Tables[0].Rows[0]["ATTACH_ID"].ToString();
                    int attachmentId = dt1.Rows.Count;


                    filename = SRNO + "_itleStudAssignment_" + Session["userno"] + "-" + attachmentId;


                }
                else
                {
                    filename = SRNO + "_itleStudAssignment_" + Session["userno"];

                }


                objAssign.ATTACHMENT = filename;
                objAssign.FILEPATH = fuAssign.FileName;

                string fileName = System.Guid.NewGuid().ToString() + fuAssign.FileName.Substring(fuAssign.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));

                string file1 = objCommon.LookUp("ACD_IASSIGNMASTER", "File_Type", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + "");

                //string extension = objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "EXTENTION_ID='" +file1+ "'");

                if (file1 == "")
                {
                    lblFiletype.Text = "doc,.docx,.odt,.pdf,.rtf,.tex,.txt,.wpd,";
                    file1 = lblFiletype.Text;
                }

                string filetype = lblFiletype.Text;
                DataSet dsDept = objCommon.FillDropDown("dbo.split('" + filetype.ToString() + "',',')", "distinct value as extension", "", "", "");
                int filecount = 0;
                DataTable dtt = dsDept.Tables[0];
                foreach (DataRow dr in dtt.Rows)
                {
                    var dttextension = dr["extension"].ToString();

                    if (fileExtention.ToUpper() == dttextension.ToUpper())
                    {
                        filecount++;
                    }
                }


                if (filecount > 0)
                {
                    divAttch.Visible = true;
                    DataSet dsPURPOSE = new DataSet();

                    dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");

                    //if (count != 0)
                    //{

                    string filePath = file_path + "ITLE\\upload_files\\student_assignment_reply\\" + fileName;


                    string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                    string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                    result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                    if (fuAssign.PostedFile.ContentLength < File_size)
                    {
                        

                        if (result == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, fuAssign);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }
                            objAssign.FILEPATH = fuAssign.FileName;
                        }
                        else
                        {
                            HttpPostedFile file = fuAssign.PostedFile;
                            fuAssign.SaveAs(filePath);
                            objAssign.FILEPATH = file_path + "ITLE\\upload_files\\student_assignment_reply\\" + fileName;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                        return;
                    }

                    DataTable dt;

                    if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                    {


                        //int maxnofile = Convert.ToInt32(objCommon.LookUp("ACD_IASSIGNMASTER", "MAX_NO_OF_FILE", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + ""));



                        dt = ((DataTable)Session["Attachments"]);
                        //if (dt.Rows.Count >= maxnofile)
                        //{
                        //    objCommon.DisplayMessage("You Can Upload Max No Of File " + maxnofile, this);
                        //    return;
                        //}
                        DataRow dr = dt.NewRow();

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            if (result == true)
                            {
                                dr["FILE_NAME"] = filename + ext;
                            }
                            else
                            {
                                dr["FILE_NAME"] = fuAssign.FileName;
                            }
                            dr["FILE_PATH"] = objAssign.FILEPATH;
                            dr["SIZE"] = (fuAssign.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session["Attachments"] = dt;
                            this.BindListView_Attachments(dt);

                        }
                        else
                        {
                            dt = this.GetAttachmentDataTable();
                            dr = dt.NewRow();
                            dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            if (result == true)
                            {
                                dr["FILE_NAME"] = filename + ext;
                            }
                            else
                            {
                                dr["FILE_NAME"] = fuAssign.FileName;
                            }
                            dr["FILE_PATH"] = objAssign.FILEPATH;
                            dr["SIZE"] = (fuAssign.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session.Add("Attachments", dt);
                            this.BindListView_Attachments(dt);
                        }
                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        DataRow dr = dt.NewRow();
                        dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (result == true)
                        {
                            dr["FILE_NAME"] = filename + ext;
                        }
                        else
                        {
                            dr["FILE_NAME"] = fuAssign.FileName;
                        }
                        dr["FILE_PATH"] = objAssign.FILEPATH;
                        dr["SIZE"] = (fuAssign.PostedFile.ContentLength);
                        dt.Rows.Add(dr);
                        Session.Add("Attachments", dt);
                        this.BindListView_Attachments(dt);
                    }
                }

                else
                {
                    objCommon.DisplayMessage("File Format not supported.", this);
                }
            }


            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
            }

            divDescription.Text = Session["txtDescription"].ToString();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnSaveDD_Info_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    

    }

    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = Convert.ToInt32(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
                if (dt.Rows.Count == 0)
                {
                    Session["Attachments"] = null;
                }
            }

            //to permanently delete from database in case of Edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                string count = objCommon.LookUp("ACD_IASSIGNMENT_STUDREPLY_ATTACHMENTS", "ATTACHMENT_ID", "ATTACHMENT_ID=" + fileId + "AND STUDENT_IDNO=" + Convert.ToInt32(Session["idno"]) + "AND AS_NO=" + Convert.ToInt32(ViewState["asno"]));
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ACD_IASSIGNMENT_STUDREPLY_ATTACHMENTS", "ATTACHMENT_ID=" + fileId + "AND STUDENT_IDNO=" + Convert.ToInt32(Session["idno"]) + "AND AS_NO=" + Convert.ToInt32(ViewState["asno"]));
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        divDescription.Text = Session["txtDescription"].ToString();
    }

    #endregion

    #region Public Methods

    public void ControlReset(Boolean status)
    {
        lblReply.Visible = status;
        //fuAssign.Visible = status;
        tdUploadFiles.Visible = status;
        txtReplyDescription.Visible = false;
        //fuAssign.Visible = status;
        tdFileSize.Visible = status;
        btnSubmit.Text = status == true ? "Submit" : "Reply";
        // btnSave.Visible = status;
        DivAssignmentList.Visible = false;
    }

    public void ControlResetTrue()
    {
        DivAssignmentList.Visible = false;
        lblReply.Visible = true;
        tdUploadFiles.Visible = true;
        txtReplyDescription.Visible = true;
        tdFileSize.Visible = true;
        btnSubmit.Text = true == true ? "Submit" : "Reply";
        // btnSave.Visible = true;

        //  pnlAssignDetail.Visible = true;
    }

    public string GetFileName(object filename, object assingmentno)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/assignment/assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/" + filename.ToString();
        else
            return "";
    }

    void submitAssignReply()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            Assignment objAssign = new Assignment();
            List<AssignmentAttachment> attachments = new List<AssignmentAttachment>();

            string filename = string.Empty;
            objAssign.AS_NO = Convert.ToInt32(ViewState["asno"].ToString());
            objAssign.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objAssign.IDNO = Convert.ToInt32(Session["idno"].ToString());
            objAssign.REPLY_DATE = DateTime.Now;
            objAssign.DESCRIPTION = txtReplyDescription.Text.Trim();

            if (lblBlobConnectiontring.Text != "")
            {
                objAssign.ATTACHMENT = "1";
            }
            else
            {
                objAssign.ATTACHMENT = "0";
            }
            
            objAssign.STATUS = '1';

            //if (fuAssign.PostedFile.ContentLength < File_size || fuAssign.HasFile.ToString() == "")
            //{

            //    objAssign.ATTACHMENT = fuAssign.FileName;

            //    //fuAssign.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
            //}
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                DataTable dt = ((DataTable)Session["Attachments"]);
                foreach (DataRow dr in dt.Rows)
                {
                    AssignmentAttachment attach = new AssignmentAttachment();
                    attach.AttachmentId = Convert.ToInt32(dr["ATTACH_ID"]);
                    attach.FileName = dr["FILE_NAME"].ToString();
                    attach.FilePath = dr["FILE_PATH"].ToString();
                    attach.Size = Convert.ToInt32(dr["SIZE"]);
                    attachments.Add(attach);
                }
            }
            objAssign.Attachments = attachments;


            //else
            //{
            //    objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
            //    return;

            //}




            CustomStatus cs = (CustomStatus)objAM.ReplyAssignment(objAssign, fuAssign);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(updReplyAssign, "Assignment Submitted Successfully", this.Page);
                Clear();
                return;
                //lblStatus.Text = "Record Set";
            }
            else
                if (cs.Equals(CustomStatus.FileExists))
                {
                    lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        txtReplyDescription.Text = "";
        DivAssignmentList.Visible = true;

    }



    public string GetStatus(object status)
    {

        DateTime DT = Convert.ToDateTime(status);
        if (Convert.ToDateTime(Convert.ToDateTime(DT).ToString("dd-MMM-yyyy")) <= DateTime.Today)
        {
            if (Convert.ToDateTime(Convert.ToDateTime(DT).ToString("dd-MMM-yyyy")) == DateTime.Today)
            {
                if (Convert.ToDateTime(DT).TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    return "<span style='color:Red'>Expired</span>";
                }
                else
                {
                    return "<span style='color:Green'>Active</span>";
                }
            }
            else
            {
                return "<span style='color:Red'>Expired</span>";
            }
        }
        else
        {
            return "<span style='color:Green'>Active</span>";
        }

    }

    public Boolean checkeEnable(object as_no)
    {

        if (as_no != null)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    #endregion
    protected void btnimgdow_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnimgdow = sender as ImageButton;
        int assignno = int.Parse(btnimgdow.CommandArgument);
        ViewState["asno"] = assignno;

        GetAttachmentAssignmenet(assignno);
        upd_ModalPopupExtender1.Show();
    }

    protected void GetAttachmentDown(int bookno)
    {

        try
        {
            ILibraryController objLC = new ILibraryController();
            DataSet ds;
            ds = objLC.GetEBookAttachments(bookno);

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvDoc.DataSource = ds;
                lvDoc.DataBind();
            }
            else
            {
                objCommon.DisplayMessage("Data Not Found...", this.Page);


            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            //string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";

            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\" href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameitledoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }


    }
    //protected void imgdow_Click(object sender, ImageClickEventArgs e)
    //{
    //    string Url = string.Empty;
    //    string directoryPath = string.Empty;
    //    try
    //    {
    //        //string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    //        //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
    //        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
    //        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

    //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
    //        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
    //        string directoryName = "~/DownloadImg" + "/";
    //        directoryPath = Server.MapPath(directoryName);

    //        if (!Directory.Exists(directoryPath.ToString()))
    //        {

    //            Directory.CreateDirectory(directoryPath.ToString());
    //        }
    //        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
    //        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
    //        var ImageName = img;
    //        if (img == null || img == "")
    //        {
    //            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
    //            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    //            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    //            embed += "</object>";
    //            ltEmbed.Text = "Image Not Found....!";
               
               
    //        }
    //        else
    //        {
    //            DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
    //            var blob = blobContainer.GetBlockBlobReference(ImageName);

    //            string filePath = directoryPath + "\\" + ImageName;

    //            if ((System.IO.File.Exists(filePath)))
    //            {
    //                System.IO.File.Delete(filePath);
    //            }
    //            blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
    //            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
    //            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    //            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    //            embed += "</object>";
    //            ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                
    //        }
            
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        
        ViewState["asno"] = null;
        try
        {

            LinkButton btnEdit = sender as LinkButton;
            int assignno = int.Parse(btnEdit.CommandArgument);
            ViewState["asno"] = assignno;
            ShowDetail(assignno, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["idno"].ToString()));
            GetAttachmentByAsNo(assignno);

            ViewState["action"] = "edit";
            pnlAssignDetail.Visible = true;
            pnlDisplayMarks.Visible = false;
            ControlReset(false);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool ValidDocType()
    {
        try
        {
            string doc_type = objCommon.LookUp("ACD_IASSIGNMASTER", "DOC_TYPE_ID", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + "");
            string maxFile = objCommon.LookUp("ACD_IASSIGNMASTER", "MAX_NO_OF_FILE", "AS_NO=" + Convert.ToInt32(ViewState["asno"]) + "");

            if (doc_type == "2" && Session["Attachments"] == null && ((DataTable)Session["Attachments"]) == null)
            {
                objCommon.DisplayUserMessage(updReplyAssign, "Assignment is Upload Based, Please upload Attachment !", this.Page);
                btnSave.Enabled = false;
                return false;
            }
        }
        catch (Exception ex)
        {

        }
        return true;
    }
}


