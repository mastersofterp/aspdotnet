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

public partial class Itle_SyllabusMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ISyllabus objSub = new ISyllabus();
    ISyllabusController objSC = new ISyllabusController();
    BlobController objBlob = new BlobController();

    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    decimal File_size;
    string PageId;

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
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                //Page Authorization
                //Page Authorization
                if (Session["Page"] == null)
                {
                    CheckPageAuthorization();
                    Session["Page"] = 1;
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                BindListView();
            }
        }

        // Used to get maximum size of file attachment
        txtSyllabus.Text = Session["ICourseName"].ToString();
        txtSyllabus.Enabled = false;
        GetAttachmentSize();
        BlobDetails();
    }

    #endregion

    #region Private Method

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("SyllabusMaster.aspx");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_SyllabusMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_SyllabusMaster.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            ISyllabus objSub = new ISyllabus();
            ISyllabusController objSC = new ISyllabusController();

            objSub.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            DataSet ds = objSC.GetSyllabusByUaNo(objSub);

            lvSyllabus.DataSource = ds;
            lvSyllabus.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_SyllabusMaster.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        //txtSyllabus.Text = string.Empty;
        ViewState["action"] = null;
        txtTopic.Text = string.Empty;
        txtUnit.Text = string.Empty;
        //lblStatus.Text = string.Empty;
        ftbDescription.Text = string.Empty;
        lblPreAttach.Text = string.Empty;
    }

    private void ShowDetail(int sub_no, int courseno, int sessionno, int ua_no)
    {
        try
        {
            ISyllabusController objSC = new ISyllabusController();

            ViewState["sub_no"] = sub_no;
            DataTableReader dtr = objSC.GetSingleSyllabus(Convert.ToInt32(ViewState["sub_no"]), courseno, sessionno, ua_no);

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    ftbDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    //TXT.Text = dtr["STARTDATE"] == null ? "" : Convert.ToDateTime(dtr["STARTDATE"].ToString()).ToString("dd-MMM-yyyy");
                    txtSyllabus.Text = dtr["SYLLABUS_NAME"] == null ? "" : dtr["SYLLABUS_NAME"].ToString();
                    txtUnit.Text = dtr["UNIT_NAME"] == null ? "" : dtr["UNIT_NAME"].ToString();
                    txtTopic.Text = dtr["TOPIC_NAME"] == null ? "" : dtr["TOPIC_NAME"].ToString();
                    hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    //lblCurrdate.Text = dtr["STARTDATE"] == null ? "" : Convert.ToDateTime(dtr["STARTDATE"].ToString()).ToString("dd-MMM-yyyy");
                    //lblPreAttach.Visible = false;
                    //lblPreAttach.Text = "";


                    lblPreAttach.Visible = false;
                    lblPreAttach.Text = "";
                    if (dtr["ATTACHMENT"] != null)
                    {
                        lblPreAttach.Visible = true;
                        lblPreAttach.Text = dtr["ATTACHMENT"].ToString();
                    }
                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_ELibraryMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]); //",COURSE_NAME=" + Session["ICourseName"] +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "SyllabusMaster.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

       
        CheckPageRefresh();
        try
        {
            string DOCFOLDER = file_path + "ITLE\\upload_files\\SYLLABUS";

            if (!System.IO.Directory.Exists(DOCFOLDER))
            {
                System.IO.Directory.CreateDirectory(DOCFOLDER);

            }
            ISyllabus objSub = new ISyllabus();
            ISyllabusController objSC = new ISyllabusController();

            string filename = string.Empty;
            objSub.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objSub.SYLLABUS_NAME = txtSyllabus.Text.Trim();
            objSub.TOPIC_NAME = txtTopic.Text.Trim();
            objSub.UNIT_NAME = txtUnit.Text.Trim();
            objSub.DESCRIPTION = ftbDescription.Text.Trim();
            objSub.CREATED_DATE = Convert.ToDateTime(lblCurrdate.Text);
            objSub.COLLEGE_CODE = Session["colcode"].ToString();
            objSub.ATTACHMENT = fuSyllabus.FileName;
            
          

            if (lblBlobConnectiontring.Text != "")
            {
                objSub.IsBlob = 1;
            }
            else
            {
                objSub.IsBlob = 0;
            }




            bool result1;
            string FilePath = string.Empty;
           
            if (fuSyllabus.HasFile == true)
            {
                string fileName = System.Guid.NewGuid().ToString() + fuSyllabus.FileName.Substring(fuSyllabus.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);
                string ext = System.IO.Path.GetExtension(fuSyllabus.PostedFile.FileName);


                int sub_no = Convert.ToInt32(objCommon.LookUp("ACD_ISYLLABUS", "(ISNULL(MAX(SUB_NO),0))+1 AS SUB_NO", ""));  

                filename = sub_no + "_SyllabusMaster_" + sub_no;

                objSub.FILE_PATH = sub_no + "_SyllabusMaster_" + sub_no + ext;

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));
                DataSet dsPURPOSE = new DataSet();

                dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");



                if (count != 0)
                {

                    if (fuSyllabus.PostedFile.ContentLength < File_size || fuSyllabus.HasFile.ToString() == "")
                    {

                        objSub.ATTACHMENT = fuSyllabus.FileName;

                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        result1 = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                        if (result1 == true)
                        {

                            int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, fuSyllabus);
                            if (retval == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                return;
                            }

                            objSub.ATTACHMENT = fuSyllabus.FileName;
                           
                       
                        }
                        else
                        {
                            objSub.ATTACHMENT = fuSyllabus.FileName;

                        }

                        //fileUploader.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);

                        return;

                    }
                }
                else
                {
                    string Extension = "";
                    for (int i = 0; i < dsPURPOSE.Tables[0].Rows.Count; i++)
                    {
                        if (Extension == "")
                            Extension = dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                        else
                            Extension = Extension + ", " + dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                    }
                    objCommon.DisplayMessage("Upload Supported File Format.Please Upload File In " + Extension, this);
                    return;
                }

            }

            if (txtUnit.Text.Trim() == "" && objSub.ATTACHMENT == null)
            {
                objCommon.DisplayUserMessage(UpdSyllabus, "....Please enter valid data or attach file", this.Page);
                return;
            }

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objSub.SUB_NO = Convert.ToInt32(ViewState["sub_no"]);


                objSub.OLDFILENAME = hdnFile.Value;


                if (hdnFile.Value != "" && hdnFile.Value != null && fuSyllabus.HasFile == false)
                {
                    objSub.ATTACHMENT = hdnFile.Value;
                }

                CustomStatus cs = (CustomStatus)objSC.UpdateSyllabus(objSub, fuSyllabus);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(UpdSyllabus, "....Record Modified", this.Page);
                }
                else
                    if (cs.Equals(CustomStatus.FileExists))
                    {
                        objCommon.DisplayUserMessage(UpdSyllabus, "Server Unavailable", this.Page);
                    }
            }
            else
            {
                bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    ClearControls();
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objSC.AddSyllabus(objSub, fuSyllabus);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(UpdSyllabus, "Record Saved  Successfully...", this.Page);
                    }


                    else
                        if (cs.Equals(CustomStatus.FileExists))
                        {
                            objCommon.DisplayUserMessage(UpdSyllabus, "Server Unavailable", this.Page);
                        }
                }
            }
            BindListView();
            ClearControls();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_SyllabusMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        //Response.Redirect("SyllabusMaster.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int sub_no = int.Parse(btnEdit.CommandArgument);

            ShowDetail(sub_no, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SyllabusMaster.aspx.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int sub_no = int.Parse(btnDel.CommandArgument);

            //IAnnouncementController objAC = new IAnnouncementController();

            CustomStatus cs = (CustomStatus)objSC.DeleteSyllabus(Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), sub_no);
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayUserMessage(UpdSyllabus, "Record Deleted Successfully...", this.Page);
            //lblStatus.Text = "Record Deleted Successfully...";
            BindListView();

        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UpdSyllabus, "ITLE_AnnouncementMaster.btnDelete_Click->  " + ex.Message + ex.StackTrace, this.Page);
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
    protected void btnViewSyllabus_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_Course_Syllabus", "Itle_View_Syllabus.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "SyllabusMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    #endregion

    #region Attachments

    //Get Page Id for getting File size from configuration
    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));

            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));

                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);
                }



        }
        catch (Exception ex)
        {

        }

    }

    public string GetFileName(object filename, object sub_no)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/SYLLABUS/SYLLABUS_" + Convert.ToInt32(sub_no) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/SYLLABUS/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        if (status.ToString().Equals("Expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    //To DownLoad File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.Text;
        string filePath = file_path + "Itle/upload_files/SYLLABUS/" + "SYLLABUS_" + Convert.ToInt32(an_no) + System.IO.Path.GetExtension(fileName);

        Response.Redirect("DownloadAttachment.aspx?file=" + filePath + "&filename=" + fileName);
        //HttpContext.Current.Response.ContentType = "Text/Doc";
        ////HttpContext.Current.Response.ContentType = "application/octet-stream";
        //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        //HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        //HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";


    }

    #endregion

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();  

        dsPURPOSE = objCommon.FillDropDown("ACD_ISYLLABUS", "*", "UNIT_NAME='" + txtUnit.Text + "'", "TOPIC_NAME='" + txtTopic.Text +"AND COURSENO="+Convert.ToInt32(Session["ICourseNo"])+ "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}
